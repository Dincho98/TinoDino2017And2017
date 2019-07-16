using Microsoft.Reporting.WinForms;
using PCPOS.Report;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using PCPOS.Eracun.Entities;
using System.Collections.Generic;
using System.IO;
using PCPOS.Entities;
using System.Net;

namespace PCPOS
{
    public partial class frmFaktura : Form
    {
        public bool kalendarIspunjenostiRefresh = false;

        public string broj_fakture_edit { get; set; }
        public string id_ducan { get; set; }
        public string id_kasa { get; set; }

        private int indexKolicina = 5;
        private decimal kolKolicina = 0;
        private int indexRowKolicina = 0;
        private bool bKol = true;
        private bool fromJSON = false;

        public int idSkladiste { get; set; }

        public frmFaktura()
        {
            InitializeComponent();
        }

        private static DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private static DataTable DToib = classSQL.select("SELECT oib from zaposlenici where id_zaposlenik='" +
            Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0];

        private int kreiranZapisnikZaFakturu = 0;
        private DataTable DTRoba = new DataTable();
        private DataSet DS_Skladiste = new DataSet();
        private DataSet DS_ZiroRacun = new DataSet();
        private DataSet DS_Zaposlenik = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DSIzjava = new DataSet();
        private DataSet DSnazivPlacanja = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataTable DSfakture = new DataTable();
        private DataTable DTpromocije1;
        private DataTable DSFS = new DataTable();
        private DataTable DTOtprema = new DataTable();
        public DataTable DTrezervacija { get; set; }
        private static DataTable DTpdv = new DataTable();
        private static DataRow RowPdv;

        private double u = 0;
        private bool edit = false;
        private double SveUkupno = 0;
        public frmMenu MainForm { get; set; }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_RESTORE = 0xF120;

            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_RESTORE)
            {
                this.Dock = DockStyle.None;
            }

