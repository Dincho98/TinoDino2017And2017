using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCPOS.Global
{
    public static class Database
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable GetZaposleniciNaziv(string sifra = null)
        {
            return classSQL.select($"SELECT id_zaposlenik, CONCAT(ime, ' ', prezime) AS naziv FROM zaposlenici {(sifra != null ? $"WHERE id_zaposlenik = {sifra}" : "")}", "zaposlenici").Tables[0];
        }

        public static DataTable GetZaposlenici(string sifra = null)
        {
            return classSQL.select($"SELECT * FROM zaposlenici {(sifra != null ? $"WHERE id_zaposlenik = {sifra}" : "")}", "zaposlenici").Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable GetRobniDobropisi()
        {
            string query = $@"SELECT dobropis.broj_dobropis,
                                    dobropis.id_partner,
                                    partners.ime_tvrtke,
                                    dobropis.id_izradio,
                                    dobropis.id_skladiste,
                                    skladiste.skladiste,
                                    dobropis.datum,
                                    dobropis.ukupno
                                FROM dobropis
                                LEFT JOIN skladiste ON skladiste.id_skladiste = dobropis.id_skladiste INNER JOIN partners ON dobropis.id_partner=partners.id_partner 
                                ORDER BY dobropis.broj_dobropis ASC";
            return classSQL.select(query, "dobropis").Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sifra"></param>
        /// <returns></returns>
        public static DataTable GetDobropisStavke(int sifra)
        {
            string query = $@"SELECT dobropis_stavke.sifra_robe,
                                roba.naziv,
                                dobropis_stavke.jm,
                                dobropis_stavke.kolicina,
                                dobropis_stavke.porez,
                                dobropis_stavke.mpc,
                                dobropis_stavke.vpc,
                                dobropis_stavke.rabat,
                                dobropis_stavke.rabat_iznos,
                                dobropis_stavke.cijena_bez_pdv,
                                dobropis_stavke.iznos_bez_pdv,
                                dobropis_stavke.iznos_ukupno
                            FROM dobropis_stavke 
                            LEFT JOIN roba ON roba.sifra = dobropis_stavke.sifra_robe
                            WHERE id_dobropis = {sifra}";
            return classSQL.select(query, "dobropis_stavke").Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPocetno()
        {
            string query = $@"SELECT * FROM pocetno LIMIT 1";
            return classSQL.select(query, "pocetno").Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sifra"></param>
        /// <returns></returns>
        public static DataTable GetPartners(string sifra = null)
        {
            string query = $@"SELECT * FROM partners {(sifra != null ? $"WHERE id_partner = '{sifra}'" : "")}";
            return classSQL.select(query, "partners").Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sifra"></param>
        /// <returns></returns>
        public static DataTable GetRoba(string sifra = null)
        {
            string query = $@"SELECT * FROM roba {(sifra != null ? $"WHERE sifra = '{sifra}'" : "")}";
            return classSQL.select(query, "roba").Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sifra"></param>
        /// <returns></returns>
        public static DataTable GetSobe(string sifra = null)
        {
            string query = $@"SELECT * FROM sobe {(sifra != null ? $"WHERE broj_sobe = '{sifra}'" : "")} ORDER BY naziv_sobe ASC";
            return classSQL.select(query, "sobe").Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable GetRezervacije(string brojRezervacije = null, string brojSobe = null)
        {
            string query = $@"SELECT * FROM unos_rezervacije 
                            {(brojRezervacije != null || brojSobe != null ? " WHERE " : "")}
                            {(brojRezervacije != null ? $"broj = {brojRezervacije}" : "")}
                            {((brojRezervacije != null && brojSobe != null) ? " AND " : "")}
                            {(brojSobe != null ? $"id_soba = '{brojSobe}'" : "")}
                            ORDER BY broj ASC";
            return classSQL.select(query, "unos_rezervacije").Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAgencija()
        {
            string query = $@"SELECT * FROM agencija ORDER BY ime_agencije ASC";
            return classSQL.select(query, "agencija").Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable GetVrstaUsluge(string sifra = null)
        {
            string query = $@"SELECT * FROM vrsta_usluge {(sifra != null ? $"WHERE id = {sifra}" : "")} ORDER BY naziv_usluge ASC";
            return classSQL.select(query, "vrsta_usluge").Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sifra"></param>
        public static DataTable GetAgencija(string sifra = null)
        {
            string query = $@"SELECT * FROM agencija {(sifra != null ? $"WHERE id={sifra}" : "")} ORDER BY ime_agencije ASC";
            return classSQL.select(query, "agencija").Tables[0];
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable GetTipSobe(string id = null)
        {
            string query = $@"SELECT * FROM tip_sobe {(id != null ? $"WHERE id = {id}" : "")} ORDER BY tip ASC";
            return classSQL.select(query, "tip_sobe").Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static string GetMaxBroj(string tableName, string columnName)
        {
            string result = "";
            string query = $@"SELECT MAX({columnName}) FROM {tableName}";
            DataTable dataTable = classSQL.select(query, tableName)?.Tables[0];
            if (dataTable.Rows.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(dataTable.Rows[0][0].ToString()))
                    result = (Convert.ToInt32(dataTable.Rows[0][0].ToString()) + 1).ToString();
                else
                    result = "1";
            }
            return result;
        }

        /// <summary>
        /// Updates rezervation payment status (unos_rezervacije.naplaceno)
        /// </summary>
        /// <param name="brojRezervacije"></param>
        /// <param name="naplaceno"></param>
        public static void UpdateRezervacijaNaplaceno(string brojRezervacije, int naplaceno)
        {
            string query = $@"UPDATE unos_rezervacije SET naplaceno = {naplaceno} WHERE broj = {brojRezervacije}";
            classSQL.update(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="naSobu"></param>
        /// <returns></returns>
        public static DataTable GetOtpremnice(bool naSobu = false)
        {
            string query = $@"SELECT * FROM otpremnice {(naSobu ? "WHERE na_sobu = 'TRUE'" : "")}";
            return classSQL.select(query, "otpremnice", true).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idRezervacije"></param>
        /// <returns></returns>
        public static DataTable GetIdPartnera(string brojRezervacije)
        {
            string query = $@"SELECT * FROM unos_rezervacije WHERE broj={brojRezervacije}";
            return classSQL.select(query, "unos_rezervacije").Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idRezervacije"></param>
        /// <returns></returns>
        public static DataTable GetIdVrstaUsluge(string brojRezervacije)
        {
            string query = $@"SELECT * FROM unos_rezervacije WHERE broj={brojRezervacije}";
            return classSQL.select(query, "unos_rezervacije").Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable GetPopisGostiju(string id = null)
        {
            string query = $@"SELECT * FROM popisgostiju {(id != null ? $"WHERE id={id}" : "")}";
            return classSQL.select(query, "popisgostiju").Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable GetPartnerByName(string sifra = null)
        {
            string query = $@"SELECT * FROM partners {(sifra != null ? $"WHERE ime_tvrtke = '{sifra}'" : "")}";
            return classSQL.select(query, "partners").Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sifra"></param>
        /// <returns></returns>
        public static DataTable GetRacuni(string sifra = null)
        {
            string query = $@"SELECT broj_racuna,id_ducan,datum_racuna,ukupno,nacin_placanja FROM racuni {(sifra != null ? "WHERE broj_racuna=" + sifra : "")}";
            return classSQL.select(query, "racuni").Tables[0];
        }

        public static DataTable GetRacuniDate(string datumPocetka = null, string datumKraja = null)
        {
            datumPocetka = datumPocetka.Replace('.','-');
            datumKraja = datumKraja.Replace('.', '-');
            datumPocetka += " 00:00:01";
            datumKraja += " 23:59:59";
            string query = $@"SELECT broj_racuna,id_ducan,datum_racuna,ukupno,nacin_placanja FROM racuni {(datumPocetka != null && datumKraja != null ? "WHERE datum_racuna>='"+datumPocetka+"' AND datum_racuna<='"+datumKraja+"'": "")}";
            return classSQL.select(query, "racuni").Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sifra"></param>
        /// <returns></returns>
        public static string GetImePoslovnice(string sifra = null)
        {
            string query = $@"SELECT ime_ducana FROM ducan {(sifra != null ? "WHERE id_ducan=" + sifra : "")}";
            DataTable dt = classSQL.select(query, "ducan").Tables[0];
            return dt.Rows[0]["ime_ducana"].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sifra"></param>
        /// <returns></returns>
        public static DataTable GetFakture(string sifra = null, string datumPocetka = null, string datumKraja = null)
        {
            string query = $@"SELECT broj_fakture,id_ducan,date,ukupno,id_nacin_placanja FROM fakture {(sifra != null ? "WHERE broj_fakture=" + sifra : "")}";
            return classSQL.select(query, "fakture").Tables[0];
        }

        public static DataTable GetFaktureDate(string datumPocetka = null, string datumKraja = null)
        {
            datumPocetka = datumPocetka.Replace('.', '-');
            datumKraja = datumKraja.Replace('.', '-');
            datumPocetka += " 00:00:01";
            datumKraja += " 23:59:59";
            string query = $@"SELECT broj_fakture,id_ducan,date,ukupno,id_nacin_placanja FROM fakture {(datumPocetka != null && datumKraja != null ? "WHERE date>='" + datumPocetka + "' AND date<='" + datumKraja + "'" : "")}";
            return classSQL.select(query, "racuni").Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetNacinPlacanja(int id)
        {
            string sqlCmd = "SELECT * FROM nacin_placanja WHERE id_placanje=" + id;
            DataTable dt = classSQL.select(sqlCmd, "nacin_placanja").Tables[0];
            DataRow row = dt.Rows[0];
            return row["naziv_placanja"].ToString().Substring(0, 1);
        }
    }
}
