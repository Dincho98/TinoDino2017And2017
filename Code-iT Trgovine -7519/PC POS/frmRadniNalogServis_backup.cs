//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;
//using System.Drawing.Drawing2D;

//namespace PCPOS
//{
//    public partial class frmRadniNalogSerivs_backup : Form
//    {
//        public string broj_rn_edit { get; set; }
//        public frmRadniNalogSerivs_backup()
//        {
//            InitializeComponent();
//        }
//        DataTable DTRoba = new DataTable();
//        DataSet DS_Skladiste = new DataSet();
//        DataSet DS_ZiroRacun = new DataSet();
//        DataSet DS_Zaposlenik = new DataSet();
//        DataSet DSValuta = new DataSet();
//        DataSet DSIzjava = new DataSet();
//        DataSet DSnazivPlacanja = new DataSet();
//        DataSet DSvd = new DataSet();
//        DataTable DTpromocije1;
//        DataTable DTOtprema = new DataTable();
//        DataTable DSrns = new DataTable();
//        DataTable DSFS = new DataTable();
//        DataTable DTpostavke = new DataTable();
//        double u = 0;
//        public frmMenu MainForm { get; set; }
//        bool edit = false;

//        protected override void WndProc(ref Message m)
//        {
//            const int WM_SYSCOMMAND = 0x0112;
//            const int SC_RESTORE = 0xF120;

//            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_RESTORE)
//            {
//                this.Dock = DockStyle.None;
//            }

//            base.WndProc(ref m);
//        }

//        private void frmPonude_Load(object sender, EventArgs e)
//        {
//            MyDataGrid.MainForm = this;

//            this.Paint += new PaintEventHandler(Form1_Paint);

//            PaintRows(dgw);
//            DTpostavke = classSQL.select_settings("SELECT * FROM postavke","postavke").Tables[0];
//            //DTpromocije1 = classSQL.select("SELECT * FROM promocija1", "promocija1").Tables[0];
//            numeric();
//            fillComboBox();
//            ttxBrojRN.Text = brojRN();
//            EnableDisable(false);
//            ControlDisableEnable(1,0,0,1,0);
//            if (broj_rn_edit != null) { fillRN(); }
//            ttxBrojRN.Select();
//            txtSifraNacinPlacanja.Text=cbNacinPlacanja.SelectedValue.ToString();
//            //this.Paint += new PaintEventHandler(Form1_Paint);
//        }

//        private class MyDataGrid : System.Windows.Forms.DataGridView
//        {
//            public static frmRadniNalogSerivs MainForm { get; set; }

//            protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
//            {
//                if (keyData == Keys.Enter)
//                {
//                    MainForm.EnterDGW(MainForm.dgw);
//                    return true;
//                }
//                else if (keyData == Keys.Right)
//                {
//                    MainForm.RightDGW(MainForm.dgw);
//                    return true;
//                }
//                else if (keyData == Keys.Left)
//                {
//                    MainForm.LeftDGW(MainForm.dgw);
//                    return true;
//                }
//                else if (keyData == Keys.Up)
//                {
//                    MainForm.UpDGW(MainForm.dgw);
//                    return true;
//                }
//                else if (keyData == Keys.Down)
//                {
//                    MainForm.DownDGW(MainForm.dgw);
//                    return true;
//                }
//                return base.ProcessCmdKey(ref msg, keyData);
//            }
//        }

//        private void EnterDGW(DataGridView d)
//        {
//            if (d.Rows.Count < 1)
//                return;
//            if (d.CurrentCell.ColumnIndex == 3)
//            {
//                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
//                d.BeginEdit(true);
//            }
//            else if (d.CurrentCell.ColumnIndex == 5)
//            {
//                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
//                d.BeginEdit(true);
//            }
//            else if (d.CurrentCell.ColumnIndex == 7)
//            {
//                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[8];
//                d.BeginEdit(true);
//            }
//            else if (d.CurrentCell.ColumnIndex == 8)
//            {
//                int curent = d.CurrentRow.Index;
//                txtSifra_robe.Text = "";
//                txtSifra_robe.Focus();
//            }
//        }

//        private void LeftDGW(DataGridView d)
//        {
//            if (d.Rows.Count < 1)
//                return;
//            if (d.CurrentCell.ColumnIndex == 2)
//            {
//            }
//            else if (d.CurrentCell.ColumnIndex == 3)
//            {
//                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[2];
//                d.BeginEdit(true);
//            }
//            else if (d.CurrentCell.ColumnIndex == 4)
//            {
//                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
//                d.BeginEdit(true);
//            }
//            else if (d.CurrentCell.ColumnIndex == 5)
//            {
//                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
//                d.BeginEdit(true);
//            }
//            else if (d.CurrentCell.ColumnIndex == 7)
//            {
//                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
//                d.BeginEdit(true);
//            }
//            else if (d.CurrentCell.ColumnIndex == 8)
//            {
//                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
//                d.BeginEdit(true);
//            }
//        }

//        private void RightDGW(DataGridView d)
//        {
//            if (d.Rows.Count < 1)
//                return;
//            if (d.CurrentCell.ColumnIndex == 2)
//            {
//                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
//                d.BeginEdit(true);
//            }
//            else if (d.CurrentCell.ColumnIndex == 3)
//            {
//                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
//                d.BeginEdit(true);
//            }
//            else if (d.CurrentCell.ColumnIndex == 4)
//            {
//                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
//                d.BeginEdit(true);
//            }
//            else if (d.CurrentCell.ColumnIndex == 5)
//            {
//                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
//                d.BeginEdit(true);
//            }
//            else if (d.CurrentCell.ColumnIndex == 7)
//            {
//                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[8];
//                d.BeginEdit(true);
//            }
//            else if (d.CurrentCell.ColumnIndex == 8)
//            {
//                int curent = d.CurrentRow.Index;
//                txtSifra_robe.Text = "";
//                txtSifra_robe.Focus();
//            }
//        }

//        private void UpDGW(DataGridView d)
//        {
//            if (d.Rows.Count < 1)
//                return;
//            int curent = d.CurrentRow.Index;
//            if (d.CurrentCell.ColumnIndex == 3)
//            {
//            }
//            else if (curent == 0)
//            {
//                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[2];
//                d.BeginEdit(true);
//            }
//            else
//            {
//                d.CurrentCell = dgw.Rows[d.CurrentRow.Index-1].Cells[2];
//                d.BeginEdit(true);
//            }
//        }

//        private void DownDGW(DataGridView d)
//        {
//            if (d.Rows.Count < 1)
//                return;
//            int curent = d.CurrentRow.Index;
//            if (d.CurrentCell.ColumnIndex == 3)
//            {
//                SendKeys.Send("{F4}");
//            }
//            else if (curent == d.RowCount - 1)
//            {
//                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[2];
//                d.BeginEdit(true);
//            }
//            else
//            {
//                d.CurrentCell = dgw.Rows[d.CurrentRow.Index+1].Cells[2];
//                d.BeginEdit(true);
//            }
//        }

//        private void TRENUTNI_Enter(object sender, EventArgs e)
//        {
//            if (sender is TextBox)
//            {
//                TextBox txt = ((TextBox)sender);
//                txt.BackColor = Color.Khaki;
//            }
//            else if (sender is ComboBox)
//            {
//                ComboBox txt = ((ComboBox)sender);
//                txt.BackColor = Color.Khaki;
//            }
//            else if (sender is DateTimePicker)
//            {
//                DateTimePicker control = ((DateTimePicker)sender);
//                control.BackColor = Color.Khaki;
//            }
//            else if (sender is NumericUpDown)
//            {
//                NumericUpDown control = ((NumericUpDown)sender);
//                control.BackColor = Color.Khaki;
//            }
//        }

