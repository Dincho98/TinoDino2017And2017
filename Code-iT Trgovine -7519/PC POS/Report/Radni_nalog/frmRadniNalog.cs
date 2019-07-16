using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.Radni_nalog
{
    public partial class repRadniNalog : Form
    {
        public repRadniNalog()
        {
            InitializeComponent();
        }

        public string broj_dokumenta { get; set; }
        public string skladiste { get; set; }
        public string godina { get; set; }

        public string dokumenat { get; set; }
        public string ImeForme { get; set; }

        private void repRadniNalog_Load(object sender, EventArgs e)
        {
            if (broj_dokumenta != null)
            {
                LoadDocument();
            }

            this.reportViewer1.RefreshReport();
        }

        private void LoadDocument()
        {
            string grad_string = "", skontakt = "";

            string sql1 = "SELECT grad,posta, tel, mob, email  FROM grad, radni_nalog" +
                " LEFT JOIN partners ON partners.id_partner=radni_nalog.id_narucioc" +
                " WHERE radni_nalog.broj_naloga='" + broj_dokumenta + "'" +
                " AND partners.id_grad=grad.id_grad";
            DataTable grad = classSQL.select(sql1, "grad").Tables[0];

            if (grad.Rows.Count != 0)
            {
                grad_string = grad.Rows[0]["posta"].ToString().Trim() + " " + grad.Rows[0]["grad"].ToString();

                skontakt += grad.Rows[0]["tel"].ToString().Trim().Length > 0 ? "Tel: " + grad.Rows[0]["tel"].ToString().Trim() : "";
                if (skontakt.Length > 0) skontakt += Environment.NewLine;
                skontakt += grad.Rows[0]["mob"].ToString().Trim().Length > 0 ? "Mob: " + grad.Rows[0]["mob"].ToString().Trim() : "";
                if (skontakt.Length > 0) skontakt += Environment.NewLine;
                skontakt += grad.Rows[0]["email"].ToString().Trim().Length > 0 ? "E-mail: " + grad.Rows[0]["email"].ToString().Trim() : "";
            }

            ReportParameter p1 = new ReportParameter("kontakt_podaci", skontakt.ToString());
            ReportParameter p2 = new ReportParameter("proizvodnja", Class.Postavke.proizvodnja_normativ_pc.ToString());
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });

            sql1 = "SELECT " +
                " p.broj_naloga AS broj," +
                " p.napomena AS napomena," +
                " p.godina_naloga AS godina," +
                " p.datum_naloga," +
                " p.datum_primitka," +
                " p.zavrsna_kartica as datum_zavrsetka," +
                " partners.ime_tvrtke AS kupac_tvrtka," +
                " partners.adresa AS kupac_adresa," +
                " '" + grad_string + "' AS kupac_grad," +
                " partners.id_grad AS kupac_grad_id," +
                " partners.id_partner AS sifra_kupac," +
                " partners.oib AS kupac_oib," +
                " '' AS Naslov," +
                " a.ime + ' ' + a.prezime AS izradio," +
                " case when p.izvrsio is not null and length(p.izvrsio) > 0 then p.izvrsio else b.ime + ' ' + b.prezime end AS izvrsio," +
                " grad.grad AS mjesto" +
                " FROM radni_nalog p" +
                " LEFT JOIN partners ON partners.id_partner=p.id_narucioc" +
                " LEFT JOIN zaposlenici a ON a.id_zaposlenik=p.id_izradio" +
                " LEFT JOIN zaposlenici b ON b.id_zaposlenik=p.id_izvrsio" +
                " LEFT JOIN grad ON grad.id_grad=p.mj_troska" +
                " WHERE p.broj_naloga='" + broj_dokumenta + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql1).Fill(dSRadniNalog, "DTRRadniNalog");
            }
            else
            {
                classSQL.NpgAdatpter(sql1.Replace("nvarchar", "varchar")).Fill(dSRadniNalog, "DTRRadniNalog");
            }

            string sql_liste = "SELECT " +
                " p.sifra_robe AS sifra," +
                " p.vpc," +
                " p.kolicina," +
                " p.nbc," +
                " p.porez," +
                " '0' as iznos_porez," +
                " r.naziv AS naziv_robe," +
                " " + broj_dokumenta + " AS broj_naloga," +
                " r.jm" +
                " FROM roba r, radni_nalog_stavke p" +
                " LEFT JOIN radni_nalog ON radni_nalog.broj_naloga=p.broj_naloga" +
                " WHERE radni_nalog.broj_naloga='" + broj_dokumenta + "'" +
                " AND p.sifra_robe=r.sifra";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRadniNalogStavke, "DTRadniNalogStavke");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRadniNalogStavke, "DTRadniNalogStavke");
            }

            DataTable DT = dSRadniNalogStavke.Tables[0];
            double vpc, kol, vpcKol, pdv;
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                vpc = Math.Round(Convert.ToDouble(DT.Rows[i]["vpc"].ToString()), 2);
                kol = Math.Round(Convert.ToDouble(DT.Rows[i]["kolicina"].ToString()), 3);
                pdv = Math.Round(Convert.ToDouble(DT.Rows[i]["porez"].ToString()), 2);
                vpcKol = Math.Round((vpc + (vpc * (pdv / 100))) * kol, 2);
                DT.Rows[i].SetField("iznos_porez", vpcKol);
                DT.Rows[i].SetField("mpc", Math.Round((vpc + (vpc * (pdv / 100))), 2));
            }

            string id_kupac_grad = "";
            string id_kupac = "";
            if (dSRadniNalog.Tables[0].Rows.Count > 0)
            {
                id_kupac_grad = dSRadniNalog.Tables[0].Rows[0]["kupac_grad_id"].ToString();
                id_kupac = dSRadniNalog.Tables[0].Rows[0]["sifra_kupac"].ToString();
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
            }
        }
    }
}