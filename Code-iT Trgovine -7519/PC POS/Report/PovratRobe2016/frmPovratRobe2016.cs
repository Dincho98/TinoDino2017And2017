using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Report.Kalkulacija
{
    public partial class frmPovratRobe2016 : Form
    {
        public frmPovratRobe2016()
        {
            InitializeComponent();
        }

        public string broj_kalkulacije { get; set; }
        public string skladiste { get; set; }
        public bool pregled { get; set; }
        private string _dokument = "kalkulacija";
        public string dokument { get { return _dokument; } set { _dokument = value; } }
        private INIFile ini = new INIFile();

        private void frmKalkulacija_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);
            //if (dokument == "kalkulacija")
            //{
            //    SetKalkulacija();
            //}
            if (dokument == "povrat_dobavljacu")
            {
                SetPovratDobavljacu();
            }
            else if (dokument == "otpis_robe")
            {
                SetOtpisRobe();
            }

            string marza = "0";
            try
            {
                if (ini.Read("POSTAVKE", "inventura_nabavna") == "1")
                {
                    marza = "1";
                }
            }
            catch { }

            ReportParameter p1 = new ReportParameter("marza", marza);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1 });

            this.reportViewer1.RefreshReport();
        }

        private void SetPovratDobavljacu()
        {
            string filter = "";
            if (pregled == true)
            {
                filter = "kalkulacija.id_kalkulacija='" + broj_kalkulacije + "';";
            }
            else
            {
                filter = "povrat_robe.broj='" + broj_kalkulacije + "' AND povrat_robe.id_skladiste = (select id_skladiste from skladiste where skladiste = '" + skladiste + "');";
            }

            string SustavPdv = @"select * from partners where id_partner = (select id_partner from povrat_robe where broj = '" + broj_kalkulacije + @"' and id_skladiste =(select id_skladiste from skladiste where skladiste = '" + skladiste + "'))";

            DataSet dsPartner = classSQL.select(SustavPdv, "partners");
            bool partnerExist = false, uSustavuPdv = false;
            if (dsPartner != null && dsPartner.Tables.Count > 0 && dsPartner.Tables[0] != null && dsPartner.Tables[0].Rows.Count > 0)
            {
                partnerExist = true;
                uSustavuPdv = Convert.ToBoolean(dsPartner.Tables[0].Rows[0]["uSustavPdv"].ToString());
            }

            string sqlTecaj = @"SELECT povrat_robe.broj, povrat_robe.godina, skladiste.skladiste, 1 as tecaj, valute.ime_valute AS valuta
FROM povrat_robe
LEFT JOIN skladiste ON povrat_robe.id_skladiste=skladiste.id_skladiste
LEFT JOIN valute ON 5 = valute.id_valuta
LEFT JOIN partners ON povrat_robe.id_partner=partners.id_partner
WHERE " + filter + " ";

            DataTable dtTecaj = classSQL.select(sqlTecaj, "kalkulacija").Tables[0];

            double tecaj = 1;

            if (dtTecaj.Rows.Count > 0)
            {
                tecaj = Convert.ToDouble(dtTecaj.Rows[0]["tecaj"].ToString());
            }

            string naziv_fakture = " podaci_tvrtka.naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql("", naziv_fakture, "");

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }
            else
            {
                //classSQL.NpgAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
                classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }

            DataTable dtPdv = new DataTable();
            dtPdv.Columns.Add("stopa");
            dtPdv.Columns.Add("iznos");
            dtPdv.Columns.Add("osnovica");

            string filter1 = "";
            if (pregled == true)
            {
                filter1 = "povrat_robe_stavke.id_kalkulacija='" + broj_kalkulacije + "'";
            }
            else
            {
                filter1 = "povrat_robe_stavke.broj='" + broj_kalkulacije + "' ";
            }

            string sql_stavke = "SELECT " +
                " povrat_robe_stavke.sifra," +
                " povrat_robe_stavke.id_stavka AS id," +
                " roba.naziv," +
                " roba.jm AS jmj," +
                " povrat_robe_stavke.kolicina," +
                " povrat_robe_stavke.rabat," +
                " povrat_robe_stavke.nbc as fak_cijena," +
                " 0 as prijevoz," +
                " 0 as carina," +
                " 0 as posebni_porez," +
                " round(((povrat_robe_stavke.vpc / povrat_robe_stavke.nbc::numeric) - 1) * 100, 6) as marza," +
                " povrat_robe_stavke.vpc," +
                " povrat_robe_stavke.pdv AS pdv" +
                " FROM povrat_robe_stavke" +
                " LEFT JOIN roba ON povrat_robe_stavke.sifra=roba.sifra" +
                " WHERE " + filter1 + " ORDER BY CAST(id_stavka AS INT) ASC";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_stavke).Fill(dSkalkulacija_stavke, "DTkalkDtavke");
            }
            else
            {
                classSQL.NpgAdatpter(sql_stavke).Fill(dSkalkulacija_stavke, "DTkalkDtavke");
            }

            DataTable DT = dSkalkulacija_stavke.Tables[0];
            double fakIznos = 0;
            double fak_iznos_stavka = 0;
            double fakNetto = 0;
            double osnovica = 0;
            double fakPdv = 0;
            double fak_ukupno_stavka = 0;
            double fakUk = 0;
            double fak_cijena = 0;
            double kolicina = 0;
            double rabat = 0;
            double rabatStavka = 0;
            double rabatUk = 0;
            double mpc = 0;
            double mpc_uk = 0;
            double porez = 0;
            double vpc = 0;
            double nab_cijena = 0;
            double posebni_porez = 0;
            double prijevoz = 0;
            double carina = 0;
            double net_cijena = 0;
            double marza = 0;
            double marzaIznos = 0;
            double marzaUk = 0;
            double nabavna_zadnja = 0;
            double marza_uk_ = 0;
            double fak_cijena_S_pdv = 0;
            double fak_cijena_S_pdv_ukp = 0;

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                kolicina = Math.Round(Convert.ToDouble(DT.Rows[i]["kolicina"].ToString()), 3) * (-1);
                fak_cijena = Math.Round(Convert.ToDouble(DT.Rows[i]["fak_cijena"].ToString()), 3);
                rabat = Math.Round(Convert.ToDouble(DT.Rows[i]["rabat"].ToString()), 3);
                porez = Math.Round(Convert.ToDouble(DT.Rows[i]["pdv"].ToString()), 3);
                vpc = Math.Round(Convert.ToDouble(DT.Rows[i]["vpc"].ToString()), 3);
                posebni_porez = Math.Round(Convert.ToDouble(DT.Rows[i]["posebni_porez"].ToString()), 3);
                prijevoz = Math.Round(Convert.ToDouble(DT.Rows[i]["prijevoz"].ToString()), 4);
                carina = Math.Round(Convert.ToDouble(DT.Rows[i]["carina"].ToString()), 3);
                marza = Math.Round(Convert.ToDouble(DT.Rows[i]["marza"].ToString()), 8);

                if (marza.ToString() == "NaN" || marza.ToString() == "Infinity")
                    marza = 0;
                double vpc_s_kol = vpc * kolicina;
                double fak_s_kol = fak_cijena * kolicina;
                nabavna_zadnja = Math.Round((fak_s_kol - (fak_s_kol * (rabat / 100)) + ((prijevoz + carina) * kolicina)), 3);
                marza_uk_ = Math.Round(nabavna_zadnja * (marza / 100), 3);
                marzaIznos = Math.Round(marza / 100 * nabavna_zadnja, 3);
                marzaUk += marzaIznos;

                rabatStavka = fak_cijena * kolicina * rabat / 100;
                rabatUk += rabatStavka;

                mpc = vpc * (1 + porez / 100.0);

                mpc_uk = mpc * kolicina;

                nab_cijena = kolicina * fak_cijena * (1 - rabat / 100) - posebni_porez + ((prijevoz + carina) * kolicina);
                //if (Util.Korisno.oibTvrtke == "41109922301") {
                //    nab_cijena = fak_cijena * (1 - rabat / 100) - posebni_porez + ((prijevoz + carina));
                //}

                fak_cijena_S_pdv = nab_cijena * (1 + (porez / 100));
                if (partnerExist && !uSustavuPdv)
                    fak_cijena_S_pdv = nab_cijena;

                fak_cijena_S_pdv_ukp += fak_cijena_S_pdv;

                net_cijena = kolicina * fak_cijena * (1 - rabat / 100);

                fak_iznos_stavka = kolicina * fak_cijena;
                fakIznos += fak_iznos_stavka;

                fakNetto += fak_iznos_stavka * (1 - rabat / 100);

                fak_ukupno_stavka = vpc * (1 + porez / 100) * kolicina;
                fakUk += fak_ukupno_stavka;

                osnovica += fak_ukupno_stavka * 100 / (100 + porez);
                fakPdv += fak_ukupno_stavka * (1 - 100 / (100 + porez));
                //fak_pdv += fak_ukupno_stavka / (1 - porez);

                //double stopa = ((mpc - vpc) / vpc);
                double pdvStavka_cisto = (nab_cijena) * (porez / 100);
                if (partnerExist && !uSustavuPdv)
                    pdvStavka_cisto = 0;
                StopePDVa(Math.Round((decimal)porez, 4), Math.Round((decimal)pdvStavka_cisto, 4), Math.Round(((decimal)nab_cijena), 4));

                DT.Rows[i].SetField("mpc_uk", Math.Round(mpc_uk, 3));
                DT.Rows[i].SetField("mpc", Math.Round(mpc, 3));
                DT.Rows[i].SetField("rabat", Math.Round(rabat, 3));
                DT.Rows[i].SetField("rabat_uk", Math.Round(rabatStavka, 3));
                DT.Rows[i].SetField("marza", Math.Round(marza, 3));
                DT.Rows[i].SetField("marzaIznos", Math.Round(marza_uk_, 3));
                DT.Rows[i].SetField("posebni_porez", posebni_porez);
                DT.Rows[i].SetField("prijevoz", Math.Round(prijevoz, 4));
                DT.Rows[i].SetField("carina", Math.Round(carina, 3));

                DT.Rows[i].SetField("kolicina", Math.Round(kolicina, 3));
                DT.Rows[i].SetField("fak_cijena", Math.Round(fak_cijena, 3));
                DT.Rows[i].SetField("fak_cijena_val", Math.Round(fak_cijena / tecaj, 3));
                DT.Rows[i].SetField("net_cijena", Math.Round(net_cijena, 3));
                DT.Rows[i].SetField("nab_cijena", Math.Round(nab_cijena, 3));
                DT.Rows[i].SetField("fak_cijena_S_pdv", Math.Round(fak_cijena_S_pdv, 3));

                //DT.Rows[i].SetField("nab_porez", Math.Round(nab_cijena, 3));
            }

            fakIznos = Math.Round(fakIznos, 3);
            fakNetto = Math.Round(fakNetto, 3);
            fakPdv = Math.Round(fakPdv, 3);
            osnovica = Math.Round(osnovica, 3);
            fakUk = Math.Round(fakUk, 3);
            rabatUk = Math.Round(rabatUk, 3);
            marzaUk = Math.Round(marzaUk, 3);
            string filter2 = "";
            if (pregled == true)
            {
                filter2 = "kalkulacija.id_kalkulacija='" + broj_kalkulacije + "';";
            }
            else
            {
                filter2 = "povrat_robe.broj='" + broj_kalkulacije + "' AND povrat_robe.id_skladiste=(select id_skladiste from skladiste where skladiste ='" + skladiste + "');";
            }
            string sql_kalk = @"SELECT povrat_robe.broj, povrat_robe.id_partner, povrat_robe.godina, 'Povrat robe' AS naslov, skladiste.skladiste, povrat_robe.datum,
