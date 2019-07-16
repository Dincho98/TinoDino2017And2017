using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;

namespace PCPOS
{
    public partial class frmKarticaRobe : Form
    {
        public frmKarticaRobe()
        {
            InitializeComponent();
        }

        private DataSet DSvd = new DataSet();
        private DataSet DSMT = new DataSet();
        public frmMenu MainFormMenu { get; set; }

        private static DataTable DTroba = new DataTable();

        private DateTime zadnjaInventuraDatum { get; set; }

        private DateTime pocetnoStanjeDatum { get; set; }
        private decimal pocetnoStanjeKolicina { get; set; }
        private decimal kolicinaRobeNakonPocetnog = 0.0m;

        private void frmKartica_Load(object sender, EventArgs e)
        {
            brRedaka.Text = "";
            fillComboBox();

            dtpOdDatuma.Value = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);
            dtpDoDatuma.Value = new DateTime(DateTime.Now.Year, 12, 31, 23, 59, 59);

            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 500);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private DataTable DTitems_sort = new DataTable();

        private void fillComboBox()
        {
            DTitems_sort.Columns.Add("Šifra");
            DTitems_sort.Columns.Add("Naziv");
            DTitems_sort.Columns.Add("Količina");
            DTitems_sort.Columns.Add("JMJ");
            DTitems_sort.Columns.Add("NBC");
            DTitems_sort.Columns.Add("VPC");
            DataColumn c2 = new DataColumn("Datum", typeof(DateTime));
            DTitems_sort.Columns.Add(c2);
            //DTitems_sort.Columns.Add("Datum");
            DTitems_sort.Columns.Add("Dokument");
            DTitems_sort.Columns.Add("ID");
            DTitems_sort.Columns.Add("Ulaz");
            DTitems_sort.Columns.Add("Poslovnica");
            DTitems_sort.Columns.Add("Naplatni");

            //fill mjTroška
            DSMT = classSQL.select("SELECT * FROM skladiste", "skladiste");
            cbSkladiste.DataSource = DSMT.Tables[0];
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";
        }

