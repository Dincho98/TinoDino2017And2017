using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmNovaKalkulacija2 : Form
    {
        private DataSet DSskladiste = new DataSet();
        private DataSet DSroba = new DataSet();
        private DataSet DSPartneri = new DataSet();
        private DataSet DSzaposlenik = new DataSet();
        private DataSet DSporezi = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataTable DTstavke = new DataTable();
        private DataTable TBroba = new DataTable();
        private bool update = false;
        private double nabavna_ukupno = 0;
        public string broj_kalkulacije_edit { get; set; }
        public string edit_skladiste { get; set; }
        public frmMenu MainForm { get; set; }
        private bool load = false;
        private string TXT_MPC = "";
        private decimal GLmarza = 1;
        private string[] oibs;
        private bool dodaj_pdv = false;
        private bool izradaPogresneKalkulacije = false;
        private int kreiranZapisnikZaFakturu = 0;
        private decimal trosak = 0;

        public frmNovaKalkulacija2()
        {
            InitializeComponent();
        }

        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private void frmNovaKalkulacija_Load(object sender, EventArgs e)
        {
            chbDodajPDV.Checked = dodaj_pdv;

            /****************************SINKRONIZACIJA SA WEB-OM*****************/
            bgSinkronizacija = new BackgroundWorker();
            bgSinkronizacija.DoWork += new DoWorkEventHandler(bgSinkronizacija_DoWork);
            bgSinkronizacija.WorkerSupportsCancellation = true;
            /****************************SINKRONIZACIJA SA WEB-OM*****************/

            if (Util.Korisno.oibTvrtke == "41109922301")
            {
                chbDodajPDV.Checked = false;
                chbDodajPDV.Visible = false;
            }

            txtPPMV.Text = "0";
            if (Class.Postavke.prodaja_automobila)
            {
                txtPPMV.Visible = true;
                lblPPMV.Visible = true;
                lblCarina.Text = "Ostali troškovi";
            }

            oibs = new string[] { "67660751355", "76846500940", "30820005785", "85501330524", "86115638842", "26988876020", "03739413747", "90222495098" };
            izradaPogresneKalkulacije = oibs.Contains(Class.PodaciTvrtka.oibTvrtke);
            //Util.Korisno k = new Util.Korisno();
            //PaintRows(dataGridView1);
            this.Text = "Nova kalkulacija " + DateTime.Now.Year;
            numeric();
            tabControl1.SelectedTab = tabPage2;
            loadCB();
            nuGodinaKalk.Text = DateTime.Now.Year.ToString();
            txtBrojKalkulacije.Text = brojKalkulacije();
            SetRekapitulacija();
            EnableDisable(false);
            txtBrojKalkulacije.Select();
            ReadOnly(true);
            ControlDisableEnable(true, false, false, true, false);
            if (broj_kalkulacije_edit != null)
            {
                edit_kalkulacija(broj_kalkulacije_edit, edit_skladiste);
            }
            this.Paint += new PaintEventHandler(Form1_Paint);
            load = true;

            ProvjeriDaliPostojiSkeniraniDok();
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
            PokretacSinkronizacije.PokreniSinkronizaciju(true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
        }

        #region keydown/leave

        private void txtFakCijenaVal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (txtSifra_robe.Text != "MarzaRabljeniAuto")
                {
                    if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
                    {
                        (sender as TextBox).Text = "1,000";
                    }

                    try
                    {
                        double vrijednost = 0;
                        vrijednost = Convert.ToDouble(txtFakCijenaVal.Text);
                        if (vrijednost == 0)
                        {
                            txtFakCijenaVal.Text = "1,00";
                        }
                    }
                    catch { }
                }

                if (chbDodajPDV.Checked)
                {
                    Decimal vr = Convert.ToDecimal(txtFakCijenaVal.Text);
                    txtFakCijenaVal.Text = ((vr * Convert.ToDecimal(txtPDV.SelectedValue) / 100) + vr).ToString();
                }

                SrediFakCijenuPoValuti();

                txtFakCijena.Select();
            }
        }

        private void txtPovrNaknada_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                try
                {
                    txtPovrNaknada.Text = Math.Round(Convert.ToDouble(txtPovrNaknada.Text), 2).ToString("#0.00");
                }
                catch
                {
                    txtPovrNaknada.Text = "0,00";
                }
                changeDataGrid();
            }
        }

        private void txtNazivRobe_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            txtKolicina.Select();
        }

        private void txtSifraDobavljac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtSifraDobavljac.Text == "")
                {
                    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                    partnerTrazi.ShowDialog();
                    if (Properties.Settings.Default.id_partner != "")
                    {
                        DataSet partner = new DataSet();
                        partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                        if (partner.Tables[0].Rows.Count > 0)
                        {
                            txtSifraDobavljac.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtSifraDobavljac.Select();
                        }
                    }
                    else
                    {
                        txtSifraDobavljac.Select();
                        return;
                    }
                }

                string Str = txtSifraDobavljac.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtSifraDobavljac.Text = "0";
                }

                DataTable DT = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraDobavljac.Text + "'", "partners").Tables[0];

                if (DT.Rows.Count > 0)
                {
                    txtImeDobavljaca.Text = DT.Rows[0][0].ToString();
                    txtMjestoTroska.Select();
                }
                else
                {
                    MessageBox.Show("Traženi dobavljač ne postoji.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSifraDobavljac.Select();
                }
            }
        }

        private DataTable DT1 = new DataTable();

        private void txtImeDobavljaca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtMjestoTroska.Select();
            }
        }

        private void txtBrojKalkulacije_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                long brkalk;
                try
                {
                    brkalk = Convert.ToInt64(txtBrojKalkulacije.Text);
                }
                catch
                {
                    MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška!");
                    this.ActiveControl = txtBrojKalkulacije;
                    txtBrojKalkulacije.SelectAll();
                    return;
                }
                string sql = "SELECT broj FROM kalkulacija WHERE godina='" + nuGodinaKalk.Value.ToString() + "' AND broj='" + brkalk.ToString() + "' AND id_skladiste='" + CBskladiste.SelectedValue + "'";
                DataTable DT = classSQL.select(sql, "kalkulacija").Tables[0];
                delete_fields();
                if (DT.Rows.Count == 0)
                {
                    if (brojKalkulacije() == txtBrojKalkulacije.Text.Trim())
                    {
                        update = false;
                        EnableDisable(true);
                        btnSve.Enabled = false;
                        txtBrojKalkulacije.Text = brojKalkulacije();
                        btnDeleteAll.Enabled = false;
                        txtBrojKalkulacije.ReadOnly = true;
                        nuGodinaKalk.ReadOnly = true;
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    broj_kalkulacije_edit = txtBrojKalkulacije.Text;
                    edit_skladiste = CBskladiste.SelectedValue.ToString();
                    edit_kalkulacija(txtBrojKalkulacije.Text, CBskladiste.SelectedValue.ToString());
                    EnableDisable(true);
                    update = true;
                    btnDeleteAll.Enabled = true;
                    txtBrojKalkulacije.ReadOnly = true;
                    nuGodinaKalk.ReadOnly = true;
                }
                ControlDisableEnable(false, true, true, false, true);
                dtpDatumNow.Select();

                if (txtValutaValuta.Text != "1")
                    dataGridView1.Columns["fakturnaCijenaValuta"].Visible = true;
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            dtpDatumNow.Select();
        }

        private void dtpDatumNow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSifraDobavljac.Select();
            }
        }

        private void CBskladiste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifraDobavljac.Select();
            }
        }

        private void cbDobavljac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtMjestoTroska.Select();
            }
        }

        private void txtMjestoTroska_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtBrojRac.Select();
            }
        }

        private void txtBrojRac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                dtpRacun.Select();
                txtOtpremnica.Text = txtBrojRac.Text;

                DataTable DTpr = classSQL.select("SELECT * FROM kalkulacija WHERE id_partner='" + txtSifraDobavljac.Text + "' " +
                    " AND racun='" + txtBrojRac.Text + "'", "kalk").Tables[0];
                if (DTpr.Rows.Count > 0)
                {
                    MessageBox.Show("Program je prepoznao kalkulaciju koja ima istog " +
                        "partnera i broj računa. \r\nBroj kalkulacije je: " + DTpr.Rows[0]["broj"].ToString(), "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void dtpRacun_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtOtpremnica.Select();
                dtpOtpremnica.Value = dtpRacun.Value;
            }
        }

        private void txtOtpremnica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                dtpOtpremnica.Select();
            }
        }

        private void dtpOtpremnica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifra_robe.Select();
            }
        }

        private void decimal_set_Leave(object sender, EventArgs e)
        {
            if (txtSifra_robe.Text == "")
            {
                return;
            }

            if (txtPDV.SelectedValue == null)
            {
                MessageBox.Show("PDV nije odabran.");
                return;
            }

            if (txtSifra_robe.Text != "MarzaRabljeniAuto")
            {
                try
                {
                    double vrijednost = 0;
                    vrijednost = Convert.ToDouble(txtFakCijenaVal.Text);
                    if (vrijednost == 0)
                    {
                        txtFakCijenaVal.Text = "1,00";
                    }
                }
                catch { }
                try
                {
                    double vrijednost1 = 0;
                    vrijednost1 = Convert.ToDouble(txtFakCijena.Text);
                    if (vrijednost1 == 0)
                    {
                        txtFakCijena.Text = "1,00";
                    }
                }
                catch { }
            }

            Izracun2(sender);

            srediDecimalnaMjesta();
        }

        private void srediDecimalnaMjesta()
        {
            txtKolicina.Text = Math.Round(Convert.ToDouble(txtKolicina.Text), 3).ToString("#0.000");
            txtFakCijena.Text = Math.Round(Convert.ToDouble(txtFakCijena.Text), 4).ToString("#0.0000");
            txtVPC.Text = Math.Round(Convert.ToDouble(txtVPC.Text), 3).ToString("#0.000");
            txtMPC.Text = Math.Round(Convert.ToDouble(txtMPC.Text), 2).ToString("#0.00");
            txtPrijevoz.Text = Math.Round(Convert.ToDouble(txtPrijevoz.Text), 4).ToString("#0.0000");
            txtCarina.Text = Math.Round(Convert.ToDouble(txtCarina.Text), 2).ToString("#0.00");
            txtPosebniPorez.Text = Math.Round(Convert.ToDouble(txtPosebniPorez.Text), 2).ToString("#0.00");
            txtIznosRabat.Text = Math.Round(Convert.ToDouble(txtIznosRabat.Text), 2).ToString("#0.00");
            txtIznosFakCijena.Text = Math.Round(Convert.ToDouble(txtIznosFakCijena.Text), 2).ToString("#0.00");
            txtUkupno.Text = Math.Round(Convert.ToDouble(txtUkupno.Text), 2).ToString("#0.00");
            txtIznosMPC.Text = Math.Round(Convert.ToDouble(txtIznosMPC.Text), 2).ToString("#0.00");
            txtIznosPDV.Text = Math.Round(Convert.ToDouble(txtIznosPDV.Text), 2).ToString("#0.00");
            txtNabavnaCijenaKune.Text = Math.Round(Convert.ToDouble(txtNabavnaCijenaKune.Text), 2).ToString("#0.00");
        }

        private void decimal_set_Leave_MPC(object sender, EventArgs e)
        {
            if (txtSifra_robe.Text == "")
            {
                return;
            }

            Izracun2(sender);

            srediDecimalnaMjesta();
        }

        private void decimal_set_Leave_VPC(object sender, EventArgs e)
        {
            if (txtSifra_robe.Text == "")
            {
                return;
            }

            if (dataGridView1.RowCount == 0)
            {
                return;
            }

            Izracun2(sender);

            changeDataGrid();

            srediDecimalnaMjesta();
        }

        private void provjera_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender.ToString()[0] == '+')
            {
                if ('+' == (e.KeyChar))
                    return;
            }

            if (sender.ToString()[0] == '-')
            {
                // if ((char)('-') == (e.KeyChar)) return;
            }

            if (',' == (e.KeyChar))
            {
                e.Handled = false;
                return;
            }

            if (sender.ToString().Length == 0)
                return;

            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != 45)
            {
                e.Handled = true;
            }
        }

        private DataTable DTpodaci = classSQL.select_settings("SELECT * FROM podaci_tvrtka WHERE id='1'", "podaci_tvrtka").Tables[0];
        public string kolicina_unesi_povrat { get; set; }

        private void txtSifra_robe_KeyDown(object sender, KeyEventArgs e)
        {
            btnSve.Enabled = false;
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                // Ovaj tu IF sluzi da 'Sutilis' i 'LL obrt za cviječarstvo' moze raditi krive kalkulacije :)

                //if ((DTpodaci.Rows[0]["oib"].ToString() != "67660751355" && DTpodaci.Rows[0]["oib"].ToString() != "76846500940"))
                if (!oibs.Contains(Util.Korisno.oibTvrtke))
                    for (int y = 0; y < dataGridView1.Rows.Count; y++)
                    {
                        if (txtSifra_robe.Text.Trim() == dataGridView1.Rows[y].Cells["sifra"].FormattedValue.ToString().Trim())
                        {
                            if (MessageBox.Show("Ova šifra već postoji u ovoj kalkulaciji!\r\nŽelite li dodati količinu na već unešenu šifru?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                frmUnosiPovrat up = new frmUnosiPovrat();
                                up.Text = "Unos količine";
                                //up.kalku = this;
                                kolicina_unesi_povrat = null;
                                up.povrat_na = "KALK";
                                up.txtNatpis.Text = "Unesite količinu za sifru: " + txtSifra_robe.Text.Trim();
                                up.ShowDialog();

                                if (kolicina_unesi_povrat != "0" || kolicina_unesi_povrat != null)
                                {
                                    dataGridView1.Rows[y].Cells["kolicina"].Value = Convert.ToDecimal(dataGridView1.Rows[y].Cells["kolicina"].Value) + Convert.ToDecimal(kolicina_unesi_povrat);
                                    dataGridView1.Rows[y].Selected = true;
                                    dataGridView1.CurrentCell = dataGridView1.Rows[y].Cells[0];
                                    change();
                                    EnableDisable(true);
                                    txtFakCijenaVal.Select();
                                    return;
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                    }

                string sifra= CheckEan(txtSifra_robe.Text) ?? txtSifra_robe.Text;
                //TBroba = classSQL.select("SELECT * FROM roba WHERE (sifra='" + txtSifra_robe.Text + @"' AND oduzmi='DA'", "roba").Tables[0];
                TBroba = classSQL.select($"SELECT * FROM roba WHERE (sifra='{txtSifra_robe.Text}' OR sifra='{sifra}') AND oduzmi='DA'","roba").Tables[0];

                if (TBroba.Rows.Count > 0)
                {
                    ReadOnly(false);
                    EnableDisable(true);
                    txtSifra_robe.Text = TBroba.Rows[0]["sifra"].ToString().Trim();
                    txtNazivRobe.Text = TBroba.Rows[0]["naziv"].ToString();

                    setPovratnaNaknada(TBroba.Rows[0]["sifra"].ToString().Trim());

                    txtPPMV.Text = setPPMV(TBroba.Rows[0]["sifra"].ToString().Trim()).ToString();

                    setRoba(TBroba.Rows[0]["porez"].ToString());

                    if (dataGridView1.RowCount == 0)
                    {
                        return;
                    }

                    double pdv = Convert.ToDouble(txtPDV.SelectedValue);
                    double kol = Convert.ToDouble(txtKolicina.Text);
                    //double marza = Convert.ToDouble(txtMarza.Text);

                    txtIznosFakCijena.Text = (kol * Convert.ToDouble(txtFakCijena.Text)).ToString();

                    txtIznosRabat.Text = Math.Round(Convert.ToDouble(txtFakCijena.Text) * Convert.ToDouble(txtRabat.Text) / 100, 2).ToString("#0.00");

                    nabavna_ukupno = vratiNabavnu();

                    double VPC = Convert.ToDouble(txtVPC.Text);

                    txtNabavnaCijenaKune.Text = Math.Round(nabavna_ukupno * kol, 2).ToString("#0.00");
                    //VPC = ((VPC * marza / 100) + VPC);
                    //VPC = Math.Round(VPC, 3);
                    //txtVPC.Text = Math.Round(VPC, 3).ToString("#0.000");

                    //double MPC = Convert.ToDouble(txtMPC.Text);
                    //MPC = Math.Round((VPC * pdv / 100) + VPC, 2);
                    //txtIznosVPC.Text = Math.Round(VPC * kol, 3).ToString("#0.000");
                    //txtUkupno.Text = Math.Round(VPC - nabavna_ukupno, 2).ToString("#0.00");
                    //txtIznosPDV.Text = Math.Round(VPC * (pdv / 100) * kol, 2).ToString("#0.00");
                    //txtIznosMPC.Text = Math.Round(MPC * kol, 2).ToString("#0.00");
                    if (nabavna_ukupno == 0)
                    {
                        txtMarza.Text = "0,00";
                    }
                    else
                    {
                        txtMarza.Text = ((VPC / nabavna_ukupno - 1) * 100).ToString("#0.00000000");
                    }

                    //txtMPC.Text = Math.Round(MPC, 2).ToString("#0.00");
                    TXT_MPC = txtMPC.Text;

                    changeDataGrid();

                    txtKolicina.Select();
                }
                else
                {
                    //if (MessageBox.Show("Za ovu šifru ne postoji artikl ili na artiklu nije aktivirano oduzimanje sa skladišta.\r\nŽelite li dodati novu šifru?", "Greška", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes) {
                    //frmRobaUsluge robaUsluge = new frmRobaUsluge();
                    //robaUsluge.Show();
                    frmRobaTrazi roba_trazi = new frmRobaTrazi();
                    roba_trazi.ShowDialog();
                    if (Properties.Settings.Default.id_roba != "")
                    {
                        ReadOnly(false);
                        EnableDisable(true);
                        Fill_Roba(Properties.Settings.Default.id_roba);
                    }
                    //}
                }
            }
        }

        private string CheckEan(string code)
        {
            string result = null;
            DataTable DTroba = Global.Database.GetRoba();
            if (DTroba?.Rows.Count > 0)
            {
                foreach (DataRow row in DTroba.Rows)
                {
                    string ean = row["ean"].ToString();
                    if (ean != "-1")
                    {
                        string[] array = ean.Split(';');
                        int index = Array.IndexOf(array, code);
                        if (index > -1)
                        {
                            result = row["sifra"].ToString();
                            break;
                        }
                    }
                }
            }
            return result;
        }

        private void txtKolicina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
                {
                    (sender as TextBox).Text = "1,000";
                }

                try
                {
                    double vrijednost = 0;
                    vrijednost = Convert.ToDouble(txtKolicina.Text);
                    if (vrijednost == 0)
                    {
                        txtKolicina.Text = "1,00";
                    }
                }
                catch { }

                txtFakCijenaVal.Select();
            }
        }

        private void txtFakCijena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (txtSifra_robe.Text != "MarzaRabljeniAuto")
                {
                    if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
                    {
                        (sender as TextBox).Text = "1,00";
                    }

                    try
                    {
                        double vrijednost = 0;
                        vrijednost = Convert.ToDouble(txtFakCijena.Text);
                        if (vrijednost == 0)
                        {
                            txtFakCijena.Text = "1,00";
                        }
                    }
                    catch { }
                }
                txtRabat.Select();
            }
        }

        private void txtRabat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
                {
                    (sender as TextBox).Text = "0,00";
                }

                txtPrijevoz.Select();
            }
        }

        private void txtPrijevoz_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
                {
                    (sender as TextBox).Text = "0,00";
                }

                txtCarina.Select();
            }
        }

        private void txtCarina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
                {
                    (sender as TextBox).Text = "0,00";
                }

                txtPosebniPorez.Select();
            }
        }

        private void txtPosebniPorez_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
                {
                    (sender as TextBox).Text = "0,00";
                }

                if (Class.Postavke.prodaja_automobila)
                {
                    txtPPMV.Select();
                }
                else
                {
                    txtMarza.Select();
                }
            }
        }

        private void txtMarza_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
                {
                    (sender as TextBox).Text = "0,00";
                }

                txtVPC.Text = ((nabavna_ukupno * Convert.ToDouble(txtMarza.Text) / 100) + nabavna_ukupno).ToString("#0.00");
                txtMPC.Text = ((Convert.ToDouble(txtVPC.Text) * Convert.ToDouble(txtPDV.SelectedValue) / 100) +
                    Convert.ToDouble(txtVPC.Text)).ToString("#0.00");
                txtVPC.Select();
            }
        }

        private void txtVPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
                {
                    (sender as TextBox).Text = "0,00";
                }
                izracunVPC();
                txtPDV.Select();
            }
        }

        private void txtPDV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Izracun2(sender);
                txtMPC.Select();
            }
        }

        private bool mpcPaste = false;

        private void txtMPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || mpcPaste)
            {
                e.SuppressKeyPress = true;
                if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
                {
                    (sender as TextBox).Text = "0,00";
                }
                decimal dMpc = 0;
                decimal.TryParse((sender as TextBox).Text, out dMpc);
                txtMPC.Text = dMpc.ToString();
                if (TXT_MPC != txtMPC.Text && TXT_MPC != "")
                    izracunMPC();
                txtSifra_robe.Select();
                dataGridView1.ClearSelection();
                novi();

                if (txtValutaValuta.Text != "1")
                    dataGridView1.Columns["fakturnaCijenaValuta"].Visible = true;

                //mpcPaste = false;
            }
            //else if ((e.Control && e.KeyCode == Keys.V && !mpcPaste))
            //{
            //    mpcPaste = true;
            //    txtMPC_KeyDown(sender, e);
            //    mpcPaste = false;
            //}
        }

        private void cbValuta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                tabControl1.SelectedTab = tabPage2;
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

        #endregion keydown/leave

        #region paint

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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void Form1_Paint1(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        #endregion paint

        #region util

        private void SrediFakCijenuPoValuti()
        {
            double val = Convert.ToDouble(txtFakCijenaVal.Text) * Convert.ToDouble(txtValutaValuta.Text);

            txtFakCijena.Text = Math.Round(val, 2).ToString("#0.00");
        }

        private void SrediValutuPoFakCijeni()
        {
            double val = Convert.ToDouble(txtFakCijena.Text) / Convert.ToDouble(txtValutaValuta.Text);

            txtFakCijenaVal.Text = Math.Round(val, 2).ToString("#0.00");
        }

        private void Fill_Roba(string id_roba)
        {
            TBroba = classSQL.select("SELECT * FROM roba WHERE sifra='" + id_roba + "' AND oduzmi ='DA'", "roba").Tables[0];
            if (TBroba.Rows.Count == 0)
            {
                MessageBox.Show("Odabrani artikl ili usluga se ne oduzima iz skladište te nije potrebno raditi kalkulaciju.", "Greška");
                return;
            }

            setPovratnaNaknada(TBroba.Rows[0]["sifra"].ToString().Trim());

            txtSifra_robe.Text = TBroba.Rows[0]["sifra"].ToString();
            txtNazivRobe.Text = TBroba.Rows[0]["naziv"].ToString();
            //txtFakCijenaVal.Text = TBroba.Rows[0]["nc"].ToString();
            //txtFakCijena.Text = TBroba.Rows[0]["nc"].ToString();
            setRoba("xxx");

            txtKolicina.Select();
            PaintRows(dataGridView1);
        }

        private void setRoba(string pdv)
        {
            //Za sutilisa i

            //if ((DTpodaci.Rows[0]["oib"].ToString() != "67660751355" && DTpodaci.Rows[0]["oib"].ToString() != "76846500940"))
            if (!oibs.Contains(Util.Korisno.oibTvrtke))
            {
                for (int y = 0; y < dataGridView1.Rows.Count; y++)
                {
                    if (txtSifra_robe.Text.Trim() == dataGridView1.Rows[y].Cells["sifra"].FormattedValue.ToString().Trim())
                    {
                        if (MessageBox.Show("Ova šifra već postoji u ovoj kalkulaciji!\r\nŽelite li dodati količinu na već unešenu šifru?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            frmUnosiPovrat up = new frmUnosiPovrat();
                            up.Text = "Unos količine";
                            //up.kalku = this;
                            kolicina_unesi_povrat = null;
                            up.povrat_na = "KALK";
                            up.txtNatpis.Text = "Unesite količinu za sifru: " + txtSifra_robe.Text.Trim();
                            up.ShowDialog();

                            if (kolicina_unesi_povrat != "0" || kolicina_unesi_povrat != null)
                            {
                                dataGridView1.Rows[y].Cells["kolicina"].Value = Convert.ToDecimal(dataGridView1.Rows[y].Cells["kolicina"].Value) + Convert.ToDecimal(kolicina_unesi_povrat);
                                dataGridView1.Rows[y].Selected = true;
                                dataGridView1.CurrentCell = dataGridView1.Rows[y].Cells[0];
                                change();
                                EnableDisable(true);
                                txtFakCijenaVal.Select();
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }

            if (TBroba.Rows[0]["nc"].ToString() != "0")
            {
                txtFakCijenaVal.Text = TBroba.Rows[0]["nc"].ToString();
            }
            else
            {
                txtFakCijenaVal.Text = "1,00";
            }
            if (TBroba.Rows[0]["nc"].ToString() != "0")
            {
                txtFakCijena.Text = TBroba.Rows[0]["nc"].ToString();
            }
            else
            {
                txtFakCijena.Text = "1,00";
            }

            txtVPC.Text = TBroba.Rows[0]["vpc"].ToString();
            txtMPC.Text = TBroba.Rows[0]["mpc"].ToString();
            txtKolicina.Text = "1";
            int br = dataGridView1.Rows.Count + 1;
            if (pdv != "xxx")
            {
                dataGridView1.Rows.Add(br, txtSifra_robe.Text, txtNazivRobe.Text, txtKolicina.Text, txtFakCijena.Text, "",
                    txtMarza.Text, txtVPC.Text, txtMPC.Text, txtRabat.Text, txtPrijevoz.Text, txtCarina.Text,
                    txtPosebniPorez.Text, pdv, "", txtPovrNaknada.Text, "", txtPPMV.Text);

                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected = true;

                txtPDV.SelectedValue = pdv;
            }
            else
            {
                dataGridView1.Rows.Add(br, txtSifra_robe.Text, txtNazivRobe.Text, txtKolicina.Text, txtFakCijena.Text, "",
                    txtMarza.Text, txtVPC.Text, txtMPC.Text, txtRabat.Text, txtPrijevoz.Text, txtCarina.Text,
                    txtPosebniPorez.Text, txtPDV.SelectedValue, "", txtPovrNaknada.Text, "", txtPPMV.Text);
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected = true;
            }

            //PaintRows(dataGridView1);
        }

        private decimal setPovratnaNaknada(string sifra)
        {
            DataTable DTPovrNaknd = classSQL.select("SELECT iznos FROM povratna_naknada WHERE sifra='" +
                sifra + "'", "povratna_naknada").Tables[0];

            txtPovrNaknada.Text = DTPovrNaknd.Rows.Count > 0 ? DTPovrNaknd.Rows[0][0].ToString() : "0,00";
            decimal zaVratiti;
            decimal.TryParse(txtPovrNaknada.Text, out zaVratiti);
            return zaVratiti;
        }

        private decimal setPPMV(string sifra)
        {
            decimal ppmv = 0;
            string sql = string.Format(@"select ppmv from roba_prodaja where sifra = '{0}' and id_skladiste = {1};", sifra, CBskladiste.SelectedValue);
            DataSet dsPPMV = classSQL.select(sql, "roba");
            if (dsPPMV != null && dsPPMV.Tables.Count > 0 && dsPPMV.Tables[0] != null && dsPPMV.Tables[0].Rows.Count > 0 && decimal.TryParse(dsPPMV.Tables[0].Rows[0]["ppmv"].ToString(), out ppmv))
                return ppmv;
            else
                return ppmv;
        }

        private void ControlDisableEnable(bool novi, bool odustani, bool spremi, bool sve, bool delAll)
        {
            btnNoviUnos.Enabled = novi;
            btnOdustani.Enabled = odustani;
            btnSpremi.Enabled = spremi;
            btnSve.Enabled = sve;
            btnDeleteAll.Enabled = delAll;
        }

        private void novi()
        {
            //txtSifra_robe.Select();
            txtSifra_robe.Text = "";
            txtNazivRobe.Text = "";
            txtKolicina.Text = "0,00";
            txtFakCijena.Text = "0,00";
            txtMarza.Text = "0,00";
            txtVPC.Text = "0,0000";
            txtMPC.Text = "0,00";
            txtRabat.Text = "0,00";
            txtPrijevoz.Text = "0,00";
            txtCarina.Text = "0,00";
            txtPosebniPorez.Text = "0,00";
            txtPPMV.Text = "0,00";

            txtIznosRabat.Text = "0,00";
            txtIznosFakCijena.Text = "0,00";
            txtValutaFakIznos.Text = "0,00";
            txtUkupno.Text = "0,00";
            txtIznosMPC.Text = "0,00";
            txtPovrNaknada.Text = "0,00";
        }

        private void izracun()
        {
            if (dataGridView1.RowCount == 0)
            {
                return;
            }

            SrediFakCijenuPoValuti();

            double pdv = Convert.ToDouble(txtPDV.SelectedValue);
            double kol = Convert.ToDouble(txtKolicina.Text);
            double marza = Convert.ToDouble(txtMarza.Text);

            txtIznosFakCijena.Text = (kol * Convert.ToDouble(txtFakCijena.Text)).ToString();

            txtIznosRabat.Text = Math.Round(Convert.ToDouble(txtFakCijena.Text) * Convert.ToDouble(txtRabat.Text) / 100, 2).ToString("#0.00");

            nabavna_ukupno = vratiNabavnu();

            //nabavna_ukupno = Math.Round(nabavna_ukupno, 2);

            double VPC;
            if (nabavna_ukupno == 0)
            {
                VPC = Convert.ToDouble(txtVPC.Text);
            }
            else
            {
                VPC = nabavna_ukupno;
            }

            txtNabavnaCijenaKune.Text = Math.Round(nabavna_ukupno * kol, 2).ToString("#0.00");
            VPC = ((VPC * marza / 100) + VPC);
            VPC = Math.Round(VPC, 3);
            txtVPC.Text = Math.Round(VPC, 3).ToString("#0.000");
            //txtVPC.Text = VPC.ToString();

            double MPC = nabavna_ukupno;
            MPC = Math.Round((VPC * pdv / 100) + VPC, 2);
            //txtMPC.Text = MPC.ToString();
            txtIznosVPC.Text = Math.Round(VPC * kol, 3).ToString("#0.000");
            //double MPC = Convert.ToDouble(txtMPC.Text)
            //VPC = Convert.ToDouble(txtVPC.Text);
            txtUkupno.Text = Math.Round(VPC - nabavna_ukupno, 2).ToString("#0.00");
            txtIznosPDV.Text = Math.Round(VPC * (pdv / 100) * kol, 2).ToString("#0.00");
            txtIznosMPC.Text = Math.Round(MPC * kol, 2).ToString("#0.00");

            //
            if (nabavna_ukupno == 0)
            {
                txtMarza.Text = "0,0000";
            }
            else
            {
                txtMarza.Text = ((VPC / nabavna_ukupno - 1) * 100).ToString("#0.00000000");
            }
            //txtMPC.Text = String.Format("{0:0.00}", Convert.ToString((Convert.ToDouble(txtVPC.Text) * (Convert.ToDouble(txtPDV.SelectedValue)) / 100) + Convert.ToDouble(txtVPC.Text)));

            //--------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------
            //Math.Round() je točniji od String.Format()
            //on 8.504999999 zaokružuje na 8.505 (tj. na 8.50), dok String.Format() isti broj zaokružuje na 8.51!!!
            txtMPC.Text = Math.Round(MPC, 2).ToString("#0.00");
            TXT_MPC = txtMPC.Text;
            //txtMPC.Text = String.Format("{0:0.00}", MPC).ToString("#0.00");

            string test1 = string.Format("{0:0.00}", MPC);
            string test2 = MPC.ToString("#0.00");
            //--------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------

            changeDataGrid();
        }

        private double vratiNabavnu()
        {
            return Convert.ToDouble(txtFakCijena.Text) -
                Math.Round(Convert.ToDouble(txtIznosRabat.Text), 2) +
                (Convert.ToDouble(txtPrijevoz.Text)) +
                (Convert.ToDouble(txtCarina.Text)) +
                (Convert.ToDouble(txtPosebniPorez.Text));
        }

        private void izracunVPC()
        {
            double vpc = Convert.ToDouble(txtVPC.Text);
            double pdv = Convert.ToDouble(txtPDV.SelectedValue);

            nabavna_ukupno = vratiNabavnu();

            double pov_nak = 0, ppmv = 0;
            double.TryParse(txtPovrNaknada.Text, out pov_nak);
            double.TryParse(txtPPMV.Text, out ppmv);

            if (!Class.Postavke.koristi_povratnu_naknadu)
                pov_nak = 0;

            txtMPC.Text = Math.Round((((vpc * pdv) / 100) + vpc) + pov_nak + ppmv, 2).ToString("#0.00");
            TXT_MPC = txtMPC.Text;
            txtMarza.Text = ((vpc / nabavna_ukupno - 1) * 100).ToString("#0.00000000");
            txtUkupno.Text = Math.Round(vpc - nabavna_ukupno, 2).ToString("#0.00");

            if (Convert.ToDouble(txtMarza.Text) < 0)
            {
                txtMarza.BackColor = Color.Red;
                MessageBox.Show("Upozorenje.\r\nMarža je manja od nule.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                txtMarza.BackColor = Color.White;
            }

            changeDataGrid();
        }

        private void izracunMPC()
        {
            nabavna_ukupno = vratiNabavnu();

            double pov_nak = 0, ppmv = 0;
            double.TryParse(txtPovrNaknada.Text, out pov_nak);
            double.TryParse(txtPPMV.Text, out ppmv);

            if (!Class.Postavke.koristi_povratnu_naknadu)
                pov_nak = 0;
            //nabavna_ukupno = Math.Round(nabavna_ukupno, 2);

            double pdv = Convert.ToDouble(txtPDV.SelectedValue);
            double porez_preracunata_stopa = Math.Round((100 * pdv) / (100 + pdv), 2);

            double mpc = Convert.ToDouble(txtMPC.Text);

            mpc = mpc - pov_nak - ppmv;
            double vpc = Math.Round(mpc - (mpc * porez_preracunata_stopa / 100), 3);
            txtVPC.Text = vpc.ToString("#0.000");

            txtMarza.Text = ((vpc / nabavna_ukupno - 1) * 100).ToString("#0.00000000");
            txtUkupno.Text = Math.Round(vpc - nabavna_ukupno, 2).ToString("#0.00");

            if (Convert.ToDouble(txtMarza.Text) < 0)
            {
                txtMarza.BackColor = Color.Red;
                MessageBox.Show("Upozorenje.\r\nMarža je manja od nule.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                txtMarza.BackColor = Color.White;
            }

            TXT_MPC = txtMPC.Text;

            changeDataGrid();
            //ovo mora(?) biti otkomentirano jer inače daje krive iznose
            //EnableDisable(false);
        }

        private void ReadOnly(bool b)
        {
            bool ab;
            if (b == false) { ab = true; } else { ab = false; }
            dtpDatumNow.Enabled = ab;
            //CBskladiste.Enabled = b;
            txtBrojKalkulacije.Enabled = b;
            nuGodinaKalk.Enabled = b;
            txtSifraDobavljac.Enabled = ab;

            //txtMjestoTroska.ReadOnly = b;
            //txtBrojRac.ReadOnly = b;
            //txtOtpremnica.ReadOnly = b;
            txtKolicina.ReadOnly = b;
            txtPrijevoz.ReadOnly = b;
            txtCarina.ReadOnly = b;
            txtMarza.ReadOnly = b;
            txtPPMV.ReadOnly = b;
            txtRabat.ReadOnly = b;
            txtVPC.ReadOnly = b;
            txtMPC.ReadOnly = b;
            txtFakCijena.ReadOnly = b;
            txtPosebniPorez.ReadOnly = b;
            txtPovrNaknada.ReadOnly = b;
        }

        private void delete_fields()
        {
            //txtSifra_robe.Select();
            txtSifra_robe.Text = "";
            txtNazivRobe.Text = "";
            txtKolicina.Text = "0,00";
            txtFakCijena.Text = "0,00";
            txtMarza.Text = "0,00";
            txtVPC.Text = "0,0000";
            txtMPC.Text = "0,00";
            txtRabat.Text = "0,00";
            txtPrijevoz.Text = "0,00";
            txtCarina.Text = "0,00";
            txtPosebniPorez.Text = "0,00";
            txtIznosRabat.Text = "0,00";
            txtIznosFakCijena.Text = "0,00";
            txtUkupno.Text = "0,00";
            txtIznosMPC.Text = "0,00";
            txtNabavnaCijenaKune.Text = "0.00";
            txtPovrNaknada.Text = "0.00";
            txtMjestoTroska.Text = "";
            txtBrojRac.Text = "";
            txtOtpremnica.Text = "";

            dataGridView1.Rows.Clear();
        }

        private void EnableDisable(bool b)
        {
            txtKolicina.Enabled = b;
            txtPrijevoz.Enabled = b;
            txtCarina.Enabled = b;
            txtMarza.Enabled = b;
            txtRabat.Enabled = b;
            txtPDV.Enabled = b;
            txtVPC.Enabled = b;
            txtMPC.Enabled = b;
            txtFakCijena.Enabled = b;
            txtFakCijenaVal.Enabled = b;
            txtPosebniPorez.Enabled = b;
            if (Class.Postavke.prodaja_automobila)
            {
                txtPPMV.Enabled = b;
            }

            dtpDatumNow.Enabled = b;
            CBskladiste.Enabled = b;
            txtSifraDobavljac.Enabled = b;
            txtMjestoTroska.Enabled = b;
            txtBrojRac.Enabled = b;
            txtOtpremnica.Enabled = b;
            dtpRacun.Enabled = b;
            dtpOtpremnica.Enabled = b;

            if (!b)
            {
                txtBrojKalkulacije.Enabled = true;
                nuGodinaKalk.Enabled = true;
                CBskladiste.Enabled = true;
            }
            else
            {
                txtBrojKalkulacije.Enabled = false;
                nuGodinaKalk.Enabled = false;
                //CBskladiste.Enabled = false;
            }
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void numeric()
        {
            nuGodinaKalk.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nuGodinaKalk.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nuGodinaKalk.Value = Convert.ToInt16(DateTime.Now.Year.ToString());
            ;
        }

        private string brojKalkulacije()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj) FROM kalkulacija WHERE id_skladiste='" + CBskladiste.SelectedValue + "'", "kalkulacija").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void SetRekapitulacija()
        {
            dataGridView2.Rows.Add("NABAVNI IZNOS U VALUTI", "");
            dataGridView2.Rows.Add("NABAVNI IZNOS U KUNAMA", "");
            dataGridView2.Rows.Add("IZNOS PRIJEVOZA", "");
            dataGridView2.Rows.Add("IZNOS CARINE", "");
            dataGridView2.Rows.Add("IZNOS TROŠKOVA UKUPNO", "");
            dataGridView2.Rows.Add("IZNOS POSEBNOG POREZA", "");
            dataGridView2.Rows.Add("VELEPRODAJNI IZNOS", "");
            dataGridView2.Rows.Add("MALOPRODAJNI IZNOS", "");
        }

        private void loadCB()
        {
            //CB zaposlenik
            //Properties.Settings.Default.id_zaposlenik = "1";
            DSzaposlenik = classSQL.select("SELECT ime + ' ' + prezime AS ime_prezime,id_zaposlenik FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici");
            txtZaposlenik.Text = DSzaposlenik.Tables[0].Rows[0][0].ToString();

            //DS skladiste
            DSskladiste = classSQL.select("SELECT * FROM skladiste ORDER BY skladiste ", "skladiste");
            CBskladiste.DataSource = DSskladiste.Tables[0];
            CBskladiste.DisplayMember = "skladiste";
            CBskladiste.ValueMember = "id_skladiste";

            ////DS partneri
            //DSPartneri = classSQL.select("SELECT * FROM partners ORDER BY ime_tvrtke", "partners");
            //cbDobavljac.DataSource = DSPartneri.Tables[0];
            //cbDobavljac.DisplayMember = "ime_tvrtke";
            //cbDobavljac.ValueMember = "id_partner";

            //DS porez
            DSporezi = classSQL.select("select naziv, replace(iznos, ',','.')::numeric(7,2) as iznos from porezi ORDER BY id_porez ASC", "porezi");
            txtPDV.DataSource = DSporezi.Tables[0];
            txtPDV.DisplayMember = "naziv";
            txtPDV.ValueMember = "iznos";

            //DS Valuta
            DSValuta = classSQL.select("SELECT * FROM valute", "valute");
            cbValuta.DataSource = DSValuta.Tables[0];
            cbValuta.DisplayMember = "ime_valute";
            cbValuta.ValueMember = "id_valuta";
            cbValuta.SelectedValue = 5;
            //txtValutaValuta.DataBindings.Add("Text", DSValuta.Tables[0], "tecaj");
            txtValutaValuta.Text = "1";
        }

        #endregion util

        #region buttons

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            ReadOnly(false);
            dtpDatumNow.Select();
            txtMjestoTroska.Enabled = true;
            txtBrojRac.Enabled = true;
            txtOtpremnica.Enabled = true;
            ControlDisableEnable(false, true, true, false, true);
            dtpRacun.Enabled = true;
            dtpOtpremnica.Enabled = true;
            update = false;
            trosak = 0;
        }

        private void provjeri_polja()
        {
            double dec_parse;
            if (!Double.TryParse(txtKolicina.Text, out dec_parse))
            {
                txtKolicina.Text = "0";
                return;
            }

            if (!Double.TryParse(txtFakCijena.Text, out dec_parse))
            {
                txtFakCijena.Text = "0";
                return;
            }
            if (!Double.TryParse(txtFakCijenaVal.Text, out dec_parse))
            {
                txtFakCijenaVal.Text = "0";
                return;
            }
            if (double.IsNaN(Convert.ToDouble(txtMarza.Text)))
            {
                txtMarza.Text = "0";
            }
            if (double.IsInfinity(Convert.ToDouble(txtMarza.Text)))
            {
                txtMarza.Text = "0";
            }

            if (txtMarza.Text == "Infinity")
            {
                txtMarza.Text = "0";
            }
            if (txtVPC.Text.ToLower() == "nan")
            {
                txtVPC.Text = "0";
            }
        }

        private void PregledKolicine()
        {
            //OVO RADI SPREMLJENA PROCEDURA
            try
            {
                if (Class.Postavke.skidajSkladisteProgramski)
                {
                    string _sql = "";

                    foreach (DataGridViewRow r in dataGridView1.Rows)
                    {
                        _sql += "SELECT postavi_kolicinu_sql_funkcija_prema_sifri('" + r.Cells["sifra"].FormattedValue.ToString() + $", '{Global.Functions.GetDateParam()}'') AS odgovor; ";
                    }

                    frmLoad l = new frmLoad();
                    l.Text = "Radim provjeru skladišta";
                    l.Show();
                    classSQL.insert(_sql);
                    l.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //---------------------------------------------------------------SPREMANJE-----------------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            if (DTpostavke.Rows[0]["sustav_pdv"].ToString() != "1")
            {
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    int __pdv;
                    int.TryParse(r.Cells["pdv"].FormattedValue.ToString(), out __pdv);
                    if (__pdv > 0)
                    {
                        if (MessageBox.Show("Vaša tvrtka nije u sustavu pdv-a a vi ste odabrali u stavci pdv od: " + txtPDV.SelectedValue + "%.\r\n" +
                            " Ako želite spremiti sa pogrešnim pdv-om pritisnite 'YES' a ako želite ispraviti stavku pritisnite na 'NO'", "Bitno upozorenje.",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Error) != DialogResult.Yes)
                        {
                            return;
                        }
                        break;
                    }
                }
            }

            //---------------------------------------------------- zapisnik ------------------------------------------
            DataTable dtZapisnikStavke = null;
            if (!izradaPogresneKalkulacije && !Class.Dokumenti.isKasica && Class.Postavke.automatski_zapisnik)
                dtZapisnikStavke = Class.ZapisnikPromjeneCijene.createTableForSale();
            //---------------------------------------------------- zapisnik ------------------------------------------

            DataTable DTsend = new DataTable();
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("fak_cijena");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("prijevoz");
            DTsend.Columns.Add("carina");
            DTsend.Columns.Add("marza_postotak");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("posebni_porez");
            DTsend.Columns.Add("ppmv");
            DTsend.Columns.Add("broj");
            DTsend.Columns.Add("sifra");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("id_kalkulacija");
            DataRow row;

            btnSve.Enabled = true;
            provjeri_za_nivelaciju();
            string Str = txtSifraDobavljac.Text.Trim();
            double Num;
            bool isNum = double.TryParse(Str, out Num);
            if (!isNum)
            {
                txtSifraDobavljac.Text = "0";
            }

            if (dataGridView1.Rows.Count < 1)
            {
                MessageBox.Show("Nemate niti jedan artikl za spremiti", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtPDV.SelectedValue == null)
            {
                MessageBox.Show("PDV nije odabran.");
                return;
            }

            if (update)
            {
                string brojZaIspis = txtBrojKalkulacije.Text;
                string skladisteZaIspis = CBskladiste.SelectedValue.ToString();
                update_kalkulacija();
                ReadOnly(true);
                if (MessageBox.Show("Spremljeno.\r\nŽelite li ispisati kalkulaciju?", "Spremljeno", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //TODO: tu treba provjeru zabaciti da se vidi koji je oib il da radje napravim po postavkama? hhhhh, to sad pitance.
                    if (Class.Postavke.idKalkulacija == 2)
                    {
                        Report.Kalkulacija.frmKalkulacija2016 kalk = new Report.Kalkulacija.frmKalkulacija2016();
                        kalk.broj_kalkulacije = brojZaIspis;
                        kalk.skladiste = skladisteZaIspis;
                        kalk.ShowDialog();
                    }
                    else
                    {
                        Report.Kalkulacija.frmKalkulacija kalk = new Report.Kalkulacija.frmKalkulacija();
                        kalk.broj_kalkulacije = brojZaIspis;
                        kalk.skladiste = skladisteZaIspis;
                        kalk.ShowDialog();
                    }
                }

                txtBrojKalkulacije.ReadOnly = false;
                ControlDisableEnable(true, false, false, true, false);
                return;
            }
            string broj = brojKalkulacije();
            if (broj.Trim() != txtBrojKalkulacije.Text.Trim())
            {
                MessageBox.Show("Vaš broj dokumenta već je netko iskoristio.\r\nDodijeljen Vam je novi broj '" + broj + "'.", "Info");
                txtBrojKalkulacije.Text = broj;
            }

            double veleprodaja = 0;
            double maloprodaja = 0;
            double fak_cijena = 0;
            double fak_cijena_Rabat = 0;
            double rabat = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                //---------------------------------------------------- zapisnik ------------------------------------------
                if (!izradaPogresneKalkulacije && !Class.Dokumenti.isKasica && Class.Postavke.automatski_zapisnik)
                {
                    double kolZaZapisnik = 0;
                    DataSet dsKolZaZapisnik = classSQL.select(string.Format("select coalesce(replace(kolicina, ',','.')::numeric, 0) AS kolicina from roba_prodaja where id_skladiste = {0} and sifra= '{1}'", CBskladiste.SelectedValue, dg(i, "sifra")), "kolicina");
                    if (dsKolZaZapisnik.Tables[0].Rows.Count > 0)
                        double.TryParse(dsKolZaZapisnik.Tables[0].Rows[0]["kolicina"].ToString(), out kolZaZapisnik);

                    DataRow drRow = dtZapisnikStavke.NewRow();
                    drRow["sifra"] = dg(i, "sifra");
                    drRow["mpc"] = dg(i, "mpc");
                    drRow["kolicina"] = kolZaZapisnik.ToString();
                    drRow["id_skladiste"] = CBskladiste.SelectedValue.ToString();
                    dtZapisnikStavke.Rows.Add(drRow);
                }
                //---------------------------------------------------- zapisnik ------------------------------------------

                rabat = Convert.ToDouble(dataGridView1.Rows[i].Cells["rabat"].FormattedValue.ToString());
                veleprodaja += (Convert.ToDouble(dataGridView1.Rows[i].Cells["VPC"].FormattedValue.ToString()) * Convert.ToDouble(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString()));
                maloprodaja += (Convert.ToDouble(dataGridView1.Rows[i].Cells["MPC"].FormattedValue.ToString()) * Convert.ToDouble(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString()));
                //fak_cijena = Convert.ToDouble(dataGridView1.Rows[i].Cells["fak_cijena"].Value) + fak_cijena;
                //fak_cijena_Rabat = (fak_cijena * rabat / 100) + fak_cijena_Rabat;
                fak_cijena = (Convert.ToDouble(dataGridView1.Rows[i].Cells["nab_cijena"].FormattedValue.ToString()) * Convert.ToDouble(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString())) + fak_cijena;
            }

            //------------------------------ zapisnik spremanje start -----------------------------
            bool kreiraniZapisnik = false;
            if (!izradaPogresneKalkulacije && !Class.Dokumenti.isKasica && Class.Postavke.automatski_zapisnik)
            {
                Class.ZapisnikPromjeneCijene _zapisnik = new Class.ZapisnikPromjeneCijene(dtZapisnikStavke, false, dtpDatumNow.Value.AddSeconds(-1), "Kreiranje zapisnika zbog promjene cijena na kalkulaciji", txtBrojKalkulacije.Text + "/" + Util.Korisno.GodinaKojaSeKoristiUbazi + "-" + CBskladiste.SelectedValue);

                kreiraniZapisnik = _zapisnik.kreiraniZapisnik;
                _zapisnik = null;
            }

            //------------------------------- zapisnik spremanje end ------------------------------

            //DateTime dRac = Convert.ToDateTime(dtpRacun.Value);
            //string dtRac = dRac.Month + "." + dRac.Day + "." + dRac.Year;

            //DateTime dOtp = Convert.ToDateTime(dtpOtpremnica.Value);
            //string dtOtp = dOtp.Month + "." + dOtp.Day + "." + dOtp.Year;

            //DateTime dNow = Convert.ToDateTime(dtpDatumNow.Value);
            //string dtNow = dNow.Month + "." + dNow.Day + "." + dNow.Year;

            string fak_ukupno = (fak_cijena - fak_cijena_Rabat).ToString().Replace(",", ".");
            string vpc_ukupno = veleprodaja.ToString();
            string mpc_ukupno = maloprodaja.ToString();
            if (classSQL.remoteConnectionString == "")
            {
                fak_ukupno = fak_ukupno.Replace(",", ".");
                vpc_ukupno = vpc_ukupno.Replace(",", ".");
                mpc_ukupno = mpc_ukupno.Replace(",", ".");
            }
            else
            {
                fak_ukupno = fak_ukupno.Replace(".", ",");
                vpc_ukupno = vpc_ukupno.Replace(".", ",");
                mpc_ukupno = mpc_ukupno.Replace(".", ",");
            }

            classSQL.transaction("BEGIN;");
            string sql = "INSERT INTO kalkulacija (godina,broj,id_partner,racun,otpremnica,racun_datum,otpremnica_datum," +
            "mjesto_troska,datum,ukupno_vpc,ukupno_mpc,fakturni_iznos,tecaj,id_valuta,id_skladiste,trosak,id_zaposlenik,novo)" +
            " VALUES (" +
            "'" + nuGodinaKalk.Text + "'," +
            "'" + txtBrojKalkulacije.Text + "'," +
            "'" + txtSifraDobavljac.Text + "'," +
            "'" + txtBrojRac.Text + "'," +
            "'" + txtOtpremnica.Text + "'," +
            "'" + dtpRacun.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
            "'" + dtpOtpremnica.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
            "'" + txtMjestoTroska.Text + "'," +
            "'" + dtpDatumNow.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
            "'" + vpc_ukupno + "'," +
            "'" + mpc_ukupno + "'," +
            "'" + fak_ukupno + "'," +
            "'" + txtValutaValuta.Text + "'," +
            "'" + cbValuta.SelectedValue + "'," +
            "'" + CBskladiste.SelectedValue + "'," +
            "" + trosak + "," +
            "'" + Properties.Settings.Default.id_zaposlenik + "','1'" +
            ")";
            provjera_sql(classSQL.transaction(sql));

            string kol;
            decimal ppmv = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                decimal.TryParse(dataGridView1.Rows[i].Cells["ppmv"].FormattedValue.ToString(), out ppmv);

                kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), CBskladiste.SelectedValue.ToString(), dg(i, "kolicina"), "1", "+");

                string updt = "UPDATE roba_prodaja SET kolicina='" + kol.ToString() + "', ppmv = '" + ppmv.ToString().Replace(',', '.') + "' WHERE id_skladiste = '" + CBskladiste.SelectedValue.ToString() +
                        "' AND sifra='" + dg(i, "sifra") + "';";

                classSQL.update(updt);

                row = DTsend.NewRow();
                row["kolicina"] = dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString();
                row["fak_cijena"] = dataGridView1.Rows[i].Cells["fak_cijena"].FormattedValue.ToString();
                row["rabat"] = dataGridView1.Rows[i].Cells["rabat"].FormattedValue.ToString();
                row["prijevoz"] = dataGridView1.Rows[i].Cells["prijevoz"].FormattedValue.ToString();
                row["carina"] = dataGridView1.Rows[i].Cells["carina"].FormattedValue.ToString();
                row["marza_postotak"] = dataGridView1.Rows[i].Cells["marza"].FormattedValue.ToString();
                row["porez"] = dataGridView1.Rows[i].Cells["pdv"].FormattedValue.ToString();
                row["posebni_porez"] = dataGridView1.Rows[i].Cells["posebni_porez"].FormattedValue.ToString();
                row["ppmv"] = dataGridView1.Rows[i].Cells["ppmv"].FormattedValue.ToString();
                row["broj"] = txtBrojKalkulacije.Text;
                row["sifra"] = dataGridView1.Rows[i].Cells["sifra"].FormattedValue.ToString();
                row["vpc"] = dataGridView1.Rows[i].Cells["vpc"].FormattedValue.ToString();
                row["id_skladiste"] = CBskladiste.SelectedValue.ToString();
                row["id_kalkulacija"] = classSQL.select("Select coalesce(max(id_kalkulacija) zbroj 1,1) From kalkulacija", "idkalk").Tables[0].Rows[0][0].ToString();
                DTsend.Rows.Add(row);

                string nbc = dataGridView1.Rows[i].Cells["nab_cijena"].FormattedValue.ToString();
                string vpc = dataGridView1.Rows[i].Cells["vpc"].FormattedValue.ToString();
                string mpc = dataGridView1.Rows[i].Cells["mpc"].FormattedValue.ToString();

                if (classSQL.remoteConnectionString == "")
                {
                    nbc = nbc.Replace(",", ".");
                    vpc = vpc.Replace(",", ".");
                    mpc = mpc.Replace(",", ".");
                }
                else
                {
                    nbc = nbc.Replace(".", ",");
                    vpc = vpc.Replace(",", ".");
                    mpc = mpc.Replace(".", ",");
                }

                decimal prosjecna_nabavna = 0, nabavna_stavka = 0;
                decimal broj_istih = 0;
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    if (dataGridView1.Rows[i].Cells["sifra"].FormattedValue.ToString() == r.Cells["sifra"].FormattedValue.ToString())
                    {
                        decimal.TryParse(r.Cells["nab_cijena"].FormattedValue.ToString(), out nabavna_stavka);
                        prosjecna_nabavna += nabavna_stavka;
                        broj_istih++;
                    }
                }
                prosjecna_nabavna = Math.Round((prosjecna_nabavna / broj_istih), 4);

                classSQL.transaction("UPDATE roba SET nc='" + prosjecna_nabavna.ToString().Replace(".", ",") + "',porez='" + dataGridView1.Rows[i].Cells["pdv"].FormattedValue.ToString() + "',mpc='" + mpc + "',vpc='" + vpc + "' WHERE sifra='" + dataGridView1.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'");
                classSQL.transaction("UPDATE roba_prodaja SET nc='" + prosjecna_nabavna.ToString().Replace(".", ",") + "',porez='" + dataGridView1.Rows[i].Cells["pdv"].FormattedValue.ToString() + "',vpc='" + vpc + "' WHERE sifra='" + dataGridView1.Rows[i].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + CBskladiste.SelectedValue + "'");

                classSQL.transaction(string.Format(@"UPDATE roba SET nc = '{0}', porez = '{1}', mpc = '{2}', vpc = '{3}', proizvodacka_cijena = {4}
WHERE sifra='{5}';",
prosjecna_nabavna.ToString().Replace(".", ","),
dataGridView1.Rows[i].Cells["pdv"].FormattedValue.ToString(),
mpc,
vpc,
prosjecna_nabavna.ToString().Replace(',', '.'),
dataGridView1.Rows[i].Cells["sifra"].FormattedValue.ToString()));

                classSQL.transaction(string.Format(@"UPDATE roba_prodaja SET nc = '{0}', porez = '{1}', vpc = '{2}', proizvodacka_cijena = {3}
WHERE sifra='{4}' AND id_skladiste='{5}'",
prosjecna_nabavna.ToString().Replace(".", ","),
dataGridView1.Rows[i].Cells["pdv"].FormattedValue.ToString(),
vpc,
prosjecna_nabavna.ToString().Replace(',', '.'),
dataGridView1.Rows[i].Cells["sifra"].FormattedValue.ToString(),
CBskladiste.SelectedValue));

                updatePovratnaNaknada(dataGridView1.Rows[i].Cells["sifra"].FormattedValue.ToString(),
                    dataGridView1.Rows[i].Cells["povrNaknada"].FormattedValue.ToString());
            }

            SQL.SQLkalkulacija.InsertStavke(DTsend);
            classSQL.transaction("COMMIT;");

            classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Nova kalkulacija br." + txtBrojKalkulacije.Text + "')");
            Util.AktivnostZaposlenika.SpremiAktivnost(dataGridView1, CBskladiste.SelectedValue.ToString(), "Kalkulacija", txtBrojKalkulacije.Text, false);
            PregledKolicine();
            ReadOnly(true);
            EnableDisable(false);
            delete_fields();
            txtBrojKalkulacije.Text = brojKalkulacije();
            txtBrojKalkulacije.ReadOnly = false;
            btnSve.Enabled = true;
            update = false;

            if (broj != txtBrojKalkulacije.Text)
            {
                if (kreiraniZapisnik && !Class.Dokumenti.isKasica && Class.Postavke.automatski_zapisnik)
                {
                    MessageBox.Show("Kreiran je automatski zapisnik zbog promjene cijene na kalkulaciji.");
                }

                if (MessageBox.Show("Spremljeno.\r\nŽelite li ispisati kalkulaciju?", "Spremljeno", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (Class.Postavke.idKalkulacija == 2)
                    {
                        Report.Kalkulacija.frmKalkulacija2016 kalk = new Report.Kalkulacija.frmKalkulacija2016();
                        kalk.broj_kalkulacije = broj;
                        kalk.skladiste = CBskladiste.SelectedValue.ToString();
                        kalk.ShowDialog();
                    }
                    else
                    {
                        Report.Kalkulacija.frmKalkulacija kalk = new Report.Kalkulacija.frmKalkulacija();
                        kalk.broj_kalkulacije = broj;
                        kalk.skladiste = CBskladiste.SelectedValue.ToString();
                        kalk.ShowDialog();
                    }
                }
            }
            else
            {
                MessageBox.Show("Kalkulacija nije spremljena.", "Greška!");
            }
            trosak = 0;
            ControlDisableEnable(true, false, false, true, false);
        }

        private void provjeri_za_nivelaciju()
        {
            //string id_kalk = classSQL.select("Select max(id_kalkulacija) From kalkulacija", "idkalk").Tables[0].Rows[0][0].ToString();
            //decimal epsilon = Convert.ToDecimal("0,01");
            //DataTable DTnivprovjera = classSQL.select("Select * From kalkulacija_stavke Where id_kalkulacija = '" + id_kalk + "'", "provjera").Tables[0];
            //for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //{
            //    decimal vpc = Convert.ToDecimal(dataGridView1.Rows[i].Cells["vpc"].FormattedValue.ToString());
            //    decimal pdv = Convert.ToDecimal(dataGridView1.Rows[i].Cells["pdv"].FormattedValue.ToString());
            //    decimal mpc = Math.Round(vpc * (1 + (pdv / 100)), 3);
            //        string sif = DTnivprovjera.Rows[i]["sifra"].ToString();
            //        decimal mpc_roba = Convert.ToDecimal(classSQL.select("Select * From roba Where sifra = '" + sif + "'", "provjera").Tables[0].Rows[0]["mpc"].ToString());
            //        decimal rez = mpc - mpc_roba;
            //        decimal roba_prodaja = Convert.ToDecimal(classSQL.select("Select * From roba_prodaja Where sifra = '" + sif + "'", "provjera").Tables[0].Rows[0]["kolicina"].ToString());
            //        decimal ukupno_za_kolicinu = Math.Round(roba_prodaja * rez,3);
            //        if (rez > epsilon)
            //        {
            //            classSQL.insert("INSERT INTO nivelacija ( stara_cijena, nova_cijena, " +
            //            "razlika, po_dokumentu, broj_dokumenta, sifra, datum, kolicina_za_razliku, ukupno_za_kolicinu) VALUES " +
            //            "('" + mpc_roba.ToString().Replace(",", ".") + "', '" + mpc.ToString().Replace(",", ".") + "', '" + rez.ToString().Replace(",", ".") + "', 'Kalkulacija', '" + id_kalk + "', '" + sif + "', '" + DateTime.Now + "', '" + roba_prodaja.ToString().Replace(",", ".") + "', '" + ukupno_za_kolicinu.ToString().Replace(",", ".") + "')");
            //        }

            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            roba_trazi.ShowDialog();
            if (Properties.Settings.Default.id_roba != "")
            {
                ReadOnly(false);
                EnableDisable(true);
                Fill_Roba(Properties.Settings.Default.id_roba);
            }
        }

        private void btnOtvoriKalkulacije_Click(object sender, EventArgs e)
        {
            frmPopisKalkulacija popisKalkulacija = new frmPopisKalkulacija();
            popisKalkulacija.MainForm = this;
            broj_kalkulacije_edit = null;
            popisKalkulacija.ShowDialog();

            if (broj_kalkulacije_edit != null)
            {
                edit_kalkulacija(broj_kalkulacije_edit, edit_skladiste);
                ReadOnly(false);
                EnableDisable(true);
                ControlDisableEnable(false, true, true, false, true);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0)
            {
                MessageBox.Show("Nemate stavke za brisanje.", "Greška");
                return;
            }

            if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id_stavka"].Value.ToString() != "")
            {
                if (MessageBox.Show("Brisanjem ove stavke brišete i količinu robe sa skladišta. Da li ste sigurni da želite obrisati ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    classSQL.transaction("BEGIN;");
                    int sadf = dataGridView1.SelectedRows[0].Index;

                    DataRow[] dataROW = DTstavke.Select("id_stavka = " + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString());
                    string kol = classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + Properties.Settings.Default.idSkladiste + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString();
                    kol = (Convert.ToDouble(kol) - Convert.ToDouble(dataROW[0]["kolicina"].ToString())).ToString();
                    classSQL.transaction("UPDATE roba_prodaja SET kolicina='" + kol + "' WHERE sifra='" + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + Properties.Settings.Default.idSkladiste + "'");

                    classSQL.transaction("DELETE FROM kalkulacija_stavke WHERE id_stavka='" + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() + "'");
                    if (classSQL.transaction("COMMIT;") == "")
                        dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                    Util.AktivnostZaposlenika.SpremiAktivnost(dataGridView1, CBskladiste.SelectedValue.ToString(), "Kalkulacija", txtBrojKalkulacije.Text, true);
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Cells[0].Value = i + 1;
                    }
                }
            }
            else
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = i + 1;
                }
            }
            //MessageBox.Show("Obrisano.");
        }

        private void btnObrisiSve_Click(object sender, EventArgs e)
        {
            trosak = 0;
            if (dataGridView1.RowCount == 0) { MessageBox.Show("Nemate stavke za brisanje.", "Greška"); return; }
            if (MessageBox.Show("Brisanjem ove kalkulacije brišete i količinu robe sa skladišta. Da li ste sigurni da želite obrisati ovu kalkulaciju?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                classSQL.transaction("BEGIN;");
                int y = dataGridView1.Rows.Count;
                for (int i = 0; i < y; i++)
                {
                    if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() != "")
                    {
                        DataRow[] dataROW = DTstavke.Select("id_stavka = " + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString());
                        DataTable ddt = classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + CBskladiste.SelectedValue + "'", "roba_prodaja").Tables[0];
                        if (ddt.Rows.Count > 0)
                        {
                            string kol = ddt.Rows[0][0].ToString();
                            kol = (Convert.ToDouble(kol) - Convert.ToDouble(dataROW[0]["kolicina"].ToString())).ToString();
                            classSQL.transaction("UPDATE roba_prodaja SET kolicina='" + kol + "' WHERE sifra='" + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + CBskladiste.SelectedValue + "'");
                        }
                        classSQL.transaction("DELETE FROM kalkulacija_stavke WHERE id_stavka='" + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() + "'");
                        dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                    }
                    else
                    {
                        dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                    }
                }

                classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") + "','Brisanje kalkulacije br." + txtBrojKalkulacije.Text + "')");
                Util.AktivnostZaposlenika.SpremiAktivnost(dataGridView1, CBskladiste.SelectedValue.ToString(), "Kalkulacija", txtBrojKalkulacije.Text, true);
                classSQL.transaction("DELETE FROM kalkulacija WHERE broj='" + txtBrojKalkulacije.Text + "' AND id_skladiste='" + edit_skladiste + "'");
                classSQL.transaction("COMMIT;");
                PregledKolicine();
                ReadOnly(true);
                MessageBox.Show("Obrisano.");
                ControlDisableEnable(true, false, false, true, false);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReadOnly(true);
            btnSve.Enabled = true;
            delete_fields();
            EnableDisable(false);
            txtBrojKalkulacije.Text = brojKalkulacije();
            txtBrojKalkulacije.Enabled = true;
            txtBrojKalkulacije.ReadOnly = false;
            txtBrojKalkulacije.Select();
            ControlDisableEnable(true, false, false, true, false);
            trosak = 0;
        }

        #endregion buttons

        #region dgw

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
                return;
            EnableDisable(true);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            change();
        }

        private void change()
        {
            try
            {
                if (dataGridView1.RowCount == 0)
                    return;

                int br = 0;
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    br = dataGridView1.SelectedCells[0].RowIndex;
                }
                else
                {
                    return;
                }

                txtSifra_robe.Text = dataGridView1.Rows[br].Cells[1].FormattedValue.ToString();
                txtNazivRobe.Text = dataGridView1.Rows[br].Cells[2].FormattedValue.ToString();
                txtKolicina.Text = dataGridView1.Rows[br].Cells[3].FormattedValue.ToString();
                txtFakCijena.Text = dataGridView1.Rows[br].Cells[4].FormattedValue.ToString();
                SrediValutuPoFakCijeni();
                txtMarza.Text = dataGridView1.Rows[br].Cells[6].FormattedValue.ToString();
                txtVPC.Text = dataGridView1.Rows[br].Cells[7].FormattedValue.ToString();
                txtMPC.Text = dataGridView1.Rows[br].Cells[8].FormattedValue.ToString();
                TXT_MPC = txtMPC.Text;
                txtRabat.Text = dataGridView1.Rows[br].Cells[9].FormattedValue.ToString();
                txtPrijevoz.Text = dataGridView1.Rows[br].Cells[10].FormattedValue.ToString();
                txtCarina.Text = dataGridView1.Rows[br].Cells[11].FormattedValue.ToString();
                txtPosebniPorez.Text = dataGridView1.Rows[br].Cells[12].FormattedValue.ToString();
                txtPovrNaknada.Text = dataGridView1.Rows[br].Cells["povrNaknada"].FormattedValue.ToString();
                txtPDV.SelectedValue = Convert.ToDecimal(dataGridView1.Rows[br].Cells[13].FormattedValue.ToString());

                txtPPMV.Text = dataGridView1.Rows[br].Cells["ppmv"].FormattedValue.ToString();

                //if (txtPovrNaknada.Text.Trim() == "")
                //{
                //    txtPovrNaknada.Text = "0,00";
                //    dataGridView1.Rows[br].Cells["povrNaknada"].Value = txtPovrNaknada.Text;
                //}

                double pdv = Convert.ToDouble(txtPDV.SelectedValue);
                double kol = Convert.ToDouble(txtKolicina.Text);
                double marza = Convert.ToDouble(txtMarza.Text);

                txtIznosFakCijena.Text = (kol * Convert.ToDouble(txtFakCijena.Text)).ToString();

                txtIznosRabat.Text = Convert.ToString(Convert.ToDouble(txtFakCijena.Text) * Convert.ToDouble(txtRabat.Text) / 100);

                nabavna_ukupno = vratiNabavnu();

                //nabavna_ukupno = Math.Round(nabavna_ukupno, 2);

                double VPC = Math.Round(Convert.ToDouble(txtVPC.Text), 3);

                txtNabavnaCijenaKune.Text = Math.Round(nabavna_ukupno + kol, 3).ToString("#0.000");
                txtVPC.Text = VPC.ToString("#0.000");
                txtIznosVPC.Text = Math.Round(VPC * kol, 3).ToString("#0.000");

                double MPC;
                MPC = Math.Round((VPC * pdv / 100) + VPC, 2);

                if (Class.Postavke.koristi_povratnu_naknadu)
                    MPC += Convert.ToDouble(txtPovrNaknada.Text);

                MPC += Convert.ToDouble(txtPPMV.Text);

                txtUkupno.Text = Math.Round(VPC - nabavna_ukupno, 3).ToString("#0.000");
                txtIznosPDV.Text = Math.Round(VPC * (pdv / 100) * kol, 3).ToString("#0.000");
                txtIznosMPC.Text = Math.Round(MPC * kol, 3).ToString("#0.000");

                //txtMarza.Text = ((VPC / nabavna_ukupno - 1) * 100).ToString("#0.0000");

                txtMPC.Text = Math.Round(MPC, 3).ToString("#0.000");

                changeDataGrid();

                srediDecimalnaMjesta();
            }
            catch (Exception)
            {
            }
        }

        private void changeDataGrid()
        {
            if (dataGridView1.SelectedRows.Count < 1)
            {
                MessageBox.Show("Odaberite robu ili stavku!", "Upozorenje!");
                return;
            }

            int i = dataGridView1.SelectedCells[0].RowIndex;

            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["fakturnaCijenaValuta"].Value = txtFakCijenaVal.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[1].Value = txtSifra_robe.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[2].Value = txtNazivRobe.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[3].Value = txtKolicina.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[4].Value = txtFakCijena.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[5].Value = Math.Round(nabavna_ukupno, 4).ToString("#0.0000");
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[6].Value = txtMarza.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[7].Value = txtVPC.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[8].Value = txtMPC.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[9].Value = txtRabat.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[10].Value = txtPrijevoz.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[11].Value = txtCarina.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[12].Value = txtPosebniPorez.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[13].Value = txtPDV.SelectedValue;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["povrNaknada"].Value = txtPovrNaknada.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["ppmv"].Value = txtPPMV.Text;

            //dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["povrNaknada"].Value = txtPovrNaknada.Text.Trim() == "" ? "0,00" : txtPovrNaknada.Text;
        }

        private string dg(int row, string cell)
        {
            return dataGridView1.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        #endregion dgw

        #region edit/update

        private void edit_kalkulacija(string id, string skl)
        {
            ReadOnly(false);
            update = true;

            string sql1 = "SELECT id_kalkulacija FROM kalkulacija WHERE godina='" + nuGodinaKalk.Value.ToString() + "' AND broj='" + id + "' AND id_skladiste='" + CBskladiste.SelectedValue + "'";
            DT1 = classSQL.select(sql1, "kalkulacija").Tables[0];

            DataTable DTKalkulacije = classSQL.select("SELECT * FROM kalkulacija WHERE broj='" + id + "' AND id_skladiste='" + skl + "'", "kalkulacija").Tables[0];
            string sql = "SELECT kalkulacija.id_skladiste,kalkulacija_stavke.kolicina,kalkulacija_stavke.fak_cijena " +
                " ,kalkulacija_stavke.rabat,kalkulacija_stavke.prijevoz,kalkulacija_stavke.vpc,kalkulacija_stavke.carina," +
                " kalkulacija_stavke.marza_postotak,kalkulacija_stavke.porez,kalkulacija_stavke.posebni_porez,kalkulacija_stavke.broj," +
                " kalkulacija_stavke.sifra,kalkulacija_stavke.id_stavka,roba.naziv, kalkulacija_stavke.ppmv, kalkulacija.trosak " +
                " FROM kalkulacija_stavke " +
                " LEFT JOIN kalkulacija ON kalkulacija_stavke.broj=kalkulacija.broj AND kalkulacija_stavke.id_skladiste=kalkulacija.id_skladiste " +
                " LEFT JOIN roba ON roba.sifra=kalkulacija_stavke.sifra" +
                " WHERE kalkulacija_stavke.broj='" + id + "' AND kalkulacija.id_skladiste='" + skl + "'" +
                " ORDER BY kalkulacija_stavke.id_stavka";
            DTstavke = classSQL.select(sql, "kalkulacija_stavke").Tables[0];

            //kalkulacija
            SetTrosak(Convert.ToDecimal(DTKalkulacije.Rows[0]["trosak"].ToString()));
            txtBrojKalkulacije.Text = DTKalkulacije.Rows[0]["broj"].ToString();
            txtSifraDobavljac.Text = DTKalkulacije.Rows[0]["id_partner"].ToString();
            txtBrojRac.Text = DTKalkulacije.Rows[0]["racun"].ToString();
            txtOtpremnica.Text = DTKalkulacije.Rows[0]["otpremnica"].ToString();
            dtpRacun.Text = DTKalkulacije.Rows[0]["racun_datum"].ToString();
            dtpOtpremnica.Text = DTKalkulacije.Rows[0]["otpremnica_datum"].ToString();
            txtMjestoTroska.Text = DTKalkulacije.Rows[0]["mjesto_troska"].ToString();
            dtpDatumNow.Text = DTKalkulacije.Rows[0]["datum"].ToString();
            nuGodinaKalk.Text = DTKalkulacije.Rows[0]["godina"].ToString();
            cbValuta.SelectedValue = DTKalkulacije.Rows[0]["id_valuta"].ToString();
            txtValutaValuta.Text = DTKalkulacije.Rows[0]["tecaj"].ToString();
            CBskladiste.SelectedValue = DTKalkulacije.Rows[0]["id_skladiste"].ToString();
            txtZaposlenik.Text = classSQL.select("SELECT ime + ' ' + prezime AS ime_prezime FROM zaposlenici WHERE id_zaposlenik='" + DTKalkulacije.Rows[0]["id_zaposlenik"].ToString() + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
            Properties.Settings.Default.idSkladiste = DTKalkulacije.Rows[0]["id_skladiste"].ToString();
            Properties.Settings.Default.Save();

            decimal.TryParse(txtValutaValuta.Text, out GLmarza);

            dataGridView1.Rows.Clear();

            double kol, fakCijena, nabavna, VPC, marza, pdv, MPC, tecaj, prijevoz, Kolicina, Rabat, ppmv = 0;
            int i = 0;
            foreach (DataRow row in DTstavke.Rows)
            {
                ppmv = 0;
                double.TryParse(row["fak_cijena"].ToString(), out fakCijena);
                double.TryParse(txtValutaValuta.Text, out tecaj);
                double.TryParse(row["prijevoz"].ToString(), out prijevoz);
                double.TryParse(row["kolicina"].ToString(), out Kolicina);
                double.TryParse(row["rabat"].ToString(), out Rabat);
                double.TryParse(row["ppmv"].ToString(), out ppmv);

                nabavna = (fakCijena - (fakCijena * Rabat / 100)) + prijevoz;

                double.TryParse(row["vpc"].ToString(), out VPC);
                double.TryParse(row["porez"].ToString(), out pdv);
                MPC = VPC * (1 + (pdv / 100));

                dataGridView1.Rows.Add(i + 1,
                    row["sifra"].ToString(),
                    row["naziv"].ToString(),
                    row["kolicina"].ToString(),
                    row["fak_cijena"].ToString(),
                    Math.Round(nabavna, 2, MidpointRounding.AwayFromZero).ToString("#0.00"),
                    row["marza_postotak"].ToString(),
                    row["vpc"].ToString(),
                    Math.Round(MPC, 2).ToString("#0.00"),
                    row["rabat"].ToString(),
                    row["prijevoz"].ToString(),
                    row["carina"].ToString(),
                    row["posebni_porez"].ToString(),
                    row["porez"].ToString(),
                    row["id_stavka"].ToString(),
                    setPovratnaNaknada(DTstavke.Rows[i]["sifra"].ToString()),
                    fakCijena / tecaj,
                    ppmv.ToString()
                    );

                i++;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected = true;
            }

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
            }

            ControlDisableEnable(false, true, true, false, true);
        }

        //-----------------------------------------------------------UPDATE----------------------------------------------------------

        private void update_kalkulacija()
        {
            //------------------------------------ zapisnik -----------------------------------------------------
            DataTable dtZapisnikStavke = null;
            if (!izradaPogresneKalkulacije && !Class.Dokumenti.isKasica && Class.Postavke.automatski_zapisnik)
                dtZapisnikStavke = Class.ZapisnikPromjeneCijene.createTableForSale();
            //------------------------------------ zapisnik -----------------------------------------------------

            string id = txtBrojKalkulacije.Text;
            string sql1 = "SELECT id_kalkulacija FROM kalkulacija WHERE godina='" + nuGodinaKalk.Value.ToString() + "' AND broj='" + id + "' AND id_skladiste='" + CBskladiste.SelectedValue + "'";
            DT1 = classSQL.select(sql1, "kalkulacija").Tables[0];

            DataTable DTsend = new DataTable();
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("fak_cijena");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("prijevoz");
            DTsend.Columns.Add("carina");
            DTsend.Columns.Add("marza_postotak");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("posebni_porez");
            DTsend.Columns.Add("broj");
            DTsend.Columns.Add("sifra");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("id_kalkulacija");
            DTsend.Columns.Add("ppmv");
            DataRow row;

            DataTable DTsend2 = new DataTable();
            DTsend2.Columns.Add("kolicina");
            DTsend2.Columns.Add("fak_cijena");
            DTsend2.Columns.Add("rabat");
            DTsend2.Columns.Add("prijevoz");
            DTsend2.Columns.Add("carina");
            DTsend2.Columns.Add("marza_postotak");
            DTsend2.Columns.Add("porez");
            DTsend2.Columns.Add("posebni_porez");
            DTsend2.Columns.Add("broj");
            DTsend2.Columns.Add("where_broj");
            DTsend2.Columns.Add("sifra");
            DTsend2.Columns.Add("vpc");
            DTsend2.Columns.Add("id_skladiste");
            DTsend2.Columns.Add("id_skladiste_staro");
            DTsend2.Columns.Add("id_stavka");
            DTsend2.Columns.Add("ppmv");
            DataRow row2;

            double veleprodaja = 0;
            double maloprodaja = 0;
            double fak_cijena = 0;
            double fak_cijena_Rabat = 0;
            double rabat = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                rabat = Convert.ToDouble(dataGridView1.Rows[i].Cells["rabat"].FormattedValue.ToString());
                veleprodaja += (Convert.ToDouble(dataGridView1.Rows[i].Cells["VPC"].FormattedValue.ToString()) * Convert.ToDouble(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString()));
                maloprodaja += (Convert.ToDouble(dataGridView1.Rows[i].Cells["MPC"].FormattedValue.ToString()) * Convert.ToDouble(dataGridView1.Rows[i].Cells["Kolicina"].FormattedValue.ToString()));
                fak_cijena += (Convert.ToDouble(dataGridView1.Rows[i].Cells["nab_cijena"].FormattedValue.ToString()) * Convert.ToDouble(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString()));
            }

            //------------------------------------ zapisnik -----------------------------------------------------
            bool kreiraniZapisnik = false;
            if (!izradaPogresneKalkulacije && Class.Postavke.automatski_zapisnik)
            {
                Class.ZapisnikPromjeneCijene _zapisnik = new Class.ZapisnikPromjeneCijene(dtZapisnikStavke, false, dtpDatumNow.Value.AddSeconds(-1), "Kreiranje zapisnika zbog promjene cijena na kalkulaciji", txtBrojKalkulacije.Text + "/" + Util.Korisno.GodinaKojaSeKoristiUbazi + "-" + CBskladiste.SelectedValue, kreiranZapisnikZaFakturu);
                kreiraniZapisnik = _zapisnik.kreiraniZapisnik;
                _zapisnik = null;
            }
            //------------------------------------ zapisnik -----------------------------------------------------

            string fak_ukupno = (fak_cijena - fak_cijena_Rabat).ToString().Replace(",", ".");
            string vpc_ukupno = veleprodaja.ToString();
            string mpc_ukupno = maloprodaja.ToString();
            if (classSQL.remoteConnectionString == "")
            {
                fak_ukupno = fak_ukupno.Replace(",", ".");
                vpc_ukupno = vpc_ukupno.Replace(",", ".");
                mpc_ukupno = mpc_ukupno.Replace(",", ".");
            }
            else
            {
                fak_ukupno = fak_ukupno.Replace(".", ",");
                vpc_ukupno = vpc_ukupno.Replace(".", ",");
                mpc_ukupno = mpc_ukupno.Replace(".", ",");
            }

            classSQL.transaction("BEGIN;");
            string sql_update_kalkulacija = @"UPDATE kalkulacija SET
broj ='" + txtBrojKalkulacije.Text + @"',
id_partner='" + txtSifraDobavljac.Text + @"',
racun='" + txtBrojRac.Text + @"',
racun_datum='" + dtpRacun.Value.ToString("yyyy-MM-dd H:mm:ss") + @"',
otpremnica_datum='" + dtpOtpremnica.Value.ToString("yyyy-MM-dd H:mm:ss") + @"',
mjesto_troska='" + txtMjestoTroska.Text + @"',
datum='" + dtpDatumNow.Value.ToString("yyyy-MM-dd H:mm:ss") + @"',
godina='" + nuGodinaKalk.Text + @"',
ukupno_vpc='" + vpc_ukupno + @"',
ukupno_mpc='" + mpc_ukupno + @"',
tecaj = '" + txtValutaValuta.Text + @"',
id_valuta='" + cbValuta.SelectedValue + @"',
fakturni_iznos='" + fak_ukupno + @"',
editirano='1',
trosak='" + trosak.ToString().Replace(',', '.') + @"',
id_skladiste='" + CBskladiste.SelectedValue + @"'
WHERE broj ='" + broj_kalkulacije_edit + @"' AND id_skladiste='" + edit_skladiste + @"';";

            provjera_sql(classSQL.transaction(sql_update_kalkulacija));

            //Update kalkulacija_stavke
            string kol;
            decimal ppmv = 0;
            for (int br = 0; br < dataGridView1.Rows.Count; br++)
            {
                if (Class.Postavke.prodaja_automobila)
                    decimal.TryParse(dataGridView1.Rows[br].Cells["ppmv"].FormattedValue.ToString(), out ppmv);

                string id_stavka = dataGridView1.Rows[br].Cells["id_stavka"].FormattedValue.ToString();
                if (id_stavka == "")
                {
                    //INSERT
                    kol = SQL.ClassSkladiste.GetAmount(dg(br, "sifra"), CBskladiste.SelectedValue.ToString(), dg(br, "kolicina"), "1", "+");
                    //SQL.SQLroba_prodaja.UpdateRows(CBskladiste.SelectedValue.ToString(), kol, dg(br, "sifra"));
                    kol = Math.Round(Convert.ToDecimal(kol), 3).ToString();
                    string updt = @"UPDATE roba_prodaja SET
kolicina='" + kol.ToString() + @"', ppmv = '" + ppmv.ToString().Replace(',', '.') + @"'
WHERE id_skladiste='" + CBskladiste.SelectedValue.ToString() + @"'
AND sifra = '" + dg(br, "sifra") + "';";
                    classSQL.update(updt);

                    row = DTsend.NewRow();
                    row["kolicina"] = dataGridView1.Rows[br].Cells["kolicina"].FormattedValue.ToString();
                    row["fak_cijena"] = dataGridView1.Rows[br].Cells["fak_cijena"].FormattedValue.ToString();
                    row["rabat"] = dataGridView1.Rows[br].Cells["rabat"].FormattedValue.ToString();
                    row["prijevoz"] = dataGridView1.Rows[br].Cells["prijevoz"].FormattedValue.ToString();
                    row["carina"] = dataGridView1.Rows[br].Cells["carina"].FormattedValue.ToString();
                    row["marza_postotak"] = dataGridView1.Rows[br].Cells["marza"].FormattedValue.ToString();
                    row["porez"] = dataGridView1.Rows[br].Cells["pdv"].FormattedValue.ToString();
                    row["posebni_porez"] = dataGridView1.Rows[br].Cells["posebni_porez"].FormattedValue.ToString();
                    row["ppmv"] = dataGridView1.Rows[br].Cells["ppmv"].FormattedValue.ToString();
                    row["broj"] = txtBrojKalkulacije.Text;
                    row["sifra"] = dataGridView1.Rows[br].Cells["sifra"].FormattedValue.ToString();
                    row["vpc"] = dataGridView1.Rows[br].Cells["vpc"].FormattedValue.ToString();
                    row["id_skladiste"] = CBskladiste.SelectedValue.ToString();
                    row["id_kalkulacija"] = DT1.Rows[0]["id_kalkulacija"].ToString();
                    DTsend.Rows.Add(row);
                }
                else
                {
                    //UPDATE

                    //ako je promijenjeno skladište
                    DataRow[] dataROW = DTstavke.Select("id_stavka = " + id_stavka);
                    if (CBskladiste.SelectedValue.ToString() != dataROW[0]["id_skladiste"].ToString())
                    {
                        //vraća na staro skladiste
                        kol = SQL.ClassSkladiste.GetAmount(dg(br, "sifra"),
                            dataROW[0]["id_skladiste"].ToString(),
                            dataROW[0]["kolicina"].ToString(),
                            "1",
                            "-");
                        kol = Math.Round(Convert.ToDecimal(kol), 3).ToString();
                        /*SQL.SQLroba_prodaja.UpdateRows(
                            dataROW[0]["id_skladiste"].ToString(),
                            kol,
                            dg(br, "sifra"));*/

                        string updt = @"UPDATE roba_prodaja SET kolicina='" + kol.ToString() + @"', ppmv = 0
WHERE id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + @"'
AND sifra='" + dg(br, "sifra") + @"';";
                        classSQL.update(updt);

                        //oduzima sa novog skladiste
                        kol = SQL.ClassSkladiste.GetAmount(
                            dg(br, "sifra"),
                            CBskladiste.SelectedValue.ToString(),
                            dg(br, "kolicina"),
                            "1",
                            "+");
                        kol = Math.Round(Convert.ToDecimal(kol), 3).ToString();
                        /*SQL.SQLroba_prodaja.UpdateRows(
                            CBskladiste.SelectedValue.ToString(),
                            kol,
                            dg(br, "sifra"));*/

                        updt = @"UPDATE roba_prodaja SET
kolicina = '" + kol.ToString() + @"', ppmv = '" + ppmv.ToString().Replace(',', '.') + @"'
WHERE id_skladiste='" + CBskladiste.SelectedValue.ToString() + @"'
AND sifra='" + dg(br, "sifra") + @"';";

                        classSQL.update(updt);
                    }
                    else
                    {
                        kol = SQL.ClassSkladiste.GetAmount(
                            dg(br, "sifra"),
                            CBskladiste.SelectedValue.ToString(),
                            (Convert.ToDouble(dataROW[0]["kolicina"].ToString()) - Convert.ToDouble(dg(br, "kolicina"))).ToString(),
                            "1",
                            "-");
                        kol = Math.Round(Convert.ToDecimal(kol), 3).ToString();
                        /*SQL.SQLroba_prodaja.UpdateRows(
                            CBskladiste.SelectedValue.ToString(),
                            kol,
                            dg(br, "sifra"));*/

                        string updt = @"UPDATE roba_prodaja SET
kolicina='" + kol.ToString() + @"', ppmv = '" + ppmv.ToString().Replace(',', '.') + @"'
WHERE id_skladiste='" + CBskladiste.SelectedValue.ToString() + @"' AND sifra='" + dg(br, "sifra") + @"';";
                        classSQL.update(updt);
                    }

                    row2 = DTsend2.NewRow();
                    row2["kolicina"] = dataGridView1.Rows[br].Cells["kolicina"].FormattedValue.ToString();
                    row2["fak_cijena"] = dataGridView1.Rows[br].Cells["fak_cijena"].FormattedValue.ToString();
                    row2["rabat"] = dataGridView1.Rows[br].Cells["rabat"].FormattedValue.ToString();
                    row2["prijevoz"] = dataGridView1.Rows[br].Cells["prijevoz"].FormattedValue.ToString();
                    row2["carina"] = dataGridView1.Rows[br].Cells["carina"].FormattedValue.ToString();
                    row2["marza_postotak"] = dataGridView1.Rows[br].Cells["marza"].FormattedValue.ToString();
                    row2["porez"] = dataGridView1.Rows[br].Cells["pdv"].FormattedValue.ToString();
                    row2["posebni_porez"] = dataGridView1.Rows[br].Cells["posebni_porez"].FormattedValue.ToString();
                    row2["ppmv"] = dataGridView1.Rows[br].Cells["ppmv"].FormattedValue.ToString();
                    row2["broj"] = txtBrojKalkulacije.Text;
                    row2["where_broj"] = broj_kalkulacije_edit;
                    row2["sifra"] = dataGridView1.Rows[br].Cells["sifra"].FormattedValue.ToString();
                    row2["vpc"] = dataGridView1.Rows[br].Cells["vpc"].FormattedValue.ToString();
                    row2["id_skladiste"] = CBskladiste.SelectedValue.ToString();
                    row2["id_skladiste_staro"] = edit_skladiste;
                    row2["id_stavka"] = dataGridView1.Rows[br].Cells["id_stavka"].FormattedValue.ToString();
                    DTsend2.Rows.Add(row2);
                }//ako je stavka ==""

                string nbc = dataGridView1.Rows[br].Cells["nab_cijena"].FormattedValue.ToString();
                string vpc = dataGridView1.Rows[br].Cells["vpc"].FormattedValue.ToString();
                string mpc = dataGridView1.Rows[br].Cells["mpc"].FormattedValue.ToString();
                if (classSQL.remoteConnectionString == "")
                {
                    nbc = nbc.Replace(",", ".");
                    vpc = vpc.Replace(",", ".");
                    mpc = mpc.Replace(",", ".");
                }
                else
                {
                    nbc = nbc.Replace(".", ",");
                    vpc = vpc.Replace(",", ".");
                    mpc = mpc.Replace(".", ",");
                }

                classSQL.transaction(string.Format(@"UPDATE roba SET nc = '{0}', porez = '{1}', mpc = '{2}', vpc = '{3}', proizvodacka_cijena = {4}
WHERE sifra='{5}';",
nbc,
dataGridView1.Rows[br].Cells["pdv"].FormattedValue.ToString(),
mpc,
vpc,
nbc.Replace(',', '.'),
dataGridView1.Rows[br].Cells["sifra"].FormattedValue.ToString()));

                classSQL.transaction(string.Format(@"UPDATE roba_prodaja SET nc = '{0}', porez = '{1}', vpc = '{2}', proizvodacka_cijena = {3}
WHERE sifra='{4}' AND id_skladiste='{5}'",
nbc,
dataGridView1.Rows[br].Cells["pdv"].FormattedValue.ToString(),
vpc,
nbc.Replace(',', '.'),
dataGridView1.Rows[br].Cells["sifra"].FormattedValue.ToString(),
CBskladiste.SelectedValue));

                //update povratna_naknada
                updatePovratnaNaknada(dataGridView1.Rows[br].Cells["sifra"].FormattedValue.ToString(),
                    dataGridView1.Rows[br].Cells["povrNaknada"].FormattedValue.ToString());
            }//for

            provjera_sql(SQL.SQLkalkulacija.InsertStavke(DTsend));
            provjera_sql(SQL.SQLkalkulacija.UpdateStavke(DTsend2));
            classSQL.transaction("COMMIT;");

            PregledKolicine();

            classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Uređivanje kalkulacije br." + txtBrojKalkulacije.Text + "')");
            Util.AktivnostZaposlenika.SpremiAktivnost(dataGridView1, CBskladiste.SelectedValue.ToString(), "Kalkulacija", txtBrojKalkulacije.Text, true);
            EnableDisable(false);
            delete_fields();
            update = false;
            txtBrojKalkulacije.Text = brojKalkulacije();
            btnSve.Enabled = true;

            if (kreiraniZapisnik && !Class.Dokumenti.isKasica && Class.Postavke.automatski_zapisnik)
            {
                MessageBox.Show("Kreiran je automatski zapisnik za promjenu cijena zbog promjene maloprodajne cijene na kalkulaciji.");
            }
        }

        private void updatePovratnaNaknada(string sifra, string povrNaknada)
        {
            string sql = string.Format(@"SELECT id FROM povratna_naknada
LEFT JOIN roba ON povratna_naknada.sifra = roba.sifra
WHERE roba.sifra = '{0}';", sifra);
            DataTable sifrePovratnaNaknadaUpdate = classSQL.select(sql, "povratna_naknada").Tables[0];

            povrNaknada = classSQL.remoteConnectionString == "" ? povrNaknada.Replace(",", ".") : povrNaknada.Replace(",", ".");

            if (sifrePovratnaNaknadaUpdate.Rows.Count > 0)
            {
                sql = "UPDATE povratna_naknada SET sifra = '" + sifra + "', iznos = '" + povrNaknada + "'" +
                    " WHERE id='" + sifrePovratnaNaknadaUpdate.Rows[0]["id"].ToString() + "'";
                provjera_sql(classSQL.transaction(sql));

                if (sifrePovratnaNaknadaUpdate.Rows.Count > 1)
                {
                    for (int i = 1; i == sifrePovratnaNaknadaUpdate.Rows.Count - 1; i++)
                    {
                        sql = string.Format(@"DELETE FROM povratna_naknada
WHERE id = '{0}';",
sifrePovratnaNaknadaUpdate.Rows[i]["id"].ToString());
                        provjera_sql(classSQL.update(sql));
                    }
                }
            }
            else
            {
                sql = "INSERT INTO povratna_naknada(sifra,iznos)" +
                    " VALUES('" + sifra + "','" + povrNaknada + "')";

                provjera_sql(classSQL.transaction(sql));
            }
        }

        #endregion edit/update

        #region form controls

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            double nabavni_valuta = 0;
            double nabavni_kune = 0;
            double prijevoz = 0;
            double carina = 0;
            double troskovi = 0;
            double posebni_porez = 0;
            double pdv = 0;
            double veleprodaja = 0;
            double maloprodaja = 0;
            double rabat = 0;
            decimal Kolicina = 0;
            double fak_cijena = 0;
            double fak_cijena_Rabat = 0;
            decimal proba = 0;
            decimal fakturnaCijenaValuta = 0;
            decimal fakturnaCijenaValutaUkupno = 0;

            string ss = txtValutaValuta.Text;

            if (tabControl1.SelectedIndex == 2)
            {
                if (dataGridView1.Rows.Count < 1)
                {
                    tabControl1.SelectedTab = tabPage2;
                    MessageBox.Show("Za pogled na rekapitulaciju morate imati najmanje jednu stavku upisanu.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    double PDV_postotak = Convert.ToDouble(dataGridView1.Rows[i].Cells["pdv"].FormattedValue.ToString());
                    rabat = Convert.ToDouble(dataGridView1.Rows[i].Cells["rabat"].FormattedValue.ToString());
                    nabavni_valuta += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells["nab_cijena"].FormattedValue.ToString()) *
                        Convert.ToDouble(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString()), 3);
                    nabavni_kune += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells["nab_cijena"].FormattedValue.ToString()) *
                        Convert.ToDouble(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString()), 3);
                    fak_cijena += Convert.ToDouble(dataGridView1.Rows[i].Cells["fak_cijena"].Value);
                    prijevoz += Convert.ToDouble(dataGridView1.Rows[i].Cells["prijevoz"].FormattedValue.ToString());
                    carina += Convert.ToDouble(dataGridView1.Rows[i].Cells["carina"].FormattedValue.ToString());
                    posebni_porez += Convert.ToDouble(dataGridView1.Rows[i].Cells["posebni_porez"].FormattedValue.ToString());
                    troskovi += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells["nab_cijena"].FormattedValue.ToString()) *
                        Convert.ToDouble(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString()), 3);
                    pdv += (troskovi * PDV_postotak / 100);
                    veleprodaja += (Convert.ToDouble(dataGridView1.Rows[i].Cells["VPC"].FormattedValue.ToString()) *
                        Convert.ToDouble(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString()));
                    maloprodaja += (Convert.ToDouble(dataGridView1.Rows[i].Cells["MPC"].FormattedValue.ToString()) *
                        Convert.ToDouble(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString()));
                    fak_cijena_Rabat += (Convert.ToDouble(dataGridView1.Rows[i].Cells["fak_cijena"].Value) * rabat / 100);

                    decimal.TryParse(dataGridView1.Rows[i].Cells["fakturnaCijenaValuta"].FormattedValue.ToString(), out fakturnaCijenaValuta);
                    decimal.TryParse(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString(), out Kolicina);
                    fakturnaCijenaValutaUkupno += (fakturnaCijenaValuta - (fakturnaCijenaValuta * (decimal)rabat / 100)) * Kolicina;
                }

                try
                {
                    dataGridView2.Rows[0].Cells[1].Value = Math.Round(fakturnaCijenaValutaUkupno, 3).ToString("#0.00");
                }
                catch
                {
                    dataGridView2.Rows[0].Cells[1].Value = Math.Round(fakturnaCijenaValutaUkupno, 3).ToString("#0.00");
                }

                dataGridView2.Rows[1].Cells[1].Value = Math.Round(nabavni_kune, 3).ToString("#0.00");
                dataGridView2.Rows[2].Cells[1].Value = Math.Round(prijevoz, 3).ToString("#0.00");
                dataGridView2.Rows[3].Cells[1].Value = Math.Round(carina, 3).ToString("#0.00");
                dataGridView2.Rows[4].Cells[1].Value = Math.Round(troskovi, 3).ToString("#0.00");
                dataGridView2.Rows[5].Cells[1].Value = Math.Round(posebni_porez, 3).ToString("#0.00");
                dataGridView2.Rows[6].Cells[1].Value = Math.Round(veleprodaja, 3).ToString("#0.00");
                dataGridView2.Rows[7].Cells[1].Value = Math.Round(maloprodaja, 3).ToString("#0.00");

                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.Format = "N2";
                dataGridView2.Columns[1].DefaultCellStyle = style;
                dataGridView2.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView2.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            else if (tabControl1.SelectedIndex == 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    double PDV_postotak = Convert.ToDouble(dataGridView1.Rows[i].Cells["pdv"].FormattedValue.ToString());
                    double kolicina = Convert.ToDouble(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString());
                    rabat = Convert.ToDouble(dataGridView1.Rows[i].Cells["rabat"].FormattedValue.ToString());

                    nabavni_valuta += (Convert.ToDouble(dataGridView1.Rows[i].Cells["nab_cijena"].FormattedValue.ToString()) * kolicina);
                    nabavni_kune += Convert.ToDouble(dataGridView1.Rows[i].Cells["nab_cijena"].FormattedValue.ToString());
                    fak_cijena += (Convert.ToDouble(dataGridView1.Rows[i].Cells["fak_cijena"].Value) * kolicina);
                    prijevoz += Convert.ToDouble(dataGridView1.Rows[i].Cells["prijevoz"].FormattedValue.ToString());
                    carina += Convert.ToDouble(dataGridView1.Rows[i].Cells["carina"].FormattedValue.ToString());
                    troskovi += (Convert.ToDouble(dataGridView1.Rows[i].Cells["nab_cijena"].FormattedValue.ToString()) * Convert.ToDouble(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString()));
                    posebni_porez += Convert.ToDouble(dataGridView1.Rows[i].Cells["posebni_porez"].FormattedValue.ToString());
                    pdv += (troskovi * PDV_postotak / 100);
                    veleprodaja += Convert.ToDouble(dataGridView1.Rows[i].Cells["VPC"].FormattedValue.ToString());
                    maloprodaja += Convert.ToDouble(dataGridView1.Rows[i].Cells["MPC"].FormattedValue.ToString());
                    fak_cijena_Rabat += (fak_cijena * rabat / 100);
                }

                if (cbValuta.Text != "HR Kuna")
                {
                    txtValutaFakIznos.Text = Math.Round(fak_cijena, 3).ToString("#0.00");
                    txtValutaIznosRabata.Text = Math.Round(fak_cijena_Rabat, 3).ToString("#0.00");
                    txtValutaUkupniIznos.Text = Math.Round(fak_cijena - fak_cijena_Rabat, 3).ToString("#0.00");
                    txtValutaUkupniPorez.Text = Math.Round(pdv / Convert.ToDouble(txtValutaValuta.Text), 3).ToString("#0.00");
                }

                txtKuneUkupno.Text = Math.Round((fak_cijena - fak_cijena_Rabat) * Convert.ToDouble(txtValutaValuta.Text), 3).ToString("#0.00");
                txtKuneCarina.Text = Math.Round(carina, 3).ToString("#0.00");
                txtKunePrijevoz.Text = Math.Round(prijevoz, 3).ToString("#0.00");
                txtKunePosebniPorez.Text = Math.Round(posebni_porez, 3).ToString("#0.00");
                txtKuneNabavniIznos.Text = Math.Round(((fak_cijena - fak_cijena_Rabat) *
                    Convert.ToDouble(txtValutaValuta.Text))
                    + carina + prijevoz + posebni_porez, 3).ToString("#0.00");
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                string aa = txtValutaValuta.Text;
                //cbValuta.Enabled = dataGridView1.Rows.Count == 0 ? false : true;
                //txtValutaValuta.Enabled = dataGridView1.Rows.Count == 0 ? false : true;
                txtSifra_robe.Select();

                if (dataGridView1.RowCount > 0)
                {
                    //txtValutaValuta.Text = GLmarza.ToString();
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtSifraDobavljac.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtImeDobavljaca.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void CBskladiste_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (load == true)
            {
                if (update == true && edit_skladiste == CBskladiste.SelectedValue.ToString())
                {
                    txtBrojKalkulacije.Text = broj_kalkulacije_edit;
                }
                else
                {
                    txtBrojKalkulacije.Text = brojKalkulacije();
                }
            }
        }

        private void txtPDV_SelectedValueChanged(object sender, EventArgs e)
        {
            if (txtSifra_robe.Text == "")
            {
                return;
            }

            Izracun2(sender);

            try
            {
                srediDecimalnaMjesta();
            }
            catch
            {
            }
        }

        #endregion form controls

        private void txtIznosRabat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
            }
            if (e.KeyChar == ','
                && (sender as TextBox).Text.IndexOf(',') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtIznosRabat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                decimal postotak = 0;
                decimal iznos_jedinicni = 0;
                decimal iznos_postotka = 0;

                iznos_jedinicni = Convert.ToDecimal(txtFakCijena.Text);
                iznos_postotka = Convert.ToDecimal(txtIznosRabat.Text);

                postotak = (iznos_postotka * 100) / iznos_jedinicni;

                txtRabat.Text = Math.Round(postotak, 3).ToString();

                txtPrijevoz.Select();
                //txtRabat_KeyDown(new object(),new System.Windows.Forms.KeyDownEventArgs(e) );,
            }
        }

        private void chbDodajPDV_CheckedChanged(object sender, EventArgs e)
        {
            dodaj_pdv = chbDodajPDV.Checked;
            if (dodaj_pdv)
            {
                Decimal vr = Convert.ToDecimal(txtFakCijenaVal.Text);
                txtFakCijenaVal.Text = ((vr * Convert.ToDecimal(txtPDV.SelectedValue) / 100) + vr).ToString();
                txtFakCijena.Select();
            }
            else
            {
                Decimal vr = Convert.ToDecimal(txtFakCijenaVal.Text);
                txtFakCijenaVal.Text = ((vr / (1 + (Convert.ToDecimal(txtPDV.SelectedValue) / 100)))).ToString();
                txtFakCijena.Select();
            }
        }

        private void txtMarza_TextChanged(object sender, EventArgs e)
        {
            if (txtMarza.Text == "Infinity")
            {
                txtMarza.Text = "0";
            }
        }

        private void MoveFile()
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "JPG|*.jpg;*.jpeg|PDF|*.pdf|"
       + "All Graphics Types|*.pdf;*.jpg";

            openFileDialog1.FilterIndex = 3;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (File.Exists(openFileDialog1.FileName))
                    {
                        if (!Directory.Exists("skenirani_fajlovi"))
                            Directory.CreateDirectory("skenirani_fajlovi");

                        string fileDestination = DTpostavke.Rows[0]["putanja_za_skenirane_fajlove"].ToString() + "KAL_BR_" + txtBrojKalkulacije.Text +
                            "_SK_" + CBskladiste.SelectedValue.ToString() +
                            "_" + DateTime.Now.Year + Path.GetExtension(openFileDialog1.FileName);

                        try
                        {
                            if (File.Exists(fileDestination))
                                File.Delete(fileDestination);
                        }
                        catch (Exception ex) { MessageBox.Show("Greška: Ne mogu obrisati dokumenat iz diska.\r\n Orginalna greška: " + ex.Message); }

                        File.Move(openFileDialog1.FileName, fileDestination);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška: Ne mogu učitati dokumenat iz diska.\r\n Orginalna greška: " + ex.Message);
                }
            }
        }

        private void OpenFile(string fileDestination)
        {
            if (MessageBox.Show("Datoteka za ovaj dokumenat već postoji.\r\nAko želite otvoriti dokumenat pritisnite 'YES'\r\na ako želite urediti postavljeni dokumenat pritisnite 'NO'.", "Uredi/Otvori",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                MoveFile();
                ProvjeriDaliPostojiSkeniraniDok();
            }
            else
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.EnableRaisingEvents = false;
                proc.StartInfo.FileName = fileDestination;
                proc.Start();
            }
        }

        private void frmSkeniraniDoc_Click(object sender, EventArgs e)
        {
            string filePDF = DTpostavke.Rows[0]["putanja_za_skenirane_fajlove"].ToString() + "KAL_BR_" + txtBrojKalkulacije.Text +
                "_SK_" + CBskladiste.SelectedValue.ToString() +
                "_" + nuGodinaKalk.Value.ToString() + ".pdf";

            string fileJpg = DTpostavke.Rows[0]["putanja_za_skenirane_fajlove"].ToString() + "KAL_BR_" + txtBrojKalkulacije.Text +
                "_SK_" + CBskladiste.SelectedValue.ToString() +
                "_" + nuGodinaKalk.Value.ToString() + ".jpg";

            if (File.Exists(filePDF))
            {
                OpenFile(filePDF);
            }
            else if (File.Exists(fileJpg))
            {
                OpenFile(fileJpg);
            }
            else
            {
                MoveFile();
                ProvjeriDaliPostojiSkeniraniDok();
            }
        }

        private void ProvjeriDaliPostojiSkeniraniDok()
        {
            string filePDF = DTpostavke.Rows[0]["putanja_za_skenirane_fajlove"].ToString() + "KAL_BR_" + txtBrojKalkulacije.Text +
                "_SK_" + CBskladiste.SelectedValue.ToString() +
                "_" + nuGodinaKalk.Value.ToString() + ".pdf";

            string fileJpg = DTpostavke.Rows[0]["putanja_za_skenirane_fajlove"].ToString() + "KAL_BR_" + txtBrojKalkulacije.Text +
                "_SK_" + CBskladiste.SelectedValue.ToString() +
                "_" + nuGodinaKalk.Value.ToString() + ".jpg";

            if (File.Exists(filePDF) || File.Exists(fileJpg))
            {
                frmSkeniraniDoc.Image = global::PCPOS.Properties.Resources.Done;
                this.toolTip1.SetToolTip(this.frmSkeniraniDoc, "Skenirani dokumenat postoji na adresi: " + fileJpg);
            }
            else
            {
                frmSkeniraniDoc.Image = global::PCPOS.Properties.Resources.scanner1;
                this.toolTip1.SetToolTip(this.frmSkeniraniDoc, "Skenirani dokumenat ne postoji.");
            }
        }

        private void txtBrojKalkulacije_TextChanged(object sender, EventArgs e)
        {
            ProvjeriDaliPostojiSkeniraniDok();
        }

        private void cbValuta_SelectionChangeCommitted(object sender, EventArgs e)
        {
            decimal.TryParse(cbValuta.SelectedValue.ToString(), out GLmarza);
            DSValuta = classSQL.select("SELECT * FROM valute WHERE id_valuta='" + cbValuta.SelectedValue.ToString() + "'", "valute");
            if (DSValuta.Tables[0].Rows.Count > 0)
                txtValutaValuta.Text = DSValuta.Tables[0].Rows[0]["tecaj"].ToString();
        }

        private void btnRaspodjeliPrijevoz_Click(object sender, EventArgs e)
        {
            frmRaspodjeliTroskovePrijevoza rtp = new frmRaspodjeliTroskovePrijevoza();
            rtp.Kalkulacija = this;
            rtp.ShowDialog();
        }

        public void PreracunajPrijevozPoStavkama(decimal IznosUkupnogPrijevoza)
        {
            decimal iznosPrijevozaPoStavci, koeficijent, nbcStavka, ukupanIznosKalkulacijeNBC, kolicinaStavka, Vpc, PrijevozStavka;

            //koristim za test
            ukupanIznosKalkulacijeNBC = VratiUkupnoNabavnuCijenu();
            trosak = IznosUkupnogPrijevoza;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                decimal.TryParse(row.Cells["nab_cijena"].FormattedValue.ToString(), out nbcStavka);
                decimal.TryParse(row.Cells["kolicina"].FormattedValue.ToString(), out kolicinaStavka);
                decimal.TryParse(row.Cells["prijevoz"].FormattedValue.ToString(), out PrijevozStavka);

                koeficijent = (kolicinaStavka * (nbcStavka - PrijevozStavka)) / ukupanIznosKalkulacijeNBC;
                iznosPrijevozaPoStavci = Math.Round(IznosUkupnogPrijevoza * koeficijent, 5);
                row.Cells["prijevoz"].Value = iznosPrijevozaPoStavci / kolicinaStavka;

                decimal.TryParse(row.Cells["vpc"].FormattedValue.ToString(), out Vpc);
                row.Cells["nab_cijena"].Value = Math.Round(nbcStavka - PrijevozStavka + iznosPrijevozaPoStavci / kolicinaStavka, 2, MidpointRounding.AwayFromZero);
                row.Cells["marza"].Value = Math.Round(((Vpc / (nbcStavka - PrijevozStavka + (iznosPrijevozaPoStavci / kolicinaStavka)) - 1) * 100), 3).ToString("#0.00000000");
            }
            txtPrijevoz.Text = "0";
        }

        private decimal VratiUkupnoNabavnuCijenu()
        {
            decimal nbcStavka, kolicinaStavka, UkupnoNBC = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                decimal.TryParse(row.Cells["nab_cijena"].FormattedValue.ToString(), out nbcStavka);
                decimal.TryParse(row.Cells["kolicina"].FormattedValue.ToString(), out kolicinaStavka);
                UkupnoNBC += nbcStavka * kolicinaStavka;
            }
            return UkupnoNBC;
        }

        private void Izracun2(object sender)
        {
            TextBox txt = new TextBox();
            ComboBox cb = new ComboBox();

            if (sender.GetType() == typeof(TextBox))
                txt = (TextBox)sender;
            else
                cb = (ComboBox)sender;

            decimal Kolicina, FakturnaCijenaKn, FakturnaCijenaValuta, Rabat, Marza, Vpc, Mpc, Pdv, Tecaj, NabavnaUkupno, Prijevoz, pov_nak, ppmv, carina;
            decimal.TryParse(txtKolicina.Text, out Kolicina);
            decimal.TryParse(txtFakCijena.Text, out FakturnaCijenaKn);
            decimal.TryParse(txtFakCijenaVal.Text, out FakturnaCijenaValuta);
            decimal.TryParse(txtRabat.Text, out Rabat);
            decimal.TryParse(txtMarza.Text, out Marza);
            decimal.TryParse(txtVPC.Text, out Vpc);
            decimal.TryParse(txtMPC.Text, out Mpc);
            decimal.TryParse(txtPDV.SelectedValue.ToString(), out Pdv);
            decimal.TryParse(txtValutaValuta.Text, out Tecaj);
            decimal.TryParse(txtPrijevoz.Text, out Prijevoz);
            decimal.TryParse(txtPovrNaknada.Text, out pov_nak);
            decimal.TryParse(txtPPMV.Text, out ppmv);
            decimal.TryParse(txtCarina.Text, out carina);

            if (!Class.Postavke.koristi_povratnu_naknadu)
                pov_nak = 0;

            //Fakturna cijena u valuti
            FakturnaCijenaKn = Math.Round((FakturnaCijenaValuta * Tecaj), 2);
            txtFakCijena.Text = FakturnaCijenaKn.ToString("#0.0000");
            txtIznosFakCijena.Text = (FakturnaCijenaKn * Kolicina).ToString("#0.00");

            //Rabat
            txtIznosRabat.Text = Math.Round((FakturnaCijenaKn * Rabat / 100), 2).ToString("#0.0000");

            //Nabavna Cijena
            NabavnaUkupno = Math.Round((FakturnaCijenaKn - (FakturnaCijenaKn * Rabat / 100)) + Prijevoz + carina, 2);
            txtNabavnaCijenaKune.Text = NabavnaUkupno.ToString("#0.00");

            //Marža
            if (txt.Name != "txtMarza")
                txtMarza.Text = ((Vpc / (NabavnaUkupno == 0 ? 1 : NabavnaUkupno) - 1) * 100).ToString("#0.00000000");

            if (txt.Name == "txtMarza")
            {
                Vpc = Math.Round((NabavnaUkupno + (NabavnaUkupno * Marza / 100)), 3);
                txtVPC.Text = Math.Round((Vpc), 3).ToString("#0.000");
                txtMPC.Text = Math.Round((Vpc * (1 + (Pdv / 100))) + pov_nak + ppmv, 3).ToString("#0.000");
                decimal.TryParse(txtMPC.Text, out Mpc);
            }

            //PDV
            if (cb.Name == "txtPDV")
            {
                txtMPC.Text = Math.Round((Vpc * (1 + (Pdv / 100))) + pov_nak + ppmv, 3).ToString("#0.000");
            }

            //Vpc
            if (txt.Name == "txtVPC")
            {
                txtMarza.Text = ((Vpc / NabavnaUkupno - 1) * 100).ToString("#0.00000000");
                txtMPC.Text = Math.Round((Vpc * (1 + (Pdv / 100))) + pov_nak + ppmv, 3).ToString("#0.000");
            }

            //Mpc
            if (txt.Name == "txtMPC")
            {
                txtVPC.Text = Math.Round(((Mpc - pov_nak - ppmv) / (1 + (Pdv / 100))), 3).ToString("#0.000");
                decimal.TryParse(txtVPC.Text, out Vpc);
                txtMarza.Text = ((Vpc / NabavnaUkupno - 1) * 100).ToString("#0.00000000");
            }

            txtIznosVPC.Text = Math.Round((Vpc * Kolicina), 3).ToString("#0.000");
            txtIznosPDV.Text = Math.Round(((Vpc * Pdv / 100) * Kolicina), 2).ToString("#0.00");
            txtIznosMPC.Text = Math.Round((Mpc * Kolicina), 2).ToString("#0.00");
        }

        private void AutomatskiIzracunPoStavkama()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                //fakturnaCijenaValuta
                decimal Kolicina, FakturnaCijenaKn, FakturnaCijenaValuta, Rabat, Marza, Vpc, Mpc, Pdv, Tecaj, NabavnaUkupno, Prijevoz;
                decimal.TryParse(row.Cells["kolicina"].FormattedValue.ToString(), out Kolicina);
                decimal.TryParse(row.Cells["fak_cijena"].FormattedValue.ToString(), out FakturnaCijenaKn);
                decimal.TryParse(txtValutaValuta.Text, out Tecaj);
                decimal.TryParse(row.Cells["fakturnaCijenaValuta"].FormattedValue.ToString(), out FakturnaCijenaValuta);
                decimal.TryParse(row.Cells["rabat"].FormattedValue.ToString(), out Rabat);
                decimal.TryParse(row.Cells["marza"].FormattedValue.ToString(), out Marza);
                decimal.TryParse(row.Cells["vpc"].FormattedValue.ToString(), out Vpc);
                decimal.TryParse(row.Cells["mpc"].FormattedValue.ToString(), out Mpc);
                decimal.TryParse(row.Cells["pdv"].FormattedValue.ToString(), out Pdv);
                decimal.TryParse(row.Cells["prijevoz"].FormattedValue.ToString(), out Prijevoz);

                //row.Cells["fak_cijena"].Value = FakturnaCijenaValuta * Tecaj;
                NabavnaUkupno = (FakturnaCijenaKn - (FakturnaCijenaKn * Rabat / 100)) + Prijevoz;
                row.Cells["nab_cijena"].Value = NabavnaUkupno;
                row.Cells["fakturnaCijenaValuta"].Value = FakturnaCijenaKn / Tecaj;
                row.Cells["marza"].Value = ((Vpc / NabavnaUkupno - 1) * 100).ToString("#0.0000000");
                row.Cells["mpc"].Value = Math.Round((Vpc * (1 + (Pdv / 100))), 2).ToString("#0.00");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iznos"></param>
        public void SetTrosak(decimal iznos)
        {
            trosak = iznos;
        }

        private void dtpOtpremnica_ValueChanged(object sender, EventArgs e)
        {
        }

        private void txtPPMV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
                {
                    (sender as TextBox).Text = "0,00";
                }

                txtMarza.Select();
            }
        }

        private void txtPPMV_Leave(object sender, EventArgs e)
        {
        }
    }
}