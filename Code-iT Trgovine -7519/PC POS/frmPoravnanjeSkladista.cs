using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmPoravnanjeSkladista : Form
    {
        public frmPoravnanjeSkladista()
        {
            InitializeComponent();
        }

        private void frmPoravnanjeSkladista_Load(object sender, EventArgs e)
        {
            DataTable DT = classSQL.select("SELECT roba_prodaja.id_roba_prodaja, roba_prodaja.id_skladiste, skladiste.skladiste, roba_prodaja.kolicina, roba_prodaja.nc, " +
                " roba_prodaja.vpc, roba_prodaja.porez, roba_prodaja.sifra, roba_prodaja.porez_potrosnja, roba.naziv FROM roba_prodaja " +
                " LEFT JOIN roba ON roba_prodaja.sifra = roba.sifra LEFT JOIN skladiste ON skladiste.id_skladiste=roba_prodaja.id_skladiste ORDER BY naziv ASC", "roba_prodaja").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                dgv.Rows.Add(DT.Rows[i]["sifra"].ToString(), DT.Rows[i]["naziv"].ToString(), DT.Rows[i]["kolicina"].ToString(), DT.Rows[i]["id_roba_prodaja"].ToString(), DT.Rows[i]["skladiste"].ToString());
            }
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell.ColumnIndex == 2)
            {
                try
                {
                    string sql = "UPDATE roba_prodaja SET kolicina='" + dgv.Rows[e.RowIndex].Cells["kolicina"].FormattedValue.ToString() + "' WHERE id_roba_prodaja='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
                    classSQL.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (dgv["sifra", i].FormattedValue.ToString() == txtSifra.Text)
                {
                    dgv.CurrentCell = dgv["kolicina", i];
                    dgv.Select();
                    return;
                }
            }
            MessageBox.Show("Nema tražene šifre.");
        }

        private void txtSifra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnTrazi.PerformClick();
            }
        }
    }
}