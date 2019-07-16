using PCPOS.Report;
using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmKnjigaPopisa : Form
    {
        public frmKnjigaPopisa()
        {
            InitializeComponent();
        }

        private void frmKnjigaPopisa_Load(object sender, EventArgs e)
        {
            try
            {
                DateTime dtNow = DateTime.Now;
                dtpDatumDo.Value = new DateTime(dtNow.Year, dtNow.Month, DateTime.DaysInMonth(dtNow.Year, dtNow.Month), 23, 59, 59);
                dtpDatumOd.Value = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);

                if (Util.Korisno.GodinaKojaSeKoristiUbazi == 2017 && Class.PodaciTvrtka.oibTvrtke == "52598763302")
                {
                    dtpDatumOd.Value = new DateTime(Util.Korisno.GodinaKojaSeKoristiUbazi, 1, 1, 0, 0, 0);
                    dtpDatumOd.Enabled = false;
                }

                DataSet dsSkladista = classSQL.select("select id_skladiste, skladiste from skladiste where aktivnost = 'DA';", "skladiste");
                if (dsSkladista != null && dsSkladista.Tables.Count > 0 && dsSkladista.Tables[0] != null && dsSkladista.Tables[0].Rows.Count > 0)
                {
                    cmbSkladiste.DisplayMember = "skladiste";
                    cmbSkladiste.ValueMember = "id_skladiste";
                    cmbSkladiste.DataSource = dsSkladista.Tables[0];

                    string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");
                    classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
                }
                else
                {
                    pnlTop.Enabled = false;
                    pnlFill.Enabled = false;
                    MessageBox.Show("Za izvještaj su potrebna skladišta, kreirajte skladište!");
                }
            }
            catch (Exception)
            {
                throw;
            }
            this.reportViewer1.RefreshReport();
        }

        private void btnUcitaj_Click(object sender, EventArgs e)
        {
            try
            {
                dSRpodaciTvrtke.Tables[0].Rows[0]["string1"] = "Datum od: " + dtpDatumOd.Value.ToString("dd.MM.yyyy. HH:mm:ss") + " do: " + dtpDatumDo.Value.ToString("dd.MM.yyyy. HH:mm:ss") +
                    Environment.NewLine + "Skladište: " + (cmbSkladiste.SelectedItem as DataRowView)["skladiste"].ToString() +
                    Environment.NewLine + (chbUzmiIUsluge.Checked ? "Roba i Usluga" : "Samo Roba");
                listaUniverzalna.Clear();

                string pocetni_broj = "1";
                if (Util.Korisno.GodinaKojaSeKoristiUbazi == 2017 && Class.PodaciTvrtka.oibTvrtke == "52598763302")
                {
                    pocetni_broj = "516";
                }

                string sql = @"SELECT " + pocetni_broj + @" - 1 zbroj row_number() over(ORDER BY x.DATUM asc) as decimal1, x.broj as decimal2, x.datum as datum1, x.skladiste as decimal3,
case when x.ui = 'ulaz' then coalesce(round(sum((x.mpc - (x.mpc * x.rabat / 100)) * x.kolicina), 2), 0) else 0 end as decimal4,
case when x.ui = 'izlaz' then coalesce(round(sum((x.mpc - (x.mpc * x.rabat / 100)) * x.kolicina), 2), 0) else 0 end as decimal5,
x.doc as string1, x.ui as string2, p.partner as string3, x.dokument as string4  FROM (
/*RACUNI*/
SELECT racun_stavke.sifra_robe as sifra, 0 as broj, to_timestamp(concat(cast(datum_racuna as date)::varchar, ' 23:59:59'), 'YYYY-MM-DD HH24:MI:SS')::timestamp without time zone as datum, racun_stavke.id_skladiste as skladiste, COALESCE(CAST(REPLACE(racun_stavke.kolicina,',','.') as NUMERIC),0) AS kolicina,
CAST(racun_stavke.mpc as numeric) as mpc, CAST(REPLACE(racun_stavke.rabat,',','.') as numeric) as rabat, 'Maloprodaja' as doc, 'izlaz' as ui, 0 as partner,
--concat(racuni.broj_racuna, '/', d.ime_ducana, '/', b.ime_blagajne) as dokument
cast(datum_racuna as date)::varchar as dokument
FROM racun_stavke
LEFT JOIN racuni ON racuni.broj_racuna = racun_stavke.broj_racuna AND racuni.id_ducan = racun_stavke.id_ducan AND racuni.id_kasa = racun_stavke.id_kasa
left join ducan d on racuni.id_ducan = d.id_ducan
left join blagajna b on racuni.id_kasa = b.id_blagajna and d.id_ducan = b.id_ducan
UNION ALL

/*KALKULACIJE*/
SELECT kalkulacija_stavke.sifra as sifra, CAST(kalkulacija.broj as BIGINT) as broj, racun_datum, kalkulacija.id_skladiste as skladiste, COALESCE(CAST(REPLACE(kolicina,',','.') as NUMERIC),0) AS kolicina,
round((vpc * (1 zbroj CAST(REPLACE(porez,',','.') as numeric) / 100)),6) zbroj " + (Class.Postavke.koristi_povratnu_naknadu ? "pm.iznos" : "0") + @" as mpc, CAST('0' as numeric) as rabat, 'Kalkulacija' as doc, 'ulaz' as ui, coalesce(id_partner, 0) as partner,
case when otpremnica = null or length(otpremnica) = 0 then racun else otpremnica end as dokument
FROM kalkulacija_stavke
LEFT JOIN kalkulacija ON kalkulacija.broj = kalkulacija_stavke.broj AND kalkulacija.id_skladiste = kalkulacija_stavke.id_skladiste
left join (select distinct sifra, iznos from povratna_naknada) pm on kalkulacija_stavke.sifra = pm.sifra
UNION ALL

/*IZDATNICE*/
SELECT izdatnica_stavke.sifra as sifra, izdatnica.broj, datum, id_skladiste as skladiste, COALESCE(CAST(REPLACE(kolicina,',','.') as NUMERIC),0) AS kolicina,
mpc::numeric as mpc, CAST(REPLACE(rabat,',','.') as numeric) as rabat, 'Izdatnica' as doc, 'izlaz' as ui, coalesce(id_partner, 0) as partner,
case when originalni_dokument = null or length(originalni_dokument) = 0 then concat(extract(year from datum)::varchar, '/', izdatnica.broj::varchar) else originalni_dokument end as dokument
FROM izdatnica_stavke
LEFT JOIN izdatnica ON izdatnica.id_izdatnica = izdatnica_stavke.id_izdatnica
UNION ALL

--PRIMKE
SELECT primka_stavke.sifra as sifra, primka.broj, datum, id_skladiste as skladiste, COALESCE(CAST(REPLACE(kolicina,',','.') as NUMERIC),0) AS kolicina,
mpc::numeric as mpc, CAST('0' as numeric) as rabat, 'Primka' as doc, 'ulaz' as ui, coalesce(id_partner, 0) as partner,
case when originalni_dokument = null or length(originalni_dokument) = 0 then concat(extract(year from datum)::varchar, '/', primka.broj::varchar) else originalni_dokument end as dokument
FROM primka_stavke
LEFT JOIN primka ON primka.id_primka = primka_stavke.id_primka
UNION ALL

--FAKTURE
SELECT faktura_stavke.sifra as sifra, fakture.broj_fakture as broj, datedvo as datum, id_skladiste as skladiste, COALESCE(CAST(REPLACE(faktura_stavke.kolicina,',','.') as NUMERIC),0) AS kolicina,
round((faktura_stavke.vpc * (1 zbroj CAST(REPLACE(faktura_stavke.porez,',','.') as numeric) / 100)),6) as mpc, CAST(REPLACE(faktura_stavke.rabat,',','.') as numeric) as rabat, 'Faktura' as doc, 'izlaz' as ui, coalesce(id_fakturirati, 0) as partner,
concat(fakture.broj_fakture, '/', d.ime_ducana, '/', b.ime_blagajne) as dokument
FROM faktura_stavke
LEFT JOIN fakture ON fakture.broj_fakture = faktura_stavke.broj_fakture AND fakture.id_ducan = faktura_stavke.id_ducan AND fakture.id_kasa = faktura_stavke.id_kasa
left join ducan d on fakture.id_ducan = d.id_ducan
left join blagajna b on fakture.id_kasa = b.id_blagajna and d.id_ducan = b.id_ducan
UNION ALL

--MEĐUSKLADIŠNICA IZ SKLADIŠTA
SELECT meduskladisnica_stavke.sifra as sifra, CAST(meduskladisnica.broj as BIGINT) as broj, datum, meduskladisnica.id_skladiste_od as skladiste, COALESCE(CAST(REPLACE(kolicina,',','.') as NUMERIC),0) AS kolicina,
mpc::numeric as mpc, CAST('0' as numeric) as rabat, 'Međuskladišnica IZ' as doc, 'izlaz' as ui, 0 as partner,
case when org_dokumenat = null or length(org_dokumenat) = 0 then concat(meduskladisnica.broj::varchar, '/', extract(year from datum)::varchar) else org_dokumenat end as dokument
FROM meduskladisnica_stavke
LEFT JOIN meduskladisnica ON meduskladisnica.broj = meduskladisnica_stavke.broj AND meduskladisnica.id_skladiste_od = meduskladisnica_stavke.iz_skladista
UNION ALL

--MEĐUSKLADIŠNICA U SKLADIŠTE
SELECT meduskladisnica_stavke.sifra as sifra, CAST(meduskladisnica.broj as BIGINT) as broj, datum, meduskladisnica.id_skladiste_do as skladiste, COALESCE(CAST(REPLACE(kolicina,',','.') as NUMERIC),0) AS kolicina,
mpc::numeric as mpc, CAST('0' as numeric) as rabat, 'Međuskladišnica U' as doc, 'ulaz' as ui, 0 as partner,
case when org_dokumenat = null or length(org_dokumenat) = 0 then concat(meduskladisnica.broj::varchar, '/', extract(year from datum)::varchar) else org_dokumenat end as dokument
FROM meduskladisnica_stavke
LEFT JOIN meduskladisnica ON meduskladisnica.broj = meduskladisnica_stavke.broj AND meduskladisnica.id_skladiste_od = meduskladisnica_stavke.iz_skladista
UNION ALL

--OTPREMNICA
SELECT otpremnica_stavke.sifra_robe as sifra, otpremnice.broj_otpremnice as broj, datum, otpremnice.id_skladiste as skladiste, COALESCE(CAST(REPLACE(kolicina,',','.') as NUMERIC),0) AS kolicina,
round((vpc * (1 zbroj CAST(REPLACE(porez,',','.') as numeric) / 100)),6) as mpc, CAST(REPLACE(rabat,',','.') as numeric) as rabat, 'Otpremnica' as doc, 'izlaz' as ui, coalesce(osoba_partner, 0) as partner,
concat(otpremnice.broj_otpremnice::varchar, '/', extract(year from datum)::varchar) as dokument
FROM otpremnica_stavke
LEFT JOIN otpremnice ON otpremnica_stavke.broj_otpremnice = otpremnice.broj_otpremnice AND otpremnica_stavke.id_skladiste = otpremnice.id_skladiste
UNION ALL

--OTPIS ROBE
SELECT otpis_robe_stavke.sifra as sifra, otpis_robe.broj as broj, datum, id_skladiste as skladiste, COALESCE(CAST(REPLACE(kolicina,',','.') as NUMERIC),0) AS kolicina,
mpc::numeric as mpc, CAST(REPLACE(rabat,',','.') as numeric) as rabat,'Otpis robe' as doc,'izlaz' as ui, 0 as partner,
cast(datum as date)::varchar as dokument
FROM otpis_robe_stavke
LEFT JOIN otpis_robe ON otpis_robe.broj = otpis_robe_stavke.broj
UNION ALL

--POVRAT ROBE
SELECT povrat_robe_stavke.sifra as sifra, povrat_robe.broj, datum,id_skladiste as skladiste, COALESCE(CAST(REPLACE(kolicina,',','.') as NUMERIC),0) AS kolicina,
vpc * (1 zbroj replace(pdv, ',','.')::numeric / 100)* (-1) as mpc, CAST(REPLACE(rabat,',','.') as numeric) as rabat, 'Povrat robe' as doc, 'ulaz' as ui, coalesce(id_partner, 0) as partner,
case when orginalni_dokument = null or length(orginalni_dokument) = 0 then concat(extract(year from datum)::varchar, '/', povrat_robe.broj::varchar) else orginalni_dokument end as dokument
FROM povrat_robe_stavke
LEFT JOIN povrat_robe ON povrat_robe.broj = povrat_robe_stavke.broj
UNION ALL

--ZAPISNIK O PROMJENI CIJENE
SELECT promjena_cijene_stavke.sifra as sifra, promjena_cijene.broj, date as datum, id_skladiste as skladiste, COALESCE(CAST(REPLACE(kolicina,',','.') as NUMERIC),0) AS kolicina,
CASE WHEN promjena_cijene_stavke.stara_cijena::numeric < promjena_cijene_stavke.nova_cijena::numeric THEN (promjena_cijene_stavke.nova_cijena - promjena_cijene_stavke.stara_cijena)::numeric ELSE
(promjena_cijene_stavke.stara_cijena - promjena_cijene_stavke.nova_cijena)::numeric * (-1)::numeric end as mpc,
CAST('0' as numeric) as rabat, 'Promjena cijene' as doc, 'ulaz' as ui, 0 as partner,
concat(promjena_cijene.broj::varchar, '/', extract(year from date)::varchar) as dokument
FROM promjena_cijene_stavke
LEFT JOIN promjena_cijene ON promjena_cijene.broj = promjena_cijene_stavke.broj
UNION ALL

--INVENTURA
SELECT inventura_stavke.sifra_robe as sifra, CAST(inventura.broj_inventure AS BIGINT) as broj, datum, id_skladiste as skladiste, COALESCE(CAST(REPLACE(COALESCE(kolicina,'0'),',','.') as NUMERIC),0) - inventura_stavke.kolicina_koja_je_bila AS kolicina,
cijena as mpc, CAST('0' as numeric) as rabat, 'Inventura' as doc, 'ulaz' as ui, 0 as partner,
concat(inventura.broj_inventure::varchar, '/', extract(year from datum)::varchar) as dokument
FROM inventura_stavke
LEFT JOIN inventura ON inventura.broj_inventure = inventura_stavke.broj_inventure
WHERE inventura.pocetno_stanje != '1'
UNION ALL

--POCETNO
SELECT pocetno.sifra as sifra, CAST('0' as BIGINT) as broj, datum, id_skladiste as skladiste, COALESCE(CAST(REPLACE(kolicina,',','.') as NUMERIC),0) AS kolicina,
prodajna_cijena as mpc, CAST('0' as numeric) as rabat, 'Početno' as doc, 'ulaz' as ui, 0 as partner,
cast(datum as date)::varchar as dokument
FROM pocetno
UNION ALL

/*RADNI NALOG STAVKE koji dodaje na skladište kad se radni nalog izradi*/
SELECT radni_nalog_stavke.sifra_robe as sifra, radni_nalog.broj_naloga as broj, datum_naloga as datum, radni_nalog_stavke.id_skladiste as skladiste, COALESCE(CAST(REPLACE(radni_nalog_stavke.kolicina,',','.') as NUMERIC),0) AS kolicina,
round((CAST(radni_nalog_stavke.vpc as numeric) * (1 zbroj CAST(REPLACE(radni_nalog_stavke.porez,',','.') as numeric)/100)),6) as mpc, CAST('0' as numeric) as rabat, 'Radni nalog stavke' as doc, 'ulaz' as ui, coalesce(id_narucioc, 0) as partner,
concat(extract(year from datum_naloga)::varchar, '/', radni_nalog.broj_naloga) as dokument
FROM radni_nalog_stavke
LEFT JOIN radni_nalog ON radni_nalog.broj_naloga = radni_nalog_stavke.broj_naloga
UNION ALL

/*RADNI NALOG STAVKE-->NORMATIVI*/
SELECT normativi_stavke.sifra_robe as sifra, radni_nalog.broj_naloga as broj, radni_nalog.datum_naloga as datum, normativi_stavke.id_skladiste as skladiste, COALESCE(CAST(replace(normativi_stavke.kolicina, ',', '.') as numeric),0) * COALESCE(CAST(replace(radni_nalog_stavke.kolicina, ',', '.') as numeric),0) AS kolicina,
round((roba_prodaja.vpc * (1 zbroj replace(roba_prodaja.porez, ',','.')::numeric / 100)), 6) as mpc, CAST('0' as numeric) as rabat, 'Radni nalog stavke normativi' as doc, 'izlaz' as ui, 0 as partner,
concat(radni_nalog.broj_naloga::varchar, '/', extract(year from radni_nalog.datum_naloga)::varchar) as dokument
FROM normativi_stavke
LEFT JOIN normativi ON normativi.broj_normativa = normativi_stavke.broj_normativa
LEFT JOIN radni_nalog_stavke ON normativi.sifra_artikla=radni_nalog_stavke.sifra_robe
LEFT JOIN radni_nalog ON radni_nalog.broj_naloga=radni_nalog_stavke.broj_naloga
LEFT JOIN roba_prodaja ON roba_prodaja.sifra = normativi_stavke.sifra_robe AND roba_prodaja.id_skladiste = normativi_stavke.id_skladiste
LEFT JOIN roba ON roba.sifra = normativi_stavke.sifra_robe
WHERE replace(radni_nalog_stavke.kolicina, ',','.')::numeric <> 0

) X
left join (select id_partner, case when vrsta_korisnika = 1 then ime_tvrtke else concat(ime, ' ', prezime) end as partner from partners) p on x.partner = p.id_partner
left join roba r on x.sifra = r.sifra
where x.skladiste = '" + cmbSkladiste.SelectedValue + @"'
and x.datum between '" + dtpDatumOd.Value.ToString("yyyy-MM-dd HH:mm:ss") + @"' and '" + dtpDatumDo.Value.ToString("yyyy-MM-dd HH:mm:ss") + @"'
and " + (chbUzmiIUsluge.Checked ? "1 = 1" : "case when x.ui = 'ulaz' then r.oduzmi = 'DA' else r.oduzmi = 'DA' end") + @"
group by broj, datum, skladiste, doc, ui, p.partner, x.dokument
ORDER BY DATUM asc
";

                if (classSQL.remoteConnectionString == "")
                {
                    classSQL.CeAdatpter(sql).Fill(listaUniverzalna, "DTListaUniverzalna");
                }
                else
                {
                    classSQL.NpgAdatpter(sql).Fill(listaUniverzalna, "DTListaUniverzalna");
                }

                if (listaUniverzalna != null && listaUniverzalna.Tables.Count > 0 && listaUniverzalna.Tables[0] != null && listaUniverzalna.Tables[0].Rows.Count > 0)
                {
                    this.reportViewer1.RefreshReport();
                }
                else
                {
                    MessageBox.Show("Nema rezultata za odabranu pretragu!");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}