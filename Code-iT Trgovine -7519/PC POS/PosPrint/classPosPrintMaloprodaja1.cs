//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Data;
//using System.Drawing;
//using System.Drawing.Printing;
//using System.Windows.Forms;
//using GenCode128;
//using System.Runtime.InteropServices;
//using System.Drawing.Text;

//namespace PCPOS.PosPrint
//{
//    class classPosPrintMaloprodaja1
//    {
//        private static DataTable DTfis = classSQL.select_settings("SELECT * FROM fiskalizacija", "fiskalizacija").Tables[0];
//        private static DataTable DTsetting = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
//        private static DataTable DT = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];
//        private static DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
//        private static DataTable DTrac;
//        private static int RecLineChars = Convert.ToInt16(DTsetting.Rows[0]["broj_slova_na_racunu"].ToString());
//        private static DataTable dt_blaganik = classSQL.select("SELECT ime,prezime,oib FROM zaposlenici WHERE id_zaposlenik='" +
//            Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0];
//        private static DataTable DTtemp;
//        private static string _1;
//        private static string _2;
//        private static string _3;
//        private static string _4;
//        private static string _5;
//        private static Image img_barcode = null;
//        private static string BrojRacuna;
//        private static string tekst;

//        private static DataTable DTpdv = new DataTable();
//        //private static DataTable DTpovratnenaknade = new DataTable();
//        private static DataTable DTPorezi = new DataTable();
//        private static DataRow RowPdv;

//        /// <summary>
//        /// DTstavke - stavke računa; blagajnik; brojRacunaDugi - br. rac. s godinom; kupac; barcode;
//        /// brRac - broj računa; ako je "" onda uzima sve račune od do datuma; plaćanje; datumi od do;
//        /// samo ispis bez fiskalizacije - ako samo želimo ispis računa onda ne treba fiskalizirati
//        /// </summary>
//        /// <param name="DTstavke"></param>
//        /// <param name="blagajnik"></param>
//        /// <param name="brojRacunaDugi"></param>
//        /// <param name="kupac"></param>
//        /// <param name="barcode"></param>
//        /// <param name="brRac"></param>
//        /// <param name="placanje"></param>
//        /// <param name="datumi"></param>
//        /// <param name="samoIspisBezFiskalizacije"></param>
//        public static void PrintReceipt(DataTable DTstavke, string blagajnik, string brojRacunaDugi,
//            string kupac, string barcode, string brRac, string placanje, DateTime[] datumi, bool samoIspisBezFiskalizacije)
//        {
//            BrojRacuna = brRac;
//            tekst = "";
//            string sql;
//            //DTpdv = new DataTable();
//            DTpdv.Clear();
//            //DTpovratnenaknade.Clear();

//            try
//            {
//                //račun je odabran
//                if (brRac != "")
//                {
//                    DTrac = classSQL.select("SELECT * FROM racuni WHERE broj_racuna='" + brRac + "'", "racuni").Tables[0];
//                }
//                //račun nije odabran pa uzima sve račune u vremenskom rasponu! (to je recimo za dnevni ispis na POS)
//                else
//                {
//                    sql = "SELECT sum(ukupno_gotovina) as ukupno_gotovina, sum(ukupno_kartice) as ukupno_kartice," +
//                        " sum(dobiveno_gotovina) as dobiveno_gotovina, sum(ukupno_virman) as ukupno_virman FROM racuni WHERE racuni.datum_racuna<'" +
//                        datumi[1] + "' AND racuni.datum_racuna>='" + datumi[0] + "'";
//                    DTrac = classSQL.select(sql, "racuni").Tables[0];
//                }

//                //--------HEADER

//                PrintReceiptHeader(DT.Rows[0]["text_racun"].ToString(), DT.Rows[0]["adresa"].ToString(),
//                    "My State, My Country", DT.Rows[0]["tel"].ToString(), DateTime.Now, blagajnik, brojRacunaDugi, kupac);

//                //--------HEADER

//                //--------BODY

//                dodajKoloneDTpdv();
//                //dodajKoloneDTpovratnaNaknada();

//                /////priprema za fiskalizaciju
//                DataTable DTOstaliPor = dodajKoloneDTOstaliPor();
//                DataTable DTnaknade = dodajKoloneDTnaknade();

//                string[] porezNaPotrosnju = setPorezNaPotrosnju();

//                string iznososlobpdv = "";
//                string iznos_marza = "";
//                /////kraj priprema za fiskalizaciju

//                //vrijednosti se izračunavaju u metodi 'PrintStavke__i__srediPorezeOsnoviceUkupno'
//                //te se po referenci prenose dalje
//                double ukupno = 0;
//                double osnovicaSve = 0;
//                double porezPotrosnjaSve = 0;
//                double pdvSve = 0;
//                double rabatSve = 0;
//                double povratnaNaknadaSve = 0;

//                DTstavke.Select("", "porez, broj_racuna");

//                //sve stavke grupira po porezima i izračunava osnovice po pdv
//                //glavni dio

