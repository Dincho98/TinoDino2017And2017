using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace PCPOS.Sinkronizacija.Partneri
{
    public partial class Afrodita : Form
    {
        public Afrodita()
        {
            InitializeComponent();
        }

        private void frmIzvozIzPrograma_Load(object sender, EventArgs e)
        {
            fill();
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

        private string[] GetSlike(string sifra)
        {
            sifra = sifra.Remove(0, 3);

            string urlAddress = "http://www.skolskaknjiga.hr/hrv/page.asp?item=" + sifra + "&act=more";
            bool postoji = false, postoji_src = false, PostojiOpis = false, PostojiOpisZapisi = false;
            string str = GetHtml(urlAddress), slika = "", opis = "";

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == 'a' && str[i + 3] == 'i' && str[i + 4] == 'c' && str[i + 6] == 'e' && str[i + 9] == 'g')
                {
                    postoji = true;
                }
                else if (postoji)
                {
                    if (postoji && str[i] == '.' && str[i - 3] == '=' && str[i - 4] == 'c' && str[i - 6] == 's')
                    {
                        postoji_src = true;
                    }
                    else if (postoji_src)
                    {
                        if (str[i + 0] == '"' && str[i + 2] == 'a' && str[i + 4] == 't')
                        {
                            postoji_src = false;
                            PostojiOpis = true;
                        }
                        else
                        {
                            slika += str[i];
                        }
                    }
                    else if (PostojiOpis)
                    {
                        if (str[i] == '>' && str[i - 1] == 'r' && str[i - 2] == 'b' && str[i - 3] == '<' && str[i - 5] == 'p' && str[i - 6] == 's' && str[i - 11] == 'r' && str[i - 12] == 'b' && str[i - 12] == 'b' && str[i - 15] == 'n' && str[i - 16] == 'a')
                        {
                            PostojiOpisZapisi = true;
                        }
                        else if (PostojiOpisZapisi)
                        {
                            if (str[i + 0] == '<' && str[i + 1] == 'd' && str[i + 2] == 'i' && str[i + 3] == 'v' && str[i + 5] == 's' && str[i + 6] == 't' && str[i + 23] == '1' && str[i + 24] == '0')
                            {
                                break;
                            }
                            opis += str[i];
                        }
                    }
                }
            }

            string[] vraca = new string[2]; ;
            vraca[0] = "http://www.skolskaknjiga.hr" + slika;
            vraca[1] = opis;

            return vraca;
        }

        public static string GetHtml(string urlAddr)
        {
            if (urlAddr == null || string.IsNullOrEmpty(urlAddr))
            {
                throw new ArgumentNullException("urlAddr");
            }
            else
            {
                string result;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddr);
                request.Proxy = null;
                request.UseDefaultCredentials = true;
                CookieContainer cc = new CookieContainer();
                request.CookieContainer = cc;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                    sr.Close();
                }
                return result;
            }
        }

        private void fill()
        {
            DataTable DT = classSQL.select("SELECT * FROM skladiste", "skladiste").Tables[0];
            cbSkladiste.DataSource = DT;
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";

            DataTable DTSK = new DataTable("skladiste");
            DTSK.Columns.Add("id_podgrupa", typeof(string));
            DTSK.Columns.Add("grupa", typeof(string));

            string sql = "SELECT grupa.grupa+'  /  '+podgrupa.naziv AS grupa,podgrupa.id_podgrupa FROM podgrupa " +
                "LEFT JOIN grupa ON grupa.id_grupa=podgrupa.id_grupa WHERE podgrupa.id_podgrupa NOT IN ('1') ORDER BY grupa.grupa ASC";

            DataTable DTg = classSQL.select(sql, "grupa").Tables[0];
            for (int i = 0; i < DTg.Rows.Count; i++)
            {
                string d = DTg.Rows[i]["id_podgrupa"].ToString();
                DTSK.Rows.Add(DTg.Rows[i]["id_podgrupa"].ToString(), DTg.Rows[i]["grupa"].ToString());
            }

            grupa.DataSource = DTSK;
            grupa.DataPropertyName = "grupa";
            grupa.DisplayMember = "grupa";
            grupa.HeaderText = "Grupa";
            grupa.Name = "grupa";
            grupa.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            grupa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            grupa.ValueMember = "id_podgrupa";
        }

        #region ŠifraPartnerKeyDown

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

        private void txtSifraPartnera_Leave(object sender, EventArgs e)
        {
        }

        #endregion ŠifraPartnerKeyDown

        private void btnUcitajExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog saveFileDialog1 = new OpenFileDialog();

            saveFileDialog1.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            //saveFileDialog1.FileName = "artikli.xml";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = saveFileDialog1.FileName;
                loadXLS(file);
            }
        }

        private DataTable DT;
        private Uri curPage;
        private int row = 0;

        private void loadXLS(string path)
        {
            string connectionString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", path);
            string query = string.Format("select * from [{0}$]", "List1");
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connectionString);
            DataSet DS = new DataSet();
            dataAdapter.Fill(DS);

            DT = DS.Tables[0];

            progressBar1.Value = 0;
            progressBar1.Visible = true;

            for (int i = DT.Rows.Count - 1; i > 0; i--) if (DT.Rows[i]["Sifra"].ToString() == "") DT.Rows.RemoveAt(i);

            for (int i = 0; i < DT.Rows.Count - 1; i++)
                for (int j = i + 1; j < DT.Rows.Count; j++)
                    if (DT.Rows[i]["Sifra"].ToString() == DT.Rows[j]["Sifra"].ToString())
                    {
                        string str1 = DT.Rows[i]["Sifra"].ToString();
                        string str2 = DT.Rows[j]["Sifra"].ToString();
                        DT.Rows.RemoveAt(j);
                        j--;
                    }

            int row = 0;
            foreach (DataRow r in DT.Rows)
            {
                int x, y;
                string search = GetHtml("http://www.kozmetika-afrodita.com/cro/search?search=" + r["Naziv"]);
                x = search.IndexOf("\"xsltsearch_result\"");
                if (x != -1)
                {
                    x = search.IndexOf("href=", x) + 6;
                    y = search.IndexOf("\"", x);
                    string produkt = GetHtml("http://www.kozmetika-afrodita.com/" + search.Substring(x, y - x));

                    x = produkt.IndexOf("productLeft");
                    if (x == -1) continue;
                    x = produkt.IndexOf("src=", x) + 5;
                    y = produkt.IndexOf("\"", x);
                    string slika = produkt.Substring(x, y - x);
                    if (slika.IndexOf("/postotak") != -1)
                    {
                        x = produkt.IndexOf("src=", x) + 5;
                        y = produkt.IndexOf("\"", x);
                        slika = produkt.Substring(x, y - x);
                    }
                    r["LinkSlikeWeb"] = "http://www.kozmetika-afrodita.com/" + slika;

                    decimal vpc, pdv;
                    decimal.TryParse(r["VPC"].ToString(), out vpc);
                    decimal.TryParse(r["PDV"].ToString(), out pdv);
                    r["MPC"] = (vpc * (1 + (pdv / 100)));
                }

                progressBar1.Value = (++row * 100) / DT.Rows.Count;
            }

            progressBar1.Value = 0;
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                DataTable DTgrupa = classSQL.select("SELECT * FROM grupa WHERE grupa='" + DT.Rows[i]["Grupa"].ToString() + "'", "").Tables[0];
                DataTable DTpodgrupa = classSQL.select("SELECT * FROM podgrupa WHERE naziv='" + DT.Rows[i]["Podgrupa"].ToString() + "'", "").Tables[0];
                //DataTable DTbrand = classSQL.select("SELECT * FROM manufacturers WHERE manufacturers='" + DT.Rows[i]["Brand"].ToString() + "'", "").Tables[0];

                if (DTgrupa.Rows.Count == 0)
                {
                    classSQL.insert("INSERT INTO grupa (grupa) VALUES ('" + DT.Rows[i]["Grupa"].ToString() + "')");
                    DTgrupa = classSQL.select("SELECT * FROM grupa WHERE grupa='" + DT.Rows[i]["Grupa"].ToString() + "' ORDER BY id_grupa DESC LIMIT 1", "").Tables[0];
                    create();
                }

                if (DTpodgrupa.Rows.Count == 0)
                {
                    classSQL.insert("INSERT INTO podgrupa (naziv,id_grupa) VALUES ('" + DT.Rows[i]["Podgrupa"].ToString() + "','" + DTgrupa.Rows[0]["id_grupa"].ToString() + "')");
                    DTpodgrupa = classSQL.select("SELECT * FROM podgrupa WHERE naziv='" + DT.Rows[i]["Podgrupa"].ToString() + "' ORDER BY id_podgrupa DESC LIMIT 1", "").Tables[0];
                    create();
                }

                /*if (DTbrand.Rows.Count == 0)
                {
                    classSQL.insert("INSERT INTO manufacturers (manufacturers) VALUES ('" + DT.Rows[i]["Brand"].ToString() + "')");
                    create();
                }*/

                progressBar1.Value = (i * 100) / DT.Rows.Count;

                if (DT.Rows[i]["LinkSlikeWeb"].ToString() != "")
                    dgv.Rows.Add(
                        DT.Rows[i]["Sifra"].ToString(),
                        DT.Rows[i]["Naziv"].ToString(),
                        DTpodgrupa.Rows[0][0].ToString(),
                        DT.Rows[i]["Sifra"].ToString(),
                        DT.Rows[i]["Opis"].ToString(),
                        DT.Rows[i]["Grupa"].ToString(),
                        DT.Rows[i]["Podgrupa"].ToString(),
                        "",
                        "",
                        DT.Rows[i]["VPC"].ToString(),
                        DT.Rows[i]["MPC"].ToString(),
                        DT.Rows[i]["Stanje"].ToString(),
                        "",
                        DT.Rows[i]["LinkSlikeWeb"].ToString(),
                        DT.Rows[i]["PDV"].ToString()
                    );
            }

            progressBar1.Visible = false;
        }

        private void create()
        {
            DataTable DTSK = new DataTable("skladiste");
            DTSK.Columns.Add("id_podgrupa", typeof(string));
            DTSK.Columns.Add("grupa", typeof(string));
            string sql = "SELECT grupa.grupa+'  /  '+podgrupa.naziv AS grupa,podgrupa.id_podgrupa FROM podgrupa " +
            "LEFT JOIN grupa ON grupa.id_grupa=podgrupa.id_grupa WHERE podgrupa.id_podgrupa NOT IN ('1') ORDER BY grupa.grupa ASC";

            DataTable DTg = classSQL.select(sql, "grupa").Tables[0];
            for (int i = 0; i < DTg.Rows.Count; i++)
            {
                string d = DTg.Rows[i]["id_podgrupa"].ToString();
                DTSK.Rows.Add(DTg.Rows[i]["id_podgrupa"].ToString(), DTg.Rows[i]["grupa"].ToString());
            }

            grupa.DataSource = DTSK;
            grupa.DataPropertyName = "grupa";
            grupa.DisplayMember = "grupa";
            grupa.HeaderText = "Grupa";
            grupa.Name = "grupa";
            grupa.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            grupa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            grupa.ValueMember = "id_podgrupa";
        }

        private void btnKreirajXMLiSpremi_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv.RowCount; i++)
            {
                //PODGRUPA
                //DataTable DTpodgrupa = classSQL.select("SELECT id_podgrupa FROM podgrupa WHERE naziv='" + dgv.Rows[i].Cells["Podgrupa"].FormattedValue.ToString() + "'", "").Tables[0];
                string podgrupa = dgv.Rows[i].Cells["grupa"].Value.ToString();
                //GRUPA
                DataTable DTgrupa = classSQL.select("SELECT id_grupa FROM grupa WHERE grupa='" + dgv.Rows[i].Cells["grupa_"].FormattedValue.ToString() + "'", "").Tables[0];
                string grupa = DTgrupa.Rows[0][0].ToString();
                //BRAND
                //DataTable DTbrand = classSQL.select("SELECT id_manufacturers FROM manufacturers WHERE manufacturers='" + dgv.Rows[i].Cells["Brand"].FormattedValue.ToString() + "'", "").Tables[0];
                //string Brand = DTgrupa.Rows[0][0].ToString();

                string jamstvo = "0";
                string akcija = "0";

                if (dgv.Rows[i].Cells["Akcija"].FormattedValue.ToString().ToUpper() == "DA")
                {
                    akcija = "1";
                }

                int p;
                if (!int.TryParse(dgv.Rows[i].Cells["Jamstvo"].FormattedValue.ToString(), out p))
                {
                    jamstvo = "0";
                }
                else
                {
                    jamstvo = dgv.Rows[i].Cells["Jamstvo"].FormattedValue.ToString();
                }

                if (!int.TryParse(txtSifraPartnera.Text, out p))
                {
                    MessageBox.Show("Greška:\r\nNiste ispravno upisali šifru partnera čiju robu unosite.");
                    return;
                }

                string sql = "";
                try
                {
                    sql = "SELECT sifra FROM roba WHERE sifra='" + dgv.Rows[i].Cells["Sifra"].FormattedValue.ToString() + "'";
                    DataTable DT_bool = classSQL.select(sql, "roba").Tables[0];

                    if (DT_bool.Rows.Count == 0)
                    {
                        ///////////////////////////////////////AKO ROBA NE POSTOJI U BAZI///////////////////////////////////////////////

                        sql = "INSERT INTO roba (naziv,id_grupa,jm,vpc,mpc,id_zemlja_porijekla,id_zemlja_uvoza,id_partner," +
                           "sifra,ean,porez,oduzmi,nc,opis,jamstvo,akcija,link_za_slike,id_podgrupa) VALUES " +
                           "(" +
                           "'" + dgv.Rows[i].Cells["Naziv"].FormattedValue.ToString().Replace("'", " ") + "'," +
                           "'" + grupa + "'," +
                           "'KOM'," +
                           "'" + dgv.Rows[i].Cells["VPC"].FormattedValue.ToString().Replace(",", ".") + "'," +
                           "'" + dgv.Rows[i].Cells["MPC"].FormattedValue.ToString().Replace(".", ",") + "'," +
                           "'247'," +
                           "'247'," +
                           "'" + txtSifraPartnera.Text + "'," +
                           "'" + dgv.Rows[i].Cells["Sifra"].FormattedValue.ToString() + "'," +
                           "'-1'," +
                           "'" + dgv.Rows[i].Cells["pdv"].FormattedValue.ToString() + "'," +
                           "'DA'," +
                           "'0'," +
                           "'" + dgv.Rows[i].Cells["Opis"].FormattedValue.ToString().Replace("'", " ") + "'," +
                           "'" + jamstvo + "'," +
                           "'" + akcija + "'," +
                           "'" + dgv.Rows[i].Cells["LinkSlikeWeb"].FormattedValue.ToString() + "'," +
                           "'" + podgrupa + "'" +
                           ")";

                        classSQL.insert(sql);

                        sql = "INSERT INTO roba_prodaja (id_skladiste,kolicina,nc,vpc,porez,sifra) VALUES " +
                             "(" +
                             "'" + cbSkladiste.SelectedValue.ToString() + "'," +
                             "'0'," +
                             "'0'," +
                             "'" + dgv.Rows[i].Cells["VPC"].FormattedValue.ToString().Replace(",", ".") + "'," +
                             "'" + dgv.Rows[i].Cells["pdv"].FormattedValue.ToString().Replace(".", ",") + "'," +
                             "'" + dgv.Rows[i].Cells["Sifra"].FormattedValue.ToString() + "'" +
                             ")";

                        classSQL.insert(sql);
                    }
                    else
                    {
                        ///////////////////////////////////////AKO ROBA POSTOJI U BAZI///////////////////////////////////////////////

                        sql = "UPDATE roba SET " +
                        "naziv='" + dgv.Rows[i].Cells["Naziv"].FormattedValue.ToString() + "'," +
                        "id_grupa='" + grupa + "'," +
                        "jm='KOM'," +
                        "vpc='" + dgv.Rows[i].Cells["VPC"].FormattedValue.ToString().Replace(",", ".") + "'," +
                        "mpc='" + dgv.Rows[i].Cells["MPC"].FormattedValue.ToString().Replace(".", ",") + "'," +
                        "id_zemlja_porijekla='247'," +
                        "id_zemlja_uvoza='247'," +
                        "id_partner='" + txtSifraPartnera.Text + "'," +
                        "ean='-1'," +
                        "porez='" + dgv.Rows[i].Cells["pdv"].FormattedValue.ToString() + "'," +
                        "oduzmi='DA'," +
                        "nc='0'," +
                        "opis='" + dgv.Rows[i].Cells["Opis"].FormattedValue.ToString() + "'," +
                        "jamstvo='" + jamstvo + "'," +
                        "akcija='" + akcija + "'," +
                        "link_za_slike='" + dgv.Rows[i].Cells["LinkSlikeWeb"].FormattedValue.ToString() + "'," +
                        "id_podgrupa='" + podgrupa + "'" +
                        " WHERE sifra='" + dgv.Rows[i].Cells["Sifra"].FormattedValue.ToString() + "'";

                        classSQL.update(sql);

                        sql = "UPDATE roba_prodaja SET " +
                             " id_skladiste='" + cbSkladiste.SelectedValue.ToString() + "'," +
                             " kolicina='0'," +
                             " nc='0'," +
                             " vpc='" + dgv.Rows[i].Cells["VPC"].FormattedValue.ToString().Replace(",", ".") + "'," +
                             " porez='" + dgv.Rows[i].Cells["pdv"].FormattedValue.ToString().Replace(".", ",") + "'" +
                             " WHERE sifra='" + dgv.Rows[i].Cells["Sifra"].FormattedValue.ToString() + "'";

                        classSQL.update(sql);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            MessageBox.Show("Uspješno sinkronizirano!");
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
    }
}