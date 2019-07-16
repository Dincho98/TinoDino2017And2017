using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace PCPOS.Robno
{
    public partial class frmRobniDobropis : Form
    {
        private int idIzradio { get; set; }
        private bool edit = false;

        public frmRobniDobropis()
        {
            InitializeComponent();
        }

        private void frmRobniDobropis_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(frmRobniDobropis_Paint); // Sets background gradient

            SetBrojDobropis();
            SetNumericGodina();
            FillMjestoTroskaCB();
            FillSkladisteCB();
            SetIzradio();
        }

        private void frmRobniDobropis_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        /// <summary>
        /// Gets highest "broj" from "dobropis" table and sets next highest ID
        /// </summary>
        private void SetBrojDobropis()
        {
            string query = "SELECT MAX(broj_dobropis) FROM dobropis";
            DataTable DTbroj = classSQL.select(query, "dobropis").Tables[0];
            if (DTbroj.Rows.Count > 0)
            {
                bool result = Int32.TryParse(DTbroj.Rows[0][0].ToString(), out int maxBroj);
                if (result)
                    tbBrojUnosa.Text = (maxBroj + 1).ToString();
                else
                    tbBrojUnosa.Text = "1";
            }
        }

        /// <summary>
        /// Sets current year to nmGodina
        /// </summary>
        private void SetNumericGodina()
        {
            nmGodina.Minimum = DateTime.Now.Year - 30;
            nmGodina.Maximum = DateTime.Now.Year + 30;
            nmGodina.Value = DateTime.Now.Year;
        }

        /// <summary>
        /// Used to fill "Skladiste" combobox with data from database
        /// </summary>
        private void FillSkladisteCB()
        {
            string query = @"SELECT id_skladiste, skladiste FROM skladiste";
            DataTable DTskladiste = classSQL.select(query, "skladiste").Tables[0];
            if (DTskladiste.Rows.Count > 0)
            {
                cbSkladiste.ValueMember = "id_skladiste";
                cbSkladiste.DisplayMember = "skladiste";
                cbSkladiste.DataSource = DTskladiste;
            }
        }

        /// <summary>
        /// Gets current logged user and sets tbIzradio
        /// </summary>
        private void SetIzradio()
        {
            string query = $"SELECT id_zaposlenik, CONCAT(ime, ' ', prezime) AS naziv FROM zaposlenici WHERE id_zaposlenik = '{Properties.Settings.Default.id_zaposlenik}'";
            DataTable DTzaposlenik = classSQL.select(query, "zaposlenici").Tables[0];
            if (DTzaposlenik.Rows.Count > 0)
            {
                idIzradio = Convert.ToInt32(DTzaposlenik.Rows[0]["id_zaposlenik"].ToString());
                tbIzradio.Text = DTzaposlenik.Rows[0]["naziv"].ToString();
            }
        }

        /// <summary>
        /// Used to fill "Mjesto troska" combobox with data from database
        /// </summary>
        private void FillMjestoTroskaCB()
        {
            DataTable DTgrad = classSQL.select("SELECT * FROM grad ORDER BY grad", "grad").Tables[0];
            cbMjestoTroska.DisplayMember = "grad";
            cbMjestoTroska.ValueMember = "id_grad";
            cbMjestoTroska.DataSource = DTgrad;
        }

        /// <summary>
        /// Fills DataGridView with recently selected item (roba)
        /// </summary>
        /// <param name="dataTable">Result from database</param>
        private void SetRoba(DataTable dataTable, bool beginEdit = true)
        {
            dgw.Rows.Add();
            int rowIndex = dgw.Rows.Count - 1;

            dgw.Rows[rowIndex].Cells["br"].Value = dgw.Rows.Count.ToString();
            foreach (DataRow row in dataTable.Rows)
            {
                decimal.TryParse(row["porez"].ToString(), out decimal porez);
                decimal.TryParse(row["nc"].ToString(), out decimal vpc);

                decimal mpc = (vpc + (vpc * porez / 100));

                dgw.Rows[rowIndex].Cells["sifra"].Value = row["sifra"].ToString();
                dgw.Rows[rowIndex].Cells["naziv_robe"].Value = row["naziv"].ToString();
                dgw.Rows[rowIndex].Cells["jm"].Value = row["jm"].ToString().ToString();
                dgw.Rows[rowIndex].Cells["kolicina"].Value = "1,00";
                dgw.Rows[rowIndex].Cells["porez"].Value = porez.ToString("#0.00");
                dgw.Rows[rowIndex].Cells["mpc"].Value = mpc.ToString("#0.00");
                dgw.Rows[rowIndex].Cells["vpc"].Value = vpc.ToString("#0.00");
                dgw.Rows[rowIndex].Cells["rabat"].Value = "0,00";
                dgw.Rows[rowIndex].Cells["rabat_iznos"].Value = "0,00";
                dgw.Rows[rowIndex].Cells["cijena_bez_pdv"].Value = Math.Round(vpc, 3).ToString("#0.00");
                dgw.Rows[rowIndex].Cells["iznos_bez_pdv"].Value = vpc.ToString("#0.00");
                dgw.Rows[rowIndex].Cells["iznos_ukupno"].Value = vpc.ToString("#0.00");
            }

            if (beginEdit)
            {
                dgw.Rows[rowIndex].Selected = true;
                dgw.CurrentCell = dgw.Rows[rowIndex].Cells[4];
                dgw.BeginEdit(true);
            }
        }

        private void btnIzlaz_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pbTraziOdrediste_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    tbPartnerSifra.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    tbPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void pbTraziStavke_Click(object sender, EventArgs e)
        {
            SelectRoba();
        }

        private void dgw_KeyDown(object sender, KeyEventArgs e)
        {
            int iColumn = dgw.CurrentCell.ColumnIndex;
            int iRow = dgw.CurrentCell.RowIndex;

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (iColumn == dgw.Columns.Count - 1)
                {
                    dgw.Rows.Add();
                    dgw.CurrentCell = dgw[0, iRow + 1];
                }
                else
                {
                    dgw.CurrentCell = dgw[iColumn + 1, iRow];
                }
            }
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            // Used to enable editing
            if (dgw.Rows.Count > 0)
                dgw.BeginEdit(true);
        }

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.Rows.Count < 1)
                return;

            if (dgw.CurrentCell.ColumnIndex == 2)
            {
                dgw.CurrentCell = dgw.Rows[dgw.CurrentRow.Index].Cells[4];
                dgw.BeginEdit(true);
            }
            else if (dgw.CurrentCell.ColumnIndex == 3)
            {
                dgw.CurrentCell = dgw.Rows[dgw.CurrentRow.Index].Cells[4];
                dgw.BeginEdit(true);
            }
            else if (dgw.CurrentCell.ColumnIndex == 4)
            {
                decimal tempKol = 0;
                string EditedFormattedValue = "0";
                if (dgw.Rows[dgw.CurrentRow.Index].Cells[4].EditedFormattedValue == null)
                {
                    EditedFormattedValue = "0";
                }
                else
                {
                    EditedFormattedValue = dgw.Rows[dgw.CurrentRow.Index].Cells[4].EditedFormattedValue.ToString();
                }
                if (decimal.TryParse(EditedFormattedValue, out tempKol))
                {
                    if (tempKol <= 0 && !Class.Postavke.kolicina_u_minus)
                    {
                        MessageBox.Show("Nije dozvoljeno upisivanje količine u minus.");
                        dgw.CurrentCell = dgw.Rows[dgw.CurrentRow.Index].Cells[(4)];
                        dgw.BeginEdit(true);
                        return;
                    }
                }

                dgw.CurrentCell = dgw.Rows[dgw.CurrentRow.Index].Cells[5];
                dgw.BeginEdit(true);
            }
            else if (dgw.CurrentCell.ColumnIndex == 5)
            {
                dgw.CurrentCell = dgw.Rows[dgw.CurrentRow.Index].Cells[6];
                dgw.BeginEdit(true);
            }
            else if (dgw.CurrentCell.ColumnIndex == 6)
            {
                dgw.CurrentCell = dgw.Rows[dgw.CurrentRow.Index].Cells[7];
                dgw.BeginEdit(true);
            }
            else if (dgw.CurrentCell.ColumnIndex == 7)
            {
                dgw.CurrentCell = dgw.Rows[dgw.CurrentRow.Index].Cells[8];
                dgw.BeginEdit(true);
            }
            else if (dgw.CurrentCell.ColumnIndex == 8)
            {
                dgw.CurrentCell = dgw.Rows[dgw.CurrentRow.Index].Cells[9];
                dgw.BeginEdit(true);
            }
            else if (dgw.CurrentCell.ColumnIndex == 9)
            {
                dgw.CurrentCell = dgw.Rows[dgw.CurrentRow.Index].Cells[11];
                dgw.BeginEdit(true);
            }
            else if (dgw.CurrentCell.ColumnIndex == 10)
            {
                dgw.CurrentCell = dgw.Rows[dgw.CurrentRow.Index].Cells[11];
                dgw.BeginEdit(true);
            }
            else if (dgw.CurrentCell.ColumnIndex == 11)
            {
                dgw.CurrentCell = dgw.Rows[dgw.CurrentRow.Index].Cells[12];
                dgw.BeginEdit(true);
            }
            else if (dgw.CurrentCell.ColumnIndex == 12)
            {
                tbSifraRobe.Text = "";
                tbSifraRobe.Focus();
            }

            Calculate();
        }

        /// <summary>
        /// Method used to calculate all values in current row after cell edit
        /// </summary>
        private void Calculate()
        {
            int currentRowIndex = dgw.CurrentRow.Index;

            // Get cell values
            if (!decimal.TryParse(dgw.Rows[currentRowIndex].Cells["kolicina"].FormattedValue.ToString(), out decimal kolicina))
            {
                dgw.Rows[currentRowIndex].Cells["kolicina"].Value = "1.00";
                MessageBox.Show("Greška kod upisa količine.", "Greška");
                return;
            }

            if (!decimal.TryParse(dgw.Rows[currentRowIndex].Cells["mpc"].FormattedValue.ToString(), out decimal mpc))
            {
                dgw.Rows[currentRowIndex].Cells["mpc"].Value = "0.00";
                MessageBox.Show("Greška kod upisa MPC.", "Greška");
                return;
            }

            if (!decimal.TryParse(dgw.Rows[currentRowIndex].Cells["vpc"].FormattedValue.ToString(), out decimal vpc))
            {
                dgw.Rows[currentRowIndex].Cells["vpc"].Value = "0.00";
                MessageBox.Show("Greška kod upisa VPC.", "Greška");
                return;
            }

            if (!decimal.TryParse(dgw.Rows[currentRowIndex].Cells["porez"].FormattedValue.ToString(), out decimal porez))
            {
                dgw.Rows[currentRowIndex].Cells["porez"].Value = "0.00";
                MessageBox.Show("Greška kod upisa poreza.", "Greška");
                return;
            }

            if (!decimal.TryParse(dgw.Rows[currentRowIndex].Cells["rabat"].FormattedValue.ToString(), out decimal rabat))
            {
                dgw.Rows[currentRowIndex].Cells["rabat"].Value = "0.00";
                MessageBox.Show("Greška kod upisa rabata.", "Greška");
                return;
            }

            // Calculate values
            decimal cijenaBezPdv, iznosBezPdv, rabatIznos, iznosUkupno; // Cijena = samo cijena bez količine, Iznos = Cijena * količina
            rabatIznos = vpc * (rabat / 100);
            cijenaBezPdv = vpc - rabatIznos;
            iznosBezPdv = cijenaBezPdv * kolicina;
            iznosUkupno = porez != 0 ? iznosBezPdv * (1 + (porez / 100)) : iznosBezPdv;

            // Set cell values
            dgw.Rows[currentRowIndex].Cells["kolicina"].Value = kolicina.ToString("#0.00");
            dgw.Rows[currentRowIndex].Cells["porez"].Value = porez.ToString("#0.00");
            dgw.Rows[currentRowIndex].Cells["mpc"].Value = mpc.ToString("#0.00");
            dgw.Rows[currentRowIndex].Cells["vpc"].Value = vpc.ToString("#0.00");
            dgw.Rows[currentRowIndex].Cells["rabat"].Value = rabat.ToString("#0.00");
            dgw.Rows[currentRowIndex].Cells["rabat_iznos"].Value = rabatIznos.ToString("#0.00");
            dgw.Rows[currentRowIndex].Cells["cijena_bez_pdv"].Value = cijenaBezPdv.ToString("#0.00");
            dgw.Rows[currentRowIndex].Cells["iznos_bez_pdv"].Value = iznosBezPdv.ToString("#0.00");
            dgw.Rows[currentRowIndex].Cells["iznos_ukupno"].Value = iznosUkupno.ToString("#0.00");
        }

        /// <summary>
        /// Method used to disable all Buttons, TextBoxes, etc.
        /// </summary>
        /// <param name="status">Enabled status</param>
        private void ItemsControl(bool status)
        {
            btnNoviUnos.Enabled = status;
            btnOdustani.Enabled = status;
            btnSpremi.Enabled = status;
            btnObrisi.Enabled = status;
            btnObrisiStavku.Enabled = status;

            dtpDatum.Enabled = status;

            pbTraziStavke.Enabled = status;
            pbTraziOdrediste.Enabled = status;

            nmGodina.Enabled = status;

            tbBrojUnosa.Enabled = status;
            tbPartnerSifra.Enabled = status;
            tbPartnerNaziv.Enabled = status;
            tbSifraRobe.Enabled = status;
            tbOriginalniDokument.Enabled = status;

            rtbNapomena.Enabled = status;

            cbMjestoTroska.Enabled = status;
            cbSkladiste.Enabled = status;

            dgw.Enabled = status;
        }

        /// <summary>
        /// Removes all items from DataGridView
        /// </summary>
        private void ClearGrid()
        {
            dgw.Rows.Clear();
            dgw.Refresh();
        }

        /// <summary>
        /// Clears textbox fields
        /// </summary>
        private void ClearFields()
        {
            tbPartnerSifra.Text = "";
            tbPartnerNaziv.Text = "";
            tbOriginalniDokument.Text = "";
            rtbNapomena.Text = "";
        }

        /// <summary>
        /// Method used to clear whole form and disable/enable needed buttons
        /// </summary>
        private void ClearForm()
        {
            ItemsControl(false);
            btnNoviUnos.Enabled = true;
            tbBrojUnosa.Enabled = true;
            tbBrojUnosa.ReadOnly = false;

            ClearGrid();
            ClearFields();
        }

        /// <summary>
        /// Opens articles (roba) selection menu
        /// </summary>
        private void SelectRoba()
        {
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            if (Convert.ToInt32(cbSkladiste.SelectedValue) != 0)
            {
                roba_trazi.idSkladiste = Convert.ToInt32(cbSkladiste.SelectedValue);
            }
            roba_trazi.ShowDialog();
            string roba_sifra = Properties.Settings.Default.id_roba.Trim();
            if (roba_sifra != "")
            {
                for (int y = 0; y < dgw.Rows.Count; y++)
                {
                    if (roba_sifra == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString())
                    {
                        MessageBox.Show("Artikl ili usluga već postoje!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string sql = "SELECT * FROM roba WHERE sifra='" + roba_sifra + "'";

                DataTable DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                    SetRoba(DTRoba);
                else
                    MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Opens partner selection menu
        /// </summary>
        private void SelectPartner()
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    tbPartnerSifra.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    tbPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        /// <summary>
        /// Method used to create/update "dobropis" in database and creates its items (dobropis_stavke)
        /// </summary>
        private void SaveDobropis()
        {
            decimal dobropisUkupno = 0;

            if (edit)
                DeleteDobropisStavke();

            // Save stavke
            foreach (DataGridViewRow row in dgw.Rows)
            {
                decimal.TryParse(row.Cells["iznos_ukupno"].Value.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal ukupnoIznos);
                string insertDobropisStavke = $@"INSERT INTO dobropis_stavke (id_dobropis, sifra_robe, jm, kolicina, porez, mpc, vpc, rabat, rabat_iznos, cijena_bez_pdv, iznos_bez_pdv, iznos_ukupno)
                        VALUES ({tbBrojUnosa.Text}
                                ,'{row.Cells["sifra"].Value.ToString()}'
                                ,'{row.Cells["jm"].Value.ToString()}'
                                ,{row.Cells["kolicina"].Value.ToString().Replace(',', '.')}
                                ,{row.Cells["porez"].Value.ToString().Replace(',', '.')}
                                ,{row.Cells["mpc"].Value.ToString().Replace(',', '.')}
                                ,{row.Cells["vpc"].Value.ToString().Replace(',', '.')}
                                ,{row.Cells["rabat"].Value.ToString().Replace(',', '.')}
                                ,{row.Cells["rabat_iznos"].Value.ToString().Replace(',', '.')}
                                ,{row.Cells["cijena_bez_pdv"].Value.ToString().Replace(',', '.')}
                                ,{row.Cells["iznos_bez_pdv"].Value.ToString().Replace(',', '.')}
                                ,{ukupnoIznos.ToString().Replace(',', '.')})";
                dobropisUkupno += ukupnoIznos;
                classSQL.insert(insertDobropisStavke);
            }

            // Save/update dobropis
            if (!edit)
            {
                string insertDobropis = $@"INSERT INTO dobropis (id_mjesto, id_partner, id_izradio, id_skladiste, broj_dobropis, datum, godina, mjesto_troska, originalni_dokument, napomena, ukupno)
                        VALUES ({cbMjestoTroska.SelectedValue}, {tbPartnerSifra.Text}, {idIzradio}, {cbSkladiste.SelectedValue}, {tbBrojUnosa.Text}, '{dtpDatum.Value.ToString("dd-MM-yyy HH:mm:ss")}', '{nmGodina.Value}', '{cbMjestoTroska.Text}', '{tbOriginalniDokument.Text}', '{rtbNapomena.Text}', {dobropisUkupno.ToString().Replace(',', '.')})";
                classSQL.insert(insertDobropis);
            }
            else
            {
                string updateDobropis = $@"UPDATE dobropis SET id_mjesto = {cbMjestoTroska.SelectedValue}, id_partner = {tbPartnerSifra.Text}, id_izradio = {idIzradio}, id_skladiste = {cbSkladiste.SelectedValue}, broj_dobropis = {tbBrojUnosa.Text}, datum = '{dtpDatum.Value.ToString("dd-MM-yyy HH:mm:ss")}', godina = '{nmGodina.Value}', mjesto_troska = '{cbMjestoTroska.Text}', originalni_dokument = '{tbOriginalniDokument.Text}', napomena = '{rtbNapomena.Text}', ukupno = {dobropisUkupno.ToString().Replace(',', '.')} WHERE broj_dobropis = '{tbBrojUnosa.Text}'";
                classSQL.update(updateDobropis);
            }

            // Clear form
            ClearForm();
            SetBrojDobropis();
        }

        /// <summary>
        /// Method used to "delete" "dobropis" and its items (stavke)
        /// </summary>
        private void DeleteDobropis()
        {
            // Dobropis
            string queryDobropis = $@"UPDATE dobropis SET id_mjesto = 0, id_partner = 0, id_izradio = 0, id_skladiste = 0, mjesto_troska = null, originalni_dokument = null, napomena = null, ukupno = 0 WHERE broj_dobropis = {tbBrojUnosa.Text}";
            classSQL.update(queryDobropis);

            // Dobropis stavke
            DeleteDobropisStavke();

            // Clear form
            ClearForm();
        }

        /// <summary>
        /// Used to delete "dobropis_stavke" for selected ID
        /// </summary>
        private void DeleteDobropisStavke()
        {
            classSQL.delete($"DELETE FROM dobropis_stavke WHERE id_dobropis = {tbBrojUnosa.Text}");
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            ItemsControl(true);
            edit = false;
        }

        /// <summary>
        /// Usually used when editing, fills all fields and data grid
        /// </summary>
        /// <param name="DTdobropis">DataTable with items</param>
        private void FillData(DataTable DTdobropis)
        {
            DataRow row = DTdobropis.Rows[0];
            DataTable DTpartner = classSQL.select($"SELECT id_partner, ime_tvrtke FROM partners WHERE id_partner = {Convert.ToInt32(row["id_partner"])}", "partners").Tables[0];
            DataTable DTzaposlenik = classSQL.select($"SELECT CONCAT(ime, ' ', prezime) AS naziv FROM zaposlenici WHERE id_zaposlenik = {(Convert.ToInt32(row["id_izradio"]).ToString() == "0" ? Class.Postavke.id_default_blagajnik : Convert.ToInt32(row["id_izradio"].ToString()))}", "zaposlenici").Tables[0];

            // Dobropis
            nmGodina.Value = Convert.ToInt32(row["godina"].ToString());
            cbSkladiste.SelectedValue = Convert.ToInt32(row["id_skladiste"]) == 0 ? Class.Postavke.id_default_skladiste : Convert.ToInt32(row["id_skladiste"]);
            if (DTpartner.Rows.Count > 0)
            {
                tbPartnerSifra.Text = DTpartner.Rows[0]["id_partner"].ToString() == null ? "" : DTpartner.Rows[0]["id_partner"].ToString();
                tbPartnerNaziv.Text = DTpartner.Rows[0]["ime_tvrtke"].ToString() == null ? "" : DTpartner.Rows[0]["ime_tvrtke"].ToString();
            }
            else
            {
                tbPartnerSifra.Text = "";
                tbPartnerNaziv.Text = "";
            }
            tbIzradio.Text = DTzaposlenik.Rows[0]["naziv"].ToString();
            dtpDatum.Value = DateTime.Parse(row["datum"].ToString());
            cbMjestoTroska.SelectedValue = Convert.ToInt32(row["id_mjesto"]) == 0 ? 1 : Convert.ToInt32(row["id_mjesto"]);
            tbOriginalniDokument.Text = row["originalni_dokument"].ToString() == null ? "" : row["originalni_dokument"].ToString();
            rtbNapomena.Text = row["napomena"].ToString() == null ? "" : row["napomena"].ToString();

            // Dobropis stavke
            string query = $@"SELECT dobropis_stavke.sifra_robe,
	                            roba.naziv,
	                            dobropis_stavke.jm,
	                            dobropis_stavke.kolicina,
	                            dobropis_stavke.porez,
	                            dobropis_stavke.vpc,
	                            dobropis_stavke.mpc,
	                            dobropis_stavke.rabat,
	                            dobropis_stavke.rabat_iznos,
	                            dobropis_stavke.cijena_bez_pdv,
	                            dobropis_stavke.iznos_bez_pdv,
	                            dobropis_stavke.iznos_ukupno
                            FROM dobropis_stavke
                            LEFT JOIN dobropis ON dobropis.broj_dobropis = dobropis_stavke.id_dobropis
                            LEFT JOIN roba ON roba.sifra = dobropis_stavke.sifra_robe
                            WHERE dobropis_stavke.id_dobropis = {row["broj_dobropis"].ToString()}";
            DataTable DTdobropisStavke = classSQL.select(query, "dobropis_stavke").Tables[0];
            if (DTdobropisStavke.Rows.Count > 0)
            {
                foreach (DataRow rowStavke in DTdobropisStavke.Rows)
                {
                    dgw.Rows.Add();
                    int rowIndex = dgw.Rows.Count - 1;
                    dgw.Rows[rowIndex].Cells["br"].Value = dgw.Rows.Count.ToString();

                    decimal.TryParse(rowStavke["porez"].ToString(), out decimal porez);
                    decimal.TryParse(rowStavke["mpc"].ToString(), out decimal mpc);
                    decimal.TryParse(rowStavke["vpc"].ToString(), out decimal vpc);
                    decimal.TryParse(rowStavke["kolicina"].ToString(), out decimal kolicina);
                    decimal.TryParse(rowStavke["rabat"].ToString(), out decimal rabat);
                    decimal.TryParse(rowStavke["rabat_iznos"].ToString(), out decimal rabatIznos);
                    decimal.TryParse(rowStavke["cijena_bez_pdv"].ToString(), out decimal cijenaBezPdv);
                    decimal.TryParse(rowStavke["iznos_bez_pdv"].ToString(), out decimal iznosBezPdv);
                    decimal.TryParse(rowStavke["iznos_ukupno"].ToString(), out decimal iznosUkupno);

                    //decimal mpc = vpc + (vpc * porez / 100);

                    dgw.Rows[rowIndex].Cells["sifra"].Value = rowStavke["sifra_robe"].ToString();
                    dgw.Rows[rowIndex].Cells["naziv_robe"].Value = rowStavke["naziv"].ToString();
                    dgw.Rows[rowIndex].Cells["jm"].Value = rowStavke["jm"].ToString();
                    dgw.Rows[rowIndex].Cells["kolicina"].Value = kolicina.ToString("#0.00");
                    dgw.Rows[rowIndex].Cells["porez"].Value = porez.ToString("#0.00");
                    dgw.Rows[rowIndex].Cells["mpc"].Value = mpc.ToString("#0.00");
                    dgw.Rows[rowIndex].Cells["vpc"].Value = vpc.ToString("#0.00");
                    dgw.Rows[rowIndex].Cells["rabat"].Value = rabat.ToString("#0.00");
                    dgw.Rows[rowIndex].Cells["rabat_iznos"].Value = rabatIznos.ToString("#0.00");
                    dgw.Rows[rowIndex].Cells["cijena_bez_pdv"].Value = cijenaBezPdv.ToString("#0.00");
                    dgw.Rows[rowIndex].Cells["iznos_bez_pdv"].Value = iznosBezPdv.ToString("#0.00");
                    dgw.Rows[rowIndex].Cells["iznos_ukupno"].Value = iznosUkupno.ToString("#0.00");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadDobropis()
        {
            DataTable DTdobropis = classSQL.select($"SELECT * FROM dobropis WHERE broj_dobropis = {tbBrojUnosa.Text}", "dobropis").Tables[0];
            if (DTdobropis.Rows.Count > 0)
            {
                edit = true;
                ClearForm();
                ItemsControl(true);
                FillData(DTdobropis);
                tbSifraRobe.Focus();
            }
            else
                MessageBox.Show("Robni dobropis pod upisanom šifrom ne postoji!", "Obavijest");
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Pritiskom na ovaj gumb poništavaju se sve izmjene. Jeste li sigurni da želite nastaviti?", "Upozorenje", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                edit = false;
                ItemsControl(false);
                SetBrojDobropis();
                ClearFields();
                ClearGrid();
                btnNoviUnos.Enabled = true;
                tbBrojUnosa.Enabled = true;
                tbBrojUnosa.ReadOnly = false;
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            if (dgw.Rows.Count > 0)
            {
                var confirmResult = MessageBox.Show("Jeste li sigurni da želite spremiti dobropis?", "Upozorenje", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                    SaveDobropis();
            }
            else
            {
                MessageBox.Show("Niste odabrali niti jednu stavku!", "Greška");
                return;
            }
        }

        private void PbTraziOdrediste_Click_1(object sender, EventArgs e)
        {
            SelectPartner();
        }

        private void TbSifraRobe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(tbSifraRobe.Text))
                    SelectRoba();
                else
                {
                    DataTable DTroba = Global.Database.GetRoba(tbSifraRobe.Text);
                    if (DTroba.Rows.Count > 0)
                        SetRoba(DTroba);
                    else
                        MessageBox.Show("Artikl za ovu šifru na postoji!", "Greška");
                }
            }
        }

        private void BtnObrisiStavku_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Jeste li sigurni da želite obrisati ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
        }

        private void TbBrojUnosa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                LoadDobropis();
        }

        private void BtnObrisi_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Jeste li sigurni da želite obrisati ovaj dobropis?", "Upozorenje", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
                DeleteDobropis();
        }

        private void BtnSviDobropisi_Click(object sender, EventArgs e)
        {
            frmSviRobniDobropisi form = new frmSviRobniDobropisi
            {
                Dock = DockStyle.Fill
            };
            form.ShowDialog();
            if (form.IdDobropis != 0)
            {
                tbBrojUnosa.Text = form.IdDobropis.ToString();
                LoadDobropis();
            }
        }

        private void TbPartnerSifra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(tbPartnerSifra.Text))
                    SelectPartner();
                else
                {
                    DataTable DTpartners = Global.Database.GetPartners(tbPartnerSifra.Text);
                    if (DTpartners.Rows.Count > 0)
                    {
                        tbPartnerSifra.Text = DTpartners.Rows[0]["id_partner"].ToString();
                        tbPartnerNaziv.Text = DTpartners.Rows[0]["ime_tvrtke"].ToString();
                    }
                    else
                        MessageBox.Show("Šifra ne postoji!", "Greška");
                }
            }
        }
    }
}
