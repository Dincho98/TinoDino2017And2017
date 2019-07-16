using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmRadniNalogSerivs2016 : Form
    {
        public string servisBarkode { get; set; }
        public int servisGodina { get; set; }

        public frmRadniNalogSerivs2016()
        {
            InitializeComponent();
        }

        private DataTable DTRoba = new DataTable();
        private DataSet DS_Skladiste = new DataSet();

        //DataSet DS_ZiroRacun = new DataSet();
        //DataSet DS_Zaposlenik = new DataSet();
        //DataSet DSValuta = new DataSet();
        //DataSet DSIzjava = new DataSet();
        //DataSet DSnazivPlacanja = new DataSet();
        //DataSet DSvd = new DataSet();
        //DataTable DTpromocije1;
        //DataTable DTOtprema = new DataTable();
        private DataTable DSrns = new DataTable();

        private DataTable DSFS = new DataTable();
        private DataTable DTpostavke = new DataTable();
        private List<Statusi> lStatusi;// = new List<Statusi>();

        //double u = 0;
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
            MyDataGrid2016.MainForm = this;
            lStatusi = new List<Statusi>() {
                new Statusi(){ ID = 0, Naziv=  "Nulti servis"},
                new Statusi(){ ID = 1, Naziv=  "Zaprimljeno"},
                new Statusi(){ ID = 2, Naziv=  "Servis u toku"},
                new Statusi(){ ID = 3, Naziv=  "Na vanjskom servisu"},
                new Statusi(){ ID = 4, Naziv=  "Završen servis"},
                new Statusi(){ ID = 5, Naziv=  "Povrat kupcu"},
            };

            cmbStatus.ValueMember = "ID";
            cmbStatus.DisplayMember = "Naziv";
            cmbStatus.DataSource = lStatusi;

            this.Paint += new PaintEventHandler(Form1_Paint);

            DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            numeric();
            fillComboBox();
            //EnableDisable(false);
            ControlDisableEnable(1, 0, 0, 1, 0);
            if (servisBarkode != null) { fillRN(); edit = true; }
            txtBarkode.Select();
            cmbStatus.SelectedIndex = 0;
            disableHeader(true);
        }

        private class MyDataGrid2016 : System.Windows.Forms.DataGridView
        {
            public static frmRadniNalogSerivs2016 MainForm { get; set; }

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

        public class Statusi
        {
            public int ID { get; set; }
            public string Naziv { get; set; }
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
            }
        }

        private void LeftDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            if (d.CurrentCell.ColumnIndex == 2)
            {
            }
            else if (d.CurrentCell.ColumnIndex == 3)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[2];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 4)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
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
            if (d.CurrentCell.ColumnIndex == 2)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 3)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 4)
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
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[2];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index - 1].Cells[2];
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
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[2];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index + 1].Cells[2];
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

        private string brojRN()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj AS bigint)) FROM radni_nalog_servis", "radni_nalog_servis").Tables[0];
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
            ////fill ziroracun
            //DS_ZiroRacun = classSQL.select("SELECT * FROM ziro_racun", "ziro_racun");
            //cbZiroRacun.DataSource = DS_ZiroRacun.Tables[0];
            //cbZiroRacun.DisplayMember = "ziroracun";
            //cbZiroRacun.ValueMember = "id_ziroracun";
            //cbZiroRacun.SelectedValue = "1";
            ////fill otprema
            //DTOtprema = classSQL.select("SELECT * FROM otprema", "otprema").Tables[0];
            //cbOtprema.DataSource = DTOtprema;
            //cbOtprema.DisplayMember = "naziv";
            //cbOtprema.ValueMember = "id_otprema";

            //fill komercijalist

            ////fill izjava
            //DSIzjava = classSQL.select("SELECT * FROM izjava ORDER BY id_izjava", "izjava");
            //cbIzjava.DataSource = DSIzjava.Tables[0];
            //cbIzjava.DisplayMember = "izjava";
            //cbIzjava.ValueMember = "id_izjava";

            ////fill vrsta dokumenta
            //DSvd = classSQL.select("SELECT * FROM fakture_vd  WHERE grupa = 'rn' ORDER BY id_vd", "fakture_vd");
            //cbVD.DataSource = DSvd.Tables[0];
            //cbVD.DisplayMember = "vd";
            //cbVD.ValueMember = "id_vd";

            ////fill nacin_placanja
            //DSnazivPlacanja = classSQL.select("SELECT * FROM nacin_placanja", "nacin_placanja");
            //cbNacinPlacanja.DataSource = DSnazivPlacanja.Tables[0];
            //cbNacinPlacanja.DisplayMember = "naziv_placanja";
            //cbNacinPlacanja.ValueMember = "id_placanje";
            //cbNacinPlacanja.SelectedValue = 3;
            //txtSifraNacinPlacanja.Text = cbNacinPlacanja.SelectedValue.ToString();

            ////DS Valuta
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
            for (int i = 0; i < DS_Skladiste.Tables[0].Rows.Count; i++)
            {
                DTSK.Rows.Add(DS_Skladiste.Tables[0].Rows[i]["id_skladiste"], DS_Skladiste.Tables[0].Rows[i]["skladiste"]);
            }

            //skladiste.DataSource = DTSK;
            //skladiste.DataPropertyName = "skladiste";
            //skladiste.DisplayMember = "skladiste";
            //skladiste.HeaderText = "Skladište";
            //skladiste.Name = "skladiste";
            //skladiste.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            //skladiste.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            //skladiste.ValueMember = "id_skladiste";

            //fill tko je prijavljen
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();

            DS_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste");
        }

        private void numeric()
        {
            nmGodina.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nmGodina.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nmGodina.Value = Convert.ToInt16(DateTime.Now.Year.ToString());
            ;
        }

        //private void EnableDisable (bool x) {
        //    txtSifraPartnera.Enabled = x;
        //    txtPartnerNaziv.Enabled = x;
        //    txtNapomena.Enabled = x;
        //    btnPartner.Enabled = x;
        //    dtpDatum.Enabled = x;
        //    //txtBarkode.ReadOnly = x;
        //    txtArtikl.Enabled = x;
        //    btnSrchArtikl.Enabled = x;
        //    cmbStatus.Enabled = x;
        //    btnNoviStatus.Enabled = x;
        //    txtSerijskiBroj.Enabled = x;
        //    nmGodina.ReadOnly = x;
        //}

        private void deleteFields()
        {
            txtSifraPartnera.Text = "";
            txtPartnerNaziv.Text = "";
            txtSerijskiBroj.Text = "";
            txtArtikl.Text = "";
            txtBarkode.Text = "";
            txtArtikl.Tag = null;
            dtpDatum.Value = DateTime.Now;
            txtNapomena.Text = "";
            cmbStatus.SelectedIndex = 0;
            dgw.Rows.Clear();
            otvaranjezaEditiranje = false;
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
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtSifraPartnera.Text == "")
                {
                    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                    partnerTrazi.ShowDialog();
                    if (Properties.Settings.Default.id_partner != "")
                    {
                        DataSet partner = new DataSet();
                        partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                        if (partner.Tables[0].Rows.Count > 0)
                        {
                            txtSifraPartnera.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                            txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtSifraPartnera.Select();
                        }
                    }
                    else
                    {
                        txtSifraPartnera.Select();
                        return;
                    }
                }

                string Str = txtSifraPartnera.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtSifraPartnera.Text = "0";
                }

                DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraPartnera.Text + "'", "partners").Tables[0];
                if (DSpar.Rows.Count > 0)
                {
                    txtPartnerNaziv.Text = DSpar.Rows[0][0].ToString();
                    txtArtikl.Select();
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
            }
        }

        private void txtIzradio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtNapomena.Select();
            }
        }

        private void txtOtprema_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtNapomena.Select();
            }
        }

        #endregion ON_KEY_DOWN_REGION

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            edit = false;
            //EnableDisable(true);
            disableHeader();
            deleteFields();
            btnSveFakture.Enabled = false;
            btnDeleteAllFaktura.Enabled = false;
            nmGodina.ReadOnly = true;
            ControlDisableEnable(0, 1, 1, 0, 1);
            txtBarkode.Select();
        }

        private void SetRoba()
        {
            dgw.Rows.Add();
            int br = dgw.Rows.Count - 1;

            dgw.Rows[br].Cells[0].Value = "1";
            dgw.Rows[br].Cells["sifra"].Value = DTRoba.Rows[0]["sifra"].ToString();
            dgw.Rows[br].Cells["naziv"].Value = DTRoba.Rows[0]["naziv"].ToString();
            dgw.Rows[br].Cells["jmj"].Value = DTRoba.Rows[0]["jm"].ToString();
            dgw.Rows[br].Cells["cijena_bez_pdva"].Value = string.Format("{0:0.000}", DTRoba.Rows[0]["vpc"]);
            dgw.Rows[br].Cells["kolicina"].Value = "1";
            dgw.Rows[br].Cells["porez"].Value = DTRoba.Rows[0]["porez"].ToString();
            dgw.Rows[br].Cells["rabat"].Value = "0,00";
            dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
            dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,000";
            dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
            dgw.Rows[br].Cells["vpc"].Value = string.Format("{0:0.000}", DTRoba.Rows[0]["vpc"]);
            dgw.Rows[br].Cells["mpc"].Value = string.Format("{0:0.00}", DTRoba.Rows[0]["mpc"]);
            dgw.Rows[br].Cells["nc"].Value = string.Format("{0:0.00}", DTRoba.Rows[0]["nc"]);
            dgw.Rows[br].Cells["oduzmi"].Value = DTRoba.Rows[0]["oduzmi"].ToString();
            dgw.Rows[br].Cells["skladiste"].Value = DTpostavke.Rows[0]["default_skladiste"].ToString();

            dgw.Select();
            dgw.CurrentCell = dgw.Rows[br].Cells[3];
            dgw.BeginEdit(true);

            //izracun();
        }

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
                        dgw.CurrentRow.Cells["porez"].Value = dsRobaProdaja.Tables[0].Rows[0]["porez"].ToString();
                        dgw.CurrentRow.Cells["vpc"].Value = string.Format("{0:0.000}", dsRobaProdaja.Tables[0].Rows[0]["vpc"]);
                    }
                    else
                    {
                        dgw.CurrentRow.Cells["porez"].Value = dsRobaProdaja.Tables[0].Rows[0]["porez"].ToString();
                        dgw.CurrentRow.Cells["vpc"].Value = string.Format("{0:0.000}", dsRobaProdaja.Tables[0].Rows[0]["vpc"]);
                    }
                }
            }
        }

        public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle,
                System.Globalization.CultureInfo.CurrentCulture, out result);
        }

        //double SveUkupno = 0;
        //private void izracun () {
        //    if (dgw.RowCount > 0) {
        //        int rowBR = dgw.CurrentRow.Index;

        //        double dec_parse;
        //        if (!Double.TryParse(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString(), out dec_parse)) {
        //            dgw.Rows[rowBR].Cells["kolicina"].Value = "1";
        //            MessageBox.Show("Greška kod upisa količine.", "Greška");
        //            return;
        //        }

        //        if (!Double.TryParse(dgw.Rows[rowBR].Cells["rabat"].FormattedValue.ToString(), out dec_parse)) {
        //            dgw.Rows[rowBR].Cells["rabat"].Value = "0";
        //            MessageBox.Show("Greška kod upisa rabata.", "Greška");
        //            return;
        //        }

        //        if (!Double.TryParse(dgw.Rows[rowBR].Cells["rabat_iznos"].FormattedValue.ToString(), out dec_parse)) {
        //            dgw.Rows[rowBR].Cells["rabat_iznos"].Value = "0,00";
        //            MessageBox.Show("Greška kod rabata.", "Greška");
        //            return;
        //        }

        //        double kol = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString()), 3);
        //        double vpc = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["vpc"].FormattedValue.ToString()), 3);
        //        double porez = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["porez"].FormattedValue.ToString()), 2);
        //        double rbt = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["rabat"].FormattedValue.ToString()), 2);

        //        double porez_ukupno = vpc * (porez) / 100;
        //        double mpc = porez_ukupno + vpc;
        //        double mpc_sa_kolicinom = mpc * kol;
        //        double rabat = mpc * rbt / 100;
        //        double iznos = 0;

        //        dgw.Rows[rowBR].Cells["mpc"].Value = Math.Round(mpc, 2).ToString("#0.00");
        //        dgw.Rows[rowBR].Cells["rabat_iznos"].Value = Math.Round(rabat * kol, 2).ToString("#0.00");
        //        dgw.Rows[rowBR].Cells["iznos_bez_pdva"].Value = Math.Round((((mpc - rabat) * kol) / (1 + porez / 100)), 3).ToString("#0.000");
        //        dgw.Rows[rowBR].Cells["iznos_ukupno"].Value = Math.Round((mpc - rabat) * kol, 2).ToString("#0.00");
        //        dgw.Rows[rowBR].Cells["cijena_bez_pdva"].Value = Math.Round(((mpc - rabat) / (1 + porez / 100)), 3).ToString("#0.000");
        //        dgw.Rows[rowBR].Cells["kolicina"].Value = kol.ToString("#0.000");
        //        dgw.Rows[rowBR].Cells["rabat"].Value = rbt.ToString("#0.00");
        //        dgw.Rows[rowBR].Cells["porez"].Value = porez.ToString("#0.00");
        //    }
        //}

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            btnSveFakture.Enabled = true;
            //EnableDisable(false);
            deleteFields();
            disableHeader();
            //txtBarkode.Text = brojRN();
            edit = false;
            btnDeleteAllFaktura.Enabled = false;
            txtBarkode.ReadOnly = false;
            nmGodina.ReadOnly = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
            txtBarkode.Select();
            disableHeader(true);
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            decimal dec_parse;
            if (Decimal.TryParse(txtSifraPartnera.Text, out dec_parse))
            {
                txtSifraPartnera.Text = dec_parse.ToString();
            }
            else
            {
                MessageBox.Show("Greška kod upisa partnera.", "Greška");
                return;
            }

            if (dgw.RowCount == 0)
            {
                MessageBox.Show("Upozorenje.\r\nOvaj servis je prazan.", "Greška");
                return;
            }

            if (txtNapomena.Text.Length > 0)
            {
            }

            if (edit)
            {
                UpdateRN();
                //EnableDisable(false);
                deleteFields();
                btnSveFakture.Enabled = true;
                disableHeader(true);
                ControlDisableEnable(1, 0, 0, 1, 0);
                //txtBarkode.Text = brojRN();
                return;
            }

            string broj = brojRN();
            //if (broj.Trim() != txtBarkode.Text.Trim()) {
            //MessageBox.Show("Vaš broj dokumenta već je netko iskoristio.\r\nDodijeljen Vam je novi broj '" + broj + "'.", "Info");
            //txtBarkode.Text = broj;
            //}

            if (txtSifraPartnera.Text == "")
            {
                MessageBox.Show("Niste upisali šifru odredišta ili sifru za koga fakturirati.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //'" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
            string sql = @"INSERT INTO servis (servisna_primka,godina,partner,izradio,seriski_broj,sifra,naziv)
VALUES ( '" + txtBarkode.Text + @"',
'" + nmGodina.Value.ToString() + @"',
'" + txtSifraPartnera.Text + @"',
'" + Properties.Settings.Default.id_zaposlenik + @"',
'" + txtSerijskiBroj.Text + @"',
'" + (txtArtikl.Tag == null ? null : txtArtikl.Tag.ToString()) + @"',
'" + txtArtikl.Text + @"'
) RETURNING id;";

            DataSet dsId = classSQL.select(sql, "servis");
            if (dsId != null && dsId.Tables.Count > 0 && dsId.Tables[0] != null && dsId.Tables[0].Rows.Count > 0)
            {
                int id = Convert.ToInt32((dsId as DataSet).Tables[0].Rows[0]["id"].ToString());
                string sql_stavke = "";
                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    sql_stavke = @"INSERT INTO servis_status (id_servis, datum, status, napomena)
VALUES
(
'" + id + @"',
'" + dg(i, "datum") + @"',
'" + lStatusi.Where(a => a.Naziv == dg(i, "status")).FirstOrDefault().ID + @"',
'" + dg(i, "napomena") + @"')";
                    provjera_sql(classSQL.insert(sql_stavke));
                }

                classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Izrada novog RN servisa br." + txtBarkode.Text + "')");
                //Util.AktivnostZaposlenika.SpremiAktivnost(dgw, null, "Radni nalog servis", txtBarkode.Text, false);

                if (MessageBox.Show("Spremljeno.\r\nŽelite li ispisati dokument?", "Spremljeno.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    printaj(txtBarkode.Text);
                }

                edit = false;
                //EnableDisable(false);
                deleteFields();
                disableHeader(true);
                btnSveFakture.Enabled = true;
                ControlDisableEnable(1, 0, 0, 1, 0);
            }
        }

        private void printaj(string broj)
        {
            Report.Servis.repServis pr = new Report.Servis.repServis();
            pr.broj_dokumenta = txtBarkode.Text;
            pr.godina = (int)nmGodina.Value;
            pr.ImeForme = "Servisna primka";
            pr.ShowDialog();
        }

        //DataSet DSprovjeraRobaProdaja = new DataSet();
        //private void ProvjeriDaliPostojiRobaProdaja (string sif, object skl, int r) {
        //    DSprovjeraRobaProdaja = classSQL.select("SELECT * FROM roba_prodaja WHERE sifra='" + sif + "' AND id_skladiste='" + skl.ToString() + "'", "roba_prodaja");

        //    if (DSprovjeraRobaProdaja.Tables[0].Rows.Count == 0) {
        //        string sql = "INSERT INTO roba_prodaja (id_skladiste,kolicina,nc,vpc,porez,sifra) VALUES" +
        //            "('" + skl.ToString() + "','0','" + dg(r, "nc").Replace(",", ".") + "','" + dg(r, "vpc").Replace(",", ".") + "','" + dg(r, "porez") + "','" + sif + "')";
        //        classSQL.insert(sql);
        //    }
        //}

        private string dg(int row, string cell)
        {
            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        private void UpdateRN()
        {
            DataSet dsID = classSQL.select("select id from servis where servisna_primka = '" + servisBarkode + "' and godina = '" + servisGodina + "'", "servis");
            int id = 0;
            if (dsID != null && dsID.Tables.Count > 0 && dsID.Tables[0] != null && dsID.Tables[0].Rows.Count > 0 && Int32.TryParse(dsID.Tables[0].Rows[0][0].ToString(), out id) && id > 0)
            {
                string sql = "";

                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    int id_stavka = 0;
                    if (Int32.TryParse(dgw.Rows[i].Tag.ToString(), out id_stavka))
                    {
                        if (id_stavka > 0)
                        {
                            sql = "UPDATE servis_status SET " +
                            " datum='" + dgw.Rows[i].Cells["datum"].Value.ToString() + "'," +
                            " status ='" + lStatusi.Where(a => a.Naziv == dg(i, "status")).FirstOrDefault().ID + "'," +
                            " napomena='" + dg(i, "napomena") + "'" +
                            " WHERE id_servis_status = '" + id_stavka + "'";
                            provjera_sql(classSQL.update(sql));
                        }
                        else
                        {
                            sql = @"INSERT INTO servis_status (id_servis, datum, status, napomena)
VALUES
(
'" + id + @"',
'" + dg(i, "datum") + @"',
'" + lStatusi.Where(a => a.Naziv == dg(i, "status")).FirstOrDefault().ID + @"',
'" + dg(i, "napomena") + @"')";
                            provjera_sql(classSQL.insert(sql));
                        }
                    }
                }

                MessageBox.Show("Spremljeno.");
                //Util.AktivnostZaposlenika.SpremiAktivnost(dgw, null, "Servis", txtBarkode.Text, true);
                edit = false;
                //EnableDisable(false);
                deleteFields();
                disableHeader();
                btnSpremi.Enabled = false;
                btnSveFakture.Enabled = true;

                classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik, datum, radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Editiranje Servisa br." + txtBarkode.Text + "')");
            }
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void btnPartner_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,case when vrsta_korisnika = 1 then ime_tvrtke else concat(ime, ' ', prezime) end as imePartnera FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtSifraPartnera.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["imePartnera"].ToString();
                    dtpDatum.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            frmSviRadniNaloziServis2016 objForm3 = new frmSviRadniNaloziServis2016();
            objForm3.sifra_rn = "";
            objForm3.MainForm = this;
            servisBarkode = null;
            servisGodina = 0;
            objForm3.ShowDialog();
            if (servisBarkode != null && servisGodina != 0)
            {
                fillRN();
                //EnableDisable(true);
                disableHeader();
                edit = true;
                nmGodina.ReadOnly = true;
            }
        }

        private void fillRN()
        {
            //EnableDisable(true);
            edit = true;
            DSrns = classSQL.select("SELECT * FROM servis WHERE servisna_primka = '" + servisBarkode + "' and godina = '" + servisGodina + "'", "servis").Tables[0];
            txtSifraPartnera.Text = DSrns.Rows[0]["partner"].ToString();
            txtPartnerNaziv.Text = classSQL.select("select case when vrsta_korisnika = 1 then ime_tvrtke else concat(ime, ' ', prezime) end as imePartnera from partners WHERE id_partner = '" + DSrns.Rows[0]["partner"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString();
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + DSrns.Rows[0]["izradio"].ToString() + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
            txtBarkode.Text = servisBarkode;
            txtArtikl.Text = DSrns.Rows[0]["naziv"].ToString();
            txtArtikl.Tag = DSrns.Rows[0]["sifra"].ToString();
            txtSerijskiBroj.Text = DSrns.Rows[0]["seriski_broj"].ToString();
            nmGodina.Value = servisGodina;

            //--------fill servis status------------------------------

            DataTable DSFSdtR = new DataTable();
            string sql = "SELECT * FROM servis_status WHERE id_servis = (select id from servis where servisna_primka = '" + servisBarkode + "' and godina = '" + servisGodina + "');";
            DSFS = classSQL.select(sql, "radni_nalog_servis").Tables[0];

            for (int i = 0; i < DSFS.Rows.Count; i++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;

                dgw.Rows[br].Cells["br"].Value = i + 1;
                dgw.Rows[br].Cells["status"].Value = lStatusi.Where(a => a.ID == Convert.ToInt32(DSFS.Rows[i]["status"].ToString())).FirstOrDefault().Naziv;
                dgw.Rows[br].Cells["datum"].Value = DSFS.Rows[i]["datum"].ToString();
                dgw.Rows[br].Cells["napomena"].Value = DSFS.Rows[i]["napomena"].ToString();
                dgw.Rows[br].Tag = Convert.ToInt32(DSFS.Rows[i]["id_servis_status"].ToString());
                dgw.Select();
                dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[3];
                ControlDisableEnable(0, 1, 1, 0, 1);
                disableHeader();
            }
        }

        //private void btnObrisi_Click (object sender, EventArgs e) {
        //    if (dgw.RowCount == 0) {
        //        return;
        //    }

        //    if (dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].Value != null) {
        //        if (MessageBox.Show("Da li ste sigurni da želite obrisati ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) {
        //            classSQL.delete("DELETE FROM radni_nalog_servis_stavke WHERE id_stavka='" + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() + "'");
        //            dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
        //            classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja)" +
        //                "VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje stavke sa RN servisa br." + txtBarkode.Text + "')");
        //            Util.AktivnostZaposlenika.SpremiAktivnost(dgw, null, "Radni nalog servis", txtBarkode.Text, true);
        //            MessageBox.Show("Obrisano.");
        //        }

        //    } else {
        //        dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
        //        MessageBox.Show("Obrisano.");
        //    }
        //}

        private void btnDeleteAllFaktura_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da želite obrisati ovaj servis?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                classSQL.delete("DELETE FROM servis_status WHERE id_servis = (select id from servis where servisna_primka = '" + txtBarkode.Text + "' and godina = '" + nmGodina.Value + "');");
                classSQL.delete("DELETE FROM servis WHERE servisna_primka = '" + txtBarkode.Text + "';");
                classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "',' Brisanje cijelog servisa br. " + txtBarkode.Text + "')");
                //Util.AktivnostZaposlenika.SpremiAktivnost(new DataGridView(), null, "Servis", txtBarkode.Text, true);
                MessageBox.Show("Obrisano.");

                edit = false;
                //EnableDisable(false);
                deleteFields();
                disableHeader();
                btnDeleteAllFaktura.Enabled = false;
                ControlDisableEnable(1, 0, 0, 1, 0);
                disableHeader(true);
            }
        }

        private void ttxBrojPonude_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT * FROM servis WHERE godina='" + nmGodina.Value.ToString() + "' AND servisna_primka = '" + txtBarkode.Text + "'", "radni_nalog_servis").Tables[0];
                if (btnNoviUnos.Enabled)
                {
                    deleteFields();
                }
                if (DT.Rows.Count == 0)
                {
                    if (txtBarkode.Text.Trim().ToString().Length > 0)
                    {
                        edit = false;
                        //EnableDisable(true);
                        disableHeader();
                        btnSveFakture.Enabled = false;
                        btnDeleteAllFaktura.Enabled = false;
                        nmGodina.ReadOnly = true;
                        nmGodina.Value = Convert.ToInt32(txtBarkode.Text.Split('-')[0]);
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    servisBarkode = DT.Rows[0]["servisna_primka"].ToString();
                    servisGodina = Convert.ToInt32(DT.Rows[0]["godina"].ToString());
                    fillRN();
                    //EnableDisable(true);
                    edit = true;
                    btnDeleteAllFaktura.Enabled = true;
                    txtSifraPartnera.Select();
                    nmGodina.ReadOnly = true;
                }
                txtSifraPartnera.Select();
                ControlDisableEnable(0, 1, 1, 0, 1);
            }
        }

        private void frmPonude_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MainForm != null)
            {
            }
        }

        private void rtbNapomena_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtNapomena.Text == "")
                {
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void cbKomercijalist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtNapomena.Select();
            }
        }

        private void btnSpremiStatus_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNapomena.Text.Length == 0)
                {
                    MessageBox.Show("Napomena nije upisana.");
                    return;
                }
                if (txtArtikl.Text.Length == 0)
                {
                    MessageBox.Show("Naziv artikla nije upisan.");
                    return;
                }
                if (txtBarkode.Text.Length == 0)
                {
                    MessageBox.Show("Barkod nije upisan.");
                    return;
                }
                if (txtSifraPartnera.Text.Length == 0)
                {
                    MessageBox.Show("Partner nije odabran.");
                    return;
                }
                //if ((int)cmbStatus.SelectedValue == 1) {
                //    var t = (from r in dgw.Rows.Cast<DataGridViewRow>()
                //                 where r.Cells["status"].Value.ToString() == lStatusi[(int)cmbStatus.SelectedValue].Naziv
                //                 select r).FirstOrDefault();
                //    if (t != null) {
                //        var p = (from r in dgw.Rows.Cast<DataGridViewRow>()
                //                 where r.Cells["status"].Value.ToString() == lStatusi[(int)cmbStatus.SelectedValue].Naziv
                //                 && r.Cells["datum"]
                //                 select r).FirstOrDefault();
                //    }

                //}

                if (rowIndex > -1)
                {
                    dgw.Rows[rowIndex].Cells["datum"].Value = dtpDatum.Value;
                    dgw.Rows[rowIndex].Cells["status"].Value = lStatusi.Where(a => a.ID == (int)cmbStatus.SelectedValue).FirstOrDefault().Naziv;
                    dgw.Rows[rowIndex].Cells["napomena"].Value = txtNapomena.Text;
                }
                else
                {
                    dgw.Rows.Add(dgw.Rows.Count + 1, dtpDatum.Value, lStatusi.Where(a => a.ID == (int)cmbStatus.SelectedValue).FirstOrDefault().Naziv, txtNapomena.Text);
                    dgw.Rows[dgw.Rows.Count - 1].Tag = 0;
                }
                setNewStatus();
                disableHeader();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void setNewStatus()
        {
            dtpDatum.Value = DateTime.Now;
            cmbStatus.SelectedIndex = 0;
            txtNapomena.Text = "";
            rowIndex = -1;
        }

        protected void disableHeader(bool b = false)
        {
            try
            {
                bool x = true;
                if (dgw.Rows.Count > 0)
                {
                    x = false;
                }
                if (b)
                    x = !b;
                txtBarkode.Enabled = x;
                nmGodina.Enabled = x;
                txtSifraPartnera.Enabled = x;
                btnPartner.Enabled = x;
                txtPartnerNaziv.Enabled = x;
                txtIzradio.Enabled = x;
                txtArtikl.Enabled = x;
                btnSrchArtikl.Enabled = x;
                txtSerijskiBroj.Enabled = x;
                //lblServisnaPrimka.Enabled = x;
                //lblPartner.Enabled = x;
                //lblIzradio.Enabled = x;
                //lblNazivUredaja.Enabled = x;
                //lblSerijskiBroj.Enabled = x;

                //gbHeader.Enabled = x;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private int rowIndex = -1;

        private void btnUrediStatus_Click(object sender, EventArgs e)
        {
            try
            {
                rowIndex = -1;
                if (dgw.Rows.Count > 0)
                {
                    rowIndex = dgw.CurrentCell.RowIndex;
                    if (rowIndex >= 0)
                    {
                        DataGridViewRow dRow = dgw.Rows[rowIndex];
                        if (dRow != null)
                        {
                            dtpDatum.Value = Convert.ToDateTime(dRow.Cells["datum"].Value);
                            cmbStatus.SelectedValue = lStatusi.Where(a => a.Naziv == dRow.Cells["status"].Value.ToString()).FirstOrDefault().ID;
                            txtNapomena.Text = dRow.Cells["napomena"].Value.ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnNoviStatus_Click(object sender, EventArgs e)
        {
            try
            {
                setNewStatus();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnSrchArtikl_Click(object sender, EventArgs e)
        {
            try
            {
                frmRobaTrazi roba_trazi = new frmRobaTrazi();
                roba_trazi.ShowDialog();
                if (Properties.Settings.Default.id_roba != "")
                {
                    DataSet ds = classSQL.select("select naziv from roba where sifra = '" + Properties.Settings.Default.id_roba + "';", "roba");
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        txtArtikl.Text = ds.Tables[0].Rows[0]["naziv"].ToString();
                        txtArtikl.Tag = Properties.Settings.Default.id_roba;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool otvaranjezaEditiranje = false;

        private void txtArtikl_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (edit && !otvaranjezaEditiranje)
                {
                    otvaranjezaEditiranje = true;
                    return;
                }
                txtArtikl.Tag = null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}