using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmSviAvansi : Form
    {
        public frmAvans MainForm { get; set; }
        public string sifra_avansa;

        public frmSviAvansi()
        {
            InitializeComponent();
        }

        public Int32 idpartner = 0;

        private DataSet DSavansi = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DS_Zaposlenik = new DataSet();
        private DataTable DTpostavke = new DataTable();
        public frmMenu MainFormMenu { get; set; }
        public frmPoveznicaNaAvansa FormPoveznicaAvans { get; set; }

        private void FrmSviAvansi_Load(object sender, EventArgs e)
        {
            kreirajTablice();
            if (FormPoveznicaAvans != null)
            {
                btnSveFakture.Text = "Odaberi";
            }
            this.Paint += new PaintEventHandler(Form1_Paint);

            fillCB();
            fillDataGrid();

            //DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 1000);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            if (FormPoveznicaAvans != null)
            {
                FormPoveznicaAvans.txtBrojAvansa.Text = dgv.CurrentRow.Cells["Broj avansa"].FormattedValue.ToString();
                FormPoveznicaAvans.txtGodinaAvansa.Text = dgv.CurrentRow.Cells["Godina"].FormattedValue.ToString();
                this.Close();
            }
            else
            {
                printaj();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DateTime dOtp = Convert.ToDateTime(dtpOD.Value);
            string dtOd = dOtp.Month + "." + dOtp.Day + "." + dOtp.Year;

            DateTime dNow = Convert.ToDateTime(dtpDO.Value);
            string dtDo = dNow.Month + "." + dNow.Day + "." + dNow.Year;

            string Broj = "";
            string Partner = "";
            string DateStart = "";
            string DateEnd = "";
            string VD = "";
            string Valuta = "";
            string Izradio = "";
            string Kupac = "";
            string SifraArtikla = "";

            if (chbBroj.Checked)
            {
                Broj = "avansi.broj_avansa = '" + txtBroj.Text + "' AND ";
            }
            if (chbVD.Checked)
            {
                VD = "avansi.id_vd = '" + cbVD.SelectedValue.ToString() + "' AND ";
            }
            if (chbOD.Checked)
            {
                DateStart = "avansi.dat_knj >= '" + dtpOD.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "avansi.dat_knj <= '" + dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbValuta.Checked)
            {
                Valuta = "avansi.id_valuta = '" + cbValuta.SelectedValue.ToString() + "' AND ";
            }
            if (chbIzradio.Checked)
            {
                Izradio = "avansi.id_zaposlenik_izradio='" + cbIzradio.SelectedValue + "' AND ";
            }
            if (cbkupac.Checked)
            {
                Kupac = "avansi.id_partner = '" + idpartner + "' AND ";
            }

            string filter = Broj + Partner + VD + DateStart + DateEnd + Valuta + SifraArtikla + Izradio + Kupac;

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

            string sql = "SELECT DISTINCT " + top + " avansi.broj_avansa AS [Broj avansa], avansi.godina_avansa AS [Godina], avansi.id_vd AS [Vrsta], avansi.dat_knj AS [Datum knjiženja], avansi.dat_dok as [Datum], valute.ime_valute as [Valuta]" +
                    ", avansi.ukupno as [Ukupno], avansi.opis as [Opis] " +
                    " FROM avansi INNER JOIN valute ON avansi.id_valuta = valute.id_valuta " +
                    " LEFT JOIN partners ON partners.id_partner = avansi.id_partner " +
                    filter + " ORDER BY avansi.dat_knj DESC" + remote;

            DSavansi = classSQL.select(sql, "avansi");
            dgv.DataSource = DSavansi.Tables[0];

            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");
        }

        private void printaj()
        {
            Report.Avans.repAvans rav = new Report.Avans.repAvans();
            rav.dokumenat = "AVA";
            rav.ImeForme = "Avansi";
            rav.broj_dokumenta = dgv.CurrentRow.Cells["Broj avansa"].FormattedValue.ToString();
            rav.godina = dgv.CurrentRow.Cells["Godina"].FormattedValue.ToString();
            rav.ShowDialog();
        }

        private void fillCB()
        {
            //fill valuta
            DSValuta = classSQL.select("SELECT * FROM valute", "valute");
            cbValuta.DataSource = DSValuta.Tables[0];
            cbValuta.DisplayMember = "ime_valute";
            cbValuta.ValueMember = "id_valuta";
            cbValuta.SelectedValue = 5;

            //fill vrsta dokumenta
            DSvd = classSQL.select("SELECT * FROM avansi_vd ORDER BY id_vd", "avansi_vd");
            cbVD.DataSource = DSvd.Tables[0];
            cbVD.DisplayMember = "vd";
            cbVD.ValueMember = "id_vd";

            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime + ' ' + prezime as IME, id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzradio.DataSource = DS_Zaposlenik.Tables[0];
            cbIzradio.DisplayMember = "IME";
            cbIzradio.ValueMember = "id_zaposlenik";
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

            string sql = "SELECT " + top + " avansi.broj_avansa AS [Broj avansa], avansi.godina_avansa AS [Godina]," +
                "avansi.id_vd AS [Vrsta], avansi.dat_knj AS [Datum knjiženja], partners.ime_tvrtke AS [Kupac]," +
                "avansi.dat_dok as [Datum], valute.ime_valute as [Valuta], avansi.ukupno as [Ukupno], avansi.opis as [Opis] " +
                " FROM avansi INNER JOIN valute ON avansi.id_valuta = valute.id_valuta " +
                " LEFT JOIN partners ON partners.id_partner=avansi.id_partner " +
                " ORDER BY CAST(avansi.godina_avansa AS integer) DESC, CAST(avansi.broj_avansa AS integer) DESC" +
                "" + remote + "";

            DSavansi = classSQL.select(sql, "avansi");
            dgv.DataSource = DSavansi.Tables[0];

            dgv.Columns["Godina"].Visible = false;

            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");

            dgv.Columns["Ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Format = "N2";
            dgv.Columns["Ukupno"].DefaultCellStyle = style;
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            printaj();
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

        private void btnUrediSifru_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            if (dgv.RowCount > 0)
            {
                int avans;
                int godina;

                try
                {
                    avans = Convert.ToInt32(dgv.CurrentRow.Cells["Broj avansa"].Value.ToString());
                    godina = Convert.ToInt16(dgv.CurrentRow.Cells["Godina"].Value.ToString());
                }
                catch
                {
                    MessageBox.Show("Unesi numeričku vrijednost!");
                    return;
                }

                string sql_fis = string.Format("SELECT zki FROM avansi WHERE broj_avansa = {0} and godina_avansa::integer = {1};", avans, godina);
                DataTable DTprovjfis = classSQL.select(sql_fis, "avansi").Tables[0];

                if (DTprovjfis.Rows[0]["zki"].ToString().Length > 1)
                {
                    MessageBox.Show("Nije moguće mijenjati ovaj avans! Fiskaliziran!");
                    return;
                }

                if (this.MdiParent != null)
                {
                    var mdiForm = this.MdiParent;
                    mdiForm.IsMdiContainer = true;
                    frmAvans childForm = new frmAvans();
                    childForm.MainForm = MainFormMenu;
                    childForm.broj_avansa_edit = avans;
                    childForm.godina = godina;
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                }
                else
                {
                    MainForm.broj_avansa_edit = avans;
                    MainForm.godina = godina;
                    MainForm.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Niste odabrali niti jednu stavku.", "Greška");
            }
        }

        private void kreirajTablice()
        {
            DataTable DTremote = classSQL.select("select table_name from information_schema.tables where TABLE_TYPE <> 'VIEW'", "Table").Tables[0];

            if (DTremote.Rows.Count > 0)
            {
                DataRow[] dataROW = DTremote.Select("table_name = 'avansi'");
                if (dataROW.Length == 0)
                {
                    string sql = "CREATE TABLE avansi" +
                                            "(broj_avansa bigint NOT NULL," +
                                            "dat_dok timestamp without time zone," +
                                            "dat_knj timestamp without time zone," +
                                            "id_zaposlenik integer," +
                                            "id_zaposlenik_izradio integer," +
                                            "model character varying(10)," +
                                            "id_nacin_placanja bigint," +
                                            "id_valuta integer," +
                                            "opis text," +
                                            "id_vd character(5)," +
                                            "godina_avansa character(6)," +
                                            "ukupno numeric," +
                                            "nult_stp numeric," +
                                            "neoporezivo numeric," +
                                            "osnovica10 numeric," +
                                            "osnovica_var numeric," +
                                            "porez_var numeric," +
                                            "id_pdv integer," +
                                            "id_partner bigint," +
                                            "ziro bigint," +
                                            "jir character varying(100)," +
                                            "zki character varying(100)," +
                                            "storno character varying(2)," +
                                            "CONSTRAINT broj_avansa PRIMARY KEY (broj_avansa)" +
                                            ")";
                    classSQL.select(sql, "avansi");
                }

                dataROW = DTremote.Select("table_name = 'avansi_vd'");
                if (dataROW.Length == 0)
                {
                    string sql = "CREATE TABLE avansi_vd" +
                        "id_vd bigint NOT NULL," +
                        "vd character varying(30)," +
                        "grupa character varying(5)," +
                        "CONSTRAINT primary_key_id_vd PRIMARY KEY (id_vd )" +
                        ")";
                    classSQL.select(sql, "avansi_vd");
                    classSQL.insert("INSERT INTO avansi_vd (id_vd,vd,grupa) VALUES ('Predujam','IP')");
                    classSQL.insert("INSERT INTO avansi_vd (id_vd,vd,grupa) VALUES ('Storno primljenog predujma','PRS')");
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    //txtSifraOdrediste.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    //txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    idpartner = Convert.ToInt32(partner.Tables[0].Rows[0]["id_partner"].ToString());
                    txtkupac.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString(); cbVD.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
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

        private void frmSviAvansi_SizeChanged(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Form1_Paint);
        }
    }
}