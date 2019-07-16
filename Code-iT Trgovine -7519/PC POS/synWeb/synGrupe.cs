using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.synWeb
{
    internal class synGrupe
    {
        private synWeb.pomagala_syn Pomagala = new pomagala_syn();
        private newSql SqlPostgres = new newSql();

        private string domena = Properties.Settings.Default.domena_za_sinkronizaciju;
        private string poslovnica = "1";
        private bool posalji_sve = false;

        //OVAJ KONSTRUKTOR IMA MOGUČNOST DA POSTAVI VARIJABLU DA POŠALJE SVE PODATKE NA WEB
        //POZIVA SE KOD KEREIRANJA
        public synGrupe(bool _posalji_sve)
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

            if (posalji_sve)
                if (MessageBox.Show("Želite poslati grupe?", "Sinkronizacija", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

            string query = "";
            query = "SELECT * FROM grupa";

            DataTable DT = SqlPostgres.select(query, "TABLE").Tables[0];

            string sql = "sql=";
            string tempDel = "";

            //PRVO PROVJERIM SVE DOKUMENTE KOJI SU U PROGRAMU MIJENJANI I BRIŠEM ISTE NA WEBU
            //ako mijenjam neku pojedinacnu grupu svim stavkama dodijelim editirano=1
            foreach (DataRow r in DT.Rows)
            {
                if (r["editirano"].ToString() == "True")
                {
                    tempDel = "DELETE FROM grupe WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "';~";

                    if (!sql.Contains(tempDel))
                    {
                        sql += tempDel;
                    }
                }
                else if (posalji_sve)
                {
                    tempDel = "DELETE FROM grupe WHERE oib='" + DTpodaci_tvrtka.Rows[0]["oib"].ToString() + "' AND poslovnica='" + poslovnica + "';~";
                }

                if (!sql.Contains(tempDel))
                {
                    sql += tempDel;
                }
            }

            if (sql.Length > 4)
            {
                foreach (DataRow r in DT.Rows)
                {
                    sql += "INSERT INTO grupe (id_grupa, grupa, id_podgrupa, aktivnost, novo, editirano,oib,poslovnica,datum_syn) VALUES (" +
                            "'" + r["id_grupa"].ToString().Replace(";", "").Replace("~", "") + "'," +
                            "'" + r["grupa"].ToString().Replace(";", "").Replace("~", "") + "'," +
                            "'0'," +
                            "'1'," +
                            "'0'," +
                            "'0'," +
                            "'" + DTpodaci_tvrtka.Rows[0]["oib"].ToString().Replace(";", "").Replace("~", "") + "'," +
                            "'" + poslovnica.Replace(";", "").Replace("~", "") + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "');~";
                }

                sql = sql.Remove(sql.Length - 1);

                //ŠALJE NA WEB i DOBIVAM ODG
                string[] odg = Pomagala.MyWebRequest(sql + "&lozinka=sinkronizacija_za_caffeq1w2e3r4&godina=" + Util.Korisno.GodinaKojaSeKoristiUbazi, domena + "include/primam_post_sql_query.php").Split(';');

                if (odg[0] == "OK" && odg[1] == "1")
                {
                    sql = "UPDATE grupa SET editirano='0', novo='0';";
                    SqlPostgres.update(sql);
                }
            }
        }
    }
}