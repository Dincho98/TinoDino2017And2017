using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using PCPOS.Entities;

namespace PCPOS.Report.KnjigaPrometa
{
    public partial class FrmKnjigaPrometaReport : Form
    {
        ZapisKnjigePrometa[] zapis;

        public FrmKnjigaPrometaReport(ZapisKnjigePrometa[] zapis)
        {
            InitializeComponent();
            this.Controls.Add(this.reportViewer1);
            this.zapis = zapis; // this označava globalni 
            LoadData();
        }

        private void KnjigaPrometaReport_Load(object sender, EventArgs e)
        {
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);

            ReportDataSource knjigaPrometaDataSet = new ReportDataSource();
            knjigaPrometaDataSet.Name = "dataSetKnjigaPrometa"; // rdlc
            knjigaPrometaDataSet.Value = dSKnjigaPrometa.Tables[0]; // designer
            reportViewer1.LocalReport.DataSources.Add(knjigaPrometaDataSet);

            ReportDataSource podaciTvrtkeDataSet = new ReportDataSource();
            podaciTvrtkeDataSet.Name = "dataSetPodaciTvrtke"; // rdlc
            podaciTvrtkeDataSet.Value = dSRpodaciTvrtke.Tables[0]; // designer
            reportViewer1.LocalReport.DataSources.Add(podaciTvrtkeDataSet);

            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.KnjigaPrometa.KnjigaPrometaReport.rdlc";
            this.reportViewer1.RefreshReport();
        }

        private void LoadData()
        {
            LoadDataIntoTable();
            LoadDataCompany();
        }

        private void LoadDataIntoTable()
        {
            for (int i = 0; i < zapis.Length; i++)
            {
                DataRow row = dSKnjigaPrometa.Tables[0].NewRow();
                row["redniBroj"] = zapis[i].redniBroj;
                row["brojRacuna"] = zapis[i].brojRacuna;
                row["datum"] = zapis[i].datum;
                row["iznos"] = zapis[i].iznos;
                row["nacinPlacanja"] = zapis[i].nacinPlacanja;
                dSKnjigaPrometa.Tables[0].Rows.Add(row);
            }
        }

        private void LoadDataCompany()
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
        }
    }
}
