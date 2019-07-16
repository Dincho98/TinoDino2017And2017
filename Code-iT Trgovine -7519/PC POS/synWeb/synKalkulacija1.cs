using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.synWeb
{
    internal class synKalkulacije1
    {
        private synWeb.pomagala_syn Pomagala = new synWeb.pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Properties.Settings.Default.domena_za_sinkronizaciju;
        private string poslovnica = "1";
        private string id_poslovnica = "1";
        private bool posalji_sve = false;
        private int id_skladiste;
        private string posl;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synKalkulacije1(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

        public synKalkulacije1(int _id_skladiste, string _poslovnica)
        {
            id_skladiste = _id_skladiste;
            posl = _poslovnica;
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
                if (MessageBox.Show("Želite poslati kalkulacije?", "Sinkronizacija", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                filter = "";
            }
            else
            {
                filter = "WHERE (k.editirano='1' OR k.novo='1')";
            }
            //****************************************************************************

            string query = @"SELECT '" + DTpodaci_tvrtka.Rows[0]["oib"] + "' AS oib, " +
                "'" + poslovnica + "' AS poslovnica, " +
                "k.broj, " +
                "k.id_partner, " +
                "k.racun, " +
                "k.otpremnica, " +
                "k.racun_datum, " +
                "k.otpremnica_datum, " +
                "k.mjesto_troska, " +
                "k.datum, " +
                "COALESCE(k.ukupno_vpc::NUMERIC, 0) AS ukupno_vpc, " +
                "COALESCE(k.ukupno_mpc::NUMERIC, 0) AS ukupno_mpc, " +
                "COALESCE(REPLACE(k.tecaj,',','.')::NUMERIC, 0) AS tecaj, " +
                "k.id_valuta, " +
                "COALESCE(k.fakturni_iznos::NUMERIC, 0) AS fakturni_iznos, " +
                "k.id_skladiste, " +
                "k.id_zaposlenik, " +
                "COALESCE(REPLACE(ks.porez_potrosnja, ',','.')::NUMERIC, 0) AS porez_potrosnja, " +
                "COALESCE(REPLACE(ks.kolicina, ',','.')::NUMERIC, 0) AS kolicina, " +
                "COALESCE(ks.fak_cijena, 0) AS fak_cijena, " +
                "COALESCE(REPLACE(ks.rabat,',','.')::NUMERIC, 0) AS rabat, " +
                "prijevoz, " +
                "COALESCE(carina::NUMERIC, 0) AS carina, " +
                "COALESCE(REPLACE(ks.marza_postotak, ',','.')::NUMERIC, 0) AS marza_postotak, " +
                "COALESCE(REPLACE(ks.porez, ',','.')::NUMERIC, 0) AS porez, " +
                "COALESCE(ks.posebni_porez::NUMERIC, 0) AS posebni_porez, " +
                "ks.sifra, " +
                "ks.vpc, " +
                "k.novo as novo, " +
                "k.editirano as editirano, " +
                "now() as datum_syn " +
                "from kalkulacija k " +
                "left join kalkulacija_stavke ks on k.broj = ks.broj " +
                filter +
                " order by k.broj, ks.id_stavka asc;";

            query = query.Replace("+", "zbroj");

            DataTable DT = SqlPostgres.select(query, "TABLE").Tables[0];

            string sql = "sql=";
            string tempDel = "";

            //PRVO PROVJERIM SVE DOKUMENTE KOJI SU U PROGRAMU MIJENJANI I BRIŠEM ISTE NA WEBU
            if (posalji_sve)
            {
                tempDel = "DELETE FROM kalkulacija " +
                        " WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' " +
                        " AND poslovnica='" + poslovnica + "' " +
                        " AND YEAR(datum)='" + Util.Korisno.GodinaKojaSeKoristiUbazi + "';~";
                sql += tempDel;
            }
            else
            {
                foreach (DataRow r in DT.Rows)
                {
                    DateTime datum;
                    DateTime.TryParse(r["datum"].ToString(), out datum);

                    if (r["editirano"].ToString() == "True")
                    {
                        tempDel = "DELETE FROM kalkulacija " +
                            " WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' " +
                            " AND poslovnica='" + poslovnica + "' " +
                            " AND broj='" + r["broj"].ToString() + "'" +
                            " AND YEAR(datum)='" + datum.Year.ToString() + "' " +
                            " AND id_skladiste='" + r["id_skladiste"].ToString() + "';~";
                        if (!sql.Contains(tempDel))
                        {
                            sql += tempDel;
                        }
                    }
                }
            }

            foreach (DataRow r in DT.Rows)
            {
                DateTime datum = new DateTime(), racun_datum = new DateTime(), otpremnica_datum = new DateTime(), datum_syn = new DateTime();

                DateTime.TryParse(r["datum"].ToString(), out datum);
                DateTime.TryParse(r["racun_datum"].ToString(), out racun_datum);
                DateTime.TryParse(r["otpremnica_datum"].ToString(), out otpremnica_datum);
                DateTime.TryParse(r["datum_syn"].ToString(), out datum_syn);

                if (r["sifra"].ToString() != "")
                {
                    sql += "INSERT INTO `kalkulacija` (`oib`, `poslovnica`, `broj`, `id_partner`, `racun`, `otpremnica`, `racun_datum`, `otpremnica_datum`, `mjesto_troska`, `datum`, `ukupno_vpc`, `ukupno_mpc`, `tecaj`, `id_valuta`, `fakturni_iznos`, `id_skladiste`, `id_zaposlenik`, `porez_potrosnja`, `kolicina`, `fak_cijena`, `rabat`, `prijevoz`, `carina`, `marza_postotak`, `porez`, `posebni_porez`, `sifra`, `vpc`, `novo`, `editirano`, `datum_syn`) VALUES " +
                        "('" + r["oib"] + "', " +
                        "'" + r["poslovnica"] + "', " +
                        "'" + r["broj"] + "', " +
                        "'" + r["id_partner"] + "', " +
                        "'" + r["racun"] + "', " +
                        "'" + r["otpremnica"] + "', " +
                        "'" + racun_datum.ToString("yyyy-MM-dd H:mm:ss") + "', " +
                        "'" + otpremnica_datum.ToString("yyyy-MM-dd H:mm:ss") + "', " +
                        "'" + r["mjesto_troska"] + "', " +
                        "'" + datum.ToString("yyyy-MM-dd H:mm:ss") + "', " +
                        "'" + r["ukupno_vpc"].ToString().Replace(',', '.') + "', " +
                        "'" + r["ukupno_mpc"].ToString().Replace(',', '.') + "', " +
                        "'" + r["tecaj"].ToString().Replace(',', '.') + "', " +
                        "'" + r["id_valuta"] + "', " +
                        "'" + r["fakturni_iznos"].ToString().Replace(',', '.') + "', " +
                        "'" + r["id_skladiste"] + "', " +
                        "'" + r["id_zaposlenik"] + "', " +
                        "'" + r["porez_potrosnja"].ToString().Replace(',', '.') + "', " +
                        "'" + r["kolicina"].ToString().Replace(',', '.') + "', " +
                        "'" + r["fak_cijena"].ToString().Replace(',', '.') + "', " +
                        "'" + r["rabat"].ToString().Replace(',', '.') + "', " +
                        "'" + r["prijevoz"].ToString().Replace(',', '.') + "', " +
                        "'" + r["carina"].ToString().Replace(',', '.') + "', " +
                        "'" + r["marza_postotak"].ToString().Replace(',', '.') + "', " +
                        "'" + r["porez"].ToString().Replace(',', '.') + "', " +
                        "'" + r["posebni_porez"].ToString().Replace(',', '.') + "', " +
                        "'" + r["sifra"] + "', " +
                        "'" + r["vpc"].ToString().Replace(',', '.') + "', " +
                        "'0', " +
                        "'" + (Util.Korisno.centrala ? 1 : 0).ToString() + "', " +
                        "'" + datum_syn.ToString("yyyy-MM-dd H:mm:ss") + "');~";
                }
            }

            if (sql.Length > 4)
            {
                sql = sql.Remove(sql.Length - 1);

                //string[] ssss = sql.Split('~');

                //foreach (string ss in ssss) {
                //    string[] odg = Pomagala.MyWebRequest("sql=" + ss.Replace("sql=", "") + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "include/primam_post_sql_query.php").Split(';');
                //    if (odg[0] != "OK" || odg[1] != "1") {
                //        string ssssssssssssssssssss = ss;
                //    }
                //}

                //ŠALJE NA WEB i DOBIVAM ODG
                string[] odg = Pomagala.MyWebRequest(sql + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "include/primam_post_sql_query.php").Split(';');

                //if (odg[0] == "OK" && odg[1] == "1")
                //{
                //    foreach (DataRow r in DT.Rows)
                //    {
                //        sql = "UPDATE kalkulacija SET editirano='0', novo='0', datum_syn=NOW() " +
                //            " WHERE broj='" + r["broj"].ToString() + "' " +
                //            " AND id_skladiste='" + r["id_skladiste"].ToString() + "';";

                //        SqlPostgres.update(sql);
                //    }
                //}
            }
        }

        #endregion POŠALJI PODATKE

        #region PRIMI PODATKE

        public void UzmiPodatkeSaWeba(string posl = null, int id_skladiste = 0)
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

            string sql = @"SELECT * FROM kalkulacija WHERE (editirano='1') AND OIB='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + (posl != null ? posl : poslovnica) + "'";
            if (id_skladiste != 0)
            {
                sql += " and id_skladiste = '" + id_skladiste + "'";
            }
            sql += " order by broj asc;";

            DataTable DT = Pomagala.MyWebRequestXML("sql=" + sql + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "uzmi_podatke_xml/web_request.php");
            //DataTable DT = Pomagala.MyWebRequestXML("sql=" + sql + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "uzmi_podatke_xml/web_request.php");
            if (DT.Rows.Count > 0)
            {
                string zadnji_broj = "", zadnja_godina = "", zadnja_poslovnica = "";//, is_kalkulacija = "";
                int lastId = 0;
                id_skladiste = 0;
                DateTime d;

                foreach (DataRow r in DT.Rows)
                {
                    DateTime.TryParse(r["datum"].ToString(), out d);

                    if (zadnji_broj == r["broj"].ToString() && zadnja_godina == d.Year.ToString() && zadnja_poslovnica == r["poslovnica"].ToString() && id_skladiste == Convert.ToInt32(r["id_skladiste"].ToString()))
                    {
                        zadnji_broj = r["broj"].ToString();
                        zadnja_godina = d.Year.ToString();
                        zadnja_poslovnica = r["poslovnica"].ToString();
                        //is_kalkulacija = r["is_kalkulacija"].ToString();

                        SpremiStavke(r, lastId);
                    }
                    else
                    {
                        zadnji_broj = r["broj"].ToString();
                        zadnja_godina = d.Year.ToString();
                        zadnja_poslovnica = r["poslovnica"].ToString();
                        id_skladiste = Convert.ToInt32(r["id_skladiste"].ToString());
                        //is_kalkulacija = r["is_kalkulacija"].ToString();

                        lastId = SpremiHeader(r, DT, posl, id_skladiste);
                        SpremiStavke(r, lastId);
                    }

                    //**********************SQL WEB REQUEST***************************************************************************
                    string sw = "UPDATE kalkulacija SET " +
                        " editirano='0' " +
                        " WHERE broj='" + r["broj"].ToString() + "' " +
                        " AND poslovnica='" + (posl != null ? posl : r["poslovnica"].ToString()) + "'" +
                        " AND id_skladiste='" + (id_skladiste != 0 ? id_skladiste.ToString() : r["id_skladiste"].ToString()) + "'" +
                        " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "';~";

                    if (!sql_za_web.Contains(sw))
                    {
                        sql_za_web += sw;
                    }
                    //**********************SQL WEB REQUEST***************************************************************************
                }

                if (sql_za_web.Length > 4)
                {
                    sql_za_web = sql_za_web.Remove(sql_za_web.Length - 1);
                    string[] odg = Pomagala.MyWebRequest(sql_za_web + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "include/primam_post_sql_query.php").Split(';');
                }
            }
        }

        private int SpremiHeader(DataRow r, DataTable DT, string posl = null, int id_skladiste = 0)
        {
            DateTime d;
            DateTime.TryParse(r["datum"].ToString(), out d);
            DateTime dNow = DateTime.Now;
            string s = "DELETE FROM kalkulacija_stavke WHERE broj = '" + r["broj"].ToString() + "' " + (id_skladiste != 0 ? " and id_kalkulacija = (select id_kalkulacija from kalkulacija where broj = '" + r["broj"].ToString() + "' and id_skladiste = '" + id_skladiste + "')" : "") + ";" +
                "DELETE FROM kalkulacija WHERE broj = '" + r["broj"].ToString() + "' " + (id_skladiste != 0 ? " and id_skladiste = '" + id_skladiste + "'" : "") + ";";
            classSQL.delete(s);

            //decimal rabat = 0, fakturniIznos = 0, fakturniIznosUkupno = 0, prodajna_bez_poreza, porez, kolicina, UK_bez_poreza = 0, UK_sa_porezom = 0;
            decimal veleprodaja = 0, maloprodaja = 0, fak_cijena = 0, carina = 0, valuta = 0;
            string id_zaposlenik = "0";

            //" + r["oib_zaposlenika"].ToString() + "
            //21532479840
            DataTable dtZaposlenik = classSQL.select("SELECT COALESCE(id_zaposlenik, 0) as id_zaposlenik FROM zaposlenici WHERE id_zaposlenik = '" + r["id_zaposlenik"].ToString() + "'", "zaposlenici").Tables[0];

            if (dtZaposlenik.Rows.Count > 0) id_zaposlenik = dtZaposlenik.Rows[0]["id_zaposlenik"].ToString();

            //ZBRAJAM UKUPNO NA PRIMKI ILI KALKULACIJI
            DataRow[] RowsTrenutnaPrimka = DT.Select("broj='" + r["broj"].ToString() + "'");
            //foreach (DataRow Rc in RowsTrenutnaPrimka)
            //{
            //decimal.TryParse(Rc["prodajna_cijena"].ToString().Replace(".", ","), out prodajna_bez_poreza);
            //decimal.TryParse(Rc["ulazni_porez"].ToString().Replace(".", ","), out porez);
            //decimal.TryParse(Rc["kolicina"].ToString().Replace(".", ","), out kolicina);
            //decimal.TryParse(Rc["nabavni_iznos"].ToString().Replace(".", ","), out fakturniIznos);

            //UK_bez_poreza += (prodajna_bez_poreza * kolicina);
            //UK_sa_porezom += ((prodajna_bez_poreza * (1 + (porez / 100))) * kolicina);
            //fakturniIznosUkupno += fakturniIznos;

            //rabat = Convert.ToDouble(dataGridView1.Rows[i].Cells["rabat"].FormattedValue.ToString());
            //veleprodaja += (Convert.ToDouble(dataGridView1.Rows[i].Cells["VPC"].FormattedValue.ToString()) * Convert.ToDouble(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString()));
            //maloprodaja += (Convert.ToDouble(dataGridView1.Rows[i].Cells["MPC"].FormattedValue.ToString()) * Convert.ToDouble(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString()));
            //fak_cijena = (Convert.ToDouble(dataGridView1.Rows[i].Cells["nab_cijena"].FormattedValue.ToString()) * Convert.ToDouble(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString())) + fak_cijena;

            //}

            decimal.TryParse(r["ukupno_vpc"].ToString().Replace(".", ","), out veleprodaja);
            decimal.TryParse(r["ukupno_mpc"].ToString().Replace(".", ","), out maloprodaja);
            decimal.TryParse(r["fakturni_iznos"].ToString().Replace(".", ","), out fak_cijena);
            decimal.TryParse(r["carina"].ToString().Replace(".", ","), out carina);
            decimal.TryParse(r["id_valuta"].ToString().Replace(".", ","), out valuta);

            DateTime dRacun;
            DateTime.TryParse(r["racun_datum"].ToString(), out dRacun);
            DateTime dOtpremnica;
            DateTime.TryParse(r["otpremnica_datum"].ToString(), out dOtpremnica);

            string sql = @"INSERT INTO kalkulacija (broj, id_partner, racun, otpremnica, racun_datum, otpremnica_datum,
mjesto_troska, datum, godina, ukupno_vpc, ukupno_mpc, tecaj, id_valuta, fakturni_iznos, id_skladiste, id_zaposlenik,
porez_potrosnja, novo, editirano, datum_syn) VALUES (
'" + r["broj"].ToString() + @"',
'" + r["id_partner"].ToString() + @"',
            '" + r["racun"].ToString() + @"',
            '" + r["otpremnica"].ToString() + "',  " + //otpremnica
            "'" + dRacun.ToString("yyyy-MM-dd H:mm:ss") + "', " + //racun_datum
            "'" + dOtpremnica.ToString("yyyy-MM-dd H:mm:ss") + "', " + //otpremnica_datum
            "'" + r["mjesto_troska"].ToString() + "', " + //mjesto_troska
            "'" + d.ToString("yyyy-MM-dd H:mm:ss") + "', " +
            "'" + d.ToString("yyyy") + "', " +
            "'" + Math.Round(veleprodaja, 2).ToString().Replace(".", ",") + "', " +
            "'" + Math.Round(maloprodaja, 2).ToString().Replace(".", ",") + "', " +
            "'" + r["tecaj"].ToString().Replace(".", ",") + "', " + //tecaj
            "'" + r["id_valuta"].ToString() + "', " + //valuta
            "'" + Math.Round(fak_cijena, 2).ToString().Replace(".", ",") + "', " + //fakturni_iznos
            "'" + r["id_skladiste"].ToString() + "', " +
            "'" + id_zaposlenik + "', " +
            "'0', " + //porez_potrosnja
            "'0', " + //novo
            "'0', " + //editirano
            "'" + dNow.ToString("yyyy-MM-dd H:mm:ss") + "');";

            classSQL.insert(sql);

            int lastId = Convert.ToInt32(classSQL.select("SELECT MAX(id_kalkulacija) AS id FROM kalkulacija WHERE broj = '" + r["broj"].ToString() + "' and id_skladiste = '" + r["id_skladiste"] + "';", "kalkulacija").Tables[0].Rows[0]["id"]);

            return lastId;
        }

        private void SpremiStavke(DataRow r, int lastId)
        {
            decimal kolicina = 0, fakturna = 0, rabat = 0, prijevoz = 0, carina = 0, marza_postotak = 0, porez = 0, posebni_porez = 0, vpc = 0;

            decimal.TryParse(r["kolicina"].ToString().Replace(".", ","), out kolicina);
            decimal.TryParse(r["fak_cijena"].ToString().Replace(".", ","), out fakturna);
            decimal.TryParse(r["rabat"].ToString().Replace(".", ","), out rabat);
            //decimal.TryParse(0.ToString().Replace(".", ","), out prijevoz);
            decimal.TryParse(r["prijevoz"].ToString().Replace(".", ","), out prijevoz);
            decimal.TryParse(r["carina"].ToString().Replace(".", ","), out carina);
            decimal.TryParse(r["posebni_porez"].ToString(), out posebni_porez);
            decimal.TryParse(r["marza_postotak"].ToString().Replace(".", ","), out marza_postotak);
            decimal.TryParse(r["porez"].ToString().Replace(".", ","), out porez);
            decimal.TryParse(r["vpc"].ToString().Replace(".", ","), out vpc);

            string sql = "INSERT INTO kalkulacija_stavke (" +
                "kolicina, fak_cijena, rabat, prijevoz, carina, marza_postotak, porez, posebni_porez,porez_potrosnja, " +
                "broj, sifra, vpc, id_skladiste, id_kalkulacija) " +
                " VALUES " +
                "(" +
                "'" + Math.Round(kolicina, 3).ToString().Replace(".", ",") + "', " +
                "'" + Math.Round(fakturna, 2).ToString().Replace(",", ".") + "', " +
                "'" + Math.Round(rabat, 2).ToString().Replace(".", ",") + "', " +
                "'" + Math.Round(prijevoz, 2).ToString().Replace(",", ".") + "', " +
                "'" + Math.Round(carina, 2).ToString().Replace(".", ",") + "', " +
                "'" + Math.Round(marza_postotak, 4).ToString().Replace(".", ",") + "', " +
                "'" + Math.Round(porez, 4).ToString().Replace(".", ",") + "', " +
                "'" + Math.Round(posebni_porez, 2).ToString().Replace(".", ",") + "', " +
                "'0'," +
                "'" + r["broj"] + "', " +
                "'" + r["sifra"].ToString() + "'," +
                "'" + Math.Round(vpc, 3).ToString().Replace(",", ".") + "', " +
                "'" + r["id_skladiste"].ToString() + "', " +
                "'" + lastId.ToString() + "'" +
                ");";

            classSQL.insert(sql);
        }

        #endregion PRIMI PODATKE
    }
}