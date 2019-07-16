using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace PCPOS.Report.SaldaKonti
{
    public partial class frmUnosPocetnogDugovanjaPartnera : Form
    {
        private int ID = 0;

        public frmUnosPocetnogDugovanjaPartnera()
        {
            InitializeComponent();
        }

        private void btnSrchPartners_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                srchPartner(Convert.ToInt32(Properties.Settings.Default.id_partner));
                traziPocetnoDugovanje();
            }
        }

        private void srchPartner(int idPartner = 0)
        {
            DataSet DSpartner = new DataSet();
            try
            {
                DSpartner = classSQL.select(string.Format("SELECT * FROM partners WHERE id_partner ={0};", idPartner), "partners");
                if (DSpartner.Tables[0].Rows.Count > 0)
                {
                    txtSifra.Text = DSpartner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtNaziv.Text = DSpartner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DSpartner = null;
            }
        }

        private void txtSifra_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int idPartner = 0;
                    if (int.TryParse(txtSifra.Text, out idPartner))
                    {
                        srchPartner(idPartner);
                        traziPocetnoDugovanje();
                    }
                    else
                    {
                        MessageBox.Show("Pogrešna šifra za partnera.");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            try
            {
                spremipocetnoDugovanje(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Odustani()
        {
            ID = 0;
            txtSifra.Text = string.Empty;
            txtNaziv.Text = string.Empty;
            txtIsplaceno.Text = string.Empty;
            txtOtvoreno.Text = string.Empty;
            txtPotrazuje.Text = string.Empty;
            txtUplaceno.Text = string.Empty;
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            Odustani();
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void traziPocetnoDugovanje()
        {
            DataSet ds = null;
            try
            {
                int idPartner = 0, id = 0;
                decimal otvoreno = 0, uplaceno = 0, potrazujem = 0, isplaceno = 0;
                if (int.TryParse(txtSifra.Text, out idPartner))
                {
                    string sql = string.Format("select * from pocetno_dugovanje_partnera where id_partner = {0};", idPartner);
                    ds = classSQL.select(sql, "pocetno_dugovanje_partnera");
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        decimal.TryParse(ds.Tables[0].Rows[0]["otvoreno"].ToString(), out otvoreno);
                        decimal.TryParse(ds.Tables[0].Rows[0]["uplaceno"].ToString(), out uplaceno);
                        decimal.TryParse(ds.Tables[0].Rows[0]["potrazuje"].ToString(), out potrazujem);
                        decimal.TryParse(ds.Tables[0].Rows[0]["isplaceno"].ToString(), out isplaceno);
                        int.TryParse(ds.Tables[0].Rows[0]["id"].ToString(), out id);
                    }
                }

                txtOtvoreno.Text = otvoreno.ToString("0.00#,##");
                txtUplaceno.Text = uplaceno.ToString("0.00#,##");
                txtPotrazuje.Text = potrazujem.ToString("0.00#,##");
                txtIsplaceno.Text = isplaceno.ToString("0.00#,##");
                ID = id;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ds = null;
            }
        }

        private void spremipocetnoDugovanje(int id = 0)
        {
            try
            {
                int idPartner = 0;
                decimal otvoreno = 0, uplaceno = 0, potrazujem = 0, isplaceno = 0;
                if (int.TryParse(txtSifra.Text, out idPartner))
                {
                    if (!decimal.TryParse(txtOtvoreno.Text, out otvoreno)) { MessageBox.Show("Pogrešan unos za polje \"Otvoreno\""); return; }
                    if (!decimal.TryParse(txtUplaceno.Text, out uplaceno)) { MessageBox.Show("Pogrešan unos za polje \"Uplačeno\""); return; }
                    if (!decimal.TryParse(txtPotrazuje.Text, out potrazujem)) { MessageBox.Show("Pogrešan unos za polje \"Potražujem\""); return; }
                    if (!decimal.TryParse(txtIsplaceno.Text, out isplaceno)) { MessageBox.Show("Pogrešan unos za polje \"Isplačeno\""); return; }
                    string sql = "";
                    if (id == 0)
                    {
                        sql = string.Format(CultureInfo.InvariantCulture, @"INSERT INTO pocetno_dugovanje_partnera (id_partner, otvoreno, uplaceno, potrazuje, isplaceno, datum_dokumenta, datum_dvo)
VALUES
(
{0}, {1:0.00}, {2:0.00}, {3:0.00}, {4:0.00}, '{5}-01-01 00:00:00', '{5}-01-01 00:00:00'
);", idPartner, otvoreno, uplaceno, potrazujem, isplaceno, Util.Korisno.GodinaKojaSeKoristiUbazi);
                        classSQL.insert(sql);
                    }
                    else
                    {
                        sql = string.Format(CultureInfo.InvariantCulture, @"UPDATE pocetno_dugovanje_partnera
SET
    otvoreno = {0:0.00},
    uplaceno = {1:0.00},
    potrazuje = {2:0.00},
    isplaceno = {3:0.00}
WHERE id_partner = {4};", otvoreno, uplaceno, potrazujem, isplaceno, idPartner);

                        classSQL.update(sql);
                    }

                    MessageBox.Show("Spremljeno.");

                    Odustani();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}