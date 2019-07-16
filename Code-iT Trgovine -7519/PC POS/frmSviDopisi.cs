using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmSviDopisi : Form
    {
        public frmDopis MainForm { get; set; }
        public frmOtpremnica MainForm2 { get; set; }
        public string sifra_ponude;

        public frmSviDopisi()
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

            try
            {
                if (dgv.Rows.Count >= 1)
                {
                    string br = dgv.CurrentRow.Cells["Broj Ponude"].FormattedValue.ToString();

                    fillDataGrid_stavke(br);
                }
            }
            catch { }

            this.Paint += new PaintEventHandler(Form1_Paint);

            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");
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

                    if (dg.Rows[i].Cells["Realizirano"].Value.ToString() == "DA")
                    {
                        dg.Rows[i].Cells["Realizirano"].Style.ForeColor = Color.DarkGreen;
                    }
                    else
                    {
                        dg.Rows[i].Cells["Realizirano"].Style.ForeColor = Color.Red;
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
            DSvd = classSQL.select("SELECT * FROM fakture_vd WHERE grupa='pon' ORDER BY id_vd", "fakture_vd");
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
                remote = " LIMIT 5500";
            }
            else
            {
                top = " TOP(5500) ";
            }

            string sql = "SELECT " + top + " ponude.broj_ponude AS [Broj ponude],ponude.id_vd AS [VD], ponude.date AS [Datum]," +
                "ponude.vrijedi_do as [Vrijedi do],valute.ime_valute as [Ime valute]" +
                ",partners.ime_tvrtke AS [Partner],ponude.ukupno as [Ukupno], ponude.ponuda_nbc, case when ponude.realizirano = true then 'DA' else 'NE' end as [Realizirano] " +
                " FROM ponude INNER JOIN valute ON ponude.id_valuta = valute.id_valuta " +
                " LEFT JOIN partners ON ponude.id_fakturirati = partners.id_partner ORDER BY CAST(ponude.broj_ponude AS integer)" +
                " DESC " + "" + remote + "";

            DSfakture = classSQL.select(sql, "fakture");
            dgv.DataSource = DSfakture.Tables[0];

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Format = "N2";
            dgv.Columns["Ukupno"].DefaultCellStyle = style;
            dgv.Columns["Ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns["Ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns["Realizirano"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Realizirano"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns["ponuda_nbc"].Visible = false;

            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");
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
                string sql = "SELECT ponude_stavke.broj_ponude AS [Broj],ponude_stavke.sifra as [Šifra] " +
                    " ,ponude_stavke.vpc as [VPC],  " +
                    " ROUND((CAST(ponude_stavke.vpc as numeric) * (1 zbroj (CAST(replace(ponude_stavke.porez,',','.') as numeric)/100))),2) as [MPC], " +
                    " ponude_stavke.porez as [PDV], ponude_stavke.kolicina as [Količina], ponude_stavke.rabat as [Rabat], " +
                    " ponude_stavke.id_skladiste as [Skladište], ponude_stavke.porez_potrosnja as [Porez na potrošnju] " +
                    " FROM ponude_stavke " +
                    " WHERE ponude_stavke.broj_ponude = '" + broj + "' " +
                    " ORDER BY ponude_stavke.broj_ponude DESC";

                DSfakture = classSQL.select(sql, "ponude_stavke");
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
                string broj = dgv.CurrentRow.Cells["Broj ponude"].Value.ToString();

                if (this.MdiParent != null)
                {
                    var mdiForm = this.MdiParent;
                    mdiForm.IsMdiContainer = true;
                    var childForm = new frmPonude();
                    childForm.broj_ponude_edit = broj;
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                }
                else
                {
                    if (MainForm == null && MainForm2 != null)
                    {
                        MainForm2.broj_ponude_edit = broj;
                        MainForm2.Show();
                    }
                    else
                    {
                        MainForm.broj_ponude_edit = broj;
                        MainForm.Show();
                    }

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
                Broj = "ponude.broj_ponude='" + txtBroj.Text + "' AND ";
            }
            //if (chbSifra.Checked)
            //{
            //    Partner = "ponude.id_fakturirati='" + txtPartner.Text + "' AND ";
            //}
            if (txtPartner.Text.Trim() != "")
            {
                string Str = txtPartner.Text.Trim().ToLower();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (isNum)
                {
                    Partner = "ponude.id_fakturirati='" + Str + "' AND ";
                }
                else
                {
                    DataTable DSpar = classSQL.select("SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) = '" +
                        Str + "'", "partners").Tables[0];
                    if (DSpar.Rows.Count > 0)
                    {
                        if (DSpar.Rows.Count == 1)
                        {
                            Partner = "ponude.id_fakturirati='" + DSpar.Rows[0][0].ToString() + "' AND ";
                        }
                        else
                        {
                            Partner = string.Format(@"ponude.id_fakturirati in (SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) = '{0}') AND ", Str);
                        }
                    }
                    else
                    {
                        DSpar = classSQL.select("SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) like '%" +
                            Str + "%'", "partners").Tables[0];
                        if (DSpar.Rows.Count > 0)
                        {
                            Partner = "ponude.id_fakturirati='" + DSpar.Rows[0][0].ToString() + "' AND ";
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
                VD = "ponude.id_vd='" + cbVD.SelectedValue.ToString() + "' AND ";
            }
            if (chbOD.Checked)
            {
                DateStart = "ponude.date >='" + dOtp + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "ponude.date <='" + dNow + "' AND ";
            }
            if (chbValuta.Checked)
            {
                Valuta = "ponude.id_valuta='" + cbValuta.SelectedValue.ToString() + "' AND ";
            }
            if (chbArtikl.Checked)
            {
                SifraArtikla = "ponude_stavke.sifra ='" + cbArtikl.Text + "' AND ";
            }
            if (chbIzradio.Checked)
            {
                Izradio = "ponude.id_zaposlenik_izradio='" + cbIzradio.SelectedValue + "' AND ";
            }
            if (chbnapomenacb.Checked)
            {
                Napomena = "ponude.napomena ~* '" + textnapomena.Text + "' AND ";
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
                remote = " LIMIT 5500";
            }
            else
            {
                top = " TOP(5500) ";
            }
            //DataGridViewCellStyle style = new DataGridViewCellStyle();
            //style.Format = "N2";
            //dgv.Columns["Ukupno"].DefaultCellStyle = style;
            //dgv.Columns["Ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgv.Columns["Ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            string sql = @"SELECT DISTINCT " + top + @" ponude_stavke.broj_ponude AS [Broj ponude], ponude.id_vd AS [VD],
ponude.date AS [Datum], ponude.vrijedi_do as [Vrijedi do], valute.ime_valute as [Ime valute]
,partners.ime_tvrtke AS [Partner], ponude.ukupno as [Ukupno], ponude.ponuda_nbc, case when ponude.realizirano = true then 'DA' else 'NE' end as [Realizirano]
FROM ponude INNER JOIN valute ON ponude.id_valuta = valute.id_valuta
LEFT JOIN partners ON ponude.id_fakturirati = partners.id_partner
INNER JOIN ponude_stavke ON ponude.broj_ponude = ponude_stavke.broj_ponude
" + filter + @"
ORDER BY ponude.date DESC" + remote;

            DSfakture = classSQL.select(sql, "fakture");
            dgv.DataSource = DSfakture.Tables[0];
            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Format = "N2";
            dgv.Columns["Ukupno"].DefaultCellStyle = style;
            dgv.Columns["Ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns["Ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns["Realizirano"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Realizirano"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["ponuda_nbc"].Visible = false;
        }

        private void frmSvePonude_Activated(object sender, EventArgs e)
        {
            //fillDataGrid();
            SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            printaj();
        }

        private void frmSvePonude_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MainForm == null)
            {
            }
        }

        private void dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgv.Columns[e.ColumnIndex].Name != "Realizirano")
                printaj();
        }

        private void printaj(bool pos_print = false)
        {
            bool tecaj = true;

            string broj = dgv.CurrentRow.Cells["Broj ponude"].FormattedValue.ToString();
            bool ponuda_nbc = false;
            bool.TryParse(dgv.CurrentRow.Cells["ponuda_nbc"].Value.ToString(), out ponuda_nbc);

            DataTable DTponude = classSQL.select("SELECT ime_valute FROM ponude, valute WHERE broj_ponude = '" + broj + "'" +
                " AND ponude.id_valuta=valute.id_valuta", "fakture").Tables[0];

            if (DTponude.Rows.Count > 0)
            {
                string ime_valute = DTponude.Rows[0]["ime_valute"].ToString();
                if (ValutaKuna(ime_valute))
                {
                    if (MessageBox.Show("Ova ponuda izrađena je u stranoj valuti.\nŽelite li ponudu ispisati u valuti?", "Valuta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

            if (pos_print)
            {
                int brojPonude = 0;
                int.TryParse(dgv.CurrentRow.Cells["Broj ponude"].Value.ToString(), out brojPonude);
                PosPrint.classPosPrintPonuda printPonuda = new PosPrint.classPosPrintPonuda(brojPonude);
                printPonuda.printReceipt(printPonuda.broj_ponude);
                printPonuda.printaj();
                printPonuda = null;
            }
            else
            {
                Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
                rfak.dokumenat = "PON";
                rfak.racunajTecaj = tecaj;
                rfak.ImeForme = "Ponude";
                rfak.ponudaUNbc = ponuda_nbc;
                rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj ponude"].FormattedValue.ToString();
                rfak.ShowDialog();
            }
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
                    string br = dgv.CurrentRow.Cells["Broj Ponude"].FormattedValue.ToString();

                    fillDataGrid_stavke(br);
                }
            }
            catch { }
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowindex = -1;
                if (dgv.Rows.Count > 0)
                {
                    rowindex = e.RowIndex;
                    if (rowindex >= 0)
                    {
                        if (dgv.Columns[e.ColumnIndex].Name == "Realizirano")
                        {
                            if (MessageBox.Show("Želite promijeniti status realizacije?", "Realizacija", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                            {
                                string sql = "";
                                string brojPonude = dgv.Rows[rowindex].Cells["Broj ponude"].Value.ToString();
                                if (dgv.Rows[rowindex].Cells[e.ColumnIndex].Value.ToString() == "DA")
                                {
                                    dgv.Rows[rowindex].Cells[e.ColumnIndex].Value = "NE";
                                    sql = string.Format("update ponude set realizirano = false where broj_ponude = '{0}';", brojPonude);
                                }
                                else
                                {
                                    dgv.Rows[rowindex].Cells[e.ColumnIndex].Value = "DA";
                                    sql = string.Format("update ponude set realizirano = true where broj_ponude = '{0}';", brojPonude);
                                }
                                classSQL.update(sql);
                            }
                        }
                    }
                }
                SetDecimalInDgv(dgv, "NE", "NE", "NE");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnIspisPonudePOS_Click(object sender, EventArgs e)
        {
            try
            {
                printaj(true);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}