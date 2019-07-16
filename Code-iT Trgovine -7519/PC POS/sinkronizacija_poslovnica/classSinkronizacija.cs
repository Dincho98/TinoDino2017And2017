using System.Data;
using System.Windows.Forms;

namespace PCPOS.sinkronizacija_poslovnica
{
    internal class classSinkronizacija
    {
        public static void PokreniSinkronizacijiu()
        {
            DataTable DT = new DataTable();
            DT = classSQL.select("SELECT * FROM postavke_sinkronizacije", "postavke_sinkronizacije").Tables[0];

            if (DT.Rows.Count > 0)
            {
                if (DT.Rows[0]["aktivirano"].ToString() == "1")
                {
                    if (MessageBox.Show("Želite li napraviti sinkronizaciju sa primarnom udaljenom bazom?", "Sinkronizacija", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        frmSinkronizacijaStart s = new frmSinkronizacijaStart();
                        s.DTpostavke = DT;
                        s.ShowDialog();
                    }
                }
            }
        }
    }
}