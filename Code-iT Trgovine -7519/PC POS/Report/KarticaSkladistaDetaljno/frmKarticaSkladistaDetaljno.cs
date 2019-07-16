using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.frmKarticaSkladistaDetaljno
{
    public partial class frmKarticaSkladistaDetaljno : Form
    {
        public frmKarticaSkladistaDetaljno()
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
        public string sortiranje { get; set; }
        public bool fakturna_cijena { get; internal set; }

        private void frmListe_Load(object sender, EventArgs e)
        {
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);

            string doc = skladiste + " " + grupa + " " + artikl + " " + DatumOD + " " + DatumDO + " " + dobavljac;
            ListaFakture();
            ReportParameter p1 = new ReportParameter("Dokument", doc);
            ReportParameter p2 = new ReportParameter("Sortiranje", sortiranje);
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });
            this.reportViewer1.RefreshReport();
        }

        private void ListaFakture()
        {
            string naziv_fakture = " podaci_tvrtka.naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql("", naziv_fakture, "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            for (int i = 0; i < tabl.Rows.Count; i++)
            {
                //ulaz
                string lblKalk = tabl.Rows[i]["Kalkulacija"].ToString() == "" ? "0" : tabl.Rows[i]["Kalkulacija"].ToString();
                string lblPrimka = tabl.Rows[i]["Primka"].ToString() == "" ? "0" : tabl.Rows[i]["Primka"].ToString();
                string lblUlazMS = tabl.Rows[i]["Meduskladisnica na skladista"].ToString() == "" ? "0" : tabl.Rows[i]["Meduskladisnica na skladista"].ToString();
                string lblPS = tabl.Rows[i]["Pocetno stanje"].ToString() == "" ? "0" : tabl.Rows[i]["Pocetno stanje"].ToString();
                string lblRadniD = tabl.Rows[i]["Radni nalog dodaj"].ToString() == "" ? "0" : tabl.Rows[i]["Radni nalog dodaj"].ToString();

                //izlaz
                string lblKasa = tabl.Rows[i]["Maloprodaja"].ToString() == "" ? "0" : tabl.Rows[i]["Maloprodaja"].ToString();
                string lblFaktura = tabl.Rows[i]["Fakture"].ToString() == "" ? "0" : tabl.Rows[i]["Fakture"].ToString();
                string lblIzdatnica = tabl.Rows[i]["Izdatnica"].ToString() == "" ? "0" : tabl.Rows[i]["Izdatnica"].ToString();
                string lblPovratnica = tabl.Rows[i]["Povratnica"].ToString() == "" ? "0" : tabl.Rows[i]["Povratnica"].ToString();
                string lblMeduskladisteIzlaz = tabl.Rows[i]["Meduskladisnica sa skladista"].ToString() == "" ? "0" : tabl.Rows[i]["Meduskladisnica sa skladista"].ToString();
                string lblRadniN = tabl.Rows[i]["Radni nalog oduzmi"].ToString() == "" ? "0" : tabl.Rows[i]["Radni nalog oduzmi"].ToString();
                string lblOtpremnica = tabl.Rows[i]["Otpremnice"].ToString() == "" ? "0" : tabl.Rows[i]["Otpremnice"].ToString();
                string lblOtpis = tabl.Rows[i]["Otpis Robe"].ToString() == "" ? "0" : tabl.Rows[i]["Otpis Robe"].ToString();

                string sifra = tabl.Rows[i]["šifra"].ToString();
                string naziv = tabl.Rows[i]["naziv"].ToString();
                decimal ulaz = Convert.ToDecimal(lblKalk) + Convert.ToDecimal(lblPrimka) + Convert.ToDecimal(lblUlazMS) + Convert.ToDecimal(lblPS) + Convert.ToDecimal(lblRadniD);
                decimal izlaz = Convert.ToDecimal(lblOtpis) + Convert.ToDecimal(lblKasa) + Convert.ToDecimal(lblFaktura) + Convert.ToDecimal(lblIzdatnica) + Convert.ToDecimal(lblPovratnica) + Convert.ToDecimal(lblMeduskladisteIzlaz) + Convert.ToDecimal(lblRadniN) + Convert.ToDecimal(lblOtpremnica);
                decimal mpc = Convert.ToDecimal(tabl.Rows[i]["MPC"].ToString());
                decimal nbc = Convert.ToDecimal(tabl.Rows[i]["NBC"].ToString());
                if (fakturna_cijena)
                {
                    nbc = Convert.ToDecimal(tabl.Rows[i]["fak_cijena"].ToString());
                }

                DataRow DTrow = dSRliste.Tables[0].NewRow();
                DTrow["sifra"] = sifra;
                DTrow["naziv"] = naziv;
                DTrow["cijena1"] = ulaz;
                DTrow["cijena2"] = izlaz;
                DTrow["cijena3"] = mpc;
                DTrow["cijena4"] = nbc;
                dSRliste.Tables[0].Rows.Add(DTrow);
            }
        }
    }
}