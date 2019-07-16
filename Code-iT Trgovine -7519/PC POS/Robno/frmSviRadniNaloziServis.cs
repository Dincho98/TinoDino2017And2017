using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmSviRadniNaloziServis2016 : Form
    {
        public frmRadniNalogSerivs2016 MainForm { get; set; }
        public string sifra_rn;

        public frmSviRadniNaloziServis2016()
        {
            InitializeComponent();
        }

        private DataSet DS_Zaposlenik = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataSet DSValuta = new DataSet();

        public frmMenu MainFormMenu { get; set; }

        private void frmSvePonude_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);
            this.Paint += new PaintEventHandler(Form1_Paint);

            fillCB();
            fillDataGrid();

            DateTime dtNow = DateTime.Now;
            dtpDO.Value = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 23, 59, 59);
            dtpOD.Value = new DateTime(dtNow.Year, dtNow.Month, 1, 0, 0, 0);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 500);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void fillCB()
        {
            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici");
            cbIzradio.DataSource = DS_Zaposlenik.Tables[0];
            cbIzradio.DisplayMember = "IME";
            cbIzradio.ValueMember = "id_zaposlenik";
        }

        private DataSet DSfakture = new DataSet();

        private void fillDataGrid()
        {
            string sql = @"select s.servisna_primka as [Barkode], s.godina as [Godina], case when p.vrsta_korisnika = 1 then p.ime_tvrtke else concat(p.ime, ' ', p.prezime) end as [Partner],
case when s.sifra is not null and length(s.sifra) > 0 then concat(s.sifra, ' - ', s.naziv) else s.naziv end as [Artikl],
case when ss.datum is null then sss.datum else ss.datum end as [Datum], concat(z.ime, ' ', z.prezime) as [Izradio],
case when xss.status = 0 then 'Nulti servis' else
    case when xss.status = 1 then 'Zaprimljeno' else
        case when xss.status = 2 then 'Servis u toku' else
            case when xss.status = 3 then 'Na vanjskom servisu' else
                case when xss.status = 4 then 'Završen servis' else 'Povrat kupcu' end
            end
        end
    end
end as [Status]
from servis s
left join (select id_servis, max(datum) as datum from servis_status where status = 1 group by id_servis) ss on s.id = ss.id_servis
left join (select id_servis, max(datum) as datum from servis_status where status = 0 group by id_servis) sss on s.id = sss.id_servis
left join (select ssx.id_servis, ssx.datum, ssx.status
from servis_status ssx
right join (select id_servis, max(datum) as datum from servis_status group by id_servis) rss on rss.id_servis = ssx.id_servis and rss.datum = ssx.datum) xss on s.id = xss.id_servis
left join partners p on s.partner = p.id_partner
left join zaposlenici z on s.izradio = z.id_zaposlenik
order by case when ss.datum is null then sss.datum else ss.datum end desc
limit 1000;";

            DSfakture = classSQL.select(sql, "radni_nalog_servis");
            dgv.DataSource = DSfakture.Tables[0];
        }

        private void btnUrediSifru_Click(object sender, EventArgs e)
        {
            if (!Class.Registracija.dopustenoKreiranjeNovihDokumenta)
            {
                return;
            }
            if (dgv.RowCount > 0)
            {
                string broj = dgv.CurrentRow.Cells["Barkode"].Value.ToString();
                int godina = Convert.ToInt32(dgv.CurrentRow.Cells["Godina"].Value.ToString());

                if (this.MdiParent != null)
                {
                    var mdiForm = this.MdiParent;
                    mdiForm.IsMdiContainer = true;
                    var childForm = new frmRadniNalogSerivs2016();
                    childForm.servisBarkode = broj;
                    childForm.servisGodina = godina;
                    childForm.MdiParent = mdiForm;
                    childForm.Dock = DockStyle.Fill;
                    childForm.Show();
                }
                else
                {
                    MainForm.servisBarkode = broj;
                    MainForm.servisGodina = godina;
                    MainForm.Show();
                    this.Close();
                }
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
            PB.Size = new System.Drawing.Size(w + 7, h + 7);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w - 7, h - 7);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DateTime dOtp = Convert.ToDateTime(dtpOD.Value);

            DateTime dNow = Convert.ToDateTime(dtpDO.Value);

            string Broj = "";
            string Partner = "";
            string DateStart = "";
            string DateEnd = "";
            string Izradio = "";
            string SifraArtikla = "";
            string Napomena = "";

            if (chbBroj.Checked)
            {
                Broj = "s.servisna_primka='" + txtBroj.Text + "' AND ";
            }
            //if (chbSifra.Checked)
            //{
            //    Partner = "radni_nalog_servis.id_fakturirati='" + txtPartner.Text + "' AND ";
            //}
            if (txtPartner.Text.Trim() != "")
            {
                string Str = txtPartner.Text.Trim().ToLower();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (isNum)
                {
                    Partner = "s.partner='" + Str + "' AND ";
                }
                else
                {
                    DataTable DSpar = classSQL.select("SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) = '" +
                        Str + "'", "partners").Tables[0];
                    if (DSpar.Rows.Count > 0)
                    {
                        Partner = "s.partner='" + DSpar.Rows[0][0].ToString() + "' AND ";
                    }
                    else
                    {
                        DSpar = classSQL.select("SELECT id_partner FROM partners WHERE LOWER(ime_tvrtke) like '%" +
                            Str + "%'", "partners").Tables[0];
                        if (DSpar.Rows.Count > 0)
                        {
                            Partner = "s.partner='" + DSpar.Rows[0][0].ToString() + "' AND ";
                        }
                        else
                        {
                            Partner = "";
                        }
                    }
                }
            }

            if (chbOD.Checked)
            {
                DateStart = "ss.datum >='" + dOtp.ToString("yyyy-MM-dd HH:mm:ss") + "' AND ";
            }

            if (chbDO.Checked)
            {
                DateEnd = "ss.datum <='" + dNow.ToString("yyyy-MM-dd HH:mm:ss") + "' AND ";
            }

            if (chbArtikl.Checked)
            {
                SifraArtikla = "s.sifra ='" + cbArtikl.Text + "' AND ";
            }

            if (chbIzradio.Checked)
            {
                Izradio = "s.izradio='" + cbIzradio.SelectedValue + "' AND ";
            }

            if (chbnapomenacb.Checked)
            {
                Napomena = "s.naziv ~* '" + textnapomena.Text + "' AND ";
            }

            string filter = Broj + Partner + DateStart + DateEnd + SifraArtikla + Izradio + Napomena;

            if (filter.Length != 0)
            {
                filter = " WHERE " + filter;
                filter = filter.Remove(filter.Length - 4, 4);
            }

            string sql = @"select s.servisna_primka as [Barkode], s.godina as [Godina], case when p.vrsta_korisnika = 1 then p.ime_tvrtke else concat(p.ime, ' ', p.prezime) end as [Partner],
case when s.sifra is not null and length(s.sifra) > 0 then concat(s.sifra, ' - ', s.naziv) else s.naziv end as [Artikl],
case when ss.datum is null then sss.datum else ss.datum end as [Datum], concat(z.ime, ' ', z.prezime) as [Izradio],
case when xss.status = 0 then 'Nulti servis' else
    case when xss.status = 1 then 'Zaprimljeno' else
        case when xss.status = 2 then 'Servis u toku' else
            case when xss.status = 3 then 'Na vanjskom servisu' else
                case when xss.status = 4 then 'Završen servis' else 'Povrat kupcu' end
            end
        end
    end
end as [Status]
from servis s
left join (select id_servis, max(datum) as datum from servis_status where (status = 1 or status = 0) group by id_servis) ss on s.id = ss.id_servis
left join (select id_servis, max(datum) as datum from servis_status where status = 0 group by id_servis) sss on s.id = sss.id_servis
left join (select ssx.id_servis, ssx.datum, ssx.status
from servis_status ssx
right join (select id_servis, max(datum) as datum from servis_status group by id_servis) rss on rss.id_servis = ssx.id_servis and rss.datum = ssx.datum) xss on s.id = xss.id_servis
left join partners p on s.partner = p.id_partner
left join zaposlenici z on s.izradio = z.id_zaposlenik" + filter + @"
order by case when ss.datum is null then sss.datum else ss.datum end desc;";

            DSfakture = classSQL.select(sql, "fakture");
            dgv.DataSource = DSfakture.Tables[0];
            //SetDecimalInDgv(dgv, "Ukupno", "NE", "NE");
            colorGrid();
        }

        private void colorGrid()
        {
            foreach (DataGridViewRow dRow in dgv.Rows)
            {
                if (dRow.Cells["Status"].Value.ToString() == "Nulti servis")
                {
                    dRow.DefaultCellStyle.BackColor = Color.HotPink;
                    dRow.DefaultCellStyle.SelectionBackColor = Color.LightPink;
                }
                else if (dRow.Cells["Status"].Value.ToString() == "Zaprimljeno")
                {
                    dRow.DefaultCellStyle.BackColor = Color.White;
                    dRow.DefaultCellStyle.SelectionBackColor = Color.WhiteSmoke;
                }
                else if (dRow.Cells["Status"].Value.ToString() == "Servis u toku")
                {
                    dRow.DefaultCellStyle.BackColor = Color.LightGray;
                    dRow.DefaultCellStyle.SelectionBackColor = Color.DarkGray;
                    dRow.DefaultCellStyle.SelectionForeColor = Color.White;
                }
                else if (dRow.Cells["Status"].Value.ToString() == "Na vanjskom servisu")
                {
                    dRow.DefaultCellStyle.BackColor = Color.LightYellow;
                    dRow.DefaultCellStyle.SelectionBackColor = Color.DarkOrange;
                    dRow.DefaultCellStyle.SelectionForeColor = Color.White;
                }
                else if (dRow.Cells["Status"].Value.ToString() == "Završen servis")
                {
                    dRow.DefaultCellStyle.BackColor = Color.LightGreen;
                    dRow.DefaultCellStyle.SelectionBackColor = Color.DarkGreen;
                    dRow.DefaultCellStyle.SelectionForeColor = Color.White;
                }
                else
                {
                    dRow.DefaultCellStyle.BackColor = Color.LightBlue;
                    dRow.DefaultCellStyle.SelectionBackColor = Color.DarkBlue;
                    dRow.DefaultCellStyle.SelectionForeColor = Color.White;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            Report.Servis.repServis f = new Report.Servis.repServis();
            f.ImeForme = "Radni nalog servis";
            f.broj_dokumenta = dgv.CurrentRow.Cells["Barkode"].FormattedValue.ToString();
            f.godina = Convert.ToInt32(dgv.CurrentRow.Cells["Godina"].FormattedValue.ToString());
            f.ShowDialog();
        }

        private void frmSvePonude_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgv.Rows.Count > 0 && dgv.CurrentCell.RowIndex > -1)
            {
                Report.Servis.repServis f = new Report.Servis.repServis();
                f.ImeForme = "Radni nalog servis";
                f.broj_dokumenta = dgv.CurrentRow.Cells["Barkode"].FormattedValue.ToString();
                f.godina = Convert.ToInt32(dgv.CurrentRow.Cells["Godina"].FormattedValue.ToString());
                f.ShowDialog();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtPartner.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    System.Windows.Forms.SendKeys.Send("{TAB}");
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void txtPartner_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtPartner.Text == "")
                {
                    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                    partnerTrazi.ShowDialog();
                    if (Properties.Settings.Default.id_partner != "")
                    {
                        DataSet partner = new DataSet();
                        partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                        if (partner.Tables[0].Rows.Count > 0)
                        {
                            txtPartner.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                            System.Windows.Forms.SendKeys.Send("{TAB}");
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtPartner.Select();
                            txtPartner.SelectAll();
                            return;
                        }
                    }
                    else
                    {
                        txtPartner.Select();
                        txtPartner.SelectAll();
                        return;
                    }
                }

                string Str = txtPartner.Text.Trim().ToLower();
                double Num;
                bool isNum = double.TryParse(Str, out Num);

                if (isNum)
                {
                    DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + Str + "'", "partners").Tables[0];
                    if (DSpar.Rows.Count > 0)
                    {
                        txtPartner.Text = DSpar.Rows[0][0].ToString();
                        System.Windows.Forms.SendKeys.Send("{TAB}");
                    }
                    else
                    {
                        MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                        txtPartner.Select();
                        txtPartner.SelectAll();
                    }
                }
                else
                {
                    DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE LOWER(ime_tvrtke) = '" +
                        Str + "'", "partners").Tables[0];
                    if (DSpar.Rows.Count > 0)
                    {
                        txtPartner.Text = DSpar.Rows[0][0].ToString();
                        System.Windows.Forms.SendKeys.Send("{TAB}");
                    }
                    else
                    {
                        DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE LOWER(ime_tvrtke) like '%" +
                            Str + "%'", "partners").Tables[0];
                        if (DSpar.Rows.Count > 0)
                        {
                            txtPartner.Text = DSpar.Rows[0][0].ToString();
                            System.Windows.Forms.SendKeys.Send("{TAB}");
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtPartner.Select();
                            txtPartner.SelectAll();
                        }
                    }
                }
            }
        }

        private void dgv_Sorted(object sender, EventArgs e)
        {
            try
            {
                colorGrid();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void frmSviRadniNaloziServis2016_Shown(object sender, EventArgs e)
        {
            try
            {
                colorGrid();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}