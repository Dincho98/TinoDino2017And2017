using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmIspisProdajnihArtiklaPoDanima : Form
    {
        public frmIspisProdajnihArtiklaPoDanima()
        {
            InitializeComponent();
        }

        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private void frmIspisProdajnihArtiklaNaMaliPrinter_Load(object sender, EventArgs e)
        {
            SetCB();
            dtpOD.Value = Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " 00:00:00");
            dtpDO.Value = Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " 23:59:59");
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

        private DataTable DT_Zaposlenik;
        private DataTable DT_Skladiste;
        private DataTable DT_Ducan;
        private DataTable DT_Zaposlenik2 = new DataTable();
        private DataTable DT_Skladiste2 = new DataTable();
        private DataTable DT_Ducan2 = new DataTable();
        //DataTable DT_Ducan;

        private void SetCB()
        {
            //fill komercijalist
            DT_Ducan = classSQL.select("SELECT ime_ducana,id_ducan FROM ducan", "ducan").Tables[0];
            cbDucan.DataSource = DT_Ducan;
            cbDucan.DisplayMember = "ime_ducana";
            cbDucan.ValueMember = "id_ducan";

            cbDucan.SelectedValue = DTpostavke.Rows[0]["default_ducan"].ToString();

            //fill grupe
            DataTable DT_cbGrupa = classSQL.select("SELECT grupa,id_grupa FROM grupa", "grupa").Tables[0];
            cbGrupa.DataSource = DT_cbGrupa;
            cbGrupa.DisplayMember = "grupa";
            cbGrupa.ValueMember = "id_grupa";

            //fill komercijalist
            DT_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici").Tables[0];
            cbZaposlenik.DataSource = DT_Zaposlenik;
            cbZaposlenik.DisplayMember = "IME";
            cbZaposlenik.ValueMember = "id_zaposlenik";

            DT_Skladiste2.Columns.Add("id_podgrupa", typeof(string));
            DT_Skladiste2.Columns.Add("naziv", typeof(string));

            DT_Skladiste = classSQL.select("SELECT * FROM podgrupa", "podgrupa").Tables[0];
            for (int i = 0; i < DT_Skladiste.Rows.Count; i++)
            {
                DT_Skladiste2.Rows.Add(DT_Skladiste.Rows[i]["id_podgrupa"].ToString(), DT_Skladiste.Rows[i]["naziv"].ToString());
            }

            cbSkladiste.DataSource = DT_Skladiste2;
            cbSkladiste.DisplayMember = "naziv";
            cbSkladiste.ValueMember = "id_podgrupa";
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
            Report.PrometiPoDanima.frmListePoDanima aa = new Report.PrometiPoDanima.frmListePoDanima();
            aa.datumOD = dtpOD.Value;
            aa.datumDO = dtpDO.Value;
            aa.ImeForme = "Promet po danima";
            aa.dokumenat = "PrometRobe";

            if (chbDucan.Checked)
            {
                aa.ducan = cbDucan.SelectedValue.ToString();
            }
            if (chbKasa.Checked)
            {
                aa.kasa = cbKasa.SelectedValue.ToString();
            }
            if (chbBlagajnik.Checked)
            {
                aa.blagajnik = cbZaposlenik.SelectedValue.ToString();
            }
            if (chbSkladiste.Checked)
            {
                aa.id_podgrupa = cbSkladiste.SelectedValue.ToString();
            }

            if (chbGrupa.Checked)
            {
                aa.grupa = cbGrupa.SelectedValue.ToString();
            }

            aa.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Report.PrometiPoDanima.frmPosPreview aa = new Report.PrometiPoDanima.frmPosPreview();
            aa.datumOD = dtpOD.Value;
            aa.datumDO = dtpDO.Value;
            aa.ImeForme = "Promet po danima";
            aa.dokumenat = "PrometRobe";

            if (chbDucan.Checked)
            {
                aa.ducan = cbDucan.SelectedValue.ToString();
            }
            if (chbKasa.Checked)
            {
                aa.kasa = cbKasa.SelectedValue.ToString();
            }
            if (chbBlagajnik.Checked)
            {
                aa.blagajnik = cbZaposlenik.SelectedValue.ToString();
            }
            if (chbSkladiste.Checked)
            {
                aa.id_podgrupa = cbSkladiste.SelectedValue.ToString();
            }

            if (chbGrupa.Checked)
            {
                aa.grupa = cbGrupa.SelectedValue.ToString();
            }
            aa.ShowDialog();
        }

        private void chbDucan_CheckedChanged(object sender, EventArgs e)
        {
            cbDucan.Enabled = !chbDucan.Checked;
            cbKasa.Enabled = chbDucan.Checked;
            chbKasa.Enabled = chbDucan.Checked;

            if (chbDucan.Checked)
            {
                DataTable DTblagajna = classSQL.select("SELECT * FROM blagajna WHERE id_ducan=" + cbDucan.SelectedValue.ToString(), "blagajna").Tables[0];
                cbKasa.DataSource = DTblagajna;
                cbKasa.DisplayMember = "ime_blagajne";
                cbKasa.ValueMember = "id_blagajna";
                cbKasa.SelectedValue = DTpostavke.Rows[0]["default_blagajna"].ToString();
            }
            else chbKasa.Checked = false;
        }
    }
}