//                //ako nije zadan broj računa onda uzima sve stavke računa na zadane datume od-do
//                //tu ih još jednom sumira po šifri, vpc
//                if (brRac != "" && !samoIspisBezFiskalizacije)
//                {
//                    printStavke__i__srediPorezeOsnoviceUkupno(DTstavke, ref rabatSve, ref osnovicaSve,
//                        ref pdvSve, ref ukupno, ref povratnaNaknadaSve);
//                }
//                else
//                {
//                    printStavke__i__srediPorezeOsnoviceUkupno(DTstavke, ref rabatSve, ref osnovicaSve,
//                        ref pdvSve, ref ukupno, ref povratnaNaknadaSve);
//                }

//                porezNaPotrosnju[0] = DTpostavke.Rows[0]["porez_potrosnja"].ToString();
//                porezNaPotrosnju[1] = Convert.ToString(osnovicaSve);
//                porezNaPotrosnju[2] = Convert.ToString(porezPotrosnjaSve);

//                //--------BODY

//                PrintTextLine(new string('-', RecLineChars));

//                //--------FOOTER

//                //fiskalizacija
//                string[] fiskalizacija = new string[3];
//                fiskalizacija[0] = "";
//                fiskalizacija[1] = "";
//                fiskalizacija[2] = "";
//                if (brRac != "" && !samoIspisBezFiskalizacije)
//                {
//                    fiskalizacija = vratiFiskalizaciju(DTstavke, brRac,
//                                                porezNaPotrosnju, DTOstaliPor, iznososlobpdv,
//                                                iznos_marza, DTnaknade, ukupno, placanje);
//                }

//                //ažuriraj račun sa zki i jir
//                sql = "UPDATE racuni SET zki = '" + fiskalizacija[1] + "', jir='" + fiskalizacija[0] + "'" +
//                    " WHERE broj_racuna='" + brRac + "'";
//                provjera_sql(classSQL.update(sql));

//                printReceiptFooter(osnovicaSve, pdvSve, rabatSve, ukupno, barcode, fiskalizacija, placanje, povratnaNaknadaSve);

//                //--------FOOTER
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.ToString());
//            }
//            finally
//            {
//                printaj();
//            }
//        }

//        static void printStavke__i__srediPorezeOsnoviceUkupno(DataTable DTstavke, ref double rabatSve,
//            ref double osnovicaSve, ref double pdvSve, ref double ukupno, ref double povratnaNaknadaSve)
//        {
//            double osnovicaStavka = 0;
//            double pdvStavka = 0;

//            double mnozeno = 1;

//            for (int i = 0; i < DTstavke.Rows.Count; i++)
//            {
//                //za svaku stavku izračunava osnovicu, porez za osnovicu, rabat i zapisuje u public datatable DTpdv
//                //kasnije taj datatable printa na račun

//                string sifra = DTstavke.Rows[i]["ime"].ToString();

//                //ovo zakomentirano porez na potrošnju ne treba kod maloprodaje (?)
//                double kolicina = Convert.ToDouble(DTstavke.Rows[i]["kolicina"].ToString());
//                mnozeno = kolicina >= 0 ? 1 : -1;
//                //double PP = Convert.ToDouble(DTstavke.Rows[i]["porez_potrosnja"].ToString());
//                double PDV = Convert.ToDouble(DTstavke.Rows[i]["porez"].ToString());
//                double VPC = Convert.ToDouble(DTstavke.Rows[i]["vpc"].ToString());
//                double rabat = Convert.ToDouble(DTstavke.Rows[i]["rabat"].ToString());
//                double povratnaNaknada;
//                //mora biti tak jer prije nije postojala povratna naknada!
//                try
//                {
//                    povratnaNaknada = Convert.ToDouble(DTstavke.Rows[i]["povratna_naknada"].ToString());
//                }
//                catch
//                {
//                    povratnaNaknada = 0;
//                }
//                double porez_potrosnja = Convert.ToDouble(DTstavke.Rows[i]["porez_potrosnja"].ToString());
//                //double cijena = ((VPC * (PP + PDV) / 100) + VPC);
//                double cijena = Math.Round(VPC * PDV / 100 + VPC,2);
//                double mpc = cijena * kolicina * (1 - rabat / 100);
//                mpc = Convert.ToDouble(mpc.ToString("#0.00"));

//                rabatSve += cijena * kolicina - mpc;

//                //Ovaj kod dobiva PDV
//                //double PreracunataStopaPDV = Convert.ToDouble((100 * PDV) / (100 + PDV + PP));
//                double PreracunataStopaPDV = (100 * PDV) / (100 + PDV);

//                //Ovaj kod dobiva porez na potrošnju
//                //double PreracunataStopaPorezNaPotrosnju = Convert.ToDouble((100 * PP) / (100 + PDV + PP));
//                double PreracunataStopaPorezNaPotrosnju = 100 / (100 + PDV);

//                //Porez_potrosnja_stavka = (mpc * PreracunataStopaPorezNaPotrosnju) / 100;

//                PrintLineItem(DTstavke.Rows[i]["ime"].ToString(), kolicina, cijena, rabat.ToString() + "%", mpc);

