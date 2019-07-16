using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmSveMeduskladisnice : Form
    {
        public frmMeduskladisnica MainForm { get; set; }

        private DataSet DSmo = new DataSet();

        public frmSveMeduskladisnice()
        {
            InitializeComponent();
        }

        public frmMenu MainFormMenu { get; set; }

        private void sveMeduskladisnice_Load(object sender, EventArgs e)
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

            fillCB();
            fillDataGrid();

            try
            {
                if (dgv.Rows.Count >= 1)
                {
                    string br = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
                    string skl = dgv.CurrentRow.Cells["Skladište"].FormattedValue.ToString();

                    fillDataGrid_stavke(br, skl);
                }
            }
            catch { }

            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void fillDataGrid()
        {
            string sql = "SELECT meduskladisnica.broj AS [Broj],meduskladisnica.godina AS [Godina]," +
                "meduskladisnica.datum AS [Datum],skladiste.skladiste AS [Iz skladišta],T2.skladiste AS [U skladište]," +
                "zaposlenici.ime+' '+ zaposlenici.prezime AS [Izradio],meduskladisnica.id_skladiste_od AS [Skladište]," +
                "meduskladisnica.id_skladiste_do,meduskladisnica.id_skladiste_od" +
                " FROM meduskladisnica" +
                " LEFT JOIN skladiste ON skladiste.id_skladiste = meduskladisnica.id_skladiste_od " +
                " LEFT JOIN skladiste T2 ON T2.id_skladiste = meduskladisnica.id_skladiste_do " +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik = meduskladisnica.id_izradio " +
                " ORDER BY CAST(meduskladisnica.broj AS integer) DESC";

            DSmo = classSQL.select(sql, "meduskladisnica");
            dgv.DataSource = DSmo.Tables[0];

            dgv.Columns["id_skladiste_od"].Visible = false;
            dgv.Columns["id_skladiste_do"].Visible = false;

            //dgv.Columns["Ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            //DataGridViewCellStyle style = new DataGridViewCellStyle();
            //style.Format = "N2";
            //dgv.Columns["Ukupno"].DefaultCellStyle = style;
        }

        private void fillDataGrid_stavke(string broj, string skladiste)
        {
            try
            {
                if (broj == null || broj == "")
                {
                    return;
                }

                dataGridView1.Visible = true;

                string sql_liste = "SELECT " +
                        " meduskladisnica_stavke.sifra," +
                        " meduskladisnica_stavke.kolicina," +
                        " meduskladisnica_stavke.vpc," +
                        " meduskladisnica_stavke.mpc," +
                        " meduskladisnica_stavke.pdv," +
                        " ROUND(CAST(meduskladisnica_stavke.mpc AS numeric) * CAST(REPLACE(meduskladisnica_stavke.kolicina,',','.') AS numeric),2) AS [Ukupno za stavku] " +
                        " FROM meduskladisnica_stavke" +
                        " WHERE broj='" + broj + "' AND iz_skladista='" + skladiste + "'";

                DSmo = classSQL.select(sql_liste, "meduskladisnica_stavke");
                dataGridView1.DataSource = DSmo.Tables[0];
            }
            catch { }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 500);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private DataSet DS_Zaposlenik = new DataSet();
        private DataSet DS_SkladisteIZ = new DataSet();
        private DataSet DS_SkladisteUU = new DataSet();

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
                Broj = "meduskladisnica.broj='" + txtBroj.Text + "' AND ";
            }
            if (chbOD.Checked)
            {
                DateStart = "meduskladisnica.datum >='" + dtpOD.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "meduskladisnica.datum <='" + dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbArtikl.Checked)
            {
                SifraArtikla = "meduskladisnica_stavke.sifra ='" + cbArtikl.Text + "' AND ";
            }
            if (chbIzradio.Checked)
            {
                Izradio = "meduskladisnica.id_izradio='" + cbIzradio.SelectedValue + "' AND ";
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

            string sql = "SELECT DISTINCT " + top + " meduskladisnica.broj AS [Broj],meduskladisnica.godina AS [Godina]," +
                "meduskladisnica.datum AS [Datum],skladiste.skladiste AS [Iz skladišta],T2.skladiste AS [U skladište]," +
                "zaposlenici.ime+' '+ zaposlenici.prezime AS [Izradio],meduskladisnica.id_skladiste_od AS [Skladište]," +
                "meduskladisnica.id_skladiste_do,meduskladisnica.id_skladiste_od" +
                " FROM meduskladisnica" +
                " LEFT JOIN meduskladisnica_stavke ON meduskladisnica_stavke.broj = meduskladisnica.broj AND meduskladisnica_stavke.iz_skladista = meduskladisnica.id_skladiste_od" +
                " LEFT JOIN skladiste ON skladiste.id_skladiste = meduskladisnica.id_skladiste_od " +
                " LEFT JOIN skladiste T2 ON T2.id_skladiste = meduskladisnica.id_skladiste_do " +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik = meduskladisnica.id_izradio " + filter +
                " ORDER BY meduskladisnica.datum DESC" + remote;

            DSmo = classSQL.select(sql, "meduskladisnica");
            dgv.DataSource = DSmo.Tables[0];

            dgv.Columns["id_skladiste_od"].Visible = false;
            dgv.Columns["id_skladiste_do"].Visible = false;
        }

        private void btnUrediSifru_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }

            if (dgv.RowCount > 0)
            {
                string broj = dgv.CurrentRow.Cells["Broj"].Value.ToString();
                string godina = dgv.CurrentRow.Cells["Godina"].Value.ToString();
                string skladiste = dgv.CurrentRow.Cells["id_skladiste_od"].Value.ToString();

                if (this.MdiParent != null)
                {
                    var mdiForm = this.MdiParent;
                    mdiForm.IsMdiContainer = true;
                    var childForm = new frmMeduskladisnica();
                    childForm.broj_ms_edit = broj;
                    childForm.godina_edit = godina;
                    childForm.skladiste_edit = skladiste;
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                }
                else
                {
                    MainForm.broj_ms_edit = broj;
                    MainForm.godina_edit = godina;
                    MainForm.skladiste_edit = skladiste;
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
            if (Class.Postavke.proizvodnjaMeduskladisnicaPC)
            {
                Report.Liste.frmListeMeduskladisnicaPC rfak = new Report.Liste.frmListeMeduskladisnicaPC();
                rfak.dokument = "meduskladisnica";
                rfak.ImeForme = "Međuskladišnica";
                rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
                rfak.godina = dgv.CurrentRow.Cells["Godina"].Value.ToString();
                rfak.skladiste = dgv.CurrentRow.Cells["id_skladiste_od"].FormattedValue.ToString();
                rfak.ShowDialog();
            }
            else
            {
                Report.Liste.frmListe rfak = new Report.Liste.frmListe();
                rfak.dokument = "meduskladisnica";
                rfak.ImeForme = "Međuskladišnica";
                rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
                rfak.godina = dgv.CurrentRow.Cells["Godina"].Value.ToString();
                rfak.skladiste = dgv.CurrentRow.Cells["id_skladiste_od"].FormattedValue.ToString();
                rfak.ShowDialog();
            }
        }

        private void dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }

            if (Class.Postavke.proizvodnjaMeduskladisnicaPC)
            {
                Report.Liste.frmListeMeduskladisnicaPC rfak = new Report.Liste.frmListeMeduskladisnicaPC();
                rfak.dokument = "meduskladisnica";
                rfak.ImeForme = "Međuskladišnica";
                rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
                rfak.godina = dgv.CurrentRow.Cells["Godina"].Value.ToString();
                rfak.skladiste = dgv.CurrentRow.Cells["id_skladiste_od"].FormattedValue.ToString();
                rfak.ShowDialog();
            }
            else
            {
                Report.Liste.frmListe rfak = new Report.Liste.frmListe();
                rfak.dokument = "meduskladisnica";
                rfak.ImeForme = "Međuskladišnica";
                rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
                rfak.godina = dgv.CurrentRow.Cells["Godina"].Value.ToString();
                rfak.skladiste = dgv.CurrentRow.Cells["id_skladiste_od"].FormattedValue.ToString();
                rfak.ShowDialog();
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv.Rows.Count >= 1)
                {
                    string br = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
                    string skl = dgv.CurrentRow.Cells["Skladište"].FormattedValue.ToString();

                    fillDataGrid_stavke(br, skl);
                }
            }
            catch { }
        }
    }
}