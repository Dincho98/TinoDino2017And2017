using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmOtpisRobe : Form
    {
        public frmOtpisRobe()
        {
            InitializeComponent();
        }

        private string skladiste_pocetno = "";
        private bool edit = false;
        private DataTable DT_Skladiste;
        private DataTable DT_stavke;
        public string broj_povrata_edit { get; set; }
        public frmMenu MainForm { get; set; }

        private int test = 0;
        private int test1 = 1;

        private class MyDataGrid : System.Windows.Forms.DataGridView
        {
            public static frmOtpisRobe MainForm { get; set; }

            protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
            {
                if (keyData == Keys.Enter)
                {
                    MainForm.EnterDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Right)
                {
                    MainForm.RightDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Left)
                {
                    MainForm.LeftDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Up)
                {
                    MainForm.UpDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Down)
                {
                    MainForm.DownDGW(MainForm.dgw);
                    return true;
                }
                //else if (keyData == Keys.Insert)
                //{
                //    MainForm.dgw.Rows.Add("", "", "", "1", "0", "0", "0", "0", "0", "0", "0");
                //    MainForm.RedniBroj();

                //    return true;
                //}
                else if (keyData == Keys.Delete)
                {
                    MainForm.dgw.Rows.RemoveAt(MainForm.dgw.CurrentRow.Index);
                    MainForm.RedniBroj();
                    return true;
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void RedniBroj()
        {
            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                dgw.Rows[i].Cells["br"].Value = i + 1;
            }
        }

        private void frmOtpisRobe_Load(object sender, EventArgs e)
        {
            MyDataGrid.MainForm = this;
            SetSkladiste();
            numeric();
            txtBroj.Text = brojPovrata();
            ControlDisableEnable(1, 0, 0, 1, 0);
            this.Paint += new PaintEventHandler(Form1_Paint);
            if (broj_povrata_edit != null) { FillPovrat(); }
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
        }

        private void SetSkladiste()
        {
            DT_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste").Tables[0];
            cbSkladiste.DataSource = DT_Skladiste;
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private DataTable DTheader;

        private void FillPovrat()
        {
            EnableDisable(true);
            DeleteFields();
            ControlDisableEnable(0, 1, 0, 0, 1);

            string sssql = "SELECT * FROM otpis_robe WHERE broj='" + broj_povrata_edit + "'";
            DTheader = classSQL.select(sssql, "otpis_robe").Tables[0];

            nmGodina.Value = Convert.ToInt16(DTheader.Rows[0]["godina"].ToString());
            txtBroj.Text = DTheader.Rows[0]["broj"].ToString();
            //cbSkladiste.SelectedValue = Convert.ToInt16(DTheader.Rows[0]["id_skladiste"].ToString());
            dtpDatum.Value = Convert.ToDateTime(DTheader.Rows[0]["datum"].ToString());
            rtbNapomena.Text = DTheader.Rows[0]["napomena"].ToString();
            txtMjestoTroska.Text = DTheader.Rows[0]["mjesto_troska"].ToString();
            skladiste_pocetno = DTheader.Rows[0]["id_skladiste"].ToString();
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + DTheader.Rows[0]["id_izradio"].ToString() + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();

            string sql = "SELECT " +
                " otpis_robe_stavke.id_stavka," +
                " otpis_robe_stavke.sifra," +
                " otpis_robe_stavke.kolicina," +
                " otpis_robe_stavke.vpc," +
                " otpis_robe_stavke.nbc," +
                " otpis_robe_stavke.pdv," +
                " otpis_robe_stavke.rabat," +
                " roba.naziv" +
                " FROM otpis_robe_stavke " +
                " LEFT JOIN roba ON roba.sifra=otpis_robe_stavke.sifra" +
                " WHERE otpis_robe_stavke.broj='" + broj_povrata_edit + "'";

            DT_stavke = classSQL.select(sql, "promjena_cijene_stavke").Tables[0];

            for (int i = 0; i < DT_stavke.Rows.Count; i++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;
                dgw.Select();
                dgw.CurrentCell = dgw.Rows[br].Cells[3];

                double kolicina = Convert.ToDouble(DT_stavke.Rows[i]["kolicina"].ToString());
                double vpc = Convert.ToDouble(DT_stavke.Rows[i]["vpc"].ToString());
                double pdv = Convert.ToDouble(DT_stavke.Rows[i]["pdv"].ToString());

                dgw.Rows[br].Cells[0].Value = dgw.RowCount;
                dgw.Rows[br].Cells["sifra"].Value = DT_stavke.Rows[i]["sifra"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = DT_stavke.Rows[i]["naziv"].ToString();
                dgw.Rows[br].Cells["kolicina"].Value = DT_stavke.Rows[i]["kolicina"].ToString();
                dgw.Rows[br].Cells["vpc"].Value = vpc.ToString("#0.000");
                dgw.Rows[br].Cells["nbc"].Value = Convert.ToDouble(DT_stavke.Rows[i]["nbc"].ToString()).ToString("#0.00");
                dgw.Rows[br].Cells["pdv"].Value = DT_stavke.Rows[i]["pdv"].ToString();
                dgw.Rows[br].Cells["id_stavka"].Value = DT_stavke.Rows[i]["id_stavka"].ToString();
                dgw.Rows[br].Cells["rabat"].Value = DT_stavke.Rows[i]["rabat"].ToString();
                dgw.Rows[br].Cells["mpc"].Value = Math.Round(vpc + (vpc * pdv / 100), 2).ToString("#0.00");
                dgw.Rows[br].Cells["ukupno"].Value = Convert.ToDouble((vpc * kolicina) + ((vpc * kolicina) * pdv / 100)).ToString("#0.00");
            }

            ControlDisableEnable(0, 1, 1, 0, 1);
            edit = true;

            dgw.Columns["ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private string brojPovrata()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj) FROM otpis_robe", "otpis_robe").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
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

            if (delAll == 0)
            {
                btnDeleteAllFaktura.Enabled = false;
            }
            else if (delAll == 1)
            {
                btnDeleteAllFaktura.Enabled = true;
            }
        }

        private void numeric()
        {
            nmGodina.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nmGodina.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nmGodina.Value = Convert.ToInt16(DateTime.Now.Year.ToString()); ;
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            txtBroj.Text = brojPovrata();
            EnableDisable(true);
            edit = false;
            ControlDisableEnable(0, 1, 1, 0, 1);
        }

        private void EnableDisable(bool x)
        {
            dtpDatum.Enabled = x;
            cbSkladiste.Enabled = x;
            dtpDatum.Enabled = x;
            rtbNapomena.Enabled = x;
            txtMjestoTroska.Enabled = x;
            txtSifra_robe.Enabled = x;

            txtBroj.Enabled = !x;
            nmGodina.Enabled = !x;
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            EnableDisable(false);
            DeleteFields();
            txtBroj.Text = brojPovrata();
            edit = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private void DeleteFields()
        {
            dtpDatum.Text = "";
            rtbNapomena.Text = "";
            dgw.Rows.Clear();
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            DataTable DTbool = classSQL.select("SELECT broj FROM otpis_robe WHERE broj = '" + txtBroj.Text + "'", "otpis_robe").Tables[0];

            //decimal dec_parse;
            //if (!Decimal.TryParse(txtSifraPartnera.Text, out dec_parse) || txtSifraPartnera.Text == "")
            //{
            //    MessageBox.Show("Greška kod upisa šifre partnera.", "Greška"); return;
            //}

            //if (!Decimal.TryParse(txtSifraOdredista.Text, out dec_parse) || txtSifraOdredista.Text == "")
            //{
            //    MessageBox.Show("Greška kod upisa šifre odredišta.", "Greška"); return;
            //}

            string sql = "";
            if (DTbool.Rows.Count == 0)
            {
                sql = "INSERT INTO otpis_robe (broj,godina,datum,mjesto_troska,id_izradio,napomena,id_skladiste) VALUES (" +
                     " '" + txtBroj.Text + "'," +
                     " '" + nmGodina.Value.ToString() + "'," +
                      " '" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                         " '" + txtMjestoTroska.Text + "'," +
                          " '" + Properties.Settings.Default.id_zaposlenik + "'," +
                           " '" + rtbNapomena.Text + "'," +
                           " '" + cbSkladiste.SelectedValue + "'" +
                                ")";
                provjera_sql(classSQL.insert(sql));
                provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Otpis robe br." + txtBroj.Text + "')"));
            }
            else
            {
                sql = "UPDATE otpis_robe SET " +
                     " godina='" + nmGodina.Value.ToString() + "'," +
                     " datum='" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                     " id_skladiste='" + cbSkladiste.SelectedValue + "'," +
                     " mjesto_troska='" + txtMjestoTroska.Text + "'," +
                     " id_izradio='" + Properties.Settings.Default.id_zaposlenik + "'," +
                     " napomena='" + rtbNapomena.Text + "" +
                     "' WHERE broj='" + txtBroj.Text + "'" +
                                "";
                provjera_sql(classSQL.update(sql));
                provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Uređivanje otpisa robe br." + txtBroj.Text + "')"));
            }

            string ssql = "";
            string kol = "";

            for (int i = 0; i < dgw.RowCount; i++)
            {
                if (dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString() == "")
                {
                    ssql = "INSERT INTO otpis_robe_stavke (sifra,vpc,nbc,mpc,pdv,kolicina,rabat,broj) VALUES (" +
                        "'" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'," +
                        "'" + dgw.Rows[i].Cells["vpc"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "'" + dgw.Rows[i].Cells["nbc"].FormattedValue.ToString() + "'," +
                        "'" + dgw.Rows[i].Cells["mpc"].FormattedValue.ToString() + "'," +
                        "'" + dgw.Rows[i].Cells["pdv"].FormattedValue.ToString() + "'," +
                        "'" + dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString() + "'," +
                        "'" + dgw.Rows[i].Cells["rabat"].FormattedValue.ToString() + "'," +
                        "'" + txtBroj.Text + "'" +
                        ")";

                    provjera_sql(classSQL.insert(ssql));

                    kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), cbSkladiste.SelectedValue.ToString(), dg(i, "kolicina"), "1", "-");
                    SQL.SQLroba_prodaja.UpdateRows(cbSkladiste.SelectedValue.ToString(), kol, dg(i, "sifra"));
                }
                else
                {
                    ssql = "UPDATE otpis_robe_stavke SET " +
                        "sifra='" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'," +
                        "vpc='" + dgw.Rows[i].Cells["vpc"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "nbc='" + dgw.Rows[i].Cells["nbc"].FormattedValue.ToString() + "'," +
                        "mpc='" + dgw.Rows[i].Cells["mpc"].FormattedValue.ToString() + "'," +
                        "pdv='" + dgw.Rows[i].Cells["pdv"].FormattedValue.ToString() + "'," +
                        "kolicina='" + dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString() + "'," +
                        "rabat='" + dgw.Rows[i].Cells["rabat"].FormattedValue.ToString() + "'" +
                        " WHERE id_stavka='" + dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString() + "'";

                    provjera_sql(classSQL.update(ssql));

                    DataRow[] dataROW = DT_stavke.Select("id_stavka = " + dgw.Rows[i].Cells["id_stavka"].Value.ToString());

                    if (cbSkladiste.SelectedValue.ToString() == skladiste_pocetno)
                    {
                        kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), cbSkladiste.SelectedValue.ToString(), (Convert.ToDouble(dataROW[0]["kolicina"].ToString()) - Convert.ToDouble(dg(i, "kolicina"))).ToString(), "1", "+");
                        SQL.SQLroba_prodaja.UpdateRows(cbSkladiste.SelectedValue.ToString(), kol, dg(i, "sifra"));
                    }
                    else
                    {
                        //vraća na staro skladiste
                        kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), DTheader.Rows[0]["id_skladiste"].ToString(), dataROW[0]["kolicina"].ToString(), "1", "+");
                        SQL.SQLroba_prodaja.UpdateRows(DTheader.Rows[0]["id_skladiste"].ToString(), kol, dg(i, "sifra"));

                        //oduzima sa novog skladiste
                        kol = SQL.ClassSkladiste.GetAmount(dg(i, "sifra"), cbSkladiste.SelectedValue.ToString(), dg(i, "kolicina"), "1", "-");
                        SQL.SQLroba_prodaja.UpdateRows(cbSkladiste.SelectedValue.ToString(), kol, dg(i, "sifra"));
                    }
                }
            }

            EnableDisable(false);
            DeleteFields();
            txtBroj.Text = brojPovrata();
            edit = false;
            ControlDisableEnable(1, 0, 0, 1, 0);
            MessageBox.Show("Spremljeno.");
        }

        private string dg(int row, string cell)
        {
            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        private void txtBroj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                DataTable DT = classSQL.select("SELECT broj FROM otpis_robe WHERE  broj='" + txtBroj.Text + "'", "otpis_robe").Tables[0];
                DeleteFields();
                if (DT.Rows.Count == 0)
                {
                    if (brojPovrata() == txtBroj.Text.Trim())
                    {
                        edit = false;
                        EnableDisable(true);
                        btnSveFakture.Enabled = false;
                        cbSkladiste.Select();
                        ControlDisableEnable(0, 1, 1, 0, 1);
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    broj_povrata_edit = txtBroj.Text;
                    FillPovrat();
                    EnableDisable(true);
                    edit = true;
                    cbSkladiste.Select();
                    ControlDisableEnable(0, 1, 1, 0, 1);
                }
            }
        }

        private void cbSkladiste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                dtpDatum.Select();
            }
            else if (e.KeyCode == Keys.Insert)
            {
                txtSifra_robe.Select();
            }
        }

        private void dtpDatum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Insert)
            {
                txtSifra_robe.Select();
            }
        }

        private void txtIzradio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtMjestoTroska.Select();
            }
            else if (e.KeyCode == Keys.Insert)
            {
                txtSifra_robe.Select();
            }
        }

        private void txtMjestoTroska_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Insert)
            {
                txtSifra_robe.Select();
            }
        }

        private void rtbNapomena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifra_robe.Select();
            }
            else if (e.KeyCode == Keys.Insert)
            {
                txtSifra_robe.Select();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private DataTable DTRoba;

        private void txtSifra_robe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                for (int y = 0; y < dgw.Rows.Count; y++)
                {
                    if (txtSifra_robe.Text == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString().Trim())
                    {
                        MessageBox.Show("Artikl ili usluga već postoje u ovoj inventuri.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string sql = "SELECT * FROM roba" +
                    " WHERE sifra='" + txtSifra_robe.Text + "' AND oduzmi='DA'";

                DTRoba = classSQL.select(sql, "roba_prodaja").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();
                    txtSifra_robe.Text = "";
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoji artikl.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SetRoba()
        {
            dgw.Rows.Add();
            int br = dgw.Rows.Count - 1;
            dgw.Select();
            dgw.CurrentCell = dgw.Rows[br].Cells[3];

            decimal _NBC = Util.Korisno.VratiNabavnuCijenu(DTRoba.Rows[0]["sifra"].ToString(), cbSkladiste.SelectedValue.ToString());

            double vpc = Convert.ToDouble(DTRoba.Rows[0]["vpc"].ToString());
            double mpc = Convert.ToDouble(DTRoba.Rows[0]["mpc"].ToString());
            decimal nbc = _NBC;
            double pdv = Convert.ToDouble(DTRoba.Rows[0]["porez"].ToString());
            double kol = 0;

            dgw.Rows[br].Cells[0].Value = dgw.RowCount;
            dgw.Rows[br].Cells["sifra"].Value = DTRoba.Rows[0]["sifra"].ToString();
            dgw.Rows[br].Cells["naziv"].Value = DTRoba.Rows[0]["naziv"].ToString();
            dgw.Rows[br].Cells["vpc"].Value = vpc.ToString("#0.000");
            dgw.Rows[br].Cells["nbc"].Value = nbc.ToString("#0.00");
            dgw.Rows[br].Cells["rabat"].Value = "0";
            dgw.Rows[br].Cells["mpc"].Value = mpc.ToString("#0.00");
            dgw.Rows[br].Cells["pdv"].Value = pdv;
            dgw.Rows[br].Cells["kolicina"].Value = "1";
            dgw.Rows[br].Cells["rabat"].Value = "0";
            dgw.Rows[br].Cells["ukupno"].Value = mpc.ToString("#0.00");

            DataTable DTprodaja = classSQL.select("SELECT nc,vpc,porez,kolicina FROM roba_prodaja WHERE id_skladiste='" + cbSkladiste.SelectedValue + "' AND sifra='" + DTRoba.Rows[0]["sifra"].ToString() + "'", "roba_prodaja").Tables[0];
            if (DTprodaja.Rows.Count > 0)
            {
                vpc = Convert.ToDouble(DTprodaja.Rows[0]["vpc"].ToString());
                pdv = Convert.ToDouble(DTprodaja.Rows[0]["porez"].ToString());
                kol = Convert.ToDouble(dgw.Rows[br].Cells["kolicina"].FormattedValue.ToString());
                dgw.Rows[br].Cells["vpc"].Value = Convert.ToDouble(DTprodaja.Rows[0]["vpc"].ToString()).ToString("#0.000");
                //dgw.Rows[br].Cells["nbc"].Value = Convert.ToDouble(DTprodaja.Rows[0]["nc"].ToString()).ToString("#0.00");
                dgw.Rows[br].Cells["mpc"].Value = (vpc + (vpc * pdv / 100)).ToString("#0.00");
                dgw.Rows[br].Cells["ukupno"].Value = ((vpc + (vpc * pdv / 100)) * kol).ToString("#0.00");
                dgw.Rows[br].Cells["pdv"].Value = pdv;

                if (Convert.ToDouble(DTprodaja.Rows[0]["kolicina"].ToString()) > 0)
                {
                    lblNaDan.ForeColor = Color.Green;
                    lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima " + DTprodaja.Rows[0]["kolicina"].ToString() + " " + DTRoba.Rows[0]["jm"].ToString();
                }
                else
                {
                    lblNaDan.ForeColor = Color.Red;
                    lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima " + DTprodaja.Rows[0]["kolicina"].ToString() + " " + DTRoba.Rows[0]["jm"].ToString();
                    MessageBox.Show("Na odabranom skladištu nemate unešeni artikl.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                lblNaDan.ForeColor = Color.Red;
                lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima 0 " + DTRoba.Rows[0]["jm"].ToString();
            }

            dgw.BeginEdit(true);
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
                    if (propertis_sifra == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString())
                    {
                        MessageBox.Show("Artikl ili usluga već postoje!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string sql = "SELECT * FROM roba WHERE sifra='" + propertis_sifra + "'  AND oduzmi='DA'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
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

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.Rows.Count < 1)
            {
                return;
            }
            dgw.BeginEdit(true);
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            Robno.frmSviOtpisi sp = new Robno.frmSviOtpisi();
            sp.sifra = "";
            sp.MainForm = this;
            sp.ShowDialog();
            if (broj_povrata_edit != null)
            {
                DeleteFields();
                FillPovrat();
            }
        }

        private void btnDeleteAllFaktura_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Brisanjem ove povratnice vraćate i količinu robe na skladišta. Da li ste sigurni da želite obrisati ovu povratnicu?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                double skl = 0;
                double fa_kolicina = 0;
                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    if (dgw.Rows[i].Cells["id_stavka"].Value != null)
                    {
                        DataRow[] dataROW = DT_stavke.Select("id_stavka = " + dgw.Rows[i].Cells["id_stavka"].Value.ToString());
                        skl = Convert.ToDouble(classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + skladiste_pocetno + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString());
                        fa_kolicina = Convert.ToDouble(dataROW[0]["kolicina"].ToString());

                        skl = skl + fa_kolicina;
                        classSQL.update("UPDATE roba_prodaja SET kolicina='" + skl + "' WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + skladiste_pocetno + "'");
                    }
                }

                classSQL.delete("DELETE FROM otpis_robe_stavke WHERE broj='" + txtBroj.Text + "'");
                classSQL.delete("DELETE FROM otpis_robe WHERE broj='" + txtBroj.Text + "'");
                classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") + "','Brisanje cijelog otpisa robe br." + txtBroj.Text + "')");
                MessageBox.Show("Obrisano.");

                edit = false;
                EnableDisable(false);
                DeleteFields();
                ControlDisableEnable(1, 0, 0, 1, 0);
            }
            this.Refresh();
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dgw.RowCount == 0)
            {
                return;
            }
            if (dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].Value != null)
            {
                DataRow[] dataROW = DT_stavke.Select("id_stavka = " + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].Value.ToString());

                if (MessageBox.Show("Brisanjem ove stavke vraćate i količinu robe na skladišta. Da li ste sigurni da želite obrisati ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    string kol = classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dgw.Rows[dgw.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + skladiste_pocetno + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString();
                    kol = (Convert.ToDouble(kol) + Convert.ToDouble(dataROW[0]["kolicina"].ToString())).ToString();
                    classSQL.update("UPDATE roba_prodaja SET kolicina='" + kol + "' WHERE sifra='" + dgw.Rows[dgw.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + skladiste_pocetno + "'");

                    classSQL.delete("DELETE FROM otpis_robe_stavke WHERE id_stavka='" + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() + "'");
                    dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                    classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") + "','Brisanje stavke sa povratnice robe br." + txtBroj.Text + "')");
                    MessageBox.Show("Obrisano.");
                }
            }
            else
            {
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                MessageBox.Show("Obrisano.");
            }
        }

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5)
            {
                double PDV, vpc, kol, mpc;
                try
                {
                    PDV = Convert.ToDouble(dgw.CurrentRow.Cells["pdv"].FormattedValue.ToString());
                }
                catch
                {
                    MessageBox.Show("Unesite numeričku vrijednost za pdv", "Greška!");
                    dgw.CurrentRow.Cells["pdv"].Value = "0,00";
                    PDV = 0;
                    dgw.CurrentCell = dgw.Rows[dgw.CurrentRow.Index].Cells[5];
                    dgw.BeginEdit(true);
                }
                try
                {
                    vpc = Convert.ToDouble(dgw.CurrentRow.Cells["vpc"].FormattedValue.ToString());
                }
                catch
                {
                    MessageBox.Show("Unesite numeričku vrijednost za vpc", "Greška!");
                    dgw.CurrentRow.Cells["vpc"].Value = "0,000";
                    vpc = 0;
                    dgw.CurrentCell = dgw.Rows[dgw.CurrentRow.Index].Cells[4];
                    dgw.BeginEdit(true);
                }
                try
                {
                    kol = Convert.ToDouble(dgw.CurrentRow.Cells["kolicina"].FormattedValue.ToString());
                }
                catch
                {
                    MessageBox.Show("Unesite numeričku vrijednost za količinu", "Greška!");
                    dgw.CurrentRow.Cells["kolicina"].Value = "1,000";
                    kol = 0;
                    dgw.CurrentCell = dgw.CurrentRow.Cells[3];
                    dgw.BeginEdit(true);
                }

                mpc = Math.Round(vpc * PDV / 100 + vpc, 2);

                dgw.CurrentRow.Cells["mpc"].Value = mpc.ToString("#0.00");
                dgw.CurrentRow.Cells["ukupno"].Value = Math.Round(mpc * kol, 2).ToString("#0.00");
            }

            //if (dgw.CurrentCell.ColumnIndex == 3)
            //{
            //    try
            //    {
            //        dgw.CurrentRow.Cells[4].Selected = true;
            //        //dgw.CurrentCell = dgw.CurrentRow.Cells[5];
            //        dgw.BeginEdit(true);
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("Koristite enter za sljedeću kolonu.");
            //    }
            //}
            //else if (dgw.CurrentCell.ColumnIndex == 4)
            //{
            //    try
            //    {
            //        dgw.CurrentCell = dgw.CurrentRow.Cells[5];
            //        dgw.BeginEdit(true);
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("Koristite enter za sljedeću kolonu.");
            //    }
            //}
            //else if (dgw.CurrentCell.ColumnIndex == 5)
            //{
            //    try
            //    {
            //        dgw.CurrentCell = dgw.CurrentRow.Cells[7];
            //        dgw.BeginEdit(true);
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("Koristite enter za sljedeću kolonu.");
            //    }
            //}
            //else if (dgw.CurrentCell.ColumnIndex == 7)
            //{
            //    try
            //    {
            //        txtSifra_robe.Text = "";
            //        txtSifra_robe.Select();
            //        dgw.ClearSelection();

            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("Koristite enter za sljedeću kolonu.");
            //    }
            //}
        }

        private void EnterDGW(DataGridView d)
        {
            if (dgw.Rows.Count < 1)
            {
                return;
            }
            if (d.CurrentCell.ColumnIndex == 1)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[2];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 2)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 3)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 4)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[6];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 6)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 7)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[8];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 8)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[1];
                d.BeginEdit(true);
            }
        }

        private void LeftDGW(DataGridView d)
        {
            if (dgw.Rows.Count < 1)
            {
                return;
            }
            if (d.CurrentCell.ColumnIndex == 1)
            {
            }
            else if (d.CurrentCell.ColumnIndex == 2)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[1];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 3)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[2];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 4)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 6)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 7)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[6];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 8)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 9)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[8];
                d.BeginEdit(true);
            }
        }

        private void RightDGW(DataGridView d)
        {
            if (dgw.Rows.Count < 1)
            {
                return;
            }
            if (d.CurrentCell.ColumnIndex == 1)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[2];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 2)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 3)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 4)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[6];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 6)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[7];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 7)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[8];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 8)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[1];
                d.BeginEdit(true);
            }
        }

        private void UpDGW(DataGridView d)
        {
            if (dgw.Rows.Count < 1)
            {
                return;
            }
            int curent = d.CurrentRow.Index;
            if (curent == 0)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[1];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index - 1].Cells[1];
                d.BeginEdit(true);
            }
        }

        private void DownDGW(DataGridView d)
        {
            if (dgw.Rows.Count < 1)
            {
                return;
            }
            int curent = d.CurrentRow.Index;
            if (curent == d.RowCount - 1)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[1];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index + 1].Cells[1];
                d.BeginEdit(true);
            }
        }
    }
}