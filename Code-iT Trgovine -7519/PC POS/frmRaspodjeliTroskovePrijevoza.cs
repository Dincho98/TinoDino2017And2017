using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmRaspodjeliTroskovePrijevoza : Form
    {
        public frmRaspodjeliTroskovePrijevoza()
        {
            InitializeComponent();
        }

        public frmNovaKalkulacija2 Kalkulacija { get; set; }

        private void frmRaspodjeliTroskovePrijevoza_Load(object sender, EventArgs e)
        {
            txtUkupnoPrijevozZaRaspodjelu.Text = "0";
        }

        private void btnRaspodjeli_Click(object sender, EventArgs e)
        {
            decimal IznosUkupnogPrijevoza;
            decimal.TryParse(txtUkupnoPrijevozZaRaspodjelu.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out IznosUkupnogPrijevoza);
            Kalkulacija.PreracunajPrijevozPoStavkama(IznosUkupnogPrijevoza);
            Kalkulacija.SetTrosak(IznosUkupnogPrijevoza);
            this.Close();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }
    }
}