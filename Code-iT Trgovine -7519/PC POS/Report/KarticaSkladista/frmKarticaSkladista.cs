using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.KarticaSkladista
{
    public partial class frmKarticaSkladista : Form
    {
        public frmKarticaSkladista()
        {
            InitializeComponent();
        }

        public string skladiste { get; set; }
        public string grupa { get; set; }
        public string artikl { get; set; }
        public string DatumOD { get; set; }
        public string DatumDO { get; set; }
        public string dobavljac { get; set; }
        public DataTable tabl { get; set; }

        private void frmListe_Load(object sender, EventArgs e)
        {
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);

            string doc = skladiste + " " + grupa + " " + artikl + " " + DatumOD + " - " + DatumDO + " " + dobavljac;
            ListaFakture();
            ReportParameter p1 = new
            ReportParameter("Dokument", doc);
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1 });
            this.reportViewer1.RefreshReport();
        }

        private void ListaFakture()
        {
            string naziv_fakture = " podaci_tvrtka.naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql("", naziv_fakture, "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            string top = "";
            for (int i = 0; i < tabl.Rows.Count; i++)
            {
                string sifra = tabl.Rows[i]["šifra"].ToString();
                string naziv = tabl.Rows[i]["naziv"].ToString();
                string kol = tabl.Rows[i]["Kolicina skladište"].ToString();
                string sklad = tabl.Rows[i]["skladište"].ToString();
                string cijena = tabl.Rows[i]["MPC"].ToString();

                DataRow DTrow = dSRliste.Tables[0].NewRow();
                DTrow["sifra"] = sifra.ToString();
                DTrow["naziv"] = naziv.ToString();
                DTrow["cijena1"] = kol.ToString();
                DTrow["jmj"] = sklad.ToString();
                DTrow["cijena3"] = cijena.ToString();
                dSRliste.Tables[0].Rows.Add(DTrow);
            }
        }
    }
}