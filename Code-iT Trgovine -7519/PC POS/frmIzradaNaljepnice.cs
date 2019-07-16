using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmIzradaNaljepnice : Form
    {
        public frmIzradaNaljepnice()
        {
            InitializeComponent();
        }

        private void frmIzradaNaljepnice_Load(object sender, EventArgs e)
        {
            txtOdKal.Select();
            DataTable DSMT = classSQL.select("SELECT * FROM skladiste WHERE aktivnost='DA'", "skladiste").Tables[0];
            cbSklKal.DataSource = DSMT;
            cbSklKal.DisplayMember = "skladiste";
            cbSklKal.ValueMember = "id_skladiste";

            chbMultiItems.Enabled = false;

            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((byte)(105)), ((byte)(170)), ((byte)(197)));
            Color y = System.Drawing.Color.FromArgb(((byte)(40)), ((byte)(109)), ((byte)(135)));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void btnUcitaj_Click(object sender, EventArgs e)
        {
            if (cbkalk.Checked)
            {
                int dec_parse;
                if (!int.TryParse(txtOdKal.Text, out dec_parse))
                {
                    MessageBox.Show("Krivo upisani podaci.", "Greška");
                    this.ActiveControl = txtOdKal;
                    txtOdKal.SelectAll();
                    return;
                }

                if (!int.TryParse(txtDO.Text, out dec_parse))
                {
                    MessageBox.Show("Krivo upisani podaci.", "Greška");
                    this.ActiveControl = txtDO;
                    txtDO.SelectAll();
                    return;
                }
            }
            if (cbMS.Checked)
            {
                int dec_parse;
                if (!int.TryParse(txtMDOd.Text, out dec_parse))
                {
                    MessageBox.Show("Krivo upisani podaci.", "Greška");
                    this.ActiveControl = txtMDOd;
                    txtOdKal.SelectAll();
                    return;
                }

                if (!int.TryParse(txtMDDo.Text, out dec_parse))
                {
                    MessageBox.Show("Krivo upisani podaci.", "Greška");
                    this.ActiveControl = txtMDDo;
                    txtDO.SelectAll();
                    return;
                }
            }
            if (cbPC.Checked)
            {
                int dec_parse;
                if (!int.TryParse(txtPCOd.Text, out dec_parse))
                {
                    MessageBox.Show("Krivo upisani podaci.", "Greška");
                    this.ActiveControl = txtPCOd;
                    txtOdKal.SelectAll();
                    return;
                }

                if (!int.TryParse(txtPCDo.Text, out dec_parse))
                {
                    MessageBox.Show("Krivo upisani podaci.", "Greška");
                    this.ActiveControl = txtPCDo;
                    txtDO.SelectAll();
                    return;
                }
            }
            string sql_liste = "";

            if (!chbMultiItems.Enabled || !chbMultiItems.Checked)
                dgv.Rows.Clear();

            if (cbkalk.Checked)
            {
                sql_liste = string.Format(@"SELECT
kalkulacija_stavke.sifra,
roba.naziv,
roba.jm,
roba.ean,
kalkulacija_stavke.broj,
kalkulacija.godina,
kalkulacija_stavke.kolicina,
kalkulacija_stavke.porez,
kalkulacija_stavke.vpc,
povratna_naknada.iznos,
'KAL' AS [dokument]
FROM kalkulacija_stavke
LEFT JOIN kalkulacija ON kalkulacija_stavke.broj=kalkulacija.broj AND kalkulacija_stavke.id_skladiste=kalkulacija.id_skladiste
LEFT JOIN roba ON kalkulacija_stavke.sifra = roba.sifra
LEFT JOIN povratna_naknada on kalkulacija_stavke.sifra = povratna_naknada.sifra
WHERE kalkulacija_stavke.broj >= '{0}' AND kalkulacija_stavke.broj <= '{1}' AND kalkulacija.id_skladiste = {2}
order by kalkulacija_stavke.id_stavka asc;",
txtOdKal.Text,
txtDO.Text,
cbSklKal.SelectedValue.ToString());
            }
            if (cbMS.Checked)
            {
                sql_liste = sql_liste == "" ? "" : sql_liste + @"
UNION ALL
";
                sql_liste += string.Format(@"SELECT
meduskladisnica_stavke.sifra,
roba.naziv,
roba.jm,
roba.ean,
CAST(meduskladisnica_stavke.broj AS bigint),
meduskladisnica.godina,
meduskladisnica_stavke.kolicina,
meduskladisnica_stavke.pdv AS [porez],
CAST(meduskladisnica_stavke.vpc AS numeric) as vpc,
povratna_naknada.iznos,
'MDS' AS [dokument]
FROM meduskladisnica_stavke
LEFT JOIN meduskladisnica ON meduskladisnica_stavke.broj = meduskladisnica.broj AND meduskladisnica_stavke.iz_skladista = meduskladisnica.id_skladiste_od
LEFT JOIN roba ON meduskladisnica_stavke.sifra = roba.sifra
LEFT JOIN povratna_naknada on meduskladisnica_stavke.sifra = povratna_naknada.sifra
WHERE meduskladisnica_stavke.broj >= '{0}' AND meduskladisnica_stavke.broj <= '{1}' AND meduskladisnica.id_skladiste_do = {2}",
txtMDOd.Text,
txtMDDo.Text,
cbSklKal.SelectedValue.ToString());
            }
            if (cbPC.Checked)
            {
                sql_liste = sql_liste == "" ? "" : sql_liste + @"
UNION ALL
";
                sql_liste += string.Format(@"SELECT
promjena_cijene_stavke.sifra,
roba.naziv,
roba.jm,
roba.ean,
promjena_cijene_stavke.broj,
cast(extract(year from promjena_cijene.date) as varchar) AS [godina],
'0' AS [kolicina],
promjena_cijene_stavke.pdv AS [porez],
(CAST(promjena_cijene_stavke.nova_cijena AS numeric)/(1 zbroj CAST(pdv AS numeric)/100)) as [vpc],
povratna_naknada.iznos,
'PCJ' AS [dokument]
FROM promjena_cijene_stavke
LEFT JOIN promjena_cijene ON promjena_cijene_stavke.broj = promjena_cijene.broj
LEFT JOIN roba ON promjena_cijene_stavke.sifra = roba.sifra
LEFT JOIN povratna_naknada on promjena_cijene_stavke.sifra = povratna_naknada.sifra
WHERE promjena_cijene_stavke.broj >= '{0}' AND promjena_cijene_stavke.broj <= '{1}' AND promjena_cijene.id_skladiste = {2}",
txtPCOd.Text,
txtPCDo.Text,
cbSklKal.SelectedValue.ToString());
            }
            if (cbrucno.Checked)
            {
                if (txtkol.Text == "")
                { txtkol.Text = "1"; }
                sql_liste = string.Format(@"SELECT
roba.sifra,
roba.naziv,
roba.jm,
roba.ean,
'rucno' as broj,
date_part('year', now()) as godina,
'{0}' as kolicina,
roba_prodaja.porez,
roba_prodaja.vpc,
povratna_naknada.iznos,
'ruc' AS [dokument]
FROM roba
LEFT JOIN roba_prodaja ON roba_prodaja.sifra = roba.sifra and roba_prodaja.id_skladiste = {1}
LEFT JOIN povratna_naknada on roba.sifra = povratna_naknada.sifra
WHERE roba.sifra = '{2}'
LIMIT 1; ",
txtkol.Text,
Class.Postavke.id_default_skladiste,
txtartikl.Text);
            }

            DataTable DT = classSQL.select(sql_liste, "kalkulacija").Tables[0];

            decimal vpc = 0;
            decimal porez = 0;
            decimal pov_nak = 0;
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                try
                {
                    porez = Convert.ToDecimal(DT.Rows[i]["porez"].ToString());
                    vpc = Convert.ToDecimal(DT.Rows[i]["vpc"].ToString());
                    decimal.TryParse(DT.Rows[i]["iznos"].ToString(), out pov_nak);
                    if (!Class.Postavke.koristi_povratnu_naknadu)
                        pov_nak = 0;
                }
                catch
                {
                }

                dgv.Rows.Add(i + 1,
                    DT.Rows[i]["broj"].ToString(),
                    DT.Rows[i]["sifra"].ToString(),
                    DT.Rows[i]["naziv"].ToString(),
                    DT.Rows[i]["kolicina"].ToString(),
                    (((vpc * porez / 100) + vpc) + pov_nak).ToString("#0.00"),
                    Math.Round(vpc, 3).ToString("#0.000"),
                    DT.Rows[i]["jm"].ToString(),
                    DT.Rows[i]["godina"].ToString(),
                    DT.Rows[i]["ean"].ToString(),
                    DT.Rows[i]["dokument"].ToString()
                );
            }
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            if (!chbMPC.Checked && !chbVPC.Checked)
            {
                MessageBox.Show("Označite MPC i/ili VPC za ispis!", "Greška"); return;
            }

            int dec_parse;

            if (txtPocetak.Text == "")
            {
                txtPocetak.Text = "1";
                dec_parse = 1;
            }

            if (!int.TryParse(txtPocetak.Text, out dec_parse))
            {
                MessageBox.Show("Krivo upisani podaci.", "Greška"); return;
            }

            DataTable DTSK = new DataTable("skladiste");
            DTSK.Columns.Add("sifra", typeof(string));
            DTSK.Columns.Add("naziv", typeof(string));
            DTSK.Columns.Add("jmj", typeof(string));
            DTSK.Columns.Add("mpc", typeof(string));
            DTSK.Columns.Add("vpc", typeof(string));
            DTSK.Columns.Add("godina", typeof(string));
            DTSK.Columns.Add("broj", typeof(string));
            DTSK.Columns.Add("barcode", typeof(string));
            DTSK.Columns.Add("dokument", typeof(string));

            if (radioButton1.Checked)
            {
                DTSK.Columns.Add("kolicina", typeof(string));

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    double kolicina;
                    try
                    {
                        kolicina = Convert.ToDouble(dgv.Rows[i].Cells["kolicina"].FormattedValue.ToString());
                    }
                    catch
                    {
                        kolicina = 0;
                    }

                    //kolicina = kolicina <= 0 ? 0 : 1;

                    if (kolicina > 0)
                    {
                        for (int z = 0; z < Convert.ToInt16(kolicina); z++)
                        {
                            DataRow DTrow = DTSK.NewRow();
                            DTrow["sifra"] = dgv.Rows[i].Cells["sifra"].FormattedValue.ToString();
                            DTrow["naziv"] = dgv.Rows[i].Cells["naziv"].FormattedValue.ToString();
                            DTrow["jmj"] = dgv.Rows[i].Cells["jmj"].FormattedValue.ToString();
                            DTrow["mpc"] = dgv.Rows[i].Cells["mpc"].FormattedValue.ToString();
                            DTrow["vpc"] = dgv.Rows[i].Cells["vpc"].FormattedValue.ToString();
                            DTrow["godina"] = dgv.Rows[i].Cells["godina"].FormattedValue.ToString();
                            DTrow["broj"] = dgv.Rows[i].Cells["broj_kalkulacije"].FormattedValue.ToString();
                            DTrow["barcode"] = dgv.Rows[i].Cells["ean"].FormattedValue.ToString();
                            DTrow["dokument"] = dgv.Rows[i].Cells["dokument"].FormattedValue.ToString();
                            DTrow["kolicina"] = Convert.ToInt16(kolicina).ToString();
                            DTSK.Rows.Add(DTrow);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    decimal kk = Convert.ToDecimal(dgv.Rows[i].Cells["kolicina"].FormattedValue.ToString());

                    for (int k = 0; k < Convert.ToInt16(kk); k++)
                    {
                        DataRow DTrow = DTSK.NewRow();
                        DTrow["sifra"] = dgv.Rows[i].Cells["sifra"].FormattedValue.ToString();
                        DTrow["naziv"] = dgv.Rows[i].Cells["naziv"].FormattedValue.ToString();
                        DTrow["jmj"] = dgv.Rows[i].Cells["jmj"].FormattedValue.ToString();
                        DTrow["mpc"] = dgv.Rows[i].Cells["mpc"].FormattedValue.ToString();
                        DTrow["vpc"] = dgv.Rows[i].Cells["vpc"].FormattedValue.ToString();
                        DTrow["godina"] = dgv.Rows[i].Cells["godina"].FormattedValue.ToString();
                        DTrow["broj"] = dgv.Rows[i].Cells["broj_kalkulacije"].FormattedValue.ToString();
                        DTrow["barcode"] = dgv.Rows[i].Cells["ean"].FormattedValue.ToString();
                        DTrow["dokument"] = dgv.Rows[i].Cells["dokument"].FormattedValue.ToString();
                        DTSK.Rows.Add(DTrow);
                    }
                }
            }

            if (radioButton1.Checked)
            {
                Report.Naljepnice.frmNaljepnice3 np = new Report.Naljepnice.frmNaljepnice3();
                np.DT = DTSK;
                np.mpcBool = chbMPC.Checked;
                np.vpcBool = chbVPC.Checked;
                np.rucno = cbrucno.Checked;
                np.pocetak = dec_parse - 1;
                np.ShowDialog();
            }
            else if (radioButton2.Checked)
            {
                if (checkBox1.Checked)
                {
                    Report.Naljepnice.frmNaljepnice np = new Report.Naljepnice.frmNaljepnice();
                    np.DT = DTSK;
                    np.mpcBool = chbMPC.Checked;
                    np.vpcBool = chbVPC.Checked;
                    np.pocetak = dec_parse - 1;
                    np.ShowDialog();
                }
                else
                {
                    Report.Naljepnice.frmNaljepniceCustom np = new Report.Naljepnice.frmNaljepniceCustom();
                    np.DT = DTSK;
                    np.mpcBool = chbMPC.Checked;
                    np.vpcBool = chbVPC.Checked;
                    np.pocetak = dec_parse - 1;
                    np.ShowDialog();
                }
            }
            else
            {
                if (checkBox1.Checked)
                {
                    Report.Naljepnice.frmNaljepnice np = new Report.Naljepnice.frmNaljepnice();
                    np.DT = DTSK;
                    np.mpcBool = chbMPC.Checked;
                    np.vpcBool = chbVPC.Checked;
                    np.pocetak = dec_parse - 1;
                    np.ShowDialog();
                }
                else
                {
                    Report.Naljepnice.frmNaljepniceCustom np = new Report.Naljepnice.frmNaljepniceCustom();
                    np.DT = DTSK;
                    np.mpcBool = chbMPC.Checked;
                    np.vpcBool = chbVPC.Checked;
                    np.pocetak = dec_parse - 1;
                    np.ShowDialog();
                }
            }
        }

        private void txtOd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                //btnUcitaj.Select();
            }
        }

        private void txtDO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                btnUcitaj_Click(null, null);
            }
        }

        private void txtPocetak_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                //btnUcitaj_Click(null, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmRobaTrazi rtra = new frmRobaTrazi();
            rtra.ShowDialog();
            txtartikl.Text = Properties.Settings.Default.id_roba.ToString();
            if (txtartikl.Text != "")
            {
                txtimeartikla.Text = classSQL.select("Select naziv from roba where sifra = '" + Properties.Settings.Default.id_roba.ToString() + "'", "ime robe").Tables[0].Rows[0][0].ToString();
            }
        }

        private void provjera_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender.ToString().Length == 0) return;

            if (sender.ToString()[0] == '+')
            {
                if ('+' == (e.KeyChar)) return;
            }

            if (sender.ToString()[0] == '-')
            {
                if ('-' == (e.KeyChar)) return;
            }

            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
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

        private void cbkalk_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox sndr = sender as CheckBox;
            if (sndr.Checked)
            {
                cbrucno.Checked = false;
                txtartikl.Text = "";
                txtimeartikla.Text = "";
                txtkol.Text = "";
            }
        }

        private void cbrucno_CheckedChanged(object sender, EventArgs e)
        {
            if (cbrucno.Checked)
            {
                cbkalk.Checked = false;
                txtOdKal.Text = "";
                txtDO.Text = "";
                txtPocetak.Text = "";
                cbMS.Checked = false;
                txtMDOd.Text = "";
                txtMDDo.Text = "";
                cbPC.Checked = false;
                txtPCDo.Text = "";
                txtPCOd.Text = "";
                chbMultiItems.Enabled = true;
            }
            else
            {
                chbMultiItems.Enabled = false;
            }
        }

        private void txtartikl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                try
                {
                    if (txtartikl.Text != "")
                    {
                        txtimeartikla.Text = classSQL.select("Select naziv from roba where sifra = '" + txtartikl.Text + "'", "ime artikla").Tables[0].Rows[0][0].ToString();
                        if (txtkol.Text == "")
                        {
                            txtkol.Text = "1";
                        }
                    }
                }
                catch
                { }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
            }
        }
    }
}