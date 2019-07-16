using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.PreuzeoRobu
{
    public partial class frmPreuzeoRobu : Form
    {
        public frmPreuzeoRobu()
        {
            InitializeComponent();
        }

        public string sort { get; set; }
        public string ime_partnera { get; set; }
        public string sifra_partnera { get; set; }
        public string sifra_preuzeo { get; set; }
        public string ducan { get; set; }
        public string kasa { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }

        private void frmListe_Load(object sender, EventArgs e)
        {
            RobuPreuzeo();
            this.reportViewer1.RefreshReport();
        }

        private void RobuPreuzeo()
        {
            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string filter = "";

            if (sifra_partnera != "")
                filter += " AND id_fakturirati='" + sifra_partnera.ToString() + "' ";

            if (sifra_preuzeo != "")
                filter += " AND id_odrediste='" + sifra_preuzeo.ToString() + "' ";

            filter += " AND fakture.date>='" + datumOD.ToString("yyyy-MM-dd H:mm:ss") +
                "' AND fakture.date<='" + datumDO.ToString("yyyy-MM-dd H:mm:ss") + "' ";

            if (sort == "F") sort = "ORDER BY CAST(fakture.broj_fakture AS INT) ASC";
            else sort = "ORDER BY p.ime_tvrtke ASC ,p.ime ASC";

            string sql = "SELECT p.ime_tvrtke AS naziv2,p.ime,p.prezime,fakture.ukupno_osnovica AS cijena1," +
                " fakture.ukupno_porez AS cijena2,fakture.ukupno AS cijena3, 'Faktura br: '+fakture.broj_fakture AS jmj, pf.ime_tvrtke AS naziv FROM fakture " +
                " LEFT JOIN partners p ON p.id_partner=fakture.id_odrediste " +
                " LEFT JOIN partners pf ON pf.id_partner=fakture.id_fakturirati " +
                " WHERE fakture.broj_fakture is not null " + filter + sort;

            classSQL.NpgAdatpter(sql).Fill(dSRliste, "DTliste");

            foreach (DataRow row in dSRliste.Tables[0].Rows)
            {
                if (row["naziv2"].ToString() == "")
                {
                    row["naziv2"] = row["ime"] + " " + row["prezime"];
                }
            }

            //------------------------------------OVAJ DIO RACUNA RABAT-------------------------------------------------------------------
            DataSet DS = new DataSet();

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter("").Fill(DS, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter("").Fill(DS, "DTliste");
            }

            string sql_liste_string = "SELECT 'Preuzeta roba za partnera: " + ime_partnera + "' AS naslov" +
                ",'Od datuma: " + datumOD.ToString("yyyy-MM-dd H:mm:ss") + " do datuma: " + datumDO.ToString("yyyy-MM-dd H:mm:ss") + " ' AS string1";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
        }
    }
}