using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class promet_po_fakt_blagajna : Form
    {
        public promet_po_fakt_blagajna()
        {
            InitializeComponent();
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            Report.Liste2.frmListe aa = new Report.Liste2.frmListe();
            aa.datumOD = dtpOD.Value;
            aa.datumDO = dtpDO.Value;
            aa.dokumenat = "FAK";

            if (chbBlagajnik.Checked)
            {
                aa.blagajnik = cbZaposlenik.SelectedValue.ToString();
            }
            if (chbSkladiste.Checked)
            {
                aa.skladiste = cbSkladiste.SelectedValue.ToString();
            }
            aa.ShowDialog();
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w + 7, h + 7);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w - 7, h - 7);
        }

        private DataTable DT_Zaposlenik;
        private DataTable DT_Skladiste;
        private DataTable DT_Ducan;
        private DataTable DT_Zaposlenik2 = new DataTable();
        private DataTable DT_Skladiste2 = new DataTable();
        private DataTable DT_Ducan2 = new DataTable();

        private void SetCB()
        {
            //fill komercijalist
            DT_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici").Tables[0];
            cbZaposlenik.DataSource = DT_Zaposlenik;
            cbZaposlenik.DisplayMember = "IME";
            cbZaposlenik.ValueMember = "id_zaposlenik";

            DT_Skladiste2.Columns.Add("id_skladiste", typeof(string));
            DT_Skladiste2.Columns.Add("skladiste", typeof(string));

            DT_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste").Tables[0];

            for (int i = 0; i < DT_Skladiste.Rows.Count; i++)
            {
                DT_Skladiste2.Rows.Add(DT_Skladiste.Rows[i]["id_skladiste"].ToString(), DT_Skladiste.Rows[i]["skladiste"].ToString());
            }

            cbSkladiste.DataSource = DT_Skladiste2;
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";
        }
    }
}