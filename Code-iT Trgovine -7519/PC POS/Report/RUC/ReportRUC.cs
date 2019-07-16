using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCPOS.Report.RUC
{
    public partial class ReportRUC : Form
    {
        public DateTime datumOd { get; set; }
        public DateTime datumDo { get; set; }
        public string idDucan { get; set; }
        public string idKasa { get; set; }

        public ReportRUC()
        {
            InitializeComponent();
            this.Controls.Add(this.reportViewer1);
        }

        private void ReportRUC_Load(object sender, EventArgs e)
        {
            SetPoslovnica();
            LoadPodaciTvrtke();
            LoadData();

            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);

            ReportDataSource podaciTvrtkeDataSource = new ReportDataSource();
            ReportDataSource listeDataSource = new ReportDataSource();

            podaciTvrtkeDataSource.Name = "DSPodaciTvrtke";
            podaciTvrtkeDataSource.Value = dSRpodaciTvrtke.Tables[0];
            reportViewer1.LocalReport.DataSources.Add(podaciTvrtkeDataSource);

            listeDataSource.Name = "DSListe";
            listeDataSource.Value = dSRlisteTekst.Tables[0];
            reportViewer1.LocalReport.DataSources.Add(listeDataSource);

            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.RUC.RazlikaUCijeni.rdlc";
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.RefreshReport();
        }

        /// <summary>
        /// Sets "ducan" and "kasa" ID's
        /// </summary>
        private void SetPoslovnica()
        {
            DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            if (DTpostavke.Rows.Count > 0)
            {
                try
                {
                    idKasa = DTpostavke.Rows[0]["naplatni_uredaj_faktura"].ToString();
                }
                catch
                {
                    MessageBox.Show("Kasa nije odabrana. Provjerite postavke programa.", "Upozorenje!");
                    idKasa = "1";
                }
                try
                {
                    idDucan = DTpostavke.Rows[0]["default_ducan"].ToString();
                }
                catch
                {
                    MessageBox.Show("Dućan nije odabran. Provjerite postavke programa.", "Upozorenje!");
                    idDucan = "1";
                }
            }
        }

        /// <summary>
        /// Loads podaci_tvrtke data
        /// </summary>
        private void LoadPodaciTvrtke()
        {
            string sql = "SELECT " +
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
                " podaci_tvrtka.naziv_fakture," +
                " podaci_tvrtka.text_bottom," +
                " grad.grad + '' + grad.posta AS grad" +
                " FROM podaci_tvrtka" +
                " LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad" +
                "";
            classSQL.CeAdatpter(sql).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
        }

        /// <summary>
        /// Loads report data
        /// </summary>
        private void LoadData()
        {
            decimal nbcUkupno = 0;
            decimal vpcUkupno = 0;
            decimal mpcukupno = 0;
            decimal pdvUkupno = 0;
            decimal rucUkupno = 0;

            // Racuni
            string queryRacun = $@"SELECT CAST(racun_stavke.nbc AS numeric) AS nbc,
	                            CAST(racun_stavke.vpc as numeric) AS vpc,
	                            CAST(racun_stavke.mpc as numeric) AS mpc
                            FROM racun_stavke
                            LEFT JOIN racuni ON racun_stavke.broj_racuna = racuni.broj_racuna
                            WHERE racuni.datum_racuna >= '{datumOd.ToString("dd-MM-yyyy 00:00:00")}' AND racuni.datum_racuna <= '{datumDo.ToString("dd-MM-yyyy 23:59:59")}'";

            DataTable DTracun = classSQL.select(queryRacun, "racun_stavke").Tables[0];

            for (int i = 0; i < DTracun.Rows.Count; i++)
            {
                decimal nbc = Convert.ToDecimal(DTracun.Rows[i]["nbc"].ToString());
                decimal vpc = Convert.ToDecimal(DTracun.Rows[i]["vpc"].ToString());
                decimal mpc = Convert.ToDecimal(DTracun.Rows[i]["mpc"].ToString());

                nbcUkupno += nbc;
                vpcUkupno += vpc;
                mpcukupno += mpc;
                rucUkupno += (vpc - nbc);
                if (Class.Postavke.sustavPdv)
                    pdvUkupno += (mpc - vpc);
            }

            // Fakture
            string queryFakture = $@"SELECT CAST(faktura_stavke.nbc AS numeric) AS nbc,
	                            CAST(faktura_stavke.vpc as numeric) AS vpc,
	                            CAST(REPLACE(faktura_stavke.porez, ',', '.') as numeric) AS porez
                            FROM faktura_stavke
                            LEFT JOIN fakture ON faktura_stavke.broj_fakture = fakture.broj_fakture
                            WHERE fakture.date >= '{datumOd.ToString("dd-MM-yyyy 00:00:00")}' AND fakture.date <= '{datumDo.ToString("dd-MM-yyyy 23:59:59")}'";

            DataTable DTfakture = classSQL.select(queryFakture, "faktura_stavke").Tables[0];

            for (int i = 0; i < DTfakture.Rows.Count; i++)
            {
                decimal nbc = Convert.ToDecimal(DTfakture.Rows[i]["nbc"].ToString());
                decimal vpc = Convert.ToDecimal(DTfakture.Rows[i]["vpc"].ToString());
                decimal porez = Convert.ToDecimal(DTfakture.Rows[i]["porez"].ToString());
                decimal mpc = (vpc * (1 + (porez / 100)));

                nbcUkupno += nbc;
                vpcUkupno += vpc;
                mpcukupno += mpc;
                rucUkupno += (vpc - nbc);
                if (Class.Postavke.sustavPdv)
                    pdvUkupno += (mpc - vpc);
            }

            DataRow row = dSRlisteTekst.Tables[0].NewRow();
            row["ukupno1"] = nbcUkupno.ToString();
            row["ukupno2"] = vpcUkupno.ToString();
            row["ukupno3"] = mpcukupno.ToString();
            row["ukupno4"] = rucUkupno.ToString();
            row["ukupno5"] = pdvUkupno.ToString();
            row["datum1"] = datumOd.ToString();
            row["datum2"] = datumDo.ToString();
            dSRlisteTekst.Tables[0].Rows.Add(row);
        }
    }
}
