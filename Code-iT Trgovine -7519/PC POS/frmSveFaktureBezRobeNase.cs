using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmSveFaktureBezRobeNase : Form
    {
        public frmFakturaBezRobeNasa MainForm { get; set; }
        public string sifra_fakture;

        public frmSveFaktureBezRobeNase()
        {
            InitializeComponent();
        }

        private DataSet DSfakture = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DS_Zaposlenik = new DataSet();
        private DataTable DTpostavke = new DataTable();
        private string oib_tvrtke = "";

        //string[] arrOib = new string[] { "19058708438" };
        private string[] arrOib = new string[] { "19058708Pero" };

        public frmMenu MainFormMenu { get; set; }

        private void frmSveFakture_Load(object sender, EventArgs e)
        {
            oib_tvrtke = classSQL.select_settings("select top (1) oib from podaci_tvrtka;", "podaci_tvrtka").Tables[0].Rows[0][0].ToString();
            DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            if (MainFormMenu == null)
            {
                int heigt = SystemInformation.VirtualScreen.Height;
                this.Height = heigt - 60;
                this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 505, 5);
            }
            else
            {
                int heigt = SystemInformation.VirtualScreen.Height;
                this.Height = heigt - 140;
                this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 505, 5);
            }

            fillCB();
            fillDataGrid();

            try
            {
                if (dgv.Rows.Count >= 1)
                {
                    string br = dgv.CurrentRow.Cells["broj"].FormattedValue.ToString();

                    fillDataGrid_stavke(br, dgv.CurrentRow.Cells["id_ducan"].FormattedValue.ToString(), dgv.CurrentRow.Cells["id_kasa"].FormattedValue.ToString());
                }
            }
            catch { }

            //DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

            //if (DTpostavke.Rows[0]["on_off_postotak"].ToString() == "NE")
            //{
            //    txtIspisBona.Visible = false;
            //}

            //this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 500);
            c.FillRectangle(bG, 0, 0, Width, Height);
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

        private void fillDataGrid()
        {
            dgv.Rows.Clear();

            string top = "";
            string remote = "";
            if (classSQL.remoteConnectionString != "")
            {
                remote = " LIMIT 3000";
            }
            else
            {
                top = " TOP(3000) ";
            }

            //fakture.date DESC

            string sql = "SELECT " + top + " fakture.broj_fakture AS [broj],id_ducan,id_kasa,fakture.id_vd AS [VD],fakture.broj_ispisa, fakture.date AS [Datum]," +
                " fakture.datum_valute as [Datum valute],valute.ime_valute as [Ime Valute],id_nacin_placanja" +
                ", partners.id_partner as sifra_partnera, partners.ime_tvrtke AS [Partner],fakture.ukupno as [Ukupno],fakture.storno as [Storno], partners.id_partner," +
                " (SELECT SUM(uplaceno) FROM salda_konti WHERE salda_konti.broj_dokumenta=fakture.broj_fakture AND salda_konti.id_ducan=fakture.id_ducan AND salda_konti.id_kasa=fakture.id_kasa AND salda_konti.godina=CAST(fakture.godina_fakture AS INT)) as salda_konti_uplaceno" +
                " FROM fakture INNER JOIN valute ON fakture.id_valuta = valute.id_valuta " +
                " LEFT JOIN partners ON fakture.id_odrediste = partners.id_partner ORDER BY CAST(fakture.broj_fakture AS integer) DESC" +
                "" + remote + "";

            sql = "SELECT " + top + " fakture.broj_fakture AS [broj],id_ducan,id_kasa,fakture.id_vd AS [VD],fakture.broj_ispisa, fakture.date AS [Datum]," +
                " fakture.datum_valute as [Datum valute],valute.ime_valute as [Ime Valute],id_nacin_placanja" +
                ", partners.id_partner as sifra_partnera, partners.ime_tvrtke AS [Partner],fakture.ukupno as [Ukupno],fakture.storno as [Storno], partners.id_partner," +
                " (SELECT SUM(uplaceno) FROM salda_konti WHERE salda_konti.broj_dokumenta=fakture.broj_fakture AND salda_konti.id_ducan=fakture.id_ducan AND salda_konti.id_kasa=fakture.id_kasa AND salda_konti.godina=CAST(fakture.godina_fakture AS INT)) as salda_konti_uplaceno" +
                " FROM fakture INNER JOIN valute ON fakture.id_valuta = valute.id_valuta " +
                " LEFT JOIN partners ON fakture.id_odrediste = partners.id_partner ORDER BY fakture.date DESC" +
                "" + remote + "";

            DSfakture = classSQL.select(sql, "fakture");

            //string[] arrOib = new string[] { "19058708438" };

            foreach (DataRow r in DSfakture.Tables[0].Rows)
            {
                string StatusSalda = "";
                decimal placeno_salda_konti, ukupno_faktura;
                decimal.TryParse(r["salda_konti_uplaceno"].ToString(), out placeno_salda_konti);
                decimal.TryParse(r["Ukupno"].ToString(), out ukupno_faktura);

                if (r["id_nacin_placanja"].ToString() == "1")
                {
                    StatusSalda = "Plaćeno";
                }
                else
                {
                    if (placeno_salda_konti == 0)
                    {
                        StatusSalda = "Nenaplaćeno";
                    }
                    else if (placeno_salda_konti > 0 && placeno_salda_konti < ukupno_faktura)
                    {
                        StatusSalda = "Plaćeno dijelomočno";
                    }
                    else if (placeno_salda_konti >= ukupno_faktura)
                    {
                        StatusSalda = "Plaćeno";
                    }
                }

                DateTime dt_;
                int br_;

                DateTime.TryParse(r["Datum"].ToString(), out dt_);
                int.TryParse(r["broj"].ToString(), out br_);

                dgv.Rows.Add(br_,
                    arrOib.Contains(oib_tvrtke) ? dt_.ToShortDateString() : dt_.ToString(),
                    r["Ime Valute"].ToString(),
                    r["sifra_partnera"].ToString(),
                    r["Partner"].ToString(),
                    r["broj_ispisa"].ToString(),
                    r["Ukupno"].ToString(),
                    r["Storno"].ToString(),
                    r["id_ducan"].ToString(),
                    StatusSalda,
                    r["id_partner"].ToString(),
                    r["id_kasa"].ToString()
                    );
            }

            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Format = "N2";
            dgv.Columns["Ukupno"].DefaultCellStyle = style;
            dgv.Columns["Ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns["Ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            if (arrOib.Contains(oib_tvrtke))
            {
                dgv.Columns["id_ducan"].Visible = false;
                dgv.Columns["sifra_partnera"].Visible = true;
            }
            else
            {
                dgv.Columns["id_ducan"].Visible = true;
                dgv.Columns["sifra_partnera"].Visible = false;
            }
        }

        private void fillDataGrid_stavke(string broj, string _id_ducan, string _id_kasa)
        {
            try
            {
                if (broj == null || broj == "")
                {
                    return;
                }

                dataGridView1.Visible = true;
                string sql = "SELECT faktura_stavke.broj_fakture AS [Broj],faktura_stavke.sifra as [Šifra] " +
                    " ,faktura_stavke.vpc as [VPC], faktura_stavke.ukupno_mpc as [Ukupno MPC], faktura_stavke.nbc as [NBC], " +
                    " faktura_stavke.porez as [PDV], faktura_stavke.kolicina as [Količina], faktura_stavke.rabat as [Rabat], " +
                    " skladiste.skladiste as [Skladište] " +
                    " FROM faktura_stavke " +
                    " LEFT JOIN skladiste ON faktura_stavke.id_skladiste = skladiste.id_skladiste" +
                    " WHERE faktura_stavke.broj_fakture = '" + broj + "'  AND id_ducan='" + _id_ducan + "' AND id_kasa='" + _id_kasa + "'" +
                    " ORDER BY faktura_stavke.broj_fakture DESC";

                DSfakture = classSQL.select(sql, "fakture");
                dataGridView1.DataSource = DSfakture.Tables[0];
            }
            catch { }
        }

        private void btnUrediSifru_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                string broj = dgv.CurrentRow.Cells["broj"].Value.ToString();
                string id_ducan = dgv.CurrentRow.Cells["id_ducan"].Value.ToString();
                string id_kasa = dgv.CurrentRow.Cells["id_kasa"].Value.ToString();

                string sql_fis = "SELECT broj_fakture,zki FROM fakture WHERE broj_fakture = '" + broj + "'";
                DataTable DTprovjfis = classSQL.select(sql_fis, "fakture").Tables[0];

                if (DTprovjfis.Rows[0]["zki"].ToString().Length > 1)
                {
                    MessageBox.Show("Nije moguće mijenjati ovu fakturu! Fiskalizirana!");
                    return;
                }

                if (this.MdiParent != null)
                {
                    var mdiForm = this.MdiParent;
                    mdiForm.IsMdiContainer = true;
                    frmFaktura childForm = new frmFaktura();
                    childForm.MainForm = MainFormMenu;
                    childForm.broj_fakture_edit = broj;
                    childForm.id_ducan = id_ducan;
                    childForm.id_kasa = id_kasa;
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                }
                else
                {
                    MainForm.broj_fakture_edit = broj;
                    MainForm.id_ducan = id_ducan;
                    MainForm.id_kasa = id_kasa;
                    MainForm.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Niste odabrali niti jednu stavku.", "Greška");
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
            string Napomena = "";
            string SifraArtikla = "";
            string Poslovnica = "";

            if (chbBroj.Checked)
            {
                Broj = "fakture.broj_fakture='" + txtBroj.Text + "' AND ";
            }

            if (txtPartner.Text.Trim() != "")
            {
                string Str = txtPartner.Text.Trim().ToLower();
                if (Properties.Settings.Default.id_partner != "")
                    Str = Properties.Settings.Default.id_partner;
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (isNum)
                {
                    Partner = "fakture.id_fakturirati='" + Str + "' AND ";
                }
                else
                {
                    //Properties.Settings.Default.id_partner

                    DataTable DSpar = classSQL.select("SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) = '" + Str + "'", "partners").Tables[0];
                    if (DSpar.Rows.Count > 0)
                    {
                        Partner = "fakture.id_fakturirati='" + DSpar.Rows[0][0].ToString() + "' AND ";
                    }
                    else
                    {
                        DSpar = classSQL.select("SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) like '%" +
                            Str + "%'", "partners").Tables[0];
                        if (DSpar.Rows.Count > 0)
                        {
                            Partner = "fakture.id_fakturirati='" + DSpar.Rows[0][0].ToString() + "' AND ";
                        }
                        else
                        {
                            Partner = "";
                        }
                    }
                }
            }

            if (chbPoslovnica.Checked)
            {
                Poslovnica = "fakture.id_ducan >='" + cbPoslovnica.SelectedValue.ToString() + "' AND ";
            }

            if (chbOD.Checked)
            {
                DateStart = "fakture.date >='" + dtpOD.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "fakture.date <='" + dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbValuta.Checked)
            {
                Valuta = "fakture.id_valuta='" + cbValuta.SelectedValue.ToString() + "' AND ";
            }

            if (chbIzradio.Checked)
            {
                Izradio = "fakture.id_zaposlenik_izradio='" + cbIzradio.SelectedValue + "' AND ";
            }
            if (chbNapomenackb.Checked)
            {
                Napomena = "fakture.napomena ~* '" + textNapomena.Text + "' AND ";
            }

            string filter = Broj + Partner + VD + DateStart + Poslovnica + DateEnd + Valuta + SifraArtikla + Izradio + Napomena;

            if (filter.Length != 0)
            {
                filter = " WHERE " + filter;
                filter = filter.Remove(filter.Length - 4, 4);
            }

            string top = "";
            string remote = "";
            if (classSQL.remoteConnectionString != "")
            {
                remote = " LIMIT 3000";
            }
            else
            {
                top = " TOP(3000) ";
            }

            //string sql = "SELECT DISTINCT " + top + " faktura_stavke.broj_fakture AS [broj],fakture.id_vd AS [VD]," +
            //        "fakture.date AS Datum,fakture.datum_valute as [Datum valute],valute.ime_valute as [Ime Valute]" +
            //        ",partners.ime_tvrtke AS [Partner],fakture.ukupno as [Ukupno],fakture.storno as [Storno]  " +
            //        " FROM fakture INNER JOIN valute ON fakture.id_valuta = valute.id_valuta " +
            //        " LEFT JOIN partners ON fakture.id_fakturirati = partners.id_partner"+
            //        " INNER JOIN faktura_stavke ON faktura_stavke.broj_fakture = fakture.broj_fakture" + filter +
            //        " ORDER BY fakture.date DESC" + remote;

            string sql = "SELECT DISTINCT " + top + " fakture.broj_fakture AS [broj],fakture.id_vd AS [VD],fakture.broj_ispisa, fakture.date AS [Datum],id_nacin_placanja," +
                " fakture.datum_valute as [Datum valute],valute.ime_valute as [Ime Valute]" +
                ",partners.ime_tvrtke AS [Partner],fakture.ukupno as [Ukupno],fakture.storno as [Storno],partners.id_partner, fakture.id_ducan,fakture.id_kasa, " +
                " (SELECT SUM(uplaceno) FROM salda_konti WHERE salda_konti.broj_dokumenta=fakture.broj_fakture AND salda_konti.id_ducan=fakture.id_ducan AND salda_konti.id_kasa=fakture.id_kasa AND salda_konti.godina=CAST(fakture.godina_fakture AS INT)) as salda_konti_uplaceno,fakture.id_fakturirati AS [Partner]" +
                " FROM fakture INNER JOIN valute ON fakture.id_valuta = valute.id_valuta " +
                " LEFT JOIN faktura_stavke ON faktura_stavke.broj_fakture = fakture.broj_fakture" +
                " AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa" +
                " LEFT JOIN partners ON fakture.id_odrediste = partners.id_partner " + filter + " ORDER BY fakture.date DESC" +
                "" + remote + "";

            //partners.id_partner as sifra_partnera

            DSfakture = classSQL.select(sql, "fakture");
            dgv.Rows.Clear();

            foreach (DataRow r in DSfakture.Tables[0].Rows)
            {
                string StatusSalda = "";
                decimal placeno_salda_konti, ukupno_faktura;
                decimal.TryParse(r["salda_konti_uplaceno"].ToString(), out placeno_salda_konti);
                decimal.TryParse(r["Ukupno"].ToString(), out ukupno_faktura);

                if (r["id_nacin_placanja"].ToString() == "1")
                {
                    StatusSalda = "Plaćeno";
                }
                else
                {
                    if (placeno_salda_konti == 0)
                    {
                        StatusSalda = "Nenaplaćeno";
                    }
                    else if (placeno_salda_konti > 0 && placeno_salda_konti < ukupno_faktura)
                    {
                        StatusSalda = "Plaćeno dijelomično";
                    }
                    else if (placeno_salda_konti >= ukupno_faktura)
                    {
                        StatusSalda = "Plaćeno";
                    }
                }

                DateTime dt_;
                int br_;

                DateTime.TryParse(r["Datum"].ToString(), out dt_);
                int.TryParse(r["broj"].ToString(), out br_);

                //dgv.Rows.Add(br_,
                //    dt_,
                //    r["Ime Valute"].ToString(),
                //    r["Partner"].ToString(),
                //    r["broj_ispisa"].ToString(),
                //    r["Ukupno"].ToString(),
                //    r["Storno"].ToString(),
                //    r["id_ducan"].ToString(),
                //    StatusSalda,
                //    r["id_partner"].ToString(),
                //    r["id_kasa"].ToString()
                //    );

                dgv.Rows.Add(br_,
                arrOib.Contains(oib_tvrtke) ? dt_.ToShortDateString() : dt_.ToString(),
                r["Ime Valute"].ToString(),
                r["id_partner"].ToString(),
                r["Partner"].ToString(),
                r["broj_ispisa"].ToString(),
                r["Ukupno"].ToString(),
                r["Storno"].ToString(),
                r["id_ducan"].ToString(),
                StatusSalda,
                r["id_partner"].ToString(),
                r["id_kasa"].ToString()
                );
            }

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Format = "N2";
            dgv.Columns["Ukupno"].DefaultCellStyle = style;
            dgv.Columns["Ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns["Ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");
        }

        private void fillCB()
        {
            //fill valuta
            DSValuta = classSQL.select("SELECT * FROM valute", "valute");
            cbValuta.DataSource = DSValuta.Tables[0];
            cbValuta.DisplayMember = "ime_valute";
            cbValuta.ValueMember = "id_valuta";
            cbValuta.SelectedValue = 5;

            DataTable DTducan = classSQL.select("SELECT * FROM ducan", "ducan").Tables[0];
            cbPoslovnica.DataSource = DTducan;
            cbPoslovnica.DisplayMember = "ime_ducana";
            cbPoslovnica.ValueMember = "id_ducan";

            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzradio.DataSource = DS_Zaposlenik.Tables[0];
            cbIzradio.DisplayMember = "IME";
            cbIzradio.ValueMember = "id_zaposlenik";
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            printaj();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            printaj();
        }

        private void printaj()
        {
            bool tecaj = true;

            string broj = dgv.CurrentRow.Cells["broj"].FormattedValue.ToString();
            string id_ducan = dgv.CurrentRow.Cells["id_ducan"].FormattedValue.ToString();
            string id_kasa = dgv.CurrentRow.Cells["id_kasa"].FormattedValue.ToString();

            DataTable DTponude = classSQL.select("SELECT ime_valute FROM fakture,valute WHERE broj_fakture = '" + broj + "'" +
                " AND fakture.id_valuta=valute.id_valuta AND fakture.id_ducan='" + id_ducan + "' AND fakture.id_kasa='" + id_kasa + "'", "fakture").Tables[0];

            if (DTponude.Rows.Count > 0)
            {
                string ime_valute = DTponude.Rows[0]["ime_valute"].ToString();
                if (ValutaKuna(ime_valute))
                {
                    if (MessageBox.Show("Ova faktura izrađena je u stranoj valuti.\nŽelite li fakturu ispisati u valuti?", "Valuta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        tecaj = true;
                    }
                    else
                    {
                        tecaj = false;
                    }
                }
                else
                {
                    tecaj = false;
                }
            }
            else
            {
                tecaj = false;
            }

            Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
            //Report.Faktura.repFakturaNovo rfak = new Report.Faktura.repFakturaNovo();

            rfak.dokumenat = "FAK";
            rfak.racunajTecaj = tecaj;
            rfak.poslovnica = id_ducan;
            rfak.naplatni = id_kasa;
            rfak.ImeForme = "Fakture";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["broj"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        private void printaj_engl()
        {
            bool tecaj = true;

            string broj = dgv.CurrentRow.Cells["broj"].FormattedValue.ToString();
            string id_ducan = dgv.CurrentRow.Cells["id_ducan"].FormattedValue.ToString();
            string id_kasa = dgv.CurrentRow.Cells["id_kasa"].FormattedValue.ToString();

            DataTable DTponude = classSQL.select("SELECT ime_valute FROM fakture,valute WHERE broj_fakture = '" + broj + "'" +
                " AND fakture.id_valuta=valute.id_valuta", "fakture").Tables[0];

            if (DTponude.Rows.Count > 0)
            {
                string ime_valute = DTponude.Rows[0]["ime_valute"].ToString();
                if (ValutaKuna(ime_valute))
                {
                    if (MessageBox.Show("Ova faktura izrađena je u stranoj valuti.\nŽelite li ponudu ispisati u valuti?", "Valuta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        tecaj = true;
                    }
                    else
                    {
                        tecaj = false;
                    }
                }
                else
                {
                    tecaj = false;
                }
            }
            else
            {
                tecaj = false;
            }

            Report.Faktura_engl.repFaktura_engl rfak = new Report.Faktura_engl.repFaktura_engl();
            rfak.dokumenat = "FAK";
            rfak.racunajTecaj = tecaj;
            rfak.ImeForme = "Fakture";
            rfak.poslovnica = id_ducan;
            rfak.naplatni = id_kasa;
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["broj"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        private bool ValutaKuna(string valuta)
        {
            string val = valuta.ToLower();

            if (val.Contains("hr"))
                return false;
            else if (val.Contains("hrk"))
                return false;
            else if (val.Contains("hrvatska"))
            {
                return false;
            }
            else if (val.Contains("kun"))
            {
                return false;
            }
            else
                return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSveFakture_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MainFormMenu != null)
            {
            }
        }

        private void txtIspisBona_Click(object sender, EventArgs e)
        {
            frmBarCode bc = new frmBarCode();
            bc.broj_dokumenta = dgv.CurrentRow.Cells["broj"].FormattedValue.ToString();
            bc.ukupno = Convert.ToDouble(dgv.CurrentRow.Cells["Ukupno"].FormattedValue.ToString());
            bc.ShowDialog();
        }

        private void txtPartner_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtPartner.Text == "")
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
                            txtPartner.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                            //txtSifraFakturirati.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                            //txtPartnerNaziv1.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString(); cbVD.Select();
                            //txtSifraFakturirati.Select();
                            System.Windows.Forms.SendKeys.Send("{TAB}");
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtPartner.Select();
                            txtPartner.SelectAll();
                            return;
                        }
                    }
                    else
                    {
                        txtPartner.Select();
                        txtPartner.SelectAll();
                        return;
                    }
                }

                string Str = txtPartner.Text.Trim().ToLower();
                double Num;
                bool isNum = double.TryParse(Str, out Num);

                if (isNum)
                {
                    DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + Str + "'", "partners").Tables[0];
                    if (DSpar.Rows.Count > 0)
                    {
                        Properties.Settings.Default.id_partner = Str;
                        txtPartner.Text = DSpar.Rows[0][0].ToString();
                        //txtSifraFakturirati.Text = txtSifraOdrediste.Text;
                        //txtSifraFakturirati_KeyDown(txtSifraOdrediste, e);

                        System.Windows.Forms.SendKeys.Send("{TAB}");
                    }
                    else
                    {
                        MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                        txtPartner.Select();
                        txtPartner.SelectAll();
                    }
                }
                else
                {
                    DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE LOWER(ime_tvrtke) = '" +
                        Str + "'", "partners").Tables[0];
                    if (DSpar.Rows.Count > 0)
                    {
                        txtPartner.Text = DSpar.Rows[0][0].ToString();
                        System.Windows.Forms.SendKeys.Send("{TAB}");
                    }
                    else
                    {
                        DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE LOWER(ime_tvrtke) like '%" +
                            Str + "%'", "partners").Tables[0];
                        if (DSpar.Rows.Count > 0)
                        {
                            txtPartner.Text = DSpar.Rows[0][0].ToString();
                            System.Windows.Forms.SendKeys.Send("{TAB}");
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtPartner.Select();
                            txtPartner.SelectAll();
                        }
                    }
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
                    txtPartner.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    //txtSifraFakturirati.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    //txtPartnerNaziv1.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString(); cbVD.Select();
                    System.Windows.Forms.SendKeys.Send("{TAB}");
                }
                else
                {
                    Properties.Settings.Default.id_partner = "";
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            if (dgv.Columns[e.ColumnIndex].Name == "placeno")
            {
                //}
                //if(e.ColumnIndex==9)
                //{
                decimal u;
                DateTime d;
                decimal.TryParse(dgv.CurrentRow.Cells["Ukupno"].FormattedValue.ToString(), out u);
                DateTime.TryParse(dgv.CurrentRow.Cells["datum"].FormattedValue.ToString(), out d);

                Salda_konti.frmUnosSaldaKonti sk = new Salda_konti.frmUnosSaldaKonti();
                sk._dokumenat = "FAKTURA";
                sk._broj = dgv.CurrentRow.Cells["broj"].FormattedValue.ToString();
                sk._id_ducan = dgv.CurrentRow.Cells["id_ducan"].FormattedValue.ToString();
                sk._id_kasa = dgv.CurrentRow.Cells["id_kasa"].FormattedValue.ToString();
                sk._iznos = u;
                sk._id_partner = dgv.CurrentRow.Cells["dgv_partner"].FormattedValue.ToString();
                sk._id_skladiste = "";
                sk._godina = d.Year.ToString();
                sk.ShowDialog();
                pictureBox1_Click(sender, e);
                //fillDataGrid();
                return;
            }

            try
            {
                if (dgv.Rows.Count >= 1)
                {
                    string br = dgv.CurrentRow.Cells["broj"].FormattedValue.ToString();

                    fillDataGrid_stavke(br, dgv.CurrentRow.Cells["id_ducan"].FormattedValue.ToString(), dgv.CurrentRow.Cells["id_kasa"].FormattedValue.ToString());
                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            printaj_engl();
        }

        private void btnvezinaavans_Click(object sender, EventArgs e)
        {
            string br = "";
            try
            {
                if (dgv.Rows.Count >= 1)
                {
                    br = dgv.CurrentRow.Cells["broj"].FormattedValue.ToString();
                }
            }
            catch { }
            frmPoveznicaNaAvansa pov = new frmPoveznicaNaAvansa();
            pov.broj_fakture = br;
            pov.ShowDialog();
        }
    }
}