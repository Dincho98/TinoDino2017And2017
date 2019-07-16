using GenCode128;
using PCPOS.Util;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PCPOS.PosPrint
{
    internal class classPosPrintMaloprodaja2
    {
        private static DataTable DTfis = classSQL.select_settings("SELECT * FROM fiskalizacija", "fiskalizacija").Tables[0];
        private static DataTable DTsetting = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
        private static DataTable DT = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];
        private static DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
        private static DataTable DTrac;
        private static int RecLineChars;
        private static DataTable DTblagajnik = classSQL.select(string.Format("SELECT ime, prezime, oib FROM zaposlenici WHERE id_zaposlenik = '{0}';", Properties.Settings.Default.id_zaposlenik), "zaposlenici").Tables[0];
        private static DataTable DTtemp;
        private static string _1;
        private static string _2;
        private static string _3;
        private static string _4;
        private static string _5;
        private static Image img_barcode = null;
        private static string BrojRacuna;
        private static string tekst;
        public static bool stari_printer = false;
        private static DataTable DTpdv = new DataTable();

        //private static DataTable DTpovratnenaknade = new DataTable();
        private static DataTable DTPorezi = new DataTable();

        private static DataRow RowPdv;
        private static string maliP;
        private static Korisno korisno = new Korisno();
        private static DataTable DTstavkePublic;

        /// <summary>
        /// DTstavke - stavke računa; blagajnik; brojRacunaDugi - br. rac. s godinom; kupac; barcode;
        /// brRac - broj računa; ako je "" onda uzima sve račune od do datuma; plaćanje; datumi od do;
        /// samo ispis bez fiskalizacije - ako samo želimo ispis računa onda ne treba fiskalizirati;
        /// samo pregled računa - trenutni račun se spremi u tablicu ispis_racuni i ispis_racun_stavke,
        /// te se takav ispisuje na ekranu (NE SPREMA SE U racuni i racuni_stavke)
        /// </summary>
        /// <param name="DTstavke"></param>
        /// <param name="blagajnik"></param>
        /// <param name="brojRacunaDugi"></param>
        /// <param name="kupac"></param>
        /// <param name="barcode"></param>
        /// <param name="brRac"></param>
        /// <param name="placanje"></param>
        /// <param name="datumi"></param>
        /// <param name="samoIspisBezFiskalizacije"></param>
        public static void PrintReceipt(DataTable DTstavke, string blagajnik, string brojRacunaDugi,
            string kupac, string barcode, string brRac, string placanje, DateTime[] datumi, bool samoIspisBezFiskalizacije,
            string mali, bool samoPregledRacuna, bool ispisStavakaMaliPrinter, string id_ducan, string id_kasa, bool naknadno = false)
        {
            DTstavkePublic = DTstavke;

            //_4 mora biti empty string jer inače pamti sve od prethodnih računa
            _4 = "";

            maliP = mali;
            BrojRacuna = brRac;
            tekst = "";
            string sql;
            //DTpdv = new DataTable();
            DTpdv.Clear();
            //DTpovratnenaknade.Clear();

            try
            {
                if (!samoPregledRacuna)
                {
                    OstaleFunkcije.PovecajBrojIspisaRacuna(brRac, id_ducan, id_kasa, "RAC");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string tablicaRacuni, tablicaRacunStavke;

            if (samoPregledRacuna)
            {
                tablicaRacuni = "ispis_racuni";
                tablicaRacunStavke = "ispis_racun_stavke";
            }
            else
            {
                tablicaRacuni = "racuni";
                tablicaRacunStavke = "racun_stavke";
            }

            double ukupno = 0;
            try
            {
                DTsetting = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
                int a = Convert.ToInt16(DTsetting.Rows[0]["ispred_artikl"].ToString());
                int k = Convert.ToInt16(DTsetting.Rows[0]["ispred_kolicine"].ToString());
                int c = Convert.ToInt16(DTsetting.Rows[0]["ispred_cijene"].ToString());
                int p = Convert.ToInt16(DTsetting.Rows[0]["ispred_popust"].ToString());
                int s = Convert.ToInt16(DTsetting.Rows[0]["ispred_ukupno"].ToString());
                RecLineChars = a + k + c + p + s;

                string napomena = "";

                //račun je odabran

                if (brRac != "")
                {
                    string sqll = "SELECT * FROM " + tablicaRacuni + " WHERE broj_racuna='" + brRac + "'";
                    if (tablicaRacuni == "racuni") sqll += " AND  id_kasa=" + id_kasa + " AND id_ducan=" + id_ducan;
                    DTrac = classSQL.select(sqll, "racuni").Tables[0];

                    napomena = DTrac.Rows.Count > 0 ? DTrac.Rows[0]["napomena"].ToString() : "";
                }
                //račun nije odabran pa uzima sve račune u vremenskom rasponu! (to je recimo za dnevni ispis na POS)
                else
                {
                    sql = "SELECT sum(ukupno_gotovina-(ukupno_karticezbrojukupno_gotovina-ukupno)) as ukupno_gotovina, sum(ukupno_kartice) as ukupno_kartice," +
                        " sum(dobiveno_gotovina) as dobiveno_gotovina, sum(ukupno_virman) as ukupno_virman,sum(ukupno) as ukupno FROM racuni WHERE racuni.datum_racuna<'" +
                        datumi[1] + "' AND racuni.datum_racuna >= '" + datumi[0] + "'";
                    DTrac = classSQL.select(sql, "racuni").Tables[0];
                }

                //--------HEADER

                if (maliP == "1")
                {
                    PrintReceiptHeader(DT.Rows[0]["text_racun"].ToString(), DT.Rows[0]["adresa"].ToString(),
                    "My State, My Country", DT.Rows[0]["tel"].ToString(), datumi, blagajnik, brRac, kupac);
                }

                //--------HEADER

                //--------BODY

                dodajKoloneDTpdv();
                //dodajKoloneDTpovratnaNaknada();

                /////priprema za fiskalizaciju
                DataTable DTOstaliPor = dodajKoloneDTOstaliPor();
                DataTable DTnaknade = dodajKoloneDTnaknade();

                string[] porezNaPotrosnju = setPorezNaPotrosnju();

                string iznososlobpdv = "";
                string iznos_marza = "";
                /////kraj priprema za fiskalizaciju

                //vrijednosti se izračunavaju u metodi 'PrintStavke__i__srediPorezeOsnoviceUkupno'
                //te se po referenci prenose dalje

                double osnovicaSve = 0;
                double porezPotrosnjaSve = 0;
                double pdvSve = 0;
                double rabatSve = 0;
                double povratnaNaknadaSve = 0;

                DTstavke.Select("", "porez, broj_racuna");

                //sve stavke grupira po porezima i izračunava osnovice po pdv
                //glavni dio

                //ako nije zadan broj računa onda uzima sve stavke računa na zadane datume od-do
                //tu ih još jednom sumira po šifri, vpc
                if (brRac != "" && !samoIspisBezFiskalizacije)
                {
                    printStavke__i__srediPorezeOsnoviceUkupno(DTstavke, ref rabatSve, ref osnovicaSve,
                        ref pdvSve, ref ukupno, ref povratnaNaknadaSve, ispisStavakaMaliPrinter);
                }
                else
                {
                    printStavke__i__srediPorezeOsnoviceUkupno(DTstavke, ref rabatSve, ref osnovicaSve,
                        ref pdvSve, ref ukupno, ref povratnaNaknadaSve, ispisStavakaMaliPrinter);
                }

                porezNaPotrosnju[0] = DTpostavke.Rows[0]["porez_potrosnja"].ToString();
                porezNaPotrosnju[1] = Convert.ToString(osnovicaSve);
                porezNaPotrosnju[2] = Convert.ToString(porezPotrosnjaSve);

                if (povratnaNaknadaSve != 0)
                {
                    RowPdv = DTnaknade.NewRow();
                    RowPdv["iznos"] = povratnaNaknadaSve.ToString("0.00");
                    RowPdv["naziv"] = "Povratna naknada";
                    DTnaknade.Rows.Add(RowPdv);
                }

                //--------BODY

                if (maliP == "1") PrintTextLine(new string('=', RecLineChars));

                //--------FOOTER

                //fiskalizacija
                string[] fiskalizacija = new string[3];
                fiskalizacija[0] = "";
                fiskalizacija[1] = "";
                fiskalizacija[2] = "";
                if (brRac != "" && !samoIspisBezFiskalizacije)
                {
                    fiskalizacija = vratiFiskalizaciju(DTstavke, brRac,
                                                porezNaPotrosnju, DTOstaliPor, iznososlobpdv,
                                                iznos_marza, DTnaknade, Convert.ToDouble(DTrac.Rows[0]["ukupno"].ToString()),
                                                placanje, datumi[0], naknadno);

                    //ažuriraj račun sa zki i jir
                    sql = "UPDATE racuni SET zki = '" + fiskalizacija[1] + "', jir='" + fiskalizacija[0] + "'" +
                        " WHERE broj_racuna='" + brRac + "' AND id_ducan=" + id_ducan + " AND id_kasa=" + id_kasa;
                    provjera_sql(classSQL.update(sql));

                    if (naknadno)
                    {
                        return;
                    }
                }
                else
                {
                    if (brRac != "")
                    {
                        sql = "SELECT zki, jir FROM " + tablicaRacuni + " WHERE broj_racuna='" + brRac + "'";
                        DataTable dt = classSQL.select(sql, "racuni").Tables[0];
                        fiskalizacija[0] = dt.Rows[0]["jir"].ToString();
                        fiskalizacija[1] = dt.Rows[0]["zki"].ToString();
                        fiskalizacija[2] = "";
                    }
                }

                if (maliP == "1")
                    printReceiptFooter(osnovicaSve, pdvSve, rabatSve, ukupno, barcode,
                    fiskalizacija, placanje, povratnaNaknadaSve, napomena);

                //--------FOOTER
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (maliP == "1")
                {
                    printaj();

                    // Ova tu partizanija je namjenjena printanju "kupona" za kebabove
                    // Treba samo maknuti ovaj false na pocetku if-a, osmisliti tekst i odrediti iznos za koji se daje kupon
                    if (false && (DT.Rows[0]["oib"].ToString() == Class.Postavke.OIB_PC1) && (ukupno >= 500.00))
                    {
                        _1 = "Ovo je kupon za kupnju iznad 500.kn\r\nKoji se moze zamijeniti za\r\nza kebab u pekari Suncokret";
                        _2 = "";
                        _3 = "";
                        _4 = "";
                        _5 = "";
                        img_barcode = null;
                        printaj();
                    }
                }
            }
        }

        private static void printStavke__i__srediPorezeOsnoviceUkupno(DataTable DTstavke, ref double rabatSve,
            ref double osnovicaSve, ref double pdvSve, ref double ukupno, ref double povratnaNaknadaSve, bool ispisStavakaMaliPrinter)
        {
            double osnovicaStavka = 0;
            double pdvStavka = 0;

            double mnozeno = 1;

            double test = 0;

            for (int i = 0; i < DTstavke.Rows.Count; i++)
            {
                //za svaku stavku izračunava osnovicu, porez za osnovicu, rabat i zapisuje u public datatable DTpdv
                //kasnije taj datatable printa na račun

                string sifra = DTstavke.Rows[i]["ime"].ToString();

                //ovo zakomentirano porez na potrošnju ne treba kod maloprodaje (?)
                double kolicina = Convert.ToDouble(DTstavke.Rows[i]["kolicina"].ToString());
                mnozeno = kolicina >= 0 ? 1 : -1;
                //double PP = Convert.ToDouble(DTstavke.Rows[i]["porez_potrosnja"].ToString());
                double PDV = Convert.ToDouble(DTstavke.Rows[i]["porez"].ToString());
                double VPC = Convert.ToDouble(DTstavke.Rows[i]["vpc"].ToString());
                double MPC = Convert.ToDouble(DTstavke.Rows[i]["mpc"].ToString());
                double rabat = Convert.ToDouble(DTstavke.Rows[i]["rabat"].ToString());
                double povratnaNaknada;
                //mora biti tak jer prije nije postojala povratna naknada!
                try
                {
                    povratnaNaknada = Convert.ToDouble(DTstavke.Rows[i]["povratna_naknada"].ToString());
                }
                catch
                {
                    povratnaNaknada = 0;
                }
                double porez_potrosnja = Convert.ToDouble(DTstavke.Rows[i]["porez_potrosnja"].ToString());
                //double cijena = ((VPC * (PP + PDV) / 100) + VPC);
                //double cijena = Math.Round(VPC * PDV / 100 + VPC, 2);
                //double MPC1 = cijena;

                if (MPC != Math.Round(VPC * PDV / 100 + VPC, 2))
                {
                    MPC = MPC;
                }

                //cijena = MPC;
                //double cijena = Math.Round(VPC * PDV / 100 + VPC, 2);
                //double mpc = cijena * kolicina * (1 - rabat / 100);
                //mpc = Convert.ToDouble(mpc.ToString("#0.00"));
                //zamijenjeno 23.4.2013 s ovim dole
                double mpc = Math.Round(MPC * kolicina * (1 - rabat / 100), 2);

                rabatSve += MPC * kolicina * rabat / 100;
                //Dodao za apsolutni popust
                if (DTstavke.Rows[i]["sifra_robe"].ToString() == "!popustABS") rabatSve -= Convert.ToDouble(DTstavke.Rows[i]["cijena"].ToString());

                //Ovaj kod dobiva PDV
                //double PreracunataStopaPDV = Convert.ToDouble((100 * PDV) / (100 + PDV + PP));
                double PreracunataStopaPDV = (100 * PDV) / (100 + PDV);

                //Ovaj kod dobiva porez na potrošnju
                //double PreracunataStopaPorezNaPotrosnju = Convert.ToDouble((100 * PP) / (100 + PDV + PP));
                double PreracunataStopaPorezNaPotrosnju = 100 / (100 + PDV);

                //Porez_potrosnja_stavka = (mpc * PreracunataStopaPorezNaPotrosnju) / 100;
                string nazivArtiklaZaPrintanje = DTstavke.Rows[i]["ime"].ToString();
                if (Class.PosPrint.ispisSifreNaPosPrinter)
                {
                    nazivArtiklaZaPrintanje = DTstavke.Rows[i]["sifra_robe"].ToString() + " " + DTstavke.Rows[i]["ime"].ToString();
                }

                if (maliP == "1" && ispisStavakaMaliPrinter) PrintLineItem(nazivArtiklaZaPrintanje, kolicina, MPC, rabat.ToString() + "%", mpc);

                ////izračun porez potrosnja
                //Porez_potrosnja_sve = (Porez_potrosnja_stavka * kolicina) + Porez_potrosnja_sve;

                //treba smanjiti za iznos povratne naknade

                povratnaNaknada *= mnozeno;
                osnovicaStavka = (mpc - povratnaNaknada) / (1 + PDV / 100 + porez_potrosnja / 100);
                pdvStavka = ((mpc - povratnaNaknada) * PreracunataStopaPDV) / 100;
                pdvStavka = mpc - povratnaNaknada - osnovicaStavka;

                if (Math.Round(pdvStavka + osnovicaStavka, 2) != Math.Round(mpc - povratnaNaknada, 2))
                {
                    pdvStavka = pdvStavka;
                }

                test += mpc - povratnaNaknada;

                dodajPDV(PDV, osnovicaStavka, pdvStavka);

                osnovicaSve += osnovicaStavka;

                pdvSve += pdvStavka;

                povratnaNaknadaSve += povratnaNaknada;
            }

            rabatSve *= mnozeno;

            ukupno = osnovicaSve + pdvSve + povratnaNaknadaSve;// + Porez_potrosnja_sve
        }

        #region Print Footer

        private static void printReceiptFooter(double subTotal, double tax, double discount,
            double ukupno, string barcode, string[] fiskalizacija, string placanje, double povratnaNaknada, string napomena)
        {
            string offSetString = new string(' ', 0);

            //printaj kartice gotovine ostalo
            printReceiptFooterHelper_KarticeGotovinaOstalo(
                Convert.ToDecimal(DTrac.Rows[0]["ukupno_gotovina"].ToString()),
                Convert.ToDecimal(DTrac.Rows[0]["ukupno_kartice"].ToString()),
                Convert.ToDecimal(DTrac.Rows[0]["dobiveno_gotovina"].ToString())
            );

            //printaj poreze i osnovice po grupama, i ukupno
            printReceiptFooterHelper_DTPDV(Convert.ToDouble(DTrac.Rows[0]["ukupno"].ToString()), povratnaNaknada, discount);

            //printaj fiskalizacija
            PrintTextLine(offSetString + new string('-', RecLineChars));
            printReceiptFooterHelper_Fiskalizacija(fiskalizacija);

            PrintTextLine(offSetString + new string('=', RecLineChars));

            //naša zahvala blablabla, napomena računa
            printReceiptFooterHelper_Ostatak(ukupno, barcode, napomena);
        }

        private static void printReceiptFooterHelper_DTPDV(double ukupno, double povratnaNaknada, double rabat)
        {
            int a = Convert.ToInt16(DTsetting.Rows[0]["ispred_artikl"].ToString());
            int k = Convert.ToInt16(DTsetting.Rows[0]["ispred_kolicine"].ToString());
            int c = Convert.ToInt16(DTsetting.Rows[0]["ispred_cijene"].ToString());
            int p = Convert.ToInt16(DTsetting.Rows[0]["ispred_popust"].ToString());
            int s = Convert.ToInt16(DTsetting.Rows[0]["ispred_ukupno"].ToString());

            if (rabat != 0)
            {
                PrintText(TruncateAt("RABAT: ".PadRight(a + k), a + k));
                PrintTextLine(TruncateAt(rabat.ToString("#0.00").PadLeft(c + p + s), c + p + s));
            }

            string textPDV1, textPDV2, textOsnovica1, textOsnovica2;
            decimal ukupno_pdv = 0;
            //sustav_pdv
            if (DTpostavke.Rows[0]["sustav_pdv"].ToString() == "0")
            {
                ////PrintTextLine(offSetString + new string('-', RecLineChars));
                //PrintTextLine("PDV nije uračunat u cijenu!");
                //string offSetString = new string(' ', 0);
                //PrintTextLine(offSetString + new string('-', RecLineChars));

                //dole printa da pdv nije uračunat u cijenu
            }
            else
            {
                //tu ispisuje iznose po grupama poreza!
                double stopa, iznos, osnovica;
                for (int i = 0; i < DTpdv.Rows.Count; i++)
                {
                    stopa = Convert.ToDouble(Convert.ToDouble(DTpdv.Rows[i]["stopa"].ToString().Replace(".", ",")));
                    iznos = Convert.ToDouble(DTpdv.Rows[i]["iznos"].ToString().Replace(".", ","));
                    osnovica = Convert.ToDouble(DTpdv.Rows[i]["osnovica"].ToString().Replace(".", ","));
                    ukupno_pdv += (decimal)iznos;
                    if (iznos != 0 || osnovica != 0)
                    {
                        textPDV1 = "PDV" + Math.Round(stopa).ToString("#0") + "%: ";

                        textPDV2 = Math.Round(iznos, 2).ToString("#0.00");
                        textOsnovica1 = "OSNOVICA:";
                        textOsnovica2 = Math.Round(osnovica, 2).ToString("#0.00");

                        PrintText(TruncateAt(textPDV1.PadRight(8), 8));
                        PrintText(TruncateAt(textPDV2.PadLeft(a - 8), a - 8));

                        PrintText(TruncateAt(textOsnovica1.PadLeft(k + c), k + c));
                        PrintTextLine(TruncateAt(textOsnovica2.PadLeft(p + s), p + s));
                    }
                }
            }

            if (povratnaNaknada != 0)
            {
                PrintText(TruncateAt("POVRATNA NAKNADA: ".PadRight(a + k), a + k));
                PrintTextLine(TruncateAt(povratnaNaknada.ToString("#0.00").PadLeft(c + p + s), c + p + s));
            }

            decimal _prirezD = 0, _porezNaDohodakD = 0, _porezNaDohodakIznos = 0, _prirezIznos = 0, _porezNaDohodakSum = 0, _prirezSum = 0, _kolicina = 0;
            foreach (DataRow r in DTstavkePublic.Rows)
            {
                if (DTstavkePublic.Columns.Contains("porez_na_dohodak"))
                {
                    decimal.TryParse(r["porez_na_dohodak"].ToString().Replace(".", ","), out _porezNaDohodakD);
                }
                if (DTstavkePublic.Columns.Contains("prirez"))
                {
                    decimal.TryParse(r["prirez"].ToString().Replace(".", ","), out _prirezD);
                }
                if (DTstavkePublic.Columns.Contains("porez_na_dohodak_iznos"))
                {
                    decimal.TryParse(r["porez_na_dohodak_iznos"].ToString().Replace(".", ","), out _porezNaDohodakIznos);
                }
                if (DTstavkePublic.Columns.Contains("prirez_iznos"))
                {
                    decimal.TryParse(r["prirez_iznos"].ToString().Replace(".", ","), out _prirezIznos);
                }
                if (DTstavkePublic.Columns.Contains("kolicina"))
                {
                    decimal.TryParse(r["kolicina"].ToString().Replace(".", ","), out _kolicina);
                }

                _porezNaDohodakSum += _porezNaDohodakIznos * _kolicina;
                _prirezSum += _prirezIznos * _kolicina;
            }

            if (_porezNaDohodakSum != 0 || _prirezSum != 0)
            {
                string pnd = "POREZ NA DOHODAK (" + _porezNaDohodakD.ToString().Trim() + "%):";
                PrintText(TruncateAt(pnd.PadRight(26), 26));
                PrintTextLine(TruncateAt(Math.Round(_porezNaDohodakSum, 2).ToString("N2").PadLeft(14), 14));

                string prirez = "PRIREZ (" + _prirezD.ToString().Trim() + "%):";
                PrintText(TruncateAt(prirez.PadRight(26), 26));
                PrintTextLine(TruncateAt(Math.Round(_prirezSum, 2).ToString("N2").PadLeft(14), 14));

                string trosak = "UKUPNO TROŠAK ISPLATITELJA:";
                PrintText(TruncateAt(trosak.PadRight(28), 28));
                PrintTextLine(TruncateAt(Math.Round((decimal)ukupno + _porezNaDohodakSum + _prirezSum, 3).ToString("N2").PadLeft(12), 12));
            }

            PrintText(TruncateAt("SVE UKUPNO: ".PadRight(a), a));
            PrintTextLine(TruncateAt(ukupno.ToString("#0.00").PadLeft(k + c + p + s), k + c + p + s));

            if (DTpostavke.Rows[0]["sustav_pdv"].ToString() == "0")
            {
                tekst += "PDV nije uračunat u cijenu prema\r\nčl.90. st.1. i st.2. NN 106/18 zakona o PDV-u.\r\n";
                string offSetString = new string(' ', 0);
                PrintTextLine(offSetString + new string('-', RecLineChars));
            }
        }

        private static void printReceiptFooterHelper_KarticeGotovinaOstalo(decimal ukupno_gotovina,
            decimal ukupno_kartice, decimal dobiveno_gotovina, decimal ukupno_bon = 0)
        {
            if (DTrac.Columns.Contains("ukupno_bon"))
                decimal.TryParse(DTrac.Rows[0]["ukupno_bon"].ToString(), out ukupno_bon);

            string za_povrat = "PLAĆENO: ";

            if (ukupno_gotovina != 0)
            {
                za_povrat += "NOVČANICE: " + ukupno_gotovina.ToString("#0.00") + " ";
            }
            if (ukupno_kartice != 0)
            {
                if (za_povrat != "PLAĆENO: ")
                {
                    PrintTextLine(za_povrat);
                    za_povrat = "         ";
                }
                za_povrat += "KARTICE: " + ukupno_kartice.ToString("#0.00");
            }
            if (ukupno_bon != 0)
            {
                if (za_povrat != "PLAĆENO: ")
                {
                    PrintTextLine(za_povrat);
                    za_povrat = "         ";
                }
                za_povrat += "BON: " + ukupno_bon.ToString("#0.00");
            }

            if (ukupno_kartice == 0 && ukupno_gotovina == 0)
            {
                if (za_povrat != "PLAĆENO: ")
                {
                    PrintTextLine(za_povrat);
                    za_povrat = "";
                }
                za_povrat += "OSTALO:";
            }

            PrintTextLine(za_povrat);
            PrintTextLine("");

            if (dobiveno_gotovina != 0 && BrojRacuna != "")
            {
                za_povrat = "ZA VRATITI: " + Convert.ToDecimal(
                    dobiveno_gotovina - ukupno_gotovina - ukupno_kartice - ukupno_bon
                    ).ToString("#0.00") + "\r\n";
                PrintTextLine(za_povrat);
            }
        }

        private static void printReceiptFooterHelper_Fiskalizacija(string[] fiskalizacija)
        {
            if (DTfis.Rows[0]["aktivna"].ToString() == "1")
            {
                tekst += "JIR:" + fiskalizacija[0] + "\r\n" + "ZKI:" + fiskalizacija[1] + "\r\n";
            }
        }

        private static void printReceiptFooterHelper_Ostatak(double ukupno, string barcode, string napomena)
        {
            string offSetString = new string(' ', 0);

            _2 = tekst;
            tekst = "";

            PrintTextLine(offSetString + string.Format("UKUPNO:    {0}", Convert.ToDouble(DTrac.Rows[0]["ukupno"].ToString()).ToString("#0.00")) + " kn");

            _3 = tekst;
            tekst = "";

            if (napomena != "")
            {
                _4 += new string('-', (RecLineChars)) + Environment.NewLine;
                nap = "";
                _4 += PrintTextRecursiveString(napomena + Environment.NewLine);
            }

            DataTable dt = classSQL.select("SELECT traje_do,popust,aktivnost FROM promocija1", "promocija1").Tables[0];

            if (dt.Rows.Count > 0 && dt.Rows[0]["aktivnost"].ToString() == "DA" && barcode != "")
            {
                img_barcode = Code128Rendering.MakeBarcodeImage(/*"000" + BrojRacuna*/barcode, int.Parse("3"), true);
                tekst += offSetString + new string('-', (RecLineChars)) + Environment.NewLine;
                tekst += "Naša zahvala za Vašu kupovinu." + Environment.NewLine;

                double UKpopust = ukupno * Convert.ToDouble(dt.Rows[0]["popust"].ToString()) / 100;

                tekst += UKpopust.ToString("#0.00") + " kn popusta." + Environment.NewLine + Environment.NewLine;
                _4 += tekst;
                tekst = "";

                DateTime RunsUntil;
                DateTime dvo = DateTime.Now;
                RunsUntil = dvo.AddDays(Convert.ToInt16(dt.Rows[0]["traje_do"].ToString())); ;

                tekst = Environment.NewLine + "Popust odgovara " + dt.Rows[0]["popust"].ToString() +
                    "% vrijednosti kupovine \r\nkoju dobivate kod iduće kupovine. \r\nTrajanje kupona do " +
                    RunsUntil.ToString() + Environment.NewLine;
                tekst += "Gotovinska isplata nije moguća." + Environment.NewLine +
                    "Iznos sljedeće kupovine mora biti jednak \r\nili veći od vrijednosti bona." + Environment.NewLine;
            }
            else
            {
                tekst = "";
            }

            tekst += Environment.NewLine + Environment.NewLine + DTsetting.Rows[0]["bottom_text"].ToString() + Environment.NewLine;

            for (int i = 0; i < Convert.ToInt16(DTsetting.Rows[0]["linija_praznih_bottom"].ToString()); i++)
            {
                tekst += Environment.NewLine;
            }
            _5 = tekst;
            tekst = "";
        }

        #endregion Print Footer

        /// <summary>
        /// Globalne varijable za kolone na račinu
        /// </summary>
        public static int a = Convert.ToInt16(DTsetting.Rows[0]["ispred_artikl"].ToString());

        public static int k = Convert.ToInt16(DTsetting.Rows[0]["ispred_kolicine"].ToString());
        public static int c = Convert.ToInt16(DTsetting.Rows[0]["ispred_cijene"].ToString());
        public static int p = Convert.ToInt16(DTsetting.Rows[0]["ispred_popust"].ToString());
        public static int s = Convert.ToInt16(DTsetting.Rows[0]["ispred_ukupno"].ToString());

        private static void PrintLineItem(string artikl, double kolicina, double cijena, string popust, double cijena_sve)
        {
            try
            {
                if (DTpostavke.Rows[0]["ispis_cijele_stavke"].ToString() != "1")
                {
                    PrintText(TruncateAt(artikl.PadRight(a), a));
                }
                else
                {
                    tekst += classPrintStavke.StavkaZaPrinter(artikl, a);
                }
            }
            catch (Exception ex)
            {
                PrintText(TruncateAt(artikl.PadRight(a), a));
            }

            PrintText(TruncateAt(kolicina.ToString("#0.00").PadLeft(k), k));
            PrintText(TruncateAt(cijena.ToString("#0.00").PadLeft(c), c));
            if (p != 0) { PrintText(TruncateAt(popust.ToString().PadLeft(p), p)); }
            PrintTextLine(TruncateAt(cijena_sve.ToString("#0.00").PadLeft(s), s));
        }

        private static void PrintReceiptHeader(string companyName, string addressLine1, string addressLine2,
            string taxNumber, DateTime[] dateTime, string cashierName, string broj, string kupac)
        {
            string[] dib = Util.Korisno.VratiDucanIBlagajnu();
            string ducan = dib[0];
            string blagajna = dib[1];
            if (companyName != "")
            {
                tekst += companyName + "\r\n";
            }
            else
            {
                PrintTextLine(DT.Rows[0]["skraceno_ime"].ToString());
                if (DT.Rows[0]["vl"].ToString() != "") PrintTextLine("Vlasnik: " + DT.Rows[0]["vl"].ToString());
                if (addressLine1 != "") PrintTextLine("ADRESA: " + addressLine1);
                if (DT.Rows[0]["poslovnica_adresa"].ToString() != "") PrintTextLine("POSLOVNICA: " + DT.Rows[0]["poslovnica_adresa"].ToString());
                if (taxNumber != "") PrintTextLine("TELEFON: " + taxNumber);
                if (DT.Rows[0]["oib"].ToString() != "") PrintTextLine("OIB: " + DT.Rows[0]["oib"].ToString());
            }

            PrintTextLine("DATUM: " + dateTime[0]);
            PrintTextLine(new string('-', RecLineChars));

            tekst += "BLAGAJNIK : \r\n" + cashierName + "\r\n";
            PrintTextLine(string.Format("RAČUN BROJ : {0}", broj + "/" + ducan + "/" + blagajna));

            if (kupac != "" && kupac != "0")
            {

                string sql_partner = string.Format(@"SELECT
partners.vrsta_korisnika,
case when partners.zacrnjeno = true then '##########' else partners.ime_tvrtke end as ime_tvrtke,
case when partners.zacrnjeno = true then '##########' else partners.adresa end as adresa,
case when partners.zacrnjeno = true then '##########' else partners.oib end as oib,
case when partners.zacrnjeno = true then '##########' else partners.napomena end as napomena,
case when partners.zacrnjeno = true then '##########' else grad.grad end as grad,
case when partners.zacrnjeno = true then '##########' else grad.posta end as posta
FROM partners
LEFT JOIN grad ON partners.id_grad = grad.id_grad
WHERE id_partner = '{0}';", kupac);


                DataTable DTkupac = classSQL.select(sql_partner, "partners").Tables[0];
                PrintTextLine(string.Empty);
                if (DTkupac.Rows[0]["vrsta_korisnika"].ToString() == "1")
                {
                    PrintTextLine("RAČUN " + DT.Rows[0]["r1"].ToString() + ":");
                }
                else
                {
                    PrintTextLine("RAČUN:");
                }

                if (DT.Rows[0]["oib"].ToString() == "67521709619")
                    PrintTextLine("DOBAVLJAC:");
                else
                    PrintTextLine("KUPAC:");

                PrintTextLine(DTkupac.Rows[0]["ime_tvrtke"].ToString());
                PrintTextLine(DTkupac.Rows[0]["adresa"].ToString());
                PrintTextLine(DTkupac.Rows[0]["grad"].ToString() + " " + DTkupac.Rows[0]["posta"].ToString());
                PrintTextLine(DTkupac.Rows[0]["oib"].ToString());
                if (DTkupac.Rows[0]["napomena"].ToString().Trim() != "") PrintTextLine(DTkupac.Rows[0]["napomena"].ToString());
            }
            _1 = tekst;
            tekst = "";

            PrintTextLine(string.Empty);
            PrintText(TruncateAt("STAVKA".PadRight(a), a));
            PrintText(TruncateAt("KOL".PadLeft(k), k));
            PrintText(TruncateAt("CIJENA".PadLeft(c), c));
            PrintText(TruncateAt("POP".PadLeft(p), p));
            PrintText(TruncateAt("UKUPNO".PadLeft(s), s));
            PrintText("\r\n");
            PrintTextLine(new string('=', RecLineChars));
        }

        private static void PrintTextRecursive(string text)
        {
            if (text.Length <= RecLineChars)
                tekst += text;
            else if (text.Length > RecLineChars)
            {
                tekst += TruncateAt(text, RecLineChars) + "\r\n";
                text = text.Substring(RecLineChars);
                PrintTextRecursive(text);
            }
        }

        public static string nap { get; set; }

        private static string PrintTextRecursiveString(string text)
        {
            if (text.Length <= RecLineChars)
            {
                nap += text;
                return nap;
            }
            else
            {
                nap += TruncateAt(text, RecLineChars) + "\r\n";
                text = text.Substring(RecLineChars);
                return PrintTextRecursiveString(text);
            }
        }

        private static void PrintText(string text)
        {
            if (text.Length <= RecLineChars)
                tekst += text;
            else if (text.Length > RecLineChars)
                tekst += TruncateAt(text, RecLineChars);
        }

        private static void PrintTextLine(string text)
        {
            if (text.Length < RecLineChars)
                tekst += text + Environment.NewLine;
            else if (text.Length > RecLineChars)
                tekst += TruncateAt(text, RecLineChars);
            else if (text.Length == RecLineChars)
                tekst += text + Environment.NewLine;
        }

        private static string TruncateAt(string text, int maxWidth)
        {
            string retVal = text;
            if (text.Length > maxWidth)
                retVal = text.Substring(0, maxWidth);

            return retVal;
        }

        private static void PrintPage(object o, PrintPageEventArgs e)
        {
            float height = 0;
            try
            {
                System.Drawing.Text.PrivateFontCollection privateFonts = new PrivateFontCollection();
                privateFonts.AddFontFile("slike/msgothic.ttc");
                System.Drawing.Font font = new Font(privateFonts.Families[0], 9.5f);
                if (Class.PodaciTvrtka.oibTvrtke == "85501330524")
                {
                    font = new Font(privateFonts.Families[0], 8.5f);
                }

                System.Drawing.Text.PrivateFontCollection privateFonts_ukupno = new PrivateFontCollection();
                privateFonts_ukupno.AddFontFile("slike/msgothic.ttc");
                System.Drawing.Font font_ukupno = new Font(privateFonts.Families[0], 13);
                if (Class.PodaciTvrtka.oibTvrtke == "85501330524")
                {
                    font_ukupno = new Font(privateFonts.Families[0], 11);
                }

                System.Drawing.Text.PrivateFontCollection privateFonts_mali = new PrivateFontCollection();
                privateFonts_ukupno.AddFontFile("slike/msgothic.ttc");
                System.Drawing.Font font_mali = new Font(privateFonts.Families[0], 9);

                if (Class.PodaciTvrtka.oibTvrtke == "85501330524")
                {
                    font_mali = new Font(privateFonts.Families[0], 8);
                }

                try
                {
                    //if (File.Exists("C://logo/logo.jpg"))
                    //{
                    //    Image ik = Image.FromFile("C://logo/logo.jpg");
                    //    height = ik.Height;
                    //    Point pp = new Point(0, 0);
                    //    e.Graphics.DrawImage(ik, 0, 0, 300, 113);
                    //}
                    if (File.Exists("C://logo/logo.jpg"))
                    {
                        Image ik = Image.FromFile("C://logo/logo.jpg");
                        bool bigerWidth = false;
                        float rezol = 0;
                        if (ik.Size.Width > ik.Size.Height)
                        {
                            bigerWidth = true;
                            rezol = ik.Size.Width / ik.Size.Height;
                        }
                        else
                        {
                            rezol = ik.Size.Height / ik.Size.Width;
                        }

                        int newWidth = ik.Size.Width, newHeight = ik.Size.Height;

                        if ((bigerWidth ? ik.Size.Width : ik.Size.Height) > e.PageSettings.PrintableArea.Size.Width)
                        {
                            if (bigerWidth)
                            {
                                newWidth = (int)e.PageSettings.PrintableArea.Size.Width;
                                newHeight = (ik.Size.Height * newWidth) / ik.Size.Width;
                            }
                            else
                            {
                                newHeight = (int)e.PageSettings.PrintableArea.Size.Width;
                                newWidth = (newHeight * ik.Size.Width) / ik.Size.Height;
                            }
                        }

                        Point pp = new Point(0, 0);
                        e.Graphics.DrawImage(ik, (e.PageSettings.PrintableArea.Size.Width - newWidth) / 2, height, newWidth, newHeight);
                        //pageHeight = (int)(height + newHeight);

                        height = newHeight;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                //header
                string drawString = _1;
                Font drawFont = font;
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                StringFormat drawFormat = new StringFormat();
                e.Graphics.DrawString(drawString, drawFont, drawBrush, 0, height, drawFormat);
                SizeF stringSize = new SizeF();
                stringSize = e.Graphics.MeasureString(_1, drawFont);

                height = float.Parse(stringSize.Height.ToString()) + height;

                //stavke
                drawString = _2;
                drawFont = font;
                float y = height;
                float x = 0.0F;
                drawFormat = new StringFormat();
                e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
                stringSize = e.Graphics.MeasureString(_2, drawFont);

                height = float.Parse(stringSize.Height.ToString()) + height;

                //Ukupno
                drawString = _3;
                drawFont = font_ukupno;
                y = height;
                x = 0.0F;
                drawFormat = new StringFormat();
                e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
                stringSize = e.Graphics.MeasureString(_3, drawFont);

                height = float.Parse(stringSize.Height.ToString()) + height;

                //Naša zahvala
                drawString = _4;
                drawFont = font;
                y = height;
                x = 0.0F;
                drawFormat = new StringFormat();
                e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
                stringSize = e.Graphics.MeasureString(_4, drawFont);

                height = float.Parse(stringSize.Height.ToString()) + height;

                //Barcode
                if (img_barcode != null)
                {
                    System.Drawing.Image img = img_barcode;
                    e.Graphics.DrawImage(img_barcode, 0, height, 250, 50);
                    height = 60 + height;
                }

                //Bottom
                drawString = _5;
                drawFont = font;
                y = height;
                x = 0.0F;
                drawFormat = new StringFormat();
                e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);

                stringSize = e.Graphics.MeasureString(_5, drawFont);

                height = float.Parse(stringSize.Height.ToString()) + height;

                //if (e.HasMorePages) {
                //    e.HasMorePages = false;
                //}

                //if (height > e.PageSettings.PaperSize.Height) {
                //    PaperSize psNew = new System.Drawing.Printing.PaperSize("Racun", e.PageSettings.PrinterSettings.DefaultPageSettings.PaperSize.Width, (int)height + 1);
                //    Size sSize = new Size(psNew.Width, psNew.Height);

                //    e.PageSettings.PrinterSettings.DefaultPageSettings.PaperSize = psNew;
                //    e.PageSettings.PrinterSettings.DefaultPageSettings.Bounds.Inflate(sSize);
                //    e.PageSettings.PrinterSettings.DefaultPageSettings.PrintableArea.Inflate(sSize);
                //    e.PageSettings.PrinterSettings.DefaultPageSettings.PrinterSettings.DefaultPageSettings.PrintableArea.Inflate(sSize);

                //    e.PageSettings.PaperSize = psNew;

                //    e.PageSettings.Bounds.Inflate(sSize);

                //    e.PageBounds.Inflate(sSize);

                //    e.PageSettings.PrintableArea.Inflate(sSize);

                //    e.HasMorePages = true;
                //    e.Graphics.Clear(Color.White);
                //    e.Graphics.ResetClip();
                //    e.Graphics.Clip.MakeEmpty();

                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Font za ispis nije pronađen!" + Environment.NewLine + Environment.NewLine +
                ex.Message, "Upozorenje!");
            }
        }

        public static bool BoolPrint { get; set; }
        public static bool BoolPreview { get; set; }

        public static void printaj()
        {
            BoolPrint = false;
            if (BoolPreview)
            {
                //Dodao 24.10.2013
                Kasa.frmPosPreview p = new Kasa.frmPosPreview();
                string pom = _2;
                if (_2.Contains("JIR:"))
                {
                    pom = _2.Remove(_2.IndexOf("JIR:"));
                }
                string pom2 = _5;
                if (_5.Contains("I"))
                {
                    //pom2 = _5.Remove(_5.IndexOf('I'));
                }
                //string pom2 = _5.Remove(_5.IndexOf('I'));
                p.pregledRacuna = true;
                p.tekst = "\r\n" + _1 + pom + _3 + _4 + pom2;
                p._1 = _1;
                p._2 = pom;
                p._3 = _3;
                p._4 = _4;
                p._5 = pom2;

                //Dodao 24.10.2013
                p.ShowDialog();
                if (!BoolPrint)
                {
                    return;
                }
            }

            string printerName = DTsetting.Rows[0]["windows_printer_name"].ToString();
            PrintDocument printDoc = new PrintDocument();

            printDoc.PrinterSettings.PrinterName = printerName;

            if (stari_printer)
            {
                string ttx = "\r\n" + _1 + _2 + _3 + _4 + _5;
                ttx = ttx.Replace("č", "c");
                ttx = ttx.Replace("Č", "C");
                ttx = ttx.Replace("ž", "z");
                ttx = ttx.Replace("Ž", "Z");
                ttx = ttx.Replace("ć", "c");
                ttx = ttx.Replace("Ć", "C");
                ttx = ttx.Replace("đ", "d");
                ttx = ttx.Replace("Đ", "D");
                ttx = ttx.Replace("š", "s");
                ttx = ttx.Replace("Š", "S");

                string GS = Convert.ToString((char)29);
                string ESC = Convert.ToString((char)27);

                string COMMAND = "";
                COMMAND = ESC + "@";
                COMMAND += GS + "V" + (char)1;

                RawPrinterHelper.SendStringToPrinter(printDoc.PrinterSettings.PrinterName, ttx + COMMAND);
                BoolPreview = false;
                stari_printer = false;
                return;
            }

            if (DTpostavke.Rows[0]["direct_print"].ToString() == "1")
            {
                if (DTpostavke.Rows[0]["ladicaOn"].ToString() == "1")
                {
                    openCashDrawer1();
                }

                string ttx = "\r\n" + _1 + _2 + _3 + _4 + _5;
                ttx = ttx.Replace("č", "c");
                ttx = ttx.Replace("Č", "C");
                ttx = ttx.Replace("ž", "z");
                ttx = ttx.Replace("Ž", "Z");
                ttx = ttx.Replace("ć", "c");
                ttx = ttx.Replace("Ć", "C");
                ttx = ttx.Replace("đ", "d");
                ttx = ttx.Replace("Đ", "D");
                ttx = ttx.Replace("š", "s");
                ttx = ttx.Replace("Š", "S");

                string GS = Convert.ToString((char)29);
                string ESC = Convert.ToString((char)27);

                string COMMAND = "";
                COMMAND = ESC + "@";
                COMMAND += GS + "V" + (char)1;

                RawPrinterHelper.SendStringToPrinter(printDoc.PrinterSettings.PrinterName, ttx + COMMAND);
            }
            else
            {
                if (!printDoc.PrinterSettings.IsValid)
                {
                    string msg = string.Format(
                       "Can't find printer \"{0}\".", printerName);
                    MessageBox.Show(msg, "Print Error");
                    return;
                }
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                printDoc.Print();
            }
            BoolPreview = false;
            BoolPrint = false;
        }

        #region Helperi

        /// <summary>
        /// dodaje stopu PDV-a i iznos u tablicu DTpdv ako ne postoji stopa;
        /// ako postoji zbraja s postojećim iznosom
        /// </summary>
        /// <param name="stopa"></param>
        /// <param name="iznos"></param>
        private static void dodajPDV(double stopa, double iznos, double iznosPdv)
        {
            DataRow[] dataROW = DTpdv.Select("stopa = '" + stopa.ToString("0.00") + "'");

            if (dataROW.Count() == 0)
            {
                RowPdv = DTpdv.NewRow();
                RowPdv["stopa"] = stopa.ToString("0.00");
                RowPdv["iznos"] = iznosPdv.ToString();
                RowPdv["osnovica"] = iznos.ToString();
                DTpdv.Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["iznos"] = Convert.ToDouble(dataROW[0]["iznos"].ToString()) + iznosPdv;
                dataROW[0]["osnovica"] = Convert.ToDouble(dataROW[0]["osnovica"].ToString()) + iznos;
            }
        }

        //static void dodajPDV(double stopa, double iznos)
        //{
        //    DataRow[] dataROW = DTpdv.Select("stopa = '" + stopa.ToString() + "'");
        //    DataRow RowPdv;

        //    if (dataROW.Count() == 0)
        //    {
        //        RowPdv = DTpdv.NewRow();
        //        RowPdv["stopa"] = Math.Round(stopa, 2).ToString("#0.00");
        //        RowPdv["iznos"] = Math.Round(iznos * stopa / 100, 2).ToString("#0.00");
        //        RowPdv["osnovica"] = iznos.ToString("#0.00");
        //        DTpdv.Rows.Add(RowPdv);
        //    }
        //    else
        //    {
        //        dataROW[0]["iznos"] = (Math.Round(Convert.ToDouble(dataROW[0]["iznos"].ToString()), 2) + Math.Round(iznos * stopa / 100, 2)).ToString("#0.00");
        //        dataROW[0]["osnovica"] = (Math.Round(Convert.ToDouble(dataROW[0]["osnovica"].ToString()), 2) + Math.Round(iznos, 2)).ToString("#0.00");
        //    }
        //}

        //ne treba ova metoda, to sam ja nekaj krivo
        //static void provjeraPoreza(string pdv)
        //{
        //    DTPorezi = classSQL.select("SELECT naziv, iznos FROM porezi WHERE iznos='" +
        //        Convert.ToInt16(pdv).ToString() + "'", "porezi").Tables[0];

        //    if (DTPorezi.Rows.Count == 0)
        //    {
        //        classSQL.insert("INSERT INTO porezi (naziv, iznos) VALUES (" +
        //            "'PDV " + pdv + " %'" +
        //            "'" + pdv + "'" + ")");
        //    }
        //}

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

        ///// <summary>
        ///// Dodaje kolone tablici DTpovratnaNaknada ako još nisu dodane
        ///// </summary>
        //static void dodajKoloneDTpovratnaNaknada()
        //{
        //    if (DTpovratnenaknade.Columns["sifra"] == null)
        //    {
        //        DTpovratnenaknade.Columns.Add("sifra");
        //        DTpovratnenaknade.Columns.Add("iznos");
        //    }
        //}

        public static DataTable dodajKoloneDTOstaliPor()
        {
            DataTable DTOstaliPor = new DataTable();

            DTOstaliPor.Columns.Add("naziv");
            DTOstaliPor.Columns.Add("stopa");
            DTOstaliPor.Columns.Add("osnovica");
            DTOstaliPor.Columns.Add("iznos");

            return DTOstaliPor;
        }

        public static DataTable dodajKoloneDTnaknade()
        {
            DataTable DTnaknade = new DataTable();

            DTnaknade.Columns.Add("naziv");
            DTnaknade.Columns.Add("iznos");

            return DTnaknade;
        }

        /// <summary>
        /// postavlja porez_na_potrosnju na empty string
        /// </summary>
        /// <returns></returns>
        public static string[] setPorezNaPotrosnju()
        {
            string[] porez_na_potrosnju = new string[3];
            porez_na_potrosnju[0] = "";
            porez_na_potrosnju[1] = "";
            porez_na_potrosnju[2] = "";

            return porez_na_potrosnju;
        }

        public static string[] vratiFiskalizaciju(DataTable DTstavke, string brRac,
            string[] porez_na_potrosnju, DataTable DTOstaliPor, string iznososlobpdv,
            string iznos_marza, DataTable DTnaknade, double ukupno, string placanje, DateTime datum, bool naknadno = false)
        {
            bool pdv = false;
            if (DTpostavke.Rows[0]["sustav_pdv"].ToString() == "1")
            {
                pdv = true;
            }

            if (DTfis.Rows[0]["aktivna"].ToString() == "1")
            {
                string[] fiskalizacija = Fiskalizacija.classFiskalizacija.Fiskalizacija(
                    DT.Rows[0]["oib"].ToString(),
                    DTblagajnik.Rows[0]["oib"].ToString(),
                    datum,
                    pdv,
                    Convert.ToInt32(brRac),
                    DTpdv,
                    porez_na_potrosnju,
                    DTOstaliPor,
                    iznososlobpdv,
                    iznos_marza,
                    DTnaknade,
                    Convert.ToDecimal(ukupno),
                    placanje,
                    naknadno,
                    "račun"
                    );

                return fiskalizacija;
            }
            else
            {
                return new string[2];
            }
        }

        #endregion Helperi

        private static void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private static void openCashDrawer1()
        {
            string printerName = DTsetting.Rows[0]["windows_printer_name"].ToString();

            byte[] codeOpenCashDrawer = new byte[] { 27, 112, 48, 55, 121 };
            IntPtr pUnmanagedBytes = new IntPtr(0);
            pUnmanagedBytes = Marshal.AllocCoTaskMem(5);
            Marshal.Copy(codeOpenCashDrawer, 0, pUnmanagedBytes, 5);
            RawPrinterHelper.SendBytesToPrinter(printerName, pUnmanagedBytes, 5);
            Marshal.FreeCoTaskMem(pUnmanagedBytes);
        }
    }
}