//        private void NAPUSTENI_Leave(object sender, EventArgs e)
//        {
//            if (sender is TextBox)
//            {
//                TextBox txt = ((TextBox)sender);
//                txt.BackColor = Color.White;
//            }
//            else if (sender is ComboBox)
//            {
//                ComboBox txt = ((ComboBox)sender);
//                txt.BackColor = Color.White;
//                //txt.DropDownStyle = ComboBoxStyle.DropDownList;
//            }
//            else if (sender is DateTimePicker)
//            {
//                DateTimePicker control = ((DateTimePicker)sender);
//                control.BackColor = Color.White;
//            }
//            else if (sender is NumericUpDown)
//            {
//                NumericUpDown control = ((NumericUpDown)sender);
//                control.BackColor = Color.White;
//            }
//        }

//        private void ControlDisableEnable(int novi, int odustani, int spremi, int sve, int delAll)
//        {
//            if (novi == 0)
//            {
//                btnNoviUnos.Enabled = false;
//            }
//            else if (novi == 1)
//            {
//                btnNoviUnos.Enabled = true;
//            }

//            if (odustani == 0)
//            {
//                btnOdustani.Enabled = false;
//            }
//            else if (odustani == 1)
//            {
//                btnOdustani.Enabled = true;
//            }

//            if (spremi == 0)
//            {
//                btnSpremi.Enabled = false;
//            }
//            else if (spremi == 1)
//            {
//                btnSpremi.Enabled = true;
//            }

//            if (sve == 0)
//            {
//                btnSveFakture.Enabled = false;
//            }
//            else if (sve == 1)
//            {
//                btnSveFakture.Enabled = true;
//            }

//            if (delAll == 0)
//            {
//                btnDeleteAllFaktura.Enabled = false;
//            }
//            else if (delAll == 1)
//            {
//                btnDeleteAllFaktura.Enabled = true;
//            }
//        }

//        void Form1_Paint(object sender, PaintEventArgs e)
//        {
//            Graphics c = e.Graphics;
//            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
//            c.FillRectangle(bG, 0, 0, Width, Height);
//        }

//        private string brojRN()
//        {
//            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj AS bigint)) FROM radni_nalog_servis", "radni_nalog_servis").Tables[0];
//            if (DSbr.Rows[0][0].ToString() != "")
//            {
//                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
//            }
//            else
//            {
//                return "1";
//            }
//        }

//        private void fillComboBox()
//        {
//            //fill ziroracun
//            DS_ZiroRacun = classSQL.select("SELECT * FROM ziro_racun", "ziro_racun");
//            cbZiroRacun.DataSource = DS_ZiroRacun.Tables[0];
//            cbZiroRacun.DisplayMember = "ziroracun";
//            cbZiroRacun.ValueMember = "id_ziroracun";
//            cbZiroRacun.SelectedValue = "1";
//            //fill otprema
//            DTOtprema = classSQL.select("SELECT * FROM otprema", "otprema").Tables[0];
//            cbOtprema.DataSource = DTOtprema;
//            cbOtprema.DisplayMember = "naziv";
//            cbOtprema.ValueMember = "id_otprema";

//            //fill komercijalist
//            DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
//            cbKomercijalist.DataSource = DS_Zaposlenik.Tables[0];
//            cbKomercijalist.DisplayMember = "IME";
//            cbKomercijalist.ValueMember = "id_zaposlenik";

//            //fill izjava
//            DSIzjava = classSQL.select("SELECT * FROM izjava ORDER BY id_izjava", "izjava");
//            cbIzjava.DataSource = DSIzjava.Tables[0];
//            cbIzjava.DisplayMember = "izjava";
//            cbIzjava.ValueMember = "id_izjava";

//            //fill vrsta dokumenta
//            DSvd = classSQL.select("SELECT * FROM fakture_vd  WHERE grupa = 'rn' ORDER BY id_vd", "fakture_vd");
//            cbVD.DataSource = DSvd.Tables[0];
//            cbVD.DisplayMember = "vd";
//            cbVD.ValueMember = "id_vd";

//            //fill nacin_placanja
//            DSnazivPlacanja = classSQL.select("SELECT * FROM nacin_placanja", "nacin_placanja");
//            cbNacinPlacanja.DataSource = DSnazivPlacanja.Tables[0];
//            cbNacinPlacanja.DisplayMember = "naziv_placanja";
//            cbNacinPlacanja.ValueMember = "id_placanje";
//            cbNacinPlacanja.SelectedValue = 3;
//            txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();

//            //DS Valuta
//            DSValuta = classSQL.select("SELECT * FROM valute", "valute");
//            cbValuta.DataSource = DSValuta.Tables[0];
//            cbValuta.DisplayMember = "ime_valute";
//            cbValuta.ValueMember = "id_valuta";
//            cbValuta.SelectedValue = 5;
//            txtTecaj.DataBindings.Add("Text", DSValuta.Tables[0], "tecaj");
//            txtTecaj.Text = "1";

//            DataTable DTSK = new DataTable("Roba");
//            DTSK.Columns.Add("id_skladiste", typeof(string));
//            DTSK.Columns.Add("skladiste", typeof(string));

//            DS_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste");
//            for (int i = 0; i < DS_Skladiste.Tables[0].Rows.Count; i++)
//            {
//                DTSK.Rows.Add(DS_Skladiste.Tables[0].Rows[i]["id_skladiste"], DS_Skladiste.Tables[0].Rows[i]["skladiste"]);
//            }

//            skladiste.DataSource = DTSK;
//            skladiste.DataPropertyName = "skladiste";
//            skladiste.DisplayMember = "skladiste";
//            skladiste.HeaderText = "Skladište";
//            skladiste.Name = "skladiste";
//            skladiste.Resizable = System.Windows.Forms.DataGridViewTriState.True;
//            skladiste.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
//            skladiste.ValueMember = "id_skladiste";

//            //fill tko je prijavljen
//            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();

//            DS_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste");
//        }

//        private void numeric()
//        {
//            nmGodina.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
//            nmGodina.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
//            nmGodina.Value = Convert.ToInt16(DateTime.Now.Year.ToString());;
//        }

//        private void EnableDisable(bool x)
//        {
//            cbVD.Enabled = x;
//            txtSifraOdrediste.Enabled = x;
//            txtSifraFakturirati.Enabled = x;
//            txtPartnerNaziv.Enabled = x;
//            txtPartnerNaziv1.Enabled = x;
//            txtSifraNacinPlacanja.Enabled = x;
//            txtModel.Enabled = x;
//            txtSifraNarKupca.Enabled = x;
//            txtNarKupca1.Enabled = x;
//            cbOtprema.Enabled = x;
//            rtbNapomena.Enabled = x;
//            txtSifra_robe.Enabled = x;
//            btnPartner.Enabled = x;
//            btnPartner1.Enabled = x;
//            dtpDatum.Enabled = x;
//            dtpDanaValuta.Enabled = x;
//            cbIzjava.Enabled = x;
//            cbKomercijalist.Enabled = x;
//            cbNacinPlacanja.Enabled = x;
//            cbZiroRacun.Enabled = x;
//            cbValuta.Enabled = x;
//            cbNarKupca.Enabled = x;
//            btnObrisi.Enabled = x;
//            btnOpenRoba.Enabled = x;
//            btnNarKupca.Enabled = x;

//            ttxBrojRN.ReadOnly = x;
//            nmGodina.ReadOnly = x;
//        }

//        private void deleteFields()
//        {
//            txtSifraOdrediste.Text = "";
//            txtSifraFakturirati.Text = "";
//            txtPartnerNaziv.Text = "";
//            txtPartnerNaziv1.Text = "";
//            txtSifraNacinPlacanja.Text = "";
//            txtModel.Text = "";
//            txtSifraNarKupca.Text = "";
//            txtNarKupca1.Text = "";
//            rtbNapomena.Text = "";
//            txtSifra_robe.Text = "";

//            dgw.Rows.Clear();
//        }

//        private void btnIzlaz_Click(object sender, EventArgs e)
//        {
//            this.Close();
//        }

