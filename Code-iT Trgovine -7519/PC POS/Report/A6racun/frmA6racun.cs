using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Report.A6racun
{
    public partial class frmA6racun : Form
    {
        public frmA6racun()
        {
            InitializeComponent();
        }

        public bool samoIspis { get; set; }

        public string broj_dokumenta { get; set; }
        public string poslovnica { get; set; }
        public string naplatni { get; set; }
        public string dokumenat { get; set; }
        public string from_skladiste { get; set; }
        public string ImeForme { get; set; }

        //double osnovicaUkupno = 0;
        //double SveUkupno = 0;
        //double pdvUkupno = 0;

        private void frmA6racun_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

            this.Text = ImeForme;
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            //dokumenat = "OTP";
            //broj_dokumenta = "1";
            //from_skladiste = "3";

            INIFile ini = new INIFile();
            string vpcplusporez = "MPC";
            try
            {
                if (ini.Read("VPCPLUSPOREZ", "vpcplusporez") == "1")
                {
                    vpcplusporez = "VPC + porez";
                }
            }
            catch
            {
            }

            ReportParameter p = new ReportParameter("MPCHeader", vpcplusporez);

            this.reportViewer1.LocalReport.SetParameters(p);
            //this.reportViewer1.LocalReport.Refresh();

            string[] imeTablice = new string[2];

            if (dokumenat == "RAC")
            {
                if (broj_dokumenta == null) { return; }
                imeTablice[0] = samoIspis ? "ispis_racuni" : "racuni";
                imeTablice[1] = samoIspis ? "ispis_racun_stavke" : "racun_stavke";
                FillRacun(broj_dokumenta, imeTablice);
            }

            this.reportViewer1.RefreshReport();
        }

        private void FillRacun(string broj, string[] imeTablice)
        {
            PCPOS.classNumberToLetter broj_u_text = new PCPOS.classNumberToLetter();

            //MessageBox.Show(broj_slovima.ToLower());

            string sql2 = "SELECT " +
                " " + imeTablice[1] + ".kolicina," +
                " " + imeTablice[1] + ".vpc," +
                " " + imeTablice[1] + ".porez," +
                " " + imeTablice[1] + ".broj_racuna," +
                " " + imeTablice[1] + ".rabat," +
                " " + imeTablice[1] + ".sifra_robe AS sifra," +
                " roba.naziv as naziv," +
                " roba.jm as jm," +
                " " + imeTablice[1] + ".id_skladiste AS skladiste" +
                " FROM " + imeTablice[1] + "" +
                " LEFT JOIN roba ON roba.sifra=" + imeTablice[1] + ".sifra_robe " +
                " WHERE " + imeTablice[1] + ".broj_racuna='" + broj + "'";
            if (imeTablice[1] == "racun_stavke") sql2 += " AND racun_stavke.id_ducan=" + poslovnica + " AND racun_stavke.id_kasa=" + naplatni;

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
                porez = Math.Round(Convert.ToDouble(DT.Rows[i]["porez"].ToString()), 2);
                rabat = Math.Round(Convert.ToDouble(DT.Rows[i]["rabat"].ToString()), 2);

                mpc = Math.Round(vpc * (1 + porez / 100.0), 2);

                rabatStavka = Math.Round(mpc * kol * rabat / 100, 2);
                mpcStavka = Math.Round(mpc * kol - rabatStavka, 2);
                pdvStavka = Math.Round(mpcStavka * (1 - 100 / (100 + porez)), 2);
                osnovicaStavka = Math.Round(mpcStavka - pdvStavka, 2);

                DT.Rows[i].SetField("mpc", Math.Round(mpc, 2));
                DT.Rows[i].SetField("vpc", Math.Round(vpc, 3));
                DT.Rows[i].SetField("porez", Math.Round(porez, 2));
                DT.Rows[i].SetField("rabat", Math.Round(rabat, 2));
                DT.Rows[i].SetField("kolicina", Math.Round(kol, 3));
                DT.Rows[i].SetField("mpcStavka", Math.Round(mpcStavka, 2));
                DT.Rows[i].SetField("rabatStavka", Math.Round(rabatStavka, 2));

                rabatSve += rabatStavka;
                osnovicaUkupno += osnovicaStavka;
                pdvUkupno += pdvStavka;
                sveUkupno += mpcStavka;
            }

            string broj_slovima = broj_u_text.PretvoriBrojUTekst(sveUkupno.ToString(), ',', "kn", "lp").ToString();

            string year;
            if (imeTablice[0] == "ispis_racuni")
                year = DateTime.Now.Year.ToString();
            else
            {
                year = classSQL.select("SELECT datum_racuna FROM racuni WHERE broj_racuna='" + broj_dokumenta + "'", "racuni").Tables[0].Rows[0][0].ToString();
                DateTime date = Convert.ToDateTime(year);
                year = date.Year.ToString();
            }

            string sql = "SELECT " +
                " " + imeTablice[0] + ".broj_racuna," +
                " " + imeTablice[0] + ".datum_racuna AS datum," +
                " " + imeTablice[0] + ".jir," +
                " " + imeTablice[0] + ".zki," +
                " " + imeTablice[0] + ".napomena," +
                " '" + sveUkupno + "' AS ukupno," +
                " '" + pdvUkupno + "' AS iznos_pdv," +
                " '" + osnovicaUkupno + "' AS osnovica," +
                " '" + rabatSve + "' AS rabat,";
            if (imeTablice[0] == "racuni") sql += "CAST (racuni.broj_racuna AS varchar) + '/' + ime_ducana + '/' + ime_blagajne AS Naslov,";
            else sql += " CAST (" + imeTablice[0] + ".broj_racuna AS nvarchar) + CAST ('" + Util.Korisno.VratiDucanIBlagajnuZaIspis(1) + "' AS nvarchar)  AS Naslov,";
            sql += " partners.ime_tvrtke AS kupac_tvrtka," +
                " partners.adresa AS kupac_adresa," +
                " ' Novčanice: ' + CAST(" + imeTablice[0] + ".ukupno_gotovina AS money) + '   Kartice: ' + CAST(" + imeTablice[0] + ".ukupno_kartice AS money)  AS placanje," +
                " partners.id_grad AS id_kupac_grad," +
                " partners.id_partner AS sifra_kupac," +
                " partners.napomena AS napomena_tvrtka," +
                " ziro_racun.ziroracun AS zr," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio," +
                " ziro_racun.banka AS banka," +
                " '" + broj_slovima.ToLower() + "' AS broj_slovima," +
                " partners.oib AS kupac_oib" +
                " FROM " + imeTablice[0] + "" +
                " LEFT JOIN ziro_racun ON ziro_racun.id_ziroracun='1'" +
                " LEFT JOIN zacrnjeni_partner as  partners ON partners.id_partner=" + imeTablice[0] + ".id_kupac";
            if (imeTablice[0] == "racuni") sql += " LEFT JOIN ducan ON racuni.id_ducan = ducan.id_ducan " + " LEFT JOIN blagajna ON racuni.id_kasa = blagajna.id_blagajna ";
            sql += " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=" + imeTablice[0] + ".id_blagajnik WHERE " + imeTablice[0] + ".broj_racuna='" + broj_dokumenta + "'";
            if (imeTablice[0] == "racuni") sql += " AND racuni.id_ducan=" + poslovnica + " AND racuni.id_kasa=" + naplatni;

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql).Fill(dSFaktura, "DTRfaktura");
            }
            else
            {
                classSQL.NpgAdatpter(sql.Replace("nvarchar", "varchar")).Fill(dSFaktura, "DTRfaktura");
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

            string ziro = dSRpodaciTvrtke.Tables[0].Rows[0]["zr"].ToString();
            dSFaktura.Tables[0].Rows[0].SetField("zr", ziro);
            string iban = dSRpodaciTvrtke.Tables[0].Rows[0]["iban"].ToString();
            dSFaktura.Tables[0].Rows[0].SetField("iban", iban);

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
    }
}