//                ////izračun porez potrosnja
//                //Porez_potrosnja_sve = (Porez_potrosnja_stavka * kolicina) + Porez_potrosnja_sve;

//                //treba smanjiti za iznos povratne naknade

//                povratnaNaknada *= mnozeno;
//                osnovicaStavka = (mpc - povratnaNaknada) / (1 + PDV / 100 + porez_potrosnja / 100);
//                pdvStavka = ((mpc - povratnaNaknada) * PreracunataStopaPDV) / 100;

//                dodajPDV(PDV, osnovicaStavka);

//                osnovicaSve += osnovicaStavka;

//                pdvSve += pdvStavka;

//                povratnaNaknadaSve += povratnaNaknada;
//            }

//            rabatSve *= mnozeno;

//            ukupno = osnovicaSve + pdvSve + povratnaNaknadaSve;// + Porez_potrosnja_sve
//        }

//        static void printStavke__i__srediPorezeOsnoviceUkupno__ZA__ISPIS(DataTable DTstavke, ref double rabatSve,
//            ref double osnovicaSve, ref double pdvSve, ref double ukupno, ref double povratnaNaknadaSve)
//        {
//            double osnovicaStavka = 0;
//            double pdvStavka = 0;

//            double mnozeno = 1;

//            for (int i = 0; i < DTstavke.Rows.Count; i++)
//            {
//                //za svaku stavku izračunava osnovicu, porez za osnovicu, rabat i zapisuje u public datatable DTpdv
//                //kasnije taj datatable printa na račun

//                string sifra = DTstavke.Rows[i]["ime"].ToString();

//                //ovo zakomentirano porez na potrošnju ne treba kod maloprodaje (?)
//                double kolicina = Convert.ToDouble(DTstavke.Rows[i]["kolicina"].ToString());
//                mnozeno = kolicina >= 0 ? 1 : -1;
//                //double PP = Convert.ToDouble(DTstavke.Rows[i]["porez_potrosnja"].ToString());
//                double PDV = Convert.ToDouble(DTstavke.Rows[i]["porez"].ToString());
//                double VPC = Convert.ToDouble(DTstavke.Rows[i]["vpc"].ToString());
//                double rabat = Convert.ToDouble(DTstavke.Rows[i]["rabat"].ToString());
//                double povratnaNaknada;
//                //mora biti tak jer prije nije postojala povratna naknada!
//                try
//                {
//                    povratnaNaknada = Convert.ToDouble(DTstavke.Rows[i]["povratna_naknada"].ToString());
//                }
//                catch
//                {
//                    povratnaNaknada = 0;
//                }
//                double porez_potrosnja = Convert.ToDouble(DTstavke.Rows[i]["porez_potrosnja"].ToString());
//                //double cijena = ((VPC * (PP + PDV) / 100) + VPC);
//                double cijena = Math.Round(VPC * PDV / 100 + VPC, 2);
//                double mpc = cijena * kolicina * (1 - rabat / 100);
//                mpc = Convert.ToDouble(mpc.ToString("#0.00"));

//                rabatSve += cijena * kolicina - mpc;

//                //Ovaj kod dobiva PDV
//                //double PreracunataStopaPDV = Convert.ToDouble((100 * PDV) / (100 + PDV + PP));
//                double PreracunataStopaPDV = (100 * PDV) / (100 + PDV);

//                //Ovaj kod dobiva porez na potrošnju
//                //double PreracunataStopaPorezNaPotrosnju = Convert.ToDouble((100 * PP) / (100 + PDV + PP));
//                double PreracunataStopaPorezNaPotrosnju = 100 / (100 + PDV);

//                //Porez_potrosnja_stavka = (mpc * PreracunataStopaPorezNaPotrosnju) / 100;

//                PrintLineItem(DTstavke.Rows[i]["ime"].ToString(), kolicina, cijena, rabat.ToString() + "%", mpc);

//                ////izračun porez potrosnja
//                //Porez_potrosnja_sve = (Porez_potrosnja_stavka * kolicina) + Porez_potrosnja_sve;

//                //treba smanjiti za iznos povratne naknade

//                povratnaNaknada *= mnozeno;
//                osnovicaStavka = (mpc - povratnaNaknada) / (1 + PDV / 100 + porez_potrosnja / 100);
//                pdvStavka = ((mpc - povratnaNaknada) * PreracunataStopaPDV) / 100;

//                dodajPDV(PDV, osnovicaStavka);

//                osnovicaSve += osnovicaStavka;

//                pdvSve += pdvStavka;

//                povratnaNaknadaSve += povratnaNaknada;
//            }

//            rabatSve *= mnozeno;

//            ukupno = osnovicaSve + pdvSve + povratnaNaknadaSve;// + Porez_potrosnja_sve
//        }

//        #region Print Footer

//        private static void printReceiptFooter(double subTotal, double tax, double discount,
//            double ukupno, string barcode, string[] fiskalizacija, string placanje, double povratnaNaknada)
//        {
//            string offSetString = new string(' ', 0);