//        private void provjera_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (sender.ToString().Length == 0) return;

//            if (sender.ToString()[0] == '+')
//            {
//                if ((char)('+') == (e.KeyChar)) return;
//            }

//            if (sender.ToString()[0] == '-')
//            {
//                if ((char)('-') == (e.KeyChar)) return;
//            }

//            if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
//            {
//                e.Handled = true;
//            }
//        }

//        #region ON_KEY_DOWN_REGION
//        private void txtSifraOdrediste_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//            {
//                e.SuppressKeyPress = true;

//                if (txtSifraOdrediste.Text == "")
//                {
//                    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
//                    partnerTrazi.ShowDialog();
//                    if (Properties.Settings.Default.id_partner != "")
//                    {
//                        DataSet partner = new DataSet();
//                        partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
//                        if (partner.Tables[0].Rows.Count > 0)
//                        {
//                            txtSifraOdrediste.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
//                            txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
//                            txtSifraFakturirati.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
//                            txtPartnerNaziv1.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString(); cbVD.Select();
//                            txtSifraFakturirati.Select();
//                        }
//                        else
//                        {
//                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
//                            txtSifraOdrediste.Select();
//                        }
//                    }
//                    else
//                    {
//                        txtSifraOdrediste.Select();
//                        return;
//                    }
//                }

//                string Str = txtSifraOdrediste.Text.Trim();
//                double Num;
//                bool isNum = double.TryParse(Str, out Num);
//                if (!isNum)
//                {
//                    txtSifraOdrediste.Text = "0";
//                }

//                DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraOdrediste.Text + "'", "partners").Tables[0];
//                if (DSpar.Rows.Count > 0)
//                {
//                    txtPartnerNaziv.Text = DSpar.Rows[0][0].ToString();
//                    txtPartnerNaziv1.Text = DSpar.Rows[0][0].ToString();
//                    txtSifraFakturirati.Text = txtSifraOdrediste.Text;
//                    txtSifraFakturirati.Select();
//                }
//                else
//                {
//                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
//                }
//            }
//        }

//        private void txtSifraFakturirati_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//            {
//                e.SuppressKeyPress = true;

//                string Str = txtSifraFakturirati.Text.Trim();
//                double Num;
//                bool isNum = double.TryParse(Str, out Num);
//                if (!isNum)
//                {
//                    txtSifraFakturirati.Text = "0";
//                }

//                DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraFakturirati.Text + "'", "partners").Tables[0];
//                if (DSpar.Rows.Count > 0)
//                {
//                    txtPartnerNaziv1.Text = DSpar.Rows[0][0].ToString();
//                    cbVD.Select();
//                }
//                else
//                {
//                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
//                }
//            }
//        }

//        private void cbVD_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//            {
//                e.SuppressKeyPress = true;
//                dtpDatum.Select();
//            }
//        }

//        private void dtpDatum_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//            {
//                e.SuppressKeyPress = true;
//                txtDana.Select();
//            }
//        }

//        private void txtDana_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//            {
//                e.SuppressKeyPress = true;
//                if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
//                {
//                    (sender as TextBox).Text = "0";
//                    dtpDanaValuta.Select();
//                }

//                try
//                {
//                    DateTime dvo = dtpDanaValuta.Value;
//                    dtpDanaValuta.Value = dvo.AddDays(Convert.ToInt16(txtDana.Text)); ;
//                    dtpDanaValuta.Select();
//                }
//                catch (Exception)
//                {
//                }

//            }
//        }

//        private void dtpDanaValuta_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//            {
//                e.SuppressKeyPress = true;
//                try
//                {
//                    DateTime dt1 = dtpDanaValuta.Value;
//                    DateTime dt2 = dtpDanaValuta.Value;
//                    TimeSpan ts = dt1 - dt2;
//                    txtDana.Text = (Convert.ToInt16(ts.Days.ToString()) + 1).ToString();
//                    cbIzjava.Select();
//                }
//                catch (Exception)
//                {
//                }
//            }
//        }

//        private void cbIzjava_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//            {
//                e.SuppressKeyPress = true;
//                rtbNapomena.Text += cbIzjava.Text;
//                cbKomercijalist.Select();
//            }
//        }

//        private void cbKomercijalist_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//            {
//                e.SuppressKeyPress = true;
//                txtModel.Select();
//            }
//        }

//        private void txtIzradio_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//            {
//                e.SuppressKeyPress = true;
//                txtModel.Select();
//            }
//        }

//        private void txtModel_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//            {
//                e.SuppressKeyPress = true;
//                txtSifraNacinPlacanja.Select();
//            }
//        }

//        private void txtSifraNacinPlacanja_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//            {
//                try
//                {
//                    e.SuppressKeyPress = true;
//                    DataRow[] dataROW = DSnazivPlacanja.Tables[0].Select("id_placanje = " + txtSifraNacinPlacanja.Text);
//                    cbNacinPlacanja.SelectedValue = dataROW[0]["id_placanje"].ToString();
//                    cbNacinPlacanja.Select();
//                }
//                catch (Exception)
//                {
//                    MessageBox.Show("Krivi unos.", "Greška");
//                }
//            }
//        }

//        private void cbNacinPlacanja_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//            {
//                e.SuppressKeyPress = true;
//                cbZiroRacun.Select();
//            }
//        }
//        private void cbNacinPlacanja_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();
//        }

//        private void cbZiroRacun_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//            {
//                e.SuppressKeyPress = true;
//                cbValuta.Select();
//            }
//        }

//        private void cbValuta_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//            {
//                e.SuppressKeyPress = true;
//                txtSifraNarKupca.Select();
//            }
//        }

//        private void txtTecaj_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//            {
//                e.SuppressKeyPress = true;
//                txtSifraNarKupca.Select();
//            }
//        }

//        private void txtSifraNarKupca_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//            {
//                e.SuppressKeyPress = true;
//                cbNarKupca.Select();
//            }
//        }

//        private void cbNarKupca_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//            {
//                e.SuppressKeyPress = true;
//                txtNarKupca1.Select();
//            }
//        }

//        private void txtNarKupca1_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//            {
//                e.SuppressKeyPress = true;
//                cbOtprema.Select();
//            }
//        }

//        private void txtOtprema_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//            {
//                e.SuppressKeyPress = true;
//                rtbNapomena.Select();
//            }
//        }

//        private void rtbNapomena_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//            {
//                e.SuppressKeyPress = true;
//                txtSifra_robe.Select();
//            }
//        }
//        private void txtSifra_robe_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//            {
//                e.SuppressKeyPress = true;

//                if (txtSifra_robe.Text.Length > 2)
//                {
//                    //for (int y = 0; y < dgw.Rows.Count; y++)
//                    //{
//                    //    if (txtSifra_robe.Text == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString())
//                    //    {
//                    //        MessageBox.Show("Artikl ili usluga već postoje u ovoj ponudi.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    //        return;
//                    //    }
//                    //}

//                    if (DTpromocije1.Rows[0]["aktivnost"].ToString() == "DA" && txtSifra_robe.Text.Substring(0, 3) == "000")
//                    {
//                        double uk;
//                        double popust;
//                        DataTable DTrp = classSQL.select("SELECT * FROM racun_popust_kod_sljedece_kupnje WHERE broj_racuna='" + txtSifra_robe.Text.Substring(3, txtSifra_robe.Text.Length - 3) + "' AND dokumenat='FA'", "racun_popust_kod_sljedece_kupnje").Tables[0];

//                        if (DTrp.Rows.Count == 0)
//                        {
//                            MessageBox.Show("Ovaj popust nije valjan.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                            return;
//                        }

//                        if (DTrp.Rows[0]["koristeno"].ToString() == "DA")
//                        {
//                            MessageBox.Show("Ovaj popust je već iskorišten.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                            return;
//                        }

