using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.synWeb
{
    internal class synInventura
    {
        private synWeb.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Properties.Settings.Default.domena_za_sinkronizaciju;
        private string poslovnica = "1";
        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synInventura(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

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
            string filter = "";
            if (posalji_sve)
            {
                if (MessageBox.Show("Želite poslati inventure?", "Sinkronizacija", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                filter = "";
            }
            else
            {
                filter = " WHERE (inventura.editirano='1' OR inventura.novo='1')";
            }
            //****************************************************************************

            string query = "SELECT " +
                " inventura.broj_inventure," +
                " inventura.id_skladiste," +
                " inventura.datum," +
                " inventura.id_zaposlenik," +
                " inventura.godina," +
                " inventura_stavke.sifra_robe," +
                " inventura_stavke.jmj," +
                " inventura_stavke.kolicina," +
                " inventura_stavke.kolicina_koja_je_bila," +
                " inventura_stavke.cijena," +
                " inventura_stavke.nbc," +
                " inventura_stavke.naziv," +
                " inventura_stavke.id_stavke," +
                " zaposlenici.oib AS oib_zaposlenika," +
                " inventura_stavke.porez," +
                " inventura.novo," +
                " inventura.editirano," +
                " inventura.pocetno_stanje," +
                " '0' as povratna_naknada," +
                " inventura_stavke.cijena as mpc" +
                " FROM inventura" +
                " LEFT JOIN inventura_stavke ON inventura_stavke.broj_inventure = inventura.broj_inventure" +
                " LEFT JOIN roba_prodaja ON inventura_stavke.sifra_robe = roba_prodaja.sifra AND inventura.id_skladiste = roba_prodaja.id_skladiste" +
                " LEFT JOIN zaposlenici ON inventura.id_zaposlenik = zaposlenici.id_zaposlenik " +
                " " + filter;

            DataTable DT = SqlPostgres.select(query, "TABLE").Tables[0];

            string sql = "sql=";
            string tempDel = "";

            //PRVO PROVJERIM SVE DOKUMENTE KOJI SU U PROGRAMU MIJENJANI I BRIŠEM ISTE NA WEBU
            foreach (DataRow r in DT.Rows)
            {
                if (posalji_sve)
                {
                    tempDel = "DELETE FROM inventura WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' " +
                            " AND poslovnica='" + poslovnica + "'" +
                            " AND godina='" + r["godina"].ToString().Replace(";", "").Replace("~", "") + "';~";
                }
                else
                {
                    if (r["editirano"].ToString() == "True")
                    {
                        tempDel = "DELETE FROM inventura WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' " +
                            " AND poslovnica='" + poslovnica + "' AND broj_inventure='" + r["broj_inventure"].ToString() + "'" +
                            " AND godina='" + r["godina"].ToString().Replace(";", "").Replace("~", "") + "';~";
                    }
                }

                if (!sql.Contains(tempDel))
                {
                    sql += tempDel;
                }
            }

            string is_pocetno_stanje = "0";

            foreach (DataRow r in DT.Rows)
            {
                DateTime datum;
                DateTime.TryParse(r["datum"].ToString(), out datum);

                if (r["pocetno_stanje"].ToString().ToUpper() == "1") { is_pocetno_stanje = "1"; } else { is_pocetno_stanje = "0"; }

                if (r["sifra_robe"].ToString() != "")
                {
                    string povratna_naknada = "0";
                    decimal mpc;
                    povratna_naknada = r["povratna_naknada"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "");
                    decimal.TryParse(r["mpc"].ToString(), out mpc);

                    sql += "INSERT INTO inventura (broj_inventure,id_skladiste,datum,id_zaposlenik,godina,sifra_robe,jmj,kolicina,cijena," +
                        "naziv,porez,kolicina_koja_je_bila_na_skl,oib,poslovnica,novo,editirano,oib_zaposlenika,povratna_naknada,is_pocetno_stanje,datum_syn,mpc) VALUES (" +
                        "'" + r["broj_inventure"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["id_skladiste"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + datum.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        "'" + r["id_zaposlenik"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["godina"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["sifra_robe"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["jmj"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["kolicina"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["nbc"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["naziv"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["porez"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["kolicina_koja_je_bila"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + DTpodaci_tvrtka.Rows[0]["oib"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + poslovnica.Replace(";", "").Replace("~", "") + "'," +
                        "'0'," +
                        "'0'," +
                        "'" + r["oib_zaposlenika"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + povratna_naknada.Replace(",", ".") + "'," +
                        "'" + is_pocetno_stanje + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        "'" + mpc.ToString().Replace(",", ".") + "'" +
                        ");~";
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
                        sql = "UPDATE inventura SET editirano='0', novo='0' " +
                            "WHERE broj_inventure='" + r["broj_inventure"].ToString() + "' AND inventura.id_skladiste='" + DT.Rows[0]["id_skladiste"].ToString() + "';";

                        SqlPostgres.update(sql);
                    }
                }
            }
        }

        #region PRIMI PODATKE

        public void UzmiPodatkeSaWeba()
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

                //PCPOS.Until.classFukcijeZaUpravljanjeBazom baza = new Until.classFukcijeZaUpravljanjeBazom("CAFFE", "DB");
                string GodinaKojaSeKoristi = Util.Korisno.GodinaKojaSeKoristiUbazi.ToString();

                string sql_za_web = "sql=";
                string query = "SELECT * FROM inventura WHERE (novo='1' OR editirano='1') " +
                    "AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "' AND YEAR(datum)='" + GodinaKojaSeKoristi + "'";
                DataTable DT = Pomagala.MyWebRequestXML("sql=" + query + "&godina=" + GodinaKojaSeKoristi, domena + "uzmi_podatke_xml/web_request.php");

                if (DT.Rows.Count > 0)
                {
                    foreach (DataRow r in DT.Rows)
                    {
                        decimal kolicina;
                        decimal cijena, mpc;
                        decimal povratna_naknada;
                        decimal kolicina_koja_je_bila_na_skl;

                        decimal.TryParse(r["kolicina"].ToString().Replace(".", ","), out kolicina);
                        decimal.TryParse(r["cijena"].ToString().Replace(".", ","), out cijena);
                        decimal.TryParse(r["mpc"].ToString().Replace(".", ","), out mpc);
                        decimal.TryParse(r["kolicina_koja_je_bila_na_skl"].ToString().Replace(".", ","), out kolicina_koja_je_bila_na_skl);
                        decimal.TryParse(r["povratna_naknada"].ToString().Replace(".", ","), out povratna_naknada);

                        string sqlPG = "BEGIN; UPDATE inventura_stavke SET " +
                            " kolicina='" + Math.Round(kolicina, 4).ToString().Replace(".", ",") + "'," +
                            " kolicina_koja_je_bila='" + Math.Round(kolicina_koja_je_bila_na_skl, 4).ToString().Replace(",", ".") + "'," +
                            " nbc='" + Math.Round(cijena, 4).ToString().Replace(",", ".") + "'," +
                            " cijena='" + Math.Round(mpc, 4).ToString().Replace(",", ".") + "'" +
                            " WHERE sifra_robe='" + r["sifra_robe"].ToString() + "' " +
                            " AND broj_inventure='" + r["broj_inventure"].ToString() + "'; COMMIT;";
                        classSQL.update(sqlPG);

                        //**********************SQL WEB REQUEST***************************************
                        sql_za_web += "UPDATE inventura SET novo='0', editirano='0',datum_syn='" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "' " +
                            " WHERE id='" + r["id"].ToString() + "'" +
                            " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "'" +
                            " AND inventura.id_skladiste='" + DT.Rows[0]["id_skladiste"].ToString() + "';~";
                        //**********************SQL WEB REQUEST***************************************
                    }

                    if (sql_za_web.Length > 4)
                    {
                        sql_za_web = sql_za_web.Remove(sql_za_web.Length - 1);
                        string[] odg = Pomagala.MyWebRequest(sql_za_web + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "include/primam_post_sql_query.php").Split(';');
                    }
                }
            }
            catch
            { }
        }

        #endregion PRIMI PODATKE
    }
}