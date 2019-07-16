using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PCPOS.Kartoteka
{
    public partial class frmpretraga : Form
    {
        public frmunospodsjetnika MainForm_unospodsjetnika { get; set; }

        public frmpretraga()
        {
            InitializeComponent();
        }

        private Int32 var1 = 0;
        private Int32 var2 = 0;
        private Int32 var3 = 0;
        private Int32 var4 = 0;
        private Int32 var5 = 0;
        private Int32 var6 = 0;

        private void tbxpretragaime_TextChanged(object sender, EventArgs e)
        {
            string limit = "";
            string limitRemote = "";
            string where = "";

            limitRemote = " LIMIT 40 ";
            where = "partners.ime ~* '" + tbxpretragaime.Text + "'";

            string sql = "SELECT " + limit + " partners.ime as [Ime],partners.prezime AS [Prezime],partners.adresa as [Adresa], partners.mob as [Broj_mobitela], " +
                "  partners.oib AS [OIB], partners.datum_rodenja AS [Datum_rodenja] " +
                "  FROM partners" +
                "  WHERE " + where + limitRemote;
            dataGridView1.DataSource = classSQL.select(sql, "partners").Tables[0];
            PaintRows(dataGridView1);

            dataGridView1.Columns[1].Width = 150;
        }

        private void PaintRows(DataGridView dg)
        {
            int br = 0;
            for (int i = 0; i < dg.Rows.Count; i++)
            {
                if (br == 0)
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
                    br++;
                }
                else
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    br = 0;
                }
            }
            DataGridViewRow row = dg.RowTemplate;
            row.Height = 35;
        }

        private void tbxpretragaprezime_TextChanged(object sender, EventArgs e)
        {
            string limit = "";
            string limitRemote = "";
            string where = "";

            limitRemote = " LIMIT 40 ";
            where = "partners.prezime ~* '" + tbxpretragaprezime.Text + "'";

            string sql = "SELECT " + limit + " partners.ime as [Ime],partners.prezime AS [Prezime],partners.adresa as [Adresa], partners.mob as [Broj_mobitela], " +
                "  partners.oib AS [OIB], partners.datum_rodenja AS [Datum_rodenja] " +
                "  FROM partners" +
                "  WHERE " + where + limitRemote;
            dataGridView1.DataSource = classSQL.select(sql, "partners").Tables[0];
            PaintRows(dataGridView1);
            dataGridView1.Columns[2].Width = 250;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 119;
            dataGridView1.Columns[5].Width = 119;
        }

        private void tbxpretragaoib_TextChanged(object sender, EventArgs e)
        {
            string limit = "";
            string limitRemote = "";
            string where = "";

            limitRemote = " LIMIT 1000 ";
            where = "partners.oib ~* '" + tbxpretragaoib.Text + "'";

            string sql = "SELECT " + limit + " partners.ime as [Ime],partners.prezime AS [Prezime],partners.adresa as [Adresa], partners.mob as [Broj_mobitela], " +
                "  partners.oib AS [OIB], partners.datum_rodenja AS [Datum_rodenja] " +
                "  FROM partners" +
                "  WHERE " + where + limitRemote;
            dataGridView1.DataSource = classSQL.select(sql, "partners").Tables[0];
            PaintRows(dataGridView1);

            dataGridView1.Columns[2].Width = 250;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 119;
            dataGridView1.Columns[5].Width = 119;
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int col = dataGridView1.CurrentCell.ColumnIndex;
            int row = dataGridView1.CurrentCell.RowIndex;

            string celija_ime = dataGridView1.Rows[row].Cells[0].FormattedValue.ToString();
            string celija_prezime = dataGridView1.Rows[row].Cells[1].FormattedValue.ToString();

            DataTable podaci = new DataTable();

            podaci = classSQL.select("SELECT" +
                " partners.ime_tvrtke, " +
                " partners.email " +
                " FROM partners " +
                " WHERE partners.ime = '" + celija_ime + "' AND partners.prezime = '" + celija_prezime + "' ", "partners").Tables[0];

            if (celija_ime == "")
            {
                string celija_ime_tvrtke = podaci.Rows[0][0].ToString();
                MainForm_unospodsjetnika.tbxpodsjime.Text = celija_ime_tvrtke;
                MainForm_unospodsjetnika.tbxpodsjprezime.Clear();
            }
            else
            {
                MainForm_unospodsjetnika.tbxpodsjime.Text = celija_ime;
                MainForm_unospodsjetnika.tbxpodsjprezime.Text = celija_prezime;
            }
            string email_kljenta = podaci.Rows[0][1].ToString();
            MainForm_unospodsjetnika.tbxpodsjklijentaemail.Text = email_kljenta;
            this.Close();
        }

        private void popuni_grid()
        {
            string sql = "SELECT partners.ime as [Ime], partners.prezime AS [Prezime], partners.adresa as [Adresa], partners.mob as [Broj_mobitela], " +
        "  partners.oib AS [OIB], partners.datum_rodenja AS [Datum_rodenja] " +
        "  FROM partners LIMIT 500";
            dataGridView1.DataSource = classSQL.select(sql, "partners").Tables[0];
            PaintRows(dataGridView1);

            dataGridView1.Columns[2].Width = 250;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 119;
            dataGridView1.Columns[5].Width = 119;
        }

        private void frmpretraga_Load(object sender, EventArgs e)
        {
            SetRemoteFields();

            popuni_grid();
        }

        private void SetRemoteFields()
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\postavke.xml";
            XDocument xmlFile = XDocument.Load(path);
            var query = from c in xmlFile.Element("postavke").Elements("email").Elements("gmail") select c;
            foreach (XElement book in query)
            {
                var1 = Convert.ToInt32(book.Attribute("var1").Value);
                var2 = Convert.ToInt32(book.Attribute("var2").Value);
                var3 = Convert.ToInt32(book.Attribute("var3").Value);
                var4 = Convert.ToInt32(book.Attribute("var4").Value);
                var5 = Convert.ToInt32(book.Attribute("var5").Value);
                var6 = Convert.ToInt32(book.Attribute("var6").Value);
            }
        }

        private void frmpretraga_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.FromArgb(var1, var2, var3), Color.FromArgb(var4, var5, var6), 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }
    }
}