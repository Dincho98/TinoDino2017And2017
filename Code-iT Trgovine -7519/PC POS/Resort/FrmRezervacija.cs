using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace PCPOS.Resort
{
    public partial class FrmRezervacija : Form
    {
        public int BrojRezervacije { get; set; }
        public bool EditMode = false;
        public bool ReservationCreated = false;

        public FrmRezervacija()
        {
            InitializeComponent();
        }

        private void FrmUnosRezervacije_Load(object sender, EventArgs e)
        {
            Paint += new PaintEventHandler(FrmUnosRezervacije_Paint); // Sets background gradient
            SetGodinaBorders();
            SetDateFormat();
            LoadComboBoxes();
            if (EditMode)
                LoadReservation(Global.Database.GetRezervacije(BrojRezervacije.ToString()));
            else
                SetFields();

            FillGrid();
        }

        private void FrmUnosRezervacije_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        /// <summary>
        /// Form load used when creating new reservation
        /// </summary>
        private void SetFields()
        {
            SetBroj();
            numGodina.Value = DateTime.Now.Year;
        }

        /// <summary>
        /// Form load used when editing reservation
        /// </summary>
        /// <param name="dataTable">Reservation data</param>
        private void LoadReservation(DataTable dataTable)
        {
            tbBrojUnosa.Text = dataTable.Rows[0]["broj"].ToString();
            numGodina.Value = Convert.ToInt32(dataTable.Rows[0]["godina"].ToString());

            DataTable DTpartner = Global.Database.GetPartners(dataTable.Rows[0]["id_partner"].ToString());
            if (DTpartner?.Rows.Count > 0)
            {
                tbPartnerBroj.Text = DTpartner.Rows[0]["id_partner"].ToString();
                tbPartnerNaziv.Text = DTpartner.Rows[0]["ime_tvrtke"].ToString();
            }

            dtpDatumOdlaska.Value = DateTime.Parse(dataTable.Rows[0]["datum_odlaska"].ToString());
            dtpDatumDolaska.Value = DateTime.Parse(dataTable.Rows[0]["datum_dolaska"].ToString());

            cbAgencija.SelectedValue = dataTable.Rows[0]["id_agencija"].ToString();
            cbSoba.SelectedValue = dataTable.Rows[0]["id_soba"].ToString();
            cbVrstaUsluge.SelectedValue = dataTable.Rows[0]["id_vrsta_usluge"].ToString();

            rtbNapomena.Text = dataTable.Rows[0]["napomena"].ToString();
            tbBrojOsobne.Text = dataTable.Rows[0]["broj_osobne"].ToString();
            tbBrojPutovnice.Text = dataTable.Rows[0]["broj_putovnice"].ToString();

            numOdrasli.Value = Convert.ToInt32(dataTable.Rows[0]["odrasli"].ToString());
            numDjeca.Value = Convert.ToInt32(dataTable.Rows[0]["djeca"].ToString());
            numBebe.Value = Convert.ToInt32(dataTable.Rows[0]["bebe"].ToString());

            chbDorucak.Checked = Convert.ToInt32(dataTable.Rows[0]["dorucak"].ToString()) == 1 ? true : false;
            chbRucak.Checked = Convert.ToInt32(dataTable.Rows[0]["rucak"].ToString()) == 1 ? true : false;
            chbVecera.Checked = Convert.ToInt32(dataTable.Rows[0]["vecera"].ToString()) == 1 ? true : false;
        }

        /// <summary>
        /// Used to fill grid with reservations data (unos_rezervacije)
        /// </summary>
        private void FillGrid()
        {
            if (dataGridView.Rows.Count > 0)
                RefreshGrid();

            DataTable DTrezervacije = Global.Database.GetRezervacije();
            if (DTrezervacije?.Rows.Count > 0)
            {
                foreach (DataRow row in DTrezervacije.Rows)
                {
                    int rowIndex = dataGridView.Rows.Add();
                    dataGridView.Rows[rowIndex].Cells["broj"].Value = row["broj"].ToString();
                    dataGridView.Rows[rowIndex].Cells["partner"].Value = Global.Database.GetPartners(row["id_partner"].ToString())?.Rows[0]["ime_tvrtke"].ToString();
                    dataGridView.Rows[rowIndex].Cells["soba"].Value = Global.Database.GetSobe(row["id_soba"].ToString())?.Rows[0]["naziv_sobe"].ToString();
                    dataGridView.Rows[rowIndex].Cells["datum_dolaska"].Value = DateTime.Parse(row["datum_dolaska"].ToString()).ToString("dd.MM.yyyy. HH:mm");
                    dataGridView.Rows[rowIndex].Cells["datum_odlaska"].Value = DateTime.Parse(row["datum_odlaska"].ToString()).ToString("dd.MM.yyyy. HH:mm");
                    dataGridView.Rows[rowIndex].Cells["vrsta_usluge"].Value = Global.Database.GetVrstaUsluge(row["id_vrsta_usluge"].ToString())?.Rows[0]["naziv_usluge"].ToString();
                    dataGridView.Rows[rowIndex].Cells["naplaceno"].Value = row["naplaceno"].ToString() == "1" ? "Naplačeno" : "Nije naplačeno";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void RefreshGrid()
        {
            dataGridView.Rows.Clear();
            dataGridView.Refresh();
        }

        /// <summary>
        /// Gets and sets highest reservation number
        /// </summary>
        /// <returns></returns>
        private void SetBroj()
        {
            string query = "SELECT MAX(broj) FROM unos_rezervacije";
            DataTable DTmaxBroj = classSQL.select(query, "unos_rezervacije").Tables[0];
            string broj = DTmaxBroj.Rows[0][0].ToString();
            tbBrojUnosa.Text = string.IsNullOrWhiteSpace(broj) ? "1" : (Convert.ToInt32(broj) + 1).ToString();
        }

        /// <summary>
        /// Sets current year
        /// </summary>
        private void SetGodinaBorders()
        {
            numGodina.Minimum = DateTime.Now.Year - 30;
            numGodina.Maximum = DateTime.Now.Year + 30;
        }

        /// <summary>
        /// Sets dates format
        /// </summary>
        private void SetDateFormat()
        {
            dtpDatumDolaska.Format = DateTimePickerFormat.Custom;
            dtpDatumDolaska.CustomFormat = "dd.MM.yyyy HH:mm";

            dtpDatumOdlaska.Format = DateTimePickerFormat.Custom;
            dtpDatumOdlaska.CustomFormat = "dd.MM.yyyy HH:mm";
        }

        /// <summary>
        /// Sets room price and calcualtes total price
        /// </summary>
        private void SetPrices()
        {
            DataTable DTsoba = Global.Database.GetSobe(cbSoba.SelectedValue.ToString());
            if (DTsoba?.Rows.Count > 0)
            {
                decimal.TryParse(DTsoba.Rows[0]["cijena_nocenja"].ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal cijenaNocenja);
                lblCijenaSobe.Text = cijenaNocenja.ToString("#0.00") + " kn";

                // Calculate total
                var totalDays = UkupnoDana();
                if (totalDays >= 0)
                    lblUkupno.Text = (cijenaNocenja * totalDays).ToString() + " kn";
            }
        }

        private int UkupnoDana()
        {
            //Potrebno je pretvoriti dateTimePicker u string zbog toga što je dateTimePicker u custom formatu.
            //Nakon toga uzimamo broj dana, pretvaramo ih u int i oduzimamo te vraćamo razliku.
            string startDate = dtpDatumDolaska.Value.ToString();
            string endDate = dtpDatumOdlaska.Value.ToString();

            //Uzimamo dane
            startDate = startDate.Substring(0, 2);
            endDate = endDate.Substring(0, 2);

            //Ako je npr dan 6., trebamo maknuti točku
            if (startDate[1] == '.')
                startDate = startDate.Substring(0, 1);
            if (endDate[1] == '.')
                endDate = endDate.Substring(0, 1);

            //Vraćamo razliku
            return Int32.Parse(endDate) - Int32.Parse(startDate) + 1;
        }

        /// <summary>
        /// Load all Comboboxes data
        /// </summary>
        private void LoadComboBoxes()
        {
            FillComboBox(Global.Database.GetAgencija(), cbAgencija, "id", "ime_agencije");
            FillComboBox(Global.Database.GetSobe(), cbSoba, "broj_sobe", "naziv_sobe");
            FillComboBox(Global.Database.GetVrstaUsluge(), cbVrstaUsluge, "id", "naziv_usluge");
        }

        /// <summary>
        /// Loads data for specific Combobox
        /// </summary>
        /// <param name="dataTable">DataTable to use as Datasource</param>
        /// <param name="comboBox">ComboBox to fill</param>
        /// <param name="valueMember"></param>
        /// <param name="displayMember"></param>
        private void FillComboBox(DataTable dataTable, ComboBox comboBox, string valueMember, string displayMember)
        {
            if (dataTable?.Rows.Count > 0)
            {
                comboBox.ValueMember = valueMember;
                comboBox.DisplayMember = displayMember;
                comboBox.DataSource = dataTable;
            }
        }

        /// <summary>
        /// Opens form to select partner
        /// </summary>
        private void SelectPartner()
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataTable DTpartner = classSQL.select($"SELECT * FROM partners WHERE id_partner ='{Properties.Settings.Default.id_partner}'", "partners").Tables[0];
                if (DTpartner?.Rows.Count > 0)
                {
                    tbPartnerBroj.Text = DTpartner.Rows[0]["id_partner"].ToString();
                    tbPartnerNaziv.Text = DTpartner.Rows[0]["ime_tvrtke"].ToString();
                }
            }
        }

        /// <summary>
        /// Used when user clicks on Save button (Spremi promjene)
        /// </summary>
        private void SaveReservation()
        {
            if (!string.IsNullOrWhiteSpace(tbPartnerBroj.Text) && !string.IsNullOrWhiteSpace(tbPartnerNaziv.Text))
            {
                if (Global.Database.GetPartners(tbPartnerBroj.Text).Rows.Count == 0)
                {
                    MessageBox.Show("Partner pod tom šifrom ne postoji!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var confirmDialog = MessageBox.Show("Jeste li sigurni da želite spremiti ovu rezervaciju?", "Obavijest", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmDialog == DialogResult.Yes)
                {
                    if (!EditMode)
                        CreateReservation();
                    else
                        UpdateReservation();
                }
            }
            else
                MessageBox.Show("Niste odabrali partnera!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Used to create new reservation
        /// </summary>
        private void CreateReservation()
        {
            if (!PostojiAgencija())
                return;

            string insertQuery = $@"INSERT INTO unos_rezervacije (broj, godina, vrijeme_unosa, id_partner, broj_osobne, broj_putovnice, datum_dolaska, datum_odlaska, id_agencija, id_soba, id_vrsta_usluge, dorucak, rucak, vecera, odrasli, djeca, bebe, ukupno, napomena)
                                            VALUES ({tbBrojUnosa.Text}, {numGodina.Value}, '{DateTime.Now.ToString("yyyy-MM-dd HH:mm")}', '{tbPartnerBroj.Text}',
                                            '{tbBrojOsobne.Text}', '{tbBrojPutovnice.Text}', '{dtpDatumDolaska.Value.ToString("yyyy-MM-dd HH:mm")}', '{dtpDatumOdlaska.Value.ToString("yyyy-MM-dd HH:mm")}',
                                            {cbAgencija.SelectedValue.ToString()}, {cbSoba.SelectedValue.ToString()}, {cbVrstaUsluge.SelectedValue.ToString()},
                                            {(chbDorucak.Checked ? "1" : "0")}, {(chbRucak.Checked ? "1" : "0")}, {(chbVecera.Checked ? "1" : "0")},
                                            {numOdrasli.Value}, {numDjeca.Value}, {numBebe.Value}, {lblUkupno.Text.Replace(" kn", "").Replace(',', '.')}, '{rtbNapomena.Text}')";

            try
            {
                classSQL.insert(insertQuery);
                MessageBox.Show("Spremljeno.", "Obavijest", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReservationCreated = true;
                Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Execute exception issue: {ex.Message}");
            }
        }

        private bool PostojiAgencija()
        {
            if (cbAgencija.Items.Count == 0)
            {
                MessageBox.Show("Niste odabrali agenciju.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Used to update/edit current reservation
        /// </summary>
        private void UpdateReservation()
        {
            string updateQuery = $@"UPDATE unos_rezervacije SET 
                                        godina = {numGodina.Value},
                                        id_partner = '{tbPartnerBroj.Text}',
                                        broj_osobne = '{tbBrojOsobne.Text}',
                                        broj_putovnice = '{tbBrojPutovnice.Text}',
                                        datum_dolaska = '{dtpDatumDolaska.Value.ToString("yyyy-MM-dd HH:mm")}',
                                        datum_odlaska = '{dtpDatumOdlaska.Value.ToString("yyyy-MM-dd HH:mm")}',
                                        id_agencija = {cbAgencija.SelectedValue},
                                        id_soba = {cbSoba.SelectedValue},
                                        id_vrsta_usluge = {cbVrstaUsluge.SelectedValue},
                                        dorucak = {(chbDorucak.Checked ? "1" : "0")},
                                        rucak = {(chbRucak.Checked ? "1" : "0")},
                                        vecera = {(chbVecera.Checked ? "1" : "0")},
                                        odrasli = {numOdrasli.Value},
                                        djeca = {numDjeca.Value},
                                        bebe = {numBebe.Value},
                                        ukupno = {lblUkupno.Text},
                                        napomena = '{rtbNapomena.Text}'
                                    WHERE broj = {BrojRezervacije}";

            try
            {
                classSQL.update(updateQuery);
                MessageBox.Show("Spremljeno.", "Obavijest", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Execute exception issue: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        private void DeleteReservation()
        {
            try
            {
                string query = $@"DELETE FROM unos_rezervacije WHERE broj = {GetSelectedReservationId()}";
                classSQL.delete(query);
                MessageBox.Show("Obrisano.", "Obavijest");
                FillGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Gets selected room id (broj) from the grid
        /// </summary>
        /// <returns></returns>
        private string GetSelectedReservationId()
        {
            return dataGridView.Rows[dataGridView.CurrentCell.RowIndex].Cells["broj"].Value.ToString();
        }

        /// <summary>
        /// Resets all form fields
        /// </summary>
        private void ResetFields()
        {
            tbBrojUnosa.Text = null;
            tbPartnerBroj.Text = null;
            tbPartnerNaziv.Text = null;
            cbAgencija.SelectedIndex = 0;
            cbSoba.SelectedIndex = 0;
            rtbNapomena.Text = null;
            dtpDatumDolaska.Value = DateTime.Now;
            dtpDatumOdlaska.Value = DateTime.Now;
            tbBrojOsobne.Text = null;
            tbBrojPutovnice.Text = null;
            lblCijenaSobe.Text = "0,00 kn";
            lblUkupno.Text = "0,00 kn";
            numBebe.Value = 0;
            numDjeca.Value = 0;
            numOdrasli.Value = 0;
            cbVrstaUsluge.SelectedIndex = 0;
            chbDorucak.Checked = false;
            chbRucak.Checked = false;
            chbVecera.Checked = false;
        }

        private void TbBrojUnosa_KeyDown(object sender, KeyEventArgs e)
        {
            DataTable DTrezervacija = Global.Database.GetRezervacije(tbBrojUnosa.Text);
            if (DTrezervacija?.Rows.Count > 0)
                LoadReservation(DTrezervacija);
        }

        private void TbPartnerBroj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    DataTable DTpartner = Global.Database.GetPartners(tbPartnerBroj.Text);
                    if (DTpartner?.Rows.Count > 0)
                    {
                        tbPartnerBroj.Text = DTpartner.Rows[0]["id_partner"].ToString();
                        tbPartnerNaziv.Text = DTpartner.Rows[0]["ime_tvrtke"].ToString();
                    }
                }
                catch
                {
                    SelectPartner();
                }
            }
        }

        #region Set Prices

        private void CbSoba_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPrices();
        }

        private void DtpDatumDolaska_ValueChanged(object sender, EventArgs e)
        {
            SetPrices();
        }

        private void DtpDatumOdlaska_ValueChanged(object sender, EventArgs e)
        {
            SetPrices();
        }

        #endregion

        private void BtnSpremi_Click(object sender, EventArgs e)
        {
            SaveReservation();
        }

        private void BtnUredi_Click(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count > 0)
            {
                DataTable DTrezervacija = Global.Database.GetRezervacije(GetSelectedReservationId());
                if (DTrezervacija?.Rows.Count > 0)
                {
                    EditMode = true;
                    LoadReservation(DTrezervacija);
                }
            }
        }

        private void BtnObrisi_Click(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count > 0)
            {
                var confirmDialog = MessageBox.Show("Jeste li sigurni da želite obrisati označenu rezervaciju?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmDialog == DialogResult.Yes)
                {
                    if (tbBrojUnosa.Text == GetSelectedReservationId())
                    {
                        EditMode = false;
                        ResetFields();
                    }
                    DeleteReservation();
                    FillGrid();
                    SetFields();
                }
            }
        }
    }
}
