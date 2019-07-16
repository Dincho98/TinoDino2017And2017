using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmSveFaktureBezRobe : Form
    {
        public frmFakturaBezRobe MainForm { get; set; }
        public string sifra_fakture;

        public frmSveFaktureBezRobe()
        {
            InitializeComponent();
        }

        private DataSet DSfakture = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DS_Zaposlenik = new DataSet();
        private DataTable DTpostavke = new DataTable();
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
                    string br = dgv.CurrentRow.Cells["broj"].FormattedValue.ToString();

                    fillDataGrid_stavke(br);
                }
            }
            catch { }

            DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 500);
            c.FillRectangle(bG, 0, 0, Width, Height);
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

            string sql = "SELECT " + top + " ifb.broj, ifb.datum AS [Datum],ifb.datum_valute as [Datum valute],valute.ime_valute as [Ime Valute]," +
                " (SELECT SUM(uplaceno) FROM salda_konti WHERE salda_konti.broj_dokumenta=ifb.broj  AND dokumenat='FAKTURA BEZ ROBE' AND salda_konti.godina=ifb.godina) as salda_konti_uplaceno" +
                ",partners.ime_tvrtke AS Partner, ifb.ukupno as [Ukupno],id_nacin_placanja,ifb.odrediste " +
                " FROM ifb INNER JOIN valute ON ifb.id_valuta = valute.id_valuta " +
                " LEFT JOIN partners ON ifb.odrediste = partners.id_partner ORDER BY ifb.broj DESC" +
                "" + remote + "";

            DSfakture = classSQL.select(sql, "ifb");

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

                dgv.Rows.Add(r["broj"].ToString(),
                    r["Datum"].ToString(),
                    r["Ime Valute"].ToString(),
                    r["Partner"].ToString(),
                    r["Ukupno"].ToString(),
                    StatusSalda,
                    r["odrediste"].ToString()
                    );
            }

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
                string sql = "SELECT ifb_stavke.broj AS [Broj fakture],ifb_stavke.naziv as [Naziv] " +
                    ",ifb_stavke.jmj as [JMJ], ifb_stavke.kolicina as [Količina], ifb_stavke.vpc as [VPC], " +
                    "ifb_stavke.mpc as [MPC], ifb_stavke.rabat as [Rabat], ifb_stavke.porez as [Porez] " +
                    " FROM ifb_stavke WHERE broj = '" + broj + "' ORDER BY ifb_stavke.broj DESC";

                DSfakture = classSQL.select(sql, "ifb_stavke");
                dataGridView1.DataSource = DSfakture.Tables[0];

                //dgv.Columns["Ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                //dgv.Columns["Ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                //DataGridViewCellStyle style = new DataGridViewCellStyle();
                //style.Format = "N2";
                //dgv.Columns["MPC"].DefaultCellStyle = style;
                //dgv.Columns["VPC"].DefaultCellStyle = style;
                //dgv.Columns["Rabat"].DefaultCellStyle = style;
                //dgv.Columns["Količina"].DefaultCellStyle = style;
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
                string broj = dgv.CurrentRow.Cells["Broj"].Value.ToString();

                if (this.MdiParent != null)
                {
                    var mdiForm = this.MdiParent;
                    mdiForm.IsMdiContainer = true;
                    Robno.frmFakturaBezRobe childForm = new Robno.frmFakturaBezRobe();
                    childForm.MainForm = MainFormMenu;
                    childForm.broj_fakture_edit = broj;
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                }
                else
                {
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
            string SifraArtikla = "";

            if (chbBroj.Checked)
            {
                Broj = "ifb.broj='" + txtBroj.Text + "' AND ";
            }
            //if (chbSifra.Checked)
            //{
            //    Partner = "ifb.odrediste='" + txtPartner.Text + "' AND ";
            //}
            if (txtPartner.Text.Trim() != "")
            {
                string Str = txtPartner.Text.Trim().ToLower();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (isNum)
                {
                    Partner = "ifb.odrediste='" + Str + "' AND ";
                }
                else
                {
                    DataTable DSpar = classSQL.select("SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) = '" +
                        Str + "'", "partners").Tables[0];
                    if (DSpar.Rows.Count > 0)
                    {
                        Partner = "ifb.odrediste='" + DSpar.Rows[0][0].ToString() + "' AND ";
                    }
                    else
                    {
                        DSpar = classSQL.select("SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) like '%" +
                            Str + "%'", "partners").Tables[0];
                        if (DSpar.Rows.Count > 0)
                        {
                            Partner = "ifb.odrediste='" + DSpar.Rows[0][0].ToString() + "' AND ";
                        }
                        else
                        {
                            Partner = "";
                        }
                    }
                }
            }
            if (chbOD.Checked)
            {
                DateStart = "ifb.datum >='" + dtpOD.Value.ToString("yyyy-MM-dd") + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "ifb.datum <='" + dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbValuta.Checked)
            {
                Valuta = "ifb.id_valuta='" + cbValuta.SelectedValue.ToString() + "' AND ";
            }
            if (chbIzradio.Checked)
            {
                Izradio = "ifb.id_zaposlenik='" + cbIzradio.SelectedValue + "' AND ";
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
                remote = " LIMIT 1000";
            }
            else
            {
                top = " TOP(1000) ";
            }

            /*
            string sql = "SELECT DISTINCT " + top + " ifb.broj AS [Broj fakture], ifb.datum AS Datum,ifb.datum as [Datum valute],valute.ime_valute as [Ime Valute]" +
                    ",partners.ime_tvrtke AS Partner,ifb.ukupno as [Ukupno] " +
                    " FROM ifb INNER JOIN valute ON ifb.id_valuta = valute.id_valuta " +
                    " LEFT JOIN partners ON ifb.odrediste = partners.id_partner " + filter + " ORDER BY ifb.datum DESC" + remote;
            */

            string sql = "SELECT " + top + " ifb.broj, ifb.datum AS [Datum],ifb.datum_valute as [Datum valute],valute.ime_valute as [Ime Valute]," +
                 " (SELECT SUM(uplaceno) FROM salda_konti WHERE salda_konti.broj_dokumenta=ifb.broj  AND dokumenat='FAKTURA BEZ ROBE' AND salda_konti.godina=ifb.godina) as salda_konti_uplaceno" +
                 ",partners.ime_tvrtke AS Partner,ifb.ukupno as [Ukupno],id_nacin_placanja,ifb.odrediste " +
                 " FROM ifb INNER JOIN valute ON ifb.id_valuta = valute.id_valuta " +
                 " LEFT JOIN partners ON ifb.odrediste = partners.id_partner  " + filter + " ORDER BY ifb.broj DESC" +
                 "" + remote + "";

            DSfakture = classSQL.select(sql, "ifb");
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
                        StatusSalda = "Plaćeno dijelomočno";
                    }
                    else if (placeno_salda_konti >= ukupno_faktura)
                    {
                        StatusSalda = "Plaćeno";
                    }
                }

                dgv.Rows.Add(r["broj"].ToString(),
                    r["Datum"].ToString(),
                    r["Ime Valute"].ToString(),
                    r["Partner"].ToString(),
                    r["Ukupno"].ToString(),
                    StatusSalda,
                    r["odrediste"].ToString()
                    );
            }
        }

        private void fillCB()
        {
            //fill valuta
            DSValuta = classSQL.select("SELECT * FROM valute", "valute");
            cbValuta.DataSource = DSValuta.Tables[0];
            cbValuta.DisplayMember = "ime_valute";
            cbValuta.ValueMember = "id_valuta";
            cbValuta.SelectedValue = 5;

            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzradio.DataSource = DS_Zaposlenik.Tables[0];
            cbIzradio.DisplayMember = "IME";
            cbIzradio.ValueMember = "id_zaposlenik";
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Report.IFB.repFakturaIFB rfak = new Report.IFB.repFakturaIFB();
            rfak.dokumenat = "IFB";
            rfak.ImeForme = "Fakture";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            try
            {
                bool tecaj = true;

                string broj = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();

                DataTable DTponude = classSQL.select("SELECT ime_valute FROM ifb,valute WHERE broj= '" + broj + "'" +
                    " AND ifb.id_valuta=valute.id_valuta", "ifb").Tables[0];

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

                Report.IFB.repFakturaIFB rfak = new Report.IFB.repFakturaIFB();
                rfak.dokumenat = "IFB";
                rfak.ImeForme = "Fakture";
                rfak.racunajTecaj = tecaj;
                rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
                rfak.ShowDialog();
            }
            catch { }
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

        /*
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv.Rows.Count >= 1)
                {
                    string br = dgv.CurrentRow.Cells["Broj fakture"].FormattedValue.ToString();

                    fillDataGrid_stavke(br);
                }
            }
            catch { }
        }
        */

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            try
            {
                if (dgv.Rows.Count >= 1)
                {
                    string br = dgv.CurrentRow.Cells["broj"].FormattedValue.ToString();

                    fillDataGrid_stavke(br);
                }
            }
            catch { }

            if (e.ColumnIndex == 5)
            {
                decimal u;
                DateTime d;
                decimal.TryParse(dgv.CurrentRow.Cells["Ukupno"].FormattedValue.ToString(), out u);
                DateTime.TryParse(dgv.CurrentRow.Cells["datum"].FormattedValue.ToString(), out d);

                Salda_konti.frmUnosSaldaKonti sk = new Salda_konti.frmUnosSaldaKonti();
                sk._dokumenat = "FAKTURA BEZ ROBE";
                sk._broj = dgv.CurrentRow.Cells["broj"].FormattedValue.ToString();
                //sk._id_ducan = dgv.CurrentRow.Cells["id_ducan"].FormattedValue.ToString();
                //sk._id_kasa = dgv.CurrentRow.Cells["id_kasa"].FormattedValue.ToString();
                sk._iznos = u;
                sk._id_partner = dgv.CurrentRow.Cells["dgv_partner"].FormattedValue.ToString();
                sk._id_skladiste = "";
                sk._godina = d.Year.ToString();
                sk.ShowDialog();
                pictureBox1_Click(sender, e);
                //fillDataGrid();
                return;
            }
        }
    }
}