partners.ime_tvrtke + ' ' + partners.id_partner AS dobavljac, 1 AS tecaj, '' as racun, '' as otpremnica,
povrat_robe.datum AS otpremnica_datum, 1 as tecaj, zaposlenici.ime + ' ' + zaposlenici.prezime AS kalkulirao,
povrat_robe.datum AS racun_datum, valute.ime_valute AS valuta, '" + fakIznos + @"' AS fak_iznos, '" + fakNetto + @"' AS netto_fak_iznos,
'" + fakPdv + @"' AS pdv, '" + rabatUk + @"' AS rabatUk, '" + marzaUk + @"' AS marzaUk, '" + osnovica + @"' AS osnovica, '" + fakUk + @"' AS ukupno,
'" + Math.Round(fak_cijena_S_pdv_ukp, 2) + @"' as fak_cijena_S_pdv, case when partners.uSustavPdv = true then 'Dobavljač u sustavu PDV-a: DA' else 'Dobavljač u sustavu PDV-a: NE' end as sustavPdv
FROM povrat_robe
LEFT JOIN skladiste ON povrat_robe.id_skladiste=skladiste.id_skladiste
LEFT JOIN zaposlenici ON povrat_robe.id_izradio=zaposlenici.id_zaposlenik
LEFT JOIN valute ON 5 = valute.id_valuta
LEFT JOIN partners ON povrat_robe.id_partner=partners.id_partner
WHERE " + filter2;

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_kalk).Fill(dSkalkulacija, "DTKalkulacijA");
            }
            else
            {
                classSQL.NpgAdatpter(sql_kalk).Fill(dSkalkulacija, "DTKalkulacijA");
            }
        }

        private void SetOtpisRobe()
        {
            string filter = "";
            if (pregled == true)
            {
                filter = "kalkulacija.id_kalkulacija='" + broj_kalkulacije + "';";
            }
            else
            {
                filter = "otpis_robe.broj='" + broj_kalkulacije + "' AND otpis_robe.id_skladiste = (select id_skladiste from skladiste where skladiste = '" + skladiste + "');";
            }

            string SustavPdv = @"select * from partners where id_partner = (select id_partner from otpis_robe where broj = '" + broj_kalkulacije + @"' and id_skladiste =(select id_skladiste from skladiste where skladiste = '" + skladiste + "'))";

            DataSet dsPartner = null;// classSQL.select(SustavPdv, "partners");
            bool partnerExist = false, uSustavuPdv = false;
            if (dsPartner != null && dsPartner.Tables.Count > 0 && dsPartner.Tables[0] != null && dsPartner.Tables[0].Rows.Count > 0)
            {
                partnerExist = true;
                uSustavuPdv = Convert.ToBoolean(dsPartner.Tables[0].Rows[0]["uSustavPdv"].ToString());
            }

            string sqlTecaj = @"SELECT otpis_robe.broj, otpis_robe.godina, skladiste.skladiste, 1 as tecaj, valute.ime_valute AS valuta
                                FROM otpis_robe
                                LEFT JOIN skladiste ON otpis_robe.id_skladiste=skladiste.id_skladiste
                                LEFT JOIN valute ON 5 = valute.id_valuta
                                WHERE " + filter + " ";

            DataTable dtTecaj = classSQL.select(sqlTecaj, "kalkulacija").Tables[0];

            double tecaj = 1;

            if (dtTecaj.Rows.Count > 0)
            {
                tecaj = Convert.ToDouble(dtTecaj.Rows[0]["tecaj"].ToString());
            }

            string naziv_fakture = " podaci_tvrtka.naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql("", naziv_fakture, "");

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }
            else
            {
                //classSQL.NpgAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
                classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }

            DataTable dtPdv = new DataTable();
            dtPdv.Columns.Add("stopa");
            dtPdv.Columns.Add("iznos");
            dtPdv.Columns.Add("osnovica");

            string filter1 = "";
            if (pregled == true)
            {
                filter1 = "otpis_robe_stavke.id_kalkulacija='" + broj_kalkulacije + "'";
            }
            else
            {
                filter1 = "otpis_robe_stavke.broj='" + broj_kalkulacije + "' ";
            }

            string sql_stavke = "SELECT " +
                " otpis_robe_stavke.sifra," +
                " otpis_robe_stavke.id_stavka AS id," +
                " roba.naziv," +
                " roba.jm AS jmj," +
                " otpis_robe_stavke.kolicina," +
                " otpis_robe_stavke.rabat," +
                " otpis_robe_stavke.nbc as fak_cijena," +
                " 0 as prijevoz," +
                " 0 as carina," +
                " 0 as posebni_porez," +
                " round(case when otpis_robe_stavke.nbc::numeric = 0::numeric then 0 else ((otpis_robe_stavke.vpc /  otpis_robe_stavke.nbc::numeric ) - 1) * 100 end, 6) as marza," +
                " otpis_robe_stavke.vpc," +
                " otpis_robe_stavke.pdv AS pdv" +
                " FROM otpis_robe_stavke" +
                " LEFT JOIN roba ON otpis_robe_stavke.sifra=roba.sifra" +
                " WHERE " + filter1 + " ORDER BY CAST(id_stavka AS INT) ASC";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_stavke).Fill(dSkalkulacija_stavke, "DTkalkDtavke");
            }
            else
            {
                classSQL.NpgAdatpter(sql_stavke).Fill(dSkalkulacija_stavke, "DTkalkDtavke");
            }

            DataTable DT = dSkalkulacija_stavke.Tables[0];
            double fakIznos = 0;
            double fak_iznos_stavka = 0;
            double fakNetto = 0;
            double osnovica = 0;
            double fakPdv = 0;
            double fak_ukupno_stavka = 0;
            double fakUk = 0;
            double fak_cijena = 0;
            double kolicina = 0;
            double rabat = 0;
            double rabatStavka = 0;
            double rabatUk = 0;
            double mpc = 0;
            double mpc_uk = 0;
            double porez = 0;
            double vpc = 0;
            double nab_cijena = 0;
            double posebni_porez = 0;
            double prijevoz = 0;
            double carina = 0;
            double net_cijena = 0;
            double marza = 0;
            double marzaIznos = 0;
            double marzaUk = 0;
            double nabavna_zadnja = 0;
            //double marza_uk_ = 0;
            double fak_cijena_S_pdv = 0;
            double fak_cijena_S_pdv_ukp = 0;

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                kolicina = Math.Round(Convert.ToDouble(DT.Rows[i]["kolicina"].ToString().Replace('.', ',')), 3) * (-1);
                fak_cijena = Math.Round(Convert.ToDouble(DT.Rows[i]["fak_cijena"].ToString()), 3);
                rabat = Math.Round(Convert.ToDouble(DT.Rows[i]["rabat"].ToString()), 3);
                porez = Math.Round(Convert.ToDouble(DT.Rows[i]["pdv"].ToString()), 3);
                vpc = Math.Round(Convert.ToDouble(DT.Rows[i]["vpc"].ToString()), 3);
                posebni_porez = Math.Round(Convert.ToDouble(DT.Rows[i]["posebni_porez"].ToString()), 3);
                prijevoz = Math.Round(Convert.ToDouble(DT.Rows[i]["prijevoz"].ToString()), 4);
                carina = Math.Round(Convert.ToDouble(DT.Rows[i]["carina"].ToString()), 3);
                marza = Math.Round(Convert.ToDouble(DT.Rows[i]["marza"].ToString()), 8);

                if (marza.ToString() == "NaN" || marza.ToString() == "Infinity")
                    marza = 0;
                double vpc_s_kol = vpc * kolicina;
                double fak_s_kol = fak_cijena * kolicina;
                nabavna_zadnja = Math.Round((fak_s_kol - (fak_s_kol * (rabat / 100)) + ((prijevoz + carina) * kolicina)), 3);
                marzaIznos = Math.Round(nabavna_zadnja * (marza / 100), 3);

                if (marza == 0 && fak_cijena == 0)
                {
                    marzaIznos = Math.Round((vpc - (vpc * (rabat / 100))) * kolicina, 3);
                }
                //marzaIznos = Math.Round(marza / 100 * nabavna_zadnja, 3);
                marzaUk += marzaIznos;

                rabatStavka = fak_cijena * kolicina * rabat / 100;
                rabatUk += rabatStavka;

                mpc = vpc * (1 + porez / 100.0);

                mpc_uk = mpc * kolicina;

                nab_cijena = kolicina * fak_cijena * (1 - rabat / 100) - posebni_porez + ((prijevoz + carina) * kolicina);
                //if (Util.Korisno.oibTvrtke == "41109922301") {
                //    nab_cijena = fak_cijena * (1 - rabat / 100) - posebni_porez + ((prijevoz + carina));
                //}

                fak_cijena_S_pdv = nab_cijena * (1 + (porez / 100));
                if (partnerExist && !uSustavuPdv)
                    fak_cijena_S_pdv = nab_cijena;

                fak_cijena_S_pdv_ukp += fak_cijena_S_pdv;

                net_cijena = kolicina * fak_cijena * (1 - rabat / 100);

                fak_iznos_stavka = kolicina * fak_cijena;
                fakIznos += fak_iznos_stavka;

                fakNetto += fak_iznos_stavka * (1 - rabat / 100);

                fak_ukupno_stavka = (vpc * (1 + porez / 100)) * kolicina;
                fakUk += fak_ukupno_stavka;

                osnovica += fak_ukupno_stavka * 100 / (100 + porez);
                fakPdv += fak_ukupno_stavka * (1 - 100 / (100 + porez));
                //fak_pdv += fak_ukupno_stavka / (1 - porez);

                //double stopa = ((mpc - vpc) / vpc);
                double pdvStavka_cisto = (nab_cijena) * (porez / 100);
                double pdvVpc = vpc * (porez / 100);
                if (partnerExist && !uSustavuPdv)
                {
                    pdvStavka_cisto = 0;
                    pdvVpc = 0;
                }

                StopePDVa(Math.Round((decimal)porez, 4), Math.Round((decimal)pdvStavka_cisto, 4), Math.Round(((decimal)nab_cijena), 4), "roba");
                StopePDVa(Math.Round((decimal)porez, 4), Math.Round((decimal)pdvVpc, 4), Math.Round(((decimal)vpc_s_kol), 4), "mpc");

                DT.Rows[i].SetField("mpc_uk", Math.Round(mpc_uk, 3));
                DT.Rows[i].SetField("mpc", Math.Round(mpc, 3));
                DT.Rows[i].SetField("rabat", Math.Round(rabat, 3));
                DT.Rows[i].SetField("rabat_uk", Math.Round(rabatStavka, 3));
                DT.Rows[i].SetField("marza", Math.Round(marza, 3));
                DT.Rows[i].SetField("marzaIznos", Math.Round(marzaIznos, 3));
                DT.Rows[i].SetField("posebni_porez", posebni_porez);
                DT.Rows[i].SetField("prijevoz", Math.Round(prijevoz, 4));
                DT.Rows[i].SetField("carina", Math.Round(carina, 3));

                DT.Rows[i].SetField("kolicina", Math.Round(kolicina, 3));
                DT.Rows[i].SetField("fak_cijena", Math.Round(fak_cijena, 3));
                DT.Rows[i].SetField("fak_cijena_val", Math.Round(fak_cijena / tecaj, 3));
                DT.Rows[i].SetField("net_cijena", Math.Round(net_cijena, 3));
                DT.Rows[i].SetField("nab_cijena", Math.Round(nab_cijena, 3));
                DT.Rows[i].SetField("fak_cijena_S_pdv", Math.Round(fak_cijena_S_pdv, 3));

                //DT.Rows[i].SetField("nab_porez", Math.Round(nab_cijena, 3));
            }

            fakIznos = Math.Round(fakIznos, 3);
            fakNetto = Math.Round(fakNetto, 3);
            fakPdv = Math.Round(fakPdv, 3);
            osnovica = Math.Round(osnovica, 3);
            fakUk = Math.Round(fakUk, 3);
            fakPdv = fakUk - osnovica;
            rabatUk = Math.Round(rabatUk, 3);
            marzaUk = Math.Round(marzaUk, 3);
            string filter2 = "";
            if (pregled == true)
            {
                filter2 = "kalkulacija.id_kalkulacija='" + broj_kalkulacije + "';";
            }
            else
            {
                filter2 = "otpis_robe.broj='" + broj_kalkulacije + "' AND otpis_robe.id_skladiste=(select id_skladiste from skladiste where skladiste ='" + skladiste + "');";
            }
            string sql_kalk = @"SELECT otpis_robe.broj,  0 as id_partner, otpis_robe.godina, 'Otpis robe' AS naslov, skladiste.skladiste, otpis_robe.datum,
