using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Proizvodnja
{
    public partial class frmKarticaRobeNaTudemSkladistu : Form
    {
        public frmKarticaRobeNaTudemSkladistu()
        {
            InitializeComponent();
        }

        private void frmKarticaRobeNaTudemSkladistu_Load(object sender, EventArgs e)
        {
            try
            {
                lblPartnerNaziv.Text = "";
                int trenutnaGodina = Util.Korisno.GodinaKojaSeKoristiUbazi;
                dtpDatumOd.MinDate = new DateTime(trenutnaGodina, 1, 1);
                dtpDatumOd.MaxDate = new DateTime(trenutnaGodina, 12, 31);
                dtpDatumDo.MinDate = dtpDatumOd.MinDate;
                dtpDatumDo.MaxDate = dtpDatumOd.MaxDate;

                getPoslovnice(Class.Postavke.id_default_ducan);
                getSkladiste(0);
                setDates();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Metoda uzima sve poslovnice iz baze koje su aktivne
        /// </summary>
        /// <param name="select">Defaultna vrijednost koja se prikazuje u comboboxu</param>
        private void getPoslovnice(int select = 0)
        {
            try
            {
                string sql = string.Format(@"select x.id, x.poslovnica
from
(
    select 0 as id, 'Sve' as poslovnica, 1 as srt
    union all
    select id_ducan as id, ime_ducana as poslovnica, 2 as srt
    from
    ducan
    where aktivnost = 'DA'
) x
order by x.srt, x.poslovnica asc;");

                DataSet dsPoslovnice = classSQL.select(sql, "poslovnice");

                if (dsPoslovnice != null && dsPoslovnice.Tables.Count > 0 && dsPoslovnice.Tables[0] != null && dsPoslovnice.Tables[0].Rows.Count > 0)
                {
                    cmbPoslovnice.DisplayMember = "poslovnica";
                    cmbPoslovnice.ValueMember = "id";
                    cmbPoslovnice.DataSource = dsPoslovnice.Tables[0];

                    cmbPoslovnice.SelectedValue = select;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void getSkladiste(int select = 0)
        {
            try
            {
                string sql = string.Format(@"select x.id, x.skladiste
from
(
    select 0 as id, 'Sve' as skladiste, 1 as srt
    union all
    select id_skladiste as id, skladiste as skladiste, 2 as srt
    from
    skladiste
    where aktivnost = 'DA'
) x
order by x.srt, x.skladiste asc;");

                DataSet dsSkladiste = classSQL.select(sql, "poslovnice");

                if (dsSkladiste != null && dsSkladiste.Tables.Count > 0 && dsSkladiste.Tables[0] != null && dsSkladiste.Tables[0].Rows.Count > 0)
                {
                    cmbSkladiste.DisplayMember = "skladiste";
                    cmbSkladiste.ValueMember = "id";
                    cmbSkladiste.DataSource = dsSkladiste.Tables[0];

                    cmbSkladiste.SelectedValue = select;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Metoda uzima id i naziv partnera
        /// </summary>
        /// <param name="id">Id partnera kojeg uzima</param>
        /// <param name="deleteFields">Briše vrijednosti u texboxima vezane za partnere</param>
        private void getPartner(int id = 0, bool deleteFields = false)
        {
            try
            {
                if (deleteFields)
                {
                    txtPartnerId.Text = "";
                    lblPartnerNaziv.Text = txtPartnerId.Text;
                    return;
                }

                string sql = string.Format(@"select id_partner as id,
case when vrsta_korisnika = 1 then ime_tvrtke else concat(ime, ' ', prezime) end as naziv
from partners where id_partner = {0}", id);
                DataSet dsPartners = classSQL.select(sql, "partners");
                if (dsPartners != null && dsPartners.Tables.Count > 0 && dsPartners.Tables[0] != null && dsPartners.Tables[0].Rows.Count > 0)
                {
                    txtPartnerId.Text = dsPartners.Tables[0].Rows[0]["id"].ToString();
                    lblPartnerNaziv.Text = dsPartners.Tables[0].Rows[0]["naziv"].ToString();
                    return;
                }
                MessageBox.Show("Partner ne postoji.");
                lblPartnerNaziv.Text = "";
                txtPartnerId.SelectAll();
                return;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Postavlja defultne vrijednosti u datetimepickere
        /// </summary>
        private void setDates()
        {
            try
            {
                DateTime danas = DateTime.Now;
                dtpDatumDo.Value = danas;
                dtpDatumOd.Value = danas.AddDays((DateTime.DaysInMonth(danas.Year, danas.Month) * (-1)));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnPartnerSrch_Click(object sender, EventArgs e)
        {
            try
            {
                frmPartnerTrazi f = new frmPartnerTrazi();
                f.ShowDialog();
                int id = 0;
                if (int.TryParse(Properties.Settings.Default.id_partner, out id))
                {
                    getPartner(id);
                }
                Properties.Settings.Default.id_partner = null;
                Properties.Settings.Default.Save();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void txtPartnerId_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int id = 0;
                    if (int.TryParse(txtPartnerId.Text, out id) && id > 0)
                    {
                        getPartner(id);
                        return;
                    }
                    MessageBox.Show("Pogrešan unos za ID partnera.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            try
            {
                getPartner(0, true);
                getPoslovnice(Class.Postavke.id_default_ducan);
                getSkladiste(0);
                setDates();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnPrikazi_Click(object sender, EventArgs e)
        {
            try
            {
                int partner_id = 0;
                if (!int.TryParse(txtPartnerId.Text.Trim(), out partner_id))
                {
                    MessageBox.Show("Niste odabrali partnera.");
                    return;
                }

                if (dtpDatumOd.Value > dtpDatumDo.Value)
                {
                    MessageBox.Show("Pogrešan odabir datuma.");
                    return;
                }

                string sql = string.Format(@"");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}