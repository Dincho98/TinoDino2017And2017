using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmPovijestDokumenata : Form
    {
        public frmPovijestDokumenata()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPovijestDokumenata_Load(object sender, EventArgs e)
        {
            DataSet DSMT = classSQL.select("SELECT id_skladiste,skladiste FROM skladiste", "skladiste");
            cbSkl.DataSource = DSMT.Tables[0];
            cbSkl.DisplayMember = "skladiste";
            cbSkl.ValueMember = "id_skladiste";

            dtpDatumOD.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDatumDO.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

            dokumentiRefresh(sender, e);
        }

        private void stavkeRefresh(object sender, DataGridViewCellEventArgs e)
        {
            dgvStavke.DataSource = new DataTable();
            string stavke = dgvDokumenti.CurrentRow.Cells["stavke"].FormattedValue.ToString();
            if (stavke != "")
            {
                string[] red = stavke.Split(';');
                string sql = "SELECT * FROM (";

                for (int i = 0; i < red.Count(); i++)
                {
                    if (red[i] != "")
                    {
                        string[] details = red[i].Split('ˇ');
                        if (i != 0) sql += " UNION";
                        sql += " (SELECT roba.sifra AS [Šifra],roba.naziv AS [Naziv],'" + details[1].Substring(4, details[1].Length - 4) + "' AS [Količina],skladiste.skladiste AS [Skladište] FROM roba_prodaja LEFT JOIN roba ON roba.sifra=roba_prodaja.sifra LEFT JOIN skladiste ON skladiste.id_skladiste=roba_prodaja.id_skladiste WHERE roba.sifra='" + details[0].Substring(4, details[0].Length - 4) + "' AND skladiste.id_skladiste=" + details[2].Substring(4, details[2].Length - 4) + ") ";
                    }
                }
                sql += ") t;";

                dgvStavke.DataSource = classSQL.select(sql, "stavke").Tables[0];
                dgvStavke.Columns["Naziv"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void txtSifraArtikla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSifraArtikla.Text != "")
                {
                    DataSet partner = new DataSet();
                    partner = classSQL.select("SELECT sifra,naziv FROM roba WHERE sifra ='" + txtSifraArtikla.Text + "'", "roba");
                    if (partner.Tables[0].Rows.Count > 0)
                    {
                        txtSifraArtikla.Text = partner.Tables[0].Rows[0]["sifra"].ToString();
                        txtImeArtikla.Text = partner.Tables[0].Rows[0]["naziv"].ToString();
                        dtpDatumOD.Select();
                    }
                    else
                    {
                        MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                    }
                }
            }
        }

        private void dokumentiRefresh(object sender, EventArgs e)
        {
            string sqlBase = "SELECT stavke, dokument AS [Dokument], datum AS [Datum], ime||' '||prezime AS [Zaposlenik], broj_dokumenta AS [Broj Dokumenta], skladiste AS [Skladište]" +
            " FROM povijest_koristenja_dokumenata LEFT JOIN zaposlenici ON povijest_koristenja_dokumenata.id_izradio=zaposlenici.id_zaposlenik" +
            " LEFT JOIN skladiste ON povijest_koristenja_dokumenata.id_skladiste=skladiste.id_skladiste WHERE";

            if (chbSkladiste.Checked) sqlBase += " (dokument,broj_dokumenta,povijest_koristenja_dokumenata.id_skladiste) in (SELECT DISTINCT dokument,broj_dokumenta,id_skladiste FROM povijest_koristenja_dokumenata WHERE stavke LIKE '%skl:" + cbSkl.SelectedValue.ToString() + "%') AND";
            if (chbSifraArtikla.Checked) sqlBase += " (dokument,broj_dokumenta,povijest_koristenja_dokumenata.id_skladiste) in (SELECT DISTINCT dokument,broj_dokumenta,id_skladiste FROM povijest_koristenja_dokumenata WHERE stavke LIKE '%sif:" + txtSifraArtikla.Text + "%') AND";
            if (chbBrojDokumenta.Checked) sqlBase += " broj_dokumenta =" + txtBrojDokumenta.Text + " AND";

            sqlBase += " CAST(datum AS date) BETWEEN '" + dtpDatumOD.Value.ToString("yyyy-MM-dd") + "' AND '" + dtpDatumDO.Value.ToString("yyyy-MM-dd") + "' ORDER BY datum ASC;";

            dgvDokumenti.DataSource = classSQL.select(sqlBase, "dokumenti").Tables[0];
            dgvDokumenti.Columns["stavke"].Visible = false;

            dgvStavke.DataSource = new DataTable();
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w + 7, h + 7);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w - 7, h - 7);
        }

        private void dgvStavke_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDokumenti.CurrentRow.Cells["Dokument"].FormattedValue.ToString() == "Radni nalog")
            {
                frmStavkeRadnogNaloga frmst = new frmStavkeRadnogNaloga();
                frmst.sf_artikla = dgvStavke.CurrentRow.Cells["Šifra"].FormattedValue.ToString();
                frmst.ShowDialog();
            }
        }
    }
}