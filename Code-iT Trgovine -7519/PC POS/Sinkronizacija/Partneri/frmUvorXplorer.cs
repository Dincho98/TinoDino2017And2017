using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Xml;

namespace PCPOS.Sinkronizacija.Partneri
{
    public partial class frmUvorXplorer : Form
    {
        public frmUvorXplorer()
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

        private void loadXML()
        {
            XmlTextReader reader = new XmlTextReader("http://www.xplorerlife.hr/Handlers/WebShopXml.ashx");

            DataSet ds = new DataSet();
            ds.ReadXml(reader);
            DataTable DT = ds.Tables[0];
            reader.Close();

            DT.Columns["Kategorija1"].ColumnName = "Grupa";
            DT.Columns["Kategorija2"].ColumnName = "Podgrupa";
            DT.Columns["Naziv"].ColumnName = "naziv";
            DT.Columns["Sifra"].ColumnName = "sifra";
            DT.Columns["Preporucena-cijena"].ColumnName = "MPC";
            DT.Columns["Dobavljivost"].ColumnName = "Stanje";
            DT.Columns["Slika1"].ColumnName = "LinkSlikeWeb";

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                DataTable DTgrupa = classSQL.select("SELECT * FROM grupa WHERE grupa='" + DT.Rows[i]["Grupa"].ToString() + "'", "").Tables[0];
                DataTable DTpodgrupa = classSQL.select("SELECT * FROM podgrupa WHERE naziv='" + DT.Rows[i]["Podgrupa"].ToString() + "'", "").Tables[0];
                DataTable DTbrand = classSQL.select("SELECT * FROM manufacturers WHERE manufacturers='Xplorer'", "").Tables[0];

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

                if (DTbrand.Rows.Count == 0)
                {
                    classSQL.insert("INSERT INTO manufacturers (manufacturers) VALUES ('Xplorer')");
                    create();
                }

                decimal veleprodajno = 0;

                try
                {
                    decimal pps = (Convert.ToDecimal(DT.Rows[i]["Porez"].ToString()) * 100) / (100 + Convert.ToDecimal(DT.Rows[i]["Porez"].ToString()));

                    veleprodajno = Convert.ToDecimal(DT.Rows[i]["MPC"].ToString()) - (Convert.ToDecimal(DT.Rows[i]["MPC"].ToString()) * pps / 100);
                }
                catch { }

                dgv.Rows.Add(
                    DT.Rows[i]["sifra"].ToString(),
                    DT.Rows[i]["naziv"].ToString(),
                    DTpodgrupa.Rows[0][0].ToString(),
                    DT.Rows[i]["sifra"].ToString(),
                    DT.Rows[i]["Opis"].ToString() + DT.Rows[i]["Specifikacija"].ToString(),
                    DT.Rows[i]["Grupa"].ToString(),
                    DT.Rows[i]["Podgrupa"].ToString(),
                    "X-PLORER",
                    1,
                    veleprodajno,
                    DT.Rows[i]["MPC"].ToString(),
                    DT.Rows[i]["Stanje"].ToString(),
                    0,
                    DT.Rows[i]["LinkSlikeWeb"].ToString(),
                    DT.Rows[i]["Porez"].ToString()
                );
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
                           "'4'," +
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
                        "id_partner='4'," +
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

        private void btnUcitaj_Click(object sender, EventArgs e)
        {
            loadXML();
        }
    }
}