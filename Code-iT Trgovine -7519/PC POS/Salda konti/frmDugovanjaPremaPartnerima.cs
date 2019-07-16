using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Salda_konti
{
    public partial class frmDugovanjaPremaPartnerima : Form
    {
        public frmDugovanjaPremaPartnerima()
        {
            InitializeComponent();
        }

        private void frmDugovanjaPremaPartnerima_Load(object sender, EventArgs e)
        {
            SetPartners("");
            IzracunajRazliku();
            this.dgvPartners.Sort(this.dgvPartners.Columns[7], ListSortDirection.Descending);

            if (dgvPartners.Rows.Count > 0)
            {
                SetDocumentsFromPartners(dgvPartners.Rows[0].Cells["ID"].FormattedValue.ToString());
            }

            dgvPartners.Columns[0].Width = 150;
            dgvPartners.Columns[1].Width = 350;

            //dgvPartners.CurrentCell.Selected = false;
            // dgvDocuments.CurrentCell.Selected = false;
        }

        private void IzracunajRazliku()
        {
            foreach (DataGridViewRow r in dgvPartners.Rows)
            {
                decimal potrazivanje, uplaceno;
                decimal.TryParse(r.Cells["Potraživanja"].FormattedValue.ToString(), out potrazivanje);
                decimal.TryParse(r.Cells["Plaćeno"].FormattedValue.ToString(), out uplaceno);
                r.Cells["Razlika"].Value = Math.Round((potrazivanje - uplaceno), 3).ToString("#0.00");

                if (uplaceno == 0)
                {
                    r.DefaultCellStyle.BackColor = Color.MistyRose;
                }
                else if (uplaceno == potrazivanje)
                {
                    r.DefaultCellStyle.BackColor = Color.PaleGreen;
                }
                else
                {
                    r.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void SetPartners(string ime_partnera)
        {
            string where = "";
            if (ime_partnera.Length > 3)
            {
                where = " AND partners.ime_tvrtke ~* '" + txtTrazeniPartner.Text + "' ";
            }
            else if (ime_partnera.Length > 0 && ime_partnera.Length <= 3)
            {
                return;
            }

            string sql = "SELECT partners.id_partner AS [ID],partners.ime_tvrtke AS [Tvrtka],partners.tel AS [Telefon],partners.ime AS [Ime],partners.prezime AS [Prezime], " +
            " (SELECT SUM(ukupno) FROM fakture WHERE fakture.id_fakturirati = partners.id_partner AND id_nacin_placanja<>'1') AS [Potraživanja],   " +
            " (SELECT SUM(uplaceno) FROM salda_konti WHERE salda_konti.id_partner=partners.id_partner) AS [Plaćeno],'0' AS [Razlika]" +
            " FROM partners WHERE partners.id_partner IN (SELECT DISTINCT id_fakturirati FROM fakture WHERE fakture.id_fakturirati=partners.id_partner)  " + where + " LIMIT 50";

            DataTable DT = classSQL.select(sql, "par").Tables[0];
            dgvPartners.DataSource = DT;
            ColorRowsInDgvPartners();

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Format = "N2";
            dgvPartners.Columns["Potraživanja"].DefaultCellStyle = style;
            dgvPartners.Columns["Potraživanja"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPartners.Columns["Potraživanja"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvPartners.Columns["Plaćeno"].DefaultCellStyle = style;
            dgvPartners.Columns["Plaćeno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPartners.Columns["Plaćeno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvPartners.Columns["Razlika"].DefaultCellStyle = style;
            dgvPartners.Columns["Razlika"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPartners.Columns["Razlika"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void SetDocumentsFromPartners(string id_partner)
        {
            string sql = "SELECT " +
                " fakture.broj_fakture AS [Broj]," +
                " fakture.date as [Datum], " +
                " fakture.ukupno as [Iznos fakture], " +
                " (SELECT SUM(uplaceno) FROM salda_konti WHERE salda_konti.broj_dokumenta=fakture.broj_fakture AND salda_konti.id_ducan=fakture.id_ducan AND salda_konti.id_kasa=fakture.id_kasa AND salda_konti.godina=CAST(fakture.godina_fakture AS INT)) AS [Uplačeno]" +
                " FROM fakture " +
                " LEFT JOIN partners ON partners.id_partner=fakture.id_fakturirati WHERE fakture.id_fakturirati='" + id_partner + "'  AND id_nacin_placanja<>'1'";

            DataTable DT = classSQL.select(sql, "doc").Tables[0];
            dgvDocuments.DataSource = DT;
            ColorRowsInDgvDocuments();

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Format = "N2";
            dgvDocuments.Columns["Uplačeno"].DefaultCellStyle = style;
            dgvDocuments.Columns["Uplačeno"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDocuments.Columns["Uplačeno"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvDocuments.Columns["Iznos fakture"].DefaultCellStyle = style;
            dgvDocuments.Columns["Iznos fakture"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDocuments.Columns["Iznos fakture"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void ColorRowsInDgvPartners()
        {
            foreach (DataGridViewRow r in dgvPartners.Rows)
            {
                decimal potrazivanje, uplaceno;
                decimal.TryParse(r.Cells["Potraživanja"].FormattedValue.ToString(), out potrazivanje);
                decimal.TryParse(r.Cells["Plaćeno"].FormattedValue.ToString(), out uplaceno);

                if (uplaceno == 0)
                {
                    r.DefaultCellStyle.BackColor = Color.MistyRose;
                }
                else if (uplaceno == potrazivanje)
                {
                    r.DefaultCellStyle.BackColor = Color.PaleGreen;
                }
                else
                {
                    r.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void ColorRowsInDgvDocuments()
        {
            decimal _ukupno = 0;

            foreach (DataGridViewRow r in dgvDocuments.Rows)
            {
                decimal potrazivanje, uplaceno;
                decimal.TryParse(r.Cells["Iznos fakture"].FormattedValue.ToString(), out potrazivanje);
                decimal.TryParse(r.Cells["Uplačeno"].FormattedValue.ToString(), out uplaceno);

                if (uplaceno == 0)
                {
                    r.DefaultCellStyle.BackColor = Color.MistyRose;
                }
                else if (uplaceno == potrazivanje)
                {
                    r.DefaultCellStyle.BackColor = Color.PaleGreen;
                }
                else
                {
                    r.DefaultCellStyle.BackColor = Color.White;
                }

                _ukupno += (potrazivanje - uplaceno);
            }

            label2.Text = "Ukupno potraživanje prema odabranom partneru iznosi: " + _ukupno.ToString("#0.00");
        }

        private void dgvPartners_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SetDocumentsFromPartners(dgvPartners.CurrentRow.Cells["ID"].FormattedValue.ToString());
            if (dgvDocuments.CurrentCell != null)
                dgvDocuments.CurrentCell.Selected = false;
        }

        private void txtTrazeniPartner_TextChanged(object sender, EventArgs e)
        {
            SetPartners(txtTrazeniPartner.Text);
        }
    }
}