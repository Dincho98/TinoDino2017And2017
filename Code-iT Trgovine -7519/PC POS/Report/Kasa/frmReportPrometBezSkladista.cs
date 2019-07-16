using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Report.Kasa
{
    public partial class frmReportPrometBezSkladista : Form
    {
        public frmReportPrometBezSkladista()
        {
            InitializeComponent();
        }

        public string artikl { get; set; }
        public string godina { get; set; }
        public string broj_dokumenta { get; set; }
        public string dokumenat { get; set; }
        public string ImeForme { get; set; }
        public string blagajnik { get; set; }
        public string ducan { get; set; }
        public bool samoPorezi { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }
        private static DataTable DTpdv = new DataTable();

        private void frmReportPrometBezSkladista_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

            string blag, duc, art;
            blag = blagajnik != null ? " AND racuni.id_blagajnik='" + blagajnik + "'" : "";
            duc = ducan != null ? " AND racuni.id_ducan='" + ducan + "'" : "";
            art = artikl != null ? " AND racun_stavke.sifra_robe='" + artikl + "'" : "";
            string sqlRacuni = "select COUNT(*) FROM racun_stavke " +
                " LEFT JOIN racuni ON racun_stavke.broj_racuna=racuni.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa" +
                " LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe" +
                " WHERE racuni.datum_racuna>'" + datumOD + "'" +
                " AND racuni.datum_racuna<'" + datumDO + "'" +
                blag + duc + art;

            DataTable DTrac = classSQL.select(sqlRacuni, "racun_stavke").Tables[0];

            if (DTrac.Rows.Count > 0)
            {
                if (DTrac.Rows[0][0].ToString() != "0")
                {
                    PodaciTvrtke();

                    if (!samoPorezi) SetDS();
                    else
                    {
                        //trba dodati datume ako se ispisuju samo porezi (datumi se moraju upisati u dataset dSRacuniPromet)
                        dSRacuniPromet.Tables[0].Rows.Add();
                        dSRacuniPromet.Tables[0].Rows[0].SetField("datod", datumOD);
                        dSRacuniPromet.Tables[0].Rows[0].SetField("datdo", datumDO);
                    }

                    IspisPoreza();

                    this.reportViewer1.RefreshReport();
                }
                else
                {
                    MessageBox.Show("Nema računa na zadane datume!", "Upozorenje!");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Nema računa na zadane datume!", "Upozorenje!");
                this.Close();
            }
        }

        private void PodaciTvrtke()
        {
            string naziv_fakture = " podaci_tvrtka.naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql("", naziv_fakture, "");

            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            ReportParameter rds = artikl != null ? new ReportParameter("artikl", artikl) : new ReportParameter("artikl", "");
            this.reportViewer1.LocalReport.SetParameters(rds);
            this.reportViewer1.LocalReport.Refresh();
        }

        private void SetDS()
        {
            string duc, blag, art;

            string sql_roba = "SELECT " +
                " roba_prodaja.sifra,roba.jm,roba.naziv,roba_prodaja.vpc,grupa.grupa," +
                " roba_prodaja.kolicina,roba_prodaja.porez_potrosnja,roba_prodaja.porez " +
                " FROM roba_prodaja LEFT JOIN roba ON roba.sifra=roba_prodaja.sifra " +
                " LEFT JOIN grupa ON grupa.id_grupa=roba.id_grupa ORDER BY grupa.id_grupa";
            DataTable DTsvaRoba = classSQL.select(sql_roba, "roba_prodaja").Tables[0];

            blag = blagajnik != null ? " AND racuni.id_blagajnik='" + blagajnik + "'" : "";
            duc = ducan != null ? " AND racuni.id_ducan='" + ducan + "'" : "";
            art = artikl != null ? " AND racun_stavke.sifra_robe='" + artikl + "'" : "";
            //string sqlRacuni = "SELECT SUM(CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC)) AS [kolicina]," +
            //    " '" + datumOD.ToString("yyyy-MM-dd") + "' AS datod," +
            //    " '" + datumDO.ToString("yyyy-MM-dd") + "' AS datdo," +
            //    " racun_stavke.sifra_robe AS [sifra], racun_stavke.mpc," +
            //    " racun_stavke.vpc, roba.naziv FROM racun_stavke " +
            //    " LEFT JOIN racuni ON racun_stavke.broj_racuna=racuni.broj_racuna" +
            //    " LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe" +
            //    " WHERE racuni.datum_racuna>'" + datumOD.ToString("yyyy-MM-dd H:mm:ss") + "'" +
            //    " AND racuni.datum_racuna<'" + datumDO.ToString("yyyy-MM-dd H:mm:ss") + "'" +
            //    blag + duc + art +
            //    " GROUP BY roba.naziv,racun_stavke.sifra_robe, racun_stavke.mpc, racun_stavke.vpc";
            string sqlRacuni = "SELECT SUM(CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC)) AS [kolicina]," +
                " '" + datumOD.ToString("yyyy-MM-dd") + "' AS datod," +
                " '" + datumDO.ToString("yyyy-MM-dd") + "' AS datdo," +
                " racun_stavke.sifra_robe AS [sifra], racun_stavke.mpc, racun_stavke.nbc, racun_stavke.rabat," +
                " Round(CAST(racun_stavke.mpc AS numeric), 2) * (1 - CAST(REPLACE(racun_stavke.rabat,',','.') AS NUMERIC) / 100) AS mpcRabat," +
                " Round(racun_stavke.vpc, 2) AS vpc, roba.naziv FROM racun_stavke " +
                " LEFT JOIN racuni ON racun_stavke.broj_racuna=racuni.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa" +
                " LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe" +
                " WHERE racuni.datum_racuna>'" + datumOD + "'" +
                " AND racuni.datum_racuna<'" + datumDO + "'" +
                blag + duc + art +
                " GROUP BY roba.naziv,racun_stavke.sifra_robe, racun_stavke.rabat, racun_stavke.nbc, racun_stavke.mpc, Round(racun_stavke.vpc, 2)";

            //zamijenjeno je sa round(vpc, 2) jer inače negde grupira po npr. 6.6667 a negde 6.67 jer je drugačije upisano!!!

            DataTable DTrac = classSQL.select(sqlRacuni, "racun_stavke").Tables[0];

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sqlRacuni).Fill(dSRacuniPromet, "DTRRacuniPromet");
            }
            else
            {
                classSQL.NpgAdatpter(sqlRacuni).Fill(dSRacuniPromet, "DTRRacuniPromet");
            }
        }

        private void IspisPoreza()
        {
            string duc, blag, art;

            blag = blagajnik != null ? " AND racuni.id_blagajnik='" + blagajnik + "'" : "";
            duc = ducan != null ? " AND racuni.id_ducan='" + ducan + "'" : "";
            art = artikl != null ? " AND racun_stavke.sifra_robe='" + artikl + "'" : "";

            string[] dodatniUvjeti = new string[3];
            dodatniUvjeti[0] = blag;
            dodatniUvjeti[1] = duc;
            dodatniUvjeti[2] = art;

            string Gotovina, Kartice, Virman, Ukupno;
            decimal UG = 0;
            decimal UK = 0;
            decimal UV = 0;

            DTpdv.Clear();
            if (DTpdv.Columns["stopa"] == null)
            {
                DTpdv.Columns.Add("stopa");
                DTpdv.Columns.Add("iznos");
                DTpdv.Columns.Add("vrsta");
            }

            //sređuje poreze za (k=1 && k=0) && (k=0 && k=1)
            //sređuje poreze za (k=0 && k=0) ne radi ništa i javlja grešku
            SrediPoreze(ref UG, ref UK, ref UV, dodatniUvjeti);

            //sređuje poreze za (k=1 && k=1)
            SrediPoreze1(ref UG, ref UK, ref UV, dodatniUvjeti);

            //sortiraj po vrsti, stopi
            DataRow[] foundRows = DTpdv.Select("", "vrsta ASC, stopa DESC");

            Gotovina = UG != 0 ? Convert.ToDouble(UG).ToString("#0.00") : "0,00";
            Kartice = UK != 0 ? Convert.ToDouble(UK).ToString("#0.00") : "0,00";
            Virman = UV != 0 ? Convert.ToDouble(UV).ToString("#0.00") : "0,00";
            Ukupno = (UG + UK + UV) != 0 ? Convert.ToDouble(UG + UK + UV).ToString("#0.00") : "0,00";

            List<ReportParameter> list = new List<ReportParameter>();
            list.Add(VratiListuPoreza(foundRows, Gotovina, Kartice, Virman, Ukupno));
            list.Add(VratiMpcMinusRabat());
            list.Add(VratiMpc());
            list.Add(VratiVpc());
            list.Add(VratiNbc());
            list.Add(VratiRabat());
            IEnumerable<ReportParameter> parameters = list;

            this.reportViewer1.LocalReport.SetParameters(parameters);
            this.reportViewer1.LocalReport.Refresh();
        }

        private void SrediPoreze(ref decimal UG, ref decimal UK, ref decimal UV, string[] blagDucArt)
        {
            string sqlUkupno = "SELECT gotovina, kartice, porez, ukupno_virman, " +
                " SUM(CAST(REPLACE(kolicina,',','.') AS NUMERIC)*Round(cast(mpc as numeric), 2)*(1-CAST(REPLACE(rabat,',','.') AS NUMERIC)/100)) as kolicinaPutaMpc " +
                " FROM (" +
                " SELECT DISTINCT ON (racun_stavke.id_stavka,racun_stavke.porez) * from racuni,racun_stavke " +
                " WHERE racuni.broj_racuna=racun_stavke.broj_racuna " +
                " AND racuni.datum_racuna>'" + datumOD + "'" +
                " AND racuni.datum_racuna<'" + datumDO + "'" +
                " AND (racuni.gotovina <> '1' OR racuni.kartice <> '1')" +
                blagDucArt[0] + blagDucArt[1] + blagDucArt[2] +
                " ORDER BY racun_stavke.id_stavka,racun_stavke.porez " +
                ") sq " +
                " GROUP BY gotovina, kartice, porez, ukupno_virman order by gotovina, kartice, porez, ukupno_virman";
            DataTable DT = classSQL.select(sqlUkupno, "racuni").Tables[0];

            decimal porezOsnovica, virmanOsnovica, porez, iznos, virman;

            foreach (DataRow r in DT.Rows)
            {
                if (r["gotovina"].ToString() == "1" && r["kartice"].ToString() == "0")
                {
                    porez = Convert.ToDecimal(r["porez"].ToString());
                    virman = r["ukupno_virman"].ToString() != "" ? Convert.ToDecimal(r["ukupno_virman"].ToString()) : 0;
                    //if (virman != 0)
                    //{
                    //    virman = virman;
                    //}
                    iznos = Convert.ToDecimal(r["kolicinaPutaMpc"].ToString()) - virman;
                    porezOsnovica = iznos / (1 + porez / 100);
                    virmanOsnovica = virman / (1 + porez / 100);

                    UG += iznos;
                    dodajPDV("gotovina", porez, porezOsnovica);
                    UV += virman;
                    dodajPDV("virman", porez, virmanOsnovica);
                }
                else if (r["gotovina"].ToString() == "0" && r["kartice"].ToString() == "1")
                {
                    porez = Convert.ToDecimal(r["porez"]);
                    virman = r["ukupno_virman"].ToString() != "" ? Convert.ToDecimal(r["ukupno_virman"].ToString()) : 0;
                    if (virman != 0)
                    {
                        virman = virman;
                    }
                    iznos = Convert.ToDecimal(r["kolicinaPutaMpc"].ToString()) - virman;
                    porezOsnovica = iznos / (1 + porez / 100);
                    virmanOsnovica = virman / (1 + porez / 100);

                    UK += iznos;
                    dodajPDV("kartica", porez, porezOsnovica);
                    UV += virman;
                    dodajPDV("virman", porez, virmanOsnovica);
                }
                else if (r["gotovina"].ToString() == "1" && r["kartice"].ToString() == "1")
                {
                    //-----------------
                    //to je riješeno u drugoj metodi
                    //-----------------
                }
                else
                {
                    //MessageBox.Show("Greška! Račun nije plaćen niti karticom niti gotovinom!" + Environment.NewLine +
                    //    "Gotovina=" + r["gotovina"].ToString() + Environment.NewLine +
                    //    "Kartice=" + r["kartice"].ToString() + Environment.NewLine +
                    //    "Javite se kontakt službi za korisnike.");
                }
            }
        }

        private void SrediPoreze1(ref decimal UG, ref decimal UK, ref decimal UV, string[] blagDucArt)
        {
            string sqlUkupno = "SELECT ukupno_gotovina, ukupno_kartice, ukupno_virman, porez," +
                " SUM(CAST(REPLACE(kolicina,',','.') AS NUMERIC)*Round(cast(mpc as numeric), 2)*(1-CAST(REPLACE(rabat,',','.') AS NUMERIC)/100)) as kolicinaPutaMpc " +
                " FROM (" +
                " SELECT DISTINCT ON (racun_stavke.id_stavka,racun_stavke.porez) * from racuni,racun_stavke " +
                " WHERE racuni.broj_racuna=racun_stavke.broj_racuna " +
                " AND racuni.datum_racuna>'" + datumOD + "'" +
                " AND racuni.datum_racuna<'" + datumDO + "'" +
                " AND (racuni.gotovina = '1' AND racuni.kartice = '1')" +
                blagDucArt[0] + blagDucArt[1] + blagDucArt[2] +
                " ORDER BY racun_stavke.id_stavka,racun_stavke.porez " +
                ") sq " +
                " GROUP BY ukupno_gotovina, ukupno_kartice, ukupno_virman, porez";

            DataTable DT = classSQL.select(sqlUkupno, "racuni").Tables[0];

            decimal porez, iznos;
            decimal porezOsnovicaG, porezOsnovicaK, porezOsnovicaV;
            decimal postotakKartice, postotakGotovina, postotakVirman;
            decimal stavkaGotovina, stavkaKartice, stavkaVirman;

            foreach (DataRow r in DT.Rows)
            {
                stavkaGotovina = r["ukupno_gotovina"].ToString() != "" ? Convert.ToDecimal(r["ukupno_gotovina"].ToString()) : 0;
                stavkaKartice = r["ukupno_kartice"].ToString() != "" ? Convert.ToDecimal(r["ukupno_kartice"].ToString()) : 0;
                stavkaVirman = r["ukupno_virman"].ToString() != "" ? Convert.ToDecimal(r["ukupno_virman"].ToString()) : 0;

                postotakGotovina = stavkaGotovina / (stavkaGotovina + stavkaKartice + stavkaVirman);
                postotakKartice = stavkaKartice / (stavkaGotovina + stavkaKartice + stavkaVirman);
                postotakVirman = stavkaVirman / (stavkaGotovina + stavkaKartice + stavkaVirman);

                porez = Convert.ToDecimal(r["porez"].ToString());
                iznos = Convert.ToDecimal(r["kolicinaPutaMpc"].ToString());//ovaj iznos mora biti = r["ukupno_gotovina"] + r["ukupno_kartice"] !!!

                if (iznos != stavkaGotovina + stavkaKartice + stavkaVirman) ;//MessageBox.Show("Greška!");

                porezOsnovicaG = iznos / (1 + porez / 100) * postotakGotovina;
                porezOsnovicaK = iznos / (1 + porez / 100) * postotakKartice;
                porezOsnovicaV = iznos / (1 + porez / 100) * postotakVirman;
                //stavkaGotovina *= postotakGotovina;
                //stavkaKartice *= postotakKartice;

                UG += porezOsnovicaG * (1 + porez / 100);
                dodajPDV("gotovina", porez, porezOsnovicaG);
                UK += porezOsnovicaK * (1 + porez / 100);
                dodajPDV("kartica", porez, porezOsnovicaK);
                UV += porezOsnovicaV * (1 + porez / 100);
                dodajPDV("virman", porez, porezOsnovicaV);
            }
        }

        private ReportParameter VratiListuPoreza(DataRow[] foundRows, string Gotovina, string Kartice, string Virman, string Ukupno)
        {
            bool ispisG = false, ispisK = false, ispisV = false;

            StavkeZaIspisUkupno aaa = new StavkeZaIspisUkupno();

            foreach (DataRow r in foundRows)
            {
                if (Gotovina == "0,00") continue;

                if (r["vrsta"].ToString() == "gotovina")
                {
                    if (!ispisG)
                    {
                        aaa.listaZaIspis.Add(new StavkeZaIspisUkupno.obj { vrstaIspisa = 1, text = "NOVČANICE:", iznos = Gotovina });
                        ispisG = true;
                    }
                    if (Convert.ToDouble(r["iznos"].ToString()) != 0)
                    {
                        aaa.listaZaIspis.Add(new StavkeZaIspisUkupno.obj
                        {
                            vrstaIspisa = 2,
                            text = Convert.ToDouble(r["stopa"].ToString()).ToString("#0.00"),
                            iznos = Convert.ToDouble(r["iznos"].ToString()).ToString("#0.00")
                        });
                    }
                }
                else if (r["vrsta"].ToString() == "kartica")
                {
                    if (Kartice == "0,00") continue;

                    if (!ispisK)
                    {
                        aaa.listaZaIspis.Add(new StavkeZaIspisUkupno.obj { vrstaIspisa = 1, text = "KARTICE:", iznos = Kartice });
                        ispisK = true;
                    }
                    if (Convert.ToDouble(r["iznos"].ToString()) != 0)
                    {
                        aaa.listaZaIspis.Add(new StavkeZaIspisUkupno.obj
                        {
                            vrstaIspisa = 2,
                            text = Convert.ToDouble(r["stopa"].ToString()).ToString("#0.00"),
                            iznos = Convert.ToDouble(r["iznos"].ToString()).ToString("#0.00")
                        });
                    }
                }
                else if (r["vrsta"].ToString() == "virman")
                {
                    if (Virman == "0,00") continue;

                    if (!ispisV)
                    {
                        aaa.listaZaIspis.Add(new StavkeZaIspisUkupno.obj { vrstaIspisa = 1, text = "TRANSAKCIJSKI RAČUN:", iznos = Virman });
                        ispisV = true;
                    }
                    if (Convert.ToDouble(r["iznos"].ToString()) != 0)
                    {
                        aaa.listaZaIspis.Add(new StavkeZaIspisUkupno.obj
                        {
                            vrstaIspisa = 2,
                            text = Convert.ToDouble(r["stopa"].ToString()).ToString("#0.00"),
                            iznos = Convert.ToDouble(r["iznos"].ToString()).ToString("#0.00")
                        });
                    }
                }
            }

            aaa.listaZaIspis.Add(new StavkeZaIspisUkupno.obj { vrstaIspisa = 3, text = "SVE UKUPNO:", iznos = Ukupno });

            ReportParameter rds = new ReportParameter("listaPoreza", aaa.PrintajListu(aaa.listaZaIspis, 30, 10, 5, 5).ToString());

            return rds;
        }

        private ReportParameter VratiMpc()
        {
            string sql = "SELECT round(SUM(CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC) * " +
                "Round(cast(racun_stavke.mpc as numeric), 2)), 2) " +
                "as kolicinaPutaMpc " +
                "FROM  racuni,racun_stavke  " +
                "WHERE racuni.broj_racuna=racun_stavke.broj_racuna " +
                " AND racuni.datum_racuna>'" + datumOD + "'" +
                " AND racuni.datum_racuna<'" + datumDO + "'";
            ReportParameter rds1;
            DataTable dt = classSQL.select(sql, "racun").Tables[0];
            if (dt.Rows.Count > 0)
            {
                rds1 = new ReportParameter("MPC", dt.Rows[0][0].ToString());
            }
            else
            {
                rds1 = new ReportParameter("MPC", "0");
            }

            return rds1;
        }

        private ReportParameter VratiMpcMinusRabat()
        {
            string sql = "SELECT round(SUM(CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC) * " +
                "Round(cast(racun_stavke.mpc as numeric), 2)*(1-CAST(REPLACE(racun_stavke.rabat,',','.') AS NUMERIC)/100)), 2) " +
                "as kolicinaPutaMpc " +
                "FROM  racuni,racun_stavke  " +
                "WHERE racuni.broj_racuna=racun_stavke.broj_racuna " +
                " AND racuni.datum_racuna>'" + datumOD + "'" +
                " AND racuni.datum_racuna<'" + datumDO + "'";
            ReportParameter rds1;
            DataTable dt = classSQL.select(sql, "racun").Tables[0];
            if (dt.Rows.Count > 0)
            {
                rds1 = new ReportParameter("MPCRabat", dt.Rows[0][0].ToString());
            }
            else
            {
                rds1 = new ReportParameter("MPCRabat", "0");
            }

            return rds1;
        }

        private ReportParameter VratiVpc()
        {
            string sql = "SELECT round(SUM(CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC) * Round(racun_stavke.vpc, 2)), 2)" +
                "as kolicinaPutaVpc " +
                "FROM  racuni,racun_stavke  " +
                "WHERE racuni.broj_racuna=racun_stavke.broj_racuna " +
                " AND racuni.datum_racuna>'" + datumOD + "'" +
                " AND racuni.datum_racuna<'" + datumDO + "'";

            ReportParameter rds2;
            DataTable dt = classSQL.select(sql, "racun").Tables[0];
            if (dt.Rows.Count > 0)
            {
                rds2 = new ReportParameter("VPC", dt.Rows[0][0].ToString());
            }
            else
            {
                rds2 = new ReportParameter("VPC", "0");
            }

            return rds2;
        }

        private ReportParameter VratiNbc()
        {
            string sql = "SELECT round(SUM(CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC) * " +
                "Round(cast(racun_stavke.nbc as numeric), 2)), 2) " +
                "as kolicinaPutaNbc " +
                "FROM  racuni,racun_stavke  " +
                "WHERE racuni.broj_racuna=racun_stavke.broj_racuna " +
                " AND racuni.datum_racuna>'" + datumOD + "'" +
                " AND racuni.datum_racuna<'" + datumDO + "'";
            ReportParameter rds1;
            DataTable dt = classSQL.select(sql, "racun").Tables[0];
            if (dt.Rows.Count > 0)
            {
                rds1 = new ReportParameter("NBC", dt.Rows[0][0].ToString());
            }
            else
            {
                rds1 = new ReportParameter("NBC", "0");
            }

            return rds1;
        }

        private ReportParameter VratiRabat()
        {
            string sql = "SELECT round(SUM(CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC) * " +
                "CAST(racun_stavke.mpc AS NUMERIC) * " +
                "Round(CAST(REPLACE(racun_stavke.rabat,',','.') as NUMERIC), 2)) / 100, 2) " +
                "as Rabat " +
                "FROM  racuni,racun_stavke  " +
                "WHERE racuni.broj_racuna=racun_stavke.broj_racuna " +
                " AND racuni.datum_racuna>'" + datumOD + "'" +
                " AND racuni.datum_racuna<'" + datumDO + "'";
            ReportParameter rds1;
            DataTable dt = classSQL.select(sql, "racun").Tables[0];
            if (dt.Rows.Count > 0)
            {
                rds1 = new ReportParameter("Rabat", dt.Rows[0][0].ToString());
            }
            else
            {
                rds1 = new ReportParameter("Rabat", "0");
            }

            return rds1;
        }

        /// <summary>
        /// dodaje stopu PDV-a i iznos u tablicu DTpdv ako ne postoji stopa;
        /// ako postoji zbraja s postojećim iznosom
        /// </summary>
        /// <param name="stopa"></param>
        /// <param name="iznos"></param>
        private static void dodajPDV(string vrsta, decimal stopa, decimal iznos)
        {
            DataRow[] dataROW = DTpdv.Select("stopa = '" + Convert.ToDecimal(stopa).ToString() + "' AND vrsta = '" + vrsta + "'");
            DataRow RowPdv;
            if (dataROW.Count() == 0)
            {
                RowPdv = DTpdv.NewRow();
                RowPdv["stopa"] = Convert.ToDecimal(stopa).ToString();
                RowPdv["iznos"] = iznos.ToString();
                RowPdv["vrsta"] = vrsta;
                DTpdv.Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["iznos"] = Convert.ToDecimal(dataROW[0]["iznos"].ToString()) + iznos;
                if (dataROW[0]["vrsta"].ToString() == "virman")
                {
                    string a = "";
                }
            }
        }
    }
}