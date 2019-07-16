using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.Liste2
{
    public partial class frmListe : Form
    {
        public frmListe()
        {
            InitializeComponent();
        }

        public string broj_dokumenta { get; set; }
        public string dokumenat { get; set; }
        public string ImeForme { get; set; }
        public string blagajnik { get; set; }
        public string skladiste { get; set; }
        public string ducan { get; set; }
        public string kasa { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }

        private void frmListe_Load(object sender, EventArgs e)
        {
            this.Text = ImeForme;

            if (dokumenat == "PROMET")
            {
                PrometKase();
                this.Text = ImeForme;
            }
            else if (dokumenat == "FAK")
            {
                PrometKase_fak();
            }

            this.reportViewer1.RefreshReport();
        }

        private void PrometKase()
        {
            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string WHERE = "WHERE ", whereAvans = "WHERE ";
            string _ducan = "SVI";
            string _zaposlenik = "SVI";
            string _skladiste = "SVE";
            string _kasa = "SVE";

            if (blagajnik != null)
            {
                WHERE += "racuni.id_blagajnik='" + blagajnik + "' AND ";
                whereAvans += "a.id_zaposlenik_izradio = '" + blagajnik + "' AND ";
                try
                {
                    _zaposlenik = classSQL.select("SELECT ime + ' ' + prezime  FROM zaposlenici WHERE id_zaposlenik='" + blagajnik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
                }
                catch (Exception)
                {
                }
            }

            if (ducan != null)
            {
                WHERE += "racuni.id_ducan='" + ducan + "' AND ";
                try
                {
                    _ducan = classSQL.select("SELECT ime_ducana  FROM ducan WHERE id_ducan='" + ducan + "'", "ducan").Tables[0].Rows[0][0].ToString();
                }
                catch (Exception)
                {
                }
            }

            if (kasa != null)
            {
                WHERE += "racuni.id_kasa='" + kasa + "' AND ";
                try
                {
                    _kasa = classSQL.select("SELECT ime_blagajne  FROM blagajna WHERE id_blagajna='" + kasa + "'", "kasa").Tables[0].Rows[0][0].ToString();
                }
                catch (Exception)
                {
                }
            }

            WHERE += "racuni.datum_racuna >='" + datumOD.ToString("yyyy-MM-dd HH:mm:ss") + "' AND racuni.datum_racuna <= '" + datumDO.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            whereAvans += "a.id_nacin_placanja in (1,2) and  a.dat_knj >= '" + datumOD.ToString("yyyy-MM-dd HH:mm:ss") + "' AND a.dat_knj <= '" + datumDO.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

            //            string avansi = "";
            //            if (Class.Postavke.uzmi_avanse_u_promet_kase_POS)
            //            {
            //                avansi = string.Format(@"union all
            //select '-1' as sifra_robe, a.ukupno, case when length(a.artikl) > 0 then a.artikl else a.opis end as naziv,
            //a.osnovica_var as vpc, a.ukupno::money as mpc, replace(p.iznos::character varying(5), '.',',') as porez,
            //1 as kolicna, '0' as rabat, case when a.id_nacin_placanja = 1 then 'G' else case when a.id_nacin_placanja = 2 then 'K' else case when a.id_nacin_placanja = 3 then 'T' else 'O' end end end as nacin_placanja,
            //0 as povratna_naknada, ukupno::money as cijena
            //from avansi as a
            //left join porezi as p on a.id_pdv = p.id_porez
            //where a.id_nacin_placanja in (1,2) and  a.dat_knj >= '{0}' AND a.dat_knj <= '{1}'
            //{2}{3}",
            //                datumOD.ToString("yyyy-MM-dd HH:mm:ss"),
            //                datumDO.ToString("yyyy-MM-dd HH:mm:ss"),
            //                blagA,
            //                dobA);

            //            }

            string sql_liste = @"SELECT SUM(ukupno) AS [cijena1],
SUM(ukupno_gotovina-(ukupno_karticezbrojukupno_gotovinazbrojukupno_bon-ukupno)) AS [cijena2],
SUM(ukupno_kartice) AS [cijena3],
sum(ukupno_bon) as [cijena4]
FROM racuni " +
                WHERE;

            if (Class.Postavke.uzmi_avanse_u_promet_kase_POS)
            {
                sql_liste = string.Format(@"select sum(x.cijena1) as cijena1, sum(x.cijena2) as cijena2, sum(x.cijena3) as cijena3, sum(x.cijena4) as cijena4
from (
    SELECT SUM(ukupno)::numeric  AS [cijena1],
    SUM(ukupno_gotovina-(ukupno_karticezbrojukupno_gotovinazbrojukupno_bon-ukupno))::numeric  AS [cijena2],
    SUM(ukupno_kartice)::numeric  AS [cijena3],
    sum(ukupno_bon)::numeric  as [cijena4]
    FROM racuni
    {0}
    union all
	select sum(a.ukupno) as [cijena1],
	case when a.id_nacin_placanja = 1 then sum(a.ukupno) else 0 end as [cijena2],
	case when a.id_nacin_placanja = 2 then sum(a.ukupno) else 0 end as [cijena3],
	case when a.id_nacin_placanja = 3 then 0 else 0 end as [cijena4]
    from avansi as a
    {1}
    group by a.id_nacin_placanja
) x"
, WHERE, whereAvans);
            }

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            //------------------------------------OVAJ DIO RACUNA RABAT-------------------------------------------------------------------
            DataSet DS = new DataSet();
            string sql_liste1 = "SELECT  " +
                "racun_stavke.sifra_robe," +
                "racun_stavke.mpc," +
                "racun_stavke.rabat," +
                "racun_stavke.kolicina" +
                " FROM racun_stavke " +
                " LEFT JOIN racuni ON racun_stavke.broj_racuna=racuni.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa " +
                WHERE +
                "";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste1).Fill(DS, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste1).Fill(DS, "DTliste");
            }

            decimal mpc = 0;
            decimal rabat = 0;
            decimal kolicina = 0;
            decimal rabat_iznos = 0;

            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                mpc = Convert.ToDecimal(DS.Tables[0].Rows[i]["mpc"].ToString());
                rabat = Convert.ToDecimal(DS.Tables[0].Rows[i]["rabat"].ToString());
                kolicina = Convert.ToDecimal(DS.Tables[0].Rows[i]["kolicina"].ToString());
                rabat_iznos += (mpc * rabat / 100) * kolicina;
                if (DS.Tables[0].Rows[i]["sifra_robe"].ToString() == "!popustABS") rabat_iznos -= Convert.ToDecimal(DS.Tables[0].Rows[i]["mpc"].ToString()) * kolicina;
            }

            //------------------------------------OVAJ DIO RACUNA RABAT----------------kraj--------------------------------------

            string sql_liste_string = "SELECT " +
                " 'Način plaćanja ' AS tbl1," +
                " 'Popust ' AS tbl2," +
                " 'Iznos ' AS tbl3," +
                " 'NOVČANICE ' AS string1," +
                " 'KARTICE ' AS string2," +
                " 'BONOVI ' AS string9," +
                " 'Dućan: " + _ducan + "' AS string3," +
                " 'Kasa: " + _kasa + "' AS string4," +
                " 'Skladište: " + _skladiste + "' AS string5," +
                " 'Blagajnik: " + _zaposlenik + "' AS string6," +
                " 'PROMET KASE PO NAČINIMA PLAĆANJA ' AS naslov," +
                " 'Rabat ukupno: " + Math.Round(rabat_iznos, 2).ToString("#0.00") + "' AS string3," +
                " 'Za razdoblje: " + datumOD + "  -  " + datumDO + "' AS godina" +
                "";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
        }

        private void PrometKase_fak()
        {
            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string WHERE = "WHERE ";
            string _ducan = "SVI";
            string _zaposlenik = "SVI";
            string _skladiste = "SVE";

            if (blagajnik != null)
            {
                WHERE += "fakture.id_zaposlenik_izradio='" + blagajnik + "' AND ";
                try
                {
                    _zaposlenik = classSQL.select("SELECT ime + ' ' + prezime  FROM zaposlenici WHERE id_zaposlenik='" + blagajnik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
                }
                catch (Exception)
                {
                }
            }

            if (skladiste != null)
            {
                WHERE += "faktura_stavke.id_skladiste='" + skladiste + "' AND ";
                try
                {
                    _skladiste = classSQL.select("SELECT skladiste FROM skladiste WHERE id_skladiste='" + skladiste + "'", "skladiste").Tables[0].Rows[0][0].ToString();
                }
                catch (Exception)
                {
                }
            }
            string dodatak = "";
            //WHERE += "racuni.datum_racuna >'" + datumOD + "' AND racuni.datum_racuna <'" + datumDO + "' AND racuni.storno = 'NE'";
            dodatak += " AND fakture.date >'" + datumOD + "' AND fakture.date <'" + datumDO + "' ";

            string gotovina = "Select sum(ukupno) as ukupno From fakture where id_nacin_placanja = '1' " + dodatak;
            DataTable DTgoto = classSQL.select(gotovina, "gotovina").Tables[0];
            string kartice = "Select sum(ukupno) as ukupno From fakture where id_nacin_placanja = '2' " + dodatak;
            DataTable DTkart = classSQL.select(kartice, "gotovina").Tables[0];
            decimal kartice_ = 0;
            decimal gotovina_ = 0;
            if (DTkart.Rows[0]["ukupno"].ToString() != "")
            { kartice_ = Convert.ToDecimal(DTkart.Rows[0]["ukupno"].ToString()); }
            if (DTgoto.Rows[0]["ukupno"].ToString() != "")
            { gotovina_ = Convert.ToDecimal(DTgoto.Rows[0]["ukupno"].ToString()); }

            decimal ukupno = kartice_ + gotovina_;

            string sql_liste = "SELECT '" + ukupno + "' AS [cijena1], '" + gotovina_ + "' AS [cijena2],'" + kartice_ + "' AS [cijena3] ";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            //------------------------------------OVAJ DIO RACUNA RABAT-------------------------------------------------------------------
            WHERE += " fakture.date >'" + datumOD + "' AND fakture.date <'" + datumDO + "' ";
            DataSet DS = new DataSet();
            string sql_liste1 = "SELECT  " +
                "faktura_stavke.vpc," +
                "faktura_stavke.rabat," +
                "faktura_stavke.porez," +
                "faktura_stavke.kolicina" +
                " FROM faktura_stavke " +
                " LEFT JOIN fakture ON faktura_stavke.broj_fakture=fakture.broj_fakture " +
                " AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa " +
                 WHERE + " AND fakture.id_nacin_placanja <> '3' AND fakture.id_nacin_placanja <> '4'" +
                "";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste1).Fill(DS, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste1).Fill(DS, "DTliste");
            }

            decimal mpc = 0;
            decimal vpc = 0;
            decimal pdv = 0;
            decimal rabat = 0;
            decimal kolicina = 0;
            decimal rabat_iznos = 0;

            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                vpc = Convert.ToDecimal(DS.Tables[0].Rows[i]["vpc"].ToString());
                pdv = Convert.ToDecimal(DS.Tables[0].Rows[i]["porez"].ToString());
                rabat = Convert.ToDecimal(DS.Tables[0].Rows[i]["rabat"].ToString());
                kolicina = Convert.ToDecimal(DS.Tables[0].Rows[i]["kolicina"].ToString());
                mpc = vpc * (1 + (pdv / 100));
                rabat_iznos += (mpc * rabat / 100) * kolicina;
            }

            //------------------------------------OVAJ DIO RACUNA RABAT----------------kraj--------------------------------------

            string sql_liste_string = "SELECT " +
                " 'Način plaćanja ' AS tbl1," +
                " 'Popust ' AS tbl2," +
                " 'Iznos ' AS tbl3," +
                " 'NOVČANICE ' AS string1," +
                " 'KARTICE ' AS string2," +
                " 'Dućan: " + _ducan + "' AS string3," +
                " '' AS string4," +
                " 'Skladište: " + _skladiste + "' AS string5," +
                " 'Blagajnik: " + _zaposlenik + "' AS string6," +
                " 'PROMET KASE PO NAČINIMA PLAĆANJA ' AS naslov," +
                " 'Rabat ukupno: " + Math.Round(rabat_iznos, 2).ToString("#0.00") + "' AS string3," +
                " 'Za razdoblje: " + datumOD + "  -  " + datumDO + "' AS godina" +
                "";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
        }
    }
}