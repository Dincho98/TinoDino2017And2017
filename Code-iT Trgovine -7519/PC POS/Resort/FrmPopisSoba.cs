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
    public partial class FrmPopisSoba : Form
    {
        public FrmPopisSoba()
        {
            InitializeComponent();
        }

        private void FrmSobe_Load(object sender, EventArgs e)
        {
            FillGrid();
        }

        /// <summary>
        /// Used to fill grid with room data
        /// </summary>
        private void FillGrid()
        {
            if (dataGridView.Rows.Count > 0)
                RefreshGrid();

            DataTable DTsobe = Global.Database.GetSobe();
            if (DTsobe?.Rows.Count > 0)
            {
                foreach (DataRow row in DTsobe.Rows)
                {
                    int index = dataGridView.Rows.Add();

                    dataGridView.Rows[index].Cells["broj"].Value = index + 1;
                    dataGridView.Rows[index].Cells["naziv"].Value = row["naziv_sobe"].ToString();
                    dataGridView.Rows[index].Cells["tip_sobe"].Value = Global.Database.GetTipSobe(row["id_tip_sobe"].ToString())?.Rows[0]["tip"].ToString();
                    dataGridView.Rows[index].Cells["broj_lezaja"].Value = row["broj_lezaja"].ToString();
                    dataGridView.Rows[index].Cells["cijena"].Value = row["cijena_nocenja"].ToString() + " kn";
                    dataGridView.Rows[index].Cells["id"].Value = row["broj_sobe"].ToString();
                }
            }
        }

        /// <summary>
        /// Clears grid
        /// </summary>
        private void RefreshGrid()
        {
            dataGridView.Rows.Clear();
            dataGridView.Refresh();
        }

        /// <summary>
        /// Used to edit selected room
        /// </summary>
        /// <param name="edit">Edit mode</param>
        private void Soba(bool edit, int brojSobe = 0)
        {
            FrmSoba form = new FrmSoba
            {
                BrojSobe = brojSobe,
                EditMode = edit
            };
            form.ShowDialog();
            if (form.Successful)
            {
                // Kada unesemo jedan zapis, potreban je refresh bez zatvaranja forme FrmSoba i FrmPopisSoba
                FillGrid();
                Soba(false); 
            }
        }

        /// <summary>
        /// Used to delete selected room
        /// </summary>
        private void DeleteRoom()
        {
            try
            {
                string query = $@"DELETE FROM sobe WHERE broj_sobe = {GetSelectedRoomId()}";
                classSQL.delete(query);
                FillGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Returns Id (broj_sobe) of selected room in grid
        /// </summary>
        /// <returns></returns>
        private string GetSelectedRoomId()
        {
            int rowIndex = dataGridView.CurrentCell.RowIndex;
            return dataGridView.Rows[rowIndex].Cells["id"].Value.ToString();
        }

        private void BtnNovaSoba_Click(object sender, EventArgs e)
        {
            Soba(false);
        }

        private void BtnUrediSobu_Click(object sender, EventArgs e)
        {
            Soba(true, Convert.ToInt32(GetSelectedRoomId()));
        }

        private void BtnObrisiSobu_Click(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count > 0)
            {
                var confirmDialog = MessageBox.Show("Jeste li sigurni da želite obrisati odabranu sobu?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmDialog == DialogResult.Yes)
                    DeleteRoom();
            }
        }

        private void BtnTipSobe_Click(object sender, EventArgs e)
        {
            FrmPopisTipovaSoba form = new FrmPopisTipovaSoba();
            form.ShowDialog();
        }
    }
}
