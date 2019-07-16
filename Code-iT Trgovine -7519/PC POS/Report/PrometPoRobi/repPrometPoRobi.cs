using System;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Report.PrometPoRobi
{
    public partial class repPrometPoRobi : Form
    {
        public repPrometPoRobi()
        {
            InitializeComponent();
        }

        public string artikl { get; set; }
        public string godina { get; set; }
        public string broj_dokumenta { get; set; }
        public string dokumenat { get; set; }
        public string ImeForme { get; set; }
        public string blagajnik { get; set; }
        public string skladiste { get; set; }
        public string ducan { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }

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
            if (dokumenat == "PrometPoRobi")
            {
                PrometPoRobi();
                this.Text = ImeForme;
            }
            //Ne koristi se nigdje zasad!
            else if (dokumenat == "PrometPoRobiBezSkladista")
            {
                PrometPoRobiBezSkladista();
                this.Text = ImeForme;
            }

            this.reportViewer1.RefreshReport();
        }

        private void PrometPoRobi()
        {
            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");

            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string skl = "";
            if (skladiste != null)
            {
                skl = " AND racun_stavke.id_skladiste='" + skladiste + "'";
            }

            string duc = "";
            if (ducan != null)
            {
                duc = " AND racuni.id_ducan='" + ducan + "'";
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

            string Fskl = "";
            if (skladiste != null)
            {
                Fskl = " AND faktura_stavke.id_skladiste='" + skladiste + "'";
            }

            string Fblag = "";
            if (blagajnik != null)
            {
                Fblag = " AND fakture.id_zaposlenik='" + blagajnik + "'";
            }

            string Fart = "";
            if (artikl != null)
            {
                Fart = " AND faktura_stavke.sifra='" + artikl + "'";
            }

            string sql_liste = "SELECT " +
                " racun_stavke.sifra_robe AS sifra," +
                " roba.naziv AS naziv ," +
                " CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC) AS cijena1," +
                " 'Račun' AS jmj," +
                //" roba_prodaja.kolicina AS cijena3 ," +
                " racun_stavke.rabat AS cijena3 ," +
                " racun_stavke.broj_racuna AS cijena5 ," +
                " racun_stavke.vpc AS cijena6 ," +
                " Round(CAST(racun_stavke.mpc AS numeric), 2) * (1 - CAST(REPLACE(racun_stavke.rabat,',','.') AS NUMERIC) / 100) AS cijena7," +
                " racun_stavke.nbc AS cijena8 ," +
                " Round(CAST(racun_stavke.mpc AS numeric), 2) * (CAST(REPLACE(racun_stavke.rabat,',','.') AS NUMERIC) / 100) AS cijena9," +
                //" (CAST(racun_stavke.vpc AS NUMERIC)*(CAST(REPLACE(racun_stavke.porez,',','.') AS NUMERIC)zbrojCAST(REPLACE(racun_stavke.porez_potrosnja,',','.') AS NUMERIC))/100)zbrojCAST(racun_stavke.vpc AS NUMERIC) AS cijena5," +
                " skladiste.skladiste AS cijena4 " +
                //" (CAST(racun_stavke.vpc AS NUMERIC)*(CAST(REPLACE(racun_stavke.porez,',','.') AS NUMERIC)zbrojCAST(REPLACE(racun_stavke.porez_potrosnja,',','.') AS NUMERIC))/100)zbrojCAST(racun_stavke.vpc AS NUMERIC)*CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC) AS cijena6 " +
                " FROM racun_stavke" +
                " LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe" +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=racun_stavke.id_skladiste" +
                " LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_kasa=racun_stavke.id_kasa AND racuni.id_ducan=racun_stavke.id_ducan" +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=racun_stavke.sifra_robe AND roba_prodaja.id_skladiste=racun_stavke.id_skladiste " +
                " WHERE  racuni.datum_racuna>'" + datumOD + "' AND racuni.datum_racuna<'" + datumDO + "'" +
                " " + skl + blag + duc + art +
                "";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            string sql_fak = "SELECT " +
                " faktura_stavke.sifra AS sifra," +
                " roba.naziv AS naziv ," +
                " CAST(REPLACE(faktura_stavke.kolicina,',','.') AS NUMERIC) AS cijena1," +
                " 'Faktura' AS jmj," +
                //" roba_prodaja.kolicina AS cijena3 ," +
                " faktura_stavke.rabat AS cijena3 ," +
                " faktura_stavke.nbc AS cijena8 ," +
                " faktura_stavke.broj_fakture AS cijena5 ," +
                " faktura_stavke.vpc AS cijena6 ," +
                " ((CAST(faktura_stavke.vpc AS NUMERIC)*(CAST(REPLACE(faktura_stavke.porez,',','.') AS NUMERIC)/100))zbrojCAST(faktura_stavke.vpc AS NUMERIC)) AS cijena7, " +
                " skladiste.skladiste AS cijena4 " +
                //" (CAST(faktura_stavke.vpc AS NUMERIC)*(CAST(REPLACE(faktura_stavke.porez,',','.') AS NUMERIC)zbrojCAST(REPLACE(faktura_stavke.porez_potrosnja,',','.') AS NUMERIC))/100)zbrojCAST(faktura_stavke.vpc AS NUMERIC)*CAST(REPLACE(faktura_stavke.kolicina,',','.') AS NUMERIC) AS cijena6 " +
                " FROM faktura_stavke" +
                " LEFT JOIN roba ON roba.sifra=faktura_stavke.sifra" +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=faktura_stavke.id_skladiste" +
                " LEFT JOIN fakture ON fakture.broj_fakture=faktura_stavke.broj_fakture" +
                " AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa " +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=faktura_stavke.sifra AND roba_prodaja.id_skladiste=faktura_stavke.id_skladiste " +
                " WHERE  fakture.date>'" + datumOD + "' AND fakture.date<'" + datumDO + "'" +
                " " + Fskl + Fblag + Fart;

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_fak).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_fak).Fill(dSRliste, "DTliste");
            }

            string year = "";
            //DataTable DT = classSQL.select("SELECT godina_normativa FROM normativi WHERE broj_normativa='" + broj_dokumenta + "'", "normativi").Tables[0];

            //if (DT.Rows.Count > 0)
            // {
            //year = DT.Rows[0]["godina_normativa"].ToString();
            // }

            string s = "";
            if (skladiste != null)
            {
                s = "\r\nSkladište: " + skladiste;
            }

            string b = "";
            if (blagajnik != null)
            {
                b = "\r\nBlagajnik: " + blagajnik;
            }

            string a = "";
            if (artikl != null)
            {
                a = "\r\nArtikl: " + artikl;
            }

            string sql_liste_string = "SELECT " +
                " 'Šifra' AS tbl1," +
                " 'Naziv' AS tbl2," +
                " 'Dokument' AS tbl3," +
                " 'BR.Dok.' AS tbl4," +
                " 'Skladište' AS tbl5," +
                " 'Količina' AS tbl6," +
                " 'Rabat %' AS tbl7," +
                " 'VPC' AS tbl8," +
                " 'ne' AS string6," +
                " '" + s + "' AS skladiste," +
                " '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "' AS datum1," +
                " CAST ('Promet po robi  ' AS nvarchar) AS naslov," +
                " '\r\nOd datuma:" + datumOD + " - " + datumDO + s + b + a + "' AS komentar" +
                "";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste_string.Replace("nvarchar", "varchar")).Fill(dSRlisteTekst, "DTlisteTekst");
            }
        }

        private void PrometPoRobiBezSkladista()
        {
            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");

            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string skl = "";
            if (skladiste != null)
            {
                skl = " AND racun_stavke.id_skladiste='" + skladiste + "'";
            }

            string duc = "";
            if (ducan != null)
            {
                duc = " AND racuni.id_ducan='" + ducan + "'";
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

            string Fskl = "";
            if (skladiste != null)
            {
                Fskl = " AND faktura_stavke.id_skladiste='" + skladiste + "'";
            }

            string Fblag = "";
            if (blagajnik != null)
            {
                Fblag = " AND fakture.id_zaposlenik='" + blagajnik + "'";
            }

            string Fart = "";
            if (artikl != null)
            {
                Fart = " AND faktura_stavke.sifra='" + artikl + "'";
            }

            string sql_liste = "SELECT " +
                " racun_stavke.sifra_robe AS sifra," +
                " roba.naziv AS naziv ," +
                " CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC) AS cijena1," +
                " 'Račun' AS jmj," +
                " roba_prodaja.kolicina AS cijena3 ," +
                " racun_stavke.broj_racuna AS cijena5 ," +
                " racun_stavke.vpc AS cijena6 ," +
                //" (CAST(racun_stavke.vpc AS NUMERIC)*(CAST(REPLACE(racun_stavke.porez,',','.') AS NUMERIC)zbrojCAST(REPLACE(racun_stavke.porez_potrosnja,',','.') AS NUMERIC))/100)zbrojCAST(racun_stavke.vpc AS NUMERIC) AS cijena5," +
                " skladiste.skladiste AS cijena4 " +
                //" (CAST(racun_stavke.vpc AS NUMERIC)*(CAST(REPLACE(racun_stavke.porez,',','.') AS NUMERIC)zbrojCAST(REPLACE(racun_stavke.porez_potrosnja,',','.') AS NUMERIC))/100)zbrojCAST(racun_stavke.vpc AS NUMERIC)*CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC) AS cijena6 " +
                " FROM racun_stavke" +
                " LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe" +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=racun_stavke.id_skladiste" +
                " LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna" +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=racun_stavke.sifra_robe AND roba_prodaja.id_skladiste=racun_stavke.id_skladiste " +
                " WHERE  racuni.datum_racuna>'" + datumOD + "' AND racuni.datum_racuna<'" + datumDO + "'" +
                " " + skl + blag + duc + art +
                "";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            string sql_fak = "SELECT " +
                " faktura_stavke.sifra AS sifra," +
                " roba.naziv AS naziv ," +
                " CAST(REPLACE(faktura_stavke.kolicina,',','.') AS NUMERIC) AS cijena1," +
                " 'Faktura' AS jmj," +
                " roba_prodaja.kolicina AS cijena3 ," +
                " faktura_stavke.broj_fakture AS cijena5 ," +
                " faktura_stavke.vpc AS cijena6 ," +
                //" (CAST(faktura_stavke.vpc AS NUMERIC)*(CAST(REPLACE(faktura_stavke.porez,',','.') AS NUMERIC)zbrojCAST(REPLACE(faktura_stavke.porez_potrosnja,',','.') AS NUMERIC))/100)zbrojCAST(faktura_stavke.vpc AS NUMERIC) AS cijena5," +
                " skladiste.skladiste AS cijena4 " +
                //" (CAST(faktura_stavke.vpc AS NUMERIC)*(CAST(REPLACE(faktura_stavke.porez,',','.') AS NUMERIC)zbrojCAST(REPLACE(faktura_stavke.porez_potrosnja,',','.') AS NUMERIC))/100)zbrojCAST(faktura_stavke.vpc AS NUMERIC)*CAST(REPLACE(faktura_stavke.kolicina,',','.') AS NUMERIC) AS cijena6 " +
                " FROM faktura_stavke" +
                " LEFT JOIN roba ON roba.sifra=faktura_stavke.sifra" +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=faktura_stavke.id_skladiste" +
                " LEFT JOIN fakture ON fakture.broj_fakture=faktura_stavke.broj_fakture" +
                " AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa " +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=faktura_stavke.sifra AND roba_prodaja.id_skladiste=faktura_stavke.id_skladiste " +
                " WHERE  fakture.date>'" + datumOD + "' AND fakture.date<'" + datumDO + "'" +
                " " + Fskl + Fblag + Fart;

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_fak).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_fak).Fill(dSRliste, "DTliste");
            }

            string year = "";
            //DataTable DT = classSQL.select("SELECT godina_normativa FROM normativi WHERE broj_normativa='" + broj_dokumenta + "'", "normativi").Tables[0];

            //if (DT.Rows.Count > 0)
            // {
            //year = DT.Rows[0]["godina_normativa"].ToString();
            // }

            string s = "";
            if (skladiste != null)
            {
                s = "\r\nSkladište: " + skladiste;
            }

            string b = "";
            if (blagajnik != null)
            {
                b = "\r\nBlagajnik: " + blagajnik;
            }

            string a = "";
            if (artikl != null)
            {
                a = "\r\nArtikl: " + artikl;
            }

            string sql_liste_string = "SELECT " +
                " 'Šifra' AS tbl1," +
                " 'Naziv' AS tbl2," +
                " 'Dokument' AS tbl3," +
                " 'Količina' AS tbl4," +
                " 'Stanje SK' AS tbl5," +
                " 'Skladište' AS tbl6," +
                " 'BR.Dok.' AS tbl7," +
                " 'VPC' AS tbl8," +
                " 'ne' AS string6," +
                " '" + s + "' AS skladiste," +
                " '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "' AS datum1," +
                " CAST ('Promet po robi  ' AS nvarchar) AS naslov," +
                " '\r\nOd datuma:" + datumOD + " - " + datumDO + s + b + a + "' AS komentar" +
                "";

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