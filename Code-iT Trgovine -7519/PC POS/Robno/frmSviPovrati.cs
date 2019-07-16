using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmSviPovrati : Form
    {
        private DataSet DS;

        public frmSviPovrati()
        {
            InitializeComponent();
        }

        public Robno.frmPovratRobe MainForm { get; set; }
        public frmMenu MainFormMenu { get; set; }
        public string sifra;

        private void frmSviPovrati_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Form1_Paint);

            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

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

            string sql = "SELECT " + top + " povrat_robe.broj AS [Broj], povrat_robe.datum AS [Datum],skladiste.skladiste AS [Skladište]," +
                "partners.ime_tvrtke  AS [Partner] , zaposlenici.ime + ' ' + zaposlenici.prezime AS [Izradio], povrat_robe.godina AS [Godina]" +
                " FROM povrat_robe " +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik = povrat_robe.id_izradio " +
                " LEFT JOIN skladiste ON skladiste.id_skladiste = povrat_robe.id_skladiste " +
                " LEFT JOIN partners ON povrat_robe.id_partner = partners.id_partner ORDER BY CAST(povrat_robe.broj AS integer) DESC" +
                "" + remote + "";

            DS = classSQL.select(sql, "fakture");
            dgv.DataSource = DS.Tables[0];

            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");

            PaintRows(dgv);
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
                string sql = "SELECT povrat_robe_stavke.broj AS [Broj],povrat_robe_stavke.sifra as [Šifra] " +
                    " ,povrat_robe_stavke.vpc as [VPC], povrat_robe_stavke.mpc as [MPC], povrat_robe_stavke.nbc as [NBC], " +
                    " povrat_robe_stavke.pdv as [PDV], povrat_robe_stavke.kolicina as [Količina], povrat_robe_stavke.rabat as [Rabat] " +
                    " FROM povrat_robe_stavke " +
                    " WHERE povrat_robe_stavke.broj = '" + broj + "' " +
                    " ORDER BY povrat_robe_stavke.broj DESC";

                DS = classSQL.select(sql, "povrat_robe_stavke");
                dataGridView1.DataSource = DS.Tables[0];

                PaintRows(dgv);
            }
            catch { }
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
            row.Height = 22;
        }

        private void SetDecimalInDgv(DataGridView dg, string column, string column1, string column2)
        {
            for (int i = 0; i < dg.RowCount; i++)
            {
                try
                {
                    if (column != "NE")
                    {
                        //dg.Rows[i].Cells[column].Value = Convert.ToDouble(dg.Rows[i].Cells[column].FormattedValue.ToString()).ToString("#0.00");
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
                string broj = dgv.CurrentRow.Cells["Broj"].Value.ToString();

                if (this.MdiParent != null)
                {
                    var mdiForm = this.MdiParent;
                    mdiForm.IsMdiContainer = true;
                    Robno.frmPovratRobe childForm = new Robno.frmPovratRobe();
                    childForm.MainForm = MainFormMenu;
                    childForm.broj_povrata_edit = broj;
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                }
                else
                {
                    MainForm.broj_povrata_edit = broj;
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
                Broj = "povrat_robe.broj='" + txtBroj.Text + "' AND ";
            }
            //if (chbSifra.Checked)
            //{
            //    Partner = "povrat_robe.id_partner='" + txtPartner.Text + "' AND ";
            //}
            if (txtPartner.Text.Trim() != "")
            {
                string Str = txtPartner.Text.Trim().ToLower();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (isNum)
                {
                    Partner = "povrat_robe.id_partner='" + Str + "' AND ";
                }
                else
                {
                    DataTable DSpar = classSQL.select("SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) = '" +
                        Str + "'", "partners").Tables[0];
                    if (DSpar.Rows.Count > 0)
                    {
                        Partner = "povrat_robe.id_partner='" + DSpar.Rows[0][0].ToString() + "' AND ";
                    }
                    else
                    {
                        DSpar = classSQL.select("SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) like '%" +
                            Str + "%'", "partners").Tables[0];
                        if (DSpar.Rows.Count > 0)
                        {
                            Partner = "povrat_robe.id_partner='" + DSpar.Rows[0][0].ToString() + "' AND ";
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
                DateStart = "povrat_robe.datum >='" + dtpOD.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "povrat_robe.datum <='" + dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbArtikl.Checked)
            {
                SifraArtikla = "povrat_robe_stavke.sifra ='" + cbArtikl.Text + "' AND ";
            }
            if (chbIzradio.Checked)
            {
                Izradio = "povrat_robe.id_izradio='" + cbIzradio.SelectedValue + "' AND ";
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
                remote = " LIMIT 30";
            }
            else
            {
                top = " TOP(30) ";
            }

            string sql = "SELECT DISTINCT " + top + " povrat_robe.broj AS [Broj], povrat_robe.datum AS [Datum],skladiste.skladiste AS [Skladište]," +
                "partners.ime_tvrtke  AS [Partner], povrat_robe.godina AS [Godina] , zaposlenici.ime + ' ' + zaposlenici.prezime AS [Izradio]" +
                " FROM povrat_robe " +
                " LEFT JOIN povrat_robe_stavke ON povrat_robe_stavke.broj = povrat_robe.broj " +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik = povrat_robe.id_izradio " +
                " LEFT JOIN skladiste ON skladiste.id_skladiste = povrat_robe.id_skladiste " +
                " LEFT JOIN partners ON povrat_robe.id_partner = partners.id_partner " + filter + " ORDER BY povrat_robe.datum DESC" +
                "" + remote + "";

            DS = classSQL.select(sql, "fakture");
            dgv.DataSource = DS.Tables[0];

            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");

            PaintRows(dgv);
        }

        private DataSet DS_Zaposlenik;

        private void fillCB()
        {
            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzradio.DataSource = DS_Zaposlenik.Tables[0];
            cbIzradio.DisplayMember = "IME";
            cbIzradio.ValueMember = "id_zaposlenik";
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Class.Postavke.idKalkulacija == 2)
            {
                Report.Kalkulacija.frmPovratRobe2016 f = new Report.Kalkulacija.frmPovratRobe2016();
                f.dokument = "povrat_dobavljacu";
                f.broj_kalkulacije = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
                f.skladiste = dgv.CurrentRow.Cells["Skladište"].FormattedValue.ToString();
                f.Text = "Povrat robe";
                f.ShowDialog();
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("S prodajnim cijenama - YES, s nabavnim cijenama - NO?", "Odabir", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Report.PovratnicaPDV.frmListe4 rfak = new Report.PovratnicaPDV.frmListe4();

                    rfak.documenat = "POVRATNICAPDV";
                    rfak.ImeForme = "Povrat robe";
                    rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
                    rfak.godina = dgv.CurrentRow.Cells["Godina"].FormattedValue.ToString();
                    rfak.ShowDialog();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Report.PovratnicaPDV.frmListe4 rfak1 = new Report.PovratnicaPDV.frmListe4();

                    rfak1.documenat = "POVRATNICA";
                    rfak1.ImeForme = "Povrat robepdv";
                    rfak1.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
                    rfak1.godina = dgv.CurrentRow.Cells["Godina"].FormattedValue.ToString();
                    rfak1.ShowDialog();
                }
            }
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            if (Class.Postavke.idKalkulacija == 2)
            {
                Report.Kalkulacija.frmPovratRobe2016 f = new Report.Kalkulacija.frmPovratRobe2016();
                f.dokument = "povrat_dobavljacu";
                f.broj_kalkulacije = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
                f.skladiste = dgv.CurrentRow.Cells["Skladište"].FormattedValue.ToString();
                f.Text = "Povrat robe";
                f.ShowDialog();
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("S prodajnim cijenama - YES, s nabavnim cijenama - NO?", "Odabir", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Report.PovratnicaPDV.frmListe4 rfak = new Report.PovratnicaPDV.frmListe4();

                    rfak.documenat = "POVRATNICAPDV";
                    rfak.ImeForme = "Povrat robe";
                    rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
                    rfak.godina = dgv.CurrentRow.Cells["Godina"].FormattedValue.ToString();
                    rfak.ShowDialog();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Report.PovratnicaPDV.frmListe4 rfak1 = new Report.PovratnicaPDV.frmListe4();

                    rfak1.documenat = "POVRATNICA";
                    rfak1.ImeForme = "Povrat robepdv";
                    rfak1.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
                    rfak1.godina = dgv.CurrentRow.Cells["Godina"].FormattedValue.ToString();
                    rfak1.ShowDialog();
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
                            txtPartner.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
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

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv.Rows.Count >= 1)
                {
                    string br = dgv.CurrentRow.Cells["broj"].FormattedValue.ToString();

                    fillDataGrid_stavke(br);
                }
            }
            catch { }
        }
    }
}