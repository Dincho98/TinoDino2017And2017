using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmPrimka : Form
    {
        public frmPrimka()
        {
            InitializeComponent();
        }

        private string skladiste_pocetno = "";
        private bool edit = false;

        private DataTable DT_Skladiste;
        private DataTable DT_stavke;
        private DataTable DTRoba;

        //ova 4 datatablea nam trebaju za konkurentnost
        private DataTable PRIMKA_IZ_BAZE;

        private DataTable TRENUTNA_PRIMKA_IZ_BAZE;
        private DataTable STAVKE_IZ_BAZE;
        private DataTable TRENUTNA_STAVKA_IZ_BAZE;

        public int broj_primke { get; set; }
        public int broj_skladista { get; set; }

        private string ID_PRIMKA;
        public frmMenu MainForm { get; set; }

        private void frmPrimka_Load(object sender, EventArgs e)
        {
            /****************************SINKRONIZACIJA SA WEB-OM*****************/
            bgSinkronizacija = new BackgroundWorker();
            bgSinkronizacija.DoWork += new DoWorkEventHandler(bgSinkronizacija_DoWork);
            bgSinkronizacija.WorkerSupportsCancellation = true;
            /****************************SINKRONIZACIJA SA WEB-OM*****************/

            MyDataGrid.MainForm = this;
            classSQL.update("UPDATE primka_stavke SET kolicina=REPLACE(kolicina,'.',',')");
            createTablesRobno c = new createTablesRobno();
            c.create();

            setDefault();
            setTextBrojPrimke();
            ControlDisableEnable(true, false, false, false, false, true);
            EnableDisable(false);
            this.Paint += new PaintEventHandler(Form1_Paint);
            if (broj_primke != 0 && broj_skladista != 0)
            {
                FillPrimka(broj_primke, broj_skladista);
            }
            setTextIzradio(Properties.Settings.Default.id_zaposlenik);
        }

        /****************************SINKRONIZACIJA SA WEB-OM*****************/
        private BackgroundWorker bgSinkronizacija = null;
        private synWeb.synPokretac PokretacSinkronizacije = new synWeb.synPokretac();
        /****************************SINKRONIZACIJA SA WEB-OM*****************/

        private void frmPrimka_FormClosing(object sender, FormClosingEventArgs e)
        {
            /****************************SINKRONIZACIJA SA WEB-OM*****************/
            bgSinkronizacija.RunWorkerAsync();
            /****************************SINKRONIZACIJA SA WEB-OM*****************/
        }

        private void bgSinkronizacija_DoWork(object sender, DoWorkEventArgs e)
        {
            PokretacSinkronizacije.PokreniSinkronizaciju(true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
        }

        #region Customize DataGridView

        private class MyDataGrid : System.Windows.Forms.DataGridView
        {
            public static frmPrimka MainForm { get; set; }

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
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void EnterDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            if (d.CurrentCell.ColumnIndex == 4)
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
                int curent = d.CurrentRow.Index;
                txtSifra_robe.Text = "";
                txtSifra_robe.Focus();
            }
        }

        private void LeftDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            if (d.CurrentCell.ColumnIndex == 4)
            {
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
        }

        private void RightDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            if (d.CurrentCell.ColumnIndex == 4)
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
                int curent = d.CurrentRow.Index;
                txtSifra_robe.Text = "";
                txtSifra_robe.Focus();
            }
        }

        private void UpDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            int curent = d.CurrentRow.Index;
            if (d.CurrentCell.ColumnIndex == 4)
            {
            }
            else if (curent == 0)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index - 1].Cells[4];
                d.BeginEdit(true);
            }
        }

        private void DownDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            int curent = d.CurrentRow.Index;
            if (d.CurrentCell.ColumnIndex == 4)
            {
                SendKeys.Send("{F4}");
            }
            else if (curent == d.RowCount - 1)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index + 1].Cells[4];
                d.BeginEdit(true);
            }
        }

        #endregion Customize DataGridView

        #region Buttons

        private void button2_Click(object sender, EventArgs e)
        {
            Report.Robno.repPrimka rav = new Report.Robno.repPrimka();
            rav.dokumenat = "Pri";
            rav.ImeForme = "Primka";
            rav.broj_dokumenta = txtBroj.Text;
            rav.broj_skladista = cbSkladiste.SelectedValue.ToString();
            rav.ShowDialog();
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            setTextBrojPrimke();
            ID_PRIMKA = null;
            EnableDisable(true);
            edit = false;
            ControlDisableEnable(false, true, true, false, false, false);
        }

        private void txtSifra_robe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                txtSifra_robe.Text = txtSifra_robe.Text.Trim();

                if (txtSifra_robe.Text == "")
                {
                    frmRobaTrazi roba = new frmRobaTrazi();
                    roba.ShowDialog();

                    if (Properties.Settings.Default.id_roba != "")
                    {
                        txtSifra_robe.Text = Properties.Settings.Default.id_roba;
                        txtSifra_robe.Select();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    txtSifra_robe.Text = CheckEan(txtSifra_robe.Text);
                }

                selectRobaHelper(txtSifra_robe.Text);
            }
        }

        /// <summary>
        /// Checks if any article contains given code
        /// </summary>
        /// <param name="code"></param>
        private string CheckEan(string code)
        {
            string result = null;
            DataTable DTroba = Global.Database.GetRoba();
            if (DTroba?.Rows.Count > 0)
            {
                foreach (DataRow row in DTroba.Rows)
                {
                    string ean = row["ean"].ToString();
                    if (ean != "-1")
                    {
                        string[] array = ean.Split(';');
                        int index = Array.IndexOf(array, code);
                        if (index > -1)
                        {
                            result = row["sifra"].ToString();
                            break;
                        }
                    }
                }
            }
            return result;
        }

        private void btnOpenRoba_Click(object sender, EventArgs e)
        {
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            roba_trazi.ShowDialog();
            string propertis_sifra = Properties.Settings.Default.id_roba.Trim();
            if (propertis_sifra != "")
            {
                selectRobaHelper(propertis_sifra);
            }
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            Robno.frmSvePrimke objForm2 = new Robno.frmSvePrimke();
            //objForm2.sifra_fakture = "";
            objForm2.MainForm = this;
            objForm2.ShowDialog();
            if (broj_primke != 0 && broj_skladista != 0)
            {
                deleteFields();
                FillPrimka(broj_primke, broj_skladista);
            }
        }

        private void btnDeleteAllFaktura_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Brisanjem ove povratnice vraćate i količinu robe na skladišta. Da li ste sigurni da želite obrisati ovu povratnicu?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                provjera_sql(classSQL.delete("DELETE FROM primka WHERE broj='" + ID_PRIMKA + "'"));

                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    deleteStavkaHelper(i);
                }
                dgw.Rows.Clear();

                spremiAktivnostZaposlenika(Properties.Settings.Default.id_zaposlenik, DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"), "Brisanje cijele povratnice dobavljaču br." + txtBroj.Text);
                Util.AktivnostZaposlenika.SpremiAktivnost(dgw, cbSkladiste.SelectedValue.ToString(), "Primka", txtBroj.Text, true);
                MessageBox.Show("Obrisano.");

                edit = false;
                EnableDisable(false);
                deleteFields();
                ControlDisableEnable(true, false, false, true, false, true);
            }
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dgw.RowCount > 0)
            {
                deleteStavkaRoba();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtSifraOdrediste.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            EnableDisable(false);
            deleteFields();
            setTextBrojPrimke();
            edit = false;
            ControlDisableEnable(true, false, false, false, false, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            decimal dec_parse;

            if (!Decimal.TryParse(txtSifraOdrediste.Text, out dec_parse) || txtSifraOdrediste.Text == "")
            {
                MessageBox.Show("GREŠKA\r\nGreška kod upisa šifre odredišta.", "Greška");
                return;
            }

            bool spremljeno = SetPrimka();

            try
            {
                string sql = @"update primka_stavke a
set proizvodacka_cijena = b.vpc
from (
	select c.id_stavka, (c.nbc / (1 zbroj (35::numeric / 100::numeric))) as vpc
	from primka_stavke c
) b
where b.id_stavka = a.id_stavka;";
                //classSQL.update(sql);
            }
            catch (Exception)
            {
            }

            if (spremljeno)
            {
                EnableDisable(false);
                deleteFields();
                setTextBrojPrimke();
                edit = false;
                ControlDisableEnable(true, false, false, false, false, true);
                MessageBox.Show("Spremljeno.");
            }
            else
            {
                FillPrimka(Convert.ToInt64(txtBroj.Text), Convert.ToInt16(cbSkladiste.SelectedValue));
            }
        }

        #endregion Buttons

        #region Insert/Update Primka/Stavke

        /// <summary>
        /// Sprema primku u bazu. Ako je došlo do greške zbog konkurentnosti ili
        /// nešto drugo vraća false
        /// </summary>
        /// <returns></returns>
        private bool SetPrimka()
        {
            string sql = "SELECT broj FROM primka WHERE broj = '" + txtBroj.Text +
                "' AND id_skladiste='" + cbSkladiste.SelectedValue +
                "' AND godina='" + nmGodina.Value.ToString() + "'";
            DataTable DTbool = classSQL.select(sql, "primka").Tables[0];

            if (DTbool.Rows.Count == 0)
            {
                InsertPrimka();

                SetPrimkaStavke();

                spremiAktivnostZaposlenika(Properties.Settings.Default.id_zaposlenik, DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"),
                    "Nova Primka br." + txtBroj.Text + "/" + nmGodina.Value.ToString() + ", Skladište: " + cbSkladiste.SelectedValue);
                Util.AktivnostZaposlenika.SpremiAktivnost(dgw, cbSkladiste.SelectedValue.ToString(), "Primka", txtBroj.Text, false);
                return true;
            }
            else
            {
                //ovaj prvi if znači sljedeće:
                //prvi korisnik je kliknuo novi zapis,
                //drugi korisnik je u međuvremenu kreirao i spremio u bazu isti zapis (zato je i DTbool.Rows.Count>0);
                //prvi korisnik je kliknuo spremi,
                //program dolazi do ovog našeg ifa. edit je postavljen na false jer je prvi korisnik kliknuo na novi zapis
                if (!edit)
                {
                    MessageBox.Show("Drugi korisnik je već kreirao primku '" + txtBroj.Text + "/" + nmGodina.Value.ToString() + "'. " +
                        Environment.NewLine + Environment.NewLine +
                        "Pokušaj kreiranja primke '" + (Convert.ToInt64(txtBroj.Text) + 1).ToString() + "/" + nmGodina.Value.ToString() + "'...", "UPOZORENJE!");

                    //kaj sad napraviti? dal da se učita nova primka i izbriše nesejvana primka koju je korisnik napravil?
                    //ili da proba spremiti primku s brojačem + 1, npr.:
                    spremiAktivnostZaposlenika(Properties.Settings.Default.id_zaposlenik, DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"),
                        "UPOZORENJE! Pokušaj kreiranja primke br." + txtBroj.Text + "/" + nmGodina.Value.ToString() +
                        ", Skladište: " + cbSkladiste.SelectedValue);

                    txtBroj.Text = (Convert.ToInt64(txtBroj.Text) + 1).ToString();
                    SetPrimka();

                    return true;
                }

                if (!UpdatePrimka())
                {
                    MessageBox.Show("Drugi korisnik je već uredio ovu primku. " +
                        "Primka '" + txtBroj.Text + "/" + nmGodina.Value.ToString() + "' s ažuriranim podacima bit će ponovo učitana!", "UPOZORENJE!");
                    spremiAktivnostZaposlenika(Properties.Settings.Default.id_zaposlenik, DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"),
                        "UPOZORENJE! Pokušaj uređivanja Primke br." + txtBroj.Text + "/" + nmGodina.Value.ToString() +
                        ", Skladište: " + cbSkladiste.SelectedValue);

                    return false;
                }

                //treba riješiti konkurentnost i za stavke!!!
                SetPrimkaStavke();

                spremiAktivnostZaposlenika(Properties.Settings.Default.id_zaposlenik, DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"),
                    "Uređivanje Primke br." + txtBroj.Text + "/" + nmGodina.Value.ToString() + ", Skladište: " + cbSkladiste.SelectedValue);
                Util.AktivnostZaposlenika.SpremiAktivnost(dgw, cbSkladiste.SelectedValue.ToString(), "Primka", txtBroj.Text, true);

                return true;
            }
        }

        private void InsertPrimka()
        {
            //LOCK TABLE primka IN SHARE ROW EXCLUSIVE MODE; (to nakon begin)
            //string sql = "BEGIN;" +
            string sql = "INSERT INTO primka (broj,godina,datum,id_mjesto,originalni_dokument," +
                "id_izradio,napomena,id_skladiste,id_partner,novo) VALUES (" +
                 " '" + txtBroj.Text + "'," +
                 " '" + nmGodina.Value.ToString() + "'," +
                 " '" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                 " '" + cbMjesto.SelectedValue + "'," +
                 " '" + txtOrginalniDok.Text + "'," +
                 " '" + Properties.Settings.Default.id_zaposlenik + "'," +
                 " '" + rtbNapomena.Text + "'," +
                 " '" + cbSkladiste.SelectedValue + "'," +
                 " '" + txtSifraOdrediste.Text + "','1');";
            provjera_sql(classSQL.insert(sql));

            //sad treba pokupiti id iz baze od nove primke!!!
            ID_PRIMKA = classSQL.select("SELECT id_primka FROM primka WHERE" +
                " broj='" + txtBroj.Text + "' AND " +
                " godina='" + nmGodina.Value.ToString() + "' AND " +
                " datum='" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "' AND " +
                " id_mjesto='" + cbMjesto.SelectedValue + "' AND " +
                " originalni_dokument='" + txtOrginalniDok.Text + "' AND " +
                " id_izradio='" + Properties.Settings.Default.id_zaposlenik + "' AND " +
                " napomena='" + rtbNapomena.Text + "' AND " +
                " id_skladiste='" + cbSkladiste.SelectedValue + "' AND " +
                " id_partner='" + txtSifraOdrediste.Text + "';" +
                " ", "primka").Tables[0].Rows[0][0].ToString();
        }

        /// <summary>
        /// Vraća true ako je primka uspješno ažurirana, false ako je došlo do greške zbog konkurentnosti
        /// </summary>
        /// <returns></returns>
        private bool UpdatePrimka()
        {
            //prvo treba spremiti početne vrijednosti učitane primke (spremljene u globalni PRIMKA_IZ_BAZE)
            //zatim prije apdejtanja provjeriti da li su prethodno učitane
            //vrijednosti iz baze jednake trenutnim vrijednostima iz baze  (spremljene u globalni TRENUTNA_PRIMKA_IZ_BAZE)
            //ako jesu nastavi s apdejtom, ako nisu upozori korisnika da
            //ponovo učita podatke iz baze

            if (UsporediPrimke(PRIMKA_IZ_BAZE))
            {
                string sql = "UPDATE primka SET " +
                    " godina='" + nmGodina.Value.ToString() + "'," +
                    " datum='" + dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    " id_mjesto='" + cbMjesto.SelectedValue + "'," +
                    " originalni_dokument='" + txtOrginalniDok.Text + "'," +
                    " id_izradio='" + Properties.Settings.Default.id_zaposlenik + "'," +
                    " napomena='" + rtbNapomena.Text + "'," +
                    " editirano='1'," +
                    " id_partner='" + txtSifraOdrediste.Text + "'" +
                    " WHERE broj='" + txtBroj.Text + "'" +
                    " AND id_skladiste='" + cbSkladiste.SelectedValue + "'" +
                    " AND godina='" + nmGodina.Value.ToString() + "'";
                provjera_sql(classSQL.update(sql));

                return true;
            }
            else
            {
                //vrijednosti učitane primke i primke iz baze se ne poklapaju
                //tj. drugi korisnik je već izmjenio podatke
                return false;
            }
        }

        /// <summary>
        /// Uspoređuje zadanu primku s primkom iz baze
        /// </summary>
        /// <param name="PRIMKA_IZ_BAZE"></param>
        /// <returns></returns>
        private bool UsporediPrimke(DataTable PRIMKA_IZ_BAZE)
        {
            string sql = "SELECT * FROM primka WHERE broj = '" + txtBroj.Text +
                 "' AND id_skladiste = '" + cbSkladiste.SelectedValue +
                 "' AND godina= '" + nmGodina.Value.ToString() + "'; ";
            TRENUTNA_PRIMKA_IZ_BAZE = classSQL.select(sql, "primka").Tables[0];

            if (TRENUTNA_PRIMKA_IZ_BAZE.Rows.Count < 1) return false;

            for (int i = 0; i < PRIMKA_IZ_BAZE.Columns.Count; i++)
            {
                if (PRIMKA_IZ_BAZE.Rows[0][i].ToString() != TRENUTNA_PRIMKA_IZ_BAZE.Rows[0][i].ToString())
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Ažurira stavke iz datagridviewa
        /// </summary>
        private void SetPrimkaStavke()
        {
            for (int i = 0; i < dgw.RowCount; i++)
            {
                DataGridViewRow r = dgw.Rows[i];
                //ako nema id onda je nova stavka pa ju treba samo ubaciti u bazu
                if (r.Cells["id_stavka"].FormattedValue.ToString() == "")
                {
                    PrimkaStavkeInsert(r);

                    SetRoba(r.Cells["sifra"].FormattedValue.ToString(), r.Cells["kolicina"].FormattedValue.ToString(), "+");
                }
                else
                {
                    if (!PrimkaStavkeUpdate(r))
                    {
                        MessageBox.Show("Drugi korisnik je već uredio stavku s robom '" +
                            r.Cells["sifra"].FormattedValue.ToString() + "'. " +
                            "Navedena stavka neće biti spremljena! Nakon što program spremi primku sa svim " +
                            "ostalim stavkama, ponovo učitajte ovu primku za dodavanje novih stavaka.", "UPOZORENJE!");

                        spremiAktivnostZaposlenika(Properties.Settings.Default.id_zaposlenik, DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"),
                        "UPOZORENJE! Pokušaj uređivanja Primke br." + txtBroj.Text + "/" + nmGodina.Value.ToString() +
                        ", Skladište: " + cbSkladiste.SelectedValue + ". Stavka: " +
                        r.Cells["sifra"].FormattedValue.ToString() + " (Konkurentnost!)");
                    }
                    else
                    {
                        //stavka je spremljena/apdejtana
                    }
                }
            }
        }

        private void PrimkaStavkeInsert(DataGridViewRow r)
        {
            string ssql = "INSERT INTO primka_stavke (sifra,vpc,mpc,nbc,pdv,kolicina,rabat,ukupno,iznos,broj,id_primka) VALUES (" +
                "'" + r.Cells["sifra"].FormattedValue.ToString() + "'," +
                "'" + r.Cells["vpc"].FormattedValue.ToString().Replace(",", ".") + "'," +
                "'" + r.Cells["mpc"].FormattedValue.ToString().Replace(",", ".") + "'," +
                "'" + r.Cells["nbc"].FormattedValue.ToString().Replace(",", ".") + "'," +
                "'" + r.Cells["pdv"].FormattedValue.ToString().Replace(".", ",") + "'," +
                "'" + r.Cells["kolicina"].FormattedValue.ToString().Replace(".", ",") + "'," +
                "'" + r.Cells["rabat"].FormattedValue.ToString().Replace(",", ".") + "'," +
                "'" + r.Cells["ukupno"].FormattedValue.ToString().Replace(",", ".") + "'," +
                "'" + r.Cells["iznos"].FormattedValue.ToString().Replace(",", ".") + "'," +
                "'" + ID_PRIMKA + "'," +
                "'" + ID_PRIMKA + "')";
            provjera_sql(classSQL.insert(ssql));
        }

        /// <summary>
        /// Ako je stavka uspješno ažurirana vraća true, false inače
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        private bool PrimkaStavkeUpdate(DataGridViewRow r)
        {
            string idStavka = r.Cells["id_stavka"].FormattedValue.ToString();

            if (UsporediPrimkaStavke(idStavka))
            {
                //treba smanjiti za prethodni iznos i tek onda povečati
                DataRow[] dr = STAVKE_IZ_BAZE.Select("id_stavka=" + idStavka);
                SetRoba(dr[0]["sifra"].ToString(), dr[0]["kolicina"].ToString().Trim().Replace(".", ","), "-");

                string ssql = "UPDATE primka_stavke SET" +
                    " sifra='" + r.Cells["sifra"].FormattedValue.ToString() + "'," +
                    " vpc='" + r.Cells["vpc"].FormattedValue.ToString().Replace(",", ".") + "'," +
                    " mpc='" + r.Cells["mpc"].FormattedValue.ToString().Replace(",", ".") + "'," +
                    " nbc='" + r.Cells["nbc"].FormattedValue.ToString().Replace(",", ".") + "'," +
                    " pdv='" + r.Cells["pdv"].FormattedValue.ToString().Replace(".", ",") + "'," +
                    " kolicina='" + r.Cells["kolicina"].FormattedValue.ToString().Replace(".", ",") + "'," +
                    " rabat='" + r.Cells["rabat"].FormattedValue.ToString().Replace(",", ".") + "'," +
                    " ukupno='" + r.Cells["ukupno"].FormattedValue.ToString().Replace(",", ".") + "'," +
                    " iznos='" + r.Cells["iznos"].FormattedValue.ToString().Replace(",", ".") + "'," +
                    " broj='" + txtBroj.Text + "'" +
                    " WHERE id_stavka='" + r.Cells["id_stavka"].FormattedValue.ToString() + "'";
                provjera_sql(classSQL.update(ssql));

                SetRoba(r.Cells["sifra"].FormattedValue.ToString(), r.Cells["kolicina"].FormattedValue.ToString(), "+");

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool UsporediPrimkaStavke(string idStavka)
        {
            string sql = "SELECT * FROM primka_stavke WHERE id_stavka = '" + idStavka + "'";
            TRENUTNA_STAVKA_IZ_BAZE = classSQL.select(sql, "primka").Tables[0];
            DataRow[] dr = STAVKE_IZ_BAZE.Select("id_stavka=" + idStavka);

            if (TRENUTNA_STAVKA_IZ_BAZE.Rows.Count < 1) return false;

            return TRENUTNA_STAVKA_IZ_BAZE.Rows[0]["sifra"].ToString() == dr[0]["sifra"].ToString() &&
                TRENUTNA_STAVKA_IZ_BAZE.Rows[0]["kolicina"].ToString() == dr[0]["kolicina"].ToString() &&
                TRENUTNA_STAVKA_IZ_BAZE.Rows[0]["vpc"].ToString() == dr[0]["vpc"].ToString() &&
                TRENUTNA_STAVKA_IZ_BAZE.Rows[0]["nbc"].ToString() == dr[0]["nbc"].ToString() &&
                TRENUTNA_STAVKA_IZ_BAZE.Rows[0]["pdv"].ToString() == dr[0]["pdv"].ToString() &&
                TRENUTNA_STAVKA_IZ_BAZE.Rows[0]["id_stavka"].ToString() == dr[0]["id_stavka"].ToString() &&
                TRENUTNA_STAVKA_IZ_BAZE.Rows[0]["id_primka"].ToString() == dr[0]["id_primka"].ToString() &&
                TRENUTNA_STAVKA_IZ_BAZE.Rows[0]["rabat"].ToString() == dr[0]["rabat"].ToString() &&
                //TRENUTNA_STAVKA_IZ_BAZE.Rows[0]["mpc"].ToString() == dr[0]["mpc"].ToString() &&
                TRENUTNA_STAVKA_IZ_BAZE.Rows[0]["iznos"].ToString() == dr[0]["iznos"].ToString() &&
                TRENUTNA_STAVKA_IZ_BAZE.Rows[0]["ukupno"].ToString() == dr[0]["ukupno"].ToString();
        }

        #endregion Insert/Update Primka/Stavke

        #region Delete Stavka, Update skladište

        /// <summary>
        /// Briše stavke iz datagrida, iz baze i ažurira robu na skladištu
        /// </summary>
        private void deleteStavkaRoba()
        {
            string a = dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString();

            //stavka je tek dodana (još ne postoji u bazi), pa samo obrišemo iz datagrida
            if (dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].Value == null)
            {
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                MessageBox.Show("Obrisano.");
            }

            //stavka je već u bazi pa ju moramo izbrisati iz baze i ažurirati skladište
            else
            {
                if (MessageBox.Show("Brisanjem ove stavke vraćate i količinu robe na skladišta. Da li ste sigurni da želite obrisati ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    DataGridViewRow r = dgw.CurrentRow;

                    deleteStavkaHelper(r.Index);
                    dgw.Rows.RemoveAt(r.Index);
                    Util.AktivnostZaposlenika.SpremiAktivnost(dgw, cbSkladiste.SelectedValue.ToString(), "Primka", txtBroj.Text, true);
                    MessageBox.Show("Obrisano.");
                }
            }
        }

        private void deleteStavkaHelper(int redak)
        {
            DataGridViewRow r = dgw.Rows[redak];

            string s = r.Cells["id_stavka"].FormattedValue.ToString();

            SetRoba(r.Cells["sifra"].FormattedValue.ToString(), r.Cells["kolicina"].FormattedValue.ToString(), "-");

            classSQL.delete("DELETE FROM primka_stavke WHERE id_stavka='" + s + "'");
            //dgw.Rows.RemoveAt(redak);
            spremiAktivnostZaposlenika(Properties.Settings.Default.id_zaposlenik, DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"), "Brisanje stavke " + s + " sa povratnice robe br." + txtBroj.Text);
        }

        #endregion Delete Stavka, Update skladište

        #region Set roba na skladištu, addrowtodatagrid

        private void SetRoba(string sifra, string kolicina, string plusMinus)
        {
            //(trenutna kolicina=)kol = prijašnja kolicina + dodana kolicina
            string kol = SQL.ClassSkladiste.GetAmount(sifra,
                cbSkladiste.SelectedValue.ToString(),
                kolicina,
                "1",
                plusMinus);

            //update trenutna kolicina
            SQL.SQLroba_prodaja.UpdateRows(
                cbSkladiste.SelectedValue.ToString(),
                kol,
                sifra);
        }

        //void setRoba()
        //{
        //    for (int i = 0; i < dgw.RowCount; i++)
        //    {
        //        if (dgw.Rows[i].Cells["id_stavka"].FormattedValue.ToString() == "")
        //        {
        //            //nova stavka, ubacujemo u bazu
        //            DataGridViewRow r = dgw.Rows[i];

        //            //(trenutna kolicina=)kol = prijašnja kolicina + dodana kolicina
        //            string kol = SQL.ClassSkladiste.GetAmount(r.Cells["sifra"].FormattedValue.ToString(),
        //                                                cbSkladiste.SelectedValue.ToString(),
        //                                                r.Cells["kolicina"].FormattedValue.ToString(), "1", "+");

        //            //update trenutna kolicina
        //            SQL.SQLroba_prodaja.UpdateRows(cbSkladiste.SelectedValue.ToString(),
        //                                                kol,
        //                                                r.Cells["sifra"].FormattedValue.ToString());
        //        }
        //        else
        //        {
        //            //tu se ne bi smjeli nalaziti jer bi sve stavke trebale imati id_stavka==null
        //            string a = "";
        //        }

        //    }
        //}

        private void addRowToDatagridview()
        {
            dgw.Rows.Add();
            int br = dgw.Rows.Count - 1;
            DataGridViewRow r = dgw.Rows[br];
            dgw.Select();
            dgw.CurrentCell = dgw.Rows[br].Cells[4];

            decimal _NBC = Util.Korisno.VratiNabavnuCijenu(DTRoba.Rows[0]["sifra"].ToString(), cbSkladiste.SelectedValue.ToString());

            double vpc = Convert.ToDouble(DTRoba.Rows[0]["vpc"].ToString());
            double mpc = Convert.ToDouble(DTRoba.Rows[0]["mpc"].ToString());
            decimal nbc = _NBC;
            double pdv = Convert.ToDouble(DTRoba.Rows[0]["porez"].ToString());
            //double ukupno = Convert.ToDouble(DTRoba.Rows[0]["ukupno"].ToString());

            r.Cells[0].Value = dgw.RowCount;
            r.Cells["sifra"].Value = DTRoba.Rows[0]["sifra"].ToString();
            r.Cells["naziv"].Value = DTRoba.Rows[0]["naziv"].ToString();
            r.Cells["vpc"].Value = vpc.ToString("#0.00");
            r.Cells["nbc"].Value = nbc.ToString("#0.00");
            r.Cells["rabat"].Value = "0";
            r.Cells["mpc"].Value = mpc.ToString("#0.00");
            r.Cells["pdv"].Value = pdv;
            r.Cells["kolicina"].Value = "1";
            r.Cells["ukupno"].Value = "0";
            r.Cells["primkaID"].Value = ID_PRIMKA;//null ako je nova primka
            r.Cells["id_stavka"].Value = null;
        }

        private void addRoba()
        {
            addRowToDatagridview();

            dgw.BeginEdit(true);
        }

        #endregion Set roba na skladištu, addrowtodatagrid

        #region Fill forma

        private void FillPrimka(long broj_primke, int broj_skladista)
        {
            EnableDisable(true);
            deleteFields();

            string sql = "SELECT * FROM primka WHERE broj = '" + broj_primke + "' AND id_skladiste = '" + broj_skladista + "'";
            DataTable DTheader = classSQL.select(sql, "primka").Tables[0];
            PRIMKA_IZ_BAZE = DTheader;

            if (DTheader.Rows.Count < 1)
            {
                EnableDisable(false);
                deleteFields();
                setTextBrojPrimke();
                edit = false;
                ControlDisableEnable(true, false, false, false, false, true);
                MessageBox.Show("U bazi ne postoji primka " + broj_primke + " na skladištu " + broj_skladista);
                return;
            }
            fillHeader(DTheader);

            ////za preslikavanje čitavog datatablea u datagrid
            ////nije dobro jer treba napraviti neke transformacije
            //dgw.DataSource = DT_stavke;

            fillDataGridView(DT_stavke);

            ControlDisableEnable(false, true, true, true, true, false);
            edit = true;
        }

        private void fillDataGridView(DataTable DT_stavke)
        {
            string sql = "SELECT " +
                " primka_stavke.id_stavka," +
                " primka_stavke.sifra," +
                " primka_stavke.kolicina," +
                " primka_stavke.vpc," +
                " primka_stavke.nbc," +
                " primka_stavke.broj," +
                " primka_stavke.pdv," +
                " primka_stavke.rabat," +
                " primka_stavke.ukupno," +
                " primka_stavke.iznos," +
                " primka_stavke.id_primka," +
                " roba.naziv" +
                " FROM primka_stavke " +
                " LEFT JOIN roba ON roba.sifra=primka_stavke.sifra" +
                " WHERE primka_stavke.id_primka = '" + ID_PRIMKA + "'";

            DT_stavke = classSQL.select(sql, "primka_stavke").Tables[0];

            STAVKE_IZ_BAZE = DT_stavke;

            for (int i = 0; i < DT_stavke.Rows.Count; i++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;
                DataGridViewRow r = dgw.Rows[br];
                dgw.Select();
                dgw.CurrentCell = dgw.Rows[br].Cells[3];

                double kolicina = Math.Round(Convert.ToDouble(DT_stavke.Rows[i]["kolicina"].ToString().Replace(".", ",")), 3);
                double vpc = Math.Round(Convert.ToDouble(DT_stavke.Rows[i]["vpc"].ToString()), 3);
                double pdv = Math.Round(Convert.ToDouble(DT_stavke.Rows[i]["pdv"].ToString().Replace(".", ",")), 2);
                double iznos = Math.Round(Convert.ToDouble(DT_stavke.Rows[i]["iznos"].ToString()), 2);
                double ukupno = Math.Round(Convert.ToDouble(DT_stavke.Rows[i]["ukupno"].ToString()), 2);
                double nbc = Math.Round(Convert.ToDouble(DT_stavke.Rows[i]["nbc"].ToString()), 2);

                r.Cells[0].Value = dgw.RowCount;
                r.Cells["sifra"].Value = DT_stavke.Rows[i]["sifra"].ToString();
                r.Cells["naziv"].Value = DT_stavke.Rows[i]["naziv"].ToString();
                r.Cells["kolicina"].Value = kolicina.ToString("#0.000");
                r.Cells["vpc"].Value = vpc.ToString("#0.000");
                r.Cells["nbc"].Value = nbc.ToString("#0.00");
                r.Cells["pdv"].Value = pdv.ToString("#0.00");
                r.Cells["id_stavka"].Value = DT_stavke.Rows[i]["id_stavka"].ToString();
                r.Cells["primkaID"].Value = DT_stavke.Rows[i]["id_primka"].ToString();
                r.Cells["rabat"].Value = DT_stavke.Rows[i]["rabat"].ToString().Replace(".", ",");
                r.Cells["mpc"].Value = Math.Round((vpc + (vpc * pdv / 100)), 2).ToString("#0.00");
                r.Cells["ukupno"].Value = ukupno.ToString("#0.00");
                r.Cells["iznos"].Value = iznos.ToString("#0.00");
            }

            dgw.Columns["ukupno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["ukupno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void fillHeader(DataTable DTheader)
        {
            nmGodina.Value = Convert.ToInt16(DTheader.Rows[0]["godina"].ToString());
            txtBroj.Text = DTheader.Rows[0]["broj"].ToString();
            cbSkladiste.SelectedValue = Convert.ToInt16(DTheader.Rows[0]["id_skladiste"].ToString());
            dtpDatum.Value = Convert.ToDateTime(DTheader.Rows[0]["datum"].ToString());
            rtbNapomena.Text = DTheader.Rows[0]["napomena"].ToString();
            cbMjesto.SelectedValue = Convert.ToInt16(DTheader.Rows[0]["id_mjesto"].ToString());
            txtOrginalniDok.Text = DTheader.Rows[0]["originalni_dokument"].ToString();
            skladiste_pocetno = DTheader.Rows[0]["id_skladiste"].ToString();
            setTextIzradio(DTheader.Rows[0]["id_izradio"].ToString());
            txtSifraOdrediste.Text = DTheader.Rows[0]["id_partner"].ToString();

            //napuni txtSifra
            if (txtSifraOdrediste.Text.Trim() != "")
                txtSifraOdrediste_KeyDown(txtSifraOdrediste, new KeyEventArgs(Keys.Enter));

            ID_PRIMKA = DTheader.Rows[0]["id_primka"].ToString();
        }

        #endregion Fill forma

        #region Util

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private string brojPrimke()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj) FROM primka WHERE id_skladiste='" + cbSkladiste.SelectedValue + "'", "primka").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void setTextBrojPrimke()
        {
            txtBroj.Text = brojPrimke();
        }

        private void setSkladiste()
        {
            DT_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste").Tables[0];
            cbSkladiste.DataSource = DT_Skladiste;
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";
            if (Class.Postavke.proizvodnja_normativ_pc) {
                cbSkladiste.SelectedValue = Class.Postavke.id_default_skladiste_normativ;
            }

        }

        private void setGodina()
        {
            nmGodina.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nmGodina.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nmGodina.Value = DateTime.Now.Year;
        }

        private void setMjesto()
        {
            DataTable DT_Mjesto;
            DT_Mjesto = classSQL.select("SELECT * FROM grad order by grad", "grad").Tables[0];
            cbMjesto.DataSource = DT_Mjesto;
            cbMjesto.DisplayMember = "grad";
            cbMjesto.ValueMember = "id_grad";
        }

        private void setDefault()
        {
            setSkladiste();
            setGodina();
            setMjesto();

            //dgw.Columns["vpc"].Visible = false;
        }

        private void selectRobaHelper(string sifra)
        {
            for (int y = 0; y < dgw.Rows.Count; y++)
            {
                if (sifra == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString().Trim())
                {
                    MessageBox.Show("Artikl ili usluga već postoje!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            string sql = "SELECT * FROM roba" +
                " WHERE sifra='" + sifra + "' AND oduzmi='DA'";

            DTRoba = classSQL.select(sql, "roba_prodaja").Tables[0];
            if (DTRoba.Rows.Count > 0)
            {
                txtSifra_robe.BackColor = Color.White;
                addRoba();
            }
            else
            {
                MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void setTextIzradio(string id_zaposlenik)
        {
            txtIzradio.Text = getIzradio(id_zaposlenik);
        }

        private string getIzradio(string id_zaposlenik)
        {
            return classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" +
                id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
        }

        private void spremiAktivnostZaposlenika(string id, string datum, string radnja)
        {
            provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" +
            id + "','" + datum + "','" + radnja + "')"));
        }

        private void ControlDisableEnable(bool novi, bool odustani, bool spremi, bool ispis, bool obrisi, bool sve)
        {
            btnNoviUnos.Enabled = novi;

            btnOdustani.Enabled = odustani;

            btnSpremi.Enabled = spremi;

            btnIspis.Enabled = ispis;

            btnDeleteAllFaktura.Enabled = obrisi;

            btnSveFakture.Enabled = sve;
        }

        private void deleteFields()
        {
            dtpDatum.Text = "";
            rtbNapomena.Text = "";
            txtSifraOdrediste.Text = "";
            txtPartnerNaziv.Text = "";
            txtOrginalniDok.Text = "";
            txtSifra_robe.Text = "";
            dgw.Rows.Clear();
        }

        private void EnableDisable(bool x)
        {
            dtpDatum.Enabled = x;
            rtbNapomena.Enabled = x;
            txtSifraOdrediste.Enabled = x;
            cbMjesto.Enabled = x;
            txtOrginalniDok.Enabled = x;
            txtSifra_robe.Enabled = x;
            btnOpenRoba.Enabled = x;
            btnObrisi.Enabled = x;
            pictureBox1.Enabled = x;

            txtBroj.Enabled = !x;
            nmGodina.Enabled = !x;
            cbSkladiste.Enabled = !x;
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        #endregion Util

        #region DataGridView helpers

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.Rows.Count < 1)
                return;
            dgw.BeginEdit(true);
        }

        private string dg(int row, string cell)
        {
            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        /// <summary>
        /// Sređuje cijene u retku datagrida. Određuje iznos i ukupan iznos prema količini, veleprodajnoj cijeni i porezu
        /// </summary>
        /// <param name="red"></param>
        private void izracun(int red)
        {
            decimal dec_parse;
            if (!Decimal.TryParse(dgw.Rows[red].Cells["kolicina"].FormattedValue.ToString(), out dec_parse))
            {
                dgw.Rows[red].Cells["kolicina"].Value = "1";
                MessageBox.Show("Greška kod upisa količine.", "Greška"); return;
            }
            if (!Decimal.TryParse(dgw.Rows[red].Cells["rabat"].FormattedValue.ToString(), out dec_parse))
            {
                dgw.Rows[red].Cells["rabat"].Value = "0";
                MessageBox.Show("Greška kod upisa rabata.", "Greška"); return;
            }
            if (!Decimal.TryParse(dgw.Rows[red].Cells["vpc"].FormattedValue.ToString(), out dec_parse))
            {
                dgw.Rows[red].Cells["vpc"].Value = "0,000";
                MessageBox.Show("Greška kod upisa veleprodajne cijene.", "Greška"); return;
            }
            if (!Decimal.TryParse(dgw.Rows[red].Cells["mpc"].FormattedValue.ToString(), out dec_parse))
            {
                dgw.Rows[red].Cells["mpc"].Value = "0,00";
                MessageBox.Show("Greška kod upisa maloprodajne cijene.", "Greška"); return;
            }
            if (!Decimal.TryParse(dgw.Rows[red].Cells["pdv"].FormattedValue.ToString(), out dec_parse))
            {
                dgw.Rows[red].Cells["pdv"].Value = "0,00";
                MessageBox.Show("Greška kod upisa pdv-a.", "Greška"); return;
            }
            if (!Decimal.TryParse(dgw.Rows[red].Cells["nbc"].FormattedValue.ToString(), out dec_parse))
            {
                dgw.Rows[red].Cells["nbc"].Value = "0,00";
                MessageBox.Show("Greška kod upisa nabavne cijene.", "Greška"); return;
            }

            try
            {
                dgw.Rows[red].Cells["vpc"].Value = dgw.Rows[red].Cells["vpc"].Value.ToString().Replace(".", ",");
                dgw.Rows[red].Cells["kolicina"].Value = dgw.Rows[red].Cells["kolicina"].Value.ToString().Replace(".", ",");
                dgw.Rows[red].Cells["pdv"].Value = dgw.Rows[red].Cells["pdv"].Value.ToString().Replace(".", ",");
                dgw.Rows[red].Cells["mpc"].Value = dgw.Rows[red].Cells["mpc"].Value.ToString().Replace(".", ",");
                dgw.Rows[red].Cells["nbc"].Value = dgw.Rows[red].Cells["nbc"].Value.ToString().Replace(".", ",");
                dgw.Rows[red].Cells["rabat"].Value = dgw.Rows[red].Cells["rabat"].Value.ToString().Replace(".", ",");

                //provjera dal su podaci numerički
                //ako nisu dolazi u catch
                double vpc = Math.Round(Convert.ToDouble(dgw.Rows[red].Cells["vpc"].Value), 3);
                double kol = Math.Round(Convert.ToDouble(dgw.Rows[red].Cells["kolicina"].Value.ToString().Replace(".", ",")), 3);
                double pdv = Math.Round(Convert.ToDouble(dgw.Rows[red].Cells["pdv"].Value.ToString().Replace(".", ",")), 2);
                double mpc = Math.Round(Convert.ToDouble(dgw.Rows[red].Cells["mpc"].Value.ToString().Replace(".", ",")), 2);
                double nbc = Math.Round(Convert.ToDouble(dgw.Rows[red].Cells["nbc"].Value.ToString().Replace(".", ",")), 2);
                double rabat = Math.Round(Convert.ToDouble(dgw.Rows[red].Cells["rabat"].Value.ToString().Replace(".", ",")), 2);
                //double vpc = Convert.ToDouble(dgw.Rows[red].Cells["vpc"].Value.ToString().Replace(".", ","));
                //double kol = Convert.ToDouble(dgw.Rows[red].Cells["kolicina"].Value.ToString().Replace(".", ","));
                //double pdv = Convert.ToDouble(dgw.Rows[red].Cells["pdv"].Value.ToString().Replace(".", ","));
                //double mpc = Convert.ToDouble(dgw.Rows[red].Cells["mpc"].Value.ToString().Replace(".", ","));
                //double nbc = Convert.ToDouble(dgw.Rows[red].Cells["nbc"].Value.ToString().Replace(".", ","));
                //double rabat = Convert.ToDouble(dgw.Rows[red].Cells["rabat"].Value.ToString().Replace(".", ","));

                double iznosBezPdv, vpcUkupno, mpcUkupno;

                if (dgw.CurrentCell.ColumnIndex == 6)
                {
                    //izračun mpc -> vpc
                    mpcUkupno = mpc * kol;
                    vpcUkupno = mpcUkupno * (1 - rabat / 100);
                    iznosBezPdv = vpcUkupno / (1 + pdv / 100);
                    vpc = mpc / (1 + pdv / 100);
                    //izračun
                }
                else
                {
                    //izračun vpc -> mpc
                    iznosBezPdv = vpc * kol;
                    vpcUkupno = iznosBezPdv * (1 + pdv / 100);
                    mpcUkupno = vpcUkupno / (1 - rabat / 100);
                    mpc = mpcUkupno / kol;
                    //izračun
                }

                dgw.Rows[red].Cells["ukupno"].Value = Math.Round(vpcUkupno, 2).ToString("#0.00");
                dgw.Rows[red].Cells["iznos"].Value = Math.Round(iznosBezPdv, 2).ToString("#0.00");
                dgw.Rows[red].Cells["vpc"].Value = Math.Round(vpc, 3).ToString("#0.000");
                dgw.Rows[red].Cells["mpc"].Value = Math.Round(mpc, 2).ToString("#0.00");

                moveToCellHelper(dgw.CurrentCell.ColumnIndex, true);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Greška!\n\n" + ex.Message);
                //moveToCellHelper(dgw.CurrentCell.ColumnIndex, false);
            }

            dgw.Refresh();
        }

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.CurrentCell.ColumnIndex >= 4 && dgw.CurrentCell.ColumnIndex <= 7)
            {
            }
            else if (dgw.CurrentCell.ColumnIndex == 8)
            {
                txtSifra_robe.Text = "";
                txtSifra_robe.Select();
                dgw.ClearSelection();
            }

            izracun(dgw.CurrentCell.RowIndex);
        }

        private void moveToCellHelper(int i, bool t)
        {
            try
            {
                dgw.CurrentCell = t ? dgw.CurrentRow.Cells[i + 1] : dgw.CurrentRow.Cells[i];
                dgw.BeginEdit(true);
            }
            catch (Exception)
            {
                //MessageBox.Show("Koristite enter za sljedeću kolonu.");
            }
        }

        #endregion DataGridView helpers

        #region ON_KEY_DOWN

        private void txtSifraOdrediste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtSifraOdrediste.Text == "")
                {
                    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                    partnerTrazi.ShowDialog();
                    if (Properties.Settings.Default.id_partner != "")
                    {
                        DataSet partner = new DataSet();
                        partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                        if (partner.Tables[0].Rows.Count > 0)
                        {
                            txtSifraOdrediste.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                            txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                            //txtSifraFakturirati.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                            //txtPartnerNaziv1.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString(); cbVD.Select();
                            //txtSifraFakturirati.Select();
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtSifraOdrediste.Select();//staviti return; tu dole ili ne?
                        }
                    }
                    else
                    {
                        txtSifraOdrediste.Select();
                        return;
                    }
                }

                string Str = txtSifraOdrediste.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtSifraOdrediste.Text = "0";
                }

                DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraOdrediste.Text + "'", "partners").Tables[0];
                if (DSpar.Rows.Count > 0)
                {
                    txtPartnerNaziv.Text = DSpar.Rows[0][0].ToString();
                    //txtSifraFakturirati.Text = txtSifraOdrediste.Text;
                    //txtSifraFakturirati_KeyDown(txtSifraOdrediste, e);

                    System.Windows.Forms.SendKeys.Send("{TAB}");
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void txtBroj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                DataTable DT = classSQL.select("SELECT broj FROM primka WHERE broj='" + txtBroj.Text +
                    "' AND id_skladiste = '" + cbSkladiste.SelectedValue + "'", "primka").Tables[0];

                deleteFields();

                if (DT.Rows.Count == 0)
                {
                    if ((Convert.ToInt16(brojPrimke()) - 1).ToString() == txtBroj.Text.Trim())
                    {
                        edit = false;
                        EnableDisable(true);
                        btnSveFakture.Enabled = false;
                        cbSkladiste.Select();
                        ControlDisableEnable(false, true, true, false, false, false);

                        System.Windows.Forms.SendKeys.Send("{TAB}");
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    broj_primke = Convert.ToInt32(txtBroj.Text);
                    broj_skladista = Convert.ToInt32(cbSkladiste.SelectedValue);
                    FillPrimka(broj_primke, broj_skladista);
                    EnableDisable(true);
                    cbSkladiste.Select();
                    ControlDisableEnable(false, true, true, true, false, false);

                    System.Windows.Forms.SendKeys.Send("{TAB}");
                }
            }
        }

        //private void cbSkladiste_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        dtpDatum.Select();
        //    }
        //    else if (e.KeyCode == Keys.Insert)
        //    {
        //        txtSifra_robe.Select();
        //    }
        //}

        //private void dtpDatum_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        txtSifraOdrediste.Select();
        //    }
        //    else if (e.KeyCode == Keys.Insert)
        //    {
        //        txtSifra_robe.Select();
        //    }
        //}

        private void txtSifraOdredista_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                string Str = txtSifraOdrediste.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtSifraOdrediste.Text = "0";
                }

                DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraOdrediste.Text + "'", "partners").Tables[0];
                if (DSpar.Rows.Count > 0)
                {
                    txtPartnerNaziv.Text = DSpar.Rows[0][0].ToString();
                    System.Windows.Forms.SendKeys.Send("{TAB}");
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
            //else if (e.KeyCode == Keys.Insert)
            //{
            //    txtSifra_robe.Select();
            //}
        }

        //private void txtSifraPartnera_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        txtIzradio.Select();
        //    }
        //    else if (e.KeyCode == Keys.Insert)
        //    {
        //        txtSifra_robe.Select();
        //    }
        //}

        //private void txtIzradio_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        cbMjesto.Select();
        //    }
        //    else if (e.KeyCode == Keys.Insert)
        //    {
        //        txtSifra_robe.Select();
        //    }
        //}

        //private void txtMjestoTroska_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        txtOrginalniDok.Select();
        //    }
        //    else if (e.KeyCode == Keys.Insert)
        //    {
        //        txtSifra_robe.Select();
        //    }
        //}

        //private void txtOrginalniDok_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        rtbNapomena.Select();
        //    }
        //    else if (e.KeyCode == Keys.Insert)
        //    {
        //        txtSifra_robe.Select();
        //    }
        //}

        //private void rtbNapomena_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        txtSifra_robe.Select();
        //    }
        //    else if (e.KeyCode == Keys.Insert)
        //    {
        //        txtSifra_robe.Select();
        //    }
        //}

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
            else if (sender is RichTextBox)
            {
                RichTextBox control = ((RichTextBox)sender);
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
            else if (sender is RichTextBox)
            {
                RichTextBox control = ((RichTextBox)sender);
                control.BackColor = Color.White;
            }
        }

        private void KeyDownEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }

        #endregion ON_KEY_DOWN
    }
}