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
    internal class classPosPrintPonuda
    {
        private int _broj_ponude = 0;
        public int broj_ponude { get { return _broj_ponude; } }

        private string tekst = "", _1 = "", _2 = "", _3 = "";

        private int brojZnakovaULinji = 0, brojZnakovaStavka = 0, brojZnakovaKolicina = 0, brojZnakovaCijena = 0, brojZnakovaPopust = 0, brojZnakovaUkupno = 0;

        private DataTable dtPDV;

        public classPosPrintPonuda(int broj = 0)
        {
            DataSet dsMaxBrojPonude;
            brojZnakovaStavka = Class.PosPrint.ispredArtikla;
            brojZnakovaKolicina = Class.PosPrint.ispredKolicine;
            brojZnakovaCijena = Class.PosPrint.ispredCijene;
            brojZnakovaPopust = Class.PosPrint.ispredPopusta;
            brojZnakovaUkupno = Class.PosPrint.ispredUkupno;

            brojZnakovaULinji = brojZnakovaStavka + brojZnakovaKolicina + brojZnakovaCijena + brojZnakovaPopust + brojZnakovaUkupno;

            if (broj == 0)
            {
                string sql = string.Format(@"select coalesce(max(broj_ponude), 0) as broj_ponude
from ponude
where godina_ponude::integer = {0};", Util.Korisno.GodinaKojaSeKoristiUbazi);

                dsMaxBrojPonude = classSQL.select(sql, "ponude");

                if (dsMaxBrojPonude != null && dsMaxBrojPonude.Tables.Count > 0 && dsMaxBrojPonude.Tables[0] != null && dsMaxBrojPonude.Tables[0].Rows.Count > 0)
                {
                    broj = (int)dsMaxBrojPonude.Tables[0].Rows[0]["broj_ponude"];
                }
                dsMaxBrojPonude = null;
            }

            _broj_ponude = broj;
        }

        #region helpers

        private void PrintTextLine(string text)
        {
            if (text.Length < brojZnakovaULinji)
            {
                tekst += text + Environment.NewLine;
            }
            else if (text.Length > brojZnakovaULinji)
            {
                tekst += TruncateAt(text, brojZnakovaULinji);
            }
            else if (text.Length == brojZnakovaULinji)
            {
                tekst += text + Environment.NewLine;
            }
        }

        private string TruncateAt(string text, int maxZnakova)
        {
            string retVal = text;
            if (text.Length > maxZnakova)
            {
                retVal = text.Substring(0, maxZnakova);
            }
            return retVal;
        }

        private void PrintText(string text)
        {
            if (text.Length <= brojZnakovaULinji)
            {
                tekst += text;
            }
            else if (text.Length > brojZnakovaULinji)
            {
                tekst += TruncateAt(text, brojZnakovaULinji);
            }
        }

        private void dodajKoloneDtPdv()
        {
            if (dtPDV == null)
            {
                dtPDV = new DataTable();
            }

            if (dtPDV.Columns["stopa"] == null)
            {
                dtPDV.Columns.Add(new DataColumn("stopa", typeof(int)));
                dtPDV.Columns.Add(new DataColumn("osnovica", typeof(decimal)));
                dtPDV.Columns.Add(new DataColumn("iznos", typeof(decimal)));
            }
        }

        private void dodajPDV(int stopa, decimal iznos, decimal iznosPdv)
        {
            DataRow[] dataROW = dtPDV.Select(string.Format("stopa = {0}", stopa));

            if (dataROW.Count() == 0)
            {
                DataRow RowPdv = dtPDV.NewRow();
                RowPdv["stopa"] = stopa;
                RowPdv["iznos"] = iznosPdv;
                RowPdv["osnovica"] = iznos;
                dtPDV.Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["iznos"] = (decimal)dataROW[0]["iznos"] + iznosPdv;
                dataROW[0]["osnovica"] = (decimal)dataROW[0]["osnovica"] + iznos;
            }
        }

        private void printReceiptFooterHelper_DTPDV(decimal ukupno, decimal povratnaNaknada, decimal rabat)
        {
            PrintTextLine(new string('=', brojZnakovaULinji));
            if (rabat != 0)
            {
                PrintText(TruncateAt("RABAT: ".PadRight(brojZnakovaStavka + brojZnakovaKolicina), brojZnakovaStavka + brojZnakovaKolicina));
                PrintTextLine(TruncateAt(rabat.ToString("#0.00").PadLeft(brojZnakovaCijena + brojZnakovaPopust + brojZnakovaUkupno), brojZnakovaCijena + brojZnakovaPopust + brojZnakovaUkupno));
            }

            //string textPDV1, textPDV2, textOsnovica1, textOsnovica2;
            //decimal ukupno_pdv = 0;

            //sustav_pdv
            if (Class.Postavke.sustavPdv)
            {
                //tu ispisuje iznose po grupama poreza!
                //decimal stopa, iznos, osnovica;
                int znakoviZaPostotak = brojZnakovaULinji - 5 - 10 - 9;
                int znakoviIzmeduPoreza = znakoviZaPostotak / 2;

                int ostatak = znakoviZaPostotak % 2;
                //if (znakoviZaPostotak % 2 == 1)
                //    ostatak = 1;
                if (dtPDV.Rows.Count > 0)
                {
                    PrintText("STOPA");
                    PrintText(" OSNOVICA ".PadLeft(znakoviIzmeduPoreza + 10));
                    PrintText("   PDV   ".PadLeft(znakoviIzmeduPoreza + ostatak + 9));
                    PrintText(Environment.NewLine);
                    PrintTextLine(new string('-', brojZnakovaULinji));
                    //PrintText(Environment.NewLine);
                }

                for (int i = 0; i < dtPDV.Rows.Count; i++)
                {
                    string stopa = ((int)dtPDV.Rows[i]["stopa"]).ToString() + "%";
                    string osnovica = ((decimal)dtPDV.Rows[i]["osnovica"]).ToString("#0.00");
                    string iznos = ((decimal)dtPDV.Rows[i]["iznos"]).ToString("#0.00");
                    PrintText(centerString(stopa, 5));
                    PrintText(centerString(osnovica, 10).PadLeft(znakoviIzmeduPoreza + 10));
                    PrintTextLine(centerString(iznos, 9).PadLeft(ostatak + znakoviIzmeduPoreza + 9));
                }
                PrintTextLine(new string('-', brojZnakovaULinji));
            }

            if (povratnaNaknada != 0)
            {
                PrintText(TruncateAt("POVRATNA NAKNADA: ".PadRight(brojZnakovaStavka + brojZnakovaKolicina), brojZnakovaStavka + brojZnakovaKolicina));
                PrintTextLine(TruncateAt(povratnaNaknada.ToString("#0.00").PadLeft(brojZnakovaCijena + brojZnakovaPopust + brojZnakovaUkupno), brojZnakovaCijena + brojZnakovaPopust + brojZnakovaUkupno));
            }

            PrintText(TruncateAt("SVE UKUPNO: ".PadRight(brojZnakovaStavka), brojZnakovaStavka));
            PrintTextLine(TruncateAt(ukupno.ToString("#0.00").PadLeft(brojZnakovaKolicina + brojZnakovaCijena + brojZnakovaPopust + brojZnakovaUkupno), brojZnakovaKolicina + brojZnakovaCijena + brojZnakovaPopust + brojZnakovaUkupno));
            PrintTextLine(new string('=', brojZnakovaULinji));
            if (!Class.Postavke.sustavPdv)
            {
                tekst += string.Format(@"PDV nije uračunat u cijenu prema{0}čl.90. st.1. i st.2. NN 106/18 zakona o PDV-u.{0}", Environment.NewLine);
                string offSetString = new string(' ', 0);
                PrintTextLine(offSetString + new string('-', brojZnakovaULinji));
            }
        }

        private string centerString(string tekstZaCentriranje, int brojPolja)
        {
            try
            {
                int brojPoljaZaDodanje = brojPolja - tekstZaCentriranje.Length;
                int ostatak = brojPoljaZaDodanje % 2;
                return tekstZaCentriranje.PadLeft(ostatak + brojPoljaZaDodanje).PadRight(brojPoljaZaDodanje);
            }
            catch (Exception)
            {
                return tekstZaCentriranje;
            }
        }

        #endregion helpers

        private void PrintLineItem(string artikl, decimal kolicina, decimal cijena, decimal popust, decimal iznos)
        {
            try
            {
                if (!Class.Postavke.ispis_cijele_stavke)
                {
                    PrintText(TruncateAt(artikl.PadRight(brojZnakovaStavka), brojZnakovaStavka));
                }
                else
                {
                    tekst += classPrintStavke.StavkaZaPrinter(artikl, brojZnakovaStavka);
                }
            }
            catch (Exception)
            {
                PrintText(TruncateAt(artikl.PadRight(brojZnakovaStavka), brojZnakovaStavka));
            }

            PrintText(TruncateAt(kolicina.ToString("#0.00").PadLeft(brojZnakovaKolicina), brojZnakovaKolicina));
            PrintText(TruncateAt(cijena.ToString("#0.00").PadLeft(brojZnakovaCijena), brojZnakovaCijena));
            if (brojZnakovaPopust != 0) { PrintText(TruncateAt(popust.ToString("#0.00").PadLeft(brojZnakovaPopust), brojZnakovaPopust)); }
            PrintTextLine(TruncateAt(iznos.ToString("#0.00").PadLeft(brojZnakovaUkupno), brojZnakovaUkupno));
        }

        public void printReceipt(int brojDokumenta)
        {
            try
            {
                string sql = string.Format(@"select *
from ponude
where broj_ponude = {0} and godina_ponude::integer = {1};", brojDokumenta, Util.Korisno.GodinaKojaSeKoristiUbazi);
                DataSet dsPonude = classSQL.select(sql, "ponude");

                if (dsPonude != null && dsPonude.Tables.Count > 0 && dsPonude.Tables[0] != null && dsPonude.Tables[0].Rows.Count > 0)
                {
                    DateTime datumDokumenta = new DateTime();
                    DateTime.TryParse(dsPonude.Tables[0].Rows[0]["date"].ToString(), out datumDokumenta);
                    int blagajnikID = 0, kupacID = 0;
                    int.TryParse(dsPonude.Tables[0].Rows[0]["id_zaposlenik_izradio"].ToString(), out blagajnikID);
                    int.TryParse(dsPonude.Tables[0].Rows[0]["id_odrediste"].ToString(), out kupacID);

                    sql = string.Format(@"select
sifra,
naziv,
id_skladiste,
replace(kolicina, ',', '.')::numeric as kolicina,
replace(porez, ',', '.')::numeric as porez,
replace(porez_potrosnja, ',','.')::numeric as porez_potrosnja,
vpc,
replace(rabat, ',','.')::numeric as rabat,
case when oduzmi = 'DA' then true else false end as oduzmi
from ponude_stavke
where broj_ponude = {0};", brojDokumenta);

                    DataSet dsPonudaStavke = classSQL.select(sql, "ponude_stavke");
                    if (dsPonudaStavke != null && dsPonudaStavke.Tables.Count > 0 && dsPonudaStavke.Tables[0] != null && dsPonudaStavke.Tables[0].Rows.Count > 0)
                    {
                        printHead(brojDokumenta, datumDokumenta, blagajnikID, kupacID);

                        dodajKoloneDtPdv();
                        decimal kolicina = 0, vpc = 0, mpc = 0, rabatPostotak = 0, rabatStavka = 0, rabatSve = 0, ukupnoStavka = 0, porez = 0, pdvStavka = 0, pdvUkupno = 0, osnovicaStavka = 0, osnovicaUkupno = 0, sveUkupno = 0;

                        foreach (DataRow dRow in dsPonudaStavke.Tables[0].Rows)
                        {
                            kolicina = 0;
                            vpc = 0;
                            mpc = 0;
                            rabatPostotak = 0;
                            rabatStavka = 0;
                            ukupnoStavka = 0;
                            porez = 0;
                            pdvStavka = 0;
                            osnovicaStavka = 0;

                            decimal.TryParse(dRow["kolicina"].ToString(), out kolicina);
                            decimal.TryParse(dRow["vpc"].ToString(), out vpc);
                            decimal.TryParse(dRow["rabat"].ToString(), out rabatPostotak);
                            decimal.TryParse(dRow["porez"].ToString(), out porez);
                            mpc = Math.Round((vpc * (1 + (porez / 100))), 3, MidpointRounding.AwayFromZero);

                            rabatStavka = (vpc * kolicina * rabatPostotak / 100);
                            osnovicaStavka = Math.Round((vpc * kolicina - rabatStavka), 2);
                            pdvStavka = Math.Round((osnovicaStavka * (porez / 100)), 3, MidpointRounding.AwayFromZero);

                            rabatSve += rabatStavka;
                            osnovicaUkupno += osnovicaStavka;
                            pdvUkupno += pdvStavka;

                            ukupnoStavka = (osnovicaStavka + pdvStavka);
                            sveUkupno += ukupnoStavka;

                            dodajPDV((int)porez, osnovicaStavka, pdvStavka);

                            PrintLineItem(dRow["naziv"].ToString(), kolicina, mpc, rabatStavka, ukupnoStavka);
                        }
                        printFoother(sveUkupno, rabatSve);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void printHead(int brojDokumenta, DateTime datumDokumenta, int blagajnikID, int kupacID = 0)
        {
            string sql = "", blagajnik = "";

            if (Class.PodaciTvrtka.textRacuna1 != "")
            {
                tekst += Class.PodaciTvrtka.textRacuna1 + Environment.NewLine;
            }
            else
            {
                PrintTextLine(Class.PodaciTvrtka.kratkiNazivTvrtke);
                if (Class.PodaciTvrtka.vlasnikTvrtke != "") PrintTextLine(string.Format("Vlasnik: {0}", Class.PodaciTvrtka.vlasnikTvrtke));
                if (Class.PodaciTvrtka.adresaTvrtke != "") PrintTextLine(string.Format("ADRESA: {0}", Class.PodaciTvrtka.adresaTvrtke));
                if (Class.PodaciTvrtka.adresaPoslovnice != "") PrintTextLine(string.Format("POSLOVNICA: {0}", Class.PodaciTvrtka.adresaPoslovnice));
                if (Class.PodaciTvrtka.telefonTvrtke != "") PrintTextLine(string.Format("TELEFON: {0}", Class.PodaciTvrtka.telefonTvrtke));
                if (Class.PodaciTvrtka.oibTvrtke != "") PrintTextLine(string.Format("OIB: {0}", Class.PodaciTvrtka.oibTvrtke));
            }

            PrintTextLine(string.Format("DATUM: {0}", datumDokumenta.ToString("dd.MM.yyyy. HH:mm:ss")));
            PrintTextLine(new string('-', brojZnakovaULinji));

            sql = string.Format(@"select concat(ime, ' ', prezime) as blagajnik
from zaposlenici
where id_zaposlenik = {0};", blagajnikID);

            DataSet dsBlagajnik = classSQL.select(sql, "zaposlenici");

            if (dsBlagajnik != null && dsBlagajnik.Tables.Count > 0 && dsBlagajnik.Tables[0] != null && dsBlagajnik.Tables[0].Rows.Count > 0)
                blagajnik = dsBlagajnik.Tables[0].Rows[0]["blagajnik"].ToString();

            tekst += string.Format("BLAGAJNIK: {0}{1}{0}", Environment.NewLine, blagajnik);

            string[] oibs = new string[2] { "82374273773", "82374273773" };
            if ((Array.IndexOf(oibs, Class.PodaciTvrtka.oibTvrtke)) > -1)
            {
                PrintTextLine("PONUDA");
            }
            else
            {
                PrintTextLine(string.Format("PONUDA: {0}/{1}", brojDokumenta, Util.Korisno.GodinaKojaSeKoristiUbazi));
            }

            if (kupacID > 0)
            {
                sql = string.Format(@"SELECT
partners.vrsta_korisnika,
case when partners.zacrnjeno = true then '##########' else partners.ime_tvrtke end as ime_tvrtke,
case when partners.zacrnjeno = true then '##########' else partners.adresa end as adresa,
case when partners.zacrnjeno = true then '##########' else partners.oib end as oib,
case when partners.zacrnjeno = true then '##########' else partners.napomena end as napomena,
case when partners.zacrnjeno = true then '##########' else grad.grad end as grad,
case when partners.zacrnjeno = true then '##########' else grad.posta end as posta
FROM partners
LEFT JOIN grad ON partners.id_grad = grad.id_grad
WHERE id_partner = '{0}';", kupacID);

                DataTable DTkupac = classSQL.select(sql, "partners").Tables[0];
                PrintText(Environment.NewLine);

                PrintTextLine("KUPAC:");
                PrintTextLine(DTkupac.Rows[0]["ime_tvrtke"].ToString());
                PrintTextLine(DTkupac.Rows[0]["adresa"].ToString());
                PrintTextLine(DTkupac.Rows[0]["grad"].ToString() + " " + DTkupac.Rows[0]["posta"].ToString());
                PrintTextLine(DTkupac.Rows[0]["oib"].ToString());
                if (DTkupac.Rows[0]["napomena"].ToString().Trim() != "") PrintTextLine(DTkupac.Rows[0]["napomena"].ToString());
            }

            _1 = tekst;
            tekst = "";

            PrintText(Environment.NewLine);
            PrintText(TruncateAt("STAVKA".PadRight(brojZnakovaStavka), brojZnakovaStavka));
            PrintText(TruncateAt("KOL".PadLeft(brojZnakovaKolicina), brojZnakovaKolicina));
            PrintText(TruncateAt("CIJENA".PadLeft(brojZnakovaCijena), brojZnakovaCijena));
            PrintText(TruncateAt("POP".PadLeft(brojZnakovaPopust), brojZnakovaPopust));
            PrintText(TruncateAt("UKUPNO".PadLeft(brojZnakovaUkupno), brojZnakovaUkupno));
            PrintText(Environment.NewLine);

            PrintTextLine(new string('=', brojZnakovaULinji));
        }

        private void printFoother(decimal ukupno, decimal popust)
        {
            string offSetString = new string(' ', 0);

            //printaj poreze i osnovice po grupama, i ukupno
            printReceiptFooterHelper_DTPDV(ukupno, 0, popust);

            _2 = tekst;

            PrintTextLine(offSetString + new string('=', brojZnakovaULinji));

            tekst = "";

            PrintTextLine(offSetString + string.Format("UKUPNO:    {0} kn", ukupno.ToString("#0.00")));

            _3 = tekst;

            tekst = "";
        }

        private void PrintPage(object o, PrintPageEventArgs e)
        {
            float height = 0;
            try
            {
                PrivateFontCollection privateFonts = new PrivateFontCollection();
                privateFonts.AddFontFile("slike/msgothic.ttc");
                Font font = new Font(privateFonts.Families[0], 9.5f);
                if (Class.PodaciTvrtka.oibTvrtke == "85501330524")
                {
                    font = new Font(privateFonts.Families[0], 8.5f);
                }

                PrivateFontCollection privateFonts_ukupno = new PrivateFontCollection();
                privateFonts_ukupno.AddFontFile("slike/msgothic.ttc");
                Font font_ukupno = new Font(privateFonts.Families[0], 13);
                if (Class.PodaciTvrtka.oibTvrtke == "85501330524")
                {
                    font_ukupno = new Font(privateFonts.Families[0], 11);
                }

                PrivateFontCollection privateFonts_mali = new PrivateFontCollection();
                privateFonts_ukupno.AddFontFile("slike/msgothic.ttc");
                Font font_mali = new Font(privateFonts.Families[0], 9);

                if (Class.PodaciTvrtka.oibTvrtke == "85501330524")
                {
                    font_mali = new Font(privateFonts.Families[0], 8);
                }

                try
                {
                    if (File.Exists("C://logo/logo.jpg"))
                    {
                        Image ik = Image.FromFile("C://logo/logo.jpg");
                        height = ik.Height;
                        Point pp = new Point(0, 0);
                        e.Graphics.DrawImage(ik, 0, 0, 300, 113);
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
                //drawString = this._4;
                //drawFont = font;
                //y = height;
                //x = 0.0F;
                //drawFormat = new StringFormat();
                //e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
                //stringSize = e.Graphics.MeasureString(_4, drawFont);

                //height = float.Parse(stringSize.Height.ToString()) + height;

                //Bottom
                //drawString = _5;
                //drawFont = font;
                //y = height;
                //x = 0.0F;
                //drawFormat = new StringFormat();
                //e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);

                //stringSize = e.Graphics.MeasureString(_5, drawFont);

                //height = float.Parse(stringSize.Height.ToString()) + height;

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

        public void printaj()
        {
            string printerName = Class.PosPrint.windowsPrinterName1;
            PrintDocument printDoc = new PrintDocument();

            printDoc.PrinterSettings.PrinterName = printerName;

            if (Class.Postavke.direct_print)
            {
                if (Class.Postavke.ladicaOn)
                {
                    openCashDrawer1();
                }

                string ttx = "\r\n" + _1 + _2 + _3;// + _4 + _5;
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

                for (int i = 0; i < Class.PosPrint.linijaPraznihBottom; i++)
                {
                    ttx += Environment.NewLine;
                }

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
        }

        private void openCashDrawer1()
        {
            string printerName = Class.PosPrint.windowsPrinterName1;
            byte[] codeOpenCashDrawer = new byte[] { 27, 112, 48, 55, 121 };
            IntPtr pUnmanagedBytes = new IntPtr(0);
            pUnmanagedBytes = Marshal.AllocCoTaskMem(5);
            Marshal.Copy(codeOpenCashDrawer, 0, pUnmanagedBytes, 5);
            RawPrinterHelper.SendBytesToPrinter(printerName, pUnmanagedBytes, 5);
            Marshal.FreeCoTaskMem(pUnmanagedBytes);
        }
    }
}