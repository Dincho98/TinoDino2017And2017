using Microsoft.Reporting.WinForms;
using PCPOS.Report;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.FakturaVan
{
    public partial class frmFakturaVan : Form
    {
        public string broj_fakture_edit { get; set; }

        public frmFakturaVan()
        {
            InitializeComponent();
        }

        private static DataTable DTtvrtka = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];

        //private static DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
        private static DataTable DToib = classSQL.select("SELECT oib from zaposlenici where id_zaposlenik='" +
            Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0];

        private DataTable DTRoba = new DataTable();
        private DataSet DS_Skladiste = new DataSet();
        private DataSet DS_ZiroRacun = new DataSet();
        private DataSet DS_Zaposlenik = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DSIzjava = new DataSet();
        private DataSet DSnazivPlacanja = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataTable DSfakture = new DataTable();
        private DataTable DTpromocije1;
        private DataTable DSFS = new DataTable();
        private DataTable DTOtprema = new DataTable();
        private static DataTable DTpdv = new DataTable();
        private static DataRow RowPdv;

        private double u = 0;
        private bool edit = false;
        private double SveUkupno = 0;
        public frmMenu MainForm { get; set; }

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

        private bool ucitano = false;

        private void frmFaktura_Load(object sender, EventArgs e)
        {
            btnStorno.Enabled = true;

            MyDataGrid.MainForm = this;

            numeric();
            DTpromocije1 = classSQL.select("SELECT * FROM promocija1", "promocija1").Tables[0];
            //DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            fillComboBox();
            ttxBrojFakture.Text = brojFakture();
            EnableDisable(false);
            ControlDisableEnable(1, 0, 0, 1, 0);
            if (broj_fakture_edit != null) { fillFaktute(); }
            ucitano = true;
            //this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private class MyDataGrid : System.Windows.Forms.DataGridView
        {
            public static frmFakturaVan MainForm { get; set; }

            protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
            {
                if (keyData == Keys.Enter)
                {
                    MainForm.EnterDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Right)
                {
                    MainForm.RightDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Left)
                {
                    MainForm.LeftDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Up)
                {
                    MainForm.UpDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Down)
                {
                    MainForm.DownDGW(MainForm.dgw);
                    return true;
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void EnterDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            if (d.CurrentCell.ColumnIndex == 3)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 7)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[8];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 8)
            {
                int curent = d.CurrentRow.Index;
                txtSifra_robe.Text = "";
                txtSifra_robe.Focus();
            }
        }

        private void LeftDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            if (d.CurrentCell.ColumnIndex == 3)
            {
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 7)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 8)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
                d.BeginEdit(true);
            }
        }

        private void RightDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            if (d.CurrentCell.ColumnIndex == 3)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 7)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[8];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 8)
            {
                int curent = d.CurrentRow.Index;
                txtSifra_robe.Text = "";
                txtSifra_robe.Focus();
            }
        }

        private void UpDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            int curent = d.CurrentRow.Index;
            if (d.CurrentCell.ColumnIndex == 3)
            {
            }
            else if (curent == 0)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index - 1].Cells[3];
                d.BeginEdit(true);
            }
        }

        private void DownDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            int curent = d.CurrentRow.Index;
            if (d.CurrentCell.ColumnIndex == 3)
            {
                SendKeys.Send("{F4}");
            }
            else if (curent == d.RowCount - 1)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index + 1].Cells[3];
                d.BeginEdit(true);
            }
        }

        private void ControlDisableEnable(int novi, int odustani, int spremi, int sve, int delAll)
        {
            if (novi == 0)
            {
                btnNoviUnos.Enabled = false;
            }
            else if (novi == 1)
            {
                btnNoviUnos.Enabled = true;
            }

            if (odustani == 0)
            {
                btnOdustani.Enabled = false;
            }
            else if (odustani == 1)
            {
                btnOdustani.Enabled = true;
            }

            if (spremi == 0)
            {
                btnSpremi.Enabled = false;
            }
            else if (spremi == 1)
            {
                btnSpremi.Enabled = true;
            }

            if (sve == 0)
            {
                btnSveFakture.Enabled = false;
            }
            else if (sve == 1)
            {
                btnSveFakture.Enabled = true;
            }

            if (delAll == 0)
            {
                btnDeleteAllFaktura.Enabled = false;
            }
            else if (delAll == 1)
            {
                btnDeleteAllFaktura.Enabled = true;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void fillComboBox()
        {
            nmGodinaFakture.Value = Convert.ToInt16(DateTime.Now.Year.ToString());

            //fill ziroracun
            DS_ZiroRacun = classSQL.select("SELECT * FROM ziro_racun", "ziro_racun");
            cbZiroRacun.DataSource = DS_ZiroRacun.Tables[0];
            cbZiroRacun.DisplayMember = "ziroracun";
            cbZiroRacun.ValueMember = "id_ziroracun";
            //cbZiroRacun.SelectedValue = "1";

            //fill otprema
            DTOtprema = classSQL.select("SELECT * FROM otprema", "otprema").Tables[0];
            cbOtprema.DataSource = DTOtprema;
            cbOtprema.DisplayMember = "naziv";
            cbOtprema.ValueMember = "id_otprema";

            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbKomercijalist.DataSource = DS_Zaposlenik.Tables[0];
            cbKomercijalist.DisplayMember = "IME";
            cbKomercijalist.ValueMember = "id_zaposlenik";

            //fill izjava
            DSIzjava = classSQL.select("SELECT * FROM izjava ORDER BY id_izjava", "izjava");
            cbIzjava.DataSource = DSIzjava.Tables[0];
            cbIzjava.DisplayMember = "izjava";
            cbIzjava.ValueMember = "id_izjava";

            //fill vrsta dokumenta
            DSvd = classSQL.select("SELECT * FROM fakture_vd WHERE grupa='fak' ORDER BY id_vd", "fakture_vd");
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

            //DS Valuta
            DSValuta = classSQL.select("SELECT * FROM valute", "valute");
            cbValuta.DataSource = DSValuta.Tables[0];
            cbValuta.DisplayMember = "ime_valute";
            cbValuta.ValueMember = "id_valuta";
            cbValuta.SelectedValue = 5;
            txtTecaj.DataBindings.Add("Text", DSValuta.Tables[0], "tecaj");
            txtTecaj.Text = "1";

            DataTable DTSK = new DataTable("skladiste");
            DTSK.Columns.Add("id_skladiste", typeof(string));
            DTSK.Columns.Add("skladiste", typeof(string));

            DS_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste");
            //DTSK.Rows.Add(0,"");
            for (int i = 0; i < DS_Skladiste.Tables[0].Rows.Count; i++)
            {
                DTSK.Rows.Add(DS_Skladiste.Tables[0].Rows[i]["id_skladiste"], DS_Skladiste.Tables[0].Rows[i]["skladiste"]);
            }

            skladiste.DataSource = DTSK;
            skladiste.DataPropertyName = "skladiste";
            skladiste.DisplayMember = "skladiste";
            skladiste.HeaderText = "Skladište";
            skladiste.Name = "skladiste";
            skladiste.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            skladiste.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            skladiste.ValueMember = "id_skladiste";

            //fill tko je prijavljen
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();

            DS_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste");

            if (cbValuta.SelectedValue.ToString() == "5")
            {
                chbObracunPoreza.Visible = false;
            }
            else
            {
                chbObracunPoreza.Visible = true;
            }
        }

        private void numeric()
        {
            nmGodinaFakture.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nmGodinaFakture.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nmGodinaFakture.Value = Convert.ToInt16(DateTime.Now.Year.ToString());

            nuGodinaPredujma.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nuGodinaPredujma.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nuGodinaPredujma.Value = Convert.ToInt16(DateTime.Now.Year.ToString());

            nuGodinaPonude.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nuGodinaPonude.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nuGodinaPonude.Value = Convert.ToInt16(DateTime.Now.Year.ToString());

            nuGodinaServisa.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nuGodinaServisa.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nuGodinaServisa.Value = Convert.ToInt16(DateTime.Now.Year.ToString());
        }

        private void SetCijenaSkladiste()
        {
            if (dgw.CurrentRow.Cells["skladiste"].Value != null)
            {
                DataSet dsRobaProdaja = new DataSet();
                dsRobaProdaja = classSQL.select("SELECT * FROM roba_prodaja WHERE id_skladiste='" + dgw.CurrentRow.Cells["skladiste"].Value + "' AND sifra='" + dgw.CurrentRow.Cells["sifra"].FormattedValue.ToString() + "'", "roba_prodaja");
                if (dsRobaProdaja.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToDouble(dsRobaProdaja.Tables[0].Rows[0]["kolicina"].ToString()) > 0)
                    {
                        if (chbObracunPoreza.Checked)
                        {
                            dgw.CurrentRow.Cells["porez"].Value = dsRobaProdaja.Tables[0].Rows[0]["porez"].ToString();
                        }
                        else
                        {
                            dgw.CurrentRow.Cells["porez"].Value = "0";
                        }
                        dgw.CurrentRow.Cells["vpc"].Value = string.Format("{0:0.000}", dsRobaProdaja.Tables[0].Rows[0]["vpc"]);
                        lblNaDan.ForeColor = Color.Lime;
                        lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima " + dsRobaProdaja.Tables[0].Rows[0]["kolicina"].ToString() + " " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
                    }
                    else
                    {
                        if (chbObracunPoreza.Checked)
                        {
                            dgw.CurrentRow.Cells["porez"].Value = dsRobaProdaja.Tables[0].Rows[0]["porez"].ToString();
                        }
                        else
                        {
                            dgw.CurrentRow.Cells["porez"].Value = "0";
                        }
                        dgw.CurrentRow.Cells["vpc"].Value = string.Format("{0:0.000}", dsRobaProdaja.Tables[0].Rows[0]["vpc"]);
                        lblNaDan.ForeColor = Color.Red;
                        lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima " + dsRobaProdaja.Tables[0].Rows[0]["kolicina"].ToString() + " " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
                        MessageBox.Show("Na odabranom skladištu nemate unešeni artikl.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    lblNaDan.ForeColor = Color.Red;
                    lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima 0,00 " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
                }
            }
        }

        private string ReturnSifra(string sifra)
        {
            if (sifra.Length > 3)
            {
                if (DTpromocije1.Rows[0]["aktivnost"].ToString() == "DA" && sifra.Substring(0, 3) == "000")
                {
                    return "00000";
                }
            }
            return sifra;
        }

        private void txtSifraArtikla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtSifra_robe.Text == "")
                {
                    frmRobaTrazi roba = new frmRobaTrazi();
                    roba.ShowDialog();

                    if (Properties.Settings.Default.id_roba != "")
                    {
                        txtSifra_robe.Text = Properties.Settings.Default.id_roba;
                        txtSifra_robe.Select();
                    }
                    else
                    {
                        return;
                    }
                }

                if (txtSifra_robe.Text.Length > 2)
                {
                    if (DTpromocije1.Rows[0]["aktivnost"].ToString() == "DA" && txtSifra_robe.Text.Substring(0, 3) == "000")
                    {
                        double uk;
                        double popust;
                        DataTable DTrp = classSQL.select("SELECT * FROM racun_popust_kod_sljedece_kupnje WHERE broj_racuna='" + txtSifra_robe.Text.Substring(3, txtSifra_robe.Text.Length - 3) + "' AND dokumenat='FA'", "racun_popust_kod_sljedece_kupnje").Tables[0];

                        if (DTrp.Rows.Count == 0)
                        {
                            MessageBox.Show("Ovaj popust nije valjan.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (DTrp.Rows[0]["koristeno"].ToString() == "DA")
                        {
                            MessageBox.Show("Ovaj popust je već iskorišten.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        DateTime dateFromPopust = Convert.ToDateTime(DTrp.Rows[0]["datum"].ToString()).AddDays(Convert.ToDouble(DTpromocije1.Rows[0]["traje_do"].ToString()));

                        if (dateFromPopust < DateTime.Now)
                        {
                            MessageBox.Show("Ovom popustu je istekao datum.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        uk = Convert.ToDouble(DTrp.Rows[0]["ukupno"].ToString());
                        popust = Convert.ToDouble(DTrp.Rows[0]["popust"].ToString());
                        uk = uk * 3 / 100;

                        if ((Convert.ToDouble(SveUkupno) - uk) < 0)
                        {
                            MessageBox.Show("Popust je veći od ukupnog računa.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        dgw.Rows.Add(
                                    dgw.RowCount - 1,
                                    txtSifra_robe.Text,
                                    "Popust sa prethodnog računa",
                                    "1",
                                    "kn",
                                    1,
                                    //DTpostavke.Rows[0]["pdv"].ToString(),
                                    Class.Postavke.pdv.ToString(),
                                    Math.Round(uk * -1, 2).ToString("#0.00"),
                                    "0",
                                    "0",
                                    Math.Round(uk * -1 / (1 + Class.Postavke.pdv / 100), 3).ToString("#0.000"),
                                    Math.Round(uk * -1 / (1 + Class.Postavke.pdv / 100), 3).ToString("#0.000"),
                                    Math.Round(uk * -1, 2).ToString("#0.00"),
                                    Math.Round(uk * -1 / (1 + Class.Postavke.pdv / 100), 3).ToString("#0.000"),
                                    Math.Round(uk * -1, 2).ToString("#0.00"),
                                    "",
                                    "",
                                    ""
                                );

                        int br = dgw.Rows.Count - 1;
                        dgw.ClearSelection();
                        izracun(true);
                        dgw.ClearSelection();
                        txtSifra_robe.Text = "";
                        txtSifra_robe.Select();
                        return;
                    }
                }

                //for (int y = 0; y < dgw.Rows.Count; y++)
                //{
                //    if (txtSifra_robe.Text == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString().Trim())
                //    {
                //        MessageBox.Show("Artikl ili usluga već postoje!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        return;
                //    }
                //}

                string sql = "SELECT * FROM roba WHERE sifra='" + txtSifra_robe.Text + "'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();
                    ttxBrojFakture.ReadOnly = true;
                    nmGodinaFakture.ReadOnly = true;
                    cbValuta.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle,
                System.Globalization.CultureInfo.CurrentCulture, out result);
        }

        private void izracun(bool izracunajPovrNaknadIspocetka)
        {
            if (dgw.RowCount > 0)
            {
                int rowBR = dgw.CurrentRow.Index;

                double dec_parse;
                if (!double.TryParse(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString(), out dec_parse))
                {
                    dgw.Rows[rowBR].Cells["kolicina"].Value = "1";
                    MessageBox.Show("Greška kod upisa količine.", "Greška");
                    return;
                }

                if (!double.TryParse(dgw.Rows[rowBR].Cells["rabat"].FormattedValue.ToString(), out dec_parse))
                {
                    dgw.Rows[rowBR].Cells["rabat"].Value = "0";
                    MessageBox.Show("Greška kod upisa rabata.", "Greška");
                    return;
                }

                if (!double.TryParse(dgw.Rows[rowBR].Cells["rabat_iznos"].FormattedValue.ToString(), out dec_parse))
                {
                    dgw.Rows[rowBR].Cells["rabat_iznos"].Value = "0,00";
                    MessageBox.Show("Greška kod rabata.", "Greška");
                    return;
                }

                //if (isNumeric(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) == false) { dgw.Rows[rowBR].Cells["kolicina"].Value = "1"; MessageBox.Show("Greška kod upisa količine.", "Greška"); }
                //    if (isNumeric(dgw.Rows[rowBR].Cells["rabat"].FormattedValue.ToString().ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) == false) { dgw.Rows[rowBR].Cells["rabat"].Value = "0"; MessageBox.Show("Greška kod upisa rabata.", "Greška"); }
                //    if (isNumeric(dgw.Rows[rowBR].Cells["rabat_iznos"].FormattedValue.ToString().ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) == false) { dgw.Rows[rowBR].Cells["rabat_iznos"].Value = "0,00"; MessageBox.Show("Greška kod upisa rabata.", "Greška"); }

                double kol = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString()), 3);

                if (!Class.Dokumenti.isKasica && ((Class.PodaciTvrtka.oibTvrtke == Class.Postavke.OIB_PC1 && Util.Korisno.GodinaKojaSeKoristiUbazi >= Class.Postavke.GODINA_POCETKA_FIFO_PC1) || Util.Korisno.GodinaKojaSeKoristiUbazi >= Class.Postavke.GODINA_POCETKA_FIFO))
                {
                    double nbc = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["nc"].FormattedValue.ToString()), 6);
                    decimal _nbc = Class.FIFO.getNbc(Util.Korisno.GodinaKojaSeKoristiUbazi, dtpDatum.Value, Convert.ToInt32(dgw.CurrentRow.Cells["skladiste"].Value.ToString()), kol, dgw.Rows[rowBR].Cells["sifra"].Value.ToString(), nbc);
                    dgw.Rows[rowBR].Cells["nc"].Value = _nbc.ToString();
                }

                double vpc = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["vpc"].FormattedValue.ToString()), 3);
                double porez = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["porez"].FormattedValue.ToString()), 2);
                double rbt = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["rabat"].FormattedValue.ToString()), 2);
                //double porez_na_potrosnju = Convert.ToDouble(dgw.Rows[rowBR].Cells["porez_potrosnja"].FormattedValue.ToString());
                //string velep = classSQL.select_settings("Select veleprodaja From postavke", "veleprodaja").Tables[0].Rows[0][0].ToString();
                double porez_ukupno = vpc * porez / 100;
                double mpc = porez_ukupno + vpc;
                double rabat = 0;
                //if (velep == "1")
                if (Class.Postavke.veleprodaja)
                {
                    rabat = vpc * rbt / 100;
                }
                else
                {
                    rabat = mpc * rbt / 100;
                }

                double mpc_sa_kolicinom = mpc * kol;

                if (Class.Postavke.veleprodaja)
                {
                    dgw.Rows[rowBR].Cells["mpc"].Value = Math.Round(mpc, 2).ToString("#0.00");
                    dgw.Rows[rowBR].Cells["rabat_iznos"].Value = Math.Round(rabat * kol, 2).ToString("#0.00");
                    dgw.Rows[rowBR].Cells["iznos_bez_pdva"].Value = Math.Round((((vpc - rabat) * kol)), 3).ToString("#0.00");
                    dgw.Rows[rowBR].Cells["iznos_ukupno"].Value = Math.Round((((vpc - rabat) * (1 + porez / 100)) * kol), 2).ToString("#0.00");
                    dgw.Rows[rowBR].Cells["cijena_bez_pdva"].Value = Math.Round((vpc - rabat), 3).ToString("#0.000");
                    dgw.Rows[rowBR].Cells["kolicina"].Value = kol.ToString("#0.000");
                    dgw.Rows[rowBR].Cells["rabat"].Value = rbt.ToString("#0.00");
                    dgw.Rows[rowBR].Cells["porez"].Value = porez.ToString("#0.00");
                }
                else
                {
                    dgw.Rows[rowBR].Cells["mpc"].Value = Math.Round(mpc, 2).ToString("#0.00");
                    dgw.Rows[rowBR].Cells["rabat_iznos"].Value = Math.Round(rabat * kol, 2).ToString("#0.00");
                    dgw.Rows[rowBR].Cells["iznos_bez_pdva"].Value = Math.Round((((mpc - rabat) * kol) / (1 + porez / 100)), 3).ToString("#0.00");
                    dgw.Rows[rowBR].Cells["iznos_ukupno"].Value = Math.Round((mpc - rabat) * kol, 2).ToString("#0.00");
                    dgw.Rows[rowBR].Cells["cijena_bez_pdva"].Value = Math.Round(((mpc - rabat) / (1 + porez / 100)), 3).ToString("#0.000");
                    dgw.Rows[rowBR].Cells["kolicina"].Value = kol.ToString("#0.000");
                    dgw.Rows[rowBR].Cells["rabat"].Value = rbt.ToString("#0.00");
                    dgw.Rows[rowBR].Cells["porez"].Value = porez.ToString("#0.00");
                }

                SrediPovratnuNaknaduUkupno(rowBR, izracunajPovrNaknadIspocetka);
            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = ((ComboBox)sender).SelectedIndex;
            //MessageBox.Show("Selected Index = " + selectedIndex);
        }

        private void btnPartner_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtSifraOdrediste.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    txtSifraFakturirati.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartnerNaziv1.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString(); cbVD.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void btnPartner1_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtSifraOdrediste.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    txtSifraFakturirati.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartnerNaziv1.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString(); cbVD.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        #region ON_KEY_DOWN_REGION

        private void txtSifraOdrediste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtSifraOdrediste.Text == "")
                {
                    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                    partnerTrazi.ShowDialog();
                    if (Properties.Settings.Default.id_partner != "")
                    {
                        DataSet partner = new DataSet();
                        partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                        if (partner.Tables[0].Rows.Count > 0)
                        {
                            txtSifraOdrediste.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                            txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                            txtSifraFakturirati.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                            txtPartnerNaziv1.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString(); cbVD.Select();
                            txtSifraFakturirati.Select();
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtSifraOdrediste.Select();
                        }
                    }
                    else
                    {
                        txtSifraOdrediste.Select();
                        return;
                    }
                }

                string Str = txtSifraOdrediste.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtSifraOdrediste.Text = "0";
                }

                DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraOdrediste.Text + "'", "partners").Tables[0];
                if (DSpar.Rows.Count > 0)
                {
                    txtPartnerNaziv.Text = DSpar.Rows[0][0].ToString();
                    txtSifraFakturirati.Text = txtSifraOdrediste.Text;
                    txtSifraFakturirati_KeyDown(txtSifraOdrediste, e);
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void txtSifraFakturirati_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                string Str = txtSifraFakturirati.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtSifraFakturirati.Text = "0";
                }

                DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraFakturirati.Text + "'", "partners").Tables[0];
                if (DSpar.Rows.Count > 0)
                {
                    txtPartnerNaziv1.Text = DSpar.Rows[0][0].ToString();
                    cbVD.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void cbVD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                dtpDatum.Select();
            }
        }

        private void dtpDatum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                dtpDatumDVO.Value = dtpDatum.Value;
                dtpDanaValuta.Value = dtpDatum.Value;
                dtpDatumDVO.Select();
            }
        }

        private void dtpDatumDVO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtDana.Select();
            }
        }

        private void txtDana_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
                {
                    (sender as TextBox).Text = "0";
                    dtpDanaValuta.Select();
                }

                try
                {
                    DateTime dvo = dtpDatumDVO.Value;
                    dtpDanaValuta.Value = dvo.AddDays(Convert.ToInt16(txtDana.Text)); ;
                    dtpDanaValuta.Select();
                }
                catch (Exception)
                {
                }
            }
        }

        private void dtpDanaValuta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                try
                {
                    DateTime dt1 = dtpDanaValuta.Value;
                    DateTime dt2 = dtpDatumDVO.Value;
                    TimeSpan ts = dt1 - dt2;
                    txtDana.Text = (Convert.ToInt16(ts.Days.ToString()) + 1).ToString();
                    cbIzjava.Select();
                }
                catch (Exception)
                {
                }
            }
        }

        private void cbIzjava_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                rtbNapomena.Text += cbIzjava.Text;
                cbKomercijalist.Select();
            }
        }

        private void cbKomercijalist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtModel.Select();
            }
        }

        private void txtIzradio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtModel.Select();
            }
        }

        private void txtModel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifraNacinPlacanja.Select();
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
                }
                catch (Exception)
                {
                    MessageBox.Show("Krivi unos.", "Greška");
                }
            }
        }

        private void cbNacinPlacanja_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void cbZiroRacun_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbValuta.Select();
            }
        }

        private void cbValuta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifraNacinPlacanja.Select();
            }
        }

        private void txtTecaj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifraNacinPlacanja.Select();
            }
        }

        private void txtSifraNacinPlacanja_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbNacinPlacanja.Select();
                cbNacinPlacanja.SelectedValue = txtSifraNacinPlacanja.Text;
            }
        }

        private void cbNacinPlacanja_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSifraRadniNalog.Select();
                txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();
            }
        }

        private void txtSifraRadniNalog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbRadniBalog.Select();
            }
        }

        private void cbRadniBalog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifraNarKupca.Select();
            }
        }

        private void txtSifraNarKupca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbNarKupca.Select();
            }
        }

        private void cbNarKupca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtNarKupca1.Select();
            }
        }

        private void txtNarKupca1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbOtprema.Select();
            }
        }

        private void txtOtprema_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                nuGodinaPredujma.Select();
            }
        }

        private void nuGodinaPredujma_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbPredujam.Select();
            }
        }

        private void cbPredujam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                rtbNapomena.Select();
            }
        }

        private void rtbNapomena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (rtbNapomena.Text == "")
                {
                    e.SuppressKeyPress = true;
                    txtSifra_robe.Select();
                }
            }
        }

        #endregion ON_KEY_DOWN_REGION

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dgw.RowCount == 0)
            {
                return;
            }
            if (dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].Value != null)
            {
                DataRow[] dataROW = DSFS.Select("id_stavka = " + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].Value.ToString());

                if (MessageBox.Show("Brisanjem ove stavke brišete i količinu robe sa skladišta. Da li ste sigurni da želite obrisati ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (chbOduzmiIzSkladista.Checked)
                    {
                        if (dgw.Rows[dgw.CurrentRow.Index].Cells["oduzmi"].FormattedValue.ToString() == "DA")
                        {
                            string kol = classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dgw.Rows[dgw.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString();
                            kol = (Convert.ToDouble(kol) + Convert.ToDouble(dataROW[0]["kolicina"].ToString())).ToString();
                            classSQL.update("UPDATE roba_prodaja SET kolicina='" + kol + "' WHERE sifra='" + dgw.Rows[dgw.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'");
                        }
                    }

                    classSQL.delete("DELETE FROM faktura_van_stavke WHERE id_stavka='" + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() + "'");
                    dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                    classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje stavke sa fakture_van br." + ttxBrojFakture.Text + "')");
                    MessageBox.Show("Obrisano.");
                }
            }
            else
            {
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                MessageBox.Show("Obrisano.");
            }

            izracun(false);
        }

        private void SrediPovratnuNaknaduUkupno(int rowBR, bool izracunajPovrNaknadIspocetka)
        {
            double kol = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString()), 3);

            //double povr_n_uk = 0;

            //for (int i = 0; i < dgw.RowCount; i++)
            //{
            //    povr_n_uk += Convert.ToDouble(dgw.Rows[i].Cells["povratna_naknada"].FormattedValue.ToString());
            //}
            //txtPovrNaknada.Text = Math.Round(povr_n_uk, 2).ToString("#0.00");

            if (izracunajPovrNaknadIspocetka)
            {
                DataTable DTtemp = classSQL.select("SELECT iznos FROM povratna_naknada WHERE sifra='" + dgw.Rows[rowBR].Cells["sifra"].Value + "'", "povratna_naknada")
                        .Tables[0];
                double povratnaNaknada = DTtemp.Rows.Count > 0 ? Convert.ToDouble(DTtemp.Rows[0]["iznos"].ToString()) * kol : 0;
                dgw.Rows[rowBR].Cells["povratna_naknada"].Value = Math.Round(povratnaNaknada, 2).ToString("#0.00");
            }
            else
            {
                dgw.Rows[rowBR].Cells["povratna_naknada"].Value = dgw.Rows[rowBR].Cells["povratna_naknada"].Value;
            }

            //double pdv = 0;
            double povr_n_uk = 0;
            double B_pdv = 0;
            u = 0;
            double iznos;
            double povratnaNaknadaZaUk;

            for (int i = 0; i < dgw.RowCount; i++)
            {
                try
                {
                    povratnaNaknadaZaUk = Convert.ToDouble(dgw.Rows[i].Cells["povratna_naknada"].FormattedValue.ToString());
                }
                catch
                {
                    povratnaNaknadaZaUk = 0;
                }
                povr_n_uk += povratnaNaknadaZaUk;
                iznos = Convert.ToDouble(dgw.Rows[i].Cells["iznos_ukupno"].FormattedValue.ToString());
                u += Math.Round(iznos, 2);
                iznos = Convert.ToDouble(dgw.Rows[i].Cells["iznos_bez_pdva"].FormattedValue.ToString());
                B_pdv += Math.Round(iznos, 2);
            }

            SveUkupno = u;
            textBox1.Text = "Ukupno sa PDV-om: " + Math.Round(u, 2).ToString("#0.00");
            textBox2.Text = "Bez PDV-a: " + Math.Round(B_pdv, 2).ToString("#0.00");
            textBox3.Text = "PDV: " + Math.Round(Math.Round(u, 2) - Math.Round(B_pdv, 2), 2).ToString("#0.00");

            txtPovrNaknada.Text = Math.Round(povr_n_uk, 2).ToString("#0.00");
        }

        private void btnDeleteAllFaktura_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Brisanjem ove fakture_van brišete i količinu robe sa skladišta. Da li ste sigurni da želite obrisati ovu fakturu?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                double skl = 0;
                double fa_kolicina = 0;
                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    if (dgw.Rows[i].Cells["id_stavka"].Value != null)
                    {
                        if (chbOduzmiIzSkladista.Checked)
                        {
                            if (dgw.Rows[i].Cells["oduzmi"].FormattedValue.ToString() == "DA")
                            {
                                DataRow[] dataROW = DSFS.Select("id_stavka = " + dgw.Rows[i].Cells["id_stavka"].Value.ToString());
                                skl = Convert.ToDouble(classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString());
                                fa_kolicina = Convert.ToDouble(dataROW[0]["kolicina"].ToString());

                                skl = skl + fa_kolicina;
                                classSQL.update("UPDATE roba_prodaja SET kolicina='" + skl + "' WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'");
                            }
                        }
                    }
                }

                classSQL.delete("DELETE FROM faktura_van_stavke WHERE broj_fakture='" + ttxBrojFakture.Text + "'");
                classSQL.delete("DELETE FROM fakture_van WHERE broj_fakture='" + ttxBrojFakture.Text + "'");
                classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje cijele fakture_van br." + ttxBrojFakture.Text + "')");
                MessageBox.Show("Obrisano.");

                ttxBrojFakture.ReadOnly = false;
                nmGodinaFakture.ReadOnly = false;
                edit = false;
                EnableDisable(false);
                deleteFields();
                ControlDisableEnable(1, 0, 0, 1, 0);
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            btnSveFakture.Enabled = true;
            EnableDisable(false);
            deleteFields();
            ttxBrojFakture.Text = brojFakture();
            edit = false;
            ttxBrojFakture.ReadOnly = false;
            nmGodinaFakture.ReadOnly = false;
            btnObrisi.Enabled = true;
            chbOduzmiIzSkladista.Enabled = true;

            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private void EnableDisable(bool x)
        {
            cbVD.Enabled = x;
            txtSifraOdrediste.Enabled = x;
            txtSifraFakturirati.Enabled = x;
            txtPartnerNaziv.Enabled = x;
            txtPartnerNaziv1.Enabled = x;
            txtSifraNacinPlacanja.Enabled = x;
            txtModel.Enabled = x;
            txtSifraNarKupca.Enabled = x;
            txtSifraRadniNalog.Enabled = x;
            txtNarKupca1.Enabled = x;
            cbOtprema.Enabled = x;
            rtbNapomena.Enabled = x;
            txtSifra_robe.Enabled = x;
            btnPartner.Enabled = x;
            btnPartner1.Enabled = x;
            dtpDatum.Enabled = x;
            dtpDatumDVO.Enabled = x;
            dtpDanaValuta.Enabled = x;
            cbIzjava.Enabled = x;
            cbKomercijalist.Enabled = x;
            cbNacinPlacanja.Enabled = x;
            cbZiroRacun.Enabled = x;
            cbValuta.Enabled = x;
            cbRadniBalog.Enabled = x;
            cbNarKupca.Enabled = x;
            nuGodinaPredujma.Enabled = x;
            cbPredujam.Enabled = x;
            btnObrisi.Enabled = x;
            btnOpenRoba.Enabled = x;
            btnRadniNalog.Enabled = x;
            btnNarKupca.Enabled = x;
            btnPredujam.Enabled = x;
        }

        private void deleteFields()
        {
            txtSifraOdrediste.Text = "";
            txtSifraFakturirati.Text = "";
            txtPartnerNaziv.Text = "";
            txtPartnerNaziv1.Text = "";
            //txtSifraNacinPlacanja.Text = "";
            txtModel.Text = "";
            txtSifraNarKupca.Text = "";
            txtSifraRadniNalog.Text = "";
            txtNarKupca1.Text = "";
            rtbNapomena.Text = "";
            txtSifra_robe.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            dgw.Rows.Clear();
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            edit = false;
            EnableDisable(true);
            ttxBrojFakture.Text = brojFakture();
            ControlDisableEnable(0, 1, 1, 0, 1);
            ttxBrojFakture.ReadOnly = true;
            nmGodinaFakture.ReadOnly = true;
            txtSifraOdrediste.Select();
            //chbOduzmiIzSkladista.Enabled = true;
        }

        private string brojFakture()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj_fakture) FROM fakture_van", "fakture_van").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private DataTable VratiTablicuSKolonama()
        {
            DataTable DTsend = new DataTable();
            DTsend.Columns.Add("broj_fakture");
            DTsend.Columns.Add("sifra");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("mpc");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("nbc");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("oduzmi");
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("cijena");
            DTsend.Columns.Add("ime");
            DTsend.Columns.Add("porez_potrosnja");
            DTsend.Columns.Add("povratna_naknada");
            DTsend.Columns.Add("povratna_naknada_izn");
            DTsend.Columns.Add("rabat_izn");
            DTsend.Columns.Add("mpc_rabat");
            DTsend.Columns.Add("ukupno_rabat");
            DTsend.Columns.Add("ukupno_vpc");
            DTsend.Columns.Add("ukupno_mpc");
            DTsend.Columns.Add("ukupno_mpc_rabat");
            DTsend.Columns.Add("ukupno_porez");
            DTsend.Columns.Add("ukupno_osnovica");
            DTsend.Columns.Add("id_stavka");
            return DTsend;
        }

        private void btnSpremi_Click_1(object sender, EventArgs e)
        {
            if (chbOduzmiIzSkladista.Checked == false)
            {
                if (MessageBox.Show("Upozorenje ! Isključeno je skidanje robe iz skladišta ! \n\r Želite li nastaviti ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            DataTable DTsend = new DataTable();
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("nbc");
            DTsend.Columns.Add("broj_fakture");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("sifra");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("oduzmi");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("povratna_naknada");
            DataRow row;

            if (dgw.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku za spremiti.", "Greška");
                return;
            }

            decimal dec_parse;
            if (Decimal.TryParse(txtSifraOdrediste.Text, out dec_parse))
            {
                txtSifraOdrediste.Text = dec_parse.ToString();
            }
            else
            {
                MessageBox.Show("Greška kod upisa odredišta.", "Greška"); return;
            }

            if (Decimal.TryParse(txtSifraFakturirati.Text, out dec_parse))
            {
                txtSifraFakturirati.Text = dec_parse.ToString();
            }
            else
            {
                MessageBox.Show("Greška kod upisa šifre za fakturirati.", "Greška"); return;
            }

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                if (dgw.Rows[i].Cells["skladiste"].Value == null & dgw.Rows[i].Cells["oduzmi"].FormattedValue.ToString() == "DA")
                {
                    MessageBox.Show("Faktura nije spremljena zbog ne odabira skladišta na pojedinim artiklima.", "Greška");
                    return;
                }
            }

            if (edit)
            {
                DataTable DT = classSQL.select("SELECT broj_fakture,zki FROM fakture_van WHERE broj_fakture='" + ttxBrojFakture.Text + "'", "fakture_van").Tables[0];

                if (DT.Rows[0]["zki"].ToString().Length > 1)
                {
                    MessageBox.Show("Nije moguće mijenjati ovu fakturu! Fiskalizirana!");
                }
                else
                {
                    UpdateFaktura();
                    if (MessageBox.Show("Spremljeno.\r\nŽelite li ispisati dokument?", "Spremljeno.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        printaj(ttxBrojFakture.Text);
                    }
                }

                EnableDisable(false);
                deleteFields();
                btnSveFakture.Enabled = true;
                ControlDisableEnable(1, 0, 0, 1, 0);
                return;
            }

            string broj = brojFakture();
            if (broj.Trim() != ttxBrojFakture.Text.Trim())
            {
                MessageBox.Show("Vaš broj dokumenta već je netko iskoristio.\r\nDodijeljen Vam je novi broj '" + broj + "'.", "Info");
                ttxBrojFakture.Text = broj;
            }

            string uk = u.ToString();
            if (classSQL.remoteConnectionString == "")
            {
                uk = uk.Replace(",", ".");
            }
            else
            {
                uk = uk.Replace(".", ",");
            }

            string oduzmi_iz_skladista = "";
            if (chbOduzmiIzSkladista.Checked)
            {
                oduzmi_iz_skladista = "1";
            }
            else
            {
                oduzmi_iz_skladista = "0";
            }

            //--------STAVKE-------------
            DataTable DTsend1 = VratiTablicuSKolonama();

            double ukupnoMpcRabat, ukupnoVpc, ukupnoMpc, ukupnoPovratnaNaknada, ukupnoRabat, ukupnoPorez, ukupnoOsnovica;
            double kolicina, rabatIzn, rabat, mpc, porez, mpcRabat, vpc, povratnaNaknada;

            double povratnaNaknadaUkRacun = 0;
            double ukupnoMpcRabatRacun = 0;
            double ukupnoMpcRacun = 0;
            double ukupnoVpcRacun = 0;
            double ukupnoRabatRacun = 0;
            double ukupnoPorezRacun = 0;
            double ukupnoOsnovicaRacun = 0;

            string sifra = "";
            string kol = "";

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                sifra = dg(i, "sifra");

                row = DTsend1.NewRow();
                row["sifra"] = ReturnSifra(sifra);
                row["id_skladiste"] = dgw.Rows[i].Cells["skladiste"].Value;
                row["kolicina"] = dg(i, "kolicina");
                row["rabat"] = dg(i, "rabat");
                row["ime"] = dg(i, "naziv");
                row["oduzmi"] = dg(i, "oduzmi");
                row["porez_potrosnja"] = "0";
                row["broj_fakture"] = ttxBrojFakture.Text;

                kolicina = Convert.ToDouble(dg(i, "kolicina"));

                //POVRATNA NAKNADA
                //DTtemp = classSQL.select("SELECT iznos FROM povratna_naknada WHERE sifra='" + sifra + "'", "povratna_naknada")
                //    .Tables[0];

                //if (DTtemp.Rows.Count > 0)
                //{
                ukupnoPovratnaNaknada = Math.Round(Convert.ToDouble(dg(i, "povratna_naknada")), 2);
                povratnaNaknada = ukupnoPovratnaNaknada / kolicina;
                //}
                //else
                //{
                //    povratnaNaknada = 0;
                //    ukupnoPovratnaNaknada = 0;
                //}

                povratnaNaknadaUkRacun += ukupnoPovratnaNaknada;
                //POVRATNA NAKNADA

                rabat = Convert.ToDouble(dg(i, "rabat"));
                mpc = Convert.ToDouble(dg(i, "mpc"));
                vpc = Convert.ToDouble(dg(i, "vpc"));
                porez = Convert.ToDouble(dg(i, "porez"));

                ukupnoMpc = Math.Round(mpc * kolicina, 2);
                ukupnoMpcRacun += ukupnoMpc;

                ukupnoVpc = Math.Round(vpc * kolicina, 4);
                ukupnoVpcRacun += ukupnoVpc;

                ukupnoMpcRabat = Math.Round(Convert.ToDouble(dg(i, "iznos_ukupno")), 2);
                ukupnoMpcRabatRacun += ukupnoMpcRabat;
                ukupnoRabat = ukupnoMpc - ukupnoMpcRabat;
                mpcRabat = Math.Round(ukupnoMpcRabat / kolicina, 2);
                rabatIzn = mpc - mpcRabat;
                ukupnoRabatRacun += ukupnoRabat;

                ukupnoOsnovica = Math.Round((ukupnoMpcRabat - ukupnoPovratnaNaknada) / (1 + porez / 100), 2);
                ukupnoOsnovicaRacun += ukupnoOsnovica;
                ukupnoPorez = Math.Round(ukupnoMpcRabat - ukupnoPovratnaNaknada - ukupnoOsnovica, 2);
                ukupnoPorezRacun += ukupnoPorez;

                if (classSQL.remoteConnectionString == "")
                {
                    row["mpc"] = dg(i, "mpc").Replace(",", ".");
                    row["porez"] = dg(i, "porez").Replace(",", ".");
                    row["vpc"] = dg(i, "vpc").Replace(",", ".");
                    row["nbc"] = dg(i, "nc").Replace(",", ".");
                    //row["cijena"] = dg(i, "cijena").Replace(",", ".");
                    row["povratna_naknada_izn"] = povratnaNaknada.ToString("#0.00").Replace(",", ".");
                    row["povratna_naknada"] = ukupnoPovratnaNaknada.ToString("#0.00").Replace(",", ".");
                    row["rabat_izn"] = rabatIzn.ToString("#0.00").Replace(",", ".");
                    row["mpc_rabat"] = mpcRabat.ToString("#0.00").Replace(",", ".");
                    row["ukupno_rabat"] = ukupnoRabat.ToString("#0.00").Replace(",", ".");
                    row["ukupno_vpc"] = ukupnoVpc.ToString("#0.0000").Replace(",", ".");
                    row["ukupno_mpc"] = ukupnoMpc.ToString("#0.00").Replace(",", ".");
                    row["ukupno_mpc_rabat"] = ukupnoMpcRabat.ToString("#0.00").Replace(",", ".");
                    row["ukupno_porez"] = ukupnoPorez.ToString("#0.00").Replace(",", ".");
                    row["ukupno_osnovica"] = ukupnoOsnovica.ToString("#0.00").Replace(",", ".");
                }
                else
                {
                    row["mpc"] = dg(i, "mpc").Replace(".", ",");
                    row["porez"] = dg(i, "porez").Replace(".", ",");
                    row["vpc"] = dg(i, "vpc").Replace(".", ",");
                    row["nbc"] = dg(i, "nc").Replace(".", ",");
                    //row["cijena"] = dg(i, "cijena").Replace(".", ",");
                    row["povratna_naknada_izn"] = povratnaNaknada.ToString("#0.00").Replace(".", ",");
                    row["povratna_naknada"] = ukupnoPovratnaNaknada.ToString("#0.00").Replace(".", ",");
                    row["rabat_izn"] = rabatIzn.ToString("#0.00").Replace(".", ",");
                    row["mpc_rabat"] = mpcRabat.ToString("#0.00").Replace(".", ",");
                    row["ukupno_rabat"] = ukupnoRabat.ToString("#0.00").Replace(".", ",");
                    row["ukupno_vpc"] = ukupnoVpc.ToString("#0.0000").Replace(".", ",");
                    row["ukupno_mpc"] = ukupnoMpc.ToString("#0.00").Replace(".", ",");
                    row["ukupno_mpc_rabat"] = ukupnoMpcRabat.ToString("#0.00").Replace(".", ",");
                    row["ukupno_porez"] = ukupnoPorez.ToString("#0.00").Replace(".", ",");
                    row["ukupno_osnovica"] = ukupnoOsnovica.ToString("#0.00").Replace(".", ",");
                }

                DTsend1.Rows.Add(row);

                if (chbOduzmiIzSkladista.Checked)
                {
                    if (dg(i, "oduzmi") == "DA")
                    {
                        kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), dgw.Rows[i].Cells[3].Value.ToString(), dg(i, "kolicina"), "1", "-");
                        SQL.SQLroba_prodaja.UpdateRows(dgw.Rows[i].Cells[3].Value.ToString(), kol, dg(i, "sifra"));
                    }
                }

                sifra = dg(i, "sifra");

                //row = DTsend.NewRow();
                //row["kolicina"] = dg(i, "kolicina");
                //row["vpc"] = dg(i, "vpc");
                //row["nbc"] = dg(i, "nc");
                //row["broj_fakture"] = ttxBrojFakture.Text;
                //row["porez"] = dg(i, "porez");
                //row["sifra"] = ReturnSifra(dg(i, "sifra"));
                //row["rabat"] = dg(i, "rabat");
                //row["oduzmi"] = dg(i, "oduzmi");
                //row["id_skladiste"] = dgw.Rows[i].Cells[3].Value;
                //row["povratna_naknada"] = dg(i, "povratna_naknada");

                if (dg(i, "sifra").Length > 4)
                {
                    if (dg(i, "sifra").Substring(0, 3) == "000")
                    {
                        string sqlnext = "UPDATE racun_popust_kod_sljedece_kupnje SET koristeno='DA' WHERE broj_racuna='" + dg(i, "sifra").Substring(3, dg(i, "sifra").Length - 3) + "' AND dokumenat='FA'";
                        classSQL.update(sqlnext);
                    }
                }
            }

            string PovratnaNaknadaUkRacun, UkupnoMpcRacun, UkupnoVpcRacun,
                UkupnoMpcRabatRacun, UkupnoRabatRacun, UkupnoOsnovicaRacun, UkupnoPorezRacun;

            if (classSQL.remoteConnectionString == "")
            {
                PovratnaNaknadaUkRacun = povratnaNaknadaUkRacun.ToString("#0.00").Replace(",", ".");
                UkupnoMpcRacun = ukupnoMpcRacun.ToString("#0.00").Replace(",", ".");
                UkupnoVpcRacun = ukupnoVpcRacun.ToString("#0.0000").Replace(",", ".");
                UkupnoMpcRabatRacun = ukupnoMpcRabatRacun.ToString("#0.00").Replace(",", ".");
                UkupnoRabatRacun = ukupnoRabatRacun.ToString("#0.00").Replace(",", ".");
                UkupnoOsnovicaRacun = ukupnoOsnovicaRacun.ToString("#0.00").Replace(",", ".");
                UkupnoPorezRacun = ukupnoPorezRacun.ToString("#0.00").Replace(",", ".");
            }
            else
            {
                PovratnaNaknadaUkRacun = povratnaNaknadaUkRacun.ToString("#0.00").Replace(",", ".");
                UkupnoMpcRacun = ukupnoMpcRacun.ToString("#0.00").Replace(",", ".");
                UkupnoVpcRacun = ukupnoVpcRacun.ToString("#0.0000").Replace(",", ".");
                UkupnoMpcRabatRacun = ukupnoMpcRabatRacun.ToString("#0.00").Replace(",", ".");
                UkupnoRabatRacun = ukupnoRabatRacun.ToString("#0.00").Replace(",", ".");
                UkupnoOsnovicaRacun = ukupnoOsnovicaRacun.ToString("#0.00").Replace(",", ".");
                UkupnoPorezRacun = ukupnoPorezRacun.ToString("#0.00").Replace(",", ".");
            }
            if (txtTecaj.Text == "")
            {
                txtTecaj.Text = "1";
            }

            DateTime datum = DateTime.Now;

            string sql = "INSERT INTO fakture_van (broj_fakture,id_odrediste,id_fakturirati,date," +
                "dateDVO,datum_valute,id_izjava,id_zaposlenik,id_zaposlenik_izradio,model" +
                ",id_nacin_placanja,zr,id_valuta,otprema,id_predujam,napomena,id_vd,godina_predujma," +
                "godina_ponude,godina_fakture,oduzmi_iz_skladista,tecaj,ukupno,storno," +
                "ukupno_povratna_naknada,ukupno_mpc,ukupno_vpc,ukupno_mpc_rabat,ukupno_rabat,ukupno_osnovica,ukupno_porez)" +
                " VALUES " +
                " (" +
                 " '" + ttxBrojFakture.Text + "'," +
                " '" + txtSifraOdrediste.Text + "'," +
                " '" + txtSifraFakturirati.Text + "'," +
                " '" + datum + "'," +
                " '" + dtpDatumDVO.Value.ToString("yyyy-MM-dd") + "'," +
                " '" + dtpDanaValuta.Value.ToString("yyyy-MM-dd") + "'," +
                " '" + cbIzjava.SelectedValue + "'," +
                " '" + cbKomercijalist.SelectedValue.ToString() + "'," +
                " '" + Properties.Settings.Default.id_zaposlenik + "'," +
                " '" + txtModel.Text + "'," +
                " '" + cbNacinPlacanja.SelectedValue.ToString() + "'," +
                " '" + cbZiroRacun.SelectedValue.ToString() + "'," +
                " '" + cbValuta.SelectedValue.ToString() + "'," +
                " '" + cbOtprema.SelectedValue + "'," +
                " '" + "0" + "'," +
                " '" + rtbNapomena.Text + "'," +
                " '" + cbVD.SelectedValue.ToString().Trim() + "'," +
                " '" + nuGodinaPredujma.Value.ToString() + "'," +
                " '" + nuGodinaPonude.Value.ToString() + "'," +
                " '" + nmGodinaFakture.Value.ToString() + "'," +
                " '" + oduzmi_iz_skladista + "'," +
                " '" + txtTecaj.Text.Replace(",", ".") + "'," +
                " '" + uk + "'," +
                " 'NE'," +
                "'" + PovratnaNaknadaUkRacun + "'," +
                "'" + UkupnoMpcRacun + "'," +
                "'" + UkupnoVpcRacun + "'," +
                "'" + UkupnoMpcRabatRacun + "'," +
                "'" + UkupnoRabatRacun + "'," +
                "'" + UkupnoOsnovicaRacun + "'," +
                "'" + UkupnoPorezRacun + "'" +
                ")";
            provjera_sql(classSQL.insert(sql));

            if (MessageBox.Show("Fiskalizirati fakturu?", "Fiskalizacija?",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int brFak = Convert.ToInt32(ttxBrojFakture.Text);
                FiskalizirajFakturu(DTsend1, datum, brFak);
            }

            string barcode = "000" + ttxBrojFakture.Text;
            if (DTpromocije1.Rows[0]["aktivnost"].ToString() == "DA")
            {
                if (classSQL.remoteConnectionString == "")
                {
                    uk = uk.Replace(",", ".");
                }
                else
                {
                    uk = uk.Replace(".", ",");
                }

                provjera_sql(classSQL.insert("INSERT INTO racun_popust_kod_sljedece_kupnje (broj_racuna,datum,ukupno,popust,koristeno,dokumenat) VALUES (" +
                     "'" + ttxBrojFakture.Text + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                     "'" + Math.Round(SveUkupno, 2).ToString("#0.00") + "'," +
                     "'" + DTpromocije1.Rows[0]["popust"].ToString() + "'," +
                     "'NE'," +
                     "'FA'" +
                     ")"));
            }

            provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Nova faktura br." + ttxBrojFakture.Text + "')"));
            provjera_sql(SQL.SQLfakturaVan.InsertStavke(DTsend1));
            if (MessageBox.Show("Spremljeno.\r\nŽelite li ispisati dokument?", "Spremljeno.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                printaj(ttxBrojFakture.Text);
            }
            edit = false;
            EnableDisable(false);
            deleteFields();
            btnSveFakture.Enabled = true;
            ttxBrojFakture.ReadOnly = false;
            nmGodinaFakture.ReadOnly = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
            chbOduzmiIzSkladista.Enabled = true;
            btnObrisi.Enabled = true;
            if (popunjena_s_otpremnicom)
            {
                for (int i = 0; i < DTnaplaceneotpremnice.Rows.Count; i++)
                {
                    string pop_s_otpr = "Update otpremnica_stavke Set naplaceno_fakturom = '1' WHERE id_otpremnice = '" + DTnaplaceneotpremnice.Rows[i]["id_otpremnica"].ToString() + "' ";
                    classSQL.update(pop_s_otpr);
                }
            }
        }

        #region fiskalizacija helper

        private void FiskalizirajFakturu(DataTable DTsend, DateTime datum, int brojFakture)
        {
            DataSet DSkolicina = new DataSet();

            dodajKoloneDTpdv();
            DTpdv.Clear();

            double mnozeno = 1;
            double osnovicaStavka, pdvStavka;
            double osnovicaSve = 0;
            double pdvSve = 0;
            double povratnaNaknadaSve = 0;
            double rabatSve = 0;
            double kolNaknada;
            double povratnaNaknada;
            DataTable DTtemp;
            string sifra = "";
            string kol = "";

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                //ovo zakomentirano porez na potrošnju ne treba kod maloprodaje (?)
                double kolicina = Convert.ToDouble(dg(i, "kolicina").ToString());
                mnozeno = kolicina >= 0 ? 1 : -1;
                //double PP = Convert.ToDouble(DTstavke.Rows[i]["porez_potrosnja"].ToString());
                double PDV = Convert.ToDouble(dg(i, "porez").ToString());
                double VPC = Convert.ToDouble(dg(i, "vpc").ToString());
                double rabat = Convert.ToDouble(dg(i, "rabat").ToString());
                povratnaNaknada = Convert.ToDouble(dg(i, "povratna_naknada"));

                //double povratnaNaknada;
                ////mora biti tak jer prije nije postojala povratna naknada!
                //try
                //{
                //    povratnaNaknada = Convert.ToDouble(row["povratna_naknada"].ToString());
                //}
                //catch
                //{
                //    povratnaNaknada = 0;
                //}

                //double cijena = ((VPC * (PP + PDV) / 100) + VPC);
                double cijena = Math.Round(VPC * PDV / 100 + VPC - 0.0000001, 2);
                //double cijena = Math.Round(VPC * PDV / 100 + VPC, 2);
                double mpc = cijena * kolicina * (1 - rabat / 100);
                mpc = Convert.ToDouble(mpc.ToString("#0.00"));

                rabatSve += cijena * kolicina - mpc;

                //Ovaj kod dobiva PDV
                //double PreracunataStopaPDV = Convert.ToDouble((100 * PDV) / (100 + PDV + PP));
                double PreracunataStopaPDV = (100 * PDV) / (100 + PDV);

                //Ovaj kod dobiva porez na potrošnju
                //double PreracunataStopaPorezNaPotrosnju = Convert.ToDouble((100 * PP) / (100 + PDV + PP));
                double PreracunataStopaPorezNaPotrosnju = 100 / (100 + PDV);

                //treba smanjiti za iznos povratne naknade

                povratnaNaknada *= mnozeno;
                osnovicaStavka = (mpc - povratnaNaknada) / (1 + PDV / 100);
                pdvStavka = ((mpc - povratnaNaknada) * PreracunataStopaPDV) / 100;

                dodajPDV(PDV, osnovicaStavka);

                osnovicaSve += osnovicaStavka;

                pdvSve += pdvStavka;

                povratnaNaknadaSve += povratnaNaknada;
            }

            //--------------------------------------------------------
            //fiskalizacija

            DataTable DTOstaliPor = PosPrint.classPosPrintMaloprodaja2.dodajKoloneDTOstaliPor();
            DataTable DTnaknade = PosPrint.classPosPrintMaloprodaja2.dodajKoloneDTnaknade();

            if (povratnaNaknadaSve != 0)
            {
                RowPdv = DTnaknade.NewRow();
                RowPdv["iznos"] = povratnaNaknadaSve.ToString("0.00");
                RowPdv["naziv"] = "Povratna naknada";
                DTnaknade.Rows.Add(RowPdv);
            }

            double porezPotrosnjaSve = 0;

            string[] porezNaPotrosnju = setPorezNaPotrosnju();
            //porezNaPotrosnju[0] = DTpostavke.Rows[0]["porez_potrosnja"].ToString();
            porezNaPotrosnju[0] = Class.Postavke.porez_potrosnja.ToString();
            porezNaPotrosnju[1] = Convert.ToString(osnovicaSve);
            porezNaPotrosnju[2] = Convert.ToString(porezPotrosnjaSve);

            string iznososlobpdv = "";
            string iznos_marza = "";

            string np = Util.Korisno.VratiNacinPlacanja(cbNacinPlacanja.Text.ToLower());

            bool pdv = false;
            //if (DTpostavke.Rows[0]["sustav_pdv"].ToString() == "1")
            if (Class.Postavke.sustavPdv)
            {
                pdv = true;
            }

            string oib = DToib.Rows.Count > 0 ? DToib.Rows[0][0].ToString() : "";

            string[] fiskalizacija = new string[3];

            try
            {
                fiskalizacija = Fiskalizacija.classFiskalizacija.Fiskalizacija(
                    DTtvrtka.Rows[0]["oib"].ToString(),
                    oib,
                    datum,
                    pdv,
                    brojFakture,
                    DTpdv,
                    porezNaPotrosnju,
                    DTOstaliPor,
                    iznososlobpdv,
                    iznos_marza,
                    DTnaknade,
                    Convert.ToDecimal(u),//.ToString().Replace(",", ".")
                    np,
                    false,
                    "faktura"
                    );
            }
            catch
            {
                fiskalizacija = new string[3];
                fiskalizacija[0] = "";
                fiskalizacija[1] = "";
                fiskalizacija[2] = "";
            }

            //ažuriraj fakturu sa zki i jir
            string sql = "UPDATE fakture_van SET zki = '" + fiskalizacija[1] + "', jir='" + fiskalizacija[0] + "'" +
                " WHERE broj_fakture='" + brojFakture + "'";
            provjera_sql(classSQL.update(sql));
        }

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
            DataRow[] dataROW = DTpdv.Select("stopa = '" + stopa.ToString("0.00") + "'");

            if (dataROW.Count() == 0)
            {
                RowPdv = DTpdv.NewRow();
                RowPdv["stopa"] = stopa.ToString("0.00");
                RowPdv["iznos"] = (iznos * stopa / 100).ToString();
                RowPdv["osnovica"] = iznos.ToString();
                DTpdv.Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["iznos"] = Convert.ToDouble(dataROW[0]["iznos"].ToString()) + iznos * stopa / 100;
                dataROW[0]["osnovica"] = Convert.ToDouble(dataROW[0]["osnovica"].ToString()) + iznos;
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

        private void printaj(string broj)
        {
            Report.FakturaVan.repFaktura pr = new Report.FakturaVan.repFaktura();
            pr.dokumenat = "FAK";
            pr.racunajTecaj = ValutaKuna(cbValuta.Text);
            pr.broj_dokumenta = broj;
            pr.ImeForme = "Faktura";
            pr.ShowDialog();
        }

        private bool ValutaKuna(string valuta)
        {
            string val = valuta.ToLower();

            if (val.Contains("hr"))
                return false;
            else if (val.Contains("hrk"))
                return false;
            else if (val.Contains("hrvatska"))
            {
                return false;
            }
            else if (val.Contains("kun"))
            {
                return false;
            }
            else
                return true;
        }

        private string dg(int row, string cell)
        {
            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        private void btnOpenRoba_Click(object sender, EventArgs e)
        {
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            roba_trazi.ShowDialog();
            string propertis_sifra = Properties.Settings.Default.id_roba.Trim();
            if (propertis_sifra != "")
            {
                for (int y = 0; y < dgw.Rows.Count; y++)
                {
                    if (propertis_sifra == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString())
                    {
                        MessageBox.Show("Artikl ili usluga već postoje!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string sql = "SELECT * FROM roba WHERE sifra='" + propertis_sifra + "'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();

                    ttxBrojFakture.ReadOnly = true;
                    nmGodinaFakture.ReadOnly = true;
                    cbValuta.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cbNacinPlacanja_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();
        }

        private void provjera_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender.ToString().Length == 0) return;

            if (sender.ToString()[0] == '+')
            {
                if ('+' == (e.KeyChar)) return;
            }

            if (sender.ToString()[0] == '-')
            {
                if ('-' == (e.KeyChar)) return;
            }

            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            FakturaVan.frmSveFaktureVan objForm2 = new FakturaVan.frmSveFaktureVan();
            objForm2.sifra_fakture = "";
            objForm2.MainForm = this;
            objForm2.ShowDialog();
            if (broj_fakture_edit != null)
            {
                deleteFields();
                fillFaktute();
            }
        }

        private void fillFaktute()
        {
            //fill header

            EnableDisable(true);
            edit = true;
            ControlDisableEnable(0, 1, 1, 0, 1);

            DSfakture = classSQL.select("SELECT * FROM fakture_van WHERE broj_fakture = '" + broj_fakture_edit + "' ", "fakture_van").Tables[0];

            cbVD.SelectedValue = DSfakture.Rows[0]["id_vd"].ToString();
            txtTecaj.Text = DSfakture.Rows[0]["tecaj"].ToString();
            txtSifraOdrediste.Text = DSfakture.Rows[0]["id_odrediste"].ToString();
            txtSifraFakturirati.Text = DSfakture.Rows[0]["id_fakturirati"].ToString();
            txtPartnerNaziv.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DSfakture.Rows[0]["id_odrediste"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString();
            txtPartnerNaziv1.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DSfakture.Rows[0]["id_fakturirati"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString();
            txtSifraNacinPlacanja.Text = DSfakture.Rows[0]["id_nacin_placanja"].ToString();
            txtModel.Text = DSfakture.Rows[0]["model"].ToString();
            cbOtprema.SelectedValue = DSfakture.Rows[0]["otprema"].ToString();
            rtbNapomena.Text = DSfakture.Rows[0]["napomena"].ToString();
            dtpDatum.Value = Convert.ToDateTime(DSfakture.Rows[0]["date"].ToString());
            dtpDatumDVO.Value = Convert.ToDateTime(DSfakture.Rows[0]["dateDVO"].ToString());
            dtpDanaValuta.Value = Convert.ToDateTime(DSfakture.Rows[0]["datum_valute"].ToString());
            cbKomercijalist.SelectedValue = DSfakture.Rows[0]["id_zaposlenik"].ToString();
            cbNacinPlacanja.SelectedValue = DSfakture.Rows[0]["id_nacin_placanja"].ToString();
            cbZiroRacun.SelectedValue = DSfakture.Rows[0]["zr"].ToString();
            cbValuta.SelectedValue = DSfakture.Rows[0]["id_valuta"].ToString();
            nuGodinaPredujma.Value = Convert.ToInt16(DSfakture.Rows[0]["godina_predujma"].ToString());
            nuGodinaPonude.Value = Convert.ToInt16(DSfakture.Rows[0]["godina_ponude"].ToString());
            nmGodinaFakture.Value = Convert.ToInt16(DSfakture.Rows[0]["godina_fakture"].ToString());
            cbPredujam.SelectedValue = DSfakture.Rows[0]["id_predujam"].ToString();
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + DSfakture.Rows[0]["id_zaposlenik_izradio"].ToString() + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
            ttxBrojFakture.Text = DSfakture.Rows[0]["broj_fakture"].ToString();
            //MessageBox.Show(DSfakture.Rows[0]["id_odrediste"].ToString());

            if (DSfakture.Rows[0]["oduzmi_iz_skladista"].ToString() == "1")
            {
                chbOduzmiIzSkladista.Checked = true;
            }
            else
            {
                chbOduzmiIzSkladista.Checked = false;
            }
            chbOduzmiIzSkladista.Enabled = false;

            //--------fill faktura stavke------------------------------

            DataTable dtR = new DataTable();
            DSFS = classSQL.select("SELECT * FROM faktura_van_stavke WHERE broj_fakture = '" + DSfakture.Rows[0]["broj_fakture"].ToString() + "'", "broj_fakture").Tables[0];

            for (int i = 0; i < DSFS.Rows.Count; i++)
            {
                SQL.ClassSkladiste.provjeri_roba_prodaja(DSFS.Rows[i]["sifra"].ToString(), DSFS.Rows[i]["id_skladiste"].ToString());
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;
                string s = "";
                if (DSFS.Rows[i]["oduzmi"].ToString() == "DA")
                {
                    s = "SELECT roba_prodaja.nc,roba.naziv,roba.jm,roba_prodaja.sifra,roba.oduzmi FROM roba_prodaja INNER JOIN roba ON roba_prodaja.sifra=roba.sifra WHERE roba_prodaja.sifra='" + DSFS.Rows[i]["sifra"].ToString() + "' AND roba_prodaja.id_skladiste='" + DSFS.Rows[i]["id_skladiste"].ToString() + "'";
                }
                else
                {
                    s = "SELECT roba.nc,roba.naziv,roba.jm,roba.sifra,roba.oduzmi FROM roba WHERE sifra='" + DSFS.Rows[i]["sifra"].ToString() + "'";
                }

                dtR = classSQL.select(s, "roba_prodaja").Tables[0];

                if (dtR.Rows.Count == 0)
                {
                    if (DSFS.Rows[i]["sifra"].ToString().Length > 2)
                    {
                        DSFS.Rows[i]["sifra"] = DSFS.Rows[i]["sifra"].ToString().Remove(0, 3);

                        dtR = classSQL.select("SELECT * FROM fakture_van WHERE broj_fakture='" + DSFS.Rows[i]["sifra"].ToString() + "'", "broj_fakture").Tables[0];
                        if (dtR.Rows.Count > 0)
                        {
                            dgw.Rows[br].Cells[0].Value = i + 1;
                            dgw.Rows[br].Cells["sifra"].Value = "000" + DSFS.Rows[i]["sifra"].ToString();
                            dgw.Rows[br].Cells["naziv"].Value = "POPUST";
                            dgw.Rows[br].Cells["jmj"].Value = "kom";
                            dgw.Rows[br].Cells["oduzmi"].Value = "NE";
                            dgw.Rows[br].Cells["cijena_bez_pdva"].Value = string.Format("{0:0.000}", DSFS.Rows[i]["vpc"]);
                            dgw.Rows[br].Cells["kolicina"].Value = DSFS.Rows[i]["kolicina"].ToString();
                            dgw.Rows[br].Cells["porez"].Value = DSFS.Rows[i]["porez"].ToString();
                            dgw.Rows[br].Cells["rabat"].Value = DSFS.Rows[i]["rabat"].ToString();
                            dgw.Rows[br].Cells["skladiste"].Value = DSFS.Rows[i]["id_skladiste"].ToString();
                            dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
                            dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
                            dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
                            dgw.Rows[br].Cells["vpc"].Value = Convert.ToDecimal(DSFS.Rows[i]["vpc"].ToString()).ToString("#0.000");
                            //dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["mpc"]);
                            dgw.Rows[br].Cells["nc"].Value = string.Format("{0:0.000}", DSFS.Rows[i]["vpc"]);
                            dgw.Rows[br].Cells["id_stavka"].Value = DSFS.Rows[i]["id_stavka"].ToString();
                            //gw.Rows[br].Cells["porez_potrosnja"].Value = DSFS.Rows[i]["porez_potrosnja"].ToString();

                            dgw.Select();
                            dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[3];

                            //povratna naknada mora biti poslije izračuna jer bi se inače upisivala trenutna povratna naknada
                            //a ne povratna naknada upisana u fakturi
                            dgw.Rows[br].Cells["povratna_naknada"].Value = DSFS.Rows[i]["povratna_naknada"].ToString();
                            //txtPovrNaknada.Text = DSFS.Rows[i]["povratna_naknada"].ToString() == "" ? "0,00" : Math.Round(Convert.ToDouble(DSFS.Rows[i]["povratna_naknada"].ToString()), 2).ToString("#0.00");

                            izracun(false);
                        }
                    }
                }
                else if (dtR.Rows.Count > 0)
                {
                    dgw.Rows[br].Cells[0].Value = i + 1;
                    dgw.Rows[br].Cells["sifra"].Value = dtR.Rows[0]["sifra"].ToString();
                    dgw.Rows[br].Cells["naziv"].Value = dtR.Rows[0]["naziv"].ToString();
                    dgw.Rows[br].Cells["jmj"].Value = dtR.Rows[0]["jm"].ToString();
                    dgw.Rows[br].Cells["oduzmi"].Value = dtR.Rows[0]["oduzmi"].ToString();
                    dgw.Rows[br].Cells["cijena_bez_pdva"].Value = string.Format("{0:0.000}", DSFS.Rows[i]["vpc"]);
                    dgw.Rows[br].Cells["kolicina"].Value = DSFS.Rows[i]["kolicina"].ToString();
                    dgw.Rows[br].Cells["porez"].Value = DSFS.Rows[i]["porez"].ToString();
                    dgw.Rows[br].Cells["rabat"].Value = DSFS.Rows[i]["rabat"].ToString();
                    dgw.Rows[br].Cells["skladiste"].Value = DSFS.Rows[i]["id_skladiste"].ToString();
                    dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
                    dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
                    dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
                    dgw.Rows[br].Cells["vpc"].Value = string.Format("{0:0.000}", DSFS.Rows[i]["vpc"]);
                    //dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["mpc"]);
                    dgw.Rows[br].Cells["nc"].Value = string.Format("{0:0.00}", dtR.Rows[0]["nc"].ToString());
                    dgw.Rows[br].Cells["id_stavka"].Value = DSFS.Rows[i]["id_stavka"].ToString();
                    dgw.Rows[br].Cells["povratna_naknada"].Value = DSFS.Rows[i]["povratna_naknada"].ToString();
                    //dgw.Rows[br].Cells["porez_potrosnja"].Value = DSFS.Rows[i]["porez_potrosnja"].ToString();

                    dgw.Select();
                    dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[3];

                    //povratna naknada mora biti poslije izračuna jer bi se inače upisivala trenutna povratna naknada
                    //a ne povratna naknada upisana u fakturi
                    dgw.Rows[br].Cells["povratna_naknada"].Value = DSFS.Rows[i]["povratna_naknada"].ToString();
                    //txtPovrNaknada.Text = DSFS.Rows[i]["povratna_naknada"].ToString() == "" ? "0,00" : Math.Round(Convert.ToDouble(DSFS.Rows[i]["povratna_naknada"].ToString()), 2).ToString("#0.00");

                    izracun(false);
                }
            }

            dgw.Columns["cijena_bez_pdva"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["rabat_iznos"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos_bez_pdva"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos_ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgw.Columns["cijena_bez_pdva"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["rabat_iznos"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos_bez_pdva"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos_ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //DataGridViewCellStyle style = new DataGridViewCellStyle();
            //style.Format = "N2";
            //dgw.Columns["cijena_bez_pdva"].DefaultCellStyle = style;
            //dgw.Columns["rabat_iznos"].DefaultCellStyle = style;
            //dgw.Columns["iznos_bez_pdva"].DefaultCellStyle = style;
            //dgw.Columns["iznos_ukupno"].DefaultCellStyle = style;
        }

        private void UpdateFaktura()
        {
            DataRow row;
            DataRow row1;

            //--------STAVKE-------------
            DataTable DTsend1 = VratiTablicuSKolonama();
            DataTable DTsend = VratiTablicuSKolonama();

            double ukupnoMpcRabat, ukupnoVpc, ukupnoMpc, ukupnoPovratnaNaknada, ukupnoRabat, ukupnoPorez, ukupnoOsnovica;
            double kolicina, rabatIzn, rabat, mpc, porez, mpcRabat, vpc, povratnaNaknada;

            double povratnaNaknadaUkRacun = 0;
            double ukupnoMpcRabatRacun = 0;
            double ukupnoMpcRacun = 0;
            double ukupnoVpcRacun = 0;
            double ukupnoRabatRacun = 0;
            double ukupnoPorezRacun = 0;
            double ukupnoOsnovicaRacun = 0;

            string sifra = "";
            string kol = "";

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                sifra = dg(i, "sifra");

                kolicina = Convert.ToDouble(dg(i, "kolicina"));

                ukupnoPovratnaNaknada = Math.Round(Convert.ToDouble(dg(i, "povratna_naknada")), 2);
                povratnaNaknada = ukupnoPovratnaNaknada / kolicina;

                povratnaNaknadaUkRacun += ukupnoPovratnaNaknada;

                rabat = Convert.ToDouble(dg(i, "rabat"));
                mpc = Convert.ToDouble(dg(i, "mpc"));
                vpc = Convert.ToDouble(dg(i, "vpc"));
                porez = Convert.ToDouble(dg(i, "porez"));

                ukupnoMpc = Math.Round(mpc * kolicina, 2);
                ukupnoMpcRacun += ukupnoMpc;

                ukupnoVpc = Math.Round(vpc * kolicina, 4);
                ukupnoVpcRacun += ukupnoVpc;

                ukupnoMpcRabat = Math.Round(Convert.ToDouble(dg(i, "iznos_ukupno")), 2);
                ukupnoMpcRabatRacun += ukupnoMpcRabat;
                ukupnoRabat = ukupnoMpc - ukupnoMpcRabat;
                mpcRabat = Math.Round(ukupnoMpcRabat / kolicina, 2);
                rabatIzn = mpc - mpcRabat;
                ukupnoRabatRacun += ukupnoRabat;

                ukupnoOsnovica = Math.Round((ukupnoMpcRabat - ukupnoPovratnaNaknada) / (1 + porez / 100), 2);
                ukupnoOsnovicaRacun += ukupnoOsnovica;
                ukupnoPorez = Math.Round(ukupnoMpcRabat - ukupnoPovratnaNaknada - ukupnoOsnovica, 2);
                ukupnoPorezRacun += ukupnoPorez;

                if (dgw.Rows[i].Cells["id_stavka"].Value != null)
                {
                    DataRow[] dataROW = DSFS.Select("id_stavka = " + dgw.Rows[i].Cells["id_stavka"].Value.ToString());

                    if (chbOduzmiIzSkladista.Checked)
                    {
                        if (dg(i, "oduzmi") == "DA")
                        {
                            if (dgw.Rows[i].Cells[3].Value.ToString() == dataROW[0]["id_skladiste"].ToString())
                            {
                                kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), dgw.Rows[i].Cells[3].Value.ToString(), (Convert.ToDouble(dataROW[0]["kolicina"].ToString()) - Convert.ToDouble(dg(i, "kolicina"))).ToString(), "1", "+");
                                SQL.SQLroba_prodaja.UpdateRows(dgw.Rows[i].Cells[3].Value.ToString(), kol, dg(i, "sifra"));
                            }
                            else
                            {
                                //vraća na staro skladiste
                                kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), dataROW[0]["id_skladiste"].ToString(), dataROW[0]["kolicina"].ToString(), "1", "+");
                                SQL.SQLroba_prodaja.UpdateRows(dataROW[0]["id_skladiste"].ToString(), kol, dg(i, "sifra"));

                                //oduzima sa novog skladiste
                                kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), dgw.Rows[i].Cells[3].Value.ToString(), dg(i, "kolicina"), "1", "-");
                                SQL.SQLroba_prodaja.UpdateRows(dgw.Rows[i].Cells[3].Value.ToString(), kol, dg(i, "sifra"));
                            }
                        }
                    }

                    row1 = DTsend1.NewRow();
                    row1["kolicina"] = dg(i, "kolicina");
                    row1["vpc"] = dg(i, "vpc");
                    row1["nbc"] = dg(i, "nc");
                    row1["broj_fakture"] = ttxBrojFakture.Text;
                    row1["porez"] = dg(i, "porez");
                    row1["sifra"] = ReturnSifra(dg(i, "sifra"));
                    row1["rabat"] = dg(i, "rabat");
                    row1["id_stavka"] = dg(i, "id_stavka");
                    row1["oduzmi"] = dg(i, "oduzmi");
                    row1["id_skladiste"] = dgw.Rows[i].Cells[3].Value;
                    row1["povratna_naknada"] = dg(i, "povratna_naknada");
                    row1["povratna_naknada_izn"] = povratnaNaknada.ToString("#0.00").Replace(".", ",");
                    row1["rabat_izn"] = rabatIzn.ToString("#0.00").Replace(".", ",");
                    row1["mpc_rabat"] = mpcRabat.ToString("#0.00").Replace(".", ",");
                    row1["ukupno_rabat"] = ukupnoRabat.ToString("#0.00").Replace(".", ",");
                    row1["ukupno_vpc"] = ukupnoVpc.ToString("#0.0000").Replace(".", ",");
                    row1["ukupno_mpc"] = ukupnoMpc.ToString("#0.00").Replace(".", ",");
                    row1["ukupno_mpc_rabat"] = ukupnoMpcRabat.ToString("#0.00").Replace(".", ",");
                    row1["ukupno_porez"] = ukupnoPorez.ToString("#0.00").Replace(".", ",");
                    row1["ukupno_osnovica"] = ukupnoOsnovica.ToString("#0.00").Replace(".", ",");
                    DTsend1.Rows.Add(row1);
                }
                else
                {
                    if (chbOduzmiIzSkladista.Checked)
                    {
                        if (dg(i, "oduzmi") == "DA")
                        {
                            kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), dgw.Rows[i].Cells[3].Value.ToString(), dg(i, "kolicina"), "1", "-");
                            SQL.SQLroba_prodaja.UpdateRows(dgw.Rows[i].Cells[3].Value.ToString(), kol, dg(i, "sifra"));
                        }
                    }

                    row = DTsend.NewRow();
                    row["kolicina"] = dg(i, "kolicina");
                    row["vpc"] = dg(i, "vpc");
                    row["nbc"] = dg(i, "nc");
                    row["broj_fakture"] = ttxBrojFakture.Text;
                    row["porez"] = dg(i, "porez");
                    row["sifra"] = ReturnSifra(dg(i, "sifra"));
                    row["rabat"] = dg(i, "rabat");
                    ////nova stavka pa ne treba
                    //row["id_stavka"] = dg(i, "id_stavka");
                    row["oduzmi"] = dg(i, "oduzmi");
                    row["id_skladiste"] = dgw.Rows[i].Cells[3].Value;
                    row["povratna_naknada"] = dg(i, "povratna_naknada");
                    row["povratna_naknada_izn"] = povratnaNaknada.ToString("#0.00").Replace(".", ",");
                    row["rabat_izn"] = rabatIzn.ToString("#0.00").Replace(".", ",");
                    row["mpc_rabat"] = mpcRabat.ToString("#0.00").Replace(".", ",");
                    row["ukupno_rabat"] = ukupnoRabat.ToString("#0.00").Replace(".", ",");
                    row["ukupno_vpc"] = ukupnoVpc.ToString("#0.0000").Replace(".", ",");
                    row["ukupno_mpc"] = ukupnoMpc.ToString("#0.00").Replace(".", ",");
                    row["ukupno_mpc_rabat"] = ukupnoMpcRabat.ToString("#0.00").Replace(".", ",");
                    row["ukupno_porez"] = ukupnoPorez.ToString("#0.00").Replace(".", ",");
                    row["ukupno_osnovica"] = ukupnoOsnovica.ToString("#0.00").Replace(".", ",");
                    DTsend.Rows.Add(row);
                }
            }

            string PovratnaNaknadaUkRacun, UkupnoMpcRacun, UkupnoVpcRacun,
                UkupnoMpcRabatRacun, UkupnoRabatRacun, UkupnoOsnovicaRacun, UkupnoPorezRacun;

            if (classSQL.remoteConnectionString == "")
            {
                PovratnaNaknadaUkRacun = povratnaNaknadaUkRacun.ToString("#0.00").Replace(",", ".");
                UkupnoMpcRacun = ukupnoMpcRacun.ToString("#0.00").Replace(",", ".");
                UkupnoVpcRacun = ukupnoVpcRacun.ToString("#0.0000").Replace(",", ".");
                UkupnoMpcRabatRacun = ukupnoMpcRabatRacun.ToString("#0.00").Replace(",", ".");
                UkupnoRabatRacun = ukupnoRabatRacun.ToString("#0.00").Replace(",", ".");
                UkupnoOsnovicaRacun = ukupnoOsnovicaRacun.ToString("#0.00").Replace(",", ".");
                UkupnoPorezRacun = ukupnoPorezRacun.ToString("#0.00").Replace(",", ".");
            }
            else
            {
                PovratnaNaknadaUkRacun = povratnaNaknadaUkRacun.ToString("#0.00").Replace(",", ".");
                UkupnoMpcRacun = ukupnoMpcRacun.ToString("#0.00").Replace(",", ".");
                UkupnoVpcRacun = ukupnoVpcRacun.ToString("#0.0000").Replace(",", ".");
                UkupnoMpcRabatRacun = ukupnoMpcRabatRacun.ToString("#0.00").Replace(",", ".");
                UkupnoRabatRacun = ukupnoRabatRacun.ToString("#0.00").Replace(",", ".");
                UkupnoOsnovicaRacun = ukupnoOsnovicaRacun.ToString("#0.00").Replace(",", ".");
                UkupnoPorezRacun = ukupnoPorezRacun.ToString("#0.00").Replace(",", ".");
            }

            string uk = u.ToString();
            if (classSQL.remoteConnectionString == "")
            {
                uk = uk.Replace(",", ".");
            }
            else
            {
                uk = uk.Replace(".", ",");
            }

            DateTime datum = dtpDatum.Value;

            string sql = "UPDATE fakture_van SET" +
                " id_odrediste= '" + txtSifraOdrediste.Text + "'," +
                " id_fakturirati='" + txtSifraFakturirati.Text + "'," +
                " date='" + datum.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " dateDVO='" + dtpDatumDVO.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " datum_valute='" + dtpDanaValuta.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " id_izjava='" + cbIzjava.SelectedValue + "'," +
                " id_zaposlenik='" + cbKomercijalist.SelectedValue + "'," +
                " id_zaposlenik_izradio='" + Properties.Settings.Default.id_zaposlenik + "'," +
                " model= '" + txtModel.Text + "'," +
                " id_nacin_placanja='" + cbNacinPlacanja.SelectedValue + "'," +
                " zr='" + cbZiroRacun.SelectedValue + "'," +
                " id_valuta='" + cbValuta.SelectedValue + "'," +
                " otprema='" + cbOtprema.SelectedValue + "'," +
                " id_predujam='0'," +
                " napomena='" + rtbNapomena.Text + "'," +
                " id_vd='" + cbVD.SelectedValue.ToString() + "'," +
                " godina_predujma='" + nuGodinaPredujma.Value.ToString() + "'," +
                " godina_ponude='" + nuGodinaPonude.Value.ToString() + "'," +
                " godina_fakture='" + nmGodinaFakture.Value.ToString() + "'," +
                " ukupno='" + uk + "'," +
                " ukupno_rabat='" + UkupnoRabatRacun + "'," +
                " ukupno_porez='" + UkupnoPorezRacun + "'," +
                " ukupno_osnovica='" + UkupnoOsnovicaRacun + "'," +
                " ukupno_mpc='" + UkupnoMpcRacun + "'," +
                " ukupno_vpc='" + UkupnoVpcRacun + "'," +
                " ukupno_mpc_rabat='" + UkupnoMpcRabatRacun + "'," +
                " ukupno_povratna_naknada='" + PovratnaNaknadaUkRacun + "'" +
                " WHERE  broj_fakture='" + ttxBrojFakture.Text + "'";
            provjera_sql(classSQL.update(sql));

            DataSet DSkolicina = new DataSet();

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                string barcode = "000" + ttxBrojFakture.Text;
                if (DTpromocije1.Rows[0]["aktivnost"].ToString() == "DA")
                {
                    if (classSQL.remoteConnectionString == "")
                    {
                        uk = uk.Replace(",", ".");
                    }
                    else
                    {
                        uk = uk.Replace(".", ",");
                    }

                    provjera_sql(classSQL.update("UPDATE racun_popust_kod_sljedece_kupnje SET " +
                        "datum='" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        "ukupno='" + Math.Round(SveUkupno, 2).ToString("#0.00") + "'," +
                        "popust='" + DTpromocije1.Rows[0]["popust"].ToString() + "'," +
                        "koristeno='NE'" +
                        " WHERE dokumenat='FA' AND broj_racuna='" + ttxBrojFakture.Text + "'"));
                }
            }

            provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Nova faktura br." + ttxBrojFakture.Text + "')"));
            provjera_sql(SQL.SQLfaktura.InsertStavke(DTsend, false));
            provjera_sql(SQL.SQLfaktura.UpdateStavke(DTsend1, false));

            DTsend.Merge(DTsend1, true);
            if (MessageBox.Show("Fiskalizirati fakturu?", "Fiskalizacija?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int brFak = Convert.ToInt32(ttxBrojFakture.Text);
                FiskalizirajFakturu(DTsend1, datum, brFak);
            }

            ttxBrojFakture.ReadOnly = false;
            nmGodinaFakture.ReadOnly = false;
            edit = false;
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmSvePonude sp = new frmSvePonude();
            sp.btnUrediSifru.Enabled = false;
            sp.ShowDialog();
        }

        private void SetRoba()
        {
            dgw.Rows.Add();
            int br = dgw.Rows.Count - 1;
            dgw.Select();

            dgw.Rows[br].Cells[0].Value = "1";
            dgw.Rows[br].Cells["sifra"].Value = DTRoba.Rows[0]["sifra"].ToString();
            dgw.Rows[br].Cells["naziv"].Value = DTRoba.Rows[0]["naziv"].ToString();
            dgw.Rows[br].Cells["jmj"].Value = DTRoba.Rows[0]["jm"].ToString();
            dgw.Rows[br].Cells["cijena_bez_pdva"].Value = string.Format("{0:0.000}", DTRoba.Rows[0]["vpc"]);
            dgw.Rows[br].Cells["kolicina"].Value = "1";

            if (chbObracunPoreza.Checked)
            {
                dgw.Rows[br].Cells["porez"].Value = DTRoba.Rows[0]["porez"].ToString();
            }
            else
            {
                dgw.Rows[br].Cells["porez"].Value = 0;
            }

            dgw.Rows[br].Cells["oduzmi"].Value = DTRoba.Rows[0]["oduzmi"].ToString();
            //dgw.Rows[br].Cells["skladiste"].Value = DTRoba.Rows[0]["id_skladiste"].ToString();
            dgw.Rows[br].Cells["rabat"].Value = "0,00";
            dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
            dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
            dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
            dgw.Rows[br].Cells["vpc"].Value = string.Format("{0:0.000}", DTRoba.Rows[0]["vpc"]);
            try
            {
                dgw.Rows[br].Cells["mpc"].Value = string.Format("{0:0.00}", DTRoba.Rows[0]["mpc"]);
            }
            catch
            {
                double rabat = Convert.ToDouble(DTRoba.Rows[0]["porez"].ToString());
                double vp = Convert.ToDouble(DTRoba.Rows[0]["vpc"].ToString());
                double mpc = vp + (vp * rabat / 100);
                dgw.Rows[br].Cells["mpc"].Value = string.Format("{0:0.00}", mpc);
            }
            dgw.Rows[br].Cells["nc"].Value = string.Format("{0:0.000}", DTRoba.Rows[0]["nc"]);
            dgw.Rows[br].Cells["skladiste"].Value = Class.Postavke.id_default_skladiste;

            dgw.CurrentCell = dgw.Rows[br].Cells[3];
            dgw.BeginEdit(true);
            izracun(true);
        }

        private void ttxBrojFakture_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT broj_fakture,zki FROM fakture_van WHERE godina_fakture='" + nmGodinaFakture.Value.ToString() + "' AND broj_fakture='" + ttxBrojFakture.Text + "'", "fakture_van").Tables[0];
                deleteFields();
                if (DT.Rows.Count == 0)
                {
                    if (brojFakture() == ttxBrojFakture.Text.Trim())
                    {
                        deleteFields();
                        edit = false;
                        EnableDisable(true);
                        btnSveFakture.Enabled = false;
                        //ttxBrojFakture.Text = brojFakture();
                        btnDeleteAllFaktura.Enabled = false;
                        txtSifraOdrediste.Select();
                        ttxBrojFakture.ReadOnly = true;
                        nmGodinaFakture.ReadOnly = true;
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    if (DT.Rows[0]["zki"].ToString().Length > 1)
                    {
                        MessageBox.Show("Nije moguće mijenjati ovu fakturu! Fiskalizirana!");
                        ControlDisableEnable(1, 0, 0, 1, 0);
                        return;
                    }

                    broj_fakture_edit = ttxBrojFakture.Text;
                    fillFaktute();
                    EnableDisable(true);
                    edit = true;
                    btnDeleteAllFaktura.Enabled = true;
                    txtSifraOdrediste.Select();
                    ttxBrojFakture.ReadOnly = true;
                    nmGodinaFakture.ReadOnly = true;
                }

                ControlDisableEnable(0, 1, 1, 0, 1);
            }
        }

        private void txtBrojPonude_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT broj_ponude FROM ponude WHERE godina_ponude='" + nuGodinaPonude.Value.ToString() + "' AND broj_ponude='" + txtBrojPonude.Text + "'", "ponude").Tables[0];
                deleteFields();
                if (DT.Rows.Count == 0)
                {
                    MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                }
                else if (DT.Rows.Count == 1)
                {
                    EnableDisable(true);
                    edit = true;
                    btnDeleteAllFaktura.Enabled = true;
                    fillPonude(DT.Rows[0][0].ToString());
                }
            }
        }

        private void fillPonude(string broj)
        {
            //fill header
            DataTable DTotpremnice = classSQL.select("SELECT * FROM ponude WHERE broj_ponude = '" + broj + "'", "ponude").Tables[0];

            txtSifraOdrediste.Text = DTotpremnice.Rows[0]["id_odrediste"].ToString();
            try { txtPartnerNaziv.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["id_odrediste"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); }
            catch (Exception) { }
            txtSifraFakturirati.Text = DTotpremnice.Rows[0]["id_fakturirati"].ToString();
            try { txtPartnerNaziv1.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["id_fakturirati"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); }
            catch (Exception) { }
            cbZiroRacun.SelectedValue = DTotpremnice.Rows[0]["zr"].ToString();
            cbNacinPlacanja.SelectedValue = DTotpremnice.Rows[0]["id_nacin_placanja"].ToString();
            cbValuta.SelectedValue = DTotpremnice.Rows[0]["id_valuta"].ToString();
            cbOtprema.SelectedValue = DTotpremnice.Rows[0]["otprema"].ToString();

            //--------fill otpremnica stavke----------------------------
            DataTable dtR = new DataTable();
            DSFS = classSQL.select("SELECT * FROM ponude_stavke WHERE broj_ponude = '" + DTotpremnice.Rows[0]["broj_ponude"].ToString() + "'", "ponude_stavke").Tables[0];

            for (int i = 0; i < DSFS.Rows.Count; i++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;
                string s = "";

                DataTable tblRoba = classSQL.select("SELECT oduzmi FROM roba WHERE sifra='" + DSFS.Rows[i]["sifra"].ToString().Trim() + "'", "roba").Tables[0];

                if (tblRoba.Rows[0]["oduzmi"].ToString() == "DA")
                {
                    s = "SELECT roba_prodaja.nc,roba.naziv,roba.jm,roba_prodaja.sifra,roba.oduzmi FROM roba_prodaja INNER JOIN roba ON roba_prodaja.sifra=roba.sifra WHERE roba_prodaja.sifra='" + DSFS.Rows[i]["sifra"].ToString() + "' AND roba_prodaja.id_skladiste='" + DSFS.Rows[i]["id_skladiste"].ToString() + "'";
                }
                else
                {
                    s = "SELECT roba.nc,roba.naziv,roba.jm,roba.sifra,roba.oduzmi FROM roba WHERE sifra='" + DSFS.Rows[i]["sifra"].ToString() + "'";
                }

                dtR = classSQL.select(s, "roba_prodaja").Tables[0];

                dgw.Rows[br].Cells[0].Value = i + 1;
                dgw.Rows[br].Cells["sifra"].Value = dtR.Rows[0]["sifra"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = dtR.Rows[0]["naziv"].ToString();
                dgw.Rows[br].Cells["jmj"].Value = dtR.Rows[0]["jm"].ToString();
                dgw.Rows[br].Cells["oduzmi"].Value = dtR.Rows[0]["oduzmi"].ToString();
                dgw.Rows[br].Cells["cijena_bez_pdva"].Value = string.Format("{0:0.000}", DSFS.Rows[i]["vpc"]);
                dgw.Rows[br].Cells["kolicina"].Value = DSFS.Rows[i]["kolicina"].ToString();
                dgw.Rows[br].Cells["porez"].Value = DSFS.Rows[i]["porez"].ToString();
                dgw.Rows[br].Cells["rabat"].Value = DSFS.Rows[i]["rabat"].ToString();
                dgw.Rows[br].Cells["skladiste"].Value = DSFS.Rows[i]["id_skladiste"].ToString();
                dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
                dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
                dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
                dgw.Rows[br].Cells["vpc"].Value = string.Format("{0:0.000}", DSFS.Rows[i]["vpc"]);
                //dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["mpc"]);
                dgw.Rows[br].Cells["nc"].Value = string.Format("{0:0.00}", dtR.Rows[0]["nc"].ToString());
                dgw.Rows[br].Cells["id_stavka"].Value = DSFS.Rows[i]["id_stavka"].ToString();

                dgw.Select();
                dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[3];
                ControlDisableEnable(0, 1, 1, 0, 1);
                edit = false;

                //povratna naknada mora biti poslije izračuna jer bi se inače upisivala trenutna povratna naknada
                //a ne povratna naknada upisana u fakturi
                try
                {
                    dgw.Rows[br].Cells["povratna_naknada"].Value = DSFS.Rows[i]["povratna_naknada"].ToString();
                }
                catch
                {
                    //uzeti iz baze
                    DataTable DTpovrNaknada = classSQL.select("SELECT iznos FROM povratna_naknada WHERE sifra='" + DSFS.Rows[i]["sifra"].ToString().Trim() + "'", "roba").Tables[0];
                    dgw.Rows[br].Cells["povratna_naknada"].Value = DTpovrNaknada.Rows.Count > 0 ? DTpovrNaknada.Rows[0]["iznos"].ToString() : "0,00";
                }

                izracun(false);
            }
        }

        private DataTable DTnaplaceneotpremnice = new DataTable();

        private void fillOtpremnice(string broj)
        {
            //fill header

            //--------fill otpremnica stavke----------------------------
            DataTable DTotpremnice = new DataTable();
            DataTable DTotpremnica_stavke = new DataTable();
            DataTable DTskl = classSQL.select("SELECT * FROM skladiste ", "skl").Tables[0];
            if (DTnaplaceneotpremnice.Columns.Contains("id") != true)
            {
                DTnaplaceneotpremnice.Columns.Add("id");
                DTnaplaceneotpremnice.Columns.Add("id_otpremnica");
            }

            DataRow row;
            DataTable dtR = new DataTable();
            //DSFS = classSQL.select("SELECT * FROM otpremnica_stavke WHERE broj_ponude = '" + DTotpremnice.Rows[0]["broj_ponude"].ToString() + "'", "ponude_stavke").Tables[0];
            bool header_1x = true;
            for (int i = 0; i < DTskl.Rows.Count; i++)
            {
                DTotpremnice = classSQL.select("SELECT * FROM otpremnice  WHERE osoba_partner = '" + broj + "' AND id_skladiste = '" + DTskl.Rows[i]["id_skladiste"].ToString() + "' ", "otpremnice").Tables[0];

                if (DTotpremnice.Rows.Count != 0)
                {
                    for (int z = 0; z < DTotpremnice.Rows.Count; z++)
                    {
                        row = DTnaplaceneotpremnice.NewRow();
                        row["id"] = DTnaplaceneotpremnice.Rows.Count.ToString();
                        row["id_otpremnica"] = DTotpremnice.Rows[z]["id_otpremnica"];
                        DTnaplaceneotpremnice.Rows.Add(row);

                        //dtR = classSQL.select(s, "roba_prodaja").Tables[0];
                        try
                        {
                            DTotpremnica_stavke = classSQL.select("SELECT * FROM otpremnica_stavke  WHERE id_otpremnice = '" + DTotpremnice.Rows[z]["id_otpremnica"].ToString() + "' AND naplaceno_fakturom <> 'TRUE'", "otpremnice stavke").Tables[0];
                            for (int x = 0; x < DTotpremnica_stavke.Rows.Count; x++)
                            {
                                dgw.Rows.Add();
                                int br = dgw.Rows.Count - 1;
                                DataTable roba = classSQL.select("Select * from roba where sifra = '" + DTotpremnica_stavke.Rows[x]["sifra_robe"].ToString() + "'", "roba").Tables[0];
                                dgw.Rows[br].Cells[0].Value = dgw.Rows.Count;
                                dgw.Rows[br].Cells["sifra"].Value = DTotpremnica_stavke.Rows[x]["sifra_robe"].ToString();
                                dgw.Rows[br].Cells["naziv"].Value = roba.Rows[0]["naziv"].ToString();
                                dgw.Rows[br].Cells["jmj"].Value = roba.Rows[0]["jm"].ToString();
                                dgw.Rows[br].Cells["oduzmi"].Value = roba.Rows[0]["oduzmi"].ToString();
                                dgw.Rows[br].Cells["cijena_bez_pdva"].Value = string.Format("{0:0.000}", DTotpremnica_stavke.Rows[x]["vpc"]);
                                dgw.Rows[br].Cells["kolicina"].Value = DTotpremnica_stavke.Rows[x]["kolicina"].ToString();
                                dgw.Rows[br].Cells["porez"].Value = DTotpremnica_stavke.Rows[x]["porez"].ToString();
                                dgw.Rows[br].Cells["rabat"].Value = DTotpremnica_stavke.Rows[x]["rabat"].ToString();
                                dgw.Rows[br].Cells["skladiste"].Value = DTotpremnica_stavke.Rows[x]["id_skladiste"].ToString();
                                dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
                                dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
                                dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
                                dgw.Rows[br].Cells["vpc"].Value = string.Format("{0:0.000}", DTotpremnica_stavke.Rows[x]["vpc"]);
                                //dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["mpc"]);
                                dgw.Rows[br].Cells["nc"].Value = string.Format("{0:0.00}", DTotpremnica_stavke.Rows[x]["nbc"].ToString());
                                dgw.Rows[br].Cells["id_stavka"].Value = DTotpremnica_stavke.Rows[x]["id_stavka"].ToString();

                                dgw.Select();
                                dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[3];
                                ControlDisableEnable(0, 1, 1, 0, 1);
                                edit = false;

                                //povratna naknada mora biti poslije izračuna jer bi se inače upisivala trenutna povratna naknada
                                //a ne povratna naknada upisana u fakturi
                                try
                                {
                                    dgw.Rows[br].Cells["povratna_naknada"].Value = DTotpremnica_stavke.Rows[x]["povratna_naknada"].ToString();
                                }
                                catch
                                {
                                    //uzeti iz baze
                                    //DataTable DTpovrNaknada = classSQL.select("SELECT iznos FROM povratna_naknada WHERE sifra='" + DSFS.Rows[i]["sifra"].ToString().Trim() + "'", "roba").Tables[0];
                                    //dgw.Rows[br].Cells["povratna_naknada"].Value = DTpovrNaknada.Rows.Count > 0 ? DTpovrNaknada.Rows[0]["iznos"].ToString() : "0,00";
                                    dgw.Rows[br].Cells["povratna_naknada"].Value = "0.00";
                                }

                                if (header_1x)
                                {
                                    txtSifraOdrediste.Text = DTotpremnice.Rows[0]["id_odrediste"].ToString();
                                    try { txtPartnerNaziv.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["id_odrediste"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); }
                                    catch (Exception) { }
                                    txtSifraFakturirati.Text = DTotpremnice.Rows[0]["osoba_partner"].ToString();
                                    try { txtPartnerNaziv1.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["osoba_partner"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); }
                                    catch (Exception) { }
                                    header_1x = false;
                                }
                                izracun(false);
                            }
                        }
                        catch { }
                    }
                }
            }
        }

        private void fillServis(string broj)
        {
            //fill header
            DataTable DTotpremnice = classSQL.select("SELECT * FROM radni_nalog_servis WHERE broj = '" + broj + "'", "radni_nalog_servis").Tables[0];

            txtSifraOdrediste.Text = DTotpremnice.Rows[0]["id_odrediste"].ToString();
            try { txtPartnerNaziv.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["id_odrediste"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); }
            catch (Exception) { }
            txtSifraFakturirati.Text = DTotpremnice.Rows[0]["id_fakturirati"].ToString();
            try { txtPartnerNaziv1.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["id_fakturirati"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); }
            catch (Exception) { }
            cbZiroRacun.SelectedValue = DTotpremnice.Rows[0]["zr"].ToString();
            cbNacinPlacanja.SelectedValue = DTotpremnice.Rows[0]["id_nacin_placanja"].ToString();
            cbValuta.SelectedValue = DTotpremnice.Rows[0]["id_valuta"].ToString();
            cbOtprema.SelectedValue = DTotpremnice.Rows[0]["otprema"].ToString();

            //--------fill otpremnica stavke----------------------------
            DataTable dtR = new DataTable();
            DSFS = classSQL.select("SELECT * FROM radni_nalog_servis_stavke WHERE broj = '" + DTotpremnice.Rows[0]["broj"].ToString() + "'", "radni_nalog_servis_stavke").Tables[0];

            for (int i = 0; i < DSFS.Rows.Count; i++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;
                string s = "";

                DataTable tblRoba = classSQL.select("SELECT oduzmi FROM roba WHERE sifra='" + DSFS.Rows[i]["sifra"].ToString().Trim() + "'", "roba").Tables[0];

                if (tblRoba.Rows[0]["oduzmi"].ToString() == "DA")
                {
                    s = "SELECT roba_prodaja.nc,roba.naziv,roba.jm,roba_prodaja.sifra,roba.oduzmi FROM roba_prodaja INNER JOIN roba ON roba_prodaja.sifra=roba.sifra WHERE roba_prodaja.sifra='" + DSFS.Rows[i]["sifra"].ToString() + "' AND roba_prodaja.id_skladiste='" + DSFS.Rows[i]["id_skladiste"].ToString() + "'";
                }
                else
                {
                    s = "SELECT roba.nc,roba.naziv,roba.jm,roba.sifra,roba.oduzmi FROM roba WHERE sifra='" + DSFS.Rows[i]["sifra"].ToString() + "'";
                }

                dtR = classSQL.select(s, "roba_prodaja").Tables[0];

                dgw.Rows[br].Cells[0].Value = i + 1;
                dgw.Rows[br].Cells["sifra"].Value = dtR.Rows[0]["sifra"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = dtR.Rows[0]["naziv"].ToString();
                dgw.Rows[br].Cells["jmj"].Value = dtR.Rows[0]["jm"].ToString();
                dgw.Rows[br].Cells["oduzmi"].Value = dtR.Rows[0]["oduzmi"].ToString();
                dgw.Rows[br].Cells["cijena_bez_pdva"].Value = string.Format("{0:0.000}", DSFS.Rows[i]["vpc"]);
                dgw.Rows[br].Cells["kolicina"].Value = DSFS.Rows[i]["kolicina"].ToString();
                dgw.Rows[br].Cells["porez"].Value = DSFS.Rows[i]["porez"].ToString();
                dgw.Rows[br].Cells["rabat"].Value = DSFS.Rows[i]["rabat"].ToString();
                dgw.Rows[br].Cells["skladiste"].Value = DSFS.Rows[i]["id_skladiste"].ToString();
                dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
                dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
                dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
                dgw.Rows[br].Cells["vpc"].Value = string.Format("{0:0.000}", DSFS.Rows[i]["vpc"]);
                //dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["mpc"]);
                dgw.Rows[br].Cells["nc"].Value = string.Format("{0:0.00}", dtR.Rows[0]["nc"].ToString());
                dgw.Rows[br].Cells["id_stavka"].Value = DSFS.Rows[i]["id_stavka"].ToString();

                dgw.Select();
                dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[3];
                ControlDisableEnable(0, 1, 1, 0, 1);
                edit = false;

                //povratna naknada mora biti poslije izračuna jer bi se inače upisivala trenutna povratna naknada
                //a ne povratna naknada upisana u fakturi
                try
                {
                    dgw.Rows[br].Cells["povratna_naknada"].Value = DSFS.Rows[i]["povratna_naknada"].ToString();
                }
                catch
                {
                    //uzeti iz baze
                    DataTable DTpovrNaknada = classSQL.select("SELECT iznos FROM povratna_naknada WHERE sifra='" + DSFS.Rows[i]["sifra"].ToString().Trim() + "'", "roba").Tables[0];
                    dgw.Rows[br].Cells["povratna_naknada"].Value = DTpovrNaknada.Rows.Count > 0 ? DTpovrNaknada.Rows[0]["iznos"].ToString() : "0,00";
                }

                izracun(false);
            }
        }

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.CurrentCell.ColumnIndex == 3 & dgw.CurrentRow.Cells["oduzmi"].FormattedValue.ToString() == "DA")
            {
                SetCijenaSkladiste();
            }
            else if (dgw.CurrentCell.ColumnIndex == 9)
            {
                if (dgw.CurrentRow.Cells["skladiste"].Value == null & dgw.CurrentRow.Cells["oduzmi"].FormattedValue.ToString() == "DA")
                {
                    MessageBox.Show("Niste odabrali skladište", "Greška");
                    return;
                }

                dgw.CurrentCell.Selected = false;
                txtSifra_robe.Text = "";
                txtSifra_robe.BackColor = Color.Silver;
                txtSifra_robe.Select();
            }
            else if (dgw.CurrentCell.ColumnIndex == 7)
            {
                try
                {
                    double mpc = Convert.ToDouble(dgw.CurrentRow.Cells["mpc"].FormattedValue.ToString());
                    double porez = 1 + Convert.ToDouble(dgw.CurrentRow.Cells["porez"].FormattedValue.ToString()) / 100;
                    dgw.CurrentRow.Cells["vpc"].Value = Math.Round(mpc / porez, 3);
                }
                catch (Exception)
                {
                    MessageBox.Show("Koristite enter za sljedeću kolonu.");
                }
            }

            izracun(true);
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.Rows.Count < 1)
                return;
            dgw.BeginEdit(true);
        }

        private void cbOtprema_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                nuGodinaPredujma.Select();
            }
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

        private void txtBrojServisa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT broj FROM radni_nalog_servis" +
                    " WHERE godina='" + nuGodinaServisa.Value.ToString() + "' AND broj='" + txtBrojServisa.Text + "'",
                    "radni_nalog_servis").Tables[0];

                deleteFields();

                if (DT.Rows.Count == 0)
                {
                    MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                }
                else if (DT.Rows.Count == 1)
                {
                    EnableDisable(true);
                    edit = true;
                    btnDeleteAllFaktura.Enabled = true;
                    fillServis(DT.Rows[0][0].ToString());
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmSviRadniNaloziServis sp = new frmSviRadniNaloziServis();
            sp.btnUrediSifru.Enabled = false;
            sp.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataTable DTsend = new DataTable();
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("nbc");
            DTsend.Columns.Add("broj_fakture");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("sifra");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("oduzmi");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("povratna_naknada");
            DataRow row;

            if (dgw.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku.", "Greška");
                return;
            }

            decimal dec_parse;
            if (Decimal.TryParse(txtSifraOdrediste.Text, out dec_parse))
            {
                txtSifraOdrediste.Text = dec_parse.ToString();
            }
            else
            {
                MessageBox.Show("Greška kod upisa odredišta.", "Greška"); return;
            }

            if (Decimal.TryParse(txtSifraFakturirati.Text, out dec_parse))
            {
                txtSifraFakturirati.Text = dec_parse.ToString();
            }
            else
            {
                MessageBox.Show("Greška kod upisa šifre za fakturirati.", "Greška"); return;
            }

            string uk = u.ToString();
            if (classSQL.remoteConnectionString == "")
            {
                uk = uk.Replace(",", ".");
            }
            else
            {
                uk = uk.Replace(".", ",");
            }

            string oduzmi_iz_skladista = "";
            if (chbOduzmiIzSkladista.Checked)
            {
                oduzmi_iz_skladista = "1";
            }
            else
            {
                oduzmi_iz_skladista = "0";
            }

            DateTime datum = dtpDatum.Value;

            string sql = "DELETE FROM ispis_fakture; DELETE FROM ispis_faktura_stavke;";
            provjera_sql(classSQL.update(sql));

            sql = "INSERT INTO ispis_fakture (broj_fakture,id_odrediste,id_fakturirati,date,dateDVO,datum_valute,id_izjava,id_zaposlenik,id_zaposlenik_izradio,model" +
                ",id_nacin_placanja,zr,id_valuta,otprema,id_predujam,napomena,id_vd,godina_predujma,godina_ponude,godina_fakture,oduzmi_iz_skladista,ukupno) VALUES " +
                " (" +
                 " '" + ttxBrojFakture.Text + "'," +
                " '" + txtSifraOdrediste.Text + "'," +
                " '" + txtSifraFakturirati.Text + "'," +
                " '" + datum + "'," +
                " '" + dtpDatumDVO.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " '" + dtpDanaValuta.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " '" + cbIzjava.SelectedValue + "'," +
                " '" + cbKomercijalist.SelectedValue.ToString() + "'," +
                " '" + Properties.Settings.Default.id_zaposlenik + "'," +
                " '" + txtModel.Text + "'," +
                " '" + cbNacinPlacanja.SelectedValue.ToString() + "'," +
                " '" + cbZiroRacun.SelectedValue.ToString() + "'," +
                " '" + cbValuta.SelectedValue.ToString() + "'," +
                " '" + cbOtprema.SelectedValue + "'," +
                " '" + "0" + "'," +
                " '" + rtbNapomena.Text + "'," +
                " '" + cbVD.SelectedValue.ToString().Trim() + "'," +
                " '" + nuGodinaPredujma.Value.ToString() + "'," +
                " '" + nuGodinaPonude.Value.ToString() + "'," +
                " '" + nmGodinaFakture.Value.ToString() + "'," +
                " '" + oduzmi_iz_skladista + "'," +
                " '" + uk + "'" +
                ")";
            provjera_sql(classSQL.insert(sql));

            string sifra;
            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                sifra = dg(i, "sifra");

                row = DTsend.NewRow();
                row["kolicina"] = dg(i, "kolicina");
                row["vpc"] = dg(i, "vpc");
                row["nbc"] = dg(i, "nc");
                row["broj_fakture"] = ttxBrojFakture.Text;
                row["porez"] = dg(i, "porez");
                row["sifra"] = ReturnSifra(dg(i, "sifra"));
                row["rabat"] = dg(i, "rabat");
                row["oduzmi"] = dg(i, "oduzmi");
                row["id_skladiste"] = dgw.Rows[i].Cells[3].Value;
                row["povratna_naknada"] = dg(i, "povratna_naknada");

                DTsend.Rows.Add(row);

                sql = "INSERT INTO ispis_faktura_stavke (kolicina,vpc,nbc,porez,broj_fakture,rabat,id_skladiste,sifra,povratna_naknada,oduzmi) VALUES " +
                    "(" +
                    "'" + DTsend.Rows[i]["kolicina"].ToString() + "'," +
                    "'" + DTsend.Rows[i]["vpc"].ToString().Replace(",", ".") + "'," +
                    "'" + DTsend.Rows[i]["nbc"].ToString() + "'," +
                    "'" + DTsend.Rows[i]["porez"].ToString() + "'," +
                    "'" + DTsend.Rows[i]["broj_fakture"].ToString() + "'," +
                    "'" + DTsend.Rows[i]["rabat"].ToString() + "'," +
                    "'" + DTsend.Rows[i]["id_skladiste"].ToString() + "'," +
                    "'" + DTsend.Rows[i]["sifra"].ToString() + "'," +
                    "'" + DTsend.Rows[i]["povratna_naknada"].ToString().Replace(",", ".") + "'," +
                    "'" + DTsend.Rows[i]["oduzmi"].ToString() + "'" +
                    ")";

                provjera_sql(classSQL.insert(sql));
            }
            //DialogResult dialogResult = MessageBox.Show("Na Engleskom jeziku - YES, na Hrvatskom - NO?", "Odabir", MessageBoxButtons.YesNo);
            //if (dialogResult == DialogResult.Yes)
            //{
            //    Report.Faktura_engl.repFaktura_engl pr = new Report.Faktura_engl.repFaktura_engl();
            //    pr.samoIspis = true;
            //    pr.racunajTecaj = ValutaKuna(cbValuta.Text);
            //    pr.dokumenat = "FAK";
            //    pr.broj_dokumenta = ttxBrojFakture.Text;
            //    pr.ImeForme = "Faktura";
            //    pr.ShowDialog();
            //}
            //else
            //{
            Report.Faktura.repFaktura pr = new Report.Faktura.repFaktura();
            pr.samoIspis = true;
            pr.racunajTecaj = ValutaKuna(cbValuta.Text);
            pr.dokumenat = "FAK";
            pr.broj_dokumenta = ttxBrojFakture.Text;
            pr.ImeForme = "Faktura";
            pr.ShowDialog();
            //}
        }

        /// <summary>
        /// ne koristi se jer nisam znal napuniti ručno dataset
        /// </summary>
        private void NapuniReport()
        {
            if (dgw.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku.", "Greška");
                return;
            }

            decimal dec_parse;
            if (Decimal.TryParse(txtSifraOdrediste.Text, out dec_parse))
            {
                txtSifraOdrediste.Text = dec_parse.ToString();
            }
            else
            {
                MessageBox.Show("Greška kod upisa odredišta.", "Greška"); return;
            }

            if (Decimal.TryParse(txtSifraFakturirati.Text, out dec_parse))
            {
                txtSifraFakturirati.Text = dec_parse.ToString();
            }
            else
            {
                MessageBox.Show("Greška kod upisa šifre za fakturirati.", "Greška"); return;
            }

            Report.Faktura.repFaktura pr = new Report.Faktura.repFaktura();
            //pr.ispisdSFaktura = dSFaktura;
            //pr.ispisdSRfakturaStavke = dSRfakturaStavke;
            //pr.ispisdSRpodaciTvrtke = dSRpodaciTvrtke;

            Dataset.DSRfakturaStavke dSRfakturaStavke = pr.dSRfakturaStavke;
            dSRfakturaStavke.Tables.Clear();
            DataTable DTsend = dSRfakturaStavke.Tables.Add("DTfakturaStavke");

            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("mpc");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("nbc");
            DTsend.Columns.Add("broj_fakture");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("sifra");
            DTsend.Columns.Add("jm");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("oduzmi");
            DTsend.Columns.Add("mpcStavka");
            DTsend.Columns.Add("rabatStavka");
            DTsend.Columns.Add("naziv");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("povratna_naknada");
            DataRow row;

            string broj = brojFakture();

            string uk = u.ToString();
            if (classSQL.remoteConnectionString == "")
            {
                uk = uk.Replace(",", ".");
            }
            else
            {
                uk = uk.Replace(".", ",");
            }

            string kol = "";
            string sifra;

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                sifra = dg(i, "sifra");

                row = DTsend.NewRow();
                row["kolicina"] = dg(i, "kolicina");
                row["vpc"] = dg(i, "vpc");
                row["nbc"] = dg(i, "nc");
                row["broj_fakture"] = ttxBrojFakture.Text;
                row["porez"] = dg(i, "porez");
                row["sifra"] = ReturnSifra(dg(i, "sifra"));
                row["rabat"] = dg(i, "rabat");
                row["oduzmi"] = dg(i, "oduzmi");
                row["id_skladiste"] = dgw.Rows[i].Cells[3].Value;
                row["povratna_naknada"] = dg(i, "povratna_naknada");

                DTsend.Rows.Add(row);
            }

            PCPOS.classNumberToLetter broj_u_text = new PCPOS.classNumberToLetter();

            double vpc = 0;
            double kolicina = 0;
            double porez;
            double rabat;
            double mpcStavka;
            double rabatStavka = 0;
            double pdvStavka = 0;
            double osnovicaStavka = 0;
            double rabatSve = 0;
            double mpc = 0;
            double sveUkupno = 0;
            double pdvUkupno = 0;
            double osnovicaUkupno = 0;
            DataTable dtTemp;

            for (int i = 0; i < DTsend.Rows.Count; i++)
            {
                sifra = DTsend.Rows[i]["sifra"].ToString();
                vpc = Math.Round(Convert.ToDouble(DTsend.Rows[i]["vpc"].ToString()), 3);
                kolicina = Math.Round(Convert.ToDouble(DTsend.Rows[i]["kolicina"].ToString()), 3);
                porez = Math.Round(Convert.ToDouble(DTsend.Rows[i]["porez"].ToString()), 2);
                rabat = Math.Round(Convert.ToDouble(DTsend.Rows[i]["rabat"].ToString()), 2);

                mpc = Math.Round(vpc * (1 + porez / 100.0) - 0.0000001, 2);

                rabatStavka = Math.Round(mpc * kolicina * rabat / 100, 2);
                mpcStavka = Math.Round(mpc * kolicina - rabatStavka, 2);
                pdvStavka = Math.Round(mpcStavka * (1 - 100 / (100 + porez)), 2);
                osnovicaStavka = Math.Round(mpcStavka - pdvStavka, 2);

                DTsend.Rows[i].SetField("mpc", mpc);
                DTsend.Rows[i].SetField("vpc", vpc);
                DTsend.Rows[i].SetField("porez", porez);
                DTsend.Rows[i].SetField("rabat", rabat);
                DTsend.Rows[i].SetField("kolicina", kol);
                DTsend.Rows[i].SetField("mpcStavka", mpcStavka);
                DTsend.Rows[i].SetField("rabatStavka", rabatStavka);

                string sqlS = "Select naziv," +
                    " jm" +
                    " FROM roba where sifra='" + sifra + "'";

                dtTemp = classSQL.select(sqlS, "roba").Tables[0];

                if (dtTemp.Rows.Count > 0)
                {
                    DTsend.Rows[i].SetField("naziv", dtTemp.Rows[0]["naziv"].ToString());
                    DTsend.Rows[i].SetField("jm", dtTemp.Rows[0]["jm"].ToString());
                }

                rabatSve += rabatStavka;
                osnovicaUkupno += osnovicaStavka;
                pdvUkupno += pdvStavka;
                sveUkupno += mpcStavka;
            }

            dSRfakturaStavke.Tables[0].Merge(DTsend, false);
            DTsend.AcceptChanges();
            dSRfakturaStavke.AcceptChanges();
            //dSRfakturaStavke.Tables.Add(DTsend.Copy());

            //dSRfakturaStavke.Merge(DTsend, false, MissingSchemaAction.Add);

            DateTime datum = dtpDatum.Value;

            string broj_slovima = broj_u_text.PretvoriBrojUTekst(sveUkupno.ToString(), ',', "kn", "lp").ToString();

            //Dataset.DSFaktura dSFaktura = new Dataset.DSFaktura();
            Dataset.DSFaktura dSFaktura = pr.dSFaktura;
            dSFaktura.Tables.Clear();
            DataTable DTFaktura = dSFaktura.Tables.Add("DTRfaktura");
            //DataTable DTFaktura = dSFaktura.Tables[0];
            DTFaktura.Columns.Add("broj_fakture");
            DTFaktura.Columns.Add("datum");
            DTFaktura.Columns.Add("datum_dvo");
            DTFaktura.Columns.Add("datum_valute");
            DTFaktura.Columns.Add("jir");
            DTFaktura.Columns.Add("zki");
            DTFaktura.Columns.Add("mjesto_troska");
            DTFaktura.Columns.Add("placanje");
            DTFaktura.Columns.Add("otprema");
            DTFaktura.Columns.Add("model");
            DTFaktura.Columns.Add("napomena");
            DTFaktura.Columns.Add("rabat");
            DTFaktura.Columns.Add("ukupno");
            DTFaktura.Columns.Add("iznos_pdv");
            DTFaktura.Columns.Add("osnovica");
            DTFaktura.Columns.Add("godina_fakture");
            DTFaktura.Columns.Add("Naslov");
            DTFaktura.Columns.Add("kupac_tvrtka");
            DTFaktura.Columns.Add("naziv_date1");
            DTFaktura.Columns.Add("naziv_date2");
            DTFaktura.Columns.Add("kupac_adresa");
            DTFaktura.Columns.Add("id_kupac_grad");
            DTFaktura.Columns.Add("sifra_kupac");
            DTFaktura.Columns.Add("napomena_tvrtka");
            DTFaktura.Columns.Add("zr");
            DTFaktura.Columns.Add("izradio");
            DTFaktura.Columns.Add("banka");
            DTFaktura.Columns.Add("broj_slovima");
            DTFaktura.Columns.Add("kupac_oib");

            row = DTFaktura.NewRow();
            row["broj_fakture"] = broj;
            row["datum"] = datum;
            row["datum_dvo"] = dtpDatumDVO.Value.ToString("yyyy-MM-dd H:mm:ss");
            row["datum_valute"] = dtpDanaValuta.Value.ToString("yyyy-MM-dd H:mm:ss");
            row["jir"] = "123456789";
            row["zki"] = "987654321";
            row["mjesto_troska"] = "";
            row["placanje"] = cbNacinPlacanja.SelectedValue.ToString();
            row["otprema"] = cbOtprema.SelectedValue;
            row["model"] = txtModel.Text + broj + nmGodinaFakture.Value.ToString() + txtSifraFakturirati.Text;
            row["napomena"] = rtbNapomena.Text;
            row["rabat"] = rabatSve;
            row["ukupno"] = sveUkupno;
            row["iznos_pdv"] = pdvUkupno;
            row["osnovica"] = osnovicaUkupno;
            row["godina_fakture"] = nmGodinaFakture.Value.ToString();
            row["Naslov"] = broj + Util.Korisno.VratiDucanIBlagajnuZaIspis(2);
            row["kupac_tvrtka"] = "";
            row["naziv_date1"] = "Datum isporuke:";
            row["naziv_date2"] = "Datum dospijeća:";
            row["kupac_adresa"] = "";
            row["id_kupac_grad"] = "";
            row["sifra_kupac"] = "";
            row["napomena_tvrtka"] = "";
            row["zr"] = "";
            row["izradio"] = "";
            row["banka"] = "";
            row["broj_slovima"] = broj_slovima.ToLower();
            row["kupac_oib"] = "";
            DTFaktura.Rows.Add(row);

            string sql = "SELECT " +
                " nacin_placanja.naziv_placanja AS placanje," +
                " otprema.naziv AS otprema," +
                " partners.ime_tvrtke AS kupac_tvrtka," +
                " partners.adresa AS kupac_adresa," +
                " partners.id_grad AS id_kupac_grad," +
                " partners.id_partner AS sifra_kupac," +
                " partners.napomena AS napomena_tvrtka," +
                " ziro_racun.ziroracun AS zr," +
                " zaposlenici.ime + ' ' + zaposlenici.prezime AS izradio," +
                " ziro_racun.banka AS banka," +
                " partners.oib AS kupac_oib" +
                " FROM fakture_van" +
                " LEFT JOIN partners ON partners.id_partner='" + txtSifraOdrediste.Text + "'" +
                " LEFT JOIN otprema ON otprema.id_otprema='" + cbOtprema.SelectedValue + "'" +
                " LEFT JOIN nacin_placanja ON nacin_placanja.id_placanje='" + cbNacinPlacanja.SelectedValue.ToString() + "'" +
                " LEFT JOIN ziro_racun ON ziro_racun.id_ziroracun='" + cbZiroRacun.SelectedValue.ToString() + "'" +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'";

            dtTemp = classSQL.select(sql, "partners").Tables[0];

            if (dtTemp.Rows.Count > 0)
            {
                DTFaktura.Rows[0].SetField("placanje", dtTemp.Rows[0]["placanje"].ToString());
                DTFaktura.Rows[0].SetField("otprema", dtTemp.Rows[0]["otprema"].ToString());
                DTFaktura.Rows[0].SetField("kupac_tvrtka", dtTemp.Rows[0]["kupac_tvrtka"].ToString());
                DTFaktura.Rows[0].SetField("kupac_adresa", dtTemp.Rows[0]["kupac_adresa"].ToString());
                DTFaktura.Rows[0].SetField("id_kupac_grad", dtTemp.Rows[0]["id_kupac_grad"].ToString());
                DTFaktura.Rows[0].SetField("sifra_kupac", dtTemp.Rows[0]["sifra_kupac"].ToString());
                DTFaktura.Rows[0].SetField("napomena_tvrtka", dtTemp.Rows[0]["napomena_tvrtka"].ToString());
                DTFaktura.Rows[0].SetField("zr", dtTemp.Rows[0]["zr"].ToString());
                DTFaktura.Rows[0].SetField("izradio", dtTemp.Rows[0]["izradio"].ToString());
                DTFaktura.Rows[0].SetField("banka", dtTemp.Rows[0]["banka"].ToString());
                DTFaktura.Rows[0].SetField("kupac_oib", dtTemp.Rows[0]["kupac_oib"].ToString());
            }

            string id_kupac_grad = "";
            string id_kupac = "";
            if (dSFaktura.Tables[0].Rows.Count > 0)
            {
                id_kupac_grad = dSFaktura.Tables[0].Rows[0]["id_kupac_grad"].ToString();
                id_kupac = dSFaktura.Tables[0].Rows[0]["sifra_kupac"].ToString();
            }
            else
            {
                //MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            dSFaktura.AcceptChanges();

            string naziv_fakture = " podaci_tvrtka.naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql(id_kupac_grad, naziv_fakture, id_kupac);

            //Dataset.DSRpodaciTvrtke dSRpodaciTvrtke = new Dataset.DSRpodaciTvrtke();
            Dataset.DSRpodaciTvrtke dSRpodaciTvrtke = pr.dSRpodaciTvrtke;
            dSRpodaciTvrtke.Tables.Clear();
            DataTable DTpodaciTvrtke = dSRpodaciTvrtke.Tables.Add("DTRpodaciTvrtke");

            //DataTable DTpodaciTvrtke = classSQL.select(sql1, "podaci_tvrtka").Tables[0].Copy();
            //dSRpodaciTvrtke.Tables.Add(DTpodaciTvrtke);
            //dSRpodaciTvrtke.Tables[0].Rows[0] = DTpodaciTvrtke.Rows[0];
            dSRpodaciTvrtke.AcceptChanges();

            pr.dTRfakturaBindingSource.DataMember = "DTRfaktura";
            pr.dTRfakturaBindingSource.DataSource = dSFaktura;
            pr.dTRpodaciTvrtkeBindingSource.DataMember = "DTRpodaciTvrtke";
            pr.dTRpodaciTvrtkeBindingSource.DataSource = dSRpodaciTvrtke;
            pr.dTfakturaStavkeBindingSource.DataMember = "DTfakturaStavke";
            pr.dTfakturaStavkeBindingSource.DataSource = dSRfakturaStavke;
            //pr.dTRfakturaBindingSource.ResetBindings(false);
            //pr.dTRpodaciTvrtkeBindingSource.ResetBindings(false);
            //pr.dTfakturaStavkeBindingSource.ResetBindings(false);

            ReportDataSource source = new ReportDataSource("DTRfaktura", dSFaktura.Tables[0]);

            ReportDataSource source1 = new ReportDataSource("DTRpodaciTvrtke", dSRpodaciTvrtke.Tables[0]);
            ReportDataSource source2 = new ReportDataSource("DTfakturaStavke", dSRfakturaStavke.Tables[0]);
            pr.reportViewer1.LocalReport.DataSources.Clear();
            pr.reportViewer1.LocalReport.DataSources.Add(source);
            pr.reportViewer1.LocalReport.DataSources.Add(source1);
            pr.reportViewer1.LocalReport.DataSources.Add(source2);
            //pr.reportViewer1.DataBind();
            pr.reportViewer1.LocalReport.Refresh();

            pr.samoIspis = true;
            pr.dokumenat = "FAK";
            pr.broj_dokumenta = broj;
            pr.ImeForme = "Faktura";
            pr.ShowDialog();
        }

        private void chbObracunPoreza_CheckedChanged(object sender, EventArgs e)
        {
            if (dgw.Rows.Count == 0)
                return;

            if (chbObracunPoreza.Checked)
            {
                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    try
                    {
                        string sifra = dgw.Rows[i].Cells["sifra"].FormattedValue.ToString();
                        DataTable DT = classSQL.select("SELECT porez FROM roba WHERE sifra='" + sifra + "'", "").Tables[0];
                        dgw.Rows[i].Cells["porez"].Value = DT.Rows[0][0].ToString();
                        izracun(false);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            else
            {
                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    dgw.Rows[i].Cells["porez"].Value = 0;
                    izracun(false);
                }
            }
        }

        private void cbValuta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ucitano)
            {
                if (cbValuta.SelectedValue.ToString() == "5")
                {
                    chbObracunPoreza.Visible = false;
                }
                else
                {
                    chbObracunPoreza.Visible = true;
                }
            }
        }

        private void btnStorno_Click(object sender, EventArgs e)
        {
            FakturaVan.frmFakturaVanStorno a = new FakturaVan.frmFakturaVanStorno();
            a.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi sp = new frmPartnerTrazi();
            sp.ShowDialog();
            txtotprkupac.Text = Properties.Settings.Default.id_partner;
        }

        public bool popunjena_s_otpremnicom = false; //ako je fakturirano zapisuje u otpremnice (naplaceno da ne uzme jos 1 put)

        private void txtotprkupac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT id_partner FROM partners WHERE id_partner = '" + txtotprkupac.Text + "' ", "otpremnice").Tables[0];
                deleteFields();
                if (DT.Rows.Count == 0)
                {
                    MessageBox.Show("Odabrani partner ne postoji.", "Greška");
                }
                else if (DT.Rows.Count == 1)
                {
                    EnableDisable(true);
                    edit = true;
                    btnDeleteAllFaktura.Enabled = true;
                    fillOtpremnice(DT.Rows[0][0].ToString());
                    popunjena_s_otpremnicom = true;
                    chbOduzmiIzSkladista.Enabled = false;
                    chbOduzmiIzSkladista.Checked = false;
                    btnObrisi.Enabled = false;
                    txtotprkupac.Text = "";
                }
            }
        }

        private void txtotprkupac_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender.ToString().Length == 0) return;

            if (sender.ToString()[0] == '+')
            {
                if ('+' == (e.KeyChar)) return;
            }

            if (sender.ToString()[0] == '-')
            {
                if ('-' == (e.KeyChar)) return;
            }

            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dtpDatum.Value = DateTime.Now;
            dtpDatumDVO.Value = DateTime.Now;
            dtpDanaValuta.Value = DateTime.Now;
        }
    }
}