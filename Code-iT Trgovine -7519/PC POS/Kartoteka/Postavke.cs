using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PCPOS.Kartoteka
{
    public partial class Postavke : Form
    {
        public Postavke()
        {
            InitializeComponent();
        }

        private Int32 var1 = 0;
        private Int32 var2 = 0;
        private Int32 var3 = 0;
        private Int32 var4 = 0;
        private Int32 var5 = 0;
        private Int32 var6 = 0;

        private void Spremi_postavke_Click(object sender, EventArgs e)
        {
            if (Validator.EmailIsValid(tbxemailxml.Text) == true)
            {
                string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\postavke.xml";
                XDocument xmlFile = XDocument.Load(path);
                var query = from c in xmlFile.Element("postavke").Elements("email").Elements("gmail") select c;
                foreach (XElement book in query)
                {
                    book.Attribute("email").Value = tbxemailxml.Text;
                    book.Attribute("password").Value = tbxpasswordxml.Text;
                    book.Attribute("subject").Value = tbxsubjectxml.Text;
                    book.Attribute("content").Value = tbxcontentxml.Text;
                    book.Attribute("salji_meni").Value = ckbsaljimeni.Checked.ToString();
                    book.Attribute("podsjetime").Value = ckbnepodsjecajme.Checked.ToString();
                    book.Attribute("saljicestitku").Value = ckbrodjendan.Checked.ToString();
                    book.Attribute("sadrzajrodj").Value = tbxrodjtema.Text;
                    book.Attribute("temarodj").Value = rtbxsadrzajcestitke.Text;
                    book.Attribute("satidoporuke").Value = tbxsati.Text;
                    book.Attribute("var1").Value = textBox1.Text;
                    book.Attribute("var2").Value = textBox2.Text;
                    book.Attribute("var3").Value = textBox3.Text;
                    book.Attribute("var4").Value = textBox4.Text;
                    book.Attribute("var5").Value = textBox5.Text;
                    book.Attribute("var6").Value = textBox6.Text;
                }
                xmlFile.Save(path);
                MessageBox.Show("Spremljeno!");
                Application.Restart();
            }
            else
            {
                MessageBox.Show("Email adresa koju ste upisali, " + Environment.NewLine + " nije u valjanom obliku!");
            }
        }

        private void SetRemoteFields()
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\postavke.xml";

            XDocument xmlFile = XDocument.Load(path);
            var query = from c in xmlFile.Element("postavke").Elements("email").Elements("gmail") select c;
            foreach (XElement book in query)
            {
                tbxemailxml.Text = book.Attribute("email").Value;
                tbxpasswordxml.Text = book.Attribute("password").Value;
                tbxsubjectxml.Text = book.Attribute("subject").Value;
                tbxcontentxml.Text = book.Attribute("content").Value;
                ckbsaljimeni.Checked = Convert.ToBoolean(book.Attribute("salji_meni").Value);
                ckbnepodsjecajme.Checked = Convert.ToBoolean(book.Attribute("podsjetime").Value);
                ckbrodjendan.Checked = Convert.ToBoolean(book.Attribute("saljicestitku").Value);
                tbxrodjtema.Text = book.Attribute("sadrzajrodj").Value;
                rtbxsadrzajcestitke.Text = book.Attribute("temarodj").Value;
                tbxsati.Text = book.Attribute("satidoporuke").Value;
                var1 = Convert.ToInt32(book.Attribute("var1").Value);
                var2 = Convert.ToInt32(book.Attribute("var2").Value);
                var3 = Convert.ToInt32(book.Attribute("var3").Value);
                var4 = Convert.ToInt32(book.Attribute("var4").Value);
                var5 = Convert.ToInt32(book.Attribute("var5").Value);
                var6 = Convert.ToInt32(book.Attribute("var6").Value);

                //try
                //{
                //    DataTable DTDB = classSQL.select("SELECT datname FROM pg_database WHERE datistemplate IS FALSE AND datallowconn IS TRUE AND datname!='postgres';", "").Tables[0];

                //    for (int i = 0; i < DTDB.Rows.Count; i++)
                //    {
                //        cbRemoteNameDatabase.Items.Add(DTDB.Rows[i][0].ToString());
                //    }
                //}
                //catch (Exception)
                //{ }
            }
        }

        private void Postavke_Load(object sender, EventArgs e)
        {
            tbxcontentxml.Text = "Postovani,\r\n";
            tbxcontentxml.Text += "" + "imate dog sastanak\r\n";
            SetRemoteFields();
            textBox1.Text = var1.ToString();
            textBox2.Text = var2.ToString();
            textBox3.Text = var3.ToString();
            textBox4.Text = var4.ToString();
            textBox5.Text = var5.ToString();
            textBox6.Text = var6.ToString();
        }

        private void tbxpasswordxml_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            tbxpasswordxml.Text = tbxpasswordxml.Mask;
        }

        private void Postavke_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.FromArgb(var1, var2, var3), Color.FromArgb(var4, var5, var6), 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void tabPage1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.FromArgb(var1, var2, var3), Color.FromArgb(var4, var5, var6), 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void tabPage2_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.FromArgb(var1, var2, var3), Color.FromArgb(var4, var5, var6), 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            frmTestBoje test = new frmTestBoje();
            test.proba1 = Convert.ToInt32(textBox1.Text);
            test.proba2 = Convert.ToInt32(textBox2.Text);
            test.proba3 = Convert.ToInt32(textBox3.Text);
            test.proba4 = Convert.ToInt32(textBox4.Text);
            test.proba5 = Convert.ToInt32(textBox5.Text);
            test.proba6 = Convert.ToInt32(textBox6.Text);

            test.ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                tbxpasswordxml.PasswordChar = (char)0;
            }
            else
            {
                tbxpasswordxml.PasswordChar = '*';
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox1.Text) > 255)
            {
                textBox1.Text = "0";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox2.Text) > 255)
            {
                textBox2.Text = "0";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox3.Text) > 255)
            {
                textBox3.Text = "0";
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox4.Text) > 255)
            {
                textBox4.Text = "0";
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox5.Text) > 255)
            {
                textBox5.Text = "0";
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox6.Text) > 255)
            {
                textBox6.Text = "0";
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
             && !char.IsDigit(e.KeyChar)
             && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
    }
}