//                        DateTime dateFromPopust = Convert.ToDateTime(DTrp.Rows[0]["datum"].ToString()).AddDays(Convert.ToDouble(DTpromocije1.Rows[0]["traje_do"].ToString()));

//                        if (dateFromPopust < DateTime.Now)
//                        {
//                            MessageBox.Show("Ovom popustu je istekao datum.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                            return;
//                        }

//                        uk = Convert.ToDouble(DTrp.Rows[0]["ukupno"].ToString());
//                        popust = Convert.ToDouble(DTrp.Rows[0]["popust"].ToString());
//                        uk = uk * 3 / 100;

//                        if ((Convert.ToDouble(u.ToString()) - uk) < 0)
//                        {
//                            MessageBox.Show("Popust je veći od ukupnog računa.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                            return;
//                        }

//                        dgw.Rows.Add(
//                                    dgw.RowCount - 1,
//                                    txtSifra_robe.Text,
//                                    "Popust sa prethodnog računa",
//                                    "1",
//                                    "kn",
//                                    1,
//                                    DTpostavke.Rows[0]["pdv"].ToString(),
//                                    Math.Round(uk * -1, 2).ToString("#0.00"),
//                                    "0",
//                                    "0",
//                                    Math.Round(uk * -1 / (1 + Convert.ToDouble(DTpostavke.Rows[0]["pdv"].ToString()) / 100), 2).ToString("#0.00"),
//                                    Math.Round(uk * -1 / (1 + Convert.ToDouble(DTpostavke.Rows[0]["pdv"].ToString()) / 100), 2).ToString("#0.00"),
//                                    Math.Round(uk * -1, 2).ToString("#0.00"),
//                                    Math.Round(uk * -1 / (1 + Convert.ToDouble(DTpostavke.Rows[0]["pdv"].ToString()) / 100), 2).ToString("#0.00"),
//                                    Math.Round(uk * -1, 2).ToString("#0.00"),
//                                    "",
//                                    "",
//                                    ""
//                                );

//                        int br = dgw.Rows.Count - 1;
//                        dgw.ClearSelection();
//                        izracun();
//                        PaintRows(dgw);
//                        dgw.ClearSelection();
//                        txtSifra_robe.Text = "";
//                        txtSifra_robe.Select();
//                        return;

//                    }

//                }

//                string sql = "SELECT * FROM roba WHERE sifra='" + txtSifra_robe.Text + "'";

//                DTRoba = classSQL.select(sql, "roba").Tables[0];
//                if (DTRoba.Rows.Count > 0)
//                {
//                    txtSifra_robe.BackColor = Color.White;
//                    SetRoba();
//                    //ttxBrojPonude.Enabled = false;
//                    //nmGodinaPonude.Enabled = false;
//                    dgw.Rows[dgw.Rows.Count - 1].Cells["skladiste"].Selected = true;
//                    cbValuta.Enabled = false;
//                }
//                else
//                {
//                    MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        #endregion

//        private void btnNoviUnos_Click(object sender, EventArgs e)
//        {
//            edit = false;
//            EnableDisable(true);
//            deleteFields();
//            btnSveFakture.Enabled = false;
//            ttxBrojRN.Text = brojRN();
//            btnDeleteAllFaktura.Enabled = false;
//            ttxBrojRN.ReadOnly = true;
//            nmGodina.ReadOnly = true;
//            ControlDisableEnable(0,1,1,0,1);
//            txtSifraOdrediste.Select();
//            txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();
//        }
//        private void SetRoba()
//        {
//            dgw.Rows.Add();
//            int br = dgw.Rows.Count - 1;

//            dgw.Rows[br].Cells[0].Value = "1";
//            dgw.Rows[br].Cells["sifra"].Value = DTRoba.Rows[0]["sifra"].ToString();
//            dgw.Rows[br].Cells["naziv"].Value = DTRoba.Rows[0]["naziv"].ToString();
//            dgw.Rows[br].Cells["jmj"].Value = DTRoba.Rows[0]["jm"].ToString();
//            dgw.Rows[br].Cells["cijena_bez_pdva"].Value = String.Format("{0:0.00}", DTRoba.Rows[0]["vpc"]);
//            dgw.Rows[br].Cells["kolicina"].Value = "1";
//            dgw.Rows[br].Cells["porez"].Value = DTRoba.Rows[0]["porez"].ToString();
//            dgw.Rows[br].Cells["rabat"].Value = "0,00";
//            dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
//            dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
//            dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
//            dgw.Rows[br].Cells["vpc"].Value = String.Format("{0:0.00}", DTRoba.Rows[0]["vpc"]);
//            dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", DTRoba.Rows[0]["mpc"]);
//            dgw.Rows[br].Cells["nc"].Value = String.Format("{0:0.00}", DTRoba.Rows[0]["nc"]);
//            dgw.Rows[br].Cells["oduzmi"].Value = DTRoba.Rows[0]["oduzmi"].ToString();
//            dgw.Rows[br].Cells["skladiste"].Value = DTpostavke.Rows[0]["default_skladiste"].ToString();

//            dgw.Select();
//            dgw.CurrentCell = dgw.Rows[br].Cells[3];
//            dgw.BeginEdit(true);

//            izracun();
//            PaintRows(dgw);
//        }

//        //private void dgw_CellValidated(object sender, DataGridViewCellEventArgs e)
//        //{
//        //    int row = dgw.CurrentCell.RowIndex;
//        //    if (dgw.CurrentCell.ColumnIndex == 3)
//        //    {
//        //        SetCijenaSkladiste();
//        //    }

//        //    else if (dgw.CurrentCell.ColumnIndex == 9)
//        //    {
//        //        if (dgw.CurrentRow.Cells["skladiste"].Value == null)
//        //        {
//        //            MessageBox.Show("Niste odabrali skladište", "Greška");
//        //            return;
//        //        }

//        //        dgw.CurrentCell.Selected = false;
//        //        txtSifra_robe.Text = "";
//        //        txtSifra_robe.BackColor = Color.Silver;
//        //        txtSifra_robe.Select();
//        //    }
//        //    izracun();
//        //}

//        private void SetCijenaSkladiste()
//        {
//            if (dgw.CurrentRow.Cells["skladiste"].Value != null)
//            {
//                DataSet dsRobaProdaja = new DataSet();
//                dsRobaProdaja = classSQL.select("SELECT * FROM roba_prodaja WHERE id_skladiste='" + dgw.CurrentRow.Cells["skladiste"].Value + "' AND sifra='" + dgw.CurrentRow.Cells["sifra"].FormattedValue.ToString() + "'", "roba_prodaja");
//                if (dsRobaProdaja.Tables[0].Rows.Count > 0)
//                {
//                    if (Convert.ToDouble(dsRobaProdaja.Tables[0].Rows[0]["kolicina"].ToString()) > 0)
//                    {
//                        dgw.CurrentRow.Cells["porez"].Value = dsRobaProdaja.Tables[0].Rows[0]["porez"].ToString();
//                        dgw.CurrentRow.Cells["vpc"].Value = String.Format("{0:0.00}", dsRobaProdaja.Tables[0].Rows[0]["vpc"]);
//                        lblNaDan.ForeColor = Color.Green;
//                        lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima " + dsRobaProdaja.Tables[0].Rows[0]["kolicina"].ToString() + " " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
//                    }
//                    else
//                    {
//                        dgw.CurrentRow.Cells["porez"].Value = dsRobaProdaja.Tables[0].Rows[0]["porez"].ToString();
//                        dgw.CurrentRow.Cells["vpc"].Value = String.Format("{0:0.00}", dsRobaProdaja.Tables[0].Rows[0]["vpc"]);
//                        lblNaDan.ForeColor = Color.Red;
//                        lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima " + dsRobaProdaja.Tables[0].Rows[0]["kolicina"].ToString() + " " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
//                    }
//                }
//                else
//                {
//                    lblNaDan.ForeColor = Color.Red;
//                    lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima 0,00 " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
//                }
//            }
//        }

