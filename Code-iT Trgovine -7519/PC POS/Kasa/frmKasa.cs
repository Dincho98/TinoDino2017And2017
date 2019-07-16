using Microsoft.VisualBasic;
using PCPOS.Class;
using PCPOS.Util;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Media;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmKasa : Form
    {

        private UDS UDSAPI;

        private PartnerCompanyInfoType udsCompany;
        private CustomerInfoType udsCustomer;
        private bool UDSAPI_APPLY_DISCOUNT;
        private decimal UDSAPI_DISCOUNT_BASE = 0;
        private decimal UDSAPI_MAX_SCORES_DISCOUNT = 0;
        private decimal UDSAPI_SCORES_TO_SUBSTRACT_FROM_CUSTOMER_ACCOUNT = 0;
        private int CODE = 0;
        private Button btnKoristiUds;
        public frmKasa()
        {
            InitializeComponent();
        }

        public bool karticaKupcaB { get; set; }
        public string karticaKupcaS { get; set; }

        public string IznosKartica { get; set; }
        public string IznosGotovina { get; set; }
        public string IznosBon { get; set; }
        public string IznosVirman { get; set; } //trenutno nije implementirano
        public string DobivenoGotovina { get; set; }
        public string placanje { get; set; }
        public string odabranoSkladiste = "1";

        //public bool vecSelektiran = true;
        private string spVagaValue = "";

        private string oldVagaValue = "";
        private int vagaInterval = 0;

        public string id_ducan { get; set; }
        public string id_kasa { get; set; }
        public string sifra_skladiste { get; set; }
        private DataTable DTpostavkePrinter;
        private DataSet DS_Skladiste;
        private DataSet DSpostavke;
        private DataTable DTpromocije;
        private DataTable DTpromocije1;
        private DataTable DTsend = new DataTable();
        public DataTable DTrezervacija = null;
        private double ukupno = 0;
        private double ukupnoBezRabata = 0;
        public string brRac;
        private string blagajnik_ime;
        private string sifraPartnera = "0";
        private string ZadnjiRacun;
        public frmMenu MainForm { get; set; }
        private DataTable DTb = new DataTable();
        private string blagajna = "";

        // Dodano za apsolutni popust
        private bool popustABS;

        private DataTable DTpodaci;
        private object[] objDrugiPorezi = new object[5];
        private Korisno korisno = new Korisno();

        /****************************SINKRONIZACIJA SA WEB-OM*****************/
        private BackgroundWorker bgSinkronizacija = null;
        private synWeb.synPokretac PokretacSinkronizacije = new synWeb.synPokretac();
        /****************************SINKRONIZACIJA SA WEB-OM*****************/

        private void frmKasa_Load(object sender, EventArgs e)
        {

            if (Class.Postavke.UDSGame)
            {

                UDSAPI = new UDS(Class.Postavke.UDSGameApiKey);
                udsCompany = UDSAPI.getCompany();

                if (udsCompany != null)
                {
                    UDSAPI_APPLY_DISCOUNT = (udsCompany.baseDiscountPolicy == "APPLY_DISCOUNT" ? true : false);
                    UDSAPI_DISCOUNT_BASE = udsCompany.marketingSettings.discountBase;
                    UDSAPI_MAX_SCORES_DISCOUNT = udsCompany.marketingSettings.maxScoresDiscount;
                    if (UDSAPI_MAX_SCORES_DISCOUNT > 99)
                    {
                        MessageBox.Show("Nije dozvoljeno fiskaliziranje računa s 0 kn, stoga je maximalni dozvoljeni postotak popusta promijenjen na 99%.");
                        UDSAPI_MAX_SCORES_DISCOUNT = 99;
                    }

                    if (UDSAPI_APPLY_DISCOUNT)
                    {
                        if (UDSAPI_DISCOUNT_BASE > UDSAPI_MAX_SCORES_DISCOUNT)
                        {
                            MessageBox.Show("Osnovni postotak popusta je veči od maksimalnog dozvoljenog postotka.\r\nOsnovni postotak je promjenjen u maksimalni dozvoljeni postotak.");
                            UDSAPI_DISCOUNT_BASE = UDSAPI_MAX_SCORES_DISCOUNT;
                        }
                    }
                }
                btnDodajNaPartnera.Text = "UDS Game";
                btnDodajNaPartnera.Height = 45;

                btnKoristiUds = new Button();
                btnKoristiUds.Width = btnDodajNaPartnera.Width;
                btnKoristiUds.Height = btnDodajNaPartnera.Height;
                btnKoristiUds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
                btnKoristiUds.BackColor = System.Drawing.Color.White;
                btnKoristiUds.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                btnKoristiUds.Cursor = System.Windows.Forms.Cursors.Hand;
                btnKoristiUds.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(79)))));
                btnKoristiUds.FlatAppearance.CheckedBackColor = btnDodajNaPartnera.FlatAppearance.CheckedBackColor;
                btnKoristiUds.FlatAppearance.MouseDownBackColor = btnDodajNaPartnera.FlatAppearance.MouseDownBackColor;
                btnKoristiUds.FlatAppearance.MouseOverBackColor = btnDodajNaPartnera.FlatAppearance.MouseOverBackColor;
                btnKoristiUds.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btnKoristiUds.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                btnKoristiUds.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
                btnKoristiUds.Location = new System.Drawing.Point(196, 520);
                btnKoristiUds.TabIndex = 27;
                btnKoristiUds.Text = "Odustani od kartica";
                btnKoristiUds.UseVisualStyleBackColor = false;
                btnKoristiUds.Click += btnKoristiUds_Click;

                this.Controls.Add(btnKoristiUds);

                //else
                //{
                //btnOdustaniKartica.Enabled = false;
                //btnOdustaniKartica.Visible = false;
                //}
            }

            btnGotovina.Enabled = Class.Postavke.maloprodaja_naplata_gotovina_button_show;
            //btnGotovina.Visible = Class.Postavke.maloprodaja_naplata_gotovina_button_show;

            btnKartica.Enabled = Class.Postavke.maloprodaja_naplata_kartica_button_show;
            //btnKartica.Visible = Class.Postavke.maloprodaja_naplata_kartica_button_show;

            lblVaga.Visible = Korisno.vaga;
            lblVagaValue.Visible = Korisno.vaga;

            if (Korisno.vaga)
            {
                if (spVaga.IsOpen) spVaga.Close();
                spVaga.Open();
                timRefreshVaga.Start();
            }

            /****************************SINKRONIZACIJA SA WEB-OM*****************/
            bgSinkronizacija = new BackgroundWorker();
            bgSinkronizacija.DoWork += new DoWorkEventHandler(bgSinkronizacija_DoWork);
            bgSinkronizacija.WorkerSupportsCancellation = true;
            /****************************SINKRONIZACIJA SA WEB-OM*****************/

            if (variable.Zaposlenici().ToString().ToLower() != "administrator" || variable.Zaposlenici().ToString().ToLower() != "blagajnik")
            {
                this.MinimizeBox = false;
                this.MaximizeBox = false;
            }
            DTpartner_ = null;
            DSpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke");
            DTb = classSQL.select("SELECT ime_blagajne FROM blagajna WHERE id_blagajna='" + DSpostavke.Tables[0].Rows[0]["default_blagajna"].ToString() + "'", "blagajna").Tables[0];
            blagajna = DTb.Rows.Count > 0 ? DTb.Rows[0][0].ToString() : DSpostavke.Tables[0].Rows[0]["default_blagajna"].ToString();

            if (DSpostavke.Tables[0].Rows.Count > 0)
            {
                try
                {
                    id_kasa = DSpostavke.Tables[0].Rows[0]["default_blagajna"].ToString();
                }
                catch
                {
                    MessageBox.Show("Kasa nije odabrana. Provjerite postavke programa.", "Upozorenje!");
                    id_kasa = "0";
                }
                try
                {
                    id_ducan = DSpostavke.Tables[0].Rows[0]["default_ducan"].ToString();
                }
                catch
                {
                    MessageBox.Show("Dućan nije odabran. Provjerite postavke programa.", "Upozorenje!");
                    id_ducan = "0";
                }
                try
                {
                    sifra_skladiste = DSpostavke.Tables[0].Rows[0]["default_skladiste"].ToString();
                }
                catch
                {
                    MessageBox.Show("Skladište nije odabrano. Provjerite postavke programa.", "Upozorenje!");
                    sifra_skladiste = "0";
                }
            }
            else
            {
                MessageBox.Show("Kasa, dućan ili skladište nisu odabrani.", "Upozorenje!");
            }

            SetSmjene();

            SetSkladiste();

            DTpromocije1 = classSQL.select("SELECT * FROM promocija1", "promocija1").Tables[0];
            brRac = brojRacuna();
            lblBrojRac.Text = "Broj računa: " + brRac + "/" + DateTime.Now.Year;
            blagajnik_ime = classSQL.select("SELECT ime + ' ' + prezime FROM zaposlenici WHERE id_zaposlenik = '" +
                Properties.Settings.Default.id_zaposlenik + "'",
                "zaposlenici").Tables[0].Rows[0][0].ToString();
            SetSize();
            DTpromocije = classSQL.select("SELECT * FROM promocije WHERE do_datuma >='" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") +
                "' AND od_datuma <= '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'", "promocije").Tables[0];

            DTpostavkePrinter = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];

            PaintRows(dgv);

            panTastatura.Visible = true;

            SrediSirinuHeadera();

            DTpodaci = classSQL.select_settings("SELECT * FROM podaci_tvrtka WHERE id='1'", "podaci_tvrtka").Tables[0];
            popustABS = DTpodaci.Rows[0]["oib"].ToString() == "67660751355";
            if (popustABS)
            {
                btnPopust.Visible = popustABS;
                btnPopustSve.Visible = !popustABS;
            }

            if (DTpodaci.Rows[0]["oib"].ToString() == "67521709619")
            {
                chbNeObracunavajPorezPrirez.Visible = true;
                label13.Visible = true;
            }

            // Rezervacija
            if (DTrezervacija != null)
                LoadReservation();
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadReservation()
        {
            DataRow row = DTrezervacija.Rows[0];

            int index = dgv.Rows.Add();

            dgv.Rows[index].Cells["sifra"].Value = "-";
            dgv.Rows[index].Cells["naziv"].Value = Global.Database.GetSobe(row["id_soba"].ToString()).Rows[0]["naziv_sobe"].ToString()
                + " | " + Global.Database.GetPartners(row["id_partner"].ToString()).Rows[0]["ime_tvrtke"].ToString()
                + " | " + $"Dolazak: {row["datum_dolaska"].ToString()} - Odlazak: {row["datum_odlaska"].ToString()}"; ;
            dgv.Rows[index].Cells["jmj"].Value = "-";
            dgv.Rows[index].Cells["skladiste"].Value = Class.Postavke.id_default_skladiste.ToString();
            dgv.Rows[index].Cells["cijena"].Value = row["ukupno"].ToString();
            dgv.Rows[index].Cells["kolicina"].Value = "1";
            dgv.Rows[index].Cells["popust"].Value = "0";
            dgv.Rows[index].Cells["porez"].Value = "13,00";
            dgv.Rows[index].Cells["iznos"].Value = row["ukupno"].ToString();
            dgv.Rows[index].Cells["vpc"].Value = row["ukupno"].ToString();
            dgv.Rows[index].Cells["nbc"].Value = row["ukupno"].ToString();
            dgv.Rows[index].Cells["oduzmi"].Value = "NE";
            dgv.Rows[index].Cells["porez_na_dohodak"].Value = "0";
            dgv.Rows[index].Cells["prirez"].Value = "0";
            dgv.Rows[index].Cells["porez_na_dohodak_iznos"].Value = "0";
            dgv.Rows[index].Cells["prirez_iznos"].Value = "0";

            IzracunUkupno();
        }

        private void btnKoristiUds_Click(object sender, EventArgs e)
        {
            try
            {
                //frmKoristiKarticu f = new frmKoristiKarticu();
                //f.useUDS = Class.Postavke.UDSGame;
                //f.udsScores = udsCustomer.scores;
                //f.ukupno_iznos = Convert.ToDecimal(lblUkupno.Text.Split(' ')[0]);
                //f.ukupno_iznos = Convert.ToDecimal(ukupno);
                //if (f.ShowDialog(this) == DialogResult.OK)
                //{
                //    karticaIznosZaOduzeti = Convert.ToDecimal(f.txtIznosZaOduzeti.Text);
                //    UDSAPI_SCORES_TO_SUBSTRACT_FROM_CUSTOMER_ACCOUNT = karticaIznosZaOduzeti;
                //    ukupno = (double)f.ukupno_iznos;
                //    lblUkupno.Text = f.ukupno_iznos.ToString("#0.00") + " Kn";
                //}
            }
            catch
            {
            }
        }

        #region Util

        private void SrediSirinuHeadera()
        {
            DataGridViewColumnCollection cc = dgv.Columns;

            cc["sifra"].Width = dgv.Width / 5;
            cc["naziv"].Width = dgv.Width / 5;
            cc["jmj"].Width = dgv.Width / 10;
            cc["skladiste"].Width = dgv.Width / 10;
            cc["kolicina"].Width = dgv.Width / 10;
            cc["popust"].Width = dgv.Width / 10;
            cc["iznos"].Width = dgv.Width / 10;
            cc["cijena"].Width = dgv.Width / 10;
        }

        private void SetSmjene()
        {
            string ZBS = ZadnjiBrojSmjene();

            if (ZBS == "null")
            {
                Kasa.frmPocetnoStanjeSmjene ps = new Kasa.frmPocetnoStanjeSmjene();
                ps.ShowDialog();
            }
            else
            {
                string sql = "SELECT * FROM smjene WHERE id='" + ZBS + "'";
                DataTable DT_smjena = classSQL.select(sql, "smjene").Tables[0];

                if (DT_smjena.Rows.Count > 0)
                {
                    if (DT_smjena.Rows[0]["zavrsetak"].ToString() != "")
                    {
                        Kasa.frmPocetnoStanjeSmjene ps = new Kasa.frmPocetnoStanjeSmjene();
                        ps.ShowDialog();
                    }
                }
            }
        }

        private string ZadnjiBrojSmjene()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(id) FROM smjene", "smjene").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString())).ToString();
            }
            else
            {
                return "null";
            }
        }

        private void SetSkladiste()
        {
            DS_Skladiste = classSQL.select("SELECT * FROM skladiste ORDER BY skladiste", "skladiste");
            CBskladiste.DataSource = DS_Skladiste.Tables[0];
            CBskladiste.DisplayMember = "skladiste";
            CBskladiste.ValueMember = "id_skladiste";
            CBskladiste.SelectedValue = DSpostavke.Tables[0].Rows[0]["default_skladiste"].ToString();
        }

        private void SetSkladiste(string skladiste)
        {
            if (dgv.RowCount > 0)
            {
                if (dgv.CurrentRow.Cells[8].FormattedValue.ToString() == "DA")
                {
                    dgv.CurrentRow.Cells["skladiste"].Value = skladiste;

                    DataTable DSprodaja = classSQL.select("SELECT vpc,porez,kolicina FROM roba_prodaja WHERE sifra='" +
                        dgv.CurrentRow.Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + skladiste + "'",
                        "roba_prodaja").Tables[0];
                    if (DSprodaja.Rows.Count > 0)
                    {
                        decimal _NBC = Util.Korisno.VratiNabavnuCijenu(dgv.CurrentRow.Cells["sifra"].FormattedValue.ToString(), skladiste);
                        dgv.CurrentRow.Cells[4].Value = Math.Round((Convert.ToDouble(DSprodaja.Rows[0]["vpc"].ToString()) *
                            Convert.ToDouble(DSprodaja.Rows[0]["porez"].ToString()) / 100) +
                            Convert.ToDouble(DSprodaja.Rows[0]["vpc"].ToString()), 2).ToString("#0.00");
                        dgv.CurrentRow.Cells[7].Value = Math.Round((Convert.ToDouble(DSprodaja.Rows[0]["vpc"].ToString()) *
                            Convert.ToDouble(DSprodaja.Rows[0]["porez"].ToString()) / 100) +
                            Convert.ToDouble(DSprodaja.Rows[0]["vpc"].ToString()), 2).ToString("#0.00");

                        dgv.CurrentRow.Cells[10].Value = Math.Round(Convert.ToDouble(DSprodaja.Rows[0]["vpc"].ToString()), 2).ToString("#0.000");
                        dgv.CurrentRow.Cells[9].Value = DSprodaja.Rows[0]["porez"].ToString();
                        dgv.CurrentRow.Cells["nbc"].Value = _NBC.ToString("#0.000");
                        double rabat = Convert.ToDouble(dgv.CurrentRow.Cells[6].Value.ToString());
                        double cijena = Convert.ToDouble(dgv.CurrentRow.Cells[4].Value.ToString());
                        double kolicina = Convert.ToDouble(dgv.CurrentRow.Cells[5].Value.ToString());
                        double ukupno;
                        ukupno = cijena * kolicina;
                        rabat = ukupno * rabat / 100;
                        dgv.CurrentRow.Cells[7].Value = Math.Round(ukupno - rabat, 2).ToString("#0.00");
                        IzracunUkupno();
                    }
                    else
                    {
                        //MessageBox.Show("Roba nije definirana na odabranom skladištu!");
                        //btnObrisiStavku.Select();
                    }
                }
                else
                {
                    //MessageBox.Show("Kod robe koja se ne oduzima na skladištu se niti ne mijenja skladište!");
                }
            }
        }

        private void SetRabat(double rabat)
        {
            double cijena = Convert.ToDouble(dgv.CurrentRow.Cells[4].Value.ToString());
            double kolicina = Convert.ToDouble(dgv.CurrentRow.Cells[5].Value.ToString());
            double ukupno;
            ukupno = cijena * kolicina;
            rabat = ukupno * rabat / 100;
            dgv.CurrentRow.Cells[7].Value = Math.Round(ukupno - rabat, 2).ToString("#0.00");
            IzracunUkupno();
        }

        private void SetKolicina(double kolicina)
        {
            double cijena = Convert.ToDouble(dgv.CurrentRow.Cells[4].Value.ToString());
            double rabat = Convert.ToDouble(dgv.CurrentRow.Cells[6].Value.ToString());
            double ukupno;
            ukupno = Math.Round(cijena * kolicina, 2);
            rabat = Math.Round(ukupno * rabat / 100, 2);
            dgv.CurrentRow.Cells[7].Value = Math.Round(ukupno - rabat, 2).ToString("#0.00");
        }

        private void SetRoba(string sifra)
        {
            if (txtUnos.Text.Length > 2)
            {
                if (DTpromocije1.Rows.Count > 0 && DTpromocije1.Rows[0]["aktivnost"].ToString() == "DA" && txtUnos.Text.Substring(0, 3) == "000")
                {
                    double uk;
                    double popust;
                    DataTable DTrp = classSQL.select("SELECT * FROM racun_popust_kod_sljedece_kupnje WHERE broj_racuna='" +
                        txtUnos.Text.Substring(3, txtUnos.Text.Length - 3) + "'" +
                        " AND dokumenat='RN'", "racun_popust_kod_sljedece_kupnje").Tables[0];

                    if (DTrp.Rows.Count == 0)
                    {
                        MessageBox.Show("Ovaj popust nije valjan.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (DTrp.Rows[0]["koristeno"].ToString() == "DA")
                    {
                        MessageBox.Show("Ovaj popust je već iskorišten.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    DateTime dateFromPopust = Convert.ToDateTime(DTrp.Rows[0]["datum"].ToString()).
                        AddDays(Convert.ToDouble(DTpromocije1.Rows[0]["traje_do"].ToString()));

                    if (dateFromPopust < DateTime.Now)
                    {
                        MessageBox.Show("Ovom popustu je istekao datum.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    uk = Convert.ToDouble(DTrp.Rows[0]["ukupno"].ToString());
                    popust = Convert.ToDouble(DTrp.Rows[0]["popust"].ToString());
                    uk = uk * popust / 100;

                    if ((ukupno - uk) < 0)
                    {
                        MessageBox.Show("Popust je veći od ukupnog računa.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    dgv.Rows.Add(
                                txtUnos.Text,
                                "Popust sa prethodnog računa",
                                "kn",
                                1,
                                Math.Round(uk * -1, 2).ToString("#0.00"),
                                "1",
                                0,
                                Math.Round(uk * -1, 2).ToString("#0.00"),
                                "",
                                DSpostavke.Tables[0].Rows[0]["pdv"].ToString(),
                                (uk * (-1)) / (1 + Convert.ToDouble(DSpostavke.Tables[0].Rows[0]["pdv"].ToString()) / 100)
                            );

                    IzracunUkupno();
                    dgv.Select();
                    dgv.CurrentCell = dgv.Rows[dgv.Rows.Count - 1].Cells[3];
                    PaintRows(dgv);
                    txtUnos.Text = "";
                    txtUnos.Select();
                    return;
                }
            }

            string ItemDisplay = "";
            string PriceItem = "";
            string sqlRobaProdaja;
            DataTable DTroba = classSQL.select("SELECT nc, sifra, oduzmi, naziv, jm, vpc, mpc, porez FROM roba" +
                " WHERE sifra='" + sifra + "'", "roba").Tables[0];
            if (DTroba.Rows.Count != 0)
            {
                //ako robe nema na skladištu pita korisnika da li želi skinuti
                //ako želi funkcija vraća TRUE
                //ako ne želi vraća FALSE i izlazi iz metode SetRoba
                //if (!GetKolicinaSkaldisteUpozorenje(sifra, CBskladiste.SelectedValue.ToString()))
                //if (Properties.Settings.Default.idSkladiste == "")
                Properties.Settings.Default.idSkladiste = CBskladiste.SelectedValue.ToString();
                if (!GetKolicinaSkaldisteUpozorenje(sifra, Properties.Settings.Default.idSkladiste, true))
                    return;

                //vecSelektiran = false;
                if (skladiste_s_drugom_cijenom == true)
                {
                    PriceItem = Math.Round(Convert.ToDouble(DTcijenakomadno.Rows[0]["nova_cijena"]), 2).ToString("#0.00");
                }
                else
                {
                    PriceItem = Math.Round(Convert.ToDouble(DTroba.Rows[0]["mpc"]), 2).ToString("#0.00");
                }

                ItemDisplay = DTroba.Rows[0]["naziv"].ToString();

                decimal _NBC = Korisno.VratiNabavnuCijenu(DTroba.Rows[0]["sifra"].ToString(), Properties.Settings.Default.idSkladiste);

                double pov_nak = 0;
                DataSet dsPovNak = classSQL.select(string.Format("select iznos from povratna_naknada where sifra = '{0}'", sifra), "povratna_naknada");
                if (dsPovNak != null && dsPovNak.Tables.Count > 0 && dsPovNak.Tables[0] != null && dsPovNak.Tables[0].Rows.Count > 0)
                {
                    double.TryParse(dsPovNak.Tables[0].Rows[0][0].ToString(), out pov_nak);
                }
                if (!Class.Postavke.koristi_povratnu_naknadu)
                    pov_nak = 0;

                decimal cijena_stavkeMPC;
                decimal.TryParse(PriceItem, out cijena_stavkeMPC);
                objDrugiPorezi = korisno.Vrati_PorezDohodak_Prirez(cijena_stavkeMPC, sifraPartnera, DSpostavke.Tables[0], true);
                decimal prirezD = 0, porezNaDohodakD = 0, _osnovica = 0, porezNaDohodakVal = 0, prirezVal = 0;
                if (objDrugiPorezi[0] != null && objDrugiPorezi[1] != null && !chbNeObracunavajPorezPrirez.Checked)
                {
                    //if (objDrugiPorezi[0].ToString() != "0" && objDrugiPorezi[1].ToString() != "0")
                    {
                        decimal.TryParse(objDrugiPorezi[0].ToString().Trim(), out porezNaDohodakD);
                        decimal.TryParse(objDrugiPorezi[1].ToString().Trim(), out prirezD);
                        decimal.TryParse(objDrugiPorezi[2].ToString().Trim(), out _osnovica);
                        decimal.TryParse(objDrugiPorezi[3].ToString().Trim(), out porezNaDohodakVal);
                        decimal.TryParse(objDrugiPorezi[4].ToString().Trim(), out prirezVal);
                        PriceItem = Math.Round((_osnovica - (porezNaDohodakD + prirezD)), 2).ToString("#0.00");

                        /*dgv.CurrentRow.Cells["porez_na_dohodak"].Value = porezNaDohodakVal;
                        dgv.CurrentRow.Cells["prirez"].Value = prirezVal;
                        dgv.CurrentRow.Cells["porez_na_dohodak_iznos"].Value = porezNaDohodakD;
                        dgv.CurrentRow.Cells["prirez_iznos"].Value = prirezD;*/
                    }
                }

                // if (Class.PodaciTvrtka.oibTvrtke == "33251441748")
                PriceItem = PriceItem + pov_nak;

                dgv.Rows.Add(
                    DTroba.Rows[0]["sifra"].ToString(),
                    ItemDisplay,
                    DTroba.Rows[0]["jm"].ToString(),
                    Properties.Settings.Default.idSkladiste,
                    PriceItem,
                    "1",
                    "0",
                    PriceItem,
                    DTroba.Rows[0]["oduzmi"].ToString(),
                    DTroba.Rows[0]["porez"].ToString(),
                    DTroba.Rows[0]["vpc"].ToString(),
                    //stavil sam zakljucaj=1, neznam kaj to radi
                    //inače prije nije bilo te kolone kod dodavanja redova, pa je nabavnu cijenu pospremal u kolonu zaključaj,
                    //i zato nije niri mogel kasnije pročitati nabavnu cijenu
                    "",//
                       //
                    _NBC.ToString().Replace(".", ","),
                    porezNaDohodakVal,
                    prirezVal,
                    porezNaDohodakD,
                    prirezD);

                dgv.Select();
                dgv.CurrentCell = dgv.Rows[dgv.Rows.Count - 1].Cells[3];
                txtUnos.Select();

                if (DTroba.Rows[0]["oduzmi"].ToString() == "DA")
                {
                    sqlRobaProdaja = "SELECT nc, vpc, porez, kolicina FROM roba_prodaja WHERE sifra = '" + sifra + "'" +
                        " AND id_skladiste='" + Properties.Settings.Default.idSkladiste + "'";

                    sqlRobaProdaja = string.Format(@"SELECT rp.nc, rp.vpc, rp.porez, rp.kolicina, pn.iznos
FROM roba_prodaja rp
left join povratna_naknada pn on rp.sifra = pn.sifra
WHERE rp.sifra = '{0}'
and rp.id_skladiste = '{1}';", sifra, Properties.Settings.Default.idSkladiste);

                    DataTable DSprodaja = classSQL.select(sqlRobaProdaja, "roba_prodaja").Tables[0];
                    if (DSprodaja.Rows.Count > 0)
                    {
                        double.TryParse(DSprodaja.Rows[0]["iznos"].ToString(), out pov_nak);
                        //pov_nak = 0;
                        if (!Class.Postavke.koristi_povratnu_naknadu)
                            pov_nak = 0;

                        if (skladiste_s_drugom_cijenom == true)
                        {
                            decimal vpc = (Convert.ToDecimal(DTcijenakomadno.Rows[0]["nova_cijena"].ToString()) - (decimal)pov_nak) / (1 + (Convert.ToDecimal(DSprodaja.Rows[0]["porez"].ToString()) / 100));
                            dgv.CurrentRow.Cells["vpc"].Value = vpc;
                            dgv.CurrentRow.Cells["porez"].Value = DSprodaja.Rows[0]["porez"].ToString();
                            dgv.CurrentRow.Cells["nbc"].Value = DSprodaja.Rows[0]["nc"].ToString();
                            string p = DSprodaja.Rows[0]["porez"].ToString();
                            dgv.CurrentRow.Cells[4].Value = Math.Round((
                                Convert.ToDouble(vpc + (decimal)pov_nak) *
                                Convert.ToDouble(DSprodaja.Rows[0]["porez"].ToString()) / 100) +
                                Convert.ToDouble(vpc + (decimal)pov_nak), 2).ToString("#0.00");
                            dgv.CurrentRow.Cells[7].Value = Math.Round((
                                Convert.ToDouble(vpc + (decimal)pov_nak) *
                                Convert.ToDouble(DSprodaja.Rows[0]["porez"].ToString()) / 100) +
                                Convert.ToDouble(vpc + (decimal)pov_nak), 2).ToString("#0.00");
                            GetKolicinaSkladiste(sifra, CBskladiste.SelectedValue.ToString(),
                                dgv.CurrentRow.Cells["jmj"].FormattedValue.ToString());
                        }
                        else
                        {
                            double mpc = Math.Round((
                                (Convert.ToDouble(DSprodaja.Rows[0]["vpc"].ToString())) *
                                Convert.ToDouble(DSprodaja.Rows[0]["porez"].ToString()) / 100) +
                                (Convert.ToDouble(DSprodaja.Rows[0]["vpc"].ToString())), 2) + pov_nak;

                            dgv.CurrentRow.Cells["vpc"].Value = Convert.ToDouble(DSprodaja.Rows[0]["vpc"]).ToString("#0.000");
                            dgv.CurrentRow.Cells["porez"].Value = DSprodaja.Rows[0]["porez"].ToString();
                            dgv.CurrentRow.Cells["nbc"].Value = DSprodaja.Rows[0]["nc"].ToString();
                            string p = DSprodaja.Rows[0]["porez"].ToString();
                            dgv.CurrentRow.Cells[4].Value = mpc.ToString("#0.00");
                            dgv.CurrentRow.Cells[7].Value = mpc.ToString("#0.00");
                            GetKolicinaSkladiste(sifra, CBskladiste.SelectedValue.ToString(),
                                dgv.CurrentRow.Cells["jmj"].FormattedValue.ToString());
                            //Ukupno();
                        }
                    }
                    else
                    {
                        GetKolicinaSkladiste(sifra, CBskladiste.SelectedValue.ToString(),
                            dgv.CurrentRow.Cells["jmj"].FormattedValue.ToString());
                    }
                }
                else
                {
                    lblSkladiste.Text = "";
                }

                string sifra_kaucija;

                DataTable DTkaucijaPostoji = classSQL.select("SELECT sifra_kaucija, kolicina FROM roba_kaucija" +
                " WHERE sifra='" + sifra + "'", "roba").Tables[0];
                if (DTkaucijaPostoji.Rows.Count > 0)
                {
                    for (int i = 0; i < DTkaucijaPostoji.Rows.Count; i++)
                    {
                        sifra_kaucija = DTkaucijaPostoji.Rows[i]["sifra_kaucija"].ToString();

                        DataTable DTkaucija = classSQL.select("SELECT nc,sifra,oduzmi,naziv,jm,vpc,mpc,porez FROM roba" +
                        " WHERE sifra='" + sifra_kaucija + "'", "roba").Tables[0];

                        dgv.Rows.Add(
                            sifra_kaucija,
                            DTkaucija.Rows[0]["naziv"].ToString(),
                            DTkaucija.Rows[0]["jm"].ToString(),
                            Properties.Settings.Default.idSkladiste,
                            Math.Round(Convert.ToDouble(DTkaucija.Rows[0]["mpc"]), 2).ToString("#0.00"),
                            DTkaucijaPostoji.Rows[i]["kolicina"].ToString(),
                            "0",
                            Math.Round(Convert.ToDouble(DTkaucija.Rows[0]["mpc"]), 2).ToString("#0.00"),
                            DTkaucija.Rows[0]["oduzmi"].ToString(),
                            DTkaucija.Rows[0]["porez"].ToString(),
                            DTkaucija.Rows[0]["vpc"].ToString(),
                            "",
                            DTkaucija.Rows[0]["nc"].ToString());

                        dgv.Select();
                        dgv.CurrentCell = dgv.Rows[dgv.Rows.Count - 1].Cells[3];

                        if (DTroba.Rows[0]["oduzmi"].ToString() == "DA")
                        {
                            sqlRobaProdaja = "SELECT nc,vpc,porez,kolicina FROM roba_prodaja WHERE sifra='" + sifra_kaucija + "'" +
                                " AND id_skladiste='" + Properties.Settings.Default.idSkladiste + "'";

                            DataTable DSprodaja = classSQL.select(sqlRobaProdaja, "roba_prodaja").Tables[0];
                            if (DSprodaja.Rows.Count > 0)
                            {
                                dgv.CurrentRow.Cells["vpc"].Value = Convert.ToDouble(DSprodaja.Rows[0]["vpc"]).ToString("#0.000");
                                dgv.CurrentRow.Cells["porez"].Value = DSprodaja.Rows[0]["porez"].ToString();
                                dgv.CurrentRow.Cells["nbc"].Value = DSprodaja.Rows[0]["nc"].ToString();
                                string p = DSprodaja.Rows[0]["porez"].ToString();
                                dgv.CurrentRow.Cells[4].Value = Math.Round((
                                    Convert.ToDouble(DSprodaja.Rows[0]["vpc"].ToString()) *
                                    Convert.ToDouble(DSprodaja.Rows[0]["porez"].ToString()) / 100) +
                                    Convert.ToDouble(DSprodaja.Rows[0]["vpc"].ToString()), 2).ToString("#0.00");
                                dgv.CurrentRow.Cells[7].Value = Math.Round((
                                    Convert.ToDouble(DSprodaja.Rows[0]["vpc"].ToString()) *
                                    Convert.ToDouble(DSprodaja.Rows[0]["porez"].ToString()) / 100) +
                                    Convert.ToDouble(DSprodaja.Rows[0]["vpc"].ToString()), 2).ToString("#0.00");

                                GetKolicinaSkladiste(sifra, CBskladiste.SelectedValue.ToString(),
                                    dgv.CurrentRow.Cells["jmj"].FormattedValue.ToString());
                            }
                            else
                            {
                                GetKolicinaSkladiste(sifra, CBskladiste.SelectedValue.ToString(),
                                    dgv.CurrentRow.Cells["jmj"].FormattedValue.ToString());
                            }
                        }

                        SetKolicina(Convert.ToDouble(dgv.CurrentRow.Cells["kolicina"].Value));
                    }
                }

                txtUnos.Select();
                ProvjeraPromocije(sifra);
                PartnerPostaviPopust();
                IzracunUkupno();
                PaintRows(dgv);
                txtUnos.Text = "";
            }
            else
            {
                MessageBox.Show("Kriva šifra", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void PaintRows(DataGridView dg)
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
            row.Height = 40;
        }

        private void GetKolicinaSkladiste(string sifra, string skladiste, string jmj)
        {
            DataTable DTkol = classSQL.select($@"SELECT kolicina FROM roba_prodaja WHERE id_skladiste='{skladiste}' AND sifra='{sifra}'", "roba_prodaja").Tables[0];

            decimal kolicina;
            if (skladiste_s_drugom_cijenom == true)
            {
                DTkol.Clear();
                DTkol = classSQL.select("SELECT kolicina_ostatak FROM promjena_cijene_komadno_stavke WHERE id_skladiste='" + skladiste + "'" +
                     " AND sifra='" + sifra + "'", "promjena_cijene_komadno_stavke").Tables[0];
            }
            if (skladiste_s_drugom_cijenom == false)
            {
                if (DTkol.Rows.Count < 1)
                {
                    kolicina = 0;
                    lblSkladiste.Text = "Artikl pod šifrom " + sifra + " na odabranom skladištu ima " + kolicina + " " + jmj;
                    lblSkladiste.ForeColor = Color.Red;
                }
                else
                {
                    kolicina = Convert.ToDecimal(DTkol.Rows[0][0].ToString());
                    lblSkladiste.Text = "Artikl pod šifrom " + sifra + " na odabranom skladištu ima " + kolicina + " " + jmj;
                    lblSkladiste.ForeColor = kolicina > 0 ? Color.Lime : Color.Red;
                }
            }
            else
            {
                if (DTkol.Rows.Count < 1)
                {
                    kolicina = 0;
                    lblSkladiste.Text = "Artikl pod šifrom " + sifra + " na skladištu s posebnom cijenom ima " + kolicina + " " + jmj;
                    lblSkladiste.ForeColor = Color.Red;
                }
                else
                {
                    kolicina = Convert.ToDecimal(DTkol.Rows[0][0].ToString());
                    lblSkladiste.Text = "Artikl pod šifrom " + sifra + " na skladištu s posebnom cijenom ima " + kolicina + " " + jmj;
                    lblSkladiste.ForeColor = kolicina > 0 ? Color.Lime : Color.Red;
                }
            }
        }

        private bool GetKolicinaSkaldisteUpozorenje(string sifra, string skladiste, bool noviUnos)
        {
            string sql = "SELECT oduzmi FROM roba WHERE roba.sifra='" + sifra + "'";
            DataTable DTkol = classSQL.select(sql, "roba_prodaja").Tables[0];

            if (DTkol.Rows[0][0].ToString() == "NE") return true;
            if (DTkol.Rows[0][0].ToString() == "")
            {
                MessageBox.Show("Roba ne postoji na skladištu!");
                return false;
            }
            DataTable DTkolSvaSkladista = new DataTable();

            sql = "SELECT kolicina FROM roba_prodaja WHERE id_skladiste='" + skladiste + "' AND sifra='" + sifra + "'";
            DTkol = classSQL.select(sql, "roba_prodaja").Tables[0];

            sql = "select sum(cast(replace(kolicina,',','.') as numeric)) AS [Količina], " +
                "skladiste AS [Skladište],s.id_skladiste from roba_prodaja r, skladiste s where r.id_skladiste=s.id_skladiste and sifra='" +
                sifra + "' group by skladiste,s.id_skladiste order by skladiste";
            DTkolSvaSkladista = classSQL.select(sql, "roba_prodaja").Tables[0];

            if (DTcijenakomadno.Rows.Count > 0)
            {
                DataTable sql_kol_skl = classSQL.select("Select max(id_stavka) from promjena_cijene_komadno_stavke where sifra = '" + sifra + "' ", "kolicina na skladistu posebno").Tables[0];
                if (DTcijenakomadno.Rows[0]["kolicina_ostatak"].ToString() != "0")
                {
                    DTkolSvaSkladista.Clear();
                    DTkol.Clear();
                    sql = "SELECT kolicina_ostatak FROM roba_prodaja WHERE id_skladiste='" + skladiste + "' AND sifra='" + sifra + "' AND id_stavka = '" + sql_kol_skl.Rows[0][0].ToString() + "'";
                    DTkol.Columns.Add("kolicina_ostatak");
                    DTkol.Rows.Add(DTcijenakomadno.Rows[0]["kolicina_ostatak"].ToString());
                    //DTkol.Rows[0]["kolicina_ostatak"] = DTcijenakomadno.Rows[0]["kolicina_ostatak"];

                    sql = "select sum(kolicina_ostatak) AS [Količina], " +
                      "skladiste AS [Skladište],id_skladiste from promjena_cijene_komadno_stavke  where sifra='" +
                      sifra + "' AND id_stavka = '" + sql_kol_skl.Rows[0][0].ToString() + "' group by skladiste,id_skladiste order by skladiste";
                    DTkolSvaSkladista = classSQL.select(sql, "roba_prodaja").Tables[0];
                }
            }
            if (DTkol.Rows.Count > 0)
            {
                decimal kolicina = Convert.ToDecimal(DTkol.Rows[0][0].ToString());

                if (kolicina > 0)
                {
                    return true;
                }
                else
                {
                    //UPOZORENJE!!!
                    if (Class.Postavke.sakrij_formu_za_prodaju_u_minus)
                    {
                        return true;
                    }
                    else
                    {
                        return skladisteUMinusUpozorenje(kolicina, DTkolSvaSkladista, noviUnos);
                    }
                }
            }
            else
            {
                //UPOZORENJE!!!
                if (Class.Postavke.sakrij_formu_za_prodaju_u_minus)
                {
                    return true;
                }
                else
                {
                    return skladisteUMinusUpozorenje(0, DTkolSvaSkladista, noviUnos);
                }
            }
        }

        private bool skladisteUMinusUpozorenje(decimal kolicina, DataTable svaSkladista, bool noviUnos)
        {
            //forma za šifru
            Kasa.frmUnesiSifruSkladisteUMinus frm = new Kasa.frmUnesiSifruSkladisteUMinus();
            frm.svaSkladista = svaSkladista;
            frm.ShowDialog();

            if (frm.SKIDAJ && !frm.novoKol)
            {
                //unesi zapisnik u bazu
                string d = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                string sql = "INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja)" +
                    " VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + d +
                    "','SKLADIŠTE U MINUS! RAČUN: " + brRac + "/" + DateTime.Now.Year + "')";
                classSQL.insert(sql);
            }

            if (frm.SKIDAJ && (frm.novoSkladiste != null))
            {
                if (noviUnos) Properties.Settings.Default.idSkladiste = frm.novoSkladiste;
                else SetSkladiste(frm.novoSkladiste);
                CBskladiste.SelectedValue = frm.novoSkladiste;
            }

            return frm.SKIDAJ;
        }

        public void IzracunUkupno()
        {
            ukupno = 0;
            ukupnoBezRabata = 0;
            decimal _popust = 0;
            decimal _kolicina = 0;
            decimal _mpc = 0;

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (dgv.Rows[i].Cells["iznos"].Value != null)
                {
                    _popust = Convert.ToDecimal(dgv.Rows[i].Cells["popust"].FormattedValue.ToString());
                    _kolicina = Convert.ToDecimal(dgv.Rows[i].Cells["kolicina"].FormattedValue.ToString());
                    _mpc = Convert.ToDecimal(dgv.Rows[i].Cells["cijena"].FormattedValue.ToString()) * _kolicina;

                    ukupno += Convert.ToDouble(dgv.Rows[i].Cells["iznos"].FormattedValue);
                    //lblUkupno.Text = String.Format("{0:0.00}", ukupno) + " Kn";
                }
            }

            lblUkupno.Text = Math.Round(ukupno, 2).ToString("#0.00") + " Kn";

            //if (DTpostavkePrinter.Rows[0]["lineDisplay_bool"].ToString() == "1")
            //{
            //    backgroundWorkerSendToDisplay.RunWorkerAsync();
            //}
        }

        private string artikl_start = "";
        private string cijena_start = "";
        private string artikl_display = "";
        private string cijena_display = "";

        private void backgroundWorkerSendToDisplay_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = dgv.Rows.Count - 1;
            if (dgv.Rows[i].Cells["naziv"].FormattedValue.ToString() == "") { return; }
            artikl_start = dgv.Rows[i].Cells["naziv"].FormattedValue.ToString();
            cijena_start = dgv.Rows[i].Cells["iznos"].FormattedValue.ToString();
            if (artikl_start.Length > 12) { artikl_start = artikl_start.Substring(0, 12); }

            if (cijena_start != cijena_display || artikl_start != artikl_display)
            {
                cijena_display = cijena_start;
                artikl_display = artikl_start;
                //PosPrint.classLineDisplay.WriteOnDisplay(artikl_display + " " + String.Format("{0:0.00}",
                //Convert.ToDouble(cijena_display)) + "\n" + "UKUPNO: " + String.Format("{0:0.00}", ukupno));
            }
        }

        public void SetOnNull()
        {
            dgv.Rows.Clear();
            txtImePartnera.Text = "";
            txtImePartnera.Text = "";
            lblUkupno.Text = "0,00 Kn";
            sifraPartnera = "0";
        }

        private void NoviUnos()
        {
            dodjeli_popust = 0;
            DTpartner_ = null;
            brRac = brojRacuna();
            lblBrojRac.Text = "Broj računa: " + brRac + "/" + DateTime.Now.Year;
            SetOnNull();
        }

        public string brojRacuna()
        {
            // WHERE naplatni_uredaj='" + blagajna + "'
            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_racuna AS bigint)) FROM racuni WHERE id_ducan=" + id_ducan + " AND id_kasa=" + id_kasa, "racuni").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private DataTable VratiTablicuSKolonama()
        {
            DTsend = new DataTable();
            DTsend.Columns.Add("broj_racuna");
            DTsend.Columns.Add("sifra_robe");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("mpc");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("nbc");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("cijena");
            DTsend.Columns.Add("ime");
            DTsend.Columns.Add("porez_potrosnja");
            DTsend.Columns.Add("povratna_naknada");
            DTsend.Columns.Add("povratna_naknada_izn");
            DTsend.Columns.Add("rabat_izn");
            DTsend.Columns.Add("mpc_rabat");
            DTsend.Columns.Add("ukupno_rabat");
            DTsend.Columns.Add("ukupno_vpc");
            DTsend.Columns.Add("ukupno_mpc");
            DTsend.Columns.Add("ukupno_mpc_rabat");
            DTsend.Columns.Add("ukupno_porez");
            DTsend.Columns.Add("ukupno_osnovica");
            DTsend.Columns.Add("id_ducan");
            DTsend.Columns.Add("id_kasa");

            DTsend.Columns.Add("porez_na_dohodak");
            DTsend.Columns.Add("prirez");
            DTsend.Columns.Add("porez_na_dohodak_iznos");
            DTsend.Columns.Add("prirez_iznos");
            DTsend.Columns.Add("naziv");
            return DTsend;
        }

        private string barcode = "";

        private void SpremiRacun(string kartica, string gotovina, string bon = null)
        {
            //AVIO
            string[] avioPodaci = null;
            if (Class.PodaciTvrtka.oibTvrtke == "73502823843")
            {
                try
                {
                    Util.frmAvioPodaciDodaj f = new Util.frmAvioPodaciDodaj(true);
                    //if (lblAvioPodaci.Tag != null)
                    //{
                    //    f.podaci = (string[])lblAvioPodaci.Tag;
                    //}
                    f.ShowDialog();
                    if (f.podaci != null)
                    {
                        //lblAvioPodaci.Text = string.Format(@"Reg. oznaka: {0}{3}Tip: {1}{3}MTOW: {2}", f.podaci[0], f.podaci[1], f.podaci[2], Environment.NewLine);
                        //prikaziAvioPodatke(true);

                        avioPodaci = f.podaci;
                    }
                    else
                    {
                        //prikaziAvioPodatke(false);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            classSQL.transaction("BEGIN;");
            //upiši napomenu ako je označeno u postavkama
            string napomena = IspisNapomene().Trim();

            DTsend = VratiTablicuSKolonama();
            DataRow row;
            DataTable DTtemp;

            //priprema načina plaćanja
            string g = IznosGotovina != null && IznosGotovina != "0" ? "1" : "0";

            string k = IznosKartica != null && IznosKartica != "0" ? "1" : "0";
            string b = IznosBon != null && IznosBon != "0" ? "1" : "0";

            placanje = "O";

            if (Convert.ToDecimal(IznosGotovina) == 0 && Convert.ToDecimal(IznosBon) == 0 && Convert.ToDecimal(IznosKartica) != 0)
            {
                placanje = "K";
            }
            else if (Convert.ToDecimal(IznosGotovina) != 0 && Convert.ToDecimal(IznosBon) == 0 && Convert.ToDecimal(IznosKartica) == 0)
            {
                placanje = "G";
            }
            else if (Convert.ToDecimal(IznosGotovina) != 0 && Convert.ToDecimal(IznosKartica) != 0)
            {
                placanje = "O";
            }

            //priprema načina plaćanja

            //--------DATUMI-----------
            //datumi[0] se sprema u bazu i šalje na fiskalizaciju
            DateTime[] datumi = new DateTime[2];
            datumi[0] = DateTime.Now;
            datumi[1] = datumi[0];

            string dt = datumi[0].ToString("yyyy-MM-dd H:mm:ss");
            //--------DATUMI-----------

            //--------STAVKE-------------
            double ukupnoMpcRabat, ukupnoVpc, ukupnoMpc, ukupnoPovratnaNaknada, ukupnoRabat, ukupnoPorez, ukupnoOsnovica;
            double kolicina, rabatIzn, rabat, mpc, porez, mpcRabat, vpc, povratnaNaknada;

            double povratnaNaknadaUkRacun = 0;
            double ukupnoMpcRabatRacun = 0;
            double ukupnoMpcRacun = 0;
            double ukupnoVpcRacun = 0;
            double ukupnoRabatRacun = 0;
            double ukupnoPorezRacun = 0;
            double ukupnoOsnovicaRacun = 0;

            string sifra = "";
            //------------------------------------ zapisnik -----------------------------------------------------
            DataTable dtZapisnikStavke = Class.ZapisnikPromjeneCijene.createTableForSale();
            //------------------------------------ zapisnik -----------------------------------------------------

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                sifra = dg(i, "sifra");

                if (!Class.Dokumenti.isKasica && Class.Postavke.automatski_zapisnik)
                {
                    //------------------------------------ zapisnik -----------------------------------------------------
                    DataRow dRow = dtZapisnikStavke.NewRow();
                    dRow["sifra"] = ReturnSifra(sifra);
                    dRow["mpc"] = dg(i, "cijena");
                    dRow["kolicina"] = dg(i, "kolicina");
                    dRow["id_skladiste"] = dgv.Rows[i].Cells["skladiste"].Value;
                    dtZapisnikStavke.Rows.Add(dRow);
                    //------------------------------------ zapisnik -----------------------------------------------------
                }

                row = DTsend.NewRow();
                row["sifra_robe"] = ReturnSifra(sifra);
                row["id_skladiste"] = dgv.Rows[i].Cells["skladiste"].Value;
                row["kolicina"] = dg(i, "kolicina");
                row["rabat"] = dg(i, "popust");
                row["ime"] = dg(i, "naziv");
                row["porez_potrosnja"] = "0";

                kolicina = Convert.ToDouble(dg(i, "kolicina"));

                //POVRATNA NAKNADA
                DTtemp = classSQL.select("SELECT iznos FROM povratna_naknada WHERE sifra='" + sifra + "'", "povratna_naknada")
                    .Tables[0];

                if (DTtemp.Rows.Count > 0)
                {
                    povratnaNaknada = Math.Round(Convert.ToDouble(DTtemp.Rows[0]["iznos"].ToString()), 2);
                    ukupnoPovratnaNaknada = Math.Round(povratnaNaknada * kolicina, 2);
                }
                else
                {
                    povratnaNaknada = 0;
                    ukupnoPovratnaNaknada = 0;
                }

                povratnaNaknadaUkRacun += ukupnoPovratnaNaknada;
                //POVRATNA NAKNADA

                rabat = Convert.ToDouble(dg(i, "popust"));
                mpc = Convert.ToDouble(dg(i, "cijena"));
                vpc = Convert.ToDouble(dg(i, "vpc"));
                porez = Convert.ToDouble(dg(i, "porez"));

                ukupnoMpc = Math.Round(mpc * kolicina, 2);
                ukupnoMpcRacun += ukupnoMpc;

                ukupnoVpc = Math.Round(vpc * kolicina, 3);
                ukupnoVpcRacun += ukupnoVpc;

                ukupnoMpcRabat = Math.Round(Convert.ToDouble(dg(i, "iznos")), 2);
                ukupnoMpcRabatRacun += ukupnoMpcRabat;
                ukupnoRabat = ukupnoMpc - ukupnoMpcRabat;
                mpcRabat = Math.Round(ukupnoMpcRabat / kolicina, 2);
                rabatIzn = mpc - mpcRabat;
                ukupnoRabatRacun += ukupnoRabat;

                ukupnoOsnovica = Math.Round((ukupnoMpcRabat - ukupnoPovratnaNaknada) / (1 + porez / 100), 2);
                ukupnoOsnovicaRacun += ukupnoOsnovica;
                ukupnoPorez = Math.Round(ukupnoMpcRabat - ukupnoPovratnaNaknada - ukupnoOsnovica, 2);
                ukupnoPorezRacun += ukupnoPorez;

                if (classSQL.remoteConnectionString == "")
                {
                    row["mpc"] = dg(i, "cijena").Replace(",", ".");
                    row["porez"] = dg(i, "porez").Replace(",", ".");
                    row["vpc"] = dg(i, "vpc").Replace(",", ".");
                    row["nbc"] = dg(i, "nbc").Replace(",", ".");
                    row["cijena"] = dg(i, "cijena").Replace(",", ".");
                    row["povratna_naknada_izn"] = povratnaNaknada.ToString("#0.00").Replace(",", ".");
                    row["povratna_naknada"] = ukupnoPovratnaNaknada.ToString("#0.00").Replace(",", ".");
                    row["rabat_izn"] = rabatIzn.ToString("#0.00").Replace(",", ".");
                    row["mpc_rabat"] = mpcRabat.ToString("#0.00").Replace(",", ".");
                    row["ukupno_rabat"] = ukupnoRabat.ToString("#0.00").Replace(",", ".");
                    row["ukupno_vpc"] = ukupnoVpc.ToString("#0.000").Replace(",", ".");
                    row["ukupno_mpc"] = ukupnoMpc.ToString("#0.00").Replace(",", ".");
                    row["ukupno_mpc_rabat"] = ukupnoMpcRabat.ToString("#0.00").Replace(",", ".");
                    row["ukupno_porez"] = ukupnoPorez.ToString("#0.00").Replace(",", ".");
                    row["ukupno_osnovica"] = ukupnoOsnovica.ToString("#0.00").Replace(",", ".");
                }
                else
                {
                    row["mpc"] = dg(i, "cijena").Replace(".", ",");
                    row["porez"] = dg(i, "porez").Replace(".", ",");
                    row["vpc"] = dg(i, "vpc").Replace(".", ",");
                    row["nbc"] = dg(i, "nbc").Replace(".", ",");
                    row["cijena"] = dg(i, "cijena").Replace(".", ",");
                    row["povratna_naknada_izn"] = povratnaNaknada.ToString("#0.00").Replace(".", ",");
                    row["povratna_naknada"] = ukupnoPovratnaNaknada.ToString("#0.00").Replace(".", ",");
                    row["rabat_izn"] = rabatIzn.ToString("#0.00").Replace(".", ",");
                    row["mpc_rabat"] = mpcRabat.ToString("#0.00").Replace(".", ",");
                    row["ukupno_rabat"] = ukupnoRabat.ToString("#0.00").Replace(".", ",");
                    row["ukupno_vpc"] = ukupnoVpc.ToString("#0.000").Replace(".", ",");
                    row["ukupno_mpc"] = ukupnoMpc.ToString("#0.00").Replace(".", ",");
                    row["ukupno_mpc_rabat"] = ukupnoMpcRabat.ToString("#0.00").Replace(".", ",");
                    row["ukupno_porez"] = ukupnoPorez.ToString("#0.00").Replace(".", ",");
                    row["ukupno_osnovica"] = ukupnoOsnovica.ToString("#0.00").Replace(".", ",");

                    row["porez_na_dohodak"] = dg(i, "porez_na_dohodak").Replace(",", ".");
                    row["prirez"] = dg(i, "prirez").Replace(",", ".");
                    row["porez_na_dohodak_iznos"] = dg(i, "porez_na_dohodak_iznos").Replace(",", ".");
                    row["prirez_iznos"] = dg(i, "prirez_iznos").Replace(",", ".");
                }

                DTsend.Rows.Add(row);

                if (sifra.Length > 4)
                {
                    if (sifra.Substring(0, 3) == "000")
                    {
                        string sqlnext = "UPDATE racun_popust_kod_sljedece_kupnje SET koristeno='DA'" +
                            " WHERE broj_racuna='" + sifra.Substring(3, sifra.Length - 3) + "' AND dokumenat='RN'; ";
                        classSQL.transaction(sqlnext);
                    }
                }
            }

            if (!Class.Dokumenti.isKasica && ((Class.PodaciTvrtka.oibTvrtke == Class.Postavke.OIB_PC1 && Util.Korisno.GodinaKojaSeKoristiUbazi >= Class.Postavke.GODINA_POCETKA_FIFO_PC1) || Util.Korisno.GodinaKojaSeKoristiUbazi >= Class.Postavke.GODINA_POCETKA_FIFO))
            {
                DTsend = Class.FIFO.setRacunNbc(DTsend);
            }

            //trenutno nije implementirano
            IznosVirman = "0.00";

            string uk1 = ukupno.ToString();
            string dobiveno_gotovina, PovratnaNaknadaUkRacun, UkupnoMpcRacun, UkupnoVpcRacun,
                UkupnoMpcRabatRacun, UkupnoRabatRacun, UkupnoOsnovicaRacun, UkupnoPorezRacun;

            if (classSQL.remoteConnectionString == "")
            {
                uk1 = uk1.Replace(",", ".");
                IznosGotovina = IznosGotovina.Replace(",", ".");
                IznosKartica = IznosKartica.Replace(",", ".");
                IznosBon = IznosBon.Replace(",", ".");
                IznosVirman = IznosKartica.Replace(",", ".");
                dobiveno_gotovina = DobivenoGotovina.Replace(",", ".");
                PovratnaNaknadaUkRacun = povratnaNaknadaUkRacun.ToString("#0.00").Replace(",", ".");
                UkupnoMpcRacun = ukupnoMpcRacun.ToString("#0.00").Replace(",", ".");
                UkupnoVpcRacun = ukupnoVpcRacun.ToString("#0.000").Replace(",", ".");
                UkupnoMpcRabatRacun = ukupnoMpcRabatRacun.ToString("#0.00").Replace(",", ".");
                UkupnoRabatRacun = ukupnoRabatRacun.ToString("#0.00").Replace(",", ".");
                UkupnoOsnovicaRacun = ukupnoOsnovicaRacun.ToString("#0.00").Replace(",", ".");
                UkupnoPorezRacun = ukupnoPorezRacun.ToString("#0.00").Replace(",", ".");
            }
            else
            {
                IznosGotovina = IznosGotovina.Replace(".", ",");
                IznosKartica = IznosKartica.Replace(".", ",");
                IznosBon = IznosBon.Replace(".", ",");
                IznosVirman = IznosVirman.Replace(".", ",");
                uk1 = uk1.Replace(".", ",");
                dobiveno_gotovina = DobivenoGotovina.Replace(".", ",");
                PovratnaNaknadaUkRacun = povratnaNaknadaUkRacun.ToString("#0.00").Replace(",", ".");
                UkupnoMpcRacun = ukupnoMpcRacun.ToString("#0.00").Replace(",", ".");
                UkupnoVpcRacun = ukupnoVpcRacun.ToString("#0.000").Replace(",", ".");
                UkupnoMpcRabatRacun = ukupnoMpcRabatRacun.ToString("#0.00").Replace(",", ".");
                UkupnoRabatRacun = ukupnoRabatRacun.ToString("#0.00").Replace(",", ".");
                UkupnoOsnovicaRacun = ukupnoOsnovicaRacun.ToString("#0.00").Replace(",", ".");
                UkupnoPorezRacun = ukupnoPorezRacun.ToString("#0.00").Replace(",", ".");
            }

            if (Math.Round(Convert.ToDouble(uk1), 2) != Math.Round(ukupnoOsnovicaRacun + ukupnoPorezRacun + povratnaNaknadaUkRacun, 2))
                MessageBox.Show("Greška");

            brRac = brojRacuna();
            ZadnjiRacun = brRac;

            //------------------------------------ zapisnik -----------------------------------------------------
            bool kreiraniZapisnik = false;
            if (!Class.Dokumenti.isKasica && Class.Postavke.automatski_zapisnik)
            {
                Class.ZapisnikPromjeneCijene _zapisnik = new Class.ZapisnikPromjeneCijene(dtZapisnikStavke, false, datumi[0].AddSeconds(-1), "Kreiranje zapisnika zbog promjene cijena na maloprodajnom računu", brRac + "/" + Util.Korisno.nazivPoslovnica + "/" + Util.Korisno.nazivNaplatnogUredaja);
                kreiraniZapisnik = _zapisnik.kreiraniZapisnik;
                _zapisnik = null;
            }
            //------------------------------------ zapisnik -----------------------------------------------------

            string sql = "INSERT INTO racuni (broj_racuna,id_kupac,datum_racuna,id_ducan,id_kasa,id_blagajnik," +
                "gotovina, kartice, bon, ukupno_gotovina, ukupno_kartice, ukupno_bon, broj_kartice_cashback,broj_kartice_bodovi," +
                "br_sa_prethodnog_racuna,ukupno,storno,dobiveno_gotovina,ukupno_virman,napomena,nacin_placanja,ukupno_ostalo," +
                "ukupno_povratna_naknada,ukupno_mpc,ukupno_vpc,ukupno_mpc_rabat,ukupno_rabat,ukupno_osnovica,ukupno_porez" + (karticaKupcaB ? ", kartica_kupca" : "") + (avioPodaci != null ? ", avio_registracija, avio_tip_zrakoplova, avio_maks_tezina_polijetanja" : "") + ")" +
                "VALUES (" +
                "'" + brRac + "'," +
                "'" + sifraPartnera + "'," +
                "'" + dt + "'," +
                "'" + id_ducan + "'," +
                "'" + id_kasa + "'," +
                "'" + Properties.Settings.Default.id_zaposlenik + "'," +
                "'" + g + "'," +
                "'" + k + "'," +
                "'" + b + "'," +
                "'" + IznosGotovina + "'," +
                "'" + IznosKartica + "'," +
                "'" + IznosBon + "'," +
                "'" + txtBrojKarticeCB.Text + "'," +
                "'" + txtBrojKarticeSB.Text + "'," +
                "'" + txtBrojKarticePO.Text + "'," +
                "'" + uk1.ToString() + "'," +
                "'NE'," +
                "'" + dobiveno_gotovina + "'," +
                "'" + IznosVirman + "'," +
                "'" + napomena + "'," +
                "'" + placanje + "'," +
                "'0'," +
                "'" + PovratnaNaknadaUkRacun + "'," +
                "'" + UkupnoMpcRacun + "'," +
                "'" + UkupnoVpcRacun + "'," +
                "'" + UkupnoMpcRabatRacun + "'," +
                "'" + UkupnoRabatRacun + "'," +
                "'" + UkupnoOsnovicaRacun + "'," +
                "'" + UkupnoPorezRacun + "'" +
                (karticaKupcaB ? ", '" + karticaKupcaS + "'" : "") +
                (avioPodaci != null ? string.Format(",'{0}','{1}','{2}'", avioPodaci[0], avioPodaci[1], avioPodaci[2]) : "") +
                "); ";

            provjera_sql(classSQL.transaction(sql));

            if (skladiste_s_drugom_cijenom == false)
            {
                OduzmiSaSkladista();
            }
            else
            {
                OduzmiSaSkladista();
                OduzmiSaSkladistaPosebno();
            }

            //postavi broj_racuna na upravo uneseni račun
            for (int i = 0; i < DTsend.Rows.Count; i++)
            {
                DTsend.Rows[i].SetField("broj_racuna", brRac);
                DTsend.Rows[i].SetField("id_ducan", id_ducan);
                DTsend.Rows[i].SetField("id_kasa", id_kasa);
            }

            if (DTrezervacija != null)
            {
                DTsend.Rows[0].SetField("naziv", Global.Database.GetSobe(DTrezervacija.Rows[0]["id_soba"].ToString()).Rows[0]["naziv_sobe"].ToString() + " | " + Global.Database.GetPartners(DTrezervacija.Rows[0]["id_partner"].ToString()).Rows[0]["ime_tvrtke"].ToString()
                    + $" | Dolazak: {DateTime.Parse(DTrezervacija.Rows[0]["datum_dolaska"].ToString()).ToString("dd.MM.yyyy.")} - Odlazak: {DateTime.Parse(DTrezervacija.Rows[0]["datum_odlaska"].ToString()).ToString("dd.MM.yyyy.")}");
                Global.Database.UpdateRezervacijaNaplaceno(DTrezervacija.Rows[0]["broj"].ToString(), 1);
            }

            sifra_skladiste = "";
            provjera_sql(SQL.SQLracun.InsertStavke(DTsend));

            barcode = "000" + brRac + id_ducan + id_kasa + DateTime.Now.Year.ToString().Remove(0, 2);
            if (DTpromocije1.Rows.Count > 0 && DTpromocije1.Rows[0]["aktivnost"].ToString() == "DA")
            {
                string uk = ukupno.ToString();
                if (classSQL.remoteConnectionString == "")
                {
                    uk = uk.Replace(",", ".");
                }
                else
                {
                    uk = uk.Replace(".", ",");
                }

                string pop = "INSERT INTO racun_popust_kod_sljedece_kupnje" +
                     " (broj_racuna,datum,ukupno,popust,koristeno,dokumenat) VALUES (" +
                     "'" + brRac + id_ducan + id_kasa + DateTime.Now.Year.ToString().Remove(0, 2) + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                     "'" + uk + "'," +
                     "'" + DTpromocije1.Rows[0]["popust"].ToString() + "'," +
                     "'NE'," +
                     "'RN'" +
                     "); ";
                provjera_sql(classSQL.transaction(pop));
            }

            string mali = DTpostavkePrinter.Rows[0]["posPrinterBool"].ToString();
            string a5 = classSQL.select_settings("SELECT a5print FROM postavke", "postavke").Tables[0].Rows[0]["a5print"].ToString();
            string a6 = classSQL.select_settings("SELECT a6print FROM postavke", "postavke").Tables[0].Rows[0]["a6print"].ToString();
            classSQL.transaction("COMMIT;");

            Util.AktivnostZaposlenika.SpremiAktivnost(dgv, null, "Maloprodaja", brRac, false);

            if (kreiraniZapisnik && !Class.Dokumenti.isKasica && Class.Postavke.automatski_zapisnik)
            {
                MessageBox.Show("Kreiran je automatski zapisnik zbog promjene cijene u maloprodaji.");
            }

            try
            {
                PosPrint.classPosPrintMaloprodaja2.BoolPreview = false;
                PosPrint.classPosPrintMaloprodaja2.PrintReceipt(DTsend, blagajnik_ime, brRac + "/" +
                    datumi[0].Year.ToString(), sifraPartnera, barcode, brRac, placanje, datumi, false, mali, false, true, id_ducan, id_kasa);

                if (mali == "1")
                {
                    //već je isprintan u gornjoj metodi
                }
                else if (a5 == "1")
                {
                    printajA5(brRac, false);
                }
                else if (a6 == "1")
                {
                    printajA6(brRac, false);
                }
                else if (mali != "1")
                {
                    printaj(brRac, false);
                }
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("Desila se pogreška kod ispisa na 'mali' pos printer.\r\n" +
                    "Želite li ispisati ovaj dokument na A4 format?" + Environment.NewLine + Environment.NewLine
                    + ex.Message, "Printer") == DialogResult.Yes)
                {
                    printaj(brRac, false);
                }
            }

            karticaKupcaInfo(false);
            //if (!Util.Korisno.RadimSinkronizaciju) {
            //    Util.Korisno.RadimSinkronizaciju = true;
            //    bgSinkronizacija.RunWorkerAsync();
            //}
            /****************************SINKRONIZACIJA SA WEB-OM*****************/
            bgSinkronizacija.RunWorkerAsync();
            /****************************SINKRONIZACIJA SA WEB-OM*****************/
        }

        private void bgSinkronizacija_DoWork(object sender, DoWorkEventArgs e)
        {
            PokretacSinkronizacije.PokreniSinkronizaciju(true, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
        }

        private void OduzmiSaSkladista()
        {
            string kol; decimal k;
            //--------STAVKE ODUZMI SA SKLADIŠTA-------------
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (dg(i, "oduzmi") == "DA")
                {
                    kol = SQL.ClassSkladiste.GetAmount(
                        dg(i, "sifra"),
                        dgv.Rows[i].Cells["skladiste"].Value.ToString(),
                        dg(i, "kolicina"),
                        "1",
                        "-");
                    /*SQL.SQLroba_prodaja.UpdateRows(
                        dgv.Rows[i].Cells["skladiste"].Value.ToString(),
                        kol,
                        dg(i, "sifra"));*/

                    decimal.TryParse(kol, out k);

                    string update = "UPDATE roba_prodaja SET kolicina='" + Math.Round(k, 5).ToString("#0.0000") +
                        "' WHERE id_skladiste='" + dgv.Rows[i].Cells["skladiste"].Value.ToString() +
                        "' AND sifra='" + dg(i, "sifra") + "';";
                    classSQL.update(update);
                }
                else
                {
                    string normativ = "SELECT * FROM normativi_stavke LEFT JOIN normativi ON normativi.broj_normativa=normativi_stavke.broj_normativa WHERE sifra_artikla='" + dg(i, "sifra") + "'";
                    DataTable DTnormativ = classSQL.select(normativ, "normativ").Tables[0];

                    foreach (DataRow r in DTnormativ.Rows)
                    {
                        kol = SQL.ClassSkladiste.GetAmount(
                        r["sifra_robe"].ToString(),
                        r["id_skladiste"].ToString(),
                        r["kolicina"].ToString(),
                        dg(i, "kolicina"),
                        "-");

                        decimal.TryParse(kol, out k);
                        string update = "UPDATE roba_prodaja SET kolicina='" + Math.Round(k, 5).ToString("#0.0000") +
                        "' WHERE id_skladiste='" + r["id_skladiste"].ToString() +
                        "' AND sifra='" + r["sifra_robe"].ToString() + "';";
                        classSQL.update(update);
                    }
                }
            }
            //--------STAVKE ODUZMI SA SKLADIŠTA-------------
        }

        private void OduzmiSaSkladistaPosebno()
        {
            string kol;
            //--------STAVKE ODUZMI SA SKLADIŠTA-------------
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (dg(i, "oduzmi") == "DA")
                {
                    kol = SQL.ClassSkladistePosebno.GetAmount(
                        dg(i, "sifra"),
                        dgv.Rows[i].Cells["skladiste"].Value.ToString(),
                        dg(i, "kolicina"),
                        "1",
                        "-");
                    SQL.SQLroba_prodajaPosebno.UpdateRows(
                        dgv.Rows[i].Cells["skladiste"].Value.ToString(),
                        kol,
                        dg(i, "sifra"));
                }
            }
            //--------STAVKE ODUZMI SA SKLADIŠTA-------------
        }

        private void SrediIznoseURacunima()
        {
            string sql = "UPDATE racun_stavke SET povratna_naknada=0 where povratna_naknada='';";
            classSQL.select(sql, "racun_stavke");

            sql = "UPDATE racun_stavke SET povratna_naknada_izn=ROUND(povratna_naknada/CAST(REPLACE(kolicina,',','.') AS NUMERIC), 2) where povratna_naknada='';";
            classSQL.select(sql, "racun_stavke");

            sql = "UPDATE racuni SET povratna_naknada=0 where povratna_naknada='';";
            classSQL.select(sql, "racuni");
        }

        private void printaj(string brRac, bool testniIspis)
        {
            if (Class.Postavke.idFaktura == 1)
            {
                Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
                rfak.dokumenat = "RAC";
                rfak.samoIspis = testniIspis;
                rfak.ImeForme = "Račun";
                rfak.naplatni = id_kasa;
                rfak.poslovnica = id_ducan;
                rfak.broj_dokumenta = brRac;
                rfak.ShowDialog();
            }
            else if (Class.Postavke.idFaktura == 2)
            {
                Report.Faktura.repFakturaNovo rfak = new Report.Faktura.repFakturaNovo();
                rfak.dokumenat = "RAC";
                rfak.ImeForme = "Račun";
                rfak.poslovnica = id_ducan;
                rfak.naplatni = id_kasa;
                rfak.broj_dokumenta = brRac;
                rfak.ShowDialog();
            }
            else if (Class.Postavke.idFaktura == 3)
            {
                Report.Faktura3.repFaktura3 rfak = new Report.Faktura3.repFaktura3();
                rfak.dokumenat = "RAC";
                rfak.ImeForme = "Račun";
                rfak.poslovnica = id_ducan;
                rfak.naplatni = id_kasa;
                rfak.broj_dokumenta = brRac;
                rfak.ShowDialog();
            }
        }

        private void printajA5(string brRac, bool testniIspis)
        {
            //if (Class.Postavke.idFaktura == 1)
            //{
            Report.A5racun.frmA5racun rfak = new Report.A5racun.frmA5racun();
            rfak.dokumenat = "RAC";
            rfak.samoIspis = testniIspis;
            rfak.ImeForme = "Račun";
            rfak.naplatni = id_kasa;
            rfak.poslovnica = id_ducan;
            rfak.broj_dokumenta = brRac;
            rfak.ShowDialog();
            //}
            //else if (Class.Postavke.idFaktura == 2)
            //{
            //    Report.Faktura.repFakturaNovo rfak = new Report.Faktura.repFakturaNovo();
            //    rfak.dokumenat = "RAC";
            //    rfak.ImeForme = "Račun";
            //    rfak.poslovnica = id_ducan;
            //    rfak.naplatni = id_kasa;
            //    rfak.broj_dokumenta = brRac;
            //    rfak.ShowDialog();
            //}
            //else if (Class.Postavke.idFaktura == 3)
            //{
            //    Report.Faktura3.repFaktura3 rfak = new Report.Faktura3.repFaktura3();
            //    rfak.dokumenat = "RAC";
            //    rfak.ImeForme = "Račun";
            //    rfak.poslovnica = id_ducan;
            //    rfak.naplatni = id_kasa;
            //    rfak.broj_dokumenta = brRac;
            //    rfak.ShowDialog();
            //}
        }

        private void printajA6(string brRac, bool testniIspis)
        {
            Report.A6racun.frmA6racun rfak = new Report.A6racun.frmA6racun();
            rfak.dokumenat = "RAC";
            rfak.samoIspis = testniIspis;
            rfak.ImeForme = "Račun";
            rfak.naplatni = id_kasa;
            rfak.poslovnica = id_ducan;
            rfak.broj_dokumenta = brRac;
            rfak.ShowDialog();
        }

        private string ReturnSifra(string sifra)
        {
            try
            {
                if (sifra == "-")
                    return "!serial" + Util.Korisno.GodinaKojaSeKoristiUbazi + Global.Database.GetMaxBroj("racuni", "broj_racuna");

                if (sifra.Length > 3)
                {
                    if (DTpromocije1.Rows[0]["aktivnost"].ToString() == "DA" && sifra.Substring(0, 3) == "000")
                    {
                        return "00000";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return sifra;
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private int brojac = 0;
        private int brojac_po3 = 1;
        private double brojac_iznos = 1000000;
        private int RoW = 0;

        private int brojac1 = 0;
        private int brojac_po2 = 1;
        private double brojac_iznos1 = 1000000;
        private int RoW1 = 0;

        private void ProvjeraPromocije(string sifra)
        {
            if (DTpromocije.Rows.Count > 0)
            {
                bool a_true = false;
                bool a1_true = false;
                bool a2_true = false;
                //bool a3_true = false;
                for (int i = 0; i < DTpromocije.Rows.Count; i++)
                {
                    int row1 = 0;
                    int row2 = 0;
                    int row3 = 0;
                    int row_za_popust = 0;

                    if (DTpromocije.Rows[i]["nacin1"].ToString() == "1&2=3")
                    {
                        string art1 = DTpromocije.Rows[i]["artikl1"].ToString();
                        string art2 = DTpromocije.Rows[i]["artikl2"].ToString();
                        string art3 = DTpromocije.Rows[i]["artikl3"].ToString();

                        for (int a = 0; a < dgv.Rows.Count - 1; a++)
                        {
                            if (dgv.Rows[a].Cells["zakljucaj"].FormattedValue.ToString() == "")
                            {
                                if (dgv.Rows[a].Cells["sifra"].FormattedValue.ToString().Trim() == art1.Trim())
                                {
                                    a_true = true;
                                    row1 = a;
                                }
                                else if (dgv.Rows[a].Cells["sifra"].FormattedValue.ToString().Trim() == art2.Trim())
                                {
                                    a1_true = true;
                                    row2 = a;
                                }
                                else if (dgv.Rows[a].Cells["sifra"].FormattedValue.ToString().Trim() == art3.Trim())
                                {
                                    a2_true = true;
                                    row_za_popust = a;
                                }

                                if (a_true == true && a1_true == true && a2_true == true && dgv.Rows[row1].Cells["kolicina"].FormattedValue.ToString() == "1" && dgv.Rows[row2].Cells["kolicina"].FormattedValue.ToString() == "1")
                                {
                                    dgv.Rows[row1].Cells["kolicina"].ReadOnly = true;
                                    dgv.Rows[row2].Cells["kolicina"].ReadOnly = true;
                                    dgv.Rows[row_za_popust].Cells["kolicina"].ReadOnly = true;

                                    dgv.Rows[row1].Cells["zakljucaj"].Value = 1;
                                    dgv.Rows[row2].Cells["zakljucaj"].Value = 1;
                                    dgv.Rows[row_za_popust].Cells["zakljucaj"].Value = 1;
                                    dgv.Rows[row_za_popust].Cells["popust"].Value = DTpromocije.Rows[i]["popust3"].ToString();
                                    dgv.Rows[row_za_popust].Cells["iznos"].Value = Math.Round(
                                        Convert.ToDouble(dgv.Rows[row_za_popust].Cells["cijena"].FormattedValue.ToString()) -
                                        (Convert.ToDouble(dgv.Rows[row_za_popust].Cells["cijena"].FormattedValue.ToString()) *
                                        Convert.ToDouble(dgv.Rows[row_za_popust].Cells["popust"].Value) / 100), 2).ToString("#0.00");
                                    return;
                                }
                            }
                        }
                    }
                    else if (DTpromocije.Rows[i]["nacin1"].ToString() == "1")
                    {
                        string art1 = DTpromocije.Rows[i]["artikl1"].ToString();
                        for (int a = 0; a < dgv.Rows.Count; a++)
                        {
                            if (dgv.Rows[a].Cells["zakljucaj"].FormattedValue.ToString() == "")
                            {
                                if (dgv.Rows[a].Cells["sifra"].FormattedValue.ToString().Trim() == art1.Trim())
                                {
                                    dgv.Rows[a].Cells["zakljucaj"].Value = 1;
                                    dgv.Rows[a].Cells["popust"].Value = DTpromocije.Rows[i]["popust1"].ToString();
                                    dgv.Rows[a].Cells["iznos"].Value = string.Format("{0:0.00}", (Convert.ToDouble(dgv.Rows[row_za_popust].Cells["cijena"].FormattedValue.ToString()) - (Convert.ToDouble(dgv.Rows[row_za_popust].Cells["cijena"].FormattedValue.ToString()) * Convert.ToDouble(dgv.Rows[row_za_popust].Cells["popust"].Value) / 100)));
                                    return;
                                }
                            }
                        }
                    }
                    else if (DTpromocije.Rows[i]["nacin1"].ToString() == "1&2")
                    {
                        string art1 = DTpromocije.Rows[i]["artikl1"].ToString();
                        string art2 = DTpromocije.Rows[i]["artikl2"].ToString();
                        for (int a = 0; a < dgv.Rows.Count; a++)
                        {
                            if (dgv.Rows[a].Cells["zakljucaj"].FormattedValue.ToString() == "")
                            {
                                if (dgv.Rows[a].Cells["sifra"].FormattedValue.ToString() == art1)
                                {
                                    a_true = true;
                                    row1 = a;
                                }
                                else if (dgv.Rows[a].Cells["sifra"].FormattedValue.ToString() == art2)
                                {
                                    a1_true = true;
                                    row2 = a;
                                }
                            }
                        }
                        if (a_true == true && a1_true == true && dgv.Rows[row1].Cells["kolicina"].FormattedValue.ToString() == "1" && dgv.Rows[row2].Cells["kolicina"].FormattedValue.ToString() == "1")
                        {
                            dgv.Rows[row1].Cells["kolicina"].ReadOnly = true;
                            dgv.Rows[row2].Cells["kolicina"].ReadOnly = true;

                            dgv.Rows[row1].Cells["zakljucaj"].Value = 1;
                            dgv.Rows[row2].Cells["zakljucaj"].Value = 1;
                            dgv.Rows[row1].Cells["popust"].Value = DTpromocije.Rows[i]["popust1"].ToString();
                            dgv.Rows[row1].Cells["iznos"].Value = string.Format("{0:0.00}", (Convert.ToDouble(dgv.Rows[row1].Cells["cijena"].FormattedValue.ToString()) - (Convert.ToDouble(dgv.Rows[row1].Cells["cijena"].FormattedValue.ToString()) * Convert.ToDouble(dgv.Rows[row1].Cells["popust"].Value) / 100)));
                            dgv.Rows[row2].Cells["popust"].Value = DTpromocije.Rows[i]["popust2"].ToString();
                            dgv.Rows[row2].Cells["iznos"].Value = string.Format("{0:0.00}", (Convert.ToDouble(dgv.Rows[row2].Cells["cijena"].FormattedValue.ToString()) - (Convert.ToDouble(dgv.Rows[row2].Cells["cijena"].FormattedValue.ToString()) * Convert.ToDouble(dgv.Rows[row2].Cells["popust"].Value) / 100)));
                            return;
                        }
                    }
                    else if (DTpromocije.Rows[i]["nacin1"].ToString() == "1&2&3")
                    {
                        string art1 = DTpromocije.Rows[i]["artikl1"].ToString();
                        string art2 = DTpromocije.Rows[i]["artikl2"].ToString();
                        string art3 = DTpromocije.Rows[i]["artikl3"].ToString();
                        for (int a = 0; a < dgv.Rows.Count; a++)
                        {
                            if (dgv.Rows[a].Cells["sifra"].FormattedValue.ToString() == art1)
                            {
                                a_true = true; row1 = a;
                            }
                            else if (dgv.Rows[a].Cells["sifra"].FormattedValue.ToString() == art2)
                            {
                                a1_true = true; row2 = a;
                            }
                            else if (dgv.Rows[a].Cells["sifra"].FormattedValue.ToString() == art3)
                            {
                                a2_true = true; row3 = a;
                            }
                        }
                        if (a_true == true && a1_true == true && a2_true == true && dgv.Rows[row1].Cells["kolicina"].FormattedValue.ToString() == "1" && dgv.Rows[row2].Cells["kolicina"].FormattedValue.ToString() == "1")
                        {
                            dgv.Rows[row1].Cells["kolicina"].ReadOnly = true;
                            dgv.Rows[row2].Cells["kolicina"].ReadOnly = true;
                            dgv.Rows[row3].Cells["kolicina"].ReadOnly = true;

                            dgv.Rows[row1].Cells["zakljucaj"].Value = 1;
                            dgv.Rows[row2].Cells["zakljucaj"].Value = 1;
                            dgv.Rows[row3].Cells["zakljucaj"].Value = 1;

                            dgv.Rows[row1].Cells["popust"].Value = DTpromocije.Rows[i]["popust1"].ToString();
                            dgv.Rows[row1].Cells["iznos"].Value = string.Format("{0:0.00}", (Convert.ToDouble(dgv.Rows[row1].Cells["cijena"].FormattedValue.ToString()) - (Convert.ToDouble(dgv.Rows[row1].Cells["cijena"].FormattedValue.ToString()) * Convert.ToDouble(dgv.Rows[row1].Cells["popust"].Value) / 100)));
                            dgv.Rows[row2].Cells["popust"].Value = DTpromocije.Rows[i]["popust2"].ToString();
                            dgv.Rows[row2].Cells["iznos"].Value = string.Format("{0:0.00}", (Convert.ToDouble(dgv.Rows[row2].Cells["cijena"].FormattedValue.ToString()) - (Convert.ToDouble(dgv.Rows[row2].Cells["cijena"].FormattedValue.ToString()) * Convert.ToDouble(dgv.Rows[row2].Cells["popust"].Value) / 100)));
                            dgv.Rows[row3].Cells["popust"].Value = DTpromocije.Rows[i]["popust3"].ToString();
                            dgv.Rows[row3].Cells["iznos"].Value = string.Format("{0:0.00}", (Convert.ToDouble(dgv.Rows[row3].Cells["cijena"].FormattedValue.ToString()) - (Convert.ToDouble(dgv.Rows[row3].Cells["cijena"].FormattedValue.ToString()) * Convert.ToDouble(dgv.Rows[row3].Cells["popust"].Value) / 100)));
                            return;
                        }
                    }
                    else if (DTpromocije.Rows[i]["nacin1"].ToString() == "a+a=a")
                    {
                        brojac++;
                        if (brojac == 3)
                        {
                            for (int a = (brojac_po3 - 3); a < brojac_po3; a++)
                            {
                                if (Convert.ToDouble(dgv.Rows[a].Cells["iznos"].FormattedValue.ToString()) < brojac_iznos)
                                {
                                    brojac_iznos = Convert.ToDouble(dgv.Rows[a].Cells["iznos"].FormattedValue.ToString());
                                    RoW = a;
                                }
                            }
                            brojac_iznos = 100000000000;
                            dgv.Rows[RoW].Cells["kolicina"].ReadOnly = true;
                            dgv.Rows[RoW].Cells["popust"].Value = DTpromocije.Rows[i]["popust3"].ToString();
                            dgv.Rows[RoW].Cells["iznos"].Value = Convert.ToDouble(dgv.Rows[RoW].Cells["cijena"].FormattedValue.ToString()) - (Convert.ToDouble(dgv.Rows[RoW].Cells["cijena"].FormattedValue.ToString()) * Convert.ToDouble(dgv.Rows[RoW].Cells["popust"].Value) / 100);
                            IzracunUkupno();
                            brojac = 0;
                        }
                        brojac_po3++;
                    }
                    else if (DTpromocije.Rows[i]["nacin1"].ToString() == "a=a")
                    {
                        brojac1++;
                        if (brojac1 == 2)
                        {
                            for (int a = (brojac_po2 - 2); a < brojac_po2; a++)
                            {
                                if (Convert.ToDouble(dgv.Rows[a].Cells["iznos"].FormattedValue.ToString()) < brojac_iznos1)
                                {
                                    brojac_iznos1 = Convert.ToDouble(dgv.Rows[a].Cells["iznos"].FormattedValue.ToString());
                                    RoW1 = a;
                                }
                            }
                            brojac_iznos = 100000000000;
                            dgv.Rows[RoW].Cells["kolicina"].ReadOnly = true;
                            dgv.Rows[RoW1].Cells["popust"].Value = DTpromocije.Rows[i]["popust3"].ToString();
                            dgv.Rows[RoW1].Cells["iznos"].Value = Convert.ToDouble(dgv.Rows[RoW].Cells["cijena"].FormattedValue.ToString()) - (Convert.ToDouble(dgv.Rows[RoW].Cells["cijena"].FormattedValue.ToString()) * Convert.ToDouble(dgv.Rows[RoW].Cells["popust"].Value) / 100);
                            IzracunUkupno();
                            brojac1 = 0;
                        }
                        brojac_po2++;
                    }
                }
            }
        }

        public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle,
                System.Globalization.CultureInfo.CurrentCulture, out result);
        }

        private void CheckPosEquipment(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str, "Greška");
            }
        }

        #endregion Util

        #region buttons

        private void button12_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Text == "C")
            {
                if (txtUnos.Text.Length > 0) txtUnos.Text = txtUnos.Text.Remove(txtUnos.Text.Length - 1);
            }
            else
            {
                txtUnos.Text += btn.Text;
            }
        }

        //private void button15_Click(object sender, EventArgs e)
        private void btnOdustani_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Jeste li sigurni da želite odustati?", "Odustani", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                if (Class.Postavke.UDSGame && udsCustomer != null && CODE != 0)
                {
                    if (udsCompany == null)
                    {
                        return;
                    }

                    if (CODE != 0)
                    {
                        CODE = 0;

                        if (udsCustomer != null && dgv.Rows.Count > 0 && UDSAPI_APPLY_DISCOUNT)
                        {
                            dgv.Columns["rabat"].Visible = false;
                            dgv.Columns["rabat"].Width = 55;

                            foreach (DataGridViewRow row in dgv.Rows)
                            {
                                row.Cells["rabat"].Value = 0;
                            }

                            IzracunUkupno();
                        }

                        //if (UDSAPI_APPLY_DISCOUNT)
                        //{
                        btnDodajNaPartnera.Width = 95;
                        btnDodajNaPartnera.Text = "UDS Game";
                        //}
                        //else
                        //{
                        btnKoristiUds.Width = 95;
                        btnKoristiUds.Text = "Koristi karticu";
                        btnKoristiUds.Enabled = false;
                        btnKoristiUds.Visible = false;
                        //}

                        udsCustomer = null;

                        return;
                    }

                    var input = Microsoft.VisualBasic.Interaction.InputBox("Discount code");
                    CODE = 0;

                    if (int.TryParse(input, out CODE))
                    {
                        udsCustomer = UDSAPI.getCustomer(CODE);
                        if (udsCustomer != null)
                        {
                            if (UDSAPI_APPLY_DISCOUNT)
                            {
                                btnDodajNaPartnera.Width = 196;
                                btnDodajNaPartnera.Text = string.Format("UDS Game odustani za{0}{1} {2}", Environment.NewLine, udsCustomer.name, udsCustomer.surname);

                                dgv.Columns["rabat"].Visible = true;
                                dgv.Columns["rabat"].Width = 55;

                                if (dgv.Rows.Count > 0)
                                {
                                    foreach (DataGridViewRow row in dgv.Rows)
                                    {
                                        row.Cells["rabat"].Value = UDSAPI_DISCOUNT_BASE;
                                    }

                                    IzracunUkupno();
                                }
                            }
                            else
                            {
                                btnKoristiUds.Visible = true;
                                btnKoristiUds.Enabled = true;
                                btnDodajNaPartnera.Text = "UDS Game odustani";
                                btnKoristiUds.Text = string.Format("UDS Game bodovi za{0}{1} {2}", Environment.NewLine, udsCustomer.name, udsCustomer.surname);
                                btnKoristiUds.Width = 196;
                            }
                        }
                        else
                        {
                            CODE = 0;
                            if (UDSAPI_APPLY_DISCOUNT)
                            {
                                //btnDodajNaPartnera.Width = 95;
                                btnDodajNaPartnera.Text = "UDS Game";
                            }
                            else
                            {
                                //btnKoristiUds.Width = 95;
                                btnKoristiUds.Text = "Koristi karticu";
                                btnKoristiUds.Enabled = false;
                                btnKoristiUds.Visible = false;
                            }
                        }
                    }
                }

                DTpartner_ = null;
                dodjeli_popust = 0;
                brRac = brojRacuna();
                lblBrojRac.Text = "Broj računa: " + brRac + "/" + DateTime.Now.Year;
                SetOnNull();
                Properties.Settings.Default.id_partner = "";
            }
            txtUnos.Select();
        }

        //private void btnOdustani_Click(object sender, EventArgs e)
        //{
        //    txtImePartnera.Text = "";
        //    txtUnos.Select();
        //}

        private DataSet DTpartner_ = new DataSet();

        private void btnPartner_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DTpartner_ = classSQL.select("SELECT * FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (DTpartner_.Tables[0].Rows.Count > 0)
                {
                    sifraPartnera = DTpartner_.Tables[0].Rows[0]["id_partner"].ToString();
                    txtImePartnera.Text = DTpartner_.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    dodjeli_popust = 0;
                    PartnerPostaviPopust();
                }
                else
                {
                    sifraPartnera = "0";
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
            txtUnos.Select();
        }

        /// <summary>
        /// FUNKCIJA POSTAVLJA POPUST NA SVE STAVKE
        /// </summary>

        private int dodjeli_popust = 0;

        private void PartnerPostaviPopust()
        {
            decimal rabat = 0, rabatukupno = 0;

            if (DTpartner_ != null)
            {
                decimal.TryParse(DTpartner_.Tables[0].Rows[0]["popust"].ToString(), out rabat);

                if (dodjeli_popust == -1 || rabat == 0)
                {
                    return;
                }
                else if (dodjeli_popust != -1 && dodjeli_popust != 1)
                {
                    if (MessageBox.Show("Ovaj partner ima popust od " + rabat.ToString() + "%\r\nDali ste sigurni da želite dodijeliti navedeni popust.", "Popust na račun", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        dodjeli_popust = 1;
                    else
                        dodjeli_popust = -1;
                }

                foreach (DataGridViewRow r in dgv.Rows)
                {
                    decimal cijena = Convert.ToDecimal(r.Cells[4].Value.ToString());
                    decimal kolicina = Convert.ToDecimal(r.Cells[5].Value.ToString());
                    decimal ukupno = 0;
                    ukupno = cijena * kolicina;
                    r.Cells["popust"].Value = Math.Round(rabat, 5).ToString("#0.00000");

                    rabatukupno = ukupno * rabat / 100;
                    r.Cells[7].Value = Math.Round(ukupno - rabatukupno, 2).ToString("#0.00");
                    IzracunUkupno();
                }
            }
        }

        /// <summary>
        /// Ako je uključeno ispisivanje napomene na računu onda vraća napomenu iz male forme
        /// </summary>
        /// <returns></returns>
        private string IspisNapomene()
        {
            string napomena = "";
            if (DSpostavke.Tables[0].Rows.Count > 0)
            {
                if (DSpostavke.Tables[0].Rows[0]["napomena_na_racunu"].ToString() == "1")
                {
                    Kasa.frmNapomenaRacun nap = new Kasa.frmNapomenaRacun();
                    nap.ShowDialog();
                    napomena = nap.napomena;
                }
            }

            return napomena;
        }

        private decimal istra_partner_dan, istra_partner_mjesec;

        private void btnGotovina_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku.", "Greška");
                return;
            }
            if (!Class.Postavke.dozvoli_fikaliranje_0_kn)
            {
                if (ukupno == 0)
                {
                    MessageBox.Show("Ne možete fiskalizirati račun sa 0kn.", "Greška");
                    return;
                }
            }

            #region ************************************OVO JE ZA KARABANA IZ ZADRA*********************************

            try
            {
                if (DTpodaci.Rows[0]["oib"].ToString() == "67521709619" && sifraPartnera != "0")
                {
                    DataTable DTpartner = classSQL.select("SELECT id_partner,ime_tvrtke,vrsta_korisnika FROM partners WHERE id_partner ='" + sifraPartnera + "'", "partners").Tables[0];

                    if (DTpartner.Rows.Count > 0)
                    {
                        DataTable DTmjesec = classSQL.select("SELECT SUM(ukupno_mpc) FROM racuni " +
                            " WHERE id_kupac='" + sifraPartnera + "'" +
                            " AND datum_racuna>='" + DateTime.Now.ToString("yyyy-MM-01 00:00:01") + "'" +
                            "", "racuni").Tables[0];

                        //DataTable DTdan = classSQL.select("SELECT SUM(ukupno_mpc) FROM racuni " +
                        //    " WHERE id_kupac='" + sifraPartnera + "'" +
                        //    " AND datum_racuna>='" + DateTime.Now.ToString("yyyy-MM-dd 00:00:01") + "'" +
                        //    "", "racuni").Tables[0];

                        //if (DTdan.Rows.Count > 0) { decimal.TryParse(DTdan.Rows[0][0].ToString(), out istra_partner_dan); } else { istra_partner_dan = 0; }
                        if (DTmjesec.Rows.Count > 0) { decimal.TryParse(DTmjesec.Rows[0][0].ToString(), out istra_partner_mjesec); } else { istra_partner_mjesec = 0; }

                        if (DTpartner.Rows[0]["vrsta_korisnika"].ToString() == "0")
                        {
                            if (istra_partner_mjesec + (decimal)ukupno > 1508)
                            {
                                MessageBox.Show("Mjesečni limit je 1508,00 kn a Vaš iznos je " + (istra_partner_mjesec + (decimal)ukupno).ToString("#0.00") + "kn.", "Limit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            if ((decimal)ukupno > 100)
                            {
                                MessageBox.Show("Dnevni limit je 100,00 kn a Vaš iznos na računu je " + ukupno.ToString("#0.00") + "kn.", "Limit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //******************************************OVO JE ZA KARABANA IZ ZADRA  KRAJ*********************************

            #endregion ************************************OVO JE ZA KARABANA IZ ZADRA*********************************

            #region PROVJERAVA DALI JE DOBRA GODINA

            try
            {
                PCPOS.Util.classFukcijeZaUpravljanjeBazom B = new Util.classFukcijeZaUpravljanjeBazom("MALOPRODAJA", "POS");
                int g = B.UzmiGodinuKojaSeKoristi();
                if (g != DateTime.Now.Year)
                {
                    if (g != 0)
                    {
                        MessageBox.Show("UPOZORENJE!!!\r\nVrlo vjerovatno koristite krivu godinu za izradu računa!\r\n" +
                            "Zakonom o fiskalizaciji to nije dozvoljeno.\r\n Za više informacija kontaktirajte Code-iT.\r\n", "Upozorenje.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            catch
            {
            }

            #endregion PROVJERAVA DALI JE DOBRA GODINA

            frmUzvratiti objForm2 = new frmUzvratiti();
            IznosGotovina = null;
            IznosKartica = null;
            IznosBon = null;
            objForm2.getUkupnoKasa = ukupno.ToString();
            objForm2.getNacin = "GO";
            objForm2.MainForm = this;
            objForm2.ShowDialog();

            if (IznosKartica != null && IznosGotovina != null && IznosBon != null)
            {

                if (Class.Postavke.UDSGame && udsCompany != null && udsCustomer != null && CODE != 0 && !UDSAPI_APPLY_DISCOUNT)
                {
                    //bool udsDobarUnos = false;
                    //while (!udsDobarUnos) {
                    //   decimal[] rez = UDSAPI.getScoreToSubstractFromCustomerAccount((decimal)ukupnoBezRabata, UDSAPI_MAX_SCORES_DISCOUNT);

                    //    udsDobarUnos = Convert.ToBoolean(rez[0]);
                    //    UDSAPI_SCORES_TO_SUBSTRACT_FROM_CUSTOMER_ACCOUNT = rez[1];
                    //}

                    //if (udsDobarUnos) {
                    //}
                }
                else
                {
                    UDSAPI_SCORES_TO_SUBSTRACT_FROM_CUSTOMER_ACCOUNT = 0;
                }

                SpremiRacun(IznosKartica, IznosGotovina, IznosBon);


                if (Class.Postavke.UDSGame && udsCompany != null && udsCustomer != null && CODE != 0)
                {
                    UDSAPI.postPurchase(udsCompany, CODE, (udsCustomer.participantId == null ? 0 : udsCustomer.participantId), (Class.Postavke.UDSGameEmployees ? Properties.Settings.Default.id_zaposlenik : ""), string.Format("{0}/{1}/{2}", brRac, Class.Postavke.default_poslovnica, Class.Postavke.maloprodaja_naplatni_uredaj), (decimal)ukupnoBezRabata, UDSAPI_SCORES_TO_SUBSTRACT_FROM_CUSTOMER_ACCOUNT);
                    btnDodajNaPartnera.PerformClick();
                    UDSAPI_SCORES_TO_SUBSTRACT_FROM_CUSTOMER_ACCOUNT = 0;
                    ukupnoBezRabata = 0;
                }


                PregledKolicine();
                NoviUnos();
                txtUnos.Select();
            }

            //Kartoteka
            if (IznosKartica != null && IznosGotovina != null)
            {
                DataTable DSaktivnosDok = classSQL.select_settings("SELECT kartoteka FROM aktivnost_podataka", "aktivnost_podataka").Tables[0];
                if (DSaktivnosDok.Rows[0]["kartoteka"].ToString() == "1")
                {
                    DialogResult dialogResult = MessageBox.Show("Želite li dodati račun u kronologiju ?", "Odabir", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (Properties.Settings.Default.id_partner == "0" || Properties.Settings.Default.id_partner == "")
                        {
                            frmPartnerTrazi p = new frmPartnerTrazi();
                            p._pozivaKartoteku = true;
                            p.prenos_broj_racuna = ZadnjiRacun;
                            p.kartoteka_ulj = true;
                            p.ShowDialog();
                        }
                        else if (Properties.Settings.Default.id_partner != "0" || Properties.Settings.Default.id_partner != "")
                        {
                            string id_par = Properties.Settings.Default.id_partner;
                            Kartoteka.galerija novo = new Kartoteka.galerija();

                            string popuni_gal = "SELECT * FROM partners WHERE id_partner = '" + id_par + "'";
                            DataTable DTpopunigaleriju = classSQL.select(popuni_gal, "popuni_galeriju").Tables[0];
                            novo.partner_id = id_par.ToString();
                            novo.ime_kupca = DTpopunigaleriju.Rows[0]["ime"].ToString();
                            novo.prezime_kupca = DTpopunigaleriju.Rows[0]["prezime"].ToString();
                            novo.ime_tvrtke = DTpopunigaleriju.Rows[0]["ime_tvrtke"].ToString();
                            novo.broj_racuna = ZadnjiRacun;
                            novo.ShowDialog();
                        }
                    }
                }
            }
            DTpartner_ = null;
        }

        private void PregledKolicine()
        {
            //OVO RADI SPREMLJENA PROCEDURA
            try
            {
                if (Class.Postavke.skidajSkladisteProgramski)
                {
                    string _sql = "";

                    foreach (DataGridViewRow r in dgv.Rows)
                    {
                        _sql += "SELECT postavi_kolicinu_sql_funkcija_prema_sifri('" + r.Cells["sifra"].FormattedValue.ToString() + $"'{Global.Functions.GetDateParam()}'') AS odgovor; ";
                    }

                    frmLoad l = new frmLoad();
                    l.Text = "Radim provjeru skladišta";
                    l.Show();
                    classSQL.insert(_sql);
                    l.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnKartica_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku.", "Greška");
                return;
            }

            if (!Class.Postavke.dozvoli_fikaliranje_0_kn)
            {
                if (ukupno == 0)
                {
                    MessageBox.Show("Ne možete fiskalizirati račun sa 0kn.", "Greška");
                    return;
                }
            }

            #region PROVJERAVA DALI JE DOBRA GODINA

            try
            {
                PCPOS.Util.classFukcijeZaUpravljanjeBazom B = new Util.classFukcijeZaUpravljanjeBazom("MALOPRODAJA", "POS");
                int g = B.UzmiGodinuKojaSeKoristi();
                if (g != DateTime.Now.Year)
                {
                    if (g != 0)
                    {
                        MessageBox.Show("UPOZORENJE!!!\r\nVrlo vjerovatno koristite krivu godinu za izradu računa!\r\n" +
                            "Zakonom o fiskalizaciji to nije dozvoljeno.\r\n Za više informacija kontaktirajte Code-iT.\r\n", "Upozorenje.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            catch
            {
            }

            #endregion PROVJERAVA DALI JE DOBRA GODINA

            bool dodano_zbog_kartice = false;
            if (File.Exists("dodatno_na_kartice"))
            {
                string postotak = File.ReadAllText("dodatno_na_kartice");
                decimal rabat;

                if (decimal.TryParse(postotak, out rabat))
                {
                    if (MessageBox.Show("Dali želite na cijenu dodati " + postotak + " posto zbog plaćanja na karticu?", "Kartice", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dodano_zbog_kartice = true;
                        for (int i = 0; i < dgv.RowCount; i++)
                        {
                            decimal mpc = 0;
                            decimal vpc = 0;
                            decimal cijena = Convert.ToDecimal(dgv.Rows[i].Cells["cijena"].Value.ToString());
                            decimal porez = Convert.ToDecimal(dgv.Rows[i].Cells["porez"].FormattedValue.ToString());
                            decimal kol = Convert.ToDecimal(dgv.Rows[i].Cells["kolicina"].FormattedValue.ToString());
                            decimal pop = Convert.ToDecimal(dgv.Rows[i].Cells["popust"].FormattedValue.ToString());

                            mpc = cijena + (cijena * rabat / 100);

                            decimal preracunataStopa = (100 * porez) / (porez + 100);
                            vpc = mpc - (mpc * preracunataStopa / 100);

                            dgv.Rows[i].Cells["cijena"].Value = mpc.ToString("#0.00");
                            dgv.Rows[i].Cells["vpc"].Value = vpc.ToString("#0.000");

                            dgv.Rows[i].Cells["iznos"].Value = Math.Round(mpc * kol, 2).ToString("#0.00");
                        }
                    }
                    IzracunUkupno();
                }
            }

            frmUzvratiti objForm2 = new frmUzvratiti();
            objForm2.getUkupnoKasa = ukupno.ToString();
            IznosGotovina = null;
            IznosKartica = null;
            IznosBon = null;
            objForm2.getNacin = "KA";
            objForm2.MainForm = this;
            objForm2.ShowDialog();

            if (IznosKartica != null && IznosGotovina != null && IznosBon != null)
            {
                SpremiRacun(IznosKartica, IznosGotovina);
                PregledKolicine();
                NoviUnos();
                txtUnos.Select();
            }
            else
            {
                if (dodano_zbog_kartice)
                {
                    string postotak = File.ReadAllText("dodatno_na_kartice");
                    decimal rabat;

                    if (decimal.TryParse(postotak, out rabat))
                    {
                        for (int i = 0; i < dgv.RowCount; i++)
                        {
                            decimal mpc = 0;
                            decimal vpc = 0;
                            decimal cijena = Convert.ToDecimal(dgv.Rows[i].Cells["cijena"].Value.ToString());
                            decimal porez = Convert.ToDecimal(dgv.Rows[i].Cells["porez"].FormattedValue.ToString());
                            decimal kol = Convert.ToDecimal(dgv.Rows[i].Cells["kolicina"].FormattedValue.ToString());
                            decimal pop = Convert.ToDecimal(dgv.Rows[i].Cells["popust"].FormattedValue.ToString());

                            mpc = cijena;
                            decimal preracunataStopa_rabat = (100 * rabat) / (rabat + 100);
                            mpc = mpc - (mpc * preracunataStopa_rabat / 100);

                            decimal preracunataStopa = (100 * porez) / (porez + 100);
                            vpc = mpc - (mpc * preracunataStopa / 100);

                            dgv.Rows[i].Cells["cijena"].Value = mpc.ToString("#0.00");
                            dgv.Rows[i].Cells["vpc"].Value = vpc.ToString("#0.000");

                            dgv.Rows[i].Cells["iznos"].Value = Math.Round(mpc * kol, 2).ToString("#0.00");
                            IzracunUkupno();
                        }
                    }
                    IzracunUkupno();
                }
            }
            DTpartner_ = null;
        }

        private void btnOdustaniCB_Click(object sender, EventArgs e)
        {
            txtBrojKarticeCB.Text = "";
            txtPodaciOvlasnikuCB.Text = "";
        }

        private void btnOdustaniSB_Click(object sender, EventArgs e)
        {
            txtBrojKarticeSB.Text = "";
            txtPodaciOvlasnikuSB.Text = "";
        }

        private void btnOdustaniPO_Click(object sender, EventArgs e)
        {
            txtBrojKarticePO.Text = "";
            txtPodaciOvlasnikuPO.Text = "";
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            frmRobaTrazi roba = new frmRobaTrazi();
            roba.ShowDialog();

            if (Properties.Settings.Default.id_roba != "")
            {
                txtUnos.Text = Properties.Settings.Default.id_roba;
                txtUnos.Select();
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    if (dgv.RowCount > 0)
        //    {
        //        Kasa.frmPromjenaCijene pr = new Kasa.frmPromjenaCijene();
        //        pr.idSkladiste = CBskladiste.SelectedValue.ToString();
        //        pr.sifra = dgv.CurrentRow.Cells["sifra"].FormattedValue.ToString();
        //        pr.porez = dgv.CurrentRow.Cells["porez"].FormattedValue.ToString();
        //        pr.ShowDialog();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Nemate niti jednu stavku.");
        //    }

        //    //decimal dec_parse;
        //    //if (Decimal.TryParse(txtUnos.Text, out dec_parse))
        //    //{
        //    //    txtUnos.Text = dec_parse.ToString();
        //    //}
        //    //else
        //    //{
        //    //    MessageBox.Show("Greška kod upisa odredišta.", "Greška"); return;
        //    //}

        //    //// ovo je izračun ako odredite MPC automatski računa popust
        //    //double VPC = Convert.ToDouble(dgv.CurrentRow.Cells["vpc"].FormattedValue.ToString());
        //    //double StariMPC = (VPC * Convert.ToDouble(dgv.CurrentRow.Cells["porez"].FormattedValue.ToString()) / 100) + VPC;

        //    //dgv.CurrentRow.Cells["popust"].Value = Convert.ToString(((Convert.ToDouble(txtUnos.Text) / StariMPC - 1) * 100) * (-1));
        //    //if (dgv.CurrentRow.Cells["popust"].FormattedValue.ToString().Length > 10)
        //    //{
        //    //    dgv.CurrentRow.Cells["popust"].Value = dgv.CurrentRow.Cells["popust"].FormattedValue.ToString().Remove(10);
        //    //}

        //    //SetRabat(Convert.ToDouble(dgv.CurrentRow.Cells["popust"].FormattedValue.ToString()));
        //    //txtUnos.Text = "";
        //    //txtUnos.Select();

        //}

        private void btnKolicina_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku.", "Greška");
                return;
            }
            //if (isNumeric(txtUnos.Text, System.Globalization.NumberStyles.AllowDecimalPoint) == false) { MessageBox.Show("Greška kod upisa količine.", "Greška"); return; }

            decimal dec_parse;
            if (Decimal.TryParse(Korisno.vaga ? lblVagaValue.Text : txtUnos.Text, out dec_parse))
            {
                if (Korisno.vaga)
                {
                    lblVagaValue.Text = dec_parse.ToString();
                }
                else
                {
                    double kol = Convert.ToDouble(dgv.Rows[dgv.CurrentRow.Index].Cells[5].Value);
                    if ((dec_parse <= 0 || kol <= 0) && !Class.Postavke.kolicina_u_minus)
                    {
                        MessageBox.Show("Krivi unos za količinu");
                        return;
                    }
                    txtUnos.Text = dec_parse.ToString();
                }
            }
            else
            {
                MessageBox.Show("Greška kod upisa količine.", "Greška"); return;
            }

            string sifra = dgv.CurrentRow.Cells["sifra"].Value.ToString();
            double kolicina = Convert.ToDouble(Korisno.vaga ? lblVagaValue.Text : txtUnos.Text);
            dgv.CurrentRow.Cells["kolicina"].Value = kolicina.ToString();

            SetKolicina(kolicina);

            //i za kauciju povećaj količine
            double kolicinaKaucije;
            DataTable DTkaucijaPostoji = classSQL.select("SELECT sifra_kaucija, kolicina FROM roba_kaucija" +
                " WHERE sifra='" + sifra + "'", "roba").Tables[0];

            if (DTkaucijaPostoji.Rows.Count > 0)
            {
                for (int i = 0; i < DTkaucijaPostoji.Rows.Count; i++)
                {
                    for (int j = 0; j < dgv.Rows.Count; j++)
                    {
                        if (DTkaucijaPostoji.Rows[i]["sifra_kaucija"].ToString() == dgv.Rows[j].Cells["sifra"].Value.ToString())
                        {
                            dgv.CurrentCell = dgv.Rows[j].Cells[3];
                            kolicinaKaucije = kolicina * Convert.ToDouble(DTkaucijaPostoji.Rows[i]["kolicina"].ToString());
                            dgv.CurrentRow.Cells["kolicina"].Value = kolicinaKaucije;
                            SetKolicina(kolicinaKaucije);
                        }
                    }
                }
            }

            IzracunUkupno();
            txtUnos.Text = "";
            txtUnos.Select();
        }

        private void btnRabat_Click(object sender, EventArgs e)
        {

            if (dgv.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku.", "Greška");
                return;
            }

            if (isNumeric(txtUnos.Text, System.Globalization.NumberStyles.AllowDecimalPoint) == false)
            {
                MessageBox.Show("Greška kod upisa rabata.", "Greška");
                return;
            }

            double kol = Convert.ToDouble(dgv.CurrentRow.Cells[5].Value);
            if (kol <= 0 && !Class.Postavke.kolicina_u_minus)
            {
                MessageBox.Show("Nije dozvoljeno mijenjanje rabata artiklu koji je dodan u minus.");
                return;
            }

            dgv.CurrentRow.Cells["popust"].Value = txtUnos.Text;
            SetRabat(Convert.ToDouble(txtUnos.Text));
            txtUnos.Text = "";
            txtUnos.Select();
        }

        private void btnOdjava_Click(object sender, EventArgs e)
        {
            dodjeli_popust = 0;
            DTpartner_ = null;
            this.Close();
            this.Dispose();
        }

        private void btnAlati_Click(object sender, EventArgs e)
        {
            frmKasaOpcije ko = new frmKasaOpcije();
            ko.Owner = this;
            ko.sifraPartnera = sifraPartnera;
            ko.ShowDialog();
            txtUnos.Select();
        }

        private void btnObrisiStavku_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount < 1)
            {
                MessageBox.Show("Nemate niti jednu stavku.", "Greška");
                return;
            }
            //else if (dgv.RowCount == 1)
            //{
            //    int current = dgv.CurrentRow.Index;
            //    //int ukupnoRows= dgv.Rows.Count;
            //    //dgv.Rows[ukupnoRows-1].Cells[0].Selected = true;
            //    dgv.Rows.RemoveAt(current);
            //    Ukupno();
            //    txtUnos.Select();
            //}
            else
            {
                int current = dgv.CurrentRow.Index;
                //int ukupnoRows= dgv.Rows.Count;
                //dgv.Rows[ukupnoRows-1].Cells[0].Selected = true;
                dgv.Rows.RemoveAt(current);
                IzracunUkupno();
                txtUnos.Select();
            }
        }

        #endregion buttons

        #region Keydown

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                int col = dgv.CurrentCell.ColumnIndex;
                int row = dgv.CurrentCell.RowIndex;

                if (col < dgv.ColumnCount - 1)
                {
                    col++;
                }
                else
                {
                    col = 0;
                    row++;
                }

                if (row == dgv.RowCount)
                    dgv.Rows.Add();

                //SendKeys.Send("{TAB}");

                //datagridview.CurrentCell = datagridview[col, row];
                dgv.BeginEdit(true);
                e.Handled = true;
            }

            if (e.KeyData == Keys.Right)
            {
                e.Handled = true;
                DataGridViewCell cell =
                dgv.Rows[0].Cells[dgv.CurrentCell.ColumnIndex];
                dgv.CurrentCell = cell;
                dgv.BeginEdit(true);
                //dgv.CurrentCell=dgv.CurrentRow.Cells[3];
            }
        }

        private void EditKeyDown(object sender, KeyEventArgs e)
        {
            string a = "";

            if (e.KeyCode == Keys.F1)
            {
                btnKolicina_Click(btnKolicina, e);
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnRabat_Click(btnRabat, e);
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnPromjenaCijene_Click(btnPromjenaCijene, e);
            }
            else if (e.KeyCode == Keys.F4)
            {
                btnTrazi_Click(btnTrazi, e);
            }
            else if (e.KeyCode == Keys.F5)
            {
                if (Class.Postavke.maloprodaja_naplata_gotovina_button_show)
                {
                    btnGotovina_Click(btnGotovina, e);
                }
            }
            else if (e.KeyCode == Keys.F6)
            {
                if (Class.Postavke.maloprodaja_naplata_kartica_button_show)
                {
                    btnKartica_Click(btnKartica, e);
                }
            }
            else if (e.KeyCode == Keys.F7)
            {
                btnOdustani_Click(btnOdustaniSve, e);
            }
            else if (e.KeyCode == Keys.F8)
            {
                btnPartner_Click(btnPartner, e);
            }
            else if (e.KeyCode == Keys.F9)
            {
                btnObrisiStavku_Click(btnObrisiStavku, e);
            }
            else if (e.KeyCode == Keys.F10)
            {
                btnAlati_Click(btnAlati, e);
            }
            else if (e.KeyCode == Keys.F11)
            {
            }
            else if (e.KeyCode == Keys.F12)
            {
            }
            else if (e.KeyCode == Keys.Up)
            {
            }
            else if (e.KeyCode == Keys.Down)
            {
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else
            {
                base.OnKeyDown(e);
            }
        }

        private DataTable DTean = new DataTable();

        private void btnOdjava_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownEvent(e);
        }

        private DataTable DTcijenakomadno = new DataTable();
        private bool skladiste_s_drugom_cijenom = false;

        private void provjeri_cijenu(string sifra_)
        {
            string sql = "Select * from promjena_cijene_komadno_stavke Where sifra = '" + sifra_ + "' AND kolicina_ostatak > 0 and id_skladiste = '" + CBskladiste.SelectedValue + "' ";
            DataTable DTcijena = classSQL.select(sql, "provjera").Tables[0];

            if (DTcijena.Rows.Count > 0)
            {
                string sql_cijenakomadno = "Select nova_cijena,kolicina_ostatak from promjena_cijene_komadno_stavke Where sifra = '" + sifra_ + "' AND id_skladiste = '" + CBskladiste.SelectedValue + "' AND datum = ( SELECT MAX(datum) FROM promjena_cijene_komadno_stavke WHERE sifra = '" + sifra_ + "'  AND datum <= current_timestamp )";
                DTcijenakomadno = classSQL.select(sql_cijenakomadno, "cijenakomadno").Tables[0];
                skladiste_s_drugom_cijenom = true;
            }
            else
            {
                DTcijenakomadno.Clear();
                skladiste_s_drugom_cijenom = false;
            }
        }

        private void KeyDownEvent(KeyEventArgs e)
        {
                if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                //provjeri_cijenu(txtUnos.Text);
                DataGridViewRow row = this.dgv.RowTemplate;
                row.DefaultCellStyle.BackColor = Color.Bisque;
                row.Height = 40;

                txtUnos.Text = txtUnos.Text.Trim();
                if (txtUnos.Text == "")
                {
                    frmRobaTrazi roba = new frmRobaTrazi();
                    roba.ShowDialog();

                    if (Properties.Settings.Default.id_roba != "")
                    {
                        txtUnos.Text = Properties.Settings.Default.id_roba;
                        //CBskladiste.SelectedValue = Convert.ToInt32(Properties.Settings.Default.idSkladiste);
                        //vecSelektiran = false;
                        txtUnos.Select();

                        //vecSelektiran = false;

                        CBskladiste.SelectedValue = Properties.Settings.Default.idSkladiste;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    Properties.Settings.Default.idSkladiste = CBskladiste.SelectedValue.ToString();
                    //vecSelektiran = false;
                }

                string idSkladiste = Properties.Settings.Default.idSkladiste;

                string sifra = CheckEan(txtUnos.Text) ?? txtUnos.Text;

                if (Util.Korisno.ProvijeriIUpozoriAkoNemaNaSkladistu(sifra, Properties.Settings.Default.idSkladiste, DSpostavke.Tables[0]))
                    SetRoba(sifra);

                return;
            }
            else if (e.KeyCode == Keys.F1)
            {
                btnKolicina_Click(btnKolicina, e);
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnRabat_Click(btnRabat, e);
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnPromjenaCijene_Click(btnPromjenaCijene, e);
            }
            else if (e.KeyCode == Keys.F4)
            {
                btnTrazi_Click(btnTrazi, e);
            }
            else if (e.KeyCode == Keys.F5)
            {
                if (Class.Postavke.maloprodaja_naplata_gotovina_button_show)
                {
                    btnGotovina_Click(btnGotovina, e);
                }
            }
            else if (e.KeyCode == Keys.F6)
            {
                if (Class.Postavke.maloprodaja_naplata_kartica_button_show)
                {
                    btnKartica_Click(btnKartica, e);
                }
            }
            else if (e.KeyCode == Keys.F7)
            {
                btnOdustani_Click(btnOdustaniSve, e);
            }
            else if (e.KeyCode == Keys.F8)
            {
                btnPartner_Click(btnPartner, e);
            }
            else if (e.KeyCode == Keys.F9)
            {
                btnObrisiStavku_Click(btnObrisiStavku, e);
            }
            else if (e.KeyCode == Keys.F10)
            {
                btnAlati_Click(btnAlati, e);
            }
            else if (e.KeyCode == Keys.F11)
            {
                btnDodajNaPartnera.PerformClick();
            }
            else if (e.KeyCode == Keys.F12)
            {
                if (popustABS) btnPopust.PerformClick();
                else btnPopustSve.PerformClick();
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (dgv.Rows.Count > 0)
                {
                    int curr = dgv.CurrentCell.RowIndex;
                    curr--;
                    if (curr >= 0)
                    {
                        dgv.CurrentCell = dgv.Rows[curr].Cells[0];
                        dgv.Rows[curr].Selected = true;
                        getKolicinaNaSkladistu();
                    }
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (dgv.Rows.Count > 0)
                {
                    int curr = dgv.CurrentCell.RowIndex;
                    curr++;
                    if (curr < dgv.Rows.Count)
                    {
                        dgv.CurrentCell = dgv.Rows[curr].Cells[0];
                        dgv.Rows[curr].Selected = true;
                        getKolicinaNaSkladistu();
                    }
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            txtUnos.Select();
        }

        //private void txtUnos_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if(e.KeyCode==Keys.Enter)
        //    {
        //        DataGridViewRow row = this.dgv.RowTemplate;
        //        row.DefaultCellStyle.BackColor = Color.Bisque;
        //        row.Height = 40;

        //        if (txtUnos.Text.Length == 2)
        //        {
        //            SetKolicina(Convert.ToDouble(txtUnos.Text));
        //        }

        //        SetRoba(txtUnos.Text);
        //        txtUnos.Text = "";
        //        return;
        //    }

        //    if (dgv.RowCount > 0)
        //    {
        //        if (e.KeyCode == Keys.Up)
        //        {
        //            int cr = dgv.SelectedRows.Count-1;
        //            if(cr>=0)
        //            {
        //                dgv.Rows[cr].Selected = true;
        //            }
        //            txtUnos.Select();
        //        }

        //        if (e.KeyCode == Keys.Down)
        //        {
        //            int cr = dgv.SelectedRows.Count+1;
        //            if (cr <= dgv.Rows.Count)
        //            {
        //                dgv.Rows[cr].Selected = true;
        //            }
        //            txtUnos.Select();
        //        }
        //    }
        //}

        #endregion Keydown

        #region Datagridview helpers

        private string dg(int row, string cell)
        {
            string ret = dgv.Rows[row].Cells[cell].FormattedValue.ToString();
            return ret;
        }

        private void dgv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //if(dgv.CurrentCell.ColumnIndex==3)
            //{
            //    var txtEdit = (ComboBox)e.Control;
            //    txtEdit.KeyDown += EditKeyDown;
            //}
            //else
            //{
            //    var txtEdit = (TextBox)e.Control;
            //    txtEdit.KeyDown += EditKeyDown;
            //}
        }

        private void dgv_KeyDown(object sender, KeyEventArgs e)
        {
            //if (dgv.RowCount > 0)
            //{
            //    if (e.KeyCode == Keys.Up)
            //    {
            //        int cr = dgv.CurrentRow.Index-1;
            //        dgv.Rows[cr].Selected = true;
            //    }

            //    if (e.KeyCode == Keys.Down)
            //    {
            //        int cr = dgv.CurrentRow.Index+1;
            //        dgv.Rows[cr].Selected = true;
            //    }
            //}
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getKolicinaNaSkladistu();
        }

        private void getKolicinaNaSkladistu()
        {
            if (dgv.Rows.Count > 0)
            {
                if (dg(dgv.CurrentRow.Index, "sifra") == "!popustABS")
                {
                    lblSkladiste.Text = "";
                    return;
                }

                //vecSelektiran = false;
                odabranoSkladiste = dg(dgv.CurrentRow.Index, "skladiste");
                CBskladiste.SelectedValue = dgv.CurrentRow.Cells["skladiste"].FormattedValue.ToString();
                GetKolicinaSkladiste(dg(dgv.CurrentRow.Index, "sifra"), CBskladiste.SelectedValue.ToString(),
                    dg(dgv.CurrentRow.Index, "jmj"));
            }
        }

        #endregion Datagridview helpers

        #region Form components

        private void frmKasa_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if(spVaga.IsOpen) spVaga.Close();

            if (Korisno.vaga)
            {
                timRefreshVaga.Stop();

                if (spVaga.IsOpen)
                {
                    //spVaga.Dispose();
                    spVaga.Close();
                }
            }
            //if (DTpostavkePrinter.Rows[0]["posPrinterBool"].ToString() == "1")
            //{
            //    //PosPrint.classPosPrint.DisconnectFromPrinter();
            //}
            //if (DTpostavkePrinter.Rows[0]["lineDisplay_bool"].ToString() == "1")
            //{
            //    //PosPrint.classLineDisplay.WriteOnDisplay("");
            //    // PosPrint.classLineDisplay.CloseDisplay();
            //}
            //if (DTpostavkePrinter.Rows[0]["drawer_bool"].ToString() == "1")
            //{
            //    //PosPrint.classCashDrawer.ClosePortCashDrawer();
            //}
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w + 3, h + 3);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w - 3, h - 3);
        }

        private void SetSize()
        {
            //if(this.Width>1300)
            //{
            //    panTastatura.Visible = true;
            //}
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 500);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void CBskladiste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUnos.Select();
            }
        }

        //private void CBskladiste_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (dgv.RowCount > 0)
        //    {
        //        dgv.CurrentRow.Cells["skladiste"].Value = CBskladiste.SelectedValue;
        //        SetSkladiste(CBskladiste.SelectedValue.ToString());
        //    }
        //}

        private void CBskladiste_SelectionChangeCommited(object sender, EventArgs e)
        {
            //if (!vecSelektiran)
            //{
            //    vecSelektiran = true;
            //    return;
            //}

            if (dgv.RowCount > 0)
            {
                ////ako robe nema na skladištu pita korisnika da li želi skinuti
                ////ako želi funkcija vraća TRUE
                ////ako ne želi vraća FALSE i izlazi iz metode SetRoba
                if (!GetKolicinaSkaldisteUpozorenje(dg(dgv.CurrentRow.Index, "sifra"), CBskladiste.SelectedValue.ToString(), false))
                {
                    //mora vratiti skladiste na početno skladište jer inače u datagridu ostaje novoodabrano skladište
                    //s kojeg se ne smije skidati!
                    //vecSelektiran = false;
                    CBskladiste.SelectedValue = odabranoSkladiste;
                    return;
                }

                SetSkladiste(CBskladiste.SelectedValue.ToString());

                GetKolicinaSkladiste(dg(dgv.CurrentRow.Index, "sifra"), CBskladiste.SelectedValue.ToString(), dg(dgv.CurrentRow.Index, "jmj"));

                odabranoSkladiste = CBskladiste.SelectedValue.ToString();

                Util.Korisno.ProvijeriIUpozoriAkoNemaNaSkladistu(dg(dgv.CurrentRow.Index, "sifra"), odabranoSkladiste, DSpostavke.Tables[0]);
            }

            //vecSelektiran = true;
        }

        #endregion Form components

        private void button1_Click_1(object sender, EventArgs e)
        {
            KeyEventArgs k = new KeyEventArgs(Keys.Enter);
            //k.KeyCode = Keys.Enter;
            KeyDownEvent(k);
        }

        private void btnPromjenaCijene_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownEvent(e);
        }

        private void btnPromjenaCijene_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                double kol = Convert.ToDouble(dgv.CurrentRow.Cells[5].Value);
                if (kol <= 0 && !Class.Postavke.kolicina_u_minus)
                {
                    MessageBox.Show("Nije dozvoljeno mijenjanje cijene artiklu koji je dodan u minus.");
                    return;
                }

                decimal dec_parse;
                if (!Decimal.TryParse(txtUnos.Text, out dec_parse))
                {
                    txtUnos.Text = "0";
                    MessageBox.Show("Krivi unos za cijenu.");
                }

                decimal mpc = 0;
                decimal vpc = 0;
                decimal porez = Convert.ToDecimal(dgv.CurrentRow.Cells["porez"].FormattedValue.ToString());

                mpc = Math.Round(Convert.ToDecimal(txtUnos.Text), 2);

                objDrugiPorezi = korisno.Vrati_PorezDohodak_Prirez(mpc, sifraPartnera, DSpostavke.Tables[0], true);
                if (objDrugiPorezi[0] != null && objDrugiPorezi[1] != null && !chbNeObracunavajPorezPrirez.Checked)
                {
                    {
                        decimal prirezD, porezNaDohodakD, osnovica, porezNaDohodakVal, prirezVal;
                        decimal.TryParse(objDrugiPorezi[0].ToString().Trim(), out porezNaDohodakD);
                        decimal.TryParse(objDrugiPorezi[1].ToString().Trim(), out prirezD);
                        decimal.TryParse(objDrugiPorezi[2].ToString().Trim(), out osnovica);
                        decimal.TryParse(objDrugiPorezi[3].ToString().Trim(), out porezNaDohodakVal);
                        decimal.TryParse(objDrugiPorezi[4].ToString().Trim(), out prirezVal);

                        dgv.CurrentRow.Cells["porez_na_dohodak"].Value = porezNaDohodakVal;
                        dgv.CurrentRow.Cells["prirez"].Value = prirezVal;
                        dgv.CurrentRow.Cells["porez_na_dohodak_iznos"].Value = porezNaDohodakD;
                        dgv.CurrentRow.Cells["prirez_iznos"].Value = prirezD;

                        mpc = Math.Round((osnovica - (porezNaDohodakD + prirezD)), 2);
                    }
                }

                //porez = Convert.ToDecimal(txtUnos.Text);
                decimal preracunataStopa = (100 * porez) / (porez + 100);
                vpc = mpc - (mpc * preracunataStopa / 100);

                dgv.CurrentRow.Cells["cijena"].Value = mpc.ToString("#0.00");
                dgv.CurrentRow.Cells["vpc"].Value = vpc.ToString("#0.000");
                //dgv.CurrentRow.Cells["porez"].Value = porez;
                SetKolicina(Convert.ToDouble(dgv.CurrentRow.Cells["kolicina"].Value));
                IzracunUkupno();
                txtUnos.Text = "";
            }
            else
            {
                MessageBox.Show("Nemate niti jednu stavku.");
            }
        }

        private void frmKasa_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (variable.Zaposlenici().ToString().ToLower() == "korisnik")
            {
                frmKasaPrijava log = new frmKasaPrijava();
                log.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku.", "Greška");
                return;
            }

            DTsend = new DataTable();
            DTsend.Columns.Add("broj_racuna");
            DTsend.Columns.Add("sifra_robe");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("mpc");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("nbc");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("cijena");
            DTsend.Columns.Add("ime");
            DTsend.Columns.Add("porez_potrosnja");
            DTsend.Columns.Add("povratna_naknada");
            DataRow row;

            DataTable DTtemp;

            string g;
            string k;

            string iznosGotovinaTest = lblUkupno.Text.Replace("Kn", "").Trim();
            string IznosKarticaTest = "0";
            string DobivenoGotovinaTest = "0";

            if (iznosGotovinaTest != null && iznosGotovinaTest != "0")
            {
                g = "1";
            }
            else
            {
                g = "0";
            }

            if (IznosKarticaTest != null && IznosKarticaTest != "0")
            {
                k = "1";
            }
            else
            {
                k = "0";
            }

            placanje = "O";

            if (Convert.ToDecimal(iznosGotovinaTest) == 0 && Convert.ToDecimal(IznosKarticaTest) > 0)
            {
                placanje = "K";
            }
            else if (Convert.ToDecimal(iznosGotovinaTest) > 0 && Convert.ToDecimal(IznosKarticaTest) == 0)
            {
                placanje = "G";
            }
            else if (Convert.ToDecimal(iznosGotovinaTest) > 0 && Convert.ToDecimal(IznosKarticaTest) > 0)
            {
                placanje = "O";
            }

            //trenutno nije implementirano
            IznosVirman = "0.00";
            //trenutno nije implementirano

            DateTime dRac = Convert.ToDateTime(DateTime.Now);
            string dt = dRac.ToString("yyyy-MM-dd H:mm:ss");

            string uk1 = ukupno.ToString();
            string dobiveno_gotovina;
            if (classSQL.remoteConnectionString == "")
            {
                uk1 = uk1.Replace(",", ".");
                iznosGotovinaTest = iznosGotovinaTest.Replace(",", ".");
                IznosKarticaTest = IznosKarticaTest.Replace(",", ".");
                IznosVirman = IznosKartica.Replace(",", ".");
                dobiveno_gotovina = DobivenoGotovina.Replace(",", ".");
            }
            else
            {
                iznosGotovinaTest = iznosGotovinaTest.Replace(".", ",");
                IznosKarticaTest = IznosKarticaTest.Replace(".", ",");
                IznosVirman = IznosVirman.Replace(".", ",");
                uk1 = uk1.Replace(".", ",");
                dobiveno_gotovina = DobivenoGotovinaTest.Replace(".", ",");
            }

            string napomena = IspisNapomene().Trim();
            brRac = brojRacuna();
            ZadnjiRacun = brRac;

            string sql = "DELETE FROM ispis_racuni; DELETE FROM ispis_racun_stavke;";
            provjera_sql(classSQL.update(sql));

            sql = "INSERT INTO ispis_racuni (broj_racuna,id_kupac,datum_racuna,id_ducan,id_kasa,id_blagajnik," +
                "gotovina,kartice,ukupno_gotovina,ukupno_kartice,broj_kartice_cashback,broj_kartice_bodovi," +
                "br_sa_prethodnog_racuna,ukupno,storno,dobiveno_gotovina,ukupno_virman,napomena,nacin_placanja) " +
                "VALUES (" +
                "'" + brRac + "'," +
                "'" + sifraPartnera + "'," +
                "'" + dt + "'," +
                "'" + id_ducan + "'," +
                "'" + id_kasa + "'," +
                "'" + Properties.Settings.Default.id_zaposlenik + "'," +
                "'" + g + "'," +
                "'" + k + "'," +
                "'" + iznosGotovinaTest + "'," +
                "'" + IznosKarticaTest + "'," +
                "'" + txtBrojKarticeCB.Text + "'," +
                "'" + txtBrojKarticeSB.Text + "'," +
                "'" + txtBrojKarticePO.Text + "'," +
                "'" + uk1.ToString() + "'," +
                "'NE'," +
                "'" + dobiveno_gotovina + "'," +
                "'" + IznosVirman + "'," +
                "'" + napomena + "'," +
                "'" + placanje + "'" +
                ")";

            provjera_sql(classSQL.insert(sql));

            double kolNaknada;
            double povratnaNaknada;
            //--------STAVKE-------------
            string sifra = "";
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                sifra = dg(i, "sifra");

                row = DTsend.NewRow();
                row["broj_racuna"] = brRac;
                row["sifra_robe"] = ReturnSifra(sifra);
                row["id_skladiste"] = dgv.Rows[i].Cells["skladiste"].Value;
                //row["mpc"] = dg(i, "cijena");
                //row["porez"] = dg(i, "porez");
                row["kolicina"] = dg(i, "kolicina");
                row["rabat"] = dg(i, "popust");
                //row["vpc"] = dg(i, "vpc");
                //row["nbc"] = dg(i, "nbc");
                //row["cijena"] = dg(i, "cijena");
                row["ime"] = dg(i, "naziv");
                row["porez_potrosnja"] = "0";

                DTtemp = classSQL.select("SELECT iznos FROM povratna_naknada WHERE sifra='" + sifra + "'", "povratna_naknada")
                    .Tables[0];

                //ako postoji povratna naknada na proizvod
                if (DTtemp.Rows.Count > 0)
                {
                    kolNaknada = Convert.ToDouble(dg(i, "kolicina"));
                    povratnaNaknada = Convert.ToDouble(DTtemp.Rows[0]["iznos"].ToString()) * kolNaknada;
                }
                //ako ne postoji onda je 0
                else
                {
                    povratnaNaknada = 0;
                }

                if (classSQL.remoteConnectionString == "")
                {
                    row["mpc"] = dg(i, "cijena").Replace(",", ".");
                    row["porez"] = dg(i, "porez").Replace(",", ".");
                    row["vpc"] = dg(i, "vpc").Replace(",", ".");
                    row["nbc"] = dg(i, "nbc").Replace(",", ".");
                    row["cijena"] = dg(i, "cijena").Replace(",", ".");
                    row["povratna_naknada"] = povratnaNaknada.ToString("#0.00").Replace(",", ".");
                }
                else
                {
                    row["mpc"] = dg(i, "cijena").Replace(".", ",");
                    row["porez"] = dg(i, "porez").Replace(".", ",");
                    row["vpc"] = dg(i, "vpc").Replace(".", ",");
                    row["nbc"] = dg(i, "nbc").Replace(".", ",");
                    row["cijena"] = dg(i, "cijena").Replace(".", ",");
                    row["povratna_naknada"] = povratnaNaknada.ToString("#0.00").Replace(".", ",");
                }

                DTsend.Rows.Add(row);

                sql = "INSERT INTO ispis_racun_stavke (broj_racuna,sifra_robe,id_skladiste,mpc,porez,kolicina,rabat,vpc,nbc,porez_potrosnja,povratna_naknada)" +
                             " VALUES (" +
                            "'" + DTsend.Rows[i]["broj_racuna"].ToString() + "'," +
                            "'" + DTsend.Rows[i]["sifra_robe"].ToString() + "'," +
                             "'" + DTsend.Rows[i]["id_skladiste"].ToString() + "'," +
                            "'" + DTsend.Rows[i]["mpc"].ToString() + "'," +
                            "'" + DTsend.Rows[i]["porez"].ToString() + "'," +
                             "'" + DTsend.Rows[i]["kolicina"].ToString() + "'," +
                            "'" + DTsend.Rows[i]["rabat"].ToString() + "'," +
                            "'" + DTsend.Rows[i]["vpc"].ToString().Replace(",", ".") + "'," +
                            "'" + DTsend.Rows[i]["nbc"].ToString() + "'," +
                            "'" + DTsend.Rows[i]["porez_potrosnja"].ToString() + "'," +
                            "'" + DTsend.Rows[i]["povratna_naknada"].ToString().Replace(",", ".") + "'" +
                             ")";

                classSQL.select(sql, "ispis_racun_stavke");
            }

            barcode = "000" + brRac;

            DateTime[] datumi = new DateTime[2];
            datumi[0] = DateTime.Now;
            datumi[1] = datumi[0];

            string mali = DTpostavkePrinter.Rows[0]["posPrinterBool"].ToString();
            string a5 = classSQL.select_settings("SELECT a5print FROM postavke", "postavke").Tables[0].Rows[0]["a5print"].ToString();

            try
            {
                //Dodao 24.10.2013
                PosPrint.classPosPrintMaloprodaja2.BoolPreview = true;
                PosPrint.classPosPrintMaloprodaja2.PrintReceipt(DTsend, blagajnik_ime, brRac + "/" +
                    datumi[0].Year.ToString(), sifraPartnera, barcode, brRac, placanje, datumi, true, mali, true, true, "", "");

                if (mali == "1")
                {
                    //već je isprintan u gornjoj metodi
                }
                else if (a5 == "1")
                {
                    printajA5(brRac, true);
                }
                else if (mali != "1")
                {
                    printaj(brRac, true);
                }
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("Desila se pogreška kod ispisa na 'mali' pos printer.\r\n" +
                    "Želite li ispisati ovaj dokument na A4 format?" + Environment.NewLine + Environment.NewLine
                    + ex.Message, "Printer") == DialogResult.Yes)
                {
                    printaj(brRac, true);
                }
            }
        }

        private void btnDodajNaPartnera_Click(object sender, EventArgs e)
        {
            if (Class.Postavke.UDSGame)
            {
                if (udsCompany == null)
                {
                    return;
                }

                if (CODE != 0)
                {
                    CODE = 0;

                    if (udsCustomer != null && dgv.Rows.Count > 0 && UDSAPI_APPLY_DISCOUNT)
                    {
                        dgv.Columns["rabat"].Visible = false;
                        dgv.Columns["rabat"].Width = 55;

                        foreach (DataGridViewRow row in dgv.Rows)
                        {
                            row.Cells["rabat"].Value = 0;
                        }

                        IzracunUkupno();
                    }

                    //if (UDSAPI_APPLY_DISCOUNT)
                    //{
                    btnDodajNaPartnera.Text = "UDS Game";
                    //btnOdustaniKartica.Width = 95;
                    //btnOdustaniKartica.Text = "UDS Game";
                    //}
                    //else
                    //{
                    btnKoristiUds.Enabled = false;
                    btnKoristiUds.Visible = false;

                    //btnKoristiKarticu.Width = 95;
                    //btnKoristiKarticu.Text = "Koristi karticu";
                    //btnKoristiKarticu.Enabled = false;
                    //btnKoristiKarticu.Visible = false;
                    //}

                    udsCustomer = null;

                    return;
                }

                var input = Microsoft.VisualBasic.Interaction.InputBox("Discount code");
                CODE = 0;

                if (int.TryParse(input, out CODE))
                {
                    udsCustomer = UDSAPI.getCustomer(CODE);
                    if (udsCustomer != null)
                    {
                        if (UDSAPI_APPLY_DISCOUNT)
                        {
                            //btnDodajNaPartnera.Width = 196;
                            btnDodajNaPartnera.Text = string.Format("UDS Game odustani za{0}{1} {2}", Environment.NewLine, udsCustomer.name, udsCustomer.surname);

                            dgv.Columns["rabat"].Visible = true;
                            dgv.Columns["rabat"].Width = 55;

                            if (dgv.Rows.Count > 0)
                            {
                                foreach (DataGridViewRow row in dgv.Rows)
                                {
                                    row.Cells["rabat"].Value = UDSAPI_DISCOUNT_BASE;
                                }

                                IzracunUkupno();
                            }
                        }
                        else
                        {
                            btnKoristiUds.Visible = true;
                            btnKoristiUds.Enabled = true;
                            btnDodajNaPartnera.Text = "UDS Game odustani";
                            btnKoristiUds.Text = string.Format("UDS Game bodovi za{0}{1} {2}", Environment.NewLine, udsCustomer.name, udsCustomer.surname);
                            //btnKoristiUds.Width = 196;
                        }
                    }
                    else
                    {
                        CODE = 0;
                        if (UDSAPI_APPLY_DISCOUNT)
                        {
                            //btnDodajNaPartnera.Width = 95;
                            btnDodajNaPartnera.Text = "UDS Game";
                        }
                        else
                        {
                            //btnKoristiUds.Width = 95;
                            btnKoristiUds.Text = "Koristi karticu";
                            btnKoristiUds.Enabled = false;
                            btnKoristiUds.Visible = false;
                        }
                    }
                }
            }
            else
            {

                dodaci.frmGrupirajPoKupcima g = new dodaci.frmGrupirajPoKupcima();
                g.Kasa = this;
                g.ShowDialog();
                PaintRows(dgv);
            }
        }

        private void btnPopust_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count != 1)
            {
                MessageBox.Show("Izaberite jednu stavku na koju želite dodati popust");
                return;
            };
            Kasa.frmApsolutniPopust aps = new Kasa.frmApsolutniPopust();
            decimal pop = aps.napraviPopust(Convert.ToDecimal(dgv["IZNOS", dgv.CurrentRow.Index].Value.ToString()));
            if (pop != 0)
            {
                if (classSQL.select("SELECT sifra FROM roba WHERE sifra = '!popustABS'", "provjera").Tables[0].Rows.Count == 0)
                    classSQL.insert("INSERT INTO roba (naziv,id_grupa,jm,vpc,mpc,id_zemlja_porijekla,id_zemlja_uvoza,id_partner,id_manufacturers,sifra,ean,porez,oduzmi,nc,porez_potrosnja,jamstvo,akcija) VALUES ('Popust','1','kom','0','0','60','60','1','1','!popustABS','0','0','NE','0','0','0','0');");
                dgv.Rows.Insert(dgv.CurrentRow.Index + 1, "!popustABS", "Popust na " + dgv["Naziv", dgv.CurrentRow.Index].Value.ToString(), "kn", "0", pop.ToString("#0.00"), "1", 0, pop.ToString("#0.00"), "NE", dgv["porez", dgv.CurrentRow.Index].Value.ToString(), ((double)pop / 1.25).ToString("#0.00"), "0", "0");
                this.IzracunUkupno();
            }
            txtUnos.Select();
        }

        private void btnPopustSve_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku.", "Greška");
                return;
            }
            if (isNumeric(txtUnos.Text, System.Globalization.NumberStyles.AllowDecimalPoint) == false) { MessageBox.Show("Greška kod upisa rabata.", "Greška"); return; }

            SetRabatSve(Convert.ToDouble(txtUnos.Text));
            txtUnos.Text = "";
            txtUnos.Select();
        }

        private void SetRabatSve(double rabat)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                double kolicina = Convert.ToDouble(dgv.Rows[i].Cells[5].Value.ToString());
                if (dgv.Rows[i].Cells["sifra"].FormattedValue.ToString() == "!popustABS" || (!Class.Postavke.kolicina_u_minus && kolicina <= 0)) continue;

                //double stari_popust = Convert.ToDouble(dgv.Rows[i].Cells["popust"].FormattedValue.ToString());
                //if (stari_popust == 0)
                //{
                //    dgv.Rows[i].Cells["popust"].Value = rabat.ToString();
                //    stari_popust = rabat;
                //}
                //else
                //{
                //    stari_popust = (1 - (1 - rabat / 100) * (1 - stari_popust / 100)) * 100;
                //    dgv.Rows[i].Cells["popust"].Value = stari_popust.ToString();
                //}

                dgv.Rows[i].Cells["popust"].Value = rabat.ToString();

                double cijena = Convert.ToDouble(dgv.Rows[i].Cells[4].Value.ToString());
                double ukupno;
                ukupno = cijena * kolicina;
                //ukupno *= 1.0 - (stari_popust / 100);
                ukupno = ukupno - (ukupno * rabat / 100);
                dgv.Rows[i].Cells[7].Value = Math.Round(ukupno, 2).ToString("#0.00");
            }
            IzracunUkupno();
        }

        private void spVaga_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                if (Korisno.vaga)
                {
                    if (spVaga.IsOpen)
                    {
                        try
                        {
                            spVagaValue = spVaga.ReadTo("=").Replace('.', ',');
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void timRefreshVaga_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Korisno.vaga)
                {
                    string spVagaValueTemp = spVagaValue.Trim().Replace(" ", "");
                    string minus = "";

                    if (spVagaValueTemp.Length != 0 && Regex.IsMatch(spVagaValueTemp, @"[+-]?\d+"))
                    {
                        if (spVagaValueTemp[spVagaValueTemp.Length - 1].ToString() == "-") { minus = "-"; }
                        string outputVagaValue = "";
                        for (int i = spVagaValueTemp.Length - 1; i >= 0; i--)
                        {
                            outputVagaValue += spVagaValueTemp[i];
                        }
                        vagaInterval += timRefreshVaga.Interval;
                        outputVagaValue = Convert.ToDecimal(outputVagaValue).ToString();
                        if (minus == "-" && !outputVagaValue.Contains("-")) outputVagaValue = minus + outputVagaValue;
                        if (oldVagaValue == outputVagaValue && (outputVagaValue != "0,000" && outputVagaValue != "0") && vagaInterval > 2000 && dgv.Rows.Count > 0)
                        {
                            btnKolicina_Click(sender, e);
                            SystemSounds.Beep.Play();
                            vagaInterval = 0;
                        }
                        else if (dgv.Rows.Count == 0)
                        {
                            vagaInterval = 0;
                        }
                        oldVagaValue = outputVagaValue;
                        lblVagaValue.Text = outputVagaValue;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Greška");
            }
        }

        private void btnKarticaKupca_Click(object sender, EventArgs e)
        {
            try
            {
                karticaKupcaS = Interaction.InputBox("Kartica kupca", "Kartica kupca", "");
                if (karticaKupcaS.Length > 0)
                {
                    karticaKupcaB = true;
                    karticaKupcaInfo(karticaKupcaB, karticaKupcaS);
                }
                else
                {
                    karticaKupcaS = null;
                    karticaKupcaB = false;
                    karticaKupcaInfo(karticaKupcaB, karticaKupcaS);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void karticaKupcaInfo(bool b = false, string s = null)
        {
            if (b)
            {
                string sql = @"select concat(x.naziv,' - ', coalesce(sum(y.ukupno::numeric), 0)) as naziv
                    from (
                    select case when vrsta_korisnika = 1 then ime_tvrtke else concat(ime, ' ', prezime) end as naziv, broj_kartice from partners where broj_kartice = '" + s + @"'
                    ) x
                    left join (select * from racuni where racuni.datum_racuna >=
                    (select coalesce(max(datum_resetiranja), '1990-01-03 00:00:00') as datum
                    from reset_kartica_kupca
                    where kartica_kupca = '" + s + @"')) y  on x.broj_kartice = y.kartica_kupca
                    group by x.naziv";

                DataSet dsKarticaKupca = classSQL.select(sql, "partners");
                if (dsKarticaKupca != null && dsKarticaKupca.Tables.Count > 0 && dsKarticaKupca.Tables[0] != null && dsKarticaKupca.Tables[0].Rows.Count > 0)
                {
                    lblKarticaKupca.Text = dsKarticaKupca.Tables[0].Rows[0]["naziv"].ToString();
                }
                else
                {
                    lblKarticaKupca.Text = "";
                    karticaKupcaS = null;
                    karticaKupcaB = false;
                }
            }
            else
            {
                karticaKupcaS = null;
                karticaKupcaB = false;
                lblKarticaKupca.Text = "";
            }
        }

        /// <summary>
        /// Checks if any article contains given code
        /// </summary>
        /// <param name="code"></param>
        private string CheckEan(string code)
        {
            string result = null;
            DataTable DTroba = Global.Database.GetRoba();
            if (DTroba?.Rows.Count > 0)
            {
                foreach(DataRow row in DTroba.Rows)
                {
                    string ean = row["ean"].ToString();
                    if(ean != "-1")
                    {
                        string[] array = ean.Split(';');
                        int index = Array.IndexOf(array, code);
                        if (index > -1)
                        {
                            result = row["sifra"].ToString();
                            break;
                        }
                    }
                }
            }
            return result;
        }
    }
}