using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmObracunporeza : Form
    {
        public frmObracunporeza()
        {
            InitializeComponent();
        }

        private void frmObracunporeza_Load(object sender, EventArgs e)
        {
            Set();

            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 1000);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void Set()
        {
            //DataTable DTSK = new DataTable("Izlracuni");

            //DTSK.Columns.Add("id", typeof(string));
            //DTSK.Columns.Add("naziv", typeof(string));

            string sqlstring = "SELECT " +
            "ducan.id_ducan as id ," +
            "ducan.ime_ducana as ducan " +
            "FROM ducan";

            DataTable DTcombo1;
            DTcombo1 = classSQL.select(sqlstring, "ducan").Tables[0];

            string sqlstring1 = "SELECT " +
            "blagajna.id_blagajna as id ," +
            "blagajna.ime_blagajne as kasa " +
            "FROM blagajna";

            DataTable DTcombo2;
            DTcombo2 = classSQL.select(sqlstring1, "kasa").Tables[0];

            string sqlstring2 = "SELECT " +
            "zaposlenici.id_zaposlenik as id ," +
            "zaposlenici.ime || '  ' || zaposlenici.prezime as blagajnik " +
            "FROM zaposlenici";

            DataTable DTcombo3;
            DTcombo3 = classSQL.select(sqlstring2, "blagajnik").Tables[0];

            //DTSK.Rows.Add("kalk", "Kalkulacije");
            //DTSK.Rows.Add("mpra", "Malo prodajni računi");
            //DTSK.Rows.Add("otpr", "Otpremnice");
            //DTSK.Rows.Add("izda", "Izdatnice");
            //DTSK.Rows.Add("prim", "Primke");
            //DTSK.Rows.Add("fakt", "Fakture");

            comboBox1.DataSource = DTcombo1;

            comboBox1.DisplayMember = "ducan";

            comboBox1.ValueMember = "id";

            comboBox2.DataSource = DTcombo2;

            comboBox2.DisplayMember = "kasa";

            comboBox2.ValueMember = "id";

            comboBox3.DataSource = DTcombo3;

            comboBox3.DisplayMember = "blagajnik";

            comboBox3.ValueMember = "id";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Report.ObracunPoreza.frmListe5 lista = new Report.ObracunPoreza.frmListe5();

            if (ckbducan.Checked == false)
            {
                lista.ducbool = false;
            }
            else
            {
                lista.ducbool = true;
            }

            if (ckbkasa.Checked == false)
            {
                lista.kasbool = false;
            }
            else
            {
                lista.kasbool = true;
            }

            if (ckbblagajnik.Checked == false)
            {
                lista.blabool = false;
            }
            else
            {
                lista.blabool = true;
            }

            if (ckbracun.Checked == false)
            {
                lista.racbool = false;
            }
            else
            {
                lista.racbool = true;
            }

            lista.racunodd = textBox1.Text;

            lista.racundoo = textBox2.Text;

            lista.ducan = comboBox1.SelectedValue.ToString();

            lista.kasa = comboBox2.SelectedValue.ToString();

            lista.blagajnik = comboBox3.Text;

            lista.racunOD = textBox1.Text;

            lista.racunDO = textBox2.Text;

            lista.datumOD = dateTimePicker1.Value;

            lista.datumDO = dateTimePicker2.Value;

            lista.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Report.ObracunPoreza.frmobrfak lista = new Report.ObracunPoreza.frmobrfak();

            if (ckbblagajnik.Checked == false)
            {
                lista.blabool = false;
            }
            else
            {
                lista.blabool = true;
            }

            if (ckbracun.Checked == false)
            {
                lista.racbool = false;
            }
            else
            {
                lista.racbool = true;
            }

            lista.ducbool = ckbducan.Checked;
            lista.kasbool = ckbkasa.Checked;

            lista.racunodd = textBox1.Text;

            lista.racundoo = textBox2.Text;

            lista.ducan = comboBox1.SelectedValue.ToString();

            lista.kasa = comboBox2.SelectedValue.ToString();

            lista.blagajnik = comboBox3.Text;

            lista.racunOD = textBox1.Text;

            lista.racunDO = textBox2.Text;

            lista.datumOD = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day, 0, 0, 0);

            lista.datumDO = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day, 23, 59, 59);

            lista.ShowDialog();
        }
    }
}