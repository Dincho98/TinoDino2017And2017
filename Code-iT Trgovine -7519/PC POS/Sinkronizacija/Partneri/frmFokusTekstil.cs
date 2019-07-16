using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace PCPOS.Sinkronizacija.Partneri
{
    public partial class frmFokusTekstil : Form
    {
        private DataTable DT = new DataTable();
        private DataTable DTsveVrijednosti = new DataTable();

        public frmFokusTekstil()
        {
            InitializeComponent();

            if (DT.Columns.Count == 0)
            {
                DT.Columns.Add("link");
                DT.Columns.Add("cijena");
            }

            if (DTsveVrijednosti.Columns.Count == 0)
            {
                DTsveVrijednosti.Columns.Add("sifra");
                DTsveVrijednosti.Columns.Add("naziv");
                DTsveVrijednosti.Columns.Add("opis");
                DTsveVrijednosti.Columns.Add("grupa");
                DTsveVrijednosti.Columns.Add("podgrupa");
                DTsveVrijednosti.Columns.Add("brand");
                DTsveVrijednosti.Columns.Add("jamstvo");
                DTsveVrijednosti.Columns.Add("vpc");
                DTsveVrijednosti.Columns.Add("mpc");
                DTsveVrijednosti.Columns.Add("akcije");
                DTsveVrijednosti.Columns.Add("linkovi");
            }
        }

        private WebBrowser web = new WebBrowser();

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
            }
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
                DataTable DTpodgrupa = classSQL.select("SELECT id_podgrupa FROM podgrupa WHERE naziv='" + dgv.Rows[i].Cells["Podgrupa"].FormattedValue.ToString() + "'", "").Tables[0];
                string podgrupa = DTpodgrupa.Rows[0][0].ToString();
                //GRUPA
                DataTable DTgrupa = classSQL.select("SELECT id_grupa FROM grupa WHERE grupa='" + dgv.Rows[i].Cells["grupa_"].FormattedValue.ToString() + "'", "").Tables[0];
                string grupa = DTgrupa.Rows[0][0].ToString();
                //BRAND
                DataTable DTbrand = classSQL.select("SELECT id_manufacturers FROM manufacturers WHERE manufacturers='" + dgv.Rows[i].Cells["Brand"].FormattedValue.ToString() + "'", "").Tables[0];
                string Brand = DTgrupa.Rows[0][0].ToString();

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

                        sql = "INSERT INTO roba (naziv,id_grupa,jm,vpc,mpc,id_zemlja_porijekla,id_zemlja_uvoza,id_partner,id_manufacturers," +
                           "sifra,ean,porez,oduzmi,nc,opis,jamstvo,akcija,link_za_slike,id_podgrupa,brand) VALUES " +
                           "(" +
                           "'" + dgv.Rows[i].Cells["Naziv"].FormattedValue.ToString() + "'," +
                           "'" + grupa + "'," +
                           "'KOM'," +
                           "'" + dgv.Rows[i].Cells["VPC"].FormattedValue.ToString().Replace(",", ".") + "'," +
                           "'" + dgv.Rows[i].Cells["MPC"].FormattedValue.ToString().Replace(".", ",") + "'," +
                           "'247'," +
                           "'247'," +
                           "'" + txtSifraPartnera.Text + "'," +
                           "'" + Brand + "'," +
                           "'" + dgv.Rows[i].Cells["Sifra"].FormattedValue.ToString() + "'," +
                           "'-1'," +
                           "'" + dgv.Rows[i].Cells["pdv"].FormattedValue.ToString() + "'," +
                           "'DA'," +
                           "'0'," +
                           "'" + dgv.Rows[i].Cells["Opis"].FormattedValue.ToString() + "'," +
                           "'" + jamstvo + "'," +
                           "'" + akcija + "'," +
                           "'" + dgv.Rows[i].Cells["LinkSlikeWeb"].FormattedValue.ToString() + "'," +
                           "'" + podgrupa + "'," +
                           "'" + dgv.Rows[i].Cells["Brand"].FormattedValue.ToString() + "'" +
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
                        "id_manufacturers='" + Brand + "'," +
                        "ean='-1'," +
                        "porez='" + dgv.Rows[i].Cells["pdv"].FormattedValue.ToString() + "'," +
                        "oduzmi='DA'," +
                        "nc='0'," +
                        "opis='" + dgv.Rows[i].Cells["Opis"].FormattedValue.ToString() + "'," +
                        "jamstvo='" + jamstvo + "'," +
                        "akcija='" + akcija + "'," +
                        "link_za_slike='" + dgv.Rows[i].Cells["LinkSlikeWeb"].FormattedValue.ToString() + "'," +
                        "id_podgrupa='" + podgrupa + "'," +
                        "brand='" + dgv.Rows[i].Cells["Brand"].FormattedValue.ToString() + "'" +
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

        private void btnUcitajSaWeba_Click(object sender, EventArgs e)
        {
            bgUcitaj.RunWorkerAsync();
        }

        private void bgUcitaj_DoWork(object sender, DoWorkEventArgs e)
        {
            if (funkcija == 2)
            {
                UzmiArtikliDetaljno();
            }
            else
            {
                webBrowser1.DocumentText = UzmiStringZaLogin();
            }
        }

        private void bgUcitaj_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (funkcija == 2)
            {
                if (bgUcitaj.CancellationPending != true)
                {
                    progressBar1.Style = ProgressBarStyle.Blocks;
                    double d = (Convert.ToDouble(e.ProgressPercentage) / Convert.ToDouble(DT.Rows.Count)) * 100;
                    lblStatus.Text = "Učitavam artikl: " + e.ProgressPercentage + " od " + DT.Rows.Count.ToString();
                    progressBar1.Value = (int)d;
                }
            }
        }

        private void bgUcitaj_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetInDgv(DTsveVrijednosti);
        }

        private void Navigate(string address)
        {
            if (string.IsNullOrEmpty(address)) return;
            if (address.Equals("about:blank")) return;
            if (!address.StartsWith("http://") && !address.StartsWith("https://"))
            {
                address = "http://" + address;
            }
            try
            {
                webBrowser1.Navigate(new Uri(address));
            }
            catch (System.UriFormatException)
            {
                return;
            }
        }

        private string UzmiStringZaLogin()
        {
            return "<form id=\"UserLoginForm\" action=\"https://www.fokus.hr/Prijava/\" method=\"post\">" +
                 " <input type=\"text\" name=\"username\" id=\"username\" class=\"linpt\" value=\"powercomputers\"> " +
                 " <input type=\"password\" class=\"linpt\" name=\"password\" id=\"password\" value=\"prodajapcpro\"> " +
                 " <input type=\"submit\" value=\"Ulaz\" class=\"btn10\" /> " +
                 " <input type=\"hidden\" name=\"action\" value=\"login\" id=\"action\"/> " +
                 " </form>";
        }

        private int funkcija = -1;
        private int brojac_na_jednoj_stranici = 0;
        private int brojac_stranice = 1;
        private int brojac_linkova_detaljno;
        private string _sifra, _naziv, _opis, _cijena, _slika, _grupa, _podgrupa, html;

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //FUNKCIJA -1 koristi se za LOGIN
            if (funkcija == -1)
            {
                funkcija = 0;
            }
            //FUNKCIJA 0 KORISI SE ZA LISTU ARTIKLA NA STRANICI 1
            else if (funkcija == 0)
            {
                Navigate("https://www.fokus.hr/catalog.php?a=v&order=gbn&g=8.7.1.&b=&n=&orderType=&orderDirection=&start=" + brojac_stranice.ToString());
                funkcija = 1;
            }
            //FUNKCIJA 1 KORISTI SE ZA PRIKAZ SVIH ARTIKLA x cca 31 stranica
            else if (funkcija == 1)
            {
                if (webBrowser1.Document != null)
                {
                    int x = 0, y = 0;
                    html = webBrowser1.DocumentText;

                    //AKO JE PROŠLO KROZ SVE STRANICE U FOKUSU 1,2,3,4,5....81
                    if (html.IndexOf("<td style=\"height:66px;width:230px\">\r\n\t\t    <a href=\"", y) == -1)
                    {
                        funkcija = 2;
                        if (DT.Rows.Count > 0)
                        {
                            Navigate(DT.Rows[0]["link"].ToString());
                            brojac_linkova_detaljno++;
                            return;
                        }
                    }
                    lblStatus.Text = "Učitava se stranica: " + brojac_stranice.ToString();

                    string cijena = "", link = "";

                    while (true)
                    {
                        y = html.IndexOf("<tr>", y);
                        if (y == -1) { break; }

                        x = html.IndexOf("<td style=\"height:66px;width:230px\">\r\n\t\t    <a href=\"", y) + 53;
                        y = html.IndexOf("\">", x);
                        link = "https://www.fokus.hr" + new string(html.ToArray(), x, y - x);

                        x = html.IndexOf("(", y) + 1;
                        y = html.IndexOf(")", x);
                        cijena = new string(html.ToArray(), x, y - x);
                        cijena = cijena.Replace("\t", "");
                        cijena = cijena.Replace("\n", "");
                        cijena = cijena.Replace("\r", "");
                        cijena = cijena.Replace("kn", "");
                        cijena = cijena.Trim();
                        DT.Rows.Add(link, cijena);
                        brojac_na_jednoj_stranici++;
                    }

                    if (brojac_na_jednoj_stranici > 0)
                    {
                        Navigate("https://www.fokus.hr/catalog.php?a=v&order=gbn&g=8.7.1.&b=&n=&orderType=&orderDirection=&start=" + brojac_stranice.ToString());
                        brojac_stranice++;
                    }
                    brojac_na_jednoj_stranici = 0;
                }
            }
            else if (funkcija == 2)
            {
                bgUcitaj.RunWorkerAsync();
            }
        }

        private void UzmiArtikliDetaljno()
        {
            int cou = 0;
            foreach (DataRow r in DT.Rows)
            {
                try
                {
                    bgUcitaj.ReportProgress(cou + 1);

                    int x = 0, y = 0;
                    html = GetHtml(r["link"].ToString());

                    x = html.IndexOf("<div class=\"artk_box\"><span>&#352;ifra:</span>", y) + 46;
                    y = html.IndexOf("</div>", x);
                    _sifra = new string(html.ToArray(), x, y - x);

                    x = html.IndexOf("<div class=\"artk_box\"><span>Naziv:</span><br/> \r\n\t\t", y) + 51;
                    y = html.IndexOf("</div>", x);
                    _naziv = new string(html.ToArray(), x, y - x);
                    _naziv = _naziv.Replace("<span style='color:#8B0101;'>", "");
                    _naziv = _naziv.Replace("\n", "");
                    _naziv = _naziv.Replace("\r", "");
                    _naziv = _naziv.Replace("\t", "");
                    _naziv = _naziv.Replace("</span>", "");
                    _naziv = _naziv.Replace("<br>", "  ");

                    x = html.IndexOf("<img class=\"alignright2\" src=\"", y) + 31;
                    y = html.IndexOf("\"", x);
                    _slika = "https://www.fokus.hr/" + new string(html.ToArray(), x, y - x);

                    x = html.IndexOf("<div class=\"artkl_descr\">", y) + 27;
                    y = html.IndexOf("</div>", x);
                    _opis = new string(html.ToArray(), x, y - x);

                    decimal vpc, mpc;

                    decimal.TryParse(r["cijena"].ToString(), out vpc);
                    vpc = vpc * 1.25m;

                    mpc = (vpc * (decimal)1.25);

                    _grupa = "Fokus";

                    string[] arrPodgrupa = _naziv.Split(' ');
                    if (arrPodgrupa.Length > 1)
                    {
                        _podgrupa = arrPodgrupa[0] + " " + arrPodgrupa[1] + " " + arrPodgrupa[2];
                    }
                    else
                    {
                        _podgrupa = "Nema podgrupe";
                    }

                    if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\SlikeFokusTmp")) Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\SlikeFokusTmp");
                    using (WebClient Client = new WebClient())
                    {
                        try { Client.DownloadFile(_slika, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\SlikeFokusTmp\" + "FK_" + _sifra.Trim() + ".jpg"); }
                        catch { }
                    }

                    DTsveVrijednosti.Rows.Add("FK_" + _sifra.Trim(), _naziv, _opis, _grupa, _podgrupa, "Fokus", "1", Math.Round(vpc, 3).ToString("#0.00"), Math.Round(mpc, 3).ToString("#0.00"), "0", _slika);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                cou++;
            }
        }

        private void SetInDgv(DataTable DT)
        {
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                DataTable DTgrupa = classSQL.select("SELECT * FROM grupa WHERE grupa='" + DT.Rows[i]["grupa"].ToString() + "'", "").Tables[0];
                DataTable DTpodgrupa = classSQL.select("SELECT * FROM podgrupa WHERE naziv='" + DT.Rows[i]["podgrupa"].ToString() + "'", "").Tables[0];
                DataTable DTbrand = classSQL.select("SELECT * FROM manufacturers WHERE manufacturers='" + DT.Rows[i]["brand"].ToString() + "'", "").Tables[0];

                if (DTgrupa.Rows.Count == 0)
                {
                    classSQL.insert("INSERT INTO grupa (grupa) VALUES ('" + DT.Rows[i]["grupa"].ToString() + "')");
                    DTgrupa = classSQL.select("SELECT * FROM grupa WHERE grupa='" + DT.Rows[i]["grupa"].ToString() + "' ORDER BY id_grupa DESC LIMIT 1", "").Tables[0];
                    create();
                }

                if (DTpodgrupa.Rows.Count == 0)
                {
                    classSQL.insert("INSERT INTO podgrupa (naziv,id_grupa) VALUES ('" + DT.Rows[i]["podgrupa"].ToString() + "','" + DTgrupa.Rows[0]["id_grupa"].ToString() + "')");
                    DTpodgrupa = classSQL.select("SELECT * FROM podgrupa WHERE naziv='" + DT.Rows[i]["podgrupa"].ToString() + "' ORDER BY id_podgrupa DESC LIMIT 1", "").Tables[0];
                    create();
                }

                if (DTbrand.Rows.Count == 0)
                {
                    classSQL.insert("INSERT INTO manufacturers (manufacturers) VALUES ('" + DT.Rows[i]["brand"].ToString() + "')");
                    create();
                }

                dgv.Rows.Add(
                    DT.Rows[i]["sifra"].ToString(),
                    DT.Rows[i]["naziv"].ToString(),
                    DTpodgrupa.Rows[0][0].ToString(),
                    DT.Rows[i]["sifra"].ToString(),
                    DT.Rows[i]["opis"].ToString(),
                    DT.Rows[i]["grupa"].ToString(),
                    DT.Rows[i]["podgrupa"].ToString(),
                    DT.Rows[i]["brand"].ToString(),
                    DT.Rows[i]["jamstvo"].ToString(),
                    DT.Rows[i]["vpc"].ToString(),
                    DT.Rows[i]["mpc"].ToString(),
                    10,
                    DT.Rows[i]["akcije"].ToString(),
                    DT.Rows[i]["linkovi"].ToString(),
                    25
                );
            }
        }

        public string GetHtml(string urlAddr)
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
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("windows-1250")))
                {
                    result = sr.ReadToEnd();
                    sr.Close();
                }
                return result;
            }
        }
    }
}