using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmAktivacijaDokumenata : Form
    {
        private string[] kasicas = { "chbFakture", "chbFakBezRobe", "chbPonude", "chbPrometPoRobi" };
        private string[] trgovina = { "chbKalkulacije", "chbInventure", "chbKarticaRobe", "chbFakture", "chbFakBezRobe", "chbPonude", "chbRN", "chbOdjavaKomisione", "chbZapisnikOpromjeniCijene", "chbPovratnicaDob", "chbOtpisRobe", "chbNaljepnice", "chbUlazneFak", "chbOtpremnica", "chbMeduskl", "chbPrimke", "chbIzdatnice", "chbPromocije", "chbPocetnoStanje", "chbPrometPoRobi" };

        public frmAktivacijaDokumenata()
        {
            InitializeComponent();
        }

        private void frmAktivacijaDokumenata_Load(object sender, EventArgs e)
        {
            groupBox1.Hide();
            SETvalue();
            rbTrgovina_CheckedChanged(sender, e);
        }

        private void SETvalue()
        {
            DataTable DT = classSQL.select_settings("SELECT * FROM aktivnost_podataka", "aktivnost_podataka").Tables[0];

            if (Convert.ToBoolean(DT.Rows[0]["isKasica"].ToString()))
            {
                rbKasica.Checked = true;
                rbTrgovina.Checked = false;
            }
            else
            {
                rbKasica.Checked = false;
                rbTrgovina.Checked = true;
            }

            chbKartoteka.Checked = Class.Dokumenti.kartoteka;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "yaqxswcde")
            {
                groupBox1.Show();
            }
            else
            {
                groupBox1.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kartoteka = "0";
            if (chbKartoteka.Checked)
            {
                kartoteka = "1";
            }

            string kalkulacija = "0";
            if (chbKalkulacije.Checked)
            {
                kalkulacija = "1";
            }

            string inventura = "0";
            if (chbInventure.Checked)
            {
                inventura = "1";
            }

            string karticaRobe = "0";
            if (chbKarticaRobe.Checked)
            {
                karticaRobe = "1";
            }

            string fakture = "0";
            if (chbFakture.Checked)
            {
                fakture = "1";
            }

            string faktureBR = "0";
            if (chbFakBezRobe.Checked)
            {
                faktureBR = "1";
            }

            string ponude = "0";
            if (chbPonude.Checked)
            {
                ponude = "1";
            }

            string radni_nalozi = "0";
            if (chbRN.Checked)
            {
                radni_nalozi = "1";
            }

            string odjavaKomisione = "0";
            if (chbOdjavaKomisione.Checked)
            {
                odjavaKomisione = "1";
            }

            string povratnicaDobavljacu = "0";
            if (chbPovratnicaDob.Checked)
            {
                povratnicaDobavljacu = "1";
            }

            string otpisRobe = chbOtpisRobe.Checked ? "1" : "0";

            string naljepnice = "0";
            if (chbNaljepnice.Checked)
            {
                naljepnice = "1";
            }

            string ulazneFakture = "0";
            if (chbUlazneFak.Checked)
            {
                ulazneFakture = "1";
            }

            string otpremnica = "0";
            if (chbOtpremnica.Checked)
            {
                otpremnica = "1";
            }

            string meduskladisnica = "0";
            if (chbMeduskl.Checked)
            {
                meduskladisnica = "1";
            }

            string primke = "0";
            if (chbPrimke.Checked)
            {
                primke = "1";
            }

            string izdatnice = "0";
            if (chbIzdatnice.Checked)
            {
                izdatnice = "1";
            }

            string promocije = "0";
            if (chbPromocije.Checked)
            {
                promocije = "1";
            }

            string pocetno_stanje = "0";
            if (chbPocetnoStanje.Checked)
            {
                pocetno_stanje = "1";
            }

            string prometPoRobi = "0";
            if (chbPrometPoRobi.Checked)
            {
                prometPoRobi = "1";
            }

            string robnoBOOL = "0";
            if (kalkulacija == "1" || fakture == "1" || primke == "1")
            {
                robnoBOOL = "1";
            }

            string sql = @"UPDATE aktivnost_podataka SET
kartoteka='" + kartoteka + @"',
kalkulacije='" + kalkulacija + @"',
inventura='" + inventura + @"',
kartica='" + karticaRobe + @"',
faktura='" + fakture + @"',
faktura_bez_robe='" + faktureBR + @"',
ponude='" + ponude + @"',
radni_nalog='" + radni_nalozi + @"',
odjava_robe='" + odjavaKomisione + @"',
povrat_dobavljacu='" + povratnicaDobavljacu + @"',
naljepnice='" + naljepnice + @"',
ulazne_fakture='" + ulazneFakture + @"',
otpremnica='" + otpremnica + @"',
meduskladisnice='" + meduskladisnica + @"',
primke='" + primke + @"',
izdatnice='" + izdatnice + @"',
promocije='" + promocije + @"',
pocetno_stanje='" + pocetno_stanje + @"',
promet_po_robi='" + prometPoRobi + @"',
otpis_robe='" + otpisRobe + @"',
boolRobno='" + robnoBOOL + @"',
isKasica=" + (rbKasica.Checked ? 1 : 0) + ";";
            classSQL.Setings_Update(sql);
        }

        private void rbTrgovina_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (Control c in this.groupBox1.Controls)
                {
                    if (c is CheckBox)
                    {
                        if (((CheckBox)c).Name != "chbKartoteka")
                        {
                            if (rbTrgovina.Checked)
                            {
                                if (trgovina.Contains(((CheckBox)c).Name))
                                    ((CheckBox)c).Checked = true;
                                else
                                    ((CheckBox)c).Checked = false;
                            }
                            else
                            {
                                if (kasicas.Contains(((CheckBox)c).Name))
                                    ((CheckBox)c).Checked = true;
                                else
                                    ((CheckBox)c).Checked = false;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void frmAktivacijaDokumenata_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Class.Dokumenti.GetDokumenti();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}