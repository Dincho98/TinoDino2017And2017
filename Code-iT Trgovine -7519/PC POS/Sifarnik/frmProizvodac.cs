using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class frmProizvodac : Form
    {
        public frmProizvodac()
        {
            InitializeComponent();
        }

        private DataSet DS = new DataSet();

        private void frmGrupeProizvoda_Load(object sender, EventArgs e)
        {
            SetGrupe();
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

        //DataSet DS_Skladiste;
        private void SetGrupe()
        {
            if (dgv.Rows.Count > 0)
            {
                dgv.Rows.Clear();
            }

            DataTable DT = classSQL.select("SELECT * FROM manufacturers;", "manufacturers").Tables[0];
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                dgv.Rows.Add(DT.Rows[i]["manufacturers"].ToString(), DT.Rows[i]["id_manufacturers"].ToString());
            }
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            if (txtnazivProizvodaca.Text == "")
            {
                MessageBox.Show("Greška niste upisali naziv grupe."); return;
            }

            string sql = string.Format(@"INSERT INTO manufacturers
(
    manufacturers
)
VALUES
(
    '{0}'
);",
txtnazivProizvodaca.Text);

            classSQL.insert(sql);

            SetGrupe();
            MessageBox.Show("Spremljno.");
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell.ColumnIndex == 0)
            {
                try
                {
                    string sql = string.Format(@"UPDATE manufacturers
SET
    manufacturers = '{0}'
WHERE id_manufacturers = '{1}';",
dgv.Rows[e.RowIndex].Cells["proizvodac"].FormattedValue.ToString(),
dgv.Rows[e.RowIndex].Cells["id_proizvodac"].FormattedValue.ToString());

                    classSQL.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void frmProizvodac_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnNoviUnos.Select();
        }
    }
}