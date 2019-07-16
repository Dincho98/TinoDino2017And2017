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
    public partial class FrmVrstaUsluge : Form
    {
        public FrmVrstaUsluge()
        {
            InitializeComponent();
        }

        private void FrmVrstaUsluge_Load(object sender, EventArgs e)
        {
            FillGrid();
        }


        private void FillGrid()
        {
            if (dataGridView.Rows.Count > 0)
                RefreshGrid();

            DataTable DTvrstaUsluge = Global.Database.GetVrstaUsluge();
            if (DTvrstaUsluge?.Rows.Count > 0)
            {
                foreach (DataRow row in DTvrstaUsluge.Rows)
                {
                    int index = dataGridView.Rows.Add();

                    dataGridView.Rows[index].Cells["broj"].Value = index + 1;
                    dataGridView.Rows[index].Cells["naziv"].Value = row["naziv_usluge"].ToString();
                    dataGridView.Rows[index].Cells["iznos"].Value = row["iznos"].ToString();
                    dataGridView.Rows[index].Cells["napomena"].Value = row["napomena"].ToString();
                    dataGridView.Rows[index].Cells["id"].Value = row["id"].ToString();
                }
            }
        }

        private void RefreshGrid()
        {
            dataGridView.Rows.Clear();
            dataGridView.Refresh();
        }

        //Dodati novi uslugu u dataGridView i bazu
        private void btnDodaj_Click(object sender, EventArgs e)
        {
            int brojZapisaDoSad = dataGridView.Rows.Count;
            FrmVrstaUslugeDodaj frmVrstaUslugeDodaj = new FrmVrstaUslugeDodaj(brojZapisaDoSad);
            frmVrstaUslugeDodaj.ShowDialog();
            FillGrid(); // Refresh dataGridView
        }

        //Uredi novi uslugu u dataGridView i bazi
        private void btnUredi_Click(object sender, EventArgs e)
        {
            int idArtiklaIzBaze = Int32.Parse(dataGridView.Rows[dataGridView.CurrentCell.RowIndex].Cells["id"].Value.ToString());
            int brojArtiklaIzTablice= Int32.Parse(dataGridView.Rows[dataGridView.CurrentCell.RowIndex].Cells["broj"].Value.ToString());

            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("Nema retka za uređivanje.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                FrmVrstaUslugeUredi frmVrstaUslugeUredi = new FrmVrstaUslugeUredi(idArtiklaIzBaze, brojArtiklaIzTablice);
                frmVrstaUslugeUredi.ShowDialog();
                FillGrid(); // Refresh dataGridView
            }
        }

        //Izbrisi novu uslugu iz dataGridView i baze
        private void btnIzbrisi_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView.Rows.Count == 0)
                {
                    MessageBox.Show("Nema brisanje za uređivanje.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string sqlCmd = $"DELETE FROM vrsta_usluge WHERE id= {GetSelectedServiceTypeId()}";
                    classSQL.delete(sqlCmd);
                    FillGrid(); // Refresh dataGridView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not delete this cell.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Vraća ID(koji odgovara onome u bazi) od trenutnog row-a koji je trenutno selectan
        private string GetSelectedServiceTypeId()
        {
            return dataGridView.Rows[dataGridView.CurrentCell.RowIndex].Cells["id"].Value.ToString();
        }
    }
}
