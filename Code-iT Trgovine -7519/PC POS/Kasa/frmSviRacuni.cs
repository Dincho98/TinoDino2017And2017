using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PCPOS.Kasa
{
    public partial class frmSviRacuni : Form
    {
        public frmSviRacuni()
        {
            InitializeComponent();
        }

        private DataSet DSfakture = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DS_Zaposlenik = new DataSet();

        private void frmSviRacuni_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);
            DateTime currentDatetime = DateTime.Now;
            dtpOD.Value = new DateTime(currentDatetime.Year, currentDatetime.Month, currentDatetime.Day, 0, 0, 0);
            dtpDO.Value = new DateTime(currentDatetime.Year, currentDatetime.Month, currentDatetime.Day, 23, 59, 59);

            PaintRows(dgv);
            fillCB();
            fillDataGrid();
            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");
            this.Paint += new PaintEventHandler(Form1_Paint);
            //prikaziBrIspisa();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 500);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void SetDecimalInDgv(DataGridView dg, string column, string column1, string column2)
        {
            for (int i = 0; i < dg.RowCount; i++)
            {
                try
                {
                    if (column != "NE")
                    {
                        dg.Rows[i].Cells[column].Value = Convert.ToDouble(dg.Rows[i].Cells[column].FormattedValue.ToString()).ToString("#0.00");
                    }

                    if (column1 != "NE")
                    {
                        dg.Rows[i].Cells[column1].Value = Convert.ToDouble(dg.Rows[i].Cells[column1].FormattedValue.ToString()).ToString("#0.00");
                    }

                    if (column2 != "NE")
                    {
                        dg.Rows[i].Cells[column2].Value = Convert.ToDouble(dg.Rows[i].Cells[column2].FormattedValue.ToString()).ToString("#0.00");
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void PaintRows(DataGridView dg)
        {
            int br = 0;
            for (int i = 0; i < dg.Rows.Count; i++)
            {
                if (br == 0)
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
                    br++;
                }
                else
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    br = 0;
                }
            }
            DataGridViewRow row = dg.RowTemplate;
            row.Height = 22;
        }

        private void fillDataGrid()
        {
            string top = "";
            string remote = "";
            if (classSQL.remoteConnectionString != "")
            {
                remote = " LIMIT 500";
            }
            else
            {
                top = " TOP(500) ";
            }

            //racuni.broj_ispisa as [Broj ispisa] ,
            string sql = string.Format(@"SELECT {0} racuni.broj_racuna AS [Broj računa], racuni.datum_racuna AS [Datum],
partners.ime_tvrtke AS [Partner], zaposlenici.ime + ' ' + zaposlenici.prezime AS [Blagajnik],
racuni.ukupno as [Ukupno], racuni.storno AS [Storno], ime_ducana AS [Poslovnica], ime_blagajne AS [Uređaj], racuni.id_ducan, racuni.id_kasa
FROM racuni
LEFT JOIN partners ON racuni.id_kupac = partners.id_partner
LEFT JOIN ducan ON racuni.id_ducan = ducan.id_ducan
LEFT JOIN blagajna ON racuni.id_kasa = blagajna.id_blagajna
LEFT JOIN zaposlenici ON racuni.id_blagajnik = zaposlenici.id_zaposlenik
ORDER BY racuni.datum_racuna DESC
{1}", top, remote);

            DSfakture = classSQL.select(sql, "racuni");
            dgv.DataSource = DSfakture.Tables[0];
            prikaziBrIspisa();
            dgv.Columns["id_kasa"].Visible = dgv.Columns["id_ducan"].Visible = false;
            dgv.Columns["Datum"].Width = 110;

            PaintRows(dgv);
        }

        private void btnUrediSifru_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                string sifra = dgv.CurrentRow.Cells["Broj fakture"].Value.ToString();
                //sifra_fakture = sifra;
                //MainForm.broj_fakture_edit = sifra_fakture;
                //MainForm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Niste odabrali niti jednu stavku.", "Greška");
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DateTime dOtp = Convert.ToDateTime(dtpOD.Value);
            string dtOd = dOtp.Month + "." + dOtp.Day + "." + dOtp.Year;

            DateTime dNow = Convert.ToDateTime(dtpDO.Value);
            string dtDo = dNow.Month + "." + dNow.Day + "." + dNow.Year;

            string Broj = "";
            string Partner = "";
            string DateStart = "";
            string DateEnd = "";
            string VD = "";
            string Valuta = "";
            string Izradio = "";
            string SifraArtikla = "";

            if (chbBroj.Checked)
            {
                Broj = string.Format("racuni.broj_racuna = '{0}' AND ", txtBroj.Text);
            }
            if (chbSifra.Checked)
            {
                Partner = string.Format("racuni.id_kupac = '{0}' AND ", txtPartner.Text);
            }
            if (chbOD.Checked)
            {
                DateStart = string.Format("racuni.datum_racuna >= '{0}' AND ", dtpOD.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (chbDO.Checked)
            {
                DateEnd = string.Format("racuni.datum_racuna <= '{0}' AND ", dtpDO.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            if (chbArtikl.Checked)
            {
                SifraArtikla = string.Format("racun_stavke.sifra_robe = '{0}' AND ", cbArtikl.Text);
            }

            if (chbIzradio.Checked)
            {
                Izradio = string.Format("racuni.id_blagajnik = '{0}' AND ", cbIzradio.SelectedValue);
            }

            if (chbPoslovnica.Checked)
            {
                Izradio = string.Format("racuni.id_ducan = '{0}' AND ", cbPoslovnica.SelectedValue);
            }

            if (chbNaplatni.Checked)
            {
                Izradio = string.Format("racuni.id_kasa = '{0}' AND ", cbNaplatni.SelectedValue);
            }

            string filter = Broj + Partner + VD + DateStart + DateEnd + Valuta + SifraArtikla + Izradio;

            if (filter.Length != 0)
            {
                filter = string.Format(" WHERE {0}", filter);
                filter = filter.Remove(filter.Length - 4, 4);
            }

            string top = "";
            string remote = "";
            if (classSQL.remoteConnectionString != "")
            {
                //remote = " LIMIT 500";
            }
            else
            {
                // top = " TOP(500) ";
            }

            string sql = string.Format(@"SELECT DISTINCT {0} racuni.broj_racuna AS [Broj računa], racuni.datum_racuna AS [Datum],
partners.ime_tvrtke AS [Partner], zaposlenici.ime + ' ' + zaposlenici.prezime AS [Blagajnik], racuni.ukupno as [Ukupno], racuni.storno AS [Storno],
ime_ducana AS [Poslovnica], ime_blagajne AS [Uređaj], racuni.id_ducan, racuni.id_kasa
FROM racuni
LEFT JOIN partners ON racuni.id_kupac = partners.id_partner
LEFT JOIN racun_stavke ON racuni.broj_racuna = racun_stavke.broj_racuna AND racuni.id_ducan = racun_stavke.id_ducan AND racuni.id_kasa = racun_stavke.id_kasa
LEFT JOIN ducan ON racuni.id_ducan = ducan.id_ducan
LEFT JOIN blagajna ON racuni.id_kasa = blagajna.id_blagajna
LEFT JOIN zaposlenici ON racuni.id_blagajnik = zaposlenici.id_zaposlenik
{1}
ORDER BY racuni.datum_racuna DESC
{2}", top, filter, remote);

            prikaziBrIspisa();
            DSfakture = classSQL.select(sql, "fakture");
            dgv.DataSource = DSfakture.Tables[0];
            dgv.Columns["id_kasa"].Visible = dgv.Columns["id_ducan"].Visible = false;

            PaintRows(dgv);
            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");
        }

        private void prikaziBrIspisa()
        {
            if (File.Exists("dontshownumbersofprint"))
            {
                try
                {
                    dgv.Columns["Broj ispisa"].Visible = false;
                }
                catch { }
            }
        }

        private void fillCB()
        {
            DataTable DTducan = classSQL.select("SELECT * FROM ducan", "ducan").Tables[0];
            cbPoslovnica.DataSource = DTducan;
            cbPoslovnica.DisplayMember = "ime_ducana";
            cbPoslovnica.ValueMember = "id_ducan";

            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime + ' ' + prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzradio.DataSource = DS_Zaposlenik.Tables[0];
            cbIzradio.DisplayMember = "IME";
            cbIzradio.ValueMember = "id_zaposlenik";
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Class.Postavke.idFaktura == 1)
            {
                Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
                rfak.dokumenat = "RAC";
                rfak.ImeForme = "Račun";
                rfak.poslovnica = dgv.CurrentRow.Cells["id_ducan"].FormattedValue.ToString();
                rfak.naplatni = dgv.CurrentRow.Cells["id_kasa"].FormattedValue.ToString();
                rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj računa"].FormattedValue.ToString();
                rfak.ShowDialog();
            }
            else if (Class.Postavke.idFaktura == 2)
            {
                Report.Faktura.repFakturaNovo rfak = new Report.Faktura.repFakturaNovo();
                rfak.dokumenat = "RAC";
                rfak.ImeForme = "Račun";
                rfak.poslovnica = dgv.CurrentRow.Cells["id_ducan"].FormattedValue.ToString();
                rfak.naplatni = dgv.CurrentRow.Cells["id_kasa"].FormattedValue.ToString();
                rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj računa"].FormattedValue.ToString();
                rfak.ShowDialog();
            }
            else if (Class.Postavke.idFaktura == 3)
            {
                Report.Faktura3.repFaktura3 rfak = new Report.Faktura3.repFaktura3();
                rfak.dokumenat = "RAC";
                rfak.ImeForme = "Račun";
                rfak.poslovnica = dgv.CurrentRow.Cells["id_ducan"].FormattedValue.ToString();
                rfak.naplatni = dgv.CurrentRow.Cells["id_kasa"].FormattedValue.ToString();
                rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj računa"].FormattedValue.ToString();
                rfak.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            if (Class.Postavke.idFaktura == 1)
            {
                Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
                rfak.dokumenat = "RAC";
                rfak.ImeForme = "Račun";
                rfak.poslovnica = dgv.CurrentRow.Cells["id_ducan"].FormattedValue.ToString();
                rfak.naplatni = dgv.CurrentRow.Cells["id_kasa"].FormattedValue.ToString();
                rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj računa"].FormattedValue.ToString();
                rfak.ShowDialog();
            }
            else if (Class.Postavke.idFaktura == 2)
            {
                Report.Faktura.repFakturaNovo rfak = new Report.Faktura.repFakturaNovo();
                rfak.dokumenat = "RAC";
                rfak.ImeForme = "Račun";
                rfak.poslovnica = dgv.CurrentRow.Cells["id_ducan"].FormattedValue.ToString();
                rfak.naplatni = dgv.CurrentRow.Cells["id_kasa"].FormattedValue.ToString();
                rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj računa"].FormattedValue.ToString();
                rfak.ShowDialog();
            }
            else if (Class.Postavke.idFaktura == 3)
            {
                Report.Faktura3.repFaktura3 rfak = new Report.Faktura3.repFaktura3();
                rfak.dokumenat = "RAC";
                rfak.ImeForme = "Račun";
                rfak.poslovnica = dgv.CurrentRow.Cells["id_ducan"].FormattedValue.ToString();
                rfak.naplatni = dgv.CurrentRow.Cells["id_kasa"].FormattedValue.ToString();
                rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj računa"].FormattedValue.ToString();
                rfak.ShowDialog();
            }
        }

        private void chbPoslovnica_CheckedChanged(object sender, EventArgs e)
        {
            cbPoslovnica.Enabled = !chbPoslovnica.Checked;
            cbNaplatni.Enabled = chbPoslovnica.Checked;
            chbNaplatni.Enabled = chbPoslovnica.Checked;

            if (chbPoslovnica.Checked)
            {
                DataTable DTblagajna = classSQL.select(string.Format("SELECT * FROM blagajna WHERE id_ducan = {0}", cbPoslovnica.SelectedValue.ToString()), "blagajna").Tables[0];
                cbNaplatni.DataSource = DTblagajna;
                cbNaplatni.DisplayMember = "ime_blagajne";
                cbNaplatni.ValueMember = "id_blagajna";
            }
            else chbNaplatni.Checked = false;
        }

        #region btnSpremiExcel EXCEL

        private void btnSpremiExcel_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (Directory.Exists(folderBrowserDialog1.SelectedPath))
                {
                    if (b.Name == "btnSpremiExcel")
                    {
                        SpremiSveExcel();
                    }
                    else if (b.Name == "btnSpremiExcelGrupiraj")
                    {
                        SpremiSveExcelGrupirano();
                    }
                }
            }
        }

        private void SpremiSveExcel()
        {
            /*string sql = @"SELECT  racuni.broj_racuna,
                             SUM((porez_na_dohodak_iznos)*(CAST(REPLACE(kolicina,',','.') as numeric))) AS porez_na_dohodakSUM,
                             SUM((prirez_iznos)*(CAST(REPLACE(kolicina,',','.') as numeric))) AS prirez_iznosSUM,
                             SUM((((mpc::numeric)*CAST(REPLACE(kolicina,',','.') as numeric)/100))*(CAST(REPLACE(kolicina,',','.') as numeric))) AS ukupno,
                             racuni.datum_racuna,partners.ime_tvrtke,partners.ime,partners.prezime,partners.oib  FROM racuni
                             LEFT JOIN racuni ON racuni.broj_racuna = racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa
                             LEFT JOIN partners ON racuni.id_kupac=partners.id_partner
                             WHERE racuni.ukupno>'0' " + PripremaFilteraZaSQL() + @"
                             ORDER BY CAST(racuni.broj_racuna AS INT) ASC";*/

            string sql = @"SELECT  racuni.broj_racuna,
                    SUM((porez_na_dohodak_iznos)*(CAST(REPLACE(kolicina,',','.') as numeric))) AS porez_na_dohodakSUM,
                    SUM((prirez_iznos)*(CAST(REPLACE(kolicina,',','.') as numeric))) AS prirez_iznosSUM,
                    SUM(((mpc::numeric)-((mpc::numeric)*CAST(REPLACE(rabat,',','.') as numeric)/100))*(CAST(REPLACE(kolicina,',','.') as numeric))) AS ukupno,
                    racuni.datum_racuna,partners.ime_tvrtke,partners.ime,partners.prezime,partners.oib  FROM racuni
                    LEFT JOIN racun_stavke ON racuni.broj_racuna = racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa
                    LEFT JOIN partners ON racuni.id_kupac=partners.id_partner
                    WHERE racuni.ukupno>'0' " + PripremaFilteraZaSQL() + @"
                    GROUP BY racuni.broj_racuna,racuni.datum_racuna,partners.ime_tvrtke,partners.ime,partners.prezime,partners.oib
                    ORDER BY CAST(racuni.broj_racuna AS INT) ASC";

            DataTable DT = classSQL.select(sql, "racuni").Tables[0];
            decimal ukupno;
            DateTime dt;

            string csvDoc = "Broj računa;Tvrtka;Ime i Prezime;OIB partnera;Datum;Iznos;Porez na dohodak;Prirez\n";
            foreach (DataRow r in DT.Rows)
            {
                decimal.TryParse(r["ukupno"].ToString(), out ukupno);
                DateTime.TryParse(r["datum_racuna"].ToString(), out dt);

                csvDoc += r["broj_racuna"].ToString() + ";" +
                    r["ime_tvrtke"].ToString() + ";" +
                    r["ime"].ToString() + " " + r["prezime"].ToString() + ";" +
                    r["oib"].ToString() + ";" +
                    dt.ToString("yyyy-MM-dd H:mm:ss") + ";" +
                    Math.Round(ukupno, 3).ToString("#0.00") + ";" +
                    r["porez_na_dohodakSUM"].ToString() + ";" +
                    r["prirez_iznosSUM"].ToString() + "\n";
            }

            csvDoc += "UKUPNO;;;;;=SUM(F2:F" + (DT.Rows.Count + 1).ToString() + ");=SUM(G2:G" + (DT.Rows.Count + 1).ToString() + ");=SUM(H2:H" + (DT.Rows.Count + 1).ToString() + ");\n";

            File.WriteAllText(folderBrowserDialog1.SelectedPath + "/Računi.csv", csvDoc, Encoding.GetEncoding(1250));
        }

        #endregion btnSpremiExcel EXCEL

        #region SpremiSveExcelGrupirano EXCEL

        private void SpremiSveExcelGrupirano()
        {
            string sql = "SELECT partners.id_partner,partners.ime,partners.prezime,partners.ime_tvrtke FROM racuni" +
                            " LEFT JOIN partners ON racuni.id_kupac=partners.id_partner" +
                            " WHERE racuni.ukupno>'0' " + PripremaFilteraZaSQL() + "" +
                            " GROUP BY partners.id_partner,partners.ime,partners.prezime,partners.ime_tvrtke";

            DataTable DTpartnersInRac = classSQL.select(sql, "racuni").Tables[0];

            foreach (DataRow RowPartner in DTpartnersInRac.Rows)
            {
                int id_partner;
                int.TryParse(RowPartner["id_partner"].ToString(), out id_partner);

                sql = "SELECT racuni.broj_racuna,ukupno,racuni.datum_racuna,partners.ime_tvrtke,partners.ime,partners.prezime,partners.oib FROM racuni" +
                                " LEFT JOIN partners ON racuni.id_kupac=partners.id_partner" +
                                " WHERE racuni.ukupno>'0' " + PripremaFilteraZaSQL() + " AND racuni.id_kupac='" + id_partner.ToString() + "'" +
                                " ORDER BY partners.ime, partners.prezime,ime_tvrtke";

                DataTable DT = classSQL.select(sql, "racuni").Tables[0];

                decimal ukupno;
                DateTime dt;

                string csvDoc = "Broj računa;Ime i Prezime;OIB partnera;Datum;Iznos\n";
                foreach (DataRow r in DT.Rows)
                {
                    decimal.TryParse(r["ukupno"].ToString(), out ukupno);
                    DateTime.TryParse(r["datum_racuna"].ToString(), out dt);

                    if (RowPartner["ime"].ToString() == "" && RowPartner["prezime"].ToString() == "")
                        RowPartner["ime"] = r["ime_tvrtke"].ToString();

                    csvDoc += r["broj_racuna"].ToString() + ";" +
                        r["ime"].ToString() + " " + r["prezime"].ToString() + ";" +
                        r["oib"].ToString() + ";" +
                        dt.ToString("yyyy-MM-dd H:mm:ss") + ";" +
                        Math.Round(ukupno, 3).ToString("#0.00") + ";\n";
                }

                csvDoc += "UKUPNO;;;;=SUM(E2:E" + (DT.Rows.Count + 1).ToString() + ");\n";

                if (RowPartner["ime"].ToString() == "" && RowPartner["prezime"].ToString() == "")
                    RowPartner["ime"] = RowPartner["ime_tvrtke"];

                if (RowPartner["ime"].ToString() == "")
                    RowPartner["ime"] = "_";

                RowPartner["ime"] = RowPartner["ime"].ToString().Replace("'", "").Replace("\"", "");
                RowPartner["prezime"] = RowPartner["prezime"].ToString().Replace("'", "").Replace("\"", "");

                File.WriteAllText(folderBrowserDialog1.SelectedPath + "/" + RowPartner["ime"].ToString() + " " + RowPartner["prezime"].ToString() + ".csv", csvDoc, Encoding.GetEncoding(1250));
            }
        }

        #endregion SpremiSveExcelGrupirano EXCEL

        #region PripremaFilteraZaSQL EXCEL

        private string PripremaFilteraZaSQL()
        {
            string Broj = "";
            string Partner = "";
            string DateStart = "";
            string DateEnd = "";
            string VD = "";
            string Valuta = "";
            string Izradio = "";
            string SifraArtikla = "";

            if (chbBroj.Checked)
            {
                Broj = " AND racuni.broj_racuna='" + txtBroj.Text + "'";
            }

            if (chbSifra.Checked)
            {
                Partner = " AND racuni.id_kupac='" + txtPartner.Text + "'";
            }

            if (chbOD.Checked)
            {
                DateStart = " AND racuni.datum_racuna >='" + dtpOD.Value.ToString("yyyy-MM-dd H:mm:ss") + "'";
            }
            if (chbDO.Checked)
            {
                DateEnd = " AND racuni.datum_racuna <='" + dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss") + "'";
            }

            if (chbArtikl.Checked)
            {
                SifraArtikla = " AND racun_stavke.sifra_robe ='" + cbArtikl.Text + "'";
            }

            if (chbIzradio.Checked)
            {
                Izradio = " AND racuni.id_blagajnik='" + cbIzradio.SelectedValue + "'";
            }

            if (chbPoslovnica.Checked)
            {
                Izradio = " AND racuni.id_ducan='" + cbPoslovnica.SelectedValue + "'";
            }

            if (chbNaplatni.Checked)
            {
                Izradio = " AND racuni.id_kasa='" + cbNaplatni.SelectedValue + "'";
            }

            string filter = Broj + Partner + VD + DateStart + DateEnd + Valuta + SifraArtikla + Izradio;

            return filter;
        }

        #endregion PripremaFilteraZaSQL EXCEL

        private void btnDodajPartneraNaRacun_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.Rows.Count == 0 || dgv.CurrentRow == null)
                    return;

                string mesageBoxQuestion = "Želite dodati partnera na odabrani račun?";
                if (dgv.CurrentRow.Cells["Partner"].Value.ToString().Length > 0)
                {
                    mesageBoxQuestion = "Želite promjeniti partnera na odabranom računu?";
                }

                if (MessageBox.Show(mesageBoxQuestion, "Račun", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    frmPartnerTrazi partner = new frmPartnerTrazi();
                    partner.ShowDialog();

                    string sql = string.Format(@"update racuni
set
    id_kupac = {0}
where broj_racuna = '{1}' and id_ducan = {2} and id_kasa = {3};",
    (Properties.Settings.Default.id_partner.Length > 0 ? Properties.Settings.Default.id_partner : "0"),
    dgv.CurrentRow.Cells["Broj računa"].FormattedValue.ToString(),
    dgv.CurrentRow.Cells["id_ducan"].FormattedValue.ToString(),
    dgv.CurrentRow.Cells["id_kasa"].FormattedValue.ToString());
                    classSQL.update(sql);

                    string partnerName = "";
                    if (Properties.Settings.Default.id_partner.Length > 0)
                    {
                        sql = string.Format("select case when vrsta_korisnika = 1 then ime_tvrtke else concat(ime, ' ', prezime) end as naziv from partners where id_partner = {0};", Properties.Settings.Default.id_partner);
                        DataSet ds = classSQL.select(sql, "partners");
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                        {
                            partnerName = ds.Tables[0].Rows[0]["naziv"].ToString();
                        }
                    }
                    dgv.CurrentRow.Cells["Partner"].Value = partnerName;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}