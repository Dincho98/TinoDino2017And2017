using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Kasa
{
    public partial class frmSveSmjene : Form
    {
        public frmSveSmjene()
        {
            InitializeComponent();
        }

        private void frmSveSmjene_Load(object sender, EventArgs e)
        {
            LoadSmjene();
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void LoadSmjene()
        {
            string sql = "SELECT pocetno_stanje AS [Blag.Minimum],pocetak AS [Početak],zavrsetak AS [Završetak smjene],zavrsno_stanje AS [Završno stanje],napomena AS [Napomena],id FROM smjene ORDER BY id DESC";
            DataTable DT = classSQL.select(sql, "smjene").Tables[0];
            //dgv.DataSource = DT;

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                dgv.Rows.Add(DT.Rows[i]["Blag.Minimum"].ToString(),
                    DT.Rows[i]["Početak"].ToString(),
                    DT.Rows[i]["Završetak smjene"].ToString(),
                    DT.Rows[i]["Završno stanje"].ToString(),
                    DT.Rows[i]["Napomena"].ToString(),
                    DT.Rows[i]["id"].ToString()
                    );

                dgv.Columns["id"].Visible = false;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((byte)(105)), ((byte)(170)), ((byte)(197)));
            Color y = System.Drawing.Color.FromArgb(((byte)(40)), ((byte)(109)), ((byte)(135)));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Kasa.frmPregledSmjene ps = new Kasa.frmPregledSmjene();
            ps.id = dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString();
            ps.datumOD = dgv.Rows[e.RowIndex].Cells["pocetak"].FormattedValue.ToString();
            ps.datumDO = dgv.Rows[e.RowIndex].Cells["zavrsetak"].FormattedValue.ToString();
            ps.ShowDialog();
        }
    }
}