using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Report.NormativBezCijene
{
    public partial class frmNormativBezCijene : Form
    {
        public frmNormativBezCijene()
        {
            InitializeComponent();
        }

        public string artikl { get; set; }
        public string godina { get; set; }
        public string broj_dokumenta { get; set; }
        public string dokument { get; set; }
        public string ImeForme { get; set; }
        public string blagajnik { get; set; }
        public string skladiste { get; set; }
        public string ducan { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }

        private void frmListe_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

            //dokumenat = "PrometPoRobi";
            //broj_dokumenta = "2";
            //skladiste = "2";
            //datumOD = DateTime.Now.AddDays(-2);
            //datumDO = DateTime.Now.AddDays(+10);

            if (dokument == "promjena_cijene")
            {
                promjenaCijene();
                this.Text = ImeForme;
            }
            else if (dokument == "promjena_cijene_komadno")
            {
                promjenaCijeneKomadno();
                this.Text = ImeForme;
            }
            else if (dokument == "meduskladisnica")
            {
                meduskladisnica();
                this.Text = ImeForme;
            }
            else if (dokument == "NORMATIV")
            {
                Normativ();
                this.Text = ImeForme;
            }
            else if (dokument == "OTPREMNICA")
            {
                Otpremnica();
                this.Text = ImeForme;
            }
            else if (dokument == "PrometPoRobi")
            {
                PrometPoRobi();
                this.Text = ImeForme;
            }
            else if (dokument == "PrometPoRobiBezSkladista")
            {
                PrometPoRobiBezSkladista();
                this.Text = ImeForme;
            }
            else if (dokument == "SKLfinancije")
            {
                SkladisteFinancije();
                this.Text = ImeForme;
            }

            this.reportViewer1.RefreshReport();
        }

        private void Otpremnica()
        {
            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");

            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string sql_liste = "SELECT " +
                " otpremnica_stavke.sifra_robe as sifra," +
                " roba.naziv," +
                " otpremnica_stavke.kolicina AS cijena1," +
                " roba.jm AS jmj," +
                " roba_prodaja.vpc AS cijena3 ," +
                " (COALESCE(CAST(roba_prodaja.vpc AS NUMERIC),0)*(COALESCE(CAST(replace(roba_prodaja.porez, ',','.') AS NUMERIC),0) zbroj COALESCE(CAST(roba_prodaja.porez_potrosnja AS NUMERIC),0))/100)zbrojCOALESCE(CAST(roba_prodaja.vpc AS NUMERIC),0) AS cijena5," +
                " skladiste.skladiste AS cijena4, " +
                " (COALESCE(CAST(roba_prodaja.vpc AS NUMERIC),0)*(COALESCE(CAST(replace(roba_prodaja.porez, ',','.') AS NUMERIC),0)zbrojCOALESCE(CAST(roba_prodaja.porez_potrosnja AS NUMERIC),0))/100)zbrojCOALESCE(CAST(roba_prodaja.vpc AS NUMERIC),0)*COALESCE(CAST(REPLACE(otpremnica_stavke.kolicina,',','.') AS NUMERIC),0) AS cijena6 " +
                " FROM otpremnice " +
                " left join otpremnica_stavke on otpremnica_stavke.broj_otpremnice = otpremnice.broj_otpremnice and otpremnica_stavke.id_skladiste = otpremnice.id_skladiste" +
                " LEFT JOIN roba ON roba.sifra=otpremnica_stavke.sifra_robe" +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=otpremnica_stavke.id_skladiste" +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=otpremnica_stavke.sifra_robe AND roba_prodaja.id_skladiste=otpremnica_stavke.id_skladiste " +
                " WHERE otpremnice.broj_otpremnice='" + broj_dokumenta + "' and otpremnice.id_skladiste = '" + skladiste + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            string year = "";
            DataTable DT = classSQL.select("SELECT godina_otpremnice FROM otpremnice WHERE broj_otpremnice ='" + broj_dokumenta + "' and id_skladiste = '" + skladiste + "'", "normativi").Tables[0];

            if (DT.Rows.Count > 0)
            {
                year = DT.Rows[0]["godina_otpremnice"].ToString();
            }

            //DataTable DTnor = classSQL.select("SELECT * FROM normativi WHERE broj_normativa='" + broj_dokumenta + "'", "normativi").Tables[0];

            string sql_liste_string = "SELECT " +
                " 'Šifra' AS tbl1," +
                " 'Naziv' AS tbl2," +
                " 'Količina' AS tbl4," +
                " 'JMJ' AS tbl3," +
                " 'VPC' AS tbl5," +
                " 'MPC' AS tbl7," +
                " 'Skladište' AS tbl6," +
                " 'Iznos' AS tbl8," +
                //"  normativi.godina_normativa AS datum1," +
                "  otpremnice.napomena AS komentar," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS string1," +
                " CAST ('Otpremnica  ' AS nvarchar) + CAST (otpremnice.broj_otpremnice AS nvarchar) +'/'+ CAST (" + year + " AS nvarchar) AS naslov" +
                " FROM otpremnice " +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=otpremnice.id_izradio " +
                // " LEFT JOIN roba ON roba.sifra=normativi.sifra_artikla " +
                " WHERE otpremnice.broj_otpremnice='" + broj_dokumenta + "' and otpremnice.id_skladiste = '" + skladiste + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste_string.Replace("nvarchar", "varchar")).Fill(dSRlisteTekst, "DTlisteTekst");
            }
        }

        private void SkladisteFinancije()
        {
            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");

            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            if (skladiste != "")
            {
                skladiste = " AND roba_prodaja.id_skladiste='" + skladiste + "'";
            }

            string sql_liste = "SELECT roba_prodaja.kolicina,roba_prodaja.vpc,roba_prodaja.porez,roba_prodaja.nc,roba_prodaja.id_skladiste,skladiste.skladiste " +
                " FROM roba_prodaja" +
                " LEFT JOIN skladiste ON roba_prodaja.id_skladiste = skladiste.id_skladiste" +
                " LEFT JOIN roba ON roba_prodaja.sifra=roba.sifra WHERE roba.oduzmi='DA' " + skladiste;
            DataTable DT = classSQL.select(sql_liste, "roba_prodaja").Tables[0];

            DataRow RowSkl;
            foreach (DataRow row in DT.Rows)
            {
                DataRow[] dataROW = dSRliste.Tables["DTliste"].Select("kolicina = '" + row["id_skladiste"].ToString() + "'");
                decimal vpc = Convert.ToDecimal(row["vpc"].ToString());
                decimal pdv = Convert.ToDecimal(row["porez"].ToString());
                decimal kol = Convert.ToDecimal(row["kolicina"].ToString().Replace(".", ","));
                decimal nbc;
                decimal.TryParse(row["nc"].ToString().Replace(".", ","), out nbc);
                //if (kol > 90)
                //{
                //    MessageBox.Show("");
                //}

                if (dataROW.Count() == 0)
                {
                    RowSkl = dSRliste.Tables["DTliste"].NewRow();
                    RowSkl["kolicina"] = row["id_skladiste"].ToString();
                    RowSkl["naziv"] = row["skladiste"].ToString();
                    RowSkl["cijena1"] = (vpc * kol).ToString();
                    RowSkl["cijena3"] = ((vpc * pdv / 100) * kol).ToString();
                    RowSkl["cijena6"] = ((vpc * pdv / 100) + vpc) * kol;
                    RowSkl["jmj"] = nbc * kol;
                    dSRliste.Tables["DTliste"].Rows.Add(RowSkl);
                }
                else
                {
                    dataROW[0]["cijena1"] = Math.Round(Convert.ToDecimal(dataROW[0]["cijena1"].ToString()) + (vpc * kol), 3);
                    dataROW[0]["cijena6"] = Convert.ToDecimal(dataROW[0]["cijena6"].ToString()) + (((vpc * pdv / 100) + vpc) * kol);
                    dataROW[0]["cijena3"] = Convert.ToDecimal(dataROW[0]["cijena3"].ToString()) + ((vpc * pdv / 100) * kol);
                    dataROW[0]["jmj"] = Convert.ToDecimal(dataROW[0]["jmj"].ToString()) + (nbc * kol);
                }
            }

            //if (classSQL.remoteConnectionString == "")
            //{
            //    classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            //}
            //else
            //{
            //    classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            //}

            string sql_liste_string = "SELECT " +
                " '' AS tbl1," +
                " 'Naziv skladišta' AS tbl2," +
                " 'NBC' AS tbl3," +
                " 'VPC' AS tbl4," +
                " 'PDV' AS tbl5," +
                " '' AS tbl6," +
                " 'MPC' AS tbl8," +
                " '' AS tbl7," +
                " 'Stanje na skladištu financijski' AS naslov";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste_string.Replace("nvarchar", "varchar")).Fill(dSRlisteTekst, "DTlisteTekst");
            }
        }

        private void promjenaCijene()
        {
            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");

            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string sql_liste = "SELECT " +
                " promjena_cijene_stavke.sifra," +
                " roba.naziv," +
                " promjena_cijene_stavke.postotak AS jmj," +
                " promjena_cijene_stavke.stara_cijena AS cijena1 ," +
                " promjena_cijene_stavke.nova_cijena AS cijena3," +
                " promjena_cijene_stavke.kolicina AS cijena4," +
                " (CAST(promjena_cijene_stavke.nova_cijena AS numeric) - CAST(promjena_cijene_stavke.stara_cijena AS numeric))*CAST(REPLACE(promjena_cijene_stavke.kolicina,',','.') AS numeric) AS cijena6," +
                " (CAST(CAST(promjena_cijene_stavke.nova_cijena AS money) - CAST(promjena_cijene_stavke.stara_cijena AS money) AS money)" +
                "      -      " +
                " CAST((CAST(promjena_cijene_stavke.nova_cijena AS money) - CAST(promjena_cijene_stavke.stara_cijena AS money))" +
                "      /      " +
                " CAST('1.'+promjena_cijene_stavke.pdv AS numeric) AS money))*CAST(REPLACE(promjena_cijene_stavke.kolicina,',','.') AS numeric)  AS cijena5" +
                " FROM promjena_cijene_stavke" +
                " LEFT JOIN roba ON roba.sifra=promjena_cijene_stavke.sifra WHERE broj='" + broj_dokumenta + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            string year = "";
            DataTable DT = classSQL.select("SELECT date FROM promjena_cijene WHERE broj='" + broj_dokumenta + "'", "promjena_cijene").Tables[0];

            if (DT.Rows.Count > 0)
            {
                year = Convert.ToDateTime(DT.Rows[0]["date"].ToString()).Year.ToString();
            }

            string sql_liste_string = "SELECT " +
                " 'Šifra' AS tbl1," +
                " 'Naziv' AS tbl2," +
                " 'Postotak' AS tbl3," +
                " 'Stara cijena' AS tbl4," +
                " 'Nova cijena' AS tbl5," +
                " 'Količina' AS tbl6," +
                " 'PDV iznos' AS tbl7," +
                " 'Iznos' AS tbl8," +
                " '1' AS string6," +
                " promjena_cijene.date AS datum1," +
                " promjena_cijene.napomena AS komentar," +
                " skladiste.skladiste AS skladiste," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS string1," +
                " CAST ('ZAPISNIK O PROMJENI CIJENE  ' AS nvarchar) + CAST (promjena_cijene.broj AS nvarchar) +'/'+ CAST (" + year + " AS nvarchar) AS naslov" +
                " FROM promjena_cijene " +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=promjena_cijene.id_skladiste " +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=promjena_cijene.id_izradio " +
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

        private void promjenaCijeneKomadno()
        {
            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");

            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string sql_liste = "SELECT " +
                " promjena_cijene_komadno_stavke.sifra," +
                " roba.naziv," +
                " promjena_cijene_komadno_stavke.postotak AS jmj," +
                " promjena_cijene_komadno_stavke.stara_cijena AS cijena1 ," +
                " promjena_cijene_komadno_stavke.nova_cijena AS cijena3," +
                " promjena_cijene_komadno_stavke.pdv AS cijena4," +
                " CAST(promjena_cijene_komadno_stavke.nova_cijena AS money) - CAST(promjena_cijene_komadno_stavke.stara_cijena AS money) AS cijena6," +
                " CAST(CAST(promjena_cijene_komadno_stavke.nova_cijena AS money) - CAST(promjena_cijene_komadno_stavke.stara_cijena AS money) AS money)" +
                "      -      " +
                " CAST((CAST(promjena_cijene_komadno_stavke.nova_cijena AS money) - CAST(promjena_cijene_komadno_stavke.stara_cijena AS money))" +
                "      /      " +
                " CAST('1.'+promjena_cijene_komadno_stavke.pdv AS numeric) AS money)  AS cijena5" +
                " FROM promjena_cijene_komadno_stavke" +
                " LEFT JOIN roba ON roba.sifra=promjena_cijene_komadno_stavke.sifra WHERE broj='" + broj_dokumenta + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            string year = "";
            DataTable DT = classSQL.select("SELECT date FROM promjena_cijene_komadno WHERE broj='" + broj_dokumenta + "'", "promjena_cijene_komadno").Tables[0];

            if (DT.Rows.Count > 0)
            {
                year = Convert.ToDateTime(DT.Rows[0]["date"].ToString()).Year.ToString();
            }

            string sql_liste_string = "SELECT " +
                " 'Šifra' AS tbl1," +
                " 'Naziv' AS tbl2," +
                " 'Postotak' AS tbl3," +
                " 'Stara cijena' AS tbl4," +
                " 'Nova cijena' AS tbl5," +
                " 'PDV' AS tbl6," +
                " 'PDV iznos' AS tbl7," +
                " 'Iznos' AS tbl8," +
                " promjena_cijene_komadno.date AS datum1," +
                " promjena_cijene_komadno.napomena AS komentar," +
                " skladiste.skladiste AS skladiste," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS string1," +
                " CAST ('ZAPISNIK O PROMJENI CIJENE  ' AS nvarchar) + CAST (promjena_cijene_komadno.broj AS nvarchar) +'/'+ CAST (" + year + " AS nvarchar) AS naslov" +
                " FROM promjena_cijene_komadno " +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=promjena_cijene_komadno.id_skladiste " +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=promjena_cijene_komadno.id_izradio " +
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

        private void meduskladisnica()
        {
            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");

            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string sql_liste = "SELECT " +
                " meduskladisnica_stavke.sifra," +
                " roba.naziv," +
                " meduskladisnica_stavke.kolicina AS cijena1," +
                " roba.jm AS jmj," +
                " meduskladisnica_stavke.vpc AS cijena3 ," +
                " meduskladisnica_stavke.mpc AS cijena5," +
                " meduskladisnica_stavke.pdv+'%  / '+ROUND((CAST(meduskladisnica_stavke.vpc AS NUMERIC)*CAST(REPLACE(meduskladisnica_stavke.kolicina,',','.') AS NUMERIC))*(CAST(REPLACE(meduskladisnica_stavke.pdv,',','.') AS NUMERIC))/100,2) AS cijena4," +
                " ROUND((CAST(meduskladisnica_stavke.vpc AS NUMERIC)*CAST(REPLACE(meduskladisnica_stavke.kolicina,',','.') AS NUMERIC))*(CAST(REPLACE(meduskladisnica_stavke.pdv,',','.') AS NUMERIC))/100,2) AS cijena9," +
                " CAST(meduskladisnica_stavke.mpc AS numeric) * CAST(REPLACE(meduskladisnica_stavke.kolicina,',','.') AS numeric) AS cijena6 " +
                " FROM meduskladisnica_stavke" +
                " LEFT JOIN roba ON roba.sifra=meduskladisnica_stavke.sifra WHERE broj='" + broj_dokumenta + "' AND iz_skladista='" + skladiste + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            string year = "";
            DataTable DT = classSQL.select("SELECT datum FROM meduskladisnica WHERE broj='" + broj_dokumenta + "'", "meduskladisnica").Tables[0];

            if (DT.Rows.Count > 0)
            {
                year = Convert.ToDateTime(DT.Rows[0]["datum"].ToString()).Year.ToString();
            }

            string sql_liste_string = "SELECT " +
                " 'Šifra' AS tbl1," +
                " 'Naziv' AS tbl2," +
                " 'Količina' AS tbl4," +
                " 'JMJ' AS tbl3," +
                " 'VPC' AS tbl5," +
                " 'MPC' AS tbl7," +
                " 'PDV / Iznos PDV-a' AS tbl6," +
                " 'Iznos' AS tbl8," +
                " '0' AS string6," +
                " meduskladisnica.datum AS datum1," +
                " meduskladisnica.napomena AS komentar," +
                " 'Iz skladišta: '+skladiste.skladiste + ' u skladište: ' + T2.skladiste AS skladiste," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS string1," +
                " CAST ('MEĐUSKLADIŠNICA  ' AS nvarchar) + CAST (meduskladisnica.broj AS nvarchar) +'/'+ CAST (" + year + " AS nvarchar) AS naslov" +
                " FROM meduskladisnica " +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=meduskladisnica.id_skladiste_od " +
                " LEFT JOIN skladiste T2 ON T2.id_skladiste = meduskladisnica.id_skladiste_do " +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=meduskladisnica.id_izradio " +
                " WHERE meduskladisnica.broj ='" + broj_dokumenta + "' AND meduskladisnica.id_skladiste_od='" + skladiste + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste_string.Replace("nvarchar", "varchar")).Fill(dSRlisteTekst, "DTlisteTekst");
            }
        }

        private void Normativ()
        {
            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");

            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string sql_liste = "SELECT " +
                " normativi_stavke.sifra_robe as sifra," +
                " roba.naziv," +
                " normativi_stavke.kolicina AS cijena1," +
                " roba.jm AS jmj," +
                " roba_prodaja.vpc AS cijena3 ," +
                " (COALESCE(CAST(roba_prodaja.vpc AS NUMERIC),0)*(COALESCE(CAST(replace(roba_prodaja.porez, ',','.') AS NUMERIC),0) zbroj COALESCE(CAST(roba_prodaja.porez_potrosnja AS NUMERIC),0))/100)zbrojCOALESCE(CAST(roba_prodaja.vpc AS NUMERIC),0) AS cijena5," +
                " skladiste.skladiste AS cijena4, " +
                " (COALESCE(CAST(roba_prodaja.vpc AS NUMERIC),0)*(COALESCE(CAST(replace(roba_prodaja.porez, ',','.') AS NUMERIC),0)zbrojCOALESCE(CAST(roba_prodaja.porez_potrosnja AS NUMERIC),0))/100)zbrojCOALESCE(CAST(roba_prodaja.vpc AS NUMERIC),0)*COALESCE(CAST(REPLACE(normativi_stavke.kolicina,',','.') AS NUMERIC),0) AS cijena6 " +
                " FROM normativi_stavke" +
                " LEFT JOIN roba ON roba.sifra=normativi_stavke.sifra_robe" +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=normativi_stavke.id_skladiste" +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=normativi_stavke.sifra_robe AND roba_prodaja.id_skladiste=normativi_stavke.id_skladiste " +
                " WHERE broj_normativa='" + broj_dokumenta + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            string year = "";
            DataTable DT = classSQL.select("SELECT godina_normativa FROM normativi WHERE broj_normativa='" + broj_dokumenta + "'", "normativi").Tables[0];

            if (DT.Rows.Count > 0)
            {
                year = DT.Rows[0]["godina_normativa"].ToString();
            }

            //DataTable DTnor = classSQL.select("SELECT * FROM normativi WHERE broj_normativa='" + broj_dokumenta + "'", "normativi").Tables[0];

            string sql_liste_string = "SELECT " +
                " 'Šifra' AS tbl1," +
                " 'Naziv' AS tbl2," +
                " 'Količina' AS tbl4," +
                " 'JMJ' AS tbl3," +
                " 'VPC' AS tbl5," +
                " 'MPC' AS tbl7," +
                " 'Skladište' AS tbl6," +
                " 'Iznos' AS tbl8," +
                //"  normativi.godina_normativa AS datum1," +
                "  normativi.komentar+'\r\n\r\nŠIFRA OVOG NORMATIVA JE: '+normativi.sifra_artikla+'\r\nNAZIV ARTIKLA: '+ roba.naziv AS komentar," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS string1," +
                " CAST ('Normativ  ' AS nvarchar) + CAST (normativi.broj_normativa AS nvarchar) +'/'+ CAST (" + year + " AS nvarchar) AS naslov" +
                " FROM normativi " +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=normativi.id_zaposlenik " +
                " LEFT JOIN roba ON roba.sifra=normativi.sifra_artikla " +
                " WHERE normativi.broj_normativa ='" + broj_dokumenta + "'";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste_string.Replace("nvarchar", "varchar")).Fill(dSRlisteTekst, "DTlisteTekst");
            }
        }

        private void PrometPoRobi()
        {
            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");

            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string skl = "";
            if (skladiste != null)
            {
                skl = " AND racun_stavke.id_skladiste='" + skladiste + "'";
            }

            string duc = "";
            if (ducan != null)
            {
                duc = " AND racuni.id_ducan='" + ducan + "'";
            }

            string blag = "";
            if (blagajnik != null)
            {
                blag = " AND racuni.id_blagajnik='" + blagajnik + "'";
            }

            string art = "";
            if (artikl != null)
            {
                art = " AND racun_stavke.sifra_robe='" + artikl + "'";
            }

            string Fskl = "";
            if (skladiste != null)
            {
                Fskl = " AND faktura_stavke.id_skladiste='" + skladiste + "'";
            }

            string Fblag = "";
            if (blagajnik != null)
            {
                Fblag = " AND fakture.id_zaposlenik='" + blagajnik + "'";
            }

            string Fart = "";
            if (artikl != null)
            {
                Fart = " AND faktura_stavke.sifra='" + artikl + "'";
            }

            string sql_liste = "SELECT " +
                " racun_stavke.sifra_robe AS sifra," +
                " roba.naziv AS naziv ," +
                " CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC) AS cijena1," +
                " 'Račun' AS jmj," +
                " roba_prodaja.kolicina AS cijena3 ," +
                " racun_stavke.broj_racuna AS cijena5 ," +
                " racun_stavke.vpc AS cijena6 ," +
                //" (CAST(racun_stavke.vpc AS NUMERIC)*(CAST(REPLACE(racun_stavke.porez,',','.') AS NUMERIC)zbrojCAST(REPLACE(racun_stavke.porez_potrosnja,',','.') AS NUMERIC))/100)zbrojCAST(racun_stavke.vpc AS NUMERIC) AS cijena5," +
                " skladiste.skladiste AS cijena4 " +
                //" (CAST(racun_stavke.vpc AS NUMERIC)*(CAST(REPLACE(racun_stavke.porez,',','.') AS NUMERIC)zbrojCAST(REPLACE(racun_stavke.porez_potrosnja,',','.') AS NUMERIC))/100)zbrojCAST(racun_stavke.vpc AS NUMERIC)*CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC) AS cijena6 " +
                " FROM racun_stavke" +
                " LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe" +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=racun_stavke.id_skladiste" +
                " LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa" +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=racun_stavke.sifra_robe AND roba_prodaja.id_skladiste=racun_stavke.id_skladiste " +
                " WHERE  racuni.datum_racuna>'" + datumOD + "' AND racuni.datum_racuna<'" + datumDO + "'" +
                " " + skl + blag + duc + art +
                "";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            string sql_fak = "SELECT " +
                " faktura_stavke.sifra AS sifra," +
                " roba.naziv AS naziv ," +
                " CAST(REPLACE(faktura_stavke.kolicina,',','.') AS NUMERIC) AS cijena1," +
                " 'Faktura' AS jmj," +
                " roba_prodaja.kolicina AS cijena3 ," +
                " faktura_stavke.broj_fakture AS cijena5 ," +
                " faktura_stavke.vpc AS cijena6 ," +
                //" (CAST(faktura_stavke.vpc AS NUMERIC)*(CAST(REPLACE(faktura_stavke.porez,',','.') AS NUMERIC)zbrojCAST(REPLACE(faktura_stavke.porez_potrosnja,',','.') AS NUMERIC))/100)zbrojCAST(faktura_stavke.vpc AS NUMERIC) AS cijena5," +
                " skladiste.skladiste AS cijena4 " +
                //" (CAST(faktura_stavke.vpc AS NUMERIC)*(CAST(REPLACE(faktura_stavke.porez,',','.') AS NUMERIC)zbrojCAST(REPLACE(faktura_stavke.porez_potrosnja,',','.') AS NUMERIC))/100)zbrojCAST(faktura_stavke.vpc AS NUMERIC)*CAST(REPLACE(faktura_stavke.kolicina,',','.') AS NUMERIC) AS cijena6 " +
                " FROM faktura_stavke" +
                " LEFT JOIN roba ON roba.sifra=faktura_stavke.sifra" +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=faktura_stavke.id_skladiste" +
                " LEFT JOIN fakture ON fakture.broj_fakture=faktura_stavke.broj_fakture" +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=faktura_stavke.sifra AND roba_prodaja.id_skladiste=faktura_stavke.id_skladiste " +
                " WHERE  fakture.date>'" + datumOD + "' AND fakture.date<'" + datumDO + "'" +
                " " + Fskl + Fblag + Fart;

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_fak).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_fak).Fill(dSRliste, "DTliste");
            }

            string year = "";
            //DataTable DT = classSQL.select("SELECT godina_normativa FROM normativi WHERE broj_normativa='" + broj_dokumenta + "'", "normativi").Tables[0];

            //if (DT.Rows.Count > 0)
            // {
            //year = DT.Rows[0]["godina_normativa"].ToString();
            // }

            string s = "";
            if (skladiste != null)
            {
                s = "\r\nSkladište: " + skladiste;
            }

            string b = "";
            if (blagajnik != null)
            {
                b = "\r\nBlagajnik: " + blagajnik;
            }

            string a = "";
            if (artikl != null)
            {
                a = "\r\nArtikl: " + artikl;
            }

            string sql_liste_string = "SELECT " +
                " 'Šifra' AS tbl1," +
                " 'Naziv' AS tbl2," +
                " 'Dokument' AS tbl3," +
                " 'Količina' AS tbl4," +
                " 'Stanje SK' AS tbl5," +
                " 'Skladište' AS tbl6," +
                " 'BR.Dok.' AS tbl7," +
                " 'VPC' AS tbl8," +
                " 'ne' AS string6," +
                " '" + s + "' AS skladiste," +
                " '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "' AS datum1," +
                " CAST ('Promet po robi  ' AS nvarchar) AS naslov," +
                " '\r\nOd datuma:" + datumOD + " - " + datumDO + s + b + a + "' AS komentar" +
                "";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste_string.Replace("nvarchar", "varchar")).Fill(dSRlisteTekst, "DTlisteTekst");
            }
        }

        private void PrometPoRobiBezSkladista()
        {
            string sql1 = SqlPodaciTvrtke.VratiSql("", "", "");

            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            string skl = "";
            if (skladiste != null)
            {
                skl = " AND racun_stavke.id_skladiste='" + skladiste + "'";
            }

            string duc = "";
            if (ducan != null)
            {
                duc = " AND racuni.id_ducan='" + ducan + "'";
            }

            string blag = "";
            if (blagajnik != null)
            {
                blag = " AND racuni.id_blagajnik='" + blagajnik + "'";
            }

            string art = "";
            if (artikl != null)
            {
                art = " AND racun_stavke.sifra_robe='" + artikl + "'";
            }

            string Fskl = "";
            if (skladiste != null)
            {
                Fskl = " AND faktura_stavke.id_skladiste='" + skladiste + "'";
            }

            string Fblag = "";
            if (blagajnik != null)
            {
                Fblag = " AND fakture.id_zaposlenik='" + blagajnik + "'";
            }

            string Fart = "";
            if (artikl != null)
            {
                Fart = " AND faktura_stavke.sifra='" + artikl + "'";
            }

            string sql_liste = "SELECT " +
                " racun_stavke.sifra_robe AS sifra," +
                " roba.naziv AS naziv ," +
                " CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC) AS cijena1," +
                " 'Račun' AS jmj," +
                " roba_prodaja.kolicina AS cijena3 ," +
                " racun_stavke.broj_racuna AS cijena5 ," +
                " racun_stavke.vpc AS cijena6 ," +
                //" (CAST(racun_stavke.vpc AS NUMERIC)*(CAST(REPLACE(racun_stavke.porez,',','.') AS NUMERIC)zbrojCAST(REPLACE(racun_stavke.porez_potrosnja,',','.') AS NUMERIC))/100)zbrojCAST(racun_stavke.vpc AS NUMERIC) AS cijena5," +
                " skladiste.skladiste AS cijena4 " +
                //" (CAST(racun_stavke.vpc AS NUMERIC)*(CAST(REPLACE(racun_stavke.porez,',','.') AS NUMERIC)zbrojCAST(REPLACE(racun_stavke.porez_potrosnja,',','.') AS NUMERIC))/100)zbrojCAST(racun_stavke.vpc AS NUMERIC)*CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC) AS cijena6 " +
                " FROM racun_stavke" +
                " LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe" +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=racun_stavke.id_skladiste" +
                " LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa" +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=racun_stavke.sifra_robe AND roba_prodaja.id_skladiste=racun_stavke.id_skladiste " +
                " WHERE  racuni.datum_racuna>'" + datumOD + "' AND racuni.datum_racuna<'" + datumDO + "'" +
                " " + skl + blag + duc + art +
                "";

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            }

            string sql_fak = "SELECT " +
                " faktura_stavke.sifra AS sifra," +
                " roba.naziv AS naziv ," +
                " CAST(REPLACE(faktura_stavke.kolicina,',','.') AS NUMERIC) AS cijena1," +
                " 'Faktura' AS jmj," +
                " roba_prodaja.kolicina AS cijena3 ," +
                " faktura_stavke.broj_fakture AS cijena5 ," +
                " faktura_stavke.vpc AS cijena6 ," +
                //" (CAST(faktura_stavke.vpc AS NUMERIC)*(CAST(REPLACE(faktura_stavke.porez,',','.') AS NUMERIC)zbrojCAST(REPLACE(faktura_stavke.porez_potrosnja,',','.') AS NUMERIC))/100)zbrojCAST(faktura_stavke.vpc AS NUMERIC) AS cijena5," +
                " skladiste.skladiste AS cijena4 " +
                //" (CAST(faktura_stavke.vpc AS NUMERIC)*(CAST(REPLACE(faktura_stavke.porez,',','.') AS NUMERIC)zbrojCAST(REPLACE(faktura_stavke.porez_potrosnja,',','.') AS NUMERIC))/100)zbrojCAST(faktura_stavke.vpc AS NUMERIC)*CAST(REPLACE(faktura_stavke.kolicina,',','.') AS NUMERIC) AS cijena6 " +
                " FROM faktura_stavke" +
                " LEFT JOIN roba ON roba.sifra=faktura_stavke.sifra" +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=faktura_stavke.id_skladiste" +
                " LEFT JOIN fakture ON fakture.broj_fakture=faktura_stavke.broj_fakture" +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=faktura_stavke.sifra AND roba_prodaja.id_skladiste=faktura_stavke.id_skladiste " +
                " WHERE  fakture.date>'" + datumOD + "' AND fakture.date<'" + datumDO + "'" +
                " " + Fskl + Fblag + Fart;

            if (classSQL.remoteConnectionString == "")
            {
                classSQL.CeAdatpter(sql_fak).Fill(dSRliste, "DTliste");
            }
            else
            {
                classSQL.NpgAdatpter(sql_fak).Fill(dSRliste, "DTliste");
            }

            string year = "";
            //DataTable DT = classSQL.select("SELECT godina_normativa FROM normativi WHERE broj_normativa='" + broj_dokumenta + "'", "normativi").Tables[0];

            //if (DT.Rows.Count > 0)
            // {
            //year = DT.Rows[0]["godina_normativa"].ToString();
            // }

            string s = "";
            if (skladiste != null)
            {
                s = "\r\nSkladište: " + skladiste;
            }

            string b = "";
            if (blagajnik != null)
            {
                b = "\r\nBlagajnik: " + blagajnik;
            }

            string a = "";
            if (artikl != null)
            {
                a = "\r\nArtikl: " + artikl;
            }

            string sql_liste_string = "SELECT " +
                " 'Šifra' AS tbl1," +
                " 'Naziv' AS tbl2," +
                " 'Dokument' AS tbl3," +
                " 'Količina' AS tbl4," +
                " 'Stanje SK' AS tbl5," +
                " 'Skladište' AS tbl6," +
                " 'BR.Dok.' AS tbl7," +
                " 'VPC' AS tbl8," +
                " 'ne' AS string6," +
                " '" + s + "' AS skladiste," +
                " '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "' AS datum1," +
                " CAST ('Promet po robi  ' AS nvarchar) AS naslov," +
                " '\r\nOd datuma:" + datumOD + " - " + datumDO + s + b + a + "' AS komentar" +
                "";

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