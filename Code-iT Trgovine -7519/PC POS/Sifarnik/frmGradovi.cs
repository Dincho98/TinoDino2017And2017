using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class Gradovi : Form
    {
        public Gradovi()
        {
            InitializeComponent();
        }

        private DataSet Dsgrad = new DataSet();

        private void frmGradovi_Load(object sender, EventArgs e)
        {
            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter("SELECT id_grad AS [Šifra], grad AS [Ime Grada], posta AS [Broj pošte], zupanija AS Županija FROM grad").Fill(Dsgrad, "Grad");
            }
            else
            {
                classSQL.NpgAdatpter("SELECT id_grad AS [Šifra], grad AS [Ime Grada], posta AS [Broj pošte], zupanija AS [Županija] FROM grad").Fill(Dsgrad, "Grad");
            }

            dgvGrad.DataSource = Dsgrad.Tables["Grad"];

            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter("SELECT id_grad AS [Šifra], grad AS [Ime Grada], posta AS [Broj pošte], zupanija AS Županija, naselje as Naselje FROM grad").Update(Dsgrad, "Grad");
            }
            else
            {
                classSQL.NpgAdatpter("SELECT id_grad AS [Šifra], grad AS [Ime Grada], posta AS [Broj pošte], zupanija AS [Županija], naselje as Naselje FROM grad").Update(Dsgrad, "Grad");
            }
            MessageBox.Show("Spremljeno.");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Dsgrad = new DataSet();
            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(string.Format("SELECT TOP(200) id_grad AS [Šifra], grad AS [Ime Grada], posta AS [Broj pošte], zupanija AS [Županija], naselje AS [Naselje] FROM grad WHERE grad LIKE '%{0}%'", textBox2.Text)).Fill(Dsgrad, "Grad");
            }
            else
            {
                classSQL.NpgAdatpter(string.Format("SELECT id_grad AS [Šifra], grad AS [Ime Grada], posta AS [Broj pošte], zupanija AS [Županija], naselje AS [Naselje] FROM grad WHERE grad ~* '{0}' LIMIT 500", textBox2.Text)).Fill(Dsgrad, "Grad");
            }

            dgvGrad.DataSource = Dsgrad.Tables["Grad"];
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Dsgrad = new DataSet();

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(string.Format("SELECT TOP(200) id_grad AS Šifra, grad AS [Ime Grada], posta AS [Broj pošte], zupanija AS [Županija], naselje AS [Naselje] FROM grad WHERE posta LIKE '%{0}%'", textBox1.Text)).Fill(Dsgrad, "Grad");
            }
            else
            {
                classSQL.NpgAdatpter(string.Format("SELECT id_grad AS [Šifra], grad AS [Ime Grada], posta AS [Broj pošte], zupanija AS [Županija], naselje AS [Naselje] FROM grad WHERE posta LIKE '%{0}%' LIMIT 500", textBox1.Text)).Fill(Dsgrad, "Grad");
            }

            dgvGrad.DataSource = Dsgrad.Tables["Grad"];
        }
    }
}