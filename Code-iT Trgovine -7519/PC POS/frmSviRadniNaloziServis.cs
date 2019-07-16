using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmSviRadniNaloziServis : Form
    {
        public frmRadniNalogSerivs MainForm { get; set; }
        public string sifra_rn;

        public frmSviRadniNaloziServis()
        {
            InitializeComponent();
        }

        private DataSet DS_Zaposlenik = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataSet DSValuta = new DataSet();

        public frmMenu MainFormMenu { get; set; }

        private void frmSvePonude_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

            fillCB();
            fillDataGrid();
            this.Paint += new PaintEventHandler(Form1_Paint);
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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 500);
            c.FillRectangle(bG, 0, 0, Width, Height);
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
            DSvd = classSQL.select("SELECT * FROM fakture_vd WHERE grupa='rn' ORDER BY id_vd", "fakture_vd");
            cbVD.DataSource = DSvd.Tables[0];
            cbVD.DisplayMember = "vd";
            cbVD.ValueMember = "id_vd";

            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzradio.DataSource = DS_Zaposlenik.Tables[0];
            cbIzradio.DisplayMember = "IME";
            cbIzradio.ValueMember = "id_zaposlenik";
        }

        private DataSet DSfakture = new DataSet();

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

            string sql = "SELECT " + top + " radni_nalog_servis.broj AS [Broj],radni_nalog_servis.id_vd AS [VD], radni_nalog_servis.date AS [Datum]," +
                "radni_nalog_servis.vrijedi_do as [Vrijedi do],valute.ime_valute as [Ime valute]" +
                ",partners.ime_tvrtke AS [Partner],radni_nalog_servis.ukupno as [Ukupno] " +
                " FROM radni_nalog_servis INNER JOIN valute ON radni_nalog_servis.id_valuta = valute.id_valuta " +
                " LEFT JOIN partners ON radni_nalog_servis.id_fakturirati = partners.id_partner ORDER BY CAST(radni_nalog_servis.broj AS integer)" +
                " DESC " + "" + remote + "";

            DSfakture = classSQL.select(sql, "radni_nalog_servis");
            dgv.DataSource = DSfakture.Tables[0];
            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Format = "N2";
            dgv.Columns["Ukupno"].DefaultCellStyle = style;
            dgv.Columns["Ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns["Ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
                    var childForm = new frmRadniNalogSerivs();
                    childForm.broj_rn_edit = broj;
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                }
                else
                {
                    MainForm.broj_rn_edit = broj;
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

            DateTime dNow = Convert.ToDateTime(dtpDO.Value);

            string Broj = "";
            string Partner = "";
            string DateStart = "";
            string DateEnd = "";
            string VD = "";
            string Valuta = "";
            string Izradio = "";
            string SifraArtikla = "";
            string Napomena = "";

            if (chbBroj.Checked)
            {
                Broj = "radni_nalog_servis.broj='" + txtBroj.Text + "' AND ";
            }
            //if (chbSifra.Checked)
            //{
            //    Partner = "radni_nalog_servis.id_fakturirati='" + txtPartner.Text + "' AND ";
            //}
            if (txtPartner.Text.Trim() != "")
            {
                string Str = txtPartner.Text.Trim().ToLower();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (isNum)
                {
                    Partner = "radni_nalog_servis.id_fakturirati='" + Str + "' AND ";
                }
                else
                {
                    DataTable DSpar = classSQL.select("SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) = '" +
                        Str + "'", "partners").Tables[0];
                    if (DSpar.Rows.Count > 0)
                    {
                        Partner = "radni_nalog_servis.id_fakturirati='" + DSpar.Rows[0][0].ToString() + "' AND ";
                    }
                    else
                    {
                        DSpar = classSQL.select("SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) like '%" +
                            Str + "%'", "partners").Tables[0];
                        if (DSpar.Rows.Count > 0)
                        {
                            Partner = "radni_nalog_servis.id_fakturirati='" + DSpar.Rows[0][0].ToString() + "' AND ";
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
                VD = "radni_nalog_servis.id_vd='" + cbVD.SelectedValue.ToString() + "' AND ";
            }
            if (chbOD.Checked)
            {
                DateStart = "radni_nalog_servis.date >='" + dOtp + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "radni_nalog_servis.date <='" + dNow + "' AND ";
            }
            if (chbValuta.Checked)
            {
                Valuta = "radni_nalog_servis.id_valuta='" + cbValuta.SelectedValue.ToString() + "' AND ";
            }
            if (chbArtikl.Checked)
            {
                SifraArtikla = "radni_nalog_servis_stavke.sifra ='" + cbArtikl.Text + "' AND ";
            }
            if (chbIzradio.Checked)
            {
                Izradio = "radni_nalog_servis.id_zaposlenik_izradio='" + cbIzradio.SelectedValue + "' AND ";
            }
            if (chbnapomenacb.Checked)
            {
                Napomena = "radni_nalog_servis.napomena ~* '" + textnapomena.Text + "' AND ";
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
            //DataGridViewCellStyle style = new DataGridViewCellStyle();
            //style.Format = "N2";
            //dgv.Columns["Ukupno"].DefaultCellStyle = style;
            //dgv.Columns["Ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgv.Columns["Ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            string sql = "SELECT DISTINCT " + top + " radni_nalog_servis_stavke.broj AS [Broj],radni_nalog_servis.id_vd AS [VD]," +
                "radni_nalog_servis.date AS [Datum],radni_nalog_servis.vrijedi_do as [Vrijedi do],valute.ime_valute as [Ime valute]" +
                ",partners.ime_tvrtke AS [Partner],radni_nalog_servis.ukupno as [Ukupno] " +
                " FROM radni_nalog_servis INNER JOIN valute ON radni_nalog_servis.id_valuta = valute.id_valuta " +
                " LEFT JOIN partners ON radni_nalog_servis.id_fakturirati = partners.id_partner INNER JOIN radni_nalog_servis_stavke ON " +
                "radni_nalog_servis.broj = radni_nalog_servis_stavke.broj" + filter + " ORDER BY radni_nalog_servis.date DESC" + remote;

            DSfakture = classSQL.select(sql, "fakture");
            dgv.DataSource = DSfakture.Tables[0];
            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Format = "N2";
            dgv.Columns["Ukupno"].DefaultCellStyle = style;
            dgv.Columns["Ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns["Ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void frmSvePonude_Activated(object sender, EventArgs e)
        {
            fillDataGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
            rfak.dokumenat = "RNS";
            rfak.ImeForme = "Radni nalog servis";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        private void frmSvePonude_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MainForm == null)
            {
            }
        }

        private void dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
            rfak.dokumenat = "RNS";
            rfak.ImeForme = "Radni nalog servis";
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
    }
}