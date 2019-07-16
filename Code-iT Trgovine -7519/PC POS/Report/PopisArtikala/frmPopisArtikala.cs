using System;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Report.Liste
{
    public partial class frmPopisArtikala : Form
    {
        public frmPopisArtikala()
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

            //Popunjava tablicu na reportu
            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");

            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
        }

        private void btnUcitaj_Click(object sender, EventArgs e)
        {
            string filter = "";
            if (chbRobno.Checked && !chbUsluge.Checked)
                filter = " AND oduzmi='DA' ";
            else if (chbUsluge.Checked && !chbRobno.Checked)
                filter = " AND oduzmi='NE' ";

            string sql_liste = @"SELECT sifra,naziv,CAST(mpc AS numeric) as cijena2,jm as jmj, vpc AS cijena1, porez FROM roba
                WHERE sifra not like '!%'" + filter + " ORDER BY naziv";

            dSRliste.Clear();
            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            string sql_liste_string = "SELECT " +
                " 'Šifra artikla' AS tbl1," +
                " 'Naziv artikla' AS tbl2," +
                " 'JMJ' AS tbl3," +
                " '' AS tbl4," +
                " '' AS tbl5," +
                " '' AS tbl6," +
                " 'Cijena bez PDV-a' AS tbl7," +
                " 'Maloprodajna cijena' AS tbl8," +
                " 'Cjenik artikala i usluga' AS naslov";

            dSRlisteTekst.Clear();
            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste_string.Replace("nvarchar", "varchar")).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            //Tablice

            this.reportViewer1.RefreshReport();
        }
    }
}