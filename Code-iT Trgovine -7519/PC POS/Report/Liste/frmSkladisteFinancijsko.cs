using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.Liste
{
    public partial class frmSkladisteFinancijsko : Form
    {
        public frmSkladisteFinancijsko()
        {
            InitializeComponent();
        }

        private void frmSkladisteFinancijsko_Load(object sender, EventArgs e)
        {
            DataTable DT_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste").Tables[0];
            cbSkladiste.DataSource = DT_Skladiste;
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            Report.Liste.frmListe l = new Report.Liste.frmListe();
            l.Text = "Skladište financijsko";
            l.dokument = "SKLfinancije";
            if (chbSkladiste.Checked)
            {
                l.skladiste = cbSkladiste.SelectedValue.ToString();
            }
            else
            {
                l.skladiste = "";
            }

            l.ShowDialog();
        }
    }
}