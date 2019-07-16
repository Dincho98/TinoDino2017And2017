using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmNovaKalkulacija : Form
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

        public frmNovaKalkulacija()
        {
            InitializeComponent();
        }

        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private void frmNovaKalkulacija_Load(object sender, EventArgs e)
        {
            //Util.Korisno k = new Util.Korisno();
            //PaintRows(dataGridView1);
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
                //string sql1 = "SELECT id_kalkulacija FROM kalkulacija WHERE godina='" + nuGodinaKalk.Value.ToString() + "' AND broj='" + brkalk.ToString() + "' AND id_skladiste='" + CBskladiste.SelectedValue + "'";
                DataTable DT = classSQL.select(sql, "kalkulacija").Tables[0];
                //DT1 = classSQL.select(sql1, "kalkulacija").Tables[0];
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

            izracun();

            srediDecimalnaMjesta();
        }

        private void srediDecimalnaMjesta()
        {
            txtKolicina.Text = Math.Round(Convert.ToDouble(txtKolicina.Text), 3).ToString("#0.000");
            txtFakCijena.Text = Math.Round(Convert.ToDouble(txtFakCijena.Text), 4).ToString("#0.0000");
            txtVPC.Text = Math.Round(Convert.ToDouble(txtVPC.Text), 3).ToString("#0.000");
            txtMPC.Text = Math.Round(Convert.ToDouble(txtMPC.Text), 2).ToString("#0.00");
            txtPrijevoz.Text = Math.Round(Convert.ToDouble(txtPrijevoz.Text), 2).ToString("#0.00");
            txtCarina.Text = Math.Round(Convert.ToDouble(txtCarina.Text), 2).ToString("#0.00");
            txtPosebniPorez.Text = Math.Round(Convert.ToDouble(txtPosebniPorez.Text), 2).ToString("#0.00");
            txtIznosRabat.Text = Math.Round(Convert.ToDouble(txtIznosRabat.Text), 3).ToString("#0.000");
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

            //izracun();

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

            double pdv = Convert.ToDouble(txtPDV.SelectedValue);
            double kol = Convert.ToDouble(txtKolicina.Text);
            double marza = Convert.ToDouble(txtMarza.Text);

            txtIznosFakCijena.Text = (kol * Convert.ToDouble(txtFakCijena.Text)).ToString();

            txtIznosRabat.Text = Convert.ToString(Convert.ToDouble(txtFakCijena.Text) * Convert.ToDouble(txtRabat.Text) / 100);

            nabavna_ukupno = vratiNabavnu();
            //nabavna_ukupno = Math.Round(nabavna_ukupno, 2);

            double VPC = nabavna_ukupno;

            txtNabavnaCijenaKune.Text = Math.Round(nabavna_ukupno * kol, 2).ToString("#0.00");
            VPC = Convert.ToDouble(txtVPC.Text);
            VPC = Math.Round(VPC, 3);

            double MPC = nabavna_ukupno;
            MPC = Math.Round((VPC * pdv / 100) + VPC, 2);
            txtUkupno.Text = Math.Round(VPC - nabavna_ukupno, 2).ToString("#0.00");
            txtIznosPDV.Text = Math.Round(VPC * (pdv / 100) * kol, 2).ToString("#0.00");
            txtIznosMPC.Text = Math.Round(MPC * kol, 2).ToString("#0.00");

            txtMarza.Text = ((VPC / nabavna_ukupno - 1) * 100).ToString("#0.0000");

            txtMPC.Text = Math.Round(MPC, 2).ToString("#0.00");

            string test1 = string.Format("{0:0.00}", MPC);
            string test2 = MPC.ToString("#0.00");

            changeDataGrid();

            srediDecimalnaMjesta();
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

            if (',' == (e.KeyChar))
            {
                e.Handled = false;
                return;
            }

            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
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

                if ((DTpodaci.Rows[0]["oib"].ToString() != "67660751355" && DTpodaci.Rows[0]["oib"].ToString() != "76846500940"))
                    for (int y = 0; y < dataGridView1.Rows.Count; y++)
                    {
                        if (txtSifra_robe.Text.Trim() == dataGridView1.Rows[y].Cells["sifra"].FormattedValue.ToString().Trim())
                        {
                            if (MessageBox.Show("Ova šifra već postoji u ovoj kalkulaciji!\r\nŽelite li dodati količinu na već unešenu šifru?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                frmUnosiPovrat up = new frmUnosiPovrat();
                                up.Text = "Unos količine";
                                up.kalku = this;
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

                TBroba = classSQL.select("SELECT * FROM roba WHERE sifra='" + txtSifra_robe.Text + "' AND oduzmi='DA'", "roba").Tables[0];

                if (TBroba.Rows.Count > 0)
                {
                    ReadOnly(false);
                    EnableDisable(true);
                    txtSifra_robe.Text = TBroba.Rows[0]["sifra"].ToString().Trim();
                    txtNazivRobe.Text = TBroba.Rows[0]["naziv"].ToString();

                    setPovratnaNaknada(TBroba.Rows[0]["sifra"].ToString().Trim());

                    setRoba(TBroba.Rows[0]["porez"].ToString());

                    if (dataGridView1.RowCount == 0)
                    {
                        return;
                    }

                    double pdv = Convert.ToDouble(txtPDV.SelectedValue);
                    double kol = Convert.ToDouble(txtKolicina.Text);
                    //double marza = Convert.ToDouble(txtMarza.Text);

                    txtIznosFakCijena.Text = (kol * Convert.ToDouble(txtFakCijena.Text)).ToString();

                    txtIznosRabat.Text = Math.Round(Convert.ToDouble(txtFakCijena.Text) * Convert.ToDouble(txtRabat.Text) / 100, 3).ToString("#0.000");

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
                        txtMarza.Text = ((VPC / nabavna_ukupno - 1) * 100).ToString("#0.0000");
                    }

                    //txtMPC.Text = Math.Round(MPC, 2).ToString("#0.00");
                    TXT_MPC = txtMPC.Text;

                    changeDataGrid();

                    txtKolicina.Select();
                }
                else
                {
                    if (MessageBox.Show("Za ovu šifru ne postoji artikl ili na artiklu nije aktivirano oduzimanje sa skladišta.\r\nŽelite li dodati novu šifru?", "Greška", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                        frmRobaUsluge robaUsluge = new frmRobaUsluge();
                        robaUsluge.Show();
                    }
                }
            }
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

                txtMarza.Select();
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
                izracun();
                txtMPC.Select();
            }
        }

        private void txtMPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
                {
                    (sender as TextBox).Text = "0,00";
                }
                if (TXT_MPC != txtMPC.Text && TXT_MPC != "")
                    izracunMPC();
                txtSifra_robe.Select();
                dataGridView1.ClearSelection();
                novi();
            }
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

            txtFakCijena.Text = Math.Round(val, 4).ToString("#0.0000");
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
            if ((DTpodaci.Rows[0]["oib"].ToString() != "67660751355" && DTpodaci.Rows[0]["oib"].ToString() != "76846500940"))
            {
                for (int y = 0; y < dataGridView1.Rows.Count; y++)
                {
                    if (txtSifra_robe.Text.Trim() == dataGridView1.Rows[y].Cells["sifra"].FormattedValue.ToString().Trim())
                    {
                        if (MessageBox.Show("Ova šifra već postoji u ovoj kalkulaciji!\r\nŽelite li dodati količinu na već unešenu šifru?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            frmUnosiPovrat up = new frmUnosiPovrat();
                            up.Text = "Unos količine";
                            up.kalku = this;
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
                    txtPosebniPorez.Text, pdv, "", txtPovrNaknada.Text);

                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected = true;

                txtPDV.SelectedValue = pdv;
            }
            else
            {
                dataGridView1.Rows.Add(br, txtSifra_robe.Text, txtNazivRobe.Text, txtKolicina.Text, txtFakCijena.Text, "",
                    txtMarza.Text, txtVPC.Text, txtMPC.Text, txtRabat.Text, txtPrijevoz.Text, txtCarina.Text,
                    txtPosebniPorez.Text, txtPDV.SelectedValue, "", txtPovrNaknada.Text);
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected = true;
            }

            //PaintRows(dataGridView1);
        }

        private void setPovratnaNaknada(string sifra)
        {
            DataTable DTPovrNaknd = classSQL.select("SELECT iznos FROM povratna_naknada WHERE sifra='" +
                sifra + "'", "povratna_naknada").Tables[0];

            txtPovrNaknada.Text = DTPovrNaknd.Rows.Count > 0 ? DTPovrNaknd.Rows[0][0].ToString() : "0,00";
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

            txtIznosRabat.Text = Math.Round(Convert.ToDouble(txtFakCijena.Text) * Convert.ToDouble(txtRabat.Text) / 100, 3).ToString("#0.000");

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
                txtMarza.Text = ((VPC / nabavna_ukupno - 1) * 100).ToString("#0.0000");
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
                Convert.ToDouble(txtIznosRabat.Text) +
                (Convert.ToDouble(txtPrijevoz.Text) / Convert.ToDouble(txtKolicina.Text)) +
                (Convert.ToDouble(txtCarina.Text) / Convert.ToDouble(txtKolicina.Text)) +
                (Convert.ToDouble(txtPosebniPorez.Text) / Convert.ToDouble(txtKolicina.Text));
        }

        private void izracunVPC()
        {
            double vpc = Convert.ToDouble(txtVPC.Text);
            double pdv = Convert.ToDouble(txtPDV.SelectedValue);

            nabavna_ukupno = vratiNabavnu();

            //nabavna_ukupno = Math.Round(nabavna_ukupno, 2);

            txtMPC.Text = Math.Round((((vpc * pdv) / 100) + vpc), 2).ToString("#0.00");
            TXT_MPC = txtMPC.Text;
            txtMarza.Text = ((vpc / nabavna_ukupno - 1) * 100).ToString("#0.0000");
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

            //nabavna_ukupno = Math.Round(nabavna_ukupno, 2);

            double pdv = Convert.ToDouble(txtPDV.SelectedValue);
            double porez_preracunata_stopa = Math.Round((100 * pdv) / (100 + pdv), 2);

            double mpc = Convert.ToDouble(txtMPC.Text);

            double vpc = Math.Round(mpc - (mpc * porez_preracunata_stopa / 100), 3);
            txtVPC.Text = vpc.ToString("#0.000");

            txtMarza.Text = ((vpc / nabavna_ukupno - 1) * 100).ToString("#0.0000");
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
            DSporezi = classSQL.select("SELECT * FROM porezi ORDER BY id_porez ASC", "porezi");
            txtPDV.DataSource = DSporezi.Tables[0];
            txtPDV.DisplayMember = "naziv";
            txtPDV.ValueMember = "iznos";

            //DS Valuta
            DSValuta = classSQL.select("SELECT * FROM valute", "valute");
            cbValuta.DataSource = DSValuta.Tables[0];
            cbValuta.DisplayMember = "ime_valute";
            cbValuta.ValueMember = "id_valuta";
            cbValuta.SelectedValue = 5;
            txtValutaValuta.DataBindings.Add("Text", DSValuta.Tables[0], "tecaj");
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
                        _sql += "SELECT postavi_kolicinu_sql_funkcija_prema_sifri('" + r.Cells["sifra"].FormattedValue.ToString() + "') AS odgovor; ";
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

            if (update)
            {
                string brojZaIspis = txtBrojKalkulacije.Text;
                string skladisteZaIspis = CBskladiste.SelectedValue.ToString();
                update_kalkulacija();
                ReadOnly(true);
                if (MessageBox.Show("Spremljeno.\r\nŽelite li ispisati kalkulaciju?", "Spremljeno", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
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
                rabat = Convert.ToDouble(dataGridView1.Rows[i].Cells["rabat"].FormattedValue.ToString());
                veleprodaja += (Convert.ToDouble(dataGridView1.Rows[i].Cells["VPC"].FormattedValue.ToString()) * Convert.ToDouble(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString()));
                maloprodaja += (Convert.ToDouble(dataGridView1.Rows[i].Cells["MPC"].FormattedValue.ToString()) * Convert.ToDouble(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString()));
                //fak_cijena = Convert.ToDouble(dataGridView1.Rows[i].Cells["fak_cijena"].Value) + fak_cijena;
                //fak_cijena_Rabat = (fak_cijena * rabat / 100) + fak_cijena_Rabat;
                fak_cijena = (Convert.ToDouble(dataGridView1.Rows[i].Cells["nab_cijena"].FormattedValue.ToString()) * Convert.ToDouble(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString())) + fak_cijena;
            }

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
            "mjesto_troska,datum,ukupno_vpc,ukupno_mpc,fakturni_iznos,tecaj,id_valuta,id_skladiste,id_zaposlenik)" +
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
            "'" + Properties.Settings.Default.id_zaposlenik + "'" +
            ")";
            provjera_sql(classSQL.transaction(sql));

            string kol;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), CBskladiste.SelectedValue.ToString(), dg(i, "kolicina"), "1", "+");

                string updt = "UPDATE roba_prodaja SET kolicina='" + kol.ToString() +
                        "' WHERE id_skladiste='" + CBskladiste.SelectedValue.ToString() +
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
            //popisKalkulacija.MainForm = this;
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

                int br = dataGridView1.SelectedCells[0].RowIndex;

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
                txtPDV.SelectedValue = dataGridView1.Rows[br].Cells[13].FormattedValue.ToString();
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

                txtNabavnaCijenaKune.Text = Math.Round(nabavna_ukupno * kol, 2).ToString("#0.00");
                txtVPC.Text = VPC.ToString("#0.000");
                txtIznosVPC.Text = Math.Round(VPC * kol, 3).ToString("#0.000");

                double MPC;
                MPC = Math.Round((VPC * pdv / 100) + VPC, 2);

                txtUkupno.Text = Math.Round(VPC - nabavna_ukupno, 2).ToString("#0.00");
                txtIznosPDV.Text = Math.Round(VPC * (pdv / 100) * kol, 2).ToString("#0.00");
                txtIznosMPC.Text = Math.Round(MPC * kol, 2).ToString("#0.00");

                txtMarza.Text = ((VPC / nabavna_ukupno - 1) * 100).ToString("#0.0000");

                txtMPC.Text = Math.Round(MPC, 2).ToString("#0.00");

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

            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[1].Value = txtSifra_robe.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[2].Value = txtNazivRobe.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[3].Value = txtKolicina.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[4].Value = txtFakCijena.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[5].Value = Math.Round(nabavna_ukupno, 3).ToString("#0.000");
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[6].Value = txtMarza.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[7].Value = txtVPC.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[8].Value = txtMPC.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[9].Value = txtRabat.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[10].Value = txtPrijevoz.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[11].Value = txtCarina.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[12].Value = txtPosebniPorez.Text;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[13].Value = txtPDV.SelectedValue;
            dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["povrNaknada"].Value = txtPovrNaknada.Text;
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
                "kalkulacija_stavke.sifra,kalkulacija_stavke.id_stavka " +
                " FROM kalkulacija_stavke " +
                " LEFT JOIN kalkulacija ON kalkulacija_stavke.broj=kalkulacija.broj AND kalkulacija_stavke.id_skladiste=kalkulacija.id_skladiste " +
                " WHERE kalkulacija_stavke.broj='" + id + "' AND kalkulacija.id_skladiste='" + skl + "'" +
                " ORDER BY kalkulacija_stavke.id_stavka";
            DTstavke = classSQL.select(sql, "kalkulacija_stavke").Tables[0];

            //kalkulacija

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

            double kol, fakCijena, nabavna, VPC, marza, pdv, MPC;
            for (int i = 0; i < DTstavke.Rows.Count; i++)
            {
                txtSifra_robe.Text = DTstavke.Rows[i]["sifra"].ToString();
                string s = DTstavke.Rows[i]["sifra"].ToString();
                txtNazivRobe.Text = classSQL.select("select naziv from roba WHERE sifra='" + DTstavke.Rows[i]["sifra"].ToString() + "'", "roba").Tables[0].Rows[0][0].ToString();

                kol = Convert.ToDouble(DTstavke.Rows[i]["kolicina"].ToString());
                txtKolicina.Text = Math.Round(kol, 3).ToString("#0.000");

                txtRabat.Text = DTstavke.Rows[i]["rabat"].ToString();

                fakCijena = Math.Round(Convert.ToDouble(DTstavke.Rows[i]["fak_cijena"].ToString()), 5);
                txtFakCijena.Text = Math.Round(fakCijena, 5).ToString("#0.0000");

                txtPrijevoz.Text = DTstavke.Rows[i]["prijevoz"].ToString();
                txtCarina.Text = DTstavke.Rows[i]["carina"].ToString();
                txtPosebniPorez.Text = DTstavke.Rows[i]["posebni_porez"].ToString();

                pdv = Convert.ToDouble(DTstavke.Rows[i]["porez"].ToString());

                marza = Math.Round(Convert.ToDouble(DTstavke.Rows[i]["marza_postotak"].ToString()), 4);
                txtMarza.Text = marza.ToString("#0.0000");

                VPC = Math.Round(Convert.ToDouble(DTstavke.Rows[i]["vpc"].ToString()), 3);
                txtVPC.Text = VPC.ToString("#0.000");

                MPC = Math.Round(((VPC * pdv / 100) + VPC), 2);
                txtMPC.Text = MPC.ToString("#0.00");
                //kol = Convert.ToDouble(txtKolicina.Text);

                nabavna = ((Convert.ToDouble(txtFakCijena.Text) * Convert.ToDouble(txtValutaValuta.Text)) -
                    (Convert.ToDouble(txtFakCijena.Text) * Convert.ToDouble(txtRabat.Text) / 100 * Convert.ToDouble(txtValutaValuta.Text))) +
                    Convert.ToDouble(txtPrijevoz.Text) +
                    Convert.ToDouble(txtCarina.Text) +
                    Convert.ToDouble(txtPosebniPorez.Text) * kol;

                setPovratnaNaknada(s);

                dataGridView1.Rows.Add(i + 1,
                    txtSifra_robe.Text,
                    txtNazivRobe.Text,
                    kol.ToString("#0.000"),
                    fakCijena.ToString("#0.00"),
                    Math.Round(nabavna, 3).ToString("#0.000"),
                    marza.ToString("#0.0000"),
                    VPC.ToString("#0.000"),
                    MPC.ToString("#0.00"),
                    txtRabat.Text,
                    txtPrijevoz.Text,
                    txtCarina.Text,
                    txtPosebniPorez.Text, DTstavke.Rows[i]["porez"].ToString(),
                    DTstavke.Rows[i]["id_stavka"].ToString(),
                    txtPovrNaknada.Text
                    );
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected = true;

                //setPovratnaNaknada(s);//Math.Round(nabavna * kol, 2).ToString("#0.00")

                //dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["povrNaknada"].Value = txtPovrNaknada.Text;

                txtPDV.SelectedValue = DTstavke.Rows[i]["porez"].ToString();
                SrediValutuPoFakCijeni();
            }

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
            }

            ControlDisableEnable(false, true, true, false, true);
            //PaintRows(dataGridView1);
        }

        //-----------------------------------------------------------UPDATE----------------------------------------------------------

        private void update_kalkulacija()
        {
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
                //fak_cijena = Convert.ToDouble(dataGridView1.Rows[i].Cells["fak_cijena"].Value) + fak_cijena;
                //fak_cijena_Rabat = (fak_cijena * rabat / 100) + fak_cijena_Rabat;
                fak_cijena += (Convert.ToDouble(dataGridView1.Rows[i].Cells["nab_cijena"].FormattedValue.ToString()) * Convert.ToDouble(dataGridView1.Rows[i].Cells["kolicina"].FormattedValue.ToString()));
            }

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
            string sql_update_kalkulacija = "UPDATE kalkulacija SET " +
                " broj ='" + txtBrojKalkulacije.Text + "'," +
                " id_partner='" + txtSifraDobavljac.Text + "'," +
                " racun='" + txtBrojRac.Text + "'," +
                " racun_datum='" + dtpRacun.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " otpremnica_datum='" + dtpOtpremnica.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " mjesto_troska='" + txtMjestoTroska.Text + "'," +
                " datum='" + dtpDatumNow.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " godina='" + nuGodinaKalk.Text + "'," +
                " ukupno_vpc='" + vpc_ukupno + "'," +
                " ukupno_mpc='" + mpc_ukupno + "'," +
                " tecaj='" + txtValutaValuta.Text + "'," +
                " id_valuta='" + cbValuta.SelectedValue + "'," +
                " fakturni_iznos='" + fak_ukupno + "'," +
                " id_skladiste='" + CBskladiste.SelectedValue + "'" +
                " WHERE broj ='" + broj_kalkulacije_edit + "' AND id_skladiste='" + edit_skladiste + "'";

            provjera_sql(classSQL.transaction(sql_update_kalkulacija));

            //Update kalkulacija_stavke
            string kol;
            for (int br = 0; br < dataGridView1.Rows.Count; br++)
            {
                string id_stavka = dataGridView1.Rows[br].Cells["id_stavka"].FormattedValue.ToString();
                if (id_stavka == "")
                {
                    //INSERT
                    kol = SQL.ClassSkladiste.GetAmount(dg(br, "sifra"), CBskladiste.SelectedValue.ToString(), dg(br, "kolicina"), "1", "+");
                    //SQL.SQLroba_prodaja.UpdateRows(CBskladiste.SelectedValue.ToString(), kol, dg(br, "sifra"));

                    string updt = "UPDATE roba_prodaja SET kolicina='" + kol.ToString() +
                        "' WHERE id_skladiste='" + CBskladiste.SelectedValue.ToString() +
                        "' AND sifra='" + dg(br, "sifra") + "';";
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
                        /*SQL.SQLroba_prodaja.UpdateRows(
                            dataROW[0]["id_skladiste"].ToString(),
                            kol,
                            dg(br, "sifra"));*/

                        string updt = "UPDATE roba_prodaja SET kolicina='" + kol.ToString() +
                        "' WHERE id_skladiste='" + dataROW[0]["id_skladiste"].ToString() +
                        "' AND sifra='" + dg(br, "sifra") + "';";
                        classSQL.update(updt);

                        //oduzima sa novog skladiste
                        kol = SQL.ClassSkladiste.GetAmount(
                            dg(br, "sifra"),
                            CBskladiste.SelectedValue.ToString(),
                            dg(br, "kolicina"),
                            "1",
                            "+");
                        /*SQL.SQLroba_prodaja.UpdateRows(
                            CBskladiste.SelectedValue.ToString(),
                            kol,
                            dg(br, "sifra"));*/

                        updt = "UPDATE roba_prodaja SET kolicina='" + kol.ToString() +
                        "' WHERE id_skladiste='" + CBskladiste.SelectedValue.ToString() +
                        "' AND sifra='" + dg(br, "sifra") + "';";
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
                        /*SQL.SQLroba_prodaja.UpdateRows(
                            CBskladiste.SelectedValue.ToString(),
                            kol,
                            dg(br, "sifra"));*/

                        string updt = "UPDATE roba_prodaja SET kolicina='" + kol.ToString() +
                        "' WHERE id_skladiste='" + CBskladiste.SelectedValue.ToString() +
                        "' AND sifra='" + dg(br, "sifra") + "';";
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

                classSQL.transaction("UPDATE roba SET nc='" + nbc + "',porez='" + dataGridView1.Rows[br].Cells["pdv"].FormattedValue.ToString() + "',mpc='" + mpc + "',vpc='" + vpc + "' WHERE sifra='" + dataGridView1.Rows[br].Cells["sifra"].FormattedValue.ToString() + "'");
                classSQL.transaction("UPDATE roba_prodaja SET nc='" + nbc + "',porez='" + dataGridView1.Rows[br].Cells["pdv"].FormattedValue.ToString() + "',vpc='" + vpc + "' WHERE sifra='" + dataGridView1.Rows[br].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + CBskladiste.SelectedValue + "'");

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
        }

        private void updatePovratnaNaknada(string sifra, string povrNaknada)
        {
            string sql = "SELECT id FROM povratna_naknada" +
                " LEFT JOIN roba ON povratna_naknada.sifra=roba.sifra WHERE roba.sifra='" + sifra + "'";
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
            double fak_cijena = 0;
            double fak_cijena_Rabat = 0;
            decimal proba = 0;

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
                }

                try
                {
                    dataGridView2.Rows[0].Cells[1].Value = Math.Round(nabavni_valuta / Convert.ToDouble(txtValutaValuta.Text), 3).ToString("#0.00");
                }
                catch
                {
                    dataGridView2.Rows[0].Cells[1].Value = Math.Round(nabavni_valuta, 3).ToString("#0.00");
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
                cbValuta.Enabled = dataGridView1.Rows.Count == 0 ? false : true;
                txtValutaValuta.Enabled = dataGridView1.Rows.Count == 0 ? false : true;
                txtSifra_robe.Select();

                if (dataGridView1.RowCount > 0)
                {
                    txtValutaValuta.Text = GLmarza.ToString();
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

            izracun();

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
            Decimal vr = Convert.ToDecimal(txtFakCijenaVal.Text);
            txtFakCijenaVal.Text = ((vr * Convert.ToDecimal(txtPDV.SelectedValue) / 100) + vr).ToString();
            txtFakCijena.Select();
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
        }
    }
}