        private DataSet DSSkalkulacija_stavke = new DataSet();

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void FillGrid()
        {
            if (dgw.Rows.Count > 0)
            {
                DTitems_sort.Clear();
            }

            kolicinaRobeNakonPocetnog = 0;
            pocetnoStanjeKolicina = 0;

            DTroba.Clear();

            dodajKoloneDTroba();

            string sifra = txtSifraArtikla.Text.Trim();

            if (chbPocetno.Checked)
                Pocetno(sifra);

            if (chbKalkulacija.Checked)
                Kalkulacija(sifra);

            if (chbFaktura.Checked)
                Fakture(sifra);

            if (chbIFB.Checked)
                FaktureBezRobe(sifra);

            if (chbMaloprodaja.Checked)
                Racuni(sifra);

            if (chbOtpremnica.Checked)
                Otpremnica(sifra);

            if (chbRadniNalog.Checked)
                RadniNalog(sifra);

            //if (chbRNS.Checked) RadniNalogServis(sifra);

            if (chbMediskladisnica.Checked)
                Medjuskladisnica(sifra);

            if (chbInventura.Checked)
                Inventura(sifra);

            if (chbPovratDob.Checked)
                PovratRobe(sifra);

            if (chbOtpisRobe.Checked)
                OtpisRobe(sifra);

            if (chbPromjenaCijene.Checked)
                PromjenaCijene(sifra);

            if (chbPrimka.Checked)
                Primka(sifra);

            if (chbIzdatnica.Checked)
                Izdatnica(sifra);

            dgw.DataSource = DTitems_sort;
            dgw.Columns["ID"].Visible = false;
            dgw.Columns["Ulaz"].Visible = false;
            brRedaka.Text = dgw.Rows.Count.ToString();

            SetStanje();

            //dgw1.DataSource = DTroba;

            try
            {
                dgw.Columns["Poslovnica"].Visible = false;
                dgw.Columns["Naplatni"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //DataGridViewCellStyle style = new DataGridViewCellStyle();
            //style.Format = "N2";
            //dgw.Columns["VPC"].DefaultCellStyle = style;
            //dgw.Columns["NBC"].DefaultCellStyle = style;

            //dgw.Columns["cijena"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgw.Columns["cijena"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //dataGridView1.Columns[1].DefaultCellStyle.Format = "dd.MM.yyyy ";
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
                " WHERE kalkulacija.datum >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " AND kalkulacija.datum <= '" + dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                traziSkladiste + traziSifru;

            DSSkalkulacija_stavke = classSQL.select(sql, "kartica");
            FillTable(DSSkalkulacija_stavke);
            GetKolicina(DSSkalkulacija_stavke, 6, true);

            Sredi(DSSkalkulacija_stavke.Tables[0], "+");
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
                  " WHERE promjena_cijene.date >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                  " AND promjena_cijene.date <= '" + dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                  traziSkladiste + traziSifru;

            DSSkalkulacija_stavke = classSQL.select(sql_promjena_cijene, "kartica");
            GetKolicina(DSSkalkulacija_stavke, 6);
            FillTable(DSSkalkulacija_stavke);

            //Sredi(DSSkalkulacija_stavke.Tables[0]);
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
              " WHERE povrat_robe.datum >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
              " AND povrat_robe.datum <= '" + dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
              traziSkladiste + traziSifru;

            DSSkalkulacija_stavke = classSQL.select(sql_povrat, "kartica");
            GetKolicina(DSSkalkulacija_stavke, 6);
            FillTable(DSSkalkulacija_stavke);

            Sredi(DSSkalkulacija_stavke.Tables[0], "-");
        }

        private void Pocetno(string sifra)
        {
            //if (dtpOdDatuma.Value <= new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0) && dtpDoDatuma.Value >= new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0))
            {
                string traziSifru = sifra != "" ? " AND pocetno.sifra='" + sifra + "'" : "";

                string sql = "SELECT " +
                   " roba.sifra AS [Šifra]," +
                   " roba.naziv AS [Naziv]," +
                   " roba.jm AS [JMJ]," +
                   " datum AS [Datum]," +
                   " pocetno.nbc AS [NBC]," +
                   " roba.vpc AS [VPC]," +
                   " pocetno.kolicina AS [Količina]," +
                   " 'POČETNO STANJE ' AS [Dokument]," +
                   " 1 AS [Ulaz]," +
                   " '0' as ID" +
                   " FROM pocetno" +
                   " LEFT JOIN roba ON roba.sifra=pocetno.sifra" +
                   " WHERE pocetno.id_skladiste='" + cbSkladiste.SelectedValue + "' and cast(datum as date) >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "' and cast(datum as date) <= '" + dtpDoDatuma.Value.ToString("yyyy-MM-dd") + "'" + traziSifru;
                DSSkalkulacija_stavke = classSQL.select(sql, "pocetno");
                if (DSSkalkulacija_stavke.Tables[0].Rows.Count > 0)
                {
                    pocetnoStanjeDatum = DateTime.Parse(DSSkalkulacija_stavke.Tables[0].Rows[0]["datum"].ToString());
                    pocetnoStanjeKolicina = Convert.ToDecimal(DSSkalkulacija_stavke.Tables[0].Rows[0][6].ToString());
                    FillTable(DSSkalkulacija_stavke);

                    Sredi(DSSkalkulacija_stavke.Tables[0], "+");
                }
            }
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
              " WHERE otpis_robe.datum >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
              " AND otpis_robe.datum <= '" + dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
              traziSkladiste + traziSifru;

            DSSkalkulacija_stavke = classSQL.select(sql_povrat, "otpis_robe");
            GetKolicina(DSSkalkulacija_stavke, 6);
            FillTable(DSSkalkulacija_stavke);

            Sredi(DSSkalkulacija_stavke.Tables[0], "-");
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
              " WHERE inventura.datum >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
              " AND inventura.datum <= '" + dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
              traziSkladiste + traziSifru + " AND inventura.pocetno_stanje=0 " +
                /*" UNION ALL " +
                "SELECT " +
                " roba.sifra AS [Šifra]," +
                " roba.naziv AS [Naziv]," +
                " roba.jm AS [JMJ]," +
                " inventura.datum AS [Datum]," +
                " roba.nc AS [NBC]," +
                " inventura_stavke.cijena AS [VPC]," +
                " inventura_stavke.kolicina AS [Količina]," +
                " 'POČETNO STANJE ' + inventura.broj_inventure + '.' + inventura.godina [Dokument]," +
                " 1 AS [Ulaz]," +
                " inventura.broj_inventure as ID" +
                " FROM inventura_stavke " +
                " LEFT JOIN inventura ON inventura.broj_inventure=inventura_stavke.broj_inventure" +
                " LEFT JOIN roba ON roba.sifra=inventura_stavke.sifra_robe" +
                " WHERE inventura.datum >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " AND inventura.datum <= '" + dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                traziSkladiste + traziSifru + " AND inventura.pocetno_stanje=1"*/"";

            DSSkalkulacija_stavke = classSQL.select(sql_odjava, "inventura");
            GetKolicina(DSSkalkulacija_stavke, 6);
            FillTable(DSSkalkulacija_stavke);

            Sredi(DSSkalkulacija_stavke.Tables[0], "+");
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
              " WHERE meduskladisnica.datum >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
              " AND meduskladisnica.datum <= '" + dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
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
              " WHERE meduskladisnica.datum >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
              " AND meduskladisnica.datum <= '" + dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
              traziSkladiste2 + traziSifru;

            DSSkalkulacija_stavke = classSQL.select(sql_me1, "kartica");
            GetKolicina(DSSkalkulacija_stavke, 6);
            FillTable(DSSkalkulacija_stavke);

            return;

            sql_me1 = "SELECT " +
                " SUM(CAST(REPLACE(meduskladisnica_stavke.kolicina,',','.') AS NUMERIC)) AS [Količina]," +
                " meduskladisnica_stavke.sifra AS [Šifra]" +
                " FROM meduskladisnica_stavke " +
                " LEFT JOIN meduskladisnica ON meduskladisnica.broj=meduskladisnica_stavke.broj " +
                " WHERE meduskladisnica.datum >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " AND meduskladisnica.datum <= '" + dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                traziSkladiste + traziSifru +
                " GROUP BY meduskladisnica_stavke.sifra";
            DataTable DTMedjuDoSkladista = classSQL.select(sql_me1, "meduskladisnica").Tables[0];
            MedjuskladisniceHelper(DTMedjuDoSkladista, true);

            traziSkladiste = "AND meduskladisnica.id_skladiste_od='" + cbSkladiste.SelectedValue + "'";

            sql_me1 = "SELECT " +
                " SUM(CAST(REPLACE(meduskladisnica_stavke.kolicina,',','.') AS NUMERIC)) AS [Količina]," +
                " meduskladisnica_stavke.sifra AS [Šifra]" +
                " FROM meduskladisnica_stavke " +
                " LEFT JOIN meduskladisnica ON meduskladisnica.broj=meduskladisnica_stavke.broj " +
                " WHERE meduskladisnica.datum >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " AND meduskladisnica.datum <= '" + dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                traziSkladiste + traziSifru +
                " GROUP BY meduskladisnica_stavke.sifra";
            DataTable DTMedjuOdSkladista = classSQL.select(sql_me1, "meduskladisnica").Tables[0];

            MedjuskladisniceHelper(DTMedjuOdSkladista, false);
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
              " WHERE radni_nalog.datum_naloga >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
              " AND radni_nalog.datum_naloga <= '" + dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
              traziSkladiste + traziSifru;

            DSSkalkulacija_stavke = classSQL.select(sql_rn, "kartica");
            GetKolicina(DSSkalkulacija_stavke, 6, true);
            FillTable(DSSkalkulacija_stavke);

            Sredi(DSSkalkulacija_stavke.Tables[0], "+");

            // Normativi
            /*string queryNormativi = "select radni_nalog_normativ.sifra AS [Šifra], roba.naziv AS [Naziv], roba.jm AS [JMJ], radni_nalog.datum_naloga AS [Datum], radni_nalog_normativ.nbc AS [NBC], radni_nalog_normativ.vpc AS [VPC], " +
                      "CAST(REPLACE(radni_nalog_stavke.kolicina, ',', '.') AS numeric) * (CAST(REPLACE(radni_nalog_normativ.kolicina, ',', '.') AS numeric) * CAST(REPLACE(normativi_stavke.kolicina, ',', '.') AS numeric)) AS [Količina], " +
                      "concat('RADNI NALOG OD '::text,radni_nalog.broj_naloga::text,'.',radni_nalog.godina_naloga::text) AS [Dokument], " +
                      " 0 AS [Ulaz]," +
                     " radni_nalog.broj_naloga as ID " +
                    "from radni_nalog " +
                    "left join radni_nalog_stavke on radni_nalog.broj_naloga = radni_nalog_stavke.broj_naloga " +
                    "left join radni_nalog_normativ on radni_nalog.broj_naloga = radni_nalog_normativ.broj " +
                    "left join roba on radni_nalog_normativ.sifra = roba.sifra " +
                    "left join normativi_stavke on normativi_stavke.sifra_robe = roba.sifra " +
                    "where radni_nalog.datum_naloga>= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "' " +
                    "AND radni_nalog.datum_naloga <= '" + dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "' " +
                    " AND radni_nalog_normativ.sifra='" + sifra + "'" +
                    traziSkladiste;*/
            string queryNormativi = $@"SELECT radni_nalog_normativ.sifra AS [Šifra]
	                                    , roba.naziv AS [Naziv]
	                                    , roba.jm AS [JMJ]
	                                    , radni_nalog.datum_naloga AS [Datum]
	                                    , radni_nalog_normativ.nbc AS [NBC]
	                                    , radni_nalog_normativ.vpc AS [VPC]
	                                    , CAST(REPLACE(radni_nalog_stavke.kolicina, ',', '.') AS numeric) * (CAST(REPLACE(radni_nalog_normativ.kolicina, ',', '.') AS numeric)) AS [Količina]
	                                    , concat('RADNI NALOG OD '::text,radni_nalog.broj_naloga::text,'.',radni_nalog.godina_naloga::text) AS [Dokument]
	                                    , 0 AS [Ulaz]
	                                    , radni_nalog.broj_naloga AS ID
                                    FROM radni_nalog_normativ
                                    LEFT JOIN radni_nalog ON radni_nalog.broj_naloga = radni_nalog_normativ.broj
                                    LEFT JOIN radni_nalog_stavke ON radni_nalog.broj_naloga = radni_nalog_stavke.broj_naloga
                                    LEFT JOIN roba ON radni_nalog_normativ.sifra = roba.sifra 
                                    WHERE radni_nalog.datum_naloga>= '{dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss")}' AND radni_nalog.datum_naloga <= '{dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss")}' AND radni_nalog_normativ.sifra='{sifra}' {traziSkladiste}";

            DSSkalkulacija_stavke = classSQL.select(queryNormativi, "kartica");
            GetKolicina(DSSkalkulacija_stavke, 6);
            FillTable(DSSkalkulacija_stavke);

            Sredi(DSSkalkulacija_stavke.Tables[0], "-");
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
              " WHERE otpremnice.datum >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
              " AND otpremnice.datum <= '" + dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
              traziSkladiste + traziSifru;

            DSSkalkulacija_stavke = classSQL.select(sql_otpremnica, "kartica");
            GetKolicina(DSSkalkulacija_stavke);
            FillTable(DSSkalkulacija_stavke);

            Sredi(DSSkalkulacija_stavke.Tables[0], "-");
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
              " WHERE racuni.datum_racuna >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
              " AND racuni.datum_racuna <= '" + dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
              traziSkladiste + traziSifru;

            DSSkalkulacija_stavke = classSQL.select(sql_maloprodaja, "kartica");
            GetKolicina(DSSkalkulacija_stavke, 8);
            FillTable(DSSkalkulacija_stavke);

            Sredi(DSSkalkulacija_stavke.Tables[0], "-");

            traziSkladiste = "AND normativi_stavke.id_skladiste='" + cbSkladiste.SelectedValue + "'";
            traziSifru = sifra != "" ? " AND normativi_stavke.sifra_robe='" + sifra + "'" : "";

            sql_maloprodaja = "SELECT " +
              " R.sifra AS [Šifra]," +
              " R.naziv AS [Naziv]," +
              " R.jm AS [JMJ]," +
              " racuni.datum_racuna AS [Datum]," +
              " racun_stavke.nbc AS [NBC]," +
              " racun_stavke.id_ducan," +
              " racun_stavke.id_kasa ," +
              " racun_stavke.vpc AS [VPC]," +
              " racun_stavke.kolicina AS [Količina]," +
              " 'MALOPRODAJA ' + racun_stavke.broj_racuna +'/'+ ducan.ime_ducana +'/'+ blagajna.ime_blagajne AS [Dokument]," +
              " 0 AS [Ulaz]," +
              " racuni.broj_racuna as ID" +
              " FROM racun_stavke " +
              " LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa" +
              " LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe" +
              " LEFT JOIN normativi ON racun_stavke.sifra_robe=normativi.sifra_artikla" +
              " LEFT JOIN normativi_stavke ON normativi_stavke.broj_normativa=normativi.broj_normativa" +
              " LEFT JOIN roba R ON normativi_stavke.sifra_robe=R.sifra" +
              " LEFT JOIN blagajna ON blagajna.id_blagajna=racun_stavke.id_kasa" +
              " LEFT JOIN ducan ON ducan.id_ducan=racun_stavke.id_ducan" +
              " WHERE racuni.datum_racuna >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
              " AND racuni.datum_racuna <= '" + dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
              traziSkladiste + traziSifru + "AND roba.oduzmi='NE'";

            DSSkalkulacija_stavke = classSQL.select(sql_maloprodaja, "kartica");
            GetKolicina(DSSkalkulacija_stavke);
            FillTable(DSSkalkulacija_stavke);

            Sredi(DSSkalkulacija_stavke.Tables[0], "-");
        }

        private void Fakture(string sifra)
        {
            string traziSkladiste = "AND faktura_stavke.id_skladiste='" + cbSkladiste.SelectedValue + "'";
            string traziSifru = sifra != "" ? " AND faktura_stavke.sifra='" + sifra + "'" : "";

            string sql_fak = "SELECT " +
                " roba.sifra AS [Šifra]," +
                " roba.naziv AS [Naziv]," +
                " roba.jm AS [JMJ]," +
                " fakture.date AS [Datum]," +
                " faktura_stavke.id_ducan," +
                " faktura_stavke.id_kasa," +
                " faktura_stavke.nbc AS [NBC]," +
                " faktura_stavke.vpc AS [VPC]," +
                " fakture.oduzmi_iz_skladista AS [oduzmi]," +
                " faktura_stavke.kolicina AS [Količina]," +
                " 'FAKTURA' +' '+ faktura_stavke.broj_fakture AS [Dokument]," +
                " 0 AS [Ulaz]," +
                " fakture.broj_fakture as ID" +
                " FROM faktura_stavke " +
                " LEFT JOIN fakture ON fakture.broj_fakture=faktura_stavke.broj_fakture AND fakture.broj_fakture = faktura_stavke.broj_fakture AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa" +
                " LEFT JOIN roba ON roba.sifra=faktura_stavke.sifra" +
                " WHERE fakture.date >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " AND fakture.date <= '" + dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " AND fakture.oduzmi_iz_skladista='1'" +
                traziSkladiste + traziSifru;

            DSSkalkulacija_stavke = classSQL.select(sql_fak, "kartica");
            GetKolicina(DSSkalkulacija_stavke, 9);
            FillTable(DSSkalkulacija_stavke);

            //Sredi(DSSkalkulacija_stavke.Tables[0], "-");

            double kolicina;
            DataTable dt = DSSkalkulacija_stavke.Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sifra = dt.Rows[i]["Šifra"].ToString();
                kolicina = Convert.ToDouble(dt.Rows[i]["Količina"].ToString());
                if (dt.Rows[i]["oduzmi"].ToString() == "1")
                    dodajKolicinu(sifra, kolicina, "-");
            }
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
                " WHERE ifb.datum >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " AND ifb.datum <= '" + dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                traziSkladiste + traziSifru;

            DSSkalkulacija_stavke = classSQL.select(sql_fak, "kartica");
            GetKolicina(DSSkalkulacija_stavke);
            FillTable(DSSkalkulacija_stavke);
        }

        //void RadniNalogServis(string sifra)
        //{
        //    string traziSkladiste = "AND radni_nalog_servis_stavke.id_skladiste='" + cbSkladiste.SelectedValue + "'";
        //    string traziSifru = sifra != "" ? " AND radni_nalog_servis_stavke.sifra='" + sifra + "'" : "";

        //    string sql_rn = "SELECT " +
        //      " roba.sifra AS [Šifra]," +
        //      " roba.naziv AS [Naziv]," +
        //      " roba.jm AS [JMJ]," +
        //      " radni_nalog_servis.date AS [Datum]," +
        //      " '' AS [NBC]," +
        //      " radni_nalog_servis_stavke.vpc AS [VPC]," +
        //      " radni_nalog_servis_stavke.kolicina AS [Količina]," +
        //      " 'RADNI NALOG SERVIS ' + radni_nalog_servis.broj + '.' + radni_nalog_servis.godina AS [Dokument]" +
        //      " FROM radni_nalog_servis_stavke " +
        //      " LEFT JOIN radni_nalog_servis ON radni_nalog_servis.broj=radni_nalog_servis_stavke.broj" +
        //      " LEFT JOIN roba ON roba.sifra=radni_nalog_servis_stavke.sifra" +
        //      " WHERE radni_nalog_servis.date >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
        //      " AND radni_nalog_servis.date <= '" + dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
        //      traziSkladiste + traziSifru;

        //    DSSkalkulacija_stavke = classSQL.select(sql_rn, "kartica");
        //    FillTable(DSSkalkulacija_stavke);

        //    Sredi(DSSkalkulacija_stavke.Tables[0], "+");
        //}

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
                " WHERE primka.datum >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " AND primka.datum <= '" + dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                traziSkladiste + traziSifru;

            DSSkalkulacija_stavke = classSQL.select(sql_fak, "kartica");
            GetKolicina(DSSkalkulacija_stavke, 6, true);
            FillTable(DSSkalkulacija_stavke);

            Sredi(DSSkalkulacija_stavke.Tables[0], "+");
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
                " WHERE izdatnica.datum >= '" + dtpOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                " AND izdatnica.datum <= '" + dtpDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                traziSkladiste + traziSifru;

            DSSkalkulacija_stavke = classSQL.select(sql_fak, "kartica");
            GetKolicina(DSSkalkulacija_stavke);
            FillTable(DSSkalkulacija_stavke);

            Sredi(DSSkalkulacija_stavke.Tables[0], "-");
        }

