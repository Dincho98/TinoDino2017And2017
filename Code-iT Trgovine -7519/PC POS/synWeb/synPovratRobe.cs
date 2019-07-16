using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.synWeb
{
    internal class synPovratRobe
    {
        private synWeb.pomagala_syn Pomagala = new synWeb.pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Properties.Settings.Default.domena_za_sinkronizaciju;
        private string poslovnica = "1";
        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synPovratRobe(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

        #region Pošalji podatke

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
                if (MessageBox.Show("Želite poslati otpis robe?", "Sinkronizacija", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                filter = "";
            }
            else
            {
                filter = " WHERE povrat_robe.editirano='1' OR povrat_robe.novo='1'";
            }
            //****************************************************************************

            string query = "SELECT " +
                " povrat_robe.broj," +
                " povrat_robe.datum," +
                " povrat_robe.id_odrediste," +
                " povrat_robe.id_partner," +
                " povrat_robe.mjesto_troska," +
                " povrat_robe.orginalni_dokument," +
                " povrat_robe.id_izradio," +
                " povrat_robe.napomena," +
                " povrat_robe.godina," +
                " povrat_robe_stavke.prodajna_cijena," +
                " povrat_robe.novo," +
                " povrat_robe.editirano," +
                " povrat_robe.id_skladiste," +
                " povrat_robe_stavke.sifra," +
                " povrat_robe_stavke.vpc," +
                " povrat_robe_stavke.pdv," +
                " povrat_robe_stavke.rabat," +
                " povrat_robe_stavke.broj," +
                " povrat_robe_stavke.kolicina," +
                " zaposlenici.oib AS oib_zaposlenika," +
                " povrat_robe_stavke.id_stavka," +
                " povrat_robe_stavke.nbc," +
                " povrat_robe_stavke.mpc" +
                " FROM povrat_robe" +
                " LEFT JOIN povrat_robe_stavke ON povrat_robe_stavke.broj=povrat_robe.broj " +
                " LEFT JOIN roba_prodaja ON povrat_robe_stavke.sifra = roba_prodaja.sifra AND povrat_robe.id_skladiste = roba_prodaja.id_skladiste" +
                " LEFT JOIN zaposlenici ON povrat_robe.id_izradio = zaposlenici.id_zaposlenik " +
                " " + filter;

            DataTable DT = SqlPostgres.select(query, "TABLE").Tables[0];

            string sql = "sql=";
            string tempDel = "";

            //PRVO PROVJERIM SVE DOKUMENTE KOJI SU U PROGRAMU MIJENJANI I BRIŠEM ISTE NA WEBU
            foreach (DataRow r in DT.Rows)
            {
                if (posalji_sve)
                {
                    tempDel = "DELETE FROM povrat_robe WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' " +
                            "AND poslovnica='" + poslovnica + "' AND godina='" + r["godina"].ToString() + "';~";
                }
                else
                {
                    if (r["editirano"].ToString().ToUpper() == "TRUE")
                    {
                        tempDel = "DELETE FROM povrat_robe WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND povrat_robe.id_skladiste ='" + r["id_skladiste"].ToString() + "' " +
                            "AND poslovnica='" + poslovnica + "' AND broj='" + r["broj"].ToString() + "' AND godina='" + r["godina"].ToString() + "';~";
                    }
                }

                if (!sql.Contains(tempDel))
                {
                    sql += tempDel;
                }
            }

            foreach (DataRow r in DT.Rows)
            {
                DateTime datum;
                DateTime.TryParse(r["datum"].ToString(), out datum);

                if (r["sifra"].ToString() != "")
                {
                    string Partner = r["id_partner"].ToString().Replace(",", ".") == "" ? "0" : r["id_partner"].ToString().Replace(",", ".");

                    int _godina = datum.Year;
                    int.TryParse(r["godina"].ToString(), out _godina);
                    if (_godina == 0)
                        _godina = datum.Year;

                    sql += "INSERT INTO povrat_robe (broj, datum, id_partner, id_izradio, godina,id_skladiste, sifra,vpc,pdv," +
                           "kolicina,prodajna_cijena,id_stavka,nbc,mpc,rabat,novo,editirano,oib,oib_zaposlenika,povratna_naknada,datum_syn,poslovnica) VALUES (" +
                           "'" + r["broj"].ToString().Replace(";", "").Replace("~", "") + "'," +
                           "'" + datum.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                           "'" + Partner + "'," +
                           "'" + r["id_izradio"].ToString() + "'," +
                           "'" + _godina.ToString().Replace(";", "").Replace("~", "") + "'," +
                           "'" + r["id_skladiste"].ToString().Replace(";", "").Replace("~", "") + "'," +
                           "'" + r["sifra"].ToString().Replace(";", "").Replace("~", "") + "'," +
                           "'" + r["vpc"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                           "'" + r["pdv"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                           "'" + r["kolicina"].ToString().Replace(";", "").Replace(",", ".").Replace("~", "") + "'," +
                           "'" + r["prodajna_cijena"].ToString().Replace(";", "").Replace(",", ".").Replace("~", "") + "'," +
                           "'" + r["id_stavka"].ToString().Replace(";", "").Replace("~", "") + "'," +
                           "'" + r["nbc"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                           "'" + r["mpc"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                           "'" + r["rabat"].ToString().Replace(";", "").Replace(",", ".").Replace("~", "") + "'," +
                           "'0'," +
                           "'0','" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "'," +
                           "'" + r["oib_zaposlenika"].ToString().Replace(";", "").Replace("~", "") + "'," +
                           "'0'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                           "'" + poslovnica + "');~";
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
                        sql = "UPDATE povrat_robe SET editirano='0', novo='0' " +
                            "WHERE broj='" + r["broj"].ToString() + "' AND povrat_robe.id_skladiste ='" + r["id_skladiste"].ToString() + "';";

                        SqlPostgres.update(sql);
                    }
                }
            }
        }

        #endregion Pošalji podatke
    }
}