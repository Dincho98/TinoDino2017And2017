using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Report.Faktura3
{
    public partial class repFaktura3 : Form
    {
        public repFaktura3()
        {
            InitializeComponent();
        }

        public bool racunajTecaj { get; set; }
        public bool samoIspis { get; set; }
        public string broj_dokumenta { get; set; }
        public string dokumenat { get; set; }
        public string from_skladiste { get; set; }
        public string ImeForme { get; set; }
        public string poslovnica { get; set; }
        public string naplatni { get; set; }
        public bool otpr_id { get; set; }
        public bool ponudaUNbc { get; set; }

        public Dataset.DSFaktura ispisdSFaktura;
        public Dataset.DSRfakturaStavke ispisdSRfakturaStavke;
        public Dataset.DSRpodaciTvrtke ispisdSRpodaciTvrtke;
        private ReportParameter p10, p21, p23;

        private string avio_registracija = "", avio_tip_zrakoplova = "", avio_maks_tezina_polijetanja = "";

        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private void repFaktura_Load(object sender, EventArgs e)
        {
            p21 = new ReportParameter("ppmv", "0");
            p23 = new ReportParameter("oib", Class.PodaciTvrtka.oibTvrtke);
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

            this.Text = ImeForme;
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);

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

            string root = @"C:\logo\logo_faktura_1.jpg";
            //if (DTpostavke.Rows[0]["logo"].ToString() == "1")
            //{
            //    root = DTpostavke.Rows[0]["logopath"].ToString();
            //}
            //else
            if (!File.Exists(root))
            {
                string localPath = Path.GetDirectoryName(Application.ExecutablePath);
                root = localPath + "\\bijela.jpg";
            }

            string root2 = @"C:\logo\logo_faktura_2.jpg";
            if (!File.Exists(root2))
            {
                string localPath = Path.GetDirectoryName(Application.ExecutablePath);
                root2 = localPath + "\\bijela.jpg";
            }

            string tekst = "";
            if (DTpostavke.Rows[0]["sustav_pdv"].ToString() == "0")
            {
                tekst = "PDV nije uračunat u cijenu prema\r\nčl.90. st.1. i st.2. NN 106/18 zakona o PDV-u.\r\n";
            }
            else
            {
                tekst = " ";
            }
            string drzava = "Hrvatska";
            string kn = racunajTecaj ? " " : " kn"; ;

            p10 = new ReportParameter("sakrij_jedinicnu_cijenu", "0");

            string[] imeTablica = new string[2];
            string imeBrojRacuna, imeGodinaRacuna;

            if (dokumenat == "FAK")
            {
                if (broj_dokumenta == null) { return; }
                imeTablica[0] = samoIspis ? "ispis_fakture" : "fakture";
                imeTablica[1] = samoIspis ? "ispis_faktura_stavke" : "faktura_stavke";
                FillFaktura(broj_dokumenta, imeTablica);
            }
            else if (dokumenat == "RAC")
            {
                if (broj_dokumenta == null) { return; }

                imeTablica[0] = samoIspis ? "ispis_racuni" : "racuni";
                imeTablica[1] = samoIspis ? "ispis_racun_stavke" : "racun_stavke";
                FillRacun(broj_dokumenta, imeTablica, poslovnica, naplatni);
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

            string Uvaluti = "";
            if (dokumenat == "FAK")
            {
                string valut = " Select " + imeTablica[0] + ".id_valuta, valute.ime_valute AS valuta FROM " + imeTablica[0] + " " +
                " LEFT JOIN valute ON valute.id_valuta=" + imeTablica[0] + ".id_valuta WHERE " + imeTablica[0] + ".broj_fakture='" + broj_dokumenta + "' AND " + imeTablica[0] + ".id_ducan='" + poslovnica + "' AND " + imeTablica[0] + ".id_kasa='" + naplatni + "'";

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

                string sql = "SELECT valute.naziv, ime_valute," + imeTablica[0] + ".tecaj, '" + dSFaktura.Tables[0].Rows[0]["ukupno"] + "' as ukupno FROM " + imeTablica[0] + ",valute WHERE broj_fakture= '" + broj_dokumenta + "'" +
                " AND " + imeTablica[0] + ".id_valuta=valute.id_valuta AND " + imeTablica[0] + ".id_ducan='" + poslovnica + "' AND " + imeTablica[0] + ".id_kasa='" + naplatni + "'";
                Uvaluti = AkoJeStranaValuta(sql, kn);
            }
            else if (dokumenat == "PON")
            {
                string valut = " Select ponude.id_valuta, valute.ime_valute AS valuta FROM ponude " +
                " LEFT JOIN valute ON valute.id_valuta=ponude.id_valuta WHERE ponude.broj_ponude='" + broj_dokumenta + "'";
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
                string sql = "SELECT valute.naziv, ime_valute,ponude.tecaj, '" + dSFaktura.Tables[0].Rows[0]["ukupno"] + "' as ukupno FROM ponude,valute WHERE broj_ponude= '" + broj_dokumenta + "'" +
                " AND ponude.id_valuta=valute.id_valuta";
                Uvaluti = AkoJeStranaValuta(sql, kn);
            }

            ReportParameter p1 = new ReportParameter("MPCHeader", vpcplusporez);
            ReportParameter p2 = new ReportParameter("path", root);
            ReportParameter p22 = new ReportParameter("path2", root2);
            ReportParameter p3 = new ReportParameter("kn", kn);
            ReportParameter p4 = new ReportParameter("drzava", drzava);
            ReportParameter p5 = new ReportParameter("sas", sas);
            ReportParameter p6 = new ReportParameter("sustav_pdv", tekst);
            ReportParameter p8 = new ReportParameter("iznos_valuta_kn", "0");
            ReportParameter p9 = new ReportParameter("tecaj_opis", Uvaluti);
            //ReportParameter p10 = new ReportParameter("sakrij_jedinicnu_cijenu", "0");
            ReportParameter p11 = new ReportParameter("dokumenat", dokumenat);
            ReportParameter p18 = new ReportParameter("avio_registracija", avio_registracija);
            ReportParameter p19 = new ReportParameter("avio_tip", avio_tip_zrakoplova);
            ReportParameter p20 = new ReportParameter("avio_tezina", avio_maks_tezina_polijetanja);

            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6, p8, p9, p10, p11, p18, p19, p20, p21, p22, p23 });

            this.reportViewer1.RefreshReport();
        }

        private string AkoJeStranaValuta(string sql, string valuta)
        {
            DataTable DT = classSQL.select(sql, "fakture").Tables[0];

            if (DT.Rows.Count > 0)
            {
                decimal tecaj, ukupno;
                if (!decimal.TryParse(DT.Rows[0]["tecaj"].ToString(), out tecaj))
                {
                    tecaj = 1;
                }

                if (!decimal.TryParse(DT.Rows[0]["ukupno"].ToString(), out ukupno))
                {
                    ukupno = 1;
                }

                if (DT.Rows[0]["naziv"].ToString() != "")
                {
                    if (valuta == " kn")
                        return "Prema tečaju " + tecaj.ToString() + " ukupna vrijednost iznosi " + Math.Round(ukupno / tecaj, 3).ToString("#0.00") + " " + DT.Rows[0]["naziv"].ToString();
                    else
                        return "Prema tečaju " + tecaj.ToString() + " ukupna vrijednost iznosi " + Math.Round(ukupno * tecaj, 3).ToString("#0.00") + " KN";
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

        private DataRow RowPdv;

        private void StopePDVa(decimal pdv, decimal iznos, decimal osnovica)
        {
            DataRow[] dataROW = dSstope.Tables["DTstope"].Select("stopa = '" + Math.Round(pdv).ToString() + "'");

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
                dataROW[0]["iznos"] = Convert.ToDecimal(dataROW[0]["iznos"].ToString()) + Math.Round(iznos, 3);
                dataROW[0]["osnovica"] = Convert.ToDecimal(dataROW[0]["osnovica"].ToString()) + Math.Round(osnovica, 3);
            }
        }

        private void FillRacun(string broj, string[] imeTablica, string poslovnica, string naplatni)
        {
            PCPOS.classNumberToLetter broj_u_text = new PCPOS.classNumberToLetter();

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
                " WHERE " + imeTablica[1] + ".broj_racuna='" + broj + "'";
            if (imeTablica[1] == "racun_stavke") sql2 += " AND racun_stavke.id_ducan=" + poslovnica + " AND racun_stavke.id_kasa=" + naplatni;
            sql2 += " order by " + imeTablica[1] + ".id_stavka";

            //DSRfakturaStavke DSfs = new DSRfakturaStavke();

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }
            else
            {
                classSQL.NpgAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }

            decimal vpc = 0;
            decimal kol = 0;
            decimal porez;
            decimal rabat;
            decimal mpc;
            decimal mpcStavka;
            decimal rabatStavka = 0;
            decimal pdvStavka = 0;
            decimal osnovicaStavka = 0;
            decimal stopa = 0;
            decimal osnovicaUkupno = 0;
            decimal sveUkupno = 0;
            decimal pdvUkupno = 0;
            decimal rabatSve = 0;

            DataTable DT = dSRfakturaStavke.Tables[0];
            bool ima_marzu_na_auto = false;
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                vpc = Math.Round(Convert.ToDecimal(DT.Rows[i]["vpc"].ToString()), 3);
                kol = Math.Round(Convert.ToDecimal(DT.Rows[i]["kolicina"].ToString()), 3);
                porez = Math.Round(Convert.ToDecimal(DT.Rows[i]["porez"].ToString()), 3);
                rabat = Math.Round(Convert.ToDecimal(DT.Rows[i]["rabat"].ToString()), 3);

                mpc = Math.Round(vpc * (1m + porez / 100m) - 0.0000001m, 2);

                rabatStavka = Math.Round((mpc * kol) * rabat / 100, 3);
                mpcStavka = Math.Round(mpc * kol - rabatStavka, 3);

                decimal preracunataSto = (100 * porez) / (100 + porez);
                pdvStavka = Math.Round(mpcStavka * (preracunataSto / 100), 3);

                osnovicaStavka = Math.Round(mpcStavka - pdvStavka, 3);

                if ((mpc - vpc) != 0)
                    stopa = ((mpc - vpc) / vpc);
                else
                    stopa = 0;

                DT.Rows[i].SetField("mpc", Math.Round(mpc, 3));
                DT.Rows[i].SetField("vpc", Math.Round(vpc, 3));
                DT.Rows[i].SetField("porez", Math.Round(porez, 3));
                DT.Rows[i].SetField("rabat", Math.Round(rabat, 3));
                DT.Rows[i].SetField("kolicina", Math.Round(kol, 3));
                DT.Rows[i].SetField("mpcStavka", Math.Round(mpcStavka, 3));
                DT.Rows[i].SetField("rabatStavka", Math.Round(rabatStavka, 3));

                rabatSve += Math.Round(rabatStavka, 2);
                osnovicaUkupno += Math.Round(osnovicaStavka, 2);
                pdvUkupno += Math.Round(pdvStavka, 2);
                sveUkupno += Math.Round((mpcStavka), 2);

                StopePDVa(Math.Round(porez, 0), Math.Round(pdvStavka, 2), Math.Round(((osnovicaStavka)), 2));
            }

            //*********************************************************************************
            ///////////////////////////SLUŽI ZA POREZ NA MARŽU///////////////////////////////////
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                if (DT.Rows[i]["sifra"].ToString() == "MarzaRabljeniAuto")
                {
                    decimal BPmpc, BPvpc, SPmpc, SPvpc;
                    ima_marzu_na_auto = true;
                    decimal.TryParse(DT.Rows[i - 1]["mpc"].ToString(), out BPmpc);
                    decimal.TryParse(DT.Rows[i - 1]["vpc"].ToString(), out BPvpc);

                    decimal.TryParse(DT.Rows[i]["mpc"].ToString(), out SPmpc);
                    decimal.TryParse(DT.Rows[i]["vpc"].ToString(), out SPvpc);

                    DT.Rows[i - 1]["mpc"] = BPmpc + SPmpc;
                    DT.Rows[i - 1]["mpcStavka"] = BPmpc + SPmpc;
                    DT.Rows[i - 1]["vpc"] = BPvpc + SPvpc;
                    if (i == 0)
                    {
                        MessageBox.Show("GREŠKA!!!\r\nAko imate stavku \"MarzaRabljeniAuto\" na koju ide porez prvo morate upisti stavku na koju ne ide porez!");
                        this.Close();
                    }
                    DT.Rows[i].Delete();
                    i++;

                    //dSstope.Tables[0].Rows.Clear();
                    for (int f = 0; f < dSstope.Tables[0].Rows.Count; f++)
                    {
                        dSstope.Tables[0].Rows[f]["iznos"] = 0;
                    }
                }
            }
            //*********************************************************************************

            string broj_slovima = "Slovima: " + broj_u_text.PretvoriBrojUTekst(sveUkupno.ToString("0.00"), ',', "kn", "lp").ToString().ToLower();

            string year;
            if (imeTablica[0] == "ispis_racuni")
                year = DateTime.Now.Year.ToString();
            else
            {
                year = classSQL.select("SELECT datum_racuna FROM racuni WHERE broj_racuna='" + broj_dokumenta + "'", "racuni").Tables[0].Rows[0][0].ToString();
                DateTime date = Convert.ToDateTime(year);
                year = date.Year.ToString();
            }

            if (imeTablica[0] == "racuni")
            {
                //                sql = string.Format(@"select avio_registracija, avio_tip_zrakoplova, avio_maks_tezina_polijetanja
                //from {0}
                //where {0}.broj_fakture = '{1}' AND {0}.id_ducan = '{2}' AND {0}.id_kasa = '{3}';",
                //    imeTablica[0], broj, poslovnica, naplatni);

                //                if (imeTablica[0] == "racuni")
                //                {
                string sql_avio = string.Format(@"select avio_registracija, avio_tip_zrakoplova, round(avio_maks_tezina_polijetanja, 2) as avio_maks_tezina_polijetanja
from {0}
where {0}.broj_racuna = '{1}' AND {0}.id_ducan = '{2}' AND {0}.id_kasa = '{3}';",
imeTablica[0], broj, poslovnica, naplatni);
                //}
                DataSet dsAvio = classSQL.select(sql_avio, "avio");

                if (dsAvio != null && dsAvio.Tables.Count > 0 && dsAvio.Tables[0] != null && dsAvio.Tables[0].Rows.Count > 0)
                {
                    avio_registracija = dsAvio.Tables[0].Rows[0]["avio_registracija"].ToString();
                    avio_tip_zrakoplova = dsAvio.Tables[0].Rows[0]["avio_tip_zrakoplova"].ToString();
                    avio_maks_tezina_polijetanja = dsAvio.Tables[0].Rows[0]["avio_maks_tezina_polijetanja"].ToString();
                }
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
                " '" + rabatSve + "' AS rabat,";
            if (imeTablica[0] == "racuni") sql += "CAST (racuni.broj_racuna AS varchar) + '/' + ime_ducana + '/' + ime_blagajne AS Naslov,";
            else sql += " CAST (" + imeTablica[0] + ".broj_racuna AS nvarchar) + CAST ('" + Util.Korisno.VratiDucanIBlagajnuZaIspis(1) + "' AS nvarchar)  AS Naslov,";
            sql += " partners.ime_tvrtke AS kupac_tvrtka," +
                " partners.adresa AS kupac_adresa," +
                " ' Novčanice: ' + CAST(" + imeTablica[0] + ".ukupno_gotovina AS money) + '   Kartice: ' + CAST(" + imeTablica[0] + ".ukupno_kartice AS money)  AS placanje," +
                " partners.id_grad AS id_kupac_grad," +
                " partners.id_partner AS sifra_kupac," +
                " partners.napomena AS napomena_tvrtka," +
                " partners.oib_polje AS string5, " +
                " zemlja.zemlja AS kupac_drzava, " +
                " ziro_racun.ziroracun AS zr," +
                " '1960-1-1' AS datum_dvo," +
                " '1960-1-1' AS datum_valute," +
                " ziro_racun.ziroracun AS zr," +
                " '" + ima_marzu_na_auto.ToString() + "' AS string4," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio," +
                " ziro_racun.banka AS banka," +
                " '" + broj_slovima + "' AS broj_slovima," +
                " partners.oib AS kupac_oib" +
                " FROM " + imeTablica[0] + "" +
                " LEFT JOIN ziro_racun ON ziro_racun.id_ziroracun='1'" +
                " LEFT JOIN zacrnjeni_partner as  partners ON partners.id_partner=" + imeTablica[0] + ".id_kupac" +
                " LEFT JOIN zemlja ON zemlja.id_zemlja=partners.id_zemlja";
            if (imeTablica[0].ToString() == "racuni") sql += " LEFT JOIN ducan ON racuni.id_ducan = ducan.id_ducan " + " LEFT JOIN blagajna ON racuni.id_kasa = blagajna.id_blagajna ";
            sql += " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=" + imeTablica[0] + ".id_blagajnik WHERE " + imeTablica[0] + ".broj_racuna='" + broj_dokumenta + "'";
            if (imeTablica[0].ToString() == "racuni") sql += " AND racuni.id_ducan=" + poslovnica + " AND racuni.id_kasa=" + naplatni;

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
            dSFaktura.Tables[0].Rows[0].SetField("napomena_tvrtka", dSFaktura.Tables[0].Rows[0]["napomena_tvrtka"].ToString().Replace("\n", " ").Replace("\r", " "));

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
                //classSQL.NpgAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }

            string sakrij_jedinicnu_cijenu_0_1 = "0";
            if (ima_marzu_na_auto)
            {
                sakrij_jedinicnu_cijenu_0_1 = "1";
            }

            p10 = new ReportParameter("sakrij_jedinicnu_cijenu", sakrij_jedinicnu_cijenu_0_1);

            //this.reportViewer1.LocalReport.EnableExternalImages = true;
            //this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p10 });

            PostaviPodatkeOBanci(ref dSRpodaciTvrtke, ref dSFaktura);
            DateTime datum_ = Convert.ToDateTime(dSFaktura.Tables[0].Rows[0]["datum"].ToString());

            if (DTpostavke.Rows[0]["ispis_partnera"].ToString() == "0")
            {
                if (MessageBox.Show("Želite li na ovoj fakturi ispisati podatke o kupcu?", "Kupac", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    dSFaktura.Tables[0].Rows[0]["kupac_tvrtka"] = "";
                    dSFaktura.Tables[0].Rows[0]["kupac_adresa"] = "";
                    dSFaktura.Tables[0].Rows[0]["id_kupac_grad"] = "0";
                    dSFaktura.Tables[0].Rows[0]["sifra_kupac"] = "";
                    dSFaktura.Tables[0].Rows[0]["napomena_tvrtka"] = "";
                    dSFaktura.Tables[0].Rows[0]["kupac_drzava"] = "";
                    dSFaktura.Tables[0].Rows[0]["kupac_oib"] = "";
                    dSFaktura.Tables[0].Rows[0]["string5"] = "";
                    dSRpodaciTvrtke.Tables[0].Rows[0]["grad_kupac"] = "";
                }
            }

            reportViewer1.LocalReport.DisplayName = "Racun-" + dSFaktura.Tables[0].Rows[0]["broj_racuna"].ToString() + "-" + datum_.ToString("dd-MM-yyyy") + "";
        }

        private void FillFaktura(string broj, string[] imeTablica)
        {
            PCPOS.classNumberToLetter_engl broj_u_text_engl = new PCPOS.classNumberToLetter_engl();
            PCPOS.classNumberToLetter broj_u_text = new PCPOS.classNumberToLetter();

            decimal tecaj;

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
                " " + imeTablica[1] + ".ppmv," +
                " CASE WHEN length(faktura_stavke.naziv) IS NULL THEN roba.naziv ELSE faktura_stavke.naziv END as naziv," +
                //" roba.naziv as naziv," +
                " roba.opis as opis," +
                " CASE" +
                " WHEN roba.ean='-1' THEN ''" +
                " WHEN roba.ean='0' THEN ''" +
                " ELSE roba.ean" +
                " END " +
                " AS ean," +
                " roba.jm as jm," +
                " " + imeTablica[1] + ".id_skladiste AS skladiste" +
                " FROM " + imeTablica[1] + "" +
                " LEFT JOIN roba ON roba.sifra=" + imeTablica[1] + ".sifra" +
                " WHERE " + imeTablica[1] + ".broj_fakture='" + broj + "'" +
                " AND " + imeTablica[1] + ".id_ducan='" + poslovnica + "' AND " + imeTablica[1] + ".id_kasa='" + naplatni + "'" +
                " order by " + imeTablica[1] + ".id_stavka";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }
            else
            {
                classSQL.NpgAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }

            decimal vpc = 0;
            decimal vpc_cisto = 0;
            decimal kol = 0;
            decimal porez;
            decimal rabat;
            decimal mpcStavka;
            decimal mpcStavke_cisto;
            decimal rabatStavka = 0;
            decimal rabatStavka_cisto = 0;
            decimal pdvStavka = 0;
            decimal pdvStavka_cisto = 0;
            decimal osnovicaStavka = 0;
            decimal rabatSve = 0;
            decimal mpc = 0;
            decimal sveUkupno = 0;
            decimal stopa = 0;
            decimal pdvUkupno = 0;
            decimal osnovicaUkupno = 0;
            decimal cista_cijena_sveukupno = 0;
            decimal ppmv_ukp = 0;
            //decimal posebni_porez_motorna_vozila = 0;
            //decimal ukp_posebni_porez_motorna_vozila = 0;

            DataTable DT = dSRfakturaStavke.Tables[0];
            string velep = classSQL.select_settings("Select veleprodaja From postavke", "veleprodaja").Tables[0].Rows[0][0].ToString();
            bool ima_marzu_na_auto = false;
            if (velep == "1")
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    vpc = Math.Round(Convert.ToDecimal(DT.Rows[i]["vpc"].ToString()) / tecaj, 4);
                    vpc_cisto = Math.Round(Convert.ToDecimal(DT.Rows[i]["vpc"].ToString()), 4);
                    kol = Math.Round(Convert.ToDecimal(DT.Rows[i]["kolicina"].ToString()), 4);
                    porez = Math.Round(Convert.ToDecimal(DT.Rows[i]["porez"].ToString()), 4);
                    rabat = Math.Round(Convert.ToDecimal(DT.Rows[i]["rabat"].ToString()), 4);

                    mpc = Math.Round(vpc * (1m + porez / 100.0m) - 0.0000001m, 4);
                    stopa = ((mpc - vpc) / vpc);
                    rabatStavka = Math.Round(vpc * kol * rabat / 100, 4);
                    rabatStavka_cisto = Math.Round(vpc_cisto * kol * rabat / 100, 4);
                    mpcStavka = Math.Round(((vpc * kol - rabatStavka)), 4);
                    mpcStavke_cisto = Math.Round(((vpc_cisto * kol - rabatStavka_cisto)), 4);
                    pdvStavka = Math.Round(mpcStavka * (porez / 100), 4);
                    pdvStavka_cisto = Math.Round(mpcStavke_cisto * (porez / 100), 4);
                    osnovicaStavka = Math.Round(mpcStavka, 4);

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
                    cista_cijena_sveukupno += mpcStavke_cisto + pdvStavka_cisto;

                    if (racunajTecaj)
                    {
                        StopePDVa(Math.Round((stopa * 100), 4), Math.Round(pdvStavka_cisto, 4), Math.Round(((vpc * kol) - rabatStavka), 4));
                    }
                    else
                    {
                        StopePDVa(Math.Round((stopa * 100), 4), Math.Round(pdvStavka_cisto, 4), Math.Round(((vpc_cisto * kol) - rabatStavka_cisto), 4));
                    }
                }
            }
            else
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    vpc = Math.Round(Convert.ToDecimal(DT.Rows[i]["vpc"].ToString()) / tecaj, 4);
                    vpc_cisto = Math.Round(Convert.ToDecimal(DT.Rows[i]["vpc"].ToString()) / tecaj, 4);
                    kol = Math.Round(Convert.ToDecimal(DT.Rows[i]["kolicina"].ToString()), 4);
                    porez = Math.Round(Convert.ToDecimal(DT.Rows[i]["porez"].ToString()), 3);
                    rabat = Math.Round(Convert.ToDecimal(DT.Rows[i]["rabat"].ToString()), 3);
                    ppmv_ukp += (Math.Round(Convert.ToDecimal(DT.Rows[i]["ppmv"].ToString()), 3) * kol);

                    mpc = Math.Round(vpc * (1m + porez / 100.0m) - 0.0000001m, 4);
                    try
                    {
                        stopa = ((mpc - vpc) / vpc);
                    }
                    catch
                    {
                        stopa = porez / 100;
                    }

                    rabatStavka = Math.Round(vpc * kol * rabat / 100, 4);
                    rabatStavka_cisto = Math.Round(vpc_cisto * kol * rabat / 100, 4);

                    if (pdvStavka == 0)
                        mpcStavka = Math.Round(vpc * kol - rabatStavka, 2, MidpointRounding.AwayFromZero);
                    else
                        mpcStavka = Math.Round(vpc * kol - rabatStavka, 3);

                    mpcStavke_cisto = Math.Round(((vpc_cisto * kol - rabatStavka_cisto)), 4);
                    pdvStavka = Math.Round(mpcStavka * (porez / 100), 4);
                    pdvStavka_cisto = Math.Round(mpcStavke_cisto * (porez / 100), 4);
                    osnovicaStavka = Math.Round(mpcStavka, 2);

                    //promijenio 13,04,2015
                    //pdvStavka round iz 4->2
                    //osnovicaStavka round iz 4->2

                    DT.Rows[i].SetField("mpc", mpc);
                    DT.Rows[i].SetField("vpc", vpc);
                    DT.Rows[i].SetField("porez", porez);
                    DT.Rows[i].SetField("rabat", rabat);
                    DT.Rows[i].SetField("kolicina", kol);
                    DT.Rows[i].SetField("mpcStavka", Math.Round(mpcStavka, 2, MidpointRounding.AwayFromZero));
                    DT.Rows[i].SetField("rabatStavka", rabatStavka);

                    if (DT.Rows[i]["opis"].ToString() != "" && DT.Rows[i]["opis"].ToString() != "1")
                    {
                        //DT.Rows[i].SetField("naziv", DT.Rows[i]["naziv"].ToString() + "\r\nOPIS ARTIKLA:\r\n" + DT.Rows[i]["opis"].ToString());
                        DT.Rows[i].SetField("naziv", DT.Rows[i]["naziv"].ToString());
                    }

                    rabatSve += rabatStavka;
                    osnovicaUkupno += osnovicaStavka;
                    pdvUkupno += pdvStavka;

                    decimal ss = Math.Round((mpcStavka + pdvStavka), 2);

                    if (pdvStavka == 0)
                        sveUkupno += Math.Round((mpcStavka + pdvStavka), 2);
                    else
                        sveUkupno += Math.Round((mpcStavka + pdvStavka), 3);

                    cista_cijena_sveukupno += mpcStavke_cisto + pdvStavka_cisto;
                    if (racunajTecaj)
                    {
                        StopePDVa(Math.Round((stopa * 100), 4), Math.Round(pdvStavka_cisto, 4), Math.Round(((vpc * kol) - rabatStavka), 4));
                    }
                    else
                    {
                        StopePDVa(Math.Round((stopa * 100), 4), Math.Round(pdvStavka_cisto, 4), Math.Round(((vpc_cisto * kol) - rabatStavka_cisto), 4));
                    }
                }

                //*********************************************************************************
                ///////////////////////////SLUŽI ZA POREZ NA MARŽU///////////////////////////////////

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (DT.Rows[i]["sifra"].ToString() == "MarzaRabljeniAuto")
                    {
                        decimal BPmpc, BPvpc, SPmpc, SPvpc;
                        ima_marzu_na_auto = true;

                        decimal.TryParse(DT.Rows[i - 1]["mpc"].ToString(), out BPmpc);
                        decimal.TryParse(DT.Rows[i - 1]["vpc"].ToString(), out BPvpc);

                        decimal.TryParse(DT.Rows[i]["mpc"].ToString(), out SPmpc);
                        decimal.TryParse(DT.Rows[i]["vpc"].ToString(), out SPvpc);

                        DT.Rows[i - 1]["mpc"] = BPmpc + SPmpc;
                        DT.Rows[i - 1]["mpcStavka"] = BPmpc + SPmpc;
                        DT.Rows[i - 1]["vpc"] = BPvpc + SPvpc;
                        if (i == 0)
                        {
                            MessageBox.Show("GREŠKA!!!\r\nAko imate stavku \"MarzaRabljeniAuto\" na koju ide porez prvo morate upisti stavku na koju ne ide porez!");
                            this.Close();
                        }
                        DT.Rows[i].Delete();
                        i++;
                    }
                }
                //*********************************************************************************
            }

            string val = " Select " + imeTablica[0] + ".id_valuta, valute.ime_valute AS valuta FROM " + imeTablica[0] + " " +
             " LEFT JOIN valute ON valute.id_valuta=" + imeTablica[0] + ".id_valuta WHERE " + imeTablica[0] + ".broj_fakture='" + broj + "' AND " + imeTablica[0] + ".id_ducan='" + poslovnica + "' AND " + imeTablica[0] + ".id_kasa='" + naplatni + "'";
            DataTable DTval = classSQL.select(val, "valute").Tables[0];
            sveUkupno += ppmv_ukp;
            string broj_slovima = "Slovima: " + broj_u_text.PretvoriBrojUTekst(sveUkupno.ToString("0.00"), ',', "kn", "lp").ToString().ToLower();
            if (racunajTecaj) broj_slovima = "";
            if (racunajTecaj == true)
            {
                if (DTval.Rows[0]["valuta"].ToString() == "978 EUR")
                    broj_slovima = "Slovima: " + broj_u_text.PretvoriBrojUTekst(sveUkupno.ToString("0.00"), ',', "€", "c").ToString().ToLower();

                if (DTval.Rows[0]["valuta"].ToString() == "036 AUD")
                    broj_slovima = "Slovima: " + broj_u_text.PretvoriBrojUTekst(sveUkupno.ToString("0.00"), ',', "$", "c").ToString().ToLower();
            }

            string sql = "SELECT " +
                " " + imeTablica[0] + ".broj_fakture," +
                " " + imeTablica[0] + ".broj_avansa," +
                " " + imeTablica[0] + ".godina_avansa," +
                " " + imeTablica[0] + ".date AS datum," +
                " " + imeTablica[0] + ".dateDVO AS datum_dvo," +
                " " + imeTablica[0] + ".datum_valute," +
                " " + imeTablica[0] + ".jir," +
                " " + imeTablica[0] + ".zki," +
                " " + imeTablica[0] + ".tecaj AS tecaj," +
                " valute.ime_valute AS valuta," +
                " " + imeTablica[0] + ".mj_troska AS mjesto_troska," +
                " case when " + imeTablica[0] + ".pouzece = true then concat(nacin_placanja.naziv_placanja, ' - POUZEĆEM') else nacin_placanja.naziv_placanja end AS placanje," +
                " otprema.naziv AS otprema," +
                " CAST (" + imeTablica[0] + ".model AS nvarchar) + '  '+ CAST (" + imeTablica[0] + ".broj_fakture AS nvarchar)+" +
                "CAST (" + imeTablica[0] + ".godina_fakture AS nvarchar)+'-'+CAST (" + imeTablica[0] + ".id_fakturirati AS nvarchar) AS model," +
                " " + imeTablica[0] + ".napomena," +
                " '" + rabatSve + "' AS rabat," +
                " '" + sveUkupno + "' AS ukupno," +
                " '" + pdvUkupno + "' AS iznos_pdv," +
                " '" + osnovicaUkupno + "' AS osnovica," +
                " " + imeTablica[0] + ".godina_fakture," +
                //" CAST (fakture.broj_fakture AS nvarchar) +'/'+ CAST (fakture.godina_fakture AS nvarchar) AS Naslov," +
                " CAST (" + imeTablica[0] + ".broj_fakture AS nvarchar) + CAST ('" + Util.Korisno.VratiDucanIBlagajnuZaIspis(2) + "' AS nvarchar)  AS Naslov," +
                " partners.ime_tvrtke AS kupac_tvrtka," +
                " 'Datum isporuke:' AS naziv_date1," +
                " 'Datum dospijeća:' AS naziv_date2," +
                " partners.adresa AS kupac_adresa," +
                " partners.id_grad AS id_kupac_grad," +
                " partners.id_partner AS sifra_kupac," +
                " partners.napomena AS napomena_tvrtka," +
                " zemlja.zemlja AS kupac_drzava, " +
                " ziro_racun.ziroracun AS zr," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio," +
                " ziro_racun.banka AS banka," +
                " '" + broj_slovima + "' AS broj_slovima," +
                " partners.oib AS kupac_oib, " +
                //" case when ((jir IS NULL OR jir = '') AND (zki IS NULL OR zki = '')) then 'OVO NIJE FISKALIZIRAN RAČUN' else '' end as string3," +
                " partners.oib_polje AS string5, '' as string4 " +
                " FROM " + imeTablica[0] + "" +
                " LEFT JOIN zacrnjeni_partner as  partners ON partners.id_partner=" + imeTablica[0] + ".id_odrediste" +
                " LEFT JOIN otprema ON otprema.id_otprema=" + imeTablica[0] + ".otprema" +
                " LEFT JOIN nacin_placanja ON nacin_placanja.id_placanje=" + imeTablica[0] + ".id_nacin_placanja" +
                " LEFT JOIN ziro_racun ON ziro_racun.id_ziroracun=" + imeTablica[0] + ".zr" +
                " LEFT JOIN valute ON valute.id_valuta=" + imeTablica[0] + ".id_valuta" +
                " LEFT JOIN zemlja ON zemlja.id_zemlja=partners.id_zemlja" +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=" + imeTablica[0] + ".id_zaposlenik_izradio WHERE " + imeTablica[0] + ".broj_fakture='" + broj + "'" +
                " AND " + imeTablica[0] + ".id_ducan='" + poslovnica + "' AND " + imeTablica[0] + ".id_kasa='" + naplatni + "'" +
                "";

            //AND fakture.id_ducan='" + poslovnica + "' AND fakture.id_kasa='" + naplatni + "'

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql).Fill(dSFaktura, "DTRfaktura");
            }
            else
            {
                classSQL.NpgAdatpter(sql.Replace("nvarchar", "varchar")).Fill(dSFaktura, "DTRfaktura");
            }

            try
            {
                dSFaktura.Tables[0].Rows[0].SetField("napomena_tvrtka", dSFaktura.Tables[0].Rows[0]["napomena_tvrtka"].ToString().Replace("\n", " ").Replace("\r", " "));
            }
            catch (Exception)
            {
            }

            string dodatak = "";

            if (DTpostavke.Rows[0]["oslobodenje_pdva"].ToString() == "1" && pdvUkupno == 0)
                dodatak = "\r\n\r\nPrijenos porezne obveze prema čl. 75., st. 3b. i čl. 79. Zakona o PDV-u. Kupac će sam obračunati PDV od 25% i iskazati ga u poreznoj prijavi.";
            else if (DTpostavke.Rows[0]["oslobodenje_pdva"].ToString() == "1")
                dodatak = "\r\n\r\nObračun prema naplaćenoj naknadi";

            dSFaktura.Tables[0].Rows[0]["napomena"] = dSFaktura.Tables[0].Rows[0]["napomena"] + dodatak;

            DataTable DT1 = classSQL.select(sql.Replace("nvarchar", "varchar"), "fakture").Tables[0];
            string natpis = "";
            string porezi_novo = "";
            string avans_natpis = "";
            string avans_porezi_novo = "";
            string avans_razlika_zaPlatiti = "";
            decimal ukupno_porez_pdv = 0;

            if (DT1.Rows[0]["broj_avansa"].ToString() != "0" && DT1.Rows[0]["broj_avansa"].ToString() != "")
            {
                DataTable DTavans = classSQL.select("" +
                    "SELECT * FROM avansi " +
                    " WHERE godina_avansa='" + DT1.Rows[0]["godina_avansa"].ToString() + "' " +
                    " AND broj_avansa=" + DT1.Rows[0]["broj_avansa"].ToString() + "", "avansi").Tables[0];

                DataTable DTavansPor = classSQL.select("Select * from porezi where id_porez='" + DTavans.Rows[0]["id_pdv"].ToString() + "'", "porezi avansa").Tables[0];
                avans_natpis += "\r\nUplaćeno bruto po računu za predujam br. " + DTavans.Rows[0]["broj_avansa"].ToString() + ":\r\n";
                avans_natpis += "Osnovica PDV-a iz predujma:\r\n";
                avans_natpis += "PDV iz predujma " + DTavansPor.Rows[0]["iznos"].ToString() + "%:\r\n";
                avans_natpis += "Razlika PDV-a " + DTavansPor.Rows[0]["iznos"].ToString() + "%:";

                decimal aUku = 0;
                decimal aOnov = 0;
                decimal aPdv = 0;
                decimal razlika_pdva = 0;
                if (dSstope.Tables[0].Rows.Count != 0)
                {
                    for (int i = 0; i < dSstope.Tables[0].Rows.Count; i++)
                    {
                        if (dSstope.Tables[0].Rows[i]["stopa"].ToString() == DTavansPor.Rows[0]["iznos"].ToString())
                        {
                            decimal iznos_pdv = Convert.ToDecimal(dSstope.Tables[0].Rows[i]["iznos"].ToString());
                            decimal iznos_pdv_avans = Convert.ToDecimal(DTavans.Rows[0]["porez_var"].ToString());
                            razlika_pdva = iznos_pdv - iznos_pdv_avans;
                        }
                    }
                }

                decimal.TryParse(DTavans.Rows[0]["ukupno"].ToString(), out aUku);
                decimal.TryParse(DTavans.Rows[0]["osnovica_var"].ToString(), out aOnov);
                decimal.TryParse(DTavans.Rows[0]["porez_var"].ToString(), out aPdv);

                avans_porezi_novo += "\r\n" + string.Format("{0:n}", Math.Round(aUku)) + " kn\r\n";
                avans_porezi_novo += string.Format("{0:n}", Math.Round(aOnov)) + " kn\r\n";
                avans_porezi_novo += string.Format("{0:n}", Math.Round(aPdv)) + " kn\r\n";
                avans_porezi_novo += string.Format("{0:n}", Math.Round(razlika_pdva, 3)) + " kn";

                //string pokusno = "";
                //pokusno += "Uplaćeno bruto po računu za predujam br. " + DTavans.Rows[0]["broj_avansa"].ToString() + ": "+String.Format("{0:n}", Math.Round(aUku)) + " kn\r\n";
                //pokusno += "Osnovica PDV-a iz predujma: " + String.Format("{0:n}", Math.Round(aOnov)) + " kn\r\n";
                //pokusno += "PDV iz predujma " + DTavansPor.Rows[0]["iznos"].ToString() + "%: " + String.Format("{0:n}", Math.Round(aPdv)) + " kn\r\n";
                //pokusno += "Razlika PDV-a " + DTavansPor.Rows[0]["iznos"].ToString() + "%: " + String.Format("{0:n}", Math.Round(razlika_pdva, 3)) + " kn";

                avans_razlika_zaPlatiti = "Po ovom računu zbog obračuna predujma razlika za plačanje je:  " + string.Format("{0:n}", (Convert.ToDecimal(sveUkupno) - aUku)) + " kn\r\n";
            }

            DataRow DTrow1 = dataSet1.Tables["DSavansiFak"].NewRow();
            DTrow1["avans_iznosi"] = avans_porezi_novo;
            DTrow1["avans_porez_natpis"] = avans_natpis;
            DTrow1["napomena_avans_ukupno"] = avans_razlika_zaPlatiti;
            dataSet1.Tables["DSavansiFak"].Rows.Add(DTrow1);

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

                if (dSRpodaciTvrtke.Tables[0].Rows.Count > 0)
                {
                    string iban = dSRpodaciTvrtke.Tables[0].Rows[0]["iban"].ToString();

                    if (iban.Length > 5)
                    {
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

                    string swift = dSRpodaciTvrtke.Tables[0].Rows[0]["swift"].ToString();
                    if (swift.Length > 5)
                    {
                        string[] swifts = swift.Split(';');
                        string swiftR = "";
                        foreach (string s in swifts)
                        {
                            swiftR += s + "\r\n";
                        }
                        if (swiftR.Length > 4)
                        {
                            swiftR = swiftR.Remove(swiftR.Length - 2);
                        }
                        dSRpodaciTvrtke.Tables[0].Rows[0]["swift"] = swiftR;
                    }
                }

                //classSQL.NpgAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }

            PostaviPodatkeOBanci(ref dSRpodaciTvrtke, ref dSFaktura);
            DateTime datum_ = Convert.ToDateTime(dSFaktura.Tables[0].Rows[0]["datum"].ToString());

            if (DTpostavke.Rows[0]["ispis_partnera"].ToString() == "0")
            {
                if (MessageBox.Show("Želite li na ovoj fakturi ispisati podatke o kupcu?", "Kupac", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    dSFaktura.Tables[0].Rows[0]["kupac_tvrtka"] = "";
                    dSFaktura.Tables[0].Rows[0]["kupac_adresa"] = "";
                    dSFaktura.Tables[0].Rows[0]["id_kupac_grad"] = "0";
                    dSFaktura.Tables[0].Rows[0]["sifra_kupac"] = "";
                    dSFaktura.Tables[0].Rows[0]["napomena_tvrtka"] = "";
                    dSFaktura.Tables[0].Rows[0]["kupac_drzava"] = "";
                    dSFaktura.Tables[0].Rows[0]["kupac_oib"] = "";
                    dSFaktura.Tables[0].Rows[0]["string5"] = "";
                    dSRpodaciTvrtke.Tables[0].Rows[0]["grad_kupac"] = "";
                }
            }

            reportViewer1.LocalReport.DisplayName = "Faktura-" + dSFaktura.Tables[0].Rows[0]["broj_fakture"].ToString() + "-" + datum_.ToString("dd-MM-yyyy") + "";
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

            string sakrij_jedinicnu_cijenu_0_1 = "0";

            if (ima_marzu_na_auto)
            {
                sakrij_jedinicnu_cijenu_0_1 = "1";
            }

            if (imeTablica[0] == "fakture")
            {
                sql = string.Format(@"select avio_registracija, avio_tip_zrakoplova, round(avio_maks_tezina_polijetanja, 2) as avio_maks_tezina_polijetanja
from {0}
where {0}.broj_fakture = '{1}' AND {0}.id_ducan = '{2}' AND {0}.id_kasa = '{3}';",
    imeTablica[0], broj, poslovnica, naplatni);

                //                if (imeTablica[0] == "racuni") {
                //                    sql = string.Format(@"select avio_registracija, avio_tip_zrakoplova, avio_maks_tezina_polijetanja
                //from {0}
                //where {0}.broj_racuna = '{1}' AND {0}.id_ducan = '{2}' AND {0}.id_kasa = '{3}';",
                //    imeTablica[0], broj, poslovnica, naplatni);
                //                }
                DataSet dsAvio = classSQL.select(sql, "avio");

                if (dsAvio != null && dsAvio.Tables.Count > 0 && dsAvio.Tables[0] != null && dsAvio.Tables[0].Rows.Count > 0)
                {
                    avio_registracija = dsAvio.Tables[0].Rows[0]["avio_registracija"].ToString();
                    avio_tip_zrakoplova = dsAvio.Tables[0].Rows[0]["avio_tip_zrakoplova"].ToString();
                    avio_maks_tezina_polijetanja = dsAvio.Tables[0].Rows[0]["avio_maks_tezina_polijetanja"].ToString();
                }
            }

            p10 = new ReportParameter("sakrij_jedinicnu_cijenu", sakrij_jedinicnu_cijenu_0_1);
            p21 = new ReportParameter("ppmv", ppmv_ukp.ToString());
            ReportParameter p7 = new ReportParameter("iznos_valuta_kn", u_kunama);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p7 });
        }

        private void FillPonude(string broj, string[] imeTablica, string imeBrojPonude, string imeGodinaPonude)
        {
            classNumberToLetter broj_u_text = new classNumberToLetter();

            decimal tecaj;

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
                " " + imeTablica[1] + "." + imeBrojPonude + ",";
            if (ponudaUNbc)
            {
                sql2 += " '0' as rabat,";
            }
            else
            {
                sql2 += " " + imeTablica[1] + ".rabat,";
            }
            sql2 += " " + imeTablica[1] + ".sifra," +
            " " + imeTablica[1] + ".naziv as naziv," +
            " roba.jm as jm," +
            " " + imeTablica[1] + ".id_skladiste AS skladiste" +
            " FROM " + imeTablica[1] + "" +
            " LEFT JOIN roba ON roba.sifra=" + imeTablica[1] + ".sifra" +
            " WHERE " + imeTablica[1] + "." + imeBrojPonude + "='" + broj + "'" +
            " order by " + imeTablica[1] + ".id_stavka";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }
            else
            {
                classSQL.NpgAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }

            decimal vpc = 0;
            decimal kolicina = 0;
            decimal porez;
            decimal rabatPostotak;
            decimal mpc;
            decimal mpcStavka;
            decimal rabatStavka = 0;
            decimal pdvStavka = 0;
            decimal osnovicaStavka = 0;

            decimal osnovicaUkupno = 0;
            decimal sveUkupno = 0;
            decimal pdvUkupno = 0;
            decimal rabatSve = 0;

            DataTable DT = dSRfakturaStavke.Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                vpc = Math.Round((Convert.ToDecimal(DT.Rows[i]["vpc"].ToString()) / tecaj), 4);
                kolicina = Math.Round(Convert.ToDecimal(DT.Rows[i]["kolicina"].ToString()), 3);
                porez = Math.Round(Convert.ToDecimal(DT.Rows[i]["porez"].ToString()), 3);
                rabatPostotak = Math.Round(Convert.ToDecimal(DT.Rows[i]["rabat"].ToString()), 3);

                mpc = Math.Round(vpc * (1m + porez / 100.0m) - 0.0000001m, 3);

                rabatStavka = vpc * kolicina * rabatPostotak / 100;
                osnovicaStavka = Math.Round(vpc * kolicina - rabatStavka, 3);
                pdvStavka = Math.Round(osnovicaStavka * (porez / 100), 3);

                DT.Rows[i].SetField("mpc", Math.Round(mpc, 3));
                DT.Rows[i].SetField("vpc", Math.Round(vpc, 3));
                DT.Rows[i].SetField("porez", Math.Round(porez, 3));
                DT.Rows[i].SetField("rabat", Math.Round(rabatPostotak, 3));
                DT.Rows[i].SetField("kolicina", Math.Round(kolicina, 3));
                if (Util.Korisno.oibTvrtke == "13504838223")
                {
                    DT.Rows[i].SetField("mpcStavka", Math.Round(osnovicaStavka, 3));
                }
                else
                {
                    DT.Rows[i].SetField("mpcStavka", Math.Round(osnovicaStavka + pdvStavka, 3));
                }
                DT.Rows[i].SetField("rabatStavka", Math.Round(rabatStavka, 3));

                rabatSve += rabatStavka;
                osnovicaUkupno += osnovicaStavka;
                pdvUkupno += pdvStavka;
                sveUkupno += (osnovicaStavka + pdvStavka);

                try
                {
                    StopePDVa(porez, Math.Round(pdvStavka, 4), Math.Round(((vpc * kolicina) - rabatStavka), 4));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            string broj_slovima = "Slovima: " + broj_u_text.PretvoriBrojUTekst(sveUkupno.ToString("0.00"), ',', "kn", "lp").ToString().ToLower();
            if (racunajTecaj) broj_slovima = "";

            string sql = "SELECT " +
                " " + imeTablica[0] + "." + imeBrojPonude + "," +
                " " + imeTablica[0] + ".date AS datum," +
                " " + imeTablica[0] + ".tecaj AS tecaj," +
                " valute.ime_valute AS valuta," +
                " " + imeTablica[0] + ".vrijedi_do AS datum_dvo," +
                " '01.01.1000' AS datum_valute, " +
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
                " 'OVO NIJE FISKALIZIRAN RAČUN' AS string3, " +
                " partners.oib_polje AS string5, " +
                " case when partners.tel is null then '' else partners.tel end AS string4, " +
                " zemlja.zemlja AS kupac_drzava, " +
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
                " LEFT JOIN zemlja ON zemlja.id_zemlja=partners.id_zemlja" +
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

            dSFaktura.Tables[0].Rows[0].SetField("napomena_tvrtka", dSFaktura.Tables[0].Rows[0]["napomena_tvrtka"].ToString().Replace("\n", " ").Replace("\r", " "));

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

            string naziv_fakture = "";
            if (File.Exists("predracun"))
            {
                naziv_fakture = " 'Predračun: ' AS naziv_fakture,";
            }
            else
            {
                naziv_fakture = " 'Ponuda: ' AS naziv_fakture,";
            }

            string sql1 = SqlPodaciTvrtke.VratiSql(id_kupac, naziv_fakture, "");

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
                    if (iban.Length > 5)
                    {
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
                //classSQL.NpgAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }

            PostaviPodatkeOBanci(ref dSRpodaciTvrtke, ref dSFaktura);
            DateTime datum_ = Convert.ToDateTime(dSFaktura.Tables[0].Rows[0]["datum"].ToString());
            if (samoIspis)
            {
                reportViewer1.LocalReport.DisplayName = "Ponuda-" + dSFaktura.Tables[0].Rows[0]["broj_fakture"].ToString() + "-" + datum_.ToString("dd-MM-yyyy") + "";
            }
            else
            {
                reportViewer1.LocalReport.DisplayName = "Ponuda-" + dSFaktura.Tables[0].Rows[0]["broj_ponude"].ToString() + "-" + datum_.ToString("dd-MM-yyyy") + "";
            }
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
                    " CASE WHEN otpremnice.use_nbc = TRUE then otpremnica_stavke.nbc::numeric else otpremnica_stavke.vpc end as vpc," +
                    " CASE WHEN otpremnice.use_nbc = TRUE then '0' else otpremnica_stavke.porez end as porez," +
                    " otpremnica_stavke.broj_otpremnice," +
                    " otpremnica_stavke.rabat," +
                    " otpremnica_stavke.sifra_robe as sifra," +
                    " roba.naziv as naziv," +
                    " roba.jm as jm," +
                    " otpremnica_stavke.id_skladiste AS skladiste" +
                    " FROM otpremnica_stavke" +
                    " LEFT JOIN otpremnice on otpremnica_stavke.id_otpremnice= otpremnice.id_otpremnica" +
                    " LEFT JOIN roba ON roba.sifra=otpremnica_stavke.sifra_robe WHERE otpremnica_stavke.id_otpremnice='" + broj + "'" +
                    " order by otpremnica_stavke.id_stavka";
            }
            else
            {
                sql2 = "SELECT " +
                    " otpremnica_stavke.kolicina," +
                    " CASE WHEN otpremnice.use_nbc = TRUE then otpremnica_stavke.nbc::numeric else otpremnica_stavke.vpc end as vpc," +
                    " CASE WHEN otpremnice.use_nbc = TRUE then '0' else otpremnica_stavke.porez end as porez," +
                    " otpremnica_stavke.broj_otpremnice," +
                    " otpremnica_stavke.rabat," +
                    " otpremnica_stavke.sifra_robe as sifra," +
                    " roba.naziv as naziv," +
                    " roba.jm as jm," +
                    " otpremnica_stavke.id_skladiste AS skladiste" +
                    " FROM otpremnica_stavke" +
                    " LEFT JOIN otpremnice on otpremnica_stavke.id_otpremnice= otpremnice.id_otpremnica" +
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

            decimal vpc = 0;
            decimal kol = 0;
            decimal porez;
            decimal rabat;
            decimal mpc;
            decimal mpcStavka;
            decimal rabatStavka = 0;
            decimal pdvStavka = 0;
            decimal osnovicaStavka = 0;
            decimal osnovicaUkupno = 0;
            decimal sveUkupno = 0;
            decimal pdvUkupno = 0;
            decimal rabatSve = 0;

            DataTable DT = dSRfakturaStavke.Tables[0];

            string broj_slovima = "Slovima: " + broj_u_text.PretvoriBrojUTekst(sveUkupno.ToString("0.00"), ',', "kn", "lp").ToString().ToLower();
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
                " otpremnice.datum AS datum," +
                " '01.01.1600' AS datum_dvo," +
                " '01.01.1600' AS datum_valute," +
                " 'Mjesto otpreme:  '+otpremnice.mj_otpreme  + '\nAdresa otpreme:  ' + otpremnice.adr_otpreme + '\nIstovarno mjesto:  ' +  otpremnice.istovarno_mj + '\nRegistracija:  ' +  otpremnice.registracija    AS string1," +
                " 'Isprave:  '+otpremnice.isprave  + '\nTroškovi prijevoza:  ' + otpremnice.troskovi_prijevoza + '\nIstovarni rok:  ' +  otpremnice.istovarni_rok    AS string2," +
                " otprema.naziv AS otprema," +
                " otpremnice.napomena," +
                " '" + rabatSve + "' AS rabat," +
                " '" + sveUkupno + "' AS ukupno," +
                " '" + pdvUkupno + "' AS iznos_pdv," +
                " '" + osnovicaUkupno + "' AS osnovica," +
                " otpremnice.godina_otpremnice," +
                " CAST (otpremnice.broj_otpremnice AS nvarchar) +'/'+ CAST (otpremnice.godina_otpremnice AS nvarchar) AS Naslov," +
                " partners.ime_tvrtke AS kupac_tvrtka," +
                " partners.adresa AS kupac_adresa," +
                " partners.id_grad AS id_kupac_grad," +
                " partners.id_partner AS sifra_kupac," +
                " partners.napomena AS napomena_tvrtka," +
                " 'OVO NIJE FISKALIZIRAN RAČUN' AS string3, " +
                " partners.oib_polje AS string5, " +
                " zemlja.zemlja AS kupac_drzava, " +
                " ziro_racun.ziroracun AS zr," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio," +
                " ziro_racun.banka AS banka," +
                " '" + broj_slovima + "' AS broj_slovima," +
                " partners.oib AS kupac_oib, " +
                " otpremnice.use_nbc AS string4 " +
                " FROM otpremnice" +
                " LEFT JOIN zacrnjeni_partner as  partners ON partners.id_partner=otpremnice.osoba_partner" +
                " LEFT JOIN otprema ON otprema.id_otprema=otpremnice.id_otprema" +
                " LEFT JOIN zemlja ON zemlja.id_zemlja=partners.id_zemlja" +
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

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                bool has_nbc = false;
                has_nbc = Convert.ToBoolean(dSFaktura.Tables[0].Rows[0]["string4"].ToString());

                vpc = Math.Round(Convert.ToDecimal(DT.Rows[i]["vpc"].ToString()), 3);
                kol = Math.Round(Convert.ToDecimal(DT.Rows[i]["kolicina"].ToString()), 3);
                porez = Math.Round(Convert.ToDecimal(DT.Rows[i]["porez"].ToString()), 3);
                rabat = Math.Round(Convert.ToDecimal(DT.Rows[i]["rabat"].ToString()), 3);

                mpc = Math.Round(vpc * (1m + porez / 100.0m) - 0.0000001m, 3);

                rabatStavka = vpc * kol * rabat / 100;
                mpcStavka = Math.Round(vpc * kol - rabatStavka, 3);
                pdvStavka = Math.Round(mpcStavka * (porez / 100), 3);
                osnovicaStavka = mpcStavka;

                DT.Rows[i].SetField("mpc", Math.Round(mpc, 3));
                DT.Rows[i].SetField("vpc", Math.Round(vpc, 3));
                DT.Rows[i].SetField("porez", Math.Round(porez, 3));
                DT.Rows[i].SetField("rabat", Math.Round(rabat, 3));
                DT.Rows[i].SetField("kolicina", Math.Round(kol, 3));
                DT.Rows[i].SetField("mpcStavka", Math.Round(mpcStavka, 3));
                DT.Rows[i].SetField("rabatStavka", Math.Round(rabatStavka, 3));

                if (!has_nbc)
                {
                    try
                    {
                        StopePDVa(porez, Math.Round(pdvStavka, 4), Math.Round(((vpc * kol) - rabatStavka), 4));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    rabatSve += rabatStavka;
                    osnovicaUkupno += osnovicaStavka;
                    pdvUkupno += pdvStavka;
                    sveUkupno += (mpcStavka + pdvStavka);
                }
                else
                {
                    sveUkupno += (vpc * kol);
                }
            }

            dSFaktura.Tables[0].Rows[0].SetField("rabat", rabatSve);
            dSFaktura.Tables[0].Rows[0].SetField("ukupno", sveUkupno);
            dSFaktura.Tables[0].Rows[0].SetField("iznos_pdv", pdvUkupno);
            dSFaktura.Tables[0].Rows[0].SetField("osnovica", osnovicaUkupno);
            broj_slovima = "Slovima: " + broj_u_text.PretvoriBrojUTekst(sveUkupno.ToString("0.00"), ',', "kn", "lp").ToString().ToLower();
            dSFaktura.Tables[0].Rows[0].SetField("broj_slovima", broj_slovima);

            //mora se postaviti na "" i 1 jer inače javlja error na reportu
            dSFaktura.Tables[0].Rows[0].SetField("valuta", "");
            dSFaktura.Tables[0].Rows[0].SetField("tecaj", 1);
            dSFaktura.Tables[0].Rows[0].SetField("napomena_tvrtka", dSFaktura.Tables[0].Rows[0]["napomena_tvrtka"].ToString().Replace("\n", " ").Replace("\r", " "));

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
            DateTime datum_ = Convert.ToDateTime(dSFaktura.Tables[0].Rows[0]["datum"].ToString());
            reportViewer1.LocalReport.DisplayName = "Otpremnica-" + dSFaktura.Tables[0].Rows[0]["broj_otpremnice"].ToString() + "-" + datum_.ToString("dd-MM-yyyy") + "";
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

            decimal vpc = 0;
            decimal kol = 0;
            decimal porez;
            decimal rabat;
            decimal mpc;
            decimal mpcStavka;
            decimal rabatStavka = 0;
            decimal pdvStavka = 0;
            decimal osnovicaStavka = 0;
            decimal rabatSve = 0;
            decimal sveUkupno = 0;
            decimal pdvUkupno = 0;
            decimal osnovicaUkupno = 0;

            DataTable DT = dSRfakturaStavke.Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                vpc = Math.Round(Convert.ToDecimal(DT.Rows[i]["vpc"].ToString()), 3);
                kol = Math.Round(Convert.ToDecimal(DT.Rows[i]["kolicina"].ToString()), 3);
                porez = Math.Round(Convert.ToDecimal(DT.Rows[i]["porez"].ToString()), 3);
                rabat = Math.Round(Convert.ToDecimal(DT.Rows[i]["rabat"].ToString()), 3);

                mpc = Math.Round(vpc * (1m + porez / 100.0m) - 0.0000001m, 3);

                rabatStavka = vpc * kol * rabat / 100;
                mpcStavka = Math.Round(vpc * kol - rabatStavka, 3);
                pdvStavka = Math.Round(mpcStavka * (porez / 100), 3);
                osnovicaStavka = mpcStavka;

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
                sveUkupno += (mpcStavka + pdvStavka);
            }

            string broj_slovima = "Slovima: " + broj_u_text.PretvoriBrojUTekst(sveUkupno.ToString("0.00"), ',', "kn", "lp").ToString().ToLower();

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
                " 'Datum DVO:' AS naziv_date1," +
                " 'Datum dospijeća:' AS naziv_date2," +
                " partners.adresa AS kupac_adresa," +
                " partners.id_grad AS id_kupac_grad," +
                " partners.id_partner AS sifra_kupac," +
                " partners.napomena AS napomena_tvrtka," +
                " partners.oib_polje AS string5, '' as string4, " +
                " zemlja.zemlja AS kupac_drzava, " +
                //" ziro_racun.ziroracun AS zr," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio," +
                //" ziro_racun.banka AS banka," +
                " '" + broj_slovima + "' AS broj_slovima," +
                " partners.oib AS kupac_oib" +
                " FROM ifb" +
                " LEFT JOIN zacrnjeni_partner as  partners ON partners.id_partner=ifb.odrediste" +
                " LEFT JOIN nacin_placanja ON nacin_placanja.id_placanje=ifb.id_nacin_placanja" +
                " LEFT JOIN zemlja ON zemlja.id_zemlja=partners.id_zemlja" +
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

            //mora se postaviti na "" i 1 jer inače javlja error na reportu
            dSFaktura.Tables[0].Rows[0].SetField("valuta", "");
            dSFaktura.Tables[0].Rows[0].SetField("tecaj", 1);
            dSFaktura.Tables[0].Rows[0].SetField("napomena_tvrtka", dSFaktura.Tables[0].Rows[0]["napomena_tvrtka"].ToString().Replace("\n", " ").Replace("\r", " "));

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
            DateTime datum_ = Convert.ToDateTime(dSFaktura.Tables[0].Rows[0]["datum"].ToString());
            reportViewer1.LocalReport.DisplayName = "IFB-" + dSFaktura.Tables[0].Rows[0]["broj"].ToString() + "-" + datum_.ToString("dd-MM-yyyy") + "";
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

            decimal vpc = 0;
            decimal kol = 0;
            decimal porez;
            decimal rabat;
            decimal mpc;
            decimal mpcStavka;
            decimal rabatStavka = 0;
            decimal pdvStavka = 0;
            decimal osnovicaStavka = 0;

            decimal osnovicaUkupno = 0;
            decimal sveUkupno = 0;
            decimal pdvUkupno = 0;
            decimal rabatSve = 0;

            DataTable DT = dSRfakturaStavke.Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                vpc = Math.Round(Convert.ToDecimal(DT.Rows[i]["vpc"].ToString()), 3);
                kol = Math.Round(Convert.ToDecimal(DT.Rows[i]["kolicina"].ToString()), 3);
                porez = Math.Round(Convert.ToDecimal(DT.Rows[i]["porez"].ToString()), 3);
                rabat = Math.Round(Convert.ToDecimal(DT.Rows[i]["rabat"].ToString()), 3);

                mpc = Math.Round(vpc * (1m + porez / 100.0m) - 0.0000001m, 3);

                rabatStavka = vpc * kol * rabat / 100;
                mpcStavka = Math.Round(vpc * kol - rabatStavka, 3);
                pdvStavka = Math.Round(mpcStavka * (porez / 100), 3);
                osnovicaStavka = mpcStavka;

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
                sveUkupno += (mpcStavka + pdvStavka);
            }

            string broj_slovima = "Slovima: " + broj_u_text.PretvoriBrojUTekst(sveUkupno.ToString("0.00"), ',', "kn", "lp").ToString().ToLower();

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
                " 'RNS' AS string1," +
                " 'OVO NIJE FISKALIZIRAN RAČUN' AS string3," +
                " partners.oib_polje AS string5, '' as string4, " +
                " zemlja.zemlja AS kupac_drzava, " +
                " ziro_racun.ziroracun AS zr," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio," +
                " ziro_racun.banka AS banka," +
                " '" + broj_slovima + "' AS broj_slovima," +
                " partners.oib AS kupac_oib" +
                " FROM radni_nalog_servis" +
                " LEFT JOIN zacrnjeni_partner as  partners ON partners.id_partner=radni_nalog_servis.id_fakturirati" +
                " LEFT JOIN otprema ON otprema.id_otprema=radni_nalog_servis.otprema" +
                " LEFT JOIN nacin_placanja ON nacin_placanja.id_placanje=radni_nalog_servis.id_nacin_placanja" +
                " LEFT JOIN zemlja ON zemlja.id_zemlja=partners.id_zemlja" +
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
            dSFaktura.Tables[0].Rows[0].SetField("napomena_tvrtka", dSFaktura.Tables[0].Rows[0]["napomena_tvrtka"].ToString().Replace("\n", " ").Replace("\r", " "));

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
            DateTime datum_ = Convert.ToDateTime(dSFaktura.Tables[0].Rows[0]["datum"].ToString());
            reportViewer1.LocalReport.DisplayName = "RadniNalog-" + dSFaktura.Tables[0].Rows[0]["broj"].ToString() + "-" + datum_.ToString("dd-MM-yyyy") + "";
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

        private decimal VratiTecaj(string imeTablice, string imeBrojPonude, string broj)
        {
            decimal tecaj;
            string sqlTecaj = "SELECT tecaj" +
                " FROM " + imeTablice + " WHERE " + imeTablice + "." + imeBrojPonude + "='" + broj + "'";
            if (dokumenat == "FAK")
            {
                sqlTecaj += " and " + imeTablice + ".id_ducan = '" + poslovnica + "'";
            }

            DataTable dt = classSQL.select(sqlTecaj, imeTablice).Tables[0];
            if (dt.Rows.Count > 0)
            {
                try
                {
                    tecaj = Convert.ToDecimal(dt.Rows[0][0].ToString());
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

        private void reportViewer1_Print(object sender, ReportPrintEventArgs e)
        {
            if (dokumenat == "RAC")
                OstaleFunkcije.PovecajBrojIspisaRacuna(broj_dokumenta, poslovnica, naplatni, dokumenat);
            else if (dokumenat == "FAK")
                OstaleFunkcije.PovecajBrojIspisaRacuna(broj_dokumenta, poslovnica, naplatni, dokumenat);
        }
    }
}