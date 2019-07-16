using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmZapisnikopromjeniCijene : Form
    {
        public frmZapisnikopromjeniCijene()
        {
            InitializeComponent();
        }

        private DataTable DTRoba = new DataTable();
        private DataTable DTRoba1 = new DataTable();
        private DataTable DTIzradio = new DataTable();
        public bool editbit { get; set; }
        public string broj_edit { get; set; }
        private DataTable DT_Skladiste = new DataTable();
        public string broj_pocetno_edit { get; set; }
        public frmMenu MainForm { get; set; }

        private void frmZapisnikopromjeniCijene_Load(object sender, EventArgs e)
        {
            EnableDisable(false);
            SetCB();
            ControlDisableEnable(1, 0, 0, 1, 0);
            if (editbit)
            {
                txtBrojInventure.Text = broj_edit;
                System.Windows.Forms.SendKeys.Send("{ENTER}");
                editbit = false;
            }
            else
            {
                txtBrojInventure.Text = brojPromjene();
            }

            txtBrojInventure.ReadOnly = false;
            nmGodinaInventure.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nmGodinaInventure.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nmGodinaInventure.Value = DateTime.Now.Year;
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 500);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void txtSifra_robe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                for (int y = 0; y < dgw.Rows.Count; y++)
                {
                    if (txtSifra_robe.Text == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString().Trim())
                    {
                        MessageBox.Show("Artikl ili usluga već postoje!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string sql = string.Format("SELECT * FROM roba_prodaja WHERE sifra = '{0}' AND id_skladiste = '{1}';", txtSifra_robe.Text, cbSkladiste.SelectedValue);
                DTRoba = classSQL.select(sql, "roba_prodaja").Tables[0];

                string sql1 = string.Format("SELECT * FROM roba WHERE sifra = '{0}';", txtSifra_robe.Text);
                DTRoba1 = classSQL.select(sql1, "roba").Tables[0];

                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SetRoba()
        {
            try
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;
                dgw.Select();
                dgw.CurrentCell = dgw.Rows[br].Cells[4];

                double vpc = Convert.ToDouble(DTRoba.Rows[0]["vpc"].ToString());
                double pdv = Convert.ToDouble(DTRoba.Rows[0]["porez"].ToString());

                dgw.Rows[br].Cells[0].Value = dgw.RowCount;
                dgw.Rows[br].Cells["sifra"].Value = DTRoba.Rows[0]["sifra"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = DTRoba1.Rows[0]["naziv"].ToString();
                dgw.Rows[br].Cells["stara_cijena"].Value = (vpc * pdv / 100) + vpc;
                dgw.Rows[br].Cells["postotak"].Value = "0";
                dgw.Rows[br].Cells["nova_cijena"].Value = (vpc * pdv / 100) + vpc;
                dgw.Rows[br].Cells["pdv"].Value = pdv;
                dgw.Rows[br].Cells["iznos"].Value = (vpc * pdv / 100) + vpc;

                if (DTRoba.Rows[0]["kolicina"] != null)
                {
                    dgw.Rows[br].Cells["kolicina"].Value = DTRoba.Rows[0]["kolicina"].ToString();
                }
                else
                {
                    dgw.Rows[br].Cells["kolicina"].Value = 0;
                }

                dgw.BeginEdit(true);
                PaintRows(dgw);
            }
            catch
            {
            }
        }

        private void SetCB()
        {
            DTIzradio = classSQL.select(string.Format("SELECT ime + ' ' + prezime as Ime, id_zaposlenik  FROM zaposlenici WHERE id_zaposlenik = '{0}';", Properties.Settings.Default.id_zaposlenik), "zaposlenici").Tables[0];
            txtIzradio.Text = DTIzradio.Rows[0]["Ime"].ToString();
            DT_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste").Tables[0];
            cbSkladiste.DataSource = DT_Skladiste;
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.Rows.Count < 1)
                return;
            dgw.BeginEdit(true);
        }

        private void SetRabat()
        {
            int br = dgw.CurrentRow.Index;

            double mpc = Convert.ToDouble(dgw.Rows[br].Cells["stara_cijena"].FormattedValue.ToString());
            double postotak = Convert.ToDouble(dgw.Rows[br].Cells["postotak"].FormattedValue.ToString());

            dgw.Rows[br].Cells["nova_cijena"].Value = (mpc * postotak / 100) + mpc;
            dgw.Rows[br].Cells["iznos"].Value = Convert.ToDouble(dgw.Rows[br].Cells["nova_cijena"].FormattedValue.ToString()) - Convert.ToDouble(dgw.Rows[br].Cells["stara_cijena"].FormattedValue.ToString());
        }

        private void SetNovaCijena()
        {
            int br = dgw.CurrentRow.Index;
            dgw.Rows[br].Cells["iznos"].Value = Convert.ToDouble(dgw.Rows[br].Cells["nova_cijena"].FormattedValue.ToString()) - Convert.ToDouble(dgw.Rows[br].Cells["stara_cijena"].FormattedValue.ToString());
            dgw.Rows[br].Cells["postotak"].Value = (Convert.ToDouble(dgw.Rows[br].Cells["nova_cijena"].FormattedValue.ToString()) / Convert.ToDouble(dgw.Rows[br].Cells["stara_cijena"].FormattedValue.ToString()) - 1) * 100;
        }

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (MouseButtons != 0) return;

            if (dgw.CurrentCell.ColumnIndex == 4)
            {
                try
                {
                    SetRabat();
                    dgw.CurrentCell = dgw.CurrentRow.Cells[5];
                    dgw.BeginEdit(true);
                }
                catch (Exception)
                { }
            }
            else if (dgw.CurrentCell.ColumnIndex == 5)
            {
                try
                {
                    SetNovaCijena();
                    txtSifra_robe.Text = "";
                    txtSifra_robe.Select();
                    dgw.ClearSelection();
                }
                catch (Exception)
                { }
            }
        }

        private void ControlDisableEnable(int novi, int odustani, int spremi, int sve, int delAll)
        {
            if (novi == 0)
            {
                btnNoviUnos.Enabled = false;
            }
            else if (novi == 1)
            {
                btnNoviUnos.Enabled = true;
            }

            if (odustani == 0)
            {
                btnOdustani.Enabled = false;
            }
            else if (odustani == 1)
            {
                btnOdustani.Enabled = true;
            }

            if (spremi == 0)
            {
                btnSpremi.Enabled = false;
            }
            else if (spremi == 1)
            {
                btnSpremi.Enabled = true;
            }

            if (sve == 0)
            {
                btnSveFakture.Enabled = false;
            }
            else if (sve == 1)
            {
                btnSveFakture.Enabled = true;
            }
        }

        private void DelField()
        {
            txtSifra_robe.Text = "";
            dgw.Rows.Clear();
        }

        private void EnableDisable(bool x)
        {
            txtSifra_robe.Enabled = x;
            cbSkladiste.Enabled = x;
            dtpDatum.Enabled = x;
            rtbNapomena.Enabled = x;
            btnObrisi.Enabled = x;

            if (x == true)
            {
                txtBrojInventure.Enabled = false;
                nmGodinaInventure.Enabled = false;
            }
            else if (x == false)
            {
                txtBrojInventure.Enabled = true;
                nmGodinaInventure.Enabled = true;
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            txtBrojInventure.Text = brojPromjene();
            EnableDisable(false);
            DelField();
            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private string brojPromjene()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj) FROM promjena_cijene", "promjena_cijene").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            frmSvePromjeneCijena prom = new frmSvePromjeneCijena();
            prom.MainForm = this;
            this.Close();
            prom.ShowDialog();
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            txtBrojInventure.Text = brojPromjene();
            EnableDisable(true);
            ControlDisableEnable(0, 1, 1, 0, 1);
        }

        private void txtBrojInventure_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select(string.Format("SELECT broj FROM promjena_cijene WHERE broj = '{0}';", txtBrojInventure.Text), "pocetno_stanje").Tables[0];
                DelField();
                if (DT.Rows.Count == 0)
                {
                    if (brojPromjene() == txtBrojInventure.Text.Trim())
                    {
                        DelField();
                        EnableDisable(true);
                        btnSveFakture.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    broj_pocetno_edit = txtBrojInventure.Text;
                    FillPocetnoStanje(broj_pocetno_edit);
                    EnableDisable(true);
                }

                ControlDisableEnable(0, 1, 1, 0, 1);
            }
        }

        private void FillPocetnoStanje(string broj)
        {
            DataTable DTheader = classSQL.select(string.Format("SELECT * FROM promjena_cijene WHERE broj = '{0}';", broj), "promjena_cijene").Tables[0];
            if (DTheader.Rows.Count > 0)
            {
                cbSkladiste.SelectedValue = DTheader.Rows[0]["id_skladiste"].ToString();
                rtbNapomena.Text = DTheader.Rows[0]["napomena"].ToString();
                dtpDatum.Value = Convert.ToDateTime(DTheader.Rows[0]["date"].ToString());
                txtIzradio.Text = classSQL.select(string.Format("SELECT ime + ' ' + prezime as Ime FROM zaposlenici WHERE id_zaposlenik = '{0}';", DTheader.Rows[0]["id_izradio"].ToString()), "zaposlenici").Tables[0].Rows[0][0].ToString();
            }

            string sql = string.Format(@"SELECT promjena_cijene_stavke.sifra,
promjena_cijene_stavke.id_stavka,
promjena_cijene_stavke.stara_cijena,
promjena_cijene_stavke.pdv,
promjena_cijene_stavke.nova_cijena,
promjena_cijene_stavke.postotak,
promjena_cijene_stavke.kolicina,
roba.naziv,
roba.jm
FROM promjena_cijene_stavke
LEFT JOIN roba ON roba.sifra=promjena_cijene_stavke.sifra
WHERE promjena_cijene_stavke.broj = '{0}'", broj);

            DataTable DT = classSQL.select(sql, "promjena_cijene_stavke").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;
                dgw.Select();
                dgw.CurrentCell = dgw.Rows[br].Cells[3];

                dgw.Rows[br].Cells[0].Value = dgw.RowCount;
                dgw.Rows[br].Cells["sifra"].Value = DT.Rows[i]["sifra"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = DT.Rows[i]["naziv"].ToString();
                dgw.Rows[br].Cells["stara_cijena"].Value = DT.Rows[i]["stara_cijena"].ToString();
                dgw.Rows[br].Cells["nova_cijena"].Value = DT.Rows[i]["nova_cijena"].ToString();
                dgw.Rows[br].Cells["postotak"].Value = DT.Rows[i]["postotak"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = DT.Rows[i]["naziv"].ToString();
                dgw.Rows[br].Cells["id_stavka"].Value = DT.Rows[i]["id_stavka"].ToString();
                dgw.Rows[br].Cells["pdv"].Value = DT.Rows[i]["pdv"].ToString();
                dgw.Rows[br].Cells["iznos"].Value = Convert.ToDouble(DT.Rows[i]["nova_cijena"].ToString()) - Convert.ToDouble(DT.Rows[i]["stara_cijena"].ToString());
                dgw.Rows[br].Cells["kolicina"].Value = DT.Rows[i]["kolicina"].ToString();
            }
            //edit = true;
            PaintRows(dgw);
        }

        private void Spremi()
        {
            DataTable DTboll = classSQL.select(string.Format("SELECT broj FROM promjena_cijene WHERE broj = '{0}';", txtBrojInventure.Text), "promjena_cijene").Tables[0];
            if (DTboll.Rows.Count == 0)
            {
                txtBrojInventure.Text = brojPromjene();
                string s = string.Format(@"INSERT INTO promjena_cijene (broj, id_skladiste, date, id_izradio, napomena) VALUES
( '{0}', '{1}', '{2}', '{3}', '{4}' );",
                    txtBrojInventure.Text,
                    cbSkladiste.SelectedValue,
                    dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss"),
                    Properties.Settings.Default.id_zaposlenik,
                    rtbNapomena.Text);
                classSQL.insert(s);

                classSQL.insert(string.Format("INSERT INTO aktivnost_zaposlenici (id_zaposlenik, datum, radnja) VALUES ('{0}','{1}','Novi zapisnik o promjeni cijene br. {2}')",
                    Properties.Settings.Default.id_zaposlenik,
                    DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"),
                    txtBrojInventure.Text));
            }
            else
            {
                string s = string.Format(@"UPDATE promjena_cijene
SET
    broj = '{0}', id_skladiste = '{1}', date = '{2}', napomena = '{3}'
WHERE broj = '{4}';",
                txtBrojInventure.Text,
                cbSkladiste.SelectedValue,
                dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss"),
                rtbNapomena.Text,
                txtBrojInventure.Text);

                classSQL.update(s);

                classSQL.insert(string.Format("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('{0}', '{1}', 'Ažuriranje zapisnika o promjeni cijene br. {2}');",
                    Properties.Settings.Default.id_zaposlenik,
                    DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"),
                    txtBrojInventure.Text));
            }

            for (int i = 0; i < dgw.RowCount; i++)
            {
                double pdv = double.Parse(dgw.Rows[i].Cells["pdv"].FormattedValue.ToString());
                double stara = double.Parse(dgw.Rows[i].Cells["stara_cijena"].FormattedValue.ToString());
                double nova = double.Parse(dgw.Rows[i].Cells["nova_cijena"].FormattedValue.ToString());

                string StrStara = stara.ToString();
                string StrNova = nova.ToString();

                if (classSQL.remoteConnectionString == "")
                {
                    StrStara = StrStara.Replace(",", ".");
                    StrNova = StrNova.Replace(",", ".");
                }
                else
                {
                    StrStara = StrStara.Replace(".", ",");
                    StrNova = StrNova.Replace(".", ",");
                }

                if (dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString() == "")
                {
                    string s1 = string.Format(@"INSERT INTO promjena_cijene_stavke (stara_cijena, nova_cijena, pdv, sifra, postotak, broj, kolicina) VALUES
( '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' )",
                        StrStara,
                        StrNova,
                        pdv.ToString(),
                        dgw.Rows[i].Cells["sifra"].FormattedValue.ToString(),
                        dgw.Rows[i].Cells["postotak"].FormattedValue.ToString(),
                        txtBrojInventure.Text,
                        dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString());

                    classSQL.insert(s1);
                }
                else
                {
                    string s2 = string.Format(@"UPDATE promjena_cijene_stavke
SET
    stara_cijena = '{0}', nova_cijena = '{1}',
    pdv = '{2}', sifra = '{3}',
    postotak = '{4}', kolicina = '{5}'
WHERE id_stavka = '{6}';",
                        StrStara,
                        StrNova,
                        pdv.ToString(),
                        dgw.Rows[i].Cells["sifra"].FormattedValue.ToString(),
                        dgw.Rows[i].Cells["postotak"].FormattedValue.ToString(),
                        dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString(),
                        dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString());

                    classSQL.update(s2);
                }
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            Class.ZapisnikPromjeneCijene _zapisnik = new Class.ZapisnikPromjeneCijene(dgw);
            _zapisnik.ZapisnikSpremi(txtBrojInventure.Text, cbSkladiste.SelectedValue.ToString(), dtpDatum.Value, rtbNapomena.Text);

            for (int i = 0; i < dgw.RowCount; i++)
            {
                double pdv = double.Parse(dgw.Rows[i].Cells["pdv"].FormattedValue.ToString());
                double vpc = double.Parse(dgw.Rows[i].Cells["nova_cijena"].FormattedValue.ToString());
                vpc = vpc / (1 + pdv / 100);

                double mpc = double.Parse(dgw.Rows[i].Cells["nova_cijena"].FormattedValue.ToString());

                string StrMpc = mpc.ToString();
                string StrVpc = vpc.ToString("#0.000");

                if (classSQL.remoteConnectionString == "")
                {
                    StrMpc = StrMpc.Replace(",", ".");
                    StrVpc = StrVpc.Replace(",", ".");
                }
                else
                {
                    StrMpc = StrMpc.Replace(".", ",");
                    StrVpc = StrVpc.Replace(".", ",");
                }

                string sql = string.Format(@"UPDATE roba
SET
    vpc = '{0}', mpc = '{1}'
WHERE sifra = '{2}';",
    StrVpc.Replace(",", "."),
    StrMpc,
    dgw.Rows[i].Cells["sifra"].FormattedValue.ToString());

                classSQL.update(sql);

                DataTable DT = classSQL.select(string.Format("SELECT sifra FROM roba_prodaja WHERE sifra = '{0}' AND id_skladiste = '{1}';",
                    dgw.Rows[i].Cells["sifra"].FormattedValue.ToString(),
                    cbSkladiste.SelectedValue.ToString()), "roba_prodaja").Tables[0];

                if (DT.Rows.Count != 0)
                {
                    string sql1 = string.Format(@"UPDATE roba_prodaja SET vpc = '{0}' WHERE sifra = '{1}' AND id_skladiste = '{2}';",
                        StrVpc.Replace(",", "."),
                        dgw.Rows[i].Cells["sifra"].FormattedValue.ToString(),
                        cbSkladiste.SelectedValue.ToString());

                    classSQL.update(sql1);
                }
            }

            EnableDisable(false);

            if (MessageBox.Show("Spremljeno.\r\nŽelite li ispisati dokument?", "Spremljeno.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                printaj(txtBrojInventure.Text);
            }

            DelField();
            txtBrojInventure.Text = brojPromjene();
            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private void printaj(string broj)
        {
            Report.Liste.frmListe pr = new Report.Liste.frmListe();
            pr.dokument = "promjena_cijene";
            pr.broj_dokumenta = broj;
            pr.ImeForme = "Zapisnik o promjeni cijene";
            pr.ShowDialog();
        }

        private void btnOpenRoba_Click_1(object sender, EventArgs e)
        {
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            roba_trazi.ShowDialog();
            string propertis_sifra = Properties.Settings.Default.id_roba.Trim();
            if (propertis_sifra != "")
            {
                for (int y = 0; y < dgw.Rows.Count; y++)
                {
                    if (propertis_sifra == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString())
                    {
                        MessageBox.Show("Artikl ili usluga već postoje!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string sql = string.Format("SELECT * FROM roba_prodaja WHERE sifra = '{0}' AND id_skladiste = '{1}';",
                    propertis_sifra,
                    cbSkladiste.SelectedValue);

                DTRoba = classSQL.select(sql, "roba_prodaja").Tables[0];

                string sql1 = string.Format("SELECT * FROM roba WHERE sifra = '{0}';", propertis_sifra);
                DTRoba1 = classSQL.select(sql1, "roba").Tables[0];

                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                classSQL.delete(string.Format("DELETE FROM promjena_cijene_stavke WHERE id_stavka = '{0}';",
                    dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString()));

                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);

                classSQL.insert(string.Format("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('{0}', '{1}', 'Brisanje stavke sa zapisnika o promjeni cijene br. {3}');",
                    Properties.Settings.Default.id_zaposlenik,
                    DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"),
                    txtBrojInventure.Text));
            }
            else
            {
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
            }
        }
    }
}