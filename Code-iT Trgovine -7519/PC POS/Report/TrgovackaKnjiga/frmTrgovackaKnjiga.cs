using System;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Report.TrgovackaKnjiga
{
    public partial class frmTrgovackaKnjiga : Form
    {
        public frmTrgovackaKnjiga()
        {
            InitializeComponent();
        }

        public string artikl { get; set; }
        public string godina { get; set; }
        public string broj_dokumenta { get; set; }
        public string dokument { get; set; }
        public string ImeForme { get; set; }
        public string blagajnik { get; set; }
        public string skladiste { get; set; }
        public string ducan { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }
        public bool sa_rabatom { get; set; }

        private void frmListe_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

            //dokumenat = "PrometPoRobi";
            //broj_dokumenta = "2";
            //skladiste = "2";
            //datumOD = DateTime.Now.AddDays(-2);
            //datumDO = DateTime.Now.AddDays(+10);

            this.Text = "Knjiga Ulaza i Izlaza";

            this.KnjigaIO();

            this.reportViewer1.RefreshReport();
        }

        private void KnjigaIO()
        {
            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");

            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string sql_liste;

            if (sa_rabatom)
            {
                sql_liste = "SELECT (CURRENT_DATE zbroj i) AS datum1, " +
                "'Račun' AS naziv, " +
                "(SELECT SUM(racun_stavke.vpc*CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric)*(1-CAST(REPLACE(racun_stavke.rabat,',','.') AS numeric)/100)*(1 zbroj CAST(REPLACE(racun_stavke.porez,',','.') AS numeric)/100)) FROM racun_stavke LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna WHERE CAST(racuni.datum_racuna as date) = (CURRENT_DATE zbroj i)) AS cijena1, " +
                "(SELECT SUM(racun_stavke.vpc*CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric)*(1-CAST(REPLACE(racun_stavke.rabat,',','.') AS numeric)/100)) FROM racun_stavke LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna WHERE CAST(racuni.datum_racuna as date) = (CURRENT_DATE zbroj i)) AS cijena2, " +
                "(SELECT SUM(racun_stavke.vpc*CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric)*(CAST(REPLACE(racun_stavke.rabat,',','.') AS numeric)/100)*(1 zbroj CAST(REPLACE(racun_stavke.porez,',','.') AS numeric)/100)) FROM racun_stavke LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna WHERE CAST(racuni.datum_racuna as date) = (CURRENT_DATE zbroj i)) AS rabat1, " +
                "(SELECT SUM(racun_stavke.vpc*CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric)*(CAST(REPLACE(racun_stavke.rabat,',','.') AS numeric)/100)) FROM racun_stavke LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna WHERE CAST(racuni.datum_racuna as date) = (CURRENT_DATE zbroj i)) AS rabat2, " +
                "0.0 AS cijena3, " +
                "'Faktura' AS naziv2, " +
                "(SELECT SUM(faktura_stavke.vpc*CAST(REPLACE(faktura_stavke.kolicina,',','.') AS numeric)*(1-CAST(REPLACE(faktura_stavke.rabat,',','.') AS numeric)/100)*(1 zbroj CAST(REPLACE(faktura_stavke.porez,',','.') AS numeric)/100)) FROM faktura_stavke LEFT JOIN fakture ON fakture.broj_fakture=faktura_stavke.broj_fakture  AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa  WHERE CAST(fakture.date as date) = (CURRENT_DATE zbroj i)) AS cijena5, " +
                "(SELECT SUM(faktura_stavke.vpc*CAST(REPLACE(faktura_stavke.kolicina,',','.') AS numeric)*(1-CAST(REPLACE(faktura_stavke.rabat,',','.') AS numeric)/100)) FROM faktura_stavke LEFT JOIN fakture ON fakture.broj_fakture=faktura_stavke.broj_fakture  AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa WHERE CAST(fakture.date as date) = (CURRENT_DATE zbroj i)) AS cijena6, " +
                "(SELECT SUM(faktura_stavke.vpc*CAST(REPLACE(faktura_stavke.kolicina,',','.') AS numeric)*(CAST(REPLACE(faktura_stavke.rabat,',','.') AS numeric)/100)*(1 zbroj CAST(REPLACE(faktura_stavke.porez,',','.') AS numeric)/100)) FROM faktura_stavke LEFT JOIN fakture ON fakture.broj_fakture=faktura_stavke.broj_fakture  AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa WHERE CAST(fakture.date as date) = (CURRENT_DATE zbroj i)) AS rabat3, " +
                "(SELECT SUM(faktura_stavke.vpc*CAST(REPLACE(faktura_stavke.kolicina,',','.') AS numeric)*(CAST(REPLACE(faktura_stavke.rabat,',','.') AS numeric)/100)) FROM faktura_stavke LEFT JOIN fakture ON fakture.broj_fakture=faktura_stavke.broj_fakture  AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa WHERE CAST(fakture.date as date) = (CURRENT_DATE zbroj i)) AS rabat4, " +
                "0.0 AS cijena7, " +
                " 'Kalkulacija' AS cijena4, " +
                "CAST((SELECT SUM(kalkulacija.ukupno_mpc) FROM kalkulacija WHERE CAST(kalkulacija.datum as date) = (CURRENT_DATE zbroj i)) AS NUMERIC)AS cijena8, " +
                "CAST((SELECT SUM(kalkulacija.ukupno_vpc) FROM kalkulacija WHERE CAST(kalkulacija.datum as date) = (CURRENT_DATE zbroj i)) AS NUMERIC)AS cijena9, " +
                "0.0 AS cijena10, " +
                "'Faktura IFB' AS naziv3,  " +
                "(SELECT SUM(ifb_stavke.vpc*ifb_stavke.kolicina*(1-ifb_stavke.rabat/100)*(1 zbroj ifb_stavke.porez/100)) FROM ifb_stavke LEFT JOIN ifb ON ifb.broj=ifb_stavke.broj WHERE CAST(ifb.datum as date) = (CURRENT_DATE zbroj i)) AS cijena11,  " +
                "(SELECT SUM(ifb_stavke.vpc*ifb_stavke.kolicina*(1-ifb_stavke.rabat/100)) FROM ifb_stavke LEFT JOIN ifb ON ifb.broj=ifb_stavke.broj WHERE CAST(ifb.datum as date) = (CURRENT_DATE zbroj i)) AS cijena12,  " +
                "(SELECT SUM((ifb_stavke.vpc*ifb_stavke.kolicina*ifb_stavke.rabat/100)*(1 zbroj ifb_stavke.porez/100)) FROM ifb_stavke LEFT JOIN ifb ON ifb.broj=ifb_stavke.broj WHERE CAST(ifb.datum as date) = (CURRENT_DATE zbroj i)) AS cijena13,  " +
                "(SELECT SUM((ifb_stavke.vpc*ifb_stavke.kolicina*ifb_stavke.rabat/100)) FROM ifb_stavke LEFT JOIN ifb ON ifb.broj=ifb_stavke.broj WHERE CAST(ifb.datum as date) = (CURRENT_DATE zbroj i)) AS cijena14, 0.0 AS cijena14, 0.0 AS cijena15 " +
                "FROM generate_series(date '" + datumOD.ToString("yyyy-MM-dd") + "'- CURRENT_DATE, " +
                "date '" + datumDO.ToString("yyyy-MM-dd") + "' - CURRENT_DATE ) i ";
            }
            else
            {
                sql_liste = "SELECT (CURRENT_DATE zbroj i) AS datum1, " +
                  "'Račun' AS naziv, " +
                  "(SELECT SUM(racun_stavke.vpc*CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric)*(1 zbroj CAST(REPLACE(racun_stavke.porez,',','.') AS numeric)/100)) FROM racun_stavke LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna WHERE CAST(racuni.datum_racuna as date) = (CURRENT_DATE zbroj i)) AS cijena1, " +
                  "(SELECT SUM(racun_stavke.vpc*CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric)) FROM racun_stavke LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna WHERE CAST(racuni.datum_racuna as date) = (CURRENT_DATE zbroj i)) AS cijena2, " +
                  "(SELECT SUM(racun_stavke.vpc*CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric)*(1 zbroj CAST(REPLACE(racun_stavke.porez,',','.') AS numeric)/100)) FROM racun_stavke LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna WHERE CAST(racuni.datum_racuna as date) = (CURRENT_DATE zbroj i)) AS rabat1, " +
                  "(SELECT SUM(racun_stavke.vpc*CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric)) FROM racun_stavke LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna WHERE CAST(racuni.datum_racuna as date) = (CURRENT_DATE zbroj i)) AS rabat2, " +
                  "0.0 AS cijena3, " +
                  "'Faktura' AS naziv2, " +
                  "(SELECT SUM(faktura_stavke.vpc*CAST(REPLACE(faktura_stavke.kolicina,',','.') AS numeric)*(1 zbroj CAST(REPLACE(faktura_stavke.porez,',','.') AS numeric)/100)) FROM faktura_stavke LEFT JOIN fakture ON fakture.broj_fakture=faktura_stavke.broj_fakture AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa WHERE CAST(fakture.date as date) = (CURRENT_DATE zbroj i)) AS cijena5, " +
                  "(SELECT SUM(faktura_stavke.vpc*CAST(REPLACE(faktura_stavke.kolicina,',','.') AS numeric)) FROM faktura_stavke LEFT JOIN fakture ON fakture.broj_fakture=faktura_stavke.broj_fakture AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa WHERE CAST(fakture.date as date) = (CURRENT_DATE zbroj i)) AS cijena6, " +
                  "(SELECT SUM(faktura_stavke.vpc*CAST(REPLACE(faktura_stavke.kolicina,',','.') AS numeric)*(1 zbroj CAST(REPLACE(faktura_stavke.porez,',','.') AS numeric)/100)) FROM faktura_stavke LEFT JOIN fakture ON fakture.broj_fakture=faktura_stavke.broj_fakture AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa WHERE CAST(fakture.date as date) = (CURRENT_DATE zbroj i)) AS rabat3, " +
                  "(SELECT SUM(faktura_stavke.vpc*CAST(REPLACE(faktura_stavke.kolicina,',','.') AS numeric)) FROM faktura_stavke LEFT JOIN fakture ON fakture.broj_fakture=faktura_stavke.broj_fakture AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa WHERE CAST(fakture.date as date) = (CURRENT_DATE zbroj i)) AS rabat4, " +
                  "0.0 AS cijena7, " +
                  " 'Kalkulacija' AS cijena4, " +
                  "CAST((SELECT SUM(kalkulacija.ukupno_mpc) FROM kalkulacija WHERE CAST(kalkulacija.datum as date) = (CURRENT_DATE zbroj i)) AS NUMERIC)AS cijena8, " +
                  "CAST((SELECT SUM(kalkulacija.ukupno_vpc) FROM kalkulacija WHERE CAST(kalkulacija.datum as date) = (CURRENT_DATE zbroj i)) AS NUMERIC)AS cijena9, " +
                  "0.0 AS cijena10, " +
                  "'Faktura IFB' AS naziv3,  " +
                  "(SELECT SUM(ifb_stavke.vpc*ifb_stavke.kolicina*(1 zbroj ifb_stavke.porez/100)) FROM ifb_stavke LEFT JOIN ifb ON ifb.broj=ifb_stavke.broj WHERE CAST(ifb.datum as date) = (CURRENT_DATE zbroj i)) AS cijena11,  " +
                  "(SELECT SUM(ifb_stavke.vpc*ifb_stavke.kolicina) FROM ifb_stavke LEFT JOIN ifb ON ifb.broj=ifb_stavke.broj WHERE CAST(ifb.datum as date) = (CURRENT_DATE zbroj i)) AS cijena12,  " +
                  "(SELECT SUM((ifb_stavke.vpc*ifb_stavke.kolicina)*(1 zbroj ifb_stavke.porez/100)) FROM ifb_stavke LEFT JOIN ifb ON ifb.broj=ifb_stavke.broj WHERE CAST(ifb.datum as date) = (CURRENT_DATE zbroj i)) AS cijena13,  " +
                  "(SELECT SUM(ifb_stavke.vpc*ifb_stavke.kolicina) FROM ifb_stavke LEFT JOIN ifb ON ifb.broj=ifb_stavke.broj WHERE CAST(ifb.datum as date) = (CURRENT_DATE zbroj i)) AS cijena14, 0.0 AS cijena14, 0.0 AS cijena15 " +
                  "FROM generate_series(date '" + datumOD.ToString("yyyy-MM-dd") + "'- CURRENT_DATE, " +
                  "date '" + datumDO.ToString("yyyy-MM-dd") + "' - CURRENT_DATE ) i ";
            }

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            string sql_liste_string = "SELECT " +
                " 'Datum' AS tbl1," +
                " 'Opis' AS tbl2," +
                " 'MPC razduženje' AS tbl3," +
                " 'VPC razduženje' AS tbl4," +
                " 'PDV razduženje' AS tbl5," +
                " 'MPC zaduženje' AS tbl6," +
                " 'VPC zaduženje' AS tbl7," +
                " 'PDV zaduženje' AS tbl8," +
                " 'Knjiga Ulaza i Izlaza' AS naslov";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste_string.Replace("nvarchar", "varchar")).Fill(dSRlisteTekst, "DTlisteTekst");
            }

            for (int i = 0; i < dSRliste.Tables[0].Rows.Count; i++)
            {
                decimal c1, c2, c5, c6, c8, c9, c11, c12, c13, c14;
                decimal.TryParse(dSRliste.Tables[0].Rows[i]["cijena1"].ToString(), out c1);
                decimal.TryParse(dSRliste.Tables[0].Rows[i]["cijena2"].ToString(), out c2);
                decimal.TryParse(dSRliste.Tables[0].Rows[i]["cijena5"].ToString(), out c5);
                decimal.TryParse(dSRliste.Tables[0].Rows[i]["cijena6"].ToString(), out c6);
                decimal.TryParse(dSRliste.Tables[0].Rows[i]["cijena8"].ToString(), out c8);
                decimal.TryParse(dSRliste.Tables[0].Rows[i]["cijena9"].ToString(), out c9);

                decimal.TryParse(dSRliste.Tables[0].Rows[i]["cijena11"].ToString(), out c11);
                decimal.TryParse(dSRliste.Tables[0].Rows[i]["cijena12"].ToString(), out c12);
                decimal.TryParse(dSRliste.Tables[0].Rows[i]["cijena13"].ToString(), out c13);

                dSRliste.Tables[0].Rows[i]["cijena1"] = c1;
                dSRliste.Tables[0].Rows[i]["cijena2"] = c2;
                dSRliste.Tables[0].Rows[i]["cijena3"] = c1 - c2;
                dSRliste.Tables[0].Rows[i]["cijena5"] = c5;
                dSRliste.Tables[0].Rows[i]["cijena6"] = c6;
                dSRliste.Tables[0].Rows[i]["cijena7"] = c5 - c6;
                dSRliste.Tables[0].Rows[i]["cijena8"] = c8;
                dSRliste.Tables[0].Rows[i]["cijena9"] = c9;
                dSRliste.Tables[0].Rows[i]["cijena10"] = c8 - c9;

                dSRliste.Tables[0].Rows[i]["cijena11"] = c11;
                dSRliste.Tables[0].Rows[i]["cijena12"] = c12;
                dSRliste.Tables[0].Rows[i]["cijena13"] = c13;
                dSRliste.Tables[0].Rows[i]["cijena14"] = c11 - c12;
            }
        }
    }
}