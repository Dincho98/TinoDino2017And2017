using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Report.lzlazniRacuni
{
    public partial class frmListakalk : Form
    {
        public string ImeForme { get; set; }
        public string documenat { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }

        public bool prema_rac { get; set; }

        public string BrojFakOD { get; set; }
        public string BrojFakDO { get; set; }

        public frmListakalk()
        {
            InitializeComponent();
        }

        private void frmListakalk_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);
            this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            if (documenat == "kalk")
            {
                ListaKalkulacije();
                this.Text = ImeForme;
            }

            this.reportViewer1.RefreshReport();
        }

        private void ListaKalkulacije()
        {
            string naziv_fakture = " podaci_tvrtka.naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql("", naziv_fakture, "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            //string Broj_fakture = "";
            //DateTime Datum_dokumenta;
            //DateTime Datum_valute;
            //string Sifra_i_naziv_partnera = "";

            string kriterij = "";
            if (prema_rac)
            {
                kriterij = " WHERE kalkulacija.broj >= '" + BrojFakOD + "' AND kalkulacija.broj <= '" + BrojFakDO + "' ORDER BY kalkulacija.broj ASC";
            }
            else
            {
                kriterij = " WHERE kalkulacija.racun_datum >= '" + datumOD + "' AND kalkulacija.racun_datum <= '" + datumDO + "' ORDER BY kalkulacija.racun_datum ASC";
            }

            string sqlHeder = "SELECT " +
            " kalkulacija.broj, " +
            " kalkulacija.id_skladiste, " +
            " kalkulacija.id_kalkulacija, " +
            " kalkulacija.racun_datum, " +
            " partners.id_partner," +
            " partners.ime_tvrtke" +
            " FROM kalkulacija" +
            " LEFT JOIN partners ON partners.id_partner=kalkulacija.id_partner " + kriterij +
            "";
            DataRow DDTrow = dSRlisteTekst.Tables[0].NewRow();
            if (BrojFakDO != "")
            {
                DDTrow["string3"] = "Od računa :" + " " + BrojFakOD.ToString();
                DDTrow["string4"] = "Do računa :" + " " + BrojFakDO.ToString();
            }
            else
            {
                DDTrow["string3"] = "Od datuma :" + " " + datumOD.ToString();
                DDTrow["string4"] = "Do datuma :" + " " + datumDO.ToString();
            }

            if (documenat == "kalk")
            {
                DDTrow["string5"] = "Kalkulacije";
            }

            dSRlisteTekst.Tables[0].Rows.Add(DDTrow);

            DataTable DTheader = classSQL.select(sqlHeder, "kalkulacija").Tables[0];

            DataTable DTstavke;

            for (int i = 0; i < DTheader.Rows.Count; i++)
            {
                string sqlStavka = "SELECT " +
                    " kalkulacija_stavke.broj," +
                    " kalkulacija_stavke.vpc," +
                    " kalkulacija_stavke.porez," +
                    " kalkulacija_stavke.kolicina," +
                    " kalkulacija_stavke.id_skladiste," +
                    " kalkulacija_stavke.rabat, " +
                    " kalkulacija_stavke.marza_postotak, " +
                    " kalkulacija_stavke.fak_cijena, " +
                    " kalkulacija_stavke.carina, " +
                    " kalkulacija_stavke.prijevoz, " +
                    " kalkulacija_stavke.posebni_porez " +
                    " FROM kalkulacija_stavke" +
                    " WHERE kalkulacija_stavke.broj='" + DTheader.Rows[i]["broj"].ToString() + "' AND kalkulacija_stavke.id_skladiste='" + DTheader.Rows[i]["id_skladiste"].ToString() + "'";

                DTstavke = classSQL.select(sqlStavka, "kalkulacija_stavke").Tables[0];

                decimal Iznos_fakturni_uk = 0;
                decimal Iznos_nabavni_uk = 0;
                decimal fakt_cijena_sa_sab = 0;
                decimal iznos_bez_poreza = 0;
                decimal iznos_sa_porezom = 0;
                decimal zavisni = 0;
                decimal marza = 0;

                for (int y = 0; y < DTstavke.Rows.Count; y++)
                {
                    decimal vpc = Convert.ToDecimal(DTstavke.Rows[y]["vpc"].ToString());
                    decimal pdv = Convert.ToDecimal(DTstavke.Rows[y]["porez"].ToString());
                    decimal rabat = Convert.ToDecimal(DTstavke.Rows[y]["rabat"].ToString());
                    decimal kol = Convert.ToDecimal(DTstavke.Rows[y]["kolicina"].ToString());
                    decimal marza_post = Convert.ToDecimal(DTstavke.Rows[y]["marza_postotak"].ToString());
                    decimal car = Convert.ToDecimal(DTstavke.Rows[y]["carina"].ToString());
                    decimal transport = Convert.ToDecimal(DTstavke.Rows[y]["prijevoz"].ToString());
                    decimal fakt_cijena = Convert.ToDecimal(DTstavke.Rows[y]["fak_cijena"].ToString());

                    Iznos_fakturni_uk = (fakt_cijena * kol) + Iznos_fakturni_uk;
                    fakt_cijena_sa_sab = fakt_cijena - (fakt_cijena * (rabat / 100));
                    Iznos_nabavni_uk = ((fakt_cijena_sa_sab * kol) - transport - car) + Iznos_nabavni_uk;
                    iznos_bez_poreza = (vpc * kol) + iznos_bez_poreza;
                    iznos_sa_porezom = ((vpc + (vpc * (pdv / 100))) * kol) + iznos_sa_porezom;
                    zavisni = (car + transport) + zavisni;
                    marza = Math.Round(marza / 100 * fakt_cijena, 2);
                }

                DataRow DTrow = dSRliste.Tables[0].NewRow();
                DTrow["jmj"] = DTheader.Rows[i]["broj"].ToString();
                DTrow["datum2"] = Convert.ToDateTime(DTheader.Rows[i]["racun_datum"].ToString()).ToString("dd-MM-yyyy");
                DTrow["sifra"] = DTheader.Rows[i]["id_kalkulacija"].ToString();
                DTrow["naziv"] = DTheader.Rows[i]["id_partner"].ToString() + " " + DTheader.Rows[i]["ime_tvrtke"].ToString();
                DTrow["cijena1"] = Iznos_fakturni_uk.ToString("#0.00");
                DTrow["cijena2"] = Iznos_nabavni_uk.ToString("#0.00");
                DTrow["cijena3"] = iznos_bez_poreza.ToString("#0.00");
                DTrow["cijena5"] = iznos_sa_porezom.ToString("#0.00");
                DTrow["cijena6"] = 0;
                DTrow["cijena7"] = marza.ToString("#0.00");
                DTrow["cijena8"] = zavisni.ToString("#0.00");
                dSRliste.Tables[0].Rows.Add(DTrow);
            }
        }
    }
}