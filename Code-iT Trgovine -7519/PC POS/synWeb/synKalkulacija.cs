using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.synWeb
{
    internal class synKalkulacije
    {
        private synWeb.pomagala_syn Pomagala = new synWeb.pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Properties.Settings.Default.domena_za_sinkronizaciju;
        private string poslovnica = "1";
        private string id_poslovnica = "1";
        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synKalkulacije(bool _posalji_sve)
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
                if (MessageBox.Show("Želite poslati kalkulacije?", "Sinkronizacija", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                filter = ";";
            }
            else
            {
                filter = "  WHERE (kalkulacija.editirano='1' OR kalkulacija.novo='1');";
            }
            //****************************************************************************

            string query = @"SELECT
            CAST(REPLACE(marza_postotak,',','.') AS numeric) as marza,
            ROUND(((kalkulacija_stavke.fak_cijena::numeric-(kalkulacija_stavke.fak_cijena::numeric*REPLACE(kalkulacija_stavke.rabat,',','.')::numeric/100))*CAST(REPLACE(marza_postotak,',','.') AS numeric)/100),4) as iznos_marze,
            vpc as prodajna_cijena,
            ROUND(vpc*(1+(CAST(REPLACE(porez,',','.') AS numeric)/100)),3) as prodajna_cijena_sa_porezom,
            kalkulacija.broj,
            kalkulacija.id_skladiste,
            kalkulacija.racun as ulazni_dok,
            kalkulacija.id_partner,
            kalkulacija.tecaj,
            kalkulacija.datum,
            kalkulacija.id_zaposlenik,
            zaposlenici.oib as oib_zaposlenika,
            ''::text as napomena,
            kalkulacija_stavke.carina::numeric as carina,
            kalkulacija_stavke.sifra,
            REPLACE(kalkulacija_stavke.kolicina,',','.')::numeric as kolicina,
            REPLACE(kalkulacija_stavke.kolicina,',','.')::numeric as u_pakiranju,
            '1'::numeric as broj_paketa,
            kalkulacija_stavke.fak_cijena::numeric as cijena_po_komadu,
            REPLACE(kalkulacija_stavke.rabat,',','.')::numeric as rabat,
            ROUND((kalkulacija_stavke.fak_cijena::numeric-(kalkulacija_stavke.fak_cijena::numeric*REPLACE(kalkulacija_stavke.rabat,',','.')::numeric/100)),4) as nabavna_cijena,
            REPLACE(porez,',','.')::numeric as ulazni_porez,
            ROUND((kalkulacija_stavke.fak_cijena::numeric-(kalkulacija_stavke.fak_cijena::numeric*REPLACE(kalkulacija_stavke.rabat,',','.')::numeric/100))*REPLACE(kalkulacija_stavke.kolicina,',','.')::numeric,4) as nabavni_iznos,
            '0'::numeric as povratna_naknada,
            kalkulacija.editirano,
            kalkulacija.novo
            FROM kalkulacija
            LEFT JOIN kalkulacija_stavke ON kalkulacija.broj=kalkulacija_stavke.broj AND kalkulacija.id_skladiste=kalkulacija_stavke.id_skladiste
            LEFT JOIN zaposlenici ON kalkulacija.id_zaposlenik=zaposlenici.id_zaposlenik
            " + filter;

            query = query.Replace("+", "zbroj");

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
                    tempDel = "DELETE FROM primke " +
                            " WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' " +
                            " AND poslovnica='" + poslovnica + "' " +
                            " AND YEAR(datum)='" + datum.Year.ToString() + "'" +
                            " AND id_skladiste='" + r["id_skladiste"].ToString() + "'" +
                            " AND is_kalkulacija='1';~";
                }
                else
                {
                    if (r["editirano"].ToString() == "True")
                    {
                        tempDel = "DELETE FROM primke " +
                            " WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' " +
                            " AND poslovnica='" + poslovnica + "' " +
                            " AND broj='" + r["broj"].ToString() + "'" +
                            " AND YEAR(datum)='" + datum.Year.ToString() + "' " +
                            " AND id_skladiste='" + r["id_skladiste"].ToString() + "'" +
                            " AND is_kalkulacija='1';~";
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
                    sql += "INSERT INTO primke (marza, iznos_marze, prodajna_cijena, prodajna_cijena_sa_porezom, broj, id_skladiste,ulazni_dok,id_partner" +
                        ",datum, carina, valuta, napomena, id_zaposlenik, sifra, u_pakiranju, broj_paketa,kolicina, cijena_po_komadu, rabat, nabavna_cijena, ulazni_porez," +
                        "nabavni_iznos,povratna_naknada,editirano,oib,is_kalkulacija,oib_zaposlenika,poslovnica) VALUES (" +
                        "'" + r["marza"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["iznos_marze"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["prodajna_cijena"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["prodajna_cijena_sa_porezom"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["broj"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["id_skladiste"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["ulazni_dok"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["id_partner"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + datum.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        "'" + r["carina"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["tecaj"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["napomena"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["id_zaposlenik"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["sifra"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["u_pakiranju"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["broj_paketa"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["kolicina"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["cijena_po_komadu"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["rabat"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["nabavna_cijena"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["ulazni_porez"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["nabavni_iznos"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["povratna_naknada"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'0'," +
                        "'" + DTpodaci_tvrtka.Rows[0]["oib"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'1'," +
                        "'" + r["oib_zaposlenika"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
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
                        sql = "UPDATE kalkulacija SET editirano='0', novo='0', datum_syn=NOW() " +
                            " WHERE broj='" + r["broj"].ToString() + "' " +
                            " AND id_skladiste='" + r["id_skladiste"].ToString() + "';";

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

            string sql = "SELECT * FROM primke" +
                " WHERE is_kalkulacija = 1 AND (editirano='1') AND OIB='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "'";
            DataTable DT = Pomagala.MyWebRequestXML("sql=" + sql + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "uzmi_podatke_xml/web_request.php");

            if (DT.Rows.Count > 0)
            {
                string zadnji_broj = "", zadnja_godina = "", zadnja_poslovnica = "", is_kalkulacija = "";
                int lastId = 0;
                DateTime d;

                foreach (DataRow r in DT.Rows)
                {
                    DateTime.TryParse(r["datum"].ToString(), out d);

                    if (zadnji_broj == r["broj"].ToString() && zadnja_godina == d.Year.ToString() && zadnja_poslovnica == r["poslovnica"].ToString() && is_kalkulacija == r["is_kalkulacija"].ToString())
                    {
                        zadnji_broj = r["broj"].ToString();
                        zadnja_godina = d.Year.ToString();
                        zadnja_poslovnica = r["poslovnica"].ToString();
                        is_kalkulacija = r["is_kalkulacija"].ToString();

                        SpremiStavke(r, lastId);
                    }
                    else
                    {
                        zadnji_broj = r["broj"].ToString();
                        zadnja_godina = d.Year.ToString();
                        zadnja_poslovnica = r["poslovnica"].ToString();
                        is_kalkulacija = r["is_kalkulacija"].ToString();

                        lastId = SpremiHeader(r, DT);
                        SpremiStavke(r, lastId);
                    }

                    //**********************SQL WEB REQUEST***************************************************************************
                    sql_za_web += "UPDATE primke SET " +
                        " editirano='0' " +
                        " WHERE id='" + r["id"].ToString() + "' " +
                        " AND poslovnica='" + poslovnica + "'" +
                        " AND id_skladiste='" + r["id_skladiste"].ToString() + "'" +
                        " AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "';~";
                    //**********************SQL WEB REQUEST***************************************************************************
                }

                if (sql_za_web.Length > 4)
                {
                    sql_za_web = sql_za_web.Remove(sql_za_web.Length - 1);
                    string[] odg = Pomagala.MyWebRequest(sql_za_web + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "include/primam_post_sql_query.php").Split(';');
                }
            }
        }

        private int SpremiHeader(DataRow r, DataTable DT)
        {
            DateTime d;
            DateTime.TryParse(r["datum"].ToString(), out d);
            DateTime dNow = DateTime.Now;
            classSQL.delete("DELETE FROM kalkulacija WHERE broj = '" + r["broj"].ToString() + "';" +
                "DELETE FROM kalkulacija_stavke WHERE broj = '" + r["broj"].ToString() + "';");

            decimal fakturniIznos = 0, fakturniIznosUkupno = 0, prodajna_bez_poreza, porez, kolicina, carina, valuta, UK_bez_poreza = 0, UK_sa_porezom = 0;

            string id_zaposlenik = "0";
            //" + r["oib_zaposlenika"].ToString() + "
            //21532479840
            DataTable dtZaposlenik = (classSQL.select("SELECT COALESCE(id_zaposlenik, 0) as id_zaposlenik FROM zaposlenici WHERE oib = '" + r["oib_zaposlenika"].ToString() + "'", "zaposlenici")).Tables[0];

            if (dtZaposlenik.Rows.Count > 0 && dtZaposlenik.Rows[0]["id_zaposlenik"] != null && dtZaposlenik.Rows[0]["id_zaposlenik"].ToString().Length > 0) id_zaposlenik = dtZaposlenik.Rows[0]["id_zaposlenik"].ToString();

            //ZBRAJAM UKUPNO NA PRIMKI ILI KALKULACIJI
            DataRow[] RowsTrenutnaPrimka = DT.Select("broj='" + r["broj"].ToString() + "'");
            foreach (DataRow Rc in RowsTrenutnaPrimka)
            {
                decimal.TryParse(Rc["prodajna_cijena"].ToString().Replace(".", ","), out prodajna_bez_poreza);
                decimal.TryParse(Rc["ulazni_porez"].ToString().Replace(".", ","), out porez);
                decimal.TryParse(Rc["kolicina"].ToString().Replace(".", ","), out kolicina);
                decimal.TryParse(Rc["nabavni_iznos"].ToString().Replace(".", ","), out fakturniIznos);

                UK_bez_poreza += (prodajna_bez_poreza * kolicina);
                UK_sa_porezom += ((prodajna_bez_poreza * (1 + (porez / 100))) * kolicina);
                fakturniIznosUkupno += fakturniIznos;
            }

            decimal.TryParse(r["carina"].ToString().Replace(".", ","), out carina);
            decimal.TryParse(r["valuta"].ToString().Replace(".", ","), out valuta);

            string sql = "INSERT INTO kalkulacija (broj, id_partner, racun, otpremnica, racun_datum, otpremnica_datum,\n\t" + "mjesto_troska, datum, godina, ukupno_vpc, ukupno_mpc, tecaj, id_valuta, fakturni_iznos, id_skladiste, id_zaposlenik,\n\t" + "porez_potrosnja, novo, editirano, datum_syn)\nVALUES (" +
                "'" + r["broj"].ToString() + "', " +
                "'" + r["id_partner"].ToString() + "', " +
                "'" + r["ulazni_dok"].ToString() + "', " +
                "'', " + //otpremnica
                "'" + d.ToString("yyyy-MM-dd H:mm:ss") + "', " + //racun_datum
                "'" + d.ToString("yyyy-MM-dd H:mm:ss") + "',\n\t" + //otpremnica_datum
                "'', " + //mjesto_troska
                "'" + d.ToString("yyyy-MM-dd H:mm:ss") + "', " +
                "'" + d.ToString("yyyy") + "', " +
                "'" + Math.Round(UK_bez_poreza, 2).ToString().Replace(".", ",") + "', " +
                "'" + Math.Round(UK_sa_porezom, 2).ToString().Replace(".", ",") + "', " +
                "'1', " + //tecaj
                "'5', " + //valuta
                "'" + Math.Round(fakturniIznosUkupno, 2).ToString().Replace(".", ",") + "', " + //fakturni_iznos
                "'" + r["id_skladiste"].ToString() + "', " +
                "'" + id_zaposlenik + "',\n\t" +
                "'0', " + //porez_potrosnja
                "'0', " + //novo
                "'0', " + //editirano
                "'" + dNow.ToString("yyyy-MM-dd H:mm:ss") + "');";

            classSQL.insert(sql);

            int lastId = Convert.ToInt32(classSQL.select("SELECT MAX(id_kalkulacija) AS id FROM kalkulacija WHERE broj = '" + r["broj"].ToString() + "';", "kalkulacija").Tables[0].Rows[0]["id"]);

            return lastId;
        }

        private void SpremiStavke(DataRow r, int lastId)
        {
            decimal kolicina, fakturna, rabat, prijevoz, carina = 0, marza_postotak, porez, posebni_porez = 0, vpc;

            decimal.TryParse(r["kolicina"].ToString().Replace(".", ","), out kolicina);
            decimal.TryParse(r["cijena_po_komadu"].ToString().Replace(".", ","), out fakturna);
            decimal.TryParse(r["rabat"].ToString().Replace(".", ","), out rabat);
            decimal.TryParse(0.ToString().Replace(".", ","), out prijevoz);
            //decimal.TryParse(r["prijevoz"].ToString().Replace(".", ","), out prijevoz);
            decimal.TryParse(r["marza"].ToString().Replace(".", ","), out marza_postotak);
            decimal.TryParse(r["ulazni_porez"].ToString().Replace(".", ","), out porez);
            decimal.TryParse(r["prodajna_cijena"].ToString().Replace(".", ","), out vpc);

            string sql = "INSERT INTO kalkulacija_stavke (" +
                "kolicina, fak_cijena, rabat, prijevoz, carina, marza_postotak, porez, posebni_porez,porez_potrosnja,\n\t" +
                "broj, sifra, vpc, id_skladiste, id_kalkulacija)\n" +
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