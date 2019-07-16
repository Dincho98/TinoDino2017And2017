using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.PorezNaDohodak
{
    public partial class frmPorezNaDohodak : Form
    {
        public frmPorezNaDohodak()
        {
            InitializeComponent();
        }

        public string sort { get; set; }
        public string ime_partnera { get; set; }
        public string sifra_partnera { get; set; }
        public string ducan { get; set; }
        public string kasa { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }

        private void frmListe_Load(object sender, EventArgs e)
        {
            tdOdDatuma.Value = new DateTime(DateTime.Now.Year, 01, 01, 00, 00, 01);
            DataTable DTskl = classSQL.select("SELECT id_ducan,ime_ducana FROM ducan ORDER BY ime_ducana ASC", "skladiste").Tables[0];
            DataRow r = DTskl.NewRow();
            r[0] = "0";
            r[1] = "Svi poslovni prostori";
            DTskl.Rows.Add(r);
            cbDucan.DataSource = DTskl;
            cbDucan.DisplayMember = "ime_ducana";
            cbDucan.ValueMember = "id_ducan";
            cbDucan.SelectedValue = "0";

            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            dSRpodaciTvrtke.Tables[0].Rows[0]["naziv_fakture"] = "";
            this.reportViewer1.RefreshReport();
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            string uvijet_datum = "";
            string filterDucan = "";

            uvijet_datum = " AND racuni.datum_racuna>='" + tdOdDatuma.Value.ToString("yyyy-MM-dd H:mm:00") +
                "' AND racuni.datum_racuna<='" + tdDoDatuma.Value.ToString("yyyy-MM-dd H:mm:00") + "'";

            if (cbDucan.SelectedValue.ToString() != "0")
                filterDucan = " AND racuni.id_ducan='" + cbDucan.SelectedValue + "'";

            listaUniverzalna.Clear();

            string sql = @"DROP TABLE IF EXISTS racuni_sum;
                            CREATE TEMP TABLE racuni_sum AS
                            SELECT
                            CAST(racuni.broj_racuna as INT),
                            racuni.datum_racuna,
                            racuni.id_kasa,
                            racuni.id_ducan,
                            SUM((porez_na_dohodak_iznos)*(CAST(REPLACE(kolicina,',','.') as numeric))) AS porez_na_dohodakSUM,
                            SUM((prirez_iznos)*(CAST(REPLACE(kolicina,',','.') as numeric))) AS prirez_iznosSUM,
                            SUM((mpc::numeric)*(CAST(REPLACE(kolicina,',','.') as numeric))) AS ukupno,
                            racun_stavke.prirez,
                            racun_stavke.porez_na_dohodak
                            FROM racun_stavke
                            LEFT JOIN racuni ON racuni.broj_racuna = racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa
                            WHERE racuni.id_kupac<>'0' " + uvijet_datum + filterDucan + @"
                            GROUP BY racuni.broj_racuna,racuni.datum_racuna,racuni.id_kasa,racuni.id_ducan,racun_stavke.prirez,racun_stavke.porez_na_dohodak
                            ORDER BY CAST(racuni.broj_racuna as INT);

                            /*OVDJE KREIRAM UPIT IZ TEMP TABLE KOJI SAM IZNAD IZRADIO*/
                            SELECT
                            broj_racuna as string1,
                            datum_racuna as datum1,
                            id_kasa,
                            id_ducan,
                            ROUND(porez_na_dohodakSUM,3) as decimal2,
                            ROUND(prirez_iznosSUM,3) as decimal3,
                            ROUND(ukupno,3) as decimal1
                            FROM racuni_sum;";

            sql = sql.Replace("+", "zbroj");
            classSQL.NpgAdatpter(sql).Fill(listaUniverzalna, "DTListaUniverzalna");

            if (listaUniverzalna.Tables[0].Rows.Count > 0)
            {
                listaUniverzalna.Tables[0].Rows[0]["string8"] = "FILTER-> OD DATUMA: " + tdOdDatuma.Value.ToString("dd.MM.yyyy H:mm:ss") + "  DO DATUMA: " + tdDoDatuma.Value.ToString("dd.MM.yyyy H:mm:ss");
            }

            this.reportViewer1.RefreshReport();
        }
    }
}