//            //printaj kartice gotovine ostalo
//            printReceiptFooterHelper_KarticeGotovinaOstalo(
//                Convert.ToDecimal(DTrac.Rows[0]["ukupno_gotovina"].ToString()),
//                Convert.ToDecimal(DTrac.Rows[0]["ukupno_kartice"].ToString()),
//                Convert.ToDecimal(DTrac.Rows[0]["dobiveno_gotovina"].ToString())
//            );

//            //printaj poreze i osnovice po grupama, i ukupno
//            printReceiptFooterHelper_DTPDV(ukupno, povratnaNaknada);

//            //printaj fiskalizacija
//            printReceiptFooterHelper_Fiskalizacija(fiskalizacija);

//            PrintTextLine(offSetString + new string('-', RecLineChars));

//            //naša zahvala blablabla
//            printReceiptFooterHelper_Ostatak(ukupno, barcode);
//        }

//        static void printReceiptFooterHelper_DTPDV(double ukupno, double povratnaNaknada)
//        {
//            int a = Convert.ToInt16(DTsetting.Rows[0]["ispred_artikl"].ToString());
//            int k = Convert.ToInt16(DTsetting.Rows[0]["ispred_kolicine"].ToString());
//            int c = Convert.ToInt16(DTsetting.Rows[0]["ispred_cijene"].ToString());
//            int p = Convert.ToInt16(DTsetting.Rows[0]["ispred_popust"].ToString());
//            int s = Convert.ToInt16(DTsetting.Rows[0]["ispred_ukupno"].ToString());

//            //PrintText(TruncateAt(artikl.PadRight(a), a));
//            //PrintText(TruncateAt(kolicina.ToString("#0.00").PadLeft(k), k));
//            //PrintText(TruncateAt(cijena.ToString("#0.00").PadLeft(c), c));
//            //PrintText(TruncateAt(popust.ToString().PadLeft(p), p));
//            //PrintTextLine(TruncateAt(cijena_sve.ToString("#0.00").PadLeft(s), s));

//            string textPDV1, textPDV2, textOsnovica1, textOsnovica2;

//            //tu ispisuje iznose po grupama poreza!
//            for (int i = 0; i < DTpdv.Rows.Count; i++)
//            {
//                textPDV1 = "PDV" + Math.Round(Convert.ToDouble(Convert.ToDouble(DTpdv.Rows[i]["stopa"].ToString().Replace(".", ",")))).ToString("#0") + "%: ";

//                textPDV2 = Math.Round(Convert.ToDouble(DTpdv.Rows[i]["iznos"].ToString().Replace(".", ",")), 2).ToString("#0.00");
//                textOsnovica1 = "OSNOVICA:";
//                textOsnovica2 = Math.Round(Convert.ToDouble(DTpdv.Rows[i]["osnovica"].ToString().Replace(".", ",")), 2).ToString("#0.00");

//                //PrintText(TruncateAt(textPDV.PadRight(a), a));
//                //PrintTextLine(TruncateAt(textOsnovica.PadLeft(a + 1), a + 1));

//                PrintText(TruncateAt(textPDV1.PadRight(8), 8));
//                PrintText(TruncateAt(textPDV2.PadLeft(a - 8), a - 8));

//                PrintText(TruncateAt(textOsnovica1.PadLeft(k + c), k + c));
//                PrintTextLine(TruncateAt(textOsnovica2.PadLeft(p + s), p + s));
//            }

//            if (povratnaNaknada != 0)
//            {
//                PrintText(TruncateAt("POVRATNA NAKNADA: ".PadRight(a + k), a + k));
//                PrintTextLine(TruncateAt(povratnaNaknada.ToString("#0.00").PadLeft(c + p + s), c + p + s));
//            }

//            //PrintText(TruncateAt("SVE UKUPNO: ".PadRight(a), a));
//            //PrintTextLine(TruncateAt(ukupno.ToString("#0.00").PadLeft(a + 1), a + 1));
//            PrintText(TruncateAt("SVE UKUPNO: ".PadRight(a), a));
//            PrintTextLine(TruncateAt(ukupno.ToString("#0.00").PadLeft(k + c + p + s), k + c + p + s));

//            //PrintTextLine(TruncateAt(textUkupno.PadRight(a + k + c + p + s), a + k + c + p + s));
//            //PrintTextLine("SVE UKUPNO:       " + (ukupno_gotovina + ukupno_kartice).ToString("#0.00"));

//            //////ovo je prije bilo
//            ////PrintTextLine(offSetString + String.Format("OSNOVICA:  {0}", subTotal.ToString("#0.00")));
//            ////PrintTextLine(offSetString + String.Format("PDV:       {0}", tax.ToString("#0.00")));
//            ////PrintTextLine(offSetString + String.Format("POPUST:    {0}", discount.ToString("#0.00")));
//        }

//        static void printReceiptFooterHelper_KarticeGotovinaOstalo(decimal ukupno_gotovina,
//            decimal ukupno_kartice, decimal dobiveno_gotovina)
//        {
//            string za_povrat = "Plaćeno: ";

