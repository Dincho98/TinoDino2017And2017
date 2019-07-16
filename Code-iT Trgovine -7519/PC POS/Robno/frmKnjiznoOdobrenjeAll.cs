using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmKnjiznoOdobrenjeAll : Form
    {
        private string sql = "";
        private int broj_odobrenje = 0;

        public frmKnjiznoOdobrenjeAll()
        {
            InitializeComponent();
        }

        private void frmKnjiznoOdobrenjeAll_Load(object sender, EventArgs e)
        {
            try
            {
                dtpDo.Value = DateTime.Now;
                dtpOd.Value = dtpDo.Value.AddMonths(-1);
                btnSrch_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmKnjiznoOdobrenjeAll_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                splitContainer1.SplitterDistance = splitContainer1.Height / 2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSrch_Click(object sender, EventArgs e)
        {
            try
            {
                sql = "Select distinct ko.broj_odobrenje as Broj, ko.datum as Datum, z.ime || z.prezime as Izradio, ko.porez_odobrenja as Porez from knjizno_odobrenje ko " +
                        "left join zaposlenici z on ko.id_izradio = z.id_zaposlenik order by ko.broj_odobrenje asc;";
                DataTable dt = classSQL.select(sql, "knjizno_odobrenje").Tables[0];
                dgvKnjiznoOdobrenje.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvKnjiznoOdobrenje_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Int32.TryParse(dgvKnjiznoOdobrenje.Rows[e.RowIndex].Cells["broj"].Value.ToString(), out broj_odobrenje))
                {
                    sql = "select ko.broj_odobrenje, ko.datum, ko.broj_fakture, ko.porez_odobrenja, f.ukupno::numeric as ukupno, f.date, v.ime_valute " +
"from knjizno_odobrenje ko " +
"left join fakture f on ko.broj_fakture = f.broj_fakture and ko.id_ducan_faktura = f.id_ducan and ko.id_kasa_faktura = f.id_kasa and EXTRACT(YEAR FROM (cast(ko.datum as date))) = EXTRACT(year from (cast(f.date as date))) " +
"left join valute v on f.id_valuta = v.id_valuta " +
"where broj_odobrenje = " + broj_odobrenje;

                    DataTable dt = classSQL.select(sql, "knjizno_odobrenje").Tables[0];
                    dgvKnjiznoOdobrenjeItems.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (broj_odobrenje != 0)
                {
                    Report.KnjiznoOdobrenje.frmKnjiznoOdobrenje f = new Report.KnjiznoOdobrenje.frmKnjiznoOdobrenje();
                    f.broj_odobrenje = broj_odobrenje;
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Nije odabrano knjižno odobrenje.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}