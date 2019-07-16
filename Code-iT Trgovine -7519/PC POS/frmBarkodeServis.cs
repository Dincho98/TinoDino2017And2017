using GenCode128;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmBarkodeServis : Form
    {
        public frmBarkodeServis()
        {
            InitializeComponent();
        }

        private void frmIzradaNaljepnice_Load(object sender, EventArgs e)
        {
            this.reportViewer1.LocalReport.EnableExternalImages = true;

            this.Paint += new PaintEventHandler(Form1_Paint);
            txtPocetak.Select();

            //this.reportViewer1.RefreshReport();
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
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTlista = listaUniverzalna.DTListaUniverzalna;
                int lengthBarkode = 5;
                string nest = 9.ToString().PadLeft(lengthBarkode, '9');
                DTlista.Clear();

                int x = (radioButton1.Checked ? 3 : 4);
                int y = (checkBox1.Checked ? 16 : 17);

                int start = 0, br = 1;
                DataRow r = DTlista.NewRow();
                if (txtPocetak.Text.Length > 0 && Int32.TryParse(txtPocetak.Text, out start) && start > 0 && (start + (x * y)) < Convert.ToInt32(nest))
                {
                    for (int i = start; i < (start + (x * y)); i++)
                    {
                        string s = i.ToString().PadLeft(lengthBarkode, '0');
                        string barkode = Util.Korisno.GodinaKojaSeKoristiUbazi.ToString() + "-" + s + "-" + Util.Korisno.nazivPoslovnica;
                        Image img_barcode = Code128Rendering.MakeBarcodeImage(barkode, int.Parse("3"), true);
                        if (!Directory.Exists("EanPictures"))
                            Directory.CreateDirectory("EanPictures");

                        img_barcode.Save("EanPictures/" + barkode + ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        string myExeDir = "file://" + AppDomain.CurrentDomain.BaseDirectory + "EanPictures\\" + barkode + ".jpeg";

                        if (br == 1 && i > start)
                        {
                            r = DTlista.NewRow();
                        }

                        r["string" + (((br - 1) * 5) + 1).ToString()] = "";
                        r["string" + (((br - 1) * 5) + 2).ToString()] = barkode;
                        r["string" + (((br - 1) * 5) + 3).ToString()] = "";
                        r["string" + (((br - 1) * 5) + 4).ToString()] = "";
                        r["string" + (((br - 1) * 5) + 5).ToString()] = "";
                        r["slika" + (br).ToString()] = myExeDir;

                        if (br == x)
                        {
                            DTlista.Rows.Add(r);
                            br = 1;
                        }
                        else
                        {
                            br++;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Pogrešni podaci.");
                }

                this.reportViewer1.RefreshReport();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void txtOd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
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
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //frmRobaTrazi rtra = new frmRobaTrazi();
            //rtra.ShowDialog();
            //txtartikl.Text = Properties.Settings.Default.id_roba.ToString();
            //if (txtartikl.Text != "")
            //{
            //    txtimeartikla.Text = classSQL.select("Select naziv from roba where sifra = '" + Properties.Settings.Default.id_roba.ToString() + "'", "ime robe").Tables[0].Rows[0][0].ToString();
            //}
        }

        private void provjera_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender.ToString().Length == 0)
                return;

            if (sender.ToString()[0] == '+')
            {
                if ('+' == (e.KeyChar))
                    return;
            }

            if (sender.ToString()[0] == '-')
            {
                if ('-' == (e.KeyChar))
                    return;
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
                //cbrucno.Checked = false;
                //txtartikl.Text = "";
                //txtimeartikla.Text = "";
                //txtkol.Text = "";
            }
        }

        private void cbrucno_CheckedChanged(object sender, EventArgs e)
        {
            //if (cbrucno.Checked)
            //{
            //    cbkalk.Checked = false;
            //    txtOdKal.Text = "";
            //    txtDO.Text = "";
            //    txtPocetak.Text = "";
            //    cbMS.Checked = false;
            //    txtMDOd.Text = "";
            //    txtMDDo.Text = "";
            //    cbPC.Checked = false;
            //    txtPCDo.Text = "";
            //    txtPCOd.Text = "";
            //}
        }

        private void txtartikl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                try
                {
                    //if (txtartikl.Text != "")
                    //{
                    //    txtimeartikla.Text = classSQL.select("Select naziv from roba where sifra = '" + txtartikl.Text + "'", "ime artikla").Tables[0].Rows[0][0].ToString();
                    //    if (txtkol.Text == "")
                    //    {
                    //        txtkol.Text = "1";
                    //    }

                    //}
                }
                catch { }
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

        private void txtPocetak_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnIspis.PerformClick();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}