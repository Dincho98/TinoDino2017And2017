using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmSvePrimke : Form
    {
        public frmPrimka MainForm { get; set; }
        public string sifra_avansa;

        public frmSvePrimke()
        {
            InitializeComponent();
        }

        private DataSet DSprimke = new DataSet();
        private DataSet DSpartners = new DataSet();
        private DataSet DSzaposlenik = new DataSet();

        //private bool proizvodacka_cijena = false;
        public frmMenu MainFormMenu { get; set; }

        private void frmSvePrimke_Load(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Želite ispisati primku s proizvođačkim cijenama?") == DialogResult.Yes) {
            //    proizvodacka_cijena = true;
            //}
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
                    string br = dgv.CurrentRow.Cells["ID"].FormattedValue.ToString();

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

        #region buttons

        private void button1_Click(object sender, EventArgs e)
        {
            printaj();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }

            if (dgv.RowCount > 0)
            {
                int broj_primke;
                int broj_skladista;

                try
                {
                    broj_primke = Convert.ToInt16(dgv.CurrentRow.Cells["Broj primke"].Value.ToString());
                    broj_skladista = Convert.ToInt16(dgv.CurrentRow.Cells["Broj skladista"].Value.ToString());
                }
                catch
                {
                    MessageBox.Show("Unesi numeričku vrijednost!");
                    return;
                }

                if (this.MdiParent != null)
                {
                    var mdiForm = this.MdiParent;
                    mdiForm.IsMdiContainer = true;
                    frmPrimka childForm = new frmPrimka();
                    childForm.MainForm = MainFormMenu;
                    childForm.broj_primke = broj_primke;
                    childForm.broj_skladista = broj_skladista;
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                }
                else
                {
                    MainForm.broj_primke = broj_primke;
                    MainForm.broj_skladista = broj_skladista;
                    MainForm.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Niste odabrali niti jednu stavku.", "Greška");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DateTime dOtp = Convert.ToDateTime(dtpOD.Value);

            DateTime dNow = Convert.ToDateTime(dtpDO.Value);

            string Broj = "";
            string Partner = "";
            string DateStart = "";
            string DateEnd = "";
            string Izradio = "";

            if (chbBroj.Checked)
            {
                Broj = "primka.broj='" + txtBroj.Text + "' AND ";
            }
            //if (chbPartner.Checked)
            //{
            //    Partner = "primka.id_partner='" + cbPartner.SelectedValue + "' AND ";
            //}
            if (txtPartner.Text.Trim() != "")
            {
                string Str = txtPartner.Text.Trim().ToLower();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (isNum)
                {
                    Partner = "primka.id_partner='" + Str + "' AND ";
                }
                else
                {
                    DataTable DSpar = classSQL.select("SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) = '" +
                        Str + "'", "partners").Tables[0];
                    if (DSpar.Rows.Count > 0)
                    {
                        Partner = "primka.id_partner='" + DSpar.Rows[0][0].ToString() + "' AND ";
                    }
                    else
                    {
                        DSpar = classSQL.select("SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) like '%" +
                            Str + "%'", "partners").Tables[0];
                        if (DSpar.Rows.Count > 0)
                        {
                            Partner = "primka.id_partner='" + DSpar.Rows[0][0].ToString() + "' AND ";
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
                DateStart = "primka.datum >='" + dtpOD.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "primka.datum <='" + dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbIzradio.Checked)
            {
                Izradio = "primka.id_izradio='" + cbIzradio.SelectedValue + "' AND ";
            }

            string filter = Broj + Partner + DateStart + DateEnd + Izradio;

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

            string sql = "SELECT DISTINCT " + top + " primka.broj AS [Broj primke], primka.id_skladiste AS [Broj skladista]," +
                " primka.datum AS [Datum], " +
                " partners.ime_tvrtke AS [Partner], sum(primka_stavke.ukupno) as [Ukupno], primka.id_primka AS [ID] " +
                " FROM primka" +
                " LEFT JOIN partners ON primka.id_partner = partners.id_partner " +
                " LEFT JOIN primka_stavke ON primka_stavke.id_primka = primka.id_primka" +
                filter +
                " GROUP BY primka.broj, primka.id_skladiste, primka.datum, partners.ime_tvrtke, primka.id_primka" +
                " ORDER BY primka.datum DESC" + remote;

            DSprimke = classSQL.select(sql, "primka");
            dgv.DataSource = DSprimke.Tables[0];

            setNulaZaUkupno();

            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");
            dgv.Columns["ID"].Visible = false;
        }

        private void printaj()
        {
            bool proizvodacka_cijena = false;

            if (Class.PodaciTvrtka.oibTvrtke == "88985647471")
            {
                if (MessageBox.Show("Želite ispisati primku s proizvođačkim cijenama?", "Primka", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    proizvodacka_cijena = true;
                }
            }

            Report.Robno.repPrimka rav = new Report.Robno.repPrimka();

            rav.dokumenat = "AVA";

            rav.ImeForme = "Primke";

            try
            {
                rav.broj_dokumenta = dgv.CurrentRow.Cells["Broj primke"].FormattedValue.ToString();
                rav.broj_skladista = dgv.CurrentRow.Cells["Broj skladista"].FormattedValue.ToString();
                rav.proizvodacka_cijena = proizvodacka_cijena;
                rav.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Primka se ne može isprintati!");
            }
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            printaj();
        }

        #endregion buttons

        #region Util

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

            string sql = "SELECT " + top + " primka.broj AS [Broj primke], primka.id_skladiste AS [Broj skladista]," +
                " primka.datum AS [Datum], " +
                " partners.ime_tvrtke AS [Partner], sum(primka_stavke.ukupno) as [Ukupno], primka.id_primka AS [ID] " +
                " FROM primka" +
                " LEFT JOIN partners ON primka.id_partner = partners.id_partner " +
                " LEFT JOIN primka_stavke ON primka.id_primka = primka_stavke.id_primka" +
                " GROUP BY primka.broj, primka.id_skladiste, primka.datum, partners.ime_tvrtke, primka.id_primka" +
                " ORDER BY primka.broj DESC" + remote;

            DSprimke = classSQL.select(sql, "primka");
            dgv.DataSource = DSprimke.Tables[0];

            setNulaZaUkupno();

            //SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Format = "N2";
            dgv.Columns["Ukupno"].DefaultCellStyle = style;
            dgv.Columns["Ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns["Ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns["ID"].Visible = false;
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
                string sql = "SELECT primka_stavke.broj AS [Broj Primke],primka_stavke.sifra as [Sifra] " +
                    " ,primka_stavke.nbc as [NBC], primka_stavke.kolicina as [Količina], primka_stavke.vpc as [VPC], " +
                    " primka_stavke.mpc as [MPC], primka_stavke.rabat as [Rabat], primka_stavke.pdv as [Porez], " +
                    " primka_stavke.ukupno as [Ukupno] " +
                    " FROM primka_stavke " +
                    " WHERE primka_stavke.id_primka = '" + broj + "' " +
                    " ORDER BY primka_stavke.sifra DESC";

                DSprimke = classSQL.select(sql, "primka_stavke");
                dataGridView1.DataSource = DSprimke.Tables[0];
            }
            catch { }
        }

        private void setNulaZaUkupno()
        {
            if (DSprimke.Tables[0].Rows.Count < 1)
            {
                MessageBox.Show("U bazi nema stavaka!");
            }
            else
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if (dgv.Rows[i].Cells["ukupno"].FormattedValue.ToString().Trim() == "")
                    {
                        try
                        {
                            dgv.Rows[i].Cells["ukupno"].Value = "0,00";
                        }
                        catch
                        {
                            dgv.Rows[i].Cells["ukupno"].Value = "0.00";
                        }
                    }
                }
            }
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

        private void fillCB()
        {
            //fill valuta
            DSpartners = classSQL.select("SELECT ime_tvrtke, id_partner FROM partners order by ime_tvrtke", "partners");
            cbPartner.DataSource = DSpartners.Tables[0];
            cbPartner.DisplayMember = "ime_tvrtke";
            cbPartner.ValueMember = "id_partner";
            cbPartner.SelectedValue = 5;

            //fill komercijalist
            DSzaposlenik = classSQL.select("SELECT ime + ' ' + prezime as IME, id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzradio.DataSource = DSzaposlenik.Tables[0];
            cbIzradio.DisplayMember = "IME";
            cbIzradio.ValueMember = "id_zaposlenik";
        }

        #endregion Util

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
                if (dgv.Rows.Count > 1)
                {
                    string br = dgv.CurrentRow.Cells["ID"].FormattedValue.ToString();

                    fillDataGrid_stavke(br);
                }
            }
            catch { }
        }
    }
}