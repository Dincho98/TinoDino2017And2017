using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class staracki_dom : Form
    {
        public staracki_dom()
        {
            InitializeComponent();
        }

        private DataSet DSpartneri = new DataSet();
        public frmMenu MainFormMenu { get; set; }

        private void staracki_dom_Load(object sender, EventArgs e)
        {
            if (MainFormMenu == null)
            {
                int heigt = SystemInformation.VirtualScreen.Height;
                this.Height = heigt - 60;
                this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);
            }
            else
            {
                int heigt = SystemInformation.VirtualScreen.Height;
                this.Height = heigt - 140;
                this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);
            }

            fillDataGrid();
            this.Paint += new PaintEventHandler(staracki_dom_Paint);
            Set();
        }

        private DataSet DSfakture = new DataSet();

        private void fillDataGrid()
        {
            string top = "";
            string remote = "";
            if (classSQL.remoteConnectionString != "")
            {
                remote = " LIMIT 500";
            }
            else
            {
                top = " TOP(500) ";
            }

            string sql = "SELECT " + top + " partners.id_partner AS [ID],partners.ime AS [Ime], partners.prezime AS [Prezime]," +
                "partners.datum_rodenja AS [Datum rođenja] " +
                " FROM partners WHERE partners.ime <> '' OR partners.prezime <> '' " +
                " ORDER BY id_partner DESC " + "" + remote + "";

            DSfakture = classSQL.select(sql, "fakture");
            dgv.DataSource = DSfakture.Tables[0];
            dgv.Columns["ID"].Visible = false;
        }

        private void Set()
        {
            if (dgv.Rows.Count > 0)
            {
                label4.Visible = true;
                string br = dgv.Rows[0].Cells["ID"].FormattedValue.ToString();
                label4.Text = classSQL.select("SELECT ime + '  ' + prezime as osoba FROM partners WHERE id_partner = '" + br + "'", "upit").Tables[0].Rows[0]["osoba"].ToString();
            }
            else
            {
                label4.Visible = false;
            }
        }

        private void staracki_dom_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 500);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                return;
            }

            string top = "";
            string remote = "";
            string where = "";
            if (classSQL.remoteConnectionString != "")
            {
                remote = " LIMIT 500";
                //where = "CAST(ime AS TEXT) LIKE '%" + textIme.Text + "%'";
                where = "ime ~* '" + textBox1.Text + "'";
            }
            else
            {
                where = "ime LIKE '%" + textBox1.Text + "%'";
                top = " TOP(500) ";
            }

            DSpartneri = classSQL.select("SELECT " + top + " partners.id_partner AS ID," +
                " partners.ime as [Ime], partners.prezime as [Prezime], partners.datum_rodenja AS [Datum rođenja]" +
                " FROM partners " +
                " WHERE " + where + " ORDER BY partners.ime " + remote + "", "partners");
            dgv.DataSource = DSpartneri.Tables[0];
            dgv.Columns["ID"].Visible = false;

            PaintRows(dgv);
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
            row.Height = 25;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                return;
            }

            string top = "";
            string remote = "";
            string where = "";
            if (classSQL.remoteConnectionString != "")
            {
                remote = " LIMIT 500";
                //where = "CAST(prezime AS TEXT) LIKE '%" + textPrezime.Text + "%'";
                where = "prezime ~* '" + textBox2.Text + "'";
            }
            else
            {
                where = "prezime LIKE '%" + textBox2.Text + "%'";
                top = " TOP(500) ";
            }

            string sql = "SELECT " + top + " partners.id_partner AS ID," +
                " partners.ime as [Ime], partners.prezime as [Prezime], partners.datum_rodenja AS [Datum rođenja]" +
                " FROM partners " +
                " WHERE " + where + " ORDER BY partners.prezime " + remote + "";

            DSpartneri = classSQL.select(sql, "partners");
            dgv.DataSource = DSpartneri.Tables[0];
            dgv.Columns["ID"].Visible = false;

            PaintRows(dgv);
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv.Rows.Count > 0)
                {
                    string br = dgv.CurrentRow.Cells["ID"].FormattedValue.ToString();
                    label4.Text = classSQL.select("SELECT ime + '  ' + prezime as osoba FROM partners WHERE id_partner = '" + br + "'", "upit").Tables[0].Rows[0]["osoba"].ToString();
                }
                else
                {
                    MessageBox.Show("Niste odabrali niti jedan zapis !");
                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.Rows.Count > 0)
                {
                    Report.Staracki.frmListe star = new Report.Staracki.frmListe();
                    try
                    {
                        star.partner = dgv.CurrentRow.Cells["ID"].FormattedValue.ToString();
                    }
                    catch { star.partner = dgv.Rows[0].Cells["ID"].FormattedValue.ToString(); }
                    star.datumOD = Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                    star.datumDO = Convert.ToDateTime(dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                    star.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Niste odabrali niti jedan zapis !");
                }
            }
            catch { }
        }
    }
}