        private void FillTable(DataSet DS)
        {
            DataRow dr;

            for (int i = 0; i < DSSkalkulacija_stavke.Tables[0].Rows.Count; i++)
            {
                decimal kol;
                decimal.TryParse(DSSkalkulacija_stavke.Tables[0].Rows[i]["Količina"].ToString().Replace(".", ","), out kol);

                dr = DTitems_sort.NewRow();
                dr["Ulaz"] = DSSkalkulacija_stavke.Tables[0].Rows[i]["Ulaz"].ToString();
                dr["Šifra"] = DSSkalkulacija_stavke.Tables[0].Rows[i]["Šifra"].ToString();
                dr["Naziv"] = DSSkalkulacija_stavke.Tables[0].Rows[i]["Naziv"].ToString();
                dr["Količina"] = Math.Round(kol, 3).ToString("#0.000");
                dr["JMJ"] = DSSkalkulacija_stavke.Tables[0].Rows[i]["JMJ"].ToString();
                dr["Datum"] = Convert.ToDateTime(DSSkalkulacija_stavke.Tables[0].Rows[i]["Datum"].ToString());

                try
                {
                    dr["NBC"] = Math.Round(Convert.ToDouble(DSSkalkulacija_stavke.Tables[0].Rows[i]["NBC"].ToString()), 2).ToString("N2");
                }
                catch
                {
                    dr["NBC"] = "0";
                }
                dr["VPC"] = Math.Round(Convert.ToDouble(DSSkalkulacija_stavke.Tables[0].Rows[i]["VPC"].ToString()), 2).ToString("N3");
                dr["Dokument"] = DSSkalkulacija_stavke.Tables[0].Rows[i]["Dokument"].ToString();
                dr["ID"] = DSSkalkulacija_stavke.Tables[0].Rows[i]["ID"].ToString();

                try
                {
                    bool ima = false;
                    foreach (DataColumn col in DSSkalkulacija_stavke.Tables[0].Columns)
                    {
                        if (col.ToString() == "id_ducan")
                            ima = true;
                    }

                    if (ima)
                    {
                        dr["Poslovnica"] = DSSkalkulacija_stavke.Tables[0].Rows[i]["id_ducan"].ToString();
                        dr["Naplatni"] = DSSkalkulacija_stavke.Tables[0].Rows[i]["id_kasa"].ToString();
                    }
                }
                catch
                {
                    dr["Poslovnica"] = "";
                    dr["Naplatni"] = "";
                }

                DTitems_sort.Rows.Add(dr);
            }

            if (DTitems_sort.Rows.Count > 0)
            {
                DataView dv = DTitems_sort.DefaultView;
                dv.Sort = "Datum";
                DTitems_sort = dv.ToTable();
            }
        }

