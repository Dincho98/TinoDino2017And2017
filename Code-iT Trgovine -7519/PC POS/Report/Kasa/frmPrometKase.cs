using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Report.Kasa
{
    public partial class frmPrometKase : Form
    {
        public frmPrometKase()
        {
            InitializeComponent();
        }

        //public string od_datuma { get; set; }
        //public string do_datuma { get; set; }

        private void test_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);
            SetDS();
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

        private void SetDS()
        {
            string naziv_fakture = " podaci_tvrtka.naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql("", naziv_fakture, "");

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }
            else
            {
                classSQL.NpgAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");
            }

            string sql2 = "SELECT " +
                "racuni.broj_racuna AS br_racuna," +
                "racuni.id_kasa AS blagajna," +
                "racuni.datum_racuna AS datum," +
                "racuni.ukupno AS ukupno," +
                "racuni.ukupno_gotovina AS gotovina," +
                "racuni.ukupno_kartice AS kartice," +
                "zaposlenici.ime + ' ' + zaposlenici.prezime AS blagajnik," +
                "'PROMET KASE PO RAČUNIMA' AS naslov " +
                " FROM racuni LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=racuni.id_blagajnik" +
                " ORDER BY racuni.datum_racuna";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql2).Fill(dSKasaPromet, "DTkasaPromet");
            }
            else
            {
                classSQL.NpgAdatpter(sql2).Fill(dSKasaPromet, "DTkasaPromet");
            }

            DataTable dt = dSKasaPromet.Tables[0];

            double uk, got, kart, povrat;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                uk = Convert.ToDouble(dt.Rows[i]["ukupno"].ToString());
                got = Convert.ToDouble(dt.Rows[i]["gotovina"].ToString());
                kart = Convert.ToDouble(dt.Rows[i]["kartice"].ToString());

                povrat = uk - got - kart;

                dt.Rows[i].SetField("povrat", povrat);
            }

            //    string sql3 = "SELECT "+
            //        " racun_stavke.mpc," +
            //        " racun_stavke.vpc," +
            //        " racun_stavke.rabat," +
            //        " racun_stavke.kolicina," +
            //        " racun_stavke.porez," +
            //        " racuni.gotovina," +
            //        " racuni.kartice" +
            //        " FROM racun_stavke LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna "+
            //        " WHERE  racuni.datum_racuna >= '" + od_datuma + "' AND racuni.datum_racuna <='" + do_datuma + "'";

            //   DataTable DTrc = classSQL.select(sql3, "racun_stavke").Tables[0];

            //   double kolicina_stavka = 0;
            //   double rabat_stavka = 0;
            //   double vpc_stavka = 0;
            //   double porez_stavka = 0;
            //   double mpc_stavka = 0;

            //   double sve_ukupno=0;
            //   double rabat = 0;
            //   double osnovica = 0;
            //   double pdv = 0;

            //   double sve_ukupno1 = 0;
            //   double rabat1 = 0;
            //   double osnovica1 = 0;
            //   double pdv1 = 0;

            //   for (int i = 0; i < DTrc.Rows.Count; i++)
            //   {
            //       kolicina_stavka = Convert.ToDouble(DTrc.Rows[i]["kolicina"].ToString());
            //       rabat_stavka = Convert.ToDouble(DTrc.Rows[i]["rabat"].ToString());
            //       vpc_stavka = Convert.ToDouble(DTrc.Rows[i]["vpc"].ToString());
            //       porez_stavka = Convert.ToDouble(DTrc.Rows[i]["porez"].ToString());
            //       mpc_stavka = ((vpc_stavka * porez_stavka / 100) + vpc_stavka)*kolicina_stavka;

            //           //izracun gotovina
            //           sve_ukupno = (mpc_stavka - (mpc_stavka * rabat_stavka / 100)) + sve_ukupno;
            //           rabat = (mpc_stavka * rabat_stavka / 100) + rabat;
            //           osnovica = ((mpc_stavka - (mpc_stavka * rabat_stavka / 100)) / (1 + Convert.ToDouble(porez_stavka)/100) + osnovica;
            //           pdv = (mpc_stavka - (mpc_stavka * rabat_stavka / 100)) - ((mpc_stavka - (mpc_stavka * rabat_stavka / 100)) / (1 + Convert.ToDouble(porez_stavka)/100);

            //           //izracun kartica
            //           sve_ukupno1 = (mpc_stavka - (mpc_stavka * rabat_stavka / 100)) + sve_ukupno;
            //           rabat1 = (mpc_stavka * rabat_stavka / 100) + rabat;
            //           osnovica1 = ((mpc_stavka - (mpc_stavka * rabat_stavka / 100)) / (1 + Convert.ToDouble(porez_stavka)/100) + osnovica;
            //           pdv1 = (mpc_stavka - (mpc_stavka * rabat_stavka / 100)) - ((mpc_stavka - (mpc_stavka * rabat_stavka / 100)) / (1 + Convert.ToDouble(porez_stavka)/100);

            //   }
        }
    }
}