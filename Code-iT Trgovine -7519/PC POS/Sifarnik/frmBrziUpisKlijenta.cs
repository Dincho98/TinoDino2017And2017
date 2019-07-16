using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Sifarnik
{
    public partial class frmBrziUpisKlijenta : Form
    {
        public frmBrziUpisKlijenta()
        {
            InitializeComponent();
        }

        public frmParagonac MainForm { get; set; }

        private void frmBrziUpisKlijenta_Load(object sender, EventArgs e)
        {
            //CB grad
            DataSet DSgrad = classSQL.select("SELECT * FROM grad WHERE drzava = '60' ORDER BY grad ", "grad");
            cbGrad.DataSource = DSgrad.Tables[0];
            cbGrad.DisplayMember = "grad";
            cbGrad.ValueMember = "id_grad";
            cbGrad.SelectedValue = "2806";

            txtImeKlijenta.Select();
        }

        private string brojPartner()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(id_partner) FROM partners", "partners").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO partners (ime_tvrtke,id_grad,adresa,oib,id_djelatnost,id_zemlja,aktivan,id_zupanija,oib_polje,primanje_letaka,vrsta_korisnika,id_partner) VALUES (" +
                "'" + txtImeKlijenta.Text + "'," +
                "'" + cbGrad.SelectedValue + "'," +
                "'" + txtAdresa.Text + "'," +
                "'" + txtOIB.Text + "'," +
                "'5'," +
                "'60'," +
                "'1'," +
                "'8'," +
                "'OIB'," +
                "'1'," +
                "'1'," +
                "'" + brojPartner() + "'" +
                ")";

            classSQL.insert(sql);

            DataTable DT = classSQL.select("SELECT MAX(id_partner) FROM partners", "partners").Tables[0];

            if (DT.Rows.Count > 0)
            {
                Properties.Settings.Default.id_partner = DT.Rows[0][0].ToString();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmNoviGrad novigr = new frmNoviGrad();
            novigr.ShowDialog();
        }
    }
}