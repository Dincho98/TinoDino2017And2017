using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class podloga : Form
    {
        public podloga()
        {
            InitializeComponent();
        }

        private int racun_bool;

        private void frmPodaci_tvrtka_Load(object sender, EventArgs e)
        {
            DataTable DTSK = new DataTable("nacin");
            DTSK.Columns.Add("id", typeof(string));
            DTSK.Columns.Add("naziv", typeof(string));
            DTSK.Rows.Add("R1", "Račun R1");
            DTSK.Rows.Add("R2", "Račun R2");

            cbR1.DataSource = DTSK;
            cbR1.DisplayMember = "naziv";
            cbR1.ValueMember = "id";

            SetValue();

            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void SetValue()
        {
            DataSet DSgrad = classSQL.select("SELECT * FROM grad ORDER BY grad", "grad");
            cbGrad.DataSource = DSgrad.Tables[0];
            cbGrad.DisplayMember = "grad";
            cbGrad.ValueMember = "id_grad";

            DataSet DSgrad1 = classSQL.select("SELECT * FROM grad ORDER BY grad", "grad");
            cbPoslovnicaGrad.DataSource = DSgrad1.Tables[0];
            cbPoslovnicaGrad.DisplayMember = "grad";
            cbPoslovnicaGrad.ValueMember = "id_grad";

            DataTable DTpodaci = classSQL.select_settings("SELECT * FROM podaci_tvrtka WHERE id='1'", "podaci_tvrtka").Tables[0];
            txtImeTvrtke.Text = DTpodaci.Rows[0]["ime_tvrtke"].ToString();
            txtSkraceno.Text = DTpodaci.Rows[0]["skraceno_ime"].ToString();
            txtRacun.Text = DTpodaci.Rows[0]["text_racun"].ToString();
            txtRacun2.Text = DTpodaci.Rows[0]["text_racun2"].ToString();

            if (DTpodaci.Rows[0]["racun_bool"].ToString() == "1")
            {
                text1.Checked = true;
                text2.Checked = false;
            }
            else
            {
                text1.Checked = false;
                text2.Checked = true;
            }
            txtOib.Text = DTpodaci.Rows[0]["oib"].ToString();
            txtTelefon.Text = DTpodaci.Rows[0]["tel"].ToString();
            txtFax.Text = DTpodaci.Rows[0]["fax"].ToString();
            txtMobitel.Text = DTpodaci.Rows[0]["mob"].ToString();
            txtAdresa.Text = DTpodaci.Rows[0]["adresa"].ToString();
            txtVlasnik.Text = DTpodaci.Rows[0]["vl"].ToString();
            txtDirektor.Text = DTpodaci.Rows[0]["direktor"].ToString();
            richTextBoxDodatniPodaciHeader.Text = DTpodaci.Rows[0]["dodatniPodaciHeader"].ToString();
            txtZR.Text = DTpodaci.Rows[0]["zr"].ToString();
            cbGrad.SelectedValue = Convert.ToInt16(DTpodaci.Rows[0]["id_grad"].ToString());
            txtEmail.Text = DTpodaci.Rows[0]["email"].ToString();
            txtPoslovnicaAdresa.Text = DTpodaci.Rows[0]["poslovnica_adresa"].ToString();
            string sql1 = "SELECT id_grad FROM grad WHERE grad='" + DTpodaci.Rows[0]["poslovnica_grad"].ToString() + "'";
            DataTable poslovnica = classSQL.select(sql1, "grad").Tables[0];
            if (poslovnica.Rows.Count > 0)
            {
                cbPoslovnicaGrad.SelectedValue = poslovnica.Rows[0]["id_grad"].ToString();
            }
            else
            {
                cbPoslovnicaGrad.Text = DTpodaci.Rows[0]["poslovnica_grad"].ToString();
            }
            txtIBAN.Text = DTpodaci.Rows[0]["iban"].ToString();
            txtSWIFT.Text = DTpodaci.Rows[0]["swift"].ToString();
            txtNaslovFakture.Text = DTpodaci.Rows[0]["naziv_fakture"].ToString();
            txtpdv_br.Text = DTpodaci.Rows[0]["pdv_br"].ToString();
            txtposlovnica.Text = DTpodaci.Rows[0]["ime_poslovnice"].ToString();
            rtbKrajDokumenta.Text = DTpodaci.Rows[0]["text_bottom"].ToString();
            txtSifraDjelatnosti.Text = DTpodaci.Rows[0]["sifra_djelatnosti"].ToString();
            txtAdresaPrebivalista.Text = DTpodaci.Rows[0]["adresa_prebivalista"].ToString();
            cbR1.SelectedValue = DTpodaci.Rows[0]["r1"].ToString();
            richTextServis.Text = DTpodaci.Rows[0]["servis_text"].ToString();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void brnSpremi_Click(object sender, EventArgs e)
        {
            if (text1.Checked == true)
            {
                racun_bool = 1;
            }
            else
            {
                racun_bool = 0;
            }

            string sql = "UPDATE podaci_tvrtka SET " +
                " ime_tvrtke='" + txtImeTvrtke.Text + "'," +
                " skraceno_ime='" + txtSkraceno.Text + "'," +
                " oib='" + txtOib.Text + "'," +
                " tel='" + txtTelefon.Text + "'," +
                " fax='" + txtFax.Text + "'," +
                " mob='" + txtMobitel.Text + "'," +
                " adresa='" + txtAdresa.Text + "'," +
                " vl='" + txtVlasnik.Text + "'," +
                " direktor='" + txtDirektor.Text + "'," +
                " zr='" + txtZR.Text + "'," +
                " racun_bool='" + racun_bool.ToString() + "'," +
                " id_grad='" + cbGrad.SelectedValue + "'," +
                " email='" + txtEmail.Text + "'," +
                " text_bottom='" + rtbKrajDokumenta.Text + "'," +
                " text_racun='" + txtRacun.Text + "'," +
                " text_racun2='" + txtRacun2.Text + "'," +
                " poslovnica_adresa='" + txtPoslovnicaAdresa.Text + "'," +
                " poslovnica_grad='" + cbPoslovnicaGrad.Text + "'," +
                " iban='" + txtIBAN.Text + "'," +
                " swift='" + txtSWIFT.Text + "'," +
                " naziv_fakture='" + txtNaslovFakture.Text + "'," +
                " pdv_br='" + txtpdv_br.Text + "'," +
                " ime_poslovnice='" + txtposlovnica.Text + "'," +
                " dodatniPodaciHeader='" + richTextBoxDodatniPodaciHeader.Text + "'," +
                " sifra_djelatnosti ='" + txtSifraDjelatnosti.Text + "'," +
                " adresa_prebivalista ='" + txtAdresaPrebivalista.Text + "'," +
                " r1='" + cbR1.SelectedValue + "'," +
                " servis_text='" + richTextServis.Text + "'" +
                " WHERE id='1'";

            classSQL.Setings_Update(sql);

            string sql1 = "UPDATE podaci_tvrtka SET " +
                " ime_tvrtke='" + txtImeTvrtke.Text + "'," +
                " skraceno_ime='" + txtSkraceno.Text + "'," +
                " oib='" + txtOib.Text + "'," +
                " tel='" + txtTelefon.Text + "'," +
                " fax='" + txtFax.Text + "'," +
                " mob='" + txtMobitel.Text + "'," +
                " adresa='" + txtAdresa.Text + "'," +
                " vl='" + txtVlasnik.Text + "'," +
                " direktor='" + txtDirektor.Text + "'," +
                " zr='" + txtZR.Text + "'," +
                " racun_bool='" + racun_bool.ToString() + "'," +
                " id_grad='" + cbGrad.SelectedValue + "'," +
                " email='" + txtEmail.Text + "'," +
                " text_bottom='" + rtbKrajDokumenta.Text + "'," +
                " text_racun='" + txtRacun.Text + "'," +
                " text_racun2='" + txtRacun2.Text + "'," +
                " poslovnica_adresa='" + txtPoslovnicaAdresa.Text + "'," +
                " poslovnica_grad='" + cbPoslovnicaGrad.Text + "'," +
                " iban='" + txtIBAN.Text + "'," +
                " swift='" + txtSWIFT.Text + "'," +
                " naziv_fakture='" + txtNaslovFakture.Text + "'," +
                " pdv_br='" + txtpdv_br.Text + "'," +
                " ime_poslovnice='" + txtposlovnica.Text + "'," +
                " dodatniPodaciHeader='" + richTextBoxDodatniPodaciHeader.Text + "'," +
                " sifra_djelatnosti ='" + txtSifraDjelatnosti.Text + "'," +
                " adresa_prebivalista ='" + txtAdresaPrebivalista.Text + "'," +
                " r1='" + cbR1.SelectedValue + "'" +
                " WHERE id='1'";

            // classSQL.update(sql1);

            Util.Korisno.oibTvrtke = txtOib.Text;

            MessageBox.Show("Spremljeno.");
        }

        private void text1_CheckedChanged(object sender, EventArgs e)
        {
            if (text1.Checked == true)
            {
                text2.Checked = false;
            }
        }

        private void text2_CheckedChanged(object sender, EventArgs e)
        {
            if (text2.Checked == true)
            {
                text1.Checked = false;
            }
        }

        private void podloga_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Class.PodaciTvrtka.GetPodaciTvrtke();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}