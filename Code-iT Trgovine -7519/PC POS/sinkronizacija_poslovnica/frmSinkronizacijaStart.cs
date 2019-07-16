using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.sinkronizacija_poslovnica
{
    public partial class frmSinkronizacijaStart : Form
    {
        public frmSinkronizacijaStart()
        {
            InitializeComponent();
        }

        public DataTable DTpostavke { get; set; }
        private DataTable DT = new DataTable();
        private double ukupno_progres = 1;
        private string dokumenat = "";
        private string[] ArrSkladiste;
        private string skladista_kalkulacija = "";

        private void frmSinkronizacijaStart_Load(object sender, EventArgs e)
        {
            WebReference.KonektorPostgres Server = new WebReference.KonektorPostgres();
            ArrSkladiste = DTpostavke.Rows[0]["skladiste_kalkulacije"].ToString().Split(';');
            UcitajSkladiste();
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.RunWorkerAsync();
        }

        private void UcitajSkladiste()
        {
            for (int i = 0; i < ArrSkladiste.Length; i++)
            {
                if (ArrSkladiste[i] != "")
                {
                    skladista_kalkulacija += "'" + ArrSkladiste[i] + "',";
                }
            }

            if (skladista_kalkulacija.Length > 0)
            {
                skladista_kalkulacija = skladista_kalkulacija.Remove(skladista_kalkulacija.Length - 1);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string sql = "SELECT " +
                "(SELECT coalesce(MAX(id_kalkulacija),0) FROM kalkulacija) AS kalkulacija, " +
                "(SELECT coalesce(MAX(id_stavka),0) FROM kalkulacija_stavke) AS kalkulacija_stavke, " +
                "(SELECT coalesce(MAX(id_roba),0) FROM roba) AS roba, " +
                "(SELECT coalesce(MAX(id_roba_prodaja),0) FROM roba_prodaja) AS roba_prodaja, " +
                "(SELECT coalesce(MAX(id_partner),0) FROM partners WHERE id_partner<200000) AS partners, " +
                "(SELECT coalesce(MAX(id_primka),0) FROM primka) AS primka, " +
                "(SELECT coalesce(MAX(id_stavka),0) FROM primka_stavke) AS primka_stavke, " +
                "(SELECT coalesce(MAX(id),0) FROM meduskladisnica) AS meduskladisnica, " +
                "(SELECT coalesce(MAX(id_zaposlenik),0) FROM zaposlenici) AS zaposlenici, " +
                "(SELECT coalesce(MAX(id_skladiste),0) FROM skladiste) AS skladiste, " +
                "(SELECT coalesce(MAX(id_stavka),0) FROM meduskladisnica_stavke) AS meduskladisnica_stavke, " +
                "(SELECT coalesce(MAX(broj),0) FROM povrat_robe) AS povrat_robe, " +
                "(SELECT coalesce(MAX(id_stavka),0) FROM povrat_robe_stavke) AS povrat_robe_stavke";
            DataTable DTmaxBroj = classSQL.select(sql, "DT").Tables[0];

            GetSkladista(DTmaxBroj, e);
            GetZaposlenici(DTmaxBroj, e);
            GetPartners(DTmaxBroj, e);
            GetRoba(DTmaxBroj, e);
            //GetRobaProdaja(DTmaxBroj, e);
            GetKalkulacije(DTmaxBroj, e);
            GetKalkulacijaStavke(DTmaxBroj, e);
            GetMeduskladisnica(DTmaxBroj, e);
            GetMeduskladisnicaStavke(DTmaxBroj, e);
            //GetPrimke(DTmaxBroj, e);
            //GetPrimkaStavke(DTmaxBroj, e);
            GetPovratRobe(DTmaxBroj, e);
            GetPovratRobeStavke(DTmaxBroj, e);
            PoravnavanjeIdSerialBazi();
        }

        #region GetZaposlenici*************************************************************

        public struct Zaposlenici
        {
            public int id_zaposlenik;
            public string ime;
            public string prezime;
            public int id_grad;
            public string adresa;
            public int id_dopustenje;
            public string oib;
            public string tel;
            public DateTime datum_rodenja;
            public string email;
            public string mob;
            public string aktivan;
            public string zaporka;
        }

        private void GetZaposlenici(DataTable DT, DoWorkEventArgs e)
        {
            WebReference.KonektorPostgres Server = new WebReference.KonektorPostgres();
            Zaposlenici r = new Zaposlenici();
            string sql = "SELECT * FROM zaposlenici WHERE id_zaposlenik>'" + DT.Rows[0]["zaposlenici"].ToString() + "'";
            DataTable DTk = Server.GetDataset(sql, "DT", DTpostavke.Rows[0]["korisnickoime"].ToString(), DTpostavke.Rows[0]["lozinka"].ToString()).Tables[0];
            ukupno_progres = DTk.Rows.Count;
            dokumenat = "ZAPOSLENICI";

            for (int i = 0; i < DTk.Rows.Count; i++)
            {
                //*************************************************PROVJERA ROBE I SPREMANJE U STRUCT************************************************
                if (!int.TryParse(DTk.Rows[i]["id_zaposlenik"].ToString(), out r.id_zaposlenik)) { r.id_zaposlenik = 1; }
                r.ime = DTk.Rows[i]["ime"].ToString();
                r.prezime = DTk.Rows[i]["prezime"].ToString();
                if (!int.TryParse(DTk.Rows[i]["id_grad"].ToString(), out r.id_grad)) { r.id_grad = 1; }
                r.adresa = DTk.Rows[i]["adresa"].ToString();
                if (!int.TryParse(DTk.Rows[i]["id_dopustenje"].ToString(), out r.id_dopustenje)) { r.id_dopustenje = 1; }
                r.oib = DTk.Rows[i]["oib"].ToString();
                r.tel = DTk.Rows[i]["tel"].ToString();
                if (!DateTime.TryParse(DTk.Rows[i]["datum_rodenja"].ToString(), out r.datum_rodenja)) { r.datum_rodenja = DateTime.Now; }
                r.email = DTk.Rows[i]["email"].ToString();
                r.mob = DTk.Rows[i]["mob"].ToString();
                r.aktivan = DTk.Rows[i]["aktivan"].ToString() == "" ? "NE" : DTk.Rows[i]["aktivan"].ToString();
                r.zaporka = DTk.Rows[i]["zaporka"].ToString();
                //**************************************************************************************************************************************

                sql = "INSERT INTO zaposlenici (id_zaposlenik,ime,prezime,id_grad,adresa,id_dopustenje,oib,tel,datum_rodenja,email,mob,aktivan,zaporka" +
                    ") VALUES (" +
                    "'" + r.id_zaposlenik + "'," +
                    "'" + r.ime + "'," +
                    "'" + r.prezime + "'," +
                    "'" + r.id_grad + "'," +
                    "'" + r.adresa + "'," +
                    "'" + r.id_dopustenje + "'," +
                    "'" + r.oib + "'," +
                    "'" + r.tel + "'," +
                    "'" + r.datum_rodenja + "'," +
                    "'" + r.email + "'," +
                    "'" + r.mob + "'," +
                    "'" + r.aktivan + "'," +
                    "'" + r.zaporka + "'" +
                    ")";
                classSQL.insert(sql);

                backgroundWorker1.ReportProgress(i);
            }
        }

        #endregion GetZaposlenici*************************************************************

        #region GetSkladista*************************************************************

        public struct Skladista
        {
            public string skladiste;
            public int id_grad;
            public int id_zemlja;
            public int id_skladiste;
            public string aktivnost;
        }

        private void GetSkladista(DataTable DT, DoWorkEventArgs e)
        {
            WebReference.KonektorPostgres Server = new WebReference.KonektorPostgres();
            Skladista r = new Skladista();
            string sql = "SELECT * FROM skladiste WHERE id_skladiste>'" + DT.Rows[0]["skladiste"].ToString() + "'";
            DataTable DTk = Server.GetDataset(sql, "DT", DTpostavke.Rows[0]["korisnickoime"].ToString(), DTpostavke.Rows[0]["lozinka"].ToString()).Tables[0];
            ukupno_progres = DTk.Rows.Count;
            dokumenat = "SKLADIŠTE";

            for (int i = 0; i < DTk.Rows.Count; i++)
            {
                //*************************************************PROVJERA ROBE I SPREMANJE U STRUCT************************************************
                r.skladiste = DTk.Rows[i]["skladiste"].ToString();
                if (!int.TryParse(DTk.Rows[i]["id_grad"].ToString(), out r.id_grad)) { r.id_grad = 1; }
                if (!int.TryParse(DTk.Rows[i]["id_zemlja"].ToString(), out r.id_zemlja)) { r.id_zemlja = 1; }
                if (!int.TryParse(DTk.Rows[i]["id_skladiste"].ToString(), out r.id_skladiste)) { r.id_skladiste = 1; }
                r.aktivnost = DTk.Rows[i]["aktivnost"].ToString() == "" ? "NE" : DTk.Rows[i]["aktivnost"].ToString();
                //**************************************************************************************************************************************

                sql = "INSERT INTO skladiste (skladiste,id_grad,id_zemlja,id_skladiste,aktivnost" +
                    ") VALUES (" +
                    "'" + r.skladiste + "'," +
                    "'" + r.id_grad + "'," +
                    "'" + r.id_zemlja + "'," +
                    "'" + r.id_skladiste + "'," +
                    "'" + r.aktivnost + "'" +
                    ")";
                classSQL.insert(sql);

                backgroundWorker1.ReportProgress(i);
            }
        }

        #endregion GetSkladista*************************************************************

        #region GetRoba*************************************************************

        public struct Roba
        {
            public string naziv;
            public string jm;
            public string vpc;
            public string mpc;
            public string id_zemlja_porijekla;
            public string id_zemlja_uvoza;
            public string id_partner;
            public string id_manufacturers;
            public string id_roba;
            public string sifra;
            public string ean;
            public string porez;
            public string oduzmi;
            public string nc;
            public string porez_potrosnja;
            public string opis;
            public string brand;
            public string jamstvo;
            public string akcija;
            public string link_za_slike;
            public string id_podgrupa;
        }

        private void GetRoba(DataTable DT, DoWorkEventArgs e)
        {
            WebReference.KonektorPostgres Server = new WebReference.KonektorPostgres();
            Roba r = new Roba();
            string sql = "SELECT * FROM roba WHERE id_roba>'" + DT.Rows[0]["roba"].ToString() + "'";
            DataTable DTk = Server.GetDataset(sql, "DT", DTpostavke.Rows[0]["korisnickoime"].ToString(), DTpostavke.Rows[0]["lozinka"].ToString()).Tables[0];
            ukupno_progres = DTk.Rows.Count;
            dokumenat = "ROBA";
            for (int i = 0; i < DTk.Rows.Count; i++)
            {
                //*************************************************PROVJERA ROBE I SPREMANJE U STRUCT************************************************
                r.naziv = DTk.Rows[i]["naziv"].ToString();
                r.jm = DTk.Rows[i]["jm"].ToString() == "" ? "kom" : DTk.Rows[i]["jm"].ToString();
                r.vpc = DTk.Rows[i]["vpc"].ToString() == "" ? "0" : DTk.Rows[i]["vpc"].ToString().Replace(",", ".");
                r.mpc = DTk.Rows[i]["mpc"].ToString() == "" ? "0" : DTk.Rows[i]["mpc"].ToString().Replace(".", ",");
                r.id_zemlja_porijekla = DTk.Rows[i]["id_zemlja_porijekla"].ToString() == "" ? "1" : DTk.Rows[i]["id_zemlja_porijekla"].ToString();
                r.id_zemlja_uvoza = DTk.Rows[i]["id_zemlja_uvoza"].ToString() == "" ? "1" : DTk.Rows[i]["id_zemlja_uvoza"].ToString();
                r.id_partner = DTk.Rows[i]["id_partner"].ToString() == "" ? "1" : DTk.Rows[i]["id_partner"].ToString();
                r.id_manufacturers = DTk.Rows[i]["id_manufacturers"].ToString() == "" ? "1" : DTk.Rows[i]["id_manufacturers"].ToString();
                r.id_roba = DTk.Rows[i]["id_roba"].ToString() == "" ? "1" : DTk.Rows[i]["id_roba"].ToString();
                r.sifra = DTk.Rows[i]["sifra"].ToString() == "" ? "greska" : DTk.Rows[i]["sifra"].ToString();
                r.ean = DTk.Rows[i]["ean"].ToString() == "" ? "-1" : DTk.Rows[i]["ean"].ToString();
                r.porez = DTk.Rows[i]["porez"].ToString() == "" ? "0" : DTk.Rows[i]["porez"].ToString();
                r.oduzmi = DTk.Rows[i]["oduzmi"].ToString() == "" ? "NE" : DTk.Rows[i]["oduzmi"].ToString();
                r.nc = DTk.Rows[i]["nc"].ToString() == "" ? "0" : DTk.Rows[i]["nc"].ToString().Replace(".", ",");
                r.porez_potrosnja = DTk.Rows[i]["porez_potrosnja"].ToString() == "" ? "0" : DTk.Rows[i]["porez_potrosnja"].ToString();
                r.opis = DTk.Rows[i]["opis"].ToString() == "" ? "1" : DTk.Rows[i]["opis"].ToString();
                r.brand = DTk.Rows[i]["brand"].ToString() == "" ? "" : DTk.Rows[i]["brand"].ToString();
                r.jamstvo = DTk.Rows[i]["jamstvo"].ToString() == "" ? "0" : DTk.Rows[i]["jamstvo"].ToString();
                r.akcija = DTk.Rows[i]["akcija"].ToString() == "" ? "0" : DTk.Rows[i]["akcija"].ToString();
                r.link_za_slike = DTk.Rows[i]["link_za_slike"].ToString() == "" ? "" : DTk.Rows[i]["link_za_slike"].ToString();
                r.id_podgrupa = DTk.Rows[i]["id_podgrupa"].ToString() == "" ? "1" : DTk.Rows[i]["id_podgrupa"].ToString();

                r.naziv = r.naziv.Replace("\'", "");
                r.naziv = r.naziv.Replace("\"", "");
                r.naziv = r.naziv.Replace("&", " AND ");

                r.opis = r.opis.Replace("\'", "");
                r.opis = r.opis.Replace("\"", "");
                r.opis = r.opis.Replace("&", " AND ");

                //**************************************************************************************************************************************

                sql = "INSERT INTO roba (naziv,jm,vpc,mpc,id_zemlja_porijekla,id_zemlja_uvoza,id_partner," +
                    "id_manufacturers,id_roba,sifra,ean,porez,oduzmi,nc,porez_potrosnja," +
                    "opis,brand,jamstvo,akcija,link_za_slike,id_podgrupa) VALUES (" +
                    "'" + r.naziv + "'," +
                    "'" + r.jm + "'," +
                    "'" + r.vpc + "'," +
                    "'" + r.mpc + "'," +
                    "'" + r.id_zemlja_porijekla + "'," +
                    "'" + r.id_zemlja_uvoza + "'," +
                    "'" + r.id_partner + "'," +
                    "'" + r.id_manufacturers + "'," +
                    "'" + r.id_roba + "'," +
                    "'" + r.sifra + "'," +
                    "'" + r.ean + "'," +
                    "'" + r.porez + "'," +
                    "'" + r.oduzmi + "'," +
                    "'" + r.nc + "'," +
                    "'" + r.porez_potrosnja + "'," +
                    "'" + r.opis + "'," +
                    "'" + r.brand + "'," +
                    "'" + r.jamstvo + "'," +
                    "'" + r.akcija + "'," +
                    "'" + r.link_za_slike + "'," +
                    "'" + r.id_podgrupa + "'" +
                    ")";
                classSQL.insert(sql);

                backgroundWorker1.ReportProgress(i);
            }
        }

        #endregion GetRoba*************************************************************

        #region GetRobaProdaja**********************************************

        /*
        public struct RobaProdaja
        {
            public string id_roba_prodaja;
            public string id_skladiste;
            public string kolicina;
            public string nc;
            public string vpc;
            public string porez;
            public string sifra;
            public string porez_potrosnja;
        }

        private void GetRobaProdaja(DataTable DT, DoWorkEventArgs e)
        {
            WebReference.KonektorPostgres Server = new WebReference.KonektorPostgres();
            RobaProdaja rp = new RobaProdaja();
            string sql = "SELECT * FROM roba_prodaja WHERE id_roba_prodaja>'" + DT.Rows[0]["roba_prodaja"].ToString() + "'";
            DataTable DTrp = Server.GetDataset(sql, "DT", DTpostavke.Rows[0]["korisnickoime"].ToString(), DTpostavke.Rows[0]["lozinka"].ToString()).Tables[0];
            ukupno_progres = DTrp.Rows.Count;
            dokumenat = "ROBA PRODAJA";
            for (int i = 0; i < DTrp.Rows.Count; i++)
            {
                //*************************************************PROVJERA ROBE I SPREMANJE U STRUCT***************************************************
                decimal kol=0;
                rp.id_roba_prodaja = DTrp.Rows[i]["id_roba_prodaja"].ToString() == "" ? "1" : DTrp.Rows[i]["id_roba_prodaja"].ToString();
                rp.id_skladiste = DTrp.Rows[i]["id_skladiste"].ToString() == "" ? "1" : DTrp.Rows[i]["id_skladiste"].ToString();
                rp.kolicina = decimal.TryParse(DTrp.Rows[i]["kolicina"].ToString(), out kol) ? DTrp.Rows[i]["kolicina"].ToString() : "0" ;
                rp.nc = DTrp.Rows[i]["nc"].ToString() == "" ? "0" : DTrp.Rows[i]["nc"].ToString().Replace(".",",");
                rp.vpc = DTrp.Rows[i]["vpc"].ToString() == "" ? "0" : DTrp.Rows[i]["vpc"].ToString().Replace(",", ".");
                rp.porez = DTrp.Rows[i]["porez"].ToString() == "" ? "0" : DTrp.Rows[i]["porez"].ToString();
                rp.sifra = DTrp.Rows[i]["sifra"].ToString() == "" ? "1" : DTrp.Rows[i]["sifra"].ToString();
                rp.porez_potrosnja = DTrp.Rows[i]["porez_potrosnja"].ToString() == "" ? "0" : DTrp.Rows[i]["porez_potrosnja"].ToString();
                //**************************************************************************************************************************************

                sql = "INSERT INTO roba_prodaja (id_roba_prodaja,id_skladiste,kolicina,nc,vpc,porez,sifra,porez_potrosnja" +
                    ") VALUES (" +
                    "'" + rp.id_roba_prodaja + "'," +
                    "'" + rp.id_skladiste + "'," +
                    "'" + rp.kolicina + "'," +
                    "'" + rp.nc + "'," +
                    "'" + rp.vpc + "'," +
                    "'" + rp.porez + "'," +
                    "'" + rp.sifra + "'," +
                    "'" + rp.porez_potrosnja + "'" +
                    ")";
                classSQL.insert(sql);

                backgroundWorker1.ReportProgress(i);
            }
        }
         * */

        #endregion GetRobaProdaja**********************************************

        #region GetKalkulacije**********************************************

        public struct Kalkulacija
        {
            public int broj;
            public int id_partner;
            public string racun;
            public string otpremnica;
            public DateTime racun_datum;
            public DateTime otpremnica_datum;
            public string mjesto_troska;
            public DateTime datum;
            public string godina;
            public int id_kalkulacija;
            public decimal ukupno_vpc;
            public decimal ukupno_mpc;
            public string tecaj;
            public int id_valuta;
            public decimal fakturni_iznos;
            public int id_skladiste;
            public int id_zaposlenik;
            public string porez_potrosnja;
        }

        private void GetKalkulacije(DataTable DT, DoWorkEventArgs e)
        {
            WebReference.KonektorPostgres Server = new WebReference.KonektorPostgres();
            Kalkulacija kal = new Kalkulacija();

            string sql = "SELECT * FROM kalkulacija WHERE id_kalkulacija>'" + DT.Rows[0]["kalkulacija"].ToString() + "';";
            DataTable DTk = Server.GetDataset(sql, "DT", DTpostavke.Rows[0]["korisnickoime"].ToString(), DTpostavke.Rows[0]["lozinka"].ToString()).Tables[0];
            ukupno_progres = DTk.Rows.Count;
            dokumenat = "KALKULACIJA";
            for (int i = 0; i < DTk.Rows.Count; i++)
            {
                //*************************************************PROVJERA KALKULACIJE I SPREMANJE U STRUCT************************************************
                if (!int.TryParse(DTk.Rows[i]["broj"].ToString(), out kal.broj)) { kal.broj = 1; }
                if (!int.TryParse(DTk.Rows[i]["id_partner"].ToString(), out kal.id_partner)) { kal.id_partner = 1; }
                kal.racun = DTk.Rows[i]["racun"].ToString();
                kal.otpremnica = DTk.Rows[i]["otpremnica"].ToString();
                if (!DateTime.TryParse(DTk.Rows[i]["racun_datum"].ToString(), out kal.racun_datum)) { kal.racun_datum = DateTime.Now; }
                if (!DateTime.TryParse(DTk.Rows[i]["otpremnica_datum"].ToString(), out kal.otpremnica_datum)) { kal.otpremnica_datum = DateTime.Now; }
                kal.mjesto_troska = DTk.Rows[i]["mjesto_troska"].ToString();
                if (!DateTime.TryParse(DTk.Rows[i]["datum"].ToString(), out kal.datum)) { kal.datum = DateTime.Now; }
                kal.godina = DTk.Rows[i]["godina"].ToString();
                if (!int.TryParse(DTk.Rows[i]["id_kalkulacija"].ToString(), out kal.id_kalkulacija)) { kal.id_kalkulacija = 1; }
                if (!decimal.TryParse(DTk.Rows[i]["ukupno_vpc"].ToString(), out kal.ukupno_vpc)) { kal.ukupno_vpc = 0; }
                if (!decimal.TryParse(DTk.Rows[i]["ukupno_mpc"].ToString(), out kal.ukupno_mpc)) { kal.ukupno_mpc = 0; }
                kal.tecaj = DTk.Rows[i]["tecaj"].ToString();
                if (!int.TryParse(DTk.Rows[i]["id_valuta"].ToString(), out kal.id_valuta)) { kal.id_valuta = 1; }
                if (!decimal.TryParse(DTk.Rows[i]["fakturni_iznos"].ToString(), out kal.fakturni_iznos)) { kal.fakturni_iznos = 0; }
                if (!int.TryParse(DTk.Rows[i]["id_skladiste"].ToString(), out kal.id_skladiste)) { kal.id_skladiste = 1; }
                if (!int.TryParse(DTk.Rows[i]["id_zaposlenik"].ToString(), out kal.id_zaposlenik)) { kal.id_zaposlenik = 1; }
                kal.porez_potrosnja = DTk.Rows[i]["porez_potrosnja"].ToString() == "" ? "0" : DTk.Rows[i]["porez_potrosnja"].ToString();
                //*******************************************************************************************************************************************

                sql = "INSERT INTO kalkulacija (broj,id_partner,racun,otpremnica,racun_datum,otpremnica_datum,mjesto_troska," +
                    "datum,godina,id_kalkulacija,ukupno_vpc,ukupno_mpc,tecaj,id_valuta,fakturni_iznos,id_skladiste,id_zaposlenik,porez_potrosnja" +
                    ") VALUES (" +
                    "'" + kal.broj.ToString() + "'," +
                    "'" + kal.id_partner.ToString() + "'," +
                    "'" + kal.racun + "'," +
                    "'" + kal.otpremnica + "'," +
                    "'" + kal.racun_datum.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "'" + kal.otpremnica_datum.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "'" + kal.mjesto_troska + "'," +
                    "'" + kal.datum.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "'" + kal.godina + "'," +
                    "'" + kal.id_kalkulacija.ToString() + "'," +
                    "'" + kal.ukupno_vpc.ToString().Replace(".", ",") + "'," +
                    "'" + kal.ukupno_mpc.ToString().Replace(".", ",") + "'," +
                    "'" + kal.tecaj + "'," +
                    "'" + kal.id_valuta + "'," +
                    "'" + kal.fakturni_iznos.ToString().Replace(".", ",") + "'," +
                    "'" + kal.id_skladiste + "'," +
                    "'" + kal.id_zaposlenik + "'," +
                    "'" + kal.porez_potrosnja + "'" +
                    ")";
                classSQL.insert(sql);

                backgroundWorker1.ReportProgress(i);
            }
        }

        #endregion GetKalkulacije**********************************************

        #region GetKalkulacijeStavke****************************************

        public struct KalkulacijaStavke
        {
            public int id_stavka;
            public decimal kolicina;
            public decimal fak_cijena;
            public decimal rabat;
            public decimal prijevoz;
            public decimal carina;
            public decimal marza_postotak;
            public decimal porez;
            public decimal posebni_porez;
            public int broj;
            public string sifra;
            public decimal vpc;
            public int id_skladiste;
            public decimal porez_potrosnja;
            public int id_kalkulacija;
        }

        private void GetKalkulacijaStavke(DataTable DT, DoWorkEventArgs e)
        {
            WebReference.KonektorPostgres Server = new WebReference.KonektorPostgres();
            KalkulacijaStavke kal = new KalkulacijaStavke();
            string sql = "SELECT * FROM kalkulacija_stavke WHERE id_stavka>'" + DT.Rows[0]["kalkulacija_stavke"].ToString() + "';";
            DataTable DTk = Server.GetDataset(sql, "DT", DTpostavke.Rows[0]["korisnickoime"].ToString(), DTpostavke.Rows[0]["lozinka"].ToString()).Tables[0];
            ukupno_progres = DTk.Rows.Count;
            dokumenat = "KALKULACIJA STAVKE";
            for (int i = 0; i < DTk.Rows.Count; i++)
            {
                //*************************************************PROVJERA KALKULACIJE_STAVKE I SPREMANJE U STRUCT************************************************
                if (!int.TryParse(DTk.Rows[i]["id_stavka"].ToString(), out kal.id_stavka)) { kal.id_stavka = 1; }
                if (!decimal.TryParse(DTk.Rows[i]["kolicina"].ToString(), out kal.kolicina)) { kal.kolicina = 0; }
                if (!decimal.TryParse(DTk.Rows[i]["fak_cijena"].ToString(), out kal.fak_cijena)) { kal.fak_cijena = 0; }
                if (!decimal.TryParse(DTk.Rows[i]["rabat"].ToString(), out kal.rabat)) { kal.rabat = 0; }
                if (!decimal.TryParse(DTk.Rows[i]["prijevoz"].ToString(), out kal.prijevoz)) { kal.prijevoz = 0; }
                if (!decimal.TryParse(DTk.Rows[i]["carina"].ToString(), out kal.carina)) { kal.carina = 0; }
                if (!decimal.TryParse(DTk.Rows[i]["marza_postotak"].ToString(), out kal.marza_postotak)) { kal.marza_postotak = 0; }
                if (!decimal.TryParse(DTk.Rows[i]["porez"].ToString(), out kal.porez)) { kal.porez = 0; }
                if (!decimal.TryParse(DTk.Rows[i]["posebni_porez"].ToString(), out kal.posebni_porez)) { kal.posebni_porez = 0; }
                if (!int.TryParse(DTk.Rows[i]["broj"].ToString(), out kal.broj)) { kal.broj = 0; }
                kal.sifra = DTk.Rows[i]["sifra"].ToString();
                if (!decimal.TryParse(DTk.Rows[i]["vpc"].ToString(), out kal.vpc)) { kal.vpc = 0; }
                if (!int.TryParse(DTk.Rows[i]["id_skladiste"].ToString(), out kal.id_skladiste)) { kal.id_skladiste = 0; }
                if (!decimal.TryParse(DTk.Rows[i]["porez_potrosnja"].ToString(), out kal.posebni_porez)) { kal.posebni_porez = 0; }
                if (!int.TryParse(DTk.Rows[i]["id_kalkulacija"].ToString(), out kal.id_kalkulacija)) { kal.id_kalkulacija = 0; }
                //************************************************************************************************************************************************

                sql = "BEGIN; INSERT INTO kalkulacija_stavke (id_stavka,kolicina,fak_cijena,rabat,prijevoz,carina,marza_postotak,porez," +
                    "posebni_porez,broj,sifra,vpc,id_skladiste,porez_potrosnja,id_kalkulacija" +
                    ") VALUES (" +
                    "'" + kal.id_stavka.ToString() + "'," +
                    "'" + kal.kolicina.ToString().Replace(".", ",") + "'," +
                    "'" + kal.fak_cijena.ToString().Replace(".", ",") + "'," +
                    "'" + kal.rabat.ToString() + "'," +
                    "'" + kal.prijevoz.ToString().Replace(".", ",") + "'," +
                    "'" + kal.carina.ToString().Replace(".", ",") + "'," +
                    "'" + kal.marza_postotak.ToString() + "'," +
                    "'" + kal.porez.ToString() + "'," +
                    "'" + kal.posebni_porez.ToString().Replace(".", ",") + "'," +
                    "'" + kal.broj.ToString() + "'," +
                    "'" + kal.sifra + "'," +
                    "'" + kal.vpc.ToString().Replace(",", ".") + "'," +
                    "'" + kal.id_skladiste.ToString() + "'," +
                    "'" + kal.porez_potrosnja + "'," +
                    "'" + kal.id_kalkulacija.ToString() + "'" +
                    ");";

                sql += PromjenaNaSkladistu(kal.sifra,
                    kal.id_skladiste.ToString(),
                    null,
                    kal.kolicina.ToString(),
                    (kal.fak_cijena - (kal.fak_cijena * kal.rabat / 100)).ToString().Replace(".", ","),
                    kal.vpc.ToString().Replace(",", "."),
                    kal.porez.ToString(), true) +
                    " COMMIT;";

                classSQL.insert(sql);

                backgroundWorker1.ReportProgress(i);
            }
        }

        #endregion GetKalkulacijeStavke****************************************

        #region GetPartners**************************************************

        public struct Partners
        {
            public string ime_tvrtke;
            public int id_grad;
            public string adresa;
            public string r1r2;
            public string oib;
            public string napomena;
            public int id_djelatnost;
            public string zr;
            public int id_zemlja;
            public int id_partner;
            public string ime;
            public string prezime;
            public string email;
            public string tel;
            public string mob;
            public DateTime datum_rodenja;
            public decimal bodovi;
            public decimal popust;
            public string broj_kartice;
            public int aktivan;
            public int vrsta_korisnika;
            public int primanje_letaka;
            public int id_zupanija;
            public int godina_cestitke;
            public string oib_polje;
        }

        private void GetPartners(DataTable DT, DoWorkEventArgs e)
        {
            WebReference.KonektorPostgres Server = new WebReference.KonektorPostgres();
            Partners kal = new Partners();
            string sql = "SELECT * FROM partners WHERE id_partner>'" + DT.Rows[0]["partners"].ToString() + "';";
            DataTable DTk = Server.GetDataset(sql, "DT", DTpostavke.Rows[0]["korisnickoime"].ToString(), DTpostavke.Rows[0]["lozinka"].ToString()).Tables[0];
            ukupno_progres = DTk.Rows.Count;
            dokumenat = "PARTNERS";
            for (int i = 0; i < DTk.Rows.Count; i++)
            {
                //*************************************************PROVJERA KALKULACIJE_STAVKE I SPREMANJE U STRUCT************************************************
                kal.ime_tvrtke = DTk.Rows[i]["ime_tvrtke"].ToString();
                if (!int.TryParse(DTk.Rows[i]["id_grad"].ToString(), out kal.id_grad)) { kal.id_grad = 1; }
                kal.adresa = DTk.Rows[i]["adresa"].ToString();
                kal.r1r2 = DTk.Rows[i]["r1r2"].ToString();
                kal.oib = DTk.Rows[i]["oib"].ToString();
                kal.napomena = DTk.Rows[i]["napomena"].ToString();
                if (!int.TryParse(DTk.Rows[i]["id_djelatnost"].ToString(), out kal.id_djelatnost)) { kal.id_djelatnost = 0; }
                kal.zr = DTk.Rows[i]["zr"].ToString();
                if (!int.TryParse(DTk.Rows[i]["id_zemlja"].ToString(), out kal.id_zemlja)) { kal.id_zemlja = 0; }
                if (!int.TryParse(DTk.Rows[i]["id_partner"].ToString(), out kal.id_partner)) { kal.id_partner = 0; }
                kal.ime = DTk.Rows[i]["ime"].ToString();
                kal.prezime = DTk.Rows[i]["prezime"].ToString();
                kal.email = DTk.Rows[i]["email"].ToString();
                kal.tel = DTk.Rows[i]["tel"].ToString();
                kal.mob = DTk.Rows[i]["mob"].ToString();
                kal.zr = DTk.Rows[i]["zr"].ToString();
                if (!DateTime.TryParse(DTk.Rows[i]["datum_rodenja"].ToString(), out kal.datum_rodenja)) { kal.datum_rodenja = DateTime.Now; }
                if (!decimal.TryParse(DTk.Rows[i]["bodovi"].ToString(), out kal.bodovi)) { kal.bodovi = 0; }
                if (!decimal.TryParse(DTk.Rows[i]["popust"].ToString(), out kal.popust)) { kal.popust = 0; }
                //if (!int.TryParse(DTk.Rows[i]["broj_kartice"].ToString(), out kal.broj_kartice)) { kal.broj_kartice = 0; }
                kal.broj_kartice = DTk.Rows[i]["broj_kartice"].ToString();
                if (!int.TryParse(DTk.Rows[i]["aktivan"].ToString(), out kal.aktivan)) { kal.aktivan = 0; }
                if (!int.TryParse(DTk.Rows[i]["vrsta_korisnika"].ToString(), out kal.vrsta_korisnika)) { kal.vrsta_korisnika = 0; }
                if (!int.TryParse(DTk.Rows[i]["primanje_letaka"].ToString(), out kal.primanje_letaka)) { kal.primanje_letaka = 0; }
                if (!int.TryParse(DTk.Rows[i]["id_zupanija"].ToString(), out kal.id_zupanija)) { kal.id_zupanija = 0; }
                if (!int.TryParse(DTk.Rows[i]["godina_cestitke"].ToString(), out kal.godina_cestitke)) { kal.godina_cestitke = DateTime.Now.Year; }
                kal.oib_polje = DTk.Rows[i]["oib_polje"].ToString();

                kal.ime_tvrtke = kal.ime_tvrtke.Replace("\'", "");
                kal.ime_tvrtke = kal.ime_tvrtke.Replace("\"", "");
                kal.ime_tvrtke = kal.ime_tvrtke.Replace("&", " AND ");
                //************************************************************************************************************************************************

                sql = "INSERT INTO partners (ime_tvrtke,id_grad,adresa,r1r2,oib,napomena,id_djelatnost,zr," +
                    "id_zemlja,id_partner,ime,prezime,email,tel,mob,datum_rodenja,bodovi,popust,broj_kartice,aktivan," +
                    "vrsta_korisnika,primanje_letaka,id_zupanija,godina_cestitke,oib_polje" +
                    ") VALUES (" +
                    "'" + kal.ime_tvrtke + "'," +
                    "'" + kal.id_grad.ToString() + "'," +
                    "'" + kal.adresa + "'," +
                    "'" + kal.r1r2 + "'," +
                    "'" + kal.oib + "'," +
                    "'" + kal.napomena + "'," +
                    "'" + kal.id_djelatnost.ToString() + "'," +
                    "'" + kal.zr + "'," +
                    "'" + kal.id_zemlja.ToString() + "'," +
                    "'" + kal.id_partner.ToString() + "'," +
                    "'" + kal.ime + "'," +
                    "'" + kal.prezime + "'," +
                    "'" + kal.email + "'," +
                    "'" + kal.tel + "'," +
                    "'" + kal.mob + "'," +
                    "'" + kal.datum_rodenja.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "'" + kal.bodovi + "'," +
                    "'" + kal.popust + "'," +
                    "'" + kal.broj_kartice + "'," +
                    "'" + kal.aktivan + "'," +
                    "'" + kal.vrsta_korisnika + "'," +
                    "'" + kal.primanje_letaka + "'," +
                    "'" + kal.id_zupanija + "'," +
                    "'" + kal.godina_cestitke + "'," +
                    "'" + kal.oib_polje + "'" +
                    ")";
                classSQL.insert(sql);

                backgroundWorker1.ReportProgress(i);
            }
        }

        #endregion GetPartners**************************************************

        #region GetMeduskladisnica******************************************

        public struct Meduskladisnica
        {
            public string broj;
            public string godina;
            public int id_skladiste_od;
            public int id_skladiste_do;
            public DateTime datum;
            public int id_izradio;
            public string org_dokumenat;
            public string napomena;
            public int id;
        }

        private void GetMeduskladisnica(DataTable DT, DoWorkEventArgs e)
        {
            WebReference.KonektorPostgres Server = new WebReference.KonektorPostgres();
            Meduskladisnica mds = new Meduskladisnica();
            string sql = "SELECT * FROM meduskladisnica WHERE id>'" + DT.Rows[0]["meduskladisnica"].ToString() + "';";
            DataTable DTm = Server.GetDataset(sql, "DT", DTpostavke.Rows[0]["korisnickoime"].ToString(), DTpostavke.Rows[0]["lozinka"].ToString()).Tables[0];
            ukupno_progres = DTm.Rows.Count;
            dokumenat = "MEĐUSKLADIŠNICA";
            for (int i = 0; i < DTm.Rows.Count; i++)
            {
                //*************************************************PROVJERA meduskladisnice I SPREMANJE U STRUCT****************************************

                mds.broj = DTm.Rows[i]["broj"].ToString();
                mds.godina = DTm.Rows[i]["godina"].ToString();
                if (!int.TryParse(DTm.Rows[i]["id_skladiste_od"].ToString(), out mds.id_skladiste_od)) { mds.id_skladiste_od = 1; }
                if (!int.TryParse(DTm.Rows[i]["id_skladiste_do"].ToString(), out mds.id_skladiste_do)) { mds.id_skladiste_do = 1; }
                if (!DateTime.TryParse(DTm.Rows[i]["datum"].ToString(), out mds.datum)) { mds.datum = DateTime.Now; }
                if (!int.TryParse(DTm.Rows[i]["id_izradio"].ToString(), out mds.id_izradio)) { mds.id_izradio = 1; };
                mds.org_dokumenat = DTm.Rows[i]["org_dokumenat"].ToString();
                mds.napomena = DTm.Rows[i]["napomena"].ToString();
                int.TryParse(DTm.Rows[i]["id"].ToString(), out mds.id);
                //**************************************************************************************************************************************

                sql = "INSERT INTO meduskladisnica (broj,godina,id_skladiste_od,id_skladiste_do,datum,id_izradio,org_dokumenat,napomena,id)" +
                    " VALUES (" +
                    "'" + mds.broj + "'," +
                    "'" + mds.godina + "'," +
                    "'" + mds.id_skladiste_od.ToString() + "'," +
                    "'" + mds.id_skladiste_do.ToString() + "'," +
                    "'" + mds.datum.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "'" + mds.id_izradio.ToString() + "'," +
                    "'" + mds.org_dokumenat + "'," +
                    "'" + mds.napomena + "'," +
                    "'" + mds.id.ToString() + "'" +
                    ")";
                classSQL.insert(sql);

                backgroundWorker1.ReportProgress(i);
            }
        }

        #endregion GetMeduskladisnica******************************************

        #region GetMeduskladisnicaStavke***********************************

        public struct MeduskladisnicaStavke
        {
            public string sifra;
            public decimal mpc;
            public string pdv;
            public decimal vpc;
            public string kolicina;
            public string broj;
            public string godina;
            public int id_stavka;
            public decimal nbc;
            public string odjava;
            public int id_skladiste_od;
            public int id_skladiste_do;
        }

        private void GetMeduskladisnicaStavke(DataTable DT, DoWorkEventArgs e)
        {
            WebReference.KonektorPostgres Server = new WebReference.KonektorPostgres();
            MeduskladisnicaStavke mds = new MeduskladisnicaStavke();

            string sql = "SELECT meduskladisnica_stavke.sifra, " +
                " meduskladisnica_stavke.mpc," +
                " meduskladisnica_stavke.pdv," +
                " meduskladisnica_stavke.vpc," +
                " meduskladisnica_stavke.kolicina," +
                " meduskladisnica_stavke.broj," +
                " meduskladisnica_stavke.godina," +
                " meduskladisnica_stavke.id_stavka," +
                " meduskladisnica_stavke.nbc," +
                " meduskladisnica_stavke.odjava," +
                " meduskladisnica_stavke.id_stavka," +
                " meduskladisnica.id_skladiste_od," +
                " meduskladisnica.id_skladiste_do" +
                " FROM meduskladisnica_stavke " +
                " LEFT JOIN meduskladisnica ON meduskladisnica_stavke.broj=meduskladisnica.broj AND meduskladisnica_stavke.iz_skladista=meduskladisnica.id_skladiste_od" +
                " WHERE id_stavka>'" + DT.Rows[0]["meduskladisnica_stavke"].ToString() + "';";
            DataTable DTm = Server.GetDataset(sql, "DT", DTpostavke.Rows[0]["korisnickoime"].ToString(), DTpostavke.Rows[0]["lozinka"].ToString()).Tables[0];
            ukupno_progres = DTm.Rows.Count;
            dokumenat = "MEĐUSKLADIŠNICA STAVKE";
            for (int i = 0; i < DTm.Rows.Count; i++)
            {
                //*************************************************PROVJERA meduskladisnice I SPREMANJE U STRUCT****************************************
                mds.sifra = DTm.Rows[i]["sifra"].ToString();
                if (!decimal.TryParse(DTm.Rows[i]["mpc"].ToString(), out mds.mpc)) { mds.mpc = 0; }
                mds.pdv = DTm.Rows[i]["pdv"].ToString();
                if (!decimal.TryParse(DTm.Rows[i]["vpc"].ToString(), out mds.vpc)) { mds.vpc = 0; }
                mds.kolicina = DTm.Rows[i]["kolicina"].ToString();
                mds.broj = DTm.Rows[i]["broj"].ToString();
                mds.godina = DTm.Rows[i]["godina"].ToString();
                int.TryParse(DTm.Rows[i]["id_stavka"].ToString(), out mds.id_stavka);
                if (!decimal.TryParse(DTm.Rows[i]["nbc"].ToString(), out mds.nbc)) { mds.nbc = 1; }
                mds.odjava = DTm.Rows[i]["odjava"].ToString() == "" ? "0" : DTm.Rows[i]["odjava"].ToString();
                if (!int.TryParse(DTm.Rows[i]["id_skladiste_od"].ToString(), out mds.id_skladiste_od)) { mds.id_skladiste_od = 1; }
                if (!int.TryParse(DTm.Rows[i]["id_skladiste_do"].ToString(), out mds.id_skladiste_do)) { mds.id_skladiste_do = 1; }
                //**************************************************************************************************************************************

                sql = "BEGIN; INSERT INTO meduskladisnica_stavke (sifra,mpc,pdv,vpc,kolicina,broj,godina,id_stavka,nbc,odjava,iz_skladista)" +
                    " VALUES (" +
                    "'" + mds.sifra + "'," +
                    "'" + mds.mpc.ToString().Replace('.', ',') + "'," +
                    "'" + mds.pdv + "'," +
                    "'" + mds.vpc.ToString().Replace('.', ',') + "'," +
                    "'" + mds.kolicina + "'," +
                    "'" + mds.broj + "'," +
                    "'" + mds.godina + "'," +
                    "'" + mds.id_stavka.ToString() + "'," +
                    "'" + mds.nbc.ToString().Replace('.', ',') + "'," +
                    "'" + mds.odjava + "'," +
                    "'" + mds.id_skladiste_od.ToString() + "'" +
                    ");";

                sql += PromjenaNaSkladistu(mds.sifra,
                        mds.id_skladiste_do.ToString(),
                        mds.id_skladiste_od.ToString(),
                        mds.kolicina,
                        mds.nbc.ToString(),
                        mds.vpc.ToString(),
                        mds.pdv, false) +
                        " COMMIT;";

                classSQL.insert(sql);

                backgroundWorker1.ReportProgress(i);
            }
        }

        #endregion GetMeduskladisnicaStavke***********************************

        #region GetPrimke***************************************************

        public struct Primka
        {
            public int broj;
            public int id_partner;
            public string originalni_dokument;
            public int id_izradio;
            public DateTime datum;
            public string napomena;
            public int id_skladiste;
            public int id_primka;
            public string godina;
            public int id_mjesto;
        }

        private void GetPrimke(DataTable DT, DoWorkEventArgs e)
        {
            WebReference.KonektorPostgres Server = new WebReference.KonektorPostgres();
            Primka prm = new Primka();
            string sql = "SELECT * FROM primka WHERE id_primka>'" + DT.Rows[0]["primka"].ToString() + "';";
            DataTable DTp = Server.GetDataset(sql, "DT", DTpostavke.Rows[0]["korisnickoime"].ToString(), DTpostavke.Rows[0]["lozinka"].ToString()).Tables[0];
            ukupno_progres = DTp.Rows.Count;
            dokumenat = "PRIMKE";

            for (int i = 0; i < DTp.Rows.Count; i++)
            {
                //*************************************************PROVJERA meduskladisnice I SPREMANJE U STRUCT****************************************

                int.TryParse(DTp.Rows[i]["broj"].ToString(), out prm.broj);
                if (!int.TryParse(DTp.Rows[i]["id_partner"].ToString(), out prm.id_partner)) { prm.id_partner = 1; }
                prm.originalni_dokument = DTp.Rows[i]["originalni_dokument"].ToString();
                if (!int.TryParse(DTp.Rows[i]["id_izradio"].ToString(), out prm.id_izradio)) { prm.id_izradio = 1; }
                if (!DateTime.TryParse(DTp.Rows[i]["datum"].ToString(), out prm.datum)) { prm.datum = DateTime.Now; }
                prm.napomena = DTp.Rows[i]["napomena"].ToString();
                if (!int.TryParse(DTp.Rows[i]["id_skladiste"].ToString(), out prm.id_skladiste)) { prm.id_skladiste = 1; }
                int.TryParse(DTp.Rows[i]["id_primka"].ToString(), out prm.id_primka);
                prm.godina = DTp.Rows[i]["godina"].ToString();
                if (!int.TryParse(DTp.Rows[i]["id_mjesto"].ToString(), out prm.id_mjesto)) { prm.id_mjesto = 1; }
                //**************************************************************************************************************************************

                sql = "INSERT INTO primka (broj,id_partner,originalni_dokument,id_izradio,datum,napomena,id_skladiste,id_primka,godina,id_mjesto)" +
                    " VALUES (" +
                    "'" + prm.broj.ToString() + "'," +
                    "'" + prm.id_partner.ToString() + "'," +
                    "'" + prm.originalni_dokument + "'," +
                    "'" + prm.id_izradio.ToString() + "'," +
                    "'" + prm.datum.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "'" + prm.napomena + "'," +
                    "'" + prm.id_skladiste.ToString() + "'," +
                    "'" + prm.id_primka.ToString() + "'," +
                    "'" + prm.godina + "'," +
                    "'" + prm.id_mjesto.ToString() + "'" +
                    ")";
                classSQL.insert(sql);

                backgroundWorker1.ReportProgress(i);
            }
        }

        #endregion GetPrimke***************************************************

        #region GetPrimkaStavke*********************************************

        public struct PrimkaStavke
        {
            public string sifra;
            public decimal vpc;
            public decimal mpc;
            public string rabat;
            public int broj;
            public string kolicina;
            public decimal nbc;
            public int id_stavka;
            public string pdv;
            public int id_primka;
            public decimal ukupno;
            public decimal iznos;
            public int id_skladiste;
        }

        private void GetPrimkaStavke(DataTable DT, DoWorkEventArgs e)
        {
            WebReference.KonektorPostgres Server = new WebReference.KonektorPostgres();
            PrimkaStavke prm = new PrimkaStavke();

            string sql = "SELECT " +
                " primka_stavke.sifra," +
                " primka_stavke.vpc," +
                " primka_stavke.mpc," +
                " primka_stavke.rabat," +
                " primka_stavke.broj," +
                " primka_stavke.kolicina," +
                " primka_stavke.nbc," +
                " primka_stavke.id_stavka," +
                " primka_stavke.pdv," +
                " primka_stavke.id_primka," +
                " primka_stavke.ukupno," +
                " primka_stavke.iznos," +
                " primka.id_skladiste" +
                " FROM primka_stavke " +
                " LEFT JOIN primka ON primka.id_primka=primka_stavke.id_primka " +
                " WHERE primka_stavke.id_stavka>'" + DT.Rows[0]["primka_stavke"].ToString() + "';";
            DataTable DTp = Server.GetDataset(sql, "DT", DTpostavke.Rows[0]["korisnickoime"].ToString(), DTpostavke.Rows[0]["lozinka"].ToString()).Tables[0];
            ukupno_progres = DTp.Rows.Count;
            dokumenat = "PRIMKA STAVKE";
            for (int i = 0; i < DTp.Rows.Count; i++)
            {
                //*************************************************PROVJERA meduskladisnice I SPREMANJE U STRUCT****************************************
                prm.sifra = DTp.Rows[i]["sifra"].ToString();
                if (!decimal.TryParse(DTp.Rows[i]["vpc"].ToString(), out prm.vpc)) { prm.vpc = 1; }
                if (!decimal.TryParse(DTp.Rows[i]["mpc"].ToString(), out prm.mpc)) { prm.mpc = 1; }
                prm.rabat = DTp.Rows[i]["rabat"].ToString();
                int.TryParse(DTp.Rows[i]["broj"].ToString(), out prm.broj);
                prm.kolicina = DTp.Rows[i]["kolicina"].ToString();
                if (!decimal.TryParse(DTp.Rows[i]["nbc"].ToString(), out prm.nbc)) { prm.nbc = 1; }
                int.TryParse(DTp.Rows[i]["id_stavka"].ToString(), out prm.id_stavka);
                prm.pdv = DTp.Rows[i]["pdv"].ToString();
                if (!int.TryParse(DTp.Rows[i]["id_primka"].ToString(), out prm.id_primka)) { prm.id_primka = 1; }
                if (!decimal.TryParse(DTp.Rows[i]["ukupno"].ToString(), out prm.ukupno)) { prm.ukupno = 1; }
                if (!decimal.TryParse(DTp.Rows[i]["iznos"].ToString(), out prm.iznos)) { prm.iznos = 1; }
                if (!int.TryParse(DTp.Rows[i]["id_skladiste"].ToString(), out prm.id_skladiste)) { prm.id_skladiste = 1; }
                //**************************************************************************************************************************************

                sql = "BEGIN; INSERT INTO primka_stavke (sifra,vpc,mpc,rabat,broj,kolicina,nbc,id_stavka,pdv,id_primka,ukupno,iznos)" +
                    " VALUES (" +
                    "'" + prm.sifra + "'," +
                    "'" + prm.vpc.ToString().Replace(",", ".") + "'," +
                    "'" + prm.mpc.ToString().Replace(",", ".") + "'," +
                    "'" + prm.rabat + "'," +
                    "'" + prm.broj.ToString() + "'," +
                    "'" + prm.kolicina + "'," +
                    "'" + prm.nbc.ToString().Replace(",", ".") + "'," +
                    "'" + prm.id_stavka.ToString() + "'," +
                    "'" + prm.pdv + "'," +
                    "'" + prm.id_primka.ToString() + "'," +
                    "'" + prm.ukupno.ToString().Replace(",", ".") + "'," +
                    "'" + prm.iznos.ToString().Replace(",", ".") + "'" +
                    ");";

                sql += PromjenaNaSkladistu(prm.sifra,
                prm.id_skladiste.ToString(),
                null,
                prm.kolicina.ToString(),
                prm.nbc.ToString(),
                prm.vpc.ToString(),
                prm.pdv,
                false) +
                " COMMIT;";

                classSQL.insert(sql);

                backgroundWorker1.ReportProgress(i);
            }
        }

        #endregion GetPrimkaStavke*********************************************

        #region GetPovratRobe***************************************************

        public struct PovratRobe
        {
            public int broj;
            public int id_odrediste;
            public int id_partner;
            public string mjesto_troska;
            public string orginalni_dokument;
            public int id_izradio;
            public string napomena;
            public string godina;
            public DateTime datum;
            public int id_skladiste;
        }

        private void GetPovratRobe(DataTable DT, DoWorkEventArgs e)
        {
            WebReference.KonektorPostgres Server = new WebReference.KonektorPostgres();
            PovratRobe pr = new PovratRobe();
            string sql = "SELECT * FROM povrat_robe WHERE broj>'" + DT.Rows[0]["povrat_robe"].ToString() + "';";
            DataTable DTp = Server.GetDataset(sql, "DT", DTpostavke.Rows[0]["korisnickoime"].ToString(), DTpostavke.Rows[0]["lozinka"].ToString()).Tables[0];
            ukupno_progres = DTp.Rows.Count;
            dokumenat = "POVRATNICA DOBAVLJAČU";

            for (int i = 0; i < DTp.Rows.Count; i++)
            {
                //*************************************************PROVJERA meduskladisnice I SPREMANJE U STRUCT****************************************

                int.TryParse(DTp.Rows[i]["broj"].ToString(), out pr.broj);
                if (!int.TryParse(DTp.Rows[i]["id_odrediste"].ToString(), out pr.id_odrediste)) { pr.id_odrediste = 1; }
                if (!int.TryParse(DTp.Rows[i]["id_partner"].ToString(), out pr.id_partner)) { pr.id_partner = 1; }
                pr.mjesto_troska = DTp.Rows[i]["mjesto_troska"].ToString();
                pr.orginalni_dokument = DTp.Rows[i]["orginalni_dokument"].ToString();
                if (!int.TryParse(DTp.Rows[i]["id_izradio"].ToString(), out pr.id_izradio)) { pr.id_izradio = 1; }
                pr.napomena = DTp.Rows[i]["napomena"].ToString();
                pr.godina = DTp.Rows[i]["godina"].ToString();
                if (!DateTime.TryParse(DTp.Rows[i]["datum"].ToString(), out pr.datum)) { pr.datum = DateTime.Now; }
                if (!int.TryParse(DTp.Rows[i]["id_skladiste"].ToString(), out pr.id_skladiste)) { pr.id_skladiste = 1; }
                //**************************************************************************************************************************************

                sql = "INSERT INTO povrat_robe (broj,id_odrediste,id_partner,mjesto_troska,orginalni_dokument,id_izradio,napomena,godina,datum,id_skladiste)" +
                    " VALUES (" +
                    "'" + pr.broj.ToString() + "'," +
                    "'" + pr.id_odrediste.ToString() + "'," +
                    "'" + pr.id_partner.ToString() + "'," +
                    "'" + pr.mjesto_troska + "'," +
                    "'" + pr.orginalni_dokument + "'," +
                    "'" + pr.id_izradio.ToString() + "'," +
                    "'" + pr.napomena + "'," +
                    "'" + pr.godina + "'," +
                    "'" + pr.datum.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "'" + pr.id_skladiste.ToString() + "'" +
                    ")";
                classSQL.insert(sql);

                backgroundWorker1.ReportProgress(i);
            }
        }

        #endregion GetPovratRobe***************************************************

        #region GetPovratRobeStavke*********************************************

        public struct PovratRobeStavke
        {
            public string sifra;
            public decimal vpc;
            public decimal mpc;
            public string pdv;
            public string rabat;
            public int broj;
            public string kolicina;
            public int id_stavka;
            public decimal nbc;
            public int id_skladiste;
        }

        private void GetPovratRobeStavke(DataTable DT, DoWorkEventArgs e)
        {
            WebReference.KonektorPostgres Server = new WebReference.KonektorPostgres();
            PovratRobeStavke pr = new PovratRobeStavke();

            string sql = "SELECT " +
                " povrat_robe_stavke.sifra," +
                " povrat_robe_stavke.vpc," +
                " povrat_robe_stavke.mpc," +
                " povrat_robe_stavke.pdv," +
                " povrat_robe_stavke.rabat," +
                " povrat_robe_stavke.broj," +
                " povrat_robe_stavke.kolicina," +
                " povrat_robe_stavke.id_stavka," +
                " povrat_robe_stavke.nbc," +
                " povrat_robe.id_skladiste" +
                " FROM povrat_robe_stavke " +
                " LEFT JOIN povrat_robe ON povrat_robe.broj=povrat_robe_stavke.broj " +
                " WHERE povrat_robe_stavke.id_stavka>'" + DT.Rows[0]["povrat_robe_stavke"].ToString() + "';";
            DataTable DTp = Server.GetDataset(sql, "DT", DTpostavke.Rows[0]["korisnickoime"].ToString(), DTpostavke.Rows[0]["lozinka"].ToString()).Tables[0];
            ukupno_progres = DTp.Rows.Count;
            dokumenat = "POVRATNICA DOBAVLJAČU STAVKE";
            for (int i = 0; i < DTp.Rows.Count; i++)
            {
                //*************************************************PROVJERA meduskladisnice I SPREMANJE U STRUCT****************************************
                pr.sifra = DTp.Rows[i]["sifra"].ToString();
                if (!decimal.TryParse(DTp.Rows[i]["vpc"].ToString(), out pr.vpc)) { pr.vpc = 1; }
                if (!decimal.TryParse(DTp.Rows[i]["mpc"].ToString(), out pr.mpc)) { pr.mpc = 1; }
                pr.pdv = DTp.Rows[i]["pdv"].ToString();
                pr.rabat = DTp.Rows[i]["rabat"].ToString();
                int.TryParse(DTp.Rows[i]["broj"].ToString(), out pr.broj);
                pr.kolicina = DTp.Rows[i]["kolicina"].ToString();
                int.TryParse(DTp.Rows[i]["id_stavka"].ToString(), out pr.id_stavka);
                if (!decimal.TryParse(DTp.Rows[i]["nbc"].ToString(), out pr.nbc)) { pr.nbc = 1; }
                if (!int.TryParse(DTp.Rows[i]["id_skladiste"].ToString(), out pr.id_skladiste)) { pr.id_skladiste = 1; }
                //**************************************************************************************************************************************

                sql = "BEGIN; INSERT INTO povrat_robe_stavke (sifra,vpc,mpc,pdv,rabat,broj,kolicina,id_stavka,nbc)" +
                    " VALUES (" +
                    "'" + pr.sifra + "'," +
                    "'" + pr.vpc.ToString().Replace(",", ".") + "'," +
                    "'" + pr.mpc.ToString().Replace(",", ".") + "'," +
                    "'" + pr.pdv + "'," +
                    "'" + pr.rabat + "'," +
                    "'" + pr.broj.ToString() + "'," +
                    "'" + pr.kolicina + "'," +
                    "'" + pr.id_stavka.ToString() + "'," +
                    "'" + pr.nbc.ToString().Replace(",", ".") + "'" +
                    ");";

                sql += PromjenaNaSkladistu(pr.sifra,
                null,
                pr.id_skladiste.ToString(),
                pr.kolicina.ToString(),
                pr.nbc.ToString(),
                pr.vpc.ToString(),
                pr.pdv,
                false) +
                " COMMIT;";

                classSQL.insert(sql);

                backgroundWorker1.ReportProgress(i);
            }
        }

        #endregion GetPovratRobeStavke*********************************************

        #region BackgroundWorker ProgressChanged AND WorkerCompleted

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblProgres.Text = dokumenat + ", ažurirano " + (e.ProgressPercentage + 1).ToString() + " od " + ukupno_progres.ToString() + ".";
            if (dokumenat != label1.Text)
            {
                label1.Text = "Trenutno se sinkronizira: " + dokumenat;
            }
            progressBar1.Value = (int)(e.ProgressPercentage / ukupno_progres * 100); ;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Sinkronizacija je uspješno završena.", "Završeno", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        #endregion BackgroundWorker ProgressChanged AND WorkerCompleted

        #region Dodavanje i skidanje sa skladista

        private string PromjenaNaSkladistu(string sifra, string skladiste, string skladiste2, string kolicina, string nabavna, string vpc, string porez, bool mijenjaj_cijene)
        {
            bool DobroSkladiste = false;
            string oduzmi = "";
            for (int i = 0; i < ArrSkladiste.Length; i++)
            {
                if (ArrSkladiste[i] == skladiste)
                {
                    DobroSkladiste = true;
                    break;
                }
                else if (ArrSkladiste[i] == skladiste2)
                {
                    DobroSkladiste = true;
                    skladiste = skladiste2;
                    oduzmi = "-";
                    break;
                }
            }
            if (!DobroSkladiste)
            {
                return "";
            }

            string sql = "";
            DataTable DT = classSQL.select("SELECT * FROM roba_prodaja WHERE id_skladiste='" + skladiste + "' AND sifra='" + sifra + "'", "roba_prodaja").Tables[0];
            if (DT.Rows.Count == 0)
            {
                sql = "INSERT INTO roba_prodaja (id_skladiste,kolicina,nc,vpc,porez,sifra,porez_potrosnja) VALUES (" +
                    "'" + skladiste + "'," +
                    "'" + oduzmi + kolicina.Replace(".", ",") + "'," +
                    "'" + nabavna.Replace(".", ",") + "'," +
                    "'" + vpc.Replace(",", ".") + "'," +
                    "'" + porez + "'," +
                    "'" + sifra + "'," +
                    "'0'" +
                    ");";
                return sql;
            }
            else
            {
                if (mijenjaj_cijene)
                {
                    sql = "UPDATE " +
                        " roba_prodaja SET kolicina=REPLACE(CAST(CAST(REPLACE(kolicina,',','.') as NUMERIC)+(" + oduzmi + kolicina.Replace(",", ".") + ") AS varchar),'.',',')," +
                        " vpc='" + vpc.Replace(",", ".") + "'," +
                        " nc='" + nabavna.Replace(".", ",") + "', " +
                        " porez='" + porez + "'" +
                        " WHERE id_skladiste='" + skladiste + "' AND sifra='" + sifra + "';";
                }
                else
                {
                    sql = "UPDATE roba_prodaja SET kolicina=REPLACE(CAST(CAST(REPLACE(kolicina,',','.') as NUMERIC)+(" + oduzmi + kolicina.Replace(",", ".") + ") AS varchar),'.',',') WHERE id_skladiste='" + skladiste + "' AND sifra='" + sifra + "';";
                }
                return sql;
            }
        }

        #endregion Dodavanje i skidanje sa skladista

        #region Poravnavanje ID_SERIAL u Bazi

        private void PoravnavanjeIdSerialBazi()
        {
            string sql = "BEGIN; " +
            " SELECT setval('roba_prodaja_id_roba_prodaja_seq', (SELECT MAX(id_roba_prodaja) FROM roba_prodaja)+1); " +
            " SELECT setval('roba_id_roba_seq', (SELECT MAX(id_roba) FROM roba)+1); " +
            " SELECT setval('kalkulacija_id_kalkulacija_seq', (SELECT MAX(id_kalkulacija) FROM kalkulacija)+1); " +
            " SELECT setval('kalkulacija_stavke_id_stavka_seq', (SELECT MAX(id_stavka) FROM kalkulacija_stavke)+1); " +
            " SELECT setval('meduskladisnica_id_seq', (SELECT MAX(id) FROM meduskladisnica)+1); " +
            " SELECT setval('meduskladisnica_stavke_id_stavka_seq', (SELECT MAX(id_stavka) FROM meduskladisnica_stavke)+1); " +
            //" SELECT setval('primka_id_primka_seq', (SELECT MAX(id_primka) FROM primka)+1); "+
            //" SELECT setval('primka_stavke_id_stavka_seq', (SELECT MAX(id_stavka) FROM primka_stavke)+1); "+
            " SELECT setval('zaposlenici_id_zaposlenik_seq', (SELECT MAX(id_zaposlenik) FROM zaposlenici)+1); " +
            " SELECT setval('skladiste_id_skladiste_seq', (SELECT MAX(id_skladiste) FROM skladiste)+1); " +
            " SELECT setval('otpremnice_id_otpremnica_seq', (SELECT MAX(id_otpremnica) FROM otpremnice)+1); " +
            " SELECT setval('otpremnica_stavke_id_stavka_seq', (SELECT MAX(id_stavka) FROM otpremnica_stavke)+1); " +
            //" SELECT setval('povrat_robe_broj_seq', (SELECT MAX(broj) FROM povrat_robe)+1); " +
            " SELECT setval('povrat_robe_stavka_id_stavka_seq', (SELECT MAX(id_stavka) FROM povrat_robe_stavke)+1); " +
            " COMMIT;";

            classSQL.insert(sql);
        }

        #endregion Poravnavanje ID_SERIAL u Bazi
    }
}