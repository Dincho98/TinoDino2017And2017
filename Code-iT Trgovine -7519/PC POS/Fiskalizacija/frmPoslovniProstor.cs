using Raverus.FiskalizacijaDEV;
using Raverus.FiskalizacijaDEV.PopratneFunkcije;
using Raverus.FiskalizacijaDEV.Schema;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Xml;

namespace PCPOS.Fiskalizacija
{
    public partial class frmPoslovniProstor : Form
    {
        public frmPoslovniProstor()
        {
            InitializeComponent();
        }

        private void frmPoslovniProstor_Load(object sender, EventArgs e)
        {
            Fillpos_prostor();
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private static DataTable DTfis = classSQL.select_settings("SELECT * FROM fiskalizacija", "fiskalizacija").Tables[0];
        //private static DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private void Fillpos_prostor()
        {
            //DataTable DT = classSQL.select("SELECT * FROM ducan WHERE id_ducan='" + Class.Postavke.id_default_ducan + "'", "ducan").Tables[0];

            txtOznakaPP.Text = Class.Postavke.default_poslovnica;

            //DataTable DTp = classSQL.select("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];
            txtOIB.Text = Class.PodaciTvrtka.oibTvrtke;

            DataTable DTf = classSQL.select_settings("SELECT * FROM podaci_poslovnica_fiskal", "podaci_poslovnica_fiskal").Tables[0];

            if (DTf.Rows.Count > 0)
            {
                txtOIB.Text = Class.PodaciTvrtka.oibTvrtke;
                //txtOznakaPP.Text = DTf.Rows[0]["oznakaPP"].ToString();
                txtUlica.Text = DTf.Rows[0]["ulica"].ToString();
                txtKucniBroj.Text = DTf.Rows[0]["broj"].ToString();
                txtKucniDodatak.Text = DTf.Rows[0]["broj_dodatak"].ToString();
                txtBrojPoste.Text = DTf.Rows[0]["posta"].ToString();
                txtNaselje.Text = DTf.Rows[0]["naselje"].ToString();
                txtOpcina.Text = DTf.Rows[0]["opcina"].ToString();
                try
                {
                    dtpDate.Value = Convert.ToDateTime(DTf.Rows[0]["datum"].ToString());
                }
                catch (Exception)
                {
                }
                txtRadnoVrijeme.Text = DTf.Rows[0]["r_vrijeme"].ToString();

                if (DTf.Rows[0]["zatvaranje"].ToString() == "1")
                {
                    chbZatvaranje.Checked = true;
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = Color.FromArgb(((byte)(105)), ((byte)(170)), ((byte)(197)));
            Color y = Color.FromArgb(((byte)(40)), ((byte)(109)), ((byte)(135)));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void btnPosaljiPodatke_Click(object sender, EventArgs e)
        {
            PosaljiPodatke();
        }

        private void PosaljiPodatke()
        {
            if (txtOIB.Text == "") { MessageBox.Show("Krivo upisani oib."); return; }
            if (txtOznakaPP.Text == "") { MessageBox.Show("Krivo upisana oznaka PP."); return; }
            if (txtUlica.Text == "") { MessageBox.Show("Krivo upisana ulica."); return; }
            if (txtKucniBroj.Text == "") { MessageBox.Show("Krivo upisani kućni broj."); return; }
            if (txtBrojPoste.Text == "") { MessageBox.Show("Krivo upisani broj pošte."); return; }
            if (txtNaselje.Text == "") { MessageBox.Show("Krivo upisani oib."); return; }
            if (txtOpcina.Text == "") { MessageBox.Show("Krivo upisana općina."); return; }
            if (txtRadnoVrijeme.Text == "") { MessageBox.Show("Krivo upisano radno vrijeme."); return; }

            if (txtOznakaPP.Text.Length > 19)
            {
                MessageBox.Show("Previše znamenaka u poslovnom prostoru.");
            }

            ZaglavljeType zaglavlje = new ZaglavljeType()
            {
                DatumVrijeme = Razno.DohvatiFormatiranoTrenutnoDatumVrijeme(),
                IdPoruke = Guid.NewGuid().ToString()
            };

            //X509Certificate2 certifikat = Raverus.FiskalizacijaDEV.PopratneFunkcije.Potpisivanje.DohvatiCertifikat(DTfis.Rows[0]["naziv_certifikata"].ToString());

            X509Certificate2 certifikat;

            if (File.Exists(Class.Postavke.putanja_certifikat))
            {
                certifikat = Potpisivanje.DohvatiCertifikat(Class.Postavke.putanja_certifikat, Class.Postavke.certifikat_zaporka);
            }
            else
            {
                certifikat = Potpisivanje.DohvatiCertifikat(DTfis.Rows[0]["naziv_certifikata"].ToString());
            }

            try
            {
                CentralniInformacijskiSustav cis = new CentralniInformacijskiSustav();
                if (Class.Postavke.TEST_FISKALIZACIJA)
                {
                    cis.CisUrl = "https://cistest.apis-it.hr:8449/FiskalizacijaServiceTest";
                }
                else
                {
                    cis.CisUrl = "https://cis.porezna-uprava.hr:8449/FiskalizacijaService";
                }
                cis.TimeOut = 10000;

                PoslovniProstorType poslovniProstor = new PoslovniProstorType();
                poslovniProstor.Oib = txtOIB.Text;
                poslovniProstor.OznPoslProstora = txtOznakaPP.Text;

                AdresaType adresa = new AdresaType();
                adresa.Ulica = txtUlica.Text;
                adresa.KucniBroj = txtKucniBroj.Text;
                if (txtKucniDodatak.Text != "")
                {
                    adresa.KucniBrojDodatak = txtKucniDodatak.Text;
                }
                adresa.BrojPoste = txtBrojPoste.Text;
                adresa.Naselje = txtNaselje.Text;
                adresa.Opcina = txtOpcina.Text;
                AdresniPodatakType adresniPodatak = new AdresniPodatakType();
                adresniPodatak.Item = adresa;
                poslovniProstor.AdresniPodatak = adresniPodatak;

                if (chbZatvaranje.Checked)
                {
                    poslovniProstor.OznakaZatvaranjaSpecified = true;
                    poslovniProstor.OznakaZatvaranja = OznakaZatvaranjaType.Z;
                }

                poslovniProstor.RadnoVrijeme = txtRadnoVrijeme.Text;
                poslovniProstor.DatumPocetkaPrimjene = Razno.FormatirajDatum(dtpDate.Value);
                poslovniProstor.SpecNamj = Class.Postavke.OIB_PC1;

                XmlDocument doc = cis.PosaljiPoslovniProstor(poslovniProstor, certifikat);

                //MessageBox.Show(doc.InnerXml.Replace("\'", ""));

                if (cis.OdgovorGreska != null)
                {
                    MessageBox.Show("Greška kod slanja zahtjeva");
                }
                else
                {
                    string zatvaranje = "0";
                    if (chbZatvaranje.Checked)
                    {
                        zatvaranje = "1";
                    }

                    string sql = "UPDATE podaci_poslovnica_fiskal SET " +
                        " OIB='" + txtOIB.Text + "'," +
                        " oznakaPP='" + txtOznakaPP.Text + "'," +
                        " ulica='" + txtUlica.Text + "'," +
                        " broj='" + txtKucniBroj.Text + "'," +
                        " broj_dodatak='" + txtKucniDodatak.Text + "'," +
                        " posta='" + txtBrojPoste.Text + "'," +
                        " naselje='" + txtNaselje.Text + "'," +
                        " opcina='" + txtOpcina.Text + "'," +
                        " datum='" + dtpDate.Value.ToString() + "'," +
                        " r_vrijeme='" + txtRadnoVrijeme.Text + "'," +
                        " zatvaranje='" + zatvaranje + "'" +
                        "";
                    classSQL.Setings_Update(sql);

                    MessageBox.Show("Zahtjev uspješno poslan.");
                }
            }
            catch (Exception ex)
            {
                string sql = "UPDATE podaci_poslovnica_fiskal SET " +
                    " OIB='" + txtOIB.Text + "'," +
                    " oznakaPP='" + txtOznakaPP.Text + "'," +
                    " ulica='" + txtUlica.Text + "'," +
                    " broj='" + txtKucniBroj.Text + "'," +
                    " broj_dodatak='" + txtKucniDodatak.Text + "'," +
                    " posta='" + txtBrojPoste.Text + "'," +
                    " naselje='" + txtNaselje.Text + "'," +
                    " opcina='" + txtOpcina.Text + "'," +
                    " datum='" + dtpDate.Value.ToString() + "'," +
                    " r_vrijeme='" + txtRadnoVrijeme.Text + "'," +
                    " zatvaranje='0'" +
                    "";
                classSQL.Setings_Update(sql);

                MessageBox.Show("Greška kod slanja zahtjeva.\r\n\r\n" + ex.ToString());
            }
        }
    }
}