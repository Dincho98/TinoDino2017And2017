//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;
//using System.Drawing.Text;
//using System.Drawing.Printing;
//using PCPOS.PosPrint;
//using System.IO;

//namespace PCPOS.Caffe
//{
//    public partial class frmMaliPrinter : Form
//    {
//        public frmMaliPrinter()
//        {
//            InitializeComponent();
//        }

//        public string artikl { get; set; }
//        public string godina { get; set; }
//        public string broj_dokumenta { get; set; }
//        public string dokumenat { get; set; }
//        public string ImeForme { get; set; }
//        public string blagajnik { get; set; }
//        public string podgrupa { get; set; }
//        public string grupa { get; set; }
//        public string ducan { get; set; }
//        public bool ispis_stavka { get; set; }

//        public DateTime datumOD { get; set; }
//        public DateTime datumDO { get; set; }
//        private int RecLineChars;

//        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
//        private DataTable DTsetting = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
//        private DataTable DT_tvr = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];
//        string tekst = "";

//        private void frmMaliPrinter_Load(object sender, EventArgs e)
//        {
//            if(dokumenat=="PrometRobe")
//            {
//                RecLineChars = Convert.ToInt16(DTsetting.Rows[0]["broj_slova_na_racunu"].ToString());
//                PrometProdajneRobe();
//            }
//        }

//        DataTable DTpdv = new DataTable();
//        DataTable DTartikli = new DataTable();
//        private DataRow RowPdv;
//        private DataRow RowOsnovica;
//        private DataRow RowArtikl;

//        private void StopePDVa(decimal pdv, decimal pdv_stavka)
//        {
//            DataRow[] dataROW = DTpdv.Select("stopa = '" + Convert.ToInt16(pdv).ToString() + "'");

//            if (dataROW.Count() == 0)
//            {
//                RowPdv = DTpdv.NewRow();
//                RowPdv["stopa"] = Convert.ToInt16(pdv).ToString();
//                RowPdv["iznos"] = pdv_stavka.ToString();
//                DTpdv.Rows.Add(RowPdv);
//            }
//            else
//            {
//                dataROW[0]["iznos"] = Convert.ToDecimal(dataROW[0]["iznos"].ToString()) + pdv_stavka;
//            }

//        }

//        private void Artikli(string artikl, decimal kolicina, string sifra, string mpc)
//        {
//            DataRow[] dataROW = DTartikli.Select("sifra = '" + sifra + "' AND mpc='"+mpc+"'");

//            if (dataROW.Count() == 0)
//            {
//                RowArtikl = DTartikli.NewRow();
//                RowArtikl["sifra"] = sifra;
//                RowArtikl["mpc"] = mpc;
//                RowArtikl["kolicina"] = kolicina.ToString();
//                RowArtikl["naziv"] = artikl;
//                DTartikli.Rows.Add(RowArtikl);
//            }
//            else
//            {
//                dataROW[0]["kolicina"] = Convert.ToDecimal(dataROW[0]["kolicina"].ToString()) + kolicina;
//            }

//        }

//        private void PrometProdajneRobe()
//        {
//            string skl = "";
//            if (podgrupa != null)
//            {
//                skl = " AND roba.id_podgrupa='" + podgrupa + "'";
//            }

//            string duc = "";
//            if (ducan != null)
//            {
//                duc = " AND racuni.id_ducan='" + ducan + "'";
//            }

//            string blag = "";
//            if (blagajnik != null)
//            {
//                blag = " AND racuni.id_blagajnik='" + blagajnik + "'";
//            }

//            string art = "";
//            if (artikl != null)
//            {
//                art = " AND racun_stavke.sifra_robe='" + artikl + "'";
//            }

//            string gr = "";
//            if (grupa != null)
//            {
//                gr = " AND grupa.id_grupa='" + grupa + "'";
//            }

//            string sql = "SELECT " +
//                " racun_stavke.kolicina," +
//                " grupa.grupa," +
//                " racun_stavke.sifra_robe," +
//                " racun_stavke.mpc," +
//                " racun_stavke.porez_potrosnja," +
//                " racun_stavke.porez," +
//                " racuni.nacin_placanja," +
//                //" racuni.ukupno_kartice," +
//                //" racuni.ukupno_virman," +
//                " roba.naziv"+
//                " FROM racun_stavke" +
//                " LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna" +
//                " LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe" +
//                " LEFT JOIN grupa ON grupa.id_grupa=roba.id_grupa" +
//                " WHERE  racuni.datum_racuna>'" + datumOD.ToString("yyyy-MM-dd H:mm:ss") + "' AND racuni.datum_racuna<'" + datumDO.ToString("yyyy-MM-dd H:mm:ss") + "'" +
//                " " + skl + blag + duc + art + gr + " ORDER BY grupa";
//            DataTable DT = classSQL.select(sql, "racun_stavke").Tables[0];

