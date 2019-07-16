using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCPOS.Report.Proizvodnja.RekapitulacijaRadnihNaloga
{
    public partial class frmRekapitulacijaRadnihNaloga : Form
    {
        public frmRekapitulacijaRadnihNaloga()
        {
            InitializeComponent();
        }

        private void frmRekapitulacijaRadnihNaloga_Load(object sender, EventArgs e)
        {
            dtpDatumOd.Value = new DateTime(Util.Korisno.GodinaKojaSeKoristiUbazi, 1, 1, 0, 0, 0);
            dtpDatumDo.Value = new DateTime(Util.Korisno.GodinaKojaSeKoristiUbazi, DateTime.Now.Month, DateTime.DaysInMonth(Util.Korisno.GodinaKojaSeKoristiUbazi, DateTime.Now.Month), 23, 59, 59);
            if (Util.Korisno.GodinaKojaSeKoristiUbazi != DateTime.Now.Year)
            {
                dtpDatumDo.Value = new DateTime(Util.Korisno.GodinaKojaSeKoristiUbazi, 12, 31, 23, 59, 59);
            }

            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");


            
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            try
            {
                listaUniverzalna.Clear();

                string sql = string.Format(@"select
rn.broj_naloga as decimal8, rn.datum_naloga as datum1,
rns.sifra_robe as string2, rns.naziv as string3,
rns.kolicina as decimal1, rns.pc as decimal2,
round((rns.kolicina * rns.pc), 6) as decimal3,
rns.nbc as decimal4,
round((rns.kolicina * rns.nbc), 6) as decimal5
from radni_nalog rn
left join (
	select broj_naloga, id_skladiste, sifra_robe, naziv, round(replace(kolicina, ',', '.')::numeric, 6) as kolicina, round(nbc::numeric, 6) as pc, round((vpc::numeric * (1 + (replace(porez, ',', '.')::numeric / 100))), 6) as nbc,
	case when oduzmi = 'DA' then true else false end as oduzmi
	from radni_nalog_stavke
) rns on rn.broj_naloga = rns.broj_naloga
where
rn.datum_naloga between '{0:yyyy-MM-dd HH:mm:ss}' and '{1:yyyy-MM-dd HH:mm:ss}' and
rns.broj_naloga is not null
order by rn.broj_naloga;", dtpDatumOd.Value, dtpDatumDo.Value);


                classSQL.NpgAdatpter(sql.Replace("+", "zbroj")).Fill(listaUniverzalna, "DTListaUniverzalna");

                dSRpodaciTvrtke.Tables[0].Rows[0]["naziv_fakture"] = string.Format("FILTER-> Od datum: {0:dd.MM.yyyy HH:mm:ss} Do datuma: {1:dd.MM.yyyy HH:mm:ss}", dtpDatumOd.Value, dtpDatumDo.Value);

                this.reportViewer.RefreshReport();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
