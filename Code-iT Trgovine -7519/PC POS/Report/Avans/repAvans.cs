using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PCPOS.Report.Avans
{
    public partial class repAvans : Form
    {
        public repAvans()
        {
            InitializeComponent();
        }

        public string broj_dokumenta { get; set; }
        public string godina { get; set; }
        public string dokumenat { get; set; }
        public string from_skladiste { get; set; }
        public string ImeForme { get; set; }
        public string poslovnica { get; set; }

        private void repAvans_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            string sas = "";
            if (File.Exists("Sas.txt"))
            {
                sas = "1";
            }
            else
            {
                sas = "0";
            }
            ReportParameter p1 = new ReportParameter("sas", sas);
            ReportParameter p3 = new ReportParameter("dokumenat", dokumenat);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p3 });

            if (dokumenat.ToUpper() == "avans_racun".ToUpper())
            {
                FillAvansRacun(broj_dokumenta, godina, poslovnica);
            }
            else
            {
                FillAvans(broj_dokumenta, godina);
            }
            this.Text = ImeForme;
        }

        private void FillAvans(string broj, string godina)
        {
            PCPOS.classNumberToLetter broj_u_text = new PCPOS.classNumberToLetter();

            ////MessageBox.Show(broj_slovima.ToLower());
            string usustavu_pdva = "SELECT sustav_pdv FROM postavke";
            DataTable DTsust_pdv = classSQL.select_settings(usustavu_pdva, "Sustav PDV").Tables[0];

            string sql = "SELECT " +
                " avansi.broj_avansa," +
                " avansi.dat_dok AS datum," +
                " avansi.dat_knj AS datum_knj," +
                " avansi.datum_valute AS datum_valute," +
                " nacin_placanja.naziv_placanja AS placanje," +
                " concat(CAST (avansi.model AS text),'  ',CAST (avansi.broj_avansa AS text),CAST (avansi.godina_avansa AS text),'-',CAST (avansi.broj_avansa AS text)) AS model," +
                " avansi.opis," +
                " avansi.ukupno," +
                " avansi.osnovica10," +
                " avansi.porez_var as porez," +
                " avansi.osnovica_var as osnovica," +
                " avansi.godina_avansa," +
                " avansi.nult_stp," +
                " avansi.neoporezivo," +
                " avansi.jir," +
                " avansi.zki," +
                " avansi.storno," +
                " concat(CAST (avansi.broj_avansa AS text),CAST ('" + Util.Korisno.VratiDucanIBlagajnuZaIspis(3) + "' AS text))  AS Naslov," +
                " partners.ime_tvrtke AS kupac_tvrtka," +
                " partners.adresa AS kupac_adresa," +
                " partners.id_grad AS id_kupac_grad," +
                " partners.id_partner AS sifra_kupac," +
                " ziro_racun.ziroracun AS ziro," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio," +
                " ziro_racun.banka AS banka," +
                " '' AS broj_slovima," +
                " partners.oib AS kupac_oib," +
                " porezi.iznos AS porez_postotak" +
                " FROM avansi" +
                " LEFT JOIN zacrnjeni_partner as  partners ON partners.id_partner=avansi.id_partner" +
                " LEFT JOIN nacin_placanja ON nacin_placanja.id_placanje=avansi.id_nacin_placanja" +
                " LEFT JOIN ziro_racun ON ziro_racun.id_ziroracun=avansi.ziro" +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=avansi.id_zaposlenik_izradio" +
                " LEFT JOIN porezi ON porezi.id_porez=avansi.id_pdv" +
                " WHERE avansi.broj_avansa='" + broj.Trim() + "'" +
                " AND avansi.godina_avansa='" + godina.Trim() + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql).Fill(dSAvans, "DTRAvans");
            }
            else
            {
                classSQL.NpgAdatpter(sql.Replace("nvarchar", "varchar")).Fill(dSAvans, "DTRAvans");
            }

            string sql_tecaj = string.Format(@"select avansi.id_valuta, avansi.tecaj, valute.ime_valute, valute.sifra, valute.naziv from avansi
left join valute on avansi.id_valuta = valute.id_valuta
where avansi.broj_avansa = {0} and avansi.godina_avansa::integer = {1};", broj.Trim(), godina.Trim());
            decimal tecaj = 1;
            string stranaValuta = "";
            DataSet dsTecaj = classSQL.select(sql_tecaj, "tecaj");
            if (dsTecaj != null && dsTecaj.Tables.Count > 0 && dsTecaj.Tables[0] != null && dsTecaj.Tables[0].Rows.Count > 0) {
                decimal.TryParse(dsTecaj.Tables[0].Rows[0]["tecaj"].ToString(), out tecaj);
                if (dsTecaj.Tables[0].Rows[0]["sifra"].ToString() == "978") {
                    decimal ukp = 0;
                    decimal.TryParse(dSAvans.Tables[0].Rows[0]["ukupno"].ToString(), out ukp);
                    stranaValuta =  "Prema tečaju " + tecaj.ToString() + " ukupna vrijednost iznosi " + Math.Round(ukp / tecaj, 3).ToString("#0.00") + " " + dsTecaj.Tables[0].Rows[0]["naziv"].ToString();
                    //stranaValuta = string.Format("Iznos u eurima prema tecaju {0} je {1}", tecaj, Math.Round((ukp / tecaj), 2, MidpointRounding.AwayFromZero));
                }
            }


            if (dSAvans.Tables[0].Rows.Count > 0)
            {
                if (dSAvans.Tables[0].Rows[0]["storno"].ToString() == "DA")
                {
                    dSAvans.Tables[0].Rows[0].SetField("storno", "Storno račun za predujam: ");
                }
                else
                {
                    if (DTsust_pdv.Rows[0]["sustav_pdv"].ToString() == "1")
                    { dSAvans.Tables[0].Rows[0].SetField("storno", "R1 Račun za predujam: "); }
                    else
                    {
                        dSAvans.Tables[0].Rows[0].SetField("storno", "R2 Račun za predujam: ");
                    }
                }
            }

            string ukupno = broj_u_text.PretvoriBrojUTekst(dSAvans.Tables[0].Rows[0]["ukupno"].ToString(), ',', "kn", "lp").ToString().ToLower();
            dSAvans.Tables[0].Rows[0]["broj_slovima"] = ukupno;

            string id_kupac_grad = "";
            string id_kupac = "";

            if (dSAvans.Tables[0].Rows.Count > 0)
            {
                id_kupac_grad = dSAvans.Tables[0].Rows[0]["id_kupac_grad"].ToString();
                id_kupac = dSAvans.Tables[0].Rows[0]["sifra_kupac"].ToString();
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

            sql = string.Format(@"select artikl from avansi where broj_avansa = '{0}' and godina_avansa = '{1}';", broj, godina);

            try
            {
                string artikl = classSQL.select(sql, "avans_racun").Tables[0].Rows[0]["artikl"].ToString().Trim();
                if (artikl.Length == 0)
                {
                    artikl = "Uplata predujma";
                }
                ReportParameter p2 = new ReportParameter("artikl", artikl);
                ReportParameter p4 = new ReportParameter("stranaValuta", stranaValuta);
                this.reportViewer1.LocalReport.EnableExternalImages = true;
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p2, p4 });
            }
            catch (Exception)
            {
            }

            this.reportViewer1.RefreshReport();
        }

        public string Model(string model)
        {
            string rez = "";
            if (model.Contains("¤"))
            {
                string[] mode = model.Split('¤');
                rez = (mode[0].Length > 0 ? "Model/Poziv na broj: " + mode[0] : "");

                if (rez.Length > 0)
                {
                    rez += Environment.NewLine;
                }

                rez += (mode[1].Length > 0 ? "Storniran račun za predujam: " + mode[1] : "");

                return rez;
            }

            return rez;
        }

        private void FillAvansRacun(string broj, string godina, string poslovnica)
        {
            PCPOS.classNumberToLetter broj_u_text = new PCPOS.classNumberToLetter();

            string usustavu_pdva = "SELECT sustav_pdv FROM postavke";
            DataTable DTsust_pdv = classSQL.select_settings(usustavu_pdva, "Sustav PDV").Tables[0];

            string sql = string.Format(@"SELECT
                avans_racun.broj_avansa, avans_racun.dat_dok AS datum, avans_racun.dat_knj as datum_knj, avans_racun.datum_valute, nacin_placanja.naziv_placanja AS placanje, CAST (avans_racun.model AS text) + '  '+ CAST (avans_racun.broj_avansa AS text)+CAST (avans_racun.godina_avansa AS text)+'-'+CAST (avans_racun.broj_avansa AS text) +'¤'+case when avans_racun.storno > 0 then (select ar.broj_avansa||'/'||d.ime_ducana||'/'||ar.godina_avansa from avans_racun ar left join ducan d on d.id_ducan = ar.poslovnica
where ar.id = avans_racun.storno) else '' end AS model, avans_racun.opis, avans_racun.ukupno, avans_racun.osnovica10, avans_racun.porez_var as porez, avans_racun.osnovica_var as osnovica, avans_racun.godina_avansa, avans_racun.nult_stp, avans_racun.neoporezivo, avans_racun.jir, avans_racun.zki, avans_racun.storno, concat(CAST(avans_racun.broj_avansa AS text),'/',ducan.ime_ducana,'/',CAST(godina_avansa as text))  AS Naslov, partners.ime_tvrtke AS kupac_tvrtka, partners.adresa AS kupac_adresa, partners.id_grad AS id_kupac_grad, partners.id_partner AS sifra_kupac, ziro_racun.ziroracun AS ziro, zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio, ziro_racun.banka AS banka, '' AS broj_slovima, partners.oib AS kupac_oib, porezi.iznos AS porez_postotak
FROM avans_racun
LEFT JOIN zacrnjeni_partner as  partners ON partners.id_partner=avans_racun.id_partner
LEFT JOIN nacin_placanja ON nacin_placanja.id_placanje=avans_racun.id_nacin_placanja
LEFT JOIN ziro_racun ON ziro_racun.id_ziroracun=avans_racun.ziro
LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=avans_racun.id_zaposlenik_izradio
LEFT JOIN porezi ON porezi.id_porez=avans_racun.id_pdv
left join ducan on ducan.id_ducan = avans_racun.poslovnica
WHERE avans_racun.broj_avansa={0} AND avans_racun.godina_avansa={1} and avans_racun.poslovnica = {2};", broj, godina, poslovnica);

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql).Fill(dSAvans, "DTRAvans");
            }
            else
            {
                classSQL.NpgAdatpter(sql.Replace("nvarchar", "varchar")).Fill(dSAvans, "DTRAvans");
            }

            if (dSAvans.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt32(dSAvans.Tables[0].Rows[0]["storno"].ToString()) > 0)
                {
                    dSAvans.Tables[0].Rows[0].SetField("storno", "Storno računa za predujam: ");
                }
                else
                {
                    if (DTsust_pdv.Rows[0]["sustav_pdv"].ToString() == "1")
                    {
                        dSAvans.Tables[0].Rows[0].SetField("storno", "Račun za predujam: ");
                    }
                    else
                    {
                        dSAvans.Tables[0].Rows[0].SetField("storno", "Račun za predujam: ");
                    }
                }
            }

            string ukupno = broj_u_text.PretvoriBrojUTekst(dSAvans.Tables[0].Rows[0]["ukupno"].ToString(), ',', "kn", "lp").ToString().ToLower();
            dSAvans.Tables[0].Rows[0]["broj_slovima"] = ukupno;

            string id_kupac_grad = "";
            string id_kupac = "";

            if (dSAvans.Tables[0].Rows.Count > 0)
            {
                id_kupac_grad = dSAvans.Tables[0].Rows[0]["id_kupac_grad"].ToString();
                id_kupac = dSAvans.Tables[0].Rows[0]["sifra_kupac"].ToString();
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

            sql = string.Format(@"select artikl from avans_racun where broj_avansa = {0} and poslovnica = {1} and godina_avansa = {2}", broj, poslovnica, godina);

            string artikl = classSQL.select(sql, "avans_racun").Tables[0].Rows[0]["artikl"].ToString().Trim();
            if (artikl.Length == 0)
            {
                artikl = "Uplata predujma";
            }
            ReportParameter p2 = new ReportParameter("artikl", artikl);
            ReportParameter p3 = new ReportParameter("stranaValuta", "1");
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p2, p3 });

            this.reportViewer1.RefreshReport();
        }
    }
}