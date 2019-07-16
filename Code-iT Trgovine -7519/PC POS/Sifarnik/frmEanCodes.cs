using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class FrmEanCodes : Form
    {
        public List<string> EanCodes { get; set; }

        public FrmEanCodes()
        {
            InitializeComponent();
        }

        private void FrmEanCodes_Load(object sender, EventArgs e)
        {
            LoadEanCodes();
        }

        /// <summary>
        /// Loads Ean codes from list and populates dataGridView
        /// </summary>
        private void LoadEanCodes()
        {
            if (EanCodes.Count > 0)
            {
                foreach (string code in EanCodes)
                    AddCodeToGrid(code);
            }
        }

        /// <summary>
        /// Adds item to dataGridView with given code
        /// </summary>
        /// <param name="code"></param>
        private void AddCodeToGrid(string code)
        {
            int index = dataGridView.Rows.Add();
            dataGridView.Rows[index].Cells["barcode"].Value = code;
        }

        /// <summary>
        /// Used when adding new code
        /// </summary>
        private void AddNewCode()
        {
            if (!string.IsNullOrWhiteSpace(tbNoviBarcode.Text))
                if (!CheckCodesInGrid() && !CheckCodesInDatabase())
                {
                    AddCodeToGrid(tbNoviBarcode.Text.Trim());
                    tbNoviBarcode.Text = null;
                }
                else
                    MessageBox.Show("Kod koji ste upisali već postoji!", "Greška");
            else
                MessageBox.Show("Nise upisali barkod!", "Greška");
        }

        /// <summary>
        /// Used to save changes
        /// </summary>
        private void SaveCodes()
        {
            if (dataGridView.Rows.Count > 0)
            {
                EanCodes.Clear();
                foreach (DataGridViewRow row in dataGridView.Rows)
                    EanCodes.Add(row.Cells["barcode"].Value.ToString());
            }
            else
                EanCodes.Clear();
        }

        /// <summary>
        /// Deletes selected row
        /// </summary>
        private void DeleteCode()
        {
            int index = dataGridView.CurrentCell.RowIndex;
            dataGridView.Rows.RemoveAt(index);
        }

        /// <summary>
        /// Used to check if code already exists in grid
        /// </summary>
        /// <returns></returns>
        private bool CheckCodesInGrid()
        {
            bool result = false;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells["barcode"].Value.ToString().ToUpper().Contains(tbNoviBarcode.Text))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Used to check if code already exists in database
        /// </summary>
        /// <returns></returns>
        private bool CheckCodesInDatabase()
        {
            bool result = false;
            DataTable DTRoba = Global.Database.GetRoba();

            if (DTRoba.Rows.Count > 0)
            {
                string newEan = tbNoviBarcode.Text;

                foreach (DataRow row in DTRoba.Rows)
                {
                    if (row["ean"].ToString() != "-1")
                    {
                        //If we have more than 1 barcodes for an item, we have to check them all
                        if (row["ean"].ToString().Contains(";"))
                        {
                            string[] array = row["ean"].ToString().Split(';');
                            foreach (var item in array)
                            {
                                if (newEan == item.ToString())
                                {
                                    result = true;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (newEan == row["ean"].ToString())
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
            }

            return result;
        }
        


        private void BtnDodaj_Click(object sender, EventArgs e)
        {
            AddNewCode();
        }

        private void TbNoviBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                AddNewCode();
        }

        private void BtnSpremi_Click(object sender, EventArgs e)
        {
            SaveCodes();
            Close();
        }

        private void BtnObrisi_Click(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count > 0)
                DeleteCode();
        }

    }
}
