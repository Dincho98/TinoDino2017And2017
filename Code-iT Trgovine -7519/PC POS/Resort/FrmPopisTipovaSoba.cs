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
    public partial class FrmPopisTipovaSoba : Form
    {
        public FrmPopisTipovaSoba()
        {
            InitializeComponent();
        }

        private void FrmPopisTipovaSoba_Load(object sender, EventArgs e)
        {
            FillGrid();
        }

        /// <summary>
        /// Used to populate grid with room type (tip_sobe) data
        /// </summary>
        private void FillGrid()
        {
            if (dataGridView.Rows.Count > 0)
                RefreshGrid();

            DataTable DTtipSobe = Global.Database.GetTipSobe();
            if (DTtipSobe?.Rows.Count > 0)
            {
                foreach (DataRow row in DTtipSobe.Rows)
                {
                    int index = dataGridView.Rows.Add();

                    dataGridView.Rows[index].Cells["broj"].Value = index + 1;
                    dataGridView.Rows[index].Cells["naziv"].Value = row["tip"].ToString();
                    dataGridView.Rows[index].Cells["id"].Value = row["id"].ToString();
                }
            }
        }

        /// <summary>
        /// Clears all rows in grid
        /// </summary>
        private void RefreshGrid()
        {
            dataGridView.Rows.Clear();
            dataGridView.Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edit"></param>
        private void RoomType(bool edit)
        {
            FrmTipSobe form = new FrmTipSobe
            {
                EditMode = edit,
                BrojSobe = edit ? GetSelectedTypeId() : "0"
            };
            form.ShowDialog();
            if (form.Successful)
                FillGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        private void DeleteType()
        {
            try
            {
                string query = $@"DELETE FROM tip_sobe WHERE id = {GetSelectedTypeId()}";
                classSQL.delete(query);
                FillGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetSelectedTypeId()
        {
            return dataGridView.Rows[dataGridView.CurrentCell.RowIndex].Cells["id"].Value.ToString();
        }

        private void BtnDodaj_Click(object sender, EventArgs e)
        {
            RoomType(false);
        }

        private void BtnUredi_Click(object sender, EventArgs e)
        {
            RoomType(true);
        }

        private void BtnObrisi_Click(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count > 0)
            {
                var confirmDialog = MessageBox.Show("Jeste li sigurni da želite obrisati odabrani tip sobe?", "Obavijest", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmDialog == DialogResult.Yes)
                    DeleteType();
            }
        }
    }
}
