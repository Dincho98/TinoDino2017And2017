using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.FakturaVan
{
    public partial class frmSveFaktureVan : Form
    {
        public frmFakturaVan MainForm { get; set; }
        public string sifra_fakture;

        public frmSveFaktureVan()
        {
            InitializeComponent();
        }

        private DataSet DSfakture = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DS_Zaposlenik = new DataSet();

        //DataTable DTpostavke = new DataTable();
        public frmMenu MainFormMenu { get; set; }

        private void frmSveFakture_Load(object sender, EventArgs e)
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
                    string br = dgv.CurrentRow.Cells["Broj fakture_van"].FormattedValue.ToString();

                    fillDataGrid_stavke(br);
                }
            }
            catch { }

            //DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

            //if (DTpostavke.Rows[0]["on_off_postotak"].ToString() == "NE")
            if (!Class.Postavke.on_of_postotak)
            {
                txtIspisBona.Visible = false;
            }

            this.Paint += new PaintEventHandler(Form1_Paint);
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

            string sql = "SELECT " + top + " fakture_van.broj_fakture AS [Broj fakture_van],fakture_van.id_vd AS [VD], fakture_van.date AS [Datum]," +
                "fakture_van.datum_valute as [Datum valute],valute.ime_valute as [Ime Valute]" +
                ",partners.ime_tvrtke AS [Partner],fakture_van.ukupno as [Ukupno],fakture_van.storno as [Storno]  " +
                " FROM fakture_van INNER JOIN valute ON fakture_van.id_valuta = valute.id_valuta " +
                " LEFT JOIN partners ON fakture_van.id_fakturirati = partners.id_partner ORDER BY CAST(fakture_van.broj_fakture AS integer) DESC" +
                "" + remote + "";

            DSfakture = classSQL.select(sql, "fakture_van");
            dgv.DataSource = DSfakture.Tables[0];

            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Format = "N2";
            dgv.Columns["Ukupno"].DefaultCellStyle = style;
            dgv.Columns["Ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns["Ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void fillDataGrid_stavke(string broj)
        {
            try
            {
                if (broj == null || broj == "")
                {
                    return;
                }

                dataGridView1.Visible = true;
                string sql = "SELECT faktura_van_stavke.broj_fakture AS [Broj],faktura_van_stavke.sifra as [Šifra] " +
                    " ,faktura_van_stavke.vpc as [VPC], faktura_van_stavke.ukupno_mpc as [Ukupno MPC], faktura_van_stavke.nbc as [NBC], " +
                    " faktura_van_stavke.porez as [PDV], faktura_van_stavke.kolicina as [Količina], faktura_van_stavke.rabat as [Rabat], " +
                    " faktura_van_stavke.id_skladiste as [Skladište], faktura_van_stavke.porez_potrosnja as [Porez na potrošnju], " +
                    " faktura_van_stavke.povratna_naknada as [Povratna naknada] " +
                    " FROM faktura_van_stavke " +
                    " WHERE faktura_van_stavke.broj_fakture = '" + broj + "' " +
                    " ORDER BY faktura_van_stavke.broj_fakture DESC";

                DSfakture = classSQL.select(sql, "fakture_van");
                dataGridView1.DataSource = DSfakture.Tables[0];
            }
            catch { }
        }

        private void btnUrediSifru_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            if (dgv.RowCount > 0)
            {
                string broj = dgv.CurrentRow.Cells["Broj fakture_van"].Value.ToString();

                string sql_fis = "SELECT broj_fakture,zki FROM fakture_van WHERE broj_fakture = '" + broj + "'";
                DataTable DTprovjfis = classSQL.select(sql_fis, "fakture_van").Tables[0];

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
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                }
                else
                {
                    if (MainForm == null)
                    {
                        MainForm = new frmFakturaVan();
                    }

                    MainForm.broj_fakture_edit = broj;
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

            if (chbBroj.Checked)
            {
                Broj = "fakture_van.broj_fakture='" + txtBroj.Text + "' AND ";
            }
            //if (chbSifra.Checked)
            //{
            //    Partner = "fakture_van.id_fakturirati='" + txtPartner.Text + "' AND ";
            //}
            if (txtPartner.Text.Trim() != "")
            {
                string Str = txtPartner.Text.Trim().ToLower();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (isNum)
                {
                    Partner = "fakture_van.id_fakturirati='" + Str + "' AND ";
                }
                else
                {
                    DataTable DSpar = classSQL.select("SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) = '" +
                        Str + "'", "partners").Tables[0];
                    if (DSpar.Rows.Count > 0)
                    {
                        Partner = "fakture_van.id_fakturirati='" + DSpar.Rows[0][0].ToString() + "' AND ";
                    }
                    else
                    {
                        DSpar = classSQL.select("SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) like '%" +
                            Str + "%'", "partners").Tables[0];
                        if (DSpar.Rows.Count > 0)
                        {
                            Partner = "fakture_van.id_fakturirati='" + DSpar.Rows[0][0].ToString() + "' AND ";
                        }
                        else
                        {
                            Partner = "";
                        }
                    }
                }
            }
            if (chbVD.Checked)
            {
                VD = "fakture_van.id_vd='" + cbVD.SelectedValue.ToString() + "' AND ";
            }
            if (chbOD.Checked)
            {
                DateStart = "fakture_van.date >='" + dtpOD.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "fakture_van.date <='" + dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbValuta.Checked)
            {
                Valuta = "fakture_van.id_valuta='" + cbValuta.SelectedValue.ToString() + "' AND ";
            }
            if (chbArtikl.Checked)
            {
                SifraArtikla = "faktura_van_stavke.sifra ='" + cbArtikl.Text + "' AND ";
            }
            if (chbIzradio.Checked)
            {
                Izradio = "fakture_van.id_zaposlenik_izradio='" + cbIzradio.SelectedValue + "' AND ";
            }
            if (chbNapomenackb.Checked)
            {
                Napomena = "fakture_van.napomena ~* '" + textNapomena.Text + "' AND ";
            }

            string filter = Broj + Partner + VD + DateStart + DateEnd + Valuta + SifraArtikla + Izradio + Napomena;

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

            string sql = "SELECT DISTINCT " + top + " faktura_van_stavke.broj_fakture AS [Broj fakture_van],fakture_van.id_vd AS [VD]," +
                    "fakture_van.date AS Datum,fakture_van.datum_valute as [Datum valute],valute.ime_valute as [Ime Valute]" +
                    ",partners.ime_tvrtke AS [Partner],fakture_van.ukupno as [Ukupno],fakture_van.storno as [Storno]  " +
                    " FROM fakture_van INNER JOIN valute ON fakture_van.id_valuta = valute.id_valuta " +
                    " LEFT JOIN partners ON fakture_van.id_fakturirati = partners.id_partner" +
                    " INNER JOIN faktura_van_stavke ON faktura_van_stavke.broj_fakture = fakture_van.broj_fakture" + filter +
                    " ORDER BY fakture_van.date DESC" + remote;

            DSfakture = classSQL.select(sql, "fakture_van");
            dgv.DataSource = DSfakture.Tables[0];

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Format = "N2";
            dgv.Columns["Ukupno"].DefaultCellStyle = style;
            dgv.Columns["Ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns["Ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
            DSvd = classSQL.select("SELECT * FROM fakture_vd WHERE grupa='fak' ORDER BY id_vd", "fakture_vd");
            cbVD.DataSource = DSvd.Tables[0];
            cbVD.DisplayMember = "vd";
            cbVD.ValueMember = "id_vd";

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

            string broj = dgv.CurrentRow.Cells["Broj fakture_van"].FormattedValue.ToString();

            DataTable DTponude = classSQL.select("SELECT ime_valute FROM fakture_van,valute WHERE broj_fakture = '" + broj + "'" +
                " AND fakture_van.id_valuta=valute.id_valuta", "fakture_van").Tables[0];

            if (DTponude.Rows.Count > 0)
            {
                string ime_valute = DTponude.Rows[0]["ime_valute"].ToString();
                if (ValutaKuna(ime_valute))
                {
                    //if (MessageBox.Show("Ova faktura izrađena je u stranoj valuti.\nŽelite li ponudu ispisati u valuti?", "Valuta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    //    tecaj = true;
                    //}
                    //else
                    //{
                    //    tecaj = false;
                    //}
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

            Report.FakturaVan.repFaktura rfak = new Report.FakturaVan.repFaktura();
            rfak.dokumenat = "FAK";
            rfak.racunajTecaj = tecaj;
            rfak.ImeForme = "fakture_van";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj fakture_van"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        private void printaj_engl()
        {
            bool tecaj = true;

            string broj = dgv.CurrentRow.Cells["Broj fakture_van"].FormattedValue.ToString();

            DataTable DTponude = classSQL.select("SELECT ime_valute FROM fakture_van,valute WHERE broj_fakture = '" + broj + "'" +
                " AND fakture_van.id_valuta=valute.id_valuta", "fakture_van").Tables[0];

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
            rfak.ImeForme = "fakture_van";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj fakture_van"].FormattedValue.ToString();
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
            bc.broj_dokumenta = dgv.CurrentRow.Cells["Broj fakture_van"].FormattedValue.ToString();
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
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv.Rows.Count >= 1)
                {
                    string br = dgv.CurrentRow.Cells["Broj fakture_van"].FormattedValue.ToString();

                    fillDataGrid_stavke(br);
                }
            }
            catch { }
        }
    }
}