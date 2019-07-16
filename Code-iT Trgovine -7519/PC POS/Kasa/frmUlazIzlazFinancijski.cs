using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Kasa
{
    public partial class frmUlazIzlazFinancijski : Form
    {
        public frmUlazIzlazFinancijski()
        {
            InitializeComponent();
        }

        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private void frmPrometKase_Load(object sender, EventArgs e)
        {
            SetCB();
            dtpOD.Value = Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " 00:00:00");
            dtpDO.Value = Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " 23:59:59");
        }

        private DataTable DT_Zaposlenik;
        private DataTable DT_Skladiste;
        private DataTable DT_Ducan;
        private DataTable DT_Zaposlenik2 = new DataTable();
        private DataTable DT_Skladiste2 = new DataTable();
        private DataTable DT_Grupe = new DataTable();
        private DataTable DT_dobavljaci = new DataTable();
        private DataTable DT_Ducan2 = new DataTable();

        private void SetCB()
        {
            //fill komercijalist
            DT_Ducan = classSQL.select("SELECT ime_ducana,id_ducan FROM ducan", "ducan").Tables[0];
            cbDucan.DataSource = DT_Ducan;
            cbDucan.DisplayMember = "ime_ducana";
            cbDucan.ValueMember = "id_ducan";

            cbDucan.SelectedValue = DTpostavke.Rows[0]["default_ducan"].ToString();

            //fill komercijalist
            DT_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici").Tables[0];
            cbZaposlenik.DataSource = DT_Zaposlenik;
            cbZaposlenik.DisplayMember = "IME";
            cbZaposlenik.ValueMember = "id_zaposlenik";

            DT_Skladiste2.Columns.Add("id_skladiste", typeof(string));
            DT_Skladiste2.Columns.Add("skladiste", typeof(string));

            DT_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste").Tables[0];

            for (int i = 0; i < DT_Skladiste.Rows.Count; i++)
            {
                DT_Skladiste2.Rows.Add(DT_Skladiste.Rows[i]["id_skladiste"].ToString(), DT_Skladiste.Rows[i]["skladiste"].ToString());
            }
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
            Kasa.frmUlazIzlazIspis aa = new Kasa.frmUlazIzlazIspis();
            aa.OD_datuma = dtpOD.Value;
            aa.DO_datuma = dtpDO.Value;
            aa.PoslovnicaNaziv = cbDucan.Text;
            if (chbBlagajnik.Checked) { aa.cbZaposlenik = cbZaposlenik.SelectedValue.ToString(); aa.ZaposlenikNaziv = cbZaposlenik.Text; }
            if (chbDucan.Checked) { aa.cbDucan = cbDucan.SelectedValue.ToString(); }
            aa.ShowDialog();
        }

        private void btnIspisPOS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Report.Kasa.frmPrometKase pr = new Report.Kasa.frmPrometKase();
            pr.ShowDialog();
        }
    }
}