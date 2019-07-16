using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmPartnerKronologijaDodaj : Form
    {
        private int _id = 0;
        public int id { get { return _id; } set { _id = value; } }

        public frmPartnerKronologijaDodaj()
        {
            InitializeComponent();
        }

        private void frmPartnerKronologija_Load(object sender, EventArgs e)
        {
            try
            {
                if (id > 0)
                {
                    string sql = string.Format("select * from partner_kronologija where id = {0};", id);
                    DataSet dsKronologijaPartner = classSQL.select(sql, "partner_kronologija");
                    if (dsKronologijaPartner != null && dsKronologijaPartner.Tables.Count > 0 && dsKronologijaPartner.Tables[0] != null && dsKronologijaPartner.Tables[0].Rows.Count > 0)
                    {
                        txtNazivPartnera.Text = dsKronologijaPartner.Tables[0].Rows[0]["naziv"].ToString();
                        txtSifraPartnera.Text = dsKronologijaPartner.Tables[0].Rows[0]["sifra"].ToString();
                        rtbOpis.Text = dsKronologijaPartner.Tables[0].Rows[0]["opis"].ToString();
                        pictureBox1.Enabled = false;
                        txtNazivPartnera.Enabled = false;
                        txtSifraPartnera.Enabled = false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void PopuniPodatke()
        {
            /*string filter="";
            if (txtFilter.Text.Length > 0)
            {
                filter = " AND sifra ~* '" + txtFilter.Text + "' OR partner.naziv ~*'" + txtFilter.Text + "' OR partner.opis ~* '" + txtFilter.Text + "'";
            }

            DataTable DT = classSQL.select("SELECT FROM partner_kronologija WHERE opis<>'' " + filter + "", "partner_kronologija").Tables[0];*/
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            if (rtbOpis.Text == "") { MessageBox.Show("Krivo upisani opis."); return; }
            else if (txtSifraPartnera.Text == "") { MessageBox.Show("Krivo upisana šifra partnera."); return; }
            else if (txtNazivPartnera.Text == "") { MessageBox.Show("Krivo upisan naziv partnera."); return; }

            if (id > 0)
            {
                string sql = string.Format(@"update partner_kronologija set
opis = '" + rtbOpis.Text.Replace("'", "").Replace("\"", "") + @"',
id_zaposlenik = '" + Properties.Settings.Default.id_zaposlenik + @"',
datum = '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + @"'
where id = '" + id + "';");
                classSQL.update(sql);
                //classSQL.insert("INSERT INTO partner_kronologija (opis,sifra,naziv,id_zaposlenik,datum) VALUES (" +
                //    "'" + rtbOpis.Text.Replace("'", "").Replace("\"", "") + "'," +
                //    "'" + txtSifraPartnera.Text + "'," +
                //    "'" + txtNazivPartnera.Text + "'," +
                //    "'" + Properties.Settings.Default.id_zaposlenik + "'," +
                //    "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                //    ")");
            }
            else
            {
                classSQL.insert("INSERT INTO partner_kronologija (opis,sifra,naziv,id_zaposlenik,datum) VALUES (" +
                    "'" + rtbOpis.Text.Replace("'", "").Replace("\"", "") + "'," +
                    "'" + txtSifraPartnera.Text + "'," +
                    "'" + txtNazivPartnera.Text + "'," +
                    "'" + Properties.Settings.Default.id_zaposlenik + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                    ")");
            }
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtSifraPartnera.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtNazivPartnera.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void txtSifraPartnera_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtSifraPartnera.Text == "")
                {
                    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                    partnerTrazi.ShowDialog();
                    if (Properties.Settings.Default.id_partner != "")
                    {
                        DataSet partner = new DataSet();
                        partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                        if (partner.Tables[0].Rows.Count > 0)
                        {
                            txtSifraPartnera.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                            txtNazivPartnera.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                            txtSifraPartnera.Select();
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtSifraPartnera.Select();
                        }
                    }
                    else
                    {
                        txtSifraPartnera.Select();
                        return;
                    }
                }

                string Str = txtSifraPartnera.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtSifraPartnera.Text = "0";
                }

                DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraPartnera.Text + "'", "partners").Tables[0];
                if (DSpar.Rows.Count > 0)
                {
                    txtNazivPartnera.Text = DSpar.Rows[0][0].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }
    }
}