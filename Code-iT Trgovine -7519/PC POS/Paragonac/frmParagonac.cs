using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmParagonac : Form
    {
        public frmParagonac()
        {
            InitializeComponent();
        }

        public string IznosKartica { get; set; }
        public string IznosGotovina { get; set; }
        public string IznosVirman { get; set; } //trenutno nije implementirano
        public string DobivenoGotovina { get; set; }
        public string placanje { get; set; }
        public string odabranoSkladiste = "1";
        public bool vecSelektiran = true;

        public string id_ducan { get; set; }
        public string id_kasa { get; set; }
        public string sifra_skladiste { get; set; }
        private DataTable DTpostavkePrinter;
        private DataSet DS_Skladiste;
        private DataSet DSpostavke;
        private DataTable DTpromocije;
        private DataTable DTpromocije1;
        private DataTable DTsend = new DataTable();
        private double ukupno = 0;
        private string brRac;
        private string blagajnik_ime;
        private string sifraPartnera = "0";
        private string ZadnjiRacun;
        private string porez_zadano = "0";
        public frmParagonac MainForm { get; set; }

        private INIFile ini = new INIFile();

        private void frmKasa_Load(object sender, EventArgs e)
        {
            btnGotovina.Enabled = Class.Postavke.maloprodaja_naplata_gotovina_button_show;
            //btnGotovina.Visible = Class.Postavke.maloprodaja_naplata_gotovina_button_show;

            btnKartica.Enabled = Class.Postavke.maloprodaja_naplata_kartica_button_show;
            //btnKartica.Visible = Class.Postavke.maloprodaja_naplata_kartica_button_show;

            if (ini.Read("POSTAVKE", "pdv_paragon") != "")
            {
                porez_zadano = ini.Read("POSTAVKE", "pdv_paragon");
            }

            if (ini.Read("POSTAVKE", "paragonac") == "1")
            {
                dgv.Columns["POREZ"].ReadOnly = true;
            }

            btnInsert.Select();
            PaintRows(dgv);
            DSpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke");
            MyDataGrid.MainForm = this;

            if (DSpostavke.Tables[0].Rows.Count > 0)
            {
                try
                {
                    id_kasa = DSpostavke.Tables[0].Rows[0]["default_blagajna"].ToString();
                }
                catch
                {
                    MessageBox.Show("Kasa nije odabrana. Provjerite postavke programa.", "Upozorenje!");
                    id_kasa = "0";
                }
                try
                {
                    id_ducan = DSpostavke.Tables[0].Rows[0]["default_ducan"].ToString();
                }
                catch
                {
                    MessageBox.Show("Dućan nije odabran. Provjerite postavke programa.", "Upozorenje!");
                    id_ducan = "0";
                }
                try
                {
                    sifra_skladiste = DSpostavke.Tables[0].Rows[0]["default_skladiste"].ToString();
                }
                catch
                {
                    MessageBox.Show("Skladište nije odabrano. Provjerite postavke programa.", "Upozorenje!");
                    sifra_skladiste = "0";
                }
            }
            else
            {
                MessageBox.Show("Kasa, dućan ili skladište nisu odabrani.", "Upozorenje!");
            }
            SetSmjene();

            DTpromocije1 = classSQL.select("SELECT * FROM promocija1", "promocija1").Tables[0];
            brRac = brojRacuna();
            lblBrojRac.Text = "Broj računa: " + brRac + "/" + DateTime.Now.Year;
            blagajnik_ime = classSQL.select("SELECT ime + ' ' + prezime FROM zaposlenici WHERE id_zaposlenik = '" +
                Properties.Settings.Default.id_zaposlenik + "'",
                "zaposlenici").Tables[0].Rows[0][0].ToString();
            DTpromocije = classSQL.select("SELECT * FROM promocije WHERE do_datuma >='" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") +
                "' AND od_datuma <= '" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'", "promocije").Tables[0];

            DTpostavkePrinter = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
        }

        private void SetSmjene()
        {
            string ZBS = ZadnjiBrojSmjene();

            if (ZBS == "null")
            {
                Kasa.frmPocetnoStanjeSmjene ps = new Kasa.frmPocetnoStanjeSmjene();
                ps.ShowDialog();
            }
            else
            {
                string sql = "SELECT * FROM smjene WHERE id='" + ZBS + "'";
                DataTable DT_smjena = classSQL.select(sql, "smjene").Tables[0];

                if (DT_smjena.Rows.Count > 0)
                {
                    if (DT_smjena.Rows[0]["zavrsetak"].ToString() != "")
                    {
                        Kasa.frmPocetnoStanjeSmjene ps = new Kasa.frmPocetnoStanjeSmjene();
                        ps.ShowDialog();
                    }
                }
            }
        }

        private string ZadnjiBrojSmjene()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(id) FROM smjene", "smjene").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString())).ToString();
            }
            else
            {
                return "null";
            }
        }

        #region UpravljanjeDGV

        private class MyDataGrid : System.Windows.Forms.DataGridView
        {
            public static frmParagonac MainForm { get; set; }

            protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
            {
                if (keyData == Keys.Enter)
                {
                    MainForm.EnterDGW(MainForm.dgv);
                    return true;
                }
                else if (keyData == Keys.Right)
                {
                    MainForm.RightDGW(MainForm.dgv);
                    return true;
                }
                else if (keyData == Keys.Left)
                {
                    MainForm.LeftDGW(MainForm.dgv);
                    return true;
                }
                else if (keyData == Keys.Up)
                {
                    MainForm.UpDGW(MainForm.dgv);
                    return true;
                }
                else if (keyData == Keys.Down)
                {
                    MainForm.DownDGW(MainForm.dgv);
                    return true;
                }
                else if (keyData == Keys.Insert)
                {
                    MainForm.dgv.Rows.Add("", "", "kom", "0,00", "1", "0,00", MainForm.porez_zadano, "0,00", "0");

                    MainForm.RedniBroj();
                    MainForm.PaintRows(MainForm.dgv);
                    return true;
                }
                else if (keyData == Keys.Delete)
                {
                    MainForm.dgv.Rows.RemoveAt(MainForm.dgv.CurrentRow.Index);
                    MainForm.RedniBroj();
                    MainForm.Ukupno();
                    return true;
                }
                else if (keyData == Keys.Insert)
                {
                    MainForm.btnInsert.PerformClick();
                }
                else if (keyData == Keys.F1)
                {
                    MainForm.btnTrazi.PerformClick();
                }
                else if (keyData == Keys.F2)
                {
                    MainForm.btnPartner.PerformClick();
                }
                else if (keyData == Keys.F3)
                {
                    MainForm.btnObrisiStavku.PerformClick();
                }
                else if (keyData == Keys.F4)
                {
                    MainForm.btnAlati.PerformClick();
                }
                else if (keyData == Keys.F5)
                {
                    if (Class.Postavke.maloprodaja_naplata_gotovina_button_show)
                    {
                        MainForm.btnGotovina.PerformClick();
                    }
                }
                else if (keyData == Keys.F6)
                {
                    if (Class.Postavke.maloprodaja_naplata_kartica_button_show)
                    {
                        MainForm.btnKartica.PerformClick();
                    }
                }
                else if (keyData == Keys.F7)
                {
                    MainForm.btnOdustaniSve.PerformClick();
                }
                else if (keyData == Keys.F9)
                {
                    MainForm.btnOdustaniSve.PerformClick();
                }
                else if (keyData == Keys.F10)
                {
                    MainForm.btnOdustaniSve.PerformClick();
                }
                else if (keyData == Keys.Escape)
                {
                    MainForm.btnOdjava.PerformClick();
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void PaintRows(DataGridView dg)
        {
            //int br = 0;
            //for (int i = 0; i < dg.Rows.Count; i++)
            //{
            //    if (br == 0)
            //    {
            //        dg.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
            //        br++;
            //    }
            //    else
            //    {
            //        dg.Rows[i].DefaultCellStyle.BackColor = Color.White;
            //        br = 0;
            //    }

            //}
            DataGridViewRow row = dg.RowTemplate;
            row.Height = 25;
        }

        private void EnterDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            if (d.CurrentCell.ColumnIndex == 1)
            {
                d.CurrentCell = dgv.Rows[d.CurrentRow.Index].Cells[2];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 2)
            {
                d.CurrentCell = dgv.Rows[d.CurrentRow.Index].Cells[3];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 3)
            {
                d.CurrentCell = dgv.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 4)
            {
                d.CurrentCell = dgv.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                d.CurrentCell = dgv.Rows[d.CurrentRow.Index].Cells[6];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 6)
            {
                dgv.Rows.Add("", "", "kom", "0,00", "1", "0,00", porez_zadano, "0,00", "0,00");
                dgv.Select();
                dgv.CurrentCell = dgv.Rows[dgv.RowCount - 1].Cells[1];
                PaintRows(dgv);
                dgv.BeginEdit(true);
                RedniBroj();
            }
        }

        private void LeftDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            if (d.CurrentCell.ColumnIndex == 1)
            {
            }
            else if (d.CurrentCell.ColumnIndex == 2)
            {
                d.CurrentCell = dgv.Rows[d.CurrentRow.Index].Cells[1];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 3)
            {
                d.CurrentCell = dgv.Rows[d.CurrentRow.Index].Cells[2];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 4)
            {
                d.CurrentCell = dgv.Rows[d.CurrentRow.Index].Cells[3];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                d.CurrentCell = dgv.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 6)
            {
                d.CurrentCell = dgv.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            //else if (d.CurrentCell.ColumnIndex == 7)
            //{
            //    d.CurrentCell = dgv.Rows[d.CurrentRow.Index].Cells[6];
            //    d.BeginEdit(true);
            //}
            //else if (d.CurrentCell.ColumnIndex == 8)
            //{
            //    d.CurrentCell = dgv.Rows[d.CurrentRow.Index].Cells[7];
            //    d.BeginEdit(true);
            //}
            //else if (d.CurrentCell.ColumnIndex == 9)
            //{
            //    d.CurrentCell = dgv.Rows[d.CurrentRow.Index].Cells[8];
            //    d.BeginEdit(true);
            //}
        }

        private void RightDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            if (d.CurrentCell.ColumnIndex == 1)
            {
                d.CurrentCell = dgv.Rows[d.CurrentRow.Index].Cells[2];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 2)
            {
                d.CurrentCell = dgv.Rows[d.CurrentRow.Index].Cells[3];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 3)
            {
                d.CurrentCell = dgv.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 4)
            {
                d.CurrentCell = dgv.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                d.CurrentCell = dgv.Rows[d.CurrentRow.Index].Cells[6];
                d.BeginEdit(true);
            }
        }

        private void UpDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            int curent = d.CurrentRow.Index;
            if (curent == 0)
            {
                d.CurrentCell = dgv.Rows[d.CurrentRow.Index].Cells[1];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgv.Rows[d.CurrentRow.Index - 1].Cells[1];
                d.BeginEdit(true);
            }
        }

        private void DownDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            int curent = d.CurrentRow.Index;
            if (curent == d.RowCount - 1)
            {
                d.CurrentCell = dgv.Rows[d.CurrentRow.Index].Cells[1];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgv.Rows[d.CurrentRow.Index + 1].Cells[1];
                d.BeginEdit(true);
            }
        }

        #endregion UpravljanjeDGV

        private void izracun()
        {
            if (dgv.RowCount > 0)
            {
                int rowBR = dgv.CurrentRow.Index;

                double dec_parse;
                if (!Double.TryParse(dgv.Rows[rowBR].Cells["cijena"].FormattedValue.ToString(), out dec_parse))
                {
                    dgv.Rows[rowBR].Cells["cijena"].Value = "0,00";
                    MessageBox.Show("Greška kod upisa količine.", "Greška"); return;
                }

                if (!Double.TryParse(dgv.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString(), out dec_parse))
                {
                    dgv.Rows[rowBR].Cells["kolicina"].Value = "1";
                    MessageBox.Show("Greška kod upisa rabata.", "Greška"); return;
                }

                if (!Double.TryParse(dgv.Rows[rowBR].Cells["popust"].FormattedValue.ToString(), out dec_parse))
                {
                    dgv.Rows[rowBR].Cells["popust"].Value = "0,00";
                    MessageBox.Show("Greška kod rabata.", "Greška"); return;
                }

                if (!Double.TryParse(dgv.Rows[rowBR].Cells["porez"].FormattedValue.ToString(), out dec_parse))
                {
                    dgv.Rows[rowBR].Cells["porez"].Value = "0,00";
                    MessageBox.Show("Greška kod rabata.", "Greška"); return;
                }

                double kol = Math.Round(Convert.ToDouble(dgv.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString()), 3);
                double stopa = Math.Round(Convert.ToDouble(dgv.Rows[rowBR].Cells["porez"].FormattedValue.ToString()), 2);
                double rbt = Math.Round(Convert.ToDouble(dgv.Rows[rowBR].Cells["popust"].FormattedValue.ToString()), 2);
                double mpc = Math.Round(Convert.ToDouble(dgv.Rows[rowBR].Cells["cijena"].FormattedValue.ToString()), 2);

                //izračun za vpc
                double PreracunataStopaPDV = 100 * stopa / (100 + stopa);
                double rabat = mpc * rbt / 100;
                double porez = (mpc - rabat) * PreracunataStopaPDV / 100;

                double vpc = (mpc - (mpc * PreracunataStopaPDV / 100));

                double mpc_sa_kolicinom = (mpc - rabat) * kol;

                double iznos = mpc_sa_kolicinom;

                dgv.Rows[rowBR].Cells["cijena"].Value = Math.Round(mpc, 3).ToString("#0.00");
                dgv.Rows[rowBR].Cells["vpc"].Value = Math.Round(vpc, 3).ToString("#0.000");
                dgv.Rows[rowBR].Cells["kolicina"].Value = Math.Round(kol, 3).ToString("#0.00");
                dgv.Rows[rowBR].Cells["iznos"].Value = Math.Round(iznos, 3).ToString("#0.00");
                dgv.Rows[rowBR].Cells["mpc"].Value = Math.Round(mpc, 3).ToString("#0.00");

                Ukupno();
            }
            //dgv.BeginEdit(true);
        }

        #region Util

        private void SetSkladiste(string skladiste)
        {
            if (dgv.RowCount > 0)
            {
                if (dgv.CurrentRow.Cells[8].FormattedValue.ToString() == "DA")
                {
                    dgv.CurrentRow.Cells["skladiste"].Value = skladiste;

                    DataTable DSprodaja = classSQL.select("SELECT vpc,porez,kolicina FROM roba_prodaja WHERE sifra='" +
                        dgv.CurrentRow.Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + skladiste + "'",
                        "roba_prodaja").Tables[0];
                    if (DSprodaja.Rows.Count > 0)
                    {
                        dgv.CurrentRow.Cells[4].Value = Math.Round((Convert.ToDouble(DSprodaja.Rows[0]["vpc"].ToString()) *
                            Convert.ToDouble(DSprodaja.Rows[0]["porez"].ToString()) / 100) +
                            Convert.ToDouble(DSprodaja.Rows[0]["vpc"].ToString()), 2).ToString("#0.00");
                        dgv.CurrentRow.Cells[7].Value = Math.Round((Convert.ToDouble(DSprodaja.Rows[0]["vpc"].ToString()) *
                            Convert.ToDouble(DSprodaja.Rows[0]["porez"].ToString()) / 100) +
                            Convert.ToDouble(DSprodaja.Rows[0]["vpc"].ToString()), 2).ToString("#0.00");

                        double rabat = Convert.ToDouble(dgv.CurrentRow.Cells[6].Value.ToString());
                        double cijena = Convert.ToDouble(dgv.CurrentRow.Cells[4].Value.ToString());
                        double kolicina = Convert.ToDouble(dgv.CurrentRow.Cells[5].Value.ToString());
                        double ukupno;
                        ukupno = cijena * kolicina;
                        rabat = ukupno * rabat / 100;
                        dgv.CurrentRow.Cells[7].Value = Math.Round(ukupno - rabat, 2).ToString("#0.00");
                        Ukupno();
                    }
                    else
                    {
                        //MessageBox.Show("Roba nije definirana na odabranom skladištu!");
                        //btnObrisiStavku.Select();
                    }
                }
                else
                {
                    //MessageBox.Show("Kod robe koja se ne oduzima na skladištu se niti ne mijenja skladište!");
                }
            }
        }

        private void SetRabat(double rabat)
        {
            double cijena = Convert.ToDouble(dgv.CurrentRow.Cells[4].Value.ToString());
            double kolicina = Convert.ToDouble(dgv.CurrentRow.Cells[5].Value.ToString());
            double ukupno;
            ukupno = cijena * kolicina;
            rabat = ukupno * rabat / 100;
            dgv.CurrentRow.Cells[7].Value = Math.Round(ukupno - rabat, 2).ToString("#0.00");
            Ukupno();
        }

        private void SetKolicina(double kolicina)
        {
            double cijena = Convert.ToDouble(dgv.CurrentRow.Cells[4].Value.ToString());
            double rabat = Convert.ToDouble(dgv.CurrentRow.Cells[6].Value.ToString());
            double ukupno;
            ukupno = cijena * kolicina;
            rabat = ukupno * rabat / 100;
            dgv.CurrentRow.Cells[7].Value = Math.Round(ukupno - rabat, 2).ToString("#0.00");
            Ukupno();
        }

        private void GetKolicinaSkladiste(string sifra, string skladiste, string jmj)
        {
            DataTable DTkol = classSQL.select("SELECT kolicina FROM roba_prodaja WHERE id_skladiste='" + skladiste + "'" +
                " AND sifra='" + sifra + "'", "roba_prodaja").Tables[0];

            decimal kolicina;

            if (DTkol.Rows.Count < 1)
            {
                kolicina = 0;
                lblSkladiste.Text = "Artikl pod šifrom " + sifra + " na odabranom skladištu ima " + kolicina + " " + jmj;
                lblSkladiste.ForeColor = Color.Red;
            }
            else
            {
                kolicina = Convert.ToDecimal(DTkol.Rows[0][0].ToString());
                lblSkladiste.Text = "Artikl pod šifrom " + sifra + " na odabranom skladištu ima " + kolicina + " " + jmj;
                lblSkladiste.ForeColor = kolicina > 0 ? Color.Lime : Color.Red;
            }
        }

        private bool GetKolicinaSkaldisteUpozorenje(string sifra, string skladiste)
        {
            string sql = "SELECT oduzmi FROM roba WHERE roba.sifra='" + sifra + "'";
            DataTable DTkol = classSQL.select(sql, "roba_prodaja").Tables[0];

            if (DTkol.Rows[0][0].ToString() == "NE") return true;
            if (DTkol.Rows[0][0].ToString() == "")
            {
                MessageBox.Show("Roba ne postoji na skladištu!");
                return false;
            }

            sql = "SELECT kolicina FROM roba_prodaja WHERE id_skladiste='" + skladiste + "' AND sifra='" + sifra + "'";
            DTkol = classSQL.select(sql, "roba_prodaja").Tables[0];

            sql = "select sum(cast(replace(kolicina,',','.') as numeric)) AS [Količina], " +
                "skladiste AS [Skladište] from roba_prodaja r, skladiste s where r.id_skladiste=s.id_skladiste and sifra='" +
                sifra + "' group by skladiste order by skladiste";
            DataTable DTkolSvaSkladista = classSQL.select(sql, "roba_prodaja").Tables[0];

            if (DTkol.Rows.Count > 0)
            {
                decimal kolicina = Convert.ToDecimal(DTkol.Rows[0][0].ToString());

                if (kolicina > 0)
                {
                    return true;
                }
                else
                {
                    //UPOZORENJE!!!
                    return skladisteUMinusUpozorenje(kolicina, DTkolSvaSkladista);
                }
            }
            else
            {
                //UPOZORENJE!!!
                return skladisteUMinusUpozorenje(0, DTkolSvaSkladista);
            }
        }

        private bool skladisteUMinusUpozorenje(decimal kolicina, DataTable svaSkladista)
        {
            //forma za šifru
            Kasa.frmUnesiSifruSkladisteUMinus frm = new Kasa.frmUnesiSifruSkladisteUMinus();
            frm.svaSkladista = svaSkladista;
            frm.ShowDialog();

            if (frm.SKIDAJ)
            {
                //unesi zapisnik u bazu
                string d = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                string sql = "INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja)" +
                    " VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + d +
                    "','SKLADIŠTE U MINUS! RAČUN: " + brRac + "/" + DateTime.Now.Year + "')";
                classSQL.insert(sql);

                //lblSkladiste.Text = "Artikl pod šifrom " + sifra + " na skladištu ima " + kolicina + " " + jmj.ToString();
                //lblSkladiste.ForeColor = Color.Red;
            }

            return frm.SKIDAJ;
        }

        private void Ukupno()
        {
            ukupno = 0;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (dgv.Rows[i].Cells["iznos"].Value != null)
                {
                    ukupno += Convert.ToDouble(dgv.Rows[i].Cells["iznos"].FormattedValue);
                    //lblUkupno.Text = String.Format("{0:0.00}", ukupno) + " Kn";
                }
            }

            lblUkupno.Text = Math.Round(ukupno, 2).ToString("#0.00") + " Kn";
        }

        private string artikl_start = "";
        private string cijena_start = "";
        private string artikl_display = "";
        private string cijena_display = "";

        private void backgroundWorkerSendToDisplay_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = dgv.Rows.Count - 1;
            if (dgv.Rows[i].Cells["naziv"].FormattedValue.ToString() == "") { return; }
            artikl_start = dgv.Rows[i].Cells["naziv"].FormattedValue.ToString();
            cijena_start = dgv.Rows[i].Cells["iznos"].FormattedValue.ToString();
            if (artikl_start.Length > 12) { artikl_start = artikl_start.Substring(0, 12); }

            if (cijena_start != cijena_display || artikl_start != artikl_display)
            {
                cijena_display = cijena_start;
                artikl_display = artikl_start;
                //PosPrint.classLineDisplay.WriteOnDisplay(artikl_display + " " + String.Format("{0:0.00}",
                //Convert.ToDouble(cijena_display)) + "\n" + "UKUPNO: " + String.Format("{0:0.00}", ukupno));
            }
        }

        private void SetOnNull()
        {
            dgv.Rows.Clear();
            txtImePartnera.Text = "";
            txtImePartnera.Text = "";
            lblUkupno.Text = "0,00 Kn";
            sifraPartnera = "0";
        }

        private void NoviUnos()
        {
            brRac = brojRacuna();
            lblBrojRac.Text = "Broj računa: " + brRac + "/" + DateTime.Now.Year;
            SetOnNull();
        }

        private string brojRacuna()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_racuna AS bigint)) FROM racuni WHERE id_ducan=" + id_ducan + " AND id_kasa=" + id_kasa, "racuni").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private string IspisNapomene()
        {
            string napomena = "";
            if (DSpostavke.Tables[0].Rows.Count > 0)
            {
                if (DSpostavke.Tables[0].Rows[0]["napomena_na_racunu"].ToString() == "1")
                {
                    Kasa.frmNapomenaRacun nap = new Kasa.frmNapomenaRacun();
                    nap.ShowDialog();
                    napomena = nap.napomena;
                }
            }

            return napomena;
        }

        private string barcode = "";

        private void SpremiRacun(string kartica, string gotovina)
        {
            classSQL.transaction("BEGIN;");
            string kol;
            DTsend = new DataTable();
            DTsend.Columns.Add("broj_racuna");
            DTsend.Columns.Add("sifra_robe");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("mpc");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("nbc");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("cijena");
            DTsend.Columns.Add("ime");
            DTsend.Columns.Add("porez_potrosnja");
            DTsend.Columns.Add("povratna_naknada");
            DTsend.Columns.Add("povratna_naknada_izn");
            DTsend.Columns.Add("rabat_izn");
            DTsend.Columns.Add("mpc_rabat");
            DTsend.Columns.Add("ukupno_rabat");
            DTsend.Columns.Add("ukupno_vpc");
            DTsend.Columns.Add("ukupno_mpc");
            DTsend.Columns.Add("ukupno_mpc_rabat");
            DTsend.Columns.Add("ukupno_porez");
            DTsend.Columns.Add("ukupno_osnovica");
            DTsend.Columns.Add("id_ducan");
            DTsend.Columns.Add("id_kasa");
            DTsend.Columns.Add("porez_na_dohodak");
            DTsend.Columns.Add("prirez");
            DTsend.Columns.Add("porez_na_dohodak_iznos");
            DTsend.Columns.Add("prirez_iznos");
            DataRow row;

            DataTable DTtemp;

            string g;
            string k;
            if (IznosGotovina != null && IznosGotovina != "0")
            {
                g = "1";
            }
            else
            {
                g = "0";
            }

            if (IznosKartica != null && IznosKartica != "0")
            {
                k = "1";
            }
            else
            {
                k = "0";
            }

            if (Convert.ToDecimal(IznosGotovina) == 0 && Convert.ToDecimal(IznosKartica) > 0)
            {
                placanje = "K";
            }
            else if (Convert.ToDecimal(IznosGotovina) > 0 && Convert.ToDecimal(IznosKartica) == 0)
            {
                placanje = "G";
            }
            else if (Convert.ToDecimal(IznosGotovina) > 0 && Convert.ToDecimal(IznosKartica) > 0)
            {
                placanje = "O";
            }

            //trenutno nije implementirano
            IznosVirman = "0.00";
            //trenutno nije implementirano

            DateTime dRac = Convert.ToDateTime(DateTime.Now);
            string dt = dRac.ToString("yyyy-MM-dd H:mm:ss");

            double kolNaknada;
            //--------STAVKE-------------
            double ukupnoMpcRabat, ukupnoVpc, ukupnoMpc, ukupnoPovratnaNaknada, ukupnoRabat, ukupnoPorez, ukupnoOsnovica;
            double rabatIzn, rabat, mpc, porez, mpcRabat, vpc, povratnaNaknada;
            double kolicina;
            double povratnaNaknadaUkRacun = 0;
            double ukupnoMpcRabatRacun = 0;
            double ukupnoMpcRacun = 0;
            double ukupnoVpcRacun = 0;
            double ukupnoRabatRacun = 0;
            double ukupnoPorezRacun = 0;
            double ukupnoOsnovicaRacun = 0;

            string sifra = "";
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                /////////////////////////////////////////////////TU NASTAVI////////////////////////////////

                row = DTsend.NewRow();
                row["broj_racuna"] = brRac;
                row["sifra_robe"] = ReturnSifra(i);
                row["id_skladiste"] = "0";
                row["mpc"] = dg(i, "cijena");
                row["porez"] = dg(i, "porez");
                row["kolicina"] = dg(i, "kolicina");
                row["rabat"] = dg(i, "popust");
                row["vpc"] = dg(i, "vpc");
                row["cijena"] = dg(i, "cijena");
                row["nbc"] = "0";
                row["ime"] = dg(i, "naziv");
                row["porez_potrosnja"] = "0";
                row["povratna_naknada"] = "0";
                row["povratna_naknada_izn"] = "0";

                kolicina = Convert.ToDouble(dg(i, "kolicina"));

                rabat = Convert.ToDouble(dg(i, "popust"));
                mpc = Convert.ToDouble(dg(i, "cijena"));
                vpc = Convert.ToDouble(dg(i, "vpc"));
                porez = Convert.ToDouble(dg(i, "porez"));

                ukupnoMpc = Math.Round(mpc * kolicina, 2);
                ukupnoMpcRacun += ukupnoMpc;

                ukupnoVpc = Math.Round(vpc * kolicina, 3);
                ukupnoVpcRacun += ukupnoVpc;

                ukupnoMpcRabat = Math.Round(Convert.ToDouble(dg(i, "iznos")), 2);
                ukupnoMpcRabatRacun += ukupnoMpcRabat;
                ukupnoRabat = ukupnoMpc - ukupnoMpcRabat;
                mpcRabat = Math.Round(ukupnoMpcRabat / kolicina, 2);
                rabatIzn = mpc - mpcRabat;
                ukupnoRabatRacun += ukupnoRabat;

                ukupnoOsnovica = Math.Round(ukupnoMpcRabat / (1 + porez / 100), 2);
                ukupnoOsnovicaRacun += ukupnoOsnovica;
                ukupnoPorez = Math.Round(ukupnoMpcRabat - ukupnoOsnovica, 2);
                ukupnoPorezRacun += ukupnoPorez;

                if (classSQL.remoteConnectionString == "")
                {
                    row["mpc"] = dg(i, "cijena").Replace(",", ".");
                    row["porez"] = dg(i, "porez").Replace(",", ".");
                    row["vpc"] = dg(i, "vpc").Replace(",", ".");
                    row["nbc"] = dg(i, "nbc").Replace(",", ".");
                    row["cijena"] = dg(i, "cijena").Replace(",", ".");
                    row["povratna_naknada_izn"] = "0";
                    row["povratna_naknada"] = "0";
                    row["rabat_izn"] = rabatIzn.ToString("#0.00").Replace(",", ".");
                    row["mpc_rabat"] = mpcRabat.ToString("#0.00").Replace(",", ".");
                    row["ukupno_rabat"] = ukupnoRabat.ToString("#0.00").Replace(",", ".");
                    row["ukupno_vpc"] = ukupnoVpc.ToString("#0.000").Replace(",", ".");
                    row["ukupno_mpc"] = ukupnoMpc.ToString("#0.00").Replace(",", ".");
                    row["ukupno_mpc_rabat"] = ukupnoMpcRabat.ToString("#0.00").Replace(",", ".");
                    row["ukupno_porez"] = ukupnoPorez.ToString("#0.00").Replace(",", ".");
                    row["ukupno_osnovica"] = ukupnoOsnovica.ToString("#0.00").Replace(",", ".");

                    row["porez_na_dohodak"] = 0;
                    row["prirez"] = 0;
                    row["porez_na_dohodak_iznos"] = 0;
                    row["prirez_iznos"] = 0;
                }
                else
                {
                    row["mpc"] = dg(i, "cijena").Replace(".", ",");
                    row["porez"] = dg(i, "porez").Replace(".", ",");
                    row["vpc"] = dg(i, "vpc").Replace(".", ",");
                    row["nbc"] = "0";
                    row["cijena"] = dg(i, "cijena").Replace(".", ",");
                    row["povratna_naknada_izn"] = "0";
                    row["povratna_naknada"] = "0";
                    row["rabat_izn"] = rabatIzn.ToString("#0.00").Replace(".", ",");
                    row["mpc_rabat"] = mpcRabat.ToString("#0.00").Replace(".", ",");
                    row["ukupno_rabat"] = ukupnoRabat.ToString("#0.00").Replace(".", ",");
                    row["ukupno_vpc"] = ukupnoVpc.ToString("#0.000").Replace(".", ",");
                    row["ukupno_mpc"] = ukupnoMpc.ToString("#0.00").Replace(".", ",");
                    row["ukupno_mpc_rabat"] = ukupnoMpcRabat.ToString("#0.00").Replace(".", ",");
                    row["ukupno_porez"] = ukupnoPorez.ToString("#0.00").Replace(".", ",");
                    row["ukupno_osnovica"] = ukupnoOsnovica.ToString("#0.00").Replace(".", ",");
                    row["porez_na_dohodak"] = 0;
                    row["prirez"] = 0;
                    row["porez_na_dohodak_iznos"] = 0;
                    row["prirez_iznos"] = 0;
                }

                DTsend.Rows.Add(row);
            }

            string uk1 = ukupno.ToString();
            string PovratnaNaknadaUkRacun, UkupnoMpcRacun, UkupnoVpcRacun,
                UkupnoMpcRabatRacun, UkupnoRabatRacun, UkupnoOsnovicaRacun, UkupnoPorezRacun;
            string dobiveno_gotovina;
            if (classSQL.remoteConnectionString == "")
            {
                uk1 = uk1.Replace(",", ".");
                IznosGotovina = IznosGotovina.Replace(",", ".");
                IznosKartica = IznosKartica.Replace(",", ".");
                IznosVirman = IznosKartica.Replace(",", ".");
                dobiveno_gotovina = DobivenoGotovina.Replace(",", ".");
                PovratnaNaknadaUkRacun = povratnaNaknadaUkRacun.ToString("#0.00").Replace(",", ".");
                UkupnoMpcRacun = ukupnoMpcRacun.ToString("#0.00").Replace(",", ".");
                UkupnoVpcRacun = ukupnoVpcRacun.ToString("#0.000").Replace(",", ".");
                UkupnoMpcRabatRacun = ukupnoMpcRabatRacun.ToString("#0.00").Replace(",", ".");
                UkupnoRabatRacun = ukupnoRabatRacun.ToString("#0.00").Replace(",", ".");
                UkupnoOsnovicaRacun = ukupnoOsnovicaRacun.ToString("#0.00").Replace(",", ".");
                UkupnoPorezRacun = ukupnoPorezRacun.ToString("#0.00").Replace(",", ".");
            }
            else
            {
                IznosGotovina = IznosGotovina.Replace(".", ",");
                IznosKartica = IznosKartica.Replace(".", ",");
                IznosVirman = IznosVirman.Replace(".", ",");
                uk1 = uk1.Replace(".", ",");
                dobiveno_gotovina = DobivenoGotovina.Replace(".", ",");
                PovratnaNaknadaUkRacun = povratnaNaknadaUkRacun.ToString("#0.00").Replace(",", ".");
                UkupnoMpcRacun = ukupnoMpcRacun.ToString("#0.00").Replace(",", ".");
                UkupnoVpcRacun = ukupnoVpcRacun.ToString("#0.000").Replace(",", ".");
                UkupnoMpcRabatRacun = ukupnoMpcRabatRacun.ToString("#0.00").Replace(",", ".");
                UkupnoRabatRacun = ukupnoRabatRacun.ToString("#0.00").Replace(",", ".");
                UkupnoOsnovicaRacun = ukupnoOsnovicaRacun.ToString("#0.00").Replace(",", ".");
                UkupnoPorezRacun = ukupnoPorezRacun.ToString("#0.00").Replace(",", ".");
            }

            string napomena = IspisNapomene().Trim();
            brRac = brojRacuna();
            ZadnjiRacun = brRac;
            string sql = "INSERT INTO racuni (broj_racuna,id_kupac,datum_racuna,id_ducan,id_kasa,id_blagajnik," +
                "gotovina,kartice,ukupno_gotovina,ukupno_kartice,broj_kartice_cashback,broj_kartice_bodovi," +
                "br_sa_prethodnog_racuna,ukupno,storno,dobiveno_gotovina,ukupno_virman,napomena,nacin_placanja,ukupno_ostalo," +
                "ukupno_povratna_naknada,ukupno_mpc,ukupno_vpc,ukupno_mpc_rabat,ukupno_rabat,ukupno_osnovica,ukupno_porez) " +
                "VALUES (" +
                "'" + brRac + "'," +
                "'" + sifraPartnera + "'," +
                "'" + dt + "'," +
                "'" + id_ducan + "'," +
                "'" + id_kasa + "'," +
                "'" + Properties.Settings.Default.id_zaposlenik + "'," +
                "'" + g + "'," +
                "'" + k + "'," +
                "'" + IznosGotovina + "'," +
                "'" + IznosKartica + "'," +
                "'" + txtBrojKarticeCB.Text + "'," +
                "'" + txtBrojKarticeSB.Text + "'," +
                "'" + txtBrojKarticePO.Text + "'," +
                "'" + uk1.ToString() + "'," +
                "'NE'," +
                "'" + dobiveno_gotovina + "'," +
                "'" + IznosVirman + "'," +
                "'" + napomena + "'," +
                "'" + placanje + "'," +
                "'0'," +
                "'" + PovratnaNaknadaUkRacun + "'," +
                "'" + UkupnoMpcRacun + "'," +
                "'" + UkupnoVpcRacun + "'," +
                "'" + UkupnoMpcRabatRacun + "'," +
                "'" + UkupnoRabatRacun + "'," +
                "'" + UkupnoOsnovicaRacun + "'," +
                "'" + UkupnoPorezRacun + "'" +
                ")";

            provjera_sql(classSQL.transaction(sql));

            //postavi broj_racuna na upravo uneseni račun
            for (int i = 0; i < DTsend.Rows.Count; i++)
            {
                DTsend.Rows[i].SetField("broj_racuna", brRac);
                DTsend.Rows[i].SetField("id_ducan", id_ducan);
                DTsend.Rows[i].SetField("id_kasa", id_kasa);
            }

            sifra_skladiste = "";
            provjera_sql(SQL.SQLracun.InsertStavke(DTsend));
            classSQL.transaction("COMMIT");

            string mali = DTpostavkePrinter.Rows[0]["posPrinterBool"].ToString();
            string a5 = classSQL.select_settings("SELECT a5print FROM postavke", "postavke").Tables[0].Rows[0]["a5print"].ToString();

            if (mali == "1")
            {
                DateTime[] datumi = new DateTime[2];
                datumi[0] = DateTime.Now;
                datumi[1] = datumi[0];

                try
                {
                    PosPrint.classPosPrintMaloprodaja2.BoolPreview = false;
                    PosPrint.classPosPrintMaloprodaja2.PrintReceipt(DTsend, blagajnik_ime, brRac + "/" +
                        datumi[0].Year.ToString(), sifraPartnera, barcode, brRac, placanje, datumi, false, mali, false, true, id_ducan, id_kasa);
                }
                catch (Exception)
                {
                    if (MessageBox.Show("Desila se pogreška kod ispisa na 'mali' pos printer.\r\n" +
                        "Želite li ispisati ovaj dokument na A4 format?", "Printer") == DialogResult.Yes)
                    {
                        printaj(brRac);
                    }
                }
            }
            else
            {
                DateTime[] datumi = new DateTime[2];
                datumi[0] = DateTime.Now;
                datumi[1] = datumi[0];
                PosPrint.classPosPrintMaloprodaja2.BoolPreview = false;
                PosPrint.classPosPrintMaloprodaja2.PrintReceipt(DTsend, blagajnik_ime, brRac + "/" +
                    datumi[0].Year.ToString(), sifraPartnera, barcode, brRac, placanje, datumi, false, mali, false, true, id_ducan, id_kasa);

                if (a5 == "1")
                {
                    printajA5(brRac);
                }
                else
                {
                    printaj(brRac);
                }
            }
        }

        private void printaj(string brRac)
        {
            Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
            rfak.dokumenat = "RAC";
            rfak.ImeForme = "Račun";
            rfak.naplatni = id_kasa;
            rfak.poslovnica = id_ducan;
            rfak.broj_dokumenta = brRac;
            rfak.ShowDialog();
        }

        private void printajA5(string brRac)
        {
            Report.A5racun.frmA5racun rfak = new Report.A5racun.frmA5racun();
            rfak.dokumenat = "RAC";
            rfak.ImeForme = "Račun";
            rfak.naplatni = id_kasa;
            rfak.poslovnica = id_ducan;
            rfak.broj_dokumenta = brRac;
            rfak.ShowDialog();
        }

        private string ReturnSifra(int row)
        {
            string vrati = "", sql = "";
            try
            {
                sql = "SELECT MAX(x.brart) FROM ( " +
                    "SELECT COALESCE(MAX(CASE WHEN LENGTH(substring(sifra, 12)) = 0 THEN 0 ELSE substring(sifra, 12)::NUMERIC end)::NUMERIC, 0) zbroj 1 AS brArt " +
                    "FROM roba r " +
                    "WHERE SUBSTRING(sifra, 1, 11) = '!serial" + Util.Korisno.GodinaKojaSeKoristiUbazi + "' " +
                    "UNION " +
                    "SELECT COALESCE(MAX(CASE WHEN LENGTH(substring(sifra_robe, 12)) = 0 THEN 0 ELSE substring(sifra_robe, 12)::NUMERIC end)::NUMERIC, 0) zbroj 1 AS brArt " +
                    "FROM racun_stavke " +
                    "WHERE SUBSTRING(sifra_robe, 1, 11) = '!serial" + Util.Korisno.GodinaKojaSeKoristiUbazi + "' " +
                    ") x;";

                DataTable DT = classSQL.select(sql, "roba").Tables[0];

                if (DT.Rows.Count > 0)
                {
                    string seri = DT.Rows[0][0].ToString();
                    vrati = "!serial" + Util.Korisno.GodinaKojaSeKoristiUbazi + DT.Rows[0][0].ToString();

                    sql = "INSERT INTO roba (naziv,id_grupa,jm,vpc,mpc,id_zemlja_porijekla,id_zemlja_uvoza,id_partner,id_manufacturers,sifra,ean,porez,oduzmi,nc,porez_potrosnja) " +
                    "VALUES (" +
                    "'" + dg(row, "naziv") + "'," +
                    "'1'," +
                    "'" + dg(row, "jmj") + "'," +
                    "'" + dg(row, "vpc").Replace(",", ".") + "'," +
                    "'" + dg(row, "cijena") + "'," +
                    "'60'," +
                    "'60'," +
                    "'1'," +
                    "'1'," +
                    "'" + vrati + "'," +
                    "'0'," +
                    "'" + dg(row, "porez") + "'," +
                    "'NE'," +
                    "'0'," +
                    "'0'" +
                    ")";

                    classSQL.insert(sql);
                }
                else
                {
                    vrati = "!serial" + Util.Korisno.GodinaKojaSeKoristiUbazi + "1";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return vrati;
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle,
                System.Globalization.CultureInfo.CurrentCulture, out result);
        }

        private void CheckPosEquipment(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str, "Greška");
            }
        }

        #endregion Util

        #region buttons

        private void button15_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Jeste li sigurni da želite odustati?", "Odustani", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                brRac = brojRacuna();
                lblBrojRac.Text = "Broj računa: " + brRac + "/" + DateTime.Now.Year;
                SetOnNull();
                Properties.Settings.Default.id_partner = "";
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            txtImePartnera.Text = "";
        }

        private void btnPartner_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    sifraPartnera = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtImePartnera.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                }
                else
                {
                    sifraPartnera = "0";
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void btnGotovina_Click(object sender, EventArgs e)
        {
            #region PROVJERAVA DALI JE DOBRA GODINA

            try
            {
                PCPOS.Util.classFukcijeZaUpravljanjeBazom B = new Util.classFukcijeZaUpravljanjeBazom("MALOPRODAJA", "POS");
                int g = B.UzmiGodinuKojaSeKoristi();
                if (g != DateTime.Now.Year)
                {
                    if (g != 0)
                    {
                        MessageBox.Show("UPOZORENJE!!!\r\nVrlo vjerovatno koristite krivu godinu za izradu računa!\r\n" +
                            "Zakonom o fiskalizaciji to nije dozvoljeno.\r\n Za više informacija kontaktirajte Code-iT.\r\n", "Upozorenje.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            catch
            {
            }

            #endregion PROVJERAVA DALI JE DOBRA GODINA

            Ukupno();
            int br = dgv.Rows.Count;

            for (int i = 0; i < br; i++)
            {
                if (dgv.Rows[i].Cells["NAZIV"].Value.ToString().Trim() == "")
                {
                    dgv.Rows.RemoveAt(i);
                    br = dgv.Rows.Count;
                    i -= 1;
                }
            }

            if (dgv.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku.", "Greška");
                return;
            }
            if (ukupno == 0)
            {
                MessageBox.Show("Ne možete fiskalizirati račun sa 0kn.", "Greška");
                return;
            }

            frmUzvratiti objForm2 = new frmUzvratiti();
            IznosGotovina = null;
            IznosKartica = null;
            objForm2.getUkupnoKasa = ukupno.ToString();
            objForm2.getNacin = "GO";
            objForm2.MainFormParagonac = this;
            objForm2.ShowDialog();

            if (IznosKartica != null && IznosGotovina != null)
            {
                SpremiRacun(IznosKartica, IznosGotovina);
                NoviUnos();
            }
        }

        private void btnKartica_Click(object sender, EventArgs e)
        {
            #region PROVJERAVA DALI JE DOBRA GODINA

            try
            {
                PCPOS.Util.classFukcijeZaUpravljanjeBazom B = new Util.classFukcijeZaUpravljanjeBazom("MALOPRODAJA", "POS");
                int g = B.UzmiGodinuKojaSeKoristi();
                if (g != DateTime.Now.Year)
                {
                    if (g != 0)
                    {
                        MessageBox.Show("UPOZORENJE!!!\r\nVrlo vjerovatno koristite krivu godinu za izradu računa!\r\n" +
                            "Zakonom o fiskalizaciji to nije dozvoljeno.\r\n Za više informacija kontaktirajte Code-iT.\r\n", "Upozorenje.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            catch
            {
            }

            #endregion PROVJERAVA DALI JE DOBRA GODINA

            Ukupno();
            int br = dgv.Rows.Count;

            for (int i = 0; i < br; i++)
            {
                if (dgv.Rows[i].Cells["NAZIV"].Value.ToString().Trim() == "")
                {
                    dgv.Rows.RemoveAt(i);
                    br = dgv.Rows.Count;
                    i -= 1;
                }
            }

            if (dgv.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku.", "Greška");
                return;
            }
            if (ukupno == 0)
            {
                MessageBox.Show("Ne možete fiskalizirati račun sa 0kn.", "Greška");
                return;
            }

            frmUzvratiti objForm2 = new frmUzvratiti();
            objForm2.getUkupnoKasa = ukupno.ToString();
            objForm2.getNacin = "KA";
            objForm2.MainFormParagonac = this;
            //objForm2.MainForm = this;
            objForm2.ShowDialog();

            if (IznosKartica != null && IznosGotovina != null)
            {
                SpremiRacun(IznosKartica, IznosGotovina);
                NoviUnos();
            }
        }

        private void btnOdustaniCB_Click(object sender, EventArgs e)
        {
            txtBrojKarticeCB.Text = "";
            txtPodaciOvlasnikuCB.Text = "";
        }

        private void btnOdustaniSB_Click(object sender, EventArgs e)
        {
            txtBrojKarticeSB.Text = "";
            txtPodaciOvlasnikuSB.Text = "";
        }

        private void btnOdustaniPO_Click(object sender, EventArgs e)
        {
            txtBrojKarticePO.Text = "";
            txtPodaciOvlasnikuPO.Text = "";
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            frmRobaTrazi roba = new frmRobaTrazi();
            roba.ShowDialog();

            if (Properties.Settings.Default.id_roba != "")
            {
                DataTable DTroba = classSQL.select("SELECT * FROM roba WHERE sifra='" + Properties.Settings.Default.id_roba + "'", "roba").Tables[0];
                dgv.Rows.Add(
                "",
                DTroba.Rows[0]["naziv"].ToString(),
                DTroba.Rows[0]["jm"].ToString(),
                Convert.ToDecimal(DTroba.Rows[0]["mpc"].ToString()).ToString("#0.00"),
                "1,00",
                "0,00",
                DTroba.Rows[0]["porez"].ToString(),
                Convert.ToDecimal(DTroba.Rows[0]["mpc"].ToString()).ToString("#0.00"),
                DTroba.Rows[0]["vpc"].ToString());

                RedniBroj();
                PaintRows(dgv);
            }

            Ukupno();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                Kasa.frmPromjenaCijene pr = new Kasa.frmPromjenaCijene();
                pr.sifra = dgv.CurrentRow.Cells["sifra"].FormattedValue.ToString();
                pr.porez = dgv.CurrentRow.Cells["porez"].FormattedValue.ToString();
                pr.ShowDialog();
            }
            else
            {
                MessageBox.Show("Nemate niti jednu stavku.");
            }
        }

        private void btnOdjava_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAlati_Click(object sender, EventArgs e)
        {
            frmKasaOpcije ko = new frmKasaOpcije();
            ko.sifraPartnera = sifraPartnera;
            ko.ShowDialog();
        }

        private void btnObrisiStavku_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount < 1)
            {
                MessageBox.Show("Nemate niti jednu stavku.", "Greška");
                return;
            }
            //else if (dgv.RowCount == 1)
            //{
            //    int current = dgv.CurrentRow.Index;
            //    //int ukupnoRows= dgv.Rows.Count;
            //    //dgv.Rows[ukupnoRows-1].Cells[0].Selected = true;
            //    dgv.Rows.RemoveAt(current);
            //    Ukupno();
            //    txtUnos.Select();
            //}
            else
            {
                int current = dgv.CurrentRow.Index;
                //int ukupnoRows= dgv.Rows.Count;
                //dgv.Rows[ukupnoRows-1].Cells[0].Selected = true;
                dgv.Rows.RemoveAt(current);
                Ukupno();
            }
        }

        #endregion buttons

        #region Datagridview helpers

        private string dg(int row, string cell)
        {
            return dgv.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        private void dgv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //if(dgv.CurrentCell.ColumnIndex==3)
            //{
            //    var txtEdit = (ComboBox)e.Control;
            //    txtEdit.KeyDown += EditKeyDown;
            //}
            //else
            //{
            //    var txtEdit = (TextBox)e.Control;
            //    txtEdit.KeyDown += EditKeyDown;
            //}
        }

        private void dgv_KeyDown(object sender, KeyEventArgs e)
        {
            //if (dgv.RowCount > 0)
            //{
            //    if (e.KeyCode == Keys.Up)
            //    {
            //        int cr = dgv.CurrentRow.Index-1;
            //        dgv.Rows[cr].Selected = true;
            //    }

            //    if (e.KeyCode == Keys.Down)
            //    {
            //        int cr = dgv.CurrentRow.Index+1;
            //        dgv.Rows[cr].Selected = true;
            //    }
            //}
        }

        #endregion Datagridview helpers

        #region Form components

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w + 3, h + 3);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w - 3, h - 3);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 500);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        #endregion Form components

        /// <summary>
        /// Ako je uključeno ispisivanje napomene na računu onda vraća napomenu iz male forme
        /// </summary>
        /// <returns></returns>

        private void btnInsert_Click(object sender, EventArgs e)
        {
            dgv.Rows.Add("", "", "kom", "0,00", "1", "0,00", porez_zadano, "0,00", "0,00");
            dgv.Select();
            dgv.CurrentCell = dgv.Rows[dgv.RowCount - 1].Cells[1];
            dgv.BeginEdit(true);
            RedniBroj();
            PaintRows(dgv);
        }

        private void RedniBroj()
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                dgv.Rows[i].Cells["RB"].Value = i + 1;
            }
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            izracun();
        }

        private void frmParagonac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Insert)
            {
                btnInsert.PerformClick();
            }
            else if (e.KeyData == Keys.F3)
            {
                btnTrazi.PerformClick();
            }
            else if (e.KeyData == Keys.F4)
            {
                btnPartner.PerformClick();
            }
            else if (e.KeyData == Keys.F5)
            {
                if (Class.Postavke.maloprodaja_naplata_gotovina_button_show)
                {
                    btnGotovina.PerformClick();
                }
            }
            else if (e.KeyData == Keys.F6)
            {
                if (Class.Postavke.maloprodaja_naplata_kartica_button_show)
                {
                    btnKartica.PerformClick();
                }
            }
            else if (e.KeyData == Keys.F7)
            {
                btnOdustaniSve.PerformClick();
            }
            else if (e.KeyData == Keys.F9)
            {
                btnObrisiStavku.PerformClick();
            }
            else if (e.KeyData == Keys.F10)
            {
                btnAlati.PerformClick();
            }
            else if (e.KeyData == Keys.Escape)
            {
                btnOdjava.PerformClick();
            }
            else if (e.KeyData == Keys.Delete)
            {
                btnObrisiStavku.PerformClick();
            }
        }

        private void btnPregledRacuna_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku.", "Greška");
                return;
            }

            DTsend = new DataTable();
            DTsend.Columns.Add("broj_racuna");
            DTsend.Columns.Add("sifra_robe");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("mpc");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("nbc");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("cijena");
            DTsend.Columns.Add("ime");
            DTsend.Columns.Add("porez_potrosnja");
            DTsend.Columns.Add("povratna_naknada");
            DTsend.Columns.Add("porez_na_dohodak");
            DTsend.Columns.Add("prirez");
            DTsend.Columns.Add("porez_na_dohodak_iznos");
            DTsend.Columns.Add("prirez_iznos");
            DataRow row;

            DataTable DTtemp;

            string g;
            string k;

            string iznosGotovinaTest = lblUkupno.Text.Replace("Kn", "").Trim();
            string IznosKarticaTest = "0";
            string DobivenoGotovinaTest = "0";

            if (iznosGotovinaTest != null && iznosGotovinaTest != "0")
            {
                g = "1";
            }
            else
            {
                g = "0";
            }

            if (IznosKarticaTest != null && IznosKarticaTest != "0")
            {
                k = "1";
            }
            else
            {
                k = "0";
            }

            placanje = "O";

            if (Convert.ToDecimal(iznosGotovinaTest) == 0 && Convert.ToDecimal(IznosKarticaTest) > 0)
            {
                placanje = "K";
            }
            else if (Convert.ToDecimal(iznosGotovinaTest) > 0 && Convert.ToDecimal(IznosKarticaTest) == 0)
            {
                placanje = "G";
            }
            else if (Convert.ToDecimal(iznosGotovinaTest) > 0 && Convert.ToDecimal(IznosKarticaTest) > 0)
            {
                placanje = "O";
            }

            //trenutno nije implementirano
            IznosVirman = "0.00";
            //trenutno nije implementirano

            DateTime dRac = Convert.ToDateTime(DateTime.Now);
            string dt = dRac.ToString("yyyy-MM-dd H:mm:ss");

            string uk1 = ukupno.ToString();
            string dobiveno_gotovina;
            if (classSQL.remoteConnectionString == "")
            {
                uk1 = uk1.Replace(",", ".");
                iznosGotovinaTest = iznosGotovinaTest.Replace(",", ".");
                IznosKarticaTest = IznosKarticaTest.Replace(",", ".");
                IznosVirman = IznosKartica.Replace(",", ".");
                dobiveno_gotovina = DobivenoGotovina.Replace(",", ".");
            }
            else
            {
                iznosGotovinaTest = iznosGotovinaTest.Replace(".", ",");
                IznosKarticaTest = IznosKarticaTest.Replace(".", ",");
                IznosVirman = IznosVirman.Replace(".", ",");
                uk1 = uk1.Replace(".", ",");
                dobiveno_gotovina = DobivenoGotovinaTest.Replace(".", ",");
            }

            string napomena = IspisNapomene().Trim();
            brRac = brojRacuna();
            ZadnjiRacun = brRac;

            string sql = "DELETE FROM ispis_racuni; DELETE FROM ispis_racun_stavke;";
            provjera_sql(classSQL.update(sql));

            sql = "INSERT INTO ispis_racuni (broj_racuna,id_kupac,datum_racuna,id_ducan,id_kasa,id_blagajnik," +
                "gotovina,kartice,ukupno_gotovina,ukupno_kartice,broj_kartice_cashback,broj_kartice_bodovi," +
                "br_sa_prethodnog_racuna,ukupno,storno,dobiveno_gotovina,ukupno_virman,napomena,nacin_placanja) " +
                "VALUES (" +
                "'" + brRac + "'," +
                "'" + sifraPartnera + "'," +
                "'" + dt + "'," +
                "'" + id_ducan + "'," +
                "'" + id_kasa + "'," +
                "'" + Properties.Settings.Default.id_zaposlenik + "'," +
                "'" + g + "'," +
                "'" + k + "'," +
                "'" + iznosGotovinaTest + "'," +
                "'" + IznosKarticaTest + "'," +
                "'" + txtBrojKarticeCB.Text + "'," +
                "'" + txtBrojKarticeSB.Text + "'," +
                "'" + txtBrojKarticePO.Text + "'," +
                "'" + uk1.ToString() + "'," +
                "'NE'," +
                "'" + dobiveno_gotovina + "'," +
                "'" + IznosVirman + "'," +
                "'" + napomena + "'," +
                "'" + placanje + "'" +
                ")";

            provjera_sql(classSQL.insert(sql));

            double kolNaknada;
            double povratnaNaknada;
            //--------STAVKE-------------
            string sifra = "";
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                sifra = dg(i, "rb");

                row = DTsend.NewRow();
                row["broj_racuna"] = brRac;
                //row["sifra_robe"] = ReturnSifra(sifra);
                //row["id_skladiste"] = dgv.Rows[i].Cells["skladiste"].Value;
                //row["mpc"] = dg(i, "cijena");
                //row["porez"] = dg(i, "porez");
                row["kolicina"] = dg(i, "kolicina");
                row["rabat"] = dg(i, "popust");
                //row["vpc"] = dg(i, "vpc");
                //row["nbc"] = dg(i, "nbc");
                //row["cijena"] = dg(i, "cijena");
                row["ime"] = dg(i, "naziv");
                row["porez_potrosnja"] = "0";

                DTtemp = classSQL.select("SELECT iznos FROM povratna_naknada WHERE sifra='" + sifra + "'", "povratna_naknada")
                    .Tables[0];

                //ako postoji povratna naknada na proizvod
                if (DTtemp.Rows.Count > 0)
                {
                    kolNaknada = Convert.ToDouble(dg(i, "kolicina"));
                    povratnaNaknada = Convert.ToDouble(DTtemp.Rows[0]["iznos"].ToString()) * kolNaknada;
                }
                //ako ne postoji onda je 0
                else
                {
                    povratnaNaknada = 0;
                }

                if (classSQL.remoteConnectionString == "")
                {
                    row["mpc"] = dg(i, "cijena").Replace(",", ".");
                    row["porez"] = dg(i, "porez").Replace(",", ".");
                    row["vpc"] = dg(i, "vpc").Replace(",", ".");
                    //row["nbc"] = dg(i, "nbc").Replace(",", ".");
                    row["cijena"] = dg(i, "cijena").Replace(",", ".");
                    row["povratna_naknada"] = povratnaNaknada.ToString("#0.00").Replace(",", ".");
                }
                else
                {
                    row["mpc"] = dg(i, "cijena").Replace(".", ",");
                    row["porez"] = dg(i, "porez").Replace(".", ",");
                    row["vpc"] = dg(i, "vpc").Replace(".", ",");
                    //row["nbc"] = dg(i, "nbc").Replace(".", ",");
                    row["cijena"] = dg(i, "cijena").Replace(".", ",");
                    row["povratna_naknada"] = povratnaNaknada.ToString("#0.00").Replace(".", ",");
                }

                DTsend.Rows.Add(row);

                sql = "INSERT INTO ispis_racun_stavke (broj_racuna,sifra_robe,id_skladiste,mpc,porez,kolicina,rabat,vpc,nbc,porez_potrosnja,povratna_naknada)" +
                             " VALUES (" +
                            "'" + DTsend.Rows[i]["broj_racuna"].ToString() + "'," +
                            "'0'," +
                             "'0'," +
                            "'" + DTsend.Rows[i]["mpc"].ToString() + "'," +
                            "'" + DTsend.Rows[i]["porez"].ToString() + "'," +
                             "'" + DTsend.Rows[i]["kolicina"].ToString() + "'," +
                            "'" + DTsend.Rows[i]["rabat"].ToString() + "'," +
                            "'" + DTsend.Rows[i]["vpc"].ToString().Replace(",", ".") + "'," +
                            "'" + DTsend.Rows[i]["nbc"].ToString() + "'," +
                            "'" + DTsend.Rows[i]["porez_potrosnja"].ToString() + "'," +
                            "'" + DTsend.Rows[i]["povratna_naknada"].ToString().Replace(",", ".") + "'" +
                             ")";

                classSQL.select(sql, "ispis_racun_stavke");
            }

            //barcode = "000" + brRac + id_ducan + id_kasa + DateTime.Now.Year.ToString().Remove(0, 2);

            DateTime[] datumi = new DateTime[2];
            datumi[0] = DateTime.Now;
            datumi[1] = datumi[0];

            string mali = DTpostavkePrinter.Rows[0]["posPrinterBool"].ToString();
            string a5 = classSQL.select_settings("SELECT a5print FROM postavke", "postavke").Tables[0].Rows[0]["a5print"].ToString();

            try
            {
                PosPrint.classPosPrintMaloprodaja2.BoolPreview = true;
                PosPrint.classPosPrintMaloprodaja2.PrintReceipt(DTsend, blagajnik_ime, brRac + "/" +
                    datumi[0].Year.ToString(), sifraPartnera, barcode, brRac, placanje, datumi, true, mali, true, true, "", "");

                if (mali == "1")
                {
                    //već je isprintan u gornjoj metodi
                }
                else if (a5 == "1")
                {
                    printajA5(brRac, true);
                }
                else if (mali != "1")
                {
                    printaj(brRac, true);
                }
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("Desila se pogreška kod ispisa na 'mali' pos printer.\r\n" +
                    "Želite li ispisati ovaj dokument na A4 format?" + Environment.NewLine + Environment.NewLine
                    + ex.Message, "Printer") == DialogResult.Yes)
                {
                    printaj(brRac, true);
                }
            }
        }

        private void printaj(string brRac, bool testniIspis)
        {
            Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
            rfak.dokumenat = "RAC";
            rfak.samoIspis = testniIspis;
            rfak.ImeForme = "Račun";
            rfak.broj_dokumenta = brRac;
            rfak.ShowDialog();
        }

        private void printajA5(string brRac, bool testniIspis)
        {
            Report.A5racun.frmA5racun rfak = new Report.A5racun.frmA5racun();
            rfak.dokumenat = "RAC";
            rfak.samoIspis = testniIspis;
            rfak.ImeForme = "Račun";
            rfak.broj_dokumenta = brRac;
            rfak.ShowDialog();
        }

        private string ReturnSifra(string sifra)
        {
            try
            {
                if (sifra.Length > 3)
                {
                    if (DTpromocije1.Rows[0]["aktivnost"].ToString() == "DA" && sifra.Substring(0, 3) == "000")
                    {
                        return "00000";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return sifra;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Sifarnik.frmBrziUpisKlijenta a = new Sifarnik.frmBrziUpisKlijenta();
            a.MainForm = this;
            Properties.Settings.Default.id_partner = "";
            a.ShowDialog();

            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    sifraPartnera = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtImePartnera.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                }
                else
                {
                    sifraPartnera = "0";
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }
    }
}