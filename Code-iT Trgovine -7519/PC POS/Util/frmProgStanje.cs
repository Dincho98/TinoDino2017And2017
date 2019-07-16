using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Util
{
    public partial class frmProgStanje : Form
    {
        public frmProgStanje()
        {
            InitializeComponent();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text == "q1w2e3r4")
            {
                txtPassword.Enabled = false;
                groupBox1.Visible = true;
            }
        }

        private void frmProgStanje_Load(object sender, EventArgs e)
        {
            DataTable DSMT = classSQL.select("SELECT * FROM skladiste WHERE aktivnost='DA'", "skladiste").Tables[0];
            cbSkl.DataSource = DSMT;
            cbSkl.DisplayMember = "skladiste";
            cbSkl.ValueMember = "id_skladiste";
        }

        public void btnPoravnaj_Click(object sender, EventArgs e)
        {
            classSQL.update(@"UPDATE roba_prodaja SET kolicina='0';
                                            SELECT postavi_kolicinu_sql_funkcija() AS answer;
                                            UPDATE roba_prodaja SET kolicina='0' WHERE sifra IN (SELECT sifra FROM roba WHERE oduzmi='NE');");

            return;

            string sql = "UPDATE roba_prodaja SET kolicina=REPLACE(CAST((coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM kalkulacija_stavke LEFT JOIN kalkulacija ON kalkulacija.broj=kalkulacija_stavke.broj AND kalkulacija.id_skladiste=kalkulacija_stavke.id_skladiste WHERE sifra=roba_prodaja.sifra AND kalkulacija_stavke.id_skladiste=roba_prodaja.id_skladiste ),0) - " +
             "coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM racun_stavke LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa WHERE sifra_robe=roba_prodaja.sifra AND racun_stavke.id_skladiste=roba_prodaja.id_skladiste),0) - " +
             "coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM izdatnica_stavke LEFT JOIN izdatnica ON izdatnica.broj=izdatnica_stavke.broj AND izdatnica.id_izdatnica=izdatnica_stavke.id_izdatnica WHERE sifra=roba_prodaja.sifra AND izdatnica.id_skladiste=roba_prodaja.id_skladiste),0) zbroj " +
             "coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM primka_stavke LEFT JOIN primka ON primka.id_primka=primka_stavke.id_primka WHERE sifra=roba_prodaja.sifra AND primka.id_skladiste=roba_prodaja.id_skladiste),0) - " +
             "coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM faktura_stavke LEFT JOIN fakture ON fakture.broj_fakture=faktura_stavke.broj_fakture WHERE sifra=roba_prodaja.sifra AND fakture.oduzmi_iz_skladista='1' AND faktura_stavke.id_skladiste=roba_prodaja.id_skladiste),0) zbroj " +
             "coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM meduskladisnica_stavke LEFT JOIN meduskladisnica ON meduskladisnica.broj=meduskladisnica_stavke.broj AND meduskladisnica.id_skladiste_od=meduskladisnica_stavke.iz_skladista WHERE sifra=roba_prodaja.sifra AND meduskladisnica.id_skladiste_do=roba_prodaja.id_skladiste),0) - " +
             "coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM meduskladisnica_stavke LEFT JOIN meduskladisnica ON meduskladisnica.broj=meduskladisnica_stavke.broj AND meduskladisnica.id_skladiste_od=meduskladisnica_stavke.iz_skladista WHERE sifra=roba_prodaja.sifra AND meduskladisnica.id_skladiste_od=roba_prodaja.id_skladiste),0) - " +
             "coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM otpremnica_stavke LEFT JOIN otpremnice ON otpremnice.broj_otpremnice=otpremnica_stavke.broj_otpremnice AND otpremnice.id_skladiste=otpremnica_stavke.id_skladiste WHERE otpremnica_stavke.sifra_robe=roba_prodaja.sifra AND otpremnica_stavke.id_skladiste=roba_prodaja.id_skladiste),0) - " +
             "coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM otpis_robe_stavke LEFT JOIN otpis_robe ON otpis_robe.broj=otpis_robe_stavke.broj WHERE otpis_robe_stavke.sifra=roba_prodaja.sifra AND otpis_robe.id_skladiste=roba_prodaja.id_skladiste),0) - " +
             "coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM povrat_robe_stavke LEFT JOIN povrat_robe ON povrat_robe.broj=povrat_robe_stavke.broj WHERE povrat_robe_stavke.sifra=roba_prodaja.sifra AND povrat_robe.id_skladiste=roba_prodaja.id_skladiste),0)  zbroj " +
             "coalesce((SELECT CAST(REPLACE(kolicina,',','.') as NUMERIC) FROM pocetno WHERE sifra=roba_prodaja.sifra AND id_skladiste=roba_prodaja.id_skladiste AND id=(SELECT MAX(id) FROM pocetno WHERE sifra=roba_prodaja.sifra AND id_skladiste=roba_prodaja.id_skladiste)),0) zbroj " +
             "coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM radni_nalog_stavke LEFT JOIN radni_nalog ON radni_nalog.broj_naloga=radni_nalog_stavke.broj_naloga WHERE radni_nalog_stavke.sifra_robe=roba_prodaja.sifra AND radni_nalog_stavke.id_skladiste=roba_prodaja.id_skladiste),0) - " +
            /*
            "coalesce((SELECT " +
            "(SELECT "+
            "   SUM( " +

               "CAST(REPLACE(radni_nalog_stavke.kolicina,',','.') as NUMERIC)* " +

               "(SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM normativi_stavke " +
               "LEFT JOIN normativi ON normativi.broj_normativa = normativi_stavke.broj_normativa  " +
               "WHERE normativi_stavke.sifra_robe = roba_prodaja.sifra AND normativi_stavke.id_skladiste=roba_prodaja.id_skladiste" +
               "AND normativi.sifra_artikla=radni_nalog_stavke.sifra_robe" +
               "))" +

            ") AS Kolicina" +
            "FROM radni_nalog_stavke " +
            "LEFT JOIN radni_nalog ON radni_nalog.broj_naloga=radni_nalog_stavke.broj_naloga " +
            "LEFT JOIN normativi ON normativi.sifra_artikla=radni_nalog_stavke.sifra_robe " +
            "LEFT JOIN normativi_stavke ON normativi_stavke.broj_normativa=normativi.broj_normativa " +
            "WHERE  normativi_stavke.sifra_robe=roba_prodaja.sifra " +
            "GROUP BY normativi_stavke.sifra_robe),0) - "+
            */

            //OVO SVE JE RADNI NALOG IZLAZ sam-sh-224db-bebe
            "coalesce((SELECT " +
            " (SELECT " +
            "   SUM( " +

                "CAST(REPLACE(radni_nalog_stavke.kolicina,',','.') as NUMERIC)* " +

                "(SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM normativi_stavke " +
                "LEFT JOIN normativi ON normativi.broj_normativa = normativi_stavke.broj_normativa  " +
                "WHERE normativi_stavke.sifra_robe = roba_prodaja.sifra AND normativi_stavke.id_skladiste=roba_prodaja.id_skladiste " +
                " AND normativi.sifra_artikla=radni_nalog_stavke.sifra_robe" +
                "))" +

             ") AS Kolicina" +
            " FROM radni_nalog_stavke" +
            " LEFT JOIN radni_nalog ON radni_nalog.broj_naloga=radni_nalog_stavke.broj_naloga" +
            " LEFT JOIN normativi ON normativi.sifra_artikla=radni_nalog_stavke.sifra_robe" +
            " LEFT JOIN normativi_stavke ON normativi_stavke.broj_normativa=normativi.broj_normativa" +
            " WHERE  normativi_stavke.sifra_robe=roba_prodaja.sifra  " +
            " GROUP BY normativi_stavke.sifra_robe),0)-" +

             " coalesce((SELECT SUM(" +
             " CAST(REPLACE(kolicina,',','.') as NUMERIC) * (SELECT SUM(CAST(REPLACE(normativi_stavke.kolicina,',','.') as NUMERIC)) FROM normativi  LEFT JOIN roba ON roba.sifra = normativi.sifra_artikla LEFT JOIN normativi_stavke ON normativi_stavke.broj_normativa=normativi.broj_normativa WHERE roba.oduzmi='NE' AND normativi.sifra_artikla=racun_stavke.sifra_robe AND normativi_stavke.sifra_robe=roba_prodaja.sifra))" +
             " FROM racun_stavke " +
             " LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa " +
             " " +
             " WHERE racun_stavke.sifra_robe IN ((SELECT normativi.sifra_artikla FROM normativi LEFT JOIN normativi_stavke ON normativi_stavke.broj_normativa=normativi.broj_normativa WHERE normativi_stavke.sifra_robe=roba_prodaja.sifra AND normativi_stavke.id_skladiste=roba_prodaja.id_skladiste))),0)" +
             " ) AS varchar),'.',',') ";
            if (chbSkl.Checked) sql += "WHERE id_skladiste=" + cbSkl.SelectedValue.ToString();
            sql += "; UPDATE roba_prodaja SET kolicina='0' WHERE sifra IN (SELECT sifra FROM roba WHERE oduzmi='NE');";

            classSQL.select(sql, "lalal");
            this.Close();
        }
    }
}