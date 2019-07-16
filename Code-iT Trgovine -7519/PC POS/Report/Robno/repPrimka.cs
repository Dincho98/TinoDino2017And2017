using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.Robno
{
    public partial class repPrimka : Form
    {
        public repPrimka()
        {
            InitializeComponent();
        }

        public string broj_dokumenta { get; set; }
        public string broj_skladista { get; set; }
        public string skladiste { get; set; }
        public string godina { get; set; }

        public string dokumenat { get; set; }
        public string ImeForme { get; set; }
        public bool proizvodacka_cijena { get; internal set; }

        private void repPrimka_Load(object sender, EventArgs e)
        {
            if (broj_dokumenta != null)
            {
                LoadDocument();
            }

            this.reportViewer1.RefreshReport();
        }

        private void LoadDocument()
        {
            string grad_string = "";
            DataTable grad = classSQL.select("SELECT grad,posta FROM grad, primka" +
                " LEFT JOIN partners ON partners.id_partner=primka.id_partner" +
                " WHERE primka.broj='" + broj_dokumenta + "' AND primka.id_skladiste = '" + broj_skladista + "'" +
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
                " grad.grad AS mjesto," +
                " skladiste.id_skladiste," +
                " skladiste.skladiste" +
                " FROM primka p" +
                " LEFT JOIN partners ON partners.id_partner=p.id_partner" +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=p.id_izradio" +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=p.id_skladiste" +
                " LEFT JOIN grad ON grad.id_grad=p.id_mjesto" +
                " WHERE p.broj='" + broj_dokumenta + "' AND p.id_skladiste = '" + broj_skladista + "'";

            //primka broj + skladište ili id_primka???

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql1).Fill(dSPrimka, "DTRPrimka");
            }
            else
            {
                classSQL.NpgAdatpter(sql1.Replace("nvarchar", "varchar")).Fill(dSPrimka, "DTRPrimka");
            }

            string sql_liste = "SELECT " +
                " p.sifra," +
                " p.vpc," +
                " p.mpc," +
                " p.rabat," +
                " p.broj," +
                " p.kolicina," +
                " p.nbc," +
                " p.pdv," +
                " p.ukupno," +
                " r.naziv AS naziv_robe," +
                " p.proizvodacka_cijena," +
                " r.jm" +
                " FROM roba r, primka_stavke p" +
                " LEFT JOIN primka ON primka.id_primka=p.id_primka" +
                " WHERE primka.broj='" + broj_dokumenta + "' AND primka.id_skladiste = '" + broj_skladista + "'" +
                " AND p.sifra=r.sifra";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSPrimkaStavke, "DTPrimkaStavke");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSPrimkaStavke, "DTPrimkaStavke");
            }

            DataTable dt = dSPrimkaStavke.Tables[0];

            decimal iznosBezPoreza, sumIznosBezPoreza, uk, sumUk, pdv, kol = 0;
            string rabat;
            sumIznosBezPoreza = 0;
            sumUk = 0;
            double proizvocaka_cijena_ukupno = 0, kolicina_ukupno = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                uk = Math.Round(Convert.ToDecimal(dt.Rows[i]["ukupno"].ToString()), 2);
                pdv = Math.Round(Convert.ToDecimal(dt.Rows[i]["pdv"].ToString().Replace(".", ",")), 2);

                iznosBezPoreza = uk / (1 + pdv / 100);

                rabat = Convert.ToDouble(dt.Rows[i]["rabat"].ToString()) == 0 ? "0" : dt.Rows[i]["rabat"].ToString();

                dt.Rows[i].SetField("iznosBezPoreza", Math.Round(iznosBezPoreza, 2));
                dt.Rows[i].SetField("rabat", rabat);

                sumIznosBezPoreza += iznosBezPoreza;
                sumUk += uk;
            }

            for (int i = 0; i < dSPrimkaStavke.Tables[0].Rows.Count; i++)
            {
                double proizvodacka_cijena = 0, kolicina = 0, nbc = 0;
                double.TryParse(dSPrimkaStavke.Tables[0].Rows[i]["proizvodacka_cijena"].ToString(), out proizvodacka_cijena);
                double.TryParse(dSPrimkaStavke.Tables[0].Rows[i]["nbc"].ToString(), out nbc);
                double.TryParse(dSPrimkaStavke.Tables[0].Rows[i]["kolicina"].ToString(), out kolicina);

                double iznos_proizvodacke_cijene = Math.Round((proizvodacka_cijena * kolicina), 2);

                if (this.proizvodacka_cijena)
                {
                    dt.Rows[i].SetField("iznosBezPoreza", iznos_proizvodacke_cijene);
                    dt.Rows[i].SetField("ukupno", Math.Round((nbc * kolicina), 2));
                }

                kolicina_ukupno += kolicina;
                proizvocaka_cijena_ukupno += iznos_proizvodacke_cijene;
            }

            ReportParameter p1 = new ReportParameter("proizvodacka_cijena", proizvocaka_cijena_ukupno.ToString("#,##0.00"));
            ReportParameter p2 = new ReportParameter("kolicina_ukupno", kolicina_ukupno.ToString("#,##0.00"));
            ReportParameter p3 = new ReportParameter("ispis_proizvodnja", proizvodacka_cijena.ToString());

            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3 });

            string id_kupac_grad = "";
            string id_kupac = "";
            if (dSPrimka.Tables[0].Rows.Count > 0)
            {
                id_kupac_grad = dSPrimka.Tables[0].Rows[0]["kupac_grad_id"].ToString();
                id_kupac = dSPrimka.Tables[0].Rows[0]["sifra_kupac"].ToString();
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
    }
}