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
    public partial class FrmAgencije : Form
    {
        public FrmAgencije()
        {
            InitializeComponent();
        }

        private void FrmAgencije_Load(object sender, EventArgs e)
        {
            UcitajComboBox();
            FillDataGridView();
            OdaberiRedniBroj(); // Mora biti nakon FillDataGridView() jer na temelju njega izračunava redni broj
        }



        //Ucitava moguće aktivnosti u combo 
        private void UcitajComboBox()
        {
            comboBoxAktivnost.Items.Add("1 - Aktivna");
            comboBoxAktivnost.Items.Add("2 - Neaktivna");
            comboBoxAktivnost.SelectedIndex = 0; // DEFAULT SELECT
        }

        //Automatski postavlja redni broj zapisa u datagridviewu
        private void OdaberiRedniBroj()
        {
            int brojevaUDataGridViewu = dataGridView.Rows.Count;
            textBoxBroj.Text = (brojevaUDataGridViewu+1).ToString();
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

        #region EventHandlers

        private void buttonDodaj_Click(object sender, EventArgs e)
        {
            //Check
            if (CheckIfNameIsEmpty())
                return;

            //Unos u bazu podataka
            try
            {
                string sqlQuery = $@"INSERT INTO agencija VALUES(DEFAULT,'{textBoxImeAgencije.Text}','{richTextBoxNapomena.Text}',
                            {(comboBoxAktivnost.SelectedItem.ToString() == "1 - Aktivna" ? 1 : 2)})";
                classSQL.insert(sqlQuery);

                MessageBox.Show("Spremljeno.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Greška prilikom unosa u bazu podataka.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Refreshaj dataGridView
            FillDataGridView();
            //Restartaj polja
            RestartPolja();
        }


        private void buttonUredi_Click(object sender, EventArgs e)
        {
            //Ako nema redaka u dataGridViewu
            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("Nema redaka za uređivanje.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Postupak uređivanja
            //Odaberi id trenutno označenog retka
            int currentlySelectedRow = dataGridView.CurrentCell.RowIndex;
            FrmAgencijeUredi form = new FrmAgencijeUredi(dataGridView.Rows[currentlySelectedRow].Cells["id"].Value.ToString(),
                                                        dataGridView.Rows[currentlySelectedRow].Cells["broj"].Value.ToString());
            form.ShowDialog();
            FillDataGridView();
        }

        private void buttonIzbrisi_Click(object sender, EventArgs e)
        {
            try
            {
                //Ako nema redaka u dataGridViewu
                if (dataGridView.Rows.Count == 0){
                    MessageBox.Show("Nema redaka za obrisati.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Postupak brisanja
                int currentlySelectedRow = dataGridView.CurrentCell.RowIndex;
                string id = dataGridView["id", currentlySelectedRow].Value.ToString() ;
                string sqlCmd = "DELETE FROM agencija WHERE id=" + id;
                classSQL.delete(sqlCmd);
                MessageBox.Show("Agencija obrisana.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FillDataGridView(); // Nakon brisanja, potrebno je refreshati dataGridView
                RestartPolja(); // Kako bi se updateao redni broj
            }catch(Exception ex)
            {
                MessageBox.Show("Greška prilikom brisanja.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        //Restart polja se koristi nakon što korisnik pritisne na gumb "Dodaj"
        private void RestartPolja()
        {
            OdaberiRedniBroj();
            textBoxImeAgencije.Text = null;
            comboBoxAktivnost.SelectedIndex = 0;
            richTextBoxNapomena.Text = null;
        }

        //Punjenje dataGridViewa se koristi kada se forma učita/uredi/doda nova agencija
        private void FillDataGridView()
        {
            //Ako mi već postoje zapisi, obrisi ih
            if (dataGridView.Rows.Count > 0)
                RefreshGrid();

            //Preuzmi agencije iz baze
            DataTable DTAgencije = Global.Database.GetAgencija();
            if (DTAgencije.Rows.Count > 0)
            {
                foreach (DataRow row in DTAgencije.Rows)
                {
                    int index = dataGridView.Rows.Add();

                    dataGridView.Rows[index].Cells["id"].Value = row["id"].ToString();
                    dataGridView.Rows[index].Cells["broj"].Value = index + 1;
                    dataGridView.Rows[index].Cells["imeAgencije"].Value = row["ime_agencije"].ToString();
                    dataGridView.Rows[index].Cells["aktivnost"].Value = row["aktivnost"].ToString();
                    dataGridView.Rows[index].Cells["napomena"].Value = row["napomena"].ToString();
                }
            }
        }

        private void RefreshGrid()
        {
            dataGridView.Rows.Clear();
            dataGridView.Refresh();
        }

    }
}
