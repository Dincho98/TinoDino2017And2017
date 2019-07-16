using System;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmTrgovackaKnjigaLoad : Form
    {
        public frmTrgovackaKnjigaLoad()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Report.TrgovackaKnjiga.frmTrgovackaKnjiga frm = new Report.TrgovackaKnjiga.frmTrgovackaKnjiga();
            frm.datumOD = dateTimePicker1.Value;
            frm.datumDO = dateTimePicker2.Value;
            if (cbRabati.Checked)
            {
                frm.sa_rabatom = cbRabati.Checked;
            }
            //this.Close();
            frm.ShowDialog();
        }

        private void bttOdustani_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}