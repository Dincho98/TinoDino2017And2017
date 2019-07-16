using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class frmSkladista : Form
    {
        public frmSkladista()
        {
            InitializeComponent();
        }

        private DataSet DSsk = new DataSet();

        private void frmSkladista_Load(object sender, EventArgs e)
        {
            /****************************SINKRONIZACIJA SA WEB-OM*****************/
            bgSinkronizacija = new BackgroundWorker();
            bgSinkronizacija.DoWork += new DoWorkEventHandler(bgSinkronizacija_DoWork);
            bgSinkronizacija.WorkerSupportsCancellation = true;
            /****************************SINKRONIZACIJA SA WEB-OM*****************/

            SetDgv();
            SetCb();
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
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private bool boolNovi = false;

        private void SetCb()
        {
            //CB grad
            DataSet DSgrad = classSQL.select("SELECT DISTINCT (grad),id_grad FROM grad ORDER BY grad ", "grad");
            cbGrad.DataSource = DSgrad.Tables[0];
            cbGrad.DisplayMember = "grad";
            cbGrad.ValueMember = "id_grad";

            //CB dražave
            DataSet DSdrazava = classSQL.select("SELECT * FROM zemlja ORDER BY zemlja", "zemlja");
            cbZemlja.DataSource = DSdrazava.Tables[0];
            cbZemlja.DisplayMember = "zemlja";
            cbZemlja.ValueMember = "id_zemlja";

            cbGrad.SelectedValue = dgvSk.CurrentRow.Cells["id_grad"].FormattedValue.ToString();
            cbZemlja.SelectedValue = dgvSk.CurrentRow.Cells["id_drzava"].FormattedValue.ToString();
            txtSkladiste.Text = dgvSk.CurrentRow.Cells["skladiste"].FormattedValue.ToString();

            if (dgvSk.CurrentRow.Cells["aktivnost"].FormattedValue.ToString() == "DA")
            {
                chbAktivnost.Checked = true;
            }
            else
            {
                chbAktivnost.Checked = false;
            }
        }

        private void SetDgv()
        {
            //dgvSk.DataBindings.Clear();
            //dgvSk.DataSource = null;
            //dgvSk.Rows.Clear();

            //if(dgvSk.Rows.Count!=0)
            //{
            //    dgvSk.Rows.Clear();
            //}

            DSsk = new DataSet();

            if (classSQL.remoteConnectionString == "")
            {
                string sql = @"SELECT skladiste.skladiste AS skladiste, grad.grad AS grad, zemlja.zemlja AS drzava, skladiste.aktivnost, case when skladiste.is_glavno = true then 'DA' else 'NE' end as glavno, skladiste.id_grad, skladiste.id_zemlja as id_drzava, skladiste.id_skladiste
FROM skladiste
LEFT JOIN grad ON skladiste.id_grad=grad.id_grad
LEFT JOIN zemlja ON zemlja.id_zemlja=skladiste.id_zemlja
order by skladiste.skladiste;";

                classSQL.CeAdatpter(sql).Fill(DSsk, "skladiste");
            }
            else
            {
                string sql = @"SELECT skladiste.skladiste AS skladiste, grad.grad AS grad, zemlja.zemlja AS drzava, skladiste.aktivnost, case when skladiste.is_glavno = true then 'DA' else 'NE' end as glavno, skladiste.id_grad, skladiste.id_zemlja as id_drzava, skladiste.id_skladiste
FROM skladiste
LEFT JOIN grad ON skladiste.id_grad=grad.id_grad
LEFT JOIN zemlja ON zemlja.id_zemlja=skladiste.id_zemlja
order by skladiste.skladiste;";

                classSQL.NpgAdatpter(sql).Fill(DSsk, "skladiste");
            }

            dgvSk.Columns["id_grad"].Visible = false;
            dgvSk.Columns["id_drzava"].Visible = false;
            dgvSk.Columns["id_skladiste"].Visible = false;
            dgvSk.DataSource = DSsk.Tables[0];
            //for (int i = 0; i < DSsk.Tables[0].Rows.Count; i++)
            //{
            //    dgvSk.Rows.Add(DSsk.Tables[0].Rows[i]["Skladište"].ToString(), DSsk.Tables[0].Rows[i]["Grad"].ToString(), DSsk.Tables[0].Rows[i]["Država"].ToString(), DSsk.Tables[0].Rows[i]["aktivnost"].ToString(), DSsk.Tables[0].Rows[i]["id_grad"].ToString(), DSsk.Tables[0].Rows[i]["id_zemlja"].ToString(), DSsk.Tables[0].Rows[i]["id_skladiste"].ToString());
            //}
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            if (boolNovi == false)
            {
                int skl = (dgvSk.Rows.Cast<DataGridViewRow>().Where(x => x.Cells["id_skladiste"].Value.ToString() != dgvSk.CurrentRow.Cells["id_skladiste"].FormattedValue.ToString()).Count(x => x.Cells["glavno"].Value.ToString() == "DA"));
                if (chbGlavno.Checked && skl > 0)
                {
                    MessageBox.Show("Samo jedno skladiše može biti glavno skladište.");
                    return;
                }

                string sql = string.Format(@"UPDATE skladiste
SET
    editirano='1',
    skladiste = '{0}',
    id_grad = '{1}',
    id_zemlja = '{2}',
    is_glavno = {3},
    aktivnost = '{4}'
WHERE id_skladiste = '{5}';",
txtSkladiste.Text,
cbGrad.SelectedValue,
cbZemlja.SelectedValue,
chbGlavno.Checked.ToString().ToLower(),
GetValueCheckBox(),
dgvSk.CurrentRow.Cells["id_skladiste"].FormattedValue.ToString());

                classSQL.update(sql);
                SetDgv();
            }
            else
            {
                int skl = (dgvSk.Rows.Cast<DataGridViewRow>().Count(x => x.Cells["glavno"].Value.ToString() == "DA"));
                if (chbGlavno.Checked && skl > 0)
                {
                    MessageBox.Show("Samo jedno skladiše može biti glavno skladište.");
                    return;
                }

                if (MessageBox.Show("Nakon unosa ovog skladišta nećete imati više mogućnosti izbrisati isto.", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string sql = string.Format(@"INSERT INTO skladiste
(
    skladiste, id_grad, id_zemlja, is_glavno, aktivnost, editirano
)
VALUES
(
    '{0}', '{1}', '{2}', '{3}', '{4}', '1'
);",
    txtSkladiste.Text, cbGrad.SelectedValue, cbZemlja.SelectedValue, chbGlavno.Checked.ToString().ToLower(), GetValueCheckBox());

                    provjera_sql(classSQL.insert(sql));
                    SetDgv();
                }
            }

            dgvSk.Enabled = true;
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void dgvSk_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSk.Rows.Count < 1)
                return;

            txtSkladiste.Text = dgvSk.CurrentRow.Cells["skladiste"].FormattedValue.ToString();
            cbGrad.SelectedValue = dgvSk.CurrentRow.Cells["id_grad"].FormattedValue.ToString();
            cbZemlja.SelectedValue = dgvSk.CurrentRow.Cells["id_drzava"].FormattedValue.ToString();

            if (dgvSk.CurrentRow.Cells["aktivnost"].FormattedValue.ToString() == "DA")
            {
                chbAktivnost.Checked = true;
            }
            else
            {
                chbAktivnost.Checked = false;
            }

            if (dgvSk.CurrentRow.Cells["glavno"].FormattedValue.ToString() == "DA")
            {
                chbGlavno.Checked = true;
            }
            else
            {
                chbGlavno.Checked = false;
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            boolNovi = true;
            SetOnNull();
            dgvSk.Enabled = false;
        }

        private void SetOnNull()
        {
            txtSkladiste.Text = "";
            chbAktivnost.Checked = true;
            chbGlavno.Checked = false;
        }

        private string GetValueCheckBox()
        {
            if (chbAktivnost.Checked)
            {
                return "DA";
            }
            else
            {
                return "NE";
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            boolNovi = false;
            dgvSk.Enabled = true;
            txtSkladiste.Text = dgvSk.CurrentRow.Cells["skladiste"].FormattedValue.ToString();
            cbGrad.SelectedValue = dgvSk.CurrentRow.Cells["id_grad"].FormattedValue.ToString();
            cbZemlja.SelectedValue = dgvSk.CurrentRow.Cells["id_drzava"].FormattedValue.ToString();
            chbGlavno.Checked = false;
        }
    }
}