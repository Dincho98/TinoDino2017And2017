using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmAvansRacun : Form
    {
        public int broj_avansa_edit { get; set; }
        public int godina { get; set; }

        public frmAvansRacun()
        {
            InitializeComponent();
        }

        private static DataTable DTtvrtka = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];
        private static DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private static DataTable DToib = classSQL.select("SELECT oib from zaposlenici where id_zaposlenik='" +
            Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0];

        private DataTable DTRoba = new DataTable();
        private static DataTable DTpdv = new DataTable();
        private DataSet DS_Skladiste = new DataSet();
        private DataSet DS_ZiroRacun = new DataSet();
        private DataSet DS_Zaposlenik = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DSPDV = new DataSet();
        private DataSet DSIzjava = new DataSet();
        private DataSet DSnazivPlacanja = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataTable DSAvansi = new DataTable();
        private DataTable DSfakture = new DataTable();
        private DataTable DTpromocije1;
        private DataTable DSFS = new DataTable();
        private DataTable DTOtprema = new DataTable();
        private bool edit = false;
        public frmMenu MainForm { get; set; }
        private string storno;

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_RESTORE = 0xF120;

            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_RESTORE)
            {
                this.Dock = DockStyle.None;
            }

            base.WndProc(ref m);
        }

        private void frmAvans_Load(object sender, EventArgs e)
        {
            //kreirajTablice();

            this.Paint += new PaintEventHandler(Form1_Paint);

            MyDataGrid.MainForm = this;
            txtBrojAvans.Text = brojAvansa(DateTime.Now.Year);
            numeric();
            fillComboBox();
            EnableDisable(false);
            ControlDisableEnable(true, true, true, true, false, true);
            cbVD.Focus();
            if (broj_avansa_edit != 0 && godina != 0)
            {
                fillForma(broj_avansa_edit, godina);
            }
            else
            {
                c = new Cijene();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 1000);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private class MyDataGrid : System.Windows.Forms.DataGridView
        {
            public static frmAvansRacun MainForm { get; set; }

            protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
            {
                //if (keyData == Keys.Enter)
                //{
                //    MainForm.EnterDGW(MainForm.dgw);
                //    return true;
                //}
                //else if (keyData == Keys.Right)
                //{
                //    MainForm.RightDGW(MainForm.dgw);
                //    return true;
                //}
                //else if (keyData == Keys.Left)
                //{
                //    MainForm.LeftDGW(MainForm.dgw);
                //    return true;
                //}
                //else if (keyData == Keys.Up)
                //{
                //    MainForm.UpDGW(MainForm.dgw);
                //    return true;
                //}
                //else if (keyData == Keys.Down)
                //{
                //    MainForm.DownDGW(MainForm.dgw);
                //    return true;
                //}
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            edit = false;
            EnableDisable(true);
            txtBrojAvans.Text = brojAvansa(DateTime.Now.Year);
            numGodina.Value = DateTime.Now.Year;
            ControlDisableEnable(false, true, true, false, false, false);
            //txtBrojAvans.ReadOnly = true;
            //numGodina.ReadOnly = true;
            c = new Cijene();
            CijeneUTextBox(c);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da želite obrisati ovaj avans?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                //string sql_fis = "SELECT zki FROM avans_racun WHERE broj_avansa = '" + txtBrojAvans.Text + "'";
                //DataTable DTprovjfis = classSQL.select(sql_fis, "avans_racun").Tables[0];

                //if (DTprovjfis.Rows[0]["zki"].ToString().Length > 1)
                //{
                //    MessageBox.Show("Nije moguće mijenjati ovaj avans! Fiskaliziran!");
                //    return;
                //}

                //classSQL.delete("DELETE FROM avansi_stavke WHERE broj_avansa='" + txtBrojAvans.Text + "'");
                classSQL.delete("DELETE FROM avans_racun WHERE broj_avansa = '" + txtBrojAvans.Text + "'");
                classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja)" +
                    " VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "'Brisanje cijelog avansa za racun br." + txtBrojAvans.Text + "')");
                MessageBox.Show("Obrisano.");

                //txtBrojAvans.ReadOnly = false;
                //numGodina.ReadOnly = false;
                edit = false;
                EnableDisable(false);
                ControlDisableEnable(true, false, false, true, false, true);
                deleteFields();
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            //PROVJERA!
            if (textUkupno.Text.Trim() == "" || c.ukupno == 0)
            {
                if (MessageBox.Show("Da li ste sigurni da želite spremiti avans s ovim iznosom?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    textUkupno.Focus();
                    return;
                }
            }

            //ako se editira avans
            try
            {
                long avans = Convert.ToInt64(txtBrojAvans.Text);
            }
            catch
            {
                MessageBox.Show("Unesi numeričku vrijednost za broj avansa!");
                txtBrojAvans.Focus();
                return;
            }

            try
            {
                int avans = Convert.ToInt32(txtAvansirati.Text);
            }
            catch
            {
                MessageBox.Show("Unesi numeričku vrijednost za partnera!", "Greška");
                txtAvansirati.Focus();
                return;
            }

            DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtAvansirati.Text + "'", "partners").Tables[0];
            if (DSpar.Rows.Count < 1)
            {
                MessageBox.Show("Partner pod tim brojem ne postoji!", "Greška");
                txtAvansirati.Focus();
                return;
            }

            KeyEventArgs enter = new KeyEventArgs(Keys.Enter);
            textUkupno_KeyDown(textUkupno, enter);
            textNultaStp_KeyDown(textNultaStp, enter);
            textNeoporezivo_KeyDown(textNeoporezivo, enter);
            textOsnov10_KeyDown(textOsnov10, enter);
            textOsnov25_KeyDown(textOsnov25, enter);

            //bool fiskalizirati = false;
            //if (MessageBox.Show("Fiskalizirati avans?", "Fiskalizacija?",
            //MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    fiskalizirati = true;
            //}

            string sql = string.Format("SELECT * from avans_racun where broj_avansa = '{0}' AND godina_avansa = '{1}' and poslovnica = '{2}'", txtBrojAvans.Text.Trim(), numGodina.Value.ToString(), Class.Postavke.id_default_ducan);

            DSAvansi = classSQL.select(sql, "avans_racun").Tables[0];

            if (c.osnovica10 != 0 && c.osnovicaVar != 0 || c.osnovica10 != 0 && c.nultaStp != 0 ||
                c.osnovica10 != 0 && c.neoporezivo != 0 ||

                c.osnovicaVar != 0 && c.nultaStp != 0 || c.osnovicaVar != 0 && c.neoporezivo != 0 ||

                c.neoporezivo != 0 && c.nultaStp != 0)
            {
                if (MessageBox.Show("Kreiranje avansa s više stopa poreza?", "Upozorenje!",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    MessageBox.Show("Avans nije kreiran.");
                    return;
                }
            }

            DateTime datum = dtpDatumDvo.Value;
            string[] fiskalizacija = new string[3];

            if (DSAvansi.Rows.Count > 0)
            {
                //if (fiskalizirati)
                //    fiskalizacija = FiskalizacijaAvansa(datum);
                //else
                //{
                //    fiskalizacija = new string[3];
                //    fiskalizacija[0] = "";
                //    fiskalizacija[1] = "";
                //    fiskalizacija[2] = "";
                //}

                sql = string.Format(@"UPDATE avans_racun SET broj_avansa = '{0}', poslovnica = '{1}', dat_dok = '{2}', dat_knj = '{3}', id_zaposlenik = '{4}', id_zaposlenik_izradio = '{5}', model = '{6}', id_nacin_placanja = '{7}', id_valuta = '{8}', opis = '{9}', id_vd = '{10}', godina_avansa = '{11}', ukupno = '{12}', ziro = '{13}', nult_stp = '{14}', neoporezivo = '{15}', osnovica10 = '{16}', osnovica_var = '{17}', porez_var = '{18}', id_pdv = '{19}', storno = {20}, id_partner = '{21}', jir = '{22}', zki = '{23}', artikl = '{26}', datum_valute = '{27}'
WHERE broj_avansa='{24}'
AND godina_avansa='{25}';",
                txtBrojAvans.Text,
                Class.Postavke.id_default_ducan,
                dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss"), dtpDatumDvo.Value.ToString("yyyy-MM-dd H:mm:ss"), "0",
                Properties.Settings.Default.id_zaposlenik,
                txtModel.Text,
                cbNacinPlacanja.SelectedValue.ToString(),
                cbValuta.SelectedValue.ToString(),
                rtbOpis.Text,
                cbVD.SelectedValue.ToString(),
                numGodina.Value.ToString(),
                 Math.Round(c.ukupno, 2).ToString("#0.00").Replace(",", "."),
                 cbZiroRacun.SelectedValue.ToString(),
                 Math.Round(c.nultaStp, 2).ToString("#0.00").Replace(",", "."),
                 Math.Round(c.neoporezivo, 2).ToString("#0.00").Replace(",", "."),
                 Math.Round(c.osnovica10, 2).ToString("#0.00").Replace(",", "."),
                 Math.Round(c.osnovicaVar, 2).ToString("#0.00").Replace(",", "."),
                 Math.Round(c.porezVar, 2).ToString("#0.00").Replace(",", "."),
                 cbPDV.SelectedValue.ToString(),
                 storno,
                 txtAvansirati.Text.Trim(),
                 fiskalizacija[0],
                 fiskalizacija[1],
                 txtBrojAvans.Text,
                 numGodina.Value.ToString(), txtOpis.Text.Trim(), dtpDatumValute.Value.ToString("yyyy-MM-dd HH:mm:ss"));

                provjera_sql(classSQL.update(sql));

                provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja)" +
                    " VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "'Edit avans za racun br." + txtBrojAvans.Text + "')"));
                MessageBox.Show("Spremljeno.");
                //if (MessageBox.Show("Spremljeno.\r\nŽelite li ispisati dokument?", "Spremljeno.",
                //MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //{
                //    printaj(txtBrojAvans.Text);
                //}

                EnableDisable(false);
                ControlDisableEnable(true, false, false, true, false, true);

                return;
            }
            //ako se editira avans

            datum = dtpDatumDvo.Value;

            //if (fiskalizirati)
            //    fiskalizacija = FiskalizacijaAvansa(datum);
            //else
            //{
            //    fiskalizacija = new string[3];
            //    fiskalizacija[0] = "";
            //    fiskalizacija[1] = "";
            //    fiskalizacija[2] = "";
            //}

            //za novi avans
            sql = string.Format(@"INSERT INTO avans_racun (broj_avansa, poslovnica, dat_dok, dat_knj, id_zaposlenik, id_zaposlenik_izradio, model, id_nacin_placanja, id_valuta, opis, id_vd, godina_avansa, ukupno, ziro, nult_stp, neoporezivo, osnovica10, osnovica_var, porez_var, id_pdv, id_partner, jir, zki, storno, artikl, datum_valute)
VALUES ( {0}, {1}, '{2}', '{3}', {4}, {5}, '{6}', {7}, {8}, '{9}', {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, '{21}', '{22}', {23}, '{24}', '{25}');",
                txtBrojAvans.Text, Class.Postavke.id_default_ducan, datum.ToString("yyyy-MM-dd H:mm:ss"), dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss"), 0, Properties.Settings.Default.id_zaposlenik, txtModel.Text, cbNacinPlacanja.SelectedValue.ToString(), cbValuta.SelectedValue.ToString(), rtbOpis.Text, cbVD.SelectedValue.ToString(), numGodina.Value.ToString(), Math.Round(c.ukupno, 2).ToString("#0.00").Replace(",", "."), cbZiroRacun.SelectedValue.ToString(), Math.Round(c.nultaStp, 2).ToString("#0.00").Replace(",", "."), Math.Round(c.neoporezivo, 2).ToString("#0.00").Replace(",", "."), Math.Round(c.osnovica10, 2).ToString("#0.00").Replace(",", "."), Math.Round(c.osnovicaVar, 2).ToString("#0.00").Replace(",", "."), Math.Round(c.porezVar, 2).ToString("#0.00").Replace(",", "."), cbPDV.SelectedValue.ToString(), txtAvansirati.Text.Trim(), fiskalizacija[0], fiskalizacija[1], 0, txtOpis.Text.Trim(), dtpDatumValute.Value.ToString("yyyy-MM-dd HH:mm:ss"));

            provjera_sql(classSQL.insert(sql));

            provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja)" +
                " VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                "'Novi račun za avans br." + txtBrojAvans.Text + "')"));
            if (MessageBox.Show("Spremljeno.\r\nŽelite li ispisati dokument?", "Spremljeno.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                printaj(txtBrojAvans.Text, numGodina.Value.ToString());
            }
            //za novi avans

            EnableDisable(false);
            ControlDisableEnable(true, false, false, false, false, false);
            deleteFields();
        }

        //string[] FiskalizacijaAvansa(DateTime datum)
        //{
        //    //priprema za fiskalizaciju

        //    DataTable DTnaknade = new DataTable();
        //    DataTable DTOstaliPor = new DataTable();
        //    bool pdv = DTpostavke.Rows[0]["sustav_pdv"].ToString() == "1" ? true : false;
        //    string oib = DToib.Rows.Count > 0 ? DToib.Rows[0][0].ToString() : "";
        //    string iznososlobpdv = "";
        //    string iznos_marza = "";

        //    double osnovica_sve = 0;
        //    double Porez_potrosnja_sve = 0;

        //    string[] porezNaPotrosnju = setPorezNaPotrosnju();

        //    dodajKoloneDTpdv();
        //    DTpdv.Clear();

        //    if (c.osnovica10 != 0) dodajPDV(10.00, Math.Round(c.osnovica10, 2));
        //    if (c.osnovicaVar != 0) dodajPDV(Math.Round(c.porezVar / c.osnovicaVar, 4) * 100, Math.Round(c.osnovicaVar, 2));
        //    if (c.nultaStp != 0) dodajPDV(0, Math.Round(c.nultaStp, 2));

        //    osnovica_sve += c.osnovica10 + c.osnovicaVar;

        //    porezNaPotrosnju[0] = DTpostavke.Rows[0]["porez_potrosnja"].ToString();
        //    porezNaPotrosnju[1] = Convert.ToString(osnovica_sve);
        //    porezNaPotrosnju[2] = Convert.ToString(Porez_potrosnja_sve);
        //    //priprema za fiskalizaciju

        //    string np = Util.Korisno.VratiNacinPlacanja(cbNacinPlacanja.Text.ToLower());

        //    string[] fiskalizacija = new string[3];
        //    try
        //    {
        //        fiskalizacija = Fiskalizacija.classFiskalizacija.Fiskalizacija(
        //            DTtvrtka.Rows[0]["oib"].ToString(),
        //            oib,
        //            datum,
        //            pdv,
        //            Convert.ToInt32(txtBrojAvans.Text),
        //            DTpdv,
        //            porezNaPotrosnju,
        //            DTOstaliPor,
        //            iznososlobpdv,
        //            iznos_marza,
        //            DTnaknade,
        //            Convert.ToDecimal(c.ukupno),//.ToString().Replace(",", ".")
        //            np,
        //            false,
        //            "avans");
        //    }
        //    catch
        //    {
        //        fiskalizacija = new string[3];
        //        fiskalizacija[0] = "";
        //        fiskalizacija[1] = "";
        //        fiskalizacija[2] = "";
        //    }

        //    return fiskalizacija;
        //}

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            EnableDisable(false);
            deleteFields();
            txtBrojAvans.Text = brojAvansa(DateTime.Now.Year);
            edit = false;
            //txtBrojAvans.ReadOnly = false;
            //numGodina.ReadOnly = false;

            ControlDisableEnable(true, false, false, true, false, true);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmSviAvansiRacun objForm2 = new frmSviAvansiRacun();
            //objForm2.sifra_fakture = "";
            objForm2.MainForm = this;
            objForm2.ShowDialog();
            if (broj_avansa_edit != 0 && godina != 0)
            {
                deleteFields();
                fillForma(broj_avansa_edit, godina);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    //txtSifraOdrediste.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    //txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    txtAvansirati.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartner.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString(); cbVD.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void deleteFields()
        {
            txtBrojAvans.Text = "";
            textNeoporezivo.Text = "";
            textNultaStp.Text = "";
            textOsnov10.Text = "";
            textOsnov25.Text = "";
            textPorez10.Text = "";
            textPorez25.Text = "";
            textUkupno.Text = "";
            //txtIzradio.Text = "";
            txtModel.Text = "";
            rtbOpis.Text = "";
            txtAvansirati.Text = "";
            txtPartner.Text = "";
            dtpDatumDvo.Value = DateTime.Now;
            dtpDatum.Value = dtpDatumDvo.Value;
            txtOpis.Text = "";
        }

        private void EnableDisable(bool x)
        {
            //txtBrojAvans.Enabled = x;
            textNeoporezivo.Enabled = x;
            textNultaStp.Enabled = x;
            textOsnov10.Enabled = x;
            textOsnov25.Enabled = x;
            textPorez10.Enabled = x;
            textPorez25.Enabled = x;
            textUkupno.Enabled = x;
            txtIzradio.Enabled = x;
            txtModel.Enabled = x;
            cbVD.Enabled = x;
            cbValuta.Enabled = x;
            cbZiroRacun.Enabled = x;
            //cbNacinPlacanja.Enabled = x;
            rtbOpis.Enabled = x;
            txtSifraNacinPlacanja.Enabled = x;
            cbPDV.Enabled = x;
            dtpDatumDvo.Enabled = x;
            dtpDatum.Enabled = x;
            //numGodina.Enabled = x;
            txtTecaj.Enabled = x;
            txtAvansirati.Enabled = x;
            button2.Enabled = x;
            txtOpis.Enabled = x;
        }

        private void ControlDisableEnable(bool novi, bool odustani, bool spremi, bool sve, bool delAll, bool obrisi)
        {
            btnNoviUnos.Enabled = novi;

            btnOdustani.Enabled = odustani;

            btnSpremi.Enabled = spremi;

            //btnSpremi.Enabled = sve;

            btnDelete.Enabled = delAll;

            //btnDelete.Enabled = delAll;
        }

        private string brojAvansa()
        {
            DataTable DSbr = classSQL.select(string.Format("SELECT coalesce(MAX(broj_avansa), 0) FROM avans_racun where godina_avansa = {0} and poslovnica = '{1}'", Util.Korisno.GodinaKojaSeKoristiUbazi, Class.Postavke.id_default_ducan), "avans_racun").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private string brojAvansa(int godina)
        {
            DataTable DSbr = classSQL.select(string.Format("SELECT coalesce(MAX(CAST(broj_avansa AS integer)),0) FROM avans_racun where godina_avansa = {0} and poslovnica = '{1}'", godina, Class.Postavke.id_default_ducan), "avans_racun").Tables[0];

            if (DSbr.Rows.Count != 0 && DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToInt64(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "";
            }
        }

        //string VratiSqlGodinaAvans(string andWhere, int godina)
        //{
        //    string sqlZaGodinu = godina == 0 ? "" : " " + andWhere + " godina_avansa='" + godina + "' ";
        //    return sqlZaGodinu;
        //}

        private void fillComboBox()
        {
            numGodina.Value = Convert.ToInt16(DateTime.Now.Year.ToString());

            //fill ziroracun
            DS_ZiroRacun = classSQL.select("SELECT * FROM ziro_racun", "ziro_racun");
            cbZiroRacun.DataSource = DS_ZiroRacun.Tables[0];
            cbZiroRacun.DisplayMember = "ziroracun";
            cbZiroRacun.ValueMember = "id_ziroracun";
            cbZiroRacun.SelectedValue = "1";

            //fill vrsta dokumenta
            //DSvd = classSQL.select("SELECT * FROM avansi_vd ORDER BY id_vd", "avansi_vd");
            DSvd = new DataSet();
            DataTable dt = new DataTable("avansi_vd");
            dt.Columns.Add("id_vd");
            dt.Columns.Add("vd");
            DSvd.Tables.Add(dt);
            DSvd.Tables[0].Rows.Add("1", "Predujam");
            cbVD.DataSource = DSvd.Tables[0];
            cbVD.DisplayMember = "vd";
            cbVD.ValueMember = "id_vd";

            //fill nacin_placanja
            DSnazivPlacanja = classSQL.select("SELECT * FROM nacin_placanja", "nacin_placanja");
            cbNacinPlacanja.DataSource = DSnazivPlacanja.Tables[0];
            cbNacinPlacanja.DisplayMember = "naziv_placanja";
            cbNacinPlacanja.ValueMember = "id_placanje";
            cbNacinPlacanja.SelectedValue = 3;
            txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();
            cbNacinPlacanja.Enabled = false;

            //DS Valuta
            DSValuta = classSQL.select("SELECT * FROM valute", "valute");
            cbValuta.DataSource = DSValuta.Tables[0];
            cbValuta.DisplayMember = "ime_valute";
            cbValuta.ValueMember = "id_valuta";
            cbValuta.SelectedValue = 5;
            txtTecaj.DataBindings.Add("Text", DSValuta.Tables[0], "tecaj");
            txtTecaj.Text = "1";

            //DS PDV
            DSPDV = classSQL.select("SELECT * FROM porezi", "porezi");
            cbPDV.DataSource = DSPDV.Tables[0];
            cbPDV.DisplayMember = "iznos";
            cbPDV.ValueMember = "id_porez";
            cbPDV.SelectedValue = 1;

            //fill tko je prijavljen
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici" +
                " WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
        }

        private void fillForma(long avans, int godina)
        {
            string sql = string.Format(@"SELECT id, broj_avansa, poslovnica, naplatni_uredaj, dat_dok, dat_knj, datum_valute,
       id_zaposlenik, id_zaposlenik_izradio, model, id_nacin_placanja,
       id_valuta, opis, id_vd, godina_avansa, ukupno, nult_stp, neoporezivo,
       osnovica10, osnovica_var, porez_var, id_pdv, id_partner, ziro,
       jir, zki, storno, artikl
  FROM avans_racun where broj_avansa = {0} AND godina_avansa = {1} and poslovnica = {2};", avans, godina, Class.Postavke.id_default_ducan);

            DSAvansi = classSQL.select(sql, "avans_racun").Tables[0];

            if (DSAvansi.Rows.Count < 1)
            {
                MessageBox.Show("Ne postoji avans!");
                deleteFields();
                EnableDisable(false);
                txtBrojAvans.Focus();
                txtBrojAvans.SelectAll();
                return;
            }

            ControlDisableEnable(true, true, true, true, true, true);

            DSAvansi.Rows[0][0].ToString();

            storno = DSAvansi.Rows[0]["storno"].ToString();

            txtBrojAvans.Text = avans.ToString();
            numGodina.Value = Convert.ToInt16(DSAvansi.Rows[0]["godina_avansa"].ToString());
            dtpDatum.Value = Convert.ToDateTime(DSAvansi.Rows[0]["dat_dok"].ToString());
            dtpDatumDvo.Value = Convert.ToDateTime(DSAvansi.Rows[0]["dat_knj"].ToString());
            dtpDatumValute.Value = Convert.ToDateTime(DSAvansi.Rows[0]["datum_valute"].ToString());

            txtIzradio.Text = classSQL.select(string.Format(@"SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='{0}';", Properties.Settings.Default.id_zaposlenik), "zaposlenici").Tables[0].Rows[0]["Ime"].ToString();

            txtModel.Text = DSAvansi.Rows[0]["model"].ToString();
            cbZiroRacun.SelectedValue = DSAvansi.Rows[0]["ziro"].ToString();
            cbValuta.SelectedValue = DSAvansi.Rows[0]["id_valuta"].ToString();
            cbNacinPlacanja.SelectedValue = DSAvansi.Rows[0]["id_nacin_placanja"].ToString();
            rtbOpis.Text = DSAvansi.Rows[0]["opis"].ToString();
            cbPDV.SelectedValue = DSAvansi.Rows[0]["id_pdv"].ToString();
            txtAvansirati.Text = DSAvansi.Rows[0]["id_partner"].ToString();
            txtOpis.Text = DSAvansi.Rows[0]["artikl"].ToString();
            DataTable DSpar = classSQL.select(string.Format(@"SELECT ime_tvrtke FROM partners WHERE id_partner = {0};", txtAvansirati.Text), "partners").Tables[0];
            if (DSpar.Rows.Count > 0) txtPartner.Text = DSpar.Rows[0]["ime_tvrtke"].ToString();

            c = new Cijene(Convert.ToDouble(DSAvansi.Rows[0]["ukupno"].ToString()),
                                  Convert.ToDouble(DSAvansi.Rows[0]["osnovica10"].ToString()),
                                  Convert.ToDouble(DSAvansi.Rows[0]["osnovica_var"].ToString()),
                                  Convert.ToDouble(DSAvansi.Rows[0]["nult_stp"].ToString()),
                                  Convert.ToDouble(DSAvansi.Rows[0]["neoporezivo"].ToString()),
                                  Convert.ToDouble(DSAvansi.Rows[0]["porez_var"].ToString()));
            CijeneUTextBox(c);

            promjeniLabelePorezOsnovica();

            ((Control)this).SelectNextControl(ActiveControl, true, true, true, true);
            EnableDisable(true);
        }

        private void numeric()
        {
            numGodina.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            numGodina.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            numGodina.Value = Convert.ToInt16(DateTime.Now.Year.ToString());
        }

        private void cbPDV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (textUkupno.Text.Trim() == "")
            {
                return;
            }

            try
            {
                //int index = cbPDV.SelectedIndex;

                promjeniLabelePorezOsnovica();

                double porez = Convert.ToDouble(cbPDV.Text);
                c.SrediCijeneZaPorez(porez / 100);

                CijeneUTextBox(c);
            }
            catch
            {
                MessageBox.Show("Unesi numeričku vrijednost!");
            }
        }

        private void cbNacinPlacanja_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void printaj(string broj, string godina)
        {
            Report.Avans.repAvans rav = new Report.Avans.repAvans();
            rav.dokumenat = "avans_racun".ToUpper();
            rav.ImeForme = "Avansi";
            rav.broj_dokumenta = broj;
            rav.godina = godina;
            rav.poslovnica = Class.Postavke.id_default_ducan.ToString();
            rav.ShowDialog();
        }

        private void promjeniLabelePorezOsnovica()
        {
            label11.Text = "Osnovica " + cbPDV.Text + ":";
            label16.Text = "Porez " + cbPDV.Text + ":";
        }

        private void TRENUTNI_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox txt = ((TextBox)sender);
                txt.BackColor = Color.Khaki;
            }
            else if (sender is ComboBox)
            {
                ComboBox txt = ((ComboBox)sender);
                txt.BackColor = Color.Khaki;
            }
            else if (sender is DateTimePicker)
            {
                DateTimePicker control = ((DateTimePicker)sender);
                control.BackColor = Color.Khaki;
            }
            else if (sender is NumericUpDown)
            {
                NumericUpDown control = ((NumericUpDown)sender);
                control.BackColor = Color.Khaki;
            }
        }

        private void NAPUSTENI_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox txt = ((TextBox)sender);
                txt.BackColor = Color.White;
            }
            else if (sender is ComboBox)
            {
                ComboBox txt = ((ComboBox)sender);
                txt.BackColor = Color.White;
                //txt.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            else if (sender is DateTimePicker)
            {
                DateTimePicker control = ((DateTimePicker)sender);
                control.BackColor = Color.White;
            }
            else if (sender is NumericUpDown)
            {
                NumericUpDown control = ((NumericUpDown)sender);
                control.BackColor = Color.White;
            }
        }

        private void KeyDownEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ((Control)sender).SelectNextControl(ActiveControl, true, true, true, true); ;
            }
        }

        private void KeyDownDole(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                txtBrojAvans.Focus();
            }
        }

        private void textPorez25_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.OemPeriod)
            {
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Oemcomma || e.KeyCode == (Keys)110)
            {
                if (textPorez25.Text.Count(x => x == ',') > 0)
                {
                    e.SuppressKeyPress = true;
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                try
                {
                    //string vr = textPorez25.Text;//.Replace(",", ".");
                    //double p = Convert.Todouble(vr);
                    //string post = cbPDV.Text;//.Replace(",", ".");
                    //double p1 = Convert.Todouble(post);

                    //c.SrediCijenePorezVar(p, p1/100);
                    //CijeneUTextBox(c);

                    ((Control)sender).SelectNextControl(ActiveControl, true, true, true, true); ;
                }
                catch
                {
                    MessageBox.Show("Unesi numeričku vrijednost!");
                }
            }
        }

        private void textPorez10_KeyDown(object sender, KeyEventArgs e)
        {
            //točke nisu dopuštene
            if (e.KeyCode == Keys.OemPeriod)
            {
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Oemcomma || e.KeyCode == (Keys)110)
            {
                //samo 1 zarez!
                if (textPorez10.Text.Count(x => x == ',') > 0)
                {
                    e.SuppressKeyPress = true;
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                try
                {
                    string vr = textPorez10.Text;//.Replace(",", ".");
                    double p = Convert.ToDouble(vr);

                    c.SrediCijenePorez10(p);
                    CijeneUTextBox(c);

                    ((Control)sender).SelectNextControl(ActiveControl, true, true, true, true); ;
                }
                catch
                {
                    MessageBox.Show("Unesi numeričku vrijednost!");
                }
            }
        }

        private void textOsnov25_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.OemPeriod)
            {
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Oemcomma || e.KeyCode == (Keys)110)
            {
                if (textOsnov25.Text.Count(x => x == ',') > 0)
                {
                    e.SuppressKeyPress = true;
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                try
                {
                    string vr = textOsnov25.Text;//.Replace(",", ".");
                    double p = Convert.ToDouble(vr);
                    string post = cbPDV.Text;//.Replace(",", ".");
                    double p1 = Convert.ToDouble(post);

                    c.SrediCijeneosnovicaVar(p, p1 / 100);
                    CijeneUTextBox(c);

                    //SendKeys.Send("{TAB}");
                    ((Control)sender).SelectNextControl(ActiveControl, true, true, true, true);
                }
                catch
                {
                    MessageBox.Show("Unesi numeričku vrijednost!");
                }
            }
        }

        private void textOsnov10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.OemPeriod)
            {
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Oemcomma || e.KeyCode == (Keys)110)
            {
                if (textOsnov10.Text.Count(x => x == ',') > 0)
                {
                    e.SuppressKeyPress = true;
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                try
                {
                    string vr = textOsnov10.Text;//.Replace(",", ".");
                    double p = Convert.ToDouble(vr);

                    c.SrediCijeneOsnovica10(p);
                    CijeneUTextBox(c);

                    //SendKeys.Send("{TAB}");
                    ((Control)sender).SelectNextControl(ActiveControl, true, true, true, true);
                }
                catch
                {
                    MessageBox.Show("Unesi numeričku vrijednost!");
                }
            }
        }

        private void textUkupno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.OemPeriod)
            {
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Oemcomma || e.KeyCode == (Keys)110)
            {
                if (textUkupno.Text.Count(x => x == ',') > 0)
                {
                    e.SuppressKeyPress = true;
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                try
                {
                    string vr = textUkupno.Text;//.Replace(",", ".");
                    double p = Convert.ToDouble(vr);
                    string post = cbPDV.Text;//.Replace(",", ".");
                    double p1 = Convert.ToDouble(post);

                    if (p != c.ukupno)
                    {
                        c = new Cijene();
                        c.SrediCijeneUkupno(p, p1 / 100);
                        CijeneUTextBox(c);
                    }

                    //System.Windows.Forms.SendKeys.Send("{TAB}");
                }
                catch
                {
                    MessageBox.Show("Unesi numeričku vrijednost!");
                }
            }
        }

        private void textNultaStp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.OemPeriod)
            {
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Oemcomma || e.KeyCode == (Keys)110)
            {
                if (textNultaStp.Text.Count(x => x == ',') > 0)
                {
                    e.SuppressKeyPress = true;
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                try
                {
                    string vr = textNultaStp.Text;//.Replace(",", ".");
                    double nultaStp = Convert.ToDouble(vr);

                    c.SrediCijeneNultaStp(nultaStp);
                    CijeneUTextBox(c);

                    //SendKeys.Send("{TAB}");
                    ((Control)sender).SelectNextControl(ActiveControl, true, true, true, true);
                }
                catch
                {
                    MessageBox.Show("Unesi numeričku vrijednost!");
                }

                return;
            }
        }

        private void textNeoporezivo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.OemPeriod)
            {
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Oemcomma || e.KeyCode == (Keys)110)
            {
                if (textNeoporezivo.Text.Count(x => x == ',') > 0)
                {
                    e.SuppressKeyPress = true;
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                try
                {
                    string vr = textNeoporezivo.Text;//.Replace(",", ".");
                    double Neo = Convert.ToDouble(vr);

                    c.SrediCijeneNeoporezivo(Neo);
                    CijeneUTextBox(c);

                    //SendKeys.Send("{TAB}");
                    ((Control)sender).SelectNextControl(ActiveControl, true, true, true, true);
                }
                catch
                {
                    MessageBox.Show("Unesi numeričku vrijednost!");
                }

                return;
            }
        }

        private void txtAvansirati_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                string Str = txtAvansirati.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtAvansirati.Text = "0";
                }

                DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtAvansirati.Text + "'", "partners").Tables[0];
                if (DSpar.Rows.Count > 0)
                {
                    txtPartner.Text = DSpar.Rows[0][0].ToString();
                    cbVD.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void txtSifraNacinPlacanja_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                try
                {
                    DataRow[] dataROW = DSnazivPlacanja.Tables[0].Select("id_placanje = " + txtSifraNacinPlacanja.Text);
                    cbNacinPlacanja.SelectedValue = dataROW[0]["id_placanje"].ToString();
                    cbNacinPlacanja.Select();
                    ((Control)sender).SelectNextControl(ActiveControl, true, true, true, true); ;
                }
                catch (Exception)
                {
                    MessageBox.Show("Krivi unos.", "Greška");
                }
            }
        }

        private void txtBrojAvans_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Oemcomma || e.KeyCode == Keys.OemPeriod)
            {
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                long avans;
                int godina;

                try
                {
                    avans = Convert.ToInt64(txtBrojAvans.Text);
                    godina = Convert.ToInt16(numGodina.Value);
                }
                catch
                {
                    MessageBox.Show("Unesi numeričku vrijednost!");
                    txtBrojAvans.Focus();
                    return;
                }

                fillForma(avans, godina);
            }
        }

        #region pozadina za manipulaciju nulta stopa, neoporezivo, osnovica-pdv-ukupno textboxovima

        //grupa osnovica, porez, ukupno...
        private Cijene c;

        private class Cijene
        {
            //sve su iznosi u kn a ne postoci!
            //porez10 je uvijek 10% od osnovica10
            //dok je porezVar varijabilni dio (% odabrani u formi) od osnovicaVar
            public double stopaPDV { get; set; }

            public double ukupno { get; set; }
            public double osnovica10 { get; set; }
            public double porez10 { get; set; }
            public double nultaStp { get; set; }
            public double neoporezivo { get; set; }
            public double osnovicaVar { get; set; }
            public double porezVar { get; set; }

            public Cijene()
            {
                stopaPDV = 0;
                ukupno = 0;
                osnovica10 = 0;
                porez10 = 0;
                osnovicaVar = 0;
                porezVar = 0;
                nultaStp = 0;
                neoporezivo = 0;
            }

            public Cijene(double _ukupno,
                double _osnovica10, double _osnovicaVar,
                double _nultaStp, double _neoporezivo, double _porez25)
            {
                ukupno = _ukupno;
                osnovica10 = _osnovica10;
                porez10 = osnovica10 * .1;
                osnovicaVar = _osnovicaVar;
                porezVar = _porez25;
                nultaStp = _nultaStp;
                neoporezivo = _neoporezivo;
            }

            //public Cijene(double _stopaPDV, double _ukupno,
            //    double _osnovica10, double _porez10,
            //    double _osnovica25, double _porez25,
            //    double _nultaStp, double _neoporezivo)
            //{
            //    stopaPDV = _stopaPDV;
            //    ukupno = _ukupno;
            //    osnovica10 = _osnovica10;
            //    porez10 = _porez10;
            //    osnovicaVar = _osnovica25;
            //    porezVar = _porez25;
            //    nultaStp = _nultaStp;
            //    neoporezivo = _neoporezivo;
            //}

            public void SrediCijenePDV(double pdv)
            {
            }

            public void SrediCijeneUkupno(double u, double postotak)
            {
                ukupno = u;
                osnovicaVar = u / (1 + postotak);
                porezVar = ukupno - osnovicaVar;
            }

            public void SrediCijeneZaPorez(double p)
            {
                ukupno -= (osnovicaVar + porezVar);
                porezVar = osnovicaVar * p;
                ukupno += osnovicaVar + porezVar;
            }

            //public void SrediCijenePostotak25(double p)
            //{
            //    ukupno -= (osnovicaVar + porezVar);
            //    porezVar = osnovicaVar * p;
            //    ukupno += osnovicaVar + porezVar;
            //}

            public void SrediCijenePorezVar(double p, double postotak)
            {
                ukupno -= (osnovicaVar + porezVar);
                osnovicaVar = p * (1 / postotak);
                porezVar = p;
                ukupno += osnovicaVar + porezVar;
            }

            public void SrediCijenePorez10(double p)
            {
                ukupno -= (osnovica10 + porez10);
                osnovica10 = p * 10;
                porez10 = p;
                ukupno += osnovica10 + porez10;
            }

            public void SrediCijenePorez0(double p)
            {
                ukupno -= (osnovicaVar + porezVar);
                osnovicaVar = 0;
                porezVar = 0;
                ukupno += osnovicaVar + porezVar;
            }

            public void SrediCijeneOsnovica10(double o)
            {
                ukupno -= (osnovica10 + porez10);
                osnovica10 = o;
                porez10 = o * .1;
                ukupno += osnovica10 + porez10;
            }

            public void SrediCijeneosnovicaVar(double o, double postotak)
            {
                //prvo se oduzmu trenutne vrijednosti o25 i p25...
                ukupno -= (osnovicaVar + porezVar);
                //...zato da bi se sad promijenile (inače kad bi samo dodali bez oduzimanja prvotnih
                //vrijednosti, dobili bi veći račun)

                ////!!! isto vrijedi za sve promjene!!!

                osnovicaVar = o;
                porezVar = o * postotak;
                ukupno += osnovicaVar + porezVar;
            }

            public void SrediCijeneNultaStp(double n)
            {
                ukupno -= nultaStp;
                nultaStp = n;
                ukupno += nultaStp;
            }

            public void SrediCijeneNeoporezivo(double n)
            {
                ukupno -= neoporezivo;
                neoporezivo = n;
                ukupno += neoporezivo;
            }
        }

        private void CijeneUTextBox(Cijene c)
        {
            textUkupno.Text = Math.Round(c.ukupno, 2).ToString("#0.00");
            textPorez10.Text = Math.Round(c.porez10, 2).ToString("#0.00");
            textPorez25.Text = Math.Round(c.porezVar, 2).ToString("#0.00");
            textOsnov25.Text = Math.Round(c.osnovicaVar, 2).ToString("#0.00");
            textOsnov10.Text = Math.Round(c.osnovica10, 2).ToString("#0.00");
            textNeoporezivo.Text = Math.Round(c.neoporezivo, 2).ToString("#0.00");
            textNultaStp.Text = Math.Round(c.nultaStp, 2).ToString("#0.00");
        }

        #endregion pozadina za manipulaciju nulta stopa, neoporezivo, osnovica-pdv-ukupno textboxovima

        #region fiskalizacija helper

        /// <summary>
        /// Dodaje kolone tablici DTpdv ako još nisu dodane
        /// </summary>
        private static void dodajKoloneDTpdv()
        {
            if (DTpdv.Columns["stopa"] == null)
            {
                DTpdv.Columns.Add("stopa");
                DTpdv.Columns.Add("osnovica");
                DTpdv.Columns.Add("iznos");
            }
        }

        /// <summary>
        /// dodaje stopu PDV-a i iznos u tablicu DTpdv ako ne postoji stopa;
        /// ako postoji zbraja s postojećim iznosom
        /// </summary>
        /// <param name="stopa"></param>
        /// <param name="iznos"></param>
        private static void dodajPDV(double stopa, double iznos)
        {
            DataRow[] dataROW = DTpdv.Select("stopa = '" + stopa.ToString() + "'");
            DataRow RowPdv;

            if (dataROW.Count() == 0)
            {
                RowPdv = DTpdv.NewRow();
                RowPdv["stopa"] = Math.Round(stopa, 2).ToString("#0.00");
                RowPdv["iznos"] = Math.Round(iznos * stopa / 100, 2).ToString("#0.00");
                RowPdv["osnovica"] = iznos.ToString("#0.00");
                DTpdv.Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["iznos"] = (Math.Round(Convert.ToDouble(dataROW[0]["iznos"].ToString()), 2) + Math.Round(iznos * stopa / 100, 2)).ToString("#0.00");
                dataROW[0]["osnovica"] = (Math.Round(Convert.ToDouble(dataROW[0]["osnovica"].ToString()), 2) + Math.Round(iznos, 2)).ToString("#0.00");
            }
        }

        /// <summary>
        /// postavlja porez_na_potrosnju na empty string
        /// </summary>
        /// <returns></returns>
        private static string[] setPorezNaPotrosnju()
        {
            string[] porez_na_potrosnju = new string[3];
            porez_na_potrosnju[0] = "";
            porez_na_potrosnju[1] = "";
            porez_na_potrosnju[2] = "";

            return porez_na_potrosnju;
        }

        #endregion fiskalizacija helper

        /// <summary>
        /// Kreira tablice ako već ne postoje u bazi
        /// </summary>
        //private void kreirajTablice()
        //{
        //    DataTable DTremote = classSQL.select("select table_name from information_schema.tables where TABLE_TYPE <> 'VIEW'", "Table").Tables[0];

        //    if (DTremote.Rows.Count > 0)
        //    {
        //        DataRow[] dataROW = DTremote.Select("table_name = 'avans_racun'");
        //        if (dataROW.Length == 0)
        //        {
        //            string sql = "CREATE TABLE avans_racun" +
        //                                    "(broj_avansa bigint NOT NULL," +
        //                                    "dat_dok timestamp without time zone," +
        //                                    "dat_knj timestamp without time zone," +
        //                                    "id_zaposlenik integer," +
        //                                    "id_zaposlenik_izradio integer," +
        //                                    "model character varying(10)," +
        //                                    "id_nacin_placanja bigint," +
        //                                    "id_valuta integer," +
        //                                    "opis text," +
        //                                    "id_vd character(5)," +
        //                                    "godina_avansa character(6)," +
        //                                    "ukupno numeric," +
        //                                    "nult_stp numeric," +
        //                                    "neoporezivo numeric," +
        //                                    "osnovica10 numeric," +
        //                                    "osnovica_var numeric," +
        //                                    "porez_var numeric," +
        //                                    "id_pdv integer," +
        //                                    "id_partner bigint," +
        //                                    "ziro bigint," +
        //                                    "jir character varying(100)," +
        //                                    "zki character varying(100)," +
        //                                    "storno character varying(2)," +
        //                                    "CONSTRAINT broj_avansa PRIMARY KEY (broj_avansa)" +
        //                                    ")";
        //            classSQL.select(sql, "avans_racun");
        //        }

        //        dataROW = DTremote.Select("table_name = 'avansi_vd'");
        //        if (dataROW.Length == 0)
        //        {
        //            string sql = "CREATE TABLE avansi_vd" +
        //                "(id_vd serial NOT NULL," +
        //                "vd character varying(30)," +
        //                "grupa character varying(5)," +
        //                "CONSTRAINT primary_key_id_vd PRIMARY KEY (id_vd )" +
        //                ")";
        //            classSQL.select(sql, "avansi_vd");
        //            provjera_sql(classSQL.insert("INSERT INTO avansi_vd (vd,grupa) VALUES ('Predujam','IP')"));
        //            provjera_sql(classSQL.insert("INSERT INTO avansi_vd (vd,grupa) VALUES ('Storno primljenog predujma','PRS')"));
        //        }
        //    }
        //}

        private void button3_Click(object sender, EventArgs e)
        {
            frmAvansStornoRacun a = new frmAvansStornoRacun();
            a.ShowDialog();
        }
    }
}