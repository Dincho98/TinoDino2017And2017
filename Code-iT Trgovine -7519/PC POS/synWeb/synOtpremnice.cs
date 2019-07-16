using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PCPOS.synWeb
{
    internal class synOtpremnice
    {
        private synWeb.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Properties.Settings.Default.domena_za_sinkronizaciju;
        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synOtpremnice(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

        #region POŠALJI PODATKE

        public void Send()
        {
            DataTable DTposlovnica = SqlPostgres.select("SELECT * FROM ducan WHERE aktivnost='DA' LIMIT 1", "postavke").Tables[0];

            string poslovnica = "1";
            if (DTposlovnica.Rows.Count > 0)
            {
                poslovnica = DTposlovnica.Rows[0]["ime_ducana"].ToString();
            }
            //************************************GLEDA NA VARIJABLU posalji_sve ******************

            DateTime synDate = DateTime.Now;

            string filter = "";
            if (posalji_sve)
            {
                if (MessageBox.Show("Želite poslati otpremnice?", "Sinkronizacija", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                filter = "WHERE o.godina_otpremnice = '" + Util.Korisno.GodinaKojaSeKoristiUbazi.ToString() + "'";
            }
            else
            {
                filter = "WHERE o.godina_otpremnice = '" + (Util.Korisno.GodinaKojaSeKoristiUbazi).ToString() + "' AND (o.editirano = '1' OR o.novo = '1')";
            }

            //****************************************************************************
            StringBuilder sb = new StringBuilder();

            //kreira se upit na postgres db za dohvacanje podataka

            sb.Append("SELECT\n");
            sb.Append("o.godina_otpremnice AS godina,\n");
            sb.Append("o.broj_otpremnice AS broj,\n");
            sb.Append("CASE WHEN o.partner_osoba = 'P' THEN 1 ELSE 0 END AS poslovni_partner,\n");
            sb.Append("o.osoba_partner AS partner,\n");
            sb.Append("o.vrsta_dok AS id_vd,\n");
            sb.Append("o.datum,\n");
            sb.Append("o.napomena,\n");
            sb.Append("o.id_otprema as otprema,\n");
            sb.Append("o.mj_otpreme,\n");
            sb.Append("o.adr_otpreme,\n");
            sb.Append("o.isprave,\n");
            sb.Append("o.id_prijevoznik,\n");
            sb.Append("o.registracija,\n");
            sb.Append("o.istovarno_mj,\n");
            sb.Append("o.istovarni_rok,\n");
            sb.Append("CASE WHEN LENGTH(COALESCE(o.troskovi_prijevoza, '')) = 0 THEN 0 ELSE o.troskovi_prijevoza::NUMERIC END AS troskovi_prijevoza,\n");
            sb.Append("REPLACE(os.kolicina, ',', '.')::numeric AS kolicina,\n");
            sb.Append("os.vpc,\n");
            sb.Append("os.nbc::numeric AS nbc,\n");
            sb.Append("REPLACE(os.porez, ',', '.')::numeric AS porez,\n");
            sb.Append("REPLACE(os.rabat, ',', '.')::numeric AS rabat,\n");
            sb.Append("COALESCE((SELECT SUM(vpc*CAST(REPLACE(kolicina,',','.') AS numeric)) FROM otpremnica_stavke os2 " +
                " WHERE os.broj_otpremnice=os2.broj_otpremnice AND os.id_skladiste = os2.id_skladiste),0) as sumaStavki,\n");
            sb.Append("o.id_skladiste,\n");
            sb.Append("os.sifra_robe,\n");
            sb.Append("CASE WHEN UPPER(os.oduzmi) = 'DA' THEN 1 ELSE 0 END AS oduzmi,\n");
            sb.Append("CASE WHEN LENGTH(COALESCE(os.odjava, '')) = 0 THEN '' ELSE os.odjava END AS odjava,\n");
            sb.Append("CASE WHEN (os.naplaceno_fakturom) = 'T' THEN 1 ELSE 0 END AS naplaceno_fakturom,\n");
            sb.Append("o.id_izradio,\n");
            sb.Append("z.oib AS oib_zaposlenik\n");
            sb.Append("FROM otpremnice o\n");
            sb.Append("LEFT JOIN otpremnica_stavke os ON o.broj_otpremnice = os.broj_otpremnice AND os.id_skladiste = o.id_skladiste\n");
            sb.Append("LEFT JOIN zaposlenici z ON o.id_izradio = z.id_zaposlenik\n");
            if (filter.Length > 0) sb.Append(filter + "\n");
            sb.Append("ORDER BY o.datum;");

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
                    tempDel = "DELETE FROM otpremnice WHERE oib = '" + Util.Korisno.oibTvrtke + "' AND godina = '" + datum.Year.ToString() + "' and id_skladiste = '" + r["id_skladiste"].ToString() + "' and broj = '" + r["broj"].ToString() + "' and poslovnica = '" + r["poslovnica"].ToString() + "';~";

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
                    string _skladiste = DT.Rows[0]["id_skladiste"].ToString();
                    //string _poslovnica = DT.Rows[0]["poslovnica"].ToString();
                    foreach (DataRow r in DT.Rows)
                    {
                        DateTime.TryParse(r["datum"].ToString(), out datum);

                        br = "'" + r["broj"].ToString() + "'";
                        //|| _poslovnica != r["poslovnica"].ToString()
                        if ((r.Equals(DT.Rows[DT.Rows.Count - 1])) || _skladiste != r["id_skladiste"].ToString() || _god_prethodna != datum.Year)
                        {
                            if (r.Equals(DT.Rows[DT.Rows.Count - 1]))
                            {
                                // || _poslovnica != r["poslovnica"].ToString()
                                if (_skladiste != r["id_skladiste"].ToString() || _god_prethodna != datum.Year)
                                {
                                    //AND poslovnica = '" + _poslovnica + "'
                                    tempDel = "DELETE FROM otpremnice WHERE oib = '" + Util.Korisno.oibTvrtke + "' AND godina = '" + (_god_prethodna).ToString() + "'  AND id_skladiste = '" + _skladiste + "' AND broj IN (" + brFak.ToString() + ");~\n";
                                    brFak = "";
                                }
                                if (!brFak.Contains(br))
                                {
                                    if (brFak.Length > 0) { brFak += ", "; }
                                    brFak += br;
                                }
                                _skladiste = r["id_skladiste"].ToString();
                                _god_prethodna = datum.Year;
                                //_poslovnica = r["poslovnica"].ToString();
                            }

                            //AND poslovnica = '" + _poslovnica + "'
                            tempDel += "DELETE FROM otpremnice WHERE oib = '" + Util.Korisno.oibTvrtke + "' AND godina = '" + (_god_prethodna).ToString() + "'  AND id_skladiste = '" + _skladiste + "' AND broj IN (" + brFak.ToString() + ");~";

                            if (!sql.Contains(tempDel) && tempDel.Trim().Length > 0)
                            {
                                if (sql.Length > 4) sql += "\n";
                                sql += tempDel;

                                tempDel = "";
                                brFak = "";
                                brFak += br;
                            }
                        }

                        if (!brFak.Contains(br))
                        {
                            if (brFak.Length > 0) brFak += ", ";

                            brFak += br;
                        }
                        _skladiste = r["id_skladiste"].ToString();
                        _god_prethodna = datum.Year;
                        //_poslovnica = r["poslovnica"].ToString();
                    }
                }
            }

            foreach (DataRow r in DT.Rows)
            {
                DateTime.TryParse(r["datum"].ToString(), out datum);
                decimal sumaStavki;
                decimal.TryParse(r["sumaStavki"].ToString(), out sumaStavki);

                if (r["sifra_robe"].ToString() != "" && sumaStavki != 0)
                {
                    if (sql.Length > 4) sql += "\n";

                    sql += "INSERT INTO otpremnice (oib, poslovnica, godina, broj, poslovni_partner, partner, id_vd,\n\t" +
                        "datum, napomena, id_zaposlenik, otprema, mj_otpreme, adr_otpreme, isprave, id_prijevoznik, registracija, istovarno_mj, istovarni_rok, troskovi_prijevoza, kolicina,\n\t" +
                        "vpc, mpc, nbc, porez, povratna_naknada, rabat, id_skladiste, sifra, oduzmi, odjava, naplaceno_fakturom,\n\t" +
                        "novo, editirano, datum_syn, oib_zaposlenika)\nVALUES (\n\t" +
                        "'" + Util.Korisno.oibTvrtke + "'," +
                        "'" + poslovnica + "', " +
                        "'" + r["godina"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["broj"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["poslovni_partner"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["partner"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["id_vd"].ToString().TrimEnd().Replace(";", "").Replace("~", "") + "'," +
                        "'" + Convert.ToDateTime(r["datum"]).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + r["napomena"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "',\n\t" +
                        "'" + r["id_izradio"].ToString().Replace(";", "").Replace("~", "") + "'," + //tu treba promeniti zaposlenika nema tu ima nema, hahahha.
                        "'" + r["otprema"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["mj_otpreme"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["adr_otpreme"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["isprave"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["id_prijevoznik"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["registracija"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["istovarno_mj"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + Convert.ToDateTime(r["istovarni_rok"]).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + r["troskovi_prijevoza"] + "'," +
                        "'" + r["kolicina"].ToString().Replace(",", ".") + "'," +
                        "'" + r["vpc"].ToString().Replace(",", ".") + "',\n\t" +
                        "'" + (Convert.ToDecimal(r["vpc"]) * (1 + (Convert.ToDecimal(r["porez"]) / 100))).ToString().Replace(",", ".") + "'," +
                        "'" + r["nbc"].ToString().Replace(",", ".") + "'," +
                        "'" + r["porez"].ToString().Replace(",", ".") + "'," +
                        "'" + (0).ToString().Replace(",", ".") + "'," +
                        "'" + r["rabat"].ToString().Replace(",", ".") + "'," +
                        "'" + r["id_skladiste"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["sifra_robe"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["oduzmi"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["odjava"].ToString() + "'," +
                        "'" + r["naplaceno_fakturom"].ToString().Replace(",", ".").Replace(";", "").Replace("~", "") + "'," +
                        "'" + 0.ToString() + "'," +
                        "'" + 0.ToString() + "'," +
                        "'" + synDate.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        "'" + r["oib_zaposlenik"].ToString() + "');~";
                }
            }

            if (sql.Length > 4)
            {
                sql = sql.Remove(sql.Length - 1);

                //ŠALJE NA WEB i DOBIVAM ODG
                string[] odg = Pomagala.MyWebRequest(sql + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + (Util.Korisno.GodinaKojaSeKoristiUbazi), domena + "include/primam_post_sql_query.php").Split(';');

                if (odg[0] == "OK" && odg[1] == "1")
                {
                    if (DT.Rows.Count > 0)
                    {
                        foreach (DataRow r in DT.Rows)
                        {
                            DateTime.TryParse(r["datum"].ToString(), out datum);
                            sql = "UPDATE otpremnice SET editirano = false, novo = false WHERE broj_otpremnice = '" + r["broj"].ToString() + "' AND godina_otpremnice::numeric = '" + r["godina"].ToString() + "' AND id_skladiste = '" + r["id_skladiste"] + "';";

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
            string sql_za_web = "sql=";

            string sql = "SELECT *, CAST(((mpc - (mpc * (rabat/100))) * kolicina) AS DECIMAL(15,2)) AS ukupno FROM otpremnice WHERE (editirano = '1' or novo = '1') AND oib = '" + Util.Korisno.oibTvrtke + "' AND godina = '" + Util.Korisno.GodinaKojaSeKoristiUbazi.ToString() + "' ORDER BY broj;";

            DataTable DT = Pomagala.MyWebRequestXML("sql=" + sql + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "uzmi_podatke_xml/web_request.php");
            //string columntype=
            if (DT != null && DT.Rows.Count > 0)
            {
                string _zadnji_broj = "", _zadnja_godina = "", _oib = "", brFak = "", br = "", _id_skladiste = "", _poslovnica = "";
                DateTime d;

                foreach (DataRow r in DT.Rows)
                {
                    string filter = "id_skladiste = '" + r["id_skladiste"].ToString() + "' AND broj = '" + r["broj"].ToString() + "'";
                    decimal ukupno = OstaleFunkcije.getUkupnoFromTable(DT, "ukupno", filter);

                    DateTime.TryParse(r["datum"].ToString(), out d);
                    br = r["broj"].ToString().Trim();
                    if (_oib == r["oib"].ToString().Trim() && _zadnji_broj == br && _zadnja_godina == d.Year.ToString().Trim() && _id_skladiste == r["id_skladiste"].ToString() && _poslovnica == r["poslovnica"])
                    {
                        SpremiStavke(r);
                    }
                    else
                    {
                        SpremiHeader(r, DT, ukupno);
                        SpremiStavke(r);
                    }

                    //**********************SQL WEB REQUEST***************************************************************************

                    if (_oib != r["oib"].ToString().Trim() && _zadnja_godina != d.Year.ToString().Trim() && _id_skladiste != r["id_skladiste"].ToString() && _poslovnica != r["poslovnica"].ToString() && _oib.Length > 0 && _zadnja_godina.Length > 0 && _id_skladiste.Length > 0)
                    {
                        if (!brFak.Contains(br))
                        {
                            if (brFak.Length > 0) brFak += ", ";

                            brFak += br;
                        }

                        sql_za_web += "UPDATE otpremnice SET editirano = '0', novo = '0' WHERE oib='" + _oib + "' AND godina = '" + _zadnja_godina + "' AND poslovnica = '" + _poslovnica + "' AND id_skladiste = '" + _id_skladiste + "' AND broj IN (" + brFak + ");~";
                        brFak = "";
                    }

                    if (!brFak.Contains(br))
                    {
                        if (brFak.Length > 0) brFak += ", ";
                        brFak += br;
                    }

                    //**********************SQL WEB REQUEST***************************************************************************

                    _oib = r["oib"].ToString().Trim();
                    _zadnji_broj = br;
                    _zadnja_godina = d.Year.ToString().Trim();
                    _id_skladiste = r["id_skladiste"].ToString();
                    _poslovnica = r["poslovnica"].ToString();
                }

                string lastRowSql = "UPDATE otpremnice SET editirano = '0', novo = '0' WHERE oib='" + _oib + "' AND godina = '" + _zadnja_godina + "' AND id_skladiste = '" + _id_skladiste + "' AND broj IN (" + brFak + ");~";

                if (!sql_za_web.Contains(lastRowSql))
                {
                    sql_za_web += lastRowSql;
                }

                if (sql_za_web.Length > 4)
                {
                    sql_za_web = sql_za_web.Remove(sql_za_web.Length - 1);
                    //string[] odg = Pomagala.MyWebRequest("sql=" + sql_za_web + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "include/primam_post_sql_query.php").Split(';');
                    string[] odg = Pomagala.MyWebRequest(sql_za_web + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "include/primam_post_sql_query.php").Split(';');
                }
            }
        }

        private void SpremiHeader(DataRow r, DataTable DT, decimal ukupno)
        {
            DateTime d;
            DateTime.TryParse(r["datum"].ToString(), out d);

            string delete = "DELETE FROM otpremnica_stavke WHERE broj_otpremnice = '" + r["broj"].ToString() + "' and godina_otpremnice = '" + r["godina"] + "';" + "DELETE FROM otpremnice WHERE broj_otpremnice = '" + r["broj"].ToString() + "' and godina_otpremnice = '" + r["godina"] + "';";

            SqlPostgres.delete(delete);

            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO otpremnice (godina_otpremnice, id_skladiste, osoba_partner, id_odrediste, vrsta_dok, datum, id_izjava, napomena, id_kom, id_izradio, id_otprema, mj_otpreme, adr_otpreme, isprave, id_prijevoznik, registracija, istovarno_mj, istovarni_rok, troskovi_prijevoza, broj_otpremnice, partner_osoba, ukupno, novo, editirano)");
            sql.Append("VALUES ");
            sql.Append("( ");
            sql.Append("'" + r["godina"].ToString() + "', ");
            sql.Append("'" + r["id_skladiste"].ToString() + "', ");
            sql.Append("'" + r["partner"].ToString() + "', ");
            sql.Append("'" + r["partner"].ToString() + "', ");
            sql.Append("'" + r["id_vd"].ToString() + "', ");
            sql.Append("'" + Convert.ToDateTime(r["datum"]).ToString() + "', ");
            sql.Append("'" + 1.ToString() + "', ");
            sql.Append("'" + r["napomena"].ToString() + "', ");
            sql.Append("'" + 1.ToString() + "', "); //id_kom
            sql.Append("'" + 1.ToString() + "', "); //id_izradio
            sql.Append("'" + 1.ToString() + "', "); //id_otpreme
            sql.Append("'" + r["mj_otpreme"].ToString() + "', ");
            sql.Append("'" + r["adr_otpreme"].ToString() + "', ");
            sql.Append("'" + r["isprave"].ToString() + "', ");
            sql.Append("'" + r["id_prijevoznik"].ToString() + "', ");
            sql.Append("'" + r["registracija"].ToString() + "', ");
            sql.Append("'" + r["istovarno_mj"].ToString() + "', ");
            sql.Append("'" + r["istovarni_rok"].ToString() + "', ");
            sql.Append("'" + r["troskovi_prijevoza"].ToString() + "', ");
            sql.Append("'" + r["broj"].ToString() + "', ");
            sql.Append("CASE WHEN " + r["poslovni_partner"].ToString() + " = 1 THEN 'P' ELSE 'O' END, ");
            sql.Append("'" + ukupno.ToString() + "', ");
            sql.Append("false, ");
            sql.Append("false");
            sql.Append(");");

            SqlPostgres.insert(sql.ToString());
        }

        private void SpremiStavke(DataRow r)
        {
            decimal kolicina = 0, rabat = 0, vpc = 0, povratna_naknada = 0, porez = 0, nbc = 0, mpc = 0, rabat_izn = 0, mpc_rabat = 0, ukupno_rabat = 0, ukupno_vpc = 0, ukupno_mpc = 0, ukupno_mpc_rabat = 0, ukupno_porez = 0, ukupno_osnovica = 0, plusPorez = 0;

            decimal.TryParse(r["vpc"].ToString().Replace(".", ","), out vpc);
            decimal.TryParse(r["porez"].ToString().Replace(".", ","), out porez);
            plusPorez = (1 + (porez / 100));
            mpc = vpc * plusPorez;
            decimal.TryParse(r["rabat"].ToString().Replace(".", ","), out rabat);
            decimal.TryParse(r["kolicina"].ToString().Replace(".", ","), out kolicina);
            decimal.TryParse(r["nbc"].ToString().Replace(".", ","), out nbc);
            decimal.TryParse(r["povratna_naknada"].ToString().Replace(".", ","), out povratna_naknada);

            rabat_izn = mpc * (rabat / 100);
            mpc_rabat = mpc - rabat_izn;
            ukupno_rabat = kolicina * rabat_izn;
            ukupno_mpc = mpc * kolicina;
            ukupno_vpc = vpc * kolicina;
            ukupno_mpc_rabat = mpc_rabat * kolicina;
            ukupno_osnovica = ukupno_mpc_rabat / plusPorez;
            ukupno_porez = ukupno_mpc_rabat - ukupno_osnovica;

            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO otpremnica_stavke");
            sb.Append(" (");
            sb.Append("kolicina, vpc, porez, broj_otpremnice, rabat, id_skladiste, sifra_robe, oduzmi, godina_otpremnice, odjava, nbc,");
            sb.Append("\n\t");
            sb.Append("porez_potrosnja, id_otpremnice, naplaceno_fakturom");
            sb.Append(")\n");
            sb.Append("VALUES ");
            sb.Append("(");
            sb.Append("'" + kolicina.ToString() + "',");
            sb.Append(" ");
            sb.Append("'" + vpc.ToString().Replace(",", ".") + "',");
            sb.Append(" ");
            sb.Append("'" + porez.ToString() + "',");
            sb.Append(" ");
            sb.Append("'" + r["broj"].ToString() + "',");
            sb.Append(" ");
            sb.Append("'" + rabat.ToString() + "',");
            sb.Append(" ");
            sb.Append("'" + r["id_skladiste"].ToString() + "',");
            sb.Append(" ");
            sb.Append("'" + r["sifra"].ToString() + "',");
            sb.Append(" ");
            sb.Append("CASE WHEN " + r["oduzmi"].ToString() + " = 1 THEN 'DA' ELSE 'NE' END,");
            sb.Append(" ");
            sb.Append("'" + r["godina"] + "',");
            sb.Append(" ");
            sb.Append("'" + r["odjava"].ToString() + "',");
            sb.Append(" ");
            sb.Append("'" + nbc.ToString().Replace(",", ",") + "',");
            sb.Append("\n\t");
            sb.Append("'" + 0.ToString() + "',"); //porez potrosnja, to jos trebam videti kaj bude s tem.
            sb.Append(" ");
            sb.Append("'" + 1 + "',"); //id_otpremnice isto bum trebal nekak dobiti, nema tu ima nema, kuzis me.
            sb.Append(" ");
            sb.Append("CASE WHEN " + r["naplaceno_fakturom"] + " = 1 THEN TRUE ELSE FALSE END");
            sb.Append(")");
            sb.Append(";");

            SqlPostgres.insert(sb.ToString());
        }

        #endregion PRIMI PODATKE
    }
}