//            decimal kol = 0;
//            decimal pnp = 0;
//            decimal pdv = 0;
//            decimal mpc = 0;
//            decimal pnpUKUPNO = 0;
//            decimal pdvUKUPNO = 0;
//            decimal SVE_UKUPNO = 0;
//            decimal OSNOVICA=0;
//            string g = "";

//            if (DTpdv.Columns["stopa"] == null)
//            {
//                DTpdv.Columns.Add("stopa");
//                DTpdv.Columns.Add("iznos");
//            }

//            if (DTartikli.Columns["sifra"] == null)
//            {
//                DTartikli.Columns.Add("sifra");
//                DTartikli.Columns.Add("kolicina");
//                DTartikli.Columns.Add("mpc");
//                DTartikli.Columns.Add("naziv");
//            }

//            PrintTextLine(DT_tvr.Rows[0]["skraceno_ime"].ToString());
//            PrintTextLine("Adresa: " + DT_tvr.Rows[0]["adresa"].ToString());
//            PrintTextLine("Telefon: " + DT_tvr.Rows[0]["tel"].ToString());
//            PrintTextLine("Datum: " + DateTime.Now);
//            PrintTextLine("OIB: " + DT_tvr.Rows[0]["oib"].ToString());
//            PrintTextLine("OD: " + datumOD.ToString("dd.MM.yyyy H:mm:ss"));
//            PrintTextLine("DO: " + datumDO.ToString("dd.MM.yyyy H:mm:ss"));
//            PrintTextLine(new string('-', RecLineChars));

//            if (DTpdvN.Columns["stopa"] == null)
//            {
//                DTpdvN.Columns.Add("stopa");
//                DTpdvN.Columns.Add("iznos");
//                DTpdvN.Columns.Add("nacin");
//                DTpdvN.Columns.Add("osnovica");
//            }
//            else
//            {
//                DTpdvN.Clear();
//            }

//            //if (DTosnovica.Columns["osnovica"] == null)
//            //{
//            //    DTosnovica.Columns.Add("osnovica");
//            //    DTosnovica.Columns.Add("nacin");
//            //}
//            //else
//            //{
//            //    DTosnovica.Clear();
//            //}

//            decimal UG = 0;
//            decimal UK = 0;
//            decimal UV = 0;

//            foreach (DataRow row in DT.Rows)
//            {
//                kol = Convert.ToDecimal(row["kolicina"].ToString());
//                mpc = Convert.ToDecimal(row["mpc"].ToString());
//                pnp = Convert.ToDecimal(row["porez_potrosnja"].ToString());
//                pdv = Convert.ToDecimal(row["porez"].ToString());

//                //Ovaj kod dobiva PDV
//                decimal PreracunataStopaPDV = Convert.ToDecimal((100 * pdv) / (100 + pdv + pnp));
//                decimal ppdv = (((mpc * kol) * PreracunataStopaPDV) / 100);
//                pdvUKUPNO = ppdv + pdvUKUPNO;

//                //Ovaj kod dobiva porez na potrošnju
//                decimal PreracunataStopaPorezNaPotrosnju = Convert.ToDecimal((100 * pnp) / (100 + pdv + pnp));
//                decimal ppnp = (((mpc * kol) * PreracunataStopaPorezNaPotrosnju) / 100);
//                pnpUKUPNO = ppnp + pnpUKUPNO;

//                SVE_UKUPNO = (mpc * kol)+SVE_UKUPNO;

//                //if (row["nacin_placanja"].ToString() == "G")
//                //{
//                //    StopePDVaN(pdv, ppdv, "G", ((mpc * kol) - ((ppdv) + (ppnp))));
//                //    UG = (mpc * kol) + UG;
//                //}
//                //else if (row["nacin_placanja"].ToString() == "K")
//                //{
//                //    StopePDVaN(pdv, ppdv, "K", ((mpc * kol) - ((ppdv) + (ppnp))));
//                //    UK = (mpc * kol) + UK;
//                //}
//                //else if (row["nacin_placanja"].ToString() == "T")
//                //{
//                //    StopePDVaN(pdv, ppdv, "T", ((mpc * kol) - ((ppdv) + (ppnp))));
//                //    UV = (mpc * kol) + UV;
//                //}

