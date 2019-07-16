using GenCode128;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PCPOS.Report.Naljepnice
{
    public partial class NaljepniceEan : Form
    {
        public NaljepniceEan()
        {
            InitializeComponent();
        }

        public bool vpcBool;
        public bool mpcBool;

        public int pocetak { get; set; }
        public DataTable DT { get; set; }

        private void frmNaljepnice_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists("Naljepnice"))
                {
                    string[] podaci = File.ReadAllLines("Naljepnice");
                    txtSifra.Text = podaci[0];
                    txtNaslov.Text = podaci[1];
                    txtRedak.Text = podaci[2];
                    txtRedak2.Text = podaci[3];
                    txtRedak3.Text = podaci[4];
                    txtBrojNaljepnica.Text = podaci[5];
                    txtZapocniOdBroja.Text = podaci[6];
                }
            }
            catch (Exception ex)
            {
            }

            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.RefreshReport();
        }

        private void btnKreiraj_Click(object sender, EventArgs e)
        {
            try
            {
                int brojZadanihNaljepnica;
                int.TryParse(txtBrojNaljepnica.Text, out brojZadanihNaljepnica);

                DataTable DTlista = listaUniverzalna.DTListaUniverzalna;

                Image img_barcode = Code128Rendering.MakeBarcodeImage(txtSifra.Text, int.Parse("3"), true);
                if (!Directory.Exists("EanPictures"))
                    Directory.CreateDirectory("EanPictures");

                img_barcode.Save("EanPictures/" + txtSifra.Text + ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                string myExeDir = "file://" + AppDomain.CurrentDomain.BaseDirectory + "EanPictures\\" + txtSifra.Text + ".jpeg";

                int zapocniOdBroja = 0;
                int.TryParse(txtZapocniOdBroja.Text, out zapocniOdBroja);

                int brojGeneriranih = 0;

                DTlista.Clear();

                for (int i = 0; i < (brojZadanihNaljepnica + zapocniOdBroja); i = i + 4)
                {
                    bool _1 = false, _2 = false, _3 = false, _4 = false;

                    if ((i + 1) >= zapocniOdBroja && brojGeneriranih < brojZadanihNaljepnica)
                    {
                        _1 = true;
                        brojGeneriranih++;
                    }
                    if ((i + 2) >= zapocniOdBroja && brojGeneriranih < brojZadanihNaljepnica)
                    {
                        _2 = true;
                        brojGeneriranih++;
                    }
                    if ((i + 3) >= zapocniOdBroja && brojGeneriranih < brojZadanihNaljepnica)
                    {
                        _3 = true;
                        brojGeneriranih++;
                    }
                    if ((i + 4) >= zapocniOdBroja && brojGeneriranih < brojZadanihNaljepnica)
                    {
                        _4 = true;
                        brojGeneriranih++;
                    }

                    DataRow r = DTlista.NewRow();
                    r["string1"] = _1 ? txtSifra.Text : "";
                    r["string2"] = _1 ? txtNaslov.Text : "";
                    //r["string2"] = _1 ? txtSifra.Text : "";
                    r["string3"] = _1 ? txtRedak.Text : "";
                    r["string4"] = _1 ? txtRedak2.Text : "";
                    r["string5"] = _1 ? txtRedak3.Text : "";
                    r["slika1"] = _1 ? myExeDir : "";

                    r["string6"] = _2 ? txtSifra.Text : "";
                    r["string7"] = _2 ? txtNaslov.Text : "";
                    //r["string7"] = _2 ? txtSifra.Text : "";
                    r["string8"] = _2 ? txtRedak.Text : "";
                    r["string9"] = _2 ? txtRedak2.Text : "";
                    r["string10"] = _2 ? txtRedak3.Text : "";
                    r["slika2"] = _2 ? myExeDir : "";

                    r["string11"] = _3 ? txtSifra.Text : "";
                    r["string12"] = _3 ? txtNaslov.Text : "";
                    //r["string12"] = _3 ? txtSifra.Text : "";
                    r["string13"] = _3 ? txtRedak.Text : "";
                    r["string14"] = _3 ? txtRedak2.Text : "";
                    r["string15"] = _3 ? txtRedak3.Text : "";
                    r["slika3"] = _3 ? myExeDir : "";

                    r["string16"] = _4 ? txtSifra.Text : "";
                    r["string17"] = _4 ? txtNaslov.Text : "";
                    //r["string17"] = _4 ? txtSifra.Text : "";
                    r["string18"] = _4 ? txtRedak.Text : "";
                    r["string19"] = _4 ? txtRedak2.Text : "";
                    r["string20"] = _4 ? txtRedak3.Text : "";
                    r["slika4"] = _4 ? myExeDir : "";
                    DTlista.Rows.Add(r);
                }

                this.reportViewer1.RefreshReport();

                File.WriteAllText("Naljepnice",
                    txtSifra.Text + "\r\n" +
                    txtNaslov.Text + "\r\n" +
                    txtRedak.Text + "\r\n" +
                    txtRedak2.Text + "\r\n" +
                    txtRedak3.Text + "\r\n" +
                    txtBrojNaljepnica.Text + "\r\n" +
                    txtZapocniOdBroja.Text +
                    "");
            }
            catch (Exception ex)
            {
            }
        }

        private void btnPartner_Click(object sender, EventArgs e)
        {
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            roba_trazi.ShowDialog();
            string propertis_sifra = Properties.Settings.Default.id_roba.Trim();
            if (propertis_sifra != "")
            {
                string sql = "SELECT * FROM roba WHERE sifra='" + propertis_sifra + "'";

                DataTable DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra.Text = DTRoba.Rows[0]["sifra"].ToString();
                    txtNaslov.Text = DTRoba.Rows[0]["naziv"].ToString();
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}