using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PCPOS.Report.Servis
{
    public partial class repServis : Form
    {
        public repServis()
        {
            InitializeComponent();
        }

        public string broj_dokumenta { get; set; }
        public int godina { get; set; }
        public string ImeForme { get; set; }

        public Dataset.DSFaktura ispisdSFaktura;
        public Dataset.DSRfakturaStavke ispisdSRfakturaStavke;
        public Dataset.DSRpodaciTvrtke ispisdSRpodaciTvrtke;

        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private void repFaktura_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

            this.Text = ImeForme;

            string root = "";
            if (DTpostavke.Rows[0]["logo"].ToString() == "1")
            {
                root = DTpostavke.Rows[0]["logopath"].ToString();
            }
            else
            {
                string localPath = Path.GetDirectoryName(Application.ExecutablePath);
                root = localPath + "\\bijela.jpg";
            }

            string tekst = "";
            if (DTpostavke.Rows[0]["sustav_pdv"].ToString() == "0")
            {
                tekst = "PDV nije uračunat u cijenu prema\r\nčl.90. st.1. i st.2. NN 106/18 zakona o PDV-u.\r\n";
            }
            else
            {
                tekst = " ";
            }
            ReportParameter p1 = new ReportParameter("path", root);
            ReportParameter p2 = new ReportParameter("drzava", "Hrvatska");
            ReportParameter p3 = new ReportParameter("sustav_pdv", tekst);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3 });

            FillRNS(broj_dokumenta, godina);

            this.reportViewer1.RefreshReport();
        } 

        private void FillRNS(string broj, int godina)
        {
            string sql2 = @"SELECT
COALESCE(to_char(servis_status.datum, 'YYYY.MM.DD HH24:MI:SS'), '') as broj_fakture,
case when servis_status.status = 0 then 'Nulti servis' else
    case when servis_status.status = 1 then 'Zaprimljeno' else
        case when servis_status.status = 2 then 'Servis u toku' else
            case when servis_status.status = 3 then 'Na vanjskom servisu' else
                case when servis_status.status = 4 then 'Završen servis' else 'Povrat kupcu' end
            end
        end
    end
end as naziv,
servis_status.napomena as sifra
FROM servis_status
LEFT JOIN servis ON servis.id = servis_status.id_servis WHERE servis.servisna_primka = '" + broj + "' and servis.godina = '" + godina + @"'
order by servis_status.datum asc; ";

            //DSRfakturaStavke DSfs = new DSRfakturaStavke();

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }
            else
            {
                classSQL.NpgAdatpter(sql2).Fill(dSRfakturaStavke, "DTfakturaStavke");
            }

            DataTable DT = dSRfakturaStavke.Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
            }

            string sql = "SELECT " +
                " radni_nalog_servis.broj," +
                " radni_nalog_servis.date AS datum," +
                " radni_nalog_servis.vrijedi_do AS datum_dvo," +
                " radni_nalog_servis.vrijedi_do AS datum_valute," +
                " '' AS placanje," +
                " otprema.naziv AS otprema," +
                " CAST (radni_nalog_servis.model AS nvarchar) + '  '+ CAST (radni_nalog_servis.broj AS nvarchar)+" +
                " CAST (radni_nalog_servis.godina AS nvarchar)+'-'+CAST (radni_nalog_servis.id_fakturirati AS nvarchar) AS model," +
                " radni_nalog_servis.napomena," +
                //" '" + rabatSve + "' AS rabat," +
                //" '" + sveUkupno + "' AS ukupno," +
                //" '" + pdvUkupno + "' AS iznos_pdv," +
                //" '" + osnovicaUkupno + "' AS osnovica," +
                " radni_nalog_servis.godina," +
                " CAST (radni_nalog_servis.broj AS nvarchar) +'/'+ CAST (radni_nalog_servis.godina AS nvarchar) AS Naslov," +
                " partners.ime_tvrtke AS kupac_tvrtka," +
                " 'Vrijedi do:' AS naziv_date1," +
                " partners.adresa AS kupac_adresa," +
                " partners.id_grad AS id_kupac_grad," +
                " partners.id_partner AS sifra_kupac," +
                " partners.napomena AS napomena_tvrtka," +
                " partners.oib_polje AS string5, " +
                " zemlja.zemlja AS kupac_drzava, " +
                " ziro_racun.ziroracun AS zr," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio," +
                " ziro_racun.banka AS banka," +
                //" '" + broj_slovima + "' AS broj_slovima," +
                " partners.oib AS kupac_oib" +
                " FROM radni_nalog_servis" +
                " LEFT JOIN partners ON partners.id_partner=radni_nalog_servis.id_fakturirati" +
                " LEFT JOIN otprema ON otprema.id_otprema=radni_nalog_servis.otprema" +
                " LEFT JOIN nacin_placanja ON nacin_placanja.id_placanje=radni_nalog_servis.id_nacin_placanja" +
                " LEFT JOIN zemlja ON zemlja.id_zemlja=partners.id_zemlja" +
                " LEFT JOIN ziro_racun ON ziro_racun.id_ziroracun=radni_nalog_servis.zr" +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=radni_nalog_servis.id_zaposlenik_izradio WHERE radni_nalog_servis.broj='" + broj + "'" +
                "";

            sql = @"select s.servisna_primka as broj_racuna,
