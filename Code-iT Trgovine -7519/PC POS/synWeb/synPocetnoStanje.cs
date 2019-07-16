using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.synWeb
{
    internal class synPocetnoStanje
    {
        private synWeb.pomagala_syn Pomagala = new synWeb.pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Properties.Settings.Default.domena_za_sinkronizaciju;
        private string poslovnica = "1";
        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synPocetnoStanje(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

        #region POŠALJI PODATKE

        public void Send()
        {
            DataTable DTpodaci_tvrtka = SqlPostgres.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
            DataTable DTpostavke = SqlPostgres.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            DataTable DTposlovnica = SqlPostgres.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];

            string poslovnica = "1";
            if (DTposlovnica.Rows.Count > 0)
            {
                poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
            }

            //************************************GLEDA NA VARIJABLU posalji_sve ******************
            string query = "";
            if (posalji_sve)
            {
                if (MessageBox.Show("Želite poslati početna stanja?", "Sinkronizacija", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                query = "SELECT * FROM pocetno";
            }
            else
            {
                query = "SELECT * FROM pocetno WHERE editirano='1' OR novo='1'";
            }
            //****************************************************************************

            DataTable DT = SqlPostgres.select(query, "TABLE").Tables[0];

            string sql = "sql=";
            sql = "";
            string tempDel = "";

            //PRVO PROVJERIM SVE DOKUMENTE KOJI SU U PROGRAMU MIJENJANI I BRIŠEM ISTE NA WEBU
            foreach (DataRow r in DT.Rows)
            {
                DateTime dt;
                DateTime.TryParse(r["datum"].ToString(), out dt);

                if (posalji_sve)
                {
                    tempDel = "DELETE FROM pocetno WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' " +
                        "AND poslovnica='" + poslovnica + "' AND godina='" + dt.Year.ToString() + "';~";
                    sql += tempDel;
                    break;
                }
                else
                {
                    if (r["novo"].ToString() == "True")
                    {
                        tempDel = "DELETE FROM pocetno WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' " +
                            " AND poslovnica='" + poslovnica + "' AND godina='" + dt.Year.ToString() + "';~";
                    }
                }

                if (!sql.Contains(tempDel))
                {
                    sql += tempDel;
                }
            }

            int i = 0;
            foreach (DataRow r in DT.Rows)
            {
                DateTime datum;
                DateTime.TryParse(r["datum"].ToString(), out datum);

                sql += "INSERT INTO pocetno (sifra,id_skladiste,nc,novo,editirano,kolicina,prodajna_cijena,datum,godina,oib,poslovnica,povratna_naknada,datum_syn) VALUES (" +
                        "'" + r["sifra"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["id_skladiste"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["nbc"].ToString().Replace(",", ".") + "','0','0'," +
                        "'" + r["kolicina"].ToString().Replace(",", ".") + "'," +
                        "'" + r["prodajna_cijena"].ToString().Replace(",", ".") + "'," +
                        "'" + datum.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        "'" + datum.ToString("yyyy") + "'," +
                        "'" + DTpodaci_tvrtka.Rows[0]["oib"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + poslovnica.Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["povratna_naknada"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "',NOW()" +
                        ");~";

                i++;
                if (i > 200)
                {
                    sql += "Ł";
                    i = 0;
                }
            }

            if (sql.Length > 4)
            {
                bool ispravno = true;
                string[] ss = sql.Split('Ł');
                foreach (string s in ss)
                {
                    sql = s.Remove(s.Length - 1);

                    //ŠALJE NA WEB i DOBIVAM ODG
                    string[] odg = Pomagala.MyWebRequest("sql=" + sql + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "include/primam_post_sql_query.php").Split(';');

                    if (odg[0] != "OK" || odg[1] != "1")
                        ispravno = false;
                }

                if (ispravno)
                {
                    sql = "UPDATE pocetno SET editirano='0', novo='0';";
                    SqlPostgres.update(sql);
                }
            }
        }

        #endregion POŠALJI PODATKE

        #region PRIMI PODATKE

        public void UzmiPodatkeSaWeba()
        {
            DataTable DTpodaci_tvrtka = SqlPostgres.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
            DataTable DTposlovnica = SqlPostgres.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];
            string sql_za_web = "sql=";

            poslovnica = "1";
            string id_poslovnica = "1";
            if (DTposlovnica.Rows.Count > 0)
            {
                poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
                id_poslovnica = DTposlovnica.Rows[0]["id_ducan"].ToString();
            }

            string sql = "SELECT * FROM pocetno " +
                " WHERE  (novo='1' OR editirano='1') AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' " +
                " AND poslovnica='" + poslovnica + "';";
            DataTable DT = Pomagala.MyWebRequestXML("sql=" + sql + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "uzmi_podatke_xml/web_request.php");

            if (DT.Rows.Count > 0)
            {
                sql = "BEGIN;";
                foreach (DataRow r in DT.Rows)
                {
                    if (r["sifra"].ToString() != "")
                    {
                        decimal nbc, kol, mpc, povratna_naknada;
                        decimal.TryParse(r["nc"].ToString().Replace(".", ","), out nbc);
                        decimal.TryParse(r["kolicina"].ToString().Replace(".", ","), out kol);
                        decimal.TryParse(r["prodajna_cijena"].ToString().Replace(".", ","), out mpc);
                        decimal.TryParse(r["povratna_naknada"].ToString().Replace(".", ","), out povratna_naknada);

                        DateTime DateT;
                        DateTime.TryParse(r["datum"].ToString(), out DateT);

                        sql += "DELETE FROM pocetno WHERE sifra='" + r["sifra"].ToString() + "' AND id_skladiste='" + r["id_skladiste"].ToString() + "';" +
                             " INSERT INTO pocetno (sifra,id_skladiste,novo,editirano,kolicina,nbc,datum,prodajna_cijena,povratna_naknada,datum_syn,porez) VALUES (" +
                             " '" + r["sifra"].ToString().Replace(";", "").Replace("~", "") + "'," +
                             " '" + r["id_skladiste"].ToString().Replace(";", "").Replace("~", "") + "'," +
                             " '0'," +
                             " '0'," +
                             " '" + Math.Round(kol, 4).ToString().Replace(",", ".") + "'," +
                             " '" + Math.Round(nbc, 4).ToString().Replace(",", ".") + "'," +
                             " '" + DateT.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                             " '" + Math.Round(mpc, 2).ToString().Replace(",", ".") + "'," +
                             " '" + Math.Round(povratna_naknada, 2).ToString().Replace(",", ".") + "'" +
                             ",now()," +
                             "COALESCE((SELECT (REPLACE(porez,',','.')::decimal)::int FROM roba WHERE sifra='" + r["sifra"].ToString() + "' LIMIT 1),0)" +
                             ");";
                    }

                    //**********************SQL WEB REQUEST***************************************************************************
                    sql_za_web += "UPDATE pocetno SET novo='0',editirano='0'" +
                        " WHERE id='" + r["id"].ToString() + "'" +
                        " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "';~";
                    //**********************SQL WEB REQUEST***************************************************************************
                }

                sql += " COMMIT;";
                classSQL.insert(sql);

                if (sql_za_web.Length > 4)
                {
                    sql_za_web = sql_za_web.Remove(sql_za_web.Length - 1);
                    string[] odg = Pomagala.MyWebRequest(sql_za_web + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "include/primam_post_sql_query.php").Split(';');
                }
            }
        }

        #endregion PRIMI PODATKE
    }
}