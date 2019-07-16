using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS

{
    public partial class frmTestBoje : Form
    {
        public frmTestBoje()
        {
            InitializeComponent();
        }

        public Int32 proba1 { get; set; }
        public Int32 proba2 { get; set; }
        public Int32 proba3 { get; set; }
        public Int32 proba4 { get; set; }
        public Int32 proba5 { get; set; }
        public Int32 proba6 { get; set; }

        private void frmTestBoje_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTestBoje_Load(object sender, EventArgs e)
        {
        }

        private void frmTestBoje_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.FromArgb(proba1, proba2, proba3), Color.FromArgb(proba4, proba5, proba6), 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }
    }
}