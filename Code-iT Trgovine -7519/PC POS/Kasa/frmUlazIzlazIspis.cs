using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PCPOS.Kasa
{
    public partial class frmUlazIzlazIspis : Form
    {
        public frmUlazIzlazIspis()
        {
            InitializeComponent();
        }

        public DateTime OD_datuma { get; set; }
        public DateTime DO_datuma { get; set; }
        public string cbZaposlenik { get; set; }
        public string ZaposlenikNaziv { get; set; }
        public string cbDucan { get; set; }
        public string PoslovnicaNaziv { get; set; }

        private void frmUlazIzlazIspis_Load(object sender, EventArgs e)
        {
            PostaviDgv();
            PostaviUvjete();
            PostaviChartPoMjesecima();
            dgv.ClearSelection();
        }

        private void PostaviUvjete()
        {
            lblUvijeti.Text = "OD datuma: " + OD_datuma.ToString("dd-MM-yyyy H:mm:ss") + "  ";
            lblUvijeti.Text += "DO datuma: " + DO_datuma.ToString("dd-MM-yyyy H:mm:ss") + "  ";
            if (cbDucan != null) { lblUvijeti.Text += " Poslovnica: " + PoslovnicaNaziv + "  "; }
            if (ZaposlenikNaziv != null) { lblUvijeti.Text += "Zaposlenik: " + ZaposlenikNaziv + "  "; }
        }

        private void PostaviChartPoMjesecima()
        {
            string racuni = "", fakture = "";
            if (cbZaposlenik != null)
            {
                racuni += " AND racuni.id_blagajnik='" + cbZaposlenik + "'";
                fakture += " AND fakture.id_zaposlenik='" + cbZaposlenik + "'";
            }
            if (cbDucan != null)
            {
                racuni += " AND racuni.id_ducan='" + cbDucan + "'";
                fakture += " AND fakture.id_ducan='" + cbDucan + "'";
            }

            string sql = "SELECT " +
                         "   ts, " +
                         "   EXTRACT(MONTH FROM ts), " +
                         "   (COALESCE((SELECT (COALESCE(SUM(((CAST(racun_stavke.mpc as NUMERIC)-(CAST(racun_stavke.mpc as NUMERIC)*CAST(REPLACE(rabat,',','.') as NUMERIC)/100))/(1zbroj(CAST(REPLACE(racun_stavke.porez,',','.') as NUMERIC)/100)))  * CAST(REPLACE(racun_stavke.kolicina,',','.') as NUMERIC)),0)-COALESCE(SUM(CAST(racun_stavke.nbc as NUMERIC)*CAST(REPLACE(racun_stavke.kolicina,',','.') as NUMERIC)),0)) AS Nabavna FROM racun_stavke LEFT JOIN racuni ON racuni.broj_racuna = racun_stavke.broj_racuna AND racun_stavke.id_ducan=racuni.id_ducan AND racun_stavke.id_kasa=racuni.id_kasa LEFT JOIN roba ON roba.sifra = racun_stavke.sifra_robe WHERE roba.oduzmi='DA' " + racuni + "   AND datum_racuna BETWEEN ts AND (ts  zbroj '1month'::interval)::timestamp),0) zbroj " +
                         "   COALESCE((SELECT SUM((faktura_stavke.vpc- (faktura_stavke.vpc*(CAST(REPLACE(rabat,',','.') as NUMERIC)/100))) *  CAST(REPLACE(faktura_stavke.kolicina,',','.') as NUMERIC)) -  SUM((CAST(faktura_stavke.nbc as NUMERIC)*CAST(REPLACE(faktura_stavke.kolicina,',','.') as NUMERIC)))  FROM faktura_stavke  LEFT JOIN fakture ON fakture.broj_fakture = faktura_stavke.broj_fakture  AND faktura_stavke.id_ducan=fakture.id_ducan AND faktura_stavke.id_kasa=fakture.id_kasa  LEFT JOIN roba ON roba.sifra = faktura_stavke.sifra   WHERE roba.oduzmi='DA' " + fakture + "  AND fakture.date BETWEEN ts AND (ts  zbroj '1month'::interval)::timestamp),0)) AS roba, " +

                         "   (COALESCE((SELECT (COALESCE(SUM(((CAST(racun_stavke.mpc as NUMERIC)-(CAST(racun_stavke.mpc as NUMERIC)*CAST(REPLACE(rabat,',','.') as NUMERIC)/100))/(1 zbroj (CAST(REPLACE(racun_stavke.porez,',','.') as NUMERIC)/100)))   * CAST(REPLACE(racun_stavke.kolicina,',','.') as NUMERIC)),0)-COALESCE(SUM(CAST(racun_stavke.nbc as NUMERIC)*CAST(REPLACE(racun_stavke.kolicina,',','.') as NUMERIC)),0)) AS Nabavna FROM racun_stavke LEFT JOIN racuni ON racuni.broj_racuna = racun_stavke.broj_racuna AND racun_stavke.id_ducan=racuni.id_ducan AND racun_stavke.id_kasa=racuni.id_kasa LEFT JOIN roba ON roba.sifra = racun_stavke.sifra_robe WHERE roba.oduzmi='NE' " + racuni + "   AND datum_racuna BETWEEN ts AND (ts  zbroj '1month'::interval)::timestamp),0) zbroj " +
                         "   COALESCE((SELECT SUM((faktura_stavke.vpc- (faktura_stavke.vpc*(CAST(REPLACE(rabat,',','.') as NUMERIC)/100))) *  CAST(REPLACE(faktura_stavke.kolicina,',','.') as NUMERIC)) -  SUM((CAST(faktura_stavke.nbc as NUMERIC)*CAST(REPLACE(faktura_stavke.kolicina,',','.') as NUMERIC)))  FROM faktura_stavke  LEFT JOIN fakture ON fakture.broj_fakture = faktura_stavke.broj_fakture  AND faktura_stavke.id_ducan=fakture.id_ducan AND faktura_stavke.id_kasa=fakture.id_kasa LEFT JOIN roba ON roba.sifra = faktura_stavke.sifra   WHERE roba.oduzmi='NE' " + fakture + "  AND fakture.date BETWEEN ts AND (ts  zbroj '1month'::interval)::timestamp),0)) AS usluge " +

                         " FROM generate_series('" + OD_datuma.Year + "-01-01'::timestamp, '" + OD_datuma.Year + "-12-31', '1month'::interval) AS t(ts);";

            DataTable DTchart = classSQL.select(sql, "rac").Tables[0];

            chart1.Series["Series1"].Points.Clear();

            decimal ukupno_roba, ukupno_usluga;
            foreach (DataRow r in DTchart.Rows)
            {
                decimal.TryParse(r["roba"].ToString(), out ukupno_roba);
                decimal.TryParse(r["usluge"].ToString(), out ukupno_usluga);
                chart1.Series["Series1"].Points.AddY(Math.Round(ukupno_roba, 2));
                chart2.Series["Series1"].Points.AddY(Math.Round(ukupno_usluga, 2));
            }

            chart1.Series["Series1"].ChartType = SeriesChartType.Column;
            chart1.Series["Series1"]["PointWidth"] = "0.5";
            chart1.Series["Series1"].IsValueShownAsLabel = true;
            chart1.Series["Series1"].LabelFormat = "N2";
            chart1.Series["Series1"]["BarLabelStyle"] = "Center";
            chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            chart1.Series["Series1"]["DrawingStyle"] = "Cylinder";

            chart2.Series["Series1"].ChartType = SeriesChartType.Column;
            chart2.Series["Series1"]["PointWidth"] = "0.5";
            chart2.Series["Series1"].IsValueShownAsLabel = true;
            chart2.Series["Series1"].LabelFormat = "N2";
            chart2.Series["Series1"]["BarLabelStyle"] = "Center";
            chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            chart2.Series["Series1"]["DrawingStyle"] = "Cylinder";
        }

        private void PostaviDgv()
        {
            string racuni = "", fakture = "";
            if (cbZaposlenik != null)
            {
                racuni += " AND racuni.id_blagajnik='" + cbZaposlenik + "'";
                fakture += " AND fakture.id_zaposlenik='" + cbZaposlenik + "'";
            }
            if (cbDucan != null)
            {
                racuni += " AND racuni.id_ducan='" + cbDucan + "'";
                fakture += " AND fakture.id_ducan='" + cbDucan + "'";
            }

            string sql = "SELECT  " +
                " SUM((faktura_stavke.vpc- " +
                " (faktura_stavke.vpc*(CAST(REPLACE(rabat,',','.') as NUMERIC)/100)))* " +
                " CAST(REPLACE(faktura_stavke.kolicina,',','.') as NUMERIC)) AS Prodajna, " +

                " SUM((CAST(faktura_stavke.nbc as NUMERIC)*CAST(REPLACE(faktura_stavke.kolicina,',','.') as NUMERIC))) AS Nabavna, " +

                " fakture.oduzmi_iz_skladista,roba.oduzmi " +
                " FROM faktura_stavke " +
                " LEFT JOIN fakture ON fakture.broj_fakture = faktura_stavke.broj_fakture AND faktura_stavke.id_ducan=fakture.id_ducan AND faktura_stavke.id_kasa=fakture.id_kasa " +
                " LEFT JOIN roba ON roba.sifra = faktura_stavke.sifra  " +
                " WHERE  " +
                " fakture.date >= '" + OD_datuma.ToString("yyyy-MM-dd H:mm:ss") + "' " +
                " AND fakture.date <= '" + DO_datuma.ToString("yyyy-MM-dd H:mm:ss") + "' " + fakture +
                " GROUP BY roba.oduzmi,fakture.oduzmi_iz_skladista;";

            DataTable DTfak = classSQL.select(sql, "fak").Tables[0];

            sql = "SELECT " +
            " COALESCE( " +
            "     SUM( " +
            "         ((CAST(racun_stavke.mpc as NUMERIC)-(CAST(racun_stavke.mpc as NUMERIC)*CAST(REPLACE(rabat,',','.') as NUMERIC)/100))/ " +
            "         (1zbroj(CAST(REPLACE(racun_stavke.porez,',','.') as NUMERIC)/100)))*CAST(REPLACE(racun_stavke.kolicina,',','.') as NUMERIC) " +
            "     ) " +
            " ,0) AS Prodajna, " +

            " COALESCE(SUM(CAST(racun_stavke.nbc as NUMERIC)*CAST(REPLACE(racun_stavke.kolicina,',','.') as NUMERIC)),0) AS Nabavna, " +
            " roba.oduzmi " +
            " FROM racun_stavke  " +
            " LEFT JOIN racuni ON racuni.broj_racuna = racun_stavke.broj_racuna AND racun_stavke.id_ducan=racuni.id_ducan AND racun_stavke.id_kasa=racuni.id_kasa" +
            " LEFT JOIN roba ON roba.sifra = racun_stavke.sifra_robe  " +
            " WHERE  " +
            " racuni.datum_racuna >= '" + OD_datuma.ToString("yyyy-MM-dd H:mm:ss") + "' " +
            " AND racuni.datum_racuna <= '" + DO_datuma.ToString("yyyy-MM-dd H:mm:ss") + "' " + racuni +
            " GROUP BY roba.oduzmi;";

            DataTable DTrac = classSQL.select(sql, "rac").Tables[0];

            decimal prodajna, nabavna, Uprodajna = 0, Unabavna = 0;
            foreach (DataRow r in DTfak.Rows)
            {
                decimal.TryParse(r["Nabavna"].ToString(), out nabavna);
                decimal.TryParse(r["Prodajna"].ToString(), out prodajna);

                if (r["oduzmi"].ToString() == "DA")
                {
                    if (r["oduzmi_iz_skladista"].ToString() == "1")
                    { dgv.Rows.Add("Fakture robno", nabavna.ToString("N2"), prodajna.ToString("N2"), (prodajna - nabavna).ToString("N2")); Unabavna += nabavna; Uprodajna += prodajna; }
                    else
                    { dgv.Rows.Add("Fakture robno (označena je da se ne skida sa skladišta)", nabavna.ToString("N2"), prodajna.ToString("N2"), (prodajna - nabavna).ToString("N2")); Unabavna += nabavna; Uprodajna += prodajna; }
                }
                else
                {
                    if (r["oduzmi_iz_skladista"].ToString() == "1")
                    { dgv.Rows.Add("Fakture usluge", nabavna.ToString("N2"), prodajna.ToString("N2"), (prodajna - nabavna).ToString("N2")); Unabavna += nabavna; Uprodajna += prodajna; }
                    else
                    { dgv.Rows.Add("Fakture usluge (označena je da se ne skida sa skladišta)", nabavna.ToString("N2"), prodajna.ToString("N2"), (prodajna - nabavna).ToString("N2")); Unabavna += nabavna; Uprodajna += prodajna; }
                }
            }

            foreach (DataRow r in DTrac.Rows)
            {
                decimal.TryParse(r["Nabavna"].ToString(), out nabavna);
                decimal.TryParse(r["Prodajna"].ToString(), out prodajna);

                if (r["oduzmi"].ToString() == "DA")
                {
                    { dgv.Rows.Add("Maloprodaja robno", nabavna.ToString("N2"), prodajna.ToString("N2"), (prodajna - nabavna).ToString("N2")); Unabavna += nabavna; Uprodajna += prodajna; }
                }
                else
                {
                    { dgv.Rows.Add("Maloprodaja usluge", nabavna.ToString("N2"), prodajna.ToString("N2"), (prodajna - nabavna).ToString("N2")); Unabavna += nabavna; Uprodajna += prodajna; }
                }
            }

            dgv.Rows.Add("UKUPNO:", Unabavna.ToString("N2"), Uprodajna.ToString("N2"), (Uprodajna - Unabavna).ToString("N2"));
        }
    }
}