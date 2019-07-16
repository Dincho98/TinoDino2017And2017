using System;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //if (this.dataGridView1.Columns["ime"].Index ==
            //    e.ColumnIndex && e.RowIndex >= 0)
            //{
            //    if (this.dataGridView1.Columns["ime"].Index ==
            //        e.ColumnIndex && e.RowIndex >= 0)
            //    {
            //        Color c1 = Color.Red; //Color.FromArgb(255, 113, 255, 0);
            //        Color c2 = Color.Black; // Color.FromArgb(255, 255, 255, 255);

            //        LinearGradientBrush br = new LinearGradientBrush(e.CellBounds, Color.Red, Color.Black, 0, true);
            //        ColorBlend cb = new ColorBlend();
            //        cb.Positions = new[] { 0, (float)1 };
            //        cb.Colors = new[] { c1, c2 };
            //        br.InterpolationColors = cb;

            //        Rectangle rect = new Rectangle(e.CellBounds.Location.X , e.CellBounds.Location.Y , 50,20);
            //        Rectangle rect1 = new Rectangle(e.CellBounds.Location.X + 50, e.CellBounds.Location.Y, 50, 10);
            //        e.Graphics.FillRectangle(br, rect);
            //        e.Graphics.FillRectangle(br, rect1);
            //        e.PaintContent(rect);
            //        e.PaintContent(rect1);
            //        e.Handled = true;
            //    }
            //}
        }
    }
}