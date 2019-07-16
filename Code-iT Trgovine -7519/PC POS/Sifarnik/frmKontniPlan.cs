using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class frmKontniPlan : Form
    {
        public frmKontniPlan()
        {
            InitializeComponent();
        }

        private int nRow;

        private void frmKontniPlan_Load(object sender, EventArgs e)
        {
            //if (dataGridView1.CurrentCell.RowIndex == null)
            //{
            //    nRow = 0;
            //}
            //else
            //{
            // nRow = dataGridView1.CurrentCell.RowIndex;
            //}

            Set();
            fillgrid();
        }

        private void fillgrid()
        {
            string sql = "SELECT * FROM kontni_plan ORDER BY br_konta ASC";
            DataTable DTpopuni = classSQL.select(sql, "popuni").Tables[0];

            dataGridView1.DataSource = DTpopuni;
            dataGridView1.Columns["id"].Visible = false;

            PaintRows(dataGridView1);
        }

        private void Set()
        {
            DataTable DTSK = new DataTable("Vrsta konta");

            DTSK.Columns.Add("id", typeof(string));
            DTSK.Columns.Add("naziv", typeof(string));

            DTSK.Rows.Add("K", "Kupci");
            DTSK.Rows.Add("D", "Dobavljači");
            DTSK.Rows.Add("M", "Skladišni konto");

            cbvrstakonta.DataSource = DTSK;

            cbvrstakonta.DisplayMember = "naziv";

            cbvrstakonta.ValueMember = "id";

            DataTable DTSK1 = new DataTable("Status");

            DTSK1.Columns.Add("id", typeof(string));
            DTSK1.Columns.Add("naziv", typeof(string));

            DTSK1.Rows.Add("A", "Aktivan");
            DTSK1.Rows.Add("N", "Neaktivan");

            cbstatus.DataSource = DTSK1;

            cbstatus.DisplayMember = "naziv";

            cbstatus.ValueMember = "id";
        }

        private void txtsifrakonta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if ((string)row.Cells["br_konta"].Value == txtsifrakonta.Text)
                    {
                        // we have a match
                        row.Selected = true;
                        dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                        //row.DefaultCellStyle.BackColor = Color.Yellow;
                        try
                        {
                            cbvrstakonta.SelectedValue = row.Cells["vrsta_korisnika"].FormattedValue.ToString();
                            txtopiskonta.Text = row.Cells["opis"].FormattedValue.ToString();
                            cbstatus.SelectedValue = row.Cells["status"].FormattedValue.ToString();
                            txtsifrakonta.Text = row.Cells["vrsta_korisnika"].FormattedValue.ToString();
                        }
                        catch { }
                    }
                    else
                    {
                        row.Selected = false;
                    }
                }
            }
        }

        private void txtsifrakonta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar))
            // && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            //if (e.KeyChar == '.'
            //    && (sender as TextBox).Text.IndexOf('.') > -1)
            //{
            //    e.Handled = true;
            //}
        }

        private void PaintRows(DataGridView dg)
        {
            int br = 0;
            for (int i = 0; i < dg.Rows.Count; i++)
            {
                if (br == 0)
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
                    br++;
                }
                else
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    br = 0;
                }
            }
            DataGridViewRow row = dg.RowTemplate;
            row.Height = 25;
        }

        private void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string vrijednost_celije = "";
            //string vrijednost_cbaktivnost = "";
            //if (dataGridView1.SelectedRows.Count >= 1)
            //{
            //    int col = dataGridView1.CurrentCell.ColumnIndex;
            //    int row1 = dataGridView1.CurrentCell.RowIndex;
            //    if (cbstatus.SelectedValue != null)
            //    {
            //        vrijednost_cbaktivnost = cbstatus.SelectedValue.ToString();

            //        vrijednost_celije = dataGridView1.Rows[row1].Cells[0].FormattedValue.ToString();

            //        string sql = "Update kontni_plan Set status = '" + vrijednost_cbaktivnost + "' Where id = '" + vrijednost_celije + "'";
            //        classSQL.select(sql, "aktivnost");
            //        fillgrid();
            //        foreach (DataGridViewRow row in dataGridView1.Rows)
            //        {
            //            if ((string)row.Cells[1].Value == txtsifrakonta.Text)
            //            {
            //                // we have a match
            //                row.Selected = true;
            //                dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
            //                row.DefaultCellStyle.BackColor = Color.Yellow;

            //            }
            //            else
            //            {
            //                row.Selected = false;
            //            }

            //        }

            //    }

            //}
            //else
            //{
            //    return;
            //}
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.RowCount < 1)
                return;

            nRow = dataGridView1.CurrentCell.RowIndex;
            int col = dataGridView1.CurrentCell.ColumnIndex;
            int row1 = dataGridView1.CurrentCell.RowIndex;

            cbvrstakonta.SelectedValue = dataGridView1.Rows[row1].Cells["vrsta_korisnika"].FormattedValue.ToString();
            txtopiskonta.Text = dataGridView1.Rows[row1].Cells["opis"].FormattedValue.ToString();
            cbstatus.SelectedValue = dataGridView1.Rows[row1].Cells["status"].FormattedValue.ToString();
            txtsifrakonta.Text = dataGridView1.Rows[row1].Cells["br_konta"].FormattedValue.ToString();
            //dataGridView1.Rows[row1].Cells[3].Selected = true;
        }

        private void btndesno_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount < 1)
                return;

            if (nRow < dataGridView1.RowCount)
            {
                dataGridView1.Rows[nRow].Selected = false;
                dataGridView1.Rows[++nRow].Selected = true;
            }

            if (txtsifrakonta.Text == "")
            {
                txtsifrakonta.Text = "0";
            }
            else
            {
                int zbroj = Convert.ToInt32(txtsifrakonta.Text) + 1;
                int id_konto = 0;
                txtsifrakonta.Text = zbroj.ToString();
            }

            string test = "";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if ((string)row.Cells["br_konta"].Value == txtsifrakonta.Text)
                {
                    // we have a match
                    //row.Selected = true;
                    dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                    //row.DefaultCellStyle.BackColor = Color.Yellow;
                    //dataGridView1.CurrentCell = dataGridView1.Rows[row.Index].Cells["id"];
                    dataGridView1.BeginEdit(true);
                    cbvrstakonta.SelectedValue = row.Cells["vrsta_korisnika"].FormattedValue.ToString();
                    txtopiskonta.Text = row.Cells["opis"].FormattedValue.ToString();
                    cbstatus.SelectedValue = row.Cells["status"].FormattedValue.ToString();
                    row.Cells["status"].Selected = true;
                }
                else
                {
                    row.Selected = false;
                }
            }

            //dataGridView1.Rows[][].DefaultCellStyle.BackColor = Color.Yellow;
        }

        private void btnlijevo_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount < 1)
                return;

            if (nRow < dataGridView1.RowCount)
            {
                dataGridView1.Rows[nRow].Selected = false;

                if (nRow <= 0)
                {
                    nRow = 0;
                }
                else
                {
                    dataGridView1.Rows[--nRow].Selected = true;
                }
            }

            if (txtsifrakonta.Text == "")
            {
                txtsifrakonta.Text = "0";
            }
            else
            {
                int zbroj = Convert.ToInt32(txtsifrakonta.Text) - 1;
                if (zbroj <= 0)
                {
                    zbroj = 0;
                }
                else
                {
                    txtsifrakonta.Text = zbroj.ToString();
                }
            }

            int col = dataGridView1.CurrentCell.ColumnIndex;
            int row1 = dataGridView1.CurrentCell.RowIndex;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if ((string)row.Cells["br_konta"].Value == txtsifrakonta.Text)
                {
                    // we have a match
                    try
                    {
                        row.Selected = true;
                        dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                        //row.DefaultCellStyle.BackColor = Color.Yellow;

                        cbvrstakonta.SelectedValue = row.Cells["vrsta_korisnika"].FormattedValue.ToString();
                        txtopiskonta.Text = row.Cells["opis"].FormattedValue.ToString();
                        cbstatus.SelectedValue = row.Cells["status"].FormattedValue.ToString();
                        row.Cells["status"].Selected = true;
                    }
                    catch
                    { }
                }
                else
                {
                    row.Selected = false;
                }
            }
        }

        private bool izmjeni = true;

        private void btnizmjeni_Click(object sender, EventArgs e)
        {
            if (txtsifrakonta.Text != "" && txtsifrakonta.Text != null)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if ((string)row.Cells["br_konta"].Value == txtsifrakonta.Text)
                    {
                    }
                    else
                    {
                        txtopiskonta.Enabled = false;
                    }
                }

                if (txtopiskonta.Text != "")
                {
                    popuni_podatke();
                }

                if (izmjeni)
                {
                    btnspremi.Visible = true;
                    txtopiskonta.Enabled = true;
                    cbvrstakonta.Enabled = true;
                    cbstatus.Enabled = true;
                    btndesno.Enabled = false;
                    btnlijevo.Enabled = false;
                    btntrazikonto.Enabled = false;
                    txtsifrakonta.Enabled = false;
                    izmjeni = false;
                    btnizmjeni.Text = "Odustani";
                }
                else
                {
                    btnspremi.Visible = false; ;
                    txtopiskonta.Enabled = false;
                    cbvrstakonta.Enabled = false;
                    cbstatus.Enabled = false;
                    btndesno.Enabled = true;
                    btnlijevo.Enabled = true;
                    btntrazikonto.Enabled = true;
                    txtsifrakonta.Enabled = true;
                    izmjeni = true;
                    btnizmjeni.Text = "Izmjeni konto";
                }
            }
            else
            {
                MessageBox.Show("Ne postoji šifra konta");
                return;
            }
        }

        private void btnspremi_Click(object sender, EventArgs e)
        {
            string vrijednost_cbaktivnost = "";
            string vrijednost_cbvrstakupca = "";

            vrijednost_cbaktivnost = cbstatus.SelectedValue.ToString();
            vrijednost_cbvrstakupca = cbvrstakonta.SelectedValue.ToString();

            string sql = "Update kontni_plan Set status = '" + vrijednost_cbaktivnost + "', vrsta_korisnika = '" + vrijednost_cbvrstakupca + "', " +
                "opis = '" + txtopiskonta.Text + "' Where br_konta = '" + txtsifrakonta.Text + "'";
            classSQL.update(sql);
            fillgrid();
            popuni_podatke();
            izmjeni = true;

            btnspremi.Visible = false; ;
            txtopiskonta.Enabled = false;
            cbvrstakonta.Enabled = false;
            cbstatus.Enabled = false;
            btndesno.Enabled = true;
            btnlijevo.Enabled = true;
            btntrazikonto.Enabled = true;
            txtsifrakonta.Enabled = true;

            btnizmjeni.Text = "Izmjeni konto";
        }

        private void popuni_podatke()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["br_konta"].FormattedValue.ToString() == txtsifrakonta.Text)
                {
                    // we have a match
                    try
                    {
                        cbvrstakonta.SelectedValue = row.Cells["vrsta_korisnika"].FormattedValue.ToString();
                        txtopiskonta.Text = row.Cells["opis"].FormattedValue.ToString();
                        cbstatus.SelectedValue = row.Cells["status"].FormattedValue.ToString();
                        break;
                    }
                    catch
                    { }
                }
                else
                {
                    cbvrstakonta.SelectedValue = row.Cells["vrsta_korisnika"].FormattedValue.ToString();
                    txtopiskonta.Text = "NE POSTOJI KONTO";
                    cbstatus.SelectedValue = row.Cells["status"].FormattedValue.ToString();
                }
            }
        }

        private void frmKontniPlan_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void btnnoviunos_Click(object sender, EventArgs e)
        {
            txtopiskonta.Enabled = true;

            cbvrstakonta.Enabled = true;

            cbstatus.Enabled = true;
            btndesno.Enabled = true;
            btnlijevo.Enabled = true;
            btntrazikonto.Enabled = true;
            txtsifrakonta.Enabled = true;
            txtsifrakonta.Text = "";
            txtopiskonta.Text = "";
            btnspreminovi.Visible = true;
            btnspremi.Visible = false;
            btnodustani.Visible = true;
            btnizmjeni.Visible = false;
            izmjeni = false;
        }

        private void spremi()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["br_konta"].FormattedValue.ToString() == txtsifrakonta.Text)
                {
                    MessageBox.Show("Broj konta več postoji !");
                    return;
                }
            }
            if (txtsifrakonta.Text == "")
            {
                MessageBox.Show("Da bi ste spremili novi konto potrebno je unjeti broj konta !");
                odustani();
                return;
            }
            else
            {
                string sql_spremi = "INSERT INTO kontni_plan VALUES('" + txtsifrakonta.Text + "', '" + txtopiskonta.Text + "', " +
        " '" + cbstatus.SelectedValue + "', 'D', '" + cbvrstakonta.SelectedValue + "')";
                classSQL.insert(sql_spremi);
            }
        }

        private void btnspreminovi_Click(object sender, EventArgs e)
        {
            btnspremi.Visible = false; ;
            txtopiskonta.Enabled = false;
            cbvrstakonta.Enabled = false;
            cbstatus.Enabled = false;
            btndesno.Enabled = true;
            btnlijevo.Enabled = true;
            btntrazikonto.Enabled = true;
            txtsifrakonta.Enabled = true;
            btnizmjeni.Visible = true;
            btnodustani.Visible = true;
            spremi();
            fillgrid();
        }

        private void btnodustani_Click(object sender, EventArgs e)
        {
            odustani();
        }

        private void odustani()
        {
            btnspremi.Visible = false;
            txtopiskonta.Enabled = false;
            cbvrstakonta.Enabled = false;
            cbstatus.Enabled = false;
            btndesno.Enabled = true;
            btnlijevo.Enabled = true;
            btnodustani.Visible = false;
            btntrazikonto.Enabled = true;
            txtsifrakonta.Enabled = true;
            btnspreminovi.Visible = false;
            btnizmjeni.Visible = true;
        }

        private void fill_konto(string konto)
        {
            string sql = "SELECT br_konta, opis FROM kontni_plan WHERE br_konta = '" + konto + "'";
            DataTable DTkonto = classSQL.select(sql, "konto").Tables[0];

            txtsifrakonta.Text = DTkonto.Rows[0]["br_konta"].ToString();
            txtopiskonta.Text = DTkonto.Rows[0]["opis"].ToString();
        }

        private void btntrazikonto_Click(object sender, EventArgs e)
        {
            Sifarnik.frmTraziKonto trazi = new Sifarnik.frmTraziKonto();
            trazi.ShowDialog();

            if (Properties.Settings.Default.br_konta != "")
            {
                fill_konto(Properties.Settings.Default.br_konta);
            }
        }
    }
}