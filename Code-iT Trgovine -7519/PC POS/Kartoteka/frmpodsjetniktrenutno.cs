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
    public partial class frmpodsjetniktrenutno : Form
    {
        public frmMenuKarto menu { get; set; }
        private string email = "";
        private string password = "";
        private string subject = "";
        private string content = "";
        private string email_to = "";
        private bool salji_meni = false;
        public string ime_podsj { get; set; }
        public string prezime_podsj { get; set; }
        public string datum_podsj { get; set; }
        public string napomena_podsj { get; set; }
        public string idpartner { get; set; }
        public string emailproba = "";
        private Int32 var1 = 0;
        private Int32 var2 = 0;
        private Int32 var3 = 0;
        private Int32 var4 = 0;
        private Int32 var5 = 0;
        private Int32 var6 = 0;

        public frmpodsjetniktrenutno()
        {
            InitializeComponent();
        }

        private void provjera_email()
        {
            string obav = "UPDATE kar_podsjetnik SET obavijest_ekran = '1'";
            //menu.timer1.Stop();
            classSQL.update(obav);

            string em_kl = "SELECT email_klijenta FROM kar_podsjetnik WHERE id_partner = '" + idpartner + "' AND NOT email_klijenta = '' ";

            DataTable em_klij = classSQL.select(em_kl, "podsjetnik").Tables[0];
            if (em_klij.Rows.Count > 0)
            {
                emailproba = em_klij.Rows[0]["email_klijenta"].ToString();
            }
            else
            {
                emailproba = "Ne postoji email adresa!";
            }

            //int i = 0;
            //if(emailproba == "")
            //{
            //  do
            //  {
            //    i++;
            //  }
            //  while (em_klij.Rows[i]["email_klijenta"].ToString() != "");
            //}

            if (salji_meni == true)
            {
                {
                    email_to = email;
                    Util.classMail.send_email(email, password, email_to, "Podsjetnik", content + datum_podsj + " dolazi Vam klijent" + ime_podsj + prezime_podsj);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validator.EmailIsValid(emailproba) == true)
            {
                email_to = emailproba;
                Util.classMail.send_email(email, password, email_to, tbxtema.Text, rtbxsadrzaj.Text);
            }
            else
            {
                MessageBox.Show("Nije moguće poslati email, " + Environment.NewLine + "email adresa je navažeća ili je nema!");
            }
        }

        private void SetRemoteFields()
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\postavke.xml";
            XDocument xmlFile = XDocument.Load(path);
            var query = from c in xmlFile.Element("postavke").Elements("email").Elements("gmail") select c;
            foreach (XElement book in query)
            {
                email = book.Attribute("email").Value;
                password = book.Attribute("password").Value;
                subject = book.Attribute("subject").Value;
                content = book.Attribute("content").Value;
                salji_meni = Convert.ToBoolean(book.Attribute("salji_meni").Value);
                var1 = Convert.ToInt32(book.Attribute("var1").Value);
                var2 = Convert.ToInt32(book.Attribute("var2").Value);
                var3 = Convert.ToInt32(book.Attribute("var3").Value);
                var4 = Convert.ToInt32(book.Attribute("var4").Value);
                var5 = Convert.ToInt32(book.Attribute("var5").Value);
                var6 = Convert.ToInt32(book.Attribute("var6").Value);
            }
        }

        private void frmpodsjetniktrenutno_Load(object sender, EventArgs e)
        {
            SetRemoteFields();
            provjera_email();

            lblime.Text = ime_podsj;
            lblprezime.Text = prezime_podsj;
            lbldatum.Text = datum_podsj;
            lblnapomena.Text = napomena_podsj;
            lblemailprikaz.Text = emailproba;
            string txt = "Postovani " + prezime_podsj + ",\r\n" + datum_podsj + " " + content;
            rtbxsadrzaj.Text = txt;
            tbxtema.Text = subject;
        }

        private void frmpodsjetniktrenutno_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.FromArgb(var1, var2, var3), Color.FromArgb(var4, var5, var6), 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }
    }
}