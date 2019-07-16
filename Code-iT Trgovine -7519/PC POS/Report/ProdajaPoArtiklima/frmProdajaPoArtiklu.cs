using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.ProdajaPoArtiklima
{
    public partial class frmProdajaPoArtiklu : Form
    {
        public frmProdajaPoArtiklu()
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
            tdDoDatuma.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), 23, 59, 59);

            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            dSRpodaciTvrtke.Tables[0].Rows[0]["naziv_fakture"] = "";
            this.reportViewer1.RefreshReport();
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            string SifraArtiklaR = "";
            string SifraArtiklaF = "";
            string nazivPartnera = "SVI";

            if (txtSifraArtikla.Text.Trim().Length > 0)
            {
                string sql_q = "select case when vrsta_korisnika = 1 then ime_tvrtke else concat(ime, ' ', prezime) end as naziv from partners where id_partner = '" + txtSifraArtikla.Text.Trim() + "'";
                DataSet dsPartner = classSQL.select(sql_q, "partners");

                if (dsPartner != null && dsPartner.Tables.Count > 0 && dsPartner.Tables[0].Rows.Count > 0)
                {
                    nazivPartnera = dsPartner.Tables[0].Rows[0]["naziv"].ToString();
                }
                else
                {
                    MessageBox.Show("Krivi partner.");
                    return;
                }
            }

            if (txtSifraArtikla.Text != "")
            {
                SifraArtiklaR = " AND racun_stavke.sifra_robe in (select sifra from roba where id_partner = '" + txtSifraArtikla.Text + "') ";
                SifraArtiklaF = " AND faktura_stavke.sifra in (select sifra from roba where id_partner = '" + txtSifraArtikla.Text + "') ";
            }

            dSsaldaKonti.Clear();

            //OVAJ SQL služi da uzme vrijednosti iz maloprodajnih računa i faktura, te da ih spiji u jednu cijelinu
            //i zbroji sve količine i cijene

            string sql = @"SELECT
	                        prodano.sifra as string1,
	                        ROUND(SUM(prodano.kolicina),3) as numeric1,
	                        ROUND(AVG(prodano.vpc),3) as numeric2,
	                        ROUND(AVG(prodano.mpc),3) as numeric3,
	                        doc as string3,
                            roba.naziv as string2
                        FROM
                        (
	                        /*RACUNI*/
	                        SELECT sifra_robe as sifra,
	                        CAST(REPLACE(kolicina,',','.') AS numeric) as kolicina,
	                        CAST(mpc AS numeric)/(1+(CAST(REPLACE(porez,',','.') AS numeric)/100)) as vpc,
	                        CAST(mpc AS numeric) as mpc,
	                        'RACUN' as doc,
	                        racuni.datum_racuna as datum
	                        FROM racun_stavke
	                        LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa
                            @uvijet_racun
	                        UNION ALL
	                        /*FAKTURA*/
	                        SELECT sifra as sifra,
	                        CAST(REPLACE(kolicina,',','.') AS numeric) as kolicina,
	                        vpc as vpc,
	                        vpc*(1+(CAST(REPLACE(porez,',','.') AS numeric)/100)) as mpc,
	                        'FAKTURA' as doc,fakture.date as datum
	                        FROM faktura_stavke
	                        LEFT JOIN fakture ON fakture.broj_fakture = faktura_stavke.broj_fakture AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa
                            @uvijet_faktura
                        )
                        prodano
                        LEFT JOIN roba ON roba.sifra=prodano.sifra
                        GROUP BY prodano.sifra,doc,naziv";

            //tdOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss")

            sql = sql.Replace("@uvijet_racun", " WHERE racuni.datum_racuna>='" + tdOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND racuni.datum_racuna<='" + tdDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "' " + SifraArtiklaR + "");
            sql = sql.Replace("@uvijet_faktura", " WHERE fakture.date>='" + tdOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND fakture.date<='" + tdDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "' " + SifraArtiklaF + "");

            classSQL.NpgAdatpter(sql.Replace("+", "zbroj")).Fill(dSsaldaKonti, "DTsaldaKonti");

            dSRpodaciTvrtke.Tables[0].Rows[0]["naziv_fakture"] = string.Format("FILTER-> OD DATUMA: {0} DO DATUMA: {1} ZA PARTNER: {2}",
                tdOdDatuma.Value.ToString("dd.MM.yyyy HH:mm:ss"),
                tdDoDatuma.Value.ToString("dd.MM.yyyy HH:mm:ss"),
                nazivPartnera);

            this.reportViewer1.RefreshReport();
        }
    }
}