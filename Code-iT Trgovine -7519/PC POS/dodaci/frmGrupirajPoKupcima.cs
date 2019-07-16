using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.dodaci
{
    public partial class frmGrupirajPoKupcima : Form
    {
        public frmGrupirajPoKupcima()
        {
            InitializeComponent();
        }

        public frmKasa Kasa { get; set; }

        private void frmGrupirajPoKupcima_Load(object sender, EventArgs e)
        {
            CreateTableInSql();
            DodajArtikle();
            //this.Paint += new PaintEventHandler(Form1_Paint);
        }

        public bool odabrano = false;
        public string id_partner = "";

        private void DodajArtikle()
        {
            dodaci.frmDodajKupca dk = new frmDodajKupca();
            odabrano = false;
            id_partner = "";
            dk.frm = this;
            dk.ShowDialog();

            if (!odabrano || id_partner.ToString() == "")
            {
                this.Close();
            }

            DataTable DT = classSQL.select("SELECT * FROM trenutni_kupci ORDER BY ime_kupca", "trenutni_kupci").Tables[0];
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                dgvKupac.Rows.Add(DT.Rows[i]["ime_kupca"].ToString(), DT.Rows[i]["id"].ToString());
            }

            DataGridView dg = Kasa.dgv;
            for (int i = 0; i < dg.RowCount; i++)
            {
                string sql = "INSERT INTO trenutni_kupci_artikli (" +
                    "id_kupac," +
                    "sifra_artikla," +
                    "naziv_artikla," +
                    "mpc," +
                    "kolicina," +
                    "popust," +
                    "id_skladiste," +
                    "iznos," +
                    "oduzmi," +
                    "vpc," +
                    "porez," +
                    "jmj," +
                    "nbc" +
                    ") VALUES (" +
                    "'" + id_partner + "'," +
                    "'" + dg.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'," +
                    "'" + dg.Rows[i].Cells["naziv"].FormattedValue.ToString() + "'," +
                    "'" + dg.Rows[i].Cells["cijena"].FormattedValue.ToString().Replace(",", ".") + "'," +
                    "'" + dg.Rows[i].Cells["kolicina"].FormattedValue.ToString().Replace(",", ".") + "'," +
                    "'" + dg.Rows[i].Cells["popust"].FormattedValue.ToString().Replace(",", ".") + "'," +
                    "'" + dg.Rows[i].Cells["skladiste"].FormattedValue.ToString() + "'," +
                    "'" + dg.Rows[i].Cells["iznos"].FormattedValue.ToString().Replace(",", ".") + "'," +
                    "'" + dg.Rows[i].Cells["oduzmi"].FormattedValue.ToString() + "'," +
                    "'" + dg.Rows[i].Cells["vpc"].FormattedValue.ToString().Replace(",", ".") + "'," +
                    "'" + dg.Rows[i].Cells["porez"].FormattedValue.ToString().Replace(",", ".") + "'," +
                    "'" + dg.Rows[i].Cells["jmj"].FormattedValue.ToString() + "'," +
                    "'" + dg.Rows[i].Cells["nbc"].FormattedValue.ToString().Replace(",", ".") + "'" +
                    ")";
                classSQL.insert(sql);
            }

            int Cbr = 0;
            for (int i = 0; i < dgvKupac.RowCount; i++)
            {
                if (id_partner == dgvKupac.Rows[i].Cells["id"].FormattedValue.ToString())
                {
                    Cbr = i;
                }
            }
            if (dgvKupac.RowCount > 0)
            {
                dgvKupac.CurrentCell = dgvKupac.Rows[Cbr].Cells[0];
                FillArtikli();
            }
            OdustaniNaKasi();
        }

        private void OdustaniNaKasi()
        {
            Kasa.brRac = Kasa.brojRacuna();
            Kasa.lblBrojRac.Text = "Broj računa: " + Kasa.brRac + "/" + DateTime.Now.Year;
            Kasa.SetOnNull();
            Properties.Settings.Default.id_partner = "";
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((byte)(105)), ((byte)(170)), ((byte)(197)));
            Color y = System.Drawing.Color.FromArgb(((byte)(40)), ((byte)(109)), ((byte)(135)));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        //------------------------------------------------------KREIRANJE TABLICA-------------------------------------------------------------------------------

        #region KREIRANJE TABLICA

        private void CreateTableInSql()
        {
            DataTable DTremote = classSQL.select("select table_name from information_schema.tables where TABLE_TYPE <> 'VIEW'", "Table").Tables[0];

            ///--------------------------TRENUTNI KUPCI-----------------------------
            DataRow[] dataROW = DTremote.Select("table_name = 'trenutni_kupci'");
            string sql;
            if (dataROW.Length == 0)
            {
                sql = "CREATE TABLE trenutni_kupci" +
                "(" +
                  "id serial NOT NULL," +
                  "ime_kupca character varying," +
                  "CONSTRAINT trenutni_kupci_pkey PRIMARY KEY (id))";

                classSQL.select(sql, "trenutni_kupci");
            }

            ///--------------------------ARTIKLI-----------------------------
            dataROW = DTremote.Select("table_name = 'trenutni_kupci_artikli'");
            if (dataROW.Length == 0)
            {
                sql = "CREATE TABLE trenutni_kupci_artikli" +
                "(" +
                  "id serial NOT NULL," +
                  "id_kupac integer," +
                  "sifra_artikla character varying," +
                  "naziv_artikla character varying," +
                  "mpc numeric," +
                  "kolicina numeric," +
                  "popust numeric," +
                  "id_skladiste integer," +
                  "iznos numeric," +
                  "oduzmi character varying," +
                  "porez numeric," +
                  "vpc numeric," +
                  "nbc numeric," +
                  "jmj  character varying," +
                  "CONSTRAINT trenutni_kupci_artikli_pkey PRIMARY KEY (id))";

                classSQL.select(sql, "trenutni_kupci");
            }
        }

        #endregion KREIRANJE TABLICA

        //------------------------------------------------------KREIRANJE TABLICA KRAJ--------------------------------------------------------------------------

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnZavrsiRacun_Click(object sender, EventArgs e)
        {
            Kasa.PaintRows(dgv);
            for (int i = 0; i < dgv.RowCount; i++)
            {
                Kasa.dgv.Rows.Add(
                    dgv.Rows[i].Cells["sifra_artikla"].FormattedValue.ToString(),
                    dgv.Rows[i].Cells["naziv"].FormattedValue.ToString(),
                    dgv.Rows[i].Cells["jmj"].FormattedValue.ToString(),
                    dgv.Rows[i].Cells["skladiste"].FormattedValue.ToString(),
                    dgv.Rows[i].Cells["cijena"].FormattedValue.ToString(),
                    dgv.Rows[i].Cells["kolicina"].FormattedValue.ToString(),
                    dgv.Rows[i].Cells["popust"].FormattedValue.ToString(),
                    dgv.Rows[i].Cells["iznos"].FormattedValue.ToString(),
                    dgv.Rows[i].Cells["oduzmi"].FormattedValue.ToString(),
                    dgv.Rows[i].Cells["porez"].FormattedValue.ToString(),
                    dgv.Rows[i].Cells["vpc"].FormattedValue.ToString(),
                    "",
                    dgv.Rows[i].Cells["nbc"].FormattedValue.ToString()
                );
            }
            Kasa.PaintRows(dgv);

            classSQL.delete("DELETE FROM trenutni_kupci WHERE id='" + dgvKupac.CurrentRow.Cells["id"].FormattedValue.ToString() + "'");
            classSQL.delete("DELETE FROM trenutni_kupci_artikli WHERE id_kupac='" + dgvKupac.CurrentRow.Cells["id"].FormattedValue.ToString() + "'");

            this.Close();
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Dali ste sigurni da želite obrisati kupca?\r\nBrisanjem kupca brišete i sve artikle koji su vezani za njega.", "Brisanje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                classSQL.delete("DELETE FROM trenutni_kupci WHERE id='" + dgvKupac.CurrentRow.Cells["id"].FormattedValue.ToString() + "'");
                classSQL.delete("DELETE FROM trenutni_kupci_artikli WHERE id_kupac='" + dgvKupac.CurrentRow.Cells["id"].FormattedValue.ToString() + "'");
                dgvKupac.Rows.RemoveAt(dgvKupac.CurrentRow.Index);
                dgv.Rows.Clear();
            }
        }

        private void btnObrisiStavku_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Dali ste sigurni da želite obrisati stavku?", "Brisanje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                classSQL.delete("DELETE FROM trenutni_kupci_artikli WHERE sifra_artikla='" + dgv.CurrentRow.Cells["sifra_artikla"].FormattedValue.ToString() + "' AND id_kupac='" + dgvKupac.CurrentRow.Cells["id"].FormattedValue.ToString() + "'");
                dgv.Rows.RemoveAt(dgv.CurrentRow.Index);
            }
        }

        private void dgvKupac_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            FillArtikli();
        }

        private void FillArtikli()
        {
            DataTable DT = classSQL.select("SELECT * FROM trenutni_kupci_artikli WHERE id_kupac='" + dgvKupac.CurrentRow.Cells["id"].FormattedValue.ToString() + "' ORDER BY id", "").Tables[0];

            dgv.Rows.Clear();
            foreach (DataRow row in DT.Rows)
            {
                dgv.Rows.Add(
                    row["sifra_artikla"].ToString(),
                    row["naziv_artikla"].ToString(),
                    row["kolicina"].ToString(),
                    row["popust"].ToString(),
                    row["id_skladiste"].ToString(),
                    row["iznos"].ToString(),
                    row["vpc"].ToString(),
                    row["oduzmi"].ToString(),
                    row["porez"].ToString(),
                    row["nbc"].ToString(),
                    row["jmj"].ToString(),
                    row["mpc"].ToString()
                 );
            }
        }
    }
}