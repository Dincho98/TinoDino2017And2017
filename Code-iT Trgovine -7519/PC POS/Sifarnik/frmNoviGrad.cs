using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class frmNoviGrad : Form
    {
        public frmNoviGrad()
        {
            InitializeComponent();
        }

        private void frmNoviGrad_Load(object sender, EventArgs e)
        {
            SetGrupe();

            //fill zemlja
            DataTable DT_zemlja = classSQL.select("SELECT * FROM zemlja WHERE aktivnost = 'DA' ORDER BY zemlja;", "zemlja").Tables[0];
            cbDrazava.DataSource = DT_zemlja;
            cbDrazava.DisplayMember = "zemlja";
            cbDrazava.ValueMember = "id_zemlja";
            cbDrazava.SelectedValue = "60";

            DataTable DTSK = new DataTable("zemlja");
            DTSK.Columns.Add("id_zemlja", typeof(string));
            DTSK.Columns.Add("zemlja", typeof(string));
            for (int i = 0; i < DT_zemlja.Rows.Count; i++)
            {
                DTSK.Rows.Add(DT_zemlja.Rows[i]["id_zemlja"], DT_zemlja.Rows[i]["zemlja"]);
            }

            drzava.DataSource = DTSK;
            drzava.DataPropertyName = "zemlja";
            drzava.DisplayMember = "zemlja";
            drzava.HeaderText = "Država";
            drzava.Name = "zemlja";
            drzava.Resizable = DataGridViewTriState.True;
            drzava.SortMode = DataGridViewColumnSortMode.Automatic;
            drzava.ValueMember = "id_zemlja";

            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = Color.FromArgb(((byte)(105)), ((byte)(170)), ((byte)(197)));
            Color y = Color.FromArgb(((byte)(40)), ((byte)(109)), ((byte)(135)));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void SetGrupe()
        {
            if (dgv.Rows.Count > 0)
            {
                dgv.Rows.Clear();
            }

            DataTable DT = classSQL.select("SELECT * FROM grad order by grad", "grad").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                dgv.Rows.Add(DT.Rows[i]["grad"].ToString(), DT.Rows[i]["posta"].ToString(),
                    DT.Rows[i]["zupanija"].ToString(), DT.Rows[i]["drzava"].ToString(),
                    DT.Rows[i]["naselje"].ToString(), DT.Rows[i]["id_grad"].ToString(), DT.Rows[i]["prirez"].ToString());
            }
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            if (txtnazivGrada.Text == "")
            {
                MessageBox.Show("Greška niste upisali naziv grupe."); return;
            }

            string sql = string.Format(@"INSERT INTO grad
(
    grad, posta, drzava, zupanija, naselje
)
VALUES
(
    '{0}', '{1}', '{2}', '{3}', '{4}'
);",
    txtnazivGrada.Text,
    txtPosta.Text,
    cbDrazava.SelectedValue.ToString(),
    txtZupanija.Text,
    txtNaselje.Text);

            classSQL.insert(sql);

            string sql1 = string.Format(@"INSERT INTO grad
(
    grad, posta, zupanija, naselje
)
VALUES
(
    '{0}', '{1}', '{2}', '{3}'
);",
    txtnazivGrada.Text,
    txtPosta.Text,
    txtZupanija.Text,
    txtNaselje.Text);

            classSQL.Setings_Update(sql1);
            SetGrupe();
            MessageBox.Show("Spremljno.");
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell.ColumnIndex == 0)
            {
                try
                {
                    string sql = string.Format(@"UPDATE grad
SET
    grad = '{0}'
WHERE id_grad = '{1}';",
dgv.Rows[e.RowIndex].Cells["grad"].FormattedValue.ToString(),
dgv.Rows[e.RowIndex].Cells["id_grad"].FormattedValue.ToString());
                    classSQL.update(sql);
                    classSQL.Setings_Update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 1)
            {
                try
                {
                    string sql = string.Format(@"UPDATE grad
SET
    posta = '{0}'
WHERE id_grad = '{1}';",
dgv.Rows[e.RowIndex].Cells["posta"].FormattedValue.ToString(),
dgv.Rows[e.RowIndex].Cells["id_grad"].FormattedValue.ToString());

                    classSQL.update(sql);
                    classSQL.Setings_Update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 2)
            {
                try
                {
                    string sql = string.Format(@"UPDATE grad
SET
    zupanija = '{0}'
WHERE id_grad = '{1}';",
dgv.Rows[e.RowIndex].Cells["id_zupanija"].FormattedValue.ToString(),
dgv.Rows[e.RowIndex].Cells["id_grad"].FormattedValue.ToString());
                    classSQL.update(sql);
                    classSQL.Setings_Update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 3)
            {
                try
                {
                    string sql = string.Format(@"UPDATE grad
SET
    drzava = '{0}'
WHERE id_grad = '{1}';",
    dgv.Rows[e.RowIndex].Cells[3].Value,
    dgv.Rows[e.RowIndex].Cells["id_grad"].FormattedValue.ToString());
                    classSQL.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 4)
            {
                try
                {
                    string sql = string.Format(@"UPDATE grad
SET
    naselje = '{0}'
WHERE id_grad = '{1}';",
dgv.Rows[e.RowIndex].Cells[4].Value,
dgv.Rows[e.RowIndex].Cells["id_grad"].FormattedValue.ToString());

                    classSQL.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 6)
            {
                try
                {
                    string sql = string.Format(@"UPDATE grad
SET
    prirez = '{0}'
WHERE id_grad = '{1}';",
dgv.Rows[e.RowIndex].Cells[6].FormattedValue.ToString(),
dgv.Rows[e.RowIndex].Cells["id_grad"].FormattedValue.ToString());

                    classSQL.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}