using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.dodaci
{
    public partial class frmDodajKupca : Form
    {
        public frmDodajKupca()
        {
            InitializeComponent();
        }

        public dodaci.frmGrupirajPoKupcima frm { get; set; }

        private void frmDodajKupca_Load(object sender, EventArgs e)
        {
            txtImePrezime.Select();
            DataTable DT = classSQL.select("SELECT * FROM trenutni_kupci ORDER BY ime_kupca", "trenutni_kupci").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                dgv.Rows.Add(DT.Rows[i]["id"].ToString(), DT.Rows[i]["ime_kupca"].ToString());
            }

            //this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((byte)(105)), ((byte)(170)), ((byte)(197)));
            Color y = System.Drawing.Color.FromArgb(((byte)(40)), ((byte)(109)), ((byte)(135)));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDodajKupca_Click(object sender, EventArgs e)
        {
            if (txtImePrezime.Text == "")
            {
                MessageBox.Show("GREŠKA KRIVO UPISANO IME KUPCA!");
                return;
            }

            classSQL.insert("INSERT INTO trenutni_kupci (ime_kupca) VALUES ('" + txtImePrezime.Text + "')");

            DataTable DT = classSQL.select("SELECT * FROM trenutni_kupci ORDER BY ime_kupca", "trenutni_kupci").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                dgv.Rows.Add(DT.Rows[i]["id"].ToString(), DT.Rows[i]["ime_kupca"].ToString());
            }

            DT = classSQL.select("SELECT * FROM trenutni_kupci WHERE ime_kupca='" + txtImePrezime.Text + "' LIMIT 1", "trenutni_kupci").Tables[0];
            if (DT.Rows.Count > 0)
            {
                frm.id_partner = DT.Rows[0]["id"].ToString();
                frm.odabrano = true;
            }
            Close();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            frm.id_partner = dgv.CurrentRow.Cells["id"].FormattedValue.ToString();
            frm.odabrano = true;
            Close();
        }

        private void txtImePrezime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnDodajKupca.PerformClick();
            }
        }
    }
}