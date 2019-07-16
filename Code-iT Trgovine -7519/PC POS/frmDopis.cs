using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmDopis : Form
    {
        public string broj_ponude_edit { get; set; }
        private bool preuzetoSWeba = false;
        public frmDopis()
        {
            InitializeComponent();
        }

        private DataTable DTRoba = new DataTable();
        private DataSet DS_Skladiste = new DataSet();
        private DataSet DS_ZiroRacun = new DataSet();
        private DataSet DS_Zaposlenik = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DSIzjava = new DataSet();
        private DataSet DSnazivPlacanja = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataTable DTpromocije1;
        private DataTable DTOtprema = new DataTable();
        private DataTable DSponude = new DataTable();
        private DataTable DSFS = new DataTable();
        private DataTable DTpostavke = new DataTable();
        private DataTable DTpodaci_tvrtka = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];
        private double u = 0;
        public string prvo_skladiste = "1";
        public frmMenu MainForm { get; set; }
        private bool edit = false;

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

        private void frmPonude_Load(object sender, EventArgs e)
        {
            //MyDataGrid.MainForm = this;
            //if (Util.Korisno.oibTvrtke != Class.Postavke.OIB_PC1)
            //{
            //    chbPonudaNbc.Checked = false;
            //    chbPonudaNbc.Visible = false;
            //}

            this.Paint += new PaintEventHandler(Form1_Paint);

            //PaintRows(dgw);
            DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            DTpromocije1 = classSQL.select("SELECT * FROM promocija1", "promocija1").Tables[0];
            numeric();
            fillComboBox();
            txtBrojDopisa.Text = brojPonude();
            EnableDisable(false);
            ControlDisableEnable(1, 0, 0, 1, 0);
            if (broj_ponude_edit != null) { fillPonude(); }
            txtBrojDopisa.Select();
            //txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();
            //this.Paint += new PaintEventHandler(Form1_Paint);
        }

        //private class MyDataGrid : System.Windows.Forms.DataGridView
        //{
        //    public static frmDopis MainForm { get; set; }

        //    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //    {
        //        if (keyData == Keys.Enter)
        //        {
        //            MainForm.EnterDGW(MainForm.dgw);
        //            return true;
        //        }
        //        else if (keyData == Keys.Right)
        //        {
        //            MainForm.RightDGW(MainForm.dgw);
        //            return true;
        //        }
        //        else if (keyData == Keys.Left)
        //        {
        //            MainForm.LeftDGW(MainForm.dgw);
        //            return true;
        //        }
        //        else if (keyData == Keys.Up)
        //        {
        //            MainForm.UpDGW(MainForm.dgw);
        //            return true;
        //        }
        //        else if (keyData == Keys.Down)
        //        {
        //            MainForm.DownDGW(MainForm.dgw);
        //            return true;
        //        }
        //        else if (keyData == Keys.Insert)
        //        {
        //            MainForm.dgw.Rows.Add("", "Ručno", "", MainForm.prvo_skladiste, "kom", "0", "25", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "NE", "0");
        //            MainForm.RedniBroj();
        //            MainForm.dgw.Select();
        //            MainForm.dgw.CurrentCell = MainForm.dgw.Rows[MainForm.dgw.Rows.Count - 1].Cells[2];
        //            MainForm.dgw.BeginEdit(true);
        //            return true;
        //        }
        //        else if (keyData == Keys.Delete)
        //        {
        //            MainForm.dgw.Rows.RemoveAt(MainForm.dgw.CurrentRow.Index);
        //            MainForm.RedniBroj();
        //            return true;
        //        }
        //        return base.ProcessCmdKey(ref msg, keyData);
        //    }
        //}

        //private void EnterDGW(DataGridView d)
        //{
        //    if (d.Rows.Count < 1)
        //        return;
        //    int row = d.CurrentRow.Index;
        //    if (dgw.Rows[row].Cells[1].FormattedValue.ToString() != "Ručno")
        //    {
        //        if (d.CurrentCell.ColumnIndex == 3)
        //        {
        //            d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
        //            d.BeginEdit(true);
        //        }
        //        else if (d.CurrentCell.ColumnIndex == 5)
        //        {
        //            d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[8];
        //            d.BeginEdit(true);
        //        }
        //        else if (d.CurrentCell.ColumnIndex == 8)
        //        {
        //            d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[9];
        //            d.BeginEdit(true);
        //        }
        //        else if (d.CurrentCell.ColumnIndex == 9)
        //        {
        //            int curent = d.CurrentRow.Index;
        //            txtSifra_robe.Text = "";
        //            txtSifra_robe.Focus();
        //        }
        //    }
        //    else
        //    {
        //        if (d.CurrentCell.ColumnIndex == 2)
        //        {
        //            d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
        //            d.BeginEdit(true);
        //        }
        //        else if (d.CurrentCell.ColumnIndex == 3)
        //        {
        //            d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
        //            d.BeginEdit(true);
        //        }
        //        else if (d.CurrentCell.ColumnIndex == 4)
        //        {
        //            d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
        //            d.BeginEdit(true);
        //        }
        //        else if (d.CurrentCell.ColumnIndex == 5)
        //        {
        //            d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[6];
        //            d.BeginEdit(true);
        //        }
        //        else if (d.CurrentCell.ColumnIndex == 6)
        //        {
        //            d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[8];
        //            d.BeginEdit(true);
        //        }
        //        else if (d.CurrentCell.ColumnIndex == 8)
        //        {
        //            d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[9];
        //            d.BeginEdit(true);
        //        }
        //        else if (d.CurrentCell.ColumnIndex == 9)
        //        {
        //            int curent = d.CurrentRow.Index;
        //            txtSifra_robe.Text = "";
        //            txtSifra_robe.Focus();
        //        }
        //    }
        //}

        //private void LeftDGW(DataGridView d)
        //{
        //    if (d.Rows.Count < 1)
        //        return;
        //    if (d.CurrentCell.ColumnIndex == 2)
        //    {
        //    }
        //    else if (d.CurrentCell.ColumnIndex == 3)
        //    {
        //        d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[2];
        //        d.BeginEdit(true);
        //    }
        //    else if (d.CurrentCell.ColumnIndex == 4)
        //    {
        //        d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
        //        d.BeginEdit(true);
        //    }
        //    else if (d.CurrentCell.ColumnIndex == 5)
        //    {
        //        d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
        //        d.BeginEdit(true);
        //    }
        //    else if (d.CurrentCell.ColumnIndex == 7)
        //    {
        //        d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
        //        d.BeginEdit(true);
        //    }
        //    else if (d.CurrentCell.ColumnIndex == 8)
        //    {
        //        d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
        //        d.BeginEdit(true);
        //    }
        //}

        //private void RightDGW(DataGridView d)
        //{
        //    if (d.Rows.Count < 1)
        //        return;
        //    if (d.CurrentCell.ColumnIndex == 2)
        //    {
        //        d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
        //        d.BeginEdit(true);
        //    }
        //    else if (d.CurrentCell.ColumnIndex == 3)
        //    {
        //        d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
        //        d.BeginEdit(true);
        //    }
        //    else if (d.CurrentCell.ColumnIndex == 4)
        //    {
        //        d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
        //        d.BeginEdit(true);
        //    }
        //    else if (d.CurrentCell.ColumnIndex == 5)
        //    {
        //        d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
        //        d.BeginEdit(true);
        //    }
        //    else if (d.CurrentCell.ColumnIndex == 7)
        //    {
        //        d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[8];
        //        d.BeginEdit(true);
        //    }
        //    else if (d.CurrentCell.ColumnIndex == 8)
        //    {
        //        int curent = d.CurrentRow.Index;
        //        txtSifra_robe.Text = "";
        //        txtSifra_robe.Focus();
        //    }
        //}

        //private void UpDGW(DataGridView d)
        //{
        //    if (d.Rows.Count < 1)
        //        return;
        //    int curent = d.CurrentRow.Index;
        //    if (d.CurrentCell.ColumnIndex == 3)
        //    {
        //    }
        //    else if (curent == 0)
        //    {
        //        d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[2];
        //        d.BeginEdit(true);
        //    }
        //    else
        //    {
        //        d.CurrentCell = dgw.Rows[d.CurrentRow.Index - 1].Cells[2];
        //        d.BeginEdit(true);
        //    }
        //}

        //private void DownDGW(DataGridView d)
        //{
        //    if (d.Rows.Count < 1)
        //        return;
        //    int curent = d.CurrentRow.Index;
        //    if (d.CurrentCell.ColumnIndex == 3)
        //    {
        //        SendKeys.Send("{F4}");
        //    }
        //    else if (curent == d.RowCount - 1)
        //    {
        //        d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[2];
        //        d.BeginEdit(true);
        //    }
        //    else
        //    {
        //        d.CurrentCell = dgw.Rows[d.CurrentRow.Index + 1].Cells[2];
        //        d.BeginEdit(true);
        //    }
        //}

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
                btnSviDopisi.Enabled = false;
            }
            else if (sve == 1)
            {
                btnSviDopisi.Enabled = true;
            }

            if (delAll == 0)
            {
                btnDeleteAllDopis.Enabled = false;
            }
            else if (delAll == 1)
            {
                btnDeleteAllDopis.Enabled = true;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private string brojPonude()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_ponude AS bigint)) FROM ponude", "ponude").Tables[0];
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
            //fill ziroracun
            //DS_ZiroRacun = classSQL.select("SELECT * FROM ziro_racun", "ziro_racun");
            //cbZiroRacun.DataSource = DS_ZiroRacun.Tables[0];
            //cbZiroRacun.DisplayMember = "ziroracun";
            //cbZiroRacun.ValueMember = "id_ziroracun";
            //cbZiroRacun.SelectedValue = "1";
            //fill otprema
            //DTOtprema = classSQL.select("SELECT * FROM otprema", "otprema").Tables[0];
            //cbOtprema.DataSource = DTOtprema;
            //cbOtprema.DisplayMember = "naziv";
            //cbOtprema.ValueMember = "id_otprema";

            //fill komercijalist
            //DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici;", "zaposlenici");
            //cbKomercijalist.DataSource = DS_Zaposlenik.Tables[0];
            //cbKomercijalist.DisplayMember = "IME";
            //cbKomercijalist.ValueMember = "id_zaposlenik";
            //cbKomercijalist.SelectedValue = Properties.Settings.Default.id_zaposlenik;

            //fill izjava
            //DSIzjava = classSQL.select("SELECT * FROM izjava ORDER BY id_izjava", "izjava");
            //cbIzjava.DataSource = DSIzjava.Tables[0];
            //cbIzjava.DisplayMember = "izjava";
            //cbIzjava.ValueMember = "id_izjava";

            //fill vrsta dokumenta
            //DSvd = classSQL.select("SELECT * FROM fakture_vd  WHERE grupa = 'pon' ORDER BY id_vd", "fakture_vd");
            //cbVD.DataSource = DSvd.Tables[0];
            //cbVD.DisplayMember = "vd";
            //cbVD.ValueMember = "id_vd";

            //fill nacin_placanja
            //DSnazivPlacanja = classSQL.select("SELECT * FROM nacin_placanja", "nacin_placanja");
            //cbNacinPlacanja.DataSource = DSnazivPlacanja.Tables[0];
            //cbNacinPlacanja.DisplayMember = "naziv_placanja";
            //cbNacinPlacanja.ValueMember = "id_placanje";
            //cbNacinPlacanja.SelectedValue = 3;
            //txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();

            //DS Valuta
            //DSValuta = classSQL.select("SELECT * FROM valute", "valute");
            //cbValuta.DataSource = DSValuta.Tables[0];
            //cbValuta.DisplayMember = "ime_valute";
            //cbValuta.ValueMember = "id_valuta";
            //cbValuta.SelectedValue = 5;
            //txtTecaj.DataBindings.Add("Text", DSValuta.Tables[0], "tecaj");
            //txtTecaj.Text = "1";

            DataTable DTSK = new DataTable("Roba");
            DTSK.Columns.Add("id_skladiste", typeof(string));
            DTSK.Columns.Add("skladiste", typeof(string));

            DS_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste");

            if (DS_Skladiste.Tables[0].Rows.Count > 0)
                prvo_skladiste = DS_Skladiste.Tables[0].Rows[0]["id_skladiste"].ToString();

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
        }

        private void numeric()
        {
            nmGodinaDopisa.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nmGodinaDopisa.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nmGodinaDopisa.Value = Convert.ToInt16(DateTime.Now.Year.ToString());
        }

        private void EnableDisable(bool x)
        {
            //cbVD.Enabled = x;
            txtPartnerSifra.Enabled = x;
            //txtSifraFakturirati.Enabled = x;
            txtPartnerNaziv.Enabled = x;
            //txtPartnerNaziv1.Enabled = x;
            //txtSifraNacinPlacanja.Enabled = x;
            //txtModel.Enabled = x;
            //txtSifraNarKupca.Enabled = x;
            //txtNarKupca1.Enabled = x;
            //cbOtprema.Enabled = x;
            txtText.Enabled = x;
            //txtSifra_robe.Enabled = x;
            btnPartner.Enabled = x;
            //btnPartner1.Enabled = x;
            dtpDatum.Enabled = x;
            //dtpDanaValuta.Enabled = x;
            //cbIzjava.Enabled = x;
            //cbKomercijalist.Enabled = x;
            //cbNacinPlacanja.Enabled = x;
            //cbZiroRacun.Enabled = x;
            //cbValuta.Enabled = x;
            //txtTecaj.Enabled = x;
            //cbNarKupca.Enabled = x;
            //btnObrisi.Enabled = x;
            //btnOpenRoba.Enabled = x;
            //btnNarKupca.Enabled = x;
            //label9.Enabled = x;

            txtBrojDopisa.ReadOnly = x;
            nmGodinaDopisa.ReadOnly = x;
        }

        private void deleteFields()
        {
            txtPartnerSifra.Text = "";
            //txtSifraFakturirati.Text = "";
            txtPartnerNaziv.Text = "";
            //txtPartnerNaziv1.Text = "";
            //txtSifraNacinPlacanja.Text = "";
            //txtModel.Text = "";
            //txtSifraNarKupca.Text = "";
            //txtNarKupca1.Text = "";
            txtText.Text = "";
            //txtSifra_robe.Text = "";
            //cbKomercijalist.SelectedValue = Properties.Settings.Default.id_zaposlenik;
            //cbValuta.SelectedValue = 5;
            dtpDatum.Value = DateTime.Now;
            //dtpDanaValuta.Value = dtpDatum.Value;
            //txtDana.Text = "0";

            //dgw.Rows.Clear();
        }

        private void btnIzlaz_Click(object sender, EventArgs e)
        {
            this.Close();
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

            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #region ON_KEY_DOWN_REGION

        private void txtSifraOdrediste_KeyDown(object sender, KeyEventArgs e)
        {
            //PartnerPostaviPopust();

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtPartnerSifra.Text == "")
                {
                    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                    partnerTrazi.ShowDialog();
                    if (Properties.Settings.Default.id_partner != "")
                    {
                        DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                        if (DSpartner.Tables[0].Rows.Count > 0)
                        {
                            //PartnerPostaviPopust();
                            txtPartnerSifra.Text = DSpartner.Tables[0].Rows[0]["id_partner"].ToString();
                            txtPartnerNaziv.Text = DSpartner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                            //txtSifraFakturirati.Text = DSpartner.Tables[0].Rows[0]["id_partner"].ToString();
                            //txtPartnerNaziv1.Text = DSpartner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                            //cbVD.Select();
                            //txtSifraFakturirati.Select();
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtPartnerSifra.Select();
                        }
                    }
                    else
                    {
                        txtPartnerSifra.Select();
                        return;
                    }
                }

                string Str = txtPartnerSifra.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtPartnerSifra.Text = "0";
                }

                DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtPartnerSifra.Text + "'", "partners").Tables[0];
                if (DSpar.Rows.Count > 0)
                {
                    txtPartnerNaziv.Text = DSpar.Rows[0][0].ToString();
                    //txtPartnerNaziv1.Text = DSpar.Rows[0][0].ToString();
                    //txtSifraFakturirati.Text = txtSifraOdrediste.Text;
                    //txtSifraFakturirati.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        //private void txtSifraFakturirati_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        //PartnerPostaviPopust();
        //        e.SuppressKeyPress = true;
        //        //PartnerPostaviPopust();
        //        //string Str = txtSifraFakturirati.Text.Trim();
        //        //double Num;
        //        //bool isNum = double.TryParse(Str, out Num);
        //        //if (!isNum)
        //        //{
        //        //    txtSifraFakturirati.Text = "0";
        //        //}

        //        //DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner='" + txtSifraFakturirati.Text + "'", "partners");
        //        //if (DSpartner.Tables[0].Rows.Count > 0)
        //        //{
        //        //    txtPartnerNaziv1.Text = DSpartner.Tables[0].Rows[0][0].ToString();
        //        //    cbVD.Select();
        //        //}
        //        //else
        //        //{
        //        //    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
        //        //}
        //    }
        //}

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
                //txtDana.Select();
            }
        }

        //private void txtDana_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
        //        {
        //            (sender as TextBox).Text = "0";
        //            //dtpDanaValuta.Select();
        //        }

        //        try
        //        {
        //            //DateTime dvo = dtpDatum.Value;
        //            //dtpDanaValuta.Value = dvo.AddDays(Convert.ToInt16(txtDana.Text));
        //            //dtpDanaValuta.Select();
        //        }
        //        catch (Exception)
        //        {
        //        }
        //    }
        //}

        //private void dtpDanaValuta_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        try
        //        {
        //            //DateTime dt1 = dtpDanaValuta.Value;
        //            //DateTime dt2 = dtpDanaValuta.Value;
        //            //TimeSpan ts = dt1 - dt2;
        //            //txtDana.Text = (Convert.ToInt16(ts.Days.ToString()) + 1).ToString();
        //            //cbIzjava.Select();
        //        }
        //        catch (Exception)
        //        {
        //        }
        //    }
        //}

        //private void cbIzjava_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        //rtbNapomena.Text += cbIzjava.Text;
        //        //cbKomercijalist.Select();
        //    }
        //}

        //private void cbKomercijalist_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        //txtModel.Select();
        //    }
        //}

        //private void txtIzradio_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        //txtModel.Select();
        //    }
        //}

        //private void txtModel_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        //txtSifraNacinPlacanja.Select();
        //    }
        //}

        //private void txtSifraNacinPlacanja_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        try
        //        {
        //            //e.SuppressKeyPress = true;
        //            //DataRow[] dataROW = DSnazivPlacanja.Tables[0].Select("id_placanje = " + txtSifraNacinPlacanja.Text);
        //            //cbNacinPlacanja.SelectedValue = dataROW[0]["id_placanje"].ToString();
        //            //cbNacinPlacanja.Select();
        //        }
        //        catch (Exception)
        //        {
        //            MessageBox.Show("Krivi unos.", "Greška");
        //        }
        //    }
        //}

        //private void cbNacinPlacanja_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        //cbZiroRacun.Select();
        //    }
        //}

        //private void cbNacinPlacanja_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();
        //}

        //private void cbZiroRacun_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        //cbValuta.Select();
        //    }
        //}

        //private void cbValuta_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        //txtSifraNarKupca.Select();
        //    }
        //}

        //private void txtTecaj_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        //txtSifraNarKupca.Select();
        //    }
        //}

        //private void txtSifraNarKupca_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        cbNarKupca.Select();
        //    }
        //}

        //private void cbNarKupca_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        txtNarKupca1.Select();
        //    }
        //}

        //private void txtNarKupca1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        cbOtprema.Select();
        //    }
        //}

        private void txtOtprema_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtText.Select();
            }
        }

        //private void rtbNapomena_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        //txtSifra_robe.Select();
        //    }
        //}

        //private pc1.konektor WEBbaza = new pc1.konektor();

        //private void txtSifra_robe_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        preuzetoSWeba = false;
        //        e.SuppressKeyPress = true;

        //        if (txtSifra_robe.Text == "")
        //        {
        //            frmRobaTrazi roba = new frmRobaTrazi();
        //            roba.ShowDialog();

        //            if (Properties.Settings.Default.id_roba != "")
        //            {
        //                txtSifra_robe.Text = Properties.Settings.Default.id_roba;
        //                txtSifra_robe.Select();
        //            }
        //            else
        //            {
        //                return;
        //            }
        //        }

        //        #region KOD VEZANI ZA POPUSTE KOD SLJEDECE KUPNJE

        //        if (txtSifra_robe.Text.Length > 2)
        //        {
        //            if (DTpromocije1.Rows.Count > 0 && DTpromocije1.Rows[0]["aktivnost"].ToString() == "DA" && txtSifra_robe.Text.Substring(0, 3) == "000")
        //            {
        //                double uk;
        //                double popust;
        //                DataTable DTrp = classSQL.select("SELECT * FROM racun_popust_kod_sljedece_kupnje WHERE broj_racuna='" + txtSifra_robe.Text.Substring(3, txtSifra_robe.Text.Length - 3) + "' AND dokumenat='FA'", "racun_popust_kod_sljedece_kupnje").Tables[0];

        //                if (DTrp.Rows.Count == 0)
        //                {
        //                    MessageBox.Show("Ovaj popust nije valjan.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    return;
        //                }

        //                if (DTrp.Rows[0]["koristeno"].ToString() == "DA")
        //                {
        //                    MessageBox.Show("Ovaj popust je već iskorišten.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    return;
        //                }

        //                DateTime dateFromPopust = Convert.ToDateTime(DTrp.Rows[0]["datum"].ToString()).AddDays(Convert.ToDouble(DTpromocije1.Rows[0]["traje_do"].ToString()));

        //                if (dateFromPopust < DateTime.Now)
        //                {
        //                    MessageBox.Show("Ovom popustu je istekao datum.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    return;
        //                }

        //                uk = Convert.ToDouble(DTrp.Rows[0]["ukupno"].ToString());
        //                popust = Convert.ToDouble(DTrp.Rows[0]["popust"].ToString());
        //                uk = uk * 3 / 100;

        //                if ((Convert.ToDouble(u.ToString()) - uk) < 0)
        //                {
        //                    MessageBox.Show("Popust je veći od ukupnog računa.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    return;
        //                }

        //                dgw.Rows.Add(
        //                            dgw.RowCount - 1,
        //                            txtSifra_robe.Text,
        //                            "Popust sa prethodnog računa",
        //                            "1",
        //                            "kn",
        //                            1,
        //                            DTpostavke.Rows[0]["pdv"].ToString(),
        //                            DTpostavke.Rows[0]["pdv"].ToString(),
        //                            Math.Round(uk * -1, 2).ToString("#0.00"),
        //                            "0",
        //                            "0",
        //                            Math.Round(uk * -1 / (1 + Convert.ToDouble(DTpostavke.Rows[0]["pdv"].ToString()) / 100), 3).ToString("#0.000"),
        //                            Math.Round(uk * -1 / (1 + Convert.ToDouble(DTpostavke.Rows[0]["pdv"].ToString()) / 100), 3).ToString("#0.000"),
        //                            Math.Round(uk * -1, 2).ToString("#0.00"),
        //                            Math.Round(uk * -1 / (1 + Convert.ToDouble(DTpostavke.Rows[0]["pdv"].ToString()) / 100), 3).ToString("#0.000"),
        //                            Math.Round(uk * -1, 2).ToString("#0.00"),
        //                            "",
        //                            "",
        //                            ""
        //                        );

        //                int br = dgw.Rows.Count - 1;
        //                dgw.ClearSelection();
        //                izracun();
        //                PaintRows(dgw);
        //                dgw.ClearSelection();
        //                txtSifra_robe.Text = "";
        //                txtSifra_robe.Select();
        //                return;
        //            }
        //        }

        //        #endregion KOD VEZANI ZA POPUSTE KOD SLJEDECE KUPNJE

        //        string sql = "SELECT * FROM roba WHERE sifra='" + txtSifra_robe.Text + "'";

        //        DTRoba = classSQL.select(sql, "roba").Tables[0];
        //        if (DTRoba.Rows.Count > 0)
        //        {
        //            txtSifra_robe.BackColor = Color.White;
        //            SetRoba();
        //            dgw.Rows[dgw.Rows.Count - 1].Cells["skladiste"].Selected = true;
        //        }
        //        else
        //        {
        //            if (DTpodaci_tvrtka.Rows[0]["oib"].ToString() == Class.Postavke.OIB_PC1)
        //            {
        //                //DataTable DTweb = new DataTable();
        //                //try
        //                //{
        //                //    DTweb = WEBbaza.GetDataset("SELECT * FROM artikli WHERE ProductID='" + txtSifra_robe.Text + "'", "artikli", "admin", "q1w2e3r4123").Tables[0];
        //                //}
        //                //catch (Exception ex)
        //                //{
        //                //    MessageBox.Show(ex.ToString());
        //                //}

        //                //if (DTweb.Rows.Count > 0)
        //                //{
        //                //    if (MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga u programu.\r\n" +
        //                //        "Isti artikl postoji na web stranici, želite li skinuti artikl sa weba-a?", "Greška",
        //                //        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
        //                //    {
        //                //        SetRobaWeb(DTweb);
        //                //    }
        //                //}
        //                string[] s = null;

        //                using (wsSoftKontrol.wsSoftKontrol ws = new wsSoftKontrol.wsSoftKontrol())
        //                {
        //                    s = ws.getArtiklFromWeb(txtSifra_robe.Text);
        //                }

        //                if (s != null && s.Length > 0 && s[0] != null && s[1] != null && s[2] != null && s[3] != null)
        //                {
        //                    if (MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga u programu.\r\n" +
        //                        "Isti artikl postoji na web stranici, želite li skinuti artikl sa weba-a?", "Obavijest",
        //                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
        //                    {
        //                        preuzetoSWeba = true;
        //                        //uzetoSWeba = true;
        //                        Fill_RobaWeb(s);
        //                        //txtNaziv.Select();
        //                    }
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga u šifrarniku niti na web-u.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                }
        //            }
        //            else
        //            {
        //                MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //        }
        //    }
        //}

        //private void Fill_RobaWeb(string[] DTweb)
        //{
        //    dgw.Rows.Add();
        //    int br = dgw.Rows.Count - 1;
        //    if (br < 0)
        //        br = 0;

        //    decimal porezPDV;
        //    decimal.TryParse(Properties.Settings.Default.PDV, out porezPDV);
        //    decimal mpc = 0;
        //    decimal.TryParse(DTweb[3].ToString().Replace(".", ","), out mpc);
        //    //decimal vpc = mpc / ((porezPDV / 100) + 1);
        //    mpc = mpc / Convert.ToDecimal((1 - (5f / 100f)));
        //    decimal vpc = Math.Round(mpc, 2, MidpointRounding.AwayFromZero) / ((porezPDV / 100) + 1);
        //    //enable(true);

        //    dgw.Rows[br].Cells[0].Value = "1";
        //    dgw.Rows[br].Cells["sifra"].Value = DTweb[0].ToString();
        //    dgw.Rows[br].Cells["naziv"].Value = DTweb[1].ToString();
        //    dgw.Rows[br].Cells["jmj"].Value = "kom";
        //    dgw.Rows[br].Cells["cijena_bez_pdva"].Value = Math.Round(vpc, 3).ToString("#0.00");
        //    dgw.Rows[br].Cells["kolicina"].Value = "1";
        //    dgw.Rows[br].Cells["porez"].Value = porezPDV.ToString();
        //    dgw.Rows[br].Cells["porezZaIzracun"].Value = Math.Round(porezPDV, 3).ToString("#0.00");
        //    dgw.Rows[br].Cells["rabat"].Value = "0,00";
        //    dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
        //    dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
        //    dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
        //    dgw.Rows[br].Cells["vpc"].Value = Math.Round(vpc, 3).ToString("#0.000");
        //    dgw.Rows[br].Cells["mpc"].Value = Math.Round(mpc, 3).ToString("#0.00");
        //    dgw.Rows[br].Cells["nc"].Value = Math.Round(vpc, 3).ToString("#0.00");
        //    dgw.Rows[br].Cells["oduzmi"].Value = "DA";
        //    dgw.Rows[br].Cells["skladiste"].Value = DTpostavke.Rows[0]["default_skladiste"].ToString();

        //    dgw.Select();
        //    try
        //    {
        //        dgw.CurrentCell = dgw.Rows[br].Cells[3];
        //    }
        //    catch
        //    {
        //    }

        //    dgw.BeginEdit(true);

        //    izracun();
        //    PaintRows(dgw);
        //}

        //private void SetRobaWeb(DataTable DTwebRoba)
        //{
        //    dgw.Rows.Add();
        //    int br = dgw.Rows.Count - 1;

        //    decimal porezPDV;
        //    decimal.TryParse(Properties.Settings.Default.PDV, out porezPDV);
        //    decimal mpc;
        //    decimal.TryParse(DTwebRoba.Rows[0]["Price"].ToString(), out mpc);

        //    decimal vpc = mpc / ((porezPDV / 100) + 1);

        //    dgw.Rows[br].Cells[0].Value = "1";
        //    dgw.Rows[br].Cells["sifra"].Value = DTwebRoba.Rows[0]["ProductID"].ToString();
        //    dgw.Rows[br].Cells["naziv"].Value = DTwebRoba.Rows[0]["Productname"].ToString();
        //    dgw.Rows[br].Cells["jmj"].Value = "kom";
        //    dgw.Rows[br].Cells["cijena_bez_pdva"].Value = Math.Round(vpc, 3).ToString("#0.00");
        //    dgw.Rows[br].Cells["kolicina"].Value = "1";
        //    dgw.Rows[br].Cells["porez"].Value = porezPDV.ToString();
        //    dgw.Rows[br].Cells["porezZaIzracun"].Value = Math.Round(porezPDV, 3).ToString("#0.00");
        //    dgw.Rows[br].Cells["rabat"].Value = "0,00";
        //    dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
        //    dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
        //    dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
        //    dgw.Rows[br].Cells["vpc"].Value = Math.Round(vpc, 3).ToString("#0.000");
        //    dgw.Rows[br].Cells["mpc"].Value = Math.Round(mpc, 3).ToString("#0.00");
        //    dgw.Rows[br].Cells["nc"].Value = Math.Round(vpc, 3).ToString("#0.00");
        //    dgw.Rows[br].Cells["oduzmi"].Value = "DA";
        //    dgw.Rows[br].Cells["skladiste"].Value = DTpostavke.Rows[0]["default_skladiste"].ToString();

        //    dgw.Select();
        //    try
        //    {
        //        dgw.CurrentCell = dgw.Rows[br].Cells[3];
        //    }
        //    catch
        //    {
        //    }

        //    dgw.BeginEdit(true);

        //    izracun();
        //    PaintRows(dgw);
        //}

        #endregion ON_KEY_DOWN_REGION

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            //dodjeli_popust = 0;
            DSpartner = null;
            edit = false;
            EnableDisable(true);
            deleteFields();
            btnSviDopisi.Enabled = false;
            txtBrojDopisa.Text = brojPonude();
            btnDeleteAllDopis.Enabled = false;
            txtBrojDopisa.ReadOnly = true;
            nmGodinaDopisa.ReadOnly = true;
            ControlDisableEnable(0, 1, 1, 0, 1);
            //cbStavkeValuta.Checked = false;
            txtPartnerSifra.Select();
            //txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();
        }

        //private void SetRoba()
        //{
        //    dgw.Rows.Add();
        //    int br = dgw.Rows.Count - 1;

        //    decimal _NBC = Util.Korisno.VratiNabavnuCijenu(DTRoba.Rows[0]["sifra"].ToString(), DTpostavke.Rows[0]["default_skladiste"].ToString());

        //    dgw.Rows[br].Cells[0].Value = "1";
        //    dgw.Rows[br].Cells["sifra"].Value = DTRoba.Rows[0]["sifra"].ToString();
        //    dgw.Rows[br].Cells["naziv"].Value = DTRoba.Rows[0]["naziv"].ToString();
        //    dgw.Rows[br].Cells["jmj"].Value = DTRoba.Rows[0]["jm"].ToString();
        //    dgw.Rows[br].Cells["cijena_bez_pdva"].Value = string.Format("{0:0.000}", DTRoba.Rows[0]["vpc"]);
        //    dgw.Rows[br].Cells["kolicina"].Value = "1";
        //    dgw.Rows[br].Cells["porez"].Value = DTRoba.Rows[0]["porez"].ToString();
        //    dgw.Rows[br].Cells["porezZaIzracun"].Value = DTRoba.Rows[0]["porez"].ToString();
        //    dgw.Rows[br].Cells["rabat"].Value = "0,00";
        //    dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
        //    dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
        //    dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
        //    dgw.Rows[br].Cells["vpc"].Value = string.Format("{0:0.000}", DTRoba.Rows[0]["vpc"]);
        //    dgw.Rows[br].Cells["mpc"].Value = string.Format("{0:0.00}", DTRoba.Rows[0]["mpc"]);
        //    dgw.Rows[br].Cells["nc"].Value = string.Format("{0:0.00}", DTRoba.Rows[0]["nc"]);
        //    dgw.Rows[br].Cells["oduzmi"].Value = DTRoba.Rows[0]["oduzmi"].ToString();
        //    dgw.Rows[br].Cells["skladiste"].Value = DTpostavke.Rows[0]["default_skladiste"].ToString();

        //    dgw.Select();
        //    try
        //    {
        //        dgw.CurrentCell = dgw.Rows[br].Cells[3];
        //    }
        //    catch
        //    {
        //    }
        //    PartnerPostaviPopust();

        //    dgw.BeginEdit(true);

        //    izracun();
        //    PaintRows(dgw);
        //}

        //private void dgw_CellValidated(object sender, DataGridViewCellEventArgs e)
        //{
        //    int row = dgw.CurrentCell.RowIndex;
        //    if (dgw.CurrentCell.ColumnIndex == 3)
        //    {
        //        SetCijenaSkladiste();
        //    }

        //    else if (dgw.CurrentCell.ColumnIndex == 9)
        //    {
        //        if (dgw.CurrentRow.Cells["skladiste"].Value == null)
        //        {
        //            MessageBox.Show("Niste odabrali skladište", "Greška");
        //            return;
        //        }

        //        dgw.CurrentCell.Selected = false;
        //        txtSifra_robe.Text = "";
        //        txtSifra_robe.BackColor = Color.Silver;
        //        txtSifra_robe.Select();
        //    }
        //    izracun();
        //}

        //private void SetCijenaSkladiste()
        //{
        //    if (dgw.CurrentRow.Cells["skladiste"].Value != null)
        //    {
        //        DataSet dsRobaProdaja = new DataSet();
        //        dsRobaProdaja = classSQL.select("SELECT * FROM roba_prodaja WHERE id_skladiste='" + dgw.CurrentRow.Cells["skladiste"].Value + "' AND sifra='" + dgw.CurrentRow.Cells["sifra"].FormattedValue.ToString() + "'", "roba_prodaja");
        //        if (dsRobaProdaja.Tables[0].Rows.Count > 0)
        //        {
        //            decimal _NBC = Util.Korisno.VratiNabavnuCijenu(dgw.CurrentRow.Cells["sifra"].FormattedValue.ToString(), dgw.CurrentRow.Cells["skladiste"].Value.ToString());
        //            if (Convert.ToDouble(dsRobaProdaja.Tables[0].Rows[0]["kolicina"].ToString()) > 0)
        //            {
        //                dgw.CurrentRow.Cells["porez"].Value = dsRobaProdaja.Tables[0].Rows[0]["porez"].ToString();
        //                dgw.CurrentRow.Cells["porezZaIzracun"].Value = dsRobaProdaja.Tables[0].Rows[0]["porez"].ToString();
        //                dgw.CurrentRow.Cells["vpc"].Value = string.Format("{0:0.000}", dsRobaProdaja.Tables[0].Rows[0]["vpc"]);
        //                dgw.CurrentRow.Cells["nc"].Value = _NBC.ToString("#0.000");
        //                lblNaDan.ForeColor = Color.Green;
        //                lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima " + dsRobaProdaja.Tables[0].Rows[0]["kolicina"].ToString() + " " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
        //            }
        //            else
        //            {
        //                dgw.CurrentRow.Cells["porez"].Value = dsRobaProdaja.Tables[0].Rows[0]["porez"].ToString();
        //                dgw.CurrentRow.Cells["porezZaIzracun"].Value = dsRobaProdaja.Tables[0].Rows[0]["porez"].ToString();
        //                dgw.CurrentRow.Cells["vpc"].Value = string.Format("{0:0.000}", dsRobaProdaja.Tables[0].Rows[0]["vpc"]);
        //                dgw.CurrentRow.Cells["nc"].Value = _NBC.ToString("#0.000");
        //                lblNaDan.ForeColor = Color.Red;
        //                lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima " + dsRobaProdaja.Tables[0].Rows[0]["kolicina"].ToString() + " " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
        //            }
        //        }
        //        else
        //        {
        //            lblNaDan.ForeColor = Color.Red;
        //            lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima 0,00 " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
        //        }
        //    }
        //}

        public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle,
                System.Globalization.CultureInfo.CurrentCulture, out result);
        }

        //private double SveUkupno = 0;

        //private void izracun()
        //{
        //    if (dgw.RowCount > 0)
        //    {
        //        int rowBR = dgw.CurrentRow.Index;

        //        double dec_parse;
        //        if (!Double.TryParse(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString(), out dec_parse))
        //        {
        //            dgw.Rows[rowBR].Cells["kolicina"].Value = "1";
        //            //MessageBox.Show("Greška kod upisa količine.", "Greška");
        //            return;
        //        }

        //        if (!Double.TryParse(dgw.Rows[rowBR].Cells["rabat"].FormattedValue.ToString(), out dec_parse))
        //        {
        //            dgw.Rows[rowBR].Cells["rabat"].Value = "0";
        //            MessageBox.Show("Greška kod upisa rabata.", "Greška");
        //            return;
        //        }

        //        if (!Double.TryParse(dgw.Rows[rowBR].Cells["rabat_iznos"].FormattedValue.ToString(), out dec_parse))
        //        {
        //            dgw.Rows[rowBR].Cells["rabat_iznos"].Value = "0,00";
        //            MessageBox.Show("Greška kod rabata.", "Greška");
        //            return;
        //        }

        //        if (!Double.TryParse(dgw.Rows[rowBR].Cells["Porez"].FormattedValue.ToString(), out dec_parse))
        //        {
        //            dgw.Rows[rowBR].Cells["Porez"].Value = "0,00";
        //            MessageBox.Show("Greška kod poreza.", "Greška");
        //            return;
        //        }

        //        double porez = 0;
        //        double kol = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString()), 3);
        //        double vpc = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["vpc"].FormattedValue.ToString()), 3);
        //        double nbc = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["nc"].FormattedValue.ToString()), 3);
        //        if (dgw.Rows[rowBR].Cells["sifra"].FormattedValue.ToString() == "Ručno")
        //        {
        //            dgw.Rows[rowBR].Cells["porez"].Value = chbObracunPdv.Checked ? Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["porezZaIzracun"].FormattedValue.ToString()), 2).ToString("#0.00")
        //            : "0,00";
        //            porez = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["porezZaIzracun"].FormattedValue.ToString()), 2);
        //        }
        //        else
        //        {
        //            // FIX: tu sam zamenil linje
        //            dgw.Rows[rowBR].Cells["porez"].Value = chbObracunPdv.Checked ? Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["porezZaIzracun"].FormattedValue.ToString()), 2).ToString("#0.00")
        //            : "0,00";

        //            // FIX: cita od porez a ne od porezZaIzracun
        //            porez = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["porez"].FormattedValue.ToString()), 2);
        //        }

        //        //if (Class.Postavke.proizvodnjaFakturaNbc)
        //        //{
        //        //    vpc = nbc;
        //        //    dgw.Rows[rowBR].Cells["vpc"].Value = Math.Round(vpc, 3).ToString("#0.000");
        //        //}
        //        double rbt = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["rabat"].FormattedValue.ToString()), 2);

        //        double porez_ukupno = chbPonudaNbc.Checked ? nbc * (porez) / 100 : vpc * (porez) / 100;
        //        double mpc = (chbObracunPdv.Checked ? (chbPonudaNbc.Checked ? Math.Round(porez_ukupno + nbc, 2) : Math.Round(porez_ukupno + vpc, 2)) : (chbPonudaNbc.Checked ? Math.Round(nbc, 2) : Math.Round(vpc, 2)));
        //        double mpc_sa_kolicinom = mpc * kol;
        //        double rabat = mpc * rbt / 100;
        //        double iznos;

        //        dgw.Rows[rowBR].Cells["mpc"].Value = Math.Round(mpc, 2).ToString("#0.00");
        //        dgw.Rows[rowBR].Cells["rabat_iznos"].Value = Math.Round((!chbPonudaNbc.Checked ? rabat : 0) * kol, 2).ToString("#0.00");
        //        dgw.Rows[rowBR].Cells["iznos_bez_pdva"].Value = chbObracunPdv.Checked ?
        //            Math.Round((((mpc - (!chbPonudaNbc.Checked ? rabat : 0)) * kol) / (1 + porez / 100)), 2).ToString("#0.00") :
        //            Math.Round((((mpc - (!chbPonudaNbc.Checked ? rabat : 0)) * kol) / (1 + 0 / 100)), 2).ToString("#0.00");

        //        dgw.Rows[rowBR].Cells["iznos_ukupno"].Value = Math.Round((mpc - (!chbPonudaNbc.Checked ? rabat : 0)) * kol, 2).ToString("#0.00");
        //        dgw.Rows[rowBR].Cells["cijena_bez_pdva"].Value = chbObracunPdv.Checked ?
        //            Math.Round(((mpc - (!chbPonudaNbc.Checked ? rabat : 0)) / (1 + porez / 100)), 3).ToString("#0.000") :
        //            Math.Round(((mpc - (!chbPonudaNbc.Checked ? rabat : 0)) / (1 + 0 / 100)), 3).ToString("#0.000");
        //        dgw.Rows[rowBR].Cells["kolicina"].Value = kol.ToString("#0.000");
        //        dgw.Rows[rowBR].Cells["rabat"].Value = rbt.ToString("#0.00");

        //        if (chbObracunPdv.Checked)
        //            dgw.Rows[rowBR].Cells["porezZaIzracun"].Value = porez.ToString("#0.00");

        //        double B_pdv = 0;
        //        u = 0;

        //        for (int i = 0; i < dgw.RowCount; i++)
        //        {
        //            iznos = Convert.ToDouble(dgw.Rows[i].Cells["iznos_ukupno"].FormattedValue.ToString());
        //            u += Math.Round(iznos, 2);
        //            //iznos = Convert.ToDouble(dgw.Rows[i].Cells["iznos_bez_pdva"].FormattedValue.ToString());
        //            iznos = chbObracunPdv.Checked ? Convert.ToDouble(dgw.Rows[i].Cells["iznos_bez_pdva"].FormattedValue.ToString())
        //                : Convert.ToDouble(dgw.Rows[i].Cells["iznos_ukupno"].FormattedValue.ToString());
        //            B_pdv += Math.Round(iznos, 3);
        //        }

        //        SveUkupno = u;
        //        textBox1.Text = "Ukupno sa PDV-om: " + Math.Round(u, 2).ToString("#0.00");
        //        textBox2.Text = "Bez PDV-a: " + Math.Round(B_pdv, 3).ToString("#0.000");
        //        textBox3.Text = "PDV: " + Math.Round(Math.Round(u, 2) - Math.Round(B_pdv, 3), 2).ToString("#0.00");
        //    }
        //}

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            //dodjeli_popust = 0;
            DSpartner = null;
            btnSviDopisi.Enabled = true;
            EnableDisable(false);
            deleteFields();
            txtBrojDopisa.Text = brojPonude();
            edit = false;
            btnDeleteAllDopis.Enabled = false;
            txtBrojDopisa.ReadOnly = false;
            nmGodinaDopisa.ReadOnly = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
            //cbStavkeValuta.Checked = false;
            //txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();
        }

        //private string ReturnSifra(string sifra)
        //{
        //    if (sifra.Length > 3)
        //    {
        //        if (DTpromocije1.Rows.Count > 0 && DTpromocije1.Rows[0]["aktivnost"].ToString() == "DA" && sifra.Substring(0, 3) == "000")
        //        {
        //            return "00000";
        //        }
        //    }
        //    return sifra;
        //}

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            //dodjeli_popust = 0;
            DSpartner = null;

            double tecaj = 1;

            //if (cbStavkeValuta.Checked)
            //    double.TryParse(txtTecaj.Text, out tecaj);

            decimal dec_parse;
            if (Decimal.TryParse(txtPartnerSifra.Text, out dec_parse))
            {
                txtPartnerSifra.Text = dec_parse.ToString();
            }
            else
            {
                MessageBox.Show("Greška kod upisa odredišta.", "Greška");
                return;
            }

            //if (Decimal.TryParse(txtSifraFakturirati.Text, out dec_parse))
            //{
            //    txtSifraFakturirati.Text = dec_parse.ToString();
            //}
            //else
            //{
            //    MessageBox.Show("Greška kod upisa šifre za fakturirati.", "Greška");
            //    return;
            //}

            //if (cbZiroRacun.Text == "" || cbZiroRacun.ValueMember == null)
            //{
            //    MessageBox.Show("Niste odabrali žiro račun.", "Greška");
            //    return;
            //}

            //if (dgw.RowCount == 0)
            //{
            //    MessageBox.Show("Upozorenje.\r\nOva ponuda je prazna.", "Greška");
            //    return;
            //}

            //for (int i = 0; i < dgw.Rows.Count; i++)
            //{
            //    if (dgw.Rows[i].Cells["skladiste"].Value == null & dgw.Rows[i].Cells["oduzmi"].FormattedValue.ToString() == "DA")
            //    {
            //        MessageBox.Show("Ponuda nije spremljena zbog ne odabira skladišta na pojedinim artiklima.", "Greška");
            //        return;
            //    }
            //}

            if (edit)
            {
                UpdateDopis();
                EnableDisable(false);
                deleteFields();
                btnSviDopisi.Enabled = true;
                ControlDisableEnable(1, 0, 0, 1, 0);
                txtBrojDopisa.Text = brojPonude();

                return;
            }

            string broj = brojPonude();
            if (broj.Trim() != txtBrojDopisa.Text.Trim())
            {
                MessageBox.Show("Vaš broj dokumenta već je netko iskoristio.\r\nDodijeljen Vam je novi broj '" + broj + "'.", "Info");
                txtBrojDopisa.Text = broj;
            }

            //if (txtSifraOdrediste.Text == "" || txtSifraFakturirati.Text == "")
            //{
            //    MessageBox.Show("Niste upisali šifru odredišta ili sifru za koga fakturirati.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //if (txtSifraNarKupca.Text == "")
            //{
            //    txtSifraNarKupca.Text = "0";
            //}

            //string obracunPoreza = chbObracunPdv.Checked ? "1" : "0";

            //string stavke_u_valuti = "0";
            //if (cbStavkeValuta.Checked)
            //    stavke_u_valuti = "1";

            //string komercijalista = Properties.Settings.Default.id_zaposlenik;
            //if (cbKomercijalist.SelectedValue != null)
            //    komercijalista = cbKomercijalist.SelectedValue.ToString();

            string sql = "INSERT INTO ponude (broj_ponude,id_odrediste,id_fakturirati,date,vrijedi_do,id_izjava,id_zaposlenik_komercijala," +
                "id_zaposlenik_izradio,model,id_nacin_placanja,zr,id_valuta,otprema,godina_ponude,id_nar_kupca,id_vd,napomena,ukupno,tecaj,stavke_u_valuti," +
                "obracun_poreza, ponuda_nbc) VALUES " +
                " (" +
                 " '" + txtBrojDopisa.Text + "'," +
                " '" + txtPartnerSifra.Text + "'," +
                //" '" + txtSifraFakturirati.Text + "'," +
                " '" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                //" '" + dtpDanaValuta.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                //" '" + cbIzjava.SelectedValue + "'," +
                //" '" + komercijalista + "'," +
                " '" + Properties.Settings.Default.id_zaposlenik + "'," +
                //" '" + txtModel.Text + "'," +
                //" '" + cbNacinPlacanja.SelectedValue.ToString() + "'," +
                //" '" + cbZiroRacun.SelectedValue.ToString() + "'," +
                //" '" + cbValuta.SelectedValue.ToString() + "'," +
                //" '" + cbOtprema.SelectedValue + "'," +
                " '" + nmGodinaDopisa.Value.ToString() + "'," +
                //" '" + txtSifraNarKupca.Text + "'," +
                //" '" + cbVD.SelectedValue + "'," +
                " '" + ispravi(txtText.Text) + "'," +
                " '" + Convert.ToDouble((u * tecaj).ToString()) + "'," +
                //" '" + txtTecaj.Text.Replace(",", ".") + "'," +
                //" '" + stavke_u_valuti + "'," +
                //" '" + obracunPoreza + "'," +
                //" '" + (chbPonudaNbc.Checked ? 1 : 0) + "'" +
                ")";
            provjera_sql(classSQL.insert(sql));

            //string sql_stavke = "";
            //for (int i = 0; i < dgw.Rows.Count; i++)
            //{
            //    ProvjeriDaliPostojiRobaProdaja(dg(i, "sifra"), dgw.Rows[i].Cells[3].Value, i);

            //    if (!chbObracunPdv.Checked)
            //    {
            //        //vpc = Math.Round(Convert.ToDouble(vpc) * (1 + Convert.ToDouble(dg(i, "porezZaIzracun")) / 100), 3).ToString("#0.00");
            //    }

            //    double vpc;
            //    double.TryParse(dg(i, "vpc"), out vpc);
            //    //double nbc = 0;
            //    if (chbPonudaNbc.Checked)
            //        double.TryParse(dg(i, "nc"), out vpc);
            //    sql_stavke = "INSERT INTO ponude_stavke " +
            //    "(sifra,kolicina,vpc,porez,rabat,id_skladiste,naziv,oduzmi,broj_ponude)" +
            //    "VALUES" +
            //    "(" +
            //    "'" + ReturnSifra(dg(i, "sifra")) + "'," +
            //    "'" + dg(i, "kolicina") + "'," +
            //    "'" + (tecaj * vpc).ToString().Replace(",", ".") + "'," +
            //    "'" + dg(i, "porez") + "'," +
            //    "'" + dg(i, "rabat") + "'," +
            //    "'" + dgw.Rows[i].Cells[3].Value + "'," +
            //     "'" + dg(i, "naziv").Replace('\'', '`') + "'," +
            //     "'" + dg(i, "oduzmi") + "'," +
            //    "'" + ttxBrojPonude.Text + "'" +
            //    ")";
            //    provjera_sql(classSQL.insert(sql_stavke));
            //}

            //#region OVAJ DIO SE KORISTI SAMO ŠALJEMO PONUDE NA NEKU UDALJENU PRIMARNU BAZU

            ////*********************OVAJ DIO SE KORISTI SAMO ŠALJEMO PONUDE NA NEKU UDALJENU PRIMARNU BAZU**********************
            //DataTable DTprovjera = classSQL.select("SELECT * FROM postavke_sinkronizacije", "postavke_sinkronizacije").Tables[0];
            //if (DTprovjera.Rows.Count > 0)
            //{
            //    if (DTprovjera.Rows[0]["aktivirano"].ToString() == "1")
            //    {
            //        SpremanjePonudeU_PrimarnuBazu();
            //    }
            //}
            ////*********************KRAJ**********************

            //#endregion OVAJ DIO SE KORISTI SAMO ŠALJEMO PONUDE NA NEKU UDALJENU PRIMARNU BAZU

            classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Izrada nove ponude br." + txtBrojDopisa.Text + "')");

            if (MessageBox.Show("Spremljeno.\r\nŽelite li ispisati dokument?", "Spremljeno.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                printaj(txtBrojDopisa.Text);
            }

            edit = false;
            EnableDisable(false);
            deleteFields();
            btnSviDopisi.Enabled = true;
            ControlDisableEnable(1, 0, 0, 1, 0);
            txtBrojDopisa.Text = brojPonude();
        }

        #region OVAJ DIO SE KORISTI SAMO ŠALJEMO PONUDE NA NEKU UDALJENU PRIMARNU BAZU

        //**************************************************OVAJ DIO SE KORISTI SAMO ŠALJEMO PONUDE NA NEKU UDALJENU PRIMARNU BAZU*****************
        private void SpremanjePonudeU_PrimarnuBazu()
        {
            WebReference.KonektorPostgres kon = new WebReference.KonektorPostgres();
            //string obracunPoreza = chbObracunPdv.Checked ? "1" : "0";
            string broj_na_udaljenoj_bazi = "0";

            DataTable DTzadnjiBroj = kon.GetDataset("SELECT MAX(CAST(broj_ponude AS bigint)) FROM ponude", "", "admin", "q1w2e3r4123").Tables[0];
            if (DTzadnjiBroj.Rows.Count > 0)
            {
                broj_na_udaljenoj_bazi = (Convert.ToInt16(DTzadnjiBroj.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                broj_na_udaljenoj_bazi = "1";
            }

            string _sql = "BEGIN; ";

            _sql += "INSERT INTO ponude (broj_ponude,id_odrediste,id_fakturirati,date,vrijedi_do,id_izjava,id_zaposlenik_komercijala," +
                "id_zaposlenik_izradio,model,id_nacin_placanja,zr,id_valuta,otprema,godina_ponude,id_nar_kupca,id_vd,napomena,ukupno,tecaj," +
                "obracun_poreza) VALUES " +
                " (" +
                 " '" + broj_na_udaljenoj_bazi + "'," +
                " '" + txtPartnerSifra.Text + "'," +
                //" '" + txtSifraFakturirati.Text + "'," +
                " '" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                //" '" + dtpDanaValuta.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                //" '" + cbIzjava.SelectedValue + "'," +
                //" '" + cbKomercijalist.SelectedValue.ToString() + "'," +
                " '" + Properties.Settings.Default.id_zaposlenik + "'," +
                //" '" + txtModel.Text + "'," +
                //" '" + cbNacinPlacanja.SelectedValue.ToString() + "'," +
                //" '" + cbZiroRacun.SelectedValue.ToString() + "'," +
                //" '" + cbValuta.SelectedValue.ToString() + "'," +
                //" '" + cbOtprema.SelectedValue + "'," +
                " '" + nmGodinaDopisa.Value.ToString() + "'," +
                //" '" + txtSifraNarKupca.Text + "'," +
                //" '" + cbVD.SelectedValue + "'," +
                " '" + ispravi(txtText.Text) + "'," +
                " '" + u.ToString().Replace(".", ",") + "'," +
                //" '" + txtTecaj.Text.Replace(",", ".") + "'," +
                //" '" + obracunPoreza + "'" +
                ");";

            //******************STAVKE************************
            //for (int i = 0; i < dgw.Rows.Count; i++)
            //{
            //    _sql += "INSERT INTO ponude_stavke " +
            //    "(sifra,kolicina,vpc,porez,rabat,id_skladiste,naziv,oduzmi,broj_ponude)" +
            //    "VALUES" +
            //    "(" +
            //    "'" + ReturnSifra(dg(i, "sifra")) + "'," +
            //    "'" + dg(i, "kolicina") + "'," +
            //    "'" + dg(i, "vpc").Replace(",", ".") + "'," +
            //    "'" + dg(i, "porez") + "'," +
            //    "'" + dg(i, "rabat") + "'," +
            //    "'" + dgw.Rows[i].Cells[3].Value + "'," +
            //    "'" + dg(i, "naziv") + "'," +
            //    "'" + dg(i, "oduzmi") + "'," +
            //    "'" + broj_na_udaljenoj_bazi + "'" +
            //    ");";
            //}

            _sql = _sql + " COMMIT;";

            kon.Execute(_sql, "admin", "q1w2e3r4123");
        }

        //*****************************************************************************************************************************

        #endregion OVAJ DIO SE KORISTI SAMO ŠALJEMO PONUDE NA NEKU UDALJENU PRIMARNU BAZU

        private void printaj(string broj)
        {
            string[] oibs = new string[2] { "82374273773", "82374273773" };
            if ((Array.IndexOf(oibs, Class.PodaciTvrtka.oibTvrtke)) > -1)
            {
                if (MessageBox.Show("Želite ispisati ponudu na POS printer?", "Ponuda", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int brojPonude = 0;
                    int.TryParse(broj, out brojPonude);
                    PosPrint.classPosPrintPonuda printPonuda = new PosPrint.classPosPrintPonuda(brojPonude);
                    printPonuda.printReceipt(printPonuda.broj_ponude);
                    printPonuda.printaj();
                    printPonuda = null;
                }
                else
                {
                    Report.Faktura.repFaktura pr = new Report.Faktura.repFaktura();
                    pr.dokumenat = "PON";
                    //pr.racunajTecaj = ValutaKuna(cbValuta.Text);
                    pr.broj_dokumenta = broj;
                    //pr.ponudaUNbc = chbPonudaNbc.Checked;
                    pr.ImeForme = "Ponuda";
                    pr.ShowDialog();
                }
            }
            else
            {
                Report.Faktura.repFaktura pr = new Report.Faktura.repFaktura();
                pr.dokumenat = "PON";
                //pr.racunajTecaj = ValutaKuna(cbValuta.Text);
                pr.broj_dokumenta = broj;
                //pr.ponudaUNbc = chbPonudaNbc.Checked;
                pr.ImeForme = "Ponuda";
                pr.ShowDialog();
            }
        }

        //private bool ValutaKuna(string valuta)
        //{
        //    string val = valuta.ToLower();

        //    if (val.Contains("hr"))
        //        return false;
        //    else if (val.Contains("hrk"))
        //        return false;
        //    else if (val.Contains("hrvatska"))
        //    {
        //        return false;
        //    }
        //    else if (val.Contains("kun"))
        //    {
        //        return false;
        //    }
        //    else
        //        return true;
        //}

        //private DataSet DSprovjeraRobaProdaja = new DataSet();

        //private void ProvjeriDaliPostojiRobaProdaja(string sif, object skl, int r)
        //{
        //    DSprovjeraRobaProdaja = classSQL.select("SELECT * FROM roba_prodaja WHERE sifra='" + sif + "' AND id_skladiste='" + skl.ToString() + "'", "roba_prodaja");

        //    if (DSprovjeraRobaProdaja.Tables[0].Rows.Count == 0)
        //    {
        //        string sql = "INSERT INTO roba_prodaja (id_skladiste,kolicina,nc,vpc,porez,sifra) VALUES" +
        //            "('" + skl.ToString() + "','0','" + dg(r, "nc").Replace(",", ".") + "','" + dg(r, "vpc").Replace(",", ".") + "','" + dg(r, "porez") + "','" + sif + "')";
        //        classSQL.insert(sql);
        //    }
        //}

        //private string dg(int row, string cell)
        //{
        //    return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
        //}

        private void UpdateDopis()
        {
            //if (txtSifraNarKupca.Text == "")
            //{
            //    txtSifraNarKupca.Text = "0";
            //}

            //double tecaj = 1;

            //if (cbStavkeValuta.Checked)
            //    double.TryParse(txtTecaj.Text, out tecaj);

            //string stavke_u_valuti = "0";
            //if (cbStavkeValuta.Checked)
            //    stavke_u_valuti = "1";

            //string obracunPoreza = chbObracunPdv.Checked ? "1" : "0";

            //string komercijalista = Properties.Settings.Default.id_zaposlenik;
            //if (cbKomercijalist.SelectedValue != null)
            //    komercijalista = cbKomercijalist.SelectedValue.ToString();

            string sql = "UPDATE ponude SET " +
                " broj_ponude='" + txtBrojDopisa.Text + "'," +
                " id_odrediste='" + txtPartnerSifra.Text + "'," +
                //" id_fakturirati='" + txtSifraFakturirati.Text + "'," +
                " date='" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                //" vrijedi_do='" + dtpDanaValuta.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                //" id_izjava='" + cbIzjava.SelectedValue.ToString() + "'," +
                //" id_zaposlenik_komercijala='" + komercijalista + "'," +
                //" zr='" + cbZiroRacun.SelectedValue.ToString() + "'," +
                //" id_valuta='" + cbValuta.SelectedValue.ToString() + "'," +
                //" otprema='" + cbOtprema.SelectedValue.ToString() + "'," +
                //" model='" + txtModel.Text + "'," +
                //" id_nar_kupca='" + txtSifraNarKupca.Text + "'," +
                //" id_vd='" + cbVD.SelectedValue + "'," +
                //" stavke_u_valuti='" + stavke_u_valuti + "'," +
                " godina_ponude='" + nmGodinaDopisa.Value.ToString() + "'," +
                //" ukupno='" + Convert.ToDouble((u * tecaj).ToString()) + "'," +
                //" tecaj='" + txtTecaj.Text.Replace(",", ".") + "'," +
                //" obracun_poreza='" + obracunPoreza + "'," +
                //" ponuda_nbc='" + (chbPonudaNbc.Checked ? 1 : 0) + "'," +
                " napomena='" + ispravi(txtText.Text) + "'" +
                " WHERE broj_ponude='" + txtBrojDopisa.Text + "'";
            provjera_sql(classSQL.update(sql));

            //for (int i = 0; i < dgw.Rows.Count; i++)
            //{
            //    double vpc;
            //    double.TryParse(dg(i, "vpc"), out vpc);
            //    if (chbPonudaNbc.Checked)
            //        double.TryParse(dg(i, "nc"), out vpc);

            //    if (dgw.Rows[i].Cells["id_stavka"].Value != null && dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString() != "0")
            //    {
            //        if (!chbObracunPdv.Checked)
            //        {
            //            //vpc = Math.Round(Convert.ToDouble(vpc) * (1 + Convert.ToDouble(dg(i, "porezZaIzracun")) / 100), 3).ToString("#0.00");
            //        }

            //        sql = "UPDATE ponude_stavke SET " +
            //        " id_skladiste='" + dgw.Rows[i].Cells["skladiste"].Value + "'," +
            //        " kolicina='" + dg(i, "kolicina") + "'," +
            //        " vpc='" + (tecaj * vpc).ToString().Replace(",", ".") + "'," +
            //        " porez='" + dg(i, "porez") + "'," +
            //        " naziv='" + dg(i, "naziv").Replace('\'', '`') + "'," +
            //        " sifra='" + ReturnSifra(dg(i, "sifra")) + "'," +
            //        " rabat='" + dg(i, "rabat") + "' " +
            //        " WHERE id_stavka='" + dg(i, "id_stavka") + "'";
            //        provjera_sql(classSQL.update(sql));
            //    }
            //    else
            //    {
            //        string sql_stavke = "INSERT INTO ponude_stavke (" +
            //        "id_skladiste,sifra,rabat,broj_ponude,vpc,naziv,kolicina,oduzmi,porez)" +
            //        " VALUES (" +
            //        "'" + dgw.Rows[i].Cells["skladiste"].Value + "'," +
            //        "'" + ReturnSifra(dg(i, "sifra")) + "'," +
            //        "'" + dg(i, "rabat") + "'," +
            //        "'" + ttxBrojPonude.Text + "'," +
            //        "'" + (tecaj * vpc).ToString().Replace(",", ".") + "'," +
            //        "'" + dg(i, "naziv").Replace('\'', '`') + "'," +
            //        "'" + dg(i, "kolicina") + "'," +
            //        "'" + dg(i, "oduzmi") + "'," +
            //        "'" + dg(i, "porez") + "'" +
            //        ")";
            //        provjera_sql(classSQL.insert(sql_stavke));
            //    }

            //    provjera_sql(classSQL.update(sql));
            //}

            if (MessageBox.Show("Spremljeno.\r\nŽelite li ispisati dokument?", "Spremljeno.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                printaj(txtBrojDopisa.Text);
            }
            edit = false;
            EnableDisable(false);
            deleteFields();
            btnSpremi.Enabled = false;
            btnSviDopisi.Enabled = true;

            classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Editiranje ponude br." + txtBrojDopisa.Text + "')");
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        //private void btnOpenRoba_Click(object sender, EventArgs e)
        //{
        //    preuzetoSWeba = false;

        //    frmRobaTrazi roba_trazi = new frmRobaTrazi();
        //    roba_trazi.ShowDialog();
        //    string propertis_sifra = Properties.Settings.Default.id_roba.Trim();
        //    if (propertis_sifra != "")
        //    {
        //        //for (int y = 0; y < dgw.Rows.Count; y++)
        //        //{
        //        //    if (propertis_sifra == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString())
        //        //    {
        //        //        MessageBox.Show("Artikl ili usluga već postoje!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        //        return;
        //        //    }
        //        //}

        //        string sql = "SELECT * FROM roba WHERE sifra='" + propertis_sifra + "'";

        //        DTRoba = classSQL.select(sql, "roba").Tables[0];
        //        if (DTRoba.Rows.Count > 0)
        //        {
        //            //txtSifra_robe.BackColor = Color.White;
        //            //SetRoba();
        //            //dgw.Select();
        //            ttxBrojPonude.Enabled = false;
        //            nmGodinaPonude.Enabled = false;
        //            //cbValuta.Enabled = false;
        //        }
        //        else
        //        {
        //            MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}

        /// <summary>
        /// FUNKCIJA POSTAVLJA POPUST NA SVE STAVKE
        /// </summary>

        //private int dodjeli_popust = 0;

        //private void PartnerPostaviPopust()
        //{
        //    try
        //    {
        //        decimal rabat = 0;

        //        if (DSpartner != null)
        //        {
        //            decimal.TryParse(DSpartner.Tables[0].Rows[0]["popust"].ToString(), out rabat);

        //            if (dodjeli_popust == -1 || rabat == 0)
        //            {
        //                return;
        //            }
        //            else if (dodjeli_popust != -1 && dodjeli_popust != 1)
        //            {
        //                if (MessageBox.Show("Ovaj partner ima popust od " + rabat.ToString() + "%\r\nDali ste sigurni da želite dodijeliti navedeni popust.", "Popust na račun", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //                    dodjeli_popust = 1;
        //                else
        //                    dodjeli_popust = -1;
        //            }

        //            //foreach (DataGridViewRow r in dgw.Rows)
        //            //{
        //            //    r.Cells["rabat"].Value = Math.Round(rabat, 5).ToString("#0.00000");

        //            //    //izracun();
        //            //}
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}

        private DataSet DSpartner;

        private void btnPartner_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DSpartner = new DataSet();
                DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (DSpartner.Tables[0].Rows.Count > 0)
                {
                    //PartnerPostaviPopust();
                    txtPartnerSifra.Text = DSpartner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartnerNaziv.Text = DSpartner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    //txtSifraFakturirati.Text = txtSifraOdrediste.Text;
                    //txtPartnerNaziv1.Text = DSpartner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    //cbVD.Select();
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
                DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (DSpartner.Tables[0].Rows.Count > 0)
                {
                    //PartnerPostaviPopust();
                    txtPartnerSifra.Text = DSpartner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartnerNaziv.Text = DSpartner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    //txtSifraFakturirati.Text = txtSifraOdrediste.Text;
                    //txtPartnerNaziv1.Text = DSpartner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    //cbVD.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void btnSviDopisi_Click(object sender, EventArgs e)
        {
            //dodjeli_popust = 0;
            DSpartner = null;
            frmSviDopisi objForm3 = new frmSviDopisi();
            objForm3.sifra_ponude = "";
            objForm3.MainForm = this;
            broj_ponude_edit = null;
            objForm3.ShowDialog();
            if (broj_ponude_edit != null)
            {
                fillPonude();
                EnableDisable(true);
                edit = true;
                txtBrojDopisa.ReadOnly = true;
                nmGodinaDopisa.ReadOnly = true;
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

        private string broj_celije = "";

        private void fillPonude()
        {
            //fill header

            EnableDisable(true);
            edit = true;

            DSponude = classSQL.select("SELECT * FROM ponude WHERE broj_ponude = '" + broj_ponude_edit + "'", "fakture").Tables[0];

            //cbVD.SelectedValue = DSponude.Rows[0]["id_vd"].ToString();
            txtPartnerSifra.Text = DSponude.Rows[0]["id_odrediste"].ToString();
            //txtSifraFakturirati.Text = DSponude.Rows[0]["id_fakturirati"].ToString();
            try
            {
                txtPartnerNaziv.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DSponude.Rows[0]["id_odrediste"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString();
                //txtPartnerNaziv1.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DSponude.Rows[0]["id_fakturirati"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška sa partnerom.\r\n" + ex.ToString());
            }
            //txtSifraNacinPlacanja.Text = DSponude.Rows[0]["id_nacin_placanja"].ToString();
            //txtModel.Text = DSponude.Rows[0]["model"].ToString();
            //txtSifraNarKupca.Text = DSponude.Rows[0]["id_nar_kupca"].ToString();
            //txtNarKupca1.Text = DSponude.Rows[0]["id_nar_kupca"].ToString();
            //cbOtprema.SelectedValue = DSponude.Rows[0]["otprema"].ToString();
            txtText.Text = DSponude.Rows[0]["napomena"].ToString();
            dtpDatum.Value = Convert.ToDateTime(DSponude.Rows[0]["date"].ToString());
            //dtpDanaValuta.Value = Convert.ToDateTime(DSponude.Rows[0]["vrijedi_do"].ToString());
            //try
            //{
                //cbKomercijalist.SelectedValue = DSponude.Rows[0]["id_zaposlenik_komercijala"].ToString();
            //}
            //catch { }
            //cbNacinPlacanja.SelectedValue = DSponude.Rows[0]["id_nacin_placanja"].ToString();
            //cbZiroRacun.SelectedValue = DSponude.Rows[0]["zr"].ToString();
            //cbValuta.SelectedValue = DSponude.Rows[0]["id_valuta"].ToString();
            //cbNarKupca.SelectedValue = DSponude.Rows[0]["id_nar_kupca"].ToString();
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + DSponude.Rows[0]["id_zaposlenik_izradio"].ToString() + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
            txtBrojDopisa.Text = DSponude.Rows[0]["broj_ponude"].ToString();
            //chbObracunPdv.Checked = DSponude.Rows[0]["obracun_poreza"].ToString() == "1" ? true : false;
            //chbPonudaNbc.Checked = Convert.ToBoolean(DSponude.Rows[0]["ponuda_nbc"].ToString());

            //if (DSponude.Rows[0]["stavke_u_valuti"].ToString() == "1")
            //{
            //    cbStavkeValuta.Checked = true;
            //}
            //else
            //{
            //    cbStavkeValuta.Checked = false;
            //}

            //decimal tecaj = 1;

            //if (cbStavkeValuta.Checked)
            //    decimal.TryParse(DSponude.Rows[0]["tecaj"].ToString(), out tecaj);

            //--------fill faktura stavke------------------------------

        //    DataTable dtR = new DataTable();
        //    string sql = "SELECT * FROM ponude_stavke WHERE broj_ponude = '" + DSponude.Rows[0]["broj_ponude"].ToString() + "'";
        //    DSFS = classSQL.select(sql, "broj_ponude").Tables[0];

        //    string[] arrRoba = new string[5];

        //    for (int i = 0; i < DSFS.Rows.Count; i++)
        //    {
        //        dgw.Rows.Add();
        //        int br = dgw.Rows.Count - 1;
        //        string s;
        //        int rowBR = dgw.CurrentRow.Index;

        //        string sql_postoji_u_robi = string.Format("select * from roba where sifra = '{0}';", DSFS.Rows[i]["sifra"].ToString());
        //        DataSet dsPostojiURobi = classSQL.select(sql_postoji_u_robi, "roba");
        //        if (dsPostojiURobi == null || dsPostojiURobi.Tables.Count == 0 || dsPostojiURobi.Tables[0].Rows.Count == 0) {
        //                arrRoba[0] = DSFS.Rows[i]["sifra"].ToString();
        //                arrRoba[1] = DSFS.Rows[i]["naziv"].ToString();
        //                arrRoba[2] = DSFS.Rows[i]["porez"].ToString();
        //                arrRoba[3] = DSFS.Rows[i]["vpc"].ToString();
        //                arrRoba[4] = DSFS.Rows[i]["oduzmi"].ToString();
        //                UmetniStavkuAkoNePostoji(arrRoba);
        //        }

        //        SQL.ClassSkladiste.provjeri_roba_prodaja(DSFS.Rows[i]["sifra"].ToString(), DSFS.Rows[i]["id_skladiste"].ToString());
        //        if (DSFS.Rows[i]["sifra"].ToString() != "Ručno")
        //        {
        //            if (DSFS.Rows[i]["oduzmi"].ToString() == "DA")
        //            {
        //                s = "SELECT roba_prodaja.nc,roba.naziv,roba.jm,roba_prodaja.sifra,roba_prodaja.id_skladiste,roba.oduzmi,roba_prodaja.porez" +
        //                    " FROM roba_prodaja LEFT JOIN roba ON roba_prodaja.sifra=roba.sifra WHERE roba_prodaja.sifra='" + DSFS.Rows[i]["sifra"].ToString() + "'" +
        //                    " AND roba_prodaja.id_skladiste='" + DSFS.Rows[i]["id_skladiste"].ToString() + "'";
        //            }
        //            else
        //            {
        //                s = "SELECT * FROM roba WHERE sifra='" + DSFS.Rows[i]["sifra"].ToString() + "'";
        //            }
        //            dtR = classSQL.select(s, "roba_prodaja").Tables[0];

        //            if (dtR.Rows.Count == 0)
        //            {
        //                arrRoba[0] = DSFS.Rows[i]["sifra"].ToString();
        //                arrRoba[1] = DSFS.Rows[i]["naziv"].ToString();
        //                arrRoba[2] = DSFS.Rows[i]["porez"].ToString();
        //                arrRoba[3] = DSFS.Rows[i]["vpc"].ToString();
        //                arrRoba[4] = DSFS.Rows[i]["oduzmi"].ToString();
        //                UmetniStavkuAkoNePostoji(arrRoba);
        //                dtR = classSQL.select(s, "roba_prodaja").Tables[0];
        //            }
        //        }

        //        try
        //        {
        //            for (int y = 0; y < dtR.Rows.Count; y++)
        //            {
        //                if (dtR.Rows[y]["porez"].ToString() != "0")
        //                {
        //                    chbObracunPdv.Checked = true;
        //                    break;
        //                }
        //            }
        //        }
        //        catch { }

        //        string rucno = "";
        //        string jmj = "";
        //        string nc = "";
        //        string oduzmi = "";

        //        decimal vpc;
        //        decimal.TryParse(DSFS.Rows[i]["vpc"].ToString(), out vpc);

        //        if (DSFS.Rows[i]["sifra"].ToString() == "Ručno")
        //        {
        //            rucno = "Ručno";
        //            jmj = "kom";
        //            nc = "0,00";
        //            oduzmi = "NE";
        //        }
        //        else
        //        {
        //            rucno = dtR.Rows[0]["sifra"].ToString();
        //            jmj = dtR.Rows[0]["jm"].ToString();
        //            nc = string.Format("{0:0.00}", dtR.Rows[0]["nc"].ToString());
        //            oduzmi = dtR.Rows[0]["oduzmi"].ToString();
        //        }
        //        dgw.Rows[br].Cells[0].Value = i + 1;
        //        dgw.Rows[br].Cells["sifra"].Value = rucno;
        //        dgw.Rows[br].Cells["naziv"].Value = DSFS.Rows[i]["naziv"].ToString();
        //        try { dgw.Rows[br].Cells["skladiste"].Value = DSFS.Rows[i]["id_skladiste"].ToString(); } catch (Exception) { }
        //        dgw.Rows[br].Cells["jmj"].Value = jmj;
        //        dgw.Rows[br].Cells["cijena_bez_pdva"].Value = Math.Round(vpc / tecaj, 3).ToString("#0.000");
        //        dgw.Rows[br].Cells["kolicina"].Value = DSFS.Rows[i]["kolicina"].ToString();
        //        dgw.Rows[br].Cells["porez"].Value = DSFS.Rows[i]["porez"].ToString();
        //        dgw.Rows[br].Cells["porezZaIzracun"].Value = DSFS.Rows[i]["porez"].ToString();
        //        dgw.Rows[br].Cells["rabat"].Value = DSFS.Rows[i]["rabat"].ToString();
        //        dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
        //        dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
        //        dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
        //        dgw.Rows[br].Cells["vpc"].Value = Math.Round(vpc / tecaj, 3).ToString("#0.000");
        //        //dgw.Rows[br].Cells["mpc"].Value = String.Format("{0:0.00}", DSFS.Rows[i]["mpc"]);
        //        dgw.Rows[br].Cells["nc"].Value = nc;
        //        dgw.Rows[br].Cells["id_stavka"].Value = DSFS.Rows[i]["id_stavka"].ToString();
        //        dgw.Rows[br].Cells["oduzmi"].Value = oduzmi;

        //        dgw.Select();
        //        try
        //        {
        //            dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[2];
        //        }
        //        catch
        //        {
        //            dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[5];
        //        }
        //        broj_celije = i.ToString();
        //        //izracun();
        //        ControlDisableEnable(0, 1, 1, 0, 1);
        //        PaintRows(dgw);
        //    }

        //    dgw.Columns["cijena_bez_pdva"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    dgw.Columns["rabat_iznos"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    dgw.Columns["iznos_bez_pdva"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    dgw.Columns["iznos_ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

        //    dgw.Columns["cijena_bez_pdva"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    dgw.Columns["rabat_iznos"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    dgw.Columns["iznos_bez_pdva"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    dgw.Columns["iznos_ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        //private void UmetniStavkuAkoNePostoji(string[] arrRoba)
        //{
        //    decimal veleprodajna, maloprodajna;
        //    decimal porez;
        //    decimal.TryParse(arrRoba[3], out veleprodajna);
        //    decimal.TryParse(arrRoba[2], out porez);

        //    maloprodajna = veleprodajna + (veleprodajna * porez / 100);

        //    string sql = "";
        //    sql = "INSERT INTO roba (naziv,sifra,ean,id_grupa,id_podgrupa, nc,vpc,mpc,id_zemlja_porijekla,id_zemlja_uvoza, " +
        //        " id_partner,id_manufacturers,porez,oduzmi,jm, opis, jamstvo, akcija, link_za_slike) " +
        //        "VALUES ('" + arrRoba[1].Replace('\'', '"') + "'," +
        //    "'" + arrRoba[0] + "'," +
        //    "'-1'," +
        //    "'1'," +
        //    "'1'," +
        //    "'0'," +
        //    "'" + veleprodajna.ToString().Replace(",", ".") + "'," +
        //    "'" + maloprodajna.ToString().Replace(".", ",") + "'," +
        //    "'247'," +
        //    "'247'," +
        //    "'830'," +
        //    "'4'," +
        //    "'" + porez.ToString() + "'," +
        //    "'" + arrRoba[4] + "'," +
        //    "'kom' ," +
        //    "''," +
        //    "'0'," +
        //    "'0'," +
        //    "'')";

        //    classSQL.insert(sql);
        //}

        //private void btnObrisi_Click(object sender, EventArgs e)
        //{
        //    if (dgw.RowCount == 0)
        //    {
        //        return;
        //    }

        //    if (dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].Value != null)
        //    {
        //        if (MessageBox.Show("Da li ste sigurni da želite obrisati ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
        //        {
        //            classSQL.delete("DELETE FROM ponude_stavke WHERE id_stavka='" + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() + "'");
        //            dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
        //            classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje stavke sa ponude br." + ttxBrojPonude.Text + "')");
        //            MessageBox.Show("Obrisano.");
        //        }
        //    }
        //    else
        //    {
        //        dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
        //        MessageBox.Show("Obrisano.");
        //    }
        //}

        private void btnDeleteAllDopis_Click(object sender, EventArgs e)
        {
            //dodjeli_popust = 0;
            DSpartner = null;
            if (MessageBox.Show("Brisanjem ove ponude brišete i količinu robe sa skladišta. Da li ste sigurni da želite obrisati ovu ponudu?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                classSQL.delete("DELETE FROM ponude_stavke WHERE broj_ponude='" + txtBrojDopisa.Text + "'");
                classSQL.delete("DELETE FROM ponude WHERE broj_ponude='" + txtBrojDopisa.Text + "'");
                classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje cijele ponude br." + txtBrojDopisa.Text + "')");
                MessageBox.Show("Obrisano.");

                edit = false;
                EnableDisable(false);
                deleteFields();
                btnDeleteAllDopis.Enabled = false;
                //btnObrisi.Enabled = false;
                ControlDisableEnable(1, 0, 0, 1, 0);
            }
        }

        private void ttxBrojPonude_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT broj_ponude FROM ponude WHERE godina_ponude='" + nmGodinaDopisa.Value.ToString() + "' AND broj_ponude='" + txtBrojDopisa.Text + "'", "fakture").Tables[0];
                deleteFields();
                if (DT.Rows.Count == 0)
                {
                    if (brojPonude() == txtBrojDopisa.Text.Trim())
                    {
                        edit = false;
                        EnableDisable(true);
                        btnSviDopisi.Enabled = false;
                        txtBrojDopisa.Text = brojPonude();
                        btnDeleteAllDopis.Enabled = false;
                        txtPartnerSifra.Select();
                        txtBrojDopisa.ReadOnly = true;
                        nmGodinaDopisa.ReadOnly = true;
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    broj_ponude_edit = txtBrojDopisa.Text;
                    fillPonude();
                    EnableDisable(true);
                    edit = true;
                    btnDeleteAllDopis.Enabled = true;
                    txtPartnerSifra.Select();
                    txtBrojDopisa.ReadOnly = true;
                    nmGodinaDopisa.ReadOnly = true;
                }
                txtPartnerSifra.Select();
                ControlDisableEnable(0, 1, 1, 0, 1);
            }
        }

        //private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    double dec_parse;
        //    if (dgw.CurrentCell.ColumnIndex == 3 & dgw.CurrentRow.Cells["oduzmi"].FormattedValue.ToString() == "DA")
        //    {
        //        if (!preuzetoSWeba)
        //        {
        //            SetCijenaSkladiste();
        //        }
        //    }
        //    else if (dgw.CurrentCell.ColumnIndex == 6)
        //    {
        //        if (!Double.TryParse(dgw.CurrentRow.Cells["Porez"].FormattedValue.ToString(), out dec_parse))
        //        {
        //            dgw.CurrentRow.Cells["Porez"].Value = "0,00";
        //            MessageBox.Show("Greška kod poreza.", "Greška");
        //            return;
        //        }
        //        dgw.CurrentRow.Cells["porezZaIzracun"].Value = dgw.CurrentRow.Cells[6].Value;
        //    }
        //    else if (dgw.CurrentCell.ColumnIndex == 9)
        //    {
        //        if (dgw.CurrentRow.Cells["skladiste"].Value == null & dgw.CurrentRow.Cells["oduzmi"].FormattedValue.ToString() == "DA")
        //        {
        //            MessageBox.Show("Niste odabrali skladište", "Greška");
        //            return;
        //        }

        //        dgw.CurrentCell.Selected = false;
        //        txtSifra_robe.Text = "";
        //        txtSifra_robe.BackColor = Color.Silver;
        //        txtSifra_robe.Select();
        //    }
        //    else if (dgw.CurrentCell.ColumnIndex == 8)
        //    {
        //        try
        //        {
        //            if (!Double.TryParse(dgw.CurrentRow.Cells["MPC"].FormattedValue.ToString(), out dec_parse))
        //            {
        //                dgw.CurrentRow.Cells["MPC"].Value = "0,00";
        //                MessageBox.Show("Greška kod MPC.", "Greška");
        //                return;
        //            }

        //            if (chbObracunPdv.Checked)
        //            {
        //                dgw.CurrentRow.Cells["vpc"].Value = Math.Round(Convert.ToDouble(dgw.CurrentRow.Cells["mpc"].FormattedValue.ToString()) / Convert.ToDouble(1 + (Convert.ToDouble(dgw.CurrentRow.Cells["porezZaIzracun"].FormattedValue.ToString()) / 100)), 3).ToString("#0.000");
        //            }
        //            else
        //            {
        //                dgw.CurrentRow.Cells["vpc"].Value = Math.Round(Convert.ToDouble(dgw.CurrentRow.Cells["mpc"].FormattedValue.ToString()), 3).ToString("#0.000");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Koristite enter za sljedeću kolonu." + ex.ToString());
        //        }
        //    }
        //    //if (dgw.CurrentRow.Cells["sifra"].Value.ToString() == "Ručno")
        //    //{
        //    //}
        //    //else { }
        //    izracun();
        //}

        //private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (dgw.Rows.Count < 1)
        //        return;
        //    dgw.BeginEdit(true);
        //}

        //private void frmPonude_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    if (MainForm != null)
        //    {
        //    }
        //}

        //private void rtbNapomena_KeyDown_1(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (rtbNapomena.Text == "")
        //        {
        //            e.SuppressKeyPress = true;
        //            //txtSifra_robe.Select();
        //        }
        //    }
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            decimal dec_parse;

            if (Decimal.TryParse(txtPartnerSifra.Text, out dec_parse))
            {
                txtPartnerSifra.Text = dec_parse.ToString();
            }
            else
            {
                MessageBox.Show("Greška kod upisa odredišta.", "Greška");
                return;
            }

            //if (Decimal.TryParse(txtSifraFakturirati.Text, out dec_parse))
            //{
            //    txtSifraFakturirati.Text = dec_parse.ToString();
            //}
            //else
            //{
            //    MessageBox.Show("Greška kod upisa šifre za fakturirati.", "Greška");
            //    return;
            //}

            //if (cbZiroRacun.Text == "" || cbZiroRacun.ValueMember == null)
            //{
            //    MessageBox.Show("Niste odabrali žiro račun.", "Greška");
            //    return;
            //}

            //if (dgw.RowCount == 0)
            //{
            //    MessageBox.Show("Upozorenje.\r\nOva ponuda je prazna.", "Greška");
            //    return;
            //}

            if (txtPartnerSifra.Text == "")
            {
                MessageBox.Show("Niste upisali šifru odredišta ili sifru za koga fakturirati.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //if (txtSifraNarKupca.Text == "")
            //{
            //    txtSifraNarKupca.Text = "0";
            //}

            string sql = "DELETE FROM ispis_fakture; DELETE FROM ispis_faktura_stavke;";
            provjera_sql(classSQL.update(sql));

            sql = "INSERT INTO ispis_fakture (broj_fakture,id_odrediste,id_fakturirati,date,vrijedi_do,id_izjava,id_zaposlenik_komercijala," +
                "id_zaposlenik_izradio,model,id_nacin_placanja,zr,id_valuta,otprema,godina_fakture,id_nar_kupca,id_vd,napomena,ukupno,tecaj) VALUES " +
                " (" +
                 " '" + txtBrojDopisa.Text + "'," +
                " '" + txtPartnerSifra.Text + "'," +
                //" '" + txtSifraFakturirati.Text + "'," +
                " '" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                //" '" + dtpDanaValuta.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                //" '" + cbIzjava.SelectedValue + "'," +
                //" '" + cbKomercijalist.SelectedValue.ToString() + "'," +
                " '" + Properties.Settings.Default.id_zaposlenik + "'," +
                //" '" + txtModel.Text + "'," +
                //" '" + cbNacinPlacanja.SelectedValue.ToString() + "'," +
                //" '" + cbZiroRacun.SelectedValue.ToString() + "'," +
                //" '" + cbValuta.SelectedValue.ToString() + "'," +
                //" '" + cbOtprema.SelectedValue + "'," +
                " '" + nmGodinaDopisa.Value.ToString() + "'," +
                //" '" + txtSifraNarKupca.Text + "'," +
                //" '" + cbVD.SelectedValue + "'," +
                " '" + txtText.Text + "'," +
                " '" + Convert.ToDouble(u.ToString()) + "'," +
                //" '" + txtTecaj.Text.Replace(",", ".") + "'" +
                ")";

            provjera_sql(classSQL.insert(sql));

            //string sql_stavke = "";
            //for (int i = 0; i < dgw.Rows.Count; i++)
            //{
            //    ProvjeriDaliPostojiRobaProdaja(dg(i, "sifra"), dgw.Rows[i].Cells[3].Value, i);

            //    string vpc = dg(i, "vpc");

            //    if (classSQL.remoteConnectionString == "")
            //    {
            //        vpc = vpc.Replace(",", ".");
            //        if (chbPonudaNbc.Checked)
            //            vpc = dg(i, "nc").ToString().Replace(",", ".");
            //    }
            //    else
            //    {
            //        vpc = vpc.Replace(".", ",");
            //        if (chbPonudaNbc.Checked)
            //            vpc = dg(i, "nc").ToString().Replace(".", ",");
            //    }

            //    if (!chbObracunPdv.Checked)
            //    {
            //        //vpc = Math.Round(Convert.ToDouble(vpc) * (1 + Convert.ToDouble(dg(i, "porezZaIzracun")) / 100), 3).ToString("#0.00");
            //    }

            //    //if (chbPonudaNbc.Checked)

            //    sql_stavke = "INSERT INTO ispis_faktura_stavke " +
            //    "(sifra,kolicina,vpc,porez,rabat,id_skladiste,naziv,oduzmi,broj_fakture)" +
            //    "VALUES" +
            //    "(" +
            //    "'" + ReturnSifra(dg(i, "sifra")) + "'," +
            //    "'" + dg(i, "kolicina") + "'," +
            //    "'" + vpc.Replace(",", ".") + "'," +
            //    "'" + dg(i, "porez") + "'," +
            //    "'" + dg(i, "rabat") + "'," +
            //    "'" + dgw.Rows[i].Cells[3].Value + "'," +
            //     "'" + dg(i, "naziv") + "'," +
            //     "'" + dg(i, "oduzmi") + "'," +
            //    "'" + ttxBrojPonude.Text + "'" +
            //    ")";
            //    provjera_sql(classSQL.insert(sql_stavke));
            //}

            Report.Faktura.repFaktura pr = new Report.Faktura.repFaktura();
            pr.samoIspis = true;
            //pr.racunajTecaj = ValutaKuna(cbValuta.Text);
            pr.dokumenat = "PON";
            //pr.ponudaUNbc = chbPonudaNbc.Checked;
            pr.broj_dokumenta = txtBrojDopisa.Text;
            pr.ImeForme = "Faktura";
            pr.ShowDialog();
        }

        //private void checkBox1_CheckedChanged(object sender, EventArgs e)
        //{
        //    bool t = chbObracunPdv.Checked;

        //    if (dgw.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dgw.Rows.Count; i++)
        //        {
        //            dgw.CurrentCell = dgw[0, i];
        //            izracun();
        //        }
        //    }
        //}

        //private void label9_Click(object sender, EventArgs e)
        //{
        //    preuzetoSWeba = false;

        //    dgw.Rows.Add("", "Ručno", "", prvo_skladiste, "kom", "0", "25", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "NE", "0");
        //    RedniBroj();
        //    dgw.Select();
        //    dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[2];
        //    dgw.BeginEdit(true);
        //}

        //private void RedniBroj()
        //{
        //    //for (int i = 0; i < dgw.Rows.Count; i++)
        //    //{
        //    //    dgw.Rows[i].Cells["br"].Value = i + 1;
        //    //}
        //}

        public string ispravi(string text)
        {
            string ttx = text;
            ttx = ttx.Replace("\\", "");
            ttx = ttx.Replace("\'", "");
            ttx = ttx.Replace("\"", "");
            return ttx;
        }

        //private void chbPonudaNbc_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (dgw.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dgw.Rows.Count; i++)
        //            {
        //                dgw.CurrentCell = dgw[0, i];
        //                izracun();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void txtSifra_robe_TextChanged(object sender, EventArgs e)
        //{
        //}
    }
}