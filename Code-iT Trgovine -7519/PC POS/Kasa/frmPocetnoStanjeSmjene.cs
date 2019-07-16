using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Kasa
{
    public partial class frmPocetnoStanjeSmjene : Form
    {
        public frmPocetnoStanjeSmjene()
        {
            InitializeComponent();
        }

        private void frmPocetnoStanje_Load(object sender, EventArgs e)
        {
            SetData();
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void SetData()
        {
            DataTable DT_zaposlenik = classSQL.select("SELECT ime + ' ' + prezime FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0];

            lblZaposlenik.Text = DT_zaposlenik.Rows.Count > 0 ? "Prijavljen: " + DT_zaposlenik.Rows[0][0].ToString() : "";

            try
            {
                DataTable DTmin = classSQL.select("SELECT pocetno_stanje FROM smjene WHERE id='" + ZadnjiBrojSmjene() + "'", "smjene").Tables[0];
                if (DTmin.Rows.Count > 0)
                    txtPocetno.Text = DTmin.Rows[0][0].ToString();
                else
                    txtPocetno.Text = "0";
            }
            catch (Exception)
            {
            }
        }

        private string ZadnjiBrojSmjene()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(id) FROM smjene", "smjene").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString())).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((byte)(105)), ((byte)(170)), ((byte)(197)));
            Color y = System.Drawing.Color.FromArgb(((byte)(40)), ((byte)(109)), ((byte)(135)));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void btnZapocniSmjenu_Click(object sender, EventArgs e)
        {
            decimal dec_parse;
            if (!Decimal.TryParse(txtPocetno.Text, out dec_parse))
            {
                MessageBox.Show("Krivo upisani polog.", "Upozorenje"); return;
            }

            string sql = "INSERT INTO smjene " +
                " (pocetno_stanje,blagajnik,pocetak) VALUES " +
                " (" +
                "'" + txtPocetno.Text.Replace(",", ".") + "'," +
                "'" + Properties.Settings.Default.id_zaposlenik + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                ")";
            classSQL.insert(sql);

            this.Close();
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPocetno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnZapocniSmjenu.PerformClick();
            }
            else if (e.KeyData == Keys.Escape)
            {
                button1.PerformClick();
            }
        }
    }
}