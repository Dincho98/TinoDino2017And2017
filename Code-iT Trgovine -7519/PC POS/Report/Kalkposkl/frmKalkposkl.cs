using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.Kalkposkl
{
    public partial class frmKalkposkl : Form
    {
        public frmKalkposkl()
        {
            InitializeComponent();
        }

        public string documenat { get; set; }
        public string broj_dokumenta { get; set; }
        public string dokumenat { get; set; }
        public string ImeForme { get; set; }
        public string blagajnik { get; set; }
        public string skladiste { get; set; }
        public string skladiste_odabir { get; set; }
        public string ducan { get; set; }
        public bool premadatumu { get; set; }
        public bool prema_rac { get; set; }
        public int skladiste_brojac { get; set; }
        public Boolean bool1 { get; set; }
        public Boolean bool2 { get; set; } // pomoćni bit
        public int brojac { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }

        public string BrojFakOD { get; set; }
        public string BrojFakDO { get; set; }

        private INIFile ini = new INIFile();

        private void frmKalkposkl_Load(object sender, EventArgs e)
        {
            //Sintetika_po_skladisnim_cijenama_sva_skladista();
            ListaKalkulacije();
            this.Text = ImeForme;
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);

            ReportDataSource nbcStopeDataSource = new ReportDataSource();

            nbcStopeDataSource.Name = "DSnbcStope";
            nbcStopeDataSource.Value = dSstope.Tables[1];
            reportViewer1.LocalReport.DataSources.Add(nbcStopeDataSource);

            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Kalkposkl.Kalkposkl.rdlc";

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

        private DataTable ds = new Dataset.DSRlisteTekst.DTlisteTekstDataTable();

        private void SETizracun(DataTable Kal, string NAZIV_SKLADISTA, string broj_skladista)
        {
            decimal mpc_uk = 0;
            decimal kalk_fak_uk = 0;
            decimal kalk_nab_uk = 0;
            decimal kalk_izn_bez_por_uk = 0;
            decimal kalk_izn_s_por_uk = 0;
            decimal zavisni_uk = 0;
            decimal marza_uk = 0;
            decimal iznos_rabata_nafakt_uk = 0;
            decimal povratna_naknada_ukupno = 0;
            decimal trosakUkupno = 0;
            decimal pdvRoba = 0;
            DataTable DTstavke;

            for (int i = 0; i < Kal.Rows.Count; i++)
            {
                string sqlStavka = string.Format(@"SELECT
                        kalkulacija_stavke.broj,
                        kalkulacija_stavke.vpc,
                        kalkulacija_stavke.porez,
                        kalkulacija_stavke.kolicina,
                        kalkulacija_stavke.id_skladiste,
                        kalkulacija_stavke.id_kalkulacija,
                        kalkulacija_stavke.rabat,
                        kalkulacija_stavke.marza_postotak,
                        kalkulacija_stavke.fak_cijena,
                        kalkulacija_stavke.carina,
                        kalkulacija_stavke.prijevoz,
                        kalkulacija_stavke.posebni_porez,
                        pm.iznos as povratna_naknada
                        FROM kalkulacija_stavke
                        LEFT JOIN (select distinct sifra, iznos from povratna_naknada) pm on kalkulacija_stavke.sifra = pm.sifra
                        WHERE kalkulacija_stavke.id_skladiste = '{0}'
                            AND kalkulacija_stavke.broj = '{1}'
                        ORDER BY CAST(broj as numeric)",
                            Kal.Rows[i]["id_skladiste"].ToString(), Kal.Rows[i]["broj"].ToString());
                //left join (select distinct sifra, iznos from povratna_naknada) pm on kalkulacija_stavke.sifra = pm.sifra
                DTstavke = classSQL.select(sqlStavka, "kalkulacija_stavke").Tables[0];

                decimal Iznos_fakturni_uk = 0;
                decimal Iznos_nabavni_uk = 0;
                decimal Iznos_nabavni = 0;
                decimal fakt_cijena_sa_rab = 0;
                decimal iznos_bez_poreza = 0;
                decimal iznos_sa_porezom = 0;
                decimal zavisni = 0;
                decimal zavisni_temp = 0;
                decimal marza = 0;
                decimal marza_po_stv = 0;
                decimal iznos_rabata_nafakt = 0;
                decimal iznos_rabata_nafakt_temp = 0;
                trosakUkupno += Convert.ToDecimal(Kal.Rows[i]["trosak"].ToString());

                for (int y = 0; y < DTstavke.Rows.Count; y++)
                {
                    decimal vpc = Convert.ToDecimal(DTstavke.Rows[y]["vpc"].ToString());
                    decimal kolicina = Convert.ToDecimal(DTstavke.Rows[y]["kolicina"].ToString());
                    decimal pdv = Convert.ToDecimal(DTstavke.Rows[y]["porez"].ToString());
                    decimal rabat = Convert.ToDecimal(DTstavke.Rows[y]["rabat"].ToString());
                    decimal kol = Convert.ToDecimal(DTstavke.Rows[y]["kolicina"].ToString());
                    decimal marza_post = 0;
                    decimal.TryParse(DTstavke.Rows[y]["marza_postotak"].ToString(), out marza_post);
                    decimal car = Convert.ToDecimal(DTstavke.Rows[y]["carina"].ToString());
                    decimal transport = Convert.ToDecimal(DTstavke.Rows[y]["prijevoz"].ToString());
                    decimal fakt_cijena = Convert.ToDecimal(DTstavke.Rows[y]["fak_cijena"].ToString());
                    decimal povratna_naknada = Convert.ToDecimal(DTstavke.Rows[y]["povratna_naknada"].ToString());
                    if (!Class.Postavke.koristi_povratnu_naknadu) {
                        povratna_naknada = 0;
                    }

                    decimal vpc_s_kol = vpc * kolicina;
                    decimal fak_s_kol = fakt_cijena * kolicina;

                    Iznos_fakturni_uk = Math.Round((fakt_cijena * kol) + Iznos_fakturni_uk, 6);
                    iznos_rabata_nafakt += Math.Round((fakt_cijena * (rabat / 100)) * kol, 6);
                    iznos_rabata_nafakt_temp = Math.Round((fakt_cijena * (rabat / 100)) * kol, 6);
                    iznos_rabata_nafakt_uk += Math.Round(iznos_rabata_nafakt_temp, 6);
                    fakt_cijena_sa_rab = Math.Round(fakt_cijena - (fakt_cijena * (rabat / 100)), 6);
                    Iznos_nabavni_uk = Math.Round(((fakt_cijena_sa_rab)), 2) * kol + Iznos_nabavni_uk;
                    Iznos_nabavni = Math.Round(((fakt_cijena_sa_rab * kol) + transport + car), 6);
                    iznos_bez_poreza = Math.Round((vpc * kol) + iznos_bez_poreza, 6);
                    iznos_sa_porezom = Math.Round(((vpc + (vpc * (pdv / 100))) * kol) + iznos_sa_porezom + (kol * povratna_naknada), 6);
                    povratna_naknada_ukupno += Math.Round((kol * povratna_naknada), 2, MidpointRounding.AwayFromZero);
                    mpc_uk = Math.Round(vpc + mpc_uk, 6);
                    zavisni += Math.Round((car + transport) * kolicina, 6);
                    zavisni_uk += (car + transport) * kolicina;
                    zavisni_temp += (car + transport);
                    marza = Math.Round(Iznos_nabavni * (marza_post / 100), 6);
                    marza_po_stv += marza;
                    marza_uk += marza;

                    decimal iznos = 0;

                    iznos = Math.Round((vpc), 2, MidpointRounding.AwayFromZero) * (pdv / 100);
                    if (kolicina != 0)
                    {
                        DataRow[] stope = (dSstope.Tables[0]).Select(string.Format("stopa='{0}'", pdv.ToString("#0.00")));
                        if (stope.Length == 0)
                        {
                            // MPC
                            DataRow stopa = dSstope.Tables[0].NewRow();

                            stopa["stopa"] = pdv.ToString("#0.00");
                            stopa["osnovica"] = Math.Round(vpc, 2, MidpointRounding.AwayFromZero) * kolicina;
                            stopa["iznos"] = Math.Round(iznos, 4, MidpointRounding.AwayFromZero) * kolicina;
                            stopa["vrsta"] = "roba";

                            dSstope.Tables[0].Rows.Add(stopa);
                        }
                        else
                        {
                            decimal osnovicaOld = 0, iznosOld = 0;
                            decimal.TryParse(stope[0]["osnovica"].ToString(), out osnovicaOld);
                            decimal.TryParse(stope[0]["iznos"].ToString(), out iznosOld);

                            stope[0]["osnovica"] = (osnovicaOld + Math.Round((vpc * kolicina), 6, MidpointRounding.AwayFromZero));
                            stope[0]["iznos"] = (iznosOld + Math.Round((iznos * kolicina), 6, MidpointRounding.AwayFromZero));
                        }

                        // NBC
                        DataRow nbcStopa = dSstope.Tables[1].NewRow();
                        nbcStopa["stopa"] = pdv.ToString("#0.00");
                        nbcStopa["osnovica"] = Math.Round((fakt_cijena_sa_rab * kolicina), 2, MidpointRounding.AwayFromZero);
                        decimal pdvNbc = ((fakt_cijena_sa_rab * kolicina) * (pdv / 100));
                        pdvRoba += pdvNbc;
                        nbcStopa["iznos"] = Math.Round(pdvNbc, 2, MidpointRounding.AwayFromZero);
                        nbcStopa["vrsta"] = "roba";
                        dSstope.Tables[1].Rows.Add(nbcStopa);
                    }
                }

                DataRow trosakStopa = dSstope.Tables[1].NewRow();
                trosakStopa["stopa"] = 25;
                trosakStopa["osnovica"] = Math.Round(zavisni, 2, MidpointRounding.AwayFromZero);
                trosakStopa["iznos"] = Math.Round((zavisni * 0.25m), 2, MidpointRounding.AwayFromZero);
                trosakStopa["vrsta"] = "trosak";
                dSstope.Tables[1].Rows.Add(trosakStopa);

                //DataView dv = dSstope.Tables[0].DefaultView;
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
                //dSstope.Tables[0] = dv.ToTable();

                kalk_fak_uk = Iznos_fakturni_uk + kalk_fak_uk;
                kalk_nab_uk = Iznos_nabavni_uk + kalk_nab_uk;
                kalk_izn_bez_por_uk = iznos_bez_poreza + kalk_izn_bez_por_uk;
                kalk_izn_s_por_uk = iznos_sa_porezom + kalk_izn_s_por_uk;

                if (iznos_sa_porezom != 0)
                {
                    DataRow DTrow = dSRlisteTekst.Tables[0].NewRow();
                    DTrow["string1"] = Kal.Rows[i]["broj"].ToString();
                    DTrow["string2"] = Kal.Rows[i]["id_skladiste"].ToString();
                    DTrow["string3"] = Convert.ToDateTime(Kal.Rows[i]["racun_datum"].ToString()).ToString("dd-MM-yyyy");
                    DTrow["string4"] = 0;
                    DTrow["string5"] = Kal.Rows[i]["id_partner"].ToString() + " " + Kal.Rows[i]["ime_tvrtke"].ToString();
                    DTrow["string9"] = iznos_rabata_nafakt;
                    DTrow["ukupno1"] = Iznos_fakturni_uk.ToString("#0.00");
                    DTrow["ukupno2"] = Iznos_nabavni_uk.ToString("#0.00");
                    DTrow["ukupno3"] = iznos_bez_poreza.ToString("#0.00");
                    DTrow["ukupno4"] = iznos_sa_porezom.ToString("#0.00");
                    DTrow["ukupno5"] = marza_po_stv.ToString("#0.00");
                    DTrow["ukupno6"] = trosakUkupno.ToString("#0.00");

                    dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                }
            }

            DataRow DTrow1 = dSRlisteTekst.Tables[0].NewRow();

            decimal sveukupno = 0;
            decimal zbroj = 0;
            decimal zbroj_ulaz = 0;
            decimal zbroj_izlaz = 0;
            decimal zbroj_marza = 0;
            decimal zbroj_zavisni = 0;
            decimal zbroj_izn_bez_por = 0;
            decimal zbroj_rab = 0;

            DTrow1 = dSRlisteTekst.Tables[0].NewRow();
            DTrow1["string5"] = "UKUPNO :";
            DTrow1["string4"] = "Skladiste " + " " + broj_skladista.ToString();
            DTrow1["string2"] = NAZIV_SKLADISTA.ToString();
            DTrow1["string9"] = iznos_rabata_nafakt_uk;
            DTrow1["ukupno1"] = Convert.ToDecimal(kalk_fak_uk.ToString("#0.00"));
            DTrow1["ukupno2"] = Convert.ToDecimal(kalk_nab_uk.ToString("#0.00"));
            DTrow1["ukupno3"] = Convert.ToDecimal(kalk_izn_bez_por_uk.ToString("#0.00"));
            DTrow1["ukupno4"] = Convert.ToDecimal(kalk_izn_s_por_uk.ToString("#0.00"));
            DTrow1["ukupno5"] = marza_uk.ToString("#0.00");
            DTrow1["ukupno6"] = trosakUkupno.ToString("#0.00");
            dSRlisteTekst.Tables[0].Rows.Add(DTrow1);

            zbroj = kalk_izn_s_por_uk + zbroj;

            zbroj_ulaz = kalk_fak_uk + zbroj_ulaz;
            zbroj_izlaz = kalk_nab_uk;

            zbroj_marza = marza_uk + zbroj_marza;
            zbroj_zavisni = zavisni_uk + zbroj_zavisni;
            zbroj_izn_bez_por = kalk_izn_bez_por_uk + zbroj_izn_bez_por;
            zbroj_rab += iznos_rabata_nafakt_uk;

            sveukupno = zbroj;
            string imereporta = "za prodajne cijene";
            string sqln = " SELECT " +
                " '" + zbroj_izn_bez_por.ToString("0.00") + "' As cijena9 ," +
                " '" + zbroj_ulaz.ToString("0.00") + "' As cijena8 ," +
                " '" + zbroj_izlaz.ToString("0.00") + "' As cijena7 ," +
                " '" + trosakUkupno.ToString("0.00") + "' As cijena6 ," +
                " '" + zbroj_marza.ToString("0.00") + "' As cijena5 ," +
                " '" + sveukupno.ToString("0.00") + "' As cijena1 ," +
                " '" + zbroj_rab.ToString("0.00") + "' As cijena2 ," +
                " '" + trosakUkupno.ToString("0.00") + "' As trosak_bez_pdv ," +
                " '" + (trosakUkupno * 0.25m).ToString("0.00") + "' As trosak_pdv ," +
                " '" + (trosakUkupno * 1.25m).ToString("0.00") + "' As trosak_ukupno ," +
                " '" + pdvRoba.ToString("0.00") + "' As pdv_roba ," +
                " '" + povratna_naknada_ukupno.ToString("0.00") + "' As cijena3 ," +
                " '" + datumOD.AddDays(0).ToString("yyyy-MM-dd") + "' As datum1 ," +
                " '" + datumDO.AddDays(0).ToString("yyyy-MM-dd") + "' As datum2 ," +
                " '" + imereporta + "' As jmj " +
                "";
            classSQL.CeAdatpter(sqln).Fill(dSRliste, "DTliste");
        }

        private void ListaKalkulacije()
        {
            string sqlpodaci = "SELECT " +
                " podaci_tvrtka.ime_tvrtke," +
                " podaci_tvrtka.skraceno_ime," +
                " podaci_tvrtka.oib," +
                " podaci_tvrtka.tel," +
                " podaci_tvrtka.fax," +
                " podaci_tvrtka.mob," +
                " podaci_tvrtka.iban," +
                " podaci_tvrtka.adresa," +
                " podaci_tvrtka.vl," +
                " podaci_tvrtka.poslovnica_adresa," +
                " podaci_tvrtka.poslovnica_grad," +
                " podaci_tvrtka.email," +
                " podaci_tvrtka.zr," +
                " podaci_tvrtka.naziv_fakture," +
                " podaci_tvrtka.text_bottom," +
                " grad.grad + '' + grad.posta AS grad" +
                " FROM podaci_tvrtka" +
                " LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad" +
                "";
            classSQL.CeAdatpter(sqlpodaci).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            frmObracunporeza obrporez = new frmObracunporeza();
            string sql = "SELECT * FROM skladiste ORDER BY id_skladiste ASC";
            DataTable DTskl = classSQL.select(sql, "SKL").Tables[0];

            DataTable DTkalk = new DataTable();

            string skla_kalk = "";

            if (bool1 == true)
            {
                skla_kalk = "";
            }
            else
            {
                skla_kalk = "";
            }
            if (bool1)
            {
                string opcije = "";
                if (prema_rac != true)
                {
                    opcije = "kalkulacija.racun_datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND kalkulacija.racun_datum <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "'";
                }
                else
                {
                    opcije = "kalkulacija.id_kalkulacija >= '" + BrojFakOD + "' AND kalkulacija.id_kalkulacija <= '" + BrojFakDO + "'";
                }
                string sqlHeder = "SELECT " +
                                " kalkulacija.broj, " +
                                " kalkulacija.id_skladiste, " +
                                " kalkulacija.id_kalkulacija, " +
                                " kalkulacija.racun_datum, " +
                                " kalkulacija.trosak, " +
                                " partners.id_partner," +
                                " partners.ime_tvrtke" +
                                " FROM kalkulacija" +
                                " LEFT JOIN partners ON partners.id_partner=kalkulacija.id_partner " +
                                " WHERE " + opcije + " AND kalkulacija.id_skladiste = '" + skladiste_odabir + "' ORDER BY CAST(broj as numeric)";
                //string sql1="SELECT kalkulacija_stavke.vpc, kalkulacija_stavke.id_skladiste, kalkulacija_stavke.rabat, kalkulacija_stavke.kolicina, kalkulacija_stavke.porez FROM kalkulacija_stavke LEFT JOIN kalkulacija ON kalkulacija.broj = kalkulacija_stavke.broj WHERE kalkulacija.datum >= '" + datumOD.ToString("dd-MM-yyyy") + "' AND kalkulacija.datum <= '" + datumDO.ToString("dd-MM-yyyy") + "' AND kalkulacija.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_kalk + " ";
                DTkalk = classSQL.select(sqlHeder, "Kalkulacija").Tables[0];

                SETizracun(DTkalk, skladiste, skladiste_odabir);
            }
            else
            {
                string opcije = "";
                if (prema_rac != true)
                {
                    opcije = "kalkulacija.racun_datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND kalkulacija.racun_datum <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "'";
                }
                else
                {
                    opcije = "kalkulacija.id_kalkulacija >= '" + BrojFakOD + "' AND kalkulacija.id_kalkulacija <= '" + BrojFakDO + "'";
                }
                foreach (DataRow row in DTskl.Rows)
                {
                    string sqlHeder = "SELECT " +
                                    " kalkulacija.broj, " +
                                    " kalkulacija.id_skladiste, " +
                                    " kalkulacija.id_kalkulacija, " +
                                    " kalkulacija.racun_datum, " +
                                    " kalkulacija.trosak, " +
                                    " partners.id_partner," +
                                    " partners.ime_tvrtke" +
                                    " FROM kalkulacija" +
                                    " LEFT JOIN partners ON partners.id_partner=kalkulacija.id_partner " +
                                    " WHERE " + opcije + " AND id_skladiste = '" + row["id_skladiste"].ToString() + "'ORDER BY CAST(broj as numeric)";
                    //string sql1="SELECT kalkulacija_stavke.vpc, kalkulacija_stavke.id_skladiste, kalkulacija_stavke.rabat, kalkulacija_stavke.kolicina, kalkulacija_stavke.porez FROM kalkulacija_stavke LEFT JOIN kalkulacija ON kalkulacija.broj = kalkulacija_stavke.broj WHERE kalkulacija.datum >= '" + datumOD.ToString("dd-MM-yyyy") + "' AND kalkulacija.datum <= '" + datumDO.ToString("dd-MM-yyyy") + "' AND kalkulacija.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_kalk + " ";
                    DTkalk = classSQL.select(sqlHeder, "Kalkulacija").Tables[0];

                    SETizracun(DTkalk, row["skladiste"].ToString(), row["id_skladiste"].ToString());
                }
            }
        }
    }
}