using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmUnosInventura : Form
    {
        public frmUnosInventura()
        {
            InitializeComponent();
        }

        public string broj_inventure_edit { get; set; }
        private DataTable DTRoba;
        private DataSet DS_Skladiste;
        private DataTable DTIzradio;
        private bool edit = false;
        private bool spremljeno = true;
        public frmMenu MainFormMenu { get; set; }

        private void frmInventura_Load(object sender, EventArgs e)
        {
            /****************************SINKRONIZACIJA SA WEB-OM*****************/
            bgSinkronizacija = new BackgroundWorker();
            bgSinkronizacija.DoWork += new DoWorkEventHandler(bgSinkronizacija_DoWork);
            bgSinkronizacija.WorkerSupportsCancellation = true;
            /****************************SINKRONIZACIJA SA WEB-OM*****************/

            SetCB();
            numeric();
            txtBrojInventure.Text = brojInventure();
            ControlDisableEnable(1, 0, 0, 1, 0);
            if (broj_inventure_edit != null) { fillInventura(); }
            this.Paint += new PaintEventHandler(Form1_Paint);
            EnableDisable(false);
        }

        /****************************SINKRONIZACIJA SA WEB-OM*****************/
        private BackgroundWorker bgSinkronizacija = null;
        private synWeb.synPokretac PokretacSinkronizacije = new synWeb.synPokretac();
        /****************************SINKRONIZACIJA SA WEB-OM*****************/

        private void bgSinkronizacija_DoWork(object sender, DoWorkEventArgs e)
        {
            PokretacSinkronizacije.PokreniSinkronizaciju(true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
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

        private void numeric()
        {
            nmGodinaInventure.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nmGodinaInventure.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nmGodinaInventure.Value = DateTime.Now.Year;
        }

        private void SetCB()
        {
            DTIzradio = classSQL.select(string.Format("SELECT ime + ' ' + prezime as Ime, id_zaposlenik  FROM zaposlenici WHERE id_zaposlenik = '{0}';", Properties.Settings.Default.id_zaposlenik), "zaposlenici").Tables[0];
            txtIzradio.Text = DTIzradio.Rows[0]["Ime"].ToString();
            DS_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste");
            cbSkladiste.DataSource = DS_Skladiste.Tables[0];
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";
        }

        private string brojInventure()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_inventure AS int)) FROM inventura;", "inventura").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 1000);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void btnIzlaz_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSifra_robe_KeyDown(object sender, KeyEventArgs e)
        {
            spremljeno = false;
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

                for (int y = 0; y < dgw.Rows.Count; y++)
                {
                    if (txtSifra_robe.Text == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString().Trim())
                    {
                        MessageBox.Show("Artikl ili usluga već postoje u ovoj inventuri.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgw.CurrentCell = dgw.Rows[y].Cells[4];
                        dgw.BeginEdit(true);
                        return;
                    }
                }

                string sql = string.Format(@"SELECT roba.jm, roba.naziv, roba.oduzmi, roba_prodaja.id_skladiste, roba_prodaja.kolicina, roba_prodaja.vpc, roba_prodaja.porez, roba_prodaja.sifra
                                            FROM roba_prodaja
                                            INNER JOIN roba ON roba_prodaja.sifra=roba.sifra
                                            WHERE (roba_prodaja.sifra = '{0}' OR roba.ean = '{0}') AND roba_prodaja.id_skladiste = '{1}';",
                                            txtSifra_robe.Text,
                                            cbSkladiste.SelectedValue);

                DTRoba = classSQL.select(sql, "roba_prodaja").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    for (int y = 0; y < dgw.Rows.Count; y++)
                    {
                        if (DTRoba.Rows[0]["sifra"].ToString() == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString().Trim())
                        {
                            MessageBox.Show("Artikl ili usluga već postoje u ovoj inventuri.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();
                    dgw.Select();
                    dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[4];
                    dgw.BeginEdit(true);
                    txtBrojInventure.ReadOnly = true;
                    nmGodinaInventure.ReadOnly = true;
                }
                else
                {
                    sql = string.Format(@"SELECT jm, naziv, oduzmi, '0' as kolicina, vpc, porez,sifra
FROM roba
WHERE sifra = '{0}' OR ean = '{0}';", txtSifra_robe.Text);

                    DTRoba = classSQL.select(sql, "roba").Tables[0];
                    if (DTRoba.Rows.Count > 0)
                    {
                        txtSifra_robe.BackColor = Color.White;
                        SetRoba();
                        dgw.Select();
                        dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[4];
                        dgw.BeginEdit(true);
                        txtBrojInventure.ReadOnly = true;
                        nmGodinaInventure.ReadOnly = true;
                    }
                    else
                    {
                        MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga u odabranom skladištu.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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

        private void SetRoba()
        {
            dgw.Rows.Add();
            int br = dgw.Rows.Count - 1;
            decimal _NBC = Util.Korisno.VratiNabavnuCijenu(DTRoba.Rows[0]["sifra"].ToString(), cbSkladiste.SelectedValue.ToString());
            dgw.Rows[br].Cells[0].Value = dgw.RowCount;
            dgw.Rows[br].Cells["sifra"].Value = DTRoba.Rows[0]["sifra"].ToString();
            dgw.Rows[br].Cells["naziv"].Value = DTRoba.Rows[0]["naziv"].ToString();
            dgw.Rows[br].Cells["jmj"].Value = DTRoba.Rows[0]["jm"].ToString();
            dgw.Rows[br].Cells["kolicina"].Value = "1";
            dgw.Rows[br].Cells["vpc"].Value = DTRoba.Rows[0]["vpc"].ToString();
            dgw.Rows[br].Cells["cijena"].Value = string.Format("{0:0.000}", ((Convert.ToDouble(DTRoba.Rows[0]["vpc"].ToString()) * Convert.ToDouble(DTRoba.Rows[0]["porez"].ToString())) / 100) + Convert.ToDouble(DTRoba.Rows[0]["vpc"].ToString()));
            dgw.Rows[br].Cells["KolicinaNaSk"].Value = DTRoba.Rows[0]["kolicina"].ToString();
            dgw.Rows[br].Cells["iznos"].Value = string.Format("{0:0.00}", ((Convert.ToDouble(DTRoba.Rows[0]["vpc"].ToString()) * Convert.ToDouble(DTRoba.Rows[0]["porez"].ToString())) / 100) + Convert.ToDouble(DTRoba.Rows[0]["vpc"].ToString()));
            dgw.Rows[br].Cells["porez"].Value = DTRoba.Rows[0]["porez"].ToString();
            dgw.Rows[br].Cells["nbc"].Value = _NBC.ToString("#0.000");
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Format = "N2";
            dgw.Columns["cijena"].DefaultCellStyle = style;
            dgw.Columns["iznos"].DefaultCellStyle = style;

            dgw.Columns["cijena"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["cijena"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void dgw_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var txtEdit = (TextBox)e.Control;
            txtEdit.KeyDown += EditKeyDown;
        }

        private void EditKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                dgw.CurrentRow.Cells["iznos"].Value = string.Format("{0:0.00}", (Convert.ToDouble(dgw.CurrentRow.Cells["kolicina"].FormattedValue.ToString()) * Convert.ToDouble(dgw.CurrentRow.Cells["cijena"].FormattedValue.ToString())));
                txtSifra_robe.Select();
                base.OnKeyDown(e);
            }
        }

        public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            double result;
            return double.TryParse(val, NumberStyle,
            System.Globalization.CultureInfo.CurrentCulture, out result);
        }

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            spremljeno = false;
            if (dgw.CurrentCell.ColumnIndex == 4 && dgw.CurrentCell.FormattedValue.ToString() != "")
            {
                if (isNumeric(dgw.CurrentCell.FormattedValue.ToString(), System.Globalization.NumberStyles.AllowDecimalPoint))
                {
                    dgw.CurrentRow.Cells["iznos"].Value = string.Format("{0:0.00}", (Convert.ToDouble(dgw.CurrentRow.Cells["kolicina"].FormattedValue.ToString()) * Convert.ToDouble(dgw.CurrentRow.Cells["cijena"].FormattedValue.ToString())));
                    txtSifra_robe.Text = "";
                    txtSifra_robe.Select();
                    dgw.ClearSelection();
                }
                else
                {
                    MessageBox.Show("Greška kod upisa količine.", "Greška");
                    dgw.CurrentCell.Value = 0;
                }
            }
        }

        private void btnOpenRoba_Click(object sender, EventArgs e)
        {
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            roba_trazi.ShowDialog();
            string propertis_sifra = Properties.Settings.Default.id_roba.Trim();

            for (int y = 0; y < dgw.Rows.Count; y++)
            {
                if (propertis_sifra == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString().Trim())
                {
                    MessageBox.Show("Artikl ili usluga već postoje!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            string sql = string.Format(@"SELECT roba.jm, roba.naziv, roba.oduzmi, roba_prodaja.id_skladiste, roba_prodaja.kolicina, roba_prodaja.vpc, roba_prodaja.porez, roba_prodaja.sifra
FROM roba_prodaja
INNER JOIN roba ON roba_prodaja.sifra = roba.sifra
WHERE roba_prodaja.sifra = '{0}' AND roba_prodaja.id_skladiste = '{1}';", propertis_sifra, cbSkladiste.SelectedValue);

            DTRoba = classSQL.select(sql, "roba_prodaja").Tables[0];
            if (DTRoba.Rows.Count > 0)
            {
                txtSifra_robe.BackColor = Color.White;
                SetRoba();
                dgw.Select();
                dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[4];
                dgw.BeginEdit(true);
                txtBrojInventure.ReadOnly = true;
                nmGodinaInventure.ReadOnly = true;
            }
            else
            {
                sql = string.Format(@"SELECT jm, naziv, oduzmi, '0' as kolicina, vpc, porez, sifra
FROM roba
WHERE sifra = '{0}';",
propertis_sifra);

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();
                    dgw.Select();
                    dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[4];
                    dgw.BeginEdit(true);
                    txtBrojInventure.ReadOnly = true;
                    nmGodinaInventure.ReadOnly = true;
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga u odabranom skladištu.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dgw.CurrentRow.Cells["id_stavka"].Value != null)
            {
                classSQL.delete(string.Format("DELETE FROM inventura_stavke WHERE id_stavke = '{0}';", dgw.CurrentRow.Cells["id_stavka"].FormattedValue.ToString()));
                provjera_sql(classSQL.insert(string.Format("INSERT INTO aktivnost_zaposlenici (id_zaposlenik, datum, radnja) VALUES ('{0}', '{1}', 'Brisanje stavke.Inventura br. {2}')", Properties.Settings.Default.id_zaposlenik,
                    DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"),
                    txtBrojInventure.Text)));
            }
            dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.CurrentCell.ColumnIndex == 4)
            {
                dgw.BeginEdit(true);
            }
        }

        private void EnableDisable(bool x)
        {
            if (x == true)
            {
                txtBrojInventure.Enabled = false;
                nmGodinaInventure.Enabled = false;
            }
            else
            {
                txtBrojInventure.Enabled = true;
                nmGodinaInventure.Enabled = true;
            }
            txtSifra_robe.Enabled = x;
            btnUcitajSveStavke.Enabled = x;
            cbSkladiste.Enabled = x;
            dtpDatum.Enabled = x;
            rtbNapomena.Enabled = x;
        }

        private void deleteFields()
        {
            dgw.Rows.Clear();
            nmGodinaInventure.Value = Convert.ToInt16(DateTime.Now.Year.ToString());
            rtbNapomena.Text = "";
        }

        /// <summary>
        /// Ako artikl postoji vrača True a ako ne False
        /// </summary>
        /// <param name="sifra"></param>
        /// <returns></returns>
        private bool ProvijeriDaliPostojiArtiklNaInventuri(string sifra)
        {
            try
            {
                foreach (DataGridViewRow row in dgw.Rows)
                {
                    if (sifra == row.Cells["sifra"].FormattedValue.ToString())
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            string sifra = "";
            string skladiste_ = "";
            DataTable DTsviArtikli = classSQL.select("SELECT * FROM roba;", "roba").Tables[0];
            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                sifra = dg(i, "sifra");
                DataRow[] r = DTsviArtikli.Select("sifra = '" + sifra + "'");
                skladiste_ = cbSkladiste.SelectedValue.ToString();

                if (r.Length == 0)
                {
                    SQL.ClassSkladiste.provjeri_roba_prodaja(sifra, skladiste_);
                }
            }
            if (dgw.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku za spremiti.", "Greška");
                return;
            }

            #region OVAJ DIO DODAJE NA INVENTURU SVE STAVKE KOJE NISU POPISANE

            MessageBox.Show("Ovo može potrajati i do dvije minute.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (MessageBox.Show("Ova funkcija dodaje na vašu inventuru sve artikle koje niste popisali u inventuru.\r\n" +
                "Ako neke artikle niste upisali program smatra da iste nemate na skladištu.\r\n" +
                "Nakon spremanja inventure možete izmjeniti krivo upisane stavke.\r\n\r\n" +
                "Dali ste sigurni da želite pokrenuti ovu funkciju?", "Dodavanje ne upisanih artikala",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    DataTable DTrp = classSQL.select($@"SELECT roba_prodaja.*, 
                        roba.naziv,
                        roba.jm
                    FROM roba_prodaja
                    LEFT JOIN roba ON roba.sifra = roba_prodaja.sifra
                    WHERE id_skladiste = '{cbSkladiste.SelectedValue.ToString()}' AND roba.oduzmi = 'DA';", "roba_prodaja").Tables[0];

                    foreach (DataRow r in DTrp.Rows)
                    {
                        if (!ProvijeriDaliPostojiArtiklNaInventuri(r["sifra"].ToString()))
                        {
                            decimal _NBC = Util.Korisno.VratiNabavnuCijenu(r["sifra"].ToString(), cbSkladiste.SelectedValue.ToString());
                            decimal kolicina, vpc, ukupno, pdv, mpc;
                            decimal.TryParse(r["vpc"].ToString(), out vpc);
                            decimal.TryParse(r["kolicina"].ToString(), out kolicina);
                            decimal.TryParse(r["porez"].ToString(), out pdv);

                            mpc = (vpc * pdv / 100) + vpc;
                            ukupno = kolicina * mpc;

                            dgw.Rows.Add(dgw.Rows.Count + 1, r["sifra"].ToString(), r["naziv"].ToString(), r["jm"].ToString(), 0, Math.Round(vpc, 3).ToString("#0.00"), Math.Round(mpc, 3).ToString("#0.00"), Math.Round(ukupno, 3).ToString("#0.00"), 0, "", pdv, _NBC.ToString("#0.000"));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška kod dodavanja stavki koje niste upisali u inventuri:\r\n" + ex.ToString());
                }
            }

            #endregion OVAJ DIO DODAJE NA INVENTURU SVE STAVKE KOJE NISU POPISANE

            if (!Class.Dokumenti.isKasica && ((Class.PodaciTvrtka.oibTvrtke == Class.Postavke.OIB_PC1 && Util.Korisno.GodinaKojaSeKoristiUbazi >= Class.Postavke.GODINA_POCETKA_FIFO_PC1) || Util.Korisno.GodinaKojaSeKoristiUbazi >= Class.Postavke.GODINA_POCETKA_FIFO))
            {
                string[] kolone = new string[3] { "kolicina", "nbc", "sifra" };

                Class.FIFO.setRacunDGW(ref dgw, Convert.ToInt32(cbSkladiste.SelectedValue), dtpDatum.Value, kolone);
            }

            if (edit)
            {
                UpdateInventura();
                EnableDisable(false);
                btnSveFakture.Enabled = true;
                ControlDisableEnable(1, 0, 0, 1, 0);
                txtBrojInventure.Text = brojInventure();
                return;
            }

            txtBrojInventure.Text = brojInventure();

            DataTable DTsend = new DataTable();
            DTsend.Columns.Add("broj_inventure");
            DTsend.Columns.Add("sifra_robe");
            DTsend.Columns.Add("jmj");
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("cijena");
            DTsend.Columns.Add("naziv");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("nbc");
            DataRow row;

            string sql = string.Format(@"INSERT INTO inventura (broj_inventure, id_skladiste, datum, id_zaposlenik, napomena, godina, novo)
VALUES ( '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '1' );",
                txtBrojInventure.Text,
                cbSkladiste.SelectedValue,
                dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss"),
                DTIzradio.Rows[0]["id_zaposlenik"].ToString(),
                rtbNapomena.Text,
                nmGodinaInventure.Value.ToString());

            classSQL.insert(sql);

            for (int i = 0; i < dgw.RowCount; i++)
            {
                decimal Tnbc = 0;
                decimal.TryParse(dg(i, "nbc"), out Tnbc);
                if (Tnbc == 0)
                {
                    decimal _NBC = Util.Korisno.VratiNabavnuCijenu(dg(i, "sifra"), cbSkladiste.SelectedValue.ToString());
                    Tnbc = _NBC;
                }

                row = DTsend.NewRow();
                row["broj_inventure"] = txtBrojInventure.Text;
                row["sifra_robe"] = dg(i, "sifra");
                row["jmj"] = dg(i, "jmj");
                row["kolicina"] = dg(i, "kolicina");
                row["cijena"] = dg(i, "cijena");
                row["naziv"] = dg(i, "naziv");
                row["porez"] = dg(i, "porez");
                row["nbc"] = Tnbc;
                DTsend.Rows.Add(row);
            }

            provjera_sql(classSQL.insert(string.Format("INSERT INTO aktivnost_zaposlenici (id_zaposlenik, datum, radnja) VALUES ('{0}', '{1}', 'Nova inventura br. {2}');",
                Properties.Settings.Default.id_zaposlenik,
                DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"),
                txtBrojInventure.Text)));

            SQL.SQLinventura.InsertStavke(DTsend);

            PostaviKolicineCijenu(txtBrojInventure.Text, nmGodinaInventure.Value.ToString(), dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss"), cbSkladiste.SelectedValue.ToString());
            deleteFields();
            EnableDisable(false);
            ControlDisableEnable(1, 0, 0, 1, 0);
            MessageBox.Show("Spremljeno.");
            spremljeno = true;
            txtBrojInventure.Text = brojInventure();
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private string dg(int row, string cell)
        {
            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            edit = false;
            deleteFields();
            EnableDisable(true);
            ControlDisableEnable(0, 1, 1, 0, 1);
            txtBrojInventure.Text = brojInventure();
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            edit = false;
            deleteFields();
            EnableDisable(false);
            txtBrojInventure.ReadOnly = false;
            nmGodinaInventure.ReadOnly = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
            txtBrojInventure.Text = brojInventure();
        }

        private void SveInventure_Click(object sender, EventArgs e)
        {
            frmSveInventure objForm2 = new frmSveInventure();
            objForm2.broj__inventure = "";
            objForm2.MainForm = this;
            objForm2.ShowDialog();

            if (broj_inventure_edit != null)
            {
                fillInventura();
                EnableDisable(true);
                edit = true;
                btnDeleteAllFaktura.Enabled = true;
                txtBrojInventure.ReadOnly = true;
                nmGodinaInventure.ReadOnly = true;
                ControlDisableEnable(0, 1, 1, 0, 1);
            }
        }

        private void fillInventura()
        {
            EnableDisable(true);
            edit = true;
            btnDeleteAllFaktura.Enabled = true;
            txtBrojInventure.ReadOnly = true;
            nmGodinaInventure.ReadOnly = true;
            ControlDisableEnable(0, 1, 1, 0, 1);
            edit = true;
            txtSifra_robe.Enabled = true;
            txtSifra_robe.ReadOnly = false;
            dgw.Rows.Clear();
            spremljeno = true;

            string sql = string.Format("SELECT * FROM inventura WHERE broj_inventure = '{0}';", broj_inventure_edit);
            DataTable DTinventura = classSQL.select(sql, "inventura").Tables[0];

            txtBrojInventure.Text = DTinventura.Rows[0]["broj_inventure"].ToString();
            nmGodinaInventure.Value = Convert.ToInt16(DTinventura.Rows[0]["godina"].ToString());
            dtpDatum.Value = Convert.ToDateTime(DTinventura.Rows[0]["datum"].ToString());
            rtbNapomena.Text = DTinventura.Rows[0]["napomena"].ToString();
            cbSkladiste.SelectedValue = DTinventura.Rows[0]["id_skladiste"].ToString();
            txtIzradio.Text = classSQL.select(string.Format("SELECT ime + ' ' + prezime as Ime, id_zaposlenik FROM zaposlenici WHERE id_zaposlenik = '{0}';", DTinventura.Rows[0]["id_zaposlenik"].ToString()), "zaposlenici").Tables[0].Rows[0][0].ToString();

            string sql1 = string.Format("SELECT * FROM inventura_stavke WHERE broj_inventure = '{0}';", broj_inventure_edit);

            DataTable DTinventura_stavke = classSQL.select(sql1, "inventura_stavke").Tables[0];

            for (int br = 0; br < DTinventura_stavke.Rows.Count; br++)
            {
                dgw.Rows.Add();

                dgw.Rows[br].Cells[0].Value = dgw.RowCount;
                dgw.Rows[br].Cells["sifra"].Value = DTinventura_stavke.Rows[br]["sifra_robe"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = DTinventura_stavke.Rows[br]["naziv"].ToString();
                dgw.Rows[br].Cells["jmj"].Value = DTinventura_stavke.Rows[br]["jmj"].ToString();
                dgw.Rows[br].Cells["kolicina"].Value = DTinventura_stavke.Rows[br]["kolicina"].ToString();
                dgw.Rows[br].Cells["id_stavka"].Value = DTinventura_stavke.Rows[br]["id_stavke"].ToString();
                try
                {
                    dgw.Rows[br].Cells["vpc"].Value = Math.Round(Convert.ToDouble(DTinventura_stavke.Rows[br]["cijena"].ToString())
                        / (1 + Convert.ToDouble(DTinventura_stavke.Rows[br]["porez"].ToString()) / 100), 2);
                }
                catch
                {
                    dgw.Rows[br].Cells["vpc"].Value = Math.Round(Convert.ToDouble(DTinventura_stavke.Rows[br]["cijena"].ToString()), 2);
                }
                dgw.Rows[br].Cells["cijena"].Value = Math.Round(Convert.ToDouble(DTinventura_stavke.Rows[br]["cijena"].ToString()), 2);
                dgw.Rows[br].Cells["iznos"].Value = Math.Round(Convert.ToDouble(DTinventura_stavke.Rows[br]["cijena"].ToString()) *
                    Convert.ToDouble(DTinventura_stavke.Rows[br]["kolicina"].ToString()), 2);
                dgw.Rows[br].Cells["porez"].Value = DTinventura_stavke.Rows[br]["porez"].ToString();

                dgw.Rows[br].Cells["nbc"].Value = DTinventura_stavke.Rows[br]["nbc"].ToString();
            }

            ControlDisableEnable(0, 1, 1, 0, 1);

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Format = "N2";
            dgw.Columns["cijena"].DefaultCellStyle = style;
            dgw.Columns["iznos"].DefaultCellStyle = style;
            dgw.Columns["vpc"].DefaultCellStyle = style;

            dgw.Columns["vpc"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["cijena"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["vpc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["cijena"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da želite obrisati ovu inventuru?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                classSQL.delete(string.Format("DELETE FROM inventura_stavke WHERE broj_inventure = '{0}';", txtBrojInventure.Text));
                classSQL.delete(string.Format("DELETE FROM inventura WHERE broj_inventure = '{0}';", txtBrojInventure.Text));
                classSQL.insert(string.Format("INSERT INTO aktivnost_zaposlenici (id_zaposlenik, datum, radnja) VALUES ('{0}', '{1}', 'Brisanje cijele inventure br. {2}');",
                    Properties.Settings.Default.id_zaposlenik,
                    DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"),
                    txtBrojInventure.Text));
                MessageBox.Show("Obrisano.");
                provjera_sql(classSQL.insert(string.Format("INSERT INTO aktivnost_zaposlenici (id_zaposlenik, datum, radnja) VALUES ('{0}', '{1}', 'Brisanje cijele inventure br. {2}');",
                    Properties.Settings.Default.id_zaposlenik,
                    DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"),
                    txtBrojInventure.Text)));

                edit = false;
                EnableDisable(false);
                deleteFields();
                ControlDisableEnable(1, 0, 0, 1, 0);
            }
        }

        private void UpdateInventura()
        {
            DataTable DTsend = new DataTable();
            DTsend.Columns.Add("broj_inventure");
            DTsend.Columns.Add("sifra_robe");
            DTsend.Columns.Add("jmj");
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("cijena");
            DTsend.Columns.Add("naziv");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("nbc");
            DataRow row;

            DataTable DTsend1 = new DataTable();
            DTsend1.Columns.Add("broj_inventure");
            DTsend1.Columns.Add("sifra_robe");
            DTsend1.Columns.Add("jmj");
            DTsend1.Columns.Add("kolicina");
            DTsend1.Columns.Add("cijena");
            DTsend1.Columns.Add("naziv");
            DTsend1.Columns.Add("porez");
            DTsend1.Columns.Add("nbc");
            DataRow row1;

            string sql = string.Format(@"UPDATE inventura
                        SET
                            id_skladiste = '{0}',
                            datum = '{1}',
                            napomena = '{2}',
                            editirano = '1',
                            godina = '{3}'
                        WHERE broj_inventure = '{4}';",
             cbSkladiste.SelectedValue,
             dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss"),
             rtbNapomena.Text,
             nmGodinaInventure.Value.ToString(),
             txtBrojInventure.Text);

            classSQL.update(sql);

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                decimal Tnbc = 0;
                decimal.TryParse(dg(i, "nbc"), out Tnbc);
                if (Tnbc == 0)
                {
                    decimal _NBC = Util.Korisno.VratiNabavnuCijenu(dg(i, "sifra"), cbSkladiste.SelectedValue.ToString());
                    Tnbc = _NBC;
                }

                if (dgw.Rows[i].Cells["id_stavka"].Value != null && dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString() != "")
                {
                    row = DTsend.NewRow();
                    row["broj_inventure"] = txtBrojInventure.Text;
                    row["sifra_robe"] = dg(i, "sifra");
                    row["jmj"] = dg(i, "jmj");
                    row["kolicina"] = dg(i, "kolicina");
                    row["cijena"] = Convert.ToDouble(dg(i, "cijena"));
                    row["naziv"] = dg(i, "naziv");
                    row["porez"] = dg(i, "porez");
                    row["nbc"] = Tnbc.ToString().Replace(",", ".");
                    DTsend.Rows.Add(row);
                }
                else
                {
                    row1 = DTsend1.NewRow();
                    row1["broj_inventure"] = txtBrojInventure.Text;
                    row1["sifra_robe"] = dg(i, "sifra");
                    row1["jmj"] = dg(i, "jmj");
                    row1["kolicina"] = dg(i, "kolicina");
                    row1["cijena"] = dg(i, "cijena");
                    row1["naziv"] = dg(i, "naziv");
                    row1["porez"] = dg(i, "porez");
                    row1["nbc"] = Tnbc.ToString().Replace(",", ".");
                    DTsend1.Rows.Add(row1);
                }
            }

            provjera_sql(classSQL.insert(string.Format("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('{0}', '{1}', 'Izmjena inventure br. {2}');",
                Properties.Settings.Default.id_zaposlenik,
                DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"),
                txtBrojInventure.Text)));
            provjera_sql(SQL.SQLinventura.InsertStavke(DTsend1));
            provjera_sql(SQL.SQLinventura.UpdateStavke(DTsend));

            PostaviKolicineCijenu(txtBrojInventure.Text, nmGodinaInventure.Value.ToString(), dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss"), cbSkladiste.SelectedValue.ToString());

            deleteFields();
            EnableDisable(false);
            edit = false;
            MessageBox.Show("Spremljeno.");
        }

        private void txtBrojInventure_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select(string.Format("SELECT broj_inventure FROM inventura WHERE broj_inventure = '{0}';", txtBrojInventure.Text), "inventura").Tables[0];
                deleteFields();
                if (DT.Rows.Count == 0)
                {
                    if (brojInventure() == txtBrojInventure.Text.Trim())
                    {
                        deleteFields();
                        edit = false;
                        EnableDisable(true);
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    broj_inventure_edit = txtBrojInventure.Text;
                    fillInventura();
                    EnableDisable(true);
                    edit = true;
                    btnDeleteAllFaktura.Enabled = true;
                    txtBrojInventure.ReadOnly = true;
                    nmGodinaInventure.ReadOnly = true;
                }
                ControlDisableEnable(0, 1, 1, 0, 1);

                e.SuppressKeyPress = true;
                cbSkladiste.Select();
            }
        }

        private void cbSkladiste_KeyDown(object sender, KeyEventArgs e)
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
                rtbNapomena.Select();
            }
        }

        private void txtIzradio_KeyDown(object sender, KeyEventArgs e)
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
                e.SuppressKeyPress = true;
                txtSifra_robe.Select();
            }
        }

        private void frmUnosInventura_FormClosing(object sender, FormClosingEventArgs e)
        {
            /****************************SINKRONIZACIJA SA WEB-OM*****************/
            bgSinkronizacija.RunWorkerAsync();
            /****************************SINKRONIZACIJA SA WEB-OM*****************/

            if (spremljeno == false)
            {
                if (MessageBox.Show("Niste spremili inventuru, zelite li sada spremiti inventuru?", "Inventura", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dgw.RowCount > 0)
                    {
                        btnSpremi.PerformClick();
                    }
                }
            }
        }

        private void btnUcitajSveStavke_Click(object sender, EventArgs e)
        {
            string sql = string.Format(@"SELECT roba.jm, roba.naziv, roba.oduzmi, roba_prodaja.id_skladiste, roba_prodaja.kolicina, roba_prodaja.vpc, roba_prodaja.porez, roba_prodaja.sifra
FROM roba_prodaja
INNER JOIN roba ON roba_prodaja.sifra = roba.sifra
WHERE roba_prodaja.id_skladiste = '{0}' AND roba_prodaja.sifra IN (
    SELECT * FROM (
        SELECT sifra_robe as sifra FROM racun_stavke WHERE id_skladiste = '{0}' GROUP BY sifra_robe
        UNION
        SELECT sifra as sifra FROM faktura_stavke WHERE id_skladiste = '{0}' GROUP BY sifra
        UNION
        SELECT sifra as sifra FROM kalkulacija_stavke WHERE id_skladiste = '{0}' GROUP BY sifra
        UNION
        SELECT sifra as sifra FROM pocetno GROUP BY sifra
        UNION
        SELECT sifra as sifra FROM meduskladisnica_stavke GROUP BY sifra
        UNION
        SELECT sifra as sifra FROM primka_stavke GROUP BY sifra
        UNION
        SELECT sifra as sifra FROM ponude_stavke GROUP BY sifra
    ) koristivo ORDER BY sifra ASC
) AND roba.oduzmi = 'DA'
ORDER BY roba.naziv ASC;", cbSkladiste.SelectedValue);

            DataTable DT = classSQL.select(sql, "roba_prodaja").Tables[0];
            int i = 1;

            dgw.Rows.Clear();

            foreach (DataRow r in DT.Rows)
            {
                decimal kolicina, vpc, ukupno, pdv, mpc;
                decimal.TryParse(r["vpc"].ToString(), out vpc);
                decimal.TryParse(r["kolicina"].ToString(), out kolicina);
                decimal.TryParse(r["porez"].ToString(), out pdv);

                bool postoji = false;
                for (int y = 0; y < dgw.Rows.Count; y++)
                {
                    if (r["sifra"].ToString() == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString().Trim())
                    {
                        postoji = true;
                        break;
                    }
                }

                if (!postoji)
                {
                    mpc = (vpc * pdv / 100) + vpc;
                    ukupno = kolicina * mpc;

                    decimal _NBC = Util.Korisno.VratiNabavnuCijenu(r["sifra"].ToString(), cbSkladiste.SelectedValue.ToString());

                    dgw.Rows.Add(i, r["sifra"].ToString(), r["naziv"].ToString(), r["jm"].ToString(), r["kolicina"].ToString(), Math.Round(vpc, 3).ToString("#0.00"), Math.Round(mpc, 3).ToString("#0.00"), Math.Round(ukupno, 3).ToString("#0.00"), r["kolicina"].ToString(), "", pdv, _NBC.ToString("#0.000"));
                    i++;
                }
            }
        }

        private void PostaviKolicineCijenu(string broj, string godina, string datum, string skladiste)
        {
            //prvo postavljam sve na nulu
            classSQL.update(string.Format("UPDATE inventura_stavke SET kolicina_koja_je_bila = '0' WHERE broj_inventure = '{0}';", broj));

            Util.Korisno korisno = new Util.Korisno();
            DataTable DT_kol = korisno.VratiKolicinuNaDan(datum, skladiste);
            string sql = "BEGIN;";
            foreach (DataRow r in DT_kol.Rows)
            {
                decimal kol;
                decimal.TryParse(r["kolicina"].ToString(), out kol);
                sql += string.Format(@"
UPDATE inventura_stavke SET kolicina_koja_je_bila = '{0}'
WHERE broj_inventure = '{1}' AND sifra_robe = '{2}';",
                kol.ToString().Replace(",", "."),
                broj,
                r["sifra"].ToString());
            }
            sql += "COMMIT;";

            if (sql.Length > 20)
                classSQL.update(sql);
        }
    }
}