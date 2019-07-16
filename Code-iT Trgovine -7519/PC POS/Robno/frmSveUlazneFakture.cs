using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmSveUlazneFakture : Form
    {
        public Robno.frmUlazneFakture MainForm { get; set; }
        public string sifra_fakture;

        public frmSveUlazneFakture()
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

            PaintRows(dgv);
            fillCB();
            fillDataGrid();
            DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

            //this.Paint += new PaintEventHandler(Form1_Paint);
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
                remote = " LIMIT 1000";
            }
            else
            {
                top = " TOP(1000) ";
            }

            string sql = "SELECT ulazna_faktura.*," +
                " (SELECT coalesce(SUM(salda_konti.isplaceno),0) FROM salda_konti WHERE salda_konti.broj_dokumenta=ulazna_faktura.broj AND salda_konti.godina=ulazna_faktura.godina) as salda_konti_uplaceno " +
                " FROM ulazna_faktura ORDER BY CAST(broj as INT) DESC" + remote; ;

            DSfakture = classSQL.select(sql, "fakture");

            foreach (DataRow r in DSfakture.Tables[0].Rows)
            {
                bool bollHUB = false;
                string HUB_kreirano = "NE";
                if (r["hub_kreirano"].ToString() == "1")
                {
                    HUB_kreirano = "DA";
                }

                string StatusSalda = "";
                decimal placeno_salda_konti, ukupno_faktura;
                decimal.TryParse(r["salda_konti_uplaceno"].ToString(), out placeno_salda_konti);
                decimal.TryParse(r["iznos"].ToString(), out ukupno_faktura);

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

                int bbb;
                DateTime dttt;
                int.TryParse(r["broj"].ToString(), out bbb);
                DateTime.TryParse(r["datum_izvrsenja"].ToString(), out dttt);

                dgv.Rows.Add(bbb,
                    dttt,
                    r["valuta"].ToString(),
                    r["primatelj_naziv"].ToString(),
                    r["iznos"].ToString(),
                    HUB_kreirano, false, StatusSalda, r["id"].ToString(), r["godina"].ToString(), r["primatelj_sifra"].ToString());
            }

            PaintRows(dgv);
        }

        private void btnUrediSifru_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            if (dgv.RowCount > 0)
            {
                string broj = dgv.CurrentRow.Cells["broj"].Value.ToString();

                DateTime DaT;
                DateTime.TryParse(dgv.CurrentRow.Cells["datum"].FormattedValue.ToString(), out DaT);

                if (this.MdiParent != null)
                {
                    var mdiForm = this.MdiParent;
                    mdiForm.IsMdiContainer = true;
                    Robno.frmUlazneFakture childForm = new Robno.frmUlazneFakture();
                    childForm.MainForm = MainFormMenu;
                    childForm.godina_edit = DaT.Year;
                    childForm.broj_fakture_edit = broj;
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                    //this.Close();
                }
                else
                {
                    MainForm.broj_fakture_edit = broj;
                    MainForm.godina_edit = DaT.Year;
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
                Broj = "ulazna_faktura.broj='" + txtBroj.Text + "' AND ";
            }
            //if (chbSifra.Checked)
            //{
            //    Partner = "ufa.odrediste='" + txtPartner.Text + "' AND ";
            //}
            if (txtPartner.Text.Trim() != "")
            {
                string Str = txtPartner.Text;
                Partner = "ulazna_faktura.primatelj_naziv='" + Str + "' AND ";
            }
            if (chbOD.Checked)
            {
                DateStart = "ulazna_faktura.datum_izvrsenja >='" + dtpOD.Value.AddDays(-1).ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "ulazna_faktura.datum_izvrsenja <='" + dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND ";
            }
            if (chbValuta.Checked)
            {
                Valuta = "ulazna_faktura.valuta='" + cbValuta.SelectedValue.ToString() + "' AND ";
            }
            if (chbIzradio.Checked)
            {
                Izradio = "ulazna_faktura.id_zaposlenik='" + cbIzradio.SelectedValue + "' AND ";
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

            dgv.Rows.Clear();

            string sql = "SELECT ulazna_faktura.*," +
                " (SELECT coalesce(SUM(salda_konti.isplaceno),0) FROM salda_konti WHERE salda_konti.broj_dokumenta=ulazna_faktura.broj AND salda_konti.godina=ulazna_faktura.godina) as salda_konti_uplaceno " +
                " FROM ulazna_faktura " + filter + " ORDER BY CAST(broj as INT) DESC" + remote;

            DSfakture = classSQL.select(sql, "fakture");

            foreach (DataRow r in DSfakture.Tables[0].Rows)
            {
                bool bollHUB = false;
                string HUB_kreirano = "NE";
                if (r["hub_kreirano"].ToString() == "1")
                {
                    HUB_kreirano = "DA";
                }

                string StatusSalda = "";
                decimal placeno_salda_konti, ukupno_faktura;
                decimal.TryParse(r["salda_konti_uplaceno"].ToString(), out placeno_salda_konti);
                decimal.TryParse(r["iznos"].ToString(), out ukupno_faktura);

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

                int bbb;
                DateTime dttt;
                int.TryParse(r["broj"].ToString(), out bbb);
                DateTime.TryParse(r["datum_izvrsenja"].ToString(), out dttt);

                dgv.Rows.Add(bbb,
                    dttt,
                    r["valuta"].ToString(),
                    r["primatelj_naziv"].ToString(),
                    r["iznos"].ToString(),
                    HUB_kreirano, false, StatusSalda, r["id"].ToString(), r["godina"].ToString(), r["primatelj_sifra"].ToString());
            }
            PaintRows(dgv);
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
            //Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
            //rfak.dokumenat = "FAK";
            //rfak.ImeForme = "Fakture";
            //rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj fakture"].FormattedValue.ToString();
            //rfak.ShowDialog();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            //Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
            //rfak.dokumenat = "FAK";
            //rfak.ImeForme = "Fakture";
            //rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj fakture"].FormattedValue.ToString();
            //rfak.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void btnHub_Click(object sender, EventArgs e)
        {
            List<string> brojevi_fak = new List<string>();

            foreach (DataGridViewRow r in dgv.Rows)
            {
                if (r.Cells["kreiraj_hub"].FormattedValue.ToString().ToUpper() == "True".ToUpper())
                {
                    brojevi_fak.Add(r.Cells["broj"].FormattedValue.ToString() + ";" + r.Cells["godina"].FormattedValue.ToString());
                }
            }

            HUB.frmHub hh = new HUB.frmHub();
            hh.brojevi_fak = brojevi_fak;
            hh.ShowDialog();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            if (e.ColumnIndex == 7)
            {
                decimal u;
                DateTime d;
                decimal.TryParse(dgv.CurrentRow.Cells["Ukupno"].FormattedValue.ToString(), out u);
                DateTime.TryParse(dgv.CurrentRow.Cells["datum"].FormattedValue.ToString(), out d);

                Salda_konti.frmUnosSaldaKonti sk = new Salda_konti.frmUnosSaldaKonti();
                sk._dokumenat = "ULAZNA FAKTURA";
                sk._broj = dgv.CurrentRow.Cells["broj"].FormattedValue.ToString();
                //sk._id_ducan = dgv.CurrentRow.Cells["id_ducan"].FormattedValue.ToString();
                //sk._id_kasa = dgv.CurrentRow.Cells["id_kasa"].FormattedValue.ToString();
                sk._iznos = u;
                sk._id_partner = dgv.CurrentRow.Cells["sifra_partner"].FormattedValue.ToString();
                sk._id_skladiste = "";
                sk._godina = d.Year.ToString();
                sk.ShowDialog();
                pictureBox1_Click(sender, e);

                return;
            }
        }
    }
}