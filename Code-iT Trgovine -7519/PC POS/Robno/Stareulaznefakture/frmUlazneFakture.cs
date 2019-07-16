using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Robno.Stareulaznefakture
{
    public partial class frmUlazneFakture : Form
    {
        public frmUlazneFakture()
        {
            InitializeComponent();
        }

        public string broj_fakture_edit { get; set; }
        private bool edit = false;
        public frmMenu MainForm { get; set; }

        private void frmUlazneFakture_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Form1_Paint);

            EnableDisable(false, true);
            ControlDisableEnable(1, 0, 0, 1, 0);
            ttxBrojFakture.Text = brojUFA();
            if (broj_fakture_edit != null) { Fill(); }
            fillComboBox();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 1000);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void fillComboBox()
        {
            nu1.Value = 0;
            nu2.Value = 10;
            nu3.Value = 25;
            nu1moze.Value = 10;
            nuNeMoze1.Value = 10;
            nuMoze2.Value = 25;
            nuNeMoze2.Value = 25;

            nmGodinaFakture.Value = DateTime.Now.Year;

            //fill nacin_placanja
            DataSet DSnazivPlacanja = classSQL.select("SELECT * FROM nacin_placanja", "nacin_placanja");
            cbNacinPlacanja.DataSource = DSnazivPlacanja.Tables[0];
            cbNacinPlacanja.DisplayMember = "naziv_placanja";
            cbNacinPlacanja.ValueMember = "id_placanje";
            cbNacinPlacanja.SelectedValue = 3;
            ;

            //DS Valuta
            DataSet DSValuta = classSQL.select("SELECT * FROM valute", "valute");
            cbValuta.DataSource = DSValuta.Tables[0];
            cbValuta.DisplayMember = "ime_valute";
            cbValuta.ValueMember = "id_valuta";
            cbValuta.SelectedValue = 5;
            txtTecaj.DataBindings.Add("Text", DSValuta.Tables[0], "tecaj");
            txtTecaj.Text = "1";

            //fill vrsta dokumenta
            DataSet DSvd = classSQL.select("SELECT * FROM fakture_vd WHERE grupa='fak' ORDER BY id_vd", "fakture_vd");
            cbVD.DataSource = DSvd.Tables[0];
            cbVD.DisplayMember = "vd";
            cbVD.ValueMember = "id_vd";

            //fill ziro_racun
            DataSet DSzr1 = classSQL.select("SELECT * FROM ziro_racun", "ziro_racun");
            cbSziroRacuna.DataSource = DSzr1.Tables[0];
            cbSziroRacuna.DisplayMember = "ziroracun";
            cbSziroRacuna.ValueMember = "id_ziroracun";

            //fill pdv
            DataSet DSporez = classSQL.select("SELECT * FROM porezi", "porezi");
            cbStopaPdva.DataSource = DSporez.Tables[0];
            cbStopaPdva.DisplayMember = "naziv";
            cbStopaPdva.ValueMember = "iznos";

            //fill tko je prijavljen
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private decimal tecaj = 0;

        private void izracun()
        {
            decimal dec_parse;
            if (!Decimal.TryParse(txtTecaj.Text, out dec_parse))
            {
                MessageBox.Show("Greška kod unosa tečaja.", "Greška");
            }
            else { tecaj = Convert.ToDecimal(txtTecaj.Text); }
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
                            dtpDatumKnjizenja.Select();
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtSifraOdrediste.Select();
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
                    dtpDatumKnjizenja.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void btnPartner_Click(object sender, EventArgs e)
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

        private void ttxBrojFakture_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT broj FROM ufa WHERE godina='" + nmGodinaFakture.Value.ToString() + "' AND broj='" + ttxBrojFakture.Text + "'", "ufa").Tables[0];
                deleteFields();
                if (DT.Rows.Count == 0)
                {
                    if (brojUFA() == ttxBrojFakture.Text.Trim())
                    {
                        deleteFields();
                        edit = false;
                        EnableDisable(true, false);
                        btnSveFakture.Enabled = false;
                        //ttxBrojFakture.Text = brojFakture();
                        btnDeleteAllFaktura.Enabled = false;
                        txtSifraOdrediste.Select();
                        cbVD.Select();
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    broj_fakture_edit = ttxBrojFakture.Text;
                    EnableDisable(true, false);
                    edit = true;
                    Fill();
                    btnDeleteAllFaktura.Enabled = true;
                    txtSifraOdrediste.Select();
                    cbVD.Select();
                }
                ControlDisableEnable(0, 1, 1, 0, 1);
            }
        }

        private void deleteFields()
        {
            txtSifraOdrediste.Text = "";
            txtPartnerNaziv.Text = "";
            txtModel.Text = "";
            rtbNapomena.Text = "";
            txtUkupno.Text = "0";
            txtPolaznaStavka.Text = "0";
            txtNeoporeziveStavke.Text = "0";
            txtOsnovica1.Text = "0";
            txtOsnovica2.Text = "0";
            txtOsnovica3.Text = "0";
            txtMozeSeOdbiti1.Text = "0";
            txtMozeSeOdbiti2.Text = "0";
            txtNeMozeSeOdbiti.Text = "0";
            txtNeMozeSeOdbiti2.Text = "0";
        }

        private void EnableDisable(bool x, bool y)
        {
            ttxBrojFakture.Enabled = y;
            nmGodinaFakture.Enabled = y;

            txtSifraOdrediste.Enabled = x;
            txtPartnerNaziv.Enabled = x;
            txtModel.Enabled = x;
            rtbNapomena.Enabled = x;
            txtUkupno.Enabled = x;
            txtPolaznaStavka.Enabled = x;
            txtNeoporeziveStavke.Enabled = x;
            txtOsnovica1.Enabled = x;
            txtOsnovica2.Enabled = x;
            txtOsnovica3.Enabled = x;
            txtMozeSeOdbiti1.Enabled = x;
            txtMozeSeOdbiti2.Enabled = x;
            txtNeMozeSeOdbiti.Enabled = x;
            txtNeMozeSeOdbiti2.Enabled = x;
            txtUkupnoValuta.Enabled = x;
            txtTecaj.Enabled = x;
            txtDana.Enabled = x;

            cbNacinPlacanja.Enabled = x;
            //cbNaZiroRacun.Enabled = x;
            cbStopaPdva.Enabled = x;
            cbSziroRacuna.Enabled = x;
            cbValuta.Enabled = x;
            cbVD.Enabled = x;

            _txtMozeSeOdbiti1.Enabled = x;
            _txtMozeSeOdbiti2.Enabled = x;
            _txtNeMozeSeOdbiti.Enabled = x;
            _txtNeMozeSeOdbiti2.Enabled = x;
            _txtNeoporeziveStavke.Enabled = x;
            _txtOsnovica1.Enabled = x;
            _txtOsnovica2.Enabled = x;
            _txtOsnovica3.Enabled = x;
            _txtPolaznaStavka.Enabled = x;

            dtpDanaValuta.Enabled = x;
            dtpDatumDVO.Enabled = x;
            dtpDatumKnjizenja.Enabled = x;
        }

        private void Fill()
        {
            DataTable DTfill = classSQL.select("SELECT * FROM ufa WHERE broj='" + broj_fakture_edit + "'", "ufa").Tables[0];

            if (DTfill.Rows.Count > 0)
            {
                edit = true;

                ttxBrojFakture.Text = DTfill.Rows[0]["broj"].ToString();
                nmGodinaFakture.Value = Convert.ToInt16(DTfill.Rows[0]["godina"].ToString());

                txtSifraOdrediste.Text = DTfill.Rows[0]["odrediste"].ToString();
                DataTable DTpartner = classSQL.select("SELECT * FROM partners WHERE id_partner='" + DTfill.Rows[0]["odrediste"].ToString() + "'", "ufa").Tables[0];
                if (DTpartner.Rows.Count > 0)
                {
                    txtPartnerNaziv.Text = DTpartner.Rows[0]["ime_tvrtke"].ToString();
                }
                txtModel.Text = DTfill.Rows[0]["model"].ToString();
                rtbNapomena.Text = DTfill.Rows[0]["napomena"].ToString();
                txtUkupno.Text = DTfill.Rows[0]["ukupno"].ToString();
                txtPolaznaStavka.Text = DTfill.Rows[0]["carina"].ToString(); ;
                txtNeoporeziveStavke.Text = DTfill.Rows[0]["neoporezivo"].ToString();
                txtOsnovica1.Text = DTfill.Rows[0]["osnovica1"].ToString();
                txtOsnovica2.Text = DTfill.Rows[0]["osnovica2"].ToString();
                txtOsnovica3.Text = DTfill.Rows[0]["osnovica3"].ToString();
                txtMozeSeOdbiti1.Text = DTfill.Rows[0]["odbitak1"].ToString();
                txtMozeSeOdbiti2.Text = DTfill.Rows[0]["odbitak3"].ToString();
                txtNeMozeSeOdbiti.Text = DTfill.Rows[0]["odbitak2"].ToString();
                txtNeMozeSeOdbiti2.Text = DTfill.Rows[0]["odbitak4"].ToString();
                txtTecaj.Text = DTfill.Rows[0]["valuta"].ToString();

                cbNacinPlacanja.SelectedValue = DTfill.Rows[0]["id_nacin_placanja"].ToString();
                //cbNaZiroRacun.SelectedValue = DTfill.Rows[0]["na_zr"].ToString();
                cbStopaPdva.SelectedValue = DTfill.Rows[0]["id_pdv"].ToString(); ;
                cbSziroRacuna.SelectedValue = DTfill.Rows[0]["sa_zr"].ToString();
                cbValuta.SelectedValue = DTfill.Rows[0]["id_valuta"].ToString();
                cbVD.SelectedValue = DTfill.Rows[0]["id_vd"].ToString();

                dtpDanaValuta.Value = Convert.ToDateTime(DTfill.Rows[0]["datum_valute"].ToString());
                dtpDatumDVO.Value = Convert.ToDateTime(DTfill.Rows[0]["datum_dvo"].ToString());
                dtpDatumKnjizenja.Value = Convert.ToDateTime(DTfill.Rows[0]["datum_knjizenja"].ToString());

                nu1.Value = Convert.ToInt16(DTfill.Rows[0]["nu1"].ToString());
                nu2.Value = Convert.ToInt16(DTfill.Rows[0]["nu2"].ToString());
                nu3.Value = Convert.ToInt16(DTfill.Rows[0]["nu3"].ToString());
                nu1moze.Value = Convert.ToInt16(DTfill.Rows[0]["nu4"].ToString());
                nuNeMoze1.Value = Convert.ToInt16(DTfill.Rows[0]["nu5"].ToString());
                nuMoze2.Value = Convert.ToInt16(DTfill.Rows[0]["nu6"].ToString());
                nuNeMoze2.Value = Convert.ToInt16(DTfill.Rows[0]["nu7"].ToString());

                ControlDisableEnable(0, 1, 1, 0, 1);
                EnableDisable(true, false);
                izracun();
            }
        }

        private string brojUFA()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj) FROM ufa", "ufa").Tables[0];
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

        private void cbVD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (sender is TextBox)
                {
                    TextBox txt = ((TextBox)sender);
                    txt.BackColor = Color.White;

                    if (txt.Name == "txtDana")
                    {
                        if ((sender as TextBox).Text == "" || (sender as TextBox).Text.Substring(0, 1) == ",")
                        {
                            (sender as TextBox).Text = "0";
                            dtpDanaValuta.Select();
                        }

                        try
                        {
                            DateTime dvo = dtpDatumDVO.Value;
                            dtpDanaValuta.Value = dvo.AddDays(Convert.ToInt16(txtDana.Text)); ;
                            dtpDanaValuta.Select();
                            return;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Krivi upis.");
                        }
                    }
                }

                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            decimal dec_parse;
            if (!Decimal.TryParse(txtSifraOdrediste.Text, out dec_parse))
            {
                MessageBox.Show("Greška kod upisa odredišta.", "Greška"); return;
            }

            if (edit == false)
            {
                string sql = "INSERT INTO ufa (" +
                    " broj,datum_knjizenja,datum_valute,datum_dvo,id_zaposlenik,model,valuta,id_valuta,id_pdv,na_zr,sa_zr,napomena,odrediste,godina,id_nacin_placanja,ukupno," +
                    " carina,neoporezivo,osnovica1,osnovica2,osnovica3,odbitak1,odbitak2,odbitak3,odbitak4,nu1,nu2,nu3,nu4,nu5,nu6,nu7,id_vd" +
                    ") VALUES (" +
                    "'" + ttxBrojFakture.Text + "'," +
                    "'" + dtpDatumKnjizenja.Value + "'," +
                    "'" + dtpDanaValuta.Value + "'," +
                    "'" + dtpDatumDVO.Value + "'," +
                    "'" + Properties.Settings.Default.id_zaposlenik + "'," +
                    "'" + txtModel.Text + "'," +
                    "'" + txtTecaj.Text + "'," +
                    "'" + cbValuta.SelectedValue + "'," +
                    "'" + cbStopaPdva.SelectedValue + "'," +
                    //"'" + cbNaZiroRacun.SelectedValue + "'," +
                    "'" + cbSziroRacuna.SelectedValue + "'," +
                    "'" + rtbNapomena.Text + "'," +
                    "'" + txtSifraOdrediste.Text + "'," +
                    "'" + nmGodinaFakture.Value.ToString() + "'," +
                    "'" + cbNacinPlacanja.SelectedValue + "'," +
                    "'" + txtUkupno.Text.Replace(",", ".") + "'," +
                    "'" + txtPolaznaStavka.Text.Replace(",", ".") + "'," +
                    "'" + txtNeoporeziveStavke.Text.Replace(",", ".") + "'," +
                    "'" + txtOsnovica1.Text.Replace(",", ".") + "'," +
                    "'" + txtOsnovica2.Text.Replace(",", ".") + "'," +
                    "'" + txtOsnovica3.Text.Replace(",", ".") + "'," +
                    "'" + txtMozeSeOdbiti1.Text.Replace(",", ".") + "'," +
                    "'" + txtNeMozeSeOdbiti.Text.Replace(",", ".") + "'," +
                    "'" + txtMozeSeOdbiti2.Text.Replace(",", ".") + "'," +
                    "'" + txtNeMozeSeOdbiti2.Text.Replace(",", ".") + "'," +
                    "'" + nu1.Value + "'," +
                    "'" + nu2.Value + "'," +
                    "'" + nu3.Value + "'," +
                    "'" + nu1moze.Value + "'," +
                    "'" + nuNeMoze1.Value + "'," +
                    "'" + nuMoze2.Value + "'," +
                    "'" + nuNeMoze2.Value + "'," +
                    "'" + cbVD.SelectedValue + "'" +
                    ")";

                provjera_sql(classSQL.insert(sql));
            }
            else
            {
                string sql = "UPDATE ufa SET " +
                    "broj='" + ttxBrojFakture.Text + "'," +
                    "datum_knjizenja='" + dtpDatumKnjizenja.Value + "'," +
                    "datum_valute='" + dtpDanaValuta.Value + "'," +
                    "datum_dvo='" + dtpDatumDVO.Value + "'," +
                    "id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'," +
                    "model='" + txtModel.Text + "'," +
                    "valuta='" + txtTecaj.Text + "'," +
                    "id_valuta='" + cbValuta.SelectedValue + "'," +
                    "id_pdv='" + cbStopaPdva.SelectedValue + "'," +
                    //"na_zr='" + cbNaZiroRacun.SelectedValue + "'," +
                    "sa_zr='" + cbSziroRacuna.SelectedValue + "'," +
                    "napomena='" + rtbNapomena.Text + "'," +
                    "odrediste='" + txtSifraOdrediste.Text + "'," +
                    "godina='" + nmGodinaFakture.Value.ToString() + "'," +
                    "id_nacin_placanja='" + cbNacinPlacanja.SelectedValue + "'," +
                    "ukupno='" + txtUkupno.Text.Replace(",", ".") + "'," +
                    "carina='" + txtPolaznaStavka.Text.Replace(",", ".") + "'," +
                    "neoporezivo='" + txtNeoporeziveStavke.Text.Replace(",", ".") + "'," +
                    "osnovica1='" + txtOsnovica1.Text.Replace(",", ".") + "'," +
                    "osnovica2='" + txtOsnovica2.Text.Replace(",", ".") + "'," +
                    "osnovica3='" + txtOsnovica3.Text.Replace(",", ".") + "'," +
                    "odbitak1='" + txtMozeSeOdbiti1.Text.Replace(",", ".") + "'," +
                    "odbitak2='" + txtNeMozeSeOdbiti.Text.Replace(",", ".") + "'," +
                    "odbitak3='" + txtMozeSeOdbiti2.Text.Replace(",", ".") + "'," +
                    "odbitak4='" + txtNeMozeSeOdbiti2.Text.Replace(",", ".") + "'," +
                    "nu1='" + nu1.Value + "'," +
                    "nu2='" + nu2.Value + "'," +
                    "nu3='" + nu3.Value + "'," +
                    "nu4='" + nu1moze.Value + "'," +
                    "nu5='" + nuNeMoze1.Value + "'," +
                    "nu6='" + nuMoze2.Value + "'," +
                    "nu7='" + nuNeMoze2.Value + "'," +
                    "id_vd='" + cbVD.SelectedValue + "'" +
                    " WHERE broj='" + ttxBrojFakture.Text + "'";

                provjera_sql(classSQL.update(sql));
            }

            deleteFields();
            EnableDisable(false, true);
            ControlDisableEnable(1, 0, 0, 1, 0);
            MessageBox.Show("Spremljeno.", "Spremljeno.");
            edit = false;
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            btnSveFakture.Enabled = true;
            EnableDisable(false, true);
            deleteFields();
            ttxBrojFakture.Text = brojUFA();
            edit = false;
            ttxBrojFakture.ReadOnly = false;
            nmGodinaFakture.ReadOnly = false;

            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private void btnDeleteAllFaktura_Click(object sender, EventArgs e)
        {
            classSQL.delete("DELETE FROM ufa WHERE broj='" + ttxBrojFakture.Text + "'");
            edit = false;
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            frmSveUlazneFakture Fu = new frmSveUlazneFakture();
            //Fu.MainForm = this;
            Fu.ShowDialog();
            if (broj_fakture_edit != null)
            {
                deleteFields();
                Fill();
            }
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            edit = false;
            EnableDisable(true, false);
            ttxBrojFakture.Text = brojUFA(); ;
            ControlDisableEnable(0, 1, 1, 0, 1);
            txtSifraOdrediste.Select();
        }
    }
}