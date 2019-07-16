using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmKarticaKupca : Form
    {
        private DataSet DSpostavke;

        public frmKarticaKupca()
        {
            InitializeComponent();
            lblAdresa.Text = "";
            lblKarticaKupca.Text = "";
            lblNaziv.Text = "";
            lblUkupno.Text = "";
        }

        private void frmKarticaKupca_Load(object sender, EventArgs e)
        {
            DSpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke");
            dtpOd.Value = dtpDo.Value.AddMonths(-1);
            tmTimer1.Start();
        }

        private void tmTimer1_Tick(object sender, EventArgs e)
        {
            txtKarticaKupca.Focus();
        }

        private void txtKarticaKupca_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    lblKarticaKupca.Text = txtKarticaKupca.Text;
                    getData();

                    txtKarticaKupca.Text = "";
                }
                else
                {
                    lblNaziv.Text = "";
                    lblAdresa.Text = "";
                    lblKarticaKupca.Text = "";
                    lblUkupno.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void getData()
        {
            if (lblKarticaKupca.Text.Length > 0)
            {
                string sql = "";
                DataRow drUser;
                if (chkAll.Checked)
                {
                    sql = @"select x.naziv,coalesce(sum(y.ukupno::numeric), 0) as ukupno, x.adresa
from (
select case when vrsta_korisnika = 1 then ime_tvrtke else concat(ime, ' ', prezime) end as naziv, broj_kartice, adresa from partners where broj_kartice = '" + lblKarticaKupca.Text + @"'
) x
left join (select * from racuni where racuni.datum_racuna >=
(select coalesce(max(datum_resetiranja), '1990-01-03 00:00:00') as datum
from reset_kartica_kupca
where kartica_kupca = '" + lblKarticaKupca.Text + @"')) y  on x.broj_kartice = y.kartica_kupca
group by x.naziv, x.adresa";
                }
                else
                {
                    sql = @"select x.naziv,coalesce(sum(y.ukupno::numeric), 0) as ukupno, x.adresa
from (
select case when vrsta_korisnika = 1 then ime_tvrtke else concat(ime, ' ', prezime) end as naziv, broj_kartice, adresa from partners where broj_kartice = '" + lblKarticaKupca.Text + @"'
) x
left join (select * from racuni where racuni.datum_racuna >=
(select coalesce(max(datum_resetiranja), '1990-01-03 00:00:00') as datum
from reset_kartica_kupca
where kartica_kupca = '" + lblKarticaKupca.Text + @"' and cast(datum_resetiranja as date) between '" + dtpOd.Value.ToString("yyyy-MM-dd") + @"' and '" + dtpDo.Value.ToString("yyyy-MM-dd") + @"')) y  on x.broj_kartice = y.kartica_kupca
where cast(y.datum_racuna as date) between '" + dtpOd.Value.ToString("yyyy-MM-dd") + @"' and '" + dtpDo.Value.ToString("yyyy-MM-dd") + @"'
group by x.naziv, x.adresa";
                }

                DataTable dtData = classSQL.select(sql, "partners").Tables[0];

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    drUser = dtData.Rows[0];
                    lblNaziv.Text = drUser["naziv"].ToString();
                    lblAdresa.Text = drUser["adresa"].ToString();
                    decimal ukpbod = 0;
                    decimal.TryParse(drUser["ukupno"].ToString(), out ukpbod);
                    lblUkupno.Text = "Ukupno: " + ukpbod.ToString("0.00");
                }
                else
                {
                    lblNaziv.Text = "";
                    lblAdresa.Text = "";
                    lblUkupno.Text = "";
                    lblKarticaKupca.Text = "";
                    dgvData.DataSource = null;
                    dgvData.Rows.Clear();
                    return;
                }

                if (chkAll.Checked)
                {
                    sql = @"select  ROW_NUMBER() OVER(ORDER BY x.[Datum računa]) AS [Rb.], *
from (
select d.ime_ducana as [Poslovnica], b.ime_blagajne as [Naplatni uređaj], r.broj_racuna as [Broj računa], r.datum_racuna as [Datum računa], r.ukupno::numeric as [Iznos]
from racuni r
left join ducan d on r.id_ducan = d.id_ducan
left join blagajna b on r.id_kasa = b.id_blagajna
where r.kartica_kupca = '" + lblKarticaKupca.Text + @"'
union
select '-' as [Poslovnica], '-' as [Naplatni uređaj], 'RESET' as [Broj računa], rkk.datum_resetiranja as [Datum računa], 0 as [Iznos]
from reset_kartica_kupca rkk
where rkk.kartica_kupca = '" + lblKarticaKupca.Text + @"'
) x;";
                }
                else
                {
                    sql = @"select  ROW_NUMBER() OVER(ORDER BY x.[Datum računa]) AS [Rb.], *
from (
select d.ime_ducana as [Poslovnica], b.ime_blagajne as [Naplatni uređaj], r.broj_racuna as [Broj računa], r.datum_racuna as [Datum računa], r.ukupno::numeric as [Iznos]
from racuni r
left join ducan d on r.id_ducan = d.id_ducan
left join blagajna b on r.id_kasa = b.id_blagajna
where r.kartica_kupca = '" + lblKarticaKupca.Text + @"'
and cast(r.datum_racuna as date) between '" + dtpOd.Value.ToString("yyyy-MM-dd") + @"' and '" + dtpDo.Value.ToString("yyyy-MM-dd") + @"'
union
select '-' as [Poslovnica], '-' as [Naplatni uređaj], 'RESET' as [Broj računa], rkk.datum_resetiranja as [Datum računa], 0 as [Iznos]
from reset_kartica_kupca rkk
where rkk.kartica_kupca = '" + lblKarticaKupca.Text + @"'
and cast(rkk.datum_resetiranja as date) between '" + dtpOd.Value.ToString("yyyy-MM-dd") + @"' and '" + dtpDo.Value.ToString("yyyy-MM-dd") + @"'
) x;";
                }

                dtData = classSQL.select(sql, "karticakupci_racuni").Tables[0];

                dgvData.DataSource = dtData;
                dgvData.Columns[0].Width = 80;
                dgvData.Columns[1].Width = 100;
                dgvData.Columns[2].Width = 100;
                dgvData.Columns[3].Width = 100;
                //dgvData.Columns[4].Width = 80;
                dgvData.Columns[5].Width = 150;
            }
            if (!tmTimer1.Enabled)
            {
                tmTimer1.Enabled = true;
                tmTimer1.Start();
            }
        }

        private void dtpOd_ValueChanged(object sender, EventArgs e)
        {
            chkAll.Checked = false;
            getData();
        }

        private void dtpDo_ValueChanged(object sender, EventArgs e)
        {
            chkAll.Checked = false;
            getData();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            dtpOd.Enabled = !chkAll.Checked;
            dtpDo.Enabled = !chkAll.Checked;

            getData();
        }

        private void dtpOd_Enter(object sender, EventArgs e)
        {
            tmTimer1.Stop();
            tmTimer1.Enabled = false;
        }

        private void dtpDo_Enter(object sender, EventArgs e)
        {
            tmTimer1.Stop();
            tmTimer1.Enabled = false;
        }

        private void chkAll_Enter(object sender, EventArgs e)
        {
            tmTimer1.Stop();
            tmTimer1.Enabled = false;
        }

        private void btnDodajRacune_Click(object sender, EventArgs e)
        {
            //try {
            //    int boolInteger = 0;
            //    if (lblKarticaKupca.Text.Length == 10 && int.TryParse(lblKarticaKupca.Text, out boolInteger)) {
            //        frmDodajRacunKartica frm = new frmDodajRacunKartica();
            //        frm.karticaKupca = lblKarticaKupca.Text;
            //        frm.frmKartica = (frmKarticaKupca)this.FindForm();
            //        frm.ShowDialog();

            //    } else {
            //        MessageBox.Show("Kartica nije odabrana.");
            //    }
            //} catch (Exception ex) {
            //    MessageBox.Show(ex.Message);
            //}
            //tmTimer1.Enabled = true;
            //tmTimer1.Start();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnResetKartice_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblKarticaKupca.Text != "")
                {
                    if (MessageBox.Show("Želite kreirati reset kartice za odabranog partnera?\nReset kartice nije moguče obrisati", "Reset kartice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    {
                        string sql = string.Format(@"insert into reset_kartica_kupca (kartica_kupca, datum_resetiranja) values
('{0}',
'{1}');", lblKarticaKupca.Text, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        classSQL.insert(sql);

                        MessageBox.Show("Reset kartice uspjesno kreiran. :)");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}