using System.Data;

namespace PCPOS.Report
{
    //klasa za dohvaćanje sql za report
    internal class SqlPodaciTvrtke
    {
        /// <summary>
        /// Vraća sql za podatke tvrtke
        /// </summary>
        /// <param name="idKupacGrad">Id grada od kupca</param>
        /// <param name="nazivFakture">Dio sql-a za naziv naslova, npr. " podaci_tvrtka.naziv_fakture," ako vuče naziv iz tablice;
        /// ili " 'OTPREMNICA ' AS naziv_fakture," za otpremnicu</param>
        /// <returns></returns>
        public static string VratiSql(string idKupacGrad, string nazivFakture, string idKupac)
        {
            //if (id_kupac.Trim() == "") id_kupac = "1";

            string r1 = " '0' AS R1,";

            if (nazivFakture.Trim() == "") nazivFakture = " podaci_tvrtka.naziv_fakture,";

            //e sad, vuče naziv fakture iz tablice podaci_tvrtke ALI ako je privatni partner onda mijenja 'R1' u ''
            if (nazivFakture == " podaci_tvrtka.naziv_fakture," && idKupac != "")
            {
                DataTable DTkupac = new DataTable();
                try
                {
                    string kupac = "";
                    if (idKupac != "")
                    {
                        DTkupac = classSQL.select("SELECT vrsta_korisnika FROM partners WHERE id_partner='" + idKupac + "'", "partners").Tables[0];
                    }
                    if (DTkupac.Rows.Count != 0)
                    {
                        kupac = DTkupac.Rows[0]["vrsta_korisnika"].ToString();
                        if (kupac != "1")
                        {
                            DTkupac = classSQL.select_settings("SELECT naziv_fakture FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];
                            if (DTkupac.Rows.Count != 0)
                            {
                                string nazivFaktureIzTablice = DTkupac.Rows[0]["naziv_fakture"].ToString();
                                nazivFaktureIzTablice = nazivFaktureIzTablice.Replace(" R1", "");
                                nazivFaktureIzTablice = nazivFaktureIzTablice.Replace("R1", "");
                                nazivFakture = "'" + nazivFaktureIzTablice.Replace("R1", "") + "' as naziv_fakture,";
                            }
                            r1 = " '0' AS R1,";
                        }
                        else
                        {
                            r1 = " '1' AS R1,";
                        }
                    }
                }
                catch
                {
                }
            }

            string grad_kupac = "";
            DataTable DTgrad_kupac = new DataTable();
            try
            {
                if (idKupacGrad != "")
                {
                    DTgrad_kupac = classSQL.select("SELECT grad, posta FROM grad WHERE id_grad='" + idKupacGrad + "'", "grad").Tables[0];
                }
                if (DTgrad_kupac.Rows.Count != 0)
                {
                    grad_kupac = DTgrad_kupac.Rows[0]["posta"].ToString().Trim() + " " + DTgrad_kupac.Rows[0]["grad"].ToString();
                }
            }
            catch
            {
            }

            string grad_id = "";
            DataTable DTgrad_tvrtke = classSQL.select_settings("SELECT podaci_tvrtka.id_grad FROM podaci_tvrtka", "grad").Tables[0];
            if (DTgrad_tvrtke.Rows.Count != 0)
            {
                grad_id = DTgrad_tvrtke.Rows[0]["id_grad"].ToString().Trim();
            }

            string grad_tvrtke = "";
            DTgrad_tvrtke = classSQL.select("SELECT grad, posta FROM grad WHERE id_grad='" + grad_id + "'", "grad").Tables[0];
            if (DTgrad_tvrtke.Rows.Count != 0)
            {
                grad_tvrtke = DTgrad_tvrtke.Rows[0]["grad"].ToString().Trim() + ' ' + DTgrad_tvrtke.Rows[0]["posta"].ToString().Trim();
            }

            string grad_poslovnica = "";
            DataTable dtGradPoslovnica = classSQL.select_settings("select poslovnica_grad from podaci_tvrtka", "podaci_tvrtka").Tables[0];

            DataTable DTgrad_poslovnica = classSQL.select("SELECT grad, posta FROM grad WHERE grad='" + dtGradPoslovnica.Rows[0][0].ToString() + "' and drzava = '60';", "grad").Tables[0];
            if (DTgrad_poslovnica.Rows.Count != 0)
            {
                grad_poslovnica = DTgrad_poslovnica.Rows[0]["posta"].ToString().Trim() + ' ' + DTgrad_poslovnica.Rows[0]["grad"].ToString().Trim();
            }

            string SQL_post = "Select racun_bool from podaci_tvrtka";
            string provjera_texta = classSQL.select_settings(SQL_post, "provjera_texta").Tables[0].Rows[0][0].ToString();
            string filter = "";
            if (provjera_texta == "1")
            {
                filter = " podaci_tvrtka.text_bottom";
            }
            else
            {
                filter = " podaci_tvrtka.text_racun2 as text_bottom";
            }
            string sql1 = "SELECT " +
                " podaci_tvrtka.ime_tvrtke," +
                " podaci_tvrtka.skraceno_ime," +
                " podaci_tvrtka.oib," +
                " podaci_tvrtka.tel," +
                " podaci_tvrtka.fax," +
                " podaci_tvrtka.mob," +
                " podaci_tvrtka.iban," +
                " podaci_tvrtka.dodatniPodaciHeader," +
                " podaci_tvrtka.swift," +
                " podaci_tvrtka.adresa," +
                " podaci_tvrtka.zr," +
                " podaci_tvrtka.direktor as vl," +
                " '" + grad_kupac + "' AS grad_kupac," +
                " '" + grad_tvrtke + "' AS grad," +
                " podaci_tvrtka.poslovnica_adresa," +
                " '" + grad_poslovnica + "' as poslovnica_grad," +
                " podaci_tvrtka.email," +
                " podaci_tvrtka.pdv_br," +
                " podaci_tvrtka.ime_poslovnice," +
                " podaci_tvrtka.servis_text," +
                nazivFakture +
                r1 + filter +
                " FROM podaci_tvrtka";

            return sql1;
        }
    }
}