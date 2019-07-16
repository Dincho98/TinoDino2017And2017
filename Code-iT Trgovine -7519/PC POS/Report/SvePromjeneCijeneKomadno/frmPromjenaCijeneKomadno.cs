using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Report.SvePromjeneCijeneKomadno
{
    public partial class frmPromjenaCijeneKomadno : Form
    {
        public frmPromjenaCijeneKomadno()
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

            promjenaCijeneKomadno();
            this.Text = ImeForme;

            this.reportViewer1.RefreshReport();
        }

        private void promjenaCijeneKomadno()
        {
            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");

            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string sql_liste = "SELECT " +
                " promjena_cijene_komadno_stavke.sifra," +
                " roba.naziv," +
                " promjena_cijene_komadno_stavke.postotak AS jmj," +
                " promjena_cijene_komadno_stavke.stara_cijena AS cijena1 ," +
                " promjena_cijene_komadno_stavke.nova_cijena AS cijena3," +
                " promjena_cijene_komadno_stavke.pdv AS cijena4," +
                " promjena_cijene_komadno_stavke.kolicina  as cijena7, " +
                " promjena_cijene_komadno_stavke.skladiste as naziv2, " +
                " CAST(promjena_cijene_komadno_stavke.nova_cijena AS money) - CAST(promjena_cijene_komadno_stavke.stara_cijena AS money) AS cijena6," +
                " CAST(CAST(promjena_cijene_komadno_stavke.nova_cijena AS money) - CAST(promjena_cijene_komadno_stavke.stara_cijena AS money) AS money)" +
                "      -      " +
                " CAST((CAST(promjena_cijene_komadno_stavke.nova_cijena AS money) - CAST(promjena_cijene_komadno_stavke.stara_cijena AS money))" +
                "      /      " +
                " CAST('1.'+promjena_cijene_komadno_stavke.pdv AS numeric) AS money)  AS cijena5" +
                " FROM promjena_cijene_komadno_stavke" +
                " LEFT JOIN roba ON roba.sifra=promjena_cijene_komadno_stavke.sifra WHERE broj='" + broj_dokumenta + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            string year = "";
            DataTable DT = classSQL.select("SELECT date FROM promjena_cijene_komadno WHERE broj='" + broj_dokumenta + "'", "promjena_cijene_komadno").Tables[0];

            if (DT.Rows.Count > 0)
            {
                year = Convert.ToDateTime(DT.Rows[0]["date"].ToString()).Year.ToString();
            }

            string sql_liste_string = "SELECT " +
                " 'Šifra' AS tbl1," +
                " 'Naziv i skladište' AS tbl2," +
                " 'Postotak' AS tbl3," +
                " 'Stara cijena' AS tbl4," +
                " 'Nova cijena' AS tbl5," +
                " 'PDV' AS tbl6," +
                " 'PDV iznos' AS tbl7," +
                " 'Iznos' AS tbl8," +
                " 'Kol.' AS tbl9," +
                " 'Skladište.' AS string9," +
                " promjena_cijene_komadno.date AS datum1," +
                " promjena_cijene_komadno.napomena AS komentar," +
                " skladiste.skladiste AS skladiste," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS string1," +
                " CAST ('ZAPISNIK O PROMJENI CIJENE  ' AS nvarchar) + CAST (promjena_cijene_komadno.broj AS nvarchar) +'/'+ CAST (" + year + " AS nvarchar) AS naslov" +
                " FROM promjena_cijene_komadno " +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=promjena_cijene_komadno.id_skladiste " +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=promjena_cijene_komadno.id_izradio " +
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