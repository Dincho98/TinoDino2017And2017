using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmAutomatskaUskladaSve : Form
    {
        public frmAutomatskaUskladaSve()
        {
            InitializeComponent();
        }

        private void frmAutomatskaUskladaSve_Load(object sender, EventArgs e)
        {
            try
            {
                dgwUskladaItems.BorderStyle = BorderStyle.None;
                dtpDatumOd.Value = Convert.ToDateTime(Util.Korisno.GodinaKojaSeKoristiUbazi + "-01-01 00:00:00");
                if (Util.Korisno.GodinaKojaSeKoristiUbazi < DateTime.Now.Year)
                {
                    dtpDatumDo.Value = Convert.ToDateTime(Util.Korisno.GodinaKojaSeKoristiUbazi + "-12-31 23:59:59");
                }

                string sql = @"select *
from skladiste where upper(aktivnost) = 'DA';";
                DataSet dsSkladista = classSQL.select(sql, "skladiste");
                if (dsSkladista != null && dsSkladista.Tables.Count > 0 && dsSkladista.Tables[0] != null && dsSkladista.Tables[0].Rows.Count > 0)
                {
                    this.cmbSkladiste.DataSource = dsSkladista.Tables[0];
                    this.cmbSkladiste.DisplayMember = "skladiste";
                    this.cmbSkladiste.ValueMember = "id_skladiste";
                }

                cbDatumOd.Checked = false;
                cbDatumDo.Checked = cbDatumOd.Checked;
                cbSkladiste.Checked = cbDatumOd.Checked;
                dtpDatumOd.Enabled = cbDatumOd.Checked;
                dtpDatumDo.Enabled = cbDatumOd.Checked;
                cmbSkladiste.Enabled = cbDatumOd.Checked;

                getAutomatskeUsklade();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cb_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox cb = sender as CheckBox;
                if (cb.Name == "cbDatumOd")
                {
                    dtpDatumOd.Enabled = cb.Checked;
                }
                else if (cb.Name == "cbDatumDo")
                {
                    dtpDatumDo.Enabled = cb.Checked;
                }
                else if (cb.Name == "cbSkladiste")
                {
                    cmbSkladiste.Enabled = cb.Checked;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbSkladiste_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                var combo = sender as ComboBox;
                DataTable dtSkladiste = (DataTable)combo.DataSource;
                if (dtSkladiste != null)
                {
                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(SystemColors.ControlLight), e.Bounds);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(new SolidBrush(SystemColors.Menu), e.Bounds);
                    }

                    e.Graphics.DrawString(dtSkladiste.Rows[e.Index]["skladiste"].ToString(),
                                                  e.Font,
                                                  new SolidBrush(Color.Black),
                                                  new Point(e.Bounds.X, e.Bounds.Y));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getAutomatskeUsklade()
        {
            try
            {
                string datumOd = "", datumDo = "", skladiste = "", sql = "";

                if (cbDatumOd.Checked)
                {
                    datumOd = " AND au.datum >= '" + dtpDatumOd.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                }

                if (cbDatumDo.Checked)
                {
                    datumDo = " AND au.datum <= '" + dtpDatumDo.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                }

                if (cbSkladiste.Checked)
                {
                    skladiste = " AND au.id_skladiste = '" + cmbSkladiste.SelectedValue + "'";
                }

                if (rbVpc.Checked)
                {
                    sql = @"select au.broj as [Broj], au.datum as [Datum], sum(aus.kolicina_ulaz) as [Kolicina ulaz],
au.id_poslovnica, au.id_naplatni_uredaj,
sum(aus.nbc_ulaz) as [Nbc ulaz],
sum(aus.vpc_ulaz) as [Vpc ulaz],
sum(aus.kolicina_izlaz) as [Kolicina izlaz],
sum(aus.nbc_izlaz_izracun) as [Nbc izlaz izracun],
sum(aus.vpc_izlaz_izracun) as [Vpc izlaz izracun],
sum(aus.nbc_izlaz) as [Nbc izlaz],
sum(aus.vpc_izlaz) as [Vpc izlaz],
sum(aus.nbc_izlaz_izracun) - sum(aus.nbc_izlaz) as [Nbc razlika],
sum(aus.vpc_izlaz_izracun) - sum(aus.vpc_izlaz) as [Vpc razlika]";
                }
                else
                {
                    sql = @"select au.broj as [Broj], au.datum as [Datum], sum(aus.kolicina_ulaz) as [Kolicina ulaz],
au.id_poslovnica, au.id_naplatni_uredaj,
sum(aus.nbc_ulaz) as [Nbc ulaz],
sum(aus.mpc_ulaz) as [Mpc ulaz],
sum(aus.kolicina_izlaz) as [Kolicina izlaz],
sum(aus.nbc_izlaz_izracun) as [Nbc izlaz izracun],
sum(aus.mpc_izlaz_izracun) as [Mpc izlaz izracun],
sum(aus.nbc_izlaz) as [Nbc izlaz],
sum(aus.mpc_izlaz) as [Mpc izlaz],
sum(aus.nbc_izlaz_izracun) - sum(aus.nbc_izlaz) as [Nbc razlika],
sum(aus.mpc_izlaz_izracun) - sum(aus.mpc_izlaz) as [Mpc razlika]";
                }

                sql += @" from automatska_usklada au
left join automatska_usklada_stavke aus on au.broj = aus.broj and au.id_poslovnica = aus.id_poslovnica and au.id_naplatni_uredaj = aus.id_naplatni_uredaj
where 1 = 1 " + datumOd + datumDo + skladiste + @"
group by au.broj, au.datum, au.id_poslovnica, au.id_naplatni_uredaj;";

                DataSet dsAutomatskaUsklada = classSQL.select(sql, "automatska_usklada");

                if (dsAutomatskaUsklada != null)
                {
                    dgwUskladaItems.DataSource = dsAutomatskaUsklada.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnIzlaz_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUcitaj_Click(object sender, EventArgs e)
        {
            try
            {
                getAutomatskeUsklade();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            try
            {
                Report.AutomatskaUsklada.frmAutomatskaUsklada f = new Report.AutomatskaUsklada.frmAutomatskaUsklada();
                DataGridViewRow dgvr = dgwUskladaItems.Rows[dgwUskladaItems.CurrentCell.RowIndex];

                f.id_poslovnica = (int)dgvr.Cells["id_poslovnica"].Value;
                f.id_naplatni_uredaj = (int)dgvr.Cells["id_naplatni_uredaj"].Value;
                f.broj = (int)dgvr.Cells["Broj"].Value;
                f.isVpc = rbVpc.Checked;
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}