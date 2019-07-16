using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class Izlracuni : Form
    {
        private bool isFaktura = true;

        public Izlracuni()
        {
            InitializeComponent();
            this.groupBox1.Click += new System.EventHandler(this.groupBox_Click);
            this.groupBox2.Click += new System.EventHandler(this.groupBox_Click);
            this.groupBox3.Click += new System.EventHandler(this.groupBox_Click);
        }

        private void Izlracuni_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            label5.Enabled = checkBox2.Checked;
            label4.Enabled = checkBox2.Checked;
            dateTimePicker1.Enabled = checkBox2.Checked;
            dateTimePicker2.Enabled = checkBox2.Checked;
            Set();
        }

        private void Set()
        {
            DataTable DTSK = new DataTable("Izlracuni");

            DTSK.Columns.Add("id", typeof(string));
            DTSK.Columns.Add("naziv", typeof(string));

            DTSK.Rows.Add("kalk", "Kalkulacije");
            DTSK.Rows.Add("mpra", "Maloprodajni računi");
            DTSK.Rows.Add("otpr", "Otpremnice");
            DTSK.Rows.Add("fakt", "Fakture");
            DTSK.Rows.Add("ifb", "Fakture bez robe");
            DTSK.Rows.Add("pov_dob", "Povratnice dobavljaču");
            DTSK.Rows.Add("dobfak", "Dobit po fakturama");
            DTSK.Rows.Add("dobmpra", "Dobit po maloprodajnim računima");
            DTSK.Rows.Add("rac_za_avans", "Račun za avans");

            cmbDokument.DataSource = DTSK;

            cmbDokument.DisplayMember = "naziv";

            cmbDokument.ValueMember = "id";

            getSkladiste();
        }

        private void getSkladiste()
        {
            try
            {
                if ((cmbDokument.SelectedValue.ToString() != "fakt" || Util.Korisno.oibTvrtke != Class.Postavke.OIB_PC1) && cmbDokument.SelectedValue.ToString() != "rac_za_avans")
                {
                    if (isFaktura)
                    {
                        DataTable DTSK1 = new DataTable("Izlracuni1");

                        string sql = "SELECT * FROM skladiste";
                        DTSK1 = classSQL.select(sql, "skl").Tables[0];

                        comboBox2.DataSource = DTSK1;
                        comboBox2.DisplayMember = "skladiste";
                        comboBox2.ValueMember = "id_skladiste";
                        isFaktura = false;
                    }
                }
                else if ((cmbDokument.SelectedValue.ToString() == "fakt" && Util.Korisno.oibTvrtke == Class.Postavke.OIB_PC1) || cmbDokument.SelectedValue.ToString() == "rac_za_avans")
                {
                    if (!isFaktura)
                    {
                        DataTable dtPoslovnice = new DataTable();
                        string sql = "select * from ducan;";
                        dtPoslovnice = classSQL.select(sql, "ducani").Tables[0];
                        comboBox2.DataSource = dtPoslovnice;
                        comboBox2.DisplayMember = "ime_ducana";
                        comboBox2.ValueMember = "id_ducan";
                        isFaktura = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                int dec_parse;
                if (int.TryParse(txtODRac.Text, out dec_parse))
                {
                    e.SuppressKeyPress = true;
                    System.Windows.Forms.SendKeys.Send("{TAB}");
                }
                else
                {
                    MessageBox.Show("Nije broj");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int partner = 0;
            int.TryParse(txtPartner.Text, out partner);
            if (checkBox1.Checked)
            {
                if (txtODRac.Text != "" && txtDORac.Text != "")
                {
                    if (cmbDokument.SelectedValue.ToString() != "kalk" && cmbDokument.SelectedValue.ToString() != "dobmpra" && cmbDokument.SelectedValue.ToString() != "pov_dob" && cmbDokument.SelectedValue.ToString() != "dobfak")
                    {
                        Report.lzlazniRacuni.frmListe3 lista = new Report.lzlazniRacuni.frmListe3();
                        //koristi varijablu skladiste za ducane kod faktura za Code-iT

                        if (cbsklkalk.Checked && Util.Korisno.oibTvrtke == Class.Postavke.OIB_PC1)
                        {
                            lista.skladiste = comboBox2.SelectedValue.ToString();
                        }
                        if (cmbDokument.SelectedValue.ToString() == "fakt")
                        {
                            lista.pouzecem = chbSamoPouzecem.Checked;
                        }

                        lista.partner = partner;
                        lista.BrojFakDO = txtDORac.Text;
                        lista.documenat = cmbDokument.SelectedValue.ToString();
                        lista.prema_rac = checkBox1.Checked;
                        lista.BrojFakOD = txtODRac.Text;
                        lista.datumOD = dateTimePicker1.Value.Date;
                        lista.datumDO = dateTimePicker2.Value.Date;
                        lista.ShowDialog();
                    }
                    else if (cmbDokument.SelectedValue.ToString() == "kalk")
                    {
                        if (cbsklkalk.Checked != true)
                        {
                            Report.Kalkposkl.frmKalkposkl lista = new Report.Kalkposkl.frmKalkposkl();
                            lista.documenat = cmbDokument.SelectedValue.ToString();
                            lista.prema_rac = checkBox1.Checked;
                            lista.bool1 = cbsklkalk.Checked;
                            lista.skladiste_odabir = comboBox2.SelectedValue.ToString();
                            lista.skladiste = comboBox2.Text;
                            lista.BrojFakOD = txtODRac.Text;
                            lista.BrojFakDO = txtDORac.Text;
                            lista.datumOD = dateTimePicker1.Value.Date;
                            lista.datumDO = dateTimePicker2.Value.Date;
                            lista.ShowDialog();
                        }
                        else
                        {
                            Report.Kalkposkl.frmKalkposkl lista = new Report.Kalkposkl.frmKalkposkl();
                            lista.documenat = cmbDokument.SelectedValue.ToString();
                            lista.prema_rac = checkBox1.Checked;
                            lista.bool1 = cbsklkalk.Checked;
                            lista.skladiste_odabir = comboBox2.SelectedValue.ToString();
                            lista.skladiste = comboBox2.Text;
                            lista.BrojFakOD = txtODRac.Text;
                            lista.BrojFakDO = txtDORac.Text;
                            lista.datumOD = dateTimePicker1.Value.Date;
                            lista.datumDO = dateTimePicker2.Value.Date;
                            lista.ShowDialog();
                        }
                    }
                    else if (cmbDokument.SelectedValue.ToString() == "dobmpra" || cmbDokument.SelectedValue.ToString() == "dobfak")
                    {
                        Report.Dobit.ListeDobit lista = new Report.Dobit.ListeDobit();
                        lista.datumOD = dateTimePicker1.Value.Date;
                        lista.prema_rac = checkBox1.Checked;
                        lista.documenat = cmbDokument.SelectedValue.ToString();
                        lista.datumDO = dateTimePicker2.Value.Date;
                        lista.BrojFakOD = txtODRac.Text;
                        lista.BrojFakDO = txtDORac.Text;
                        lista.ShowDialog();
                    }
                    else if (cmbDokument.SelectedValue.ToString() == "pov_dob")
                    {
                        if (cbsklkalk.Checked != true)
                        {
                            Report.Povratniceposkl.frmPovposkl lista = new Report.Povratniceposkl.frmPovposkl();
                            lista.BrojFakDO = txtDORac.Text;
                            lista.documenat = cmbDokument.SelectedValue.ToString();
                            lista.prema_rac = checkBox1.Checked;
                            lista.bool1 = cbsklkalk.Checked;
                            lista.skladiste_odabir = comboBox2.SelectedValue.ToString();
                            lista.skladiste = comboBox2.Text;
                            lista.BrojFakOD = txtODRac.Text;
                            lista.datumOD = dateTimePicker1.Value.Date;
                            lista.datumDO = dateTimePicker2.Value.Date;
                            lista.ShowDialog();
                        }
                        else
                        {
                            Report.Povratniceposkl.frmPovposkl lista = new Report.Povratniceposkl.frmPovposkl();
                            lista.BrojFakDO = txtDORac.Text;
                            lista.documenat = cmbDokument.SelectedValue.ToString();
                            lista.prema_rac = checkBox1.Checked;
                            lista.bool1 = cbsklkalk.Checked;
                            lista.skladiste_odabir = comboBox2.SelectedValue.ToString();
                            lista.skladiste = comboBox2.Text;
                            lista.BrojFakOD = txtODRac.Text;
                            lista.datumOD = dateTimePicker1.Value.Date;
                            lista.datumDO = dateTimePicker2.Value.Date;
                            lista.ShowDialog();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Niste popunili potrebne kriterije za ispis ");
                    return;
                }
            }

            if (checkBox2.Checked)
            {
                if (cmbDokument.SelectedValue.ToString() != "kalk" && cmbDokument.SelectedValue.ToString() != "pov_dob" && cmbDokument.SelectedValue.ToString() != "dobmpra" && cmbDokument.SelectedValue.ToString() != "dobfak")
                {
                    Report.lzlazniRacuni.frmListe3 lista = new Report.lzlazniRacuni.frmListe3();
                    //koristin varijablu skladiste za ducane kod faktura za Code-iT
                    if (cbsklkalk.Checked && Util.Korisno.oibTvrtke == Class.Postavke.OIB_PC1)
                    {
                        lista.skladiste = comboBox2.SelectedValue.ToString();
                    }
                    else
                    {
                        //lista.skladiste = comboBox2.SelectedValue.ToString();
                    }
                    if (cmbDokument.SelectedValue.ToString() == "fakt")
                    {
                        lista.pouzecem = chbSamoPouzecem.Checked;
                    }
                    lista.partner = partner;
                    lista.BrojFakDO = txtDORac.Text;
                    lista.documenat = cmbDokument.SelectedValue.ToString();
                    lista.prema_rac = checkBox1.Checked;
                    lista.BrojFakOD = txtODRac.Text;

                    lista.datumOD = dateTimePicker1.Value.Date;
                    lista.datumDO = dateTimePicker2.Value.Date;
                    lista.ShowDialog();
                }
                else if (cmbDokument.SelectedValue.ToString() == "kalk")
                {
                    if (cbsklkalk.Checked != true)
                    {
                        Report.Kalkposkl.frmKalkposkl lista = new Report.Kalkposkl.frmKalkposkl();
                        lista.BrojFakDO = txtDORac.Text;
                        lista.documenat = cmbDokument.SelectedValue.ToString();
                        lista.prema_rac = checkBox1.Checked;
                        lista.bool1 = cbsklkalk.Checked;
                        lista.skladiste_odabir = comboBox2.SelectedValue.ToString();
                        lista.skladiste = comboBox2.Text;
                        lista.BrojFakOD = txtODRac.Text;
                        lista.datumOD = dateTimePicker1.Value.Date;
                        lista.datumDO = dateTimePicker2.Value.Date;
                        lista.ShowDialog();
                    }
                    else
                    {
                        Report.Kalkposkl.frmKalkposkl lista = new Report.Kalkposkl.frmKalkposkl();
                        lista.BrojFakDO = txtDORac.Text;
                        lista.documenat = cmbDokument.SelectedValue.ToString();
                        lista.prema_rac = checkBox1.Checked;
                        lista.bool1 = cbsklkalk.Checked;
                        lista.skladiste_odabir = comboBox2.SelectedValue.ToString();
                        lista.skladiste = comboBox2.Text;
                        lista.BrojFakOD = txtODRac.Text;
                        lista.datumOD = dateTimePicker1.Value.Date;
                        lista.datumDO = dateTimePicker2.Value.Date;
                        lista.ShowDialog();
                    }
                }
                else if (cmbDokument.SelectedValue.ToString() == "dobmpra" || cmbDokument.SelectedValue.ToString() == "dobfak")
                {
                    Report.Dobit.ListeDobit lista = new Report.Dobit.ListeDobit();
                    lista.datumOD = dateTimePicker1.Value.Date;
                    lista.prema_rac = checkBox1.Checked;
                    lista.documenat = cmbDokument.SelectedValue.ToString();
                    lista.datumDO = dateTimePicker2.Value.Date;
                    lista.BrojFakOD = txtODRac.Text;
                    lista.BrojFakDO = txtDORac.Text;
                    lista.ShowDialog();
                }
                else if (cmbDokument.SelectedValue.ToString() == "pov_dob")
                {
                    if (cbsklkalk.Checked != true)
                    {
                        Report.Povratniceposkl.frmPovposkl lista = new Report.Povratniceposkl.frmPovposkl();
                        lista.BrojFakDO = txtDORac.Text;
                        lista.documenat = cmbDokument.SelectedValue.ToString();
                        lista.prema_rac = checkBox1.Checked;
                        lista.bool1 = cbsklkalk.Checked;
                        lista.skladiste_odabir = comboBox2.SelectedValue.ToString();
                        lista.skladiste = comboBox2.Text;
                        lista.BrojFakOD = txtODRac.Text;
                        lista.datumOD = dateTimePicker1.Value.Date;
                        lista.datumDO = dateTimePicker2.Value.Date;
                        lista.ShowDialog();
                    }
                    else
                    {
                        Report.Povratniceposkl.frmPovposkl lista = new Report.Povratniceposkl.frmPovposkl();
                        lista.BrojFakDO = txtDORac.Text;
                        lista.documenat = cmbDokument.SelectedValue.ToString();
                        lista.prema_rac = checkBox1.Checked;
                        lista.bool1 = cbsklkalk.Checked;
                        lista.skladiste_odabir = comboBox2.SelectedValue.ToString();
                        lista.skladiste = comboBox2.Text;
                        lista.BrojFakOD = txtODRac.Text;
                        lista.datumOD = dateTimePicker1.Value.Date;
                        lista.datumDO = dateTimePicker2.Value.Date;
                        lista.ShowDialog();
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
            }

            label2.Enabled = checkBox1.Checked;
            label3.Enabled = checkBox1.Checked;
            label7.Enabled = checkBox1.Checked;
            txtODRac.Enabled = checkBox1.Checked;
            txtDORac.Enabled = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                txtODRac.Text = "";
                txtDORac.Text = "";
            }

            label5.Enabled = checkBox2.Checked;
            label4.Enabled = checkBox2.Checked;
            dateTimePicker1.Enabled = checkBox2.Checked;
            dateTimePicker2.Enabled = checkBox2.Checked;
        }

        private void Izlracuni_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbsklkalk.Visible = true;
            txtPartner.Text = "";
            txtPartnerNaziv.Text = "";
            gbPartner.Visible = false;
            chbSamoPouzecem.Visible = false;

            if (cmbDokument.Text.ToLower() == "fakture")
            {
                gbPartner.Visible = true;
                chbSamoPouzecem.Visible = true;
            }
            cbsklkalk.Checked = false;
            if (cmbDokument.Text.ToLower() == "kalkulacije")
            {
                getSkladiste();
                cbsklkalk.Enabled = true;
                label7.Visible = true;
            }
            else if (cmbDokument.Text.ToLower() == "otpremnice")
            {
                gbPartner.Visible = true;
            }
            else if (cmbDokument.Text.ToLower() == "povratnice dobavljaču")
            {
                getSkladiste();
                cbsklkalk.Enabled = true;
                label7.Visible = false;
            }
            else if ((cmbDokument.SelectedValue.ToString() == "fakt" && Util.Korisno.oibTvrtke == Class.Postavke.OIB_PC1) || cmbDokument.SelectedValue.ToString() == "rac_za_avans")
            {
                cbsklkalk.Enabled = true;
                cbsklkalk.Checked = true;
                comboBox2.Visible = true;
                getSkladiste();
            }
            else
            {
                getSkladiste();
                cbsklkalk.Enabled = false;
                label7.Visible = false;
            }
        }

        private void groupBox_Click(object sender, System.EventArgs e)
        {
            if ((sender as GroupBox).Name == "groupBox1")
            {
                if (!checkBox1.Checked)
                    checkBox1.Checked = true;
            }
            else if ((sender as GroupBox).Name == "groupBox2")
            {
                if (!checkBox2.Checked)
                    checkBox2.Checked = true;
            }
            else
            {
                if (cbsklkalk.Enabled)
                    cbsklkalk.Checked = !cbsklkalk.Checked;
            }
        }

        private void btnSrchPartner_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partner = new frmPartnerTrazi();
            partner.ShowDialog();

            if (Properties.Settings.Default.id_partner != "")
            {
                FillPartner(Properties.Settings.Default.id_partner);
            }
        }

        private void FillPartner(string p)
        {
            DataSet DS = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + p + "'", "partners");
            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0] != null && DS.Tables[0].Rows.Count > 0)
            {
                txtPartner.Text = p;
                txtPartnerNaziv.Text = DS.Tables[0].Rows[0]["ime_tvrtke"].ToString();
            }
            else
            {
                MessageBox.Show("Partner ne postoji.");
            }
        }

        private void txtPartner_TextChanged(object sender, EventArgs e)
        {
            txtPartnerNaziv.Text = "";
        }

        private void txtPartner_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                FillPartner(txtPartner.Text);
            }
        }
    }
}