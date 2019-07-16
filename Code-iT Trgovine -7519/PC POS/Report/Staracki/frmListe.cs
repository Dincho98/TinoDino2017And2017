using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Report.Staracki
{
    public partial class frmListe : Form
    {
        public frmListe()
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
        public string partner { get; set; }
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
            staracki_izvjestaj();

            this.reportViewer1.RefreshReport();
        }

        private void staracki_izvjestaj()
        {
            string datOD = datumOD.Date.ToString("dd.MM.yyyy 00:00:00");
            string datDO = datumDO.Date.ToString("dd.MM.yyyy 23:59:59");
            string sql1 = "SELECT ime +' '+ prezime as osoba , oib FROM partners where id_partner = '" + partner + "'";

            DataTable DTpodaci = classSQL.select(sql1, "podaci").Tables[0];

            DataRow DTrow = dSRpodaciTvrtke.Tables[0].NewRow();
            DTrow = dSRpodaciTvrtke.Tables[0].NewRow();
            DTrow["ime_tvrtke"] = DTpodaci.Rows[0]["osoba"].ToString();
            DTrow["oib"] = DTpodaci.Rows[0]["oib"].ToString();
            DTrow["tel"] = datumOD.Date.ToString("dd.MM.yyyy");
            DTrow["fax"] = datumDO.Date.ToString("dd.MM.yyyy");
            dSRpodaciTvrtke.Tables[0].Rows.Add(DTrow);

            string sql = "SELECT ukupno_mpc, ukupno, broj_racuna, datum_racuna, napomena FROM racuni WHERE id_kupac = '" + partner + "' AND datum_racuna >= '" + datOD + "' AND datum_racuna <= '" + datDO + "'";

            DataTable DTpopis = classSQL.select(sql, "popis").Tables[0];

            for (int i = 0; i < DTpopis.Rows.Count; i++)
            {
                decimal uk = 0;
                string ime = classSQL.select("Select ime From partners Where id_partner = '" + partner + "'", "ime").Tables[0].Rows[0]["ime"].ToString();
                string prezime = classSQL.select("Select prezime From partners Where id_partner = '" + partner + "'", "prezime").Tables[0].Rows[0]["prezime"].ToString();
                if (DTpopis.Rows[i]["ukupno_mpc"].ToString() != "")
                {
                    uk = Convert.ToDecimal(DTpopis.Rows[i]["ukupno_mpc"].ToString());
                }
                else
                {
                    uk = Convert.ToDecimal(DTpopis.Rows[i]["ukupno"].ToString());
                }

                string br = DTpopis.Rows[i]["broj_racuna"].ToString();
                DateTime date = Convert.ToDateTime(DTpopis.Rows[i]["datum_racuna"].ToString());
                string napomena = DTpopis.Rows[i]["napomena"].ToString();

                DataRow DTrow1 = dSRlisteTekst.Tables[0].NewRow();
                DTrow1 = dSRlisteTekst.Tables[0].NewRow();
                DTrow1["naslov"] = ime + " " + prezime;
                DTrow1["string3"] = date.ToString("dd.MM.yyyy");
                DTrow1["string1"] = br;
                DTrow1["ukupno1"] = uk;
                DTrow1["string2"] = napomena;
                dSRlisteTekst.Tables[0].Rows.Add(DTrow1);
            }

            //classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            //string sql_liste = "SELECT " +
            //    " promjena_cijene_stavke.sifra," +
            //    " roba.naziv," +
            //    " promjena_cijene_stavke.postotak AS jmj," +
            //    " promjena_cijene_stavke.stara_cijena AS cijena1 ," +
            //    " promjena_cijene_stavke.nova_cijena AS cijena3," +
            //    " promjena_cijene_stavke.pdv AS cijena4," +
            //    " CAST(promjena_cijene_stavke.nova_cijena AS money) - CAST(promjena_cijene_stavke.stara_cijena AS money) AS cijena6," +
            //    " CAST(CAST(promjena_cijene_stavke.nova_cijena AS money) - CAST(promjena_cijene_stavke.stara_cijena AS money) AS money)" +
            //    "      -      " +
            //    " CAST((CAST(promjena_cijene_stavke.nova_cijena AS money) - CAST(promjena_cijene_stavke.stara_cijena AS money))" +
            //    "      /      " +
            //    " CAST('1.'+promjena_cijene_stavke.pdv AS numeric) AS money)  AS cijena5" +
            //    " FROM promjena_cijene_stavke" +
            //    " LEFT JOIN roba ON roba.sifra=promjena_cijene_stavke.sifra WHERE broj='" + broj_dokumenta + "'";

            //if (classSQL.remoteConnectionString == "")
            //{
            //    classSQL.CeAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            //}
            //else
            //{
            //    classSQL.NpgAdatpter(sql_liste).Fill(dSRliste, "DTliste");
            //}

            //string year = "";
            //DataTable DT = classSQL.select("SELECT date FROM promjena_cijene WHERE broj='" + broj_dokumenta + "'", "promjena_cijene").Tables[0];

            //if (DT.Rows.Count > 0)
            //{
            //    year = Convert.ToDateTime(DT.Rows[0]["date"].ToString()).Year.ToString();
            //}

            //string sql_liste_string = "SELECT " +
            //    " 'Šifra' AS tbl1," +
            //    " 'Naziv' AS tbl2," +
            //    " 'Postotak' AS tbl3," +
            //    " 'Stara cijena' AS tbl4," +
            //    " 'Nova cijena' AS tbl5," +
            //    " 'PDV' AS tbl6," +
            //    " 'PDV iznos' AS tbl7," +
            //    " 'Iznos' AS tbl8," +
            //    " promjena_cijene.date AS datum1," +
            //    " promjena_cijene.napomena AS komentar," +
            //    " skladiste.skladiste AS skladiste," +
            //    " zaposlenici.ime + ' ' + zaposlenici.prezime AS string1," +
            //    " CAST ('ZAPISNIK O PROMJENI CIJENE  ' AS nvarchar) + CAST (promjena_cijene.broj AS nvarchar) +'/'+ CAST (" + year + " AS nvarchar) AS naslov" +
            //    " FROM promjena_cijene " +
            //    " LEFT JOIN skladiste ON skladiste.id_skladiste=promjena_cijene.id_skladiste " +
            //    " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=promjena_cijene.id_izradio " +
            //    " WHERE broj ='" + broj_dokumenta + "'";

            //if (classSQL.remoteConnectionString == "")
            //{
            //    classSQL.CeAdatpter(sql_liste_string).Fill(dSRlisteTekst, "DTlisteTekst");
            //}
            //else
            //{
            //    classSQL.NpgAdatpter(sql_liste_string.Replace("nvarchar", "varchar")).Fill(dSRlisteTekst, "DTlisteTekst");
            //}
        }
    }
}