using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Report.Rekapitulacija2017
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
               // {" Svi dokumenti","svi"},
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
                //{"",""},
            }.OrderBy(x => x.Key).ToList();
            //dokumenti.Add({"Svi dokumenti", "svi"});
            dokumenti.Insert(0, new KeyValuePair<string, string>("Svi dokumenti", "svi"));

            cmbDokumenti.DisplayMember = "Key";
            cmbDokumenti.ValueMember = "Value";
            cmbDokumenti.DataSource = dokumenti;
            //cmbDokumenti.DisplayMember = dokumenti.TValue;

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
            //r[0] = "0";
            //r[1] = "Sva skladišta";
            //DTskl.Rows.Add(r);
            //cbSkladiste.DataSource = DTskl;
            //cbSkladiste.DisplayMember = "skladiste";
            //cbSkladiste.ValueMember = "id_skladiste";
            //cbSkladiste.SelectedValue = "0";

            clbSkladiste.DataSource = DTskl;
            clbSkladiste.DisplayMember = "skladiste";
            clbSkladiste.ValueMember = "id_skladiste";

            clbSkladiste.SelectedValue = "0";
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
                //if (cmbDokumenti.SelectedValue.ToString() == "") {
                //    uvjet_dokument = " AND upper(doc) = '" + cmbDokumenti.SelectedValue.ToString().ToUpper() + "'";
                //}
                uvjet_dokument2 = "where string2 = '" + cmbDokumenti.SelectedValue.ToString().ToUpper() + "'";
            }

            uvijet_datum = " datum>='" + tdOdDatuma.Value.ToString("yyyy-MM-dd H:mm:00") +
                "' AND datum<='" + tdDoDatuma.Value.ToString("yyyy-MM-dd H:mm:59") + "'";

            // uvijet_datum = " racun_datum>='" + tdOdDatuma.Value.ToString("yyyy-MM-dd H:mm:00") +
            //"' AND racun_datum<='" + tdDoDatuma.Value.ToString("yyyy-MM-dd H:mm:59") + "'";

            //if (cbSkladiste.SelectedValue.ToString() != "0") {
            string skl = "", skl2 = "";
            foreach (DataRowView item in clbSkladiste.CheckedItems)
            {
                if (skl.Length > 0)
                    skl += ", ";

                if (skl2.Length > 0)
                    skl2 += ", ";

                skl += item[0].ToString();
                skl2 += item[1].ToString();
            }
            if (skl.Length > 0)
            {
                skladiste = " AND ulaz_izlaz_robe_financijski.skladiste in (" + skl + ")";
                skladiste2 = " AND inventura.id_skladiste='" + cbSkladiste.SelectedValue + "'";
            }
            //}

            if (!chbZbirno.Checked)
            {
                zbirno = ",broj";
                doc = "upper(doc) as string2,broj,";
                if (cmbDokumenti.SelectedValue.ToString() == "kalkulacija" && Class.PodaciTvrtka.oibTvrtke == "40097758416")
                {
                    doc = "part.ime_tvrtke || ' ' || upper(doc) as string2, broj,";
                }
                doc2 = "upper('inventura') as string2, x.broj_inventure::bigint as broj,";

                sort = ",CAST(COALESCE(broj,0) AS INT) ASC";
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
'" + skl2 + @"' as string1,
" + doc + @"
ui as string3,

/*NBC*/
CASE WHEN ui='ulaz' THEN SUM(kolicina * nbc) ELSE 0 END AS decimal1,

CASE WHEN ui='izlaz' THEN SUM (kolicina*nbc) ELSE 0 END AS decimal2,

/*VPC*/
CASE WHEN ui='ulaz'  THEN SUM (kolicina*" + cijenaMpcVpc + @") ELSE 0 END AS decimal3,

CASE WHEN ui='izlaz' THEN SUM (kolicina*" + cijenaMpcVpc + @") ELSE 0 END AS decimal4
FROM ulaz_izlaz_robe_financijski
LEFT JOIN skladiste ON skladiste.id_skladiste = ulaz_izlaz_robe_financijski.skladiste";

            sql += @"
WHERE " + uvijet_datum + skladiste + uzmi_usluge + uvjet_dokument + @"
and doc not in ('radni_nalog_skida_normative_prema_uslugi')
GROUP BY string2, string3, string1 " + zbirno + "" +
            @" ORDER BY string2" + sort + ";";

            //DataTable dts = classSQL.select(sql, "dfg").Tables[0];

            //            string sql = @"drop table if exists tempUlazInv;
            //create temporary table tempUlazInv as
            //select datum, CASE WHEN ui='ulaz' THEN SUM (kolicina*nbc) ELSE 0 END AS nbc_ulaz,
            //CASE WHEN ui='izlaz' THEN SUM (kolicina*nbc) ELSE 0 END AS nbc_izlaz,
            //CASE WHEN ui='ulaz' THEN SUM (kolicina*" + cijenaMpcVpc + @") ELSE 0 END AS mpc_ulaz,
            //CASE WHEN ui='izlaz' THEN SUM (kolicina*" + cijenaMpcVpc + @") ELSE 0 END AS mpc_izlaz
            //from ulaz_izlaz_robe_financijski
            //where " + uvijet_datum + skladiste + @"
            //and doc not in ('radni_nalog_skida_normative_prema_uslugi')
            //and oduzmi = 'DA'
            //group by ui, datum;
            //
            //drop table if exists tempInvs;
            //create temporary table tempInvs as
            //select inventura.id_skladiste, inventura.datum,inventura_stavke.broj_inventure, sum(replace(kolicina, ',', '.')::numeric * nbc) as nbc,
            //sum(replace(kolicina ,',','.')::numeric *
            //(inventura_stavke.cijena " + (!rbMpc.Checked ? "/ (1::numeric + replace(COALESCE(inventura_stavke.porez, '0'::character varying)::text, ','::text, '.'::text)::numeric / 100::numeric)" : "") + @")) as vpc
            //from inventura_stavke
            //left join inventura on inventura_stavke.broj_inventure = inventura.broj_inventure
            //where 1=1 " + skladiste2 + @"
            //group by inventura_stavke.broj_inventure, inventura.datum, inventura.id_skladiste;
            //
            //drop table if exists tempFinis;
            //create temporary table tempFInis as
            //select x.datum, x. broj_inventure, sum(y.nbc_ulaz - y.nbc_izlaz) as nbc, sum(y.mpc_ulaz - y.mpc_izlaz) as mpc
            //from tempInvs x
            //left join tempUlazInv y on 1=1
            //where y.datum< x.datum
            //group by x.datum, x.broj_inventure;
            //
            //
            //
            // select * from (
            //SELECT
            //skladiste.skladiste as string1,
            //" + doc + @"
            //ui as string3,
            //
            ///*NBC*/
            //CASE WHEN ui='ulaz' THEN SUM (kolicina*nbc) ELSE 0 END AS decimal1,
            //
            //CASE WHEN ui='izlaz' THEN SUM (kolicina*nbc) ELSE 0 END AS decimal2,
            //
            ///*VPC*/
            //CASE WHEN ui='ulaz' THEN SUM (kolicina*" + cijenaMpcVpc + @") ELSE 0 END AS decimal3,
            //
            //CASE WHEN ui='izlaz' THEN SUM (kolicina*" + cijenaMpcVpc + @") ELSE 0 END AS decimal4
            //
            //FROM ulaz_izlaz_robe_financijski
            //LEFT JOIN skladiste ON skladiste.id_skladiste = ulaz_izlaz_robe_financijski.skladiste
            //WHERE " + uvijet_datum + skladiste + uzmi_usluge + uvjet_dokument + @"
            //and doc not in ('radni_nalog_skida_normative_prema_uslugi', 'inventura')
            //GROUP BY string2, string3, ulaz_izlaz_robe_financijski.skladiste, skladiste.skladiste " + zbirno + @" ORDER BY string2
            // ) alll
            //union
            //select * from (
            //select s.skladiste as string1, " + doc2 + @" 'ulaz' as string3, sum(x.nbc - y.nbc) as decimal1, 0 as decimal2,  sum(x.vpc - y.mpc) as decimal3, 0 as decimal4
            //from tempInvs x
            //left join tempFinis y on x.datum = y.datum and x.broj_inventure = y.broj_inventure
            //left join skladiste s on x.id_skladiste = s.id_skladiste
            //GROUP BY string2, string3,  s.skladiste" + zbirno + @" ORDER BY string2
            //) alll2
            //" + uvjet_dokument2 + @";
            //";

            sql = sql.Replace("+", "zbroj");
            classSQL.NpgAdatpter(sql).Fill(listaUniverzalna, "DTListaUniverzalna");

            if (listaUniverzalna.Tables[0].Rows.Count > 0)
            {
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
    }

    //public class dokumenti {
    //    public string id { get; set; }
    //    public string name { get; set; }
    //    public List<dokumenti> dList { get; set; }

    //    public string[] sId = null;
    //    public string[] sName = null;
    //    public dokumenti () {
    //        sId = new string[] { "maloprodaja", "kalkulacija", "izdatnica", "primka", "fakture", "iz_skl", "u_skl", "otpremnica", "otpis_robe", "povrat_robe", "promjena_cijene", "inventura", "pocetno", "radni_nalog_stavke", "radni_nalog_stavke_normativi", "radni_nalog_skida_normative_prema_uslugi" };
    //        sName = new string[] { "Računi", "Kalkulacija", "Izdatnica", "Primka", "Fakture", "Međuskladišnica iz skl", "Međuskladišnica u skl", "Otpremnica", "Otpis robe", "Povrat robe", "Promjena cijene", "Inventura", "Pocetno", "Radni nalog", "Normativi RN", "Radni nalog kroz uslugu" };

    //        dList = new List<dokumenti>();
    //    }
    //}
}