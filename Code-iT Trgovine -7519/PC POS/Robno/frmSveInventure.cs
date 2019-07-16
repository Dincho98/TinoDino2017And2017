using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmSveInventure : Form
    {
        public frmSveInventure()
        {
            InitializeComponent();
        }

        public frmUnosInventura MainForm { get; set; }
        public string broj__inventure;
        private DataSet DS_Zaposlenik;
        private DataSet DSinventura;
        public frmMenu MainFormMenu { get; set; }

        private void frmSveInventure_Load(object sender, EventArgs e)
        {
            fillCB();
            fillDataGrid();
        }

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

            string sql = "SELECT " + top + " inventura.broj_inventure AS [Broj],inventura.datum AS [Datum],inventura.godina AS [Godina]," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime as [Izradio],skladiste.skladiste AS [Skladište],skladiste.id_skladiste" +
                " FROM inventura" +
                " LEFT JOIN skladiste ON skladiste.id_skladiste = inventura.id_skladiste" +
                " LEFT JOIN zaposlenici ON inventura.id_zaposlenik = zaposlenici.id_zaposlenik ORDER BY CAST(inventura.broj_inventure AS integer)" +
                "" + remote + "";

            DSinventura = classSQL.select(sql, "fakture");
            dgv.DataSource = DSinventura.Tables[0];
            dgv.Columns["id_skladiste"].Visible = false;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void fillCB()
        {
            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzradio.DataSource = DS_Zaposlenik.Tables[0];
            cbIzradio.DisplayMember = "IME";
            cbIzradio.ValueMember = "id_zaposlenik";
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w + 7, h + 7);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w - 7, h - 7);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 1000);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string Broj = "";
            string Partner = "";
            string DateStart = "";
            string DateEnd = "";
            string VD = "";
            string Valuta = "";
            string Izradio = "";
            string SifraArtikla = "";

            if (chbBroj.Checked)
            {
                Broj = "inventura.broj_inventure='" + txtBroj.Text + "' AND ";
            }
            if (chbOD.Checked)
            {
                DateStart = "inventura.datum >='" + dtpOD.Value.ToString("dd.MM.yyyy") + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "inventura.datum <='" + dtpDO.Value.ToString("dd.MM.yyyy") + "' AND ";
            }
            if (chbIzradio.Checked)
            {
                Izradio = "inventura.id_zaposlenik='" + cbIzradio.SelectedValue + "' AND ";
            }

            string filter = Broj + Partner + VD + DateStart + DateEnd + Valuta + SifraArtikla + Izradio;

            if (filter.Length != 0)
            {
                filter = " WHERE " + filter;
                filter = filter.Remove(filter.Length - 4, 4);
            }

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

            string sql = "SELECT " + top + " inventura.broj_inventure AS Broj,inventura.datum AS [Datum], inventura.godina AS [Godina]," +
            "zaposlenici.ime + ' ' + zaposlenici.prezime as [Izradio], skladiste.skladiste AS [Skladište],skladiste.id_skladiste" +
            " FROM inventura" +
            " LEFT JOIN zaposlenici ON inventura.id_zaposlenik = zaposlenici.id_zaposlenik" +
            " LEFT JOIN skladiste ON skladiste.id_skladiste = inventura.id_skladiste" +
            filter + " ORDER BY CAST(inventura.broj_inventure AS integer)" + remote;

            DSinventura = classSQL.select(sql, "fakture");
            dgv.DataSource = DSinventura.Tables[0];
            dgv.Columns["id_skladiste"].Visible = false;
        }

        private void SetDecimalInDgv(DataGridView dg, string column, string column1, string column2)
        {
            for (int i = 0; i < dg.RowCount; i++)
            {
                try
                {
                    if (column != "NE")
                    {
                        dg.Rows[i].Cells[column].Value = Convert.ToDouble(dg.Rows[i].Cells[column].FormattedValue.ToString()).ToString("#0.00");
                    }

                    if (column1 != "NE")
                    {
                        dg.Rows[i].Cells[column1].Value = Convert.ToDouble(dg.Rows[i].Cells[column1].FormattedValue.ToString()).ToString("#0.00");
                    }

                    if (column2 != "NE")
                    {
                        dg.Rows[i].Cells[column2].Value = Convert.ToDouble(dg.Rows[i].Cells[column2].FormattedValue.ToString()).ToString("#0.00");
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        //private void PaintRows(DataGridView dg)
        //{
        //    int br = 0;
        //    for (int i = 0; i < dg.Rows.Count; i++)
        //    {
        //        if (br == 0)
        //        {
        //            dg.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
        //            br++;
        //        }
        //        else
        //        {
        //            dg.Rows[i].DefaultCellStyle.BackColor = Color.White;
        //            br = 0;
        //        }

        //    }
        //    DataGridViewRow row = dg.RowTemplate;
        //    row.Height = 22;
        //}

        private void btnUrediSifru_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            if (dgv.RowCount > 0)
            {
                string broj = dgv.CurrentRow.Cells["Broj"].Value.ToString();
                if (this.MdiParent != null)
                {
                    var mdiForm = this.MdiParent;
                    mdiForm.IsMdiContainer = true;
                    var childForm = new frmUnosInventura();
                    childForm.broj_inventure_edit = broj;
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                    this.Close();
                }
                else
                {
                    MainForm.broj_inventure_edit = broj;
                    MainForm.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Niste odabrali niti jednu stavku.", "Greška");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            printaj();
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            printaj();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount < 0)
                return;

            printaj();
        }

        private void printaj()
        {
            Report.Inventura.frmInventura aa = new Report.Inventura.frmInventura();
            aa.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
            aa.skladiste = dgv.CurrentRow.Cells["id_skladiste"].FormattedValue.ToString();
            aa.datum = Convert.ToDateTime(dgv.CurrentRow.Cells["Datum"].Value);
            aa.samo_razlike = null;
            aa.ShowDialog();
        }

        private void btnRazlikeIspis_Click(object sender, EventArgs e)
        {
            Report.Inventura.frmInventura aa = new Report.Inventura.frmInventura();
            aa.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
            aa.skladiste = dgv.CurrentRow.Cells["id_skladiste"].FormattedValue.ToString();
            aa.datum = Convert.ToDateTime(dgv.CurrentRow.Cells["Datum"].Value);
            aa.samo_razlike = "DA";
            aa.ShowDialog();
        }

        private void btnDetaljno_Click(object sender, EventArgs e)
        {
            Report.Inventura.frmInventura aa = new Report.Inventura.frmInventura();
            aa.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
            aa.skladiste = dgv.CurrentRow.Cells["id_skladiste"].FormattedValue.ToString();
            aa.datum = Convert.ToDateTime(dgv.CurrentRow.Cells["Datum"].Value);
            aa.samo_razlike = "NE";
            aa.ShowDialog();
        }

        private void btnVisak_Click(object sender, EventArgs e)
        {
            Report.Inventura.VisakManjak.frmInventuraVisakManjak inv =
                new Report.Inventura.VisakManjak.frmInventuraVisakManjak(true,
                    dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString(),
                    dgv.CurrentRow.Cells["id_skladiste"].FormattedValue.ToString(),
                    DateTime.Now.Year.ToString());

            inv.ShowDialog();
        }

        private void btnManjak_Click(object sender, EventArgs e)
        {
            Report.Inventura.VisakManjak.frmInventuraVisakManjak inv =
                new Report.Inventura.VisakManjak.frmInventuraVisakManjak(false,
                    dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString(),
                    dgv.CurrentRow.Cells["id_skladiste"].FormattedValue.ToString(),
                    DateTime.Now.Year.ToString());

            inv.ShowDialog();
        }
    }
}