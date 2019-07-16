using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmStanjeUMinusu : Form
    {
        private DataTable DTgrid = new DataTable();

        public frmStanjeUMinusu()
        {
            InitializeComponent();
        }

        public new DialogResult ShowDialog()
        {
            DTgrid = classSQL.select("SELECT roba_prodaja.sifra AS [Šifra artikla],roba.naziv as [Naziv artikla],CAST(REPLACE(roba_prodaja.kolicina,',','.') AS numeric) as [Stanje],skladiste.skladiste as [Skladište],skladiste.id_skladiste FROM roba_prodaja LEFT JOIN roba ON roba.sifra=roba_prodaja.sifra LEFT JOIN skladiste ON roba_prodaja.id_skladiste=skladiste.id_skladiste WHERE CAST(REPLACE(roba_prodaja.kolicina,',','.') AS numeric)<0 AND roba.oduzmi='DA'", "negativno_stanje").Tables[0];

            if (DTgrid.Rows.Count == 0) return MessageBox.Show("Nemate artikala sa negativnim stanjem na skladištu");
            else return base.ShowDialog();
        }

        public DialogResult ShowDialog(DataTable DT)
        {
            DTgrid = DT;

            if (DTgrid.Rows.Count == 0) return MessageBox.Show("Nemate artikala sa negativnim stanjem na skladištu");
            else return base.ShowDialog();
        }

        private void frmStanjeUMinusu_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DTgrid;
            dataGridView1.Columns["id_skladiste"].Visible = false;
            dataGridView1.Columns["Naziv artikla"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.ReadOnly = true;

            DataTable DT_Skladiste = classSQL.select("SELECT id_skladiste,skladiste FROM skladiste", "skladiste").Tables[0];

            DataRow r = DT_Skladiste.NewRow();
            r["id_skladiste"] = -1;
            r["skladiste"] = "Sva skladišta";

            DT_Skladiste.Rows.InsertAt(r, 0);

            cbSkladiste.DataSource = DT_Skladiste;
            cbSkladiste.DisplayMember = "skladiste";
            cbSkladiste.ValueMember = "id_skladiste";

            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 500);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void cbSkladiste_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbSkladiste.SelectedValue) != -1) DTgrid.DefaultView.RowFilter = "id_skladiste=" + cbSkladiste.SelectedValue.ToString();
            else DTgrid.DefaultView.RowFilter = "";
            dataGridView1.Select();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Report.StanjeUMinusu.frmStanjeUminusu frmReport = new Report.StanjeUMinusu.frmStanjeUminusu();
                frmReport.id_skladiste = Convert.ToInt32(cbSkladiste.SelectedValue);
                //frmReport.dtStanjeUMinus = DTgrid;
                frmReport.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}