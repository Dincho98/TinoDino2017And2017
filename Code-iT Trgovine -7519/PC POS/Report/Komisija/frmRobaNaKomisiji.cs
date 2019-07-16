using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.Komisija
{
    public partial class frmRobaNaKomisiji : Form
    {
        private string partnerSifra = "";
        private string partnerNaziv = "";
        private DataSet DSpartner;

        public frmRobaNaKomisiji()
        {
            InitializeComponent();
        }

        private void frmListe_Load(object sender, EventArgs e)
        {
            dtpDatumOd.Value = new DateTime(DateTime.Now.Year, 01, 01, 00, 00, 00);
            dtpDatumDo.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), 23, 59, 59);

            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            dSRpodaciTvrtke.Tables[0].Rows[0]["naziv_fakture"] = "";
            this.reportViewer1.RefreshReport();
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            string nazivPartnera = "SVI";
            int idPartner = 0;
            if (txtPartnerSifra.Text.Trim().Length > 0)
            {

                if (int.TryParse(txtPartnerSifra.Text.Trim(), out idPartner) && idPartner > 0)
                {
                    string sql_partner = string.Format("select case when vrsta_korisnika = 1 then ime_tvrtke else concat(ime, ' ', prezime) end as naziv from partners where id_partner = {0};", idPartner);
                    DataSet dsPartner = classSQL.select(sql_partner, "partners");
                    nazivPartnera = dsPartner.Tables[0].Rows[0]["naziv"].ToString();
                }
                else
                {
                    MessageBox.Show("Krivi partner.");
                    return;
                }
            }

            dSRliste.Clear();

            //OVAJ SQL služi da uzme vrijednosti iz maloprodajnih računa i faktura, te da ih spiji u jednu cijelinu
            //i zbroji sve količine i cijene

            string sql = string.Format(@"select true as ulaz, o.id_skladiste, o.godina_otpremnice::integer as godina, o.broj_otpremnice as broj, o.osoba_partner as id_partner, o.datum, o.id_otpremnica, o.use_nbc, o.partner_poslovnica,
os.sifra_robe as sifra,
round(replace(os.kolicina, ',', '.')::numeric, 6) as kolicina,
round(os.nbc::numeric, 6) as nbc,
round(replace(os.porez, ',', '.')::numeric, 2) as porez,
round(os.vpc, 6) as vpc,
round(replace(os.rabat, ',', '.')::numeric, 6) as rabat
from otpremnice o
left join otpremnica_stavke os on o.broj_otpremnice = os.broj_otpremnice AND o.id_skladiste = os.id_skladiste
where o.oduzmi_iz_skladista = false and o.datum between '{2:yyyy-MM-dd HH:mm:ss}' and '{3:yyyy-MM-dd HH:mm:ss}' and case when {4} = 0 then 1 = 1 else o.osoba_partner = {4} end

union 

select false as ulaz, fs.id_skladiste, f.godina_fakture::integer as godina, f.broj_fakture as broj, f.id_odrediste as id_partner, f.date as datum, id as id_fakture, f.use_nbc, f.partner_poslovnica,
fs.sifra,
round(replace(fs.kolicina, ',', '.')::numeric, 6) as kolicina,
round(fs.nbc::numeric, 6) as nbc,
round(replace(fs.porez, ',', '.')::numeric, 2) as porez,
round(fs.vpc, 6) as vpc,
round(replace(fs.rabat, ',', '.')::numeric, 6) as rabat
from fakture f
left join faktura_stavke fs on f.broj_fakture = fs.broj_fakture and f.id_ducan = fs.id_ducan and f.id_kasa = fs.id_kasa
where f.faktura_za_komisiju = true and f.id_ducan = {0} and f.id_kasa = {1} and f.date between '{2:yyyy-MM-dd HH:mm:ss}' and '{3:yyyy-MM-dd HH:mm:ss}' and case when {4} = 0 then 1 = 1 else f.id_odrediste = {4} end", Class.Postavke.id_default_ducan, Class.Postavke.naplatni_uredaj_faktura, dtpDatumOd.Value, dtpDatumDo.Value, (chbZbirno.Checked ? 0 : idPartner));

            if (chbZbirno.Checked)
            {
                sql = string.Format(@"select 0 as naziv3, '{1}' as naziv2, x.sifra, r.naziv as naziv,
coalesce(sum(case when x.ulaz = true then x.kolicina else 0 end), 0) as cijena1,
coalesce(sum(case when x.ulaz = false then x.kolicina else 0 end), 0) as cijena2,
coalesce(sum(case when x.ulaz = true then x.kolicina else 0 end), 0) - coalesce(sum(case when x.ulaz = false then x.kolicina else 0 end), 0) as cijena3
from(

    {0}

) x
left join roba r on x.sifra = r.sifra
group by x.sifra, r.naziv
order by sifra asc;", sql, nazivPartnera);

            }
            else
            {
                sql = string.Format(@"select p.id_partner as naziv3, p.partner as naziv2, x.sifra, r.naziv as naziv,
coalesce(sum(case when x.ulaz = true then x.kolicina else 0 end), 0) as cijena1,
coalesce(sum(case when x.ulaz = false then x.kolicina else 0 end), 0) as cijena2,
coalesce(sum(case when x.ulaz = true then x.kolicina else 0 end), 0) - coalesce(sum(case when x.ulaz = false then x.kolicina else 0 end), 0) as cijena3
from(

    {0}

) x
left join (select id_partner, case when vrsta_korisnika = 1 then ime_tvrtke else concat(ime, ' ', prezime) end as partner from partners) p on x.id_partner = p.id_partner
left join roba r on x.sifra = r.sifra
group by p.id_partner, p.partner, x.sifra, r.naziv
order by p.partner asc, sifra asc;", sql);

            }

            classSQL.NpgAdatpter(sql.Replace("+", "zbroj")).Fill(dSRliste, "DTliste");

            string filter = string.Format("FILTER-> OD DATUMA: {0} DO DATUMA: {1}",
                dtpDatumOd.Value.ToString("dd.MM.yyyy HH:mm:ss"),
                dtpDatumDo.Value.ToString("dd.MM.yyyy HH:mm:ss"));

            if (chbZbirno.Checked)
            {
                filter += string.Format(" ZBIRNO");
            }
            else
            {
                if (idPartner > 0)
                {
                    filter += string.Format(" ZA PARTNERA: {0}", nazivPartnera);
                }
                else
                {
                    filter += string.Format(" PO PATNERIMA");
                }
            }

            dSRpodaciTvrtke.Tables[0].Rows[0]["naziv_fakture"] = filter;

            this.reportViewer1.RefreshReport();
        }

        private void btnPonistiPartnera_Click(object sender, EventArgs e)
        {
            txtPartnerNaziv.Text = txtPartnerSifra.Text = "";
        }

        private void chbZbirno_CheckedChanged(object sender, EventArgs e)
        {

            if (chbZbirno.Checked)
            {
                if (txtPartnerSifra.Text.Length > 0)
                {
                    partnerSifra = txtPartnerSifra.Text;
                    partnerNaziv = txtPartnerNaziv.Text;
                }
                else
                {
                    partnerSifra = partnerNaziv = "";
                }

                txtPartnerSifra.Text = txtPartnerNaziv.Text = "";

                txtPartnerSifra.Enabled = false;
                txtPartnerNaziv.Enabled = false;
                btnPartner.Enabled = false;
                btnPonistiPartnera.Enabled = false;
            }
            else
            {
                if (partnerSifra.Length > 0)
                {
                    txtPartnerSifra.Text = partnerSifra;
                    txtPartnerNaziv.Text = partnerNaziv;
                }
                txtPartnerSifra.Enabled = true;
                txtPartnerNaziv.Enabled = true;
                btnPartner.Enabled = true;
                btnPonistiPartnera.Enabled = true;
            }
        }

        private void btnPartner_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DSpartner = new DataSet();
                DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (DSpartner.Tables[0].Rows.Count > 0)
                {
                    txtPartnerSifra.Text = DSpartner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartnerNaziv.Text = DSpartner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void txtPartnerSifra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtPartnerSifra.Text == "")
                {
                    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                    partnerTrazi.ShowDialog();
                    if (Properties.Settings.Default.id_partner != "")
                    {
                        DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                        if (DSpartner.Tables[0].Rows.Count > 0)
                        {
                            txtPartnerSifra.Text = DSpartner.Tables[0].Rows[0]["id_partner"].ToString();
                            txtPartnerNaziv.Text = DSpartner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }

                string Str = txtPartnerSifra.Text.Trim();
                int Num;
                bool isNum = int.TryParse(Str, out Num);

                DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner = '" + Num + "'", "partners");
                if (DSpartner.Tables[0].Rows.Count > 0)
                {
                    txtPartnerNaziv.Text = DSpartner.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }
    }
}