//        public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
//        {
//            Double result;
//            return Double.TryParse(val, NumberStyle,
//                System.Globalization.CultureInfo.CurrentCulture, out result);
//        }

//        double SveUkupno = 0;
//        private void izracun()
//        {
//            if (dgw.RowCount > 0)
//            {
//                int rowBR = dgw.CurrentRow.Index;

//                double dec_parse;
//                if (!Double.TryParse(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString(), out dec_parse))
//                {
//                    dgw.Rows[rowBR].Cells["kolicina"].Value = "1";
//                    MessageBox.Show("Greška kod upisa količine.", "Greška"); return;
//                }

//                if (!Double.TryParse(dgw.Rows[rowBR].Cells["rabat"].FormattedValue.ToString(), out dec_parse))
//                {
//                    dgw.Rows[rowBR].Cells["rabat"].Value = "0";
//                    MessageBox.Show("Greška kod upisa rabata.", "Greška"); return;
//                }

//                if (!Double.TryParse(dgw.Rows[rowBR].Cells["rabat_iznos"].FormattedValue.ToString(), out dec_parse))
//                {
//                    dgw.Rows[rowBR].Cells["rabat_iznos"].Value = "0,00";
//                    MessageBox.Show("Greška kod rabata.", "Greška"); return;
//                }

//                double kol = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString()), 3);
//                double vpc = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["vpc"].FormattedValue.ToString()), 3);
//                double porez = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["porez"].FormattedValue.ToString()), 2);
//                double rbt = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["rabat"].FormattedValue.ToString()), 2);

//                double porez_ukupno = vpc * (porez) / 100;
//                double mpc = porez_ukupno + vpc;
//                double mpc_sa_kolicinom = mpc * kol;
//                double rabat = mpc * rbt / 100;
//                double iznos;

//                dgw.Rows[rowBR].Cells["mpc"].Value = Math.Round(mpc, 2).ToString("#0.00");
//                dgw.Rows[rowBR].Cells["rabat_iznos"].Value = Math.Round(rabat * kol, 2).ToString("#0.00");
//                dgw.Rows[rowBR].Cells["iznos_bez_pdva"].Value = Math.Round((((mpc - rabat) * kol) / (1 + porez / 100)), 2).ToString("#0.00");
//                dgw.Rows[rowBR].Cells["iznos_ukupno"].Value = Math.Round((mpc - rabat) * kol, 2).ToString("#0.00");
//                dgw.Rows[rowBR].Cells["cijena_bez_pdva"].Value = Math.Round(((mpc - rabat) / (1 + porez / 100)), 2).ToString("#0.00");
//                dgw.Rows[rowBR].Cells["kolicina"].Value = kol.ToString("#0.000");
//                dgw.Rows[rowBR].Cells["rabat"].Value = rbt.ToString("#0.00");
//                dgw.Rows[rowBR].Cells["porez"].Value = porez.ToString("#0.00");

//                double B_pdv = 0;
//                u = 0;

//                for (int i = 0; i < dgw.RowCount; i++)
//                {
//                    iznos = Convert.ToDouble(dgw.Rows[i].Cells["iznos_ukupno"].FormattedValue.ToString());
//                    u += Math.Round(iznos, 2);
//                    iznos = Convert.ToDouble(dgw.Rows[i].Cells["iznos_bez_pdva"].FormattedValue.ToString());
//                    B_pdv += Math.Round(iznos, 2);
//                }

//                SveUkupno = u;
//                textBox1.Text = "Ukupno sa PDV-om: " + Math.Round(u, 2).ToString("#0.00");
//                textBox2.Text = "Bez PDV-a: " + Math.Round(B_pdv, 2).ToString("#0.00");
//                textBox3.Text = "PDV: " + Math.Round(Math.Round(u, 2) - Math.Round(B_pdv, 2), 2).ToString("#0.00");
//            }
//        }

//        private void btnOdustani_Click(object sender, EventArgs e)
//        {
//            btnSveFakture.Enabled = true;
//            EnableDisable(false);
//            deleteFields();
//            ttxBrojRN.Text = brojRN();
//            edit = false;
//            btnDeleteAllFaktura.Enabled = false;
//            ttxBrojRN.ReadOnly = false;
//            nmGodina.ReadOnly = false;
//            ControlDisableEnable(1, 0, 0, 1, 0);
//            txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();
//        }

//        private string ReturnSifra(string sifra)
//        {
//            if (sifra.Length > 3)
//            {
//                if (DTpromocije1.Rows[0]["aktivnost"].ToString() == "DA" && sifra.Substring(0, 3) == "000")
//                {
//                    return "00000";
//                }
//            }
//            return sifra;
//        }

//        private void btnSpremi_Click(object sender, EventArgs e)
//        {
//            decimal dec_parse;
//            if (Decimal.TryParse(txtSifraOdrediste.Text, out dec_parse))
//            {
//                txtSifraOdrediste.Text = dec_parse.ToString();
//            }
//            else
//            {
//                MessageBox.Show("Greška kod upisa odredišta.", "Greška"); return;
//            }

//            if (Decimal.TryParse(txtSifraFakturirati.Text, out dec_parse))
//            {
//                txtSifraFakturirati.Text = dec_parse.ToString();
//            }
//            else
//            {
//                MessageBox.Show("Greška kod upisa šifre za fakturirati.", "Greška"); return;
//            }

//            if (cbZiroRacun.Text == "" || cbZiroRacun.ValueMember == null)
//            {
//                MessageBox.Show("Niste odabrali žiro račun.", "Greška");
//                return;
//            }

//            if(dgw.RowCount==0)
//            {
//                MessageBox.Show("Upozorenje.\r\nOvaj servis je prazan.","Greška");
//                return;
//            }

//            for (int i = 0; i < dgw.Rows.Count; i++)
//            {
//                if (dgw.Rows[i].Cells["skladiste"].Value == null & dgw.Rows[i].Cells["oduzmi"].FormattedValue.ToString()=="DA")
//                {
//                    MessageBox.Show("Servis nije spremljena zbog ne odabira skladišta na pojedinim artiklima.", "Greška");
//                    return;
//                }
//            }

//            if (edit == true)
//            {
//                UpdateRN();
//                EnableDisable(false);
//                deleteFields();
//                btnSveFakture.Enabled = true;
//                ControlDisableEnable(1, 0, 0, 1, 0);
//                ttxBrojRN.Text = brojRN();
//                return;
//            }

//            string broj = brojRN();
//            if (broj.Trim() != ttxBrojRN.Text.Trim())
//            {
//                MessageBox.Show("Vaš broj dokumenta već je netko iskoristio.\r\nDodijeljen Vam je novi broj '" + broj + "'.", "Info");
//                ttxBrojRN.Text = broj;
//            }

//            if(txtSifraOdrediste.Text=="" || txtSifraFakturirati.Text=="")
//            {
//                MessageBox.Show("Niste upisali šifru odredišta ili sifru za koga fakturirati.","Greška",MessageBoxButtons.OK,MessageBoxIcon.Error);
//                return;
//            }

//            if(txtSifraNarKupca.Text=="")
//            {
//                txtSifraNarKupca.Text = "0";
//            }

