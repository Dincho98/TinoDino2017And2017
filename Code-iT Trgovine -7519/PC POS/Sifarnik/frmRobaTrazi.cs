using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmRobaTrazi : Form
    {
        private DataSet DSgrupa = new DataSet();
        private DataSet DSdobavljac = new DataSet();
        private DataSet DSmanufacturers = new DataSet();
        private DataSet dsPostavke = new DataSet();

        public frmRobaTrazi()
        {
            InitializeComponent();
        }

        private bool remoteDBboll = false;
        private INIFile ini = new INIFile();

        public int idSkladiste { get; internal set; }

        private void frmRobaTrazi_Load(object sender, EventArgs e)
        {
            dsPostavke = classSQL.select_settings("select * from postavke", "postavke");

            int p1 = 0;
            int.TryParse(Properties.Settings.Default.trazi_robno_prvih.ToString(), out p1);
            nuPrvih.Value = p1;

            string skladiste = "";

            string sql = string.Format(@"
select x.ID, x.naziv
from (
    select 0 as ID, 'Sva skladišta' as naziv, 0 as sort
    union
    select id_skladiste as id, skladiste as naziv, 1 as sort
    from skladiste
    where aktivnost = 'DA'
) x
order by x.sort;");

            DataSet dsSkladiste = classSQL.select(sql, "skladiste");
            if (dsSkladiste != null && dsSkladiste.Tables.Count > 0 && dsSkladiste.Tables[0] != null && dsSkladiste.Tables[0].Rows.Count > 0)
            {
                cmbSkladista.DisplayMember = "naziv";
                cmbSkladista.ValueMember = "ID";
                cmbSkladista.DataSource = dsSkladiste.Tables[0];
                if (idSkladiste > 0)
                {
                    cmbSkladista.SelectedValue = idSkladiste;
                }
                else
                {
                    cmbSkladista.SelectedValue = Class.Postavke.id_default_skladiste;
                }

                if (Convert.ToInt32(cmbSkladista.SelectedValue) != 0)
                {
                    skladiste = " and roba_prodaja.id_skladiste = '" + Convert.ToInt32(cmbSkladista.SelectedValue) + "'";
                }
            }

            if (Properties.Settings.Default.artikli_koji_su_na_skl)
            {
                chbRNS.Checked = true;
            }
            else
            {
                chbRNS.Checked = false;
            }

            //if (idSkladiste > 0)
            //    skladiste = " and roba_prodaja.id_skladiste = '" + idSkladiste + "'";

            //PaintRows(dataGridView2);
            PaintRows(dataGridView1);
            txtIme.Select();
            Properties.Settings.Default.id_roba = "";
            Properties.Settings.Default.idSkladiste = "";
            Properties.Settings.Default.Save();

            DataTable dt = classSQL.select_settings("SELECT * FROM aktivnost_podataka", "aktivnost_podataka").Tables[0];
            /*chbRNS.Checked = false;*/
            chbRNS.Visible = dt.Rows[0]["boolRobno"].ToString() != "1" ? false : true;

            if (classSQL.remoteConnectionString != "")
            {
                remoteDBboll = true;
            }

            string limit = "";
            string limitRemote = "";
            if (remoteDBboll != true)
            {
                limit = "TOP(" + nuPrvih.Value.ToString() + ")";
            }
            else
            {
                limitRemote = "LIMIT " + nuPrvih.Value.ToString() + "";
            }

            string rns = "";
            if (chbRNS.Checked)
            {
                rns = "AND CAST(REPLACE(roba_prodaja.kolicina,',','.') AS numeric)>0";
            }
            else
            {
                rns = "";
            }

            sql = string.Format(@"SELECT {0}
                roba.sifra as [Šifra], roba.naziv AS [Naziv], partners.ime_tvrtke as [Dobavljač], grupa.grupa as [Grupa],
                roba_prodaja.kolicina AS [Količina], skladiste.skladiste AS [Skladište], roba_prodaja.nc as [Nabavna], roba.vpc as [Veleprodajna], roba.mpc as [Maloprodajna]
                FROM roba
                LEFT JOIN grupa ON roba.id_grupa = grupa.id_grupa
                LEFT JOIN partners ON roba.id_partner = partners.id_partner
                LEFT JOIN roba_prodaja ON roba.sifra=roba_prodaja.sifra
                LEFT JOIN skladiste ON skladiste.id_skladiste = roba_prodaja.id_skladiste
                WHERE roba.sifra !~* '!serial' {1}{2}
                ORDER BY CAST(id_roba as numeric)
                {3}",
                limit,
                rns,
                skladiste,
                limitRemote);

            dataGridView2.DataSource = classSQL.select(sql, "roba").Tables[0];
            if (ini.Read("INDIVIDUALNO", "backstage") == "1")
            {
                dataGridView2.Columns["Naziv"].Width = 300;
                dataGridView2.Columns["grupa"].Visible = false;
                dataGridView2.Columns["dobavljač"].Visible = false;
                dataGridView2.Columns["nabavna"].Visible = false;
                dataGridView2.Columns["veleprodajna"].Visible = false;
            }
            SetSkladisteKojeNePostoji();
            SetDecimalInDgv(dataGridView2, "Nabavna", "Veleprodajna", "Maloprodajna");
            PaintRows(dataGridView2);
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
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

        private void textBox1_TextChanged(object sender, EventArgs e)

        {
            string rns = "", limit = "", limitRemote = "", where = "", skladiste = "";

            if (chbRNS.Checked)
            {
                rns = "AND CAST(REPLACE(roba_prodaja.kolicina,',','.') AS numeric)>0";
            }

            if (Convert.ToInt32(cmbSkladista.SelectedValue) != 0)
            {
                skladiste = " and roba_prodaja.id_skladiste = '" + cmbSkladista.SelectedValue + "'";
            }

            if (idSkladiste > 0)
            {
                skladiste = " and roba_prodaja.id_skladiste = '" + idSkladiste + "'";
            }

            if (remoteDBboll != true)
            {
                limit = " TOP(" + nuPrvih.Value.ToString() + ") ";
                where = "roba.naziv LIKE '%" + txtIme.Text + "%'";
            }
            else
            {
                limitRemote = " LIMIT " + nuPrvih.Value.ToString();
                where = "roba.naziv ~* '" + txtIme.Text + "'";
            }

            string sql = "SELECT " + limit + " roba.sifra as [Šifra],roba.naziv AS [Naziv],partners.ime_tvrtke as [Dobavljač], grupa.grupa as [Grupa], " +
                "  roba_prodaja.kolicina AS [Količina], skladiste.skladiste AS [Skladište], roba_prodaja.nc as [Nabavna], roba.vpc as [Veleprodajna], roba.mpc as [Maloprodajna] " +
                " FROM roba LEFT JOIN" +
                " grupa ON roba.id_grupa = grupa.id_grupa LEFT JOIN" +
                " partners ON roba.id_partner = partners.id_partner " +
                " LEFT JOIN roba_prodaja ON roba.sifra=roba_prodaja.sifra" +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=roba_prodaja.id_skladiste" +
                "  WHERE " + where + rns + skladiste + " AND roba.sifra !~* '!serial' ORDER BY CAST(id_roba as numeric) " + limitRemote;

            dataGridView2.DataSource = classSQL.select(sql, "roba").Tables[0];
            //PaintRows(dataGridView2);

            dataGridView2.Columns[1].Width = 300;
            if (ini.Read("INDIVIDUALNO", "backstage") == "1")
            {
                dataGridView2.Columns["Naziv"].Width = 300;
                dataGridView2.Columns["grupa"].Visible = false;
                dataGridView2.Columns["dobavljač"].Visible = false;
                dataGridView2.Columns["nabavna"].Visible = false;
                dataGridView2.Columns["veleprodajna"].Visible = false;
            }

            SetSkladisteKojeNePostoji();
            SetDecimalInDgv(dataGridView2, "Nabavna", "Veleprodajna", "Maloprodajna");
            PaintRows(dataGridView2);
        }

        private void SetSkladisteKojeNePostoji()
        {
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                if (dataGridView2.Rows[i].Cells["Skladište"].FormattedValue.ToString() == "")
                {
                    //dataGridView2.Rows[i].Cells["Skladište"].Value = "------------";
                    dataGridView2.Rows[i].Cells["Količina"].Value = "0";
                }
            }

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells["Skladište"].FormattedValue.ToString() == "")
                {
                    //dataGridView1.Rows[i].Cells["Skladište"].Value = "------------";
                    dataGridView1.Rows[i].Cells["Količina"].Value = "0";
                }
            }
        }

        private void SetDecimalInDgv(DataGridView dg, string column, string column1, string column2)
        {
            //dg.Columns["Količina"].DefaultCellStyle.Format = "n3";
            dg.Columns["Količina"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dg.Columns["Količina"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (column != "NE")
            {
                dg.Columns[column].DefaultCellStyle.Format = "n2";
                dg.Columns[column].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns[column].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (column1 != "NE")
            {
                dg.Columns[column1].DefaultCellStyle.Format = "n2";
                dg.Columns[column1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns[column1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (column2 != "NE")
            {
                dg.Columns[column2].DefaultCellStyle.Format = "n2";
                dg.Columns[column2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns[column2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            for (int i = 0; i < dg.RowCount; i++)
            {
                try
                {
                    dg.Rows[i].Cells["Količina"].Value = Convert.ToDouble(dg.Rows[i].Cells["Količina"].FormattedValue.ToString()).ToString("#,0.000");
                    if (column != "NE")
                    {
                        decimal n;
                        decimal.TryParse(dg.Rows[i].Cells[column].FormattedValue.ToString(), out n);
                        dg.Rows[i].Cells[column].Value = n.ToString();
                        //dg.Rows[i].Cells[column].Value = n.ToString("#0.00");
                    }

                    if (column1 != "NE")
                    {
                        dg.Rows[i].Cells[column1].Value = Convert.ToDouble(dg.Rows[i].Cells[column1].FormattedValue.ToString()).ToString();
                        //dg.Rows[i].Cells[column1].Value = Convert.ToDouble(dg.Rows[i].Cells[column1].FormattedValue.ToString()).ToString("#0.00");
                    }

                    if (column2 != "NE")
                    {
                        dg.Rows[i].Cells[column2].Value = Convert.ToDouble(dg.Rows[i].Cells[column2].FormattedValue.ToString()).ToString();
                        //dg.Rows[i].Cells[column2].Value = Convert.ToDouble(dg.Rows[i].Cells[column2].FormattedValue.ToString()).ToString("#0.00");
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int br = e.RowIndex;
                string id = dataGridView2.Rows[br].Cells[0].FormattedValue.ToString();
                string skl = dataGridView2.Rows[br].Cells["skladiste"].FormattedValue.ToString();
                string id_skl = classSQL.select("Select id_skladiste from skladiste where skladiste = '" + skl + "'", "id_skladiste").Tables[0].Rows[0]["id_skladiste"].ToString();
                Properties.Settings.Default.id_roba = id;
                Properties.Settings.Default.idSkladiste = id_skl;
                string skladiste = VratiIdSkladista(dataGridView2.Rows[br].Cells["Skladište"].Value.ToString());
                Properties.Settings.Default.idSkladiste = skladiste;
                Properties.Settings.Default.Save();
                this.Close();
            }
            catch (Exception)
            {
            }
        }

        private void CBset()
        {
            //CB grupe
            DSgrupa = classSQL.select("SELECT * FROM grupa ORDER BY grupa", "grupa");
            cbGrupa.DataSource = DSgrupa.Tables[0];
            cbGrupa.DisplayMember = "grupa";
            cbGrupa.ValueMember = "id_grupa";

            //CB dobavljač
            DSdobavljac = classSQL.select("SELECT * FROM partners ORDER BY ime_tvrtke", "partners");
            cbPartner.DataSource = DSdobavljac.Tables[0];
            cbPartner.DisplayMember = "ime_tvrtke";
            cbPartner.ValueMember = "id_partner";

            //CB proizvođač
            DSmanufacturers = classSQL.select("SELECT * FROM manufacturers ORDER BY manufacturers", "manufacturers");
            cbProizvodac.DataSource = DSmanufacturers.Tables[0];
            cbProizvodac.DisplayMember = "manufacturers";
            cbProizvodac.ValueMember = "id_manufacturers";
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            string query = "";
            string query1 = "";
            string query_grupa = "";
            string query_dobavljac = "";
            string query_proizvodac = "";

            if (sifra.Checked)
            {
                query = " roba.sifra LIKE '%" + txtSifra.Text + "%' ";
            }
            else if (ime.Checked)
            {
                query = " roba.naziv LIKE '%" + txtSifra.Text + "%'";
            }

            if (chGrupa.Checked)
            {
                query_grupa = " AND roba.id_grupa=" + cbGrupa.SelectedValue;
            }

            if (chProizvodac.Checked)
            {
                query_proizvodac = " AND roba.id_manufacturers=" + cbProizvodac.SelectedValue;
            }

            if (chDobavljac.Checked)
            {
                query_dobavljac = " AND roba.id_partner=" + cbPartner.SelectedValue;
            }

            string limit = "";
            string limitRemote = "";

            if (remoteDBboll == true)
            {
                limitRemote = " LIMIT 200 ";
            }
            else
            {
                limit = " TOP(200) ";
            }

            string sql = "SELECT " + limit + " roba.sifra as [Šifra],roba.naziv as Naziv,partners.ime_tvrtke as [Dobavljač], grupa.grupa as Grupa, " +
            "  roba_prodaja.kolicina AS [Količina], skladiste.skladiste AS [Skladište], roba_prodaja.nc as [Nabavna], roba.vpc as [Veleprodajna], roba.mpc as [Maloprodajna]," +
            " manufacturers.manufacturers as [Proizvođač] FROM roba LEFT JOIN" +
            " grupa ON roba.id_grupa = grupa.id_grupa LEFT JOIN" +
            " partners ON roba.id_partner = partners.id_partner LEFT JOIN" +
            " manufacturers ON roba.id_manufacturers = manufacturers.id_manufacturers " +
            " LEFT JOIN roba_prodaja ON roba.sifra=roba_prodaja.sifra" +
            " LEFT JOIN skladiste ON skladiste.id_skladiste=roba_prodaja.id_skladiste" +
            " WHERE" + query + query1 + query_grupa + query_proizvodac + query_dobavljac + " AND roba.sifra !~* '!serial' ORDER BY CAST(id_roba as numeric)" + limitRemote;

            dataGridView1.DataSource = classSQL.select(sql, "roba").Tables[0];
            if (ini.Read("INDIVIDUALNO", "backstage") == "1")
            {
                dataGridView1.Columns["Naziv"].Width = 300;
                dataGridView1.Columns["grupa"].Visible = false;
                dataGridView1.Columns["dobavljač"].Visible = false;
                dataGridView1.Columns["nabavna"].Visible = false;
                dataGridView1.Columns["veleprodajna"].Visible = false;
            }
            PaintRows(dataGridView1);
            SetSkladisteKojeNePostoji();
            SetDecimalInDgv(dataGridView1, "Nabavna", "Veleprodajna", "Maloprodajna");
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int br = e.RowIndex;
                if (br > -1)
                {
                    string id = dataGridView2.Rows[br].Cells[0].FormattedValue.ToString();
                    string skl = dataGridView2.Rows[br].Cells["Skladište"].FormattedValue.ToString();
                    provjeri_artikl(id);
                    string id_skl = "";
                    try
                    { id_skl = classSQL.select("Select id_skladiste from skladiste where skladiste = '" + skl + "'", "id_skladiste").Tables[0].Rows[0]["id_skladiste"].ToString(); }
                    catch { }

                    Properties.Settings.Default.id_roba = id;
                    Properties.Settings.Default.idSkladiste = id_skl;
                    string skladiste = VratiIdSkladista(dataGridView2.Rows[br].Cells["Skladište"].Value.ToString());
                    Properties.Settings.Default.idSkladiste = skladiste;
                    Properties.Settings.Default.Save();

                    this.Close();
                }
            }
            catch (Exception)
            {
            }
            PaintRows(dataGridView2);
        }

        private void provjeri_artikl(string sifra)
        {
            string skla_uk = "Select * from skladiste";
            DataTable DTpopisskl = classSQL.select(skla_uk, "skladiste").Tables[0];
            for (int i = 0; i < DTpopisskl.Rows.Count; i++)
            {
                string skladiste = DTpopisskl.Rows[i]["id_skladiste"].ToString();
                SQL.ClassSkladiste.provjeri_roba_prodaja(sifra, skladiste);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int br = e.RowIndex;
                string id = dataGridView1.Rows[br].Cells[0].FormattedValue.ToString();
                Properties.Settings.Default.id_roba = id;
                string skladiste = VratiIdSkladista(dataGridView2.Rows[br].Cells["Skladište"].Value.ToString());
                Properties.Settings.Default.idSkladiste = skladiste;
                Properties.Settings.Default.Save();
                this.Close();
            }
            catch (Exception)
            {
            }
        }

        private void txtSifra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnTrazi.PerformClick();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmRobaUsluge ru = new frmRobaUsluge();
            ru.Show();
        }

        private void txtTraziPremaSifri_TextChanged_1(object sender, EventArgs e)
        {
            string rns = "";
            if (chbRNS.Checked)
            {
                rns = "AND CAST(REPLACE(roba_prodaja.kolicina,',','.') AS numeric)>0";
            }
            else
            {
                rns = "";
            }

            string skladiste = "";
            if (Convert.ToInt32(cmbSkladista.SelectedValue) != 0)
            {
                skladiste = " and roba_prodaja.id_skladiste = '" + cmbSkladista.SelectedValue + "'";
            }

            if (idSkladiste > 0)
                skladiste = " and roba_prodaja.id_skladiste = '" + idSkladiste + "'";

            string limit = "";
            string limitRemote = "";
            string where = "";
            if (remoteDBboll != true)
            {
                limit = " TOP(200) ";
                where = "roba.sifra LIKE '%" + txtTraziPremaSifri.Text + "%'";
            }
            else
            {
                limitRemote = " LIMIT 200 ";
                where = "(roba.sifra ~* '" + txtTraziPremaSifri.Text + "' OR roba.ean ~* '" + txtTraziPremaSifri.Text + "')";
            }

            string sql = "SELECT " + limit + " roba.sifra as [Šifra],roba.naziv AS [Naziv],partners.ime_tvrtke as [Dobavljač], grupa.grupa as [Grupa], " +
                "  roba_prodaja.kolicina AS [Količina], skladiste.skladiste AS [Skladište], roba_prodaja.nc as [Nabavna], roba.vpc as [Veleprodajna], roba.mpc as [Maloprodajna] " +
                " FROM roba LEFT JOIN" +
                " grupa ON roba.id_grupa = grupa.id_grupa LEFT JOIN" +
                " partners ON roba.id_partner = partners.id_partner " +
                " LEFT JOIN roba_prodaja ON roba.sifra=roba_prodaja.sifra" +
                " LEFT JOIN skladiste ON skladiste.id_skladiste=roba_prodaja.id_skladiste" +
                "  WHERE " + where + rns + skladiste + " AND roba.sifra !~* '!serial' ORDER BY CAST(id_roba as numeric)" + limitRemote;

            dataGridView2.DataSource = classSQL.select(sql, "roba").Tables[0];

            //PaintRows(dataGridView2);

            dataGridView2.Columns[1].Width = 300;
            if (ini.Read("INDIVIDUALNO", "backstage") == "1")
            {
                dataGridView2.Columns["Naziv"].Width = 300;
                dataGridView2.Columns["grupa"].Visible = false;
                dataGridView2.Columns["dobavljač"].Visible = false;
                dataGridView2.Columns["nabavna"].Visible = false;
                dataGridView2.Columns["veleprodajna"].Visible = false;
            }
            SetSkladisteKojeNePostoji();
            SetDecimalInDgv(dataGridView2, "Nabavna", "Veleprodajna", "Maloprodajna");
            PaintRows(dataGridView2);
        }

        private void txtIme_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (dataGridView2.RowCount > 0)
                {
                    int br = dataGridView2.CurrentRow.Index; ;
                    string id = dataGridView2.Rows[br].Cells[0].FormattedValue.ToString();
                    Properties.Settings.Default.id_roba = id;
                    string skladiste = VratiIdSkladista(dataGridView2.Rows[br].Cells["Skladište"].Value.ToString());
                    Properties.Settings.Default.idSkladiste = skladiste;
                    Properties.Settings.Default.Save();
                    this.Close();
                }
            }

            if (e.KeyData == Keys.Up)
            {
                int curent = dataGridView2.CurrentRow != null ? dataGridView2.CurrentRow.Index : 0;
                if (curent > 0)
                    dataGridView2.CurrentCell = dataGridView2.Rows[curent - 1].Cells[0];
            }

            if (e.KeyData == Keys.Down)
            {
                int curent = dataGridView2.CurrentRow != null ? dataGridView2.CurrentRow.Index : 0;
                if (curent < dataGridView2.RowCount - 1)
                    dataGridView2.CurrentCell = dataGridView2.Rows[curent + 1].Cells[0];
            }
        }

        private string VratiIdSkladista(string skladiste)
        {
            string sql = "select id_skladiste from skladiste where skladiste = '" + skladiste + "'";

            DataTable dt = classSQL.select(sql, "roba").Tables[0];

            string skl = "1";

            if (dt.Rows.Count > 0)
            {
                skl = dt.Rows[0][0].ToString();
            }
            else
            {
                sql = "select id_skladiste from skladiste order by id_skladiste limit 1";

                dt = classSQL.select(sql, "roba").Tables[0];

                if (dt.Rows.Count > 0)
                {
                    skl = dt.Rows[0][0].ToString();
                }
                else
                {
                    MessageBox.Show("Ne postoji skladište!");
                }
            }

            return skl;
        }

        private void chbIspisSvihArtikla_CheckedChanged(object sender, EventArgs e)
        {
            if (chbIspisSvihArtikla.Checked)
            {
                DataTable dt = classSQL.select_settings("SELECT * FROM aktivnost_podataka", "aktivnost_podataka").Tables[0];
                chbRNS.Checked = false;
                chbRNS.Visible = dt.Rows[0]["boolRobno"].ToString() != "1" ? false : true;

                if (classSQL.remoteConnectionString != "")
                {
                    remoteDBboll = true;
                }

                string limit = "";
                string limitRemote = "";
                if (remoteDBboll != true)
                {
                    //limit = " TOP(200) ";
                }
                else
                {
                    //limitRemote = " LIMIT 200 ";
                }
                string sql = "SELECT " + limit + " roba.sifra as [Šifra],roba.naziv AS [Naziv],partners.ime_tvrtke as [Dobavljač], grupa.grupa as [Grupa], " +
                    "  roba_prodaja.kolicina AS [Količina], skladiste.skladiste AS [Skladište], roba_prodaja.nc as [Nabavna], roba.vpc as [Veleprodajna], roba.mpc as [Maloprodajna] " +
                    " FROM roba LEFT JOIN" +
                    " grupa ON roba.id_grupa = grupa.id_grupa LEFT JOIN" +
                    " partners ON roba.id_partner = partners.id_partner " +
                    " LEFT JOIN roba_prodaja ON roba.sifra=roba_prodaja.sifra" +
                    " LEFT JOIN skladiste ON skladiste.id_skladiste=roba_prodaja.id_skladiste " + " WHERE roba.sifra !~* '!serial' ORDER BY CAST(id_roba as numeric)" +
                    limitRemote;
                dataGridView2.DataSource = classSQL.select(sql, "roba").Tables[0];

                if (ini.Read("INDIVIDUALNO", "backstage") == "1")
                {
                    dataGridView2.Columns["Naziv"].Width = 300;
                    dataGridView2.Columns["grupa"].Visible = false;
                    dataGridView2.Columns["dobavljač"].Visible = false;
                    dataGridView2.Columns["nabavna"].Visible = false;
                    dataGridView2.Columns["veleprodajna"].Visible = false;
                }
                SetSkladisteKojeNePostoji();
                SetDecimalInDgv(dataGridView2, "Nabavna", "Veleprodajna", "Maloprodajna");
                PaintRows(dataGridView2);
            }
        }

        private void nuPrvih_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.trazi_robno_prvih = nuPrvih.Value.ToString();
            Properties.Settings.Default.Save();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                CBset();
            }
        }

        private void chbRNS_CheckedChanged(object sender, EventArgs e)
        {
            if (chbRNS.Checked)
            {
                Properties.Settings.Default.artikli_koji_su_na_skl = true;
            }
            else
            {
                Properties.Settings.Default.artikli_koji_su_na_skl = false;
            }
            Properties.Settings.Default.Save();
        }
    }
}