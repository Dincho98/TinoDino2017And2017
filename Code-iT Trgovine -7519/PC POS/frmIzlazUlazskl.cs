using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmIzlazUlazskl : Form
    {
        public frmIzlazUlazskl()
        {
            InitializeComponent();
        }

        private void frmIzlazUlazskl_Load(object sender, EventArgs e)
        {
            Set();
        }

        private void Set()
        {
            DataTable DTSK = new DataTable("Izlracuni");

            DTSK.Columns.Add("id", typeof(string));
            DTSK.Columns.Add("naziv", typeof(string));

            DTSK.Rows.Add("USKLA", "Ulaz u skladište po artiklu");
            DTSK.Rows.Add("ISKLA", "Izlaz iz skladišta po artiklu");

            comboBox1.DataSource = DTSK;

            comboBox1.DisplayMember = "naziv";

            comboBox1.ValueMember = "id";

            DataTable DTSK1 = new DataTable("Izlracuni1");

            string sql = "SELECT * FROM skladiste";
            DTSK1 = classSQL.select(sql, "skl").Tables[0];

            comboBox2.DataSource = DTSK1;

            comboBox2.DisplayMember = "skladiste";

            comboBox2.ValueMember = "id_skladiste";

            comboBox3.DataSource = DTSK1;

            comboBox3.DisplayMember = "skladiste";

            comboBox3.ValueMember = "id_skladiste";
        }

        private DataTable DTRoba = new DataTable();

        private void btnOpenRoba_Click(object sender, EventArgs e)
        {
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            roba_trazi.ShowDialog();
            string propertis_sifra = Properties.Settings.Default.id_roba.Trim();
            if (propertis_sifra != "")
            {
                string sql = "SELECT * FROM roba WHERE sifra='" + propertis_sifra + "'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    txtSifra_robe.Text = propertis_sifra;
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            if (txtSifra_robe.Text != "")
            {
                int rows = Convert.ToInt32(classSQL.select("Select count(*) From roba Where sifra = '" + txtSifra_robe.Text + "'", "redovi").Tables[0].Rows[0][0].ToString());
                if (rows > 0)
                {
                    if (comboBox1.SelectedValue.ToString() == "USKLA")
                    {
                        Report.UlazIzlaz.frmUlaz UI = new Report.UlazIzlaz.frmUlaz();
                        //Report.UlazIzlaz.frmSklulazizlaz UI = new Report.UlazIzlaz.frmSklulazizlaz();
                        UI.artikl = txtSifra_robe.Text;
                        UI.datumOD = dateTimePicker1.Value.Date;
                        UI.datumDO = dateTimePicker2.Value.Date;
                        UI.bool1 = cbsklkalk.Checked;
                        UI.skladiste_odabir = comboBox2.SelectedValue.ToString();
                        UI.skladiste = comboBox2.Text;
                        UI.documenat = comboBox1.SelectedValue.ToString();
                        UI.ShowDialog();
                    }
                    else if (comboBox1.SelectedValue.ToString() == "ISKLA")
                    {
                        Report.UlazIzlaz.frmUlaz UI = new Report.UlazIzlaz.frmUlaz();
                        UI.artikl = txtSifra_robe.Text;
                        UI.datumOD = dateTimePicker1.Value.Date;
                        UI.datumDO = dateTimePicker2.Value.Date;
                        UI.bool1 = cbsklkalk.Checked;
                        UI.skladiste_odabir = comboBox2.SelectedValue.ToString();
                        UI.skladiste = comboBox2.Text;
                        UI.documenat = comboBox1.SelectedValue.ToString();
                        UI.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Artikl koji ste upisali ne postoji !");
                }
            }
            else
            {
                MessageBox.Show("Niste unjeli šifru artikla !");
            }
        }

        private void txtSifra_robe_TextChanged(object sender, EventArgs e)
        {
            txtSifra_robe.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtSifra_robe.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            DataTable auto = classSQL.select("Select sifra From roba where sifra ~* '" + txtSifra_robe.Text + "' LIMIT 10", "autocomplete").Tables[0];
            for (int i = 0; i < auto.Rows.Count; i++)
            {
                col.Add(auto.Rows[i]["sifra"].ToString());
            }
            txtSifra_robe.AutoCompleteCustomSource = col;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Report.UlazIzlaz.frmSklulazizlaz UI = new Report.UlazIzlaz.frmSklulazizlaz();
            UI.artikl = txtSifra_robe.Text;
            UI.datumOD = dateTimePicker4.Value.Date;
            UI.datumDO = dateTimePicker3.Value.Date;
            UI.bool1 = cbsklkalk.Checked;
            UI.skladiste_odabir = comboBox3.SelectedValue.ToString();
            UI.skladiste = comboBox3.Text;
            UI.documenat = comboBox1.SelectedValue.ToString();
            UI.ShowDialog();
        }
    }
}