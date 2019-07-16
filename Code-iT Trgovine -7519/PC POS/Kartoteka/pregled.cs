using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PCPOS.Kartoteka
{
    public partial class pregled : Form
    {
        public pregled()
        {
            InitializeComponent();
        }

        public galerija MainForm_galerija { get; set; }

        private void pregled_Load(object sender, EventArgs e)
        {
            if (File.Exists(galerija.ImageToShow))
            {
                imgDisplay.Image = Image.FromFile(galerija.ImageToShow);
                lblImageName.Text = galerija.ImageToShow;
                imgDisplay.SizeMode = PictureBoxSizeMode.StretchImage;
                imgDisplay.SetBounds(0, 0, 703, 478);

                // Center image
                imgDisplay.Left = (this.Width - imgDisplay.Width) / 2;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void brisi(object sender, EventArgs e)
        {
        }
    }
}