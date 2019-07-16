using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.PovratnicaPDV
{
    public partial class frmOtpis : Form
    {
        public frmOtpis()
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

        private void frmOtpis_Load(object sender, EventArgs e)
        {
            otpis();
            this.reportViewer1.RefreshReport();
        }

        private void otpis()
        {
            string naziv_fakture = " podaci_tvrtka.naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql("", naziv_fakture, "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string sql_liste = "SELECT " +
                " CONCAT(otpis_robe_stavke.sifra, ' ',roba.naziv) AS naziv," +
                " roba.jm AS naziv2," +
                " otpis_robe_stavke.kolicina AS kolicina," +
                " (otpis_robe_stavke.vpc*replace(otpis_robe_stavke.kolicina, ',','.')::numeric) AS cijena7," +
                " otpis_robe_stavke.pdv AS cijena9," +
                " otpis_robe_stavke.rabat AS cijena1 ," +
                " otpis_robe_stavke.nbc AS cijena3," +
                " (((CAST(otpis_robe_stavke.vpc AS numeric) * CAST(otpis_robe_stavke.pdv AS numeric))/100) zbroj CAST(otpis_robe_stavke.vpc AS numeric))* CAST(otpis_robe_stavke.kolicina AS numeric) AS cijena6," +
                " CAST(otpis_robe_stavke.nbc AS numeric) * replace(otpis_robe_stavke.kolicina, ',','.')::numeric AS cijena2" +
                " FROM otpis_robe_stavke" +
                " LEFT JOIN roba ON roba.sifra=otpis_robe_stavke.sifra WHERE broj='" + broj_dokumenta + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            string year = "";
            DataTable DT = classSQL.select("SELECT datum FROM otpis_robe WHERE broj='" + broj_dokumenta + "'", "otpis_robe").Tables[0];

            if (DT.Rows.Count > 0)
            {
                year = Convert.ToDateTime(DT.Rows[0]["datum"].ToString()).Year.ToString();
            }

            string sql_liste_string = "SELECT " +
                " 'Količina' AS tbl1," +
                " 'Jmj' AS tbl2," +
                " 'Nab. cijena' AS tbl3," +
                " 'Rabat izn.' AS tbl4," +
                " 'Nab. iznos' AS tbl5," +
                " 'tb' AS tbl6," +
                " 'Mpc uk.' AS tbl7," +
                " 'rbr.' AS tbl8," +
                " 'Šifra i naziv' AS tbl9, " +
                " otpis_robe.napomena AS komentar," +
                " CAST (otpis_robe.broj AS nvarchar) +'/'+ CAST (" + year + " AS nvarchar) AS naslov" +
                " FROM otpis_robe " +
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