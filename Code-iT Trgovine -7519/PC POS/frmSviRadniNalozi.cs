using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmSviRadniNalozi : Form
    {
        public frmRadniNalog MainForm { get; set; }
        public string sifra_rn;

        private DataSet DS_Zaposlenik = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DSrn = new DataSet();

        public frmSviRadniNalozi()
        {
            InitializeComponent();
        }

        public frmMenu MainFormMenu { get; set; }

        private void frmSviRadniNalozi_Load(object sender, EventArgs e)
        {
            fillCB();
            fillDataGrid();
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w + 7, h + 7);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w - 7, h - 7);
        }

        private void fillDataGrid()
        {
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

            string sql = "SELECT " + top + " radni_nalog.broj_naloga AS Broj,radni_nalog.datum_naloga AS [Datum naloga],partners.ime_tvrtke AS [Naručioc],zaposlenici.ime + ' ' + zaposlenici.prezime AS Izradio " +
            " FROM radni_nalog " +
            " LEFT JOIN partners ON radni_nalog.id_narucioc = partners.id_partner " +
            " INNER JOIN zaposlenici ON radni_nalog.id_izradio = zaposlenici.id_zaposlenik ORDER BY CAST(radni_nalog.broj_naloga AS integer) DESC" +
            "" + remote + "";

            DSrn = classSQL.select(sql, "radni_nalog");
            dgv.DataSource = DSrn.Tables[0];
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 500);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void fillCB()
        {
            //fill vrsta dokumenta
            DSvd = classSQL.select("SELECT * FROM fakture_vd WHERE grupa='rna' ORDER BY id_vd", "fakture_vd");
            cbVD.DataSource = DSvd.Tables[0];
            cbVD.DisplayMember = "vd";
            cbVD.ValueMember = "id_vd";

            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzradio.DataSource = DS_Zaposlenik.Tables[0];
            cbIzradio.DisplayMember = "IME";
            cbIzradio.ValueMember = "id_zaposlenik";
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

                if (this.MdiParent != null)
                {
                    var mdiForm = this.MdiParent;
                    mdiForm.IsMdiContainer = true;
                    var childForm = new frmRadniNalog();
                    childForm.broj_RN_edit = broj;
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                }
                else
                {
                    MainForm.broj_RN_edit = broj;
                    MainForm.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Niste odabrali niti jednu stavku.", "Greška");
            }
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DateTime dOtp = Convert.ToDateTime(dtpOD.Value);

            DateTime dNow = Convert.ToDateTime(dtpDO.Value);

            string Broj = "";
            string Partner = "";
            string DateStart = "";
            string DateEnd = "";
            string VD = "";
            string Izradio = "";
            string SifraArtikla = "";

            if (chbBroj.Checked)
            {
                Broj = "radni_nalog.broj_naloga='" + txtBroj.Text + "' AND ";
            }
            if (chbSifra.Checked)
            {
                Partner = "radni_nalog.id_narucioc='" + txtPartner.Text + "' AND ";
            }
            if (chbVD.Checked)
            {
                VD = "radni_nalog.vrasta_dokumenta='" + cbVD.SelectedValue.ToString() + "' AND ";
            }
            if (chbOD.Checked)
            {
                DateStart = "radni_nalog.datum_naloga >='" + dOtp + "' AND ";
            }
            if (chbDO.Checked)
            {
                DateEnd = "radni_nalog.datum_naloga <='" + dNow + "' AND ";
            }

            if (chbArtikl.Checked)
            {
                SifraArtikla = "radni_nalog_stavke.sifra_robe ='" + cbArtikl.Text + "' AND ";
            }
            if (chbIzradio.Checked)
            {
                Izradio = "radni_nalog.id_izradio='" + cbIzradio.SelectedValue + "' AND ";
            }

            string filter = Broj + Partner + VD + DateStart + DateEnd + SifraArtikla + Izradio;

            if (filter.Length != 0)
            {
                filter = " WHERE " + filter;
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

            string sql = "SELECT DISTINCT " + top + " radni_nalog.broj_naloga AS Broj,radni_nalog.datum_naloga AS [Datum naloga],partners.ime_tvrtke AS [Naručioc],zaposlenici.ime + ' ' + zaposlenici.prezime AS Izradio " +
            " FROM radni_nalog " +
            " LEFT JOIN partners ON radni_nalog.id_narucioc = partners.id_partner " +
            " LEFT JOIN radni_nalog_stavke ON radni_nalog.broj_naloga = radni_nalog_stavke.broj_naloga " +
            " INNER JOIN zaposlenici ON radni_nalog.id_izradio = zaposlenici.id_zaposlenik " + filter + " ORDER BY radni_nalog.datum_naloga DESC" +
            "" + remote + "";

            DSrn = classSQL.select(sql, "radni_nalog");
            dgv.DataSource = DSrn.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            printaj();
        }

        private void frmSviRadniNalozi_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void printaj()
        {
            try
            {
                Report.Radni_nalog.repRadniNalog rav = new Report.Radni_nalog.repRadniNalog();
                rav.dokumenat = "AVA";
                rav.ImeForme = "Radni nalozi";
                rav.broj_dokumenta = dgv.CurrentRow.Cells["Broj"].FormattedValue.ToString();
                rav.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Radni nalog se ne može isprintati!");
            }
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            printaj();
        }
    }
}