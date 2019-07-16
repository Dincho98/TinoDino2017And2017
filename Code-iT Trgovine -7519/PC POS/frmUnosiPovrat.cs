using System;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmUnosiPovrat : Form
    {
        public frmUnosiPovrat()
        {
            InitializeComponent();
        }

        public string povrat_na { get; set; }
        public frmNovaKalkulacija kalku { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            if (povrat_na == "KALK")
            {
                decimal d;
                if (!decimal.TryParse(txtUnos.Text, out d))
                {
                    txtUnos.Text = "0";
                }

                kalku.kolicina_unesi_povrat = txtUnos.Text;
                this.Close();
            }
        }

        private void frmUnosiPovrat_Load(object sender, EventArgs e)
        {
            txtUnos.Select();
        }

        private void txtUnos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}