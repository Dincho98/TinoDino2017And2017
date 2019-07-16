using PCPOS.synWeb;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PCPOS.Util
{
    internal class Korisno
    {
        /// <summary>
        /// Vraća dućan i blagajnu u polju
        /// </summary>
        /// <returns></returns>
        ///
        public static bool RadimSinkronizaciju = false;

        public static int GodinaKojaSeKoristiUbazi = 0;

        public static bool centrala { get; set; }

        public static string idDucan { get; set; }
        public static string idKasa { get; set; }
        public static string nazivPoslovnica { get; set; }
        public static string nazivNaplatnogUredaja { get; set; }
        public static string oibTvrtke { get; set; }
        private static newSql SqlPostgres = new newSql();

        public static string serialPort { get; set; }
        public static int baudRate { get; set; }
        public static bool vaga { get; set; }

        public static string VratiImeDucanaPremaID(string _idDucan)
        {
            try
            {
                DataTable DTposlovnica = SqlPostgres.select("SELECT ime_ducana FROM ducan WHERE id_ducan='" + _idDucan + "'", "ducan").Tables[0];
                if (DTposlovnica.Rows.Count > 0)
                    return DTposlovnica.Rows[0][0].ToString();
                else
                    return "Greška, ne postoji trazeni ducan";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return ex.ToString();
            }
        }

        public static string VratiImeBlagajnePremaID(string _idBlagajna)
        {
            try
            {
                DataTable DTnaplatni = SqlPostgres.select("SELECT ime_blagajne FROM blagajna WHERE id_blagajna='" + _idBlagajna + "'", "blagajna").Tables[0];
                if (DTnaplatni.Rows.Count > 0)
                    return DTnaplatni.Rows[0][0].ToString();
                else
                    return "Greška, ne postoji trazena blagajna";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return ex.ToString();
            }
        }

        public static string VratiIDPremaImenuDucana(string _nazivDucana)
        {
            try
            {
                DataTable DTposlovnica = SqlPostgres.select("SELECT id_ducan FROM ducan WHERE ime_ducana='" + _nazivDucana + "'", "ducan").Tables[0];
                if (DTposlovnica.Rows.Count > 0)
                    return DTposlovnica.Rows[0][0].ToString();
                else
                    return "Greška, ne postoji trazeni ducan";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return ex.ToString();
            }
        }

        public static string VratiIDPremaImenuBlagajne(string _nazivBlagajne)
        {
            try
            {
                DataTable DTnaplatni = SqlPostgres.select("SELECT id_blagajna FROM blagajna WHERE ime_blagajne='" + _nazivBlagajne + "'", "blagajna").Tables[0];
                if (DTnaplatni.Rows.Count > 0)
                    return DTnaplatni.Rows[0][0].ToString();
                else
                    return "Greška, ne postoji trazena blagajna";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return ex.ToString();
            }
        }

        public static string[] VratiDucanIBlagajnu()
        {
            DataTable dtpodaci = SqlPostgres.select_settings("SELECT default_ducan, default_blagajna FROM postavke", "postavke").Tables[0];
            DataTable dtPodaciTvrtka = SqlPostgres.select_settings("SELECT oib FROM podaci_tvrtka", "postavke").Tables[0];

            string ducanid = dtpodaci.Rows.Count > 0 ? dtpodaci.Rows[0]["default_ducan"].ToString() : "";
            string blagajnaid = dtpodaci.Rows.Count > 0 ? dtpodaci.Rows[0]["default_blagajna"].ToString() : "";

            string ducan = "";
            string blagajna = "";

            if (ducanid != "")
            {
                DataTable DTducan = classSQL.select("SELECT ime_ducana FROM ducan where id_ducan ='" + ducanid + "'", "ducan").Tables[0];

                if (DTducan.Rows.Count == 0)
                    MessageBox.Show("Nemate kreirani dučan.");

                ducan = DTducan.Rows.Count > 0 ? DTducan.Rows[0][0].ToString() : "";
            }
            if (blagajnaid != "")
            {
                DataTable DTblagajna = classSQL.select("SELECT ime_blagajne FROM blagajna where id_blagajna='" + blagajnaid + "'", "blagajna").Tables[0];

                if (DTblagajna.Rows.Count == 0)
                    MessageBox.Show("Nemate kreiranu poslovnicu.");

                blagajna = DTblagajna.Rows.Count > 0 ? DTblagajna.Rows[0][0].ToString() : "";
            }

            string[] a = new string[2];
            a[0] = ducan;
            a[1] = blagajna;

            idDucan = ducanid;
            idKasa = blagajnaid;
            nazivPoslovnica = ducan;
            nazivNaplatnogUredaja = blagajna;

            if (dtPodaciTvrtka.Rows.Count > 0)
                oibTvrtke = dtPodaciTvrtka.Rows[0]["oib"].ToString();

            return a;
        }

        /// <summary>
        /// vraća u obliku: '/PC/3'
        /// </summary>
        /// <param name="vrsta">4 za avans, 3 za ifb, 2 za fakture, 1 maloprodaja</param>
        /// <returns></returns>
        public static string VratiDucanIBlagajnuZaIspis(int vrsta)
        {
            //DataTable dtpodaci = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];
            DataTable dtpodaci;
            string ducanid, blagajnaid;
            if (vrsta == 3)
            {
                dtpodaci = classSQL.select_settings("SELECT default_ducan, naplatni_uredaj_avans FROM postavke", "postavke").Tables[0];
                blagajnaid = dtpodaci.Rows.Count > 0 ? dtpodaci.Rows[0]["naplatni_uredaj_avans"].ToString() : "";
            }
            else if (vrsta == 4)
            {
                dtpodaci = classSQL.select_settings("SELECT default_ducan, naplatni_uredaj_faktura_bez_robe FROM postavke", "postavke").Tables[0];
                blagajnaid = dtpodaci.Rows.Count > 0 ? dtpodaci.Rows[0]["naplatni_uredaj_faktura_bez_robe"].ToString() : "";
            }
            else if (vrsta == 2)
            {
                dtpodaci = classSQL.select_settings("SELECT default_ducan, naplatni_uredaj_faktura FROM postavke", "postavke").Tables[0];
                blagajnaid = dtpodaci.Rows.Count > 0 ? dtpodaci.Rows[0]["naplatni_uredaj_faktura"].ToString() : "";
            }
            else
            {
                dtpodaci = classSQL.select_settings("SELECT default_ducan, default_blagajna FROM postavke", "postavke").Tables[0];
                blagajnaid = dtpodaci.Rows.Count > 0 ? dtpodaci.Rows[0]["default_blagajna"].ToString() : "";
            }
            ducanid = dtpodaci.Rows.Count > 0 ? dtpodaci.Rows[0]["default_ducan"].ToString() : "";

            string ducan = "";
            string blagajna = "";

            if (ducanid != "")
            {
                DataTable DTducan = classSQL.select("SELECT ime_ducana FROM ducan where id_ducan ='" + ducanid + "'", "ducan").Tables[0];
                ducan = DTducan.Rows.Count > 0 ? DTducan.Rows[0][0].ToString() : "";
            }
            if (blagajnaid != "")
            {
                DataTable DTblagajna = classSQL.select("SELECT ime_blagajne FROM blagajna where id_blagajna='" + blagajnaid + "'", "blagajna").Tables[0];
                blagajna = DTblagajna.Rows.Count > 0 ? DTblagajna.Rows[0][0].ToString() : "";
            }

            return "/" + ducan + "/" + blagajna;
        }

        /// <summary>
        /// Nadograđuje program. Ako je varijabla sPorukom true, onda pita korisnika da li želi skinuti noviju verziju programa.
        /// </summary>
        /// <param name="sPorukom"></param>
        public static void NovijaInacica(bool sPorukom)
        {
            if (!sPorukom)
            {
                Nadogradi();
                return;
            }
            if (MessageBox.Show("Na Internetu postoji novija inačica programa.\r\nŽelite li skinuti noviju verziju programa.", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Nadogradi();
            }
        }

        public static void Nadogradi()
        {
            string path = GetApplicationPath();
            File.WriteAllText(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/PC POS update.txt", path, Encoding.UTF8);

            Process.Start(path + "\\PC POS Update.exe");
        }

        private static string GetApplicationPath()
        {
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        }

        public static void BackupSvihBaza()
        {
            PCPOS.Util.classFukcijeZaUpravljanjeBazom u = new Util.classFukcijeZaUpravljanjeBazom("MALOPRODAJA", "POS");
            u.BackupSvihBaza();
        }

        public static bool ProvijeriIUpozoriAkoNemaNaSkladistu(string sifra, string skladiste, DataTable DTpostavke)
        {
            if (DTpostavke.Rows[0]["upozori_za_minus"].ToString() == "0")
                return true;

            //DataTable DT = classSQL.select("SELECT COALESCE(CAST(REPLACE(kolicina,',','.') AS numeric),0) AS kolicina FROM roba_prodaja WHERE sifra='" + sifra + "' AND id_skladiste='" + skladiste + "'", "roba_prodaja").Tables[0];

            DataTable DT = classSQL.select(@"SELECT COALESCE(CAST(REPLACE(kolicina,',','.') AS numeric),0) AS kolicina from roba_prodaja rs
left join roba r on rs.sifra = r.sifra
where rs.sifra = '" + sifra + @"'
and rs.id_skladiste = '" + skladiste + "' and r.oduzmi = 'DA'", "roba_prodaja").Tables[0];
            bool ima_na_stanju = false;
            if (DT.Rows.Count > 0)
            {
                decimal k;
                decimal.TryParse(DT.Rows[0]["kolicina"].ToString(), out k);
                if (k > 0)
                    ima_na_stanju = true;
            }
            else
            {
                ima_na_stanju = true;
            }

            if (!ima_na_stanju)
            {
                if (Class.PodaciTvrtka.oibTvrtke == Class.Postavke.OIB_PC1)
                {
                    MessageBox.Show("Nema prodaje u minus!");
                    return false;
                }

                if (MessageBox.Show("Dali ste sigurni da želite unjeti artikla kojeg nemate na skladištu?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MessageBox.Show("Još jednom Vas upozoravam da na stanju nemate odabranog artikla, a imate uključenu postavku upozorenja kod prodaje u minus.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public static decimal VratiNabavnuCijenu(string sifra, string skladiste)
        {
            decimal NBC = 0;
            try
            {
                string sql = string.Format(@"SELECT ProvjeraNabavneCijene('{0}', '{1}', cast({2} as integer));", sifra, DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"), skladiste);

                DataTable DT = classSQL.select(sql, "nbc").Tables[0];
                if (DT.Rows.Count > 0)
                {
                    decimal.TryParse(DT.Rows[0][0].ToString(), out NBC);
                }
                else
                {
                    NBC = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška kod uzimanja nabavne cijene.\r\nOrginalna greška:\r\n" + ex.ToString());
                NBC = 0;
            }

            return NBC;
        }

        public static string VratiNacinPlacanja(string nazivPlacanja)
        {
            string np;

            switch (nazivPlacanja)
            {
                case "gotovina":
                    np = "G";
                    break;

                case "novčanice":
                    np = "G";
                    break;

                case "novčanica":
                    np = "G";
                    break;

                case "kartica":
                    np = "K";
                    break;

                case "kartice":
                    np = "K";
                    break;

                case "virman":
                    np = "T";
                    break;

                case "transakcijski račun":
                    np = "T";
                    break;

                case "transakcijski račun - kupon":
                    np = "O";
                    break;

                default:
                    np = "O";
                    break;
            }

            if (nazivPlacanja.Contains("transakcijski"))
            {
                if (nazivPlacanja.ToLower() != "transakcijski račun - kupon")
                    np = "T";
            }

            if (nazivPlacanja.Contains("kartic"))
            {
                np = "K";
            }

            if (nazivPlacanja.Contains("novčan"))
            {
                np = "G";
            }

            return np;
        }

        /// <summary>
        /// Ova funkcija prevenstveno služi za karaban metali tvrtku
        /// </summary>
        /// <returns></returns>
        public object[] Vrati_PorezDohodak_Prirez(decimal osnovica, string id_partner, DataTable DTpostavke, bool oduzmiPoreze)
        {
            decimal prirez = 0, porezNaDohodak = 0;
            object[] obj = new object[5];
            if (osnovica == 0) return obj;
            if (id_partner == "" || id_partner == "0") return obj;
            try
            {
                decimal.TryParse(DTpostavke.Rows[0]["porez_na_dohodak"].ToString(), out porezNaDohodak);
                if (porezNaDohodak == 0) return obj;

                DataTable DTprirez = classSQL.select("SELECT * FROM grad WHERE id_grad=" +
                          "(SELECT id_grad FROM partners WHERE id_partner='" + id_partner + "' LIMIT 1) LIMIT 1;", "prirez")
                          .Tables[0];

                if (DTprirez.Rows.Count == 0) return obj;
                decimal.TryParse(DTprirez.Rows[0]["prirez"].ToString(), out prirez);
                decimal.TryParse(DTpostavke.Rows[0]["porez_na_dohodak"].ToString(), out porezNaDohodak);

                decimal resultPorezNaDohodak = 0;
                decimal resultPrirez = 0;
                if (oduzmiPoreze)
                {
                    /*resultPorezNaDohodak = osnovica-(osnovica / (1 + (porezNaDohodak / 100)));
                    resultPrirez = resultPorezNaDohodak-(resultPorezNaDohodak / (1 + (prirez / 100)));*/
                    resultPorezNaDohodak = (osnovica * porezNaDohodak / 100);
                    resultPrirez = (resultPorezNaDohodak * prirez / 100);
                }
                else
                {
                    decimal dodajNaOsnovicu = 0;
                    decimal pretacunata_stopa_porezNaDohodak = (100 * porezNaDohodak) / (100 - porezNaDohodak);
                    decimal pretacunata_stopa_prirez = (100 * prirez) / (100 - prirez);

                    resultPorezNaDohodak = (osnovica * pretacunata_stopa_porezNaDohodak / 100);
                    resultPrirez = (resultPorezNaDohodak * pretacunata_stopa_prirez / 100);
                }

                obj[0] = resultPorezNaDohodak;
                obj[1] = resultPrirez;
                obj[2] = osnovica;
                obj[3] = porezNaDohodak;
                obj[4] = prirez;

                return obj;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return obj;
            }
        }

        #region VratiKolicinuNaDan

        /// <summary>
        /// VratiKolicinuNaDan
        /// </summary>
        /// <param name="datum"></param>
        /// <param name="skladiste"></param>
        /// <returns></returns>
        public DataTable VratiKolicinuNaDan(string datum, string skladiste)
        {
            string query = @"--RACUNI
                            DROP TABLE IF EXISTS _racuni;
	                        CREATE TEMP TABLE _racuni AS
	                        SELECT sifra_robe AS sifra,id_skladiste as skladiste,COALESCE(SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)),0) AS kolicina FROM racun_stavke
	                        LEFT JOIN racuni ON racuni.broj_racuna = racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa
	                        WHERE racuni.datum_racuna<=@datum@ GROUP BY sifra_robe,id_skladiste;

	                        --KALKULACIJE
                            DROP TABLE IF EXISTS _kalkulacije;
	                        CREATE TEMP TABLE _kalkulacije AS
	                        SELECT sifra AS sifra,kalkulacija.id_skladiste as skladiste,COALESCE(SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)),0) AS kolicina FROM kalkulacija_stavke
	                        LEFT JOIN kalkulacija ON kalkulacija.broj = kalkulacija_stavke.broj AND kalkulacija.id_skladiste = kalkulacija_stavke.id_skladiste
	                        WHERE kalkulacija.datum<=@datum@ GROUP BY sifra,kalkulacija.id_skladiste;

	                        --IZDATNICE
                            DROP TABLE IF EXISTS _izdatnice;
	                        CREATE TEMP TABLE _izdatnice AS
	                        SELECT sifra AS sifra,id_skladiste as skladiste,COALESCE(SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)),0) AS kolicina FROM izdatnica_stavke
	                        LEFT JOIN izdatnica ON izdatnica.broj=izdatnica_stavke.broj AND izdatnica.id_izdatnica=izdatnica_stavke.id_izdatnica WHERE izdatnica.datum<=@datum@ GROUP BY sifra,id_skladiste;

                            --INVENTURA
                             DROP TABLE IF EXISTS _inventura;
                             CREATE TEMP TABLE _inventura AS
                             SELECT inventura_stavke.sifra_robe AS sifra, inventura.id_skladiste as skladiste, COALESCE(SUM(CAST(REPLACE(inventura_stavke.kolicina,',','.') as NUMERIC) - inventura_stavke.kolicina_koja_je_bila), 0)  AS kolicina
                             FROM inventura_stavke
                             LEFT JOIN inventura ON inventura.broj_inventure=inventura_stavke.broj_inventure WHERE inventura.pocetno_stanje = 0  and inventura.datum<@datum@ GROUP BY inventura_stavke.sifra_robe, inventura.id_skladiste;

	                        --PRIMKE
                            DROP TABLE IF EXISTS _primke;
	                        CREATE TEMP TABLE _primke AS
	                        SELECT sifra AS sifra,id_skladiste as skladiste,COALESCE(SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)),0) AS kolicina FROM primka_stavke
	                        LEFT JOIN primka ON primka.id_primka=primka_stavke.id_primka WHERE primka.datum<=@datum@ GROUP BY sifra,id_skladiste;

	                        --FAKTURE
                            DROP TABLE IF EXISTS _fakture;
	                        CREATE TEMP TABLE _fakture AS
	                        SELECT faktura_stavke.sifra AS sifra,id_skladiste as skladiste,COALESCE(SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)),0) AS kolicina FROM faktura_stavke
	                        LEFT JOIN fakture ON fakture.broj_fakture = faktura_stavke.broj_fakture AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa
	                        WHERE fakture.oduzmi_iz_skladista='1' AND fakture.date<=@datum@  GROUP BY faktura_stavke.sifra,id_skladiste;

	                        --MEĐUSKLADIŠNICA IZ SKLADIŠTA
                            DROP TABLE IF EXISTS _meduskladisnica_iz_skladista;
	                        CREATE TEMP TABLE _meduskladisnica_iz_skladista AS
	                        SELECT sifra AS sifra,meduskladisnica.id_skladiste_od as skladiste,COALESCE(SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)),0) AS kolicina FROM meduskladisnica_stavke
	                        LEFT JOIN meduskladisnica ON meduskladisnica.broj=meduskladisnica_stavke.broj AND meduskladisnica.id_skladiste_od=meduskladisnica_stavke.iz_skladista WHERE meduskladisnica.datum<=@datum@  GROUP BY sifra,id_skladiste_od;

	                        --MEĐUSKLADIŠNICA U SKLADIŠTE
                            DROP TABLE IF EXISTS _meduskladisnica_u_skladiste;
	                        CREATE TEMP TABLE _meduskladisnica_u_skladiste AS
	                        SELECT sifra AS sifra,meduskladisnica.id_skladiste_do as skladiste,COALESCE(SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)),0) AS kolicina FROM meduskladisnica_stavke
	                        LEFT JOIN meduskladisnica ON meduskladisnica.broj=meduskladisnica_stavke.broj AND meduskladisnica.id_skladiste_od=meduskladisnica_stavke.iz_skladista WHERE meduskladisnica.datum<=@datum@ GROUP BY sifra,id_skladiste_do;

	                        --OTPREMNICA
                            DROP TABLE IF EXISTS _otpremnica;
	                        CREATE TEMP TABLE _otpremnica AS
	                        SELECT sifra_robe AS sifra,otpremnica_stavke.id_skladiste as skladiste,COALESCE(SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)),0) AS kolicina FROM otpremnica_stavke
	                        LEFT JOIN otpremnice ON otpremnica_stavke.broj_otpremnice=otpremnice.broj_otpremnice AND otpremnica_stavke.id_skladiste=otpremnice.id_skladiste
	                        WHERE otpremnice.datum<=@datum@ and otpremnice.oduzmi_iz_skladista = true
	                        GROUP BY sifra_robe,otpremnica_stavke.id_skladiste;

	                        --OTPIS ROBE
                            DROP TABLE IF EXISTS _otpis_robe;
	                        CREATE TEMP TABLE _otpis_robe AS
	                        SELECT sifra AS sifra,id_skladiste as skladiste,COALESCE(SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)),0) AS kolicina FROM otpis_robe_stavke
	                        LEFT JOIN otpis_robe ON otpis_robe.broj=otpis_robe_stavke.broj WHERE otpis_robe.datum<=@datum@ GROUP BY sifra,id_skladiste;

	                        --POVRAT ROBE
                            DROP TABLE IF EXISTS _povrat_robe;
	                        CREATE TEMP TABLE _povrat_robe AS
	                        SELECT sifra AS sifra,id_skladiste as skladiste,COALESCE(SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)),0) AS kolicina FROM povrat_robe_stavke
	                        LEFT JOIN povrat_robe ON povrat_robe.broj=povrat_robe_stavke.broj WHERE povrat_robe.datum<=@datum@ GROUP BY sifra,id_skladiste;

	                        --POCETNO
                            DROP TABLE IF EXISTS _pocetno;
	                        CREATE TEMP TABLE _pocetno AS
	                        SELECT sifra AS sifra,id_skladiste as skladiste,COALESCE(CAST(REPLACE(kolicina,',','.') as NUMERIC),0) AS kolicina FROM pocetno GROUP BY sifra,id_skladiste,kolicina;

	                        --RADNI NALOG STAVKE
                            DROP TABLE IF EXISTS _radni_nalog_stavke;
	                        CREATE TEMP TABLE _radni_nalog_stavke AS
	                        SELECT sifra_robe AS sifra,id_skladiste as skladiste,COALESCE(SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)),0) AS kolicina FROM radni_nalog_stavke
	                        LEFT JOIN radni_nalog ON radni_nalog.broj_naloga=radni_nalog_stavke.broj_naloga WHERE radni_nalog.datum_naloga<=@datum@ GROUP BY sifra_robe,id_skladiste;

	                        --RADNI NALOG STAVKE-->NORMATIVI
                            DROP TABLE IF EXISTS _radni_nalog_normativi;
	                        CREATE TEMP TABLE _radni_nalog_normativi AS
	                        SELECT normativi_stavke.sifra_robe AS sifra,normativi_stavke.id_skladiste as skladiste,
	                        COALESCE(SUM
	                        (
		                        CAST(REPLACE(kolicina,',','.') as NUMERIC)*
		                        (SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM radni_nalog_stavke LEFT JOIN radni_nalog ON radni_nalog.broj_naloga=radni_nalog_stavke.broj_naloga
		                          WHERE normativi.sifra_artikla=radni_nalog_stavke.sifra_robe AND radni_nalog.datum_naloga<=@datum@
		                        )
	                        ),0) AS kolicina
	                        FROM normativi_stavke
                                LEFT JOIN normativi ON normativi.broj_normativa = normativi_stavke.broj_normativa
                                GROUP BY sifra_robe,id_skladiste;

                                --NORMATIVI (NA USLUGU SE DODAJU PRODAJNI ARTIKLI)
                            DROP TABLE IF EXISTS _normativi_usluga;
                            CREATE TEMP TABLE _normativi_usluga AS
	                        SELECT normativi_stavke.sifra_robe AS sifra,normativi_stavke.id_skladiste as skladiste,
	                        COALESCE(SUM
	                        (
		                        CAST(REPLACE(kolicina,',','.') as NUMERIC)*
			                        (
			                        SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM racun_stavke
			                        LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa
			                        WHERE normativi.sifra_artikla=racun_stavke.sifra_robe AND racuni.datum_racuna<=@datum@
			                        )
	                        ),0) AS kolicina
	                        FROM normativi_stavke
                                LEFT JOIN normativi ON normativi.broj_normativa = normativi_stavke.broj_normativa
                                LEFT JOIN roba ON roba.sifra=normativi.sifra_artikla
                                WHERE roba.oduzmi='NE'
                                GROUP BY sifra_robe,id_skladiste;

                            DROP TABLE IF EXISTS tbl_kolicine;
	                        CREATE TEMP TABLE tbl_kolicine AS
	                        SELECT sifra,skladiste,kolicina AS primka,CAST('0' as NUMERIC) AS racun,CAST('0' as NUMERIC) AS kalkulacije,CAST('0' as NUMERIC) AS izdatnice,CAST('0' as NUMERIC) AS fakture,CAST('0' as NUMERIC) AS meduskladisnica_iz_skladista,CAST('0' as NUMERIC) AS meduskladisnica_u_skladiste,
