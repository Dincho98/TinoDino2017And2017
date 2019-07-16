using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace PCPOS.Report.KnjiznoOdobrenje
{
    public partial class frmKnjiznoOdobrenje : Form
    {
        private string sql = "";
        public int broj_odobrenje { get; set; }

        public frmKnjiznoOdobrenje()
        {
            InitializeComponent();
        }

        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private void frmKnjiznoOdobrenje_Load(object sender, EventArgs e)
        {
            this.reportViewer1.LocalReport.EnableExternalImages = true;

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

            ReportParameter p2 = new ReportParameter("path", root);
            ReportParameter p5 = new ReportParameter("sas", sas);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p2, p5 });

            sql = "Select row_number() over() as rb, ko.broj_odobrenje, ko.datum, ko.broj_fakture, ko.id_ducan, ko.id_kasa, ko.id_ducan_faktura, ko.id_kasa_faktura, ko.id_izradio, ko.porez_odobrenja, f.ukupno::numeric as fUkp, f.ukupno_porez, " +
                    "ROUND(f.ukupno::numeric-(f.ukupno::numeric*(ko.porez_odobrenja/100)), 2) as fUkpNew, ROUND(((f.ukupno::numeric-(f.ukupno::numeric*(ko.porez_odobrenja/100)))/1.25)*0.25, 2) as fUkpPorezNew, f.date as datum_fakture " +
                    "from knjizno_odobrenje ko " +
                    "left join fakture f on ko.broj_fakture = f.broj_fakture and ko.id_kasa_faktura = f.id_kasa and ko.id_ducan_faktura = f.id_ducan where broj_odobrenje = '" + broj_odobrenje + "'";

            //if (true)
            //{
            //    sql = "Select row_number() over() as rb, ko.broj_odobrenje, ko.datum, ko.broj_fakture, ko.id_ducan, ko.id_kasa, ko.id_ducan_faktura, ko.id_kasa_faktura, ko.id_izradio, ko.porez_odobrenja, f.ukupno::numeric as fUkp, f.ukupno_porez, " +
            //        "ROUND(f.ukupno::numeric-(f.ukupno::numeric*(ko.porez_odobrenja/100)), 2) as fUkpNew, ROUND(((f.ukupno::numeric-(f.ukupno::numeric*(ko.porez_odobrenja/100)))/1.25)*0.0, 2) as fUkpPorezNew, f.date as datum_fakture " +
            //        "from knjizno_odobrenje ko " +
            //        "left join fakture f on ko.broj_fakture = f.broj_fakture and ko.id_kasa_faktura = f.id_kasa and ko.id_ducan_faktura = f.id_ducan where broj_odobrenje = '" + broj_odobrenje + "'";
            //}

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql).Fill(dSKnjiznoOdobrenje, "dtKnjiznoOdobrenje");
            }
            else
            {
                classSQL.NpgAdatpter(sql).Fill(dSKnjiznoOdobrenje, "dtKnjiznoOdobrenje");
            }

            DataTable dtKnjiznoOdobrenje = classSQL.select(sql, "knjizno_odobrenje").Tables[0];

            sql = "SELECT " +
                " fakture.broj_fakture," +
                " fakture.broj_avansa," +
                " fakture.godina_avansa," +
                " fakture.date AS datum," +
                " fakture.dateDVO AS datum_dvo," +
                " fakture.datum_valute," +
                " fakture.jir," +
                " fakture.zki," +
                " fakture.tecaj AS tecaj," +
                " valute.ime_valute AS valuta," +
                " fakture.mj_troska AS mjesto_troska," +
                " nacin_placanja.naziv_placanja AS placanje," +
                " otprema.naziv AS otprema," +
                " CAST (fakture.model AS nvarchar) + '  '+ CAST (fakture.broj_fakture AS nvarchar)+" +
                "CAST (fakture.godina_fakture AS nvarchar)+'-'+CAST (fakture.id_fakturirati AS nvarchar) AS model," +
                " fakture.napomena," +
                " '0' AS rabat," +
                " '0' AS ukupno," +
                " '0' AS iznos_pdv," +
                " '0' AS osnovica," +
                " fakture.godina_fakture," +
                //" CAST (fakture.broj_fakture AS nvarchar) +'/'+ CAST (fakture.godina_fakture AS nvarchar) AS Naslov," +
                " CAST (fakture.broj_fakture AS nvarchar) + CAST ('" + Util.Korisno.VratiDucanIBlagajnuZaIspis(2) + "' AS nvarchar)  AS Naslov," +
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
                " 'br slov' AS broj_slovima," +
                " partners.oib AS kupac_oib, " +
                " partners.oib_polje AS string5 " +
                " FROM fakture" +
                " LEFT JOIN partners ON partners.id_partner=fakture.id_odrediste" +
                " LEFT JOIN otprema ON otprema.id_otprema=fakture.otprema" +
                " LEFT JOIN nacin_placanja ON nacin_placanja.id_placanje=fakture.id_nacin_placanja" +
                " LEFT JOIN ziro_racun ON ziro_racun.id_ziroracun=fakture.zr" +
                " LEFT JOIN valute ON valute.id_valuta=fakture.id_valuta" +
                " LEFT JOIN zemlja ON zemlja.id_zemlja=partners.id_zemlja" +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=fakture.id_zaposlenik_izradio WHERE fakture.broj_fakture='" + dtKnjiznoOdobrenje.Rows[0]["broj_fakture"] + "'" +
                " AND fakture.id_ducan='" + dtKnjiznoOdobrenje.Rows[0]["id_ducan_faktura"] + "' AND fakture.id_kasa='" + dtKnjiznoOdobrenje.Rows[0]["id_kasa_faktura"] + "'" +
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

            string id_kupac_grad = "";
            string id_kupac = "";
            if (dSFaktura.Tables[0].Rows.Count > 0)
            {
                id_kupac_grad = dSFaktura.Tables[0].Rows[0]["id_kupac_grad"].ToString();
                id_kupac = dSFaktura.Tables[0].Rows[0]["sifra_kupac"].ToString();
            }
            else
            {
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

            //if (DTpostavke.Rows[0]["ispis_partnera"].ToString() == "0") {
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
            //}

            this.reportViewer1.RefreshReport();
        }

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
    }
}