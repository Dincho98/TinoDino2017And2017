using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Report.lzlazniRacuni
{
    public partial class frmListe3 : Form
    {
        public frmListe3()
        {
            InitializeComponent();
        }

        private decimal proizvodacka_cijena_ukp = 0;

        public string documenat { get; set; }
        public string broj_dokumenta { get; set; }
        public string dokumenat { get; set; }
        public string ImeForme { get; set; }
        public string blagajnik { get; set; }
        public string skladiste { get; set; }
        public string ducan { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }
        public int partner { get; set; }

        public bool prema_rac { get; set; }

        public string BrojFakOD { get; set; }
        public string BrojFakDO { get; set; }
        public bool pouzecem { get; internal set; }

        private void frmListe_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            if (documenat == "fakt")
            {
                ListaFakture();
                this.Text = ImeForme;
            }

            if (documenat == "ifb")
            {
                ListaIFB();
                this.Text = ImeForme;
            }

            if (documenat == "mpra")
            {
                ListaMaloProdajniRacun();
                this.Text = ImeForme;
            }

            if (documenat == "otpr")
            {
                ListaOtpremnice();
                this.Text = ImeForme;
            }

            if (documenat == "rac_za_avans")
            {
                ListaRacZaAvans();
                this.Text = ImeForme;
            }

            //if (documenat == "pov_dob")
            //{
            //    ListaPovratnicaDobavljacu();
            //    this.Text = ImeForme;
            //}
        }

        private void ListaRacZaAvans()
        {
            string naziv_fakture = " podaci_tvrtka.naziv_fakture,";
            naziv_fakture = "";
            string sql1 = SqlPodaciTvrtke.VratiSql("", naziv_fakture, "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string kriterij = "";
            if (prema_rac)
            {
                kriterij = string.Format(" WHERE avans_racun.broj_avansa >= {0} AND avans_racun.broj_avansa <= {1} and avans_racun.poslovnica = {2}", BrojFakOD, BrojFakDO, skladiste);
                if (partner > 0)
                {
                    kriterij += string.Format(" AND avans_racun.id_partner = {0}", partner);
                }

                kriterij += " ORDER BY CAST(avans_racun.broj_avansa as numeric) ASC;";
            }
            else
            {
                kriterij = string.Format(" WHERE avans_racun.dat_knj >= '{0}' AND avans_racun.dat_knj <= '{1}' and avans_racun.poslovnica = {2}", datumOD.ToString("yyyy-MM-dd 00:00:00"), datumDO.ToString("yyyy-MM-dd 23:59:59"), skladiste);

                if (partner > 0)
                {
                    kriterij += string.Format(" AND avans_racun.id_partner = {0}", partner);
                }

                kriterij += " ORDER BY avans_racun.dat_knj ASC;";
            }

            string sqlHeder = string.Format(@"SELECT avans_racun.broj_avansa, avans_racun.dat_knj, avans_racun.datum_valute, partners.id_partner, avans_racun.poslovnica, avans_racun.naplatni_uredaj, partners.ime_tvrtke
FROM avans_racun
LEFT JOIN partners ON partners.id_partner = avans_racun.id_partner
{0}", kriterij);

            DataRow DDTrow = dSRlisteTekst.Tables[0].NewRow();
            if (BrojFakDO != "")
            {
                DDTrow["string3"] = "Od računa: " + BrojFakOD.ToString();
                DDTrow["string4"] = "Do računa: " + BrojFakDO.ToString();
            }
            else
            {
                DDTrow["string3"] = "Od datuma: " + datumOD.ToString("dd.MM.yyyy 00:00:00");
                DDTrow["string4"] = "Do datuma: " + datumDO.ToString("dd.MM.yyyy 23:59:59");
            }

            if (documenat == "rac_za_avans")
            {
                string naslov = "Račun za avans";

                if (skladiste != null && skladiste.ToString().Length > 0)
                {
                    naslov += "\nZa poslovnicu " + classSQL.select(string.Format("select ime_ducana from ducan where id_ducan = {0};", skladiste), "ducan").Tables[0].Rows[0][0];
                }

                DDTrow["string5"] = naslov;
            }

            dSRlisteTekst.Tables[0].Rows.Add(DDTrow);

            DataTable DTheader = classSQL.select(sqlHeder, "fakture").Tables[0];

            DataTable DTstavke;

            for (int i = 0; i < DTheader.Rows.Count; i++)
            {
                string sqlStavka = string.Format(@"SELECT avans_racun.osnovica_var as vpc, avans_racun.porez_var as porez, 1 as kolicina, 'NE' as oduzmi, 0 as nbc, 0 as id_skladiste, 0 as rabat
FROM avans_racun
left join porezi on avans_racun.id_pdv = porezi.id_porez
WHERE avans_racun.broj_avansa = {0} AND avans_racun.poslovnica = {1};",
DTheader.Rows[i]["broj_avansa"].ToString(), DTheader.Rows[i]["poslovnica"].ToString());

                DTstavke = classSQL.select(sqlStavka, "faktura_stavke").Tables[0];

                decimal pdv_stavka = 0;
                decimal Usluga_ukupno_stavka = 0;
                decimal Roba_ukupno_stavka = 0;
                decimal roba_stavke = 0;
                decimal usluga_stavke = 0;
                decimal rabat_proracun = 0;
                decimal iznos_neto = 0;
                decimal osnovica_ukupno = 0;
                decimal ukupno = 0;
                decimal vpc1 = 0;
                decimal usl_bez_por = 0;
                decimal roba_bez_por = 0;

                for (int y = 0; y < DTstavke.Rows.Count; y++)
                {
                    decimal vpc = Convert.ToDecimal(DTstavke.Rows[y]["vpc"].ToString());
                    decimal pdv = Convert.ToDecimal(DTstavke.Rows[y]["porez"].ToString());
                    decimal rabat = Convert.ToDecimal(DTstavke.Rows[y]["rabat"].ToString());
                    decimal kol = Convert.ToDecimal(DTstavke.Rows[y]["kolicina"].ToString());
                    vpc1 = vpc;
                    vpc = (pdv + vpc);

                    if (DTstavke.Rows[y]["oduzmi"].ToString() != "DA")
                    {
                        usluga_stavke = (vpc1 * kol);
                        Usluga_ukupno_stavka = ((vpc * kol) - ((vpc * (rabat / 100)) * kol)) + Usluga_ukupno_stavka;
                        usl_bez_por = vpc1;
                    }
                    else
                    {
                        roba_stavke = (vpc1 * kol);
                        Roba_ukupno_stavka = ((vpc * kol) - ((vpc * (rabat / 100)) * kol)) + Roba_ukupno_stavka;
                        roba_bez_por = Roba_ukupno_stavka / (1 + (pdv / 100));
                    }

                    rabat_proracun = 0;
                    iznos_neto = (vpc1 * kol) + iznos_neto;
                    osnovica_ukupno = (usluga_stavke + Roba_ukupno_stavka);
                    ukupno = Usluga_ukupno_stavka + Roba_ukupno_stavka;
                    pdv_stavka = pdv;
                }

                //DTrow["datum1"] = Convert.ToDateTime(DTheader.Rows[i]["datedvo"].ToString()).ToString("dd-MM-yyyy");

                DataRow DTrow = dSRliste.Tables[0].NewRow();
                DTrow["datum1"] = Convert.ToDateTime(DTheader.Rows[i]["dat_knj"].ToString()).ToString("dd-MM-yyyy");
                DTrow["datum2"] = Convert.ToDateTime(DTheader.Rows[i]["datum_valute"].ToString()).ToString("dd-MM-yyyy");
                DTrow["sifra"] = DTheader.Rows[i]["broj_avansa"].ToString();
                DTrow["naziv"] = DTheader.Rows[i]["id_partner"].ToString() + " " + DTheader.Rows[i]["ime_tvrtke"].ToString();
                DTrow["cijena1"] = Usluga_ukupno_stavka.ToString("#0.00");
                DTrow["cijena2"] = Roba_ukupno_stavka.ToString("#0.00");
                DTrow["cijena3"] = iznos_neto.ToString("#0.00");
                DTrow["cijena4"] = usl_bez_por.ToString("#0.00");
                DTrow["cijena5"] = rabat_proracun.ToString("#0.00");
                DTrow["cijena6"] = osnovica_ukupno.ToString("#0.00");
                DTrow["cijena7"] = pdv_stavka.ToString("#0.00");
                DTrow["cijena8"] = ukupno.ToString("#0.00");
                DTrow["cijena9"] = roba_bez_por.ToString("#0.00");
                //MessageBox.Show(DTheader.Rows[i]["broj_fakture"].ToString());
                dSRliste.Tables[0].Rows.Add(DTrow);
            }
            ReportParameter p3 = new ReportParameter("nabavna_ukupno", 0.ToString());
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p3 });
            SetReportParameters("Usluga bez pdv-a");
        }

        private void SetReportParameters(string naslov)
        {
            ReportParameter p1 = new ReportParameter("nabavna_ili_usluga", naslov);
            ReportParameter p2 = new ReportParameter("doc", documenat);
            ReportParameter p4 = new ReportParameter("proizvodjacka_cijena_ukupno", proizvodacka_cijena_ukp.ToString());
            ReportParameter p5 = new ReportParameter("prikazi_proizvodacku_cijenu", Class.Postavke.proizvodnjaFakturaNbc.ToString());

            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p4, p5 });
            reportViewer1.RefreshReport();
        }

        private void ListaFakture()
        {
            string naziv_fakture = " podaci_tvrtka.naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql("", naziv_fakture, "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string kriterij = "";
            if (prema_rac)
            {
                kriterij = " WHERE CAST(fakture.broj_fakture as numeric) >= '" + BrojFakOD + "' AND CAST(fakture.broj_fakture as numeric) <= '" + BrojFakDO + "'";

                if (skladiste != null && skladiste.ToString().Length > 0 && Util.Korisno.oibTvrtke == Class.Postavke.OIB_PC1)
                {
                    kriterij = " WHERE CAST(fakture.broj_fakture as numeric) >= '" + BrojFakOD + "' AND CAST(fakture.broj_fakture as numeric) <= '" + BrojFakDO + "' and fakture.id_ducan = '" + skladiste + "'";
                }

                if (partner > 0)
                {
                    kriterij += " AND fakture.id_fakturirati = '" + partner + "'";
                }

                if (pouzecem)
                {
                    kriterij += " AND fakture.pouzece = true";
                }
                kriterij += " ORDER BY CAST(fakture.broj_fakture as numeric) ASC;";
            }
            else
            {
                kriterij = " WHERE fakture.datedvo >= '" + datumOD.ToString("yyyy-MM-dd 00:00:00") + "' AND fakture.datedvo <= '" + datumDO.ToString("yyyy-MM-dd 23:59:59") + "'";

                if (skladiste != null && skladiste.ToString().Length > 0 && Util.Korisno.oibTvrtke == Class.Postavke.OIB_PC1)
                {
                    kriterij = " WHERE fakture.datedvo >= '" + datumOD.ToString("yyyy-MM-dd 00:00:00") + "' AND fakture.datedvo <= '" + datumDO.ToString("yyyy-MM-dd 23:59:59") + "' and fakture.id_ducan = '" + skladiste + "'";
                }

                if (partner > 0)
                {
                    kriterij += " AND fakture.id_fakturirati = '" + partner + "'";
                }
                if (pouzecem)
                {
                    kriterij += " AND fakture.pouzece = true";
                }

                kriterij += " AND fakture.id_kasa = " + PCPOS.Class.Postavke.naplatni_uredaj_faktura;

                kriterij += @"GROUP BY fakture.broj_fakture, fakture.datedvo, fakture.datum_valute, partners.id_partner, fakture.id_ducan, fakture.id_kasa, ducan.ime_ducana
                              ORDER BY fakture.datedvo ASC;";
            }

            string sqlHeder = $@"SELECT CAST(fakture.broj_fakture as numeric), 
                                    fakture.datedvo, 
                                    fakture.datum_valute, 
                                    partners.id_partner, 
                                    fakture.id_ducan, 
                                    fakture.id_kasa, 
                                    ducan.ime_ducana, 
                                    partners.ime_tvrtke, 
                                    partners.oib 
                                FROM fakture
                                LEFT JOIN ducan ON ducan.id_ducan = fakture.id_ducan
                                LEFT JOIN partners ON partners.id_partner = fakture.id_odrediste  
                                LEFT JOIN faktura_stavke ON faktura_stavke.broj_fakture=fakture.broj_fakture and faktura_stavke.id_ducan = fakture.id_ducan 
                                {kriterij}";
            DataRow DDTrow = dSRlisteTekst.Tables[0].NewRow();
            if (BrojFakDO != "")
            {
                DDTrow["string3"] = "Od računa: " + BrojFakOD.ToString();
                DDTrow["string4"] = "Do računa: " + BrojFakDO.ToString();
            }
            else
            {
                DDTrow["string3"] = "Od datuma: " + datumOD.ToString("dd.MM.yyyy 00:00:00");
                DDTrow["string4"] = "Do datuma: " + datumDO.ToString("dd.MM.yyyy 23:59:59");
            }

            if (documenat == "fakt")
            {
                string naslov = "Izlazne Fakture";

                if (skladiste != null && skladiste.ToString().Length > 0 && Util.Korisno.oibTvrtke == "")
                {
                    naslov += "\nZa poslovnicu " + classSQL.select(string.Format("select ime_ducana from ducan where id_ducan = {0};", skladiste), "ducan").Tables[0].Rows[0][0];
                }
                DDTrow["string5"] = naslov;
            }

            dSRlisteTekst.Tables[0].Rows.Add(DDTrow);

            DataTable DTheader = classSQL.select(sqlHeder, "fakture").Tables[0];

            DataTable DTstavke;

            decimal nbc_ukp = 0;


            for (int i = 0; i < DTheader.Rows.Count; i++)
            {
                string sqlStavka = "SELECT " +
                    " faktura_stavke.vpc," +
                    " faktura_stavke.porez," +
                    " faktura_stavke.kolicina," +
                    " faktura_stavke.oduzmi," +
                    " faktura_stavke.nbc," +
                    " faktura_stavke.proizvodacka_cijena," +
                    " faktura_stavke.id_skladiste," +
                    " faktura_stavke.rabat " +
                    " FROM faktura_stavke" +
                    " WHERE CAST(faktura_stavke.broj_fakture as numeric) ='" + DTheader.Rows[i]["broj_fakture"].ToString() + "' " +
                    " AND faktura_stavke.id_ducan='" + DTheader.Rows[i]["id_ducan"].ToString() + "' AND faktura_stavke.id_kasa='" + DTheader.Rows[i]["id_kasa"].ToString() + "'";

                DTstavke = classSQL.select(sqlStavka, "faktura_stavke").Tables[0];

                decimal pdvUkupno = 0;
                decimal Usluga_ukupno_stavka = 0;
                decimal Roba_ukupno_stavka = 0;
                decimal roba_stavke = 0;
                decimal usluga_stavke = 0;
                decimal rabat_proracun = 0;
                decimal iznos_neto = 0;
                decimal osnovica_ukupno = 0;
                decimal ukupno = 0;
                decimal vpc1 = 0;
                decimal usl_bez_por = 0;
                decimal roba_bez_por = 0;
                decimal proizvodacka_cijena = 0;
                decimal proizvodacka_cijena_faktura_ukp = 0;

                for (int y = 0; y < DTstavke.Rows.Count; y++)
                {
                    decimal nbc = Convert.ToDecimal(DTstavke.Rows[y]["nbc"].ToString());
                    decimal vpc = Convert.ToDecimal(DTstavke.Rows[y]["vpc"].ToString());
                    decimal pdv = Convert.ToDecimal(DTstavke.Rows[y]["porez"].ToString());
                    decimal rabat = Convert.ToDecimal(DTstavke.Rows[y]["rabat"].ToString());
                    decimal kol = Convert.ToDecimal(DTstavke.Rows[y]["kolicina"].ToString());
                    decimal pc = Convert.ToDecimal(DTstavke.Rows[y]["proizvodacka_cijena"].ToString());
                    proizvodacka_cijena = pc * kol;
                    proizvodacka_cijena_faktura_ukp += proizvodacka_cijena;
                    proizvodacka_cijena_ukp += proizvodacka_cijena;
                    vpc1 = vpc;
                    vpc = ((vpc * pdv / 100) + vpc);

                    nbc_ukp += (nbc * kol);

                    if (DTstavke.Rows[y]["oduzmi"].ToString() != "DA")
                    {
                        usluga_stavke = (vpc1 * kol);
                        Usluga_ukupno_stavka = ((vpc * kol) - ((vpc * (rabat / 100)) * kol)) + Usluga_ukupno_stavka;
                        //usl_bez_por = Usluga_ukupno_stavka / (1 + (pdv / 100));
                        usl_bez_por += usluga_stavke;
                    }
                    else
                    {
                        roba_stavke = (vpc1 * kol);
                        Roba_ukupno_stavka = ((vpc * kol) - ((vpc * (rabat / 100)) * kol)) + Roba_ukupno_stavka;
                        //roba_bez_por = Roba_ukupno_stavka / (1 + (pdv / 100));
                        roba_bez_por += roba_stavke;
                    }

                    rabat_proracun = ((vpc * (rabat / 100)) * kol) + rabat_proracun;
                    iznos_neto = (vpc1 * kol) + iznos_neto;
                    osnovica_ukupno = (Usluga_ukupno_stavka + Roba_ukupno_stavka) / (1 + (pdv / 100));
                    ukupno = Usluga_ukupno_stavka + Roba_ukupno_stavka;
                    pdvUkupno += ukupno * (((pdv * 100) / (pdv + 100)) / 100);

                    decimal iznos = Math.Round((vpc1 * (pdv / 100)), 6, MidpointRounding.AwayFromZero);
                    if (kol != 0)
                    {
                        //DataRow[] dataROW = DTpdv.Select("stopa = '" + stopa.ToString("0.00") + "'");
                        DataRow[] stope = (dSstope.Tables[0]).Select(string.Format("stopa='{0}'", pdv.ToString("#0.00")));
                        if (stope.Length == 0)
                        {
                            DataRow stopa = dSstope.Tables[0].NewRow();

                            stopa["stopa"] = pdv.ToString("#0.00");
                            stopa["osnovica"] = Math.Round((vpc1 * kol), 6, MidpointRounding.AwayFromZero);
                            stopa["iznos"] = Math.Round((iznos * kol), 6, MidpointRounding.AwayFromZero);

                            dSstope.Tables[0].Rows.Add(stopa);
                        }
                        else
                        {
                            decimal osnovicaOld = 0, iznosOld = 0;
                            decimal.TryParse(stope[0]["osnovica"].ToString(), out osnovicaOld);
                            decimal.TryParse(stope[0]["iznos"].ToString(), out iznosOld);

                            stope[0]["osnovica"] = (osnovicaOld + Math.Round((vpc1 * kol), 6, MidpointRounding.AwayFromZero));
                            stope[0]["iznos"] = (iznosOld + Math.Round((iznos * kol), 6, MidpointRounding.AwayFromZero));
                        }
                    }

                    DataTable dt2 = dSstope.Tables[0].Clone();
                    dt2.Columns["stopa"].DataType = Type.GetType("System.Decimal");

                    foreach (DataRow dr in dSstope.Tables[0].Rows)
                    {
                        dt2.ImportRow(dr);
                    }

                    dt2.AcceptChanges();
                    DataView dv = dt2.DefaultView;
                    dv.Sort = "stopa ASC";
                    //dSstope = null;
                    dSstope.Tables[0].Rows.Clear();
                    //int stopa_index = 0;
                    foreach (DataRow item in dv.ToTable().Rows)
                    {
                        DataRow stopa = dSstope.Tables[0].NewRow();

                        stopa["stopa"] = item["stopa"].ToString();
                        stopa["osnovica"] = item["osnovica"];
                        stopa["iznos"] = item["iznos"];

                        dSstope.Tables[0].Rows.Add(stopa);
                        //stopa_index++;
                    }

                }

                //DTrow["datum1"] = Convert.ToDateTime(DTheader.Rows[i]["datedvo"].ToString()).ToString("dd-MM-yyyy");

                DataRow DTrow = dSRliste.Tables[0].NewRow();
                DTrow["datum1"] = Convert.ToDateTime(DTheader.Rows[i]["datedvo"].ToString()).ToString("dd-MM-yyyy");
                DTrow["datum2"] = Convert.ToDateTime(DTheader.Rows[i]["datum_valute"].ToString()).ToString("dd-MM-yyyy");
                DTrow["sifra"] = DTheader.Rows[i]["broj_fakture"].ToString() + "/" + DTheader.Rows[i]["ime_ducana"].ToString() + "/" + DTheader.Rows[i]["id_kasa"].ToString();
                DTrow["naziv"] = DTheader.Rows[i]["id_partner"].ToString() + " " + DTheader.Rows[i]["ime_tvrtke"].ToString();
                DTrow["cijena1"] = Usluga_ukupno_stavka.ToString("#0.00");
                DTrow["cijena2"] = Roba_ukupno_stavka.ToString("#0.00");
                DTrow["cijena3"] = iznos_neto.ToString("#0.00");
                DTrow["cijena4"] = usl_bez_por.ToString("#0.00");
                DTrow["cijena5"] = rabat_proracun.ToString("#0.00");
                DTrow["cijena6"] = osnovica_ukupno.ToString("#0.00");
                DTrow["cijena7"] = pdvUkupno.ToString("#0.00");
                DTrow["cijena8"] = ukupno.ToString("#0.00");
                DTrow["cijena9"] = roba_bez_por.ToString("#0.00");
                DTrow["cijena11"] = proizvodacka_cijena_faktura_ukp.ToString("#0.00");
                DTrow["oib"] = DTheader.Rows[i]["oib"].ToString();
                //DTrow["ime_ducana"] = DTheader.Rows[i]["ime_ducana"].ToString();
                // DTrow["ime_blagajne"] = DTheader.Rows[i]["ime_blagajne"].ToString();
                //MessageBox.Show(DTheader.Rows[i]["broj_fakture"].ToString());
                dSRliste.Tables[0].Rows.Add(DTrow);
            }
            ReportParameter p3 = new ReportParameter("nabavna_ukupno", nbc_ukp.ToString());

            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p3 });
            SetReportParameters("Usluga bez pdv-a");
        }

        private void ListaIFB()
        {
            string naziv_fakture = " podaci_tvrtka.naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql("", naziv_fakture, "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            //string Broj_fakture = "";
            //DateTime Datum_dokumenta;
            //DateTime Datum_valute;
            //string Sifra_i_naziv_partnera = "";

            string kriterij = "";
            if (prema_rac)
            {
                kriterij = " WHERE ifb.broj >= '" + BrojFakOD + "' AND ifb.broj <= '" + BrojFakDO + "' ORDER BY ifb.broj ASC";
            }
            else
            {
                kriterij = " WHERE ifb.datum_dvo >= '" + datumOD + "' AND ifb.datum_dvo <= '" + datumDO + "' ORDER BY ifb.datum_dvo ASC";
            }

            string sqlHeder = "SELECT " +
            " ifb.broj, " +
            " ifb.datum_dvo, " +
            " ifb.datum_valute, " +
            " partners.id_partner," +
            " partners.ime_tvrtke" +
            " FROM ifb" +
            " LEFT JOIN partners ON partners.id_partner=ifb.odrediste " +
            kriterij +
            "";
            DataRow DDTrow = dSRlisteTekst.Tables[0].NewRow();
            if (BrojFakDO != "")
            {
                DDTrow["string3"] = "Od računa :" + " " + BrojFakOD.ToString();
                DDTrow["string4"] = "Do računa :" + " " + BrojFakDO.ToString();
            }
            else
            {
                DDTrow["string3"] = "Od datuma :" + " " + datumOD.ToString();
                DDTrow["string4"] = "Do datuma :" + " " + datumDO.ToString();
            }

            if (documenat == "ifb")
            {
                DDTrow["string5"] = "Izlazne Fakture";
            }

            dSRlisteTekst.Tables[0].Rows.Add(DDTrow);

            DataTable DTheader = classSQL.select(sqlHeder, "fakture").Tables[0];

            DataTable DTstavke;

            decimal nbc_ukp = 0;

            for (int i = 0; i < DTheader.Rows.Count; i++)
            {
                string sqlStavka = "SELECT " +
                    " ifb_stavke.vpc," +
                    " ifb_stavke.mpc," +
                    " ifb_stavke.porez," +
                    " ifb_stavke.kolicina," +
                    " ifb_stavke.rabat " +
                    " FROM ifb_stavke" +
                    //" LEFT JOIN fakture ON fakture.id_odrediste=Roba.id_partner"+
                    //" LEFT JOIN roba ON faktura_stavke.sifra=roba.sifra"+
                    " WHERE ifb_stavke.broj='" + DTheader.Rows[i]["broj"].ToString() + "'";

                DTstavke = classSQL.select(sqlStavka, "faktura_stavke").Tables[0];

                decimal pdv_stavka = 0;
                decimal Usluga_ukupno_stavka = 0;
                decimal Roba_ukupno_stavka = 0;
                decimal rabat_proracun = 0;
                decimal iznos_neto = 0;
                decimal osnovica_ukupno = 0;
                decimal ukupno = 0;
                decimal vpc1 = 0;
                decimal usl_bez_por = 0;
                decimal roba_bez_por = 0;
                decimal mpc_rabat = 0;
                decimal mpc_rabat_uk = 0;

                for (int y = 0; y < DTstavke.Rows.Count; y++)
                {
                    decimal vpc = Convert.ToDecimal(DTstavke.Rows[y]["vpc"].ToString());
                    decimal mpc = Convert.ToDecimal(DTstavke.Rows[y]["mpc"].ToString());
                    decimal pdv = Convert.ToDecimal(DTstavke.Rows[y]["porez"].ToString());
                    decimal rabat = Convert.ToDecimal(DTstavke.Rows[y]["rabat"].ToString());
                    decimal kol = Convert.ToDecimal(DTstavke.Rows[y]["kolicina"].ToString());
                    vpc1 = vpc;
                    vpc = mpc;
                    mpc_rabat = (mpc - (mpc * rabat / 100)) * kol;
                    mpc_rabat_uk = mpc_rabat + mpc_rabat_uk;
                    rabat_proracun = ((vpc * (rabat / 100)) * kol) + rabat_proracun;
                    iznos_neto = (vpc1 * kol) + iznos_neto;
                    osnovica_ukupno = mpc_rabat_uk / (1 + (pdv / 100));
                    ukupno = mpc_rabat_uk;
                    pdv_stavka = ukupno * (((pdv * 100) / (pdv + 100)) / 100);
                }

                //DTrow["datum1"] = Convert.ToDateTime(DTheader.Rows[i]["datedvo"].ToString()).ToString("dd-MM-yyyy");

                DataRow DTrow = dSRliste.Tables[0].NewRow();
                DTrow["datum1"] = Convert.ToDateTime(DTheader.Rows[i]["datum_dvo"].ToString()).ToString("dd-MM-yyyy");
                DTrow["datum2"] = Convert.ToDateTime(DTheader.Rows[i]["datum_valute"].ToString()).ToString("dd-MM-yyyy");
                DTrow["sifra"] = DTheader.Rows[i]["broj"].ToString();
                DTrow["naziv"] = DTheader.Rows[i]["id_partner"].ToString() + " " + DTheader.Rows[i]["ime_tvrtke"].ToString();
                DTrow["cijena1"] = Usluga_ukupno_stavka.ToString("#0.00");
                DTrow["cijena2"] = Roba_ukupno_stavka.ToString("#0.00");
                DTrow["cijena3"] = iznos_neto.ToString("#0.00");
                DTrow["cijena4"] = usl_bez_por.ToString("#0.00");
                DTrow["cijena5"] = rabat_proracun.ToString("#0.00");
                DTrow["cijena6"] = osnovica_ukupno.ToString("#0.00");
                DTrow["cijena7"] = pdv_stavka.ToString("#0.00");
                DTrow["cijena8"] = ukupno.ToString("#0.00");
                DTrow["cijena9"] = roba_bez_por.ToString("#0.00");
                //MessageBox.Show(DTheader.Rows[i]["broj_fakture"].ToString());
                dSRliste.Tables[0].Rows.Add(DTrow);
            }
            ReportParameter p3 = new ReportParameter("nabavna_ukupno", nbc_ukp.ToString());
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p3 });
            SetReportParameters("Usluga bez pdv-a");
        }

        private void ListaMaloProdajniRacun()
        {
            string naziv_fakture = " podaci_tvrtka.naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql("", naziv_fakture, "");

            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string kriterij = "";
            if (prema_rac)
            {
                kriterij = " WHERE CAST(Racuni.broj_racuna as numeric) >= '" + BrojFakOD + "' AND CAST(Racuni.broj_racuna as numeric) <= '" + BrojFakDO + "' ORDER BY CAST(Racuni.broj_racuna as numeric) ASC";
            }
            else
            {
                kriterij = string.Format(@"WHERE cast(Racuni.datum_racuna as date) >= '{0:yyyy-MM-dd}' AND cast(Racuni.datum_racuna as date) <= '{1:yyyy-MM-dd}'
ORDER BY Racuni.datum_racuna ASC;",
                    datumOD, datumDO);
            }
            string sqlHeder = string.Format(@"SELECT
racuni.broj_racuna,
racuni.id_ducan,
racuni.id_kasa,
racuni.datum_racuna,
partners.id_partner,
partners.ime_tvrtke
FROM Racuni
LEFT JOIN partners ON partners.id_partner=Racuni.id_kupac
{0}", kriterij);

            DataRow DDTrow = dSRlisteTekst.Tables[0].NewRow();
            if (BrojFakDO != "")
            {
                DDTrow["string3"] = "Od računa :" + " " + BrojFakOD.ToString();
                DDTrow["string4"] = "Do računa :" + " " + BrojFakDO.ToString();
            }
            else
            {
                DDTrow["string3"] = "Od datuma :" + " " + datumOD.ToString("dd.MM.yyyy.");
                DDTrow["string4"] = "Do datuma :" + " " + datumDO.ToString("dd.MM.yyyy.");
            }

            if (documenat == "mpra")
            {
                DDTrow["string5"] = "Maloprodajni Računi";
            }

            dSRlisteTekst.Tables[0].Rows.Add(DDTrow);

            DataTable DTheader = classSQL.select(sqlHeder, "maloprodaja").Tables[0];

            DataTable DTstavke;

            decimal nbc_ukp = 0;

            for (int i = 0; i < DTheader.Rows.Count; i++)
            {
                string sqlStavka = string.Format(@"SELECT
racun_stavke.mpc,
racun_stavke.vpc,
racun_stavke.porez,
racun_stavke.kolicina,
racun_stavke.broj_racuna,
racun_stavke.nbc,
racun_stavke.id_skladiste,
racun_stavke.rabat,
racun_stavke.porez_potrosnja,
roba.oduzmi
FROM racun_stavke
LEFT JOIN roba ON roba.sifra = racun_stavke.sifra_robe
WHERE racun_stavke.broj_racuna = '{0}' and racun_stavke.id_ducan = '{1}' and racun_stavke.id_kasa = '{2}';",
    DTheader.Rows[i]["broj_racuna"].ToString(), DTheader.Rows[i]["id_ducan"].ToString(), DTheader.Rows[i]["id_kasa"].ToString());

                DTstavke = classSQL.select(sqlStavka, "racun_stavke").Tables[0];

                decimal Usluga_ukupno_stavka = 0;
                decimal Roba_ukupno_stavka = 0;
                decimal roba_vpc = 0;
                decimal usluga_vpc = 0;
                decimal pdv_stavka = 0;

                decimal Rabat_ukupno_stavke = 0;
                decimal iznos_neto = 0;
                decimal osnovica_ukupno = 0;
                decimal ukupno = 0;
                decimal roba_stavka = 0;
                decimal usl_stavka = 0;
                decimal rabat_proracun = 0;
                decimal vpc1 = 0;
                decimal usl_bez_por = 0;
                decimal roba_bez_por = 0;

                for (int y = 0; y < DTstavke.Rows.Count; y++)
                {
                    //MessageBox.Show(DTstavke.Rows[y]["vpc"].ToString());
                    decimal nbc = Convert.ToDecimal(DTstavke.Rows[y]["nbc"].ToString());
                    decimal mpc = Convert.ToDecimal(DTstavke.Rows[y]["mpc"].ToString());
                    decimal vpc = Convert.ToDecimal(DTstavke.Rows[y]["vpc"].ToString());
                    decimal pdv = Convert.ToDecimal(DTstavke.Rows[y]["porez"].ToString());
                    decimal rabat = Convert.ToDecimal(DTstavke.Rows[y]["rabat"].ToString());
                    decimal kol = Convert.ToDecimal(DTstavke.Rows[y]["kolicina"].ToString());

                    vpc1 = vpc;
                    mpc = ((vpc * pdv / 100) + vpc);

                    if (DTstavke.Rows[y]["oduzmi"].ToString() != "DA")
                    {
                        usl_stavka = (vpc1 * kol);
                        Usluga_ukupno_stavka = ((mpc * kol) - ((mpc * (rabat / 100)) * kol)) + Usluga_ukupno_stavka;
                        //usl_bez_por = Usluga_ukupno_stavka / (1 + (pdv / 100));
                        usl_bez_por += usl_stavka;
                    }
                    else
                    {
                        roba_stavka = (vpc1 * kol);
                        Roba_ukupno_stavka = ((mpc * kol) - ((mpc * (rabat / 100)) * kol)) + Roba_ukupno_stavka;
                        //roba_bez_por = Roba_ukupno_stavka / (1 + (pdv / 100));
                        roba_bez_por += roba_stavka;
                    }
                    nbc_ukp += (nbc * kol);
                    //rabat_proracun += ((vpc1 * (rabat / 100)) * kol);
                    rabat_proracun += ((mpc * (rabat / 100)) * kol);
                    iznos_neto = (vpc1 * kol) + iznos_neto;
                    osnovica_ukupno = (Usluga_ukupno_stavka + Roba_ukupno_stavka) / (1 + (pdv / 100));
                    osnovica_ukupno = iznos_neto - rabat_proracun;
                    ukupno = Usluga_ukupno_stavka + Roba_ukupno_stavka;
                    //pdv_stavka = ukupno * (((pdv * 100) / (pdv + 100)) / 100);
                    pdv_stavka = (ukupno - osnovica_ukupno);

                    //iznos_neto = (Math.Round(roba_vpc, 2) + Math.Round(usluga_vpc, 2)) + Math.Round(iznos_neto, 2);
                    //Rabat_ukupno_stavke = (iznos_neto - (Usluga_ukupno_stavka + Roba_ukupno_stavka));
                    //osnovica_ukupno = (iznos_neto - Rabat_ukupno_stavke);
                    //pdv_stavka = (osnovica_ukupno * (pdv / 100));
                    //ukupno = osnovica_ukupno + pdv_stavka;
                    decimal iznos = Math.Round((vpc1 * (pdv / 100)), 6, MidpointRounding.AwayFromZero);
                    if (kol != 0)
                    {
                        //DataRow[] dataROW = DTpdv.Select("stopa = '" + stopa.ToString("0.00") + "'");
                        DataRow[] stope = (dSstope.Tables[0]).Select(string.Format("stopa='{0}'", pdv.ToString("#0.00")));
                        if (stope.Length == 0)
                        {
                            DataRow stopa = dSstope.Tables[0].NewRow();

                            stopa["stopa"] = pdv.ToString("#0.00");
                            stopa["osnovica"] = Math.Round((vpc1 * kol), 6, MidpointRounding.AwayFromZero);
                            stopa["iznos"] = Math.Round((iznos * kol), 6, MidpointRounding.AwayFromZero);

                            dSstope.Tables[0].Rows.Add(stopa);
                        }
                        else
                        {
                            decimal osnovicaOld = 0, iznosOld = 0;
                            decimal.TryParse(stope[0]["osnovica"].ToString(), out osnovicaOld);
                            decimal.TryParse(stope[0]["iznos"].ToString(), out iznosOld);

                            stope[0]["osnovica"] = (osnovicaOld + Math.Round((vpc1 * kol), 6, MidpointRounding.AwayFromZero));
                            stope[0]["iznos"] = (iznosOld + Math.Round((iznos * kol), 6, MidpointRounding.AwayFromZero));
                        }
                    }
                }

                DataTable dt2 = dSstope.Tables[0].Clone();
                dt2.Columns["stopa"].DataType = Type.GetType("System.Decimal");

                foreach (DataRow dr in dSstope.Tables[0].Rows)
                {
                    dt2.ImportRow(dr);
                }
                dt2.AcceptChanges();
                DataView dv = dt2.DefaultView;
                dv.Sort = "stopa ASC";
                //dSstope = null;
                dSstope.Tables[0].Rows.Clear();
                //int stopa_index = 0;
                foreach (DataRow item in dv.ToTable().Rows)
                {
                    DataRow stopa = dSstope.Tables[0].NewRow();

                    stopa["stopa"] = item["stopa"].ToString();
                    stopa["osnovica"] = item["osnovica"];
                    stopa["iznos"] = item["iznos"];

                    dSstope.Tables[0].Rows.Add(stopa);
                    //stopa_index++;
                }

                DataRow DTrow = dSRliste.Tables[0].NewRow();
                DTrow["datum1"] = Convert.ToDateTime(DTheader.Rows[i]["datum_racuna"].ToString()).ToString("dd-MM-yyyy");
                DTrow["datum2"] = Convert.ToDateTime(DTheader.Rows[i]["datum_racuna"].ToString()).ToString("dd-MM-yyyy");
                DTrow["sifra"] = DTheader.Rows[i]["broj_racuna"].ToString();
                DTrow["naziv"] = DTheader.Rows[i]["id_partner"].ToString() + " " + DTheader.Rows[i]["ime_tvrtke"].ToString();
                DTrow["cijena1"] = Usluga_ukupno_stavka.ToString("#0.00");
                DTrow["cijena2"] = Roba_ukupno_stavka.ToString("#0.00");
                DTrow["cijena3"] = iznos_neto.ToString("#0.00");
                DTrow["cijena4"] = usl_bez_por.ToString("#0.00");
                DTrow["cijena5"] = rabat_proracun.ToString("#0.00");
                DTrow["cijena6"] = osnovica_ukupno.ToString("#0.00");
                DTrow["cijena7"] = pdv_stavka.ToString("#0.00");
                DTrow["cijena8"] = ukupno.ToString("#0.00");
                DTrow["cijena9"] = roba_bez_por.ToString("#0.00");
                //MessageBox.Show(DTheader.Rows[i]["broj_fakture"].ToString());
                dSRliste.Tables[0].Rows.Add(DTrow);
            }
            ReportParameter p3 = new ReportParameter("nabavna_ukupno", nbc_ukp.ToString());
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p3 });
            SetReportParameters("Usluga bez pdv-a");
        }

        private void ListaOtpremnice()
        {
            string naziv_fakture = " podaci_tvrtka.naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql("", naziv_fakture, "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            //string Broj_fakture = "";
            //DateTime Datum_dokumenta;
            //DateTime Datum_valute;
            //string Sifra_i_naziv_partnera = "";
            string kriterij = "";
            string partnerCondition = "";
            if (partner > 0)
                partnerCondition = $"otpremnice.osoba_partner = '{partner}' AND";
            if (prema_rac)
            {
                kriterij = $" WHERE {partnerCondition} otpremnice.broj_otpremnice >= '" + BrojFakOD + "' AND otpremnice.broj_otpremnice <= '" + BrojFakDO + "' ORDER BY otpremnice.broj_otpremnice ASC";
            }
            else
            {
                kriterij = $" WHERE {partnerCondition} otpremnice.datum >= '" + datumOD + "' AND otpremnice.datum <= '" + datumDO + "' ORDER BY otpremnice.datum ASC";
            }
            string sqlHeder = "SELECT " +
            " otpremnice.broj_otpremnice, " +
            " otpremnice.datum, " +
            " otpremnice.id_skladiste, " +
            " partners.id_partner," +
            " partners.ime_tvrtke" +
            " FROM otpremnice" +
            " LEFT JOIN partners ON partners.id_partner=otpremnice.osoba_partner " +
            kriterij +
            "";
            DataRow DDTrow = dSRlisteTekst.Tables[0].NewRow();

            if (BrojFakDO != "")
            {
                DDTrow["string3"] = "Od računa :" + " " + BrojFakOD.ToString();
                DDTrow["string4"] = "Do računa :" + " " + BrojFakDO.ToString();
            }
            else
            {
                DDTrow["string3"] = "Od datuma :" + " " + datumOD.ToString();
                DDTrow["string4"] = "Do datuma :" + " " + datumDO.ToString();
            }
            if (documenat == "otpr")
            {
                DDTrow["string5"] = "Otpremnice";
            }

            dSRlisteTekst.Tables[0].Rows.Add(DDTrow);

            DataTable DTheader = classSQL.select(sqlHeder, "otpremnice").Tables[0];

            DataTable DTstavke;

            for (int i = 0; i < DTheader.Rows.Count; i++)
            {
                //MessageBox.Show(DTheader.Rows[i]["broj_racuna"].ToString());
                string sqlStavka = "SELECT " +
                    " otpremnica_stavke.vpc," +
                    " otpremnica_stavke.porez," +
                    " otpremnica_stavke.kolicina," +
                    " otpremnica_stavke.broj_otpremnice," +
                    " otpremnica_stavke.nbc," +
                    " roba.proizvodacka_cijena," +
                    " otpremnica_stavke.id_skladiste," +
                    " otpremnica_stavke.rabat, " +
                    " otpremnica_stavke.porez_potrosnja, " +
                    " otpremnica_stavke.oduzmi " +
                    " FROM otpremnica_stavke" +
                    " LEFT JOIN roba ON roba.sifra = otpremnica_stavke.sifra_robe " +
                    " WHERE otpremnica_stavke.broj_otpremnice = '" + DTheader.Rows[i]["broj_otpremnice"].ToString() + "'  AND otpremnica_stavke.id_skladiste='" + DTheader.Rows[i]["id_skladiste"].ToString() + "'";

                DTstavke = classSQL.select(sqlStavka, "racun_stavke").Tables[0];

                decimal pdv_stavka = 0;
                decimal Usluga_ukupno_stavka = 0;
                decimal Roba_ukupno_stavka = 0;
                decimal roba_stavke = 0;
                decimal usluga_stavke = 0;
                decimal rabat_proracun = 0;
                decimal iznos_neto = 0;
                decimal nabavna_cijena = 0;
                decimal ukupna_nabavna_cijena = 0;
                decimal osnovica_ukupno = 0;
                decimal ukupno = 0;
                decimal vpc1 = 0;
                decimal usl_bez_por = 0;
                decimal roba_bez_por = 0;
                decimal ukupnoProizvodackaCijena = 0;

                for (int y = 0; y < DTstavke.Rows.Count; y++)
                {
                    decimal vpc = Convert.ToDecimal(DTstavke.Rows[y]["vpc"].ToString());
                    decimal pdv = Convert.ToDecimal(DTstavke.Rows[y]["porez"].ToString());
                    decimal rabat = Convert.ToDecimal(DTstavke.Rows[y]["rabat"].ToString());
                    decimal pc = Convert.ToDecimal(DTstavke.Rows[y]["proizvodacka_cijena"].ToString());

                    decimal.TryParse(DTstavke.Rows[y]["nbc"].ToString(), out nabavna_cijena);

                    decimal kol = Convert.ToDecimal(DTstavke.Rows[y]["kolicina"].ToString());
                    vpc1 = vpc;
                    vpc = ((vpc * pdv / 100) + vpc);

                    if (DTstavke.Rows[y]["oduzmi"].ToString() != "DA")
                    {
                        usluga_stavke = (vpc1 * kol);
                        Usluga_ukupno_stavka = ((vpc * kol) - ((vpc * (rabat / 100)) * kol)) + Usluga_ukupno_stavka;
                        //usl_bez_por = Usluga_ukupno_stavka / (1 + (pdv/100));
                    }
                    else
                    {
                        roba_stavke = (vpc1 * kol);
                        Roba_ukupno_stavka = ((vpc * kol) - ((vpc * (rabat / 100)) * kol)) + Roba_ukupno_stavka;
                        roba_bez_por = Roba_ukupno_stavka / (1 + (pdv / 100));
                    }

                    ukupnoProizvodackaCijena += (pc * kol);
                    ukupna_nabavna_cijena = (nabavna_cijena * kol) + ukupna_nabavna_cijena;
                    rabat_proracun = ((vpc * (rabat / 100)) * kol) + rabat_proracun;
                    iznos_neto = (vpc1 * kol) + iznos_neto;
                    osnovica_ukupno = (Usluga_ukupno_stavka + Roba_ukupno_stavka) / (1 + (pdv / 100));
                    ukupno = Usluga_ukupno_stavka + Roba_ukupno_stavka;
                    pdv_stavka = ukupno * (((pdv * 100) / (pdv + 100)) / 100);
                }

                proizvodacka_cijena_ukp += ukupnoProizvodackaCijena;

                DataRow DTrow = dSRliste.Tables[0].NewRow();
                DTrow["datum1"] = Convert.ToDateTime(DTheader.Rows[i]["datum"].ToString()).ToString("dd-MM-yyyy");
                DTrow["datum2"] = Convert.ToDateTime(DTheader.Rows[i]["datum"].ToString()).ToString("dd-MM-yyyy");
                DTrow["sifra"] = DTheader.Rows[i]["broj_otpremnice"].ToString();
                DTrow["naziv"] = DTheader.Rows[i]["id_partner"].ToString() + " " + DTheader.Rows[i]["ime_tvrtke"].ToString();
                DTrow["cijena1"] = Usluga_ukupno_stavka.ToString("#0.00");
                DTrow["cijena2"] = Roba_ukupno_stavka.ToString("#0.00");
                DTrow["cijena3"] = iznos_neto.ToString("#0.00");
                //DTrow["cijena4"] = usl_bez_por.ToString("#0.00");
                DTrow["cijena4"] = ukupna_nabavna_cijena.ToString("#0.00");
                DTrow["cijena5"] = rabat_proracun.ToString("#0.00");
                DTrow["cijena6"] = osnovica_ukupno.ToString("#0.00");
                DTrow["cijena7"] = pdv_stavka.ToString("#0.00");
                DTrow["cijena8"] = ukupno.ToString("#0.00");
                DTrow["cijena9"] = roba_bez_por.ToString("#0.00");
                DTrow["cijena11"] = ukupnoProizvodackaCijena.ToString("#0.00");
                //MessageBox.Show(DTheader.Rows[i]["broj_fakture"].ToString());
                dSRliste.Tables[0].Rows.Add(DTrow);
            }

            ReportParameter p1 = new ReportParameter("nabavna_ili_usluga", "Nabavne cijene");
            ReportParameter p2 = new ReportParameter("doc", documenat);
            ReportParameter p3 = new ReportParameter("nabavna_ukupno", 0.ToString());
            ReportParameter p4 = new ReportParameter("proizvodjacka_cijena_ukupno", proizvodacka_cijena_ukp.ToString());
            ReportParameter p5 = new ReportParameter("prikazi_proizvodacku_cijenu", "True");

            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5 });
            reportViewer1.RefreshReport();
            //SetReportParameters("Nabavne cijene");
        }

        private void ListaPovratnicaDobavljacu()
        {
            string naziv_fakture = " podaci_tvrtka.naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql("", naziv_fakture, "");

            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            //string Broj_fakture = "";
            //DateTime Datum_dokumenta;
            //DateTime Datum_valute;
            //string Sifra_i_naziv_partnera = "";
            string kriterij = "";
            if (prema_rac)
            {
                kriterij = " WHERE povrat_robe.broj >= '" + BrojFakOD + "' AND povrat_robe.broj <= '" + BrojFakDO + "' ORDER BY povrat_robe.broj ASC";
            }
            else
            {
                kriterij = " WHERE povrat_robe.datum >= '" + datumOD + "' AND povrat_robe.datum <= '" + datumDO + "' ORDER BY povrat_robe.datum ASC";
            }
            string sqlHeder = "SELECT " +
            " povrat_robe.broj, " +
            " povrat_robe.datum, " +
            " partners.id_partner," +
            " partners.ime_tvrtke" +
            " FROM povrat_robe" +
            " LEFT JOIN partners ON partners.id_partner=povrat_robe.id_partner " +
            kriterij +
            "";

            DataTable DTheader = classSQL.select(sqlHeder, "povrat_robe").Tables[0];

            DataRow DDTrow = dSRlisteTekst.Tables[0].NewRow();

            int u = DTheader.Rows.Count;
            if (BrojFakDO != "")
            {
                DDTrow["string3"] = "Od povratnice :" + " " + BrojFakOD.ToString();
                DDTrow["string4"] = "Do povratnice :" + " " + BrojFakDO.ToString();
            }
            else
            {
                DDTrow["string3"] = "Od datuma :" + " " + datumOD.ToString();
                DDTrow["string4"] = "Do datuma :" + " " + datumDO.ToString();
            }

            if (documenat == "mpra")
            {
                DDTrow["string5"] = "Maloprodajni Računi";
            }
            if (documenat == "pov_dob")
            {
                DDTrow["string5"] = "Povratnice Dobavljaču";
            }

            dSRlisteTekst.Tables[0].Rows.Add(DDTrow);

            DataTable DTstavke;

            decimal nbc_ukp = 0;

            for (int i = 0; i < DTheader.Rows.Count; i++)
            {
                //MessageBox.Show(DTheader.Rows[i]["broj_racuna"].ToString());
                string sqlStavka = "SELECT " +
                    " povrat_robe_stavke.mpc," +
                    " povrat_robe_stavke.vpc," +
                    " povrat_robe_stavke.pdv," +
                    " povrat_robe_stavke.kolicina," +
                    " povrat_robe_stavke.broj," +
                    " povrat_robe_stavke.nbc," +
                    " povrat_robe_stavke.rabat, " +
                    " roba.oduzmi " +
                    " FROM povrat_robe_stavke" +
                    " LEFT JOIN roba ON roba.sifra = povrat_robe_stavke.sifra " +
                    " WHERE povrat_robe_stavke.broj='" + DTheader.Rows[i]["broj"].ToString() + "'";

                DTstavke = classSQL.select(sqlStavka, "povrat_robe_stavke").Tables[0];

                decimal Usluga_ukupno_stavka = 0;
                decimal Roba_ukupno_stavka = 0;
                decimal roba_vpc = 0;
                decimal usluga_vpc = 0;
                decimal pdv_stavka = 0;

                decimal Rabat_ukupno_stavke = 0;
                decimal iznos_neto = 0;
                decimal osnovica_ukupno = 0;
                decimal ukupno = 0;
                decimal roba_stavka = 0;
                decimal usl_stavka = 0;
                decimal rabat_proracun = 0;
                decimal vpc1 = 0;
                decimal usl_bez_por = 0;
                decimal roba_bez_por = 0;
                for (int y = 0; y < DTstavke.Rows.Count; y++)
                {
                    //MessageBox.Show(DTstavke.Rows[y]["vpc"].ToString());
                    decimal nbc = Convert.ToDecimal(DTstavke.Rows[y]["nbc"].ToString());
                    decimal vpc = Convert.ToDecimal(DTstavke.Rows[y]["vpc"].ToString());
                    decimal pdv = Convert.ToDecimal(DTstavke.Rows[y]["pdv"].ToString());
                    decimal rabat = Convert.ToDecimal(DTstavke.Rows[y]["rabat"].ToString());
                    decimal kol = Convert.ToDecimal(DTstavke.Rows[y]["kolicina"].ToString());

                    vpc1 = vpc;
                    vpc = ((vpc * pdv / 100) + vpc);

                    nbc_ukp += (nbc * kol);

                    if (DTstavke.Rows[y]["oduzmi"].ToString() != "DA")
                    {
                        usl_stavka = (vpc1 * kol);
                        Usluga_ukupno_stavka = ((vpc * kol) - ((vpc * (rabat / 100)) * kol)) + Usluga_ukupno_stavka;
                        //usl_bez_por = Usluga_ukupno_stavka / (1 + (pdv / 100));
                        usl_bez_por += usl_stavka;
                    }
                    else
                    {
                        roba_stavka = (vpc1 * kol);
                        Roba_ukupno_stavka = ((vpc * kol) - ((vpc * (rabat / 100)) * kol)) + Roba_ukupno_stavka;
                        //roba_bez_por = Roba_ukupno_stavka / (1 + (pdv / 100));
                        roba_bez_por += roba_stavka;
                    }

                    rabat_proracun = ((vpc * (rabat / 100)) * kol) + rabat_proracun;
                    iznos_neto = (vpc1 * kol) + iznos_neto;
                    osnovica_ukupno = (Usluga_ukupno_stavka + Roba_ukupno_stavka) / (1 + (pdv / 100));
                    ukupno = Usluga_ukupno_stavka + Roba_ukupno_stavka;
                    pdv_stavka = ukupno * (((pdv * 100) / (pdv + 100)) / 100);

                    //iznos_neto = (Math.Round(roba_vpc, 2) + Math.Round(usluga_vpc, 2)) + Math.Round(iznos_neto, 2);
                    //Rabat_ukupno_stavke = (iznos_neto - (Usluga_ukupno_stavka + Roba_ukupno_stavka));
                    //osnovica_ukupno = (iznos_neto - Rabat_ukupno_stavke);
                    //pdv_stavka = (osnovica_ukupno * (pdv / 100));
                    //ukupno = osnovica_ukupno + pdv_stavka;
                }

                DataRow DTrow = dSRliste.Tables[0].NewRow();
                DTrow["datum1"] = Convert.ToDateTime(DTheader.Rows[i]["datum"].ToString()).ToString("dd-MM-yyyy");
                DTrow["datum2"] = Convert.ToDateTime(DTheader.Rows[i]["datum"].ToString()).ToString("dd-MM-yyyy");
                DTrow["sifra"] = DTheader.Rows[i]["broj"].ToString();
                DTrow["naziv"] = DTheader.Rows[i]["id_partner"].ToString() + " " + DTheader.Rows[i]["ime_tvrtke"].ToString();
                DTrow["cijena1"] = Usluga_ukupno_stavka.ToString("#0.00");
                DTrow["cijena2"] = Roba_ukupno_stavka.ToString("#0.00");
                DTrow["cijena3"] = iznos_neto.ToString("#0.00");
                DTrow["cijena4"] = usl_bez_por.ToString("#0.00");
                DTrow["cijena5"] = rabat_proracun.ToString("#0.00");
                DTrow["cijena6"] = osnovica_ukupno.ToString("#0.00");
                DTrow["cijena7"] = pdv_stavka.ToString("#0.00");
                DTrow["cijena8"] = ukupno.ToString("#0.00");
                DTrow["cijena9"] = roba_bez_por.ToString("#0.00");
                //MessageBox.Show(DTheader.Rows[i]["broj_fakture"].ToString());
                dSRliste.Tables[0].Rows.Add(DTrow);
            }
            ReportParameter p3 = new ReportParameter("nabavna_ukupno", nbc_ukp.ToString());
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p3 });
            SetReportParameters("Usluga bez pdv-a");
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
        }
    }
}