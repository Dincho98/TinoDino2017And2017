using PCPOS.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmOdjavaKomisione : Form
    {
        public frmOdjavaKomisione()
        {
            InitializeComponent();
        }

        private string skladiste_pocetno = "";
        private bool edit = false;
        private DataTable DT_Skladiste;
        private DataTable DT_stavke;
        public string broj_komisione_edit { get; set; }
        public frmMenu MainForm { get; set; }

        private void frmOdjavaKomisione_Load(object sender, EventArgs e)
        {
            SetSkladiste();
            txtBroj.Text = brojOdjave();
            ControlDisableEnable(1, 0, 0, 1, 0);
            this.Paint += new PaintEventHandler(Form1_Paint);
            if (broj_komisione_edit != null)
            {
                FillKomisione();
                ControlDisableEnable(0, 1, 1, 0, 1);
            }
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
        }

        private void SetSkladiste()
        {
            DT_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste").Tables[0];
            cbSkladiste.DataSource = DT_Skladiste;
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmOdjavaKomisione_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT broj FROM odjava_komisione WHERE  broj='" + txtBroj.Text + "'", "odjava_komisione").Tables[0];
                DeleteFields();
                if (DT.Rows.Count == 0)
                {
                    if (brojOdjave() == txtBroj.Text.Trim())
                    {
                        edit = false;
                        EnableDisable(true);
                        btnSveFakture.Enabled = false;
                        cbSkladiste.Select();
                        ControlDisableEnable(0, 1, 1, 0, 1);
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    broj_komisione_edit = txtBroj.Text;
                    FillKomisione();
                    EnableDisable(true);
                    edit = true;
                    cbSkladiste.Select();
                    ControlDisableEnable(0, 1, 1, 0, 1);
                }
            }
        }

        private void FillKomisione()
        {
            EnableDisable(true);
            DeleteFields();
            ControlDisableEnable(0, 1, 0, 0, 1);
            chbFakture.Checked = false;
            chbKasa.Checked = false;
            chbOtpremnice.Checked = false;
            chbRadniNalozi.Checked = false;
            chbSkladisnica.Checked = false;

            string sssql = "SELECT * FROM odjava_komisione WHERE broj='" + broj_komisione_edit + "'";
            DataTable DTheader = classSQL.select(sssql, "odjava_komisione").Tables[0];

            string skladisteID = DTheader.Rows[0]["id_skladiste"].ToString();

            txtBroj.Text = DTheader.Rows[0]["broj"].ToString();
            cbSkladiste.SelectedValue = Convert.ToInt16(skladisteID);
            dtpDatum.Value = Convert.ToDateTime(DTheader.Rows[0]["datum"].ToString());
            dtpOdDatuma.Value = Convert.ToDateTime(DTheader.Rows[0]["od_datuma"].ToString());
            dtpDoDatuma.Value = Convert.ToDateTime(DTheader.Rows[0]["do_datuma"].ToString());
            txtSifraPartnera.Text = DTheader.Rows[0]["id_partner"].ToString();
            rtbNapomena.Text = DTheader.Rows[0]["napomena"].ToString();
            skladiste_pocetno = DTheader.Rows[0]["id_skladiste"].ToString();
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + DTheader.Rows[0]["id_zaposlenik"].ToString() + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();

            DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraPartnera.Text + "'", "partners").Tables[0];
            if (DSpar.Rows.Count > 0)
            {
                txtPartnerIme.Text = DSpar.Rows[0][0].ToString();
            }

            if (DTheader.Rows[0]["izlazne_fakture"].ToString() == "DA")
            {
                chbFakture.Checked = true;
            }

            if (DTheader.Rows[0]["kasa"].ToString() == "DA")
            {
                chbKasa.Checked = true;
            }

            if (DTheader.Rows[0]["otpremnice"].ToString() == "DA")
            {
                chbOtpremnice.Checked = true;
            }

            if (DTheader.Rows[0]["radni_nalozi"].ToString() == "DA")
            {
                chbRadniNalozi.Checked = true;
            }

            if (DTheader.Rows[0]["skladisnica"].ToString() == "DA")
            {
                chbSkladisnica.Checked = true;
            }

            string sql = "SELECT " +
                " odjava_komisione_stavke.id_stavka," +
                " odjava_komisione_stavke.sifra," +
                " odjava_komisione_stavke.kolicina_prodano," +
                " odjava_komisione_stavke.nbc," +
                " odjava_komisione_stavke.vpc," +
                " '' as mpc," +
                " odjava_komisione_stavke.id_stavka_dokumenat," +
                " odjava_komisione_stavke.dokumenat," +
                " odjava_komisione_stavke.table_name," +
                " odjava_komisione_stavke.rabat," +
                " roba.naziv" +
                " FROM odjava_komisione_stavke " +
                " LEFT JOIN roba ON roba.sifra=odjava_komisione_stavke.sifra" +
                " WHERE odjava_komisione_stavke.broj='" + broj_komisione_edit + "'";

            DT_stavke = classSQL.select(sql, "promjena_cijene_stavke").Tables[0];

            string sifra;
            DataTable dtRoba;
            double vpc, mpc, porez, rabat;

            for (int i = 0; i < DT_stavke.Rows.Count; i++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;
                dgw.Select();
                dgw.CurrentCell = dgw.Rows[br].Cells[3];

                vpc = Math.Round(Convert.ToDouble(DT_stavke.Rows[i]["vpc"].ToString()), 3);
                rabat = Math.Round(Convert.ToDouble(DT_stavke.Rows[i]["rabat"].ToString()), 2);

                sifra = DT_stavke.Rows[i]["sifra"].ToString();
                dtRoba = classSQL.select("select porez from roba_prodaja where sifra='" + sifra + "'" +
                    " AND id_skladiste='" + skladisteID + "'", "roba_prodaja").Tables[0];

                if (dtRoba.Rows.Count > 0)
                {
                    mpc = vpc * (1 + Convert.ToDouble(dtRoba.Rows[0][0].ToString()) / 100);
                }
                else
                {
                    dtRoba = classSQL.select("select porez from roba where sifra='" + sifra + "'", "roba").Tables[0];
                    try
                    {
                        mpc = vpc * (1 + Convert.ToDouble(dtRoba.Rows[0][0].ToString()) / 100);
                    }
                    catch
                    {
                        mpc = vpc;
                    }
                }

                mpc *= (1 - rabat / 100);

                dgw.Rows[br].Cells[0].Value = dgw.RowCount;
                dgw.Rows[br].Cells["sifra"].Value = sifra;
                dgw.Rows[br].Cells["naziv"].Value = DT_stavke.Rows[i]["naziv"].ToString();
                dgw.Rows[br].Cells["kolicina"].Value = DT_stavke.Rows[i]["kolicina_prodano"].ToString();
                dgw.Rows[br].Cells["nbc"].Value = Math.Round(Convert.ToDouble(DT_stavke.Rows[i]["nbc"].ToString()), 2).ToString("#0.00");
                dgw.Rows[br].Cells["vpc"].Value = vpc.ToString("#0.000");
                dgw.Rows[br].Cells["mpc"].Value = Math.Round(mpc, 2).ToString("#0.00");
                dgw.Rows[br].Cells["id"].Value = DT_stavke.Rows[i]["id_stavka"].ToString();
                dgw.Rows[br].Cells["id_stavka"].Value = DT_stavke.Rows[i]["id_stavka_dokumenat"].ToString();
                dgw.Rows[br].Cells["dokumenat"].Value = DT_stavke.Rows[i]["dokumenat"].ToString();
                dgw.Rows[br].Cells["table"].Value = DT_stavke.Rows[i]["table_name"].ToString();
                dgw.Rows[br].Cells["rabat"].Value = Math.Round(Convert.ToDouble(DT_stavke.Rows[i]["rabat"].ToString()), 2).ToString("#0.00");// Convert.ToDouble("0").ToString("#0.00");
            }
            edit = true;
            PaintRows(dgw);
            btnPripremi.Enabled = false;
        }

        private void cbSkladiste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                dtpDatum.Select();
            }
        }

        private void dtpDatum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                dtpOdDatuma.Select();
            }
        }

        private void dtpOdDatuma_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                dtpDoDatuma.Select();
            }
        }

        private void dtpDoDatuma_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifraPartnera.Select();
            }
        }

        private void txtSifraPartnera_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtSifraPartnera.Text == "")
                {
                    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                    partnerTrazi.ShowDialog();
                    if (Properties.Settings.Default.id_partner != "")
                    {
                        DataSet partner = new DataSet();
                        partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                        if (partner.Tables[0].Rows.Count > 0)
                        {
                            txtSifraPartnera.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                            txtPartnerIme.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                            rtbNapomena.Select();
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtSifraPartnera.Select();
                            return;
                        }
                    }
                    else
                    {
                        txtSifraPartnera.Select();
                        return;
                    }
                }

                string Str = txtSifraPartnera.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtSifraPartnera.Text = "0";
                }

                DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraPartnera.Text + "'", "partners").Tables[0];
                if (DSpar.Rows.Count > 0)
                {
                    txtPartnerIme.Text = DSpar.Rows[0][0].ToString();
                    //txtSifraFakturirati.Text = txtSifraOdrediste.Text;
                    //txtSifraFakturirati_KeyDown(txtSifraOdrediste, e);

                    System.Windows.Forms.SendKeys.Send("{TAB}");
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void txtPartnerIme_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                rtbNapomena.Select();
            }
        }

        private void rtbNapomena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnPripremi.Select();
            }
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
            row.Height = 25;
        }

        private void ControlDisableEnable(int novi, int odustani, int spremi, int sve, int delAll)
        {
            if (novi == 0)
            {
                btnNoviUnos.Enabled = false;
            }
            else if (novi == 1)
            {
                btnNoviUnos.Enabled = true;
            }

            if (odustani == 0)
            {
                btnOdustani.Enabled = false;
            }
            else if (odustani == 1)
            {
                btnOdustani.Enabled = true;
            }

            if (spremi == 0)
            {
                btnSpremi.Enabled = false;
            }
            else if (spremi == 1)
            {
                btnSpremi.Enabled = true;
            }

            if (sve == 0)
            {
                btnSveFakture.Enabled = false;
            }
            else if (sve == 1)
            {
                btnSveFakture.Enabled = true;
            }
        }

        private void EnableDisable(bool x)
        {
            dtpDoDatuma.Enabled = x;
            dtpOdDatuma.Enabled = x;
            cbSkladiste.Enabled = x;
            dtpDatum.Enabled = x;
            rtbNapomena.Enabled = x;
            txtSifraPartnera.Enabled = x;
            //txtPartnerIme.Enabled = x;
            txtBroj.Enabled = !x;
            nmGodina.Enabled = !x;
        }

        private string brojOdjave()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj) FROM odjava_komisione", "odjava_komisione").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            txtBroj.Text = brojOdjave();
            EnableDisable(true);
            edit = false;
            btnPripremi.Enabled = true;
            ControlDisableEnable(0, 1, 1, 0, 1);
        }

        private void btnPripremi_Click(object sender, EventArgs e)
        {
            decimal dec_parse;
            if (!Decimal.TryParse(txtSifraPartnera.Text, out dec_parse))
            {
                MessageBox.Show("Greška kod upisa partnera.", "Greška!");
                return;
            }

            dgw.Rows.Clear();

            Racuni();

            Fakture();

            Otpremnice();

            RadniNalog();

            Medjuskladisnice();

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Format = "N2";
            dgw.Columns["nbc"].DefaultCellStyle = style;
            dgw.Columns["rabat"].DefaultCellStyle = style;
            dgw.Columns["mpc"].DefaultCellStyle = style;
            style.Format = "N3";
            dgw.Columns["vpc"].DefaultCellStyle = style;

            dgw.Columns["vpc"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["vpc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["rabat"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["rabat"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["nbc"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["nbc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["mpc"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["mpc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void Racuni()
        {
            if (chbKasa.Checked)
            {
                string sql = "SELECT racun_stavke.kolicina,racun_stavke.sifra_robe,racun_stavke.nbc,racun_stavke.rabat," +
                    "racun_stavke.vpc,racun_stavke.porez,roba.naziv,racun_stavke.id_stavka" +
                    " FROM racun_stavke" +
                    " LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa" +
                    " LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe AND roba.id_partner='" + txtSifraPartnera.Text + "'" +
                    " WHERE cast(racuni.datum_racuna as date) >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd") + "' AND cast(racuni.datum_racuna as date) <= '" +
                    dtpDoDatuma.Value.ToString("yyyy-MM-dd") + "' AND racun_stavke.odjava IS NULL" +
                    " AND racun_stavke.id_skladiste='" + cbSkladiste.SelectedValue + "'  AND roba.oduzmi='DA'";
                DataTable DTracun = classSQL.select(sql, "racun_stavke").Tables[0];

                double vpc, porez, rabat;

                for (int i = 0; i < DTracun.Rows.Count; i++)
                {
                    vpc = Math.Round(Convert.ToDouble(DTracun.Rows[i]["vpc"].ToString()), 3);
                    porez = Math.Round(Convert.ToDouble(DTracun.Rows[i]["porez"].ToString()), 2);
                    rabat = Math.Round(Convert.ToDouble(DTracun.Rows[i]["rabat"].ToString()), 2);

                    string nabPrice = Convert.ToDecimal(DTracun.Rows[i]["nbc"].ToString()) == 0 ? VracajNabavnu(DTracun.Rows[i]["sifra_robe"].ToString()) : DTracun.Rows[i]["nbc"].ToString();

                    dgw.Rows.Add(
                        dgw.Rows.Count + 1, DTracun.Rows[i]["sifra_robe"].ToString(),
                        DTracun.Rows[i]["naziv"].ToString(),
                        "Kasa",
                        Math.Round(Convert.ToDouble(DTracun.Rows[i]["kolicina"].ToString()), 3).ToString("#0.000"),
                        Math.Round(Convert.ToDouble(nabPrice), 2).ToString("#0.00"),
                        (Class.Postavke.uzmi_rabat_u_odjavi_komisije ? rabat.ToString("#0.00") : "0,00"), //rabat.ToString("#0.00"),
                        Math.Round(vpc * (1 + porez / 100) * (1 - rabat / 100), 2).ToString("#0.00"),
                        Math.Round(Convert.ToDouble(DTracun.Rows[i]["vpc"].ToString()), 2).ToString("#0.00"),
                        DTracun.Rows[i]["id_stavka"].ToString(),
                        "racun_stavke");
                }
            }
        }

        private void Otpremnice()
        {
            if (chbOtpremnice.Checked)
            {
                string sql3 = "SELECT otpremnica_stavke.kolicina,otpremnica_stavke.sifra_robe AS sifra," +
                    "otpremnica_stavke.nbc,otpremnica_stavke.vpc,otpremnica_stavke.porez,otpremnica_stavke.rabat," +
                    "otpremnica_stavke.id_stavka,roba.naziv" +
                    " FROM otpremnica_stavke" +
                    " LEFT JOIN roba ON roba.sifra=otpremnica_stavke.sifra_robe AND roba.id_partner='" + txtSifraPartnera.Text + "'" +
                    " LEFT JOIN otpremnice ON otpremnice.broj_otpremnice=otpremnica_stavke.broj_otpremnice" +
                    " AND otpremnice.id_skladiste=otpremnica_stavke.id_skladiste" +
                    " WHERE cast(otpremnice.datum as date) >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd") + "'" +
                    " AND cast(otpremnice.datum as date) <= '" + dtpDoDatuma.Value.ToString("yyyy-MM-dd") + "'" +
                    " AND otpremnica_stavke.odjava IS NULL AND otpremnice.id_skladiste='" + cbSkladiste.SelectedValue + "'" +
                    " AND roba.oduzmi='DA'";
                DataTable DTotp = classSQL.select(sql3, "otpremnica_stavke").Tables[0];

                double vpc, porez, rabat;

                for (int i = 0; i < DTotp.Rows.Count; i++)
                {
                    vpc = Math.Round(Convert.ToDouble(DTotp.Rows[i]["vpc"].ToString()), 3);
                    porez = Math.Round(Convert.ToDouble(DTotp.Rows[i]["porez"].ToString()), 2);
                    rabat = Math.Round(Convert.ToDouble(DTotp.Rows[i]["rabat"].ToString()), 2);

                    string nabPrice = Convert.ToDecimal(DTotp.Rows[i]["nbc"].ToString()) == 0 ? VracajNabavnu(DTotp.Rows[i]["sifra"].ToString()) : DTotp.Rows[i]["nbc"].ToString();

                    dgw.Rows.Add(
                        dgw.Rows.Count + 1,
                        DTotp.Rows[i]["sifra"].ToString(),
                        DTotp.Rows[i]["naziv"].ToString(),
                        "Otpremnica",
                        Math.Round(Convert.ToDouble(DTotp.Rows[i]["kolicina"].ToString()), 3).ToString("#0.000"),
                        Math.Round(Convert.ToDouble(nabPrice), 2).ToString("#0.00"),
                        (Class.Postavke.uzmi_rabat_u_odjavi_komisije ? rabat.ToString("#0.00") : "0,00"), //rabat.ToString("#0.00"),
                        Math.Round(vpc * (1 + porez / 100) * (1 - rabat / 100), 2).ToString("#0.00"),
                        Math.Round(Convert.ToDouble(DTotp.Rows[i]["vpc"].ToString()), 2).ToString("#0.000"),
                        DTotp.Rows[i]["id_stavka"].ToString(),
                        "otpremnica_stavke");
                }
            }
        }

        private void RadniNalog()
        {
            if (chbRadniNalozi.Checked)
            {
                string sql4 = "SELECT radni_nalog_normativ.kolicina, radni_nalog_normativ.sifra, radni_nalog_normativ.nbc," +
                    " radni_nalog_normativ.pdv, radni_nalog_normativ.id, radni_nalog_normativ.vpc, radni_nalog_normativ.id, roba.naziv" +
                    " FROM radni_nalog_normativ " +
                    " LEFT JOIN roba ON roba.sifra=radni_nalog_normativ.sifra AND roba.id_partner='" + txtSifraPartnera.Text + "'" +
                    " LEFT JOIN radni_nalog ON radni_nalog.broj_naloga=radni_nalog_normativ.broj" +
                    " WHERE cast(radni_nalog.datum_naloga as date) >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd") + "'" +
                    " AND cast(radni_nalog.datum_naloga as date) <= '" + dtpDoDatuma.Value.ToString("yyyy-MM-dd") + "'" +
                    " AND radni_nalog_normativ.odjava IS NULL AND id_skladiste='" + cbSkladiste.SelectedValue + "'" +
                    " AND roba.oduzmi='DA'";

                DataTable DTrn = classSQL.select(sql4, "radni_nalog_stavke").Tables[0];

                double vpc, mpc, porez;

                for (int i = 0; i < DTrn.Rows.Count; i++)
                {
                    vpc = Math.Round(Convert.ToDouble(DTrn.Rows[i]["vpc"].ToString()), 3);
                    porez = Math.Round(Convert.ToDouble(DTrn.Rows[i]["pdv"].ToString()), 2);

                    string nabPrice = Convert.ToDecimal(DTrn.Rows[i]["nbc"].ToString()) == 0 ? VracajNabavnu(DTrn.Rows[i]["sifra"].ToString()) : DTrn.Rows[i]["nbc"].ToString();

                    dgw.Rows.Add(
                        dgw.Rows.Count + 1,
                        DTrn.Rows[i]["sifra"].ToString(),
                        DTrn.Rows[i]["naziv"].ToString(),
                        "Radni nalog",
                        Math.Round(Convert.ToDouble(DTrn.Rows[i]["kolicina"].ToString()), 3).ToString("#0.000"),
                        Math.Round(Convert.ToDouble(nabPrice), 2).ToString("#0.00"),
                        "0,00", //"0,00", //
                        Math.Round(vpc * (1 + porez / 100), 2).ToString("#0.00"),
                        vpc.ToString("#0.000"),
                        DTrn.Rows[i]["id"].ToString(),
                        "radni_nalog_normativ");
                }
            }
        }

        private void Fakture()
        {
            if (chbFakture.Checked)
            {
                string sql2 = "SELECT faktura_stavke.kolicina,faktura_stavke.sifra,faktura_stavke.nbc," +
                    "faktura_stavke.rabat,faktura_stavke.vpc,faktura_stavke.porez,faktura_stavke.id_stavka,roba.naziv" +
                    " FROM faktura_stavke " +
                    " LEFT JOIN roba ON roba.sifra=faktura_stavke.sifra AND roba.id_partner='" + txtSifraPartnera.Text + "'" +
                    " LEFT JOIN fakture ON fakture.broj_fakture=faktura_stavke.broj_fakture AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa" +
                    " WHERE cast(fakture.date as date) >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd") + "'" +
                    " AND cast(fakture.date as date) <= '" +
                    dtpDoDatuma.Value.ToString("yyyy-MM-dd") + "'" +
                    " AND faktura_stavke.odjava IS NULL AND faktura_stavke.id_skladiste='" + cbSkladiste.SelectedValue + "' AND roba.oduzmi='DA'";
                DataTable DTfakture = classSQL.select(sql2, "faktura_stavke").Tables[0];

                double vpc, rabat, porez;
                for (int i = 0; i < DTfakture.Rows.Count; i++)
                {
                    vpc = Math.Round(Convert.ToDouble(DTfakture.Rows[i]["vpc"].ToString()), 3);
                    porez = Math.Round(Convert.ToDouble(DTfakture.Rows[i]["porez"].ToString()), 2);
                    rabat = Math.Round(Convert.ToDouble(DTfakture.Rows[i]["rabat"].ToString()), 2);

                    if (DTfakture.Rows[i]["nbc"].ToString() == "") DTfakture.Rows[i].SetField("nbc", "0,00");
                    string nabPrice = Convert.ToDecimal(DTfakture.Rows[i]["nbc"].ToString()) == 0 ? VracajNabavnu(DTfakture.Rows[i]["sifra"].ToString()) : DTfakture.Rows[i]["nbc"].ToString();

                    dgw.Rows.Add(
                        dgw.Rows.Count + 1,
                        DTfakture.Rows[i]["sifra"].ToString(),
                        DTfakture.Rows[i]["naziv"].ToString(),
                        "Fakture",
                        Math.Round(Convert.ToDouble(DTfakture.Rows[i]["kolicina"].ToString()), 3).ToString("#0.000"),
                        Math.Round(Convert.ToDouble(nabPrice), 2).ToString("#0.00"),
                        (Class.Postavke.uzmi_rabat_u_odjavi_komisije ? rabat.ToString("#0.00") : "0,00"), //rabat.ToString("#0.00"),
                        Math.Round(vpc * (1 + porez / 100) * (1 - rabat / 100), 2).ToString("#0.00"),
                        vpc.ToString("#0.000"),
                        DTfakture.Rows[i]["id_stavka"].ToString(),
                        "faktura_stavke");
                }
            }
        }

        private void Medjuskladisnice()
        {
            if (chbSkladisnica.Checked)
            {
                string sql3 = "SELECT meduskladisnica_stavke.kolicina, meduskladisnica_stavke.sifra," +
                " meduskladisnica_stavke.nbc, meduskladisnica_stavke.vpc, meduskladisnica_stavke.pdv, meduskladisnica_stavke.id_stavka,roba.naziv " +
                " FROM meduskladisnica_stavke" +
                " LEFT JOIN roba ON roba.sifra=meduskladisnica_stavke.sifra AND roba.id_partner='" + txtSifraPartnera.Text + "'" +
                " LEFT JOIN meduskladisnica ON meduskladisnica.broj=meduskladisnica_stavke.broj  AND meduskladisnica.id_skladiste_od=meduskladisnica_stavke.iz_skladista" +
                " WHERE cast(meduskladisnica.datum as date) >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd") + "'" +
                " AND cast(meduskladisnica.datum as date) <= '" +
                dtpDoDatuma.Value.ToString("yyyy-MM-dd") + "' AND meduskladisnica.id_skladiste_od='" +
                cbSkladiste.SelectedValue + "' AND meduskladisnica_stavke.odjava IS NULL AND roba.oduzmi='DA'";

                DataTable DTmds = classSQL.select(sql3, "meduskladisnica_stavke").Tables[0];

                double vpc, mpc, porez;

                for (int i = 0; i < DTmds.Rows.Count; i++)
                {
                    vpc = Math.Round(Convert.ToDouble(DTmds.Rows[i]["vpc"].ToString()), 3);
                    porez = Math.Round(Convert.ToDouble(DTmds.Rows[i]["pdv"].ToString()), 2);

                    string nabPrice = Convert.ToDecimal(DTmds.Rows[i]["nbc"].ToString()) == 0 ? VracajNabavnu(DTmds.Rows[i]["sifra"].ToString()) : DTmds.Rows[i]["nbc"].ToString();

                    dgw.Rows.Add(
                        dgw.Rows.Count + 1,
                        DTmds.Rows[i]["sifra"].ToString(),
                        DTmds.Rows[i]["naziv"].ToString(),
                        "Međuskladišnica",
                        Math.Round(Convert.ToDouble(DTmds.Rows[i]["kolicina"].ToString()), 3).ToString("#0.000"),
                        Math.Round(Convert.ToDouble(nabPrice), 2).ToString("#0.00"),
                        "0,00", //
                        Math.Round(vpc * (1 + porez / 100), 2).ToString("#0.00"),
                        vpc.ToString("#0.000"),
                        DTmds.Rows[i]["id_stavka"].ToString(),
                        "meduskladisnica_stavke");
                }
            }
        }

        private string VracajNabavnu(string sifra)
        {
            DataTable DT = classSQL.select("SELECT nc FROM roba WHERE sifra='" + sifra + "'", "roba").Tables[0];
            string vrati = DT.Rows.Count > 0 ? DT.Rows[0][0].ToString() : "0";
            return vrati;
        }

        private string dg(int row, string cell)
        {
            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            DataTable DTbool = classSQL.select("SELECT broj FROM odjava_komisione WHERE broj = '" + txtBroj.Text + "'", "odjava_komisione").Tables[0];

            string sql = "";

            string chFaktura = "NE";
            string chKasa = "NE";
            string chOtpremnice = "NE";
            string chRadnialozi = "NE";
            string chSkladisnica = "NE";

            if (chbFakture.Checked)
            {
                chFaktura = "DA";
            }

            if (chbKasa.Checked)
            {
                chKasa = "DA";
            }

            if (chbOtpremnice.Checked)
            {
                chOtpremnice = "DA";
            }

            if (chbRadniNalozi.Checked)
            {
                chRadnialozi = "DA";
            }

            if (chbSkladisnica.Checked)
            {
                chSkladisnica = "DA";
            }

            decimal dec_parse;
            if (!Decimal.TryParse(txtSifraPartnera.Text, out dec_parse) || txtSifraPartnera.Text == "")
            {
                MessageBox.Show("Greška kod upisa šifre partnera.", "Greška"); return;
            }

            if (DTbool.Rows.Count == 0)
            {
                sql = "INSERT INTO odjava_komisione (broj,godina,datum,od_datuma,do_datuma,id_partner,napomena" +
                    ",izlazne_fakture,kasa,otpremnice,radni_nalozi,skladisnica,id_skladiste,id_zaposlenik) VALUES (" +
                     " '" + txtBroj.Text + "'," +
                     " '" + nmGodina.Value.ToString() + "'," +
                     " '" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                     " '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                     " '" + dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                     " '" + txtSifraPartnera.Text + "'," +
                     " '" + rtbNapomena.Text + "'," +
                     " '" + chFaktura + "'," +
                     " '" + chKasa + "'," +
                     " '" + chOtpremnice + "'," +
                     " '" + chRadnialozi + "'," +
                     " '" + chSkladisnica + "'," +
                     " '" + cbSkladiste.SelectedValue.ToString() + "'," +
                     " '" + Properties.Settings.Default.id_zaposlenik + "'" +
                     ")";
                provjera_sql(classSQL.insert(sql));
            }
            else
            {
                sql = "UPDATE odjava_komisione SET" +
                    " broj='" + txtBroj.Text + "'," +
                    "godina='" + nmGodina.Value.ToString() + "'," +
                    "datum='" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "od_datuma='" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "do_datuma='" + dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "id_partner='" + txtSifraPartnera.Text + "'," +
                    "napomena='" + rtbNapomena.Text + "'," +
                    "id_skladiste='" + cbSkladiste.SelectedValue.ToString() + "'," +
                    "izlazne_fakture='" + chFaktura + "'," +
                    "kasa='" + chKasa + "'," +
                    "otpremnice='" + chOtpremnice + "'," +
                    "radni_nalozi='" + chRadnialozi + "'," +
                    "skladisnica='" + chSkladisnica + "' WHERE broj='" + txtBroj.Text + "'";
                provjera_sql(classSQL.update(sql));
            }

            string sql_odjava = "";
            string ssql = "";
            for (int i = 0; i < dgw.RowCount; i++)
            {
                sql_odjava = "UPDATE " + dgw.Rows[i].Cells["table"].FormattedValue.ToString() + " SET " +
                 " odjava='1' WHERE id_stavka='" + dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString() + "'";
                provjera_sql(classSQL.update(sql_odjava));

                if (dgw.Rows[i].Cells["id"].FormattedValue.ToString() == "")
                {
                    ssql = "INSERT INTO odjava_komisione_stavke (broj,sifra,kolicina_prodano,vpc,nbc,id_stavka_dokumenat," +
                        "dokumenat,table_name,rabat) VALUES (" +
                        "'" + txtBroj.Text + "'," +
                        "'" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'," +
                        "'" + dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString() + "'," +
                        "'" + dgw.Rows[i].Cells["vpc"].FormattedValue.ToString() + "'," +
                        "'" + dgw.Rows[i].Cells["nbc"].FormattedValue.ToString() + "'," +
                        "'" + dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString() + "'," +
                        "'" + dgw.Rows[i].Cells["dokumenat"].FormattedValue.ToString() + "'," +
                        "'" + dgw.Rows[i].Cells["table"].FormattedValue.ToString() + "'," +
                        "'" + dgw.Rows[i].Cells["rabat"].FormattedValue.ToString() + "'" +
                        ")";
                    provjera_sql(classSQL.insert(ssql));
                }
                else
                {
                    // string aa = dg(i, "id_stavka");
                    // DataRow[] dataROW = DT_stavke.Select("id_stavka = " + dg(i, "id_stavka"));

                    ssql = "UPDATE odjava_komisione_stavke SET" +
                        " broj='" + txtBroj.Text + "'," +
                        " sifra='" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'," +
                        " kolicina_prodano='" + dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString() + "'," +
                        " vpc='" + dgw.Rows[i].Cells["vpc"].FormattedValue.ToString() + "'," +
                        " nbc='" + dgw.Rows[i].Cells["nbc"].FormattedValue.ToString() + "'," +
                        " id_stavka_dokumenat='" + dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString() + "'," +
                        " dokumenat='" + dgw.Rows[i].Cells["dokumenat"].FormattedValue.ToString() + "'," +
                        " rabat='" + dgw.Rows[i].Cells["rabat"].FormattedValue.ToString() + "'," +
                        " table_name='" + dgw.Rows[i].Cells["table"].FormattedValue.ToString() + "'" +
                        " WHERE id_stavka='" + dg(i, "id") + "'";

                    provjera_sql(classSQL.update(ssql));
                }
            }

            CreateJSON();

            EnableDisable(false);
            DeleteFields();
            txtBroj.Text = brojOdjave();
            edit = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
            MessageBox.Show("Spremljeno.");
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateJSON()
        {
            if(dgw.Rows.Count > 0)
            {
                List<FakturaStavke> stavkeList = new List<FakturaStavke>();
                string maxBroj = Global.Database.GetMaxBroj("fakture", "broj_fakture");
                foreach(DataGridViewRow row in dgw.Rows)
                {
                    decimal.TryParse(row.Cells["kolicina"].FormattedValue.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal kolicina);
                    decimal.TryParse(row.Cells["nbc"].FormattedValue.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal nbc);
                    decimal.TryParse(row.Cells["vpc"].FormattedValue.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal vpc);
                    decimal.TryParse(row.Cells["rabat"].FormattedValue.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal rabat);

                    DataTable DTroba = Global.Database.GetRoba(row.Cells["sifra"].FormattedValue.ToString());
                    decimal.TryParse(DTroba.Rows[0]["porez"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal porez);
                    decimal.TryParse(DTroba.Rows[0]["mpc"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal mpc);

                    FakturaStavke stavka = new FakturaStavke
                    {
                        BrojFakture = Convert.ToInt32(maxBroj),
                        SifraRobe = row.Cells["sifra"].FormattedValue.ToString(),
                        Naziv = row.Cells["naziv"].FormattedValue.ToString(),
                        Kolicina = kolicina,
                        Nbc = nbc,
                        Mpc = mpc,
                        Vpc = vpc,
                        Porez = porez,
                        Rabat = rabat
                    };

                    stavkeList.Add(stavka);
                }

                if(stavkeList.Count > 0)
                {
                    Faktura faktura = new Faktura
                    {
                        BrojFakture = Convert.ToInt32(maxBroj),
                        IdOdrediste = Convert.ToInt32(txtSifraPartnera.Text),
                        IdFakturirati = Convert.ToInt32(txtSifraPartnera.Text),
                        IdSkladiste = Convert.ToInt32(cbSkladiste.SelectedValue.ToString()),
                        Datum = dtpDatum.Value.ToString("dd-MM-yyyy"),
                        DatumDVO = dtpDatum.Value.ToString("dd-MM-yyyy"),
                        DatumValute = dtpDatum.Value.ToString("dd-MM-yyyy"),
                        IdZaposlenik = Convert.ToInt32(Properties.Settings.Default.id_zaposlenik),
                        Napomena = rtbNapomena.Text,
                        Stavke = stavkeList
                    };

                    if (faktura != null)
                        SaveJSON(faktura);
                }
            }
        }

        private void SaveJSON(Faktura faktura)
        {
            string pathResult = AppDomain.CurrentDomain.BaseDirectory + $"JSON\\odjava_{txtBroj.Text}_{DateTime.Now.ToString("ddMMyyyyHHmm")}.txt";
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string jsonString = serializer.Serialize(faktura);
            using (StreamWriter writer = File.CreateText(pathResult))
            {
                writer.WriteLine(jsonString);
            }
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            btnPripremi.Enabled = true;
            EnableDisable(false);
            DeleteFields();
            txtBroj.Text = brojOdjave();
            edit = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private void DeleteFields()
        {
            txtPartnerIme.Text = "";
            txtSifraPartnera.Text = "";
            dgw.Rows.Clear();
            rtbNapomena.Text = "";
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            Robno.frmSveOdjave_komisione srn = new frmSveOdjave_komisione();
            srn.MainForm = this;
            srn.ShowDialog();
            if (broj_komisione_edit != null)
            {
                DeleteFields();
                FillKomisione();
                ControlDisableEnable(0, 1, 1, 0, 1);
                EnableDisable(true);

                edit = true;
            }
        }

        private decimal rabat1;
        private decimal nabavna1;

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.Rows.Count < 1)
                return;
            dgw.BeginEdit(true);
            try
            {
                nabavna1 = Convert.ToDecimal(dgw.CurrentRow.Cells["nbc"].FormattedValue.ToString());
                rabat1 = Convert.ToDecimal(dgw.CurrentRow.Cells["rabat"].FormattedValue.ToString());
            }
            catch { }
        }

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.CurrentCell.ColumnIndex == 6)
            {
                try
                {
                    decimal nabavna = Convert.ToDecimal(dgw.CurrentRow.Cells["nbc"].FormattedValue.ToString());
                    decimal rabat = Convert.ToDecimal(dgw.CurrentRow.Cells["rabat"].FormattedValue.ToString());
                    if (rabat1 != rabat || nabavna1 != nabavna) dgw.CurrentRow.Cells["nbc"].Value = (nabavna - (nabavna * rabat / 100)).ToString("#0.00");
                }
                catch
                {
                }
            }
            else if (dgw.CurrentCell.ColumnIndex == 5)
            {
                try
                {
                    dgw.CurrentRow.Cells["nbc"].Value = Math.Round(Convert.ToDouble(dgw.CurrentRow.Cells["nbc"].Value), 2).ToString("#0.00");
                }
                catch { }
            }
            else if (dgw.CurrentCell.ColumnIndex == 7)
            {
                try
                {
                    dgw.CurrentRow.Cells["vpc"].Value = Math.Round(Convert.ToDouble(dgw.CurrentRow.Cells["vpc"].Value), 2).ToString("#0.00");
                }
                catch { }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtSifraPartnera.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartnerIme.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    //txtSifraFakturirati.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    //txtPartnerNaziv1.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString(); cbVD.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }
    }
}