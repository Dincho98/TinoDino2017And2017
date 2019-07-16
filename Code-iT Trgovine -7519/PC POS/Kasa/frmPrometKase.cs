using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Kasa
{
    public partial class frmPrometKase : Form
    {
        public frmPrometKase()
        {
            InitializeComponent();
        }

        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private void frmPrometKase_Load(object sender, EventArgs e)
        {
            SetCB();
            dtpOD.Value = Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " 00:00:00");
            dtpDO.Value = Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " 23:59:59");
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 1000);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private DataTable DT_Zaposlenik;
        private DataTable DT_Skladiste;
        private DataTable DT_Ducan;
        private DataTable DT_Zaposlenik2 = new DataTable();
        private DataTable DT_Skladiste2 = new DataTable();
        private DataTable DT_Grupe = new DataTable();
        private DataTable DT_dobavljaci = new DataTable();
        private DataTable DT_Ducan2 = new DataTable();

        //DataTable DT_Ducan;
        private void SetCB()
        {
            //fill komercijalist
            DT_Ducan = classSQL.select("SELECT ime_ducana,id_ducan FROM ducan", "ducan").Tables[0];
            cbDucan.DataSource = DT_Ducan;
            cbDucan.DisplayMember = "ime_ducana";
            cbDucan.ValueMember = "id_ducan";

            cbDucan.SelectedValue = DTpostavke.Rows[0]["default_ducan"].ToString();

            //fill komercijalist
            DT_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici").Tables[0];
            cbZaposlenik.DataSource = DT_Zaposlenik;
            cbZaposlenik.DisplayMember = "IME";
            cbZaposlenik.ValueMember = "id_zaposlenik";

            //fill grupe
            DT_Grupe = classSQL.select("SELECT grupa as Grupa,id_grupa FROM grupa", "grupa").Tables[0];
            cbGrupe.DataSource = DT_Grupe;
            cbGrupe.DisplayMember = "Grupa";
            cbGrupe.ValueMember = "id_grupa";

            //fill dobavljaci
            DT_dobavljaci = classSQL.select("SELECT ime_tvrtke as Tvrtka,id_partner FROM partners WHERE vrsta_korisnika = '1' ORDER BY ime_tvrtke", "partners").Tables[0];
            cbdobavljaci.DataSource = DT_dobavljaci;
            cbdobavljaci.DisplayMember = "Tvrtka";
            cbdobavljaci.ValueMember = "id_partner";

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

        private void dtpOD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.F1)
            {
                btnIspis.PerformClick();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnIspisPOS.PerformClick();
            }
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            Report.Liste2.frmListe aa = new Report.Liste2.frmListe();
            aa.datumOD = dtpOD.Value;
            aa.datumDO = dtpDO.Value;
            aa.ImeForme = "Promet kase";
            aa.dokumenat = "PROMET";
            if (chbDucan.Checked)
            {
                aa.ducan = cbDucan.SelectedValue.ToString();
            }
            if (chbKasa.Checked)
            {
                aa.kasa = cbKasa.SelectedValue.ToString();
            }
            if (chbBlagajnik.Checked)
            {
                aa.blagajnik = cbZaposlenik.SelectedValue.ToString();
            }
            if (chbSkladiste.Checked)
            {
                aa.skladiste = cbSkladiste.SelectedValue.ToString();
            }
            aa.ShowDialog();

            //this.Close();
        }

        private void btnIspisPOS_Click(object sender, EventArgs e)
        {
            Kasa.frmPosPreview aa = new Kasa.frmPosPreview();
            aa.datumOD = dtpOD.Value;
            aa.datumDO = dtpDO.Value;
            aa.ImeForme = "Promet po danima";
            aa.dokumenat = "PrometRobe";
            aa.stavke_ispis = checkBox1.Checked;

            if (chbDucan.Checked)
            {
                aa.ducan = cbDucan.SelectedValue.ToString();
            }

            if (chbKasa.Checked)
            {
                aa.kasa = cbKasa.SelectedValue.ToString();
            }

            if (chbBlagajnik.Checked)
            {
                aa.blagajnik = cbZaposlenik.SelectedValue.ToString();
            }

            if (chbSkladiste.Checked)
            {
                aa.id_podgrupa = cbSkladiste.SelectedValue.ToString();
            }

            if (chbGrupe.Checked)
            {
                aa.grupa = cbGrupe.SelectedValue.ToString();
            }

            if (chbdobavljaci.Checked)
            {
                aa.dobavljac = cbdobavljaci.SelectedValue.ToString();
            }

            aa.ShowDialog();
        }

        private void btnIspisPOS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnIspisPOS.PerformClick();
            }
        }

        //private void printaj()
        //{
        //    string printerName = DTsetting.Rows[0]["windows_printer_name"].ToString();
        //    PrintDocument printDoc = new PrintDocument();

        //    printDoc.PrinterSettings.PrinterName = printerName;

        //    if (stari_printer)
        //    {
        //        string ttx = "\r\n" + _1 + _2 + _3 + _4 + _5;
        //        ttx = ttx.Replace("č", "c");
        //        ttx = ttx.Replace("Č", "C");
        //        ttx = ttx.Replace("ž", "z");
        //        ttx = ttx.Replace("Ž", "Z");
        //        ttx = ttx.Replace("ć", "c");
        //        ttx = ttx.Replace("Ć", "C");
        //        ttx = ttx.Replace("đ", "d");
        //        ttx = ttx.Replace("Đ", "D");
        //        ttx = ttx.Replace("š", "s");
        //        ttx = ttx.Replace("Š", "S");

        //        string GS = Convert.ToString((char)29);
        //        string ESC = Convert.ToString((char)27);

        //        string COMMAND = "";
        //        COMMAND = ESC + "@";
        //        COMMAND += GS + "V" + (char)1;

        //        RawPrinterHelper.SendStringToPrinter(printDoc.PrinterSettings.PrinterName, ttx + COMMAND);
        //        BoolPreview = false;
        //        stari_printer = false;
        //        return;
        //    }

        //    if (DTpostavke.Rows[0]["direct_print"].ToString() == "1")
        //    {
        //        if (DTpostavke.Rows[0]["ladicaOn"].ToString() == "1")
        //        {
        //            openCashDrawer1();
        //        }

        //        string ttx = "\r\n" + _1 + _2 + _3 + _4 + _5;
        //        ttx = ttx.Replace("č", "c");
        //        ttx = ttx.Replace("Č", "C");
        //        ttx = ttx.Replace("ž", "z");
        //        ttx = ttx.Replace("Ž", "Z");
        //        ttx = ttx.Replace("ć", "c");
        //        ttx = ttx.Replace("Ć", "C");
        //        ttx = ttx.Replace("đ", "d");
        //        ttx = ttx.Replace("Đ", "D");
        //        ttx = ttx.Replace("š", "s");
        //        ttx = ttx.Replace("Š", "S");

        //        string GS = Convert.ToString((char)29);
        //        string ESC = Convert.ToString((char)27);

        //        string COMMAND = "";
        //        COMMAND = ESC + "@";
        //        COMMAND += GS + "V" + (char)1;

        //        RawPrinterHelper.SendStringToPrinter(printDoc.PrinterSettings.PrinterName, ttx + COMMAND);
        //    }
        //    else
        //    {
        //        if (!printDoc.PrinterSettings.IsValid)
        //        {
        //            string msg = String.Format(
        //               "Can't find printer \"{0}\".", printerName);
        //            MessageBox.Show(msg, "Print Error");
        //            return;
        //        }
        //        printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
        //        printDoc.Print();
        //    }
        //    BoolPreview = false;

        //}

        private void button1_Click(object sender, EventArgs e)
        {
            Report.Kasa.frmPrometKase pr = new Report.Kasa.frmPrometKase();
            pr.ShowDialog();
        }

        private void chbDucan_CheckedChanged(object sender, EventArgs e)
        {
            cbDucan.Enabled = !chbDucan.Checked;
            cbKasa.Enabled = chbDucan.Checked;
            chbKasa.Enabled = chbDucan.Checked;

            if (chbDucan.Checked)
            {
                DataTable DTblagajna = classSQL.select("SELECT * FROM blagajna WHERE id_ducan=" + cbDucan.SelectedValue.ToString(), "blagajna").Tables[0];
                cbKasa.DataSource = DTblagajna;
                cbKasa.DisplayMember = "ime_blagajne";
                cbKasa.ValueMember = "id_blagajna";
                cbKasa.SelectedValue = DTpostavke.Rows[0]["default_blagajna"].ToString();
            }
            else chbKasa.Checked = false;
        }
    }
}