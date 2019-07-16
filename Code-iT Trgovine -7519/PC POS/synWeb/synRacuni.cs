using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.synWeb
{
    internal class synRacuni
    {
        private synWeb.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Properties.Settings.Default.domena_za_sinkronizaciju;
        private string poslovnica = "1";

        private bool posalji_sve = false;
        private string od_datuma = null;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synRacuni(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

        public synRacuni(bool _posalji_sve, string _od_datuma)
        {
            posalji_sve = _posalji_sve;
            od_datuma = _od_datuma;
        }

        public void Send()
        {
            try
            {
                DataTable DTpodaci_tvrtka = SqlPostgres.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
                DataTable DTpostavke = SqlPostgres.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
                DataTable DTposlovnica = SqlPostgres.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];
                DataTable DTnaplatni_uredaj = SqlPostgres.select("SELECT ime_blagajne FROM blagajna" +
                    " WHERE id_blagajna='" + DTpostavke.Rows[0]["default_blagajna"].ToString() + "'", "zaposlenici").Tables[0];

                string poslovnica = "1";
                string naplatni_uredaj = "1";
                if (DTposlovnica.Rows.Count > 0)
                {
                    poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
                }

                if (DTnaplatni_uredaj.Rows.Count > 0)
                {
                    naplatni_uredaj = DTnaplatni_uredaj.Rows[0]["ime_blagajne"].ToString();
                }

                //************************************GLEDA NA VARIJABLU posalji_sve ******************
                string filter = "";
                if (posalji_sve)
                {
                    if (od_datuma == null && MessageBox.Show("Šaljem račune. Ovo bi moglo potrajati par minuta ovisno o broju računa. Program ne dirajte sve dok ne dođe nova poruka.\r\nDali ste sigurni da želite nastaviti?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return;
                    }

                    if (od_datuma == null)
                        od_datuma = Interaction.InputBox("Odabir razdoblja", "Odabir razdoblja za koje želite poslati ponovno stavke na web server.", DateTime.Now.AddDays(-31).ToString("dd.MM.yyyy 00:00:01"));

                    DateTime dt_temp;
                    DateTime.TryParse(od_datuma, out dt_temp);

                    filter = "WHERE racuni.datum_racuna>='" + dt_temp.ToString("yyyy-MM-dd H:mm:ss") + "' order by racuni.datum_racuna, racun_stavke.id_stavka asc";
                }
                else
                {
                    filter = "  WHERE racuni.novo='1' ORDER BY racuni.datum_racuna, racun_stavke.id_stavka ASC";
                }
                //************************************************************************************

                string query = @"SELECT
racun_stavke.sifra_robe,
racun_stavke.id_skladiste,
racun_stavke.mpc,
racun_stavke.porez,
racun_stavke.kolicina,
racun_stavke.rabat,
racun_stavke.vpc,
racun_stavke.nbc,
racun_stavke.prirez,
racun_stavke.porez_na_dohodak,
racun_stavke.prirez_iznos,
racun_stavke.porez_na_dohodak_iznos,
racun_stavke.povratna_naknada,
ducan.ime_ducana,
blagajna.ime_blagajne,
racuni.broj_racuna,
racuni.novo,
racuni.datum_racuna,
racuni.id_blagajnik,
racuni.id_kupac,
racuni.id_kasa,
racuni.jir,
racuni.zki,
racuni.storno,
racuni.id_ducan,
racuni.nacin_placanja,
zaposlenici.oib AS oib_zaposlenika,
roba.id_grupa,
'0'::integer as id_podgrupa
FROM racun_stavke
LEFT JOIN roba ON racun_stavke.sifra_robe = roba.sifra
LEFT JOIN racuni ON racuni.broj_racuna = racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa
LEFT JOIN zaposlenici ON racuni.id_blagajnik = zaposlenici.id_zaposlenik
LEFT JOIN ducan ON ducan.id_ducan=racun_stavke.id_ducan
LEFT JOIN blagajna ON blagajna.id_blagajna=racun_stavke.id_kasa
" + filter + ";";

                DataTable DT = SqlPostgres.select(query, "TABLE").Tables[0];

                string sql = "";
                string tempDel = "";

                //PRVO PROVJERIM SVE DOKUMENTE KOJI SU U PROGRAMU MIJENJANI I BRIŠEM ISTE NA WEBU
                foreach (DataRow r in DT.Rows)
                {
                    DateTime datum;
                    DateTime.TryParse(r["datum_racuna"].ToString(), out datum);

                    if (posalji_sve && od_datuma == null)
                    {
                        tempDel = "DELETE FROM racuni WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND broj='" + r["broj_racuna"].ToString() + "'" +
                                " AND poslovnica='" + poslovnica + "' AND naplatni_uredaj='" + naplatni_uredaj + "' AND YEAR(datum)='" + datum.Year.ToString() + "';~";
                    }
                    else if (posalji_sve && od_datuma != null)
                    {
                        DateTime dt_temp;
                        DateTime.TryParse(od_datuma, out dt_temp);
                        tempDel = "DELETE FROM racuni WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND datum>='" + dt_temp.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                                    " AND poslovnica='" + poslovnica + "' AND naplatni_uredaj='" + naplatni_uredaj + "' AND YEAR(datum)='" + datum.Year.ToString() + "';~";
                    }
                    else
                    {
                        tempDel = "DELETE FROM racuni WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND broj='" + r["broj_racuna"].ToString() + "'" +
                                    " AND poslovnica='" + poslovnica + "' AND naplatni_uredaj='" + naplatni_uredaj + "' AND YEAR(datum)='" + datum.Year.ToString() + "';~";
                    }

                    if (!sql.Contains(tempDel))
                    {
                        sql += tempDel;
                    }
                }

                int count_sql = 0;
                foreach (DataRow r in DT.Rows)
                {
                    DateTime datum;
                    DateTime.TryParse(r["datum_racuna"].ToString(), out datum);
                    string storno = r["storno"].ToString() == "DA" ? "1" : "0";

                    sql += "INSERT INTO racuni (broj, datum, poslovnica, naplatni_uredaj, oib, id_blagajnik, kupac, naplata, sifra, " +
                        "id_skladiste, mpc, porez, kolicina, rabat, vpc, nbc, pp, povratna_naknada, id_grupa, oib_zaposlenika, id_podgrupa, jir, zki, storno, datum_syn) VALUES (" +
                        "'" + r["broj_racuna"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + datum.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        "'" + r["ime_ducana"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["ime_blagajne"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + DTpodaci_tvrtka.Rows[0]["oib"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["id_blagajnik"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["id_kupac"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["nacin_placanja"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["sifra_robe"].ToString().Replace(";", "").Replace("~", "").Replace("&", " and ") + "'," +
                        "'" + r["id_skladiste"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["mpc"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["porez"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["kolicina"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["rabat"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["vpc"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["nbc"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'0'," +
                        "'" + r["povratna_naknada"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["id_grupa"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["oib_zaposlenika"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["id_podgrupa"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["jir"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["zki"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + storno + "', NOW()" +
                        ");~";

                    //Replace("~", "").Replace("&", " and ")

                    count_sql++;
                    if (count_sql > 200)
                    {
                        sql += "Ł";
                        count_sql = 0;
                    }
                }

                if (sql.Length > 4)
                {
                    //ŠALJE NA WEB i DOBIVAM ODG

                    string[] arrSql = sql.Split('Ł');
                    bool sql_je_ispravan = true;

                    foreach (string ___sql in arrSql)
                    {
                        string sql_finish = "sql=" + ___sql.Remove(___sql.Length - 1);
                        string[] odg = Pomagala.MyWebRequest(sql_finish + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "include/primam_post_sql_query.php").Split(';');
                        if (odg[0] != "OK" || odg[1] != "1")
                        {
                            sql_je_ispravan = false;
                        }
                    }

                    if (sql_je_ispravan)
                    {
                        foreach (DataRow r in DT.Rows)
                        {
                            sql = @"UPDATE racuni
SET novo='0'
WHERE broj_racuna='" + r["broj_racuna"].ToString() + @"' AND id_ducan='" + r["id_ducan"].ToString() + @"' AND id_kasa='" + r["id_kasa"].ToString() + @"';";
                            SqlPostgres.update(sql);
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }
}