using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS
{
    internal class classProvjeraBaze
    {
        private static DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
        private static DataTable DTracun = classSQL.select("SELECT * FROM racuni LIMIT 1", "racuni").Tables[0];

        public static void ProvjeraTablica()
        {
            DataTable DTremote = classSQL.select("select table_name from information_schema.tables where TABLE_TYPE <> 'VIEW'", "Table").Tables[0];
            DataTable DTremote_cols = classSQL.select("select table_name, column_name, data_type, character_maximum_length from information_schema.columns order by table_name, column_name", "coltypes").Tables[0];
            DataTable DTcompact_cols = classSQL.select_settings("select table_name, column_name, data_type, character_maximum_length from information_schema.columns", "coltypes").Tables[0];
            string sql = "";

            DataRow[] REGISTRACIJA = DTcompact_cols.Select("table_name = 'registracija'");
            if (REGISTRACIJA.Length == 0)
            {
                sql = @"create table registracija (
                        productKey nchar(36),
                        activationCode nchar(34)
                        )";
                classSQL.select_settings(sql, "registracija");
            }

            REGISTRACIJA = DTcompact_cols.Select("table_name = 'registracija' and column_name='broj'");
            if (REGISTRACIJA.Length == 0)
            {
                sql = @"alter table registracija add column broj int;";
                classSQL.select_settings(sql, "registracija");
                sql = @"update registracija set broj = 0;";
                classSQL.select_settings(sql, "registracija");
            }

            REGISTRACIJA = DTcompact_cols.Select("table_name = 'registracija' and column_name='kraj_rada'");

            if (REGISTRACIJA.Length == 0)
            {
                sql = @"alter table registracija add column kraj_rada datetime;";
                classSQL.select_settings(sql, "registracija");
                sql = @"update registracija set kraj_rada = DATEADD(YEAR,10,GETDATE());";
                classSQL.select_settings(sql, "registracija");
            }

            REGISTRACIJA = DTcompact_cols.Select("table_name = 'registracija' and column_name='zadnja_konekcija_prema_software'");

            if (REGISTRACIJA.Length == 0)
            {
                sql = @"alter table registracija add column zadnja_konekcija_prema_software datetime;";
                classSQL.select_settings(sql, "registracija");
                sql = @"update registracija set zadnja_konekcija_prema_software = GETDATE();";
                classSQL.select_settings(sql, "registracija");
            }

            DataRow[] POS_PRINT = DTcompact_cols.Select("table_name = 'pos_print' and column_name = 'ispis_sifre_na_pos_printer'");
            if (POS_PRINT.Length == 0)
            {
                sql = "ALTER TABLE pos_print ADD COLUMN ispis_sifre_na_pos_printer BIT DEFAULT 0;";
                classSQL.select_settings(sql, "pos_print");
            }

            DataRow[] rrow = DTremote_cols.Select("table_name = 'ispis_fakture' and column_name = 'broj_avansa'");

            if (rrow.Length == 0)
            {
                try
                {
                    sql = "ALTER TABLE ispis_fakture add column broj_avansa integer DEFAULT 0;";
                    classSQL.insert(sql);
                }
                catch
                {
                    //već postoji
                }
            }

            rrow = DTremote_cols.Select("table_name = 'servis'");
            if (rrow.Length == 0)
            {
                try
                {
                    sql = @"CREATE TABLE servis
(
	id serial NOT NULL,
	servisna_primka character varying(50),
	godina int NOT NULL,
	partner int NOT NULL,
	izradio int NOT NULL,
	seriski_broj character varying(50),
	sifra character varying(50),
	naziv character varying(50),
	CONSTRAINT servis_pkey PRIMARY KEY (id)
);

CREATE TABLE servis_status
(
	id_servis_status serial NOT NULL,
	id_servis bigint NOT NULL,
	datum timestamp without time zone,
	status int NOT NULL,
	napomena character varying(500),
	CONSTRAINT servis_status_pkey PRIMARY KEY (id_servis_status)
);";
                    classSQL.insert(sql);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            rrow = DTremote_cols.Select("table_name = 'dobropis'");
            if (rrow.Length == 0)
            {
                try
                {
                    sql = @"CREATE TABLE dobropis
                    (
                      id_dobropis serial NOT NULL,
                      id_mjesto integer,
                      id_partner bigint,
                      id_izradio integer,
                      id_skladiste integer,
                      broj_dobropis bigint NOT NULL,
                      datum timestamp without time zone,
                      godina character varying(6),
                      mjesto_troska character varying(100),
                      originalni_dokument character varying(100),
                      napomena text,
                      ukupno money,
                      CONSTRAINT dobropis_pkey PRIMARY KEY (id_dobropis)
                    );";
                    classSQL.insert(sql);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            rrow = DTremote_cols.Select("table_name = 'dobropis_stavke'");
            if (rrow.Length == 0)
            {
                try
                {
                    sql = @"CREATE TABLE dobropis_stavke
                    (
                        id serial NOT NULL,
                        id_dobropis integer,
                        sifra_robe character varying(30) NOT NULL,
                        jm character varying(6) NOT NULL,
                        kolicina numeric NOT NULL,
                        porez numeric NOT NULL,
                        mpc money NOT NULL,
                        vpc money NOT NULL,
                        rabat numeric NOT NULL,
                        rabat_iznos money NOT NULL,
                        cijena_bez_pdv money NOT NULL,
                        iznos_bez_pdv money NOT NULL,
                        iznos_ukupno money NOT NULL,
                        CONSTRAINT dobropis_stavke_pkey PRIMARY KEY (id)
                    );";
                    classSQL.insert(sql);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            rrow = DTremote_cols.Select("table_name = 'unos_rezervacije'");
            if (rrow.Length == 0)
            {
                try
                {
                    sql = @"CREATE TABLE unos_rezervacije
                    (
                        id serial NOT NULL,
                        broj bigint,
                        godina integer,
                        vrijeme_unosa timestamp without time zone,
                        id_partner character varying(20),
                        broj_osobne character varying(50),
                        broj_putovnice character varying(50),
                        datum_dolaska timestamp without time zone,
                        datum_odlaska timestamp without time zone,
                        id_agencija integer,
                        id_soba integer,
                        id_vrsta_usluge integer,
                        dorucak integer,
                        rucak integer,
                        vecera integer,
                        odrasli integer,
                        djeca integer,
                        bebe integer,
                        napomena text,
                        ukupno numeric,
                        naplaceno integer DEFAULT 0,
                        CONSTRAINT unos_rezervacije_pkey PRIMARY KEY (id)
                    );";
                    classSQL.insert(sql);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            rrow = DTremote_cols.Select("table_name = 'popisgostiju'");
            if (rrow.Length == 0)
            {
                try
                {
                    sql = @"CREATE TABLE popisgostiju(
                        id serial NOT NULL,
                        broj int,
                        prezime_ime varchar(50),
                        broj_osobne varchar(30),
                        broj_putovnice varchar(30),
                        vrsta_pružene_usluge varchar(30),
                        datum_pocetka_pruzanja_usluge timestamp without time zone,
                        datum_prestanka_pruzanja_usluge timestamp without time zone,
                        primjedba text
                    );";
                    classSQL.insert(sql);
                }
                catch (Exception)
                {
                    throw;
                }
            }




            rrow = DTremote_cols.Select("table_name = 'vrsta_usluge'");
            if (rrow.Length == 0)
            {
                try
                {
                    sql = @"CREATE TABLE vrsta_usluge
                    (
                        id serial NOT NULL,
                        naziv_usluge character varying(100),
                        iznos numeric,
                        napomena text,
                        aktivnost integer DEFAULT 1,
                        CONSTRAINT vrsta_usluge_pkey PRIMARY KEY (id)
                    );";
                    classSQL.insert(sql);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            rrow = DTremote_cols.Select("table_name = 'sobe'");
            if (rrow.Length == 0)
            {
                try
                {
                    sql = @"CREATE TABLE sobe
                    (
                        id serial NOT NULL,
                        broj_sobe numeric,
                        id_tip_sobe integer,
                        naziv_sobe character varying(100),
                        broj_lezaja numeric,
                        cijena_nocenja numeric,
                        aktivnost integer,
                        napomena text,
                        CONSTRAINT sobe_pkey PRIMARY KEY (id)
                    );";
                    classSQL.insert(sql);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            rrow = DTremote_cols.Select("table_name = 'tip_sobe'");
            if (rrow.Length == 0)
            {
                try
                {
                    sql = @"CREATE TABLE tip_sobe
                    (
                        id serial NOT NULL,
                        tip character varying(70),
                        aktivnost integer,
                        CONSTRAINT tip_sobe_pkey PRIMARY KEY (id)
                    );";
                    classSQL.insert(sql);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            rrow = DTremote_cols.Select("table_name = 'agencija'");
            if (rrow.Length == 0)
            {
                try
                {
                    sql = @"CREATE TABLE agencija
                    (
                        id serial NOT NULL,
                        ime_agencije character varying(100),
                        napomena text,
                        aktivnost integer DEFAULT 1,
                        CONSTRAINT agencija_pkey PRIMARY KEY (id)
                    );";
                    classSQL.insert(sql);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            rrow = DTremote_cols.Select("table_name = 'pocetno_dugovanje_partnera'");
            if (rrow.Length == 0)
            {
                try
                {
                    sql = @"CREATE TABLE pocetno_dugovanje_partnera
                    (
                    	id serial NOT NULL,
                    	id_partner int,
                    	otvoreno decimal(15,6),
                    	uplaceno decimal(15,6),
                    	potrazuje decimal(15,6),
                    	isplaceno decimal(15,6),
                    	datum_dokumenta timestamp without time zone,
                    	datum_dvo timestamp without time zone,
                    	CONSTRAINT pocetno_dugovanje_partnera_pkey PRIMARY KEY (id)

                    );";
                    classSQL.insert(sql);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            rrow = DTremote_cols.Select("table_name = 'ispis_fakture' and column_name = 'godina_avansa'");

            if (rrow.Length == 0)
            {
                try
                {
                    sql = "ALTER TABLE ispis_fakture add column godina_avansa integer DEFAULT 0;";
                    classSQL.insert(sql);
                }
                catch
                {
                    //već postoji
                }
            }

            if (DTremote.Rows.Count > 0)
            {
                DodajProvjeruGreske(DTremote);
                DataTable DTremote_greske = classSQL.select("SELECT * FROM ispravljene_greske", "greske").Tables[0];
                AlterUlaznaFaktura(DTremote_cols);
                FixKalkulacija(DTremote_greske);
                FixZaposlenici(DTremote_greske);
                IspraviPrviPrvi(DTremote_greske);

                DodajAvanse(DTremote);
                DodajPocetno(DTremote_cols);

                AlterCreateTablePostavke_sinkronizacije(DTremote_cols);

                AktivnostZaposlenika(DTremote);
                KnjiznoOdobrenje(DTremote);

                Alterotpremnica_stavke(DTremote);

                Podaci_tvrtka_pdv(DTremote_cols);

                AlterGrupaRoba(DTremote_cols);

                AlterRobaKaucija(DTremote);

                AlterValute(DTremote_cols);

                AlterRadniNalog(DTremote_cols);

                AlterPromjenaCijeneKomadno(DTremote, DTremote_cols);

                AlterPonude(DTremote, DTremote_cols);

                faktura1alter_stavke(DTremote_cols);

                AlterPartnersGeneriranjeFakture(DTremote_cols);

                FillPartnersOIBpolje(DTremote, DTremote_cols);

                Alterracuni_stavke(DTremote_cols);

                Alterracuni_racuni(DTremote);

                AlterFaktureVan(DTremote);

                AlterFaktureVan_stavke(DTremote);

                Alterfaktura_stavke(DTremote);

                Alterfaktura_racuni(DTremote);

                Alterotpremnice_stavke(DTremote_cols);

                Alterkalkulacija_stavke(DTremote_cols);

                AlterIspisFakture(DTremote, DTremote_cols);

                AlterIspisRacuna(DTremote);

                AlterSmjene(DTremote);

                AlterIFB(DTremote);

                Alterkontni_plan(DTremote);

                InsertOstaloDjelatnosti();

                AlterOibNazivTvrtke(DTremote_cols);

                AlterRadniNalogServis(DTremote);

                //Ovaj jos treba pamento izmjeniti
                AlterColumnsDataType(DTremote_cols);

                AlterUfa(DTremote);

                AlterOtpisStavke(DTremote, DTremote_cols);

                AlterOtpis(DTremote);

                AlterNeuspjelaFiskalizacija(DTremote, DTremote_cols);

                AlterPovratnaNaknada(DTremote);

                AlterAvansi(DTremote, DTremote_cols);

                AlterFakturaStavke(DTremote, DTremote_cols);

                AlterFakture(DTremote, DTremote_cols);

                AlterFaktureBezRobe(DTremote, DTremote_cols);

                AlterPrimka(DTremote, DTremote_cols);

                AlterPrimkaStavke(DTremote, DTremote_cols);

                AlterIzdatnica(DTremote, DTremote_cols);

                AlterIzdatnicaStavke(DTremote, DTremote_cols);

                AlterSkladiste(DTremote, DTremote_cols);

                AlterWebSyn(DTremote, DTremote_cols);

                AlterKartoteka(DTremote, DTremote_cols);

                AlterGrad(DTremote_cols);

                AlterPodaciTvrtke1(DTremote_cols);

                AlterRacun(DTremote_cols);

                AlterRacuni(DTremote, DTremote_cols);

                AlterRacuniStavke(DTremote, DTremote_cols);

                AlterSaldaKonti(DTremote, DTremote_cols);

                AlterInventure(DTremote_cols);

                AlterFakturaTecaj(DTremote_cols);

                AlterPromjenaCijene(DTremote_cols);

                AlterIBAN(DTremote, DTremote_cols);

                AlterUlazneFakture(DTremote, DTremote_cols);

                AlterBlagajnickiIzvjestaj(DTremote, DTremote_cols);
                AlterGrad(DTremote, DTremote_cols);

                AlterPartneri(DTremote, DTremote_cols);

                AlterUlaznaFaktura(DTremote_cols);

                AlterFakture(DTremote_cols);

                ProvjeraViewDB(DTremote_cols);

                PartnerPoslovnice(DTremote);

                AlterOtpremnice(DTremote, DTremote_cols);
                AterAutomatskaUsklada(DTremote, DTremote_cols);
                AlterProizvodackaCijena(DTremote, DTremote_cols);
                AlterResetKarticaKupca(DTremote, DTremote_cols);
                AlterEan(DTremote, DTremote_cols);


                //creiranje funkcija
                createOrReplaceFunction();

            }

            //OVAJ DIO PROVJERAVA DALI POSTOJI SPREMLJENA PROCEDURA I AKO NE POSTOJI KREIRA ISTU
            DataTable DTpohranjena_procedura = classSQL.select("SELECT proname FROM pg_catalog.pg_namespace n JOIN pg_catalog.pg_proc p ON pronamespace = n.oid WHERE nspname = 'public'", "DT").Tables[0];
            DataRow[] rsp = DTpohranjena_procedura.Select("proname='postavi_kolicinu_sql_funkcija'");
            if (rsp.Length == 0 || !File.Exists("2015"))
            {
                classSQL.insert(Properties.Settings.Default.pohranjena_procedura_kolicina);
                classSQL.insert(Properties.Settings.Default.pohranjena_procedura_kolicina_prema_sifri);
                File.WriteAllText("2015", "");
            }

            DataTable DTnacin_pl_kup = classSQL.select("SELECT naziv_placanja FROM nacin_placanja WHERE naziv_placanja = 'Transakcijski račun - Kupon'", "nacin_pl_provjeri").Tables[0];
            string max_broj = classSQL.select("select max(id_placanje) from nacin_placanja", "max broj").Tables[0].Rows[0][0].ToString();
            max_broj = (Convert.ToDecimal(max_broj) + 1).ToString();
            if (DTnacin_pl_kup.Rows.Count < 1)
            {
                classSQL.insert("INSERT INTO nacin_placanja(id_placanje, naziv_placanja, ostalo) VALUES('" + max_broj + "', 'Transakcijski račun - Kupon', '')");
            }

            if (DTpostavke.Columns["ispis_cijele_stavke"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN ispis_cijele_stavke int;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["upozori_iskljucenu_fiskalizaciju"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN upozori_iskljucenu_fiskalizaciju INT default 1;";
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["useVaga"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN useVaga BIT DEFAULT 0;";
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["uzmi_rabat_u_odjavi_komisije"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN uzmi_rabat_u_odjavi_komisije BIT DEFAULT 0;";
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["id_default_skladiste_normativ"] == null)
            {
                sql = string.Format("ALTER TABLE postavke ADD COLUMN id_default_skladiste_normativ int;");
                classSQL.select_settings(sql, "id_default_skladiste_normativ");
                sql = string.Format("update postavke set id_default_skladiste_normativ = {0};", DTpostavke.Rows[0]["default_skladiste"]);
                classSQL.select_settings(sql, "id_default_skladiste_normativ");
            }

            if (DTpostavke.Columns["rucnoUpisivanjeKarticeKupca"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN rucnoUpisivanjeKarticeKupca BIT DEFAULT 0;";
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["COMport"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN COMport nvarchar(15) DEFAULT '5AR';";
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["baudRate"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN baudRate INT DEFAULT 0;";
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["koristiSkladista"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN koristiSkladista nvarchar(200);";
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["roba_zabrani_mijenjanje_cijena"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN roba_zabrani_mijenjanje_cijena BIT DEFAULT 0;";
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["maloprodaja_naplata_gotovina_button_show"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN maloprodaja_naplata_gotovina_button_show BIT DEFAULT 1;";
                classSQL.select_settings(sql, "postavke");
            }
            if (DTpostavke.Columns["maloprodaja_naplata_kartica_button_show"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN maloprodaja_naplata_kartica_button_show BIT DEFAULT 1;";
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["prodaja_automobila"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN prodaja_automobila BIT DEFAULT 0;";
                if (Class.PodaciTvrtka.oibTvrtke == "45031022670")
                {
                    sql = "ALTER TABLE postavke ADD COLUMN prodaja_automobila BIT DEFAULT 1;";
                }
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["kolicina_u_minus"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN kolicina_u_minus BIT DEFAULT 0;";
                classSQL.select_settings(sql, "postavke");
            }
            else
            {
                string verzijaZadnjeProvjereBaze = File.ReadAllText("ProvjeraTablicaBaze" + Util.Korisno.GodinaKojaSeKoristiUbazi.ToString());
                decimal verzijaZadnjeProvjereBazeFile = 0;
                decimal.TryParse(verzijaZadnjeProvjereBaze, out verzijaZadnjeProvjereBazeFile);
                if (verzijaZadnjeProvjereBazeFile < 2.870m)
                {
                    sql = "update postavke SET kolicina_u_minus = 0;";
                    classSQL.select_settings(sql, "postavke");
                }
            }

            if (DTpostavke.Columns["sakrij_formu_za_prodaju_u_minus"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN sakrij_formu_za_prodaju_u_minus BIT DEFAULT 0;";
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["test_fiskalizacija"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN test_fiskalizacija BIT DEFAULT 0;";
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["proizvodnja_faktura_nbc"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN proizvodnja_faktura_nbc BIT DEFAULT 0;";
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["proizvodnja_meduskladisnica_pc"] == null)
            {
                if (Class.PodaciTvrtka.oibTvrtke == "72769568812")
                {
                    sql = "ALTER TABLE postavke ADD COLUMN proizvodnja_meduskladisnica_pc BIT DEFAULT 1;";
                }
                else
                {
                    sql = "ALTER TABLE postavke ADD COLUMN proizvodnja_meduskladisnica_pc BIT DEFAULT 0;";
                }
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["proizvodnja_normativ_pc"] == null)
            {
                if (Class.PodaciTvrtka.oibTvrtke == "72769568812")
                {
                    sql = "ALTER TABLE postavke ADD COLUMN proizvodnja_normativ_pc BIT DEFAULT 1;";
                }
                else
                {
                    sql = "ALTER TABLE postavke ADD COLUMN proizvodnja_normativ_pc BIT DEFAULT 0;";
                }
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["control_box"] == null)
            {
                if (Class.PodaciTvrtka.oibTvrtke == "88985647471")
                {
                    sql = "ALTER TABLE postavke ADD COLUMN control_box BIT DEFAULT 0;";
                }
                else
                {
                    sql = "ALTER TABLE postavke ADD COLUMN control_box BIT DEFAULT 1;";
                }
                classSQL.select_settings(sql, "postavke");
            }

            //nbc_fakture

            if (DTpostavke.Columns["idKalkulacija"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN idKalkulacija INT DEFAULT 1;";
                classSQL.select_settings(sql, "postavke");
            }
            if (DTpostavke.Columns["idFaktura"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN idFaktura INT DEFAULT 1;";
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["upozori_za_minus"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN upozori_za_minus int DEFAULT 0;";
                //classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["porez_na_dohodak"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN porez_na_dohodak decimal DEFAULT 0;";
                //classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["a6print"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN a6print int DEFAULT 0;";
                //classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
            }

            //begin of centrala
            if (DTpostavke.Columns["is_centrala"] == null)
            {
                sql = @"ALTER TABLE postavke ADD COLUMN is_centrala bit DEFAULT 0;";
                classSQL.select_settings(sql, "postavke");
                sql = @"ALTER TABLE postavke ADD COLUMN centrala_aktivno bit DEFAULT 0;";
                classSQL.select_settings(sql, "postavke");
                sql = @"ALTER TABLE postavke ADD COLUMN centrala_poslovnica nvarchar(50);";
                classSQL.select_settings(sql, "postavke");
            }
            //end of centrala

            if (DTpostavke.Columns["oslobodenje_pdva"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN oslobodenje_pdva int;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["putanja_za_skenirane_fajlove"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN putanja_za_skenirane_fajlove nvarchar(500);";
                //classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
                classSQL.Setings_Update("UPDATE postavke SET putanja_za_skenirane_fajlove='skenirani_fajlovi/'");
            }

            if (DTpostavke.Columns["provjera_stanja"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN provjera_stanja int DEFAULT 0;";
                //classSQL.insert(sql);
                classSQL.Setings_Update(sql);
            }

            if (DTpostavke.Columns["grafovi"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN grafovi int DEFAULT 1;";
                //classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["napomena_na_racunu"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN napomena_na_racunu int;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["a5print"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN a5print int;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["putanja_certifikat"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN putanja_certifikat nvarchar(500);";
                //classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["skidaj_skladiste_programski"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN skidaj_skladiste_programski int;";
                classSQL.select_settings(sql, "postavke");
            }
            if (DTpostavke.Columns["automatski_zapisnik"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN automatski_zapisnik BIT DEFAULT 0;";
                if (Class.PodaciTvrtka.oibTvrtke == Class.Postavke.OIB_PC1)
                    sql = "ALTER TABLE postavke ADD COLUMN automatski_zapisnik BIT DEFAULT 1;";

                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["uzmi_avanse_u_promet_kase_POS"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN uzmi_avanse_u_promet_kase_POS BIT DEFAULT 0;";
                if (Class.PodaciTvrtka.oibTvrtke == Class.Postavke.OIB_PC1)
                    sql = "ALTER TABLE postavke ADD COLUMN uzmi_avanse_u_promet_kase_POS BIT DEFAULT 1;";

                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["dozvoli_fikaliranje_0_kn"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN dozvoli_fikaliranje_0_kn BIT DEFAULT 0;";
                if (Class.PodaciTvrtka.oibTvrtke == "56198968279" || Class.PodaciTvrtka.oibTvrtke == "52598763302")
                {
                    sql = "ALTER TABLE postavke ADD COLUMN dozvoli_fikaliranje_0_kn BIT DEFAULT 1;";
                }

                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["koristi_povratnu_naknadu"] == null)
            {
                //sql = "alter table postavke drop column koristi_povratnu_naknadu;";
                //classSQL.Setings_Update(sql);

                sql = "ALTER TABLE postavke ADD COLUMN koristi_povratnu_naknadu BIT DEFAULT 1;";
                classSQL.select_settings(sql, "postavke");
                string[] oibs = new string[] { "00032801314", "00145314055", "00434410602", "00964558163", "01150386022", "01650560875", "02123783350", "02666593481", "03514309996", "03737265550", "03739413747", "03922533094", "04880967900", "05002949086", "05951381428", "06029594826", "06154035942", "06426669645", "06713077771", "06882516543", "07044249964", "07423393581", "07433157339", "07639083197", "07843686620", "09071230296", "09475232067", "10471692974", "10684723211", "11080754991", "11626458935", "12537561535", "12608896072", "13132917725", "13504838223", "14445066056", "14658459059", "14830832457", "14890282574", "16512133506", "16649259477", "16773354710", "16809698383", "16902750615", "16977205841", "17927703270", "18695592197", "19058708438", "19292699272", "19589411295", "21052968362", "21532479840", "21577200794", "22621271142", "22965351113", "23315587468", "23322765132", "23377528174", "23642172090", "24469174749", "24863476586", "25705865250", "25922052055", "26104346671", "26951093296", "28092594790", "28095818343", "28708191256", "28795758945", "29155495318", "29609144971", "29834897561", "30436959378", "30820005785", "31425903291", "32363431523", "32431850676", "32929728933", "33376995853", "33673833361", "34085341856", "34454994337", "34785460397", "34807997575", "35420986177", "35887896196", "35984942522", "36180788522", "36463746458", "36579543153", "36720267085", "36855242653", "36985893015", "37335101028", "38225889221", "38799206384", "38925022583", "39584665281", "40097758416", "40993270015", "41109922301", "41250518628", "41355227266", "42644339901", "42667761694", "42809901116", "43095922969", "43181200412", "43250576732", "43327893078", "43418321773", "43429334548", "43453315378", "43710892024", "43879685686", "44219526794", "44223863190", "44386753506", "45031022670", "46256698417", "47691847161", "48158929689", "48167107908", "48438342582", "48449005656", "48630421938", "48950602226", "51179740620", "51652929155", "51721439980", "53215683594", "53870720259", "54821481767", "55754023566", "56120375369", "56356050912", "56664162697", "57375677395", "57786498387", "57829962896", "58314067995", "58554868405", "58820434031", "59977248176", "59979839341", "60949457482", "61594659153", "61713540254", "62572410306", "62832727394", "62933964294", "63246152838", "63407163377", "64330958012", "66173364635", "66946948595", "67648449897", "67660751355", "68700393574", "68887081508", "70117058988", "70305232839", "70989573925", "71418385724", "71719224927", "71928655682", "72467408461", "72710803315", "72769568812", "73133648845", "73761371048", "47165970760", "74340789103", "76309563023", "76381111322", "76675979509", "76846500940", "76879579405", "76964036726", "77022691931", "77696837396", "77961822351", "78411074852", "78600777524", "79532422631", "81501079989", "81547609838", "82202130704", "82336154711", "82374273773", "82441148678", "82701170088", "82708363106", "83522557013", "83825547247", "84053208913", "85501330524", "86115638842", "86637272042", "86730325034", "87545676358", "88985647471", "89780910318", "90297427352", "90386837388", "90916959510", "91330825449", "91538513762", "92183781696", "92616537081", "92878756725", "93149697023", "93623008482", "93721363179", "93726965683", "94288889253", "94968447156", "95307198967", "96432163942", "96913143663", "96979018391", "97547155250", "97650187999", "97769615219", "97987543289", "98153631516", "98327101901", "98675147453", "98735539147", "99124328679", "99384515430", "99872825684" };
                if (oibs.Contains(Class.PodaciTvrtka.oibTvrtke))
                {
                    sql = "update postavke set koristi_povratnu_naknadu = 0;";
                    classSQL.Setings_Update(sql);
                }
            }

            if (DTpostavke.Columns["certifikat_zaporka"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN certifikat_zaporka nvarchar(100);";
                //classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["logopath"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN logopath nvarchar(80);";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["logo"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN logo int;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "postavke");
            }

            if (DTpostavke.Columns["useUdsGame"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN useUdsGame BIT DEFAULT 0;";
                classSQL.select_settings(sql, "postavke");
                sql = "ALTER TABLE postavke ADD COLUMN useUdsGameEmployees BIT DEFAULT 0;";
                classSQL.select_settings(sql, "postavke");
                sql = "ALTER TABLE postavke ADD COLUMN useUdsGameApiKey NVARCHAR(1500);";
                classSQL.select_settings(sql, "postavke");
            }

            string virman = "SELECT naziv_placanja, id_placanje FROM nacin_placanja";
            DataTable DTvirman = classSQL.select(virman, "virman").Tables[0];

            for (int i = 0; DTvirman.Rows.Count > i; i++)
            {
                string nacin_pl = DTvirman.Rows[i]["naziv_placanja"].ToString();
                string id_plac = DTvirman.Rows[i]["id_placanje"].ToString();

                if (nacin_pl.Trim().ToLower() == "gotovina")
                {
                    string got = "UPDATE nacin_placanja SET naziv_placanja = 'Novčanice' WHERE id_placanje = '" + id_plac + "' ";
                    classSQL.update(got);
                }

                if (nacin_pl.Trim().ToLower() == "virman")
                {
                    string vir = "UPDATE nacin_placanja SET naziv_placanja = 'Transakcijski račun' WHERE id_placanje = '" + id_plac + "' ";
                    classSQL.update(vir);
                }
            }

            if (DTpostavke.Columns["fiskalizacija_faktura_prikazi_obavijest"] == null)
            {
                sql = "ALTER TABLE postavke ADD COLUMN fiskalizacija_faktura_prikazi_obavijest BIT DEFAULT 1;";
                classSQL.select_settings(sql, "postavke");
            }

            //-----------------------------------------------------SQL COMPACT----------------------------------------------------------------------------------------------

            DataTable DT_compactDB = classSQL.select_settings("select table_name from information_schema.tables where TABLE_TYPE <> 'VIEW' ", "Table").Tables[0];

            if (DT_compactDB.Rows.Count > 0)
            {
                DataRow[] dataROW = DT_compactDB.Select("table_name = 'podaci_poslovnica_fiskal'");
                if (dataROW.Length == 0)
                {
                    string sqlTab = "CREATE TABLE podaci_poslovnica_fiskal" +
                        " (OIB nvarchar(12), " +
                        " oznakaPP nvarchar(30)," +
                        " ulica nvarchar(50)," +
                        " broj nvarchar(25)," +
                        " broj_dodatak nvarchar(25)," +
                        " posta nvarchar(10)," +
                        " naselje nvarchar(50)," +
                        " opcina nvarchar(25)," +
                        " datum nvarchar(25)," +
                        " r_vrijeme nvarchar(500)," +
                        " zatvaranje nvarchar(1)" +
                        " )";
                    classSQL.select_settings(sqlTab, "podaci_poslovnica_fiskal");

                    classSQL.select_settings("INSERT INTO podaci_poslovnica_fiskal (OIB) VALUES ('00000000000')", "podaci_poslovnica_fiskal");
                }

                AlterAktivnostPodataka(DT_compactDB);

                AlterPodaciTvrtke(DTcompact_cols);

                try
                {
                    int ss;
                    DataRow[] row = DTcompact_cols.Select("table_name = 'podaci_tvrtka' and column_name = 'iban'");

                    if (row.Length > 0)
                    {
                        int.TryParse(row[0]["CHARACTER_MAXIMUM_LENGTH"].ToString(), out ss);
                        if (ss < 300)
                        {
                            classSQL.select_settings("alter table podaci_tvrtka alter column iban nvarchar(300)", "podaci_tvrtka");
                            //classSQL.update("alter table podaci_tvrtka alter column iban type character varying");
                        }
                    }
                }
                catch { }

                try
                {
                    int ss;
                    DataRow[] row = DTremote_cols.Select("table_name = 'partners' and column_name = 'email'");

                    if (row.Length > 0)
                    {
                        int.TryParse(row[0]["CHARACTER_MAXIMUM_LENGTH"].ToString(), out ss);
                        if (ss < 150)
                        {
                            classSQL.select("alter table partners alter column email type character varying(150)", "partners");
                        }//alter table faktura_stavke alter column sifra type varchar(30);
                    }
                }
                catch { }

                if (DTpostavke.Columns["lokacija_sigurnosne_kopije"] == null)
                {
                    sql = "ALTER TABLE postavke ADD COLUMN lokacija_sigurnosne_kopije ntext;";
                    classSQL.select_settings(sql, "postavke");
                }

                if (DTpostavke.Columns["backup_aktivnost"] == null)
                {
                    sql = "ALTER TABLE postavke ADD COLUMN backup_aktivnost int;";
                    classSQL.select_settings(sql, "postavke");
                }

                if (DTpostavke.Columns["direct_print"] == null)
                {
                    sql = "ALTER TABLE postavke ADD COLUMN direct_print int;";
                    //classSQL.insert(sql);
                    classSQL.select_settings(sql, "postavke");
                    classSQL.Setings_Update("UPDATE postavke SET direct_print='0'");
                }

                if (DTpostavke.Columns["posalji_dokumente_na_web"] == null)
                {
                    sql = "ALTER TABLE postavke ADD COLUMN posalji_dokumente_na_web int DEFAULT 0;";
                    classSQL.select_settings(sql, "postavke");
                }

                if (DTpostavke.Columns["ladicaOn"] == null)
                {
                    sql = "ALTER TABLE postavke ADD COLUMN ladicaOn int;";
                    //classSQL.insert(sql);
                    classSQL.select_settings(sql, "postavke");
                    classSQL.Setings_Update("UPDATE postavke SET ladicaOn='0'");
                }

                if (DTpostavke.Columns["naplatni_uredaj_faktura"] == null)
                {
                    sql = "ALTER TABLE postavke ADD COLUMN naplatni_uredaj_faktura int;";
                    //classSQL.insert(sql);
                    classSQL.select_settings(sql, "postavke");
                    classSQL.Setings_Update("UPDATE postavke SET naplatni_uredaj_faktura='2'");
                }

                if (DTpostavke.Columns["naplatni_uredaj_avans"] == null)
                {
                    sql = "ALTER TABLE postavke ADD COLUMN naplatni_uredaj_avans int;";
                    //classSQL.insert(sql);
                    classSQL.select_settings(sql, "postavke");
                    classSQL.Setings_Update("UPDATE postavke SET naplatni_uredaj_avans='3'");
                }

                if (DTpostavke.Columns["naplatni_uredaj_faktura_bez_robe"] == null)
                {
                    sql = "ALTER TABLE postavke ADD COLUMN naplatni_uredaj_faktura_bez_robe int;";
                    //classSQL.insert(sql);
                    classSQL.select_settings(sql, "postavke");
                    classSQL.Setings_Update("UPDATE postavke SET naplatni_uredaj_faktura_bez_robe='4'");
                }

                if (DTpostavke.Columns["ispis_partnera"] == null)
                {
                    sql = "ALTER TABLE postavke ADD COLUMN ispis_partnera int;";
                    classSQL.select_settings(sql, "postavke");
                    classSQL.Setings_Update("UPDATE postavke SET ispis_partnera='1'");
                }

                if (DTpostavke.Columns["veleprodaja"] == null)
                {
                    sql = "ALTER TABLE postavke ADD COLUMN veleprodaja int;";
                    //classSQL.insert(sql);
                    classSQL.select_settings(sql, "postavke");
                    classSQL.Setings_Update("UPDATE postavke SET veleprodaja = '0'");
                }

                if (DTpostavke.Columns["webstranica"] == null)
                {
                    sql = "ALTER TABLE postavke ADD COLUMN webstranica ntext;";
                    //classSQL.insert(sql);
                    classSQL.select_settings(sql, "postavke");
                }
            }
        }

        private static void AlterResetKarticaKupca(DataTable DTremote, DataTable DTremote_cols)
        {
            try
            {
                DataRow[] a = DTremote_cols.Select("table_name = 'reset_kartica_kupca'");
                if (a.Count() == 0)
                {
                    string sql = @"create table reset_kartica_kupca (
	kartica_kupca character varying(50),
	datum_resetiranja timestamp without time zone
)";

                    classSQL.insert(sql);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void createOrReplaceFunction()
        {
            string sql = @"CREATE OR REPLACE FUNCTION changeskladiste(i1 integer, i2 integer)
  RETURNS void AS
$BODY$
begin
DECLARE
    tables CURSOR FOR select table_name, column_name, *
			from information_schema.columns
			where table_schema = 'public' and column_name like '%id_skladiste%' and upper(is_updatable) = 'YES';
    nbRow int;
BEGIN
	FOR table_record IN tables LOOP
		EXECUTE concat('UPDATE ', table_record.table_name, ' set ', table_record.column_name, ' = ', i2, ' where ', table_record.column_name, ' = ', i1);
	END LOOP;
	PERFORM setval('skladiste_id_skladiste_seq', (SELECT coalesce(MAX(id_skladiste), 0) +1 FROM skladiste));
end;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION changeskladiste(integer, integer)
  OWNER TO postgres;";
            classSQL.update(sql);

            sql = @"CREATE OR REPLACE FUNCTION getcurrnbc(IN _godina integer, IN _datum timestamp without time zone, IN _skladiste integer, IN _kolicina numeric, IN _sifra character varying, IN _nbc numeric DEFAULT 0, IN _asc boolean DEFAULT true)
  RETURNS TABLE(articleid character varying, currentitems numeric, currentvalue numeric, datum timestamp without time zone) AS
$BODY$
declare
	mviews RECORD;
	newkol numeric(15,6) := _kolicina;
	suma numeric(15,6) := 0;
	do_datuma timestamp without time zone := case when _datum is null then NOW() else _datum end;
	r_row roba%ROWTYPE;
	--uzmi integer := case when _kolicina = 0 then -1 else 0 end;
begin

SELECT * INTO r_row FROM roba WHERE sifra = _sifra;
IF upper(r_row.oduzmi) != 'DA' THEN
	RETURN QUERY (
		select _sifra as ArticleId, _kolicina as CurrentItems, round(_nbc, 6) as CurrentValue, do_datuma as datum
	);
ELSE

drop table if exists Stock;
create temp table Stock as
select  x.datum as TranDate, x.sifra as ArticleID, x.kolicina as Items, x.nbc as Price, x.io as TranCode from (

	--MIJENJANJE KOLIČINE I NABAVNE CIJENE	__START__
	--POCETNO
	select ch1.datum as datum, coalesce(replace('0'::varchar, ',','.')::bigint,0) as broj, ch1.id_skladiste as skladiste, ch1.sifra as sifra,
	round(coalesce(replace(ch1.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	round(coalesce(ch1.nbc::numeric, 0),6) as nbc,
	round(round(coalesce(ch1.prodajna_cijena::numeric, 0), 6) / (1 + round(coalesce((ch1.porez)::numeric, 0),2) / 100), 6) as vpc,
	round(coalesce((ch1.porez)::numeric, 0),2) as pdv,
	'IN'::varchar as io, 'pocetno'::varchar as doc
	from pocetno ch1

	union

	--PRIMKA
	select p.datum as datum, coalesce(replace(p.broj::varchar, ',','.')::bigint,0) as broj, p.id_skladiste as skladiste, ch1.sifra as sifra,
	round(coalesce(replace(ch1.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	round(coalesce(ch1.nbc::numeric, 0),6) as nbc,
	round(coalesce(ch1.vpc::numeric, 0), 6) as vpc,
	round(coalesce(replace(ch1.pdv,',','.')::numeric, 0),2) as pdv,
	'IN'::varchar as io, 'primka'::varchar as doc
	from primka_stavke ch1
	right join primka p on ch1.id_primka = p.id_primka

	union

	--KALKULACIJA
	select p.datum as datum, coalesce(replace(p.broj::varchar, ',','.')::bigint,0) as broj, ch1.id_skladiste as skladiste, ch1.sifra as sifra,
	round(coalesce(replace(ch1.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	round(coalesce(ch1.fak_cijena, 0),6) - round((round(coalesce(ch1.fak_cijena, 0),6) * replace(rabat, ',','.')::numeric / 100), 6) as nbc,
	round(coalesce(ch1.vpc::numeric, 0), 6) as vpc,
	round(coalesce(replace(ch1.porez,',','.')::numeric, 0),2) as pdv,
	'IN'::varchar as io, 'kalkulacija'::varchar as doc
	from kalkulacija_stavke ch1
	right join kalkulacija p on ch1.broj = p.broj and ch1.id_skladiste = p.id_skladiste

	union

	--MEĐUSKLADISNICA
	select p.datum as datum, coalesce(replace(p.broj::varchar, ',','.')::bigint,0) as broj, p.id_skladiste_od as skladiste, ch1.sifra as sifra,
	round(coalesce(replace(ch1.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	round(coalesce(ch1.nbc::numeric, 0),6) as nbc,
	round(coalesce(ch1.vpc::numeric, 0), 6) as vpc,
	round(coalesce(replace(ch1.pdv,',','.')::numeric, 0),2) as pdv,
	'OUT'::varchar as io, 'međuskladisnica'::varchar as doc
	from meduskladisnica_stavke ch1
	right join meduskladisnica p on ch1.broj = p.broj and ch1.godina = p.godina and ch1.iz_skladista = p.id_skladiste_od

	union

	--MEĐUSKLADISNICA
	select p.datum as datum, coalesce(replace(p.broj::varchar, ',','.')::bigint,0) as broj, p.id_skladiste_do as skladiste, ch1.sifra as sifra,
	round(coalesce(replace(ch1.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	round(coalesce(ch1.nbc::numeric, 0),6) as nbc,
	round(coalesce(ch1.vpc::numeric, 0), 6) as vpc,
	round(coalesce(replace(ch1.pdv,',','.')::numeric, 0),2) as pdv,
	'IN'::varchar as io, 'međuskladisnica'::varchar as doc
	from meduskladisnica_stavke ch1
	right join meduskladisnica p on ch1.broj = p.broj and ch1.godina = p.godina and ch1.iz_skladista = p.id_skladiste_od
	-- MIJENJANJE KOLIČINE I NABAVNE CIJENE	__STOP__

	union

	--MIJENJANJE KOLICINE	__START__
	--MALOPRODAJA
	select p.datum_racuna as datum, coalesce(replace(p.broj_racuna::varchar, ',','.')::bigint,0) as broj, ch1.id_skladiste as skladiste, ch1.sifra_robe as sifra,
	round(coalesce(replace(ch1.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	round(coalesce(ch1.nbc::numeric, 0),6) as nbc,
	round(coalesce(ch1.vpc::numeric, 0), 6) as vpc,
	round(coalesce(replace(ch1.porez,',','.')::numeric, 0),2) as pdv,
	case when round(coalesce(replace(ch1.kolicina, ',','.')::numeric, 0), 6) > 0 then 'OUT'::varchar else 'IN'::varchar end as io, 'racun'::varchar as doc
	from racun_stavke ch1
	right join racuni p on ch1.id_ducan = p.id_ducan and ch1.id_kasa = p.id_kasa and ch1.broj_racuna = p.broj_racuna

	union

	--FAKTURA
	select p.date as datum, coalesce(replace(p.broj_fakture::varchar, ',','.')::bigint,0) as broj, ch1.id_skladiste as skladiste, ch1.sifra as sifra,
	round(coalesce(replace(ch1.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	round(coalesce(ch1.nbc::numeric, 0),6) as nbc,
	round(coalesce(ch1.vpc::numeric, 0), 6) as vpc,
	round(coalesce(replace(ch1.porez,',','.')::numeric, 0),2) as pdv,
	case when round(coalesce(replace(ch1.kolicina, ',','.')::numeric, 0), 6) > 0 then 'OUT'::varchar else 'IN'::varchar end as io, 'faktura'::varchar as doc
	from faktura_stavke ch1
	right join fakture p on ch1.id_ducan = p.id_ducan and ch1.id_kasa = p.id_kasa and ch1.broj_fakture = p.broj_fakture
	where p.oduzmi_iz_skladista::integer = 1

	union

	--FAKTURA ZA VAN
	select p.date as datum, coalesce(replace(p.broj_fakture::varchar, ',','.')::bigint,0) as broj, ch1.id_skladiste as skladiste, ch1.sifra as sifra,
	round(coalesce(replace(ch1.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	round(coalesce(ch1.nbc::numeric, 0),6) as nbc,
	round(coalesce(ch1.vpc::numeric, 0), 6) as vpc,
	round(coalesce(replace(ch1.porez,',','.')::numeric, 0),2) as pdv,
	'OUT'::varchar as io, 'faktura za van'::varchar as doc
	from faktura_van_stavke ch1
	right join fakture_van p on ch1.broj_fakture = p.broj_fakture

	union

	--OTPREMNICA
	select p.datum as datum, coalesce(replace(p.broj_otpremnice::varchar, ',','.')::bigint,0) as broj, ch1.id_skladiste as skladiste, ch1.sifra_robe as sifra,
	round(coalesce(replace(ch1.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	round(coalesce(ch1.nbc::numeric, 0),6) as nbc,
	round(coalesce(ch1.vpc::numeric, 0), 6) as vpc,
	round(coalesce(replace(ch1.porez,',','.')::numeric, 0),2) as pdv,
	'OUT'::varchar as io, 'otpremnica'::varchar as doc
	from otpremnica_stavke ch1
	right join otpremnice p on ch1.broj_otpremnice = p.broj_otpremnice and ch1.id_skladiste = p.id_skladiste

	union

	--IZDATNICA
	select p.datum as datum, coalesce(replace(p.broj::varchar, ',','.')::bigint,0) as broj, p.id_skladiste as skladiste, ch1.sifra as sifra,
	round(coalesce(replace(ch1.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	round(coalesce(ch1.nbc::numeric, 0),6) as nbc,
	round(coalesce(ch1.vpc::numeric, 0), 6) as vpc,
	round(coalesce(replace(ch1.pdv,',','.')::numeric, 0),2) as pdv,
	'OUT'::varchar as io, 'izdatnica'::varchar as doc
	from izdatnica_stavke ch1
	right join izdatnica p on ch1.id_izdatnica = p.id_izdatnica

	union

	--POCETNO STANJE
	select p.date as datum, coalesce(replace(p.broj::varchar, ',','.')::bigint,0) as broj, p.id_skladiste as skladiste, ch1.sifra as sifra,
	round(coalesce(replace(ch1.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	round(coalesce(ch1.nbc::numeric, 0),6) as nbc,
	round((round(coalesce(ch1.mpc::numeric, 0), 6) / (1 + round(coalesce(replace(ch1.pdv,',','.')::numeric, 0),2) / 100)), 6) as vpc,
	round(coalesce(replace(ch1.pdv,',','.')::numeric, 0),2) as pdv,
	'IN'::varchar as io, 'pocetno stanje'::varchar as doc
	from pocetno_stanje_stavke ch1
	right join pocetno_stanje p on ch1.broj = p.broj

	union

	--POVRAT ROBE
	select p.datum as datum, coalesce(replace(p.broj::varchar, ',','.')::bigint,0) as broj, p.id_skladiste as skladiste, ch1.sifra as sifra,
	round(coalesce(replace(ch1.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	round(coalesce(ch1.nbc::numeric, 0),6) as nbc,
	round(coalesce(ch1.vpc::numeric, 0), 6) as vpc,
	round(coalesce(replace(ch1.pdv,',','.')::numeric, 0),2) as pdv,
	'OUT'::varchar as io, 'povrat robe'::varchar as doc
	from povrat_robe_stavke ch1
	right join povrat_robe p on ch1.broj = p.broj

	union

	--RADNI NALOG
	select p.datum_naloga as datum, coalesce(replace(p.broj_naloga::varchar, ',','.')::bigint,0) as broj, ch1.id_skladiste as skladiste, ch1.sifra_robe as sifra,
	round(coalesce(replace(ch1.kolicina, ',','.')::numeric, 0), 6) as kolicina,
	round(coalesce(ch1.nbc::numeric, 0),6) as nbc,
	round(coalesce(ch1.vpc::numeric, 0), 6) as vpc,
	round(coalesce(replace(ch1.porez,',','.')::numeric, 0),2) as pdv,
	'IN'::varchar as io, 'radni nalog'::varchar as doc
	from radni_nalog_stavke ch1
	right join radni_nalog p on ch1.broj_naloga = p.broj_naloga
	where p.godina_naloga::integer = _godina

	union

	--NORMATIV
	select p1.datum_naloga as datum, coalesce(replace(p2.broj_normativa::varchar, ',','.')::bigint,0) as broj, ch3.id_skladiste as skladiste, ch3.sifra_robe as sifra,
	round((round(coalesce(replace(ch3.kolicina, ',','.')::numeric, 0), 6) * round(coalesce(replace(ch2.kolicina, ',','.')::numeric, 0), 6)), 6) as kolicina,
	round(coalesce(ch1.nbc::numeric, 0),6) as nbc,
	round(coalesce(ch1.vpc::numeric, 0), 6) as vpc,
	round(coalesce(replace(ch1.pdv,',','.')::numeric, 0),2) as pdv,
	'OUT'::varchar as io, 'normativ'::varchar as doc
	from radni_nalog_normativ ch1
	right join radni_nalog_stavke ch2 on ch1.broj = ch2.broj_naloga
	right join radni_nalog p1 on ch2.broj_naloga = p1.broj_naloga
	left join normativi p2 on ch2.sifra_robe = p2.sifra_artikla
	left join normativi_stavke ch3 on p2.broj_normativa = ch3.broj_normativa and ch1.sifra = ch3.sifra_robe
	where p1.godina_naloga::integer = _godina

	union

	--INVENTURA
	select p.datum as datum, coalesce(replace(p.broj_inventure::varchar, ',','.')::bigint,0) as broj, p.id_skladiste as skladiste, ch1.sifra_robe as sifra,
	round(coalesce(replace(ch1.kolicina, ',','.')::numeric, 0), 6) - ch1.kolicina_koja_je_bila as kolicina,
	round(coalesce(ch1.nbc::numeric, 0),6) as nbc,
	round((round(coalesce(ch1.cijena::numeric, 0), 6) / (1 + round(coalesce(replace(ch1.porez,',','.')::numeric, 0),2) / 100)), 6) as vpc,
	round(coalesce(replace(ch1.porez,',','.')::numeric, 0),2) as pdv,
	'IN'::varchar as io, 'inventura'::varchar as doc
	from inventura_stavke ch1
	right join inventura p on ch1.broj_inventure = p.broj_inventure
	where p.pocetno_stanje <> 1

) x
left join roba on x.sifra = roba.sifra
where x.skladiste = _skladiste and x.datum < do_datuma and
roba.oduzmi = 'DA'
and case when _sifra is null then 1=1 else x.sifra = _sifra end
order by x.datum asc, x.broj asc, x.sifra asc, doc asc;

drop table if exists StockTemp;
create temp table StockTemp as
SELECT
 Stock.ArticleId, TranDate, Price ,Items,TranCode

-- dummy column to get the current price in the next step, new group starts with every 'IN'
,SUM(CASE WHEN Stock.TranCode = 'IN' THEN 1 ELSE 0 END)
 OVER (PARTITION BY Stock.ArticleID
       ORDER BY Stock.TranDate
       ROWS UNBOUNDED PRECEDING) AS PriceGroup

-- Aggregating all in/out movements -> number of items left in stock after all transactions
,SUM(CASE WHEN Stock.TranCode IN ('IN', 'RET') THEN Stock.Items ELSE -Stock.Items END)
 OVER (PARTITION BY Stock.ArticleID) AS TotalStock

-- reverse sum of all inbound IN/RET movements
,(SUM(CASE WHEN Stock.TranCode IN ('IN', 'RET') THEN Stock.Items END)
 OVER (PARTITION BY Stock.ArticleID)
- SUM(CASE WHEN Stock.TranCode IN ('IN', 'RET') THEN Stock.Items END)
 OVER (PARTITION BY Stock.ArticleID
       ORDER BY Stock.TranDate
       ROWS UNBOUNDED PRECEDING)) AS RollingStock

FROM Stock;

drop table if exists StockTempSecond;
create temp table StockTempSecond as
SELECT
      x.ArticleId

     -- how many items will be used from this transaction, maybe less than all for the oldest row
     ,CASE WHEN x.RollingStock + x.Items > x.TotalStock THEN x.TotalStock - x.RollingStock ELSE x.Items END AS ItemCnt

     -- find the latest IN-price for RET rows
     ,MAX(x.Price)
      OVER (PARTITION BY x.ArticleID, x.PriceGroup
            ORDER BY x.TranDate) AS CurrentPrice
   FROM
    (
      SELECT
         StockTemp.ArticleId, StockTemp.TranDate, StockTemp.Price, StockTemp.Items --,TranCode

        -- dummy column to get the current price in the next step, new group starts with every 'IN'
        ,StockTemp.PriceGroup

        -- Aggregating all in/out movements -> number of items left in stock after all transactions
        ,StockTemp.TotalStock

        -- reverse sum of all inbound IN/RET movements
        ,StockTemp.RollingStock

      FROM StockTemp
      -- only keep the row needed to calculate the value
      -- plus all IN rows to find the current price for RET rows in the next step

      where ((StockTemp.TranCode = 'IN') OR (StockTemp.RollingStock <= StockTemp.TotalStock AND StockTemp.TranCode = 'RET'))AND (StockTemp.TotalStock > 0)
    ) x;

FOR mviews IN
SELECT
   dt.ArticleId
  --,SUM(dt.ItemCnt) AS CurrentItems -- same as TotalStock
  ,dt.ItemCnt AS CurrentItems
  --,SUM(dt.ItemCnt * dt.CurrentPrice) AS CurrentValue
  ,dt.CurrentPrice AS CurrentValue

FROM
 (
   SELECT
      StockTempSecond.ArticleId, StockTempSecond.ItemCnt, StockTempSecond.CurrentPrice
   FROM
    StockTempSecond
   where StockTempSecond.ItemCnt >= 0
   order by StockTempSecond.ArticleId ASC
 ) AS dt
--GROUP BY 1 --
ORDER BY 1
LOOP

	IF mviews.CurrentItems > 0 THEN
		IF newkol <= mviews.CurrentItems THEN
			suma := suma + (newkol * mviews.CurrentValue);
			EXIT;  -- exit loop
		ELSE
			suma := suma + (mviews.CurrentItems * mviews.CurrentValue);
			newkol := newkol - mviews.CurrentItems;
		END IF;
	END IF;

END LOOP;

	RETURN QUERY (
		select _sifra as ArticleId, _kolicina as CurrentItems, case when _kolicina = 0 then round(suma/1, 6) else round(suma/_kolicina, 6) end as CurrentValue, do_datuma as datum
	);

END IF;

end;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION getcurrnbc(integer, timestamp without time zone, integer, numeric, character varying, numeric, boolean)
  OWNER TO postgres;";
            classSQL.update(sql);

            sql = @"CREATE OR REPLACE FUNCTION public.postavi_kolicinu_sql_funkcija_prema_sifri(skup_sifra character varying, datum_param character varying)
          RETURNS void AS
        $BODY$
        BEGIN

	        --RACUNI
	        CREATE TEMP TABLE _racuni AS
	        SELECT sifra_robe AS sifra,id_skladiste as skladiste,COALESCE(SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)),0) AS kolicina FROM racun_stavke LEFT JOIN racuni ON racuni.broj_racuna = racun_stavke.broj_racuna WHERE racuni.datum_racuna >= @datum_param GROUP BY sifra_robe,id_skladiste;

	        --KALKULACIJE
	        CREATE TEMP TABLE _kalkulacije AS
	        SELECT sifra AS sifra,id_skladiste as skladiste,COALESCE(SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)),0) AS kolicina FROM kalkulacija_stavke LEFT JOIN kalkulacija ON kalkulacija.broj = kalkulacija_stavke.broj WHERE kalkulacija.racun_datum >= @datum_param GROUP BY sifra,id_skladiste;

	        --IZDATNICE
	        CREATE TEMP TABLE _izdatnice AS
	        SELECT sifra AS sifra,id_skladiste as skladiste,COALESCE(SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)),0) AS kolicina FROM izdatnica_stavke LEFT JOIN izdatnica ON izdatnica.broj=izdatnica_stavke.broj AND izdatnica.id_izdatnica=izdatnica_stavke.id_izdatnica WHERE izdatnica.datum >= @datum_param GROUP BY sifra,id_skladiste;

	        --INVENTURE
	        -- CREATE TEMP TABLE _inventura AS
	        -- SELECT inventura_stavke.sifra_robe AS sifra, inventura.id_skladiste as skladiste, COALESCE(SUM(CAST(REPLACE(inventura_stavke.kolicina,',','.') as NUMERIC) - inventura_stavke.kolicina_koja_je_bila), 0)  AS kolicina
	        -- FROM inventura_stavke
	        -- LEFT JOIN inventura ON inventura.broj_inventure=inventura_stavke.broj_inventure where inventura.pocetno_stanje = 0 GROUP BY inventura_stavke.sifra_robe, inventura.id_skladiste;

	        --PRIMKE
	        CREATE TEMP TABLE _primke AS
	        SELECT sifra AS sifra,id_skladiste as skladiste,COALESCE(SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)),0) AS kolicina FROM primka_stavke LEFT JOIN primka ON primka.id_primka=primka_stavke.id_primka WHERE primka.datum >= @datum_param GROUP BY sifra,id_skladiste;

	        --FAKTURE
	        CREATE TEMP TABLE _fakture AS
	        SELECT sifra AS sifra,id_skladiste as skladiste,COALESCE(SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)),0) AS kolicina FROM faktura_stavke LEFT JOIN fakture ON fakture.broj_fakture = faktura_stavke.broj_fakture AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa WHERE fakture.oduzmi_iz_skladista='1' AND fakture.date >= @datum_param GROUP BY sifra,id_skladiste;

	        --MEĐUSKLADIŠNICA IZ SKLADIŠTA
	        CREATE TEMP TABLE _meduskladisnica_iz_skladista AS
	        SELECT sifra AS sifra,meduskladisnica.id_skladiste_od as skladiste,COALESCE(SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)),0) AS kolicina FROM meduskladisnica_stavke LEFT JOIN meduskladisnica ON meduskladisnica.broj=meduskladisnica_stavke.broj AND meduskladisnica.id_skladiste_od=meduskladisnica_stavke.iz_skladista WHERE meduskladisnica.datum >= @datum_param GROUP BY sifra,id_skladiste_od;

	        --MEĐUSKLADIŠNICA U SKLADIŠTE
	        CREATE TEMP TABLE _meduskladisnica_u_skladiste AS
	        SELECT sifra AS sifra,meduskladisnica.id_skladiste_do as skladiste,COALESCE(SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)),0) AS kolicina FROM meduskladisnica_stavke LEFT JOIN meduskladisnica ON meduskladisnica.broj=meduskladisnica_stavke.broj AND meduskladisnica.id_skladiste_od=meduskladisnica_stavke.iz_skladista WHERE meduskladisnica.datum >= @datum_param GROUP BY sifra,id_skladiste_do;

	        --OTPREMNICA
	        CREATE TEMP TABLE _otpremnica AS
	        SELECT sifra_robe AS sifra,id_skladiste as skladiste, COALESCE(SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)),0) AS kolicina FROM otpremnica_stavke LEFT JOIN otpremnice ON otpremnice.broj_otpremnice = otpremnica_stavke.broj_otpremnice WHERE oduzmi = 'DA' AND otpremnice.datum >= @datum_param GROUP BY sifra_robe,id_skladiste;

	        --OTPIS ROBE
	        CREATE TEMP TABLE _otpis_robe AS
	        SELECT sifra AS sifra,id_skladiste as skladiste,COALESCE(SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)),0) AS kolicina FROM otpis_robe_stavke LEFT JOIN otpis_robe ON otpis_robe.broj=otpis_robe_stavke.broj WHERE otpis_robe.datum >= @datum_param GROUP BY sifra,id_skladiste;

	        --POVRAT ROBE
	        CREATE TEMP TABLE _povrat_robe AS
	        SELECT sifra AS sifra,id_skladiste as skladiste,COALESCE(SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)),0) AS kolicina FROM povrat_robe_stavke LEFT JOIN povrat_robe ON povrat_robe.broj=povrat_robe_stavke.broj WHERE povrat_robe.datum >= @datum_param GROUP BY sifra,id_skladiste;

	        --POCETNO
	        CREATE TEMP TABLE _pocetno AS
	        SELECT sifra AS sifra,id_skladiste as skladiste,COALESCE(CAST(REPLACE(kolicina,',','.') as NUMERIC),0) AS kolicina FROM pocetno GROUP BY sifra,id_skladiste,kolicina;

	        --RADNI NALOG STAVKE
	        CREATE TEMP TABLE _radni_nalog_stavke AS
	        SELECT sifra_robe AS sifra,id_skladiste as skladiste,COALESCE(SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)),0) AS kolicina FROM radni_nalog_stavke LEFT JOIN radni_nalog ON radni_nalog.broj_naloga=radni_nalog_stavke.broj_naloga WHERE radni_nalog.datum_naloga >= @datum_param GROUP BY sifra_robe,id_skladiste;

	        --RADNI NALOG STAVKE-->NORMATIVI
	        CREATE TEMP TABLE _radni_nalog_normativi AS
	        SELECT normativi_stavke.sifra_robe AS sifra,normativi_stavke.id_skladiste as skladiste,
	        COALESCE(SUM
	        (
		        CAST(REPLACE(kolicina,',','.') as NUMERIC)*
		        (SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM radni_nalog_stavke LEFT JOIN radni_nalog ON radni_nalog.broj_naloga=radni_nalog_stavke.broj_naloga
		          WHERE normativi.sifra_artikla=radni_nalog_stavke.sifra_robe AND radni_nalog.datum_naloga >= @datum_param
		        )
	        ),0) AS kolicina
	        FROM normativi_stavke
                LEFT JOIN normativi ON normativi.broj_normativa = normativi_stavke.broj_normativa
                GROUP BY sifra_robe,id_skladiste;

                --NORMATIVI (NA USLUGU SE DODAJU PRODAJNI ARTIKLI)
                CREATE TEMP TABLE _normativi_usluga AS
	        SELECT normativi_stavke.sifra_robe AS sifra,normativi_stavke.id_skladiste as skladiste,
	        COALESCE(SUM
	        (
		        CAST(REPLACE(kolicina,',','.') as NUMERIC)*
		        (SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM racun_stavke LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa
		          WHERE normativi.sifra_artikla=racun_stavke.sifra_robe AND racuni.datum_racuna >= @datum_param
		        )
	        ),0) AS kolicina
	        FROM normativi_stavke
                LEFT JOIN normativi ON normativi.broj_normativa = normativi_stavke.broj_normativa
                LEFT JOIN roba ON roba.sifra=normativi.sifra_artikla
                WHERE roba.oduzmi='NE'
                GROUP BY sifra_robe,id_skladiste;

	        UPDATE roba_prodaja SET kolicina=
	        (
	        REPLACE(CAST(
		        COALESCE(
			        COALESCE((SELECT _pocetno.kolicina FROM _pocetno WHERE _pocetno.sifra=roba_prodaja.sifra AND _pocetno.skladiste=roba_prodaja.id_skladiste),0)
			        -- +
			        -- COALESCE((SELECT _inventura.kolicina FROM _inventura WHERE _inventura.sifra=roba_prodaja.sifra AND _inventura.skladiste=roba_prodaja.id_skladiste),0)
			        +
			        COALESCE((SELECT _kalkulacije.kolicina FROM _kalkulacije WHERE _kalkulacije.sifra=roba_prodaja.sifra AND _kalkulacije.skladiste=roba_prodaja.id_skladiste),0)
			        +
			        COALESCE((SELECT _radni_nalog_stavke.kolicina FROM _radni_nalog_stavke WHERE _radni_nalog_stavke.sifra=roba_prodaja.sifra AND _radni_nalog_stavke.skladiste=roba_prodaja.id_skladiste),0)
			        +
			        COALESCE((SELECT _primke.kolicina FROM _primke WHERE _primke.sifra=roba_prodaja.sifra AND _primke.skladiste=roba_prodaja.id_skladiste),0)
			        +
			        COALESCE((SELECT _meduskladisnica_u_skladiste.kolicina FROM _meduskladisnica_u_skladiste WHERE _meduskladisnica_u_skladiste.sifra=roba_prodaja.sifra AND _meduskladisnica_u_skladiste.skladiste=roba_prodaja.id_skladiste),0)
			        -
			        COALESCE((SELECT _racuni.kolicina FROM _racuni WHERE _racuni.sifra=roba_prodaja.sifra AND _racuni.skladiste=roba_prodaja.id_skladiste),0)
			        -
			        COALESCE((SELECT _izdatnice.kolicina FROM _izdatnice WHERE _izdatnice.sifra=roba_prodaja.sifra AND _izdatnice.skladiste=roba_prodaja.id_skladiste),0)
			        -
			        COALESCE((SELECT _fakture.kolicina FROM _fakture WHERE _fakture.sifra=roba_prodaja.sifra AND _fakture.skladiste=roba_prodaja.id_skladiste),0)
			        -
			        COALESCE((SELECT _meduskladisnica_iz_skladista.kolicina FROM _meduskladisnica_iz_skladista WHERE _meduskladisnica_iz_skladista.sifra=roba_prodaja.sifra AND _meduskladisnica_iz_skladista.skladiste=roba_prodaja.id_skladiste),0)
			        -
			        COALESCE((SELECT _otpremnica.kolicina FROM _otpremnica WHERE _otpremnica.sifra=roba_prodaja.sifra AND _otpremnica.skladiste=roba_prodaja.id_skladiste),0)
			        -
			        COALESCE((SELECT _otpis_robe.kolicina FROM _otpis_robe WHERE _otpis_robe.sifra=roba_prodaja.sifra AND _otpis_robe.skladiste=roba_prodaja.id_skladiste),0)
			        -
			        COALESCE((SELECT _povrat_robe.kolicina FROM _povrat_robe WHERE _povrat_robe.sifra=roba_prodaja.sifra AND _povrat_robe.skladiste=roba_prodaja.id_skladiste),0)
			        -
			        COALESCE((SELECT _radni_nalog_normativi.kolicina FROM _radni_nalog_normativi WHERE _radni_nalog_normativi.sifra=roba_prodaja.sifra AND _radni_nalog_normativi.skladiste=roba_prodaja.id_skladiste),0)
			        -
			        COALESCE((SELECT _normativi_usluga.kolicina FROM _normativi_usluga WHERE _normativi_usluga.sifra=roba_prodaja.sifra AND _normativi_usluga.skladiste=roba_prodaja.id_skladiste),0)
		        ,0)
		        AS character varying),'.',',')
	        ) WHERE sifra IN (skup_sifra);
        UPDATE roba_prodaja SET kolicina='0' WHERE sifra IN (SELECT sifra FROM roba WHERE oduzmi='NE');

        DROP TABLE IF EXISTS _racuni;
        DROP TABLE IF EXISTS _primke;
	        DROP TABLE IF EXISTS _kalkulacije;
	        DROP TABLE IF EXISTS _izdatnice;
	        -- DROP TABLE IF EXISTS _inventura;
	        DROP TABLE IF EXISTS _fakture;
	        DROP TABLE IF EXISTS _meduskladisnica_iz_skladista;
	        DROP TABLE IF EXISTS _meduskladisnica_u_skladiste;
	        DROP TABLE IF EXISTS _otpremnica;
	        DROP TABLE IF EXISTS _otpis_robe;
	        DROP TABLE IF EXISTS _povrat_robe;
	        DROP TABLE IF EXISTS _pocetno;
	        DROP TABLE IF EXISTS _radni_nalog_stavke;
	        DROP TABLE IF EXISTS _radni_nalog_normativi;
	        DROP TABLE IF EXISTS _normativi_usluga;

        END;
        $BODY$
          LANGUAGE plpgsql VOLATILE
          COST 100;
        ALTER FUNCTION public.postavi_kolicinu_sql_funkcija_prema_sifri(character varying)
          OWNER TO postgres;";
            classSQL.update(sql);
        }

        private static void AlterInventure(DataTable DTremote)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'inventura' and column_name='pocetno_stanje'");
            if (dataROW.Length == 0)
            {
                string sql = "ALTER TABLE inventura ADD COLUMN pocetno_stanje integer DEFAULT 0;";

                classSQL.update(sql);
            }

            dataROW = DTremote.Select("table_name = 'inventura_stavke' and column_name='proizvodacka_cijena'");
            if (dataROW.Length == 0)
            {
                string sql = @"alter table primka_stavke add column proizvodacka_cijena numeric (15,6) default 0;
alter table inventura_stavke add column proizvodacka_cijena numeric(15, 6) default 0;";
                classSQL.update(sql);

                sql = @"update primka_stavke a
set proizvodacka_cijena = b.vpc
from (
	select c.id_stavka, (c.nbc / (1 + (35::numeric / 100::numeric))) as vpc
	from primka_stavke c
) b
where b.id_stavka = a.id_stavka;

update inventura_stavke a
set proizvodacka_cijena = b.vpc
from (
	select c.id_stavke, (nbc / (1 + (35::numeric / 100::numeric))) as vpc
	from inventura_stavke c
) b
where b.id_stavke = a.id_stavke;

update roba a
set proizvodacka_cijena = b.vpc
from (
	select c.id_roba, (c.nc::numeric / (1 + (35::numeric / 100::numeric))) as vpc
	from roba c
) b
where b.id_roba = a.id_roba;

update roba_prodaja a
set proizvodacka_cijena = b.vpc
from (
	select c.id_roba_prodaja, (c.nc::numeric / (1 + (35::numeric / 100::numeric))) as vpc
	from roba_prodaja c
) b
where b.id_roba_prodaja = a.id_roba_prodaja;";
                classSQL.update(sql);
            }

            dataROW = DTremote.Select("table_name = 'faktura_stavke' and column_name='proizvodacka_cijena'");
            if (dataROW.Length == 0)
            {
                string sql = @"alter table faktura_stavke add column proizvodacka_cijena numeric (15,6) default 0;";
                classSQL.update(sql);

                sql = @"update faktura_stavke a
set proizvodacka_cijena = b.vpc
from (
	select c.id_stavka, (c.nbc::numeric / (1 + (35::numeric / 100::numeric))) as vpc
	from faktura_stavke c
) b
where b.id_stavka = a.id_stavka;";

                classSQL.update(sql);
            }
        }

        private static void AlterUlaznaFaktura(DataTable DTremote)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'ulazna_faktura'");
            if (dataROW.Length == 0)
            {
                string sql = "CREATE TABLE ulazna_faktura" +
                     "( " +
                     " broj bigint NOT NULL," +
                     " godina int NOT NULL," +
                     " id serial NOT NULL," +
                     " primatelj_naziv character varying(200)," +
                     " primatelj_adresa character varying(200)," +
                     " primatelj_sjediste character varying(200)," +
                     " primatelj_sifra_zemlje character varying(10)," +
                     " primatelj_swift character varying(200)," +
                     " primatelj_naziv_banke character varying(200)," +
                     " primatelj_adresa_banke character varying(200)," +
                     " primatelj_sjediste_banke character varying(200)," +
                     " primatelj_sifra_zemlje_banke character varying(200)," +
                     " primatelj_troskovna_opcija int," +
                     " primatelj_vrste_strane_osobe int," +
                     " valuta_pokrica character varying(20)," +
                     " valuta character varying(20)," +
                     " iznos numeric," +
                     " iban_platitelja character varying(80)," +
                     " model_platitelja character varying(10)," +
                     " poziv_na_broj_platitelja character varying(80)," +
                     " iban_primatelja character varying(80)," +
                     " model_primatelja character varying(10)," +
                     " poziv_na_broj_primatelja character varying(80)," +
                     " sifra_namjene character varying(10)," +
                     " opis_placanja text," +
                     " datum_izvrsenja timestamp without time zone," +
                     " id_zaposlenik integer," +
                     " oznaka_hitnosti integer," +
                     " hub_kreirano int DEFAULT 0," +
                     " CONSTRAINT ulazna_faktura_pkey PRIMARY KEY (id)" +
                     " )";

                classSQL.select(sql, "ifa");
            }

            DataRow[] a = DTremote.Select("table_name = 'ulazna_faktura' and column_name = 'primatelj_sifra'");

            if (a.Length == 0)
            {
                string sql = "ALTER TABLE ulazna_faktura ADD COLUMN primatelj_sifra INT DEFAULT 0;";
                classSQL.insert(sql);
            }
        }

        private static void AlterPromjenaCijene(DataTable cols)
        {
            DataRow[] a = cols.Select("table_name = 'promjena_cijene_stavke' and column_name = 'kolicina'");

            if (a.Length == 0)
            {
                string sql = "ALTER TABLE promjena_cijene_stavke ADD COLUMN kolicina character varying(10);";
                classSQL.insert(sql);
            }
        }

        private static void DodajPocetno(DataTable cols)
        {
            DataRow[] dataROW = cols.Select("table_name = 'pocetno'");
            if (dataROW.Length == 0)
            {
                string sql = "CREATE TABLE pocetno" +
                    "(" +
                        "id serial NOT NULL," +
                        "sifra character varying," +
                        "id_skladiste integer," +
                        "kolicina character varying(10)," +
                        "datum timestamp without time zone," +
                        "CONSTRAINT pocetno_pkey PRIMARY KEY (id)" +
                    ");";
                classSQL.select(sql, "avansi");
            }

            DataRow[] a = cols.Select("table_name = 'pocetno' and column_name = 'nbc'");
            if (a.Length == 0)
            {
                string sql = "ALTER TABLE pocetno ADD COLUMN nbc numeric DEFAULT 0;";
                sql += "ALTER TABLE pocetno ADD COLUMN porez int DEFAULT 0;";
                sql += "ALTER TABLE pocetno ADD COLUMN povratna_naknada numeric DEFAULT 0;";
                classSQL.insert(sql);
            }

            a = cols.Select("table_name = 'inventura_stavke' and column_name = 'povratna_naknada'");
            if (a.Length == 0)
            {
                string sql = "ALTER TABLE inventura_stavke ADD COLUMN povratna_naknada numeric DEFAULT 0;";
                classSQL.insert(sql);
            }

            a = cols.Select("table_name = 'pocetno' and column_name = 'prodajna_cijena'");
            if (a.Length == 0)
            {
                string sql = "ALTER TABLE pocetno ADD COLUMN prodajna_cijena numeric DEFAULT 0;";
                classSQL.insert(sql);
            }

            a = cols.Select("table_name = 'pocetno' and column_name = 'datum_postave'");
            if (a.Length == 0)
            {
                string sql = "ALTER TABLE pocetno ADD COLUMN datum_postave timestamp without time zone;";
                classSQL.insert(sql);
            }
        }

        private static void IspraviPrviPrvi(DataTable DTremote_greske)
        {
            DataRow[] dataROW = DTremote_greske.Select("opis = 'ispravi_prvi_prvi'");
            if (dataROW.Length != 0)
                return;

            PCPOS.Util.classFukcijeZaUpravljanjeBazom baza = new Util.classFukcijeZaUpravljanjeBazom("MALOPRODAJA", "POS");
            if (!baza.PostojiProslaGodina() || baza.UzmiGodinuKojaSeKoristi() != 2014)
                return;

            baza.ispraviPrviPrvi();
            classSQL.insert("INSERT INTO ispravljene_greske (opis) VALUES ('ispravi_prvi_prvi');");
        }

        private static void AlterSaldaKonti(DataTable dt, DataTable cols)
        {
            DataRow[] dataROW = dt.Select("table_name = 'salda_konti'");
            string sql;
            if (dataROW.Length == 0)
            {
                sql = "CREATE TABLE salda_konti ( " +
                      " id serial, " +
                      " broj_dokumenta bigint NOT NULL, " +
                      " dokumenat character varying(30), " +
                      " id_skladiste integer, " +
                      " datum date, " +
                      " godina int, " +
                      " id_izradio integer, " +
                      " uplaceno numeric, " +
                      " isplaceno numeric, " +
                      " napomena text, " +
                      " CONSTRAINT salda_konti_pkey PRIMARY KEY (id))";
                classSQL.insert(sql);
            }

            dataROW = cols.Select("table_name = 'salda_konti' AND column_name='broj'");

            if (dataROW.Length > 0)
            {
                sql = "ALTER TABLE salda_konti RENAME COLUMN broj TO id;";
                classSQL.insert(sql);
            }

            dataROW = cols.Select("table_name = 'salda_konti' AND column_name='id_partner'");

            if (dataROW.Length == 0)
            {
                sql = "ALTER TABLE salda_konti ADD COLUMN id_partner bigint;";
                classSQL.insert(sql);
            }
        }

        private static void DodajAvanse(DataTable DTremote)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'avansi'");
            if (dataROW.Length == 0)
            {
                string sql = "CREATE TABLE avansi" +
                                        "(broj_avansa bigint NOT NULL," +
                                        "dat_dok timestamp without time zone," +
                                        "dat_knj timestamp without time zone," +
                                        "id_zaposlenik integer," +
                                        "id_zaposlenik_izradio integer," +
                                        "model character varying(10)," +
                                        "id_nacin_placanja bigint," +
                                        "id_valuta integer," +
                                        "opis text," +
                                        "id_vd character(5)," +
                                        "godina_avansa character(6)," +
                                        "ukupno numeric," +
                                        "nult_stp numeric," +
                                        "neoporezivo numeric," +
                                        "osnovica10 numeric," +
                                        "osnovica_var numeric," +
                                        "porez_var numeric," +
                                        "id_pdv integer," +
                                        "id_partner bigint," +
                                        "ziro bigint," +
                                        "jir character varying(100)," +
                                        "zki character varying(100)," +
                                        "storno character varying(2)," +
                                        "CONSTRAINT broj_avansa PRIMARY KEY (broj_avansa)" +
                                        ")";
                classSQL.select(sql, "avansi");
            }

            dataROW = DTremote.Select("table_name = 'avansi_vd'");
            if (dataROW.Length == 0)
            {
                string sql = "CREATE TABLE avansi_vd" +
                    "(id_vd serial NOT NULL," +
                    "vd character varying(30)," +
                    "grupa character varying(5)," +
                    "CONSTRAINT primary_key_id_vd PRIMARY KEY (id_vd )" +
                    ")";
                classSQL.select(sql, "avansi_vd");
                classSQL.insert("INSERT INTO avansi_vd (vd,grupa) VALUES ('Predujam','IP')");
                classSQL.insert("INSERT INTO avansi_vd (vd,grupa) VALUES ('Storno primljenog predujma','PRS')");
            }
        }

        private static void AktivnostZaposlenika(DataTable DTremote)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'povijest_koristenja_dokumenata'");
            string sql;
            if (dataROW.Length == 0)
            {
                sql = "CREATE TABLE povijest_koristenja_dokumenata" +
                    "(" +
                      "id serial NOT NULL," +
                      "dokument character varying(100)," +
                      "id_izradio integer," +
                      "datum timestamp without time zone," +
                      "stavke character varying," +
                      "editirano integer," +
                      "broj_dokumenta integer," +
                      "id_skladiste bigint," +
                      "CONSTRAINT povijest_koristenja_dokumenata_pkey PRIMARY KEY (id )" +
                    ")" +
                    "WITH (" +
                    "  OIDS=FALSE" +
                    ");" +
                    "ALTER TABLE povijest_koristenja_dokumenata" +
                    "  OWNER TO postgres;";

                classSQL.insert(sql);
            }
        }

        private static void KnjiznoOdobrenje(DataTable DTremote)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'knjizno_odobrenje'");
            string sql;
            if (dataROW.Length == 0)
            {
                sql = "CREATE TABLE knjizno_odobrenje" +
                    "(" +
                      "id_knjizno_odobrenje serial NOT NULL," +
                      "broj_odobrenje integer," +
                      "datum timestamp without time zone," +
                      "broj_fakture integer," +
                      "id_ducan integer," +
                      "id_kasa integer," +
                      "id_ducan_faktura integer," +
                      "id_kasa_faktura integer," +
                      "id_izradio integer," +
                      "porez_odobrenja numeric," +
                      "CONSTRAINT knjizno_odobrenje_pkey PRIMARY KEY (id_knjizno_odobrenje )" +
                    ")" +
                    "WITH (" +
                    "  OIDS=FALSE" +
                    ");" +
                    "ALTER TABLE knjizno_odobrenje" +
                    "  OWNER TO postgres;";

                classSQL.insert(sql);
            }
        }

        private static void PartnerPoslovnice(DataTable DTremote)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'partner_poslovnice'");
            string sql;
            if (dataROW.Length == 0)
            {
                sql = "CREATE TABLE partner_poslovnice " +
                    "(" +
                      "id_partner_poslovnica serial NOT NULL, " +
                      "id_partner integer, " +
                      "naziv character varying(100), " +
                      "adresa character varying(100), " +
                      "id_drzava integer, " +
                      "id_grad integer, " +
                      "oib character varying(100), " +
                      "CONSTRAINT partner_poslovnice_pkey PRIMARY KEY (id_partner_poslovnica ) " +
                    ")" +
                    "WITH (" +
                    " OIDS=FALSE " +
                    "); " +
                    "ALTER TABLE partner_poslovnice " +
                    "OWNER TO postgres;";

                classSQL.insert(sql);
            }
        }

        private static void Podaci_tvrtka_compact(DataTable DTcompact)
        {
            DataRow[] r = DTcompact.Select("table_name='podaci_tvrtka' and column_name='ime_poslovnice'");

            int i = r.Length;

            if (r[0][3].ToString() != "150")
                classSQL.Setings_Update("ALTER TABLE podaci_tvrtka ALTER COLUMN ime_poslovnice nvarchar(150)");

            r = DTcompact.Select("table_name='podaci_tvrtka' and column_name='dodatniPodaciHeader'");
            if (r.Length == 0)
            {
                classSQL.Setings_Update("ALTER TABLE podaci_tvrtka ADD COLUMN dodatniPodaciHeader ntext;");
                //classSQL.insert("ALTER TABLE podaci_tvrtka ADD COLUMN dodatniPodaciHeader text;");
            }

            if (DTcompact.Select("table_name = 'podaci_tvrtka' and column_name = 'servis_text'").Length == 0)
            {
                classSQL.Setings_Update("ALTER TABLE podaci_tvrtka ADD COLUMN servis_text ntext");
            }

            if (DTcompact.Select("table_name = 'podaci_tvrtka' and column_name = 'sifra_djelatnosti'").Length == 0)
            {
                classSQL.Setings_Update("alter table podaci_tvrtka add column sifra_djelatnosti nvarchar(150) default '';");
                classSQL.Setings_Update("alter table podaci_tvrtka add column adresa_prebivalista nvarchar(500) default '';");
            }
        }

        private static void FixZaposlenici(DataTable DTremote_greske)
        {
            DataRow[] dataROW = DTremote_greske.Select("opis = 'blag_kom'");

            if (dataROW.Length == 0)
            {
                string sql = "UPDATE dopustenja SET naziv='Komercijalist' WHERE id_dopustenje='2'; " +
                    "UPDATE dopustenja SET naziv='Blagajnik' WHERE id_dopustenje='3'; ";

                classSQL.update(sql);
                classSQL.insert("INSERT INTO ispravljene_greske (opis) VALUES ('blag_kom');");
            }
        }

        private static void AlterCreateTablePostavke_sinkronizacije(DataTable DTr)
        {
            DataRow[] r = DTr.Select("table_name='postavke_sinkronizacije'");
            if (r.Length == 0)
            {
                string sql = "CREATE TABLE postavke_sinkronizacije (" +
                    " id serial," +
                    " sifra_partnera_start int," +
                    " skladiste_kalkulacije character varying," +
                    " skladiste_primke character varying," +
                    " ip character varying," +
                    " korisnickoime character varying," +
                    " lozinka character varying," +
                    " aktivirano smallint," +
                    " CONSTRAINT postavke_fiskalizacije_id_key PRIMARY KEY (id )" +
                    ")";
                classSQL.insert(sql);
            }
        }

        private static void FixKalkulacija(DataTable DTremote_greske)
        {
            DataRow[] dataROW = DTremote_greske.Select("opis = 'mpc_vpc_kal_kol'");

            if (dataROW.Length == 0)
            {
                string sql = "UPDATE kalkulacija SET " +
                    "ukupno_vpc=" +
                    "(SELECT SUM(kalkulacija_stavke.vpc*CAST(REPLACE(kalkulacija_stavke.kolicina,',','.') AS numeric)) FROM kalkulacija_stavke " +
                    "WHERE kalkulacija_stavke.broj = kalkulacija.broj AND kalkulacija_stavke.id_skladiste = kalkulacija.id_skladiste)," +
                    "ukupno_mpc=" +
                    "(SELECT SUM(kalkulacija_stavke.vpc*CAST(REPLACE(kalkulacija_stavke.kolicina,',','.') AS numeric)*(1+(CAST(REPLACE(kalkulacija_stavke.porez,',','.') AS numeric))/100)) FROM kalkulacija_stavke " +
                    "WHERE kalkulacija_stavke.broj = kalkulacija.broj AND kalkulacija_stavke.id_skladiste = kalkulacija.id_skladiste);";

                classSQL.update(sql);
                classSQL.insert("INSERT INTO ispravljene_greske (opis) VALUES ('mpc_vpc_kal_kol');");
            }
        }

        private static void DodajProvjeruGreske(DataTable DTremote)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'ispravljene_greske'");

            if (dataROW.Length == 0)
            {
                string sql = "CREATE TABLE ispravljene_greske " +
                            "(" +
                            "id serial NOT NULL," +
                            "opis character varying UNIQUE," +
                            "CONSTRAINT ispravljene_greske_primary_key PRIMARY KEY (id )" +
                            ")" +
                            "";

                classSQL.select(sql, "partners_odrzavanje");
            }

            dataROW = DTremote.Select("table_name = 'partner_kronologija'");

            if (dataROW.Length == 0)
            {
                string sql = "CREATE TABLE partner_kronologija " +
                            "(" +
                            "id serial NOT NULL," +
                            "opis  text," +
                            "sifra character varying," +
                            "naziv character varying," +
                            "datum timestamp without time zone," +
                            "id_zaposlenik integer," +
                            "CONSTRAINT partner_kronologija_primary_key PRIMARY KEY (id )" +
                            ");" +
                            "";

                classSQL.select(sql, "partner_kronologija");
            }
        }

        /// <summary>
        /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!OVO NE BRISATI!!!!!!!!!!!!!!!!!!!!!!!!
        /// </summary>
        /// <param name="DT"></param>
        private static void AlterRacun(DataTable DT)
        {
            DataRow[] a = DT.Select("table_name = 'racun_stavke' and column_name = 'id_ducan'");

            if (a.Length == 0)
            {
                string sql = " ALTER TABLE racuni DROP CONSTRAINT racuni_primary_key;" +
                    " ALTER TABLE racuni ADD COLUMN id serial;" +
                    " ALTER TABLE racuni ADD PRIMARY KEY (id);" +
                    " ALTER TABLE racun_stavke ADD COLUMN id_ducan int;" +
                    " ALTER TABLE racun_stavke ADD COLUMN id_kasa int;" +
                    " UPDATE racun_stavke SET id_ducan=(SELECT id_ducan FROM racuni WHERE racuni.broj_racuna=racun_stavke.broj_racuna),id_kasa=(SELECT id_kasa FROM racuni WHERE racuni.broj_racuna=racun_stavke.broj_racuna);";
                classSQL.insert(sql);
            }

            a = DT.Select("table_name = 'racun_stavke' and column_name = 'prirez'");

            if (a.Length == 0)
            {
                string sql = " ALTER TABLE racun_stavke ADD COLUMN prirez numeric DEFAULT 0;" +
                             " ALTER TABLE racun_stavke ADD COLUMN porez_na_dohodak numeric DEFAULT 0;" +
                             " ALTER TABLE racun_stavke ADD COLUMN prirez_iznos numeric DEFAULT 0;" +
                             " ALTER TABLE racun_stavke ADD COLUMN porez_na_dohodak_iznos numeric DEFAULT 0;";
                classSQL.insert(sql);
            }

            a = DT.Select("table_name = 'racuni' and column_name = 'datum_syn'");
            if (a.Length == 0)
            {
                try
                {
                    string sql = " ALTER TABLE racuni ADD COLUMN novo boolean DEFAULT '1';";
                    classSQL.insert(sql);
                }
                catch { }

                try
                {
                    string sql = "ALTER TABLE racuni ADD COLUMN datum_syn timestamp without time zone DEFAULT null;";
                    classSQL.insert(sql);
                }
                catch { }
            }
        }

        private static void AlterFakture(DataTable DT)
        {
            DataRow[] a = DT.Select("table_name = 'faktura_stavke' and column_name = 'id_ducan'");

            if (a.Length == 0)
            {
                DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
                string id_kasa = "1";
                string id_ducan = "1";

                try
                {
                    id_kasa = DTpostavke.Rows[0]["naplatni_uredaj_faktura"].ToString();
                    id_ducan = DTpostavke.Rows[0]["default_ducan"].ToString();
                }
                catch { }

                string sql = " ALTER TABLE fakture DROP CONSTRAINT fakture_primary_key;" +
                    " ALTER TABLE fakture ADD COLUMN id serial;" +
                    " ALTER TABLE fakture ADD PRIMARY KEY (id);" +
                    " ALTER TABLE fakture ADD COLUMN id_ducan int;" +
                    " ALTER TABLE fakture ADD COLUMN id_kasa int;" +
                    " ALTER TABLE faktura_stavke ADD COLUMN id_ducan int;" +
                    " ALTER TABLE faktura_stavke ADD COLUMN id_kasa int;" +
                    " UPDATE fakture SET id_ducan='" + id_ducan + "',id_kasa='" + id_kasa + "';" +
                    " UPDATE faktura_stavke SET id_ducan='" + id_ducan + "',id_kasa='" + id_kasa + "';";
                classSQL.insert(sql);

                sql = " ALTER TABLE ispis_faktura_stavke ADD COLUMN id_ducan int;" +
                      " ALTER TABLE ispis_faktura_stavke ADD COLUMN id_kasa int;" +
                      " ALTER TABLE ispis_fakture ADD COLUMN id_ducan int;" +
                      " ALTER TABLE ispis_fakture ADD COLUMN id_kasa int;";
                classSQL.insert(sql);
            }

            a = DT.Select("table_name = 'fakture' and column_name = 'avio_registracija'");
            if (a.Length == 0)
            {
                string sql = string.Format(@"alter table fakture add column avio_registracija character varying(50) default null;
alter table fakture add column avio_tip_zrakoplova character varying(50) default null;
alter table fakture add column avio_maks_tezina_polijetanja numeric(15,6) default 0;");
                classSQL.insert(sql);
            }

            a = DT.Select("table_name = 'racuni' and column_name = 'avio_registracija'");
            if (a.Length == 0)
            {
                string sql = string.Format(@"alter table racuni add column avio_registracija character varying(50) default null;
alter table racuni add column avio_tip_zrakoplova character varying(50) default null;
alter table racuni add column avio_maks_tezina_polijetanja numeric(15,6) default 0;");
                classSQL.insert(sql);
            }

            a = DT.Select("table_name = 'salda_konti' and column_name = 'id_ducan'");

            if (a.Length == 0)
            {
                DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
                string id_kasa = "1";
                string id_ducan = "1";

                try
                {
                    id_kasa = DTpostavke.Rows[0]["naplatni_uredaj_faktura"].ToString();
                    id_ducan = DTpostavke.Rows[0]["default_ducan"].ToString();
                }
                catch { }

                string sql = " ALTER TABLE salda_konti ADD COLUMN id_ducan int;" +
                             " ALTER TABLE salda_konti ADD COLUMN id_kasa int;" +
                             " UPDATE salda_konti SET id_ducan='" + id_ducan + "',id_kasa='" + id_kasa + "';";
                classSQL.insert(sql);
            }
        }

        private static void AlterPartnersGeneriranjeFakture(DataTable DTremote)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'partners_odrzavanje'");

            if (dataROW.Length == 0)
            {
                string sql = "CREATE TABLE partners_odrzavanje " +
                            "(" +
                                "id serial NOT NULL," +
                                "id_partner int," +
                                "odrzavanje numeric," +
                                "odrzavanje_kol numeric," +
                                "internet numeric," +
                                "internet_kol numeric," +
                                "CONSTRAINT partners_odrzavanje_primary_key PRIMARY KEY (id )" +
                            ")" +
                            "";

                classSQL.select(sql, "partners_odrzavanje");
            }

            DataRow[] a = DTremote.Select("table_name = 'partners_odrzavanje' and column_name = 'nas_program'");

            if (a.Length == 0)
            {
                string sql = " ALTER TABLE partners_odrzavanje ADD COLUMN nas_program int;" +
                             " ALTER TABLE partners_odrzavanje ADD COLUMN web_ured int;" +
                             " ALTER TABLE partners_odrzavanje ADD COLUMN ugovor int;";
                classSQL.insert(sql);
            }

            a = DTremote.Select("table_name = 'partners_odrzavanje' and column_name = 'bivsi_korisnik'");
            if (a.Length == 0)
            {
                string sql = " ALTER TABLE partners_odrzavanje ADD COLUMN bivsi_korisnik int;" +
                             " ALTER TABLE partners_odrzavanje ADD COLUMN tablet int;" +
                             " ALTER TABLE partners_odrzavanje ADD COLUMN pcpos int;" +
                             " ALTER TABLE partners_odrzavanje ADD COLUMN pccaffe int;" +
                             " ALTER TABLE partners_odrzavanje ADD COLUMN godisnje_odr int;" +
                             " ALTER TABLE partners_odrzavanje ADD COLUMN resort int;";
                classSQL.insert(sql);
            }
        }

        private static void Alterkontni_plan(DataTable DTremote)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'kontni_plan'");
            if (dataROW.Length == 0)
            {
                string sql = "CREATE TABLE kontni_plan" +
                     "( id serial NOT NULL," +
                     " br_konta character varying(50)," +
                     " opis character varying(255)," +
                     " status character varying(5)," +
                     " sinteticki_konto_knjizenje_nije_moguce character varying(15)," +
                     " vrsta_korisnika character varying(5)," +
                     " CONSTRAINT kontni_plan_pkey PRIMARY KEY (id)" +
                     " )";

                classSQL.select(sql, "kontni_plan");
            }
        }

        private static void AlterFaktureVan(DataTable DTremote)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'fakture_van'");

            if (dataROW.Length == 0)
            {
                string sql = "CREATE TABLE fakture_van" +
                            "(" +
                            "broj_fakture bigint NOT NULL," +
                            "id_odrediste bigint," +
                            "id_fakturirati bigint," +
                            "date timestamp without time zone," +
                            "datedvo timestamp without time zone," +
                            "datum_valute timestamp without time zone," +
                            "id_izjava integer," +
                            "id_zaposlenik integer," +
                            "id_zaposlenik_izradio integer," +
                            "model character varying(10)," +
                            "id_nacin_placanja bigint," +
                            "zr integer," +
                            "id_valuta integer," +
                            "otprema integer," +
                            "id_predujam bigint," +
                            "napomena text," +
                            "ukupno money," +
                            "id_vd character(5)," +
                            "godina_predujma character(6)," +
                            "godina_ponude character(6)," +
                            "godina_fakture character varying(6)," +
                            "mj_troska character varying(100)," +
                            "oduzmi_iz_skladista character varying(1)," +
                            "jir character varying(100)," +
                            "zki character varying(100)," +
                            "storno character varying(2)," +
                            "ukupno_rabat numeric," +
                            "ukupno_porez numeric," +
                            "ukupno_osnovica numeric," +
                            "ukupno_mpc numeric," +
                            "ukupno_vpc numeric," +
                            "ukupno_mpc_rabat numeric," +
                            "ukupno_povratna_naknada numeric," +
                            "tecaj numeric, " +
                            "CONSTRAINT fakture_van_primary_key PRIMARY KEY (broj_fakture ))";

                classSQL.select(sql, "fakture_van");
            }
        }

        private static void AlterFaktureVan_stavke(DataTable DTremote)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'faktura_van_stavke'");

            if (dataROW.Length == 0)
            {
                string sql = "CREATE TABLE faktura_van_stavke" +
                            "(" +
                            "id_stavka serial NOT NULL," +
                            "kolicina character varying(8)," +
                            "vpc numeric," +
                            "porez character varying(10)," +
                            "broj_fakture bigint," +
                            "rabat character varying(10)," +
                            "id_skladiste bigint," +
                            "sifra character varying(30)," +
                            "oduzmi character varying(2)," +
                            "odjava character varying(1)," +
                            "nbc money," +
                            "porez_potrosnja character varying(5)," +
                            "povratna_naknada numeric," +
                            "rabat_izn numeric," +
                            "mpc_rabat numeric," +
                            "ukupno_rabat numeric," +
                            "ukupno_vpc numeric," +
                            "ukupno_mpc numeric," +
                            "ukupno_mpc_rabat numeric," +
                            "povratna_naknada_izn numeric," +
                            "ukupno_porez numeric," +
                            "ukupno_osnovica numeric," +
                            "CONSTRAINT faktura_van_stavke_primary_key PRIMARY KEY (id_stavka )" +
                            ")" +
                            "";

                classSQL.select(sql, "fakture_van_stavke");
            }
        }

        private static void Alterotpremnice_stavke(DataTable cols)
        {
            DataRow[] a = cols.Select("table_name = 'otpremnica_stavke' and column_name = 'id_otpremnice'");

            if (a.Length == 0)
            {
                string sql = "ALTER TABLE otpremnica_stavke ADD COLUMN id_otpremnice bigint; " +
                             "ALTER TABLE otpremnica_stavke ADD COLUMN naplaceno_fakturom boolean DEFAULT false; ";
                classSQL.insert(sql);
            }
        }

        private static void faktura1alter_stavke(DataTable dt)
        {
            DataRow[] a = dt.Select("table_name = 'fakture' and column_name = 'broj_avansa'");

            if (a.Length == 0)
            {
                string sql = "ALTER TABLE fakture ADD COLUMN broj_avansa numeric;";
                classSQL.insert(sql);
            }

            a = dt.Select("table_name = 'fakture' and column_name = 'godina_avansa'");

            if (a.Length == 0)
            {
                string sql = "ALTER TABLE fakture ADD COLUMN godina_avansa numeric;";
                classSQL.insert(sql);
            }
        }

        private static void Alterkalkulacija_stavke(DataTable cols)
        {
            DataRow[] a = cols.Select("table_name = 'kalkulacija_stavke' and column_name = 'id_kalkulacija'");
            if (a.Length == 0)
            {
                string sql = "ALTER TABLE kalkulacija_stavke ADD COLUMN id_kalkulacija bigint; ";
                classSQL.insert(sql);

                DataTable kalksredi = classSQL.select("Select id_skladiste From skladiste", "sredi").Tables[0];

                for (int i = 0; i < kalksredi.Rows.Count; i++)
                {
                    DataTable id_kal = classSQL.select("Select id_kalkulacija, broj from kalkulacija Where id_skladiste = '" + kalksredi.Rows[i]["id_skladiste"].ToString() + "' ", "id").Tables[0];
                    for (int z = 0; z < id_kal.Rows.Count; z++)
                    {
                        string broj = id_kal.Rows[z]["broj"].ToString();
                        string id = id_kal.Rows[z]["id_kalkulacija"].ToString();
                        string sql1 = "Update kalkulacija_stavke Set id_kalkulacija = '" + id + "' Where broj = '" + broj + "' AND id_skladiste = '" + kalksredi.Rows[i]["id_skladiste"].ToString() + "'";
                        classSQL.update(sql1);
                    }
                }
            }
        }

        private static void Alterfaktura_stavke(DataTable DTremote)
        {
            try
            {
                DataTable DTcount = classSQL.select("SELECT * FROM faktura_stavke WHERE ukupno_rabat IS NULL LIMIT 1", "racun_stavke").Tables[0];

                if (DTcount.Rows.Count > 0)
                {
                    DataTable DTtempstv = classSQL.select("SELECT * FROM faktura_stavke", "temp stavke").Tables[0];
                    if (DTtempstv.Rows.Count > 0)
                    {
                        for (int x = 0; x < DTtempstv.Rows.Count; x++)
                        {
                            string broj = DTtempstv.Rows[x]["id_stavka"].ToString();
                            decimal rab = Convert.ToDecimal(DTtempstv.Rows[x]["rabat"].ToString());
                            decimal vpc = Convert.ToDecimal(DTtempstv.Rows[x]["vpc"].ToString());
                            decimal pdv = Convert.ToDecimal(DTtempstv.Rows[x]["porez"].ToString());
                            decimal kol = Convert.ToDecimal(DTtempstv.Rows[x]["kolicina"].ToString());

                            decimal mpc = Math.Round((vpc * (1 + (pdv / 100))), 3);
                            decimal iznos_rabat = Math.Round((mpc * (rab / 100)), 3);
                            decimal mpc_rabat = Math.Round(mpc - iznos_rabat, 3);
                            decimal ukupno_rabat = Math.Round(iznos_rabat * kol, 3);
                            decimal ukupno_vpc = Math.Round(vpc * kol, 3);
                            decimal ukupno_mpc = Math.Round(mpc * kol, 3);
                            decimal ukupno_mpc_rabat = Math.Round(mpc_rabat * kol, 3);
                            decimal povratna_naknada_izn = 0;
                            decimal ukupno_osnovica = Math.Round(ukupno_mpc_rabat / (1 + pdv / 100), 3);
                            decimal ukupno_porez = Math.Round(ukupno_mpc_rabat - ukupno_osnovica, 3);

                            string sql = "UPDATE faktura_stavke SET povratna_naknada = '0', rabat_izn = '" + iznos_rabat.ToString().Replace(",", ".") + "', mpc_rabat = '" + mpc_rabat.ToString().Replace(",", ".") + "', " +
                            "ukupno_rabat = '" + ukupno_rabat.ToString().Replace(",", ".") + "', ukupno_vpc = '" + ukupno_vpc.ToString().Replace(",", ".") + "', ukupno_mpc = '" + ukupno_mpc.ToString().Replace(",", ".") + "',  " +
                            "ukupno_mpc_rabat = '" + ukupno_mpc_rabat.ToString().Replace(",", ".") + "', povratna_naknada_izn = '" + povratna_naknada_izn + "', " +
                            "ukupno_porez = '" + ukupno_porez.ToString().Replace(",", ".") + "', ukupno_osnovica = '" + ukupno_osnovica.ToString().Replace(",", ".") + "'WHERE id_stavka = '" + broj + "'";
                            classSQL.update(sql);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        private static void Alterfaktura_racuni(DataTable DTremote)
        {
            try
            {
                DataTable DTcount = classSQL.select("SELECT * FROM fakture WHERE ukupno_rabat IS NULL LIMIT 10", "racun_stavke").Tables[0];

                if (DTcount.Rows.Count > 0)
                {
                    DataTable DTtemp = new DataTable();

                    string temp = "SELECT * FROM fakture";
                    DTtemp = classSQL.select(temp, "temp").Tables[0];
                    if (DTtemp.Rows.Count > 0)
                    {
                        for (int i = 0; i < DTtemp.Rows.Count; i++)
                        {
                            string broj = DTtemp.Rows[i]["broj_fakture"].ToString();

                            decimal uk_ra = 0;
                            decimal uk_pdv = 0;
                            decimal uk_osn = 0;
                            decimal uk_mpc = 0;
                            decimal uk_mpc_ra = 0;
                            decimal uk_vpc = 0;
                            decimal uk_p_nak = 0;

                            DataTable DTtempstv1 = classSQL.select("SELECT * FROM faktura_stavke WHERE broj_fakture = '" + broj + "'", "temp stavke").Tables[0];
                            for (int y = 0; y < DTtempstv1.Rows.Count; y++)
                            {
                                decimal ukupno_rabat = Convert.ToDecimal(DTtempstv1.Rows[y]["ukupno_rabat"].ToString());
                                decimal ukupno_porez = Convert.ToDecimal(DTtempstv1.Rows[y]["ukupno_porez"].ToString());
                                decimal ukupno_osnovica = Convert.ToDecimal(DTtempstv1.Rows[y]["ukupno_osnovica"].ToString());
                                decimal ukupno_mpc = Convert.ToDecimal(DTtempstv1.Rows[y]["ukupno_mpc"].ToString());
                                decimal ukupno_vpc = Convert.ToDecimal(DTtempstv1.Rows[y]["ukupno_vpc"].ToString());
                                decimal ukupno_mpc_rabat = Convert.ToDecimal(DTtempstv1.Rows[y]["ukupno_mpc_rabat"].ToString());
                                decimal ukupno_povratna_naknada = Convert.ToDecimal(DTtempstv1.Rows[y]["povratna_naknada_izn"].ToString());
                                uk_ra += ukupno_rabat;
                                uk_pdv += ukupno_porez;
                                uk_osn += ukupno_osnovica;
                                uk_mpc += ukupno_mpc;
                                uk_mpc_ra += ukupno_mpc_rabat;
                                uk_vpc += ukupno_vpc;
                                uk_p_nak += ukupno_povratna_naknada;
                            }

                            string sql = "UPDATE fakture SET ukupno_rabat = '" + uk_ra.ToString().Replace(",", ".") + "', ukupno_porez = '" + uk_pdv.ToString().Replace(",", ".") + "', ukupno_osnovica = '" + uk_osn.ToString().Replace(",", ".") + "', " +
                            "ukupno_mpc = '" + uk_mpc.ToString().Replace(",", ".") + "', ukupno_vpc = '" + uk_vpc.ToString().Replace(",", ".") + "', ukupno_mpc_rabat = '" + uk_mpc_ra.ToString().Replace(",", ".") + "', ukupno_povratna_naknada = '" + uk_p_nak.ToString().Replace(",", ".") + "' WHERE broj_fakture = '" + broj + "'";

                            classSQL.update(sql);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        private static void Alterracuni_stavke(DataTable cols)
        {
            try
            {
                DataRow[] ar = cols.Select("table_name = 'kalkulacija_stavke' and column_name = 'prijevoz'");

                if (ar.Length > 0 && ar[0]["data_type"].ToString() == "money")
                {
                    string sql = @"DROP VIEW if exists ulaz_izlaz_robe_financijski;
                                    ALTER TABLE kalkulacija_stavke ALTER COLUMN prijevoz TYPE numeric USING prijevoz::numeric;";
                    classSQL.insert(sql);
                    ProvjeraViewDB(cols);
                }

                ar = cols.Select("table_name = 'kalkulacija_stavke' and column_name = 'fak_cijena'");
                if (ar.Length > 0 && ar[0]["data_type"].ToString() == "money")
                {
                    string sql = @"DROP VIEW if exists ulaz_izlaz_robe_financijski;
                                   ALTER TABLE kalkulacija_stavke ALTER COLUMN fak_cijena TYPE numeric USING fak_cijena::numeric;";
                    classSQL.insert(sql);
                    ProvjeraViewDB(cols);
                }

                DataTable DTcount = classSQL.select("SELECT * FROM racun_stavke WHERE ukupno_rabat IS NULL LIMIT 10", "racun_stavke").Tables[0];

                if (DTcount.Rows.Count > 0)
                {
                    DataTable DTtempstv = classSQL.select("SELECT * FROM racun_stavke", "temp stavke").Tables[0];
                    if (DTtempstv.Rows.Count > 0)
                    {
                        for (int x = 0; x < DTtempstv.Rows.Count; x++)
                        {
                            string broj = DTtempstv.Rows[x]["id_stavka"].ToString();
                            decimal rab = Convert.ToDecimal(DTtempstv.Rows[x]["rabat"].ToString());
                            decimal vpc = Convert.ToDecimal(DTtempstv.Rows[x]["vpc"].ToString());
                            decimal pdv = Convert.ToDecimal(DTtempstv.Rows[x]["porez"].ToString());
                            decimal kol = Convert.ToDecimal(DTtempstv.Rows[x]["kolicina"].ToString());

                            decimal mpc = Math.Round((vpc * (1 + (pdv / 100))), 3);
                            decimal iznos_rabat = Math.Round((mpc * (rab / 100)), 3);
                            decimal mpc_rabat = Math.Round(mpc - iznos_rabat, 3);
                            decimal ukupno_rabat = Math.Round(iznos_rabat * kol, 3);
                            decimal ukupno_vpc = Math.Round(vpc * kol, 3);
                            decimal ukupno_mpc = Math.Round(mpc * kol, 3);
                            decimal ukupno_mpc_rabat = Math.Round(mpc_rabat * kol, 3);
                            decimal povratna_naknada_izn = 0;
                            decimal ukupno_osnovica = Math.Round(ukupno_mpc_rabat / (1 + pdv / 100), 3);
                            decimal ukupno_porez = Math.Round(ukupno_mpc_rabat - ukupno_osnovica, 3);

                            string sql = "UPDATE racun_stavke SET povratna_naknada = '0', rabat_izn = '" + iznos_rabat.ToString().Replace(",", ".") + "', mpc_rabat = '" + mpc_rabat.ToString().Replace(",", ".") + "', " +
                            "ukupno_rabat = '" + ukupno_rabat.ToString().Replace(",", ".") + "', ukupno_vpc = '" + ukupno_vpc.ToString().Replace(",", ".") + "', ukupno_mpc = '" + ukupno_mpc.ToString().Replace(",", ".") + "',  " +
                            "ukupno_mpc_rabat = '" + ukupno_mpc_rabat.ToString().Replace(",", ".") + "', povratna_naknada_izn = '" + povratna_naknada_izn + "', " +
                            "ukupno_porez = '" + ukupno_porez.ToString().Replace(",", ".") + "', ukupno_osnovica = '" + ukupno_osnovica.ToString().Replace(",", ".") + "'WHERE id_stavka = '" + broj + "'";
                            classSQL.update(sql);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            // Dodao 25.10.2013
            DataRow[] a = cols.Select("table_name = 'racun_stavke' and column_name = 'kolicina'");

            if (a[0][3].ToString() != "")
            {
                classSQL.insert("ALTER TABLE racun_stavke ALTER COLUMN kolicina type character varying");
            }

            //Kraj
        }

        private static void Alterracuni_racuni(DataTable DTremote)
        {
            try
            {
                DataTable DTcount = classSQL.select("SELECT * FROM racuni WHERE ukupno_rabat IS NULL LIMIT 10", "racun_stavke").Tables[0];

                if (DTcount.Rows.Count > 0)
                {
                    DataTable DTtemp = new DataTable();

                    string temp = "SELECT * FROM racuni";
                    DTtemp = classSQL.select(temp, "temp").Tables[0];
                    if (DTtemp.Rows.Count > 0)
                    {
                        for (int i = 0; i < DTtemp.Rows.Count; i++)
                        {
                            string broj = DTtemp.Rows[i]["broj_racuna"].ToString();

                            decimal uk_ra = 0;
                            decimal uk_pdv = 0;
                            decimal uk_osn = 0;
                            decimal uk_mpc = 0;
                            decimal uk_mpc_ra = 0;
                            decimal uk_vpc = 0;
                            decimal uk_p_nak = 0;

                            DataTable DTtempstv1 = classSQL.select("SELECT * FROM racun_stavke WHERE broj_racuna = '" + broj + "'", "temp stavke").Tables[0];
                            for (int y = 0; y < DTtempstv1.Rows.Count; y++)
                            {
                                decimal ukupno_rabat = Convert.ToDecimal(DTtempstv1.Rows[y]["ukupno_rabat"].ToString());
                                decimal ukupno_porez = Convert.ToDecimal(DTtempstv1.Rows[y]["ukupno_porez"].ToString());
                                decimal ukupno_osnovica = Convert.ToDecimal(DTtempstv1.Rows[y]["ukupno_osnovica"].ToString());
                                decimal ukupno_mpc = Convert.ToDecimal(DTtempstv1.Rows[y]["ukupno_mpc"].ToString());
                                decimal ukupno_vpc = Convert.ToDecimal(DTtempstv1.Rows[y]["ukupno_vpc"].ToString());
                                decimal ukupno_mpc_rabat = Convert.ToDecimal(DTtempstv1.Rows[y]["ukupno_mpc_rabat"].ToString());
                                decimal ukupno_povratna_naknada = Convert.ToDecimal(DTtempstv1.Rows[y]["povratna_naknada_izn"].ToString());
                                uk_ra += ukupno_rabat;
                                uk_pdv += ukupno_porez;
                                uk_osn += ukupno_osnovica;
                                uk_mpc += ukupno_mpc;
                                uk_mpc_ra += ukupno_mpc_rabat;
                                uk_vpc += ukupno_vpc;
                                uk_p_nak += ukupno_povratna_naknada;
                            }

                            string sql = "UPDATE racuni SET ukupno_rabat = '" + uk_ra.ToString().Replace(",", ".") + "', ukupno_porez = '" + uk_pdv.ToString().Replace(",", ".") + "', ukupno_osnovica = '" + uk_osn.ToString().Replace(",", ".") + "', " +
                            "ukupno_mpc = '" + uk_mpc.ToString().Replace(",", ".") + "', ukupno_vpc = '" + uk_vpc.ToString().Replace(",", ".") + "', ukupno_mpc_rabat = '" + uk_mpc_ra.ToString().Replace(",", ".") + "', ukupno_povratna_naknada = '" + uk_p_nak.ToString().Replace(",", ".") + "', ukupno_ostalo = '0' WHERE broj_racuna = '" + broj + "'";

                            classSQL.update(sql);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        private static void Alterotpremnica_stavke(DataTable DTremote)
        {
            try
            {
                DataTable DTcount = classSQL.select("SELECT * FROM otpremnica_stavke WHERE id_otpremnice IS NULL LIMIT 10", "otpremnica_stavke").Tables[0];

                if (DTcount.Rows.Count > 0)
                {
                    DataTable DTtemp = new DataTable();

                    string temp = "SELECT * FROM otpremnice";
                    DTtemp = classSQL.select(temp, "temp").Tables[0];
                    if (DTtemp.Rows.Count > 0)
                    {
                        for (int i = 0; i < DTtemp.Rows.Count; i++)
                        {
                            string broj = DTtemp.Rows[i]["broj_otpremnice"].ToString();
                            string skla = DTtemp.Rows[i]["id_skladiste"].ToString();

                            DataTable DTtempstv1 = classSQL.select("SELECT * FROM otpremnica_stavke WHERE broj_otpremnice = '" + broj + "' AND id_skladiste = '" + skla + "'", "temp stavke").Tables[0];
                            for (int y = 0; y < DTtempstv1.Rows.Count; y++)
                            {
                                decimal id = Convert.ToDecimal(DTtemp.Rows[i]["id_otpremnica"].ToString());
                                decimal id_stv = Convert.ToDecimal(DTtempstv1.Rows[y]["id_stavka"].ToString());

                                string sql = "UPDATE otpremnica_stavke SET id_otpremnice = '" + id + "' WHERE id_stavka = '" + id_stv + "'";
                                classSQL.update(sql);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        private static void AlterFakturaTecaj(DataTable cols)
        {
            DataRow[] a = cols.Select("table_name = 'fakture' and column_name = 'tecaj'");

            if (a.Length == 0)
            {
                string sql = "ALTER TABLE fakture ADD COLUMN tecaj numeric;";
                classSQL.insert(sql);
                classSQL.select_settings(sql, "fakture");
                classSQL.Setings_Update("UPDATE fakture SET tecaj='1'");
                classSQL.update("UPDATE fakture SET tecaj='1'");
            }
        }

        private static void AlterGrupaRoba(DataTable dt)
        {
            DataRow[] a = dt.Select("table_name = 'roba' and column_name = 'id_podgrupa'");

            if (a.Length == 0)
            {
                string sql = "ALTER TABLE roba ADD COLUMN id_podgrupa int;";
                classSQL.insert(sql);
            }

            a = dt.Select("table_name = 'podgrupa' and column_name = 'id_grupa'");

            if (a.Length == 0)
            {
                string sql = "ALTER TABLE podgrupa ADD COLUMN id_grupa int;";
                classSQL.insert(sql);
            }
        }

        private static void AlterPromjenaCijeneKomadno(DataTable dt, DataTable cols)
        {
            DataRow[] dataROW = dt.Select("table_name = 'promjena_cijene_komadno'");

            string sql;

            if (dataROW.Length == 0)
            {
                sql = "CREATE TABLE promjena_cijene_komadno( " +
                      " broj bigint NOT NULL, " +
                      " id_skladiste integer, " +
                      " date date, " +
                      " id_izradio integer, " +
                      " napomena text, " +
                      " CONSTRAINT promjena_cijene_komadno_pkey PRIMARY KEY (broj ))";
                classSQL.select(sql, "promjena_cijene_komadno");
            }

            DataRow[] dataROW1 = dt.Select("table_name = 'promjena_cijene_komadno_stavke'");

            string sql1;

            if (dataROW1.Length == 0)
            {
                sql1 = "CREATE TABLE promjena_cijene_komadno_stavke( " +
                       " id_stavka serial NOT NULL, " +
                       " stara_cijena money, " +
                       " nova_cijena money, " +
                       " pdv character varying(10), " +
                       " sifra character varying(30), " +
                       " postotak character varying(20), " +
                       " kolicina numeric, " +
                       " kolicina_ostatak numeric, " +
                       " skladiste character varying(40), " +
                       " broj bigint, " +
                       " CONSTRAINT promjena_cijene_komadno_stavke_pkey PRIMARY KEY (id_stavka ))";
                classSQL.select(sql1, "promjena_cijene_komadno_stavke");
            }

            DataRow[] a = cols.Select("table_name = 'promjena_cijene_komadno_stavke' and column_name = 'datum'");

            if (a.Length == 0)
            {
                try
                {
                    string sql2 = "ALTER TABLE promjena_cijene_komadno_stavke ADD COLUMN datum timestamp without time zone";
                    classSQL.update(sql2);
                }
                catch { }
            }

            a = cols.Select("table_name = 'promjena_cijene_komadno_stavke' and column_name = 'id_skladiste'");

            if (a.Length == 0)
            {
                try
                {
                    string sql3 = "ALTER TABLE promjena_cijene_komadno_stavke ADD COLUMN id_skladiste numeric";
                    classSQL.update(sql3);
                }
                catch { }
            }
        }

        private static void InsertOstaloDjelatnosti()
        {
            string sql = "SELECT * FROM djelatnosti WHERE lower(ime_djelatnosti) like '%ostalo%'; ";
            DataTable dt = classSQL.select(sql, "partners").Tables[0];

            if (dt.Rows.Count < 1)
            {
                try
                {
                    sql = "INSERT INTO djelatnosti (ime_djelatnosti) VALUES ('Ostalo');";
                    //classSQL.insert(sql);
                }
                catch
                {
                }
            }
        }

        private static void AlterOibNazivTvrtke(DataTable cols)
        {
            ////za ms sql
            //int maxStringLength = dataSet.Tables[0].Columns["ime_tvrtke"].MaxLength;
            //DataSet ds = new DataSet("partners");
            //SqlConnection conn = new SqlConnection(connectionString);
            //SqlDataAdapter da = new SqlDataAdapter(, conn);
            //conn.Open();
            //da.FillSchema(ds, SchemaType.Source, "partners");
            //da.Fill(ds);
            //int maxStringLength = ds.Tables[0].Columns["ime_tvrtke"].MaxLength;
            //conn.Close();
            ////za ms sql

            //DataRow[] dataROW = dt.Select("table_name = 'partners'");

            //string remoteConnectionString = SQL.claasConnectDatabase.GetRemoteConnectionString();
            //string sql1 = "select ime_tvrtke from partners";
            //NpgsqlConnection remoteConnection = new NpgsqlConnection(remoteConnectionString);
            //DataSet dataSet = new DataSet();
            //if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
            //NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql1.Replace("+", "||").Replace("[", "\"").Replace("]", "\"").Replace("zbroj", "+"), remoteConnection);

            //dataSet.Reset();

            ////treba napuniti šemu tak da znamo veličinu kolone
            //da.FillSchema(dataSet, SchemaType.Source, "partners");
            //da.Fill(dataSet);

            //int maxStringLength = dataSet.Tables[0].Columns["ime_tvrtke"].MaxLength;

            ////ako je postavljeno na 200 onda ne treba update
            //if (maxStringLength == 200)
            //    return;

            DataRow[] a = cols.Select("table_name = 'partners' and column_name = 'ime_tvrtke'");

            //ako je postavljeno na 200 onda ne treba update
            if (a[0][3].ToString() != "200")
            {
                try
                {
                    string sql = "alter table partners alter column ime_tvrtke type varchar(200); " +
                        "alter table partners alter column oib type varchar(30); ";
                    classSQL.select(sql, "partners");
                }
                catch
                {
                }
            }

            try
            {
                DataRow[] row = cols.Select("table_name = 'partners' and column_name = 'uSustavPdv'");

                if (row.Length == 0)
                {
                    classSQL.select("alter table partners add column uSustavPdv boolean default true;", "partners");
                }//alter table faktura_stavke alter column sifra type varchar(30);
            }
            catch { }
        }

        private static void AlterRobaKaucija(DataTable dt)
        {
            DataRow[] dataROW = dt.Select("table_name = 'roba_kaucija'");

            string sql;

            if (dataROW.Length == 0)
            {
                sql = "CREATE TABLE roba_kaucija" +
                "(" +
                  "id_stavka serial NOT NULL," +
                  "sifra character varying(30)," +
                  "sifra_kaucija character varying(30)," +
                  "kolicina numeric," +
                  "CONSTRAINT roba_kaucija_pkey PRIMARY KEY (id_stavka))";

                classSQL.select(sql, "roba_kaucija");
            }
        }

        private static void AlterValute(DataTable dt)
        {
            DataRow[] a = dt.Select("table_name = 'valute' and column_name = 'sifra'");

            if (a.Length == 0)
            {
                try
                {
                    string sql = "ALTER TABLE valute ADD COLUMN sifra character(3); " +
                        "ALTER TABLE valute ADD COLUMN naziv character(3); " +
                        "ALTER TABLE valute ADD COLUMN puni_naziv character varying(100); " +
                        "ALTER TABLE valute ADD COLUMN paritet numeric; ";
                    classSQL.update(sql);

                    sql = "UPDATE valute SET sifra='978',naziv='EUR',puni_naziv='Euro',paritet='1'" +
                        " WHERE ime_valute='978 EUR'";
                    classSQL.update(sql);
                    sql = "UPDATE valute SET sifra='840',naziv='USD',puni_naziv='Američki Dolar',paritet='1'" +
                        " WHERE ime_valute='840 USD'";
                    classSQL.update(sql);
                    sql = "UPDATE valute SET sifra='348',naziv='HUF',puni_naziv='Mađarska forinta',paritet='100'" +
                        " WHERE ime_valute='348 HUF'";
                    classSQL.update(sql);
                    sql = "UPDATE valute SET sifra='756',naziv='CHF',puni_naziv='Švicarski franak',paritet='1'" +
                        " WHERE ime_valute='756 CHF'";
                    classSQL.update(sql);
                }
                catch
                {
                }
            }
        }

        private static void AlterRadniNalog(DataTable DTremote_cols)
        {
            DataRow[] a = DTremote_cols.Select("table_name = 'radni_nalog' and column_name = 'izvrsio'");

            if (a.Length == 0)
            {
                try
                {
                    string sql = "ALTER TABLE radni_nalog ADD COLUMN izvrsio character(150);";
                    classSQL.update(sql);
                }
                catch
                {
                }
            }
        }

        private static void AlterIFB(DataTable dt)
        {
            DataRow[] dataROW = dt.Select("table_name = 'ifb'");

            string sql;

            if (dataROW.Length == 0)
            {
                sql = "CREATE TABLE ifb" +
                "(" +
                  "broj bigint," +
                  "id serial NOT NULL," +
                  "datum timestamp without time zone," +
                  "datum_valute timestamp without time zone," +
                  "datum_dvo timestamp without time zone," +
                  "id_zaposlenik integer," +
                  "model character varying(20)," +
                  "valuta numeric," +
                  "id_valuta integer," +
                  "otprema character varying(100)," +
                  "napomena text," +
                  "ukupno numeric," +
                  "mj_troska text," +
                  "odrediste integer," +
                  "godina integer," +
                  "id_nacin_placanja integer," +
                  "jir character varying(100)," +
                  "zki character varying(100)," +
                  "CONSTRAINT ifb_pkey PRIMARY KEY (id ))";

                classSQL.select(sql, "ifb");

                sql = "CREATE TABLE ifb_stavke" +
                    "(" +
                      "id_stavka serial NOT NULL," +
                      "kolicina numeric," +
                      "vpc numeric," +
                      "broj bigint," +
                      "mpc numeric," +
                      "porez numeric," +
                      "rabat numeric," +
                      "naziv text," +
                      "jmj character varying(20)," +
                      "CONSTRAINT ifb_stavke_pkey PRIMARY KEY (id_stavka )" +
                    ")";

                classSQL.select(sql, "ifb_stavke");
            }
        }

        private static void Podaci_tvrtka_pdv(DataTable dt)
        {
            // Zakomentirano jer se nigdje ne koristi: DataRow[] dataROW = dt.Select("table_name = 'podaci_tvrtka'");

            DataRow[] a = dt.Select("table_name = 'podaci_tvrtka' and column_name = 'pdv_br'");

            if (a.Length == 0)
            {
                try
                {
                    string sql = "ALTER TABLE podaci_tvrtka ADD COLUMN pdv_br character varying DEFAULT '-'";
                    classSQL.update(sql);
                }
                catch
                {
                }
            }

            a = dt.Select("table_name = 'podaci_tvrtka' and column_name = 'ime_poslovnice'");

            if (a.Length == 0)
            {
                try
                {
                    string sql = "ALTER TABLE podaci_tvrtka ADD COLUMN ime_poslovnice character varying DEFAULT '-'";
                    classSQL.update(sql);
                }
                catch
                {
                }
            }

            a = dt.Select("table_name = 'podaci_tvrtka' and column_name = 'direktor'");

            if (a.Length == 0)
            {
                try
                {
                    string sql = "ALTER TABLE podaci_tvrtka ADD COLUMN direktor character varying";
                    classSQL.update(sql);
                    sql = "update podaci_tvrtka set direktor = (select vl from podaci_tvrtka);";
                    classSQL.update(sql);
                }
                catch
                {
                }
            }

            a = dt.Select("table_name = 'podaci_tvrtka' and column_name = 'racun_bool'");

            if (a.Length == 0)
            {
                try
                {
                    string sql = "ALTER TABLE podaci_tvrtka ADD COLUMN racun_bool int DEFAULT 1";
                    classSQL.update(sql);
                }
                catch
                {
                }
            }

            a = dt.Select("table_name = 'podaci_tvrtka' and column_name = 'text_racun2'");

            if (a.Length == 0)
            {
                try
                {
                    string sql = "ALTER TABLE podaci_tvrtka ADD COLUMN text_racun2 character varying";
                    classSQL.update(sql);
                }
                catch
                {
                }
            }
        }

        private static void AlterRadniNalogServis(DataTable dt)
        {
            DataRow[] dataROW = dt.Select("table_name = 'radni_nalog_servis'");

            string sql;

            if (dataROW.Length == 0)
            {
                sql = "CREATE TABLE radni_nalog_servis" +
                        "(" +
                          "broj bigint NOT NULL," +
                          "id_odrediste bigint," +
                          "id_fakturirati bigint," +
                          "date timestamp without time zone," +
                          "vrijedi_do timestamp without time zone," +
                          "id_izjava integer," +
                          "id_zaposlenik_komercijala bigint," +
                          "id_zaposlenik_izradio bigint," +
                          "model character varying(15)," +
                          "id_nacin_placanja integer," +
                          "id_valuta integer," +
                          "otprema integer," +
                          "id_nar_kupca bigint," +
                          "id_vd character varying(5)," +
                          "godina character varying(6)," +
                          "ukupno money," +
                          "napomena text," +
                          "zr integer," +
                          "CONSTRAINT radni_nalog_servis_primary_key PRIMARY KEY (broj))";

                classSQL.select(sql, "radni_nalog_servis");

                sql = "CREATE TABLE radni_nalog_servis_stavke(" +
                          "kolicina character varying(10)," +
                          "vpc numeric," +
                          "porez character varying(5)," +
                          "broj bigint," +
                          "rabat character varying(5)," +
                          "id_skladiste integer," +
                          "sifra character varying(30)," +
                          "id_stavka serial NOT NULL," +
                          "naziv character varying(150)," +
                          "oduzmi character varying(2)," +
                          "porez_potrosnja character varying(5)," +
                          "CONSTRAINT radni_nalog_servis_stavke_primary_key PRIMARY KEY (id_stavka ))";

                classSQL.select(sql, "radni_nalog_servis_stavke");
            }
        }

        private static void AlterPodaciTvrtke(DataTable DT_compactDB)
        {
            try
            {
                int ss;
                DataRow[] row = DT_compactDB.Select("table_name = 'podaci_tvrtka' and column_name = 'email'");

                if (row.Length > 0)
                {
                    int.TryParse(row[0]["CHARACTER_MAXIMUM_LENGTH"].ToString(), out ss);
                    if (ss < 512)
                    {
                        classSQL.select_settings("alter table podaci_tvrtka alter column email nvarchar(512)", "podaci_tvrtka");
                        //classSQL.update("alter table podaci_tvrtka alter column iban type character varying");
                    }
                }
            }
            catch { }

            string provjeri_col_type = "select column_name, data_type, character_maximum_length from information_schema.columns where " +
               " table_name='podaci_tvrtka' AND column_name ='text_racun'";
            DataTable DTcoltype = classSQL.select_settings(provjeri_col_type, "podaci_tvrtka").Tables[0];

            if (DTcoltype.Rows.Count < 1)
            {
                try
                {
                    string sql = "ALTER TABLE podaci_tvrtka ADD COLUMN text_racun nvarchar(1000)";
                    classSQL.Setings_Update(sql);
                }
                catch
                {
                }
            }

            //;

            if (DT_compactDB.Select("table_name = 'podaci_tvrtka' and column_name = 'direktor'").Length == 0)
            {
                try
                {
                    string sql = "ALTER TABLE podaci_tvrtka ADD COLUMN direktor nvarchar(100)";
                    classSQL.Setings_Update(sql);
                    sql = "update podaci_tvrtka set direktor = '" + (Class.PodaciTvrtka.vlasnikTvrtke == null ? "" : Class.PodaciTvrtka.vlasnikTvrtke) + "';";
                    classSQL.Setings_Update(sql);
                }
                catch
                {
                }
            }

            string provjeri_col_type_posl = "select column_name, data_type, character_maximum_length from information_schema.columns where " +
                       " table_name='podaci_tvrtka' AND column_name ='ime_poslovnice'";
            DataTable DTcoltype_posl = classSQL.select_settings(provjeri_col_type_posl, "podaci_tvrtka").Tables[0];

            if (DTcoltype_posl.Rows.Count < 1)
            {
                try
                {
                    string sql = "ALTER TABLE podaci_tvrtka ADD COLUMN ime_poslovnice nvarchar(150) DEFAULT '-'";
                    classSQL.Setings_Update(sql);
                }
                catch
                {
                }
            }
            else
            {
                Podaci_tvrtka_compact(DT_compactDB);
            }

            string provjeri_col_type3 = "select column_name, data_type, character_maximum_length from information_schema.columns where " +
                    " table_name='podaci_tvrtka' AND column_name ='pdv_br'";
            DataTable DTcoltype3 = classSQL.select_settings(provjeri_col_type3, "podaci_tvrtka").Tables[0];

            if (DTcoltype3.Rows.Count < 1)
            {
                try
                {
                    string sql_pdv = "ALTER TABLE podaci_tvrtka ADD COLUMN pdv_br nvarchar(50)";
                    classSQL.Setings_Update(sql_pdv);
                }
                catch
                {
                }
            }

            string provjeri_col_type1 = "select column_name, data_type, character_maximum_length from information_schema.columns where " +
                " table_name='podaci_tvrtka' AND column_name ='text_racun2'";
            DataTable DTcoltype1 = classSQL.select_settings(provjeri_col_type1, "podaci_tvrtka").Tables[0];

            if (DTcoltype1.Rows.Count < 1)
            {
                try
                {
                    string sql1 = "ALTER TABLE podaci_tvrtka ADD COLUMN text_racun2 nvarchar(1000)";
                    classSQL.Setings_Update(sql1);
                }
                catch
                {
                }
            }

            string provjeri_col_type2 = "select column_name, data_type, character_maximum_length from information_schema.columns where " +
                " table_name='podaci_tvrtka' AND column_name ='racun_bool'";
            DataTable DTcoltype2 = classSQL.select_settings(provjeri_col_type2, "podaci_tvrtka").Tables[0];

            if (DTcoltype2.Rows.Count < 1)
            {
                try
                {
                    string sql2 = "ALTER TABLE podaci_tvrtka ADD COLUMN racun_bool int DEFAULT 1";
                    classSQL.Setings_Update(sql2);
                }
                catch
                {
                }
            }

            provjeri_col_type = "select column_name, data_type, character_maximum_length from information_schema.columns where " +
               " table_name='podaci_tvrtka' AND column_name ='swift'";
            DTcoltype = classSQL.select_settings(provjeri_col_type, "podaci_tvrtka").Tables[0];

            if (DTcoltype.Rows.Count < 1)
            {
                try
                {
                    string sql = "ALTER TABLE podaci_tvrtka ADD COLUMN swift nvarchar(30)";
                    classSQL.Setings_Update(sql);
                }
                catch
                {
                }
            }
        }

        private static void AlterColumnsDataType(DataTable cols)
        {
            string provjeri_col_type = "select column_name , data_type, character_maximum_length from information_schema.columns where " +
                           " table_name='ponude_stavke' AND column_name ='rabat'";
            DataTable DTcoltype = classSQL.select(provjeri_col_type, "provjera").Tables[0];
            if (DTcoltype.Rows[0]["character_maximum_length"].ToString() != "30")
            {
                string prom_velicine = "alter table faktura_stavke alter column sifra type varchar(30); " +
                    "alter table inventura_stavke alter column sifra_robe type varchar(30); " +
                    "alter table izdatnica_stavke alter column sifra type varchar(30); " +
                    "alter table kalkulacija_stavke alter column sifra type varchar(30); " +
                    "alter table meduskladisnica_stavke alter column sifra type varchar(30); " +
                    "alter table normativi alter column sifra_artikla type varchar(30); " +
                    "alter table odjava_komisione_stavke alter column sifra type varchar(30); " +
                    "alter table otpis_robe_stavke alter column sifra type varchar(30); " +
                    "alter table otpremnica_stavke alter column sifra_robe type varchar(30); " +
                    "alter table pocetno_stanje_stavke alter column sifra type varchar(30); " +
                    "alter table ponude_stavke alter column sifra type varchar(30); " +
                    "alter table ponude_stavke alter column naziv type varchar(150); " +
                    "alter table ponude_stavke alter column rabat type varchar(30);" +
                    "alter table ponude_stavke alter column porez type varchar(30);" +
                    "alter table ponude_stavke alter column porez_potrosnja type varchar(30);" +
                    "alter table povrat_robe_stavke alter column sifra type varchar(30); " +
                    "alter table povratna_naknada alter column sifra type varchar(30); " +
                    "alter table primka_stavke alter column sifra type varchar(30); " +
                    "alter table promjena_cijene_stavke alter column sifra type varchar(30); " +
                    "alter table racun_stavke alter column sifra_robe type varchar(30); " +
                    "alter table radni_nalog_normativ alter column sifra type varchar(30); " +
                    "alter table radni_nalog_stavke alter column sifra_robe type varchar(30); " +
                    "alter table roba alter column sifra type varchar(30); " +
                    "alter table roba_prodaja alter column sifra type varchar(30); ";
                classSQL.insert(prom_velicine);
            }

            provjeri_col_type = "select table_name, column_name, data_type, character_maximum_length from information_schema.columns where column_name = 'naziv'";
            DTcoltype = classSQL.select(provjeri_col_type, "provjera").Tables[0];

            for (int i = 0; i < DTcoltype.Rows.Count; i++)
                if ((DTcoltype.Rows[i]["data_type"].ToString() == "character varying") & (DTcoltype.Rows[i]["character_maximum_length"].ToString() != ""))
                    classSQL.insert("alter table " + DTcoltype.Rows[i]["table_name"].ToString() + " alter column naziv type character varying");
        }

        private static void AlterPodaciTvrtke1(DataTable cols)
        {
            //Zakomentirano jer nigdje ne postoji
            //DataRow[] dataROW = DT_compactDB.Select("table_name = 'podaci_tvrtka'");

            DataRow[] a = cols.Select("table_name = 'podaci_tvrtka' and column_name = 'text_racun'");

            if (a.Length == 0)
            {
                try
                {
                    string sql = "ALTER TABLE podaci_tvrtka ADD COLUMN text_racun character varying(1000)";
                    classSQL.update(sql);
                }
                catch
                {
                }
            }

            a = cols.Select("table_name = 'podaci_tvrtka' and column_name = 'swift'");

            if (a.Length == 0)
            {
                try
                {
                    string sql = "ALTER TABLE podaci_tvrtka ADD COLUMN swift character varying(30)";
                    classSQL.update(sql);
                }
                catch
                {
                }
            }
        }

        private static void AlterAktivnostPodataka(DataTable DT_compactDB)
        {
            DataRow[] dataROW = DT_compactDB.Select("table_name = 'aktivnost_podataka'");

            string sql;

            try
            {
                string provjeri_col_type = "select column_name, data_type, character_maximum_length from information_schema.columns where " +
                   " table_name='aktivnost_podataka' AND column_name ='kartoteka'";
                DataTable DTcoltype = classSQL.select_settings(provjeri_col_type, "aktivnost_podataka").Tables[0];

                if (DTcoltype.Rows.Count < 1)
                {
                    try
                    {
                        sql = "ALTER TABLE aktivnost_podataka ADD COLUMN kartoteka int";
                        classSQL.Setings_Update(sql);
                    }
                    catch
                    {
                    }
                }

                sql = "SELECT kartoteka FROM aktivnost_podataka";
                DataTable dt = classSQL.select_settings(sql, "aktivnost_podataka").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == "")
                    {
                        sql = "UPDATE aktivnost_podataka SET kartoteka='0'";
                        classSQL.Setings_Update(sql);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            try
            {
                string provjeri_col_type = "select column_name, data_type, character_maximum_length from information_schema.columns where " +
                   " table_name='aktivnost_podataka' AND column_name ='otpis_robe'";
                DataTable DTcoltype = classSQL.select_settings(provjeri_col_type, "aktivnost_podataka").Tables[0];

                if (DTcoltype.Rows.Count < 1)
                {
                    try
                    {
                        sql = "ALTER TABLE aktivnost_podataka ADD COLUMN otpis_robe int";
                        classSQL.Setings_Update(sql);
                    }
                    catch
                    {
                    }
                }

                sql = "SELECT otpis_robe FROM aktivnost_podataka";
                DataTable dt = classSQL.select_settings(sql, "aktivnost_podataka").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == "")
                    {
                        sql = "UPDATE aktivnost_podataka SET otpis_robe='1'";
                        classSQL.Setings_Update(sql);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            if (dataROW.Length == 0)
            {
                sql = " CREATE TABLE aktivnost_podataka" +
                         " ( id int," +
                         " kalkulacije int," +
                         " inventura int," +
                         " kartica int," +
                         " faktura int," +
                         " faktura_bez_robe int," +
                         " ponude int," +
                         " radni_nalog int," +
                         " odjava_robe int," +
                         " povrat_dobavljacu int," +
                         " naljepnice int," +
                         " ulazne_fakture int," +
                         " otpremnica int," +
                         " meduskladisnice int," +
                         " primke int," +
                         " izdatnice int," +
                         " promocije int," +
                         " pocetno_stanje int," +
                         " promet_po_robi int," +
                         " boolRobno int," +
                         " kartoteka int," +
                         " otpis_robe int," +
                         " CONSTRAINT aktivnost_podataka_pkey PRIMARY KEY (id)" +
                         " )";

                classSQL.select_settings(sql, "aktivnost_podataka");
                classSQL.select_settings("INSERT INTO aktivnost_podataka (id) VALUES (1)", "aktivnost_podataka");

                sql = "UPDATE aktivnost_podataka SET " +
                    "kalkulacije='1'," +
                    "inventura='1'," +
                    "kartica='1'," +
                    "faktura='1'," +
                    "faktura_bez_robe='1'," +
                    "ponude='1'," +
                    "radni_nalog='1'," +
                    "odjava_robe='1'," +
                    "povrat_dobavljacu='1'," +
                    "naljepnice='1'," +
                    "ulazne_fakture='1'," +
                    "otpremnica='1'," +
                    "meduskladisnice='1'," +
                    "primke='1'," +
                    "izdatnice='1'," +
                    "promocije='1'," +
                    "pocetno_stanje='1'," +
                    "promet_po_robi='1'," +
                    "kartoteka='1'," +
                    "otpis_robe='1'," +
                    "boolRobno='1'";
                classSQL.Setings_Update(sql);
            }

            try
            {
                string provjeri_col_type = @"select column_name, data_type, character_maximum_length from information_schema.columns
where table_name='aktivnost_podataka' AND column_name ='isKasica'";
                DataTable DTcoltype = classSQL.select_settings(provjeri_col_type, "aktivnost_podataka").Tables[0];

                if (DTcoltype.Rows.Count < 1)
                {
                    try
                    {
                        sql = @"ALTER TABLE aktivnost_podataka ADD COLUMN isKasica bit;";
                        classSQL.Setings_Update(sql);
                        sql = @"select kalkulacije from aktivnost_podataka;";
                        DataSet dsIsKasica = classSQL.select_settings(sql, "aktivnost_podataka");
                        if (dsIsKasica.Tables[0].Rows[0]["kalkulacije"] == "0")
                        {
                            sql = "update aktivnost_podataka set isKasica = 1;";
                        }
                        else
                        {
                            sql = "update aktivnost_podataka set isKasica = 0;";
                        }
                        classSQL.Setings_Update(sql);
                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private static void AlterUfa(DataTable DTremote)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'ufa'");
            if (dataROW.Length == 0)
            {
                string sql = "CREATE TABLE ufa" +
                     "( broj bigint," +
                     " id serial NOT NULL," +
                     " datum_knjizenja timestamp without time zone," +
                     " datum_valute timestamp without time zone," +
                     " datum_dvo timestamp without time zone," +
                     " id_zaposlenik integer," +
                     " model character varying(20)," +
                     " valuta numeric," +
                     " id_valuta integer," +
                     " id_pdv integer," +
                     " na_zr integer," +
                     " sa_zr integer," +
                     " napomena text," +
                     " odrediste integer," +
                     " godina integer," +
                     " id_nacin_placanja integer," +
                     " ukupno numeric," +
                     " carina numeric," +
                     " neoporezivo numeric," +
                     " osnovica1 numeric," +
                     " osnovica2 numeric," +
                     " osnovica3 numeric," +
                     " odbitak1 numeric," +
                     " odbitak2 numeric," +
                     " odbitak3 numeric," +
                     " odbitak4 numeric," +
                     " nu1 numeric," +
                     " nu2 numeric," +
                     " nu3 numeric," +
                     " nu4 numeric," +
                     " nu5 numeric," +
                     " nu6 numeric," +
                     " nu7 numeric," +
                     " id_vd character varying(10)," +
                     " CONSTRAINT ufa_pkey PRIMARY KEY (id)" +
                     " )";

                classSQL.select(sql, "ifa");
            }
        }

        private static void AlterOtpis(DataTable DTremote)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'otpis_robe'");
            if (dataROW.Length == 0)
            {
                string sql = "CREATE TABLE otpis_robe" +
                     "( broj bigint NOT NULL," +
                     " mjesto_troska character varying(20)," +
                     " id_izradio integer," +
                     " napomena text," +
                     " godina character varying(5)," +
                     " datum timestamp without time zone," +
                     " id_skladiste integer," +
                     " CONSTRAINT otpis_robe_pkey PRIMARY KEY (broj)" +
                     " )";

                classSQL.select(sql, "otpis_robe");
            }
        }

        private static void AlterPonude(DataTable DTremote, DataTable cols)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'ponude'");
            if (dataROW.Length > 0)
            {
                DataRow[] a = cols.Select("table_name = 'ponude' and column_name = 'tecaj'");

                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE ponude ADD COLUMN tecaj numeric; " +
                            "ALTER TABLE ponude ADD COLUMN obracun_poreza character varying(1);";
                        classSQL.select(sql, "ponude");
                    }
                    catch
                    {
                    }
                }

                a = cols.Select("table_name = 'ponude' and column_name = 'ponuda_nbc'");

                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE ponude ADD COLUMN ponuda_nbc boolean default false;";
                        classSQL.select(sql, "ponude");
                    }
                    catch
                    {
                    }
                }

                a = cols.Select("table_name = 'ponude' and column_name = 'realizirano'");

                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE ponude ADD COLUMN realizirano boolean default false;";
                        classSQL.select(sql, "ponude");
                    }
                    catch
                    {
                    }
                }
            }
        }

        private static void AlterOtpisStavke(DataTable DTremote, DataTable cols)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'otpis_robe_stavke'");
            if (dataROW.Length == 0)
            {
                string sql = "CREATE TABLE otpis_robe_stavke" +
                     "( sifra character varying(25)," +
                     " vpc numeric," +
                     " mpc money," +
                     " pdv character varying(5)," +
                     " rabat character varying(15)," +
                     " broj bigint NOT NULL," +
                     " kolicina character varying(8)," +
                     " id_stavka serial NOT NULL," +
                     " nbc money," +
                     " CONSTRAINT otpis_robe_stavke_pkey PRIMARY KEY (id_stavka)" +
                     " )";

                classSQL.select(sql, "otpis_robe_stavke");
            }

            DataRow[] a = cols.Select("table_name = 'otpis_robe_stavke' and column_name = 'vpc'");

            if (a.Length == 1)
            {
                if (a[0][2].ToString() == "money")
                {
                    try
                    {
                        string sql = "ALTER TABLE otpis_robe_stavke alter column vpc TYPE numeric;";
                        classSQL.select(sql, "otpis_robe_stavke");
                    }
                    catch
                    {
                    }
                }
            }

            a = cols.Select("table_name = 'povrat_robe_stavke' and column_name = 'vpc'");

            if (a.Length == 1)
            {
                if (a[0][2].ToString() == "money")
                {
                    try
                    {
                        string sql = "ALTER TABLE povrat_robe_stavke alter column vpc TYPE numeric;";
                        classSQL.select(sql, "povrat_robe_stavke");
                    }
                    catch
                    {
                    }
                }
            }
        }

        private static void AlterNeuspjelaFiskalizacija(DataTable DTremote, DataTable cols)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'neuspjela_fiskalizacija'");
            if (dataROW.Length == 0)
            {
                try
                {
                    string sql = "CREATE TABLE neuspjela_fiskalizacija" +
                         "(" +
                         " id serial NOT NULL," +
                         " broj_racuna character varying(20)," +
                         " xml text," +
                         " greska text," +
                         " id_ducan character varying(20)," +
                         " id_kasa character varying(20)," +
                         " blagajnik character varying(20)," +
                         " iznos character varying(20)," +
                         " date timestamp without time zone," +
                         " vrsta character varying(20)," +
                         " CONSTRAINT neuspjela_fiskalizacija_primary_key PRIMARY KEY (id )" +
                         ")" +
                         " WITH (" +
                         " OIDS=FALSE" +
                         ");" +
                         " ALTER TABLE neuspjela_fiskalizacija" +
                         " OWNER TO postgres;";
                    classSQL.select(sql, "neuspjela_fiskalizacija");

                    return;
                }
                catch
                {
                    //već postoji
                }
            }

            try
            {
                string sql;
                DataRow[] a = cols.Select("table_name = 'neuspjela_fiskalizacija' and column_name = 'vrsta'");

                if (a.Length == 0)
                {
                    try
                    {
                        sql = "ALTER TABLE neuspjela_fiskalizacija ADD COLUMN vrsta character varying(20)";
                        classSQL.select(sql, "neuspjela_fiskalizacija");
                    }
                    catch
                    {
                    }
                }

                //apdejta sve neuspjele fiskalizacije
                sql = "SELECT vrsta, id FROM neuspjela_fiskalizacija";
                string vrsta;
                DataTable dt = classSQL.select(sql, "neuspjela_fiskalizacija").Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    vrsta = dt.Rows[i]["vrsta"].ToString();
                    if (vrsta == "")
                    {
                        vrsta = "račun";
                        sql = "UPDATE neuspjela_fiskalizacija SET vrsta='" + vrsta + "' WHERE id ='" + dt.Rows[i]["id"].ToString() + "'";
                        classSQL.update(sql);
                    }
                }
            }
            catch
            {
            }
        }

        private static void AlterPovratnaNaknada(DataTable DTremote)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'povratna_naknada'");
            if (dataROW.Length == 0)
            {
                try
                {
                    string sql = "create TABLE povratna_naknada" +
                        " (id serial NOT NULL," +
                        " sifra character varying(30)," +
                        " iznos numeric," +
                        " CONSTRAINT povratna_naknada_id PRIMARY KEY (id)" +
                        " )";
                    classSQL.select(sql, "povratna_naknada");
                }
                catch
                {
                    //već postoji
                }
            }
        }

        private static void FillPartnersOIBpolje(DataTable DTremote, DataTable cols)
        {
            DataRow[] a = cols.Select("table_name = 'partners' and column_name = 'oib_polje'");

            if (a.Length == 0)
            {
                DataRow[] dataROW = DTremote.Select("table_name = 'partners'");

                try
                {
                    string sql = "ALTER TABLE partners ADD COLUMN oib_polje character varying;";
                    classSQL.select(sql, "partners");
                }
                catch
                {
                    //već postoji
                }

                string sql_oib = "Select * from partners";
                DataTable DToib = classSQL.select(sql_oib, "oib_polje").Tables[0];
                for (int i = 0; i < DToib.Rows.Count; i++)
                {
                    string oib_p = DToib.Rows[i]["oib_polje"].ToString();
                    string id = DToib.Rows[i]["id_partner"].ToString();
                    if (oib_p == null || oib_p == "")
                    {
                        classSQL.update("update partners set oib_polje='OIB' WHERE id_partner = '" + id + "'");
                    }
                }
            }
        }

        private static void AlterRacuniStavke(DataTable DTremote, DataTable cols)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'racun_stavke'");
            if (dataROW.Length > 0)
            {
                DataRow[] a = cols.Select("table_name = 'racun_stavke' and column_name = 'povratna_naknada'");

                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE racun_stavke ADD COLUMN povratna_naknada numeric;";
                        classSQL.select(sql, "racun_stavke");
                    }
                    catch
                    {
                    }
                }

                a = cols.Select("table_name = 'racun_stavke' AND column_name = 'naziv'");
                if(a.Length == 0)
                {
                    string sql = "ALTER TABLE racun_stavke ADD COLUMN naziv character varying (100) DEFAULT NULL;";
                    classSQL.select(sql, "racun_stavke");
                }

                a = cols.Select("table_name = 'racun_stavke' and column_name = 'rabat_izn'");

                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE racun_stavke ADD COLUMN rabat_izn numeric;" +
                            "ALTER TABLE racun_stavke ADD COLUMN mpc_rabat numeric;" +
                            "ALTER TABLE racun_stavke ADD COLUMN ukupno_rabat numeric;" +
                            "ALTER TABLE racun_stavke ADD COLUMN ukupno_vpc numeric;" +
                            "ALTER TABLE racun_stavke ADD COLUMN ukupno_mpc numeric;" +
                            "ALTER TABLE racun_stavke ADD COLUMN ukupno_mpc_rabat numeric;" +
                            "ALTER TABLE racun_stavke ADD COLUMN povratna_naknada_izn numeric;" +
                            "ALTER TABLE racun_stavke ADD COLUMN ukupno_porez numeric;" +
                            "ALTER TABLE racun_stavke ADD COLUMN ukupno_osnovica numeric;";
                        classSQL.select(sql, "racun_stavke");
                    }
                    catch
                    {
                    }
                }
            }
        }

        private static void AlterRacuni(DataTable DTremote, DataTable cols)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'racuni'");
            if (dataROW.Length > 0)
            {
                DataRow[] a = cols.Select("table_name = 'racuni' and column_name = 'jir'");

                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE racuni ADD COLUMN jir character varying(100);" +
                            "ALTER TABLE racuni ADD COLUMN zki character varying(100);";
                        classSQL.select(sql, "racuni");
                    }
                    catch
                    {
                    }
                }

                a = cols.Select("table_name = 'racuni' and column_name = 'napomena'");

                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE racuni ADD COLUMN napomena text;";
                        classSQL.select(sql, "racuni");
                    }
                    catch
                    {
                    }
                }

                a = cols.Select("table_name = 'racuni' and column_name = 'kartica_kupca'");

                if (a.Length == 0)
                {
                    string sql = @"ALTER TABLE racuni ADD COLUMN kartica_kupca varchar(50) default null;";
                    classSQL.insert(sql);

                    sql = "ALTER TABLE partners alter COLUMN broj_kartice TYPE varchar(50);";
                    classSQL.update(sql);
                }

                a = cols.Select("table_name = 'racuni' and column_name = 'broj_ispisa'");
                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE racuni ADD COLUMN broj_ispisa INT DEFAULT 0;";
                        classSQL.select(sql, "racuni");

                        sql = "ALTER TABLE fakture ADD COLUMN broj_ispisa INT DEFAULT 0;";
                        classSQL.select(sql, "fakture");
                    }
                    catch
                    {
                    }
                }

                a = cols.Select("table_name = 'racuni' and column_name = 'nacin_placanja'");

                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE racuni ADD COLUMN nacin_placanja character varying(3);";
                        classSQL.select(sql, "racuni");
                    }
                    catch
                    {
                    }
                }

                a = cols.Select("table_name = 'racuni' and column_name = 'bon'");

                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE racuni ADD COLUMN bon character varying(1) default '0';";
                        classSQL.select(sql, "racuni");
                    }
                    catch
                    {
                    }
                }
                a = cols.Select("table_name = 'racuni' and column_name = 'ukupno_bon'");

                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE racuni ADD COLUMN ukupno_bon money default 0;";
                        classSQL.select(sql, "racuni");
                    }
                    catch
                    {
                    }
                }

                a = cols.Select("table_name = 'inventura_stavke' and column_name = 'pocetno_stanje'");
                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE inventura_stavke ADD COLUMN pocetno_stanje int DEFAULT 0;";
                        classSQL.select(sql, "inventura_stavka");
                    }
                    catch { }
                }

                //a = cols.Select("table_name = 'inventura_stavke' and column_name = 'nbc_bilo'");
                //if (a.Length == 0) {
                //    try {
                //        string sql = "ALTER TABLE inventura_stavke ADD COLUMN nbc_bilo numeric default 0; ALTER TABLE inventura_stavke ADD COLUMN vpc_bilo numeric default 0;";
                //        classSQL.select(sql, "inventura_stavka");
                //    } catch { }
                //}

                a = cols.Select("table_name = 'inventura_stavke' and column_name = 'nbc'");
                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE inventura_stavke ADD COLUMN nbc decimal DEFAULT 0;";
                        classSQL.select(sql, "inventura_stavka");
                    }
                    catch { }
                }

                a = cols.Select("table_name = 'inventura_stavke' and column_name = 'kolicina_koja_je_bila'");

                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE inventura_stavke ADD COLUMN kolicina_koja_je_bila NUMERIC DEFAULT 0;";
                        classSQL.select(sql, "inventura_stavke");
                        sql = "ALTER TABLE inventura_stavke ADD COLUMN postavljena_kao_stanje int DEFAULT 0;";
                        classSQL.select(sql, "inventura_stavke");
                    }
                    catch { }
                }

                a = cols.Select("table_name = 'racuni' and column_name = 'ukupno_rabat'");

                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE racuni ADD COLUMN ukupno_rabat numeric;" +
                            "ALTER TABLE racuni ADD COLUMN ukupno_porez numeric;" +
                            "ALTER TABLE racuni ADD COLUMN ukupno_osnovica numeric;" +
                            "ALTER TABLE racuni ADD COLUMN ukupno_mpc numeric;" +
                            "ALTER TABLE racuni ADD COLUMN ukupno_vpc numeric;" +
                            "ALTER TABLE racuni ADD COLUMN ukupno_mpc_rabat numeric;" +
                            "ALTER TABLE racuni ADD COLUMN ukupno_povratna_naknada numeric;";
                        classSQL.select(sql, "racuni");
                    }
                    catch
                    {
                    }
                }

                a = cols.Select("table_name = 'racuni' and column_name = 'ukupno_ostalo'");

                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE racuni ADD COLUMN ukupno_ostalo numeric;";
                        classSQL.select(sql, "racuni");
                    }
                    catch
                    {
                    }
                }
            }
        }

        private static void AlterIspisRacuna(DataTable DTremote)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'ispis_racuni'");

            if (dataROW.Length == 0)
            {
                string sql = "CREATE TABLE ispis_racuni" +
                  "(" +
                  "broj_racuna character varying(10) NOT NULL," +
                  "datum_racuna timestamp without time zone," +
                  "id_ducan integer," +
                  "id_kasa integer," +
                  "id_kupac bigint," +
                  "id_blagajnik integer," +
                  "gotovina character varying(1)," +
                  "kartice character varying(1)," +
                  "ukupno_gotovina money," +
                  "ukupno_kartice money," +
                  "storno character varying(2)," +
                  "ukupno money," +
                  "dobiveno_gotovina money," +
                  "id_stol integer," +
                  "ukupno_virman money," +
                  "br_sa_prethodnog_racuna character varying(15)," +
                  "broj_kartice_cashback character varying(16)," +
                  "broj_kartice_bodovi character varying(15)," +
                  "jir character varying(100)," +
                  "zki character varying(100)," +
                  "napomena text," +
                  "nacin_placanja character varying(3)," +
                  "ukupno_ostalo numeric," +
                  "CONSTRAINT ispis_racuni_primary_key PRIMARY KEY (broj_racuna ))";

                classSQL.select(sql, "ispis_racuni");

                sql = "CREATE TABLE ispis_racun_stavke" +
                  "(" +
                  "broj_racuna character varying(10)," +
                  "sifra_robe character varying(30)," +
                  "id_skladiste integer," +
                  "mpc money," +
                  "porez character varying(5)," +
                  "kolicina character varying(6)," +
                  "id_stavka serial NOT NULL," +
                  "rabat character varying(20)," +
                  "vpc numeric," +
                  "nbc money," +
                  "odjava character varying(1)," +
                  "porez_potrosnja character varying(10)," +
                  "povratna_naknada numeric," +
                  "CONSTRAINT ispis_racun_stavke_primary_key PRIMARY KEY (id_stavka))";

                classSQL.select(sql, "ispis_racun_stavke");
            }
        }

        private static void AlterIspisFakture(DataTable DTremote, DataTable cols)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'ispis_fakture'");

            if (dataROW.Length == 0)
            {
                string sql = "CREATE TABLE ispis_faktura_stavke" +
                    "(" +
                      "id_stavka serial NOT NULL," +
                      "kolicina character varying(8)," +
                      "vpc numeric," +
                      "porez character varying(10)," +
                      "broj_fakture bigint," +
                      "rabat character varying(10)," +
                      "id_skladiste bigint," +
                      "sifra character varying(30)," +
                      "oduzmi character varying(2)," +
                      "odjava character varying(1)," +
                      "nbc money," +
                      "naziv character varying(150)," +
                      "porez_potrosnja character varying(5)," +
                      "povratna_naknada numeric," +
                      "CONSTRAINT ispis_faktura_stavke_primary_key PRIMARY KEY (id_stavka)" +
                    ")";

                classSQL.select(sql, "ispis_faktura_stavke");

                sql = "CREATE TABLE ispis_fakture" +
                  "(" +
                  "broj_fakture bigint NOT NULL," +
                  "broj_avansa bigint NOT NULL," +
                  "id_odrediste bigint," +
                  "id_fakturirati bigint," +
                  "date timestamp without time zone," +
                  "datedvo timestamp without time zone," +
                  "datum_valute timestamp without time zone," +
                  "vrijedi_do timestamp without time zone," +
                  "id_zaposlenik_komercijala bigint," +
                  "id_nar_kupca bigint," +
                  "id_izjava integer," +
                  "id_zaposlenik integer," +
                  "id_zaposlenik_izradio integer," +
                  "model character varying(10)," +
                  "id_nacin_placanja bigint," +
                  "zr integer," +
                  "id_valuta integer," +
                  "otprema integer," +
                  "id_predujam bigint," +
                  "napomena text," +
                  "ukupno money," +
                  "id_vd character(5)," +
                  "godina_predujma character(6)," +
                  "godina_ponude character(6)," +
                  "godina_fakture character varying(6)," +
                  "mj_troska character varying(100)," +
                  "oduzmi_iz_skladista character varying(1)," +
                  "jir character varying(100)," +
                  "zki character varying(100)," +
                  "CONSTRAINT ispis_fakture_primary_key PRIMARY KEY (broj_fakture))";
                classSQL.select(sql, "ispis_fakture");
            }

            DataRow[] a = cols.Select("table_name = 'ispis_fakture' and column_name = 'tecaj'");

            if (a.Length == 0)
            {
                try
                {
                    string sql = "ALTER TABLE ispis_fakture ADD COLUMN tecaj numeric;";
                    classSQL.select(sql, "ispis_fakture");
                }
                catch
                {
                }
            }
        }

        private static void AlterSmjene(DataTable DTremote)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'smjene'");

            if (dataROW.Length == 0)
            {
                string sql = "CREATE TABLE smjene" +
                    "(" +
                      "id serial NOT NULL," +
                      "pocetno_stanje numeric," +
                      "blagajnik character varying(10)," +
                      "pocetak timestamp without time zone," +
                      "zavrsetak timestamp without time zone," +
                      "zavrsno_stanje numeric," +
                      "napomena text," +
                      "blagajnikz integer," +
                      "CONSTRAINT id_primary_key PRIMARY KEY (id )" +
                    ")";

                classSQL.select(sql, "smjene");
            }
        }

        private static void AlterOtpremnice(DataTable DTremote, DataTable cols)
        {
            DataRow[] dataROW = cols.Select("table_name = 'otpremnice' and column_name = 'use_nbc'");

            if (dataROW.Length == 0)
            {
                string sql = "ALTER TABLE otpremnice ADD COLUMN use_nbc boolean default false;";
                classSQL.select(sql, "otpremnice");
            }

            dataROW = cols.Select("table_name = 'otpremnice' and column_name = 'oduzmi_iz_skladista'");

            if (dataROW.Length == 0)
            {
                string sql = "ALTER TABLE otpremnice ADD COLUMN oduzmi_iz_skladista boolean default true;";
                classSQL.select(sql, "otpremnice");
            }
        }

        private static void AterAutomatskaUsklada(DataTable dtRemote, DataTable dtCols)
        {
            DataRow[] dataROW = dtRemote.Select("table_name = 'automatska_usklada'");
            if (dataROW.Length == 0)
            {
                string sql = @"
                            CREATE TABLE automatska_usklada
                            (
                                id serial NOT NULL,
                                broj integer,
                                datum timestamp without time zone,
                                id_zaposlenik integer,
                                id_poslovnica integer,
                                id_naplatni_uredaj integer,
                                id_skladiste integer,
                                novo bool default true,
                                editirano bool default false,
                                datum_syn timestamp without time zone,
                                CONSTRAINT automatska_usklada_id_primary_key PRIMARY KEY (id)
                            )";

                classSQL.select(sql, "automatska_usklada");

                sql = @"
                            CREATE TABLE automatska_usklada_stavke
                            (
                                id serial NOT NULL,
                                broj integer,
                                id_poslovnica integer,
                                id_naplatni_uredaj integer,
                                id_skladiste integer,
                                sifra character varying,
                                kolicina_ulaz numeric(15,4),
                                nbc_ulaz numeric(15,4),
                                vpc_ulaz numeric(15,4),
                                mpc_ulaz numeric(15,4),
                                nbc_prosjecna numeric(15,4),
                                vpc_prosjecna numeric(15,4),
                                mpc_prosjecna numeric(15,4),
                                kolicina_izlaz numeric(15,4),
                                nbc_izlaz_izracun numeric(15,4),
                                vpc_izlaz_izracun numeric(15,4),
                                mpc_izlaz_izracun numeric(15,4),
                                nbc_izlaz numeric(15,4),
                                vpc_izlaz numeric(15,4),
                                mpc_izlaz numeric(15,4),
                                CONSTRAINT automatska_usklada_stavke_id_primary_key PRIMARY KEY (id)
                            )";

                classSQL.select(sql, "automatska_usklada_stavke");
            }

            dataROW = dtCols.Select("table_name = 'automatska_usklada' and column_name = 'sifra'");
            if (dataROW.Length == 0)
            {
                string sql = "alter table automatska_usklada add column sifra character varying after id_skladiste";
                //classSQL.select(sql, "automatska_usklada_stavke");
            }
        }

        private static void AlterUlazneFakture(DataTable DTremote, DataTable cols)
        {
            DataRow[] dataROW = cols.Select("table_name = 'ufa' and column_name = 'iban_primatelja'");
            if (dataROW.Length == 0)
            {
                try
                {
                    string sql = "ALTER TABLE ufa ADD COLUMN iban_primatelja character varying(100);";
                    classSQL.select(sql, "ufa");
                }
                catch
                {
                }
            }

            dataROW = cols.Select("table_name = 'ulazna_faktura' and column_name = 'hub_kreirano'");
            if (dataROW.Length == 0)
            {
                try
                {
                    string sql = "ALTER TABLE ulazna_faktura ADD COLUMN hub_kreirano int DEFAULT 0;";
                    classSQL.select(sql, "ufa");
                }
                catch
                {
                }
            }

            dataROW = cols.Select("table_name = 'ulazna_faktura' and column_name = 'vrsta_naloga'");
            if (dataROW.Length == 0)
            {
                try
                {
                    string sql = " ALTER TABLE ulazna_faktura ADD COLUMN vrsta_naloga int;" +
                        " ALTER TABLE ulazna_faktura ADD COLUMN izvor_dokumenta int DEFAULT 300;";
                    classSQL.select(sql, "ufa");
                }
                catch
                {
                }
            }
        }

        private static void AlterIBAN(DataTable DTremote, DataTable cols)
        {
            DataRow[] dataROW = cols.Select("table_name = 'ziro_racun' and column_name = 'iban'");

            if (dataROW.Length == 0)
            {
                try
                {
                    string sql = "ALTER TABLE ziro_racun ADD COLUMN iban character varying(100);" +
                        "ALTER TABLE ziro_racun ADD COLUMN adresa_banke character varying(70);";
                    classSQL.select(sql, "ziro_racun");
                }
                catch
                {
                }
            }
        }

        private static void AlterPartneri(DataTable DTremote, DataTable cols)
        {
            DataRow[] dataROW = cols.Select("table_name = 'partners' and column_name = 'iban'");

            if (dataROW.Length == 0)
            {
                try
                {
                    string sql = "ALTER TABLE partners ADD COLUMN iban character varying(100);" +
                        "ALTER TABLE partners ADD COLUMN naziv_banke character varying(100);" +
                        "ALTER TABLE partners ADD COLUMN adresa_banke character varying(100);" +
                        "ALTER TABLE partners ADD COLUMN sjediste_banke character varying(100);" +
                        "ALTER TABLE partners ADD COLUMN sifra_zemlje_banke character varying(10);" +
                        "ALTER TABLE partners ADD COLUMN swift character varying(70);";
                    classSQL.select(sql, "ziro_racun");
                }
                catch
                {
                }
            }

            dataROW = cols.Select("table_name = 'partners' and column_name = 'default_skladiste'");
            if (dataROW.Length == 0)
            {
                string sql = " ALTER TABLE partners ADD COLUMN default_skladiste integer DEFAULT '0';";
                classSQL.insert(sql);
            }

            if (cols.Select("table_name = 'partners' and column_name = 'odgoda_placanja_u_danima'").Length == 0)
            {
                string sql = "ALTER TABLE partners ADD COLUMN odgoda_placanja_u_danima INTEGER DEFAULT 0;";
                classSQL.select(sql, "partners");
            }

            if (cols.Select("table_name = 'partners' and column_name = 'zacrnjeno'").Length == 0)
            {
                string sql = "ALTER TABLE partners ADD COLUMN zacrnjeno boolean DEFAULT false;";
                classSQL.select(sql, "partners");

                sql = string.Format(@"CREATE OR REPLACE VIEW zacrnjeni_partner AS 
 SELECT 
        CASE
            WHEN partners.zacrnjeno = true THEN '##########'::character varying
            ELSE partners.ime_tvrtke
        END AS ime_tvrtke, 
        CASE
            WHEN partners.zacrnjeno = true THEN 0::bigint
            ELSE partners.id_grad
        END AS id_grad, 
        CASE
            WHEN partners.zacrnjeno = true THEN '##########'::character varying
            ELSE partners.adresa
        END AS adresa, 
        CASE
            WHEN partners.zacrnjeno = true THEN '##########'::character varying
            ELSE partners.r1r2
        END AS r1r2, 
        CASE
            WHEN partners.zacrnjeno = true THEN '##########'::character varying
            ELSE partners.oib
        END AS oib, 
        CASE
            WHEN partners.zacrnjeno = true THEN '##########'::character varying
            ELSE partners.napomena
        END AS napomena, 
        CASE
            WHEN partners.zacrnjeno = true THEN 0
            ELSE partners.id_djelatnost
        END AS id_djelatnost, 
        CASE
            WHEN partners.zacrnjeno = true THEN '##########'::character varying(30)
            ELSE partners.zr
        END AS zr, 
        CASE
            WHEN partners.zacrnjeno = true THEN 0
            ELSE partners.id_zemlja
        END AS id_zemlja, 
    partners.id_partner, 
        CASE
            WHEN partners.zacrnjeno = true THEN '##########'::character varying
            ELSE partners.ime
        END AS ime, 
        CASE
            WHEN partners.zacrnjeno = true THEN '##########'::character varying
            ELSE partners.prezime
        END AS prezime, 
        CASE
            WHEN partners.zacrnjeno = true THEN '##########'::character varying
            ELSE partners.email
        END AS email, 
        CASE
            WHEN partners.zacrnjeno = true THEN '##########'::character varying
            ELSE partners.tel
        END AS tel, 
        CASE
            WHEN partners.zacrnjeno = true THEN '##########'::character varying
            ELSE partners.mob
        END AS mob, 
        CASE
            WHEN partners.zacrnjeno = true THEN '1900-01-01'::date
            ELSE partners.datum_rodenja
        END AS datum_rodenja, 
        CASE
            WHEN partners.zacrnjeno = true THEN 0::numeric
            ELSE partners.bodovi
        END AS bodovi, 
        CASE
            WHEN partners.zacrnjeno = true THEN 0::numeric
            ELSE partners.popust
        END AS popust, 
        CASE
            WHEN partners.zacrnjeno = true THEN '##########'::character varying(50)
            ELSE partners.broj_kartice
        END AS broj_kartice, 
    partners.aktivan, 
    partners.vrsta_korisnika, 
    partners.primanje_letaka, 
        CASE
            WHEN partners.zacrnjeno = true THEN 0
            ELSE partners.id_zupanija
        END AS id_zupanija, 
    partners.godina_cestitke, 
    partners.oib_polje, 
    partners.novo, 
    partners.editirano, 
    partners.datum_syn, 
        CASE
            WHEN partners.zacrnjeno = true THEN '##########'::character varying
            ELSE partners.iban
        END AS iban, 
        CASE
            WHEN partners.zacrnjeno = true THEN '##########'::character varying
            ELSE partners.naziv_banke
        END AS naziv_banke, 
        CASE
            WHEN partners.zacrnjeno = true THEN '##########'::character varying
            ELSE partners.adresa_banke
        END AS adresa_banke, 
        CASE
            WHEN partners.zacrnjeno = true THEN '##########'::character varying
            ELSE partners.sjediste_banke
        END AS sjediste_banke, 
        CASE
            WHEN partners.zacrnjeno = true THEN '##########'::character varying
            ELSE partners.sifra_zemlje_banke
        END AS sifra_zemlje_banke, 
        CASE
            WHEN partners.zacrnjeno = true THEN '##########'::character varying
            ELSE partners.swift
        END AS swift, 
    partners.usustavpdv, 
        CASE
            WHEN partners.zacrnjeno = true THEN 0
            ELSE partners.default_skladiste
        END AS default_skladiste, 
        CASE
            WHEN partners.zacrnjeno = true THEN 0
            ELSE partners.odgoda_placanja_u_danima
        END AS odgoda_placanja_u_danima, 
    partners.zacrnjeno
   FROM partners;

ALTER TABLE zacrnjeni_partner
  OWNER TO postgres;
");

                classSQL.select(sql, "partners");
            }
        }

        private static void AlterFakture(DataTable DTremote, DataTable cols)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'fakture'");

            if (dataROW.Length > 0)
            {
                DataRow[] a = cols.Select("table_name = 'fakture' and column_name = 'jir'");

                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE fakture ADD COLUMN jir character varying(100);" +
                            "ALTER TABLE fakture ADD COLUMN zki character varying(100);";
                        classSQL.select(sql, "fakture");
                    }
                    catch
                    {
                    }
                }

                a = cols.Select("table_name = 'fakture' and column_name = 'storno'");

                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE fakture ADD COLUMN storno character varying(2);";
                        classSQL.select(sql, "fakture");
                    }
                    catch
                    {
                    }
                }

                a = cols.Select("table_name = 'fakture' and column_name = 'ukupno_rabat'");

                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE fakture ADD COLUMN ukupno_rabat numeric;" +
                            "ALTER TABLE fakture ADD COLUMN ukupno_porez numeric;" +
                            "ALTER TABLE fakture ADD COLUMN ukupno_osnovica numeric;" +
                            "ALTER TABLE fakture ADD COLUMN ukupno_mpc numeric;" +
                            "ALTER TABLE fakture ADD COLUMN ukupno_vpc numeric;" +
                            "ALTER TABLE fakture ADD COLUMN ukupno_mpc_rabat numeric;" +
                            "ALTER TABLE fakture ADD COLUMN ukupno_povratna_naknada numeric;";
                        classSQL.select(sql, "racuni");
                    }
                    catch
                    {
                    }
                }

                //partner_poslovnica
                a = cols.Select("table_name = 'fakture' and column_name = 'partner_poslovnica'");

                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE fakture ADD COLUMN partner_poslovnica numeric DEFAULT 0;";
                        classSQL.select(sql, "fakture");
                    }
                    catch
                    {
                    }
                }

                a = cols.Select("table_name = 'fakture' and column_name = 'faktura_za_komisiju'");

                if (a.Length == 0)
                {
                    string sql = "ALTER TABLE fakture ADD COLUMN faktura_za_komisiju boolean default false;";
                    classSQL.select(sql, "fakture");
                }
            }
        }

        private static void AlterFakturaStavke(DataTable DTremote, DataTable cols)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'faktura_stavke'");
            if (dataROW.Length > 0)
            {
                DataRow[] a = cols.Select("table_name = 'faktura_stavke' and column_name = 'povratna_naknada'");

                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE faktura_stavke ADD COLUMN povratna_naknada numeric;";
                        classSQL.select(sql, "faktura_stavke");
                    }
                    catch
                    {
                    }
                }

                a = cols.Select("table_name = 'faktura_stavke' and column_name = 'rabat_izn'");

                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE faktura_stavke ADD COLUMN rabat_izn numeric;" +
                            "ALTER TABLE faktura_stavke ADD COLUMN mpc_rabat numeric;" +
                            "ALTER TABLE faktura_stavke ADD COLUMN ukupno_rabat numeric;" +
                            "ALTER TABLE faktura_stavke ADD COLUMN ukupno_vpc numeric;" +
                            "ALTER TABLE faktura_stavke ADD COLUMN ukupno_mpc numeric;" +
                            "ALTER TABLE faktura_stavke ADD COLUMN ukupno_mpc_rabat numeric;" +
                            "ALTER TABLE faktura_stavke ADD COLUMN povratna_naknada_izn numeric;" +
                            "ALTER TABLE faktura_stavke ADD COLUMN ukupno_porez numeric;" +
                            "ALTER TABLE faktura_stavke ADD COLUMN ukupno_osnovica numeric;";
                        classSQL.select(sql, "faktura_stavke");
                    }
                    catch
                    {
                    }
                }

                a = cols.Select("table_name = 'faktura_stavke' and column_name = 'naziv'");

                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE faktura_stavke ADD COLUMN naziv text;";
                        classSQL.select(sql, "faktura_stavke");
                    }
                    catch
                    {
                    }
                }

                a = cols.Select("table_name = 'fakture' and column_name = 'stavke_u_valuti'");
                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE fakture ADD COLUMN stavke_u_valuti int;";
                        classSQL.select(sql, "fakture");
                    }
                    catch
                    {
                    }
                }

                a = cols.Select("table_name = 'ponude' and column_name = 'stavke_u_valuti'");
                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE ponude ADD COLUMN stavke_u_valuti int;";
                        classSQL.select(sql, "fakture");
                    }
                    catch
                    {
                    }
                }
            }

            DataRow[] a1 = cols.Select("table_name = 'ponude' and column_name = 'stavke_u_valuti'");
            if (a1.Length == 0)
            {
                try
                {
                    string sql = "ALTER TABLE ponude ADD COLUMN stavke_u_valuti int;";
                    classSQL.select(sql, "fakture");
                }
                catch
                {
                }
            }
        }

        private static void AlterFaktureBezRobe(DataTable DTremote, DataTable cols)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'ifb'");

            if (dataROW.Length > 0)
            {
                DataRow[] a = cols.Select("table_name = 'ifb' and column_name = 'jir'");

                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE ifb ADD COLUMN jir character varying(100);";
                        classSQL.insert(sql);

                        sql = "ALTER TABLE ifb ADD COLUMN zki character varying(100);";
                        classSQL.insert(sql);
                    }
                    catch
                    {
                    }
                }

                a = cols.Select("table_name = 'ifb' and column_name = 'otpremnica_broj'");

                if (a.Length == 0)
                {
                    try
                    {
                        string sql = @"alter table ifb add column otpremnica_broj integer default 0;
alter table ifb add column otpremnica_godina integer default 0;
alter table ifb add column otpremnica_id_skladiste integer default 0;";
                        classSQL.insert(sql);
                    }
                    catch
                    {
                    }
                }
            }
        }

        private static void AlterAvansi(DataTable DTremote, DataTable cols)
        {
            DataRow[] dataROW = DTremote.Select("table_name = 'avansi'");
            if (dataROW.Length > 0)
            {
                DataRow[] a = cols.Select("table_name = 'avansi' and column_name = 'jir'");

                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE avansi ADD COLUMN jir character varying(100);" +
                            "ALTER TABLE avansi ADD COLUMN zki character varying(100);";
                        classSQL.select(sql, "avansi");
                    }
                    catch
                    {
                        //već postoji
                    }
                }

                a = cols.Select("table_name = 'avansi' and column_name = 'storno'");

                if (a.Length == 0)
                {
                    try
                    {
                        string sql = "ALTER TABLE avansi ADD COLUMN storno character varying(2);";
                        classSQL.select(sql, "avansi");
                    }
                    catch
                    {
                        //već postoji
                    }
                }

                a = cols.Select("table_name = 'avansi' and column_name = 'id'");

                if (a.Length == 0)
                {
                    try
                    {
                        //MANIPULACIJA SA PRIMARY KEY
                        string sql_CONSTRAINT = "ALTER TABLE avansi DROP CONSTRAINT broj_avansa;";
                        classSQL.insert(sql_CONSTRAINT);
                        sql_CONSTRAINT = "ALTER TABLE avansi ADD COLUMN id serial;";
                        classSQL.insert(sql_CONSTRAINT);
                        sql_CONSTRAINT = "ALTER TABLE avansi ADD PRIMARY KEY (id);";
                        classSQL.insert(sql_CONSTRAINT);
                    }
                    catch
                    {
                        //već postoji
                    }
                }

                a = cols.Select("table_name = 'avansi' and column_name = 'tecaj'");
                if (a.Length == 0)
                {
                    try
                    {
                        //MANIPULACIJA SA PRIMARY KEY
                        string sql = "ALTER TABLE avansi ADD COLUMN tecaj decimal default 1;";
                        classSQL.insert(sql);
                        //sql_CONSTRAINT = "ALTER TABLE avansi ADD COLUMN id serial;";
                        //classSQL.insert(sql_CONSTRAINT);
                        //sql_CONSTRAINT = "ALTER TABLE avansi ADD PRIMARY KEY (id);";
                        //classSQL.insert(sql_CONSTRAINT);
                    }
                    catch
                    {
                        //već postoji
                    }
                }
            }

            dataROW = DTremote.Select("table_name = 'avans_racun'");
            if (dataROW.Length == 0)
            {
                string sql = @"-- Table: avans_racun

-- DROP TABLE avans_racun;

CREATE TABLE avans_racun
(
  id serial NOT NULL,
  broj_avansa integer NOT NULL,
  poslovnica integer,
  naplatni_uredaj int default 0,
  dat_dok timestamp without time zone,
  dat_knj timestamp without time zone,
  id_zaposlenik integer,
  id_zaposlenik_izradio integer,
  model character varying(10),
  id_nacin_placanja bigint,
  id_valuta integer,
  artikl text,
  opis text,
  id_vd character(5),
  godina_avansa integer,
  ukupno numeric(15,6),
  nult_stp numeric(15,6),
  neoporezivo numeric(15,6),
  osnovica10 numeric(15,6),
  osnovica_var numeric(15,6),
  porez_var numeric(15,6),
  id_pdv integer,
  id_partner bigint,
  ziro bigint,
  jir character varying(100),
  zki character varying(100),
  storno integer default 0,
  CONSTRAINT avans_racun_pkey PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE avans_racun
  OWNER TO postgres;";
                classSQL.select(sql, "avans_racun");
            }

            dataROW = cols.Select("table_name = 'avans_racun' and column_name = 'datum_valute'");
            if (dataROW.Length == 0)
            {
                string sql = @"alter table avans_racun add column datum_valute timestamp without time zone;
update avans_racun
set datum_valute = tm.dat_knj
from (
	select *
from avans_racun
) tm
where avans_racun.id = tm.id;";

                classSQL.select(sql, "avans_racun");
            }

            dataROW = cols.Select("table_name = 'avansi' and column_name = 'artikl'");
            if (dataROW.Length == 0)
            {
                string sql = @"alter table avansi add column artikl text;
alter table avansi add column datum_valute timestamp without time zone;
update avansi
set datum_valute = tm.dat_knj
from (
	select *
from avansi
) tm
where avansi.id = tm.id;";

                classSQL.select(sql, "avansi");
            }
        }

        private static void AlterPrimka(DataTable DTremote, DataTable cols)
        {
            string sql;

            DataRow[] dataROW = DTremote.Select("table_name = 'primka'");
            if (dataROW.Length == 0)
            {
                sql = "CREATE TABLE primka" +
                    "(broj bigint NOT NULL," +
                    "id_partner bigint," +
                    "originalni_dokument character varying(100)," +
                    "id_izradio integer," +
                    "datum timestamp without time zone," +
                    "napomena text," +
                    "id_skladiste integer," +
                    "id_primka serial NOT NULL," +
                    "godina character varying(6)," +
                    "id_mjesto integer," +
                    "CONSTRAINT primka_pkey PRIMARY KEY (id_primka))";
                classSQL.select(sql, "primka");
            }
            else
            {
                try
                {
                    string provjeri = "select column_name, data_type, character_maximum_length from information_schema.columns where " +
                                      " table_name='primka' AND column_name ='originalni_dokument'";
                    DataTable DTcoltype = classSQL.select(provjeri, "primka").Tables[0];

                    DataRow[] a = cols.Select("table_name = 'primka' and column_name = 'originalni_dokument'");

                    if (a.Length == 0)
                    {
                        sql = "ALTER TABLE primka RENAME column orginalni_dokumenat TO originalni_dokument;";
                        classSQL.select(sql, "primka");
                    }
                }
                catch
                {
                    //već postoji
                }

                bool apdejtano = false;
                try
                {
                    DataRow[] a = cols.Select("table_name = 'primka' and column_name = 'mjesto_troska'");

                    if (a.Length > 1)
                    {
                        sql = "ALTER TABLE primka drop column mjesto_troska;";
                        classSQL.select(sql, "primka");
                    }
                }
                catch
                {
                    //već postoji
                    apdejtano = true;
                }

                if (apdejtano)
                    return;

                try
                {
                    DataRow[] a = cols.Select("table_name = 'primka' and column_name = 'godina'");

                    if (a.Length == 0)
                    {
                        sql = "ALTER TABLE primka add column godina character varying(6);";
                        classSQL.select(sql, "primka");
                    }
                }
                catch
                {
                    //već postoji
                }

                try
                {
                    DataRow[] a = cols.Select("table_name = 'primka' and column_name = 'id_mjesto'");

                    if (a.Length == 0)
                    {
                        sql = "ALTER TABLE primka add column id_mjesto integer;";
                        classSQL.select(sql, "primka");
                    }
                }
                catch
                {
                    //već postoji
                }
            }
        }

        private static void AlterPrimkaStavke(DataTable DTremote, DataTable cols)
        {
            string sql;

            DataRow[] dataROW = DTremote.Select("table_name = 'primka_stavke'");
            if (dataROW.Length == 0)
            {
                sql = "CREATE TABLE primka_stavke" +
                    "(sifra character varying(25)," +
                    "vpc numeric," +
                    "mpc numeric," +
                    "rabat character varying(15)," +
                    "broj bigint," +
                    "kolicina character(15)," +
                    "nbc numeric," +
                    "id_stavka serial NOT NULL," +
                    "pdv character varying(5)," +
                    "ukupno numeric," +
                    "iznos numeric," +
                    "id_primka integer NOT NULL," +
                    "CONSTRAINT primka_stavke_pkey PRIMARY KEY (id_stavka));";
                classSQL.select(sql, "primka_stavke");
            }
            else
            {
                bool apdejtano = false;
                DataRow[] a = cols.Select("table_name = 'primka_stavke' and column_name = 'pdv'");

                if (a.Length == 0)
                {
                    try
                    {
                        sql = "ALTER TABLE primka_stavke add column pdv character varying(5);";
                        classSQL.select(sql, "primka_stavke");
                    }
                    catch
                    {
                        //već postoji
                        apdejtano = true;
                    }
                }

                if (apdejtano)
                    return;

                a = cols.Select("table_name = 'primka_stavke' and column_name = 'id_primka'");

                if (a.Length == 0)
                {
                    try
                    {
                        sql = "ALTER TABLE primka_stavke add column id_primka integer NOT NULL;";
                        classSQL.select(sql, "primka_stavke");
                    }
                    catch
                    {
                        //već postoji
                    }
                }

                a = cols.Select("table_name = 'primka_stavke' and column_name = 'ukupno'");

                if (a.Length == 0)
                {
                    try
                    {
                        sql = "ALTER TABLE primka_stavke add column ukupno numeric;";
                        classSQL.select(sql, "primka_stavke");
                    }
                    catch
                    {
                        //već postoji
                    }
                }

                a = cols.Select("table_name = 'primka_stavke' and column_name = 'iznos'");

                if (a.Length == 0)
                {
                    try
                    {
                        sql = "ALTER TABLE primka_stavke add column iznos numeric;";
                        classSQL.select(sql, "primka_stavke");
                    }
                    catch
                    {
                        //već postoji
                    }
                }

                a = cols.Select("table_name = 'primka_stavke' and column_name = 'vpc'");

                if (a[0][2].ToString() != "numeric")
                {
                    try
                    {
                        sql = "ALTER TABLE primka_stavke alter column vpc TYPE numeric;";
                        classSQL.select(sql, "primka_stavke");
                    }
                    catch
                    {
                        //već postoji
                    }
                }

                a = cols.Select("table_name = 'primka_stavke' and column_name = 'mpc'");

                if (a[0][2].ToString() != "numeric")
                {
                    try
                    {
                        sql = "ALTER TABLE primka_stavke alter column mpc TYPE numeric;";
                        classSQL.select(sql, "primka_stavke");
                    }
                    catch
                    {
                        //već postoji
                    }
                }

                a = cols.Select("table_name = 'primka_stavke' and column_name = 'nbc'");

                if (a[0][2].ToString() != "numeric")
                {
                    try
                    {
                        sql = "ALTER TABLE primka_stavke alter column nbc TYPE numeric;";
                        classSQL.select(sql, "primka_stavke");
                    }
                    catch
                    {
                        //već postoji
                    }
                }
            }
        }

        private static void AlterIzdatnica(DataTable DTremote, DataTable cols)
        {
            string sql;

            DataRow[] dataROW = DTremote.Select("table_name = 'izdatnica'");
            if (dataROW.Length == 0)
            {
                sql = "CREATE TABLE izdatnica" +
                    "(broj bigint NOT NULL," +
                    "id_partner bigint," +
                    "originalni_dokument character varying(100)," +
                    "id_izradio integer," +
                    "datum timestamp without time zone," +
                    "napomena text," +
                    "id_skladiste integer," +
                    "id_izdatnica serial NOT NULL," +
                    "godina character varying(6)," +
                    "id_mjesto integer," +
                    "CONSTRAINT izdatnica_pkey PRIMARY KEY (id_izdatnica))";
                classSQL.select(sql, "izdatnica");
            }
            else
            {
                DataRow[] a = cols.Select("table_name = 'izdatnica' and column_name = 'orginalni_dokumenat'");

                if (a.Length > 0)
                {
                    try
                    {
                        sql = "ALTER TABLE izdatnica RENAME column orginalni_dokumenat TO originalni_dokument;";
                        classSQL.select(sql, "izdatnica");
                    }
                    catch
                    {
                        //već postoji
                    }
                }

                bool apdejtano = false;

                a = cols.Select("table_name = 'izdatnica' and column_name = 'mjesto_troska'");

                if (a.Length > 0)
                {
                    try
                    {
                        sql = "ALTER TABLE izdatnica drop column mjesto_troska;";
                        classSQL.select(sql, "izdatnica");
                    }
                    catch
                    {
                        //već postoji
                        apdejtano = true;
                    }
                }

                if (apdejtano)
                    return;

                a = cols.Select("table_name = 'izdatnica' and column_name = 'godina'");

                if (a.Length == 0)
                {
                    try
                    {
                        sql = "ALTER TABLE izdatnica add column godina character varying(6);";
                        classSQL.select(sql, "izdatnica");
                    }
                    catch
                    {
                        //već postoji
                    }
                }

                a = cols.Select("table_name = 'izdatnica' and column_name = 'id_mjesto'");

                if (a.Length == 0)
                {
                    try
                    {
                        sql = "ALTER TABLE izdatnica add column id_mjesto integer;";
                        classSQL.select(sql, "izdatnica");
                    }
                    catch
                    {
                        //već postoji
                    }
                }
            }
        }

        private static void AlterBlagajnickiIzvjestaj(DataTable DTremote, DataTable cols)
        {
            if (DTremote.Select("table_name='blagajnicki_izvjestaj'").Length == 0)
            {
                string sql_ = "CREATE TABLE blagajnicki_izvjestaj " +
                   "(" +
                     "id serial NOT NULL," +
                     "datum timestamp without time zone," +
                     "dokumenat character varying," +
                     "oznaka_dokumenta character varying," +
                     "uplaceno decimal," +
                     "izdatak decimal," +
                     "novo boolean DEFAULT '1'," +
                     "editirano boolean DEFAULT '0'," +
                     "CONSTRAINT blagajnicki_izvjestaj_auto_pkey PRIMARY KEY (id )" +
                   ")";
                classSQL.insert(sql_);
            }

            DataRow[] a = cols.Select("table_name = 'blagajnicki_izvjestaj' and column_name = 'id_partner'");
            if (a.Length == 0)
            {
                string sql = "ALTER TABLE blagajnicki_izvjestaj add column id_partner bigint default null;";
                classSQL.select(sql, "izdatnica_stavke");
            }
        }

        private static void AlterIzdatnicaStavke(DataTable DTremote, DataTable cols)
        {
            string sql;

            DataRow[] dataROW = DTremote.Select("table_name = 'izdatnica_stavke'");
            if (dataROW.Length == 0)
            {
                sql = "CREATE TABLE izdatnica_stavke" +
                    "(sifra character varying(25)," +
                    "vpc numeric," +
                    "mpc numeric," +
                    "rabat character varying(15)," +
                    "broj bigint," +
                    "kolicina character(15)," +
                    "nbc numeric," +
                    "id_stavka serial NOT NULL," +
                    "pdv character varying(5)," +
                    "id_izdatnica integer NOT NULL," +
                    "ukupno numeric," +
                    "iznos numeric," +
                    "CONSTRAINT izdatnica_stavke_pkey PRIMARY KEY (id_stavka));";
                classSQL.select(sql, "izdatnica_stavke");
            }
            else
            {
                bool apdejtano = false;

                DataRow[] a = cols.Select("table_name = 'izdatnica_stavke' and column_name = 'pdv'");

                if (a.Length == 0)
                {
                    try
                    {
                        sql = "ALTER TABLE izdatnica_stavke add column pdv character varying(5);";
                        classSQL.select(sql, "izdatnica_stavke");
                    }
                    catch
                    {
                        //već postoji
                        apdejtano = true;
                    }
                }
                if (apdejtano)
                    return;

                a = cols.Select("table_name = 'izdatnica_stavke' and column_name = 'id_izdatnica'");

                if (a.Length == 0)
                {
                    try
                    {
                        sql = "ALTER TABLE izdatnica_stavke add column id_izdatnica integer NOT NULL;";
                        classSQL.select(sql, "izdatnica_stavke");
                    }
                    catch
                    {
                        //već postoji
                    }
                }

                a = cols.Select("table_name = 'izdatnica_stavke' and column_name = 'ukupno'");

                if (a.Length == 0)
                {
                    try
                    {
                        sql = "ALTER TABLE izdatnica_stavke add column ukupno numeric;";
                        classSQL.select(sql, "izdatnica_stavke");
                    }
                    catch
                    {
                        //već postoji
                    }
                }

                a = cols.Select("table_name = 'izdatnica_stavke' and column_name = 'iznos'");

                if (a.Length == 0)
                {
                    try
                    {
                        sql = "ALTER TABLE izdatnica_stavke add column iznos numeric;";
                        classSQL.select(sql, "izdatnica_stavke");
                    }
                    catch
                    {
                        //već postoji
                    }
                }

                a = cols.Select("table_name = 'izdatnica_stavke' and column_name = 'vpc'");

                if (a[0][2].ToString() != "numeric")
                {
                    try
                    {
                        sql = "ALTER TABLE izdatnica_stavke alter column vpc numeric;";
                        classSQL.select(sql, "izdatnica_stavke");
                    }
                    catch
                    {
                        //već postoji
                    }
                }

                a = cols.Select("table_name = 'izdatnica_stavke' and column_name = 'mpc'");

                if (a[0][2].ToString() != "numeric")
                {
                    try
                    {
                        sql = "ALTER TABLE izdatnica_stavke alter column mpc numeric;";
                        classSQL.select(sql, "izdatnica_stavke");
                    }
                    catch
                    {
                        //već postoji
                    }
                }

                a = cols.Select("table_name = 'izdatnica_stavke' and column_name = 'nbc'");

                if (a[0][2].ToString() != "numeric")
                {
                    try
                    {
                        sql = "ALTER TABLE izdatnica_stavke alter column nbc numeric;";
                        classSQL.select(sql, "izdatnica_stavke");
                    }
                    catch
                    {
                        //već postoji
                    }
                }

                a = cols.Select("table_name = 'izdatnica_stavke' and column_name = 'id_primka'");

                if (a.Length == 1)
                {
                    try
                    {
                        sql = "ALTER TABLE izdatnica_stavke drop column id_primka;";
                        classSQL.select(sql, "izdatnica_stavke");
                    }
                    catch
                    {
                        //već postoji
                    }
                }
            }
        }

        private static void AlterGrad(DataTable cols)
        {
            string sql;
            bool apdejtano = false;

            DataRow[] a = cols.Select("table_name = 'grad' and column_name = 'drzava'");

            if (a.Length == 0)
            {
                try
                {
                    sql = "ALTER TABLE grad add column drzava integer;";
                    classSQL.select(sql, "grad");
                    apdejtano = true;
                }
                catch
                {
                    apdejtano = false;
                }
            }
            if (apdejtano)
            {
                DataTable dtZemlja = classSQL.select("SELECT id_zemlja FROM zemlja WHERE zemlja='Croatia'", "zemlja").Tables[0];
                if (dtZemlja.Rows.Count > 0)
                {
                    sql = "UPDATE grad SET drzava='" + dtZemlja.Rows[0][0].ToString() + "'";
                }
                else
                {
                    sql = "UPDATE grad SET drzava='1'";
                }
                classSQL.update(sql);
            }

            a = cols.Select("table_name = 'grad' and column_name = 'grad'");

            if (a.Length == 1)
            {
                if (a[0][3].ToString() == "200")
                    return;
                try
                {
                    sql = "ALTER TABLE grad ALTER COLUMN grad nvarchar(200); ";
                    classSQL.select_settings(sql, "grad");
                    sql = "ALTER TABLE grad ALTER COLUMN posta nvarchar(100); ";
                    classSQL.select_settings(sql, "grad");
                }
                catch
                {
                }
            }
        }

        private static void AlterKartoteka(DataTable DTremote, DataTable cols)
        {
            string sql;
            DataRow[] dataROW = DTremote.Select("table_name = 'KAR_kronologija'");
            if (dataROW.Length == 0)
            {
                sql = "CREATE TABLE KAR_kronologija" +
                    "( broj bigint," +
                     "id serial NOT NULL," +
                     "id_partner bigint," +
                     "datum timestamp without time zone," +
                     "napomena character varying," +
                     "broj_racuna character varying," +
                     " CONSTRAINT KAR_kronologija_pkey PRIMARY KEY (id)" +
                     " )";

                classSQL.select(sql, "KAR_kronologija");
            }

            DataRow[] a = cols.Select("table_name = 'partners' and column_name = 'godina_cestitke'");

            if (a.Length == 0)
            {
                sql = "ALTER TABLE partners ADD COLUMN godina_cestitke bigint;";
                classSQL.insert(sql);
            }

            dataROW = DTremote.Select("table_name = 'KAR_podsjetnik'");
            if (dataROW.Length == 0)
            {
                sql = "CREATE TABLE KAR_podsjetnik" +
                    "( id_partner bigint," +
                     " id serial NOT NULL," +
                     " ime_partnera character varying," +
                     " prezime_partnera character varying," +
                     " dodatni_podatak1 character varying," +
                     " dodatni_podatak2 character varying," +
                     " datum timestamp without time zone," +
                     " napomena character varying," +
                     " email_klijenta character varying," +
                     " obavijest_ekran boolean DEFAULT false," +
                     " rodendan_bool boolean DEFAULT false," +
                     " id_zaposlenik character varying," +
                     " CONSTRAINT KAR_podsjetnik_pkey PRIMARY KEY (id)" +
                     " )";
                classSQL.select(sql, "id_partner");
            }
            else
            {
                a = cols.Select("table_name = 'kar_podsjetnik' and column_name = 'id_zaposlenik'");

                if (a.Length == 0)
                {
                    try
                    {
                        sql = "ALTER TABLE kar_podsjetnik ADD COLUMN id_zaposlenik character varying;";
                        classSQL.select(sql, "kar_podsjetnik");
                    }
                    catch
                    {
                    }
                }
            }
        }

        private static void AlterGrad(DataTable DTremote, DataTable cols)
        {
            string sql;

            DataRow[] dataROW = cols.Select("table_name = 'grad' AND column_name='prirez'");
            if (dataROW.Length == 0)
            {
                sql = "ALTER TABLE grad ADD COLUMN prirez numeric DEFAULT 0;";
                classSQL.select(sql, "kar_podsjetnik");
            }
        }

        public static void ProvjeraViewDB(DataTable cols)
        {
            #region ulaz_izlaz_robe UZME SVE VRIJEDNOSTI IZ BAZE

            DataTable DTindex = classSQL.select("SELECT * FROM pg_class c, pg_namespace n WHERE c.relnamespace = n.oid AND relname = 'index_roba_prodaja' AND relkind = 'i';", "index").Tables[0];
            if (DTindex.Rows.Count == 0)
            {
                frmPostavke p = new frmPostavke();
                p.btnBrisiIsteArtikle_Click(null, null);
            }

            DataRow[] a = cols.Select("table_name = 'ulaz_izlaz_robe'");
            DataRow[] b = cols.Select("table_name = 'ulaz_izlaz_robe_financijski'");

            //if (b.Length == 0)
            {
                string query = @"DROP VIEW IF EXISTS ulaz_izlaz_robe_financijski;
                        CREATE OR REPLACE VIEW ulaz_izlaz_robe_financijski AS
                        SELECT * FROM
                        (
	                        /*RACUNI*/
	                        SELECT racun_stavke.id_stavka as id,
                            CAST(racuni.broj_racuna as BIGINT) as broj,
                            datum_racuna as datum,
                            racun_stavke.sifra_robe AS sifra,
                            racun_stavke.id_skladiste as skladiste,
                            COALESCE(CAST(REPLACE(racun_stavke.kolicina,',','.') as NUMERIC),0) AS kolicina,
	                        CAST(racun_stavke.nbc as numeric) as nbc,
                            CAST(REPLACE(racun_stavke.porez,',','.') as numeric) as porez,
	                        CAST(racun_stavke.mpc as numeric)/(1+(CAST(REPLACE(racun_stavke.porez,',','.') as numeric)/100)) as vpc,
                            CAST(REPLACE(racun_stavke.rabat,',','.') as numeric) as rabat,
                            'maloprodaja' as doc,
                            'izlaz' as ui,
                            roba.oduzmi as oduzmi
	                        FROM racun_stavke
                            LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa
                            LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe
	                        UNION ALL

	                        /*KALKULACIJE*/
	                        SELECT kalkulacija_stavke.id_stavka as id,CAST(kalkulacija.broj as BIGINT) as broj, racun_datum,sifra AS sifra,kalkulacija.id_skladiste as skladiste,COALESCE(CAST(REPLACE(kolicina,',','.') as NUMERIC),0) AS kolicina,
	                        ROUND(
	                        COALESCE(
					            (kalkulacija_stavke.fak_cijena-
					            (kalkulacija_stavke.fak_cijena*CAST(REPLACE(kalkulacija_stavke.rabat,',','.') AS numeric)/100))+
					            COALESCE((kalkulacija_stavke.prijevoz),0)
					            ,0)
				            ,4) as nbc,
	                        CAST(REPLACE(porez,',','.') as numeric) as porez,
	                        vpc,CAST('0' as numeric) as rabat,'kalkulacija' as doc,'ulaz' as ui,'DA' as oduzmi
	                        FROM kalkulacija_stavke LEFT JOIN kalkulacija ON kalkulacija.broj=kalkulacija_stavke.broj AND kalkulacija.id_skladiste=kalkulacija_stavke.id_skladiste
	                        UNION ALL

	                        /*IZDATNICE*/
	                        SELECT izdatnica_stavke.id_stavka as id,izdatnica.broj,datum,sifra AS sifra,id_skladiste as skladiste,COALESCE(CAST(REPLACE(kolicina,',','.') as NUMERIC),0) AS kolicina,
	                        nbc,CAST(REPLACE(pdv,',','.') as numeric) as porez,vpc,CAST(REPLACE(rabat,',','.') as numeric) as rabat,'izdatnica' as doc ,'izlaz' as ui ,'DA' as oduzmi
	                        FROM izdatnica_stavke LEFT JOIN izdatnica ON izdatnica.id_izdatnica=izdatnica_stavke.id_izdatnica
	                        UNION ALL

	                        --PRIMKE
	                        SELECT primka_stavke.id_stavka as id,primka.broj,datum,sifra AS sifra,id_skladiste as skladiste,COALESCE(CAST(REPLACE(kolicina,',','.') as NUMERIC),0) AS kolicina,
	                        nbc,CAST(REPLACE(pdv,',','.') as numeric) as porez,vpc,CAST('0' as numeric) as rabat,'primka' as doc,'ulaz' as ui,'DA' as oduzmi
	                        FROM primka_stavke LEFT JOIN primka ON primka.id_primka=primka_stavke.id_primka
	                        UNION ALL

	                        --FAKTURE
	                        SELECT faktura_stavke.id_stavka as id,fakture.broj_fakture as broj,date as datum, faktura_stavke.sifra AS sifra,id_skladiste as skladiste,COALESCE(CAST(REPLACE(faktura_stavke.kolicina,',','.') as NUMERIC),0) AS kolicina,
                            CAST(nbc as numeric) as nbc,CAST(REPLACE(faktura_stavke.porez,',','.') as numeric) as porez,faktura_stavke.vpc,CAST(REPLACE(faktura_stavke.rabat,',','.') as numeric) as rabat,'fakture' as doc,'izlaz' as ui,roba.oduzmi
	                        FROM faktura_stavke
                            LEFT JOIN fakture ON fakture.broj_fakture = faktura_stavke.broj_fakture AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa
                            LEFT JOIN roba ON roba.sifra=faktura_stavke.sifra
	                        UNION ALL

	                        --MEĐUSKLADIŠNICA IZ SKLADIŠTA
	                        SELECT meduskladisnica_stavke.id_stavka as id,CAST(meduskladisnica.broj as BIGINT),datum,sifra AS sifra,meduskladisnica.id_skladiste_od as skladiste,COALESCE(CAST(REPLACE(kolicina,',','.') as NUMERIC),0) AS kolicina,
	                        CAST(nbc as numeric) as nbc,CAST(REPLACE(pdv,',','.') as numeric) as porez, CAST(vpc as numeric) as vpc,CAST('0' as numeric) as rabat,'iz_skl' as doc,'izlaz' as ui,'DA' as oduzmi
	                        FROM meduskladisnica_stavke LEFT JOIN meduskladisnica ON meduskladisnica.broj=meduskladisnica_stavke.broj AND meduskladisnica.id_skladiste_od=meduskladisnica_stavke.iz_skladista
	                        UNION ALL

	                        --MEĐUSKLADIŠNICA U SKLADIŠTE
	                        SELECT meduskladisnica_stavke.id_stavka as id,CAST(meduskladisnica.broj as BIGINT),datum,sifra AS sifra,meduskladisnica.id_skladiste_do as skladiste,COALESCE(CAST(REPLACE(kolicina,',','.') as NUMERIC),0) AS kolicina,
	                        CAST(nbc as numeric) as nbc,CAST(REPLACE(pdv,',','.') as numeric) as porez, CAST(vpc as numeric) as vpc,CAST('0' as numeric) as rabat,'u_skl' as doc,'ulaz' as ui,'DA' as oduzmi
	                        FROM meduskladisnica_stavke LEFT JOIN meduskladisnica ON meduskladisnica.broj=meduskladisnica_stavke.broj AND meduskladisnica.id_skladiste_od=meduskladisnica_stavke.iz_skladista
	                        UNION ALL

	                        --OTPREMNICA
	                        SELECT otpremnica_stavke.id_stavka as id,otpremnice.broj_otpremnice as broj,datum,sifra_robe AS sifra,otpremnice.id_skladiste as skladiste, COALESCE(CAST(REPLACE(kolicina,',','.') as NUMERIC),0) AS kolicina,
	                        CAST(nbc as numeric) as nbc,CAST(REPLACE(porez,',','.') as numeric) as porez, vpc,CAST(REPLACE(rabat,',','.') as numeric) as rabat,'otpremnica' as doc,'izlaz' as ui, otpremnica_stavke.oduzmi as oduzmi
	                        FROM otpremnica_stavke
	                        LEFT JOIN otpremnice ON otpremnica_stavke.broj_otpremnice=otpremnice.broj_otpremnice AND otpremnica_stavke.id_skladiste=otpremnice.id_skladiste
	                        UNION ALL

	                        --OTPIS ROBE
	                        SELECT otpis_robe_stavke.id_stavka as id,otpis_robe.broj as broj,datum,sifra AS sifra,id_skladiste as skladiste,COALESCE(CAST(REPLACE(kolicina,',','.') as NUMERIC),0) AS kolicina,
	                        CAST(nbc as numeric) as nbc,CAST(REPLACE(pdv,',','.') as numeric) as porez, vpc,CAST(REPLACE(rabat,',','.') as numeric) as rabat,'otpis_robe' as doc,'izlaz' as ui,'DA' as oduzmi
	                        FROM otpis_robe_stavke LEFT JOIN otpis_robe ON otpis_robe.broj=otpis_robe_stavke.broj
	                        UNION ALL

	                        --POVRAT ROBE
	                        SELECT povrat_robe_stavke.id_stavka as id,povrat_robe.broj,datum,sifra AS sifra,id_skladiste as skladiste,COALESCE(CAST(REPLACE(kolicina,',','.') as NUMERIC),0) AS kolicina,
	                        CAST(nbc as numeric) as nbc,CAST(REPLACE(pdv,',','.') as numeric) as porez, vpc,CAST(REPLACE(rabat,',','.') as numeric) as rabat,'povrat_robe' as doc,'izlaz' as ui,'DA' as oduzmi
	                        FROM povrat_robe_stavke LEFT JOIN povrat_robe ON povrat_robe.broj=povrat_robe_stavke.broj
	                        UNION ALL

	                        --ZAPISNIK O PROMJENI CIJENE
	                        SELECT promjena_cijene_stavke.id_stavka as id,
                            promjena_cijene.broj,
                            date as datum,
                            sifra AS sifra,
                            id_skladiste as skladiste,
                            COALESCE(CAST(REPLACE(kolicina,',','.') as NUMERIC),0) AS kolicina,
                            CAST('0' as numeric) as nbc,
                            CAST(REPLACE(pdv,',','.') as numeric) as porez,
                            (promjena_cijene_stavke.nova_cijena - promjena_cijene_stavke.stara_cijena)::numeric / (1::numeric + replace(promjena_cijene_stavke.pdv::text, ','::text, '.'::text)::numeric / 100::numeric)::numeric AS vpc,
                            CAST('0' as numeric) as rabat,
                            'promjena_cijene' as doc,
                            'ulaz' as ui,
                            'DA' as oduzmi
	                        FROM promjena_cijene_stavke LEFT JOIN promjena_cijene ON promjena_cijene.broj=promjena_cijene_stavke.broj
	                        UNION ALL

	                        --INVENTURA
	                        SELECT inventura_stavke.id_stavke as id,CAST(inventura.broj_inventure AS BIGINT) as broj,datum,sifra_robe AS sifra,id_skladiste as skladiste,COALESCE(CAST(REPLACE(COALESCE(kolicina,'0'),',','.') as NUMERIC),0)-inventura_stavke.kolicina_koja_je_bila AS kolicina,
	                        nbc,CAST(REPLACE(COALESCE(porez,'0'),',','.') as numeric) as porez,
	                        cijena/(1+(CAST(REPLACE(COALESCE(porez,'0'),',','.') as numeric)/100)) as vpc,
	                        CAST('0' as numeric) as rabat,'inventura' as doc,'ulaz' as ui,'DA' as oduzmi
	                        FROM inventura_stavke LEFT JOIN inventura ON inventura.broj_inventure=inventura_stavke.broj_inventure
	                        WHERE inventura.pocetno_stanje != '1'
	                        UNION ALL

	                        --POCETNO
	                        SELECT pocetno.id as id,CAST('0' as BIGINT) as broj,datum,sifra AS sifra,id_skladiste as skladiste,COALESCE(CAST(REPLACE(kolicina,',','.') as NUMERIC),0) AS kolicina,
	                        nbc,CAST(porez as numeric) as porez, prodajna_cijena/(1+(porez::numeric/100)) as vpc,CAST('0' as numeric) as rabat,'pocetno' as doc,'ulaz' as ui,'DA' as oduzmi  FROM pocetno
	                        UNION ALL

                            /*RADNI NALOG STAVKE koji dodaje na skladište kad se radni nalog izradi*/
                            SELECT radni_nalog_stavke.id_stavka as id,radni_nalog.broj_naloga as broj,datum_naloga as datum,sifra_robe AS sifra,radni_nalog_stavke.id_skladiste as skladiste,COALESCE(CAST(REPLACE(radni_nalog_stavke.kolicina,',','.') as NUMERIC),0) AS kolicina,
                            CAST(radni_nalog_stavke.nbc as numeric),CAST(REPLACE(radni_nalog_stavke.porez,',','.') as numeric) as porez, CAST(radni_nalog_stavke.vpc as numeric) as vpc,CAST('0' as numeric) as rabat,'radni_nalog_stavke' as doc,'ulaz' as ui,roba.oduzmi as oduzmi FROM radni_nalog_stavke
                            LEFT JOIN radni_nalog ON radni_nalog.broj_naloga=radni_nalog_stavke.broj_naloga
                            LEFT JOIN roba ON roba.sifra=radni_nalog_stavke.sifra_robe
	                        UNION ALL

                            /*RADNI NALOG STAVKE-->NORMATIVI*/
                            SELECT  normativi_stavke.id_stavka as id,radni_nalog.broj_naloga as broj,
                            radni_nalog.datum_naloga as datum,normativi_stavke.sifra_robe AS sifra,normativi_stavke.id_skladiste as skladiste,
	                        COALESCE(CAST(replace(normativi_stavke.kolicina, ',', '.') as numeric),0) * COALESCE(CAST(replace(radni_nalog_stavke.kolicina, ',', '.') as numeric),0) AS kolicina,
	                        CAST(roba_prodaja.nc as numeric) as nbc,CAST(roba_prodaja.porez as numeric) as porez, roba_prodaja.vpc,CAST('0' as numeric) as rabat,'radni_nalog_stavke_normativi' as doc ,'izlaz' as ui,roba.oduzmi as oduzmi
                            FROM normativi_stavke
                            LEFT JOIN normativi ON normativi.broj_normativa = normativi_stavke.broj_normativa
                            LEFT JOIN radni_nalog_stavke ON normativi.sifra_artikla=radni_nalog_stavke.sifra_robe
                            LEFT JOIN radni_nalog ON radni_nalog.broj_naloga=radni_nalog_stavke.broj_naloga
                            LEFT JOIN roba_prodaja ON roba_prodaja.sifra = normativi_stavke.sifra_robe AND roba_prodaja.id_skladiste = normativi_stavke.id_skladiste
                            LEFT JOIN roba ON roba.sifra=normativi_stavke.sifra_robe
                            UNION ALL

                            /*NORMATIVI (NA USLUGU SE DODAJU PRODAJNI ARTIKLI)*/
	                        SELECT normativi_stavke.id_stavka as id,CAST(racuni.broj_racuna as BIGINT) as broj,
	                        racuni.datum_racuna as datum,normativi_stavke.sifra_robe AS sifra,normativi_stavke.id_skladiste as skladiste,
                            COALESCE(CAST(REPLACE(normativi_stavke.kolicina,',','.') as NUMERIC),0)* COALESCE(CAST(REPLACE(racun_stavke.kolicina,',','.') as NUMERIC),0) as kolicina,
                            CAST(roba_prodaja.nc as numeric) as nbc, CAST(roba_prodaja.porez as numeric) as porez, roba_prodaja.vpc,CAST('0' as numeric) as rabat,'radni_nalog_skida_normative_prema_uslugi' as doc,'izlaz' as ui,roba.oduzmi as oduzmi
                            FROM normativi_stavke
                            LEFT JOIN normativi ON normativi.broj_normativa = normativi_stavke.broj_normativa
                            LEFT JOIN racun_stavke ON normativi.sifra_artikla=racun_stavke.sifra_robe
                            LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa
                            LEFT JOIN roba_prodaja ON roba_prodaja.sifra = normativi_stavke.sifra_robe AND roba_prodaja.id_skladiste = normativi_stavke.id_skladiste
                            LEFT JOIN roba ON roba.sifra=normativi.sifra_artikla
                        )
                        ulaz_izlaz_robe_financijski;";

                query = @"CREATE OR REPLACE VIEW ulaz_izlaz_robe_financijski AS
 SELECT ulaz_izlaz_robe_financijski.id, ulaz_izlaz_robe_financijski.broj,
    ulaz_izlaz_robe_financijski.datum, ulaz_izlaz_robe_financijski.sifra,
    ulaz_izlaz_robe_financijski.skladiste, ulaz_izlaz_robe_financijski.kolicina,
    ulaz_izlaz_robe_financijski.nbc, ulaz_izlaz_robe_financijski.porez,
    case when skladiste.samo_nbc = true then ulaz_izlaz_robe_financijski.vpc else ulaz_izlaz_robe_financijski.vpc end as vpc, ulaz_izlaz_robe_financijski.rabat,
    ulaz_izlaz_robe_financijski.doc, ulaz_izlaz_robe_financijski.ui,
    ulaz_izlaz_robe_financijski.oduzmi
   FROM (        (        (        (        (        (        (        (        (        (        (        (        (        (        (         SELECT racun_stavke.id_stavka AS id,
                                                                                                                                    racuni.broj_racuna::bigint AS broj,
                                                                                                                                    racuni.datum_racuna AS datum,
                                                                                                                                    racun_stavke.sifra_robe AS sifra,
                                                                                                                                    racun_stavke.id_skladiste AS skladiste,
                                                                                                                                    COALESCE(replace(racun_stavke.kolicina::text, ','::text, '.'::text)::numeric, 0::numeric) AS kolicina,
                                                                                                                                    racun_stavke.nbc::numeric AS nbc,
                                                                                                                                    replace(racun_stavke.porez::text, ','::text, '.'::text)::numeric AS porez,
                                                                                                                                    racun_stavke.mpc::numeric / (1::numeric + replace(racun_stavke.porez::text, ','::text, '.'::text)::numeric / 100::numeric) AS vpc,
                                                                                                                                    replace(racun_stavke.rabat::text, ','::text, '.'::text)::numeric AS rabat,
                                                                                                                                    'maloprodaja'::text AS doc,
                                                                                                                                    'izlaz'::text AS ui,
                                                                                                                                    roba.oduzmi
                                                                                                                                   FROM racun_stavke
                                                                                                                              LEFT JOIN racuni ON racuni.broj_racuna::text = racun_stavke.broj_racuna::text AND racuni.id_ducan = racun_stavke.id_ducan AND racuni.id_kasa = racun_stavke.id_kasa
                                                                                                                         LEFT JOIN roba ON roba.sifra::text = racun_stavke.sifra_robe::text
                                                                                                                        UNION ALL
                                                                                                                                 SELECT kalkulacija_stavke.id_stavka AS id,
                                                                                                                                    kalkulacija.broj,
                                                                                                                                    kalkulacija.racun_datum,
                                                                                                                                    kalkulacija_stavke.sifra,
                                                                                                                                    kalkulacija.id_skladiste AS skladiste,
                                                                                                                                    COALESCE(replace(kalkulacija_stavke.kolicina::text, ','::text, '.'::text)::numeric, 0::numeric) AS kolicina,
                                                                                                                                    round(COALESCE(kalkulacija_stavke.fak_cijena - kalkulacija_stavke.fak_cijena * replace(kalkulacija_stavke.rabat::text, ','::text, '.'::text)::numeric / 100::numeric + COALESCE(kalkulacija_stavke.prijevoz, 0::numeric), 0::numeric), 4) AS nbc,
                                                                                                                                    replace(kalkulacija_stavke.porez::text, ','::text, '.'::text)::numeric AS porez,
                                                                                                                                    kalkulacija_stavke.vpc,
                                                                                                                                    0::numeric AS rabat,
                                                                                                                                    'kalkulacija'::text AS doc,
                                                                                                                                    'ulaz'::text AS ui,
                                                                                                                                    'DA'::bpchar AS oduzmi
                                                                                                                                   FROM kalkulacija_stavke
                                                                                                                              LEFT JOIN kalkulacija ON kalkulacija.broj = kalkulacija_stavke.broj AND kalkulacija.id_skladiste = kalkulacija_stavke.id_skladiste)
                                                                                                                UNION ALL
                                                                                                                         SELECT izdatnica_stavke.id_stavka AS id,
                                                                                                                            izdatnica.broj,
                                                                                                                            izdatnica.datum,
                                                                                                                            izdatnica_stavke.sifra,
                                                                                                                            izdatnica.id_skladiste AS skladiste,
                                                                                                                            COALESCE(replace(izdatnica_stavke.kolicina::text, ','::text, '.'::text)::numeric, 0::numeric) AS kolicina,
                                                                                                                            izdatnica_stavke.nbc,
                                                                                                                            replace(izdatnica_stavke.pdv::text, ','::text, '.'::text)::numeric AS porez,
                                                                                                                            izdatnica_stavke.vpc,
                                                                                                                            replace(izdatnica_stavke.rabat::text, ','::text, '.'::text)::numeric AS rabat,
                                                                                                                            'izdatnica'::text AS doc,
                                                                                                                            'izlaz'::text AS ui,
                                                                                                                            'DA'::bpchar AS oduzmi
                                                                                                                           FROM izdatnica_stavke
                                                                                                                      LEFT JOIN izdatnica ON izdatnica.id_izdatnica = izdatnica_stavke.id_izdatnica)
                                                                                                        UNION ALL
                                                                                                                 SELECT primka_stavke.id_stavka AS id,
                                                                                                                    primka.broj,
                                                                                                                    primka.datum,
                                                                                                                    primka_stavke.sifra,
                                                                                                                    primka.id_skladiste AS skladiste,
                                                                                                                    COALESCE(replace(primka_stavke.kolicina::text, ','::text, '.'::text)::numeric, 0::numeric) AS kolicina,
                                                                                                                    primka_stavke.nbc,
                                                                                                                    replace(primka_stavke.pdv::text, ','::text, '.'::text)::numeric AS porez,
                                                                                                                    primka_stavke.vpc,
                                                                                                                    0::numeric AS rabat,
                                                                                                                    'primka'::text AS doc,
                                                                                                                    'ulaz'::text AS ui,
                                                                                                                    'DA'::bpchar AS oduzmi
                                                                                                                   FROM primka_stavke
                                                                                                              LEFT JOIN primka ON primka.id_primka = primka_stavke.id_primka)
                                                                                                UNION ALL
                                                                                                         SELECT faktura_stavke.id_stavka AS id,
                                                                                                            fakture.broj_fakture AS broj,
                                                                                                            fakture.datedvo AS datum,
                                                                                                            faktura_stavke.sifra,
                                                                                                            faktura_stavke.id_skladiste AS skladiste,
                                                                                                            COALESCE(replace(faktura_stavke.kolicina::text, ','::text, '.'::text)::numeric, 0::numeric) AS kolicina,
                                                                                                            faktura_stavke.nbc::numeric AS nbc,
                                                                                                            replace(faktura_stavke.porez::text, ','::text, '.'::text)::numeric AS porez,
                                                                                                            faktura_stavke.vpc,
                                                                                                            replace(faktura_stavke.rabat::text, ','::text, '.'::text)::numeric AS rabat,
                                                                                                            'fakture'::text AS doc,
                                                                                                            'izlaz'::text AS ui,
                                                                                                            roba.oduzmi
                                                                                                           FROM faktura_stavke
                                                                                                      LEFT JOIN fakture ON fakture.broj_fakture = faktura_stavke.broj_fakture AND fakture.id_ducan = faktura_stavke.id_ducan AND fakture.id_kasa = faktura_stavke.id_kasa
                                                                                                 LEFT JOIN roba ON roba.sifra::text = faktura_stavke.sifra::text)
                                                                                        UNION ALL
                                                                                                 SELECT meduskladisnica_stavke.id_stavka AS id,
                                                                                                    meduskladisnica.broj::bigint AS broj,
                                                                                                    meduskladisnica.datum,
                                                                                                    meduskladisnica_stavke.sifra,
                                                                                                    meduskladisnica.id_skladiste_od AS skladiste,
                                                                                                    COALESCE(replace(meduskladisnica_stavke.kolicina::text, ','::text, '.'::text)::numeric, 0::numeric) AS kolicina,
                                                                                                    meduskladisnica_stavke.nbc::numeric AS nbc,
                                                                                                    replace(meduskladisnica_stavke.pdv::text, ','::text, '.'::text)::numeric AS porez,
                                                                                                    meduskladisnica_stavke.vpc::numeric AS vpc,
                                                                                                    0::numeric AS rabat,
                                                                                                    'iz_skl'::text AS doc,
                                                                                                    'izlaz'::text AS ui,
                                                                                                    'DA'::bpchar AS oduzmi
                                                                                                   FROM meduskladisnica_stavke
                                                                                              LEFT JOIN meduskladisnica ON meduskladisnica.broj::text = meduskladisnica_stavke.broj::text AND meduskladisnica.id_skladiste_od = meduskladisnica_stavke.iz_skladista)
                                                                                UNION ALL
                                                                                         SELECT meduskladisnica_stavke.id_stavka AS id,
                                                                                            meduskladisnica.broj::bigint AS broj,
                                                                                            meduskladisnica.datum,
                                                                                            meduskladisnica_stavke.sifra,
                                                                                            meduskladisnica.id_skladiste_do AS skladiste,
                                                                                            COALESCE(replace(meduskladisnica_stavke.kolicina::text, ','::text, '.'::text)::numeric, 0::numeric) AS kolicina,
                                                                                            meduskladisnica_stavke.nbc::numeric AS nbc,
                                                                                            replace(meduskladisnica_stavke.pdv::text, ','::text, '.'::text)::numeric AS porez,
                                                                                            meduskladisnica_stavke.vpc::numeric AS vpc,
                                                                                            0::numeric AS rabat,
                                                                                            'u_skl'::text AS doc,
                                                                                            'ulaz'::text AS ui,
                                                                                            'DA'::bpchar AS oduzmi
                                                                                           FROM meduskladisnica_stavke
                                                                                      LEFT JOIN meduskladisnica ON meduskladisnica.broj::text = meduskladisnica_stavke.broj::text AND meduskladisnica.id_skladiste_od = meduskladisnica_stavke.iz_skladista)
                                                                        UNION ALL
                                                                                 SELECT otpremnica_stavke.id_stavka AS id,
                                                                                    otpremnice.broj_otpremnice AS broj,
                                                                                    otpremnice.datum,
                                                                                    otpremnica_stavke.sifra_robe AS sifra,
                                                                                    otpremnice.id_skladiste AS skladiste,
                                                                                    COALESCE(replace(otpremnica_stavke.kolicina::text, ','::text, '.'::text)::numeric, 0::numeric) AS kolicina,
                                                                                    otpremnica_stavke.nbc::numeric AS nbc,
                                                                                    replace(otpremnica_stavke.porez::text, ','::text, '.'::text)::numeric AS porez,
                                                                                    otpremnica_stavke.vpc,
                                                                                    replace(otpremnica_stavke.rabat::text, ','::text, '.'::text)::numeric AS rabat,
                                                                                    'otpremnica'::text AS doc,
                                                                                    'izlaz'::text AS ui,
                                                                                    otpremnica_stavke.oduzmi AS oduzmi
                                                                                   FROM otpremnica_stavke
                                                                              LEFT JOIN otpremnice ON otpremnica_stavke.broj_otpremnice = otpremnice.broj_otpremnice AND otpremnica_stavke.id_skladiste = otpremnice.id_skladiste)
                                                                UNION ALL
                                                                         SELECT otpis_robe_stavke.id_stavka AS id,
                                                                            otpis_robe.broj,
                                                                            otpis_robe.datum,
                                                                            otpis_robe_stavke.sifra,
                                                                            otpis_robe.id_skladiste AS skladiste,
                                                                            COALESCE(replace(otpis_robe_stavke.kolicina::text, ','::text, '.'::text)::numeric, 0::numeric) AS kolicina,
                                                                            otpis_robe_stavke.nbc::numeric AS nbc,
                                                                            replace(otpis_robe_stavke.pdv::text, ','::text, '.'::text)::numeric AS porez,
                                                                            otpis_robe_stavke.vpc,
                                                                            replace(otpis_robe_stavke.rabat::text, ','::text, '.'::text)::numeric AS rabat,
                                                                            'otpis_robe'::text AS doc,
                                                                            'izlaz'::text AS ui,
                                                                            'DA'::bpchar AS oduzmi
                                                                           FROM otpis_robe_stavke
                                                                      LEFT JOIN otpis_robe ON otpis_robe.broj = otpis_robe_stavke.broj)
                                                        UNION ALL
                                                                 SELECT povrat_robe_stavke.id_stavka AS id,
                                                                    povrat_robe.broj,
                                                                    povrat_robe.datum,
                                                                    povrat_robe_stavke.sifra,
                                                                    povrat_robe.id_skladiste AS skladiste,
                                                                    COALESCE(replace(povrat_robe_stavke.kolicina::text, ','::text, '.'::text)::numeric, 0::numeric) AS kolicina,
                                                                    povrat_robe_stavke.nbc::numeric * (-1)::numeric AS nbc,
                                                                    replace(povrat_robe_stavke.pdv::text, ','::text, '.'::text)::numeric AS porez,
                                                                    povrat_robe_stavke.vpc * (-1)::numeric AS vpc,
                                                                    replace(povrat_robe_stavke.rabat::text, ','::text, '.'::text)::numeric AS rabat,
                                                                    'povrat_robe'::text AS doc,
                                                                    'ulaz'::text AS ui,
                                                                    'DA'::bpchar AS oduzmi
                                                                   FROM povrat_robe_stavke
                                                              LEFT JOIN povrat_robe ON povrat_robe.broj = povrat_robe_stavke.broj)
                                                UNION ALL
                                                         SELECT promjena_cijene_stavke.id_stavka AS id,
                                                            promjena_cijene.broj,
                                                            promjena_cijene.date AS datum,
                                                            promjena_cijene_stavke.sifra,
                                                            promjena_cijene.id_skladiste AS skladiste,
                                                            COALESCE(replace(promjena_cijene_stavke.kolicina::text, ','::text, '.'::text)::numeric, 0::numeric) AS kolicina,
                                                            0::numeric AS nbc,
                                                            replace(promjena_cijene_stavke.pdv::text, ','::text, '.'::text)::numeric AS porez,
                                                                CASE
                                                                    WHEN promjena_cijene_stavke.stara_cijena::numeric < promjena_cijene_stavke.nova_cijena::numeric THEN (promjena_cijene_stavke.nova_cijena - promjena_cijene_stavke.stara_cijena)::numeric / (1::numeric + replace(promjena_cijene_stavke.pdv::text, ','::text, '.'::text)::numeric / 100::numeric)
                                                                    ELSE (promjena_cijene_stavke.stara_cijena - promjena_cijene_stavke.nova_cijena)::numeric / (1::numeric + replace(promjena_cijene_stavke.pdv::text, ','::text, '.'::text)::numeric / 100::numeric) * (-1)::numeric
                                                                END AS vpc,
                                                            0::numeric AS rabat,
                                                            'promjena_cijene'::text AS doc,
                                                            'ulaz'::text AS ui,
                                                            'DA'::bpchar AS oduzmi
                                                           FROM promjena_cijene_stavke
                                                      LEFT JOIN promjena_cijene ON promjena_cijene.broj = promjena_cijene_stavke.broj)
                                        UNION ALL
                                                 SELECT inventura_stavke.id_stavke AS id,
                                                    inventura.broj_inventure::bigint AS broj,
                                                    inventura.datum,
                                                    inventura_stavke.sifra_robe AS sifra,
                                                    inventura.id_skladiste AS skladiste,
                                                    COALESCE(replace(COALESCE(inventura_stavke.kolicina, '0'::character varying)::text, ','::text, '.'::text)::numeric, 0::numeric) - inventura_stavke.kolicina_koja_je_bila AS kolicina,
                                                    inventura_stavke.nbc,
                                                    replace(COALESCE(inventura_stavke.porez, '0'::character varying)::text, ','::text, '.'::text)::numeric AS porez,
                                                    inventura_stavke.cijena / (1::numeric + replace(COALESCE(inventura_stavke.porez, '0'::character varying)::text, ','::text, '.'::text)::numeric / 100::numeric) AS vpc,
                                                    0::numeric AS rabat,
                                                    'inventura'::text AS doc,
                                                    'ulaz'::text AS ui,
                                                    'DA'::bpchar AS oduzmi
                                                   FROM inventura_stavke
                                              LEFT JOIN inventura ON inventura.broj_inventure::text = inventura_stavke.broj_inventure::text
                                             WHERE inventura.pocetno_stanje != 1)
                                UNION ALL
                                         SELECT pocetno.id, 0::bigint AS broj,
                                            pocetno.datum, pocetno.sifra,
                                            pocetno.id_skladiste AS skladiste,
                                            COALESCE(replace(pocetno.kolicina::text, ','::text, '.'::text)::numeric, 0::numeric) AS kolicina,
                                            pocetno.nbc,
                                            pocetno.porez::numeric AS porez,
                                            pocetno.prodajna_cijena / (1::numeric + pocetno.porez::numeric / 100::numeric) AS vpc,
                                            0::numeric AS rabat,
                                            'pocetno'::text AS doc,
                                            'ulaz'::text AS ui,
                                            'DA'::bpchar AS oduzmi
                                           FROM pocetno)
                        UNION ALL
                                 SELECT radni_nalog_stavke.id_stavka AS id,
                                    radni_nalog.broj_naloga AS broj,
                                    radni_nalog.datum_naloga AS datum,
                                    radni_nalog_stavke.sifra_robe AS sifra,
                                    radni_nalog_stavke.id_skladiste AS skladiste,
                                    COALESCE(replace(radni_nalog_stavke.kolicina::text, ','::text, '.'::text)::numeric, 0::numeric) AS kolicina,
                                    radni_nalog_stavke.nbc::numeric AS nbc,
                                    replace(radni_nalog_stavke.porez::text, ','::text, '.'::text)::numeric AS porez,
                                    radni_nalog_stavke.vpc::numeric AS vpc,
                                    0::numeric AS rabat,
                                    'radni_nalog_stavke'::text AS doc,
                                    'ulaz'::text AS ui, roba.oduzmi
                                   FROM radni_nalog_stavke
                              LEFT JOIN radni_nalog ON radni_nalog.broj_naloga = radni_nalog_stavke.broj_naloga
                         LEFT JOIN roba ON roba.sifra::text = radni_nalog_stavke.sifra_robe::text)
                UNION ALL ";

                if (Class.PodaciTvrtka.oibTvrtke == Class.Postavke.OIB_PC1)
                {
                    query += @"(SELECT X.* FROM (
	SELECT NS.id_stavka AS id,
	RN.broj_naloga AS broj,
	RN.datum_naloga AS datum,
	NS.sifra_robe AS sifra,
	NS.id_skladiste AS skladiste,
	COALESCE(replace(NS.kolicina::text, ','::text, '.'::text)::numeric, 0::numeric) * COALESCE(replace(RNS.kolicina::text, ','::text, '.'::text)::numeric, 0::numeric) AS kolicina,
	RNN.NBC::numeric AS nbc,
	replace(RNN.PDV::text, ','::text, '.'::text)::numeric AS porez,
	RNN.vpc::numeric as vpc, 0::numeric AS rabat,
	'radni_nalog_stavke_normativi'::text AS doc,
	'izlaz'::text AS ui, R.oduzmi
	from radni_nalog_normativ RNN
	right join radni_nalog_stavke RNS on RNN.broj = RNS.broj_naloga
	right join radni_nalog RN on RNS.broj_naloga = RN.broj_naloga
	left join normativi N on RNS.sifra_robe = N.sifra_artikla
	left join normativi_stavke NS on N.broj_normativa = NS.broj_normativa and RNN.sifra = NS.sifra_robe
	LEFT JOIN ROBA R ON NS.sifra_robe = R.SIFRA
) X
where x.id is not null))";
                }
                else
                {
                    query += @"SELECT normativi_stavke.id_stavka AS id,
                            radni_nalog.broj_naloga AS broj,
                            radni_nalog.datum_naloga AS datum,
                            normativi_stavke.sifra_robe AS sifra,
                            normativi_stavke.id_skladiste AS skladiste,
                            COALESCE(replace(normativi_stavke.kolicina::text, ','::text, '.'::text)::numeric, 0::numeric) * COALESCE(replace(radni_nalog_stavke.kolicina::text, ','::text, '.'::text)::numeric, 0::numeric) AS kolicina,
                            roba_prodaja.nc::numeric AS nbc,
                            replace(roba_prodaja.porez, ',','.')::numeric AS porez,
                            roba_prodaja.vpc, 0::numeric AS rabat,
                            'radni_nalog_stavke_normativi'::text AS doc,
                            'izlaz'::text AS ui, roba.oduzmi
                           FROM normativi_stavke
                      LEFT JOIN normativi ON normativi.broj_normativa = normativi_stavke.broj_normativa
                 LEFT JOIN radni_nalog_stavke ON normativi.sifra_artikla::text = radni_nalog_stavke.sifra_robe::text
            LEFT JOIN radni_nalog ON radni_nalog.broj_naloga = radni_nalog_stavke.broj_naloga
       LEFT JOIN roba_prodaja ON roba_prodaja.sifra::text = normativi_stavke.sifra_robe::text AND roba_prodaja.id_skladiste = normativi_stavke.id_skladiste
   LEFT JOIN roba ON roba.sifra::text = normativi_stavke.sifra_robe::text)";
                }

                query += @"UNION ALL
                 SELECT normativi_stavke.id_stavka AS id,
                    racuni.broj_racuna::bigint AS broj,
                    racuni.datum_racuna AS datum,
                    normativi_stavke.sifra_robe AS sifra,
                    normativi_stavke.id_skladiste AS skladiste,
                    COALESCE(replace(normativi_stavke.kolicina::text, ','::text, '.'::text)::numeric, 0::numeric) * COALESCE(replace(racun_stavke.kolicina::text, ','::text, '.'::text)::numeric, 0::numeric) AS kolicina,
                    roba_prodaja.nc::numeric AS nbc,
                    replace(roba_prodaja.porez, ',','.')::numeric AS porez, roba_prodaja.vpc,
                    0::numeric AS rabat,
                    'radni_nalog_skida_normative_prema_uslugi'::text AS doc,
                    'izlaz'::text AS ui, roba.oduzmi
                   FROM normativi_stavke
              LEFT JOIN normativi ON normativi.broj_normativa = normativi_stavke.broj_normativa
         LEFT JOIN racun_stavke ON normativi.sifra_artikla::text = racun_stavke.sifra_robe::text
    LEFT JOIN racuni ON racuni.broj_racuna::text = racun_stavke.broj_racuna::text AND racuni.id_ducan = racun_stavke.id_ducan AND racuni.id_kasa = racun_stavke.id_kasa
LEFT JOIN roba_prodaja ON roba_prodaja.sifra::text = normativi_stavke.sifra_robe::text AND roba_prodaja.id_skladiste = normativi_stavke.id_skladiste
LEFT JOIN roba ON roba.sifra::text = normativi.sifra_artikla::text) ulaz_izlaz_robe_financijski
LEFT JOIN skladiste on ulaz_izlaz_robe_financijski.skladiste = skladiste.id_skladiste;";

                classSQL.insert(query);
            }

            if (a.Length == 0)
            {
                string sql_view = @"CREATE OR REPLACE VIEW ulaz_izlaz_robe AS
                SELECT * FROM
                (
                /*POČETNO*/
                SELECT COALESCE(CAST(REPLACE(kolicina,',','.') as NUMERIC),0) AS kolicina,id_skladiste as skladiste,datum, sifra AS sifra,'Pocetno' as documenat FROM pocetno

                UNION ALL

                /*KALKULACIJA*/
                (SELECT CAST(REPLACE(kolicina,',','.') as NUMERIC) as kolicina, kalkulacija_stavke.id_skladiste as skladiste,kalkulacija.datum as datum, sifra as sifra,'Kalkulacija' as documenat
                FROM kalkulacija_stavke
                LEFT JOIN kalkulacija ON kalkulacija.broj=kalkulacija_stavke.broj
                AND kalkulacija.id_skladiste=kalkulacija_stavke.id_skladiste)

                UNION ALL

                /*RAČUN*/
                (SELECT CAST(REPLACE(kolicina,',','.') as NUMERIC) as kolicina, racun_stavke.id_skladiste as skladiste,racuni.datum_racuna as datum, sifra_robe as sifra,'Racuni' as documenat
                FROM racun_stavke
                LEFT JOIN racuni ON racuni.broj_racuna = racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa)

                UNION ALL

                /*IZDATNICA*/
                (SELECT CAST(REPLACE(kolicina,',','.') as NUMERIC)  as kolicina, izdatnica.id_skladiste as skladiste,izdatnica.datum as datum, sifra as sifra,'Izdatnica' as documenat
                FROM izdatnica_stavke
                LEFT JOIN izdatnica ON izdatnica.id_izdatnica=izdatnica_stavke.id_izdatnica)

                UNION ALL

                /*PRIMKA*/
                (SELECT CAST(REPLACE(kolicina,',','.') as NUMERIC) as kolicina, primka.id_skladiste as skladiste,primka.datum as datum, sifra as sifra,'Primka' as documenat
                FROM primka_stavke
                LEFT JOIN primka ON primka.id_primka=primka_stavke.id_primka )

                UNION ALL

                /*FAKTURE*/
                (SELECT CAST(REPLACE(kolicina,',','.') as NUMERIC) as kolicina, faktura_stavke.id_skladiste as skladiste,fakture.date as datum, sifra as sifra,'Faktura' as documenat
                FROM faktura_stavke
                LEFT JOIN fakture ON fakture.broj_fakture = faktura_stavke.broj_fakture AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa
                WHERE fakture.oduzmi_iz_skladista='1')

                UNION ALL

                /*MS u skl*/
                (SELECT CAST(REPLACE(kolicina,',','.') as NUMERIC) as kolicina, meduskladisnica.id_skladiste_do as skladiste,meduskladisnica.datum as datum, sifra as sifra,'MS u skl' as documenat
                FROM meduskladisnica_stavke
                LEFT JOIN meduskladisnica ON meduskladisnica.broj=meduskladisnica_stavke.broj
                AND meduskladisnica.id_skladiste_od=meduskladisnica_stavke.iz_skladista )

                UNION ALL

                /*MS iz skl*/
                (SELECT CAST(REPLACE(kolicina,',','.') as NUMERIC) as kolicina, meduskladisnica.id_skladiste_od as skladiste,meduskladisnica.datum as datum, sifra as sifra,'MS iz skl' as documenat
                FROM meduskladisnica_stavke
                LEFT JOIN meduskladisnica ON meduskladisnica.broj=meduskladisnica_stavke.broj
                AND meduskladisnica.id_skladiste_od=meduskladisnica_stavke.iz_skladista)

                UNION ALL

                /*OTPREMNICA*/
                (SELECT CAST(REPLACE(kolicina,',','.') as NUMERIC) as kolicina, otpremnica_stavke.id_skladiste as skladiste,otpremnice.datum as datum, otpremnica_stavke.sifra_robe as sifra,'Otpremnica' as documenat
                FROM otpremnica_stavke
                LEFT JOIN otpremnice ON otpremnice.broj_otpremnice=otpremnica_stavke.broj_otpremnice
                AND otpremnice.id_skladiste=otpremnica_stavke.id_skladiste
where otpremnice.oduzmi_iz_skladista = true)

                UNION ALL

                /*OTPIS ROBE*/
                (SELECT CAST(REPLACE(kolicina,',','.') as NUMERIC) as kolicina, otpis_robe.id_skladiste as skladiste,otpis_robe.datum as datum, sifra as sifra,'Otpis' as documenat
                FROM otpis_robe_stavke
                LEFT JOIN otpis_robe ON otpis_robe.broj=otpis_robe_stavke.broj)

                UNION ALL

                /*POVRAT ROBE*/
                (SELECT CAST(REPLACE(kolicina,',','.') as NUMERIC) as kolicina, povrat_robe.id_skladiste as skladiste,povrat_robe.datum as datum, sifra as sifra,'Povrat robe' as documenat
                FROM povrat_robe_stavke
                LEFT JOIN povrat_robe ON povrat_robe.broj=povrat_robe_stavke.broj)

                UNION ALL

                /*RADNI NALOG STAVKE koji dodaje na skladište kad se radni nalog izradi*/
                (SELECT COALESCE(CAST(REPLACE(kolicina,',','.') as NUMERIC),0) as kolicina, id_skladiste as skladiste,datum_naloga as datum, sifra_robe as sifra,'Radni nalog dodaje na skl' as documenat
                FROM radni_nalog_stavke
                LEFT JOIN radni_nalog ON radni_nalog.broj_naloga=radni_nalog_stavke.broj_naloga)

                UNION ALL

                --RADNI NALOG STAVKE-->NORMATIVI
                SELECT
                COALESCE(CAST(REPLACE(normativi_stavke.kolicina,',','.') as NUMERIC),0)* COALESCE(CAST(REPLACE(radni_nalog_stavke.kolicina,',','.') as NUMERIC),0) as kolicina,
                normativi_stavke.id_skladiste as skladiste,radni_nalog.datum_naloga as datum, normativi_stavke.sifra_robe as sifra,'Radni nalog skida normative' as documenat
                FROM normativi_stavke
                LEFT JOIN normativi ON normativi.broj_normativa = normativi_stavke.broj_normativa
                LEFT JOIN radni_nalog_stavke ON normativi.sifra_artikla=radni_nalog_stavke.sifra_robe
                LEFT JOIN radni_nalog ON radni_nalog.broj_naloga=radni_nalog_stavke.broj_naloga

                UNION ALL

                --NORMATIVI (NA USLUGU SE DODAJU PRODAJNI ARTIKLI)
                SELECT
                COALESCE(CAST(REPLACE(normativi_stavke.kolicina,',','.') as NUMERIC),0)* COALESCE(CAST(REPLACE(racun_stavke.kolicina,',','.') as NUMERIC),0) as kolicina,
                normativi_stavke.id_skladiste as skladiste,racuni.datum_racuna as datum, normativi_stavke.sifra_robe as sifra,'Radni nalog skida normative prema uslugi' as documenat
                FROM normativi_stavke
                LEFT JOIN normativi ON normativi.broj_normativa = normativi_stavke.broj_normativa
                LEFT JOIN racun_stavke ON normativi.sifra_artikla=racun_stavke.sifra_robe
                LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa
                LEFT JOIN roba ON roba.sifra=normativi.sifra_artikla
                WHERE roba.oduzmi='NE'

                ) robno;";

                classSQL.insert(sql_view);
            }

            #endregion ulaz_izlaz_robe UZME SVE VRIJEDNOSTI IZ BAZE
        }

        private static void AlterSkladiste(DataTable DTremote, DataTable cols)
        {
            DataRow[] dataROW = cols.Select("table_name = 'skladiste' and column_name = 'is_glavno'");
            if (dataROW.Length == 0)
            {
                try
                {
                    string sql = "ALTER TABLE skladiste ADD COLUMN is_glavno boolean default false;";
                    classSQL.select(sql, "skladiste");
                }
                catch
                {
                }
            }

            dataROW = cols.Select("table_name = 'skladiste' and column_name = 'samo_nbc'");
            if (dataROW.Length == 0)
            {
                try
                {
                    string sql = "ALTER TABLE skladiste ADD COLUMN samo_nbc boolean default false;";
                    classSQL.select(sql, "skladiste");
                    if (Class.PodaciTvrtka.oibTvrtke == Class.Postavke.OIB_PC1)
                    {
                        sql = "update skladiste set samo_nbc = true where id_skladiste in (3,4);";
                        classSQL.select(sql, "skladiste");
                    }
                }
                catch
                {
                }
            }
        }

        private static void AlterWebSyn(DataTable DTremote, DataTable cols)
        {
            DataRow[] a = cols.Select("table_name = 'skladiste' and column_name = 'editirano'");
            if (a.Length == 0)
            {
                string sql = " ALTER TABLE skladiste ADD COLUMN editirano boolean DEFAULT '1';" +
                             " ALTER TABLE skladiste ADD COLUMN datum_syn timestamp without time zone DEFAULT null;";
                classSQL.insert(sql);
            }

            a = cols.Select("table_name = 'skladiste' and column_name = 'id_skl_centrala'");
            if (a.Length == 0)
            {
                string sql = " ALTER TABLE skladiste ADD COLUMN id_skl_centrala integer DEFAULT '0';";
                classSQL.insert(sql);
            }

            //a = cols.Select("table_name = 'skladiste' and column_name = 'isGlavno'");
            //if (a.Length == 0)
            //{
            //    string sql = " ALTER TABLE skladiste ADD COLUMN isGlavno boolean DEFAULT false;";
            //    classSQL.insert(sql);
            //}

            a = cols.Select("table_name = 'kalkulacija' and column_name = 'novo'");
            if (a.Length == 0)
            {
                string sql = " ALTER TABLE kalkulacija ADD COLUMN novo boolean DEFAULT '1';" +
                             " ALTER TABLE kalkulacija ADD COLUMN editirano boolean DEFAULT '1';" +
                             " ALTER TABLE kalkulacija ADD COLUMN trosak numeric DEFAULT 0;" +
                             " ALTER TABLE kalkulacija ADD COLUMN datum_syn timestamp without time zone DEFAULT null;";
                classSQL.insert(sql);
            }

            a = cols.Select("table_name = 'kalkulacija' and column_name = 'trosak'");
            if (a.Length == 0)
            {
                string sql = " ALTER TABLE kalkulacija ADD COLUMN trosak numeric DEFAULT 0;";
                classSQL.insert(sql);
            }

            a = cols.Select("table_name = 'otpremnice' and column_name = 'novo'");
            if (a.Length == 0)
            {
                string sql = " ALTER TABLE otpremnice ADD COLUMN novo boolean DEFAULT '1';" +
                             " ALTER TABLE otpremnice ADD COLUMN editirano boolean DEFAULT '1';" +
                             " ALTER TABLE otpremnice ADD COLUMN datum_syn timestamp without time zone DEFAULT null;";
                classSQL.insert(sql);
            }

            a = cols.Select("table_name = 'otpremnice' and column_name = 'partner_poslovnica'");
            if (a.Length == 0)
            {
                try
                {
                    string sql = "ALTER TABLE otpremnice ADD COLUMN partner_poslovnica numeric DEFAULT 0;";
                    classSQL.select(sql, "otpremnice");
                }
                catch
                {
                }
            }

            a = cols.Select("table_name = 'primka' and column_name = 'novo'");
            if (a.Length == 0)
            {
                string sql = @"ALTER TABLE primka ADD COLUMN novo boolean DEFAULT '1';
                               ALTER TABLE primka ADD COLUMN editirano boolean DEFAULT '1';
                               ALTER TABLE primka ADD COLUMN datum_syn timestamp without time zone DEFAULT null;
                               ALTER TABLE roba ADD COLUMN novo boolean DEFAULT '1';
                               ALTER TABLE roba ADD COLUMN editirano boolean DEFAULT '1';
                               ALTER TABLE roba ADD COLUMN datum_syn timestamp without time zone DEFAULT null;
                               ALTER TABLE roba_prodaja ADD COLUMN novo boolean DEFAULT '1';
                               ALTER TABLE roba_prodaja ADD COLUMN editirano boolean DEFAULT '1';
                               ALTER TABLE roba_prodaja ADD COLUMN datum_syn timestamp without time zone DEFAULT null;
                               ALTER TABLE grupa ADD COLUMN novo boolean DEFAULT '1';
                               ALTER TABLE grupa ADD COLUMN editirano boolean DEFAULT '1';
                               ALTER TABLE grupa ADD COLUMN datum_syn timestamp without time zone DEFAULT null;";
                classSQL.insert(sql);
            }

            if (cols.Select("table_name = 'roba_prodaja' and column_name = 'ppmv'").Length == 0)
            {
                string sql = "alter table roba_prodaja add column ppmv numeric (15,6) default 0;";
                classSQL.insert(sql);
                sql = "alter table faktura_stavke add column ppmv numeric (15,6) default 0;";
                classSQL.insert(sql);
                sql = "alter table kalkulacija_stavke add column ppmv numeric (15,6) default 0;";
                classSQL.insert(sql);
            }

            if (cols.Select("table_name = 'ispis_faktura_stavke' and column_name = 'ppmv'").Length == 0)
            {
                string sql = "ALTER TABLE ISPIS_FAKTURA_STAVKE ADD COLUMN ppmv numeric(15,6) DEFAULT 0;";
                classSQL.insert(sql);
            }

            a = cols.Select("table_name = 'grupa' and column_name = 'novo'");
            if (a.Length == 0)
            {
                string sql = @"ALTER TABLE grupa ADD COLUMN novo boolean DEFAULT '1';
                               ALTER TABLE grupa ADD COLUMN editirano boolean DEFAULT '1';
                               ALTER TABLE grupa ADD COLUMN datum_syn timestamp without time zone DEFAULT null;";
                classSQL.insert(sql);
            }

            a = cols.Select("table_name = 'zaposlenici' and column_name = 'novo'");
            if (a.Length == 0)
            {
                string sql = @"ALTER TABLE zaposlenici ADD COLUMN novo boolean DEFAULT '1';
                               ALTER TABLE zaposlenici ADD COLUMN editirano boolean DEFAULT '1';
                               ALTER TABLE zaposlenici ADD COLUMN datum_syn timestamp without time zone DEFAULT null;";
                classSQL.insert(sql);
            }

            a = cols.Select("table_name = 'povrat_robe' and column_name = 'novo'");
            if (a.Length == 0)
            {
                string sql = @"ALTER TABLE povrat_robe ADD COLUMN novo boolean DEFAULT '1';
                               ALTER TABLE povrat_robe ADD COLUMN editirano boolean DEFAULT '1';
                               ALTER TABLE povrat_robe ADD COLUMN datum_syn timestamp without time zone DEFAULT null;

                               ALTER TABLE otpis_robe ADD COLUMN novo boolean DEFAULT '1';
                               ALTER TABLE otpis_robe ADD COLUMN editirano boolean DEFAULT '1';
                               ALTER TABLE otpis_robe ADD COLUMN datum_syn timestamp without time zone DEFAULT null;

                               ALTER TABLE inventura ADD COLUMN novo boolean DEFAULT '1';
                               ALTER TABLE inventura ADD COLUMN editirano boolean DEFAULT '1';
                               ALTER TABLE inventura ADD COLUMN datum_syn timestamp without time zone DEFAULT null;

                               ALTER TABLE pocetno ADD COLUMN novo boolean DEFAULT '1';
                               ALTER TABLE pocetno ADD COLUMN editirano boolean DEFAULT '1';
                               ALTER TABLE pocetno ADD COLUMN datum_syn timestamp without time zone DEFAULT null;

                               ALTER TABLE partners ADD COLUMN novo boolean DEFAULT '1';
                               ALTER TABLE partners ADD COLUMN editirano boolean DEFAULT '1';
                               ALTER TABLE partners ADD COLUMN datum_syn timestamp without time zone DEFAULT null;

                               ALTER TABLE ducan ADD COLUMN novo boolean DEFAULT '1';
                               ALTER TABLE ducan ADD COLUMN editirano boolean DEFAULT '1';
                               ALTER TABLE ducan ADD COLUMN datum_syn timestamp without time zone DEFAULT null;

                               ALTER TABLE meduskladisnica ADD COLUMN novo boolean DEFAULT '1';
                               ALTER TABLE meduskladisnica ADD COLUMN editirano boolean DEFAULT '1';
                               ALTER TABLE meduskladisnica ADD COLUMN datum_syn timestamp without time zone DEFAULT null;

                               ALTER TABLE fakture ADD COLUMN novo boolean DEFAULT '1';
                               ALTER TABLE fakture ADD COLUMN editirano boolean DEFAULT '1';
                               ALTER TABLE fakture ADD COLUMN datum_syn timestamp without time zone DEFAULT null;";

                classSQL.insert(sql);
            }
        }

        private static void AlterProizvodackaCijena(DataTable dtRemote, DataTable dtCols)
        {
            try
            {
                DataRow[] a = dtCols.Select("table_name = 'roba' and column_name = 'proizvodacka_cijena'");
                if (a.Length == 0)
                {
                    string sql = " ALTER TABLE roba ADD COLUMN proizvodacka_cijena numeric DEFAULT 0;" +
                                 " ALTER TABLE roba_prodaja ADD COLUMN proizvodacka_cijena numeric DEFAULT 0;";
                    classSQL.insert(sql);

                    if (Util.Korisno.oibTvrtke == "88985647471")
                    {
                        sql = @"update roba
set proizvodacka_cijena = dummy.proizvodacka_cijena
from (
	select id_roba, nc, nc::numeric as nbc, ((nc::numeric) / (1::numeric + (35::numeric / 100::numeric))) as proizvodacka_cijena
from roba
) as dummy
where roba.id_roba = dummy.id_roba;";

                        classSQL.update(sql);
                    }
                }

                a = dtCols.Select("table_name = 'fakture' and column_name = 'use_nbc'");
                if (a.Length == 0)
                {
                    string sql = "ALTER TABLE fakture ADD COLUMN use_nbc BOOLEAN DEFAULT FALSE;";
                    classSQL.insert(sql);
                }

                a = dtCols.Select("table_name = 'fakture' and column_name = 'pouzece'");
                if (a.Length == 0)
                {
                    string sql = "ALTER TABLE fakture ADD COLUMN pouzece BOOLEAN DEFAULT FALSE;";
                    classSQL.insert(sql);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static void AlterEan(DataTable dtRemote, DataTable dtCols)
        {
            try
            {
                DataRow[] a = dtCols.Select("table_name = 'roba' and column_name = 'ean'");
                if (a.Length > 0)
                {
                    string sql = " ALTER TABLE roba ALTER COLUMN ean TYPE text;";
                    classSQL.insert(sql);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}