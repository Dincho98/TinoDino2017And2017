using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PCPOS.Kartoteka
{
    public partial class frmMenuKarto : Form
    {
        public frmMenuKarto()
        {
            InitializeComponent();
        }

        private bool podsjetime = false;
        private bool rodjcest = false;
        private string moj_email = "";
        private string moj_passw = "";
        private string temarodj = "";
        private string sadrzajrodj = "";
        private string id_par = "";

        private Int32 var1 = 100;
        private Int32 var2 = 100;
        private Int32 var3 = 100;
        private Int32 var4 = 200;
        private Int32 var5 = 200;
        private Int32 var6 = 200;

        private void frmMenu_Load(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\postavke.xml";

            if (!ProvjeraXML.Provjeri(path))
            {
                //MessageBox.Show("Ne postoji datoteka postavke.xml!", "Greška!");
                this.Close();
                return;
            }

            SetRemoteFields();

            //provjeri();
            timer1.Start();

            RodendanskaCestitka();
            popuni_2();

            timer1.Interval = 1000;
        }

        private void RodendanskaCestitka()
        {
            if (rodjcest == true)
            {
                DataTable DT = classSQL.select("SELECT ime_tvrtke, ime, prezime, id_partner, email FROM partners" +
                 " Where date_part('DAY', datum_rodenja) = date_part('DAY', CURRENT_DATE)" +
                 " And date_part('MONTH', datum_rodenja) = date_part('MONTH', CURRENT_DATE)" +
                 " And (godina_cestitke != date_part('YEAR', CURRENT_DATE) OR godina_cestitke is null)", "partners").Tables[0];
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    string email = DT.Rows[i]["email"].ToString();
                    string id_partner = DT.Rows[i]["id_partner"].ToString();

                    if (Validator.EmailIsValid(email) == true)
                    {
                        string postavi_godinu = "UPDATE partners SET godina_cestitke = date_part('YEAR', CURRENT_DATE) WHERE id_partner = '" + id_partner + "'";
                        classSQL.update(postavi_godinu);

                        Util.classMail.send_email(moj_email, moj_passw, email, temarodj, sadrzajrodj);
                    }
                }
            }
        }

        private void provjeri()
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\postavke.xml";

            // provjeri ako postoji folder
            if (File.Exists(path))
            {
                return;
            }

            // Stvori folder ako ne postoji

            File.Create(path);
            File.GetCreationTime(path);
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
            //row.Height = 25;
        }

        private string puni_tabl_x = "";
        private string puni_tabl_y = "";
        private string puni_tabl_id = "";

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            DTon_click.Clear();
            dgv2.Rows.Clear();
            dgv2.Columns.Clear();
            djelatnici();

            if (DTon_click.Columns.Count == 0)
            {
                DTon_click.Clear();
                DTon_click.Columns.Add("_y");
                DTon_click.Columns.Add("_x");
                DTon_click.Columns.Add("id_unos");
            }
            DataRow Row;

            string sve = "SELECT * FROM kar_podsjetnik WHERE datum >= '" + monthCalendar1.SelectionRange.Start.ToString("yyyy-MM-dd 00:00:00") + "' AND datum <= '" + monthCalendar1.SelectionRange.Start.ToString("yyyy-MM-dd 23:59:59") + "'";
            DataTable DTsve = classSQL.select(sve, "podsje").Tables[0];
            for (int y = 0; y < DTsve.Rows.Count; y++)
            {
                DateTime datum = Convert.ToDateTime(DTsve.Rows[y]["datum"].ToString());
                DateTime datum_kalendar = monthCalendar1.SelectionStart;
                string zaposlenik = DTsve.Rows[y]["id_zaposlenik"].ToString();
                string Ime = DTsve.Rows[y]["ime_partnera"].ToString();
                string Prezime = DTsve.Rows[y]["prezime_partnera"].ToString();
                string napomena = DTsve.Rows[y]["napomena"].ToString();
                DateTime sat = datum;
                string minute = datum.ToString("mm");
                int row = 0;

                if (datum.Date == datum_kalendar.Date && datum.Hour < 20 && datum.Hour > 5)
                {
                    for (int i = 0; i < dgv2.Rows.Count; i++)
                    {
                        string[] vrijeme = dgv2.Rows[i].Cells[0].FormattedValue.ToString().Split(':');
                        if (vrijeme[0] == Convert.ToString(sat.Hour))
                        {
                            if (Convert.ToInt16(minute) < 15)
                            {
                                row = i;
                                break;
                            }
                            else if (Convert.ToInt16(minute) >= 15 && Convert.ToInt16(minute) <= 30)
                            {
                                row = i++;
                                row = i;
                                break;
                            }
                            else if (Convert.ToInt16(minute) <= 45 && Convert.ToInt16(minute) > 30)
                            {
                                row = i++;
                                row = i;
                                break;
                            }
                            else if (Convert.ToInt16(minute) > 45)
                            {
                                row = i + 2;
                                break;
                            }
                        }
                    }
                }

                //DTon_click.Clear();
                for (int k = 0; k < dgv2.Columns.Count; k++)
                {
                    string kolona = dgv2.Columns[k].Name;

                    if (zaposlenik == kolona)
                    {
                        if (datum.Date == datum_kalendar.Date && datum.Hour < 20 && datum.Hour > 5)
                        {
                            dgv2.Rows[row].Cells[k].Value = Prezime + "  " + Truncate(napomena, 20);

                            Row = DTon_click.NewRow();
                            Row["_x"] = row;
                            Row["_y"] = k;
                            Row["id_unos"] = DTsve.Rows[y]["id"].ToString();
                            DTon_click.Rows.Add(Row);

                            //Row = DTon_click.NewRow();
                            //Row["_x"] = row;
                            //Row["_y"] = k;
                            //Row["id_unos"] = DTsve.Rows[y]["id"].ToString();
                            //DTon_click.Rows.Add(Row);

                            DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
                            CellStyle.BackColor = Color.LightBlue;
                            dgv2.Rows[row].Cells[k].Style = CellStyle;
                        }
                    }
                }
            }
            PaintRows(dgv2);
        }

        private void djelatnici()
        {
            DataTable Djelatnici = classSQL.select("SELECT ime, prezime, id_zaposlenik FROM zaposlenici", "zaposlenici").Tables[0];

            for (int i = 0; i < Djelatnici.Rows.Count; i++)
            {
                string ime = Djelatnici.Rows[i]["ime"].ToString() + " " + Djelatnici.Rows[i]["prezime"].ToString();
                dgv2.Columns.Add(new DataGridViewColumn() { HeaderText = "", Name = "__" + i.ToString(), CellTemplate = new DataGridViewTextBoxCell() });
                dgv2.Columns["__" + i.ToString()].Width = 40;
                dgv2.Columns.Add(new DataGridViewColumn() { HeaderText = ime.ToString(), Name = Djelatnici.Rows[i]["id_zaposlenik"].ToString(), CellTemplate = new DataGridViewTextBoxCell() });
                dgv2.Columns[Djelatnici.Rows[i]["id_zaposlenik"].ToString()].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;

                int dan = DateTime.Now.Day;
                int mjesec = DateTime.Now.Month;
                int godina = DateTime.Now.Year;
                DateTime pocetno = Convert.ToDateTime(godina.ToString() + "-" + mjesec.ToString() + "-" + dan.ToString() + " 07:00");
                for (int y = 0; y < 31; y++)
                {
                    if (i == 0)
                    {
                        dgv2.Rows.Add();
                    }

                    dgv2.Rows[y].Cells["__" + i.ToString()].Value = pocetno.AddHours(y * 0.5).ToString("H:mm");
                }
            }
            foreach (DataGridViewColumn c in dgv2.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Arial", 12F, GraphicsUnit.Pixel);
            }
        }

        public void popuni()
        {
            string sql1 = "SELECT ime_partnera , prezime_partnera , datum , napomena FROM kar_podsjetnik WHERE datum > '" + DateTime.Today.AddDays(-1).ToString("dd-MM-yyyy 23:59:59") + "' AND datum < '" + DateTime.Today.AddDays(1).ToString("dd-MM-yyyy 00:00:00") + "'";
            dgv2.Rows.Clear();
            DataTable DT = classSQL.select(sql1, "kalendar").Tables[0];

            foreach (DataRow row in DT.Rows)
            {
                dgv2.Rows.Add(row["ime_partnera"].ToString(),
                    row["prezime_partnera"].ToString(),
                    row["datum"].ToString(),
                    row["napomena"].ToString()
                    );
            }
            PaintRows(dgv2);
        }

        private int satipreset = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (podsjetime == false)
            {
                try
                {
                    string notify = "SELECT ime_partnera, prezime_partnera, datum, napomena, obavijest_ekran, id_partner, rodendan_bool, email_klijenta FROM kar_podsjetnik ";

                    DataTable notif = classSQL.select(notify, "podsjetnik").Tables[0];

                    int sati = -satipreset;

                    for (int i = 0; i < notif.Rows.Count; i++)
                    {
                        DateTime DatZakazano = Convert.ToDateTime(notif.Rows[i]["datum"].ToString());

                        bool ob_ekr = Convert.ToBoolean(notif.Rows[i]["obavijest_ekran"].ToString());
                        string a = notif.Rows[i]["obavijest_ekran"].ToString();
                        string ime = notif.Rows[i]["ime_partnera"].ToString();

                        string prezime = notif.Rows[i]["prezime_partnera"].ToString();

                        string napomena = notif.Rows[i]["napomena"].ToString();

                        string id_part = notif.Rows[i]["id_partner"].ToString();

                        string emproba = notif.Rows[i]["email_klijenta"].ToString();

                        DateTime noviDat = DatZakazano.AddHours(sati);
                        TimeSpan datums = DatZakazano - DateTime.Now;
                        string novidatum = datums.TotalHours.ToString();

                        if (ob_ekr == false)
                        {
                            if (DateTime.Now > noviDat && datums.TotalHours > 0)
                            {
                                frmpodsjetniktrenutno pods = new frmpodsjetniktrenutno();
                                pods.menu = this;
                                pods.ime_podsj = ime;
                                pods.prezime_podsj = prezime;
                                pods.datum_podsj = DatZakazano.ToString();
                                pods.napomena_podsj = napomena;
                                pods.idpartner = id_part;
                                pods.emailproba = emproba;

                                pods.ShowDialog();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                return;
            }
        }

        private void SetRemoteFields()
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\postavke.xml";
            XDocument xmlFile = XDocument.Load(path);
            var query = from c in xmlFile.Element("postavke").Elements("email").Elements("gmail") select c;
            foreach (XElement book in query)
            {
                moj_email = book.Attribute("email").Value;
                moj_passw = book.Attribute("password").Value;
                satipreset = Convert.ToInt16(book.Attribute("satidoporuke").Value);
                podsjetime = Convert.ToBoolean(book.Attribute("podsjetime").Value);
                rodjcest = Convert.ToBoolean(book.Attribute("saljicestitku").Value);
                temarodj = book.Attribute("temarodj").Value;
                sadrzajrodj = book.Attribute("sadrzajrodj").Value;
                var1 = Convert.ToInt32(book.Attribute("var1").Value);
                var2 = Convert.ToInt32(book.Attribute("var2").Value);
                var3 = Convert.ToInt32(book.Attribute("var3").Value);
                var4 = Convert.ToInt32(book.Attribute("var4").Value);
                var5 = Convert.ToInt32(book.Attribute("var5").Value);
                var6 = Convert.ToInt32(book.Attribute("var6").Value);
            }
        }

        private void frmMenu_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.FromArgb(var1, var2, var3), Color.FromArgb(var4, var5, var6), 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void postavkeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PCPOS.Kartoteka.Postavke post = new PCPOS.Kartoteka.Postavke();

            post.ShowDialog();
        }

        private void dgv2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int col = dgv2.CurrentCell.ColumnIndex;
            int row = dgv2.CurrentCell.RowIndex;

            id_par = dgv2.Rows[row].Cells[2].FormattedValue.ToString();
        }

        private void btnkrono_Click(object sender, EventArgs e)
        {
            PCPOS.frmPartnerTrazi gal = new PCPOS.frmPartnerTrazi();
            gal._pozivapregledkartoteke = true;
            gal.kartoteka_ulj = true;
            gal.ShowDialog();
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w + 3, h + 3);
        }

        private void pic_ButtonEnter(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            PB.BackColor = Color.Transparent;
            PB.FlatStyle = FlatStyle.Flat;
            PB.FlatAppearance.BorderSize = 0;
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new Size(w + 3, h + 3);
        }

        private void pic_ButtonLeave(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            PB.BackColor = Color.Transparent;
            PB.FlatStyle = FlatStyle.Flat;
            PB.FlatAppearance.BorderSize = 0;
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new Size(w - 3, h - 3);
        }

        private void provjeriDaliPostoji9NovijaVerzijaProgramaToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ažurirajProgramToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private string id_unosa = "";

        private DataTable DTon_click = new DataTable();

        public void popuni_2()
        {
            dgv2.Rows.Clear();
            DTon_click.Clear();
            dgv2.Columns.Clear();
            djelatnici();

            if (DTon_click.Columns.Count == 0)
            {
                DTon_click.Clear();
                DTon_click.Columns.Add("_y");
                DTon_click.Columns.Add("_x");
                DTon_click.Columns.Add("id_unos");
            }
            DataRow Row;

            string sve = "SELECT * FROM kar_podsjetnik WHERE datum >= '" + monthCalendar1.SelectionRange.Start.ToString("yyyy-MM-dd 00:00:00") + "' AND datum <= '" + monthCalendar1.SelectionRange.Start.ToString("yyyy-MM-dd 23:59:59") + "'";
            DataTable DTsve = classSQL.select(sve, "podsje").Tables[0];
            for (int y = 0; y < DTsve.Rows.Count; y++)
            {
                DateTime datum = Convert.ToDateTime(DTsve.Rows[y]["datum"].ToString());
                string zaposlenik = DTsve.Rows[y]["id_zaposlenik"].ToString();
                string Ime = DTsve.Rows[y]["ime_partnera"].ToString();
                string Prezime = DTsve.Rows[y]["prezime_partnera"].ToString();
                string napomena = DTsve.Rows[y]["napomena"].ToString();
                DateTime sat = datum;
                string minute = datum.ToString("mm");
                int row = 0;

                if (datum.Date == monthCalendar1.SelectionStart.Date && datum.Hour < 20 && datum.Hour > 5)
                {
                    for (int i = 0; i < dgv2.Rows.Count; i++)
                    {
                        string[] vrijeme = dgv2.Rows[i].Cells[0].FormattedValue.ToString().Split(':');
                        if (vrijeme[0] == Convert.ToString(sat.Hour))
                        {
                            if (Convert.ToInt16(minute) < 15)
                            {
                                row = i;
                                break;
                            }
                            else if (Convert.ToInt16(minute) >= 15 && Convert.ToInt16(minute) <= 30)
                            {
                                row = i++;
                                row = i;
                                break;
                            }
                            else if (Convert.ToInt16(minute) <= 45 && Convert.ToInt16(minute) > 30)
                            {
                                row = i++;
                                row = i;
                                break;
                            }
                            else if (Convert.ToInt16(minute) > 45)
                            {
                                row = i + 2;
                                break;
                            }
                        }
                    }
                }

                for (int k = 0; k < dgv2.Columns.Count; k++)
                {
                    string kolona = dgv2.Columns[k].Name;

                    if (zaposlenik == kolona)
                    {
                        if (datum.Date == monthCalendar1.SelectionStart.Date && datum.Hour < 20 && datum.Hour > 5)
                        {
                            dgv2.Rows[row].Cells[k].Value = Prezime + "  " + Truncate(napomena, 20);

                            Row = DTon_click.NewRow();
                            Row["_x"] = row;
                            Row["_y"] = k;
                            Row["id_unos"] = DTsve.Rows[y]["id"].ToString();
                            DTon_click.Rows.Add(Row);

                            DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
                            CellStyle.BackColor = Color.LightBlue;
                            dgv2.Rows[row].Cells[k].Style = CellStyle;
                            dgv2.Rows[row].Cells[k].ToolTipText = "Opis: " + napomena;
                        }
                    }
                }
            }
            PaintRows(dgv2);
        }

        public static string Truncate(string source, int length)
        {
            if (source.Length > length)
            {
                source = source.Substring(0, length);
            }
            return source;
        }

        private void dgv2_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgv2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv2.Columns[e.ColumnIndex].HeaderText != "")
                {
                    DataRow[] dataROW = DTon_click.Select("_y = '" + e.ColumnIndex + "' AND _x = '" + e.RowIndex + "'");

                    if (dataROW.Length > 0)
                    {
                        id_unosa = dataROW[0]["id_unos"].ToString();
                        if (id_unosa != "")
                        {
                            frmunospodsjetnika pod = new frmunospodsjetnika();

                            string select_unos = "SELECT * FROM kar_podsjetnik where id = '" + id_unosa + "' ";
                            DataTable DTunos = classSQL.select(select_unos, "unos").Tables[0];
                            pod.id_zaposlenik = DTunos.Rows[0]["id_zaposlenik"].ToString();
                            pod.tbxpodsjime.Text = DTunos.Rows[0]["ime_partnera"].ToString();
                            pod.tbxpodsjprezime.Text = DTunos.Rows[0]["prezime_partnera"].ToString();
                            pod.tbxpodsjdod1.Text = DTunos.Rows[0]["dodatni_podatak1"].ToString();
                            pod.tbxpodsjdod2.Text = DTunos.Rows[0]["dodatni_podatak2"].ToString();
                            pod.dateTimePicker1.Text = DTunos.Rows[0]["datum"].ToString();
                            pod.rtbxpodsjnapomena.Text = DTunos.Rows[0]["napomena"].ToString();
                            pod.tbxpodsjklijentaemail.Text = DTunos.Rows[0]["email_klijenta"].ToString();
                            pod.update_dogadaj = true;

                            pod.unos_id = Convert.ToInt16(id_unosa);
                            pod.ShowDialog();
                            popuni_2();
                        }
                    }

                    if (dataROW.Length <= 0)
                    {
                        frmunospodsjetnika unospod = new frmunospodsjetnika();
                        string koord_v = dgv2.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].FormattedValue.ToString();
                        string x = monthCalendar1.SelectionStart.ToString("dd.MM.yyyy.");
                        DateTime vrijeme = Convert.ToDateTime(koord_v);
                        unospod.dateTimePicker1.Text = x + koord_v;
                        unospod.id_zaposlenik = dgv2.Columns[e.ColumnIndex].Name;
                        unospod.ShowDialog();
                        popuni_2();
                    }
                }
                else
                {
                    return;
                }
            }
            catch
            {
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ovom akcijom brišete sve podsjetnike iz baze do današnjeg dana. Da li ste sigurni da želite sve podsjetnike?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                string slq = "DELETE FROM kar_podsjetnik WHERE datum <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                classSQL.delete(slq);
                popuni_2();
            }
        }
    }
}