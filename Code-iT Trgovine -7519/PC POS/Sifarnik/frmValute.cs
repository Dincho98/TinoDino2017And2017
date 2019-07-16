using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class frmValute : Form
    {
        public frmValute()
        {
            InitializeComponent();
        }

        private DataSet DSsk = new DataSet();

        private void frmValute_Load(object sender, EventArgs e)
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

            DataTable DT = classSQL.select("SELECT * FROM valute;", "valute").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                dgvSk.Rows.Add(
                    DT.Rows[i]["sifra"].ToString(),
                    DT.Rows[i]["naziv"].ToString(),
                    DT.Rows[i]["ime_valute"].ToString(),
                    DT.Rows[i]["puni_naziv"].ToString(),
                    DT.Rows[i]["tecaj"].ToString(),
                    DT.Rows[i]["paritet"].ToString(),
                    DT.Rows[i]["id_valuta"].ToString());
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
            if (txtNaziv.Text.Trim() == "")
            {
                MessageBox.Show("Niste pravilno upisali naziv valute."); return;
            }

            decimal dec_parse;
            if (!decimal.TryParse(txtIznos.Text, out dec_parse))
            {
                MessageBox.Show("Greška kod upisa iznosa tečaja.", "Greška"); return;
            }

            if (!decimal.TryParse(txtParitet.Text, out dec_parse))
            {
                MessageBox.Show("Greška kod upisa iznosa pariteta.", "Greška"); return;
            }

            provjera_sql(classSQL.insert(string.Format(@"INSERT INTO valute
(
    sifra, naziv, ime_valute, puni_naziv, tecaj, paritet
)
VALUES
(
    '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'
);",
    txtSifra.Text,
    txtSkraceniNaziv.Text,
    txtNaziv.Text,
    txtPuniNaziv.Text,
    txtIznos.Text,
    txtParitet.Text)));

            SetDgv();
        }

        private void dgvSk_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSk.CurrentCell.ColumnIndex == 0)
            {
                try
                {
                    string sql = string.Format(@"UPDATE valute
SET
    ime_valute = '{0}'
WHERE id_valuta = '{1}';",
dgvSk.Rows[e.RowIndex].Cells["naziv"].FormattedValue.ToString(),
dgvSk.Rows[e.RowIndex].Cells["id_valuta"].FormattedValue.ToString());

                    classSQL.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgvSk.CurrentCell.ColumnIndex == 4)
            {
                try
                {
                    string sql = string.Format(@"UPDATE valute
SET
    tecaj = '{0}'
WHERE id_valuta = '{1}';",
dgvSk.Rows[e.RowIndex].Cells["tecaj"].FormattedValue.ToString(),
dgvSk.Rows[e.RowIndex].Cells["id_valuta"].FormattedValue.ToString());

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