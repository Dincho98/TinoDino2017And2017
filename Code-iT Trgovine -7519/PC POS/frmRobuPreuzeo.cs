using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmRobuPreuzeo : Form
    {
        public frmRobuPreuzeo()
        {
            InitializeComponent();
        }

        private void frmRobuPreuzeo_Load(object sender, EventArgs e)
        {
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            int sifra;
            string filter = "";

            if (txtSifra.Text != "")
            {
                int.TryParse(txtSifra.Text, out sifra);
                filter = " AND id_fakturirati='" + sifra.ToString() + "' ";
            }

            if (txtPreuzeo.Text != "")
            {
                int.TryParse(txtPreuzeo.Text, out sifra);
                filter += " AND id_odrediste='" + sifra.ToString() + "' ";
            }

            DataTable DTp = classSQL.select("SELECT p.ime_tvrtke,p.ime,p.prezime,fakture.* FROM fakture " +
                " LEFT JOIN partners p ON p.id_partner=fakture.id_fakturirati " +
                " WHERE fakture.date>='" + tdOdDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") +
                "' AND fakture.date<='" + tdDoDatuma.Value.ToString("yyyy-MM-dd H:mm:ss") + "' " + filter + " ORDER BY CAST(fakture.broj_fakture AS INT) ASC", "fakture").Tables[0];

            string klijent = "";
            decimal vpc, pdv, mpc;
            dgv.Rows.Clear();

            foreach (DataRow r in DTp.Rows)
            {
                if (r["ime_tvrtke"].ToString() == "") klijent = r["ime"].ToString() + "" + r["prezime"].ToString();
                else klijent = r["ime_tvrtke"].ToString();

                decimal.TryParse(r["ukupno_vpc"].ToString(), out vpc);
                decimal.TryParse(r["ukupno_porez"].ToString(), out pdv);
                decimal.TryParse(r["ukupno"].ToString(), out mpc);

                dgv.Rows.Add(klijent, "Faktura br. " + r["broj_fakture"].ToString(), vpc.ToString("N2"), pdv.ToString("N2"), mpc.ToString("N2"));
            }
        }

        private DataSet DSpartner;

        private void btnPartner_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DSpartner = new DataSet();
                DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
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
        }

        private void txtSifra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtSifra.Text == "")
                {
                    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                    partnerTrazi.ShowDialog();
                    if (Properties.Settings.Default.id_partner != "")
                    {
                        DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                        if (DSpartner.Tables[0].Rows.Count > 0)
                        {
                            txtSifra.Text = DSpartner.Tables[0].Rows[0]["id_partner"].ToString();
                            txtNaziv.Text = DSpartner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtSifra.Select();
                        }
                    }
                    else
                    {
                        txtSifra.Select();
                        return;
                    }
                }

                string Str = txtSifra.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtSifra.Text = "0";
                }
                DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner='" + txtSifra.Text + "'", "partners");
                if (DSpartner.Tables[0].Rows.Count > 0)
                {
                    txtNaziv.Text = DSpartner.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            Report.PreuzeoRobu.frmPreuzeoRobu p = new Report.PreuzeoRobu.frmPreuzeoRobu();
            p.sort = "O";
            p.sifra_preuzeo = txtPreuzeo.Text;
            p.datumOD = tdOdDatuma.Value;
            p.datumDO = tdDoDatuma.Value;
            p.ime_partnera = txtNaziv.Text;
            p.sifra_partnera = txtSifra.Text;
            p.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Report.PreuzeoRobu.frmPreuzeoRobu p = new Report.PreuzeoRobu.frmPreuzeoRobu();
            p.sort = "F";
            p.sifra_preuzeo = txtPreuzeo.Text;
            p.datumOD = tdOdDatuma.Value;
            p.datumDO = tdDoDatuma.Value;
            p.ime_partnera = txtNaziv.Text;
            p.sifra_partnera = txtSifra.Text;
            p.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DSpartner = new DataSet();
                DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (DSpartner.Tables[0].Rows.Count > 0)
                {
                    txtPreuzeo.Text = DSpartner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtNazivPreuzeo.Text = DSpartner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void txtPreuzeo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtSifra.Text == "")
                {
                    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                    partnerTrazi.ShowDialog();
                    if (Properties.Settings.Default.id_partner != "")
                    {
                        DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                        if (DSpartner.Tables[0].Rows.Count > 0)
                        {
                            txtPreuzeo.Text = DSpartner.Tables[0].Rows[0]["id_partner"].ToString();
                            txtNazivPreuzeo.Text = DSpartner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtSifra.Select();
                        }
                    }
                    else
                    {
                        txtSifra.Select();
                        return;
                    }
                }

                string Str = txtSifra.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtSifra.Text = "0";
                }
                DSpartner = classSQL.select("SELECT * FROM partners WHERE id_partner='" + txtSifra.Text + "'", "partners");
                if (DSpartner.Tables[0].Rows.Count > 0)
                {
                    txtNaziv.Text = DSpartner.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }
    }
}