//            string sql = "INSERT INTO radni_nalog_servis (broj,id_odrediste,id_fakturirati,date,vrijedi_do,id_izjava,id_zaposlenik_komercijala," +
//                "id_zaposlenik_izradio,model,id_nacin_placanja,zr,id_valuta,otprema,godina,id_nar_kupca,id_vd,napomena,ukupno) VALUES " +
//                " (" +
//                 " '" + ttxBrojRN.Text + "'," +
//                " '" + txtSifraOdrediste.Text + "'," +
//                " '" + txtSifraFakturirati.Text + "'," +
//                " '" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
//                " '" + dtpDanaValuta.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
//                " '" + cbIzjava.SelectedValue + "'," +
//                " '" + cbKomercijalist.SelectedValue.ToString() + "'," +
//                " '" + Properties.Settings.Default.id_zaposlenik + "'," +
//                " '" + txtModel.Text + "'," +
//                " '" + cbNacinPlacanja.SelectedValue.ToString() + "'," +
//                " '" + cbZiroRacun.SelectedValue.ToString() + "'," +
//                " '" + cbValuta.SelectedValue.ToString() + "'," +
//                " '" + cbOtprema.SelectedValue + "'," +
//                " '" + nmGodina.Value.ToString() + "'," +
//                " '" + txtSifraNarKupca.Text + "'," +
//                " '" + cbVD.SelectedValue + "'," +
//                " '" + rtbNapomena.Text + "'," +
//                " '" + Convert.ToDouble(u.ToString()) + "'" +
//                ")";
//            provjera_sql(classSQL.insert(sql));

//            string sql_stavke="";
//            for (int i = 0; i < dgw.Rows.Count; i++)
//            {
//                ProvjeriDaliPostojiRobaProdaja(dg(i, "sifra"), dgw.Rows[i].Cells[3].Value,i);

//                string vpc = dg(i, "vpc");
//                if (classSQL.remoteConnectionString == "")
//                {
//                    vpc=vpc.Replace(",", ".");
//                }
//                else
//                {
//                    vpc = vpc.Replace(".", ",");
//                }

//                sql_stavke = "INSERT INTO radni_nalog_servis_stavke " +
//                "(sifra,kolicina,vpc,porez,rabat,id_skladiste,naziv,oduzmi,broj)" +
//                "VALUES" +
//                "(" +
//                "'" + ReturnSifra(dg(i, "sifra")) + "'," +
//                "'" + dg(i, "kolicina") + "'," +
//                "'" + vpc.Replace(",", ".") + "'," +
//                "'" + dg(i, "porez") + "'," +
//                "'" + dg(i, "rabat") + "'," +
//                "'" + dgw.Rows[i].Cells[3].Value + "'," +
//                 "'" + dg(i, "naziv") + "'," +
//                 "'" + dg(i, "oduzmi") + "'," +
//                "'" + ttxBrojRN.Text + "'" +
//                ")";
//                provjera_sql(classSQL.insert(sql_stavke));

//                ////update za robu
//                //sql = "UPDATE roba SET " +
//                //                " naziv='" + dg(i, "naziv") + "'," +
//                //                " jm='" + dg(i, "jmj") + "'," +
//                //                " mpc='" + dg(i, "mpc") + "'" +
//                //                " WHERE sifra='" + dg(i, "sifra") + "'";

//                //provjera_sql(classSQL.update(sql));
//            }

//            classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Izrada novog RN servisa br." + ttxBrojRN.Text + "')");

//            if (MessageBox.Show("Spremljeno.\r\nŽelite li ispisati dokument?", "Spremljeno.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
//            {
//                printaj(ttxBrojRN.Text);
//            }

//            edit = false;
//            EnableDisable(false);
//            deleteFields();
//            btnSveFakture.Enabled = true;
//            ControlDisableEnable(1, 0, 0, 1, 0);
//            ttxBrojRN.Text = brojRN();
//        }

//        private void printaj(string broj)
//        {
//            Report.Faktura.repFaktura pr = new Report.Faktura.repFaktura();
//            pr.dokumenat = "RNS";
//            pr.broj_dokumenta = broj;
//            pr.ImeForme = "Radni nalog servis";
//            pr.ShowDialog();
//        }

//        DataSet DSprovjeraRobaProdaja = new DataSet();
//        private void ProvjeriDaliPostojiRobaProdaja(string sif, object skl,int r)
//        {
//            DSprovjeraRobaProdaja = classSQL.select("SELECT * FROM roba_prodaja WHERE sifra='" + sif + "' AND id_skladiste='"+skl.ToString()+"'","roba_prodaja");

//            if(DSprovjeraRobaProdaja.Tables[0].Rows.Count==0)
//            {
//                string sql = "INSERT INTO roba_prodaja (id_skladiste,kolicina,nc,vpc,porez,sifra) VALUES" +
//                    "('" + skl.ToString() + "','0','" + dg(r, "nc").Replace(",", ".") + "','" + dg(r, "vpc").Replace(",", ".") + "','" + dg(r, "porez") + "','" + sif + "')";
//                classSQL.insert(sql);
//            }
//        }

//        private string dg(int row, string cell)
//        {
//            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
//        }

//        private void UpdateRN()
//        {
//            if (txtSifraNarKupca.Text == "")
//            {
//                txtSifraNarKupca.Text = "0";
//            }

//            string sql = "UPDATE radni_nalog_servis SET " +
//                " broj='" + ttxBrojRN.Text + "'," +
//                " id_odrediste='" + txtSifraOdrediste.Text + "'," +
//                " id_fakturirati='" + txtSifraFakturirati.Text + "'," +
//                " date='" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
//                " vrijedi_do='" + dtpDanaValuta.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
//                " id_izjava='" + cbIzjava.SelectedValue.ToString() + "'," +
//                " id_zaposlenik_komercijala='" + cbKomercijalist.SelectedValue.ToString() + "'," +
//                " zr='" + cbZiroRacun.SelectedValue.ToString() + "'," +
//                " id_valuta='" + cbValuta.SelectedValue.ToString() + "'," +
//                " otprema='" + cbOtprema.SelectedValue.ToString() + "'," +
//                " model='" + txtModel.Text + "'," +
//                " id_nar_kupca='" + txtSifraNarKupca.Text + "'," +
//                " id_vd='" + cbVD.SelectedValue + "'," +
//                " godina='" + nmGodina.Value.ToString() + "'," +
//                " ukupno='" + Convert.ToDouble(u.ToString()) + "'," +
//                " napomena='" + rtbNapomena.Text + "'" +
//                " WHERE broj='" + ttxBrojRN.Text + "'";
//            provjera_sql(classSQL.update(sql));

//            for (int i = 0; i < dgw.Rows.Count; i++)
//            {
//                string vpc = dg(i, "vpc");
//                if (classSQL.remoteConnectionString == "")
//                {
//                    vpc = vpc.Replace(",", ".");
//                }
//                else
//                {
//                    vpc = vpc.Replace(".", ",");
//                }

//                if (dgw.Rows[i].Cells["id_stavka"].Value != null)
//                {
//                    sql = "UPDATE radni_nalog_servis_stavke SET " +
//                    " id_skladiste='" + dgw.Rows[i].Cells["skladiste"].Value + "'," +
//                    " kolicina='" + dg(i, "kolicina") + "'," +
//                    " vpc='" + vpc.Replace(",", ".") + "'," +
//                    " porez='" + dg(i, "porez") + "'," +
//                    " naziv='" + dg(i, "naziv") + "'," +
//                    " sifra='" + ReturnSifra(dg(i, "sifra")) + "'," +
//                    " rabat='" + dg(i, "rabat") + "' " +
//                    " WHERE id_stavka='" + dg(i, "id_stavka") + "'";
//                    provjera_sql(classSQL.update(sql));
//                }
//                else
//                {
//                    string sql_stavke = "INSERT INTO radni_nalog_servis_stavke (" +
//                    "id_skladiste,sifra,rabat,broj,vpc,naziv,kolicina,oduzmi,porez)" +
//                    " VALUES (" +
//                    "'" + dgw.Rows[i].Cells["skladiste"].Value + "'," +
//                    "'" + ReturnSifra(dg(i, "sifra")) + "'," +
//                    "'" + dg(i, "rabat") + "'," +
//                    "'" + ttxBrojRN.Text + "'," +
//                    "'" + vpc.Replace(",",".") + "'," +
//                    "'" + dg(i, "naziv") + "'," +
//                    "'" + dg(i, "kolicina") + "'," +
//                    "'" + dg(i, "oduzmi") + "'," +
//                    "'" + dg(i, "porez") + "'" +
//                    ")";
//                    provjera_sql(classSQL.insert(sql_stavke));
//                }

