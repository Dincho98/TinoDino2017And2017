using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmStavkeRadnogNaloga : Form
    {
        public frmStavkeRadnogNaloga()
        {
            InitializeComponent();
        }

        public frmRadniNalog MainForm { get; set; }
        public string sf_artikla { get; set; }
        public int broj_naloga { get; set; }

        private void frmStavkeRadnogNaloga_Load(object sender, EventArgs e)
        {
            string sql = "SELECT normativi_stavke.sifra_robe AS [Šifra artikla],roba.naziv AS [Naziv],normativi_stavke.kolicina as [Količina], " +
                " skladiste.skladiste as [Skladište] " +
                " FROM normativi_stavke INNER JOIN normativi ON normativi.broj_normativa=normativi_stavke.broj_normativa " +
                " INNER JOIN skladiste ON skladiste.id_skladiste=normativi_stavke.id_skladiste " +
                " INNER JOIN roba ON normativi_stavke.sifra_robe=roba.sifra " +
                " WHERE normativi.sifra_artikla ='" + sf_artikla + "'";

            dataGridView1.DataSource = classSQL.select(sql, "normativi_stavke").Tables[0];

            //sql = "SELECT rns.sifra_robe AS [Šifra robe], r.naziv AS [Naziv], rns.kolicina AS [Količina], s.skladiste AS [Skladište] " +
            //                "FROM radni_nalog_stavke rns " +
            //                "LEFT JOIN roba r on rns.sifra_robe = r.sifra " +
            //                "LEFT JOIN skladiste s on rns.id_skladiste = s.id_skladiste " +
            //                "WHERE rns.broj_naloga = '" + broj_naloga + "';";

            //dataGridView1.DataSource = classSQL.select(sql, "radni_nalog_stavke").Tables[0];

            zbroji_artikle();
        }

        private void zbroji_artikle()
        {
            decimal vpc = 0;
            decimal mpc = 0;
            decimal kol = 0;
            string sifra = "";
            for (int y = 0; y < dataGridView1.Rows.Count; y++)
            {
                sifra = dataGridView1.Rows[y].Cells["Šifra artikla"].FormattedValue.ToString();
                kol = Convert.ToDecimal(dataGridView1.Rows[y].Cells["Količina"].FormattedValue.ToString());
                string sql = "SELECT vpc,mpc FROM roba WHERE sifra='" + sifra + "'";
                DataTable DTcijene = classSQL.select(sql, "roba").Tables[0];
                decimal vpc_t = Convert.ToDecimal(DTcijene.Rows[0]["vpc"].ToString());
                decimal mpc_t = Convert.ToDecimal(DTcijene.Rows[0]["mpc"].ToString());
                vpc += vpc_t * kol;
                mpc += mpc_t * kol;
            }

            txtvpcnormativ.Text = vpc.ToString();
            txtmpcnormativ.Text = mpc.ToString();
        }
    }
}