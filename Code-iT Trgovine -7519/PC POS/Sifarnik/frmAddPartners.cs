using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class frmAddPartners : Form
    {
        public frmAddPartners()
        {
            InitializeComponent();
        }

        private bool zapocni_uređivanje_odrzavanja = false;
        private bool edit = false;
        private string trenutniOIB;
        public frmMenu MainFormMenu { get; set; }
        private bool preload = false;
        private bool addPoslovnica = false;
        private int id = 0;

        private DataTable dtPartnerPoslovnice = null;

        private DataTable DTpostavke_sinkronizacije = classSQL.select("SELECT * FROM postavke_sinkronizacije", "postavke_sinkronizacije").Tables[0];

        private void frmAddPartners_Load(object sender, EventArgs e)
        {
            txtSifra.Text = brojPartner();
            txtBrojKartice.Text = DateTime.Now.Year.ToString().Remove(0, 2) + brojKartice();
            SetCb();
            txtIme.Select();
            trenutniOIB = "";
            oib_polje.Text = "OIB";
            txtOdgodaPlacanja.Text = "0";
            addPoslovnica = false;
            poslovnicaPartnerKontrole(addPoslovnica);
            preload = true;

            if (Class.Postavke.rucnoUpisivanjeKarticeKupca)
            {
                txtBrojKartice.ReadOnly = !Class.Postavke.rucnoUpisivanjeKarticeKupca;
                txtBrojKartice.Text = "";
            }

            if (Class.PodaciTvrtka.oibTvrtke == Class.Postavke.OIB_PC1)
            {
                nuBrojOdrzavanja.Visible = true;
                nuBrojVipa.Visible = true;
                chbKorisnikPrograma.Visible = true;
                chbUgovor.Visible = true;
                chbBivsiKorisnik.Visible = true;
                chbPcPos.Visible = true;
                chbCaffe.Visible = true;
                chbResort.Visible = true;
                chbGodisnjeOdr.Visible = true;
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

        private void SetCb()
        {
            DataTable DT = classSQL.select("SELECT * FROM zupanije ORDER BY naziv", "zupanije").Tables[0];
            txtZupanija.DataSource = DT;
            txtZupanija.DisplayMember = "naziv";
            txtZupanija.ValueMember = "id_zupanija";
            txtZupanija.SelectedValue = "8";

            DataTable DT1 = classSQL.select("SELECT * FROM djelatnosti ORDER BY ime_djelatnosti", "djelatnosti").Tables[0];
            txtDjelatnost.DataSource = DT1;
            txtDjelatnost.DisplayMember = "ime_djelatnosti";
            txtDjelatnost.ValueMember = "id_djelatnost";

            //combobox drzave
            //string sql = "SELECT * FROM zemlja ORDER BY zemlja;";
            getCmbDrzave(true);

            //combobox gradovi
            getCmbGradovi(true);

            //dobiva skladista
            getSkladista(false);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Color x = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
            //Color y = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(109)))), ((int)(((byte)(135)))));

            //Graphics c = e.Graphics;
            //Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            //c.FillRectangle(bG, 0, 0, Width, Height);
        }

        //private void radioButton2_CheckedChanged(object sender, EventArgs e)
        //{
        //    xIme.Visible = true;
        //    xPrezime.Visible = true;
        //    xGrad.Visible = true;
        //    xAdresa.Visible = true;
        //    xTel.Visible = false;
        //    xmail.Visible = true;
        //    xOIB.Visible = false;
        //    xTvrtka.Visible = false;
        //}

        //private void radioButton1_CheckedChanged(object sender, EventArgs e)
        //{
        //    xIme.Visible = false;
        //    xPrezime.Visible = false;
        //    xGrad.Visible = true;
        //    xAdresa.Visible = true;
        //    xTel.Visible = true;
        //    xmail.Visible = true;
        //    xOIB.Visible = true;
        //    xTvrtka.Visible = true;
        //}

        private string brojPartner()
        {
            string sql = "SELECT MAX(id_partner) FROM partners WHERE id_partner < 200000";
            if (DTpostavke_sinkronizacije.Rows.Count > 0)
            {
                if (DTpostavke_sinkronizacije.Rows[0]["aktivirano"].ToString() == "1")
                {
                    sql = "SELECT MAX(id_partner) FROM partners WHERE id_partner >= 200000";
                    DataTable DSbr1 = classSQL.select(sql, "partners").Tables[0];
                    if (DSbr1.Rows[0][0].ToString() != "")
                    {
                        return (Convert.ToDouble(DSbr1.Rows[0][0].ToString()) + 1).ToString();
                    }
                    else
                    {
                        return "200000";
                    }
                }
            }

            DataTable DSbr = classSQL.select(sql, "partners").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private string brojKartice(bool rucnoUpisivanjeKarticeKupcaOverride = false)
        {
            //if (Class.Postavke.rucnoUpisivanjeKarticeKupca)
            //{
            //    return "";
            //}
            //else 
            if(!Class.Postavke.rucnoUpisivanjeKarticeKupca || rucnoUpisivanjeKarticeKupcaOverride)
            {
                try
                {
                    DataTable DSbr = classSQL.select("SELECT coalesce(MAX(case when broj_kartice is null or length(broj_kartice) = 0 then 0 else broj_kartice::bigint end), 0) FROM partners where length(broj_kartice) <= 6", "partners").Tables[0];
                    if (DSbr.Rows[0][0].ToString() != "")
                    {
                        return (Convert.ToInt32(DSbr.Rows[0][0].ToString()) + 1).ToString("000000");
                    }
                    else
                    {
                        return 1.ToString("000000");
                    }
                }
                catch
                {
                    return 1.ToString("000000");
                }
            }
            return "";
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            if (txtBrojKartice.Text.Trim().Length > 0) {
                int brojkartice = 0;
                if (!int.TryParse(txtBrojKartice.Text.Trim(), out brojkartice)) {
                    MessageBox.Show(string.Format("Partner nije spremljen.Broj kartice treba biti broj.", Environment.NewLine));
                    return;
                }
            }
            txtEmail.Text = txtEmail.Text.Trim().Replace("'", "").Replace("\"", "");
            txtOib.Text = txtOib.Text.Trim().Replace("'", "").Replace("\"", "");
            txtIme.Text = txtIme.Text.Trim().Replace("'", "").Replace("\"", "");
            txtPrezime.Text = txtPrezime.Text.Trim().Replace("'", "").Replace("\"", "");
            txtAdresa.Text = txtAdresa.Text.Trim().Replace("'", "").Replace("\"", "");
            txtTvrtka.Text = txtTvrtka.Text.Trim().Replace("'", "").Replace("\"", "");
            if (rbPoslovni.Checked)
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^[0-9]+$");

                if (txtTvrtka.Text == "")
                {
                    MessageBox.Show("Krivi unos za tvrtku.", "Greška");
                    return;
                }

                if (!OstaleFunkcije.OIB_Validacija(txtOib.Text))
                {
                    MessageBox.Show("Krivi unos za OIB.\r\nProgram dopušta spremanje sa krivim oib-om.", "Greška");
                }

                if (txtAdresa.Text == "")
                {
                    MessageBox.Show("Krivi unos za adresu.", "Greška");
                    return;
                }

                if (postojiOib(txtOib.Text))
                {
                    if (edit)
                    {
                        if (trenutniOIB != txtOib.Text)
                        {
                            if (MessageBox.Show("U bazi već postoji partner sa zadanim OIB-om. Da li želite " +
                                 "spremiti još jednog partnera s istim OIB-om?", "Upozorenje",
                                 MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                            }
                            else
                            {
                                MessageBox.Show("Partner nije spremljen!", "Upozorenje");
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("U bazi već postoji partner sa zadanim OIB-om. Da li želite " +
                             "spremiti još jednog partnera s istim OIB-om?", "Upozorenje",
                             MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                        }
                        else
                        {
                            MessageBox.Show("Partner nije spremljen!", "Upozorenje");
                            return;
                        }
                    }
                }
            }
            else if (rbPrivatni.Checked)
            {
                if (txtIme.Text == "")
                {
                    MessageBox.Show("Krivi unos za ime.", "Greška");
                    return;
                }
                if (txtPrezime.Text == "")
                {
                    MessageBox.Show("Krivi unos za prezime.", "Greška");
                    return;
                }
                if (txtAdresa.Text == "")
                {
                    MessageBox.Show("Krivi unos za adresu.", "Greška");
                    return;
                }

                if (txtTvrtka.Text == "")
                {
                    txtTvrtka.Text = txtIme.Text + " " + txtPrezime.Text;
                }

                if (txtOib.Text != "")
                {
                    if (postojiOib(txtOib.Text))
                    {
                        if (edit)
                        {
                            if (trenutniOIB != txtOib.Text)
                            {
                                if (MessageBox.Show("U bazi već postoji partner sa zadanim OIB-om. Da li želite " +
                                     "spremiti još jednog partnera s istim OIB-om?", "Upozorenje",
                                     MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                }
                                else
                                {
                                    MessageBox.Show("Partner nije spremljen!", "Upozorenje");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (MessageBox.Show("U bazi već postoji partner sa zadanim OIB-om. Da li želite " +
                                 "spremiti još jednog partnera s istim OIB-om?", "Upozorenje",
                                 MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                            }
                            else
                            {
                                MessageBox.Show("Partner nije spremljen!", "Upozorenje");
                                return;
                            }
                        }
                    }
                }
            }

            if (edit)
            {
                Update();
                edit = true;
            }
            else
            {
                Spremi();
                edit = true;
            }
            addPoslovnica = true; //true
            poslovnicaPartnerKontrole(addPoslovnica);
            zapocni_uređivanje_odrzavanja = false;
            EnableDisable(false);
        }

        private void Update()
        {
            string brojKarticeSTR = "0";
            string aktivan = "0";
            string letak = "0";
            string vrsta_korisnika = "0";
            string broj_bodova = "0";
            decimal popust = 0;

            if (chbAktivan.Checked)
            {
                aktivan = "1";
            }

            if (chbPrimanjeLetaka.Checked)
            {
                letak = "1";
            }

            if (rbPoslovni.Checked)
            {
                vrsta_korisnika = "1";
            }

            decimal.TryParse(txtPopust.Text, out popust);

            if (txtBodovi.Text != "")
            {
                broj_bodova = "1";
            }

            if (txtBrojKartice.Text == "" || txtBrojKartice.Text == "0")
            {
                brojKarticeSTR = brojKartice();
                txtBrojKartice.Text = brojKarticeSTR;
            }
            else
            {
                brojKarticeSTR = txtBrojKartice.Text;
            }

            if (Class.Postavke.rucnoUpisivanjeKarticeKupca)
            {
                brojKarticeSTR = txtBrojKartice.Text;
            }

            if (Class.Postavke.rucnoUpisivanjeKarticeKupca && txtBrojKartice.Text.Length > 0)
            {
                DataSet dsKulkoIhIma = classSQL.select(string.Format("Select coalesce(count(*), 0) as br from partners where broj_kartice = '{0}' and id_partner != {1};", txtBrojKartice.Text, id), "partners");
                if (Convert.ToInt32(dsKulkoIhIma.Tables[0].Rows[0][0].ToString()) >= 1)
                {
                    brojKarticeSTR = brojKartice(true);
                    txtBrojKartice.Text = brojKarticeSTR;
                    MessageBox.Show(string.Format("Kartica kupca pod odabranim brojem več postoji u partnerima.{0}Dodijeljen je automatski novi broj.", Environment.NewLine));
                    //return;
                }
            }

            string sql = string.Format(@"UPDATE partners
SET
    ime_tvrtke = '{0}',
    id_grad = '{1}',
    adresa = '{2}',
    oib = '{3}',
    napomena = '{4}',
    id_djelatnost = '{5}',
    ime = '{6}',
    prezime = '{7}',
    email = '{8}',
    tel = '{9}',
    mob = '{10}',
    datum_rodenja = '{11}',
    bodovi = '{12}',
    editirano = '1',
    popust = '{13}',
    broj_kartice = '{14}',
    aktivan = '{15}',
    vrsta_korisnika = '{16}',
    primanje_letaka = '{17}',
    id_zupanija = '{18}',
    oib_polje = '{19}',
    id_zemlja = '{20}',
    odgoda_placanja_u_danima = '{21}',
    uSustavPdv = {22},
    default_skladiste = {23},
    zacrnjeno = {24}
WHERE id_partner = '{25}';",
            txtTvrtka.Text.Replace("'", "").Replace("\"", ""),
            cbGrad.SelectedValue,
            txtAdresa.Text.Replace("'", "").Replace("\"", ""),
            txtOib.Text.Replace("'", "").Replace("\"", ""),
            rtbNapomena.Text.Replace("'", "").Replace("\"", ""),
            txtDjelatnost.SelectedValue,
            txtIme.Text.Replace("'", "").Replace("\"", ""),
            txtPrezime.Text.Replace("'", "").Replace("\"", ""),
            txtEmail.Text.Replace("'", "").Replace("\"", ""),
            txtTel.Text.Replace("'", "").Replace("\"", ""),
            txtMob.Text.Replace("'", "").Replace("\"", ""),
            dtpDatum.Value.ToString("yyyy-MM-dd"),
            broj_bodova,
            popust.ToString().Replace(",", "."),
            brojKarticeSTR,
            aktivan,
            vrsta_korisnika,
            letak,
            txtZupanija.SelectedValue,
            oib_polje.Text,
            cbDrzava.SelectedValue,
            (txtOdgodaPlacanja.Text == "" ? "0" : txtOdgodaPlacanja.Text),
            chbSustavPdv.Checked,
            cmbSkladistePartnera.SelectedValue,
            chbZacrniPodatke.Checked,
            txtSifra.Text.Replace("'", "").Replace("\"", "")
            );

            //rtbNapomena.Text = sql;
            provjera_sql(classSQL.update(sql));
            trenutniOIB = "";
            MessageBox.Show("Spremljeno.");
        }

        private void Spremi()
        {
            string aktivan = "0";
            string letak = "0";
            string vrsta_korisnika = "0";
            string broj_bodova = "0";
            decimal popust = 0;

            if (chbAktivan.Checked)
            {
                aktivan = "1";
            }

            if (chbPrimanjeLetaka.Checked)
            {
                letak = "1";
            }

            if (rbPoslovni.Checked)
            {
                vrsta_korisnika = "1";
            }

            if (txtBodovi.Text != "")
            {
                broj_bodova = "1";
            }

            if (Class.Postavke.rucnoUpisivanjeKarticeKupca && txtBrojKartice.Text.Length > 0 && txtBrojKartice.Text != "0")
            {
                DataSet dsKulkoIhIma = classSQL.select(@"Select coalesce(count(*), 0) as br from partners where broj_kartice = '" + txtBrojKartice.Text + "'", "partners");
                if (Convert.ToInt32(dsKulkoIhIma.Tables[0].Rows[0][0].ToString()) != 0)
                {
                    MessageBox.Show("Kartica kupca pod odabranim brojem več postoji u partnerima.");
                    return;
                }
            }

            decimal.TryParse(txtPopust.Text, out popust);

            string sql = string.Format(@"INSERT INTO partners
(
    id_partner, ime_tvrtke, id_grad, adresa, oib, napomena, id_djelatnost, ime, prezime, email, tel, mob, datum_rodenja,
    bodovi, popust, broj_kartice, aktivan, vrsta_korisnika, primanje_letaka, id_zupanija, oib_polje, id_zemlja, novo, odgoda_placanja_u_danima, uSustavPdv, default_skladiste, zacrnjeno
)
VALUES
(
    '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}',
    '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}', '1', '{22}', {23}, {24}, {25}
);",
    brojPartner(),
    txtTvrtka.Text.Replace("'", "").Replace("\"", ""),
    cbGrad.SelectedValue,
    txtAdresa.Text.Replace("'", "").Replace("\"", ""),
    txtOib.Text.Replace("'", "").Replace("\"", ""),
    rtbNapomena.Text.Replace("'", "").Replace("\"", ""),
    txtDjelatnost.SelectedValue,
    txtIme.Text.Replace("'", "").Replace("\"", ""),
    txtPrezime.Text.Replace("'", "").Replace("\"", ""),
    txtEmail.Text.Replace("'", "").Replace("\"", ""),
    txtTel.Text.Replace("'", "").Replace("\"", ""),
    txtMob.Text.Replace("'", "").Replace("\"", ""),
    dtpDatum.Value.ToString("yyyy-MM-dd"),
    broj_bodova,
    popust.ToString().Replace(",", "."),
    txtBrojKartice.Text,
    aktivan,
    vrsta_korisnika,
    letak,
    txtZupanija.SelectedValue,
    oib_polje.Text.Replace("'", "").Replace("\"", ""),
    cbDrzava.SelectedValue,
    (txtOdgodaPlacanja.Text == "" ? "0" : txtOdgodaPlacanja.Text),
    chbSustavPdv.Checked,
    cmbSkladistePartnera.SelectedValue,
    chbZacrniPodatke.Checked);

            //rtbNapomena.Text = sql;
            provjera_sql(classSQL.insert(sql));
            MessageBox.Show("Spremljeno.");
            trenutniOIB = "";
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            txtAdresa.Text = "";
            txtBodovi.Text = "";
            txtBrojKartice.Text = DateTime.Now.Year.ToString().Remove(0, 2) + brojKartice();
            txtEmail.Text = "";
            txtIme.Text = "";
            txtMob.Text = "";
            txtOib.Text = "";
            txtPopust.Text = "0";
            txtPrezime.Text = "";
            txtSifra.Text = brojPartner();
            txtTel.Text = "";
            txtTvrtka.Text = "";
            txtOdgodaPlacanja.Text = "0";
            chbSustavPdv.Checked = true;
            chbZacrniPodatke.Checked = false;
            edit = false;
            zapocni_uređivanje_odrzavanja = false;
            addPoslovnica = false;
            poslovnicaPartnerKontrole(addPoslovnica);

            if (Class.Postavke.rucnoUpisivanjeKarticeKupca)
            {
                txtBrojKartice.ReadOnly = !Class.Postavke.rucnoUpisivanjeKarticeKupca;
                txtBrojKartice.Text = "";
            }

            id = 0;

            getSkladista(false);
        }

        private void EnableDisable(bool x)
        {
            txtAdresa.Enabled = x;
            txtBodovi.Enabled = x;
            txtEmail.Enabled = x;
            txtIme.Enabled = x;
            txtMob.Enabled = x;
            txtOib.Enabled = x;
            txtPopust.Enabled = x;
            txtPrezime.Enabled = x;
            txtTel.Enabled = x;
            txtTvrtka.Enabled = x;
            cbGrad.Enabled = x;

            if (x == true) { button1.Visible = false; } else { button1.Visible = true; }
        }

        private void poslovnicaPartnerKontrole(bool b)
        {
            try
            {
                groupBox4.Enabled = b;
                getPartnerPoslovniceData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partner = new frmPartnerTrazi();
            partner.ShowDialog();

            if (Properties.Settings.Default.id_partner != "")
            {
                //                string sql = string.Format(@"select ID, naziv from (
                //select 0 as ID, 'Nije odabrano' as naziv 0 as sort
                //union seelct id_skladiste as ID, skladiste as naziv 1 as sort from skladista
                //) x
                //order by x.sort, x.naziv");

                //                DataSet dsSkladiste = classSQL.select(sql, "skladiste");
                //                if (dsSkladiste != null && dsSkladiste.Tables.Count > 0 && dsSkladiste.Tables[0] != null && dsSkladiste.Tables[0].Rows.Count > 0) {
                //                    cmbSkladistePartnera.DisplayMember = "naziv";
                //                    cmbSkladistePartnera.ValueMember = "ID";
                //                    cmbSkladistePartnera.DataSource = dsSkladiste;
                //                }

                getSkladista(true);

                FillPartner(Properties.Settings.Default.id_partner);
                edit = true;
                EnableDisable(true);
            }
        }

        private void FillPartner(string id)
        {
            this.id = Convert.ToInt32(id);
            DataTable DT = classSQL.select(string.Format("SELECT * FROM partners WHERE id_partner = '{0}';", id), "partners").Tables[0];
            txtAdresa.Text = DT.Rows[0]["adresa"].ToString();
            txtBodovi.Text = DT.Rows[0]["bodovi"].ToString();
            txtBrojKartice.Text = DT.Rows[0]["broj_kartice"].ToString();
            txtDjelatnost.SelectedValue = DT.Rows[0]["id_djelatnost"].ToString();
            txtEmail.Text = DT.Rows[0]["email"].ToString();
            txtIme.Text = DT.Rows[0]["ime"].ToString();
            txtMob.Text = DT.Rows[0]["mob"].ToString();
            txtOib.Text = DT.Rows[0]["oib"].ToString().Trim();
            trenutniOIB = DT.Rows[0]["oib"].ToString().Trim();
            txtPopust.Text = DT.Rows[0]["popust"].ToString();
            txtPrezime.Text = DT.Rows[0]["prezime"].ToString();
            txtSifra.Text = DT.Rows[0]["id_partner"].ToString();
            txtTel.Text = DT.Rows[0]["tel"].ToString();
            txtTvrtka.Text = DT.Rows[0]["ime_tvrtke"].ToString();
            rtbNapomena.Text = DT.Rows[0]["napomena"].ToString();
            txtOdgodaPlacanja.Text = DT.Rows[0]["odgoda_placanja_u_danima"].ToString();
            chbSustavPdv.Checked = Convert.ToBoolean(DT.Rows[0]["uSustavPdv"]);
            try
            {
                cbDrzava.SelectedValue = (DT.Rows[0]["id_zemlja"].ToString().Length == 0 ? 60.ToString() : DT.Rows[0]["id_zemlja"].ToString());
            }
            catch { }
            txtZupanija.SelectedValue = DT.Rows[0]["id_zupanija"].ToString();
            cbGrad.SelectedValue = DT.Rows[0]["id_grad"].ToString();
            oib_polje.Text = DT.Rows[0]["oib_polje"].ToString();

            cmbSkladistePartnera.SelectedValue = DT.Rows[0]["default_skladiste"].ToString();

            try
            {
                dtpDatum.Value = Convert.ToDateTime(DT.Rows[0]["datum_rodenja"].ToString());
            }
            catch (Exception)
            {
            }

            if (DT.Rows[0]["aktivan"].ToString() == "1")
            {
                chbAktivan.Checked = true;
            }

            if (DT.Rows[0]["primanje_letaka"].ToString() == "1")
            {
                chbPrimanjeLetaka.Checked = true;
            }

            if (DT.Rows[0]["vrsta_korisnika"].ToString() == "1")
            {
                rbPoslovni.Checked = true;
                rbPrivatni.Checked = false;
                addPoslovnica = true; //true
                poslovnicaPartnerKontrole(addPoslovnica);
            }
            else
            {
                rbPoslovni.Checked = false;
                rbPrivatni.Checked = true;
                addPoslovnica = false; //true
                poslovnicaPartnerKontrole(addPoslovnica);
            }

            chbZacrniPodatke.Checked = Convert.ToBoolean(DT.Rows[0]["zacrnjeno"]);

            DataTable DTodr = classSQL.select(string.Format("SELECT * FROM partners_odrzavanje WHERE id_partner = '{0}';", id), "partners_odrzavanje").Tables[0];

            if (DTodr.Rows.Count > 0)
            {
                int odr = 0, vip = 0;
                int.TryParse(DTodr.Rows[0]["odrzavanje_kol"].ToString(), out odr);
                int.TryParse(DTodr.Rows[0]["internet_kol"].ToString(), out vip);

                nuBrojOdrzavanja.Value = odr;
                nuBrojVipa.Value = vip;

                if (DTodr.Rows[0]["web_ured"].ToString() == "1")
                    chbWebUred.Checked = true;
                else
                    chbWebUred.Checked = false;

                if (DTodr.Rows[0]["nas_program"].ToString() == "1")
                    chbKorisnikPrograma.Checked = true;
                else
                    chbKorisnikPrograma.Checked = false;

                if (DTodr.Rows[0]["ugovor"].ToString() == "1")
                    chbUgovor.Checked = true;
                else
                    chbUgovor.Checked = false;

                if (DTodr.Rows[0]["bivsi_korisnik"].ToString() == "1")
                    chbBivsiKorisnik.Checked = true;
                else
                    chbBivsiKorisnik.Checked = false;

                if (DTodr.Rows[0]["tablet"].ToString() == "1")
                    chbTablet.Checked = true;
                else
                    chbTablet.Checked = false;

                if (DTodr.Rows[0]["pcpos"].ToString() == "1")
                    chbPcPos.Checked = true;
                else
                    chbPcPos.Checked = false;

                if (DTodr.Rows[0]["pccaffe"].ToString() == "1")
                    chbCaffe.Checked = true;
                else
                    chbCaffe.Checked = false;

                if (DTodr.Rows[0]["resort"].ToString() == "1")
                    chbResort.Checked = true;
                else
                    chbResort.Checked = false;

                if (DTodr.Rows[0]["godisnje_odr"].ToString() == "1")
                    chbGodisnjeOdr.Checked = true;
                else
                    chbGodisnjeOdr.Checked = false;
            }
            else
            {
                chbUgovor.Checked = false;
                chbKorisnikPrograma.Checked = false;
                chbWebUred.Checked = false;
                chbBivsiKorisnik.Checked = false;
                chbTablet.Checked = false;
                chbPcPos.Checked = false;
                chbCaffe.Checked = false;
                chbResort.Checked = false;
                chbGodisnjeOdr.Checked = false;

                nuBrojOdrzavanja.Value = 0;
                nuBrojVipa.Value = 0;
            }
            zapocni_uređivanje_odrzavanja = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EnableDisable(true);
            btnOdustani.PerformClick();
            zapocni_uređivanje_odrzavanja = false;
        }

        private void txtOib_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        private bool postojiOib(string oib)
        {
            if (oib == "")
                return false;

            DataTable DT = classSQL.select(string.Format("SELECT count(*), id_partner FROM partners WHERE oib = '{0}' group by id_partner;", oib), "partners").Tables[0];

            if (DT.Rows.Count > 0)
            {
                if (Convert.ToInt16(DT.Rows[0][0].ToString()) > 0)
                {
                    if (DT.Rows[0][1].ToString() != txtSifra.Text)
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void cbDrzava_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (preload == true)
            {
                try
                {
                    ComboBox cmb = (ComboBox)sender;
                    if (cmb.SelectedValue != null)
                    {
                        DataSet DSgrad1 = classSQL.select(string.Format("SELECT * FROM grad WHERE drzava = '{0}' ORDER BY grad;", cmb.SelectedValue), "grad");

                        if (cmb.Name == "cbDrzava")
                        {
                            cbGrad.DataSource = DSgrad1.Tables[0];
                            cbGrad.DisplayMember = "grad";
                            cbGrad.ValueMember = "id_grad";
                        }
                        else
                        {
                            cmbPoslovnicaGrad.DataSource = DSgrad1.Tables[0];
                            cmbPoslovnicaGrad.DisplayMember = "grad";
                            cmbPoslovnicaGrad.ValueMember = "id_grad";
                        }
                    }
                }
                catch { }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                frmNoviGrad novigr = new frmNoviGrad();
                novigr.ShowDialog();

                //CB grad
                getCmbGradovi(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getCmbGradovi(bool all)
        {
            try
            {
                cbGrad.DataSource = null;
                cmbPoslovnicaGrad.DataSource = null;

                string sql = string.Format("SELECT * FROM grad WHERE drzava = '{0}' ORDER BY grad;", cbDrzava.SelectedValue.ToString());

                DataSet DSgrad = classSQL.select(sql, "grad");
                cbGrad.DataSource = DSgrad.Tables[0];
                cbGrad.DisplayMember = "grad";
                cbGrad.ValueMember = "id_grad";

                cbGrad.SelectedValue = Class.PodaciTvrtka.gradTvrtkaId;

                if (all)
                {
                    DataTable dtGrad = classSQL.select(sql, "grad").Tables[0];
                    getCmbGradPoslovnice(dtGrad);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getSkladista(bool uzmiSvaSkladista = false)
        {
            try
            {
                string sql = string.Format(@"select ID, naziv
from (
    select 0 as ID, 'Nije odabrano' as naziv, 0 as sort
    union
    select id_skladiste as ID, skladiste as naziv, 1 as sort from skladiste where aktivnost = 'DA'
) x
order by x.sort, x.naziv;");

                if (uzmiSvaSkladista)
                {
                    sql = string.Format(@"select ID, naziv
from (
    select 0 as ID, 'Nije odabrano' as naziv, 0 as sort
    union
    select id_skladiste as ID, skladiste as naziv, 1 as sort from skladiste
) x
order by x.sort, x.naziv;");
                }

                DataSet dsSkladiste = classSQL.select(sql, "skladiste");

                if (dsSkladiste != null && dsSkladiste.Tables.Count > 0 && dsSkladiste.Tables[0].Rows.Count > 0)
                {
                    cmbSkladistePartnera.ValueMember = "ID";
                    cmbSkladistePartnera.DisplayMember = "naziv";
                    cmbSkladistePartnera.DataSource = dsSkladiste.Tables[0];
                    cmbSkladistePartnera.SelectedValue = 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void getCmbGradPoslovnice(DataTable dt)
        {
            if (dt == null)
            {
                string sql = string.Format("SELECT * FROM grad WHERE drzava = '{0}' ORDER BY grad;", cbDrzava.SelectedValue.ToString());
                dt = classSQL.select(sql, "grad").Tables[0];
            }

            cmbPoslovnicaGrad.DataSource = dt;
            cmbPoslovnicaGrad.DisplayMember = "grad";
            cmbPoslovnicaGrad.ValueMember = "id_grad";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                frmDodajZemlju zemlje = new frmDodajZemlju();
                zemlje.ShowDialog();

                getCmbDrzave(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getCmbDrzave(bool all)
        {
            try
            {
                cbDrzava.DataSource = null;
                cmbPoslovnicaDrzava.DataSource = null;

                string sql = "SELECT * FROM zemlja ORDER BY zemlja;";

                DataSet DSdrzava = classSQL.select(sql, "zemlja");
                cbDrzava.DataSource = DSdrzava.Tables[0];
                cbDrzava.DisplayMember = "zemlja";
                cbDrzava.ValueMember = "id_zemlja";
                cbDrzava.SelectedValue = "60";

                if (all)
                {
                    DataTable dtDrzava = classSQL.select(sql, "zemlja").Tables[0];
                    getCmbDrzavaPoslovnice(dtDrzava);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getCmbDrzavaPoslovnice(DataTable dt)
        {
            if (dt == null)
            {
                string sql = "SELECT * FROM zemlja ORDER BY zemlja;";
                dt = classSQL.select(sql, "zemlja").Tables[0];
            }

            cmbPoslovnicaDrzava.DataSource = dt;
            cmbPoslovnicaDrzava.DisplayMember = "zemlja";
            cmbPoslovnicaDrzava.ValueMember = "id_zemlja";
            cmbPoslovnicaDrzava.SelectedValue = "60";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmPartnerKronologija kr = new frmPartnerKronologija();
            if (txtSifra.Text != "")
                kr.id_pr = txtSifra.Text;

            kr.ShowDialog();
        }

        private void chbKorisnikPrograma_CheckedChanged(object sender, EventArgs e)
        {
            SpremiPromjeneSaOdrzavanjima();
        }

        private void chbWebUred_CheckedChanged(object sender, EventArgs e)
        {
            SpremiPromjeneSaOdrzavanjima();
        }

        private void chbUgovor_CheckedChanged(object sender, EventArgs e)
        {
            SpremiPromjeneSaOdrzavanjima();
        }

        private void nuBrojOdrzavanja_ValueChanged(object sender, EventArgs e)
        {
            SpremiPromjeneSaOdrzavanjima();
        }

        private void nuBrojVipa_ValueChanged(object sender, EventArgs e)
        {
            SpremiPromjeneSaOdrzavanjima();
        }

        private void chbBivsiKorisnik_CheckedChanged(object sender, EventArgs e)
        {
            SpremiPromjeneSaOdrzavanjima();
        }

        private void SpremiPromjeneSaOdrzavanjima()
        {
            if (!zapocni_uređivanje_odrzavanja)
                return;

            string odrzavanje = "0", broj_odrzavanja = "0", vip = "0", broj_vipa = "0", nas_program = "0", web_ured = "0", ugovor = "0", bivsi_korisnik = "0", tablet = "0", pcpos = "0", pccaffe = "0", resort = "0", godisnje_odr = "0";

            if (nuBrojOdrzavanja.Value > 0) { odrzavanje = "123.75"; broj_odrzavanja = nuBrojOdrzavanja.Value.ToString(); }
            if (nuBrojVipa.Value > 0) { vip = "50"; broj_vipa = nuBrojVipa.Value.ToString(); }
            if (chbKorisnikPrograma.Checked) { nas_program = "1"; }
            if (chbWebUred.Checked) { web_ured = "1"; }
            if (chbUgovor.Checked) { ugovor = "1"; }
            if (chbBivsiKorisnik.Checked) { bivsi_korisnik = "1"; }
            if (chbTablet.Checked) { tablet = "1"; }
            if (chbPcPos.Checked) { pcpos = "1"; }
            if (chbCaffe.Checked) { pccaffe = "1"; }
            if (chbResort.Checked) { resort = "1"; }
            if (chbGodisnjeOdr.Checked) { godisnje_odr = "1"; }

            DataTable DTodr = classSQL.select(string.Format("SELECT * FROM partners_odrzavanje WHERE id_partner ='{0}';", txtSifra.Text), "partners_odrzavanje").Tables[0];
            if (DTodr.Rows.Count > 0)
            {
                classSQL.update(string.Format(@"UPDATE partners_odrzavanje
SET
    odrzavanje_kol = '{0}',
    internet_kol = '{1}',
    nas_program = '{2}',
    web_ured = '{3}',
    ugovor = '{4}',
    bivsi_korisnik = '{5}',
    tablet = '{6}',
    pcpos = '{7}',
    pccaffe = '{8}',
    godisnje_odr = '{9}',
    resort = '{10}'
WHERE id_partner = '{11}';",
    broj_odrzavanja,
    broj_vipa,
    nas_program,
    web_ured,
    ugovor,
    bivsi_korisnik,
    tablet,
    pcpos,
    pccaffe,
    godisnje_odr,
    resort,
    txtSifra.Text));
            }
            else
            {
                classSQL.insert(string.Format(@"INSERT INTO partners_odrzavanje
(
    internet, internet_kol, odrzavanje, odrzavanje_kol, nas_program, web_ured, ugovor, id_partner, godisnje_odr, bivsi_korisnik, tablet, pcpos, pccaffe, resort
)
VALUES
(
    '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}'
);",
    vip,
    broj_vipa,
    odrzavanje,
    broj_odrzavanja,
    nas_program,
    web_ured,
    ugovor,
    txtSifra.Text,
    godisnje_odr,
    bivsi_korisnik,
    tablet,
    pcpos,
    pccaffe,
    resort));
            }
        }

        private void getPartnerPoslovniceData()
        {
            string sql = string.Format("select * from partner_poslovnice where id_partner = '{0}';", txtSifra.Text);

            DataSet dsPartnerPoslovnice = classSQL.select(sql, "partner_poslovnice");
            if (dsPartnerPoslovnice != null && dsPartnerPoslovnice.Tables.Count > 0 && dsPartnerPoslovnice.Tables[0] != null && dsPartnerPoslovnice.Tables[0].Rows.Count > 0)
            {
                dtPartnerPoslovnice = dsPartnerPoslovnice.Tables[0];

                cmbPoslovnicaPartner.DataSource = dtPartnerPoslovnice;
                cmbPoslovnicaPartner.DisplayMember = "naziv";
                cmbPoslovnicaPartner.ValueMember = "id_partner_poslovnica";
                cmbPoslovnicaDrzava.SelectedValue = dtPartnerPoslovnice.Rows[0]["id_drzava"];

                txtPoslovnicaId.Text = dtPartnerPoslovnice.Rows[0]["id_partner_poslovnica"].ToString();
                txtPoslovnicaNaziv.Text = dtPartnerPoslovnice.Rows[0]["naziv"].ToString();
                txtPoslovnicaAdresa.Text = dtPartnerPoslovnice.Rows[0]["adresa"].ToString();
                txtPoslovnicaOib.Text = dtPartnerPoslovnice.Rows[0]["oib"].ToString();

                getCmbGradPoslovnice(null);
                cmbPoslovnicaGrad.SelectedValue = dtPartnerPoslovnice.Rows[0]["id_grad"];
            }
            else
            {
                cmbPoslovnicaPartner.DataSource = null;
                cmbPoslovnicaPartner.Items.Clear();
                getCmbDrzavaPoslovnice(null);
                getCmbGradPoslovnice(null);
                getNewPoslovnica();
            }
        }

        private void getNewPoslovnica()
        {
            txtPoslovnicaId.Text = "0";
            txtPoslovnicaNaziv.Text = "";
            txtPoslovnicaAdresa.Text = "";
            txtPoslovnicaOib.Text = "";
        }

        private void btnPoslovnicaBrisi_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Želite obrisati poslovnicu partnera?", "Brisanje poslovnice partnera", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (cmbPoslovnicaPartner.SelectedValue != null)
                    {
                        string sql = string.Format("delete from partner_poslovnice where id_partner_poslovnica = '{0}';", cmbPoslovnicaPartner.SelectedValue);
                        classSQL.delete(sql);
                        getPartnerPoslovniceData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPoslovnicaNoviUnos_Click_1(object sender, EventArgs e)
        {
            try
            {
                getNewPoslovnica();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPoslovnicaOdustani_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("select * from partner_poslovnice where id_partner_poslovnica = '{0}';", cmbPoslovnicaPartner.SelectedValue);
                DataSet dsPartnerPoslovnica = null;
                if (cmbPoslovnicaPartner.SelectedValue != null && cmbPoslovnicaPartner.Items.Count > 0 && (dsPartnerPoslovnica = classSQL.select(sql, "partner_poslovnica")) != null)
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPoslovnicaSpremi_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("select * from partners where id_partner = '{0}';", txtSifra.Text);
                DataSet dsPartners = classSQL.select(sql, "partners");

                if (dsPartners != null && dsPartners.Tables.Count > 0 && dsPartners.Tables[0] != null && dsPartners.Tables[0].Rows.Count > 0)
                {
                    DataTable dtPartners = dsPartners.Tables[0];
                    if (Convert.ToInt32(txtPoslovnicaId.Text) == 0)
                    {
                        sql = string.Format(@"INSERT INTO partner_poslovnice
(
    id_partner, naziv, adresa, id_drzava, id_grad, oib
)
VALUES
(
    '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'
);",
    txtSifra.Text,
    txtPoslovnicaNaziv.Text,
    txtPoslovnicaAdresa.Text,
    cmbPoslovnicaDrzava.SelectedValue,
    cmbPoslovnicaGrad.SelectedValue,
    txtPoslovnicaOib.Text);

                        classSQL.insert(sql);
                    }
                    else
                    {
                        sql = string.Format(@"UPDATE partner_poslovnice
SET
    id_partner = '{0}',
    naziv = '{1}',
    adresa = '{2}',
    id_drzava = '{3}',
    id_grad = '{4}',
    oib = '{5}'
WHERE id_partner_poslovnica = '{6}';",
    txtSifra.Text,
    txtPoslovnicaNaziv.Text,
    txtPoslovnicaAdresa.Text,
    cmbPoslovnicaDrzava.SelectedValue,
    cmbPoslovnicaGrad.SelectedValue,
    txtPoslovnicaOib.Text,
    txtPoslovnicaId.Text);

                        classSQL.update(sql);
                    }
                    getPartnerPoslovniceData();
                }
                else
                {
                    MessageBox.Show("Nije odabran partner.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbPoslovnicaPartner_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("SELECT * FROM partner_poslovnice WHERE id_partner_poslovnica = '{0}';", cmbPoslovnicaPartner.SelectedValue);
                DataSet dsPoslovnicaPartner = classSQL.select(sql, "partner_poslovnica");
                if (dsPoslovnicaPartner != null)
                {
                    getCmbDrzavaPoslovnice(null);
                    getCmbGradPoslovnice(null);
                    cmbPoslovnicaDrzava.SelectedValue = dsPoslovnicaPartner.Tables[0].Rows[0]["id_drzava"];
                    cmbPoslovnicaGrad.SelectedValue = dsPoslovnicaPartner.Tables[0].Rows[0]["id_grad"];

                    txtPoslovnicaNaziv.Text = dsPoslovnicaPartner.Tables[0].Rows[0]["naziv"].ToString();
                    txtPoslovnicaAdresa.Text = dsPoslovnicaPartner.Tables[0].Rows[0]["adresa"].ToString();
                    txtPoslovnicaOib.Text = dsPoslovnicaPartner.Tables[0].Rows[0]["oib"].ToString();
                    txtPoslovnicaId.Text = dsPoslovnicaPartner.Tables[0].Rows[0]["id_partner_poslovnica"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}