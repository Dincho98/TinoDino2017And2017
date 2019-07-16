using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Report.IFB
{
    public partial class repFakturaIFB : Form
    {
        public repFakturaIFB()
        {
            InitializeComponent();
        }

        public bool racunajTecaj { get; set; }
        public string broj_dokumenta { get; set; }
        public string dokumenat { get; set; }
        public string from_skladiste { get; set; }
        public string ImeForme { get; set; }

        private double osnovica_ukupno = 0;
        private double SveUkupno = 0;
        private double pdv_ukupno = 0;
        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private void repFaktura_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

            this.Text = ImeForme;
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            //dokumenat = "OTP";
            //broj_dokumenta = "1";
            //from_skladiste = "3";
            string root = "";
            if (DTpostavke.Rows[0]["logo"].ToString() == "1")
            {
                root = DTpostavke.Rows[0]["logopath"].ToString();
            }
            else
            {
                root = "c:/";
            }
            string sas = "";
            if (File.Exists("Sas.txt"))
            {
                sas = "1";
            }
            else
            {
                sas = "0";
            }
            string tekst = "";
            if (DTpostavke.Rows[0]["sustav_pdv"].ToString() == "0")
            {
                //tekst = "PDV nije uračunat u cijenu prema\r\nčl.90. st.2. zakona o PDV-u.\r\n";
                tekst = "PDV nije uračunat u cijenu prema\r\nčl.90. st.1. i st.2. NN 106/18 zakona o PDV-u.\r\n";
            }
            else
            {
                tekst = " ";
            }

            string drzava = "Hrvatska";
            string kn = racunajTecaj ? " " : " kn";
            string valut = " Select ifb.id_valuta, valute.ime_valute AS valuta FROM ifb " +
                            " LEFT JOIN valute ON valute.id_valuta=ifb.id_valuta WHERE ifb.broj='" + broj_dokumenta + "'";
            DataTable DTvalut = classSQL.select(valut, "valute").Tables[0];
            if (racunajTecaj)
            {
                if (DTvalut.Rows[0]["valuta"].ToString() == "978 EUR")
                    kn = " €";
                if (DTvalut.Rows[0]["valuta"].ToString() == "036 AUD")
                    kn = " $";
                if (DTvalut.Rows[0]["valuta"].ToString() == "840 USD")
                    kn = " $";
                if (DTvalut.Rows[0]["valuta"].ToString() == "756 CHF")
                    kn = " CHF";
            }

            ReportParameter p1 = new ReportParameter("path", root);
            ReportParameter p2 = new ReportParameter("sas", sas);
            ReportParameter p3 = new ReportParameter("sustav_pdv", tekst);
            ReportParameter p4 = new ReportParameter("kn", AkoJeStranaValuta(broj_dokumenta));
            ReportParameter p5 = new ReportParameter("iznos_valuta_kn", "0");
            ReportParameter p6 = new ReportParameter("oib", Class.PodaciTvrtka.oibTvrtke);

            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6 });

            if (dokumenat == "IFB")
            {
                if (broj_dokumenta == null) { return; }
                FillIFB(broj_dokumenta);
            }
        }

        private string AkoJeStranaValuta(string broj)
        {
            DataTable DT = classSQL.select("SELECT valute.naziv, ime_valute,ifb.valuta,ifb.ukupno FROM ifb,valute WHERE broj= '" + broj + "'" +
                    " AND ifb.id_valuta=valute.id_valuta", "ifb").Tables[0];

            if (DT.Rows.Count > 0)
            {
                decimal tecaj, ukupno;
                if (!decimal.TryParse(DT.Rows[0]["valuta"].ToString(), out tecaj))
                {
                    tecaj = 1;
                }

                if (!decimal.TryParse(DT.Rows[0]["ukupno"].ToString(), out ukupno))
                {
                    ukupno = 1;
                }
                if (DT.Rows[0]["naziv"].ToString() != "")
                {
                    return "Prema tečaju " + tecaj.ToString() + " ukupna faktura iznosi " + Math.Round(ukupno / tecaj, 3).ToString("#0.00") + " " + DT.Rows[0]["naziv"].ToString();
                }
                else
                {
                    return " ";
                }
            }
            else
            {
                return " ";
            }
        }

        private void FillIFB(string broj)
        {
            PCPOS.classNumberToLetter broj_u_text = new PCPOS.classNumberToLetter();

            string sql2 = "SELECT " +
                " ifb_stavke.kolicina," +
                " ifb_stavke.vpc," +
                " ifb_stavke.porez," +
                " ifb_stavke.broj," +
                " ifb_stavke.rabat," +
                " ifb_stavke.jmj AS jm," +
                " ifb_stavke.naziv as naziv" +
                " FROM ifb_stavke" +
                " WHERE ifb_stavke.broj='" + broj + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }
            else
            {
                classSQL.NpgAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }

            double vpc = 0;
            //double kol_stavka = 0;
            double pdv = 0;
            double kol = 0;
            double mpc = 0;
            double rabat = 0;
            double mpc_stavka = 0;
            double rabat_stavka = 0;
            double pdv_stavka = 0;
            double osnovica_stavka = 0;
            double RabatSve = 0;
            double ukupno_stav = 0;
            double pdvStavka = 0;
            double stopa = 0;
            double mpcStavka = 0;
            double rabatStavka = 0;
            double osnovicaStavka = 0;
            double cista_cijena_sveukupno = 0;

            DataTable DT = dSRfakturaStavke.Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                vpc = Convert.ToDouble(DT.Rows[i]["vpc"].ToString());
                //kol_stavka = Convert.ToDouble(DT.Rows[i]["kolicina"].ToString());
                pdv = Convert.ToDouble(DT.Rows[i]["porez"].ToString());
                rabat = Convert.ToDouble(DT.Rows[i]["rabat"].ToString());
                kol = Convert.ToDouble(DT.Rows[i]["kolicina"].ToString());
                mpc = vpc * (1 + (pdv / 100));
                ukupno_stav = vpc * kol - vpc * kol * rabat / 100;
                rabat_stavka = vpc * kol * rabat / 100;
                rabatStavka = mpc * kol * rabat / 100;
                mpc_stavka = vpc * kol - rabat_stavka;
                pdv_stavka = mpc_stavka - mpc_stavka / (1 + pdv / 100);
                osnovica_stavka = mpc_stavka;

                mpcStavka = Math.Round(mpc * kol - rabatStavka, 2);
                osnovicaStavka = mpcStavka;
                double preracunataSto = (100 * pdv) / (100 + pdv);
                pdvStavka = Math.Round(mpcStavka * (preracunataSto / 100), 3);
                stopa = ((mpc - vpc) / vpc);

                dSRfakturaStavke.Tables[0].Rows[i].SetField("mpcStavka", mpc_stavka);
                dSRfakturaStavke.Tables[0].Rows[i].SetField("rabatStavka", rabat_stavka);
                dSRfakturaStavke.Tables[0].Rows[i].SetField("mpc", mpc);
                dSRfakturaStavke.Tables[0].Rows[i].SetField("kolicina", kol.ToString("#0.00"));

                RabatSve = rabat_stavka + RabatSve;
                osnovica_ukupno = Math.Round(osnovica_stavka, 2) + osnovica_ukupno;
                pdv_ukupno = Math.Round(pdvStavka, 2) + pdv_ukupno;
                SveUkupno = mpcStavka + SveUkupno;

                StopePDVa(Math.Round((stopa * 100), 0), Math.Round(pdvStavka, 2), Math.Round(osnovica_stavka, 2));
            }

            string broj_slovima = broj_u_text.PretvoriBrojUTekst(SveUkupno.ToString(), ',', "kn", "lp").ToString();

            string sql = "SELECT " +
                " ifb.broj," +
                " ifb.datum AS datum," +
                " ifb.datum_dvo AS datum_dvo," +
                " ifb.datum_valute," +
                " ifb.jir," +
                " ifb.zki," +
                " ifb.mj_troska AS mjesto_troska," +
                " nacin_placanja.naziv_placanja AS placanje," +
                " ifb.otprema AS otprema," +
                " CAST (ifb.model AS nvarchar) + '  '+ CAST (ifb.broj AS nvarchar)+CAST (ifb.godina AS nvarchar)+'-'+CAST (ifb.odrediste AS nvarchar) AS model," +
                " ifb.napomena," +
                " '" + mpc + "' AS string4," +
                " '" + RabatSve + "' AS rabat," +
                " '" + SveUkupno + "' AS ukupno," +
                " '" + pdv_ukupno + "' AS iznos_pdv," +
                " '" + osnovica_ukupno + "' AS osnovica," +
                " '" + ukupno_stav + "' AS string5," +
                " ifb.godina," +
                //" CAST (ifb.broj AS nvarchar) +'/'+ CAST (ifb.godina AS nvarchar) AS Naslov," +
                " CAST (ifb.broj AS nvarchar) + CAST ('" + Util.Korisno.VratiDucanIBlagajnuZaIspis(4) + "' AS nvarchar)  AS Naslov," +
                " partners.ime_tvrtke AS kupac_tvrtka," +
                " 'Datum isporuke:' AS naziv_date1," +
                " 'Datum dospijeća:' AS naziv_date2," +
                " 'OVO NIJE FISKALIZIRAN RAČUN' AS string3," +
                " partners.adresa AS kupac_adresa," +
                " partners.id_grad AS id_kupac_grad," +
                " partners.id_partner AS sifra_kupac," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio," +
                " '" + broj_slovima.ToLower() + "' AS broj_slovima," +
                " partners.oib AS kupac_oib" +
                " FROM ifb" +
                " LEFT JOIN zacrnjeni_partner as  partners ON partners.id_partner=ifb.odrediste" +
                " LEFT JOIN nacin_placanja ON nacin_placanja.id_placanje=ifb.id_nacin_placanja" +
                //" LEFT JOIN ziro_racun ON ziro_racun.id_ziroracun=fakture.zr" +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=ifb.id_zaposlenik WHERE ifb.broj='" + broj + "'" +
                "";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql).Fill(dSFaktura, "DTRfaktura");
            }
            else
            {
                classSQL.NpgAdatpter(sql.Replace("nvarchar", "varchar")).Fill(dSFaktura, "DTRfaktura");
            }

            string dodatak = "";

            if (DTpostavke.Rows[0]["oslobodenje_pdva"].ToString() == "1" && pdv_ukupno == 0)
                dodatak = "\r\n\r\nPrijenos porezne obveze prema čl. 75., st. 3b. i čl. 79. Zakona o PDV-u.\r\nKupac će sam obračunati PDV od 25% i iskazati ga u poreznoj prijavi.\r\nObračun prema naplaćenoj naknadi.";
            else if (DTpostavke.Rows[0]["oslobodenje_pdva"].ToString() == "1")
                dodatak = "\r\n\r\nObračun prema naplaćenoj naknadi";

            dSFaktura.Tables[0].Rows[0]["napomena"] = dSFaktura.Tables[0].Rows[0]["napomena"] + dodatak;

            string id_kupac_grad = "";
            string id_kupac = "";
            if (dSFaktura.Tables[0].Rows.Count > 0)
            {
                id_kupac_grad = dSFaktura.Tables[0].Rows[0]["id_kupac_grad"].ToString();
                id_kupac = dSFaktura.Tables[0].Rows[0]["sifra_kupac"].ToString();
            }
            else
            {
                MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            string naziv_fakture = " podaci_tvrtka.naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql(id_kupac_grad, naziv_fakture, id_kupac);

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }
            else
            {
                classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

                if (dSRpodaciTvrtke.Tables[0].Rows.Count > 0)
                {
                    string iban = dSRpodaciTvrtke.Tables[0].Rows[0]["iban"].ToString();
                    string[] ibans = iban.Split(';');
                    string ibanR = "";
                    foreach (string s in ibans)
                    {
                        ibanR += s + "\r\n";
                    }
                    if (ibanR.Length > 4)
                    {
                        ibanR = ibanR.Remove(ibanR.Length - 2);
                    }
                    dSRpodaciTvrtke.Tables[0].Rows[0]["iban"] = ibanR;
                }
            }

            DateTime datum_ = Convert.ToDateTime(dSFaktura.Tables[0].Rows[0]["datum"].ToString());
            reportViewer1.LocalReport.DisplayName = "IFB-" + dSFaktura.Tables[0].Rows[0]["broj"].ToString() + "-" + datum_.ToString("dd-MM-yyyy") + "";

            decimal u_kune_iz_valute = 0;
            string u_kunama = "";

            if (racunajTecaj)
            {
                u_kune_iz_valute = Math.Round(Convert.ToDecimal(cista_cijena_sveukupno), 2);
                u_kunama = u_kune_iz_valute.ToString() + " kn";
            }
            else
            {
                u_kunama = "0 kn";
            }

            ReportParameter p7 = new ReportParameter("iznos_valuta_kn", u_kunama);

            this.reportViewer1.RefreshReport();
        }

        private DataRow RowPdv;

        private void StopePDVa(double pdv, double iznos, double osnovica)
        {
            DataRow[] dataROW = dSstope.Tables["DTstope"].Select("stopa = '" + pdv.ToString() + "'");

            if (dataROW.Count() == 0)
            {
                RowPdv = dSstope.Tables["DTstope"].NewRow();
                RowPdv["stopa"] = Math.Round(pdv, 0);
                RowPdv["iznos"] = Math.Round(iznos, 3);
                RowPdv["osnovica"] = Math.Round(osnovica, 3);
                dSstope.Tables["DTstope"].Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["iznos"] = Convert.ToDouble(dataROW[0]["iznos"].ToString()) + Math.Round(iznos, 3);
                dataROW[0]["osnovica"] = Convert.ToDouble(dataROW[0]["osnovica"].ToString()) + Math.Round(osnovica, 3);
            }
        }

        private double VratiTecaj(string imeTablice, string imeBrojPonude, string broj)
        {
            double tecaj;
            string sqlTecaj = "SELECT tecaj" +
                " FROM " + imeTablice + " WHERE " + imeTablice + "." + imeBrojPonude + "='" + broj + "'";

            DataTable dt = classSQL.select(sqlTecaj, imeTablice).Tables[0];
            if (dt.Rows.Count > 0)
            {
                try
                {
                    tecaj = Convert.ToDouble(dt.Rows[0][0].ToString());
                }
                catch
                {
                    tecaj = 1;
                }
            }
            else
            {
                tecaj = 1;
            }

            return tecaj;
        }
    }
}