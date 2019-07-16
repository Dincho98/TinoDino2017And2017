using PCPOS.PosPrint;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Report.PrometiPoDanima
{
    public partial class frmPosPreview : Form
    {
        public frmPosPreview()
        {
            InitializeComponent();
        }

        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
        private DataTable DTsetting = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
        private DataTable DT_tvr = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];

        public string artikl { get; set; }
        public string godina { get; set; }
        public string broj_dokumenta { get; set; }
        public string dokumenat { get; set; }
        public string ImeForme { get; set; }
        public string blagajnik { get; set; }
        public string skladiste { get; set; }
        public string id_podgrupa { get; set; }

        public string grupa { get; set; }
        public string ducan { get; set; }
        public string kasa { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.FileName = "Izvješće.txt";
            //saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.OpenFile().ToString(), rtb.Text);
            }
        }

        public string tekst { get; set; }

        private void frmPosPreview_Load(object sender, EventArgs e)
        {
            System.Drawing.Text.PrivateFontCollection privateFonts = new PrivateFontCollection();
            privateFonts.AddFontFile("Slike/msgothic.ttc");
            System.Drawing.Font font = new Font(privateFonts.Families[0], 11);

            rtb.Font = font;

            PrometProdajneRobe();
            rtb.Text = ttekst;
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            print(ttekst);
            this.Close();
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

        #region create

        private void promjenaCijene()
        {
            string sql1 = "SELECT " +
                " podaci_tvrtka.ime_tvrtke," +
                " podaci_tvrtka.skraceno_ime," +
                " podaci_tvrtka.oib," +
                " podaci_tvrtka.tel," +
                " podaci_tvrtka.fax," +
                " podaci_tvrtka.mob," +
                " podaci_tvrtka.iban," +
                " podaci_tvrtka.adresa," +
                " podaci_tvrtka.vl," +
                " podaci_tvrtka.poslovnica_adresa," +
                " podaci_tvrtka.poslovnica_grad," +
                " podaci_tvrtka.email," +
                " podaci_tvrtka.naziv_fakture," +
                " podaci_tvrtka.text_bottom," +
                " grad.grad + '' + grad.posta AS grad" +
                " FROM podaci_tvrtka" +
                " LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad" +
                "";
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
        }

        private DataTable DTpdv = new DataTable();
        private DataTable DTartikli = new DataTable();
        private DataRow RowPdv;
        private DataRow RowOsnovica;
        private DataRow RowArtikl;

        private void StopePDVa(decimal pdv, decimal pdv_stavka)
        {
            DataRow[] dataROW = DTpdv.Select("stopa = '" + Convert.ToInt16(pdv).ToString() + "'");

            if (dataROW.Count() == 0)
            {
                RowPdv = DTpdv.NewRow();
                RowPdv["stopa"] = Convert.ToInt16(pdv).ToString();
                RowPdv["iznos"] = pdv_stavka.ToString();
                DTpdv.Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["iznos"] = Convert.ToDecimal(dataROW[0]["iznos"].ToString()) + pdv_stavka;
            }
        }

        //DataSet dSRliste = new DataSet();
        private string ttekst = "";

        private void Artikli(string datum, decimal osnovica, decimal pdv, decimal pnp, decimal mpc, decimal gotovina, decimal kartice, decimal transakcijski, decimal ostalo, decimal rabat)
        {
            if (kartice > 0)
            {
            }

            DataRow[] dataROW = dSRliste.Tables[0].Select("sifra = '" + datum + "'");

            if (dataROW.Count() == 0)
            {
                RowArtikl = dSRliste.Tables[0].NewRow();
                RowArtikl["sifra"] = datum;
                RowArtikl["cijena1"] = Math.Round(osnovica, 3).ToString("#0.000");
                RowArtikl["cijena2"] = Math.Round(pdv, 3).ToString("#0.000");
                RowArtikl["cijena3"] = Math.Round(pnp, 3).ToString("#0.000"); ;
                RowArtikl["cijena5"] = Math.Round(mpc, 3).ToString("#0.000"); ;
                RowArtikl["cijena6"] = Math.Round(gotovina, 3).ToString("#0.000"); ;
                RowArtikl["cijena7"] = Math.Round(kartice, 3).ToString("#0.000"); ;
                RowArtikl["cijena8"] = Math.Round(transakcijski, 3).ToString("#0.000"); ;
                RowArtikl["cijena9"] = Math.Round(ostalo, 3).ToString("#0.000"); ;
                RowArtikl["rabat1"] = Math.Round(rabat, 3).ToString("#0.000");
                dSRliste.Tables[0].Rows.Add(RowArtikl);
            }
            else
            {
                dataROW[0]["cijena1"] = Math.Round((Convert.ToDecimal(dataROW[0]["cijena1"].ToString()) + osnovica), 3).ToString("#0.000"); ;
                dataROW[0]["cijena2"] = Math.Round((Convert.ToDecimal(dataROW[0]["cijena2"].ToString()) + pdv), 3).ToString("#0.000"); ;
                dataROW[0]["cijena3"] = Math.Round((Convert.ToDecimal(dataROW[0]["cijena3"].ToString()) + pnp), 3).ToString("#0.000"); ;
                dataROW[0]["cijena5"] = Math.Round((Convert.ToDecimal(dataROW[0]["cijena5"].ToString()) + mpc), 3).ToString("#0.000"); ;
                dataROW[0]["cijena6"] = Math.Round((Convert.ToDecimal(dataROW[0]["cijena6"].ToString()) + gotovina), 3).ToString("#0.000"); ;
                dataROW[0]["cijena7"] = Math.Round((Convert.ToDecimal(dataROW[0]["cijena7"].ToString()) + kartice), 3).ToString("#0.000"); ;
                dataROW[0]["cijena8"] = Math.Round((Convert.ToDecimal(dataROW[0]["cijena8"].ToString()) + transakcijski), 3).ToString("#0.000"); ;
                dataROW[0]["cijena9"] = Math.Round((Convert.ToDecimal(dataROW[0]["cijena9"].ToString()) + ostalo), 3).ToString("#0.000"); ;
                dataROW[0]["rabat1"] = Math.Round((Convert.ToDecimal(dataROW[0]["rabat1"].ToString()) + rabat), 3).ToString("#0.000"); ;
            }
        }

        private void PrometProdajneRobe()
        {
            promjenaCijene();
            string duc = "";
            if (ducan != null)
            {
                duc = " AND racuni.id_ducan='" + ducan + "'";
            }

            string kas = "";
            if (kasa != null)
            {
                kas = " AND racuni.id_kasa='" + kasa + "'";
            }

            string blag = "";
            if (blagajnik != null)
            {
                blag = " AND racuni.id_blagajnik='" + blagajnik + "'";
            }

            string art = "";
            if (artikl != null)
            {
                art = " AND racun_stavke.sifra_robe='" + artikl + "'";
            }

            string gr = "";
            if (grupa != null)
            {
                gr = " AND grupa.id_grupa='" + grupa + "'";
            }

            string sql = "SELECT " +
                " racun_stavke.kolicina," +
                " grupa.grupa," +
                " racun_stavke.sifra_robe," +
                " racun_stavke.mpc," +
                " racun_stavke.porez_potrosnja," +
                " racun_stavke.porez," +
                " racun_stavke.rabat," +
                " racuni.nacin_placanja," +
                " racuni.datum_racuna," +
                " roba.naziv" +
                " FROM racun_stavke" +
                " LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_kasa=racun_stavke.id_kasa AND racuni.id_ducan=racun_stavke.id_ducan" +
                " LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe" +
                " LEFT JOIN grupa ON grupa.id_grupa=roba.id_grupa" +
                " WHERE  racuni.datum_racuna>'" + datumOD.ToString("yyyy-MM-dd H:mm:ss") + "' AND racuni.datum_racuna<'" + datumDO.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " " + blag + duc + kas + art + gr + "  ORDER BY racuni.datum_racuna ASC";
            DataTable DT = classSQL.select(sql, "racun_stavke").Tables[0];

            decimal kol = 0;
            decimal pnp = 0;
            decimal pdv = 0;
            decimal mpc = 0;
            decimal rabat = 0;
            decimal pnpUKUPNO = 0;
            decimal pdvUKUPNO = 0;
            decimal SVE_UKUPNO = 0;
            decimal OSNOVICA = 0;
            string g = "";

            if (DTpdv.Columns["stopa"] == null)
            {
                DTpdv.Columns.Add("stopa");
                DTpdv.Columns.Add("iznos");
            }

            if (DTartikli.Columns["sifra"] == null)
            {
                DTartikli.Columns.Add("sifra");
                DTartikli.Columns.Add("kolicina");
                DTartikli.Columns.Add("mpc");
                DTartikli.Columns.Add("naziv");
            }

            if (DTpdvN.Columns["stopa"] == null)
            {
                DTpdvN.Columns.Add("stopa");
                DTpdvN.Columns.Add("iznos");
                DTpdvN.Columns.Add("nacin");
                DTpdvN.Columns.Add("osnovica");
            }
            else
            {
                DTpdvN.Clear();
            }

            int oo = DT.Rows.Count;

            foreach (DataRow row in DT.Rows)
            {
                kol = Convert.ToDecimal(row["kolicina"].ToString());
                mpc = Convert.ToDecimal(row["mpc"].ToString());
                pnp = Convert.ToDecimal(row["porez_potrosnja"].ToString());
                pdv = Convert.ToDecimal(row["porez"].ToString());
                rabat = Convert.ToDecimal(row["rabat"].ToString()) / 100;
                if (row["sifra_robe"].ToString() == "!popustABS") { rabat = -1 * mpc; }
                else { rabat = mpc * rabat; mpc -= rabat; rabat *= kol; }

                //Ovaj kod dobiva PDV
                decimal PreracunataStopaPDV = Convert.ToDecimal((100 * pdv) / (100 + pdv + pnp));
                decimal ppdv = (((mpc * kol) * PreracunataStopaPDV) / 100);
                pdvUKUPNO = ppdv + pdvUKUPNO;

                //Ovaj kod dobiva porez na potrošnju
                decimal PreracunataStopaPorezNaPotrosnju = Convert.ToDecimal((100 * pnp) / (100 + pdv + pnp));
                decimal ppnp = (((mpc * kol) * PreracunataStopaPorezNaPotrosnju) / 100);
                pnpUKUPNO = ppnp + pnpUKUPNO;

                SVE_UKUPNO = (mpc * kol) + SVE_UKUPNO;

                decimal UG = 0;
                decimal UK = 0;
                decimal UV = 0;
                decimal UO = 0;

                if (row["nacin_placanja"].ToString() == "G")
                {
                    StopePDVaN(pdv, ppdv, "G", ((mpc * kol) - ((ppdv) + (ppnp))));
                    UG = (mpc * kol);
                }
                else if (row["nacin_placanja"].ToString() == "K")
                {
                    StopePDVaN(pdv, ppdv, "K", ((mpc * kol) - ((ppdv) + (ppnp))));
                    UK = (mpc * kol);
                }
                else if (row["nacin_placanja"].ToString() == "T")
                {
                    StopePDVaN(pdv, ppdv, "T", ((mpc * kol) - ((ppdv) + (ppnp))));
                    UV = (mpc * kol);
                }
                else if (row["nacin_placanja"].ToString() == "O")
                {
                    StopePDVaN(pdv, ppdv, "O", ((mpc * kol) - ((ppdv) + (ppnp))));
                    UO = (mpc * kol);
                }

                string ajjj = row["nacin_placanja"].ToString();

                DateTime d = Convert.ToDateTime(row["datum_racuna"].ToString());
                decimal o = ((mpc * kol) - ((ppdv) + (ppnp)));
                decimal p = ((mpc * kol) * PreracunataStopaPDV) / 100;

                Artikli(d.ToString("dd.MM.yyyy"), o, p, ppnp, mpc * kol, UG, UK, UV, UO, rabat);

                StopePDVa(pdv, ((mpc * kol) * PreracunataStopaPDV) / 100);

                OSNOVICA = ((mpc * kol) - ((ppdv) + (ppnp))) + OSNOVICA;
            }

            string porezi = "";
            for (int i = 0; i < DTpdvN.Rows.Count; i++)
            {
                if (Convert.ToDecimal(DTpdvN.Rows[i]["stopa"].ToString()) >= 0)
                {
                    string nacin_pplacanja = "";

                    if (DTpdvN.Rows[i]["nacin"].ToString().ToUpper() == "G")
                    {
                        nacin_pplacanja = "GOTOVINA: ";
                    }
                    else if (DTpdvN.Rows[i]["nacin"].ToString().ToUpper() == "T")
                    {
                        nacin_pplacanja = "TRANSAKCIJSKI RAČUN: ";
                    }
                    else if (DTpdvN.Rows[i]["nacin"].ToString().ToUpper() == "O")
                    {
                        nacin_pplacanja = "OSTALO: ";
                    }
                    else if (DTpdvN.Rows[i]["nacin"].ToString().ToUpper() == "K")
                    {
                        nacin_pplacanja = "KARTICE: ";
                    }

                    porezi += "Način plačanja: " + nacin_pplacanja +
                        "\r\nOsnovica " + DTpdvN.Rows[i]["stopa"].ToString() + " %: " + Math.Round(Convert.ToDecimal(DTpdvN.Rows[i]["osnovica"].ToString()), 3).ToString("#0.00") + "" +
                        "\r\nIznos " + Math.Round(Convert.ToDecimal(DTpdvN.Rows[i]["iznos"].ToString()), 3).ToString("#0.00") +
                        "\r\n\r\n";
                }
            }

            foreach (DataRow row in dSRliste.Tables[0].Rows)
            {
                ttekst += "\r\n" + "Datum: " + row["sifra"].ToString() + "\r\n" +
                    "Osnovica: " + Math.Round(Convert.ToDecimal(row["cijena1"].ToString()), 2) + vrati_razmake(row["cijena1"].ToString(), 10) +
                    "PDV:           " + Math.Round(Convert.ToDecimal(row["cijena2"].ToString()), 2) + "\r\n" +
                    "Mpc:      " + Math.Round(Convert.ToDecimal(row["cijena5"].ToString()), 2) + vrati_razmake(row["cijena5"].ToString(), 10) +
                    "Gotovina:      " + Math.Round(Convert.ToDecimal(row["cijena6"].ToString()), 2) + "\r\n" +
                    "Kartice:  " + Math.Round(Convert.ToDecimal(row["cijena7"].ToString()), 2) + vrati_razmake(row["cijena7"].ToString(), 10) +
                    "Transakcijski: " + Math.Round(Convert.ToDecimal(row["cijena8"].ToString()), 2) + "\r\n" +
                    "Ostalo:   " + Math.Round(Convert.ToDecimal(row["cijena9"].ToString()), 2) + vrati_razmake(row["cijena9"].ToString(), 10) +
                    "Rabat:         " + Math.Round(Convert.ToDecimal(row["rabat1"].ToString()), 2) + "\r\n";
            }
            ttekst += "\r\n\r\n" + porezi;
            //rtb.Text = ttekst;
            //print(ttekst);
        }

        private string vrati_razmake(string broj_slova, decimal broj_mjesta)
        {
            decimal vrati = 0;
            string vrati1 = "";
            vrati = broj_mjesta - broj_slova.Length;
            if (vrati == 0)
            {
                vrati1 = "";
            }
            else
            {
                for (int i = 0; i < vrati; i++)
                    vrati1 += " ";
            }
            return vrati1;
        }

        private void print(string ttx)
        {
            string printerName = DTsetting.Rows[0]["windows_printer_name"].ToString();
            PrintDocument printDoc = new PrintDocument();

            printDoc.PrinterSettings.PrinterName = printerName;

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

            for (int i = 0; i < Convert.ToInt16(DTsetting.Rows[0]["linija_praznih_bottom"].ToString()); i++)
            {
                ttx += Environment.NewLine;
            }
            RawPrinterHelper.SendStringToPrinter(printDoc.PrinterSettings.PrinterName, ttx + COMMAND);
        }

        private DataTable DTpdvN = new DataTable();

        private void StopePDVaN(decimal pdv, decimal pdv_stavka, string nacin_P, decimal osnovica)
        {
            if (osnovica < 0 && Convert.ToInt16(pdv) == 0)
            {
            }

            DataRow[] dataROW = DTpdvN.Select("stopa = '" + Convert.ToInt16(pdv).ToString() + "' AND nacin='" + nacin_P + "'");

            if (dataROW.Count() == 0)
            {
                RowPdv = DTpdvN.NewRow();
                RowPdv["stopa"] = Convert.ToInt16(pdv).ToString();
                RowPdv["iznos"] = pdv_stavka.ToString();
                RowPdv["nacin"] = nacin_P;
                RowPdv["osnovica"] = osnovica;
                DTpdvN.Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["iznos"] = Convert.ToDecimal(dataROW[0]["iznos"].ToString()) + pdv_stavka;
                dataROW[0]["osnovica"] = Convert.ToDecimal(dataROW[0]["osnovica"].ToString()) + osnovica;
            }
        }

        #endregion create
    }
}