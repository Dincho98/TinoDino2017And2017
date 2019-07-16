using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmKnjigaPII : Form
    {
        public frmKnjigaPII()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string OD = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd");
            string DO = dateTimePicker2.Value.Date.ToString("yyyy-MM-dd");

            Report.Knjigaprimitakaiizadatak.knjigaPII kpii = new Report.Knjigaprimitakaiizadatak.knjigaPII();
            kpii.datumOD = OD;
            kpii.datumDO = DO;
            kpii.ShowDialog();
        }

        private void frmKnjigaPII_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }
    }
}