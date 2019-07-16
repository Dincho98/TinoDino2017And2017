using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmSviRobniDobropisi : Form
    {
        private DataTable DTdobropisi { get; set; }
        private DataTable DTfiltered = new DataTable();
        public int IdDobropis { get; set; }

        public frmSviRobniDobropisi()
        {
            InitializeComponent();
        }

        private void FrmSviRobniDobropisi_Load(object sender, EventArgs e)
        {
            FillZaposlenikComboBox();
            dtpDatumOd.Value = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);
            dtpDatumDo.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            DTdobropisi = Global.Database.GetRobniDobropisi();
            if (DTdobropisi.Rows.Count > 0)
            {
                DTfiltered.Columns.Add("broj_dobropis");
                DTfiltered.Columns.Add("partner");
                DTfiltered.Columns.Add("skladiste");
                DTfiltered.Columns.Add("datum");
                DTfiltered.Columns.Add("ukupno");
                FillGrid(DTdobropisi);
            }
        }

        /// <summary>
        /// Populates "zaposlenici" ComboBox with data from database
        /// </summary>
        private void FillZaposlenikComboBox()
        {
            DataTable DTzaposlenici = Global.Database.GetZaposleniciNaziv();
            DataRow row = DTzaposlenici.NewRow();
            row["id_zaposlenik"] = 0;
            row["naziv"] = "-- Svi zaposlenici --";
            DTzaposlenici.Rows.Add(row);

            // Sort DataTable
            DataView dv = DTzaposlenici.DefaultView;
            dv.Sort = "id_zaposlenik ASC";
            DTzaposlenici = dv.ToTable();

            cbZaposlenik.ValueMember = "id_zaposlenik";
            cbZaposlenik.DisplayMember = "naziv";
            cbZaposlenik.DataSource = DTzaposlenici;
            cbZaposlenik.SelectedValue = 0;
        }

        /// <summary>
        /// Populates DataGridView with given DataTable
        /// </summary>
        /// <param name="dataTable">Data</param>
        private void FillGrid(DataTable dataTable)
        {
            ClearGrid();
            foreach (DataRow row in dataTable.Rows)
            {
                dgv.Rows.Add();
                int rowIndex = dgv.Rows.Count - 1;

                dgv.Rows[rowIndex].Cells["broj_dobropis"].Value = row["broj_dobropis"].ToString();
                dgv.Rows[rowIndex].Cells["Partner"].Value = row["ime_tvrtke"].ToString();
                dgv.Rows[rowIndex].Cells["skladiste"].Value = row["skladiste"].ToString();
                dgv.Rows[rowIndex].Cells["datum"].Value = row["datum"].ToString();
                dgv.Rows[rowIndex].Cells["ukupno"].Value = row["ukupno"].ToString() + " kn";
            }

            if (dgv.Rows.Count > 0)
                dgv.Rows[0].Selected = true;
        }

        /// <summary>
        /// Removes all rows from DataGridView
        /// </summary>
        private void ClearGrid()
        {
            dgv.Rows.Clear();
            dgv.Refresh();
        }

        private void BtnUredi_Click(object sender, EventArgs e)
        {
            int rowIndex = dgv.CurrentRow.Index;
            IdDobropis = Convert.ToInt32(dgv.Rows[rowIndex].Cells["broj_dobropis"].Value.ToString());
            Close();
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            int rowIndex = dgv.CurrentRow.Index;
            Report.RobniDobropis.ReportRobniDobropis form = new Report.RobniDobropis.ReportRobniDobropis
            {
                IdDobropis = Convert.ToInt32(dgv.Rows[rowIndex].Cells["broj_dobropis"].Value.ToString()),
                Date = DateTime.Parse(dgv.Rows[rowIndex].Cells["datum"].FormattedValue.ToString())
            };
            form.Show();
        }

        private void pbTrazi_Click(object sender, EventArgs e)
        {
            if (DTdobropisi.Rows.Count > 0)
            {
                if (DTfiltered.Rows.Count > 0)
                    DTfiltered.Clear();
                List<DataRow> dobropisList = DTdobropisi.AsEnumerable().ToList();
                if (!string.IsNullOrWhiteSpace(tbBrojDobropisa.Text))
                    dobropisList = dobropisList.Where(it => it.Field<long>("broj_dobropis").ToString() == tbBrojDobropisa.Text).ToList();
                if (!string.IsNullOrWhiteSpace(tbSifraPartnera.Text))
                    dobropisList = dobropisList.Where(it => it.Field<long>("id_partner").ToString() == tbSifraPartnera.Text).ToList();
                if (cbZaposlenik.SelectedIndex != 0)
                    dobropisList = dobropisList.Where(it => it.Field<int>("id_izradio").ToString() == cbZaposlenik.SelectedIndex.ToString()).ToList();

                dobropisList = dobropisList.Where(it => it.Field<DateTime>("datum") >= dtpDatumOd.Value && it.Field<DateTime>("datum") <= dtpDatumDo.Value).ToList();

                foreach (DataRow row in dobropisList)
                {
                    DataRow newRow = DTfiltered.NewRow();
                    newRow["broj_dobropis"] = row["broj_dobropis"].ToString();
                    newRow["partner"] = row["ime_tvrtke"].ToString();
                    newRow["skladiste"] = row["skladiste"].ToString();
                    newRow["datum"] = row["datum"].ToString();
                    newRow["ukupno"] = row["ukupno"].ToString();
                    DTfiltered.Rows.Add(newRow);
                }
                FillGrid(DTfiltered);
            }
        }
        private void PbTraziPartner_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                    tbSifraPartnera.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                else
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
            }
        }

        private void BtnIzlaz_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}