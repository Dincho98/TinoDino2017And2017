using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PCPOS.Report.FakturaVan
{
    public partial class repFaktura : Form
    {
        public repFaktura()
        {
            InitializeComponent();
        }

        public bool racunajTecaj { get; set; }
        public bool samoIspis { get; set; }
        public string broj_dokumenta { get; set; }
        public string dokumenat { get; set; }
        public string from_skladiste { get; set; }
        public string ImeForme { get; set; }
        public bool otpr_id { get; set; }
        public Dataset.DSFaktura ispisdSFaktura;
        public Dataset.DSRfakturaStavke ispisdSRfakturaStavke;
        public Dataset.DSRpodaciTvrtke ispisdSRpodaciTvrtke;

        //double osnovicaUkupno = 0;
        //double SveUkupno = 0;
        //double pdvUkupno = 0;
        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private void repFaktura_Load(object sender, EventArgs e)
        {
            racunajTecaj = true;
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

            this.Text = ImeForme;
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            //if (samoIspis)
            //{
            //this.dSFaktura = ispisdSFaktura;
            //this.dSRfakturaStavke = ispisdSRfakturaStavke;
            //this.dSRpodaciTvrtke = ispisdSRpodaciTvrtke;
            //}

            INIFile ini = new INIFile();
            string vpcplusporez = "MPC";
            try
            {
                if (ini.Read("VPCPLUSPOREZ", "vpcplusporez") == "1")
                {
                    vpcplusporez = "VPC+porez";
                }
            }
            catch
            {
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
            string root = "";
            if (DTpostavke.Rows[0]["logo"].ToString() == "1")
            {
                root = DTpostavke.Rows[0]["logopath"].ToString();
            }
            else
            {
                string localPath = Path.GetDirectoryName(Application.ExecutablePath);
                root = localPath + "\\bijela.jpg";
            }
            string drzava = "Hrvatska";
            string kn = racunajTecaj ? " " : " kn";
            string valut = " Select fakture_van.id_valuta, valute.ime_valute AS valuta FROM fakture_van " +
             " LEFT JOIN valute ON valute.id_valuta=fakture_van.id_valuta WHERE fakture_van.broj_fakture='" + broj_dokumenta + "'";
            DataTable DTvalut = classSQL.select(valut, "valute").Tables[0];
            if (racunajTecaj)
            {
                if (DTvalut.Rows[0]["valuta"].ToString() == "978 EUR")
                    kn = "€";
            }

            string val1 = " Select fakture_van.id_valuta, valute.ime_valute AS valuta FROM fakture_van " +
             " LEFT JOIN valute ON valute.id_valuta=fakture_van.id_valuta WHERE fakture_van.broj_fakture='" + broj_dokumenta + "'";
            DataTable DTval1 = classSQL.select(val1, "valute").Tables[0];

            if (racunajTecaj)
            {
                if (DTval1.Rows[0]["valuta"].ToString() == "978 EUR")
                {
                    kn = " €";
                }
            }
            ReportParameter p1 = new ReportParameter("MPCHeader", vpcplusporez);
            ReportParameter p2 = new ReportParameter("path", root);
            ReportParameter p3 = new ReportParameter("kn", kn);
            ReportParameter p4 = new ReportParameter("drzava", drzava);
            ReportParameter p5 = new ReportParameter("sas", sas);
            ReportParameter p6 = new ReportParameter("oib", Class.PodaciTvrtka.oibTvrtke);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6 });

            //ReportParameter p = new ReportParameter("MPCHeader", vpcplusporez);
            //this.reportViewer1.LocalReport.SetParameters(p);

            string[] imeTablica = new string[2];
            string imeBrojRacuna, imeGodinaRacuna;

            if (dokumenat == "FAK")
            {
                if (broj_dokumenta == null) { return; }
                imeTablica[0] = samoIspis ? "ispis_fakture" : "fakture_van";
                imeTablica[1] = samoIspis ? "ispis_faktura_stavke" : "faktura_van_stavke";
                FillFaktura(broj_dokumenta, imeTablica);
            }
            else if (dokumenat == "RAC")
            {
                if (broj_dokumenta == null) { return; }

                imeTablica[0] = samoIspis ? "ispis_racuni" : "racuni";
                imeTablica[1] = samoIspis ? "ispis_racun_stavke" : "racun_stavke";

                FillRacun(broj_dokumenta, imeTablica);
            }
            else if (dokumenat == "PON")
            {
                if (broj_dokumenta == null) { return; }

                imeTablica[0] = samoIspis ? "ispis_fakture" : "ponude";
                imeTablica[1] = samoIspis ? "ispis_faktura_stavke" : "ponude_stavke";
                imeBrojRacuna = samoIspis ? "broj_fakture" : "broj_ponude";
                imeGodinaRacuna = samoIspis ? "godina_fakture" : "godina_ponude";

                FillPonude(broj_dokumenta, imeTablica, imeBrojRacuna, imeGodinaRacuna);
            }
            else if (dokumenat == "OTP")
            {
                if (broj_dokumenta == null) { return; }
                FillOtpremnicu(broj_dokumenta, from_skladiste);
            }
            else if (dokumenat == "IFB")
            {
                if (broj_dokumenta == null) { return; }
                FillIFB(broj_dokumenta);
            }
            else if (dokumenat == "RNS")
            {
                if (broj_dokumenta == null) { return; }
                FillRNS(broj_dokumenta);
            }

            this.reportViewer1.RefreshReport();
        }

        private void FillRacun(string broj, string[] imeTablica)
        {
            PCPOS.classNumberToLetter broj_u_text = new PCPOS.classNumberToLetter();

            //MessageBox.Show(broj_slovima.ToLower());

            string sql2 = "SELECT " +
                " " + imeTablica[1] + ".kolicina," +
                " " + imeTablica[1] + ".vpc," +
                " " + imeTablica[1] + ".porez," +
                " " + imeTablica[1] + ".broj_racuna," +
                " " + imeTablica[1] + ".rabat," +
                " " + imeTablica[1] + ".sifra_robe AS sifra," +
                " roba.naziv as naziv," +
                " roba.jm as jm," +
                " " + imeTablica[1] + ".id_skladiste AS skladiste" +
                " FROM " + imeTablica[1] + "" +
                " LEFT JOIN roba ON roba.sifra=" + imeTablica[1] + ".sifra_robe " +
                " WHERE " + imeTablica[1] + ".broj_racuna='" + broj + "' order by " + imeTablica[1] + ".id_stavka";

            //DSRfakturaStavke DSfs = new DSRfakturaStavke();

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }
            else
            {
                classSQL.NpgAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }

            double vpc = 0;
            double kol = 0;
            double porez;
            double rabat;
            double mpc;
            double mpcStavka;
            double rabatStavka = 0;
            double pdvStavka = 0;
            double osnovicaStavka = 0;

            double osnovicaUkupno = 0;
            double sveUkupno = 0;
            double pdvUkupno = 0;
            double rabatSve = 0;

            DataTable DT = dSRfakturaStavke.Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                vpc = Math.Round(Convert.ToDouble(DT.Rows[i]["vpc"].ToString()), 3);
                kol = Math.Round(Convert.ToDouble(DT.Rows[i]["kolicina"].ToString()), 3);
                porez = Math.Round(Convert.ToDouble(DT.Rows[i]["porez"].ToString()), 3);
                rabat = Math.Round(Convert.ToDouble(DT.Rows[i]["rabat"].ToString()), 3);

                mpc = Math.Round(vpc * (1 + porez / 100.0) - 0.0000001, 2);

                rabatStavka = Math.Round(mpc * kol * rabat / 100, 2);
                mpcStavka = Math.Round(mpc * kol - rabatStavka, 2);
                pdvStavka = Math.Round(mpcStavka * (1 - 100 / (100 + porez)), 2);
                osnovicaStavka = Math.Round(mpcStavka - pdvStavka, 2);

                DT.Rows[i].SetField("mpc", Math.Round(mpc, 3));
                DT.Rows[i].SetField("vpc", Math.Round(vpc, 3));
                DT.Rows[i].SetField("porez", Math.Round(porez, 3));
                DT.Rows[i].SetField("rabat", Math.Round(rabat, 3));
                DT.Rows[i].SetField("kolicina", Math.Round(kol, 3));
                DT.Rows[i].SetField("mpcStavka", Math.Round(mpcStavka, 3));
                DT.Rows[i].SetField("rabatStavka", Math.Round(rabatStavka, 3));

                rabatSve += rabatStavka;
                osnovicaUkupno += osnovicaStavka;
                pdvUkupno += pdvStavka;
                sveUkupno += mpcStavka;
            }

            string broj_slovima = "Slovima: " + broj_u_text.PretvoriBrojUTekst(sveUkupno.ToString(), ',', "kn", "lp").ToString().ToLower();

            string year;
            if (imeTablica[0] == "ispis_racuni")
                year = DateTime.Now.Year.ToString();
            else
            {
                year = classSQL.select("SELECT datum_racuna FROM racuni WHERE broj_racuna='" + broj_dokumenta + "'", "racuni").Tables[0].Rows[0][0].ToString();
                DateTime date = Convert.ToDateTime(year);
                year = date.Year.ToString();
            }

            string sql = "SELECT " +
                " " + imeTablica[0] + ".broj_racuna," +
                " " + imeTablica[0] + ".datum_racuna AS datum," +
                " " + imeTablica[0] + ".jir," +
                " " + imeTablica[0] + ".zki," +
                " " + imeTablica[0] + ".napomena," +
                " '" + sveUkupno + "' AS ukupno," +
                " '" + pdvUkupno + "' AS iznos_pdv," +
                " '" + osnovicaUkupno + "' AS osnovica," +
                " '" + rabatSve + "' AS rabat," +
                " CAST (" + imeTablica[0] + ".broj_racuna AS nvarchar) + CAST ('" + Util.Korisno.VratiDucanIBlagajnuZaIspis(1) + "' AS nvarchar)  AS Naslov," +
                " partners.ime_tvrtke AS kupac_tvrtka," +
                " partners.adresa AS kupac_adresa," +
                " ' Novčanice: ' + CAST(" + imeTablica[0] + ".ukupno_gotovina AS money) + '   Kartice: ' + CAST(" + imeTablica[0] + ".ukupno_kartice AS money)  AS placanje," +
                " partners.id_grad AS id_kupac_grad," +
                " partners.id_partner AS sifra_kupac," +
                " partners.napomena AS napomena_tvrtka," +
                " partners.oib_polje AS string5, " +
                " ziro_racun.ziroracun AS zr," +
                " '1960-1-1' AS datum_dvo," +
                " '1960-1-1' AS datum_valute," +
                " ziro_racun.ziroracun AS zr," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio," +
                " ziro_racun.banka AS banka," +
                " '" + broj_slovima + "' AS broj_slovima," +
                " partners.oib AS kupac_oib" +
                " FROM " + imeTablica[0] + "" +
                " LEFT JOIN ziro_racun ON ziro_racun.id_ziroracun='1'" +
                " LEFT JOIN zacrnjeni_partner as  partners ON partners.id_partner=" + imeTablica[0] + ".id_kupac" +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=" + imeTablica[0] + ".id_blagajnik WHERE " + imeTablica[0] + ".broj_racuna='" + broj_dokumenta + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql).Fill(dSFaktura, "DTRfaktura");
            }
            else
            {
                classSQL.NpgAdatpter(sql.Replace("nvarchar", "varchar")).Fill(dSFaktura, "DTRfaktura");
            }

            //mora se postaviti na "" i 1 jer inače javlja error na reportu
            dSFaktura.Tables[0].Rows[0].SetField("valuta", "");
            dSFaktura.Tables[0].Rows[0].SetField("tecaj", 1);

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
                return;
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
                //classSQL.NpgAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }

            PostaviPodatkeOBanci(ref dSRpodaciTvrtke, ref dSFaktura);
        }

        private void FillFaktura(string broj, string[] imeTablica)
        {
            PCPOS.classNumberToLetter broj_u_text = new PCPOS.classNumberToLetter();

            //MessageBox.Show(broj_slovima.ToLower());

            double tecaj;

            if (racunajTecaj)
            {
                tecaj = VratiTecaj(imeTablica[0], "broj_fakture", broj);
            }
            else
            {
                tecaj = 1;
            }

            string sql2 = "SELECT " +
                " " + imeTablica[1] + ".kolicina," +
                " " + imeTablica[1] + ".vpc," +
                " " + imeTablica[1] + ".porez," +
                " " + imeTablica[1] + ".broj_fakture," +
                " " + imeTablica[1] + ".rabat," +
                " " + imeTablica[1] + ".sifra," +
                " roba.naziv as naziv," +
                " roba.jm as jm," +
                " " + imeTablica[1] + ".id_skladiste AS skladiste" +
                " FROM " + imeTablica[1] + "" +
                " LEFT JOIN roba ON roba.sifra=" + imeTablica[1] + ".sifra" +
                " WHERE " + imeTablica[1] + ".broj_fakture='" + broj + "'" +
                " order by " + imeTablica[1] + ".id_stavka";

            //DSRfakturaStavke DSfs = new DSRfakturaStavke();

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }
            else
            {
                classSQL.NpgAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }

            double vpc = 0;
            double kol = 0;
            double porez;
            double rabat;
            double mpcStavka;
            double rabatStavka = 0;
            double pdvStavka = 0;
            double osnovicaStavka = 0;
            double rabatSve = 0;
            double mpc = 0;
            double sveUkupno = 0;
            double pdvUkupno = 0;
            double osnovicaUkupno = 0;

            DataTable DT = dSRfakturaStavke.Tables[0];
            string velep = classSQL.select_settings("Select veleprodaja From postavke", "veleprodaja").Tables[0].Rows[0][0].ToString();
            if (velep == "1")
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    vpc = Math.Round(Convert.ToDouble(DT.Rows[i]["vpc"].ToString()) / tecaj, 3);
                    kol = Math.Round(Convert.ToDouble(DT.Rows[i]["kolicina"].ToString()), 3);
                    porez = Math.Round(Convert.ToDouble(DT.Rows[i]["porez"].ToString()), 3);
                    rabat = Math.Round(Convert.ToDouble(DT.Rows[i]["rabat"].ToString()), 3);

                    mpc = Math.Round(vpc * (1 + porez / 100.0) - 0.0000001, 3);

                    rabatStavka = Math.Round(vpc * kol * rabat / 100, 3);
                    mpcStavka = Math.Round(((vpc * kol - rabatStavka)), 3);
                    pdvStavka = Math.Round(mpcStavka * (porez / 100), 3);
                    osnovicaStavka = Math.Round(mpcStavka - pdvStavka, 3);

                    DT.Rows[i].SetField("mpc", mpc);
                    DT.Rows[i].SetField("vpc", vpc);
                    DT.Rows[i].SetField("porez", porez);
                    DT.Rows[i].SetField("rabat", rabat);
                    DT.Rows[i].SetField("kolicina", kol);
                    DT.Rows[i].SetField("mpcStavka", mpcStavka);
                    DT.Rows[i].SetField("rabatStavka", rabatStavka);

                    rabatSve += rabatStavka;
                    osnovicaUkupno += osnovicaStavka;
                    pdvUkupno += pdvStavka;
                    sveUkupno += (mpcStavka + pdvStavka);
                }
            }
            else
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    vpc = Math.Round(Convert.ToDouble(DT.Rows[i]["vpc"].ToString()) / tecaj, 3);
                    kol = Math.Round(Convert.ToDouble(DT.Rows[i]["kolicina"].ToString()), 3);
                    porez = Math.Round(Convert.ToDouble(DT.Rows[i]["porez"].ToString()), 3);
                    rabat = Math.Round(Convert.ToDouble(DT.Rows[i]["rabat"].ToString()), 3);

                    mpc = Math.Round(vpc * (1 + porez / 100.0) - 0.0000001, 3);

                    rabatStavka = Math.Round(vpc * kol * rabat / 100, 3);
                    mpcStavka = Math.Round(vpc * kol - rabatStavka, 3);
                    pdvStavka = Math.Round(mpcStavka * (porez / 100), 3);
                    osnovicaStavka = Math.Round(mpcStavka, 3);

                    DT.Rows[i].SetField("mpc", mpc);
                    DT.Rows[i].SetField("vpc", vpc);
                    DT.Rows[i].SetField("porez", porez);
                    DT.Rows[i].SetField("rabat", rabat);
                    DT.Rows[i].SetField("kolicina", kol);
                    DT.Rows[i].SetField("mpcStavka", mpcStavka);
                    DT.Rows[i].SetField("rabatStavka", rabatStavka);

                    rabatSve += rabatStavka;
                    osnovicaUkupno += osnovicaStavka;
                    pdvUkupno += pdvStavka;
                    sveUkupno += (mpcStavka + pdvStavka);
                }
            }

            string val = " Select fakture_van.id_valuta, valute.ime_valute AS valuta FROM fakture_van " +
             " LEFT JOIN valute ON valute.id_valuta=fakture_van.id_valuta WHERE fakture_van.broj_fakture='" + broj + "'";
            DataTable DTval = classSQL.select(val, "fakture_van").Tables[0];

            string broj_slovima = "Slovima: " + broj_u_text.PretvoriBrojUTekst(sveUkupno.ToString(), ',', "kn", "lp").ToString().ToLower();
            if (racunajTecaj) broj_slovima = "";
            if (racunajTecaj == true)
            {
                if (DTval.Rows[0]["valuta"].ToString() == "978 EUR")
                    broj_slovima = "Slovima: " + broj_u_text.PretvoriBrojUTekst(sveUkupno.ToString(), ',', "€", "c").ToString().ToLower();
            }

            string sql = "SELECT " +
                " " + imeTablica[0] + ".broj_fakture," +
                " " + imeTablica[0] + ".date AS datum," +
                " " + imeTablica[0] + ".dateDVO AS datum_dvo," +
                " " + imeTablica[0] + ".datum_valute," +
                " " + imeTablica[0] + ".jir," +
                " " + imeTablica[0] + ".zki," +
                " " + imeTablica[0] + ".tecaj AS tecaj," +
                " valute.ime_valute AS valuta," +
                " " + imeTablica[0] + ".mj_troska AS mjesto_troska," +
                " nacin_placanja.naziv_placanja AS placanje," +
                " otprema.naziv AS otprema," +
                " CAST (" + imeTablica[0] + ".model AS nvarchar) + '  '+ CAST (" + imeTablica[0] + ".broj_fakture AS nvarchar)+" +
                "CAST (" + imeTablica[0] + ".godina_fakture AS nvarchar)+'-'+CAST (" + imeTablica[0] + ".id_fakturirati AS nvarchar) AS model," +
                " " + imeTablica[0] + ".napomena," +
                " '" + rabatSve + "' AS rabat," +
                " '" + sveUkupno + "' AS ukupno," +
                " '" + pdvUkupno + "' AS iznos_pdv," +
                " '" + osnovicaUkupno + "' AS osnovica," +
                " " + imeTablica[0] + ".godina_fakture," +
                //" CAST (fakture_van.broj_fakture AS nvarchar) +'/'+ CAST (fakture_van.godina_fakture AS nvarchar) AS Naslov," +
                " CAST (" + imeTablica[0] + ".broj_fakture AS nvarchar) + CAST ('" + Util.Korisno.VratiDucanIBlagajnuZaIspis(2) + "' AS nvarchar)  AS Naslov," +
                " partners.ime_tvrtke AS kupac_tvrtka," +
                " 'Datum isporuke:' AS naziv_date1," +
                " 'Datum dospijeća:' AS naziv_date2," +
                " partners.adresa AS kupac_adresa," +
                " partners.id_grad AS id_kupac_grad," +
                " partners.id_partner AS sifra_kupac," +
                " partners.napomena AS napomena_tvrtka," +
                " ziro_racun.ziroracun AS zr," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio," +
                " ziro_racun.banka AS banka," +
                " '" + broj_slovima + "' AS broj_slovima," +
                " partners.oib AS kupac_oib, " +
                " partners.oib_polje AS string5 " +
                " FROM " + imeTablica[0] + "" +
                " LEFT JOIN zacrnjeni_partner as  partners ON partners.id_partner=" + imeTablica[0] + ".id_fakturirati" +
                " LEFT JOIN otprema ON otprema.id_otprema=" + imeTablica[0] + ".otprema" +
                " LEFT JOIN nacin_placanja ON nacin_placanja.id_placanje=" + imeTablica[0] + ".id_nacin_placanja" +
                " LEFT JOIN ziro_racun ON ziro_racun.id_ziroracun=" + imeTablica[0] + ".zr" +
                " LEFT JOIN valute ON valute.id_valuta=" + imeTablica[0] + ".id_valuta" +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=" + imeTablica[0] + ".id_zaposlenik_izradio WHERE " + imeTablica[0] + ".broj_fakture='" + broj + "'" +
                "";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql).Fill(dSFaktura, "DTRfaktura");
            }
            else
            {
                classSQL.NpgAdatpter(sql.Replace("nvarchar", "varchar")).Fill(dSFaktura, "DTRfaktura");
            }

            //postavi valutu i tečaj
            string valuta = "";
            if (dSFaktura.Tables[0].Rows.Count > 0)
            {
                valuta = dSFaktura.Tables[0].Rows[0]["valuta"].ToString();
                if (!ValutaKuna(valuta)) valuta = "";
            }

            dSFaktura.Tables[0].Rows[0].SetField("valuta", valuta);

            if (valuta == "")
                dSFaktura.Tables[0].Rows[0].SetField("tecaj", tecaj);
            else
            {
                if (!racunajTecaj)
                    dSFaktura.Tables[0].Rows[0].SetField("tecaj", VratiTecaj(imeTablica[0], "broj_fakture", broj));
                else
                    dSFaktura.Tables[0].Rows[0].SetField("tecaj", tecaj);
            }

            string id_kupac_grad = "";
            string id_kupac = "";
            if (dSFaktura.Tables[0].Rows.Count > 0)
            {
                id_kupac_grad = dSFaktura.Tables[0].Rows[0]["id_kupac_grad"].ToString();
                id_kupac = dSFaktura.Tables[0].Rows[0]["sifra_kupac"].ToString();
            }
            else
            {
                if (!samoIspis) MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                //classSQL.NpgAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }

            PostaviPodatkeOBanci(ref dSRpodaciTvrtke, ref dSFaktura);
        }

        private void FillPonude(string broj, string[] imeTablica, string imeBrojPonude, string imeGodinaPonude)
        {
            PCPOS.classNumberToLetter broj_u_text = new PCPOS.classNumberToLetter();

            //MessageBox.Show(broj_slovima.ToLower());

            double tecaj;

            if (racunajTecaj)
            {
                tecaj = VratiTecaj(imeTablica[0], imeBrojPonude, broj);
            }
            else
            {
                tecaj = 1;
            }

            string sql2 = "SELECT " +
                " " + imeTablica[1] + ".kolicina," +
                " " + imeTablica[1] + ".vpc," +
                " " + imeTablica[1] + ".porez," +
                " " + imeTablica[1] + "." + imeBrojPonude + "," +
                " " + imeTablica[1] + ".rabat," +
                " " + imeTablica[1] + ".sifra," +
                " " + imeTablica[1] + ".naziv as naziv," +
                " roba.jm as jm," +
                " " + imeTablica[1] + ".id_skladiste AS skladiste" +
                " FROM " + imeTablica[1] + "" +
                " LEFT JOIN roba ON roba.sifra=" + imeTablica[1] + ".sifra" +
                " WHERE " + imeTablica[1] + "." + imeBrojPonude + "='" + broj + "'" +
                " order by " + imeTablica[1] + ".id_stavka";

            //DSRfakturaStavke DSfs = new DSRfakturaStavke();

            //DataRow DTrow = dSRfakturaStavke.Tables[0].NewRow();

            // DTrow = dSRfakturaStavke.Tables[0].NewRow();
            //    DTrow["string5"] = broj_skladista.ToString();
            //    DTrow["string4"] = NAZIV_SKLADISTA.ToString();
            //    DTrow["string9"] = ime_dokumenta1.ToString();
            //    DTrow["string8"] = "0";
            //    DTrow["string7"] = mpc_uk.ToString("#0.00");
            //    DTrow["string6"] = (0 - mpc_uk).ToString("#0.00");
            //    dSRfakturaStavke.Tables[0].Rows.Add(DTrow);

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }
            else
            {
                classSQL.NpgAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }

            double vpc = 0;
            double kol = 0;
            double porez;
            double rabat;
            double mpc;
            double mpcStavka;
            double rabatStavka = 0;
            double pdvStavka = 0;
            double osnovicaStavka = 0;

            double osnovicaUkupno = 0;
            double sveUkupno = 0;
            double pdvUkupno = 0;
            double rabatSve = 0;

            DataTable DT = dSRfakturaStavke.Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                vpc = Math.Round(Convert.ToDouble(DT.Rows[i]["vpc"].ToString()), 3) / tecaj;
                kol = Math.Round(Convert.ToDouble(DT.Rows[i]["kolicina"].ToString()), 3);
                porez = Math.Round(Convert.ToDouble(DT.Rows[i]["porez"].ToString()), 3);
                rabat = Math.Round(Convert.ToDouble(DT.Rows[i]["rabat"].ToString()), 3);

                mpc = Math.Round(vpc * (1 + porez / 100.0) - 0.0000001, 3);

                rabatStavka = mpc * kol * rabat / 100;
                mpcStavka = Math.Round(mpc * kol - rabatStavka, 3);
                pdvStavka = Math.Round(mpcStavka * (1 - 100 / (100 + porez)), 3);
                osnovicaStavka = mpcStavka - pdvStavka;

                DT.Rows[i].SetField("mpc", Math.Round(mpc, 3));
                DT.Rows[i].SetField("vpc", Math.Round(vpc, 3));
                DT.Rows[i].SetField("porez", Math.Round(porez, 3));
                DT.Rows[i].SetField("rabat", Math.Round(rabat, 3));
                DT.Rows[i].SetField("kolicina", Math.Round(kol, 3));
                DT.Rows[i].SetField("mpcStavka", Math.Round(mpcStavka, 3));
                DT.Rows[i].SetField("rabatStavka", Math.Round(rabatStavka, 3));

                rabatSve += rabatStavka;
                osnovicaUkupno += osnovicaStavka;
                pdvUkupno += pdvStavka;
                sveUkupno += mpcStavka;
            }

            string broj_slovima = "Slovima: " + broj_u_text.PretvoriBrojUTekst(sveUkupno.ToString(), ',', "kn", "lp").ToString().ToLower();
            if (racunajTecaj) broj_slovima = "";

            string sql = "SELECT " +
                " " + imeTablica[0] + "." + imeBrojPonude + "," +
                " " + imeTablica[0] + ".date AS datum," +
                " " + imeTablica[0] + ".tecaj AS tecaj," +
                " valute.ime_valute AS valuta," +
                " " + imeTablica[0] + ".vrijedi_do AS datum_dvo," +
                " '01.01.1000' AS datum_valute, " +
                //" " + imeTablice[0] + ".mj_troska AS mjesto_troska," +
                " nacin_placanja.naziv_placanja AS placanje," +
                " otprema.naziv AS otprema," +
                " CAST (" + imeTablica[0] + ".model AS nvarchar) + '  '+ CAST (" + imeTablica[0] + "." + imeBrojPonude + " AS nvarchar)+" +
                "CAST (" + imeTablica[0] + "." + imeGodinaPonude + " AS nvarchar)+'-'+CAST (" + imeTablica[0] + ".id_fakturirati AS nvarchar) AS model," +
                " " + imeTablica[0] + ".napomena," +
                " '" + rabatSve + "' AS rabat," +
                " '" + sveUkupno + "' AS ukupno," +
                " '" + pdvUkupno + "' AS iznos_pdv," +
                " '" + osnovicaUkupno + "' AS osnovica," +
                " " + imeTablica[0] + "." + imeGodinaPonude + "," +
                " CAST (" + imeTablica[0] + "." + imeBrojPonude + " AS nvarchar) +'/'+ CAST (" + imeTablica[0] + "." + imeGodinaPonude + " AS nvarchar) AS Naslov," +
                " partners.ime_tvrtke AS kupac_tvrtka," +
                " 'Vrijedi do:' AS naziv_date1," +
                " partners.adresa AS kupac_adresa," +
                " partners.id_grad AS id_kupac_grad," +
                " partners.id_partner AS sifra_kupac," +
                " partners.napomena AS napomena_tvrtka," +
                " partners.oib_polje AS string5, " +
                " ziro_racun.ziroracun AS zr," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio," +
                " ziro_racun.banka AS banka," +
                " '" + broj_slovima + "' AS broj_slovima," +
                " partners.oib AS kupac_oib" +
                " FROM " + imeTablica[0] + "" +
                " LEFT JOIN zacrnjeni_partner as  partners ON partners.id_partner=" + imeTablica[0] + ".id_fakturirati" +
                " LEFT JOIN otprema ON otprema.id_otprema=" + imeTablica[0] + ".otprema" +
                " LEFT JOIN nacin_placanja ON nacin_placanja.id_placanje=" + imeTablica[0] + ".id_nacin_placanja" +
                " LEFT JOIN ziro_racun ON ziro_racun.id_ziroracun=" + imeTablica[0] + ".zr" +
                " LEFT JOIN valute ON valute.id_valuta=" + imeTablica[0] + ".id_valuta" +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=" + imeTablica[0] + ".id_zaposlenik_izradio WHERE " + imeTablica[0] + "." + imeBrojPonude + "='" + broj + "'" +
                "";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql).Fill(dSFaktura, "DTRfaktura");
            }
            else
            {
                classSQL.NpgAdatpter(sql.Replace("nvarchar", "varchar")).Fill(dSFaktura, "DTRfaktura");
            }

            string id_kupac = "";
            if (dSFaktura.Tables[0].Rows.Count > 0)
            {
                id_kupac = dSFaktura.Tables[0].Rows[0]["id_kupac_grad"].ToString();
            }
            else
            {
                MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //return;
            }

            //postavi valutu i tečaj
            string valuta = "";
            if (dSFaktura.Tables[0].Rows.Count > 0)
            {
                valuta = dSFaktura.Tables[0].Rows[0]["valuta"].ToString();
                if (!ValutaKuna(valuta)) valuta = "";
            }

            dSFaktura.Tables[0].Rows[0].SetField("valuta", valuta);

            if (valuta == "")
                dSFaktura.Tables[0].Rows[0].SetField("tecaj", tecaj);
            else
            {
                if (!racunajTecaj)
                    dSFaktura.Tables[0].Rows[0].SetField("tecaj", VratiTecaj(imeTablica[0], imeBrojPonude, broj));
                else
                    dSFaktura.Tables[0].Rows[0].SetField("tecaj", tecaj);
            }

            string naziv_fakture = " 'Ponuda ' AS naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql(id_kupac, naziv_fakture, "");

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }
            else
            {
                classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
                //classSQL.NpgAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }

            PostaviPodatkeOBanci(ref dSRpodaciTvrtke, ref dSFaktura);
        }

        private void FillOtpremnicu(string broj, string skladiste)
        {
            PCPOS.classNumberToLetter broj_u_text = new PCPOS.classNumberToLetter();

            //MessageBox.Show(broj_slovima.ToLower());
            string sql2 = "";
            if (otpr_id == true)
            {
                sql2 = "SELECT " +
                    " otpremnica_stavke.kolicina," +
                    " otpremnica_stavke.vpc," +
                    " otpremnica_stavke.porez," +
                    " otpremnica_stavke.broj_otpremnice," +
                    " otpremnica_stavke.rabat," +
                    " otpremnica_stavke.sifra_robe as sifra," +
                    " roba.naziv as naziv," +
                    " roba.jm as jm," +
                    " otpremnica_stavke.id_skladiste AS skladiste" +
                    " FROM otpremnica_stavke" +
                    " LEFT JOIN roba ON roba.sifra=otpremnica_stavke.sifra_robe WHERE otpremnica_stavke.id_otpremnice='" + broj + "'" +
                    " order by otpremnica_stavke.id_stavka";
            }
            else
            {
                sql2 = "SELECT " +
                    " otpremnica_stavke.kolicina," +
                    " otpremnica_stavke.vpc," +
                    " otpremnica_stavke.porez," +
                    " otpremnica_stavke.broj_otpremnice," +
                    " otpremnica_stavke.rabat," +
                    " otpremnica_stavke.sifra_robe as sifra," +
                    " roba.naziv as naziv," +
                    " roba.jm as jm," +
                    " otpremnica_stavke.id_skladiste AS skladiste" +
                    " FROM otpremnica_stavke" +
                    " LEFT JOIN roba ON roba.sifra=otpremnica_stavke.sifra_robe WHERE otpremnica_stavke.broj_otpremnice='" + broj + "'" +
                    " AND otpremnica_stavke.id_skladiste='" + skladiste + "'" +
                    " order by otpremnica_stavke.id_stavka";
            }

            //DSRfakturaStavke DSfs = new DSRfakturaStavke();

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }
            else
            {
                classSQL.NpgAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }

            double vpc = 0;
            double kol = 0;
            double porez;
            double rabat;
            double mpc;
            double mpcStavka;
            double rabatStavka = 0;
            double pdvStavka = 0;
            double osnovicaStavka = 0;
            double osnovicaUkupno = 0;
            double sveUkupno = 0;
            double pdvUkupno = 0;
            double rabatSve = 0;

            DataTable DT = dSRfakturaStavke.Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                vpc = Math.Round(Convert.ToDouble(DT.Rows[i]["vpc"].ToString()), 3);
                kol = Math.Round(Convert.ToDouble(DT.Rows[i]["kolicina"].ToString()), 3);
                porez = Math.Round(Convert.ToDouble(DT.Rows[i]["porez"].ToString()), 3);
                rabat = Math.Round(Convert.ToDouble(DT.Rows[i]["rabat"].ToString()), 3);

                mpc = Math.Round(vpc * (1 + porez / 100.0) - 0.0000001, 3);

                rabatStavka = mpc * kol * rabat / 100;
                mpcStavka = Math.Round(mpc * kol - rabatStavka, 3);
                pdvStavka = Math.Round(mpcStavka * (1 - 100 / (100 + porez)), 3);
                osnovicaStavka = mpcStavka - pdvStavka;

                DT.Rows[i].SetField("mpc", Math.Round(mpc, 3));
                DT.Rows[i].SetField("vpc", Math.Round(vpc, 3));
                DT.Rows[i].SetField("porez", Math.Round(porez, 3));
                DT.Rows[i].SetField("rabat", Math.Round(rabat, 3));
                DT.Rows[i].SetField("kolicina", Math.Round(kol, 3));
                DT.Rows[i].SetField("mpcStavka", Math.Round(mpcStavka, 3));
                DT.Rows[i].SetField("rabatStavka", Math.Round(rabatStavka, 3));

                rabatSve += rabatStavka;
                osnovicaUkupno += osnovicaStavka;
                pdvUkupno += pdvStavka;
                sveUkupno += mpcStavka;
            }

            string broj_slovima = "Slovima: " + broj_u_text.PretvoriBrojUTekst(sveUkupno.ToString(), ',', "kn", "lp").ToString().ToLower();
            string filter = "";
            if (otpr_id == true)
            {
                filter = "otpremnice.id_otpremnica='" + broj + "' ";
            }
            else
            {
                filter = "otpremnice.broj_otpremnice='" + broj + "' AND otpremnice.id_skladiste='" + skladiste + "'";
            }
            string sql = "SELECT " +
                " otpremnice.broj_otpremnice," +
                " otpremnice.datum AS datum," +
                //" otpremnice.vrijedi_do AS datum_dvo," +
                " 'Mjesto otpreme:  '+otpremnice.mj_otpreme  + '\nAdresa otpreme:  ' + otpremnice.adr_otpreme + '\nIstovarno mjesto:  ' +  otpremnice.istovarno_mj + '\nRegistracija:  ' +  otpremnice.registracija    AS string1," +
                " 'Isprave:  '+otpremnice.isprave  + '\nTroškovi prijevoza:  ' + otpremnice.troskovi_prijevoza + '\nIstovarni rok:  ' +  otpremnice.istovarni_rok    AS string2," +
                //" nacin_placanja.naziv_placanja AS placanje," +
                " otprema.naziv AS otprema," +
                //" CAST (ponude.model AS nvarchar) + '  '+ CAST (ponude.broj_ponude AS nvarchar)+CAST (ponude.godina_ponude AS nvarchar)+'-'+CAST (ponude.id_fakturirati AS nvarchar) AS model," +
                " otpremnice.napomena," +
                " '" + rabatSve + "' AS rabat," +
                " '" + sveUkupno + "' AS ukupno," +
                " '" + pdvUkupno + "' AS iznos_pdv," +
                " '" + osnovicaUkupno + "' AS osnovica," +
                " otpremnice.godina_otpremnice," +
                " CAST (otpremnice.broj_otpremnice AS nvarchar) +'/'+ CAST (otpremnice.godina_otpremnice AS nvarchar) AS Naslov," +
                " partners.ime_tvrtke AS kupac_tvrtka," +
                //" 'Vrijedi do:' AS naziv_date1," +
                " partners.adresa AS kupac_adresa," +
                " partners.id_grad AS id_kupac_grad," +
                " partners.id_partner AS sifra_kupac," +
                " partners.napomena AS napomena_tvrtka," +
                " partners.oib_polje AS string5, " +
                " ziro_racun.ziroracun AS zr," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio," +
                " ziro_racun.banka AS banka," +
                " '" + broj_slovima + "' AS broj_slovima," +
                " partners.oib AS kupac_oib" +
                " FROM otpremnice" +
                " LEFT JOIN zacrnjeni_partner as  partners ON partners.id_partner=otpremnice.osoba_partner" +
                " LEFT JOIN otprema ON otprema.id_otprema=otpremnice.id_otprema" +
                //" LEFT JOIN nacin_placanja ON nacin_placanja.id_placanje=otpremnice.id_nacin_placanja" +
                " LEFT JOIN ziro_racun ON ziro_racun.id_ziroracun='1'" +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=otpremnice.id_izradio WHERE " + filter + " ";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql).Fill(dSFaktura, "DTRfaktura");
            }
            else
            {
                classSQL.NpgAdatpter(sql.Replace("nvarchar", "varchar")).Fill(dSFaktura, "DTRfaktura");
            }

            //mora se postaviti na "" i 1 jer inače javlja error na reportu
            dSFaktura.Tables[0].Rows[0].SetField("valuta", "");
            dSFaktura.Tables[0].Rows[0].SetField("tecaj", 1);

            string id_kupac = "";
            if (dSFaktura.Tables[0].Rows.Count > 0)
            {
                id_kupac = dSFaktura.Tables[0].Rows[0]["id_kupac_grad"].ToString();
            }
            else
            {
                MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //return;
            }

            string naziv_fakture = " 'OTPREMNICA ' AS naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql(id_kupac, naziv_fakture, "");

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }
            else
            {
                classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
                //classSQL.NpgAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }

            PostaviPodatkeOBanci(ref dSRpodaciTvrtke, ref dSFaktura);
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
                " ifb_stavke.naziv as naziv" +
                " FROM ifb_stavke" +
                " WHERE ifb_stavke.broj='" + broj + "'" +
                " order by ifb_stavke.id_stavka";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }
            else
            {
                classSQL.NpgAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }

            double vpc = 0;
            double kol = 0;
            double porez;
            double rabat;
            double mpc;
            double mpcStavka;
            double rabatStavka = 0;
            double pdvStavka = 0;
            double osnovicaStavka = 0;
            double rabatSve = 0;
            double sveUkupno = 0;
            double pdvUkupno = 0;
            double osnovicaUkupno = 0;

            DataTable DT = dSRfakturaStavke.Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                vpc = Math.Round(Convert.ToDouble(DT.Rows[i]["vpc"].ToString()), 3);
                kol = Math.Round(Convert.ToDouble(DT.Rows[i]["kolicina"].ToString()), 3);
                porez = Math.Round(Convert.ToDouble(DT.Rows[i]["porez"].ToString()), 3);
                rabat = Math.Round(Convert.ToDouble(DT.Rows[i]["rabat"].ToString()), 3);

                mpc = Math.Round(vpc * (1 + porez / 100.0) - 0.0000001, 3);

                rabatStavka = mpc * kol * rabat / 100;
                mpcStavka = Math.Round(mpc * kol - rabatStavka, 3);
                pdvStavka = Math.Round(mpcStavka * (1 - 100 / (100 + porez)), 3);
                osnovicaStavka = mpcStavka - pdvStavka;

                DT.Rows[i].SetField("mpc", Math.Round(mpc, 3));
                DT.Rows[i].SetField("vpc", Math.Round(vpc, 3));
                DT.Rows[i].SetField("porez", Math.Round(porez, 3));
                DT.Rows[i].SetField("rabat", Math.Round(rabat, 3));
                DT.Rows[i].SetField("kolicina", Math.Round(kol, 3));
                DT.Rows[i].SetField("mpcStavka", Math.Round(mpcStavka, 3));
                DT.Rows[i].SetField("rabatStavka", Math.Round(rabatStavka, 3));

                rabatSve += rabatStavka;
                osnovicaUkupno += osnovicaStavka;
                pdvUkupno += pdvStavka;
                sveUkupno += mpcStavka;
            }

            string broj_slovima = "Slovima: " + broj_u_text.PretvoriBrojUTekst(sveUkupno.ToString(), ',', "kn", "lp").ToString().ToLower();

            string sql = "SELECT " +
                " ifb.broj," +
                " ifb.datum_valute AS datum," +
                " ifb.datum_valute," +
                " ifb.mj_troska AS mjesto_troska," +
                " nacin_placanja.naziv_placanja AS placanje," +
                " ifb.otprema AS otprema," +
                " CAST (ifb.model AS nvarchar) + '  '+ CAST (ifb.broj AS nvarchar)+CAST (ifb.godina AS nvarchar)+'-'+CAST (ifb.odrediste AS nvarchar) AS model," +
                " ifb.napomena," +
                " '" + rabatSve + "' AS rabat," +
                " '" + sveUkupno + "' AS ukupno," +
                " '" + pdvUkupno + "' AS iznos_pdv," +
                " '" + osnovicaUkupno + "' AS osnovica," +
                " ifb.godina," +
                " CAST (ifb.broj AS nvarchar) +'/'+ CAST (ifb.godina AS nvarchar) AS Naslov," +
                " partners.ime_tvrtke AS kupac_tvrtka," +
                " 'Datum isporuke:' AS naziv_date1," +
                " 'Datum dospijeća:' AS naziv_date2," +
                " partners.adresa AS kupac_adresa," +
                " partners.id_grad AS id_kupac_grad," +
                " partners.id_partner AS sifra_kupac," +
                " partners.napomena AS napomena_tvrtka," +
                " partners.oib_polje AS string5, " +
                //" ziro_racun.ziroracun AS zr," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio," +
                //" ziro_racun.banka AS banka," +
                " '" + broj_slovima + "' AS broj_slovima," +
                " partners.oib AS kupac_oib" +
                " FROM ifb" +
                " LEFT JOIN zacrnjeni_partner as  partners ON partners.id_partner=ifb.odrediste" +
                " LEFT JOIN nacin_placanja ON nacin_placanja.id_placanje=ifb.id_nacin_placanja" +
                //" LEFT JOIN ziro_racun ON ziro_racun.id_ziroracun=fakture_van.zr" +
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

            //mora se postaviti na "" i 1 jer inače javlja error na reportu
            dSFaktura.Tables[0].Rows[0].SetField("valuta", "");
            dSFaktura.Tables[0].Rows[0].SetField("tecaj", 1);

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
            }

            PostaviPodatkeOBanci(ref dSRpodaciTvrtke, ref dSFaktura);
        }

        private void FillRNS(string broj)
        {
            PCPOS.classNumberToLetter broj_u_text = new PCPOS.classNumberToLetter();

            //MessageBox.Show(broj_slovima.ToLower());

            string sql2 = "SELECT " +
                " radni_nalog_servis_stavke.kolicina," +
                " radni_nalog_servis_stavke.vpc," +
                " radni_nalog_servis_stavke.porez," +
                " radni_nalog_servis_stavke.broj," +
                " radni_nalog_servis_stavke.rabat," +
                " radni_nalog_servis_stavke.sifra," +
                " 'OVO NIJE FISKALIZIRAN RAČUN' AS string3," +
                " radni_nalog_servis_stavke.naziv as naziv," +
                " roba.jm as jm," +
                " radni_nalog_servis_stavke.id_skladiste AS skladiste" +
                " FROM radni_nalog_servis_stavke" +
                " LEFT JOIN roba ON roba.sifra=radni_nalog_servis_stavke.sifra WHERE radni_nalog_servis_stavke.broj='" + broj + "'";

            //DSRfakturaStavke DSfs = new DSRfakturaStavke();

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }
            else
            {
                classSQL.NpgAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }

            double vpc = 0;
            double kol = 0;
            double porez;
            double rabat;
            double mpc;
            double mpcStavka;
            double rabatStavka = 0;
            double pdvStavka = 0;
            double osnovicaStavka = 0;

            double osnovicaUkupno = 0;
            double sveUkupno = 0;
            double pdvUkupno = 0;
            double rabatSve = 0;

            DataTable DT = dSRfakturaStavke.Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                vpc = Math.Round(Convert.ToDouble(DT.Rows[i]["vpc"].ToString()), 3);
                kol = Math.Round(Convert.ToDouble(DT.Rows[i]["kolicina"].ToString()), 3);
                porez = Math.Round(Convert.ToDouble(DT.Rows[i]["porez"].ToString()), 3);
                rabat = Math.Round(Convert.ToDouble(DT.Rows[i]["rabat"].ToString()), 3);

                mpc = Math.Round(vpc * (1 + porez / 100.0) - 0.0000001, 3);

                rabatStavka = mpc * kol * rabat / 100;
                mpcStavka = Math.Round(mpc * kol - rabatStavka, 3);
                pdvStavka = Math.Round(mpcStavka * (1 - 100 / (100 + porez)), 3);
                osnovicaStavka = mpcStavka - pdvStavka;

                DT.Rows[i].SetField("mpc", Math.Round(mpc, 3));
                DT.Rows[i].SetField("vpc", Math.Round(vpc, 3));
                DT.Rows[i].SetField("porez", Math.Round(porez, 3));
                DT.Rows[i].SetField("rabat", Math.Round(rabat, 3));
                DT.Rows[i].SetField("kolicina", Math.Round(kol, 3));
                DT.Rows[i].SetField("mpcStavka", Math.Round(mpcStavka, 3));
                DT.Rows[i].SetField("rabatStavka", Math.Round(rabatStavka, 3));

                rabatSve += rabatStavka;
                osnovicaUkupno += osnovicaStavka;
                pdvUkupno += pdvStavka;
                sveUkupno += mpcStavka;
            }

            string broj_slovima = "Slovima: " + broj_u_text.PretvoriBrojUTekst(sveUkupno.ToString(), ',', "kn", "lp").ToString().ToLower();

            string sql = "SELECT " +
                " radni_nalog_servis.broj," +
                " radni_nalog_servis.date AS datum," +
                " radni_nalog_servis.vrijedi_do AS datum_dvo," +
                " radni_nalog_servis.vrijedi_do AS datum_valute," +
                " '' AS placanje," +
                " otprema.naziv AS otprema," +
                " CAST (radni_nalog_servis.model AS nvarchar) + '  '+ CAST (radni_nalog_servis.broj AS nvarchar)+" +
                " CAST (radni_nalog_servis.godina AS nvarchar)+'-'+CAST (radni_nalog_servis.id_fakturirati AS nvarchar) AS model," +
                " radni_nalog_servis.napomena," +
                " '" + rabatSve + "' AS rabat," +
                " '" + sveUkupno + "' AS ukupno," +
                " '" + pdvUkupno + "' AS iznos_pdv," +
                " '" + osnovicaUkupno + "' AS osnovica," +
                " radni_nalog_servis.godina," +
                " CAST (radni_nalog_servis.broj AS nvarchar) +'/'+ CAST (radni_nalog_servis.godina AS nvarchar) AS Naslov," +
                " partners.ime_tvrtke AS kupac_tvrtka," +
                " 'Vrijedi do:' AS naziv_date1," +
                " partners.adresa AS kupac_adresa," +
                " partners.id_grad AS id_kupac_grad," +
                " partners.id_partner AS sifra_kupac," +
                " partners.napomena AS napomena_tvrtka," +
                " partners.oib_polje AS string5, " +
                " ziro_racun.ziroracun AS zr," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio," +
                " ziro_racun.banka AS banka," +
                " '" + broj_slovima + "' AS broj_slovima," +
                " partners.oib AS kupac_oib" +
                " FROM radni_nalog_servis" +
                " LEFT JOIN zacrnjeni_partner as  partners ON partners.id_partner=radni_nalog_servis.id_fakturirati" +
                " LEFT JOIN otprema ON otprema.id_otprema=radni_nalog_servis.otprema" +
                " LEFT JOIN nacin_placanja ON nacin_placanja.id_placanje=radni_nalog_servis.id_nacin_placanja" +
                " LEFT JOIN ziro_racun ON ziro_racun.id_ziroracun=radni_nalog_servis.zr" +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=radni_nalog_servis.id_zaposlenik_izradio WHERE radni_nalog_servis.broj='" + broj + "'" +
                "";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql).Fill(dSFaktura, "DTRfaktura");
            }
            else
            {
                classSQL.NpgAdatpter(sql.Replace("nvarchar", "varchar")).Fill(dSFaktura, "DTRfaktura");
            }

            //mora se postaviti na "" i 1 jer inače javlja error na reportu
            dSFaktura.Tables[0].Rows[0].SetField("valuta", "");
            dSFaktura.Tables[0].Rows[0].SetField("tecaj", 1);

            string id_kupac = "";
            if (dSFaktura.Tables[0].Rows.Count > 0)
            {
                id_kupac = dSFaktura.Tables[0].Rows[0]["id_kupac_grad"].ToString();
            }
            else
            {
                MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //return;
            }
            //string drzava = dSRpodaciTvrtke.Tables[0].Rows[0]["id_kupac_grad"].ToString();
            string naziv_fakture = " 'Radni nalog servis ' AS naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql(id_kupac, naziv_fakture, "");

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }
            else
            {
                classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
                //classSQL.NpgAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }

            PostaviPodatkeOBanci(ref dSRpodaciTvrtke, ref dSFaktura);
        }

        #region Util

        private void PostaviPodatkeOBanci(ref Dataset.DSRpodaciTvrtke dSRpodaciTvrtke, ref Dataset.DSFaktura dSFaktura)
        {
            string ziro = dSRpodaciTvrtke.Tables[0].Rows[0]["zr"].ToString();
            //dSFaktura.Tables[0].Rows[0].SetField("zr", ziro);
            string iban = dSRpodaciTvrtke.Tables[0].Rows[0]["iban"].ToString();
            dSFaktura.Tables[0].Rows[0].SetField("iban", iban);
            string swift = dSRpodaciTvrtke.Tables[0].Rows[0]["swift"].ToString();
            dSFaktura.Tables[0].Rows[0].SetField("swift", swift);

            string banka = "";
            DataTable dt = classSQL.select("SELECT banka FROM ziro_racun WHERE ziroracun = '" + ziro + "'", "ziro_racun").Tables[0];
            if (dt.Rows.Count > 0)
            {
                banka = dt.Rows[0]["banka"].ToString();
                dSFaktura.Tables[0].Rows[0].SetField("banka", banka);
            }
            else
            {
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

        private bool ValutaKuna(string valuta)
        {
            string val = valuta.ToLower();

            if (val.Contains("hr"))
                return false;
            else if (val.Contains("hrk"))
                return false;
            else if (val.Contains("hrvatska"))
            {
                return false;
            }
            else if (val.Contains("kun"))
            {
                return false;
            }
            else
                return true;
        }

        #endregion Util
    }
}