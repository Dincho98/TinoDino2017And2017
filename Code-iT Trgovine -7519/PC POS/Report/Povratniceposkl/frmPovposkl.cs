using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.Povratniceposkl
{
    public partial class frmPovposkl : Form
    {
        public frmPovposkl()
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
        public string ducan { get; set; }
        public bool premadatumu { get; set; }
        public bool prema_rac { get; set; }
        public int skladiste_brojac { get; set; }
        public Boolean bool1 { get; set; }
        public Boolean bool2 { get; set; } // pomoćni bit
        public int brojac { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }

        public string BrojFakOD { get; set; }
        public string BrojFakDO { get; set; }

        private INIFile ini = new INIFile();

        private void frmKalkposkl_Load(object sender, EventArgs e)
        {
            //Sintetika_po_skladisnim_cijenama_sva_skladista();
            ListaKalkulacije();
            this.Text = ImeForme;

            this.reportViewer1.RefreshReport();
        }

        private DataTable ds = new Dataset.DSRlisteTekst.DTlisteTekstDataTable();

        private void SETizracun(DataTable Kal, string NAZIV_SKLADISTA, string broj_skladista)
        {
            decimal mpc_uk = 0;
            decimal povdob_rab_uk = 0;
            decimal povdob_nab_uk = 0;
            decimal povdob_izn_bez_por_uk = 0;
            decimal povdob_izn_s_por_uk = 0;
            DataTable DTstavke;

            for (int i = 0; i < Kal.Rows.Count; i++)
            {
                string sqlStavka = "SELECT " +
                    " povrat_robe_stavke.broj," +
                    " povrat_robe_stavke.vpc," +
                    " povrat_robe_stavke.pdv," +
                    " povrat_robe_stavke.nbc," +
                    " povrat_robe_stavke.kolicina," +
                    " povrat_robe.id_skladiste," +
                    " povrat_robe_stavke.rabat " +
                    " FROM povrat_robe_stavke" +
                    " LEFT JOIN povrat_robe on povrat_robe_stavke.broj = povrat_robe.broj" +
                    " WHERE povrat_robe_stavke.broj='" + Kal.Rows[i]["broj"].ToString() + "' ORDER BY CAST(povrat_robe.broj as numeric)";

                DTstavke = classSQL.select(sqlStavka, "povrat_robe_stavke").Tables[0];

                decimal ukupno_rabat = 0;
                decimal ukupno_vpc = 0;
                decimal ukupno_mpc = 0;
                decimal ukupno_nbc = 0;

                for (int y = 0; y < DTstavke.Rows.Count; y++)
                {
                    decimal vpc = Convert.ToDecimal(DTstavke.Rows[y]["vpc"].ToString());
                    decimal kolicina = Convert.ToDecimal(DTstavke.Rows[y]["kolicina"].ToString());
                    decimal pdv = Convert.ToDecimal(DTstavke.Rows[y]["pdv"].ToString());
                    decimal rabat = Convert.ToDecimal(DTstavke.Rows[y]["rabat"].ToString());
                    decimal kol = Convert.ToDecimal(DTstavke.Rows[y]["kolicina"].ToString());
                    decimal nbc = Convert.ToDecimal(DTstavke.Rows[y]["nbc"].ToString());

                    decimal mpc = vpc * (1 + (pdv / 100));
                    decimal vpc_s_kol = vpc * kolicina;
                    decimal mpc_rab = mpc - (mpc * (rabat / 100));

                    ukupno_rabat += (mpc * rabat / 100) * kol;
                    ukupno_vpc += vpc_s_kol;
                    ukupno_mpc += mpc_rab * kol;
                    ukupno_nbc += nbc * kol;
                }

                povdob_rab_uk = ukupno_rabat + povdob_rab_uk;
                povdob_nab_uk = ukupno_nbc + povdob_nab_uk;
                povdob_izn_s_por_uk = ukupno_mpc + povdob_izn_s_por_uk;
                povdob_izn_bez_por_uk = ukupno_vpc + povdob_izn_bez_por_uk;

                if (ukupno_mpc != 0)
                {
                    DataRow DTrow = dSRlisteTekst.Tables[0].NewRow();
                    DTrow["string1"] = Kal.Rows[i]["broj"].ToString();
                    DTrow["string2"] = Kal.Rows[i]["id_skladiste"].ToString();
                    DTrow["string3"] = Convert.ToDateTime(Kal.Rows[i]["datum"].ToString()).ToString("dd-MM-yyyy");
                    DTrow["string5"] = Kal.Rows[i]["id_partner"].ToString() + " " + Kal.Rows[i]["ime_tvrtke"].ToString();
                    DTrow["ukupno1"] = ukupno_rabat.ToString("#0.00");
                    DTrow["ukupno2"] = ukupno_nbc.ToString("#0.00");
                    DTrow["ukupno3"] = ukupno_vpc.ToString("#0.00");
                    DTrow["ukupno4"] = ukupno_mpc.ToString("#0.00");
                    dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                }
            }

            DataRow DTrow1 = dSRlisteTekst.Tables[0].NewRow();

            decimal sveukupno = 0;
            decimal zbroj = 0;
            decimal zbroj_ulaz = 0;
            decimal zbroj_izlaz = 0;
            decimal zbroj_marza = 0;
            decimal zbroj_zavisni = 0;
            decimal zbroj_izn_bez_por = 0;
            decimal zbroj_rab = 0;

            DTrow1 = dSRlisteTekst.Tables[0].NewRow();
            DTrow1["string1"] = "";
            DTrow1["string2"] = "";
            DTrow1["string3"] = "";
            DTrow1["string4"] = "";
            DTrow1["string5"] = "";
            DTrow1["ukupno1"] = DBNull.Value;
            DTrow1["ukupno2"] = DBNull.Value;
            DTrow1["ukupno3"] = DBNull.Value;
            DTrow1["ukupno4"] = DBNull.Value;
            dSRlisteTekst.Tables[0].Rows.Add(DTrow1);

            DTrow1 = dSRlisteTekst.Tables[0].NewRow();
            DTrow1["string5"] = "UKUPNO :";
            DTrow1["string4"] = "Skladiste " + " " + broj_skladista.ToString();
            DTrow1["string2"] = NAZIV_SKLADISTA.ToString();
            DTrow1["ukupno1"] = Convert.ToDecimal(povdob_rab_uk.ToString("#0.00"));
            DTrow1["ukupno2"] = Convert.ToDecimal(povdob_nab_uk.ToString("#0.00"));
            DTrow1["ukupno3"] = Convert.ToDecimal(povdob_izn_bez_por_uk.ToString("#0.00"));
            DTrow1["ukupno4"] = Convert.ToDecimal(povdob_izn_s_por_uk.ToString("#0.00"));
            dSRlisteTekst.Tables[0].Rows.Add(DTrow1);

            zbroj = povdob_izn_s_por_uk + zbroj;

            zbroj_ulaz = povdob_izn_s_por_uk + zbroj_ulaz;
            zbroj_izlaz = povdob_nab_uk + zbroj_izlaz;

            zbroj_izn_bez_por = povdob_izn_bez_por_uk + zbroj_izn_bez_por;
            zbroj_rab += povdob_rab_uk;
            //sveukupno = kalk_izn_s_por_uk + sveukupno;

            DTrow1 = dSRlisteTekst.Tables[0].NewRow();
            DTrow1["string1"] = "";
            DTrow1["string2"] = "";
            DTrow1["string3"] = "";
            DTrow1["string4"] = "";
            DTrow1["string5"] = "";
            DTrow1["ukupno1"] = DBNull.Value;
            DTrow1["ukupno2"] = DBNull.Value;
            DTrow1["ukupno3"] = DBNull.Value;
            DTrow1["ukupno4"] = DBNull.Value;
            dSRlisteTekst.Tables[0].Rows.Add(DTrow1);

            sveukupno = zbroj;
            string imereporta = "za prodajne cijene";
            string sqln = " SELECT " +
                " '" + zbroj_izn_bez_por.ToString("0.00") + "' As cijena9 ," +
                " '" + zbroj_izlaz.ToString("0.00") + "' As cijena7 ," +
                " '" + zbroj.ToString("0.00") + "' As cijena1 ," +
                " '" + zbroj_rab.ToString("0.00") + "' As cijena2 ," +
                " '" + datumOD.AddDays(0).ToString("yyyy-MM-dd") + "' As datum1 ," +
                " '" + datumDO.AddDays(0).ToString("yyyy-MM-dd") + "' As datum2 ," +
                " '" + imereporta + "' As jmj " +
                "";
            classSQL.CeAdatpter(sqln).Fill(dSRliste, "DTliste");
        }

        private void ListaKalkulacije()
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

            DataTable DTkalk = new DataTable();

            string skla_kalk = "";

            if (bool1 == true)
            {
                skla_kalk = "";
            }
            else
            {
                skla_kalk = "";
            }
            if (bool1)
            {
                string opcije = "";
                if (prema_rac != true)
                {
                    opcije = "povrat_robe.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND povrat_robe.datum <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "'";
                }
                else
                {
                    opcije = "povrat_robe.broj >= '" + BrojFakOD + "' AND povrat_robe.broj <= '" + BrojFakDO + "'";
                }
                string sqlHeder = "SELECT " +
                                " povrat_robe.broj, " +
                                " povrat_robe.id_skladiste, " +
                                " povrat_robe.datum, " +
                                " partners.id_partner," +
                                " partners.ime_tvrtke" +
                                " FROM povrat_robe" +
                                " LEFT JOIN partners ON partners.id_partner=povrat_robe.id_odrediste " +
                                " WHERE " + opcije + " AND povrat_robe.id_skladiste = '" + skladiste_odabir + "' ORDER BY CAST(broj as numeric)";
                //string sql1="SELECT kalkulacija_stavke.vpc, kalkulacija_stavke.id_skladiste, kalkulacija_stavke.rabat, kalkulacija_stavke.kolicina, kalkulacija_stavke.porez FROM kalkulacija_stavke LEFT JOIN kalkulacija ON kalkulacija.broj = kalkulacija_stavke.broj WHERE kalkulacija.datum >= '" + datumOD.ToString("dd-MM-yyyy") + "' AND kalkulacija.datum <= '" + datumDO.ToString("dd-MM-yyyy") + "' AND kalkulacija.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_kalk + " ";
                DTkalk = classSQL.select(sqlHeder, "povrat_robe").Tables[0];

                SETizracun(DTkalk, skladiste, skladiste_odabir);
            }
            else
            {
                string opcije = "";
                if (prema_rac != true)
                {
                    opcije = "povrat_robe.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND povrat_robe.datum <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "'";
                }
                else
                {
                    opcije = "povrat_robe.broj >= '" + BrojFakOD + "' AND povrat_robe.broj <= '" + BrojFakDO + "'";
                }
                foreach (DataRow row in DTskl.Rows)
                {
                    string sqlHeder = "SELECT " +
                                    " povrat_robe.broj, " +
                                    " povrat_robe.id_skladiste, " +
                                    " povrat_robe.datum, " +
                                    " partners.id_partner," +
                                    " partners.ime_tvrtke" +
                                    " FROM povrat_robe" +
                                    " LEFT JOIN partners ON partners.id_partner=povrat_robe.id_odrediste " +
                                    " WHERE " + opcije + " AND id_skladiste = '" + row["id_skladiste"].ToString() + "'ORDER BY CAST(broj as numeric)";
                    //string sql1="SELECT kalkulacija_stavke.vpc, kalkulacija_stavke.id_skladiste, kalkulacija_stavke.rabat, kalkulacija_stavke.kolicina, kalkulacija_stavke.porez FROM kalkulacija_stavke LEFT JOIN kalkulacija ON kalkulacija.broj = kalkulacija_stavke.broj WHERE kalkulacija.datum >= '" + datumOD.ToString("dd-MM-yyyy") + "' AND kalkulacija.datum <= '" + datumDO.ToString("dd-MM-yyyy") + "' AND kalkulacija.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_kalk + " ";
                    DTkalk = classSQL.select(sqlHeder, "povrat_robe").Tables[0];

                    SETizracun(DTkalk, row["skladiste"].ToString(), row["id_skladiste"].ToString());
                }
            }
        }
    }
}