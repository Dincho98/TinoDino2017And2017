using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCPOS.Resort
{
    public partial class FrmPopisGostijuUredi : Form
    {
        int idPopisaGostiju;
        public FrmPopisGostijuUredi(string redniBroj, string idPopisGostiju)
        {
            InitializeComponent();
            idPopisaGostiju = Int32.Parse(idPopisGostiju);
            AddItemsToComboBox();
            PopuniTextBoxove(redniBroj, idPopisGostiju);
        }


        private void FrmPopisGostijuUredi_Load(object sender, EventArgs e)
        {

        }

        private void PopuniTextBoxove(string redniBroj, string idPopisGostiju)
        {
            textBoxRedniBroj.Text = redniBroj;
            DataTable DTPopisGostiju = Global.Database.GetPopisGostiju(idPopisGostiju);
            if (DTPopisGostiju?.Rows.Count > 0)
            {
                textBoxPrezimeImeBroj.Text = BrojPartnera(DTPopisGostiju.Rows[0]["prezime_ime"].ToString()); // Pronadji  ID partnera po imenu partnera
                textBoxPrezimeImeNaziv.Text = DTPopisGostiju.Rows[0]["prezime_ime"].ToString();
                textBoxBrojOsobne.Text = DTPopisGostiju.Rows[0]["broj_osobne"].ToString();
                textBoxBrojPutovnice.Text = DTPopisGostiju.Rows[0]["broj_putovnice"].ToString();
                comboBoxVrstaUsluge.SelectedIndex = comboBoxVrstaUsluge.Items.IndexOf(DTPopisGostiju.Rows[0]["vrsta_pružene_usluge"].ToString());
                dateTimePickerDatumPocetka.Value = DateTime.Parse(DTPopisGostiju.Rows[0]["datum_pocetka_pruzanja_usluge"].ToString());
                dateTimePickerDatumKraja.Value = DateTime.Parse(DTPopisGostiju.Rows[0]["datum_prestanka_pruzanja_usluge"].ToString());
                richTextBoxNapomena.Text = DTPopisGostiju.Rows[0]["primjedba"].ToString();
            }
        }

        #region Helper Methods
        //Dodaj iteme vrsta usluge u ComboBox
        private void AddItemsToComboBox()
        {
            DataTable DTUsluge = Global.Database.GetVrstaUsluge();
            foreach (DataRow row in DTUsluge.Rows)
            {
                comboBoxVrstaUsluge.Items.Add(row["naziv_usluge"].ToString());
            }
        }

        //Vrati id partnera prema imenu
        private string BrojPartnera(string idPopisGostiju)
        {
            DataTable DTBrojPartnera = Global.Database.GetPartnerByName(idPopisGostiju);
            return DTBrojPartnera.Rows[0]["id_partner"].ToString();
        }
        #endregion

        #region EventHandlers
        //Kad se pritisne enter, tada se prikazuje i ime samog partnera
        private void textBoxPrezimeImeBroj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
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

        private void buttonUredi_Click(object sender, EventArgs e)
        {
            if (!CheckIfPrezimeImeEntered())
                return;
            if (!CheckIfOsobnaOrPutovnicaEntered())
                return;
            if (!CheckIfVrstaUslugeSelected())
                return;
            if (!CheckIfDatumDolaskaManjiOdDatumOdlaska())
                return;

            string sqlCmd = $@"UPDATE popisgostiju SET prezime_ime='{textBoxPrezimeImeNaziv.Text}',broj_osobne='{textBoxBrojOsobne.Text}',
                        broj_putovnice='{textBoxBrojPutovnice.Text}',vrsta_pružene_usluge='{comboBoxVrstaUsluge.GetItemText(comboBoxVrstaUsluge.SelectedItem)}',
                        datum_pocetka_pruzanja_usluge='{dateTimePickerDatumPocetka.Value.ToString("yyyy-MM-dd")}',
                        datum_prestanka_pruzanja_usluge='{dateTimePickerDatumKraja.Value.ToString("yyyy-MM-dd")}',
                        primjedba='{richTextBoxNapomena.Text}' WHERE id={idPopisaGostiju}";
            try
            {
                classSQL.insert(sqlCmd);
                MessageBox.Show("Spremljeno.", "Obavijest", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom unosa u bazu podataka.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (dateTimePickerDatumPocetka.Value > dateTimePickerDatumKraja.Value)
            {
                MessageBox.Show("Datum kraja mora biti veći od datuma početka.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        #endregion

    }
}