//                ////update za robu
//                //sql = "UPDATE roba SET " +
//                //                " naziv='" + dg(i, "naziv") + "'," +
//                //                " jm='" + dg(i, "jmj") + "'," +
//                //                " mpc='" + dg(i, "mpc") + "'" +
//                //                " WHERE sifra='" + dg(i, "sifra") + "'";

//                provjera_sql(classSQL.update(sql));

//                ////update poreze u roba_prodaja
//                //sql = "SELECT id_roba_prodaja FROM roba_prodaja" +
//                //    " LEFT JOIN roba ON roba_prodaja.sifra=roba.sifra WHERE roba.sifra='" + dg(b, "sifra") + "'";
//                //DataTable sifreRobaProdajaUpdate = classSQL.select(sql, "id_roba_prodaja").Tables[0];
//                //for (int i = 0; i < sifreRobaProdajaUpdate.Rows.Count; i++)
//                //{
//                //    sql = "UPDATE roba_prodaja SET " +
//                //        " porez='" + txtPDV.SelectedValue + "'" +
//                //        " WHERE id_roba_prodaja='" + sifreRobaProdajaUpdate.Rows[i]["id_roba_prodaja"].ToString() + "'";

//                //    provjera_sql(classSQL.update(sql));
//                //}
//            }

//            MessageBox.Show("Spremljeno.");
//            edit = false;
//            EnableDisable(false);
//            deleteFields();
//            btnSpremi.Enabled = false;
//            btnSveFakture.Enabled = true;

//            classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + 3 + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Editiranje RN servisa br." + ttxBrojRN.Text + "')");

//        }

//        private void provjera_sql(string str)
//        {
//            if (str != "")
//            {
//                MessageBox.Show(str);
//            }

//        }

//        private void btnOpenRoba_Click(object sender, EventArgs e)
//        {
//            frmRobaTrazi roba_trazi = new frmRobaTrazi();
//            roba_trazi.ShowDialog();
//            string propertis_sifra = Properties.Settings.Default.id_roba.Trim();
//            if (propertis_sifra != "")
//            {
//                //for (int y = 0; y < dgw.Rows.Count; y++)
//                //{
//                //    if (propertis_sifra == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString())
//                //    {
//                //        MessageBox.Show("Artikl ili usluga već postoje!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                //        return;
//                //    }
//                //}

//                string sql = "SELECT * FROM roba WHERE sifra='" + propertis_sifra + "'";

//                DTRoba = classSQL.select(sql, "roba").Tables[0];
//                if (DTRoba.Rows.Count > 0)
//                {
//                    txtSifra_robe.BackColor = Color.White;
//                    SetRoba();
//                    dgw.Select();
//                    ttxBrojRN.Enabled = false;
//                    nmGodina.Enabled = false;
//                    cbValuta.Enabled = false;
//                }
//                else
//                {
//                    MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        private void btnPartner_Click(object sender, EventArgs e)
//        {
//            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
//            partnerTrazi.ShowDialog();
//            if (Properties.Settings.Default.id_partner != "")
//            {
//                DataSet partner = new DataSet();
//                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
//                if (partner.Tables[0].Rows.Count > 0)
//                {
//                    txtSifraOdrediste.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
//                    txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
//                    txtSifraFakturirati.Text = txtSifraOdrediste.Text;
//                    txtPartnerNaziv1.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
//                    cbVD.Select();
//                }
//                else
//                {
//                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
//                }
//            }
//        }

//        private void btnPartner1_Click(object sender, EventArgs e)
//        {
//            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
//            partnerTrazi.ShowDialog();
//            if (Properties.Settings.Default.id_partner != "")
//            {
//                DataSet partner = new DataSet();
//                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
//                if (partner.Tables[0].Rows.Count > 0)
//                {
//                    txtSifraOdrediste.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
//                    txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
//                    txtSifraFakturirati.Text = txtSifraOdrediste.Text;
//                    txtPartnerNaziv1.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
//                    cbVD.Select();
//                }
//                else
//                {
//                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
//                }
//            }
//        }

//        private void btnSveFakture_Click(object sender, EventArgs e)
//        {
//            frmSviRadniNaloziServis objForm3 = new frmSviRadniNaloziServis();
//            objForm3.sifra_rn = "";
//            objForm3.MainForm = this;
//            broj_rn_edit = null;
//            objForm3.ShowDialog();
//            if (broj_rn_edit != null)
//            {
//                fillRN();
//                EnableDisable(true);
//                edit = true;
//                ttxBrojRN.ReadOnly = true;
//                nmGodina.ReadOnly = true;

//            }
//        }

//        private void PaintRows(DataGridView dg)
//        {
//            int br = 0;
//            for (int i = 0; i < dg.Rows.Count; i++)
//            {
//                if (br == 0)
//                {
//                    dg.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
//                    br++;
//                }
//                else
//                {
//                    dg.Rows[i].DefaultCellStyle.BackColor = Color.White;
//                    br = 0;
//                }

//            }
//            DataGridViewRow row = dg.RowTemplate;
//            row.Height = 25;
//        }

//        private void fillRN()
//        {
//            //fill header

//            EnableDisable(true);
//            edit = true;

//            DSrns = classSQL.select("SELECT * FROM radni_nalog_servis WHERE broj = '" + broj_rn_edit + "'", "radni_nalog_servis").Tables[0];

//            cbVD.SelectedValue = DSrns.Rows[0]["id_vd"].ToString();
//            txtSifraOdrediste.Text = DSrns.Rows[0]["id_odrediste"].ToString();
//            txtSifraFakturirati.Text = DSrns.Rows[0]["id_fakturirati"].ToString();
//            txtPartnerNaziv.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DSrns.Rows[0]["id_odrediste"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString();
//            txtPartnerNaziv1.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DSrns.Rows[0]["id_fakturirati"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString();
//            txtSifraNacinPlacanja.Text = DSrns.Rows[0]["id_nacin_placanja"].ToString();
//            txtModel.Text = DSrns.Rows[0]["model"].ToString();
//            txtSifraNarKupca.Text = DSrns.Rows[0]["id_nar_kupca"].ToString();
//            txtNarKupca1.Text = DSrns.Rows[0]["id_nar_kupca"].ToString();
//            cbOtprema.SelectedValue = DSrns.Rows[0]["otprema"].ToString();
//            rtbNapomena.Text = DSrns.Rows[0]["napomena"].ToString();
//            dtpDatum.Value = Convert.ToDateTime(DSrns.Rows[0]["date"].ToString());
//            dtpDanaValuta.Value = Convert.ToDateTime(DSrns.Rows[0]["vrijedi_do"].ToString());
//            cbKomercijalist.SelectedValue = DSrns.Rows[0]["id_zaposlenik_komercijala"].ToString();
//            cbNacinPlacanja.SelectedValue = DSrns.Rows[0]["id_nacin_placanja"].ToString();
//            cbZiroRacun.SelectedValue = DSrns.Rows[0]["zr"].ToString();
//            cbValuta.SelectedValue = DSrns.Rows[0]["id_valuta"].ToString();
//            cbNarKupca.SelectedValue = DSrns.Rows[0]["id_nar_kupca"].ToString();
//            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + DSrns.Rows[0]["id_zaposlenik_izradio"].ToString() + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
//            ttxBrojRN.Text = DSrns.Rows[0]["broj"].ToString();

//            //--------fill faktura stavke------------------------------

//            DataTable dtR = new DataTable();
//            string sql = "SELECT * FROM radni_nalog_servis_stavke WHERE broj = '" + DSrns.Rows[0]["broj"].ToString() + "'";
//            DSFS = classSQL.select(sql, "radni_nalog_servis").Tables[0];

