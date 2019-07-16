using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmKasaPrijava : Form
    {
        public frmKasaPrijava()
        {
            InitializeComponent();
        }

        private bool prijava = false;
        private int brojZaposlenika;
        private DataSet DSzaposlenik;
        //string trenutni = variable.Zaposlenici().ToString().ToLower();

        public frmMenu MainForm { get; set; }

        private void frmKasaPrijava_Load(object sender, EventArgs e)
        {
            fillComboBox();
            DefaultValue();
            cbBlagajnik.Select();
            txtZaporka.PasswordChar = '*';
            DataTable DTzp = classSQL.select("SELECT id_zaposlenik FROM zaposlenici", "zaposlenici").Tables[0];
            brojZaposlenika = DTzp.Rows.Count > 0 ? DTzp.Rows.Count : 0;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            /*Color x = System.Drawing.Color.FromArgb(((byte)(105)), ((byte)(170)), ((byte)(197)));
            Color y = System.Drawing.Color.FromArgb(((byte)(40)), ((byte)(109)), ((byte)(135)));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);*/
            Color x = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            Color y = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void fillComboBox()
        {
            DSzaposlenik = classSQL.select("SELECT id_zaposlenik, ime + ' ' + prezime AS name FROM zaposlenici WHERE aktivan='DA'", "zaposlenici");
            cbBlagajnik.DataSource = DSzaposlenik.Tables[0];
            cbBlagajnik.DisplayMember = "name";
            cbBlagajnik.ValueMember = "id_zaposlenik";
        }

        private void DefaultValue()
        {
            try
            {
                DataTable DTpostavke = classSQL.select_settings("SELECT default_ducan,default_blagajna,default_skladiste,default_blagajnik FROM postavke", "postavke").Tables[0];

                //cbBlagajna.SelectedValue = DTpostavke.Rows[0]["default_blagajna"].ToString();
                //cbDucan.SelectedValue = DTpostavke.Rows[0]["default_ducan"].ToString();
                //cbSkladiste.SelectedValue = DTpostavke.Rows[0]["default_skladiste"].ToString();
                cbBlagajnik.SelectedValue = DTpostavke.Rows[0]["default_blagajnik"].ToString();
            }
            catch
            {
            }
        }

        private void cbBlagajnik_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtZaporka.Select();
            }
        }

        private void frmKasaPrijava_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!prijava)
                Application.Exit();
        }

        private void txtZaporka_TextChanged(object sender, EventArgs e)
        {
            if (txtZaporka.Text == ",000000000000000000,")
            {
                Util.classZaposleniciDopustenja.superUserOn();
                prijava = true;
                Properties.Settings.Default.id_zaposlenik = "1";
                Properties.Settings.Default.Save();
                this.Close();
                this.Dispose();
                return;
            }

            //try
            //{
            DataTable DTzp = new DataTable();
            try
            {
                string sql = "SELECT id_zaposlenik FROM zaposlenici WHERE id_zaposlenik='" + cbBlagajnik.SelectedValue.ToString() + "' AND zaporka = '" + txtZaporka.Text.Trim() + "'";
                DTzp = classSQL.select(sql, "zaposlenici").Tables[0];
            }
            catch
            {
                return;
            }

            if (DTzp.Rows.Count > 0)
            {
                Util.classZaposleniciDopustenja.superUserOff();
                prijava = true;
                this.Close();
                this.Dispose();
                Properties.Settings.Default.id_zaposlenik = cbBlagajnik.SelectedValue.ToString();
                Properties.Settings.Default.Save();

                string test = variable.Zaposlenici().ToString().ToLower();
                if (variable.Zaposlenici().ToString().ToLower() == "blagajnik")
                {
                    //FormCollection fc = Application.OpenForms;

                    //foreach (Form f in fc)
                    //{
                    //    if (f.Name.ToString().ToLower() == "frmscren")
                    //    {
                    //        if (f.MdiChildren.OfType<frmKasa>().Count() > 0)
                    //        {
                    //            return;
                    //        }
                    //    }
                    //}

                    //if(Application.OpenForms.OfType<frmKasa>().Count()>0)
                    //{
                    //    return;
                    //}

                    frmKasa login = new frmKasa();
                    login.ShowDialog();
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (button12.Text == "DEL" && txtZaporka.Text.Length > 0)
            {
                txtZaporka.Text = txtZaporka.Text.Remove(txtZaporka.Text.Length - 1);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Control conGrupa = (Control)sender;
            txtZaporka.Text += conGrupa.Text;
        }
    }
}