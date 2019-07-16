using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PCPOS.Report.PopisGostiju;
using PCPOS.Entities;

namespace PCPOS.Resort
{
    public partial class FrmPopisGostiju : Form
    {
        public FrmPopisGostiju()
        {
            InitializeComponent();
        }

        private void FrmPopisGostiju_Load(object sender, EventArgs e)
        {
            AddItemsToComboBox();
            FillGrid();
            PostaviVrijednostRednogBroja();
        }

        #region Helper Methods
        //Refreshaj polja
        private void RefreshPolja()
        {
            textBoxPrezimeImeBroj.Text = null;
            textBoxPrezimeImeNaziv.Text = null;
            textBoxBrojOsobne.Text = null;
            textBoxBrojPutovnice.Text = null;
            comboBoxVrstaUsluge.SelectedIndex = -1;
            PostaviVrijednostRednogBroja();
        }

        //Postavi vrijednost rednog broja
        private void PostaviVrijednostRednogBroja()
        {
            textBoxRedniBroj.Text = (dataGridView.Rows.Count + 1).ToString();
        }

        //Dodaj iteme vrsta usluge u ComboBox
        private void AddItemsToComboBox()
        {
            DataTable DTUsluge = Global.Database.GetVrstaUsluge();
            foreach (DataRow row in DTUsluge.Rows)
            {
                comboBoxVrstaUsluge.Items.Add(row["naziv_usluge"].ToString());
            }
        }

        //Popuni datagridview podacima iz baze
        private void FillGrid()
        {
            if (dataGridView.Rows.Count > 0)
                RefreshGrid();

            DataTable DTpopisGostiju = Global.Database.GetPopisGostiju();
            if (DTpopisGostiju?.Rows.Count > 0)
            {
                foreach (DataRow row in DTpopisGostiju.Rows)
                {
                    int index = dataGridView.Rows.Add();
                    dataGridView.Rows[index].Cells["broj"].Value = index + 1;
                    dataGridView.Rows[index].Cells["id"].Value = row["id"].ToString();
                    dataGridView.Rows[index].Cells["prezimeime"].Value = row["prezime_ime"].ToString();
                    dataGridView.Rows[index].Cells["brojosobne"].Value = row["broj_osobne"].ToString();
                    dataGridView.Rows[index].Cells["brojputovnice"].Value = row["broj_putovnice"].ToString();
                    dataGridView.Rows[index].Cells["vrstapruzeneusluge"].Value = row["vrsta_pružene_usluge"].ToString();
                    dataGridView.Rows[index].Cells["datumpocetka"].Value = row["datum_pocetka_pruzanja_usluge"].ToString();
                    dataGridView.Rows[index].Cells["datumkraja"].Value = row["datum_prestanka_pruzanja_usluge"].ToString();
                    dataGridView.Rows[index].Cells["primjedba"].Value = row["primjedba"].ToString();
                }
            }
        }

        private void RefreshGrid()
        {
            dataGridView.Rows.Clear();
            dataGridView.Refresh();
        }

