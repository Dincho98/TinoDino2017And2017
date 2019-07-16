using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.sinkronizacija_poslovnica
{
    public partial class frmSkladistaSinkronizacija : Form
    {
        public frmSkladistaSinkronizacija()
        {
            InitializeComponent();
        }

        private void frmSkladistaSinkronizacija_Load(object sender, EventArgs e)
        {
            try
            {
                string sql = "select id_skladiste, skladiste, id_skl_centrala from skladiste;";
                DataSet dsSkladiste = classSQL.select(sql, "skladiste");

                if (dsSkladiste != null && dsSkladiste.Tables.Count > 0 && dsSkladiste.Tables[0] != null && dsSkladiste.Tables[0].Rows.Count > 0)
                {
                    dgvSkladiste.DataSource = dsSkladiste.Tables[0];
                }
                else
                {
                    MessageBox.Show("Nemate kreirana skladista");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> iList = new List<int>(); ;
                foreach (DataGridViewRow dRow in dgvSkladiste.Rows)
                {
                    int i = 0;

                    if (!int.TryParse(dRow.Cells["id_skl_centrala"].Value.ToString(), out i))
                    {
                        MessageBox.Show("Krivo upisani podaci za centralno skladiste.");
                        return;
                    }

                    if (iList.Contains(i) && i != 0)
                    {
                        MessageBox.Show("Krivo upisani podaci za centralno skladiste.");
                        return;
                    }
                    if (!iList.Contains(i))
                        iList.Add(i);
                }

                foreach (DataGridViewRow dRow in dgvSkladiste.Rows)
                {
                    int id_skladiste = Convert.ToInt32(dRow.Cells["id_skladiste"].Value);
                    int id_skl_centrala = Convert.ToInt32(dRow.Cells["id_skl_centrala"].Value);
                    string sql = "";

                    if (id_skl_centrala == 0)
                    {
                        sql = @"update skladiste set id_skl_centrala = '" + id_skl_centrala + @"' where id_skladiste = '" + id_skladiste + @"'";
                        classSQL.select(sql, "skladiste");
                    }
                    else
                    {
                        sql = "select * from skladiste where id_skladiste = '" + id_skl_centrala + "'";
                        DataSet dsExist = classSQL.select(sql, "skladiste");

                        if (dsExist != null && dsExist.Tables.Count > 0 && dsExist.Tables[0] != null && dsExist.Tables[0].Rows.Count > 0 && id_skladiste != id_skl_centrala)
                        {
                            //promjena id-ja na prvi slobodni

                            int indexExist = 0;

                            foreach (DataGridViewRow dr in dgvSkladiste.Rows)
                            {
                                if (Convert.ToInt32(dr.Cells["id_skladiste"].Value) == id_skl_centrala)
                                {
                                    indexExist = dr.Index;
                                    break;
                                }
                            }

                            int newId = 0;
                            newId = Convert.ToInt32(classSQL.select(@"SELECT min(gs.generate_series) as id
FROM (select generate_series from GENERATE_SERIES(1, (select max(id_skladiste) zbroj 2 from skladiste))) gs
left join skladiste s on gs.generate_series::numeric = coalesce(id_skladiste::numeric, 0)
where s.id_skladiste is null and gs.generate_series != '" + id_skl_centrala + @"'", "skladiste").Tables[0].Rows[0]["id"]);

                            dgvSkladiste.Rows[indexExist].Cells["id_skladiste"].Value = newId;

                            sql = @"select changeskladiste(" + id_skl_centrala + @", " + newId + ");";
                            classSQL.select(sql, "skladiste");

                            //

                            sql = @"select changeskladiste(" + id_skladiste + @", " + id_skl_centrala + @");
                            update skladiste set id_skl_centrala = '" + id_skl_centrala + @"' where id_skladiste = '" + id_skl_centrala + @"';";
                            classSQL.select(sql, "skladiste");
                        }
                        else
                        {
                            sql = @"select changeskladiste(" + id_skladiste + @", " + id_skl_centrala + @");
                            update skladiste set id_skl_centrala = '" + id_skl_centrala + @"' where id_skladiste = '" + id_skl_centrala + @"'";
                            classSQL.select(sql, "skladiste");

                            foreach (DataGridViewRow dr in dgvSkladiste.Rows)
                            {
                                if (Convert.ToInt32(dr.Cells["id_skladiste"].Value) == id_skladiste)
                                {
                                    dr.Cells["id_skladiste"].Value = id_skl_centrala;
                                    break;
                                }
                            }
                        }
                    }
                }

                MessageBox.Show("Izvršeno.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}