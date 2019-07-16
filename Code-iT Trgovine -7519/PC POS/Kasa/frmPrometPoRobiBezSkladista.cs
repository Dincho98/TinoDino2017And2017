using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Kasa
{
    public partial class frmPrometPoRobiBezSkladista : Form
    {
        public frmPrometPoRobiBezSkladista()
        {
            InitializeComponent();
        }

        private void frmPrometPoRobiBezSkladista_Load(object sender, EventArgs e)
        {
            //za novi izvještaj
            button1.Enabled = false;
            button1.Visible = false;

            SetCB();
            dtpOD.Value = Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " 00:00:00");
            dtpDO.Value = Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " 23:59:59");
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 1000);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private DataTable DT_Zaposlenik;
        private DataTable DT_Skladiste;
        private DataTable DT_Ducan;
        private DataTable DT_Zaposlenik2 = new DataTable();
        private DataTable DT_Ducan2 = new DataTable();

        //DataTable DT_Ducan;
        private void SetCB()
        {
            //fill komercijalist
            DT_Ducan = classSQL.select("SELECT ime_ducana,id_ducan FROM ducan", "ducan").Tables[0];
            cbDucan.DataSource = DT_Ducan;
            cbDucan.DisplayMember = "ime_ducana";
            cbDucan.ValueMember = "id_ducan";

            //fill komercijalist
            DT_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici").Tables[0];
            cbZaposlenik.DataSource = DT_Zaposlenik;
            cbZaposlenik.DisplayMember = "IME";
            cbZaposlenik.ValueMember = "id_zaposlenik";
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w + 7, h + 7);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w - 7, h - 7);
        }

        private void dtpOD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.F1)
            {
                btnIspis.PerformClick();
            }
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            Ispis();
            //this.Close();
        }

        private void Ispis()
        {
            Report.Kasa.frmReportPrometBezSkladista aa = new Report.Kasa.frmReportPrometBezSkladista();
            aa.datumOD = dtpOD.Value;
            aa.datumDO = dtpDO.Value;
            aa.ImeForme = "Promet po robi";
            aa.dokumenat = "PrometPoRobiBezSkladista";
            if (txtArtikl.Text != "")
            {
                aa.artikl = txtArtikl.Text;
            }
            if (chbDucan.Checked)
            {
                aa.ducan = cbDucan.SelectedValue.ToString();
            }
            if (chbBlagajnik.Checked)
            {
                aa.blagajnik = cbZaposlenik.SelectedValue.ToString();
            }

            aa.samoPorezi = chbSamoPorezi.Checked ? true : false;

            aa.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Report.Kasa.frmReportPrometBezSkladista2 aa = new Report.Kasa.frmReportPrometBezSkladista2();
            aa.datumOD = dtpOD.Value;
            aa.datumDO = dtpDO.Value;
            aa.ImeForme = "Promet po robi";
            aa.dokumenat = "PrometPoRobiBezSkladista";
            if (txtArtikl.Text != "")
            {
                aa.artikl = txtArtikl.Text;
            }
            if (chbDucan.Checked)
            {
                aa.ducan = cbDucan.SelectedValue.ToString();
            }
            if (chbBlagajnik.Checked)
            {
                aa.blagajnik = cbZaposlenik.SelectedValue.ToString();
            }

            aa.samoPorezi = chbSamoPorezi.Checked ? true : false;

            aa.ShowDialog();
        }
    }
}