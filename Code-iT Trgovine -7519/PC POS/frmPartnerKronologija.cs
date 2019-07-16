using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmPartnerKronologija : Form
    {
        public frmPartnerKronologija()
        {
            InitializeComponent();
        }

        public string id_pr { get; set; }
        private DataTable DTtvrtka = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];

        private void frmPartnerKronologija_Load(object sender, EventArgs e)
        {
            PopuniPodatke();
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            if (id_pr != null)
                txtFilter.Text = id_pr;
        }

        private void PopuniPodatke()
        {
            string filter = "";
            if (txtFilter.Text.Length > 0)
            {
                filter = " AND partner_kronologija.sifra ~* '" + txtFilter.Text + "' OR partner_kronologija.naziv ~*'" + txtFilter.Text + "' OR partner_kronologija.opis ~* '" + txtFilter.Text + "'";
            }

            DataTable DT = classSQL.select("SELECT partner_kronologija.*,zaposlenici.ime,zaposlenici.prezime FROM partner_kronologija" +
                " LEFT JOIN zaposlenici ON zaposlenici.id_zaposlenik=partner_kronologija.id_zaposlenik" +
                " WHERE opis<>'' " + filter + " ORDER BY datum DESC LIMIT 500", "partner_kronologija").Tables[0];
            dgv.Rows.Clear();
            foreach (DataRow r in DT.Rows)
            {
                DateTime datum;
                DateTime.TryParse(r["datum"].ToString(), out datum);
                dgv.Rows.Add(r["sifra"].ToString(), r["naziv"].ToString(), r["opis"].ToString(), datum, r["ime"].ToString() + " " + r["prezime"].ToString(), r["id"].ToString());
            }
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            frmPartnerKronologijaDodaj dod = new frmPartnerKronologijaDodaj();
            dod.ShowDialog();
            PopuniPodatke();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            PopuniPodatke();
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell.ColumnIndex == 2)
            {
                try
                {
                    string sql = "UPDATE partner_kronologija SET opis='" + dgv.Rows[e.RowIndex].Cells["opis"].FormattedValue.ToString() + "' WHERE id='" + dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString() + "'";
                    classSQL.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void frmPartnerKronologija_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnDodaj.Select();
        }

        private void PopuniPodatkeDugovanja(string partner)
        {
            string uvijet_partner = "";
            string uvijet_datum = "";

            if (partner != "")
            {
                uvijet_partner = " AND SaldaKonti.id_partner='" + partner + "' AND partners.id_partner='" + partner + "'";
            }

            string sql = " SELECT SUM(SaldaKonti.otvoreno) AS otvoreno,SUM(SaldaKonti.potrazuje) AS potrazuje, " +
                " SUM((SELECT COALESCE(SUM(salda_konti.uplaceno),0) AS uplaceno FROM salda_konti WHERE salda_konti.broj_dokumenta=SaldaKonti.broj AND SaldaKonti.dokumenat=salda_konti.dokumenat AND SaldaKonti.id_ducan=salda_konti.id_ducan AND extract(year from SaldaKonti.datum)=salda_konti.godina AND SaldaKonti.id_kasa=salda_konti.id_kasa)) AS uplaceno, " +
                " SUM((SELECT COALESCE(SUM(salda_konti.isplaceno),0) AS isplaceno FROM salda_konti WHERE salda_konti.broj_dokumenta=SaldaKonti.broj AND SaldaKonti.dokumenat=salda_konti.dokumenat AND SaldaKonti.id_ducan=salda_konti.id_ducan AND extract(year from SaldaKonti.datum)=salda_konti.godina AND SaldaKonti.id_kasa=salda_konti.id_kasa)) AS isplaceno " +
                " FROM " +
                " (" +
                " SELECT broj_fakture as broj,'FAKTURA' AS dokumenat,date AS datum,datedvo AS datum2,fakture.id_odrediste AS id_partner,CAST(fakture.ukupno AS NUMERIC) AS otvoreno,CAST('0' AS numeric) AS potrazuje,fakture.id_ducan,fakture.id_kasa FROM fakture  " +
                " UNION ALL " +
                " SELECT broj as broj, 'FAKTURA BEZ ROBE' AS dokumenat,datum AS datum,datum_dvo AS datum2,ifb.odrediste AS id_partner, ifb.ukupno AS otvoreno,CAST('0' AS numeric) AS potrazuje,'0' as id_ducan,'0' AS id_kasa FROM ifb " +
                " UNION ALL " +
                " SELECT broj as broj, 'ULAZNA FAKTURA' AS dokumenat,datum_izvrsenja AS datum,datum_izvrsenja AS datum2,ulazna_faktura.primatelj_sifra AS id_partner, CAST('0' AS NUMERIC) AS otvoreno, ulazna_faktura.iznos AS potrazuje,'0' as id_ducan,'0' AS id_kasa FROM ulazna_faktura " +
                " UNION ALL " +
                " SELECT broj as broj, 'KALKULACIJA' AS dokumenat,datum AS datum,datum AS datum2,kalkulacija.id_partner, CAST('0' AS NUMERIC) AS otvoreno, CAST(kalkulacija.fakturni_iznos AS NUMERIC) AS potrazuje,'0' as id_ducan,'0' AS id_kasa FROM kalkulacija " +
                " ) AS SaldaKonti " +
                "" +
                " LEFT JOIN partners ON partners.id_partner=SaldaKonti.id_partner " +
                " WHERE partners.id_partner IS NOT NULL " + uvijet_datum + uvijet_partner + "; ";

            DataTable DT = classSQL.select(sql, "dugovanja").Tables[0];

            decimal otvoreno, uplaceno, potrazuje, isplaceno;
            decimal.TryParse(DT.Rows[0]["otvoreno"].ToString(), out otvoreno);
            decimal.TryParse(DT.Rows[0]["uplaceno"].ToString(), out uplaceno);
            decimal.TryParse(DT.Rows[0]["potrazuje"].ToString(), out potrazuje);
            decimal.TryParse(DT.Rows[0]["isplaceno"].ToString(), out isplaceno);

            lblDuguje.Text = "DUGUJE: " + Math.Round(otvoreno - uplaceno, 3).ToString("N2");
            lblPotrazuje.Text = "POTRAŽUJE: " + Math.Round(potrazuje - isplaceno, 3).ToString("N2");

            int SumBroj = 0;
            string Sifra = dgv.CurrentRow.Cells["sifra"].FormattedValue.ToString();
            foreach (DataGridViewRow r in dgv.Rows)
            {
                if (Sifra == r.Cells["sifra"].FormattedValue.ToString())
                {
                    SumBroj++;
                }
            }

            lblBrojDogadaja.Text = "Broj događaja: " + SumBroj.ToString();

            if (DTtvrtka.Rows[0]["oib"].ToString() == Class.Postavke.OIB_PC1)
            {
                DataTable DTodr = classSQL.select("SELECT * FROM partners_odrzavanje WHERE id_partner='" + Sifra + "'" +
                    " AND (odrzavanje*odrzavanje_kol)>0", "partners_odrzavanje").Tables[0];

                if (DTodr.Rows.Count > 0)
                    lblOdr.Text = "Održavanje: DA";
                else
                    lblOdr.Text = "Održavanje: NE";
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            PopuniPodatkeDugovanja(dgv.Rows[e.RowIndex].Cells["sifra"].FormattedValue.ToString());
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int id = 0;
                if (dgv.Rows.Count > 0 && int.TryParse(dgv.Rows[dgv.CurrentCell.RowIndex].Cells["id"].Value.ToString(), out id) && id > 0)
                {
                    frmPartnerKronologijaDodaj dod = new frmPartnerKronologijaDodaj();
                    dod.id = id;
                    dod.ShowDialog();
                    PopuniPodatke();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}