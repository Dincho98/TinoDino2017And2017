using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class frmZiroRacun : Form
    {
        public frmZiroRacun()
        {
            InitializeComponent();
        }

        private DataSet DSducani = new DataSet();

        private void frmDucani_Load(object sender, EventArgs e)
        {
            SetDucani();
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((byte)(105)), ((byte)(170)), ((byte)(197)));
            Color y = System.Drawing.Color.FromArgb(((byte)(40)), ((byte)(109)), ((byte)(135)));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void SetDucani()
        {
            if (dgv.Rows.Count > 0)
            {
                dgv.Rows.Clear();
            }

            DataTable DT = classSQL.select("SELECT id_ziroracun, ziroracun, banka, aktivnost FROM ziro_racun", "ziro_racun").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                bool b = false;
                if (DT.Rows[i]["aktivnost"].ToString().ToUpper() == "DA")
                {
                    b = true;
                }
                dgv.Rows.Add(DT.Rows[i]["banka"].ToString(), DT.Rows[i]["ziroracun"].ToString(), b, DT.Rows[i]["id_ziroracun"].ToString());
            }
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            if (txtNazivBanke.Text == "")
            {
                MessageBox.Show("Greška niste upisali naziv poslovnice."); return;
            }

            string sql = string.Format(@"INSERT INTO ziro_racun
(
    banka, ziroracun, aktivnost
)
VALUES
(
    '{0}', '{1}', 'DA'
);",
txtNazivBanke.Text,
txtBrojRacuna.Text);

            classSQL.insert(sql);

            SetDucani();
            MessageBox.Show("Spremljno.");
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell.ColumnIndex == 0)
            {
                try
                {
                    string sql = string.Format(@"UPDATE ziro_racun
SET
    banka = '{0}'
WHERE id_ziroracun = '{1}';",
dgv.Rows[e.RowIndex].Cells["banka"].FormattedValue.ToString(),
dgv.Rows[e.RowIndex].Cells["id_ziroracun"].FormattedValue.ToString());

                    classSQL.update(sql);
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
                    string sql = string.Format(@"UPDATE ziro_racun
SET
    ziroracun = '{0}'
WHERE id_ziroracun = '{1}';",
dgv.Rows[e.RowIndex].Cells["ziroracun"].FormattedValue.ToString(),
dgv.Rows[e.RowIndex].Cells["id_ziroracun"].FormattedValue.ToString());

                    classSQL.update(sql);
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
                    string aa = "NE";
                    if (dgv.Rows[e.RowIndex].Cells["aktivnost"].FormattedValue.ToString() == "True")
                    {
                        aa = "DA";
                    }

                    string sql = string.Format(@"UPDATE ziro_racun
SET
    aktivnost = '{0}'
WHERE id_ziroracun = '{1}';",
aa,
dgv.Rows[e.RowIndex].Cells["id_ziroracun"].FormattedValue.ToString());

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