//            if (ukupno_gotovina != 0 && ukupno_kartice == 0)
//            {
//                za_povrat += "Gotovina: " + ukupno_gotovina.ToString("#0.00") + " ";
//            }
//            else if (ukupno_kartice != 0 && ukupno_gotovina == 0)
//            {
//                za_povrat += "Kartice: " + ukupno_kartice.ToString("#0.00");
//            }
//            else
//            {
//                za_povrat += "Ostalo";
//            }

//            PrintTextLine(za_povrat);

//            if (dobiveno_gotovina != 0)
//            {
//                za_povrat = "Za vratiti: " + Convert.ToDecimal(
//                    dobiveno_gotovina - ukupno_gotovina - ukupno_kartice
//                    ).ToString("#0.00") + "\r\n";
//                PrintTextLine(za_povrat);
//            }
//        }

//        static void printReceiptFooterHelper_Fiskalizacija(string[] fiskalizacija)
//        {
//            if (DTfis.Rows[0]["aktivna"].ToString() == "1")
//            {
//                tekst += "JIR:" + fiskalizacija[0] + "\r\n" + "ZKI:" + fiskalizacija[1] + "\r\n";
//            }
//        }

//        static void printReceiptFooterHelper_Ostatak(double ukupno, string barcode)
//        {
//            string offSetString = new string(' ', 0);

//            _2 = tekst;
//            tekst = "";

//            PrintTextLine(offSetString + String.Format("UKUPNO:    {0}", ukupno.ToString("#0.00")) + " kn");

//            _3 = tekst;
//            tekst = "";

//            DataTable dt = classSQL.select("SELECT traje_do,popust,aktivnost FROM promocija1", "promocija1").Tables[0];

//            if (dt.Rows[0]["aktivnost"].ToString() == "DA" && barcode != "")
//            {
//                img_barcode = Code128Rendering.MakeBarcodeImage("000" + BrojRacuna, int.Parse("3"), true);
//                tekst += offSetString + new string('-', (RecLineChars)) + Environment.NewLine;
//                tekst += "Naša zahvala za Vašu kupovinu." + Environment.NewLine;

//                double UKpopust = ukupno * Convert.ToDouble(dt.Rows[0]["popust"].ToString()) / 100;

//                tekst += UKpopust.ToString("#0.00") + " kn popusta." + Environment.NewLine + Environment.NewLine;
//                _4 = tekst;
//                tekst = "";

//                DateTime RunsUntil;
//                DateTime dvo = DateTime.Now;
//                RunsUntil = dvo.AddDays(Convert.ToInt16(dt.Rows[0]["traje_do"].ToString())); ;

//                tekst = Environment.NewLine + "Popust odgovara " + dt.Rows[0]["popust"].ToString() +
//                    "% vrijednosti kupovine \r\nkoju dobivate kod iduće kupovine. \r\nTrajanje kupona do " +
//                    RunsUntil.ToString() + Environment.NewLine;
//                tekst += "Gotovinska isplata nije moguća." + Environment.NewLine +
//                    "Iznos sljedeće kupovine mora biti jednak \r\nili veći od vrijednosti bona." + Environment.NewLine;
//            }
//            else
//            {
//                tekst = "";
//            }

//            tekst += Environment.NewLine + Environment.NewLine + DTsetting.Rows[0]["bottom_text"].ToString() + Environment.NewLine;

//            for (int i = 0; i < Convert.ToInt16(DTsetting.Rows[0]["linija_praznih_bottom"].ToString()); i++)
//            {
//                tekst += Environment.NewLine;
//            }
//            _5 = tekst;
//            tekst = "";
//        }

//        #endregion

//        private static void PrintLineItem(string artikl, double kolicina, double cijena, string popust, double cijena_sve)
//        {
//            int a = Convert.ToInt16(DTsetting.Rows[0]["ispred_artikl"].ToString());
//            int k = Convert.ToInt16(DTsetting.Rows[0]["ispred_kolicine"].ToString());
//            int c = Convert.ToInt16(DTsetting.Rows[0]["ispred_cijene"].ToString());
//            int p = Convert.ToInt16(DTsetting.Rows[0]["ispred_popust"].ToString());
//            int s = Convert.ToInt16(DTsetting.Rows[0]["ispred_ukupno"].ToString());

//            PrintText(TruncateAt(artikl.PadRight(a), a));
//            PrintText(TruncateAt(kolicina.ToString("#0.00").PadLeft(k), k));
//            PrintText(TruncateAt(cijena.ToString("#0.00").PadLeft(c), c));
//            PrintText(TruncateAt(popust.ToString().PadLeft(p), p));
//            PrintTextLine(TruncateAt(cijena_sve.ToString("#0.00").PadLeft(s), s));
//        }

//        private static void PrintReceiptHeader(string companyName, string addressLine1, string addressLine2,
//            string taxNumber, DateTime dateTime, string cashierName, string broj, string kupac)
//        {
//            PrintTextRecursive(companyName);
//            PrintText("\r\n");
//            PrintTextLine("Adresa: " + addressLine1);

//            PrintTextLine("Telefon: " + taxNumber);
//            PrintTextLine("Datum: " + DateTime.Now);
//            PrintTextLine("OIB: " + DT.Rows[0]["oib"].ToString());
//            PrintTextLine(new string('-', RecLineChars));

