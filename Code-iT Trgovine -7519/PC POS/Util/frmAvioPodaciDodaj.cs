using System;
using System.Windows.Forms;

namespace PCPOS.Util
{
    public partial class frmAvioPodaciDodaj : Form
    {
        public string[] podaci { get; set; }
        private bool maloprodaja = false;

        public frmAvioPodaciDodaj(bool _maloprodaja = false)
        {
            maloprodaja = _maloprodaja;
            InitializeComponent();
            if (maloprodaja)
            {
                btnObrisi.Text = "Odustani";
            }
        }

        private void frmAvioPodaciDodaj_Load(object sender, EventArgs e)
        {
            if (podaci != null)
            {
                txtRegistracijskaOznaka.Text = podaci[0];
                txtTipZrakoplova.Text = podaci[1];
                txtMaksTezinaPolijetanja.Text = Convert.ToDecimal(podaci[2].ToString()).ToString("0.00");
            }
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            decimal tezina = 0;
            if (decimal.TryParse(txtMaksTezinaPolijetanja.Text, out tezina) && tezina > 0)
            {
                if (MessageBox.Show("Dodaj upisane podatke na fakturu?", "Podaci zrakoplova", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) { return; }

                podaci = new string[3];
                podaci[0] = txtRegistracijskaOznaka.Text;
                podaci[1] = txtTipZrakoplova.Text;
                podaci[2] = tezina.ToString();
                Close();
            }
            else
            {
                MessageBox.Show("Krivi iznos za količinu.");
            }
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (!maloprodaja)
                if (MessageBox.Show("Obriši upisane podatke sa fakture?", "Podaci zrakoplova", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) { return; }

            podaci = null;
            Close();
        }
    }
}