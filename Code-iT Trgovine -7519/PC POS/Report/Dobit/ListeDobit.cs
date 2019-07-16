using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Report.Dobit
{
    public partial class ListeDobit : Form
    {
        public string ImeForme { get; set; }
        public string documenat { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }

        public bool prema_rac { get; set; }

        public string BrojFakOD { get; set; }
        public string BrojFakDO { get; set; }

        public ListeDobit()
        {
            InitializeComponent();
        }

        private void ListeDobit_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);
            this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            if (documenat == "dobmpra")
            {
                dobit_po_maloprodajnim();
                this.Text = ImeForme;
            }
            if (documenat == "dobfak")
            {
                dobit_po_fakturama();
                this.Text = ImeForme;
            }
            this.reportViewer1.RefreshReport();
        }

        private void dobit_po_maloprodajnim()
        {
            //datumDO = DateTime.Now;
            //datumOD = DateTime.Now.AddDays(-365);

            //BrojFakDO = "";
            //BrojFakOD = "";

            string naziv_fakture = " podaci_tvrtka.naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql("", naziv_fakture, "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string kriterij = "";
            if (prema_rac)
            {
                kriterij = " WHERE CAST(racuni.broj_racuna as int) >= '" + BrojFakOD + "' AND CAST(racuni.broj_racuna as int) <= '" + BrojFakDO + "' ORDER BY CAST(racuni.broj_racuna as int) ASC";
            }
            else
            {
                kriterij = " WHERE racuni.datum_racuna >= '" + datumOD + "' AND racuni.datum_racuna <= '" + datumDO + "' ORDER BY racuni.datum_racuna ASC";
            }

            string sqlHeder = "SELECT " +
                    " racuni.broj_racuna, " +
                    " racuni.datum_racuna, " +
                    " partners.id_partner," +
                    " partners.ime_tvrtke" +
                    " FROM racuni" +
                    " LEFT JOIN partners ON partners.id_partner=racuni.id_kupac " + kriterij +
                    "";

            DataRow DDTrow = dSRlisteTekst.Tables[0].NewRow();
            if (BrojFakDO != "")
            {
                DDTrow["string3"] = "Od računa :" + " " + BrojFakOD.ToString();
                DDTrow["string4"] = "Do računa :" + " " + BrojFakDO.ToString();
            }
            else
            {
                DDTrow["string3"] = "Od datuma :" + " " + datumOD.ToString("dd.MM.yyyy");
                DDTrow["string4"] = "Do datuma :" + " " + datumDO.ToString("dd.MM.yyyy");
            }

            //if (documenat == "kalk")
            //{
            //    DDTrow["string5"] = "Kalkulacije";

            //}

            dSRlisteTekst.Tables[0].Rows.Add(DDTrow);

            DataTable DTheader = classSQL.select(sqlHeder, "racuni").Tables[0];

            DataTable DTstavke;

            for (int i = 0; i < DTheader.Rows.Count; i++)
            {
                string sqlStavka = "SELECT " +
                    " racun_stavke.broj_racuna," +
                    " racun_stavke.vpc," +
                    " racun_stavke.sifra_robe," +
                    " racun_stavke.mpc," +
                    " racun_stavke.porez," +
                    " racun_stavke.nbc," +
                    " racun_stavke.kolicina," +
                    " racun_stavke.rabat " +
                    " FROM racun_stavke" +
                    " WHERE CAST(racun_stavke.broj_racuna as int) ='" + DTheader.Rows[i]["broj_racuna"].ToString() + "'";

                DTstavke = classSQL.select(sqlStavka, "racun_stavke").Tables[0];

                decimal Bruto = 0;
                decimal Neto = 0;
                decimal Dobit = 0;
                decimal Nabavna = 0;
                decimal Porez = 0;
                decimal Rabat_uk = 0;
                decimal vpc_sa_rab = 0;
                decimal prer_stopa = 0;

                for (int y = 0; y < DTstavke.Rows.Count; y++)
                {
                    string sifra = DTstavke.Rows[y]["sifra_robe"].ToString();
                    decimal vpc = Convert.ToDecimal(DTstavke.Rows[y]["vpc"].ToString());
                    decimal nbc = Convert.ToDecimal(classSQL.select("Select nc From roba Where sifra = '" + sifra + "'", "nbc").Tables[0].Rows[0][0].ToString());//Convert.ToDecimal(DTstavke.Rows[y]["nbc"].ToString());
                    decimal mpc = Convert.ToDecimal(DTstavke.Rows[y]["mpc"].ToString());
                    decimal pdv = Convert.ToDecimal(DTstavke.Rows[y]["porez"].ToString());
                    decimal rabat = Convert.ToDecimal(DTstavke.Rows[y]["rabat"].ToString());
                    decimal kol = Convert.ToDecimal(DTstavke.Rows[y]["kolicina"].ToString());
                    decimal mpc_rab = (mpc - (mpc * (rabat / 100)));

                    prer_stopa = ((100 * pdv) / (100 + pdv)) / 100;
                    Bruto = (mpc_rab * kol) + Bruto;
                    Neto = (mpc_rab / (1 + (pdv / 100)) * kol) + Neto;
                    Nabavna = (nbc * kol) + Nabavna;
                    Porez = Bruto - Neto;
                    Rabat_uk = ((mpc * (rabat / 100)) * kol) + Rabat_uk;
                    vpc_sa_rab = Rabat_uk / (1 + (pdv / 100));
                    Dobit = Neto - Nabavna;
                }

                string sifrainaziv = "";
                if (sifrainaziv == "0" || sifrainaziv == "" || sifrainaziv == null)
                {
                    sifrainaziv = "PRIVATNI KUPAC";
                }
                else
                {
                    sifrainaziv = DTheader.Rows[i]["id_partner"].ToString() + " " + DTheader.Rows[i]["ime_tvrtke"].ToString();
                }

                DataRow DTrow = dSRliste.Tables[0].NewRow();
                DTrow["datum1"] = Convert.ToDateTime(DTheader.Rows[i]["datum_racuna"].ToString()).ToString("dd-MM-yyyy");
                DTrow["sifra"] = DTheader.Rows[i]["broj_racuna"].ToString();
                DTrow["naziv"] = sifrainaziv;
                DTrow["cijena1"] = Bruto.ToString("#0.00");
                DTrow["cijena2"] = Neto.ToString("#0.00");
                DTrow["cijena3"] = Nabavna.ToString("#0.00");
                DTrow["cijena4"] = Porez.ToString("#0.00");
                DTrow["cijena5"] = Dobit.ToString("#0.00");
                DTrow["cijena6"] = Rabat_uk.ToString("#0.00");
                dSRliste.Tables[0].Rows.Add(DTrow);
            }
        }

        private void dobit_po_fakturama()
        {
            //datumDO = DateTime.Now;
            //datumOD = DateTime.Now.AddDays(-365);

            //BrojFakDO = "";
            //BrojFakOD = "";

            string naziv_fakture = " podaci_tvrtka.naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql("", naziv_fakture, "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string kriterij = "";
            if (prema_rac)
            {
                kriterij = " WHERE CAST(fakture.broj_fakture as int) >= '" + BrojFakOD + "' AND CAST(fakture.broj_fakture as int) <= '" + BrojFakDO + "' ORDER BY CAST(fakture.broj_fakture as int) ASC";
            }
            else
            {
                kriterij = " WHERE fakture.date >= '" + datumOD + "' AND fakture.date <= '" + datumDO + "' ORDER BY fakture.date ASC";
            }

            string sqlHeder = "SELECT " +
                    " fakture.broj_fakture, " +
                    " fakture.date, " +
                    " partners.id_partner," +
                    " partners.ime_tvrtke" +
                    " FROM fakture" +
                    " LEFT JOIN partners ON partners.id_partner=fakture.id_fakturirati " + kriterij +
                    "";

            DataRow DDTrow = dSRlisteTekst.Tables[0].NewRow();

            if (BrojFakDO != "")
            {
                DDTrow["string3"] = "Od računa :" + " " + BrojFakOD.ToString();
                DDTrow["string4"] = "Do računa :" + " " + BrojFakDO.ToString();
            }
            else
            {
                DDTrow["string3"] = "Od datuma :" + " " + datumOD.ToString("dd.MM.yyyy");
                DDTrow["string4"] = "Do datuma :" + " " + datumDO.ToString("dd.MM.yyyy");
            }

            //if (documenat == "kalk")
            //{
            //    DDTrow["string5"] = "Kalkulacije";

            //}

            dSRlisteTekst.Tables[0].Rows.Add(DDTrow);

            DataTable DTheader = classSQL.select(sqlHeder, "fakture").Tables[0];

            DataTable DTstavke;

            for (int i = 0; i < DTheader.Rows.Count; i++)
            {
                string sqlStavka = "SELECT " +
                    " faktura_stavke.broj_fakture," +
                    " faktura_stavke.vpc," +
                    " faktura_stavke.porez," +
                    " faktura_stavke.sifra," +
                    " faktura_stavke.nbc," +
                    " faktura_stavke.kolicina," +
                    " faktura_stavke.rabat " +
                    " FROM faktura_stavke" +
                    " WHERE CAST(faktura_stavke.broj_fakture as int) ='" + DTheader.Rows[i]["broj_fakture"].ToString() + "'";

                DTstavke = classSQL.select(sqlStavka, "faktura_stavke").Tables[0];

                decimal Bruto = 0;
                decimal Neto = 0;
                decimal Dobit = 0;
                decimal Nabavna = 0;
                decimal Porez = 0;
                decimal vpc_sa_rab = 0;
                decimal Rabat_uk = 0;
                decimal nbc = 0;
                decimal prer_stopa = 0;

                for (int y = 0; y < DTstavke.Rows.Count; y++)
                {
                    string sifra = DTstavke.Rows[y]["sifra"].ToString();
                    decimal vpc = Convert.ToDecimal(DTstavke.Rows[y]["vpc"].ToString());
                    try
                    {
                        nbc = Convert.ToDecimal(classSQL.select("Select nc From roba Where sifra = '" + sifra + "'", "nbc").Tables[0].Rows[0][0].ToString());
                    }
                    catch
                    { }

                    decimal pdv = Convert.ToDecimal(DTstavke.Rows[y]["porez"].ToString());
                    decimal mpc = vpc * ((pdv / 100) + 1);
                    decimal rabat = Convert.ToDecimal(DTstavke.Rows[y]["rabat"].ToString());
                    decimal kol = Convert.ToDecimal(DTstavke.Rows[y]["kolicina"].ToString());
                    decimal mpc_rab = (mpc - (mpc * (rabat / 100)));

                    prer_stopa = ((100 * pdv) / (100 + pdv)) / 100;
                    Rabat_uk = ((mpc * (rabat / 100)) * kol) + Rabat_uk;
                    Bruto = (mpc_rab * kol) + Bruto;
                    Neto = ((mpc_rab / ((100 + pdv) / 100)) * kol) + Neto;
                    Nabavna = (nbc * kol) + Nabavna;
                    Porez = Bruto - Neto;

                    vpc_sa_rab = Rabat_uk / (1 + (pdv / 100));
                    Dobit = Neto - Nabavna;
                }

                string sifrainaziv = "";
                if (sifrainaziv == "0" || sifrainaziv == "" || sifrainaziv == null)
                {
                    sifrainaziv = "PRIVATNI KUPAC";
                }
                else
                {
                    sifrainaziv = DTheader.Rows[i]["id_partner"].ToString() + " " + DTheader.Rows[i]["ime_tvrtke"].ToString();
                }

                DataRow DTrow = dSRliste.Tables[0].NewRow();
                DTrow["datum1"] = Convert.ToDateTime(DTheader.Rows[i]["date"].ToString()).ToString("dd-MM-yyyy");
                DTrow["sifra"] = DTheader.Rows[i]["broj_fakture"].ToString();
                DTrow["naziv"] = sifrainaziv;
                DTrow["cijena1"] = Bruto.ToString("#0.00");
                DTrow["cijena2"] = Neto.ToString("#0.00");
                DTrow["cijena3"] = Nabavna.ToString("#0.00");
                DTrow["cijena4"] = Porez.ToString("#0.00");
                DTrow["cijena5"] = Dobit.ToString("#0.00");
                DTrow["cijena6"] = Rabat_uk.ToString("#0.00");
                dSRliste.Tables[0].Rows.Add(DTrow);
            }
        }
    }
}