using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Odrzavanja
{
    public partial class frmOdrzavanja : Form
    {
        public frmOdrzavanja()
        {
            InitializeComponent();
        }

        public string id_ducan { get; set; }
        public string id_kasa { get; set; }

        private void frmIzvozIzPrograma_Load(object sender, EventArgs e)
        {
            SetirajNaplatniPoslovnicuDefault();
            fill();
            Racunaj();
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private static DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private void SetirajNaplatniPoslovnicuDefault()
        {
            try
            {
                id_kasa = DTpostavke.Rows[0]["naplatni_uredaj_faktura"].ToString();
            }
            catch
            {
                MessageBox.Show("Kasa nije odabrana. Provjerite postavke programa.", "Upozorenje!");
                id_kasa = "1";
            }
            try
            {
                id_ducan = DTpostavke.Rows[0]["default_ducan"].ToString();
            }
            catch
            {
                MessageBox.Show("Dućan nije odabran. Provjerite postavke programa.", "Upozorenje!");
                id_ducan = "1";
            }
        }

        private void Racunaj()
        {
            decimal UKinternet_mpc = 0;
            decimal UKodrzavanje_mpc = 0;
            decimal UKinternet_kom = 0;
            decimal UKodrzavanje_kom = 0;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                UKinternet_kom += Convert.ToDecimal(dgv.Rows[i].Cells["internet_kolicina"].FormattedValue.ToString());
                UKinternet_mpc += Convert.ToDecimal(dgv.Rows[i].Cells["cijena_interneta"].FormattedValue.ToString()) * Convert.ToDecimal(dgv.Rows[i].Cells["internet_kolicina"].FormattedValue.ToString());
                UKodrzavanje_kom += Convert.ToDecimal(dgv.Rows[i].Cells["kolicina_odrzavanje"].FormattedValue.ToString());
                UKodrzavanje_mpc += Convert.ToDecimal(dgv.Rows[i].Cells["cijena_odrzavanje"].FormattedValue.ToString()) * Convert.ToDecimal(dgv.Rows[i].Cells["kolicina_odrzavanje"].FormattedValue.ToString());
            }

            lblOdrMpc.Text = (UKodrzavanje_mpc).ToString("#0.00") + "kn";
            lblUkupnoInternetMpc.Text = (UKinternet_mpc).ToString("#0.00") + "kn";
            lblUkInt.Text = UKinternet_kom.ToString("#0.00");
            lblUkOd.Text = UKodrzavanje_kom.ToString("#0.00");
        }

        private void fill()
        {
            DataTable DT = classSQL.select(@"SELECT partners_odrzavanje.*,partners.ime_tvrtke
                FROM partners_odrzavanje
                LEFT JOIN partners ON partners_odrzavanje.id_partner=partners.id_partner
                WHERE (bivsi_korisnik='0' OR bivsi_korisnik IS NULL) AND (godisnje_odr<>'1' OR godisnje_odr IS NULL) ORDER BY partners.ime_tvrtke ASC;", "partners_odrzavanje").Tables[0];

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                dgv.Rows.Add(DT.Rows[i]["id_partner"].ToString(),
                    DT.Rows[i]["ime_tvrtke"].ToString(),
                    DT.Rows[i]["internet"].ToString(),
                    DT.Rows[i]["internet_kol"].ToString(),
                    DT.Rows[i]["odrzavanje"].ToString(),
                    DT.Rows[i]["odrzavanje_kol"].ToString(),
                    DT.Rows[i]["web_ured"].ToString() == "" ? "0" : DT.Rows[i]["web_ured"].ToString(),
                    DT.Rows[i]["tablet"].ToString() == "" ? "0" : DT.Rows[i]["tablet"].ToString(),
                    DT.Rows[i]["pcpos"].ToString() == "" ? "0" : DT.Rows[i]["pcpos"].ToString(),
                    DT.Rows[i]["pccaffe"].ToString() == "" ? "0" : DT.Rows[i]["pccaffe"].ToString(),
                    DT.Rows[i]["resort"].ToString() == "" ? "0" : DT.Rows[i]["resort"].ToString()
                    );
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color x = System.Drawing.Color.FromArgb(((byte)(105)), ((byte)(170)), ((byte)(197)));
            Color y = System.Drawing.Color.FromArgb(((byte)(40)), ((byte)(109)), ((byte)(135)));

            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void txtSifraPartnera_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtSifraPartnera.Text == "")
                {
                    frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                    partnerTrazi.ShowDialog();
                    if (Properties.Settings.Default.id_partner != "")
                    {
                        DataSet partner = new DataSet();
                        partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                        if (partner.Tables[0].Rows.Count > 0)
                        {
                            txtSifraPartnera.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                            txtNazivPartnera.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                            btnDodaj.Select();
                        }
                        else
                        {
                            MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                            txtSifraPartnera.Select();
                        }
                    }
                    else
                    {
                        txtSifraPartnera.Select();
                        return;
                    }
                }

                string Str = txtSifraPartnera.Text.Trim();
                double Num;
                bool isNum = double.TryParse(Str, out Num);
                if (!isNum)
                {
                    txtSifraPartnera.Text = "0";
                }

                DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraPartnera.Text + "'", "partners").Tables[0];
                if (DSpar.Rows.Count > 0)
                {
                    txtNazivPartnera.Text = DSpar.Rows[0][0].ToString();
                    btnDodaj.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            if (txtSifraPartnera.Text != "")
            {
                DataTable Dbool = classSQL.select("SELECT id_partner FROM partners_odrzavanje WHERE id_partner='" + txtSifraPartnera.Text + "'", "partners").Tables[0];
                if (Dbool.Rows.Count > 0)
                {
                    MessageBox.Show("Partner pod tim nazivom već postoji.", "Greška");
                    return;
                }

                DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraPartnera.Text + "'", "partners").Tables[0];
                if (DSpar.Rows.Count > 0)
                {
                    txtNazivPartnera.Text = DSpar.Rows[0][0].ToString();
                    DataTable DTinternet = classSQL.select("SELECT * FROM roba WHERE sifra='INT-PRETPLATA'", "roba").Tables[0];
                    DataTable DTodrzavanje = classSQL.select("SELECT * FROM roba WHERE sifra='PCP-ODRŽAVANJE'", "roba").Tables[0];

                    dgv.Rows.Add(txtSifraPartnera.Text, txtNazivPartnera.Text, DTodrzavanje.Rows[0]["mpc"].ToString(), 1, DTinternet.Rows[0]["mpc"].ToString(), 1);

                    classSQL.insert("INSERT INTO partners_odrzavanje (id_partner,odrzavanje,odrzavanje_kol,internet,internet_kol) VALUES (" +
                        "'" + txtSifraPartnera.Text + "'," +
                        "'" + Convert.ToDecimal(DTodrzavanje.Rows[0]["mpc"].ToString()).ToString("#0.00").Replace(",", ".") + "'," +
                        "'1'," +
                        "'" + Convert.ToDecimal(DTinternet.Rows[0]["mpc"].ToString()).ToString("#0.00").Replace(",", ".") + "'," +
                        "'1'" +
                        ")");

                    txtNazivPartnera.Text = "";
                    txtSifraPartnera.Text = "";
                    txtSifraPartnera.Select();
                    Racunaj();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            decimal d;
            if (dgv.CurrentCell.ColumnIndex == 4)
            {
                try
                {
                    if (!decimal.TryParse(dgv.Rows[e.RowIndex].Cells["cijena_odrzavanje"].FormattedValue.ToString(), out d))
                    {
                        dgv.Rows[e.RowIndex].Cells["cijena_odrzavanje"].Value = "0";
                    }

                    string sql = "UPDATE partners_odrzavanje SET odrzavanje='" + dgv.Rows[e.RowIndex].Cells["cijena_odrzavanje"].FormattedValue.ToString().Replace(",", ".") + "' WHERE id_partner='" + dgv.Rows[e.RowIndex].Cells["sifra"].FormattedValue.ToString() + "'";
                    classSQL.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 5)
            {
                try
                {
                    if (!decimal.TryParse(dgv.Rows[e.RowIndex].Cells["kolicina_odrzavanje"].FormattedValue.ToString(), out d))
                    {
                        dgv.Rows[e.RowIndex].Cells["kolicina_odrzavanje"].Value = "0";
                    }
                    string sql = "UPDATE partners_odrzavanje SET odrzavanje_kol='" + dgv.Rows[e.RowIndex].Cells["kolicina_odrzavanje"].FormattedValue.ToString().Replace(",", ".") + "' WHERE id_partner='" + dgv.Rows[e.RowIndex].Cells["sifra"].FormattedValue.ToString() + "'";
                    classSQL.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 2)
            {
                try
                {
                    if (!decimal.TryParse(dgv.Rows[e.RowIndex].Cells["cijena_interneta"].FormattedValue.ToString(), out d))
                    {
                        dgv.Rows[e.RowIndex].Cells["cijena_interneta"].Value = "0";
                    }
                    string sql = "UPDATE partners_odrzavanje SET internet='" + dgv.Rows[e.RowIndex].Cells["cijena_interneta"].FormattedValue.ToString().Replace(",", ".") + "' WHERE id_partner='" + dgv.Rows[e.RowIndex].Cells["sifra"].FormattedValue.ToString() + "'";
                    classSQL.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (dgv.CurrentCell.ColumnIndex == 3)
            {
                try
                {
                    if (!decimal.TryParse(dgv.Rows[e.RowIndex].Cells["internet_kolicina"].FormattedValue.ToString(), out d))
                    {
                        dgv.Rows[e.RowIndex].Cells["internet_kolicina"].Value = "0";
                    }

                    string sql = "UPDATE partners_odrzavanje SET internet_kol='" + dgv.Rows[e.RowIndex].Cells["internet_kolicina"].FormattedValue.ToString().Replace(",", ".") + "' WHERE id_partner='" + dgv.Rows[e.RowIndex].Cells["sifra"].FormattedValue.ToString() + "'";
                    classSQL.update(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            Racunaj();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.Rows.Count < 1)
                return;
            dgv.BeginEdit(true);
        }

        private void btnTraziPartnera_Click(object sender, EventArgs e)
        {
            if (txtSifraPartnera.Text == "")
            {
                frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
                partnerTrazi.ShowDialog();
                if (Properties.Settings.Default.id_partner != "")
                {
                    DataSet partner = new DataSet();
                    partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                    if (partner.Tables[0].Rows.Count > 0)
                    {
                        txtSifraPartnera.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                        txtNazivPartnera.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                        btnDodaj.Select();
                    }
                    else
                    {
                        MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                        txtSifraPartnera.Select();
                    }
                }
                else
                {
                    txtSifraPartnera.Select();
                    return;
                }
            }

            string Str = txtSifraPartnera.Text.Trim();
            double Num;
            bool isNum = double.TryParse(Str, out Num);
            if (!isNum)
            {
                txtSifraPartnera.Text = "0";
            }

            DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraPartnera.Text + "'", "partners").Tables[0];
            if (DSpar.Rows.Count > 0)
            {
                txtNazivPartnera.Text = DSpar.Rows[0][0].ToString();
                btnDodaj.Select();
            }
            else
            {
                MessageBox.Show("Upisana šifra ne postoji.", "Greška");
            }
        }

        private void btnGeneriraj_Click(object sender, EventArgs e)
        {
            GenerirajFakturu();

            if (brojevi_fak.Count > 0)
            {
                if (MessageBox.Show("\r\nGenerirane su fakture od broja " + brojevi_fak[0] + " do broja " + brojevi_fak[brojevi_fak.Count - 1] + ".\n Želite li ispisati iste?", "Generirano!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < brojevi_fak.Count; i++)
                    {
                        Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
                        rfak.dokumenat = "FAK";
                        rfak.racunajTecaj = false;
                        rfak.ImeForme = "Fakture";
                        rfak.naplatni = id_kasa;
                        rfak.poslovnica = id_ducan;
                        rfak.broj_dokumenta = brojevi_fak[i];
                        rfak.ShowDialog();
                    }
                }
            }
        }

        private string brojFakture()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj_fakture) FROM fakture where id_ducan = '" + id_ducan + "'", "fakture").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private List<string> brojevi_fak = new List<string>();

        private void GenerirajFakturu()
        {
            DataTable DTinternet = classSQL.select("SELECT * FROM roba WHERE sifra='CodeiT-odrzavanje'", "roba").Tables[0];
            DataTable DTweb_ured = classSQL.select("SELECT * FROM roba WHERE sifra='CodeiT-najam-a'", "roba").Tables[0];
            decimal pdv = Convert.ToDecimal(DTinternet.Rows[0]["porez"].ToString());

            for (int i = 0; i < dgv.RowCount; i++)
            {
                DateTime datum_sad = DateTime.Now;
                decimal kolicina_internet = Convert.ToDecimal(dgv.Rows[i].Cells["internet_kolicina"].FormattedValue.ToString());
                decimal kolicina_odrzavanje = Convert.ToDecimal(dgv.Rows[i].Cells["kolicina_odrzavanje"].FormattedValue.ToString());
                decimal cijena_interneta = Convert.ToDecimal(dgv.Rows[i].Cells["cijena_interneta"].FormattedValue.ToString());
                decimal cijena_odrzavanje = Convert.ToDecimal(dgv.Rows[i].Cells["cijena_odrzavanje"].FormattedValue.ToString());
                decimal PreracunataStopaPDV = (100 * pdv) / (100 + pdv);
                decimal cijena_internet_vpc = cijena_interneta - ((cijena_interneta * PreracunataStopaPDV) / 100);
                decimal cijena_odrzavanje_vpc = cijena_odrzavanje - ((cijena_odrzavanje * PreracunataStopaPDV) / 100);

                decimal cijena_web_ureda = 0;
                decimal kol_web_ureda = 0;
                decimal.TryParse(DTweb_ured.Rows[0]["mpc"].ToString(), out cijena_web_ureda);
                decimal.TryParse(dgv.Rows[i].Cells["web_ured"].FormattedValue.ToString(), out kol_web_ureda);

                decimal ukupno = (cijena_interneta * kolicina_internet) + (kolicina_odrzavanje * cijena_odrzavanje) + (cijena_web_ureda * kol_web_ureda);

                if (ukupno > 0)
                {
                    string broj_fakture = brojFakture();
                    brojevi_fak.Add(broj_fakture);

                    string sql = "INSERT INTO fakture (broj_fakture,id_odrediste,id_fakturirati,date," +
                        "dateDVO,datum_valute,id_izjava,id_zaposlenik,id_zaposlenik_izradio,model" +
                        ",id_nacin_placanja,zr,id_valuta,otprema,id_predujam,napomena,id_vd,godina_predujma," +
                        "godina_ponude,godina_fakture,oduzmi_iz_skladista,tecaj,ukupno,storno," +
                        "ukupno_povratna_naknada,ukupno_mpc,ukupno_vpc,ukupno_mpc_rabat,ukupno_rabat,ukupno_osnovica,ukupno_porez,id_ducan,id_kasa,stavke_u_valuti)" +
                        " VALUES " +
                        " (" +
                         " '" + broj_fakture + "'," +
                        " '" + dgv.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'," +
                        " '" + dgv.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'," +
                        " '" + datum_sad.ToString("yyyy-MM-dd") + "'," +
                        " '" + datum_sad.ToString("yyyy-MM-dd") + "'," +
                        " '" + datum_sad.AddDays(10).ToString("yyyy-MM-dd") + "'," +
                        " '0'," +
                        " '" + Properties.Settings.Default.id_zaposlenik + "'," +
                        " '" + Properties.Settings.Default.id_zaposlenik + "'," +
                        " ''," +
                        " '3'," +
                        " '1'," +
                        " '5'," +
                        " '1'," +
                        " '0'," +
                        " 'Plačanje za održavanje za " + DateTime.Now.Month.ToString() + " mjesec.'," +
                        " 'IFA'," +
                        " '" + DateTime.Now.Year.ToString() + "'," +
                        " '" + DateTime.Now.Year.ToString() + "'," +
                        " '" + DateTime.Now.Year.ToString() + "'," +
                        " '1'," +
                        " '1'," +
                        "'" + ukupno.ToString("0.00").Replace(".", ",") + "'," +
                        " 'NE'," +
                        "'0'," +
                        "'" + ukupno.ToString("0.00").Replace(",", ".") + "'," +
                        "'" + (ukupno - ((ukupno * PreracunataStopaPDV) / 100)).ToString().Replace(",", ".") + "'," +
                        "'0'," +
                        "'0'," +
                        "'" + (ukupno - ((ukupno * PreracunataStopaPDV) / 100)).ToString().Replace(",", ".") + "'," +
                        "'" + ((ukupno * PreracunataStopaPDV) / 100).ToString().Replace(",", ".") + "'," +
                        "'" + id_ducan + "','" + id_kasa + "','0')";
                    classSQL.insert(sql);

                    if ((cijena_interneta * kolicina_internet) > 0)
                    {
                        sql = "INSERT INTO faktura_stavke (kolicina,vpc,porez,broj_fakture,rabat,id_skladiste,sifra,oduzmi,odjava,nbc,porez_potrosnja,povratna_naknada,rabat_izn,mpc_rabat" +
                            ",ukupno_rabat,ukupno_vpc,ukupno_mpc,ukupno_mpc_rabat,povratna_naknada_izn,ukupno_porez,ukupno_osnovica,id_ducan,id_kasa) VALUES (" +
                            "'" + kolicina_internet.ToString().Replace(",", ".") + "'," +
                            "'" + cijena_internet_vpc.ToString().Replace(",", ".") + "'," +
                            "'" + pdv.ToString().Replace(",", ".") + "'," +
                            "'" + broj_fakture + "'," +
                            "'0'," +
                            "'17'," +
                            "'INT-PRETPLATA'," +
                            "'NE'," +
                            "'0'," +
                            "'0'," +
                            "'0'," +
                            "'0'," +
                            "'0'," +
                            "'0'," +
                            "'0'," +
                            "'" + (kolicina_internet * cijena_internet_vpc).ToString().Replace(",", ".") + "'," +
                            "'" + (kolicina_internet * cijena_interneta).ToString().Replace(",", ".") + "'," +
                            "'0'," +
                            "'0'," +
                            "'" + ((kolicina_internet * cijena_interneta) - (kolicina_internet * cijena_internet_vpc)).ToString().Replace(",", ".") + "'," +
                            "'" + (kolicina_internet * cijena_internet_vpc).ToString().Replace(",", ".") + "'," +
                            "'" + id_ducan + "','" + id_kasa + "'" +
                            ")";

                        classSQL.insert(sql);
                    }

                    if ((cijena_web_ureda * kol_web_ureda) > 0)
                    {
                        sql = "INSERT INTO faktura_stavke (kolicina,vpc,porez,broj_fakture,rabat,id_skladiste,sifra,oduzmi,odjava,nbc,porez_potrosnja,povratna_naknada,rabat_izn,mpc_rabat" +
                            ",ukupno_rabat,ukupno_vpc,ukupno_mpc,ukupno_mpc_rabat,povratna_naknada_izn,ukupno_porez,ukupno_osnovica,id_ducan,id_kasa) VALUES (" +
                            "'" + kol_web_ureda.ToString().Replace(",", ".") + "'," +
                            "'" + (cijena_web_ureda / (1 + (pdv / 100))).ToString().Replace(",", ".") + "'," +
                            "'" + pdv.ToString().Replace(",", ".") + "'," +
                            "'" + broj_fakture + "'," +
                            "'0'," +
                            "'17'," +
                            "'CodeiT-najam-a'," +
                            "'NE'," +
                            "'0'," +
                            "'0'," +
                            "'0'," +
                            "'0'," +
                            "'0'," +
                            "'0'," +
                            "'0'," +
                            "'" + (kol_web_ureda * (cijena_web_ureda / (1 + (pdv / 100)))).ToString().Replace(",", ".") + "'," +
                            "'" + (cijena_web_ureda * kol_web_ureda).ToString().Replace(",", ".") + "'," +
                            "'0'," +
                            "'0'," +
                            "'" + ((kol_web_ureda * (cijena_web_ureda)) - (kol_web_ureda * (cijena_web_ureda / (1 + (pdv / 100))))).ToString().Replace(",", ".") + "'," +
                            "'" + (kol_web_ureda * (cijena_web_ureda / (1 + (pdv / 100)))).ToString().Replace(",", ".") + "'," +
                            "'" + id_ducan + "','" + id_kasa + "'" +
                            ")";

                        classSQL.insert(sql);
                    }

                    if ((cijena_odrzavanje * kolicina_odrzavanje) > 0)
                    {
                        sql = "INSERT INTO faktura_stavke (kolicina,vpc,porez,broj_fakture,rabat,id_skladiste,sifra,oduzmi,odjava,nbc,porez_potrosnja,povratna_naknada,rabat_izn,mpc_rabat" +
                            ",ukupno_rabat,ukupno_vpc,ukupno_mpc,ukupno_mpc_rabat,povratna_naknada_izn,ukupno_porez,ukupno_osnovica,id_ducan,id_kasa) VALUES (" +
                            "'" + kolicina_odrzavanje.ToString().Replace(",", ".") + "'," +
                            "'" + cijena_odrzavanje_vpc.ToString().Replace(",", ".") + "'," +
                            "'" + pdv.ToString().Replace(",", ".") + "'," +
                            "'" + broj_fakture + "'," +
                            "'0'," +
                            "'17'," +
                            "'CodeiT-odrzavanje'," +
                            "'NE'," +
                            "'0'," +
                            "'0'," +
                            "'0'," +
                            "'0'," +
                            "'0'," +
                            "'0'," +
                            "'0'," +
                            "'" + (kolicina_odrzavanje * cijena_odrzavanje_vpc).ToString().Replace(",", ".") + "'," +
                            "'" + (kolicina_odrzavanje * cijena_odrzavanje).ToString().Replace(",", ".") + "'," +
                            "'0'," +
                            "'0'," +
                            "'" + ((kolicina_odrzavanje * cijena_odrzavanje) - (kolicina_odrzavanje * cijena_odrzavanje_vpc)).ToString().Replace(",", ".") + "'," +
                            "'" + (cijena_odrzavanje_vpc * kolicina_odrzavanje).ToString().Replace(",", ".") + "'," +
                            "'" + id_ducan + "','" + id_kasa + "'" +
                            ")";

                        classSQL.insert(sql);
                    }
                }
            }
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                classSQL.delete("DELETE FROM partners_odrzavanje WHERE id_partner='" + dgv.Rows[dgv.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "'");
                dgv.Rows.RemoveAt(dgv.CurrentRow.Index);
                Racunaj();
            }
        }
    }
}