        //Otvara prozor za odabir partnera
        private void SelectPartner()
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataTable DTpartner = classSQL.select($"SELECT * FROM partners WHERE id_partner ='{Properties.Settings.Default.id_partner}'", "partners").Tables[0];
                if (DTpartner?.Rows.Count > 0)
                {
                    textBoxPrezimeImeBroj.Text = DTpartner.Rows[0]["id_partner"].ToString();
                    textBoxPrezimeImeNaziv.Text = DTpartner.Rows[0]["ime_tvrtke"].ToString();
                }
            }
        }
        #endregion

        #region EventHandlers
        //Upiše se šifra partnera te pritisne enter, a sljedeći textbox izbacuje ime partnera
        //Event Handlers
        private void textBoxPrezimeImeBroj_KeyDown(object sender, KeyEventArgs e)
        {
            //Ako se pritisne enter i nije uneseno niti jedan broj
            if (e.KeyCode == Keys.Enter && string.IsNullOrWhiteSpace(textBoxPrezimeImeBroj.Text))
                SelectPartner();

            //Ako se pritisne enter i unesen je neki broj
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(textBoxPrezimeImeBroj.Text))
            {
                try
                {
                    DataTable DTPartner = Global.Database.GetPartners(textBoxPrezimeImeBroj.Text);
                    if (DTPartner?.Rows.Count > 0)
                    {
                        textBoxPrezimeImeBroj.Text = DTPartner.Rows[0]["id_partner"].ToString();
                        textBoxPrezimeImeNaziv.Text = DTPartner.Rows[0]["ime_tvrtke"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Partner s tim ID-om ne postoji.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


        //Buttons
        //Dodaj
        private void buttonDodaj_Click(object sender, EventArgs e)
        {
            if (!CheckIfPrezimeImeEntered())
                return;
            if (!CheckIfOsobnaOrPutovnicaEntered())
                return;
            if (!CheckIfVrstaUslugeSelected())
                return;
            if (!CheckIfDatumDolaskaManjiOdDatumOdlaska())
                return;

            string sqlCmd = $@"INSERT INTO popisgostiju(id,broj,prezime_ime,broj_osobne,broj_putovnice,vrsta_pružene_usluge,
                            datum_pocetka_pruzanja_usluge,datum_prestanka_pruzanja_usluge,primjedba) 
                            VALUES(DEFAULT,{textBoxRedniBroj.Text},'{textBoxPrezimeImeNaziv.Text}','{textBoxBrojOsobne.Text}',
                            '{textBoxBrojPutovnice.Text}','{comboBoxVrstaUsluge.GetItemText(comboBoxVrstaUsluge.SelectedItem)}',
                            '{dateTimePickerDatumPocetka.Value.ToString("yyyy-MM-dd")}',
                            '{dateTimePickerDatumKraja.Value.ToString("yyyy-MM-dd")}','{richTextBoxNapomena.Text}');";
            try
            {
                classSQL.insert(sqlCmd);
                MessageBox.Show("Spremljeno.", "Obavijest", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FillGrid();
                RefreshPolja();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom unosa u bazu podataka.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Uredi
        private void buttonUredi_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView.Rows.Count == 0)
                {
                    MessageBox.Show("Nema retka za uređivanje.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    FrmPopisGostijuUredi form = new FrmPopisGostijuUredi(dataGridView.Rows[dataGridView.CurrentCell.RowIndex].Cells["broj"].Value.ToString(),
                                                                         dataGridView.Rows[dataGridView.CurrentCell.RowIndex].Cells["id"].Value.ToString()); // Argument je redni broj iz dataGridViewa i 
                    form.ShowDialog();
                    FillGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ovaj redak se ne može urediti.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Obrisi
        private void buttonObrisi_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView.Rows.Count == 0)
                {
                    MessageBox.Show("Nema redaka za obrisati.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string sqlCmd = $"DELETE FROM popisgostiju WHERE id={dataGridView.Rows[dataGridView.CurrentCell.RowIndex].Cells["id"].Value.ToString()}";
                    classSQL.delete(sqlCmd);
                    FillGrid(); // Refresh dataGridView
                    RefreshPolja(); // refresha textboxove za unos i redni broj
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ovaj redak se ne može obrisati.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Checks
        private bool CheckIfPrezimeImeEntered()
        {
            if (string.IsNullOrWhiteSpace(textBoxPrezimeImeBroj.Text) || string.IsNullOrWhiteSpace(textBoxPrezimeImeNaziv.Text))
            {
                MessageBox.Show("Potrebno je unijeti ime i prezime. (ID partnera)", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return true;
        }

        private bool CheckIfOsobnaOrPutovnicaEntered()
        {
            if (string.IsNullOrWhiteSpace(textBoxBrojOsobne.Text) && string.IsNullOrWhiteSpace(textBoxBrojPutovnice.Text))
            {
                MessageBox.Show("Potreban je broj osobne ili broj putovnice.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool CheckIfVrstaUslugeSelected()
        {
            if (comboBoxVrstaUsluge.SelectedIndex == -1)
            {
                MessageBox.Show("Potrebno je odabrati vrstu usluge.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool CheckIfDatumDolaskaManjiOdDatumOdlaska()
        {
            if (dateTimePickerDatumPocetka.Value > dateTimePickerDatumKraja.Value.AddMinutes(5.0))
            {
                MessageBox.Show("Datum kraja mora biti veći ili jednak datumu početka.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        #endregion
        private void buttonIspisi_Click(object sender, EventArgs e)
        {
            //Provjeravamo postoji li bilo koji zapis u popisu gostiju. 
            int dataRowCount = dataGridView.Rows.Count;
            if (dataRowCount == 0)
            {
                MessageBox.Show("Nema zapisa za printanje.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int selectedRow = dataGridView.CurrentCell.RowIndex;
            Gost gost = new Gost
            {           
                //Preuzimanje podataka iz dataGridViewa i pridruživanje istih u objekt gost
                broj = Int32.Parse(dataGridView["broj", selectedRow].Value.ToString()),
                imePrezime = dataGridView["prezimeime", selectedRow].Value.ToString(),
                brojOsobne = dataGridView["brojosobne", selectedRow].Value.ToString(),
                brojPutovnice = dataGridView["brojputovnice", selectedRow].Value.ToString(),
                vrstaPruzeneUsluge = dataGridView["vrstapruzeneusluge", selectedRow].Value.ToString(),
                datumPocetka = dataGridView["datumpocetka", selectedRow].Value.ToString(),
                datumKraja = dataGridView["datumkraja", selectedRow].Value.ToString(),
                primjedba = dataGridView["primjedba", selectedRow].Value.ToString()
            };

            FrmPopisGostijuReport frmPopisGostijuReport = new FrmPopisGostijuReport(gost);
            frmPopisGostijuReport.ShowDialog();
        }

        private void buttonIspisiSve_Click(object sender, EventArgs e)
        {
            //Provjeravamo postoji li bilo koji zapis u popisu gostiju. 
            int dataRowCount = dataGridView.Rows.Count;
            if (dataRowCount == 0)
            {
                MessageBox.Show("Nema zapisa za printanje.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Stvaramo onoliko objekata klase Gost koliko je redova
            Gost[] gosti = new Gost[dataRowCount]; // Zauzimanje mjesta u memoriji
            for(int i = 0; i < dataRowCount; i++)
            {
                //Preuzimanje podataka iz dataGridViewa i pridruživanje istih u objekt gost
                gosti[i] = new Gost(); // Stvaranje objekta u memoriji
                gosti[i].broj = Int32.Parse(dataGridView["broj", i].Value.ToString());
                gosti[i].imePrezime = dataGridView["prezimeime", i].Value.ToString();
                gosti[i].brojOsobne = dataGridView["brojosobne", i].Value.ToString();
                gosti[i].brojPutovnice = dataGridView["brojputovnice", i].Value.ToString();
                gosti[i].vrstaPruzeneUsluge = dataGridView["vrstapruzeneusluge", i].Value.ToString();
                gosti[i].datumPocetka = dataGridView["datumpocetka", i].Value.ToString();
                gosti[i].datumKraja = dataGridView["datumkraja", i].Value.ToString();
                gosti[i].primjedba = dataGridView["primjedba", i].Value.ToString();
            }

            FrmPopisGostijuReport form = new FrmPopisGostijuReport(gosti);
            form.ShowDialog();
        }
    }
}