'' AS dobavljac, 1 AS tecaj, otpis_robe.napomena as racun, '' as otpremnica,
otpis_robe.datum AS otpremnica_datum, 1 as tecaj, zaposlenici.ime + ' ' + zaposlenici.prezime AS kalkulirao,
otpis_robe.datum AS racun_datum, valute.ime_valute AS valuta, '" + fakIznos + @"' AS fak_iznos, '" + fakNetto + @"' AS netto_fak_iznos,
'" + fakPdv + @"' AS pdv, '" + rabatUk + @"' AS rabatUk, '" + marzaUk + @"' AS marzaUk, '" + osnovica + @"' AS osnovica, '" + fakUk + @"' AS ukupno,
'" + Math.Round(fak_cijena_S_pdv_ukp, 2) + @"' as fak_cijena_S_pdv, '' as sustavPdv
FROM otpis_robe
LEFT JOIN skladiste ON otpis_robe.id_skladiste=skladiste.id_skladiste
LEFT JOIN zaposlenici ON otpis_robe.id_izradio=zaposlenici.id_zaposlenik
LEFT JOIN valute ON 5 = valute.id_valuta
-- LEFT JOIN partners ON otpis_robe.id_partner=partners.id_partner
WHERE " + filter2;

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_kalk).Fill(dSkalkulacija, "DTKalkulacijA");
            }
            else
            {
                classSQL.NpgAdatpter(sql_kalk).Fill(dSkalkulacija, "DTKalkulacijA");
            }
        }

        private void SetKalkulacija()
        {
            string filter = "";
            if (pregled == true)
            {
                filter = "kalkulacija.id_kalkulacija='" + broj_kalkulacije + "';";
            }
            else
            {
                filter = "kalkulacija.broj='" + broj_kalkulacije + "' AND kalkulacija.id_skladiste='" + skladiste + "';";
            }

            string SustavPdv = @"select * from partners where id_partner = (select id_partner from kalkulacija where broj = '" + broj_kalkulacije + @"' and id_skladiste = '" + skladiste + "')";

            DataSet dsPartner = classSQL.select(SustavPdv, "partners");
            bool partnerExist = false, uSustavuPdv = false;
            if (dsPartner != null && dsPartner.Tables.Count > 0 && dsPartner.Tables[0] != null && dsPartner.Tables[0].Rows.Count > 0)
            {
                partnerExist = true;
                uSustavuPdv = Convert.ToBoolean(dsPartner.Tables[0].Rows[0]["uSustavPdv"].ToString());
            }

            string sqlTecaj = @"SELECT kalkulacija.broj, kalkulacija.godina, skladiste.skladiste, kalkulacija.tecaj, valute.ime_valute AS valuta
FROM kalkulacija
LEFT JOIN skladiste ON kalkulacija.id_skladiste=skladiste.id_skladiste
LEFT JOIN valute ON kalkulacija.id_valuta=valute.id_valuta
LEFT JOIN partners ON kalkulacija.id_partner=partners.id_partner
WHERE " + filter + " ";

            DataTable dtTecaj = classSQL.select(sqlTecaj, "kalkulacija").Tables[0];

            double tecaj = 1;

            if (dtTecaj.Rows.Count > 0)
            {
                tecaj = Convert.ToDouble(dtTecaj.Rows[0]["tecaj"].ToString());
            }

            string naziv_fakture = " podaci_tvrtka.naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql("", naziv_fakture, "");

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }
            else
            {
                //classSQL.NpgAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
                classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }

            DataTable dtPdv = new DataTable();
            dtPdv.Columns.Add("stopa");
            dtPdv.Columns.Add("iznos");
            dtPdv.Columns.Add("osnovica");

            string filter1 = "";
            if (pregled == true)
            {
                filter1 = "kalkulacija_stavke.id_kalkulacija='" + broj_kalkulacije + "'";
            }
            else
            {
                filter1 = "kalkulacija_stavke.broj='" + broj_kalkulacije + "' AND kalkulacija_stavke.id_skladiste='" + skladiste + "'";
            }

            string sql_stavke = "SELECT " +
                " kalkulacija_stavke.sifra," +
                " kalkulacija_stavke.id_stavka AS id," +
                " roba.naziv," +
                " roba.jm AS jmj," +
                " kalkulacija_stavke.kolicina," +
                " kalkulacija_stavke.rabat," +
                " kalkulacija_stavke.fak_cijena," +
                " kalkulacija_stavke.prijevoz," +
                " kalkulacija_stavke.carina," +
                " kalkulacija_stavke.posebni_porez," +
                " kalkulacija_stavke.marza_postotak as marza," +
                " kalkulacija_stavke.vpc," +
                " kalkulacija_stavke.porez AS pdv" +
                " FROM kalkulacija_stavke" +
                " LEFT JOIN roba ON kalkulacija_stavke.sifra=roba.sifra" +
                " WHERE " + filter1 + " ORDER BY CAST(id_stavka AS INT) ASC";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_stavke).Fill(dSkalkulacija_stavke, "DTkalkDtavke");
            }
            else
            {
                classSQL.NpgAdatpter(sql_stavke).Fill(dSkalkulacija_stavke, "DTkalkDtavke");
            }

            DataTable DT = dSkalkulacija_stavke.Tables[0];
            double fakIznos = 0;
            double fak_iznos_stavka = 0;
            double fakNetto = 0;
            double osnovica = 0;
            double fakPdv = 0;
            double fak_ukupno_stavka = 0;
            double fakUk = 0;
            double fak_cijena = 0;
            double kolicina = 0;
            double rabat = 0;
            double rabatStavka = 0;
            double rabatUk = 0;
            double mpc = 0;
            double mpc_uk = 0;
            double porez = 0;
            double vpc = 0;
            double nab_cijena = 0;
            double posebni_porez = 0;
            double prijevoz = 0;
            double carina = 0;
            double net_cijena = 0;
            double marza = 0;
            double marzaIznos = 0;
            double marzaUk = 0;
            double nabavna_zadnja = 0;
            double marza_uk_ = 0;
            double fak_cijena_S_pdv = 0;
            double fak_cijena_S_pdv_ukp = 0;

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                kolicina = Math.Round(Convert.ToDouble(DT.Rows[i]["kolicina"].ToString()), 3);
                fak_cijena = Math.Round(Convert.ToDouble(DT.Rows[i]["fak_cijena"].ToString()), 3);
                rabat = Math.Round(Convert.ToDouble(DT.Rows[i]["rabat"].ToString()), 3);
                porez = Math.Round(Convert.ToDouble(DT.Rows[i]["pdv"].ToString()), 3);
                vpc = Math.Round(Convert.ToDouble(DT.Rows[i]["vpc"].ToString()), 3);
                posebni_porez = Math.Round(Convert.ToDouble(DT.Rows[i]["posebni_porez"].ToString()), 3);
                prijevoz = Math.Round(Convert.ToDouble(DT.Rows[i]["prijevoz"].ToString()), 4);
                carina = Math.Round(Convert.ToDouble(DT.Rows[i]["carina"].ToString()), 3);
                marza = Math.Round(Convert.ToDouble(DT.Rows[i]["marza"].ToString()), 8);

                if (marza.ToString() == "NaN" || marza.ToString() == "Infinity")
                    marza = 0;
                double vpc_s_kol = vpc * kolicina;
                double fak_s_kol = fak_cijena * kolicina;
                nabavna_zadnja = Math.Round((fak_s_kol - (fak_s_kol * (rabat / 100)) + ((prijevoz + carina) * kolicina)), 3);
                marza_uk_ = Math.Round(nabavna_zadnja * (marza / 100), 3);
                marzaIznos = Math.Round(marza / 100 * nabavna_zadnja, 3);
                marzaUk += marzaIznos;

                rabatStavka = fak_cijena * kolicina * rabat / 100;
                rabatUk += rabatStavka;

                mpc = vpc * (1 + porez / 100.0);

                mpc_uk = mpc * kolicina;

                nab_cijena = kolicina * fak_cijena * (1 - rabat / 100) - posebni_porez + ((prijevoz + carina) * kolicina);
                //if (Util.Korisno.oibTvrtke == "41109922301") {
                //    nab_cijena = fak_cijena * (1 - rabat / 100) - posebni_porez + ((prijevoz + carina));
                //}

                fak_cijena_S_pdv = nab_cijena * (1 + (porez / 100));
                if (partnerExist && !uSustavuPdv)
                    fak_cijena_S_pdv = nab_cijena;

                fak_cijena_S_pdv_ukp += fak_cijena_S_pdv;

                net_cijena = kolicina * fak_cijena * (1 - rabat / 100);

                fak_iznos_stavka = kolicina * fak_cijena;
                fakIznos += fak_iznos_stavka;

                fakNetto += fak_iznos_stavka * (1 - rabat / 100);

                fak_ukupno_stavka = vpc * (1 + porez / 100) * kolicina;
                fakUk += fak_ukupno_stavka;

                osnovica += fak_ukupno_stavka * 100 / (100 + porez);
                fakPdv += fak_ukupno_stavka * (1 - 100 / (100 + porez));
                //fak_pdv += fak_ukupno_stavka / (1 - porez);

                //double stopa = ((mpc - vpc) / vpc);
                double pdvStavka_cisto = (nab_cijena) * (porez / 100);
                if (partnerExist && !uSustavuPdv)
                    pdvStavka_cisto = 0;
                StopePDVa(Math.Round((decimal)porez, 4), Math.Round((decimal)pdvStavka_cisto, 4), Math.Round(((decimal)nab_cijena), 4));

                DT.Rows[i].SetField("mpc_uk", Math.Round(mpc_uk, 3));
                DT.Rows[i].SetField("mpc", Math.Round(mpc, 3));
                DT.Rows[i].SetField("rabat", Math.Round(rabat, 3));
                DT.Rows[i].SetField("rabat_uk", Math.Round(rabatStavka, 3));
                DT.Rows[i].SetField("marza", Math.Round(marza, 3));
                DT.Rows[i].SetField("marzaIznos", Math.Round(marza_uk_, 3));
                DT.Rows[i].SetField("posebni_porez", posebni_porez);
                DT.Rows[i].SetField("prijevoz", Math.Round(prijevoz, 4));
                DT.Rows[i].SetField("carina", Math.Round(carina, 3));

                DT.Rows[i].SetField("kolicina", Math.Round(kolicina, 3));
                DT.Rows[i].SetField("fak_cijena", Math.Round(fak_cijena, 3));
                DT.Rows[i].SetField("fak_cijena_val", Math.Round(fak_cijena / tecaj, 3));
                DT.Rows[i].SetField("net_cijena", Math.Round(net_cijena, 3));
                DT.Rows[i].SetField("nab_cijena", Math.Round(nab_cijena, 3));
                DT.Rows[i].SetField("fak_cijena_S_pdv", Math.Round(fak_cijena_S_pdv, 3));

                //DT.Rows[i].SetField("nab_porez", Math.Round(nab_cijena, 3));
            }

            fakIznos = Math.Round(fakIznos, 3);
            fakNetto = Math.Round(fakNetto, 3);
            fakPdv = Math.Round(fakPdv, 3);
            osnovica = Math.Round(osnovica, 3);
            fakUk = Math.Round(fakUk, 3);
            rabatUk = Math.Round(rabatUk, 3);
            marzaUk = Math.Round(marzaUk, 3);
            string filter2 = "";
            if (pregled == true)
            {
                filter2 = "kalkulacija.id_kalkulacija='" + broj_kalkulacije + "';";
            }
            else
            {
                filter2 = "kalkulacija.broj='" + broj_kalkulacije + "' AND kalkulacija.id_skladiste='" + skladiste + "';";
            }
            string sql_kalk = @"SELECT kalkulacija.broj, kalkulacija.id_partner, kalkulacija.godina, 'Kalkulacija' AS naslov, skladiste.skladiste, kalkulacija.datum,