//            for (int i = 0; i < DSFS.Rows.Count; i++)
//            {
//                dgw.Rows.Add();
//                int br = dgw.Rows.Count - 1;
//                string s;
//                if (DSFS.Rows[i]["oduzmi"].ToString()=="DA")
//                {
//                     s = "SELECT roba_prodaja.nc,roba.naziv,roba.jm,roba_prodaja.sifra,roba_prodaja.id_skladiste,roba.oduzmi" +
//                         " FROM roba_prodaja LEFT JOIN roba ON roba_prodaja.sifra=roba.sifra WHERE roba_prodaja.sifra='" + DSFS.Rows[i]["sifra"].ToString() + "'" +
//                         " AND roba_prodaja.id_skladiste='" + DSFS.Rows[i]["id_skladiste"].ToString() + "'";
//                }
//                else
//                {
//                     s = "SELECT * FROM roba WHERE sifra='" + DSFS.Rows[i]["sifra"].ToString() + "'";
//                }
//                dtR = classSQL.select(s, "roba_prodaja").Tables[0];

//                dgw.Rows[br].Cells[0].Value = i + 1;
//                dgw.Rows[br].Cells["sifra"].Value = dtR.Rows[0]["sifra"].ToString();
//                dgw.Rows[br].Cells["naziv"].Value = DSFS.Rows[i]["naziv"].ToString();
//                try { dgw.Rows[br].Cells["skladiste"].Value = DSFS.Rows[i]["id_skladiste"].ToString(); }
//                catch (Exception) { }
//                dgw.Rows[br].Cells["jmj"].Value = dtR.Rows[0]["jm"].ToString();
//                dgw.Rows[br].Cells["cijena_bez_pdva"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["vpc"]);
//                dgw.Rows[br].Cells["kolicina"].Value = DSFS.Rows[i]["kolicina"].ToString();
//                dgw.Rows[br].Cells["porez"].Value = DSFS.Rows[i]["porez"].ToString();
//                dgw.Rows[br].Cells["rabat"].Value = DSFS.Rows[i]["rabat"].ToString();
//                dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
//                dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
//                dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
//                dgw.Rows[br].Cells["vpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["vpc"].ToString());
//                //dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["mpc"]);
//                dgw.Rows[br].Cells["nc"].Value = String.Format("{0:0.00}", dtR.Rows[0]["nc"].ToString());
//                dgw.Rows[br].Cells["id_stavka"].Value = DSFS.Rows[i]["id_stavka"].ToString();
//                dgw.Rows[br].Cells["oduzmi"].Value = dtR.Rows[0]["oduzmi"].ToString();

//                dgw.Select();
//                dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[3];
//                izracun();
//                ControlDisableEnable(0, 1, 1, 0, 1);
//                PaintRows(dgw);
//            }

//            dgw.Columns["cijena_bez_pdva"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
//            dgw.Columns["rabat_iznos"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
//            dgw.Columns["iznos_bez_pdva"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
//            dgw.Columns["iznos_ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

//            dgw.Columns["cijena_bez_pdva"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
//            dgw.Columns["rabat_iznos"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
//            dgw.Columns["iznos_bez_pdva"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
//            dgw.Columns["iznos_ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
//        }

//        private void btnObrisi_Click(object sender, EventArgs e)
//        {
//            if(dgw.RowCount==0)
//            {
//                return;
//            }

//            if (dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].Value != null)
//            {
//                if (MessageBox.Show("Da li ste sigurni da želite obrisati ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
//                {
//                    classSQL.delete("DELETE FROM radni_nalog_servis_stavke WHERE id_stavka='" + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() + "'");
//                    dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
//                    classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje stavke sa RN servisa br." + ttxBrojRN.Text + "')");
//                    MessageBox.Show("Obrisano.");
//                }

//            }
//            else
//            {
//                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
//                MessageBox.Show("Obrisano.");
//            }
//        }

//        private void btnDeleteAllFaktura_Click(object sender, EventArgs e)
//        {
//            if (MessageBox.Show("Brisanjem ovog servisa brišete i količinu robe sa skladišta. Da li ste sigurni da želite obrisati ovaj servis?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
//            {
//                classSQL.delete("DELETE FROM radni_nalog_servis_stavke WHERE broj='" + ttxBrojRN.Text + "'");
//                classSQL.delete("DELETE FROM radni_nalog_servis WHERE broj='" + ttxBrojRN.Text + "'");
//                classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje cijelog rn servisa br." + ttxBrojRN.Text + "')");
//                MessageBox.Show("Obrisano.");

//                edit = false;
//                EnableDisable(false);
//                deleteFields();
//                btnDeleteAllFaktura.Enabled = false;
//                btnObrisi.Enabled = false;
//                ControlDisableEnable(1, 0, 0, 1, 0);
//            }
//        }

//        private void ttxBrojPonude_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//            {
//                DataTable DT = classSQL.select("SELECT broj FROM radni_nalog_servis WHERE godina='" + nmGodina.Value.ToString() + "' AND broj='" + ttxBrojRN.Text + "'", "radni_nalog_servis").Tables[0];
//                deleteFields();
//                if (DT.Rows.Count == 0)
//                {
//                    if (brojRN() == ttxBrojRN.Text.Trim())
//                    {
//                        edit = false;
//                        EnableDisable(true);
//                        btnSveFakture.Enabled = false;
//                        ttxBrojRN.Text = brojRN();
//                        btnDeleteAllFaktura.Enabled = false;
//                        txtSifraOdrediste.Select();
//                        ttxBrojRN.ReadOnly = true;
//                        nmGodina.ReadOnly = true;
//                    }
//                    else
//                    {
//                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
//                    }
//                }
//                else if (DT.Rows.Count == 1)
//                {
//                    broj_rn_edit = ttxBrojRN.Text;
//                    fillRN();
//                    EnableDisable(true);
//                    edit = true;
//                    btnDeleteAllFaktura.Enabled = true;
//                    txtSifraOdrediste.Select();
//                    ttxBrojRN.ReadOnly = true;
//                    nmGodina.ReadOnly = true;
//                }
//                txtSifraOdrediste.Select();
//                ControlDisableEnable(0, 1, 1, 0, 1);
//            }
//        }

//        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
//        {
//            if (dgw.CurrentCell.ColumnIndex == 3 & dgw.CurrentRow.Cells["oduzmi"].FormattedValue.ToString() == "DA")
//            {
//                SetCijenaSkladiste();
//            }

//            else if (dgw.CurrentCell.ColumnIndex == 9)
//            {
//                if (dgw.CurrentRow.Cells["skladiste"].Value == null & dgw.CurrentRow.Cells["oduzmi"].FormattedValue.ToString() == "DA")
//                {
//                    MessageBox.Show("Niste odabrali skladište", "Greška");
//                    return;
//                }

//                dgw.CurrentCell.Selected = false;
//                txtSifra_robe.Text = "";
//                txtSifra_robe.BackColor = Color.Silver;
//                txtSifra_robe.Select();
//            }

//            else if (dgw.CurrentCell.ColumnIndex == 7)
//            {
//                try
//                {
//                    dgw.CurrentRow.Cells["vpc"].Value = Convert.ToDouble(dgw.CurrentRow.Cells["mpc"].FormattedValue.ToString()) /
//						(1 + Convert.ToDouble(Convert.ToDouble(dgw.CurrentRow.Cells["porez"].FormattedValue.ToString()))/100);
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show("Koristite enter za sljedeću kolonu." + ex.ToString());
//                }
//            }

//            izracun();
//        }
//        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
//        {
//            if (dgw.Rows.Count < 1)
//                return;
//            dgw.BeginEdit(true);
//        }

//        private void frmPonude_FormClosing(object sender, FormClosingEventArgs e)
//        {
//            if (MainForm != null)
//            {
//            }
//        }

//        private void rtbNapomena_KeyDown_1(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//            {
//                if (rtbNapomena.Text == "")
//                {
//                    e.SuppressKeyPress = true;
//                    txtSifra_robe.Select();

//                }
//            }
//        }

//    }
//}