using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmKnjiznoOdobrenje : Form
    {
        private string sql = "";

        private DataTable DTpostavke = new DataTable();

        public frmKnjiznoOdobrenje()
        {
            InitializeComponent();
        }

        private void frmKnjiznoOdobrenje_Load(object sender, EventArgs e)
        {
            try
            {
                DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

                sql = "DROP TABLE IF EXISTS tempUplate; " +
                        "CREATE TEMP TABLE tempUplate AS " +
                        "SELECT f.broj_fakture, " +
                        "sum(uplaceno) as uplaceno " +
                        "from fakture f " +
                        "left join salda_konti sk on f.broj_fakture = sk.broj_dokumenta " +
                                        "and f.id_ducan = sk.id_ducan " +
                                        "and f.id_kasa = sk.id_kasa " +
                                        "and f.godina_fakture::numeric = sk.godina " +
                        "group by f.broj_fakture " +
                        "order by f.broj_fakture; " +

                        "select distinct p.id_partner, p.ime_tvrtke " +
                        "from fakture f " +
                        "left join tempUplate tu on f.broj_fakture = tu.broj_fakture " +
                        "left join partners p on f.id_odrediste = p.id_partner " +
                        "where coalesce(tu.uplaceno, 0) = 0 " +
                        "AND f.id_ducan = '" + DTpostavke.Rows[0]["default_ducan"] + "'" +
                        "and f.id_kasa = '" + DTpostavke.Rows[0]["naplatni_uredaj_faktura"] + "'" +
                        "and f.godina_fakture = '" + Util.Korisno.GodinaKojaSeKoristiUbazi.ToString() + "';";
                DataTable dt = classSQL.select(sql, "partners").Tables[0];
                cmbPartner.DisplayMember = "ime_tvrtke";
                cmbPartner.ValueMember = "id_partner";
                cmbPartner.DataSource = dt;

                if (cmbPartner.Items.Count > 0)
                {
                    cmbPartner_SelectionChangeCommitted(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbPartner_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                sql = "DROP TABLE IF EXISTS tempUplate; " +
                        "CREATE TEMP TABLE tempUplate AS " +
                        "SELECT f.broj_fakture, " +
                        "SUM(uplaceno) AS uplaceno " +
                        "FROM fakture f " +
                        "LEFT JOIN salda_konti sk ON f.broj_fakture = sk.broj_dokumenta " +
                                        "AND f.id_ducan = sk.id_ducan " +
                                        "AND f.id_kasa = sk.id_kasa " +
                                        "AND f.godina_fakture::numeric = sk.godina " +
                        "GROUP BY f.broj_fakture " +
                        "ORDER BY f.broj_fakture; " +

                        "SELECT f.broj_fakture, f.date, v.ime_valute, p.ime_tvrtke, f.ukupno, f.storno, d.ime_ducana, f.id_ducan as fid_ducan, f.id_kasa as fid_kasa, " +
                        "'" + DTpostavke.Rows[0]["default_ducan"] + "' as id_ducan, " +
                        "'" + DTpostavke.Rows[0]["naplatni_uredaj_faktura"] + "' as id_kasa " +
                        "FROM fakture f " +
                        "LEFT JOIN tempUplate tu ON f.broj_fakture = tu.broj_fakture " +
                        "LEFT JOIN partners p ON f.id_odrediste = p.id_partner " +
                        "LEFT JOIN valute v on f.id_valuta = v.id_valuta " +
                        "LEFT JOIN ducan d on f.id_ducan = d.id_ducan " +
                        "WHERE COALESCE(tu.uplaceno, 0) = 0 AND f.id_odrediste = '" + cmbPartner.SelectedValue + "'" +
                        "AND f.id_ducan = '" + DTpostavke.Rows[0]["default_ducan"] + "'" +
                        "and f.id_kasa = '" + DTpostavke.Rows[0]["naplatni_uredaj_faktura"] + "'" +
                        "AND f.godina_fakture = '" + Util.Korisno.GodinaKojaSeKoristiUbazi.ToString() + "';";
                dgvFakture.DataSource = classSQL.select(sql, "fakture").Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Želite dodati fakture u knjižno odobrenje?", "Knjižno odobrenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                decimal porez = 0;
                DateTime dtNow = DateTime.Now;
                int br = Convert.ToInt32(classSQL.select("select coalesce(max(broj_odobrenje), 0) zbroj 1 as br from knjizno_odobrenje where id_ducan = '" + DTpostavke.Rows[0]["default_ducan"] + "' and id_kasa = '" + DTpostavke.Rows[0]["naplatni_uredaj_faktura"] + "'", "knjizno_odobrenje").Tables[0].Rows[0]["br"]);
                if (txtPorez.Text.Length > 0 && Decimal.TryParse(txtPorez.Text, out porez))
                {
                    foreach (DataGridViewRow dr in dgvFakture.Rows)
                    {
                        if (Convert.ToBoolean(dr.Cells["oznaci"].Value))
                        {
                            sql = "INSERT INTO knjizno_odobrenje (broj_odobrenje, datum, broj_fakture, id_ducan, id_kasa, id_ducan_faktura, id_kasa_faktura, id_izradio, porez_odobrenja) VALUES ( " +
                                "'" + br + "', " +
                                "'" + dtNow.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                                "'" + dr.Cells["broj"].Value + "', " +
                                "'" + dr.Cells["id_ducan"].Value + "', " +
                                "'" + dr.Cells["id_kasa"].Value + "', " +
                                "'" + dr.Cells["fid_ducan"].Value + "', " +
                                "'" + dr.Cells["fid_kasa"].Value + "', " +
                                "'" + Properties.Settings.Default.id_zaposlenik + "', " +
                                "'" + porez.ToString().Replace(',', '.') + "');";
                            classSQL.insert(sql);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnKnjiznoOdobrenjeAll_Click(object sender, EventArgs e)
        {
            try
            {
                Robno.frmKnjiznoOdobrenjeAll f = new Robno.frmKnjiznoOdobrenjeAll();
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}