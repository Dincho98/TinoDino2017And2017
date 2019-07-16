using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Kasa
{
    public partial class frmIspisOdredenogRacuna : Form
    {
        private bool fikaliziraj = false;

        public frmIspisOdredenogRacuna()
        {
            InitializeComponent();
        }

        public string id_kasa { get; set; }
        public string id_ducan { get; set; }

        private void frmIspisOdredenogRacuna_Load(object sender, EventArgs e)
        {
            textBox1.Select();
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                button2.PerformClick();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private DataTable DTsend;
        private string sifra = "";
        private string barcode = "";

        private void button1_Click(object sender, EventArgs e)
        {
            DTsend = new DataTable();
            DTsend.Columns.Add("broj_racuna");
            DTsend.Columns.Add("sifra_robe");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("mpc");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("cijena");
            DTsend.Columns.Add("ime");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("porez_potrosnja");
            DTsend.Columns.Add("povratna_naknada");

            DTsend.Columns.Add("porez_na_dohodak");
            DTsend.Columns.Add("prirez");
            DTsend.Columns.Add("porez_na_dohodak_iznos");
            DTsend.Columns.Add("prirez_iznos");
            DataRow row;

            string sql = "SELECT racun_stavke.vpc,racun_stavke.porez_potrosnja,racun_stavke.sifra_robe,roba.naziv," +
                "racun_stavke.porez_na_dohodak,racun_stavke.prirez,racun_stavke.porez_na_dohodak_iznos,racun_stavke.prirez_iznos," +
                    "racun_stavke.id_skladiste,racun_stavke.mpc,racun_stavke.porez,racun_stavke.kolicina,racun_stavke.rabat," +
                    "racun_stavke.povratna_naknada,roba.oduzmi,roba.mpc AS cijena FROM racun_stavke " +
                    " INNER JOIN roba ON roba.sifra=racun_stavke.sifra_robe" +
                    " WHERE racun_stavke.broj_racuna='" + textBox1.Text + "' AND  id_kasa=" + id_kasa + " AND id_ducan=" + id_ducan;

            DataTable DT = classSQL.select("SELECT * FROM racuni WHERE broj_racuna='" + textBox1.Text + "' AND  id_kasa=" + id_kasa + " AND id_ducan=" + id_ducan, "racuni").Tables[0];

            if (DT.Rows.Count == 0)
            {
                MessageBox.Show("Krivi unos.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable DTitems = classSQL.select(sql, "racun_stavke").Tables[0];
            DataTable DTpostavkePrinter = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
            string imeBlagajnika = classSQL.select("SELECT ime + ' ' + prezime AS name FROM zaposlenici WHERE id_zaposlenik='" + DT.Rows[0]["id_blagajnik"].ToString() + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();

            for (int i = 0; i < DTitems.Rows.Count; i++)
            {
                sifra = DTitems.Rows[i]["sifra_robe"].ToString();
                row = DTsend.NewRow();
                row["broj_racuna"] = textBox1.Text;
                row["sifra_robe"] = DTitems.Rows[i]["sifra_robe"].ToString();
                row["id_skladiste"] = DTitems.Rows[i]["id_skladiste"].ToString();
                row["mpc"] = DTitems.Rows[i]["mpc"].ToString();
                row["porez"] = DTitems.Rows[i]["porez"].ToString();
                row["kolicina"] = DTitems.Rows[i]["kolicina"].ToString();
                row["rabat"] = DTitems.Rows[i]["rabat"].ToString();
                row["cijena"] = DTitems.Rows[i]["mpc"].ToString();
                row["ime"] = DTitems.Rows[i]["naziv"].ToString();
                row["vpc"] = DTitems.Rows[i]["vpc"].ToString();
                row["porez_potrosnja"] = DTitems.Rows[i]["porez_potrosnja"].ToString();
                row["povratna_naknada"] = DTitems.Rows[i]["povratna_naknada"].ToString();

                row["porez_na_dohodak"] = DTitems.Rows[i]["porez_na_dohodak"].ToString();
                row["prirez"] = DTitems.Rows[i]["prirez"].ToString();
                row["porez_na_dohodak_iznos"] = DTitems.Rows[i]["porez_na_dohodak_iznos"].ToString();
                row["prirez_iznos"] = DTitems.Rows[i]["prirez_iznos"].ToString();
                DTsend.Rows.Add(row);

                if (sifra.Length > 4)
                {
                    if (sifra.Substring(0, 3) == "000")
                    {
                        string sqlnext = "UPDATE racun_popust_kod_sljedece_kupnje SET koristeno='DA' WHERE broj_racuna='" + sifra.Substring(3, sifra.Length - 3) + "'";
                        classSQL.update(sqlnext);
                    }
                }
            }

            barcode = "000" + textBox1.Text;

            string brRac = textBox1.Text;

            string mali = DTpostavkePrinter.Rows[0]["posPrinterBool"].ToString();
            string a5 = classSQL.select_settings("SELECT a5print FROM postavke", "postavke").Tables[0].Rows[0]["a5print"].ToString();
            string a6 = classSQL.select_settings("SELECT a6print FROM postavke", "postavke").Tables[0].Rows[0]["a6print"].ToString();

            try
            {
                DataTable DTRacun = classSQL.select("SELECT datum_racuna, nacin_placanja FROM racuni where broj_racuna='" + textBox1.Text + "' AND id_kasa=" + id_kasa + " AND id_ducan=" + id_ducan, "pos_print").Tables[0];
                DateTime[] datumi = new DateTime[2];
                datumi[0] = Convert.ToDateTime(DTRacun.Rows[0][0].ToString());
                datumi[1] = datumi[0];
                barcode += id_ducan + id_kasa + datumi[0].Year.ToString().Remove(0, 2);
                PosPrint.classPosPrintMaloprodaja2.PrintReceipt(DTsend, imeBlagajnika,
                    textBox1.Text + "/" + datumi[0].Year.ToString(), DT.Rows[0]["id_kupac"].ToString(),
                    barcode, brRac, DTRacun.Rows[0][1].ToString(), datumi, !fikaliziraj, mali, false, true, id_ducan, id_kasa, true);

                if (mali == "1")
                {
                    //već je isprintan u gornjoj metodi
                }
                else if (a5 == "1")
                {
                    printajA5(brRac);
                }
                else if (a6 == "1")
                {
                    printajA6(brRac);
                }
                else if (mali != "1")
                {
                    printaj(brRac);
                }
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("Desila se pogreška kod ispisa na 'mali' pos printer.\r\n" +
                    "Želite li ispisati ovaj dokument na A4 format?" + Environment.NewLine + Environment.NewLine
                    + ex.Message, "Printer") == DialogResult.Yes)
                {
                    printaj(brRac);
                }
            }
        }

        private void printaj(string brRac)
        {
            Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
            rfak.dokumenat = "RAC";
            rfak.ImeForme = "Račun";
            rfak.poslovnica = id_ducan;
            rfak.naplatni = id_kasa;
            rfak.broj_dokumenta = brRac;
            rfak.ShowDialog();
        }

        private void printajA5(string brRac)
        {
            Report.A5racun.frmA5racun rfak = new Report.A5racun.frmA5racun();
            rfak.dokumenat = "RAC";
            rfak.ImeForme = "Račun";
            rfak.poslovnica = id_ducan;
            rfak.naplatni = id_kasa;
            rfak.broj_dokumenta = brRac;
            rfak.ShowDialog();
        }

        private void printajA6(string brRac)
        {
            Report.A6racun.frmA6racun rfak = new Report.A6racun.frmA6racun();
            rfak.dokumenat = "RAC";
            rfak.ImeForme = "Račun";
            rfak.poslovnica = id_ducan;
            rfak.naplatni = id_kasa;
            rfak.broj_dokumenta = brRac;
            rfak.ShowDialog();
        }

        private void btnFiskaliziraj_Click(object sender, EventArgs e)
        {
            try
            {
                Util.frmInputPassword f = new Util.frmInputPassword();
                f.ShowDialog();

                if (f.password == "Fiskalq1w2e3r4")
                {
                    fikaliziraj = true;
                    button1.PerformClick();
                }
                fikaliziraj = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                fikaliziraj = false;
            }
        }
    }
}