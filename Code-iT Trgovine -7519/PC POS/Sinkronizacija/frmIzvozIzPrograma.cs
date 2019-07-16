using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PCPOS.Sinkronizacija
{
    public partial class frmIzvozIzPrograma : Form
    {
        public frmIzvozIzPrograma()
        {
            InitializeComponent();
        }

        private void frmIzvozIzPrograma_Load(object sender, EventArgs e)
        {
            fill();
            classProvjeraTablica.Provjera();
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

        private void fill()
        {
            DataTable DT = classSQL.select("SELECT * FROM skladiste", "skladiste").Tables[0];
            cbSkladiste.DataSource = DT;
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";
        }

        public string CreateXML()
        {
            Cursor.Current = Cursors.No;

            string partnersArr = "";

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                partnersArr += "'" + dgv.Rows[i].Cells["sifra"].FormattedValue.ToString() + "',";
            }

            if (partnersArr.Length > 0)
            {
                partnersArr = partnersArr.Remove(partnersArr.Length - 1);
                partnersArr = " AND id_partner IN (" + partnersArr + ")";
            }

            string od_datuma = "";
            int dec;
            if (int.TryParse(txtMjeseci.Text, out dec))
            {
                if (dec > 0 && dec < 10000)
                {
                    od_datuma = " AND datum_zadnje_aktivnosti='" + DateTime.Now.AddMonths(dec * -1).ToString("yyyy-MM-dd") + "'";
                }
            }

            string skl = cbSkladiste.SelectedValue.ToString();

            string sql = "SELECT " +
                " roba.sifra," +
                " roba.naziv," +
                " roba.opis," +
                " roba.brand," +
                " roba.jamstvo," +
                " roba.akcija," +
                " roba.link_za_slike," +
                " grupa.grupa," +
                " podgrupa.naziv AS podgrupa," +
                " manufacturers.manufacturers AS brand," +
                " roba.jamstvo," +
                " roba.mpc," +
                " roba.vpc," +
                " roba_prodaja.kolicina AS stanje," +
                " roba.akcija," +
                " roba.jm" +
                " FROM roba " +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=roba.sifra" +
                " LEFT JOIN manufacturers ON manufacturers.id_manufacturers=roba.id_manufacturers" +
                " LEFT JOIN grupa ON grupa.id_grupa=roba.id_grupa" +
                " LEFT JOIN podgrupa ON roba.id_podgrupa=podgrupa.id_podgrupa" +
                " WHERE " +
                " roba_prodaja.id_skladiste='" + skl + "'" +
                " " + partnersArr + od_datuma;

            DataTable DTart = classSQL.select(sql, "roba").Tables[0];

            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n" +
            "<PCArtikli>\r\n" +
            "<xs:schema id=\"PCArtikli\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"\"  xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">\r\n" +
            "<xs:element name=\"Artikl\">\r\n" +
            "<xs:complexType>\r\n" +
            "<xs:sequence>\r\n" +
            "<xs:element name=\"Sifra\" type=\"xs:string\"/>\r\n" +
            "<xs:element name=\"Naziv\" type=\"xs:string\"/>\r\n" +
            "<xs:element name=\"Opis\" type=\"xs:string\"/>\r\n" +
            "<xs:element name=\"Grupa\" type=\"xs:string\"/>\r\n" +
            "<xs:element name=\"Podgrupa\" type=\"xs:string\"/>\r\n" +
            "<xs:element name=\"Brand\" type=\"xs:string\"/>\r\n" +
            "<xs:element name=\"Jamstvo\" type=\"xs:string\"/>\r\n" +
            "<xs:element name=\"VPC\" type=\"xs:decimal\"/>\r\n" +
            "<xs:element name=\"MPC\" type=\"xs:decimal\"/>\r\n" +
            "<xs:element name=\"Stanje\" type=\"xs:int\"/>\r\n" +
            "<xs:element name=\"Akcija\" type=\"xs:boolean\"/>\r\n" +
            "<xs:element name=\"LinkSlikeWeb\" type=\"xs:boolean\"/>\r\n" +
            "</xs:sequence>\r\n" +
            "</xs:complexType>\r\n" +
            "</xs:element>\r\n" +
            "</xs:schema>\r\n";

            for (int i = 0; i < DTart.Rows.Count; i++)
            {
                string sifra = DTart.Rows[i]["sifra"].ToString().Replace("&", "and");
                string naziv = DTart.Rows[i]["naziv"].ToString().Replace("&", "and").Replace("\n", "").Replace("\r", "");
                string opis = DTart.Rows[i]["opis"].ToString().Replace("&", "and");
                string grupa = DTart.Rows[i]["grupa"].ToString().Replace("&", "and");
                string podgrupa = DTart.Rows[i]["podgrupa"].ToString().Replace("&", "and");
                string brand = DTart.Rows[i]["brand"].ToString().Replace("&", "and");
                string jamstvo = DTart.Rows[i]["jamstvo"].ToString();
                string vpc = DTart.Rows[i]["vpc"].ToString();
                string mpc = DTart.Rows[i]["mpc"].ToString();
                string stanje = DTart.Rows[i]["stanje"].ToString();
                string akcija = DTart.Rows[i]["akcija"].ToString();
                string link = DTart.Rows[i]["link_za_slike"].ToString().Replace("&", "");

                sifra = sifra.Replace("<", "");
                sifra = sifra.Replace(">", "");

                naziv = naziv.Replace("<", "");
                naziv = naziv.Replace(">", "");

                opis = opis.Replace("<", "");
                opis = opis.Replace(">", "");

                sifra = sifra.Trim();
                sifra = sifra.Replace("č", "c");
                sifra = sifra.Replace("Č", "C");
                sifra = sifra.Replace("ž", "z");
                sifra = sifra.Replace("Ž", "Z");
                sifra = sifra.Replace("ć", "c");
                sifra = sifra.Replace("Ć", "C");
                sifra = sifra.Replace("đ", "d");
                sifra = sifra.Replace("Đ", "D");
                sifra = sifra.Replace("š", "s");
                sifra = sifra.Replace("Š", "S");

                if (checkBox1.Checked)
                {
                    link = "http://plavazebra.hr/slike_privremeno/" + sifra + ".JPG";
                }

                //<UTF8>
                //byte[] bytes = Encoding.Default.GetBytes(naziv);
                //naziv = Encoding.UTF8.GetString(bytes);
                //</UTF8>

                decimal d;
                int br;
                if (!decimal.TryParse(vpc, out d)) { vpc = "0"; }
                if (!decimal.TryParse(mpc, out d)) { mpc = "0"; }
                if (!int.TryParse(stanje, out br)) { stanje = "1"; }
                if (!int.TryParse(jamstvo, out br)) { jamstvo = "0"; }
                if (int.Parse(stanje) < 0) { stanje = "0"; }
                if (stanje == "0") { stanje = "1"; }

                xml += "<Artikl>\n" +
                "<Sifra>" + sifra + "</Sifra>\r\n" +
                "<Naziv>" + naziv + "</Naziv>\r\n" +
                "<Opis>" + opis + "</Opis>\r\n" +
                "<Grupa>" + grupa + "</Grupa>\r\n" +
                "<Podgrupa>" + podgrupa + "</Podgrupa>\r\n" +
                "<Brand>" + brand + "</Brand>\r\n" +
                "<Jamstvo>" + jamstvo + "</Jamstvo>\r\n" +
                "<VPC>" + Math.Round(Convert.ToDecimal(vpc), 3).ToString("#0.000") + "</VPC>\r\n" +
                "<MPC>" + Math.Round(Convert.ToDecimal(mpc), 3).ToString("#0.000") + "</MPC>\r\n" +
                "<Stanje>" + stanje + "</Stanje>\r\n" +
                "<Akcija>" + akcija + "</Akcija>\r\n" +
                "<LinkSlikeWeb>" + link + "</LinkSlikeWeb>\r\n" +
                "</Artikl>\n";
            }

            xml += "</PCArtikli>\r\n";
            Cursor.Current = Cursors.Default;
            return xml;
        }

        private void btnKreiraj_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("xml_sinkronizacija"))
                Directory.CreateDirectory("xml_sinkronizacija");

            File.WriteAllText(@"c:\artikli.xml", CreateXML(), Encoding.UTF8);
            MessageBox.Show("Kreiranje XML datoteke je završeno.", "Završeno");
        }

        private void txtSifraPartnera_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtSifraPartnera.Text == "")
                {
                    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                    partnerTrazi.ShowDialog();
                    if (Properties.Settings.Default.id_partner != "")
                    {
                        DataSet partner = new DataSet();
                        partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                        if (partner.Tables[0].Rows.Count > 0)
                        {
                            txtSifraPartnera.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                            txtNazivPartnera.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                            txtSifraPartnera.Select();
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtSifraPartnera.Select();
                        }
                    }
                    else
                    {
                        txtSifraPartnera.Select();
                        return;
                    }
                }

                string Str = txtSifraPartnera.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtSifraPartnera.Text = "0";
                }

                DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraPartnera.Text + "'", "partners").Tables[0];
                if (DSpar.Rows.Count > 0)
                {
                    txtNazivPartnera.Text = DSpar.Rows[0][0].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            if (txtSifraPartnera.Text != "")
            {
                DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraPartnera.Text + "'", "partners").Tables[0];
                if (DSpar.Rows.Count > 0)
                {
                    txtNazivPartnera.Text = DSpar.Rows[0][0].ToString();
                    dgv.Rows.Add(txtSifraPartnera.Text, txtNazivPartnera.Text);
                    txtNazivPartnera.Text = "";
                    txtSifraPartnera.Text = "";
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void btnKreirajXMLiSpremi_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.xml)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.FileName = "artikli.xml";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = saveFileDialog1.FileName;
                string xml = CreateXML();
                File.WriteAllText(file, xml);

                MessageBox.Show("Kreiranje XML datoteke je završeno.", "Završeno");
            }
        }

        private void btnTraziPartnera_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtSifraPartnera.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtNazivPartnera.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void btnSpremiSQL_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                DataTable DT = classSQL.select("SELECT * FROM roba", "roba").Tables[0];

                sb.Append("BEGIN;\r\n");
                foreach (DataRow r in DT.Rows)
                {
                    sb.Append(@"INSERT INTO roba (naziv,id_grupa,jm,vpc,mpc,id_zemlja_porijekla,id_zemlja_uvoza,id_partner,id_manufacturers,
                sifra,ean,porez,oduzmi,nc,porez_potrosnja,id_podgrupa,opis,brand,jamstvo,akcija,link_za_slike,novo,editirano)
                VALUES (
                '" + r["naziv"].ToString() + @"',
                '" + r["id_grupa"].ToString() + @"',
                '" + r["jm"].ToString() + @"',
                '" + r["vpc"].ToString().Replace(",", ".") + @"',
                '" + r["mpc"].ToString().Replace(".", ",") + @"',
                '" + r["id_zemlja_porijekla"].ToString() + @"',
                '" + r["id_zemlja_uvoza"].ToString() + @"',
                '" + r["id_partner"].ToString() + @"',
                '" + r["id_manufacturers"].ToString() + @"',
                '" + r["sifra"].ToString() + @"',
                '" + r["ean"].ToString() + @"',
                '" + r["porez"].ToString().Replace(".", ",") + @"',
                '" + r["oduzmi"].ToString() + @"',
                '" + r["nc"].ToString().Replace(".", ",") + @"',
                '" + r["porez_potrosnja"].ToString().Replace(".", ",") + @"',
                '" + r["id_podgrupa"].ToString() + @"',
                '" + r["opis"].ToString() + @"',
                '" + r["brand"].ToString() + @"',
                '" + r["jamstvo"].ToString() + @"',
                '" + r["akcija"].ToString() + @"',
                '" + r["link_za_slike"].ToString() + @"',
                '0',
                '0'
                );" + "\r\n");
                }
                sb.Append("COMMIT;\r\n");
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                File.WriteAllText(path + "/roba.sql", sb.ToString(), Encoding.Default);
                MessageBox.Show("File je uspješno kreiran. Nalazi se na radnoj površini.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}