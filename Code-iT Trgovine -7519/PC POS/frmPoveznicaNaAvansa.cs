using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmPoveznicaNaAvansa : Form
    {
        public string broj_fakture { get; set; }

        public frmPoveznicaNaAvansa()
        {
            InitializeComponent();
        }

        private void frmPoveznicaNaAvansa_Load(object sender, EventArgs e)
        {
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

        public string dokumenat { get; set; }

        private void frmTrazi_Click(object sender, EventArgs e)
        {
            txtBrojAvansa.Text = "";
            txtGodinaAvansa.Text = "";
            broj_avansa_edit = 0;
            godina = 0;

            frmSviAvansi objForm2 = new frmSviAvansi();
            objForm2.FormPoveznicaAvans = this;
            objForm2.ShowDialog();
            if (broj_avansa_edit != 0 && godina != 0)
            {
                txtBrojAvansa.Text = broj_avansa_edit.ToString();

                txtGodinaAvansa.Text = godina.ToString();
            }
        }

        public string broj { get; set; }
        public int broj_avansa_edit { get; set; }
        public int godina { get; set; }

        private void btnDodajNaAvans_Click(object sender, EventArgs e)
        {
            if (txtBrojAvansa.Text != "" && txtGodinaAvansa.Text != "")
            {
                classSQL.update("UPDATE fakture SET broj_avansa=" + txtBrojAvansa.Text + " , godina_avansa=" + txtGodinaAvansa.Text + "  WHERE broj_fakture=" + broj_fakture + "");
                MessageBox.Show("Dodan avans " + txtBrojAvansa.Text + " na fakturu broj " + broj_fakture);
            }
            else
            {
                MessageBox.Show("nema podataka!");
            }
        }

        private void btnUkloniSaAvansa_Click(object sender, EventArgs e)
        {
            classSQL.update("UPDATE fakture SET broj_avansa=0 , godina_avansa=0 WHERE broj_fakture=" + broj_fakture + "");
            MessageBox.Show("Obrisan avans " + txtBrojAvansa.Text + " sa fakture broj " + broj_fakture);
        }
    }
}