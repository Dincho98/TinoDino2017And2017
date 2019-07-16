using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.synWeb
{
    internal class synMeduSkladiste
    {
        private synWeb.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Properties.Settings.Default.domena_za_sinkronizaciju;
        private string poslovnica = "1";
        private string id_poslovnica = "1";
        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synMeduSkladiste(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

        #region POŠALJI PODATKE

        public void Send()
        {
            DataTable DTpodaci_tvrtka = SqlPostgres.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
            DataTable DTpostavke = SqlPostgres.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            DataTable DTposlovnica = SqlPostgres.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];

            poslovnica = "1";
            id_poslovnica = "1";
            if (DTposlovnica.Rows.Count > 0)
            {
                poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
                id_poslovnica = DTposlovnica.Rows[0]["id_ducan"].ToString();
            }

            //************************************GLEDA NA VARIJABLU posalji_sve ******************
            string filter = "";
            if (posalji_sve)
            {
                if (MessageBox.Show("Želite poslati međuskladišnice?", "Sinkronizacija", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                filter = "";
            }
            else
            {
                filter = "  WHERE (m.editirano = '1' OR m.novo = '1')";
            }
            //****************************************************************************

            string query = "SELECT REPLACE(m.broj, ',', '.')::NUMERIC AS broj, " +
            "REPLACE(m.godina, ',', '.')::NUMERIC AS godina, " +
            "m.id_skladiste_od AS skladiste_od, " +
            "m.id_skladiste_do AS skladiste_do, " +
            "m.datum AS datum, " +
            "m.id_izradio AS id_izradio, " +
            "m.napomena AS napomena, " +
            "COALESCE(ms.sifra, '') AS sifra, " +
            "COALESCE(ms.vpc::NUMERIC, 0) AS vpc, " +
            "COALESCE(ms.mpc::numeric, 0) AS mpc, " +
            "COALESCE(ms.nbc::numeric, 0) AS nbc, " +
            "COALESCE(REPLACE(ms.pdv, ',', '.')::NUMERIC, 0) AS porez, " +
            "0 AS rabat, " +
            "COALESCE(REPLACE(ms.kolicina, ',', '.')::NUMERIC, 0) AS kolicina, " +
            "0 AS povratna_naknada, " +
            "m.novo AS novo_od, " +
            "m.editirano AS editirano_od " +
            "FROM meduskladisnica m " +
            "LEFT JOIN meduskladisnica_stavke ms ON m.broj = ms.broj " + filter;

            DataTable DT = SqlPostgres.select(query, "TABLE").Tables[0];

            string sql = "sql=";
            string tempDel = "";

            //PRVO PROVJERIM SVE DOKUMENTE KOJI SU U PROGRAMU MIJENJANI I BRIŠEM ISTE NA WEBU
            foreach (DataRow r in DT.Rows)
            {
                DateTime datum;
                DateTime.TryParse(r["datum"].ToString(), out datum);

                if (posalji_sve)
                {
                    tempDel = "DELETE FROM medu_skladiste WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' " +
                            " AND poslovnica_od='" + poslovnica + "' AND YEAR(datum)='" + datum.Year.ToString() + "';~";
                }
                else
                {
                    if (r["novo_od"].ToString() == "True" || r["editirano_od"].ToString() == "True")
                    {
                        tempDel = "DELETE FROM medu_skladiste WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "'" +
                            " AND (poslovnica_od='" + poslovnica + "' OR poslovnica_do = '" + poslovnica + "') AND (skladiste_od = '" + r["skladiste_od"].ToString() + "' AND skladiste_do = '" + r["skladiste_do"].ToString() + "') AND broj='" + r["broj"].ToString() + "'" +
                            " AND YEAR(datum)='" + datum.Year.ToString() + "';~";
                    }
                }

                if (!sql.Contains(tempDel))
                {
                    sql += tempDel;
                }
            }
            DateTime sad = DateTime.Now;
            foreach (DataRow r in DT.Rows)
            {
                DateTime datum;
                DateTime.TryParse(r["datum"].ToString(), out datum);

                if (r["sifra"].ToString() != "")
                {
                    sql += "INSERT INTO medu_skladiste (oib, poslovnica_od, poslovnica_do, skladiste_od, skladiste_do, broj, datum, id_izradio, napomena, sifra, vpc, mpc, nbc, porez, rabat, kolicina, povratna_naknada, novo_od, novo_do, editirano_od, editirano_do, datum_syn) VALUES (" +
                        "'" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "'," +
                        "'" + poslovnica + "'," +
                        "'" + poslovnica + "'," +
                        "'" + r["skladiste_od"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["skladiste_do"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["broj"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + datum.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        "'" + r["id_izradio"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["napomena"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["sifra"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["vpc"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["mpc"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["nbc"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["porez"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["rabat"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["kolicina"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["povratna_naknada"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'0'," + //novo_od
                        "'0'," + //novo_do
                        "'0'," + //editirano_od
                        "'0', " + //editirano_do
                        "'" + sad.ToString("yyyy-MM-dd H:mm:ss") + "');~"; //datum_syn
                }
            }

            if (sql.Length > 4)
            {
                sql = sql.Remove(sql.Length - 1);

                //ŠALJE NA WEB i DOBIVAM ODG
                string[] odg = Pomagala.MyWebRequest(sql + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "include/primam_post_sql_query.php").Split(';');

                if (odg[0] == "OK" && odg[1] == "1")
                {
                    foreach (DataRow r in DT.Rows)
                    {
                        sql = "UPDATE meduskladisnica SET editirano='0', novo='0' " +
                            " WHERE broj='" + r["broj"].ToString() + "';";

                        SqlPostgres.update(sql);
                    }
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
            id_poslovnica = "1";

            if (DTposlovnica.Rows.Count > 0)
            {
                poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
                id_poslovnica = DTposlovnica.Rows[0]["id_ducan"].ToString();
            }

            string sql = "SELECT * FROM medu_skladiste" +
                " WHERE (editirano_od='1' OR editirano_do = '1' OR novo_od = '1' OR novo_do = '1') AND OIB='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND (poslovnica_od = '" + poslovnica + "' OR poslovnica_do = '" + poslovnica + "')";
            DataTable DT = Pomagala.MyWebRequestXML("sql=" + sql + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "uzmi_podatke_xml/web_request.php");

            if (DT.Rows.Count > 0)
            {
                DateTime dtSyn = DateTime.Now;
                string zadnji_broj = "", zadnja_godina = "", poslovnica_od = "", poslovnica_do = "", sklasiste_od = "", skladiste_do = "";
                DateTime d;

                foreach (DataRow r in DT.Rows)
                {
                    DateTime.TryParse(r["datum"].ToString(), out d);

                    if (zadnji_broj == r["broj"].ToString() && zadnja_godina == d.Year.ToString() && poslovnica_od == r["poslovnica_od"].ToString() && poslovnica_do == r["poslovnica_do"].ToString() && sklasiste_od == r["skladiste_od"].ToString() && skladiste_do == r["skladiste_do"].ToString())
                    {
                        zadnji_broj = r["broj"].ToString();
                        zadnja_godina = d.Year.ToString();
                        poslovnica_od = r["poslovnica_od"].ToString();
                        poslovnica_do = r["poslovnica_do"].ToString();
                        sklasiste_od = r["skladiste_od"].ToString();
                        skladiste_do = r["skladiste_do"].ToString();

                        SpremiStavke(r);
                    }
                    else
                    {
                        zadnji_broj = r["broj"].ToString();
                        zadnja_godina = d.Year.ToString();
                        poslovnica_od = r["poslovnica_od"].ToString();
                        poslovnica_do = r["poslovnica_do"].ToString();
                        sklasiste_od = r["skladiste_od"].ToString();
                        skladiste_do = r["skladiste_do"].ToString();

                        SpremiHeader(r, DT, dtSyn);
                        SpremiStavke(r);
                    }

                    //**********************SQL WEB REQUEST***************************************************************************
                    sql_za_web += "UPDATE medu_skladiste SET editirano_od = '0', novo_od = '0'" +
                        " WHERE oib = '" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND broj = '" + r["broj"].ToString() + "' AND poslovnica_od = '" + poslovnica + "' AND skladiste_od = '" + r["skladiste_od"].ToString() + "';~";
                    sql_za_web += "UPDATE medu_skladiste SET editirano_od = '0', novo_od = '0'" +
    " WHERE oib = '" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND broj = '" + r["broj"].ToString() + "' AND poslovnica_od = '" + poslovnica + "' AND skladiste_do = '" + r["skladiste_od"].ToString() + "';~";

                    sql_za_web += "UPDATE medu_skladiste SET editirano_do = '0', novo_do = '0' " +
                        " WHERE oib = '" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND broj = '" + r["broj"].ToString() + "' AND poslovnica_do = '" + poslovnica + "' AND skladiste_do = '" + r["skladiste_od"].ToString() + "';~";
                    sql_za_web += "UPDATE medu_skladiste SET editirano_do = '0', novo_do = '0' " +
    " WHERE oib = '" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND broj = '" + r["broj"].ToString() + "' AND poslovnica_do = '" + poslovnica + "' AND skladiste_od = '" + r["skladiste_od"].ToString() + "';~";
                    //**********************SQL WEB REQUEST***************************************************************************
                }

                if (sql_za_web.Length > 4)
                {
                    sql_za_web = sql_za_web.Remove(sql_za_web.Length - 1);
                    string[] odg = Pomagala.MyWebRequest(sql_za_web + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "include/primam_post_sql_query.php").Split(';');
                }
            }
        }

        private void SpremiHeader(DataRow r, DataTable DT, DateTime dtSyn)
        {
            DateTime d;
            DateTime.TryParse(r["datum"].ToString(), out d);

            classSQL.delete("DELETE FROM meduskladisnica_stavke WHERE broj = '" + r["broj"].ToString() + "' AND godina = '" + d.Year.ToString() + "' AND iz_skladista = '" + r["skladiste_od"].ToString() + "';");
            classSQL.delete("DELETE FROM meduskladisnica WHERE broj = '" + r["broj"].ToString() + "' AND godina = '" + d.Year.ToString() + "' AND id_skladiste_od = '" + r["skladiste_od"].ToString() + "' AND id_skladiste_do = '" + r["skladiste_do"].ToString() + "';");

            decimal mpc = 0, nbc = 0, vpc = 0, pdv = 0, kolicina = 0;
            Decimal.TryParse(r["mpc"].ToString().Replace('.', ','), out mpc);
            Decimal.TryParse(r["vpc"].ToString().Replace('.', ','), out vpc);
            Decimal.TryParse(r["nbc"].ToString().Replace('.', ','), out nbc);
            Decimal.TryParse(r["porez"].ToString().Replace('.', ','), out pdv);
            Decimal.TryParse(r["kolicina"].ToString().Replace('.', ','), out kolicina);

            string sql = "INSERT INTO meduskladisnica (" +
                "broj, godina, id_skladiste_od, id_skladiste_do, datum, id_izradio, napomena, novo, editirano, datum_syn)" +
                " VALUES " +
                "(" +
                "'" + r["broj"].ToString() + "', " +
                "'" + d.Year.ToString() + "', " +
                "'" + r["skladiste_od"].ToString() + "', " +
                "'" + r["skladiste_do"].ToString() + "', " +
                "'" + d.ToString("yyyy-MM-dd H:mm:ss") + "', " +
                "'" + r["id_izradio"].ToString() + "', " +
                "'" + r["napomena"].ToString() + "', " +
                "'0', " +
                "'0', " +
                "'" + dtSyn.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                ")";

            classSQL.insert(sql);
        }

        private void SpremiStavke(DataRow r)
        {
            DateTime d;
            DateTime.TryParse(r["datum"].ToString(), out d);
            decimal mpc = 0, nbc = 0, vpc = 0, pdv = 0, kolicina = 0;
            Decimal.TryParse(r["mpc"].ToString().Replace('.', ','), out mpc);
            Decimal.TryParse(r["vpc"].ToString().Replace('.', ','), out vpc);
            Decimal.TryParse(r["nbc"].ToString().Replace('.', ','), out nbc);
            Decimal.TryParse(r["porez"].ToString().Replace('.', ','), out pdv);
            Decimal.TryParse(r["kolicina"].ToString().Replace('.', ','), out kolicina);

            string sql = "INSERT INTO meduskladisnica_stavke (" +
                "sifra, mpc, pdv, vpc, kolicina, broj, godina, nbc, iz_skladista)" +
                " VALUES " +
                "(" +
                "'" + r["sifra"].ToString() + "'," +
                "'" + mpc.ToString() + "'," +
                "'" + pdv.ToString() + "'," +
                "'" + vpc.ToString() + "'," +
                "'" + kolicina.ToString() + "'," +
                "'" + r["broj"].ToString() + "'," +
                "'" + d.Year.ToString() + "'," +
                "'" + nbc.ToString() + "'," +
                "'" + r["skladiste_od"].ToString() + "'" +
                ")";

            classSQL.insert(sql);
        }

        #endregion PRIMI PODATKE
    }
}