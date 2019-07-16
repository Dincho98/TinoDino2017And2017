using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmSvePromjeneCijena : Form
    {
        public frmZapisnikopromjeniCijene MainForm { get; set; }
        public frmMenu MainFormMenu { get; set; }

        public frmSvePromjeneCijena()
        {
            InitializeComponent();
        }

        private void frmSvePromjeneCijena_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            if (MainFormMenu == null)
            {
                this.Height = heigt - 60;
            }
            else
            {
                this.Height = heigt - 150;
            }
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);

            fillDataGrid();

            try
            {
                if (dgv.Rows.Count > 1)
                {
                    string br = dgv.CurrentRow.Cells["broj"].FormattedValue.ToString();

                    fillDataGrid_stavke(br);
                }
            }
            catch { }
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 500);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void PaintRows(DataGridView dg)
        {
            int br = 0;
            for (int i = 0; i < dg.Rows.Count; i++)
            {
                if (br == 0)
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
                    br++;
                }
                else
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    br = 0;
                }
            }
            DataGridViewRow row = dg.RowTemplate;
            row.Height = 22;
        }

        private void SetDecimalInDgv(DataGridView dg, string column, string column1, string column2)
        {
            for (int i = 0; i < dg.RowCount; i++)
            {
                try
                {
                    if (column != "NE")
                    {
                        dg.Rows[i].Cells[column].Value = Convert.ToDouble(dg.Rows[i].Cells[column].FormattedValue.ToString()).ToString("#0.00");
                    }

                    if (column1 != "NE")
                    {
                        dg.Rows[i].Cells[column1].Value = Convert.ToDouble(dg.Rows[i].Cells[column1].FormattedValue.ToString()).ToString("#0.00");
                    }

                    if (column2 != "NE")
                    {
                        dg.Rows[i].Cells[column2].Value = Convert.ToDouble(dg.Rows[i].Cells[column2].FormattedValue.ToString()).ToString("#0.00");
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private DataSet DSpromjene;

        private void fillDataGrid()
        {
            string top = "";
            string remote = "";
            if (classSQL.remoteConnectionString != "")
            {
                remote = "LIMIT 500";
            }
            else
            {
                top = "TOP(500)";
            }

            string sql = string.Format(@"SELECT {0}
promjena_cijene.broj AS [Broj], promjena_cijene.date AS [Datum],
skladiste.skladiste AS [Skladište], zaposlenici.ime + ' ' + zaposlenici.prezime AS [Izradio]
FROM promjena_cijene
LEFT JOIN zaposlenici ON promjena_cijene.id_izradio = zaposlenici.id_zaposlenik
LEFT JOIN skladiste ON promjena_cijene.id_skladiste = skladiste.id_skladiste
ORDER BY CAST(promjena_cijene.broj AS integer) DESC
{1};", top, remote);

            DSpromjene = classSQL.select(sql, "promjena_cijene");
            dgv.DataSource = DSpromjene.Tables[0];

            PaintRows(dgv);
        }

        private void fillDataGrid_stavke(string broj)
        {
            try
            {
                if (broj == null || broj == "")
                {
                    return;
                }

                dataGridView1.Visible = true;
                string sql = string.Format(@"SELECT promjena_cijene_stavke.broj AS [Broj], promjena_cijene_stavke.stara_cijena as [Stara Cijena],
promjena_cijene_stavke.nova_cijena as [Nova Cijena], promjena_cijene_stavke.pdv as [Porez], promjena_cijene_stavke.sifra as [Šifra]
FROM promjena_cijene_stavke
WHERE promjena_cijene_stavke.broj = '{0}'
ORDER BY promjena_cijene_stavke.broj DESC;", broj);

                DSpromjene = classSQL.select(sql, "promjena_cijene_stavke");
                dataGridView1.DataSource = DSpromjene.Tables[0];

                PaintRows(dgv);
            }
            catch { }
        }

        private void btnUrediSifru_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }

            if (dgv.RowCount > 0)
            {
                string broj = dgv.CurrentRow.Cells["Broj"].Value.ToString();

                bool onsiteopen = false;
                foreach (Form S in Application.OpenForms)
                {
                    if (S is frmZapisnikopromjeniCijene) { onsiteopen = true; }

                    if (onsiteopen == true) { MessageBox.Show("this form is already open."); }
                }

                frmZapisnikopromjeniCijene childForm = new frmZapisnikopromjeniCijene();
                childForm.broj_edit = broj;
                childForm.editbit = true;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Niste odabrali niti jednu stavku.", "Greška");
            }
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new Size(w + 7, h + 7);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new Size(w - 7, h - 7);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DateTime dOtp = Convert.ToDateTime(dtpOD.Value);
            string dtOd = dOtp.Month + "." + dOtp.Day + "." + dOtp.Year;

            DateTime dNow = Convert.ToDateTime(dtpDO.Value);
            string dtDo = dNow.Month + "." + dNow.Day + "." + dNow.Year;

            string Broj = "";
            string Partner = "";
            string DateStart = "";
            string DateEnd = "";
            string VD = "";
            string Valuta = "";
            string Izradio = "";
            string SifraArtikla = "";

            if (chbBroj.Checked)
            {
                Broj = string.Format("promjena_cijene.broj = '{0}' AND ", txtBroj.Text);
            }
            if (chbOD.Checked)
            {
                DateStart = string.Format("promjena_cijene.date >= '{0}' AND ", dtpOD.Value.ToString("yyyy-MM-dd H:mm:ss"));
            }
            if (chbDO.Checked)
            {
                DateEnd = string.Format("promjena_cijene.date <= '{0}' AND ", dtpDO.Value.ToString("yyyy-MM-dd H:mm:ss"));
            }
            if (chbArtikl.Checked)
            {
                SifraArtikla = string.Format("promjena_cijene_stavke.sifra = '{0}' AND ", cbArtikl.Text);
            }
            if (chbIzradio.Checked)
            {
                Izradio = string.Format("promjena_cijene.id_izradio = '{0}' AND ", cbIzradio.SelectedValue);
            }

            string filter = Broj + Partner + VD + DateStart + DateEnd + Valuta + SifraArtikla + Izradio;

            if (filter.Length != 0)
            {
                filter = string.Format(" WHERE {0}", filter);
                filter = filter.Remove(filter.Length - 4, 4);
            }

            string top = "";
            string remote = "";
            if (classSQL.remoteConnectionString != "")
            {
                remote = " LIMIT 500";
            }
            else
            {
                top = " TOP(500) ";
            }

            string sql = string.Format(@"SELECT DISTINCT {0} promjena_cijene.broj AS [Broj],promjena_cijene.date AS [Datum],
skladiste.skladiste AS [Skladište], zaposlenici.ime + ' ' + zaposlenici.prezime AS [Izradio]
FROM promjena_cijene
LEFT JOIN zaposlenici ON promjena_cijene.id_izradio = zaposlenici.id_zaposlenik
LEFT JOIN promjena_cijene_stavke ON promjena_cijene.broj = promjena_cijene_stavke.broj
LEFT JOIN skladiste ON promjena_cijene.id_skladiste = skladiste.id_skladiste
{1}
ORDER BY promjena_cijene.date DESC
{2}", top, filter, remote);

            DSpromjene = classSQL.select(sql, "fakture");
            dgv.DataSource = DSpromjene.Tables[0];
            PaintRows(dgv);
        }

        private DataSet DS_Zaposlenik;

        private void fillCB()
        {
            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime + ' ' + prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzradio.DataSource = DS_Zaposlenik.Tables[0];
            cbIzradio.DisplayMember = "IME";
            cbIzradio.ValueMember = "id_zaposlenik";
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Report.Liste.frmListe rfak = new Report.Liste.frmListe();
            rfak.dokument = "promjena_cijene";
            rfak.ImeForme = "Zapisnik o promjeni cijene";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            Report.Liste.frmListe rfak = new Report.Liste.frmListe();
            rfak.dokument = "promjena_cijene";
            rfak.ImeForme = "Zapisnik o promjeni cijene";
            rfak.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
            rfak.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv.Rows.Count > 1)
                {
                    string br = dgv.CurrentRow.Cells["broj"].FormattedValue.ToString();

                    fillDataGrid_stavke(br);
                }
            }
            catch { }
        }
    }
}