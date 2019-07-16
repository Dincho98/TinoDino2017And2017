using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.UlazIzlaz
{
    public partial class frmSklulazizlaz : Form
    {
        public frmSklulazizlaz()
        {
            InitializeComponent();
        }

        public string documenat { get; set; }
        public string broj_dokumenta { get; set; }
        public string dokumenat { get; set; }
        public string ImeForme { get; set; }
        public string blagajnik { get; set; }
        public string skladiste { get; set; }
        public string skladiste_odabir { get; set; }
        public string artikl { get; set; }
        public string ducan { get; set; }
        public int skladiste_brojac { get; set; }
        public Boolean bool1 { get; set; }
        public Boolean bool2 { get; set; } // pomoćni bit
        public int brojac { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }

        public string BrojFakOD { get; set; }
        public string BrojFakDO { get; set; }

        private void frmListe_Load(object sender, EventArgs e)
        {
            //Sintetika_po_skladisnim_cijenama_sva_skladista();

            //Ulaz();
            UlazTest();

            this.reportViewer1.RefreshReport();
        }

        private void UlazTest()
        {
            string sqlpodaci = "SELECT " +
                " podaci_tvrtka.ime_tvrtke," +
                " podaci_tvrtka.skraceno_ime," +
                " podaci_tvrtka.oib," +
                " podaci_tvrtka.tel," +
                " podaci_tvrtka.fax," +
                " podaci_tvrtka.mob," +
                " podaci_tvrtka.iban," +
                " podaci_tvrtka.adresa," +
                " podaci_tvrtka.vl," +
                " podaci_tvrtka.poslovnica_adresa," +
                " podaci_tvrtka.poslovnica_grad," +
                " podaci_tvrtka.email," +
                " podaci_tvrtka.zr," +
                " podaci_tvrtka.naziv_fakture," +
                " podaci_tvrtka.text_bottom," +
                " grad.grad + '' + grad.posta AS grad" +
                " FROM podaci_tvrtka" +
                " LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad" +
                "";
            classSQL.CeAdatpter(sqlpodaci).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            frmObracunporeza obrporez = new frmObracunporeza();
            string sql = "SELECT * FROM skladiste ORDER BY id_skladiste ASC";
            DataTable DTskl = classSQL.select(sql, "SKL").Tables[0];

            DataTable DTizlazna_fak = new DataTable();
            DataTable MS_ulaz = new DataTable();
            DataTable MS_izlaz = new DataTable();
            DataTable DTkalk = new DataTable();
            DataTable DTOtpremnica = new DataTable();
            DataTable DTradni_nalog = new DataTable();
            DataTable DTpocetno_stanje = new DataTable();
            DataTable DTpovratnica_dobavljacu = new DataTable();
            DataTable DTprimka = new DataTable();
            DataTable DTmpr = new DataTable();

            DataTable DTrobaprodaja = classSQL.select("SELECT DISTINCT roba_prodaja.sifra, roba.naziv,roba_prodaja.vpc,roba_prodaja.porez FROM roba_prodaja " +
                " LEFT JOIN roba on roba_prodaja.sifra = roba.sifra WHERE roba.oduzmi = 'DA'" +
                                    " ", "").Tables[0];

            //ULAZ
            decimal kolicina_MSu = 0;
            decimal vpc_MSu = 0;

            decimal kolicina_kalk = 0;
            decimal vpc_kalk = 0;

            decimal kolicina_RN = 0;
            decimal vpc_RN = 0;

            decimal kolicina_PS = 0;
            decimal vpc_PS = 0;

            decimal kolicina_Primka = 0;
            decimal vpc_Primka = 0;

            //IZLAZ

            decimal kolicina_IZF = 0;
            decimal vpc_IZF = 0;

            decimal kolicina_Otp = 0;
            decimal vpc_Otp = 0;

            decimal kolicina_Povdob = 0;
            decimal vpc_Povdob = 0;

            decimal kolicina_mpr = 0;
            decimal vpc_mpr = 0;

            decimal mpc = 0;
            string sifra = "";
            string naziv_artika = "";
            string filter = "";

            for (int i = 0; i < DTrobaprodaja.Rows.Count; i++)
            {
                decimal vpc = Convert.ToDecimal(DTrobaprodaja.Rows[i]["vpc"].ToString());
                sifra = DTrobaprodaja.Rows[i]["sifra"].ToString();
                decimal porez = Convert.ToDecimal(DTrobaprodaja.Rows[i]["porez"].ToString());
                mpc = vpc * (1 + (porez / 100));
                naziv_artika = DTrobaprodaja.Rows[i]["naziv"].ToString();

                MS_ulaz = classSQL.select("SELECT  meduskladisnica_stavke.sifra," +
                        " meduskladisnica_stavke.iz_skladista," +
                        " meduskladisnica_stavke.kolicina , meduskladisnica.id_skladiste_od , meduskladisnica.id_skladiste_do" +
                        " FROM meduskladisnica_stavke LEFT JOIN meduskladisnica ON meduskladisnica.broj = meduskladisnica_stavke.broj" +
                        " WHERE meduskladisnica.datum >= '" + datumOD.ToString("dd-MM-yyyy") + "' " +
                        " AND meduskladisnica.datum <= '" + datumDO.ToString("dd-MM-yyyy") + "' " +
                        " AND meduskladisnica.id_skladiste_do='" + skladiste_odabir + "' AND " +
                        " meduskladisnica_stavke.sifra = '" + sifra + "' AND CAST(Replace(meduskladisnica_stavke.kolicina,',','.') as numeric) <> 0", "").Tables[0];
                if (MS_ulaz.Rows.Count > 0)
                {
                    //vpc_MSu = Convert.ToDecimal(MS_ulaz.Rows[0]["vpc"].ToString());
                    kolicina_MSu = Convert.ToDecimal(MS_ulaz.Rows[0]["kolicina"].ToString());
                }

                DTkalk = classSQL.select("SELECT kalkulacija_stavke.sifra," +
                    " kalkulacija_stavke.id_skladiste , kalkulacija_stavke.kolicina " +
                    " FROM kalkulacija_stavke " +
                    " LEFT JOIN kalkulacija ON kalkulacija.id_kalkulacija = kalkulacija_stavke.id_kalkulacija " +
                    " WHERE kalkulacija.datum >= '" + datumOD.ToString("dd-MM-yyyy") + "'" +
                    " AND kalkulacija.datum <= '" + datumDO.ToString("dd-MM-yyyy") + "' " +
                    " AND kalkulacija.id_skladiste='" + skladiste_odabir + "'" +
                    " AND kalkulacija_stavke.sifra = '" + sifra + "' " +
                    " AND CAST(Replace(kalkulacija_stavke.kolicina,',','.') as numeric) <> 0 ", "Kalkulacija").Tables[0];
                if (DTkalk.Rows.Count > 0)
                {
                    //vpc_kalk = Convert.ToDecimal(DTkalk.Rows[0]["vpc"].ToString());
                    kolicina_kalk = Convert.ToDecimal(DTkalk.Rows[0]["kolicina"].ToString());
                }

                DTradni_nalog = classSQL.select("SELECT radni_nalog_stavke.sifra_robe, " +
                    " radni_nalog_stavke.id_skladiste," +
                    " radni_nalog_stavke.kolicina FROM radni_nalog_stavke " +
                    " LEFT JOIN radni_nalog ON radni_nalog.broj_naloga = radni_nalog_stavke.broj_naloga " +
                    " WHERE  radni_nalog.datum_naloga >= '" + datumOD.ToString("dd-MM-yyyy") + "' " +
                    " AND radni_nalog.datum_naloga <= '" + datumDO.ToString("dd-MM-yyyy") + "' " +
                    " AND radni_nalog_stavke.id_skladiste='" + skladiste_odabir + "'" +
                    " AND radni_nalog_stavke.sifra_robe = '" + sifra + "' " +
                    " AND CAST(Replace(radni_nalog_stavke.kolicina,',','.') as numeric) <> 0", "radni_nalog").Tables[0];
                if (DTradni_nalog.Rows.Count > 0)
                {
                    //vpc_RN = Convert.ToDecimal(DTradni_nalog.Rows[0]["vpc"].ToString());
                    kolicina_RN = Convert.ToDecimal(DTradni_nalog.Rows[0]["kolicina"].ToString());
                }

                DTpocetno_stanje = classSQL.select("SELECT pocetno_stanje_stavke.sifra," +
                    " pocetno_stanje.id_skladiste, pocetno_stanje_stavke.kolicina " +
                    " FROM pocetno_stanje_stavke LEFT JOIN pocetno_stanje ON " +
                    " pocetno_stanje.broj = pocetno_stanje_stavke.broj " +
                    " WHERE  pocetno_stanje.date >= '" + datumOD.ToString("dd-MM-yyyy") + "' " +
                    " AND pocetno_stanje.date <= '" + datumDO.ToString("dd-MM-yyyy") + "' " +
                    " AND pocetno_stanje.id_skladiste='" + skladiste_odabir + "'" +
                    " AND pocetno_stanje_stavke.sifra = '" + sifra + "' " +
                    " AND CAST(Replace(pocetno_stanje_stavke.kolicina,',','.') as numeric) <> 0 ", "pocetno_stanje").Tables[0];

                if (DTpocetno_stanje.Rows.Count > 0)
                {
                    //vpc_PS = Convert.ToDecimal(DTpocetno_stanje.Rows[0]["vpc"].ToString());
                    kolicina_PS = Convert.ToDecimal(DTpocetno_stanje.Rows[0]["kolicina"].ToString());
                }

                DTprimka = classSQL.select("SELECT primka_stavke.sifra, " +
                    " primka.id_skladiste, primka_stavke.kolicina" +
                    " FROM primka_stavke LEFT JOIN primka ON primka.id_primka = primka_stavke.id_primka " +
                    " WHERE  primka.datum >= '" + datumOD.ToString("dd-MM-yyyy") + "' " +
                    " AND primka.datum <= '" + datumDO.ToString("dd-MM-yyyy") + "' " +
                    " AND primka.id_skladiste='" + skladiste_odabir + "' " +
                    " AND primka_stavke.sifra = '" + sifra + "' " +
                    " AND CAST(Replace(primka_stavke.kolicina,',','.') as numeric) <> 0 ", "primka").Tables[0];

                if (DTprimka.Rows.Count > 0)
                {
                    //vpc_Primka = Convert.ToDecimal(DTprimka.Rows[0]["vpc"].ToString());
                    kolicina_Primka = Convert.ToDecimal(DTprimka.Rows[0]["kolicina"].ToString());
                }

                DTizlazna_fak = classSQL.select("SELECT " +
                    " faktura_stavke.sifra, faktura_stavke.id_skladiste, " +
                    " faktura_stavke.kolicina FROM faktura_stavke LEFT JOIN fakture ON " +
                    " fakture.broj_fakture = faktura_stavke.broj_fakture " +
                    " AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa" +
                    " WHERE  " +
                    " fakture.date >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' " +
                    " AND fakture.date <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' " +
                    " AND faktura_stavke.id_skladiste='" + skladiste_odabir + "' " +
                    " AND faktura_stavke.sifra = '" + sifra + "' " +
                    " AND CAST(Replace(faktura_stavke.kolicina,',','.') as numeric) <> 0 ", "Izlazne_fakture").Tables[0];

                if (DTizlazna_fak.Rows.Count > 0)
                {
                    //vpc_IZF = Convert.ToDecimal(DTizlazna_fak.Rows[0]["vpc"].ToString());
                    kolicina_IZF = Convert.ToDecimal(DTizlazna_fak.Rows[0]["kolicina"].ToString());
                }

                DTOtpremnica = classSQL.select("SELECT otpremnica_stavke.sifra_robe,  " +
                    " otpremnica_stavke.id_skladiste, " +
                    " otpremnica_stavke.kolicina FROM otpremnica_stavke LEFT JOIN otpremnice ON " +
                    " otpremnice.id_otpremnica = otpremnica_stavke.id_otpremnice WHERE  " +
                    " otpremnice.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND " +
                    " otpremnice.datum <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND " +
                    " otpremnice.id_skladiste='" + skladiste_odabir + "' " +
                    " AND otpremnica_stavke.sifra_robe = '" + sifra + "' " +
                    " AND CAST(Replace(otpremnica_stavke.kolicina,',','.') as numeric) <> 0 ", "Otpremnice").Tables[0];

                if (DTOtpremnica.Rows.Count > 0)
                {
                    //vpc_Otp = Convert.ToDecimal(DTOtpremnica.Rows[0]["vpc"].ToString());
                    kolicina_Otp = Convert.ToDecimal(DTOtpremnica.Rows[0]["kolicina"].ToString());
                }

                DTpovratnica_dobavljacu = classSQL.select("SELECT " +
                    " povrat_robe_stavke.sifra, povrat_robe.id_skladiste, " +
                    " povrat_robe_stavke.kolicina FROM povrat_robe_stavke LEFT JOIN povrat_robe ON " +
                    " povrat_robe.broj = povrat_robe_stavke.broj WHERE  " +
                    " povrat_robe.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' " +
                    " AND povrat_robe.datum <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' " +
                    " AND povrat_robe.id_skladiste='" + skladiste_odabir + "' " +
                    " AND povrat_robe_stavke.sifra = '" + sifra + "' " +
                    " AND CAST(Replace(povrat_robe_stavke.kolicina,',','.') as numeric) <> 0 ", "povrat_robe").Tables[0];

                if (DTpovratnica_dobavljacu.Rows.Count > 0)
                {
                    //vpc_Povdob = Convert.ToDecimal(DTpovratnica_dobavljacu.Rows[0]["vpc"].ToString());
                    kolicina_Povdob = Convert.ToDecimal(DTpovratnica_dobavljacu.Rows[0]["kolicina"].ToString());
                }

                DTmpr = classSQL.select("SELECT racun_stavke.sifra_robe, " +
                    "racun_stavke.id_skladiste, racun_stavke.kolicina FROM racun_stavke " +
                    " LEFT JOIN racuni ON racuni.broj_racuna = racun_stavke.broj_racuna WHERE  " +
                    " racuni.datum_racuna >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' " +
                    " AND racuni.datum_racuna <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' " +
                    " AND racun_stavke.id_skladiste='" + skladiste_odabir + "' " +
                    " AND racun_stavke.sifra_robe = '" + sifra + "' " +
                    " AND CAST(Replace(racun_stavke.kolicina,',','.') as numeric) <> 0 ", "racuni_stavke").Tables[0];
                if (DTmpr.Rows.Count > 0)
                {
                    //vpc_mpr = Convert.ToDecimal(DTmpr.Rows[0]["vpc"].ToString());
                    kolicina_mpr = Convert.ToDecimal(DTmpr.Rows[0]["kolicina"].ToString());
                }

                //zapisivanje u DATASET
                decimal ulaz_stanje = kolicina_MSu + kolicina_kalk + kolicina_RN + kolicina_PS + kolicina_Primka;
                decimal izlaz_stanje = kolicina_IZF + kolicina_Otp + kolicina_Povdob + kolicina_mpr;
                decimal ulaz_stanje_V = mpc * ulaz_stanje;
                decimal izlaz_stanje_V = mpc * izlaz_stanje;

                if (ulaz_stanje + izlaz_stanje != 0)
                {
                    DataRow DTrow = dSRliste.Tables[0].NewRow();
                    DTrow = dSRliste.Tables[0].NewRow();
                    DTrow["sifra"] = sifra + "---" + naziv_artika;
                    DTrow["cijena1"] = ulaz_stanje;
                    DTrow["cijena2"] = izlaz_stanje;
                    DTrow["cijena3"] = ulaz_stanje - izlaz_stanje;
                    DTrow["cijena5"] = mpc.ToString();
                    DTrow["cijena6"] = ulaz_stanje_V;
                    DTrow["cijena7"] = izlaz_stanje_V;
                    DTrow["cijena8"] = ulaz_stanje_V - izlaz_stanje_V;
                    dSRliste.Tables[0].Rows.Add(DTrow);
                }
            }
        }
    }
}