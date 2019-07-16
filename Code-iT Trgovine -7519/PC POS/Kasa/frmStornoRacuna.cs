using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Kasa
{
    public partial class frmStornoRacuna : Form
    {
        public string brojRacuna = "-1";

        public frmStornoRacuna()
        {
            InitializeComponent();
        }

        private void frmStornoRacuna_Load(object sender, EventArgs e)
        {
            textBox1.Select();
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                button2.PerformClick();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable DT = classSQL.select("SELECT broj_racuna FROM racuni WHERE broj_racuna='" + textBox1.Text + "'", "racuni").Tables[0];

            if (DT.Rows.Count != 0)
            {
                try
                {
                    long broj = Convert.ToInt64(textBox1.Text);
                    brojRacuna = broj.ToString();
                }
                catch
                {
                    MessageBox.Show("Krivi broj računa.", "Greška");
                }
            }
            else
            {
                MessageBox.Show("Krivi broj računa.", "Greška");
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}