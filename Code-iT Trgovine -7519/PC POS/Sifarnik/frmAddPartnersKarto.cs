using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class frmAddPartnersKarto : Form
    {
        public string vrati_id = null;
        public bool _pozivaKartoteku;

        public frmAddPartnersKarto()
        {
            InitializeComponent();
        }

        private bool edit = false;
        public frmMenu MainFormMenu { get; set; }

        private void frmAddPartnersKarto_Load(object sender, EventArgs e)
        {
            txtSifra.Text = brojPartner();
            SetCb();
            txtIme.Select();

            //this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private bool privatni_kor = true;

        private void provjeri_vrstu_korisnika()
        {
            string popuni_kor = "";
            if (Properties.Settings.Default.id_partner != "")
            {
                popuni_kor = Properties.Settings.Default.id_partner;
                //edit = true;
                //EnableDisable(true);

                DataTable DTvrstakorisnika = classSQL.select("SELECT vrsta_korisnika FROM partners WHERE id_partner = '" + popuni_kor + "'", "vrsta_korisnika").Tables[0];
                if (DTvrstakorisnika.Rows[0]["vrsta_korisnika"].ToString() == "0")
                {
                    privatni_kor = true;
                }
                else
                {
                    privatni_kor = false;
                }
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

            //CB grad
            DataSet DSgrad = classSQL.select("SELECT * FROM grad ORDER BY grad ", "grad");
            cbGrad.DataSource = DSgrad.Tables[0];
            cbGrad.DisplayMember = "grad";
            cbGrad.ValueMember = "id_grad";
            cbGrad.SelectedValue = "2806";
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((byte)(105)), ((byte)(170)), ((byte)(197)));
            Color y = System.Drawing.Color.FromArgb(((byte)(40)), ((byte)(109)), ((byte)(135)));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
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
            DataTable DSbr = classSQL.select("SELECT MAX(id_partner) FROM partners", "partners").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            if (txtIme.Text == "")
            {
                MessageBox.Show("Krivi unos za ime.", "Greška"); return;
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
                if (_pozivaKartoteku)
                {
                    vrati_id = (Convert.ToInt32(brojPartner()) - 1).ToString();
                    this.Close();
                }
            }

            EnableDisable(false);
        }

        private void Update()
        {
            if (privatni_kor == true)
            {
                string sql = "UPDATE partners SET " +
           "id_grad='" + cbGrad.SelectedValue + "'," +
           "adresa='" + txtAdresa.Text + "'," +
           "napomena='" + rtbNapomena.Text + "'," +
           "id_djelatnost='" + txtDjelatnost.SelectedValue + "'," +
           "ime='" + txtIme.Text + "'," +
           "prezime='" + txtPrezime.Text + "'," +
           "email='" + txtEmail.Text + "'," +
           "tel='" + txtTel.Text + "'," +
           "mob='" + txtMob.Text + "'," +
           "datum_rodenja='" + dtpDatum.Value.ToString("yyyy-MM-dd") + "'," +
           "id_zupanija='" + txtZupanija.SelectedValue + "' WHERE id_partner='" + txtSifra.Text + "'" +
           "";

                //rtbNapomena.Text = sql;
                provjera_sql(classSQL.update(sql));
                MessageBox.Show("Spremljeno.");
            }
        }

        private void Spremi()
        {
            string sql = "INSERT INTO partners (id_partner,id_grad,adresa,napomena," +
            "id_djelatnost,ime,prezime,email,tel,mob,datum_rodenja,vrsta_korisnika," +
            "id_zupanija) VALUES (" +
            "'" + brojPartner() + "'," +
            "'" + cbGrad.SelectedValue + "'," +
            "'" + txtAdresa.Text + "'," +
            "'" + rtbNapomena.Text + "'," +
            "'" + txtDjelatnost.SelectedValue + "'," +
            "'" + txtIme.Text + "'," +
            "'" + txtPrezime.Text + "'," +
            "'" + txtEmail.Text + "'," +
            "'" + txtTel.Text + "'," +
            "'" + txtMob.Text + "'," +
            "'" + dtpDatum.Value.ToString("yyyy-MM-dd") + "'," +
            "'0'," +
            "'" + txtZupanija.SelectedValue + "'" +
            ")";

            //rtbNapomena.Text = sql;
            provjera_sql(classSQL.insert(sql));
            MessageBox.Show("Spremljeno.");
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
            txtEmail.Text = "";
            txtIme.Text = "";
            txtMob.Text = "";
            txtPrezime.Text = "";
            txtSifra.Text = brojPartner();
            txtTel.Text = "";
            edit = false;
        }

        private void EnableDisable(bool x)
        {
            txtAdresa.Enabled = x;
            txtEmail.Enabled = x;
            txtIme.Enabled = x;
            txtMob.Enabled = x;
            txtPrezime.Enabled = x;
            txtTel.Enabled = x;
            cbGrad.Enabled = x;
            button1.Visible = !x;

            //if (x == true) { button1.Visible = false; } else { button1.Visible = true; }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partner = new frmPartnerTrazi();
            partner.ShowDialog();

            provjeri_vrstu_korisnika();
            if (Properties.Settings.Default.id_partner != "")
            {
                FillPartner(Properties.Settings.Default.id_partner);
                edit = true;
                EnableDisable(true);
            }
        }

        private void FillPartner(string id)
        {
            if (privatni_kor == true)
            {
                DataTable DT = classSQL.select("SELECT * FROM partners WHERE id_partner='" + id + "'", "partners").Tables[0];

                if (DT.Rows[0]["adresa"].ToString() == "")
                {
                    txtAdresa.Text = "";
                }
                else
                {
                    txtAdresa.Text = DT.Rows[0]["adresa"].ToString();
                }

                if (DT.Rows[0]["id_djelatnost"].ToString() == "")
                {
                    txtDjelatnost.Text = "";
                }
                else
                {
                    txtDjelatnost.SelectedValue = DT.Rows[0]["id_djelatnost"].ToString();
                }

                if (DT.Rows[0]["email"].ToString() == "")
                {
                    txtEmail.Text = "";
                }
                else
                {
                    txtEmail.Text = DT.Rows[0]["email"].ToString();
                }

                if (DT.Rows[0]["ime"].ToString() == "")
                {
                    txtIme.Text = "";
                }
                else
                {
                    txtIme.Text = DT.Rows[0]["ime"].ToString();
                }

                if (DT.Rows[0]["mob"].ToString() == "")
                {
                    txtMob.Text = "";
                }
                else
                {
                    txtMob.Text = DT.Rows[0]["mob"].ToString();
                }

                if (DT.Rows[0]["prezime"].ToString() == "")
                {
                    txtPrezime.Text = "";
                }
                else
                {
                    txtPrezime.Text = DT.Rows[0]["prezime"].ToString();
                }

                if (DT.Rows[0]["id_partner"].ToString() == "")
                {
                    txtSifra.Text = "";
                }
                else
                {
                    txtSifra.Text = DT.Rows[0]["id_partner"].ToString();
                }

                if (DT.Rows[0]["tel"].ToString() == "")
                {
                    txtTel.Text = "";
                }
                else
                {
                    txtTel.Text = DT.Rows[0]["tel"].ToString();
                }

                if (DT.Rows[0]["id_zupanija"].ToString() == "")
                {
                    txtZupanija.Text = "";
                }
                else
                {
                    txtZupanija.SelectedValue = DT.Rows[0]["id_zupanija"].ToString();
                }

                if (DT.Rows[0]["id_grad"].ToString() == "")
                {
                    cbGrad.Text = "";
                }
                else
                {
                    cbGrad.SelectedValue = DT.Rows[0]["id_grad"].ToString();
                }

                try
                {
                    dtpDatum.Value = Convert.ToDateTime(DT.Rows[0]["datum_rodenja"].ToString());
                }
                catch (Exception)
                {
                }
            }
            else
            {
                MessageBox.Show("Nije moguće mjenjati podatke za poslovne korisnike unutar kronologije !");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EnableDisable(true);
            btnOdustani.PerformClick();
        }
    }
}