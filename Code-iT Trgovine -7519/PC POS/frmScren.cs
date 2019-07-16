using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PCPOS
{
    public partial class frmScren : Form
    {
        public frmScren()
        {
            InitializeComponent();
        }

        public frmMenu MainForm { get; set; }
        public bool kartoteka { get; set; }
        public string id_kasa { get; set; }
        public string id_ducan { get; set; }
        public string naplatni_uredaj_faktura { get; set; }

        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private void frmScren_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Class.Postavke.changeBackground);

            try
            {
                id_kasa = DTpostavke.Rows[0]["default_blagajna"].ToString();
                naplatni_uredaj_faktura = DTpostavke.Rows[0]["naplatni_uredaj_faktura"].ToString();
            }
            catch
            {
                MessageBox.Show("Kasa nije odabrana. Provjerite postavke programa.", "Upozorenje!");
                id_kasa = "0";
            }
            try
            {
                id_ducan = DTpostavke.Rows[0]["default_ducan"].ToString();
            }
            catch
            {
                MessageBox.Show("Dućan nije odabran. Provjerite postavke programa.", "Upozorenje!");
                id_ducan = "0";
            }

            pictureBox3.Visible = kartoteka;
            lblKartoteka.Visible = kartoteka;

            try
            {
                label4.Text = "Verzija programa: " + Properties.Settings.Default.verzija_programa;
                PCPOS.Util.classFukcijeZaUpravljanjeBazom B = new Util.classFukcijeZaUpravljanjeBazom("MALOPRODAJA", "POS");
                int trenutnaG = B.UzmiGodinuKojaSeKoristi();

                if (trenutnaG == DateTime.Now.Year)
                {
                    lblTrenutnaGodina.ForeColor = Color.White;
                    timerUpozoranaNaKrivuGodinu.Stop();
                }
                else
                {
                    lblTrenutnaGodina.ForeColor = Color.Red;
                    timerUpozoranaNaKrivuGodinu.Interval = 500;
                    timerUpozoranaNaKrivuGodinu.Start();
                }
                lblTrenutnaGodina.Text = "Trenutno koristite " + trenutnaG + " g:";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            timer2.Start();
            SetMoneyValue();
            timer1.Start();
            PieStart(10);
            txtBrojDana.Text = "10";

            //DataTable DT = OstaleFunkcije.DSaktivnosDok;

            if (Class.Dokumenti.kalkulacije != true) // DT.Rows[0]["kalkulacije"].ToString() != "1"
            {
                button1.Enabled = false;
            }
            if (Class.Dokumenti.fakture != true) //DT.Rows[0]["faktura"].ToString() != "1"
            {
                button2.Enabled = false;
            }
            if (Class.Dokumenti.ponude != true) //DT.Rows[0]["ponude"].ToString() != "1"
            {
                button3.Enabled = false;
            }
            if (Class.Dokumenti.otpremnice != true) //DT.Rows[0]["otpremnica"].ToString() != "1"
            {
                button7.Enabled = false;
            }
            if (Class.Postavke.grafovi == false) //DTpostavke.Rows[0]["grafovi"].ToString() == "0"
            {
                chbFakture.Visible = false;
                cbMaloprodaja.Visible = false;
                chbIfb.Visible = false;
                label2.Visible = false;
                label1.Visible = false;
                txtBrojDana.Visible = false;
                Chart1.Visible = false;
                tableLayoutPanel1.Visible = false;
            }
            else
            {
                chbFakture.Visible = true;
                cbMaloprodaja.Visible = true;
                label2.Visible = true;
                chbIfb.Visible = true;
                label1.Visible = true;
                txtBrojDana.Visible = true;
                Chart1.Visible = true;
                tableLayoutPanel1.Visible = true;
            }

            //this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private string OO(string s)
        {
            try
            {
                return Convert.ToDecimal(s).ToString("#0.00");
            }
            catch (Exception)
            {
                return "0";
            }
        }

        private string SetValue()
        {
            string html = "";
            string sql_liste = "SELECT " +
                " racun_stavke.sifra_robe AS sifra," +
                " roba.naziv AS naziv," +
                " racun_stavke.mpc ," +
                " CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC) AS kolicina," +
                " roba_prodaja.kolicina AS skl_kol" +
                " FROM racun_stavke" +
                " LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe" +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=racun_stavke.id_skladiste" +
                " LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa" +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=racun_stavke.sifra_robe AND roba_prodaja.id_skladiste=racun_stavke.id_skladiste " +
                " WHERE  cast(racuni.datum_racuna as date) >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND cast(racuni.datum_racuna as date) <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                " AND racun_stavke.id_ducan='" + id_ducan + "' AND racun_stavke.id_kasa='" + id_kasa + "'";

            DataTable DT = classSQL.select(sql_liste, "roba").Tables[0];

            string sql_fak = "SELECT " +
                " faktura_stavke.sifra AS sifra," +
                " roba.naziv AS naziv ," +
                " ROUND(faktura_stavke.vpc*(1zbroj(CAST(REPLACE(faktura_stavke.porez,',','.') AS numeric)/100)),2) as mpc ," +
                " CAST(REPLACE(faktura_stavke.kolicina,',','.') AS NUMERIC) AS kolicina," +
                " roba_prodaja.kolicina AS skl_kol " +
                " FROM faktura_stavke" +
                " LEFT JOIN roba ON roba.sifra=faktura_stavke.sifra" +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=faktura_stavke.id_skladiste" +
                " LEFT JOIN fakture ON fakture.broj_fakture=faktura_stavke.broj_fakture" +
                " AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa" +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=faktura_stavke.sifra AND roba_prodaja.id_skladiste=faktura_stavke.id_skladiste " +
                " WHERE  cast(fakture.date as date) > '" + DateTime.Now.AddDays(0).ToString("yyyy-MM-dd") + "' AND fakture.date<'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " AND faktura_stavke.id_ducan='" + id_ducan + "' AND faktura_stavke.id_kasa='" + naplatni_uredaj_faktura + "'";

            DataTable DTf = classSQL.select(sql_fak, "roba").Tables[0];

            foreach (DataRow row in DT.Rows)
            {
                html += "<tr style=\" font-size:12px; font-family:Arial, Helvetica, sans-serif;\">\n<td>" + row["sifra"].ToString() + "</td>" +
                    "<td>" + row["naziv"].ToString() + "</td>\n" +
                    "<td>" + row["kolicina"].ToString() + "</td>\n" +
                    "<td>" + row["skl_kol"].ToString() + "</td>\n" +
                    "<td>" + OO(row["mpc"].ToString()) + "</td>\n" +
                    "<td>Račun</td>\n" +
                    "</tr>";
            }

            foreach (DataRow row in DTf.Rows)
            {
                html += "<tr style=\" font-size:12px; font-family:Arial, Helvetica, sans-serif;\">\n<td>" + row["sifra"].ToString() + "</td>" +
                    "<td>" + row["naziv"].ToString() + "</td>\n" +
                    "<td>" + row["kolicina"].ToString() + "</td>\n" +
                    "<td>" + row["skl_kol"].ToString() + "</td>\n" +
                    "<td>" + OO(row["mpc"].ToString()) + "</td>\n" +
                    "<td>Faktura</td>\n" +
                    "</tr>";
            }

            return html;
        }

        private void Upload(string fakture, string maloprodaja, string kartice, string gotovina)
        {
            try
            {
                string html = "<html><head><meta name=\"viewport\" content=\"width=device-width\"/>\n" +
                    "<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\" />\n" +
                    "</head>\n" +
                    "<body style=\"\">\n" +
                    "<script>function prikaz(){document.getElementById('tablica').style.visibility='visible';}</script>\n" +
                    "<div style=\"  text-align:left; width:300px; margin-top:20px; font-family:Tahoma, Geneva, sans-serif; font-size:16px;\">\n" +
                    "<div style=\" font-size:20px; font-weight:bold;\">Stanje prodaje:</div><br/>\n" +
                    "<div>" + maloprodaja + "</div>\n" +
                    "<div>" + gotovina + "</div>\n" +
                    "<div>" + kartice + "</div>\n" +
                    "<div>" + fakture + "</div><br/>\n" +
                    "<input type=\"button\" onClick=\"prikaz();\" style=\"width:200px; height:40px;\" value=\"Promet po robi\"/>\n" +
                    "<br/><br/><br/>\n<table id='tablica' border=\"1px\" style=\"width:700px;  visibility:hidden;\">\n<tr style=\" background-color:#666; font-weight:bold; color:#FFF;\">\n<td style=\"width:140px; padding:5px;\">Šifra</td><td>Naziv</td>\n<td style=\"width:50px;\">Kol</td>\n<td style=\"width:50px;\">Kol.skl</td><td style=\"width:60px;\">MPC</td><td style=\"width:60px;\">Dok.</td></tr>\n" +
                    SetValue() +
                    "</table>" +
                    "</div>" +
                    "</body>" +
                    "</html>";
                File.WriteAllText("index.html", html);

                //
                if (!Util.CheckConnection.Check()) return;
                //

                FtpWebRequest requestFTPUploader = (FtpWebRequest)WebRequest.Create(DTpostavke.Rows[0]["salji_na_web_ftp"].ToString());
                requestFTPUploader.Credentials = new NetworkCredential(DTpostavke.Rows[0]["salji_na_web_user"].ToString(), DTpostavke.Rows[0]["salji_na_web_pass"].ToString());
                requestFTPUploader.Method = WebRequestMethods.Ftp.UploadFile;

                FileInfo fileInfo = new FileInfo("index.html");
                FileStream fileStream = fileInfo.OpenRead();

                int bufferLength = 2048;
                byte[] buffer = new byte[bufferLength];

                Stream uploadStream = requestFTPUploader.GetRequestStream();
                int contentLength = fileStream.Read(buffer, 0, bufferLength);

                while (contentLength != 0)
                {
                    uploadStream.Write(buffer, 0, contentLength);
                    contentLength = fileStream.Read(buffer, 0, bufferLength);
                }

                uploadStream.Close();
                fileStream.Close();

                requestFTPUploader = null;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        private void PieStart(int br)
        {
            //Random random = new Random();
            Chart1.Series["Series1"].Points.Clear();

            for (int i = 1; i < br + 1; i++)
            {
                DataTable DT = new DataTable();
                DataTable DT_fak = new DataTable();
                DataTable DT_ifb = new DataTable();

                string sql = "SELECT SUM(ukupno) AS [ukupno] FROM racuni " +
                    " WHERE datum_racuna>'" + DateTime.Today.AddDays((i - br)).ToString("yyyy-MM-dd H:mm:ss") + "' AND datum_racuna<'" + DateTime.Today.AddDays((i - br) + 1).ToString("yyyy-MM-dd H:mm:ss") + "' AND id_ducan=" + id_ducan + " AND id_kasa=" + id_kasa +
                    "";
                DT = classSQL.select(sql, "racuni").Tables[0];

                string sql_fak = "SELECT SUM(ukupno) AS [ukupno] FROM fakture " +
                " WHERE date>'" + DateTime.Today.AddDays((i - br)).ToString("yyyy-MM-dd H:mm:ss") + "' AND date<'" + DateTime.Today.AddDays((i - br) + 1).ToString("yyyy-MM-dd H:mm:ss") + "'  AND id_ducan='" + id_ducan + "' AND id_kasa='" + naplatni_uredaj_faktura + "' " +
                "";
                DT_fak = classSQL.select(sql_fak, "racuni").Tables[0];

                string sql_ifb = "SELECT COALESCE(SUM(ukupno),0) AS [ukupno] FROM ifb " +
                " WHERE datum>'" + DateTime.Today.AddDays((i - br)).ToString("yyyy-MM-dd H:mm:ss") + "' AND datum<'" + DateTime.Today.AddDays((i - br) + 1).ToString("yyyy-MM-dd H:mm:ss") + "'" +
                "";
                DT_ifb = classSQL.select(sql_ifb, "racuni").Tables[0];

                double sveM = 0;

                if (DT.Rows[0]["ukupno"].ToString() != "")
                {
                    if (cbMaloprodaja.Checked)
                        sveM = Convert.ToDouble(DT.Rows[0]["ukupno"].ToString());
                }

                if (DT_fak.Rows[0]["ukupno"].ToString() != "")
                {
                    if (chbFakture.Checked)
                        sveM = Convert.ToDouble(DT_fak.Rows[0]["ukupno"].ToString()) + sveM;
                }

                double ifb_ukupno;
                double.TryParse(DT_ifb.Rows[0][0].ToString(), out ifb_ukupno);
                sveM += ifb_ukupno;

                Chart1.Series["Series1"].Points.AddY(sveM.ToString("#0.00").Replace(",", "."));

                if (sveM != 0)
                {
                    Chart1.Series["Series1"].Points[i - 1].MarkerStyle = MarkerStyle.Circle;
                    Chart1.Series["Series1"].Points[i - 1].MarkerSize = 5;
                    if (sveM > 0)
                        Chart1.Series["Series1"].Points[i - 1].MarkerColor = Color.Green;
                    else
                        Chart1.Series["Series1"].Points[i - 1].MarkerColor = Color.Red;
                }
            }

            Chart1.Series["Series1"].ChartType = SeriesChartType.SplineArea;
            Chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(106, 86, 5);
            Chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(106, 86, 5);
            Chart1.Series["Series1"].BorderColor = Color.FromArgb(31, 53, 79);
            Chart1.Series["Series1"].Color = Color.FromArgb(128, 31, 53, 79);
            /*Chart1.Series["Series1"]["PointWidth"] = "0.5";
            if (br <= 20)
            {
                Chart1.Series["Series1"].IsValueShownAsLabel = true;
            }
            else
            {
                Chart1.Series["Series1"].IsValueShownAsLabel = false;
            }
            Chart1.Series["Series1"]["BarLabelStyle"] = "Center";
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            Chart1.Series["Series1"]["DrawingStyle"] = "Cylinder";*/
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((byte)(105)), ((byte)(170)), ((byte)(197)));
            Color y = System.Drawing.Color.FromArgb(((byte)(40)), ((byte)(109)), ((byte)(135)));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        public void SetMoneyValue()
        {
            try
            {
                DataTable DT = new DataTable();
                DataTable DT3 = new DataTable();

                string sql = "SELECT SUM(ukupno) AS [ukupno],SUM(ukupno_gotovina-(ukupno_karticezbrojukupno_gotovina-ukupno)) AS [gotovina],SUM(ukupno_kartice) AS [kartice],SUM(ukupno_virman) AS [virman] FROM racuni " +
                    " WHERE datum_racuna>'" + DateTime.Today.AddDays(0).ToString("yyyy-MM-dd H:mm:ss") + "' AND datum_racuna<'" + DateTime.Today.AddDays(+1).ToString("yyyy-MM-dd H:mm:ss") + "' AND id_ducan=" + id_ducan + " AND id_kasa=" + id_kasa;

                DT = classSQL.select(sql, "racuni").Tables[0];
                if (DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "")
                {
                    label8.Text = "Maloprodaja ukupno: " + Math.Round(Convert.ToDouble(DT.Rows[0]["ukupno"].ToString()), 2).ToString("#0.00") + " kn";
                    label10.Text = "Blagajna gotovina: " + Math.Round(Convert.ToDouble(DT.Rows[0]["gotovina"].ToString()), 2).ToString("#0.00") + " kn";
                    label11.Text = "Kartice ukupno: " + Math.Round(Convert.ToDouble(DT.Rows[0]["kartice"].ToString()), 2).ToString("#0.00") + " kn";
                }
                else
                {
                    label8.Text = "Maloprodaja ukupno: 0.00 kn";
                    label10.Text = "Blagajna gotovina: 0.00 kn";
                    label11.Text = "Kartice ukupno: 0.00 kn";
                }

                string sql3 = "SELECT SUM(ukupno) AS [Ukupno] FROM fakture " +
                    " WHERE date>'" + DateTime.Today.AddDays(0).ToString("yyyy-MM-dd H:mm:ss") + "' AND date<'" + DateTime.Today.AddDays(+1).ToString("yyyy-MM-dd H:mm:ss") + "'" +
                    " AND id_ducan='" + id_ducan + "' AND id_kasa='" + naplatni_uredaj_faktura + "'" +
                    "";
                DT3 = classSQL.select(sql3, "fakture").Tables[0];
                string sql_ifb = "SELECT COALESCE(SUM(ukupno),0) AS [ukupno] FROM ifb " +
                " WHERE datum>'" + DateTime.Today.AddDays(0).ToString("yyyy-MM-dd H:mm:ss") + "' AND datum<'" + DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59).ToString("yyyy-MM-dd H:mm:ss") + "'" +
                "";
                DataTable DT_ifb = classSQL.select(sql_ifb, "racuni").Tables[0];

                double ukupno_ifb = 0;
                double ukupno_fak = 0;
                if (DT_ifb.Rows.Count > 0)
                {
                    double.TryParse(DT_ifb.Rows[0][0].ToString(), out ukupno_ifb);
                }

                if (DT3.Rows.Count > 0)
                {
                    double.TryParse(DT3.Rows[0][0].ToString(), out ukupno_fak);
                }

                if ((ukupno_ifb + ukupno_fak) > 0)
                {
                    label9.Text = "Fakture ukupno: " + Math.Round((ukupno_fak + ukupno_ifb), 2).ToString("#0.00") + " kn";
                }
                else
                {
                    label9.Text = "Fakture ukupno: 0.00 kn";
                }

                if (DTpostavke.Rows[0]["salji_na_web"].ToString() == "1")
                {
                    if (!backgroundWorker1.IsBusy)
                        backgroundWorker1.RunWorkerAsync();
                }

                monthCalendar1.SelectionStart = DateTime.Today;
                monthCalendar1.SelectionEnd = DateTime.Today;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            try
            {
                DataTable DT = new DataTable();
                DataTable DT3 = new DataTable();

                string sql = "SELECT SUM(ukupno) AS [ukupno],SUM(ukupno_gotovina-(ukupno_karticezbrojukupno_gotovina-ukupno)) AS [gotovina],SUM(ukupno_kartice) AS [kartice],SUM(ukupno_virman) AS [virman] FROM racuni " +
                    " WHERE datum_racuna>'" + monthCalendar1.SelectionStart.AddDays(0).ToString("yyyy-MM-dd H:mm:ss") + "' AND datum_racuna<'" + monthCalendar1.SelectionStart.AddDays(+1).ToString("yyyy-MM-dd H:mm:ss") + "' AND id_ducan=" + id_ducan + " AND id_kasa=" + id_kasa;
                DT = classSQL.select(sql, "racuni").Tables[0];

                //MessageBox.Show(monthCalendar1.SelectionStart.ToString());

                if (DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "")
                {
                    label8.Text = "Maloprodaja ukupno: " + Math.Round(Convert.ToDouble(DT.Rows[0]["ukupno"].ToString()), 2).ToString("#0.00") + " kn";
                    label10.Text = "Blagajna gotovina: " + Math.Round(Convert.ToDouble(DT.Rows[0]["gotovina"].ToString()), 2).ToString("#0.00") + " kn";
                    label11.Text = "Kartice ukupno: " + Math.Round(Convert.ToDouble(DT.Rows[0]["kartice"].ToString()), 2).ToString("#0.00") + " kn";
                }
                else
                {
                    label8.Text = "Maloprodaja ukupno: 0.00 kn";
                    label10.Text = "Blagajna gotovina: 0.00 kn";
                    label11.Text = "Kartice ukupno: 0.00 kn";
                }

                string sql3 = "SELECT SUM(ukupno) AS [Ukupno] FROM fakture " +
                " WHERE date>'" + monthCalendar1.SelectionStart.AddDays(0).ToString("yyyy-MM-dd H:mm:ss") + "' AND date<'" + monthCalendar1.SelectionStart.AddDays(+1).ToString("yyyy-MM-dd H:mm:ss") + "' AND id_ducan='" + id_ducan + "' AND id_kasa='" + naplatni_uredaj_faktura + "'" +
                "";
                DT3 = classSQL.select(sql3, "fakture").Tables[0];

                string sql_ifb = "SELECT COALESCE(SUM(ukupno),0) AS [ukupno] FROM ifb " +
                " WHERE datum >= '" + monthCalendar1.SelectionStart.AddDays(0).ToString("yyyy-MM-dd H:mm:ss") + "' AND datum <= '" + monthCalendar1.SelectionStart.AddHours(23).AddMinutes(59).AddSeconds(59).ToString("yyyy-MM-dd H:mm:ss") + "'" +
                "";
                DataTable DT_ifb = classSQL.select(sql_ifb, "racuni").Tables[0];

                double ukupno_ifb = 0;
                double ukupno_fak = 0;
                if (DT_ifb.Rows.Count > 0)
                {
                    double.TryParse(DT_ifb.Rows[0][0].ToString(), out ukupno_ifb);
                }

                if (DT3.Rows.Count > 0)
                {
                    double.TryParse(DT3.Rows[0][0].ToString(), out ukupno_fak);
                }

                if ((ukupno_ifb + ukupno_fak) > 0)
                {
                    label9.Text = "Fakture ukupno: " + Math.Round((ukupno_fak + ukupno_ifb), 2).ToString("#0.00") + " kn";
                }
                else
                {
                    label9.Text = "Fakture ukupno: 0.00 kn";
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w + 7, h + 7);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w - 7, h - 7);
        }

        private INIFile ini = new INIFile();

        private void picMaloprodaj_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            foreach (Form OpenForm in Application.OpenForms)
            {
                if (OpenForm.GetType() == typeof(frmKasa))
                {
                    OpenForm.WindowState = FormWindowState.Maximized;
                    return;
                }
            }

            if (ini.Read("POSTAVKE", "paragonac") == "1")
            {
                frmParagonac p = new frmParagonac();
                //p.MainForm = MainForm;
                p.Show();
            }
            else
            {
                frmKasa ks = new frmKasa();
                ks.MainForm = MainForm;
                ks.Show();
            }
        }

        private void picKalk_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            frmNovaKalkulacija2 nova_kalkulacija = new frmNovaKalkulacija2();
            nova_kalkulacija.MdiParent = MainForm;
            nova_kalkulacija.Dock = DockStyle.Fill;
            nova_kalkulacija.MainForm = MainForm;
            nova_kalkulacija.Show();
        }

        private void picFak_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            if (ini.Read("POSTAVKE", "paragonac") == "0" || ini.Read("POSTAVKE", "paragonac") == "")
            {
                frmFaktura f = new frmFaktura();
                f.MdiParent = MainForm;
                f.Dock = DockStyle.Fill;
                f.MainForm = MainForm;
                f.Show();
            }
            else
            {
                Robno.frmFakturaBezRobe FR = new Robno.frmFakturaBezRobe();
                FR.MdiParent = MainForm;
                FR.Dock = DockStyle.Fill;
                FR.MainForm = MainForm;
                FR.Show();
            }
        }

        private void picPonude_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmPonude f = new frmPonude();
            f.MdiParent = MainForm;
            f.Dock = DockStyle.Fill;
            f.MainForm = MainForm;
            f.Show();
        }

        private void picRoba_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmRobaUsluge f = new frmRobaUsluge();
            f.MdiParent = MainForm;
            //f.Dock = DockStyle.Fill;
            f.MainFormMenu = MainForm;
            f.Show();
        }

        private void picPartner_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Sifarnik.frmAddPartners f = new Sifarnik.frmAddPartners();
            f.MdiParent = MainForm;
            f.Focus();
            f.TopMost = true;
            f.LayoutMdi(MdiLayout.Cascade);
            //f.Dock = DockStyle.Fill;
            f.MainFormMenu = MainForm;
            f.Show();
        }

        private void picOtpremnica_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmOtpremnica f = new frmOtpremnica();
            f.MdiParent = MainForm;
            f.Dock = DockStyle.Fill;
            f.MainForm = MainForm;
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetMoneyValue();
        }

        private void monthCalendar1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                SetMoneyValue();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SetMoneyValue();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmKasaPrijava kp = new frmKasaPrijava();
            kp.ShowDialog();

            //this.Close();
            //MainForm.zatvori = true;
            //MainForm.Activate();
            //MainForm.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lblSat.Text = DateTime.Now.ToString("H:mm:ss");
        }

        private void txtBrojDana_TextChanged(object sender, EventArgs e)
        {
            if (txtBrojDana.Text != "" || txtBrojDana.Text == "0")
            {
                int broj = 0;
                if (!int.TryParse(txtBrojDana.Text, out broj))
                {
                    MessageBox.Show("Greška kod upisa.", "Greška"); return;
                }
                PieStart(broj);
            }
        }

        private void cbMaloprodaja_CheckedChanged(object sender, EventArgs e)
        {
            PieStart(Convert.ToInt16(txtBrojDana.Text));
        }

        private void chbFakture_CheckedChanged(object sender, EventArgs e)
        {
            PieStart(Convert.ToInt16(txtBrojDana.Text));
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Upload(label9.Text, label8.Text, label11.Text, label10.Text);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //string path = GetApplicationPath();
            //string path_plusapp = path + "\\KARTOTEKA.exe";
            ////File.WriteAllText(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/KartotekaUpdate.txt", path, Encoding.UTF8);
            //if (File.Exists(path_plusapp.Replace("file:\\","")))
            //{
            //    Process.Start(path + "\\KARTOTEKA.exe");
            //}
            PCPOS.Kartoteka.frmMenuKarto kartoteka = new PCPOS.Kartoteka.frmMenuKarto();

            kartoteka.ShowDialog();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                //Process.Start(Path.GetDirectoryName(Application.ExecutablePath) + "\\" + "pc_sc.exe");
                string _path = Path.GetDirectoryName(Application.ExecutablePath) + "\\" + "help1.exe";
                if (!File.Exists(_path))
                {
                    Util.Download.SkidajPodrsku();
                }
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.WorkingDirectory = _path;
                proc.StartInfo.FileName = _path;
                proc.Start();
            }
            catch
            {
                MessageBox.Show("Spajanje na Code-iT nije uspjelo!", "Upozorenje!");
            }
        }

        private void btnPromjenaGodine_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            PCPOS.Util.frmPromjenaGodine pg = new Util.frmPromjenaGodine();
            pg.ShowDialog();
        }

        private void timerUpozoranaNaKrivuGodinu_Tick(object sender, EventArgs e)
        {
            if (lblTrenutnaGodina.Visible == false)
            {
                lblTrenutnaGodina.Visible = true;
            }
            else
            {
                lblTrenutnaGodina.Visible = false;
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Util.frmHtmlInfo iss = new Util.frmHtmlInfo();
            iss.Show();
        }

        private void chbIfb_CheckedChanged(object sender, EventArgs e)
        {
            PieStart(Convert.ToInt16(txtBrojDana.Text));
        }

        private void btnRadniNalogServis_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmRadniNalogSerivs s = new frmRadniNalogSerivs();
            s.ShowDialog();
        }
    }
}