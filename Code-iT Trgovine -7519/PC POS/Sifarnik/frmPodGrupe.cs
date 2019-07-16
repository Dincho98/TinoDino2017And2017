using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class frmPodGrupe : Form
    {
        public frmPodGrupe()
        {
            InitializeComponent();
        }

        private DataSet DSgrupa = new DataSet();

        private DataSet DS = new DataSet();

        private void frmGrupeProizvoda_Load(object sender, EventArgs e)
        {
            fillcombo();
            FillDGV();
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void fillcombo()
        {
            DSgrupa = classSQL.select("SELECT * FROM grupa ORDER BY grupa", "grupa");
            cbgrupe.DataSource = DSgrupa.Tables[0];
            cbgrupe.DisplayMember = "grupa";
            cbgrupe.ValueMember = "id_grupa";
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void FillDGV()
        {
            try
            {
                dgv.Rows.Clear();
                DS.Clear();
                if (classSQL.remoteConnectionString == "")
                {
                    classSQL.CeAdatpter("SELECT  podgrupa.id_podgrupa AS ID,podgrupa.naziv AS [Ime podgrupe], " +
                        " grupa.id_grupa FROM podgrupa " +
                        " LEFT JOIN grupa ON podgrupa.id_grupa = grupa.id_grupa").Fill(DS, "podgrupa");
                }
                else
                {
                    classSQL.NpgAdatpter("SELECT  podgrupa.id_podgrupa AS ID,podgrupa.naziv AS [Ime podgrupe], " +
                        " grupa.id_grupa FROM podgrupa " +
                        " LEFT JOIN grupa ON podgrupa.id_grupa = grupa.id_grupa ").Fill(DS, "podgrupa");
                }

                //dgv.DataSource = DS.Tables["podgrupa"];

                DataTable DTSK = new DataTable("skladiste");
                DTSK.Columns.Add("id_grupa", typeof(string));
                DTSK.Columns.Add("grupa", typeof(string));

                DataTable DS_Skladiste = classSQL.select("SELECT * FROM grupa", "grupa").Tables[0];
                //DTSK.Rows.Add(0,"");
                for (int i = 0; i < DS_Skladiste.Rows.Count; i++)
                {
                    DTSK.Rows.Add(DS_Skladiste.Rows[i]["id_grupa"], DS_Skladiste.Rows[i]["grupa"]);
                }

                Grupa.DataSource = DTSK;
                Grupa.DataPropertyName = "grupa";
                Grupa.DisplayMember = "grupa";
                //Grupa.HeaderText = "Grupa";
                //Grupa.Name = "Grupa";
                Grupa.Resizable = System.Windows.Forms.DataGridViewTriState.True;
                Grupa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
                Grupa.ValueMember = "id_grupa";

                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    dgv.Rows.Add(DS.Tables[0].Rows[i]["ID"].ToString(), DS.Tables[0].Rows[i]["Ime podgrupe"].ToString(), DS.Tables[0].Rows[i]["id_grupa"].ToString());
                }
                PaintRows(dgv);
                dgv.Enabled = true;
            }
            catch { }
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            if (txtpodgrupa.Text != "")
            {
                DataTable DTpost = classSQL.select("Select id_podgrupa from podgrupa where naziv = '" + txtpodgrupa.Text + "'", "").Tables[0];
                if (DTpost.Rows.Count == 0)
                {
                    string sql = "Insert into podgrupa(naziv, id_grupa) VALUES('" + txtpodgrupa.Text + "', '" + cbgrupe.SelectedValue + "')";
                    classSQL.insert(sql);
                }
                else
                {
                    MessageBox.Show("Već postoji podgrupa pod tim imenom");
                }

                FillDGV();
                txtpodgrupa.Text = "";
            }
            else
            {
                MessageBox.Show("Polje za naziv podgrupe je prazno !");
            }
        }

        private void btnobrisi_Click(object sender, EventArgs e)
        {
            try
            {
                int col = dgv.CurrentCell.ColumnIndex;
                int row = dgv.CurrentCell.RowIndex;
                int id = Convert.ToInt32(dgv.Rows[row].Cells["ID"].FormattedValue.ToString());
                string ime_pod = classSQL.select("Select naziv from podgrupa where id_podgrupa = '" + id + "'", "").Tables[0].Rows[0]["naziv"].ToString();
                DialogResult result1 = MessageBox.Show("Jeste li sigurni da želite obrisati podgrupu " + ime_pod + " ? ", "Obrisati ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result1 == DialogResult.Yes)
                {
                    classSQL.delete("DELETE FROM podgrupa WHERE id_podgrupa= '" + id + "' ");
                    FillDGV();
                }
            }
            catch { }
        }

        private void cbgrupe_SelectedValueChanged(object sender, EventArgs e)
        {
            //FillDGV();
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

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell.ColumnIndex == 1)
            {
                try
                {
                    string sql = "UPDATE podgrupa SET naziv='" + dgv.Rows[e.RowIndex].Cells["naziv"].FormattedValue.ToString() + "' WHERE id_podgrupa='" + dgv.Rows[e.RowIndex].Cells["id_podgrupa"].FormattedValue.ToString() + "'";
                    classSQL.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            if (dgv.CurrentCell.ColumnIndex == 2)
            {
                try
                {
                    string sql = "UPDATE podgrupa SET id_grupa='" + dgv.Rows[e.RowIndex].Cells["Grupa"].Value.ToString() + "' WHERE id_podgrupa='" + dgv.Rows[e.RowIndex].Cells["id_podgrupa"].FormattedValue.ToString() + "'";
                    classSQL.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void frmPodGrupe_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnDodaj.Select();
        }
    }
}