s.godina as godina,
s.seriski_broj as string1,
s.sifra as string2,
s.naziv as string3,
case when p.vrsta_korisnika = 1 then p.ime_tvrtke else concat(p.ime, ' ', p.prezime) end AS kupac_tvrtka,
p.adresa AS kupac_adresa,
p.id_grad AS id_kupac_grad,
p.id_partner AS sifra_kupac,
p.napomena AS napomena_tvrtka,
p.tel as jir,
p.mob as zki,
p.oib_polje AS string5,
z.zemlja AS kupac_drzava,
p.oib AS kupac_oib,
za.ime + ' ' + za.prezime AS izradio
from servis s
left join partners p on s.partner = p.id_partner
left join zemlja z ON z.id_zemlja = p.id_zemlja
LEFT JOIN zaposlenici za ON za.id_zaposlenik = s.izradio
where s.servisna_primka = '" + broj + "' and s.godina = '" + godina + "';";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql).Fill(dSFaktura, "DTRfaktura");
            }
            else
            {
                classSQL.NpgAdatpter(sql.Replace("nvarchar", "varchar")).Fill(dSFaktura, "DTRfaktura");
            }

            string id_kupac = "";
            if (dSFaktura.Tables[0].Rows.Count > 0)
            {
                id_kupac = dSFaktura.Tables[0].Rows[0]["id_kupac_grad"].ToString();
            }
            else
            {
                MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //return;
            }

            //string drzava = dSRpodaciTvrtke.Tables[0].Rows[0]["id_kupac_grad"].ToString();
            string naziv_fakture = " 'Servisna primka ' AS naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql(id_kupac, naziv_fakture, "");

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }
            else
            {
                classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }

            PostaviPodatkeOBanci(ref dSRpodaciTvrtke, ref dSFaktura);
            //DateTime datum_ = Convert.ToDateTime(dSFaktura.Tables[0].Rows[0]["datum"].ToString());
            reportViewer1.LocalReport.DisplayName = "Servisna primka - " + dSFaktura.Tables[0].Rows[0]["broj_racuna"].ToString();
        }

        #region Util

        private void PostaviPodatkeOBanci(ref Dataset.DSRpodaciTvrtke dSRpodaciTvrtke, ref Dataset.DSFaktura dSFaktura)
        {
            string ziro = dSRpodaciTvrtke.Tables[0].Rows[0]["zr"].ToString();
            string iban = dSRpodaciTvrtke.Tables[0].Rows[0]["iban"].ToString();
            dSFaktura.Tables[0].Rows[0].SetField("iban", iban);
            string swift = dSRpodaciTvrtke.Tables[0].Rows[0]["swift"].ToString();
            dSFaktura.Tables[0].Rows[0].SetField("swift", swift);

            string banka = "";
            DataTable dt = classSQL.select("SELECT banka FROM ziro_racun WHERE ziroracun = '" + ziro + "'", "ziro_racun").Tables[0];
            if (dt.Rows.Count > 0)
            {
                banka = dt.Rows[0]["banka"].ToString();
                dSFaktura.Tables[0].Rows[0].SetField("banka", banka);
            }
            else
            {
            }
        }

        #endregion Util
    }
}