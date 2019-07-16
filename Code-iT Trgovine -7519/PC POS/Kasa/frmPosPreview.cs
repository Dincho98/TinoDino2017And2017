using PCPOS.PosPrint;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Kasa
{
    public partial class frmPosPreview : Form
    {
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
        public string dobavljac { get; set; }
        public string tekst { get; set; }

        public bool stavke_ispis { get; set; }
        public bool pregledRacuna = false;

        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }
        public string _1 { get; set; }
        public string _2 { get; set; }
        public string _3 { get; set; }
        public string _4 { get; set; }
        public string _5 { get; set; }

        private DataTable DTpdvN = new DataTable();
        private DataTable DTpdv = new DataTable();
        private DataTable DTartikli = new DataTable();

        private DataRow RowPdv;

        private decimal UG_sve = 0;
        private decimal UK_sve = 0;
        private decimal UV_sve = 0;
        private decimal UO_sve = 0;

        private int RecLineChars;
        private string ttekst;

        private void frmPosPreview_Load(object sender, EventArgs e)
        {
            PrivateFontCollection privateFonts = new PrivateFontCollection();
            privateFonts.AddFontFile("Slike/msgothic.ttc");
            Font font = new Font(privateFonts.Families[0], 11);
            //Dodao 24.10.2013
            if (pregledRacuna) ttekst = tekst;
            else preview();

            rtb.Font = font;
            rtb.Text = ttekst;
        }

        public frmPosPreview()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.rtf)|*.rtf|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.FileName = "Izvješće.rtf";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                rtb.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
            }
        }

        private void podacitvrtka()
        {
            string sql1 = @"SELECT podaci_tvrtka.ime_tvrtke, podaci_tvrtka.skraceno_ime, podaci_tvrtka.oib,
podaci_tvrtka.tel, podaci_tvrtka.fax, podaci_tvrtka.mob, podaci_tvrtka.iban, podaci_tvrtka.adresa, podaci_tvrtka.vl,
podaci_tvrtka.poslovnica_adresa, podaci_tvrtka.poslovnica_grad, podaci_tvrtka.email, podaci_tvrtka.naziv_fakture,
podaci_tvrtka.text_bottom, grad.grad + '' + grad.posta AS grad
FROM podaci_tvrtka
LEFT JOIN grad ON grad.id_grad = podaci_tvrtka.id_grad;";
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
        }

        private void preview()
        {
            string id_blag = Properties.Settings.Default.id_zaposlenik;
            DataTable DTblagajnik = classSQL.select(string.Format("Select ime, prezime from Zaposlenici where id_zaposlenik = '{0}';", id_blag), "blagajnik").Tables[0];
            string blagajnik_ = DTblagajnik.Rows[0]["ime"].ToString() + " " + DTblagajnik.Rows[0]["prezime"].ToString();
            DateTime[] datumi = new DateTime[2];
            datumi[0] = datumOD;
            datumi[1] = datumDO;

            string ducR = "";//, ducA = "";
            if (ducan != null)
            {
                ducR = string.Format(" AND racuni.id_ducan = '{0}'", ducan);
                //ducA = string.Format(" AND a.id_ducan = '{0}'", ducan);
            }

            string napR = "";
            if (kasa != null)
            {
                napR = string.Format(" AND racuni.id_kasa = '{0}'", kasa);
            }

            string blagR = "", blagA = "";
            if (blagajnik != null)
            {
                blagR = string.Format(" AND racuni.id_blagajnik = '{0}'", blagajnik);
                blagA = string.Format(" AND a.id_zaposlenik = '{0}'", blagajnik);
            }

            string grR = "";
            if (grupa != null)
            {
                grR = string.Format(" AND roba.id_grupa = '{0}'", grupa);
            }

            string dobR = "", dobA = "";
            if (dobavljac != null)
            {
                dobR = string.Format(" AND roba.id_partner = '{0}'", dobavljac);
                dobA = string.Format(" AND a.id_partner = '{0}'", dobavljac);
            }

            string odDO = null;
            string sOdDo = string.Format(@"select min(racuni.broj_racuna::integer)::text || ' - ' || max(racuni.broj_racuna::integer)::text as oddo
FROM racun_stavke
LEFT JOIN racuni ON racun_stavke.broj_racuna=racuni.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa
LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe where racuni.datum_racuna >= '{0}' AND racuni.datum_racuna <= '{1}' {2}{3}{4}{5}{6};", datumOD, datumDO, blagR, ducR, napR, dobR, grR);

            DataSet dsOdDo = classSQL.select(sOdDo, "racuni");
            if (dsOdDo != null && dsOdDo.Tables.Count > 0 && dsOdDo.Tables[0] != null && dsOdDo.Tables[0].Rows.Count > 0 && dsOdDo.Tables[0].Rows[0][0].ToString().Length > 0)
            {
                odDO = string.Format("OD-DO računa: {0}", dsOdDo.Tables[0].Rows[0][0].ToString());
            }

            PrintReceiptHeader(Class.PodaciTvrtka.textRacuna1, Class.PodaciTvrtka.adresaTvrtke, Class.PodaciTvrtka.telefonTvrtke, datumi, blagajnik_, odDO);

            decimal kolicina = 0;
            decimal povratnaNaknada = 0;
            decimal porezNaPotrosnju = 0;
            decimal pdv = 0;
            decimal mpc = 0;
            decimal rabat = 0;
            decimal ukupnoPorezNaPotrosnju = 0;
            decimal ukupnoPdv = 0;
            decimal ukupnoSve = 0;
            decimal osnovica = 0;
            decimal ukupnoPovratnaNaknada = 0;

            if (DTpdv.Columns["stopa"] == null)
            {
                DataColumn dataColumn = new DataColumn();
                dataColumn.DataType = Type.GetType("System.Int32");
                dataColumn.ColumnName = "stopa";
                DTpdv.Columns.Add(dataColumn);
                DTpdv.Columns.Add("iznos");
                DTpdv.Columns.Add("osnovica");
                DTpdv.Columns.Add("iznos_ukupno");
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
                DataColumn dataColumn = new DataColumn();
                dataColumn.DataType = Type.GetType("System.Int32");
                dataColumn.ColumnName = "stopa";
                DTpdvN.Columns.Add(dataColumn);

                DTpdvN.Columns.Add("iznos");
                DTpdvN.Columns.Add("nacin");
                DTpdvN.Columns.Add("osnovica");
                DTpdvN.Columns.Add("pov_nak");

                dataColumn = new DataColumn();
                dataColumn.DataType = Type.GetType("System.Int32");
                dataColumn.ColumnName = "srt";
                DTpdvN.Columns.Add(dataColumn);
            }
            else
            {
                DTpdvN.Clear();
            }
            string avansi = "";
            if (Class.Postavke.uzmi_avanse_u_promet_kase_POS)
            {
                avansi = string.Format(@"union all
select '-1' as sifra_robe, a.ukupno, case when length(a.artikl) > 0 then a.artikl else a.opis end as naziv,
a.osnovica_var as vpc, a.ukupno::money as mpc, replace(p.iznos::character varying(5), '.',',') as porez,
1 as kolicna, '0' as rabat, case when a.id_nacin_placanja = 1 then 'G' else case when a.id_nacin_placanja = 2 then 'K' else case when a.id_nacin_placanja = 3 then 'T' else 'O' end end end as nacin_placanja,
0 as povratna_naknada, ukupno::money as cijena
from avansi as a
left join porezi as p on a.id_pdv = p.id_porez
where a.id_nacin_placanja in (1,2) and  a.dat_knj >= '{0}' AND a.dat_knj <= '{1}'
{2}{3}",
                datumOD.ToString("yyyy-MM-dd HH:mm:ss"),
                datumDO.ToString("yyyy-MM-dd HH:mm:ss"),
                blagA,
                dobA);
            }

            string sql_stv = string.Format(@"SELECT racun_stavke.sifra_robe, racun_stavke.ukupno_mpc_rabat as ukupno, roba.naziv,
Round(racun_stavke.vpc, 3) as vpc, racun_stavke.mpc, racun_stavke.porez,
SUM(CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC)) AS [kolicina], racun_stavke.rabat, racuni.nacin_placanja,
sum(racun_stavke.povratna_naknada) as povratna_naknada, roba.mpc AS cijena
FROM racun_stavke
LEFT JOIN racuni ON racun_stavke.broj_racuna=racuni.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa
LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe
WHERE racuni.datum_racuna >= '{0}' AND racuni.datum_racuna <= '{1}'
{2} {3} {4} {5} {6}
GROUP BY racun_stavke.sifra_robe, roba.naziv, Round(racun_stavke.vpc, 3), racun_stavke.mpc, racun_stavke.porez, racun_stavke.rabat, roba.mpc, racun_stavke.ukupno_mpc_rabat, racuni.nacin_placanja
{7};",
                datumOD.ToString("yyyy-MM-dd HH:mm:ss"),
                datumDO.ToString("yyyy-MM-dd HH:mm:ss"),
                blagR,
                ducR,
                napR,
                dobR,
                grR,
                avansi);

            DataTable DT1 = classSQL.select(sql_stv, "racun_stavke").Tables[0];

            //foreach (DataRow row in DT1.Rows)
            //{
            //    povratnaNaknada = Convert.ToDecimal(row["povratna_naknada"].ToString());
            //    kolicina = Convert.ToDecimal(row["kolicina"].ToString());
            //    mpc = Convert.ToDecimal(row["mpc"].ToString()) - (povratnaNaknada == 0 ? povratnaNaknada : (kolicina == 0 ? 0 : povratnaNaknada / kolicina));
            //    pdv = Convert.ToDecimal(row["porez"].ToString());
            //    rabat = Convert.ToDecimal(row["rabat"].ToString());
            //    decimal mpc_s_rab = mpc - (mpc * (rabat / 100));

            //    //Ovaj kod dobiva PDV
            //    decimal PreracunataStopaPDV = Convert.ToDecimal((100 * pdv) / (100 + pdv + porezNaPotrosnju));
            //    decimal ppdv = (((mpc_s_rab * kolicina) * PreracunataStopaPDV) / 100);
            //    ukupnoPdv = ppdv + ukupnoPdv;
            //    ukupnoPovratnaNaknada += povratnaNaknada;

            //    //Ovaj kod dobiva porez na potrošnju
            //    decimal PreracunataStopaPorezNaPotrosnju = Convert.ToDecimal((100 * porezNaPotrosnju) / (100 + pdv + porezNaPotrosnju));
            //    decimal ppnp = (((mpc_s_rab * kolicina) * PreracunataStopaPorezNaPotrosnju) / 100);
            //    ukupnoPorezNaPotrosnju = ppnp + ukupnoPorezNaPotrosnju;

            //    ukupnoSve = (mpc_s_rab * kolicina) + ukupnoSve;

            //    decimal UG = 0;
            //    decimal UK = 0;
            //    decimal UV = 0;
            //    decimal UO = 0;

            //    int sort = 0;
            //    if (row["nacin_placanja"].ToString() == "G")
            //    {
            //        sort = 1;
            //        UG = (mpc_s_rab * kolicina);
            //        UG_sve += UG;
            //    }
            //    else if (row["nacin_placanja"].ToString() == "K")
            //    {
            //        sort = 2;
            //        UK = (mpc_s_rab * kolicina);
            //        UK_sve += UK;
            //    }
            //    else if (row["nacin_placanja"].ToString() == "T")
            //    {
            //        sort = 3;
            //        UV = (mpc_s_rab * kolicina);
            //        UV_sve += UV;
            //    }
            //    else if (row["nacin_placanja"].ToString() == "O")
            //    {
            //        sort = 4;
            //        UO = (mpc_s_rab * kolicina);
            //        UO_sve += UO;
            //    }

            //    StopePDVaN(pdv, ppdv, row["nacin_placanja"].ToString(), ((mpc_s_rab * kolicina) - ((ppdv) + (ppnp))), povratnaNaknada, sort);

            //    decimal o = ((mpc_s_rab * kolicina) - ((ppdv) + (ppnp)));
            //    decimal p = ((mpc_s_rab * kolicina) * PreracunataStopaPDV) / 100;

            //    StopePDVa(pdv, ((mpc_s_rab * kolicina) * PreracunataStopaPDV) / 100, ((mpc_s_rab * kolicina) - ((ppdv) + (ppnp))), mpc_s_rab);
            //    osnovica = ((mpc_s_rab * kolicina) - ((ppdv) + (ppnp))) + osnovica;
            //}
            decimal vpc = 0;

            foreach (DataRow row in DT1.Rows)
            {
                povratnaNaknada = Convert.ToDecimal(row["povratna_naknada"].ToString());
                kolicina = Convert.ToDecimal(row["kolicina"].ToString());
                mpc = Convert.ToDecimal(row["mpc"].ToString()) - (povratnaNaknada == 0 ? povratnaNaknada : (kolicina == 0 ? 0 : povratnaNaknada / kolicina));
                pdv = Convert.ToDecimal(row["porez"].ToString());
                rabat = Convert.ToDecimal(row["rabat"].ToString());
                //vpc = Convert.ToDecimal(row["vpc"].ToString());
                //vpc = mpc / (1 + pdv / 100);

                decimal rabatIznos = Math.Round((mpc * (rabat / 100)), 6, MidpointRounding.AwayFromZero);
                decimal mpc_s_rab = Math.Round(mpc - rabatIznos, 6, MidpointRounding.AwayFromZero);
                decimal mpcSRabUkupno = Math.Round((mpc_s_rab * kolicina), 6, MidpointRounding.AwayFromZero);

                //Ovaj kod dobiva PDV
                decimal PreracunataStopaPDV = Convert.ToDecimal((100 * pdv) / (100 + pdv + porezNaPotrosnju));
                decimal pdvIznos = Math.Round(((mpc_s_rab * (Math.Round(PreracunataStopaPDV, 6, MidpointRounding.AwayFromZero) / 100)) * kolicina), 6, MidpointRounding.AwayFromZero);
                ukupnoPdv = pdvIznos + ukupnoPdv;

                ukupnoPovratnaNaknada += povratnaNaknada;

                //Ovaj kod dobiva porez na potrošnju
                decimal PreracunataStopaPorezNaPotrosnju = Convert.ToDecimal((100 * porezNaPotrosnju) / (100 + pdv + porezNaPotrosnju));
                decimal porezNaPotrosnjuIznos = Math.Round(((mpc_s_rab * (Math.Round(PreracunataStopaPorezNaPotrosnju, 6, MidpointRounding.AwayFromZero) / 100)) * kolicina), 6, MidpointRounding.AwayFromZero);
                ukupnoPorezNaPotrosnju = porezNaPotrosnjuIznos + ukupnoPorezNaPotrosnju;

                ukupnoSve = mpcSRabUkupno + ukupnoSve;

                decimal UG = 0;
                decimal UK = 0;
                decimal UV = 0;
                decimal UO = 0;

                int sort = 0;
                if (row["nacin_placanja"].ToString() == "G")
                {
                    sort = 1;
                    UG = mpcSRabUkupno;
                    UG_sve += UG;
                }
                else if (row["nacin_placanja"].ToString() == "K")
                {
                    sort = 2;
                    UK = mpcSRabUkupno;
                    UK_sve += UK;
                }
                else if (row["nacin_placanja"].ToString() == "T")
                {
                    sort = 3;
                    UV = mpcSRabUkupno;
                    UV_sve += UV;
                }
                else if (row["nacin_placanja"].ToString() == "O")
                {
                    sort = 4;
                    UO = mpcSRabUkupno;
                    UO_sve += UO;
                }

                StopePDVaN(pdv, pdvIznos, row["nacin_placanja"].ToString(), (mpcSRabUkupno - ((pdvIznos) + (porezNaPotrosnjuIznos))), povratnaNaknada, sort);

                StopePDVa(pdv, pdvIznos, (mpcSRabUkupno - ((pdvIznos) + (porezNaPotrosnjuIznos))), mpc_s_rab);
                osnovica = (mpcSRabUkupno - ((pdvIznos) + (porezNaPotrosnjuIznos))) + osnovica;
            }

            string porezi = "";

            DataView dv = DTpdvN.DefaultView;
            dv.Sort = "srt, stopa";
            DTpdvN = dv.ToTable();

            for (int i = 0; i < DTpdvN.Rows.Count; i++)
            {
                //if (Convert.ToDecimal(DTpdvN.Rows[i]["stopa"].ToString()) > 0 || !Class.Postavke.sustavPdv)
                if (true)
                {
                    string nacin_pplacanja = "";

                    if (DTpdvN.Rows[i]["nacin"].ToString().ToUpper() == "G")
                    {
                        nacin_pplacanja = "GOTOVINA";// +UG_sve + " kn";
                    }
                    else if (DTpdvN.Rows[i]["nacin"].ToString().ToUpper() == "T")
                    {
                        nacin_pplacanja = "TRANSAKCIJSKI RAČUN";// + UV_sve + " kn";
                    }
                    else if (DTpdvN.Rows[i]["nacin"].ToString().ToUpper() == "O")
                    {
                        nacin_pplacanja = "OSTALO";// + UO_sve + " kn";
                    }
                    else if (DTpdvN.Rows[i]["nacin"].ToString().ToUpper() == "K")
                    {
                        nacin_pplacanja = "KARTICE";// + UK_sve + " kn";
                    }
                    decimal ispisOsnovica = Math.Round(Convert.ToDecimal(DTpdvN.Rows[i]["osnovica"].ToString()), 2, MidpointRounding.AwayFromZero);
                    decimal ispisPorez = Math.Round(Convert.ToDecimal(DTpdvN.Rows[i]["iznos"].ToString()), 2, MidpointRounding.AwayFromZero);
                    decimal iznosPovratnaNaknada = Math.Round(Convert.ToDecimal(DTpdvN.Rows[i]["pov_nak"].ToString()), 2, MidpointRounding.AwayFromZero);

                    decimal ukupno_za_nacin_pl = ispisPorez + ispisOsnovica + iznosPovratnaNaknada;
                    porezi += "Način fiskaliziranja: " + nacin_pplacanja +
                        "\r\nOsnovica " + DTpdvN.Rows[i]["stopa"].ToString() + " %: " + ispisOsnovica.ToString("#0.00") + " kn" +
                        "\r\nIznos poreza: " + ispisPorez.ToString("#0.00") + " kn" +
                        "\r\nPovratna naknada: " + iznosPovratnaNaknada.ToString("#0.00") + " kn" +
                        "\r\nUkupno za " + nacin_pplacanja.ToLower() + ": " + ukupno_za_nacin_pl.ToString("#0.00") + " kn" +
                        "\r\n\r\n";
                }
            }
            dv = null;

            //porezi += Environment.NewLine + "Povratna naknada: " + ukupnoPovratnaNaknada.ToString("#0.00") + " kn";

            int a = Class.PosPrint.ispredArtikla;
            int k = Class.PosPrint.ispredKolicine;
            int c = Class.PosPrint.ispredCijene;
            int pp = Class.PosPrint.ispredPopusta;
            int s = Class.PosPrint.ispredUkupno;

            int pp_alt = pp + 2;
            int k_alt = k + 2;
            int s_alt = s + 2;
            int c_alt = c + 2;

            RecLineChars = a + k_alt + c_alt + pp_alt + s_alt;

            //Dodao 24.10.2013
            if (stavke_ispis)
            {
                PrintTextLine(string.Empty);
                PrintText(TruncateAt("STAVKA".PadRight(a), a));
                PrintText(TruncateAt("KOL".PadLeft(k_alt), k_alt));
                PrintText(TruncateAt("CIJENA".PadLeft(c_alt), c_alt));
                PrintText(TruncateAt("POP".PadLeft(pp_alt), pp_alt));
                PrintText(TruncateAt("UKUPNO".PadLeft(s_alt), s_alt));
                PrintText("\r\n");
                PrintTextLine(new string('=', RecLineChars));
            }

            string naziv_artika = "";
            //string sifra_artikla = "";
            decimal kol_artikla = 0;
            decimal rabat_postotak_artikla = 0;
            decimal mpc_artikla = 0;
            decimal ukupno_artikl = 0;
            decimal ukupno_po_rac = 0;
            decimal ukupno_rabat = 0;

            foreach (DataRow row1 in DT1.Rows)
            {
                decimal mpc_sa_rabatom = 0;
                naziv_artika = row1["naziv"].ToString();
                kol_artikla = Convert.ToDecimal(row1["kolicina"].ToString());
                mpc_artikla = Convert.ToDecimal(row1["mpc"].ToString());
                rabat_postotak_artikla = Convert.ToDecimal(row1["rabat"].ToString());
                mpc_sa_rabatom = mpc_artikla - (mpc_artikla * (rabat_postotak_artikla / 100));
                ukupno_artikl = mpc_sa_rabatom * kol_artikla;
                ukupno_rabat += mpc_artikla * (rabat_postotak_artikla / 100) * kol_artikla;
                // Dodano za apsolutne popuste
                if (row1[0].ToString() == "!popustABS") ukupno_rabat -= Convert.ToDecimal(row1["mpc"].ToString()) * kol_artikla;
                ukupno_po_rac += mpc_sa_rabatom * kol_artikla;

                if (stavke_ispis) PrintLineItem(naziv_artika, kol_artikla, mpc_artikla, rabat_postotak_artikla, ukupno_artikl);
            }

            if (stavke_ispis)
            {
                PrintTextLine(new string('=', RecLineChars));
                PrintText("\r\n");
            }
            else
            {
                ttekst += "\r\n\r\n";
            }
            //Dodao 24.10.2013

            ttekst += "RAZVRSTANO PO POREZIMA\r\n";

            dv = null;
            dv = DTpdv.DefaultView;
            dv.Sort = "stopa";
            DTpdv = dv.ToTable();
            for (int i = 0; i < DTpdv.Rows.Count; i++)
            {
                string Tpdv = Math.Round(Convert.ToDecimal(DTpdv.Rows[i]["iznos"].ToString()), 2, MidpointRounding.AwayFromZero).ToString("#0.00") + " kn";
                string Tosnovica = Math.Round(Convert.ToDecimal(DTpdv.Rows[i]["osnovica"].ToString()), 2, MidpointRounding.AwayFromZero).ToString("#0.00") + " kn";

                PrintText(TruncateAt("PDV " + DTpdv.Rows[i]["stopa"].ToString() + "%: ".PadRight(8), 8));
                PrintText(TruncateAt(Tpdv.PadLeft(11), 11));

                PrintText(TruncateAt("OSNOVICA: ".PadLeft(12), 12));
                PrintTextLine(TruncateAt(Tosnovica.PadLeft(13), 13));
            }

            dv = null;

            ttekst += "POVRATNA NAKNADA: " + Math.Round(ukupnoPovratnaNaknada, 2, MidpointRounding.AwayFromZero).ToString("#0.00") + " kn" + "\r\n";
            ttekst += "UKUPNO BEZ POV.NAK.: " + Math.Round(ukupnoSve, 2, MidpointRounding.AwayFromZero).ToString("#0.00") + " kn" + "\r\n";
            ttekst += "RABAT  : " + Math.Round(ukupno_rabat, 2, MidpointRounding.AwayFromZero).ToString("#0.00") + " kn" + "\r\n";
            ttekst += "UKUPNO S POV.NAK.: " + Math.Round(ukupnoSve + ukupnoPovratnaNaknada, 2, MidpointRounding.AwayFromZero).ToString("#0.00") + " kn" + "\r\n";
            if (Class.PodaciTvrtka.oibTvrtke == "67660751355") { ttekst += "UKUPNO BEZ RABATA: " + Math.Round(ukupnoSve + ukupno_rabat, 2, MidpointRounding.AwayFromZero).ToString("#0.00") + " kn"; }

            ttekst += "\r\n\r\n";
            PrintTextLine(new string('=', RecLineChars));
            ttekst += "DETALJNO PO NAČINIMA FISKALIZIRANJA:\r\n" + porezi;
        }

        private void PrintLineItem(string artikl, decimal kolicina, decimal cijena, decimal popust, decimal cijena_sve)
        {
            int a = Class.PosPrint.ispredArtikla;
            int k = Class.PosPrint.ispredKolicine;
            int c = Class.PosPrint.ispredCijene;
            int p = Class.PosPrint.ispredPopusta;
            int s = Class.PosPrint.ispredUkupno;
            int p_alt = p + 2;
            int k_alt = k + 2;
            int s_alt = s + 2;
            int c_alt = c + 2;

            RecLineChars = a + k_alt + c_alt + p_alt + s_alt;
            try
            {
                if (!Class.Postavke.ispis_cijele_stavke)
                {
                    PrintText(TruncateAt(artikl.PadRight(a), a));
                }
                else
                {
                    ttekst += classPrintStavke.StavkaZaPrinter(artikl, a);
                }
            }
            catch (Exception ex)
            {
                PrintText(TruncateAt(artikl.PadRight(a), a));
            }

            string pop = popust.ToString() + "%";
            PrintText(TruncateAt(kolicina.ToString("#0.00").PadLeft(k_alt), k_alt));
            PrintText(TruncateAt(cijena.ToString("#0.00").PadLeft(c_alt), c_alt));
            if (p != 0) { PrintText(TruncateAt(pop.PadLeft(p_alt), p_alt)); }
            PrintTextLine(TruncateAt(cijena_sve.ToString("#0.00").PadLeft(s_alt), s_alt));
        }

        private void PrintReceiptHeader(string companyName, string addressLine1, string taxNumber, DateTime[] dateTime, string cashierName, string odDo = null)
        {
            int a = Class.PosPrint.ispredArtikla;
            int k = Class.PosPrint.ispredKolicine;
            int c = Class.PosPrint.ispredCijene;
            int p = Class.PosPrint.ispredPopusta;
            int s = Class.PosPrint.ispredUkupno;

            int p_alt = p + 2;
            int k_alt = k + 2;
            int s_alt = s + 2;
            int c_alt = c + 2;

            RecLineChars = a + k_alt + c_alt + p_alt + s_alt;
            string[] dib = Util.Korisno.VratiDucanIBlagajnu();
            string ducan = dib[0];
            string blagajna = dib[1];
            if (companyName != "")
            {
                ttekst += companyName + "\r\n";
            }
            else
            {
                PrintTextLine(Class.PodaciTvrtka.kratkiNazivTvrtke);
                if (addressLine1 != "") PrintTextLine("ADRESA: " + addressLine1);
                if (Class.PodaciTvrtka.adresaPoslovnice != "") PrintTextLine("POSLOVNICA: " + Class.PodaciTvrtka.adresaPoslovnice);
                if (taxNumber != "") PrintTextLine("TELEFON: " + taxNumber);
                if (Class.PodaciTvrtka.oibTvrtke != "") PrintTextLine("OIB: " + Class.PodaciTvrtka.oibTvrtke);
            }

            PrintTextLine("DATUM: " + DateTime.Now);
            PrintTextLine(new string('-', RecLineChars));

            if (ducan != null || ducan != "")
            {
                ttekst += "DUĆAN: " + ducan + "\r\n";
            }
            PrintTextLine("DATUM OD: " + dateTime[0]);
            PrintTextLine("DATUM DO : " + dateTime[1]);
            if (odDo != null)
            {
                ttekst += odDo + Environment.NewLine;
            }
            _1 = ttekst;
            RecLineChars = 0;
        }

        private void PrintText(string text)
        {
            if (text.Length <= RecLineChars)
                ttekst += text;
            else if (text.Length > RecLineChars)
                ttekst += TruncateAt(text, RecLineChars);
        }

        private void PrintTextLine(string text)
        {
            if (text.Length < RecLineChars)
                ttekst += text + Environment.NewLine;
            else if (text.Length > RecLineChars)
                ttekst += TruncateAt(text, RecLineChars);
            else if (text.Length == RecLineChars)
                ttekst += text + Environment.NewLine;
        }

        private string TruncateAt(string text, int maxWidth)
        {
            string retVal = text;
            if (text.Length > maxWidth)
            {
                retVal = text.Substring(0, maxWidth);
            }
            return retVal;
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

        private void StopePDVaN(decimal pdv, decimal pdv_stavka, string nacin_P, decimal osnovica, decimal pov_nak = 0, int srt = 0)
        {
            DataRow[] dataROW = DTpdvN.Select("stopa = '" + Convert.ToInt16(pdv).ToString() + "' AND nacin='" + nacin_P + "'");

            if (dataROW.Count() == 0)
            {
                RowPdv = DTpdvN.NewRow();
                RowPdv["stopa"] = Convert.ToInt16(pdv).ToString();
                RowPdv["iznos"] = pdv_stavka.ToString();
                RowPdv["nacin"] = nacin_P;
                RowPdv["osnovica"] = osnovica;
                RowPdv["pov_nak"] = pov_nak;
                RowPdv["srt"] = srt;

                DTpdvN.Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["iznos"] = Convert.ToDecimal(dataROW[0]["iznos"].ToString()) + pdv_stavka;
                dataROW[0]["osnovica"] = Convert.ToDecimal(dataROW[0]["osnovica"].ToString()) + osnovica;
                dataROW[0]["pov_nak"] = Convert.ToDecimal(dataROW[0]["pov_nak"].ToString()) + pov_nak;
            }
        }

        private void StopePDVa(decimal pdv, decimal pdv_stavka, decimal osnovica, decimal iznos_ukupno)
        {
            DataRow[] dataROW = DTpdv.Select("stopa = '" + Convert.ToInt16(pdv).ToString() + "'");

            if (dataROW.Count() == 0)
            {
                RowPdv = DTpdv.NewRow();
                RowPdv["stopa"] = Convert.ToInt16(pdv).ToString();
                RowPdv["iznos"] = pdv_stavka.ToString();
                RowPdv["osnovica"] = osnovica.ToString();
                RowPdv["iznos_ukupno"] = iznos_ukupno.ToString();
                DTpdv.Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["iznos"] = Convert.ToDecimal(dataROW[0]["iznos"].ToString()) + pdv_stavka;
                dataROW[0]["osnovica"] = Convert.ToDecimal(dataROW[0]["osnovica"].ToString()) + osnovica;
                dataROW[0]["iznos_ukupno"] = Convert.ToDecimal(dataROW[0]["iznos_ukupno"].ToString()) + iznos_ukupno;
            }
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
            PB.Size = new Size(w + 3, h + 3);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new Size(w - 3, h - 3);
        }

        private void print(string ttx)
        {
            string printerName = Class.PosPrint.windowsPrinterName1;
            PrintDocument printDoc = new PrintDocument();

            printDoc.PrinterSettings.PrinterName = printerName;

            //if (!Class.Postavke.direct_print)
            //{
            //    if (!printDoc.PrinterSettings.IsValid)
            //    {
            //        string msg = string.Format(
            //           "Can't find printer \"{0}\".", printerName);
            //        MessageBox.Show(msg, "Print Error");
            //        return;
            //    }
            //    //_1 = ttx;
            //    printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            //    printDoc.Print();
            //}
            //else
            //{
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

            for (int i = 0; i < Class.PosPrint.linijaPraznihBottom; i++)
            {
                ttx += Environment.NewLine;
            }
            RawPrinterHelper.SendStringToPrinter(printDoc.PrinterSettings.PrinterName, ttx + COMMAND);
            // }
        }

        private void PrintPage(object o, PrintPageEventArgs e)
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
                    if (System.IO.File.Exists("C://logo/logo.jpg"))
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
                drawString = _4;
                drawFont = font;
                y = height;
                x = 0.0F;
                drawFormat = new StringFormat();
                e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
                stringSize = e.Graphics.MeasureString(_4, drawFont);

                height = float.Parse(stringSize.Height.ToString()) + height;

                //Barcode
                //if (img_barcode != null)
                //{
                //    System.Drawing.Image img = img_barcode;
                //    e.Graphics.DrawImage(img_barcode, 0, height, 250, 50);
                //    height = 60 + height;
                //}

                //Bottom
                drawString = _5;
                drawFont = font;
                y = height;
                x = 0.0F;
                drawFormat = new StringFormat();
                e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);

                stringSize = e.Graphics.MeasureString(_5, drawFont);

                height = float.Parse(stringSize.Height.ToString()) + height;

                if (e.HasMorePages)
                {
                    e.HasMorePages = false;
                }

                if (height > e.PageSettings.PaperSize.Height)
                {
                    PaperSize psNew = new System.Drawing.Printing.PaperSize("Racun", e.PageSettings.PrinterSettings.DefaultPageSettings.PaperSize.Width, (int)height + 1);
                    Size sSize = new Size(psNew.Width, psNew.Height);

                    e.PageSettings.PrinterSettings.DefaultPageSettings.PaperSize = psNew;
                    e.PageSettings.PrinterSettings.DefaultPageSettings.Bounds.Inflate(sSize);
                    e.PageSettings.PrinterSettings.DefaultPageSettings.PrintableArea.Inflate(sSize);
                    e.PageSettings.PrinterSettings.DefaultPageSettings.PrinterSettings.DefaultPageSettings.PrintableArea.Inflate(sSize);

                    e.PageSettings.PaperSize = psNew;

                    e.PageSettings.Bounds.Inflate(sSize);

                    e.PageBounds.Inflate(sSize);

                    e.PageSettings.PrintableArea.Inflate(sSize);

                    e.HasMorePages = true;
                    e.Graphics.Clear(Color.White);
                    e.Graphics.ResetClip();
                    e.Graphics.Clip.MakeEmpty();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Font za ispis nije pronađen!" + Environment.NewLine + Environment.NewLine +
                ex.Message, "Upozorenje!");
            }
        }

        private void frmPosPreview_FormClosing(object sender, FormClosingEventArgs e)
        {
            ttekst = "";
        }
    }
}