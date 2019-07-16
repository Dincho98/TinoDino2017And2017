using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmSveOdjave_komisione : Form
    {
        public frmSveOdjave_komisione()
        {
            InitializeComponent();
        }

        public frmMenu MainFormMenu { get; set; }
        public Robno.frmOdjavaKomisione MainForm { get; set; }

        private DataTable DTodjave;

        private void frmSveOdjave_komisione_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Form1_Paint);

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
                if (dgv.Rows.Count > 1)
                {
                    string br = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();

                    fillDataGrid_stavke(br);
                }
            }
            catch { }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 500);
            c.FillRectangle(bG, 0, 0, Width, Height);
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

            string sql = "SELECT " + top + " odjava_komisione.broj AS [Broj], odjava_komisione.datum AS Datum,odjava_komisione.od_datuma AS [Od datuma],odjava_komisione.do_datuma AS [Do datuma]" +
                ",partners.ime_tvrtke AS Partner,skladiste.skladiste AS [Skladište]" +
                " FROM odjava_komisione LEFT JOIN skladiste ON skladiste.id_skladiste = odjava_komisione.id_skladiste " +
                " LEFT JOIN partners ON odjava_komisione.id_partner = partners.id_partner ORDER BY CAST(odjava_komisione.broj AS integer) DESC" +
                "" + remote + "";

            DTodjave = classSQL.select(sql, "odjava_komisione").Tables[0];
            dgv.DataSource = DTodjave;

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
                string sql = "SELECT odjava_komisione_stavke.broj AS [Broj Odjave],odjava_komisione_stavke.sifra as [Sifra] " +
                    " ,odjava_komisione_stavke.kolicina_prodano as [Prodano], odjava_komisione_stavke.vpc as [VPC], " +
                    " odjava_komisione_stavke.nbc as [NBC], odjava_komisione_stavke.vpc as [VPC], " +
                    " odjava_komisione_stavke.rabat as [Rabat], odjava_komisione_stavke.dokumenat as [Dokumenat]" +
                    " FROM odjava_komisione_stavke " +
                    " WHERE odjava_komisione_stavke.broj = '" + broj + "' " +
                    " GROUP BY odjava_komisione_stavke.sifra,kolicina_prodano, vpc ";

                DTodjave = classSQL.select(sql, "odjava_komisione").Tables[0];
                dataGridView1.DataSource = DTodjave;

                PaintRows(dgv);

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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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
                Broj = "odjava_komisione.broj='" + txtBroj.Text + "' AND ";
            }
            //if (chbSifra.Checked)
            //{
            //    Partner = "odjava_komisione.id_partner='" + txtPartner.Text + "' AND ";
            //}
            if (txtPartner.Text.Trim() != "")
            {
                string Str = txtPartner.Text.Trim().ToLower();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (isNum)
                {
                    Partner = "odjava_komisione.id_partner='" + Str + "' AND ";
                }
                else
                {
                    DataTable DSpar = classSQL.select("SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) = '" +
                        Str + "'", "partners").Tables[0];
                    if (DSpar.Rows.Count > 0)
                    {
                        Partner = "odjava_komisione.id_partner='" + DSpar.Rows[0][0].ToString() + "' AND ";
                    }
                    else
                    {
                        DSpar = classSQL.select("SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) like '%" +
                            Str + "%'", "partners").Tables[0];
                        if (DSpar.Rows.Count > 0)
                        {
                            Partner = "odjava_komisione.id_partner='" + DSpar.Rows[0][0].ToString() + "' AND ";
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
                DateStart = "odjava_komisione.datum >='" + dtpOD.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "odjava_komisione.datum <='" + dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }

            if (chbArtikl.Checked)
            {
                SifraArtikla = "odjava_komisione_stavke.sifra ='" + cbArtikl.Text + "' AND ";
            }
            if (chbIzradio.Checked)
            {
                Izradio = "odjava_komisione.id_zaposlenik='" + cbIzradio.SelectedValue + "' AND ";
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

            string sql = "SELECT  DISTINCT " + top + " odjava_komisione.broj AS [Broj], odjava_komisione.datum AS Datum,odjava_komisione.od_datuma AS [Od datuma],odjava_komisione.do_datuma AS [Do datuma]" +
                ",partners.ime_tvrtke AS Partner,skladiste.skladiste AS [Skladište]" +
                " FROM odjava_komisione LEFT JOIN skladiste ON skladiste.id_skladiste = odjava_komisione.id_skladiste " +
                " LEFT JOIN odjava_komisione_stavke ON odjava_komisione_stavke.broj = odjava_komisione.broj" +
                " LEFT JOIN partners ON odjava_komisione.id_partner = partners.id_partner " + filter + " ORDER BY odjava_komisione.datum DESC" +
                "" + remote + "";

            DTodjave = classSQL.select(sql, "odjava_komisione").Tables[0];
            dgv.DataSource = DTodjave;

            PaintRows(dgv);
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

        private DataSet DS_Zaposlenik;

        private void fillCB()
        {
            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzradio.DataSource = DS_Zaposlenik.Tables[0];
            cbIzradio.DisplayMember = "IME";
            cbIzradio.ValueMember = "id_zaposlenik";
        }

        private void btnUrediSifru_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            string broj = dgv.CurrentRow.Cells["Broj"].Value.ToString();

            if (this.MdiParent != null)
            {
                var mdiForm = this.MdiParent;
                mdiForm.IsMdiContainer = true;
                Robno.frmOdjavaKomisione childForm = new Robno.frmOdjavaKomisione();
                childForm.MainForm = MainFormMenu;
                childForm.broj_komisione_edit = broj;
                childForm.MdiParent = mdiForm;
                childForm.Dock = DockStyle.Fill;
                childForm.Show();
            }
            else
            {
                MainForm.broj_komisione_edit = broj;
                MainForm.Show();
                this.Close();
            }
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            Report.Odjava.frmOdjava rfak = new Report.Odjava.frmOdjava();
            rfak.dokumenat = "odjava";
            rfak.ImeForme = "Odjava robe";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Report.Odjava.frmOdjava rfak = new Report.Odjava.frmOdjava();
            rfak.dokumenat = "odjava";
            rfak.ImeForme = "Odjava robe";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
            rfak.ShowDialog();
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
                    string br = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();

                    fillDataGrid_stavke(br);
                }
            }
            catch { }
        }
    }
}