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
    public partial class FrmVrstaUslugeDodaj : Form
    {
        int brojZapisa;
        public FrmVrstaUslugeDodaj(int brojZapisaDoSad)
        {
            brojZapisa = brojZapisaDoSad + 1;
            InitializeComponent();
        }

        private void FrmVrstaUslugeDodaj_Load(object sender, EventArgs e)
        {
            txtBoxId.Text = brojZapisa.ToString();
            //txtBoxId.Text = Global.Database.GetMaxBroj("vrsta_usluge", "id"); // Automatski ucitaj ID
        }


        //Klikom na button Spremi, provjeravaju se errori tipa: Jesu li ispunjena sva polja osim napomena.
        //Ukoliko jesu, spremi u bazu i refreshaj formu FrmVrstaUsluge
        //Ako nisu, izbaci error
        private void btnSpremi_Click(object sender, EventArgs e)
        {
            if (!CheckIfAllFieldsAreFilled())
                return;

            try
            {
                decimal insertIznos = CheckIfIznosIsNumber();
                //Ako je insertIznos 0, tada korisnik nije unio broj, već npr. text.
                if (insertIznos == 0)
                {
                    MessageBox.Show("Iznos treba biti broj.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                insertIznos = Math.Round(insertIznos, 2); // Ako korisnik unese broj kao 59.321, zaokružuje se na 59.32
                //Ako mi je iznos negativan ili 0, nema smisla unositi ga kao artikl
                if (insertIznos > 0)
                {
                    //Other values to insert:
                    int insertId = Int32.Parse(txtBoxId.Text);
                    string insertNazivUsluge = txtBoxNazivUsluge.Text;
                    string napomene = "";
                    napomene = richTextBoxNapomene.Text;

                    string sqlCmd = $"INSERT INTO vrsta_usluge(id,naziv_usluge,iznos,napomena,aktivnost) VALUES({insertId},'{insertNazivUsluge}',{insertIznos.ToString().Replace(',', '.')},'{napomene}',1)";
                    classSQL.insert(sqlCmd);
                    MessageBox.Show("Spremljeno.", "Obavijest", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                    MessageBox.Show("Iznos treba biti veći od 0.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
