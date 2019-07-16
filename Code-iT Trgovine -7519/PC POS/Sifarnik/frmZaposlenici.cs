using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class frmZaposlenici : Form
    {
        public frmZaposlenici()
        {
            InitializeComponent();
        }

        //DataTable DTBojeForme;
        private void frmSobe_Load(object sender, EventArgs e)
        {
            //DTBojeForme = classDBlite.LiteSelect("SELECT * FROM FormColors", "FormColors").Tables[0];

            /****************************SINKRONIZACIJA SA WEB-OM*****************/
            bgSinkronizacija = new BackgroundWorker();
            bgSinkronizacija.DoWork += new DoWorkEventHandler(bgSinkronizacija_DoWork);
            bgSinkronizacija.WorkerSupportsCancellation = true;
            /****************************SINKRONIZACIJA SA WEB-OM*****************/

            DataTable DTtip_sobe = classSQL.select("SELECT * FROM grad ORDER BY grad;", "grad").Tables[0];
            cbGrad.DataSource = DTtip_sobe;
            cbGrad.DisplayMember = "grad";
            cbGrad.ValueMember = "id_grad";

            DataTable DTdopustenja = classSQL.select("SELECT * FROM dopustenja;", "dopustenja").Tables[0];
            cbDopustenje.DataSource = DTdopustenja;
            cbDopustenje.DisplayMember = "naziv";
            cbDopustenje.ValueMember = "id_dopustenje";

            DataTable DT_zemlja = classSQL.select("SELECT * FROM dopustenja;", "dopustenja").Tables[0];
            DataTable DTSK = new DataTable("dopustenje");
            DTSK.Columns.Add("id_dopustenje", typeof(string));
            DTSK.Columns.Add("naziv", typeof(string));
            for (int i = 0; i < DT_zemlja.Rows.Count; i++)
            {
                DTSK.Rows.Add(DT_zemlja.Rows[i]["id_dopustenje"], DT_zemlja.Rows[i]["naziv"]);
            }

            dopustenje.DataSource = DTSK;
            dopustenje.DataPropertyName = "naziv";
            dopustenje.DisplayMember = "naziv";
            dopustenje.HeaderText = "naziv";
            dopustenje.Name = "naziv";
            dopustenje.Resizable = DataGridViewTriState.True;
            dopustenje.SortMode = DataGridViewColumnSortMode.Automatic;
            dopustenje.ValueMember = "id_dopustenje";

            DataTable DT_grad = classSQL.select("SELECT * FROM grad ORDER BY grad ASC;", "grad").Tables[0];
            DataTable DTg = new DataTable("grad");
            DTg.Columns.Add("id_grad", typeof(string));
            DTg.Columns.Add("grad", typeof(string));
            for (int i = 0; i < DT_grad.Rows.Count; i++)
            {
                DTg.Rows.Add(DT_grad.Rows[i]["id_grad"], DT_grad.Rows[i]["grad"]);
            }

            grad.DataSource = DTg;
            grad.DataPropertyName = "grad";
            grad.DisplayMember = "grad";
            grad.HeaderText = "grad";
            grad.Name = "grad";
            grad.Resizable = DataGridViewTriState.True;
            grad.SortMode = DataGridViewColumnSortMode.Automatic;
            grad.AutoComplete = true;
            grad.ValueMember = "id_grad";

            Set();
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        /****************************SINKRONIZACIJA SA WEB-OM*****************/
        private BackgroundWorker bgSinkronizacija = null;
        private synWeb.synPokretac PokretacSinkronizacije = new synWeb.synPokretac();
        /****************************SINKRONIZACIJA SA WEB-OM*****************/

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

        private void Set()
        {
            if (dgv.Rows.Count > 0)
            {
                dgv.Rows.Clear();
            }

            DataTable DT = classSQL.select("SELECT * FROM zaposlenici ORDER BY ime;", "zaposlenici").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                bool b = false;
                if (DT.Rows[i]["aktivan"].ToString() == "DA")
                {
                    b = true;
                }
                dgv.Rows.Add(
                    DT.Rows[i]["ime"].ToString(),
                    DT.Rows[i]["prezime"].ToString(),
                    DT.Rows[i]["id_grad"].ToString(),
                    DT.Rows[i]["adresa"].ToString(),
                    DT.Rows[i]["id_dopustenje"].ToString(),
                    DT.Rows[i]["oib"].ToString(),
                    DT.Rows[i]["tel"].ToString(),
                    DT.Rows[i]["mob"].ToString(),
                    DT.Rows[i]["email"].ToString(),
                    DT.Rows[i]["zaporka"].ToString(),
                    DT.Rows[i]["datum_rodenja"].ToString(),
                    b,
                    DT.Rows[i]["id_zaposlenik"].ToString());
            }
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Unosom ovog zaposlenika nemate više mogućnosti obrisati istog.\r\nDa li ste sigurni da želite unjeti novog djelatnika?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //        decimal dec = 0;
                if (string.IsNullOrEmpty(txtIme.Text))
                {
                    MessageBox.Show("Greška, niste upisali pravilno ime."); return;
                }

                if (txtPrezime.Text == "")
                {
                    MessageBox.Show("Greška, niste upisali pravilno prezime."); return;
                }

                if (txtZaporka.Text == "")
                {
                    MessageBox.Show("Greška, niste upisali pravilno zaporku."); return;
                }

                if (!OstaleFunkcije.OIB_Validacija(txtOib.Text))
                {
                    MessageBox.Show("Greška kod upisa oib-a.", "Greška"); return;
                }

                string sql = string.Format(@"INSERT INTO zaposlenici
(
    ime, prezime, id_grad, adresa, id_dopustenje, oib, tel, datum_rodenja, email, mob, aktivan, zaporka, novo
)
VALUES
(
    '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', 'DA', '{10}', '1'
);",
    txtIme.Text,
    txtPrezime.Text,
    cbGrad.SelectedValue,
    txtAdresa.Text,
    cbDopustenje.SelectedValue,
    txtOib.Text,
    txtTel.Text,
    dtpRodenje.Value,
    txtEmail.Text,
    txtMob.Text,
    txtZaporka.Text);

                classSQL.insert(sql);

                Set();

                txtAdresa.Text = "";
                txtEmail.Text = "";
                txtIme.Text = "";
                txtPrezime.Text = "";
                txtMob.Text = "";
                txtOib.Text = "";
                txtZaporka.Text = "";
                txtTel.Text = "";
            }
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell.ColumnIndex == 0)
            {
                try
                {
                    string sql = string.Format(@"UPDATE zaposlenici
SET
    editirano = '1',
    ime = '{0}'
WHERE id_zaposlenik = '{1}';",
dgv.Rows[e.RowIndex].Cells["ime"].FormattedValue.ToString(),
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
                    string sql = string.Format(@"UPDATE zaposlenici
SET
    editirano = '1',
    prezime = '{0}'
WHERE id_zaposlenik = '{1}';",
dgv.Rows[e.RowIndex].Cells["prezime"].FormattedValue.ToString(),
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
                    string sql = string.Format(@"UPDATE zaposlenici
SET
    editirano = '1',
    id_grad = '{0}'
WHERE id_zaposlenik = '{1}';",
dgv.Rows[e.RowIndex].Cells[2].Value,
dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString());

                    classSQL.update(sql);
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
                    string sql = string.Format(@"UPDATE zaposlenici
SET
    editirano = '1',
    adresa = '{0}'
WHERE id_zaposlenik = '{1}';",
dgv.Rows[e.RowIndex].Cells["adresa"].FormattedValue.ToString(),
dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString());

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
                    string sql = string.Format(@"UPDATE zaposlenici
SET
    editirano = '1',
    id_dopustenje = '{0}'
WHERE id_zaposlenik = '{1}';",
dgv.Rows[e.RowIndex].Cells[4].Value,
dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString());
                    classSQL.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 5)
            {
                try
                {
                    if (!OstaleFunkcije.OIB_Validacija(dgv.Rows[e.RowIndex].Cells["oib"].FormattedValue.ToString()))
                    {
                        MessageBox.Show("Greška kod upisa oib-a.", "Greška"); return;
                    }

                    string sql = string.Format(@"UPDATE zaposlenici
SET
    editirano = '1',
    oib = '{0}'
WHERE id_zaposlenik = '{1}';",
dgv.Rows[e.RowIndex].Cells["oib"].FormattedValue.ToString(),
dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString());

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
                    string sql = string.Format(@"UPDATE zaposlenici
SET
    editirano = '1',
    tel = '{0}'
WHERE id_zaposlenik = '{1}';",
dgv.Rows[e.RowIndex].Cells["tel"].FormattedValue.ToString(),
dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString());

                    classSQL.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 7)
            {
                try
                {
                    string sql = string.Format(@"UPDATE zaposlenici
SET
    editirano = '1',
    mob = '{0}'
WHERE id_zaposlenik = '{1}';",
dgv.Rows[e.RowIndex].Cells["mob"].FormattedValue.ToString(),
dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString());
                    classSQL.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 8)
            {
                try
                {
                    string sql = string.Format(@"UPDATE zaposlenici
SET
    editirano = '1',
    email = '{0}'
WHERE id_zaposlenik = '{1}';",
dgv.Rows[e.RowIndex].Cells["email"].FormattedValue.ToString(),
dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString());

                    classSQL.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 9)
            {
                try
                {
                    string sql = string.Format(@"UPDATE zaposlenici
SET
    editirano = '1',
    zaporka = '{0}'
WHERE id_zaposlenik = '{1}';",
dgv.Rows[e.RowIndex].Cells["zaporka"].FormattedValue.ToString(),
dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString());
                    classSQL.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 10)
            {
                try
                {
                    string sql = string.Format(@"UPDATE zaposlenici
SET
    editirano = '1',
    datum_rodenja = '{0}'
WHERE id_zaposlenik = '{1}';",
dgv.Rows[e.RowIndex].Cells["dat_rod"].FormattedValue.ToString(),
dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString());

                    classSQL.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 11)
            {
                try
                {
                    string aa = "NE";
                    if (dgv.Rows[e.RowIndex].Cells["aktivnost"].FormattedValue.ToString() == "True")
                    {
                        aa = "DA";
                    }

                    string sql = string.Format(@"UPDATE zaposlenici
SET
    editirano = '1',
    aktivan = '{0}'
WHERE id_zaposlenik = '{1}';",
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

        private void frmSobe_FormClosing(object sender, FormClosingEventArgs e)
        {
            bgSinkronizacija.RunWorkerAsync();
            btnNoviUnos.Select();
        }
    }
}