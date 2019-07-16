using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.Knjigaprimitakaiizadatak
{
    public partial class knjigaPII : Form
    {
        public knjigaPII()
        {
            InitializeComponent();
        }

        public string datumOD { get; set; }
        public string datumDO { get; set; }

        private void knjigaPII_Load(object sender, EventArgs e)
        {
            knjiga();
            this.reportViewer1.RefreshReport();
        }

        private void knjiga()
        {
            string naziv_fakture = " podaci_tvrtka.naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql("", naziv_fakture, "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string sql = string.Format(@"select row_number() over(order by x.datum asc) as rb,
x.datum, concat(x.broj, ' ', x.temeljnica) as temeljnica,
x.temeljnica as string1, x.opis,
0 as gotovina,
case when x.temeljnica in ('PRIM', 'KALK') then round(sum(kolicina * (round(((vpc - (vpc * rabat / 100)) * (1 zbroj (pdv / 100))), 3)) ), 3) else 0 end as ziro_ra,
0 as komp,
case when x.temeljnica in ('PRIM', 'KALK') then round(sum(kolicina * ((vpc - (vpc * rabat / 100)) * (pdv / 100))), 3) else 0 end as pdv,
case when x.temeljnica in ('PRIM', 'KALK') then round(sum(kolicina * (round(((vpc - (vpc * rabat / 100)) * (1 zbroj (pdv / 100))), 3)) ), 3) else 0 end as ukupni_primci,

case when x.temeljnica not in ('PRIM', 'KALK') and x.id_nacin_placanja = 1 then round(sum(kolicina * (round(((vpc - (vpc * rabat / 100)) * (1 zbroj (pdv / 100))), 3)) ), 3) else 0 end as gotovina1,
case when x.temeljnica not in ('PRIM', 'KALK') and id_nacin_placanja = 3 then round(sum(kolicina * (round(((vpc - (vpc * rabat / 100)) * (1 zbroj (pdv / 100))), 3)) ), 3) else 0 end as ziro_ra1,
0 as komp1,
case when x.temeljnica not in ('PRIM', 'KALK') then round(sum(kolicina * ((vpc - (vpc * rabat / 100)) * pdv / 100)), 3) else 0 end as pdv1,
case when x.temeljnica not in ('PRIM', 'KALK') then round(sum(kolicina * (round(((vpc - (vpc * rabat / 100)) * (1 zbroj (pdv / 100))), 3)) ), 3) else 0 end as ukupni_izdaci
from (

	select h.broj as broj, 'OTP' as temeljnica, 1 as id_nacin_placanja, h.datum as datum,
	concat(h.id_izradio, ', ', concat(z.ime, ' ', z.prezime)) as opis,
	c.sifra,
	round(coalesce(replace(c.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	round(coalesce(c.vpc, 0), 6) as vpc,
	round(coalesce(replace(c.pdv, ',','.')::numeric, 0), 2) as pdv,
	round(coalesce(replace(c.rabat, ',','.')::numeric, 0), 6) as rabat
	from otpis_robe h
	right join otpis_robe_stavke c on h.broj = c.broj
	left join zaposlenici z on h.id_izradio = z.id_zaposlenik
	where cast(h.datum as date) >= '{0}' and cast(h.datum as date) <= '{1}'

	union all

	select h.broj as broj, 'IZD' as temeljnica, 1 as id_nacin_placanja, h.datum as datum,
	concat(h.id_partner, ', ',
	case when h.id_partner is null or h.id_partner = 0 then 'PRIVATNI KUPAC' else
		case when p.vrsta_korisnika = 1 then p.ime_tvrtke else concat(p.ime, ' ', p.prezime) end
	end) as opis,
	c.sifra,
	round(coalesce(replace(c.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	round(coalesce(c.vpc, 0), 6) as vpc,
	round(coalesce(replace(c.pdv, ',','.')::numeric, 0), 2) as pdv,
	round(coalesce(replace(c.rabat, ',','.')::numeric, 0), 6) as rabat
	from izdatnica h
	right join izdatnica_stavke c on h.broj = c.broj
	left join partners p on h.id_partner = p.id_partner
	where cast(h.datum as date) >= '{0}' and cast(h.datum as date) <= '{1}'

	union all

	select h.broj_racuna::bigint as broj, 'MPR' as temeljnica, 1 as id_nacin_placanja, h.datum_racuna as datum,
	concat(h.id_kupac, ', ',
	case when h.id_kupac is null or h.id_kupac = 0 then 'PRIVATNI KUPAC' else
		case when p.vrsta_korisnika = 1 then p.ime_tvrtke else concat(p.ime, ' ', p.prezime) end
	end) as opis,
	c.sifra_robe,
	round(coalesce(replace(c.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	round(coalesce(c.vpc, 0), 6) as vpc,
	round(coalesce(replace(c.porez, ',','.')::numeric, 0), 2) as pdv,
	round(coalesce(replace(c.rabat, ',','.')::numeric, 0), 6) as rabat
	from racuni h
	right join racun_stavke c on h.broj_racuna = c.broj_racuna and h.id_ducan = c.id_ducan and h.id_kasa = c.id_kasa
	left join partners p on h.id_kupac = p.id_partner
	where cast(h.datum_racuna as date) >= '{0}' and cast(h.datum_racuna as date) <= '{1}'

	union all

	select h.broj as broj, 'IFB' as temeljnica, h.id_nacin_placanja, h.datum as datum,
	concat(h.odrediste, ', ',
	case when h.odrediste is null or h.odrediste = 0 then 'PRIVATNI KUPAC' else
		case when p.vrsta_korisnika = 1 then p.ime_tvrtke else concat(p.ime, ' ', p.prezime) end
	end) as opis,
	c.naziv,
	round(coalesce(c.kolicina, 0), 6) as kolicina,
	round(coalesce(c.vpc, 0), 6) as vpc,
	round(coalesce(c.porez, 0), 2) as pdv,
	round(coalesce(c.rabat, 0), 6) as rabat
	from ifb h
	right join ifb_stavke c on h.broj = c.broj
	left join partners p on h.odrediste = p.id_partner
	where cast(h.datum as date) >= '{0}' and cast(h.datum as date) <= '{1}'

	union all

	select h.broj_fakture as broj, 'IFA' as temeljnica, h.id_nacin_placanja, h.date as datum,
	concat(h.id_fakturirati, ', ',
	case when h.id_fakturirati is null or h.id_fakturirati = 0 then 'PRIVATNI KUPAC' else
		case when p.vrsta_korisnika = 1 then p.ime_tvrtke else concat(p.ime, ' ', p.prezime) end
	end) as opis,
	c.sifra,
	round(coalesce(replace(c.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	round(coalesce(c.vpc, 0), 6) as vpc,
	round(coalesce(replace(c.porez, ',','.')::numeric, 0), 2) as pdv,
	round(coalesce(replace(c.rabat, ',','.')::numeric, 0), 6) as rabat
	from fakture h
	right join faktura_stavke c on h.broj_fakture = c.broj_fakture and h.id_ducan = c.id_ducan and h.id_kasa = c.id_kasa
	left join partners p on h.id_fakturirati = p.id_partner
	where cast(h.date as date) >= '{0}' and cast(h.date as date) <= '{1}'

	union all

	select h.broj as broj, 'PRIM' as temeljnica, 3 as id_nacin_placanja, h.datum as datum,
	concat(h.id_partner, ', ',
	case when h.id_partner is null or h.id_partner = 0 then 'PRIVATNI KUPAC' else
		case when p.vrsta_korisnika = 1 then p.ime_tvrtke else concat(p.ime, ' ', p.prezime) end
	end) as opis,
	c.sifra,
	round(coalesce(replace(c.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	round(coalesce(c.vpc, 0), 6) as vpc,
	round(coalesce(replace(c.pdv, ',','.')::numeric, 0), 2) as pdv,
	round(coalesce(replace(c.rabat, ',','.')::numeric, 0), 6) as rabat
	from primka h
	right join primka_stavke c on h.broj = c.broj
	left join partners p on h.id_partner = p.id_partner
	where cast(h.datum as date) >= '{0}' and cast(h.datum as date) <= '{1}'

	union all

	select h.broj as broj, 'KALK' as temeljnica, 3 as id_nacin_placanja, h.datum as datum,
	concat(h.id_partner, ', ',
	case when h.id_partner is null or h.id_partner = 0 then 'PRIVATNI KUPAC' else
		case when p.vrsta_korisnika = 1 then p.ime_tvrtke else concat(p.ime, ' ', p.prezime) end
	end) as opis,
	c.sifra,
	round(coalesce(replace(c.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	round(coalesce(c.vpc, 0), 6) as vpc,
	round(coalesce(replace(c.porez, ',','.')::numeric, 0), 2) as pdv,
	round(coalesce(0, 0), 6) as rabat
	from kalkulacija h
	right join kalkulacija_stavke c on h.broj = c.broj and h.id_skladiste = c.id_skladiste
	left join partners p on h.id_partner = p.id_partner
	where cast(h.datum as date) >= '{0}' and cast(h.datum as date) <= '{1}'

) x
-- left join (select distinct sifra, iznos from povratna_naknada where iznos != 0) p on x.sifra = p.sifra
group by x.broj, x.temeljnica, x.datum, x.id_nacin_placanja, x.opis
order by x.datum asc;", datumOD, datumDO);

            classSQL.NpgAdatpter(sql).Fill(knjigaPII1, "DataTable1");

            sql = string.Format(@"select y.porez,
sum(y.gotovima) as gotovima,
sum(y.ziro_ra) as ziro_ra,
sum(y.komp) as komp,
sum(y.pdv) as pdv,
sum(y.ukupni_primici) as ukupni_primici,

sum(y.gotovina1) as gotovina1,
sum(y.ziro_ra1) as ziro_ra1,
sum(y.komp1) as komp1,
sum(y.pdv1) as pdv1,
sum(y.ukupni_izdaci) as ukupni_izdaci
from (
    select x.pdv as porez,
    0 as gotovima,
    case when x.temeljnica in ('PRIM', 'KALK') then round(sum(kolicina * (round(((vpc - (vpc * rabat / 100)) * (1 zbroj (pdv / 100))), 3)) ), 3) else 0 end as ziro_ra,
    0 as komp,
    case when x.temeljnica in ('PRIM', 'KALK') then round(sum(kolicina * ((vpc - (vpc * rabat / 100)) * (pdv / 100))), 3) else 0 end as pdv,
    case when x.temeljnica in ('PRIM', 'KALK') then round(sum(kolicina * (round(((vpc - (vpc * rabat / 100)) * (1 zbroj (pdv / 100))), 3)) ), 3) else 0 end as ukupni_primici,

    case when x.temeljnica not in ('PRIM', 'KALK') and x.id_nacin_placanja = 1 then round(sum(kolicina * (round(((vpc - (vpc * rabat / 100)) * (1 zbroj (pdv / 100))), 3)) ), 3) else 0 end as gotovina1,
    case when x.temeljnica not in ('PRIM', 'KALK') and id_nacin_placanja = 3 then round(sum(kolicina * (round(((vpc - (vpc * rabat / 100)) * (1 zbroj (pdv / 100))), 3)) ), 3) else 0 end as ziro_ra1,
    0 as komp1,
    case when x.temeljnica not in ('PRIM', 'KALK') then round(sum(kolicina * ((vpc - (vpc * rabat / 100)) * pdv / 100)), 3) else 0 end as pdv1,
    case when x.temeljnica not in ('PRIM', 'KALK') then round(sum(kolicina * (round(((vpc - (vpc * rabat / 100)) * (1 zbroj (pdv / 100))), 3)) ), 3) else 0 end as ukupni_izdaci
    from (

	    select h.broj as broj, 'OTP' as temeljnica, 1 as id_nacin_placanja, h.datum as datum,
	    concat(h.id_izradio, ', ', concat(z.ime, ' ', z.prezime)) as opis,
	    c.sifra,
	    round(coalesce(replace(c.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	    round(coalesce(c.vpc, 0), 6) as vpc,
	    round(coalesce(replace(c.pdv, ',','.')::numeric, 0), 2) as pdv,
	    round(coalesce(replace(c.rabat, ',','.')::numeric, 0), 6) as rabat
	    from otpis_robe h
	    left join otpis_robe_stavke c on h.broj = c.broj
	    left join zaposlenici z on h.id_izradio = z.id_zaposlenik
	    where cast(h.datum as date) >= '{0}' and cast(h.datum as date) <= '{1}'

	    union all

	    select h.broj as broj, 'IZD' as temeljnica, 1 as id_nacin_placanja, h.datum as datum,
	    concat(h.id_partner, ', ',
	    case when h.id_partner is null or h.id_partner = 0 then 'PRIVATNI KUPAC' else
		    case when p.vrsta_korisnika = 1 then p.ime_tvrtke else concat(p.ime, ' ', p.prezime) end
	    end) as opis,
	    c.sifra,
	    round(coalesce(replace(c.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	    round(coalesce(c.vpc, 0), 6) as vpc,
	    round(coalesce(replace(c.pdv, ',','.')::numeric, 0), 2) as pdv,
	    round(coalesce(replace(c.rabat, ',','.')::numeric, 0), 6) as rabat
	    from izdatnica h
	    left join izdatnica_stavke c on h.broj = c.broj
	    left join partners p on h.id_partner = p.id_partner
	    where cast(h.datum as date) >= '{0}' and cast(h.datum as date) <= '{1}'

	    union all

	    select h.broj_racuna::bigint as broj, 'MPR' as temeljnica, 1 as id_nacin_placanja, h.datum_racuna as datum,
	    concat(h.id_kupac, ', ',
	    case when h.id_kupac is null or h.id_kupac = 0 then 'PRIVATNI KUPAC' else
		    case when p.vrsta_korisnika = 1 then p.ime_tvrtke else concat(p.ime, ' ', p.prezime) end
	    end) as opis,
	    c.sifra_robe,
	    round(coalesce(replace(c.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	    round(coalesce(c.vpc, 0), 6) as vpc,
	    round(coalesce(replace(c.porez, ',','.')::numeric, 0), 2) as pdv,
	    round(coalesce(replace(c.rabat, ',','.')::numeric, 0), 6) as rabat
	    from racuni h
	    left join racun_stavke c on h.broj_racuna = c.broj_racuna and h.id_ducan = c.id_ducan and h.id_kasa = c.id_kasa
	    left join partners p on h.id_kupac = p.id_partner
	    where cast(h.datum_racuna as date) >= '{0}' and cast(h.datum_racuna as date) <= '{1}'

	    union all

	    select h.broj as broj, 'IFB' as temeljnica, h.id_nacin_placanja, h.datum as datum,
	    concat(h.odrediste, ', ',
	    case when h.odrediste is null or h.odrediste = 0 then 'PRIVATNI KUPAC' else
		    case when p.vrsta_korisnika = 1 then p.ime_tvrtke else concat(p.ime, ' ', p.prezime) end
	    end) as opis,
	    c.naziv,
	    round(coalesce(c.kolicina, 0), 6) as kolicina,
	    round(coalesce(c.vpc, 0), 6) as vpc,
	    round(coalesce(c.porez, 0), 2) as pdv,
	    round(coalesce(c.rabat, 0), 6) as rabat
	    from ifb h
	    left join ifb_stavke c on h.broj = c.broj
	    left join partners p on h.odrediste = p.id_partner
	    where cast(h.datum as date) >= '{0}' and cast(h.datum as date) <= '{1}'

	    union all

	    select h.broj_fakture as broj, 'IFA' as temeljnica, h.id_nacin_placanja, h.date as datum,
	    concat(h.id_fakturirati, ', ',
	    case when h.id_fakturirati is null or h.id_fakturirati = 0 then 'PRIVATNI KUPAC' else
		    case when p.vrsta_korisnika = 1 then p.ime_tvrtke else concat(p.ime, ' ', p.prezime) end
	    end) as opis,
	    c.sifra,
	    round(coalesce(replace(c.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	    round(coalesce(c.vpc, 0), 6) as vpc,
	    round(coalesce(replace(c.porez, ',','.')::numeric, 0), 2) as pdv,
	    round(coalesce(replace(c.rabat, ',','.')::numeric, 0), 6) as rabat
	    from fakture h
	    left join faktura_stavke c on h.broj_fakture = c.broj_fakture and h.id_ducan = c.id_ducan and h.id_kasa = c.id_kasa
	    left join partners p on h.id_fakturirati = p.id_partner
	    where cast(h.date as date) >= '{0}' and cast(h.date as date) <= '{1}'

	    union all

	    select h.broj as broj, 'PRIM' as temeljnica, 3 as id_nacin_placanja, h.datum as datum,
	    concat(h.id_partner, ', ',
	    case when h.id_partner is null or h.id_partner = 0 then 'PRIVATNI KUPAC' else
		    case when p.vrsta_korisnika = 1 then p.ime_tvrtke else concat(p.ime, ' ', p.prezime) end
	    end) as opis,
	    c.sifra,
	    round(coalesce(replace(c.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	    round(coalesce(c.vpc, 0), 6) as vpc,
	    round(coalesce(replace(c.pdv, ',','.')::numeric, 0), 2) as pdv,
	    round(coalesce(replace(c.rabat, ',','.')::numeric, 0), 6) as rabat
	    from primka h
	    left join primka_stavke c on h.broj = c.broj
	    left join partners p on h.id_partner = p.id_partner
	    where cast(h.datum as date) >= '{0}' and cast(h.datum as date) <= '{1}'

	    union all

	    select h.broj as broj, 'KALK' as temeljnica, 3 as id_nacin_placanja, h.datum as datum,
	    concat(h.id_partner, ', ',
	    case when h.id_partner is null or h.id_partner = 0 then 'PRIVATNI KUPAC' else
		    case when p.vrsta_korisnika = 1 then p.ime_tvrtke else concat(p.ime, ' ', p.prezime) end
	    end) as opis,
	    c.sifra,
	    round(coalesce(replace(c.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	    round(coalesce(c.vpc, 0), 6) as vpc,
	    round(coalesce(replace(c.porez, ',','.')::numeric, 0), 2) as pdv,
	    round(coalesce(0, 0), 6) as rabat
	    from kalkulacija h
	    left join kalkulacija_stavke c on h.broj = c.broj and h.id_skladiste = c.id_skladiste
	    left join partners p on h.id_partner = p.id_partner
	    where cast(h.datum as date) >= '{0}' and cast(h.datum as date) <= '{1}'

    ) x
    -- left join (select distinct sifra, iznos from povratna_naknada where iznos != 0) p on x.sifra = p.sifra
    where x.temeljnica in ('PRIM', 'KALK')
    group by x.pdv, x.temeljnica, id_nacin_placanja
) y
group by y.porez
order by y.porez;", datumOD, datumDO);

            classSQL.NpgAdatpter(sql).Fill(knjigaPII1, "porezi_prim");

            sql = string.Format(@"select y.porez,
sum(y.gotovima) as gotovima,
sum(y.ziro_ra) as ziro_ra,
sum(y.komp) as komp,
sum(y.pdv) as pdv,
sum(y.ukupni_primici) as ukupni_primici,

sum(y.gotovina1) as gotovina1,
sum(y.ziro_ra1) as ziro_ra1,
sum(y.komp1) as komp1,
sum(y.pdv1) as pdv1,
sum(y.ukupni_izdaci) as ukupni_izdaci
from (
    select x.pdv as porez,
    0 as gotovima,
    case when x.temeljnica in ('PRIM', 'KALK') then round(sum(kolicina * (round(((vpc - (vpc * rabat / 100)) * (1 zbroj (pdv / 100))), 3)) ), 3) else 0 end as ziro_ra,
    0 as komp,
    case when x.temeljnica in ('PRIM', 'KALK') then round(sum(kolicina * ((vpc - (vpc * rabat / 100)) * (pdv / 100))), 3) else 0 end as pdv,
    case when x.temeljnica in ('PRIM', 'KALK') then round(sum(kolicina * (round(((vpc - (vpc * rabat / 100)) * (1 zbroj (pdv / 100))), 3)) ), 3) else 0 end as ukupni_primici,

    case when x.temeljnica not in ('PRIM', 'KALK') and x.id_nacin_placanja = 1 then round(sum(kolicina * (round(((vpc - (vpc * rabat / 100)) * (1 zbroj (pdv / 100))), 3)) ), 3) else 0 end as gotovina1,
    case when x.temeljnica not in ('PRIM', 'KALK') and id_nacin_placanja = 3 then round(sum(kolicina * (round(((vpc - (vpc * rabat / 100)) * (1 zbroj (pdv / 100))), 3)) ), 3) else 0 end as ziro_ra1,
    0 as komp1,
    case when x.temeljnica not in ('PRIM', 'KALK') then round(sum(kolicina * ((vpc - (vpc * rabat / 100)) * pdv / 100)), 3) else 0 end as pdv1,
    case when x.temeljnica not in ('PRIM', 'KALK') then round(sum(kolicina * (round(((vpc - (vpc * rabat / 100)) * (1 zbroj (pdv / 100))), 3)) ), 3) else 0 end as ukupni_izdaci
    from (

	    select h.broj as broj, 'OTP' as temeljnica, 1 as id_nacin_placanja, h.datum as datum,
	    concat(h.id_izradio, ', ', concat(z.ime, ' ', z.prezime)) as opis,
	    c.sifra,
	    round(coalesce(replace(c.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	    round(coalesce(c.vpc, 0), 6) as vpc,
	    round(coalesce(replace(c.pdv, ',','.')::numeric, 0), 2) as pdv,
	    round(coalesce(replace(c.rabat, ',','.')::numeric, 0), 6) as rabat
	    from otpis_robe h
	    right join otpis_robe_stavke c on h.broj = c.broj
	    left join zaposlenici z on h.id_izradio = z.id_zaposlenik
	    where cast(h.datum as date) >= '{0}' and cast(h.datum as date) <= '{1}'

	    union all

	    select h.broj as broj, 'IZD' as temeljnica, 1 as id_nacin_placanja, h.datum as datum,
	    concat(h.id_partner, ', ',
	    case when h.id_partner is null or h.id_partner = 0 then 'PRIVATNI KUPAC' else
		    case when p.vrsta_korisnika = 1 then p.ime_tvrtke else concat(p.ime, ' ', p.prezime) end
	    end) as opis,
	    c.sifra,
	    round(coalesce(replace(c.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	    round(coalesce(c.vpc, 0), 6) as vpc,
	    round(coalesce(replace(c.pdv, ',','.')::numeric, 0), 2) as pdv,
	    round(coalesce(replace(c.rabat, ',','.')::numeric, 0), 6) as rabat
	    from izdatnica h
	    right join izdatnica_stavke c on h.broj = c.broj
	    left join partners p on h.id_partner = p.id_partner
	    where cast(h.datum as date) >= '{0}' and cast(h.datum as date) <= '{1}'

	    union all

	    select h.broj_racuna::bigint as broj, 'MPR' as temeljnica, 1 as id_nacin_placanja, h.datum_racuna as datum,
	    concat(h.id_kupac, ', ',
	    case when h.id_kupac is null or h.id_kupac = 0 then 'PRIVATNI KUPAC' else
		    case when p.vrsta_korisnika = 1 then p.ime_tvrtke else concat(p.ime, ' ', p.prezime) end
	    end) as opis,
	    c.sifra_robe,
	    round(coalesce(replace(c.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	    round(coalesce(c.vpc, 0), 6) as vpc,
	    round(coalesce(replace(c.porez, ',','.')::numeric, 0), 2) as pdv,
	    round(coalesce(replace(c.rabat, ',','.')::numeric, 0), 6) as rabat
	    from racuni h
	    right join racun_stavke c on h.broj_racuna = c.broj_racuna and h.id_ducan = c.id_ducan and h.id_kasa = c.id_kasa
	    left join partners p on h.id_kupac = p.id_partner
	    where cast(h.datum_racuna as date) >= '{0}' and cast(h.datum_racuna as date) <= '{1}'

	    union all

	    select h.broj as broj, 'IFB' as temeljnica, h.id_nacin_placanja, h.datum as datum,
	    concat(h.odrediste, ', ',
	    case when h.odrediste is null or h.odrediste = 0 then 'PRIVATNI KUPAC' else
		    case when p.vrsta_korisnika = 1 then p.ime_tvrtke else concat(p.ime, ' ', p.prezime) end
	    end) as opis,
	    c.naziv,
	    round(coalesce(c.kolicina, 0), 6) as kolicina,
	    round(coalesce(c.vpc, 0), 6) as vpc,
	    round(coalesce(c.porez, 0), 2) as pdv,
	    round(coalesce(c.rabat, 0), 6) as rabat
	    from ifb h
	    right join ifb_stavke c on h.broj = c.broj
	    left join partners p on h.odrediste = p.id_partner
	    where cast(h.datum as date) >= '{0}' and cast(h.datum as date) <= '{1}'

	    union all

	    select h.broj_fakture as broj, 'IFA' as temeljnica, h.id_nacin_placanja, h.date as datum,
	    concat(h.id_fakturirati, ', ',
	    case when h.id_fakturirati is null or h.id_fakturirati = 0 then 'PRIVATNI KUPAC' else
		    case when p.vrsta_korisnika = 1 then p.ime_tvrtke else concat(p.ime, ' ', p.prezime) end
	    end) as opis,
	    c.sifra,
	    round(coalesce(replace(c.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	    round(coalesce(c.vpc, 0), 6) as vpc,
	    round(coalesce(replace(c.porez, ',','.')::numeric, 0), 2) as pdv,
	    round(coalesce(replace(c.rabat, ',','.')::numeric, 0), 6) as rabat
	    from fakture h
	    right join faktura_stavke c on h.broj_fakture = c.broj_fakture and h.id_ducan = c.id_ducan and h.id_kasa = c.id_kasa
	    left join partners p on h.id_fakturirati = p.id_partner
	    where cast(h.date as date) >= '{0}' and cast(h.date as date) <= '{1}'

	    union all

	    select h.broj as broj, 'PRIM' as temeljnica, 3 as id_nacin_placanja, h.datum as datum,
	    concat(h.id_partner, ', ',
	    case when h.id_partner is null or h.id_partner = 0 then 'PRIVATNI KUPAC' else
		    case when p.vrsta_korisnika = 1 then p.ime_tvrtke else concat(p.ime, ' ', p.prezime) end
	    end) as opis,
	    c.sifra,
	    round(coalesce(replace(c.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	    round(coalesce(c.vpc, 0), 6) as vpc,
	    round(coalesce(replace(c.pdv, ',','.')::numeric, 0), 2) as pdv,
	    round(coalesce(replace(c.rabat, ',','.')::numeric, 0), 6) as rabat
	    from primka h
	    right join primka_stavke c on h.broj = c.broj
	    left join partners p on h.id_partner = p.id_partner
	    where cast(h.datum as date) >= '{0}' and cast(h.datum as date) <= '{1}'

	    union all

	    select h.broj as broj, 'KALK' as temeljnica, 3 as id_nacin_placanja, h.datum as datum,
	    concat(h.id_partner, ', ',
	    case when h.id_partner is null or h.id_partner = 0 then 'PRIVATNI KUPAC' else
		    case when p.vrsta_korisnika = 1 then p.ime_tvrtke else concat(p.ime, ' ', p.prezime) end
	    end) as opis,
	    c.sifra,
	    round(coalesce(replace(c.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	    round(coalesce(c.vpc, 0), 6) as vpc,
	    round(coalesce(replace(c.porez, ',','.')::numeric, 0), 2) as pdv,
	    round(coalesce(0, 0), 6) as rabat
	    from kalkulacija h
	    right join kalkulacija_stavke c on h.broj = c.broj and h.id_skladiste = c.id_skladiste
	    left join partners p on h.id_partner = p.id_partner
	    where cast(h.datum as date) >= '{0}' and cast(h.datum as date) <= '{1}'

    ) x
    -- left join (select distinct sifra, iznos from povratna_naknada where iznos != 0) p on x.sifra = p.sifra
    where x.temeljnica not in ('PRIM', 'KALK')
    group by x.pdv, x.temeljnica, id_nacin_placanja
) y
group by y.porez
order by y.porez;", datumOD, datumDO);

            classSQL.NpgAdatpter(sql).Fill(knjigaPII1, "porezi_izd");

            //izdaci();
        }

        private DataTable sveukupno = new DataTable();
        public DataRow Row;
        public DataRow Row1;

        //decimal prinos_iz_prethodnog_prim = 0;
        //decimal prinos_iz_pretnodnog_izd = 0;

        private void izdaci()
        {
            if (sveukupno.Columns.Contains("broj") == false)
            {
                sveukupno.Columns.Add("broj");
                sveukupno.Columns.Add("ukupno");
                sveukupno.Columns.Add("ukupno_prim");

                sveukupno.Columns.Add("datum");
                sveukupno.Columns["datum"].DataType = Type.GetType("System.DateTime");
                //DataColumn colDateTime = new DataColumn("datum");
                //colDateTime.DataType = System.Type.GetType("System.DateTime");
                //sveukupno.Columns.Add(colDateTime);

                sveukupno.Columns.Add("temeljnica");
                sveukupno.Columns.Add("opis");
                sveukupno.Columns.Add("gotovina_izd");
                sveukupno.Columns.Add("gotovina_prim");
                sveukupno.Columns.Add("ziro_ra_prim");
                sveukupno.Columns.Add("ziro_ra_izd");
                sveukupno.Columns.Add("pdv_izd");
                sveukupno.Columns.Add("pdv_prim");
            }

            fakt_izd();
            ifb_izda();
            mpr();
            izd();
            otpis_robe();
            kalkulacije();
            primke();

            DataView dv = new DataView(sveukupno);
            dv.Sort = "datum asc";

            int k = 1;
            foreach (DataRowView rowView in dv)
            {
                //}

                //    for (int k = 0; k < sveukupno.Rows.Count; k++)
                //{
                decimal got_izd = 0;
                decimal ziro_izd1 = 0;
                decimal pdv_izd = 0;
                decimal pdv_prim = 0;
                decimal got_prim = 0;
                decimal ziro_prim = 0;
                decimal uk_prim = 0;
                if (rowView["gotovina_izd"].ToString() != "") { got_izd = Convert.ToDecimal(rowView["gotovina_izd"].ToString()); } else { got_izd = 0; };
                if (rowView["ziro_ra_izd"].ToString() != "") { ziro_izd1 = Convert.ToDecimal(rowView["ziro_ra_izd"].ToString()); } else { ziro_izd1 = 0; };
                if (rowView["pdv_izd"].ToString() != "") { pdv_izd = Convert.ToDecimal(rowView["pdv_izd"].ToString()); } else { pdv_izd = 0; };
                if (rowView["pdv_prim"].ToString() != "") { pdv_prim = Convert.ToDecimal(rowView["pdv_prim"].ToString()); } else { pdv_prim = 0; };
                if (rowView["gotovina_prim"].ToString() != "") { got_prim = Convert.ToDecimal(rowView["gotovina_prim"].ToString()); } else { got_prim = 0; };
                if (rowView["ziro_ra_prim"].ToString() != "") { ziro_prim = Convert.ToDecimal(rowView["ziro_ra_prim"].ToString()); } else { ziro_prim = 0; };
                if (rowView["ukupno_prim"].ToString() != "") { uk_prim = Convert.ToDecimal(rowView["ukupno_prim"].ToString()); } else { uk_prim = 0; };
                DateTime datum = Convert.ToDateTime(rowView["datum"].ToString());

                DataRow DTrow = knjigaPII1.Tables[0].NewRow();

                DTrow = knjigaPII1.Tables[0].NewRow();
                DTrow["rb"] = k.ToString();
                DTrow["datum"] = string.Format("{0:d/M/yyyy}", datum);
                DTrow["temeljnica"] = rowView["broj"].ToString() + " " + rowView["temeljnica"].ToString();
                DTrow["string1"] = rowView["temeljnica"].ToString();
                DTrow["opis"] = rowView["opis"].ToString();
                DTrow["gotovina"] = got_prim;
                DTrow["ziro_ra"] = ziro_prim;
                DTrow["komp"] = 0;
                DTrow["pdv"] = pdv_prim;
                DTrow["ukupni_primci"] = uk_prim;
                DTrow["gotovina1"] = got_izd;
                DTrow["ziro_ra1"] = ziro_izd1;
                DTrow["komp1"] = 0;
                DTrow["pdv1"] = pdv_izd;
                DTrow["ukupni_izdaci"] = Convert.ToDecimal(rowView["ukupno"].ToString());
                knjigaPII1.Tables[0].Rows.Add(DTrow);

                k++;
            }
        }

        private void otpis_robe()
        {
            DataTable DTotpis = classSQL.select("SELECT * FROM otpis_robe Where cast(datum as date) >= '" + datumOD + "' AND cast(datum as date) <= '" + datumDO + "'", "otpisi robe").Tables[0];

            for (int n = 0; n < DTotpis.Rows.Count; n++)
            {
                decimal porez = 0;
                decimal porez_uk = 0;
                decimal ukupno_mpc_faktura = 0;
                decimal gotovina = 0;
                decimal ziro_izd = 0;

                DataTable zaposlenik = classSQL.select("Select ime,prezime From zaposlenici Where id_zaposlenik = " +
                "'" + DTotpis.Rows[n]["id_izradio"].ToString() + "'", "ime zaposlenika").Tables[0];
                string ime_partnera = zaposlenik.Rows[n]["ime"].ToString() + " " + zaposlenik.Rows[n]["prezime"].ToString();

                decimal otpis_mpc = 0;

                DataTable DTotpis_stv = classSQL.select("SELECT * FROM otpis_robe_stavke Where broj = '" + DTotpis.Rows[n]["broj"].ToString() + "'", "izdatnice").Tables[0];

                for (int p = 0; p < DTotpis_stv.Rows.Count; p++)
                {
                    decimal pdv = Convert.ToDecimal(DTotpis_stv.Rows[p]["pdv"].ToString());
                    decimal vpc = Convert.ToDecimal(DTotpis_stv.Rows[p]["vpc"].ToString());
                    decimal kol = Convert.ToDecimal(DTotpis_stv.Rows[p]["kolicina"].ToString());
                    decimal rabat = Convert.ToDecimal(DTotpis_stv.Rows[p]["rabat"].ToString());
                    decimal mpc = Math.Round(vpc * (1 + (pdv / 100)), 3);
                    decimal mpc_bez_ra = Math.Round(mpc - (mpc * (rabat / 100)), 3);
                    decimal stopa = (pdv * 100) / (pdv + 100);
                    ukupno_mpc_faktura += Math.Round(mpc_bez_ra * kol, 3);
                    porez = Math.Round((mpc_bez_ra * (stopa / 100)) * kol, 3);
                    porez_uk += porez;
                }

                try
                {
                    gotovina = Convert.ToDecimal(ukupno_mpc_faktura);
                }
                catch
                {
                    gotovina = 0;
                }
                Row = sveukupno.NewRow();
                Row["broj"] = DTotpis.Rows[n]["broj"].ToString();
                Row["temeljnica"] = "OTP";
                Row["datum"] = Convert.ToDateTime(DTotpis.Rows[n]["datum"].ToString());
                Row["ukupno"] = ukupno_mpc_faktura;
                Row["ukupno_prim"] = 0;
                Row["pdv_izd"] = porez_uk;
                Row["gotovina_izd"] = gotovina;
                Row["gotovina_prim"] = 0;
                Row["ziro_ra_prim"] = 0;
                Row["ziro_ra_izd"] = ziro_izd;
                Row["opis"] = DTotpis.Rows[n]["id_izradio"].ToString() + " " + ime_partnera;

                sveukupno.Rows.Add(Row);
            }
        }

        private void izd()
        {
            DataTable DTizd = classSQL.select("SELECT * FROM izdatnica Where cast(datum as date) >= '" + datumOD + "' AND cast(datum as date) <= '" + datumDO + "'", "izdatnice").Tables[0];

            for (int z = 0; z < DTizd.Rows.Count; z++)
            {
                decimal porez = 0;
                decimal porez_uk = 0;
                decimal ukupno_mpc_faktura = 0;
                decimal gotovina = 0;
                decimal ziro_izd = 0;

                string ime_partnera = classSQL.select("Select ime_tvrtke From partners Where id_partner = " +
                "'" + DTizd.Rows[z]["id_partner"].ToString() + "'", "ime partnera").Tables[0].Rows[0]["ime_tvrtke"].ToString();
                if (ime_partnera == "") { ime_partnera = "PRIVATNI KUPAC"; }

                DataTable DTizd_stv = classSQL.select("SELECT * FROM izdatnica_stavke Where broj = '" + DTizd.Rows[z]["broj"].ToString() + "'", "izdatnice").Tables[0];
                for (int p = 0; p < DTizd_stv.Rows.Count; p++)
                {
                    decimal pdv = Convert.ToDecimal(DTizd_stv.Rows[p]["pdv"].ToString().Replace(".", ","));
                    decimal vpc = Convert.ToDecimal(DTizd_stv.Rows[p]["vpc"].ToString());
                    decimal kol = Convert.ToDecimal(DTizd_stv.Rows[p]["kolicina"].ToString().Replace(".", ","));
                    decimal rabat = Convert.ToDecimal(DTizd_stv.Rows[p]["rabat"].ToString().Replace(".", ","));
                    decimal mpc = Math.Round(vpc * (1 + (pdv / 100)), 3);
                    decimal mpc_bez_ra = Math.Round(mpc - (mpc * (rabat / 100)), 3);
                    decimal stopa = (pdv * 100) / (pdv + 100);
                    ukupno_mpc_faktura += Math.Round(mpc_bez_ra * kol, 3);
                    porez = Math.Round((mpc_bez_ra * (stopa / 100)) * kol, 3);
                    porez_uk += porez;
                }

                try
                {
                    gotovina = Convert.ToDecimal(ukupno_mpc_faktura);
                }
                catch
                {
                    gotovina = 0;
                }

                Row = sveukupno.NewRow();
                Row["broj"] = DTizd.Rows[z]["broj"].ToString();
                Row["temeljnica"] = "IZD";
                Row["datum"] = Convert.ToDateTime(DTizd.Rows[z]["datum"].ToString());
                Row["ukupno"] = ukupno_mpc_faktura;
                Row["ukupno_prim"] = 0;
                Row["pdv_izd"] = porez_uk;
                Row["gotovina_izd"] = gotovina;
                Row["gotovina_prim"] = 0;
                Row["ziro_ra_prim"] = 0;
                Row["ziro_ra_izd"] = ziro_izd;
                Row["opis"] = DTizd.Rows[z]["id_partner"].ToString() + " " + ime_partnera;

                sveukupno.Rows.Add(Row);
            }
        }

        private void mpr()
        {
            DataTable DTrac = classSQL.select("SELECT * FROM racuni Where cast(datum_racuna as date) >= '" + datumOD + "' AND cast(datum_racuna as date) <= '" + datumDO + "'", "racuni").Tables[0];

            for (int i = 0; i < DTrac.Rows.Count; i++)
            {
                decimal porez = 0;
                decimal porez_uk = 0;
                decimal ukupno_mpc_faktura = 0;
                decimal gotovina = 0;

                DataTable DTracstv = classSQL.select("Select * from racun_stavke Where broj_racuna = '" + DTrac.Rows[i]["broj_racuna"].ToString() + "'", "stavke mpr").Tables[0];

                for (int p = 0; p < DTracstv.Rows.Count; p++)
                {
                    decimal pdv = Convert.ToDecimal(DTracstv.Rows[p]["porez"].ToString());
                    decimal vpc = Convert.ToDecimal(DTracstv.Rows[p]["vpc"].ToString());
                    decimal kol = Convert.ToDecimal(DTracstv.Rows[p]["kolicina"].ToString());
                    decimal rabat = Convert.ToDecimal(DTracstv.Rows[p]["rabat"].ToString());
                    decimal mpc = Math.Round(vpc * (1 + (pdv / 100)), 3);
                    decimal mpc_bez_ra = Math.Round(mpc - (mpc * (rabat / 100)), 3);
                    decimal stopa = (pdv * 100) / (pdv + 100);
                    ukupno_mpc_faktura += Math.Round(mpc_bez_ra * kol, 3);
                    porez = Math.Round((mpc_bez_ra * (stopa / 100)) * kol, 3);
                    porez_uk += porez;
                }

                string ime_partnera = "";
                try
                {
                    string id_kupac = DTrac.Rows[i]["id_kupac"].ToString();
                    if (id_kupac == "0")
                    {
                        ime_partnera = "PRIVATNI KUPAC";
                    }
                    else
                    {
                        ime_partnera = classSQL.select("Select ime_tvrtke From partners Where id_partner = " +
                       "'" + DTrac.Rows[i]["id_kupac"].ToString() + "'", "ime partnera").Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    }
                }
                catch
                { if (ime_partnera == "") { ime_partnera = "PRIVATNI KUPAC"; } }

                try
                {
                    gotovina = Convert.ToDecimal(ukupno_mpc_faktura);
                }
                catch
                {
                    gotovina = 0;
                }

                Row = sveukupno.NewRow();
                Row["broj"] = DTrac.Rows[i]["broj_racuna"].ToString();
                Row["temeljnica"] = "MPR";
                Row["datum"] = Convert.ToDateTime(DTrac.Rows[i]["datum_racuna"].ToString());
                Row["ukupno"] = DTrac.Rows[i]["ukupno"].ToString();
                Row["ukupno_prim"] = 0;
                Row["pdv_izd"] = porez_uk;
                Row["gotovina_izd"] = gotovina;
                Row["gotovina_prim"] = 0;
                Row["ziro_ra_prim"] = 0;
                Row["ziro_ra_izd"] = 0;
                Row["opis"] = DTrac.Rows[i]["id_kupac"].ToString() + " " + ime_partnera;

                sveukupno.Rows.Add(Row);
            }
        }

        private void ifb_izda()
        {
            DataTable DTifb = classSQL.select("SELECT * FROM ifb Where cast(datum as date) >= '" + datumOD + "' AND cast(datum as date) <= '" + datumDO + "'", "faktura bez robe").Tables[0];

            for (int i = 0; i < DTifb.Rows.Count; i++)
            {
                decimal porez = 0;
                decimal porez_uk = 0;
                decimal ukupno_mpc_faktura = 0;
                decimal gotovina = 0;
                decimal ziro_izd = 0;

                DataTable DTifbstv = classSQL.select("Select * from ifb_stavke Where broj = '" + DTifb.Rows[i]["broj"].ToString() + "'", "stavke ifb").Tables[0];

                for (int p = 0; p < DTifbstv.Rows.Count; p++)
                {
                    decimal pdv = Convert.ToDecimal(DTifbstv.Rows[p]["porez"].ToString());
                    decimal vpc = Convert.ToDecimal(DTifbstv.Rows[p]["vpc"].ToString());
                    decimal kol = Convert.ToDecimal(DTifbstv.Rows[p]["kolicina"].ToString());
                    decimal rabat = Convert.ToDecimal(DTifbstv.Rows[p]["rabat"].ToString());
                    decimal mpc = Math.Round(vpc * (1 + (pdv / 100)), 3);
                    decimal mpc_bez_ra = Math.Round(mpc - (mpc * (rabat / 100)), 3);
                    decimal stopa = (pdv * 100) / (pdv + 100);
                    ukupno_mpc_faktura += Math.Round(mpc_bez_ra * kol, 3);
                    porez = Math.Round((mpc_bez_ra * (stopa / 100)) * kol, 3);
                    porez_uk += porez;
                }

                string ime_partnera = classSQL.select("Select ime_tvrtke From partners Where id_partner = " +
                "'" + DTifb.Rows[i]["odrediste"].ToString() + "'", "ime partnera").Tables[0].Rows[0]["ime_tvrtke"].ToString();
                if (ime_partnera == "") { ime_partnera = "PRIVATNI KUPAC"; }
                if (DTifb.Rows[i]["id_nacin_placanja"].ToString() == "1") { gotovina = Convert.ToDecimal(ukupno_mpc_faktura); } else { gotovina = 0; };
                if (DTifb.Rows[i]["id_nacin_placanja"].ToString() == "3") { ziro_izd = Convert.ToDecimal(ukupno_mpc_faktura); } else { ziro_izd = 0; };

                Row = sveukupno.NewRow();
                Row["broj"] = DTifb.Rows[i]["broj"].ToString();
                Row["temeljnica"] = "IFB";
                Row["datum"] = Convert.ToDateTime(DTifb.Rows[i]["datum"].ToString());
                Row["ukupno"] = DTifb.Rows[i]["ukupno"].ToString();
                Row["ukupno_prim"] = 0;
                Row["pdv_izd"] = porez_uk;
                Row["gotovina_izd"] = gotovina;
                Row["gotovina_prim"] = 0;
                Row["ziro_ra_prim"] = 0;
                Row["ziro_ra_izd"] = ziro_izd;
                Row["opis"] = DTifb.Rows[i]["odrediste"].ToString() + " " + ime_partnera;

                sveukupno.Rows.Add(Row);
            }
        }

        private void fakt_izd()
        {
            DataTable DTfak = classSQL.select("SELECT * FROM fakture Where cast(date as date) >= '" + datumOD + "' AND cast(date as date) <= '" + datumDO + "'", "fakture").Tables[0];

            for (int i = 0; i < DTfak.Rows.Count; i++)
            {
                decimal porez = 0;
                decimal porez_uk = 0;
                decimal ukupno_mpc_faktura = 0;
                decimal gotovina = 0;
                decimal ziro_izd = 0;

                DataTable DTfakstv = classSQL.select("Select * from faktura_stavke Where " +
                    " broj_fakture = '" + DTfak.Rows[i]["broj_fakture"].ToString() + "' " +
                    " AND faktura_stavke.id_ducan='" + DTfak.Rows[i]["id_ducan"].ToString() + "'" +
                    " AND faktura_stavke.id_kasa='" + DTfak.Rows[i]["id_kasa"].ToString() + "'", "stavke fakture").Tables[0];

                for (int p = 0; p < DTfakstv.Rows.Count; p++)
                {
                    decimal pdv = Convert.ToDecimal(DTfakstv.Rows[p]["porez"].ToString());
                    decimal vpc = Convert.ToDecimal(DTfakstv.Rows[p]["vpc"].ToString());
                    decimal kol = Convert.ToDecimal(DTfakstv.Rows[p]["kolicina"].ToString());
                    decimal rabat = Convert.ToDecimal(DTfakstv.Rows[p]["rabat"].ToString());
                    decimal mpc = Math.Round(vpc * (1 + (pdv / 100)), 3);
                    decimal mpc_bez_ra = Math.Round(mpc - (mpc * (rabat / 100)), 3);
                    decimal stopa = (pdv * 100) / (pdv + 100);
                    ukupno_mpc_faktura += Math.Round(mpc_bez_ra * kol, 3);
                    porez = Math.Round((mpc_bez_ra * (stopa / 100)) * kol, 3);
                    porez_uk += porez;
                }

                string ime_partnera = classSQL.select("Select ime_tvrtke From partners Where id_partner = " +
                "'" + DTfak.Rows[i]["id_fakturirati"].ToString() + "'", "ime partnera").Tables[0].Rows[0]["ime_tvrtke"].ToString();
                if (ime_partnera == "") { ime_partnera = "PRIVATNI KUPAC"; }
                if (DTfak.Rows[i]["id_nacin_placanja"].ToString() == "1") { gotovina = Convert.ToDecimal(ukupno_mpc_faktura); } else { gotovina = 0; };
                if (DTfak.Rows[i]["id_nacin_placanja"].ToString() == "3") { ziro_izd = Convert.ToDecimal(ukupno_mpc_faktura); } else { ziro_izd = 0; };

                Row = sveukupno.NewRow();
                Row["broj"] = DTfak.Rows[i]["broj_fakture"].ToString();
                Row["temeljnica"] = "IFA";
                Row["datum"] = Convert.ToDateTime(DTfak.Rows[i]["date"].ToString());
                Row["ukupno"] = DTfak.Rows[i]["ukupno"].ToString();
                Row["ukupno_prim"] = 0;
                Row["pdv_izd"] = porez_uk;
                Row["gotovina_izd"] = gotovina;
                Row["gotovina_prim"] = 0;
                Row["ziro_ra_prim"] = 0;
                Row["ziro_ra_izd"] = ziro_izd;
                Row["opis"] = DTfak.Rows[i]["id_fakturirati"].ToString() + " " + ime_partnera;

                sveukupno.Rows.Add(Row);
            }
        }

        private void primke()
        {
            DataTable DTprim = classSQL.select("SELECT * FROM primka Where cast(datum as date) >= '" + datumOD + "' AND cast(datum as date) <= '" + datumDO + "'", "primke").Tables[0];

            for (int i = 0; i < DTprim.Rows.Count; i++)
            {
                decimal porez = 0;
                decimal porez_uk = 0;
                decimal ukupno_mpc_faktura = 0;
                decimal ziro_izd = 0;

                DataTable DTprimstv = classSQL.select("Select * from primka_stavke Where broj = '" + DTprim.Rows[i]["broj"].ToString() + "'", "stavke primka").Tables[0];

                for (int p = 0; p < DTprimstv.Rows.Count; p++)
                {
                    decimal pdv = Convert.ToDecimal(DTprimstv.Rows[p]["pdv"].ToString());
                    decimal vpc = Convert.ToDecimal(DTprimstv.Rows[p]["vpc"].ToString());
                    decimal kol = Convert.ToDecimal(DTprimstv.Rows[p]["kolicina"].ToString());
                    decimal rabat = Convert.ToDecimal(DTprimstv.Rows[p]["rabat"].ToString());
                    decimal mpc = Math.Round(vpc * (1 + (pdv / 100)), 3);
                    decimal mpc_bez_ra = Math.Round(mpc - (mpc * (rabat / 100)), 3);
                    decimal stopa = (pdv * 100) / (pdv + 100);
                    ukupno_mpc_faktura += Math.Round(mpc_bez_ra * kol, 3);
                    porez = Math.Round((mpc_bez_ra * (stopa / 100)) * kol, 3);
                    porez_uk += porez;
                }

                string ime_partnera = classSQL.select("Select ime_tvrtke From partners Where id_partner = " +
                "'" + DTprim.Rows[i]["id_partner"].ToString() + "'", "ime partnera").Tables[0].Rows[0]["ime_tvrtke"].ToString();
                if (ime_partnera == "") { ime_partnera = "PRIVATNI KUPAC"; }

                ziro_izd = Convert.ToDecimal(ukupno_mpc_faktura);

                Row = sveukupno.NewRow();
                Row["broj"] = DTprim.Rows[i]["broj"].ToString();
                Row["temeljnica"] = "PRIM";
                Row["datum"] = Convert.ToDateTime(DTprim.Rows[i]["datum"].ToString());
                Row["ukupno"] = 0;
                Row["ukupno_prim"] = ukupno_mpc_faktura;
                Row["pdv_izd"] = 0;
                Row["pdv_prim"] = porez_uk;
                Row["gotovina_izd"] = 0;
                Row["gotovina_prim"] = 0;
                Row["ziro_ra_prim"] = ziro_izd;
                Row["ziro_ra_izd"] = 0;
                Row["opis"] = DTprim.Rows[i]["id_partner"].ToString() + " " + ime_partnera;

                sveukupno.Rows.Add(Row);
            }
        }

        private void kalkulacije()
        {
            DataTable DTkalk = classSQL.select("SELECT * FROM kalkulacija Where cast(datum as date) >= '" + datumOD + "' AND cast(datum as date) <= '" + datumDO + "'", "kalkulacije").Tables[0];

            for (int i = 0; i < DTkalk.Rows.Count; i++)
            {
                decimal ukupno_mpc_faktura = 0;
                decimal ziro_izd = 0;

                DataTable DTkalkstv = classSQL.select(string.Format("Select * from kalkulacija_stavke Where broj = {0} and id_skladiste = {1};", DTkalk.Rows[i]["broj"].ToString(), DTkalk.Rows[i]["id_skladiste"].ToString()), "stavke fakture").Tables[0];

                //decimal Iznos_fakturni_uk = 0;
                //decimal Iznos_nabavni_uk = 0;
                //decimal Iznos_nabavni = 0;
                //decimal fakt_cijena_sa_sab = 0;
                decimal iznos_bez_poreza = 0;
                decimal iznos_sa_porezom = 0;
                //decimal zavisni = 0;
                //decimal zavisni_temp = 0;
                //decimal marza = 0;
                //decimal iznos_rabata_nafakt = 0;
                //decimal iznos_rabata_nafakt_temp = 0;
                decimal uk_pdv = 0;

                for (int y = 0; y < DTkalkstv.Rows.Count; y++)
                {
                    decimal vpc = Convert.ToDecimal(DTkalkstv.Rows[y]["vpc"].ToString());
                    //decimal kolicina = Convert.ToDecimal(DTkalkstv.Rows[y]["kolicina"].ToString());
                    decimal pdv = Convert.ToDecimal(DTkalkstv.Rows[y]["porez"].ToString());
                    decimal rabat = Convert.ToDecimal(DTkalkstv.Rows[y]["rabat"].ToString());
                    decimal kol = Convert.ToDecimal(DTkalkstv.Rows[y]["kolicina"].ToString());
                    //decimal marza_post = Convert.ToDecimal(DTkalkstv.Rows[y]["marza_postotak"].ToString());
                    //decimal car = Convert.ToDecimal(DTkalkstv.Rows[y]["carina"].ToString());
                    //decimal transport = Convert.ToDecimal(DTkalkstv.Rows[y]["prijevoz"].ToString());
                    //decimal fakt_cijena = Convert.ToDecimal(DTkalkstv.Rows[y]["fak_cijena"].ToString());

                    //decimal vpc_s_kol = vpc * kolicina;
                    //decimal fak_s_kol = fakt_cijena * kolicina;

                    //Iznos_fakturni_uk = Math.Round((fakt_cijena * kol) + Iznos_fakturni_uk, 3);
                    //iznos_rabata_nafakt += Math.Round((fakt_cijena * (rabat / 100)) * kol, 3);
                    //iznos_rabata_nafakt_temp = Math.Round((fakt_cijena * (rabat / 100)) * kol, 3);
                    //fakt_cijena_sa_sab = Math.Round(fakt_cijena - (fakt_cijena * (rabat / 100)), 3);
                    //Iznos_nabavni_uk = Math.Round(((fakt_cijena_sa_sab * kol) + transport + car) + Iznos_nabavni_uk, 3);
                    //Iznos_nabavni = Math.Round(((fakt_cijena_sa_sab * kol) + transport + car), 3);
                    iznos_bez_poreza = Math.Round((vpc * kol) + iznos_bez_poreza, 3);
                    iznos_sa_porezom = Math.Round(((vpc + (vpc * (pdv / 100))) * kol) + iznos_sa_porezom, 3);
                    //zavisni = Math.Round((car + transport), 3);
                    //zavisni_temp += (car + transport);
                    //marza = Math.Round(Iznos_nabavni * (marza_post / 100), 3);
                    uk_pdv = iznos_sa_porezom - iznos_bez_poreza;
                }

                string ime_partnera = "";

                DataSet dsPartners = classSQL.select("Select ime_tvrtke From partners Where id_partner = " +
                "'" + DTkalk.Rows[i]["id_partner"].ToString() + "'", "ime partnera");
                if (dsPartners != null && dsPartners.Tables.Count > 0 && dsPartners.Tables[0] != null && dsPartners.Tables[0].Rows.Count > 0)
                {
                    ime_partnera = dsPartners.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                }
                else
                {
                    ime_partnera = "PRIVATNI KUPAC";
                }

                ziro_izd = Convert.ToDecimal(ukupno_mpc_faktura);

                Row = sveukupno.NewRow();
                Row["broj"] = DTkalk.Rows[i]["broj"].ToString();
                Row["temeljnica"] = "KALK";
                Row["datum"] = Convert.ToDateTime(DTkalk.Rows[i]["datum"].ToString());
                Row["ukupno"] = 0;
                Row["ukupno_prim"] = DTkalk.Rows[i]["ukupno_mpc"].ToString();
                Row["pdv_izd"] = 0;
                Row["pdv_prim"] = uk_pdv;
                Row["gotovina_izd"] = 0;
                Row["gotovina_prim"] = 0;
                Row["ziro_ra_prim"] = iznos_sa_porezom;
                Row["ziro_ra_izd"] = 0;
                Row["opis"] = DTkalk.Rows[i]["id_partner"].ToString() + " " + ime_partnera;

                sveukupno.Rows.Add(Row);
            }
        }
    }
}