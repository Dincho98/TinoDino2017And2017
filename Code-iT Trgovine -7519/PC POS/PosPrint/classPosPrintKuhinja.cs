using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace PCPOS.PosPrint
{
    internal class classPosPrintKuhinja
    {
        private static DataTable DTsetting = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
        private static DataTable DT = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];
        private static DataTable DTrac;
        private static DataTable dt = classSQL.select("SELECT traje_do,popust,aktivnost FROM promocija1", "promocija1").Tables[0];

        private static bool ima_stavke_za_kuhinju = false;

        private static string tekst;

        private static int RecLineChars = Convert.ToInt16(DTsetting.Rows[0]["broj_slova_na_racunu"].ToString());

        private static string _1;
        private static string _2;
        private static string _3;

        private static Image img_barcode = null;

        public static void PrintReceipt(string blagajnik, string broj_racuna)
        {
            tekst = "";
            try
            {
                string sql = "SELECT " +
                    " roba.sifra," +
                    " roba.naziv," +
                    " roba.jm," +
                    " racun_stavke.kolicina" +
                    " FROM racuni " +
                    " LEFT JOIN racun_stavke ON racun_stavke.broj_racuna=racuni.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa" +
                    " LEFT JOIN roba ON racun_stavke.sifra_robe=roba.sifra" +
                    " LEFT JOIN grupa ON grupa.id_grupa=roba.id_grupa" +
                    " WHERE racuni.broj_racuna='" + broj_racuna + "' AND grupa.id_podgrupa='2' ";

                DTrac = classSQL.select(sql, "racuni").Tables[0];
                PrintReceiptHeader(DateTime.Now, blagajnik, broj_racuna);

                for (int i = 0; i < DTrac.Rows.Count; i++)
                {
                    ima_stavke_za_kuhinju = true;
                    PrintLineItem(DTrac.Rows[i]["sifra"].ToString() + " " + DTrac.Rows[i]["naziv"].ToString(), DTrac.Rows[i]["jm"].ToString(), DTrac.Rows[i]["kolicina"].ToString());
                }

                PrintTextLine(new string('=', RecLineChars));

                _2 = tekst;
            }
            finally
            {
                if (ima_stavke_za_kuhinju == true)
                    printaj();
            }
        }

        private static void PrintLineItem(string artikl, string jmj, string kolicina)
        {
            int a = 20;
            int j = 7;
            int k = 5;

            PrintText(TruncateAt(artikl.PadRight(a), a));
            PrintText(TruncateAt(jmj.PadRight(j), j));
            PrintText(TruncateAt(kolicina.PadLeft(k), k));
            PrintText("\r\n");
        }

        private static void PrintReceiptHeader(DateTime dateTime, string cashierName, string broj)
        {
            PrintTextLine("Naruđba prema broju računa " + broj + ".");
            PrintTextLine(string.Format("Blagajnik : {0}", cashierName));
            PrintTextLine("Datum: " + DateTime.Now);

            _1 = tekst;
            tekst = "";

            int a = 20;
            int j = 7;
            int k = 5;

            PrintTextLine(string.Empty);
            PrintText(TruncateAt("STAVKA".PadRight(a), a));
            PrintText(TruncateAt("JMJ".PadRight(j), j));
            PrintText(TruncateAt("KOL".PadLeft(k), k));
            PrintText("\r\n");
            PrintTextLine(new string('=', RecLineChars));
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

            //header
            string drawString = _1;
            Font drawFont = new Font("MS Gothic", 11, FontStyle.Regular);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();
            e.Graphics.DrawString(drawString, drawFont, drawBrush, 0, 0, drawFormat);
            SizeF stringSize = new SizeF();
            stringSize = e.Graphics.MeasureString(_1, drawFont);

            height = float.Parse(stringSize.Height.ToString()) + height;

            //stavke
            drawString = _2;
            drawFont = new Font("MS Gothic", 10, FontStyle.Regular);
            float y = height;
            float x = 0.0F;
            drawFormat = new StringFormat();
            e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            stringSize = e.Graphics.MeasureString(_2, drawFont);

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

        private static void printaj()
        {
            string printerName = DTsetting.Rows[0]["windows_printer_name"].ToString();
            //const string printerName = "Send To OneNote 2007";

            string drawString = _1 + _2;

            PrintDocument printDoc = new PrintDocument();
            printDoc.PrinterSettings.PrinterName = printerName;
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
}