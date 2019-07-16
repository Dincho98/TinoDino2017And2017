using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Report.Inventura.VisakManjak
{
    public partial class frmInventuraVisakManjak : Form
    {
        private INIFile ini = new INIFile();
        private bool Visak = false;
        private string Broj = null;
        private string Skladiste = null;
        private string Godina = null;

        public frmInventuraVisakManjak(bool _visak, string _broj, string _skladiste, string _godina)
        {
            Visak = _visak;
            Broj = _broj;
            Skladiste = _skladiste;
            Godina = _godina;
            InitializeComponent();
        }

        private void frmInventura_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            LoadDocument();

            string vis = "0";
            if (Visak) vis = "1";
            ReportParameter p1 = new ReportParameter("SamoVisak", vis);
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1 });

            this.reportViewer1.RefreshReport();
        }

        private string nbc = "";

        private void LoadDocument()
        {
            string naziv_fakture = " podaci_tvrtka.naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql("", naziv_fakture, "");

            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            if (ini.Read("POSTAVKE", "inventura_nabavna") != "")
            {
                nbc = ini.Read("POSTAVKE", "inventura_nabavna");
            }

            string where_razlike = "";

            if (Visak)
                where_razlike = " AND (coalesce(inventura_stavke.kolicina_koja_je_bila,0)-coalesce(CAST(REPLACE(inventura_stavke.kolicina,',','.') as NUMERIC),0))<0 ";
            else
                where_razlike = " AND (coalesce(inventura_stavke.kolicina_koja_je_bila,0)-coalesce(CAST(REPLACE(inventura_stavke.kolicina,',','.') as NUMERIC),0))>0 ";

            #region UZIMAM VRIJEDNOSTI IZ INVENTURE

            string sql_liste = "SELECT  " +
                " roba_prodaja.sifra AS sifra," +
                " inventura_stavke.nbc as cijena1, " +
                " (inventura_stavke.cijena) as rabat1, " +
                " inventura_stavke.kolicina_koja_je_bila as cijena2" +
                " ," +
                " coalesce(CAST(REPLACE(inventura_stavke.kolicina,',','.') as NUMERIC),0) as cijena9 ," +
                " (inventura_stavke.cijena/(1 zbroj CAST(REPLACE(roba_prodaja.porez,',','.') AS numeric)/100)) as cijena8," +
                " roba.naziv," +
                " inventura_stavke.porez " +
                " FROM roba_prodaja  " +
                " LEFT JOIN inventura_stavke ON roba_prodaja.sifra=inventura_stavke.sifra_robe AND inventura_stavke.broj_inventure='" + Broj + "' " +
                " LEFT JOIN roba ON roba_prodaja.sifra=roba.sifra " +
                " WHERE  " +
                " roba_prodaja.id_skladiste='" + Skladiste + "' AND roba.oduzmi='DA' " + where_razlike + ";";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            #endregion UZIMAM VRIJEDNOSTI IZ INVENTURE

            #region OVDJE UZIMAM INVENTURNE VIŠKOVE I MANJKOVE I SPRAMAM U DATASET ZA REPORT

            string query = @"SELECT
                        COALESCE(SUM(
                        (CAST(REPLACE(kolicina,',','.') AS NUMERIC)-
                        kolicina_koja_je_bila)*
                        nbc
                        ),0) AS nbc,
                        COALESCE(SUM(
                        (CAST(REPLACE(kolicina,',','.') AS NUMERIC)-
                        kolicina_koja_je_bila)*
                        (cijena/(1+(CAST(REPLACE(porez,',','.') AS NUMERIC)/100)))
                        ),0) AS vpc,
                        COALESCE(SUM(
                        (CAST(REPLACE(kolicina,',','.') AS NUMERIC)-
                        kolicina_koja_je_bila)*
                        cijena
                        ),0) AS mpc
                        FROM inventura_stavke
                        WHERE broj_inventure='@broj' AND (CAST(REPLACE(kolicina,',','.') AS NUMERIC)-kolicina_koja_je_bila)<0;";

            query = query.Replace("@broj", Broj).Replace("+", "zbroj");
            DataTable DTinvManjak = classSQL.select(query, "inv").Tables[0];

            query = @"SELECT
                        COALESCE(SUM(
                        (CAST(REPLACE(kolicina,',','.') AS NUMERIC)-
                        kolicina_koja_je_bila)*
                        nbc
                        ),0) AS nbc,
                        COALESCE(SUM(
                        (CAST(REPLACE(kolicina,',','.') AS NUMERIC)-
                        kolicina_koja_je_bila)*
                        (cijena/(1+(CAST(REPLACE(porez,',','.') AS NUMERIC)/100)))
                        ),0) AS vpc,
                        COALESCE(SUM(
                        (CAST(REPLACE(kolicina,',','.') AS NUMERIC)-
                        kolicina_koja_je_bila)*
                        cijena
                        ),0) AS mpc
                        FROM inventura_stavke
                        WHERE broj_inventure='@broj' AND (CAST(REPLACE(kolicina,',','.') AS NUMERIC)-kolicina_koja_je_bila)>0;";

            query = query.Replace("@broj", Broj).Replace("+", "zbroj");
            DataTable DTinvVisak = classSQL.select(query, "inv").Tables[0];

            DataRow ROW = listaUniverzalna.Tables[0].NewRow();
            ROW["decimal1"] = DTinvManjak.Rows[0]["nbc"].ToString();
            ROW["decimal2"] = DTinvManjak.Rows[0]["vpc"].ToString();
            ROW["decimal3"] = DTinvManjak.Rows[0]["mpc"].ToString();
            ROW["decimal4"] = DTinvVisak.Rows[0]["nbc"].ToString();
            ROW["decimal5"] = DTinvVisak.Rows[0]["vpc"].ToString();
            ROW["decimal6"] = DTinvVisak.Rows[0]["mpc"].ToString();
            listaUniverzalna.Tables[0].Rows.Add(ROW);

            query = @"SELECT
                    COALESCE(SUM(kolicina_koja_je_bila*nbc),0) AS skladiste,
                    COALESCE(SUM(CAST(REPLACE(kolicina,',','.') AS NUMERIC)*nbc),0) AS stvarno,
                    COALESCE((SUM((CAST(REPLACE(kolicina,',','.') AS NUMERIC)*nbc)-(kolicina_koja_je_bila*nbc))),0) AS razlika,

                    COALESCE(SUM(kolicina_koja_je_bila*(cijena/(1+(CAST(REPLACE(porez,',','.') AS NUMERIC)/100)))),0) AS skladiste_vpc,
                    COALESCE(SUM(CAST(REPLACE(kolicina,',','.') AS NUMERIC)*(cijena/(1+(CAST(REPLACE(porez,',','.') AS NUMERIC)/100)))),0) AS stvarno_vpc,
                    COALESCE((SUM((CAST(REPLACE(kolicina,',','.') AS NUMERIC)*(cijena/(1+(CAST(REPLACE(porez,',','.') AS NUMERIC)/100))))-(kolicina_koja_je_bila*(cijena/(1+(CAST(REPLACE(porez,',','.') AS NUMERIC)/100)))))),0) AS razlika_vpc,

                    COALESCE(SUM(kolicina_koja_je_bila*cijena),0) AS skladiste_mpc,
                    COALESCE(SUM(CAST(REPLACE(kolicina,',','.') AS NUMERIC)*cijena),0) AS stvarno_mpc,
                    COALESCE((SUM((CAST(REPLACE(kolicina,',','.') AS NUMERIC)*cijena)-(kolicina_koja_je_bila*cijena))),0) AS razlika_mpc
                    FROM inventura_stavke
                    WHERE broj_inventure='@broj';";
            query = query.Replace("@broj", Broj).Replace("+", "zbroj");
            DataTable DTinvSumirano = classSQL.select(query, "inv").Tables[0];

            #endregion OVDJE UZIMAM INVENTURNE VIŠKOVE I MANJKOVE I SPRAMAM U DATASET ZA REPORT

            string year = "";
            DataTable DT = classSQL.select("SELECT datum FROM inventura WHERE broj_inventure='" + Broj + "'", "inventura").Tables[0];

            if (DT.Rows.Count > 0)
            {
                year = Convert.ToDateTime(DT.Rows[0]["datum"].ToString()).Year.ToString();
            }
            string tbl3 = "Cijena";
            if (nbc == "1")
            {
                tbl3 = "Nbc";
            }

            decimal SumSkladiste = 0, SumKartice = 0, SumRazlika = 0, SumSkladisteVpc = 0, SumKarticeVpc = 0, SumRazlikaVpc = 0, SumSkladisteMpc = 0, SumKarticeMpc = 0, SumRazlikaMpc = 0;

            if (DTinvSumirano.Rows.Count > 0)
            {
                decimal.TryParse(DTinvSumirano.Rows[0]["skladiste"].ToString(), out SumSkladiste);
                decimal.TryParse(DTinvSumirano.Rows[0]["stvarno"].ToString(), out SumKartice);
                decimal.TryParse(DTinvSumirano.Rows[0]["razlika"].ToString(), out SumRazlika);

                decimal.TryParse(DTinvSumirano.Rows[0]["skladiste_vpc"].ToString(), out SumSkladisteVpc);
                decimal.TryParse(DTinvSumirano.Rows[0]["stvarno_vpc"].ToString(), out SumKarticeVpc);
                decimal.TryParse(DTinvSumirano.Rows[0]["razlika_vpc"].ToString(), out SumRazlikaVpc);

                decimal.TryParse(DTinvSumirano.Rows[0]["skladiste_mpc"].ToString(), out SumSkladisteMpc);
                decimal.TryParse(DTinvSumirano.Rows[0]["stvarno_mpc"].ToString(), out SumKarticeMpc);
                decimal.TryParse(DTinvSumirano.Rows[0]["razlika_mpc"].ToString(), out SumRazlikaMpc);
            }

            string sql_liste_string = "SELECT " +
                " 'Šifra ' AS tbl1," +
                " 'Naziv' AS tbl2," +
                " '" + tbl3 + "' AS tbl3," +
                " 'Kol.Kar' AS tbl4," +
                " 'Iznos.Kar' AS tbl5," +
                " 'Kol.inv' AS tbl6," +
                " 'Iznos inv' AS tbl7," +
                " 'Razlika' AS tbl8," +
                " 'Iznos' AS tbl9," +
                " '" + Math.Round(SumSkladisteVpc, 3).ToString("N2") + "' AS ukupno1," +
                " '" + Math.Round(SumKarticeVpc, 3).ToString("N2") + "' AS ukupno2," +
                " '" + Math.Round(SumRazlikaVpc, 3).ToString("N2") + "' AS ukupno3," +

                " '" + Math.Round(SumSkladiste, 3).ToString("N2") + "' AS ukupno4," +
                " '" + Math.Round(SumKartice, 3).ToString("N2") + "' AS ukupno5," +
                " '" + Math.Round(SumRazlika, 3).ToString("N2") + "' AS ukupno6," +

                " '" + Math.Round(SumSkladisteMpc, 3).ToString("N2") + "' AS ukupno7," +
                " '" + Math.Round(SumKarticeMpc, 3).ToString("N2") + "' AS ukupno8," +
                " '" + Math.Round(SumRazlikaMpc, 3).ToString("N2") + "' AS ukupno9," +

                " '" + Math.Round(SumSkladisteMpc - SumSkladisteVpc, 3).ToString("N2") + "' AS ukupno10," +
                " '" + Math.Round(SumKarticeMpc - SumKarticeVpc, 3).ToString("N2") + "' AS ukupno11," +
                " '" + Math.Round(SumRazlikaMpc - SumRazlikaVpc, 3).ToString("N2") + "' AS ukupno12," +

                " inventura.datum AS datum1," +
                " inventura.napomena AS komentar," +
                " 'Skladište: '+skladiste.skladiste AS skladiste," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS string1," +
                " CAST ('INVENTURA  ' AS nvarchar) + CAST (inventura.broj_inventure AS nvarchar) +'/'+ CAST (" + year + " AS nvarchar) AS naslov" +
                " FROM inventura " +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=inventura.id_skladiste " +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=inventura.id_zaposlenik " +
                " WHERE inventura.broj_inventure ='" + Broj + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste_string.Replace("nvarchar", "varchar")).Fill(dSRlisteTekst, "DTlisteTekst");
            }
        }
    }
}