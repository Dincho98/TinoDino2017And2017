using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace PCPOS.Report.Inventura
{
    public partial class frmInventura : Form
    {
        public frmInventura()
        {
            InitializeComponent();
        }

        private INIFile ini = new INIFile();
        public string broj_dokumenta { get; set; }
        public DateTime datum { get; set; }
        public string skladiste { get; set; }
        public string godina { get; set; }

        public string dokumenat { get; set; }
        public string ImeForme { get; set; }
        public string samo_razlike { get; set; }

        private bool ispis_proizvodacka_cijena = false;

        private void frmInventura_Load(object sender, EventArgs e)
        {
            if (Class.PodaciTvrtka.oibTvrtke == "88985647471")
            {
                if (MessageBox.Show("Želite ispisati inventuru s proizvođačkim cijenama?", "Inventura", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ispis_proizvodacka_cijena = true;
                }
            }

            ReportDataSource visakManjakDataSource = new ReportDataSource();

            visakManjakDataSource.Name = "DSvisakManjakStope";
            visakManjakDataSource.Value = dSstope.Tables[2];
            reportViewer1.LocalReport.DataSources.Add(visakManjakDataSource);

            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Inventura.inventura.rdlc";

            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            LoadDocument();
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

            switch (samo_razlike)
            {
                case "DA":
                    where_razlike = " AND coalesce(inventura_stavke.kolicina_koja_je_bila,0) <> coalesce(CAST(REPLACE(inventura_stavke.kolicina,',','.') as NUMERIC),0)";
                    break;

                case null:
                    where_razlike = " AND roba.sifra IN (SELECT inventura_stavke.sifra_robe WHERE inventura_stavke.broj_inventure='" + broj_dokumenta + "' AND (kolicina_koja_je_bila<>'0' OR coalesce(CAST(REPLACE(inventura_stavke.kolicina,',','.') as NUMERIC),0)<>'0'))";
                    break;

                case "NE":
                    where_razlike = " AND (roba.sifra IN (SELECT inventura_stavke.sifra_robe WHERE inventura_stavke.broj_inventure='" + broj_dokumenta + "') OR inventura_stavke.kolicina_koja_je_bila <> coalesce(CAST(REPLACE(inventura_stavke.kolicina,',','.') AS DECIMAL),0))";
                    break;
            }

            #region OVDJE PROVJERAVAM DALI POSTOJI DOBRA CIJENA, POREZ, NBC

            //POSTAVLJAM NABAVNE CIJENE AKO JE NBC=0
            DataTable DTnbcError = classSQL.select(@"SELECT * FROM inventura_stavke WHERE nbc='0'
                                        AND (CAST(REPLACE(kolicina,',','.') AS NUMERIC)-kolicina_koja_je_bila)<>0
                                        AND inventura_stavke.broj_inventure='" + broj_dokumenta + "';", "nbc").Tables[0];
            string sql_update = "BEGIN;";
            foreach (DataRow r in DTnbcError.Rows)
            {
                decimal _NBC = Util.Korisno.VratiNabavnuCijenu(r["sifra_robe"].ToString(), skladiste);
                sql_update += "UPDATE inventura_stavke SET nbc='" + Math.Round(_NBC, 4).ToString().Replace(",", ".") + "' WHERE id_stavke='" + r["id_stavke"].ToString() + "';";
            }
            if (sql_update.Length > 8)
            {
                sql_update += "COMMIT;";
                classSQL.update(sql_update);
            }

            //OVDJE KONTROLIRAM POREZ CIJENU
            string sql_inv = "SELECT id_stavke,sifra_robe,cijena,porez FROM inventura_stavke WHERE (cijena='0' OR cijena IS NULL OR porez='') AND inventura_stavke.broj_inventure='" + broj_dokumenta + "';";
            DataTable DTinvProvjera = classSQL.select(sql_inv, "inv").Tables[0];
            DataTable DTinvRoba = classSQL.select(@"SELECT sifra,coalesce(vpc,0) as vpc,porez FROM roba_prodaja
                                                    WHERE sifra
                                                    IN (SELECT sifra_robe AS sifra FROM inventura_stavke WHERE cijena='0' OR cijena IS NULL OR porez='' AND inventura_stavke.broj_inventure='" + broj_dokumenta + @"')
                                                    AND id_skladiste='" + skladiste + "'", "roba").Tables[0];
            sql_update = "BEGIN;";
            decimal cijenaInv = 0, porezInv = 0;
            foreach (DataRow r in DTinvProvjera.Rows)
            {
                DataRow[] rRoba = DTinvRoba.Select("sifra='" + r["sifra_robe"].ToString() + "'");
                if (rRoba.Length > 0)
                {
                    decimal.TryParse(rRoba[0]["vpc"].ToString(), out cijenaInv);
                    decimal.TryParse(rRoba[0]["porez"].ToString(), out porezInv);
                    if (porezInv > 0)
                        cijenaInv = cijenaInv / (1 / (porezInv / 100));
                }
                else
                {
                    cijenaInv = 0;
                }

                sql_update += "UPDATE inventura_stavke SET porez='" + Math.Round(porezInv, 3).ToString().Replace(",", ".") + "', cijena='" + Math.Round(cijenaInv, 3).ToString().Replace(",", ".") + "' WHERE id_stavke='" + r["id_stavke"].ToString() + "';";
            }
            // TODO: Test komentar za TODO da vidim dal se pojavi u Task List-u
            // HACK: Test komentar za HACK da vidim dal se pojavi u Task List-u
            // NOTE: Test komentar za NOTE da vidim dal se pojavi u Task List-u
            // UNDONE: Test komentar za UNDONE da vidim dal se pojavi u Task List-u
            if (sql_update.Length > 8)
            {
                sql_update += "COMMIT;";
                classSQL.update(sql_update);
            }

            #endregion OVDJE PROVJERAVAM DALI POSTOJI DOBRA CIJENA, POREZ, NBC

            string cijena1 = "inventura_stavke.cijena", cijena8 = "(inventura_stavke.cijena/(1 zbroj CAST(REPLACE(roba_prodaja.porez,',','.') AS numeric)/100))";

            if (nbc == "1")
            {
                cijena1 = "inventura_stavke.nbc";
                cijena8 = "(inventura_stavke.cijena/(1 zbroj CAST(REPLACE(roba_prodaja.porez,',','.') AS numeric)/100))";
            }

            if (ispis_proizvodacka_cijena)
            {
                cijena8 = "inventura_stavke.proizvodacka_cijena";
                cijena1 = "inventura_stavke.nbc";
            }

            #region UZIMAM VRIJEDNOSTI IZ INVENTURE

            string sql_liste = string.Format(@"SELECT
roba_prodaja.sifra AS sifra,
{3} as cijena1,
(inventura_stavke.cijena) as rabat1,
inventura_stavke.kolicina_koja_je_bila as cijena2,
coalesce(CAST(REPLACE(inventura_stavke.kolicina,',','.') as NUMERIC),0) as cijena9,
{4} as cijena8,
roba.naziv,
round(inventura_stavke.proizvodacka_cijena, 2) as proizvodacka_cijena,
inventura_stavke.porez
FROM roba_prodaja
left JOIN inventura_stavke ON roba_prodaja.sifra=inventura_stavke.sifra_robe AND inventura_stavke.broj_inventure = '{0}'
LEFT JOIN roba ON roba_prodaja.sifra=roba.sifra
WHERE roba_prodaja.id_skladiste = '{1}' AND roba.oduzmi = 'DA'{2};",
                broj_dokumenta,
                skladiste,
                where_razlike,
                cijena1,
                cijena8);

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            string sql = string.Format(@"drop table if exists tmpInv;
create temp table tmpInv as
select broj_inventure::integer as broj,
sifra_robe as sifra,
round(replace(kolicina, ',', '.')::numeric, 6) as upisana_kolicina,
round(kolicina_koja_je_bila, 6) as programska_kolicina,
round(replace(porez, ',', '.')::numeric, 2) as pdv,
round((cijena / (1 zbroj (round(replace(porez, ',', '.')::numeric, 2) / 100))), 6) as vpc,
coalesce(povratna_naknada.iznos, 0) as povratna_naknada
from inventura_stavke
left join (select distinct sifra, iznos from povratna_naknada) as povratna_naknada on inventura_stavke.sifra_robe = povratna_naknada.sifra
where broj_inventure::integer = 1 and round(replace(kolicina, ',', '.')::numeric, 6) <> 0 and round(kolicina_koja_je_bila, 6) <> 0
order by broj, sifra;

select
coalesce(round(sum(programska_kolicina * povratna_naknada), 2), 0) as naknada_programa,
coalesce(round(sum(upisana_kolicina * povratna_naknada), 2), 0) as naknada_inventure,
coalesce(round(sum(programska_kolicina * povratna_naknada) - sum(upisana_kolicina * povratna_naknada), 2), 0) as naknada_razlika
from tmpInv;");

            DataSet dsTemp = classSQL.select(sql, "povratna");

            double proizvodacka_cijena_ukupno = 0;

            for (int i = 0; i < dSRliste.Tables[0].Rows.Count; i++)
            {
                double kolicina = 0, proizvodacka_cijena = 0;
                double.TryParse(dSRliste.Tables[0].Rows[i]["cijena9"].ToString(), out kolicina);
                double.TryParse(dSRliste.Tables[0].Rows[i]["proizvodacka_cijena"].ToString(), out proizvodacka_cijena);
                proizvodacka_cijena_ukupno += Math.Round((kolicina * proizvodacka_cijena), 2, MidpointRounding.AwayFromZero);

                // TODO: tu more izracunati porez

                double osnovica = 0, iznos = 0, porez = 0;
                double.TryParse(dSRliste.Tables[0].Rows[i]["porez"].ToString(), out porez);
                double.TryParse(dSRliste.Tables[0].Rows[i]["cijena8"].ToString(), out osnovica);

                osnovica = Math.Round(osnovica, 6, MidpointRounding.AwayFromZero);
                iznos = Math.Round((osnovica * (porez / 100)), 6, MidpointRounding.AwayFromZero);
                if (kolicina != 0)
                {
                    //DataRow[] dataROW = DTpdv.Select("stopa = '" + stopa.ToString("0.00") + "'");
                    DataRow[] stope = (dSstope.Tables[0]).Select(string.Format("stopa='{0}'", porez.ToString("#0.00")));
                    if (stope.Length == 0)
                    {
                        DataRow stopa = dSstope.Tables[0].NewRow();

                        stopa["stopa"] = porez.ToString("#0.00");
                        stopa["osnovica"] = Math.Round((osnovica * kolicina), 2, MidpointRounding.AwayFromZero);
                        stopa["iznos"] = Math.Round((iznos * kolicina), 2, MidpointRounding.AwayFromZero);

                        dSstope.Tables[0].Rows.Add(stopa);
                    }
                    else
                    {
                        double osnovicaOld = 0, iznosOld = 0;
                        double.TryParse(stope[0]["osnovica"].ToString(), out osnovicaOld);
                        double.TryParse(stope[0]["iznos"].ToString(), out iznosOld);

                        stope[0]["osnovica"] = (osnovicaOld + Math.Round((osnovica * kolicina), 2, MidpointRounding.AwayFromZero));
                        stope[0]["iznos"] = (iznosOld + Math.Round((iznos * kolicina), 2, MidpointRounding.AwayFromZero));
                    }
                }
            }

            ReportParameter p1 = new ReportParameter("proizvodacka_cijena", proizvodacka_cijena_ukupno.ToString("#,##0.00"));
            ReportParameter p2 = new ReportParameter("ispis_proizvodacka_cijena", ispis_proizvodacka_cijena.ToString());
            ReportParameter p3 = new ReportParameter("naknada_programa", dsTemp.Tables[0].Rows[0]["naknada_programa"].ToString());
            ReportParameter p4 = new ReportParameter("naknada_inventure", dsTemp.Tables[0].Rows[0]["naknada_inventure"].ToString());
            ReportParameter p5 = new ReportParameter("naknada_razlika", dsTemp.Tables[0].Rows[0]["naknada_razlika"].ToString());

            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5 });

            #endregion UZIMAM VRIJEDNOSTI IZ INVENTURE

            #region OVDJE UZIMAM INVENTURNE VIŠKOVE I MANJKOVE I SPREMAM U DATASET ZA REPORT

            SetVisakManjakStope();

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

            query = query.Replace("@broj", broj_dokumenta).Replace("+", "zbroj");
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

            query = query.Replace("@broj", broj_dokumenta).Replace("+", "zbroj");
            DataTable DTinvVisak = classSQL.select(query, "inv").Tables[0];

            DataRow ROW = listaUniverzalna.Tables[0].NewRow();
            ROW["decimal1"] = DTinvManjak.Rows[0]["nbc"].ToString();
            ROW["decimal2"] = DTinvManjak.Rows[0]["vpc"].ToString();
            ROW["decimal3"] = DTinvManjak.Rows[0]["mpc"].ToString();
            ROW["decimal4"] = DTinvVisak.Rows[0]["nbc"].ToString();
            ROW["decimal5"] = DTinvVisak.Rows[0]["vpc"].ToString();
            ROW["decimal6"] = DTinvVisak.Rows[0]["mpc"].ToString();
            listaUniverzalna.Tables[0].Rows.Add(ROW);

            //COALESCE((SUM((CAST(REPLACE(kolicina,',','.') AS NUMERIC)*nbc)-(kolicina_koja_je_bila*nbc))),0) AS razlika,
            query = @"SELECT
                    COALESCE(SUM(kolicina_koja_je_bila*nbc),0) AS skladiste,
                    COALESCE(SUM(CAST(REPLACE(kolicina,',','.') AS NUMERIC)*nbc),0) AS stvarno,
                    sum((REPLACE(kolicina,',','.')::numeric - kolicina_koja_je_bila) * nbc) as razlika,

                    COALESCE(SUM(kolicina_koja_je_bila*(cijena/(1+(CAST(REPLACE(porez,',','.') AS NUMERIC)/100)))),0) AS skladiste_vpc,
                    COALESCE(SUM(CAST(REPLACE(kolicina,',','.') AS NUMERIC)*(cijena/(1+(CAST(REPLACE(porez,',','.') AS NUMERIC)/100)))),0) AS stvarno_vpc,
                    COALESCE((SUM((CAST(REPLACE(kolicina,',','.') AS NUMERIC)*(cijena/(1+(CAST(REPLACE(porez,',','.') AS NUMERIC)/100))))-(kolicina_koja_je_bila*(cijena/(1+(CAST(REPLACE(porez,',','.') AS NUMERIC)/100)))))),0) AS razlika_vpc,

                    COALESCE(SUM(kolicina_koja_je_bila*cijena),0) AS skladiste_mpc,
                    COALESCE(SUM(CAST(REPLACE(kolicina,',','.') AS NUMERIC)*cijena),0) AS stvarno_mpc,
                    COALESCE((SUM((CAST(REPLACE(kolicina,',','.') AS NUMERIC)*cijena)-(kolicina_koja_je_bila*cijena))),0) AS razlika_mpc
                    FROM inventura_stavke
                    WHERE broj_inventure='@broj';";

            if (ispis_proizvodacka_cijena)
            {
                query = @"SELECT
                    COALESCE(SUM(kolicina_koja_je_bila*inventura_stavke.proizvodacka_cijena),0) AS skladiste,
                    COALESCE(SUM(CAST(REPLACE(kolicina,',','.') AS NUMERIC) * inventura_stavke.proizvodacka_cijena),0) AS stvarno,
                    sum((REPLACE(kolicina,',','.')::numeric - kolicina_koja_je_bila) * inventura_stavke.proizvodacka_cijena) as razlika,

                    COALESCE(SUM(kolicina_koja_je_bila * nbc),0) AS skladiste_vpc,
                    COALESCE(SUM(CAST(REPLACE(kolicina,',','.') AS NUMERIC)*nbc),0) AS stvarno_vpc,
                    sum((REPLACE(kolicina,',','.')::numeric - kolicina_koja_je_bila) * nbc) AS razlika_vpc,

                    COALESCE(SUM(kolicina_koja_je_bila*cijena),0) AS skladiste_mpc,
                    COALESCE(SUM(CAST(REPLACE(kolicina,',','.') AS NUMERIC)*cijena),0) AS stvarno_mpc,
                    COALESCE((SUM((CAST(REPLACE(kolicina,',','.') AS NUMERIC)*cijena)-(kolicina_koja_je_bila*cijena))),0) AS razlika_mpc
                    FROM inventura_stavke
                    WHERE broj_inventure='@broj';";
            }

            query = query.Replace("@broj", broj_dokumenta).Replace("+", "zbroj");
            DataTable DTinvSumirano = classSQL.select(query, "inv").Tables[0];

            #endregion OVDJE UZIMAM INVENTURNE VIŠKOVE I MANJKOVE I SPREMAM U DATASET ZA REPORT

            string year = "";
            DataTable DT = classSQL.select("SELECT datum FROM inventura WHERE broj_inventure='" + broj_dokumenta + "'", "inventura").Tables[0];

            if (DT.Rows.Count > 0)
            {
                year = Convert.ToDateTime(DT.Rows[0]["datum"].ToString()).Year.ToString();
            }
            string tbl3 = "MPC";
            if (nbc == "1")
            {
                tbl3 = "NBC";
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
                " WHERE inventura.broj_inventure ='" + broj_dokumenta + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste_string.Replace("nvarchar", "varchar")).Fill(dSRlisteTekst, "DTlisteTekst");
            }
        }

        private void SetVisakManjakStope()
        {
            string query = $@"SELECT kolicina,
                                    kolicina_koja_je_bila,
                                    cijena,
                                    porez
                                FROM inventura_stavke
                                WHERE broj_inventure = '{broj_dokumenta}' AND (CAST(REPLACE(kolicina,',','.') AS NUMERIC)-kolicina_koja_je_bila) <> 0";
            DataTable DTstavke = classSQL.select(query, "inventura_stavke").Tables[0];

            foreach (DataRow row in DTstavke.Rows)
            {
                decimal.TryParse(row["kolicina"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal kolicina);
                decimal.TryParse(row["kolicina_koja_je_bila"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal prijasnjaKolicina);
                decimal.TryParse(row["cijena"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal cijena);
                decimal.TryParse(row["porez"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal porez);

                decimal vpc = (kolicina - prijasnjaKolicina) * (cijena / (1 + (porez / 100)));
                decimal ukupnoCijena = (kolicina - prijasnjaKolicina) * cijena;

                DataRow newRow = dSstope.Tables[2].NewRow();
                newRow["stopa"] = porez;
                newRow["osnovica"] = vpc;
                newRow["iznos"] = ukupnoCijena * (porez / 100);
                newRow["vrsta"] = (kolicina - prijasnjaKolicina) < 0 ? "manjak" : "visak";
                dSstope.Tables[2].Rows.Add(newRow);
            }
        }
    }
}