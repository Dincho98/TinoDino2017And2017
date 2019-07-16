using PCPOS.Resort;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PCPOS
{
    public partial class frmMenu : Form
    {
        private Util.classFukcijeZaUpravljanjeBazom baza = new Util.classFukcijeZaUpravljanjeBazom("MALOPRODAJA", "POS");

        public frmMenu()
        {
            CheckConnection();
            baza.ProvjeriDaliTrebaKreiratiNovuGodinu();
            Util.Korisno.GodinaKojaSeKoristiUbazi = baza.UzmiGodinuKojaSeKoristi();
            InitializeComponent();
        }

        private bool kartoteka = true;
        public bool zatvori = false;
        private INIFile ini = new INIFile();
        private DataTable DTpostavke;
        private static DataTable DTfis = classSQL.select_settings("SELECT * FROM fiskalizacija", "fiskalizacija").Tables[0];
        private string put_za_stavke = "";
        private string put_za_stavkeWeb = "";

        private void Form1_Load(object sender, EventArgs e)
        {
            Class.PodaciTvrtka.GetPodaciTvrtke();
            Properties.Settings.Default.verzija_programa = 2.892m;
            Util.Korisno.RadimSinkronizaciju = false;
            this.Text = Class.PodaciTvrtka.nazivTvrtke;
            CultureInfo before = System.Threading.Thread.CurrentThread.CurrentCulture;
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("hr-HR");
                CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator = ",";
            }
            finally
            {
            }

            #region PROVJERAVAM DALI TREBAM PROVJERITI VERZIJU PROGRAMA I NADOGRADITI TABLICE

            string gggodina = Util.Korisno.GodinaKojaSeKoristiUbazi.ToString();
            if (!File.Exists("ProvjeraTablicaBaze" + gggodina))
                File.WriteAllText("ProvjeraTablicaBaze" + gggodina, "0");

            string verzijaZadnjeProvjereBaze = File.ReadAllText("ProvjeraTablicaBaze" + gggodina);
            decimal verzijaZadnjeProvjereBazeFile = 0, verzijaZadnjeProvjereBazeCurent = 0;
            decimal.TryParse(verzijaZadnjeProvjereBaze, out verzijaZadnjeProvjereBazeFile);
            decimal.TryParse(Properties.Settings.Default.verzija_programa.ToString(), out verzijaZadnjeProvjereBazeCurent);

            if (verzijaZadnjeProvjereBazeFile < verzijaZadnjeProvjereBazeCurent)
            {
                ProvjeraPostGressFunkcija pgSQL = new ProvjeraPostGressFunkcija();
                pgSQL.ProvjeraDaliPostojiFunkcijaProvjeraNabavnihCijena();
                classProvjeraBaze.ProvjeraTablica();
                File.WriteAllText("ProvjeraTablicaBaze" + gggodina, Properties.Settings.Default.verzija_programa.ToString());
            }

            //for (int i = 0; i < 100; i++)
            //{
            //    Console.WriteLine(i);
            //}


            Class.Postavke.GetPostavke();
            Class.Registracija.GetRegistracija();
            Class.PodaciTvrtka.GetPodaciTvrtke();
            Class.PosPrint.GetPosPrint();
            Class.Dokumenti.GetDokumenti();
            Util.Korisno.VratiDucanIBlagajnu();

            //if (Class.Postavke.ControlBox) {
            this.ControlBox = Class.Postavke.controlBox;
            //}

            if (Class.Postavke.proizvodnja_normativ_pc)
            {
                //otpremnicaKomisijaToolStripMenuItem.Visible = true;
            }

            #endregion PROVJERAVAM DALI TREBAM PROVJERITI VERZIJU PROGRAMA I NADOGRADITI TABLICE

            DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

            if (DTpostavke == null || DTpostavke.Rows.Count == 0)
            {
                Application.Exit();
            }

            Util.Korisno kor = new Util.Korisno();
            kor.ProvjeriNadogradnjuPremaOibu(Class.PodaciTvrtka.oibTvrtke);

            Util.Korisno.centrala = Convert.ToBoolean(DTpostavke.Rows[0]["is_centrala"].ToString());

            stvori_sliku();

            string testYes = File.Exists("FiskalTest") ? "1" : "0";
            if (testYes == "1")
            {
                File.Delete("FiskalTest");
            }
            Sinkronizacija.classProvjeraTablica.Provjera();
            if (!File.Exists("log.txt"))
            {
                File.WriteAllText("log.txt", "");
            }

            string odrzavanje = "";
            if (ini.Read("POSTAVKE", "odrzavanje") != "")
            {
                odrzavanje = ini.Read("POSTAVKE", "odrzavanje");
                if (odrzavanje == "1")
                {
                    održavanjeToolStripMenuItem.Visible = true;
                    održavanjeProvjeraToolStripMenuItem.Visible = true;
                }
                else
                {
                    održavanjeToolStripMenuItem.Visible = false;
                    održavanjeProvjeraToolStripMenuItem.Visible = false;
                }
            }

            if (DTpostavke.Rows[0]["upozori_iskljucenu_fiskalizaciju"].ToString() == "1" && DTfis.Rows[0]["aktivna"].ToString() == "0")
            {
                MessageBox.Show("Molimo obratite pozornost na aktivnost fiskalizacije.\r\nU postavkama fiskalizacije fiskalizacija je isključena.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //if (DateTime.Now.Year > 2014 && DTpostavke.Rows[0]["skidaj_skladiste_programski"].ToString() != "1")
            //classSQL.Setings_Update("UPDATE postavke SET skidaj_skladiste_programski='1'");

            KojiServer();

            Util.ProvjeraDll.ZamijeniDll();

            if (File.Exists("code"))
            {
                File.Delete("code");
            }

            string[] machineNames = new string[] { "POWER-RAC", "DEJANVIBOVIC", "PCI3" };

            if (Array.IndexOf(machineNames, System.Environment.MachineName) < 0)
            {
                if (!File.Exists("code") || !File.Exists("name"))
                {
                    List<string> keys = new List<string>();
                    string uniqId = Class.Registracija.getUniqueID("C");
                    string s = Class.Registracija.GetMD5(uniqId + "5AR" + (Class.Registracija.broj == 0 ? "" : Class.Registracija.broj.ToString())).ToUpper();
                    string ns = s.Substring(0, 8) + "-" + s.Substring(8, 4) + "-" + s.Substring(12, 4) + "-" + s.Substring(16, 4) + "-" + s.Substring(20);

                    if (Util.Korisno.CheckForInternetConnection() && Util.Korisno.CheckForInternetConnection(Properties.Settings.Default.PC_POS_wsSoftKontrol_wsSoftKontrol.ToString()))
                    {
                        if (Class.Registracija.productKey.Length == 0 || Class.Registracija.activationCode.Length == 0 || (Class.Registracija.productKey.Length > 0 && Class.Registracija.productKey != ns))
                        {
                            int newBroj = 0;
                            using (var ws = new wsSoftKontrol.wsSoftKontrol())
                            {
                                if (Class.Registracija.productKey.Length > 0 && Class.Registracija.productKey != ns)
                                {
                                    s = Class.Registracija.GetMD5(uniqId + "5AR").ToUpper();
                                    ns = s.Substring(0, 8) + "-" + s.Substring(8, 4) + "-" + s.Substring(12, 4) + "-" + s.Substring(16, 4) + "-" + s.Substring(20);
                                }

                                keys.Add(ns);
                                if (ws.checkIfProductKeyExists(ns))
                                {
                                    for (int i = 1; i < 100; i++)
                                    {
                                        s = Class.Registracija.GetMD5(uniqId + "5AR" + i.ToString()).ToUpper();
                                        ns = s.Substring(0, 8) + "-" + s.Substring(8, 4) + "-" + s.Substring(12, 4) + "-" + s.Substring(16, 4) + "-" + s.Substring(20);
                                        keys.Add(ns);
                                        if (!ws.checkIfProductKeyExists(ns))
                                        {
                                            newBroj = i;
                                            break;
                                        }
                                    }
                                }
                            }

                            frmRegistracija2017 CR = new frmRegistracija2017();
                            CR.broj = newBroj;
                            CR.productKey = ns;
                            CR.MainForm = this;
                            CR.ShowDialog();
                        }
                        else
                        {
                            using (var ws = new wsSoftKontrol.wsSoftKontrol())
                            {
                                if (!ws.checkIfProductKeyExists(ns))
                                {
                                    string sql = "update registracija set productKey = '', activationCode = '', broj = 0;";
                                    classSQL.Setings_Update(sql);
                                    Application.Restart();
                                }
                            }
                        }
                    }
                }
            }

            provjeraStart();

            provjeri_filove_za_backup();

            foreach (Control c in this.Controls)
            {
                if (c is MdiClient)
                    c.BackColor = System.Drawing.Color.FromArgb(((byte)(40)), ((byte)(109)), ((byte)(135)));
            }

            //OstaleFunkcije.DSaktivnosDok = classSQL.select_settings("SELECT * FROM aktivnost_podataka", "aktivnost_podataka").Tables[0];

            EnableDisableDocuments();

            //tu treba provjeriti tečaj
            try
            {
                WebRequest reguest = WebRequest.Create("http://www.pbz.hr/Downloads/HNBteclist.xml");

                DataSet DSpodaci = new DataSet();
                WebResponse response = reguest.GetResponse();
                Stream stream = response.GetResponseStream();

                StreamReader sr = new StreamReader(stream);

                DSpodaci.ReadXml(sr);
                sr.Close();
                stream.Close();

                foreach (DataRow dRow in DSpodaci.Tables[1].Rows)
                {
                    string sifra = dRow["Code"].ToString();
                    string srednji_tecaj = dRow["MeanRate"].ToString();
                    string sql_azuriraj_val = "Update valute Set tecaj = '" + srednji_tecaj + "' WHERE sifra = '" + sifra + "'";
                    classSQL.update(sql_azuriraj_val);
                }
            }
            catch (Exception ex)
            {
            }

            //end provjera tecaja

            try
            {
                frmScren sc = new frmScren();
                sc.MdiParent = this;
                sc.MainForm = this;
                //sc.Width = this.Width - 50;
                //sc.Height = this.Height - 120;
                sc.Dock = DockStyle.Fill;
                sc.kartoteka = kartoteka;
                sc.Show();

                //********************************************************************************************************
                //SINKRONIZACIJA AKO SE RADI NA DRUGOJ LOKACIJI I SA RAZLIČITOM BAZOM A UPRAVLJA SE SA PRIMARNE POSLOVNICE

                if (baza.UzmiGodinuKojaSeKoristi() == DateTime.Now.Year)
                    sinkronizacija_poslovnica.classSinkronizacija.PokreniSinkronizacijiu();

                //********************************************************************************************************

                //if (System.Environment.MachineName == "POWER-RAC" && Util.Korisno.GodinaKojaSeKoristiUbazi==2015)
                //{
                //Properties.Settings.Default.domena_za_sinkronizaciju = "http://localhost/spojeno/";
                //Properties.Settings.Default.Save();

                bgSinkronizacija.RunWorkerAsync();
                //}

                //this.Text = DTtvrtka.Rows[0]["ime_tvrtke"].ToString();
                this.Text = Class.PodaciTvrtka.nazivTvrtke;

                //Util.Korisno kor = new Util.Korisno();

                frmKasaPrijava kp = new frmKasaPrijava();
                kp.ShowDialog();



                //svakih pola sata
                //timerProvjeraNadogradnje.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            backgroundWorkerServer.RunWorkerAsync();
            timer1.Start();
            timer1.Interval = 20000;
            OstaleProvjere();


            //            string sql_minusi = @"SELECT roba_prodaja.sifra AS ""Šifra artikla"",
            //roba.naziv as ""Naziv artikla"",
            //roba_prodaja.kolicina as ""Stanje"",
            //skladiste.skladiste as ""Skladište"",
            //skladiste.id_skladiste
            //FROM (select sifra, (REPLACE(roba_prodaja.kolicina, ',', '.')::numeric) as kolicina, id_skladiste from roba_prodaja where (REPLACE(roba_prodaja.kolicina, ',', '.')::numeric) < 0) as roba_prodaja
            //LEFT JOIN roba ON roba.sifra = roba_prodaja.sifra
            //LEFT JOIN skladiste ON roba_prodaja.id_skladiste = skladiste.id_skladiste
            //WHERE roba.oduzmi = 'DA';";

            string sql_minusi = "SELECT roba_prodaja.sifra AS [Šifra artikla],roba.naziv as [Naziv artikla],CAST(REPLACE(roba_prodaja.kolicina,',','.') AS numeric) as [Stanje],skladiste.skladiste as [Skladište],skladiste.id_skladiste FROM roba_prodaja LEFT JOIN roba ON roba.sifra=roba_prodaja.sifra LEFT JOIN skladiste ON roba_prodaja.id_skladiste=skladiste.id_skladiste WHERE CAST(REPLACE(roba_prodaja.kolicina,',','.') AS numeric)<0 AND roba.oduzmi='DA';";


            DataSet dsNegativnoStanje = classSQL.select(sql_minusi, "negativno_stanje");

            if ((dsNegativnoStanje != null && dsNegativnoStanje.Tables.Count > 0 && dsNegativnoStanje.Tables[0] != null && dsNegativnoStanje.Tables[0].Rows.Count > 0) && (DTpostavke.Rows[0]["provjera_stanja"].ToString() == "1"))
            {
                if (MessageBox.Show("Postoje artikli sa negativnim stanjem na skladištu!\r\nŽelite li sada pregledati?", "Upozorenje!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    frmStanjeUMinusu frm = new frmStanjeUMinusu();
                    frm.ShowDialog(dsNegativnoStanje.Tables[0]);
                }
            }

            DataTable DTpodaci = classSQL.select_settings("SELECT * FROM podaci_tvrtka WHERE id='1'", "podaci_tvrtka").Tables[0];
            if ((DTpodaci.Rows[0]["oib"].ToString() == Class.Postavke.OIB_PC1) || (DTpodaci.Rows[0]["oib"].ToString() == "69704829478"))
                uvozOdPartneraToolStripMenuItem.Visible = true;

            backgroundWorker1.RunWorkerAsync();
        }

        private void CheckConnection()
        {
            try
            {
                if (classSQL.remoteConnectionString != "")
                {
                    if (SQL.claasConnectDatabase.TestRemoteConnection() != true)
                    {
                        frmPostavkeUdaljeneBaze pu = new frmPostavkeUdaljeneBaze();
                        pu.ShowDialog();
                    }
                }
            }
            catch (Exception)
            {
                frmPostavkeUdaljeneBaze pu = new frmPostavkeUdaljeneBaze();
                pu.ShowDialog();
                Application.Exit();
            }
        }

        private string getOSArchitecture()
        {
            string architectureStr;
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)))
            {
                architectureStr = "64-bit";
            }
            else
            {
                architectureStr = "32-bit";
            }
            return architectureStr;
        }

        public static bool RunInstallMSI(string sMSIPath)
        {
            try
            {
                Console.WriteLine("Starting to install application");
                Process process = new Process();
                process.StartInfo.FileName = "vcredist_x86.exe";
                process.StartInfo.Arguments = string.Format(" /qb /i \"{0}\" ALLUSERS=1", sMSIPath);
                process.Start();
                process.WaitForExit();
                Console.WriteLine("Application installed successfully!");
                return true; //Return True if process ended successfully
            }
            catch
            {
                Console.WriteLine("There was a problem installing the application!");
                return false;  //Return False if process ended unsuccessfully
            }
        }

        private void stvori_sliku()
        {
            //if (!File.Exists("bijela.jpg"))
            //{
            System.Drawing.Bitmap flag = new System.Drawing.Bitmap(10, 10);
            for (int x = 0; x < 10; ++x)
                for (int y = 0; y < 10; ++y)
                    flag.SetPixel(x, y, Color.White);
            //for (int x = 0; x < flag.Height; ++x)
            //    flag.SetPixel(x, x, Color.Red);

            Stream stream = new FileStream("bijela.jpg", FileMode.Create, FileAccess.Write);

            flag.Save(stream, ImageFormat.Jpeg);
            //}
        }

        private void dll_dl()
        {
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string windows_bit = getOSArchitecture();

            if (!File.Exists("msvcr100.dll"))
            {
                put_za_stavkeWeb = "https://www.pc1.hr/pcpos/update/msvcr100.dll";
                put_za_stavke = "msvcr100.dll";
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                try
                {
                    if (!File.Exists("C:\\windows\\sysWOW64\\msvcr100.dll"))
                    {
                        string sourceFile = "msvcr100.dll";
                        string destFile = "C:\\windows\\sysWOW64\\msvcr100.dll";
                        File.Copy(sourceFile, destFile, true);
                    }
                }
                catch { }

                try
                {
                    if (!File.Exists("C:\\windows\\system32\\msvcr100.dll"))
                    {
                        string sourceFile = "msvcr100.dll";
                        string destFile = "C:\\windows\\system32\\msvcr100.dll";
                        File.Copy(sourceFile, destFile, true);
                    }
                }
                catch { }
            }
        }

        private void OstaleProvjere()
        {
            DownloadFontHelper();

            Util.FormatBroja.Provjeri();

            ProvjeraNaplatnihUredaja();
        }

        private void KojiServer()
        {
            string remoteServer = "";
            string DBname = "";
            string IP = LocalIPAddress();

            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";
            XDocument xmlFile = XDocument.Load(path);
            var query = from c in xmlFile.Element("settings").Elements("database_remote").Elements("postgree") select c;
            foreach (XElement book in query)
            {
                remoteServer = book.Attribute("server").Value;

                DBname = book.Attribute("database").Value;
            }

            if (IP == "192.168.0.108" || IP == "192.168.0.159")
                if (remoteServer == "192.168.0.108")
                {
                    MessageBox.Show("PRODUKCIJSKI SERVER!!!");
                }
        }

        private void provjeri_filove_za_backup()
        {
            if (File.Exists("DBbackup/novi_backup"))
            {
                if (!File.Exists("DBbackup/novi_backup1"))
                {
                    put_za_stavkeWeb = "https://www.pc1.hr/pcpos/update/Backup/pg_dump.exe";
                    put_za_stavke = "DBbackup/pg_dump.exe";
                    backgroundWorker1.RunWorkerAsync();
                    using (System.IO.File.Create(@"DBbackup/novi_backup1"))
                        ;
                }
            }

            if (!File.Exists("DBbackup/novi_backup"))
            {
                if (!File.Exists("DBbackup/novi_backup"))
                {
                    put_za_stavkeWeb = "https://www.pc1.hr/pcpos/update/Backup/libintl.dll";
                    put_za_stavke = "DBbackup/libintl.dll";
                    backgroundWorker1.RunWorkerAsync();
                }

                using (System.IO.File.Create(@"DBbackup/novi_backup"))
                    ;
            }
        }

        public string LocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

        private void ProvjeraNaplatnihUredaja()
        {
            try
            {
                DataTable DT = classSQL.select("SELECT id_blagajna FROM blagajna", "blagajna").Tables[0];

                if (DT.Rows.Count < 3)
                {
                    string sql = "INSERT INTO blagajna (ime_blagajne,id_ducan,aktivnost) VALUES ('2','1','1');INSERT INTO blagajna (ime_blagajne,id_ducan,aktivnost) VALUES ('3','1','1');INSERT INTO blagajna (ime_blagajne,id_ducan,aktivnost) VALUES ('4','1','1');";
                    classSQL.insert(sql);

                    sql = "UPDATE postavke SET naplatni_uredaj_faktura_bez_robe='4',naplatni_uredaj_avans='3',naplatni_uredaj_faktura='2'";
                    classSQL.Setings_Update(sql);
                }
            }
            catch
            {
                try
                {
                    DataTable DT = classSQL.select("SELECT id_blagajna FROM blagajna", "blagajna").Tables[0];

                    if (DT.Rows.Count < 3)
                    {
                        string sql = "INSERT INTO blagajna (ime_blagajne,id_ducan,aktivnost) VALUES ('2','1','1');INSERT INTO blagajna (ime_blagajne,id_ducan,aktivnost) VALUES ('3','1','1');INSERT INTO blagajna (ime_blagajne,id_ducan,aktivnost) VALUES ('4','1','1');";
                        classSQL.insert(sql);

                        sql = "UPDATE postavke SET naplatni_uredaj_faktura_bez_robe='4',naplatni_uredaj_avans='3',naplatni_uredaj_faktura='2'";
                        classSQL.Setings_Update(sql);
                    }
                }
                catch
                {
                }
            }
        }

        private void DownloadFontHelper()
        {
            string fontPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\slike\\msgothic.ttc";
            if (!File.Exists(fontPath))
            {
                if (!Util.CheckConnection.Check())
                {
                    MessageBox.Show("Ne postoji datoteka za postavljanje fonta kod ispisa računa. Pokušaj skidanja\n" +
                        " datoteke s Interneta nije uspio jer niste spojeni na Internet. Provjerite svoju Internet konekciju i " +
                        "ponovo pokrenite program, jer u suprotnom nećete moći ispisivati račune.", "Upozorenje!");
                    return;
                }

                MessageBox.Show("Slijedi pokušaj skidanja datoteke za ispis računa.", "");

                Util.frmUpdate a = new Util.frmUpdate();
                a.path = fontPath;
                a.ShowDialog();
            }
        }

        //private void CheckConnection()
        //{
        //    try
        //    {
        //        SqlCeConnection connection = new SqlCeConnection(classSQL.connectionString);
        //        connection.Open();
        //    }
        //    catch (Exception)
        //    {
        //        frmPostavaBaze pb = new frmPostavaBaze();
        //        pb.ShowDialog();
        //    }
        //}

        private void EnableDisableDocuments()
        {
            //DataTable DT = OstaleFunkcije.DSaktivnosDok;
            kartoteka = Class.Dokumenti.kartoteka;

            kalkulacijeToolStripMenuItem.Enabled = Class.Dokumenti.kalkulacije;
            inventuraToolStripMenuItem.Enabled = Class.Dokumenti.inventure;
            karticaToolStripMenuItem.Enabled = Class.Dokumenti.kartice;
            fakturaToolStripMenuItem1.Enabled = Class.Dokumenti.fakture;
            fakturaBezRobeToolStripMenuItem.Enabled = Class.Dokumenti.faktureBezRobe;
            ponudeToolStripMenuItem.Enabled = Class.Dokumenti.ponude;
            radniNalogToolStripMenuItem1.Enabled = Class.Dokumenti.radniNalozi;
            povratRobeToolStripMenuItem.Enabled = Class.Dokumenti.povratDobavljacu;
            //otpisRobeToolStripMenuItem.Enabled = Class.Dokumenti.otpisRobe;
            naljepniceToolStripMenuItem.Enabled = Class.Dokumenti.naljepnice;
            ulazneFaktureToolStripMenuItem.Enabled = Class.Dokumenti.ulazneFakture;
            otpremniceToolStripMenuItem.Enabled = Class.Dokumenti.otpremnice;
            međuskladišniceToolStripMenuItem.Enabled = Class.Dokumenti.medjuskladisnice;
            odjavaKomisioneRobeToolStripMenuItem.Enabled = Class.Dokumenti.odjaveRobe;
            izdatniceToolStripMenuItem.Enabled = Class.Dokumenti.izdatnice;
            primkeToolStripMenuItem.Enabled = Class.Dokumenti.primke;
            //promocijaToolStripMenuItem.Enabled = Class.Dokumenti.promocije;
            početnoStanjeToolStripMenuItem1.Enabled = Class.Dokumenti.pocetnoStanje;
            prometKaseToolStripMenuItem1.Enabled = Class.Dokumenti.prometPoRobi;
            ukupniPrometToolStripMenuItem.Enabled = Class.Dokumenti.prometPoRobi;
            prometPoRačunimaToolStripMenuItem.Enabled = Class.Dokumenti.prometPoRobi;
        }

        private DataSet DS_aktivacija;

        private void AktivacijaFiskalizacijePrograma()
        {
            if (!File.Exists("ne_salji"))
            {
                try
                {
                    newSql SqlPostgres = new newSql();

                    DataTable DTracuni = SqlPostgres.select("SELECT SUM(ukupno) FROM racuni", "racuni").Tables[0];
                    DataTable DTfakture = SqlPostgres.select("SELECT SUM(ukupno) FROM fakture", "fakture").Tables[0];
                    DataTable DTaktivnost = SqlPostgres.select_settings("SELECT kalkulacije,boolRobno FROM aktivnost_podataka", "aktivnost_podataka").Tables[0];
                    DTpostavke = SqlPostgres.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

                    string boolRobno = "NE";
                    if (DTaktivnost.Rows.Count > 0)
                    {
                        if (DTaktivnost.Rows[0]["kalkulacije"].ToString() == "1" && DTaktivnost.Rows[0]["boolRobno"].ToString() == "1")
                        {
                            boolRobno = "DA";
                        }
                    }

                    decimal racuni = 0;
                    decimal fakture = 0;
                    if (DTracuni.Rows.Count > 0 && DTracuni.Rows[0][0].ToString() != "")
                    {
                        racuni = Convert.ToDecimal(DTracuni.Rows[0][0].ToString());
                    }
                    if (DTfakture.Rows.Count > 0 && DTfakture.Rows[0][0].ToString() != "")
                    {
                        fakture = Convert.ToDecimal(DTfakture.Rows[0][0].ToString());
                    }

                    if (Util.CheckConnection.Check())
                    {
                        DS_aktivacija = FiskalizacijaRegistracijaServer.dllFiskalizacijaRegistracijaServer.SendDataOnServer(
                            "http://mc4b.eu/pcpos/insert.php",
                            Class.PodaciTvrtka.oibTvrtke,
                            Class.PodaciTvrtka.nazivTvrtke,
                            Class.PodaciTvrtka.telefonTvrtke,
                            DTpostavke.Rows[0]["lokacija_sigurnosne_kopije"].ToString(),
                            //ne id nego naziv staviti
                            DTpostavke.Rows[0]["default_blagajna"].ToString(),
                            DTpostavke.Rows[0]["default_ducan"].ToString(),
                            Properties.Settings.Default.verzija_programa.ToString(),
                            boolRobno,
                            (racuni + fakture).ToString("#0.00")
                        );
                    }

                    if (DS_aktivacija.Tables.Count > 0 && DS_aktivacija.Tables[0].Rows.Count > 0)
                    {
                        if (DS_aktivacija.Tables[0].Rows[0]["poruka_iskljucenja"].ToString() != "")
                        {
                            string aa = DS_aktivacija.Tables[0].Rows[0]["poruka_iskljucenja"].ToString();
                            MessageBox.Show(DS_aktivacija.Tables[0].Rows[0]["poruka_iskljucenja"].ToString());
                            Application.Exit();
                        }
                        else if (DS_aktivacija.Tables[0].Rows[0]["poruka_upozorenja"].ToString() != "")
                        {
                            MessageBox.Show(DS_aktivacija.Tables[0].Rows[0]["poruka_upozorenja"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("GREŠKA KOD PRIJAVE NA SERVER\r\n"+ex.ToString());
                }
            }
        }

        private void provjeraStart()
        {
            Process[] pArry = Process.GetProcesses();

            foreach (Process p in pArry)
            {
                string s = p.ProcessName;
                if (s.CompareTo("PC POS") == 0)
                {
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //probe pr = new probe();
            //pr.ShowDialog();

            Form1 h = new Form1();
            h.ShowDialog();
        }

        private void kasaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }

            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            foreach (Form OpenForm in Application.OpenForms)
            {
                if (OpenForm.GetType() == typeof(frmKasa))
                {
                    //MessageBox.Show("Kasa je već otvorena.");
                    OpenForm.WindowState = FormWindowState.Maximized;
                    return;
                }
            }

            frmKasa ks = new frmKasa();
            ks.MainForm = this;
            ks.Show();
        }

        private void frmMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            //string remoteServer = "";
            //string DBname = "";
            //if (DTpostavke.Rows.Count == 0)
            //{
            //    DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            //}

            //string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";
            //XDocument xmlFile = XDocument.Load(path);
            //var query = from c in xmlFile.Element("settings").Elements("database_remote").Elements("postgree") select c;
            //foreach (XElement book in query)
            //{
            //    remoteServer = book.Attribute("server").Value;
            //    DBname = book.Attribute("database").Value;
            //}

            //string _path = System.Environment.CurrentDirectory;
            //string putanja_za_backup = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/BackupBaze/";

            //if (Directory.Exists(DTpostavke.Rows[0]["lokacija_sigurnosne_kopije"].ToString()))
            //{
            //    putanja_za_backup = DTpostavke.Rows[0]["lokacija_sigurnosne_kopije"].ToString();
            //}
            //else if (!Directory.Exists(putanja_za_backup))
            //{
            //    Directory.CreateDirectory(putanja_za_backup);
            //}

            if (!Directory.Exists(Class.Postavke.lokacija_sigurnosne_kopije))
            {
                Directory.CreateDirectory(Class.Postavke.lokacija_sigurnosne_kopije);
            }

            string pathForPostgresqlDump = string.Format(@"{0}\DBbackup\pg_dump.exe", Environment.CurrentDirectory);

            string sql = string.Format(@"--host {0} --port {5} --username {6} --format custom --blobs --verbose --file {4}{1}\{2}-{3}.backup{4} {4}{2}{4}",
                    Class.PodaciZaSpajanjeCompaktna.remoteServer,
                    Class.Postavke.lokacija_sigurnosne_kopije,
                    Class.PodaciZaSpajanjeCompaktna.remoteDb,
                    DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"),
                    "\"",
                    Class.PodaciZaSpajanjeCompaktna.remotePort,
                    Class.PodaciZaSpajanjeCompaktna.remoteUsername);

            System.Diagnostics.Process.Start(pathForPostgresqlDump, sql);

            timer1.Stop();
        }

        private void ProvjeraNadogradnjeSNajnovijimPromjenama()
        {
            if (!File.Exists("ne_salji"))
            {
                if (!Util.CheckConnection.Check())
                    return;

                try
                {
                    WebClient webClient = new WebClient();
                    webClient.DownloadFile("https://www.pc1.hr/pcpos/update/verzija.txt", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\verzija.txt");
                    string VerzijaNaNetu = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\verzija.txt");

                    decimal verzijaPrograma = 0, verzijaNaNetu = 0;

                    decimal.TryParse(VerzijaNaNetu, out verzijaNaNetu);
                    decimal.TryParse(Properties.Settings.Default.verzija_programa.ToString(), out verzijaPrograma);

                    if (verzijaPrograma < verzijaNaNetu)
                    {
                        frmPovijestPromjena pp = new frmPovijestPromjena();
                        pp.ShowDialog();

                        Util.Korisno.NovijaInacica(true);
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void backgroundWorkerServer_DoWork(object sender, DoWorkEventArgs e)
        {
            //samo na startu programa
            //ProvjeraNadogradnjeSNajnovijimPromjenama();
        }

        #region menu

        private void stopePorezaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Sifarnik.frmStopePoreza ad = new Sifarnik.frmStopePoreza();
            ad.ShowDialog();
        }

        private void novaFakturaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Robno.frmFakturaBezRobe FR = new Robno.frmFakturaBezRobe();
            FR.MdiParent = this;
            FR.Dock = DockStyle.Fill;
            FR.MainForm = this;
            FR.Show();
        }

        private void sveFaktureToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Robno.frmSveFaktureBezRobe sf = new Robno.frmSveFaktureBezRobe();
            sf.MdiParent = this;
            sf.MainFormMenu = this;
            sf.Show();
        }

        private void novaUlaznaFakturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Robno.frmUlazneFakture uf = new Robno.frmUlazneFakture();
            uf.MdiParent = this;
            uf.Dock = DockStyle.Fill;
            uf.MainForm = this;
            uf.Show();
        }

        private void noviAvansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmAvans avans = new frmAvans();
            avans.MdiParent = this;
            avans.Dock = DockStyle.Fill;
            avans.MainForm = this;
            avans.Show();
        }

        private void sviAvansiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmSviAvansi avansSvi = new frmSviAvansi();
            avansSvi.MdiParent = this;
            avansSvi.MainFormMenu = this;
            avansSvi.Show();
        }

        private void novaPrimkaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }

            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Robno.frmPrimka zp = new Robno.frmPrimka();
            zp.MdiParent = this;
            zp.MainForm = this;
            zp.Dock = DockStyle.Fill;
            zp.Show();
        }

        private void svePrimkeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Robno.frmSvePrimke primkaSve = new Robno.frmSvePrimke();
            primkaSve.MdiParent = this;
            primkaSve.MainFormMenu = this;
            primkaSve.Show();
        }

        private void novaIzdatnicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }

            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Robno.frmIzdatnica zp = new Robno.frmIzdatnica();
            zp.MdiParent = this;
            zp.MainForm = this;
            zp.Dock = DockStyle.Fill;
            zp.Show();
        }

        private void sveIzdatniceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Robno.frmSveIzdatnice izdatnicaSve = new Robno.frmSveIzdatnice();
            izdatnicaSve.MdiParent = this;
            izdatnicaSve.MainFormMenu = this;
            izdatnicaSve.Show();
        }

        private void postaviToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Robno.frmStanjePoZadnjojInventuri zp = new Robno.frmStanjePoZadnjojInventuri();
            zp.MdiParent = this;
            zp.MainFormMenu = this;
            zp.Dock = DockStyle.Fill;
            zp.Show();
        }

        private void prometPoRobibezSkladištaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kasa.frmPrometPoRobiBezSkladista prp = new Kasa.frmPrometPoRobiBezSkladista();
            prp.ShowDialog();
        }

        private void aktivacijaDokumenataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(1))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmAktivacijaDokumenata ad = new frmAktivacijaDokumenata();
            ad.ShowDialog();
        }

        private void unosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Sifarnik.frmAddPartners new_partner = new Sifarnik.frmAddPartners();
            new_partner.MainFormMenu = this;
            new_partner.ShowDialog();
        }

        private void robauslugeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmRobaUsluge robaUsluge = new frmRobaUsluge();
            robaUsluge.MdiParent = this;
            robaUsluge.MainFormMenu = this;
            robaUsluge.Show();
        }

        private void kalkulacijaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmNovaKalkulacija2 nova_kalkulacija = new frmNovaKalkulacija2();
            nova_kalkulacija.MdiParent = this;
            nova_kalkulacija.Dock = DockStyle.Fill;
            nova_kalkulacija.MainForm = this;
            nova_kalkulacija.Show();
        }

        private void sveKalkulacijeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmPopisKalkulacija fr = new frmPopisKalkulacija();
            fr.MdiParent = this;
            fr.MainFormMenu = this;
            fr.Show();
        }

        private void novaFakturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmFaktura faktura = new frmFaktura();
            faktura.MdiParent = this;
            faktura.Dock = DockStyle.Fill;
            faktura.MainForm = this;
            faktura.Show();
        }

        private void sveFaktureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmSveFakture sf = new frmSveFakture();
            sf.MdiParent = this;
            sf.MainFormMenu = this;
            sf.Show();
        }

        private void novaPonudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmPonude ponu = new frmPonude();
            ponu.MdiParent = this;
            ponu.Dock = DockStyle.Fill;
            ponu.MainForm = this;
            ponu.Show();
        }

        private void svePonudeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            frmSvePonude sp = new frmSvePonude();
            sp.sifra_ponude = "";
            sp.MdiParent = this;
            sp.MainFormMenu = this;
            sp.Dock = DockStyle.Fill;
            sp.Show();
        }

        private void noviRadniNalogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            frmRadniNalog rn = new frmRadniNalog();
            rn.MdiParent = this;
            rn.Dock = DockStyle.Fill;
            rn.MainFormMenu = this;
            rn.Show();
        }

        private void sviRadniNaloziToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmSviRadniNalozi srn = new frmSviRadniNalozi();
            srn.MdiParent = this;
            srn.MainFormMenu = this;
            srn.Show();
        }

        private void unosNormativaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }

            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            frmNormativi rn = new frmNormativi();
            rn.MdiParent = this;
            rn.Dock = DockStyle.Fill;
            rn.MainForm = this;
            rn.Show();
        }

        private void sviNormativiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmSviNormativi sf = new frmSviNormativi();
            sf.MdiParent = this;
            sf.MainFormMenu = this;
            sf.Show();
        }

        private void karticaSkladištaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmKarticaRobe kr = new frmKarticaRobe();
            kr.MdiParent = this;
            kr.Dock = DockStyle.Fill;
            kr.MainFormMenu = this;
            kr.Show();
        }

        private void karticaSkladištaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmKarticaSkladiste ks = new frmKarticaSkladiste();
            ks.MdiParent = this;
            ks.Dock = DockStyle.Fill;
            ks.MainFormMenu = this;
            ks.Show();
        }

        public void otpremnicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }

            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmOtpremnica ot = new frmOtpremnica();
            ot.MdiParent = this;
            ot.Dock = DockStyle.Fill;
            ot.MainForm = this;
            ot.Show();
        }

        private void sveOtpremniceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmSveOtpremnice so = new frmSveOtpremnice();
            so.MdiParent = this;
            so.MainFormMenu = this;
            so.Show();
        }

        private void aktivnostiZaposlenikaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmAktivnostiZaposlenika AZ = new frmAktivnostiZaposlenika();
            AZ.ShowDialog();
        }

        private void promocijaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmPromocijePostavke pp = new frmPromocijePostavke();
            pp.ShowDialog();
        }

        private void unosInventureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Robno.frmUnosInventura inv = new Robno.frmUnosInventura();
            inv.MdiParent = this;
            inv.Dock = DockStyle.Fill;
            inv.MainFormMenu = this;
            inv.Show();
        }

        private void podaciOTvrtkiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            podloga frmpo = new podloga();
            frmpo.ShowDialog();
        }

        private void posPrinterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmPosPrinter posprint = new frmPosPrinter();
            posprint.ShowDialog();
        }

        private void postavkeProgramaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmPostavke settings_form = new frmPostavke();
            settings_form.ShowDialog();
        }

        private void skladištaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Sifarnik.frmSkladista sk = new Sifarnik.frmSkladista();
            sk.ShowDialog();
        }

        private void zemljeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            Sifarnik.frmDodajZemlju dz = new Sifarnik.frmDodajZemlju();
            dz.ShowDialog();
        }

        private void žiroRačuniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Sifarnik.frmZiroRacun zr = new Sifarnik.frmZiroRacun();
            zr.ShowDialog();
        }

        private void zaposleniciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(1))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Sifarnik.frmZaposlenici zap = new Sifarnik.frmZaposlenici();
            zap.ShowDialog();
            Util.classZaposleniciDopustenja.osvjeziDopustenja();
        }

        private void blagajneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(1))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Sifarnik.frmBlagajne bl = new Sifarnik.frmBlagajne();
            bl.ShowDialog();
        }

        private void dučaniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(1))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Sifarnik.frmDucani duc = new Sifarnik.frmDucani();
            duc.ShowDialog();
        }

        private void proizviđaćiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(1))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Sifarnik.frmProizvodac pro = new Sifarnik.frmProizvodac();
            pro.ShowDialog();
        }

        private void grupeProizvodaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Sifarnik.frmGrupeProizvoda GP = new Sifarnik.frmGrupeProizvoda();
            GP.ShowDialog();
        }

        private void međuskladišnicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }

            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmMeduskladisnica med = new frmMeduskladisnica();
            med.MdiParent = this;
            med.Dock = DockStyle.Fill;
            med.MainForm = this;
            med.Show();
        }

        private void sveOtpremniceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmSveMeduskladisnice sm = new frmSveMeduskladisnice();
            //sm.MainFormMenu = this;
            sm.MdiParent = this;
            sm.MainFormMenu = this;
            sm.Show();
        }

        private void sviRačuniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Kasa.frmSviRacuni sr = new Kasa.frmSviRacuni();
            sr.ShowDialog();
        }

        private void sveInventureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Robno.frmSveInventure sinv = new Robno.frmSveInventure();
            sinv.MdiParent = this;
            sinv.MainFormMenu = this;
            sinv.Show();
        }

        private void početnoStanjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmPocetnoStanje pc = new frmPocetnoStanje();
            pc.MdiParent = this;
            pc.Dock = DockStyle.Fill;
            pc.Show();
        }

        private void odjavaKomisioneRobeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Robno.frmOdjavaKomisione zp = new Robno.frmOdjavaKomisione();
            zp.MdiParent = this;
            zp.Dock = DockStyle.Fill;
            zp.Show();
        }

        private void sveOdjavaKomisioneRobeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Robno.frmSveOdjave_komisione sf = new Robno.frmSveOdjave_komisione();
            sf.MdiParent = this;
            sf.MainFormMenu = this;
            sf.Show();
        }

        private void zapisnikOPromjeniCijeneToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Robno.frmZapisnikopromjeniCijene zp = new Robno.frmZapisnikopromjeniCijene();
            zp.MdiParent = this;
            zp.MainForm = this;
            zp.Dock = DockStyle.Fill;
            zp.Show();
        }

        private void sviZapisniciOPromjeniCijeneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Robno.frmSvePromjeneCijena sf = new Robno.frmSvePromjeneCijena();
            sf.MdiParent = this;
            sf.MainFormMenu = this;
            sf.Show();
        }

        private void povratRobeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            Robno.frmPovratRobe zp = new Robno.frmPovratRobe();
            zp.MdiParent = this;
            zp.MainForm = this;
            zp.Dock = DockStyle.Fill;
            zp.Show();
        }

        private void sviPovratiRobeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Robno.frmSviPovrati sf = new Robno.frmSviPovrati();
            sf.MdiParent = this;
            sf.MainFormMenu = this;
            sf.Show();
        }

        private void prometKaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kasa.frmPrometKase sf = new Kasa.frmPrometKase();
            sf.MdiParent = this;
            sf.Show();
        }

        private void novaPrimkaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Robno.frmPrimka zp = new Robno.frmPrimka();
            zp.MdiParent = this;
            zp.MainForm = this;
            zp.Dock = DockStyle.Fill;
            zp.Show();
        }

        private void prometPoRobiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kasa.frmPrometPoRobi prp = new Kasa.frmPrometPoRobi();
            prp.ShowDialog();
        }

        private void neposlanaFiskalizacijaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Fiskalizacija.frmNeupjeleTransakcije aa = new Fiskalizacija.frmNeupjeleTransakcije();
            aa.ShowDialog();
        }

        private void prometIZaključnoStanjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kasa.frmPrometIzakljucnoStanje pz = new Kasa.frmPrometIzakljucnoStanje();
            pz.ShowDialog();
        }

        #endregion menu

        private void izlazneFaktureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Izlracuni izlazniracuni = new Izlracuni();

            izlazniracuni.ShowDialog();
        }

        private void obračunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmObracunporeza obracun = new frmObracunporeza();

            obracun.ShowDialog();
        }

        private void pregledGradovaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Sifarnik.Gradovi grad = new Sifarnik.Gradovi();
            grad.ShowDialog();
        }

        private void unosNovogGradaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Sifarnik.frmNoviGrad grad = new Sifarnik.frmNoviGrad();
            grad.ShowDialog();
        }

        private void rekapitulacijaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmRekapitulacija frmRek = new frmRekapitulacija();
            frmRek.ShowDialog();
        }

        private void ukupniPrometToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Kasa.frmPrometPoRobi prp = new Kasa.frmPrometPoRobi();
            prp.ShowDialog();
        }

        private void prometPoRačunimaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Kasa.frmPrometPoRobiBezSkladista prp = new Kasa.frmPrometPoRobiBezSkladista();
            prp.ShowDialog();
        }

        private void prometKaseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Kasa.frmPrometKase sf = new Kasa.frmPrometKase();
            sf.MdiParent = this;
            sf.Show();
        }

        private void poslovniProstorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Fiskalizacija.frmPoslovniProstor sf = new Fiskalizacija.frmPoslovniProstor();
            sf.ShowDialog();
        }

        private void prodajaParagonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }

            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmParagonac p = new frmParagonac();
            p.ShowDialog();
        }

        private void sveUlazneFaktureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmSveUlazneFakture p = new frmSveUlazneFakture();
            p.MdiParent = this;
            p.MainFormMenu = this;
            p.Show();
        }

        private void načiniPlaćanjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(1))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Sifarnik.frmNacinPlacanja p = new Sifarnik.frmNacinPlacanja();
            p.ShowDialog();
        }

        private void noviOtpisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Robno.frmOtpisRobe p = new Robno.frmOtpisRobe();
            p.ShowDialog();
        }

        private void sviOtpisiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Robno.frmSviOtpisi sviotp = new Robno.frmSviOtpisi();
            sviotp.MdiParent = this;
            sviotp.MainFormMenu = this;
            sviotp.Show();
        }

        private void servisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmRadniNalogSerivs s = new frmRadniNalogSerivs();
            s.ShowDialog();
        }

        private void sviServisiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmSviRadniNaloziServis s = new frmSviRadniNaloziServis();
            s.MdiParent = this;
            s.MainFormMenu = this;
            s.Show();
        }

        private void smjeneZaposlenikaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Kasa.frmSveSmjene sm = new Kasa.frmSveSmjene();
            sm.ShowDialog();
        }

        private void valuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Sifarnik.frmValute ad = new Sifarnik.frmValute();
            ad.ShowDialog();
        }

        private void knjigaPrimitkaIGubitkaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmKnjigaPII knj = new frmKnjigaPII();
            knj.ShowDialog();
        }

        private void prometKasePoFakturamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            promet_po_fakt_blagajna pr = new promet_po_fakt_blagajna();
            pr.ShowDialog();
        }

        private void izvještajPoŠtičenikuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            staracki_dom star = new staracki_dom();
            star.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 2400000;
            string sql_neuspjele = "SELECT count(*) FROM neuspjela_fiskalizacija";

            int broj = Convert.ToInt32(classSQL.select(sql_neuspjele, "neuspjela_fiskalizacija").Tables[0].Rows[0][0].ToString());

            if (broj > 0)
            {
                if (MessageBox.Show("Postoje neuspjele fiskalizacije!\r\nŽelite li sada fiskalizirati?", "Upozorenje!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Fiskalizacija.frmNeupjeleTransakcije aa = new Fiskalizacija.frmNeupjeleTransakcije();
                    aa.ShowDialog();
                }
            }
        }

        private void ulazIzlazRobaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmIzlazUlazskl UIZ = new frmIzlazUlazskl();
            UIZ.ShowDialog();
        }

        private void novaFakturaZaVanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            FakturaVan.frmFakturaVan fak = new FakturaVan.frmFakturaVan();
            fak.ShowDialog();
        }

        private void sveFaktureZaVanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            FakturaVan.frmSveFaktureVan faksve = new FakturaVan.frmSveFaktureVan();
            faksve.ShowDialog();
        }

        private void prometKasePoDanimaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmIspisProdajnihArtiklaPoDanima ispisdani = new frmIspisProdajnihArtiklaPoDanima();
            ispisdani.ShowDialog();
        }

        private void poravnavanjeSkladištaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(1))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmPoravnanjeSkladista por = new frmPoravnanjeSkladista();
            por.ShowDialog();
        }

        private void podgrupaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            PCPOS.Sifarnik.frmPodGrupe pod = new Sifarnik.frmPodGrupe();
            pod.ShowDialog();
        }

        private void održavanjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Odrzavanja.frmOdrzavanja odr = new Odrzavanja.frmOdrzavanja();
            odr.ShowDialog();
        }

        private void promjenaCijenaKomadnoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Robno.frmPromjenaCijeneKomadno prom = new Robno.frmPromjenaCijeneKomadno();
            prom.ShowDialog();
        }

        private void održavanjeProvjeraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Odrzavanja.Odrzavanje_promjena odr = new Odrzavanja.Odrzavanje_promjena();
            odr.ShowDialog();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //pc1.konektor kon = new pc1.konektor();
                //DataTable DT = kon.GetDataset(string.Format("SELECT * FROM desktop_programi WHERE oib = '{0}'", Class.PodaciTvrtka.oibTvrtke), "programi", "admin", "q1w2e3r4123").Tables[0];

                List<string> L = baza.UzmiSveBazeIzPostgressa();
                string sve_baze = "";
                foreach (string DBe in L)
                {
                    sve_baze += DBe + ";";
                }

                //if (DT.Rows.Count == 0)
                //{
                //    kon.Execute("INSERT INTO desktop_programi (oib,tvrtka,baze,trenutna_baza,vrijeme) VALUES ('" + Class.PodaciTvrtka.oibTvrtke + "'," +
                //        "'" + Class.PodaciTvrtka.kratkiNazivTvrtke + "'," +
                //        "'" + sve_baze + "'," +
                //        "'" + baza.UzmiBazuKojaSeKoristi() + "'," +
                //        "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                //        ")", "admin", "q1w2e3r4123");
                //}
                //else
                //{
                //    kon.Execute("UPDATE desktop_programi SET oib='" + Class.PodaciTvrtka.oibTvrtke + "'," +
                //    "tvrtka='" + Class.PodaciTvrtka.kratkiNazivTvrtke + "'," +
                //    "baze='" + sve_baze + "'," +
                //    "trenutna_baza='" + baza.UzmiBazuKojaSeKoristi() + "'," +
                //    "vrijeme='" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                //    "WHERE oib='" + Class.PodaciTvrtka.oibTvrtke + "'", "admin", "q1w2e3r4123");
                //}

                string dd = Environment.SystemDirectory + "\\msvcr100.dll";

                Util.classDownloadFiles d = new Util.classDownloadFiles();

                try
                {
                    if (!File.Exists("msvcr100.dll"))
                    {
                        d.SkiniDatoteku("http://pc1.hr/caffe/update/ostalo/ftpbin/msvcr100.dll", "msvcr100.dll");
                    }
                }
                catch
                {
                }

                try
                {
                    if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Podrška POWER COMPUTERS.exe"))
                    {
                        if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                        {
                            //PCPOS.Util.classDownloadFiles down = new Util.classDownloadFiles();
                            //down.SkiniDatoteku("http://pc1.hr/podrska/help.doc", Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Podrška POWER COMPUTERS.exe");
                        }
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                }

                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

                try
                {
                    if (!File.Exists(Environment.SystemDirectory + "\\msvcr100.dll"))
                    {
                        if (File.Exists("msvcr100.dll"))
                        {
                            d.SkiniDatoteku("http://pc1.hr/caffe/update/ostalo/ftpbin/DlluSystem32.doc", "DlluSystem32.exe");
                            GC.Collect();
                            if (File.Exists("DlluSystem32.exe"))
                            {
                                Process proc = Process.Start(path + "\\DlluSystem32.exe");
                            }
                        }
                    }
                }
                catch { }

                try
                {
                    if (Directory.Exists("C:\\windows\\sysWOW64"))
                    {
                        if (!File.Exists("C:\\windows\\sysWOW64\\msvcr100.dll"))
                        {
                            if (File.Exists("msvcr100.dll"))
                            {
                                d.SkiniDatoteku("http://pc1.hr/caffe/update/ostalo/ftpbin/DlluSystem32.doc", "DlluSystem32.exe");
                                GC.Collect();
                                if (File.Exists("DlluSystem32.exe"))
                                {
                                    Process proc = Process.Start(path + "\\DlluSystem32.exe");
                                }
                            }
                        }
                    }
                }
                catch { }
            }
            catch
            { }
        }

        private void uvozIzProgramaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(1))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            Sinkronizacija.frmUvoz_u_Program u = new Sinkronizacija.frmUvoz_u_Program();
            u.ShowDialog();
        }

        private void izvozUProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(1))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            Sinkronizacija.frmIzvozIzPrograma u = new Sinkronizacija.frmIzvozIzPrograma();
            u.ShowDialog();
        }

        private void xplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(1))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Sinkronizacija.Partneri.frmUvorXplorer ux = new Sinkronizacija.Partneri.frmUvorXplorer();
            ux.ShowDialog();
        }

        private void dobarPartnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(1))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Sinkronizacija.Partneri.frmUvoz_DobarPartner ux = new Sinkronizacija.Partneri.frmUvoz_DobarPartner();
            ux.ShowDialog();
        }

        private void bioBioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(1))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            Sinkronizacija.Partneri.frmBioBio ux = new Sinkronizacija.Partneri.frmBioBio();
            ux.ShowDialog();
        }

        private void školskaKnjigaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(1))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            Sinkronizacija.Partneri.frmSkolskaKnjiga sk = new Sinkronizacija.Partneri.frmSkolskaKnjiga();
            sk.ShowDialog();
        }

        private void skladišteFinancijskoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            Report.Liste.frmSkladisteFinancijsko s = new Report.Liste.frmSkladisteFinancijsko();
            s.ShowDialog();
        }

        private void knjigaUlazaIIzlazaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            frmTrgovackaKnjigaLoad frm = new frmTrgovackaKnjigaLoad();
            frm.ShowDialog();
        }

        private void provjeraNegativnihStanjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            frmStanjeUMinusu frm = new frmStanjeUMinusu();
            frm.ShowDialog();
        }

        private void špranceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            Util.frmSpranci frm = new Util.frmSpranci();
            frm.ShowDialog();
        }

        private void popisArtikalaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            Report.Liste.frmPopisArtikala frm = new Report.Liste.frmPopisArtikala();
            frm.ShowDialog();
        }

        private void povijestKoristenjadokumenataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            frmPovijestDokumenata frm = new frmPovijestDokumenata();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void tradexcoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(1))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            Sinkronizacija.Partneri.frmTradexco frm = new Sinkronizacija.Partneri.frmTradexco();
            frm.ShowDialog();
        }

        private void nirdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(1))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            Sinkronizacija.Partneri.Nird frm = new Sinkronizacija.Partneri.Nird();
            frm.ShowDialog();
        }

        private void fokusTekstilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(1))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            Sinkronizacija.Partneri.frmFokusTekstil frm = new Sinkronizacija.Partneri.frmFokusTekstil();
            frm.ShowDialog();
        }

        private void afroditaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(1))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            Sinkronizacija.Partneri.Afrodita frm = new Sinkronizacija.Partneri.Afrodita();
            frm.ShowDialog();
        }

        private void robotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(1))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            Sinkronizacija.Partneri.frmRobot r = new Sinkronizacija.Partneri.frmRobot();
            r.ShowDialog();
        }

        private void prodajneStatistikeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(1))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            Kasa.frmUlazIzlazFinancijski r = new Kasa.frmUlazIzlazFinancijski();
            r.ShowDialog();
        }

        private void robuPreuzeoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRobuPreuzeo rp = new frmRobuPreuzeo();
            rp.ShowDialog();
        }

        private void stariFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Robno.Stareulaznefakture.frmUlazneFakture sf = new Robno.Stareulaznefakture.frmUlazneFakture();
            sf.ShowDialog();
        }

        private void popisKupljeneRobePremaPartneruToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report.PopisRobe.frmPopisRobe pr = new Report.PopisRobe.frmPopisRobe();
            pr.ShowDialog();
        }

        private void popisDogađajaZaPartnereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPartnerKronologija kr = new frmPartnerKronologija();
            kr.ShowDialog();
        }

        private void popisDogađajaZaPartnereToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmPartnerKronologija kr = new frmPartnerKronologija();
            kr.ShowDialog();
        }

        private void prodajaPoArtiklimaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report.ProdajaPoArtiklima.frmProdajaPoArtiklu pa = new Report.ProdajaPoArtiklima.frmProdajaPoArtiklu();
            pa.ShowDialog();
        }

        private void blagajničkiIzvještajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report.BlagajnickiIzvjestaj.frmBlagajnickiIzvjestaj bli = new Report.BlagajnickiIzvjestaj.frmBlagajnickiIzvjestaj();
            bli.ShowDialog();
        }

        private void kronologijaPremaPartneruIRobiZaSveGodineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPregledPoGodinama pg = new frmPregledPoGodinama();
            pg.MdiParent = this;
            pg.Dock = DockStyle.Fill;
            pg.Show();
        }

        private void rekapitulacija2015ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (Class.PodaciTvrtka.oibTvrtke == Class.Postavke.OIB_PC1)
            //{
            //    Report.Rekapitulacija2017.frmRekapitulacija rep = new Report.Rekapitulacija2017.frmRekapitulacija();
            //    rep.MdiParent = this;
            //    rep.Show();

            //}
            //else
            //{
            Report.Rekapitulacija2015.frmRekapitulacija rep = new Report.Rekapitulacija2015.frmRekapitulacija();
            rep.MdiParent = this;
            rep.Show();
            //}
        }

        private void porezNaDohodakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report.PorezNaDohodak.frmPorezNaDohodak pnd = new Report.PorezNaDohodak.frmPorezNaDohodak();
            pnd.MdiParent = this;
            pnd.Show();
        }

        private synWeb.synPokretac PokretacSinkronizacije = new synWeb.synPokretac();

        private void bgSinkronizacija_DoWork(object sender, DoWorkEventArgs e)
        {

            //TODO: ovdje trebam preko web servisa dohvatiti kraj rada po productKey-u programa

            if (Util.Korisno.CheckForInternetConnection() && Util.Korisno.CheckForInternetConnection(Properties.Settings.Default.PC_POS_wsSoftKontrol_wsSoftKontrol.ToString()))
            {
                using (var ws = new wsSoftKontrol.wsSoftKontrol())
                {
                    string dohvaceniKrajRada = ws.DohvatiKrajRadaZaOdabraniProgram(Class.Registracija.productKey);
                    if (dohvaceniKrajRada.Length > 0)
                    {
                        string sql = string.Format(@"update registracija
    set
        kraj_rada = '{0}',
        zadnja_konekcija_prema_software = '{1:yyyy-MM-dd HH:mm:ss}';",
    dohvaceniKrajRada,
    DateTime.Now);
                        classSQL.Setings_Update(sql);
                        Class.Registracija.GetRegistracija();
                    }
                }

                getKontrola();
            }

            PokretacSinkronizacije.PokreniSinkronizaciju(true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
            //AktivacijaFiskalizacijePrograma();
        }

        private void getKontrola()
        {
            try
            {
                using (var ws = new wsSoftKontrol.wsSoftKontrol())
                {
                    var kontrola = ws.Kontrola(Class.PodaciTvrtka.oibTvrtke);

                    if (kontrola != null && kontrola.Split('¤')[0] != null && kontrola.Split('¤')[1] != null && kontrola.Split('¤')[0] != "0")
                    {
                        int iKon = 0;
                        if (Int32.TryParse(kontrola.Split('¤')[0], out iKon))
                        {
                            if (iKon > 0)
                            {
                                if (iKon == 1)
                                { //obavijest
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        MessageBox.Show(this, kontrola.Split('¤')[1], "Obavijest");
                                    });
                                }
                                else
                                {   //iskljucenje
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        if (MessageBox.Show(this, kontrola.Split('¤')[1], "Obavijest") == System.Windows.Forms.DialogResult.OK)
                                        {
                                            this.Close();
                                        }
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                //MessageBox.Show("Greška kod prijave na server!");
            }
        }

        private void kalk2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(2))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmNovaKalkulacija2 nova_kalkulacija = new frmNovaKalkulacija2();
            nova_kalkulacija.MdiParent = this;
            nova_kalkulacija.Dock = DockStyle.Fill;
            nova_kalkulacija.MainForm = this;
            nova_kalkulacija.Show();
        }

        private void naljepniceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmIzradaNaljepnice izn = new frmIzradaNaljepnice();
            izn.ShowDialog();
        }

        private void naljepniceBarcodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            Report.Naljepnice.NaljepniceEan izn = new Report.Naljepnice.NaljepniceEan();
            izn.ShowDialog();
        }

        private void knjižnoOdobrenjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Robno.frmKnjiznoOdobrenje f = new Robno.frmKnjiznoOdobrenje();
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void automatskaUskladaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
                {
                    return;
                }

                frmAutomatskaUsklada f = new frmAutomatskaUsklada();
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void sveAutomatskeUskladeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmAutomatskaUskladaSve f = new frmAutomatskaUskladaSve();
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void noviServisToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
                {
                    return;
                }
                if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
                {
                    MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                    return;
                }

                frmRadniNalogSerivs2016 f = new frmRadniNalogSerivs2016();
                f.MdiParent = this;
                f.MainForm = this;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void sviServisiToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
                {
                    MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                    return;
                }

                frmSviRadniNaloziServis2016 f = new frmSviRadniNaloziServis2016();
                f.MdiParent = this;
                f.MainFormMenu = this;
                f.Show();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void naljepniceToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
                {
                    MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                    return;
                }

                frmBarkodeServis f = new frmBarkodeServis();
                f.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void knjigaPopisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Robno.frmKnjigaPopisa f = new Robno.frmKnjigaPopisa();
                f.MdiParent = this;
                f.Show();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void karticaKupcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmKarticaKupca f = new frmKarticaKupca();
                f.MdiParent = this;
                f.Dock = DockStyle.Fill;
                f.Show();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void otpremnicaNaSkladišteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }

            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            frmOtpremnica ot = new frmOtpremnica(false);
            ot.MdiParent = this;
            ot.Dock = DockStyle.Fill;
            ot.MainForm = this;
            ot.Show();

        }

        private void sveOtpremniceNaKomisijuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            frmSveOtpremnice so = new frmSveOtpremnice();
            so.MdiParent = this;
            so.MainFormMenu = this;
            so.Show();
        }

        private void sveOtpremniceNaSkladišteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmSveOtpremniceNaSkladiste so = new frmSveOtpremniceNaSkladiste();
            so.MdiParent = this;
            so.MainFormMenu = this;
            so.Show();
        }

        private void računZaPredujamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
                {
                    return;
                }
                if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
                {
                    MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                    return;
                }

                frmAvansRacun f = new frmAvansRacun();
                f.MdiParent = this;
                f.Dock = DockStyle.Fill;
                f.MainForm = this;

                f.Show();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void sviRačuniZaPredujamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(3))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }
            frmSviAvansiRacun avansSvi = new frmSviAvansiRacun();
            avansSvi.MdiParent = this;
            avansSvi.MainFormMenu = this;
            avansSvi.Show();
        }

        private void karticaRobeNaTuđenSkladištuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Proizvodnja.frmKarticaRobeNaTudemSkladistu f = new Proizvodnja.frmKarticaRobeNaTudemSkladistu();
            f.ShowDialog();
        }

        private void knjigaPrometaKPRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report.KnjigaPrometaKPR.frmKPR2017 f = new Report.KnjigaPrometaKPR.frmKPR2017();
            f.WindowState = FormWindowState.Maximized;

            f.ShowDialog();
        }

        private void DugovanjaPartneraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(1))
            {
                MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                return;
            }

            Report.SaldaKonti.frmSaldaKonti sk = new Report.SaldaKonti.frmSaldaKonti();
            sk.ShowDialog();
        }

        private void unosPocetnogDugovanjaPartneraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
                {
                    return;
                }
                if (!Util.classZaposleniciDopustenja.provjeraZaposlenika(1))
                {
                    MessageBox.Show("Nemate potrebno ovlaštenje za pristup stavci");
                    return;
                }

                Report.SaldaKonti.frmUnosPocetnogDugovanjaPartnera f = new Report.SaldaKonti.frmUnosPocetnogDugovanjaPartnera();
                f.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void robaNaKomisijiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report.Komisija.frmRobaNaKomisiji pa = new Report.Komisija.frmRobaNaKomisiji();
            pa.ShowDialog();
        }

        private void rekapitulacijaRadnihNalogaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report.Proizvodnja.RekapitulacijaRadnihNaloga.frmRekapitulacijaRadnihNaloga f = new Report.Proizvodnja.RekapitulacijaRadnihNaloga.frmRekapitulacijaRadnihNaloga();
            f.ShowDialog();
        }

        private void rUCRazlikaUCijeniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IzlazniDokumenti.FormRUC form = new IzlazniDokumenti.FormRUC();
            form.ShowDialog();
        }

        private void robniDobropisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Robno.frmRobniDobropis form = new Robno.frmRobniDobropis
            {
                MdiParent = this,
                Dock = DockStyle.Fill
            };
            form.Show();
        }

        private void KalendarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmKalendarPopunjenosti form = new FrmKalendarPopunjenosti
            {
                MainMenu = this,
                MdiParent = this,
                Dock = DockStyle.Fill
            };
            form.Show();
        }

        private void SobeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Resort.FrmPopisSoba form = new Resort.FrmPopisSoba();
            form.ShowDialog();
        }

        private void ugostiteljskeOtpremniceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSveUgostiteljskeOtpremnice form = new FrmSveUgostiteljskeOtpremnice();
            form.Show();
        }

        private void vrstaUslugeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVrstaUsluge form = new FrmVrstaUsluge();
            form.Show();
        }

        private void popisGostijuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPopisGostiju form = new FrmPopisGostiju();
            form.Show();
        }

        private void knjigaPrometaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmKnjigaPrometa form = new FrmKnjigaPrometa();
            form.Show();
        }

        private void agencijeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAgencije form = new FrmAgencije();
            form.Show();
        }
    }
}