            base.WndProc(ref m);
        }

        private bool ucitano = false;

        private void frmFaktura_Load(object sender, EventArgs e)
        {
            /****************************SINKRONIZACIJA SA WEB-OM*****************/
            bgSinkronizacija = new BackgroundWorker();
            bgSinkronizacija.DoWork += new DoWorkEventHandler(bgSinkronizacija_DoWork);
            bgSinkronizacija.WorkerSupportsCancellation = true;
            /****************************SINKRONIZACIJA SA WEB-OM*****************/
            prikaziAvioPodatke(false);

            btnStorno.Enabled = true;

            MyDataGrid.MainForm = this;

            SetirajNaplatniPoslovnicuDefault();

            DateTime nowDate = DateTime.Now;
            dtpOtpremniceOd.Value = new DateTime(nowDate.Year, nowDate.Month, 1, 0, 0, 0);
            dtpOtpremniceDo.Value = new DateTime(nowDate.Year, nowDate.Month, DateTime.DaysInMonth(nowDate.Year, nowDate.Month), 23, 59, 59);

            numeric();
            DTpromocije1 = classSQL.select("SELECT * FROM promocija1", "promocija1").Tables[0];
            fillComboBox();
            ttxBrojFakture.Text = brojFakture();
            EnableDisable(false);
            ControlDisableEnable(1, 0, 0, 1, 0);
            if (broj_fakture_edit != null) { fillFaktute(); }
            ucitano = true;

            if (DTrezervacija != null)
                LoadRezervacija();
        }

        /****************************SINKRONIZACIJA SA WEB-OM*****************/
        private BackgroundWorker bgSinkronizacija = null;
        private synWeb.synPokretac PokretacSinkronizacije = new synWeb.synPokretac();
        /****************************SINKRONIZACIJA SA WEB-OM*****************/

        private void GasenjeForme_FormClosing(object sender, FormClosingEventArgs e)
        {
            /****************************SINKRONIZACIJA SA WEB-OM*****************/
            bgSinkronizacija.RunWorkerAsync();
            /****************************SINKRONIZACIJA SA WEB-OM*****************/
        }

        private void bgSinkronizacija_DoWork(object sender, DoWorkEventArgs e)
        {
            PokretacSinkronizacije.PokreniSinkronizaciju(true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
        }

        private void SetirajNaplatniPoslovnicuDefault()
        {
            try
            {
                //id_kasa = DTpostavke.Rows[0]["naplatni_uredaj_faktura"].ToString();
                id_kasa = Class.Postavke.naplatni_uredaj_faktura.ToString();
            }
            catch
            {
                MessageBox.Show("Kasa nije odabrana. Provjerite postavke programa.", "Upozorenje!");
                id_kasa = "1";
            }
            try
            {
                //id_ducan = DTpostavke.Rows[0]["default_ducan"].ToString();
                id_ducan = Class.Postavke.id_default_ducan.ToString();
            }
            catch
            {
                MessageBox.Show("Dućan nije odabran. Provjerite postavke programa.", "Upozorenje!");
                id_ducan = "1";
            }
        }

        private class MyDataGrid : System.Windows.Forms.DataGridView
        {
            public static frmFaktura MainForm { get; set; }

            protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
            {
                if (keyData == Keys.Enter)
                {
                    MainForm.EnterDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Right)
                {
                    MainForm.RightDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Left)
                {
                    MainForm.LeftDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Up)
                {
                    MainForm.UpDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Down)
                {
                    MainForm.DownDGW(MainForm.dgw);
                    return true;
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void EnterDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            if (d.CurrentCell.ColumnIndex == 2)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 3)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 4)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                decimal tempKol = 0;
                string EditedFormattedValue = "0";
                if (dgw.Rows[d.CurrentRow.Index].Cells[indexKolicina].EditedFormattedValue == null)
                {
                    EditedFormattedValue = "0";
                }
                else
                {
                    EditedFormattedValue = dgw.Rows[d.CurrentRow.Index].Cells[indexKolicina].EditedFormattedValue.ToString();
                }
                if (decimal.TryParse(EditedFormattedValue, out tempKol))
                {
                    if (tempKol <= 0 && !Class.Postavke.kolicina_u_minus)
                    {
                        MessageBox.Show("Nije dozvoljeno upisivanje količine u minus.");
                        d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[(indexKolicina)];
                        d.BeginEdit(true);
                        return;
                    }
                }

                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[6];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == (6))
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == (7))
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[8];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 8)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[9];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 9)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[10];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 10)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[11];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 11)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[12];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 12)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[13];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 13)
            {
                int curent = d.CurrentRow.Index;
                txtSifra_robe.Text = "";
                txtSifra_robe.Focus();
            }
        }

        private void LeftDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            if (d.CurrentCell.ColumnIndex == 3)
            {
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 7)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 8)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 13)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[8];
                d.BeginEdit(true);
            }
        }

        private void RightDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            if (d.CurrentCell.ColumnIndex == 3)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 7)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[8];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 8)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[13];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 13)
            {
                int curent = d.CurrentRow.Index;
                txtSifra_robe.Text = "";
                txtSifra_robe.Focus();
            }
        }

        private void UpDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;

            int curent = d.CurrentRow.Index;
            if (d.CurrentCell.ColumnIndex == 3)
            {
            }
            else if (curent == 0)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index - 1].Cells[3];
                d.BeginEdit(true);
            }
        }

        private void DownDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            int curent = d.CurrentRow.Index;
            if (d.CurrentCell.ColumnIndex == 3)
            {
                SendKeys.Send("{F4}");
            }
            else if (curent == d.RowCount - 1)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index + 1].Cells[3];
                d.BeginEdit(true);
            }
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

            if (delAll == 0)
            {
                btnDeleteAllFaktura.Enabled = false;
            }
            else if (delAll == 1)
            {
                btnDeleteAllFaktura.Enabled = true;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void fillComboBox()
        {
            nmGodinaFakture.Value = Convert.ToInt16(DateTime.Now.Year.ToString());

            //fill ziroracun
            DS_ZiroRacun = classSQL.select("SELECT * FROM ziro_racun", "ziro_racun");
            cbZiroRacun.DataSource = DS_ZiroRacun.Tables[0];
            cbZiroRacun.DisplayMember = "ziroracun";
            cbZiroRacun.ValueMember = "id_ziroracun";
            //cbZiroRacun.SelectedValue = "1";

            //fill otprema
            DTOtprema = classSQL.select("SELECT * FROM otprema", "otprema").Tables[0];
            cbOtprema.DataSource = DTOtprema;
            cbOtprema.DisplayMember = "naziv";
            cbOtprema.ValueMember = "id_otprema";

            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbKomercijalist.DataSource = DS_Zaposlenik.Tables[0];
            cbKomercijalist.DisplayMember = "IME";
            cbKomercijalist.ValueMember = "id_zaposlenik";
            cbKomercijalist.SelectedValue = Properties.Settings.Default.id_zaposlenik;

            //fill izjava
            DSIzjava = classSQL.select("SELECT * FROM izjava ORDER BY id_izjava", "izjava");
            cbIzjava.DataSource = DSIzjava.Tables[0];
            cbIzjava.DisplayMember = "izjava";
            cbIzjava.ValueMember = "id_izjava";

            //fill vrsta dokumenta
            DSvd = classSQL.select("SELECT * FROM fakture_vd WHERE grupa='fak' ORDER BY id_vd", "fakture_vd");
            cbVD.DataSource = DSvd.Tables[0];
            cbVD.DisplayMember = "vd";
            cbVD.ValueMember = "id_vd";

            //fill nacin_placanja
            DSnazivPlacanja = classSQL.select("SELECT * FROM nacin_placanja", "nacin_placanja");
            cbNacinPlacanja.DataSource = DSnazivPlacanja.Tables[0];
            cbNacinPlacanja.DisplayMember = "naziv_placanja";
            cbNacinPlacanja.ValueMember = "id_placanje";
            cbNacinPlacanja.SelectedValue = 3;
            txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();

            //DS Valuta
            DSValuta = classSQL.select("SELECT * FROM valute", "valute");
            cbValuta.DataSource = DSValuta.Tables[0];
            cbValuta.DisplayMember = "ime_valute";
            cbValuta.ValueMember = "id_valuta";
            cbValuta.SelectedValue = 5;
            //txtTecaj.DataBindings.Add("Text", DSValuta.Tables[0], "tecaj");
            txtTecaj.Text = "1";

            DataTable DTSK = new DataTable("skladiste");
            DTSK.Columns.Add("id_skladiste", typeof(string));
            DTSK.Columns.Add("skladiste", typeof(string));

            DS_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste");
            //DTSK.Rows.Add(0,"");
            for (int i = 0; i < DS_Skladiste.Tables[0].Rows.Count; i++)
            {
                DTSK.Rows.Add(DS_Skladiste.Tables[0].Rows[i]["id_skladiste"], DS_Skladiste.Tables[0].Rows[i]["skladiste"]);
            }

            skladiste.DataSource = DTSK;
            skladiste.DataPropertyName = "skladiste";
            skladiste.DisplayMember = "skladiste";
            skladiste.HeaderText = "Skladište";
            skladiste.Name = "skladiste";
            skladiste.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            skladiste.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            skladiste.ValueMember = "id_skladiste";

            //fill tko je prijavljen
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();

            DS_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste");

            //if (cbValuta.SelectedValue.ToString() == "5")
            //{
            //    chbObracunPoreza.Visible = false;
            //}
            //else
            //{
            //    chbObracunPoreza.Visible = true;
            //}
        }

        private void numeric()
        {
            nmGodinaFakture.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nmGodinaFakture.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nmGodinaFakture.Value = Convert.ToInt16(DateTime.Now.Year.ToString());

            nuGodinaPredujma.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nuGodinaPredujma.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nuGodinaPredujma.Value = Convert.ToInt16(DateTime.Now.Year.ToString());

            nuGodinaPonude.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nuGodinaPonude.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nuGodinaPonude.Value = Convert.ToInt16(DateTime.Now.Year.ToString());

            nuGodinaServisa.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nuGodinaServisa.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nuGodinaServisa.Value = Convert.ToInt16(DateTime.Now.Year.ToString());
        }

        private void SetCijenaSkladiste()
        {
            if (dgw.CurrentRow.Cells["skladiste"].Value != null)
            {
                DataSet dsRobaProdaja = new DataSet();
                dsRobaProdaja = classSQL.select("SELECT * FROM roba_prodaja WHERE id_skladiste='" + dgw.CurrentRow.Cells["skladiste"].Value + "' AND sifra='" + dgw.CurrentRow.Cells["sifra"].FormattedValue.ToString() + "'", "roba_prodaja");
                if (dsRobaProdaja.Tables[0].Rows.Count > 0)
                {
                    decimal _NBC = Util.Korisno.VratiNabavnuCijenu(dgw.CurrentRow.Cells["sifra"].FormattedValue.ToString(), dgw.CurrentRow.Cells["skladiste"].Value.ToString());

                    if (Convert.ToDouble(dsRobaProdaja.Tables[0].Rows[0]["kolicina"].ToString()) > 0)
                    {
                        if (chbObracunPoreza.Checked)
                        {
                            dgw.CurrentRow.Cells["porez"].Value = dsRobaProdaja.Tables[0].Rows[0]["porez"].ToString();
                        }
                        else
                        {
                            dgw.CurrentRow.Cells["porez"].Value = "0";
                        }

                        dgw.CurrentRow.Cells["vpc"].Value = string.Format("{0:0.000}", dsRobaProdaja.Tables[0].Rows[0]["vpc"]);
                        dgw.CurrentRow.Cells["ppmv"].Value = dsRobaProdaja.Tables[0].Rows[0]["ppmv"];
                        dgw.CurrentRow.Cells["nc"].Value = _NBC.ToString("#0.000");
                        lblNaDan.ForeColor = Color.Lime;
                        lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima " + dsRobaProdaja.Tables[0].Rows[0]["kolicina"].ToString() + " " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
                    }
                    else
                    {
                        if (chbObracunPoreza.Checked)
                        {
                            dgw.CurrentRow.Cells["porez"].Value = dsRobaProdaja.Tables[0].Rows[0]["porez"].ToString();
                        }
                        else
                        {
                            dgw.CurrentRow.Cells["porez"].Value = "0";
                        }
                        dgw.CurrentRow.Cells["vpc"].Value = string.Format("{0:0.000}", dsRobaProdaja.Tables[0].Rows[0]["vpc"]);
                        dgw.CurrentRow.Cells["nc"].Value = _NBC.ToString("#0.000");
                        lblNaDan.ForeColor = Color.Red;
                        lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima " + dsRobaProdaja.Tables[0].Rows[0]["kolicina"].ToString() + " " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
                        MessageBox.Show("Na odabranom skladištu nemate unešeni artikl.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    lblNaDan.ForeColor = Color.Red;
                    lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima 0,00 " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
                }
            }
        }

        private string ReturnSifra(string sifra)
        {
            if (sifra.Length > 3)
            {
                if (DTpromocije1.Rows[0]["aktivnost"].ToString() == "DA" && sifra.Substring(0, 3) == "000")
                {
                    return "00000";
                }
            }
            return sifra;
        }

        private void txtSifraArtikla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtSifra_robe.Text == "")
                {
                    frmRobaTrazi roba = new frmRobaTrazi();
                    if (idSkladiste != 0)
                    {
                        roba.idSkladiste = idSkladiste;
                    }
                    roba.ShowDialog();

                    if (Properties.Settings.Default.id_roba != "")
                    {
                        txtSifra_robe.Text = Properties.Settings.Default.id_roba;
                        txtSifra_robe.Select();
                    }
                    else
                    {
                        return;
                    }
                }

                string sifra = CheckEan(txtSifra_robe.Text) ?? txtSifra_robe.Text;

                if (txtSifra_robe.Text.Length > 2)
                {
                    if (DTpromocije1.Rows[0]["aktivnost"].ToString() == "DA" && txtSifra_robe.Text.Substring(0, 3) == "000")
                    {
                        double uk;
                        double popust;
                        DataTable DTrp = classSQL.select("SELECT * FROM racun_popust_kod_sljedece_kupnje WHERE broj_racuna='" + txtSifra_robe.Text.Substring(3, txtSifra_robe.Text.Length - 3) + "' AND dokumenat='FA'", "racun_popust_kod_sljedece_kupnje").Tables[0];

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

                        DateTime dateFromPopust = Convert.ToDateTime(DTrp.Rows[0]["datum"].ToString()).AddDays(Convert.ToDouble(DTpromocije1.Rows[0]["traje_do"].ToString()));

                        if (dateFromPopust < DateTime.Now)
                        {
                            MessageBox.Show("Ovom popustu je istekao datum.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        uk = Convert.ToDouble(DTrp.Rows[0]["ukupno"].ToString());
                        popust = Convert.ToDouble(DTrp.Rows[0]["popust"].ToString());
                        uk = uk * 3 / 100;

                        if ((Convert.ToDouble(SveUkupno) - uk) < 0)
                        {
                            MessageBox.Show("Popust je veći od ukupnog računa.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        dgw.Rows.Add(
                                    dgw.RowCount - 1,
                                    txtSifra_robe.Text,
                                    "Popust sa prethodnog računa",
                                    "1",
                                    "kn",
                                    1,
                                    //DTpostavke.Rows[0]["pdv"].ToString(),
                                    Class.Postavke.pdv.ToString(),
                                    Math.Round(uk * -1, 2).ToString("#0.00"),
                                    "0",
                                    "0",
                                    Math.Round(uk * -1 / (1 + Convert.ToDouble(Class.Postavke.pdv) / 100), 3).ToString("#0.000"),
                                    Math.Round(uk * -1 / (1 + Convert.ToDouble(Class.Postavke.pdv) / 100), 3).ToString("#0.000"),
                                    Math.Round(uk * -1, 2).ToString("#0.00"),
                                    Math.Round(uk * -1 / (1 + Convert.ToDouble(Class.Postavke.pdv) / 100), 3).ToString("#0.000"),
                                    Math.Round(uk * -1, 2).ToString("#0.00"),
                                    "",
                                    "",
                                    "",
                                    true
                                );

                        int br = dgw.Rows.Count - 1;
                        dgw.ClearSelection();
                        izracun(true);
                        dgw.ClearSelection();
                        txtSifra_robe.Text = "";
                        txtSifra_robe.Select();
                        return;
                    }
                }

                //for (int y = 0; y < dgw.Rows.Count; y++)
                //{
                //    if (txtSifra_robe.Text == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString().Trim())
                //    {
                //        MessageBox.Show("Artikl ili usluga već postoje!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        return;
                //    }
                //}

                string sql = $"SELECT * FROM roba WHERE sifra='{txtSifra_robe.Text}' OR sifra='{sifra}'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();
                    ttxBrojFakture.ReadOnly = true;
                    nmGodinaFakture.ReadOnly = true;
                    cbValuta.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                foreach (DataRow row in DTroba.Rows)
                {
                    string ean = row["ean"].ToString();
                    if (ean != "-1")
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

        public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle,
                System.Globalization.CultureInfo.CurrentCulture, out result);
        }

        private void izracun(bool izracunajPovrNaknadIspocetka)
        {
            if (dgw.RowCount > 0)
            {
                double tecaj = 1;

                if (cbStavkeValuta.Checked)
                    double.TryParse(txtTecaj.Text, out tecaj);

                int rowBR = dgw.CurrentRow.Index;
                double dec_parse;
                if (!double.TryParse(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString(), out dec_parse))
                {
                    dgw.Rows[rowBR].Cells["kolicina"].Value = "1";
                    MessageBox.Show("Greška kod upisa količine.", "Greška");
                    return;
                }

                if (!double.TryParse(dgw.Rows[rowBR].Cells["vpc"].FormattedValue.ToString(), out dec_parse))
                {
                    dgw.Rows[rowBR].Cells["vpc"].Value = "1";
                    MessageBox.Show("Greška kod upisa vpc.", "Greška");
                    return;
                }

                if (!double.TryParse(dgw.Rows[rowBR].Cells["rabat"].FormattedValue.ToString(), out dec_parse))
                {
                    dgw.Rows[rowBR].Cells["rabat"].Value = "0";
                    MessageBox.Show("Greška kod upisa rabata.", "Greška");
                    return;
                }

                if (!double.TryParse(dgw.Rows[rowBR].Cells["rabat_iznos"].FormattedValue.ToString(), out dec_parse))
                {
                    dgw.Rows[rowBR].Cells["rabat_iznos"].Value = "0,00";
                    MessageBox.Show("Greška kod rabata.", "Greška");
                    return;
                }

                double kol = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString()), 3);
                double vpc = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["vpc"].FormattedValue.ToString()), 3);
                double nbc = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["nc"].FormattedValue.ToString()), 4);

                if (!Class.Dokumenti.isKasica && ((Class.PodaciTvrtka.oibTvrtke == Class.Postavke.OIB_PC1 && Util.Korisno.GodinaKojaSeKoristiUbazi >= Class.Postavke.GODINA_POCETKA_FIFO_PC1) || Util.Korisno.GodinaKojaSeKoristiUbazi >= Class.Postavke.GODINA_POCETKA_FIFO))
                {
                    decimal _nbc = Class.FIFO.getNbc(Util.Korisno.GodinaKojaSeKoristiUbazi, dtpDatum.Value, Convert.ToInt32(dgw.CurrentRow.Cells["skladiste"].Value.ToString()), kol, dgw.Rows[rowBR].Cells["sifra"].Value.ToString(), nbc);
                    dgw.Rows[rowBR].Cells["nc"].Value = _nbc.ToString();
                }

                if (Class.Postavke.proizvodnjaFakturaNbc)
                {
                    vpc = nbc;
                    dgw.Rows[rowBR].Cells["vpc"].Value = Math.Round(vpc, 3).ToString("#0.000");
                }
                decimal ppmv = getPPMV(dgw.Rows[rowBR].Cells["sifra"].Value.ToString(), dgw.Rows[rowBR].Cells["skladiste"].Value.ToString());
                double porez = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["porez"].FormattedValue.ToString()), 2);
                double rbt = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["rabat"].FormattedValue.ToString()), 4);
                //string velep = classSQL.select_settings("Select veleprodaja From postavke", "veleprodaja").Tables[0].Rows[0][0].ToString();
                double porez_ukupno = vpc * porez / 100;
                double mpc = porez_ukupno + vpc + (double)ppmv;
                double rabat = 0;

                if (Class.Postavke.veleprodaja)
                {
                    rabat = vpc * rbt / 100;
                }
                else
                {
                    rabat = mpc * rbt / 100;
                }

                double mpc_sa_kolicinom = mpc * kol;

                if (Class.Postavke.veleprodaja)
                {
                    dgw.Rows[rowBR].Cells["mpc"].Value = Math.Round(mpc, 2).ToString("#0.00");
                    dgw.Rows[rowBR].Cells["rabat_iznos"].Value = Math.Round(rabat * kol, 2).ToString("#0.00");
                    dgw.Rows[rowBR].Cells["iznos_bez_pdva"].Value = Math.Round((((vpc - rabat) * kol)), 3).ToString("#0.00");
                    dgw.Rows[rowBR].Cells["iznos_ukupno"].Value = Math.Round((((vpc - rabat) * (1 + porez / 100)) * kol), 2).ToString("#0.00");
                    dgw.Rows[rowBR].Cells["cijena_bez_pdva"].Value = Math.Round((vpc - rabat), 3).ToString("#0.000");
                    dgw.Rows[rowBR].Cells["kolicina"].Value = kol.ToString("#0.000");
                    dgw.Rows[rowBR].Cells["rabat"].Value = rbt.ToString("#0.0000");
                    dgw.Rows[rowBR].Cells["porez"].Value = porez.ToString("#0.00");
                }
                else
                {
                    dgw.Rows[rowBR].Cells["mpc"].Value = Math.Round(mpc, 2).ToString("#0.00");
                    dgw.Rows[rowBR].Cells["rabat_iznos"].Value = Math.Round(rabat * kol, 2).ToString("#0.00");
                    dgw.Rows[rowBR].Cells["iznos_bez_pdva"].Value = Math.Round((((mpc - rabat - (double)ppmv) * kol) / (1 + porez / 100)), 3).ToString("#0.00");
                    dgw.Rows[rowBR].Cells["iznos_ukupno"].Value = Math.Round((mpc - rabat) * kol, 2).ToString("#0.00");
                    dgw.Rows[rowBR].Cells["cijena_bez_pdva"].Value = Math.Round(((mpc - rabat - (double)ppmv) / (1 + porez / 100)), 3).ToString("#0.000");
                    dgw.Rows[rowBR].Cells["kolicina"].Value = kol.ToString("#0.000");
                    dgw.Rows[rowBR].Cells["rabat"].Value = rbt.ToString("#0.0000");
                    dgw.Rows[rowBR].Cells["porez"].Value = porez.ToString("#0.00");
                }

                SrediPovratnuNaknaduUkupno(rowBR, izracunajPovrNaknadIspocetka);
            }
        }

        private decimal getPPMV(string sifra, object skladiste)
        {
            decimal ppmv = 0;
            if (Class.Postavke.prodaja_automobila)
            {
                string sql = string.Format(@"select ppmv from roba_prodaja where sifra = '{0}' and id_skladiste = {1};", sifra, skladiste);
                DataSet dsPPMV = classSQL.select(sql, "roba_prodaja");
                if (dsPPMV != null && dsPPMV.Tables.Count > 0 && dsPPMV.Tables[0].Rows.Count > 0)
                    decimal.TryParse(dsPPMV.Tables[0].Rows[0]["ppmv"].ToString(), out ppmv);
            }
            return ppmv;
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = ((ComboBox)sender).SelectedIndex;
            //MessageBox.Show("Selected Index = " + selectedIndex);
        }

        private DataSet DSpartner;

        private void btnPartner_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DSpartner = new DataSet();
                DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (DSpartner.Tables[0].Rows.Count > 0)
                {
                    PartnerPostaviPopust();
                    txtSifraOdrediste.Text = DSpartner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartnerNaziv.Text = DSpartner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    txtSifraFakturirati.Text = DSpartner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartnerNaziv1.Text = DSpartner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    cbVD.Select();
                    getOdgodaDana(DSpartner.Tables[0].Rows[0]);

                    int idSkl = 0;
                    int.TryParse(DSpartner.Tables[0].Rows[0]["default_skladiste"].ToString(), out idSkl);
                    idSkladiste = idSkl;
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }

                getPoslovnicaPartner();
            }
        }

        private void getPoslovnicaPartner()
        {
            string sql = "SELECT 0 AS id_partner_poslovnica, 'Adresa slanja' AS naziv";

            if (txtSifraOdrediste.Text.Trim().Length > 0)
            {
                sql = string.Format(@"SELECT 0 AS id_partner_poslovnica, 'Adresa slanja' AS naziv
union
select id_partner_poslovnica, naziv
from partner_poslovnice
where id_partner = {0}", txtSifraOdrediste.Text.Trim());
            }
            DataSet dsPoslovnicaPartner = classSQL.select(sql, "poslovnica_partner");
            cmbPoslovnicaPartner.DataSource = dsPoslovnicaPartner.Tables[0];
            cmbPoslovnicaPartner.DisplayMember = "naziv";
            cmbPoslovnicaPartner.ValueMember = "id_partner_poslovnica";
        }

        /// <summary>
        /// FUNKCIJA POSTAVLJA POPUST NA SVE STAVKE
        /// </summary>

        private int dodjeli_popust = 0;

        private void PartnerPostaviPopust(string partner = null)
        {
            try
            {
                decimal rabat = 0;

                if (partner != null && DSpartner == null)
                {
                    DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner ='" + partner + "'", "partners");
                }

                if (DSpartner != null)
                {
                    decimal.TryParse(DSpartner.Tables[0].Rows[0]["popust"].ToString(), out rabat);

                    if (dodjeli_popust == -1 || rabat == 0)
                    {
                        return;
                    }
                    else if (dodjeli_popust != -1 && dodjeli_popust != 1)
                    {
                        if ((partner != null ? true : MessageBox.Show("Ovaj partner ima popust od " + rabat.ToString() + "%\r\nDali ste sigurni da želite dodijeliti navedeni popust.", "Popust na račun", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                            dodjeli_popust = 1;
                        else
                            dodjeli_popust = -1;
                    }

                    foreach (DataGridViewRow r in dgw.Rows)
                    {
                        r.Cells["rabat"].Value = Math.Round(rabat, 5).ToString("#0.00000");

                        izracun(true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnPartner1_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DSpartner = new DataSet();
                DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (DSpartner.Tables[0].Rows.Count > 0)
                {
                    txtSifraFakturirati.Text = DSpartner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartnerNaziv1.Text = DSpartner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    cbVD.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
                getPoslovnicaPartner();
            }
        }

        #region ON_KEY_DOWN_REGION

        private void txtSifraOdrediste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtSifraOdrediste.Text == "")
                {
                    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                    partnerTrazi.ShowDialog();
                    if (Properties.Settings.Default.id_partner != "")
                    {
                        DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                        if (DSpartner.Tables[0].Rows.Count > 0)
                        {
                            PartnerPostaviPopust();
                            txtSifraOdrediste.Text = DSpartner.Tables[0].Rows[0]["id_partner"].ToString();
                            txtPartnerNaziv.Text = DSpartner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                            txtSifraFakturirati.Text = DSpartner.Tables[0].Rows[0]["id_partner"].ToString();
                            txtPartnerNaziv1.Text = DSpartner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                            cbVD.Select();
                            getOdgodaDana(DSpartner.Tables[0].Rows[0]);
                            txtSifraFakturirati.Select();
                            int idSkl = 0;
                            int.TryParse(DSpartner.Tables[0].Rows[0]["default_skladiste"].ToString(), out idSkl);
                            idSkladiste = idSkl;
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtSifraOdrediste.Select();
                        }
                    }
                    else
                    {
                        txtSifraOdrediste.Select();
                        return;
                    }
                }

                string Str = txtSifraOdrediste.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtSifraOdrediste.Text = "0";
                }

                DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner='" + txtSifraOdrediste.Text + "'", "partners");
                if (DSpartner.Tables[0].Rows.Count > 0)
                {
                    PartnerPostaviPopust();
                    txtPartnerNaziv.Text = DSpartner.Tables[0].Rows[0][0].ToString();
                    txtSifraFakturirati.Text = txtSifraOdrediste.Text;
                    txtPartnerNaziv1.Text = DSpartner.Tables[0].Rows[0][0].ToString();
                    getOdgodaDana(DSpartner.Tables[0].Rows[0]);
                    txtSifraFakturirati.Select();
                    //txtSifraFakturirati_KeyDown(txtSifraOdrediste, e);

                    int idSkl = 0;
                    int.TryParse(DSpartner.Tables[0].Rows[0]["default_skladiste"].ToString(), out idSkl);
                    idSkladiste = idSkl;
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
                getPoslovnicaPartner();
            }
        }

        private void getOdgodaDana(DataRow dRow)
        {
            int dana = 0;
            if (Int32.TryParse(dRow["odgoda_placanja_u_danima"].ToString(), out dana) && dana > 0)
            {
                txtDana.Text = dana.ToString();
                dtpDanaValuta.Value = dtpDanaValuta.Value.AddDays(dana);
            }
            else
            {
                txtDana.Text = dana.ToString();
            }
        }

        private void txtSifraFakturirati_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                string Str = txtSifraFakturirati.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtSifraFakturirati.Text = "0";
                }

                DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner='" + txtSifraFakturirati.Text + "'", "partners");
                if (DSpartner.Tables[0].Rows.Count > 0)
                {
                    PartnerPostaviPopust();
                    txtPartnerNaziv1.Text = DSpartner.Tables[0].Rows[0][0].ToString();
                    cbVD.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }

                getPoslovnicaPartner();
            }
        }

        private void cbVD_KeyDown(object sender, KeyEventArgs e)
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

                dtpDatumDVO.Value = dtpDatum.Value;
                dtpDanaValuta.Value = dtpDatum.Value;
                dtpDatumDVO.Select();
            }
        }

        private void dtpDatumDVO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtDana.Select();
            }
        }

        private void txtDana_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
                {
                    (sender as TextBox).Text = "0";
                    dtpDanaValuta.Select();
                }

                try
                {
                    DateTime dvo = dtpDatumDVO.Value;
                    dtpDanaValuta.Value = dvo.AddDays(Convert.ToInt16(txtDana.Text));
                    dtpDanaValuta.Select();
                }
                catch (Exception)
                {
                }
            }
        }

        private void dtpDanaValuta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                try
                {
                    DateTime dt1 = dtpDanaValuta.Value;
                    DateTime dt2 = dtpDatumDVO.Value;
                    TimeSpan ts = dt1 - dt2;
                    txtDana.Text = (Convert.ToInt16(ts.Days.ToString()) + 1).ToString();
                    cbIzjava.Select();
                }
                catch (Exception)
                {
                }
            }
        }

        private void cbIzjava_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                rtbNapomena.Text += cbIzjava.Text;
                cbKomercijalist.Select();
            }
        }

        private void cbKomercijalist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtModel.Select();
            }
        }

        private void txtIzradio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtModel.Select();
            }
        }

        private void txtModel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifraNacinPlacanja.Select();
            }
        }

        private void txtSifraNacinPlacanja_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                try
                {
                    DataRow[] dataROW = DSnazivPlacanja.Tables[0].Select("id_placanje = " + txtSifraNacinPlacanja.Text);
                    cbNacinPlacanja.SelectedValue = dataROW[0]["id_placanje"].ToString();
                    cbNacinPlacanja.Select();
                }
                catch (Exception)
                {
                    MessageBox.Show("Krivi unos.", "Greška");
                }
            }
        }

        private void cbNacinPlacanja_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void cbZiroRacun_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbValuta.Select();
            }
        }

        private void cbValuta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifraNacinPlacanja.Select();
            }
        }

        private void txtTecaj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifraNacinPlacanja.Select();
            }
        }

        private void txtSifraNacinPlacanja_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbNacinPlacanja.Select();
                cbNacinPlacanja.SelectedValue = txtSifraNacinPlacanja.Text;
            }
        }

        private void cbNacinPlacanja_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSifraRadniNalog.Select();
                txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();
            }
        }

        private void txtSifraRadniNalog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbRadniBalog.Select();
            }
        }

        private void cbRadniBalog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifraNarKupca.Select();
            }
        }

        private void txtSifraNarKupca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbNarKupca.Select();
            }
        }

        private void cbNarKupca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtNarKupca1.Select();
            }
        }

        private void txtNarKupca1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbOtprema.Select();
            }
        }

        private void txtOtprema_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                nuGodinaPredujma.Select();
            }
        }

        private void nuGodinaPredujma_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbPredujam.Select();
            }
        }

        private void cbPredujam_KeyDown(object sender, KeyEventArgs e)
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
                if (rtbNapomena.Text == "")
                {
                    e.SuppressKeyPress = true;
                    txtSifra_robe.Select();
                }
            }
        }

        #endregion ON_KEY_DOWN_REGION

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dgw.RowCount == 0)
            {
                return;
            }
            if (dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].Value != null)
            {
                DataRow[] dataROW = DSFS.Select("id_stavka = " + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].Value.ToString());

                if (MessageBox.Show("Brisanjem ove stavke brišete i količinu robe sa skladišta. Da li ste sigurni da želite obrisati ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (chbOduzmiIzSkladista.Checked)
                    {
                        if (dgw.Rows[dgw.CurrentRow.Index].Cells["oduzmi"].FormattedValue.ToString() == "DA")
                        {
                            string kol = classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dgw.Rows[dgw.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString();
                            kol = (Convert.ToDouble(kol) + Convert.ToDouble(dataROW[0]["kolicina"].ToString())).ToString();
                            classSQL.update("UPDATE roba_prodaja SET kolicina='" + kol + "' WHERE sifra='" + dgw.Rows[dgw.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'");
                        }
                    }

                    classSQL.delete("DELETE FROM faktura_stavke WHERE id_stavka='" + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() + "' AND id_ducan='" + id_ducan + "' AND id_kasa='" + id_kasa + "'");
                    PregledKolicine();
                    dgw.Rows.RemoveAt(dgw.CurrentRow.Index);

                    classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje stavke sa fakture br." + ttxBrojFakture.Text + "')");
                    if (chbOduzmiIzSkladista.Checked)
                        Util.AktivnostZaposlenika.SpremiAktivnost(dgw, null, "Faktura", ttxBrojFakture.Text, true);
                    MessageBox.Show("Obrisano.");
                }
            }
            else
            {
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                MessageBox.Show("Obrisano.");
            }

            izracun(false);
        }

        private void SrediPovratnuNaknaduUkupno(int rowBR, bool izracunajPovrNaknadIspocetka)
        {
            double kol = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString()), 3);

            if (izracunajPovrNaknadIspocetka)
            {
                DataTable DTtemp = classSQL.select("SELECT iznos FROM povratna_naknada WHERE sifra='" + dgw.Rows[rowBR].Cells["sifra"].Value + "'", "povratna_naknada")
                        .Tables[0];
                double povratnaNaknada = DTtemp.Rows.Count > 0 ? Convert.ToDouble(DTtemp.Rows[0]["iznos"].ToString()) * kol : 0;
                dgw.Rows[rowBR].Cells["povratna_naknada"].Value = Math.Round(povratnaNaknada, 2).ToString("#0.00");
            }
            else
            {
                dgw.Rows[rowBR].Cells["povratna_naknada"].Value = dgw.Rows[rowBR].Cells["povratna_naknada"].Value;
            }

            double povr_n_uk = 0;
            double B_pdv = 0;
            u = 0;
            double iznos;
            double povratnaNaknadaZaUk;

            for (int i = 0; i < dgw.RowCount; i++)
            {
                double.TryParse(dgw.Rows[i].Cells["povratna_naknada"].FormattedValue.ToString(), out povratnaNaknadaZaUk);

                povr_n_uk += povratnaNaknadaZaUk;
                iznos = Convert.ToDouble(dgw.Rows[i].Cells["iznos_ukupno"].FormattedValue.ToString());
                u += Math.Round(iznos, 2);
                iznos = Convert.ToDouble(dgw.Rows[i].Cells["iznos_bez_pdva"].FormattedValue.ToString());
                B_pdv += Math.Round(iznos, 2);
            }

            textBox1.Text = "Ukupno sa PDV-om: " + Math.Round(u + povr_n_uk, 2).ToString("#0.00");
            textBox2.Text = "Bez PDV-a: " + Math.Round(B_pdv, 3).ToString("#0.000");
            textBox3.Text = "PDV: " + Math.Round(Math.Round(u, 2) - Math.Round(B_pdv, 3), 2).ToString("#0.00");
            SveUkupno = u;
            txtPovrNaknada.Text = "P.N.:" + Math.Round(povr_n_uk, 2).ToString("#0.00");
        }

        private void btnDeleteAllFaktura_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Brisanjem ove fakture brišete i količinu robe sa skladišta. Da li ste sigurni da želite obrisati ovu fakturu?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                dodjeli_popust = 0;
                DSpartner = null;

                double skl = 0;
                double fa_kolicina = 0;
                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    if (dgw.Rows[i].Cells["id_stavka"].Value != null)
                    {
                        if (chbOduzmiIzSkladista.Checked)
                        {
                            if (dgw.Rows[i].Cells["oduzmi"].FormattedValue.ToString() == "DA")
                            {
                                DataRow[] dataROW = DSFS.Select("id_stavka = " + dgw.Rows[i].Cells["id_stavka"].Value.ToString());
                                skl = Convert.ToDouble(classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString());
                                fa_kolicina = Convert.ToDouble(dataROW[0]["kolicina"].ToString());

                                skl = skl + fa_kolicina;
                                classSQL.update("UPDATE roba_prodaja SET kolicina='" + skl + "' WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'");
                            }
                        }
                    }
                }

                classSQL.delete("DELETE FROM faktura_stavke WHERE broj_fakture='" + ttxBrojFakture.Text + "' AND id_ducan='" + id_ducan + "' AND id_kasa='" + id_kasa + "'");

                classSQL.update("UPDATE fakture SET ukupno = " +
                                    "COALESCE((select SUM(((vpc * (1+REPLACE(porez, ',','.')::numeric/100)) - ((vpc * (1+REPLACE(porez, ',','.')::numeric/100)) * (REPLACE(rabat, ',', '.')::numeric/100))) * REPLACE(kolicina, ',', '.')::numeric) AS ukupno " +
                                    "FROM faktura_stavke " +
                                    "WHERE broj_fakture = '" + ttxBrojFakture.Text + "' " +
                                    "AND id_ducan='" + id_ducan + "' AND id_kasa='" + id_kasa + "'),0), editirano = '1' " +
                                "WHERE broj_fakture = '" + ttxBrojFakture.Text + "' " +
                                "AND id_ducan = '" + id_ducan + "' AND id_kasa = '" + id_kasa + "';");

                //classSQL.delete("DELETE FROM fakture WHERE broj_fakture='" + ttxBrojFakture.Text + "' AND id_ducan='" + id_ducan + "' AND id_kasa='" + id_kasa + "'");
                classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje cijele fakture br." + ttxBrojFakture.Text + "')");

                if (chbOduzmiIzSkladista.Checked)
                    Util.AktivnostZaposlenika.SpremiAktivnost(new DataGridView(), null, "Faktura", ttxBrojFakture.Text, true);
                MessageBox.Show("Obrisano.");
                PregledKolicine();
                ttxBrojFakture.ReadOnly = false;
                nmGodinaFakture.ReadOnly = false;
                edit = false;
                EnableDisable(false);
                deleteFields();
                ControlDisableEnable(1, 0, 0, 1, 0);
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            dodjeli_popust = 0;
            DSpartner = null;
            fromJSON = false;
            SetirajNaplatniPoslovnicuDefault();
            btnSveFakture.Enabled = true;
            EnableDisable(false);
            deleteFields();
            ttxBrojFakture.Text = brojFakture();
            edit = false;
            ttxBrojFakture.ReadOnly = false;
            nmGodinaFakture.ReadOnly = false;
            btnObrisi.Enabled = true;
            btnOtpr.Enabled = true;
            chbOduzmiIzSkladista.Enabled = true;

            kreiranZapisnikZaFakturu = 0;

            ControlDisableEnable(1, 0, 0, 1, 0);

            idSkladiste = 0;
            DTrezervacija = null;
        }

        private void EnableDisable(bool x)
        {
            cbVD.Enabled = x;
            txtSifraOdrediste.Enabled = x;
            txtSifraFakturirati.Enabled = x;
            txtPartnerNaziv.Enabled = x;
            txtPartnerNaziv1.Enabled = x;
            txtSifraNacinPlacanja.Enabled = x;
            txtModel.Enabled = x;
            txtSifraNarKupca.Enabled = x;
            txtSifraRadniNalog.Enabled = x;
            txtNarKupca1.Enabled = x;
            cbOtprema.Enabled = x;
            rtbNapomena.Enabled = x;
            txtSifra_robe.Enabled = x;
            btnPartner.Enabled = x;
            btnPartner1.Enabled = x;
            dtpDatum.Enabled = x;
            dtpDatumDVO.Enabled = x;
            dtpDanaValuta.Enabled = x;
            cbIzjava.Enabled = x;
            cbKomercijalist.Enabled = x;
            cbNacinPlacanja.Enabled = x;
            cbZiroRacun.Enabled = x;
            cbValuta.Enabled = x;
            cbRadniBalog.Enabled = x;
            cbNarKupca.Enabled = x;
            nuGodinaPredujma.Enabled = x;
            cbPredujam.Enabled = x;
            btnObrisi.Enabled = x;
            btnOtpr.Enabled = x;
            btnOpenRoba.Enabled = x;
            btnRadniNalog.Enabled = x;
            btnNarKupca.Enabled = x;
            btnPredujam.Enabled = x;
            cmbPoslovnicaPartner.Enabled = x;
        }

        private void deleteFields(string broj_fakture = null)
        {
            ttxBrojFakture.Text = "";
            if (broj_fakture != null)
                ttxBrojFakture.Text = broj_fakture;

            //nmGodinaFakture.Value = Util.Korisno.GodinaKojaSeKoristiUbazi;
            txtBrojPonude.Text = "";
            //nuGodinaPonude.Value = Util.Korisno.GodinaKojaSeKoristiUbazi;
            txtBrojServisa.Text = "";
            //nuGodinaServisa.Value = Util.Korisno.GodinaKojaSeKoristiUbazi;

            chbPouzece.Checked = false;

            if (cmbPoslovnicaPartner.Items.Count > 0)
                cmbPoslovnicaPartner.SelectedIndex = 0;

            cbIzjava.SelectedIndex = 0;
            cbKomercijalist.SelectedValue = Properties.Settings.Default.id_zaposlenik;

            txtSifraOdrediste.Text = "";
            txtSifraFakturirati.Text = "";
            txtPartnerNaziv.Text = "";
            txtPartnerNaziv1.Text = "";
            txtModel.Text = "";
            txtSifraNarKupca.Text = "";
            txtSifraRadniNalog.Text = "";
            txtNarKupca1.Text = "";
            rtbNapomena.Text = "";
            txtSifra_robe.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            txtDana.Text = "0";

            dtpDatum.Value = DateTime.Now;
            dtpDatumDVO.Value = dtpDatum.Value;
            dtpDanaValuta.Value = dtpDatum.Value;

            dgw.Rows.Clear();
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            NoviUnos();
        }

        private void NoviUnos()
        {
            dodjeli_popust = 0;
            DSpartner = null;

            SetirajNaplatniPoslovnicuDefault();
            edit = false;
            EnableDisable(true);
            ttxBrojFakture.Text = brojFakture();
            ControlDisableEnable(0, 1, 1, 0, 1);
            ttxBrojFakture.ReadOnly = true;
            nmGodinaFakture.ReadOnly = true;
            txtSifraOdrediste.Select();
        }

        private string brojFakture()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj_fakture) FROM fakture", "fakture").Tables[0];
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
            DataTable DTsend = new DataTable();
            DTsend.Columns.Add("broj_fakture");
            DTsend.Columns.Add("sifra");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("mpc");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("nbc");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("oduzmi");
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("naziv");
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
            DTsend.Columns.Add("id_kasa");
            DTsend.Columns.Add("id_ducan");
            DTsend.Columns.Add("id_stavka");
            DTsend.Columns.Add("ppmv");
            return DTsend;
        }

        private void btnSpremi_Click_1(object sender, EventArgs e)
        {
            if (Class.PodaciTvrtka.oibTvrtke == Class.Postavke.OIB_PC1)
            {
                int t = (dgw.Rows.Cast<DataGridViewRow>().Where(x => x.Cells["oduzmi"].Value.ToString() == "DA").Count(x => ((bool)x.Cells["dozvoli_prodaju"].FormattedValue) == false));
                //if (t > 0) {
                //    MessageBox.Show("Imate odabrane artikle kojih nema na skladištu, nije dozvoljeno spremanje.");
                //    return;
                //}
            }

            dodjeli_popust = 0;
            DSpartner = null;

            double tecaj = 1;

            if (cbStavkeValuta.Checked)
                double.TryParse(txtTecaj.Text, out tecaj);

            if (chbOduzmiIzSkladista.Checked == false)
            {
                if (MessageBox.Show("Upozorenje ! Isključeno je skidanje robe iz skladišta ! \n\r Želite li nastaviti ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            DataTable dtZapisnikStavke = null;
            if (Class.Postavke.automatski_zapisnik)
                dtZapisnikStavke = Class.ZapisnikPromjeneCijene.createTableForSale();

            DataTable DTsend = new DataTable();
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("nbc");
            DTsend.Columns.Add("broj_fakture");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("sifra");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("oduzmi");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("povratna_naknada");
            DTsend.Columns.Add("naziv");
            DataRow row;

            if (dgw.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku za spremiti.", "Greška");
                return;
            }
            if (SveUkupno == 0)
            {
                MessageBox.Show("Ne možete završiti račun sa 0kn.", "Greška");
                return;
            }

            decimal dec_parse;
            if (Decimal.TryParse(txtSifraOdrediste.Text, out dec_parse))
            {
                txtSifraOdrediste.Text = dec_parse.ToString();
            }
            else
            {
                MessageBox.Show("Greška kod upisa odredišta.", "Greška");
                return;
            }

            if (Decimal.TryParse(txtSifraFakturirati.Text, out dec_parse))
            {
                txtSifraFakturirati.Text = dec_parse.ToString();
            }
            else
            {
                MessageBox.Show("Greška kod upisa šifre za fakturirati.", "Greška");
                return;
            }

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                if (dgw.Rows[i].Cells["skladiste"].Value == null & dgw.Rows[i].Cells["oduzmi"].FormattedValue.ToString() == "DA")
                {
                    MessageBox.Show("Faktura nije spremljena zbog ne odabira skladišta na pojedinim artiklima.", "Greška");
                    return;
                }
            }

            if (edit)
            {
                DataTable DT = classSQL.select("SELECT broj_fakture,zki FROM fakture WHERE broj_fakture='" + ttxBrojFakture.Text + "' AND id_ducan='" + id_ducan + "' AND id_kasa='" + id_kasa + "'", "fakture").Tables[0];

                if (DT.Rows[0]["zki"].ToString().Length > 1)
                {
                    MessageBox.Show("Nije moguće mijenjati ovu fakturu! Fiskalizirana!");
                }
                else
                {
                    UpdateFaktura();

                    if (Class.PodaciTvrtka.oibTvrtke == "88985647471")
                    {
                        string sqlM = @"update faktura_stavke a
                                        set proizvodacka_cijena = b.vpc
                                        from (
	                                        select c.id_stavka, (c.nbc::numeric / (1 + (35::numeric / 100::numeric))) as vpc
	                                        from faktura_stavke c
                                        ) b
                                        where a.broj_fakture='" + ttxBrojFakture.Text + "' AND a.id_ducan='" + id_ducan + "' AND a.id_kasa='" + id_kasa + "' and b.id_stavka = a.id_stavka;";

                        //classSQL.update(sqlM);
                    }
                    if (MessageBox.Show("Spremljeno.\r\nŽelite li ispisati dokument?", "Spremljeno.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        printaj(ttxBrojFakture.Text);
                    }
                }



                PregledKolicine();
                EnableDisable(false);
                deleteFields(brojFakture());
                btnSveFakture.Enabled = true;
                ControlDisableEnable(1, 0, 0, 1, 0);
                getPoslovnicaPartner();
                return;
            }

            string broj = brojFakture();
            if (broj.Trim() != ttxBrojFakture.Text.Trim())
            {
                MessageBox.Show("Vaš broj dokumenta već je netko iskoristio.\r\nDodijeljen Vam je novi broj '" + broj + "'.", "Info");
                ttxBrojFakture.Text = broj;
            }

            string uk = (u * tecaj).ToString();
            if (classSQL.remoteConnectionString == "")
            {
                uk = uk.Replace(",", ".");
            }
            else
            {
                uk = uk.Replace(".", ",");
            }

            string oduzmi_iz_skladista = "";
            if (chbOduzmiIzSkladista.Checked)
            {
                oduzmi_iz_skladista = "1";
            }
            else
            {
                oduzmi_iz_skladista = "0";
            }

            //--------STAVKE-------------
            DataTable DTsend1 = VratiTablicuSKolonama();

            double ukupnoMpcRabat, ukupnoVpc, ukupnoMpc, ukupnoPovratnaNaknada, ukupnoRabat, ukupnoPorez, ukupnoOsnovica;
            double kolicina, rabatIzn, rabat, mpc, porez, mpcRabat, vpc, povratnaNaknada, ppmv = 0;

            double povratnaNaknadaUkRacun = 0;
            double ukupnoMpcRabatRacun = 0;
            double ukupnoMpcRacun = 0;
            double ukupnoVpcRacun = 0;
            double ukupnoRabatRacun = 0;
            double ukupnoPorezRacun = 0;
            double ukupnoOsnovicaRacun = 0;
            double ppmv_ukp = 0;

            string sifra = "";
            string kol = "";

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                if (DTrezervacija != null)
                {
                    DataTable DTmax = classSQL.select("SELECT MAX(broj_fakture) FROM faktura_stavke", "faktura_stavke").Tables[0];
                    if (DTmax?.Rows.Count > 0)
                        sifra = $"!serial{Util.Korisno.GodinaKojaSeKoristiUbazi}{DTmax.Rows[0][0].ToString()}";
                }
                else
                    sifra = dg(i, "sifra");

                if (!Class.Dokumenti.isKasica && Class.Postavke.automatski_zapisnik)
                {
                    DataRow drRow = dtZapisnikStavke.NewRow();
                    drRow["sifra"] = sifra;
                    drRow["mpc"] = dg(i, "mpc");
                    drRow["kolicina"] = dg(i, "kolicina");
                    drRow["id_skladiste"] = dgw.Rows[i].Cells["skladiste"].Value;
                    dtZapisnikStavke.Rows.Add(drRow);
                }

                row = DTsend1.NewRow();
                row["sifra"] = ReturnSifra(sifra);
                row["id_skladiste"] = dgw.Rows[i].Cells["skladiste"].Value;
                row["kolicina"] = dg(i, "kolicina");
                row["rabat"] = dg(i, "rabat");
                row["ime"] = dg(i, "naziv");
                row["oduzmi"] = dg(i, "oduzmi");
                row["porez_potrosnja"] = "0";
                row["id_kasa"] = id_kasa;
                row["id_ducan"] = id_ducan;
                row["broj_fakture"] = ttxBrojFakture.Text;
                row["naziv"] = dgw.Rows[i].Cells["naziv"].Value;

                kolicina = Convert.ToDouble(dg(i, "kolicina"));

                //POVRATNA NAKNADA
                //DTtemp = classSQL.select("SELECT iznos FROM povratna_naknada WHERE sifra='" + sifra + "'", "povratna_naknada")
                //    .Tables[0];

                //if (DTtemp.Rows.Count > 0)
                //{
                ukupnoPovratnaNaknada = Math.Round(Convert.ToDouble(dg(i, "povratna_naknada")), 2);
                povratnaNaknada = ukupnoPovratnaNaknada / kolicina;
                //}
                //else
                //{
                //    povratnaNaknada = 0;
                //    ukupnoPovratnaNaknada = 0;
                //}

                povratnaNaknadaUkRacun += ukupnoPovratnaNaknada;
                //POVRATNA NAKNADA

                rabat = Convert.ToDouble(dg(i, "rabat"));
                mpc = Convert.ToDouble(dg(i, "mpc")) * tecaj;
                vpc = Convert.ToDouble(dg(i, "vpc")) * tecaj;
                porez = Convert.ToDouble(dg(i, "porez"));

                ukupnoMpc = Math.Round(mpc * kolicina, 3);
                ukupnoMpcRacun += ukupnoMpc;

                ukupnoVpc = Math.Round(vpc * kolicina, 4);
                ukupnoVpcRacun += ukupnoVpc;

                ukupnoMpcRabat = Math.Round(Convert.ToDouble(dg(i, "iznos_ukupno")) * tecaj, 3);
                ukupnoMpcRabatRacun += ukupnoMpcRabat;
                ukupnoRabat = ukupnoMpc - ukupnoMpcRabat;
                mpcRabat = Math.Round(ukupnoMpcRabat / kolicina, 3);
                rabatIzn = Math.Round(mpc - mpcRabat, 3);
                ukupnoRabatRacun += ukupnoRabat;

                ukupnoOsnovica = Math.Round((ukupnoMpcRabat - ukupnoPovratnaNaknada) / (1 + porez / 100), 3);
                ukupnoOsnovicaRacun += ukupnoOsnovica;
                ukupnoPorez = Math.Round(ukupnoMpcRabat - ukupnoPovratnaNaknada - ukupnoOsnovica, 3);
                ukupnoPorezRacun += ukupnoPorez;

                decimal rowMpc;
                decimal.TryParse(dg(i, "mpc"), out rowMpc);
                decimal rowVpc;
                decimal.TryParse(dg(i, "vpc"), out rowVpc);
                double.TryParse(dg(i, "ppmv"), out ppmv);
                ppmv_ukp += ppmv;

                if (classSQL.remoteConnectionString == "")
                {
                    row["mpc"] = Math.Round(mpc, 3).ToString("#0.00").Replace(",", ".");
                    row["porez"] = dg(i, "porez").Replace(",", ".");
                    row["vpc"] = Math.Round(vpc, 4).ToString("#0.0000").Replace(",", ".");
                    row["nbc"] = dg(i, "nc").Replace(",", ".");
                    row["povratna_naknada_izn"] = povratnaNaknada.ToString("#0.00").Replace(",", ".");
                    row["povratna_naknada"] = ukupnoPovratnaNaknada.ToString("#0.00").Replace(",", ".");
                    row["rabat_izn"] = rabatIzn.ToString("#0.00").Replace(",", ".");
                    row["mpc_rabat"] = mpcRabat.ToString("#0.00").Replace(",", ".");
                    row["ukupno_rabat"] = ukupnoRabat.ToString("#0.00").Replace(",", ".");
                    row["ukupno_vpc"] = ukupnoVpc.ToString("#0.0000").Replace(",", ".");
                    row["ukupno_mpc"] = ukupnoMpc.ToString("#0.00").Replace(",", ".");
                    row["ukupno_mpc_rabat"] = ukupnoMpcRabat.ToString("#0.00").Replace(",", ".");
                    row["ukupno_porez"] = ukupnoPorez.ToString("#0.00").Replace(",", ".");
                    row["ukupno_osnovica"] = ukupnoOsnovica.ToString("#0.00").Replace(",", ".");
                }
                else
                {
                    row["mpc"] = Math.Round(mpc, 3).ToString("#0.00").Replace(".", ",");
                    row["porez"] = dg(i, "porez").Replace(".", ",");
                    row["vpc"] = Math.Round(vpc, 4).ToString("#0.0000").Replace(".", ",");
                    row["nbc"] = dg(i, "nc").Replace(".", ",");
                    row["povratna_naknada_izn"] = povratnaNaknada.ToString("#0.00").Replace(".", ",");
                    row["povratna_naknada"] = ukupnoPovratnaNaknada.ToString("#0.00").Replace(".", ",");
                    row["rabat_izn"] = rabatIzn.ToString("#0.00").Replace(".", ",");
                    row["mpc_rabat"] = mpcRabat.ToString("#0.00").Replace(".", ",");
                    row["ukupno_rabat"] = ukupnoRabat.ToString("#0.00").Replace(".", ",");
                    row["ukupno_vpc"] = ukupnoVpc.ToString("#0.0000").Replace(".", ",");
                    row["ukupno_mpc"] = ukupnoMpc.ToString("#0.00").Replace(".", ",");
                    row["ukupno_mpc_rabat"] = ukupnoMpcRabat.ToString("#0.00").Replace(".", ",");
                    row["ukupno_porez"] = ukupnoPorez.ToString("#0.00").Replace(".", ",");
                    row["ukupno_osnovica"] = ukupnoOsnovica.ToString("#0.00").Replace(".", ",");
                    row["ppmv"] = ppmv.ToString("#0.00").Replace(".", ",");
                }

                DTsend1.Rows.Add(row);

                if (chbOduzmiIzSkladista.Checked)
                {
                    if (dg(i, "oduzmi") == "DA")
                    {
                        kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), dgw.Rows[i].Cells[3].Value.ToString(), dg(i, "kolicina"), "1", "-");
                        SQL.SQLroba_prodaja.UpdateRows(dgw.Rows[i].Cells[3].Value.ToString(), kol, dg(i, "sifra"));
                    }
                }

                sifra = dg(i, "sifra");
                if (sifra.Length > 4)
                {
                    if (sifra.Substring(0, 3) == "000")
                    {
                        string sqlnext = "UPDATE racun_popust_kod_sljedece_kupnje SET koristeno='DA' WHERE broj_racuna='" + sifra.Substring(3, sifra.Length - 3) + "' AND dokumenat='FA'";
                        classSQL.update(sqlnext);
                    }
                }
            }

            string PovratnaNaknadaUkRacun, UkupnoMpcRacun, UkupnoVpcRacun,
                UkupnoMpcRabatRacun, UkupnoRabatRacun, UkupnoOsnovicaRacun, UkupnoPorezRacun;

            if (classSQL.remoteConnectionString == "")
            {
                PovratnaNaknadaUkRacun = povratnaNaknadaUkRacun.ToString("#0.00").Replace(",", ".");
                UkupnoMpcRacun = ukupnoMpcRacun.ToString("#0.00").Replace(",", ".");
                UkupnoVpcRacun = ukupnoVpcRacun.ToString("#0.0000").Replace(",", ".");
                UkupnoMpcRabatRacun = ukupnoMpcRabatRacun.ToString("#0.00").Replace(",", ".");
                UkupnoRabatRacun = ukupnoRabatRacun.ToString("#0.00").Replace(",", ".");
                UkupnoOsnovicaRacun = ukupnoOsnovicaRacun.ToString("#0.00").Replace(",", ".");
                UkupnoPorezRacun = ukupnoPorezRacun.ToString("#0.00").Replace(",", ".");
            }
            else
            {
                PovratnaNaknadaUkRacun = povratnaNaknadaUkRacun.ToString("#0.00").Replace(",", ".");
                UkupnoMpcRacun = ukupnoMpcRacun.ToString("#0.00").Replace(",", ".");
                UkupnoVpcRacun = ukupnoVpcRacun.ToString("#0.0000").Replace(",", ".");
                UkupnoMpcRabatRacun = ukupnoMpcRabatRacun.ToString("#0.00").Replace(",", ".");
                UkupnoRabatRacun = ukupnoRabatRacun.ToString("#0.00").Replace(",", ".");
                UkupnoOsnovicaRacun = ukupnoOsnovicaRacun.ToString("#0.00").Replace(",", ".");
                UkupnoPorezRacun = ukupnoPorezRacun.ToString("#0.00").Replace(",", ".");
            }
            if (txtTecaj.Text == "")
            {
                txtTecaj.Text = "1";
            }

            DateTime datum = DateTime.Now;
            if (DateTime.Now.ToString("yyyy-MM-dd") != dtpDatum.Value.ToString("yyyy-MM-dd"))
            {
                datum = dtpDatum.Value;
            }

            string stavke_u_valuti = "0";
            if (cbStavkeValuta.Checked) { stavke_u_valuti = "1"; }

            string komercijalista = Properties.Settings.Default.id_zaposlenik;
            if (cbKomercijalist.SelectedValue != null)
                komercijalista = cbKomercijalist.SelectedValue.ToString();

            int izjav = 0;
            if (cbIzjava.SelectedValue == null && cbIzjava.Items.Count == 0)
            {
                //DataTable dt = classSQL.select("SELECT setval('izjava_id_izjava_seq', (SELECT MAX(id_izjava) FROM izjava)+1);", "izjava").Tables[0];
                classSQL.insert("INSERT INTO izjava (izjava) VALUES ('');");
                izjav = Convert.ToInt32(classSQL.select("select max(id_izjava) as id from izjava;", "izjava").Tables[0].Rows[0]["id"]);
            }
            bool kreiraniZapisnik = false;
            if (!Class.Dokumenti.isKasica && Class.Postavke.automatski_zapisnik)
            {
                Class.ZapisnikPromjeneCijene _zapisnik = new Class.ZapisnikPromjeneCijene(dtZapisnikStavke, false, datum.AddSeconds(-1), "Kreiranje zapisnika zbog promjene cijena na fakturi", ttxBrojFakture.Text + "/" + Util.Korisno.nazivPoslovnica + "/" + Class.Postavke.naplatni_uredaj_faktura, kreiranZapisnikZaFakturu);
                kreiraniZapisnik = _zapisnik.kreiraniZapisnik;
                _zapisnik = null;
            }
            string[] podaciAvio = (string[])lblAvioPodaci.Tag;

            string sql = "INSERT INTO fakture (broj_fakture, id_odrediste, id_fakturirati, date," +
                "dateDVO,datum_valute,id_izjava,id_zaposlenik,id_zaposlenik_izradio,model" +
                ",id_nacin_placanja,zr,id_valuta,otprema,id_predujam,napomena,id_vd,godina_predujma," +
                "godina_ponude, godina_fakture, oduzmi_iz_skladista, tecaj, ukupno, storno," +
                "ukupno_povratna_naknada,ukupno_mpc,ukupno_vpc,ukupno_mpc_rabat,ukupno_rabat,ukupno_osnovica,stavke_u_valuti,ukupno_porez,id_ducan,id_kasa,novo, editirano, partner_poslovnica, faktura_za_komisiju" + (lblAvioPodaci.Tag != null ? ", avio_registracija, avio_tip_zrakoplova, avio_maks_tezina_polijetanja" : "") + ", pouzece)" +
                " VALUES " +
                " (" +
                " '" + ttxBrojFakture.Text + "'," +
                " '" + txtSifraOdrediste.Text + "'," +
                " '" + txtSifraFakturirati.Text + "'," +
                " '" + datum.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " '" + dtpDatumDVO.Value.ToString("yyyy-MM-dd") + "'," +
                " '" + dtpDanaValuta.Value.ToString("yyyy-MM-dd") + "'," +
                " '" + (cbIzjava.SelectedValue == null ? izjav.ToString() : cbIzjava.SelectedValue) + "'," +
                " '" + komercijalista + "'," +
                " '" + Properties.Settings.Default.id_zaposlenik + "'," +
                " '" + txtModel.Text + "'," +
                " '" + cbNacinPlacanja.SelectedValue.ToString() + "'," +
                " '" + cbZiroRacun.SelectedValue.ToString() + "'," +
                " '" + cbValuta.SelectedValue.ToString() + "'," +
                " '" + cbOtprema.SelectedValue + "'," +
                " '" + "0" + "'," +
                " '" + rtbNapomena.Text + "'," +
                " '" + cbVD.SelectedValue.ToString().Trim() + "'," +
                " '" + nuGodinaPredujma.Value.ToString() + "'," +
                " '" + nuGodinaPonude.Value.ToString() + "'," +
                " '" + nmGodinaFakture.Value.ToString() + "'," +
                " '" + oduzmi_iz_skladista + "'," +
                " '" + txtTecaj.Text.Replace(",", ".") + "'," +
                " '" + uk + "'," +
                " 'NE'," +
                "'" + PovratnaNaknadaUkRacun + "'," +
                "'" + UkupnoMpcRacun + "'," +
                "'" + UkupnoVpcRacun + "'," +
                "'" + UkupnoMpcRabatRacun + "'," +
                "'" + UkupnoRabatRacun + "'," +
                "'" + UkupnoOsnovicaRacun + "'," +
                "'" + stavke_u_valuti + "'," +
                "'" + UkupnoPorezRacun + "','" + id_ducan + "','" + id_kasa + "'," +
                "'1'," +
                "'1'," +
                "'" + (cmbPoslovnicaPartner.SelectedValue == null ? 0 : cmbPoslovnicaPartner.SelectedValue) + "', " +
                "'" + (chbFakturaZaKomisiju.Checked ? 1 : 0) + "' " +
                (lblAvioPodaci.Tag != null ? string.Format(",'{0}','{1}','{2}'", podaciAvio[0], podaciAvio[1], podaciAvio[2]) : "") +
                ", '" + (chbPouzece.Checked ? 1 : 0) + "'" +
                ")";
            provjera_sql(classSQL.insert(sql));

            //Class.Postavke.fiskalizacija_faktura_prikazi_obavijest

            if (Class.Postavke.fiskalizacija_faktura_prikazi_obavijest)
            {
                if (MessageBox.Show("Fiskalizirati fakturu?", "Fiskalizacija?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int brFak = Convert.ToInt32(ttxBrojFakture.Text);
                    FiskalizirajFakturu(DTsend1, datum, brFak);
                }
            }


            provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Nova faktura br." + ttxBrojFakture.Text + "')"));
            if (chbOduzmiIzSkladista.Checked)
                Util.AktivnostZaposlenika.SpremiAktivnost(dgw, null, "Faktura", ttxBrojFakture.Text, false);
            provjera_sql(SQL.SQLfaktura.InsertStavke(DTsend1, true));

            if (Class.PodaciTvrtka.oibTvrtke == "88985647471")
            {
                string sqlM = @"update faktura_stavke a
set proizvodacka_cijena = b.vpc
from (
	select c.id_stavka, (c.nbc::numeric / (1 + (35::numeric / 100::numeric))) as vpc
	from faktura_stavke c
) b
where b.id_stavka = a.id_stavka;";

                //classSQL.update(sqlM);
            }

            if (MessageBox.Show("Spremljeno.\r\nŽelite li ispisati dokument?", "Spremljeno.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                printaj(ttxBrojFakture.Text);
            }

            if (DTrezervacija != null)
                Global.Database.UpdateRezervacijaNaplaceno(DTrezervacija.Rows[0]["broj"].ToString(), 1);

            if (fromJSON)
                MoveJSON();

            PregledKolicine();

            edit = false;
            fromJSON = false;
            EnableDisable(false);
            deleteFields();
            btnSveFakture.Enabled = true;
            ttxBrojFakture.ReadOnly = false;
            nmGodinaFakture.ReadOnly = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
            chbOduzmiIzSkladista.Enabled = true;
            btnObrisi.Enabled = true;
            btnOtpr.Enabled = true;

            if (popunjena_s_otpremnicom)
            {
                for (int i = 0; i < DTnaplaceneotpremnice.Rows.Count; i++)
                {
                    string pop_s_otpr = "Update otpremnica_stavke Set naplaceno_fakturom = '1' WHERE id_otpremnice = '" + DTnaplaceneotpremnice.Rows[i]["id_otpremnica"].ToString() + "' ";
                    classSQL.update(pop_s_otpr);
                }
            }

            SetirajNaplatniPoslovnicuDefault();
            getPoslovnicaPartner();

            if (!Class.Dokumenti.isKasica && Class.Postavke.automatski_zapisnik)
            {
                kreiranZapisnikZaFakturu = 0;

                if (kreiraniZapisnik)
                {
                    MessageBox.Show("Kreiran je automatski zapisnik zbog promjene cijene na fakturi");
                }
            }

            idSkladiste = 0;
            DTrezervacija = null;
        }

        /// <summary>
        /// Used to move JSON file to "JSON/Kreirani" directory
        /// </summary>
        private void MoveJSON()
        {
            foreach (string file in Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory + "JSON", "*.txt"))
            {
                string fileName = Path.GetFileName(file);
                string from = AppDomain.CurrentDomain.BaseDirectory + $"JSON\\{fileName}";
                string to = AppDomain.CurrentDomain.BaseDirectory + $"JSON\\Kreirani\\{fileName}";
                File.Move(from, to);
            }
        }

        private void PregledKolicine()
        {
            //OVO RADI SPREMLJENA PROCEDURA
            try
            {
                if (Class.Postavke.skidajSkladisteProgramski)
                {
                    string _sql = "";

                    foreach (DataGridViewRow r in dgw.Rows)
                    {
                        _sql += $@"SELECT postavi_kolicinu_sql_funkcija_prema_sifri('{r.Cells["sifra"].FormattedValue.ToString()}', '{Global.Functions.GetDateParam()}'') AS odgovor; ";
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

        #region fiskalizacija helper

        private void FiskalizirajFakturu(DataTable DTsend, DateTime datum, int brojFakture)
        {
            DataSet DSkolicina = new DataSet();

            dodajKoloneDTpdv();
            DTpdv.Clear();

            double mnozeno = 1;
            double osnovicaStavka, pdvStavka;
            double osnovicaSve = 0;
            double pdvSve = 0;
            double povratnaNaknadaSve = 0;
            double rabatSve = 0;
            double kolNaknada;
            double povratnaNaknada;
            DataTable DTtemp;
            string sifra = "";
            string kol = "";

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                //ovo zakomentirano porez na potrošnju ne treba kod maloprodaje (?)
                double kolicina = Convert.ToDouble(dg(i, "kolicina").ToString());
                mnozeno = kolicina >= 0 ? 1 : -1;
                //double PP = Convert.ToDouble(DTstavke.Rows[i]["porez_potrosnja"].ToString());
                double PDV = Convert.ToDouble(dg(i, "porez").ToString());
                double VPC = Convert.ToDouble(dg(i, "vpc").ToString());
                double rabat = Convert.ToDouble(dg(i, "rabat").ToString());
                povratnaNaknada = Convert.ToDouble(dg(i, "povratna_naknada"));

                //double povratnaNaknada;
                ////mora biti tak jer prije nije postojala povratna naknada!
                //try
                //{
                //    povratnaNaknada = Convert.ToDouble(row["povratna_naknada"].ToString());
                //}
                //catch
                //{
                //    povratnaNaknada = 0;
                //}

                //double cijena = ((VPC * (PP + PDV) / 100) + VPC);
                double cijena = Math.Round(VPC * PDV / 100 + VPC - 0.0000001, 2);
                //double cijena = Math.Round(VPC * PDV / 100 + VPC, 2);
                double mpc = cijena * kolicina * (1 - rabat / 100);
                mpc = Convert.ToDouble(mpc.ToString("#0.00"));

                rabatSve += cijena * kolicina - mpc;

                //Ovaj kod dobiva PDV
                //double PreracunataStopaPDV = Convert.ToDouble((100 * PDV) / (100 + PDV + PP));
                double PreracunataStopaPDV = (100 * PDV) / (100 + PDV);

                //Ovaj kod dobiva porez na potrošnju
                //double PreracunataStopaPorezNaPotrosnju = Convert.ToDouble((100 * PP) / (100 + PDV + PP));
                double PreracunataStopaPorezNaPotrosnju = 100 / (100 + PDV);

                //treba smanjiti za iznos povratne naknade

                povratnaNaknada *= mnozeno;
                osnovicaStavka = (mpc - povratnaNaknada) / (1 + PDV / 100);
                pdvStavka = ((mpc - povratnaNaknada) * PreracunataStopaPDV) / 100;

                dodajPDV(PDV, osnovicaStavka);

                osnovicaSve += osnovicaStavka;

                pdvSve += pdvStavka;

                povratnaNaknadaSve += povratnaNaknada;
            }

            //--------------------------------------------------------
            //fiskalizacija

            DataTable DTOstaliPor = PosPrint.classPosPrintMaloprodaja2.dodajKoloneDTOstaliPor();
            DataTable DTnaknade = PosPrint.classPosPrintMaloprodaja2.dodajKoloneDTnaknade();

            if (povratnaNaknadaSve != 0)
            {
                RowPdv = DTnaknade.NewRow();
                RowPdv["iznos"] = povratnaNaknadaSve.ToString("0.00");
                RowPdv["naziv"] = "Povratna naknada";
                DTnaknade.Rows.Add(RowPdv);
            }

            double porezPotrosnjaSve = 0;

            string[] porezNaPotrosnju = setPorezNaPotrosnju();
            //porezNaPotrosnju[0] = DTpostavke.Rows[0]["porez_potrosnja"].ToString();
            porezNaPotrosnju[0] = Class.Postavke.porez_potrosnja.ToString();
            porezNaPotrosnju[1] = Convert.ToString(osnovicaSve);
            porezNaPotrosnju[2] = Convert.ToString(porezPotrosnjaSve);

            string iznososlobpdv = "";
            string iznos_marza = "";

            string np = Util.Korisno.VratiNacinPlacanja(cbNacinPlacanja.Text.ToLower());

            bool pdv = Class.Postavke.sustavPdv;

            string oib = DToib.Rows.Count > 0 ? DToib.Rows[0][0].ToString() : "";

            string[] fiskalizacija = new string[3];

            try
            {
                fiskalizacija = Fiskalizacija.classFiskalizacija.Fiskalizacija(
                    Class.PodaciTvrtka.oibTvrtke,
                    oib,
                    datum,
                    pdv,
                    brojFakture,
                    DTpdv,
                    porezNaPotrosnju,
                    DTOstaliPor,
                    iznososlobpdv,
                    iznos_marza,
                    DTnaknade,
                    Convert.ToDecimal(u),//.ToString().Replace(",", ".")
                    np,
                    false,
                    "faktura"
                    );
            }
            catch
            {
                fiskalizacija = new string[3];
                fiskalizacija[0] = "";
                fiskalizacija[1] = "";
                fiskalizacija[2] = "";
            }

            //ažuriraj fakturu sa zki i jir
            string sql = "UPDATE fakture SET zki = '" + fiskalizacija[1] + "', jir='" + fiskalizacija[0] + "'" +
                " WHERE broj_fakture='" + brojFakture + "' AND id_ducan='" + id_ducan + "' AND id_kasa='" + id_kasa + "'";
            provjera_sql(classSQL.update(sql));
        }

        /// <summary>
        /// Dodaje kolone tablici DTpdv ako još nisu dodane
        /// </summary>
        private static void dodajKoloneDTpdv()
        {
            if (DTpdv.Columns["stopa"] == null)
            {
                DTpdv.Columns.Add("stopa");
                DTpdv.Columns.Add("osnovica");
                DTpdv.Columns.Add("iznos");
            }
        }

        /// <summary>
        /// dodaje stopu PDV-a i iznos u tablicu DTpdv ako ne postoji stopa;
        /// ako postoji zbraja s postojećim iznosom
        /// </summary>
        /// <param name="stopa"></param>
        /// <param name="iznos"></param>
        private static void dodajPDV(double stopa, double iznos)
        {
            DataRow[] dataROW = DTpdv.Select("stopa = '" + stopa.ToString("0.00") + "'");

            if (dataROW.Count() == 0)
            {
                RowPdv = DTpdv.NewRow();
                RowPdv["stopa"] = stopa.ToString("0.00");
                RowPdv["iznos"] = (iznos * stopa / 100).ToString();
                RowPdv["osnovica"] = iznos.ToString();
                DTpdv.Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["iznos"] = Convert.ToDouble(dataROW[0]["iznos"].ToString()) + iznos * stopa / 100;
                dataROW[0]["osnovica"] = Convert.ToDouble(dataROW[0]["osnovica"].ToString()) + iznos;
            }
        }

        /// <summary>
        /// postavlja porez_na_potrosnju na empty string
        /// </summary>
        /// <returns></returns>
        private static string[] setPorezNaPotrosnju()
        {
            string[] porez_na_potrosnju = new string[3];
            porez_na_potrosnju[0] = "";
            porez_na_potrosnju[1] = "";
            porez_na_potrosnju[2] = "";

            return porez_na_potrosnju;
        }

        #endregion fiskalizacija helper

        private void printaj(string broj)
        {
            //Report.Faktura.repFaktura pr = new Report.Faktura.repFaktura();
            //pr.dokumenat = "FAK";
            //pr.racunajTecaj = ValutaKuna(cbValuta.Text);
            //pr.broj_dokumenta = broj;
            //pr.poslovnica = id_ducan;
            //pr.naplatni = id_kasa;
            //pr.ImeForme = "Faktura";
            //pr.ShowDialog();

            if (Class.Postavke.idFaktura == 1)
            {
                Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();

                rfak.dokumenat = "FAK";
                rfak.racunajTecaj = ValutaKuna(cbValuta.Text);
                rfak.poslovnica = id_ducan;
                rfak.naplatni = id_kasa;
                rfak.ImeForme = "Fakture";
                rfak.broj_dokumenta = broj;
                rfak.ShowDialog();
            }
            else if (Class.Postavke.idFaktura == 2)
            {
                Report.Faktura.repFakturaNovo rfak = new Report.Faktura.repFakturaNovo();

                rfak.dokumenat = "FAK";
                rfak.racunajTecaj = ValutaKuna(cbValuta.Text); ;
                rfak.poslovnica = id_ducan;
                rfak.naplatni = id_kasa;
                rfak.ImeForme = "Fakture";
                rfak.broj_dokumenta = broj;
                rfak.ShowDialog();
            }
            else if (Class.Postavke.idFaktura == 3)
            {
                Report.Faktura3.repFaktura3 rfak = new Report.Faktura3.repFaktura3();
                rfak.dokumenat = "FAK";
                rfak.racunajTecaj = ValutaKuna(cbValuta.Text); ;
                rfak.poslovnica = id_ducan;
                rfak.naplatni = id_kasa;
                rfak.ImeForme = "Fakture";
                rfak.broj_dokumenta = broj;
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

        private string dg(int row, string cell)
        {
            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        private void btnOpenRoba_Click(object sender, EventArgs e)
        {
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            if (idSkladiste != 0)
            {
                roba_trazi.idSkladiste = idSkladiste;
            }
            roba_trazi.ShowDialog();
            string roba_sifra = Properties.Settings.Default.id_roba.Trim();
            if (roba_sifra != "")
            {
                for (int y = 0; y < dgw.Rows.Count; y++)
                {
                    if (roba_sifra == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString())
                    {
                        MessageBox.Show("Artikl ili usluga već postoje!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string sql = "SELECT * FROM roba WHERE sifra='" + roba_sifra + "'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;

                    SetRoba();

                    ttxBrojFakture.ReadOnly = true;
                    nmGodinaFakture.ReadOnly = true;
                    cbValuta.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cbNacinPlacanja_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();
        }

        private void provjera_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender.ToString().Length == 0)
                return;

            if (sender.ToString()[0] == '+')
            {
                if ('+' == (e.KeyChar))
                    return;
            }

            if (sender.ToString()[0] == '-')
            {
                if ('-' == (e.KeyChar))
                    return;
            }

            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            frmSveFakture objForm2 = new frmSveFakture();
            objForm2.sifra_fakture = "";
            objForm2.MainForm = this;
            objForm2.ShowDialog();
            if (broj_fakture_edit != null)
            {
                deleteFields();
                fillFaktute();
            }
        }

        private void fillFaktute()
        {
            ucitano = false;
            //fill header

            EnableDisable(true);
            edit = true;
            ControlDisableEnable(0, 1, 1, 0, 1);

            DSfakture = classSQL.select("SELECT * FROM fakture WHERE broj_fakture = '" + broj_fakture_edit + "'  AND id_ducan='" + id_ducan + "' AND id_kasa='" + id_kasa + "'", "fakture").Tables[0];

            string[] avioPodaci = null;
            if (DSfakture.Rows[0]["avio_registracija"] != null && DSfakture.Rows[0]["avio_tip_zrakoplova"] != null && DSfakture.Rows[0]["avio_maks_tezina_polijetanja"] != null &&
                DSfakture.Rows[0]["avio_registracija"].ToString().Length > 0 && DSfakture.Rows[0]["avio_tip_zrakoplova"].ToString().Length > 0 && DSfakture.Rows[0]["avio_maks_tezina_polijetanja"].ToString().Length > 0)
            {
                avioPodaci = new string[3];
                avioPodaci[0] = DSfakture.Rows[0]["avio_registracija"].ToString();
                avioPodaci[1] = DSfakture.Rows[0]["avio_tip_zrakoplova"].ToString();
                avioPodaci[2] = DSfakture.Rows[0]["avio_maks_tezina_polijetanja"].ToString();

                lblAvioPodaci.Text = string.Format(@"Reg. oznaka: {0}{3}Tip: {1}{3}MTOW: {2}", avioPodaci[0], avioPodaci[1], avioPodaci[2], Environment.NewLine);
                prikaziAvioPodatke(true);
            }

            //if (avioPodaci[0] == null || avioPodaci[0].Length == 0)
            //    avioPodaci = null;

            lblAvioPodaci.Tag = avioPodaci;

            if (DSfakture.Rows[0]["stavke_u_valuti"].ToString() == "1")
            {
                cbStavkeValuta.Checked = true;
            }
            else
            {
                cbStavkeValuta.Checked = false;
            }

            decimal tecaj = 1;

            if (cbStavkeValuta.Checked)
                decimal.TryParse(DSfakture.Rows[0]["tecaj"].ToString(), out tecaj);

            cbVD.SelectedValue = DSfakture.Rows[0]["id_vd"].ToString();
            txtTecaj.Text = DSfakture.Rows[0]["tecaj"].ToString();
            txtSifraOdrediste.Text = DSfakture.Rows[0]["id_odrediste"].ToString();
            txtSifraFakturirati.Text = DSfakture.Rows[0]["id_fakturirati"].ToString();

            getPoslovnicaPartner(); // --------------

            txtPartnerNaziv.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DSfakture.Rows[0]["id_odrediste"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString();
            txtPartnerNaziv1.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DSfakture.Rows[0]["id_fakturirati"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString();
            txtSifraNacinPlacanja.Text = DSfakture.Rows[0]["id_nacin_placanja"].ToString();
            txtModel.Text = DSfakture.Rows[0]["model"].ToString();
            cbOtprema.SelectedValue = DSfakture.Rows[0]["otprema"].ToString();
            rtbNapomena.Text = DSfakture.Rows[0]["napomena"].ToString();
            dtpDatum.Value = Convert.ToDateTime(DSfakture.Rows[0]["date"].ToString());
            dtpDatumDVO.Value = Convert.ToDateTime(DSfakture.Rows[0]["dateDVO"].ToString());
            dtpDanaValuta.Value = Convert.ToDateTime(DSfakture.Rows[0]["datum_valute"].ToString());

            try
            {
                cbKomercijalist.SelectedValue = DSfakture.Rows[0]["id_zaposlenik"].ToString();
            }
            catch { }
            cbNacinPlacanja.SelectedValue = DSfakture.Rows[0]["id_nacin_placanja"].ToString();
            cbZiroRacun.SelectedValue = DSfakture.Rows[0]["zr"].ToString();
            cbValuta.SelectedValue = DSfakture.Rows[0]["id_valuta"].ToString();
            nuGodinaPredujma.Value = Convert.ToInt16(DSfakture.Rows[0]["godina_predujma"].ToString());
            nuGodinaPonude.Value = Convert.ToInt16(DSfakture.Rows[0]["godina_ponude"].ToString());
            nmGodinaFakture.Value = Convert.ToInt16(DSfakture.Rows[0]["godina_fakture"].ToString());
            cbPredujam.SelectedValue = DSfakture.Rows[0]["id_predujam"].ToString();
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + DSfakture.Rows[0]["id_zaposlenik_izradio"].ToString() + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
            ttxBrojFakture.Text = DSfakture.Rows[0]["broj_fakture"].ToString();
            //MessageBox.Show(DSfakture.Rows[0]["id_odrediste"].ToString());
            chbPouzece.Checked = Convert.ToBoolean(DSfakture.Rows[0]["pouzece"].ToString());

            id_ducan = DSfakture.Rows[0]["id_ducan"].ToString();
            id_kasa = DSfakture.Rows[0]["id_kasa"].ToString();

            if (DSfakture.Rows[0]["oduzmi_iz_skladista"].ToString() == "1")
            {
                chbOduzmiIzSkladista.Checked = true;
            }
            else
            {
                chbOduzmiIzSkladista.Checked = false;
            }
            chbOduzmiIzSkladista.Enabled = false;

            cmbPoslovnicaPartner.SelectedValue = DSfakture.Rows[0]["partner_poslovnica"];
            chbFakturaZaKomisiju.Checked = Convert.ToBoolean(DSfakture.Rows[0]["faktura_za_komisiju"]);
            //if (Convert.ToBoolean(DSfakture.Rows[0]["use_nbc"].ToString())) {
            //    chbSNBC.Checked = true;
            //} else {
            //    chbSNBC.Checked = false;
            //}

            //--------fill faktura stavke------------------------------

            DataTable dtR = new DataTable();
            DSFS = classSQL.select("SELECT * FROM faktura_stavke WHERE broj_fakture = '" + DSfakture.Rows[0]["broj_fakture"].ToString() + "'  AND id_ducan='" + id_ducan + "' AND id_kasa='" + id_kasa + "'", "broj_fakture").Tables[0];

            for (int i = 0; i < DSFS.Rows.Count; i++)
            {
                SQL.ClassSkladiste.provjeri_roba_prodaja(DSFS.Rows[i]["sifra"].ToString(), DSFS.Rows[i]["id_skladiste"].ToString());
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;
                string s = "";
                if (DSFS.Rows[i]["oduzmi"].ToString() == "DA")
                {
                    s = "SELECT roba_prodaja.nc,roba.naziv,roba.jm,roba_prodaja.sifra,roba.oduzmi FROM roba_prodaja INNER JOIN roba ON roba_prodaja.sifra=roba.sifra WHERE roba_prodaja.sifra='" + DSFS.Rows[i]["sifra"].ToString() + "' AND roba_prodaja.id_skladiste='" + DSFS.Rows[i]["id_skladiste"].ToString() + "'";
                }
                else
                {
                    s = "SELECT roba.nc,roba.naziv,roba.jm,roba.sifra,roba.oduzmi FROM roba WHERE sifra='" + DSFS.Rows[i]["sifra"].ToString() + "'";
                }

                dtR = classSQL.select(s, "roba_prodaja").Tables[0];
                decimal vpc, povratna_naknada = 0, pdv = 0;
                decimal.TryParse(DSFS.Rows[i]["vpc"].ToString(), out vpc);
                decimal.TryParse(DSFS.Rows[i]["povratna_naknada"].ToString(), out povratna_naknada);
                decimal.TryParse(DSFS.Rows[i]["porez"].ToString(), out pdv);

                povratna_naknada = povratna_naknada / Convert.ToDecimal(DSFS.Rows[i]["kolicina"].ToString());

                decimal mpc = vpc * (1 + pdv / 100)/* - povratna_naknada*/;

                vpc = mpc / (1 + pdv / 100);

                if (dtR.Rows.Count == 0)
                {
                    if (DSFS.Rows[i]["sifra"].ToString().Length > 2)
                    {
                        DSFS.Rows[i]["sifra"] = DSFS.Rows[i]["sifra"].ToString().Remove(0, 3);

                        dtR = classSQL.select("SELECT * FROM fakture WHERE broj_fakture='" + DSFS.Rows[i]["sifra"].ToString() + "'  AND id_ducan='" + id_ducan + "' AND id_kasa='" + id_kasa + "'", "broj_fakture").Tables[0];
                        if (dtR.Rows.Count > 0)
                        {
                            dgw.Rows[br].Cells[0].Value = i + 1;
                            dgw.Rows[br].Cells["sifra"].Value = "000" + DSFS.Rows[i]["sifra"].ToString();
                            dgw.Rows[br].Cells["naziv"].Value = "POPUST";
                            dgw.Rows[br].Cells["jmj"].Value = "kom";
                            dgw.Rows[br].Cells["oduzmi"].Value = "NE";
                            dgw.Rows[br].Cells["cijena_bez_pdva"].Value = Math.Round(vpc / tecaj, 3).ToString("#0.000");
                            dgw.Rows[br].Cells["kolicina"].Value = DSFS.Rows[i]["kolicina"].ToString();
                            dgw.Rows[br].Cells["porez"].Value = DSFS.Rows[i]["porez"].ToString();
                            dgw.Rows[br].Cells["rabat"].Value = DSFS.Rows[i]["rabat"].ToString();
                            dgw.Rows[br].Cells["skladiste"].Value = DSFS.Rows[i]["id_skladiste"].ToString();
                            dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
                            dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
                            dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
                            dgw.Rows[br].Cells["vpc"].Value = Math.Round(vpc / tecaj, 3).ToString("#0.000");
                            //dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["mpc"]);
                            dgw.Rows[br].Cells["nc"].Value = string.Format("{0:0.000}", DSFS.Rows[i]["nbc"]);
                            dgw.Rows[br].Cells["id_stavka"].Value = DSFS.Rows[i]["id_stavka"].ToString();
                            dgw.Rows[br].Cells["ppmv"].Value = DSFS.Rows[i]["ppmv"].ToString();
                            //gw.Rows[br].Cells["porez_potrosnja"].Value = DSFS.Rows[i]["porez_potrosnja"].ToString();

                            dgw.Select();
                            dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[3];

                            //povratna naknada mora biti poslije izračuna jer bi se inače upisivala trenutna povratna naknada
                            //a ne povratna naknada upisana u fakturi
                            dgw.Rows[br].Cells["povratna_naknada"].Value = DSFS.Rows[i]["povratna_naknada"].ToString();
                            //txtPovrNaknada.Text = DSFS.Rows[i]["povratna_naknada"].ToString() == "" ? "0,00" : Math.Round(Convert.ToDouble(DSFS.Rows[i]["povratna_naknada"].ToString()), 2).ToString("#0.00");

                            izracun(false);
                        }
                    }
                }
                else if (dtR.Rows.Count > 0)
                {
                    dgw.Rows[br].Cells[0].Value = i + 1;
                    dgw.Rows[br].Cells["sifra"].Value = dtR.Rows[0]["sifra"].ToString();
                    dgw.Rows[br].Cells["naziv"].Value = dtR.Rows[0]["naziv"].ToString();
                    dgw.Rows[br].Cells["jmj"].Value = dtR.Rows[0]["jm"].ToString();
                    dgw.Rows[br].Cells["oduzmi"].Value = dtR.Rows[0]["oduzmi"].ToString();
                    dgw.Rows[br].Cells["cijena_bez_pdva"].Value = Math.Round(vpc / tecaj, 3).ToString("#0.000");
                    dgw.Rows[br].Cells["kolicina"].Value = DSFS.Rows[i]["kolicina"].ToString();
                    dgw.Rows[br].Cells["porez"].Value = DSFS.Rows[i]["porez"].ToString();
                    dgw.Rows[br].Cells["rabat"].Value = DSFS.Rows[i]["rabat"].ToString();
                    dgw.Rows[br].Cells["naziv"].Value = DSFS.Rows[i]["naziv"].ToString();
                    dgw.Rows[br].Cells["skladiste"].Value = DSFS.Rows[i]["id_skladiste"].ToString();
                    dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
                    dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
                    dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
                    dgw.Rows[br].Cells["vpc"].Value = Math.Round(vpc / tecaj, 3).ToString("#0.000");
                    //dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["mpc"]);
                    dgw.Rows[br].Cells["nc"].Value = string.Format("{0:0.00}", dtR.Rows[0]["nc"].ToString());
                    dgw.Rows[br].Cells["id_stavka"].Value = DSFS.Rows[i]["id_stavka"].ToString();
                    dgw.Rows[br].Cells["povratna_naknada"].Value = DSFS.Rows[i]["povratna_naknada"].ToString();
                    //dgw.Rows[br].Cells["porez_potrosnja"].Value = DSFS.Rows[i]["porez_potrosnja"].ToString();
                    dgw.Rows[br].Cells["ppmv"].Value = DSFS.Rows[i]["ppmv"].ToString();

                    dgw.Select();
                    dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[3];

                    //povratna naknada mora biti poslije izračuna jer bi se inače upisivala trenutna povratna naknada
                    //a ne povratna naknada upisana u fakturi
                    dgw.Rows[br].Cells["povratna_naknada"].Value = DSFS.Rows[i]["povratna_naknada"].ToString();
                    //txtPovrNaknada.Text = DSFS.Rows[i]["povratna_naknada"].ToString() == "" ? "0,00" : Math.Round(Convert.ToDouble(DSFS.Rows[i]["povratna_naknada"].ToString()), 2).ToString("#0.00");

                    izracun(false);
                }
            }

            dgw.Columns["cijena_bez_pdva"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["rabat_iznos"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos_bez_pdva"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos_ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgw.Columns["cijena_bez_pdva"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["rabat_iznos"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos_bez_pdva"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos_ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            PartnerPostaviPopust(txtSifraOdrediste.Text);
            //DataGridViewCellStyle style = new DataGridViewCellStyle();
            //style.Format = "N2";
            //dgw.Columns["cijena_bez_pdva"].DefaultCellStyle = style;
            //dgw.Columns["rabat_iznos"].DefaultCellStyle = style;
            //dgw.Columns["iznos_bez_pdva"].DefaultCellStyle = style;
            //dgw.Columns["iznos_ukupno"].DefaultCellStyle = style;
            ucitano = true;
        }

        private void UpdateFaktura()
        {
            DataRow row;
            DataRow row1;

            double tecaj = 1;
            if (cbStavkeValuta.Checked)
                double.TryParse(txtTecaj.Text, out tecaj);

            //--------STAVKE-------------
            DataTable DTsend1 = VratiTablicuSKolonama();
            DataTable DTsend = VratiTablicuSKolonama();

            DataTable dtZapisnikStavke = null;
            if (Class.Postavke.automatski_zapisnik)
                dtZapisnikStavke = Class.ZapisnikPromjeneCijene.createTableForSale();

            double ukupnoMpcRabat, ukupnoVpc, ukupnoMpc, ukupnoPovratnaNaknada, ukupnoRabat, ukupnoPorez, ukupnoOsnovica;
            double kolicina, rabatIzn, rabat, mpc, porez, mpcRabat, vpc, povratnaNaknada, ppmv = 0;

            double povratnaNaknadaUkRacun = 0;
            double ukupnoMpcRabatRacun = 0;
            double ukupnoMpcRacun = 0;
            double ukupnoVpcRacun = 0;
            double ukupnoRabatRacun = 0;
            double ukupnoPorezRacun = 0;
            double ukupnoOsnovicaRacun = 0;
            double ppmv_ukp = 0;

            string sifra = "";
            string kol = "";

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                sifra = dg(i, "sifra");

                if (!Class.Dokumenti.isKasica && Class.Postavke.automatski_zapisnik)
                {
                    DataRow drRow = dtZapisnikStavke.NewRow();
                    drRow["sifra"] = sifra;
                    drRow["mpc"] = dg(i, "mpc");
                    drRow["kolicina"] = dg(i, "kolicina");
                    drRow["id_skladiste"] = dgw.Rows[i].Cells["skladiste"].Value;
                    drRow["id_stavka"] = dgw.Rows[i].Cells["id_stavka"].Value;
                    dtZapisnikStavke.Rows.Add(drRow);
                }

                kolicina = Convert.ToDouble(dg(i, "kolicina"));

                ukupnoPovratnaNaknada = Math.Round(Convert.ToDouble(dg(i, "povratna_naknada")), 2);
                povratnaNaknada = ukupnoPovratnaNaknada / kolicina;

                povratnaNaknadaUkRacun += ukupnoPovratnaNaknada;

                double.TryParse(dg(i, "rabat"), out rabat);
                double.TryParse(dg(i, "mpc"), out mpc);
                double.TryParse(dg(i, "vpc"), out vpc);
                double.TryParse(dg(i, "porez"), out porez);
                double.TryParse(dg(i, "ppmv"), out ppmv);

                mpc -= ppmv;
                ppmv_ukp += ppmv;
                mpc = mpc * tecaj;
                vpc = vpc * tecaj;

                ukupnoMpc = Math.Round(mpc * kolicina, 3);
                ukupnoMpcRacun += ukupnoMpc;

                ukupnoVpc = Math.Round(vpc * kolicina, 4);
                ukupnoVpcRacun += ukupnoVpc;

                ukupnoMpcRabat = Math.Round(Convert.ToDouble(dg(i, "iznos_ukupno")) * tecaj, 3);
                ukupnoMpcRabatRacun += ukupnoMpcRabat;
                ukupnoRabat = ukupnoMpc - ukupnoMpcRabat;
                mpcRabat = Math.Round(ukupnoMpcRabat / kolicina, 2);
                rabatIzn = Math.Round(mpc - mpcRabat, 3);
                ukupnoRabatRacun += ukupnoRabat;

                ukupnoOsnovica = Math.Round((ukupnoMpcRabat - ukupnoPovratnaNaknada) / (1 + porez / 100), 3);
                ukupnoOsnovicaRacun += ukupnoOsnovica;
                ukupnoPorez = Math.Round(ukupnoMpcRabat - ukupnoPovratnaNaknada - ukupnoOsnovica, 3);
                ukupnoPorezRacun += ukupnoPorez;

                if (dgw.Rows[i].Cells["id_stavka"].Value != null)
                {
                    DataRow[] dataROW = DSFS.Select("id_stavka = " + dgw.Rows[i].Cells["id_stavka"].Value.ToString());

                    if (chbOduzmiIzSkladista.Checked)
                    {
                        if (dg(i, "oduzmi") == "DA")
                        {
                            if (dgw.Rows[i].Cells[3].Value.ToString() == dataROW[0]["id_skladiste"].ToString())
                            {
                                kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), dgw.Rows[i].Cells[3].Value.ToString(), (Convert.ToDouble(dataROW[0]["kolicina"].ToString()) - Convert.ToDouble(dg(i, "kolicina"))).ToString(), "1", "+");
                                SQL.SQLroba_prodaja.UpdateRows(dgw.Rows[i].Cells[3].Value.ToString(), kol, dg(i, "sifra"));
                            }
                            else
                            {
                                //vraća na staro skladiste
                                kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), dataROW[0]["id_skladiste"].ToString(), dataROW[0]["kolicina"].ToString(), "1", "+");
                                SQL.SQLroba_prodaja.UpdateRows(dataROW[0]["id_skladiste"].ToString(), kol, dg(i, "sifra"));

                                //oduzima sa novog skladiste
                                kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), dgw.Rows[i].Cells[3].Value.ToString(), dg(i, "kolicina"), "1", "-");
                                SQL.SQLroba_prodaja.UpdateRows(dgw.Rows[i].Cells[3].Value.ToString(), kol, dg(i, "sifra"));
                            }
                        }
                    }

                    row1 = DTsend1.NewRow();
                    row1["kolicina"] = dg(i, "kolicina");
                    row1["vpc"] = Math.Round(vpc, 4).ToString("#0.0000");
                    row1["nbc"] = dg(i, "nc");
                    row1["broj_fakture"] = ttxBrojFakture.Text;
                    row1["porez"] = dg(i, "porez");
                    row1["sifra"] = ReturnSifra(dg(i, "sifra"));
                    row1["rabat"] = dg(i, "rabat");
                    row1["naziv"] = dg(i, "naziv");
                    row1["id_stavka"] = dg(i, "id_stavka");
                    row1["oduzmi"] = dg(i, "oduzmi");
                    row1["id_skladiste"] = dgw.Rows[i].Cells[3].Value;
                    row1["povratna_naknada"] = dg(i, "povratna_naknada");
                    row1["povratna_naknada_izn"] = povratnaNaknada.ToString("#0.00").Replace(".", ",");
                    row1["rabat_izn"] = rabatIzn.ToString("#0.00").Replace(".", ",");
                    row1["mpc_rabat"] = mpcRabat.ToString("#0.00").Replace(".", ",");
                    row1["ukupno_rabat"] = ukupnoRabat.ToString("#0.00").Replace(".", ",");
                    row1["ukupno_vpc"] = ukupnoVpc.ToString("#0.0000").Replace(".", ",");
                    row1["ukupno_mpc"] = ukupnoMpc.ToString("#0.00").Replace(".", ",");
                    row1["id_kasa"] = id_kasa;
                    row1["id_ducan"] = id_ducan;
                    row1["ukupno_mpc_rabat"] = ukupnoMpcRabat.ToString("#0.00").Replace(".", ",");
                    row1["ukupno_porez"] = ukupnoPorez.ToString("#0.00").Replace(".", ",");
                    row1["ukupno_osnovica"] = ukupnoOsnovica.ToString("#0.00").Replace(".", ",");
                    row1["ppmv"] = ppmv.ToString("#0.00").Replace(".", ",");
                    DTsend1.Rows.Add(row1);
                }
                else
                {
                    if (chbOduzmiIzSkladista.Checked)
                    {
                        if (dg(i, "oduzmi") == "DA")
                        {
                            kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), dgw.Rows[i].Cells[3].Value.ToString(), dg(i, "kolicina"), "1", "-");
                            SQL.SQLroba_prodaja.UpdateRows(dgw.Rows[i].Cells[3].Value.ToString(), kol, dg(i, "sifra"));
                        }
                    }

                    row = DTsend.NewRow();
                    row["kolicina"] = dg(i, "kolicina");
                    row["vpc"] = Math.Round(vpc, 2).ToString("#0.000");
                    row["nbc"] = dg(i, "nc");
                    row["broj_fakture"] = ttxBrojFakture.Text;
                    row["porez"] = dg(i, "porez");
                    row["sifra"] = ReturnSifra(dg(i, "sifra"));
                    row["rabat"] = dg(i, "rabat");
                    row["naziv"] = dg(i, "naziv");
                    row["oduzmi"] = dg(i, "oduzmi");
                    row["id_skladiste"] = dgw.Rows[i].Cells[3].Value;
                    row["povratna_naknada"] = dg(i, "povratna_naknada");
                    row["povratna_naknada_izn"] = povratnaNaknada.ToString("#0.00").Replace(".", ",");
                    row["rabat_izn"] = rabatIzn.ToString("#0.00").Replace(".", ",");
                    row["mpc_rabat"] = mpcRabat.ToString("#0.00").Replace(".", ",");
                    row["ukupno_rabat"] = ukupnoRabat.ToString("#0.00").Replace(".", ",");
                    row["ukupno_vpc"] = ukupnoVpc.ToString("#0.0000").Replace(".", ",");
                    row["id_kasa"] = id_kasa;
                    row["id_ducan"] = id_ducan;
                    row["ukupno_mpc"] = ukupnoMpc.ToString("#0.00").Replace(".", ",");
                    row["ukupno_mpc_rabat"] = ukupnoMpcRabat.ToString("#0.00").Replace(".", ",");
                    row["ukupno_porez"] = ukupnoPorez.ToString("#0.00").Replace(".", ",");
                    row["ukupno_osnovica"] = ukupnoOsnovica.ToString("#0.00").Replace(".", ",");
                    row["ppmv"] = ppmv.ToString("#0.00").Replace(".", ",");
                    DTsend.Rows.Add(row);
                }
            }

            string PovratnaNaknadaUkRacun, UkupnoMpcRacun, UkupnoVpcRacun,
                UkupnoMpcRabatRacun, UkupnoRabatRacun, UkupnoOsnovicaRacun, UkupnoPorezRacun;

            if (classSQL.remoteConnectionString == "")
            {
                PovratnaNaknadaUkRacun = povratnaNaknadaUkRacun.ToString("#0.00").Replace(",", ".");
                UkupnoMpcRacun = ukupnoMpcRacun.ToString("#0.00").Replace(",", ".");
                UkupnoVpcRacun = ukupnoVpcRacun.ToString("#0.0000").Replace(",", ".");
                UkupnoMpcRabatRacun = ukupnoMpcRabatRacun.ToString("#0.00").Replace(",", ".");
                UkupnoRabatRacun = ukupnoRabatRacun.ToString("#0.00").Replace(",", ".");
                UkupnoOsnovicaRacun = ukupnoOsnovicaRacun.ToString("#0.00").Replace(",", ".");
                UkupnoPorezRacun = ukupnoPorezRacun.ToString("#0.00").Replace(",", ".");
            }
            else
            {
                PovratnaNaknadaUkRacun = povratnaNaknadaUkRacun.ToString("#0.00").Replace(",", ".");
                UkupnoMpcRacun = ukupnoMpcRacun.ToString("#0.00").Replace(",", ".");
                UkupnoVpcRacun = ukupnoVpcRacun.ToString("#0.0000").Replace(",", ".");
                UkupnoMpcRabatRacun = ukupnoMpcRabatRacun.ToString("#0.00").Replace(",", ".");
                UkupnoRabatRacun = ukupnoRabatRacun.ToString("#0.00").Replace(",", ".");
                UkupnoOsnovicaRacun = ukupnoOsnovicaRacun.ToString("#0.00").Replace(",", ".");
                UkupnoPorezRacun = ukupnoPorezRacun.ToString("#0.00").Replace(",", ".");
            }

            string uk = Math.Round(u * tecaj, 3).ToString();
            if (classSQL.remoteConnectionString == "")
            {
                uk = uk.Replace(",", ".");
            }
            else
            {
                uk = uk.Replace(".", ",");
            }

            DateTime datum = DateTime.Now;
            if (DateTime.Now.ToString("yyyy-MM-dd") != dtpDatum.Value.ToString("yyyy-MM-dd"))
            {
                datum = dtpDatum.Value;
            }
            string stavke_u_valuti = "0";
            if (cbStavkeValuta.Checked) { stavke_u_valuti = "1"; }

            string komercijalista = Properties.Settings.Default.id_zaposlenik;
            if (cbKomercijalist.SelectedValue != null)
                komercijalista = cbKomercijalist.SelectedValue.ToString();

            bool kreiraniZapisnik = false;
            if (!Class.Dokumenti.isKasica && Class.Postavke.automatski_zapisnik)
            {
                Class.ZapisnikPromjeneCijene _zapisnik = new Class.ZapisnikPromjeneCijene(dtZapisnikStavke, false, datum.AddSeconds(-1), "Kreiranje zapisnika zbog promjene cijena na fakturi", ttxBrojFakture.Text + "/" + Util.Korisno.nazivPoslovnica + "/" + Class.Postavke.naplatni_uredaj_faktura, kreiranZapisnikZaFakturu);
                kreiraniZapisnik = _zapisnik.kreiraniZapisnik;
                _zapisnik = null;
            }

            string[] podaciAvio = (string[])lblAvioPodaci.Tag;

            string sql = "UPDATE fakture SET" +
                " id_odrediste= '" + txtSifraOdrediste.Text + "'," +
                " id_fakturirati='" + txtSifraFakturirati.Text + "'," +
                " date='" + datum.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " dateDVO='" + dtpDatumDVO.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " datum_valute='" + dtpDanaValuta.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " id_izjava='" + cbIzjava.SelectedValue + "'," +
                " id_zaposlenik='" + komercijalista + "'," +
                " id_zaposlenik_izradio='" + Properties.Settings.Default.id_zaposlenik + "'," +
                " model= '" + txtModel.Text + "'," +
                " id_nacin_placanja='" + cbNacinPlacanja.SelectedValue + "'," +
                " zr='" + cbZiroRacun.SelectedValue + "'," +
                " id_valuta='" + cbValuta.SelectedValue + "'," +
                " tecaj='" + txtTecaj.Text.Replace(",", ".") + "'," +
                " otprema='" + cbOtprema.SelectedValue + "'," +
                " id_predujam='0'," +
                " napomena='" + rtbNapomena.Text + "'," +
                " id_vd='" + cbVD.SelectedValue.ToString() + "'," +
                " godina_predujma='" + nuGodinaPredujma.Value.ToString() + "'," +
                " godina_ponude='" + nuGodinaPonude.Value.ToString() + "'," +
                " godina_fakture='" + nmGodinaFakture.Value.ToString() + "'," +
                " ukupno='" + uk + "'," +
                " ukupno_rabat='" + UkupnoRabatRacun + "'," +
                " ukupno_porez='" + UkupnoPorezRacun + "'," +
                " ukupno_osnovica='" + UkupnoOsnovicaRacun + "'," +
                " ukupno_mpc='" + UkupnoMpcRacun + "'," +
                " stavke_u_valuti='" + stavke_u_valuti + "'," +
                " ukupno_vpc='" + UkupnoVpcRacun + "'," +
                " ukupno_mpc_rabat='" + UkupnoMpcRabatRacun + "'," +
                " use_nbc = '" + Class.Postavke.proizvodnjaFakturaNbc + "'," +
                " editirano = '1', " +
                " id_kasa='" + id_kasa + "'," +
                " id_ducan='" + id_ducan + "'," +
                " faktura_za_komisiju='" + (chbFakturaZaKomisiju.Checked ? 1 : 0) + "'," +
                " avio_registracija = '" + (lblAvioPodaci.Tag != null ? podaciAvio[0] : "") + "'," +
                " avio_tip_zrakoplova = '" + (lblAvioPodaci.Tag != null ? podaciAvio[1] : "") + "'," +
                " avio_maks_tezina_polijetanja = '" + (lblAvioPodaci.Tag != null ? podaciAvio[2] : "0") + "'," +
                " ukupno_povratna_naknada='" + PovratnaNaknadaUkRacun + "'," +
                " partner_poslovnica = '" + cmbPoslovnicaPartner.SelectedValue + "'," +
                " pouzece = '" + (chbPouzece.Checked ? 1 : 0) + "'" +
                " WHERE  broj_fakture='" + ttxBrojFakture.Text + "' AND id_ducan='" + id_ducan + "' AND id_kasa='" + id_kasa + "'";

            provjera_sql(classSQL.update(sql));

            DataSet DSkolicina = new DataSet();

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                string barcode = "000" + ttxBrojFakture.Text;
                if (DTpromocije1.Rows.Count > 0 && DTpromocije1.Rows[0]["aktivnost"].ToString() == "DA")
                {
                    if (classSQL.remoteConnectionString == "")
                    {
                        uk = uk.Replace(",", ".");
                    }
                    else
                    {
                        uk = uk.Replace(".", ",");
                    }

                    provjera_sql(classSQL.update("UPDATE racun_popust_kod_sljedece_kupnje SET " +
                        "datum='" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        "ukupno='" + Math.Round(SveUkupno, 2).ToString("#0.00") + "'," +
                        "popust='" + DTpromocije1.Rows[0]["popust"].ToString() + "'," +
                        "koristeno='NE'" +
                        " WHERE dokumenat='FA' AND broj_racuna='" + ttxBrojFakture.Text + "'"));
                }
            }

            provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Nova faktura br." + ttxBrojFakture.Text + "')"));
            if (chbOduzmiIzSkladista.Checked)
                Util.AktivnostZaposlenika.SpremiAktivnost(dgw, null, "Faktura", ttxBrojFakture.Text, true);
            provjera_sql(SQL.SQLfaktura.InsertStavke(DTsend, true));
            provjera_sql(SQL.SQLfaktura.UpdateStavke(DTsend1, true));

            DTsend.Merge(DTsend1, true);


            if (Class.Postavke.fiskalizacija_faktura_prikazi_obavijest)
            {
                if (MessageBox.Show("Fiskalizirati fakturu?", "Fiskalizacija?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int brFak = Convert.ToInt32(ttxBrojFakture.Text);
                    FiskalizirajFakturu(DTsend1, datum, brFak);
                }
            }

            ttxBrojFakture.ReadOnly = false;
            nmGodinaFakture.ReadOnly = false;
            edit = false;

            if (!Class.Dokumenti.isKasica && kreiraniZapisnik)
            {
                MessageBox.Show("Kreiran je automatski zapisnik zbog promjene cijene na fakturi");
            }
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmSvePonude sp = new frmSvePonude();
            sp.btnUrediSifru.Enabled = false;
            sp.ShowDialog();
        }

        /// <summary>
        /// Method used to calculate values after adding an item (stavka)
        /// </summary>
        private void SetRoba()
        {
            dgw.Rows.Add();
            int br = dgw.Rows.Count - 1;
            dgw.Select();

            dgw.Rows[br].Cells[0].Value = dgw.Rows.Count.ToString();
            dgw.Rows[br].Cells["sifra"].Value = DTRoba.Rows[0]["sifra"].ToString();
            dgw.Rows[br].Cells["naziv"].Value = DTRoba.Rows[0]["naziv"].ToString();
            dgw.Rows[br].Cells["jmj"].Value = DTRoba.Rows[0]["jm"].ToString();

            decimal tecaj = 1;
            decimal vpc, ppmv = getPPMV(DTRoba.Rows[0]["sifra"].ToString(), Class.Postavke.id_default_skladiste);
            decimal pdv;
            decimal povratna_naknada = 0;

            if (cbStavkeValuta.Checked)
                decimal.TryParse(txtTecaj.Text, out tecaj);

            decimal.TryParse(DTRoba.Rows[0]["vpc"].ToString(), out vpc);
            decimal.TryParse(DTRoba.Rows[0]["porez"].ToString(), out pdv);

            if (DTRoba.Columns.Contains("povratna_naknada") && decimal.TryParse(DTRoba.Rows[0]["povratna_naknada"].ToString(), out povratna_naknada))
            {
                vpc = vpc - povratna_naknada;
            }

            if (cbStavkeValuta.Checked)
            {
                vpc = vpc / tecaj;
            }

            dgw.Rows[br].Cells["mpc"].Value = (vpc + (vpc * pdv / 100)) + ppmv;

            dgw.Rows[br].Cells["cijena_bez_pdva"].Value = Math.Round(vpc, 4).ToString("#0.000");

            dgw.Rows[br].Cells["kolicina"].Value = "1";

            if (chbObracunPoreza.Checked)
            {
                dgw.Rows[br].Cells["porez"].Value = DTRoba.Rows[0]["porez"].ToString();
            }
            else
            {
                dgw.Rows[br].Cells["porez"].Value = 0;
            }

            decimal _NBC = Util.Korisno.VratiNabavnuCijenu(DTRoba.Rows[0]["sifra"].ToString(), Class.Postavke.id_default_skladiste.ToString());

            dgw.Rows[br].Cells["oduzmi"].Value = DTRoba.Rows[0]["oduzmi"].ToString();
            dgw.Rows[br].Cells["rabat"].Value = "0,00";
            dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
            dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
            dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
            dgw.Rows[br].Cells["vpc"].Value = Math.Round(vpc, 4).ToString("#0.000");
            dgw.Rows[br].Cells["nc"].Value = _NBC.ToString("#0.000");
            //dgw.Rows[br].Cells["skladiste"].Value = Class.Postavke.id_default_skladiste.ToString();

            if (idSkladiste != 0)
            {
                dgw.Rows[br].Cells["skladiste"].Value = idSkladiste.ToString();
            }
            else
            {
                dgw.Rows[br].Cells["skladiste"].Value = Class.Postavke.id_default_skladiste.ToString();

            }
            dgw.Rows[br].Cells["ppmv"].Value = ppmv.ToString();

            PartnerPostaviPopust();

            dgw.CurrentCell = dgw.Rows[br].Cells[3];
            dgw.BeginEdit(true);
            izracun(true);
        }

        private void ttxBrojFakture_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT broj_fakture,zki FROM fakture WHERE godina_fakture='" + nmGodinaFakture.Value.ToString() + "' AND broj_fakture='" + ttxBrojFakture.Text + "' AND id_ducan='" + id_ducan + "' AND id_kasa='" + id_kasa + "'", "fakture").Tables[0];

                if (DT.Rows.Count == 0)
                {
                    if (brojFakture() == ttxBrojFakture.Text.Trim())
                    {
                        deleteFields(ttxBrojFakture.Text.Trim());
                        edit = false;
                        EnableDisable(true);
                        btnSveFakture.Enabled = false;
                        //ttxBrojFakture.Text = brojFakture();
                        btnDeleteAllFaktura.Enabled = false;
                        txtSifraOdrediste.Select();
                        ttxBrojFakture.ReadOnly = true;
                        nmGodinaFakture.ReadOnly = true;
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    if (DT.Rows[0]["zki"].ToString().Length > 1)
                    {
                        MessageBox.Show("Nije moguće mijenjati ovu fakturu! Fiskalizirana!");
                        ControlDisableEnable(1, 0, 0, 1, 0);
                        return;
                    }

                    broj_fakture_edit = ttxBrojFakture.Text;

                    deleteFields(broj_fakture_edit);
                    fillFaktute();
                    EnableDisable(true);
                    edit = true;
                    btnDeleteAllFaktura.Enabled = true;
                    txtSifraOdrediste.Select();
                    ttxBrojFakture.ReadOnly = true;
                    nmGodinaFakture.ReadOnly = true;
                }

                ControlDisableEnable(0, 1, 1, 0, 1);
            }
        }

        private void txtBrojPonude_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT broj_ponude FROM ponude WHERE godina_ponude='" + nuGodinaPonude.Value.ToString() + "' AND broj_ponude='" + txtBrojPonude.Text + "'", "ponude").Tables[0];
                deleteFields();
                if (DT.Rows.Count == 0)
                {
                    MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                }
                else if (DT.Rows.Count == 1)
                {
                    EnableDisable(true);
                    edit = true;
                    btnDeleteAllFaktura.Enabled = true;
                    fillPonude(DT.Rows[0][0].ToString());
                    getPoslovnicaPartner();
                }
            }
        }

        private void fillPonude(string broj)
        {
            //fill header
            DataTable DTotpremnice = classSQL.select("SELECT * FROM ponude WHERE broj_ponude = '" + broj + "'", "ponude").Tables[0];

            txtSifraOdrediste.Text = DTotpremnice.Rows[0]["id_odrediste"].ToString();
            try { txtPartnerNaziv.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["id_odrediste"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); } catch (Exception) { }
            txtSifraFakturirati.Text = DTotpremnice.Rows[0]["id_fakturirati"].ToString();
            try { txtPartnerNaziv1.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["id_fakturirati"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); } catch (Exception) { }
            cbZiroRacun.SelectedValue = DTotpremnice.Rows[0]["zr"].ToString();
            cbNacinPlacanja.SelectedValue = DTotpremnice.Rows[0]["id_nacin_placanja"].ToString();
            cbValuta.SelectedValue = DTotpremnice.Rows[0]["id_valuta"].ToString();
            cbOtprema.SelectedValue = DTotpremnice.Rows[0]["otprema"].ToString();

            //--------fill otpremnica stavke----------------------------
            DataTable dtR = new DataTable();
            DSFS = classSQL.select("SELECT * FROM ponude_stavke WHERE broj_ponude = '" + DTotpremnice.Rows[0]["broj_ponude"].ToString() + "'", "ponude_stavke").Tables[0];

            for (int i = 0; i < DSFS.Rows.Count; i++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;
                string s = "";

                DataTable tblRoba = classSQL.select("SELECT oduzmi FROM roba WHERE sifra='" + DSFS.Rows[i]["sifra"].ToString().Trim() + "'", "roba").Tables[0];

                if (tblRoba.Rows.Count > 0 && tblRoba.Rows[0]["oduzmi"].ToString() == "DA")
                {
                    s = "SELECT roba_prodaja.nc,roba.naziv,roba.jm,roba_prodaja.sifra,roba.oduzmi FROM roba_prodaja INNER JOIN roba ON roba_prodaja.sifra=roba.sifra WHERE roba_prodaja.sifra='" + DSFS.Rows[i]["sifra"].ToString() + "' AND roba_prodaja.id_skladiste='" + DSFS.Rows[i]["id_skladiste"].ToString() + "'";
                }
                else
                {
                    s = "SELECT roba.nc,roba.naziv,roba.jm,roba.sifra,roba.oduzmi FROM roba WHERE sifra='" + DSFS.Rows[i]["sifra"].ToString() + "'";
                }

                dtR = classSQL.select(s, "roba_prodaja").Tables[0];

                if (dtR.Rows.Count == 0)
                {
                    MessageBox.Show("Greška, nemate šifre u šifrarniku iz ove ponude.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

                dgw.Rows[br].Cells[0].Value = i + 1;
                dgw.Rows[br].Cells["sifra"].Value = dtR.Rows[0]["sifra"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = dtR.Rows[0]["naziv"].ToString();
                dgw.Rows[br].Cells["jmj"].Value = dtR.Rows[0]["jm"].ToString();
                dgw.Rows[br].Cells["oduzmi"].Value = dtR.Rows[0]["oduzmi"].ToString();
                dgw.Rows[br].Cells["cijena_bez_pdva"].Value = string.Format("{0:0.000}", DSFS.Rows[i]["vpc"]);
                dgw.Rows[br].Cells["kolicina"].Value = DSFS.Rows[i]["kolicina"].ToString();
                dgw.Rows[br].Cells["porez"].Value = DSFS.Rows[i]["porez"].ToString();
                dgw.Rows[br].Cells["rabat"].Value = DSFS.Rows[i]["rabat"].ToString();
                dgw.Rows[br].Cells["skladiste"].Value = DSFS.Rows[i]["id_skladiste"].ToString();
                dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
                dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
                dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
                dgw.Rows[br].Cells["vpc"].Value = string.Format("{0:0.000}", DSFS.Rows[i]["vpc"]);
                //dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["mpc"]);
                dgw.Rows[br].Cells["nc"].Value = string.Format("{0:0.00}", dtR.Rows[0]["nc"].ToString());
                //dgw.Rows[br].Cells["id_stavka"].Value = DSFS.Rows[i]["id_stavka"].ToString();

                dgw.Select();
                dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[3];
                ControlDisableEnable(0, 1, 1, 0, 1);
                edit = false;

                //povratna naknada mora biti poslije izračuna jer bi se inače upisivala trenutna povratna naknada
                //a ne povratna naknada upisana u fakturi
                try
                {
                    dgw.Rows[br].Cells["povratna_naknada"].Value = DSFS.Rows[i]["povratna_naknada"].ToString();
                }
                catch
                {
                    //uzeti iz baze
                    DataTable DTpovrNaknada = classSQL.select("SELECT iznos FROM povratna_naknada WHERE sifra='" + DSFS.Rows[i]["sifra"].ToString().Trim() + "'", "roba").Tables[0];
                    dgw.Rows[br].Cells["povratna_naknada"].Value = DTpovrNaknada.Rows.Count > 0 ? DTpovrNaknada.Rows[0]["iznos"].ToString() : "0,00";
                }

                izracun(false);
            }
        }

        private DataTable DTnaplaceneotpremnice = new DataTable();

        private void fillOtpremnice(string broj)
        {
            //fill header

            //--------fill otpremnica stavke----------------------------
            DataTable DTotpremnice = new DataTable();
            DataTable DTotpremnica_stavke = new DataTable();
            DataTable DTskl = classSQL.select("SELECT * FROM skladiste ", "skl").Tables[0];
            if (DTnaplaceneotpremnice.Columns.Contains("id") != true)
            {
                DTnaplaceneotpremnice.Columns.Add("id");
                DTnaplaceneotpremnice.Columns.Add("id_otpremnica");
            }

            DataRow row;
            DataTable dtR = new DataTable();
            //DSFS = classSQL.select("SELECT * FROM otpremnica_stavke WHERE broj_ponude = '" + DTotpremnice.Rows[0]["broj_ponude"].ToString() + "'", "ponude_stavke").Tables[0];
            bool header_1x = true;
            for (int i = 0; i < DTskl.Rows.Count; i++)
            {
                string sql = "SELECT * FROM otpremnice  WHERE osoba_partner = '" + broj + "' AND id_skladiste = '" + DTskl.Rows[i]["id_skladiste"].ToString() + "' and datum >= '" + dtpOtpremniceOd.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' and datum <= '" + dtpOtpremniceDo.Value.ToString("yyyy-MM-dd HH:mm:ss") + "';";

                DTotpremnice = classSQL.select(sql, "otpremnice").Tables[0];

                if (DTotpremnice.Rows.Count != 0)
                {
                    for (int z = 0; z < DTotpremnice.Rows.Count; z++)
                    {
                        row = DTnaplaceneotpremnice.NewRow();
                        row["id"] = DTnaplaceneotpremnice.Rows.Count.ToString();
                        row["id_otpremnica"] = DTotpremnice.Rows[z]["id_otpremnica"];
                        DTnaplaceneotpremnice.Rows.Add(row);

                        //dtR = classSQL.select(s, "roba_prodaja").Tables[0];
                        try
                        {
                            DTotpremnica_stavke = classSQL.select("SELECT * FROM otpremnica_stavke  WHERE id_otpremnice = '" + DTotpremnice.Rows[z]["id_otpremnica"].ToString() + "' AND naplaceno_fakturom <> 'TRUE'", "otpremnice stavke").Tables[0];
                            for (int x = 0; x < DTotpremnica_stavke.Rows.Count; x++)
                            {
                                dgw.Rows.Add();
                                int br = dgw.Rows.Count - 1;
                                DataTable roba = classSQL.select("Select * from roba where sifra = '" + DTotpremnica_stavke.Rows[x]["sifra_robe"].ToString() + "'", "roba").Tables[0];
                                dgw.Rows[br].Cells[0].Value = dgw.Rows.Count;
                                dgw.Rows[br].Cells["sifra"].Value = DTotpremnica_stavke.Rows[x]["sifra_robe"].ToString();
                                dgw.Rows[br].Cells["naziv"].Value = roba.Rows[0]["naziv"].ToString();
                                dgw.Rows[br].Cells["jmj"].Value = roba.Rows[0]["jm"].ToString();
                                dgw.Rows[br].Cells["oduzmi"].Value = roba.Rows[0]["oduzmi"].ToString();
                                dgw.Rows[br].Cells["cijena_bez_pdva"].Value = string.Format("{0:0.000}", DTotpremnica_stavke.Rows[x]["vpc"]);
                                dgw.Rows[br].Cells["kolicina"].Value = DTotpremnica_stavke.Rows[x]["kolicina"].ToString();
                                dgw.Rows[br].Cells["porez"].Value = DTotpremnica_stavke.Rows[x]["porez"].ToString();
                                dgw.Rows[br].Cells["rabat"].Value = DTotpremnica_stavke.Rows[x]["rabat"].ToString();
                                dgw.Rows[br].Cells["skladiste"].Value = DTotpremnica_stavke.Rows[x]["id_skladiste"].ToString();
                                dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
                                dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
                                dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
                                dgw.Rows[br].Cells["vpc"].Value = string.Format("{0:0.000}", DTotpremnica_stavke.Rows[x]["vpc"]);
                                //dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["mpc"]);
                                dgw.Rows[br].Cells["nc"].Value = string.Format("{0:0.00}", DTotpremnica_stavke.Rows[x]["nbc"].ToString());
                                dgw.Rows[br].Cells["id_stavka"].Value = DTotpremnica_stavke.Rows[x]["id_stavka"].ToString();

                                dgw.Select();
                                dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[3];
                                ControlDisableEnable(0, 1, 1, 0, 1);
                                edit = false;

                                //povratna naknada mora biti poslije izračuna jer bi se inače upisivala trenutna povratna naknada
                                //a ne povratna naknada upisana u fakturi
                                try
                                {
                                    if (DTotpremnica_stavke.Columns.Contains("povratna_naknada"))
                                    {
                                        dgw.Rows[br].Cells["povratna_naknada"].Value = DTotpremnica_stavke.Rows[x]["povratna_naknada"].ToString();
                                    }
                                    else
                                    {
                                        dgw.Rows[br].Cells["povratna_naknada"].Value = "0.00";
                                    }
                                }
                                catch
                                {
                                    //uzeti iz baze
                                    //DataTable DTpovrNaknada = classSQL.select("SELECT iznos FROM povratna_naknada WHERE sifra='" + DSFS.Rows[i]["sifra"].ToString().Trim() + "'", "roba").Tables[0];
                                    //dgw.Rows[br].Cells["povratna_naknada"].Value = DTpovrNaknada.Rows.Count > 0 ? DTpovrNaknada.Rows[0]["iznos"].ToString() : "0,00";
                                    dgw.Rows[br].Cells["povratna_naknada"].Value = "0.00";
                                }

                                if (header_1x)
                                {
                                    txtSifraOdrediste.Text = DTotpremnice.Rows[0]["id_odrediste"].ToString();
                                    try { txtPartnerNaziv.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["id_odrediste"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); } catch (Exception) { }
                                    txtSifraFakturirati.Text = DTotpremnice.Rows[0]["osoba_partner"].ToString();
                                    try { txtPartnerNaziv1.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["osoba_partner"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); } catch (Exception) { }
                                    header_1x = false;
                                }
                                izracun(false);
                            }
                        }
                        catch { }
                    }
                }
            }
        }

        private void fillServis(string broj)
        {
            //fill header
            DataTable DTotpremnice = classSQL.select("SELECT * FROM radni_nalog_servis WHERE broj = '" + broj + "'", "radni_nalog_servis").Tables[0];

            txtSifraOdrediste.Text = DTotpremnice.Rows[0]["id_odrediste"].ToString();
            try { txtPartnerNaziv.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["id_odrediste"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); } catch (Exception) { }
            txtSifraFakturirati.Text = DTotpremnice.Rows[0]["id_fakturirati"].ToString();
            try { txtPartnerNaziv1.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["id_fakturirati"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); } catch (Exception) { }
            cbZiroRacun.SelectedValue = DTotpremnice.Rows[0]["zr"].ToString();
            cbNacinPlacanja.SelectedValue = DTotpremnice.Rows[0]["id_nacin_placanja"].ToString();
            cbValuta.SelectedValue = DTotpremnice.Rows[0]["id_valuta"].ToString();
            cbOtprema.SelectedValue = DTotpremnice.Rows[0]["otprema"].ToString();

            //--------fill otpremnica stavke----------------------------
            DataTable dtR = new DataTable();
            DSFS = classSQL.select("SELECT * FROM radni_nalog_servis_stavke WHERE broj = '" + DTotpremnice.Rows[0]["broj"].ToString() + "'", "radni_nalog_servis_stavke").Tables[0];

            for (int i = 0; i < DSFS.Rows.Count; i++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;
                string s = "";

                DataTable tblRoba = classSQL.select("SELECT oduzmi FROM roba WHERE sifra='" + DSFS.Rows[i]["sifra"].ToString().Trim() + "'", "roba").Tables[0];

                if (tblRoba.Rows[0]["oduzmi"].ToString() == "DA")
                {
                    s = "SELECT roba_prodaja.nc,roba.naziv,roba.jm,roba_prodaja.sifra,roba.oduzmi FROM roba_prodaja INNER JOIN roba ON roba_prodaja.sifra=roba.sifra WHERE roba_prodaja.sifra='" + DSFS.Rows[i]["sifra"].ToString() + "' AND roba_prodaja.id_skladiste='" + DSFS.Rows[i]["id_skladiste"].ToString() + "'";
                }
                else
                {
                    s = "SELECT roba.nc,roba.naziv,roba.jm,roba.sifra,roba.oduzmi FROM roba WHERE sifra='" + DSFS.Rows[i]["sifra"].ToString() + "'";
                }

                dtR = classSQL.select(s, "roba_prodaja").Tables[0];

                dgw.Rows[br].Cells[0].Value = i + 1;
                dgw.Rows[br].Cells["sifra"].Value = dtR.Rows[0]["sifra"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = dtR.Rows[0]["naziv"].ToString();
                dgw.Rows[br].Cells["jmj"].Value = dtR.Rows[0]["jm"].ToString();
                dgw.Rows[br].Cells["oduzmi"].Value = dtR.Rows[0]["oduzmi"].ToString();
                dgw.Rows[br].Cells["cijena_bez_pdva"].Value = string.Format("{0:0.000}", DSFS.Rows[i]["vpc"]);
                dgw.Rows[br].Cells["kolicina"].Value = DSFS.Rows[i]["kolicina"].ToString();
                dgw.Rows[br].Cells["porez"].Value = DSFS.Rows[i]["porez"].ToString();
                dgw.Rows[br].Cells["rabat"].Value = DSFS.Rows[i]["rabat"].ToString();
                dgw.Rows[br].Cells["skladiste"].Value = DSFS.Rows[i]["id_skladiste"].ToString();
                dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
                dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
                dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
                dgw.Rows[br].Cells["vpc"].Value = string.Format("{0:0.000}", DSFS.Rows[i]["vpc"]);
                //dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["mpc"]);
                dgw.Rows[br].Cells["nc"].Value = string.Format("{0:0.00}", dtR.Rows[0]["nc"].ToString());
                //dgw.Rows[br].Cells["id_stavka"].Value = DSFS.Rows[i]["id_stavka"].ToString();

                dgw.Select();
                dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[3];
                ControlDisableEnable(0, 1, 1, 0, 1);
                edit = false;

                //povratna naknada mora biti poslije izračuna jer bi se inače upisivala trenutna povratna naknada
                //a ne povratna naknada upisana u fakturi
                try
                {
                    dgw.Rows[br].Cells["povratna_naknada"].Value = DSFS.Rows[i]["povratna_naknada"].ToString();
                }
                catch
                {
                    //uzeti iz baze
                    DataTable DTpovrNaknada = classSQL.select("SELECT iznos FROM povratna_naknada WHERE sifra='" + DSFS.Rows[i]["sifra"].ToString().Trim() + "'", "roba").Tables[0];
                    dgw.Rows[br].Cells["povratna_naknada"].Value = DTpovrNaknada.Rows.Count > 0 ? DTpovrNaknada.Rows[0]["iznos"].ToString() : "0,00";
                }

                izracun(false);
            }
        }

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.CurrentCell.ColumnIndex == 3 & dgw.CurrentRow.Cells["oduzmi"].FormattedValue.ToString() == "DA")
            {
                if (Util.Korisno.ProvijeriIUpozoriAkoNemaNaSkladistu(dgw.CurrentRow.Cells["sifra"].FormattedValue.ToString(), dgw.CurrentRow.Cells["skladiste"].Value.ToString(), DTpostavke))
                {
                    SetCijenaSkladiste();
                    dgw.Rows[e.RowIndex].Cells["dozvoli_prodaju"].Value = true;
                }
                else
                {
                    if (Class.PodaciTvrtka.oibTvrtke == Class.Postavke.OIB_PC1)
                    {
                        //dgw.BeginEdit(false);
                        //dgw.Rows.RemoveAt(e.RowIndex);
                        dgw.Rows[e.RowIndex].Cells["dozvoli_prodaju"].Value = false;
                        //return;
                    }
                }
            }
            else if (dgw.CurrentCell.ColumnIndex == 9)
            {
                if (dgw.CurrentRow.Cells["skladiste"].Value == null & dgw.CurrentRow.Cells["oduzmi"].FormattedValue.ToString() == "DA")
                {
                    MessageBox.Show("Niste odabrali skladište", "Greška");
                    return;
                }

                dgw.CurrentCell.Selected = false;
                txtSifra_robe.Text = "";
                txtSifra_robe.BackColor = Color.Silver;
                txtSifra_robe.Select();
            }
            else if (dgw.CurrentCell.ColumnIndex == 7)
            {
                try
                {
                    double mpc = 0; // Convert.ToDouble(dgw.CurrentRow.Cells["mpc"].FormattedValue.ToString());
                    double porez = 0;// 1 + Convert.ToDouble(dgw.CurrentRow.Cells["porez"].FormattedValue.ToString()) / 100;

                    if (double.TryParse(dgw.CurrentRow.Cells["mpc"].FormattedValue.ToString(), out mpc) && double.TryParse(dgw.CurrentRow.Cells["porez"].FormattedValue.ToString(), out porez))
                    {
                        porez = 1 + (porez / 100);
                        dgw.CurrentRow.Cells["vpc"].Value = Math.Round(mpc / porez, 3);
                    }
                    else
                    {
                        MessageBox.Show("Koristite enter za sljedeću kolonu.");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Koristite enter za sljedeću kolonu.");
                }
            }
            else if (dgw.CurrentCell.ColumnIndex == 13)
            {
                double vpc = 0;
                if (!double.TryParse(dgw.CurrentRow.Cells["vpc"].FormattedValue.ToString(), out vpc))
                {
                    MessageBox.Show("Pogrešan iznos za vpc.");
                    return;
                }

                double porez = 1 + Convert.ToDouble(dgw.CurrentRow.Cells["porez"].FormattedValue.ToString()) / 100;
                dgw.CurrentRow.Cells["mpc"].Value = Math.Round(vpc * porez, 3);
            }

            if (!bKol)
                return;

            izracun(true);
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.Rows.Count < 1)
                return;
            dgw.BeginEdit(true);
        }

        private void cbOtprema_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                nuGodinaPredujma.Select();
            }
        }

        private void TRENUTNI_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox txt = ((TextBox)sender);
                txt.BackColor = Color.Khaki;
            }
            else if (sender is ComboBox)
            {
                ComboBox txt = ((ComboBox)sender);
                txt.BackColor = Color.Khaki;
            }
            else if (sender is DateTimePicker)
            {
                DateTimePicker control = ((DateTimePicker)sender);
                control.BackColor = Color.Khaki;
            }
            else if (sender is NumericUpDown)
            {
                NumericUpDown control = ((NumericUpDown)sender);
                control.BackColor = Color.Khaki;
            }
        }

        private void NAPUSTENI_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox txt = ((TextBox)sender);
                txt.BackColor = Color.White;
            }
            else if (sender is ComboBox)
            {
                ComboBox txt = ((ComboBox)sender);
                txt.BackColor = Color.White;
            }
            else if (sender is DateTimePicker)
            {
                DateTimePicker control = ((DateTimePicker)sender);
                control.BackColor = Color.White;
            }
            else if (sender is NumericUpDown)
            {
                NumericUpDown control = ((NumericUpDown)sender);
                control.BackColor = Color.White;
            }
        }

        private void txtBrojServisa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT broj FROM radni_nalog_servis" +
                    " WHERE godina='" + nuGodinaServisa.Value.ToString() + "' AND broj='" + txtBrojServisa.Text + "'",
                    "radni_nalog_servis").Tables[0];

                deleteFields();

                if (DT.Rows.Count == 0)
                {
                    MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                }
                else if (DT.Rows.Count == 1)
                {
                    EnableDisable(true);
                    edit = true;
                    btnDeleteAllFaktura.Enabled = true;
                    fillServis(DT.Rows[0][0].ToString());
                    getPoslovnicaPartner();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmSviRadniNaloziServis sp = new frmSviRadniNaloziServis();
            sp.btnUrediSifru.Enabled = false;
            sp.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataTable DTsend = new DataTable();
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("nbc");
            DTsend.Columns.Add("broj_fakture");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("sifra");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("oduzmi");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("povratna_naknada");
            DTsend.Columns.Add("ppmv");
            DataRow row;

            if (dgw.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku.", "Greška");
                return;
            }

            decimal dec_parse;
            if (Decimal.TryParse(txtSifraOdrediste.Text, out dec_parse))
            {
                txtSifraOdrediste.Text = dec_parse.ToString();
            }
            else
            {
                MessageBox.Show("Greška kod upisa odredišta.", "Greška");
                return;
            }

            if (Decimal.TryParse(txtSifraFakturirati.Text, out dec_parse))
            {
                txtSifraFakturirati.Text = dec_parse.ToString();
            }
            else
            {
                MessageBox.Show("Greška kod upisa šifre za fakturirati.", "Greška");
                return;
            }

            string uk = u.ToString();
            if (classSQL.remoteConnectionString == "")
            {
                uk = uk.Replace(",", ".");
            }
            else
            {
                uk = uk.Replace(".", ",");
            }

            string oduzmi_iz_skladista = "";
            if (chbOduzmiIzSkladista.Checked)
            {
                oduzmi_iz_skladista = "1";
            }
            else
            {
                oduzmi_iz_skladista = "0";
            }

            DateTime datum = dtpDatum.Value;

            string sql = "DELETE FROM ispis_faktura_stavke; DELETE FROM ispis_fakture;";
            provjera_sql(classSQL.update(sql));

            sql = "INSERT INTO ispis_fakture (broj_fakture,id_odrediste,id_fakturirati,date,dateDVO,datum_valute,id_izjava,id_zaposlenik,id_zaposlenik_izradio,model" +
                ",id_nacin_placanja,zr,id_valuta,otprema,id_predujam,napomena,id_vd, tecaj, godina_predujma,godina_ponude,godina_fakture,oduzmi_iz_skladista,ukupno,id_kasa,id_ducan) VALUES " +
                " (" +
                 " '" + ttxBrojFakture.Text + "'," +
                " '" + txtSifraOdrediste.Text + "'," +
                " '" + txtSifraFakturirati.Text + "'," +
                " '" + datum + "'," +
                " '" + dtpDatumDVO.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " '" + dtpDanaValuta.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " '" + cbIzjava.SelectedValue + "'," +
                " '" + cbKomercijalist.SelectedValue.ToString() + "'," +
                " '" + Properties.Settings.Default.id_zaposlenik + "'," +
                " '" + txtModel.Text + "'," +
                " '" + cbNacinPlacanja.SelectedValue.ToString() + "'," +
                " '" + cbZiroRacun.SelectedValue.ToString() + "'," +
                " '" + cbValuta.SelectedValue.ToString() + "'," +
                " '" + cbOtprema.SelectedValue + "'," +
                " '" + "0" + "'," +
                " '" + rtbNapomena.Text + "'," +
                " '" + cbVD.SelectedValue.ToString().Trim() + "'," +
                " '" + txtTecaj.Text.ToString().Replace(",", ".") + "'," +
                " '" + nuGodinaPredujma.Value.ToString() + "'," +
                " '" + nuGodinaPonude.Value.ToString() + "'," +
                " '" + nmGodinaFakture.Value.ToString() + "'," +
                " '" + oduzmi_iz_skladista + "'," +
                " '" + uk + "','" + id_kasa + "','" + id_ducan + "'" +
                ")";
            provjera_sql(classSQL.insert(sql));

            string sifra;
            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                sifra = dg(i, "sifra");

                row = DTsend.NewRow();
                row["kolicina"] = dg(i, "kolicina");
                row["vpc"] = dg(i, "vpc");
                row["nbc"] = dg(i, "nc");
                row["broj_fakture"] = ttxBrojFakture.Text;
                row["porez"] = dg(i, "porez");
                row["sifra"] = ReturnSifra(dg(i, "sifra"));
                row["rabat"] = dg(i, "rabat");
                row["oduzmi"] = dg(i, "oduzmi");
                row["id_skladiste"] = dgw.Rows[i].Cells[3].Value;
                row["povratna_naknada"] = dg(i, "povratna_naknada");
                row["ppmv"] = dg(i, "ppmv");

                DTsend.Rows.Add(row);

                sql = "INSERT INTO ispis_faktura_stavke (kolicina,vpc,nbc,porez,broj_fakture,rabat,id_skladiste,sifra,ppmv,povratna_naknada,oduzmi,id_kasa,id_ducan) VALUES " +
                    "(" +
                    "'" + DTsend.Rows[i]["kolicina"].ToString() + "'," +
                    "'" + DTsend.Rows[i]["vpc"].ToString().Replace(",", ".") + "'," +
                    "'" + DTsend.Rows[i]["nbc"].ToString() + "'," +
                    "'" + DTsend.Rows[i]["porez"].ToString() + "'," +
                    "'" + DTsend.Rows[i]["broj_fakture"].ToString() + "'," +
                    "'" + DTsend.Rows[i]["rabat"].ToString() + "'," +
                    "'" + DTsend.Rows[i]["id_skladiste"].ToString() + "'," +
                    "'" + DTsend.Rows[i]["sifra"].ToString() + "'," +
                    "'" + DTsend.Rows[i]["ppmv"].ToString().Replace(',', '.') + "'," +
                    "'" + DTsend.Rows[i]["povratna_naknada"].ToString().Replace(",", ".") + "'," +
                    "'" + DTsend.Rows[i]["oduzmi"].ToString() + "','" + id_kasa + "','" + id_ducan + "'" +
                    ")";

                provjera_sql(classSQL.insert(sql));
            }

            if (Class.Postavke.idFaktura == 1)
            {
                Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
                rfak.samoIspis = true;
                rfak.dokumenat = "FAK";
                rfak.racunajTecaj = ValutaKuna(cbValuta.Text);
                rfak.poslovnica = id_ducan;
                rfak.naplatni = id_kasa;
                rfak.ImeForme = "Fakture";
                rfak.broj_dokumenta = ttxBrojFakture.Text;
                rfak.ShowDialog();
            }
            else if (Class.Postavke.idFaktura == 2)
            {
                Report.Faktura.repFakturaNovo rfak = new Report.Faktura.repFakturaNovo();

                rfak.dokumenat = "FAK";
                rfak.racunajTecaj = ValutaKuna(cbValuta.Text);
                rfak.poslovnica = id_ducan;
                rfak.naplatni = id_kasa;
                rfak.ImeForme = "Fakture";
                rfak.broj_dokumenta = ttxBrojFakture.Text;
                rfak.ShowDialog();
            }
            else if (Class.Postavke.idFaktura == 3)
            {
                Report.Faktura3.repFaktura3 rfak = new Report.Faktura3.repFaktura3();
                rfak.dokumenat = "FAK";
                rfak.racunajTecaj = ValutaKuna(cbValuta.Text);
                rfak.poslovnica = id_ducan;
                rfak.naplatni = id_kasa;
                rfak.ImeForme = "Fakture";
                rfak.broj_dokumenta = ttxBrojFakture.Text;
                rfak.ShowDialog();
            }
        }

        /// <summary>
        /// Used to load reservation data
        /// </summary>
        private void LoadRezervacija()
        {
            NoviUnos();

            dgw.Rows.Add();
            int index = dgw.Rows.Count - 1;
            dgw.Select();

            // Soba
            DataTable DTsoba = Global.Database.GetSobe(DTrezervacija.Rows[0]["id_soba"].ToString());
            decimal.TryParse(DTsoba.Rows[0]["cijena_nocenja"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal cijenaNocenja);

            DateTime datumDolaska = DateTime.Parse(DTrezervacija.Rows[0]["datum_dolaska"].ToString());
            DateTime datumOdlaska = DateTime.Parse(DTrezervacija.Rows[0]["datum_odlaska"].ToString());
            int totalDays = (datumOdlaska - datumDolaska).Days+1; // +1 je zato jer se racuna i sam dan dolaska.
            decimal cijena = cijenaNocenja * totalDays;

            // Partner
            DataTable DTpartner = Global.Database.GetPartners(DTrezervacija.Rows[0]["id_partner"].ToString());
            txtSifraOdrediste.Text = DTpartner.Rows[0]["id_partner"].ToString();
            txtPartnerNaziv.Text = DTpartner.Rows[0]["ime_tvrtke"].ToString();
            txtSifraFakturirati.Text = DTpartner.Rows[0]["id_partner"].ToString();
            txtPartnerNaziv1.Text = DTpartner.Rows[0]["ime_tvrtke"].ToString();

            dgw.Rows[index].Cells[0].Value = dgw.Rows.Count.ToString();
            dgw.Rows[index].Cells["sifra"].Value = "-";
            dgw.Rows[index].Cells["naziv"].Value = DTsoba?.Rows[0]["naziv_sobe"].ToString()
                                            + " | " + DTpartner?.Rows[0]["ime_tvrtke"].ToString()
                                            + " | " + $"Dolazak: {datumDolaska.ToString("dd.MM.yyyy.")} - Odlazak: {datumOdlaska.ToString("dd.MM.yyyy.")}";
            dgw.Rows[index].Cells["skladiste"].Value = Class.Postavke.id_default_skladiste.ToString();
            dgw.Rows[index].Cells["jmj"].Value = "-";
            dgw.Rows[index].Cells["kolicina"].Value = "1,00";
            dgw.Rows[index].Cells["porez"].Value = "13,00";
            dgw.Rows[index].Cells["mpc"].Value = cijena.ToString("#0.00").Replace('.', ',');
            dgw.Rows[index].Cells["rabat"].Value = "0,00";
            dgw.Rows[index].Cells["rabat_iznos"].Value = "0,00";
            dgw.Rows[index].Cells["cijena_bez_pdva"].Value = cijena.ToString("#0.00").Replace('.', ',');
            dgw.Rows[index].Cells["iznos_bez_pdva"].Value = cijena.ToString("#0.00").Replace('.', ',');
            dgw.Rows[index].Cells["iznos_ukupno"].Value = cijena.ToString("#0.00").Replace('.', ',');
            //     decimal porezPostotak = Decimal.Parse(dgw.Rows[index].Cells["porez"].Value.ToString().Replace(',', '.');
            string porezString = dgw.Rows[index].Cells["porez"].Value.ToString();
            //porezString = porezString.Replace(',', '.');
            decimal porezDecimal = Decimal.Parse(porezString)/100;

            dgw.Rows[index].Cells["vpc"].Value = Math.Round((cijena / (1 + porezDecimal)),2);
            dgw.Rows[index].Cells["nc"].Value = cijena.ToString("#0.00").Replace('.', ',');
            dgw.Rows[index].Cells["oduzmi"].Value = "NE";

            izracun(true);

           /* dgw.Rows[index].Cells["mpc"].Value = dgw.Rows[index].Cells["vpc"].Value.ToString().Replace('.', ',');
            izracun(true);*/
        }


        /// <summary>
        /// ne koristi se jer nisam znal napuniti ručno dataset
        /// </summary>
        private void NapuniReport()
        {
            if (dgw.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku.", "Greška");
                return;
            }

            decimal dec_parse;
            if (Decimal.TryParse(txtSifraOdrediste.Text, out dec_parse))
            {
                txtSifraOdrediste.Text = dec_parse.ToString();
            }
            else
            {
                MessageBox.Show("Greška kod upisa odredišta.", "Greška");
                return;
            }

            if (Decimal.TryParse(txtSifraFakturirati.Text, out dec_parse))
            {
                txtSifraFakturirati.Text = dec_parse.ToString();
            }
            else
            {
                MessageBox.Show("Greška kod upisa šifre za fakturirati.", "Greška");
                return;
            }

            Report.Faktura.repFaktura pr = new Report.Faktura.repFaktura();

            Dataset.DSRfakturaStavke dSRfakturaStavke = pr.dSRfakturaStavke;
            dSRfakturaStavke.Tables.Clear();
            DataTable DTsend = dSRfakturaStavke.Tables.Add("DTfakturaStavke");

            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("mpc");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("nbc");
            DTsend.Columns.Add("broj_fakture");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("sifra");
            DTsend.Columns.Add("jm");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("oduzmi");
            DTsend.Columns.Add("mpcStavka");
            DTsend.Columns.Add("rabatStavka");
            DTsend.Columns.Add("naziv");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("povratna_naknada");
            DataRow row;

            string broj = brojFakture();

            string uk = u.ToString();
            if (classSQL.remoteConnectionString == "")
            {
                uk = uk.Replace(",", ".");
            }
            else
            {
                uk = uk.Replace(".", ",");
            }

            string kol = "";
            string sifra;

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                sifra = dg(i, "sifra");

                row = DTsend.NewRow();
                row["kolicina"] = dg(i, "kolicina");
                row["vpc"] = dg(i, "vpc");
                row["nbc"] = dg(i, "nc");
                row["broj_fakture"] = ttxBrojFakture.Text;
                row["porez"] = dg(i, "porez");
                row["sifra"] = ReturnSifra(dg(i, "sifra"));
                row["rabat"] = dg(i, "rabat");
                row["oduzmi"] = dg(i, "oduzmi");
                row["id_skladiste"] = dgw.Rows[i].Cells[3].Value;
                row["povratna_naknada"] = dg(i, "povratna_naknada");

                DTsend.Rows.Add(row);
            }

            PCPOS.classNumberToLetter broj_u_text = new PCPOS.classNumberToLetter();

            double vpc = 0;
            double kolicina = 0;
            double porez;
            double rabat;
            double mpcStavka;
            double rabatStavka = 0;
            double pdvStavka = 0;
            double osnovicaStavka = 0;
            double rabatSve = 0;
            double mpc = 0;
            double sveUkupno = 0;
            double pdvUkupno = 0;
            double osnovicaUkupno = 0;
            DataTable dtTemp;

            for (int i = 0; i < DTsend.Rows.Count; i++)
            {
                sifra = DTsend.Rows[i]["sifra"].ToString();
                vpc = Math.Round(Convert.ToDouble(DTsend.Rows[i]["vpc"].ToString()), 3);
                kolicina = Math.Round(Convert.ToDouble(DTsend.Rows[i]["kolicina"].ToString()), 3);
                porez = Math.Round(Convert.ToDouble(DTsend.Rows[i]["porez"].ToString()), 2);
                rabat = Math.Round(Convert.ToDouble(DTsend.Rows[i]["rabat"].ToString()), 2);

                mpc = Math.Round(vpc * (1 + porez / 100.0) - 0.0000001, 2);

                rabatStavka = Math.Round(mpc * kolicina * rabat / 100, 2);
                mpcStavka = Math.Round(mpc * kolicina - rabatStavka, 2);
                pdvStavka = Math.Round(mpcStavka * (1 - 100 / (100 + porez)), 2);
                osnovicaStavka = Math.Round(mpcStavka - pdvStavka, 2);

                DTsend.Rows[i].SetField("mpc", mpc);
                DTsend.Rows[i].SetField("vpc", vpc);
                DTsend.Rows[i].SetField("porez", porez);
                DTsend.Rows[i].SetField("rabat", rabat);
                DTsend.Rows[i].SetField("kolicina", kol);
                DTsend.Rows[i].SetField("mpcStavka", mpcStavka);
                DTsend.Rows[i].SetField("rabatStavka", rabatStavka);

                string sqlS = "Select naziv," +
                    " jm" +
                    " FROM roba where sifra='" + sifra + "'";

                dtTemp = classSQL.select(sqlS, "roba").Tables[0];

                if (dtTemp.Rows.Count > 0)
                {
                    DTsend.Rows[i].SetField("naziv", dtTemp.Rows[0]["naziv"].ToString());
                    DTsend.Rows[i].SetField("jm", dtTemp.Rows[0]["jm"].ToString());
                }

                rabatSve += rabatStavka;
                osnovicaUkupno += osnovicaStavka;
                pdvUkupno += pdvStavka;
                sveUkupno += mpcStavka;
            }

            dSRfakturaStavke.Tables[0].Merge(DTsend, false);
            DTsend.AcceptChanges();
            dSRfakturaStavke.AcceptChanges();

            DateTime datum = dtpDatum.Value;

            string broj_slovima = broj_u_text.PretvoriBrojUTekst(sveUkupno.ToString(), ',', "kn", "lp").ToString();

            Dataset.DSFaktura dSFaktura = pr.dSFaktura;
            dSFaktura.Tables.Clear();
            DataTable DTFaktura = dSFaktura.Tables.Add("DTRfaktura");
            DTFaktura.Columns.Add("broj_fakture");
            DTFaktura.Columns.Add("datum");
            DTFaktura.Columns.Add("datum_dvo");
            DTFaktura.Columns.Add("datum_valute");
            DTFaktura.Columns.Add("jir");
            DTFaktura.Columns.Add("zki");
            DTFaktura.Columns.Add("mjesto_troska");
            DTFaktura.Columns.Add("placanje");
            DTFaktura.Columns.Add("otprema");
            DTFaktura.Columns.Add("model");
            DTFaktura.Columns.Add("napomena");
            DTFaktura.Columns.Add("rabat");
            DTFaktura.Columns.Add("ukupno");
            DTFaktura.Columns.Add("iznos_pdv");
            DTFaktura.Columns.Add("osnovica");
            DTFaktura.Columns.Add("godina_fakture");
            DTFaktura.Columns.Add("Naslov");
            DTFaktura.Columns.Add("kupac_tvrtka");
            DTFaktura.Columns.Add("naziv_date1");
            DTFaktura.Columns.Add("naziv_date2");
            DTFaktura.Columns.Add("kupac_adresa");
            DTFaktura.Columns.Add("id_kupac_grad");
            DTFaktura.Columns.Add("sifra_kupac");
            DTFaktura.Columns.Add("napomena_tvrtka");
            DTFaktura.Columns.Add("zr");
            DTFaktura.Columns.Add("izradio");
            DTFaktura.Columns.Add("banka");
            DTFaktura.Columns.Add("broj_slovima");
            DTFaktura.Columns.Add("kupac_oib");

            row = DTFaktura.NewRow();
            row["broj_fakture"] = broj;
            row["datum"] = datum;
            row["datum_dvo"] = dtpDatumDVO.Value.ToString("yyyy-MM-dd H:mm:ss");
            row["datum_valute"] = dtpDanaValuta.Value.ToString("yyyy-MM-dd H:mm:ss");
            row["jir"] = "123456789";
            row["zki"] = "987654321";
            row["mjesto_troska"] = "";
            row["placanje"] = cbNacinPlacanja.SelectedValue.ToString();
            row["otprema"] = cbOtprema.SelectedValue;
            row["model"] = txtModel.Text + broj + nmGodinaFakture.Value.ToString() + txtSifraFakturirati.Text;
            row["napomena"] = rtbNapomena.Text;
            row["rabat"] = rabatSve;
            row["ukupno"] = sveUkupno;
            row["iznos_pdv"] = pdvUkupno;
            row["osnovica"] = osnovicaUkupno;
            row["godina_fakture"] = nmGodinaFakture.Value.ToString();
            row["Naslov"] = broj + Util.Korisno.VratiDucanIBlagajnuZaIspis(2);
            row["kupac_tvrtka"] = "";
            row["naziv_date1"] = "Datum isporuke:";
            row["naziv_date2"] = "Datum dospijeća:";
            row["kupac_adresa"] = "";
            row["id_kupac_grad"] = "";
            row["sifra_kupac"] = "";
            row["napomena_tvrtka"] = "";
            row["zr"] = "";
            row["izradio"] = "";
            row["banka"] = "";
            row["broj_slovima"] = broj_slovima.ToLower();
            row["kupac_oib"] = "";
            DTFaktura.Rows.Add(row);

            string sql = "SELECT " +
                " nacin_placanja.naziv_placanja AS placanje," +
                " otprema.naziv AS otprema," +
                " partners.ime_tvrtke AS kupac_tvrtka," +
                " partners.adresa AS kupac_adresa," +
                " partners.id_grad AS id_kupac_grad," +
                " partners.id_partner AS sifra_kupac," +
                " partners.napomena AS napomena_tvrtka," +
                " ziro_racun.ziroracun AS zr," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio," +
                " ziro_racun.banka AS banka," +
                " partners.oib AS kupac_oib" +
                " FROM fakture" +
                " LEFT JOIN partners ON partners.id_partner='" + txtSifraOdrediste.Text + "'" +
                " LEFT JOIN otprema ON otprema.id_otprema='" + cbOtprema.SelectedValue + "'" +
                " LEFT JOIN nacin_placanja ON nacin_placanja.id_placanje='" + cbNacinPlacanja.SelectedValue.ToString() + "'" +
                " LEFT JOIN ziro_racun ON ziro_racun.id_ziroracun='" + cbZiroRacun.SelectedValue.ToString() + "'" +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'";

            dtTemp = classSQL.select(sql, "partners").Tables[0];

            if (dtTemp.Rows.Count > 0)
            {
                DTFaktura.Rows[0].SetField("placanje", dtTemp.Rows[0]["placanje"].ToString());
                DTFaktura.Rows[0].SetField("otprema", dtTemp.Rows[0]["otprema"].ToString());
                DTFaktura.Rows[0].SetField("kupac_tvrtka", dtTemp.Rows[0]["kupac_tvrtka"].ToString());
                DTFaktura.Rows[0].SetField("kupac_adresa", dtTemp.Rows[0]["kupac_adresa"].ToString());
                DTFaktura.Rows[0].SetField("id_kupac_grad", dtTemp.Rows[0]["id_kupac_grad"].ToString());
                DTFaktura.Rows[0].SetField("sifra_kupac", dtTemp.Rows[0]["sifra_kupac"].ToString());
                DTFaktura.Rows[0].SetField("napomena_tvrtka", dtTemp.Rows[0]["napomena_tvrtka"].ToString());
                DTFaktura.Rows[0].SetField("zr", dtTemp.Rows[0]["zr"].ToString());
                DTFaktura.Rows[0].SetField("izradio", dtTemp.Rows[0]["izradio"].ToString());
                DTFaktura.Rows[0].SetField("banka", dtTemp.Rows[0]["banka"].ToString());
                DTFaktura.Rows[0].SetField("kupac_oib", dtTemp.Rows[0]["kupac_oib"].ToString());
            }

            string id_kupac_grad = "";
            string id_kupac = "";
            if (dSFaktura.Tables[0].Rows.Count > 0)
            {
                id_kupac_grad = dSFaktura.Tables[0].Rows[0]["id_kupac_grad"].ToString();
                id_kupac = dSFaktura.Tables[0].Rows[0]["sifra_kupac"].ToString();
            }
            else
            {
            }

            dSFaktura.AcceptChanges();

            string naziv_fakture = " podaci_tvrtka.naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql(id_kupac_grad, naziv_fakture, id_kupac);

            Dataset.DSRpodaciTvrtke dSRpodaciTvrtke = pr.dSRpodaciTvrtke;
            dSRpodaciTvrtke.Tables.Clear();
            DataTable DTpodaciTvrtke = dSRpodaciTvrtke.Tables.Add("DTRpodaciTvrtke");

            dSRpodaciTvrtke.AcceptChanges();

            pr.dTRfakturaBindingSource.DataMember = "DTRfaktura";
            pr.dTRfakturaBindingSource.DataSource = dSFaktura;
            pr.dTRpodaciTvrtkeBindingSource.DataMember = "DTRpodaciTvrtke";
            pr.dTRpodaciTvrtkeBindingSource.DataSource = dSRpodaciTvrtke;
            pr.dTfakturaStavkeBindingSource.DataMember = "DTfakturaStavke";
            pr.dTfakturaStavkeBindingSource.DataSource = dSRfakturaStavke;

            ReportDataSource source = new ReportDataSource("DTRfaktura", dSFaktura.Tables[0]);

            ReportDataSource source1 = new ReportDataSource("DTRpodaciTvrtke", dSRpodaciTvrtke.Tables[0]);
            ReportDataSource source2 = new ReportDataSource("DTfakturaStavke", dSRfakturaStavke.Tables[0]);
            pr.reportViewer1.LocalReport.DataSources.Clear();
            pr.reportViewer1.LocalReport.DataSources.Add(source);
            pr.reportViewer1.LocalReport.DataSources.Add(source1);
            pr.reportViewer1.LocalReport.DataSources.Add(source2);

            pr.reportViewer1.LocalReport.Refresh();

            pr.samoIspis = true;
            pr.dokumenat = "FAK";
            pr.broj_dokumenta = broj;
            pr.ImeForme = "Faktura";
            pr.ShowDialog();
        }

        private void cbValuta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ucitano)
            {
                txtTecaj.Text = DSValuta.Tables[0].Select("id_valuta=" + cbValuta.SelectedValue.ToString() + "")[0]["tecaj"].ToString();
            }
        }

        private void btnStorno_Click(object sender, EventArgs e)
        {
            frmFakturaStorno a = new frmFakturaStorno();
            a.id_ducan = id_ducan;
            a.id_kasa = id_kasa;
            a.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi sp = new frmPartnerTrazi();
            sp.ShowDialog();
            txtotprkupac.Text = Properties.Settings.Default.id_partner;
        }

        public bool popunjena_s_otpremnicom = false; //ako je fakturirano zapisuje u otpremnice (naplaceno da ne uzme jos 1 put)

        private void txtotprkupac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT id_partner FROM partners WHERE id_partner = '" + txtotprkupac.Text + "' ", "otpremnice").Tables[0];

                if (DT.Rows.Count == 0)
                {
                    MessageBox.Show("Odabrani partner ne postoji.", "Greška");
                    //deleteFields();
                }
                else if (DT.Rows.Count == 1)
                {
                    EnableDisable(true);
                    edit = true;
                    btnDeleteAllFaktura.Enabled = true;
                    fillOtpremnice(DT.Rows[0][0].ToString());
                    popunjena_s_otpremnicom = true;
                    chbOduzmiIzSkladista.Enabled = false;
                    chbOduzmiIzSkladista.Checked = false;
                    btnObrisi.Enabled = false;
                    btnOtpr.Enabled = false;
                    txtotprkupac.Text = "";
                }
            }
        }

        private void txtotprkupac_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender.ToString().Length == 0)
                return;

            if (sender.ToString()[0] == '+')
            {
                if ('+' == (e.KeyChar))
                    return;
            }

            if (sender.ToString()[0] == '-')
            {
                if ('-' == (e.KeyChar))
                    return;
            }

            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dtpDatum.Value = DateTime.Now;
            dtpDatumDVO.Value = DateTime.Now;
            dtpDanaValuta.Value = DateTime.Now;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Tecajna_lista tec = new Tecajna_lista();
            tec.ShowDialog();
            txtTecaj.DataBindings.Clear();
            DSValuta = classSQL.select("SELECT * FROM valute", "valute");
            cbValuta.DataSource = DSValuta.Tables[0];
            cbValuta.DisplayMember = "ime_valute";
            cbValuta.ValueMember = "id_valuta";
            txtTecaj.DataBindings.Add("Text", DSValuta.Tables[0], "tecaj");
        }

        private void btnOtpr_Click(object sender, EventArgs e)
        {
            frmOtpremnicaUFakturu frm = new frmOtpremnicaUFakturu();
            frm.ShowDialog();

            if ((frm.broj_otpremnice != null) && (frm.skl_otpremnice != null))
            {
                EnableDisable(true);
                edit = true;
                btnDeleteAllFaktura.Enabled = true;
                fillOtpremnice(frm.broj_otpremnice, frm.skl_otpremnice);
                popunjena_s_otpremnicom = true;
                chbOduzmiIzSkladista.Enabled = false;
                chbOduzmiIzSkladista.Checked = false;
                btnObrisi.Enabled = false;
                txtotprkupac.Text = "";
            }
        }

        private void fillOtpremnice(string broj, string skladiste)
        {
            //fill header

            //--------fill otpremnica stavke----------------------------
            DataTable DTotpremnice = new DataTable();
            DataTable DTotpremnica_stavke = new DataTable();

            if (DTnaplaceneotpremnice.Columns.Contains("id") != true)
            {
                DTnaplaceneotpremnice.Columns.Add("id");
                DTnaplaceneotpremnice.Columns.Add("id_otpremnica");
            }

            DataRow row;
            DataTable dtR = new DataTable();
            //DSFS = classSQL.select("SELECT * FROM otpremnica_stavke WHERE broj_ponude = '" + DTotpremnice.Rows[0]["broj_ponude"].ToString() + "'", "ponude_stavke").Tables[0];
            bool header_1x = !popunjena_s_otpremnicom;
            DTotpremnice = classSQL.select("SELECT * FROM otpremnice WHERE broj_otpremnice = '" + broj + "' AND id_skladiste = '" + skladiste + "' ", "otpremnice").Tables[0];

            if (DTotpremnice.Rows.Count != 0)
            {
                for (int z = 0; z < DTotpremnice.Rows.Count; z++)
                {
                    row = DTnaplaceneotpremnice.NewRow();
                    row["id"] = DTnaplaceneotpremnice.Rows.Count.ToString();
                    row["id_otpremnica"] = DTotpremnice.Rows[z]["id_otpremnica"];
                    DTnaplaceneotpremnice.Rows.Add(row);

                    //dtR = classSQL.select(s, "roba_prodaja").Tables[0];
                    try
                    {
                        DTotpremnica_stavke = classSQL.select("SELECT * FROM otpremnica_stavke  WHERE id_otpremnice = '" + DTotpremnice.Rows[z]["id_otpremnica"].ToString() + "' AND naplaceno_fakturom <> 'TRUE'", "otpremnice stavke").Tables[0];
                        for (int x = 0; x < DTotpremnica_stavke.Rows.Count; x++)
                        {
                            dgw.Rows.Add();
                            int br = dgw.Rows.Count - 1;
                            DataTable roba = classSQL.select("Select * from roba where sifra = '" + DTotpremnica_stavke.Rows[x]["sifra_robe"].ToString() + "'", "roba").Tables[0];
                            dgw.Rows[br].Cells[0].Value = dgw.Rows.Count;
                            dgw.Rows[br].Cells["sifra"].Value = DTotpremnica_stavke.Rows[x]["sifra_robe"].ToString();
                            dgw.Rows[br].Cells["naziv"].Value = roba.Rows[0]["naziv"].ToString();
                            dgw.Rows[br].Cells["jmj"].Value = roba.Rows[0]["jm"].ToString();
                            dgw.Rows[br].Cells["oduzmi"].Value = roba.Rows[0]["oduzmi"].ToString();
                            dgw.Rows[br].Cells["cijena_bez_pdva"].Value = string.Format("{0:0.000}", DTotpremnica_stavke.Rows[x]["vpc"]);
                            dgw.Rows[br].Cells["kolicina"].Value = DTotpremnica_stavke.Rows[x]["kolicina"].ToString();
                            dgw.Rows[br].Cells["porez"].Value = DTotpremnica_stavke.Rows[x]["porez"].ToString();
                            dgw.Rows[br].Cells["rabat"].Value = DTotpremnica_stavke.Rows[x]["rabat"].ToString();
                            dgw.Rows[br].Cells["skladiste"].Value = DTotpremnica_stavke.Rows[x]["id_skladiste"].ToString();
                            dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
                            dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
                            dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
                            dgw.Rows[br].Cells["vpc"].Value = string.Format("{0:0.000}", DTotpremnica_stavke.Rows[x]["vpc"]);
                            //dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["mpc"]);
                            dgw.Rows[br].Cells["nc"].Value = string.Format("{0:0.00}", DTotpremnica_stavke.Rows[x]["nbc"].ToString());
                            dgw.Rows[br].Cells["id_stavka"].Value = DTotpremnica_stavke.Rows[x]["id_stavka"].ToString();

                            dgw.Select();
                            dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[3];
                            ControlDisableEnable(0, 1, 1, 0, 1);
                            edit = false;

                            //povratna naknada mora biti poslije izračuna jer bi se inače upisivala trenutna povratna naknada
                            //a ne povratna naknada upisana u fakturi
                            try
                            {
                                dgw.Rows[br].Cells["povratna_naknada"].Value = DTotpremnica_stavke.Rows[x]["povratna_naknada"].ToString();
                            }
                            catch
                            {
                                //uzeti iz baze
                                //DataTable DTpovrNaknada = classSQL.select("SELECT iznos FROM povratna_naknada WHERE sifra='" + DSFS.Rows[i]["sifra"].ToString().Trim() + "'", "roba").Tables[0];
                                //dgw.Rows[br].Cells["povratna_naknada"].Value = DTpovrNaknada.Rows.Count > 0 ? DTpovrNaknada.Rows[0]["iznos"].ToString() : "0,00";
                                dgw.Rows[br].Cells["povratna_naknada"].Value = "0.00";
                            }

                            if (header_1x)
                            {
                                txtSifraOdrediste.Text = DTotpremnice.Rows[0]["id_odrediste"].ToString();
                                try { txtPartnerNaziv.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["id_odrediste"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); } catch (Exception) { }
                                txtSifraFakturirati.Text = DTotpremnice.Rows[0]["osoba_partner"].ToString();
                                try { txtPartnerNaziv1.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["osoba_partner"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); } catch (Exception) { }
                                header_1x = false;
                            }
                            izracun(false);
                        }
                    }
                    catch { }
                }
            }
        }

        private void txtTecaj_TextChanged(object sender, EventArgs e)
        {
            decimal test;
            if (!decimal.TryParse(txtTecaj.Text, out test))
            {
                MessageBox.Show("Greška kod unosa tečaja!");
                txtTecaj.Select();
            }
        }

        private void cbStavkeValuta_CheckedChanged(object sender, EventArgs e)
        {
            if (cbStavkeValuta.Checked && txtTecaj.Text != "1")
            {
                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    try
                    {
                        izracun(false);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private void chbObracunPoreza_CheckedChanged(object sender, EventArgs e)
        {
            if (dgw.Rows.Count == 0)
                return;

            if (chbObracunPoreza.Checked)
            {
                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    try
                    {
                        string sifra = dgw.Rows[i].Cells["sifra"].FormattedValue.ToString();
                        DataTable DT = classSQL.select("SELECT porez FROM roba WHERE sifra='" + sifra + "'", "").Tables[0];
                        dgw.Rows[i].Cells["porez"].Value = DT.Rows[0][0].ToString();
                        izracun(false);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            else
            {
                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    dgw.Rows[i].Cells["porez"].Value = 0;
                    izracun(false);
                }
            }
        }

        private void chbSNBC_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    dgw.CurrentCell = dgw[1, i];
                    izracun(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnStavkaUMinus_Click(object sender, EventArgs e)
        {
            try
            {
                Kasa.frmDodajStavkuUMinus f = new Kasa.frmDodajStavkuUMinus();
                f.dokumenat = "faktura";
                f.ShowDialog();
                if (f.DialogResult == DialogResult.Yes)
                {
                    DataGridViewRow dRow = f.dgvRez.Rows[f.dgvRez.CurrentCell.RowIndex];

                    dgw.Rows.Add();
                    int br = dgw.Rows.Count - 1;
                    dgw.Select();

                    dgw.Rows[br].Cells[0].Value = br;
                    dgw.Rows[br].Cells["sifra"].Value = dRow.Cells["Šifra"].Value.ToString();
                    dgw.Rows[br].Cells["naziv"].Value = dRow.Cells["Naziv"].Value.ToString();
                    dgw.Rows[br].Cells["jmj"].Value = dRow.Cells["JM"].Value.ToString();

                    decimal tecaj = 1;
                    decimal vpc;
                    decimal pdv;
                    if (cbStavkeValuta.Checked)
                        decimal.TryParse(txtTecaj.Text, out tecaj);

                    decimal.TryParse(dRow.Cells["VPC"].Value.ToString(), out vpc);
                    decimal.TryParse(dRow.Cells["PDV"].Value.ToString(), out pdv);

                    dgw.Rows[br].Cells["mpc"].Value = (vpc + (vpc * pdv / 100));
                    dgw.Rows[br].Cells["cijena_bez_pdva"].Value = Math.Round(vpc, 4).ToString("#0.000");
                    dgw.Rows[br].Cells["kolicina"].Value = dRow.Cells["Količina"].Value.ToString();
                    dgw.Rows[br].Cells["porez"].Value = dRow.Cells["PDV"].Value.ToString();
                    dgw.Rows[br].Cells["oduzmi"].Value = dRow.Cells["Oduzmi"].Value.ToString();
                    dgw.Rows[br].Cells["rabat"].Value = dRow.Cells["Rabat"].Value.ToString();
                    dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
                    dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
                    dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
                    dgw.Rows[br].Cells["vpc"].Value = Math.Round(vpc, 4).ToString("#0.000");
                    dgw.Rows[br].Cells["nc"].Value = dRow.Cells["NBC"].Value.ToString();
                    dgw.Rows[br].Cells["skladiste"].Value = Class.Postavke.id_default_skladiste.ToString();
                    dgw.Rows[br].Cells["povratna_naknada"].Value = dRow.Cells["PN"].Value.ToString();
                    dgw.Rows[br].Cells["ppmv"].Value = 0;

                    dgw.CurrentCell = dgw.Rows[br].Cells[3];
                    //dgw.BeginEdit(true);
                    dgw.Rows[br].ReadOnly = true;
                    izracun(false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void dgw_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == indexKolicina)
                {
                    indexRowKolicina = e.RowIndex;
                    kolKolicina = 0;
                    if (!decimal.TryParse(dgw.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out kolKolicina))
                    {
                        MessageBox.Show("Pogrešan unos za količinu.");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnDodajAvioPodatke_Click(object sender, EventArgs e)
        {
            try
            {
                Util.frmAvioPodaciDodaj f = new Util.frmAvioPodaciDodaj();
                if (lblAvioPodaci.Tag != null)
                {
                    f.podaci = (string[])lblAvioPodaci.Tag;
                }
                f.ShowDialog();
                if (f.podaci != null)
                {
                    lblAvioPodaci.Text = string.Format(@"Reg. oznaka: {0}{3}Tip: {1}{3}MTOW: {2}", f.podaci[0], f.podaci[1], f.podaci[2], Environment.NewLine);
                    prikaziAvioPodatke(true);

                    lblAvioPodaci.Tag = f.podaci;
                }
                else
                {
                    prikaziAvioPodatke(false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void prikaziAvioPodatke(bool prikazi = true)
        {
            if (prikazi)
            {
                lblAvioPodaci.Enabled = prikazi;
                lblAvioPodaci.Visible = prikazi;
            }
            else
            {
                lblAvioPodaci.Text = "";
                lblAvioPodaci.Tag = null;
                lblAvioPodaci.Enabled = prikazi;
                lblAvioPodaci.Visible = prikazi;
            }
        }

        /// <summary>
        /// Method used to send create Invoice object and send it as POST request
        /// </summary>
        private void SendInvoice()
        {
            if (dgw.Rows.Count > 0)
            {
                HttpWebResponse response;
                // Check if partner exists
                Partner takeOverPartner = GetPartnerObject(txtSifraOdrediste.Text);
                response = Eracun.Eracun.SendRequest(takeOverPartner, "api/checkpartner");
                if(response.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show($"Greška kod kreiranja partnera {takeOverPartner.Name}. Molimo da provjerite podatke ako su ispravni", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Partner invoiceToPartner = GetPartnerObject(txtSifraFakturirati.Text);
                response = Eracun.Eracun.SendRequest(invoiceToPartner, "api/checkpartner");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show($"Greška kod kreiranja partnera {invoiceToPartner.Name}. Molimo da provjerite podatke ako su ispravni", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if employee exists
                Employee employee = GetEmployeeObject(cbKomercijalist.SelectedValue.ToString());
                response = Eracun.Eracun.SendRequest(employee, "api/checkemployee");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show($"Greška kod kreiranja zaposlenika {employee.Name} {employee.Surname}. Molimo da provjerite podatke ako su ispravni", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create Invoice
                List<InvoiceItem> invoiceItemList = new List<InvoiceItem>();

                foreach (DataGridViewRow row in dgw.Rows)
                {
                    decimal.TryParse(row.Cells["porez"].Value.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal porez);
                    decimal.TryParse(row.Cells["mpc"].Value.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal mpc);
                    decimal.TryParse(row.Cells["vpc"].Value.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal vpc);
                    decimal.TryParse(row.Cells["rabat"].Value.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal rabat);
                    int.TryParse(row.Cells["kolicina"].Value.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out int kolicina);

                    InvoiceItem invoiceItem = new InvoiceItem
                    {
                        MerchandiseId = row.Cells["sifra"].Value.ToString(),
                        Name = row.Cells["naziv"].Value.ToString(),
                        Amount = kolicina,
                        TaxPercentage = porez,
                        RetailPrice = mpc,
                        WholesalePrice = vpc,
                        DiscountPercentage = rabat
                    };

                    invoiceItemList.Add(invoiceItem);
                }

                if (invoiceItemList.Count > 0)
                {
                    Invoice invoice = new Invoice
                    {
                        TakeOverPartnerId = Convert.ToInt32(txtSifraOdrediste.Text),
                        InvoiceForPartnerId = Convert.ToInt32(txtSifraFakturirati.Text),
                        EmployeeId = Convert.ToInt32(cbKomercijalist.SelectedValue.ToString()),
                        DateDVO = dtpDatum.Value,
                        DateIssued = dtpDatumDVO.Value,
                        DateCurrency = dtpDanaValuta.Value,
                        Currency = cbValuta.SelectedText.ToString(),
                        CurrencyRate = Convert.ToDecimal(txtTecaj.Text),
                        Days = Convert.ToInt32(txtDana.Text),
                        Remark = rtbNapomena.Text,
                        InvoiceItems = invoiceItemList,
                        Employee = employee
                    };

                    response = Eracun.Eracun.SendRequest(invoice, "api/createinvoice");
                    if (response.StatusCode == HttpStatusCode.OK)
                        MessageBox.Show("Eračun kreairan!", "Obavijest", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Greška kod kreiranja stavka.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Used to return Partner object for given Id (sifra)
        /// </summary>
        /// <param name="sifra"></param>
        /// <returns></returns>
        private Partner GetPartnerObject(string sifra)
        {
            DataTable DTpartner = Global.Database.GetPartners(sifra);
            Partner partner = new Partner
            {
                Code = DTpartner.Rows[0]["id_partner"].ToString(),
                Name = DTpartner.Rows[0]["ime_tvrtke"].ToString(),
                Address = DTpartner.Rows[0]["adresa"].ToString(),
                Oib = DTpartner.Rows[0]["oib"].ToString(),
                PhoneNumber = DTpartner.Rows[0]["mob"].ToString(),
                Email = DTpartner.Rows[0]["email"].ToString()
            };
            return partner;
        }

        /// <summary>
        /// Used to return Employee object for given Id (id_zaposlenik)
        /// </summary>
        /// <param name="sifra"></param>
        /// <returns></returns>
        private Employee GetEmployeeObject(string sifra)
        {
            DataTable DTzaposlenik = Global.Database.GetZaposlenici(sifra);
            Employee employee = new Employee
            {
                Name = DTzaposlenik.Rows[0]["ime"].ToString(),
                Surname = DTzaposlenik.Rows[0]["prezime"].ToString(),
                Address = DTzaposlenik.Rows[0]["adresa"].ToString(),
                Oib = DTzaposlenik.Rows[0]["oib"].ToString(),
                PhoneNumber = DTzaposlenik.Rows[0]["tel"].ToString()
            };
            return employee;
        }

        /// <summary>
        /// 
        /// </summary>
        private void ReadJSON()
        {
            foreach (string file in Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory + "JSON", "*.txt"))
            {
                string jsonString = File.ReadAllText(file);
                if (!string.IsNullOrWhiteSpace(jsonString))
                {
                    fromJSON = true;
                    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    Faktura faktura = serializer.Deserialize<Faktura>(jsonString);
                    if (faktura != null)
                    {
                        NoviUnos();
                        // Fill fields
                        ttxBrojFakture.Text = brojFakture();
                        txtSifraOdrediste.Text = faktura.IdOdrediste.ToString();
                        txtSifraFakturirati.Text = faktura.IdFakturirati.ToString();
                        dtpDatum.Value = DateTime.Parse(faktura.DatumDVO);
                        dtpDatumDVO.Value = DateTime.Parse(faktura.Datum);
                        dtpDanaValuta.Value = DateTime.Parse(faktura.DatumValute);

                        // Stavke
                        DataTable DTroba = Global.Database.GetRoba();

                        foreach (FakturaStavke stavka in faktura.Stavke)
                        {
                            if (DTroba.AsEnumerable().Any(it => it.Field<string>("sifra") == stavka.SifraRobe))
                            {
                                int index = dgw.Rows.Add();
                                dgw.Rows[index].Cells["br"].Value = index + 1;
                                dgw.Rows[index].Cells["sifra"].Value = stavka.SifraRobe;
                                dgw.Rows[index].Cells["naziv"].Value = stavka.Naziv;
                                dgw.Rows[index].Cells["skladiste"].Value = faktura.IdSkladiste.ToString();
                                dgw.Rows[index].Cells["jmj"].Value = "KOM";
                                dgw.Rows[index].Cells["kolicina"].Value = stavka.Kolicina.ToString().Replace('.', ',');
                                dgw.Rows[index].Cells["porez"].Value = stavka.Porez.ToString().Replace('.', ',');
                                dgw.Rows[index].Cells["mpc"].Value = stavka.Mpc.ToString().Replace('.', ',');
                                dgw.Rows[index].Cells["rabat"].Value = stavka.Rabat.ToString().Replace('.', ',');
                                dgw.Rows[index].Cells["rabat_iznos"].Value = stavka.Rabat > 0 ? ((stavka.Vpc * stavka.Kolicina) * stavka.Rabat / 100).ToString().Replace('.', ',') : "0";
                                dgw.Rows[index].Cells["cijena_bez_pdva"].Value = stavka.Vpc.ToString().Replace('.', ',');
                                dgw.Rows[index].Cells["iznos_bez_pdva"].Value = (stavka.Vpc * stavka.Kolicina).ToString().Replace('.', ',');
                                dgw.Rows[index].Cells["iznos_ukupno"].Value = ((stavka.Vpc * stavka.Kolicina) * (1 + (stavka.Porez / 100))).ToString().Replace('.', ',');
                                dgw.Rows[index].Cells["vpc"].Value = stavka.Vpc.ToString().Replace('.', ',');
                                dgw.Rows[index].Cells["nc"].Value = stavka.Nbc.ToString().Replace('.', ',');
                                dgw.Rows[index].Cells["id_stavka"].Value = index;
                                dgw.Rows[index].Cells["oduzmi"].Value = "DA";
                                dgw.Rows[index].Cells["porez_potrosnja"].Value = "0,00";
                                dgw.Rows[index].Cells["povratna_naknada"].Value = "0,00";
                                dgw.Rows[index].Cells["ppmv"].Value = "0,00";
                            }
                        }
                    }
                    izracun(true);
                }
            }
        }

        private void txtPartnerNaziv_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgw_KeyDown(object sender, KeyEventArgs e)
        {
            int icolumn = dgw.CurrentCell.ColumnIndex;
            int irow = dgw.CurrentCell.RowIndex;

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (icolumn == dgw.Columns.Count - 1)
                {
                    dgw.Rows.Add();
                    dgw.CurrentCell = dgw[0, irow + 1];
                }
                else
                {
                    dgw.CurrentCell = dgw[icolumn + 1, irow];
                }
            }
        }

        private void BtnEracun_Click(object sender, EventArgs e)
        {
            SendInvoice();
        }

        private void BtnJson_Click(object sender, EventArgs e)
        {
            ReadJSON();
        }
    }
}