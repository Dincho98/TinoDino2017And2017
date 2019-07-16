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
using System.Globalization;
using System.IO;
using System.Drawing.Printing;

namespace PCPOS.Report.RobniDobropis
{
    public partial class ReportRobniDobropis : Form
    {
        public int IdDobropis { get; set; }
        public DateTime Date { get; set; }
        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        public ReportRobniDobropis()
        {
            InitializeComponent();
            Controls.Add(reportViewer);
        }

        private void ReportRobniDobropis_Load(object sender, EventArgs e)
        {
            string root = DTpostavke.Rows[0]["logo"].ToString() == "1" ? DTpostavke.Rows[0]["logopath"].ToString() : Path.GetDirectoryName(Application.ExecutablePath) + "\\bijela.jpg";

            LoadComapnyData();
            LoadDobropis();

            reportViewer.SetDisplayMode(DisplayMode.PrintLayout);

            ReportDataSource podaciTvrtkeDataSource = new ReportDataSource();
            ReportDataSource listeDataSource = new ReportDataSource();
            ReportDataSource dobropisDataSource = new ReportDataSource();
            ReportDataSource fakturaDataSource = new ReportDataSource();

            podaciTvrtkeDataSource.Name = "DSPodaciTvrtke";
            podaciTvrtkeDataSource.Value = dSRpodaciTvrtke.Tables[0];
            reportViewer.LocalReport.DataSources.Add(podaciTvrtkeDataSource);

            listeDataSource.Name = "DSliste";
            listeDataSource.Value = dSRliste.Tables[0];
            reportViewer.LocalReport.DataSources.Add(listeDataSource);

            dobropisDataSource.Name = "DSdobropis";
            dobropisDataSource.Value = dSRliste.Tables[1];
            reportViewer.LocalReport.DataSources.Add(dobropisDataSource);

            fakturaDataSource.Name = "DSFaktura";
            fakturaDataSource.Value = dSFaktura.Tables[0];
            reportViewer.LocalReport.DataSources.Add(fakturaDataSource);

            ReportParameter p1 = new ReportParameter("path", root);

            PageSettings pageSettings = new PageSettings();
            pageSettings.Margins.Top = 0;
            pageSettings.Margins.Bottom = 0;
            pageSettings.Margins.Left = 0;
            pageSettings.Margins.Right = 0;
            pageSettings.Landscape = false;
            reportViewer.SetPageSettings(pageSettings);
            reportViewer.LocalReport.ReportEmbeddedResource = "PCPOS.Report.RobniDobropis.RobniDobropis.rdlc";
            reportViewer.LocalReport.EnableExternalImages = true;
            this.reportViewer.LocalReport.SetParameters(new ReportParameter[] { p1 });
            reportViewer.RefreshReport();
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadComapnyData()
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
                " podaci_tvrtka.pdv_br," +
                " podaci_tvrtka.zr," +
                " podaci_tvrtka.naziv_fakture," +
                " podaci_tvrtka.text_bottom," +
                " grad.grad + '' + grad.posta AS grad" +
                " FROM podaci_tvrtka" +
                " LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad" +
                "";
            classSQL.CeAdatpter(sqlpodaci).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadDobropis()
        {
            // Dobropis
            string queryDobropis = $@"SELECT dobropis.datum,
                                        dobropis.id_izradio,
	                                    partners.id_partner,
	                                    partners.iban,
	                                    partners.swift,
	                                    partners.oib,
                                        partners.oib_polje,
	                                    partners.ime_tvrtke,
	                                    partners.adresa,
	                                    grad.grad,
	                                    zemlja.zemlja
                                    FROM dobropis
                                    LEFT JOIN partners ON partners.id_partner = dobropis.id_partner
                                    LEFT JOIN grad ON grad.id_grad = partners.id_grad
                                    LEFT JOIN zemlja ON zemlja.id_zemlja = grad.drzava
                                    WHERE dobropis.broj_dobropis = '{IdDobropis}'";

            DataTable DTdobropis = classSQL.select(queryDobropis, "dobropis").Tables[0];
            if(DTdobropis.Rows.Count > 0)
            {
                foreach(DataRow row in DTdobropis.Rows)
                {
                    DataRow dobropisRow = dSRliste.Tables[1].NewRow();
                    dobropisRow["datum"] = DateTime.Parse(row["datum"].ToString());
                    dobropisRow["iban"] = row["iban"].ToString();
                    dobropisRow["swift"] = row["swift"].ToString();
                    dobropisRow["kupac_sifra"] = row["id_partner"].ToString();
                    dobropisRow["kupac_oib"] = row["oib"].ToString();
                    dobropisRow["kupac_tvrtka"] = row["ime_tvrtke"].ToString();
                    dobropisRow["kupac_adresa"] = row["adresa"].ToString();
                    dobropisRow["kupac_grad"] = row["grad"].ToString();
                    dobropisRow["kupac_drzava"] = row["zemlja"].ToString();
                    dobropisRow["oib_polje"] = row["oib_polje"].ToString();
                    DataTable DTZaposlenici = Global.Database.GetZaposlenici(row["id_izradio"].ToString());
                    dobropisRow["preuzeo"] = DTZaposlenici.Rows[0]["ime"] + " " + DTZaposlenici.Rows[0]["prezime"];
                    dSRliste.Tables[1].Rows.Add(dobropisRow);
                }
            }

            // Dobropis stavke
            DataTable DTdobropisStavke = Global.Database.GetDobropisStavke(IdDobropis);
            if(DTdobropisStavke.Rows.Count > 0)
            {
                foreach(DataRow row in DTdobropisStavke.Rows)
                {
                    decimal.TryParse(row["kolicina"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal kolicina);
                    decimal.TryParse(row["cijena_bez_pdv"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal cijenaBezPdv);
                    decimal.TryParse(row["porez"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal porez);
                    decimal.TryParse(row["rabat_iznos"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal rabatIznos);

                    DataRow listeRow = dSRliste.Tables[0].NewRow();
                    listeRow["sifra"] = row["sifra_robe"].ToString();
                    listeRow["naziv"] = row["naziv"].ToString();
                    listeRow["datum1"] = Date;
                    listeRow["kolicina"] = kolicina; 
                    listeRow["cijena1"] = row["vpc"].ToString();
                    listeRow["cijena2"] = row["porez"].ToString();
                    listeRow["cijena3"] = row["mpc"].ToString();
                    listeRow["cijena5"] = row["rabat"].ToString();
                    listeRow["cijena6"] = rabatIznos * kolicina;
                    listeRow["cijena7"] = cijenaBezPdv * kolicina;
                    listeRow["cijena8"] = row["iznos_bez_pdv"].ToString();
                    listeRow["cijena9"] = row["iznos_ukupno"].ToString();
                    listeRow["cijena10"] = (cijenaBezPdv * kolicina) * (porez / 100);
                    dSRliste.Tables[0].Rows.Add(listeRow);
                }
            }
        }
    }
}
