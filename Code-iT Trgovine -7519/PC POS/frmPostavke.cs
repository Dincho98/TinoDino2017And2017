using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PCPOS.Minimax.Entities;
using PCPOS.synWeb;
using PCPOS.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PCPOS
{
    public partial class frmPostavke : Form
    {
        public frmPostavke()
        {
            InitializeComponent();
        }

        private DataSet dt = new DataSet();
        public decimal VerzijPrograma = 1.15m;
        private bool loaded = false;

        private void frmPostavke_Load(object sender, EventArgs e)
        {
            verzija.Text = "Trenutna verzija je " + Properties.Settings.Default.verzija_programa.ToString().Replace(",", ".");

            centralaSyncShowHide(false);
            FillCurrencyCB();
            SetRemoteFields();
            SetComboBox();
            SetNagradivanje();
            SetFiskal();
            WebPogled();
            BackupBaze();
            setVaga();
            cbSustavPDV.SelectedValue = (Class.Postavke.sustavPdv ? 1 : 0);
            cbveleprodaja.SelectedValue = (Class.Postavke.veleprodaja ? 1 : 0);
            this.Paint += new PaintEventHandler(Form1_Paint);

            chbOslobodenjePDVa.Checked = Class.Postavke.oslobodenje_pdva;

            chbPrikazObavijestiZaFiskaliziranjeFakture.Checked = Class.Postavke.fiskalizacija_faktura_prikazi_obavijest;
            chbCentralaAktivno.Checked = Class.Postavke.centralaAktivno;
            chbIsCentrala.Checked = Class.Postavke.isCentrala;
            chbProgramskoSkladiste.Checked = Class.Postavke.skidajSkladisteProgramski;
            txtPorezNaDohodak.Text = Class.Postavke.porezNaDohodak.ToString();
            chbIspisPartneraAktivnost.Checked = Class.Postavke.ispisPartnera;
            chbNemaNaSkl.Checked = Class.Postavke.upozoriNaMinus;
            chbProvjeraSkladista.Checked = Class.Postavke.provjera_stanja;
            chbSNBC.Checked = Class.Postavke.proizvodnjaFakturaNbc;
            chbFiskalizacijaIskljucena.Checked = Class.Postavke.upozoriIskljucenuFiskalizaciju;

            if (!Directory.Exists(txtBackupLokacije.Text))
            {
                txtBackupLokacije.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Sigurnosna kopija baze " + DateTime.Now.Year.ToString();
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Sigurnosna kopija baze " + DateTime.Now.Year.ToString());
                chbBackupAktivnost.Checked = true;
                btnSpremiBackup.PerformClick();
            }

            txtArhiviraniFajlovi.Text = Class.Postavke.putanjaZaSkeniraneFajlove;
            txtCentralaPoslovnica.Text = Class.Postavke.centralaPoslovnica;
            txtLozinkaZaCert.Text = Class.Postavke.certifikat_zaporka;
            txtPutanjaZaCert.Text = Class.Postavke.putanja_certifikat;

            txtDomena.Text = Properties.Settings.Default.domena_za_sinkronizaciju;

            chbAktiviranaWebSyn.Checked = Class.Postavke.posaljiDokumenteNaWeb;
            chbZabraniPromjeneCijena.Checked = Class.Postavke.robaZabraniMijenjanjeCijena;
            chbKolicinaUMinus.Checked = Class.Postavke.kolicina_u_minus;
            chbRucnoUpisivanjeKarticeKupca.Checked = Class.Postavke.rucnoUpisivanjeKarticeKupca;
            chbSakrijFormuZaProdajuUMinus.Checked = Class.Postavke.sakrij_formu_za_prodaju_u_minus;
            chbTestFisklaizacija.Checked = Class.Postavke.TEST_FISKALIZACIJA;
            chbPovratnaNaknada.Checked = Class.Postavke.koristi_povratnu_naknadu;
            chbAutomatskiZapisnik.Checked = Class.Postavke.automatski_zapisnik;
            chbUzmiAvanseUPrometKase.Checked = Class.Postavke.uzmi_avanse_u_promet_kase_POS;
            chbMainFormControlBox.Checked = Class.Postavke.controlBox;

            chbMaloprodajaNaplataGotovinaButtonShow.Checked = Class.Postavke.maloprodaja_naplata_gotovina_button_show;
            chbMaloprodajaNaplataKarticaButtonShow.Checked = Class.Postavke.maloprodaja_naplata_kartica_button_show;
            chbProdajaAutomobila.Checked = Class.Postavke.prodaja_automobila;

            chbMedjuskladisnicaSProizvodjackomCijenom.Checked = Class.Postavke.proizvodnjaMeduskladisnicaPC;
            chbNormativSProizvodjackomCijenom.Checked = Class.Postavke.proizvodnja_normativ_pc;
            chbUzmiRabatUOdjaviKomisije.Checked = Class.Postavke.uzmi_rabat_u_odjavi_komisije;
            chbDozvoli_fikaliranje_0_kn.Checked = Class.Postavke.dozvoli_fikaliranje_0_kn;

            txtApiKey.Text = Class.Postavke.UDSGameApiKey;
            chbUseUdsGame.Checked = Class.Postavke.UDSGame;
            chbUseEmployees.Checked = Class.Postavke.UDSGameEmployees;

            GetOdabranaSkladisa();
            loaded = true;
        }

        private void GetOdabranaSkladisa()
        {
            DataSet dsOdabranaSkladista;
            string koristiSkladista;
            try
            {
                dsOdabranaSkladista = classSQL.select("Select id_skladiste as ID, skladiste as Name from skladiste where aktivnost = 'DA'", "skladiste");
                if (dsOdabranaSkladista != null && dsOdabranaSkladista.Tables.Count > 0 && dsOdabranaSkladista.Tables[0] != null && dsOdabranaSkladista.Tables[0].Rows.Count > 0)
                {
                    koristiSkladista = Class.Postavke.koristiSkladista;
                    string[] checkeds = null;

                    if (koristiSkladista.Length > 0)
                        checkeds = koristiSkladista.Split(',');

                    ((ListBox)clbKoristiSkladista).DataSource = dsOdabranaSkladista.Tables[0];
                    ((ListBox)clbKoristiSkladista).DisplayMember = "Name";
                    ((ListBox)clbKoristiSkladista).ValueMember = "ID";

                    if (checkeds == null || checkeds.Count() == 0)
                        return;

                    for (int i = 0; i < clbKoristiSkladista.Items.Count; i++)
                    {
                        if (checkeds.Contains(((DataRowView)(clbKoristiSkladista.Items[i]))["ID"].ToString()))
                        {
                            clbKoristiSkladista.SetItemChecked(i, true);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dsOdabranaSkladista = null;
                koristiSkladista = null;
            }
        }

        private void BackupBaze()
        {
            try
            {
                chbBackupAktivnost.Checked = Class.Postavke.backup_aktivnost;
                txtBackupLokacije.Text = Class.Postavke.lokacija_sigurnosne_kopije;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void WebPogled()
        {
            try
            {
                chbWebActive.Checked = Class.Postavke.salji_na_web;
                txtUsernameWeb.Text = Class.Postavke.salji_na_web_user;
                txtPasswordWeb.Text = Class.Postavke.salji_na_web_pass;
                txtDomenaWeb.Text = Class.Postavke.salji_na_web_ftp;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void SetFiskal()
        {
            DataTable DTSK = new DataTable("OznakaFiskal");
            DTSK.Columns.Add("id", typeof(string));
            DTSK.Columns.Add("naziv", typeof(string));
            DTSK.Rows.Add("P", "P - na nivou poslovnog prostora");
            DTSK.Rows.Add("N", "N - na nivou naplatnog uređaja");

            cbOznakaFiskal.DataSource = DTSK;
            cbOznakaFiskal.DisplayMember = "naziv";
            cbOznakaFiskal.ValueMember = "id";

            DataTable DT = classSQL.select_settings("SELECT * FROM fiskalizacija", "fiskalizacija").Tables[0];
            if (DT.Rows[0]["aktivna"].ToString() == "1")
            {
                chbFiskal.Checked = true;
            }
            else
            {
                chbFiskal.Checked = false;
            }

            txtNazivCertifikata.Text = DT.Rows[0]["naziv_certifikata"].ToString();
            cbOznakaFiskal.SelectedValue = DT.Rows[0]["oznaka_slijednosti"].ToString();
        }

        private void SetRemoteFields()
        {
            PCPOS.Util.classFukcijeZaUpravljanjeBazom B = new classFukcijeZaUpravljanjeBazom("MALOPRODAJA", "POS");
            List<string> L = B.UzmiSveBazeIzPostgressa();
            DataTable DT = new DataTable();
            if (DT.Columns.Count == 0)
            {
                DT.Columns.Add("id");
                DT.Columns.Add("name", typeof(int));
            }

            foreach (string db in L)
            {
                if (db != "postgres" && (db.StartsWith(Util.Korisno.prefixBazeKojaSeKoristi())))
                {
                    string baza = db;
                    baza = db.Remove(0, Util.Korisno.prefixBazeKojaSeKoristi().Length);
                    //baza = baza.Replace("DB", "");
                    //baza = baza.Replace("POS", "");
                    //baza = baza.Replace("db", "");
                    //baza = baza.Replace("pos", "");
                    DT.Rows.Add(db, baza);
                }
            }
            DataView dv = new DataView(DT);
            dv.Sort = "name asc";
            DT = dv.ToTable();
            cbRemoteNameDatabase.DataSource = DT;
            cbRemoteNameDatabase.ValueMember = "id";
            cbRemoteNameDatabase.DisplayMember = "name";

            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";
            XDocument xmlFile = XDocument.Load(path);
            var query = from c in xmlFile.Element("settings").Elements("database_remote").Elements("postgree") select c;
            foreach (XElement book in query)
            {
                txtRemoteImeServera.Text = book.Attribute("server").Value;
                txtRemoteUsername.Text = book.Attribute("username").Value;
                txtRemotePort.Text = book.Attribute("port").Value;
                cbRemoteNameDatabase.SelectedValue = book.Attribute("database").Value;
                txtRemoteLozinka.Text = Class.Postavke.pass;

                if (book.Attribute("active").Value == "1")
                {
                    chbActive.Checked = true;
                }
                else
                {
                    chbActive.Checked = false;
                }
            }

            txtRemoteLozinka.PasswordChar = '*';
            //try
            //{
            //    DataTable DTDB = classSQL.select("SELECT datname FROM pg_database WHERE datistemplate IS FALSE AND datallowconn IS TRUE AND datname!='postgres';", "").Tables[0];

            //    for (int i = 0; i < DTDB.Rows.Count; i++)
            //    {
            //        cbRemoteNameDatabase.Items.Add(DTDB.Rows[i][0].ToString());
            //    }
            //}
            //catch (Exception) { }
        }

        private void SetNagradivanje()
        {
            if (Class.Postavke.on_of_cashback)
            {
                rbCashBack.Checked = true;
            }
            else if (Class.Postavke.on_of_bodovi)
            {
                rbBodovi.Checked = true;
            }
            else if (Class.Postavke.on_of_postotak)
            {
                rbPopustSlljedecakupovina.Checked = true;
            }
            else
            {
                rbBezNagrade.Checked = true;
            }
        }

        private void SetComboBox()
        {
            DataTable dtKalkulacija, dtFaktura;
            try
            {
                dtKalkulacija = classSQL.select("select 1 as value, 'Kalkulacija 1' as display union select 2 as value, 'Kalkulacija 2' as display;", "Kalkulacije").Tables[0];

                cmbVrstaKalkulacije.DisplayMember = "display";
                cmbVrstaKalkulacije.ValueMember = "value";
                cmbVrstaKalkulacije.DataSource = dtKalkulacija;
                cmbVrstaKalkulacije.SelectedValue = Class.Postavke.idKalkulacija;

                dtFaktura = classSQL.select(@"select 1 as value, 'Faktura 1' as display
union
select 2 as value, 'Faktura 2' as display
union
select 3 as value, 'Faktura 3' as display;", "Fakture").Tables[0];

                cmbVrstaFakture.DisplayMember = "display";
                cmbVrstaFakture.ValueMember = "value";
                cmbVrstaFakture.DataSource = dtFaktura;
                cmbVrstaFakture.SelectedValue = Class.Postavke.idFaktura;

                //fill Skladiste
                DataTable DTskladiste = classSQL.select("SELECT * FROM skladiste", "skladiste").Tables[0];
                cbSkladiste.DataSource = DTskladiste;
                cbSkladiste.DisplayMember = "skladiste";
                cbSkladiste.ValueMember = "id_skladiste";

                DataTable DTskladisteNormativa = classSQL.select("SELECT * FROM skladiste", "skladiste").Tables[0];
                cmbSkladisteNormativa.DataSource = DTskladisteNormativa;
                cmbSkladisteNormativa.DisplayMember = "skladiste";
                cmbSkladisteNormativa.ValueMember = "id_skladiste";

                //fill Zaposlenik
                DataTable DS_Zaposlenik = classSQL.select("SELECT ime + ' ' + prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici").Tables[0];
                cbBlagajnik.DataSource = DS_Zaposlenik;
                cbBlagajnik.DisplayMember = "IME";
                cbBlagajnik.ValueMember = "id_zaposlenik";

                //fill Ducan
                DataTable DTducan = classSQL.select("SELECT * FROM ducan", "ducan").Tables[0];
                cbDucan.DataSource = DTducan;
                cbDucan.DisplayMember = "ime_ducana";
                cbDucan.ValueMember = "id_ducan";

                cbSkladiste.SelectedValue = Class.Postavke.id_default_skladiste;
                cmbSkladisteNormativa.SelectedValue = Class.Postavke.id_default_skladiste_normativ;
                cbBlagajnik.SelectedValue = Class.Postavke.id_default_blagajnik;
                cbDucan.SelectedValue = Class.Postavke.id_default_ducan;

                fillCbNaplatnih();

                DataTable DTSK = new DataTable("pdv");
                DTSK.Columns.Add("id", typeof(string));
                DTSK.Columns.Add("naziv", typeof(string));
                DTSK.Rows.Add("0", "NE");
                DTSK.Rows.Add("1", "DA");

                cbSustavPDV.DataSource = DTSK;
                cbSustavPDV.DisplayMember = "naziv";
                cbSustavPDV.ValueMember = "id";

                DataTable DTSK1 = new DataTable("veleprodaja");
                DTSK1.Columns.Add("id", typeof(string));
                DTSK1.Columns.Add("naziv", typeof(string));
                DTSK1.Rows.Add("0", "NE");
                DTSK1.Rows.Add("1", "DA");

                cbveleprodaja.DataSource = DTSK1;
                cbveleprodaja.DisplayMember = "naziv";
                cbveleprodaja.ValueMember = "id";
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dtKalkulacija = null;
                dtFaktura = null;
            }
        }

        private void fillCbNaplatnih()
        {
            try
            {
                string sql = string.Format("SELECT * FROM blagajna WHERE id_ducan = {0};", cbDucan.SelectedValue.ToString());

                //fill blagajna
                DataTable DTblagajna = classSQL.select(sql, "blagajna").Tables[0];
                cbKasa.DataSource = DTblagajna;
                cbKasa.DisplayMember = "ime_blagajne";
                cbKasa.ValueMember = "id_blagajna";

                //fill faktura
                DataTable DTfak = classSQL.select(sql, "blagajna").Tables[0];
                cbFaktureNP.DataSource = DTfak;
                cbFaktureNP.DisplayMember = "ime_blagajne";
                cbFaktureNP.ValueMember = "id_blagajna";

                //fill avans
                DataTable DTavans = classSQL.select(sql, "blagajna").Tables[0];
                cbAvansNP.DataSource = DTavans;
                cbAvansNP.DisplayMember = "ime_blagajne";
                cbAvansNP.ValueMember = "id_blagajna";

                //fill faktura bez robe
                DataTable DTfakBZ = classSQL.select(sql, "blagajna").Tables[0];
                cbFaktureBR.DataSource = DTfakBZ;
                cbFaktureBR.DisplayMember = "ime_blagajne";
                cbFaktureBR.ValueMember = "id_blagajna";

                cbKasa.SelectedValue = Class.Postavke.id_maloprodaja_naplatni_uredaj;
                cbFaktureNP.SelectedValue = Class.Postavke.naplatni_uredaj_faktura;
                cbAvansNP.SelectedValue = Class.Postavke.naplatni_uredaj_avans;
                cbFaktureBR.SelectedValue = Class.Postavke.naplatni_uredaj_faktura_bez_robe;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void setVaga()
        {
            try
            {
                Korisno.vaga = Class.Postavke.useVaga;
                Korisno.serialPort = Class.Postavke.comPort;
                Korisno.baudRate = Class.Postavke.baudRate;

                int[] baudRate = { 4800, 9600, 19200, 38400, 57600, 115200, 230400 };

                if (cmbSerialPortName.Items.Count > 0)
                    cmbSerialPortName.Items.Clear();
                if (cmbSerialPortBaudRate.Items.Count > 0)
                    cmbSerialPortBaudRate.Items.Clear();

                foreach (var item in SerialPort.GetPortNames())
                {
                    cmbSerialPortName.Items.Add(item);
                }
                if (Korisno.serialPort != null)
                    cmbSerialPortName.SelectedItem = Korisno.serialPort;

                foreach (var item in baudRate)
                {
                    cmbSerialPortBaudRate.Items.Add(item);
                }
                if (Korisno.baudRate != 0)
                    cmbSerialPortBaudRate.SelectedItem = Korisno.baudRate;

                chkVaga.Checked = Korisno.vaga;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Greška");
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbKasa.SelectedValue == null || cbAvansNP.SelectedValue == null || cbFaktureNP.SelectedValue == null || cbFaktureBR.SelectedValue == null)
                {
                    MessageBox.Show("Unesite brojeve naplatnih uređaja.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string cashback = "0";
                string bodovi = "0";
                string postotak = "0";
                string fiskalizacija_faktura_prikazi_obavijest = "False";

                if (chbPrikazObavijestiZaFiskaliziranjeFakture.Checked)
                {
                    fiskalizacija_faktura_prikazi_obavijest = "True";
                }
                if (rbCashBack.Checked)
                {
                    cashback = "1";
                }
                else if (rbBodovi.Checked)
                {
                    bodovi = "1";
                }
                else if (rbPopustSlljedecakupovina.Checked)
                {
                    postotak = "1";
                }

                string sql = string.Format(@"UPDATE postavke
SET default_ducan = '{0}', default_blagajna = '{1}', default_skladiste = '{2}', default_blagajnik = '{3}',
on_off_cashback = '{4}', on_off_bodovi = '{5}', sustav_pdv = '{6}', naplatni_uredaj_faktura = '{7}',
naplatni_uredaj_avans = '{8}', naplatni_uredaj_faktura_bez_robe = '{9}', putanja_certifikat = '{10}', certifikat_zaporka = '{11}',
on_off_postotak = '{12}', veleprodaja = '{13}', fiskalizacija_faktura_prikazi_obavijest = '{14}', id_default_skladiste_normativ = {15};",
cbDucan.SelectedValue, cbKasa.SelectedValue, cbSkladiste.SelectedValue, cbBlagajnik.SelectedValue,
                    cashback, bodovi, cbSustavPDV.SelectedValue, cbFaktureNP.SelectedValue,
                    cbAvansNP.SelectedValue, cbFaktureBR.SelectedValue, txtPutanjaZaCert.Text, txtLozinkaZaCert.Text,
                    postotak, cbveleprodaja.SelectedValue, fiskalizacija_faktura_prikazi_obavijest, (cmbSkladisteNormativa.SelectedValue == null ? 0 : cmbSkladisteNormativa.SelectedValue));

                provjera_sql(classSQL.Setings_Update(sql));

                sql = string.Format(@"UPDATE fiskalizacija SET oznaka_slijednosti = '{0}', naziv_certifikata = '{1}', aktivna = '{2}';", cbOznakaFiskal.SelectedValue.ToString(), txtNazivCertifikata.Text, (chbFiskal.Checked ? 1 : 0));
                provjera_sql(classSQL.Setings_Update(sql));

                sql = string.Format("update postavke set dozvoli_fikaliranje_0_kn = {0};", (chbDozvoli_fikaliranje_0_kn.Checked ? 1 : 0));
                classSQL.Setings_Update(sql);

                MessageBox.Show("Spremljeno!");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void btnKompaktnaTest_Click(object sender, EventArgs e)
        {
            if (SQL.claasConnectDatabase.TestCompactConnection(txtKompaktnaPut.Text) == true)
            {
                MessageBox.Show("Konekcija je uspjela.");
            }
            else
            {
                MessageBox.Show("Konekcija nije uspjela.");
            }
        }

        private void btnKompaktnaSpremi_Click(object sender, EventArgs e)
        {
            if (!File.Exists(txtKompaktnaPut.Text))
            {
                MessageBox.Show("Odabrana baza ne postoji.", "Greška");
                return;
            }

            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";
            XDocument xmlFile = XDocument.Load(path);
            var query = from c in xmlFile.Element("settings").Elements("database_compact").Elements("path_database") select c;
            foreach (XElement book in query)
            {
                book.Attribute("path").Value = txtKompaktnaPut.Text;
            }
            xmlFile.Save(path);
            MessageBox.Show("Spremljeno.", "Spremljeno.");
        }

        private OpenFileDialog openFileDialog1 = new OpenFileDialog();

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtKompaktnaPut.Text = openFileDialog1.FileName;
            }
        }

        //private void SetDatabase()
        //{
        //}

        private void txtRemoteTest_Click(object sender, EventArgs e)
        {
            if (SQL.claasConnectDatabase.TestRemoteConnection() == true)
            {
                MessageBox.Show("Konekcija je uspjela.");
            }
            else
            {
                MessageBox.Show("Konekcija nije uspjela.");
            }
        }

        private void txtRemoteSpremi_Click(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";
            XDocument xmlFile = XDocument.Load(path);
            var query = from c in xmlFile.Element("settings").Elements("database_remote").Elements("postgree") select c;
            foreach (XElement book in query)
            {
                book.Attribute("server").Value = txtRemoteImeServera.Text;
                book.Attribute("username").Value = txtRemoteUsername.Text;
                book.Attribute("port").Value = txtRemotePort.Text;
                book.Attribute("database").Value = cbRemoteNameDatabase.SelectedValue.ToString();
                provjera_sql(classSQL.Setings_Update("UPDATE postavke SET pass='" + txtRemoteLozinka.Text + "'"));

                if (chbActive.Checked)
                {
                    book.Attribute("active").Value = "1";
                }
                else
                {
                    book.Attribute("active").Value = "1";
                }
            }

            xmlFile.Save(path);
            Class.PodaciZaSpajanjeCompaktna.getPodaci();
            MessageBox.Show("Spremljeno.");
            Application.Restart();
        }

        private void btnNadogradi_Click(object sender, EventArgs e)
        {
            if (!File.Exists("ne_salji"))
            {
                if (!Util.CheckConnection.Check())
                {
                    MessageBox.Show("Niste spojeni na Internet!", "Upozorenje!");
                    return;
                }

                Util.Korisno.NovijaInacica(false);
            }
        }

        private static string GetApplicationPath()
        {
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmCreateRemoteDB crb = new frmCreateRemoteDB();
            crb.ShowDialog();
        }

        private void btnProvjeriNadogradnju_Click(object sender, EventArgs e)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFile("https://www.pc1.hr/pcpos/update/verzija.txt", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\verzija.txt");
            string VerzijaNaNetu = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\verzija.txt");

            if (Properties.Settings.Default.verzija_programa < Convert.ToDecimal(VerzijaNaNetu))
            {
                if (MessageBox.Show("Na Internetu postoji novija inačica programa.\r\n\r\nVaša verzija programa je: " + Properties.Settings.Default.verzija_programa.ToString().Replace(",", ".") + ".\r\nŽelite li skinuti noviju verziju programa.", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    btnNadogradi.PerformClick();
                }
            }
            else
            {
                MessageBox.Show("Trenutno koristite najnoviju inačicu programa.\r\nVaša verzija programa je: " + Properties.Settings.Default.verzija_programa.ToString().Replace(",", ".") + ".", "Update");
            }
        }

        private void btnSpremiWeb_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format(@"UPDATE postavke
SET salji_na_web = '{0}', salji_na_web_ftp = '{1}', salji_na_web_user = '{2}',  salji_na_web_pass = '{3}';", (chbWebActive.Checked ? 1 : 0), txtDomenaWeb.Text, txtUsernameWeb.Text, txtPasswordWeb.Text);
                classSQL.Setings_Update(sql);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnLoadBackup_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtBackupLokacije.Text = folderDlg.SelectedPath;
            }
        }

        private void btnSpremiBackup_Click(object sender, EventArgs e)
        {
            string sql = string.Format(@"UPDATE postavke
SET lokacija_sigurnosne_kopije ='{0}', backup_aktivnost = '{1}';", txtBackupLokacije.Text, (chbBackupAktivnost.Checked ? 1 : 0));
            classSQL.Setings_Update(sql);
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            string pathForPostgresqlDump = string.Format(@"{0}\DBbackup\pg_dump.exe", Environment.CurrentDirectory);
            string sql = string.Format(@"--host {0} --port {5} --username {6} --format custom --blobs --verbose --file {4}{1}\{2}-{3}.backup{4} {4}{2}{4}",
                txtRemoteImeServera.Text,
                txtBackupLokacije.Text,
                cbRemoteNameDatabase.Text,
                DateTime.Now.ToString("yyyy-MM-dd"),
                "\"",
                txtRemotePort.Text,
                txtRemoteUsername.Text);

            System.Diagnostics.Process.Start(pathForPostgresqlDump, sql);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string _path = Path.GetDirectoryName(Application.ExecutablePath) + "\\" + "podrska.exe";
                if (!File.Exists(_path))
                {
                    Util.Download.SkidajPodrsku();
                }
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.WorkingDirectory = _path;
                proc.StartInfo.FileName = _path;
                proc.Start();
            }
            catch
            {
                MessageBox.Show("Spajanje na Code-iT nije uspjelo!", "Upozorenje!");
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            frmPovijestPromjena p = new frmPovijestPromjena();
            p.ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //if (chbOslobodenjePDVa.Checked)
            //{
            //    string sql = "UPDATE postavke SET oslobodenje_pdva='1'";
            //    classSQL.Setings_Update(sql);
            //}
            //else
            //{
            string sql = string.Format("UPDATE postavke SET oslobodenje_pdva = '{0}';", (chbOslobodenjePDVa.Checked ? 1 : 0));
            classSQL.Setings_Update(sql);
            //}
        }

        private void btnPostavke_Click(object sender, EventArgs e)
        {
            sinkronizacija_poslovnica.frmPostavke p = new sinkronizacija_poslovnica.frmPostavke();
            p.ShowDialog();
        }

        private void chbProvjeraSkladista_CheckedChanged(object sender, EventArgs e)
        {
            //if (chbProvjeraSkladista.Checked)
            //{
            //    string sql = "UPDATE postavke SET provjera_stanja='1'";
            //    classSQL.Setings_Update(sql);
            //}
            //else
            //{
            string sql = string.Format("UPDATE postavke SET provjera_stanja = '{0}';", (chbProvjeraSkladista.Checked ? 1 : 0));
            classSQL.Setings_Update(sql);
            //}
        }

        private void chbNemaNaSkl_CheckedChanged(object sender, EventArgs e)
        {
            //if (chbNemaNaSkl.Checked)
            //{
            //    string sql = "UPDATE postavke SET upozori_za_minus='1'";
            //    classSQL.Setings_Update(sql);
            //}
            //else
            //{
            string sql = string.Format("UPDATE postavke SET upozori_za_minus = '{0}';", (chbNemaNaSkl.Checked ? 1 : 0));
            classSQL.Setings_Update(sql);
            //}
        }

        private PCPOS.Util.classFukcijeZaUpravljanjeBazom u = new Util.classFukcijeZaUpravljanjeBazom("MALOPRODAJA", "POS");

        private void btnNovaGodina_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("UPOZORENJE!!!\r\n" +
                "Prije kreiranja nove godine potrebnop je provjeriti dali je baza podataka stavljena na 'METHOD Trust'.\r\n" +
                "Ako mislite da nije tako postavljena prekinite ovu operaciju i provjerite postavke baze!\r\n" +
                "Postupak za ovaj korak je da otvorite pgAdmin->Tools->Server Configuration->pg_hba.config->Method->Trust!\r\n\r\nAko ste sigurni da je sve postavljeno kako treba nastavite sa YES!",
                "UPOZORENJE",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                u.KreirajNovuGodinu();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            u.BackupSvihBaza();
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtPutanjaZaCert.Text = openFileDialog1.FileName;
            }
        }

        private void btnSpremiPromjeneZaFiskal_Click(object sender, EventArgs e)
        {
            btnSpremi_Click(sender, e);
        }

        private void btnPS_Click(object sender, EventArgs e)
        {
            Util.Korisno ko = new Util.Korisno();

            DataTable DTskladista = classSQL.select("SELECT * FROM skladiste WHERE aktivnost='DA'", "skl").Tables[0];

            foreach (DataRow r in DTskladista.Rows)
            {
                DataTable DTroba = ko.VratiKolicinuNaDan(DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"), r["id_skladiste"].ToString());
                string sql = "BEGIN;";
                foreach (DataRow roba in DTroba.Rows)
                {
                    decimal kolicina = 0;
                    string kolici = roba["kolicina"].ToString().Replace(".", ",");
                    kolicina = Convert.ToDecimal(kolici);

                    sql += "UPDATE roba_prodaja SET kolicina='" + kolicina.ToString("#0.000").Replace(".", ",") + "' WHERE sifra='" + roba["sifra"].ToString() + "' AND id_skladiste='" + roba["id_skladiste"].ToString() + "';";
                }

                if (sql.Length > 6)
                {
                    sql += "COMMIT;";
                    classSQL.update(sql);

                    classSQL.update("UPDATE roba_prodaja SET kolicina='0' WHERE sifra IN (SELECT sifra FROM roba WHERE oduzmi='NE');");
                }
            }
            MessageBox.Show("Izvršeno");
        }

        private void cbDucan_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fillCbNaplatnih();
        }

        private void chbProgramskoSkladiste_CheckedChanged(object sender, EventArgs e)
        {
            //if (chbProgramskoSkladiste.Checked)
            //{
            //    string sql = "UPDATE postavke SET skidaj_skladiste_programski='1'";
            //    classSQL.Setings_Update(sql);
            //}
            //else
            //{
            string sql = string.Format("UPDATE postavke SET skidaj_skladiste_programski = '{0}';", (chbProgramskoSkladiste.Checked ? 1 : 0));
            classSQL.Setings_Update(sql);
            //}
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string sql = @"INSERT INTO ulazna_faktura (broj,godina,primatelj_troskovna_opcija,valuta_pokrica,
                        valuta,iznos,iban_platitelja,model_platitelja,iban_primatelja,model_primatelja,
                        poziv_na_broj_primatelja,sifra_namjene,opis_placanja,datum_izvrsenja,id_zaposlenik,oznaka_hitnosti,hub_kreirano,
                        primatelj_sifra,vrsta_naloga,izvor_dokumenta)
                        SELECT broj,godina,'1' AS primatelj_troskovna_opcija,'HRK' as valuta_pokrica,
                        'HRK' as valuta,ukupno,'HR7123400091100043553' AS iban_platitelja,'HR99' as model_platitelja,'HR7123400091100043553' AS iban_primatelja,'HR99' as model_platitelja,
                        '-' AS poziv_na_broj_primatelja,'ANSI' AS sifra_namjene,napomena as opis_placanja,datum_knjizenja AS datum_izvrsenja,'13' AS id_zaposlenik,'0' AS oznaka_hitnosti, '0' AS hub_kreirano,
                        odrediste AS primatelj_sifra,'1' AS vrsta_naloga, '300' AS izvor_dokumenta
                        FROM ufa;
                        UPDATE ulazna_faktura SET primatelj_naziv=
                        (SELECT ime_tvrtke FROM partners WHERE ulazna_faktura.primatelj_sifra=partners.id_partner);";
            classSQL.insert(sql);
        }

        private void txtArhiviraniFajlovi_TextChanged(object sender, EventArgs e)
        {
            if (loaded)
                classSQL.Setings_Update(string.Format("UPDATE postavke SET putanja_za_skenirane_fajlove = '{0}';", txtArhiviraniFajlovi.Text));
        }

        private void chbIspisPartneraAktivnost_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                //if (chbIspisPartneraAktivnost.Checked)
                //{
                //    classSQL.Setings_Update("UPDATE postavke SET ispis_partnera='1'");
                //}
                //else
                //{
                classSQL.Setings_Update(string.Format("UPDATE postavke SET ispis_partnera = '{0}';", (chbIspisPartneraAktivnost.Checked ? 1 : 0)));
                //}
            }
        }

        private void btnPostaviNabavneCijene_Click(object sender, EventArgs e)
        {
            string god = DateTime.Now.Year.ToString();
            string sqlProvjeraNBC = string.Format(@"UPDATE roba_prodaja SET nc=(SELECT ProvjeraNabavneCijene(roba_prodaja.sifra,'{0}-12-31 23:59:59',CAST(roba_prodaja.id_skladiste AS INTEGER)))
                            WHERE roba_prodaja.sifra IN(
	                            SELECT * FROM
	                            (
		                            SELECT sifra_robe as sifra FROM racun_stavke GROUP BY sifra_robe
		                            UNION
		                            SELECT sifra as sifra FROM faktura_stavke GROUP BY sifra
		                            UNION
		                            SELECT sifra as sifra FROM kalkulacija_stavke GROUP BY sifra
                                    UNION
		                            SELECT sifra FROM pocetno GROUP BY sifra
	                            ) koristivo ORDER BY sifra ASC
                            );

                            UPDATE racun_stavke SET nbc=(SELECT ProvjeraNabavneCijene(racun_stavke.sifra_robe,
                            (SELECT datum_racuna FROM racuni WHERE racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa LIMIT 1),
                            CAST(racun_stavke.id_skladiste AS INTEGER)));

                            UPDATE faktura_stavke SET nbc=(SELECT ProvjeraNabavneCijene(faktura_stavke.sifra,
                            (SELECT date as datum FROM fakture WHERE fakture.broj_fakture=faktura_stavke.broj_fakture AND fakture.broj_fakture = faktura_stavke.broj_fakture AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa LIMIT 1),
                            CAST(faktura_stavke.id_skladiste AS INTEGER)));

                            UPDATE meduskladisnica_stavke SET nbc=(SELECT ProvjeraNabavneCijene(meduskladisnica_stavke.sifra,
                            (SELECT datum FROM meduskladisnica WHERE meduskladisnica_stavke.broj=meduskladisnica.broj LIMIT 1),
                            CAST(meduskladisnica_stavke.iz_skladista AS INTEGER)));

                            UPDATE inventura_stavke SET nbc=(SELECT ProvjeraNabavneCijene(inventura_stavke.sifra_robe,
                            (SELECT datum FROM inventura WHERE inventura_stavke.broj_inventure=inventura.broj_inventure LIMIT 1),
                            (SELECT id_skladiste FROM inventura WHERE inventura_stavke.broj_inventure=inventura.broj_inventure LIMIT 1)));

                            UPDATE radni_nalog_stavke SET nbc=(SELECT ProvjeraNabavneCijene(radni_nalog_stavke.sifra_robe,
                            (SELECT datum_naloga FROM radni_nalog WHERE radni_nalog.broj_naloga=radni_nalog_stavke.broj_naloga LIMIT 1),
                            CAST(radni_nalog_stavke.id_skladiste AS INTEGER)));", Util.Korisno.GodinaKojaSeKoristiUbazi);

            classSQL.update(sqlProvjeraNBC);
            MessageBox.Show("Uspješno ažurirane nabavne cijene.");
        }

        private void btnObrisiArtikle_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Dali ste sigurni da želite obrisati sve artikle koje niste koristili u tekučoj godini.",
                "Brisanje artikla.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (MessageBox.Show("Još jednom dali ste sigurni da želite obrisati sve artikle koje niste koristili u tekučoj godini.\r\nPreporuka je da se artikli obrišu krajem 12 mjeseca.",
                "Brisanje artikla.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    #region SQL ZA BRISANJE

                    string sql = @"DELETE FROM roba
                                WHERE sifra NOT IN
                                (
	                                SELECT sifra FROM
	                                (
		                                SELECT sifra FROM pocetno GROUP BY sifra
		                                UNION
		                                SELECT sifra FROM kalkulacija_stavke GROUP BY sifra
		                                UNION
		                                SELECT sifra FROM primka_stavke GROUP BY sifra
		                                UNION
		                                SELECT sifra FROM meduskladisnica_stavke GROUP BY sifra
		                                UNION
		                                SELECT sifra FROM otpis_robe_stavke GROUP BY sifra
		                                UNION
		                                SELECT sifra_robe FROM otpremnica_stavke GROUP BY sifra_robe
		                                UNION
		                                SELECT sifra_robe FROM radni_nalog_stavke GROUP BY sifra_robe
		                                UNION
		                                SELECT sifra FROM ponude_stavke GROUP BY sifra
	                                ) r
                                );

                                DELETE FROM roba_prodaja
                                WHERE sifra NOT IN
                                (
	                                SELECT sifra FROM
	                                (
		                                SELECT sifra FROM pocetno GROUP BY sifra
		                                UNION
		                                SELECT sifra FROM kalkulacija_stavke GROUP BY sifra
		                                UNION
		                                SELECT sifra FROM primka_stavke GROUP BY sifra
		                                UNION
		                                SELECT sifra FROM meduskladisnica_stavke GROUP BY sifra
		                                UNION
		                                SELECT sifra FROM otpis_robe_stavke GROUP BY sifra
		                                UNION
		                                SELECT sifra_robe FROM otpremnica_stavke GROUP BY sifra_robe
		                                UNION
		                                SELECT sifra_robe FROM radni_nalog_stavke GROUP BY sifra_robe
		                                UNION
		                                SELECT sifra FROM ponude_stavke GROUP BY sifra
	                                ) r
                                );";

                    #endregion SQL ZA BRISANJE

                    classSQL.delete(sql);
                    MessageBox.Show("Uspješno obrisano!");
                }
            }
        }

        public void btnBrisiIsteArtikle_Click(object sender, EventArgs e)
        {
            this.Text = "Radim backup baze. Pričekajte";
            Util.Korisno.BackupSvihBaza();
            Thread.Sleep(50000);

            this.Text = "Brišem nepotrebne artikle. Pričekajte";

            string query_delete = "BEGIN;";
            string query = @"SELECT sifra,id_skladiste, COUNT(*) FROM roba_prodaja
	                    GROUP BY sifra, id_skladiste
	                    HAVING COUNT(*)>1 ORDER BY sifra,id_skladiste";
            DataTable DTdupli = classSQL.select(query, "roba").Tables[0];

            foreach (DataRow r in DTdupli.Rows)
            {
                query = "SELECT * FROM roba_prodaja WHERE sifra='" + r["sifra"].ToString() + "' AND id_skladiste='" + r["id_skladiste"].ToString() + "' ORDER BY id_roba_prodaja ASC;";
                DataTable DTroba_prodaja = classSQL.select(query, "roba").Tables[0];
                int brojac = 0;
                foreach (DataRow row in DTroba_prodaja.Rows)
                {
                    if (brojac > 0)
                    {
                        query_delete += "DELETE FROM roba_prodaja WHERE id_roba_prodaja='" + row["id_roba_prodaja"].ToString() + "';";
                    }
                    brojac++;
                }
            }

            query_delete += "COMMIT;";
            classSQL.delete(query_delete);

            try
            {
                classSQL.insert("CREATE UNIQUE INDEX index_roba_prodaja ON roba_prodaja(sifra, id_skladiste);");
                classSQL.select("SELECT setval('roba_prodaja_id_roba_prodaja_seq', max(id_roba_prodaja)) FROM   roba_prodaja;", "");
            }
            catch
            {
            }

            query_delete = "BEGIN;";
            query = @"SELECT sifra_robe,id_skladiste,inventura.broj_inventure, COUNT(*) FROM inventura_stavke
                    LEFT JOIN inventura ON inventura.broj_inventure=inventura_stavke.broj_inventure
                    GROUP BY sifra_robe, id_skladiste,inventura.broj_inventure
                    HAVING COUNT(*)>1 ORDER BY sifra_robe,id_skladiste";
            DTdupli = classSQL.select(query, "roba").Tables[0];

            foreach (DataRow r in DTdupli.Rows)
            {
                query = @"SELECT * FROM inventura_stavke
                        LEFT JOIN inventura ON inventura.broj_inventure=inventura_stavke.broj_inventure
                        WHERE sifra_robe='" + r["sifra_robe"].ToString() + "' AND inventura.id_skladiste='" + r["id_skladiste"].ToString() + "' AND inventura.broj_inventure='" + r["broj_inventure"].ToString() + "' ORDER BY id_stavke ASC;";
                DataTable DTinventura = classSQL.select(query, "roba").Tables[0];
                int brojac = 0;
                foreach (DataRow row in DTinventura.Rows)
                {
                    decimal k = 0, k1;
                    decimal.TryParse(row["kolicina"].ToString(), out k);
                    decimal.TryParse(row["kolicina_koja_je_bila"].ToString(), out k1);

                    if (brojac > 0)
                    {
                        query_delete += "DELETE FROM inventura_stavke WHERE id_stavke = '" + row["id_stavke"].ToString() + "';";
                    }
                    brojac++;
                }
            }

            query_delete += "COMMIT;";
            classSQL.delete(query_delete);

            this.Text = "Postavke";
            MessageBox.Show("Izvršeno");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            classNabavneCijene nc = new classNabavneCijene();
            nc.PostaviNBC();
        }

        private void txtPorezNaDohodak_TextChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                classSQL.Setings_Update(string.Format("UPDATE postavke SET porez_na_dohodak='{0}';", txtPorezNaDohodak.Text.Replace(",", ".")));
            }
        }

        private void chbFiskalizacijaIskljucena_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                try
                {
                    //if (chbFiskalizacijaIskljucena.Checked)
                    //{
                    //    string sql = "UPDATE postavke SET upozori_iskljucenu_fiskalizaciju='1'";
                    //    classSQL.Setings_Update(sql);
                    //}
                    //else
                    //{
                    string sql = string.Format("UPDATE postavke SET upozori_iskljucenu_fiskalizaciju='{0}';", (chbFiskalizacijaIskljucena.Checked ? 1 : 0));
                    classSQL.Setings_Update(sql);
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void txtDomena_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.domena_za_sinkronizaciju = txtDomena.Text;
            Properties.Settings.Default.Save();
        }

        private void txtLozinkaZaWebAktivaciju_TextChanged(object sender, EventArgs e)
        {
            if (txtLozinkaZaWebAktivaciju.Text == "WebSynq1w2e3r4")
            {
                chbAktiviranaWebSyn.Enabled = true;
                btnPosaljiNaWeb.Enabled = true;
                btnBrisiSve.Enabled = true;
                txtDomena.Visible = true;
                label41.Visible = true;
            }
            else
            {
                chbAktiviranaWebSyn.Enabled = false;
                btnPosaljiNaWeb.Enabled = false;
                btnBrisiSve.Enabled = false;
                txtDomena.Visible = false;
                label41.Visible = false;
            }
        }

        private void chbAktiviranaWebSyn_CheckedChanged(object sender, EventArgs e)
        {
            if (chbAktiviranaWebSyn.Checked)
            {
                string sql = "UPDATE postavke SET posalji_dokumente_na_web='1'";
                classSQL.Setings_Update(sql);
            }
            else
            {
                string sql = "UPDATE postavke SET posalji_dokumente_na_web='0'";
                classSQL.Setings_Update(sql);
            }
        }

        private void btnBrisiSve_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Funkcija briše:\r\nSve račune,\r\nSve primke,\r\nSve inventure,\r\nPočetno stanje,\r\nOtpis robe\r\n\r\n\r\n\r\n" +
                "Dali ste sigurni da želite obrisati gore navedene stavke???", "Upozorenje",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (MessageBox.Show("Prije brisanja još jednom potvrdite da želite obrisati gore navedene stavke!!!", "Upozorenje",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string query = "BEGIN;" +
                        "DELETE FROM racuni;" +
                        "DELETE FROM racun_stavke;" +
                        "DELETE FROM primka;" +
                        "DELETE FROM primka_stavke;" +
                        "DELETE FROM povrat_robe;" +
                        "DELETE FROM povrat_robe_stavke;" +
                        "DELETE FROM pocetno;" +
                        "DELETE FROM inventura;" +
                        "DELETE FROM inventura_stavke;" +
                        "COMMIT;";

                    classSQL.insert(query);
                    MessageBox.Show("Obrisano!");
                }
            }
        }

        private void btnPosaljiNaWeb_Click(object sender, EventArgs e)
        {
            synRacuni Racuni = new synRacuni(true);
            synPrimka Primka = new synPrimka(true);
            synKalkulacije1 Kalkulacija = new synKalkulacije1(true);
            synGrupe Grupe = new synGrupe(true);
            synZaposlenici Zaposlenici = new synZaposlenici(true);
            synOtpisRobe OtpisRobe = new synOtpisRobe(true);
            synInventura Inventura = new synInventura(true);
            synPocetnoStanje PocetnoStanje = new synPocetnoStanje(true);
            synPartner Partner = new synPartner(true);
            synArtikli Artikli = new synArtikli(true);
            synRobaProdaja Repromaterijal = new synRobaProdaja(true);
            synFakture Fakture = new synFakture(true);
            synOtpremnice Otprema = new synOtpremnice(true);

            if (Class.Postavke.posaljiDokumenteNaWeb && System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                if (!Util.Korisno.RadimSinkronizaciju)
                {
                    Util.Korisno.RadimSinkronizaciju = true;
                    Racuni.Send();
                    Primka.Send();
                    Kalkulacija.Send();
                    Fakture.Send();
                    Otprema.Send();
                    Grupe.Send();
                    Zaposlenici.Send();
                    Zaposlenici.UzmiPodatkeSaWeba();
                    OtpisRobe.Send();
                    Inventura.Send();
                    PocetnoStanje.Send();
                    Artikli.Send();
                    Repromaterijal.Send();
                    Partner.Send();
                    Util.Korisno.RadimSinkronizaciju = false;
                    MessageBox.Show("Izvršeno");
                }
            }
        }

        private void btnVagaSave_Click(object sender, EventArgs e)
        {
            try
            {
                Korisno.vaga = chkVaga.Checked;
                Korisno.serialPort = cmbSerialPortName.SelectedItem.ToString();
                Korisno.baudRate = Convert.ToInt32(cmbSerialPortBaudRate.SelectedItem);

                string sql = string.Format("UPDATE postavke SET useVaga = '{0}', COMport = '{1}', baudRate = '{2}';", (Korisno.vaga ? 1 : 0), Korisno.serialPort, Korisno.baudRate);
                provjera_sql(classSQL.Setings_Update(sql));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Greška");
            }
        }

        private void btnNadogradiBazu_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Želite nadograditi bazu?", "Nadogradnja tablice u bazi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    classProvjeraBaze.ProvjeraTablica();
                    File.WriteAllText("ProvjeraTablicaBaze" + Util.Korisno.GodinaKojaSeKoristiUbazi, Properties.Settings.Default.verzija_programa.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chbZabraniPromjeneCijena_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                if (chbZabraniPromjeneCijena.Checked)
                    i = 1;

                classSQL.select_settings(string.Format("update postavke set roba_zabrani_mijenjanje_cijena = '{0}'", i.ToString()), "postavke");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCentralaSkladista_Click(object sender, EventArgs e)
        {
            try
            {
                sinkronizacija_poslovnica.frmSkladistaSinkronizacija f = new sinkronizacija_poslovnica.frmSkladistaSinkronizacija();
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtCentralaLozinka_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string cpass = "CentralaSynq1w2e3r4";

                if (txtCentralaLozinka.Text == cpass)
                {
                    centralaSyncShowHide(true);
                }
                else
                {
                    centralaSyncShowHide(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void centralaSyncShowHide(bool b)
        {
            chbCentralaAktivno.Enabled = b;
            chbIsCentrala.Enabled = b;

            lblCentralaPoslovnica.Visible = b;
            txtCentralaPoslovnica.Visible = b;
            btnCentralaSkladista.Visible = b;
            chbCentralaAktivno.Visible = b;
            chbIsCentrala.Visible = b;
        }

        private void chbIsCentrala_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("update postavke set is_centrala = {0};", (chbIsCentrala.Checked ? 1 : 0));
                classSQL.Setings_Update(sql);

                if (this.chbCentralaAktivno.Checked && this.chbIsCentrala.Checked)
                {
                    this.chbCentralaAktivno.Checked = false;
                }

                Util.Korisno.centrala = chbIsCentrala.Checked;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chbCentralaAktivno_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("update postavke set centrala_aktivno = {0};", (chbCentralaAktivno.Checked ? 1 : 0));
                classSQL.Setings_Update(sql);

                if (this.chbCentralaAktivno.Checked && this.chbIsCentrala.Checked)
                {
                    this.chbIsCentrala.Checked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtCentralaPoslovnica_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("update postavke set centrala_poslovnica = '{0}';", txtCentralaPoslovnica.Text.Trim());
                classSQL.Setings_Update(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chbSNBC_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("update postavke set proizvodnja_faktura_nbc = {0};", (chbSNBC.Checked ? 1 : 0));
                classSQL.Setings_Update(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmPostavke_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Class.Postavke.GetPostavke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbVrstaKalkulacije_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("update postavke set idKalkulacija = {0};", cmbVrstaKalkulacije.SelectedValue);
                classSQL.Setings_Update(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chbRucnoUpisivanjeKarticeKupca_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("update postavke set rucnoUpisivanjeKarticeKupca = '{0}';", (chbRucnoUpisivanjeKarticeKupca.Checked ? 1 : 0));
                classSQL.Setings_Update(sql);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void clbKoristiSkladista_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                string s = "";
                for (int i = 0; i < clbKoristiSkladista.Items.Count; i++)
                {
                    if (((e.Index == i ? (e.NewValue == CheckState.Checked ? true : false) : clbKoristiSkladista.GetItemChecked(i))))
                    {
                        if (s.Length > 0)
                            s += ",";

                        s += ((DataRowView)(clbKoristiSkladista.Items[i]))["ID"].ToString();
                    }
                }

                classSQL.Setings_Update(string.Format("update postavke set koristiSkladista = '{0}'", s));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cmbVrstaFakture_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("update postavke set idFaktura = {0};", cmbVrstaFakture.SelectedValue);
                classSQL.Setings_Update(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chbKolicinaUMinus_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                classSQL.select_settings(string.Format("update postavke set kolicina_u_minus = {0}", (chbKolicinaUMinus.Checked ? 1 : 0)), "postavke");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSakrijFormuZaProdajuUMinus_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    string pass = "q1a2y3x4c5";
                    string txtt = txtSakrijFormuZaProdajuUMinus.Text.Trim();
                    if (pass == txtt)
                    {
                        chbSakrijFormuZaProdajuUMinus.Visible = true;
                    }
                    else
                    {
                        chbSakrijFormuZaProdajuUMinus.Visible = false;
                    }
                }
                else
                {
                    chbSakrijFormuZaProdajuUMinus.Visible = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void chbSakrijFormuZaProdajuUMinus_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                classSQL.select_settings(string.Format("update postavke set sakrij_formu_za_prodaju_u_minus = {0}", (chbSakrijFormuZaProdajuUMinus.Checked ? 1 : 0)), "postavke");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void chbTestFisklaizacija_Click(object sender, EventArgs e)
        {
            try
            {
                string password = "test_q1w2e3r4";

                frmInputPassword f = new frmInputPassword();
                f.Text = "TEST FISKALIZACIJA";
                if (f.ShowDialog() == DialogResult.OK && f.password == password)
                {
                    chbTestFisklaizacija.Checked = !chbTestFisklaizacija.Checked;
                    classSQL.select_settings(string.Format("update postavke set test_fiskalizacija = {0}", (chbTestFisklaizacija.Checked ? 1 : 0)), "postavke");
                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void chbPovratnaNaknada_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                classSQL.select_settings(string.Format("update postavke set koristi_povratnu_naknadu = {0}", (chbPovratnaNaknada.Checked ? 1 : 0)), "postavke");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void chbAutomatskiZapisnik_CheckedChanged(object sender, EventArgs e)
        {
            classSQL.select_settings(string.Format("update postavke set automatski_zapisnik = {0}", (chbAutomatskiZapisnik.Checked ? 1 : 0)), "postavke");
        }

        private void chbUzmiAvanseUPrometKase_CheckedChanged(object sender, EventArgs e)
        {
            classSQL.select_settings(string.Format("update postavke set uzmi_avanse_u_promet_kase_POS = {0}", (chbUzmiAvanseUPrometKase.Checked ? 1 : 0)), "postavke");
        }

        private void chbMedjuskladisnicaSProizvodjackomCijenom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("update postavke set proizvodnja_meduskladisnica_pc = {0};", (chbMedjuskladisnicaSProizvodjackomCijenom.Checked ? 1 : 0));
                classSQL.Setings_Update(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chbMainFormControlBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("update postavke set control_box = {0};", (chbMainFormControlBox.Checked ? 1 : 0));
                classSQL.Setings_Update(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chbMaloprodajaNaplataGotovinaButtonShow_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("update postavke set maloprodaja_naplata_gotovina_button_show = {0};", (chbMaloprodajaNaplataGotovinaButtonShow.Checked ? 1 : 0));
                classSQL.Setings_Update(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chbMaloprodajaNaplataKarticaButtonShow_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("update postavke set maloprodaja_naplata_kartica_button_show = {0};", (chbMaloprodajaNaplataKarticaButtonShow.Checked ? 1 : 0));
                classSQL.Setings_Update(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chbProdajaAutomobila_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("update postavke set prodaja_automobila = {0};", (chbProdajaAutomobila.Checked ? 1 : 0));
                classSQL.Setings_Update(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chbNormativSProizvodjackomCijenom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("update postavke set proizvodnja_normativ_pc = {0};", (chbNormativSProizvodjackomCijenom.Checked ? 1 : 0));
                classSQL.Setings_Update(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chbUzmiRabatUOdjaviKomisije_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("update postavke set uzmi_rabat_u_odjavi_komisije = {0};", (chbUzmiRabatUOdjaviKomisije.Checked ? 1 : 0));
                classSQL.Setings_Update(sql);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void chbUseUdsGame_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("update postavke set useUdsGame = {0};", (chbUseUdsGame.Checked ? 1 : 0));
                classSQL.Setings_Update(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chbUseEmployees_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("update postavke set useUdsGameEmployees = {0};", (chbUseEmployees.Checked ? 1 : 0));
                classSQL.Setings_Update(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtApiKey_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("update postavke set useUdsGameApiKey = '{0}';", txtApiKey.Text);
                classSQL.Setings_Update(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSendRequest_Click(object sender, EventArgs e)
        {
            string token = Minimax.Minimax.GetApiAccessToken();
            string url = "api/currentuser/orgs";
            HttpStatusCode code;
            string result;

            if (Minimax.Minimax.GetApiResultContent(url, token, out code, out result))
            {
                var jObj = JObject.Parse(result);
                var props = jObj["Rows"][0]["Organisation"].ToString();
                Organisation org = JsonConvert.DeserializeObject<Organisation>(props);

                mMApiFkField country = new mMApiFkField
                {
                    ID = 95,
                    Name = "HR",
                    ResourceUrl = "/api/orgs/4948/countries/95"
                };

                mMApiFkField currency = new mMApiFkField
                {
                    ID = 9,
                    Name = "HRK",
                    ResourceUrl = "/api/orgs/4948/currencies/9"
                };

                Customer customer = new Customer
                {
                    Address = "Glavna 29",
                    City = "PRELOG",
                    Code = "",
                    Country = country,
                    CountryName = "",
                    Currency = currency,
                    EInvoiceIssuing = "N",
                    InternalCustomerNumber = "",
                    ExpirationDays = 15,
                    Name = "Code-iT 2",
                    PostalCode = "40323",
                    RebatePercent = 0,
                    RecordDtModified = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
                    RegistrationNumber = "",
                    RowVersion = "",
                    SubjectToVAT = "D",
                    TaxNumber = "42584526821",
                    Usage = "D",
                    VATIdentificationNumber = "42584526821",
                    WebSiteURL = ""
                };

                var request = Minimax.Minimax.SendRequest($"api/orgs/{org.OrganisationId}/customers", token, customer);
                if (request == HttpStatusCode.Created)
                    MessageBox.Show("Uspješno.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateRoba()
        {
            string query = $@"SELECT roba.naziv,
                                roba.sifra,
                                CASE roba.oduzmi 
                                    WHEN 'DA' THEN 'B'
                                    ELSE 'S' END AS tip,
                                roba.jm,
                                roba.vpc,
                                roba.porez
                            FROM roba;";
            DataTable DTroba = classSQL.select(query, "roba").Tables[0];

            if (DTroba.Rows.Count > 0)
            {
                Organisation organisation = Minimax.Minimax.CurrentUserOrganisation();
                List<Item> itemList = Minimax.Minimax.GetItems();

                if (DTroba.Rows.Count > 0)
                {
                    foreach (DataRow row in DTroba.Rows)
                    {
                        bool exists = itemList.Any(it => it.Code.ToString().ToUpper() == row["sifra"].ToString().ToUpper());
                        if (!exists)
                        {
                            int idDomestic = row["tip"].ToString() == "B" ? 7600 : 7510;
                            int idEU = row["tip"].ToString() == "B" ? 7610 : 7540;
                            int idOutsideEU = row["tip"].ToString() == "B" ? 7610 : 7540;
                            int idStocksAccount = row["tip"].ToString() == "B" ? 6500 : 0;

                            string vatRateCode = GetVatRateCode(Convert.ToDecimal(row["porez"].ToString()));
                            VatRate vat = Minimax.Minimax.GetVatRate(organisation.OrganisationId, vatRateCode);
                            mMApiFkField vatRate = new mMApiFkField
                            {
                                ID = vat.VatRateId,
                                Name = vat.Code,
                                ResourceUrl = $"/api/orgs/{organisation.OrganisationId}/vatrates/{vat.VatRateId}"
                            };

                            Currency cur = Minimax.Minimax.GetCurrencyByCode(organisation.OrganisationId, cbCurrency.SelectedValue.ToString());
                            mMApiFkField currency = new mMApiFkField
                            {
                                ID = cur.CurrencyId,
                                Name = cur.Code,
                                ResourceUrl = $"/api/orgs/{organisation.OrganisationId}/currencies/{cur.CurrencyId}"
                            };

                            Account accDomestic = Minimax.Minimax.GetAccountByCode(organisation.OrganisationId, idDomestic.ToString());
                            mMApiFkField revenueAccountDomestic = new mMApiFkField
                            {
                                ID = accDomestic.AccountId,
                                Name = accDomestic.Name,
                                ResourceUrl = $"/api/orgs/{organisation.OrganisationId}/accounts/{accDomestic.AccountId}"
                            };

                            Account accEU = Minimax.Minimax.GetAccountByCode(organisation.OrganisationId, idEU.ToString());
                            mMApiFkField revenueAccountEU = new mMApiFkField
                            {
                                ID = accEU.AccountId,
                                Name = accEU.Name,
                                ResourceUrl = $"/api/orgs/{organisation.OrganisationId}/accounts/{accEU.AccountId}"
                            };

                            Account accOutsideEU = Minimax.Minimax.GetAccountByCode(organisation.OrganisationId, idOutsideEU.ToString());
                            mMApiFkField revenueAccountOutsideEU = new mMApiFkField
                            {
                                ID = accOutsideEU.AccountId,
                                Name = accOutsideEU.Name,
                                ResourceUrl = $"/api/orgs/{organisation.OrganisationId}/accounts/{accOutsideEU.AccountId}"
                            };

                            mMApiFkField stocksAccount = null;
                            if (idStocksAccount != 0)
                            {
                                Account accStocks = Minimax.Minimax.GetAccountByCode(organisation.OrganisationId, idStocksAccount.ToString());
                                stocksAccount = new mMApiFkField
                                {
                                    ID = accStocks.AccountId,
                                    Name = accStocks.Name,
                                    ResourceUrl = $"/api/orgs/{organisation.OrganisationId}/accounts/{accStocks.AccountId}"
                                };
                            }

                            Item item = new Item
                            {
                                Name = row["naziv"].ToString(),
                                Code = row["sifra"].ToString(),
                                EANCode = "",
                                Description = "",
                                ItemType = row["tip"].ToString(),
                                UnitOfMeasurement = row["jm"].ToString(),
                                VatRate = vatRate,
                                Price = Convert.ToDecimal(row["vpc"].ToString()),
                                RebatePercent = 0,
                                Usage = "D",
                                Currency = currency,
                                SerialNumbers = "D",
                                BatchNumbers = "D",
                                RevenueAccountDomestic = revenueAccountDomestic,
                                RevenueAccountEU = revenueAccountEU,
                                RevenueAccountOutsideEU = revenueAccountOutsideEU,
                                StocksAccount = stocksAccount,
                                RecordDtModified = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")
                            };

                            var requestResult = Minimax.Minimax.SendRequest($"api/orgs/{organisation.OrganisationId}/items", Minimax.Minimax.GetApiAccessToken(), item);
                            if (requestResult != HttpStatusCode.Created)
                            {
                                MessageBox.Show(row["id_partner"].ToString());
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdatePartners()
        {
            string query = $@"SELECT partners.id_partner,
                                partners.adresa,
	                            grad.grad,
	                            partners.odgoda_placanja_u_danima,
	                            partners.ime_tvrtke,
	                            grad.posta,
	                            partners.popust,
	                            partners.oib
                            FROM partners
                            LEFT JOIN grad ON partners.id_grad = grad.id_grad";
            DataTable DTpartners = classSQL.select(query, "partners").Tables[0];

            if (DTpartners.Rows.Count > 0)
            {
                string token = Minimax.Minimax.GetApiAccessToken();
                string currentUserUrl = "api/currentuser/orgs";
                HttpStatusCode code;
                string result;

                if (Minimax.Minimax.GetApiResultContent(currentUserUrl, token, out code, out result))
                {
                    var jObj = JObject.Parse(result);
                    var props = jObj["Rows"][0]["Organisation"].ToString();
                    Organisation org = JsonConvert.DeserializeObject<Organisation>(props);
                    List<Customer> customerList = Minimax.Minimax.GetCustomers();

                    foreach (DataRow row in DTpartners.Rows)
                    {
                        bool exists = customerList.Any(it => it.TaxNumber.ToString().Trim() == row["oib"].ToString().Trim());
                        if (!exists)
                        {
                            mMApiFkField country = new mMApiFkField
                            {
                                ID = 95,
                                Name = "HR",
                                ResourceUrl = $"/api/orgs/{org.OrganisationId}/countries/95"
                            };

                            mMApiFkField currency = new mMApiFkField
                            {
                                ID = 9,
                                Name = "HRK",
                                ResourceUrl = $"/api/orgs/{org.OrganisationId}/currencies/9"
                            };

                            Customer customer = new Customer
                            {
                                Address = row["adresa"].ToString(),
                                City = row["grad"].ToString(),
                                Code = row["id_partner"].ToString(),
                                Country = country,
                                CountryName = "",
                                Currency = currency,
                                EInvoiceIssuing = "N",
                                InternalCustomerNumber = "",
                                ExpirationDays = Convert.ToInt32(row["odgoda_placanja_u_danima"].ToString()),
                                Name = row["ime_tvrtke"].ToString(),
                                PostalCode = row["posta"].ToString(),
                                RebatePercent = Convert.ToDecimal(row["popust"].ToString()),
                                RecordDtModified = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
                                RegistrationNumber = "",
                                RowVersion = "",
                                SubjectToVAT = "D",
                                TaxNumber = row["oib"].ToString().Trim(),
                                Usage = "D",
                                VATIdentificationNumber = row["oib"].ToString().Trim(),
                                WebSiteURL = ""
                            };

                            var requestResult = Minimax.Minimax.SendRequest($"api/orgs/{org.OrganisationId}/customers", token, customer);
                            if (requestResult != HttpStatusCode.Created)
                            {
                                MessageBox.Show(row["id_partner"].ToString());
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Method used to send IssuedInvoices (fakture)
        /// </summary>
        private void SendInvoices()
        {
            string query = $@"SELECT fakture.broj_fakture,
                            fakture.date,
	                        fakture.datedvo,
	                        fakture.datum_valute,
                            fakture.napomena,
                            fakture.tecaj,
	                        partners.ime_tvrtke,
	                        partners.adresa,
                            partners.oib,
	                        grad.grad,
	                        grad.posta,
	                        valute.naziv AS kod_valute,
	                        zemlja.zemlja,
	                        zemlja.country_code
                        FROM fakture
                        LEFT JOIN valute ON fakture.id_valuta = valute.id_valuta
                        LEFT JOIN partners ON fakture.id_odrediste = partners.id_partner
                        LEFT JOIN grad ON grad.id_grad = partners.id_grad
                        LEFT JOIN zemlja ON zemlja.id_zemlja = grad.drzava
                        WHERE fakture.broj_fakture > 655
                        ORDER BY fakture.broj_fakture ASC";
            DataTable DTfaktura = classSQL.select(query, "fakture").Tables[0];

            if (DTfaktura.Rows.Count > 0)
            {
                List<Customer> Customers = Minimax.Minimax.GetCustomers();

                foreach (DataRow row in DTfaktura.Rows)
                {
                    Organisation organisation = Minimax.Minimax.CurrentUserOrganisation();

                    Country country = Minimax.Minimax.GetCountryByCode(organisation.OrganisationId, row["country_code"].ToString());
                    mMApiFkField mmCountry = new mMApiFkField
                    {
                        ID = country.CountryId,
                        Name = country.Name,
                        ResourceUrl = $"api/orgs/{organisation.OrganisationId}/countries/{country.CountryId}"
                    };

                    string drzavaPrimatelja = null;
                    if (row["country_code"].ToString() != "HR")
                        drzavaPrimatelja = mmCountry.Name;

                    //Customer customer = Minimax.Minimax.GetCustomerById(organisation.OrganisationId, 369533);
                    Customer customer = Customers.FirstOrDefault(it => it.TaxNumber == row["oib"].ToString());
                    mMApiFkField mmCustomer = new mMApiFkField
                    {
                        ID = customer.CustomerId,
                        Name = customer.Name,
                        ResourceUrl = $"/api/orgs/{organisation.OrganisationId}/customers/{customer.CustomerId}"
                    };

                    Currency currency = Minimax.Minimax.GetCurrencyByCode(organisation.OrganisationId, row["kod_valute"].ToString());
                    mMApiFkField mmCurrency = new mMApiFkField
                    {
                        ID = currency.CurrencyId,
                        Name = currency.Name,
                        ResourceUrl = $"/api/orgs/{organisation.OrganisationId}/currencies/{currency.CurrencyId}"
                    };

                    Employee employee = Minimax.Minimax.GetEmployeeById(organisation.OrganisationId, 12988);
                    mMApiFkField mmEmployee = new mMApiFkField
                    {
                        ID = employee.EmployeeId,
                        Name = $"{employee.FirstName} {employee.LastName}",
                        ResourceUrl = $"/api/orgs/{organisation.OrganisationId}/employees/{employee.EmployeeId}"
                    };

                    ReportTemplate reportTemplate = Minimax.Minimax.GetReportTemplateById(organisation.OrganisationId, 77365);
                    mMApiFkField mmReportTemplate = new mMApiFkField
                    {
                        ID = reportTemplate.ReportTemplateId,
                        Name = reportTemplate.Name,
                        ResourceUrl = $"api/orgs/{organisation.OrganisationId}/report-templates/{reportTemplate.ReportTemplateId}"
                    };

                    ReportTemplate deliveryReportTemplate = Minimax.Minimax.GetReportTemplateById(organisation.OrganisationId, 77360);
                    mMApiFkField mmDeliveryReportTemplate = new mMApiFkField
                    {
                        ID = deliveryReportTemplate.ReportTemplateId,
                        Name = deliveryReportTemplate.Name,
                        ResourceUrl = $"api/orgs/{organisation.OrganisationId}/report-templates/{deliveryReportTemplate.ReportTemplateId}"
                    };

                    IssuedInvoice issuedInvoice = new IssuedInvoice
                    {
                        Year = Convert.ToInt32(DateTime.Parse(row["date"].ToString()).ToString("yyyy")),
                        InvoiceNumber = Convert.ToInt64(row["broj_fakture"].ToString()),
                        Numbering = null,
                        DocumentNumbering = null,
                        Customer = mmCustomer,
                        DateIssued = DateTime.Parse(row["date"].ToString()).ToString("yyyy-MM-ddTHH:mm:ss"),
                        DateTransactionFrom = DateTime.Parse(row["datedvo"].ToString()).ToString("yyyy-MM-ddTHH:mm:ss"),
                        DateTransaction = DateTime.Parse(row["datedvo"].ToString()).ToString("yyyy-MM-ddTHH:mm:ss"),
                        DateDue = DateTime.Parse(row["datum_valute"].ToString()).ToString("yyyy-MM-ddTHH:mm:ss"),
                        AddresseeName = row["ime_tvrtke"].ToString(),
                        AddresseeAddress = row["adresa"].ToString(),
                        AddresseePostalCode = row["posta"].ToString(),
                        AddresseeCity = row["grad"].ToString(),
                        AddresseeCountryName = drzavaPrimatelja,
                        AddresseeCountry = mmCountry,
                        RecipientName = row["ime_tvrtke"].ToString(),
                        RecipientAddress = row["adresa"].ToString(),
                        RecipientPostalCode = row["posta"].ToString(),
                        RecipientCity = row["grad"].ToString(),
                        RecipientCountryName = drzavaPrimatelja,
                        RecipientCountry = mmCountry,
                        Rabate = 0,
                        ExchangeRate = 0,
                        DocumentReference = null,
                        PaymentReference = null,
                        Currency = mmCurrency,
                        Analytic = null,
                        Document = null,
                        IssuedInvoiceReportTemplate = mmReportTemplate,
                        DeliveryNoteReportTemplate = mmDeliveryReportTemplate,
                        Status = "I",
                        DescriptionAbove = null,
                        DescriptionBelow = null,
                        Notes = row["napomena"].ToString(),
                        PaymentType = "T",
                        Employee = mmEmployee,
                        PricesOnInvoice = "N",
                        RecurringInvoice = "N",
                        SalesValue = 0,
                        SalesValueVAT = 0,
                        InvoiceAttachment = null,
                        EInvoiceAttachment = null,
                        InvoiceType = "R",
                        PaymentStatus = null,
                        InvoiceValue = 0,
                        PaidValue = 0,
                        IssuedInvoiceRows = GetIssuedInvoiceRows(Convert.ToInt32(row["broj_fakture"].ToString()), organisation.OrganisationId, Convert.ToDecimal(row["tecaj"].ToString())),
                        RecordDtModified = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
                        RowVersion = ""
                    };
                    var requestResult = Minimax.Minimax.SendRequest($"api/orgs/{organisation.OrganisationId}/issuedinvoices", Minimax.Minimax.GetApiAccessToken(), issuedInvoice);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="issuedInvoice"></param>
        /// <param name="fakturaId"></param>
        private List<IssuedInvoiceRow> GetIssuedInvoiceRows(int fakturaId, int orgId, decimal currency)
        {
            List<IssuedInvoiceRow> InvoiceRows = new List<IssuedInvoiceRow>();

            string query = $@"SELECT faktura_stavke.sifra,
                                roba.naziv,
                                roba.jm,
                                faktura_stavke.vpc,
                                faktura_stavke.kolicina,
                                faktura_stavke.porez,
                                faktura_stavke.rabat,
                                faktura_stavke.ukupno_rabat
                            FROM faktura_stavke
                            LEFT JOIN roba ON roba.sifra = faktura_stavke.sifra
                            WHERE faktura_stavke.broj_fakture = {fakturaId}";
            DataTable DTfakturaStavke = classSQL.select(query, "faktura_stavke").Tables[0];

            if (DTfakturaStavke.Rows.Count > 0)
            {
                List<Item> Items = Minimax.Minimax.GetItems();
                int rowNumber = 1;
                foreach (DataRow row in DTfakturaStavke.Rows)
                {
                    Item item = Items.FirstOrDefault(it => it.Code == row["sifra"].ToString());
                    if(item != null)
                    {
                        decimal kolicina = Convert.ToDecimal(row["kolicina"].ToString());
                        decimal vpc = Convert.ToDecimal(row["vpc"].ToString()) / currency;
                        decimal porez = Convert.ToDecimal(row["porez"].ToString());
                        if (vpc < 0)
                        {
                            vpc = Math.Abs(vpc);
                            kolicina *= -1;
                        }

                        mMApiFkField mmItem = new mMApiFkField
                        {
                            ID = item.ItemId,
                            Name = item.Name,
                            ResourceUrl = $"/api/orgs/{orgId}/items/{item.ItemId}"
                        };

                        string vatRateCode = GetVatRateCode(Convert.ToDecimal(row["porez"].ToString()));
                        VatRate vat = Minimax.Minimax.GetVatRate(orgId, vatRateCode);
                        mMApiFkField mmVatRate = new mMApiFkField
                        {
                            ID = vat.VatRateId,
                            Name = vat.Code,
                            ResourceUrl = $"/api/orgs/{orgId}/vatrates/{vat.VatRateId}"
                        };

                        IssuedInvoiceRow issuedInvoiceRow = new IssuedInvoiceRow
                        {
                            IssuedInvoice = null,
                            Item = mmItem,
                            ItemName = mmItem.Name,
                            RowNumber = rowNumber,
                            ItemCode = row["sifra"].ToString(),
                            SerialNumber = "",
                            BatchNumber = "",
                            Description = "",
                            Quantity = kolicina,
                            UnitOfMeasurement = row["jm"].ToString(),
                            Price = vpc,
                            PriceWithVAT = Math.Abs((vpc * kolicina) * (1 + (porez / 100))),
                            VATPercent = porez,
                            Discount = 0,
                            DiscountPercent = Convert.ToDecimal(row["rabat"].ToString()),
                            Value = (vpc * kolicina) * (1 + (porez / 100)),
                            VatRate = mmVatRate,
                            Warehouse = null,
                            VatAccountingType = "NOP",
                            RecordDtModified = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
                            RowVersion = ""
                        };
                        InvoiceRows.Add(issuedInvoiceRow);
                        rowNumber++;
                    }
                }
            }
            return InvoiceRows;
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateIssuedInvoiceStatus()
        {
            List<IssuedInvoice> Invoices = Minimax.Minimax.GetIssuedInvoices().Where(it => it.Status == "O").ToList();
            if(Invoices?.Count > 0)
            {
                Organisation organisation = Minimax.Minimax.CurrentUserOrganisation();
                foreach (IssuedInvoice item in Invoices)
                {
                    Minimax.Minimax.CustomActionIssuedInvoice(organisation.OrganisationId, item.IssuedInvoiceId, item.RowVersion);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void FillCurrencyCB()
        {
            DataTable DTSK = new DataTable();

            DTSK.Columns.Add("id", typeof(string));
            DTSK.Columns.Add("naziv", typeof(string));

            DTSK.Rows.Add("HRK", "HRK");
            DTSK.Rows.Add("EUR", "EUR");

            cbCurrency.DataSource = DTSK;
            cbCurrency.DisplayMember = "naziv";
            cbCurrency.ValueMember = "id";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        private string GetVatRateCode(decimal amount)
        {
            string result = "";
            switch (amount)
            {
                case 25:
                    result = "S";
                    break;
                case 13:
                    result = "Z";
                    break;
                case 5:
                    result = "0";
                    break;
                case 0:
                    result = "N";
                    break;
            }
            return result;
        }

        private void btnUpdatePartners_Click(object sender, EventArgs e)
        {
            UpdatePartners();
        }

        private void btnUpdateRoba_Click(object sender, EventArgs e)
        {
            UpdateRoba();
        }

        private void btnSendInvoice_Click(object sender, EventArgs e)
        {
            SendInvoices();
        }

        private void BtnUpdateStatus_Click(object sender, EventArgs e)
        {
            UpdateIssuedInvoiceStatus();
        }

        public string GetIdMaloprodaje()
        {
            return cbKasa.SelectedValue.ToString();
            
        }

    }
}