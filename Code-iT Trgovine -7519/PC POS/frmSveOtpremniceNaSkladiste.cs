using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmSveOtpremniceNaSkladiste : Form
    {
        public frmSveOtpremniceNaSkladiste()
        {
            InitializeComponent();
        }

        private DataSet DSfakture = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DS_Zaposlenik = new DataSet();
        public frmOtpremnicaNaSkladiste MainForm { get; set; }
        public string sifra_otpremnice;
        public frmMenu MainFormMenu { get; set; }

        private void frmSveOtpremnice_Load(object sender, EventArgs e)
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
                    string br = dgv.CurrentRow.Cells["Broj otpremnice"].FormattedValue.ToString();
                    string skl = dgv.CurrentRow.Cells["Skladište OD"].FormattedValue.ToString();

                    fillDataGrid_stavke(br, skl);
                }
            }
            catch { }

            this.Paint += new PaintEventHandler(Form1_Paint);
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
                //string sql = "SELECT otpremnice_na_skl_stavke.broj_otpremnice AS [Broj],otpremnice_na_skl_stavke.sifra_robe as [Šifra] " +
                //    " ,otpremnice_na_skl_stavke.vpc as [VPC], otpremnice_na_skl_stavke.nbc as [NBC], " +
                //    " otpremnice_na_skl_stavke.porez as [PDV], otpremnice_na_skl_stavke.kolicina as [Količina], otpremnice_na_skl_stavke.rabat as [Rabat], " +
                //    " skladiste.skladiste as [Skladište] " +
                //    " FROM otpremnica_stavke " +
                //    " LEFT JOIN skladiste ON otpremnica_stavke.id_skladiste = skladiste.id_skladiste" +
                //    " WHERE otpremnica_stavke.broj_otpremnice = '" + broj + "' AND otpremnica_stavke.id_skladiste = '" + skladiste + "' " +
                //    " ORDER BY otpremnica_stavke.broj_otpremnice DESC";

                string sql = string.Format(@"SELECT os.broj_otpremnice AS [Broj], os.sifra_robe as [Šifra], os.nbc as [NBC],
os.kolicina as [Količina], os.porez as [PDV], os.rabat as [Rabat]
from otpremnice_na_skl o
left join otpremnice_na_skl_stavke os on o.id_otpremnica = os.id_otpremnice
where o.broj_otpremnice = {0} and o.id_skladisteod = (select id_skladiste from skladiste where skladiste = '{1}')
order by os.broj_otpremnice desc;", broj, skladiste);

                DSfakture = classSQL.select(sql, "otpremnica");
                dataGridView1.DataSource = DSfakture.Tables[0];
            }
            catch { }
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
                remote = " LIMIT 1500";
            }
            else
            {
                top = " TOP (1500) ";
            }

            //string sql = "SELECT " + top + " otpremnice.broj_otpremnice AS [Broj otpremnice],otpremnice.vrsta_dok AS [VD], otpremnice.datum AS [Datum]" +
            //    ",partners.ime_tvrtke AS [Partner],otpremnice.id_skladiste AS [Skladište],otpremnice.godina_otpremnice AS [Godina] FROM otpremnice" +
            //    " LEFT JOIN partners ON otpremnice.osoba_partner = partners.id_partner ORDER BY otpremnice.datum DESC" + remote +
            //    "";
            string sql = @"SELECT " + top + @" otpremnice_na_skl.broj_otpremnice AS [Broj otpremnice], otpremnice_na_skl.datum AS [Datum],
