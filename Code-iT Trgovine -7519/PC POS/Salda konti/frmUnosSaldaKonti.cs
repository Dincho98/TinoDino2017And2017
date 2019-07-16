using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Salda_konti
{
    public partial class frmUnosSaldaKonti : Form
    {
        public frmUnosSaldaKonti()
        {
            InitializeComponent();
        }

        public string _broj { get; set; }
        public string _godina { get; set; }
        public string _dokumenat { get; set; }
        public decimal _iznos { get; set; }
        public string _id_skladiste { get; set; }
        public string _id_partner { get; set; }
        public string _id_ducan { get; set; }
        public string _id_kasa { get; set; }

        private void frmUnosSaldaKonti_Load(object sender, EventArgs e)
        {
            string sql = "";

            if (_dokumenat == "FAKTURA")
            {
                sql = "SELECT * FROM salda_konti WHERE broj_dokumenta='" + _broj + "' AND godina='" + _godina + "'" +
                    " AND id_ducan='" + _id_ducan + "' AND id_kasa='" + _id_kasa + "' AND dokumenat='" + _dokumenat + "' ";
            }
            else if (_dokumenat == "ULAZNA FAKTURA")
            {
                sql = "SELECT * FROM salda_konti WHERE broj_dokumenta='" + _broj + "' AND godina='" + _godina + "'" +
                    "  AND dokumenat='" + _dokumenat + "' ";
            }
            else if (_dokumenat == "FAKTURA BEZ ROBE")
            {
                sql = "SELECT * FROM salda_konti WHERE broj_dokumenta='" + _broj + "' AND godina='" + _godina + "'" +
                    "  AND dokumenat='" + _dokumenat + "' ";
            }
            else if (_dokumenat == "KALKULACIJA")
            {
                sql = "SELECT * FROM salda_konti WHERE broj_dokumenta='" + _broj + "' AND godina='" + _godina + "' AND id_skladiste='" + _id_skladiste + "'" +
                    "  AND dokumenat='" + _dokumenat + "' ";
            }

            DataTable DT = classSQL.select(sql, "salda_konti").Tables[0];

            decimal du = 0, dp = 0;
            foreach (DataRow r in DT.Rows)
            {
                if (_dokumenat == "ULAZNA FAKTURA" || _dokumenat == "KALKULACIJA")
                {
                    dgv.Rows.Add(r["isplaceno"].ToString(), r["datum"].ToString(), r["id"].ToString(), r["napomena"].ToString());
                    decimal.TryParse(r["isplaceno"].ToString(), out dp);
                }
                else
                {
                    dgv.Rows.Add(r["uplaceno"].ToString(), r["datum"].ToString(), r["id"].ToString(), r["napomena"].ToString());
                    decimal.TryParse(r["uplaceno"].ToString(), out dp);
                }

                du += dp;
            }

            txtUplata.Text = Math.Round((_iznos - du), 3).ToString("#0.00");
            label7.Text = "Po ovoj fakturi ukupno je uplaćeno: " + du + " kn";
            label2.Text = "Broj dokumenta: " + _broj;
            label1.Text = "Dokumenat: " + _dokumenat;

            if (_id_skladiste == "")
            {
                _id_skladiste = "0";
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            decimal upl;
            decimal.TryParse(txtUplata.Text, out upl);

            if ((UzmiVrijednostiIzDgv() + upl) > _iznos)
            {
                MessageBox.Show("GREŠKA!!!\r\nUpisali ste veći iznos od ukupnog zbroja fakture ili nekog drugog dokumenta.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_dokumenat == "ULAZNA FAKTURA" || _dokumenat == "KALKULACIJA")
            {
                SpremiSaldaKonti(0, upl);
            }
            else
            {
                SpremiSaldaKonti(upl, 0);
            }
            this.Close();
        }

        private void chbPlacenoSve_CheckedChanged(object sender, EventArgs e)
        {
            decimal ostatak = _iznos - UzmiVrijednostiIzDgv();

            if (_dokumenat == "ULAZNA FAKTURA" || _dokumenat == "KALKULACIJA")
            {
                SpremiSaldaKonti(0, ostatak);
            }
            else
            {
                SpremiSaldaKonti(ostatak, 0);
            }

            this.Close();
        }

        private void SpremiSaldaKonti(decimal uplaceno, decimal izplaceno)
        {
            if ((uplaceno + izplaceno) != 0)
            {
                if (_id_kasa == null || _id_kasa == "")
                    _id_kasa = "0";

                if (_id_ducan == null || _id_ducan == "")
                    _id_ducan = "0";

                string sql = "INSERT INTO salda_konti (broj_dokumenta,id_partner,dokumenat,id_skladiste,datum,godina,id_izradio,uplaceno,isplaceno,napomena,id_kasa,id_ducan) VALUES (" +
                        "'" + _broj + "'," +
                        "'" + _id_partner + "'," +
                        "'" + _dokumenat + "'," +
                        "'" + _id_skladiste + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                        "'" + _godina + "'," +
                        "'" + Properties.Settings.Default.id_zaposlenik + "'," +
                        "'" + Math.Round(uplaceno, 3).ToString("#0.00").Replace(",", ".") + "'," +
                        "'" + Math.Round(izplaceno, 3).ToString("#0.00").Replace(",", ".") + "'," +
                        "'" + rtbNapomena.Text + "','" + _id_kasa + "','" + _id_ducan + "'" +
                    ")";

                classSQL.insert(sql);
            }
        }

        private decimal UzmiVrijednostiIzDgv()
        {
            decimal p = 0, u = 0;
            foreach (DataGridViewRow r in dgv.Rows)
            {
                decimal.TryParse(r.Cells["iznos"].FormattedValue.ToString(), out p);
                u += p;
            }
            return u;
        }

        private void btnObrisiOznaceni_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count > 0)
            {
                classSQL.delete("DELETE FROM salda_konti WHERE id='" + dgv.CurrentRow.Cells["id"].FormattedValue.ToString() + "'");
                dgv.Rows.RemoveAt(dgv.CurrentRow.Index);
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rtbNapomena.Text = dgv.CurrentRow.Cells["napomena"].FormattedValue.ToString();
        }
    }
}