using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.sinkronizacija_poslovnica
{
    public partial class frmPostavke : Form
    {
        public frmPostavke()
        {
            InitializeComponent();
        }

        private void frmPostavke_Load(object sender, EventArgs e)
        {
            //string test= Util.kriptiranje.Encrypt("user");
            PopuniPodatke();
        }

        private void PopuniPodatke()
        {
            string sql = "SELECT * FROM postavke_sinkronizacije";
            DataTable DT = classSQL.select(sql, "postavke_sinkronizacije").Tables[0];
            DataTable DTskl = classSQL.select("SELECT * FROM skladiste", "skladiste").Tables[0];

            foreach (DataRow k in DTskl.Rows)
            {
                clbKalkulacija.Items.Add(k["id_skladiste"].ToString() + "/" + k["skladiste"].ToString(), CheckState.Unchecked);
            }

            foreach (DataRow p in DTskl.Rows)
            {
                clbPrimke.Items.Add(p["id_skladiste"].ToString() + "/" + p["skladiste"].ToString(), CheckState.Unchecked);
            }

            if (DT.Rows.Count > 0)
            {
                txtIpAdresa.Text = DT.Rows[0]["ip"].ToString();
                txtKorIme.Text = DT.Rows[0]["korisnickoime"].ToString();
                txtLozinka.Text = DT.Rows[0]["lozinka"].ToString();
                txtSifraPartneraStart.Text = DT.Rows[0]["sifra_partnera_start"].ToString();

                if (DT.Rows[0]["aktivirano"].ToString() == "1")
                {
                    vbAktiviraj.Checked = true;
                }
                else
                {
                    vbAktiviraj.Checked = false;
                }

                string skl_kalk = DT.Rows[0]["skladiste_kalkulacije"].ToString();
                string skl_primke = DT.Rows[0]["skladiste_primke"].ToString();

                string[] kalk = skl_kalk.Split(';');
                string[] primke = skl_primke.Split(';');

                for (int i = 0; i < clbKalkulacija.Items.Count; i++)
                {
                    foreach (string k in kalk)
                    {
                        if (k == clbKalkulacija.Items[i].ToString().Split('/')[0])
                        {
                            clbKalkulacija.SetItemChecked(i, true);
                        }
                    }
                }

                for (int i = 0; i < clbPrimke.Items.Count; i++)
                {
                    foreach (string p in primke)
                    {
                        if (p == clbPrimke.Items[i].ToString().Split('/')[0])
                        {
                            clbPrimke.SetItemChecked(i, true);
                        }
                    }
                }
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM postavke_sinkronizacije";
            DataTable DT = classSQL.select(sql, "postavke").Tables[0];

            string skl_kalk = "";
            string skl_primke = "";

            foreach (Object item in clbKalkulacija.CheckedItems)
            {
                skl_kalk += item.ToString().Split('/')[0] + ";";
            }
            if (skl_kalk.Length > 0)
            {
                skl_kalk = skl_kalk.Remove(skl_kalk.Length - 1);
            }

            foreach (Object item in clbPrimke.CheckedItems)
            {
                skl_primke += item.ToString().Split('/')[0] + ";";
            }
            if (skl_primke.Length > 0)
            {
                skl_primke = skl_primke.Remove(skl_primke.Length - 1);
            }

            if (DT.Rows.Count == 0)
            {
                sql = "INSERT INTO postavke_sinkronizacije (sifra_partnera_start,skladiste_kalkulacije,skladiste_primke,ip,korisnickoime,lozinka) VALUES (" +
                    "'" + txtSifraPartneraStart.Text + "'," +
                    "'" + skl_kalk + "'," +
                    "'" + skl_primke + "'," +
                    "'" + txtIpAdresa.Text + "'," +
                    "'" + txtKorIme.Text + "'," +
                    "'" + txtLozinka.Text + "'" +
                    ")";
                classSQL.insert(sql);
            }
            else
            {
                sql = "UPDATE postavke_sinkronizacije SET " +
                "sifra_partnera_start='" + txtSifraPartneraStart.Text + "'," +
                "skladiste_kalkulacije='" + skl_kalk + "'," +
                "skladiste_primke='" + skl_primke + "'," +
                "ip='" + txtIpAdresa.Text + "'," +
                "korisnickoime='" + txtKorIme.Text + "'," +
                "lozinka='" + txtLozinka.Text + "'" +
                "";
                classSQL.update(sql);
            }
        }

        private void vbAktiviraj_CheckedChanged(object sender, EventArgs e)
        {
            string a = "0";

            if (vbAktiviraj.Checked)
                a = "1";

            string sql = "UPDATE postavke_sinkronizacije SET aktivirano='" + a + "'";
            classSQL.update(sql);
        }
    }
}