//            PrintTextLine(String.Format("Blagajnik : {0}", cashierName));
//            PrintTextLine(String.Format("Racun broj : {0}", broj));

//            if (kupac != "" && kupac != "0")
//            {
//                DataTable DTkupac = classSQL.select("SELECT partners.ime_tvrtke,partners.adresa,partners.oib," +
//                    "grad.grad,grad.posta FROM partners LEFT JOIN grad ON partners.id_grad=grad.id_grad WHERE id_partner='" +
//                    kupac + "'", "partners").Tables[0];
//                PrintTextLine(String.Empty);
//                PrintTextLine("Račun " + DT.Rows[0]["r1"].ToString());
//                PrintTextLine("KUPAC:");
//                PrintTextLine(DTkupac.Rows[0]["ime_tvrtke"].ToString());
//                PrintTextLine(DTkupac.Rows[0]["adresa"].ToString());
//                PrintTextLine(DTkupac.Rows[0]["grad"].ToString() + " " + DTkupac.Rows[0]["posta"].ToString());
//                PrintTextLine(DTkupac.Rows[0]["oib"].ToString());
//            }
//            _1 = tekst;
//            tekst = "";

//            int a = Convert.ToInt16(DTsetting.Rows[0]["ispred_artikl"].ToString());
//            int k = Convert.ToInt16(DTsetting.Rows[0]["ispred_kolicine"].ToString());
//            int c = Convert.ToInt16(DTsetting.Rows[0]["ispred_cijene"].ToString());
//            int p = Convert.ToInt16(DTsetting.Rows[0]["ispred_popust"].ToString());
//            int s = Convert.ToInt16(DTsetting.Rows[0]["ispred_ukupno"].ToString());

//            PrintTextLine(String.Empty);
//            PrintText(TruncateAt("STAVKA".PadRight(a), a));
//            PrintText(TruncateAt("KOL".PadLeft(k), k));
//            PrintText(TruncateAt("CIJENA".PadLeft(c), c));
//            PrintText(TruncateAt("POPUST".PadLeft(p), p));
//            PrintText(TruncateAt("UKUPNO".PadLeft(s), s));
//            PrintText("\r\n");
//            PrintTextLine(new string('=', RecLineChars));
//            //PrintTextLine(String.Empty);
//        }

//        private static void PrintTextRecursive(string text)
//        {
//            if (text.Length <= RecLineChars)
//                tekst += text;
//            else if (text.Length > RecLineChars)
//            {
//                tekst += TruncateAt(text, RecLineChars) + "\r\n";
//                text = text.Substring(RecLineChars);
//                PrintTextRecursive(text);
//            }
//        }

//        private static void PrintText(string text)
//        {
//            if (text.Length <= RecLineChars)
//                tekst += text;
//            else if (text.Length > RecLineChars)
//                tekst += TruncateAt(text, RecLineChars);
//        }

//        private static void PrintTextLine(string text)
//        {
//            if (text.Length < RecLineChars)
//                tekst += text + Environment.NewLine;
//            else if (text.Length > RecLineChars)
//                tekst += TruncateAt(text, RecLineChars);
//            else if (text.Length == RecLineChars)
//                tekst += text + Environment.NewLine;
//        }

//        private static string TruncateAt(string text, int maxWidth)
//        {
//            string retVal = text;
//            if (text.Length > maxWidth)
//                retVal = text.Substring(0, maxWidth);

//            return retVal;
//        }

//        private static void PrintPage(object o, PrintPageEventArgs e)
//        {
//            float height = 0;
//            try
//            {
//                System.Drawing.Text.PrivateFontCollection privateFonts = new PrivateFontCollection();
//                privateFonts.AddFontFile("slike/msgothic.ttc");
//                System.Drawing.Font font = new Font(privateFonts.Families[0], 10);

//                System.Drawing.Text.PrivateFontCollection privateFonts_ukupno = new PrivateFontCollection();
//                privateFonts_ukupno.AddFontFile("slike/msgothic.ttc");
//                System.Drawing.Font font_ukupno = new Font(privateFonts.Families[0], 14);

//                System.Drawing.Text.PrivateFontCollection privateFonts_mali = new PrivateFontCollection();
//                privateFonts_ukupno.AddFontFile("slike/msgothic.ttc");
//                System.Drawing.Font font_mali = new Font(privateFonts.Families[0], 9);

//                //header
//                String drawString = _1;
//                Font drawFont = font;
//                SolidBrush drawBrush = new SolidBrush(Color.Black);
//                StringFormat drawFormat = new StringFormat();
//                e.Graphics.DrawString(drawString, drawFont, drawBrush, 0, 0, drawFormat);
//                SizeF stringSize = new SizeF();
//                stringSize = e.Graphics.MeasureString(_1, drawFont);

//                height = float.Parse(stringSize.Height.ToString()) + height;

//                //stavke
//                drawString = _2;
//                drawFont = font;
//                float y = height;
//                float x = 0.0F;
//                drawFormat = new StringFormat();
//                e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
//                stringSize = e.Graphics.MeasureString(_2, drawFont);