        private DataTable DTRoba;

        private void btnArtikli_Click(object sender, EventArgs e)
        {
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            roba_trazi.ShowDialog();
            string propertis_sifra = Properties.Settings.Default.id_roba.Trim();
            if (propertis_sifra != "")
            {
                string sql = "";
                //if (cbSkladiste.SelectedValue.ToString() == "1")
                //{
                //    sql = "SELECT " +
                //        " roba.sifra," +
                //        " roba.naziv," +
                //        " roba_prodaja.porez," +
                //        " roba_prodaja.nc," +
                //        " roba_prodaja.kolicina," +
                //        " roba_prodaja.vpc" +
                //        " FROM roba " +
                //        " LEFT JOIN roba_prodaja ON roba.sifra=roba_prodaja.sifra" +
                //        " WHERE roba.sifra='" + propertis_sifra + "'";
                //}
                //else
                //{
                sql = "SELECT " +
                " roba.sifra," +
                " roba.naziv," +
                " roba_prodaja.porez," +
                " roba_prodaja.nc," +
                " roba_prodaja.kolicina," +
                " roba_prodaja.vpc" +
                " FROM roba " +
                " LEFT JOIN roba_prodaja ON roba.sifra=roba_prodaja.sifra" +
                " WHERE roba.sifra='" + propertis_sifra + "' AND roba_prodaja.id_skladiste='" + cbSkladiste.SelectedValue + "'";
                //}

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    try
                    {
                        txtSifraArtikla.Text = DTRoba.Rows[0]["sifra"].ToString();
                        txtImeArtikla.Text = DTRoba.Rows[0]["naziv"].ToString();
                        lblStanje.Text = DTRoba.Rows[0]["kolicina"].ToString();
                        double nc = Math.Round(Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString()), 2);
                        double vpc = Math.Round(Convert.ToDouble(DTRoba.Rows[0]["vpc"].ToString()), 3);
                        double porez = Math.Round(Convert.ToDouble(DTRoba.Rows[0]["porez"].ToString()), 2);
                        double mpc = Math.Round((vpc * porez / 100) + vpc, 2);
                        lblNbc.Text = nc.ToString("#0.00");
                        lblVpc.Text = vpc.ToString("#0.000");
                        lblMpc.Text = mpc.ToString("#0.00");
                    }
                    catch { }
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoji artikl na odabranome skladistu.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtSifraArtikla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                SetStanjeCijene();
                FillGrid();
            }
        }

        private void SetStanjeCijene()
        {
            if (txtSifraArtikla.Text == "")
            {
                frmRobaTrazi roba = new frmRobaTrazi();
                roba.ShowDialog();

                if (Properties.Settings.Default.id_roba != "")
                {
                    txtSifraArtikla.Text = Properties.Settings.Default.id_roba;
                    dtpOdDatuma.Select();
                }
                else
                {
                    return;
                }
            }

            string sql = "";

            sql = "SELECT " +
            " roba.sifra," +
            " roba.naziv," +
            " roba_prodaja.porez," +
            " roba_prodaja.nc," +
            " roba_prodaja.kolicina," +
            " roba_prodaja.vpc" +
            " FROM roba " +
            " LEFT JOIN roba_prodaja ON roba.sifra=roba_prodaja.sifra" +
            " WHERE roba.sifra='" + txtSifraArtikla.Text + "' AND roba_prodaja.id_skladiste='" + cbSkladiste.SelectedValue + "'";

            DTRoba = classSQL.select(sql, "roba").Tables[0];
            if (DTRoba.Rows.Count > 0)
            {
                try
                {
                    //txtSifraArtikla.Text = DTRoba.Rows[0]["sifra"].ToString();
                    txtImeArtikla.Text = DTRoba.Rows[0]["naziv"].ToString();
                    lblStanje.Text = DTRoba.Rows[0]["kolicina"].ToString();
                    double nc = Math.Round(Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString()), 2);
                    double vpc = Math.Round(Convert.ToDouble(DTRoba.Rows[0]["vpc"].ToString()), 3);
                    double porez = Math.Round(Convert.ToDouble(DTRoba.Rows[0]["porez"].ToString()), 2);
                    double mpc = Math.Round((vpc * porez / 100) + vpc, 2);
                    lblNbc.Text = nc.ToString("#0.00");
                    lblVpc.Text = vpc.ToString("#0.000");
                    lblMpc.Text = mpc.ToString("#0.00");
                }
                catch { }
            }
            else
            {
                MessageBox.Show("Za ovu šifru ne postoji artikl na odabranome skladistu.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabKartica_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabKartica.SelectedIndex == 0)
            {
                brRedaka.Text = dgw.Rows.Count.ToString();
            }
            //else if (tabKartica.SelectedIndex == 1)
            //{
            //brRedaka.Text = dgw1.Rows.Count.ToString();
            //}
            else if (tabKartica.SelectedIndex == 1)
            {
                string sqlWhereSkladiste = "";
                if (txtSifraArtikla.Text.Trim() != "")
                    sqlWhereSkladiste = " WHERE roba_prodaja.sifra='" + txtSifraArtikla.Text + "'";

                string sql = "SELECT " +
                    " roba_prodaja.sifra AS [Šifra]," +
                    " skladiste.skladiste AS [Skladište]," +
                    " roba_prodaja.kolicina AS [Količina]," +
                    " roba_prodaja.nc AS [NBC]," +
                    " roba_prodaja.vpc AS [VPC]" +
                    " FROM skladiste" +
                    " INNER JOIN roba_prodaja ON roba_prodaja.id_skladiste=skladiste.id_skladiste" +
                    sqlWhereSkladiste;

                dgw3.DataSource = classSQL.select(sql, "skladiste").Tables[0];

                brRedaka.Text = dgw3.Rows.Count.ToString();
            }
        }

        private void MedjuskladisniceHelper(DataTable dt, bool plusMinus)
        {
            if (plusMinus)
            {
                Sredi(dt, "+");
            }
            else
            {
                Sredi(dt, "-");
            }
        }

        private void Sredi(DataTable dt, string plusMinus)
        {
            string sifra;
            double kolicina;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sifra = dt.Rows[i]["Šifra"].ToString();
                kolicina = Convert.ToDouble(dt.Rows[i]["Količina"].ToString());
                dodajKolicinu(sifra, kolicina, plusMinus);
            }
        }

        private static void dodajKolicinu(string sifra, double kolicina, string plusMinus)
        {
            DataRow[] dataROW = DTroba.Select("Šifra = '" + sifra + "'");

            DataRow RowPdv;

            if (dataROW.Count() == 0)
            {
                RowPdv = DTroba.NewRow();
                if (plusMinus == "+")
                {
                    RowPdv["Količina"] = kolicina;
                }
                else
                {
                    RowPdv["Količina"] = -1 * kolicina;
                }
                RowPdv["Šifra"] = sifra;
                DTroba.Rows.Add(RowPdv);
            }
            else
            {
                if (plusMinus == "+")
                {
                    dataROW[0]["Količina"] = Convert.ToDouble(dataROW[0]["Količina"]) + kolicina;
                }
                else
                {
                    dataROW[0]["Količina"] = Convert.ToDouble(dataROW[0]["Količina"]) - kolicina;
                }
            }
        }

        private static void dodajKoloneDTroba()
        {
            if (DTroba.Columns["Šifra"] == null)
            {
                DTroba.Columns.Add("Šifra");
                DTroba.Columns.Add("Količina");
            }
        }

        private void cbSkladiste_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSifraArtikla.Text = txtSifraArtikla.Text.Trim();
            if (txtSifraArtikla.Text != "")
                SetStanjeCijene();
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w + 7, h + 7);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w - 7, h - 7);
        }

        private void dgw_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string brra = dgw.Rows[e.RowIndex].Cells["ID"].FormattedValue.ToString();
            string broj_ra = brra.ToString();
            string dok = dgw.Rows[e.RowIndex].Cells["Dokument"].FormattedValue.ToString();
            string dok_go = dok.Remove(5);
            pregled(dok_go, broj_ra);
        }

        private void pregled(string dokument, string broj)
        {
            DateTime Dat;
            DateTime.TryParse(dgw.CurrentRow.Cells["Datum"].FormattedValue.ToString(), out Dat);

            string n = dokument;
            switch (n)
            {
                case "MALOP":
                    Report.Faktura.repFaktura rac = new Report.Faktura.repFaktura();
                    rac.dokumenat = "RAC";
                    rac.ImeForme = "Račun";
                    rac.broj_dokumenta = broj;
                    rac.poslovnica = dgw.CurrentRow.Cells["Poslovnica"].FormattedValue.ToString();
                    rac.naplatni = dgw.CurrentRow.Cells["Naplatni"].FormattedValue.ToString();
                    rac.ShowDialog();
                    break;

                case "FAKTU":
                    Report.Faktura.repFaktura fak = new Report.Faktura.repFaktura();
                    fak.dokumenat = "FAK";
                    fak.broj_dokumenta = broj;
                    fak.poslovnica = dgw.CurrentRow.Cells["Poslovnica"].FormattedValue.ToString();
                    fak.naplatni = dgw.CurrentRow.Cells["Naplatni"].FormattedValue.ToString();
                    fak.ShowDialog();
                    break;

                case "OTPRE":
                    Report.Faktura.repFaktura otp = new Report.Faktura.repFaktura();
                    otp.dokumenat = "OTP";
                    otp.broj_dokumenta = broj;
                    otp.otpr_id = true;
                    otp.ShowDialog();
                    break;

                case "KALKU":
                    if (Class.Postavke.idKalkulacija == 2)
                    {
                        Report.Kalkulacija.frmKalkulacija2016 kalk = new Report.Kalkulacija.frmKalkulacija2016();
                        kalk.broj_kalkulacije = broj;

                        kalk.pregled = true;
                        kalk.ShowDialog();
                    }
                    else
                    {
                        Report.Kalkulacija.frmKalkulacija kalk = new Report.Kalkulacija.frmKalkulacija();
                        kalk.broj_kalkulacije = broj;

                        kalk.pregled = true;
                        kalk.ShowDialog();
                    }
                    break;

                case "MEĐUS":

                    DataTable DTm = classSQL.select("SELECT id_skladiste_od,broj FROM meduskladisnica" +
                        " WHERE id='" + dgw.CurrentRow.Cells["id"].FormattedValue.ToString() + "' LIMIT 1", "meduskladisnica").Tables[0];
                    string skl = "";
                    if (DTm.Rows.Count > 0)
                    {
                        skl = DTm.Rows[0][0].ToString();
                        broj = DTm.Rows[0][1].ToString();
                    }

                    Report.Liste.frmListe rfak = new Report.Liste.frmListe();
                    rfak.dokument = "meduskladisnica";
                    rfak.ImeForme = "Međuskladišnica";
                    rfak.broj_dokumenta = broj;
                    rfak.godina = Dat.Year.ToString();
                    rfak.skladiste = skl;
                    rfak.ShowDialog();
                    break;
            }
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            PCPOS.Report.KarticaRobe.frmKarticaRobe kart = new PCPOS.Report.KarticaRobe.frmKarticaRobe();
            kart.tabl = DTitems_sort;
            kart.artikl = "Za artikl: " + txtSifraArtikla.Text + "\r\n" + txtImeArtikla.Text;
            kart.skladiste = "\r\nSa skladišta: " + cbSkladiste.Text;

            kart.ShowDialog();
        }

        private void GetKolicina(DataSet dataSet, int indexKolicina = 6, bool pribroji = false)
        {
            bool initPribroji = pribroji;
            DateTime datum;
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                datum = DateTime.Parse(dataSet.Tables[0].Rows[i]["datum"].ToString());
                if (datum > pocetnoStanjeDatum)
                {
                    decimal kolicina = 0;
                    if (decimal.TryParse(dataSet.Tables[0].Rows[i][indexKolicina].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out kolicina))
                    {
                        if (kolicina < 0 && !pribroji)
                        {
                            pribroji = true;
                            kolicina *= -1;
                        }
                        IzracunajKolicinuNakonPocetnog(kolicina, pribroji);
                        pribroji = initPribroji;
                    }
                }
            }
        }

        private void IzracunajKolicinuNakonPocetnog(decimal kolicina, bool pribroji)
        {
            if (pribroji)
                pocetnoStanjeKolicina += kolicina;
            else
                pocetnoStanjeKolicina -= kolicina;
        }

        private void SetStanje()
        {
            lblStanje.Text = pocetnoStanjeKolicina.ToString("#0.00") + " kom";
        }
    }
}