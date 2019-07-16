using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmPrometStanje : Form
    {
        public frmPrometStanje()
        {
            InitializeComponent();
        }

        public string poslovnica { get; set; }
        public string skladiste { get; set; }
        public string ducan { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }

        public string ImeForme { get; set; }

        private void frmPrometStanje_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

            Fill();
            this.reportViewer1.RefreshReport();
        }

        private void Fill()
        {
            string poslo = "";
            string skladi = "";
            if (poslovnica != null) { poslo = "Poslovnica=" + poslovnica; }
            if (skladiste != null) { skladi = "  Skladište=" + skladiste; }

            string grad_id = "";
            DataTable DTgrad_tvrtke = classSQL.select("SELECT podaci_tvrtka.id_grad FROM podaci_tvrtka", "grad").Tables[0];
            if (DTgrad_tvrtke.Rows.Count != 0)
            {
                grad_id = DTgrad_tvrtke.Rows[0]["id_grad"].ToString().Trim();
            }

            string grad_tvrtke = "";
            DTgrad_tvrtke = classSQL.select("SELECT grad, posta FROM grad WHERE id_grad='" + grad_id + "'", "grad").Tables[0];
            if (DTgrad_tvrtke.Rows.Count != 0)
            {
                grad_tvrtke = DTgrad_tvrtke.Rows[0]["grad"].ToString().Trim() + ' ' + DTgrad_tvrtke.Rows[0]["posta"].ToString().Trim();
            }

            string sql1 = "SELECT " +
                " podaci_tvrtka.ime_tvrtke," +
                " podaci_tvrtka.skraceno_ime," +
                " 'OIB: ' + podaci_tvrtka.oib AS oib," +
                " podaci_tvrtka.tel," +
                " podaci_tvrtka.fax," +
                " podaci_tvrtka.mob," +
                " podaci_tvrtka.iban," +
                " podaci_tvrtka.adresa," +
                " 'Od datuma: " + datumOD + "  -  " + datumDO + "\r\n" + poslo + skladi + "' AS string1," +
                " podaci_tvrtka.vl," +
                " podaci_tvrtka.poslovnica_adresa," +
                " podaci_tvrtka.poslovnica_grad," +
                " podaci_tvrtka.email," +
                " podaci_tvrtka.naziv_fakture," +
                " podaci_tvrtka.text_bottom," +
                " '" + grad_tvrtke + "' AS grad" +
                " FROM podaci_tvrtka";

            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            Dataset.DSRlisteTekst DS = new Dataset.DSRlisteTekst();

            string skl = ""; if (skladiste != null) { skl = " WHERE roba_prodaja.skladiste ='" + skladiste + "'"; }

            string sql_roba = "SELECT " +
                " roba_prodaja.sifra,roba.jm,roba.naziv,roba_prodaja.vpc,grupa.grupa,roba_prodaja.kolicina,roba_prodaja.porez_potrosnja,roba_prodaja.porez " +
                " FROM roba_prodaja LEFT JOIN roba ON roba.sifra=roba_prodaja.sifra " +
                " LEFT JOIN grupa ON grupa.id_grupa=roba.id_grupa ORDER BY grupa.id_grupa" +
                " " + skl;
            DataTable DTsvaRoba = classSQL.select(sql_roba, "roba_prodaja").Tables[0];

            string sql_racuni = "";
            string sql_kalk = "";
            string sql_primka = "";
            DataTable DTrac = new DataTable();
            DataTable DTkalk = new DataTable();
            DataTable DTprimka = new DataTable();
            sql_racuni = "SELECT SUM(CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC)) AS [kolicina],racun_stavke.sifra_robe FROM racun_stavke " +
                " LEFT JOIN racuni ON racun_stavke.broj_racuna=racuni.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa" +
                " WHERE racuni.datum_racuna>'" + datumOD + "'" +
                " AND racuni.datum_racuna<'" + datumDO + "'" +
                " GROUP BY racun_stavke.sifra_robe";
            DTrac = classSQL.select(sql_racuni, "racun_stavke").Tables[0];

            //Kalkulacija
            sql_kalk = "SELECT SUM(CAST(REPLACE(kalkulacija_stavke.kolicina,',','.') AS NUMERIC)) AS [kolicina],kalkulacija_stavke.sifra  FROM kalkulacija_stavke " +
                " LEFT JOIN kalkulacija ON kalkulacija.broj=kalkulacija_stavke.broj" +
                " WHERE kalkulacija.datum>'" + datumOD + "'" +
                " AND kalkulacija.datum<'" + datumDO + "'" +
                " GROUP BY kalkulacija_stavke.sifra";
            DTkalk = classSQL.select(sql_kalk, "kalkulacija_stavke").Tables[0];

            sql_primka = "SELECT SUM(CAST(REPLACE(primka_stavke.kolicina,',','.') AS NUMERIC)) AS [kolicina],primka_stavke.sifra FROM primka_stavke " +
                " LEFT JOIN primka ON primka.broj=primka_stavke.broj" +
                " WHERE primka.datum>'" + datumOD + "'" +
                " AND primka.datum<'" + datumDO + "'" +
                " GROUP BY primka_stavke.sifra";
            DTprimka = classSQL.select(sql_primka, "primka_stavke").Tables[0];

            string sql_normativi = "SELECT " +
                " caffe_normativ.sifra_normativ," +
                " caffe_normativ.sifra," +
                " caffe_normativ.kolicina as [kolicina_normativ]," +
                " caffe_normativ.id_skladiste," +
                " roba.jm,roba.naziv,roba_prodaja.vpc,roba_prodaja.kolicina,roba_prodaja.porez_potrosnja,roba_prodaja.porez " +
                " FROM caffe_normativ" +
                " LEFT JOIN roba ON roba.sifra=caffe_normativ.sifra_normativ" +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=caffe_normativ.sifra_normativ" + skl;
            DataTable DTnorativi = classSQL.select(sql_normativi, "caffe_normativ").Tables[0];

            decimal pdv = 0;
            decimal pnp = 0;
            decimal vpc = 0;
            decimal kol_skl = 0;

            foreach (DataRow row in DTsvaRoba.Rows)
            {
                DataRow[] dataROW_Nor = DTnorativi.Select("sifra = '" + row["sifra"].ToString() + "'");
                if (dataROW_Nor.Count() > 0)
                {
                    decimal kol_racun = 0;
                    DataRow[] dataROW_R = DTrac.Select("sifra_robe = '" + row["sifra"].ToString() + "'");
                    if (dataROW_R.Count() > 0)
                    {
                        kol_racun = Convert.ToDecimal(dataROW_R[0]["kolicina"].ToString());
                    }

                    foreach (DataRow rowNormativ in dataROW_Nor)
                    {
                        decimal kol_ulaz = 0;
                        DataRow[] dataROW_K = DTkalk.Select("sifra = '" + rowNormativ["sifra_normativ"].ToString() + "'");
                        if (dataROW_K.Count() > 0)
                        {
                            kol_ulaz = decimal.Parse(dataROW_K[0]["kolicina"].ToString());
                        }

                        decimal kol_primaka = 0;
                        DataRow[] dataROW_P = DTprimka.Select("sifra = '" + rowNormativ["sifra_normativ"].ToString() + "'");
                        if (dataROW_P.Count() > 0)
                        {
                            kol_primaka = Convert.ToDecimal(dataROW_P[0]["kolicina"].ToString());
                        }

                        pdv = decimal.Parse(rowNormativ["porez"].ToString());
                        pnp = decimal.Parse(rowNormativ["porez_potrosnja"].ToString());
                        vpc = decimal.Parse(rowNormativ["vpc"].ToString());
                        kol_skl = decimal.Parse(rowNormativ["kolicina"].ToString());
                        decimal kolicina_normativ = decimal.Parse(rowNormativ["kolicina_normativ"].ToString());

                        DataRow DTrow = dSRlisteTekst.Tables[0].NewRow();
                        DTrow["string1"] = row["grupa"].ToString();
                        DTrow["string2"] = rowNormativ["naziv"].ToString();
                        DTrow["string3"] = rowNormativ["jm"].ToString();
                        DTrow["ukupno1"] = ((kol_skl + kol_racun) - (kol_ulaz + kol_primaka)).ToString("#0.000");
                        DTrow["ukupno2"] = (kol_ulaz + kol_primaka).ToString("#0.000");
                        DTrow["ukupno3"] = kol_skl.ToString("#0.000");
                        DTrow["ukupno4"] = kol_racun.ToString("#0.000");
                        DTrow["ukupno5"] = (vpc + ((vpc * (pdv + pnp)) / 100)).ToString("#0.00");
                        DTrow["ukupno6"] = ((kol_racun * kolicina_normativ) * (vpc + ((vpc * (pdv + pnp)) / 100))).ToString("#0.00");
                        dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                    }
                }
                else
                {
                    pdv = decimal.Parse(row["porez"].ToString());
                    pnp = decimal.Parse(row["porez_potrosnja"].ToString());
                    vpc = decimal.Parse(row["vpc"].ToString());
                    kol_skl = decimal.Parse(row["kolicina"].ToString());

                    decimal kol_racun = 0;
                    DataRow[] dataROW_R = DTrac.Select("sifra_robe = '" + row["sifra"].ToString() + "'");
                    if (dataROW_R.Count() > 0)
                    {
                        kol_racun = Convert.ToDecimal(dataROW_R[0]["kolicina"].ToString());
                    }

                    decimal kol_ulaz = 0;
                    DataRow[] dataROW_K = DTkalk.Select("sifra = '" + row["sifra"].ToString() + "'");
                    if (dataROW_K.Count() > 0)
                    {
                        kol_ulaz = decimal.Parse(dataROW_K[0]["kolicina"].ToString());
                    }

                    decimal kol_primaka = 0;
                    DataRow[] dataROW_P = DTprimka.Select("sifra = '" + row["sifra"].ToString() + "'");
                    if (dataROW_P.Count() > 0)
                    {
                        kol_primaka = Convert.ToDecimal(dataROW_P[0]["kolicina"].ToString());
                    }

                    DataRow DTrow = dSRlisteTekst.Tables[0].NewRow();
                    DTrow["string1"] = row["grupa"].ToString();
                    DTrow["string2"] = row["naziv"].ToString();
                    DTrow["string3"] = row["jm"].ToString();
                    DTrow["ukupno1"] = ((kol_skl + kol_racun) - (kol_ulaz + kol_primaka)).ToString("#0.000");
                    DTrow["ukupno2"] = (kol_ulaz + kol_primaka).ToString("#0.000");
                    DTrow["ukupno3"] = kol_skl.ToString("#0.000");
                    DTrow["ukupno4"] = kol_racun.ToString("#0.000");
                    DTrow["ukupno5"] = (vpc + ((vpc * (pdv + pnp)) / 100)).ToString("#0.00");
                    DTrow["ukupno6"] = (kol_racun * (vpc + ((vpc * (pdv + pnp)) / 100))).ToString("#0.00");
                    dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                }
            }
        }
    }
}