//                //string ajjj = row["nacin_placanja"].ToString();

//                Artikli(row["naziv"].ToString(), kol, row["naziv"].ToString(),mpc.ToString());
//                StopePDVa(pdv, ((mpc * kol) * PreracunataStopaPDV) / 100);

//                OSNOVICA = ((mpc * kol) - ((ppdv) + (ppnp))) + OSNOVICA;
//            }

//            int a = Convert.ToInt16(DTsetting.Rows[0]["ispred_artikl"].ToString())-3;
//            int k = Convert.ToInt16(DTsetting.Rows[0]["ispred_kolicine"].ToString())+3;
//            int c = Convert.ToInt16(DTsetting.Rows[0]["ispred_cijene"].ToString());
//            int s = Convert.ToInt16(DTsetting.Rows[0]["ispred_ukupno"].ToString())+3;

//            if (ispis_stavka)
//            {
//                PrintTextLine(String.Empty);
//                PrintText(TruncateAt("STAVKA".PadRight(a), a));
//                PrintText(TruncateAt("KOL".PadLeft(k), k));
//                PrintText(TruncateAt("CIJENA".PadLeft(c), c));
//                PrintText(TruncateAt("UKUPNO".PadLeft(s), s));
//                PrintText("\r\n");
//                PrintTextLine(new string('=', RecLineChars));

//                if (DTartikli.Rows.Count > 0)
//                {
//                    DataView dv = DTartikli.DefaultView;
//                    dv.Sort = "naziv";
//                    DTartikli = dv.ToTable();
//                }

//                for (int i = 0; i < DTartikli.Rows.Count; i++)
//                {
//                    PrintText(TruncateAt(DTartikli.Rows[i]["naziv"].ToString().PadRight(a), a));
//                    PrintText(TruncateAt(Convert.ToDecimal(DTartikli.Rows[i]["kolicina"].ToString()).ToString("#0.00").PadLeft(k), k));
//                    PrintText(TruncateAt(Convert.ToDecimal(DTartikli.Rows[i]["mpc"].ToString()).ToString("#0.00").PadLeft(c), c));
//                    PrintTextLine(TruncateAt((Convert.ToDecimal(DTartikli.Rows[i]["mpc"].ToString()) * Convert.ToDecimal(DTartikli.Rows[i]["kolicina"].ToString())).ToString("#0.00").PadLeft(s), s));
//                }

//                PrintTextLine("");
//                PrintTextLine(new string('-', RecLineChars));
//            }

//            //PrintTextLine("OSNOVICA: " + OSNOVICA.ToString("#0.000"));
//            PrintTextLine("PNP ukupno:       "+pnpUKUPNO.ToString("#0.000"));

//            //for(int i=0; i<DTpdv.Rows.Count; i++)
//            //{
//            //    PrintTextLine("PDV " + DTpdv.Rows[i]["stopa"].ToString() + "%: " + Convert.ToDecimal(DTpdv.Rows[i]["iznos"].ToString()).ToString("#0.00"));
//            //}

//            //GOTOVINA
//            if (UG > 0)
//            {
//                PrintTextLine(new string('-', RecLineChars));
//                PrintTextLine("UKUPNO GOTOVINA:  " + UG.ToString("#0.000"));
//                //for (int i = 0; i < DTosnovica.Rows.Count; i++)
//                //{
//                //    if (DTosnovica.Rows[i]["nacin"].ToString() == "G")
//                //        PrintTextLine("OSNOVICA: " + Convert.ToDecimal(DTosnovica.Rows[i]["osnovica"].ToString()).ToString("#0.00"));
//                //}
//                for (int i = 0; i < DTpdvN.Rows.Count; i++)
//                {
//                    if (DTpdvN.Rows[i]["nacin"].ToString() == "G")
//                    {
//                        PrintTextLine("OSNOVICA PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%: " + Convert.ToDecimal(DTpdvN.Rows[i]["osnovica"].ToString()).ToString("#0.000"));
//                        PrintTextLine("PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%:          " + Convert.ToDecimal(DTpdvN.Rows[i]["iznos"].ToString()).ToString("#0.000"));
//                    }
//                }
//            }

