using PCPOS.Sifarnik;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmRobaUsluge : Form
    {
        private DataSet DSgrupa = new DataSet();
        private DataSet DSpodgrupa = new DataSet();
        private DataSet DSdrazava = new DataSet();
        private DataSet DSdrazava_uvoz = new DataSet();
        private DataSet DSmanufacturers = new DataSet();
        private DataSet DSPartneri = new DataSet();
        private DataSet DSporezi = new DataSet();
        private DataSet DSpp = new DataSet();

        //DataSet dsIsKasica = new DataSet();
        private Boolean NoviUnos = false;

        private List<string> EanCodes = new List<string>();
        private string id_roba;
        private bool uzetoSWeba = false;

        public frmRobaUsluge()
        {
            InitializeComponent();
        }

        public frmMenu MainFormMenu { get; set; }
        private bool ucitano = false;
        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private void frmRobaUsluge_Load(object sender, EventArgs e)
        {
            /****************************SINKRONIZACIJA SA WEB-OM*****************/
            bgSinkronizacija = new BackgroundWorker();
            bgSinkronizacija.DoWork += new DoWorkEventHandler(bgSinkronizacija_DoWork);
            bgSinkronizacija.WorkerSupportsCancellation = true;
            /****************************SINKRONIZACIJA SA WEB-OM*****************/

            //dsIsKasica = classSQL.select_settings("select * from aktivnost_podataka;", "aktivnost_podataka");

            IList<MyValue> values = new List<MyValue> { new MyValue { id = "DA", name = "Kod prodaje skidaj sa skladišta" }, new MyValue { id = "NE", name = "Ne skidaj sa skladišta" } };
            bcUzmiSaSkladista.DataSource = values;
            bcUzmiSaSkladista.DisplayMember = "name";
            bcUzmiSaSkladista.ValueMember = "id";
            this.Size = new Size(933, 400);
            if (Class.Dokumenti.robno != true) // OstaleFunkcije.DSaktivnosDok.Rows[0]["boolRobno"].ToString() != "1"
            {
                bcUzmiSaSkladista.Visible = false;
                bcUzmiSaSkladista.SelectedValue = "NE";
                label14.Visible = false;
            }
            ucitano = true;

            zabraniMijenjanjeCijena();
        }

        private void zabraniMijenjanjeCijena(bool b = false)
        {
            bool zabrani_mijenjanje_cijena = false;

            try
            {
                if (!Class.Dokumenti.isKasica && !uzetoSWeba)
                {
                    zabrani_mijenjanje_cijena = Class.Postavke.robaZabraniMijenjanjeCijena;

                    if (b)
                        zabrani_mijenjanje_cijena = false;

                    txtNabavna.Enabled = !zabrani_mijenjanje_cijena;
                    txtVeleprodajna.Enabled = !zabrani_mijenjanje_cijena;
                    txtMPC.Enabled = !zabrani_mijenjanje_cijena;
                    txtProizvodackaCijena.Enabled = !zabrani_mijenjanje_cijena;
                }
            }
            catch (Exception ex)
            {
            }
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            uzetoSWeba = false;
            enable(true, true);
            EanCodes.Clear();

            string count = classSQL.select("SELECT count(*) FROM roba", "roba").Tables[0].Rows[0][0].ToString();

            while (classSQL.select(string.Format("SELECT count(*) FROM roba WHERE sifra = '{0}';", count), "roba").Tables[0].Rows[0][0].ToString() != "0")
            {
                count = Convert.ToString(Convert.ToInt16(count) + 1);
            }

            txtSifra.Text = count;
            NoviUnos = true;
            txtNaziv.Select();
        }

        private void enable(bool x, bool dozvolieditiranjecijena = false)
        {
            txtSifraDob.Enabled = x;
            txtMPC.Enabled = x;
            txtNabavna.Enabled = x;
            txtProizvodackaCijena.Enabled = x;
            txtNaziv.Enabled = x;
            txtSifra.Enabled = x;
            txtVeleprodajna.Enabled = x;
            cbGrupa.Enabled = x;
            txtJedMj.Enabled = x;
            cbProizvodac.Enabled = x;
            cbZemljaPodrijetla.Enabled = x;
            cbZemljaUvoza.Enabled = x;
            bcUzmiSaSkladista.Enabled = x;
            txtPDV.Enabled = x;
            txtPovrNaknada.Enabled = x;
            btnKaucija.Enabled = x;
            cbpodgrupa.Enabled = x;
            button2.Enabled = x;
            btnBarcode.Enabled = x;

            if (x)
            {
                txtMPC.Text = "0,00";
                txtNabavna.Text = "0,00";
                txtProizvodackaCijena.Text = "0,00";
                txtNaziv.Text = "";
                txtSifra.Text = "";
                txtNazivDob.Text = "";
                txtSifraDob.Text = "";
                txtVeleprodajna.Text = "0,00";
                txtPovrNaknada.Text = "0,00";
                rtbxOpisProizvoda.Text = "";
                txtJamstvo.Text = "0";
                cbAkcija.Checked = false;
                txtlink_na_sliku.Text = "";

                popuniCB();
            }

            if (!x)
            {
                txtNazivDob.Text = "";
                txtSifraDob.Text = "";
                txtMPC.Text = "";
                txtNabavna.Text = "";
                txtProizvodackaCijena.Text = "";
                txtNaziv.Text = "";
                txtSifra.Text = "";
                txtVeleprodajna.Text = "";
                txtPovrNaknada.Text = "";
                rtbxOpisProizvoda.Text = "";
                txtJamstvo.Text = "";
                cbAkcija.Checked = false;
                txtlink_na_sliku.Text = "";
            }

            zabraniMijenjanjeCijena(dozvolieditiranjecijena);
        }

        public class MyValue
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        private void popuniCB()
        {
            //CB grupe
            DSgrupa = classSQL.select("SELECT * FROM grupa ORDER BY grupa", "grupa");
            cbGrupa.DataSource = DSgrupa.Tables[0];
            cbGrupa.DisplayMember = "grupa";
            cbGrupa.ValueMember = "id_grupa";

            //CB podgrupe
            DSpodgrupa.Clear();
            DSpodgrupa = classSQL.select(string.Format("SELECT * FROM podgrupa WHERE id_grupa = '{0}' ORDER BY naziv;", cbGrupa.SelectedValue), "podgrupa");
            cbpodgrupa.DataSource = DSpodgrupa.Tables[0];
            cbpodgrupa.DisplayMember = "naziv";
            cbpodgrupa.ValueMember = "id_podgrupa";

            //CB dražave
            DSdrazava = classSQL.select("SELECT * FROM zemlja ORDER BY zemlja", "zemlja");
            cbZemljaPodrijetla.DataSource = DSdrazava.Tables[0];
            cbZemljaPodrijetla.DisplayMember = "zemlja";
            cbZemljaPodrijetla.ValueMember = "id_zemlja";
            cbZemljaPodrijetla.SelectedValue = "60";

            //CB dražave_uvoz
            DSdrazava_uvoz = classSQL.select("SELECT * FROM zemlja ORDER BY zemlja", "zemlja");
            cbZemljaUvoza.DataSource = DSdrazava_uvoz.Tables[0];
            cbZemljaUvoza.DisplayMember = "zemlja";
            cbZemljaUvoza.ValueMember = "id_zemlja";
            cbZemljaUvoza.SelectedValue = "60";

            //CB proizvođač
            DSmanufacturers = classSQL.select("SELECT * FROM manufacturers ORDER BY manufacturers", "manufacturers");
            cbProizvodac.DataSource = DSmanufacturers.Tables[0];
            cbProizvodac.DisplayMember = "manufacturers";
            cbProizvodac.ValueMember = "id_manufacturers";

            //DS porez
            DSporezi = classSQL.select("select naziv, replace(iznos, ',','.')::numeric(7,2) as iznos from porezi ORDER BY id_porez ASC", "porezi");
            txtPDV.DataSource = DSporezi.Tables[0];
            txtPDV.DisplayMember = "naziv";
            txtPDV.ValueMember = "iznos";
        }

        private void btnDobavljac_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi djelatnost_trazi = new frmPartnerTrazi();
            djelatnost_trazi.ShowDialog();

            //dobiva odabranog partnera
            if (Properties.Settings.Default.id_partner != "")
            {
                Fill(Properties.Settings.Default.id_partner);
                //NoviUnos = false;
            }
        }

        private void Fill(string id)
        {
            try
            {
                if (id != "")
                {
                    txtSifraDob.Text = id;
                    DataTable DT = classSQL.select(string.Format("SELECT ime_tvrtke FROM partners WHERE id_partner = '{0}';", id), "partners").Tables[0];

                    if (DT.Rows.Count > 0)
                    {
                        txtNazivDob.Text = DT.Rows[0][0].ToString();
                    }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Greška.\r\n" + x);
            }
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            frmRobaTrazi roba = new frmRobaTrazi();
            roba.ShowDialog();

            if (txtNaziv.Enabled == false)
            {
                enable(true);

                //txtSifra.Enabled = false;
            }

            if (Properties.Settings.Default.id_roba != "")
            {
                Fill_Roba(Properties.Settings.Default.id_roba);
            }
        }

        private void Fill_Roba(string id)
        {
            if (id != "")
            {
                DataSet dsRobaProdaja = null;
                DataTable tbRoba = classSQL.select(string.Format("SELECT * FROM roba WHERE sifra = '{0}';", id), "roba").Tables[0];

                if (!Class.Dokumenti.isKasica)
                {
                    dsRobaProdaja = classSQL.select(string.Format("select * from roba_prodaja where sifra = '{0}' and id_skladiste = '{1}';", id, Class.Postavke.id_default_skladiste), "roba_prodaja");
                }

                if (tbRoba.Rows.Count > 0)
                {
                    enable(true);
                    txtSifra.Enabled = false;

                    decimal kol = 0;
                    bool k = false;
                    bool roba = (tbRoba.Rows[0]["oduzmi"].ToString() == "DA" ? true : false);
                    if (!Class.Dokumenti.isKasica)
                    {
                        if (dsRobaProdaja != null && dsRobaProdaja.Tables.Count > 0 && dsRobaProdaja.Tables[0] != null && dsRobaProdaja.Tables[0].Rows.Count > 0)
                        {
                            k = decimal.TryParse(dsRobaProdaja.Tables[0].Rows[0]["kolicina"].ToString(), out kol);
                        }
                    }

                    if (Class.Postavke.robaZabraniMijenjanjeCijena && dsRobaProdaja != null && dsRobaProdaja.Tables.Count > 0 && dsRobaProdaja.Tables[0] != null && dsRobaProdaja.Tables[0].Rows.Count > 0 && k && kol != 0 && roba)
                    {
                        txtProizvodackaCijena.Enabled = false;
                        txtNabavna.Enabled = false;
                        txtVeleprodajna.Enabled = false;
                        txtMPC.Enabled = false;
                    }
                    else
                    {
                        txtProizvodackaCijena.Enabled = true;
                        txtNabavna.Enabled = true;
                        txtVeleprodajna.Enabled = true;
                        txtMPC.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Kriva šifra.");
                    return;
                }

                id_roba = tbRoba.Rows[0]["id_roba"].ToString();
                DataTable DT = classSQL.select(string.Format("SELECT ime_tvrtke FROM partners WHERE id_partner = '{0}';", tbRoba.Rows[0]["id_partner"].ToString()), "partners").Tables[0];

                if (DT.Rows.Count > 0)
                {
                    txtNazivDob.Text = DT.Rows[0][0].ToString();
                    txtSifraDob.Text = tbRoba.Rows[0]["id_partner"].ToString();
                }
                ReadEanCodes(tbRoba.Rows[0]["ean"].ToString());
                txtMPC.Text = Convert.ToDouble(tbRoba.Rows[0]["mpc"].ToString()).ToString("#0.00");
                txtNabavna.Text = Convert.ToDouble(tbRoba.Rows[0]["nc"].ToString()).ToString("#0.00");
                txtProizvodackaCijena.Text = Convert.ToDouble(tbRoba.Rows[0]["proizvodacka_cijena"].ToString()).ToString("#0.00");
                txtNaziv.Text = tbRoba.Rows[0]["naziv"].ToString();
                txtSifra.Text = tbRoba.Rows[0]["sifra"].ToString().Trim();
                txtVeleprodajna.Text = Convert.ToDouble(tbRoba.Rows[0]["vpc"].ToString()).ToString("#0.000");
                cbGrupa.SelectedValue = tbRoba.Rows[0]["id_grupa"].ToString();

                try
                {
                    cbpodgrupa.SelectedValue = tbRoba.Rows[0]["id_podgrupa"].ToString();
                }
                catch
                {
                }

                txtJedMj.Text = tbRoba.Rows[0]["jm"].ToString();
                cbProizvodac.SelectedValue = tbRoba.Rows[0]["id_manufacturers"].ToString();
                cbZemljaPodrijetla.SelectedValue = tbRoba.Rows[0]["id_zemlja_porijekla"].ToString();
                cbZemljaUvoza.SelectedValue = tbRoba.Rows[0]["id_zemlja_uvoza"].ToString();
                txtPDV.SelectedValue = tbRoba.Rows[0]["porez"].ToString();
                bcUzmiSaSkladista.SelectedValue = tbRoba.Rows[0]["oduzmi"].ToString();
                rtbxOpisProizvoda.Text = tbRoba.Rows[0]["opis"].ToString();
                txtJamstvo.Text = tbRoba.Rows[0]["jamstvo"].ToString();
                txtlink_na_sliku.Text = tbRoba.Rows[0]["link_za_slike"].ToString();
                if (tbRoba.Rows[0]["akcija"].ToString() == "1") { cbAkcija.Checked = true; } else { cbAkcija.Checked = false; }

                DataTable DTPovrNaknd = classSQL.select(string.Format("SELECT iznos FROM povratna_naknada WHERE sifra = '{0}';", tbRoba.Rows[0]["sifra"].ToString().Trim()), "povratna_naknada").Tables[0];

                txtPovrNaknada.Text = DTPovrNaknd.Rows.Count > 0 ? DTPovrNaknd.Rows[0][0].ToString() : "0,00";

                NoviUnos = false;
                try
                {
                    if (txtlink_na_sliku.Text != "")
                    {
                        pictureBox1.Image = new Bitmap(new System.IO.MemoryStream(new System.Net.WebClient().DownloadData(txtlink_na_sliku.Text)));
                    }
                }
                catch { }
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            if (DTpostavke.Rows[0]["sustav_pdv"].ToString() != "1")
            {
                if (MessageBox.Show("Vaša tvrtka nije u sustavu pdv-a a vi ste odabrali u stavci pdv od: " + txtPDV.SelectedValue + "%.\r\n" +
                    " Ako želite spremiti sa pogrešnim pdv-om pritisnite 'YES' a ako želite ispraviti stavku pritisnite na 'NO'", "Bitno upozorenje.",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Error) != DialogResult.Yes)
                {
                    return;
                }
            }

            txtNaziv.Text = txtNaziv.Text.Trim().Replace("+", "");

            if (NoviUnos == true)
            {
                spremi();
            }
            else
            {
                update();
            }

            //if (bcUzmiSaSkladista.SelectedValue.ToString() == "NE")
            {
                try
                {
                    string sql = string.Format("SELECT postavi_kolicinu_sql_funkcija_prema_sifri('{0}') AS odgovor;", txtSifra.Text);
                    classSQL.update(sql);
                }
                catch { }
            }

            uzetoSWeba = false;
            txtSifra.Enabled = true;
        }

        private void update()
        {
            if (txtSifra.Text == "") { MessageBox.Show("Niste pravilno upisali šifru."); return; }
            if (txtNaziv.Text == "") { MessageBox.Show("Niste pravilno upisali ime artikla/usluge."); return; }
            if (txtJedMj.Text == "") { MessageBox.Show("Niste pravilno upisali jedinicu mjere."); return; }
            if (txtVeleprodajna.Text == "") { MessageBox.Show("Niste pravilno upisali veleprodajnu cijenu."); return; }
            if (txtMPC.Text == "") { MessageBox.Show("Niste pravilno upisali maloprodajnu cijenu."); return; }
            if (txtSifraDob.Text == "") { MessageBox.Show("Niste pravilno upisali šifru dobavljača."); return; }
            if (txtPDV.Text == "") { MessageBox.Show("Niste pravilno upisali porez."); return; }
            if (txtProizvodackaCijena.Text == "") { MessageBox.Show("Niste pravilno upisali proizvođačku cijenu."); return; }

            string vpc = txtVeleprodajna.Text;
            string mpc = txtMPC.Text;
            string nc = txtNabavna.Text;
            string proizvodackaCijena = txtProizvodackaCijena.Text;

            if (classSQL.remoteConnectionString == "")
            {
                vpc = vpc.Replace(",", ".");
                mpc = mpc.Replace(",", ".");
                nc = nc.Replace(",", ".");
                proizvodackaCijena = proizvodackaCijena.Replace(",", ".");
            }
            else
            {
                vpc = vpc.Replace(",", ".");
                mpc = mpc.Replace(".", ",");
                nc = nc.Replace(".", ",");
                proizvodackaCijena = proizvodackaCijena.Replace(",", ".");
            }

            txtNaziv.Text = txtNaziv.Text.Replace("+", "");
            txtNaziv.Text = txtNaziv.Text.Replace("\\", "");
            txtNaziv.Text = txtNaziv.Text.Replace("'", "");
            txtNaziv.Text = txtNaziv.Text.Replace("\"", "");

            txtSifra.Text = txtSifra.Text.Replace("+", "");
            txtSifra.Text = txtSifra.Text.Replace("\\", "");
            txtSifra.Text = txtSifra.Text.Replace("'", "");
            txtSifra.Text = txtSifra.Text.Replace("\"", "");

            rtbxOpisProizvoda.Text = rtbxOpisProizvoda.Text.Replace("+", "");
            rtbxOpisProizvoda.Text = rtbxOpisProizvoda.Text.Replace("\\", "");
            rtbxOpisProizvoda.Text = rtbxOpisProizvoda.Text.Replace("'", "");
            rtbxOpisProizvoda.Text = rtbxOpisProizvoda.Text.Replace("\"", "");

            if (Class.Dokumenti.robno != true) // OstaleFunkcije.DSaktivnosDok.Rows[0]["boolRobno"].ToString() != "1"
            {
                bcUzmiSaSkladista.Visible = false;
                bcUzmiSaSkladista.SelectedValue = "NE";
                label14.Visible = false;
            }

            string sql = string.Format(@"SELECT id_roba_prodaja, id_skladiste
FROM roba_prodaja
LEFT JOIN roba ON roba_prodaja.sifra = roba.sifra
WHERE roba.id_roba = '{0}';", id_roba);
            DataTable sifreRobaProdajaUpdate = classSQL.select(sql, "id_roba_prodaja").Tables[0];
            sql = string.Format(@"SELECT id
FROM povratna_naknada
LEFT JOIN roba ON povratna_naknada.sifra = roba.sifra
WHERE roba.id_roba = '{0}';", id_roba);
            DataTable sifrePovratnaNaknadaUpdate = classSQL.select(sql, "povratna_naknada").Tables[0];
            int akcija = 0;

            if (cbAkcija.Checked) { akcija = 1; } else { akcija = 0; }
            int podgrp = Convert.ToInt32(cbpodgrupa.SelectedValue);
            if (podgrp == null)
            {
                podgrp = 1;
            }

            int jamstvo;
            if (!int.TryParse(txtJamstvo.Text, out jamstvo))
            {
                jamstvo = 0;
            }

            sql = string.Format(@"UPDATE roba
SET
    naziv = '{0}',
    id_grupa = '{1}',
    id_podgrupa = '{2}',
    jm = '{3}',
    nc = '{4}',
    proizvodacka_cijena = '{5}',
    vpc = '{6}',
    ean = '{7}',
    mpc = '{8}',
    porez = '{9}',
    id_zemlja_porijekla = '{10}',
    id_zemlja_uvoza = '{11}',
    id_partner = '{12}',
    id_manufacturers = '{13}',
    opis = '{14}',
    jamstvo = '{15}',
    akcija = '{16}',
    editirano = '1',
    oduzmi = '{17}',
    link_za_slike = '{18}',
    sifra = '{19}'
WHERE id_roba = '{20}';",
    txtNaziv.Text,
    cbGrupa.SelectedValue,
    podgrp,
    txtJedMj.Text,
    nc,
    proizvodackaCijena,
    vpc,
    GetEanString(),
    mpc,
    txtPDV.SelectedValue,
    cbZemljaPodrijetla.SelectedValue,
    cbZemljaUvoza.SelectedValue,
    txtSifraDob.Text,
    cbProizvodac.SelectedValue,
    rtbxOpisProizvoda.Text,
    jamstvo.ToString(),
    akcija,
    bcUzmiSaSkladista.SelectedValue,
    txtlink_na_sliku.Text,
    ispravi(txtSifra.Text.Trim()),
    id_roba);

            provjera_sql(classSQL.update(sql));

            if (Class.PodaciTvrtka.oibTvrtke != Class.Postavke.OIB_PC1)
            {
                if (Class.Postavke.koristiSkladista.Length > 0)
                {
                    string[] ss = Class.Postavke.koristiSkladista.Split(',');
                    for (int i = 0; i < sifreRobaProdajaUpdate.Rows.Count; i++)
                    {
                        if (Array.IndexOf(ss, sifreRobaProdajaUpdate.Rows[i]["id_skladiste"].ToString()) >= 0)
                        {
                            sql = string.Format(@"UPDATE roba_prodaja
SET
    porez = '{0}',
    vpc = '{1}',
    editirano = '1',
    nc = '{2}',
    proizvodacka_cijena = {3}
WHERE sifra = '{4}' and id_skladiste = {5};",
txtPDV.SelectedValue,
vpc,
nc,
txtProizvodackaCijena.Text.Replace(',', '.'),
ispravi(txtSifra.Text.Trim()),
sifreRobaProdajaUpdate.Rows[i]["id_skladiste"].ToString());

                            provjera_sql(classSQL.update(sql));
                        }
                    }
                }
                else if (MessageBox.Show("Želite li ovu cijenu i poreze postaviti na sva skladišta?", "Promjena cijene", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    for (int i = 0; i < sifreRobaProdajaUpdate.Rows.Count; i++)
                    {
                        sql = string.Format(@"UPDATE roba_prodaja
SET
    porez = '{0}',
    vpc = '{1}',
    editirano = '1',
    nc = '{2}',
    proizvodacka_cijena = {3}
WHERE id_roba_prodaja = '{4}';",
    txtPDV.SelectedValue,
    vpc,
    nc,
    txtProizvodackaCijena.Text.Replace(',', '.'),
    sifreRobaProdajaUpdate.Rows[i]["id_roba_prodaja"].ToString());

                        provjera_sql(classSQL.update(sql));
                    }
                }
            }
            else
            {
                sql = string.Format(@"UPDATE roba_prodaja
SET
    porez = '{0}',
    vpc = '{1}',
    editirano = '1',
    nc = '{2}',
    proizvodacka_cijena = {3}
WHERE sifra = '{4}' AND id_skladiste = '{5}';",
txtPDV.SelectedValue,
vpc,
nc,
txtProizvodackaCijena.Text.Replace(',', '.'),
ispravi(txtSifra.Text.Trim()),
Class.Postavke.id_default_skladiste.ToString());

                provjera_sql(classSQL.update(sql));
            }

            string povrNaknd = txtPovrNaknada.Text;
            povrNaknd = classSQL.remoteConnectionString == "" ? povrNaknd.Replace(",", ".") : povrNaknd.Replace(",", ".");
            if (sifrePovratnaNaknadaUpdate.Rows.Count > 0)
            {
                sql = string.Format(@"UPDATE povratna_naknada
SET
    sifra = '{0}',
    iznos = '{1}'
WHERE id = '{2}';",
ispravi(txtSifra.Text.Trim()),
povrNaknd,
sifrePovratnaNaknadaUpdate.Rows[0]["id"].ToString());

                provjera_sql(classSQL.update(sql));

                if (sifrePovratnaNaknadaUpdate.Rows.Count > 1)
                {
                    for (int i = 1; i == sifrePovratnaNaknadaUpdate.Rows.Count - 1; i++)
                    {
                        sql = string.Format(@"DELETE FROM povratna_naknada
WHERE id = '{0}';",
sifrePovratnaNaknadaUpdate.Rows[i]["id"].ToString());
                        provjera_sql(classSQL.update(sql));
                    }
                }
            }
            else
            {
                sql = string.Format(@"INSERT INTO povratna_naknada
(
    sifra, iznos
)
VALUES
(
    '{0}',
    '{1}'
);",
ispravi(txtSifra.Text.Trim()),
povrNaknd);

                provjera_sql(classSQL.insert(sql));
            }

            txtSifra.Enabled = false;
            enable(false);
        }

        private void spremi()
        {
            /*if (txtEan.Text == "")
            {
                txtEan.Text = "-1";
            }*/

            if (txtNaziv.Enabled == false)
            {
                return;
            }

            if (txtSifra.Text.Length > 2)
            {
                if (txtSifra.Text.Substring(0, 3) == "000")
                {
                    MessageBox.Show("Početak šifra ne smije sadržavati više od dvije nule.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSifra.Text = "";
                    return;
                }
            }

            string oduzimaj = "DA";
            if (Class.Dokumenti.robno != true) // OstaleFunkcije.DSaktivnosDok.Rows[0]["boolRobno"].ToString() != "1"
            {
                oduzimaj = "NE";
            }
            else
            {
                oduzimaj = bcUzmiSaSkladista.SelectedValue.ToString();
            }

            string number = classSQL.select(string.Format("SELECT count(*) FROM roba WHERE sifra = '{0}';", txtSifra.Text), "roba").Tables[0].Rows[0][0].ToString();

            if (number != "0") { MessageBox.Show("Greška.\r\nUpisana šifra već postoji."); return; }
            if (txtSifra.Text == "") { MessageBox.Show("Niste pravilno upisali šifru."); return; }
            if (txtNaziv.Text == "") { MessageBox.Show("Niste pravilno upisali ime artikla/usluge."); return; }
            if (txtJedMj.Text == "") { MessageBox.Show("Niste pravilno upisali jedinicu mjere."); return; }
            if (txtVeleprodajna.Text == "") { MessageBox.Show("Niste pravilno upisali veleprodajnu cijenu."); return; }
            if (txtMPC.Text == "") { MessageBox.Show("Niste pravilno upisali maloprodajnu cijenu."); return; }
            if (txtSifraDob.Text == "") { MessageBox.Show("Niste pravilno upisali šifru dobavljača."); return; }
            if (txtPDV.Text == "") { MessageBox.Show("Niste pravilno upisali porez."); return; }
            if (txtProizvodackaCijena.Text == "") { MessageBox.Show("Niste pravilno upisali proizvođačku cijenu."); return; }

            string vpc = txtVeleprodajna.Text;
            string mpc = txtMPC.Text;
            string nc = txtNabavna.Text;
            string proizvodackaCijena = txtProizvodackaCijena.Text;

            if (classSQL.remoteConnectionString == "")
            {
                vpc = vpc.Replace(",", ".");
                mpc = mpc.Replace(",", ".");
                nc = nc.Replace(",", ".");
                proizvodackaCijena = proizvodackaCijena.Replace(",", ".");
            }
            else
            {
                vpc = vpc.Replace(",", ".");
                mpc = mpc.Replace(".", ",");
                nc = nc.Replace(".", ",");
                proizvodackaCijena = proizvodackaCijena.Replace(",", ".");
            }

            txtNaziv.Text = txtNaziv.Text.Replace("+", "");
            txtNaziv.Text = txtNaziv.Text.Replace("\\", "");
            txtNaziv.Text = txtNaziv.Text.Replace("'", "");
            txtNaziv.Text = txtNaziv.Text.Replace("\"", "");

            txtSifra.Text = txtSifra.Text.Replace("+", "");
            txtSifra.Text = txtSifra.Text.Replace("\\", "");
            txtSifra.Text = txtSifra.Text.Replace("'", "");
            txtSifra.Text = txtSifra.Text.Replace("\"", "");

            rtbxOpisProizvoda.Text = rtbxOpisProizvoda.Text.Replace("+", "");
            rtbxOpisProizvoda.Text = rtbxOpisProizvoda.Text.Replace("\\", "");
            rtbxOpisProizvoda.Text = rtbxOpisProizvoda.Text.Replace("'", "");
            rtbxOpisProizvoda.Text = rtbxOpisProizvoda.Text.Replace("\"", "");

            int akcija = 0;

            if (cbAkcija.Checked) { akcija = 1; } else { akcija = 0; }

            int podgrp = Convert.ToInt32(cbpodgrupa.SelectedValue);
            if (podgrp == null)
            {
                podgrp = 1;
            }

            string sql = "";
            sql = string.Format(@"INSERT INTO roba
(
    naziv, sifra, ean, id_grupa, id_podgrupa, nc, vpc, mpc, id_zemlja_porijekla, id_zemlja_uvoza,
    id_partner, id_manufacturers, porez, oduzmi, jm, opis, jamstvo, akcija, link_za_slike, novo
)
VALUES
(
    '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}',
    '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '1');",
            txtNaziv.Text,
            ispravi(txtSifra.Text.Trim()),
            GetEanString(),
            cbGrupa.SelectedValue,
            podgrp,
            nc,
            vpc,
            mpc,
            cbZemljaPodrijetla.SelectedValue,
            cbZemljaUvoza.SelectedValue,
            txtSifraDob.Text,
            cbProizvodac.SelectedValue,
            txtPDV.SelectedValue,
            bcUzmiSaSkladista.SelectedValue,
            txtJedMj.Text,
            rtbxOpisProizvoda.Text,
            txtJamstvo.Text,
            akcija,
            txtlink_na_sliku.Text);

            provjera_sql(classSQL.insert(sql));

            string povrNaknd = txtPovrNaknada.Text;

            povrNaknd = classSQL.remoteConnectionString == "" ? povrNaknd.Replace(",", ".") : povrNaknd.Replace(",", ".");

            sql = string.Format(@"SELECT id
FROM povratna_naknada
LEFT JOIN roba ON povratna_naknada.sifra = roba.sifra
WHERE roba.sifra = '{0}';", ispravi(txtSifra.Text.Trim()));
            DataTable sifrePovratnaNaknadaUpdate = classSQL.select(sql, "povratna_naknada").Tables[0];

            if (sifrePovratnaNaknadaUpdate.Rows.Count > 0)
            {
                sql = string.Format(@"UPDATE povratna_naknada
SET
    sifra = '{0}',
    iznos = '{1}'
WHERE id = '{2}';",
ispravi(txtSifra.Text.Trim()),
povrNaknd,
sifrePovratnaNaknadaUpdate.Rows[0]["id"].ToString());

                provjera_sql(classSQL.update(sql));

                if (sifrePovratnaNaknadaUpdate.Rows.Count > 1)
                {
                    for (int i = 1; i == sifrePovratnaNaknadaUpdate.Rows.Count - 1; i++)
                    {
                        sql = string.Format(@"DELETE FROM povratna_naknada
WHERE id = '{0}';",
sifrePovratnaNaknadaUpdate.Rows[i]["id"].ToString());
                        provjera_sql(classSQL.update(sql));
                    }
                }
            }
            else
            {
                sql = string.Format(@"INSERT INTO povratna_naknada
(
    sifra, iznos
)
VALUES
(
    '{0}',
    '{1}'
);",
ispravi(txtSifra.Text.Trim()),
povrNaknd);

                provjera_sql(classSQL.insert(sql));
            }

            DataTable DT_skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste").Tables[0];

            for (int i = 0; i < DT_skladiste.Rows.Count; i++)
            {
                sql = string.Format("select id_roba_prodaja from roba_prodaja where id_skladiste = {0} and sifra = '{1}';", DT_skladiste.Rows[i]["id_skladiste"].ToString(), ispravi(txtSifra.Text.Trim()));

                DataSet dsRobaProdaja = classSQL.select(sql, "roba_prodaja");

                if (dsRobaProdaja != null && dsRobaProdaja.Tables.Count > 0 && dsRobaProdaja.Tables[0] != null && dsRobaProdaja.Tables[0].Rows.Count > 0)
                {
                    sql = string.Format(@"update roba_prodaja
set
    nc = '{0}',
    vpc = '{1}',
    porez = '{2}',
    editirano = '1'
where id_roba_prodaja = {3};", nc,
        vpc,
        txtPDV.SelectedValue, dsRobaProdaja.Tables[0].Rows[0]["id_roba_prodaja"]);
                    provjera_sql(classSQL.update(sql));
                }
                else
                {
                    sql = string.Format(@"INSERT INTO roba_prodaja
(
    id_skladiste, kolicina, nc, vpc, porez, sifra, novo
)
VALUES
(
    '{0}', '0', '{1}', '{2}', '{3}', '{4}', '1'
);",
        DT_skladiste.Rows[i]["id_skladiste"].ToString(),
        nc,
        vpc,
        txtPDV.SelectedValue,
        ispravi(txtSifra.Text.Trim()));

                    provjera_sql(classSQL.insert(sql));
                }
            }

            NoviUnos = false;
            enable(false);
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void txtSifra_TextChanged(object sender, EventArgs e)
        {
            if (txtSifra.Text != "")
            {
                if (txtSifra.Text.Length > 2)
                {
                    if (txtSifra.Text.Substring(0, 3) == "000")
                    {
                        MessageBox.Show("Početak šifre ne smije sadržavati više od dvije nule.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSifra.Text = "";
                        return;
                    }
                }

                string count = classSQL.select(string.Format("SELECT count(*) FROM roba WHERE sifra = '{0}';", txtSifra.Text), "roba").Tables[0].Rows[0][0].ToString();

                if (count == "0")
                {
                    txtSifra.BackColor = Color.Azure;
                }
                else
                {
                    txtSifra.BackColor = Color.MistyRose;
                }
            }
            else
            {
                txtSifra.BackColor = Color.MistyRose;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            enable(false);
            txtSifra.Enabled = true;
        }

        private void txtNaziv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbGrupa.Select();
            }
        }

        private void cbGrupa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtJedMj.Select();
            }
        }

        private void txtJedMj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtSifraDob.Select();
            }
        }

        private void cbDobavljac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtSifraDob.Text == "")
                {
                    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                    partnerTrazi.ShowDialog();
                    if (Properties.Settings.Default.id_partner != "")
                    {
                        DataSet partner = new DataSet();
                        partner = classSQL.select(string.Format("SELECT id_partner, ime_tvrtke FROM partners WHERE id_partner = '{0}';", Properties.Settings.Default.id_partner), "partners");
                        if (partner.Tables[0].Rows.Count > 0)
                        {
                            txtSifraDob.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                            bcUzmiSaSkladista.Select();
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtSifraDob.Select();
                        }
                    }
                    else
                    {
                        txtSifraDob.Select();
                        return;
                    }
                }

                DataTable DT = classSQL.select(string.Format("SELECT ime_tvrtke FROM partners WHERE id_partner = '{0}';", txtSifraDob.Text), "partners").Tables[0];

                if (DT.Rows.Count > 0)
                {
                    txtNazivDob.Text = DT.Rows[0][0].ToString();
                    bcUzmiSaSkladista.Select();
                }
                else
                {
                    MessageBox.Show("Krivi unos.", "Greška");
                    txtSifraDob.Text = "";
                }
            }
        }

        private void bcUzmiSaSkladista_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtProizvodackaCijena.Select();
            }
        }

        private void txtnabavna_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtVeleprodajna.Select();
            }
        }

        private void txtVeleprodajna_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtPDV.Select();
            }
        }

        private void txtPDV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                double porez = Convert.ToDouble(txtPDV.SelectedValue);
                txtMPC.Text = Convert.ToDouble((Convert.ToDouble(txtVeleprodajna.Text) * porez / 100) + Convert.ToDouble(txtVeleprodajna.Text)).ToString("#0.00");
                txtMPC.Select();
            }
        }

        private void txtMPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbProizvodac.Select();
            }
        }

        private void cbProizvodac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbZemljaPodrijetla.Select();
            }
        }

        private void cbZemljaPodrijetla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cbZemljaUvoza.Select();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private DataTable DTpodaci_tvrtka = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];
        private pc1.konektor WEBbaza = new pc1.konektor();

        private void txtSifra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                uzetoSWeba = false;
                e.SuppressKeyPress = true;

                if (DTpodaci_tvrtka.Rows[0]["oib"].ToString() == Class.Postavke.OIB_PC1)
                {
                    if (txtSifra.BackColor == Color.Azure)
                    {
                        string[] s = null;
                        try
                        {
                            using (wsSoftKontrol.wsSoftKontrol ws = new wsSoftKontrol.wsSoftKontrol())
                            {
                                s = ws.getArtiklFromWeb(txtSifra.Text);
                            }
                        }
                        catch
                        {
                        }

                        if (s != null && s.Length > 0 && s[0] != null && s[1] != null && s[2] != null && s[3] != null)
                        {
                            if (MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga u programu.\r\n" +
                                "Isti artikl postoji na web stranici, želite li skinuti artikl sa weba-a?", "Obavijest",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                uzetoSWeba = true;
                                Fill_RobaWeb(s);
                                txtNaziv.Select();
                            }
                        }
                        else
                        {
                            if (MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga u šifrarniku niti na web-u.\r\nŽelite li dodatu novu šifru?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                string sif = txtSifra.Text;
                                enable(true, true);
                                txtNaziv.Select();
                                txtSifra.Text = sif;
                                NoviUnos = true;
                                return;
                            }
                        }
                        return;
                    }

                    Fill_Roba(txtSifra.Text);
                    txtNaziv.Select();
                }
                else
                {
                    if (txtSifra.BackColor == Color.Azure)
                    {
                        if (MessageBox.Show("Upisana šifra nije u sustavu. \r\nŽelite li dodatu novu šifru?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string sif = txtSifra.Text;
                            enable(true, true);
                            txtNaziv.Select();
                            txtSifra.Text = sif;
                            NoviUnos = true;
                            return;
                        }
                    }

                    Fill_Roba(txtSifra.Text);
                    txtNaziv.Select();
                }
            }
        }

        private void Fill_RobaWeb(DataTable DTweb)
        {
            decimal porezPDV;
            decimal.TryParse(Properties.Settings.Default.PDV, out porezPDV);
            decimal mpc;
            decimal.TryParse(DTweb.Rows[0]["Price"].ToString(), out mpc);
            decimal vpc = mpc / ((porezPDV / 100) + 1);
            enable(true);

            txtMPC.Text = Math.Round(mpc, 3).ToString();
            txtNabavna.Text = "0,00";
            txtNaziv.Text = DTweb.Rows[0]["Productname"].ToString();
            txtSifra.Text = DTweb.Rows[0]["ProductID"].ToString();
            txtVeleprodajna.Text = Math.Round(vpc, 3).ToString();
            txtJedMj.Text = "kom";
            txtPDV.SelectedValue = porezPDV.ToString();
            bcUzmiSaSkladista.SelectedValue = "DA";
            rtbxOpisProizvoda.Text = DTweb.Rows[0]["ProductText"].ToString();
            txtJamstvo.Text = "0";
            txtlink_na_sliku.Text = "";
            txtPovrNaknada.Text = "0,00";

            NoviUnos = true;
        }

        private void Fill_RobaWeb(string[] DTweb)
        {
            decimal porezPDV;
            decimal.TryParse(Properties.Settings.Default.PDV, out porezPDV);
            decimal mpc = 0;
            decimal.TryParse(DTweb[3].ToString().Replace(".", ","), out mpc);
            mpc = mpc / Convert.ToDecimal((1 - (5f / 100f)));
            decimal vpc = Math.Round(mpc, 2, MidpointRounding.AwayFromZero) / ((porezPDV / 100) + 1);
            enable(true);

            txtMPC.Text = Math.Round(mpc, 2, MidpointRounding.AwayFromZero).ToString();
            txtNabavna.Text = "0,00";
            txtNaziv.Text = DTweb[1].ToString();
            txtSifra.Text = DTweb[0].ToString();
            txtVeleprodajna.Text = Math.Round(vpc, 3).ToString();
            txtJedMj.Text = "kom";
            txtPDV.SelectedValue = porezPDV.ToString();
            bcUzmiSaSkladista.SelectedValue = "DA";
            rtbxOpisProizvoda.Text = DTweb[2].ToString();
            txtJamstvo.Text = "0";
            txtlink_na_sliku.Text = "";
            txtPovrNaknada.Text = "0,00";
            //txtMPC_
            NoviUnos = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        private void ReadEanCodes(string data)
        {
            EanCodes.Clear();
            string[] array = data.Split(';');
            foreach (string code in array)
                EanCodes.Add(code);

            if (EanCodes.Count == 1 && EanCodes[0].ToString() == "-1")
                EanCodes.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        private string GetEanString()
        {
            string result = "";
            if (EanCodes.Count == 0)
                result = "-1";
            else
            {
                for(int i = 0; i < EanCodes.Count; i++)
                {
                    result += EanCodes[i];
                    if (i + 1 != EanCodes.Count)
                        result += ";";
                }
            }
            return result;
        }

        private void txtnabavna_Leave(object sender, EventArgs e)
        {
            try
            {
                txtNabavna.Text = Convert.ToDouble(txtNabavna.Text).ToString("#0.00");
            }
            catch (Exception)
            {
                txtNabavna.Text = "0.00";
            }
        }

        private void txtMPC_Leave_1(object sender, EventArgs e)
        {
            try
            {
                double mpc = 0, pov_nak = 0;
                if (double.TryParse(txtMPC.Text, out mpc) && double.TryParse(txtPovrNaknada.Text, out pov_nak))
                {
                    if (!Class.Postavke.koristi_povratnu_naknadu)
                    {
                        pov_nak = 0;
                    }

                    txtMPC.Text = mpc.ToString("#0.00");
                    double porez = Convert.ToDouble(txtPDV.SelectedValue);
                    txtVeleprodajna.Text = Convert.ToDouble((mpc - pov_nak) / Convert.ToDouble(1 + porez / 100)).ToString("#0.000");
                }
                else
                {
                    txtMPC.Text = "0.00";
                }
            }
            catch (Exception)
            {
                txtMPC.Text = "0.00";
            }
        }

        private void txtVeleprodajna_Leave_1(object sender, EventArgs e)
        {
            try
            {
                double pdv = 0, vpc = 0, mpc = 0, pov_nak = 0;
                if (double.TryParse(txtPDV.SelectedValue.ToString(), out pdv) &&
                double.TryParse(txtVeleprodajna.Text, out vpc) &&
                double.TryParse(txtPovrNaknada.Text, out pov_nak))
                {
                    if (!Class.Postavke.koristi_povratnu_naknadu)
                        pov_nak = 0;

                    mpc = vpc * (1 + pdv / 100) + pov_nak;
                    double porez = Convert.ToDouble(txtPDV.SelectedValue);
                    txtVeleprodajna.Text = vpc.ToString("#0.000");
                    txtMPC.Text = mpc.ToString("#0.00");
                }
                else
                {
                    txtMPC.Text = mpc.ToString("#0.00");
                }
            }
            catch (Exception)
            {
                txtVeleprodajna.Text = "0.00";
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPDV_Leave(object sender, EventArgs e)
        {
            try
            {
                double mpc = 0, pov_nak = 0;
                if (double.TryParse(txtMPC.Text, out mpc) && double.TryParse(txtPovrNaknada.Text, out pov_nak))
                {
                    if (!Class.Postavke.koristi_povratnu_naknadu)
                        pov_nak = 0;

                    txtMPC.Text = mpc.ToString("#0.00");
                    double porez = Convert.ToDouble(txtPDV.SelectedValue);
                    txtVeleprodajna.Text = Convert.ToDouble((mpc - pov_nak) / Convert.ToDouble(1 + porez / 100)).ToString("#0.000");
                }
                else
                {
                    txtMPC.Text = "0.00";
                }
            }
            catch (Exception)
            {
                txtMPC.Text = "0.00";
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            frmRobaUslugeKaucija fruk = new frmRobaUslugeKaucija();
            fruk.sifra = txtSifra.Text.Trim();
            fruk.ShowDialog();
        }

        private void txtNaziv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\'')
            {
                e.Handled = true;
            }
        }

        public string ispravi(string text)
        {
            string ttx = text;
            ttx = ttx.Replace("č", "c");
            ttx = ttx.Replace("Č", "C");
            ttx = ttx.Replace("ž", "z");
            ttx = ttx.Replace("Ž", "Z");
            ttx = ttx.Replace("ć", "c");
            ttx = ttx.Replace("Ć", "C");
            ttx = ttx.Replace("đ", "d");
            ttx = ttx.Replace("Đ", "D");
            ttx = ttx.Replace("š", "s");
            ttx = ttx.Replace("Š", "S");
            ttx = ttx.Replace("\\", "");
            ttx = ttx.Replace("\'", "");
            ttx = ttx.Replace("\"", "");
            return ttx;
        }

        private bool dokraja = false;

        private void button2_Click_2(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private int height = 420;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (dokraja == false)
            {
                if (height >= 420 && height <= 600)
                {
                    height = height + 10;

                    if (height >= 600)
                    {
                        height = 600;
                        dokraja = true;
                        timer1.Stop();
                    }
                }
            }
            else
            {
                if (height <= 600 && height >= 420)
                {
                    height = height - 10;

                    if (height < 420)
                    {
                        height = 420;
                        dokraja = false;
                        timer1.Stop();
                    }
                }
            }

            this.Size = new Size(933, height);
        }

        private void cbGrupa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ucitano)
                {
                    DSpodgrupa.Clear();
                    if (cbGrupa.Items.Count > 0 && cbGrupa.SelectedValue.ToString() != "System.Data.DataRowView")
                    {
                        DSpodgrupa = classSQL.select(string.Format("SELECT * FROM podgrupa WHERE id_grupa = '{0}' ORDER BY naziv;", cbGrupa.SelectedValue), "podgrupa");
                        cbpodgrupa.DataSource = DSpodgrupa.Tables[0];
                        cbpodgrupa.DisplayMember = "naziv";
                        cbpodgrupa.ValueMember = "id_podgrupa";
                    }
                }
            }
            catch { }
        }

        private void txtJamstvo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                 && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Dali ste sigurni da želite obrisati ovaj artikl?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            if (txtSifra.Text == "") { MessageBox.Show("Greška!\r\nŠifra nije ispravno upisana."); return; }

            string sql = string.Format(@"SELECT
(SELECT COALESCE(COUNT(*),0) FROM racun_stavke WHERE sifra_robe = '{0}') AS racuni,
(SELECT COALESCE(COUNT(*),0) FROM kalkulacija_stavke WHERE sifra = '{0}') AS kalkulacije,
(SELECT COALESCE(COUNT(*),0) FROM primka_stavke WHERE sifra = '{0}') AS primke,
(SELECT COALESCE(COUNT(*),0) FROM faktura_stavke WHERE sifra = '{0}') AS fakture)",
txtSifra.Text);

            DataTable DT = classSQL.select(sql, "a").Tables[0];

            if (DT.Rows.Count > 0)
            {
                if (DT.Rows[0]["racuni"].ToString() != "0") { MessageBox.Show("Ovaj artikl ne može biti obrisan jer je korišten u Računima!"); return; }
                if (DT.Rows[0]["kalkulacije"].ToString() != "0") { MessageBox.Show("Ovaj artikl ne može biti obrisan jer je korišten u Kalkulacijama!"); return; }
                if (DT.Rows[0]["primke"].ToString() != "0") { MessageBox.Show("Ovaj artikl ne može biti obrisan jer je korišten u Primkama!"); return; }
                if (DT.Rows[0]["fakture"].ToString() != "0") { MessageBox.Show("Ovaj artikl ne može biti obrisan jer je korišten u Fakturama!"); return; }

                classSQL.insert(string.Format("INSERT INTO aktivnost_zaposlenici (id_zaposlenik, datum, radnja) VALUES ('{0}', '{1}', 'Brisanje artikla. {2}');",
                    Properties.Settings.Default.id_zaposlenik,
                    DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"),
                    txtSifra.Text));

                classSQL.delete(string.Format("DELETE FROM roba WHERE sifra = '{0}';", txtSifra.Text));
                classSQL.delete(string.Format("DELETE FROM roba_prodaja WHERE sifra = '{0}';", txtSifra.Text));
                MessageBox.Show("Artikl je uspješno obrisani.");
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Report.Liste.frmPopisArtikala frm = new Report.Liste.frmPopisArtikala();
            frm.ShowDialog();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string path = openFileDialog1.FileName;
                    string connectionString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", path);
                    string query = string.Format("select * from [{0}$]", "Sheet");
                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connectionString);
                    DataSet DS = new DataSet();
                    dataAdapter.Fill(DS);
                    if (DS == null)
                    {
                        MessageBox.Show("Datoteka nema stavki.");
                        return;
                    }

                    DataTable DT = DS.Tables[0];
                    string sql = "";

                    sql = "alter table roba drop CONSTRAINT  roba_sifra_key;";

                    sql = string.Format(@"drop table roba;
CREATE TABLE roba
(
    naziv character varying,
    id_grupa bigint,
    jm character varying(30),
    vpc numeric,
    mpc money,
    id_zemlja_porijekla integer,
    id_zemlja_uvoza integer,
    id_partner bigint,
    id_manufacturers bigint,
    id_roba serial NOT NULL,
    sifra character varying,
    ean character varying(30),
    porez character varying(20),
    oduzmi character(2),
    nc money,
    porez_potrosnja character varying(10),
    opis text,
    brand character varying,
    jamstvo integer,
    akcija smallint,
    link_za_slike character varying,
    id_podgrupa integer,
    novo boolean DEFAULT true,
    editirano boolean DEFAULT true,
    datum_syn timestamp without time zone,
    CONSTRAINT roba_primary_key PRIMARY KEY (id_roba),
    CONSTRAINT roba_sifra_key UNIQUE (sifra)
)
WITH (
    OIDS=FALSE
);
ALTER TABLE roba
    OWNER TO postgres;");

                    //classSQL.insert(sql);

                    //sql = "ALTER TABLE roba ADD CONSTRAINT roba_sifra_key UNIQUE (sifra);";
                    //classSQL.insert(sql);

                    int sifra = 0, naziv = 0, nbc = 0, vpc = 0, mpc = 0, pdv = 0, ean = 0, kolicina = 0, jmj = 0;

                    naziv = 0;
                    //nbc = 10;
                    //vpc = 12;
                    mpc = 2;
                    //pdv = 3;

                    //kolicina = 2;
                    jmj = 1;
                    //ean = 15;

                    sql = "";
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        decimal dmpc = 0, dvpc = 0;
                        DataRow dr = DT.Rows[i];
                        if (decimal.TryParse(dr[mpc].ToString(), out dmpc))
                        {
                            dvpc = dmpc / 1.25m;
                        }
                        sql += "INSERT INTO roba (naziv, id_grupa, jm, vpc, mpc, id_zemlja_porijekla, id_zemlja_uvoza, id_partner, id_manufacturers, sifra, ean, porez, oduzmi, nc, porez_potrosnja, opis, brand, jamstvo, akcija, link_za_slike, id_podgrupa, novo, editirano)\n VALUES (" +
                            "'" + dr[naziv].ToString().Replace('\'', ' ') + "', " + //naziv
                            "'1', " + //id_grupa
                            "'" + dr[jmj] + "', " + //jm
                            "'" + dvpc.ToString().Replace(',', '.') + "', " + //vpc
                                                                              //"'" + dr[vpc].ToString().Replace(',', '.') + "', " + //vpc
                            "'" + dr[mpc] + "', " + //mpc
                            "'60', " + //id_zemlja_porijekla
                            "'60', " + //id_zemlja_uvoza
                            "'1', " + //id_partner
                            "'1', " + //id_manufacturers
                            "'" + ("".ToString().Length > 0 ? dr[sifra] : i + 1) + "', " + //sifra
                                                                                           //"'" + (dr[sifra].ToString().Length > 0 ? dr[sifra] : i) + "', " + //sifra
                            "'" + ("".ToString().Length > 0 ? dr[ean] : "-1") + "', " + //ean
                                                                                        //"'" + (dr[ean].ToString().Length > 0 ? dr[ean] : "-1") + "', " + //ean
                            "'25', " + //porez
                                       //"'" + dr[pdv] + "', " + //porez
                            "'" + (dr[jmj].ToString().ToUpper().Trim() == "usl".ToUpper() ? "NE" : "DA") + "', " + //oduzmi
                                                                                                                   //"'DA', " + //oduzmi
                            "'" + ("".ToString().ToString().Length > 0 ? dr[nbc] : 0) + "', " + //nbc
                                                                                                //"'" + (dr[nbc].ToString().ToString().Length > 0 ? dr[nbc] : 0) + "', " + //nbc
                            "'', " + //porez_potrosnja
                            "'', " + //opis
                            "'', " + //brand
                            "'0', " + //jamstvo
                            "'0', " + //akcija
                            "'', " + //link_za_slike
                            "'0', " + //id_podgrupa
                            "'1', " + //novo
                            "'1' );\n"; //editirano

                        if (i > 0 && (i % 500) == 0)
                        {
                            classSQL.insert(sql);
                            sql = "";
                        }
                    }
                    if (sql.Length > 0)
                    {
                        classSQL.insert(sql);
                    }
                    sql = "";

                    int sklCount = 0;
                    sql = "select * from skladiste";
                    DataSet dsSkl = classSQL.select(sql, "skladiste");
                    if (dsSkl != null && dsSkl.Tables.Count > 0)
                    {
                        sklCount = Convert.ToInt32(dsSkl.Tables[0].Rows.Count);
                    }

                    sql = "";
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        DataRow dr = DT.Rows[i];

                        if (sklCount == 0)
                        {
                            sql = "insert into skladiste (skladiste, id_grad, id_zemlja, aktivnost, editirano) VALUES ('Skladiste 1', '2806', '60', 'DA', '1');";
                            classSQL.insert(sql);

                            sql = "select * from skladiste";
                            dsSkl = classSQL.select(sql, "skladiste");
                            if (dsSkl != null && dsSkl.Tables.Count > 0)
                            {
                                sklCount = Convert.ToInt32(dsSkl.Tables[0].Rows.Count);
                            }
                        }

                        foreach (DataRow drSkl in dsSkl.Tables[0].Rows)
                        {
                            decimal dmpc = 0, dvpc = 0;
                            dr = DT.Rows[i];
                            if (decimal.TryParse(dr[mpc].ToString(), out dmpc))
                            {
                                dvpc = dmpc / 1.25m;
                            }

                            sql += "INSERT INTO roba_prodaja (id_skladiste, kolicina, nc, vpc, porez, sifra, porez_potrosnja, novo, editirano)\n VALUES (" +
                                "'" + drSkl["id_skladiste"] + "', " +
                                "'0', " +
                                //"'" + dr[kolicina].ToString() + "', " +
                                "'0', " +
                                //"'" + dr[nbc] + "', " +
                                "'" + dvpc.ToString().Replace(',', '.') + "', " +
                                "'25', " +
                                //"'" + dr[pdv] + "', " +
                                //"'" + (dr[sifra].ToString().Length > 0 ? dr[sifra] : i + 1) + "', " +
                                "'" + ("".ToString().Length > 0 ? dr[sifra] : i + 1) + "', " +
                                "'0', " +
                                "'1', " +
                                "'1' );\n";

                            if ((i % 500) == 0)
                            {
                                classSQL.insert(sql);
                                sql = "";
                            }
                        }
                    }

                    if (sql.Length > 0)
                    {
                        classSQL.insert(sql);
                    }

                    MessageBox.Show("Izvršeno");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtProizvodackaCijena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtNabavna.Select();
            }
        }

        private void txtProizvodackaCijena_Leave(object sender, EventArgs e)
        {
            try
            {
                txtProizvodackaCijena.Text = Convert.ToDouble(txtProizvodackaCijena.Text).ToString("#0.00");
            }
            catch (Exception)
            {
                txtProizvodackaCijena.Text = "0.00";
            }
        }

        private void BtnBarcode_Click(object sender, EventArgs e)
        {
            // Get barcodes
            /*List<string> Codes = new List<string>();
            DataTable DTroba = Global.Database.GetRoba(txtSifra.Text);
            if(DTroba.Rows.Count > 0)
            {
                string eans = DTroba.Rows[0]["ean"].ToString();
                string[] array = eans.Split(';');
                for (int i = 0; i < array.Length; i++)
                    Codes.Add(array[i]);
            }*/

            // Open form and pass barcodes list
            FrmEanCodes form = new FrmEanCodes
            {
                EanCodes = this.EanCodes
            };
            form.ShowDialog();

            // After the form is closed
            EanCodes = form.EanCodes;
        }
    }
}