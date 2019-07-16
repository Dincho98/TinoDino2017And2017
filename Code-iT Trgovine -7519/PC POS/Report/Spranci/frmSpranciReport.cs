using Microsoft.Reporting.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Report.Spranci
{
    public partial class frmSpranciReport : Form
    {
        public frmSpranciReport()
        {
            InitializeComponent();
        }

        public string logo { get; set; }
        public string poslovnica { get; set; }
        public string sifra { get; set; }
        public string slika { get; set; }
        public string opis { get; set; }
        public string gotovina { get; set; }
        public string kartice { get; set; }
        public string naziv { get; set; }

        private void frmSpranciReport_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

            ReportParameter p1 = new ReportParameter("SlikaLogo", logo);
            ReportParameter p2 = new ReportParameter("SlikaGlavna", slika);
            ReportParameter p3 = new ReportParameter("Sifra", sifra);
            ReportParameter p4 = new ReportParameter("Poslovnica", poslovnica);
            ReportParameter p5 = new ReportParameter("Opis", opis);
            ReportParameter p6 = new ReportParameter("Kartice", kartice);
            ReportParameter p8 = new ReportParameter("Gotovina", gotovina);
            ReportParameter p9 = new ReportParameter("Naziv", naziv);

            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6, p8, p9 });

            this.reportViewer1.RefreshReport();
        }
    }
}