//            //KARTICA
//            if (UK > 0)
//            {
//                PrintTextLine(new string('-', RecLineChars));
//                PrintTextLine("UKUPNO KARTICE:   " + UK.ToString("#0.000"));
//                //for (int i = 0; i < DTosnovica.Rows.Count; i++)
//                //{
//                //    if (DTosnovica.Rows[i]["nacin"].ToString() == "K")
//                //        PrintTextLine("OSNOVICA: " + Convert.ToDecimal(DTosnovica.Rows[i]["osnovica"].ToString()).ToString("#0.00"));
//                //}
//                for (int i = 0; i < DTpdvN.Rows.Count; i++)
//                {
//                    if (DTpdvN.Rows[i]["nacin"].ToString() == "K")
//                    {
//                        PrintTextLine("OSNOVICA PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%: " + Convert.ToDecimal(DTpdvN.Rows[i]["osnovica"].ToString()).ToString("#0.000"));
//                        PrintTextLine("PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%:          " + Convert.ToDecimal(DTpdvN.Rows[i]["iznos"].ToString()).ToString("#0.000"));
//                    }
//                }
//            }

//            //VIRMAN
//            if (UV > 0)
//            {
//                PrintTextLine(new string('-', RecLineChars));
//                PrintTextLine("UKUPNO VIRMAN:    " + UV.ToString("#0.000"));
//                //for (int i = 0; i < DTosnovica.Rows.Count; i++)
//                //{
//                //    if (DTosnovica.Rows[i]["nacin"].ToString() == "T")
//                //        PrintTextLine("OSNOVICA: " + Convert.ToDecimal(DTosnovica.Rows[i]["osnovica"].ToString()).ToString("#0.00"));
//                //}
//                for (int i = 0; i < DTpdvN.Rows.Count; i++)
//                {
//                    if (DTpdvN.Rows[i]["nacin"].ToString() == "T")
//                    {
//                        PrintTextLine("OSNOVICA PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%: " + Convert.ToDecimal(DTpdvN.Rows[i]["osnovica"].ToString()).ToString("#0.000"));
//                        PrintTextLine("PDV " + DTpdvN.Rows[i]["stopa"].ToString() + "%:          " + Convert.ToDecimal(DTpdvN.Rows[i]["iznos"].ToString()).ToString("#0.000"));
//                    }
//                }
//            }

//            PrintTextLine(new string('-', RecLineChars));

//            PrintTextLine("OSNOVICA UKUPNO:  " + OSNOVICA.ToString("#0.00"));
//            for(int i=0; i<DTpdv.Rows.Count; i++)
//            {
//            PrintTextLine("PDV " + DTpdv.Rows[i]["stopa"].ToString() + "% UKUPNO:   " + Convert.ToDecimal(DTpdv.Rows[i]["iznos"].ToString()).ToString("#0.00"));
//            }
//            PrintTextLine("SVE UKUPNO:       " + SVE_UKUPNO.ToString("#0.00"));

//            System.Drawing.Text.PrivateFontCollection privateFonts = new PrivateFontCollection();
//            privateFonts.AddFontFile("Slike/msgothic.ttc");
//            System.Drawing.Font font = new Font(privateFonts.Families[0], 11);

//            //rtb.Font = font;
//            //rtb.Text = tekst;

//        }

//        DataTable DTpdvN = new DataTable();
//        private void StopePDVaN(decimal pdv, decimal pdv_stavka, string nacin_P, decimal osnovica)
//        {
//            if(osnovica<0 && Convert.ToInt16(pdv)==0)
//            {
//            }

//            DataRow[] dataROW = DTpdvN.Select("stopa = '" + Convert.ToInt16(pdv).ToString() + "' AND nacin='"+nacin_P+"'");

//            if (dataROW.Count() == 0)
//            {
//                RowPdv = DTpdvN.NewRow();
//                RowPdv["stopa"] = Convert.ToInt16(pdv).ToString();
//                RowPdv["iznos"] = pdv_stavka.ToString();
//                RowPdv["nacin"] = nacin_P;
//                RowPdv["osnovica"] = osnovica;
//                DTpdvN.Rows.Add(RowPdv);
//            }
//            else
//            {
//                dataROW[0]["iznos"] = Convert.ToDecimal(dataROW[0]["iznos"].ToString()) + pdv_stavka;
//                dataROW[0]["osnovica"] = Convert.ToDecimal(dataROW[0]["osnovica"].ToString()) + osnovica;
//            }

