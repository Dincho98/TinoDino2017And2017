using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class frmBlagajne : Form
    {
        public frmBlagajne()
        {
            InitializeComponent();
        }

        private DataSet DS = new DataSet();

        private void frmGrupeProizvoda_Load(object sender, EventArgs e)
        {
            SetGrupe();

            //fill grupe
            DataTable DT_podgrupa = classSQL.select("SELECT * FROM ducan", "ducan").Tables[0];
            cbDucan.DataSource = DT_podgrupa;
            cbDucan.DisplayMember = "ime_ducana";
            cbDucan.ValueMember = "id_ducan";

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

        private DataSet DS_Skladiste;

        private void SetGrupe()
        {
            DataTable DTSK = new DataTable("ducan");
            DTSK.Columns.Add("id_ducan", typeof(string));
            DTSK.Columns.Add("ime_ducana", typeof(string));

            DS_Skladiste = classSQL.select("SELECT * FROM ducan", "ducan");
            for (int i = 0; i < DS_Skladiste.Tables[0].Rows.Count; i++)
            {
                DTSK.Rows.Add(DS_Skladiste.Tables[0].Rows[i]["id_ducan"], DS_Skladiste.Tables[0].Rows[i]["ime_ducana"]);
            }

            id_ducan.DataSource = DTSK;
            id_ducan.DataPropertyName = "ime_ducana";
            id_ducan.DisplayMember = "ime_ducana";
            id_ducan.Name = "ime_ducana";
            id_ducan.Resizable = DataGridViewTriState.True;
            id_ducan.SortMode = DataGridViewColumnSortMode.Automatic;
            id_ducan.ValueMember = "id_ducan";

            if (dgv.Rows.Count > 0)
            {
                dgv.Rows.Clear();
            }

            DataTable DT = classSQL.select("SELECT * FROM blagajna", "blagajna").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                bool b = false;
                if (DT.Rows[i]["aktivnost"].ToString() == "1")
                {
                    b = true;
                }
                dgv.Rows.Add(DT.Rows[i]["ime_blagajne"].ToString(), DT.Rows[i]["id_ducan"].ToString(), b, DT.Rows[i]["id_blagajna"].ToString());
            }
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            string s = "SELECT setval('blagajna_id_blagajna_seq', (SELECT MAX(id_blagajna) FROM blagajna) + 1)";
            classSQL.insert(s);

            string sql = string.Format(@"INSERT INTO blagajna
(
    ime_blagajne, aktivnost, id_ducan
)
VALUES
(
    '{0}', '1', '{1}'
);",
    nuBrojNaplatnog.Value.ToString(),
    cbDucan.SelectedValue.ToString());

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
                    string sql = string.Format(@"UPDATE blagajna
SET
    ime_blagajne = '{0}'
WHERE id_blagajna = '{1}';",
dgv.Rows[e.RowIndex].Cells["ime_blagajne"].FormattedValue.ToString(),
dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString());
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
                    string sql = string.Format(@"UPDATE blagajna
SET
    id_ducan = '{0}'
WHERE id_blagajna = '{1}';",
dgv.Rows[e.RowIndex].Cells[1].Value,
dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString());

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
                    string aa = "0";
                    if (dgv.Rows[e.RowIndex].Cells["aktivnost"].FormattedValue.ToString() == "True")
                    {
                        aa = "1";
                    }

                    string sql = string.Format(@"UPDATE blagajna
SET
    aktivnost = '{0}'
WHERE id_blagajna = '{1}';",
aa,
dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString());

                    classSQL.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void frmBlagajne_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnNoviUnos.Select();
        }
    }
}