using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.synWeb
{
    internal class synPartner
    {
        private synWeb.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Properties.Settings.Default.domena_za_sinkronizaciju;
        private string poslovnica = "1";
        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synPartner(bool _posalji_sve)
        {
            posalji_sve = _posalji_sve;
        }

        #region Ovdje šaljem partnere

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

            //************************************GLEDA NA VARIJABLU posalji_sve ******************
            string filter = "";
            if (posalji_sve)
            {
                if (MessageBox.Show("Želite poslati partnere?", "Sinkronizacija", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                filter = "";
            }
            else
            {
                filter = "  WHERE (partners.editirano='1' OR partners.novo='1') ";
            }
            //************************************************************************************

            query = "SELECT * FROM partners" +
                " LEFT JOIN grad ON grad.id_grad = partners.id_grad " +
                " LEFT JOIN zemlja ON zemlja.id_zemlja = partners.id_zemlja " +
                filter;

            DataTable DT = SqlPostgres.select(query, "TABLE").Tables[0];

            string sql = "";
            string tempDel = "";
            int count_sql = 0;

            //PRVO PROVJERIM SVE DOKUMENTE KOJI SU U PROGRAMU MIJENJANI I BRIŠEM ISTE NA WEBU
            foreach (DataRow r in DT.Rows)
            {
                if (r["editirano"].ToString().ToUpper() == "TRUE" || r["novo"].ToString().ToUpper() == "TRUE")
                {
                    tempDel = "DELETE FROM partneri WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "'" +
                        " AND poslovnica='" + poslovnica + "' AND id_partner='" + r["id_partner"].ToString() + "';~";

                    if (!sql.Contains(tempDel))
                    {
                        sql += tempDel;
                    }
                }
                else if (posalji_sve)
                {
                    tempDel = "DELETE FROM partneri WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "'" +
                        " AND poslovnica='" + poslovnica + "';~";
                    sql += tempDel;
                    break;
                }

                if (!sql.Contains(tempDel))
                {
                    sql += tempDel;
                }
            }

            if (sql.Length > 4)
            {
                int editirano = 0;
                int novo = 0;
                int id_partner = 0;
                foreach (DataRow r in DT.Rows)
                {
                    int.TryParse(r["id_partner"].ToString(), out id_partner);

                    sql += "INSERT INTO partneri (id_partner, ime_tvrtke, grad, adresa, oib_tvrtke," +
                        "drazava, ime, prezime, email, tel, editirano, novo, oib, poslovnica,datum_syn) VALUES (" +
                        "'" + id_partner.ToString() + "'," +
                        "'" + r["ime_tvrtke"].ToString().Replace(";", "").Replace("~", "").Replace("'", "").Replace("\"", "") + "'," +
                        "'" + r["grad"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["adresa"].ToString().Replace(";", "").Replace("~", "").Replace("'", "").Replace("\"", "") + "'," +
                        "'" + r["oib"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["zemlja"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["ime"].ToString().Replace(";", "").Replace("~", "").Replace("'", "").Replace("\"", "") + "'," +
                        "'" + r["prezime"].ToString().Replace(";", "").Replace("~", "").Replace("'", "").Replace("\"", "") + "'," +
                        "'" + r["email"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + r["tel"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'0'," +
                        "'0'," +
                        "'" + DTpodaci_tvrtka.Rows[0]["oib"].ToString().Replace(";", "").Replace("~", "") + "'," +
                        "'" + poslovnica + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                        ");~";

                    count_sql++;
                    if (count_sql > 500)
                    {
                        sql += "Ł";
                        count_sql = 0;
                    }
                }

                string[] arrSql = sql.Split('Ł');
                bool sql_je_ispravan = true;

                foreach (string ___sql in arrSql)
                {
                    string sql_finish = "sql=" + ___sql.Remove(___sql.Length - 1);

                    //ŠALJE NA WEB i DOBIVAM ODG
                    string[] odg = Pomagala.MyWebRequest(sql_finish.Replace("&", " and ") + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "include/primam_post_sql_query.php").Split(';');
                    if (odg[0] != "OK" || odg[1] != "1")
                    {
                        sql_je_ispravan = false;
                    }
                }

                if (sql_je_ispravan)
                {
                    sql = "UPDATE partners SET editirano='0', novo='0';";
                    SqlPostgres.update(sql);
                }
            }
        }

        #endregion Ovdje šaljem partnere

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

                string sql_za_web = "sql=";
                string query = "SELECT * FROM partneri WHERE (novo='1' OR editirano='1') " +
                    "AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "'";
                DataTable DT = Pomagala.MyWebRequestXML("sql=" + query + "&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "uzmi_podatke_xml/web_request.php");

                if (DT.Rows.Count > 0)
                {
                    string sql = "";
                    foreach (DataRow r in DT.Rows)
                    {
                        bool novo = r["novo"].ToString() == "1" ? true : false;
                        bool editirano = r["editirano"].ToString() == "1" ? true : false; ;

                        int[] arrMjesto = VratiIDGradIDZupaniju(r["grad"].ToString());
                        int idGrad = arrMjesto[0];
                        int idPartner = VratiIDpartnera(r["id_partner"].ToString());
                        int idZupanija = arrMjesto[1];

                        if (novo)
                        {
                            if (idPartner != -1)
                            {
                                sql += "INSERT INTO partners (id_partner,ime_tvrtke,id_grad,adresa,oib,napomena," +
                                               "id_djelatnost,ime,prezime,email,tel,mob,datum_rodenja,bodovi," +
                                               "popust,broj_kartice,aktivan,vrsta_korisnika,primanje_letaka,id_zupanija, oib_polje,datum_syn,id_zemlja) VALUES (" +
                                               "'" + idPartner + "'," +
                                               "'" + r["ime_tvrtke"].ToString().Replace(";", "").Replace("~", "").Replace("'", "").Replace("\"", "") + "'," +
                                               "'" + idGrad.ToString() + "'," +
                                               "'" + r["adresa"].ToString().Replace(";", "").Replace("~", "").Replace("'", "").Replace("\"", "") + "'," +
                                               "'" + r["oib_tvrtke"].ToString().Replace(";", "").Replace("~", "").Replace("'", "").Replace("\"", "") + "'," +
                                               "''," +
                                               "'1'," +
                                               "'" + r["ime"].ToString().Replace(";", "").Replace("~", "").Replace("'", "").Replace("\"", "") + "'," +
                                               "'" + r["prezime"].ToString().Replace(";", "").Replace("~", "").Replace("'", "").Replace("\"", "") + "'," +
                                               "''," +
                                               "'" + r["tel"].ToString().Replace(";", "").Replace("~", "").Replace("'", "").Replace("\"", "") + "'," +
                                               "''," +
                                               "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                                               "'0'," +
                                               "'0'," +
                                               "'0'," +
                                               "'1'," +
                                               "'1'," +
                                               "'1'," +
                                               "'" + idZupanija + "'," +
                                               "'OIB', " +
                                               "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                                               "'60'" +
                                               ");";
                            }
                        }

                        if (editirano && !novo)
                        {
                            sql += "UPDATE partners SET " +
                                   "ime_tvrtke='" + r["ime_tvrtke"].ToString().Replace(";", "").Replace("~", "").Replace("'", "").Replace("\"", "") + "'," +
                                   "id_grad='" + idGrad.ToString() + "'," +
                                   "adresa='" + r["adresa"].ToString().Replace(";", "").Replace("~", "").Replace("'", "").Replace("\"", "") + "'," +
                                   "oib='" + r["oib_tvrtke"].ToString().Replace(";", "").Replace("~", "").Replace("'", "").Replace("\"", "") + "'," +
                                   "ime='" + r["ime"].ToString().Replace(";", "").Replace("~", "").Replace("'", "").Replace("\"", "") + "'," +
                                   "prezime='" + r["prezime"].ToString().Replace(";", "").Replace("~", "").Replace("'", "").Replace("\"", "") + "'," +
                                   "tel='" + r["tel"].ToString().Replace(";", "").Replace("~", "").Replace("'", "").Replace("\"", "") + "'," +
                                   "datum_syn='" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                                   "id_zupanija='" + idZupanija + "' WHERE id_partner='" + r["id_partner"].ToString() + "';";
                        }

                        //**********************SQL WEB REQUEST**************************************************************************************************************************
                        sql_za_web += "UPDATE partneri SET novo='0', editirano='0' " +
                            " WHERE id_partner='" + r["id_partner"].ToString() + "' AND oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "';~";
                        //**********************SQL WEB REQUEST**************************************************************************************************************************
                    }

                    if (sql_za_web.Length > 4)
                    {
                        sql_za_web = sql_za_web.Remove(sql_za_web.Length - 1);
                        string[] odg = Pomagala.MyWebRequest(sql_za_web + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "include/primam_post_sql_query.php").Split(';');

                        if (odg[0] == "OK" && odg[1] == "1")
                            SqlPostgres.insert(sql);
                    }
                }
            }
            catch
            {
            }
        }

        #endregion PRIMI PODATKE

        #region Vrati ID Grad ID Zupaniju

        private int[] VratiIDGradIDZupaniju(string grad)
        {
            //POTREBNO ZA ID_GRAD
            //POTREBNO ZA ŽUPANIJU
            int[] vrati = new int[2];

            DataTable DT = SqlPostgres.select("SELECT * FROM grad WHERE grad ~* '" + grad + "';", "grad").Tables[0];
            if (DT.Rows.Count > 0)
            {
                int.TryParse(DT.Rows[0]["id_grad"].ToString(), out vrati[0]);
                DataTable DTzup = SqlPostgres.select("SELECT * FROM zupanije WHERE naziv ~* '" + DT.Rows[0]["zupanija"].ToString().Trim() + "';", "zup").Tables[0];
                if (DTzup.Rows.Count > 0)
                    int.TryParse(DTzup.Rows[0]["id_zupanija"].ToString(), out vrati[1]);
                else
                    vrati[1] = 20;

                return vrati;
            }

            vrati[0] = 1;
            vrati[1] = 18;
            return vrati;
        }

        #endregion Vrati ID Grad ID Zupaniju

        #region Vrati ID partnera

        private int VratiIDpartnera(string id)
        {
            int vrati = 1;

            if (id != null)
            {
                DataTable DT = SqlPostgres.select("SELECT * FROM partners WHERE id_partner='" + id + "';", "zaposlenik").Tables[0];
                if (DT.Rows.Count == 0)
                {
                    //AKO NEMAM U BAZI ID VRAČAM TRAŽENI ID I NE IZMJENJUJEM ISTOG.
                    int.TryParse(id, out vrati);
                    return vrati;
                }
                else
                {
                    //AKO VEČ POSTOJI TRAŽENI ID, JAVLJAM GREŠKU SA -1 I NE ŽELIM SPREMITI TAJ SADRŽAJ U BAZU
                    return -1;
                }
            }
            else
            {
                DataTable DT = SqlPostgres.select("SELECT COALESCE(MAX(id_partner),0) zbroj 1 FROM partners;", "zaposlenik").Tables[0];
                int.TryParse(DT.Rows[0][0].ToString(), out vrati);
                return vrati;
            }
        }

        #endregion Vrati ID partnera
    }
}