using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Odrzavanja
{
    public partial class Odrzavanje_promjena : Form
    {
        public Odrzavanje_promjena()
        {
            InitializeComponent();
        }

        private DataSet DSpartneri = new DataSet();

        private void txtIme_TextChanged(object sender, EventArgs e)
        {
            string top = "";
            string remote = "";
            string where = "";
            if (classSQL.remoteConnectionString != "")
            {
                remote = " LIMIT 500";
                where = "ime_tvrtke ~* '" + txtIme.Text + "'";
            }
            else
            {
                where = "ime_tvrtke LIKE '%" + txtIme.Text + "%'";
                top = " TOP(500) ";
            }
            dgv.Rows.Clear();
            DSpartneri = classSQL.select("SELECT " + top + " partners_odrzavanje.id_partner AS sifra, partners.ime_tvrtke AS Partner," +
            " partners_odrzavanje.odrzavanje_kol AS odrzavanje_kol, partners.tel as tel  FROM partners_odrzavanje " +
            "LEFT JOIN partners ON partners.id_partner = partners_odrzavanje.id_partner " +
            "WHERE " + where + " ORDER BY partners.ime_tvrtke " + remote + "", "partners");
            //dgv.DataSource = DSpartneri.Tables[0];
            for (int i = 0; i < DSpartneri.Tables[0].Rows.Count; i++)
            {
                dgv.Rows.Add(DSpartneri.Tables[0].Rows[i]["sifra"].ToString(), DSpartneri.Tables[0].Rows[i]["partner"].ToString(), DSpartneri.Tables[0].Rows[i]["odrzavanje_kol"].ToString(), DSpartneri.Tables[0].Rows[i]["tel"].ToString());
            }
            PaintRows(dgv);
        }

        private void Popuni_grid()
        {
            DSpartneri = classSQL.select("SELECT  partners_odrzavanje.id_partner AS sifra, " +
                        " partners.ime_tvrtke AS Partner, partners_odrzavanje.odrzavanje_kol AS odrzavanje_kol," +
                        " partners.tel as tel  FROM partners_odrzavanje " +
                        " LEFT JOIN partners ON partners.id_partner = partners_odrzavanje.id_partner " +
                        " ORDER BY partners.ime_tvrtke ", "partners");
            //dgv.DataSource = DSpartneri.Tables[0];
            for (int i = 0; i < DSpartneri.Tables[0].Rows.Count; i++)
            {
                dgv.Rows.Add(DSpartneri.Tables[0].Rows[i]["sifra"].ToString(), DSpartneri.Tables[0].Rows[i]["partner"].ToString(), DSpartneri.Tables[0].Rows[i]["odrzavanje_kol"].ToString(), DSpartneri.Tables[0].Rows[i]["tel"].ToString());
            }
            PaintRows(dgv);
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
            row.Height = 25;
        }

        private void Odrzavanje_promjena_Load(object sender, EventArgs e)
        {
            Popuni_grid();
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell.ColumnIndex == 3)
            {
                try
                {
                    string sql = "UPDATE partners SET tel='" + dgv.Rows[e.RowIndex].Cells["tel"].FormattedValue.ToString() + "' WHERE id_partner='" + dgv.Rows[e.RowIndex].Cells["sifra"].FormattedValue.ToString() + "'";
                    classSQL.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void txtTraziPremaSifri_TextChanged(object sender, EventArgs e)
        {
            string top = "";
            string remote = "";
            string where = "";
            if (classSQL.remoteConnectionString != "")
            {
                remote = " LIMIT 500";
                where = "CAST(partners_odrzavanje.id_partner as character varying) ~* '" + txtTraziPremaSifri.Text + "'";
            }
            else
            {
                where = "CAST(partners_odrzavanje.id_partner as character varying) LIKE '%" + txtTraziPremaSifri.Text + "%'";
                top = " TOP(500) ";
            }
            dgv.Rows.Clear();
            DSpartneri = classSQL.select("SELECT " + top + " partners_odrzavanje.id_partner AS sifra, partners.ime_tvrtke AS Partner," +
            " partners_odrzavanje.odrzavanje_kol AS odrzavanje_kol, partners.tel as tel  FROM partners_odrzavanje " +
            "LEFT JOIN partners ON partners.id_partner = partners_odrzavanje.id_partner " +
            "WHERE " + where + " ORDER BY partners.ime_tvrtke " + remote + "", "partners");
            //dgv.DataSource = DSpartneri.Tables[0];
            if (DSpartneri.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DSpartneri.Tables[0].Rows.Count; i++)
                {
                    dgv.Rows.Add(DSpartneri.Tables[0].Rows[i]["sifra"].ToString(), DSpartneri.Tables[0].Rows[i]["partner"].ToString(), DSpartneri.Tables[0].Rows[i]["odrzavanje_kol"].ToString(), DSpartneri.Tables[0].Rows[i]["tel"].ToString());
                }
            }

            PaintRows(dgv);
        }

        private void Odrzavanje_promjena_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }
    }
}