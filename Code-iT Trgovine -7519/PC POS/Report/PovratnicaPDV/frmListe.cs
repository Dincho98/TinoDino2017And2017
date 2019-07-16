using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.PovratnicaPDV
{
    public partial class frmListe4 : Form
    {
        public frmListe4()
        {
            InitializeComponent();
        }

        public string documenat { get; set; }
        public string broj_dokumenta { get; set; }
        public string dokumenat { get; set; }
        public string ImeForme { get; set; }
        public string blagajnik { get; set; }
        public string skladiste { get; set; }
        public string ducan { get; set; }
        public string godina { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }

        public string BrojFakOD { get; set; }
        public string BrojFakDO { get; set; }

        private void frmListe_Load(object sender, EventArgs e)
        {
            if (documenat == "POVRATNICA")
            {
                povrat_robe();
            }

            if (documenat == "POVRATNICAPDV")
            {
                povrat_robepdv();
            }

            this.reportViewer1.RefreshReport();
        }

        private void povrat_robe()
        {
            string naziv_fakture = " podaci_tvrtka.naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql("", naziv_fakture, "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string sql_liste = "SELECT " +
                " povrat_robe_stavke.sifra + '   ' + roba.naziv As Naziv," +
                " roba.jm AS naziv2," +
                " povrat_robe_stavke.kolicina AS kolicina," +
                " povrat_robe_stavke.pdv AS cijena9," +
                " povrat_robe_stavke.rabat AS cijena1 ," +
                " povrat_robe_stavke.nbc AS cijena2," +
                " (CAST(povrat_robe_stavke.nbc AS numeric) * CAST(REPLACE(povrat_robe_stavke.kolicina,',','.') AS numeric)-" +
                " ((CAST(povrat_robe_stavke.nbc AS numeric) * CAST(REPLACE(povrat_robe_stavke.kolicina,',','.') AS numeric))*CAST(REPLACE(povrat_robe_stavke.rabat,',','.') AS numeric)/100)) AS cijena3 " +
                " FROM povrat_robe_stavke" +
                " LEFT JOIN roba ON roba.sifra=povrat_robe_stavke.sifra WHERE broj='" + broj_dokumenta + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            string year = "";
            DataTable DT = classSQL.select("SELECT datum FROM povrat_robe WHERE broj='" + broj_dokumenta + "'", "povrat_robe").Tables[0];

            if (DT.Rows.Count > 0)
            {
                year = Convert.ToDateTime(DT.Rows[0]["datum"].ToString()).Year.ToString();
            }

            string sql_liste_string = "SELECT " +
                " 'Količina' AS tbl1," +
                " 'Jmj' AS tbl2," +
                " 'Nab. cijena' AS tbl3," +
                " 'Rabat' AS tbl4," +
                " 'Nab. iznos' AS tbl5," +
                " 'tb' AS tbl6," +
                " 'rbr.' AS tbl8," +
                " 'Šifra i naziv' AS tbl9," +
                " povrat_robe.datum AS datum1," +
                " povrat_robe.orginalni_dokument AS string3 ," +
                " povrat_robe.napomena AS komentar," +
                 " '(' + partners.id_partner + ')' AS string9 ," +
                " partners.ime_tvrtke  AS string2 ," +
                " partners.adresa AS string3 ," +
                " grad.posta + ' ' + grad.grad AS string4 ," +
                " '" + godina + "' +'  / '+  '" + broj_dokumenta + "' As string7, " +
                " partners.oib AS string5," +
                " partners.id_partner AS string6," +
                " skladiste.id_skladiste + '  -  ' + skladiste.skladiste AS skladiste," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS string1," +
                " CAST ('POVRATNICA DOBAVLJAČU: ' AS nvarchar) + CAST (povrat_robe.broj AS nvarchar) +'/'+ CAST (" + year + " AS nvarchar) AS naslov" +
                " FROM povrat_robe " +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=povrat_robe.id_skladiste " +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=povrat_robe.id_izradio " +
                " LEFT JOIN partners ON partners.id_partner=povrat_robe.id_partner " +
                " LEFT JOIN grad ON grad.id_grad=partners.id_grad " +
                " WHERE broj ='" + broj_dokumenta + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste_string.Replace("nvarchar", "varchar")).Fill(dSRlisteTekst, "DTlisteTekst");
            }
        }

        private void povrat_robepdv()
        {
            string naziv_fakture = " podaci_tvrtka.naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql("", naziv_fakture, "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            DataTable DTnovo;

            string sql_liste = "SELECT " +
                " povrat_robe_stavke.sifra + '   ' + roba.naziv As Naziv," +
                " roba.jm AS naziv2," +
                " povrat_robe_stavke.kolicina AS kolicina," +
                " povrat_robe_stavke.pdv AS cijena9," +
                " povrat_robe_stavke.rabat AS cijena1 ," +
                " (CAST(povrat_robe_stavke.vpc AS numeric) * CAST(REPLACE(povrat_robe_stavke.kolicina,',','.') AS numeric)) AS cijena3," +
                " CAST(povrat_robe_stavke.vpc  AS numeric) AS cijena2," +
                 " (((CAST(povrat_robe_stavke.vpc AS numeric) * CAST(povrat_robe_stavke.pdv AS numeric))/100) zbroj CAST(povrat_robe_stavke.vpc  AS numeric))*CAST(REPLACE(povrat_robe_stavke.kolicina,',','.') AS numeric) AS cijena6" +
                " FROM povrat_robe_stavke" +
                " LEFT JOIN roba ON roba.sifra=povrat_robe_stavke.sifra WHERE broj='" + broj_dokumenta + "'";

            DTnovo = classSQL.select(sql_liste, "povrat_robe_stavke").Tables[0];

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            string year = "";
            DataTable DT = classSQL.select("SELECT datum FROM povrat_robe WHERE broj='" + broj_dokumenta + "'", "povrat_robe").Tables[0];

            if (DT.Rows.Count > 0)
            {
                year = Convert.ToDateTime(DT.Rows[0]["datum"].ToString()).Year.ToString();
            }
            string sql_liste_string = "SELECT " +
                " 'Količina' AS tbl1," +
                " 'Jmj' AS tbl2," +
                " 'Cijena' AS tbl3," +
                " 'Rabat izn.' AS tbl4," +
                " 'Iznos' AS tbl5," +
                " 'tb' AS tbl6," +
                " 'Iznos s por.' AS tbl7," +
                " 'rbr.' AS tbl8," +
                " 'Šifra i naziv' AS tbl9," +
                " povrat_robe.datum AS datum1," +
                " povrat_robe.mjesto_troska AS string8," +
                " povrat_robe.napomena AS komentar," +
                " povrat_robe.orginalni_dokument AS string3 ," +
                " '(' + partners.id_partner + ')' AS string9 ," +
                " '" + godina + "' +'  /  '+  '" + broj_dokumenta + "' As string7, " +
                " partners.ime_tvrtke  AS string2 ," +
                " partners.adresa AS string3 ," +
                " grad.posta + ' ' + grad.grad AS string4 ," +
                " partners.oib AS string5," +
                " partners.id_partner AS string6," +
                " skladiste.id_skladiste + '  -  ' + skladiste.skladiste AS skladiste," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS string1," +
                " CAST ('POVRATNICA DOBAVLJAČU: ' AS nvarchar) + CAST (povrat_robe.broj AS nvarchar) +'/'+ CAST (" + year + " AS nvarchar) AS naslov" +
                " FROM povrat_robe " +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=povrat_robe.id_skladiste " +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=povrat_robe.id_izradio " +
                " LEFT JOIN partners ON partners.id_partner=povrat_robe.id_partner " +
                " LEFT JOIN grad ON grad.id_grad=partners.id_grad " +
                " WHERE broj ='" + broj_dokumenta + "'";

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