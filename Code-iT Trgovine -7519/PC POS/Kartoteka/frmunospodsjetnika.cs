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
    public partial class frmunospodsjetnika : Form
    {
        public frmMenuKarto glavni { get; set; }
        public bool update_dogadaj { get; set; }
        public int unos_id { get; set; }
        public string id_zaposlenik { get; set; }

        public frmunospodsjetnika()
        {
            InitializeComponent();
        }

        private Int32 var1 = 0;
        private Int32 var2 = 0;
        private Int32 var3 = 0;
        private Int32 var4 = 0;
        private Int32 var5 = 0;
        private Int32 var6 = 0;

        public string TextBoxValue
        {
            get { return tbxpodsjime.Text; }
            set { tbxpodsjime.Text = value; }
        }

        public string TextBoxValue1
        {
            get { return tbxpodsjprezime.Text; }
            set { tbxpodsjprezime.Text = value; }
        }

        private void frmunospodsjetnika_Load(object sender, EventArgs e)
        {
            SetRemoteFields();

            DataTable zap = classSQL.select("SELECT id_zaposlenik, ime, prezime FROM zaposlenici", "zaposlenici").Tables[0];
            cbxzaposlenik.DataSource = zap;
            cbxzaposlenik.DisplayMember = "ime";
            cbxzaposlenik.ValueMember = "id_zaposlenik";
            cbxzaposlenik.SelectedValue = id_zaposlenik;
        }

        private void btnpretragapartner_Click(object sender, EventArgs e)
        {
            PCPOS.Kartoteka.frmpretraga pret = new PCPOS.Kartoteka.frmpretraga();
            pret.MainForm_unospodsjetnika = this;
            pret.ShowDialog();
        }

        private void SetRemoteFields()
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\postavke.xml";
            XDocument xmlFile = XDocument.Load(path);
            var query = from c in xmlFile.Element("postavke").Elements("email").Elements("gmail") select c;
            foreach (XElement book in query)
            {
                var1 = Convert.ToInt32(book.Attribute("var1").Value);
                var2 = Convert.ToInt32(book.Attribute("var2").Value);
                var3 = Convert.ToInt32(book.Attribute("var3").Value);
                var4 = Convert.ToInt32(book.Attribute("var4").Value);
                var5 = Convert.ToInt32(book.Attribute("var5").Value);
                var6 = Convert.ToInt32(book.Attribute("var6").Value);
            }
        }

        private void btnspremipodj_Click(object sender, EventArgs e)
        {
            if (update_dogadaj == true)
            {
                string sql1 = "Update kar_podsjetnik SET id_zaposlenik = '" + cbxzaposlenik.SelectedValue + "' ," +
                    " ime_partnera = '" + tbxpodsjime.Text + "' ," +
                    " prezime_partnera = '" + tbxpodsjprezime.Text + "' ," +
                    " dodatni_podatak1 = '" + tbxpodsjdod1.Text + "' ," +
                    " dodatni_podatak2 = '" + tbxpodsjdod2.Text + "' ," +
                    " datum = '" + dateTimePicker1.Text + "' ," +
                    " napomena = '" + rtbxpodsjnapomena.Text + "' ," +
                    " email_klijenta = '" + tbxpodsjklijentaemail.Text + "' " +
                    " WHERE id = '" + unos_id + "'";
                classSQL.update(sql1);
                update_dogadaj = false;
            }
            else
            {
                string sql = "INSERT INTO kar_podsjetnik(id_partner, ime_partnera, prezime_partnera, dodatni_podatak1, dodatni_podatak2, datum, napomena, id_zaposlenik, " +
                                          " email_klijenta, obavijest_ekran) " +
                                          "VALUES('" + unos_id + "' ," +
                                          "'" + tbxpodsjime.Text + "' ," +
                                          "'" + tbxpodsjprezime.Text + "' ," +
                                          "'" + tbxpodsjdod1.Text + "' ," +
                                          "'" + tbxpodsjdod2.Text + "' ," +
                                          "'" + dateTimePicker1.Value.ToString("yyyy-MM-dd H:mm:ss") + "' ," +
                                          "'" + rtbxpodsjnapomena.Text + "' ," +
                                          "'" + cbxzaposlenik.SelectedValue + "' ," +
                                          "'" + tbxpodsjklijentaemail.Text + "' ," +
                                          "FALSE )" +
                                          "";
                classSQL.insert(sql);
            }

            MessageBox.Show("Uspješno spremljeno !");
            tbxpodsjime.Clear();
            tbxpodsjprezime.Clear();
            tbxpodsjdod1.Clear();
            tbxpodsjdod2.Clear();
            rtbxpodsjnapomena.Clear();
            tbxpodsjklijentaemail.Clear();
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w + 3, h + 3);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w - 3, h - 3);
        }

        private void frmunospodsjetnika_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.FromArgb(var1, var2, var3), Color.FromArgb(var4, var5, var6), 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }
    }
}