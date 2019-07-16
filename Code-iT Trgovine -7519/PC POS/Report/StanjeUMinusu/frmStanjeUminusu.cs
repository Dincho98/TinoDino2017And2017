using System;
using System.Windows.Forms;

namespace PCPOS.Report.StanjeUMinusu
{
    public partial class frmStanjeUminusu : Form
    {
        public frmStanjeUminusu()
        {
            InitializeComponent();
        }

        public int id_skladiste { get; set; }

        private void frmStanjeUminusu_Load(object sender, EventArgs e)
        {
            string sql = "SELECT roba_prodaja.sifra, roba.naziv, CAST(REPLACE(roba_prodaja.kolicina,',','.') AS numeric) as kolicina, skladiste.skladiste, skladiste.id_skladiste, row_number() over() as br FROM roba_prodaja LEFT JOIN roba ON roba.sifra=roba_prodaja.sifra LEFT JOIN skladiste ON roba_prodaja.id_skladiste=skladiste.id_skladiste WHERE CAST(REPLACE(roba_prodaja.kolicina,',','.') AS numeric)<0 AND roba.oduzmi='DA'";

            if (id_skladiste > -1)
            {
                sql += " AND roba_prodaja.id_skladiste = " + id_skladiste;
            }

            sql += ";";

            //dsStanjeUMinusu.Tables.Add(dtStanjeUMinus);
            //dsStanjeUMinusu.Tables[0]. = dtStanjeUMinus;

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql).Fill(dsStanjeUMinusu, "dtStanjeUMinusu");
            }
            else
            {
                classSQL.NpgAdatpter(sql).Fill(dsStanjeUMinusu, "dtStanjeUMinusu");
            }

            this.reportViewer1.RefreshReport();
        }

        private void dsStanjeUMinusuBindingSource_CurrentChanged(object sender, EventArgs e)
        {
        }
    }
}