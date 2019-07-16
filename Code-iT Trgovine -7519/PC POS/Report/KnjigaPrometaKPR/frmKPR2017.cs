using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.KnjigaPrometaKPR
{
    public partial class frmKPR2017 : Form
    {
        public frmKPR2017()
        {
            InitializeComponent();
        }

        private void frmKPR2017_Load(object sender, EventArgs e)
        {
            try
            {
                setMonths();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void setMonths()
        {
            try
            {
                string sql = string.Format(@"select * from (
select 0 as id, 'Cijela godina' as name
union
select 1 as id, 'Siječanj' as name
union
select 2 as id, 'Veljača' as name
union
select 3 as id, 'Ožujak' as name
union
select 4 as id, 'Travanj' as name
union
select 5 as id, 'Svibanj' as name
union
select 6 as id, 'Lipanj' as name
union
select 7 as id, 'Srpanj' as name
union
select 8 as id, 'Kolovoz' as name
union
select 9 as id, 'Rujan' as name
union
select 10 as id, 'Listopad' as name
union
select 11 as id, 'Studeni' as name
union
select 12 as id, 'Prosinac' as name
) x
order by x.id asc;");

                DataSet dsMonths = classSQL.select(sql, "months");
                if (dsMonths != null && dsMonths.Tables.Count > 0 && dsMonths.Tables[0] != null)
                {
                    cmbMonths.ValueMember = "id";
                    cmbMonths.DisplayMember = "name";
                    cmbMonths.DataSource = dsMonths.Tables[0];
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                listaUniverzalna.Clear();

                string sql = string.Format(@"select *
from (
	select row_number() over(order by x.datum asc) as string1, x.datum as datum1, '' as string2, concat('PROMET NA DAN: ', to_char(cast(x.datum as date), 'DD.MM.YYYY.')) as string3, sum(gotovinsko) as decimal1, sum(bezgotovinsko) as decimal2
	from (

		select cast(r.datum_racuna as date) as datum,
		sum(round((((rs.vpc - (rs.vpc * replace(rs.rabat,',','.')::numeric / 100)) * (1 zbroj replace(rs.porez,',','.')::numeric / 100)) * replace(rs.kolicina,',','.')::numeric), 2)) as gotovinsko,
		0 as bezgotovinsko
		from racuni r
		left join racun_stavke rs on r.broj_racuna = rs.broj_racuna and r.id_ducan = rs.id_ducan and r.id_kasa = rs.id_kasa
		group by cast(r.datum_racuna as date)

		union all

		select cast(f.datedvo as date) as datum,
		0 as gotovinsko,
		sum(round((
		case when f.use_nbc = true
		then
			(((fs.nbc::numeric - (fs.nbc::numeric * replace(fs.rabat,',','.')::numeric / 100)) * (1 zbroj replace(fs.porez,',','.')::numeric / 100)) * replace(fs.kolicina,',','.')::numeric)
		else
			(((fs.vpc - (fs.vpc * replace(fs.rabat,',','.')::numeric / 100)) * (1 zbroj replace(fs.porez,',','.')::numeric / 100)) * replace(fs.kolicina,',','.')::numeric)
		end
		),2)) as bezgotovinsko
		from fakture f
		left join faktura_stavke fs on f.broj_fakture = fs.broj_fakture and f.id_ducan = fs.id_ducan and f.id_kasa = fs.id_kasa
		group by cast(f.datedvo as date)

	) x
	group by x.datum
	order by x.datum asc
) y
where case when {0} = 0 then 1 = 1 else date_part('month', y.datum1) = {0} end;", cmbMonths.SelectedValue);

                classSQL.NpgAdatpter(sql).Fill(listaUniverzalna, "DTListaUniverzalna");

                string djelatnost_naziv = "", djelatnost_sifra = "", poduzetnik_ime_prezime = "", poduzetnik_oib = "", poduzetnik_adresa = "", poslovnica_naziv = "", poslovnica_adresa = "";
                djelatnost_naziv = Class.PodaciTvrtka.nazivTvrtke;
                djelatnost_sifra = Class.PodaciTvrtka.sifraDjelatnosti;
                poduzetnik_ime_prezime = Class.PodaciTvrtka.vlasnikTvrtke;
                poduzetnik_oib = Class.PodaciTvrtka.oibTvrtke;
                poduzetnik_adresa = Class.PodaciTvrtka.vlasnikAdresa;
                poslovnica_naziv = (Class.PodaciTvrtka.nazivPoslovnice.Length == 0 ? Class.PodaciTvrtka.nazivTvrtke : Class.PodaciTvrtka.nazivPoslovnice);
                poslovnica_adresa = (Class.PodaciTvrtka.adresaPoslovnice.Length == 0 ? Class.PodaciTvrtka.adresaTvrtke + ", " + Class.PodaciTvrtka.gradTvrtka : Class.PodaciTvrtka.adresaPoslovnice + ", " + Class.PodaciTvrtka.gradPoslovnice);

                ReportParameter p1 = new ReportParameter("djelatnost_naziv", djelatnost_naziv);
                ReportParameter p2 = new ReportParameter("djelatnost_sifra", djelatnost_sifra);
                ReportParameter p3 = new ReportParameter("poduzetnik_ime_prezime", poduzetnik_ime_prezime);
                ReportParameter p4 = new ReportParameter("poduzetnik_oib", poduzetnik_oib);
                ReportParameter p5 = new ReportParameter("poduzetnik_adresa", poduzetnik_adresa);
                ReportParameter p6 = new ReportParameter("poslovnica_naziv", poslovnica_naziv);
                ReportParameter p7 = new ReportParameter("poslovnica_adresa", poslovnica_adresa);

                this.reportViewer.LocalReport.EnableExternalImages = true;
                this.reportViewer.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6, p7 });
                reportViewer.SetDisplayMode(DisplayMode.PrintLayout);

                reportViewer.RefreshReport();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}