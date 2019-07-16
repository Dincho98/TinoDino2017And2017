using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmFakturaBezRobe : Form
    {
        public string broj_fakture_edit { get; set; }

        public frmFakturaBezRobe()
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
        private DataTable DSfakture = new DataTable();
        private DataTable DSFS = new DataTable();
        private DataTable DTOtprema = new DataTable();
        private double u = 0;
        private bool edit = false;
        private double SveUkupno = 0;
        public frmMenu MainForm { get; set; }
        public string[] otpremnica = new string[0];

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

        private void frmFaktura_Load(object sender, EventArgs e)
        {
            lblNaDan.Text = "";

            this.Paint += new PaintEventHandler(Form1_Paint);

            ProvjeraBaze();
            MyDataGrid.MainForm = this;
            numeric();
            fillComboBox();
            ttxBrojFakture.Text = brojFakture();
            EnableDisable(false);
            ControlDisableEnable(1, 0, 0, 1, 0);
            if (broj_fakture_edit != null) { fillFakture(); }
            btnNoviUnos.Select();
        }

        private class MyDataGrid : DataGridView
        {
            public static frmFakturaBezRobe MainForm { get; set; }

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
                else if (keyData == (Keys.Control | Keys.C))
                {
                    Clipboard.SetText(MainForm.dgw.CurrentCell.FormattedValue.ToString());
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
                else if (keyData == Keys.Insert)
                {
                    //MainForm.dgw.Rows.Add("", "", "", "1", "25", "0", "0", "0", "0", "0", "0");
                    if (Class.Postavke.sustavPdv)
                    {
                        MainForm.dgw.Rows.Add("", "", "", "1", "25", "0", "0", "0", "0", "0", "0");
                    }
                    else
                    {
                        MainForm.dgw.Rows.Add("", "", "", "1", "0", "0", "0", "0", "0", "0", "0");
                    }
                    MainForm.RedniBroj();

                    return true;
                }
                else if (keyData == Keys.Delete)
                {
                    MainForm.dgw.Rows.RemoveAt(MainForm.dgw.CurrentRow.Index);
                    MainForm.RedniBroj();
                    return true;
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void EnterDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            if (d.CurrentCell.ColumnIndex == 1)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[2];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 2)
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
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[8];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 8)
            {
                //dgw.Rows.Add("", "", "", "1", "25", "0", "0", "0", "0", "0", "0");
                if (Class.Postavke.sustavPdv)
                {
                    dgw.Rows.Add("", "", "", "1", "25", "0", "0", "0", "0", "0", "0");
                }
                else
                {
                    dgw.Rows.Add("", "", "", "1", "0", "0", "0", "0", "0", "0", "0");
                }
                RedniBroj();
                d.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[1];
                d.BeginEdit(true);
            }
        }

        private void LeftDGW(DataGridView d)
        {
            if (d.Rows.Count < 1) { return; }

            if (d.CurrentCell.ColumnIndex == 2)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[1];
                d.BeginEdit(true);
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
            else if (d.CurrentCell.ColumnIndex == 6)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 7)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[6];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 8)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 9)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[8];
                d.BeginEdit(true);
            }
        }

        private void RightDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            if (d.CurrentCell.ColumnIndex == 1)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[2];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 2)
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
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[8];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 8)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[1];
                d.BeginEdit(true);
            }
        }

        private void UpDGW(DataGridView d)
        {
            if (d.Rows.Count < 1) { return; }

            int curent = d.CurrentRow.Index;
            if (curent == 0)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[1];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index - 1].Cells[1];
                d.BeginEdit(true);
            }
        }

        private void DownDGW(DataGridView d)
        {
            if (d.Rows.Count < 1) { return; }

            int curent = d.CurrentRow.Index;
            if (curent == d.RowCount - 1)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[1];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index + 1].Cells[1];
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

            //fill nacin_placanja
            DSnazivPlacanja = classSQL.select("SELECT * FROM nacin_placanja", "nacin_placanja");
            cbNacinPlacanja.DataSource = DSnazivPlacanja.Tables[0];
            cbNacinPlacanja.DisplayMember = "naziv_placanja";
            cbNacinPlacanja.ValueMember = "id_placanje";
            cbNacinPlacanja.SelectedValue = 3;

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

            //fill tko je prijavljen
            txtIzradio.Text = classSQL.select(string.Format("SELECT ime + ' ' + prezime as Ime  FROM zaposlenici WHERE id_zaposlenik = '{0}';", Properties.Settings.Default.id_zaposlenik), "zaposlenici").Tables[0].Rows[0][0].ToString();
        }

        private void numeric()
        {
            nmGodinaFakture.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nmGodinaFakture.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nmGodinaFakture.Value = Convert.ToInt16(DateTime.Now.Year.ToString());
        }

        private void izracun()
        {
            if (dgw.RowCount > 0)
            {
                int rowBR = dgw.CurrentRow.Index;

                double dec_parse;
                if (!Double.TryParse(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString(), out dec_parse))
                {
                    dgw.Rows[rowBR].Cells["kolicina"].Value = "1";
                    MessageBox.Show("Greška kod upisa količine.", "Greška"); return;
                }

                if (!Double.TryParse(dgw.Rows[rowBR].Cells["rabat"].FormattedValue.ToString(), out dec_parse))
                {
                    dgw.Rows[rowBR].Cells["rabat"].Value = "0";
                    MessageBox.Show("Greška kod upisa rabata.", "Greška"); return;
                }

                if (!Double.TryParse(dgw.Rows[rowBR].Cells["rabat_iznos"].FormattedValue.ToString(), out dec_parse))
                {
                    dgw.Rows[rowBR].Cells["rabat_iznos"].Value = "0,00";
                    MessageBox.Show("Greška kod rabata.", "Greška"); return;
                }

                if (!Double.TryParse(dgw.Rows[rowBR].Cells["mpc"].FormattedValue.ToString(), out dec_parse))
                {
                    dgw.Rows[rowBR].Cells["mpc"].Value = "0,00";
                    MessageBox.Show("Greška kod cijene.", "Greška"); return;
                }

                if (!Double.TryParse(dgw.Rows[rowBR].Cells["porez"].FormattedValue.ToString(), out dec_parse))
                {
                    dgw.Rows[rowBR].Cells["porez"].Value = "0,00";
                    MessageBox.Show("Greška kod poreza.", "Greška"); return;
                }

                double kol = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString()), 3);
                double porez = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["porez"].FormattedValue.ToString()), 2);
                double rbt = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["rabat"].FormattedValue.ToString()), 2);
                double mpc = Math.Round(Convert.ToDouble(dgw.Rows[rowBR].Cells["mpc"].FormattedValue.ToString()), 3);

                double PreracunataStopaPDV = 100 * porez / (100 + porez);
                double porez_ukupno = mpc * PreracunataStopaPDV / 100;

                double vpc = mpc - porez_ukupno;
                double mpc_sa_kolicinom = mpc * kol;
                double rabat = mpc * rbt / 100;
                double iznos;

                dgw.Rows[rowBR].Cells["vpc"].Value = Math.Round(vpc, 3).ToString("#0.000");
                dgw.Rows[rowBR].Cells["rabat_iznos"].Value = Math.Round(rabat * kol, 2).ToString("#0.00");
                dgw.Rows[rowBR].Cells["iznos_bez_pdva"].Value = Math.Round((mpc - rabat) * kol / (1 + porez / 100), 2).ToString("#0.00");
                dgw.Rows[rowBR].Cells["iznos_ukupno"].Value = Math.Round((mpc - rabat) * kol, 2).ToString("#0.00");
                dgw.Rows[rowBR].Cells["cijena_bez_pdva"].Value = Math.Round((mpc - rabat) / (1 + porez / 100), 2).ToString("#0.00");
                dgw.Rows[rowBR].Cells["kolicina"].Value = Math.Round(kol, 3).ToString("#0.000");
                dgw.Rows[rowBR].Cells["rabat"].Value = Math.Round(rbt, 2).ToString("#0.00");

                double B_pdv = 0;
                u = 0;

                for (int i = 0; i < dgw.RowCount; i++)
                {
                    iznos = Convert.ToDouble(dgw.Rows[i].Cells["iznos_ukupno"].FormattedValue.ToString());
                    u += Math.Round(iznos, 2);
                    if (!double.TryParse(dgw.Rows[i].Cells["iznos_bez_pdva"].FormattedValue.ToString(), out iznos)) { iznos = 0; }
                    B_pdv += Math.Round(iznos, 2);
                }

                SveUkupno = u;
                txtUkupnoIznos.Text = "Ukupno sa PDV-om: " + Math.Round(u, 2).ToString("#0.00");
                txtOsnovicaIznosUkupno.Text = "Bez PDV-a: " + Math.Round(B_pdv, 2).ToString("#0.00");
                txtPdvIznosUkupno.Text = "PDV: " + Math.Round(Math.Round(u, 2) - Math.Round(B_pdv, 2), 2).ToString("#0.00");

                txtSifra_robe.Text = "";
            }
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
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void btnDeleteAllFaktura_Click(object sender, EventArgs e)
        {
            classSQL.delete("DELETE FROM ifb_stavke WHERE broj='" + ttxBrojFakture.Text + "'");
            classSQL.delete("DELETE FROM ifb WHERE broj='" + ttxBrojFakture.Text + "'");
            MessageBox.Show("Obrisano.");

            ttxBrojFakture.ReadOnly = false;
            nmGodinaFakture.ReadOnly = false;
            edit = false;
            EnableDisable(false);
            deleteFields();
            ControlDisableEnable(1, 0, 0, 1, 0);
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

            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private void EnableDisable(bool x)
        {
            txtSifraOdrediste.Enabled = x;
            txtPartnerNaziv.Enabled = x;
            txtModel.Enabled = x;
            txtOtprema.Enabled = x;
            rtbNapomena.Enabled = x;
            btnPartner.Enabled = x;
            dtpDatum.Enabled = x;
            dtpDatumDVO.Enabled = x;
            dtpDanaValuta.Enabled = x;
            cbNacinPlacanja.Enabled = x;
            cbValuta.Enabled = x;

            txtDana.Enabled = x;
            txtMjestoTroska.Enabled = x;
            txtIznosPredujma.Enabled = x;

            lblInsertNovaStavka.Enabled = x;
            lblDeleteStavka.Enabled = x;
            txtSifra_robe.Enabled = x;
            btnOpenRoba.Enabled = x;

            txtUkupnoIznos.Enabled = x;
            txtOsnovicaIznosUkupno.Enabled = x;
            txtPdvIznosUkupno.Enabled = x;
        }

        private void deleteFields()
        {
            ttxBrojFakture.Text = "";
            nmGodinaFakture.Value = Util.Korisno.GodinaKojaSeKoristiUbazi;
            txtOtpremnica.Text = "";

            txtSifraOdrediste.Text = "";
            txtPartnerNaziv.Text = "";
            dtpDatum.Value = DateTime.Now;
            dtpDatumDVO.Value = dtpDatum.Value;
            dtpDanaValuta.Value = dtpDatum.Value;
            txtDana.Text = "0";
            cbValuta.SelectedValue = 5;

            txtModel.Text = "";
            txtMjestoTroska.Text = "";
            cbNacinPlacanja.SelectedValue = 3;
            txtOtprema.Text = "";
            txtIznosPredujma.Text = "0";
            rtbNapomena.Text = "";

            txtSifra_robe.Text = "";

            txtPdvIznosUkupno.Text = "PDV:";
            txtOsnovicaIznosUkupno.Text = "Bez PDV-a:";
            txtUkupnoIznos.Text = "Ukupno sa PDV-om:";

            dgw.Rows.Clear();
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            NoviUnos();
        }

        private void NoviUnos()
        {
            edit = false;
            EnableDisable(true);
            ttxBrojFakture.Text = brojFakture();
            ControlDisableEnable(0, 1, 1, 0, 1);
            txtSifraOdrediste.Select();
        }

        private string brojFakture()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj) FROM ifb", "ifb").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void btnSpremi_Click_1(object sender, EventArgs e)
        {
            try
            {
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

                if (edit == true)
                {
                    UpdateFaktura();
                    if (MessageBox.Show("Spremljeno.\r\nŽelite li ispisati dokument?", "Spremljeno.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        printaj(ttxBrojFakture.Text);
                    }
                    EnableDisable(false);
                    deleteFields();
                    btnSveFakture.Enabled = true;
                    ControlDisableEnable(1, 0, 0, 1, 0);
                    return;
                }

                string uk = u.ToString();
                if (classSQL.remoteConnectionString == "")
                {
                    uk = uk.Replace(",", ".");
                }
                else
                {
                    uk = uk.Replace(",", ".");
                }

                string sql = string.Format(@"INSERT INTO ifb (broj, datum, datum_valute, datum_dvo, id_zaposlenik, model, id_nacin_placanja, valuta, id_valuta, otprema, napomena, godina, ukupno, mj_troska, odrediste)
VALUES
(
    '{0}', '{1}', '{2}', '{3}', '{4}',
    '{5}', '{6}', '{7}', '{8}', '{9}',
    '{10}', '{11}', '{12}', '{13}', '{14}'
);",
ttxBrojFakture.Text, dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss"), dtpDanaValuta.Value.ToString("yyyy-MM-dd H:mm:ss"), dtpDatumDVO.Value.ToString("yyyy-MM-dd H:mm:ss"), Properties.Settings.Default.id_zaposlenik,
txtModel.Text, cbNacinPlacanja.SelectedValue.ToString(), txtTecaj.Text.Replace(',', '.'), cbValuta.SelectedValue.ToString(), txtOtprema.Text,
rtbNapomena.Text, nmGodinaFakture.Value.ToString(), uk, txtMjestoTroska.Text, txtSifraOdrediste.Text);
                provjera_sql(classSQL.insert(sql));

                DataSet DSkolicina = new DataSet();

                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    if (dgw.Rows[i].Cells["naziv"].FormattedValue.ToString() != "")
                    {
                        sql = string.Format(@"INSERT INTO ifb_stavke (kolicina, vpc, mpc, porez, rabat, jmj, broj, naziv)
VALUES (
    '{0}', '{1}', '{2}', '{3}',
    '{4}', '{5}', '{6}', '{7}'
);",
dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString().Replace(",", "."),
dgw.Rows[i].Cells["vpc"].FormattedValue.ToString().Replace(",", "."),
dgw.Rows[i].Cells["mpc"].FormattedValue.ToString().Replace(",", "."),
dgw.Rows[i].Cells["porez"].FormattedValue.ToString().Replace(",", "."),
dgw.Rows[i].Cells["rabat"].FormattedValue.ToString().Replace(",", "."),
dgw.Rows[i].Cells["jmj"].FormattedValue.ToString(),
ttxBrojFakture.Text,
dgw.Rows[i].Cells["naziv"].FormattedValue.ToString());

                        provjera_sql(classSQL.insert(sql));
                    }
                }

                provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Nova faktura br." + ttxBrojFakture.Text + "')"));

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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška kod spremanja fakture!\n\n" + ex.ToString());
            }
        }

        private void UpdateFaktura()
        {
            string sql = string.Format(@"UPDATE ifb
SET
broj = '{0}', datum = '{1}', datum_dvo = '{2}', datum_valute = '{3}', id_zaposlenik = '{4}',
model = '{5}', id_nacin_placanja = '{6}', valuta = '{7}', id_valuta = '{8}', otprema = '{9}',
napomena = '{10}', godina = '{11}', ukupno = '{12}', mj_troska = '{13}',  odrediste = '{14}'
WHERE broj = '{15}';",
                ttxBrojFakture.Text, dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss"),
                dtpDatumDVO.Value.ToString("yyyy-MM-dd H:mm:ss"),
                dtpDanaValuta.Value.ToString("yyyy-MM-dd H:mm:ss"),
                Properties.Settings.Default.id_zaposlenik,
                txtModel.Text,
                cbNacinPlacanja.SelectedValue.ToString(),
                txtTecaj.Text.Replace(",", "."),
                cbValuta.SelectedValue.ToString(),
                txtOtprema.Text,
                rtbNapomena.Text,
                nmGodinaFakture.Value.ToString(),
                u.ToString("#0.00").Replace(",", "."),
                txtMjestoTroska.Text,
                txtSifraOdrediste.Text,
                ttxBrojFakture.Text);

            provjera_sql(classSQL.update(sql));

            DataSet DSkolicina = new DataSet();

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                if (dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString() != "")
                {
                    sql = string.Format(@"UPDATE ifb_stavke SET
kolicina = '{0}', vpc = '{1}', mpc = '{2}', porez = '{3}',
rabat = '{4}', jmj = '{5}', naziv = '{6}'
WHERE id_stavka = '{7}';",
dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString().Replace(",", "."),
dgw.Rows[i].Cells["vpc"].FormattedValue.ToString().Replace(",", "."),
dgw.Rows[i].Cells["mpc"].FormattedValue.ToString().Replace(",", "."),
dgw.Rows[i].Cells["porez"].FormattedValue.ToString().Replace(",", "."),
dgw.Rows[i].Cells["rabat"].FormattedValue.ToString().Replace(",", "."),
dgw.Rows[i].Cells["jmj"].FormattedValue.ToString(),
dgw.Rows[i].Cells["naziv"].FormattedValue.ToString(),
dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString());

                    provjera_sql(classSQL.update(sql));
                }
                else
                {
                    sql = string.Format(@"INSERT INTO ifb_stavke (kolicina, vpc, mpc, porez, rabat, jmj, broj, naziv)
VALUES (
    '{0}', '{1}', '{2}', '{3}',
    '{4}', '{5}', '{6}', '{7}'
);",
                        dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString().Replace(",", "."),
                        dgw.Rows[i].Cells["vpc"].FormattedValue.ToString().Replace(",", "."),
                        dgw.Rows[i].Cells["mpc"].FormattedValue.ToString().Replace(",", "."),
                        dgw.Rows[i].Cells["porez"].FormattedValue.ToString().Replace(",", "."),
                        dgw.Rows[i].Cells["rabat"].FormattedValue.ToString().Replace(",", "."),
                        dgw.Rows[i].Cells["jmj"].FormattedValue.ToString(),
                        ttxBrojFakture.Text,
                        dgw.Rows[i].Cells["naziv"].FormattedValue.ToString());

                    provjera_sql(classSQL.insert(sql));
                }
            }

            provjera_sql(classSQL.insert(string.Format("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('{0}', '{1}', 'Nova faktura br. {2}');",
                Properties.Settings.Default.id_zaposlenik,
                DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"),
                ttxBrojFakture.Text)));

            ttxBrojFakture.ReadOnly = false;
            nmGodinaFakture.ReadOnly = false;
            edit = false;
        }

        private void printaj(string broj)
        {
            Report.IFB.repFakturaIFB pr = new Report.IFB.repFakturaIFB();
            pr.dokumenat = "IFB";
            pr.broj_dokumenta = broj;
            pr.ImeForme = "Faktura";
            pr.ShowDialog();
        }

        private string dg(int row, string cell)
        {
            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
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
            frmSveFaktureBezRobe objForm2 = new frmSveFaktureBezRobe();
            objForm2.sifra_fakture = "";
            objForm2.MainForm = this;
            objForm2.ShowDialog();
            if (broj_fakture_edit != null)
            {
                deleteFields();
                fillFakture();
            }
        }

        private void fillFakture()
        {
            //fill header

            EnableDisable(true);
            edit = true;
            ControlDisableEnable(0, 1, 1, 0, 1);

            DSfakture = classSQL.select(string.Format("SELECT * FROM ifb WHERE broj = '{0}';", broj_fakture_edit), "fakture").Tables[0];

            if (DSfakture.Rows.Count > 0)
            {
                txtSifraOdrediste.Text = DSfakture.Rows[0]["odrediste"].ToString();
                txtPartnerNaziv.Text = classSQL.select(string.Format("SELECT ime_tvrtke FROM partners WHERE id_partner='{0}';", DSfakture.Rows[0]["odrediste"].ToString()), "partners").Tables[0].Rows[0][0].ToString();
                txtModel.Text = DSfakture.Rows[0]["model"].ToString();
                txtOtprema.Text = DSfakture.Rows[0]["otprema"].ToString();
                txtMjestoTroska.Text = DSfakture.Rows[0]["mj_troska"].ToString();
                rtbNapomena.Text = DSfakture.Rows[0]["napomena"].ToString();
                dtpDatum.Value = Convert.ToDateTime(DSfakture.Rows[0]["datum"].ToString());
                dtpDatumDVO.Value = Convert.ToDateTime(DSfakture.Rows[0]["datum_dvo"].ToString());
                dtpDanaValuta.Value = Convert.ToDateTime(DSfakture.Rows[0]["datum_valute"].ToString());
                cbNacinPlacanja.SelectedValue = DSfakture.Rows[0]["id_nacin_placanja"].ToString();
                cbValuta.SelectedValue = DSfakture.Rows[0]["id_valuta"].ToString();
                nmGodinaFakture.Value = Convert.ToInt16(DSfakture.Rows[0]["godina"].ToString());
                txtIzradio.Text = classSQL.select(string.Format("SELECT ime + ' ' + prezime as Ime FROM zaposlenici WHERE id_zaposlenik = '{0}';", DSfakture.Rows[0]["id_zaposlenik"].ToString()), "zaposlenici").Tables[0].Rows[0][0].ToString();
                ttxBrojFakture.Text = DSfakture.Rows[0]["broj"].ToString();
                string[] otpremnica = new string[3] { DSfakture.Rows[0]["otpremnica_broj"].ToString(), DSfakture.Rows[0]["otpremnica_godina"].ToString(), DSfakture.Rows[0]["otpremnica_id_skladiste"].ToString() };
                txtOtpremnica.Tag = otpremnica;
                txtOtpremnica.Text = (otpremnica[0] != "0" ? otpremnica[0] : "");
            }
            else
            {
                return;
            }

            //--------fill faktura stavke------------------------------

            DSFS = classSQL.select(string.Format("SELECT * FROM ifb_stavke WHERE broj = '{0}';", DSfakture.Rows[0]["broj"].ToString()), "ifb_stavke").Tables[0];

            for (int i = 0; i < DSFS.Rows.Count; i++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;

                dgw.Rows[br].Cells[0].Value = i + 1;
                dgw.Rows[br].Cells["naziv"].Value = DSFS.Rows[i]["naziv"].ToString();
                dgw.Rows[br].Cells["jmj"].Value = DSFS.Rows[i]["jmj"].ToString();
                dgw.Rows[br].Cells["kolicina"].Value = DSFS.Rows[i]["kolicina"].ToString();
                dgw.Rows[br].Cells["porez"].Value = DSFS.Rows[i]["porez"].ToString();
                dgw.Rows[br].Cells["mpc"].Value = DSFS.Rows[i]["mpc"].ToString();
                dgw.Rows[br].Cells["rabat"].Value = DSFS.Rows[i]["rabat"].ToString();
                dgw.Rows[br].Cells["rabat_iznos"].Value = "0,00";
                dgw.Rows[br].Cells["iznos_bez_pdva"].Value = "0,00";
                dgw.Rows[br].Cells["iznos_ukupno"].Value = "0,00";
                dgw.Rows[br].Cells["vpc"].Value = "0,00";
                dgw.Rows[br].Cells["id_stavka"].Value = DSFS.Rows[i]["id_stavka"].ToString();
                dgw.Select();
                dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[3];
                izracun();
            }

            dgw.Columns["iznos_ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos_bez_pdva"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["rabat_iznos"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["rabat_iznos"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos_bez_pdva"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos_ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Format = "N2";
            dgw.Columns["iznos_bez_pdva"].DefaultCellStyle = style;
            dgw.Columns["rabat_iznos"].DefaultCellStyle = style;
            dgw.Columns["iznos_ukupno"].DefaultCellStyle = style;
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
            sp.ShowDialog();
        }

        private void ttxBrojFakture_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select(string.Format("SELECT broj FROM ifb WHERE godina = '{0}' AND broj = '{1}';", nmGodinaFakture.Value.ToString(), ttxBrojFakture.Text), "ifb").Tables[0];
                //deleteFields();
                if (DT.Rows.Count == 0)
                {
                    broj_fakture_edit = ttxBrojFakture.Text;
                    deleteFields();
                    ttxBrojFakture.Text = broj_fakture_edit;
                    broj_fakture_edit = "";
                    edit = false;
                    EnableDisable(true);
                    btnSveFakture.Enabled = false;
                    btnDeleteAllFaktura.Enabled = false;
                    txtSifraOdrediste.Select();
                    ttxBrojFakture.ReadOnly = true;
                    nmGodinaFakture.ReadOnly = true;
                }
                else if (DT.Rows.Count == 1)
                {
                    broj_fakture_edit = ttxBrojFakture.Text;
                    deleteFields();
                    fillFakture();
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

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.Rows.Count < 1) { return; }
            dgw.BeginEdit(true);
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

        private void ttxBrojFakture_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Insert)
            {
                if (Class.Postavke.sustavPdv)
                {
                    dgw.Rows.Add("", "", "", "1", "25", "0", "0", "0", "0", "0", "0");
                }
                else
                {
                    dgw.Rows.Add("", "", "", "1", "0", "0", "0", "0", "0", "0", "0");
                }
                RedniBroj();
                dgw.CurrentCell = dgw.Rows[dgw.RowCount - 1].Cells[1];
                dgw.BeginEdit(true);
            }
            else
                if (e.KeyData == Keys.Enter)
            {
                if (sender is TextBox)
                {
                    TextBox txt = ((TextBox)sender);
                    if (txt.Name == "rtbNapomena")
                    {
                        if (rtbNapomena.Text == "")
                        {
                            if (Class.Postavke.sustavPdv)
                            {
                                dgw.Rows.Add("", "", "", "1", "25", "0", "0", "0", "0", "0", "0");
                            }
                            else
                            {
                                dgw.Rows.Add("", "", "", "1", "0", "0", "0", "0", "0", "0", "0");
                            }
                            dgw.Select();
                            int br = dgw.Rows.Count - 1;
                            dgw.CurrentCell = dgw.Rows[br].Cells[1];
                            dgw.BeginEdit(true);

                            RedniBroj();
                            return;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (txt.Name == "txtDana")
                    {
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
                            MessageBox.Show("Krivi upis.");
                        }
                    }
                }

                e.SuppressKeyPress = true;

                if (sender is TextBox)
                {
                    TextBox txt = ((TextBox)sender);
                    if (txt.Name == "txtSifraOdrediste")
                    {
                        if (txtSifraOdrediste.Text == "")
                        {
                            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                            partnerTrazi.ShowDialog();
                            if (Properties.Settings.Default.id_partner != "")
                            {
                                DataSet partner = new DataSet();
                                partner = classSQL.select(string.Format("SELECT id_partner, ime_tvrtke FROM partners WHERE id_partner = '{0}';", Properties.Settings.Default.id_partner), "partners");
                                if (partner.Tables[0].Rows.Count > 0)
                                {
                                    txtSifraOdrediste.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                                    txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                                }
                                else
                                {
                                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                                    txtSifraOdrediste.Select();
                                    return;
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
                        DataTable DSpar = classSQL.select(string.Format("SELECT ime_tvrtke FROM partners WHERE id_partner = '{0}';", txtSifraOdrediste.Text), "partners").Tables[0];
                        if (DSpar.Rows.Count > 0)
                        {
                            txtPartnerNaziv.Text = DSpar.Rows[0][0].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                        }
                    }
                }
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (Class.Postavke.sustavPdv)
            {
                dgw.Rows.Add("", "", "", "1", "25", "0", "0", "0", "0", "0", "0");
            }
            else
            {
                dgw.Rows.Add("", "", "", "1", "0", "0", "0", "0", "0", "0", "0");
            }
            RedniBroj();
            dgw.CurrentCell = dgw.Rows[dgw.RowCount - 1].Cells[1];
            dgw.BeginEdit(true);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            if (dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() != "")
            {
                string sql = string.Format("DELETE FROM ifb_stavke WHERE id_stavka = '{0}';", dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString());
                classSQL.delete(sql);
            }

            dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
            RedniBroj();
        }

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 5)
                {
                    double PDV = Convert.ToDouble(dgw.CurrentRow.Cells["porez"].FormattedValue.ToString());
                    double vpc = Convert.ToDouble(dgw.CurrentRow.Cells["vpc"].FormattedValue.ToString());
                    dgw.CurrentRow.Cells["mpc"].Value = Math.Round((((vpc * PDV) / 100) + vpc), 3).ToString("#0.000");
                }
                izracun();
            }
            catch (Exception)
            { }
        }

        private void RedniBroj()
        {
            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                dgw.Rows[i].Cells["br"].Value = i + 1;
            }
        }

        private void ProvjeraBaze()
        {
            DataTable DTremote = classSQL.select("select table_name from information_schema.tables where TABLE_TYPE <> 'VIEW'", "Table").Tables[0];

            if (DTremote.Rows.Count > 0)
            {
                DataRow[] dataROW = DTremote.Select("table_name = 'ifb'");
                if (dataROW.Length == 0)
                {
                    string sql = @"CREATE TABLE ifb
(
    broj bigint,
    id serial NOT NULL,
    datum timestamp without time zone,
    datum_valute timestamp without time zone,
    datum_dvo timestamp without time zone,
    id_zaposlenik integer,
    model character varying(20),
    valuta numeric,
    id_valuta integer,
    otprema character varying(100),
    napomena text,
    ukupno numeric,
    mj_troska text,
    odrediste integer,
    godina integer,
    jir character varying(100),
    zki character varying(100),
    id_nacin_placanja integer,
    CONSTRAINT ifb_pkey PRIMARY KEY (id)
)";

                    classSQL.select(sql, "tip_sobe");
                }

                DataRow[] dataROW1 = DTremote.Select("table_name = 'ifb_stavke'");
                if (dataROW1.Length == 0)
                {
                    string sql = @"CREATE TABLE ifb_stavke
(
    id_stavka serial NOT NULL,
    kolicina numeric,
    vpc numeric,
    broj bigint,
    mpc numeric,
    porez numeric,
    rabat numeric,
    naziv text,
    jmj character varying(20),
    CONSTRAINT ifb_stavke_pkey PRIMARY KEY (id_stavka )
)";

                    classSQL.select(sql, "tip_sobe");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadPonuda()
        {
            dgw.Rows.Clear();
            dgw.Refresh();

            DataTable DTponuda;
            DataTable DTponudaStavke;
            try
            {
                DTponuda = classSQL.select($"SELECT * FROM ponude WHERE broj_ponude = '{tbBrojPonude.Text}'", "ponude").Tables[0];
                DTponudaStavke = classSQL.select($"SELECT * FROM ponude_stavke WHERE broj_ponude = '{tbBrojPonude.Text}'", "ponude_stavke").Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Neispravan unos! - {ex.Message}", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (DTponuda.Rows.Count > 0&& DTponudaStavke.Rows.Count > 0)
            {
                NoviUnos();

                // Ponuda
                DataRow ponudaRow = DTponuda.Rows[0];
                ttxBrojFakture.Text = brojFakture();
                nmGodinaFakture.Value = Convert.ToInt32(ponudaRow["godina_ponude"].ToString());
                txtSifraOdrediste.Text = ponudaRow["id_odrediste"].ToString();
                txtPartnerNaziv.Text = Global.Database.GetPartners(ponudaRow["id_odrediste"].ToString())?.Rows[0]["ime_tvrtke"].ToString();
                dtpDatum.Value = DateTime.Parse(ponudaRow["date"].ToString());
                dtpDatumDVO.Value = DateTime.Parse(ponudaRow["date"].ToString());
                dtpDanaValuta.Value = DateTime.Parse(ponudaRow["date"].ToString());
                txtDana.Text = "15";
                txtIzradio.Text = Global.Database.GetZaposleniciNaziv(ponudaRow["id_zaposlenik_izradio"].ToString())?.Rows[0]["naziv"].ToString();
                cbValuta.SelectedValue = ponudaRow["id_valuta"].ToString();
                txtTecaj.Text = ponudaRow["tecaj"].ToString();
                txtModel.Text = ponudaRow["model"].ToString() == "''" ? "" : ponudaRow["model"].ToString();
                cbNacinPlacanja.SelectedValue = ponudaRow["id_nacin_placanja"].ToString();
                rtbNapomena.Text = ponudaRow["napomena"].ToString() == "''" ? "" : ponudaRow["napomena"].ToString();

                // Ponuda stavke
                foreach (DataRow stavkaRow in DTponudaStavke.Rows)
                {
                    DataTable DTroba = Global.Database.GetRoba(stavkaRow["sifra"].ToString());

                    int index = dgw.Rows.Add();
                    dgw.CurrentCell = dgw.Rows[index].Cells[0];
                    dgw.Rows[index].Cells["br"].Value = index + 1;
                    dgw.Rows[index].Cells["naziv"].Value = stavkaRow["sifra"].ToString() + " - " + stavkaRow["naziv"].ToString();
                    dgw.Rows[index].Cells["jmj"].Value = DTroba.Rows[0]["jm"].ToString();
                    dgw.Rows[index].Cells["kolicina"].Value = stavkaRow["kolicina"].ToString().Replace('.', ',');
                    dgw.Rows[index].Cells["porez"].Value = stavkaRow["porez"].ToString().Replace('.', ',');
                    dgw.Rows[index].Cells["vpc"].Value = stavkaRow["vpc"].ToString().Replace('.', ',');
                    dgw.Rows[index].Cells["mpc"].Value = DTroba.Rows[0]["mpc"].ToString().Replace('.', ',');
                    dgw.Rows[index].Cells["rabat"].Value = stavkaRow["rabat"].ToString().Replace('.', ',');
                    dgw.Rows[index].Cells["rabat_iznos"].Value = "0,00";
                    dgw.Rows[index].Cells["iznos_ukupno"].Value = "0,00";
                    dgw.Rows[index].Cells["cijena_bez_pdva"].Value = "0,00";
                    dgw.Rows[index].Cells["iznos_bez_pdva"].Value = "0,00";
                    izracun();
                }

            }
            else
                MessageBox.Show($"Ponuda pod brojem {tbBrojPonude.Text} ne postoji!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnOpenRoba_Click(object sender, EventArgs e)
        {
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            roba_trazi.ShowDialog();
            string propertis_sifra = Properties.Settings.Default.id_roba.Trim();
            if (propertis_sifra != "")
            {
                string sql = string.Format("SELECT * FROM roba WHERE sifra = '{0}';", propertis_sifra);

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;

                    decimal pdv = 0;
                    decimal.TryParse(DTRoba.Rows[0]["porez"].ToString().Trim().Replace(".", ","), out pdv);
                    dgw.Rows.Add("", (DTRoba.Rows[0]["sifra"].ToString() + " - " + DTRoba.Rows[0]["naziv"].ToString()), DTRoba.Rows[0]["jm"].ToString(), "1", ((int)pdv).ToString(), DTRoba.Rows[0]["vpc"].ToString(), DTRoba.Rows[0]["mpc"].ToString(), "0", "0", "0", "0");
                    RedniBroj();
                    if (dgw.RowCount > 0)
                    {
                        dgw.CurrentCell = dgw.Rows[dgw.RowCount - 1].Cells[3];
                        dgw.BeginEdit(true);
                    }

                    izracun();
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtSifra_robe_KeyDown(object sender, KeyEventArgs e)
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

                string sql = string.Format("SELECT * FROM roba WHERE sifra = '{0}';", txtSifra_robe.Text);

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;

                    decimal pdv = 0;
                    decimal.TryParse(DTRoba.Rows[0]["porez"].ToString().Trim().Replace(".", ","), out pdv);
                    dgw.Rows.Add("", (DTRoba.Rows[0]["sifra"].ToString() + " - " + DTRoba.Rows[0]["naziv"].ToString()), DTRoba.Rows[0]["jm"].ToString(), "1", ((int)pdv).ToString(), DTRoba.Rows[0]["vpc"].ToString(), DTRoba.Rows[0]["mpc"].ToString(), "0", "0", "0", "0");
                    RedniBroj();
                    if (dgw.RowCount > 0)
                    {
                        dgw.CurrentCell = dgw.Rows[dgw.RowCount - 1].Cells[3];
                        dgw.BeginEdit(true);
                    }

                    izracun();
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            otpremnica = new string[3] { "0", "0", "0" };
            frmSveOtpremnice objForm2 = new frmSveOtpremnice();
            objForm2.fakturaBezRobe = this;
            objForm2.btnUrediSifru.Text = "Uzmi otpremnicu";
            objForm2.ShowDialog();
            if (otpremnica != null)
            {
                txtOtpremnica.Tag = otpremnica;
                txtOtpremnica.Text = (otpremnica[0] != "0" ? otpremnica[0] : "");
            }
        }

        private void TbBrojPonude_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                LoadPonuda();
        }
    }
}