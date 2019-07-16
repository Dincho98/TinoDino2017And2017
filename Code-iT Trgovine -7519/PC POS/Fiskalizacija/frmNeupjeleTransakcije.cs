using Raverus.FiskalizacijaDEV;
using Raverus.FiskalizacijaDEV.PopratneFunkcije;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Xml;

namespace PCPOS.Fiskalizacija
{
    public partial class frmNeupjeleTransakcije : Form
    {
        public frmNeupjeleTransakcije()
        {
            InitializeComponent();
        }

        private static DataTable DTfis = classSQL.select_settings("SELECT * FROM fiskalizacija", "fiskalizacija").Tables[0];
        //private static DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private void frmNeupjeleTransakcije_Load(object sender, EventArgs e)
        {
            Set();
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((byte)(105)), ((byte)(170)), ((byte)(197)));
            Color y = System.Drawing.Color.FromArgb(((byte)(40)), ((byte)(109)), ((byte)(135)));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
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
            row.Height = 25;
        }

        private void Set()
        {
            //DataTable DTpostavke = classSQL.select_settings("SELECT default_ducan FROM postavke", "postavke").Tables[0];

            classSQL.update("UPDATE neuspjela_fiskalizacija SET id_ducan = '" + Class.Postavke.id_default_ducan + "'");

            string sql = "SELECT neuspjela_fiskalizacija.broj_racuna AS [Broj računa]," +
                " neuspjela_fiskalizacija.xml AS [XML]," +
                " neuspjela_fiskalizacija.greska AS [XML povrat], " +
                " ducan.ime_ducana AS [Ime dućana]," +
                " blagajna.ime_blagajne  AS [Ime blagajne]," +
                " neuspjela_fiskalizacija.date AS [Datum]," +
                " neuspjela_fiskalizacija.vrsta AS [Vrsta]" +
                " FROM neuspjela_fiskalizacija " +
                " LEFT JOIN blagajna ON blagajna.id_blagajna=CAST(neuspjela_fiskalizacija.id_kasa AS INTEGER)" +
                " LEFT JOIN ducan ON ducan.id_ducan=CAST(neuspjela_fiskalizacija.id_ducan AS INTEGER) ORDER BY neuspjela_fiskalizacija.broj_racuna" +
                "";
            DataTable DT = classSQL.select(sql, "neuspjela_fiskalizacija").Tables[0];
            dgw.DataSource = DT;

            lblU.Text = "Ukupno nefiskaliziranih računa: " + DT.Rows.Count.ToString();

            //dgw.Columns["id"].Visible=false;
            PaintRows(dgw);
        }

        private void btnNarudzbe_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM neuspjela_fiskalizacija";
            DataTable DT = classSQL.select(sql, "neuspjela_fiskalizacija").Tables[0];

            Raverus.FiskalizacijaDEV.Schema.ZaglavljeType zaglavlje = new Raverus.FiskalizacijaDEV.Schema.ZaglavljeType()
            {
                DatumVrijeme = Razno.DohvatiFormatiranoTrenutnoDatumVrijeme(),
                IdPoruke = Guid.NewGuid().ToString()
            };

            //X509Certificate2 certifikat = Raverus.FiskalizacijaDEV.PopratneFunkcije.Potpisivanje.DohvatiCertifikat(DTfis.Rows[0]["naziv_certifikata"].ToString());

            X509Certificate2 certifikat;
            if (File.Exists(Class.Postavke.putanja_certifikat))
            {
                certifikat = Raverus.FiskalizacijaDEV.PopratneFunkcije.Potpisivanje.DohvatiCertifikat(Class.Postavke.putanja_certifikat, Class.Postavke.certifikat_zaporka);
            }
            else
            {
                certifikat = Raverus.FiskalizacijaDEV.PopratneFunkcije.Potpisivanje.DohvatiCertifikat(DTfis.Rows[0]["naziv_certifikata"].ToString());
            }

            string datum_vrijeme = DateTime.Now.ToString("dd.MM.yyyyThh:mm:ss");

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                XmlDocument dokument = new XmlDocument();
                dokument.LoadXml(DT.Rows[i]["xml"].ToString());
                XmlNamespaceManager ns = new XmlNamespaceManager(dokument.NameTable);
                ns.AddNamespace("tns", "http://www.apis-it.hr/fin/2012/types/f73");
                string d = dokument.SelectSingleNode("/tns:RacunZahtjev/tns:Racun/tns:NakDost", ns).ChildNodes[0].Value = "true";

                Raverus.FiskalizacijaDEV.CentralniInformacijskiSustav cis = new CentralniInformacijskiSustav();

                string testYes = File.Exists("FiskalTest") ? "1" : "0";
                if (Class.Postavke.TEST_FISKALIZACIJA)
                {
                    cis.CisUrl = "https://cistest.apis-it.hr:8449/FiskalizacijaServiceTest";
                }
                else
                {
                    cis.CisUrl = "https://cis.porezna-uprava.hr:8449/FiskalizacijaService";
                }

