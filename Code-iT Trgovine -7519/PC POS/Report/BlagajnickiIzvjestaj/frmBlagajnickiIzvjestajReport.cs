using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Report.BlagajnickiIzvjestaj
{
    public partial class frmBlagajnickiIzvjestajReport : Form
    {
        public frmBlagajnickiIzvjestajReport()
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
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            dSRpodaciTvrtke.Tables[0].Rows[0]["naziv_fakture"] = "";
            this.reportViewer1.RefreshReport();
        }

        public void btnTrazi_Click(object sender, EventArgs e)
        {
            dSsaldaKonti.Clear();

            string filter = " WHERE cast(datum as date) >= '" + tdOdDatuma.Value.ToString("yyyy-MM-dd") + "'" +
                " AND cast(datum as date) <= '" + tdDoDatuma.Value.ToString("yyyy-MM-dd") + "'";

            DateTime datumPrethodniDan = new DateTime(tdOdDatuma.Value.AddDays(-1).Year, tdOdDatuma.Value.AddDays(-1).Month, tdOdDatuma.Value.AddDays(-1).Day);

            string sqlPomocni = string.Format(@"SELECT '{0:yyyy-MM-dd}' as datum, 'STANJE' as string1, 'Stanje blagajne na dan {0:dd.MM.yyyy.}' as string2, sum(ROUND(uplaceno, 2)) as numeric1, sum(ROUND(izdatak, 2)) as numeric2 FROM
                        (
	                        SELECT id, datum, dokumenat,
                            CASE WHEN oznaka_dokumenta is null OR length(oznaka_dokumenta) = 0
                                THEN partners.ime_tvrtke
                                ELSE concat(oznaka_dokumenta, ' - ', partners.ime_tvrtke)
                                END AS oznaka_dokumenta,
                            CASE WHEN dokumenat in ('POČETNO STANJE', 'POZAJMNICA')
                                THEN uplaceno
                                ELSE '0' END as uplaceno, izdatak
                            FROM blagajnicki_izvjestaj
                            LEFT JOIN partners ON blagajnicki_izvjestaj.id_partner = partners.id_partner
                            WHERE dokumenat <> 'PROMET BLAGAJNE'

	                        UNION ALL

                            SELECT '-1' as id,date_trunc('day', datum_racuna) as datum,'PROMET BLAGAJNE' as dokumenat,
	                        concat(MIN(CAST(racuni.broj_racuna AS INT)),'-',MAX(CAST(racuni.broj_racuna AS INT))) AS oznaka_dokumenta,

	                        SUM( ( CAST(racun_stavke.mpc AS NUMERIC) - (CAST(racun_stavke.mpc AS NUMERIC)*CAST(REPLACE(racun_stavke.rabat,',','.') AS NUMERIC)/100) ) * CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC) ) AS uplaceno,'0' as izdatak

	                        FROM racuni
	                        LEFT JOIN racun_stavke ON racuni.broj_racuna=racun_stavke.broj_racuna
                            AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa
	                        WHERE (racuni.nacin_placanja='G' OR racuni.nacin_placanja is null) GROUP BY date_trunc('day', datum_racuna)
                        )
                        bm
                        where cast(datum as date) <= '{0:yyyy-MM-dd}' and cast(datum as date) >= '{1}-01-01'

UNION ALL", datumPrethodniDan, Util.Korisno.GodinaKojaSeKoristiUbazi);

            if (datumPrethodniDan.Month == 12 && datumPrethodniDan.Day == 31)
                sqlPomocni = "";

            string sql = string.Format(@"{0}
SELECT datum, dokumenat as string1,oznaka_dokumenta as string2,ROUND(uplaceno,2) as numeric1,ROUND(izdatak,2) as numeric2 FROM
                        (

	                        SELECT id, datum, dokumenat,
                            CASE WHEN oznaka_dokumenta is null OR length(oznaka_dokumenta) = 0
                                THEN partners.ime_tvrtke
                                ELSE concat(oznaka_dokumenta, ' - ', partners.ime_tvrtke)
                                END AS oznaka_dokumenta,
                            CASE WHEN dokumenat in ('POČETNO STANJE', 'POZAJMNICA')
                                THEN uplaceno
                                ELSE '0' END as uplaceno, izdatak
                            FROM blagajnicki_izvjestaj
                            LEFT JOIN partners ON blagajnicki_izvjestaj.id_partner = partners.id_partner
                            WHERE dokumenat <> 'PROMET BLAGAJNE'

	                        UNION ALL

                            SELECT '-1' as id,date_trunc('day', datum_racuna) as datum,'PROMET BLAGAJNE' as dokumenat,
	                        concat(MIN(CAST(racuni.broj_racuna AS INT)),'-',MAX(CAST(racuni.broj_racuna AS INT))) AS oznaka_dokumenta,

	                        SUM( ( CAST(racun_stavke.mpc AS NUMERIC) - (CAST(racun_stavke.mpc AS NUMERIC)*CAST(REPLACE(racun_stavke.rabat,',','.') AS NUMERIC)/100) ) * CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC) ) AS uplaceno,'0' as izdatak

	                        FROM racuni
	                        LEFT JOIN racun_stavke ON racuni.broj_racuna=racun_stavke.broj_racuna
                            AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa
	                        WHERE (racuni.nacin_placanja='G' OR racuni.nacin_placanja is null) GROUP BY date_trunc('day', datum_racuna)

                        )
                        bm
                        {1}
                        ORDER BY datum ASC", sqlPomocni, filter);

            classSQL.NpgAdatpter(sql.Replace("+", "zbroj")).Fill(dSsaldaKonti, "DTsaldaKonti");

            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            dSRpodaciTvrtke.Tables[0].Rows[0]["naziv_fakture"] = "";
            dSRpodaciTvrtke.Tables[0].Rows[0]["naziv_fakture"] = string.Format("FILTER-> OD DATUMA: {0} DO DATUMA: {1}", tdOdDatuma.Value.ToString("dd.MM.yyyy."), tdDoDatuma.Value.ToString("dd.MM.yyyy."));

            var selectedRow = (from row in (dSsaldaKonti.Tables[0]).AsEnumerable()
                               where row.Field<DateTime>("datum") == datumPrethodniDan
                               select row).FirstOrDefault();

            decimal uplaceno = 0, izdatak = 0;
            if (selectedRow != null)
            {
                decimal.TryParse(selectedRow["numeric1"].ToString(), out uplaceno);
                decimal.TryParse(selectedRow["numeric2"].ToString(), out izdatak);
            }

            ReportParameter p1 = new ReportParameter("do_datum", tdDoDatuma.Value.ToString("dd.MM.yyyy."));
            ReportParameter p2 = new ReportParameter("uplaceno_do", uplaceno.ToString());
            ReportParameter p3 = new ReportParameter("izdatak_do", izdatak.ToString());
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3 });

            this.reportViewer1.RefreshReport();
        }
    }
}