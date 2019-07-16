using Npgsql;
using PCPOS.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmStanjePoZadnjojInventuri : Form
    {
        public frmStanjePoZadnjojInventuri()
        {
            InitializeComponent();
        }

        public frmMenu MainFormMenu { get; set; }

        private DateTime datumInventure;
        private string skladiste;
        private string inventura;
        private bool inventuraIzProsleGodine = false;
        private bool danInventure { get; set; }
        private PCPOS.Util.classFukcijeZaUpravljanjeBazom tmp = new PCPOS.Util.classFukcijeZaUpravljanjeBazom("MALOPRODAJA", "POS");
        private int pocetnoStanjeKolicina;
        private int kolicinaRobeNakonPocetnog;

        private DataSet DSSkalkulacija_stavke = new DataSet();

        private void frmStanjePoZadnjojInventuri_Load(object sender, EventArgs e)
        {
            fillCBSkladiste();
            nuGodina.Value = DateTime.Now.Year;

            if (inventura != null && skladiste != null)
            {
                fillInventura(inventura, skladiste);
                cbSkladiste.SelectedValue = skladiste;
            }
            else
            {
                inventura = "";
                skladiste = "1";
            }
        }

        private void fillCBSkladiste()
        {
            DataTable DT_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste").Tables[0];
            cbSkladiste.DataSource = DT_Skladiste;
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 1000);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void fillInventura(string broj_inventure_edit, string skl)
        {
            dgw.DataSource = null;

            string sql1 = @"SELECT
                            inventura_stavke.sifra_robe as [sifra],
                            inventura_stavke.naziv as [Naziv],
                            inventura_stavke.jmj as [Jmj],
                            inventura_stavke.kolicina as [Kolicina],
                            inventura_stavke.id_stavke as [ID_Stavke],
                            inventura.datum as [Datum],
                            inventura_stavke.kolicina_koja_je_bila AS [Kolicina_na_skladistu],
                            inventura_stavke.nbc AS [Nabavna cijena],
                            inventura_stavke.porez AS [Porez],
                            inventura_stavke.cijena AS [Prodajna cijena],
                            inventura_stavke.povratna_naknada
                            FROM inventura_stavke
                            LEFT JOIN inventura ON inventura_stavke.broj_inventure=inventura.broj_inventure
                            WHERE inventura_stavke.broj_inventure='" + broj_inventure_edit + @"' AND inventura.id_skladiste='" + skl + @"'
                            AND inventura.godina='" + nuGodina.Value.ToString() + @"' ORDER BY sifra_robe";

            DataTable DTinventura_stavke = classSQL.select(sql1, "inventura_stavke").Tables[0];

            if (DTinventura_stavke.Rows.Count == 0)
            {
                lblDate.Text = "Nema inventure za odabrano skladište!";
                return;
            }

            lblDate.Text = "Datum učitane inventure je: " + DTinventura_stavke.Rows[0]["datum"].ToString();
            datumInventure = Convert.ToDateTime(DTinventura_stavke.Rows[0]["datum"].ToString());

            DataTable dt = vratiDatatable();
            DataRow row;
            decimal zbroj = 0;
            for (int br = 0; br < DTinventura_stavke.Rows.Count; br++)
            {
                row = dt.NewRow();

                row["Broj"] = br + 1;
                row["sifra"] = DTinventura_stavke.Rows[br]["sifra"].ToString();
                row["Naziv"] = DTinventura_stavke.Rows[br]["Naziv"].ToString();
                row["Jmj"] = DTinventura_stavke.Rows[br]["Jmj"].ToString();
                row["Inv_kolicina"] = DTinventura_stavke.Rows[br]["Kolicina"].ToString();
                row["id_stavke"] = DTinventura_stavke.Rows[br]["id_stavke"].ToString();

                decimal nbc, porez, prodajna, povratna, kol;
                row["Kolicina_na_skladistu"] = DTinventura_stavke.Rows[br]["Kolicina_na_skladistu"].ToString();

                decimal.TryParse(DTinventura_stavke.Rows[br]["Kolicina_na_skladistu"].ToString().Replace(".", ","), out kol);
                decimal.TryParse(DTinventura_stavke.Rows[br]["Nabavna cijena"].ToString(), out nbc);
                decimal.TryParse(DTinventura_stavke.Rows[br]["Porez"].ToString(), out porez);
                decimal.TryParse(DTinventura_stavke.Rows[br]["Prodajna cijena"].ToString(), out prodajna);
                decimal.TryParse(DTinventura_stavke.Rows[br]["povratna_naknada"].ToString(), out povratna);

                zbroj += nbc * kol;

                row["Nabavna cijena"] = nbc;
                row["Porez"] = porez;
                row["Prodajna cijena"] = prodajna;
                row["povratna_naknada"] = povratna;

                dt.Rows.Add(row);
            }

            dgw.DataSource = dt;
            dgw.Columns["id_stavke"].Visible = false;
            dgw.Columns["povratna_naknada"].Visible = false;
        }

        private DataTable vratiDatatable()
        {
            DataTable dt = new DataTable();
            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Broj";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "sifra";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Naziv";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Jmj";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "Inv_kolicina";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "id_stavke";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "Kolicina_na_skladistu";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "Nabavna cijena";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "Porez";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "Prodajna cijena";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "povratna_naknada";
            dt.Columns.Add(column);

            return dt;
        }

        private void btnZadnjaInv_Click(object sender, EventArgs e)
        {
            inventura = brojInventure(cbSkladiste.SelectedValue.ToString());
            skladiste = cbSkladiste.SelectedValue.ToString();
            fillInventura(inventura, skladiste);
            if (inventura == "0" && tmp.PostojiProslaGodina())
            {
                classSQL.remoteConnection = new NpgsqlConnection(classSQL.remoteConnectionString.Replace(tmp.UzmiGodinuKojaSeKoristi().ToString(), (tmp.UzmiGodinuKojaSeKoristi() - 1).ToString()));
                inventuraIzProsleGodine = true;
                inventura = brojInventure(cbSkladiste.SelectedValue.ToString());
                skladiste = cbSkladiste.SelectedValue.ToString();
                fillInventura(inventura, skladiste);
                classSQL.remoteConnection = new NpgsqlConnection(classSQL.remoteConnectionString);
            }
        }

        private void btnPostavi_Click(object sender, EventArgs e)
        {
            PostaviInventuru(false);
        }

        private PCPOS.Util.classFukcijeZaUpravljanjeBazom baza = new Util.classFukcijeZaUpravljanjeBazom("MALOPRODAJA", "POS");

        private void PostaviInventuru(bool pocetno)
        {
            ControlEnableDisable(false);

            if (dgw.Rows.Count > 0)
            {
                List<DataTable> Ldt = UzmiKolicineIzOveIprosleGodine();
                DataTable DTsadasnja_godina = Ldt[0];
                DataTable DTprosla_godina = Ldt[1];
                DateTime datumspremanja = DateTime.Now;

                if (pocetno) classSQL.update("DELETE FROM pocetno WHERE id_skladiste='" + skladiste + "'");

                DataTable DTroba = classSQL.select("SELECT * FROM roba", "roba").Tables[0];

                foreach (DataGridViewRow r in dgw.Rows)
                {
                    decimal _kolicinaSadasnjaGodina = 0, _kolicinaPrijasnjaGodina = 0, ukupna_kolicina_za_spremiti = 0, inventurna_kolicina = 0, kolicina_pocetnog = 0, kolicina = 0;

                    if (DTsadasnja_godina.Rows.Count > 0)
                    {
                        DataRow[] rowSG = DTsadasnja_godina.Select("sifra='" + r.Cells["Sifra"].FormattedValue.ToString() + "'");
                        if (rowSG.Length > 0)
                        {
                            decimal.TryParse(rowSG[0]["kolicina"].ToString(), out _kolicinaSadasnjaGodina);
                        }
                    }

                    if (DTprosla_godina.Rows.Count > 0)
                    {
                        DataRow[] rowPG = DTprosla_godina.Select("sifra='" + r.Cells["Sifra"].FormattedValue.ToString() + "'");
                        if (rowPG.Length > 0)
                        {
                            decimal.TryParse(rowPG[0]["kolicina"].ToString(), out _kolicinaPrijasnjaGodina);
                        }
                    }

                    ///
                    ///OVAJ DIO je da uzme kolicinu sa popisane inventure
                    /// inventurna_kolicina.ToString().Replace(".", ",")
                    string sifra = r.Cells["Sifra"].FormattedValue.ToString();
                    //IzracunajKolicinu(sifra);
                    decimal.TryParse(r.Cells["Inv_kolicina"].FormattedValue.ToString(), out inventurna_kolicina);
                    //kolicina_pocetnog = inventurna_kolicina - _kolicinaPrijasnjaGodina;
                    ukupna_kolicina_za_spremiti = inventurna_kolicina + _kolicinaSadasnjaGodina;
                    string sqlupdate = "UPDATE roba_prodaja SET kolicina='" + ukupna_kolicina_za_spremiti.ToString().Replace('.', ',') + "'" +
                        " WHERE sifra='" + sifra + "'" +
                        " AND id_skladiste='" + skladiste + "'";

                    classSQL.update(sqlupdate);

                    if (pocetno)
                    {
                        /*" roba_prodaja.nbc AS [Nabavna cijena], " +
                                    " roba_prodaja.porez AS [Porez], " +
                                    " roba_prodaja.cijena AS [Prodajna cijena], " +

                        string sql = "ALTER TABLE pocetno ADD COLUMN nbc numeric DEFAULT 0;";
                        sql += "ALTER TABLE pocetno ADD COLUMN porez int DEFAULT 0;";
                        sql += "ALTER TABLE pocetno ADD COLUMN povratna_naknada numeric DEFAULT 0;";*/

                        //DataRow[] rroba = DTroba.Select("sifra='" + r.Cells["sifra"].FormattedValue.ToString() + "'");

                        decimal nbc, porez, povratna_naknada, cijena;
                        decimal.TryParse(r.Cells["Nabavna cijena"].FormattedValue.ToString(), out nbc);
                        decimal.TryParse(r.Cells["Porez"].FormattedValue.ToString(), out porez);
                        decimal.TryParse(r.Cells["povratna_naknada"].FormattedValue.ToString(), out povratna_naknada);
                        decimal.TryParse(r.Cells["Prodajna cijena"].FormattedValue.ToString(), out cijena);

                        string datum = "";
                        if (danInventure)
                            datum = datumInventure.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                        else
                            datum = baza.UzmiGodinuKojaSeKoristi().ToString() + "-01-01 00:00:00";

                        string sql = "INSERT INTO pocetno (sifra,novo,id_skladiste,kolicina,datum,datum_postave,prodajna_cijena,porez,nbc,povratna_naknada) VALUES (" +
                            "'" + r.Cells["sifra"].FormattedValue.ToString() + "'," +
                            "'1'," +
                            "'" + skladiste + "'," +
                            "'" + inventurna_kolicina.ToString().Replace(".", ",") + "'," +
                            "'" + datum + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                            "'" + cijena.ToString().Replace(",", ".") + "'," +
                            "'" + Math.Round(porez).ToString() + "'," +
                            "'" + nbc.ToString().Replace(",", ".") + "'," +
                            "'" + povratna_naknada.ToString().Replace(",", ".") + "'" +
                            ");";
                        classSQL.update(sql);
                    }
                }

                MessageBox.Show("Stanje je uspješno postavljeno!", "Stanje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Nije odabrana inventura.");
            }

            ControlEnableDisable(true);
        }

        private decimal kol_normativ = 0;
        private decimal kolStavka = 0;
        private decimal kolRobe = 0;
        private decimal kolSpremi = 0;
        private DataTable DTRobaProdaja;

        /*void Inventura()
		{
			//riješeno je to u prijašnjoj metodi
			//OstaloNaNulu();

			Racuni();

			Fakture();

			Medjuskladisnice();

			Kalkulacije();

			PovratRobe();

			OtpisRobe();

			////NE TREBA INVENTURA ZA SERVIS!!!!
			//RadniNalogServis();

			Otpremnice();

			RadniNalozi();

			Primke();

			Izdatnice();
		}

		private void backgroundWorkerSetKolicina_DoWork(object sender, DoWorkEventArgs e)
		{
			Inventura();
		}*/

        /*private void Racuni()
        {
            string sql = "SELECT " +
                " SUM(CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC)) AS kolicina," +
                " racun_stavke.sifra_robe as sifra" +
                " FROM racun_stavke " +
                " LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna " +
                " WHERE racuni.datum_racuna>'" + datumInventure.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " AND racun_stavke.id_skladiste='" + skladiste + "'" +
                " GROUP BY racun_stavke.sifra_robe";

            DataTable DTR = classSQL.select(sql, "racun_stavke").Tables[0];

            DataRow r;
            string sifra;
            for (int i = 0; i < DTR.Rows.Count; i++)
            {
                r = DTR.Rows[i];
                sifra = r["sifra"].ToString();
                sql = SqlSelectKolicinaWhereRobaSkladiste(sifra, skladiste);
                DTRobaProdaja = classSQL.select(sql, "roba_prodaja").Tables[0];

                kolRobe = DTRobaProdaja.Rows.Count > 0 ? Convert.ToDecimal(DTRobaProdaja.Rows[0]["kolicina"].ToString()) : 0;
                kolStavka = Convert.ToDecimal(r["kolicina"].ToString());

                kolSpremi = kolRobe - kolStavka;

                UpdateSkladiste(skladiste, sifra, kolSpremi.ToString());
            }
        }

        private void Fakture()
        {
            string sql = "SELECT " +
                " SUM(CAST(REPLACE(faktura_stavke.kolicina,',','.') AS NUMERIC)) AS kolicina," +
                " faktura_stavke.sifra," +
                " fakture.oduzmi_iz_skladista" +
                " FROM faktura_stavke " +
                " LEFT JOIN fakture ON fakture.broj_fakture=faktura_stavke.broj_fakture " +
                " WHERE fakture.date>'" + datumInventure.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " AND faktura_stavke.id_skladiste='" + skladiste + "'" +
                " GROUP BY faktura_stavke.sifra, fakture.oduzmi_iz_skladista";

            DataTable DTF = classSQL.select(sql, "faktura_stavke").Tables[0];

            DataRow r;
            string sifra;
            for (int i = 0; i < DTF.Rows.Count; i++)
            {
                r = DTF.Rows[i];
                sifra = r["sifra"].ToString();
                sql = SqlSelectKolicinaWhereRobaSkladiste(sifra, skladiste);
                DTRobaProdaja = classSQL.select(sql, "roba_prodaja").Tables[0];

                kolRobe = DTRobaProdaja.Rows.Count > 0 ? Convert.ToDecimal(DTRobaProdaja.Rows[0]["kolicina"].ToString()) : 0;
                kolStavka = Convert.ToDecimal(r["kolicina"].ToString());

                kolSpremi = r["oduzmi_iz_skladista"].ToString() == "1" ? kolRobe - kolStavka : kolRobe + kolStavka; ;
                //kolSpremi = kolRobe - kolStavka;

                UpdateSkladiste(skladiste, sifra, kolSpremi.ToString());
            }
        }

        private void Medjuskladisnice()
        {
            string sql = "SELECT " +
                " SUM(CAST(REPLACE(meduskladisnica_stavke.kolicina,',','.') AS NUMERIC)) AS kolicina," +
                " meduskladisnica_stavke.sifra" +
                " FROM meduskladisnica_stavke " +
                " LEFT JOIN meduskladisnica ON meduskladisnica.broj=meduskladisnica_stavke.broj " +
                " WHERE meduskladisnica.datum>'" + datumInventure.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " AND meduskladisnica.id_skladiste_od='" + skladiste + "'" +
                " GROUP BY meduskladisnica_stavke.sifra";

            DataTable DTMedjuOdSkladista = classSQL.select(sql, "povrat_robe_stavke").Tables[0];

            MedjuskladisniceHelper(DTMedjuOdSkladista, false);

            sql = "SELECT " +
                " SUM(CAST(REPLACE(meduskladisnica_stavke.kolicina,',','.') AS NUMERIC)) AS kolicina," +
                " meduskladisnica_stavke.sifra" +
                " FROM meduskladisnica_stavke " +
                " LEFT JOIN meduskladisnica ON meduskladisnica.broj=meduskladisnica_stavke.broj " +
                " WHERE meduskladisnica.datum>'" + datumInventure.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " AND meduskladisnica.id_skladiste_do='" + skladiste + "'" +
                " GROUP BY meduskladisnica_stavke.sifra";

            DataTable DTMedjuDoSkladista = classSQL.select(sql, "povrat_robe_stavke").Tables[0];

            MedjuskladisniceHelper(DTMedjuDoSkladista, true);
        }

        private void MedjuskladisniceHelper(DataTable dt, bool plus)
        {
            string sql;
            DataRow r;
            string sifra;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                r = dt.Rows[i];
                sifra = r["sifra"].ToString();
                sql = SqlSelectKolicinaWhereRobaSkladiste(sifra, skladiste);
                DTRobaProdaja = classSQL.select(sql, "roba_prodaja").Tables[0];

                kolRobe = DTRobaProdaja.Rows.Count > 0 ? Convert.ToDecimal(DTRobaProdaja.Rows[0]["kolicina"].ToString()) : 0;
                kolStavka = Convert.ToDecimal(r["kolicina"].ToString());

                kolSpremi = plus ? kolRobe + kolStavka : kolRobe - kolStavka;

                UpdateSkladiste(skladiste, sifra, kolSpremi.ToString());
            }
        }

        private void Kalkulacije()
        {
            string sql = "SELECT " +
                " SUM(CAST(REPLACE(kalkulacija_stavke.kolicina,',','.') AS NUMERIC)) AS kolicina," +
                " kalkulacija_stavke.sifra" +
                " FROM kalkulacija_stavke " +
                " LEFT JOIN kalkulacija ON kalkulacija.broj=kalkulacija_stavke.broj " +
                " WHERE kalkulacija.datum>'" + datumInventure.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " AND kalkulacija.id_skladiste='" + skladiste + "'" +
                " GROUP BY kalkulacija_stavke.sifra";

            DataTable DTm = classSQL.select(sql, "kalkulacija_stavke").Tables[0];

            DataRow r;
            string sifra;
            for (int i = 0; i < DTm.Rows.Count; i++)
            {
                r = DTm.Rows[i];
                sifra = r["sifra"].ToString();
                sql = SqlSelectKolicinaWhereRobaSkladiste(sifra, skladiste);
                DTRobaProdaja = classSQL.select(sql, "roba_prodaja").Tables[0];

                kolRobe = DTRobaProdaja.Rows.Count > 0 ? Convert.ToDecimal(DTRobaProdaja.Rows[0]["kolicina"].ToString()) : 0;
                kolStavka = Convert.ToDecimal(r["kolicina"].ToString());

                kolSpremi = kolRobe + kolStavka;

                UpdateSkladiste(skladiste, sifra, kolSpremi.ToString());
            }
        }

        private void PovratRobe()
        {
            string sql = "SELECT " +
                " SUM(CAST(REPLACE(povrat_robe_stavke.kolicina,',','.') AS NUMERIC)) AS kolicina," +
                " povrat_robe_stavke.sifra" +
                " FROM povrat_robe_stavke " +
                " LEFT JOIN povrat_robe ON povrat_robe.broj=povrat_robe_stavke.broj " +
                " WHERE povrat_robe.datum>'" + datumInventure.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " AND povrat_robe.id_skladiste='" + skladiste + "'" +
                " GROUP BY povrat_robe_stavke.sifra";

            DataTable DTo = classSQL.select(sql, "povrat_robe_stavke").Tables[0];

            DataRow r;
            string sifra;
            for (int i = 0; i < DTo.Rows.Count; i++)
            {
                r = DTo.Rows[i];
                sifra = r["sifra"].ToString();
                sql = SqlSelectKolicinaWhereRobaSkladiste(sifra, skladiste);
                DTRobaProdaja = classSQL.select(sql, "roba_prodaja").Tables[0];

                kolRobe = DTRobaProdaja.Rows.Count > 0 ? Convert.ToDecimal(DTRobaProdaja.Rows[0]["kolicina"].ToString()) : 0;
                kolStavka = Convert.ToDecimal(r["kolicina"].ToString());

                //ili + zbog povrata?!
                kolSpremi = kolRobe - kolStavka;

                UpdateSkladiste(skladiste, sifra, kolSpremi.ToString());
            }
        }

        private void OtpisRobe()
        {
            string sql = "SELECT " +
                " SUM(CAST(REPLACE(otpis_robe_stavke.kolicina,',','.') AS NUMERIC)) AS kolicina," +
                " otpis_robe_stavke.sifra" +
                " FROM otpis_robe_stavke " +
                " LEFT JOIN otpis_robe ON otpis_robe.broj=otpis_robe_stavke.broj " +
                " WHERE otpis_robe.datum>'" + datumInventure.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " AND otpis_robe.id_skladiste='" + skladiste + "'" +
                " GROUP BY otpis_robe_stavke.sifra";

            DataTable DTo = classSQL.select(sql, "otpis_robe_stavke").Tables[0];

            DataRow r;
            string sifra;
            for (int i = 0; i < DTo.Rows.Count; i++)
            {
                r = DTo.Rows[i];
                sifra = r["sifra"].ToString();
                sql = SqlSelectKolicinaWhereRobaSkladiste(sifra, skladiste);
                DTRobaProdaja = classSQL.select(sql, "roba_prodaja").Tables[0];

                kolRobe = DTRobaProdaja.Rows.Count > 0 ? Convert.ToDecimal(DTRobaProdaja.Rows[0]["kolicina"].ToString()) : 0;
                kolStavka = Convert.ToDecimal(r["kolicina"].ToString());

                //ili + zbog povrata?!
                kolSpremi = kolRobe - kolStavka;

                UpdateSkladiste(skladiste, sifra, kolSpremi.ToString());
            }
        }

        private void Otpremnice()
        {
            string sql = "SELECT " +
                " SUM(CAST(REPLACE(otpremnica_stavke.kolicina,',','.') AS NUMERIC)) AS kolicina," +
                " otpremnica_stavke.sifra_robe as sifra" +
                " FROM otpremnica_stavke " +
                " LEFT JOIN otpremnice ON otpremnice.broj_otpremnice=otpremnica_stavke.broj_otpremnice " +
                " WHERE otpremnice.datum>'" + datumInventure.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " AND otpremnice.id_skladiste='" + skladiste + "'" +
                " GROUP BY otpremnica_stavke.sifra_robe";

            DataTable DToT = classSQL.select(sql, "otpremnica_stavke").Tables[0];

            DataRow r;
            string sifra;
            for (int i = 0; i < DToT.Rows.Count; i++)
            {
                r = DToT.Rows[i];
                sifra = r["sifra"].ToString();
                sql = SqlSelectKolicinaWhereRobaSkladiste(sifra, skladiste);
                DTRobaProdaja = classSQL.select(sql, "roba_prodaja").Tables[0];

                kolRobe = DTRobaProdaja.Rows.Count > 0 ? Convert.ToDecimal(DTRobaProdaja.Rows[0]["kolicina"].ToString()) : 0;
                kolStavka = Convert.ToDecimal(r["kolicina"].ToString());

                kolSpremi = kolRobe - kolStavka;

                UpdateSkladiste(skladiste, sifra, kolSpremi.ToString());
            }
        }

        private void Primke()
        {
            string sql = "SELECT " +
                " SUM(CAST(REPLACE(primka_stavke.kolicina,',','.') AS NUMERIC)) AS kolicina," +
                " primka_stavke.sifra" +
                " FROM primka_stavke " +
                " LEFT JOIN primka ON primka.id_primka=primka_stavke.id_primka " +
                " WHERE primka.datum>'" + datumInventure.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " AND primka.id_skladiste='" + skladiste + "'" +
                " GROUP BY primka_stavke.sifra";

            DataTable DTR = classSQL.select(sql, "primka_stavke").Tables[0];

            DataRow r;
            string sifra;
            for (int i = 0; i < DTR.Rows.Count; i++)
            {
                r = DTR.Rows[i];
                sifra = r["sifra"].ToString();
                sql = SqlSelectKolicinaWhereRobaSkladiste(sifra, skladiste);
                DTRobaProdaja = classSQL.select(sql, "roba_prodaja").Tables[0];

                kolRobe = DTRobaProdaja.Rows.Count > 0 ? Convert.ToDecimal(DTRobaProdaja.Rows[0]["kolicina"].ToString()) : 0;
                kolStavka = Convert.ToDecimal(r["kolicina"].ToString());

                kolSpremi = kolRobe + kolStavka;

                UpdateSkladiste(skladiste, sifra, kolSpremi.ToString());
            }
        }

        private void Izdatnice()
        {
            string sql = "SELECT " +
                " SUM(CAST(REPLACE(izdatnica_stavke.kolicina,',','.') AS NUMERIC)) AS kolicina," +
                " izdatnica_stavke.sifra" +
                " FROM izdatnica_stavke " +
                " LEFT JOIN izdatnica ON izdatnica.id_izdatnica=izdatnica_stavke.id_izdatnica " +
                " WHERE izdatnica.datum>'" + datumInventure.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " AND izdatnica.id_skladiste='" + skladiste + "'" +
                " GROUP BY izdatnica_stavke.sifra";

            DataTable DTR = classSQL.select(sql, "izdatnica_stavke").Tables[0];

            DataRow r;
            string sifra;
            for (int i = 0; i < DTR.Rows.Count; i++)
            {
                r = DTR.Rows[i];
                sifra = r["sifra"].ToString();
                sql = SqlSelectKolicinaWhereRobaSkladiste(sifra, skladiste);
                DTRobaProdaja = classSQL.select(sql, "roba_prodaja").Tables[0];

                kolRobe = DTRobaProdaja.Rows.Count > 0 ? Convert.ToDecimal(DTRobaProdaja.Rows[0]["kolicina"].ToString()) : 0;
                kolStavka = Convert.ToDecimal(r["kolicina"].ToString());

                //- za primke, + za izdatnice
                kolSpremi = kolRobe - kolStavka;

                UpdateSkladiste(skladiste, sifra, kolSpremi.ToString());
            }
        }

        private void RadniNalozi()
        {
            string sql = "SELECT " +
                " SUM(CAST(REPLACE(radni_nalog_stavke.kolicina,',','.') AS NUMERIC)) AS kolicina," +
                " radni_nalog_stavke.sifra_robe as sifra," +
                " radni_nalog_stavke.id_skladiste as skladiste" +
                " FROM radni_nalog_stavke " +
                " LEFT JOIN radni_nalog ON radni_nalog.broj_naloga=radni_nalog_stavke.broj_naloga " +
                " WHERE radni_nalog.datum_naloga>'" + datumInventure.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                //" AND radni_nalog_stavke.id_skladiste='" + skladiste + "'" +
                " GROUP BY radni_nalog_stavke.sifra_robe,radni_nalog_stavke.id_skladiste";

            DataTable DTR = classSQL.select(sql, "radni_nalog_stavke").Tables[0];

            DataRow r;
            string sifra;
            string kolicina;
            for (int i = 0; i < DTR.Rows.Count; i++)
            {
                r = DTR.Rows[i];
                sifra = r["sifra"].ToString();//Sifra normativa
                kolicina = r["kolicina"].ToString();//količina normativa

                //oduzimi robu po stavkama normativa (množeno s količinom)
                UpdatePoNormativu(sifra, kolicina);

                //ako je stavka s istog skladišta na kojem vršimo inventuru onda ažuriraj
                //ako nije onda NE ažuriraj skladište
                if (r["skladiste"].ToString() == skladiste)
                {
                    sql = SqlSelectKolicinaWhereRobaSkladiste(sifra, skladiste);
                    DTRobaProdaja = classSQL.select(sql, "roba_prodaja").Tables[0];

                    kolRobe = DTRobaProdaja.Rows.Count > 0 ? Convert.ToDecimal(DTRobaProdaja.Rows[0]["kolicina"].ToString()) : 0;
                    kolStavka = Convert.ToDecimal(kolicina);

                    //18.4.2013 stavljeno +
                    //znači, r.n. dodaje na skladište ali se normativi oduzimaju
                    kolSpremi = kolRobe + kolStavka;

                    UpdateSkladiste(skladiste, sifra, kolSpremi.ToString());
                }
            }
        }*/

        private void UpdatePoNormativu(string sifraNormativa, string kolicinaNormativa)
        {
            string sql = "SELECT " +
                " SUM(CAST(REPLACE(normativi_stavke.kolicina,',','.') AS NUMERIC)) AS kolicina," +
                " normativi_stavke.sifra_robe as sifra" +
                " FROM normativi_stavke " +
                " LEFT JOIN normativi ON normativi.broj_normativa=normativi_stavke.broj_normativa " +
                " WHERE normativi_stavke.id_skladiste='" + skladiste + "'" +
                " AND normativi.sifra_artikla = '" + sifraNormativa + "'" +
                " GROUP BY normativi_stavke.sifra_robe";

            DataTable DTR = classSQL.select(sql, "normativi_stavke").Tables[0];

            DataRow r;
            string sifra;
            for (int i = 0; i < DTR.Rows.Count; i++)
            {
                r = DTR.Rows[i];
                sifra = r["sifra"].ToString();
                sql = SqlSelectKolicinaWhereRobaSkladiste(sifra, skladiste);
                DTRobaProdaja = classSQL.select(sql, "roba_prodaja").Tables[0];

                kolRobe = DTRobaProdaja.Rows.Count > 0 ? Convert.ToDecimal(DTRobaProdaja.Rows[0]["kolicina"].ToString()) : 0;
                kolStavka = Convert.ToDecimal(r["kolicina"].ToString());

                kolSpremi = kolRobe - kolStavka * Convert.ToDecimal(kolicinaNormativa);

                UpdateSkladiste(skladiste, sifra, kolSpremi.ToString());
            }
        }

        private string brojInventure(string skl)
        {
            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_inventure as numeric)) FROM inventura where id_skladiste='" + skl + "' AND godina='" + nuGodina.Value.ToString() + "'", "inventura").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString())).ToString();
            }
            else
            {
                return "0";
            }
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void ControlEnableDisable(bool t)
        {
            cbSkladiste.Enabled = t;
            btnZadnjaInv.Enabled = t;
            //btnPostavi.Enabled = t;
        }

        private string SqlSelectKolicinaWhereRobaSkladiste(string sifra, string skladiste)
        {
            string sql = "SELECT kolicina FROM roba_prodaja WHERE sifra='" + sifra + "'" +
                    " AND id_skladiste='" + skladiste + "'";
            return sql;
        }

        private void UpdateSkladiste(string skladiste, string sifra, string kolicina)
        {
            DataSet DSkol = classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + sifra + "' AND id_skladiste='" + skladiste + "'", "roba_prodaja");

            if (DSkol.Tables[0].Rows.Count == 0)
            {
                DataTable STrob = classSQL.select("SELECT nc,vpc,porez,porez_potrosnja FROM roba WHERE sifra ='" + sifra + "'", "roba").Tables[0];
                if (classSQL.remoteConnectionString == "")
                {
                    classSQL.insert("INSERT INTO roba_prodaja (id_skladiste,kolicina,nc,vpc,porez,sifra,porez_potrosnja) VALUES ('" + skladiste + "','0','" + STrob.Rows[0]["nc"].ToString().Replace(",", ".") + "','" + STrob.Rows[0]["vpc"].ToString().Replace(",", ".") + "','" + STrob.Rows[0]["porez"].ToString() + "','" + sifra + "','" + STrob.Rows[0]["porez_potrosnja"].ToString() + ")");
                }
                else
                {
                    classSQL.insert("INSERT INTO roba_prodaja (id_skladiste,kolicina,nc,vpc,porez,sifra) VALUES ('" + skladiste + "','0','" + STrob.Rows[0]["nc"].ToString() + "','" + STrob.Rows[0]["vpc"].ToString().Replace(",", ".") + "','" + STrob.Rows[0]["porez"].ToString() + "','" + sifra + "')");
                }
            }

            SQL.SQLroba_prodaja.UpdateRows(skladiste, kolicina, sifra);
        }

        private struct DokumentiRobno
        {
            public decimal kalulacija;
            public decimal racun;
            public decimal izdatnice;
            public decimal primke;
            public decimal fakture;
            public decimal meduskladisnice_u_skl;
            public decimal meduskladisnice_iz_skl;
            public decimal otpremnice;
            public decimal otpis;
            public decimal povrat_robe;
            public decimal radni_nalog;
            public decimal radni_nalog2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            danInventure = false;
            Inventura(true);
        }

        private List<DataTable> UzmiKolicineIzOveIprosleGodine()
        {
            DataSet DS = new DataSet();

            string sql = @"SELECT sifra,
	                            SUM(CASE
	                            WHEN documenat='Pocetno' OR documenat='Kalkulacija' OR documenat='Primka' OR documenat='MS u skl' OR documenat='Radni nalog dodaje na skl' THEN
		                            kolicina
	                            ELSE
		                            kolicina*-1
	                            END) as kolicina
                            FROM ulaz_izlaz_robe WHERE datum>'" + datumInventure.ToString("yyyy-MM-dd H:mm:ss") + "' AND skladiste='" + skladiste + "' GROUP BY sifra;";
            DataTable DTovaGodina = classSQL.select(sql, "Artikli").Tables[0];

            sql = @"SELECT sifra,
	                            SUM(CASE
	                            WHEN documenat='Pocetno' OR documenat='Kalkulacija' OR documenat='Primka' OR documenat='MS u skl' OR documenat='Radni nalog dodaje na skl' THEN
		                            kolicina
	                            ELSE
		                            kolicina*-1
	                            END) as kolicina
                            FROM ulaz_izlaz_robe WHERE datum<'" + datumInventure.ToString("yyyy-MM-dd H:mm:ss") + "' AND skladiste='" + skladiste + "' GROUP BY sifra;";

            #region OVAJ DIO RADI KONEKCIJU NA BAZU IZ PRETHODNE GODINE

            PCPOS.Util.classFukcijeZaUpravljanjeBazom B = new Util.classFukcijeZaUpravljanjeBazom("MALOPRODAJA", "POS");
            string _trenutna_baza = B.UzmiBazuKojaSeKoristi();
            string _prethodna_godina = "";

            if (_trenutna_baza.Length == 7)
            {
                int godina;
                if (int.TryParse(_trenutna_baza.Remove(0, 3), out godina))
                {
                    _prethodna_godina = _trenutna_baza.Remove(3) + (godina - 1).ToString();
                }
            }

            //U ovaj dio ulazi samo ako postoji baza koja je u varijabli "_prethodna_godina"
            DataTable DTprethodna_godina = new DataTable();
            DataSet DStemp = new DataSet();
            if (B.GodinaPostoji(_prethodna_godina))
            {
                if (classSQL.remoteConnection.State.ToString() != "Closed") { classSQL.remoteConnection.Close(); }
                NpgsqlConnection remoteConnection = new NpgsqlConnection(classSQL.remoteConnectionString.Replace(_trenutna_baza, _prethodna_godina));
                if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql.Replace("+", "||").Replace("[", "\"").Replace("]", "\"").Replace("zbroj", "+"), remoteConnection);
                DStemp.Reset();
                da.Fill(DStemp);
                remoteConnection.Close();
                DTprethodna_godina = DStemp.Tables[0];
            }

            #endregion OVAJ DIO RADI KONEKCIJU NA BAZU IZ PRETHODNE GODINE

            List<DataTable> Ldt = new List<DataTable>();
            Ldt.Add(DTovaGodina);
            Ldt.Add(DTprethodna_godina);
            return Ldt;
        }

        private void btnPostaviCijene_Click(object sender, EventArgs e)
        {
            this.Text = "Molim pričekajte, radim provjeru.";
            string sql = @"UPDATE pocetno SET prodajna_cijena=
                        CASE
                        WHEN (SELECT ROUND(cijena,2) FROM inventura_stavke LEFT JOIN inventura ON inventura.broj_inventure=inventura_stavke.broj_inventure WHERE inventura.id_skladiste=pocetno.id_skladiste AND inventura_stavke.sifra_robe=pocetno.sifra LIMIT 1)<>0::numeric THEN
	                        coalesce((SELECT ROUND(cijena,2) FROM inventura_stavke LEFT JOIN inventura ON inventura.broj_inventure=inventura_stavke.broj_inventure WHERE inventura.id_skladiste=pocetno.id_skladiste AND inventura_stavke.sifra_robe=pocetno.sifra LIMIT 1),0)
                        ELSE
	                        coalesce((SELECT ROUND((vpc+(vpc*CAST(REPLACE(porez,',','.') AS numeric)/100)),2) FROM roba_prodaja WHERE roba_prodaja.id_skladiste=pocetno.id_skladiste AND roba_prodaja.sifra=pocetno.sifra LIMIT 1),0)
                        END;

                        UPDATE pocetno SET porez=
                        (SELECT CAST(CAST(REPLACE(porez,',','.') AS DECIMAL) AS INTEGER) FROM roba WHERE roba.sifra=pocetno.sifra);

                        UPDATE pocetno SET nbc=
                        CASE
                        WHEN (SELECT ROUND(nbc,4) FROM inventura_stavke LEFT JOIN inventura ON inventura.broj_inventure=inventura_stavke.broj_inventure WHERE inventura.id_skladiste=pocetno.id_skladiste AND inventura_stavke.sifra_robe=pocetno.sifra LIMIT 1)<>0::numeric THEN
	                        coalesce((SELECT ROUND(nbc,4) FROM inventura_stavke LEFT JOIN inventura ON inventura.broj_inventure=inventura_stavke.broj_inventure WHERE inventura.id_skladiste=pocetno.id_skladiste AND inventura_stavke.sifra_robe=pocetno.sifra LIMIT 1),0)
                        ELSE
	                        coalesce((SELECT ROUND(CAST(nc AS numeric),4) FROM roba_prodaja WHERE roba_prodaja.id_skladiste=pocetno.id_skladiste AND roba_prodaja.sifra=pocetno.sifra LIMIT 1),0)
                        END;";
            classSQL.update(sql);
            this.Text = "Završeno";
            MessageBox.Show("Završeno.");
        }

        /// <summary>
        /// 
        /// </summary>
        private void Inventura(bool poravnanje)
        {
            PostaviInventuru(true);
            if (inventuraIzProsleGodine)
            {
                classSQL.remoteConnection = new NpgsqlConnection(classSQL.remoteConnectionString.Replace(tmp.UzmiGodinuKojaSeKoristi().ToString(), (tmp.UzmiGodinuKojaSeKoristi() - 1).ToString()));
                classSQL.update("UPDATE inventura SET editirano='1', pocetno_stanje=1 WHERE broj_inventure='" + inventura + "' AND id_skladiste='" + skladiste + "'");
                classSQL.remoteConnection = new NpgsqlConnection(classSQL.remoteConnectionString);
            }
            else
            {
                classSQL.update("UPDATE inventura SET editirano='1', pocetno_stanje=1 WHERE broj_inventure='" + inventura + "' AND id_skladiste='" + skladiste + "'");
            }

            if (poravnanje)
            {
                MessageBox.Show("Slijedi postavljanje skladišta!");
                frmProgStanje stanje = new frmProgStanje();
                stanje.btnPoravnaj_Click(null, null);

                MessageBox.Show("Uspješno postavljeno početno stanje!");
            }
        }

        private void btnDanInventure_Click(object sender, EventArgs e)
        {
            danInventure = true;
            Inventura(false);
        }

        /// <summary>
        /// 
        /// </summary>
        private void IzracunajKolicinu(string sifra)
        {
            kolicinaRobeNakonPocetnog = 0;
            Kalkulacija(sifra);
            FaktureBezRobe(sifra);
            Racuni(sifra);
            Otpremnica(sifra);
            RadniNalog(sifra);
            Medjuskladisnica(sifra);
            Inventura(sifra);
            PovratRobe(sifra);
            OtpisRobe(sifra);
            PromjenaCijene(sifra);
            Primka(sifra);
            Izdatnica(sifra);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sifra"></param>
        private int Pocetno(string sifra)
        {
            int result = 0;
            string traziSifru = sifra != "" ? " AND pocetno.sifra='" + sifra + "'" : "";

            string sql = "SELECT " +
               " roba.sifra AS [Šifra]," +
               " pocetno.kolicina AS [Količina]" +
               " FROM pocetno" +
               " LEFT JOIN roba ON roba.sifra=pocetno.sifra" +
               " WHERE pocetno.id_skladiste='" + cbSkladiste.SelectedValue + "' and cast(datum as date) >= '" + datumInventure.ToString("yyyy-MM-dd H:mm:ss") + "' and cast(datum as date) <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "'" + traziSifru;
            DSSkalkulacija_stavke = classSQL.select(sql, "pocetno");
            result = Convert.ToInt32(DSSkalkulacija_stavke.Tables[0].Rows[0]["kolicina"].ToString());
            return result;
        }

        private void Kalkulacija(string sifra)
        {
            string traziSkladiste = "AND kalkulacija.id_skladiste='" + cbSkladiste.SelectedValue + "'";
            string traziSifru = sifra != "" ? " AND kalkulacija_stavke.sifra='" + sifra + "'" : "";

            string sql = "SELECT " +
                " roba.sifra AS [Šifra]," +
                " roba.naziv AS [Naziv]," +
                " roba.jm AS [JMJ]," +
                " kalkulacija.datum AS [Datum]," +
                " round(coalesce(kalkulacija_stavke.fak_cijena, 0),6) - round((round(coalesce(kalkulacija_stavke.fak_cijena, 0),6) * replace(kalkulacija_stavke.rabat, ',','.')::numeric / 100), 6) AS [NBC]," +
                " kalkulacija_stavke.vpc AS [VPC]," +
                " kalkulacija_stavke.kolicina AS [Količina]," +
                " 'KALKULACIJA ' + kalkulacija_stavke.broj + '.' + kalkulacija.godina AS [Dokument]," +
                " 1 AS [Ulaz]," +
                " kalkulacija.id_kalkulacija AS [ID]" +
                " FROM kalkulacija_stavke " +
                " LEFT JOIN kalkulacija ON kalkulacija.broj=kalkulacija_stavke.broj AND kalkulacija.id_skladiste=kalkulacija_stavke.id_skladiste" +
                " LEFT JOIN roba ON roba.sifra=kalkulacija_stavke.sifra" +
                " WHERE kalkulacija.datum >= '" + datumInventure.ToString("yyyy-MM-dd 00:00:00") + "'" +
                " AND kalkulacija.datum <= '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                traziSkladiste + traziSifru;

            DSSkalkulacija_stavke = classSQL.select(sql, "kartica");
            GetKolicina(DSSkalkulacija_stavke, 6);
        }

        private void FaktureBezRobe(string sifra)
        {
            string traziSkladiste = "AND ifb.id_skladiste='" + cbSkladiste.SelectedValue + "'";
            traziSkladiste = "";
            string traziSifru = sifra != "" ? " AND ifb_stavke.naziv='" + sifra + "'" : "";

            string sql_fak = "SELECT " +
                " roba.sifra AS [Šifra]," +
                " roba.naziv AS [Naziv]," +
                " roba.jm AS [JMJ]," +
                " ifb.datum AS [Datum]," +
                " '' AS [NBC]," +
                " ifb_stavke.vpc AS [VPC]," +
                " ifb_stavke.kolicina AS [Količina]," +
                " 'FAKTURA BEZ ROBE ' + ifb.broj + '.' + ifb.godina AS [Dokument]," +
                " 0 AS [Ulaz]," +
                " ifb.id as ID" +
                " FROM ifb_stavke " +
                " LEFT JOIN ifb ON ifb.broj=ifb_stavke.broj" +
                " LEFT JOIN roba ON roba.sifra=ifb_stavke.naziv" +
                " WHERE ifb.datum >= '" + datumInventure.ToString("yyyy-MM-dd 00:00:00") + "'" +
                " AND ifb.datum <= '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                traziSkladiste + traziSifru;

            DSSkalkulacija_stavke = classSQL.select(sql_fak, "kartica");
            GetKolicina(DSSkalkulacija_stavke, 6);
        }

        private void Racuni(string sifra)
        {
            string traziSkladiste = "AND racun_stavke.id_skladiste='" + cbSkladiste.SelectedValue + "'";
            string traziSifru = sifra != "" ? " AND racun_stavke.sifra_robe='" + sifra + "'" : "";

            string sql_maloprodaja = "SELECT " +
              " roba.sifra AS [Šifra]," +
              " roba.naziv AS [Naziv]," +
              " roba.jm AS [JMJ]," +
              " racuni.datum_racuna AS [Datum]," +
              " racun_stavke.nbc AS [NBC]," +
              " racun_stavke.vpc AS [VPC]," +
              " racun_stavke.id_ducan," +
              " racun_stavke.id_kasa ," +
              " racun_stavke.kolicina AS [Količina]," +
              " 'MALOPRODAJA ' + racun_stavke.broj_racuna +'/'+ ducan.ime_ducana +'/'+ blagajna.ime_blagajne AS [Dokument]," +
              " 0 AS [Ulaz]," +
              " racuni.broj_racuna as ID" +
              " FROM racun_stavke " +
              " LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa" +
              " LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe" +
              " LEFT JOIN blagajna ON blagajna.id_blagajna=racun_stavke.id_kasa" +
              " LEFT JOIN ducan ON ducan.id_ducan=racun_stavke.id_ducan" +
              " WHERE racuni.datum_racuna >= '" + datumInventure.ToString("yyyy-MM-dd 00:00:00") + "'" +
              " AND racuni.datum_racuna <= '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
              traziSkladiste + traziSifru;

            DSSkalkulacija_stavke = classSQL.select(sql_maloprodaja, "kartica");
            GetKolicina(DSSkalkulacija_stavke, 6);
        }

        private void Otpremnica(string sifra)
        {
            string traziSkladiste = "AND otpremnica_stavke.id_skladiste='" + cbSkladiste.SelectedValue + "'";
            string traziSifru = sifra != "" ? " AND otpremnica_stavke.sifra_robe='" + sifra + "'" : "";

            string sql_otpremnica = "SELECT " +
              " roba.sifra AS [Šifra]," +
              " roba.naziv AS [Naziv]," +
              " roba.jm AS [JMJ]," +
              " otpremnice.datum AS [Datum]," +
              " otpremnica_stavke.nbc AS [NBC]," +
              " otpremnica_stavke.vpc AS [VPC]," +
              " otpremnica_stavke.kolicina AS [Količina]," +
              " 'OTPREMNICA ' + otpremnica_stavke.broj_otpremnice + '.' + otpremnice.godina_otpremnice AS [Dokument]," +
              " 0 AS [Ulaz]," +
              " otpremnice.id_otpremnica as ID" +
              " FROM otpremnica_stavke " +
              " LEFT JOIN otpremnice ON otpremnice.id_otpremnica=otpremnica_stavke.id_otpremnice" +
              " LEFT JOIN roba ON roba.sifra=otpremnica_stavke.sifra_robe" +
              " WHERE otpremnice.datum >= '" + datumInventure.ToString("yyyy-MM-dd 00:00:00") + "'" +
              " AND otpremnice.datum <= '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
              traziSkladiste + traziSifru;

            DSSkalkulacija_stavke = classSQL.select(sql_otpremnica, "kartica");
            GetKolicina(DSSkalkulacija_stavke, 6);
        }

        private void RadniNalog(string sifra)
        {
            string traziSkladiste = "AND radni_nalog_stavke.id_skladiste='" + cbSkladiste.SelectedValue + "'";
            string traziSifru = sifra != "" ? " AND radni_nalog_stavke.sifra_robe='" + sifra + "'" : "";

            string sql_rn = "SELECT " +
              " roba.sifra AS [Šifra]," +
              " roba.naziv AS [Naziv]," +
              " roba.jm AS [JMJ]," +
              " radni_nalog.datum_naloga AS [Datum]," +
              " radni_nalog_stavke.nbc AS [NBC]," +
              " radni_nalog_stavke.vpc AS [VPC]," +
              " radni_nalog_stavke.kolicina AS [Količina]," +
              " 'RADNI NALOG DO ' + radni_nalog_stavke.broj_naloga + '.' + radni_nalog.godina_naloga AS [Dokument]," +
              " 1 AS [Ulaz]," +
              " radni_nalog.broj_naloga as ID" +
              " FROM radni_nalog_stavke " +
              " LEFT JOIN radni_nalog ON radni_nalog.broj_naloga=radni_nalog_stavke.broj_naloga" +
              " LEFT JOIN roba ON roba.sifra=radni_nalog_stavke.sifra_robe" +
              " WHERE radni_nalog.datum_naloga >= '" + datumInventure.ToString("yyyy-MM-dd 00:00:00") + "'" +
              " AND radni_nalog.datum_naloga <= '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
              traziSkladiste + traziSifru;

            DSSkalkulacija_stavke = classSQL.select(sql_rn, "kartica");
            GetKolicina(DSSkalkulacija_stavke, 6);
        }

        private void Medjuskladisnica(string sifra)
        {
            string traziSkladiste = "AND meduskladisnica.id_skladiste_do='" + cbSkladiste.SelectedValue + "'";
            string traziSkladiste2 = "AND meduskladisnica.id_skladiste_od='" + cbSkladiste.SelectedValue + "'";
            string traziSifru = sifra != "" ? " AND meduskladisnica_stavke.sifra='" + sifra + "'" : "";

            string sql_me1 = "SELECT " +
              " roba.sifra AS [Šifra]," +
              " roba.naziv AS [Naziv]," +
              " roba.jm AS [JMJ]," +
              " meduskladisnica.datum AS [Datum]," +
              " meduskladisnica_stavke.nbc AS [NBC]," +
              " meduskladisnica_stavke.vpc AS [VPC]," +
              " meduskladisnica_stavke.kolicina AS [Količina]," +
              " 'MEĐUSKLADIŠNICA SA ' + skladiste.skladiste + ' ' + meduskladisnica.broj + '.' + meduskladisnica.godina AS [Dokument]," +
              " 1 AS [Ulaz]," +
              " meduskladisnica.id as ID" +
              " FROM meduskladisnica_stavke " +
              " LEFT JOIN meduskladisnica ON meduskladisnica.broj=meduskladisnica_stavke.broj  AND meduskladisnica_stavke.iz_skladista=meduskladisnica.id_skladiste_od" +
              " LEFT JOIN roba ON roba.sifra=meduskladisnica_stavke.sifra" +
              " LEFT JOIN skladiste ON skladiste.id_skladiste=meduskladisnica.id_skladiste_od" +
              " WHERE meduskladisnica.datum >= '" + datumInventure.ToString("yyyy-MM-dd 00:00:00") + "'" +
              " AND meduskladisnica.datum <= '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
              traziSkladiste + traziSifru + "UNION ALL " +
              "SELECT " +
              " roba.sifra AS [Šifra]," +
              " roba.naziv AS [Naziv]," +
              " roba.jm AS [JMJ]," +
              " meduskladisnica.datum AS [Datum]," +
              " meduskladisnica_stavke.nbc AS [NBC]," +
              " meduskladisnica_stavke.vpc AS [VPC]," +
              " meduskladisnica_stavke.kolicina AS [Količina]," +
              " 'MEĐUSKLADIŠNICA NA ' + skladiste.skladiste + ' ' + meduskladisnica.broj + '.' + meduskladisnica.godina AS [Dokument]," +
              " 0 AS [Ulaz]," +
              " meduskladisnica.id as ID" +
              " FROM meduskladisnica_stavke " +
              " LEFT JOIN meduskladisnica ON meduskladisnica.broj=meduskladisnica_stavke.broj  AND meduskladisnica_stavke.iz_skladista=meduskladisnica.id_skladiste_od" +
              " LEFT JOIN roba ON roba.sifra=meduskladisnica_stavke.sifra" +
              " LEFT JOIN skladiste ON skladiste.id_skladiste=meduskladisnica.id_skladiste_do" +
              " WHERE meduskladisnica.datum >= '" + datumInventure.ToString("yyyy-MM-dd 00:00:00") + "'" +
              " AND meduskladisnica.datum <= '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
              traziSkladiste2 + traziSifru;

            DSSkalkulacija_stavke = classSQL.select(sql_me1, "kartica");
            GetKolicina(DSSkalkulacija_stavke, 6);
        }

        private void Inventura(string sifra)
        {
            string traziSkladiste = "AND inventura.id_skladiste='" + cbSkladiste.SelectedValue + "'";
            string traziSifru = sifra != "" ? " AND inventura_stavke.sifra_robe='" + sifra + "'" : "";

            string sql_odjava = "SELECT " +
              " roba.sifra AS [Šifra]," +
              " roba.naziv AS [Naziv]," +
              " roba.jm AS [JMJ]," +
              " inventura.datum AS [Datum]," +
              " roba.nc AS [NBC]," +
              " inventura_stavke.cijena AS [VPC]," +
              " inventura_stavke.kolicina AS [Količina]," +
              " 'INVENTURA ' + inventura.broj_inventure + '.' + inventura.godina [Dokument]," +
              " 1 AS [Ulaz]," +
              " inventura.broj_inventure as ID" +
              " FROM inventura_stavke " +
              " LEFT JOIN inventura ON inventura.broj_inventure=inventura_stavke.broj_inventure" +
              " LEFT JOIN roba ON roba.sifra=inventura_stavke.sifra_robe" +
              " WHERE inventura.datum >= '" + datumInventure.ToString("yyyy-MM-dd 00:00:00") + "'" +
              " AND inventura.datum <= '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
              traziSkladiste + traziSifru + " AND inventura.pocetno_stanje=0 " + "";

            DSSkalkulacija_stavke = classSQL.select(sql_odjava, "inventura");
            GetKolicina(DSSkalkulacija_stavke, 6);
        }

        private void PovratRobe(string sifra)
        {
            string traziSkladiste = "AND povrat_robe.id_skladiste='" + cbSkladiste.SelectedValue + "'";
            string traziSifru = sifra != "" ? " AND povrat_robe_stavke.sifra='" + sifra + "'" : "";

            string sql_povrat = "SELECT " +
              " roba.sifra AS [Šifra]," +
              " roba.naziv AS [Naziv]," +
              " roba.jm AS [JMJ]," +
              " povrat_robe.datum AS [Datum]," +
              " povrat_robe_stavke.nbc AS [NBC]," +
              " povrat_robe_stavke.vpc AS [VPC]," +
              " povrat_robe_stavke.kolicina AS [Količina]," +
              " 'POVRAT DOB. ' + povrat_robe.broj + '.' + povrat_robe.godina AS [Dokument]," +
              " 0 AS [Ulaz]," +
              " povrat_robe.broj as ID" +
              " FROM povrat_robe_stavke " +
              " LEFT JOIN povrat_robe ON povrat_robe.broj=povrat_robe_stavke.broj" +
              " LEFT JOIN roba ON roba.sifra=povrat_robe_stavke.sifra" +
              " WHERE povrat_robe.datum >= '" + datumInventure.ToString("yyyy-MM-dd 00:00:00") + "'" +
              " AND povrat_robe.datum <= '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
              traziSkladiste + traziSifru;

            DSSkalkulacija_stavke = classSQL.select(sql_povrat, "kartica");
            GetKolicina(DSSkalkulacija_stavke, 6);
        }

        private void OtpisRobe(string sifra)
        {
            string traziSkladiste = "AND otpis_robe.id_skladiste='" + cbSkladiste.SelectedValue + "'";
            string traziSifru = sifra != "" ? " AND otpis_robe_stavke.sifra='" + sifra + "'" : "";

            string sql_povrat = "SELECT " +
              " roba.sifra AS [Šifra]," +
              " roba.naziv AS [Naziv]," +
              " roba.jm AS [JMJ]," +
              " otpis_robe.datum AS [Datum]," +
              " otpis_robe_stavke.nbc AS [NBC]," +
              " otpis_robe_stavke.vpc AS [VPC]," +
              " otpis_robe_stavke.kolicina AS [Količina]," +
              " 'OTPIS ROBE ' + otpis_robe.broj + '.' + otpis_robe.godina AS [Dokument]," +
              " 0 AS [Ulaz]," +
              " otpis_robe.broj as ID" +
              " FROM otpis_robe_stavke " +
              " LEFT JOIN otpis_robe ON otpis_robe.broj=otpis_robe_stavke.broj" +
              " LEFT JOIN roba ON roba.sifra=otpis_robe_stavke.sifra" +
              " WHERE otpis_robe.datum >= '" + datumInventure.ToString("yyyy-MM-dd 00:00:00") + "'" +
              " AND otpis_robe.datum <= '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
              traziSkladiste + traziSifru;

            DSSkalkulacija_stavke = classSQL.select(sql_povrat, "otpis_robe");
            GetKolicina(DSSkalkulacija_stavke, 6);
        }

        private void PromjenaCijene(string sifra)
        {
            string traziSkladiste = "AND promjena_cijene.id_skladiste='" + cbSkladiste.SelectedValue + "'";
            string traziSifru = sifra != "" ? " AND promjena_cijene_stavke.sifra='" + sifra + "'" : "";

            string sql_promjena_cijene = "SELECT " +
                  " roba.sifra AS [Šifra]," +
                  " roba.naziv AS [Naziv]," +
                  " roba.jm AS [JMJ]," +
                  " promjena_cijene.date AS [Datum]," +
                  " roba.nc AS [NBC]," +
                  " promjena_cijene_stavke.nova_cijena AS [VPC]," +
                  " '0' AS [Količina]," +
                  " 'PROMJENA CIJENE. ' + promjena_cijene.broj AS [Dokument]," +
                  " 0 AS [Ulaz]," +
                  " promjena_cijene.broj as ID" +
                  " FROM promjena_cijene_stavke " +
                  " LEFT JOIN promjena_cijene ON promjena_cijene.broj=promjena_cijene_stavke.broj" +
                  " LEFT JOIN roba ON roba.sifra=promjena_cijene_stavke.sifra" +
                  " WHERE promjena_cijene.date >= '" + datumInventure.ToString("yyyy-MM-dd 00:00:00") + "'" +
                  " AND promjena_cijene.date <= '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                  traziSkladiste + traziSifru;

            DSSkalkulacija_stavke = classSQL.select(sql_promjena_cijene, "kartica");
            GetKolicina(DSSkalkulacija_stavke, 6);
        }

        private void Primka(string sifra)
        {
            string traziSkladiste = "AND primka.id_skladiste='" + cbSkladiste.SelectedValue + "'";
            string traziSifru = sifra != "" ? " AND primka_stavke.sifra='" + sifra + "'" : "";

            string sql_fak = "SELECT " +
                " roba.sifra AS [Šifra]," +
                " roba.naziv AS [Naziv]," +
                " roba.jm AS [JMJ]," +
                " primka.datum AS [Datum]," +
                " primka_stavke.nbc AS [NBC]," +
                " primka_stavke.vpc AS [VPC]," +
                " primka_stavke.kolicina AS [Količina]," +
                " 'PRIMKA ' + primka.broj + '.' + primka.godina AS [Dokument]," +
                " 1 AS [Ulaz]," +
                " primka.id_primka as ID" +
                " FROM primka_stavke " +
                " LEFT JOIN primka ON primka.id_primka=primka_stavke.id_primka" +
                " LEFT JOIN roba ON roba.sifra=primka_stavke.sifra" +
                " WHERE primka.datum >= '" + datumInventure.ToString("yyyy-MM-dd 00:00:00") + "'" +
                " AND primka.datum <= '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                traziSkladiste + traziSifru;

            DSSkalkulacija_stavke = classSQL.select(sql_fak, "kartica");
            GetKolicina(DSSkalkulacija_stavke, 6);
        }

        private void Izdatnica(string sifra)
        {
            string traziSkladiste = "AND izdatnica.id_skladiste='" + cbSkladiste.SelectedValue + "'";
            string traziSifru = sifra != "" ? " AND izdatnica_stavke.sifra='" + sifra + "'" : "";

            string sql_fak = "SELECT " +
                " roba.sifra AS [Šifra]," +
                " roba.naziv AS [Naziv]," +
                " roba.jm AS [JMJ]," +
                " izdatnica.datum AS [Datum]," +
                " izdatnica_stavke.nbc AS [NBC]," +
                " izdatnica_stavke.vpc AS [VPC]," +
                " izdatnica_stavke.kolicina AS [Količina]," +
                " izdatnica_stavke.id_izdatnica AS [ID]," +
                " 'IZDATNICA ' + izdatnica.broj + '.' + izdatnica.godina AS [Dokument]," +
                " 0 AS [Ulaz]" +
                " FROM izdatnica_stavke " +
                " LEFT JOIN izdatnica ON izdatnica.broj=izdatnica_stavke.broj AND izdatnica.id_izdatnica=izdatnica_stavke.id_izdatnica" +
                " LEFT JOIN roba ON roba.sifra=izdatnica_stavke.sifra" +
                " WHERE izdatnica.datum >= '" + datumInventure.ToString("yyyy-MM-dd 00:00:00") + "'" +
                " AND izdatnica.datum <= '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                traziSkladiste + traziSifru;

            DSSkalkulacija_stavke = classSQL.select(sql_fak, "kartica");
            GetKolicina(DSSkalkulacija_stavke, 6);
        }

        private void GetKolicina(DataSet dataSet, int indexKolicina = 6)
        {
            DateTime datum;
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                datum = DateTime.Parse(dataSet.Tables[0].Rows[i]["datum"].ToString());
                if (datum > datumInventure)
                    kolicinaRobeNakonPocetnog++;
            }
        }
    }
}