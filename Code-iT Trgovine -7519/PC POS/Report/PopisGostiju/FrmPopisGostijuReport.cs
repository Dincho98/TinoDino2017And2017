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


namespace PCPOS.Report.PopisGostiju
{
    public partial class FrmPopisGostijuReport : Form
    {
        public Gost gost;
        public Gost[] gosti;

        //Konstruktor za ispis pojedinačnog gosta
        public FrmPopisGostijuReport(Gost gost)
        {
            InitializeComponent();
            this.Controls.Add(this.reportViewer);
            this.gost = gost; // this je globalni
            LoadSingleData();
        }

        //Konstuktor za ispis svih gostiju
        public FrmPopisGostijuReport(Gost[] gosti)
        {
            InitializeComponent();
            this.Controls.Add(this.reportViewer);
            this.gosti = gosti;
            LoadAllData();
        }

        private void FrmPopisGostijuReport_Load(object sender, EventArgs e)
        {
            this.reportViewer.SetDisplayMode(DisplayMode.PrintLayout);

            ReportDataSource gostiDataSet = new ReportDataSource();
            ReportDataSource podaciTvrtkeDataSource = new ReportDataSource();

            gostiDataSet.Name = "dsPopisGostiju"; // rdlc
            gostiDataSet.Value = dsPopisGostiju.Tables[0]; // designer
            reportViewer.LocalReport.DataSources.Add(gostiDataSet);

            podaciTvrtkeDataSource.Name = "dsPodaciTvrtke"; // rdlc
            podaciTvrtkeDataSource.Value = dSRpodaciTvrtke.Tables[0]; // designer
            reportViewer.LocalReport.DataSources.Add(podaciTvrtkeDataSource);

            this.reportViewer.LocalReport.ReportEmbeddedResource = "PCPOS.Report.PopisGostiju.GostReport.rdlc";
            this.reportViewer.RefreshReport();
        }

        //Učitavanje podataka za samo jedan zapis
        private void LoadSingleData()
        {
            LoadDataGuest();
            LoadDataCompany();
        }

        //Učitavanje podataka za sve zapise
        private void LoadAllData()
        {
            LoadDataGuests();
            LoadDataCompany();
        }

        private void LoadDataGuest()
        {
            DataRow row = dsPopisGostiju.Tables[0].NewRow();
            row["broj"] = gost.broj;
            row["prezimeime"] = gost.imePrezime;
            row["brojdokumenta"] = GenerirajBrojDokumenta(gost);
            row["usluga"] = gost.vrstaPruzeneUsluge;
            row["datumpocetka"] = GenerirajDatumPocetka(gost);
            row["datumkraja"] = GenerirajDatumKraja(gost);
            row["primjedba"] = gost.primjedba;
            dsPopisGostiju.Tables[0].Rows.Add(row);
        }

        public void LoadDataGuests()
        {
            for (int i = 0; i < gosti.Length; i++)
            {
                DataRow row = dsPopisGostiju.Tables[0].NewRow();
                row["broj"] = gosti[i].broj;
                row["prezimeime"] = gosti[i].imePrezime;
                row["brojdokumenta"] = GenerirajBrojDokumenta(gosti[i]);
                row["usluga"] = gosti[i].vrstaPruzeneUsluge;
                row["datumpocetka"] = GenerirajDatumPocetka(gosti[i]); 
                row["datumkraja"] = GenerirajDatumKraja(gosti[i]); 
                row["primjedba"] = gosti[i].primjedba;
                dsPopisGostiju.Tables[0].Rows.Add(row);
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

        private string GenerirajBrojDokumenta(Gost gost)
        {
            string brDokumenta = null;

            if (!string.IsNullOrWhiteSpace(gost.brojOsobne))
            {
                brDokumenta +="O: "+ gost.brojOsobne;

                if (!string.IsNullOrWhiteSpace(gost.brojPutovnice))
                {
                    brDokumenta += ", P: ";
                    brDokumenta += gost.brojPutovnice;
                }

                return brDokumenta;
            }

            if (!string.IsNullOrWhiteSpace(gost.brojPutovnice))
            {
                brDokumenta += "P: "+gost.brojPutovnice;
            }

            return brDokumenta;
        }

        //Maknut će vrijeme, ostavit će samo datum
        private string GenerirajDatumPocetka(Gost gost)
        {
            string[] array = gost.datumPocetka.Split(' ');
            return array[0];
        }

        //Maknut će vrijeme, ostavit će samo datum
        private string GenerirajDatumKraja(Gost gost)
        {
            string[] array = gost.datumKraja.Split(' ');
            return array[0];
        }
    }
}