//        }

//        private void btnIspis_Click(object sender, EventArgs e)
//        {
//            printaj();
//        }

//        private void PrintPage(object o, PrintPageEventArgs e)
//        {
//            System.Drawing.Text.PrivateFontCollection privateFonts = new PrivateFontCollection();
//            privateFonts.AddFontFile("Slike/msgothic.ttc");
//            System.Drawing.Font font = new Font(privateFonts.Families[0], 10);

//            //header
//            String drawString = tekst;
//            Font drawFont = font;
//            SolidBrush drawBrush = new SolidBrush(Color.Black);
//            StringFormat drawFormat = new StringFormat();
//            e.Graphics.DrawString(drawString, drawFont, drawBrush, 0, 0, drawFormat);
//            SizeF stringSize = new SizeF();
//            stringSize = e.Graphics.MeasureString(tekst, drawFont);

//            drawFont = font;
//            float y = 0.0F;
//            float x = 0.0F;

//            e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
//        }

//        private void printaj()
//        {
//            string printerName = DTsetting.Rows[0]["windows_printer_name"].ToString();
//            PrintDocument printDoc = new PrintDocument();

//            printDoc.PrinterSettings.PrinterName = printerName;

//            //byte[] codeOpenCashDrawer = new byte[] { 27, 112, 48, 55, 121 };
//            //IntPtr pUnmanagedBytes = new IntPtr(0);
//            //pUnmanagedBytes = Marshal.AllocCoTaskMem(5);
//            //Marshal.Copy(codeOpenCashDrawer, 0, pUnmanagedBytes, 5);
//            //RawPrinterHelper.SendBytesToPrinter(printDoc.PrinterSettings.PrinterName, pUnmanagedBytes, 5);
//            //Marshal.FreeCoTaskMem(pUnmanagedBytes);

//            for (int i = 0; i < Convert.ToInt16(DTsetting.Rows[0]["linija_praznih_bottom"].ToString()); i++)
//            {
//                tekst += Environment.NewLine;
//            }

//            if (DTpostavke.Rows[0]["direct_print"].ToString() == "1")
//            {
//                string ttx = "\r\n"+tekst;
//                ttx = ttx.Replace("è", "c");
//                ttx = ttx.Replace("È", "C");
//                ttx = ttx.Replace("ž", "z");
//                ttx = ttx.Replace("Ž", "Z");
//                ttx = ttx.Replace("æ", "c");
//                ttx = ttx.Replace("Æ", "C");
//                ttx = ttx.Replace("ð", "d");
//                ttx = ttx.Replace("Ð", "D");
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

//            //string GS = Convert.ToString((char)29);
//            //string ESC = Convert.ToString((char)27);

//            //string COMMAND = "";
//            //COMMAND = ESC + "@";
//            //COMMAND += GS + "V" + (char)1;

//            //RawPrinterHelper.SendStringToPrinter(printDoc.PrinterSettings.PrinterName, COMMAND);
//        }

//        private void PrintText(string text)
//        {
//            if (text.Length <= RecLineChars)
//                tekst += text;
//            else if (text.Length > RecLineChars)
//                tekst += TruncateAt(text, RecLineChars);
//        }

//        private void PrintTextLine(string text)
//        {
//            if (text.Length < RecLineChars)
//                tekst += text + Environment.NewLine;
//            else if (text.Length > RecLineChars)
//                tekst += TruncateAt(text, RecLineChars);
//            else if (text.Length == RecLineChars)
//                tekst += text + Environment.NewLine;

//        }

//        private string TruncateAt(string text, int maxWidth)
//        {
//            string retVal = text;
//            if (text.Length > maxWidth)
//                retVal = text.Substring(0, maxWidth);

//            return retVal;
//        }

//        private void button1_Click(object sender, EventArgs e)
//        {
//            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

//            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
//            saveFileDialog1.FilterIndex = 2;
//            saveFileDialog1.FileName = "Izvješæe.txt";
//            //saveFileDialog1.RestoreDirectory = true;

//            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
//            {
//                File.WriteAllText(saveFileDialog1.OpenFile().ToString(),rtb.Text);
//            }
//        }

//    }
//}