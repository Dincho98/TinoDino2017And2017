using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Report.Odjava
{
    public partial class frmOdjava : Form
    {
        public frmOdjava()
        {
            InitializeComponent();
        }

        public string broj_dokumenta { get; set; }
        public string dokumenat { get; set; }
        public string ImeForme { get; set; }

        private void frmOdjava_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

            if (dokumenat == "odjava")
            {
                odjava();
                this.Text = ImeForme;
            }
            else if (dokumenat == "POVRATNICA")
            {
                povrat_robe();
                this.Text = ImeForme;
            }

            this.reportViewer1.RefreshReport();
        }

        private void odjava()
        {
            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");

            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string sql_liste = $@"
                SELECT
                odjava_komisione_stavke.sifra,
                roba.naziv,
                roba.jm AS naziv2,
                SUM(CAST(REPLACE(odjava_komisione_stavke.kolicina_prodano,',','.') AS numeric)) AS kolicina,
                CAST(odjava_komisione_stavke.nbc AS money) AS cijena1,
                CAST(odjava_komisione_stavke.nbc AS money) * SUM(CAST(REPLACE(odjava_komisione_stavke.kolicina_prodano,',','.') AS numeric)) AS cijena2,
                CAST(REPLACE(odjava_komisione_stavke.rabat,',','.') AS numeric) AS cijena3,
                CAST(odjava_komisione_stavke.vpc AS money) AS cijena6
                FROM odjava_komisione_stavke
                LEFT JOIN roba ON roba.sifra=odjava_komisione_stavke.sifra WHERE broj={broj_dokumenta}
                GROUP BY odjava_komisione_stavke.sifra, roba.naziv, roba.jm, odjava_komisione_stavke.nbc, odjava_komisione_stavke.rabat, odjava_komisione_stavke.vpc";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            string sifra;
            double vpc, mpc, rabat, porez, mpc_ukp = 0, vpc_ukp = 0, kolicina = 0, ukupno = 0, ukupno_vpc = 0, ukupno_mpc = 0;
            DataTable dt = dSRliste.Tables[0];
            DataTable dtRoba;

            dtRoba = classSQL.select("select id_skladiste from odjava_komisione where broj='" + broj_dokumenta + "'", "roba_prodaja").Tables[0];

            string skladisteID = dtRoba.Rows.Count > 0 ? dtRoba.Rows[0][0].ToString() : "";

            //treba srediti mpc
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                vpc = Math.Round(Convert.ToDouble(dt.Rows[i]["cijena6"].ToString()), 3);
                rabat = Math.Round(Convert.ToDouble(dt.Rows[i]["cijena3"].ToString()), 2);
                kolicina = Math.Round(Convert.ToDouble(dt.Rows[i]["kolicina"].ToString()), 2);

                sifra = dt.Rows[i]["sifra"].ToString();
                dtRoba = classSQL.select("select porez from roba_prodaja where sifra='" + sifra + "'" +
                    " AND id_skladiste='" + skladisteID + "'", "roba_prodaja").Tables[0];

                vpc = vpc * (1 - rabat / 100);

                if (dtRoba.Rows.Count > 0)
                {
                    porez = Convert.ToDouble(dtRoba.Rows[0][0].ToString());
                    mpc = vpc * (1 + porez / 100);
                }
                else
                {
                    dtRoba = classSQL.select("select porez from roba where sifra='" + sifra + "'", "roba").Tables[0];
                    try
                    {
                        porez = Convert.ToDouble(dtRoba.Rows[0][0].ToString());
                        mpc = vpc * (1 + porez / 100);
                    }
                    catch
                    {
                        porez = 0;
                        mpc = vpc;
                    }
                }

                ukupno += (mpc * (1 - rabat / 100));
                //mpc *= (1 - rabat / 100);
                mpc_ukp = (mpc * kolicina);
                vpc_ukp = (vpc * kolicina);

                ukupno_vpc += vpc_ukp;
                ukupno_mpc += mpc_ukp;

                dt.Rows[i].SetField("cijena4", Math.Round(mpc, 2).ToString("#0.00"));
                dt.Rows[i].SetField("cijena5", Math.Round(porez).ToString("#0.00"));
                dt.Rows[i].SetField("cijena6", vpc.ToString("#0.000"));
                dt.Rows[i].SetField("cijena7", mpc_ukp.ToString("#0.000"));
                dt.Rows[i].SetField("cijena8", vpc_ukp.ToString("#0.000"));
            }

            string year = "";
            DataTable DT = classSQL.select("SELECT datum FROM odjava_komisione WHERE broj='" + broj_dokumenta + "'", "odjava_komisione").Tables[0];

            if (DT.Rows.Count > 0)
            {
                year = Convert.ToDateTime(DT.Rows[0]["datum"].ToString()).Year.ToString();
            }

            string sql_liste_string = "SELECT " +
                " 'Šifra' AS tbl1," +
                " 'Naziv' AS tbl2," +
                " 'JMJ' AS tbl3," +
                " 'Kol.' AS tbl4," +
                " 'NBC' AS tbl5," +
                " 'NBC uk.' AS tbl6," +
                " 'Rabat%' AS tbl7," +
                " 'MPC' AS tbl8," +
                " 'Porez%' AS tbl9," +
                " 'VPC' AS tbl10," +
                " odjava_komisione.datum AS datum1," +
                " odjava_komisione.napomena AS komentar," +
                " odjava_komisione.od_datuma AS datum2," +
                " odjava_komisione.do_datuma AS datum3," +
                " partners.ime_tvrtke AS string2," +
                " partners.adresa AS string3 ," +
                " grad.posta + ' ' + grad.grad AS string4 ," +
                " partners.oib AS string5," +
                " partners.id_partner AS string6," +
                " skladiste.skladiste AS skladiste," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS string1," +
                " CAST ('PREDMET: ODJAVA ROBE BROJ: ' AS nvarchar) + CAST (odjava_komisione.broj AS nvarchar) +'/'+ CAST (" + year + " AS nvarchar) AS naslov" +
                " FROM odjava_komisione " +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=odjava_komisione.id_skladiste " +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=odjava_komisione.id_zaposlenik " +
                " LEFT JOIN partners ON partners.id_partner=odjava_komisione.id_partner " +
                " LEFT JOIN grad ON grad.id_grad=partners.id_grad " +
                " WHERE broj ='" + broj_dokumenta + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste_string.Replace("nvarchar", "varchar")).Fill(dSRlisteTekst, "DTlisteTekst");
            }

            dSRlisteTekst.Tables[0].Rows[0].SetField("ukupno10", Math.Round(ukupno, 2).ToString("#0.00"));
            dSRlisteTekst.Tables[0].Rows[0].SetField("ukupno11", Math.Round(ukupno_vpc).ToString("#0.00"));
            dSRlisteTekst.Tables[0].Rows[0].SetField("ukupno12", Math.Round(ukupno_mpc).ToString("#0.00"));
            //dt.Rows[i].SetField("cijena8", vpc_ukp.ToString("#0.000"));
        }

        private void povrat_robe()
        {
            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");

            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string sql_liste = "SELECT " +
                " povrat_robe_stavke.sifra," +
                " roba.naziv," +
                " roba.jm AS naziv2," +
                " povrat_robe_stavke.kolicina AS kolicina," +
                " povrat_robe_stavke.rabat AS cijena1 ," +
                " povrat_robe_stavke.nbc AS cijena2," +
                " CAST(povrat_robe_stavke.nbc AS money) * CAST(REPLACE(odjava_komisione_stavke.kolicina_prodano,',','.') AS cijena3" +
                " FROM povrat_robe_stavke" +
                " LEFT JOIN roba ON roba.sifra=povrat_robe_stavke.sifra WHERE broj='" + broj_dokumenta + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            DataTable DT = classSQL.select("SELECT datum FROM povrat_robe WHERE broj='" + broj_dokumenta + "'", "povrat_robe").Tables[0];

            string year = DT.Rows.Count > 0 ? Convert.ToDateTime(DT.Rows[0]["datum"].ToString()).Year.ToString() : "";

            string sql_liste_string = "SELECT " +
                " 'Šifra' AS tbl1," +
                " 'Naziv' AS tbl2," +
                " 'JMJ' AS tbl3," +
                " 'Količina' AS tbl4," +
                " 'Rabat%' AS tbl5," +
                " 'Cijena' AS tbl6," +
                " 'Iznos' AS tbl7," +
                " povrat_robe.datum AS datum1," +
                " povrat_robe.napomena AS komentar," +
                " partners.ime_tvrtke AS string2," +
                " partners.adresa AS string3 ," +
                " grad.posta + ' ' + grad.grad AS string4 ," +
                " partners.oib AS string5," +
                " partners.id_partner AS string6," +
                " skladiste.skladiste AS skladiste," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS string1," +
                " CAST ('POVRATNICA DOBAVLJAČU: ' AS nvarchar) + CAST (povrat_robe.broj AS nvarchar) +'/'+ CAST (" + year + " AS nvarchar) AS naslov" +
                " FROM povrat_robe " +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=povrat_robe.id_skladiste " +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=povrat_robe.id_izradio " +
                " LEFT JOIN partners ON partners.id_partner=povrat_robe.id_partner " +
                " LEFT JOIN grad ON grad.id_grad=partners.id_grad " +
                " WHERE broj ='" + broj_dokumenta + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste_string.Replace("nvarchar", "varchar")).Fill(dSRlisteTekst, "DTlisteTekst");
            }
        }
    }
}