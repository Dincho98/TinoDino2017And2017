using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.PopisRobe
{
    public partial class frmPopisRobe : Form
    {
        public frmPopisRobe()
        {
            InitializeComponent();
        }

        public string sort { get; set; }
        public string ime_partnera { get; set; }
        public string sifra_partnera { get; set; }
        public string ducan { get; set; }
        public string kasa { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }

        private void frmListe_Load(object sender, EventArgs e)
        {
            tdOdDatuma.Value = new DateTime(DateTime.Now.Year, 01, 01, 00, 00, 00);

            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            dSRpodaciTvrtke.Tables[0].Rows[0]["naziv_fakture"] = "";
            this.reportViewer1.RefreshReport();
        }

        private DataSet DSpartner;

        private void txtSifra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtSifra.Text == "")
                {
                    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                    partnerTrazi.ShowDialog();
                    if (Properties.Settings.Default.id_partner != "")
                    {
                        DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                        if (DSpartner.Tables[0].Rows.Count > 0)
                        {
                            txtSifra.Text = DSpartner.Tables[0].Rows[0]["id_partner"].ToString();
                            txtNaziv.Text = DSpartner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtSifra.Select();
                        }
                    }
                    else
                    {
                        txtSifra.Select();
                        return;
                    }
                }

                string Str = txtSifra.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtSifra.Text = "0";
                }
                DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner='" + txtSifra.Text + "'", "partners");
                if (DSpartner.Tables[0].Rows.Count > 0)
                {
                    txtNaziv.Text = DSpartner.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
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
                    txtSifra.Text = DSpartner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtNaziv.Text = DSpartner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            string uvijet_partnerR = "";
            string uvijet_partnerF = "";

            string SifraArtiklaR = "";
            string SifraArtiklaF = "";

            if (txtSifra.Text != "")
            {
                uvijet_partnerR = " AND racuni.id_kupac='" + txtSifra.Text + "'";
                uvijet_partnerF = " AND fakture.id_odrediste='" + txtSifra.Text + "'";
            }

            if (txtSifraArtikla.Text != "")
            {
                SifraArtiklaR = " AND racun_stavke.sifra_robe='" + txtSifraArtikla.Text + "'";
                SifraArtiklaF = " AND faktura_stavke.sifra='" + txtSifraArtikla.Text + "'";
            }

            dSsaldaKonti.Clear();

            //OVAJ SQL služi da uzme vrijednosti iz maloprodajnih računa i faktura, te da ih spiji u jednu cijelinu
            //i zbroji sve količine i cijene

            string sql = @"SELECT sifra_robe as string1,naziv as string2,SUM(kolicina) as numeric1,SUM(vpc) as numeric2,SUM(cijena) as numeric3,id_partner,ime_tvrtke FROM
                            (
                            SELECT racun_stavke.sifra_robe,roba.naziv,id_kupac as id_partner,partners.ime_tvrtke,

                            ROUND(COALESCE(SUM(CAST(REPLACE(racun_stavke.kolicina,',','.') AS DECIMAL)),0),2) AS kolicina,
                            ROUND(SUM(COALESCE(CAST(REPLACE(racun_stavke.kolicina,',','.') AS DECIMAL),0)*COALESCE(CAST(racun_stavke.mpc AS NUMERIC),0)),2) AS cijena,
                            ROUND(SUM(COALESCE(CAST(REPLACE(racun_stavke.kolicina,',','.') AS DECIMAL),0)*COALESCE(CAST(racun_stavke.vpc AS NUMERIC),0)),2) AS vpc
                            FROM racun_stavke

                            LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe
                            LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa
                            LEFT JOIN partners ON partners.id_partner=racuni.id_kupac
                            WHERE racuni.datum_racuna >= '" + tdOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + @"'  AND  racuni.datum_racuna <='" + tdDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + @"' " + uvijet_partnerR + SifraArtiklaR + @"
                            GROUP BY racun_stavke.sifra_robe,roba.naziv,id_kupac,ime_tvrtke

                            UNION ALL

                            SELECT faktura_stavke.sifra,roba.naziv,id_odrediste AS id_partner,partners.ime_tvrtke,

                            ROUND(COALESCE(SUM(CAST(REPLACE(faktura_stavke.kolicina,',','.') AS DECIMAL)),0),2) AS kolicina,

                            ROUND(SUM(COALESCE(CAST(REPLACE(faktura_stavke.kolicina,',','.') AS DECIMAL),0)*COALESCE(
                            CAST(faktura_stavke.vpc AS NUMERIC)*(1zbroj(CAST(REPLACE(faktura_stavke.porez,',','.') AS DECIMAL)/100))
                            ,0)),2) AS cijena,
                            ROUND(SUM(COALESCE(CAST(REPLACE(faktura_stavke.kolicina,',','.') AS DECIMAL),0)*COALESCE(
                            CAST(faktura_stavke.vpc AS NUMERIC),0)),2) AS vpc
                            FROM faktura_stavke
                            LEFT JOIN roba ON roba.sifra=faktura_stavke.sifra
                            LEFT JOIN fakture ON fakture.broj_fakture=faktura_stavke.broj_fakture AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa
                            LEFT JOIN partners ON partners.id_partner=fakture.id_odrediste
                            WHERE date>='" + tdOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + @"' AND date<='" + tdDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + @"' " + uvijet_partnerF + SifraArtiklaF + @"
                            GROUP BY faktura_stavke.sifra,roba.naziv,id_odrediste,ime_tvrtke
                            ) AS popis_robe
                            GROUP BY sifra_robe,naziv,id_partner,ime_tvrtke
order by case when sifra_robe ~ '\D' then 1000000000::int ELSE to_number(sifra_robe, '999999999')::int END, sifra_robe;";

            classSQL.NpgAdatpter(sql).Fill(dSsaldaKonti, "DTsaldaKonti");

            dSRpodaciTvrtke.Tables[0].Rows[0]["naziv_fakture"] = "FILTER-> OD DATUMA: " + tdOdDatuma.Value.ToString("dd.MM.yyyy. HH:mm:ss") + "  DO DATUMA: " + tdDoDatuma.Value.ToString("dd.MM.yyyy. HH:mm:ss");

            this.reportViewer1.RefreshReport();
        }
    }
}