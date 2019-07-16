using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class frmTraziKonto : Form
    {
        public frmTraziKonto()
        {
            InitializeComponent();
        }

        private void txttrazisifrakonto_TextChanged(object sender, EventArgs e)
        {
            string top = "";
            string remote = "";
            string where = "";
            if (classSQL.remoteConnectionString != "")
            {
                remote = " LIMIT 500";
                where = "br_konta ~* '" + txttrazisifrakonto.Text + "'";
            }
            else
            {
                where = "br_konta LIKE '%" + txttrazisifrakonto.Text + "%'";
                top = " TOP(500) ";
            }

            string sql = "SELECT " + top + " br_konta, opis FROM kontni_plan " +
            " WHERE " + where + " ORDER BY br_konta " + remote + "";

            DataTable DTkonto = classSQL.select(sql, "Kontni plan").Tables[0];

            dgw2.DataSource = DTkonto;
            dgw2.Columns["br_konta"].Width = 60;

            //VisibleTrueFalse();

            PaintRows(dgw2);
        }

        private void txttrazisifrakonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void PaintRows(DataGridView dg)
        {
            int br = 0;
            for (int i = 0; i < dg.Rows.Count; i++)
            {
                if (br == 0)
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
                    br++;
                }
                else
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    br = 0;
                }
            }
            DataGridViewRow row = dg.RowTemplate;
            row.Height = 25;
        }

        private void frmTraziKonto_Load(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM kontni_plan ORDER BY br_konta ASC";
            DataTable DTpopuni = classSQL.select(sql, "popuni").Tables[0];

            dgw2.DataSource = DTpopuni;
            dgw2.Columns["id"].Visible = false;

            PaintRows(dgw2);
        }

        private void txttraziimekonto_TextChanged(object sender, EventArgs e)
        {
            string top = "";
            string remote = "";
            string where = "";
            if (classSQL.remoteConnectionString != "")
            {
                remote = " LIMIT 500";
                where = "opis ~* '" + txttraziimekonto.Text + "'";
            }
            else
            {
                where = "opis LIKE '%" + txttraziimekonto.Text + "%'";
                top = " TOP(500) ";
            }

            string sql = "SELECT " + top + " br_konta, opis FROM kontni_plan " +
            " WHERE " + where + " ORDER BY opis " + remote + "";

            DataTable DTkonto = classSQL.select(sql, "Kontni plan").Tables[0];

            dgw2.DataSource = DTkonto;
            dgw2.Columns["br_konta"].Width = 60;

            //VisibleTrueFalse();

            PaintRows(dgw2);
        }

        private void dgw2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int br = e.RowIndex;
                Properties.Settings.Default.br_konta = dgw2.Rows[br].Cells[0].FormattedValue.ToString();
                Properties.Settings.Default.Save();
                this.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}