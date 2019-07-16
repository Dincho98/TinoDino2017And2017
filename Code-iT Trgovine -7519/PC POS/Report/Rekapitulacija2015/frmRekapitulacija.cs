using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Report.Rekapitulacija2015
{
    public partial class frmRekapitulacija : Form
    {
        public frmRekapitulacija()
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
            var dokumenti = new Dictionary<string, string> {
                {"Računi","maloprodaja"},
                {"Kalkulacija","kalkulacija"},
                {"Izdatnica","izdatnica"},
                {"Primka","primka"},
                {"Fakture","fakture"},
                {"Međuskladišnica iz skl","iz_skl"},
                {"Međuskladišnica u skl","u_skl"},
                {"Otpremnica","otpremnica"},
                {"Otpis robe","otpis_robe"},
                {"Povrat robe","povrat_robe"},
                {"Promjena cijene","promjena_cijene"},
                {"Inventura","inventura"},
                {"Pocetno","pocetno"},
                {"Radni nalog","radni_nalog_stavke"},
                {"Normativi RN","radni_nalog_stavke_normativi"},
                {"Radni nalog kroz uslugu","radni_nalog_skida_normative_prema_uslugi"}
            }.OrderBy(x => x.Key).ToList();
            dokumenti.Insert(0, new KeyValuePair<string, string>("Svi dokumenti", "svi"));

            cmbDokumenti.DisplayMember = "Key";
            cmbDokumenti.ValueMember = "Value";
            cmbDokumenti.DataSource = dokumenti;

            tdOdDatuma.Value = new DateTime(Util.Korisno.GodinaKojaSeKoristiUbazi, 01, 01, 00, 00, 00);
            if (Util.Korisno.GodinaKojaSeKoristiUbazi < DateTime.Now.Year)
            {
                tdDoDatuma.Value = new DateTime(Util.Korisno.GodinaKojaSeKoristiUbazi, 12, 31, 23, 59, 59);
            }
            else if (Util.Korisno.GodinaKojaSeKoristiUbazi == DateTime.Now.Year)
            {
                tdDoDatuma.Value = new DateTime(Util.Korisno.GodinaKojaSeKoristiUbazi, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            }

            DataTable DTskl = classSQL.select("SELECT id_skladiste,skladiste FROM skladiste ORDER BY skladiste ASC", "skladiste").Tables[0];
            DataRow r = DTskl.NewRow();
            r[0] = "0";
            r[1] = "Sva skladišta";
            DTskl.Rows.Add(r);
            cbSkladiste.DataSource = DTskl;
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";
            cbSkladiste.SelectedValue = "0";

            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            dSRpodaciTvrtke.Tables[0].Rows[0]["naziv_fakture"] = "";
            this.reportViewer1.RefreshReport();

            //PROVJERAVAM NAVEDENE TABLICE JE DO 2015god DESILO SE DA NEMA VRIJEDNOSTI U KOLONAMA, 2016god MOŽE SE MAKNUTI
            string query = @"UPDATE inventura_stavke SET porez=(SELECT porez FROM roba
                            WHERE roba.sifra=inventura_stavke.sifra_robe LIMIT 1) WHERE porez='' OR porez IS NULL;
	                        UPDATE promjena_cijene_stavke SET kolicina='1' WHERE kolicina='' OR kolicina IS NULL;";
            classSQL.update(query);
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            if (brProizvodackaCijena.Checked)
            {
                proizvodackaCijena(cmbDokumenti.SelectedValue.ToString());
                return;
            }

            string uvijet_datum = "";
            string skladiste = "";
            string skladiste2 = "";
            string zbirno = "";
            string doc = "upper(doc) as string2,";
            string doc2 = "upper('inventura') as string2,";
            string sort = "";
            string uvjet_dokument = "";
            string uvjet_dokument2 = "";
            if (cmbDokumenti.SelectedValue.ToString() != "svi")
            {
                uvjet_dokument = " AND upper(doc) = '" + cmbDokumenti.SelectedValue.ToString().ToUpper() + "'";
                uvjet_dokument2 = "where string2 = '" + cmbDokumenti.SelectedValue.ToString().ToUpper() + "'";
            }

            uvijet_datum = " datum>='" + tdOdDatuma.Value.ToString("yyyy-MM-dd H:mm:00") +
                "' AND datum<='" + tdDoDatuma.Value.ToString("yyyy-MM-dd H:mm:59") + "'";

            if (cbSkladiste.SelectedValue.ToString() != "0")
            {
                skladiste = " AND ulaz_izlaz_robe_financijski.skladiste='" + cbSkladiste.SelectedValue + "'";
                skladiste2 = " AND inventura.id_skladiste='" + cbSkladiste.SelectedValue + "'";
            }

            if (!chbZbirno.Checked)
            {
                zbirno = ",broj";
                doc = "upper(doc) as string2, broj,";
                if (cmbDokumenti.SelectedValue.ToString() == "kalkulacija" && Class.PodaciTvrtka.oibTvrtke == "40097758416")
                {
                    doc = "part.ime_tvrtke || ' ' || upper(doc) as string2, broj,";
                }
                doc2 = "upper('inventura') as string2, x.broj_inventure::bigint as broj,";

                sort = ", CAST(COALESCE(broj,0) AS INT) ASC";
            }

            string uzmi_usluge = "";
            if (!chbUzmiUsluge.Checked)
                uzmi_usluge = " AND oduzmi='DA'";

            listaUniverzalna.Clear();

            string cijenaMpcVpc = "vpc";
            if (rbMpc.Checked)
            {
                if (chbRabat.Checked)
                {
                    cijenaMpcVpc = "((vpc*(1+(porez /100))) - (vpc*(1+(porez/100)))*rabat/100)";
                    cijenaMpcVpc = "((vpc*(1+(porez /100))) * (1- rabat/100))";
                }
                else
                {
                    cijenaMpcVpc = "(vpc * (1+(porez/100)))";
                }
            }
            else
            {
                if (chbRabat.Checked)
                {
                    cijenaMpcVpc = "vpc - (vpc*rabat/100)";
                    cijenaMpcVpc = "vpc * (1 - rabat/100)";
                }
                else
                {
                    cijenaMpcVpc = "vpc";
                }
            }

            string sql = @"SELECT
                                        skladiste.skladiste as string1,
                                        " + doc + @"
                                        ui as string3,

                                        /*NBC*/
                                        CASE WHEN ui='ulaz' THEN SUM(kolicina * nbc) ELSE 0 END AS decimal1,

                                        CASE WHEN ui='izlaz' THEN SUM (kolicina * nbc) ELSE 0 END AS decimal2,

                                        /*VPC*/
                                        CASE WHEN ui='ulaz' and skladiste.samo_nbc = false  THEN SUM (kolicina * " + cijenaMpcVpc + @") ELSE 0 END AS decimal3,

                                        CASE WHEN ui='izlaz' and skladiste.samo_nbc = false  THEN SUM (kolicina * " + cijenaMpcVpc + @") ELSE 0 END AS decimal4
                                        FROM ulaz_izlaz_robe_financijski
                                        LEFT JOIN skladiste ON skladiste.id_skladiste = ulaz_izlaz_robe_financijski.skladiste";

            if (cmbDokumenti.SelectedValue.ToString() == "kalkulacija" && Class.PodaciTvrtka.oibTvrtke == "40097758416")
            {
                sql += @"
left join (select k.broj as br, k.id_skladiste, p.ime_tvrtke
from kalkulacija k
left join partners p on k.id_partner = p.id_partner) part on ulaz_izlaz_robe_financijski.broj = part. br and ulaz_izlaz_robe_financijski.skladiste = part.id_skladiste";
            }

            sql += @"
WHERE " + uvijet_datum + skladiste + uzmi_usluge + uvjet_dokument + @"
             and doc not in ('radni_nalog_skida_normative_prema_uslugi')
GROUP BY string2, string3, ulaz_izlaz_robe_financijski.skladiste, skladiste.skladiste, skladiste.samo_nbc" + zbirno + @"
ORDER BY string2" + sort + ";";

            sql = sql.Replace("+", "zbroj");
            classSQL.NpgAdatpter(sql).Fill(listaUniverzalna, "DTListaUniverzalna");

            if (listaUniverzalna.Tables[0].Rows.Count > 0)
            {
                if (Class.PodaciTvrtka.oibTvrtke == Class.Postavke.OIB_PC1 && chbZbirno.Checked)
                {
                    foreach (DataRow dRow in listaUniverzalna.Tables[0].Rows)
                    {
                        if (dRow["string2"].ToString().ToUpper() == "RADNI_NALOG_STAVKE_NORMATIVI".ToUpper())
                        {
                            string decimal2 = (listaUniverzalna.Tables[0]).AsEnumerable().Where(x => x.Field<string>("string2").ToUpper() == "radni_nalog_stavke".ToUpper() && x.Field<string>("string1").ToString() == dRow["string1"].ToString()).Select(x => x.Field<decimal>("decimal1")).FirstOrDefault().ToString();

                            if (decimal2 != null)
                                dRow["decimal2"] = decimal2;
                        }
                    }
                }

                string cjIzrazena = rbMpc.Checked ? "MPC" : "VPC";
                string IzrazeniRabat = chbRabat.Checked ? "je obračunati rabat." : "nije obračunati rabat.";

                string uzeti_artikli = "nije";
                if (chbUzmiUsluge.Checked)
                    uzeti_artikli = "je";

                listaUniverzalna.Tables[0].Rows[0]["string8"] = "FILTER-> OD DATUMA: " + tdOdDatuma.Value.ToString("dd.MM.yyyy H:mm:ss") + "  DO DATUMA: " + tdDoDatuma.Value.ToString("dd.MM.yyyy H:mm:ss") +
                    "\r\nCijene su izražene u " + cjIzrazena + "." +
                    "\r\nNa prodajne cijene " + IzrazeniRabat + " U iznose " + uzeti_artikli + " uključena usluga.";

                if (rbMpc.Checked)
                    listaUniverzalna.Tables[0].Rows[0]["string7"] = "MPC";
                else
                    listaUniverzalna.Tables[0].Rows[0]["string7"] = "VPC";
            }

            foreach (DataRow r in listaUniverzalna.Tables[0].Rows)
            {
                if (r["string2"].ToString().ToUpper() == "iz_skl".ToUpper())
                    r["string2"] = "Međuskladišnica iz skl";
                else if (r["string2"].ToString().ToUpper() == "u_skl".ToUpper())
                    r["string2"] = "Međuskladišnica u skl";
                else if (r["string2"].ToString().ToUpper() == "radni_nalog_stavke".ToUpper())
                    r["string2"] = "Radni nalog (ULAZ)";
                else if (r["string2"].ToString().ToUpper() == "radni_nalog_stavke_normativi".ToUpper())
                    r["string2"] = "Normativi RN (IZLAZ)";
                else if (r["string2"].ToString().ToUpper() == "radni_nalog_skida_normative_prema_uslugi".ToUpper())
                    r["string2"] = "Radni nalog kroz uslugu";

                if (!chbZbirno.Checked)
                {
                    r["string2"] = r["string2"].ToString() + "  BR: " + r["broj"].ToString();
                }
            }

            this.reportViewer1.RefreshReport();
        }

        private void proizvodackaCijena(string dokument = "fakture")
        {
            string sql = "";
            string doc = "upper(doc) as string2,", zbirno = "", skladiste = "", sort = "";

            if (!chbZbirno.Checked)
            {
                zbirno = ",broj";
                doc = "upper(doc) as string2, broj,";
                sort = ", CAST(COALESCE(ulaz_izlaz_robe_financijski.broj,0) AS INT) ASC";
            }
            if (cbSkladiste.SelectedValue.ToString() != "0")
            {
                skladiste = string.Format("WHERE ulaz_izlaz_robe_financijski.skladiste = '{0}'", cbSkladiste.SelectedValue);
            }

            if (dokument == "fakture")
            {
                sql = string.Format(@"SELECT faktura_stavke.id_stavka AS id,
fakture.broj_fakture AS broj,
fakture.datedvo AS datum,
faktura_stavke.sifra,
faktura_stavke.id_skladiste AS skladiste,
COALESCE(replace(faktura_stavke.kolicina::text, ','::text, '.'::text)::numeric, 0::numeric) AS kolicina,
faktura_stavke.nbc::numeric AS nbc,
replace(faktura_stavke.porez::text, ','::text, '.'::text)::numeric AS porez,
faktura_stavke.proizvodacka_cijena,
0 AS rabat,
'fakture'::text AS doc,
'izlaz'::text AS ui,
roba.oduzmi
FROM faktura_stavke
LEFT JOIN fakture ON fakture.broj_fakture = faktura_stavke.broj_fakture AND fakture.id_ducan = faktura_stavke.id_ducan AND fakture.id_kasa = faktura_stavke.id_kasa
LEFT JOIN roba ON roba.sifra::text = faktura_stavke.sifra::text
where datedvo >= '{0}' and datedvo <= '{1}'",
tdOdDatuma.Value.ToString("yyyy-MM-dd HH:mm:ss"),
tdDoDatuma.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else if (dokument == "primka")
            {
                sql = string.Format(@"SELECT primka_stavke.id_stavka AS id,
primka.broj,
primka.datum,
primka_stavke.sifra,
primka.id_skladiste AS skladiste,
COALESCE(replace(primka_stavke.kolicina::text, ','::text, '.'::text)::numeric, 0::numeric) AS kolicina,
primka_stavke.nbc,
replace(primka_stavke.pdv::text, ','::text, '.'::text)::numeric AS porez,
primka_stavke.proizvodacka_cijena,
0::numeric AS rabat,
'primka'::text AS doc,
'ulaz'::text AS ui,
'DA'::bpchar AS oduzmi
FROM primka_stavke
LEFT JOIN primka ON primka.id_primka = primka_stavke.id_primka
where primka.datum >= '{0}' and primka.datum <= '{1}'",
tdOdDatuma.Value.ToString("yyyy-MM-dd HH:mm:ss"),
tdDoDatuma.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            sql = string.Format(@"SELECT
skladiste.skladiste as string1,
{1}
ui as string3,

/*NBC*/
CASE WHEN ulaz_izlaz_robe_financijski.ui='ulaz' THEN SUM(ulaz_izlaz_robe_financijski.kolicina * ulaz_izlaz_robe_financijski.nbc) ELSE 0 END AS decimal1,

CASE WHEN ulaz_izlaz_robe_financijski.ui='izlaz' THEN SUM (ulaz_izlaz_robe_financijski.kolicina * ulaz_izlaz_robe_financijski.nbc) ELSE 0 END AS decimal2,

/*VPC*/
CASE WHEN ulaz_izlaz_robe_financijski.ui='ulaz' and skladiste.samo_nbc = false  THEN SUM (Round(ulaz_izlaz_robe_financijski.kolicina * ulaz_izlaz_robe_financijski.proizvodacka_cijena, 2)) ELSE 0 END AS decimal3,

CASE WHEN ulaz_izlaz_robe_financijski.ui='izlaz' and skladiste.samo_nbc = false  THEN SUM (Round(ulaz_izlaz_robe_financijski.kolicina * ulaz_izlaz_robe_financijski.proizvodacka_cijena, 2)) ELSE 0 END AS decimal4
FROM ({0}) as ulaz_izlaz_robe_financijski
LEFT JOIN skladiste ON skladiste.id_skladiste = ulaz_izlaz_robe_financijski.skladiste
{2}
GROUP BY string2, string3, ulaz_izlaz_robe_financijski.skladiste, skladiste.skladiste, skladiste.samo_nbc {3}
ORDER BY string2{4}", sql, doc, skladiste, zbirno, sort);

            sql = sql.Replace("+", "zbroj");
            listaUniverzalna.Clear();

            classSQL.NpgAdatpter(sql).Fill(listaUniverzalna, "DTListaUniverzalna");

            if (listaUniverzalna != null && listaUniverzalna.Tables.Count > 0 && listaUniverzalna.Tables[0] != null && listaUniverzalna.Tables[0].Rows.Count > 0)
            {
                listaUniverzalna.Tables[0].Rows[0]["string7"] = "PC";

                listaUniverzalna.Tables[0].Rows[0]["string8"] = "FILTER-> OD DATUMA: " + tdOdDatuma.Value.ToString("dd.MM.yyyy H:mm:ss") + "  DO DATUMA: " + tdDoDatuma.Value.ToString("dd.MM.yyyy H:mm:ss") +
                        "\r\nCijene su izražene u " + listaUniverzalna.Tables[0].Rows[0]["string7"].ToString() + ".";

                foreach (DataRow r in listaUniverzalna.Tables[0].Rows)
                {
                    if (!chbZbirno.Checked)
                    {
                        r["string2"] = r["string2"].ToString() + "  BR: " + r["broj"].ToString();
                    }
                }
            }

            this.reportViewer1.RefreshReport();
        }

        private void brProizvodackaCijena_CheckedChanged(object sender, EventArgs e)
        {
            chbUzmiUsluge.Enabled = true;
            chbRabat.Enabled = true;

            if (brProizvodackaCijena.Checked)
            {
                chbRabat.Checked = false;
                chbUzmiUsluge.Checked = false;
                chbUzmiUsluge.Enabled = false;
                chbRabat.Enabled = false;
            }
        }

        private void cmbDokumenti_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                brProizvodackaCijena.Visible = false;

                string[] dokuments = new string[] { "fakture", "primka" };
                if (Class.PodaciTvrtka.oibTvrtke == "88985647471" && dokuments.Contains(cmbDokumenti.SelectedValue.ToString()))
                {
                    brProizvodackaCijena.Visible = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}