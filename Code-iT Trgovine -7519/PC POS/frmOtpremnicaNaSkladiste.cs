using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmOtpremnicaNaSkladiste : Form
    {
        public frmOtpremnicaNaSkladiste()
        {
            InitializeComponent();
        }

        public string broj_otpremnice_edit { get; set; }
        public string skladiste_edit { get; set; }
        public string godina_edit { get; set; }

        private DataTable DTRoba = new DataTable();
        private DataSet DS_Skladiste = new DataSet();
        private DataSet dsSkladisteDo = new DataSet();
        private DataSet DS_ZiroRacun = new DataSet();
        private DataSet DS_Zaposlenik = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DSIzjava = new DataSet();
        private DataSet DSnazivPlacanja = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataTable DTotpremnice = new DataTable();
        private DataTable DSFS = new DataTable();
        private DataTable DTOtprema = new DataTable();
        private double u = 0;
        private bool edit = false;
        private string SveUkupno = "0";
        private bool load = false;
        public frmMenu MainForm { get; set; }

        private void frmOtpremnica_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Form1_Paint);

            /****************************SINKRONIZACIJA SA WEB-OM***********************/
            bgSinkronizacija = new BackgroundWorker();
            bgSinkronizacija.DoWork += new DoWorkEventHandler(bgSinkronizacija_DoWork);
            bgSinkronizacija.WorkerSupportsCancellation = true;
            /****************************SINKRONIZACIJA SA WEB-OM***********************/

            MyDataGrid.MainForm = this;
            PaintRows(dgw);
            fillComboBox();
            txtBrojOtpremnice.Select();
            txtBrojOtpremnice.Text = brojOtpremnice();
            numeric();
            EnableDisable(false);
            txtBrojOtpremnice.Enabled = true;
            ControlDisableEnable(1, 0, 0, 1, 0);
            if (broj_otpremnice_edit != null) { fillOtpremnice(skladiste_edit, godina_edit); }
            //this.Paint += new PaintEventHandler(Form1_Paint);

            chbSNBC_CheckedChanged(sender, e);
            izracun();
            load = true;
        }

        /****************************SINKRONIZACIJA SA WEB-OM*****************/
        private BackgroundWorker bgSinkronizacija = null;
        private synWeb.synPokretac PokretacSinkronizacije = new synWeb.synPokretac();
        /****************************SINKRONIZACIJA SA WEB-OM*****************/

        private void GasenjeForme_FormClosing(object sender, FormClosingEventArgs e)
        {
            /****************************SINKRONIZACIJA SA WEB-OM*****************/
            bgSinkronizacija.RunWorkerAsync();
            /****************************SINKRONIZACIJA SA WEB-OM*****************/
        }

        private void bgSinkronizacija_DoWork(object sender, DoWorkEventArgs e)
        {
            //PokretacSinkronizacije.PokreniSinkronizaciju(true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
        }

        private class MyDataGrid : System.Windows.Forms.DataGridView
        {
            public static frmOtpremnicaNaSkladiste MainForm { get; set; }

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
            if (d.CurrentCell.ColumnIndex == 4)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[(chbSNBC.Checked ? 13 : 6)];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == (chbSNBC.Checked ? 13 : 6))
            {
                int curent = d.CurrentRow.Index;
                txtSifra_robe.Text = "";
                txtSifra_robe.Focus();
            }
            else if (d.CurrentCell.ColumnIndex == 7)
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
            if (d.CurrentCell.ColumnIndex == 6)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 7)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[6];
                d.BeginEdit(true);
            }
        }

        private void RightDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            if (d.CurrentCell.ColumnIndex == 4)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[6];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 6)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 7)
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
            if (curent == 0)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index - 1].Cells[4];
                d.BeginEdit(true);
            }
        }

        private void DownDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            int curent = d.CurrentRow.Index;
            if (curent == d.RowCount - 1)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index + 1].Cells[4];
                d.BeginEdit(true);
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

        private void PaintRows(DataGridView dg)
        {
            int br = 0;
            for (int i = 0; i < dg.Rows.Count; i++)
            {
                if (br == 0)
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
                    br++;
                }
                else
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    br = 0;
                }
            }
            DataGridViewRow row = dg.RowTemplate;
            row.Height = 25;
        }

        private void numeric()
        {
            nmGodinaOtpremnice.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nmGodinaOtpremnice.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nmGodinaOtpremnice.Value = Convert.ToInt16(DateTime.Now.Year.ToString());
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private string brojOtpremnice()
        {
            string sql = "SELECT MAX(broj_otpremnice) FROM otpremnice_na_skl WHERE id_skladisteod = '" + cbSkladiste.SelectedValue + "';";
            DataTable DSbr = classSQL.select(sql, "otpremnice").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void fillComboBox()
        {
            //fill otprema
            DTOtprema = classSQL.select("SELECT * FROM otprema", "otprema").Tables[0];
            cbOtprema.DataSource = DTOtprema;
            cbOtprema.DisplayMember = "naziv";
            cbOtprema.ValueMember = "id_otprema";

            //fill skladiste
            DS_Skladiste = classSQL.select("SELECT * FROM skladiste where samo_nbc = false ORDER BY skladiste", "skladiste");
            cbSkladiste.DataSource = DS_Skladiste.Tables[0];
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";

            dsSkladisteDo = classSQL.select("SELECT * FROM skladiste where samo_nbc = true ORDER BY skladiste", "skladiste");
            cmbSkladisteDo.DataSource = dsSkladisteDo.Tables[0];
            cmbSkladisteDo.DisplayMember = "skladiste";
            cmbSkladisteDo.ValueMember = "id_skladiste";

            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici;", "zaposlenici");
            cbKomercijalist.DataSource = DS_Zaposlenik.Tables[0];
            cbKomercijalist.DisplayMember = "IME";
            cbKomercijalist.ValueMember = "id_zaposlenik";
            cbKomercijalist.SelectedValue = Properties.Settings.Default.id_zaposlenik;

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
            cbVD.SelectedValue = "IFVP ";

            //fill tko je prijavljen
            txtIzradio.Text = classSQL.select("SELECT ime + ' ' + prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
        }

        private void txtBrojOtpremnice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                nmGodinaOtpremnice.Select();
            }
        }

        private void nmGodinaOtpremnice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbSkladiste.Select();
            }
        }

        private void cbSkladiste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cmbSkladisteDo.Select();
            }
        }

        //private void txtSifraOdrediste_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        string Str = txtSifraOdrediste.Text.Trim();
        //        double Num;
        //        bool isNum = double.TryParse(Str, out Num);
        //        if (!isNum)
        //        {
        //            txtSifraOdrediste.Text = "0";
        //        }

        //        //txtSifraPosPatner.Text = txtSifraOdrediste.Text;

        //        e.SuppressKeyPress = true;
        //        DSpartner = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraOdrediste.Text + "'", "partners");
        //        if (DSpartner.Tables[0].Rows.Count > 0)
        //        {
        //            txtPartnerNaziv.Text = DSpartner.Tables[0].Rows[0][0].ToString();
        //            //txtSifraPosPatner.Select();
        //            //txtSifraPosPatner.Text = txtSifraOdrediste.Text;
        //            txtSifraPosPatner.Select();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
        //            txtSifraOdrediste.Select();
        //        }

        //    }
        //}

        //private void txtPartnerNaziv_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        txtSifraPosPatner.Select();
        //    }
        //}

        //private void textBox4_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        string Str = txtSifraPosPatner.Text.Trim();
        //        double Num;
        //        bool isNum = double.TryParse(Str, out Num);
        //        if (!isNum)
        //        {
        //            txtSifraPosPatner.Text = "0";
        //        }

        //        e.SuppressKeyPress = true;
        //        DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner='" + txtSifraPosPatner.Text + "'", "partners");
        //        if (DSpartner.Tables[0].Rows.Count > 0)
        //        {
        //            txtNazivPosPartner.Text = DSpartner.Tables[0].Rows[0][0].ToString();
        //            cbVD.Select();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
        //        }
        //    }
        //}

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbVD.Select();
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
                cbIzjava.Select();
            }
        }

        private void cbIzjava_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                rtbNapomena.Text = cbIzjava.Text;
                e.SuppressKeyPress = true;
                rtbNapomena.Select();
            }
        }

        private void rtbNapomena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbKomercijalist.Select();
            }
        }

        private void cbKomercijalist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbOtprema.Select();
            }
        }

        private void txtIzradio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbOtprema.Select();
            }
        }

        private void cbOtprema_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                //txtMjOtpreme.Select();
                txtSifra_robe.Select();
            }
        }

        //private void txtMjOtpreme_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        //txtAdresaOtp.Select();
        //    }
        //}

        //private void txtAdresaOtp_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        txtIsprave1.Select();
        //    }
        //}

        //private void txtIsprave1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        txtSifraPrijevoznik.Select();
        //    }
        //}

        //private void txtBrojPonude_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        txtSifraPrijevoznik.Select();
        //    }
        //}

        //private void nuGodinaPonude_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        txtSifraPrijevoznik.Select();
        //    }
        //}

        //private void txtSifraNarKupca_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        txtSifraPrijevoznik.Select();
        //    }
        //}

        //private void cbNarKupca_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        txtSifraPrijevoznik.Select();
        //    }
        //}

        //private void txtSifraPrijevoznik_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;

        //        if (txtSifraPrijevoznik.Text == "")
        //        {
        //            txtSifraPrijevoznik.Text = "0";
        //            txtNazivPrijevoznik.Select();
        //            return;
        //        }

        //        DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraPrijevoznik.Text + "'", "partners").Tables[0];
        //        if (DSpar.Rows.Count > 0)
        //        {
        //            txtNazivPrijevoznik.Text = DSpar.Rows[0][0].ToString();
        //            txtReg.Select();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
        //        }

        //    }
        //}

        //private void txtNazivPrijevoznik_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        txtReg.Select();
        //    }
        //}

        //private void txtReg_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        txtIstovarnoMJ.Select();
        //    }
        //}

        //private void txtIstovarnoMJ_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        dtpIstovarniRok.Select();
        //    }
        //}

        //private void dtpIstovarniRok_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        txtTroskovi.Select();
        //    }
        //}

        private void txtTroskovi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifra_robe.Select();
            }
        }

        private void provjera_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender.ToString().Length == 0)
                return;

            if (sender.ToString()[0] == '+')
            {
                if ('+' == (e.KeyChar))
                    return;
            }

            if (sender.ToString()[0] == '-')
            {
                if ('-' == (e.KeyChar))
                    return;
            }

            //if ((char)(',') == (e.KeyChar))
            //{
            //    e.Handled = false; return;
            //}

            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// FUNKCIJA POSTAVLJA POPUST NA SVE STAVKE
        /// </summary>

        //int dodjeli_popust = 0;
        //private void PartnerPostaviPopust()
        //{
        //    decimal rabat = 0;

        //    if (DSpartner != null)
        //    {
        //        decimal.TryParse(DSpartner.Tables[0].Rows[0]["popust"].ToString(), out rabat);

        //        if (dodjeli_popust == -1 || rabat == 0)
        //        {
        //            return;
        //        }
        //        else if (dodjeli_popust != -1 && dodjeli_popust != 1)
        //        {
        //            if (MessageBox.Show("Ovaj partner ima popust od " + rabat.ToString() + "%\r\nDali ste sigurni da želite dodijeliti navedeni popust.", "Popust na račun", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //                dodjeli_popust = 1;
        //            else
        //                dodjeli_popust = -1;
        //        }

        //        foreach (DataGridViewRow r in dgw.Rows)
        //        {
        //            r.Cells["rabat"].Value = Math.Round(rabat, 5).ToString("#0.00000");

        //            izracun();
        //        }

        //    }
        //}

        //DataSet DSpartner;
        //private void btnPartner_Click(object sender, EventArgs e)
        //{
        //    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
        //    partnerTrazi.ShowDialog();
        //    if (Properties.Settings.Default.id_partner != "")
        //    {
        //        DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
        //        if (DSpartner.Tables[0].Rows.Count > 0)
        //        {
        //            PartnerPostaviPopust();
        //            txtSifraOdrediste.Text = DSpartner.Tables[0].Rows[0]["id_partner"].ToString();
        //            txtPartnerNaziv.Text = DSpartner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
        //            txtSifraPosPatner.Text = DSpartner.Tables[0].Rows[0]["id_partner"].ToString();
        //            txtNazivPosPartner.Text = DSpartner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
        //            cbVD.Select();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
        //        }
        //    }
        //}

        //private void button4_Click(object sender, EventArgs e)
        //{
        //    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
        //    partnerTrazi.ShowDialog();
        //    if (Properties.Settings.Default.id_partner != "")
        //    {
        //        DSpartner = new DataSet();
        //        DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
        //        if (DSpartner.Tables[0].Rows.Count > 0)
        //        {
        //            PartnerPostaviPopust();
        //            txtSifraOdrediste.Text = DSpartner.Tables[0].Rows[0]["id_partner"].ToString();
        //            txtPartnerNaziv.Text = DSpartner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
        //            txtSifraPosPatner.Text = DSpartner.Tables[0].Rows[0]["id_partner"].ToString();
        //            txtNazivPosPartner.Text = DSpartner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
        //            cbVD.Select();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
        //        }
        //    }
        //}

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
        //    partnerTrazi.ShowDialog();
        //    if (Properties.Settings.Default.id_partner != "")
        //    {
        //        DSpartner = new DataSet();
        //        DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
        //        if (DSpartner.Tables[0].Rows.Count > 0)
        //        {
        //            PartnerPostaviPopust();
        //            txtSifraPrijevoznik.Text = DSpartner.Tables[0].Rows[0]["id_partner"].ToString();
        //            txtNazivPrijevoznik.Text = DSpartner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
        //        }
        //    }
        //}

        private void btnOpenRoba_Click(object sender, EventArgs e)
        {
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            roba_trazi.idSkladiste = (int)cbSkladiste.SelectedValue;
            roba_trazi.ShowDialog();
            string propertis_sifra = Properties.Settings.Default.id_roba.Trim();
            if (propertis_sifra != "")
            {
                if (Class.PodaciTvrtka.oibTvrtke != "40097758416")
                {
                    for (int y = 0; y < dgw.Rows.Count; y++)
                    {
                        if (propertis_sifra == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString())
                        {
                            MessageBox.Show("Artikl ili usluga već postoje!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                string sql = "SELECT * FROM roba WHERE sifra='" + propertis_sifra + "'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    txtBrojOtpremnice.Enabled = false;
                    nmGodinaOtpremnice.Enabled = false;
                    //txtSifra_robe.Text = DTRoba.Rows[0]["sifra"].ToString();
                    SetRoba();
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SetRoba()
        {
            string kol;
            string nc;
            string vpc;

            string porez = "0";

            string mpc;
            DataTable DTRP = classSQL.select("SELECT * FROM roba_prodaja WHERE sifra='" + DTRoba.Rows[0]["sifra"].ToString() + "' AND id_skladiste='" + cbSkladiste.SelectedValue + "'", "roba_prodaja").Tables[0];

            decimal _NBC = Util.Korisno.VratiNabavnuCijenu(DTRoba.Rows[0]["sifra"].ToString(), cbSkladiste.SelectedValue.ToString());
            //provjerava dali postoji artikl u roba_prodaja ili dali se radi o atriklu koji se ne skida sa skladišta
            if (DTRP.Rows.Count == 0 || DTRoba.Rows[0]["oduzmi"].ToString() == "DA")
            {
                kol = "0";
                nc = _NBC.ToString("#0.00");
                vpc = DTRoba.Rows[0]["vpc"].ToString();
                porez = DTRoba.Rows[0]["porez"].ToString();

                mpc = DTRoba.Rows[0]["mpc"].ToString();
            }
            else
            {
                kol = DTRP.Rows[0]["kolicina"].ToString();
                nc = _NBC.ToString("#0.00");
                vpc = DTRP.Rows[0]["vpc"].ToString();
                porez = DTRP.Rows[0]["porez"].ToString();
                mpc = ((Convert.ToDouble(vpc) * Convert.ToDouble(porez) / 100) + Convert.ToDouble(vpc)).ToString();
            }
            vpc = 0.ToString();
            mpc = 0.ToString();

            dgw.Rows.Add();
            int br = dgw.Rows.Count - 1;

            dgw.Rows[br].Cells[0].Value = br + 1;
            dgw.Rows[br].Cells["sifra"].Value = DTRoba.Rows[0]["sifra"].ToString();
            dgw.Rows[br].Cells["naziv"].Value = DTRoba.Rows[0]["naziv"].ToString();
            dgw.Rows[br].Cells["jmj"].Value = DTRoba.Rows[0]["jm"].ToString();
            dgw.Rows[br].Cells["cijena_bez_pdva"].Value = string.Format("{0:0.000}", vpc);
            dgw.Rows[br].Cells["kolicina"].Value = "1";

            if (chbBezPdVa.Checked)
                dgw.Rows[br].Cells["porez"].Value = "0";
            else
                dgw.Rows[br].Cells["porez"].Value = porez;

            dgw.Rows[br].Cells["oduzmi"].Value = DTRoba.Rows[0]["oduzmi"].ToString();
            dgw.Rows[br].Cells["rabat"].Value = "0,00";
            dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
            dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,000";
            dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
            dgw.Rows[br].Cells["vpc"].Value = string.Format("{0:0.000}", vpc);
            dgw.Rows[br].Cells["mpc"].Value = string.Format("{0:0.00}", mpc);
            dgw.Rows[br].Cells["nc"].Value = _NBC.ToString("#0.00");

            dgw.Select();
            dgw.CurrentCell = dgw.Rows[br].Cells[4];
            dgw.BeginEdit(true);

            SetCijenaSkladiste();

            //PartnerPostaviPopust();
            izracun();
            PaintRows(dgw);
        }

        public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            double result;
            return double.TryParse(val, NumberStyle,
                System.Globalization.CultureInfo.CurrentCulture, out result);
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void izracun()
        {
            if (dgw.RowCount > 0)
            {
                int rowBR = dgw.CurrentRow != null ? dgw.CurrentRow.Index : dgw.SelectedRows[0].Index;
                //dgw.CurrentRow = dgw.SelectedRows[0].Index;

                if (isNumeric(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString(), System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowLeadingSign |
            System.Globalization.NumberStyles.AllowLeadingWhite | System.Globalization.NumberStyles.AllowTrailingWhite) == false) { dgw.Rows[rowBR].Cells["kolicina"].Value = "1"; MessageBox.Show("Greška kod upisa količine.", "Greška"); }
                if (isNumeric(dgw.Rows[rowBR].Cells["rabat"].FormattedValue.ToString().ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) == false) { dgw.Rows[rowBR].Cells["rabat"].Value = "0"; MessageBox.Show("Greška kod upisa rabata.", "Greška"); }
                if (isNumeric(dgw.Rows[rowBR].Cells["rabat_iznos"].FormattedValue.ToString().ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) == false) { dgw.Rows[rowBR].Cells["rabat_iznos"].Value = "0,00"; MessageBox.Show("Greška kod upisa rabata.", "Greška"); }

                double kol = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString()), 3);
                double vpc = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["vpc"].FormattedValue.ToString()), 3);
                double porez = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["porez"].FormattedValue.ToString()), 2);
                double rbt = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["rabat"].FormattedValue.ToString()), 2);

                double porez_ukupno = vpc * porez / 100;
                double mpc = porez_ukupno + vpc;
                double mpc_sa_kolicinom = mpc * kol;
                double rabat = mpc * rbt / 100;
                double iznos;

                //za nbc otpremnicu
                double nbc = 0, nbcUkupno = 0, nbcPdv = 0, nbcSveUkupno = 0;
                nbc = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["nc"].FormattedValue.ToString()), 2);
                nbcUkupno = nbc * kol;
                //nbcSveUkupno += nbcUkupno;

                //-----
                mpc = 0;

                dgw.Rows[rowBR].Cells["mpc"].Value = Math.Round(mpc, 2).ToString("#0.00");
                dgw.Rows[rowBR].Cells["rabat_iznos"].Value = Math.Round(rabat * kol, 2).ToString("#0.00");
                dgw.Rows[rowBR].Cells["iznos_bez_pdva"].Value = Math.Round((mpc - rabat) * kol / (1 + porez / 100), 2).ToString("#0.00");
                dgw.Rows[rowBR].Cells["iznos_ukupno"].Value = Math.Round(nbc * kol, 2).ToString("#0.00");
                dgw.Rows[rowBR].Cells["cijena_bez_pdva"].Value = Math.Round((mpc - rabat) / (1 + porez / 100), 3).ToString("#0.000");
                dgw.Rows[rowBR].Cells["porez"].Value = porez.ToString("#0.00");
                dgw.Rows[rowBR].Cells["rabat"].Value = rbt.ToString("#0.00");
                dgw.Rows[rowBR].Cells["kolicina"].Value = kol.ToString("#0.000");

                double B_pdv = 0;
                u = 0;

                for (int i = 0; i < dgw.RowCount; i++)
                {
                    iznos = Convert.ToDouble(dgw.Rows[i].Cells["iznos_ukupno"].FormattedValue.ToString());
                    u += Math.Round(iznos, 2);
                    iznos = Convert.ToDouble(dgw.Rows[i].Cells["iznos_bez_pdva"].FormattedValue.ToString());
                    B_pdv += Math.Round(iznos, 3);

                    nbc = Math.Round(Convert.ToDouble(dgw.Rows[i].Cells["nc"].FormattedValue.ToString()), 2);
                    kol = Math.Round(Convert.ToDouble(dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString()), 3);
                    nbcUkupno = nbc * kol;
                    nbcSveUkupno += nbcUkupno;
                }

                SveUkupno = u.ToString();

                if (chbSNBC.Checked)
                {
                    textBox1.Text = "Ukupno NBC: " + Math.Round(nbcSveUkupno, 2).ToString("#0.00");
                    textBox2.Text = "";
                    textBox3.Text = "";
                }
                else
                {
                    textBox1.Text = "Ukupno sa PDV-om: " + Math.Round(u, 2).ToString("#0.00");
                    textBox2.Text = "Bez PDV-a: " + Math.Round(B_pdv, 3).ToString("#0.000");
                    textBox3.Text = "PDV: " + Math.Round(Math.Round(u, 2) - Math.Round(B_pdv, 3), 2).ToString("#0.00");
                }
            }
            else
            {
                if (chbSNBC.Checked)
                {
                    textBox1.Text = "Ukupno NBC: " + Math.Round(0F, 2).ToString("#0.00");
                    textBox2.Text = "";
                    textBox3.Text = "";
                }
                else
                {
                    textBox1.Text = "Ukupno sa PDV-om: " + Math.Round(0F, 2).ToString("#0.00");
                    textBox2.Text = "Bez PDV-a: " + Math.Round(0F, 3).ToString("#0.000");
                    textBox3.Text = "PDV: " + Math.Round(Math.Round(0F, 2) - Math.Round(0F, 3), 2).ToString("#0.00");
                }
            }
        }

        private void dgw_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //int row = dgw.CurrentCell.RowIndex;
            //if (dgw.CurrentCell.ColumnIndex == 3 & dgw.CurrentRow.Cells["oduzmi"].FormattedValue.ToString() == "da")
            //{
            //    SetCijenaSkladiste();
            //}

            //else if (dgw.CurrentCell.ColumnIndex == 10)
            //{
            //    dgw.CurrentCell.Selected = false;
            //    txtSifra_robe.Text = "";
            //    //txtSifra_robe.BackColor = Color.Silver;
            //    txtSifra_robe.Select();
            //}
            //izracun();
        }

        private void SetCijenaSkladiste()
        {
            DataSet dsRobaProdaja = new DataSet();
            dsRobaProdaja = classSQL.select("SELECT * FROM roba_prodaja WHERE id_skladiste='" + cbSkladiste.SelectedValue + "' AND sifra='" + dgw.CurrentRow.Cells["sifra"].FormattedValue.ToString() + "'", "roba_prodaja");
            if (dsRobaProdaja.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToDouble(dsRobaProdaja.Tables[0].Rows[0]["kolicina"].ToString()) > 0)
                {
                    if (!chbBezPdVa.Checked)
                        dgw.CurrentRow.Cells["porez"].Value = dsRobaProdaja.Tables[0].Rows[0]["porez"].ToString();

                    dgw.CurrentRow.Cells["vpc"].Value = string.Format("{0:0.00}", dsRobaProdaja.Tables[0].Rows[0]["vpc"]);
                    lblNaDan.ForeColor = Color.Green;
                    lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima " + dsRobaProdaja.Tables[0].Rows[0]["kolicina"].ToString() + " " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
                }
                else
                {
                    if (!chbBezPdVa.Checked)
                        dgw.CurrentRow.Cells["porez"].Value = dsRobaProdaja.Tables[0].Rows[0]["porez"].ToString();

                    dgw.CurrentRow.Cells["vpc"].Value = string.Format("{0:0.00}", dsRobaProdaja.Tables[0].Rows[0]["vpc"]);
                    lblNaDan.ForeColor = Color.Red;
                    lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima " + dsRobaProdaja.Tables[0].Rows[0]["kolicina"].ToString() + " " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
                }
            }
            else
            {
                lblNaDan.ForeColor = Color.Red;
                lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima 0,00 " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
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

        private void txtSifra_robe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (Class.PodaciTvrtka.oibTvrtke != "40097758416")
                {
                    for (int y = 0; y < dgw.Rows.Count; y++)
                    {
                        if (txtSifra_robe.Text == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString().Trim())
                        {
                            MessageBox.Show("Artikl ili usluga već postoje u ovoj kalkulaciji", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                string sql = "SELECT * FROM roba WHERE sifra='" + txtSifra_robe.Text + "'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();
                    txtBrojOtpremnice.Enabled = false;
                    nmGodinaOtpremnice.Enabled = false;
                    ////Čemu ovo?
                    //dgw.Rows[0].Cells["kolicina"].Value="1";
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void printaj(string broj, string skl)
        {
            Report.Faktura.repFaktura pr = new Report.Faktura.repFaktura();
            pr.dokumenat = "OTP";
            pr.broj_dokumenta = broj;
            pr.from_skladiste = skl;
            pr.ImeForme = "Otpremnica";
            pr.ShowDialog();
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            string sql;
            //dodjeli_popust = 0;
            //DSpartner = null;
            //ako je update trenutne otpremnice onda ulazi u if.
            //treba provjeriti da li postoji otpremnica s trenutnim atributima
            //ako postoji onda ne dopusti ažuriranje i javi grešku!
            if (txtBrojOtpremnice.Text == broj_otpremnice_edit &&
                cbSkladiste.SelectedValue.ToString() == skladiste_edit &&
                nmGodinaOtpremnice.Value.ToString() == godina_edit)
            {
            }
            else
            {
                sql = "SELECT * FROM otpremnice_na_skl WHERE broj_otpremnice = '" + txtBrojOtpremnice.Text +
                    "' AND id_skladisteod='" + cbSkladiste.SelectedValue.ToString() + "' AND godina_otpremnice='" + nmGodinaOtpremnice.Value + "'";
                //fill header
                DTotpremnice = classSQL.select(sql, "otpremnice").Tables[0];

                if (DTotpremnice.Rows.Count > 0)
                {
                    MessageBox.Show("Otpremnica broj = " + txtBrojOtpremnice.Text + ", skladište = " + cbSkladiste.SelectedValue.ToString() +
                                    ", godina = " + nmGodinaOtpremnice.Value + " već postoji!" + Environment.NewLine +
                                    "Probajte izmjeniti broj, godinu ili skladište otpremnice te ponovo spremiti istu.", "Greška!");
                    return;
                }
            }

            DataTable DTsend = new DataTable();
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("nbc");
            DTsend.Columns.Add("godina_otpremnice");
            DTsend.Columns.Add("broj_otpremnice");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("sifra_robe");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("oduzmi");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("id_otpremnice");
            DTsend.Columns.Add("naplaceno_fakturom");
            DataRow row;

            if (dgw.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku za spremiti.", "Greška");
                return;
            }

            //if (txtSifraOdrediste.Text == "")
            //{
            //    MessageBox.Show("Niste odabrali odredište.", "Greška");
            //    return;
            //}

            //if (txtSifraPosPatner.Text == "")
            //{
            //    MessageBox.Show("Niste odabrali poslovnog partnera.", "Greška");
            //    return;
            //}

            if (edit == true)
            {
                UpdateOtpremnica();
                if (MessageBox.Show("Spremljeno.\r\nŽelite li ispisati dokument?", "Spremljeno.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    printaj(txtBrojOtpremnice.Text, cbSkladiste.SelectedValue.ToString());
                }
                EnableDisable(false);
                deleteFields();
                btnSveFakture.Enabled = true;
                ControlDisableEnable(1, 0, 0, 1, 0);
                return;
            }

            //string partner_osoba = "";
            //if (rbOsoba.Checked)
            //{
            //    partner_osoba = "O";
            //}
            //else if (rbPoslovniPartner.Checked)
            //{
            //    partner_osoba = "P";
            //}

            string broj = brojOtpremnice();
            if (broj.Trim() != txtBrojOtpremnice.Text.Trim())
            {
                MessageBox.Show("Vaš broj dokumenta već je netko iskoristio.\r\nDodijeljen Vam je novi broj '" + broj + "'.", "Info");
                txtBrojOtpremnice.Text = broj;
            }

            //string Str = txtSifraPrijevoznik.Text.Trim();
            //double Num;
            //bool isNum = double.TryParse(Str, out Num);
            //if (!isNum)
            //{
            //    txtSifraPrijevoznik.Text = "0";
            //}

            if (classSQL.remoteConnectionString == "")
            {
                SveUkupno = SveUkupno.Replace(",", ".");
            }
            else
            {
                SveUkupno = SveUkupno.Replace(".", ",");
            }
            SveUkupno = SveUkupno.Replace(",", ".");

            string komercijalista = Properties.Settings.Default.id_zaposlenik;
            if (cbKomercijalist.SelectedValue != null)
                komercijalista = cbKomercijalist.SelectedValue.ToString();

            //insert header
            //sql = "INSERT INTO otpremnice (broj_otpremnice,partner_osoba,godina_otpremnice,id_skladiste,osoba_partner,id_odrediste," +
            //    "vrsta_dok,datum,id_izjava,napomena,id_kom,id_izradio,id_otprema,mj_otpreme,adr_otpreme,isprave,id_prijevoznik," +
            //    "registracija,istovarno_mj,istovarni_rok,troskovi_prijevoza,ukupno, novo, editirano, use_nbc) VALUES (" +
            //    "'" + txtBrojOtpremnice.Text + "'," +
            //    "'" + partner_osoba + "'," +
            //    "'" + nmGodinaOtpremnice.Value.ToString() + "'," +
            //    "'" + cbSkladiste.SelectedValue + "'," +
            //    "'" + txtSifraPosPatner.Text + "'," +
            //    "'" + txtSifraOdrediste.Text + "'," +
            //    "'" + cbVD.SelectedValue + "'," +
            //    "'" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
            //    "'" + cbIzjava.SelectedValue + "'," +
            //    "'" + rtbNapomena.Text + "'," +
            //    "'" + komercijalista + "'," +
            //    "'" + Properties.Settings.Default.id_zaposlenik + "'," +
            //    "'" + cbOtprema.SelectedValue + "'," +
            //    "'" + txtMjOtpreme.Text + "'," +
            //    "'" + txtAdresaOtp.Text + "'," +
            //    "'" + txtIsprave1.Text + "'," +
            //    "'" + txtSifraPrijevoznik.Text + "'," +
            //    "'" + txtReg.Text + "'," +
            //    "'" + txtIstovarnoMJ.Text + "'," +
            //    "'" + dtpIstovarniRok.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
            //    "'" + txtTroskovi.Text + "'," +
            //    "'" + SveUkupno + "'," +
            //    "'1', " +
            //    "'1', " +
            //    "'" + (chbSNBC.Checked ? 1 : 0) + "'" +
            //    ")";

            sql = @"INSERT INTO otpremnice_na_skl(
            godina_otpremnice, broj_otpremnice, id_skladisteod,
            id_skladistedo, datum, vrsta_dok, id_izjava, napomena, id_komercijalist,
            id_izradio, id_otprema, ukupno, use_nbc, use_pdv)
    VALUES (" + nmGodinaOtpremnice.Value + @", " + txtBrojOtpremnice.Text + @", " + cbSkladiste.SelectedValue + @",
            " + cbSkladiste.SelectedValue + @", '" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + @"', '" + cbVD.SelectedValue + @"', " + cbIzjava.SelectedValue + @", '" + rtbNapomena.Text.Trim() + @"', " + komercijalista + @",
            " + Properties.Settings.Default.id_zaposlenik + @", " + cbOtprema.SelectedValue + @", " + SveUkupno + @", " + (chbSNBC.Checked ? true : false).ToString().ToLower() + @", " + (chbBezPdVa.Checked ? true : false).ToString().ToLower() + @") RETURNING id_otpremnica;";

            DataSet dsHeadOtpremnica = classSQL.select(sql, "otpremnice_na_skl");
            string id_otpr = classSQL.select("Select MAX(id_otpremnica) From otpremnice", "IDotpr").Tables[0].Rows[0][0].ToString();

            if (dsHeadOtpremnica != null && dsHeadOtpremnica.Tables.Count > 0 && dsHeadOtpremnica.Tables[0] != null && dsHeadOtpremnica.Tables[0].Rows.Count > 0)
                id_otpr = dsHeadOtpremnica.Tables[0].Rows[0][0].ToString();

            string kol = "";
            for (int i = 0; i < dgw.RowCount; i++)
            {
                if (dg(i, "oduzmi") == "DA")
                {
                    kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), cbSkladiste.SelectedValue.ToString(), dg(i, "kolicina"), "1", "-");
                    SQL.SQLroba_prodaja.UpdateRows(cbSkladiste.SelectedValue.ToString(), kol, dg(i, "sifra"));
                }

                //string id_otpr = classSQL.select("Select MAX(id_otpremnica) From otpremnice", "IDotpr").Tables[0].Rows[0][0].ToString();
                row = DTsend.NewRow();
                row["kolicina"] = dg(i, "kolicina");
                row["vpc"] = dg(i, "vpc").Replace(",", ".");
                row["nbc"] = dg(i, "nc");
                row["godina_otpremnice"] = nmGodinaOtpremnice.Value.ToString();
                row["broj_otpremnice"] = txtBrojOtpremnice.Text;
                row["porez"] = dg(i, "porez");
                row["sifra_robe"] = dg(i, "sifra");
                row["rabat"] = dg(i, "rabat");
                row["oduzmi"] = dg(i, "oduzmi");
                row["id_skladiste"] = cbSkladiste.SelectedValue.ToString();
                row["id_otpremnice"] = id_otpr;
                row["naplaceno_fakturom"] = '0';
                DTsend.Rows.Add(row);
            }
            provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Nova otpremnica na skladis br." + txtBrojOtpremnice.Text + "')"));
            Util.AktivnostZaposlenika.SpremiAktivnost(dgw, cbSkladiste.SelectedValue.ToString(), "Otpremnica", txtBrojOtpremnice.Text, false);
            SQL.SQLotpremnica.InsertStavke(DTsend, true);
            if (MessageBox.Show("Spremljeno.\r\nŽelite li ispisati dokument?", "Spremljeno.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                printaj(txtBrojOtpremnice.Text, cbSkladiste.SelectedValue.ToString());
            }
            edit = false;
            EnableDisable(false);
            deleteFields();
            btnSveFakture.Enabled = true;
            txtBrojOtpremnice.ReadOnly = false;
            nmGodinaOtpremnice.ReadOnly = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private void EnableDisable(bool x)
        {
            cbVD.Enabled = x;
            //txtSifraOdrediste.Enabled = x;
            //txtPartnerNaziv.Enabled = x;
            //txtSifraNarKupca.Enabled = x;
            cbOtprema.Enabled = x;
            cmbSkladisteDo.Enabled = x;
            rtbNapomena.Enabled = x;
            txtSifra_robe.Enabled = x;
            //btnPartner.Enabled = x;
            dtpDatum.Enabled = x;
            cbIzjava.Enabled = x;
            cbKomercijalist.Enabled = x;
            btnObrisi.Enabled = x;
            btnOpenRoba.Enabled = x;
            //btnNarKupca.Enabled = x;
            //txtSifraPosPatner.Enabled = x;
            //txtMjOtpreme.Enabled = x;
            //txtAdresaOtp.Enabled = x;
            //txtIsprave1.Enabled = x;
            //txtBrojPonude.Enabled = x;
            //txtSifraPrijevoznik.Enabled = x;
            //txtReg.Enabled = x;
            //txtIstovarnoMJ.Enabled = x;
            //dtpIstovarniRok.Enabled = x;
            //txtTroskovi.Enabled = x;
            //cbNarKupca.Enabled = x;
            //button2.Enabled = x;
            //button3.Enabled = x;
            //button4.Enabled = x;
            //cbSkladiste.Enabled = x;
        }

        private void deleteFields()
        {
            //txtSifraOdrediste.Text = "";
            //txtPartnerNaziv.Text = "";
            //txtSifraNarKupca.Text = "";
            rtbNapomena.Text = "";
            txtSifra_robe.Text = "";
            //txtNazivPosPartner.Text = "";
            //txtNazivPrijevoznik.Text = "";
            //txtSifraPrijevoznik.Text = "";

            //txtSifraPosPatner.Text = "";
            //txtMjOtpreme.Text = "";
            //txtAdresaOtp.Text = "";
            //txtIsprave1.Text = "";
            //txtBrojPonude.Text = "";
            //txtSifraPrijevoznik.Text = "";
            //txtReg.Text = "";
            //txtIstovarnoMJ.Text = "";
            //txtTroskovi.Text = "";

            cbKomercijalist.SelectedIndex = 0;
            cbVD.SelectedIndex = 0;
            cbOtprema.SelectedIndex = 0;
            cbIzjava.SelectedIndex = 0;

            dgw.Rows.Clear();
        }

        private string dg(int row, string cell)
        {
            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            edit = false;
            //dodjeli_popust = 0;
            //DSpartner = null;
            EnableDisable(true);
            txtBrojOtpremnice.Text = brojOtpremnice();
            txtBrojOtpremnice.ReadOnly = true;
            nmGodinaOtpremnice.ReadOnly = true;
            //txtSifraOdrediste.Select();
            ControlDisableEnable(0, 1, 1, 0, 0);
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            btnSveFakture.Enabled = true;
            EnableDisable(false);
            //dodjeli_popust = 0;
            //DSpartner = null;
            deleteFields();
            txtBrojOtpremnice.Text = brojOtpremnice();
            edit = false;
            txtBrojOtpremnice.ReadOnly = false;
            nmGodinaOtpremnice.ReadOnly = false;
            txtBrojOtpremnice.Enabled = true;
            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            frmSveOtpremniceNaSkladiste objForm2 = new frmSveOtpremniceNaSkladiste();
            objForm2.sifra_otpremnice = "";
            objForm2.MainForm = this;
            broj_otpremnice_edit = null;
            godina_edit = null;
            skladiste_edit = null;
            objForm2.ShowDialog();
            if (broj_otpremnice_edit != null)
            {
                ControlDisableEnable(0, 1, 1, 0, 0);
                fillOtpremnice(skladiste_edit, godina_edit);
            }
        }

        private void fillOtpremnice(string skl, string godina)
        {
            EnableDisable(true);
            edit = true;

            string sql = "SELECT * FROM otpremnice WHERE broj_otpremnice = '" + broj_otpremnice_edit +
                "' AND id_skladiste='" + skl + "' AND godina_otpremnice='" + godina + "'";
            //fill header
            DTotpremnice = classSQL.select(sql, "otpremnice").Tables[0];

            //txtSifraPosPatner.Text = DTotpremnice.Rows[0]["osoba_partner"].ToString();
            //try { txtNazivPosPartner.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["osoba_partner"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); } catch (Exception) { }
            //txtSifraOdrediste.Text = DTotpremnice.Rows[0]["id_odrediste"].ToString();
            //try { txtPartnerNaziv.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["id_odrediste"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); } catch (Exception) { }
            cbVD.SelectedValue = DTotpremnice.Rows[0]["vrsta_dok"].ToString();
            dtpDatum.Value = Convert.ToDateTime(DTotpremnice.Rows[0]["datum"].ToString());
            cbIzjava.SelectedValue = DTotpremnice.Rows[0]["id_izjava"].ToString();
            rtbNapomena.Text = DTotpremnice.Rows[0]["napomena"].ToString();
            cbKomercijalist.SelectedValue = DTotpremnice.Rows[0]["id_kom"].ToString();
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + DTotpremnice.Rows[0]["id_izradio"].ToString() + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
            cbOtprema.SelectedValue = DTotpremnice.Rows[0]["id_otprema"].ToString();
            //txtMjOtpreme.Text = DTotpremnice.Rows[0]["mj_otpreme"].ToString();
            //txtAdresaOtp.Text = DTotpremnice.Rows[0]["adr_otpreme"].ToString();
            //txtIsprave1.Text = DTotpremnice.Rows[0]["isprave"].ToString();
            //if (DTotpremnice.Rows[0]["id_prijevoznik"].ToString() != "0")
            //{
            //    txtSifraPrijevoznik.Text = DTotpremnice.Rows[0]["id_prijevoznik"].ToString();
            //    try { txtNazivPrijevoznik.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["id_prijevoznik"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); } catch (Exception) { }
            //}
            //txtReg.Text = DTotpremnice.Rows[0]["registracija"].ToString();
            //txtIstovarnoMJ.Text = DTotpremnice.Rows[0]["istovarno_mj"].ToString();
            //dtpIstovarniRok.Value = Convert.ToDateTime(DTotpremnice.Rows[0]["istovarni_rok"].ToString());
            //txtTroskovi.Text = DTotpremnice.Rows[0]["troskovi_prijevoza"].ToString();
            nmGodinaOtpremnice.Value = Convert.ToDecimal(godina);
            txtBrojOtpremnice.Text = broj_otpremnice_edit;
            cbSkladiste.SelectedValue = skl;

            //if (DTotpremnice.Rows[0]["partner_osoba"].ToString() == "O")
            //{
            //    rbOsoba.Checked = true;
            //}
            //else if (DTotpremnice.Rows[0]["partner_osoba"].ToString() == "P")
            //{
            //    rbPoslovniPartner.Checked = true;
            //}
            chbSNBC.Checked = Convert.ToBoolean(DTotpremnice.Rows[0]["use_nbc"].ToString());
            chbBezPdVa.Checked = Convert.ToBoolean(DTotpremnice.Rows[0]["use_pdv"].ToString());

            //if (Convert.ToBoolean(DTotpremnice.Rows[0]["use_nbc"].ToString()))
            //{
            //    chbSNBC.Checked = true;
            //}
            //else
            //{
            //    chbSNBC.Checked = false;
            //}

            //--------fill otpremnica stavke----------------------------
            DataTable dtR = new DataTable();
            sql = "SELECT * FROM otpremnica_stavke WHERE broj_otpremnice = '" + broj_otpremnice_edit + "'" +
                " AND id_skladiste='" + skl + "' AND godina_otpremnice='" + godina + "'";
            DSFS = classSQL.select(sql, "otpremnica_stavke").Tables[0];

            for (int i = 0; i < DSFS.Rows.Count; i++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;
                string s = "";
                if (DSFS.Rows[i]["oduzmi"].ToString() == "DA")
                {
                    s = "SELECT roba_prodaja.nc,roba.naziv,roba.jm,roba_prodaja.sifra,roba.oduzmi FROM roba_prodaja LEFT JOIN roba ON roba_prodaja.sifra=roba.sifra WHERE roba_prodaja.sifra='" + DSFS.Rows[i]["sifra_robe"].ToString() + "' AND roba_prodaja.id_skladiste='" + DSFS.Rows[i]["id_skladiste"].ToString() + "'";
                }
                else
                {
                    s = "SELECT roba.nc,roba.naziv,roba.jm,roba.sifra,roba.oduzmi FROM roba WHERE sifra='" + DSFS.Rows[i]["sifra_robe"].ToString() + "'";
                }

                dtR = classSQL.select(s, "roba_prodaja").Tables[0];

                dgw.Rows[br].Cells[0].Value = i + 1;
                dgw.Rows[br].Cells["sifra"].Value = dtR.Rows[0]["sifra"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = dtR.Rows[0]["naziv"].ToString();
                dgw.Rows[br].Cells["jmj"].Value = dtR.Rows[0]["jm"].ToString();
                dgw.Rows[br].Cells["oduzmi"].Value = dtR.Rows[0]["oduzmi"].ToString();
                dgw.Rows[br].Cells["cijena_bez_pdva"].Value = string.Format("{0:0.00}", DSFS.Rows[i]["vpc"]);
                dgw.Rows[br].Cells["kolicina"].Value = DSFS.Rows[i]["kolicina"].ToString();
                dgw.Rows[br].Cells["porez"].Value = DSFS.Rows[i]["porez"].ToString();
                dgw.Rows[br].Cells["rabat"].Value = DSFS.Rows[i]["rabat"].ToString();
                //dgw.Rows[br].Cells["skladiste"].Value = DSFS.Rows[i]["id_skladiste"].ToString();
                dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
                dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
                dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
                dgw.Rows[br].Cells["vpc"].Value = string.Format("{0:0.00}", DSFS.Rows[i]["vpc"]);
                //dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["mpc"]);
                dgw.Rows[br].Cells["nc"].Value = string.Format("{0:0.00}", dtR.Rows[0]["nc"].ToString());
                dgw.Rows[br].Cells["id_stavka"].Value = DSFS.Rows[i]["id_stavka"].ToString();

                dgw.Select();
                dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[3];
                izracun();

                ControlDisableEnable(0, 1, 1, 0, 1);
                PaintRows(dgw);
            }

            dgw.Columns["cijena_bez_pdva"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["rabat_iznos"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos_bez_pdva"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos_ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgw.Columns["cijena_bez_pdva"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["rabat_iznos"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos_bez_pdva"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos_ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void btnDeleteAllFaktura_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Brisanjem ove otpremnice brišete i količinu robe sa skladišta. Da li ste sigurni da želite obrisati ovu otpremnicu?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                //dodjeli_popust = 0;
                //DSpartner = null;
                double skl = 0;
                double fa_kolicina = 0;
                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    if (dgw.Rows[i].Cells["id_stavka"].Value != null)
                    {
                        if (dg(i, "oduzmi") == "DA")
                        {
                            DataRow[] dataROW = DSFS.Select("id_stavka = " + dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString());
                            skl = Convert.ToDouble(classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString());
                            fa_kolicina = Convert.ToDouble(dataROW[0]["kolicina"].ToString());

                            skl = skl + fa_kolicina;
                            classSQL.update("UPDATE roba_prodaja SET kolicina='" + skl + "' WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'");
                        }
                    }
                }

                classSQL.delete("DELETE FROM otpremnica_stavke WHERE broj_otpremnice='" + txtBrojOtpremnice.Text + "' AND id_skladiste='" + cbSkladiste.SelectedValue + "'");

                classSQL.update("update otpremnice set ukupno = " +
                                "COALESCE((select SUM(((vpc * (1+REPLACE(porez, ',','.')::numeric/100)) - ((vpc * (1+REPLACE(porez, ',','.')::numeric/100)) * (REPLACE(rabat, ',', '.')::numeric/100))) * REPLACE(kolicina, ',', '.')::numeric) AS ukupno " +
                                "from otpremnica_stavke " +
                                "where broj_otpremnice='" + txtBrojOtpremnice.Text + "' AND id_skladiste='" + cbSkladiste.SelectedValue + "'),0), " +
                                "editirano = '1' " +
                            "where broj_otpremnice='" + txtBrojOtpremnice.Text + "' AND id_skladiste='" + cbSkladiste.SelectedValue + "';");

                //classSQL.delete("DELETE FROM otpremnice WHERE broj_otpremnice='" + txtBrojOtpremnice.Text + "' AND id_skladiste='" + cbSkladiste.SelectedValue + "'");
                classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje cijele otpremnice na skladište br." + txtBrojOtpremnice.Text + "')");
                Util.AktivnostZaposlenika.SpremiAktivnost(new DataGridView(), cbSkladiste.SelectedValue.ToString(), "Otpremnica", txtBrojOtpremnice.Text, true);
                MessageBox.Show("Obrisano.");

                edit = false;
                EnableDisable(false);
                deleteFields();
                btnDeleteAllFaktura.Enabled = false;
                btnObrisi.Enabled = false;
                ControlDisableEnable(1, 0, 0, 1, 0);
            }
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dgw.RowCount == 0)
            {
                return;
            }

            if (dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].Value != null)
            {
                DataRow[] dataROW = DSFS.Select("id_stavka = " + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString());

                if (MessageBox.Show("Brisanjem ove stavke brišete i količinu robe sa skladišta. Da li ste sigurni da želite obrisati ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (dg(dgw.CurrentRow.Index, "oduzmi") == "DA")
                    {
                        string kol = classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dgw.Rows[dgw.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString();
                        kol = (Convert.ToDouble(kol) + Convert.ToDouble(dataROW[0]["kolicina"].ToString())).ToString();
                        classSQL.update("UPDATE roba_prodaja SET kolicina='" + kol + "' WHERE sifra='" + dgw.Rows[dgw.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'");
                    }
                    classSQL.delete("DELETE FROM otpremnica_stavke WHERE id_stavka='" + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() + "'");

                    classSQL.update("update otpremnice set ukupno = " +
                            " COALESCE((select SUM(((vpc * (1+REPLACE(porez, ',','.')::numeric/100)) - ((vpc * (1+REPLACE(porez, ',','.')::numeric/100)) * (REPLACE(rabat, ',', '.')::numeric/100))) * REPLACE(kolicina, ',', '.')::numeric) AS ukupno " +
                            " from otpremnica_stavke " +
                            " where broj_otpremnice='" + txtBrojOtpremnice.Text + "' AND id_skladiste='" + cbSkladiste.SelectedValue + "'),0), " +
                            " editirano = '1' " +
                            " where broj_otpremnice='" + txtBrojOtpremnice.Text + "' AND id_skladiste='" + cbSkladiste.SelectedValue + "';");

                    dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                    provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje stavke sa otpremnice br." + txtBrojOtpremnice.Text + "')"));
                    Util.AktivnostZaposlenika.SpremiAktivnost(dgw, cbSkladiste.SelectedValue.ToString(), "Otpremnica", txtBrojOtpremnice.Text, true);
                    MessageBox.Show("Obrisano.");
                }
            }
            else
            {
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                MessageBox.Show("Obrisano.");
            }
        }

        private void UpdateOtpremnica()
        {
            DataTable DTsend = new DataTable();
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("nbc");
            DTsend.Columns.Add("godina_otpremnice");
            DTsend.Columns.Add("broj_otpremnice");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("sifra_robe");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("oduzmi");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("id_stavka");
            DTsend.Columns.Add("id_otpremnice");
            DTsend.Columns.Add("naplaceno_fakturom");
            DataRow row;

            DataTable DTsend1 = new DataTable();
            DTsend1.Columns.Add("kolicina");
            DTsend1.Columns.Add("vpc");
            DTsend1.Columns.Add("nbc");
            DTsend1.Columns.Add("godina_otpremnice");
            DTsend1.Columns.Add("broj_otpremnice");
            DTsend1.Columns.Add("porez");
            DTsend1.Columns.Add("sifra_robe");
            DTsend1.Columns.Add("rabat");
            DTsend1.Columns.Add("oduzmi");
            DTsend1.Columns.Add("id_skladiste");
            DTsend1.Columns.Add("id_otpremnice");
            DTsend1.Columns.Add("naplaceno_fakturom");
            DataRow row1;

            //string partner_osoba = "";
            //if (rbOsoba.Checked)
            //{
            //    partner_osoba = "O";
            //}
            //else if (rbPoslovniPartner.Checked)
            //{
            //    partner_osoba = "P";
            //}

            //string Str = txtSifraPrijevoznik.Text.Trim();
            //double Num;
            //bool isNum = double.TryParse(Str, out Num);
            //if (!isNum)
            //{
            //    txtSifraPrijevoznik.Text = "0";
            //}

            if (classSQL.remoteConnectionString == "")
            {
                SveUkupno = SveUkupno.Replace(",", ".");
            }
            else
            {
                SveUkupno = SveUkupno.Replace(".", ",");
            }

            string komercijalista = Properties.Settings.Default.id_zaposlenik;
            if (cbKomercijalist.SelectedValue != null)
                komercijalista = cbKomercijalist.SelectedValue.ToString();

            string sql = "UPDATE otpremnice_na_skl SET " +
                " godina_otpremnice = '" + nmGodinaOtpremnice.Value + "'," +
                " id_skladisteOD = '" + cbSkladiste.SelectedValue.ToString() + "'," +
                " id_skladisteDO = '" + cbSkladiste.SelectedValue.ToString() + "'," +
                " datum = '" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                //" osoba_partner = '" + txtSifraPosPatner.Text + "'," +
                //" id_odrediste = '" + txtSifraOdrediste.Text + "'," +
                " vrsta_dok = '" + cbVD.SelectedValue + "'," +
                " id_izjava = '" + cbIzjava.SelectedValue + "'," +
                " napomena = '" + rtbNapomena.Text + "'," +
                " id_komercijalist = '" + komercijalista + "'," +
                " id_otprema = '" + cbOtprema.SelectedValue + "'," +
                //" mj_otpreme='" + txtMjOtpreme.Text + "'," +
                //" adr_otpreme='" + txtAdresaOtp.Text + "'," +
                //" isprave='" + txtIsprave1.Text + "'," +
                //" id_prijevoznik='" + txtSifraPrijevoznik.Text + "'," +
                //" registracija='" + txtReg.Text + "'," +
                //" istovarno_mj='" + txtIstovarnoMJ.Text + "'," +
                //" istovarni_rok='" + dtpIstovarniRok.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                //" troskovi_prijevoza='" + txtTroskovi.Text + "'," +
                //" partner_osoba = '" + partner_osoba + "'," +
                " ukupno = " + SveUkupno + "," +
                " editirano = true," +
                " use_nbc = " + (chbSNBC.Checked ? 1 : 0) + "," +
                " use_pdv = " + (chbBezPdVa.Checked ? 1 : 0) + "" +
                " WHERE broj_otpremnice = " + broj_otpremnice_edit + "" +
                " AND id_skladiste = " + skladiste_edit + "" +
                " AND godina_otpremnice = " + godina_edit + "";
            provjera_sql(classSQL.update(sql));

            string kol;
            for (int i = 0; i < dgw.RowCount; i++)
            {
                if (dgw.Rows[i].Cells["id_stavka"].Value != null)
                {
                    DataRow[] dataROW = DSFS.Select("id_stavka = " + dgw.Rows[i].Cells["id_stavka"].Value.ToString());

                    if (dg(i, "oduzmi") == "DA")
                    {
                        if (cbSkladiste.SelectedValue.ToString() == dataROW[0]["id_skladiste"].ToString())
                        {
                            kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), cbSkladiste.SelectedValue.ToString(), (Convert.ToDouble(dataROW[0]["kolicina"].ToString()) - Convert.ToDouble(dg(i, "kolicina"))).ToString(), "1", "+");
                            SQL.SQLroba_prodaja.UpdateRows(cbSkladiste.SelectedValue.ToString(), kol, dg(i, "sifra"));
                        }
                        else
                        {
                            //vraća na staro skladiste
                            kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), dataROW[0]["id_skladiste"].ToString(), dataROW[0]["kolicina"].ToString(), "1", "+");
                            SQL.SQLroba_prodaja.UpdateRows(dataROW[0]["id_skladiste"].ToString(), kol, dg(i, "sifra"));

                            //oduzima sa novog skladiste
                            kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), cbSkladiste.SelectedValue.ToString(), dg(i, "kolicina"), "1", "-");
                            SQL.SQLroba_prodaja.UpdateRows(cbSkladiste.SelectedValue.ToString(), kol, dg(i, "sifra"));
                        }
                    }

                    row = DTsend.NewRow();
                    row["kolicina"] = dg(i, "kolicina");
                    row["vpc"] = dg(i, "vpc");
                    row["nbc"] = dg(i, "nc");
                    row["godina_otpremnice"] = nmGodinaOtpremnice.Value.ToString();
                    row["broj_otpremnice"] = txtBrojOtpremnice.Text;
                    row["porez"] = dg(i, "porez");
                    row["sifra_robe"] = dg(i, "sifra");
                    row["rabat"] = dg(i, "rabat");
                    row["oduzmi"] = dg(i, "oduzmi");
                    row["id_stavka"] = dg(i, "id_stavka");
                    row["id_skladiste"] = cbSkladiste.SelectedValue.ToString();
                    row["id_otpremnice"] = DTotpremnice.Rows[0]["id_otpremnica"].ToString();
                    row["naplaceno_fakturom"] = '0';
                    DTsend.Rows.Add(row);
                }
                else
                {
                    if (dg(i, "oduzmi") == "DA")
                    {
                        kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), cbSkladiste.SelectedValue.ToString(), dg(i, "kolicina"), "1", "-");
                        SQL.SQLroba_prodaja.UpdateRows(cbSkladiste.SelectedValue.ToString(), kol, dg(i, "sifra"));
                    }

                    row1 = DTsend1.NewRow();
                    row1["kolicina"] = dg(i, "kolicina");
                    row1["vpc"] = dg(i, "vpc");
                    row1["nbc"] = dg(i, "nc");
                    row1["godina_otpremnice"] = nmGodinaOtpremnice.Value.ToString();
                    row1["broj_otpremnice"] = txtBrojOtpremnice.Text;
                    row1["porez"] = dg(i, "porez");
                    row1["sifra_robe"] = dg(i, "sifra");
                    row1["rabat"] = dg(i, "rabat");
                    row1["oduzmi"] = dg(i, "oduzmi");
                    row1["id_skladiste"] = cbSkladiste.SelectedValue.ToString();
                    row1["id_otpremnice"] = DTotpremnice.Rows[0]["id_otpremnica"].ToString();
                    row1["naplaceno_fakturom"] = '0';
                    DTsend1.Rows.Add(row1);
                }
            }
            provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Uređivanje otpremnice na skladiste br." + txtBrojOtpremnice.Text + "')"));
            Util.AktivnostZaposlenika.SpremiAktivnost(dgw, cbSkladiste.SelectedValue.ToString(), "Otpremnica", txtBrojOtpremnice.Text, true);
            SQL.SQLotpremnica.UpdateStavke(DTsend);
            SQL.SQLotpremnica.InsertStavke(DTsend1);
            //MessageBox.Show("Spremljeno.");
            edit = false;
            EnableDisable(false);
            deleteFields();
            btnSveFakture.Enabled = true;
        }

        private void txtBrojOtpremnice_KeyDown_1(object sender, KeyEventArgs e)
        {
            //string a = "Epson Printer SX 235W ima još";
            //if (a.Length > 15)
            //{
            //    int s = a.LastIndexOf(" ", 0, 2);
            //    string prvi_dio = a.Substring(1, s);
            //    string drugi_dio = a.Substring(s, a.Length);
            //}

            if (e.KeyCode == Keys.Enter)
            {
                string sql = "SELECT broj_otpremnice FROM otpremnice WHERE godina_otpremnice='" + nmGodinaOtpremnice.Value.ToString() +
                    "' AND broj_otpremnice='" + txtBrojOtpremnice.Text +
                    "' AND id_skladiste='" + cbSkladiste.SelectedValue.ToString() + "'";
                DataTable DT = classSQL.select(sql, "otpremnice").Tables[0];
                deleteFields();
                if (DT.Rows.Count == 0)
                {
                    if ((Convert.ToInt16(brojOtpremnice()) - 1).ToString() == txtBrojOtpremnice.Text.Trim())
                    {
                        edit = false;
                        EnableDisable(true);
                        //txtBrojOtpremnice.Text = brojOtpremnice();
                        //txtSifraOdrediste.Select();
                        txtBrojOtpremnice.ReadOnly = true;
                        nmGodinaOtpremnice.ReadOnly = true;
                        ControlDisableEnable(0, 1, 1, 0, 0);

                        cbSkladiste.Select();
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                        this.ActiveControl = this.txtBrojOtpremnice;
                        //txtBrojOtpremnice.Focus();
                        txtBrojOtpremnice.SelectAll();
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    broj_otpremnice_edit = txtBrojOtpremnice.Text;
                    skladiste_edit = cbSkladiste.SelectedValue.ToString();
                    godina_edit = nmGodinaOtpremnice.Value.ToString();
                    fillOtpremnice(skladiste_edit, nmGodinaOtpremnice.Value.ToString());
                    EnableDisable(true);
                    edit = true;
                    btnDeleteAllFaktura.Enabled = true;
                    //txtSifraOdrediste.Select();
                    txtBrojOtpremnice.ReadOnly = true;
                    nmGodinaOtpremnice.ReadOnly = true;
                    ControlDisableEnable(0, 1, 1, 0, 0);

                    cbSkladiste.Select();
                }
            }
        }

        //private void nuGodinaPonude_KeyDown_1(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        DataTable DT = classSQL.select("SELECT broj_ponude FROM ponude WHERE godina_ponude='" + nmGodinaOtpremnice.Value.ToString() + "' AND broj_ponude='" + txtBrojPonude.Text + "'", "ponude").Tables[0];
        //        deleteFields();
        //        if (DT.Rows.Count == 0)
        //        {
        //            MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
        //        }
        //        else if (DT.Rows.Count == 1)
        //        {
        //            EnableDisable(true);
        //            edit = true;
        //            btnDeleteAllFaktura.Enabled = true;
        //            txtBrojPonude.Text = DT.Rows[0][0].ToString();
        //            fillPonude(DT.Rows[0][0].ToString());
        //        }
        //    }
        //}

        //private void fillPonude(string broj)
        //{
        //    //fill header
        //    DTotpremnice = classSQL.select("SELECT * FROM ponude WHERE broj_ponude = '" + broj + "'", "ponude").Tables[0];

        //    txtSifraPosPatner.Text = DTotpremnice.Rows[0]["id_fakturirati"].ToString();
        //    try { txtNazivPosPartner.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["id_fakturirati"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); } catch (Exception) { }
        //    txtSifraOdrediste.Text = DTotpremnice.Rows[0]["id_odrediste"].ToString();
        //    try { txtPartnerNaziv.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DTotpremnice.Rows[0]["id_odrediste"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString(); } catch (Exception) { }

        //    //--------fill otpremnica stavke----------------------------
        //    DataTable dtR = new DataTable();
        //    DSFS = classSQL.select("SELECT * FROM ponude_stavke WHERE broj_ponude = '" + DTotpremnice.Rows[0]["broj_ponude"].ToString() + "'", "ponude_stavke").Tables[0];

        //    for (int i = 0; i < DSFS.Rows.Count; i++)
        //    {
        //        dgw.Rows.Add();
        //        int br = dgw.Rows.Count - 1;
        //        string s = "";

        //        DataTable tblRoba = classSQL.select("SELECT oduzmi FROM roba WHERE sifra='" + DSFS.Rows[i]["sifra"].ToString().Trim() + "'", "roba").Tables[0];

        //        if (tblRoba.Rows[0]["oduzmi"].ToString() == "DA")
        //        {
        //            s = "SELECT roba_prodaja.nc,roba.naziv,roba.jm,roba_prodaja.sifra,roba.oduzmi FROM roba_prodaja INNER JOIN roba ON roba_prodaja.sifra=roba.sifra WHERE roba_prodaja.sifra='" + DSFS.Rows[i]["sifra"].ToString() + "' AND roba_prodaja.id_skladiste='" + DSFS.Rows[i]["id_skladiste"].ToString() + "'";
        //        }
        //        else
        //        {
        //            s = "SELECT roba.nc,roba.naziv,roba.jm,roba.sifra,roba.oduzmi FROM roba WHERE sifra='" + DSFS.Rows[i]["sifra_robe"].ToString() + "'";
        //        }

        //        dtR = classSQL.select(s, "roba_prodaja").Tables[0];

        //        dgw.Rows[br].Cells[0].Value = i + 1;
        //        dgw.Rows[br].Cells["sifra"].Value = dtR.Rows[0]["sifra"].ToString();
        //        dgw.Rows[br].Cells["naziv"].Value = dtR.Rows[0]["naziv"].ToString();
        //        dgw.Rows[br].Cells["jmj"].Value = dtR.Rows[0]["jm"].ToString();
        //        dgw.Rows[br].Cells["oduzmi"].Value = dtR.Rows[0]["oduzmi"].ToString();
        //        dgw.Rows[br].Cells["cijena_bez_pdva"].Value = string.Format("{0:0.00}", DSFS.Rows[i]["vpc"]);
        //        dgw.Rows[br].Cells["kolicina"].Value = DSFS.Rows[i]["kolicina"].ToString();
        //        dgw.Rows[br].Cells["porez"].Value = DSFS.Rows[i]["porez"].ToString();
        //        dgw.Rows[br].Cells["rabat"].Value = DSFS.Rows[i]["rabat"].ToString();
        //        //dgw.Rows[br].Cells["skladiste"].Value = DSFS.Rows[i]["id_skladiste"].ToString();
        //        dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
        //        dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
        //        dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
        //        dgw.Rows[br].Cells["vpc"].Value = string.Format("{0:0.00}", DSFS.Rows[i]["vpc"]);
        //        //dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["mpc"]);
        //        dgw.Rows[br].Cells["nc"].Value = string.Format("{0:0.00}", dtR.Rows[0]["nc"].ToString());
        //        dgw.Rows[br].Cells["id_stavka"].Value = DSFS.Rows[i]["id_stavka"].ToString();

        //        dgw.Select();
        //        dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[3];
        //        ControlDisableEnable(0, 1, 1, 0, 1);
        //        izracun();
        //    }

        //}

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.CurrentCell.ColumnIndex == 6)
            {
                try
                {
                    double mpc = Convert.ToDouble(dgw.CurrentRow.Cells["mpc"].FormattedValue.ToString());
                    double porez = 1 + Convert.ToDouble(dgw.CurrentRow.Cells["porez"].FormattedValue.ToString()) / 100;

                    dgw.CurrentRow.Cells["vpc"].Value = mpc / porez;
                }
                catch (Exception)
                {
                    MessageBox.Show("Koristite enter za sljedeću kolonu.");
                }
            }

            izracun();
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.Rows.Count < 1)
                return;
            dgw.BeginEdit(true);
        }

        private void cbSkladiste_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (load == true)
            {
                if (!edit)
                    txtBrojOtpremnice.Text = brojOtpremnice();
            }
        }

        private void rtbNapomena_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (rtbNapomena.Text == "")
                {
                    e.SuppressKeyPress = true;
                    cbKomercijalist.Select();
                }
            }
        }

        private void rtbNapomena_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (sender.ToString().Length == 0) return;

            //if (sender.ToString()[0] == '\\')
            //{
            //    if ((char)('\\') == (e.KeyChar)) return;
            //}

            //if (sender.ToString()[0] == '\'')
            //{
            //    if ((char)('\'') == (e.KeyChar)) return;
            //}

            //if (sender.ToString()[0] == '\"')
            //{
            //    if ((char)('\"') == (e.KeyChar)) return;
            //}

            //if (!Char.IsNumber(e.KeyChar) && !Char.IsLetter(e.KeyChar) && !Char.IsControl(e.KeyChar) && !Char.IsWhiteSpace(e.KeyChar))
            //{
            //    e.Handled = true;
            //}
        }

        private void chbBezPdVa_CheckedChanged(object sender, EventArgs e)
        {
            if (chbBezPdVa.Checked && chbSNBC.Checked)
            {
                chbSNBC.Checked = false;
                dgw.Columns["mpc"].DisplayIndex = dgw.Columns["nc"].DisplayIndex;
                dgw.Columns["nc"].Visible = false;
                dgw.Columns["mpc"].Visible = true;
                dgw.Columns["nc"].ReadOnly = true;
            }
            if (chbBezPdVa.Checked)
            {
                foreach (DataGridViewRow row in dgw.Rows)
                {
                    row.Cells["porez"].Value = "0";
                    dgw.CurrentCell = dgw.Rows[row.Index].Cells[4];
                    dgw.Rows[row.Index].Selected = true;
                    izracun();
                }
            }
            else
            {
                foreach (DataGridViewRow row in dgw.Rows)
                {
                    DataTable DT = classSQL.select("SELECT * FROM roba WHERE sifra='" + row.Cells["sifra"].Value + "'", "roba").Tables[0];
                    if (DT.Rows.Count > 0)
                    {
                        row.Cells["porez"].Value = DT.Rows[0]["porez"].ToString();
                        dgw.CurrentCell = dgw.Rows[row.Index].Cells[4];
                        dgw.Rows[row.Index].Selected = true;
                        izracun();
                    }
                }
            }

            PaintRows(dgw);
        }

        private void chbSNBC_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chbBezPdVa.Checked && chbSNBC.Checked)
                {
                    chbBezPdVa.Checked = false;
                }

                //if (dgw.Rows.Count > 0) {
                if (chbSNBC.Checked)
                {
                    dgw.Columns["nc"].DisplayIndex = dgw.Columns["mpc"].DisplayIndex;
                    dgw.Columns["nc"].Visible = true;
                    dgw.Columns["nc"].ReadOnly = false;
                    dgw.Columns["mpc"].Visible = false;
                    dgw.Columns["rabat"].ReadOnly = true;
                }
                else if (!chbBezPdVa.Checked)
                {
                    dgw.Columns["mpc"].DisplayIndex = dgw.Columns["nc"].DisplayIndex;
                    dgw.Columns["nc"].Visible = false;
                    dgw.Columns["mpc"].Visible = true;
                    dgw.Columns["nc"].ReadOnly = true;
                    dgw.Columns["rabat"].ReadOnly = false;
                }

                izracun();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //public string broj_ponude_edit;
        //private void button2_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        frmSvePonude objForm3 = new frmSvePonude();
        //        objForm3.btnUrediSifru.Text = "Koristi ponudu";
        //        objForm3.MainForm2 = this;
        //        objForm3.sifra_ponude = "";
        //        broj_ponude_edit = null;
        //        objForm3.ShowDialog();
        //        if (broj_ponude_edit != null)
        //        {
        //            deleteFields();
        //            EnableDisable(true);
        //            edit = false;
        //            btnDeleteAllFaktura.Enabled = true;
        //            txtBrojPonude.Text = broj_ponude_edit;
        //            fillPonude(broj_ponude_edit);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}