CAST('0' as NUMERIC) AS otpremnica,CAST('0' as NUMERIC) AS otpis_robe,CAST('0' as NUMERIC) AS povrat_robe,CAST('0' as NUMERIC) AS pocetno,CAST('0' as NUMERIC) AS radni_nalog_stavke,CAST('0' as NUMERIC) AS radni_nalog_normativi,CAST('0' as NUMERIC) AS normativi_usluga, CAST('0' as NUMERIC) AS inventura FROM _primke
UNION
SELECT sifra,skladiste,CAST('0' as NUMERIC) AS primka,kolicina AS racun,CAST('0' as NUMERIC) AS kalkulacije,CAST('0' as NUMERIC) AS izdatnice,CAST('0' as NUMERIC) AS fakture,CAST('0' as NUMERIC) AS meduskladisnica_iz_skladista,CAST('0' as NUMERIC) AS meduskladisnica_u_skladiste,
CAST('0' as NUMERIC) AS otpremnica,CAST('0' as NUMERIC) AS otpis_robe,CAST('0' as NUMERIC) AS povrat_robe,CAST('0' as NUMERIC) AS pocetno,CAST('0' as NUMERIC) AS radni_nalog_stavke,CAST('0' as NUMERIC) AS radni_nalog_normativi,CAST('0' as NUMERIC) AS normativi_usluga, CAST('0' as NUMERIC) AS inventura FROM _racuni
UNION
SELECT sifra,skladiste,CAST('0' as NUMERIC) AS primka,CAST('0' as NUMERIC) AS racun,kolicina AS kalkulacije,CAST('0' as NUMERIC) AS izdatnice,CAST('0' as NUMERIC) AS fakture,CAST('0' as NUMERIC) AS meduskladisnica_iz_skladista,CAST('0' as NUMERIC) AS meduskladisnica_u_skladiste,
CAST('0' as NUMERIC) AS otpremnica,CAST('0' as NUMERIC) AS otpis_robe,CAST('0' as NUMERIC) AS povrat_robe,CAST('0' as NUMERIC) AS pocetno,CAST('0' as NUMERIC) AS radni_nalog_stavke,CAST('0' as NUMERIC) AS radni_nalog_normativi,CAST('0' as NUMERIC) AS normativi_usluga, CAST('0' as NUMERIC) AS inventura FROM _kalkulacije
UNION
SELECT sifra,skladiste,CAST('0' as NUMERIC) AS primka,CAST('0' as NUMERIC) AS racun,CAST('0' as NUMERIC) AS kalkulacije,kolicina AS izdatnice,CAST('0' as NUMERIC) AS fakture,CAST('0' as NUMERIC) AS meduskladisnica_iz_skladista,CAST('0' as NUMERIC) AS meduskladisnica_u_skladiste,
CAST('0' as NUMERIC) AS otpremnica,CAST('0' as NUMERIC) AS otpis_robe,CAST('0' as NUMERIC) AS povrat_robe,CAST('0' as NUMERIC) AS pocetno,CAST('0' as NUMERIC) AS radni_nalog_stavke,CAST('0' as NUMERIC) AS radni_nalog_normativi,CAST('0' as NUMERIC) AS normativi_usluga, CAST('0' as NUMERIC) AS inventura FROM _izdatnice
UNION
SELECT sifra,skladiste,CAST('0' as NUMERIC) AS primka,CAST('0' as NUMERIC) AS racun,CAST('0' as NUMERIC) AS kalkulacije,CAST('0' as NUMERIC) AS izdatnice,kolicina AS fakture,CAST('0' as NUMERIC) AS meduskladisnica_iz_skladista,CAST('0' as NUMERIC) AS meduskladisnica_u_skladiste,
CAST('0' as NUMERIC) AS otpremnica,CAST('0' as NUMERIC) AS otpis_robe,CAST('0' as NUMERIC) AS povrat_robe,CAST('0' as NUMERIC) AS pocetno,CAST('0' as NUMERIC) AS radni_nalog_stavke,CAST('0' as NUMERIC) AS radni_nalog_normativi,CAST('0' as NUMERIC) AS normativi_usluga, CAST('0' as NUMERIC) AS inventura FROM _fakture
UNION
SELECT sifra,skladiste,CAST('0' as NUMERIC) AS primka,CAST('0' as NUMERIC) AS racun,CAST('0' as NUMERIC) AS kalkulacije,CAST('0' as NUMERIC) AS izdatnice,CAST('0' as NUMERIC) AS fakture,kolicina AS meduskladisnica_iz_skladista,CAST('0' as NUMERIC) AS meduskladisnica_u_skladiste,
CAST('0' as NUMERIC) AS otpremnica,CAST('0' as NUMERIC) AS otpis_robe,CAST('0' as NUMERIC) AS povrat_robe,CAST('0' as NUMERIC) AS pocetno,CAST('0' as NUMERIC) AS radni_nalog_stavke,CAST('0' as NUMERIC) AS radni_nalog_normativi,CAST('0' as NUMERIC) AS normativi_usluga, CAST('0' as NUMERIC) AS inventura FROM _meduskladisnica_iz_skladista
UNION
SELECT sifra,skladiste,CAST('0' as NUMERIC) AS primka,CAST('0' as NUMERIC) AS racun,CAST('0' as NUMERIC) AS kalkulacije,CAST('0' as NUMERIC) AS izdatnice,CAST('0' as NUMERIC) AS fakture,CAST('0' as NUMERIC) AS meduskladisnica_iz_skladista,kolicina AS meduskladisnica_u_skladiste,
CAST('0' as NUMERIC) AS otpremnica,CAST('0' as NUMERIC) AS otpis_robe,CAST('0' as NUMERIC) AS povrat_robe,CAST('0' as NUMERIC) AS pocetno,CAST('0' as NUMERIC) AS radni_nalog_stavke,CAST('0' as NUMERIC) AS radni_nalog_normativi,CAST('0' as NUMERIC) AS normativi_usluga, CAST('0' as NUMERIC) AS inventura FROM _meduskladisnica_u_skladiste
UNION
SELECT sifra,skladiste,CAST('0' as NUMERIC) AS primka,CAST('0' as NUMERIC) AS racun,CAST('0' as NUMERIC) AS kalkulacije,CAST('0' as NUMERIC) AS izdatnice,CAST('0' as NUMERIC) AS fakture,CAST('0' as NUMERIC) AS meduskladisnica_iz_skladista,CAST('0' as NUMERIC) AS meduskladisnica_u_skladiste,
kolicina AS otpremnica,CAST('0' as NUMERIC) AS otpis_robe,CAST('0' as NUMERIC) AS povrat_robe,CAST('0' as NUMERIC) AS pocetno,CAST('0' as NUMERIC) AS radni_nalog_stavke,CAST('0' as NUMERIC) AS radni_nalog_normativi,CAST('0' as NUMERIC) AS normativi_usluga, CAST('0' as NUMERIC) AS inventura FROM _otpremnica
UNION
SELECT sifra,skladiste,CAST('0' as NUMERIC) AS primka,CAST('0' as NUMERIC) AS racun,CAST('0' as NUMERIC) AS kalkulacije,CAST('0' as NUMERIC) AS izdatnice,CAST('0' as NUMERIC) AS fakture,CAST('0' as NUMERIC) AS meduskladisnica_iz_skladista,CAST('0' as NUMERIC) AS meduskladisnica_u_skladiste,
CAST('0' as NUMERIC) AS otpremnica,kolicina AS otpis_robe,CAST('0' as NUMERIC) AS povrat_robe,CAST('0' as NUMERIC) AS pocetno,CAST('0' as NUMERIC) AS radni_nalog_stavke,CAST('0' as NUMERIC) AS radni_nalog_normativi,CAST('0' as NUMERIC) AS normativi_usluga, CAST('0' as NUMERIC) AS inventura FROM _otpis_robe
UNION
SELECT sifra,skladiste,CAST('0' as NUMERIC) AS primka,CAST('0' as NUMERIC) AS racun,CAST('0' as NUMERIC) AS kalkulacije,CAST('0' as NUMERIC) AS izdatnice,CAST('0' as NUMERIC) AS fakture,CAST('0' as NUMERIC) AS meduskladisnica_iz_skladista,CAST('0' as NUMERIC) AS meduskladisnica_u_skladiste,
CAST('0' as NUMERIC) AS otpremnica,CAST('0' as NUMERIC) AS otpis_robe,kolicina AS povrat_robe,CAST('0' as NUMERIC) AS pocetno,CAST('0' as NUMERIC) AS radni_nalog_stavke,CAST('0' as NUMERIC) AS radni_nalog_normativi,CAST('0' as NUMERIC) AS normativi_usluga, CAST('0' as NUMERIC) AS inventura FROM _povrat_robe
UNION
SELECT sifra,skladiste,CAST('0' as NUMERIC) AS primka,CAST('0' as NUMERIC) AS racun,CAST('0' as NUMERIC) AS kalkulacije,CAST('0' as NUMERIC) AS izdatnice,CAST('0' as NUMERIC) AS fakture,CAST('0' as NUMERIC) AS meduskladisnica_iz_skladista,CAST('0' as NUMERIC) AS meduskladisnica_u_skladiste,
CAST('0' as NUMERIC) AS otpremnica,CAST('0' as NUMERIC) AS otpis_robe,CAST('0' as NUMERIC) AS povrat_robe,kolicina AS pocetno,CAST('0' as NUMERIC) AS radni_nalog_stavke,CAST('0' as NUMERIC) AS radni_nalog_normativi,CAST('0' as NUMERIC) AS normativi_usluga, CAST('0' as NUMERIC) AS inventura FROM _pocetno
UNION
SELECT sifra,skladiste,CAST('0' as NUMERIC) AS primka,CAST('0' as NUMERIC) AS racun,CAST('0' as NUMERIC) AS kalkulacije,CAST('0' as NUMERIC) AS izdatnice,CAST('0' as NUMERIC) AS fakture,CAST('0' as NUMERIC) AS meduskladisnica_iz_skladista,CAST('0' as NUMERIC) AS meduskladisnica_u_skladiste,
CAST('0' as NUMERIC) AS otpremnica,CAST('0' as NUMERIC) AS otpis_robe,CAST('0' as NUMERIC) AS povrat_robe,CAST('0' as NUMERIC) AS pocetno,kolicina AS radni_nalog_stavke,CAST('0' as NUMERIC) AS radni_nalog_normativi,CAST('0' as NUMERIC) AS normativi_usluga, CAST('0' as NUMERIC) AS inventura FROM _radni_nalog_stavke
UNION
SELECT sifra,skladiste,CAST('0' as NUMERIC) AS primka,CAST('0' as NUMERIC) AS racun,CAST('0' as NUMERIC) AS kalkulacije,CAST('0' as NUMERIC) AS izdatnice,CAST('0' as NUMERIC) AS fakture,CAST('0' as NUMERIC) AS meduskladisnica_iz_skladista,CAST('0' as NUMERIC) AS meduskladisnica_u_skladiste,
CAST('0' as NUMERIC) AS otpremnica,CAST('0' as NUMERIC) AS otpis_robe,CAST('0' as NUMERIC) AS povrat_robe,CAST('0' as NUMERIC) AS pocetno,CAST('0' as NUMERIC) AS radni_nalog_stavke,kolicina AS radni_nalog_normativi,CAST('0' as NUMERIC) AS normativi_usluga, CAST('0' as NUMERIC) AS inventura FROM _radni_nalog_normativi
 UNION
 SELECT sifra,skladiste,CAST('0' as NUMERIC) AS primka,CAST('0' as NUMERIC) AS racun,CAST('0' as NUMERIC) AS kalkulacije,CAST('0' as NUMERIC) AS izdatnice,CAST('0' as NUMERIC) AS fakture,CAST('0' as NUMERIC) AS meduskladisnica_iz_skladista,CAST('0' as NUMERIC) AS meduskladisnica_u_skladiste,
 CAST('0' as NUMERIC) AS otpremnica,CAST('0' as NUMERIC) AS otpis_robe,CAST('0' as NUMERIC) AS povrat_robe,CAST('0' as NUMERIC) AS pocetno,CAST('0' as NUMERIC) AS radni_nalog_stavke, CAST('0' as NUMERIC) AS radni_nalog_normativi, CAST('0' as NUMERIC) AS normativi_usluga, kolicina AS inventura FROM _INVENTURA;

	                        SELECT rp.sifra, rp.id_skladiste, coalesce(SUM(COALESCE(tk.pocetno,0) + COALESCE(tk.inventura,0) + COALESCE(tk.primka,0) + COALESCE(tk.kalkulacije,0) + COALESCE(tk.radni_nalog_stavke,0) + COALESCE(tk.meduskladisnica_u_skladiste,0) - COALESCE(tk.racun, 0) - COALESCE(tk.izdatnice, 0) - COALESCE(tk.fakture, 0) - COALESCE(tk.meduskladisnica_iz_skladista, 0) - COALESCE(tk.otpremnica, 0) - COALESCE(tk.otpis_robe, 0) - COALESCE(tk.povrat_robe, 0) - COALESCE(tk.radni_nalog_normativi, 0) - COALESCE(tk.normativi_usluga, 0)), 0) AS kolicina
	                        FROM roba_prodaja rp
	                        LEFT JOIN tbl_kolicine tk on rp.id_skladiste = tk.skladiste and rp.sifra = tk.sifra
	                        WHERE rp.id_skladiste='" + skladiste + @"'
                            GROUP BY rp.sifra, rp.id_skladiste
				            ORDER BY rp.sifra;";

            //            query = @"select sifra, skladiste,
            //sum(case when doc not in ('kalkulacija', 'primka', 'u_skl', 'povrat_robe', 'promjena_cijene', 'inventura', 'pocetno', 'radni_nalog_stavke') then kolicina * (-1) else kolicina end) as kolicina,
            //sum(vpc) / count(vpc)as vpc,
            //sum(nbc) / count(nbc)as nbc
            //from ulaz_izlaz_robe_financijski
            //where doc not in ('radni_nalog_skida_normative_prema_uslugi', 'promjena_cijene')
            //and datum < @datum@
            //and skladiste = '" + skladiste + @"'
            //group by skladiste, sifra
            //order by sifra;";

            DataTable dt;
            try
            {
                dt = classSQL.select(query.Replace("+", "zbroj").Replace("@datum@", "'" + datum + "'"), "kolicine").Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dt = new DataTable();
            }
            return dt;
        }

        #endregion VratiKolicinuNaDan

        private BackgroundWorker DWnadogradnja = new BackgroundWorker();
        private string oib_nadogradnja;

        public void ProvjeriNadogradnjuPremaOibu(string _oib_nadogradnja)
        {
            DWnadogradnja.WorkerReportsProgress = true;
            DWnadogradnja.WorkerSupportsCancellation = true;
            DWnadogradnja.DoWork += new DoWorkEventHandler(DWnadogradnja_DoWorkNadogradnje);
            oib_nadogradnja = _oib_nadogradnja;
            DWnadogradnja.RunWorkerAsync();
        }

        public static string prefixBazeKojaSeKoristi()
        {
            string prefix = "pos";
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";
            XDocument xmlFile = XDocument.Load(path);
            var query = (from c in xmlFile.Element("settings").Elements("database_remote").Elements("postgree") select c);
            foreach (XElement book in query)
            {
                string databaseName = book.Attribute("database").Value.ToString();
                string godinaKojaSeKoristi = Util.Korisno.GodinaKojaSeKoristiUbazi.ToString();
                if (databaseName.EndsWith(godinaKojaSeKoristi))
                {
                    prefix = databaseName.Substring(0, databaseName.Length - godinaKojaSeKoristi.Length);
                    break;
                }
            }
            return prefix;
        }

        private void DWnadogradnja_DoWorkNadogradnje(object sender, DoWorkEventArgs e)
        {
            pomagala_syn web = new pomagala_syn();
            DataTable DT = web.MyWebRequestXML("", Properties.Settings.Default.domena_za_nadogradnju);

            if (DT.Rows.Count > 0)
            {
                string[] oibs = new string[1] { "31425903291" };
                decimal trenutna_vertija, nova_verzija;
                DataRow[] r = DT.Select("oib='svi'");
                if (r.Length == 0 || Array.IndexOf(oibs, Class.PodaciTvrtka.oibTvrtke) >= 0)
                    r = DT.Select("oib='" + oib_nadogradnja + "'");

                if (r.Length > 0)
                {
                    decimal.TryParse(Properties.Settings.Default.verzija_programa.ToString(), out trenutna_vertija);
                    decimal.TryParse(r[0]["verzija"].ToString().Replace(".", ","), out nova_verzija);
                    if (trenutna_vertija < nova_verzija)
                    {
                        string path = GetApplicationPath();
                        File.WriteAllText(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/PC POS update.txt", path, Encoding.UTF8);
                        Process.Start(path + "\\PC POS Update.exe");
                    }
                }
            }
        }

        internal static bool CheckForInternetConnection(string urlForCheck = null)
        {
            try
            {
                using (var client = new WebClient())
                {
                    string url = (urlForCheck != null ? urlForCheck : "http://www.google.com");
                    using (var stream = client.OpenRead(url))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}