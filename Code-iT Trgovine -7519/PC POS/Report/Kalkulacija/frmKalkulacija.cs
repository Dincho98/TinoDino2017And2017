using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Report.Kalkulacija
{
    public partial class frmKalkulacija : Form
    {
        public frmKalkulacija()
        {
            InitializeComponent();
        }

        public string broj_kalkulacije { get; set; }
        public string skladiste { get; set; }
        public bool pregled { get; set; }
        private INIFile ini = new INIFile();

        private void frmKalkulacija_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            SetDS();

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
            ReportParameter p4 = new ReportParameter("prodaja_automobila", Class.Postavke.prodaja_automobila.ToString());
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p4 });

            this.reportViewer1.RefreshReport();
        }

        private void SetDS()
        {
            string filter = "", sql = "";
            if (pregled == true)
            {
                filter = "kalkulacija.id_kalkulacija='" + broj_kalkulacije + "'";
                sql = @"select coalesce(sum(pn.iznos * replace(ks.kolicina, ',','.')::numeric), 0) as povratna_naknada
from povratna_naknada pn
left join kalkulacija_stavke ks on pn.sifra = ks.sifra
left join kalkulacija k on ks.broj = k.broj and ks.id_skladiste = k.id_skladiste
where k.broj = '" + broj_kalkulacije + "' and pn.iznos <> 0;";
            }
            else
            {
                filter = "kalkulacija.broj='" + broj_kalkulacije + "' AND kalkulacija.id_skladiste='" + skladiste + "'";

                sql = @"select coalesce(sum(pn.iznos * replace(ks.kolicina, ',','.')::numeric), 0) as povratna_naknada
from povratna_naknada pn
left join kalkulacija_stavke ks on pn.sifra = ks.sifra
left join kalkulacija k on ks.broj = k.broj and ks.id_skladiste = k.id_skladiste
where k.broj = '" + broj_kalkulacije + "' and k.id_skladiste = '" + skladiste + "' and pn.iznos <> 0;";
            }

            DataSet dsPovratnaNaknada = classSQL.select(sql, "povratna_naknada");
            decimal povratna_naknada = 0;

            if (dsPovratnaNaknada != null && dsPovratnaNaknada.Tables.Count > 0 && dsPovratnaNaknada.Tables[0] != null && dsPovratnaNaknada.Tables[0].Rows.Count > 0)
                povratna_naknada = Convert.ToDecimal(dsPovratnaNaknada.Tables[0].Rows[0][0]);

            ReportParameter p2 = new ReportParameter("povratna_naknada", povratna_naknada.ToString());
            //this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p2 });

            string sqlTecaj = "SELECT " +
                " kalkulacija.broj," +
                " kalkulacija.godina," +
                " skladiste.skladiste," +
                " kalkulacija.tecaj," +
                " valute.ime_valute AS valuta" +
                " FROM kalkulacija" +
                " LEFT JOIN skladiste ON kalkulacija.id_skladiste=skladiste.id_skladiste" +
                " LEFT JOIN valute ON kalkulacija.id_valuta=valute.id_valuta" +
                " LEFT JOIN partners ON kalkulacija.id_partner=partners.id_partner WHERE " + filter + " ";

            DataTable dtTecaj = classSQL.select(sqlTecaj, "kalkulacija").Tables[0];

            //string

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
                " kalkulacija_stavke.ppmv," +
                " kalkulacija_stavke.posebni_porez," +
                " kalkulacija_stavke.marza_postotak as marza," +
                " kalkulacija_stavke.vpc," +
                " kalkulacija_stavke.porez AS pdv" +
                " FROM kalkulacija_stavke" +
                " LEFT JOIN roba ON kalkulacija_stavke.sifra = roba.sifra" +
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
            double ppmv = 0;
            double ppmv_ukp = 0;

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
                marza = Math.Round(Convert.ToDouble(DT.Rows[i]["marza"].ToString()), 3);
                ppmv = Math.Round(Convert.ToDouble(DT.Rows[i]["ppmv"].ToString()), 3);

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
                mpc += ppmv;

                mpc_uk = mpc * kolicina;
                ppmv_ukp += (ppmv * kolicina);

                nab_cijena = kolicina * fak_cijena * (1 - rabat / 100) - posebni_porez + ((prijevoz + carina) * kolicina);
                if (Util.Korisno.oibTvrtke == "41109922301")
                {
                    nab_cijena = fak_cijena * (1 - rabat / 100) - posebni_porez + ((prijevoz + carina));
                }

                net_cijena = kolicina * fak_cijena * (1 - rabat / 100);

                fak_iznos_stavka = kolicina * fak_cijena;
                fakIznos += fak_iznos_stavka;

                fakNetto += fak_iznos_stavka * (1 - rabat / 100);

                fak_ukupno_stavka = vpc * (1 + porez / 100) * kolicina;
                fakUk += fak_ukupno_stavka;

                osnovica += fak_ukupno_stavka * 100 / (100 + porez);
                fakPdv += fak_ukupno_stavka * (1 - 100 / (100 + porez));
                //fak_pdv += fak_ukupno_stavka / (1 - porez);

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

                //DT.Rows[i].SetField("nab_porez", Math.Round(nab_cijena, 3));
            }

            fakIznos = Math.Round(fakIznos, 3);
            fakNetto = Math.Round(fakNetto, 3);
            fakPdv = Math.Round(fakPdv, 3);
            osnovica = Math.Round(osnovica, 3);
            fakUk = Math.Round((fakUk + ppmv_ukp), 3);
            rabatUk = Math.Round(rabatUk, 3);
            marzaUk = Math.Round(marzaUk, 3);
            string filter2 = "";
            if (pregled == true)
            {
                filter2 = "kalkulacija.id_kalkulacija='" + broj_kalkulacije + "'";
            }
            else
            {
                filter2 = "kalkulacija.broj='" + broj_kalkulacije + "' AND kalkulacija.id_skladiste='" + skladiste + "'";
            }
            string sql_kalk = "SELECT " +
            " kalkulacija.broj," +
            " kalkulacija.id_partner," +
            " kalkulacija.godina," +
            " 'Kalkulacija' AS naslov," +
            " skladiste.skladiste," +
            " kalkulacija.datum," +
            " partners.ime_tvrtke + ' ' + partners.id_partner AS dobavljac," +
            " kalkulacija.tecaj AS tecaj," +
            " kalkulacija.racun," +
            " kalkulacija.otpremnica," +
            " CAST(kalkulacija.otpremnica_datum AS date) AS otpremnica_datum," +
            " kalkulacija.tecaj," +
            " zaposlenici.ime + ' ' + zaposlenici.prezime AS kalkulirao," +
            " CAST(kalkulacija.racun_datum AS date) AS racun_datum," +
            " valute.ime_valute AS valuta," +
            " '" + fakIznos + "' AS fak_iznos," +
            " '" + fakNetto + "' AS netto_fak_iznos," +
            " '" + fakPdv + "' AS pdv," +
            " '" + rabatUk + "' AS rabatUk," +
            " '" + marzaUk + "' AS marzaUk," +
            " '" + osnovica + "' AS osnovica," +
            " '" + fakUk + "' AS ukupno" +
            " FROM kalkulacija" +
            " LEFT JOIN skladiste ON kalkulacija.id_skladiste=skladiste.id_skladiste" +
            " LEFT JOIN zaposlenici ON kalkulacija.id_zaposlenik=zaposlenici.id_zaposlenik" +
            " LEFT JOIN valute ON kalkulacija.id_valuta=valute.id_valuta" +
            " LEFT JOIN partners ON kalkulacija.id_partner=partners.id_partner WHERE " + filter2 + "";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_kalk).Fill(dSkalkulacija, "DTKalkulacijA");
            }
            else
            {
                classSQL.NpgAdatpter(sql_kalk).Fill(dSkalkulacija, "DTKalkulacijA");
            }

            ReportParameter p3 = new ReportParameter("ppmv", ppmv_ukp.ToString("0.00"));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p2, p3 });
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
        }

        private void dTRpodaciTvrtkeBindingSource_CurrentChanged(object sender, EventArgs e)
        {
        }
    }
}