using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Report.Robno
{
    public partial class repIzdatnica : Form
    {
        public repIzdatnica()
        {
            InitializeComponent();
        }

        public DataRow RowPdv { get; set; }

        public string broj_dokumenta { get; set; }
        public string broj_skladista { get; set; }
        public string skladiste { get; set; }
        public string godina { get; set; }

        public string dokumenat { get; set; }
        public string ImeForme { get; set; }

        private void repIzdatnica_Load(object sender, EventArgs e)
        {
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);

            if (broj_dokumenta != null)
            {
                LoadDocument();
            }

            ReportDataSource stopeDataSource = new ReportDataSource();

            stopeDataSource.Name = "DSstope";
            stopeDataSource.Value = dSstope.Tables[0];
            reportViewer1.LocalReport.DataSources.Add(stopeDataSource);

            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Robno.izdatnica.rdlc";
            this.reportViewer1.LocalReport.EnableExternalImages = true;

            this.reportViewer1.RefreshReport();
        }

        private void LoadDocument()
        {
            string grad_string = "";
            DataTable grad = classSQL.select("SELECT grad,posta FROM grad, izdatnica" +
                " LEFT JOIN partners ON partners.id_partner=izdatnica.id_partner" +
                " WHERE izdatnica.broj='" + broj_dokumenta + "' AND izdatnica.id_skladiste = '" + broj_skladista + "'" +
                " And partners.id_grad=grad.id_grad", "grad").Tables[0];
            if (grad.Rows.Count != 0)
            {
                grad_string = grad.Rows[0]["posta"].ToString().Trim() + " " + grad.Rows[0]["grad"].ToString();
            }

            string sql1 = "SELECT " +
                " p.broj AS broj," +
                " p.originalni_dokument," +
                " p.datum," +
                " p.napomena AS napomena," +
                " p.godina," +
                " partners.ime_tvrtke AS kupac_tvrtka," +
                " partners.adresa AS kupac_adresa," +
                " '" + grad_string + "' AS kupac_grad," +
                " partners.id_grad AS kupac_grad_id," +
                " partners.id_partner AS sifra_kupac," +
                " partners.oib AS kupac_oib," +
                " '' AS Naslov," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio," +
                " grad.grad as mjesto," +
                " skladiste.id_skladiste," +
                " skladiste.skladiste" +
                " FROM izdatnica p" +
                " LEFT JOIN partners ON partners.id_partner=p.id_partner" +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=p.id_izradio" +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=p.id_skladiste" +
                " LEFT JOIN grad ON grad.id_grad=p.id_mjesto" +
                " WHERE p.broj='" + broj_dokumenta + "' AND p.id_skladiste = '" + broj_skladista + "'";

            //izdatnica broj + skladište ili id_izdatnica???

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql1).Fill(dSIzdatnica, "DTRIzdatnica");
            }
            else
            {
                classSQL.NpgAdatpter(sql1.Replace("nvarchar", "varchar")).Fill(dSIzdatnica, "DTRIzdatnica");
            }

            string sql_liste = "SELECT " +
                " p.sifra," +
                " p.vpc," +
                " p.mpc," +
                " p.rabat," +
                " p.broj," +
                " CAST(p.kolicina AS numeric)," +
                " p.nbc," +
                " CAST(p.pdv as numeric)," +
                " p.ukupno," +
                " r.naziv AS naziv_robe," +
                " r.jm" +
                " FROM roba r, izdatnica_stavke p" +
                " LEFT JOIN izdatnica ON izdatnica.id_izdatnica=p.id_izdatnica" +
                " WHERE izdatnica.broj='" + broj_dokumenta + "' AND izdatnica.id_skladiste = '" + broj_skladista + "'" +
                " AND p.sifra=r.sifra";


            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSIzdatnicaStavke, "DTIzdatnicaStavke");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSIzdatnicaStavke, "DTIzdatnicaStavke");
            }

            DataTable dt = dSIzdatnicaStavke.Tables[0];

            decimal iznosBezPoreza = 0;
            decimal sumIznosBezPoreza = 0;
            decimal uk = 0;
            decimal sumUk = 0;
            decimal nbc = 0;
            decimal vpc = 0;
            decimal fakturna_sa_pdv = 0;
            decimal pdv_iznos = 0;
            decimal rabat = 0;
            sumIznosBezPoreza = 0;
            sumUk = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                uk = Math.Round(Convert.ToDecimal(dt.Rows[i]["ukupno"].ToString()), 2);
                nbc = Math.Round(Convert.ToDecimal(dt.Rows[i]["nbc"].ToString()), 2);
                vpc = Math.Round(Convert.ToDecimal(dt.Rows[i]["vpc"].ToString()), 2);
                decimal pdv;
                decimal.TryParse(dt.Rows[i]["pdv"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out pdv);
                decimal kolicina = Convert.ToDecimal(dt.Rows[i]["kolicina"].ToString());
                if (pdv != 0)
                    pdv_iznos = nbc * (pdv / 100);
                fakturna_sa_pdv = nbc + pdv_iznos; 
                iznosBezPoreza = uk - pdv_iznos;

                //rabat = Convert.ToDouble(dt.Rows[i]["rabat"].ToString()) == 0 ? "" : dt.Rows[i]["rabat"].ToString();
                rabat = Convert.ToDecimal(dt.Rows[i]["rabat"]);

                decimal marza = (vpc - nbc) * kolicina;
                decimal pdvStavka_cisto = (nbc) * (pdv / 100);
                decimal pdvVpc = (vpc * kolicina) * (pdv / 100);

                dt.Rows[i].SetField("marza", marza);
                dt.Rows[i].SetField("vpc_pdv", pdvVpc);
                dt.Rows[i].SetField("vpc", vpc * kolicina);
                dt.Rows[i].SetField("iznosBezPoreza", Math.Round(iznosBezPoreza, 2));
                dt.Rows[i].SetField("rabat", rabat);
                dt.Rows[i].SetField("pdv_iznos", Math.Round(pdv_iznos, 2));
                dt.Rows[i].SetField("fakturna_sa_pdv", fakturna_sa_pdv);

                sumIznosBezPoreza += iznosBezPoreza;
                sumUk += uk;

                StopePDVa(Math.Round(pdv, 4), Math.Round(pdvStavka_cisto, 4), Math.Round(nbc, 4), "nbc");
                StopePDVa(Math.Round(pdv, 4), Math.Round(pdvVpc, 4), Math.Round(vpc * kolicina, 4), "mpc");
            }

            string id_kupac_grad = "";
            string id_kupac = "";
            if (dSIzdatnica.Tables[0].Rows.Count > 0)
            {
                id_kupac_grad = dSIzdatnica.Tables[0].Rows[0]["kupac_grad_id"].ToString();
                id_kupac = dSIzdatnica.Tables[0].Rows[0]["sifra_kupac"].ToString();
            }
            else
            {
                MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            sql1 = SqlPodaciTvrtke.VratiSql(id_kupac_grad, "", id_kupac);

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }
            else
            {
                classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
                //classSQL.NpgAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }
        }

        private void StopePDVa(decimal pdv, decimal iznos, decimal osnovica, string vrsta)
        {
            DataRow[] dataROW = dSstope.Tables["DTstope"].Select("stopa = '" + Math.Round(pdv).ToString() + "' AND vrsta = '" + vrsta + "'");

            if (dataROW.Count() == 0)
            {
                RowPdv = dSstope.Tables["DTstope"].NewRow();
                RowPdv["stopa"] = Math.Round(pdv, 0);
                RowPdv["iznos"] = Math.Round(iznos, 3);
                RowPdv["osnovica"] = Math.Round(osnovica, 3);
                RowPdv["vrsta"] = vrsta;
                dSstope.Tables["DTstope"].Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["iznos"] = Convert.ToDecimal(dataROW[0]["iznos"].ToString()) + Math.Round(iznos, 3);
                dataROW[0]["osnovica"] = Convert.ToDecimal(dataROW[0]["osnovica"].ToString()) + Math.Round(osnovica, 3);
            }
        }
    }
}