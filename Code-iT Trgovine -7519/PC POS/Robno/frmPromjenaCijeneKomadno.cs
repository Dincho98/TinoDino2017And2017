using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmPromjenaCijeneKomadno : Form
    {
        public frmPromjenaCijeneKomadno()
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
        private bool edit = false;
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
                string skla_uk = "Select * from skladiste";
                DataTable DTpopisskl = classSQL.select(skla_uk, "skladiste").Tables[0];
                for (int i = 0; i < DTpopisskl.Rows.Count; i++)
                {
                    string skladiste = DTpopisskl.Rows[i]["id_skladiste"].ToString();
                    SQL.ClassSkladiste.provjeri_roba_prodaja(txtSifra_robe.Text, skladiste);
                }

                string sql = "SELECT * FROM roba_prodaja " +
                    " WHERE sifra='" + txtSifra_robe.Text + "' AND id_skladiste = '" + cbSkladiste.SelectedValue + "'";

                DTRoba = classSQL.select(sql, "roba_prodaja").Tables[0];

                string sql1 = "SELECT * FROM roba " +
                    " WHERE sifra='" + txtSifra_robe.Text + "'";

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
                string sql_skla = "Select skladiste from skladiste WHERE id_skladiste = '" + DTRoba.Rows[0]["id_skladiste"].ToString() + "'";
                DataTable DTskla = classSQL.select(sql_skla, "skladiste").Tables[0];

                string skladiste_ime = DTskla.Rows[0]["skladiste"].ToString();

                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;
                dgw.Select();
                dgw.CurrentCell = dgw.Rows[br].Cells[4];

                double vpc = Convert.ToDouble(DTRoba.Rows[0]["vpc"].ToString());
                double pdv = Convert.ToDouble(DTRoba.Rows[0]["porez"].ToString());

                dgw.Rows[br].Cells[0].Value = dgw.RowCount;
                dgw.Rows[br].Cells["sifra"].Value = DTRoba1.Rows[0]["sifra"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = DTRoba1.Rows[0]["naziv"].ToString();
                dgw.Rows[br].Cells["stara_cijena"].Value = (vpc * pdv / 100) + vpc;
                dgw.Rows[br].Cells["postotak"].Value = "0";
                dgw.Rows[br].Cells["nova_cijena"].Value = (vpc * pdv / 100) + vpc;
                dgw.Rows[br].Cells["pdv"].Value = pdv;
                dgw.Rows[br].Cells["kolicina"].Value = 0;
                dgw.Rows[br].Cells["skladiste"].Value = skladiste_ime;
                dgw.Rows[br].Cells["iznos"].Value = (vpc * pdv / 100) + vpc;

                dgw.BeginEdit(true);
                PaintRows(dgw);
            }
            catch
            {
            }
        }

        private void SetCB()
        {
            DTIzradio = classSQL.select("SELECT ime+' '+prezime as Ime, id_zaposlenik  FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0];
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
            else if (dgw.CurrentCell.ColumnIndex == 6)
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
                //txtBrojInventure.Enabled = false;
                nmGodinaInventure.Enabled = false;
            }
            else if (x == false)
            {
                //txtBrojInventure.Enabled = true;
                nmGodinaInventure.Enabled = true;
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            txtBrojInventure.Text = brojPromjene();
            EnableDisable(false);
            DelField();
            edit = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private string brojPromjene()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj) FROM promjena_cijene_komadno", "promjena_cijene_komadno").Tables[0];
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
            edit = true;
            Robno.frmSvePromjeneCijenaKomadno prom = new Robno.frmSvePromjeneCijenaKomadno();
            prom.MainForm = this;
            broj_pocetno_edit = null;
            prom.ShowDialog();

            if (broj_pocetno_edit != null)
            {
                FillPocetnoStanje(broj_pocetno_edit);
            }
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            txtBrojInventure.Text = brojPromjene();
            EnableDisable(true);
            edit = false;
            ControlDisableEnable(0, 1, 1, 0, 1);
        }

        private void btnOpenRoba_Click(object sender, EventArgs e)
        {
        }

        private void txtBrojInventure_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT broj FROM promjena_cijene_komadno WHERE  broj='" + txtBrojInventure.Text + "'", "pocetno_stanje").Tables[0];
                DelField();
                if (DT.Rows.Count == 0)
                {
                    if (brojPromjene() == txtBrojInventure.Text.Trim())
                    {
                        DelField();
                        edit = false;
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
                    edit = true;
                }
                ControlDisableEnable(0, 1, 1, 0, 1);
            }
        }

        private void FillPocetnoStanje(string broj)
        {
            DataTable DTheader = classSQL.select("SELECT * FROM promjena_cijene_komadno WHERE broj='" + broj + "'", "promjena_cijene_komadno").Tables[0];

            if (DTheader.Rows.Count > 0)
            {
                cbSkladiste.SelectedValue = DTheader.Rows[0]["id_skladiste"].ToString();
                rtbNapomena.Text = DTheader.Rows[0]["napomena"].ToString();
                dtpDatum.Value = Convert.ToDateTime(DTheader.Rows[0]["date"].ToString());
                txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + DTheader.Rows[0]["id_izradio"].ToString() + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
            }

            string sql = "SELECT promjena_cijene_komadno_stavke.sifra," +
                " promjena_cijene_komadno_stavke.id_stavka," +
                " promjena_cijene_komadno_stavke.stara_cijena," +
                " promjena_cijene_komadno_stavke.pdv," +
                " promjena_cijene_komadno_stavke.nova_cijena," +
                " promjena_cijene_komadno_stavke.postotak," +
                " promjena_cijene_komadno_stavke.kolicina," +
                " promjena_cijene_komadno_stavke.kolicina_ostatak," +
                " promjena_cijene_komadno_stavke.skladiste," +
                " roba.naziv," +
                " roba.jm " +
                " FROM promjena_cijene_komadno_stavke" +
                " LEFT JOIN roba ON roba.sifra=promjena_cijene_komadno_stavke.sifra WHERE promjena_cijene_komadno_stavke.broj='" + broj + "'" +
                "";
            DataTable DT = classSQL.select(sql, "promjena_cijene_komadno_stavke").Tables[0];

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
                dgw.Rows[br].Cells["kolicina"].Value = DT.Rows[i]["kolicina"].ToString();
                dgw.Rows[br].Cells["skladiste"].Value = DT.Rows[i]["skladiste"].ToString();
                dgw.Rows[br].Cells["iznos"].Value = Convert.ToDouble(DT.Rows[i]["nova_cijena"].ToString()) - Convert.ToDouble(DT.Rows[i]["stara_cijena"].ToString());
            }
            edit = true;
            PaintRows(dgw);
        }

        private void Spremi()
        {
            DataTable DTboll = classSQL.select("SELECT broj FROM promjena_cijene_komadno WHERE broj='" + txtBrojInventure.Text + "'", "promjena_cijene_komadno").Tables[0];

            if (DTboll.Rows.Count == 0)
            {
                txtBrojInventure.Text = brojPromjene();
                string s = "INSERT INTO promjena_cijene_komadno (broj,id_skladiste,date,id_izradio,napomena) VALUES " +
                    "(" +
                    "'" + txtBrojInventure.Text + "'," +
                    "'" + cbSkladiste.SelectedValue + "'," +
                    "'" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "'" + Properties.Settings.Default.id_zaposlenik + "'," +
                    "'" + rtbNapomena.Text + "'" +
                    ")";
                classSQL.insert(s);

                classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES " +
                    "('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Novi zapisnik o promjeni cijene br." + txtBrojInventure.Text + "')");
            }
            else
            {
                string s = "UPDATE promjena_cijene_komadno SET broj='" + txtBrojInventure.Text + "'" +
                ",id_skladiste='" + cbSkladiste.SelectedValue + "'" +
                ",date='" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                ",napomena='" + rtbNapomena.Text + "'  WHERE broj='" + txtBrojInventure.Text + "'";
                classSQL.update(s);

                classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES " +
                    "('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Ažuriranje zapisnika o promjeni cijene br." + txtBrojInventure.Text + "')");
            }

            for (int i = 0; i < dgw.RowCount; i++)
            {
                double pdv = double.Parse(dgw.Rows[i].Cells["pdv"].FormattedValue.ToString());
                double stara = double.Parse(dgw.Rows[i].Cells["stara_cijena"].FormattedValue.ToString());
                double nova = double.Parse(dgw.Rows[i].Cells["nova_cijena"].FormattedValue.ToString());

                double kol = double.Parse(dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString());
                double kol_prodano = double.Parse(dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString());
                string skladiste = dgw.Rows[i].Cells["skladiste"].FormattedValue.ToString();

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
                string sql_skla = "Select id_skladiste from skladiste where skladiste = '" + dgw.Rows[i].Cells["skladiste"].FormattedValue.ToString() + "'";
                DataTable DTskla_ = classSQL.select(sql_skla, "provjera skladista").Tables[0];

                if (dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString() == "")
                {
                    string s1 = "INSERT INTO promjena_cijene_komadno_stavke (stara_cijena,nova_cijena,pdv,sifra,postotak,kolicina,kolicina_ostatak,skladiste,broj,id_skladiste,datum) VALUES " +
                        "(" +
                        "'" + StrStara + "'," +
                        "'" + StrNova + "'," +
                        "'" + pdv.ToString() + "'," +
                        "'" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'," +
                        "'" + dgw.Rows[i].Cells["postotak"].FormattedValue.ToString() + "'," +
                        "'" + kol + "'," +
                        "'" + kol_prodano + "'," +
                        "'" + skladiste + "'," +
                        "'" + txtBrojInventure.Text + "'," +
                        "'" + DTskla_.Rows[0]["id_skladiste"].ToString() + "'," +
                        "'" + DateTime.Now + "'" +
                        ")";

                    classSQL.insert(s1);
                }
                else
                {
                    string s2 = "UPDATE promjena_cijene_komadno_stavke SET stara_cijena=" +
                        "'" + StrStara + "'," +
                        " nova_cijena='" + StrNova + "'," +
                        " pdv='" + pdv.ToString() + "'," +
                        " sifra='" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'," +
                        " postotak='" + dgw.Rows[i].Cells["postotak"].FormattedValue.ToString() + "' WHERE id_stavka = '" + dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString() + "'";

                    classSQL.update(s2);
                }
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            Spremi();

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

                //string sql = "UPDATE roba SET vpc='" + StrVpc.Replace(",",".") + "',mpc='" + StrMpc + "' WHERE sifra='" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'";
                //classSQL.update(sql);

                DataTable DT = classSQL.select("SELECT sifra FROM roba_prodaja WHERE sifra='" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + cbSkladiste.SelectedValue.ToString() + "'", "roba_prodaja").Tables[0];

                //if(DT.Rows.Count!=0)
                //{
                //    string sql1 = "UPDATE roba_prodaja SET vpc='" + StrVpc.Replace(",", ".") + "' WHERE sifra='" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + cbSkladiste.SelectedValue.ToString() + "'";
                //    classSQL.update(sql1);
                //}
            }

            EnableDisable(false);
            edit = false;

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
            pr.dokument = "promjena_cijene_komadno";
            pr.broj_dokumenta = broj;
            pr.ImeForme = "Zapisnik o promjeni cijene";
            pr.ShowDialog();
        }

        private void btnOpenRoba_Click_1(object sender, EventArgs e)
        {
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            roba_trazi.ShowDialog();
            string propertis_sifra = Properties.Settings.Default.id_roba.Trim();
            string id_skladiste = Properties.Settings.Default.idSkladiste;
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

                SQL.ClassSkladiste.provjeri_roba_prodaja(propertis_sifra, id_skladiste);
                string sql = "SELECT * FROM roba WHERE sifra='" + propertis_sifra + "'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];

                string sql1 = "SELECT * FROM roba_prodaja " +
                " WHERE sifra='" + propertis_sifra + "' AND id_skladiste = '" + id_skladiste + "'";

                DTRoba1 = classSQL.select(sql1, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRobaTrazi();
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SetRobaTrazi()
        {
            try
            {
                string sql_skla = "Select skladiste from skladiste WHERE id_skladiste = '" + DTRoba1.Rows[0]["id_skladiste"].ToString() + "'";
                DataTable DTskla = classSQL.select(sql_skla, "skladiste").Tables[0];

                string skladiste_ime = DTskla.Rows[0]["skladiste"].ToString();

                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;
                dgw.Select();
                dgw.CurrentCell = dgw.Rows[br].Cells[4];

                double vpc = Convert.ToDouble(DTRoba.Rows[0]["vpc"].ToString());
                double pdv = Convert.ToDouble(DTRoba.Rows[0]["porez"].ToString());

                dgw.Rows[br].Cells[0].Value = dgw.RowCount;
                dgw.Rows[br].Cells["sifra"].Value = DTRoba1.Rows[0]["sifra"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = DTRoba.Rows[0]["naziv"].ToString();
                dgw.Rows[br].Cells["stara_cijena"].Value = (vpc * pdv / 100) + vpc;
                dgw.Rows[br].Cells["postotak"].Value = "0";
                dgw.Rows[br].Cells["nova_cijena"].Value = (vpc * pdv / 100) + vpc;
                dgw.Rows[br].Cells["pdv"].Value = pdv;
                dgw.Rows[br].Cells["kolicina"].Value = 0;
                dgw.Rows[br].Cells["skladiste"].Value = skladiste_ime;
                dgw.Rows[br].Cells["iznos"].Value = (vpc * pdv / 100) + vpc;

                dgw.BeginEdit(true);
                PaintRows(dgw);
            }
            catch
            {
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
                //if (MessageBox.Show("Brisanjem ove međuskladišnice brišete i količinu robe sa skladišta. Dali ste sigurni da želite obrisati ovaj dokument?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                //{
                //    int i = dgw.CurrentRow.Index;
                //    //iz skladišta
                //    DataRow[] dataROW = DTfill.Select("id_stavka = " + dgw.CurrentRow.Cells["id_stavka"].Value.ToString());
                //    kol = Convert.ToDouble(classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + DTHeaderfill.Rows[0]["id_skladiste_od"].ToString() + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString());
                //    kolicina = Convert.ToDouble(dataROW[0]["kolicina"].ToString());
                //    kolicina = kolicina + kol;
                //    classSQL.update("UPDATE roba_prodaja SET kolicina='" + kolicina + "' WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + DTHeaderfill.Rows[0]["id_skladiste_od"].ToString() + "'");

                //    //u skladište
                //    kol = Convert.ToDouble(classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + DTHeaderfill.Rows[0]["id_skladiste_do"].ToString() + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString());
                //    kolicina = Convert.ToDouble(dataROW[0]["kolicina"].ToString());
                //    kolicina = kolicina - kol;
                //    classSQL.update("UPDATE roba_prodaja SET kolicina='" + kolicina + "' WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + DTHeaderfill.Rows[0]["id_skladiste_do"].ToString() + "'");

                //    classSQL.delete("DELETE FROM meduskladisnica_stavke WHERE id_stavka='" + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() + "'");
                //    dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                //    classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje stavke sa međuskladišnice br." + txtBroj.Text + "')");
                //    //MessageBox.Show("Obrisano.");
                //}

                classSQL.delete("DELETE FROM promjena_cijene_komadno_stavke WHERE id_stavka='" + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() + "'");
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje stavke sa zapisnika o promjeni cijene br." + txtBrojInventure.Text + "')");
            }
            else
            {
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                //MessageBox.Show("Obrisano.");
            }
        }
    }
}