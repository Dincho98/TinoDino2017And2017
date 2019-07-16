using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmUzvratiti : Form
    {
        private double ostatak = 0;
        public frmKasa MainForm { get; set; }
        public frmParagonac MainFormParagonac { get; set; }
        public string getUkupnoKasa;
        public string getNacin;
        private string[] oibs = new string[2] { "40097758416", Class.Postavke.OIB_PC1 };

        public frmUzvratiti()
        {
            InitializeComponent();
        }

        private void frmUzvratiti_Load(object sender, EventArgs e)
        {
            txtDobiveni.Select();
            SetComboBox();
            txtDobiveni.Text = string.Format("{0:0.00}", Convert.ToDouble(getUkupnoKasa));
            lblUkupno.Text = string.Format("{0:0.00}", Convert.ToDouble(getUkupnoKasa)) + "Kn";
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.DarkOliveGreen, Color.AliceBlue, 1000);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void SetComboBox()
        {
            DataTable DTSK = new DataTable("nacin");
            DTSK.Columns.Add("id_nacin", typeof(string));
            DTSK.Columns.Add("nacin", typeof(string));
            if (getNacin == "GO")
            {
                DTSK.Rows.Add("GO", "Gotovina");
                if (oibs.Contains(Class.PodaciTvrtka.oibTvrtke))
                    DTSK.Rows.Add("BO", "Bon");
            }
            else if (getNacin == "KA")
            {
                DTSK.Rows.Add("KA", "Kartice");
            }

            cbNacin.DataSource = DTSK;
            cbNacin.DisplayMember = "nacin";
            cbNacin.ValueMember = "id_nacin";
            cbNacin.SelectedValue = getNacin;

            DataTable DTSK1 = new DataTable("nacin");
            DTSK1.Columns.Add("id_nacin", typeof(string));
            DTSK1.Columns.Add("nacin", typeof(string));
            //DTSK1.Rows.Add("GO", "Gotovina");
            DTSK1.Rows.Add("KA", "Kartice");
            if (oibs.Contains(Class.PodaciTvrtka.oibTvrtke))
                DTSK1.Rows.Add("GO", "Gotovina");

            cbNacin2.DataSource = DTSK1;
            cbNacin2.DisplayMember = "nacin";
            cbNacin2.ValueMember = "id_nacin";
            cbNacin2.SelectedValue = "KA";
        }

        private void cbNacin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtDobiveni.Select();
            }
        }

        private void txtDobiveni_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.OemPeriod)
            {
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Oemcomma || e.KeyCode == (Keys)110)
            {
                if (txtDobiveni.Text.Count(x => x == ',') > 0)
                {
                    e.SuppressKeyPress = true;
                }
            }
            else if (txtDobiveni.Text.Count(x => x == ',') > 0 && txtDobiveni.Text.IndexOf(",") + 3 <= txtDobiveni.Text.Length)
            {
                if (txtDobiveni.SelectedText == txtDobiveni.Text) { }
                else if (txtDobiveni.Text.Substring(txtDobiveni.Text.IndexOf(","), 2).Length >= 2)
                {
                    if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
                    {
                        e.SuppressKeyPress = true;
                        return;
                    }
                    if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
                    {
                        e.SuppressKeyPress = true;
                        return;
                    }
                    if (e.KeyValue >= 96 && e.KeyValue <= 105)
                    {
                        e.SuppressKeyPress = true;
                        return;
                    }
                }
            }

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                double dobiveno = 0;
                double ukupno = 0;
                dobiveno = Convert.ToDouble(txtDobiveni.Text);
                ukupno = Convert.ToDouble(getUkupnoKasa);
                ostatak = dobiveno - ukupno;

                if (ostatak < 0)
                {
                    string nacinPlacanja = cbNacin.SelectedValue.ToString();
                    if (nacinPlacanja == "GO")
                    {
                        lbl1.Visible = true;
                        lbl2.Visible = true;
                        txtDobiveni2.Visible = true;
                        cbNacin2.Visible = true;
                        cbNacin2.Select();
                        cbNacin2.SelectedValue = "KA";
                        txtDobiveni2.Text = IzracunOstatakKartica().ToString("#0.00");
                        txtDobiveni2.ReadOnly = true;
                    }
                    else if (nacinPlacanja == "BO")
                    {
                        lbl1.Visible = true;
                        lbl2.Visible = true;
                        txtDobiveni2.Visible = true;
                        cbNacin2.Visible = true;
                        cbNacin2.Select();
                        cbNacin2.SelectedValue = "GO";
                        txtDobiveni2.Text = IzracunOstatakKartica().ToString("#0.00");
                        txtDobiveni2.ReadOnly = true;
                    }
                    else
                    {
                        lbl1.Visible = false;
                        lbl2.Visible = false;
                        txtDobiveni2.Visible = false;
                        cbNacin2.Visible = false;
                        txtDobiveni2.Text = IzracunOstatakKartica().ToString("#0.00");
                        txtDobiveni2.ReadOnly = true;

                        btnSpremi.Select();
                    }
                }
                else
                {

                    if (ostatak >= 0)
                    {
                        lbl1.Visible = false;
                        lbl2.Visible = false;
                        txtDobiveni2.Visible = false;
                        cbNacin2.Visible = false;
                        txtDobiveni2.Text = 0.ToString("#0.00");
                        txtDobiveni2.ReadOnly = true;
                    }
                    btnSpremi.Select();
                }
                lblVratiti.Text = string.Format("{0:0.00}", IzracunOstatak());
            }
        }

        private void cbNacin2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtDobiveni2.Select();
            }
        }

        private void txtDobiveni2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.OemPeriod)
            {
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Oemcomma || e.KeyCode == (Keys)110)
            {
                if (txtDobiveni.Text.Count(x => x == ',') > 0)
                {
                    e.SuppressKeyPress = true;
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ostatak = Convert.ToDouble(txtDobiveni2.Text) - (Convert.ToDouble(getUkupnoKasa) - Convert.ToDouble(txtDobiveni.Text));

                if (e.KeyCode == Keys.Enter)
                {
                    lblVratiti.Text = string.Format("{0:0.00}", IzracunOstatak());

                    if (IzracunOstatak() < 0)
                    {
                        if (MessageBox.Show("Dobiveni iznos je manji od ukupnog iznosa.\r\nJeste li sigurni da želite završiti račun?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            btnSpremi.Select();
                        }
                    }
                    else
                    {
                        btnSpremi.Select();
                    }
                }
            }
        }

        private double IzracunOstatakKartica()
        {
            double uk = Convert.ToDouble(getUkupnoKasa) - (Convert.ToDouble(txtDobiveni.Text));

            if (uk < 0)
            {
                lblVratiti.ForeColor = Color.Red;
            }
            else if (uk == 0)
            {
                lblVratiti.ForeColor = Color.Black;
            }
            else
            {
                lblVratiti.ForeColor = Color.Green;
            }
            return uk;
        }

        private double IzracunOstatak()
        {
            double uk = (Convert.ToDouble(txtDobiveni.Text) + Convert.ToDouble(txtDobiveni2.Text)) - Convert.ToDouble(getUkupnoKasa);

            if (uk < 0)
            {
                lblVratiti.ForeColor = Color.Red;
            }
            else if (uk == 0)
            {
                lblVratiti.ForeColor = Color.Black;
            }
            else
            {
                lblVratiti.ForeColor = Color.Green;
            }
            return uk;
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

            if (',' == (e.KeyChar))
            {
                e.Handled = false; return;
            }

            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            double k = 0;
            double g = 0;
            double bon = 0;

            try
            {
                if (MainForm != null)
                {
                    MainForm.DobivenoGotovina = "0";
                }

                //paragonac
                if (MainFormParagonac != null)
                {
                    MainFormParagonac.DobivenoGotovina = "0";
                }

                if (cbNacin.SelectedValue.ToString() == "GO")
                {
                    g = Convert.ToDouble(getUkupnoKasa) - Convert.ToDouble(txtDobiveni2.Text);
                    if (MainForm != null)
                    {
                        MainForm.DobivenoGotovina = txtDobiveni.Text;
                    }

                    //paragonac
                    if (MainFormParagonac != null)
                    {
                        MainFormParagonac.DobivenoGotovina = txtDobiveni.Text;
                    }
                }
                else if (cbNacin.SelectedValue.ToString() == "BO")
                {
                    bon = Convert.ToDouble(txtDobiveni.Text);
                }
                else
                {
                    if (MainForm != null)
                    {
                        MainForm.DobivenoGotovina = "0";
                    }

                    //paragonac
                    if (MainFormParagonac != null)
                    {
                        MainFormParagonac.DobivenoGotovina = "0";
                    }
                }

                if (cbNacin2.Visible == true)
                {
                    if (cbNacin2.SelectedValue.ToString() == "KA")
                    {
                        k = Convert.ToDouble(txtDobiveni2.Text) + k;
                    }

                    if (cbNacin2.SelectedValue.ToString() == "GO")
                    {
                        g = Convert.ToDouble(txtDobiveni2.Text) + g;
                    }
                }

                if (getNacin == "KA")
                {
                    k = Convert.ToDouble(txtDobiveni.Text) + k;
                }

                //dodano 0.000001 jer dolazi do greške kod zaokruživanja (ono kad javlja da iznos nije dovoljan, odnosno da fali još kn)
                if (Convert.ToDouble((Convert.ToDouble((g + k + bon + 0.000001).ToString())).ToString("0.000000000000")) >= Convert.ToDouble(getUkupnoKasa))
                {
                    if (MainForm != null)
                    {
                        MainForm.IznosGotovina = g.ToString();
                        MainForm.IznosKartica = k.ToString();
                        MainForm.IznosBon = bon.ToString();
                    }

                    //paragonac
                    if (MainFormParagonac != null)
                    {
                        MainFormParagonac.IznosGotovina = g.ToString();
                        MainFormParagonac.IznosKartica = k.ToString();
                        //MainForm.IznosBon = bon.ToString();
                    }
                    this.Close();
                }
                else
                {
                    string msg = "Iznos računa je: " + string.Format("{0:0.00}", Convert.ToDouble(getUkupnoKasa)) + " kn" +
                        "\r\na prema Vašem upisu gotovina iznosi: " + g.ToString("#0.00") + " kn \r\na kartice: " + k.ToString("#0.00") + " kn." +
                        "\r\nFali vam još " + Convert.ToDouble(Convert.ToDouble(getUkupnoKasa) - (g + k)).ToString("#0.00") + " kn.";
                    MessageBox.Show(msg, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDobiveni.Select();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pogreška kod izračunavanja. Provjerite uspisane vrijednosti.\r\nOvo je orginalna pogreška:\r\n" + ex.ToString(), "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}