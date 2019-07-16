using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Report.BlagajnickiIzvjestaj
{
    public partial class frmBlagajnickiIzvjestaj : Form
    {
        public frmBlagajnickiIzvjestaj()
        {
            InitializeComponent();
        }

        private string Dokumenat = "";
        private DataTable DTblagajnicki_izvjestaj = new DataTable();

        private void frmBlagajnickiIzvjestaj_Load(object sender, EventArgs e)
        {
            DateTime DT;
            DateTime.TryParse(DateTime.Now.AddMonths(-2).ToString("yyyy-MM-01 0:00:01"), out DT);
            odDatuma.Value = DT;

            EnableDisable(false);
            PopuniDataGrid();
            btnUtrzak.PerformClick();
        }

        private void btnUtrzak_Click(object sender, EventArgs e)
        {
            Dokumenat = "UPLATA UTRŠKA";
            lblIznos.Text = Dokumenat;
            this.Text = "Blagajnički izvještaj - " + Dokumenat;
            EnableDisable(true);
        }

        private void btnPolog_Click(object sender, EventArgs e)
        {
            Dokumenat = "POLOG ZAJMA";
            lblIznos.Text = Dokumenat;
            this.Text = "Blagajnički izvještaj - " + Dokumenat;
            EnableDisable(true);
        }

        private void btnIsplataGotRn_Click(object sender, EventArgs e)
        {
            Dokumenat = "ISPLATA PO GOT RN";
            lblIznos.Text = Dokumenat;
            this.Text = "Blagajnički izvještaj - " + Dokumenat;
            EnableDisable(true);
        }

        private void btnPocetnoGore_Click(object sender, EventArgs e)
        {
            Dokumenat = "POČETNO STANJE";
            lblIznos.Text = Dokumenat;
            this.Text = "Blagajnički izvještaj - " + Dokumenat;
            EnableDisable(true);
        }

        private void btnPozajmnica_Click(object sender, EventArgs e)
        {
            Dokumenat = "POZAJMNICA";
            lblIznos.Text = Dokumenat;
            this.Text = "Blagajnički izvještaj - " + Dokumenat;
            EnableDisable(true);
        }

        private void btnPovratPozajmnice_Click(object sender, EventArgs e)
        {
            Dokumenat = "POVRAT POZAJMNICE";
            lblIznos.Text = Dokumenat;
            this.Text = "Blagajnički izvještaj - " + Dokumenat;
            EnableDisable(true);
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            decimal u;
            decimal.TryParse(txtIznos.Text, out u);
            string[] strArr = new string[] { "POČETNO STANJE", "POZAJMNICA" };
            string sql = "";
            if (strArr.Contains(Dokumenat))
            {
                sql =
                "INSERT INTO blagajnicki_izvjestaj (" +
                    "datum, dokumenat, oznaka_dokumenta, uplaceno, izdatak, novo, editirano";
                if (txtPartnerNaziv.Text.Length > 0)
                {
                    sql += ", id_partner"; //id_partner
                }

                sql += string.Format(@")
VALUES
(
    '{0}', '{1}', '{2}', '{3}', '0', '1', '0'",
                        dtDatumVrijeme.Value.ToString("yyyy-MM-dd H:mm:ss"),
                        Dokumenat,
                        txtOznaka.Text,
                        u.ToString().Replace(",", ".")); //editirano

                if (txtPartnerNaziv.Text.Length > 0)
                {
                    sql += string.Format(", '{0}'", Properties.Settings.Default.id_partner); //id_partner
                }

                sql += ")";

                classSQL.insert(sql);
            }
            else
            {
                sql = "INSERT INTO blagajnicki_izvjestaj (" +
                    "datum,dokumenat,oznaka_dokumenta,uplaceno,izdatak,novo,editirano";
                if (txtPartnerNaziv.Text.Length > 0)
                {
                    sql += ", id_partner"; //id_partner
                }

                sql += string.Format(@")
VALUES
( '{0}', '{1}', '{2}', '0', '{3}', '1', '0'",
                    dtDatumVrijeme.Value.ToString("yyyy-MM-dd H:mm:ss"),
                    Dokumenat,
                    txtOznaka.Text,
                    u.ToString().Replace(",", "."));

                if (txtPartnerNaziv.Text.Length > 0)
                {
                    sql += string.Format(", '{0}'", Properties.Settings.Default.id_partner); //id_partner
                }
                sql += ")";

                classSQL.insert(sql);
            }

            EnableDisable(false);

            MessageBox.Show("Spremljeno", "Spremljeno");
            PopuniDataGrid();
        }

        private void EnableDisable(bool b)
        {
            txtIznos.Text = "";
            txtOznaka.Text = "";
            txtPartnerNaziv.Text = "";
            Properties.Settings.Default.id_partner = null;

            if (b)
                txtIznos.Focus();
        }

        public void PopuniDataGrid()
        {
            string filter = string.Format(@"WHERE cast(datum as date) >= '{0}' AND cast(datum as date) <= '{1}'",
                odDatuma.Value.ToString("yyyy-MM-dd"),
                doDatuma.Value.ToString("yyyy-MM-dd"));

            string sql = string.Format(@"SELECT * FROM
(
    SELECT id, datum, dokumenat,
    CASE WHEN oznaka_dokumenta is null OR length(oznaka_dokumenta) = 0
    THEN partners.ime_tvrtke
    ELSE concat(oznaka_dokumenta, ' - ', partners.ime_tvrtke)
    END AS oznaka_dokumenta,
    CASE WHEN dokumenat in ('POČETNO STANJE', 'POZAJMNICA')
    THEN uplaceno
    ELSE '0' END as uplaceno, izdatak
    FROM blagajnicki_izvjestaj
    LEFT JOIN partners ON blagajnicki_izvjestaj.id_partner = partners.id_partner
    WHERE dokumenat <> 'PROMET BLAGAJNE'

    UNION ALL

    SELECT '-1' as id,date_trunc('day', datum_racuna) as datum,'PROMET BLAGAJNE' as dokumenat,
    concat(MIN(CAST(racuni.broj_racuna AS INT)), '-', MAX(CAST(racuni.broj_racuna AS INT))) AS oznaka_dokumenta,

    SUM((CAST(racun_stavke.mpc AS NUMERIC) - (CAST(racun_stavke.mpc AS NUMERIC) * CAST(REPLACE(racun_stavke.rabat,',','.') AS NUMERIC)/100) ) * CAST(REPLACE(racun_stavke.kolicina,',','.') AS NUMERIC) ) AS uplaceno, '0' as izdatak

    FROM racuni
    LEFT JOIN racun_stavke ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa
    WHERE (racuni.nacin_placanja='G' OR racuni.nacin_placanja is null)
    GROUP BY date_trunc('day', datum_racuna)
)
bm
{0}
ORDER BY datum ASC;",
filter);

            DTblagajnicki_izvjestaj = classSQL.select(sql, "blagajnicki_izvjestaj").Tables[0];

            DateTime dat;
            decimal u, i, uuk = 0, iuk = 0;

            if (dgv.Rows.Count > 0)
                dgv.Rows.Clear();

            foreach (DataRow r in DTblagajnicki_izvjestaj.Rows)
            {
                decimal.TryParse(r["uplaceno"].ToString(), out u);
                decimal.TryParse(r["izdatak"].ToString(), out i);
                DateTime.TryParse(r["datum"].ToString(), out dat);

                uuk = u + uuk;
                iuk = i + iuk;

                dgv.Rows.Add(dat,
                    r["dokumenat"].ToString(),
                    r["oznaka_dokumenta"].ToString(),
                    Math.Round(u, 3).ToString("#0.00"),
                    Math.Round(i, 3).ToString("#0.00"),
                    r["id"].ToString());
            }

            lblIzdatak.Text = "IZDATAK: " + iuk.ToString("N2") + " kn";
            lblUplaceno.Text = "UKUPNO: " + uuk.ToString("N2") + " kn";
        }

        private void btnUcitaj_Click(object sender, EventArgs e)
        {
            PopuniDataGrid();
        }

        private void dgv_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentRow.Cells["id"].FormattedValue.ToString() == "")
                return;
            if (dgv.CurrentRow.Cells["dokument"].FormattedValue.ToString() == "PROMET BLAGAJNE")
            {
                MessageBox.Show("Zabranjeno polje!!!");
                dgv.CurrentRow.Cells["dokument"].Value = "PROMET BLAGAJNE";
                return;
            }

            if (dgv.CurrentCell.ColumnIndex == 4)
            {
                try
                {
                    string sql = string.Format(@"UPDATE blagajnicki_izvjestaj
SET
    izdatak = '{0}',
    editirano = '1'
WHERE id = '{1}';",
dgv.Rows[e.RowIndex].Cells["izdatak"].FormattedValue.ToString().Replace(",", "."),
dgv.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString());
                    classSQL.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            frmBlagajnickiIzvjestajReport blai = new frmBlagajnickiIzvjestajReport();
            blai.tdOdDatuma.Value = odDatuma.Value;
            blai.tdDoDatuma.Value = doDatuma.Value;
            blai.btnTrazi_Click(null, null);
            blai.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                frmPartnerTrazi partner = new frmPartnerTrazi();
                partner.ShowDialog();

                if (Properties.Settings.Default.id_partner != null && Properties.Settings.Default.id_partner.ToString().Length > 0)
                {
                    string sql = string.Format("select * from partners where id_partner = '{0}';", Properties.Settings.Default.id_partner);
                    txtPartnerNaziv.Text = classSQL.select(sql, "partners").Tables[0].Rows[0]["ime_tvrtke"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}