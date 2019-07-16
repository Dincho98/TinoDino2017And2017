using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.RekapitulacijaAnalitika
{
    public partial class RekapitulacijaAnalitika : Form
    {
        public RekapitulacijaAnalitika()
        {
            InitializeComponent();
        }

        public string skladiste { get; set; }
        public string cijena { get; set; }
        public DateTime DatumOD { get; set; }
        public DateTime DatumDO { get; set; }
        public bool sa_rabatom { get; set; }
        public bool samo_robno { get; set; }

        private string query_samo_robno = "";

        private void frmListe_Load(object sender, EventArgs e)
        {
            if (samo_robno)
            {
                query_samo_robno = " roba.oduzmi='DA'  AND ";
            }
            else
            {
                query_samo_robno = "";
            }

            DatumOD = new DateTime(DatumOD.Year, DatumOD.Month, DatumOD.Day, 0, 0, 0);
            DatumDO = new DateTime(DatumDO.Year, DatumDO.Month, DatumDO.Day, 23, 59, 59);

            string naziv_fakture = " podaci_tvrtka.naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql("", naziv_fakture, "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string sql_skl = "SELECT * FROM skladiste";
            if (skladiste != null) sql_skl += " WHERE id_skladiste=" + skladiste;
            DataTable skladista = classSQL.select(sql_skl, "skladista").Tables[0];

            foreach (DataRow r in skladista.Rows)
            {
                fillDataset(r["id_skladiste"].ToString(), r["skladiste"].ToString());
            }

            switch (cijena)
            {
                case "vpc":
                    cijena = "po skladišnim cijenama";
                    break;

                case "mpc":
                    cijena = "po prodajnim cijenama";
                    break;

                case "nbc":
                    cijena = "po nabavnim cijenama";
                    break;
            }
            ReportParameter p1 = new ReportParameter("Dokument", cijena);

            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1 });
            this.reportViewer1.RefreshReport();
        }

        private void fillDataset(string sklID, string sklName)
        {
            decimal doc, ukupno = 0;
            string sql;
            DataRow DTrow;

            #region FAKTURA

            sql = "SELECT" +
                " 'Faktura ' + CAST(faktura_stavke.broj_fakture AS varchar) AS broj, fakture.date AS datum, ime_tvrtke," +
                " __mpc" +
                " sum(faktura_stavke.vpc*CAST(REPLACE(kolicina,',','.') AS numeric)) AS vpc," +
                " sum(CAST(faktura_stavke.nbc AS numeric)*CAST(REPLACE(kolicina,',','.') AS numeric)) AS nbc" +
                " FROM faktura_stavke LEFT JOIN fakture ON fakture.broj_fakture=faktura_stavke.broj_fakture" +
                " AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa" +
                " LEFT JOIN partners ON fakture.id_fakturirati=partners.id_partner" +
                " LEFT JOIN roba ON roba.sifra=faktura_stavke.sifra " +
                " WHERE " + query_samo_robno + " CAST(fakture.date AS date) BETWEEN '" + DatumOD.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + DatumDO.ToString("yyyy-MM-dd 23:59:59") + "' AND faktura_stavke.id_skladiste=" + sklID +
                " GROUP BY faktura_stavke.broj_fakture,fakture.date,ime_tvrtke" +
                " ORDER BY fakture.date";

            if (sa_rabatom)
            {
                string sss; //= "sum(racun_stavke.vpc*CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric)*(1 zbroj CAST(REPLACE(racun_stavke.porez,',','.') AS numeric)/100)) AS mpc,";
                sss = "sum(CAST(REPLACE(faktura_stavke.kolicina,',','.') AS numeric)*(faktura_stavke.vpc*(1 zbroj CAST(REPLACE(faktura_stavke.porez,',','.') AS numeric)/100)-(((faktura_stavke.vpc*(1 zbroj CAST(REPLACE(faktura_stavke.porez,',','.') AS numeric)/100))*CAST(REPLACE(faktura_stavke.rabat,',','.') AS numeric))/100))) AS mpc,";
                sql = sql.Replace("__mpc", sss);
            }
            else
            {
                sql = sql.Replace("__mpc", "sum(faktura_stavke.vpc*CAST(REPLACE(kolicina,',','.') AS numeric)*(1 zbroj CAST(REPLACE(porez,',','.') AS numeric)/100)) AS mpc,");
            }

            doc = obradiDokument(sql, false);
            ukupno += doc;
            DTrow = dSRliste.Tables[0].NewRow();
            DTrow["naziv"] = sklName;
            DTrow["jmj"] = "Ukupno po fakturama";
            DTrow["cijena3"] = doc;
            dSRliste.Tables[0].Rows.Add(DTrow);
            // Prazan red
            DTrow = dSRliste.Tables[0].NewRow();
            dSRliste.Tables[0].Rows.Add(DTrow);

            #endregion FAKTURA

            #region MALOPRODAJA

            sql = "SELECT" +
                " 'Račun br. ' + racun_stavke.broj_racuna AS broj, racuni.datum_racuna AS datum, ime_tvrtke," +
                " __mpc" +
                " sum(racun_stavke.vpc*CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric)) AS vpc," +
                " sum(CAST(racun_stavke.nbc AS numeric)*CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric)) AS nbc" +
                " FROM racun_stavke LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna" +
                " LEFT JOIN partners ON racuni.id_kupac=partners.id_partner" +
                " LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe " +
                " WHERE " + query_samo_robno + "  CAST(racuni.datum_racuna AS date) BETWEEN '" + DatumOD.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + DatumDO.ToString("yyyy-MM-dd 23:59:59") + "' AND racun_stavke.id_skladiste=" + sklID +
                " GROUP BY racun_stavke.broj_racuna,racuni.datum_racuna,ime_tvrtke" +
                " ORDER BY racuni.datum_racuna";

            if (sa_rabatom)
            {
                string sss; //= "sum(racun_stavke.vpc*CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric)*(1 zbroj CAST(REPLACE(racun_stavke.porez,',','.') AS numeric)/100)) AS mpc,";
                sss = "sum(CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric)*(racun_stavke.vpc*(1 zbroj CAST(REPLACE(racun_stavke.porez,',','.') AS numeric)/100)-(((racun_stavke.vpc*(1 zbroj CAST(REPLACE(racun_stavke.porez,',','.') AS numeric)/100))*CAST(REPLACE(racun_stavke.rabat,',','.') AS numeric))/100))) AS mpc,";
                sql = sql.Replace("__mpc", sss);
            }
            else
            {
                sql = sql.Replace("__mpc", "sum(racun_stavke.vpc*CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric)*(1 zbroj CAST(REPLACE(racun_stavke.porez,',','.') AS numeric)/100)) AS mpc,");
            }

            doc = obradiDokument(sql, false);
            ukupno += doc;
            DTrow = dSRliste.Tables[0].NewRow();
            DTrow["naziv"] = sklName;
            DTrow["jmj"] = "Ukupno po računima ";
            DTrow["cijena3"] = doc;
            dSRliste.Tables[0].Rows.Add(DTrow);
            // Prazan red
            DTrow = dSRliste.Tables[0].NewRow();
            dSRliste.Tables[0].Rows.Add(DTrow);

            #endregion MALOPRODAJA

            #region KALKULACIJA

            sql = "SELECT" +
                " 'Kalkulacija ' + CAST(broj AS varchar) + '/' + CAST(id_skladiste AS varchar) AS broj, datum, ime_tvrtke," +
                " CAST(ukupno_mpc AS numeric) AS mpc," +
                " CAST(ukupno_vpc AS numeric) AS vpc," +
                " CAST(fakturni_iznos AS numeric) AS nbc" +
                " FROM kalkulacija LEFT JOIN partners ON kalkulacija.id_partner=partners.id_partner" +
                " WHERE CAST(datum AS date) BETWEEN '" + DatumOD.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + DatumDO.ToString("yyyy-MM-dd 23:59:59") + "' AND id_skladiste=" + sklID;
            doc = obradiDokument(sql, true);
            ukupno += doc;
            DTrow = dSRliste.Tables[0].NewRow();
            DTrow["naziv"] = sklName;
            DTrow["jmj"] = "Ukupno po kalkulacijama";
            DTrow["cijena3"] = doc;
            dSRliste.Tables[0].Rows.Add(DTrow);
            // Prazan red
            DTrow = dSRliste.Tables[0].NewRow();
            dSRliste.Tables[0].Rows.Add(DTrow);

            #endregion KALKULACIJA

            #region PRIMKA

            sql = "SELECT" +
                " 'PRIMKA ' + CAST(primka.broj AS varchar) + '/' + CAST(primka.id_skladiste AS varchar) AS broj, primka.datum, ime_tvrtke," +
                " SUM(mpc*CAST(REPLACE(kolicina,',','.') AS numeric)) AS mpc," +
                " SUM(vpc*CAST(REPLACE(kolicina,',','.') AS numeric)) AS vpc," +
                " SUM(nbc*CAST(REPLACE(kolicina,',','.') AS numeric)) AS nbc" +
                " FROM primka_stavke LEFT JOIN primka ON primka.id_primka=primka_stavke.id_primka" +
                " LEFT JOIN partners ON primka.id_partner=partners.id_partner" +
                " WHERE CAST(primka.datum AS date) BETWEEN '" + DatumOD.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + DatumDO.ToString("yyyy-MM-dd 23:59:59") + "' AND primka.id_skladiste=" + sklID +
                " GROUP BY primka.broj,primka.datum,ime_tvrtke,primka.id_skladiste" +
                " ORDER BY primka.datum";
            doc = obradiDokument(sql, true);
            ukupno += doc;
            DTrow = dSRliste.Tables[0].NewRow();
            DTrow["naziv"] = sklName;
            DTrow["jmj"] = "Ukupno po primkama";
            DTrow["cijena3"] = doc;
            dSRliste.Tables[0].Rows.Add(DTrow);
            // Prazan red
            DTrow = dSRliste.Tables[0].NewRow();
            dSRliste.Tables[0].Rows.Add(DTrow);

            #endregion PRIMKA

            #region IZDATNICA

            sql = "SELECT" +
                " 'Izdatnica ' + CAST(izdatnica.broj AS varchar) + '/' + CAST(izdatnica.id_skladiste AS varchar) AS broj, izdatnica.datum, ime_tvrtke," +
                " SUM(mpc*CAST(REPLACE(kolicina,',','.') AS numeric)) AS mpc," +
                " SUM(vpc*CAST(REPLACE(kolicina,',','.') AS numeric)) AS vpc," +
                " SUM(nbc*CAST(REPLACE(kolicina,',','.') AS numeric)) AS nbc" +
                " FROM izdatnica_stavke LEFT JOIN izdatnica ON izdatnica.id_izdatnica=izdatnica_stavke.id_izdatnica" +
                " LEFT JOIN partners ON izdatnica.id_partner=partners.id_partner" +
                " WHERE CAST(izdatnica.datum AS date) BETWEEN '" + DatumOD.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + DatumDO.ToString("yyyy-MM-dd 23:59:59") + "' AND izdatnica.id_skladiste=" + sklID +
                " GROUP BY izdatnica.broj,izdatnica.datum,izdatnica.id_skladiste,ime_tvrtke" +
                " ORDER BY izdatnica.datum";
            doc = obradiDokument(sql, false);
            ukupno += doc;
            DTrow = dSRliste.Tables[0].NewRow();
            DTrow["naziv"] = sklName;
            DTrow["jmj"] = "Ukupno po izdatnicama";
            DTrow["cijena3"] = doc;
            dSRliste.Tables[0].Rows.Add(DTrow);
            // Prazan red
            DTrow = dSRliste.Tables[0].NewRow();
            dSRliste.Tables[0].Rows.Add(DTrow);

            #endregion IZDATNICA

            #region MEDUSKLADISNICA IZLAZ

            sql = "SELECT" +
                " 'Međuskladišnica ' + meduskladisnica.broj + '/' + CAST(meduskladisnica.id_skladiste_od AS varchar) AS broj, meduskladisnica.datum,'' AS ime_tvrtke," +
                " SUM(CAST(mpc AS numeric)*CAST(REPLACE(kolicina,',','.') AS numeric)) AS mpc," +
                " SUM(CAST(vpc AS numeric)*CAST(REPLACE(kolicina,',','.') AS numeric)) AS vpc," +
                " SUM(CAST(nbc AS numeric)*CAST(REPLACE(kolicina,',','.') AS numeric)) AS nbc" +
                " FROM meduskladisnica_stavke LEFT JOIN meduskladisnica ON meduskladisnica.broj=meduskladisnica_stavke.broj AND meduskladisnica.id_skladiste_od=meduskladisnica_stavke.iz_skladista" +
                " WHERE CAST(meduskladisnica.datum AS date) BETWEEN '" + DatumOD.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + DatumDO.ToString("yyyy-MM-dd 23:59:59") + "' AND meduskladisnica.id_skladiste_od=" + sklID +
                " GROUP BY meduskladisnica.broj,meduskladisnica.datum,meduskladisnica.id_skladiste_od" +
                " ORDER BY meduskladisnica.datum";
            doc = obradiDokument(sql, false);
            ukupno += doc;
            DTrow = dSRliste.Tables[0].NewRow();
            DTrow["naziv"] = sklName;
            DTrow["jmj"] = "Ukupno po međuskladišnicama izlaz";
            DTrow["cijena3"] = doc;
            dSRliste.Tables[0].Rows.Add(DTrow);
            // Prazan red
            DTrow = dSRliste.Tables[0].NewRow();
            dSRliste.Tables[0].Rows.Add(DTrow);

            #endregion MEDUSKLADISNICA IZLAZ

            #region MEDUSKLADISNICA ULAZ

            sql = "SELECT" +
                " 'Međuskladišnica ' + meduskladisnica.broj + '/' + CAST(meduskladisnica.id_skladiste_od AS varchar) AS broj, meduskladisnica.datum,'' AS ime_tvrtke," +
                " SUM(CAST(mpc AS numeric)*CAST(REPLACE(kolicina,',','.') AS numeric)) AS mpc," +
                " SUM(CAST(vpc AS numeric)*CAST(REPLACE(kolicina,',','.') AS numeric)) AS vpc," +
                " SUM(CAST(nbc AS numeric)*CAST(REPLACE(kolicina,',','.') AS numeric)) AS nbc" +
                " FROM meduskladisnica_stavke LEFT JOIN meduskladisnica ON meduskladisnica.broj=meduskladisnica_stavke.broj AND meduskladisnica.id_skladiste_od=meduskladisnica_stavke.iz_skladista" +
                " WHERE CAST(meduskladisnica.datum AS date) BETWEEN '" + DatumOD.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + DatumDO.ToString("yyyy-MM-dd 23:59:59") + "' AND meduskladisnica.id_skladiste_do=" + sklID +
                " GROUP BY meduskladisnica.broj,meduskladisnica.datum,meduskladisnica.id_skladiste_od" +
                " ORDER BY meduskladisnica.datum";
            doc = obradiDokument(sql, true);
            ukupno += doc;
            DTrow = dSRliste.Tables[0].NewRow();
            DTrow["naziv"] = sklName;
            DTrow["jmj"] = "Ukupno po međuskladišnicama ulaz";
            DTrow["cijena3"] = doc;
            dSRliste.Tables[0].Rows.Add(DTrow);
            // Prazan red
            DTrow = dSRliste.Tables[0].NewRow();
            dSRliste.Tables[0].Rows.Add(DTrow);

            #endregion MEDUSKLADISNICA ULAZ

            #region POVRAT

            sql = "SELECT" +
                " 'Povratnica ' + CAST(povrat_robe.broj AS varchar) AS broj, povrat_robe.datum, ime_tvrtke," +
                " SUM(vpc*(1 zbroj CAST(REPLACE(pdv,',','.') AS numeric)/100)*CAST(REPLACE(kolicina,',','.') AS numeric)) AS mpc," +
                " SUM(vpc*CAST(REPLACE(kolicina,',','.') AS numeric)) AS vpc," +
                " SUM(CAST(nbc AS numeric)*CAST(REPLACE(kolicina,',','.') AS numeric)) AS nbc" +
                " FROM povrat_robe_stavke LEFT JOIN povrat_robe ON povrat_robe.broj=povrat_robe_stavke.broj" +
                " LEFT JOIN partners ON povrat_robe.id_partner=partners.id_partner" +
                " WHERE CAST(povrat_robe.datum AS date) BETWEEN '" + DatumOD.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + DatumDO.ToString("yyyy-MM-dd 23:59:59") + "' AND povrat_robe.id_skladiste=" + sklID +
                " GROUP BY povrat_robe.broj,povrat_robe.datum,ime_tvrtke" +
                " ORDER BY povrat_robe.datum";
            doc = obradiDokument(sql, false);
            ukupno += doc;
            DTrow = dSRliste.Tables[0].NewRow();
            DTrow["naziv"] = sklName;
            DTrow["jmj"] = "Ukupno po povratima";
            DTrow["cijena3"] = doc;
            dSRliste.Tables[0].Rows.Add(DTrow);
            // Prazan red
            DTrow = dSRliste.Tables[0].NewRow();
            dSRliste.Tables[0].Rows.Add(DTrow);

            #endregion POVRAT

            #region OTPREMNICA

            sql = "SELECT" +
                " 'Otpremnica ' + CAST(otpremnice.broj_otpremnice AS varchar) + '/' + CAST(otpremnica_stavke.id_skladiste AS varchar) AS broj, otpremnice.datum, ime_tvrtke," +
                " __mpc" +
                " SUM(vpc*CAST(REPLACE(kolicina,',','.') AS numeric)) AS vpc," +
                " SUM(CAST(nbc AS numeric)*CAST(REPLACE(kolicina,',','.') AS numeric)) AS nbc" +
                " FROM otpremnica_stavke LEFT JOIN otpremnice ON otpremnice.broj_otpremnice=otpremnica_stavke.broj_otpremnice" +
                " LEFT JOIN partners ON otpremnice.osoba_partner=partners.id_partner" +
                " WHERE CAST(otpremnice.datum AS date) BETWEEN '" + DatumOD.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + DatumDO.ToString("yyyy-MM-dd 23:59:59") + "' AND otpremnica_stavke.id_skladiste=" + sklID +
                " GROUP BY otpremnice.broj_otpremnice,otpremnice.datum,otpremnica_stavke.id_skladiste,ime_tvrtke" +
                " ORDER BY otpremnice.datum";

            if (sa_rabatom)
            {
                string sss; //= "sum(racun_stavke.vpc*CAST(REPLACE(racun_stavke.kolicina,',','.') AS numeric)*(1 zbroj CAST(REPLACE(racun_stavke.porez,',','.') AS numeric)/100)) AS mpc,";
                sss = "sum(CAST(REPLACE(otpremnica_stavke.kolicina,',','.') AS numeric)*(otpremnica_stavke.vpc*(1 zbroj CAST(REPLACE(otpremnica_stavke.porez,',','.') AS numeric)/100)-(((otpremnica_stavke.vpc*(1 zbroj CAST(REPLACE(otpremnica_stavke.porez,',','.') AS numeric)/100))*CAST(REPLACE(otpremnica_stavke.rabat,',','.') AS numeric))/100))) AS mpc,";
                sql = sql.Replace("__mpc", sss);
            }
            else
            {
                sql = sql.Replace("__mpc", "SUM(vpc*(1 zbroj CAST(REPLACE(porez,',','.') AS numeric)/100)*CAST(REPLACE(kolicina,',','.') AS numeric)) AS mpc,");
            }

            doc = obradiDokument(sql, false);
            ukupno += doc;
            DTrow = dSRliste.Tables[0].NewRow();
            DTrow["naziv"] = sklName;
            DTrow["jmj"] = "Ukupno po otpremnicama";
            DTrow["cijena3"] = doc;
            dSRliste.Tables[0].Rows.Add(DTrow);
            // Prazan red
            DTrow = dSRliste.Tables[0].NewRow();
            dSRliste.Tables[0].Rows.Add(DTrow);

            #endregion OTPREMNICA

            #region OTPIS

            sql = "SELECT" +
                " 'Otpis ' + CAST(otpis_robe.broj AS varchar) AS broj, otpis_robe.datum,'' AS ime_tvrtke," +
                " SUM(CAST(mpc AS numeric)*CAST(REPLACE(kolicina,',','.') AS numeric)) AS mpc," +
                " SUM(vpc*CAST(REPLACE(kolicina,',','.') AS numeric)) AS vpc," +
                " SUM(CAST(nbc AS numeric)*CAST(REPLACE(kolicina,',','.') AS numeric)) AS nbc" +
                " FROM otpis_robe_stavke LEFT JOIN otpis_robe ON otpis_robe.broj=otpis_robe_stavke.broj" +
                " WHERE CAST(otpis_robe.datum AS date) BETWEEN '" + DatumOD.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + DatumDO.ToString("yyyy-MM-dd 23:59:59") + "' AND otpis_robe.id_skladiste=" + sklID +
                " GROUP BY otpis_robe.broj,otpis_robe.datum" +
                " ORDER BY otpis_robe.datum";
            doc = obradiDokument(sql, false);
            ukupno += doc;
            DTrow = dSRliste.Tables[0].NewRow();
            DTrow["naziv"] = sklName;
            DTrow["jmj"] = "Ukupno po otpisima";
            DTrow["cijena3"] = doc;
            dSRliste.Tables[0].Rows.Add(DTrow);
            // Prazan red
            DTrow = dSRliste.Tables[0].NewRow();
            dSRliste.Tables[0].Rows.Add(DTrow);

            #endregion OTPIS

            #region UKUPNO

            DTrow = dSRliste.Tables[0].NewRow();
            DTrow["jmj"] = "Ukupno za " + sklName;
            DTrow["cijena3"] = ukupno;
            dSRliste.Tables[0].Rows.Add(DTrow);
            // Prazan red
            DTrow = dSRliste.Tables[0].NewRow();
            dSRliste.Tables[0].Rows.Add(DTrow);

            #endregion UKUPNO
        }

        private decimal obradiDokument(string sql, bool ulaz)
        {
            DataTable dokument = classSQL.select(sql, "dokument").Tables[0];
            decimal ret = 0;

            foreach (DataRow r in dokument.Rows)
            {
                DataRow DTrow = dSRliste.Tables[0].NewRow();
                DTrow["datum1"] = Convert.ToDateTime(r["datum"]);
                DTrow["naziv"] = r["broj"].ToString();
                DTrow["jmj"] = r["ime_tvrtke"].ToString();
                if (ulaz)
                {
                    DTrow["cijena1"] = Convert.ToDecimal(r[cijena]);
                    DTrow["cijena2"] = 0;
                    ret += Convert.ToDecimal(r[cijena]);
                }
                else
                {
                    DTrow["cijena1"] = 0;
                    DTrow["cijena2"] = Convert.ToDecimal(r[cijena]);
                    ret -= Convert.ToDecimal(r[cijena]);
                }
                dSRliste.Tables[0].Rows.Add(DTrow);
            }
            return ret;
        }
    }
}