                Raverus.FiskalizacijaDEV.PopratneFunkcije.Potpisivanje.PotpisiXmlDokument(dokument, certifikat);
                Raverus.FiskalizacijaDEV.PopratneFunkcije.XmlDokumenti.DodajSoapEnvelope(ref dokument);

                try
                {
                    string vrsta = DT.Rows[i]["vrsta"].ToString().ToLower();
                    string godina = Convert.ToDateTime(DT.Rows[i]["date"].ToString()).Year.ToString();
                    string id = DT.Rows[i]["id"].ToString();
                    string broj = DT.Rows[i]["broj_racuna"].ToString();
                    string id_ducan = DT.Rows[i]["id_ducan"].ToString();
                    string id_kasa = DT.Rows[i]["id_kasa"].ToString();

                    XmlDocument odgovor = cis.PosaljiSoapPoruku(dokument);
                    string jir = Raverus.FiskalizacijaDEV.PopratneFunkcije.XmlDokumenti.DohvatiJir(odgovor);
                    provjera_sql(classSQL.delete("DELETE FROM neuspjela_fiskalizacija WHERE id='" + id + "'"));
                    switch (vrsta)
                    {
                        case "račun":
                            provjera_sql(classSQL.update("UPDATE racuni SET jir='" + jir + "' WHERE broj_racuna='" + broj + "' AND id_ducan=" + id_ducan + " AND id_kasa=" + id_kasa));
                            break;

                        case "avans":
                            provjera_sql(classSQL.update("UPDATE avansi SET jir='" + jir + "' WHERE broj_avansa='" + broj + "'"));
                            //" AND godina_avansa ='" + godina + "'"));
                            break;

                        case "faktura":
                            provjera_sql(classSQL.update("UPDATE fakture SET jir='" + jir + "' WHERE broj_fakture='" + broj + "'"));
                            break;

                        case "ifb":
                            provjera_sql(classSQL.update("UPDATE ifb SET jir='" + jir + "' WHERE broj='" + broj + "'"));
                            break;

                        default:
                            provjera_sql(classSQL.update("UPDATE racuni SET jir='" + jir + "' WHERE broj_racuna='" + broj + "' AND id_ducan=" + id_ducan + " AND id_kasa=" + id_kasa));
                            break;
                    }
                }
                catch (Exception ex)
                {
                    string greska;
                    if (cis.OdgovorGreska != null)
                    {
                        greska = cis.OdgovorGreska.InnerXml.Replace("\'", "");
                        int x, y;
                        x = greska.IndexOf("<tns:PorukaGreske>") + 18;
                        y = greska.IndexOf("</tns:PorukaGreske>");
                        string porukaUseru = new string(greska.ToArray(), x, y - x);

                        MessageBox.Show("Greška kod fiskalizacije\r\n\r\n" + porukaUseru.Replace("\"", "") + "\r\n\r\nPokušajte naknadno fiskalizirati", "Greška od strane FINE");
                    }
                    else
                    {
                        greska = ex.ToString().Replace("\'", "");
                        MessageBox.Show("Greška kod fiskalizacije\r\n\r\n\r\nNije ostvarena veza sa poreznom upravom\r\n\r\nPokušajte naknadno fiskalizirati");
                    }
                }
            }

            Set();
        }

        private static void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void btnIzvozDisc_Click(object sender, EventArgs e)
        {
            if (dgw.RowCount > 0)
            {
                int i = dgw.CurrentRow.Index;
                string xml = dgw.CurrentRow.Cells["XML"].FormattedValue.ToString();

                XmlDocument XML = new XmlDocument();

                XML.LoadXml(xml);

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.FileName = "dd.xml";
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    XML.Save(saveFileDialog1.OpenFile());
                }
            }
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dgw.RowCount > 0)
            {
                provjera_sql(classSQL.delete("DELETE FROM neuspjela_fiskalizacija WHERE broj_racuna='" + dgw.CurrentRow.Cells[0].FormattedValue.ToString() + "'"));
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                MessageBox.Show("Obrisano.");
            }
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.Rows.Count < 1)
                return;

            frmPregledNefiskaliziranog pn = new frmPregledNefiskaliziranog();
            pn.brojRacuna = dgw.CurrentRow.Cells[0].FormattedValue.ToString();
            pn.poslano = dgw.CurrentRow.Cells[1].FormattedValue.ToString();
            pn.greska = dgw.CurrentRow.Cells[2].FormattedValue.ToString();
            pn.ducan = dgw.CurrentRow.Cells[3].FormattedValue.ToString();
            pn.blagajna = dgw.CurrentRow.Cells[4].FormattedValue.ToString();
            pn.datum = dgw.CurrentRow.Cells[5].FormattedValue.ToString();
            pn.ShowDialog();

            dgw.CurrentRow.Cells[0].Value = pn.brojRacuna;
            dgw.CurrentRow.Cells[1].Value = pn.poslano;
            dgw.CurrentRow.Cells[2].Value = pn.greska;
            dgw.CurrentRow.Cells[3].Value = pn.ducan;
            dgw.CurrentRow.Cells[4].Value = pn.blagajna;
            dgw.CurrentRow.Cells[5].Value = pn.datum;
        }

        private void dgw_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}