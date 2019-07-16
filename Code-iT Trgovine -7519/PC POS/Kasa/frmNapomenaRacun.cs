using System;
using System.Windows.Forms;

namespace PCPOS.Kasa
{
    public partial class frmNapomenaRacun : Form
    {
        public string napomena { get; set; }

        public frmNapomenaRacun()
        {
            InitializeComponent();
        }

        private void frmNapomenaRacun_Load(object sender, EventArgs e)
        {
            napomena = "";
        }

        private void zanemari_Click(object sender, EventArgs e)
        {
            napomena = "";
            this.Close();
        }

        private void ispis_Click(object sender, EventArgs e)
        {
            napomena = rtbNapomena.Text;
            this.Close();
        }
    }
}