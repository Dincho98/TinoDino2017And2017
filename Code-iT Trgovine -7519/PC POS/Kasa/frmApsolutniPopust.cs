using System;
using System.Windows.Forms;

namespace PCPOS.Kasa
{
    public partial class frmApsolutniPopust : Form
    {
        private decimal popust = 0;

        public frmApsolutniPopust()
        {
            InitializeComponent();
        }

        private void bttOK_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(textBox1.Text, out popust)) this.Close();
            else MessageBox.Show("Greška pri unosu popusta", "Greška");
        }

        public decimal napraviPopust(decimal cijena)
        {
            this.ShowDialog();
            if (popust > cijena)
            {
                MessageBox.Show("Popust ne može biti veći od iznosa");
                return 0;
            }
            return (popust * -1);
        }

        private void bttCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) bttOK.PerformClick();
            if (e.KeyCode == Keys.Escape) bttCancel.PerformClick();
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w + 3, h + 3);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w - 3, h - 3);
        }
    }
}