using System;
using System.Collections;
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
    public partial class FrmSveUgostiteljskeOtpremnice : Form
    {
        private DataTable DTotpremnice { get; set; }

        public FrmSveUgostiteljskeOtpremnice()
        {
            InitializeComponent();
        }

        private void FrmSveUgostiteljskeOtpremnice_Load(object sender, EventArgs e)
        {
            DTotpremnice = Global.Database.GetOtpremnice(true);
            FillGrid();
        }

        /// <summary>
        /// Used to fill grid with data
        /// </summary>
        private void FillGrid(bool filter = false)
        {
            RefreshGrid();

            if (DTotpremnice.Rows.Count > 0)
            {
                foreach (DataRow row in filter ? FilterData().Rows : DTotpremnice.Rows)
                {
                    int index = dataGridView.Rows.Add();
                    dataGridView.Rows[index].Cells["broj"].Value = index + 1;
                    dataGridView.Rows[index].Cells["broj_otpremnice"].Value = row["broj_otpremnice"].ToString();
                    dataGridView.Rows[index].Cells["datum"].Value = DateTime.Parse(row["datum"].ToString()).ToString("dd.MM.yyyy.");
                    dataGridView.Rows[index].Cells["soba"].Value = Global.Database.GetSobe(row["osoba_partner"].ToString()).Rows[0]["naziv_sobe"].ToString();
                    dataGridView.Rows[index].Cells["iznos"].Value = row["ukupno"].ToString() + " kn";

                    string naplaceno = null;
                    switch (row["naplaceno_fakturom"].ToString())
                    {
                        case "0":
                            naplaceno = "Obrisano";
                            dataGridView.Rows[index].DefaultCellStyle.BackColor = Color.Silver;
                            break;
                        case "1":
                            naplaceno = "Nije naplačeno";
                            dataGridView.Rows[index].DefaultCellStyle.BackColor = Color.Salmon;
                            break;
                        case "2":
                            naplaceno = "Naplačeno";
                            dataGridView.Rows[index].DefaultCellStyle.BackColor = Color.LightGreen;
                            break;
                    }

                    dataGridView.Rows[index].Cells["status"].Value = naplaceno;
                }
            }
        }

        /// <summary>
        /// Clears all rows in data grid view
        /// </summary>
        private void RefreshGrid()
        {
            dataGridView.Rows.Clear();
            dataGridView.Refresh();
        }

        /// <summary>
        /// Used to filter DTotpremnice
        /// </summary>
        /// <returns></returns>
        private DataTable FilterData()
        {
            DataTable DTfiltered = DTotpremnice.Clone();

            string filterBrojOtpremnice = !string.IsNullOrWhiteSpace(tbBrojOtpremnice.Text) && int.TryParse(tbBrojOtpremnice.Text, out int broj) ? $"broj_otpremnice = {broj}" : "";
            DataRow[] rows = DTotpremnice.Select(filterBrojOtpremnice);
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                    DTfiltered.ImportRow(row);
            }
            return DTfiltered;
        }

        /// <summary>
        /// 
        /// </summary>
        private void SelectPartner()
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (DSpartner.Tables[0].Rows.Count > 0)
                    tbSifraPartner.Text = DSpartner.Tables[0].Rows[0]["id_partner"].ToString();
            }
        }

        private void BtnPretrazi_Click(object sender, EventArgs e)
        {
            FillGrid(true);
        }

        private void BtnTraziPartner_Click(object sender, EventArgs e)
        {
            SelectPartner();
        }

        private void btnIzlaz_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
