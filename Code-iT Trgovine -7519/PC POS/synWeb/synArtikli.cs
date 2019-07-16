using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.synWeb
{
    internal class synArtikli
    {
        private synWeb.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Properties.Settings.Default.domena_za_sinkronizaciju;
        private string poslovnica = "1";

        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synArtikli(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

        #region Pošalji PODATKE

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

            string query = "";
            if (posalji_sve)
            {
                if (MessageBox.Show("Želite poslati artikle?", "Sinkronizacija", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                query = "SELECT * FROM roba";
            }
            else
            {
                query = "SELECT * FROM roba WHERE editirano='1' OR novo='1'";
            }

            DataTable DT = SqlPostgres.select(query, "TABLE").Tables[0];

            int count_sql = 0;
            string sql = "";
            foreach (DataRow r in DT.Rows)
            {
                string oduzmi = "1", jamstvo = "0";

                if (r["oduzmi"].ToString() == "NE") { oduzmi = "0"; }
                if (r["jamstvo"].ToString() != "") { jamstvo = r["jamstvo"].ToString(); }

                string pp = "0";
                if (r["porez_potrosnja"].ToString().Replace(",", ".") != "")
                    pp = r["porez_potrosnja"].ToString().Replace(",", ".");

                sql += "DELETE FROM roba WHERE sifra='" + r["sifra"].ToString().Replace(";", "").Replace("~", "").Replace("&", " and ") + "'" +
                        " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "';~\n" +
                        "" +
                        "INSERT INTO roba (naziv, id_grupa, jm, mpc, sifra, ean, porez, pp, aktivnost, id_podgrupa,border_color,button_style," +
                        "brojcanik, editirano, novo,oib,poslovnica,porezna_grupa," +
                        "oduzmi,nbc,brand,jamstvo,akcija,link_za_slike,id_zemlja_porijekla,id_zemlja_uvoza,id_partner,id_manufacturers,datum_syn" +
                        ") VALUES (" +
                        "'" + r["naziv"].ToString().Replace(";", "").Replace("~", "").Replace("'", "").Replace("\"", "").Replace("&", " and ") + "'," +
                        "'" + r["id_grupa"].ToString() + "'," +
                        "'" + r["jm"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["mpc"].ToString().Replace(",", ".") + "'," +
                        "'" + r["sifra"].ToString().Replace(";", "").Replace("~", "").Replace("&", " and ") + "'," +
                        "'" + r["ean"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["porez"].ToString().Replace(",", ".") + "'," +
                        "'" + pp + "'," +
                        "'1'," +
                        "'" + r["id_podgrupa"].ToString() + "'," +
                        "''," +
                        "''," +
                        "'0'," +
                        "'0'," +
                        "'0'," +
                        "'" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "'," +
                        "'" + poslovnica + "'," +
                        "'O'," +
                        "'" + oduzmi + "'," +
                        "'" + r["nc"].ToString().Replace(",", ".") + "'," +
                        "'" + r["brand"].ToString() + "'," +
                        "'" + jamstvo + "'," +
                        "'" + r["akcija"].ToString() + "'," +
                        "'" + r["link_za_slike"].ToString() + "'," +
                        "'" + r["id_zemlja_porijekla"].ToString() + "'," +
                        "'" + r["id_zemlja_uvoza"].ToString() + "'," +
                        "'" + r["id_partner"].ToString() + "'," +
                        "'" + r["id_manufacturers"].ToString() + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                        ");~";

                count_sql++;
                if (count_sql > 500)
                {
                    sql += "Ł";
                    count_sql = 0;
                }
            }

            if (sql.Length > 4)
            {
                string[] arrSql = sql.Split('Ł');
                bool sql_je_ispravan = true;

                foreach (string ___sql in arrSql)
                {
                    string sql_finish = "sql=" + ___sql.Remove(___sql.Length - 1);
                    //sql_finish = sql_finish.Remove(sql.Length - 1);
                    string[] odg = Pomagala.MyWebRequest(sql_finish + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "include/primam_post_sql_query.php").Split(';');
                    if (odg[0] != "OK" || odg[1] != "1")
                    {
                        sql_je_ispravan = false;
                    }
                }

                if (sql_je_ispravan)
                {
                    sql = "BEGIN;\n";
                    foreach (DataRow r in DT.Rows)
                    {
                        sql += "UPDATE roba SET editirano='0', novo='0' " +
                            "WHERE sifra='" + r["sifra"].ToString() + "';\n";
                    }
                    sql += "COMMIT;\n";
                    SqlPostgres.update(sql);
                }
            }
        }

        #endregion Pošalji PODATKE

        #region PRIMI PODATKE

        public void UzmiPodatkeSaWeba(string posl = null, int id_skladiste = 0, string sifra = null)
        {
            try
            {
                DataTable DTpostavke = SqlPostgres.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
                DataTable DTpodaci_tvrtka = SqlPostgres.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
                DataTable DTposlovnica = SqlPostgres.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];

                poslovnica = "1";
                if (DTposlovnica.Rows.Count > 0)
                {
                    poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
                }

                string sql_za_web = "sql=";
                string query = "SELECT * FROM roba WHERE (novo='1' OR editirano='1') " +
                    "AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "'";
                if (posl != null && id_skladiste != 0 && sifra != null)
                {
                    poslovnica = posl;
                    query = "SELECT * FROM roba WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + posl + "' and sifra in (" + sifra + ")";
                }
                DataTable DT = Pomagala.MyWebRequestXML("sql=" + query + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "uzmi_podatke_xml/web_request.php");

                //*************************LOCAL ARTIKLI******************************
                query = "SELECT * FROM roba;";
                DataTable DTlocal = SqlPostgres.select(query, "loc").Tables[0];
                decimal porez, pp, mpc, vpc, nbc;

                if (DT.Rows.Count > 0)
                {
                    foreach (DataRow r in DT.Rows)
                    {
                        DataRow[] row = DTlocal.Select("sifra='" + r["sifra"].ToString() + "'");
                        string oduzmi = "DA";
                        decimal.TryParse(r["porez"].ToString().Replace(".", ","), out porez);
                        decimal.TryParse(r["pp"].ToString().Replace(".", ","), out pp);
                        decimal.TryParse(r["mpc"].ToString().Replace(".", ","), out mpc);
                        if (r["oduzmi"].ToString() == "0") { oduzmi = "NE"; }
                        decimal.TryParse(r["nbc"].ToString().Replace(".", ","), out nbc);

                        vpc = mpc / (1 + ((porez + pp) / 100));

                        if (row.Length == 0)
                        {
                            query = "INSERT INTO roba (" +
                                        "naziv,id_grupa,jm,vpc,mpc,id_zemlja_porijekla,id_zemlja_uvoza,id_partner,id_manufacturers,sifra," +
                                        "ean,porez,oduzmi,nc,porez_potrosnja,brand,jamstvo,akcija,link_za_slike,id_podgrupa,datum_syn" +
                                        ") VALUES (" +
                                            "'" + r["naziv"].ToString() + "'," +
                                            "'" + r["id_grupa"].ToString() + "'," +
                                            "'" + r["jm"].ToString() + "'," +
                                            "'" + vpc.ToString().Replace(",", ".") + "'," +
                                            "'" + mpc.ToString().Replace(".", ",") + "'," +
                                            "'" + r["id_zemlja_porijekla"].ToString().Replace(".", ",") + "'," +
                                            "'" + r["id_zemlja_uvoza"].ToString().Replace(".", ",") + "'," +
                                            "'" + r["id_partner"].ToString().Replace(".", ",") + "'," +
                                            "'" + r["id_manufacturers"].ToString().Replace(".", ",") + "'," +
                                            "'" + r["sifra"].ToString() + "'," +
                                            "'" + r["ean"].ToString() + "'," +
                                            "'" + porez.ToString("#0.0").Replace(".", ",") + "'," +
                                            "'" + oduzmi + "'," +
                                            "'" + nbc.ToString().Replace(".", ",") + "'," +
                                            "'" + pp.ToString("#0.0").Replace(".", ",") + "'," +
                                            "'" + r["brand"].ToString() + "'," +
                                            "'" + r["jamstvo"].ToString() + "'," +
                                            "'" + r["akcija"].ToString() + "'," +
                                            "'" + r["link_za_slike"].ToString() + "'," +
                                            "'" + r["id_podgrupa"].ToString() + "'," +
                                            "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                                            ");";
                            SqlPostgres.insert(query);
                        }
                        else
                        {
                            query = "UPDATE roba SET " +
                                         " naziv='" + r["naziv"].ToString() + "'," +
                                         " id_grupa='" + r["id_grupa"].ToString() + "'," +
                                         " jm='" + r["jm"].ToString() + "'," +
                                         " vpc='" + vpc.ToString().Replace(",", ".") + "'," +
                                         " mpc='" + mpc.ToString().Replace(".", ",") + "'," +
                                         " id_zemlja_porijekla='" + r["id_zemlja_porijekla"].ToString().Replace(".", ",") + "'," +
                                         " id_zemlja_uvoza='" + r["id_zemlja_uvoza"].ToString().Replace(".", ",") + "'," +
                                         " id_partner='" + r["id_partner"].ToString().Replace(".", ",") + "'," +
                                         " id_manufacturers='" + r["id_manufacturers"].ToString().Replace(".", ",") + "'," +
                                         " ean='" + r["ean"].ToString() + "'," +
                                         " porez='" + porez.ToString("#0.0").Replace(".", ",") + "'," +
                                         " oduzmi='" + oduzmi + "'," +
                                         " nc='" + nbc.ToString().Replace(".", ",") + "'," +
                                         " datum_syn='" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                                         " porez_potrosnja='" + pp.ToString("#0.0").Replace(".", ",") + "'," +
                                         " brand='" + r["brand"].ToString() + "'," +
                                         " jamstvo='" + r["jamstvo"].ToString() + "'," +
                                         " akcija='" + r["akcija"].ToString() + "'," +
                                         " link_za_slike='" + r["link_za_slike"].ToString() + "'," +
                                         " id_podgrupa='" + r["id_podgrupa"].ToString() + "'" +
                                         " WHERE sifra='" + r["sifra"].ToString() + "';";
                            SqlPostgres.insert(query);
                        }

                        //**********************SQL WEB REQUEST***************************************************************************************************************
                        sql_za_web += "UPDATE roba SET novo='0', editirano='0' " +
                            " WHERE sifra='" + r["sifra"].ToString() + "' AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "';~";
                        //**********************SQL WEB REQUEST***************************************************************************************************************
                    }

                    if (sql_za_web.Length > 4)
                    {
                        sql_za_web = sql_za_web.Remove(sql_za_web.Length - 1);
                        string[] odg = Pomagala.MyWebRequest(sql_za_web + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "include/primam_post_sql_query.php").Split(';');
                    }
                }
            }
            catch
            {
            }
        }

        #endregion PRIMI PODATKE
    }
}