partners.ime_tvrtke + ' ' + partners.id_partner AS dobavljac, kalkulacija.tecaj AS tecaj, kalkulacija.racun, kalkulacija.otpremnica,
CAST(kalkulacija.otpremnica_datum AS date) AS otpremnica_datum, kalkulacija.tecaj, zaposlenici.ime + ' ' + zaposlenici.prezime AS kalkulirao,
CAST(kalkulacija.racun_datum AS date) AS racun_datum, valute.ime_valute AS valuta, '" + fakIznos + @"' AS fak_iznos, '" + fakNetto + @"' AS netto_fak_iznos,
'" + fakPdv + @"' AS pdv, '" + rabatUk + @"' AS rabatUk, '" + marzaUk + @"' AS marzaUk, '" + osnovica + @"' AS osnovica, '" + fakUk + @"' AS ukupno,
'" + Math.Round(fak_cijena_S_pdv_ukp, 2) + @"' as fak_cijena_S_pdv, case when partners.uSustavPdv = true then 'Dobavljač u sustavu PDV-a: DA' else 'Dobavljač u sustavu PDV-a: NE' end as sustavPdv
FROM kalkulacija
LEFT JOIN skladiste ON kalkulacija.id_skladiste=skladiste.id_skladiste
LEFT JOIN zaposlenici ON kalkulacija.id_zaposlenik=zaposlenici.id_zaposlenik
LEFT JOIN valute ON kalkulacija.id_valuta=valute.id_valuta
LEFT JOIN partners ON kalkulacija.id_partner=partners.id_partner
WHERE " + filter2;

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_kalk).Fill(dSkalkulacija, "DTKalkulacijA");
            }
            else
            {
                classSQL.NpgAdatpter(sql_kalk).Fill(dSkalkulacija, "DTKalkulacijA");
            }
        }

        private void StopePDVa(decimal pdv, decimal iznos, decimal osnovica, string vrsta = null)
        {
            DataRow[] dataROW = dSstope.Tables["DTstope"].Select("stopa = '" + Math.Round(pdv).ToString() + "' AND vrsta ='" + vrsta + "'");

            if (dataROW.Count() == 0)
            {
                RowPdv = dSstope.Tables["DTstope"].NewRow();
                RowPdv["stopa"] = Math.Round(pdv, 0);
                RowPdv["iznos"] = Math.Round(iznos, 3);
                RowPdv["osnovica"] = Math.Round(osnovica, 3);
                RowPdv["vrsta"] = vrsta;
                dSstope.Tables["DTstope"].Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["iznos"] = Convert.ToDecimal(dataROW[0]["iznos"].ToString()) + Math.Round(iznos, 3);
                dataROW[0]["osnovica"] = Convert.ToDecimal(dataROW[0]["osnovica"].ToString()) + Math.Round(osnovica, 3);
            }
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
        }

        private void dTRpodaciTvrtkeBindingSource_CurrentChanged(object sender, EventArgs e)
        {
        }

        public DataRow RowPdv { get; set; }
    }
}