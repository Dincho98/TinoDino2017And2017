using System;
using System.Windows.Forms;

namespace PCPOS.Report.AutomatskaUsklada
{
    public partial class frmAutomatskaUsklada : Form
    {
        public bool isVpc { get; set; }
        public int broj { get; set; }
        public int id_poslovnica { get; set; }
        public int id_naplatni_uredaj { get; set; }

        public frmAutomatskaUsklada()
        {
            InitializeComponent();
        }

        private void frmAutomatskaUsklada_Load(object sender, EventArgs e)
        {
            this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            try
            {
                string sql = "select * from automatska_usklada where broj = '" + broj + "' and id_poslovnica = '" + id_poslovnica + "' and id_naplatni_uredaj = '" + id_naplatni_uredaj + "'";

                if (classSQL.remoteConnectionString == "")
                {
                    classSQL.CeAdatpter(sql).Fill(dsAutomatskaUsklada1, "dtAutomatskaUsklada");
                }
                else
                {
                    classSQL.NpgAdatpter(sql.Replace("nvarchar", "varchar")).Fill(dsAutomatskaUsklada1, "dtAutomatskaUsklada");
                }

                sql = "select * from automatska_usklada_stavke where broj = '" + broj + "' and id_poslovnica = '" + id_poslovnica + "' and id_naplatni_uredaj = '" + id_naplatni_uredaj + "'";

                if (classSQL.remoteConnectionString == "")
                {
                    classSQL.CeAdatpter(sql).Fill(dsAutomatskaUsklada1, "dtAutomatskaUskladaStavke");
                }
                else
                {
                    classSQL.NpgAdatpter(sql.Replace("nvarchar", "varchar")).Fill(dsAutomatskaUsklada1, "dtAutomatskaUskladaStavke");
                }

                sql = "SELECT podaci_tvrtka.ime_tvrtke, podaci_tvrtka.skraceno_ime, podaci_tvrtka.oib, podaci_tvrtka.tel, podaci_tvrtka.fax, podaci_tvrtka.mob, podaci_tvrtka.iban, podaci_tvrtka.dodatniPodaciHeader, podaci_tvrtka.swift, podaci_tvrtka.adresa, podaci_tvrtka.zr, podaci_tvrtka.vl, podaci_tvrtka.poslovnica_adresa, podaci_tvrtka.email, podaci_tvrtka.pdv_br, podaci_tvrtka.ime_poslovnice FROM podaci_tvrtka";

                if (classSQL.remoteConnectionString == "")
                {
                    classSQL.CeAdatpter(sql).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
                }
                else
                {
                    classSQL.CeAdatpter(sql).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
                    //classSQL.NpgAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
                }

                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}