//                height = float.Parse(stringSize.Height.ToString()) + height;

//                //Ukupno
//                drawString = _3;
//                drawFont = font_ukupno;
//                y = height;
//                x = 0.0F;
//                drawFormat = new StringFormat();
//                e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
//                stringSize = e.Graphics.MeasureString(_3, drawFont);

//                height = float.Parse(stringSize.Height.ToString()) + height;

//                //Naša zahvala
//                drawString = _4;
//                drawFont = font;
//                y = height;
//                x = 0.0F;
//                drawFormat = new StringFormat();
//                e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
//                stringSize = e.Graphics.MeasureString(_4, drawFont);

//                height = float.Parse(stringSize.Height.ToString()) + height;

//                //Barcode
//                if (img_barcode != null)
//                {
//                    System.Drawing.Image img = img_barcode;
//                    e.Graphics.DrawImage(img_barcode, 0, height, 250, 50);
//                    height = 60 + height;
//                }

//                //Bottom
//                drawString = _5;
//                drawFont = font;
//                y = height;
//                x = 0.0F;
//                drawFormat = new StringFormat();
//                e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Font za ispis nije pronađen!" + Environment.NewLine+ Environment.NewLine+
//                ex.Message,"Upozorenje!");
//            }
//        }

//        private static void printaj()
//        {
//            string printerName = DTsetting.Rows[0]["windows_printer_name"].ToString();
//            PrintDocument printDoc = new PrintDocument();

//            printDoc.PrinterSettings.PrinterName = printerName;

//            if (DTpostavke.Rows[0]["direct_print"].ToString() == "1")
//            {
//                if (DTpostavke.Rows[0]["ladicaOn"].ToString() == "1")
//                {
//                    openCashDrawer1();
//                }

//                string ttx = "\r\n" + _1 + _2 + _3 + _4 + _5;
//                ttx = ttx.Replace("č", "c");
//                ttx = ttx.Replace("Č", "C");
//                ttx = ttx.Replace("ž", "z");
//                ttx = ttx.Replace("Ž", "Z");
//                ttx = ttx.Replace("ć", "c");
//                ttx = ttx.Replace("Ć", "C");
//                ttx = ttx.Replace("đ", "d");
//                ttx = ttx.Replace("Đ", "D");
//                ttx = ttx.Replace("š", "s");
//                ttx = ttx.Replace("Š", "S");

//                string GS = Convert.ToString((char)29);
//                string ESC = Convert.ToString((char)27);

//                string COMMAND = "";
//                COMMAND = ESC + "@";
//                COMMAND += GS + "V" + (char)1;

//                RawPrinterHelper.SendStringToPrinter(printDoc.PrinterSettings.PrinterName, ttx + COMMAND);
//            }
//            else
//            {
//                if (!printDoc.PrinterSettings.IsValid)
//                {
//                    string msg = String.Format(
//                       "Can't find printer \"{0}\".", printerName);
//                    MessageBox.Show(msg, "Print Error");
//                    return;
//                }
//                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
//                printDoc.Print();
//            }
//        }

//        #region Helperi

//        /// <summary>
//        /// dodaje stopu PDV-a i iznos u tablicu DTpdv ako ne postoji stopa;
//        /// ako postoji zbraja s postojećim iznosom
//        /// </summary>
//        /// <param name="stopa"></param>
//        /// <param name="iznos"></param>
//        static void dodajPDV(double stopa, double iznos)
//        {
//            DataRow[] dataROW = DTpdv.Select("stopa = '" + stopa.ToString("0.00") + "'");

//            if (dataROW.Count() == 0)
//            {
//                RowPdv = DTpdv.NewRow();
//                RowPdv["stopa"] = stopa.ToString("0.00");
//                RowPdv["iznos"] = (iznos * stopa/100).ToString();
//                RowPdv["osnovica"] = iznos.ToString();
//                DTpdv.Rows.Add(RowPdv);
//            }
//            else
//            {
//                dataROW[0]["iznos"] = Convert.ToDouble(dataROW[0]["iznos"].ToString()) + iznos * stopa / 100;
//                dataROW[0]["osnovica"] = Convert.ToDouble(dataROW[0]["osnovica"].ToString()) + iznos;
//            }
//        }
//        //static void dodajPDV(double stopa, double iznos)
//        //{
//        //    DataRow[] dataROW = DTpdv.Select("stopa = '" + stopa.ToString() + "'");
//        //    DataRow RowPdv;

//        //    if (dataROW.Count() == 0)
//        //    {
//        //        RowPdv = DTpdv.NewRow();
//        //        RowPdv["stopa"] = Math.Round(stopa, 2).ToString("#0.00");
//        //        RowPdv["iznos"] = Math.Round(iznos * stopa / 100, 2).ToString("#0.00");
//        //        RowPdv["osnovica"] = iznos.ToString("#0.00");
//        //        DTpdv.Rows.Add(RowPdv);
//        //    }
//        //    else
//        //    {
//        //        dataROW[0]["iznos"] = (Math.Round(Convert.ToDouble(dataROW[0]["iznos"].ToString()), 2) + Math.Round(iznos * stopa / 100, 2)).ToString("#0.00");
//        //        dataROW[0]["osnovica"] = (Math.Round(Convert.ToDouble(dataROW[0]["osnovica"].ToString()), 2) + Math.Round(iznos, 2)).ToString("#0.00");
//        //    }
//        //}

