using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmSviNormativi : Form
    {
        public frmSviNormativi()
        {
            InitializeComponent();
        }

        public frmNormativi MainForm { get; set; }
        public string sifra_normativa;
        private DataSet DSnormativ = new DataSet();
        public frmMenu MainFormMenu { get; set; }

        private void frmSviNormativi_Load(object sender, EventArgs e)
        {
            filldgv();
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 500);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void filldgv()
        {
            string dodatni_filter = "";

            //msi-r7-250-1gd5-oc
            dodatni_filter += txtBroj.Text != "" ? " AND normativi.broj_normativa='" + txtBroj.Text + "'" : "";
            dodatni_filter += cbArtikl.Text != "" ? " AND (normativi_stavke.sifra_robe ='" + cbArtikl.Text + "' OR normativi.sifra_artikla ='" + cbArtikl.Text + "')" : "";
            dodatni_filter += chbIzradio.Checked ? " AND normativi.id_zaposlenik='" + cbIzradio.SelectedValue + "'" : "";

            string sql = "SELECT normativi.broj_normativa AS [Broj normativa],roba.sifra AS [Šifra],roba.naziv AS [Ime artikla/usluge]," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS Izradio, normativi.godina_normativa as Godina  FROM normativi " +
                " JOIN roba ON roba.sifra=normativi.sifra_artikla" +
                " JOIN normativi_stavke ON normativi_stavke.broj_normativa=normativi.broj_normativa" +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=normativi.id_zaposlenik WHERE normativi.broj_normativa IS NOT NULL" + dodatni_filter +
                " GROUP BY [Broj normativa],[Ime artikla/usluge],Izradio,Godina,[Šifra] ORDER BY normativi.godina_normativa DESC,normativi.broj_normativa DESC LIMIT 2000";

            DSnormativ = classSQL.select(sql, "normativi");
            dgv.DataSource = DSnormativ.Tables[0];
        }

        private void btnUrediSifru_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            if (dgv.RowCount > 0)
            {
                string broj = dgv.CurrentRow.Cells["Broj normativa"].Value.ToString();

                if (this.MdiParent != null)
                {
                    var mdiForm = this.MdiParent;
                    mdiForm.IsMdiContainer = true;
                    var childForm = new frmNormativi();
                    childForm.broj_normativa_edit = broj;
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                }
                else
                {
                    MainForm.broj_normativa_edit = broj;
                    MainForm.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Niste odabrali niti jednu stavku.", "Greška");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            filldgv();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            Report.Liste.frmListe rfak = new Report.Liste.frmListe();
            rfak.dokument = "NORMATIV";
            rfak.ImeForme = "Normativi";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj normativa"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        private void dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Report.Liste.frmListe rfak = new Report.Liste.frmListe();
            rfak.dokument = "NORMATIV";
            rfak.ImeForme = "Normativi";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj normativa"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        private void btnIspisBezCijena_Click(object sender, EventArgs e)
        {
            Report.NormativBezCijene.frmNormativBezCijene rfak = new Report.NormativBezCijene.frmNormativBezCijene();
            rfak.dokument = "NORMATIV";
            rfak.ImeForme = "Normativi";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj normativa"].FormattedValue.ToString();
            rfak.ShowDialog();
        }
    }
}