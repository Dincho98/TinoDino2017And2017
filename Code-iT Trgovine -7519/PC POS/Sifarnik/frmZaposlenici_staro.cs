using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Sifarnik.Staro
{
    public partial class frmZaposlenici_staro : Form
    {
        public frmZaposlenici_staro()
        {
            InitializeComponent();
        }

        private DataSet DS = new DataSet();
        private bool BoolNovi = false;

        private void frmZaposlenici_staro_Load(object sender, EventArgs e)
        {
            SetDGV();
            SetCb();
            btnSpremi.Enabled = false;
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void SetDGV()
        {
            if (dgv.Rows.Count != 0)
            {
                dgv.Rows.Clear();
            }
            DS = new DataSet();

            string sql = "SELECT " +
                " zaposlenici.id_zaposlenik," +
                " zaposlenici.ime," +
                " zaposlenici.prezime," +
                " zaposlenici.id_grad," +
                " zaposlenici.adresa," +
                " zaposlenici.id_dopustenje," +
                " zaposlenici.oib," +
                " zaposlenici.tel," +
                " zaposlenici.datum_rodenja," +
                " zaposlenici.email," +
                " zaposlenici.mob," +
                " zaposlenici.aktivan," +
                " zaposlenici.id_zaposlenik," +
                " zaposlenici.zaporka," +
                " grad.grad," +
                " dopustenja.naziv" +
                " FROM zaposlenici " +
                " LEFT JOIN dopustenja ON dopustenja.id_dopustenje=zaposlenici.id_dopustenje " +
                " LEFT JOIN grad ON grad.id_grad=zaposlenici.id_grad";
            DS = classSQL.select(sql, "zaposlenici");

            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                dgv.Rows.Add(DS.Tables[0].Rows[i]["id_zaposlenik"].ToString(),
                    DS.Tables[0].Rows[i]["ime"].ToString(),
                    DS.Tables[0].Rows[i]["prezime"].ToString(),
                    DS.Tables[0].Rows[i]["grad"].ToString(),
                    DS.Tables[0].Rows[i]["adresa"].ToString(),
                    DS.Tables[0].Rows[i]["oib"].ToString(),
                    DS.Tables[0].Rows[i]["tel"].ToString(),
                    DS.Tables[0].Rows[i]["mob"].ToString(),
                    DS.Tables[0].Rows[i]["email"].ToString(),
                    DS.Tables[0].Rows[i]["datum_rodenja"].ToString(),
                    DS.Tables[0].Rows[i]["zaporka"].ToString(),
                    DS.Tables[0].Rows[i]["naziv"].ToString(),
                    DS.Tables[0].Rows[i]["aktivan"].ToString(),
                    DS.Tables[0].Rows[i]["id_grad"].ToString(),
                    DS.Tables[0].Rows[i]["id_dopustenje"].ToString(),
                    DS.Tables[0].Rows[i]["id_zaposlenik"].ToString());
            }
        }

        private void SetValueInTextBox()
        {
            txtIme.Text = dgv.CurrentRow.Cells["ime"].FormattedValue.ToString();
            txtPrezime.Text = dgv.CurrentRow.Cells["prezime"].FormattedValue.ToString();
            txtAdresa.Text = dgv.CurrentRow.Cells["adresa"].FormattedValue.ToString();
            txtTelefon.Text = dgv.CurrentRow.Cells["tel"].FormattedValue.ToString();
            txtMobitel.Text = dgv.CurrentRow.Cells["mobitel"].FormattedValue.ToString();
            txtEmail.Text = dgv.CurrentRow.Cells["email"].FormattedValue.ToString();
            txtOib.Text = dgv.CurrentRow.Cells["oib"].FormattedValue.ToString();
            dtpRoden.Value = Convert.ToDateTime(dgv.CurrentRow.Cells["roden"].FormattedValue.ToString());
            txtZaporka.Text = dgv.CurrentRow.Cells["zaporka"].FormattedValue.ToString();
            cbDopustenje.SelectedValue = dgv.CurrentRow.Cells["id_grad"].FormattedValue.ToString();
            cbGrad.SelectedValue = dgv.CurrentRow.Cells["id_dopustenje"].FormattedValue.ToString();

            cbGrad.SelectedValue = dgv.CurrentRow.Cells["id_grad"].FormattedValue.ToString();
            cbDopustenje.SelectedValue = dgv.CurrentRow.Cells["id_dopustenje"].FormattedValue.ToString();

            if (dgv.CurrentRow.Cells["aktivan"].FormattedValue.ToString() == "DA")
            {
                chbAktivnost.Checked = true;
            }
            else
            {
                chbAktivnost.Checked = false;
            }
        }

        private void SetCb()
        {
            //CB grad
            DataSet DSgrad = classSQL.select("SELECT DISTINCT (grad),id_grad FROM grad ORDER BY grad ", "grad");
            cbGrad.DataSource = DSgrad.Tables[0];
            cbGrad.DisplayMember = "grad";
            cbGrad.ValueMember = "id_grad";
            cbGrad.SelectedValue = "2086";

            //CB dražave
            DataSet DSdop = classSQL.select("SELECT * FROM dopustenja ORDER BY naziv", "dopustenja");
            cbDopustenje.DataSource = DSdop.Tables[0];
            cbDopustenje.DisplayMember = "naziv";
            cbDopustenje.ValueMember = "id_dopustenje";

            SetValueInTextBox();
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            if (BoolNovi == false)
            {
                UpdateZap();
                btnSpremi.Enabled = false;
                btnNovo.Enabled = true;
            }
            else
            {
                Spremaj();
                btnSpremi.Enabled = false;
                btnNovo.Enabled = true;
            }
        }

        private void UpdateZap()
        {
            string sql = "UPDATE zaposlenici SET " +
        " ime='" + txtIme.Text + "'," +
        " prezime='" + txtPrezime.Text + "'," +
        " id_grad='" + cbGrad.SelectedValue + "'," +
        " adresa='" + txtAdresa.Text + "'," +
        " id_dopustenje='" + cbDopustenje.SelectedValue + "'," +
        " oib='" + txtOib.Text + "'," +
        " tel='" + txtTelefon.Text + "'," +
        " datum_rodenja='" + Convert.ToDateTime(dtpRoden.Value).ToString("yyyy-MM-dd H:mm:ss") + "'," +
        " email='" + txtEmail.Text + "'," +
        " mob='" + txtMobitel.Text + "'," +
        " aktivan='" + GetValueCheckBox() + "'," +
        " zaporka='" + txtZaporka.Text + "'" +
        " WHERE id_zaposlenik='" + dgv.CurrentRow.Cells["id_zaposlenik"].FormattedValue.ToString() + "'";

            classSQL.update(sql);
            SetDGV();
            BoolNovi = false;
        }

        private void Spremaj()
        {
            if (MessageBox.Show("Nakon unosa ovog zaposlenika nećete imati više mogućnosti izbrisati istog.", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "INSERT INTO zaposlenici (" +
                " ime,prezime,id_grad,adresa,id_dopustenje,oib,tel,datum_rodenja,email,mob,aktivan,zaporka" +
                ") VALUES (" +
                " '" + txtIme.Text + "'," +
                " '" + txtPrezime.Text + "'," +
                " '" + cbGrad.SelectedValue + "'," +
                " '" + txtAdresa.Text + "'," +
                " '" + cbDopustenje.SelectedValue + "'," +
                " '" + txtOib.Text + "'," +
                " '" + txtTelefon.Text + "'," +
                " '" + dtpRoden.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " '" + txtEmail.Text + "'," +
                " '" + txtMobitel.Text + "'," +
                " '" + GetValueCheckBox() + "'," +
                " '" + txtZaporka.Text + "'" +
                " )";

                classSQL.insert(sql);
                BoolNovi = false;
                SetDGV();
            }
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

        private void btnNovo_Click(object sender, EventArgs e)
        {
            DelFields();
            BoolNovi = true;
            dgv.Enabled = false;
            btnSpremi.Enabled = true;
            btnNovo.Enabled = false;
        }

        private void DelFields()
        {
            txtIme.Text = "";
            txtPrezime.Text = "";
            txtAdresa.Text = "";
            txtOib.Text = "";
            txtTelefon.Text = "";
            dtpRoden.Value = DateTime.Now;
            txtEmail.Text = "";
            txtMobitel.Text = "";
            txtZaporka.Text = "";
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.Rows.Count < 1)
                return;
            SetValueInTextBox();
            btnSpremi.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (dgv.Rows.Count != 0)
            {
                dgv.Rows.Clear();
            }
            DS = new DataSet();

            string sql = "SELECT " +
                " zaposlenici.id_zaposlenik," +
                " zaposlenici.ime," +
                " zaposlenici.prezime," +
                " zaposlenici.id_grad," +
                " zaposlenici.adresa," +
                " zaposlenici.id_dopustenje," +
                " zaposlenici.oib," +
                " zaposlenici.tel," +
                " zaposlenici.datum_rodenja," +
                " zaposlenici.email," +
                " zaposlenici.mob," +
                " zaposlenici.aktivan," +
                " zaposlenici.id_zaposlenik," +
                " zaposlenici.zaporka," +
                " grad.grad," +
                " dopustenja.naziv" +
                " FROM zaposlenici " +
                " LEFT JOIN dopustenja ON dopustenja.id_dopustenje=zaposlenici.id_dopustenje " +
                " LEFT JOIN grad ON grad.id_grad=zaposlenici.id_grad WHERE ime LIKE '%" + textBox1.Text + "%'";
            DS = classSQL.select(sql, "zaposlenici");

            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                dgv.Rows.Add(DS.Tables[0].Rows[i]["id_zaposlenik"].ToString(),
                    DS.Tables[0].Rows[i]["ime"].ToString(),
                    DS.Tables[0].Rows[i]["prezime"].ToString(),
                    DS.Tables[0].Rows[i]["grad"].ToString(),
                    DS.Tables[0].Rows[i]["adresa"].ToString(),
                    DS.Tables[0].Rows[i]["oib"].ToString(),
                    DS.Tables[0].Rows[i]["tel"].ToString(),
                    DS.Tables[0].Rows[i]["mob"].ToString(),
                    DS.Tables[0].Rows[i]["email"].ToString(),
                    DS.Tables[0].Rows[i]["datum_rodenja"].ToString(),
                    DS.Tables[0].Rows[i]["zaporka"].ToString(),
                    DS.Tables[0].Rows[i]["naziv"].ToString(),
                    DS.Tables[0].Rows[i]["aktivan"].ToString(),
                    DS.Tables[0].Rows[i]["id_grad"].ToString(),
                    DS.Tables[0].Rows[i]["id_dopustenje"].ToString(),
                    DS.Tables[0].Rows[i]["id_zaposlenik"].ToString());
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            BoolNovi = false;
            dgv.Enabled = true;
            btnNovo.Enabled = true;
            SetDGV();
        }
    }
}