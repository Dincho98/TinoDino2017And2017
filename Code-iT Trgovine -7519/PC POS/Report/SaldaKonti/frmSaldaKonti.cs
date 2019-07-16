using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.SaldaKonti
{
    public partial class frmSaldaKonti : Form
    {
        public frmSaldaKonti()
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
            tdOdDatuma.Value = new DateTime(DateTime.Now.Year, 01, 01, 00, 00, 00);

            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            dSRpodaciTvrtke.Tables[0].Rows[0]["naziv_fakture"] = "";

            this.reportViewer1.RefreshReport();
            this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
        }

        private DataSet DSpartner;

        private void txtSifra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtSifra.Text == "")
                {
                    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                    partnerTrazi.ShowDialog();
                    if (Properties.Settings.Default.id_partner != "")
                    {
                        DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                        if (DSpartner.Tables[0].Rows.Count > 0)
                        {
                            txtSifra.Text = DSpartner.Tables[0].Rows[0]["id_partner"].ToString();
                            txtNaziv.Text = DSpartner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtSifra.Select();
                        }
                    }
                    else
                    {
                        txtSifra.Select();
                        return;
                    }
                }

                string Str = txtSifra.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtSifra.Text = "0";
                }
                DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner='" + txtSifra.Text + "'", "partners");
                if (DSpartner.Tables[0].Rows.Count > 0)
                {
                    txtNaziv.Text = DSpartner.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void btnPartner_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DSpartner = new DataSet();
                DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (DSpartner.Tables[0].Rows.Count > 0)
                {
                    txtSifra.Text = DSpartner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtNaziv.Text = DSpartner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            string uvijet_partner = "";
            string uvijet_datum = "";

            if (txtSifra.Text != "")
            {
                uvijet_partner = string.Format(@"
AND SaldaKonti.id_partner = '{0}'
AND partners.id_partner = '{1}'",
    txtSifra.Text, txtSifra.Text);
            }

            uvijet_datum = string.Format(@"
AND SaldaKonti.datum >= '{0:yyyy-MM-dd HH:mm:ss}'
AND SaldaKonti.datum <= '{1:yyyy-MM-dd HH:mm:ss}'",
    tdOdDatuma.Value, tdDoDatuma.Value);

            dSsaldaKonti.Clear();

            string sql = string.Format(@"SELECT *
FROM (
    SELECT p.ime_tvrtke, 0 AS broj, 'POČETNO STANJE' AS dokumenat, pdp.datum_dokumenta AS datum, pdp.datum_dvo AS datum2,
    pdp.id_partner AS id_partner, pdp.otvoreno, pdp.potrazuje, 0 AS id_ducan, 0 AS id_kasa, 0 AS iznos, pdp.uplaceno, pdp.isplaceno, 0 AS sort
    FROM pocetno_dugovanje_partnera pdp
    LEFT JOIN partners p ON pdp.id_partner = p.id_partner
    WHERE pdp.datum_dokumenta >= '{2:yyyy-MM-dd HH:mm:ss}'
    AND pdp.datum_dokumenta <= '{3:yyyy-MM-dd HH:mm:ss}'
    AND CASE WHEN '{4}' = '0' THEN 1=1 ELSE pdp.id_partner = '{4}' END

    UNION ALL

    SELECT partners.ime_tvrtke, SaldaKonti.*,
    '0' AS iznos,
    (SELECT COALESCE(SUM(salda_konti.uplaceno), 0) AS uplaceno
        FROM salda_konti
        WHERE salda_konti.broj_dokumenta = SaldaKonti.broj
            AND SaldaKonti.dokumenat = salda_konti.dokumenat
            AND SaldaKonti.id_ducan = salda_konti.id_ducan
            AND SaldaKonti.id_kasa = salda_konti.id_kasa
            AND extract(year from SaldaKonti.datum) = salda_konti.godina) AS uplaceno,

    (SELECT COALESCE(SUM(salda_konti.isplaceno), 0) AS isplaceno
        FROM salda_konti
        WHERE salda_konti.broj_dokumenta = SaldaKonti.broj
            AND SaldaKonti.dokumenat = salda_konti.dokumenat
            AND SaldaKonti.id_ducan = salda_konti.id_ducan
            AND SaldaKonti.id_kasa = salda_konti.id_kasa
            AND extract(year from SaldaKonti.datum) = salda_konti.godina) AS isplaceno, 1 as sort

    FROM
        (
            SELECT broj_fakture as broj, 'FAKTURA' AS dokumenat, date AS datum, datedvo AS datum2, fakture.id_odrediste AS id_partner, CAST(fakture.ukupno AS NUMERIC) AS otvoreno, CAST('0' AS numeric) AS potrazuje, fakture.id_ducan, fakture.id_kasa
            FROM fakture

            UNION ALL

            SELECT broj as broj, 'FAKTURA BEZ ROBE' AS dokumenat, datum AS datum, datum_dvo AS datum2, ifb.odrediste AS id_partner, ifb.ukupno AS otvoreno, CAST('0' AS numeric) AS potrazuje, '0' as id_ducan, '0' AS id_kasa
            FROM ifb

            UNION ALL

            SELECT broj as broj, 'ULAZNA FAKTURA' AS dokumenat, datum_izvrsenja AS datum, datum_izvrsenja AS datum2, ulazna_faktura.primatelj_sifra AS id_partner, CAST('0' AS NUMERIC) AS otvoreno, ulazna_faktura.iznos AS potrazuje, '0' as id_ducan, '0' AS id_kasa
            FROM ulazna_faktura

            UNION ALL

            SELECT broj as broj, 'KALKULACIJA' AS dokumenat, datum AS datum, datum AS datum2, kalkulacija.id_partner, CAST('0' AS NUMERIC) AS otvoreno, CAST(kalkulacija.fakturni_iznos AS NUMERIC) AS potrazuje, '0' as id_ducan, '0' AS id_kasa
            FROM kalkulacija

        ) AS SaldaKonti
    LEFT JOIN partners ON partners.id_partner = SaldaKonti.id_partner
    WHERE partners.id_partner IS NOT NULL{0}{1}
) x {5}
ORDER BY x.sort, x.dokumenat ASC;", uvijet_datum, uvijet_partner, tdOdDatuma.Value, tdDoDatuma.Value, txtSifra.Text == "" ? "0" : txtSifra.Text, FilterUvjet());

            //Postoje neki zapisi koji nisu niti placeni, niti djelomicno placeni, niti neplaceni.
            if (checkBoxNesvrstani.Checked)
            {
                sql = string.Format(@"(SELECT DISTINCT *
FROM (
    SELECT p.ime_tvrtke, 0 AS broj, 'POČETNO STANJE' AS dokumenat, pdp.datum_dokumenta AS datum, pdp.datum_dvo AS datum2,
    pdp.id_partner AS id_partner, pdp.otvoreno, pdp.potrazuje, 0 AS id_ducan, 0 AS id_kasa, 0 AS iznos, pdp.uplaceno, pdp.isplaceno, 0 AS sort
    FROM pocetno_dugovanje_partnera pdp
    LEFT JOIN partners p ON pdp.id_partner = p.id_partner
    WHERE pdp.datum_dokumenta >= '2019-01-01 00:00:00'
    AND pdp.datum_dokumenta <= '2019-07-03 10:09:13'
    AND CASE WHEN '0' = '0' THEN 1=1 ELSE pdp.id_partner = '0' END

    UNION ALL

    SELECT partners.ime_tvrtke, SaldaKonti.*,
    '0' AS iznos,
    (SELECT COALESCE(SUM(salda_konti.uplaceno), 0) AS uplaceno
        FROM salda_konti
        WHERE salda_konti.broj_dokumenta = SaldaKonti.broj
            AND SaldaKonti.dokumenat = salda_konti.dokumenat
            AND SaldaKonti.id_ducan = salda_konti.id_ducan
            AND SaldaKonti.id_kasa = salda_konti.id_kasa
            AND extract(year from SaldaKonti.datum) = salda_konti.godina) AS uplaceno,

    (SELECT COALESCE(SUM(salda_konti.isplaceno), 0) AS isplaceno
        FROM salda_konti
        WHERE salda_konti.broj_dokumenta = SaldaKonti.broj
            AND SaldaKonti.dokumenat = salda_konti.dokumenat
            AND SaldaKonti.id_ducan = salda_konti.id_ducan
            AND SaldaKonti.id_kasa = salda_konti.id_kasa
            AND extract(year from SaldaKonti.datum) = salda_konti.godina) AS isplaceno, 1 as sort
	FROM
        (
            SELECT broj_fakture as broj, 'FAKTURA' AS dokumenat, date AS datum, datedvo AS datum2, fakture.id_odrediste AS id_partner, CAST(fakture.ukupno AS NUMERIC) AS otvoreno, CAST('0' AS numeric) AS potrazuje, fakture.id_ducan, fakture.id_kasa
            FROM fakture

            UNION ALL

            SELECT broj as broj, 'FAKTURA BEZ ROBE' AS dokumenat, datum AS datum, datum_dvo AS datum2, ifb.odrediste AS id_partner, ifb.ukupno AS otvoreno, CAST('0' AS numeric) AS potrazuje, '0' as id_ducan, '0' AS id_kasa
            FROM ifb

            UNION ALL 

            SELECT broj as broj, 'ULAZNA FAKTURA' AS dokumenat, datum_izvrsenja AS datum, datum_izvrsenja AS datum2, ulazna_faktura.primatelj_sifra AS id_partner, CAST('0' AS NUMERIC) AS otvoreno, ulazna_faktura.iznos AS potrazuje, '0' as id_ducan, '0' AS id_kasa
            FROM ulazna_faktura

            UNION ALL

            SELECT broj as broj, 'KALKULACIJA' AS dokumenat, datum AS datum, datum AS datum2, kalkulacija.id_partner, CAST('0' AS NUMERIC) AS otvoreno, CAST(kalkulacija.fakturni_iznos AS NUMERIC) AS potrazuje, '0' as id_ducan, '0' AS id_kasa
            FROM kalkulacija

        ) AS SaldaKonti
    LEFT JOIN partners ON partners.id_partner = SaldaKonti.id_partner
    WHERE partners.id_partner IS NOT NULL
AND SaldaKonti.datum >= '2019-01-01 00:00:00'
AND SaldaKonti.datum <= '2019-07-03 10:09:13'
) x 
ORDER BY x.sort, x.dokumenat ASC)
 EXCEPT 
(SELECT DISTINCT *
FROM (
    SELECT p.ime_tvrtke, 0 AS broj, 'POČETNO STANJE' AS dokumenat, pdp.datum_dokumenta AS datum, pdp.datum_dvo AS datum2,
    pdp.id_partner AS id_partner, pdp.otvoreno, pdp.potrazuje, 0 AS id_ducan, 0 AS id_kasa, 0 AS iznos, pdp.uplaceno, pdp.isplaceno, 0 AS sort
    FROM pocetno_dugovanje_partnera pdp
    LEFT JOIN partners p ON pdp.id_partner = p.id_partner
    WHERE pdp.datum_dokumenta >= '2019-01-01 00:00:00'
    AND pdp.datum_dokumenta <= '2019-07-03 10:09:13'
    AND CASE WHEN '0' = '0' THEN 1=1 ELSE pdp.id_partner = '0' END

    UNION ALL

    SELECT partners.ime_tvrtke, SaldaKonti.*,
    '0' AS iznos,
    (SELECT COALESCE(SUM(salda_konti.uplaceno), 0) AS uplaceno
        FROM salda_konti
        WHERE salda_konti.broj_dokumenta = SaldaKonti.broj
            AND SaldaKonti.dokumenat = salda_konti.dokumenat
            AND SaldaKonti.id_ducan = salda_konti.id_ducan
            AND SaldaKonti.id_kasa = salda_konti.id_kasa
            AND extract(year from SaldaKonti.datum) = salda_konti.godina) AS uplaceno,

    (SELECT COALESCE(SUM(salda_konti.isplaceno), 0) AS isplaceno
        FROM salda_konti
        WHERE salda_konti.broj_dokumenta = SaldaKonti.broj
            AND SaldaKonti.dokumenat = salda_konti.dokumenat
            AND SaldaKonti.id_ducan = salda_konti.id_ducan
            AND SaldaKonti.id_kasa = salda_konti.id_kasa
            AND extract(year from SaldaKonti.datum) = salda_konti.godina) AS isplaceno, 1 as sort
	FROM
        (
            SELECT broj_fakture as broj, 'FAKTURA' AS dokumenat, date AS datum, datedvo AS datum2, fakture.id_odrediste AS id_partner, CAST(fakture.ukupno AS NUMERIC) AS otvoreno, CAST('0' AS numeric) AS potrazuje, fakture.id_ducan, fakture.id_kasa
            FROM fakture

            UNION ALL

            SELECT broj as broj, 'FAKTURA BEZ ROBE' AS dokumenat, datum AS datum, datum_dvo AS datum2, ifb.odrediste AS id_partner, ifb.ukupno AS otvoreno, CAST('0' AS numeric) AS potrazuje, '0' as id_ducan, '0' AS id_kasa
            FROM ifb

            UNION ALL 

            SELECT broj as broj, 'ULAZNA FAKTURA' AS dokumenat, datum_izvrsenja AS datum, datum_izvrsenja AS datum2, ulazna_faktura.primatelj_sifra AS id_partner, CAST('0' AS NUMERIC) AS otvoreno, ulazna_faktura.iznos AS potrazuje, '0' as id_ducan, '0' AS id_kasa
            FROM ulazna_faktura

            UNION ALL

            SELECT broj as broj, 'KALKULACIJA' AS dokumenat, datum AS datum, datum AS datum2, kalkulacija.id_partner, CAST('0' AS NUMERIC) AS otvoreno, CAST(kalkulacija.fakturni_iznos AS NUMERIC) AS potrazuje, '0' as id_ducan, '0' AS id_kasa
            FROM kalkulacija

        ) AS SaldaKonti
    LEFT JOIN partners ON partners.id_partner = SaldaKonti.id_partner
    WHERE partners.id_partner IS NOT NULL
AND SaldaKonti.datum >= '2019-01-01 00:00:00'
AND SaldaKonti.datum <= '2019-07-03 10:09:13'
) x WHERE ((x.otvoreno!=0 AND x.otvoreno - x.uplaceno = 0) OR (x.otvoreno - x.uplaceno > 0 AND x.uplaceno!=0) OR (x.uplaceno=0 AND x.otvoreno!=0))
ORDER BY x.sort, x.dokumenat ASC);", uvijet_datum, uvijet_partner, tdOdDatuma.Value, tdDoDatuma.Value, txtSifra.Text == "" ? "0" : txtSifra.Text, FilterUvjet());

            }

            classSQL.NpgAdatpter(sql).Fill(dSsaldaKonti, "DTsaldaKonti");

            dSRpodaciTvrtke.Tables[0].Rows[0]["naziv_fakture"] = "FILTER-> OD DATUMA: " + tdOdDatuma.Value.ToString("dd-MM-yyyy H:mm:ss") + "  DO DATUMA: " + tdDoDatuma.Value.ToString("dd-MM-yyyy H:mm:ss");

            foreach (DataRow r in dSsaldaKonti.Tables[0].Rows)
            {
                decimal duguje, uplaceno, potrazuje, isplaceno, iznos;
                decimal.TryParse(r["otvoreno"].ToString(), out duguje);
                decimal.TryParse(r["uplaceno"].ToString(), out uplaceno);
                decimal.TryParse(r["potrazuje"].ToString(), out potrazuje);
                decimal.TryParse(r["isplaceno"].ToString(), out isplaceno);
                iznos = duguje - uplaceno - potrazuje + isplaceno;

                r["iznos"] = duguje - uplaceno - potrazuje + isplaceno;
            }

            this.reportViewer1.RefreshReport();
            this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
        }

        //Uvjeti za filter
        private string FilterUvjet()
        {
            if (checkBoxPlaceni.Checked)
                return "WHERE x.otvoreno!=0 AND x.otvoreno - x.uplaceno = 0";
            if (checkBoxDjelomicnoPlaceni.Checked)
                return "WHERE x.otvoreno - x.uplaceno > 0 AND x.uplaceno!=0";
            if (checkBoxNeplaceni.Checked)
                return "WHERE x.uplaceno=0 AND x.otvoreno!=0";

            return "";
        }

        //Filter
        private void checkBoxPlaceni_CheckedChanged(object sender, EventArgs e)
        {
            //Može biti odabran samo 1 filter: Plačeno, Djelomično plačeno, Neplačeno ili Nesvrstani
            if (checkBoxPlaceni.CheckState == CheckState.Checked)
            {
                checkBoxDjelomicnoPlaceni.CheckState = CheckState.Unchecked;
                checkBoxNeplaceni.CheckState = CheckState.Unchecked;
                checkBoxNesvrstani.CheckState = CheckState.Unchecked;
            }

        }

        private void checkBoxDjelomicnoPlaceni_CheckedChanged(object sender, EventArgs e)
        {
            //Može biti odabran samo 1 filter: Plačeno, Djelomično plačeno, Neplačeno ili Nesvrstani
            if (checkBoxDjelomicnoPlaceni.CheckState == CheckState.Checked)
            {
                checkBoxPlaceni.CheckState = CheckState.Unchecked;
                checkBoxNeplaceni.CheckState = CheckState.Unchecked;
                checkBoxNesvrstani.CheckState = CheckState.Unchecked;
            }


        }

        private void checkBoxNeplaceni_CheckedChanged(object sender, EventArgs e)
        {
            //Može biti odabran samo 1 filter: Plačeno, Djelomično plačeno, Neplačeno ili Nesvrstani
            if (checkBoxNeplaceni.CheckState == CheckState.Checked)
            {
                checkBoxPlaceni.CheckState = CheckState.Unchecked;
                checkBoxDjelomicnoPlaceni.CheckState = CheckState.Unchecked;
                checkBoxNesvrstani.CheckState = CheckState.Unchecked;
            }
        }

        private void checkBoxNesvrstani_CheckedChanged(object sender, EventArgs e)
        {
            //Može biti odabran samo 1 filter: Plačeno, Djelomično plačeno, Neplačeno ili Nesvrstani
            if (checkBoxNesvrstani.CheckState == CheckState.Checked)
            {
                checkBoxPlaceni.CheckState = CheckState.Unchecked;
                checkBoxDjelomicnoPlaceni.CheckState = CheckState.Unchecked;
                checkBoxNeplaceni.CheckState = CheckState.Unchecked;
            }
        }
    }
}