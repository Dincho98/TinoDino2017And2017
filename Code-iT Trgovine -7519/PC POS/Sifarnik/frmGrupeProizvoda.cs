using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class frmGrupeProizvoda : Form
    {
        public frmGrupeProizvoda()
        {
            InitializeComponent();
        }

        private DataSet DS = new DataSet();

        private void frmGrupeProizvoda_Load(object sender, EventArgs e)
        {
            SetGrupe();
            /****************************SINKRONIZACIJA SA WEB-OM*****************/
            bgSinkronizacija = new BackgroundWorker();
            bgSinkronizacija.DoWork += new DoWorkEventHandler(bgSinkronizacija_DoWork);
            bgSinkronizacija.WorkerSupportsCancellation = true;
            /****************************SINKRONIZACIJA SA WEB-OM*****************/

            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        /****************************SINKRONIZACIJA SA WEB-OM*****************/
        private BackgroundWorker bgSinkronizacija = null;
        private synWeb.synPokretac PokretacSinkronizacije = new synWeb.synPokretac();
        /****************************SINKRONIZACIJA SA WEB-OM*****************/

        private void GasenjeForme_FormClosing(object sender, FormClosingEventArgs e)
        {
            /****************************SINKRONIZACIJA SA WEB-OM*****************/
            bgSinkronizacija.RunWorkerAsync();
            /****************************SINKRONIZACIJA SA WEB-OM*****************/
        }

        private void bgSinkronizacija_DoWork(object sender, DoWorkEventArgs e)
        {
            PokretacSinkronizacije.PokreniSinkronizaciju(true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
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

            DataTable DT = classSQL.select("SELECT * FROM grupa;", "grupa").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                dgv.Rows.Add(DT.Rows[i]["grupa"].ToString(), DT.Rows[i]["id_grupa"].ToString());
            }
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            if (txtnazivGrupe.Text == "")
            {
                MessageBox.Show("Greška niste upisali naziv grupe."); return;
            }

            string s = "SELECT setval('grupa_id_grupa_seq', (SELECT MAX(id_grupa) FROM grupa) + 1);";
            classSQL.insert(s);

            string sql = string.Format(@"INSERT INTO grupa
(
    grupa, novo
)
VALUES
(
    '{0}', '1'
);", txtnazivGrupe.Text);
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
                    string sql = string.Format(@"UPDATE grupa
SET
    grupa = '{0}',
    editirano = '1'
WHERE id_grupa = '{1}';",
dgv.Rows[e.RowIndex].Cells["grupa"].FormattedValue.ToString(),
dgv.Rows[e.RowIndex].Cells["id_grupa"].FormattedValue.ToString());
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