using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Management;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PCPOS.Class
{
    public static class Registracija
    {
        private static string _productKey = "";
        public static string productKey { get { return _productKey; } }

        private static string _activationCode = "";
        public static string activationCode { get { return _activationCode; } }

        private static int _broj = 0;
        public static int broj { get { return _broj; } }

        private static DateTime _krajRada = DateTime.Now;
        private static DateTime _zadnja_konekcija_prema_software = DateTime.Now;

        private static bool _active = true;

        public static bool _dopustenoKreiranjeNovihDokumenta = true;
        public static bool dopustenoKreiranjeNovihDokumenta { get { return _dopustenoKreiranjeNovihDokumenta; } }

        public static void GetRegistracija()
        {
            try
            {
                DataSet ds = classSQL.select_settings("select * from registracija;", "registracija");
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    _productKey = ds.Tables[0].Rows[0]["productKey"].ToString();
                    _activationCode = ds.Tables[0].Rows[0]["activationCode"].ToString();
                    _broj = Convert.ToInt32(ds.Tables[0].Rows[0]["broj"].ToString());
                    _krajRada = Convert.ToDateTime(ds.Tables[0].Rows[0]["kraj_rada"]);

                    if (ds.Tables[0].Columns.Contains("zadnja_konekcija_prema_software"))
                        _zadnja_konekcija_prema_software = Convert.ToDateTime(ds.Tables[0].Rows[0]["zadnja_konekcija_prema_software"]);

                    if ((_zadnja_konekcija_prema_software - _zadnja_konekcija_prema_software).Days < 5)
                    {
                        if (_active)
                        {
                            if (_krajRada < DateTime.Now)
                            {
                                _dopustenoKreiranjeNovihDokumenta = false;
                            }
                        }
                        else
                        {
                            _dopustenoKreiranjeNovihDokumenta = false;
                        }
                    }
                    else
                    {
                        _dopustenoKreiranjeNovihDokumenta = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static string GetMD5(string input)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            string password = s.ToString();
            return password;
        }

        public static string getUniqueID(string drive)
        {
            if (drive == string.Empty)
            {
                //Find first drive
                foreach (DriveInfo compDrive in DriveInfo.GetDrives())
                {
                    if (compDrive.IsReady)
                    {
                        drive = compDrive.RootDirectory.ToString();
                        break;
                    }
                }
            }

            if (drive.EndsWith(":\\"))
            {
                //C:\ -> C
                drive = drive.Substring(0, drive.Length - 2);
            }

            string volumeSerial = getVolumeSerial(drive);
            string cpuID = getCPUID();

            //Mix them up and remove some useless 0's
            return cpuID.Substring(13) + cpuID.Substring(1, 4) + volumeSerial + cpuID.Substring(4, 4);
        }

        private static string getVolumeSerial(string drive)
        {
            ManagementObject disk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + drive + @":""");
            disk.Get();

            string volumeSerial = disk["VolumeSerialNumber"].ToString();
            disk.Dispose();

            return volumeSerial;
        }

        private static string getCPUID()
        {
            string cpuInfo = "";
            ManagementClass managClass = new ManagementClass("win32_processor");
            ManagementObjectCollection managCollec = managClass.GetInstances();

            foreach (ManagementObject managObj in managCollec)
            {
                if (cpuInfo == "")
                {
                    //Get only the first CPU's ID
                    cpuInfo = managObj.Properties["processorID"].Value.ToString();
                    break;
                }
            }

            return cpuInfo;
        }
    }

    internal class Postavke
    {
        public const int GODINA_POCETKA_FIFO = 2128;
        public const int GODINA_POCETKA_FIFO_PC1 = 2017;
        public const string OIB_PC1 = "47165970760";

        private static bool _test_fiskalizacija = false;
        public static bool TEST_FISKALIZACIJA { get { return _test_fiskalizacija; } }

        public static int id_default_ducan { get; set; }
        public static int id_maloprodaja_naplatni_uredaj { get; set; }
        public static int id_default_skladiste { get; set; }
        public static int id_default_blagajnik { get; set; }
        public static bool on_of_cashback { get; set; }
        public static bool on_of_bodovi { get; set; }
        public static bool on_of_postotak { get; set; }
        public static string pass { get; set; }
        public static int caffe_icon_width { get; set; }
        public static int caffe_icon_height { get; set; }
        public static bool hrana_bool { get; set; }
        public static int pdv { get; set; }
        public static bool sustavPdv { get; set; }
        public static int porez_potrosnja { get; set; }
        public static bool salji_na_web { get; set; }
        public static string salji_na_web_ftp { get; set; }
        public static string salji_na_web_user { get; set; }
        public static string salji_na_web_pass { get; set; }
        public static string lokacija_sigurnosne_kopije { get; set; }
        public static bool backup_aktivnost { get; set; }
        public static bool direct_print { get; set; }
        public static bool ladicaOn { get; set; }
        public static int naplatni_uredaj_faktura { get; set; }
        public static int naplatni_uredaj_avans { get; set; }
        public static int naplatni_uredaj_faktura_bez_robe { get; set; }
        public static bool ispis_cijele_stavke { get; set; }
        public static bool napomena_na_racunu { get; set; }
        public static bool a5print { get; set; }
        public static string logopath { get; set; }
        public static bool logo { get; set; }
        public static bool veleprodaja { get; set; }
        public static bool grafovi { get; set; }
        public static string webstranica { get; set; }
        public static bool oslobodenje_pdva { get; set; }
        public static bool provjera_stanja { get; set; }
        public static string putanja_certifikat { get; set; }
        public static string certifikat_zaporka { get; set; }

        public static bool upozoriIskljucenuFiskalizaciju { get; set; }
        public static bool useVaga { get; set; }
        public static string comPort { get; set; }
        public static int baudRate { get; set; }
        public static bool robaZabraniMijenjanjeCijena { get; set; }
        public static bool nbcFakture { get; set; }
        public static bool upozoriNaMinus { get; set; }
        public static double porezNaDohodak { get; set; }
        public static bool a6print { get; set; }
        public static bool isCentrala { get; set; }
        public static bool centralaAktivno { get; set; }
        public static string centralaPoslovnica { get; set; }
        public static string putanjaZaSkeniraneFajlove { get; set; }
        public static bool skidajSkladisteProgramski { get; set; }
        public static bool fiskalizacija_faktura_prikazi_obavijest { get; set; }
        public static bool posaljiDokumenteNaWeb { get; set; }
        public static bool ispisPartnera { get; set; }
        public static bool proizvodnjaFakturaNbc { get; set; }
        private static int _idKalkulacija = 1;
        public static int idKalkulacija { get { return _idKalkulacija; } set { _idKalkulacija = value; } }
        private static int _idFaktura = 1;
        public static int idFaktura { get { return _idFaktura; } set { _idFaktura = value; } }

        private static bool _rucnoUpisivanjeKarticeKupca = false;
        public static bool rucnoUpisivanjeKarticeKupca { get { return _rucnoUpisivanjeKarticeKupca; } set { _rucnoUpisivanjeKarticeKupca = value; } }

        public static string default_poslovnica { get; set; }
        public static string maloprodaja_naplatni_uredaj { get; set; }

        private static string _koristiSkladista = "";
        public static string koristiSkladista { get { return _koristiSkladista; } set { _koristiSkladista = value; } }

        public static string gradientFrom { get; set; }
        public static string gradientTo { get; set; }

        private static bool _kolicina_u_minus = true;
        public static bool kolicina_u_minus { get { return _kolicina_u_minus; } set { _kolicina_u_minus = value; } }

        private static bool _sakrij_formu_za_prodaju_u_minus = false;
        public static bool sakrij_formu_za_prodaju_u_minus { get { return _sakrij_formu_za_prodaju_u_minus; } }

        private static bool _koristi_povratnu_naknadu = false;
        public static bool koristi_povratnu_naknadu { get { return _koristi_povratnu_naknadu; } }

        private static bool _automatski_zapisnik = false;
        public static bool automatski_zapisnik { get { return _automatski_zapisnik; } }

        private static bool _uzmi_avanse_u_promet_kase_POS = false;
        public static bool uzmi_avanse_u_promet_kase_POS { get { return _uzmi_avanse_u_promet_kase_POS; } }

        public static bool proizvodnjaMeduskladisnicaPC { get; internal set; }
        public static bool controlBox { get; internal set; }

        private static bool _maloprodaja_naplata_gotovina_button_show = true;
        public static bool maloprodaja_naplata_gotovina_button_show { get { return _maloprodaja_naplata_gotovina_button_show; } }
        private static bool _maloprodaja_naplata_kartica_button_show = true;
        public static bool maloprodaja_naplata_kartica_button_show { get { return _maloprodaja_naplata_kartica_button_show; } }

        private static bool _prodaja_automobila = false;
        public static bool prodaja_automobila { get { return _prodaja_automobila; } }

        private static bool _proizvodnja_normativ_pc = false;
        public static bool proizvodnja_normativ_pc { get { return _proizvodnja_normativ_pc; } }

        private static int _id_default_skladiste_normativ = id_default_skladiste;
        public static int id_default_skladiste_normativ { get { return _id_default_skladiste_normativ; } }

        private static bool _uzmi_rabat_u_odjavi_komisije = false;
        public static bool uzmi_rabat_u_odjavi_komisije { get { return _uzmi_rabat_u_odjavi_komisije; } }

        private static bool _dozvoli_fikaliranje_0_kn = false;
        public static bool dozvoli_fikaliranje_0_kn { get { return _dozvoli_fikaliranje_0_kn; } }

        private static bool _UDSGame = false;
        public static bool UDSGame { get { return _UDSGame; } }
        private static bool _UDSGameEmployees = false;
        public static bool UDSGameEmployees { get { return _UDSGameEmployees; } }
        public static string UDSGameApiKey { get; internal set; }

        public static void GetPostavke()
        {
            try
            {
                DataSet ds = classSQL.select_settings("select * from postavke;", "postavke");
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    id_default_ducan = Convert.ToInt32(dr["default_ducan"]);
                    id_maloprodaja_naplatni_uredaj = Convert.ToInt32(dr["default_blagajna"]);
                    id_default_skladiste = Convert.ToInt32(dr["default_skladiste"]);
                    id_default_blagajnik = Convert.ToInt32(dr["default_blagajnik"]);
                    on_of_cashback = (dr["on_off_cashback"].ToString() == "1" ? true : false);
                    on_of_bodovi = (dr["on_off_bodovi"].ToString() == "1" ? true : false);
                    on_of_postotak = (dr["on_off_postotak"].ToString() == "1" ? true : false);
                    pass = dr["pass"].ToString();
                    caffe_icon_width = Convert.ToInt32(dr["caffe_icon_width"]);
                    caffe_icon_height = Convert.ToInt32(dr["cafe_icon_height"]);
                    hrana_bool = (dr["hrana_bool"].ToString() == "1" ? true : false);
                    pdv = Convert.ToInt16(dr["pdv"]);
                    sustavPdv = (dr["sustav_pdv"].ToString() == "1" ? true : false);
                    porez_potrosnja = Convert.ToInt16(dr["porez_potrosnja"]);
                    salji_na_web = (dr["salji_na_web"].ToString() == "1" ? true : false);
                    salji_na_web_ftp = dr["salji_na_web_ftp"].ToString();
                    salji_na_web_user = dr["salji_na_web_user"].ToString();
                    salji_na_web_pass = dr["salji_na_web_pass"].ToString();
                    lokacija_sigurnosne_kopije = dr["lokacija_sigurnosne_kopije"].ToString();
                    backup_aktivnost = (dr["backup_aktivnost"].ToString() == "1" ? true : false);
                    direct_print = (dr["direct_print"].ToString() == "1" ? true : false);
                    ladicaOn = (dr["ladicaOn"].ToString() == "1" ? true : false);
                    naplatni_uredaj_faktura = Convert.ToInt32(dr["naplatni_uredaj_faktura"]);
                    naplatni_uredaj_avans = Convert.ToInt32(dr["naplatni_uredaj_avans"]);
                    naplatni_uredaj_faktura_bez_robe = Convert.ToInt32(dr["naplatni_uredaj_faktura_bez_robe"]);
                    ispis_cijele_stavke = (dr["ispis_cijele_stavke"].ToString() == "1" ? true : false);
                    napomena_na_racunu = (dr["napomena_na_racunu"].ToString() == "1" ? true : false);
                    a5print = (dr["a5print"].ToString() == "1" ? true : false);
                    logopath = dr["logopath"].ToString();
                    logo = (dr["logo"].ToString() == "1" ? true : false);
                    veleprodaja = (dr["veleprodaja"].ToString() == "1" ? true : false);
                    grafovi = (dr["grafovi"].ToString() == "1" ? true : false);
                    webstranica = dr["webstranica"].ToString();
                    oslobodenje_pdva = (dr["oslobodenje_pdva"].ToString() == "1" ? true : false);
                    provjera_stanja = (dr["provjera_stanja"].ToString() == "1" ? true : false);
                    putanja_certifikat = dr["putanja_certifikat"].ToString();
                    certifikat_zaporka = dr["certifikat_zaporka"].ToString();

                    upozoriIskljucenuFiskalizaciju = (dr["upozori_iskljucenu_fiskalizaciju"].ToString() == "1" ? true : false);
                    useVaga = (dr["useVaga"].ToString().ToLower() == "true" ? true : false);
                    comPort = dr["COMport"].ToString();
                    baudRate = Convert.ToInt32(dr["baudRate"]);
                    robaZabraniMijenjanjeCijena = (dr["roba_zabrani_mijenjanje_cijena"].ToString().ToLower() == "true" ? true : false);
                    nbcFakture = (dr["proizvodnja_faktura_nbc"].ToString().ToLower() == "true" ? true : false);
                    upozoriNaMinus = (dr["upozori_za_minus"].ToString() == "1" ? true : false);
                    porezNaDohodak = Convert.ToDouble(dr["porez_na_dohodak"]);
                    a6print = (dr["a6print"].ToString() == "1" ? true : false);
                    isCentrala = (dr["is_centrala"].ToString().ToLower() == "true" ? true : false);
                    centralaAktivno = (dr["centrala_aktivno"].ToString().ToLower() == "true" ? true : false);
                    centralaPoslovnica = dr["centrala_poslovnica"].ToString();
                    putanjaZaSkeniraneFajlove = dr["putanja_za_skenirane_fajlove"].ToString();
                    skidajSkladisteProgramski = (dr["skidaj_skladiste_programski"].ToString() == "1" ? true : false);
                    fiskalizacija_faktura_prikazi_obavijest = (dr["fiskalizacija_faktura_prikazi_obavijest"].ToString().ToLower() == "true" ? true : false);
                    posaljiDokumenteNaWeb = (dr["posalji_dokumente_na_web"].ToString() == "1" ? true : false);
                    ispisPartnera = (dr["veleprodaja"].ToString() == "1" ? true : false);
                    proizvodnjaFakturaNbc = (dr["proizvodnja_faktura_nbc"].ToString().ToLower() == "true" ? true : false);

                    bool defaultValue = false, useGradient = true;

                    if (defaultValue)
                    {
                        gradientFrom = "#EFEFF1";
                        gradientTo = "#BEC8D2";
                    }
                    else
                    {
                        if (useGradient)
                        {
                            gradientFrom = "#BEC8D2";
                            gradientTo = "#517487";
                        }
                        else
                        {
                            gradientFrom = "#5bc0de";
                            gradientTo = gradientFrom;
                        }
                    }

                    if (ds.Tables[0].Columns.Contains("idKalkulacija"))
                    {
                        idKalkulacija = Convert.ToInt16(dr["idKalkulacija"]);
                    }

                    if (ds.Tables[0].Columns.Contains("idFaktura"))
                    {
                        idFaktura = Convert.ToInt16(dr["idFaktura"]);
                    }

                    if (ds.Tables[0].Columns.Contains("rucnoUpisivanjeKarticeKupca"))
                    {
                        rucnoUpisivanjeKarticeKupca = Convert.ToBoolean(dr["rucnoUpisivanjeKarticeKupca"]);
                    }

                    if (ds.Tables[0].Columns.Contains("koristiSkladista"))
                    {
                        _koristiSkladista = dr["koristiSkladista"].ToString();
                    }

                    DataSet dsPoslovnice = classSQL.select("select * from ducan where id_ducan = '" + id_default_ducan + "';", "ducan");
                    if (dsPoslovnice != null && dsPoslovnice.Tables.Count > 0 && dsPoslovnice.Tables[0] != null && dsPoslovnice.Tables[0].Rows.Count > 0)
                    {
                        default_poslovnica = dsPoslovnice.Tables[0].Rows[0]["ime_ducana"].ToString();
                    }

                    DataSet dsBlagajne = classSQL.select("select * from blagajna where id_ducan = '" + id_default_ducan + "'", "blagajne");
                    if (dsBlagajne != null && dsBlagajne.Tables.Count > 0)
                    {
                        var results = (from d in dsBlagajne.Tables[0].AsEnumerable() where d.Field<int>("id_blagajna") == id_maloprodaja_naplatni_uredaj select d.Field<string>("ime_blagajne")).ToArray();
                        if (results != null)
                            maloprodaja_naplatni_uredaj = results[0].ToString();
                    }

                    if (ds.Tables[0].Columns.Contains("kolicina_u_minus"))
                    {
                        _kolicina_u_minus = Convert.ToBoolean(dr["kolicina_u_minus"].ToString());
                    }

                    if (ds.Tables[0].Columns.Contains("sakrij_formu_za_prodaju_u_minus"))
                    {
                        _sakrij_formu_za_prodaju_u_minus = Convert.ToBoolean(dr["sakrij_formu_za_prodaju_u_minus"].ToString());
                    }

                    if (ds.Tables[0].Columns.Contains("test_fiskalizacija"))
                    {
                        _test_fiskalizacija = Convert.ToBoolean(dr["test_fiskalizacija"].ToString());
                    }

                    if (ds.Tables[0].Columns.Contains("koristi_povratnu_naknadu"))
                    {
                        _koristi_povratnu_naknadu = Convert.ToBoolean(dr["koristi_povratnu_naknadu"].ToString());
                    }

                    if (ds.Tables[0].Columns.Contains("automatski_zapisnik"))
                    {
                        _automatski_zapisnik = Convert.ToBoolean(dr["automatski_zapisnik"].ToString());
                    }

                    if (ds.Tables[0].Columns.Contains("uzmi_avanse_u_promet_kase_POS"))
                    {
                        _uzmi_avanse_u_promet_kase_POS = Convert.ToBoolean(dr["uzmi_avanse_u_promet_kase_POS"].ToString());
                    }

                    if (ds.Tables[0].Columns.Contains("proizvodnja_meduskladisnica_pc"))
                    {
                        proizvodnjaMeduskladisnicaPC = Convert.ToBoolean(dr["proizvodnja_meduskladisnica_pc"]);
                    }

                    if (ds.Tables[0].Columns.Contains("control_box"))
                    {
                        controlBox = Convert.ToBoolean(dr["control_box"]);
                    }

                    if (ds.Tables[0].Columns.Contains("maloprodaja_naplata_gotovina_button_show"))
                    {
                        _maloprodaja_naplata_gotovina_button_show = Convert.ToBoolean(dr["maloprodaja_naplata_gotovina_button_show"]);
                    }

                    if (ds.Tables[0].Columns.Contains("maloprodaja_naplata_kartica_button_show"))
                    {
                        _maloprodaja_naplata_kartica_button_show = Convert.ToBoolean(dr["maloprodaja_naplata_kartica_button_show"]);
                    }

                    if (ds.Tables[0].Columns.Contains("prodaja_automobila"))
                    {
                        _prodaja_automobila = Convert.ToBoolean(dr["prodaja_automobila"]);
                    }

                    if (ds.Tables[0].Columns.Contains("proizvodnja_normativ_pc"))
                    {
                        _proizvodnja_normativ_pc = Convert.ToBoolean(dr["proizvodnja_normativ_pc"]);
                    }

                    if (ds.Tables[0].Columns.Contains("id_default_skladiste_normativ"))
                    {
                        _id_default_skladiste_normativ = Convert.ToInt32(dr["id_default_skladiste_normativ"]);
                    }

                    if (ds.Tables[0].Columns.Contains("uzmi_rabat_u_odjavi_komisije")) {
                        _uzmi_rabat_u_odjavi_komisije = Convert.ToBoolean(dr["uzmi_rabat_u_odjavi_komisije"]);
                    }

                    if (ds.Tables[0].Columns.Contains("dozvoli_fikaliranje_0_kn"))
                    {
                        _dozvoli_fikaliranje_0_kn = Convert.ToBoolean(dr["dozvoli_fikaliranje_0_kn"]);
                    }

                    if (ds.Tables[0].Columns.Contains("useUdsGame")) {
                        _UDSGame = Convert.ToBoolean(dr["useUdsGame"]);
                        _UDSGameEmployees = Convert.ToBoolean(dr["useUdsGameEmployees"]);
                        UDSGameApiKey = dr["useUdsGameApiKey"].ToString().Trim();
                    }


                    PodaciZaSpajanjeCompaktna.getPodaci();
                    if (!Directory.Exists(Class.Postavke.lokacija_sigurnosne_kopije))
                    {
                        Directory.CreateDirectory(Class.Postavke.lokacija_sigurnosne_kopije);
                    }

                    if (System.Environment.MachineName == "POWER-RAC") {
                        backup_aktivnost = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void changeBackground(object sender, PaintEventArgs e)
        {
            try
            {
                int widthForm = (sender as Form).ClientRectangle.Width;
                int heightForm = (sender as Form).ClientRectangle.Height;
                if (widthForm != 0 && heightForm != 0)
                {
                    bool useImg = true;
                    using (TextureBrush tBrush = new TextureBrush(SetImageOpacity(Properties.Resources.sace_pattern, 0.05F), WrapMode.Tile))
                    using (LinearGradientBrush lbrush = new LinearGradientBrush((sender as Form).ClientRectangle, ColorTranslator.FromHtml(Class.Postavke.gradientFrom), ColorTranslator.FromHtml(Class.Postavke.gradientTo), LinearGradientMode.Vertical))
                    {
                        //lbrush.SetBlendTriangularShape(0.8f, 0.3f);

                        e.Graphics.FillRectangle(lbrush, (sender as Form).ClientRectangle);

                        if (useImg)
                            e.Graphics.FillRectangle(tBrush, (sender as Form).ClientRectangle);
                    }
                    //(sender as Form).BackgroundImage = Properties.Resources.gradient_1761190_960_720;
                    //(sender as Form).BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static Image SetImageOpacity(Image image, float opacity)
        {
            Bitmap bmp = new Bitmap(image.Width, image.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                ColorMatrix matrix = new ColorMatrix();
                matrix.Matrix33 = opacity;
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default,
                                                  ColorAdjustType.Bitmap);
                g.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height),
                                   0, 0, image.Width, image.Height,
                                   GraphicsUnit.Pixel, attributes);
            }
            return bmp;
        }
    }

    internal class PodaciZaSpajanjeCompaktna
    {
        private static string _remoteServer = "localhost";
        public static string remoteServer { get { return _remoteServer; } }
        private static string _remoteUsername = "postgres";
        public static string remoteUsername { get { return _remoteUsername; } }
        private static string _remotePort = "5432";
        public static string remotePort { get { return _remotePort; } }
        private static string _remoteDb = "pos2017";
        public static string remoteDb { get { return _remoteDb; } }
        private static bool _aktivan = true;
        public static bool aktivan { get { return _aktivan; } }

        public static void getPodaci()
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";
            XDocument xmlFile = XDocument.Load(path);
            var query = from c in xmlFile.Element("settings").Elements("database_remote").Elements("postgree") select c;
            foreach (XElement book in query)
            {
                _remoteServer = book.Attribute("server").Value;
                _remoteUsername = book.Attribute("username").Value;
                _remotePort = book.Attribute("port").Value;
                _remoteDb = book.Attribute("database").Value;

                if (book.Attribute("active").Value == "1")
                {
                    _aktivan = true;
                }
                else
                {
                    _aktivan = false;
                }
            }
        }
    }

    internal class PodaciTvrtka
    {
        public static int id { get; set; }
        public static string nazivTvrtke { get; set; }
        public static string kratkiNazivTvrtke { get; set; }
        public static string oibTvrtke { get; set; }
        public static string telefonTvrtke { get; set; }
        public static string faxTvrtke { get; set; }
        public static string mobitelTvrtke { get; set; }
        public static string adresaTvrtke { get; set; }
        public static string vlasnikTvrtke { get; set; }
        public static string zrTvrtke { get; set; }
        public static int gradTvrtkaId { get; set; }
        public static string gradTvrtka { get; set; }
        public static string emailTvrtke { get; set; }
        public static string textNaKrajuDokumenta { get; set; }
        public static string adresaPoslovnice { get; set; }
        public static string gradPoslovnice { get; set; }
        public static int gradPoslovnicaId { get; set; }
        public static string iban { get; set; }
        public static string naslovFakture { get; set; }
        public static string naslovRacuna { get; set; }
        public static string tipRacuna { get; set; }
        public static string textRacuna1 { get; set; }
        public static string swift { get; set; }
        public static string pdvBr { get; set; }
        public static string textRacun2 { get; set; }
        public static bool racun { get; set; }
        public static string nazivPoslovnice { get; set; }
        public static string sifraDjelatnosti { get; internal set; }
        public static string vlasnikAdresa { get; internal set; }

        public static void GetPodaciTvrtke()
        {
            try
            {
                DataSet dsPostavke = classSQL.select_settings("select * from podaci_tvrtka;", "podaci_tvrtka");
                if (dsPostavke != null && dsPostavke.Tables.Count > 0 && dsPostavke.Tables[0] != null && dsPostavke.Tables[0].Rows.Count > 0)
                {
                    DataRow drPostavke = dsPostavke.Tables[0].Rows[0];

                    id = Convert.ToInt32(drPostavke["id"]);
                    nazivTvrtke = drPostavke["ime_tvrtke"].ToString();
                    kratkiNazivTvrtke = drPostavke["skraceno_ime"].ToString();
                    oibTvrtke = drPostavke["oib"].ToString();
                    telefonTvrtke = drPostavke["tel"].ToString();
                    faxTvrtke = drPostavke["fax"].ToString();
                    mobitelTvrtke = drPostavke["mob"].ToString();
                    adresaTvrtke = drPostavke["adresa"].ToString();
                    vlasnikTvrtke = drPostavke["vl"].ToString();
                    zrTvrtke = drPostavke["zr"].ToString();
                    gradTvrtkaId = Convert.ToInt32(drPostavke["id_grad"]);
                    emailTvrtke = drPostavke["email"].ToString();
                    textNaKrajuDokumenta = drPostavke["text_bottom"].ToString();
                    adresaPoslovnice = drPostavke["poslovnica_adresa"].ToString();
                    gradPoslovnice = drPostavke["poslovnica_grad"].ToString();

                    string sql1 = "SELECT id_grad FROM grad WHERE grad='" + gradPoslovnice + "'";
                    DataTable poslovnica = classSQL.select(sql1, "grad").Tables[0];
                    if (poslovnica.Rows.Count > 0)
                    {
                        gradPoslovnicaId = Convert.ToInt32(poslovnica.Rows[0]["id_grad"].ToString());
                    }
                    else
                    {
                        gradPoslovnicaId = 0;
                    }

                    sql1 = string.Format(@"select grad from grad where id_grad = {0}", gradTvrtkaId);
                    DataTable grad = classSQL.select(sql1, "grad").Tables[0];
                    if (grad.Rows.Count > 0)
                    {
                        gradTvrtka = grad.Rows[0]["grad"].ToString();
                    }

                    iban = drPostavke["iban"].ToString();
                    naslovFakture = drPostavke["naziv_fakture"].ToString();
                    naslovRacuna = drPostavke["naslov_racuna"].ToString();
                    tipRacuna = drPostavke["r1"].ToString();
                    textRacuna1 = drPostavke["text_racun"].ToString();
                    swift = drPostavke["swift"].ToString();
                    pdvBr = drPostavke["pdv_br"].ToString();
                    textRacun2 = drPostavke["text_racun2"].ToString();
                    racun = (drPostavke["racun_bool"].ToString() == "1" ? true : false);
                    nazivPoslovnice = drPostavke["ime_poslovnice"].ToString();
                    if (dsPostavke.Tables[0].Columns.Contains("sifra_djelatnosti"))
                    {
                        vlasnikAdresa = drPostavke["adresa_prebivalista"].ToString();
                        sifraDjelatnosti = drPostavke["sifra_djelatnosti"].ToString();
                    }
                    else
                    {
                        vlasnikAdresa = "";
                        sifraDjelatnosti = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    internal class PosPrint
    {
        public static int id { get; set; }
        public static string logicalName { get; set; }
        public static string deviceCategory { get; set; }
        public static int ispredKolicine { get; set; }
        public static int ispredCijene { get; set; }
        public static int ispredPopusta { get; set; }
        public static int ispredUkupno { get; set; }
        public static int linijaPraznihBottom { get; set; }
        public static int ispredArtikla { get; set; }
        public static string bottomText { get; set; }
        public static bool barcodePrint { get; set; }
        public static string logicalNameDrawer { get; set; }
        public static string deviceCategoryDrawer { get; set; }
        public static string drawer { get; set; }
        public static string lineDisplayLogicalName { get; set; }
        public static string lineDisplayDeviceCategory { get; set; }
        public static bool lineDisplay { get; set; }
        public static bool posPrinter { get; set; }
        public static string windowsPrinterName1 { get; set; }
        public static string windowsPrinterName2 { get; set; }
        public static int brojSlovaNaRacunu { get; set; }

        private static bool _ispisSifreNaPosPrinter = false;
        public static bool ispisSifreNaPosPrinter { get { return _ispisSifreNaPosPrinter; } internal set { _ispisSifreNaPosPrinter = value; } }

        public static void GetPosPrint()
        {
            try
            {
                DataSet dsPostavke = classSQL.select_settings("select * from pos_print;", "pos_print");
                if (dsPostavke != null && dsPostavke.Tables.Count > 0 && dsPostavke.Tables[0] != null && dsPostavke.Tables[0].Rows.Count > 0)
                {
                    DataRow drPostavke = dsPostavke.Tables[0].Rows[0];

                    id = Convert.ToInt32(drPostavke["id"]);
                    logicalName = drPostavke["logical_name"].ToString();
                    deviceCategory = drPostavke["device_category"].ToString();
                    ispredKolicine = Convert.ToInt32(drPostavke["ispred_kolicine"]);
                    ispredCijene = Convert.ToInt32(drPostavke["ispred_cijene"]);
                    ispredPopusta = Convert.ToInt32(drPostavke["ispred_popust"]);
                    ispredUkupno = Convert.ToInt32(drPostavke["ispred_ukupno"]);
                    linijaPraznihBottom = Convert.ToInt32(drPostavke["linija_praznih_bottom"]);
                    ispredArtikla = Convert.ToInt32(drPostavke["ispred_artikl"]);
                    bottomText = drPostavke["bottom_text"].ToString();
                    barcodePrint = (drPostavke["barcode_print_bool"].ToString() == "1" ? true : false);
                    logicalNameDrawer = drPostavke["logical_name_drawer"].ToString();
                    deviceCategoryDrawer = drPostavke["device_category_drawer"].ToString();
                    drawer = drPostavke["drawer_bool"].ToString();
                    lineDisplayLogicalName = drPostavke["lineDisplay_logicalName"].ToString();
                    lineDisplayDeviceCategory = drPostavke["lineDisplay_device_category"].ToString();
                    lineDisplay = (drPostavke["lineDisplay_bool"].ToString() == "1" ? true : false);
                    posPrinter = (drPostavke["posPrinterBool"].ToString() == "1" ? true : false);
                    windowsPrinterName1 = drPostavke["windows_printer_name"].ToString();
                    windowsPrinterName2 = drPostavke["windows_printer_name2"].ToString();
                    brojSlovaNaRacunu = Convert.ToInt32(drPostavke["broj_slova_na_racunu"]);

                    if (dsPostavke.Tables[0].Columns.Contains("ispis_sifre_na_pos_printer"))
                    {
                        _ispisSifreNaPosPrinter = Convert.ToBoolean(drPostavke["ispis_sifre_na_pos_printer"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    internal class Dokumenti
    {
        public static bool kalkulacije { get; set; }
        public static bool inventure { get; set; }
        public static bool kartice { get; set; }
        public static bool fakture { get; set; }
        public static bool faktureBezRobe { get; set; }
        public static bool ponude { get; set; }
        public static bool radniNalozi { get; set; }
        public static bool odjaveRobe { get; set; }
        public static bool povratDobavljacu { get; set; }
        public static bool naljepnice { get; set; }
        public static bool ulazneFakture { get; set; }
        public static bool otpremnice { get; set; }
        public static bool medjuskladisnice { get; set; }
        public static bool primke { get; set; }
        public static bool izdatnice { get; set; }
        public static bool promocije { get; set; }
        public static bool pocetnoStanje { get; set; }
        public static bool prometPoRobi { get; set; }
        public static bool kartoteka { get; set; }
        public static bool otpisRobe { get; set; }
        public static bool robno { get; set; }
        public static bool isKasica { get; set; }

        public static void GetDokumenti()
        {
            try
            {
                DataSet dsPostavke = classSQL.select_settings("select * from aktivnost_podataka;", "aktivnost_podataka");
                if (dsPostavke != null && dsPostavke.Tables.Count > 0 && dsPostavke.Tables[0] != null && dsPostavke.Tables[0].Rows.Count > 0)
                {
                    DataRow drPostavke = dsPostavke.Tables[0].Rows[0];

                    kalkulacije = Convert.ToBoolean(drPostavke["kalkulacije"]);
                    inventure = Convert.ToBoolean(drPostavke["inventura"]);
                    kartice = Convert.ToBoolean(drPostavke["kartica"]);
                    fakture = Convert.ToBoolean(drPostavke["faktura"]);
                    faktureBezRobe = Convert.ToBoolean(drPostavke["faktura_bez_robe"]);
                    ponude = Convert.ToBoolean(drPostavke["ponude"]);
                    radniNalozi = Convert.ToBoolean(drPostavke["radni_nalog"]);
                    odjaveRobe = Convert.ToBoolean(drPostavke["odjava_robe"]);
                    povratDobavljacu = Convert.ToBoolean(drPostavke["povrat_dobavljacu"]);
                    naljepnice = Convert.ToBoolean(drPostavke["naljepnice"]);
                    ulazneFakture = Convert.ToBoolean(drPostavke["ulazne_fakture"]);
                    otpremnice = Convert.ToBoolean(drPostavke["otpremnica"]);
                    medjuskladisnice = Convert.ToBoolean(drPostavke["meduskladisnice"]);
                    primke = Convert.ToBoolean(drPostavke["primke"]);
                    izdatnice = Convert.ToBoolean(drPostavke["izdatnice"]);
                    promocije = Convert.ToBoolean(drPostavke["promocije"]);
                    pocetnoStanje = Convert.ToBoolean(drPostavke["pocetno_stanje"]);
                    prometPoRobi = Convert.ToBoolean(drPostavke["promet_po_robi"]);
                    kartoteka = Convert.ToBoolean(drPostavke["kartoteka"]);
                    otpisRobe = Convert.ToBoolean(drPostavke["otpis_robe"]);
                    robno = Convert.ToBoolean(drPostavke["boolRobno"]);
                    isKasica = Convert.ToBoolean(drPostavke["isKasica"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}