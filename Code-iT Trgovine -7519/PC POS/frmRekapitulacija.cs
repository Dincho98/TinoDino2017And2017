using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmRekapitulacija : Form
    {
        public frmRekapitulacija()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selekcija = cbrekapitulacija.SelectedValue.ToString();

            if (selekcija.IndexOf("analitika") != -1)
            {
                Report.RekapitulacijaAnalitika.RekapitulacijaAnalitika frm = new Report.RekapitulacijaAnalitika.RekapitulacijaAnalitika();
                switch (selekcija)
                {
                    case "analitikam":
                        frm.cijena = "mpc";
                        break;

                    case "analitikav":
                        frm.cijena = "vpc";
                        break;

                    case "analitikan":
                        frm.cijena = "nbc";
                        break;
                }

                frm.DatumOD = dateTimePicker1.Value;
                frm.DatumDO = dateTimePicker2.Value;
                if (checkBox1.Checked) frm.skladiste = cbskladista.SelectedValue.ToString();

                if (cbRabati.Checked)
                {
                    frm.sa_rabatom = cbRabati.Checked;
                }

                if (cbSamoRobnoBezUsluga.Checked)
                {
                    frm.samo_robno = cbSamoRobnoBezUsluga.Checked;
                }

                frm.ShowDialog();

                return;
            }

            Report.Rekapitulacija.frmListe6 rfak = new Report.Rekapitulacija.frmListe6();

            if (checkBox1.Checked == true)
            {
                rfak.bool1 = true;
            }
            else
            {
                rfak.bool1 = false;
            }

            if (cbRabati.Checked)
            {
                rfak.sa_rabatom = cbRabati.Checked;
            }

            if (cbSamoRobnoBezUsluga.Checked)
            {
                rfak.samo_robno = cbSamoRobnoBezUsluga.Checked;
            }

            //if (chbBrojKalk.Checked)
            //{
            //    int a;
            //    if (int.TryParse(textBox1.Text, out a))
            //    {
            //        rfak.boolBrojKalk = textBox1.Text;
            //    }
            //    else
            //    {
            //        rfak.boolBrojKalk = null;
            //    }
            //}
            //else
            //{
            //    rfak.boolBrojKalk = null;
            //}

            rfak.skladiste_odabir = cbskladista.SelectedValue.ToString();

            rfak.skladiste_brojac = cbskladista.SelectedIndex;

            //cbrekapitulacija.SelectedValue = 0;

            rfak.documenat = selekcija;

            rfak.datumOD = dateTimePicker1.Value;

            rfak.datumDO = dateTimePicker2.Value;

            rfak.ShowDialog();
        }

        private void frmRekapitulacija_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);
            dateTimePicker2.Value = new DateTime(DateTime.Now.Year, 12, 31, 23, 59, 59);
            setizr();
        }

        private void setizr()
        {
            DataTable DTSK = new DataTable("Izlracuni");

            DTSK.Columns.Add("id", typeof(string));
            DTSK.Columns.Add("naziv", typeof(string));

            DTSK.Rows.Add("SINT_PRO_CJE", "SINTETIKA - po pro. cijenama");
            DTSK.Rows.Add("SINT_NAB_CJE", "SINTETIKA - po nab. cijenama");
            DTSK.Rows.Add("SINT_SKL_CJE", "SINTETIKA - po skl. cijenama");
            DTSK.Rows.Add("analitikam", "ANALITIKA - po pro. cijenama");
            DTSK.Rows.Add("analitikan", "ANALITIKA - po nab. cijenama");
            DTSK.Rows.Add("analitikav", "ANALITIKA - po skl. cijenama");

            //DTSK.Rows.Add("otpr", "Otpremnice");
            //DTSK.Rows.Add("izda", "Izdatnice");
            //DTSK.Rows.Add("prim", "Primke");
            //DTSK.Rows.Add("fakt", "Fakture");

            cbrekapitulacija.DataSource = DTSK;

            cbrekapitulacija.DisplayMember = "naziv";

            cbrekapitulacija.ValueMember = "id";

            string sqlstring = "SELECT " +
            "skladiste.skladiste," +
            "skladiste.id_skladiste " +
            " FROM skladiste";

            DataTable DTcombo1;
            DTcombo1 = classSQL.select(sqlstring, "skladiste").Tables[0];

            cbskladista.DataSource = DTcombo1;

            cbskladista.DisplayMember = "skladiste";

            cbskladista.ValueMember = "id_skladiste";
        }

        private void frmRekapitulacija_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }
    }
}