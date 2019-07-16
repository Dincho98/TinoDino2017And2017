using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCPOS.Resort
{
    public partial class FrmVrstaUslugeUredi : Form
    {
        //Vrijednosti koje se inicijaliziraju u konstruktoru kada se otvori ova forma
        int idArtiklaZaUrediti = 0;
        int brojArtiklaZaUrediti = 0;

        public FrmVrstaUslugeUredi(int idArtiklaIzBaze, int brojArtiklaIzTablice)
        {
            InitializeComponent();
            idArtiklaZaUrediti = idArtiklaIzBaze;
            brojArtiklaZaUrediti = brojArtiklaIzTablice;

        }

        private void FrmVrstaUslugeUredi_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // Prilikom otvaranje forme, potrebno je ucitati trenutne podatke koji su već u tablici.
        private void LoadData()
        {
            DataTable DTsoba = Global.Database.GetVrstaUsluge(idArtiklaZaUrediti.ToString());
            if (DTsoba?.Rows.Count > 0)
            {
                DataRow row = DTsoba.Rows[0];
                txtBoxBroj.Text = brojArtiklaZaUrediti.ToString();
                txtBoxNazivUsluge.Text = row["naziv_usluge"].ToString();
                txtBoxIznos.Text = row["iznos"].ToString();
                richTextBoxNapomene.Text = row["napomena"].ToString();
            }
        }

        //Kada se pritisne klik na "Uredi", potrebno je updateati podatke u bazi podataka i dataGridViewu
        private void btnUredi_Click(object sender, EventArgs e)
        {
            //Sva polja trebaju biti popunjena
            if (!CheckIfAllFieldsAreFilled())
                return;

            decimal updateIznos = CheckIfIznosIsNumber();
            //Ako je updateIznos 0, tada korisnik nije unio broj, već npr. text.
            if (updateIznos == 0)
            {
                MessageBox.Show("Iznos treba biti broj.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            updateIznos = Math.Round(updateIznos, 2); // Ako korisnik unese broj kao 59.321, zaokružuje se na 59.32
            //Ako mi je iznos >0, ima smisla provjeravati
            if (updateIznos > 0)
            {
                try
                {
                    string sqlCmd = $@"UPDATE vrsta_usluge SET naziv_usluge='{txtBoxNazivUsluge.Text}',
                                iznos={updateIznos.ToString().Replace(',', '.')},
                                napomena='{richTextBoxNapomene.Text}'
                             WHERE id={idArtiklaZaUrediti}";
                    classSQL.insert(sqlCmd);
                    MessageBox.Show("Spremljeno.", "Obavijest", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška u uređivanju!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Iznos treba biti veći od 0.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        #region Checks
        private bool CheckIfAllFieldsAreFilled()
        {
            if (string.IsNullOrWhiteSpace(txtBoxNazivUsluge.Text) || string.IsNullOrWhiteSpace(txtBoxIznos.Text))
            {
                MessageBox.Show("Naziv usluge i iznos trebaju biti popunjeni.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        //Pokušava pretvoriti iznos koji je korisnik unio sa zarezom u iznos s točkom zbog toga što se tako zapisuje u postgres bazi.
        //Ako ne uspije, tada se vraća 0, tj. korisnik nije unio broj, već npr. text.
        private decimal CheckIfIznosIsNumber()
        {
            if (decimal.TryParse(txtBoxIznos.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal iznos))
            {
                return iznos;
            }
            return 0;
        }
        #endregion
    }
}
