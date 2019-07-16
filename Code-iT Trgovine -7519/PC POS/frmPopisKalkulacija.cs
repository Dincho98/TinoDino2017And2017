using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmPopisKalkulacija : Form
    {
        public frmPopisKalkulacija()
        {
            InitializeComponent();
        }

        public frmMenu MainFormMenu { get; set; }
        public frmNovaKalkulacija2 MainForm { get; set; }

        private void frmPopisKalkulacija_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

            fillCB();
            Fill();
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

        private DataSet DS_Zaposlenik = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DSskladiste = new DataSet();

        private void fillCB()
        {
            //DS skladiste
            DSskladiste = classSQL.select("SELECT * FROM skladiste ORDER BY skladiste", "skladiste");
            cbSkladiste.DataSource = DSskladiste.Tables[0];
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";

            //fill valuta
            DSValuta = classSQL.select("SELECT * FROM valute", "valute");
            cbValuta.DataSource = DSValuta.Tables[0];
            cbValuta.DisplayMember = "ime_valute";
            cbValuta.ValueMember = "id_valuta";
            cbValuta.SelectedValue = 5;

            //fill vrsta dokumenta
            DSvd = classSQL.select("SELECT * FROM fakture_vd WHERE grupa='kal' ORDER BY id_vd", "fakture_vd");
            cbVD.DataSource = DSvd.Tables[0];
            cbVD.DisplayMember = "vd";
            cbVD.ValueMember = "id_vd";

            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzradio.DataSource = DS_Zaposlenik.Tables[0];
            cbIzradio.DisplayMember = "IME";
            cbIzradio.ValueMember = "id_zaposlenik";
        }

        private void Fill()
        {
            string compact = "", remote = "";
            if (classSQL.remoteConnectionString == "")
            {
                compact = " TOP (5500) ";
            }
            else
            {
                remote = " LIMIT 5500 ";
            }

            string sql = "SELECT " + compact + " kalkulacija.broj as [Broj],partners.ime_tvrtke AS [Dobavljač],skladiste.skladiste," +
                "kalkulacija.datum AS [Datum],skladiste.skladiste AS [Skladište],kalkulacija.racun AS [Racun]," +
                " (SELECT coalesce(SUM(salda_konti.isplaceno),0) FROM salda_konti WHERE dokumenat='KALKULACIJA' AND salda_konti.id_skladiste=kalkulacija.id_skladiste AND salda_konti.broj_dokumenta=kalkulacija.broj AND salda_konti.godina=CAST(kalkulacija.godina as INT)) as salda_konti_uplaceno, " +
                "kalkulacija.ukupno_vpc AS [Ukupno VPC],kalkulacija.ukupno_mpc AS [Ukupno MPC]," +
                "fakturni_iznos AS [Fakturni iznos],kalkulacija.id_skladiste AS [id_skladiste], kalkulacija.id_kalkulacija AS [ID],kalkulacija.id_partner FROM kalkulacija" +
                " LEFT JOIN partners ON kalkulacija.id_partner = partners.id_partner" +
                " LEFT JOIN skladiste ON kalkulacija.id_skladiste = skladiste.id_skladiste ORDER BY CAST(kalkulacija.broj AS integer) DESC " + remote + "";

            DataTable DTkalkulacije = classSQL.select(sql, "kalkulacija").Tables[0];

            dataGridView1.Rows.Clear();
            foreach (DataRow r in DTkalkulacije.Rows)
            {
                string StatusSalda = "";
                decimal placeno_salda_konti, ukupno_faktura;
                decimal.TryParse(r["salda_konti_uplaceno"].ToString(), out placeno_salda_konti);
                decimal.TryParse(r["Fakturni iznos"].ToString(), out ukupno_faktura);

                if (placeno_salda_konti == 0)
                {
                    StatusSalda = "Neplaćeno";
                }
                else if (placeno_salda_konti > 0 && placeno_salda_konti < ukupno_faktura)
                {
                    StatusSalda = "Plaćeno dijelomično";
                }
                else if (placeno_salda_konti >= ukupno_faktura)
                {
                    StatusSalda = "Plaćeno";
                }

                int bbb;
                DateTime dttt;
                int.TryParse(r["Broj"].ToString(), out bbb);
                DateTime.TryParse(r["Datum"].ToString(), out dttt);

                decimal Uvpc, Umpc, Fiznos;
                decimal.TryParse(r["Ukupno VPC"].ToString(), out Uvpc);
                decimal.TryParse(r["Ukupno MPC"].ToString(), out Umpc);
                decimal.TryParse(r["Fakturni iznos"].ToString(), out Fiznos);

                dataGridView1.Rows.Add(bbb,
                    r["Racun"].ToString(),
                    r["Dobavljač"].ToString(),
                    r["skladiste"].ToString(),
                    dttt,
                    Math.Round(Uvpc, 3).ToString("N2"),
                    Math.Round(Umpc, 3).ToString("N2"),
                    Math.Round(Fiznos, 3).ToString("N2"),
                    StatusSalda,
                    r["id_skladiste"].ToString(),
                    r["ID"].ToString(),
                    r["id_partner"].ToString()
                    );
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            if (e.ColumnIndex == 8)
            {
                decimal u;
                DateTime d;
                decimal.TryParse(dataGridView1.CurrentRow.Cells["fakturni_iznos"].FormattedValue.ToString(), out u);
                DateTime.TryParse(dataGridView1.CurrentRow.Cells["datum"].FormattedValue.ToString(), out d);

                Salda_konti.frmUnosSaldaKonti sk = new Salda_konti.frmUnosSaldaKonti();
                sk._dokumenat = "KALKULACIJA";
                sk._broj = dataGridView1.CurrentRow.Cells["broj"].FormattedValue.ToString();
                sk._id_ducan = "";
                sk._id_kasa = "";
                sk._iznos = u;
                sk._id_partner = dataGridView1.CurrentRow.Cells["id_partner"].FormattedValue.ToString();
                sk._id_skladiste = dataGridView1.CurrentRow.Cells["id_skladiste"].FormattedValue.ToString();
                sk._godina = d.Year.ToString();
                sk.ShowDialog();
                pictureBox1_Click(sender, e);

                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            try
            {
                string id = dataGridView1.CurrentRow.Cells[0].FormattedValue.ToString();

                if (this.MdiParent == null)
                {
                    MainForm.broj_kalkulacije_edit = id;
                    MainForm.edit_skladiste = dataGridView1.CurrentRow.Cells["id_skladiste"].FormattedValue.ToString();
                    this.Close();
                }
                else
                {
                    var mdiForm = this.MdiParent;
                    mdiForm.IsMdiContainer = true;
                    var childForm = new frmNovaKalkulacija2();
                    childForm.broj_kalkulacije_edit = id;
                    childForm.edit_skladiste = dataGridView1.CurrentRow.Cells["id_skladiste"].FormattedValue.ToString();
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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

            DateTime dNow = Convert.ToDateTime(dtpDO.Value);

            string Broj = "";
            string Partner = "";
            string DateStart = "";
            string DateEnd = "";
            string VD = "";
            string Valuta = "";
            string Izradio = "";
            string SifraArtikla = "";
            string Skladiste = "";

            if (chbBroj.Checked)
            {
                Broj = "kalkulacija.broj='" + txtBroj.Text + "' AND ";
            }

            if (txtPartner.Text.Trim() != "")
            {
                string Str = txtPartner.Text.Trim().ToLower();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (isNum)
                {
                    Partner = "kalkulacija.id_partner='" + Str + "' AND ";
                }
                else
                {
                    DataTable DSpar = classSQL.select("SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) = '" +
                        Str + "'", "partners").Tables[0];
                    if (DSpar.Rows.Count > 0)
                    {
                        Partner = "kalkulacija.id_partner='" + DSpar.Rows[0][0].ToString() + "' AND ";
                    }
                    else
                    {
                        DSpar = classSQL.select("SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) like '%" +
                            Str + "%'", "partners").Tables[0];
                        if (DSpar.Rows.Count > 0)
                        {
                            Partner = "kalkulacija.id_partner='" + DSpar.Rows[0][0].ToString() + "' AND ";
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
                DateStart = "kalkulacija.datum >='" + dOtp + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "kalkulacija.datum <='" + dNow + "' AND ";
            }
            if (chbValuta.Checked)
            {
                Valuta = "kalkulacija.id_valuta='" + cbValuta.SelectedValue.ToString() + "' AND ";
            }
            if (chbArtikl.Checked)
            {
                SifraArtikla = "kalkulacija_stavke.sifra ='" + cbArtikl.Text + "' AND ";
            }
            if (chbIzradio.Checked)
            {
                Izradio = "kalkulacija.id_zaposlenik='" + cbIzradio.SelectedValue + "' AND ";
            }
            if (chbSkladiste.Checked)
            {
                Skladiste = "kalkulacija.id_skladiste='" + cbSkladiste.SelectedValue + "' AND ";
            }

            string filter = Broj + Partner + VD + DateStart + DateEnd + Valuta + SifraArtikla + Izradio + Skladiste;

            if (filter.Length != 0)
            {
                filter = " WHERE " + filter;
                filter = filter.Remove(filter.Length - 4, 4);
            }

            string compact = "", remote = "";
            if (classSQL.remoteConnectionString == "")
            {
                compact = " TOP (5500) ";
            }
            else
            {
                remote = " LIMIT 5500 ";
            }

            string sql = "SELECT DISTINCT " + compact + " kalkulacija.broj as [Broj],partners.ime_tvrtke AS [Dobavljač],skladiste.skladiste," +
                " kalkulacija.datum AS [Datum],skladiste.skladiste AS [Skladište],kalkulacija.racun AS [Racun]," +
                " (SELECT coalesce(SUM(salda_konti.isplaceno),0) FROM salda_konti WHERE dokumenat='KALKULACIJA' AND salda_konti.id_skladiste=kalkulacija.id_skladiste AND salda_konti.broj_dokumenta=kalkulacija.broj AND salda_konti.godina=CAST(kalkulacija.godina as INT)) as salda_konti_uplaceno, " +
                " kalkulacija.ukupno_vpc AS [Ukupno VPC],kalkulacija.ukupno_mpc AS [Ukupno MPC]," +
                " fakturni_iznos AS [Fakturni iznos],kalkulacija.id_skladiste AS [id_skladiste], kalkulacija.id_kalkulacija AS [ID],kalkulacija.id_partner FROM kalkulacija" +
                " LEFT JOIN partners ON kalkulacija.id_partner = partners.id_partner" +
                " LEFT JOIN skladiste ON kalkulacija.id_skladiste = skladiste.id_skladiste " +
                " LEFT JOIN kalkulacija_stavke ON kalkulacija.broj = kalkulacija_stavke.broj AND kalkulacija.id_skladiste = kalkulacija_stavke.id_skladiste" +
                filter +
                " ORDER BY kalkulacija.datum DESC " + remote + "";

            DataTable DTkalkulacije = classSQL.select(sql, "kalkulacija").Tables[0];
            dataGridView1.Rows.Clear();
            foreach (DataRow r in DTkalkulacije.Rows)
            {
                string StatusSalda = "";
                decimal placeno_salda_konti, ukupno_faktura;
                decimal.TryParse(r["salda_konti_uplaceno"].ToString(), out placeno_salda_konti);
                decimal.TryParse(r["Fakturni iznos"].ToString(), out ukupno_faktura);

                if (placeno_salda_konti == 0)
                {
                    StatusSalda = "Neplaćeno";
                }
                else if (placeno_salda_konti > 0 && placeno_salda_konti < ukupno_faktura)
                {
                    StatusSalda = "Plaćeno dijelomično";
                }
                else if (placeno_salda_konti >= ukupno_faktura)
                {
                    StatusSalda = "Plaćeno";
                }

                int bbb;
                DateTime dttt;
                int.TryParse(r["Broj"].ToString(), out bbb);
                DateTime.TryParse(r["Datum"].ToString(), out dttt);

                decimal Uvpc, Umpc, Fiznos;
                decimal.TryParse(r["Ukupno VPC"].ToString(), out Uvpc);
                decimal.TryParse(r["Ukupno MPC"].ToString(), out Umpc);
                decimal.TryParse(r["Fakturni iznos"].ToString(), out Fiznos);

                dataGridView1.Rows.Add(bbb,
                    r["Racun"].ToString(),
                    r["Dobavljač"].ToString(),
                    r["skladiste"].ToString(),
                    dttt,
                    Math.Round(Uvpc, 3).ToString("N2"),
                    Math.Round(Umpc, 3).ToString("N2"),
                    Math.Round(Fiznos, 3).ToString("N2"),
                    StatusSalda,
                    r["id_skladiste"].ToString(),
                    r["ID"].ToString(),
                    r["id_partner"].ToString()
                    );
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                printaj();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                printaj();
            }
        }

        private void printaj()
        {
            if (Class.Postavke.idKalkulacija == 2)
            {
                Report.Kalkulacija.frmKalkulacija2016 kalk = new Report.Kalkulacija.frmKalkulacija2016();
                kalk.broj_kalkulacije = dataGridView1.CurrentRow.Cells["Broj"].FormattedValue.ToString();
                kalk.skladiste = dataGridView1.CurrentRow.Cells["id_skladiste"].FormattedValue.ToString();
                kalk.ShowDialog();
            }
            else
            {
                Report.Kalkulacija.frmKalkulacija kalk = new Report.Kalkulacija.frmKalkulacija();
                kalk.broj_kalkulacije = dataGridView1.CurrentRow.Cells["Broj"].FormattedValue.ToString();
                kalk.skladiste = dataGridView1.CurrentRow.Cells["id_skladiste"].FormattedValue.ToString();
                kalk.ShowDialog();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner = '" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtPartner.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    //SendKeys.Send("{TAB}");
                    SelectNextControl((sender as Control), true, true, true, true);
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
                            //SendKeys.Send("{TAB}");
                            SelectNextControl((sender as Control), true, true, true, true);
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
                        SelectNextControl((sender as Control), true, true, true, true);
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