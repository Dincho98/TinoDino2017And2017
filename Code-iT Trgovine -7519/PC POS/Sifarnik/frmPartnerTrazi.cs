using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmPartnerTrazi : Form
    {
        public string prenos_broj_racuna { get; set; }
        public bool _pozivaKartoteku { get; set; }
        public bool _pozivapregledkartoteke { get; set; }
        public bool vecodabrano { get; set; }
        public bool kartoteka_ulj { get; set; }
        public frmKasa formaKasa { get; set; }
        private DataSet DSpartneri = new DataSet();

        public frmPartnerTrazi()
        {
            InitializeComponent();
        }

        private INIFile ini = new INIFile();

        private void popuni_grid()
        {
            TraziPartnera();
            /*string top = "";
            string remote = "";
            string where = "";
            if (classSQL.remoteConnectionString != "")
            {
				remote = " LIMIT 500";
                where = "ime_tvrtke ~* '" + txtIme1.Text + "'";
            }
            else
            {
                where = "ime_tvrtke LIKE '%" + txtIme1.Text + "%'";
				top = " TOP(500) ";
            }

            DSpartneri = classSQL.select("SELECT " + top + " partners.id_partner AS ID, partners.ime_tvrtke AS [Tvrtka],"+
				"partners.oib AS [OIB], zemlja.zemlja AS [Država], grad.grad AS [Grad], djelatnosti.ime_djelatnosti AS [Djelatnost],"+
				"partners.ime as [Ime], partners.prezime as [Prezime]" +
				" FROM partners " +
				"LEFT JOIN grad ON partners.id_grad = grad.id_grad " +
				"LEFT JOIN djelatnosti ON partners.id_djelatnost = djelatnosti.id_djelatnost " +
				"LEFT JOIN zemlja ON partners.id_zemlja = zemlja.id_zemlja " + remote + "", "partners");
            dataGridView2.DataSource = DSpartneri.Tables[0];

			VisibleTrueFalse();

            PaintRows(dataGridView2);*/
        }

        private void VisibleTrueFalse()
        {
            if (ini.Read("INDIVIDUALNO", "backstage") == "1")
            {
                dataGridView2.Columns["Tvrtka"].Visible = false;
                dataGridView2.Columns["OIB"].Visible = false;
                dataGridView2.Columns["Država"].Visible = false;
                dataGridView2.Columns["Djelatnost"].Visible = false;
                dataGridView2.Columns["ID"].Visible = false;
            }
        }

        private void frmPartnerTrazi_Load(object sender, EventArgs e)
        {
            popuni_grid();
            PaintRows(dataGridView2);
            PaintRows(dataGridView1);
            Properties.Settings.Default.id_partner = "";
            Properties.Settings.Default.Save();
            fillCB();
            txtIme1.Select();

            DataTable DTtvrtka = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "postavke").Tables[0];
            if (DTtvrtka.Rows[0]["oib"].ToString() == Class.Postavke.OIB_PC1)
            {
                chbKorisnikPrograma.Visible = true;
                chbUgovor.Visible = true;
                chbWebUred.Visible = true;
                chbBivsiKorisnik.Visible = true;
                chbTablet.Visible = true;
                chbPCPOS.Visible = true;
                chbCaffe.Visible = true;
                chbResort.Visible = true;
                chbGodisnjeOdr.Visible = true;
                chbVipInt.Visible = true;
                chbOdrzavanje.Visible = true;
                chbNemaOdr.Visible = true;
            }

            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void GoreDole_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (dataGridView2.RowCount > 0)
                {
                    int br = dataGridView2.CurrentRow.Index; ;
                    string id = dataGridView2.Rows[br].Cells[0].FormattedValue.ToString();
                    Properties.Settings.Default.id_partner = id;
                    Properties.Settings.Default.Save();
                    this.Close();
                }
            }

            if (e.KeyData == Keys.Up)
            {
                int curent = dataGridView2.CurrentRow.Index;
                if (curent > 0)
                    dataGridView2.CurrentCell = dataGridView2.Rows[curent - 1].Cells[0];
            }

            if (e.KeyData == Keys.Down)
            {
                int curent = dataGridView2.CurrentRow.Index;
                if (curent < dataGridView2.RowCount - 1)
                    dataGridView2.CurrentCell = dataGridView2.Rows[curent + 1].Cells[0];
            }
        }

        private void PaintRows(DataGridView dg)
        {
            int br = 0;
            for (int i = 0; i < dg.Rows.Count; i++)
            {
                if (br == 0)
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
                    br++;
                }
                else
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    br = 0;
                }
            }
            DataGridViewRow row = dg.RowTemplate;
            row.Height = 25;
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            string query_top = "";
            string query_djelatnost = "";
            string query_grad = "";
            string query_drazava = "";

            if (CHime.Checked)
            {
                query_top = " WHERE partners.ime_tvrtke LIKE '%" + txtTekst.Text + "%'";
            }
            else if (CHsifra.Checked)
            {
                int idPartner = 0;
                if (int.TryParse(txtTekst.Text, out idPartner) && idPartner > 0)
                {
                    query_top = " WHERE partners.id_partner ='" + txtTekst.Text + "'";
                }
                else
                {
                    MessageBox.Show("Pogrešan unos za šifru partnera.");
                    return;
                }
            }
            else if (CHoib.Checked)
            {
                query_top = " WHERE partners.oib = '" + txtTekst.Text + "'";
            }

            if (chDjelatnost.Checked)
            {
                query_djelatnost = " AND partners.id_djelatnost='" + cbDjelatnost.SelectedValue + "'";
            }

            if (chDrzava.Checked)
            {
                query_drazava = " AND partners.id_zemlja='" + cbDrzava.SelectedValue + "'";
            }

            if (chGrad.Checked)
            {
                query_grad = " AND partners.id_grad='" + cbGrad.SelectedValue + "'";
            }

            string top = "";
            string remote = "";
            //string where = "";
            if (classSQL.remoteConnectionString != "")
            {
                remote = " LIMIT 500";
            }
            else
            {
                top = " TOP(500) ";
            }

            DSpartneri = classSQL.select("SELECT " + top + " partners.id_partner AS ID, partners.ime_tvrtke AS [Tvrtka], partners.oib AS [OIB], zemlja.zemlja AS [Država], grad.grad AS [Grad], djelatnosti.ime_djelatnosti AS [Djelatnost] FROM partners " +
            "LEFT JOIN grad ON partners.id_grad = grad.id_grad " +
            "LEFT JOIN djelatnosti ON partners.id_djelatnost = djelatnosti.id_djelatnost " +
            "LEFT JOIN zemlja ON partners.id_zemlja = zemlja.id_zemlja " + query_top + query_drazava + query_grad + query_djelatnost + "ORDER BY partners.ime_tvrtke" + remote, "partners");
            dataGridView1.DataSource = DSpartneri.Tables[0];

            VisibleTrueFalse();

            PaintRows(dataGridView1);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TraziPartnera();
        }

        private void fillCB()
        {
            //CB grad
            DataSet DSgrad = classSQL.select("SELECT * FROM grad ORDER BY grad", "grad");
            cbGrad.DataSource = DSgrad.Tables[0];
            cbGrad.DisplayMember = "grad";
            cbGrad.ValueMember = "id_grad";

            //CB djelatnosti
            DataSet DSdjelatnosti = classSQL.select("SELECT * FROM djelatnosti ORDER BY ime_djelatnosti", "djelatnosti");
            cbDjelatnost.DataSource = DSdjelatnosti.Tables[0];
            cbDjelatnost.DisplayMember = "ime_djelatnosti";
            cbDjelatnost.ValueMember = "id_djelatnost";

            //CB dražave
            DataSet DSdrazava = classSQL.select("SELECT * FROM zemlja ORDER BY zemlja", "zemlja");
            cbDrzava.DataSource = DSdrazava.Tables[0];
            cbDrzava.DisplayMember = "zemlja";
            cbDrzava.ValueMember = "id_zemlja";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int br = e.RowIndex;
                Properties.Settings.Default.id_partner = dataGridView1.Rows[br].Cells[0].FormattedValue.ToString();
                Properties.Settings.Default.Save();
                this.Close();
            }
            catch (Exception)
            {
            }
        }

        public string vrijednost_celije { get; set; }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count < 1)
                    return;

                vrijednost_celije = dataGridView2.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString();
                if (_pozivaKartoteku)
                {
                    string part = dataGridView2.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString();
                    Kartoteka.galerija novo = new Kartoteka.galerija();
                    string popuni_gal = "SELECT * FROM partners WHERE id_partner = '" + part + "'";
                    DataTable DTpopunigaleriju = classSQL.select(popuni_gal, "popuni_galeriju").Tables[0];
                    novo.partner_id = part.ToString();
                    novo.ime_kupca = DTpopunigaleriju.Rows[0]["ime"].ToString();
                    novo.prezime_kupca = DTpopunigaleriju.Rows[0]["prezime"].ToString();
                    novo.ime_tvrtke = DTpopunigaleriju.Rows[0]["ime_tvrtke"].ToString();
                    novo.broj_racuna = (Convert.ToInt32(prenos_broj_racuna)).ToString();
                    novo.partner = this;
                    novo.ShowDialog();
                }
                else if (_pozivapregledkartoteke)
                {
                    string part = dataGridView2.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString();
                    Kartoteka.galerija novo = new Kartoteka.galerija();
                    string popuni_gal = "SELECT * FROM partners WHERE id_partner = '" + part + "'";
                    DataTable DTpopunigaleriju = classSQL.select(popuni_gal, "popuni_galeriju").Tables[0];
                    novo.partner_id = part.ToString();
                    novo.ime_kupca = DTpopunigaleriju.Rows[0]["ime"].ToString();
                    novo.prezime_kupca = DTpopunigaleriju.Rows[0]["prezime"].ToString();
                    novo.ime_tvrtke = DTpopunigaleriju.Rows[0]["ime_tvrtke"].ToString();
                    novo.broj_racuna = "";
                    novo.partner = this;
                    novo.ShowDialog();
                }
                else
                {
                    try
                    {
                        //int col = dataGridView2.CurrentCell.ColumnIndex;
                        //int row = dataGridView2.CurrentCell.RowIndex;

                        //string celija_id_partnera = dataGridView2.Rows[row].Cells["id"].FormattedValue.ToString();
                        ////string celija_id_partnera = dataGridView2.Rows[row].Cells["id"].FormattedValue.ToString();
                        //DataTable krono = new DataTable();

                        //krono = classSQL.select("SELECT ime, prezime, ime_tvrtke, id_partner FROM partners WHERE id_partner = '" + celija_id_partnera + "'  ", "podaci").Tables[0];
                        //MessageBox.Show(krono.Rows[0]["ime"].ToString());
                        //formaKasa.ime_partnera = krono.Rows[0]["ime"].ToString();
                        //formaKasa.prezime_partnera = krono.Rows[0]["prezime"].ToString();
                        //formaKasa.ime_tvrtke = krono.Rows[0]["ime_tvrtke"].ToString();
                        //formaKasa.id_partnera = krono.Rows[0]["id_partner"].ToString();
                        int br = e.RowIndex;
                        Properties.Settings.Default.id_partner = dataGridView2.Rows[br].Cells[0].FormattedValue.ToString();
                        Properties.Settings.Default.Save();
                        this.Close();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch
            { }
        }

        private void txtTekst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTrazi.PerformClick();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (kartoteka_ulj == false)
            {
                Sifarnik.frmAddPartners np = new Sifarnik.frmAddPartners();
                np.Show();
            }
            else
            {
                Sifarnik.frmAddPartnersKarto addp = new Sifarnik.frmAddPartnersKarto();
                addp._pozivaKartoteku = _pozivaKartoteku;
                addp.ShowDialog();
                if (_pozivaKartoteku && (addp.vrati_id != null))
                {
                    string part = addp.vrati_id;
                    Kartoteka.galerija novo = new Kartoteka.galerija();
                    string popuni_gal = "SELECT * FROM partners WHERE id_partner = '" + part + "'";
                    DataTable DTpopunigaleriju = classSQL.select(popuni_gal, "popuni_galeriju").Tables[0];
                    novo.partner_id = part.ToString();
                    novo.ime_kupca = DTpopunigaleriju.Rows[0]["ime"].ToString();
                    novo.prezime_kupca = DTpopunigaleriju.Rows[0]["prezime"].ToString();
                    novo.ime_tvrtke = DTpopunigaleriju.Rows[0]["ime_tvrtke"].ToString();
                    novo.broj_racuna = (Convert.ToInt32(prenos_broj_racuna)).ToString();
                    novo.partner = this;
                    novo.ShowDialog();
                }
            }
        }

        private void txtPartnerPremaSifri_TextChanged(object sender, EventArgs e)
        {
            TraziPartnera();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Sifarnik.frmAddPartners n = new Sifarnik.frmAddPartners();
            n.ShowDialog();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            TraziPartnera();
        }

        private void textPrezime_TextChanged(object sender, EventArgs e)
        {
            TraziPartnera();
        }

        private void TraziPartnera()
        {
            string where = "";

            if (txtIme1.Text.Length > 0) { where += " AND partners.ime_tvrtke ~* '" + txtIme1.Text + "'"; }
            if (txtPartnerPremaSifri.Text.Length > 0) { where += " AND partners.id_partner ='" + txtPartnerPremaSifri.Text + "'"; }
            if (textIme.Text.Length > 0) { where += " AND partners.ime ~* '" + textIme.Text + "'"; }
            if (textPrezime.Text.Length > 0) { where += " AND partners.prezime ~* '" + textPrezime.Text + "'"; }

            if (chbKorisnikPrograma.Checked == true) { where += " AND partners_odrzavanje.nas_program = '1'"; }
            if (chbVipInt.Checked == true) { where += " AND partners_odrzavanje.internet_kol > '0'"; }
            if (chbOdrzavanje.Checked == true) { where += " AND partners_odrzavanje.odrzavanje_kol > '0'"; }
            if (chbWebUred.Checked == true) { where += " AND partners_odrzavanje.web_ured = '1'"; }
            if (chbGodisnjeOdr.Checked == true) { where += " AND partners_odrzavanje.godisnje_odr = '1'"; }
            if (chbBivsiKorisnik.Checked == true) { where += " AND partners_odrzavanje.bivsi_korisnik = '1'"; }
            if (chbUgovor.Checked == true) { where += " AND partners_odrzavanje.ugovor = '1'"; }
            if (chbTablet.Checked == true) { where += " AND partners_odrzavanje.tablet = '1'"; }
            if (chbCaffe.Checked == true) { where += " AND partners_odrzavanje.pccaffe = '1'"; }
            if (chbPCPOS.Checked == true) { where += " AND partners_odrzavanje.pcpos = '1'"; }
            if (chbResort.Checked == true) { where += " AND partners_odrzavanje.resort = '1'"; }
            if (chbNemaOdr.Checked == true) { where += " AND partners_odrzavanje.nas_program = '1' AND partners_odrzavanje.odrzavanje_kol = '0'  AND partners_odrzavanje.godisnje_odr = '0'"; }

            string sql = "SELECT  partners.id_partner AS ID, partners.ime_tvrtke AS [Tvrtka]," +
                    " partners.oib AS [OIB], grad.grad AS [Grad],partners.adresa AS [Adresa], djelatnosti.ime_djelatnosti AS [Djelatnost]," +
                    " partners.ime as [Ime], partners.prezime as [Prezime],partners.tel AS [TEL],partners.mob AS [MOB] " +
                    " FROM partners " +
                    " LEFT JOIN grad ON partners.id_grad = grad.id_grad " +
                    " LEFT JOIN partners_odrzavanje ON partners_odrzavanje.id_partner=partners.id_partner" +
                    " LEFT JOIN djelatnosti ON partners.id_djelatnost = djelatnosti.id_djelatnost " +
                    " LEFT JOIN zemlja ON partners.id_zemlja = zemlja.id_zemlja WHERE partners.id_partner IS NOT NULL " + where + " ORDER BY partners.ime_tvrtke LIMIT 200";

            DSpartneri = classSQL.select(sql, "partners");
            dataGridView2.DataSource = DSpartneri.Tables[0];
        }

        private void FunkCheckedChanged(object sender, EventArgs e)
        {
            TraziPartnera();
        }

        private void frms(object sender, DataGridViewCellEventArgs e)
        {
        }

        #region btnSpremiExcel EXCEL

        private void btnSpremiExcel_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (Directory.Exists(folderBrowserDialog1.SelectedPath))
                {
                    SpremiSveExcel();
                }
            }
        }

        private void SpremiSveExcel()
        {
            DataTable DTp = classSQL.select(@"SELECT
                            id_partner AS ""ID"",
                            ime_tvrtke AS ""TVRTKA"",
                            oib AS ""OIB"",
                            grad.grad AS ""GRAD"",
                            adresa AS ""ADRESA"",
                            tel AS ""TELEFON"",
                            mob AS ""MOBITEL""
                            FROM partners
                            LEFT JOIN grad ON grad.id_grad=partners.id_grad", "partneri").Tables[0];

            string csvDoc = "ID;Tvrtka;OIB;Grad;Adresa;Telefon;Mobitel\n";
            foreach (DataRow r in DTp.Rows)
            {
                csvDoc += r["ID"].ToString() + ";" + r["TVRTKA"].ToString() + ";" +
                    r["OIB"].ToString() + ";" +
                    r["GRAD"].ToString() + ";" +
                    r["ADRESA"].ToString() + ";" +
                    r["TELEFON"].ToString() + ";" +
                    r["MOBITEL"].ToString() + ";\n";
            }

            File.WriteAllText(folderBrowserDialog1.SelectedPath + "/Partneri.csv", csvDoc, Encoding.GetEncoding(1250));
            MessageBox.Show("Spremljeno!");
        }

        #endregion btnSpremiExcel EXCEL
    }
}