using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.synWeb
{
    internal class synRobaProdaja
    {
        private synWeb.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Properties.Settings.Default.domena_za_sinkronizaciju;
        private string poslovnica = "1";

        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synRobaProdaja(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

        #region SALJEM PODATKE

        public void Send()
        {
            DataTable DTpodaci_tvrtka = SqlPostgres.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
            DataTable DTpostavke = SqlPostgres.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            DataTable DTposlovnica = SqlPostgres.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];

            string query = "";
            if (posalji_sve)
            {
                if (MessageBox.Show("Želite poslati artikle?", "Sinkronizacija", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                query = "SELECT roba_prodaja.*,roba.naziv, roba.jm FROM roba_prodaja LEFT JOIN roba ON roba.sifra=roba_prodaja.sifra";
            }
            else
            {
                query = "SELECT roba_prodaja.*,roba.naziv, roba.jm FROM roba_prodaja LEFT JOIN roba ON roba.sifra=roba_prodaja.sifra WHERE roba_prodaja.editirano='1' OR roba_prodaja.novo='1'";
            }

            DataTable DT = SqlPostgres.select(query, "TABLE").Tables[0];

            poslovnica = "1";
            if (DTposlovnica.Rows.Count > 0)
            {
                poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
            }

            int count_sql = 0;
            string sql = "";
            foreach (DataRow r in DT.Rows)
            {
                decimal _mpc, _vpc, _porez, _pp;

                //int.TryParse(r["aktivnost"].ToString(), out _aktivnost);
                //int.TryParse(r["id_partner"].ToString(), out id_partner);
                decimal.TryParse(r["vpc"].ToString().Replace(".", ","), out _vpc);
                decimal.TryParse(r["porez"].ToString().Replace(".", ","), out _porez);
                decimal.TryParse(r["porez_potrosnja"].ToString().Replace(".", ","), out _pp);

                _mpc = Math.Round((_vpc * (1 + ((_porez + _pp) / 100))), 2);

                sql += "DELETE FROM roba_prodaja WHERE sifra='" + r["sifra"].ToString().Replace(";", "").Replace("~", "").Replace("&", " and ") + "'" +
                    " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString().Replace(";", "").Replace("~", "") + "'" +
                    " AND poslovnica='" + poslovnica.Replace(";", "").Replace("~", "") + "' and id_skladiste = '" + r["id_skladiste"].ToString().Replace(";", "").Replace("~", "").Replace("&", " and ") + "';~\n" +
                    "" +
                    "INSERT INTO roba_prodaja (id_skladiste, kolicina, nc, vpc, sifra, porez_potrosnja, " +
                    "id_grupa, id_podgrupa, mjera, aktivnost, povratna_naknada, poticajna_naknada, ulazni_porez, " +
                    "izlazni_porez, naziv, mpc, id_partner, kolicina_predracun, cijena2, u_pakiranju, brojcanik," +
                    " oib, poslovnica, novo, editirano,datum_syn) VALUES " +
                    "(" +
                    "'" + r["id_skladiste"].ToString() + "'," +
                    "'" + r["kolicina"].ToString().Replace(",", ".") + "'," +
                    "'" + r["nc"].ToString().Replace(",", ".") + "'," +
                    "'" + r["vpc"].ToString().Replace(",", ".") + "'," +
                    "'" + r["sifra"].ToString().Replace(";", "").Replace("~", "").Replace("&", " and ") + "'," +
                    "'" + _pp.ToString().Replace(",", ".") + "'," +
                    "'0'," +
                    "'0'," +
                    "'" + r["jm"].ToString() + "'," +
                    "'1'," +
                    "'0'," +
                    "'0'," +
                    "'" + _porez.ToString().Replace(",", ".") + "'," +
                    "'" + _porez.ToString().Replace(",", ".") + "'," +
                    "'" + r["naziv"].ToString().Replace(";", "").Replace("~", "").Replace("&", " and ") + "'," +
                    "'" + _mpc.ToString().Replace(",", ".") + "'," +
                    "'0'," +
                    "'0'," +
                    "'0'," +
                    "'0'," +
                    "'0'," +
                    "'" + DTpodaci_tvrtka.Rows[0]["oib"].ToString().Replace(";", "").Replace("~", "") + "'," +
                    "'" + poslovnica.Replace(";", "").Replace("~", "") + "'," +
                    "'0'," +
                    "'0'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                    ");~";

                count_sql++;
                if (count_sql > 100)
                {
                    sql += "Ł";
                    count_sql = 0;
                }
            }

            if (sql.Length > 4)
            {
                //sql = sql.Remove(sql.Length - 1);

                string[] arrSql = sql.Split('Ł');
                bool sql_je_ispravan = true;

                foreach (string ___sql in arrSql)
                {
                    if (___sql.Length > 0)
                    {
                        string sql_finish = "sql=" + ___sql.Remove(___sql.Length - 1);
                        string[] odg = Pomagala.MyWebRequest(sql_finish + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "include/primam_post_sql_query.php").Split(';');
                        if (odg[0] != "OK" || odg[1] != "1")
                        {
                            sql_je_ispravan = false;
                        }
                    }
                }

                if (sql_je_ispravan)
                {
                    foreach (DataRow r in DT.Rows)
                    {
                        sql = "UPDATE roba_prodaja SET editirano='0', novo='0' " +
                           "WHERE sifra='" + r["sifra"].ToString() + "' AND id_skladiste='" + r["id_skladiste"].ToString() + "';";

                        SqlPostgres.update(sql);
                    }
                }
            }
        }

        #endregion SALJEM PODATKE

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
                string query = "SELECT * FROM roba_prodaja WHERE (novo='1' OR editirano='1') " +
                    " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "';";

                if (posl != null && id_skladiste != 0 && sifra != null)
                {
                    poslovnica = posl;
                    query = "SELECT * FROM roba_prodaja WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + posl + "' and id_skladiste = '" + id_skladiste + "' and sifra in (" + sifra + ")";
                }

                DataTable DT = Pomagala.MyWebRequestXML("sql=" + query + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "uzmi_podatke_xml/web_request.php");

                query = "BEGIN;\n";
                if (DT.Rows.Count > 0)
                {
                    foreach (DataRow r in DT.Rows)
                    {
                        decimal _porez, _kolicina, _nbc, _vpc, _pp;
                        decimal.TryParse(r["izlazni_porez"].ToString().Replace(".", ","), out _porez);
                        decimal.TryParse(r["kolicina"].ToString().Replace(".", ","), out _kolicina);
                        decimal.TryParse(r["nc"].ToString().Replace(".", ","), out _nbc);
                        decimal.TryParse(r["vpc"].ToString().Replace(".", ","), out _vpc);
                        decimal.TryParse(r["porez_potrosnja"].ToString().Replace(".", ","), out _pp);

                        query += "DELETE FROM roba_prodaja WHERE sifra='" + r["sifra"].ToString() + "' AND id_skladiste='" + r["id_skladiste"].ToString() + "';\n" +

                            "INSERT INTO roba_prodaja (" +
                                        "id_skladiste,kolicina,nc,vpc,porez,sifra,porez_potrosnja,novo,editirano,datum_syn" +
                                        ") VALUES (" +
                                            "'" + r["id_skladiste"].ToString() + "'," +
                                            "'" + _kolicina.ToString().Replace(".", ",") + "'," +
                                            "'" + _nbc.ToString().Replace(".", ",") + "'," +
                                            "'" + _vpc.ToString().Replace(",", ".") + "'," +
                                            "'" + _porez.ToString().Replace(".", ",") + "'," +
                                            "'" + r["sifra"].ToString() + "'," +
                                            "'" + _pp.ToString().Replace(".", ",") + "'," +
                                            "'0'," +
                                            "'0'," +
                                            "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                                            ");\n";

                        //**********************SQL WEB REQUEST***************************************
                        sql_za_web += "UPDATE roba_prodaja SET novo='0', editirano='0' " +
                            " WHERE sifra='" + r["sifra"].ToString() + "'" +
                            " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "'" +
                            " AND poslovnica='" + poslovnica + "';~";
                        //**********************SQL WEB REQUEST***************************************
                    }

                    query += "COMMIT;";
                    if (sql_za_web.Length > 4)
                    {
                        classSQL.insert(query);

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