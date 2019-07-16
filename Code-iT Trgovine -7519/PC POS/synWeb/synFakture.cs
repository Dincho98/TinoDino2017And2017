using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PCPOS.synWeb
{
    internal class synFakture
    {
        private synWeb.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Properties.Settings.Default.domena_za_sinkronizaciju;
        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synFakture(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

        #region POŠALJI PODATKE

        public void Send()
        {
            //************************************GLEDA NA VARIJABLU posalji_sve ******************

            string filter = "";
            if (posalji_sve)
            {
                if (MessageBox.Show("Želite poslati fakture?", "Sinkronizacija", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                filter = "WHERE LEFT(f.date::varchar, 4)::numeric = " + Util.Korisno.GodinaKojaSeKoristiUbazi.ToString() + /*" AND f.id_ducan = '" + Util.Korisno.idDucan + "' AND f.id_kasa = '" + Util.Korisno.idKasa + "'*/"";
            }
            else
            {
                filter = "WHERE LEFT(f.date::varchar, 4)::numeric = " + Util.Korisno.GodinaKojaSeKoristiUbazi.ToString() + " AND (f.editirano = '1' OR f.novo = '1')" + /*AND f.id_ducan = '" + Util.Korisno.idDucan + "' AND f.id_kasa = '" + Util.Korisno.idKasa + "'*/"";
            }

            //****************************************************************************
            StringBuilder sb = new StringBuilder();

            //kreira se upit na postgres db za dohvacanje podataka

            sb.Append("SELECT\n");
            sb.Append("f.broj_fakture AS broj,\n");
            sb.Append("f.id_fakturirati AS id_kupac,\n");
            sb.Append("f.date AS datum,\n");
            sb.Append("f.datedvo AS datum_dvo,\n");
            sb.Append("f.datum_valute AS datum_valute,\n");
            sb.Append("f.id_zaposlenik AS id_zaposlenik,\n");
            sb.Append("f.model AS model,\n");
            sb.Append("CASE WHEN f.id_nacin_placanja = 1 THEN 'G' ELSE\n\t");
            sb.Append("CASE WHEN f.id_nacin_placanja = 2 THEN 'K' ELSE\n\t\t");
            sb.Append("CASE WHEN f.id_nacin_placanja = 3 THEN 'T' ELSE 'O' END\n\t");
            sb.Append("END\n");
            sb.Append("END::char(1) as placanje,\n");
            sb.Append("COALESCE((REPLACE(v.tecaj, ',','.')::NUMERIC), 0) AS tecaj,\n");
            sb.Append("f.napomena::text AS napomena,\n");
            sb.Append("f.jir::varchar(100) AS jir,\n");
            sb.Append("f.zki::varchar(100) AS zki,\n");
            sb.Append("CASE WHEN f.storno = 'DA' THEN '1' ELSE '0' END AS storno,\n");
            sb.Append("CASE WHEN f.novo = TRUE THEN '1' ELSE '0' END AS novo,\n");
            sb.Append("CASE WHEN f.editirano = TRUE THEN '1' ELSE '0' END AS editirano,\n");
            sb.Append("NOW() AS datum_syn,\n");
            sb.Append("fs.sifra::varchar(30) AS sifra,\n");
            sb.Append("fs.vpc AS vpc,\n");
            sb.Append("COALESCE((REPLACE(fs.porez, ',', '.')::NUMERIC), 0) AS porez,\n");
            sb.Append("COALESCE((REPLACE(fs.rabat, ',', '.')::NUMERIC), 0) AS rabat,\n");
            sb.Append("COALESCE((REPLACE(fs.kolicina, ',','.')::NUMERIC), 0) AS kolicina,\n");
            sb.Append("fs.povratna_naknada AS povratna_naknada,\n");
            sb.Append("fs.id_skladiste AS id_skladiste,\n");
            sb.Append("CASE WHEN fs.oduzmi = 'DA' THEN '1' ELSE '0' END AS oduzmi,\n");
            sb.Append("fs.nbc::NUMERIC AS nbc,\n");
            sb.Append("(fs.vpc * (1 zbroj COALESCE((REPLACE(porez,',', '.')::NUMERIC), 0) / 100)) AS mpc,\n");
            sb.Append("f.zr AS zr,\n");
            sb.Append("f.id_valuta AS id_valuta,\n");
            sb.Append("f.id_vd::varchar(5) AS id_vd,\n");
            sb.Append("f.broj_ispisa as broj_ispisa,\n");
            sb.Append("CASE WHEN LOWER(fs.odjava) = 't' THEN '1' ELSE CASE WHEN LOWER(fs.odjava) = 'f' THEN '0' ELSE 'NULL' END END as odjava,\n");
            sb.Append("f.id_predujam as id_predujam,\n");
            sb.Append("f.ukupno::NUMERIC as ukupno,\n");
            sb.Append("f.mj_troska as mj_troska,\n");
            sb.Append("ducan.ime_ducana as ducan,\n");
            sb.Append("blagajna.ime_blagajne as blagajna,\n");
            sb.Append("COALESCE((SELECT SUM(vpc*CAST(REPLACE(kolicina,',','.') AS numeric)) FROM faktura_stavke fs2 " +
                " WHERE fs.broj_fakture=fs2.broj_fakture AND fs.id_ducan = fs2.id_ducan AND fs.id_kasa = fs2.id_kasa),0) as sumaStavki,\n");
            sb.Append("f.id_ducan,\n");
            sb.Append("f.id_kasa,\n");
            sb.Append("CASE WHEN f.broj_avansa::VARCHAR != '' THEN f.broj_avansa ELSE 0 END as broj_avansa,\n");
            sb.Append("f.stavke_u_valuti as stavke_u_valuti,\n");
            sb.Append("COALESCE(REPLACE(fs.porez_potrosnja, ',', '.')::NUMERIC, 0) as porez_potrosnja,\n");
            sb.Append("z.oib as oib_zaposlenika\n");
            sb.Append("FROM fakture f\n");
            sb.Append("LEFT JOIN valute v ON f.id_valuta = v.id_valuta\n");
            sb.Append("LEFT JOIN faktura_stavke fs ON f.broj_fakture = fs.broj_fakture AND f.id_ducan = fs.id_ducan AND f.id_kasa = fs.id_kasa\n");
            sb.Append("LEFT JOIN zaposlenici z ON f.id_zaposlenik = z.id_zaposlenik\n");
            sb.Append("LEFT JOIN ducan ON f.id_ducan = ducan.id_ducan\n");
            sb.Append("LEFT JOIN blagajna ON f.id_kasa = blagajna.id_blagajna\n");
            if (filter.Length > 0) sb.Append(filter + "\n");
            sb.Append("ORDER BY f.date;");

            DataTable DT = SqlPostgres.select(sb.ToString(), "TABLE").Tables[0];

            string sql = "sql=";
            string tempDel = "", brFak = "";
            DateTime datum;

            //PRVO PROVJERIM SVE DOKUMENTE KOJI SU U PROGRAMU MIJENJANI I BRIŠEM ISTE NA WEBU
            if (posalji_sve)
            {
                foreach (DataRow r in DT.Rows)
                {
                    DateTime.TryParse(r["datum"].ToString(), out datum);
                    tempDel = "DELETE FROM fakture WHERE oib = '" + Util.Korisno.oibTvrtke + "' AND YEAR(datum) = '" + datum.Year.ToString() + "' AND poslovnica = '" + r["ducan"].ToString() + "'  and naplatni_uredaj = '" + r["blagajna"].ToString() + "';~";

                    if (!sql.Contains(tempDel) && tempDel.Trim().Length > 0)
                    {
                        if (sql.Length > 4) sql += "\n";
                        sql += tempDel;
                    }
                }
            }
            else
            {
                int _god_prethodna = 0;

                if (DT.Rows.Count > 0)
                {
                    string br = "";
                    DateTime.TryParse(DT.Rows[0]["datum"].ToString(), out datum);
                    _god_prethodna = datum.Year;

                    foreach (DataRow r in DT.Rows)
                    {
                        if (r["editirano"].ToString() == "1")
                        {
                            DateTime.TryParse(r["datum"].ToString(), out datum);

                            br = "'" + r["broj"].ToString() + "'";

                            if (!brFak.Contains(br))
                            {
                                if (brFak.Length > 0) brFak += ", ";

                                brFak += br;
                            }

                            if (r.Equals(DT.Rows[DT.Rows.Count - 1]))
                            {
                                tempDel = "DELETE FROM fakture WHERE oib = '" + Util.Korisno.oibTvrtke + "' AND YEAR(datum) = '" + datum.Year.ToString() + "' AND broj IN (" + brFak.ToString() + ") AND poslovnica = '" + r["ducan"].ToString() + "' AND naplatni_uredaj = '" + r["blagajna"].ToString() + "';~";

                                if (!sql.Contains(tempDel) && tempDel.Trim().Length > 0)
                                {
                                    if (sql.Length > 4) sql += "\n";
                                    sql += tempDel;

                                    brFak = "";
                                }
                            }
                        }
                    }
                }
            }

            foreach (DataRow r in DT.Rows)
            {
                DateTime.TryParse(r["datum"].ToString(), out datum);
                decimal sumaStavki;
                decimal.TryParse(r["sumaStavki"].ToString(), out sumaStavki);
                string sukp = r["nbc"].ToString();

                if (r["sifra"].ToString() != "" && sumaStavki != 0)
                {
                    if (sql.Length > 4) sql += "\n";

                    sql += "INSERT INTO fakture (broj, id_kupac, datum, datum_dvo, datum_valute, id_zaposlenik, model, placanje,\n\t" +
                        "tecaj, napomena, jir, zki, storno, poslovnica, naplatni_uredaj, novo, editirano, datum_syn, sifra, vpc, porez,\n\t" +
                        "rabat, kolicina, povratna_naknada, id_skladiste, oduzmi, nbc, mpc, oib, zr, id_valuta,\n\t" +
                        "id_vd, broj_ispisa, odjava, id_predujam, ukupno, mj_troska, broj_avansa, stavke_u_valuti, porez_potrosnja, oib_zaposlenika,otprema)\nVALUES (\n\t" +
                        "'" + r["broj"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["id_kupac"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + datum.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        "'" + Convert.ToDateTime(r["datum_dvo"]).ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        "'" + Convert.ToDateTime(r["datum_valute"]).ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        "'" + r["id_zaposlenik"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["model"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["placanje"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["tecaj"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["napomena"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "',\n\t" +
                        "'" + r["jir"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["zki"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["storno"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["ducan"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["blagajna"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'0'," +
                        "'0'," +
                        "'" + Convert.ToDateTime(r["datum_syn"]).ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        "'" + r["sifra"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["vpc"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "',\n\t" +
                        "'" + r["porez"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["rabat"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["kolicina"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["povratna_naknada"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["id_skladiste"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["oduzmi"].ToString() + "'," +
                        "'" + r["nbc"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["mpc"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "', " +
                        "'" + Util.Korisno.oibTvrtke + "', " +
                        "'" + r["zr"].ToString() + "',\n\t" +
                        "'" + r["id_valuta"].ToString() + "', " +
                        "'" + r["id_vd"].ToString() + "', " +
                        "'" + r["broj_ispisa"].ToString() + "', " +
                        "'0', " +
                        "'" + r["id_predujam"].ToString() + "', " +
                        "'" + r["ukupno"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "', " +
                        "'" + r["mj_troska"].ToString() + "', " +
                        "'" + r["broj_avansa"].ToString() + "', " +
                        "'" + r["stavke_u_valuti"].ToString() + "', " +
                        "'" + r["porez_potrosnja"].ToString() + "', " +
                        "'" + r["oib_zaposlenika"].ToString() + "','1');~";
                }
            }

            if (sql.Length > 4)
            {
                sql = sql.Remove(sql.Length - 1);

                //ŠALJE NA WEB i DOBIVAM ODG
                string[] odg = Pomagala.MyWebRequest(sql + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "include/primam_post_sql_query.php").Split(';');

                if (odg[0] == "OK" && odg[1] == "1")
                {
                    if (DT.Rows.Count > 0)
                    {
                        foreach (DataRow r in DT.Rows)
                        {
                            DateTime.TryParse(r["datum"].ToString(), out datum);
                            sql = "UPDATE fakture SET editirano = '0', novo = '0' WHERE broj_fakture = '" + r["broj"].ToString() + "' AND id_ducan = '" + r["id_ducan"].ToString() + "' AND LEFT(date::varchar, 4)::numeric = '" + datum.Year.ToString() + "' AND id_kasa = '" + r["id_kasa"].ToString() + "';";

                            SqlPostgres.update(sql);
                        }
                    }
                }
            }
        }

        #endregion POŠALJI PODATKE

        #region PRIMI PODATKE

        public void UzmiPodatkeSaWeba()
        {
            DateTime dtSyn = DateTime.Now;

            string sql_za_web = "sql=";

            string sql = "SELECT * FROM fakture WHERE (editirano = '1' OR novo = '1') AND OIB = '" + Util.Korisno.oibTvrtke + "' AND poslovnica = '" + Util.Korisno.nazivPoslovnica + "' AND YEAR(datum) = '" + Util.Korisno.GodinaKojaSeKoristiUbazi.ToString() + "';";

            DataTable DT = Pomagala.MyWebRequestXML("sql=" + sql + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "uzmi_podatke_xml/web_request.php");

            if (DT.Rows.Count > 0)
            {
                string _zadnji_broj = "", _zadnja_godina = "", _zadnja_poslovnica = "", _oib = "", _naplatni_uredaj = "", brFak = "", br = "";
                DateTime d;

                foreach (DataRow r in DT.Rows)
                {
                    DateTime.TryParse(r["datum"].ToString(), out d);
                    br = r["broj"].ToString().Trim();
                    if (_oib == r["oib"].ToString().Trim() && _zadnji_broj == br && _zadnja_godina == d.Year.ToString().Trim() && _zadnja_poslovnica == r["poslovnica"].ToString().Trim() && _naplatni_uredaj == r["naplatni_uredaj"].ToString().Trim())
                    {
                        SpremiStavke(r);
                    }
                    else
                    {
                        _oib = r["oib"].ToString().Trim();
                        _zadnji_broj = br;
                        _zadnja_godina = d.Year.ToString().Trim();
                        _zadnja_poslovnica = r["poslovnica"].ToString().Trim();
                        _naplatni_uredaj = r["naplatni_uredaj"].ToString().Trim();

                        SpremiHeader(r, DT, dtSyn);
                        SpremiStavke(r);
                    }

                    //**********************SQL WEB REQUEST***************************************************************************

                    if (!brFak.Contains(br))
                    {
                        if (brFak.Length > 0) brFak += ", ";

                        brFak += br;
                    }

                    //**********************SQL WEB REQUEST***************************************************************************
                }
                sql_za_web = "UPDATE fakture SET editirano = '0', novo = '0' WHERE oib='" + _oib + "' AND broj IN (" + brFak + ") AND poslovnica='" + _zadnja_poslovnica + "' AND naplatni_uredaj = " + _naplatni_uredaj + ";~";
                if (sql_za_web.Length > 4)
                {
                    sql_za_web = sql_za_web.Remove(sql_za_web.Length - 1);
                    string[] odg = Pomagala.MyWebRequest("sql=" + sql_za_web + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "include/primam_post_sql_query.php").Split(';');
                }
            }
        }

        private void SpremiHeader(DataRow r, DataTable DT, DateTime dtSyn)
        {
            DateTime d;
            DateTime.TryParse(r["datum"].ToString(), out d);

            //DADANO
            string delete = "DELETE FROM faktura_stavke WHERE broj_fakture = '" + r["broj"].ToString() + "' AND id_ducan = '" + Util.Korisno.VratiIDPremaImenuDucana(r["poslovnica"].ToString()) + "' AND id_kasa = '" + Util.Korisno.VratiIDPremaImenuBlagajne(r["naplatni_uredaj"].ToString()) + "'; " +
                "DELETE FROM fakture WHERE broj_fakture = '" + r["broj"].ToString() + "' AND id_ducan = '" + Util.Korisno.VratiIDPremaImenuDucana(r["poslovnica"].ToString()) + "' AND id_kasa = '" + Util.Korisno.VratiIDPremaImenuBlagajne(r["naplatni_uredaj"].ToString()) + "';";

            SqlPostgres.delete(delete);

            StringBuilder sql = new StringBuilder();
            string ukp = r["ukupno"].ToString().Replace(".", ",");
            decimal ikp = Convert.ToDecimal(ukp);
            sql.Append("INSERT INTO fakture (broj_fakture, id_odrediste, id_fakturirati, date, datedvo, datum_valute, id_izjava, id_zaposlenik, id_zaposlenik_izradio, model, id_nacin_placanja, zr, id_valuta, otprema, id_predujam, napomena, ukupno, id_vd, godina_predujma, godina_ponude, godina_fakture, mj_troska, oduzmi_iz_skladista, jir, zki, tecaj, storno, ukupno_rabat, ukupno_porez, ukupno_osnovica, ukupno_mpc, ukupno_vpc, ukupno_mpc_rabat, ukupno_povratna_naknada, broj_avansa, godina_avansa, stavke_u_valuti, id, id_ducan, id_kasa, broj_ispisa, novo, editirano, datum_syn)");
            sql.Append("VALUES ");
            sql.Append("( ");
            sql.Append("'" + r["broj"].ToString() + "', ");
            sql.Append("'" + r["id_kupac"].ToString() + "', ");
            sql.Append("'" + r["id_kupac"].ToString() + "', ");
            sql.Append("'" + Convert.ToDateTime(r["datum"]).ToString() + "', ");
            sql.Append("'" + Convert.ToDateTime(r["datum_dvo"]).ToString() + "', ");
            sql.Append("'" + Convert.ToDateTime(r["datum_valute"]).ToString() + "', ");
            sql.Append("'" + 1.ToString() + "', ");
            sql.Append("'" + r["id_zaposlenik"].ToString() + "', ");
            sql.Append("'" + r["id_zaposlenik"].ToString() + "', ");
            sql.Append("'" + r["model"].ToString() + "', ");
            sql.Append("CASE WHEN '" + r["placanje"].ToString() + "' = 'G' THEN 1 ELSE ");     //id_nacin_placanja----------
            sql.Append("CASE WHEN '" + r["placanje"].ToString() + "' = 'K' THEN 2 ELSE ");
            sql.Append("CASE WHEN '" + r["placanje"].ToString() + "' = 'T' THEN 3 ELSE 4 END ");
            sql.Append("END ");
            sql.Append("END, ");
            sql.Append("'" + r["zr"].ToString() + "', ");
            sql.Append("'" + r["id_valuta"].ToString() + "', ");
            sql.Append("'" + r["otprema"].ToString() + "', ");
            sql.Append("'" + r["id_predujam"].ToString() + "', ");
            sql.Append("'" + r["napomena"].ToString().Trim() + "', ");
            sql.Append("'" + Convert.ToDecimal(r["ukupno"].ToString().Replace(".", ",")).ToString() + "', ");
            sql.Append("'" + r["id_vd"].ToString() + "', ");
            sql.Append("'" + Convert.ToDateTime(r["datum"]).Year.ToString() + "', ");
            sql.Append("'" + Convert.ToDateTime(r["datum"]).Year.ToString() + "', ");
            sql.Append("'" + Convert.ToDateTime(r["datum"]).Year.ToString() + "', ");
            sql.Append("'" + r["mj_troska"].ToString() + "', ");
            sql.Append("'" + r["oduzmi"].ToString() + "', ");
            sql.Append("'" + r["jir"].ToString() + "', ");
            sql.Append("'" + r["zki"].ToString() + "', ");
            sql.Append("'" + r["tecaj"].ToString() + "', ");
            sql.Append("CASE WHEN '" + r["storno"].ToString() + "' = '1' THEN 'DA' ELSE 'NE' END, ");                   //storno
            sql.Append("'" + 0.ToString() + "', ");                             //ukupno_rabat
            sql.Append("'" + 0.ToString() + "', ");                             //ukupno_porez
            sql.Append("'" + 0.ToString() + "', ");                             //ukupno_osnovica
            sql.Append("'" + 0.ToString() + "', ");                             //ukupno_mpc
            sql.Append("'" + 0.ToString() + "', ");                             //ukupno_vpc
            sql.Append("'" + 0.ToString() + "', ");                             //ukupno_mpc_rabat
            sql.Append("'" + 0.ToString() + "', ");                             //ukupno_povratna_naknada
            sql.Append("'" + r["broj_avansa"].ToString() + "', ");
            sql.Append("'" + Convert.ToDateTime(r["datum"]).Year.ToString() + "', ");
            sql.Append("'" + r["stavke_u_valuti"].ToString() + "', ");
            sql.Append("'" + r["id"].ToString() + "', ");
            sql.Append("'" + Util.Korisno.VratiIDPremaImenuDucana(r["poslovnica"].ToString()) + "', ");
            sql.Append("'" + Util.Korisno.VratiIDPremaImenuBlagajne(r["naplatni_uredaj"].ToString()) + "', ");
            sql.Append("'" + r["broj_ispisa"].ToString() + "', ");
            sql.Append("'" + r["novo"].ToString() + "', ");
            sql.Append("'" + r["editirano"].ToString() + "', ");
            sql.Append("'" + dtSyn.ToString("yyyy-MM-dd H:mm:ss") + "' ");
            sql.Append(");");

            SqlPostgres.insert(sql.ToString());
        }

        private void SpremiStavke(DataRow r)
        {
            decimal kolicina = 0, rabat = 0, vpc = 0, povratna_naknada = 0, porez = 0, nbc = 0, mpc = 0, rabat_izn = 0, mpc_rabat = 0, ukupno_rabat = 0, ukupno_vpc = 0, ukupno_mpc = 0, ukupno_mpc_rabat = 0, ukupno_porez = 0, ukupno_osnovica = 0, plusPorez = 0, porez_potrosnja = 0;

            decimal.TryParse(r["vpc"].ToString().Replace(".", ","), out vpc);
            decimal.TryParse(r["porez"].ToString().Replace(".", ","), out porez);
            plusPorez = (1 + (porez / 100));
            mpc = vpc * plusPorez;
            decimal.TryParse(r["rabat"].ToString().Replace(".", ","), out rabat);
            decimal.TryParse(r["kolicina"].ToString().Replace(".", ","), out kolicina);
            decimal.TryParse(r["nbc"].ToString().Replace(".", ","), out nbc);
            decimal.TryParse(r["povratna_naknada"].ToString().Replace(".", ","), out povratna_naknada);
            decimal.TryParse(r["porez_potrosnja"].ToString().Replace(".", ","), out porez_potrosnja);

            rabat_izn = mpc * (rabat / 100);
            mpc_rabat = mpc - rabat_izn;
            ukupno_rabat = kolicina * rabat_izn;
            ukupno_mpc = mpc * kolicina;
            ukupno_vpc = vpc * kolicina;
            ukupno_mpc_rabat = mpc_rabat * kolicina;
            ukupno_osnovica = ukupno_mpc_rabat / plusPorez;
            ukupno_porez = ukupno_mpc_rabat - ukupno_osnovica;

            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO faktura_stavke");
            sb.Append(" (");
            sb.Append(" ");
            sb.Append("kolicina, vpc, porez, broj_fakture, rabat, id_skladiste, sifra, oduzmi, odjava, nbc,");
            sb.Append("\n\t");
            sb.Append("porez_potrosnja, povratna_naknada, rabat_izn, mpc_rabat, ukupno_rabat, ukupno_vpc, ukupno_mpc, ukupno_mpc_rabat, povratna_naknada_izn, ukupno_porez,");
            sb.Append("\n\t");
            sb.Append("ukupno_osnovica, id_ducan, id_kasa");
            sb.Append(" )\n");
            sb.Append("VALUES ");
            sb.Append("(");
            sb.Append(" ");
            sb.Append("'" + Math.Round(kolicina, 4).ToString() + "',");
            sb.Append(" ");
            sb.Append("'" + Math.Round(vpc, 4).ToString().Replace(",", ".") + "',");
            sb.Append(" ");
            sb.Append("'" + porez.ToString() + "',");
            sb.Append(" ");
            sb.Append("'" + r["broj"].ToString() + "',");
            sb.Append(" ");
            sb.Append("'" + Math.Round(rabat, 4).ToString() + "',");
            sb.Append(" ");
            sb.Append("'" + r["id_skladiste"].ToString() + "',");
            sb.Append(" ");
            sb.Append("'" + r["sifra"].ToString() + "',");
            sb.Append(" ");
            sb.Append("CASE WHEN " + r["oduzmi"].ToString() + " = 1 THEN 'DA' ELSE 'NE' END,");
            sb.Append(" ");
            sb.Append("'" + r["odjava"] + "',");
            sb.Append(" ");
            sb.Append("'" + Math.Round(nbc, 4).ToString() + "',");
            sb.Append("\n\t");
            sb.Append("'" + Math.Round(porez_potrosnja, 2).ToString() + "',");
            sb.Append(" ");
            sb.Append("'" + Math.Round(povratna_naknada, 4).ToString().Replace(",", ".") + "',");
            sb.Append(" ");
            sb.Append("'" + Math.Round(rabat_izn, 4).ToString().Replace(",", ".") + "',");
            sb.Append(" ");
            sb.Append("'" + Math.Round(mpc_rabat, 4).ToString().Replace(",", ".") + "',");
            sb.Append(" ");
            sb.Append("'" + Math.Round(ukupno_rabat, 4).ToString().Replace(",", ".") + "',");
            sb.Append(" ");
            sb.Append("'" + Math.Round(ukupno_vpc, 4).ToString().Replace(",", ".") + "',");
            sb.Append(" ");
            sb.Append("'" + Math.Round(ukupno_mpc, 4).ToString().Replace(",", ".") + "',");
            sb.Append(" ");
            sb.Append("'" + Math.Round(ukupno_mpc_rabat, 4).ToString().Replace(",", ".") + "',");
            sb.Append(" ");
            sb.Append("'" + Math.Round((kolicina * povratna_naknada), 4).ToString().Replace(",", ".") + "',");
            sb.Append(" ");
            sb.Append("'" + Math.Round(ukupno_porez, 4).ToString().Replace(",", ".") + "',");
            sb.Append("\n\t");
            sb.Append("'" + Math.Round(ukupno_osnovica, 4).ToString().Replace(",", ".") + "',");
            sb.Append(" ");
            sb.Append("'" + Util.Korisno.VratiIDPremaImenuDucana(r["poslovnica"].ToString()) + "',");
            sb.Append(" ");
            sb.Append("'" + Util.Korisno.VratiIDPremaImenuBlagajne(r["naplatni_uredaj"].ToString()) + "'\n");
            sb.Append(")");
            sb.Append(";");

            SqlPostgres.insert(sb.ToString());
        }

        #endregion PRIMI PODATKE
    }
}