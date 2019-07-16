using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Kasa
{
    public partial class frmUnesiSifruSkladisteUMinus : Form
    {
        public frmUnesiSifruSkladisteUMinus()
        {
            InitializeComponent();
        }

        private bool loaded = false;

        public DataTable svaSkladista;

        public bool SKIDAJ = false;
        public bool novoKol = false;
        public string novoSkladiste;

        private void button1_Click(object sender, EventArgs e)
        {
            if (novoKol)
            {
                this.Close();
                SKIDAJ = true;
            }
            else
            {
                string sql = "SELECT zaporka FROM zaposlenici WHERE id_zaposlenik='" +
                    Properties.Settings.Default.id_zaposlenik + "'";
                string zaporka = classSQL.select(sql, "zaposlenici").Tables[0].Rows[0][0].ToString();

                if (textBox1.Text.Trim() == zaporka)
                {
                    this.Close();
                    SKIDAJ = true;
                }
                else
                {
                    MessageBox.Show("Kriva lozinka!");
                }
            }
        }

        private void frmUnesiSifruSkladisteUMinus_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = svaSkladista;
            //int height = dataGridView1.ColumnHeadersHeight;
            //dataGridView1.Height = height * (dataGridView1.Rows.Count + 1) > 180 ? 180 : height * (dataGridView1.Rows.Count + 1);
            //dataGridView1.MaximumSize = new Size(180, 180);
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            this.ActiveControl = textBox1;
            dataGridView1.ClearSelection();
            loaded = true;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                button1_Click(textBox1, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (dataGridView1.SelectedRows.Count == 0) dataGridView1.Rows[0].Selected = true;
                else
                {
                    int curent = dataGridView1.CurrentRow != null ? dataGridView1.CurrentRow.Index : -1;
                    if (curent < dataGridView1.RowCount - 1)
                        dataGridView1.CurrentCell = dataGridView1.Rows[curent + 1].Cells[0];
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                int curent = dataGridView1.CurrentRow != null ? dataGridView1.CurrentRow.Index : 0;
                if (curent > 0)
                    dataGridView1.CurrentCell = dataGridView1.Rows[curent - 1].Cells[0];
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                novoSkladiste = dataGridView1.SelectedRows[0].Cells[2].FormattedValue.ToString();
                decimal kol;
                decimal.TryParse(dataGridView1.SelectedRows[0].Cells[0].FormattedValue.ToString(), out kol);
                if (kol > 0) novoKol = true;
                else novoKol = false;

                textBox1.Select();
            }
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w + 3, h + 3);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w - 3, h - 3);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}