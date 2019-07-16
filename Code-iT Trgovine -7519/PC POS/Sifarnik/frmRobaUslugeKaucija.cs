using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmRobaUslugeKaucija : Form
    {
        private DataTable DTRoba = new DataTable();

        private string id_roba;
        public string sifra;

        public frmRobaUslugeKaucija()
        {
            InitializeComponent();
        }

        private void frmRobaUsluge_Load(object sender, EventArgs e)
        {
            this.Text = "Kaucija za artikl " + sifra;
            fillRobaKaucija();
        }

        private void fillRobaKaucija()
        {
            string sql = "SELECT id_stavka, sifra_kaucija," +
                " kolicina, roba.naziv FROM roba, roba_kaucija WHERE roba_kaucija.sifra='" + sifra + "'" +
                " AND roba.sifra=roba_kaucija.sifra_kaucija";
            DataTable DTkaucija = classSQL.select(sql, "roba_kaucija").Tables[0];

            if (DTkaucija.Rows.Count > 0)
            {
                for (int i = 0; i < DTkaucija.Rows.Count; i++)
                {
                    dgw.Rows.Add(
                        DTkaucija.Rows[i]["id_stavka"].ToString(),
                        i + 1,
                        DTkaucija.Rows[i]["naziv"].ToString(),
                        DTkaucija.Rows[i]["sifra_kaucija"].ToString(),
                        DTkaucija.Rows[i]["kolicina"].ToString()
                    );
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dgw.RowCount == 0)
            {
                return;
            }

            if (dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].Value != null)
            {
                if (MessageBox.Show("Da li ste sigurni da želite obrisati ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    int i = dgw.CurrentRow.Index;
                    classSQL.delete("DELETE FROM roba_kaucija WHERE sifra='" + sifra + "'" +
                        " AND sifra_kaucija = '" + dgw.Rows[dgw.CurrentRow.Index].Cells["sifra_kaucija"].FormattedValue.ToString() + "'");

                    dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                    classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) " +
                        "VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" +
                        DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje stavke sa roba i usluge kaucija na šifri." + sifra + "')");
                }
            }
            else
            {
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
            }

            SrediBr();
        }

        private void btnOpenRoba_Click(object sender, EventArgs e)
        {
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            roba_trazi.ShowDialog();
            string propertis_sifra = Properties.Settings.Default.id_roba.Trim();
            if (propertis_sifra != "")
            {
                for (int y = 0; y < dgw.Rows.Count; y++)
                {
                    if (propertis_sifra == dgw.Rows[y].Cells["sifra_kaucija"].FormattedValue.ToString())
                    {
                        MessageBox.Show("Artikl ili usluga već postoje!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string sql = "SELECT " +
                    " roba.sifra," +
                    " roba.naziv" +
                    " FROM roba" +
                    " WHERE roba.sifra='" + propertis_sifra + "'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];

                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();
                    txtSifra_robe.Text = "";
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TRENUTNI_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox txt = ((TextBox)sender);
                txt.BackColor = Color.Khaki;
            }
            else if (sender is ComboBox)
            {
                ComboBox txt = ((ComboBox)sender);
                txt.BackColor = Color.Khaki;
            }
            else if (sender is DateTimePicker)
            {
                DateTimePicker control = ((DateTimePicker)sender);
                control.BackColor = Color.Khaki;
            }
            else if (sender is NumericUpDown)
            {
                NumericUpDown control = ((NumericUpDown)sender);
                control.BackColor = Color.Khaki;
            }
        }

        private void NAPUSTENI_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox txt = ((TextBox)sender);
                txt.BackColor = Color.White;
            }
            else if (sender is ComboBox)
            {
                ComboBox txt = ((ComboBox)sender);
                txt.BackColor = Color.White;
                //txt.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            else if (sender is DateTimePicker)
            {
                DateTimePicker control = ((DateTimePicker)sender);
                control.BackColor = Color.White;
            }
            else if (sender is NumericUpDown)
            {
                NumericUpDown control = ((NumericUpDown)sender);
                control.BackColor = Color.White;
            }
        }

        private void txtSifra_robe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSifra_robe.Text.Trim() == "")
                {
                    frmRobaTrazi roba_trazi = new frmRobaTrazi();
                    roba_trazi.ShowDialog();
                    string propertis_sifra = Properties.Settings.Default.id_roba.Trim();
                    if (propertis_sifra != "")
                    {
                        txtSifra_robe.Text = propertis_sifra;
                    }
                }

                if (txtSifra_robe.Text == "")
                    return;

                for (int y = 0; y < dgw.Rows.Count; y++)
                {
                    if (txtSifra_robe.Text == dgw.Rows[y].Cells["sifra_kaucija"].FormattedValue.ToString())
                    {
                        MessageBox.Show("Artikl ili usluga već postoje!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string sql = "SELECT " +
                    " roba.sifra," +
                    " roba.naziv" +
                    " FROM roba" +
                    " WHERE roba.sifra='" + txtSifra_robe.Text.Trim() + "'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();
                    dgw.Rows[dgw.Rows.Count - 1].Cells["kolicina"].Selected = true;
                    txtSifra_robe.Text = "";
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SetRoba()
        {
            dgw.Rows.Add();
            int br = dgw.Rows.Count - 1;

            dgw.Select();
            dgw.CurrentCell = dgw.Rows[br].Cells["kolicina"];
            dgw.BeginEdit(true);

            dgw.Rows[br].Cells["sifra_kaucija"].Value = DTRoba.Rows[0]["sifra"].ToString();
            dgw.Rows[br].Cells["naziv"].Value = DTRoba.Rows[0]["naziv"].ToString();
            dgw.Rows[br].Cells["kolicina"].Value = "1";

            SrediBr();
        }

        private void SrediBr()
        {
            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                dgw.Rows[i].Cells["br"].Value = i + 1;
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            if (dgw.Rows.Count == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku za spremiti.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Spremi();
            MessageBox.Show("Spremljeno.");
        }

        private void Spremi()
        {
            for (int i = 0; i < dgw.RowCount; i++)
            {
                if (dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString() == "")
                {
                    string sql = "INSERT INTO roba_kaucija" +
                        " (sifra,sifra_kaucija,kolicina) VALUES " +
                        " (" +
                        " '" + sifra + "'," +
                        " '" + dgw.Rows[i].Cells["sifra_kaucija"].Value + "'," +
                        " '" + dgw.Rows[i].Cells["kolicina"].FormattedValue + "'" +
                        " )";
                    provjera_sql(classSQL.insert(sql));
                }
                else
                {
                    string sql = "UPDATE roba_kaucija SET kolicina='" + dgw.Rows[i].Cells["kolicina"].FormattedValue + "'" +
                        " WHERE sifra='" + sifra + "' AND" +
                        " sifra_kaucija='" + dgw.Rows[i].Cells["sifra_kaucija"].Value + "'";
                    provjera_sql(classSQL.insert(sql));
                }
            }

            classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) " +
                "VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" +
                DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Spremanje kaucije na artiklu " + sifra + "')");
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.Rows.Count < 1)
                return;
            dgw.BeginEdit(true);
        }

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.CurrentCell.ColumnIndex == 4)
            {
                try
                {
                    if (isNumeric(dgw.CurrentRow.Cells["kolicina"].FormattedValue.ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) == false)
                    {
                        dgw.CurrentRow.Cells["kolicina"].Value = "1"; MessageBox.Show("Greška kod upisa količine.", "Greška");
                    }
                    txtSifra_robe.Text = "";
                    txtSifra_robe.Select();
                }
                catch (Exception)
                {
                    dgw.BeginEdit(true);
                }
            }
        }

        public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle,
                System.Globalization.CultureInfo.CurrentCulture, out result);
        }
    }
}