using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmKarticaSkladiste : Form
    {
        public frmKarticaSkladiste()
        {
            InitializeComponent();
        }

        private DataSet DSvd = new DataSet();
        private DataSet DSpartner = new DataSet();
        private DataSet DSMT = new DataSet();
        private DataSet DSroba = new DataSet();
        private DataTable Pocetno = new DataTable();
        public frmMenu MainFormMenu { get; set; }

        private string query = "";

        private void frmKarticaSkladiste_Load(object sender, EventArgs e)
        {
            //label655.Text = "";
            if (!Class.Postavke.prodaja_automobila)
            {
                btnIspisDetaljnoSfakturnimCijenama.Visible = false;
            }

            cbSkl.Select();
            fillComboBox();
            //fillDataGrid();

            //dtpDatumOD.Value = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);
            SetDateStart();
            dtpDatumDO.Value = new DateTime(DateTime.Now.Year, 12, 31, 23, 59, 59);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 500);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void fillComboBox()
        {
            //fill skladiste
            DSMT = classSQL.select("SELECT * FROM skladiste WHERE aktivnost='DA'", "skladiste");
            cbSkl.DataSource = DSMT.Tables[0];
            cbSkl.DisplayMember = "skladiste";
            cbSkl.ValueMember = "id_skladiste";

            //fill partner
            DSMT = classSQL.select("SELECT * FROM skladiste WHERE aktivnost='DA'", "skladiste");
            cbSkl.DataSource = DSMT.Tables[0];
            cbSkl.DisplayMember = "skladiste";
            cbSkl.ValueMember = "id_skladiste";
            try
            {
                DataTable dtGrupe = classSQL.select("select * from grupa", "grupa").Tables[0];
                cmbGrupe.DataSource = dtGrupe;
                cmbGrupe.DisplayMember = "grupa";
                cmbGrupe.ValueMember = "id_grupa";
            }
            catch (Exception ex)
            {
            }
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

        private void txtSifraArtikla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpDatumOD.Select();
            }
        }

        private void txtImeArtikla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpDatumOD.Select();
            }
        }

        private void cbVD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSifraArtikla.Select();
            }
        }

        private void dtpDatumNaloga_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpDatumDO.Select();
            }
        }

        private void txtSifraArtikla_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSifraArtikla.Text != "")
                {
                    DataSet partner = new DataSet();
                    partner = classSQL.select("SELECT sifra,naziv FROM roba WHERE sifra ='" + txtSifraArtikla.Text + "'", "roba");
                    if (partner.Tables[0].Rows.Count > 0)
                    {
                        txtSifraArtikla.Text = partner.Tables[0].Rows[0]["sifra"].ToString();
                        txtImeArtikla.Text = partner.Tables[0].Rows[0]["naziv"].ToString();
                        dtpDatumOD.Select();
                    }
                    else
                    {
                        MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                    }
                }
            }
        }

        private void txtImeArtikla_KeyDown_1(object sender, KeyEventArgs e)
        {
            dtpDatumOD.Select();
        }

        private void dtpDatumDO_KeyDown(object sender, KeyEventArgs e)
        {
            //txtSifraDob.Select();
        }

        private void fillDataGrid()
        {
            string top = "";
            string remote = "";
            if (classSQL.remoteConnectionString != "")
            {
                remote = " LIMIT 5000";
            }
            else
            {
                top = " TOP(5000) ";
            }

            string sql = "SELECT " + top + " roba_prodaja.sifra AS [Šifra], roba.naziv AS [Naziv]," +
                "roba_prodaja.kolicina AS [Količina], skladiste.skladiste AS [Skladište],roba.mpc AS [Cijena] FROM roba_prodaja " +
                " INNER JOIN roba ON roba.sifra=roba_prodaja.sifra " +
                " INNER JOIN skladiste ON skladiste.id_skladiste=roba_prodaja.id_skladiste ORDER BY [Šifra] " +
                remote;
            DSroba = classSQL.select(sql, "skladiste");

            DataTable dt = DSroba.Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i].SetField("Cijena", Math.Round(Convert.ToDouble(dt.Rows[i]["Cijena"].ToString()), 2).ToString("#0.00"));
            }

            dgw.DataSource = dt;

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Format = "N2";
            dgw.Columns["cijena"].DefaultCellStyle = style;

            dgw.Columns["cijena"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["cijena"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //label6.Text = dgw.Rows.Count.ToString();
        }

        private DataTable DT_artikli;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string DateStart = dtpDatumOD.Value.ToString("yyyy-MM-dd 00:00:00");
            string DateEnd = dtpDatumDO.Value.ToString("yyyy-MM-dd 23:59:59");

            string dodatni_filter = "";

            if (chbDobavljac.Checked)
            {
                dodatni_filter += " AND roba.id_partner='" + txtSifraDob.Text + "' ";
            }
            if (chbSifraArtikla.Checked)
            {
                dodatni_filter += "AND roba_prodaja.sifra='" + txtSifraArtikla.Text + "'";
            }

            if (chbGrupe.Checked && cmbGrupe.SelectedValue != null)
            {
                dodatni_filter += "AND roba.id_grupa = '" + cmbGrupe.SelectedValue.ToString() + "' ";
            }

            if (chbVeciOdNule.Checked && chbManjiOdNule.Checked)
            {
                dodatni_filter += "AND (CAST(REPLACE(roba_prodaja.kolicina,',','.') as NUMERIC)<'0' or CAST(REPLACE(roba_prodaja.kolicina,',','.') as NUMERIC)>'0')";
            }
            else
            {
                if (chbVeciOdNule.Checked)
                {
                    dodatni_filter += " AND CAST(REPLACE(roba_prodaja.kolicina,',','.') as NUMERIC)>'0'";
                }

                if (chbManjiOdNule.Checked)
                {
                    dodatni_filter += " AND CAST(REPLACE(roba_prodaja.kolicina,',','.') as NUMERIC)<'0'";
                }
            }

            string sql = "DROP TABLE IF EXISTS tmpskl;\n " +
"CREATE TEMPORARY TABLE tmpskl AS\n " +
            "SELECT roba.sifra as [sifra], roba_prodaja.kolicina as [kolicina], roba.naziv as [naziv],skladiste.skladiste as [skladiste],roba_prodaja.vpc,round((((roba_prodaja.vpc*CAST(REPLACE(roba_prodaja.porez,',','.') as NUMERIC)/100) zbroj roba_prodaja.vpc)),2) as [mpc], roba_prodaja.nc as [nbc],\n" +
            " coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM kalkulacija_stavke LEFT JOIN kalkulacija ON kalkulacija.broj=kalkulacija_stavke.broj AND kalkulacija.id_skladiste=kalkulacija_stavke.id_skladiste WHERE sifra=roba.sifra AND CAST(kalkulacija.datum AS timestamp)>='" + DateStart + "' AND CAST(kalkulacija.datum AS timestamp)<='" + DateEnd + "' AND kalkulacija_stavke.id_skladiste='" + cbSkl.SelectedValue.ToString() + "' ),0) as [kalkulacija] ,\n" +
            " coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM racun_stavke LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa WHERE sifra_robe=roba.sifra AND CAST(racuni.datum_racuna AS timestamp)>='" + DateStart + "' AND CAST(racuni.datum_racuna AS timestamp)<='" + DateEnd + "' AND racun_stavke.id_skladiste='" + cbSkl.SelectedValue.ToString() + "'),0) as [maloprodaja],\n" +
            " coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM izdatnica_stavke LEFT JOIN izdatnica ON izdatnica.broj=izdatnica_stavke.broj AND izdatnica.id_izdatnica=izdatnica_stavke.id_izdatnica WHERE sifra=roba.sifra AND CAST(izdatnica.datum AS timestamp)>='" + DateStart + "' AND CAST(izdatnica.datum AS timestamp)<='" + DateEnd + "' AND izdatnica.id_skladiste='" + cbSkl.SelectedValue.ToString() + "'),0) as [izdatnica],\n" +
            " coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM primka_stavke LEFT JOIN primka ON primka.id_primka=primka_stavke.id_primka WHERE primka_stavke.sifra=roba.sifra AND CAST(primka.datum AS timestamp)>='" + DateStart + "' AND CAST(primka.datum AS timestamp)<='" + DateEnd + "' AND primka.id_skladiste='" + cbSkl.SelectedValue.ToString() + "'),0) as [primka],\n" +
            " coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM faktura_stavke LEFT JOIN fakture ON fakture.broj_fakture=faktura_stavke.broj_fakture AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa WHERE sifra=roba.sifra AND fakture.oduzmi_iz_skladista='1' AND CAST(fakture.date AS timestamp)>='" + DateStart + "' AND CAST(fakture.date AS timestamp)<='" + DateEnd + "' AND faktura_stavke.id_skladiste='" + cbSkl.SelectedValue.ToString() + "'),0) as [fakture],\n" +
            " coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM meduskladisnica_stavke LEFT JOIN meduskladisnica ON meduskladisnica.broj=meduskladisnica_stavke.broj AND meduskladisnica.id_skladiste_od=meduskladisnica_stavke.iz_skladista WHERE sifra=roba.sifra AND CAST(meduskladisnica.datum AS timestamp)>='" + DateStart + "' AND CAST(meduskladisnica.datum AS timestamp)<='" + DateEnd + "' AND meduskladisnica.id_skladiste_do='" + cbSkl.SelectedValue.ToString() + "'),0) as [meduskladisnica_na],\n" +
            " coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM meduskladisnica_stavke LEFT JOIN meduskladisnica ON meduskladisnica.broj=meduskladisnica_stavke.broj AND meduskladisnica.id_skladiste_od=meduskladisnica_stavke.iz_skladista WHERE sifra=roba.sifra AND CAST(meduskladisnica.datum AS timestamp)>='" + DateStart + "' AND CAST(meduskladisnica.datum AS timestamp)<='" + DateEnd + "' AND meduskladisnica.id_skladiste_od='" + cbSkl.SelectedValue.ToString() + "'),0) as [meduskladisnica_iz],\n" +
            " coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM otpremnica_stavke LEFT JOIN otpremnice ON otpremnice.broj_otpremnice=otpremnica_stavke.broj_otpremnice AND otpremnice.id_skladiste=otpremnica_stavke.id_skladiste WHERE otpremnice.oduzmi_iz_skladista = true and otpremnica_stavke.sifra_robe=roba.sifra AND CAST(otpremnice.datum AS timestamp)>='" + DateStart + "' AND CAST(otpremnice.datum AS timestamp)<='" + DateEnd + "' AND otpremnica_stavke.id_skladiste='" + cbSkl.SelectedValue.ToString() + "'),0) as [otpremnice],\n" +
            " coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM otpis_robe_stavke LEFT JOIN otpis_robe ON otpis_robe.broj=otpis_robe_stavke.broj WHERE otpis_robe_stavke.sifra=roba.sifra AND CAST(otpis_robe.datum AS timestamp)>='" + DateStart + "' AND CAST(otpis_robe.datum AS timestamp)<='" + DateEnd + "' AND otpis_robe.id_skladiste='" + cbSkl.SelectedValue.ToString() + "'),0) as [otpis_robe],\n" +
            " coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM povrat_robe_stavke LEFT JOIN povrat_robe ON povrat_robe.broj=povrat_robe_stavke.broj WHERE povrat_robe_stavke.sifra=roba.sifra AND povrat_robe.datum>='" + DateStart + "' AND CAST(povrat_robe.datum AS timestamp)<='" + DateEnd + "' AND povrat_robe.id_skladiste='" + cbSkl.SelectedValue.ToString() + "'),0) as [povratnica],\n" +
            " coalesce((SELECT CAST(REPLACE(kolicina,',','.') as NUMERIC) FROM pocetno WHERE sifra=roba.sifra AND cast(pocetno.datum as timestamp) >= '" + DateStart + "' AND cast(pocetno.datum as timestamp) <= '" + DateEnd + "' AND id_skladiste='" + cbSkl.SelectedValue.ToString() + "'),0) as [pocetno_stanje],\n" +

            //OVO SVE JE RADNI NALOG IZLAZ sam-sh-224db-bebe
            " coalesce((SELECT\n" +
            " (SELECT \n" +
            "   SUM( \n" +
                "CAST(REPLACE(radni_nalog_stavke.kolicina,',','.') as NUMERIC)* \n" +
                "(SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM normativi_stavke \n" +
                "LEFT JOIN normativi ON normativi.broj_normativa = normativi_stavke.broj_normativa  \n" +
                "WHERE normativi_stavke.sifra_robe = roba_prodaja.sifra AND normativi_stavke.id_skladiste='" + cbSkl.SelectedValue.ToString() + "'\n" +
                "AND normativi.sifra_artikla=radni_nalog_stavke.sifra_robe\n" +
                "))\n" +
            ") AS Kolicina\n" +
            " FROM radni_nalog_stavke\n" +
            " LEFT JOIN radni_nalog ON radni_nalog.broj_naloga=radni_nalog_stavke.broj_naloga\n" +
            " LEFT JOIN normativi ON normativi.sifra_artikla=radni_nalog_stavke.sifra_robe\n" +
            " LEFT JOIN normativi_stavke ON normativi_stavke.broj_normativa=normativi.broj_normativa\n" +
            " WHERE normativi_stavke.sifra_robe=roba.sifra AND CAST(radni_nalog.datum_naloga AS date)>='" + DateStart + "' AND CAST(radni_nalog.datum_naloga AS date)<='" + DateEnd + "'\n" +
            " GROUP BY normativi_stavke.sifra_robe),0) AS [radni_nalog_oduzmi],\n" +

            //OVO JE RADNI NALOG ULAZ
            " coalesce((SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM radni_nalog_stavke LEFT JOIN radni_nalog ON radni_nalog.broj_naloga=radni_nalog_stavke.broj_naloga WHERE radni_nalog_stavke.sifra_robe=roba.sifra AND radni_nalog_stavke.id_skladiste='" + cbSkl.SelectedValue.ToString() + "' AND CAST(radni_nalog.datum_naloga AS date)>='" + DateStart + "' AND CAST(radni_nalog.datum_naloga AS date)<='" + DateEnd + "'),0) AS [radni_nalog_dodaj],\n" +
           // KRAJ RADNI NALOG

           /* " (SELECT SUM("+
            " CAST(REPLACE(kolicina,',','.') as NUMERIC) * (SELECT SUM(CAST(REPLACE(normativi_stavke.kolicina,',','.') as NUMERIC)) FROM normativi "+
            " LEFT JOIN roba ON roba.sifra = normativi.sifra_artikla"+
            " LEFT JOIN normativi_stavke ON normativi_stavke.broj_normativa=normativi.broj_normativa WHERE roba.oduzmi='NE' AND normativi.sifra_artikla=racun_stavke.sifra_robe AND normativi_stavke.sifra_robe=roba.sifra))" +
            " FROM racun_stavke "+
            " LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa "+
            " WHERE CAST(racuni.datum_racuna AS date)>='" + DateStart + "' AND CAST(racuni.datum_racuna AS date)<='" + DateEnd + "' "+
            " AND racun_stavke.id_skladiste='" + cbSkl.SelectedValue.ToString() + "' AND racun_stavke.sifra_robe "+
            " IN ((SELECT normativi.sifra_artikla FROM normativi LEFT JOIN normativi_stavke ON normativi_stavke.broj_normativa=normativi.broj_normativa "+
            " WHERE normativi_stavke.sifra_robe=roba.sifra AND normativi_stavke.id_skladiste=roba_prodaja.id_skladiste))) AS [Normativ]" +
            //*/

           " coalesce((SELECT \n" +
           " COALESCE(SUM\n" +
           " (\n" +
           "     CAST(REPLACE(kolicina,',','.') as NUMERIC)*\n" +
           "     (SELECT SUM(CAST(REPLACE(kolicina,',','.') as NUMERIC)) FROM racun_stavke LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa \n" +
           "       WHERE normativi.sifra_artikla=racun_stavke.sifra_robe\n" +
           "     )\n" +
           " ),0) AS kolicina\n" +
           " FROM normativi_stavke \n" +
           "    LEFT JOIN normativi ON normativi.broj_normativa = normativi_stavke.broj_normativa  \n" +
           "     LEFT JOIN roba ON roba.sifra=normativi.sifra_artikla\n" +
           "    WHERE roba.oduzmi='NE' AND normativi_stavke.sifra_robe=roba_prodaja.sifra AND normativi_stavke.id_skladiste=roba_prodaja.id_skladiste\n" +
           "     GROUP BY sifra_robe,id_skladiste), 0) AS [normativ]\n" +

            " FROM roba \n" +
            " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=roba.sifra\n" +
            " LEFT JOIN skladiste ON skladiste.id_skladiste=roba_prodaja.id_skladiste\n" +
            " WHERE roba.oduzmi='DA' AND roba_prodaja.id_skladiste='" + cbSkl.SelectedValue.ToString() + "' " + dodatni_filter + " LIMIT 3000;" +
            " SELECT t.sifra as [Šifra], t.pocetno_stanje zbroj t.kalkulacija zbroj t.primka zbroj t.meduskladisnica_na zbroj t.radni_nalog_dodaj - t.maloprodaja - t.fakture - t.meduskladisnica_iz - t.otpremnice - t.otpis_robe - t.radni_nalog_oduzmi - t.Izdatnica - t.povratnica as [Kolicina skladište], t.naziv as [Naziv], t.skladiste as [Skladište], t.vpc, t.mpc, t.nbc, t.normativ as [Normativ], t.kalkulacija as Kalkulacija, t.maloprodaja as Maloprodaja, t.izdatnica as Izdatnica, t.primka as Primka, t.fakture as Fakture, t.meduskladisnica_na as [Meduskladisnica na skladista], t.meduskladisnica_iz as [Meduskladisnica sa skladista], t.otpremnice as Otpremnice, t.povratnica as Povratnica, t.radni_nalog_oduzmi as [Radni nalog oduzmi], t.radni_nalog_dodaj as [Radni nalog dodaj], t.pocetno_stanje as [Pocetno stanje], t.otpis_robe as [Otpis robe] FROM tmpskl t;";

            query = sql;

            DT_artikli = classSQL.select(sql, "Artikli").Tables[0];
            dgw.DataSource = DT_artikli;

            dgw.Columns["Kalkulacija"].Visible = false;
            dgw.Columns["Maloprodaja"].Visible = false;
            dgw.Columns["Izdatnica"].Visible = false;
            dgw.Columns["Primka"].Visible = false;
            dgw.Columns["Fakture"].Visible = false;
            dgw.Columns["Meduskladisnica na skladista"].Visible = false;
            dgw.Columns["Meduskladisnica sa skladista"].Visible = false;
            dgw.Columns["Otpremnice"].Visible = false;
            dgw.Columns["Povratnica"].Visible = false;
            dgw.Columns["Radni nalog oduzmi"].Visible = false;
            dgw.Columns["Radni nalog dodaj"].Visible = false;
            dgw.Columns["Pocetno stanje"].Visible = false;
            dgw.Columns["NBC"].Visible = false;
            dgw.Columns["Otpis Robe"].Visible = false;
        }

        private void SetKucice(DataGridViewRow r)
        {
            try
            {
                //ulaz
                lblKalk.Text = r.Cells["Kalkulacija"].FormattedValue.ToString() == "" ? "0" : r.Cells["Kalkulacija"].FormattedValue.ToString();
                lblPrimka.Text = r.Cells["Primka"].FormattedValue.ToString() == "" ? "0" : r.Cells["Primka"].FormattedValue.ToString();
                lblUlazMS.Text = r.Cells["Meduskladisnica na skladista"].FormattedValue.ToString() == "" ? "0" : r.Cells["Meduskladisnica na skladista"].FormattedValue.ToString();
                lblPS.Text = r.Cells["Pocetno stanje"].FormattedValue.ToString() == "" ? "0" : r.Cells["Pocetno stanje"].FormattedValue.ToString();
                lblRadniD.Text = r.Cells["Radni nalog dodaj"].FormattedValue.ToString() == "" ? "0" : r.Cells["Radni nalog dodaj"].FormattedValue.ToString();

                //izlaz
                decimal mp = r.Cells["Maloprodaja"].FormattedValue.ToString() == "" ? 0 : Convert.ToDecimal(r.Cells["Maloprodaja"].FormattedValue.ToString());
                decimal norm = r.Cells["Normativ"].FormattedValue.ToString() == "" ? 0 : Convert.ToDecimal(r.Cells["Normativ"].FormattedValue.ToString());
                lblKasa.Text = (mp + norm).ToString();
                lblFaktura.Text = r.Cells["Fakture"].FormattedValue.ToString() == "" ? "0" : r.Cells["Fakture"].FormattedValue.ToString();
                lblIzdatnica.Text = r.Cells["Izdatnica"].FormattedValue.ToString() == "" ? "0" : r.Cells["Izdatnica"].FormattedValue.ToString();
                lblPovratnica.Text = r.Cells["Povratnica"].FormattedValue.ToString() == "" ? "0" : r.Cells["Povratnica"].FormattedValue.ToString();
                lblMeduskladisteIzlaz.Text = r.Cells["Meduskladisnica sa skladista"].FormattedValue.ToString() == "" ? "0" : r.Cells["Meduskladisnica sa skladista"].FormattedValue.ToString();
                lblRadniN.Text = r.Cells["Radni nalog oduzmi"].FormattedValue.ToString() == "" ? "0" : r.Cells["Radni nalog oduzmi"].FormattedValue.ToString();
                lblOtpremnica.Text = r.Cells["Otpremnice"].FormattedValue.ToString() == "" ? "0" : r.Cells["Otpremnice"].FormattedValue.ToString();
                lblOtpis.Text = r.Cells["Otpis Robe"].FormattedValue.ToString() == "" ? "0" : r.Cells["Otpis Robe"].FormattedValue.ToString();

                //ukupno
                lblUlaz.Text = Math.Round((Convert.ToDecimal(lblKalk.Text) + Convert.ToDecimal(lblPrimka.Text) + Convert.ToDecimal(lblUlazMS.Text) + Convert.ToDecimal(lblPS.Text) + Convert.ToDecimal(lblRadniD.Text)), 3).ToString("#0.000");
                lblIzlaz.Text = Math.Round((Math.Abs(Convert.ToDecimal(lblOtpis.Text)) + Math.Abs(Convert.ToDecimal(lblKasa.Text)) + Math.Abs(Convert.ToDecimal(lblFaktura.Text)) + Math.Abs(Convert.ToDecimal(lblIzdatnica.Text)) + Math.Abs(Convert.ToDecimal(lblPovratnica.Text)) + Math.Abs(Convert.ToDecimal(lblMeduskladisteIzlaz.Text)) + Math.Abs(Convert.ToDecimal(lblRadniN.Text)) + Math.Abs(Convert.ToDecimal(lblOtpremnica.Text))), 3).ToString("#0.000");
                lblStanjeSKL.Text = r.Cells["Kolicina skladište"].FormattedValue.ToString();
                lblStanjeSklProgramski.Text = ((Convert.ToDecimal(lblUlaz.Text) - Convert.ToDecimal(lblIzlaz.Text))).ToString();
            }
            catch
            { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmRobaTrazi robaTrazi = new frmRobaTrazi();
            robaTrazi.ShowDialog();
            if (Properties.Settings.Default.id_roba != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT sifra,naziv FROM roba WHERE sifra ='" + Properties.Settings.Default.id_roba + "'", "roba");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtSifraArtikla.Text = partner.Tables[0].Rows[0]["sifra"].ToString();
                    txtImeArtikla.Text = partner.Tables[0].Rows[0]["naziv"].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void btnArtikli_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtSifraDob.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtNazivDob.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void txtSifraDob_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSifraDob.Text != "")
                {
                    DataSet partner = new DataSet();
                    partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + txtSifraDob.Text + "'", "partners");
                    if (partner.Tables[0].Rows.Count > 0)
                    {
                        txtSifraDob.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                        txtNazivDob.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            DataTable dt1 = DT_artikli;
            PCPOS.Report.KarticaSkladista.frmKarticaSkladista kart = new PCPOS.Report.KarticaSkladista.frmKarticaSkladista();
            kart.tabl = dt1;
            if (chbSifraArtikla.Checked) { kart.artikl = " Po artiklu: " + txtSifraArtikla.Text + "  " + txtImeArtikla.Text; } else { kart.artikl = ""; }
            if (chbDobavljac.Checked) { kart.dobavljac = " Dobavljač: " + txtSifraDob.Text + "  " + txtNazivDob.Text; } else { kart.dobavljac = ""; }
            if (chbGrupe.Checked) { kart.grupa = " Grupa: " + cmbGrupe.Text; } else { kart.grupa = ""; }

            kart.DatumOD = dtpDatumOD.Value.ToString("dd.MM.yyyy");
            kart.DatumDO = dtpDatumDO.Value.ToString("dd.MM.yyyy");
            kart.ShowDialog();
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SetKucice(dgw.CurrentRow);
        }

        private void btnIspisDetaljno_Click(object sender, EventArgs e)
        {
            DataTable dt1 = DT_artikli;
            PCPOS.Report.frmKarticaSkladistaDetaljno.frmKarticaSkladistaDetaljno kart = new PCPOS.Report.frmKarticaSkladistaDetaljno.frmKarticaSkladistaDetaljno();
            kart.fakturna_cijena = false;
            kart.tabl = dt1;
            if (radNaziv.Checked) kart.sortiranje = "naziv";
            if (radSifra.Checked) kart.sortiranje = "sifra";
            if (chbSifraArtikla.Checked) { kart.artikl = " Po artiklu: " + txtSifraArtikla.Text + "\r\n" + txtImeArtikla.Text + "\r\n"; } else { kart.artikl = ""; }
            if (chbDobavljac.Checked) { kart.dobavljac = " Dobavljač: " + txtSifraDob.Text + "  " + txtNazivDob.Text + "\r\n"; } else { kart.dobavljac = ""; }
            kart.skladiste = "Za skladište: " + cbSkl.Text + "\r\n";

            kart.ShowDialog();
        }

        private void btnIspisDetaljnoSfakturnimCijenama_Click(object sender, EventArgs e)
        {
            query = query.Replace("FROM tmpskl t;", @", ks.fak_cijena
FROM tmpskl t
left join kalkulacija_stavke ks on t.sifra = ks.sifra;");

            DataTable dt1 = classSQL.select(query, "podaci").Tables[0];
            PCPOS.Report.frmKarticaSkladistaDetaljno.frmKarticaSkladistaDetaljno kart = new PCPOS.Report.frmKarticaSkladistaDetaljno.frmKarticaSkladistaDetaljno();
            kart.fakturna_cijena = true;
            kart.tabl = dt1;
            if (radNaziv.Checked) kart.sortiranje = "naziv";
            if (radSifra.Checked) kart.sortiranje = "sifra";
            if (chbSifraArtikla.Checked) { kart.artikl = " Po artiklu: " + txtSifraArtikla.Text + "\r\n" + txtImeArtikla.Text + "\r\n"; } else { kart.artikl = ""; }
            if (chbDobavljac.Checked) { kart.dobavljac = " Dobavljač: " + txtSifraDob.Text + "  " + txtNazivDob.Text + "\r\n"; } else { kart.dobavljac = ""; }
            kart.skladiste = "Za skladište: " + cbSkl.Text + "\r\n";

            kart.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetDateStart()
        {
            string query = "SELECT broj_inventure, datum FROM inventura ORDER BY broj_inventure DESC LIMIT 1";
            DataSet dataSet = classSQL.select(query, "inventura");
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                DateTime pocetnoDatum = DateTime.Parse(dataSet.Tables[0].Rows[0]["datum"].ToString());
                if (pocetnoDatum > new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0))
                {
                    dtpDatumOD.Value = pocetnoDatum;
                    SetPocetnoDatumObavijest(true);
                }
            }
            else
            {
                dtpDatumOD.Value = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);
                SetPocetnoDatumObavijest(false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private void SetPocetnoDatumObavijest(bool value)
        {
            if (value)
                lblPocetnoDatum.Text = $@"Datum zadnje inventure je automatski postavljen te stanje prije toga nije uzeto u obzir!";
            else
                lblPocetnoDatum.Text = "";
        }
    }
}