//        //ne treba ova metoda, to sam ja nekaj krivo
//        static void provjeraPoreza(string pdv)
//        {
//            DTPorezi = classSQL.select("SELECT naziv, iznos FROM porezi WHERE iznos='" +
//                Convert.ToInt16(pdv).ToString() + "'", "porezi").Tables[0];

//            if (DTPorezi.Rows.Count == 0)
//            {
//                classSQL.insert("INSERT INTO porezi (naziv, iznos) VALUES (" +
//                    "'PDV " + pdv + " %'" +
//                    "'" + pdv + "'" + ")");
//            }
//        }

//        /// <summary>
//        /// Dodaje kolone tablici DTpdv ako još nisu dodane
//        /// </summary>
//        static void dodajKoloneDTpdv()
//        {
//            if (DTpdv.Columns["stopa"] == null)
//            {
//                DTpdv.Columns.Add("stopa");
//                DTpdv.Columns.Add("osnovica");
//                DTpdv.Columns.Add("iznos");
//            }
//        }

//        ///// <summary>
//        ///// Dodaje kolone tablici DTpovratnaNaknada ako još nisu dodane
//        ///// </summary>
//        //static void dodajKoloneDTpovratnaNaknada()
//        //{
//        //    if (DTpovratnenaknade.Columns["sifra"] == null)
//        //    {
//        //        DTpovratnenaknade.Columns.Add("sifra");
//        //        DTpovratnenaknade.Columns.Add("iznos");
//        //    }
//        //}

//        static DataTable dodajKoloneDTOstaliPor()
//        {
//            DataTable DTOstaliPor = new DataTable();

//            DTOstaliPor.Columns.Add("naziv");
//            DTOstaliPor.Columns.Add("stopa");
//            DTOstaliPor.Columns.Add("osnovica");
//            DTOstaliPor.Columns.Add("iznos");

//            return DTOstaliPor;
//        }

//        static DataTable dodajKoloneDTnaknade()
//        {
//            DataTable DTnaknade = new DataTable();

//            DTnaknade.Columns.Add("naziv");
//            DTnaknade.Columns.Add("iznos");

//            return DTnaknade;
//        }

//        /// <summary>
//        /// postavlja porez_na_potrosnju na empty string
//        /// </summary>
//        /// <returns></returns>
//        static string[] setPorezNaPotrosnju()
//        {
//            string[] porez_na_potrosnju = new string[3];
//            porez_na_potrosnju[0] = "";
//            porez_na_potrosnju[1] = "";
//            porez_na_potrosnju[2] = "";

//            return porez_na_potrosnju;
//        }

//        static string[] vratiFiskalizaciju(DataTable DTstavke, string brRac,
//            string[] porez_na_potrosnju, DataTable DTOstaliPor, string iznososlobpdv,
//            string iznos_marza, DataTable DTnaknade, double ukupno, string placanje)
//        {
//            bool pdv = false;
//            if (DTpostavke.Rows[0]["sustav_pdv"].ToString() == "1")
//            {
//                pdv = true;
//            }

//            if (DTfis.Rows[0]["aktivna"].ToString() == "1")
//            {
//                string[] fiskalizacija = Fiskalizacija.classFiskalizacija.Fiskalizacija(
//                    DT.Rows[0]["oib"].ToString(),
//                    dt_blaganik.Rows[0]["oib"].ToString(),
//                    DateTime.Now,
//                    pdv,
//                    Convert.ToInt32(brRac),
//                    DTpdv,
//                    porez_na_potrosnju,
//                    DTOstaliPor,
//                    iznososlobpdv,
//                    iznos_marza,
//                    DTnaknade,
//                    Convert.ToDecimal(ukupno),
//                    placanje,
//                    false,
//                    "račun"
//                    );

//                return fiskalizacija;
//            }
//            else
//            {
//                return new string[2];
//            }
//        }

//        #endregion

//        private static void provjera_sql(string str)
//        {
//            if (str != "")
//            {
//                MessageBox.Show(str);
//            }
//        }

//        private static void openCashDrawer1()
//        {
//            string printerName = DTsetting.Rows[0]["windows_printer_name"].ToString();

//            byte[] codeOpenCashDrawer = new byte[] { 27, 112, 48, 55, 121 };
//            IntPtr pUnmanagedBytes = new IntPtr(0);
//            pUnmanagedBytes = Marshal.AllocCoTaskMem(5);
//            Marshal.Copy(codeOpenCashDrawer, 0, pUnmanagedBytes, 5);
//            RawPrinterHelper.SendBytesToPrinter(printerName, pUnmanagedBytes, 5);
//            Marshal.FreeCoTaskMem(pUnmanagedBytes);
//        }
//    }
//}