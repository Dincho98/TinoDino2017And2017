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
    public partial class FrmAgencijeUredi : Form
    {
        string idAgencije;
        string redniBroj;

        //Stringovi su argumenti konstruktora zbog toga što nema potrebe za castanjem i trošenjem vremena procesora.
        public FrmAgencijeUredi(string id, string broj)
        {
            InitializeComponent();
            idAgencije = id;
            redniBroj = broj;
            UcitajComboBox();

        }

        private void FrmAgencijeUredi_Load(object sender, EventArgs e)
        {
            //Preuzmi informacije o agenciji iz baze
            DataTable DTAgencije = Global.Database.GetAgencija(idAgencije.ToString());
            //Postavi vrijednosti o agenciji u formu
            textBoxBroj.Text = redniBroj;
            textBoxImeAgencije.Text = DTAgencije.Rows[0]["ime_agencije"].ToString();
            comboBoxAktivnost.SelectedIndex = SelectIndexInComboBox(DTAgencije.Rows[0]["aktivnost"].ToString());
            richTextBoxNapomena.Text = DTAgencije.Rows[0]["napomena"].ToString();
        }





        //Ucitava moguće aktivnosti u combo 
        private void UcitajComboBox()
        {
            comboBoxAktivnost.Items.Add("1 - Aktivna");
            comboBoxAktivnost.Items.Add("2 - Neaktivna");
            comboBoxAktivnost.SelectedIndex = 0; // DEFAULT SELECT
        }

        //Provjerava je li textbox "Ime agencije" prazan
        private bool CheckIfNameIsEmpty()
        {
            if (string.IsNullOrWhiteSpace(textBoxImeAgencije.Text))
            {
                MessageBox.Show("Niste unijeli ime agencije.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            return false;
        }

        //Postavlja sektirani index u comboBoxu
        private int SelectIndexInComboBox(string value)
        {
            if (value == "1")
                return 0;
            return 1;
        }

        //Klik na button Uredi
        private void buttonUredi_Click(object sender, EventArgs e)
        {
            if (CheckIfNameIsEmpty())
                return;

            try
            {
                string sqlCmd = $@"UPDATE agencija SET ime_agencije='{textBoxImeAgencije.Text}',napomena='{richTextBoxNapomena.Text}',
                                                aktivnost={(comboBoxAktivnost.SelectedIndex == 0 ? 1 : 2)} WHERE id={idAgencije}";
                classSQL.update(sqlCmd);
                MessageBox.Show("Spremljeno", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Uređivanje poništeno.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}