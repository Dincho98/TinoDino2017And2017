using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.KarticaRobe
{
    public partial class frmKarticaRobe : Form
    {
        public frmKarticaRobe()
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
            string doc = grupa + " " + artikl + " " + skladiste + " " + DatumOD + " " + DatumDO + " " + dobavljac;
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

            for (int i = 0; i < tabl.Rows.Count; i++)
            {
                string naziv = tabl.Rows[i]["Dokument"].ToString();
                string s = "";

                if (naziv.Length > 9)
                    s = naziv.Remove(9);
                else
                    s = naziv;

                if (s != "INVENTURA")
                {
                    DateTime datum = Convert.ToDateTime(tabl.Rows[i]["Datum"].ToString());
                    decimal kol = Convert.ToDecimal(tabl.Rows[i]["Količina"].ToString());
                    decimal cijena = Convert.ToDecimal(tabl.Rows[i]["NBC"].ToString());

                    DataRow DTrow = dSRliste.Tables[0].NewRow();
                    DTrow["datum1"] = datum;
                    DTrow["naziv"] = naziv;

                    if (tabl.Rows[i]["Ulaz"].ToString() == "1")
                    {
                        DTrow["cijena1"] = kol;
                        DTrow["cijena2"] = new decimal(0);
                    }
                    else
                    {
                        DTrow["cijena2"] = kol;
                        DTrow["cijena1"] = new decimal(0);
                    }

                    DTrow["cijena3"] = cijena;
                    DTrow["cijena5"] = frmPocetnoStanje(DTrow);
                    dSRliste.Tables[0].Rows.Add(DTrow);
                }
            }
        }

        private decimal frmPocetnoStanje(DataRow DTrow)
        {
            if (dSRliste.Tables[0].Rows.Count == 0) return 0 + (decimal)DTrow["cijena1"] - (decimal)DTrow["cijena2"];
            else return (decimal)dSRliste.Tables[0].Rows[dSRliste.Tables[0].Rows.Count - 1]["cijena5"] + (decimal)DTrow["cijena1"] - (decimal)DTrow["cijena2"];
        }
    }
}