skl1.skladiste AS [Skladište OD], skl2.skladiste AS [Skladište DO], otpremnice_na_skl.godina_otpremnice AS [Godina]
FROM otpremnice_na_skl
left join skladiste as skl1 on otpremnice_na_skl.id_skladisteod = skl1.id_skladiste
left join skladiste as skl2 on otpremnice_na_skl.id_skladistedo = skl2.id_skladiste
ORDER BY otpremnice_na_skl.datum DESC" + remote + @";";

            DSfakture = classSQL.select(sql, "otpremnice");
            dgv.DataSource = DSfakture.Tables[0];
            dgv.Columns["Godina"].Visible = false;

            //dgv.Columns["Ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            //DataGridViewCellStyle style = new DataGridViewCellStyle();
            //style.Format = "N2";
            //dgv.Columns["Ukupno"].DefaultCellStyle = style;
        }

        private void btnUrediSifru_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount != 0)
            {
                string broj = dgv.CurrentRow.Cells["Broj otpremnice"].Value.ToString();

                if (this.MdiParent != null)
                {
                    var mdiForm = this.MdiParent;
                    mdiForm.IsMdiContainer = true;
                    var childForm = new frmOtpremnica();
                    childForm.broj_otpremnice_edit = broj;
                    childForm.godina_edit = dgv.CurrentRow.Cells["Godina"].Value.ToString();
                    childForm.skladiste_edit = dgv.CurrentRow.Cells["Skladište OD"].Value.ToString();
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                }
                else
                {
                    MainForm.broj_otpremnice_edit = broj;
                    MainForm.godina_edit = dgv.CurrentRow.Cells["Godina"].Value.ToString();
                    MainForm.skladiste_edit = dgv.CurrentRow.Cells["Skladište OD"].Value.ToString();
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
            PB.Size = new Size(w + 7, h + 7);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new Size(w - 7, h - 7);
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
                Broj = "otpremnice.broj_otpremnice='" + txtBroj.Text + "' AND ";
            }
            //if (chbSifra.Checked)
            //{
            //    Partner = "otpremnice.osoba_partner='" + txtPartner.Text + "' AND ";
            //}
            //if (txtPartner.Text.Trim() != "")
            //{
            //    string Str = txtPartner.Text.Trim().ToLower();
            //    double Num;
            //    bool isNum = double.TryParse(Str, out Num);
            //    if (isNum)
            //    {
            //        Partner = "otpremnice.osoba_partner='" + Str + "' AND ";
            //    }
            //    else
            //    {
            //        DataTable DSpar = classSQL.select("SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) = '" +
            //            Str + "'", "partners").Tables[0];
            //        if (DSpar.Rows.Count > 0)
            //        {
            //            Partner = "otpremnice.osoba_partner='" + DSpar.Rows[0][0].ToString() + "' AND ";
            //        }
            //        else
            //        {
            //            DSpar = classSQL.select("SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) like '%" +
            //                Str + "%'", "partners").Tables[0];
            //            if (DSpar.Rows.Count > 0)
            //            {
            //                Partner = "otpremnice.osoba_partner='" + DSpar.Rows[0][0].ToString() + "' AND ";
            //            }
            //            else
            //            {
            //                Partner = "";
            //            }
            //        }
            //    }
            //}
            //if (chbVD.Checked)
            //{
            //    VD = "otpremnice.vrsta_dok='" + cbVD.SelectedValue.ToString() + "' AND ";
            //}
            if (chbOD.Checked)
            {
                DateStart = "otpremnice.datum >='" + dtpOD.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "otpremnice.datum <='" + dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbArtikl.Checked)
            {
                SifraArtikla = "otpremnica_stavke.sifra_robe ='" + cbArtikl.Text + "' AND ";
            }
            if (chbIzradio.Checked)
            {
                Izradio = "otpremnice.id_izradio='" + cbIzradio.SelectedValue + "' AND ";
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

            string sql = "SELECT DISTINCT " + top + " otpremnice.broj_otpremnice AS [Broj otpremnice],otpremnice.vrsta_dok AS [VD], otpremnice.datum AS [Datum]" +
                ",partners.ime_tvrtke AS [Partner],otpremnice.id_skladiste AS [Skladište],otpremnice.godina_otpremnice AS [Godina] FROM otpremnice" +
                " LEFT JOIN partners ON otpremnice.osoba_partner = partners.id_partner " +
                " LEFT JOIN otpremnica_stavke ON otpremnica_stavke.broj_otpremnice=otpremnice.broj_otpremnice AND otpremnica_stavke.id_skladiste=otpremnice.id_skladiste" + filter +
                " ORDER BY otpremnice.datum DESC" + remote +
                "";

            DSfakture = classSQL.select(sql, "fakture");
            dgv.DataSource = DSfakture.Tables[0];
            dgv.Columns["Godina"].Visible = false;
        }

        private void fillCB()
        {
            //fill vrsta dokumenta
            //DSvd = classSQL.select("SELECT * FROM fakture_vd WHERE grupa='fak' ORDER BY id_vd", "fakture_vd");
            //cbVD.DataSource = DSvd.Tables[0];
            //cbVD.DisplayMember = "vd";
            //cbVD.ValueMember = "id_vd";

            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzradio.DataSource = DS_Zaposlenik.Tables[0];
            cbIzradio.DisplayMember = "IME";
            cbIzradio.ValueMember = "id_zaposlenik";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
            rfak.dokumenat = "OTP";
            rfak.ImeForme = "Otpremnica";
            rfak.from_skladiste = dgv.CurrentRow.Cells["Skladište OD"].FormattedValue.ToString();
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj otpremnice"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        private void dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
            rfak.dokumenat = "OTP";
            rfak.ImeForme = "Otpremnica";
            rfak.from_skladiste = dgv.CurrentRow.Cells["Skladište OD"].FormattedValue.ToString();
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj otpremnice"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        //private void pictureBox2_Click(object sender, EventArgs e)
        //{
        //    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
        //    partnerTrazi.ShowDialog();
        //    if (Properties.Settings.Default.id_partner != "")
        //    {
        //        DataSet partner = new DataSet();
        //        partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
        //        if (partner.Tables[0].Rows.Count > 0)
        //        {
        //            txtPartner.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
        //            SendKeys.Send("{TAB}");
        //        }
        //        else
        //        {
        //            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
        //        }
        //    }
        //}

        //private void txtPartner_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;

        //        if (txtPartner.Text == "")
        //        {
        //            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
        //            partnerTrazi.ShowDialog();
        //            if (Properties.Settings.Default.id_partner != "")
        //            {
        //                DataSet partner = new DataSet();
        //                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
        //                if (partner.Tables[0].Rows.Count > 0)
        //                {
        //                    txtPartner.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
        //                    SendKeys.Send("{TAB}");
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
        //                    txtPartner.Select();
        //                    txtPartner.SelectAll();
        //                    return;
        //                }
        //            }
        //            else
        //            {
        //                txtPartner.Select();
        //                txtPartner.SelectAll();
        //                return;
        //            }
        //        }

        //        string Str = txtPartner.Text.Trim().ToLower();
        //        double Num;
        //        bool isNum = double.TryParse(Str, out Num);

        //        if (isNum)
        //        {
        //            DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + Str + "'", "partners").Tables[0];
        //            if (DSpar.Rows.Count > 0)
        //            {
        //                txtPartner.Text = DSpar.Rows[0][0].ToString();
        //                SendKeys.Send("{TAB}");
        //            }
        //            else
        //            {
        //                MessageBox.Show("Upisana šifra ne postoji.", "Greška");
        //                txtPartner.Select();
        //                txtPartner.SelectAll();
        //            }
        //        }
        //        else
        //        {
        //            DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE LOWER(ime_tvrtke) = '" +
        //                Str + "'", "partners").Tables[0];
        //            if (DSpar.Rows.Count > 0)
        //            {
        //                txtPartner.Text = DSpar.Rows[0][0].ToString();
        //                SendKeys.Send("{TAB}");
        //            }
        //            else
        //            {
        //                DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE LOWER(ime_tvrtke) like '%" +
        //                    Str + "%'", "partners").Tables[0];
        //                if (DSpar.Rows.Count > 0)
        //                {
        //                    txtPartner.Text = DSpar.Rows[0][0].ToString();
        //                    SendKeys.Send("{TAB}");
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
        //                    txtPartner.Select();
        //                    txtPartner.SelectAll();
        //                }
        //            }
        //        }
        //    }
        //}

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv.Rows.Count >= 1)
                {
                    string br = dgv.CurrentRow.Cells["Broj otpremnice"].FormattedValue.ToString();
                    string skl = dgv.CurrentRow.Cells["Skladište"].FormattedValue.ToString();

                    fillDataGrid_stavke(br, skl);
                }
            }
            catch { }
        }

        private void btnSrch_Click(object sender, EventArgs e)
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
                Broj = "otpremnice_na_skl.broj_otpremnice='" + txtBroj.Text + "' AND ";
            }
            //if (chbSifra.Checked)
            //{
            //    Partner = "otpremnice.osoba_partner='" + txtPartner.Text + "' AND ";
            //}
            //if (txtPartner.Text.Trim() != "")
            //{
            //    string Str = txtPartner.Text.Trim().ToLower();
            //    double Num;
            //    bool isNum = double.TryParse(Str, out Num);
            //    if (isNum)
            //    {
            //        Partner = "otpremnice_na_skl.osoba_partner='" + Str + "' AND ";
            //    }
            //    else
            //    {
            //        DataTable DSpar = classSQL.select("SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) = '" +
            //            Str + "'", "partners").Tables[0];
            //        if (DSpar.Rows.Count > 0)
            //        {
            //            Partner = "otpremnice.osoba_partner='" + DSpar.Rows[0][0].ToString() + "' AND ";
            //        }
            //        else
            //        {
            //            DSpar = classSQL.select("SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) like '%" +
            //                Str + "%'", "partners").Tables[0];
            //            if (DSpar.Rows.Count > 0)
            //            {
            //                Partner = "otpremnice.osoba_partner='" + DSpar.Rows[0][0].ToString() + "' AND ";
            //            }
            //            else
            //            {
            //                Partner = "";
            //            }
            //        }
            //    }
            //}
            //if (chbVD.Checked)
            //{
            //    VD = "otpremnice.vrsta_dok='" + cbVD.SelectedValue.ToString() + "' AND ";
            //}
            if (chbOD.Checked)
            {
                DateStart = "otpremnice_na_skl.datum >='" + dtpOD.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "otpremnice_na_skl.datum <='" + dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbArtikl.Checked)
            {
                SifraArtikla = "otpremnice_na_skl_stavke.sifra_robe ='" + cbArtikl.Text + "' AND ";
            }
            if (chbIzradio.Checked)
            {
                Izradio = "otpremnice_na_skl.id_izradio = '" + cbIzradio.SelectedValue + "' AND ";
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
                remote = " LIMIT 1500";
            }
            else
            {
                top = " TOP (1500) ";
            }

            //string sql = "SELECT DISTINCT " + top + " otpremnice.broj_otpremnice AS [Broj otpremnice],otpremnice.vrsta_dok AS [VD], otpremnice.datum AS [Datum]" +
            //    ",partners.ime_tvrtke AS [Partner],otpremnice.id_skladiste AS [Skladište],otpremnice.godina_otpremnice AS [Godina] FROM otpremnice" +
            //    " LEFT JOIN partners ON otpremnice.osoba_partner = partners.id_partner " +
            //    " LEFT JOIN otpremnica_stavke ON otpremnica_stavke.broj_otpremnice=otpremnice.broj_otpremnice AND otpremnica_stavke.id_skladiste=otpremnice.id_skladiste" + filter +
            //    " ORDER BY otpremnice.datum DESC" + remote +
            //    "";

            string sql = @"SELECT DISTINCT " + top + @" otpremnice_na_skl.broj_otpremnice AS [Broj otpremnice], otpremnice_na_skl.datum AS [Datum],
skl1.skladiste AS [Skladište OD], skl2.skladiste AS [Skladište DO], otpremnice_na_skl.godina_otpremnice AS [Godina]
FROM otpremnice_na_skl
left join otpremnice_na_skl_stavke on otpremnice_na_skl_stavke.broj_otpremnice = otpremnice_na_skl.broj_otpremnice AND otpremnice_na_skl_stavke.id_skladiste = otpremnice_na_skl.id_skladisteod
left join skladiste as skl1 on otpremnice_na_skl.id_skladisteod = skl1.id_skladiste
left join skladiste as skl2 on otpremnice_na_skl.id_skladistedo = skl2.id_skladiste " + filter + @"
ORDER BY otpremnice_na_skl.datum DESC" + remote + ";";

            DSfakture = classSQL.select(sql, "fakture");
            dgv.DataSource = DSfakture.Tables[0];
            dgv.Columns["Godina"].Visible = false;
        }
    }
}