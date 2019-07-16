using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class frmPrirezOpcine : Form
    {
        public frmPrirezOpcine()
        {
            InitializeComponent();
        }

        private DataSet DSsk = new DataSet();

        private void frmSkladista_Load(object sender, EventArgs e)
        {
            SetDgv();
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void SetDgv()
        {
            if (dgvSk.Rows.Count != 0)
            {
                dgvSk.Rows.Clear();
            }

            DataTable DT = classSQL.select("SELECT * FROM porezi", "porezi").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                dgvSk.Rows.Add(DT.Rows[i]["naziv"].ToString(), DT.Rows[i]["iznos"].ToString(), DT.Rows[i]["id_porez"].ToString());
            }
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            if (txtNaziv.Text == "")
            {
                MessageBox.Show("Niste pravilno upisali naziv poreza."); return;
            }

            decimal dec_parse;
            if (!Decimal.TryParse(txtIznos.Text, out dec_parse))
            {
                MessageBox.Show("Greška kod upisa iznosa poreza.", "Greška"); return;
            }

            provjera_sql(classSQL.insert("INSERT INTO porezi (naziv,iznos) VALUES ('" + txtNaziv.Text + "','" + txtIznos.Text + "')"));
            SetDgv();
        }

        private void dgvSk_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSk.CurrentCell.ColumnIndex == 0)
            {
                try
                {
                    string sql = "UPDATE porezi SET naziv='" + dgvSk.Rows[e.RowIndex].Cells["naziv"].FormattedValue.ToString() + "' WHERE id_porez='" + dgvSk.Rows[e.RowIndex].Cells["id_porez"].FormattedValue.ToString() + "'";
                    classSQL.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgvSk.CurrentCell.ColumnIndex == 1)
            {
                try
                {
                    string sql = "UPDATE porezi SET iznos='" + dgvSk.Rows[e.RowIndex].Cells["iznos"].FormattedValue.ToString() + "' WHERE id_porez='" + dgvSk.Rows[e.RowIndex].Cells["id_porez"].FormattedValue.ToString() + "'";
                    classSQL.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void frmStopePoreza_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnNovo.Select();
        }
    }
}