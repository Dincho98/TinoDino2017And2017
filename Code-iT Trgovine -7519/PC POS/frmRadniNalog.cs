using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmRadniNalog : Form
    {
        public string broj_RN_edit { get; set; }
        private DataTable DTRoba = new DataTable();
        private DataSet DS_Skladiste = new DataSet();
        private DataSet DSRoba = new DataSet();
        private DataSet DS_Zaposlenik = new DataSet();
        private DataSet DSValuta = new DataSet();
        private DataSet DSMT = new DataSet();
        private DataSet DSnazivPlacanja = new DataSet();
        private DataSet DSvd = new DataSet();
        private DataTable DTOtprema = new DataTable();
        private DataTable DTpostavke = new DataTable();
        private DataTable DSponude = new DataTable();
        private DataTable StavkeRadnogNaloga = new DataTable();
        private BindingSource skladisteBindingSource = new BindingSource();
        private BindingSource RnBindingSource = new BindingSource();
        private bool edit = false;
        public frmMenu MainFormMenu { get; set; }
        public frmMenu MainForm { get; set; }

        private string[] oibs = new string[1] { "33376995853" };

        public frmRadniNalog()
        {
            InitializeComponent();
        }

        private void frmRadniNalog_Load(object sender, EventArgs e)
        {
            MyDataGrid.MainForm = this;
            ControlDisableEnable(false, true, true, false, true);
            numeric();
            fillComboBox();
            ttxBrojPonude.Text = brojRadnogNaloga();
            EnableDisable(false);
            ttxBrojPonude.Select();
            DGVCREATE();
            DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
            btnSpremi.Enabled = false;
            ControlDisableEnable(true, false, false, true, false);
            if (broj_RN_edit != null) { FillRn(); edit = true; }
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 1000);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        //private void dgw_CellValidated(object sender, DataGridViewCellEventArgs e)
        //{
        //    int row = dgw.CurrentCell.RowIndex;
        //    if (dgw.CurrentCell.ColumnIndex == 2)
        //    {
        //        SetCijenaSkladiste();
        //    }

        //    else if (dgw.CurrentCell.ColumnIndex == 6)
        //    {
        //        if (dgw.CurrentRow.Cells["skladiste"].Value == null)
        //        {
        //            MessageBox.Show("Niste odabrali skladište", "Greška");
        //            return;
        //        }

        //        dgw.CurrentCell.Selected = false;
        //        txtSifra_robe.Text = "";
        //        txtSifra_robe.BackColor = Color.Silver;
        //        txtSifra_robe.Select();
        //    }
        //    izracun();
        //}

        #region Util

        private void EnableDisable(bool x)
        {
            cbVD.Enabled = x;
            txtSifraOdrediste.Enabled = x;
            txtPartnerNaziv.Enabled = x;
            rtbNapomena.Enabled = x;
            txtSifra_robe.Enabled = x;
            btnPartner.Enabled = x;
            cbMjestoTroska.Enabled = x;
            btnObrisi.Enabled = x;
            btnOpenRoba.Enabled = x;
            dtpDatumNaloga.Enabled = x;
            dtpDatumPrimitka.Enabled = x;
            dtpDatumZavrsetka.Enabled = x;
            cbIzvrsio.Enabled = x;
            nmGodinaPonude.Enabled = x;

            if (oibs.Contains(Class.PodaciTvrtka.oibTvrtke))
            {
                txtIzvrsio.Enabled = true;
                txtIzvrsio.Visible = true;
                cbIzvrsio.Enabled = false;
                cbIzvrsio.Visible = false;
            }
            else
            {
                txtIzvrsio.Enabled = false;
                txtIzvrsio.Visible = false;
                cbIzvrsio.Enabled = true;
                cbIzvrsio.Visible = true;
            }
            if (oibs.Contains(Class.PodaciTvrtka.oibTvrtke))
            {
                txtIzvrsio.Enabled = x;
            }
            else
            {
                cbIzvrsio.Enabled = x;
            }
        }

        private void numeric()
        {
            nmGodinaPonude.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nmGodinaPonude.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nmGodinaPonude.Value = DateTime.Now.Year;
        }

        private string brojRadnogNaloga()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj_naloga) FROM radni_nalog", "radni_nalog").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void SetCijenaSkladiste()
        {
            if (dgw.CurrentRow.Cells["skladiste"].Value != null)
            {
                DataSet dsRobaProdaja = new DataSet();
                dsRobaProdaja = classSQL.select("SELECT * FROM roba_prodaja WHERE id_skladiste='" + dgw.CurrentRow.Cells["skladiste"].Value + "' AND sifra='" + dgw.CurrentRow.Cells["sifra"].FormattedValue.ToString() + "'", "roba_prodaja");
                if (dsRobaProdaja.Tables[0].Rows.Count > 0)
                {

                    decimal _NBC = Util.Korisno.VratiNabavnuCijenu(dgw.CurrentRow.Cells["sifra"].FormattedValue.ToString(), dgw.CurrentRow.Cells["skladiste"].Value.ToString());
                    if (Convert.ToDouble(dsRobaProdaja.Tables[0].Rows[0]["kolicina"].ToString()) > 0)
                    {
                        double mpc = (Convert.ToDouble(dsRobaProdaja.Tables[0].Rows[0]["vpc"]) * Convert.ToDouble(dsRobaProdaja.Tables[0].Rows[0]["porez"]) / 100) + Convert.ToDouble(dsRobaProdaja.Tables[0].Rows[0]["vpc"]);
                        if (Class.Postavke.proizvodnja_normativ_pc)
                        {
                            _NBC = Convert.ToDecimal(dsRobaProdaja.Tables[0].Rows[0]["proizvodacka_cijena"]);
                            mpc = Convert.ToDouble(dsRobaProdaja.Tables[0].Rows[0]["nc"]);
                        }

                        if (Class.Postavke.proizvodnja_normativ_pc)
                        {
                            _NBC = Convert.ToDecimal(dsRobaProdaja.Tables[0].Rows[0]["proizvodacka_cijena"].ToString());

                            string sqlProizvodjackaCijena = string.Format(@"select sum(r.proizvodacka_cijena * replace(ns.kolicina, ',','.')::numeric) as pc
from normativi_stavke ns
left join normativi n on ns.broj_normativa = n.broj_normativa
left join roba r on ns.sifra_robe = r.sifra
where n.sifra_artikla = '{0}';", dsRobaProdaja.Tables[0].Rows[0]["sifra"].ToString());

                            DataSet dsProizvodjackaCijena = classSQL.select(sqlProizvodjackaCijena, "ProizvodjackaCijena");

                            if (dsProizvodjackaCijena != null && dsProizvodjackaCijena.Tables.Count > 0 && dsProizvodjackaCijena.Tables[0] != null && dsProizvodjackaCijena.Tables[0].Rows.Count > 0)
                            {

                                decimal ProizvodjackaCijena = 0;

                                decimal.TryParse(dsProizvodjackaCijena.Tables[0].Rows[0]["pc"].ToString(), out ProizvodjackaCijena);

                                if (ProizvodjackaCijena != 0 && ProizvodjackaCijena != _NBC)
                                {
                                    _NBC = ProizvodjackaCijena;
                                }

                            }
                            if (_NBC == 0)
                            {
                                //_NBC = Util.Korisno.VratiNabavnuCijenu(DTRoba.Rows[0]["sifra"].ToString(), DTpostavke.Rows[0]["default_skladiste"].ToString());
                            }
                        }

                        dgw.CurrentRow.Cells["nbc"].Value = _NBC.ToString("#0.00");
                        dgw.CurrentRow.Cells["mpc"].Value = Math.Round(mpc, 2).ToString("#0.00");
                        //lblNaDan.ForeColor = Color.Green;
                        //lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima " + dsRobaProdaja.Tables[0].Rows[0]["kolicina"].ToString() + " " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
                    }
                    else
                    {
                        double mpc = (Convert.ToDouble(dsRobaProdaja.Tables[0].Rows[0]["vpc"]) * Convert.ToDouble(dsRobaProdaja.Tables[0].Rows[0]["porez"]) / 100) + Convert.ToDouble(dsRobaProdaja.Tables[0].Rows[0]["vpc"]);
                        if (Class.Postavke.proizvodnja_normativ_pc)
                        {
                            _NBC = Convert.ToDecimal(dsRobaProdaja.Tables[0].Rows[0]["proizvodacka_cijena"]);
                            mpc = Convert.ToDouble(dsRobaProdaja.Tables[0].Rows[0]["nc"]);
                        }
                        if (Class.Postavke.proizvodnja_normativ_pc)
                        {
                            _NBC = Convert.ToDecimal(dsRobaProdaja.Tables[0].Rows[0]["proizvodacka_cijena"].ToString());

                            string sqlProizvodjackaCijena = string.Format(@"select sum(r.proizvodacka_cijena * replace(ns.kolicina, ',','.')::numeric) as pc
from normativi_stavke ns
left join normativi n on ns.broj_normativa = n.broj_normativa
left join roba r on ns.sifra_robe = r.sifra
where n.sifra_artikla = '{0}';", dsRobaProdaja.Tables[0].Rows[0]["sifra"].ToString());

                            DataSet dsProizvodjackaCijena = classSQL.select(sqlProizvodjackaCijena, "ProizvodjackaCijena");

                            if (dsProizvodjackaCijena != null && dsProizvodjackaCijena.Tables.Count > 0 && dsProizvodjackaCijena.Tables[0] != null && dsProizvodjackaCijena.Tables[0].Rows.Count > 0)
                            {

                                decimal ProizvodjackaCijena = 0;

                                decimal.TryParse(dsProizvodjackaCijena.Tables[0].Rows[0]["pc"].ToString(), out ProizvodjackaCijena);

                                if (ProizvodjackaCijena != 0 && ProizvodjackaCijena != _NBC)
                                {
                                    _NBC = ProizvodjackaCijena;
                                }

                            }
                            if (_NBC == 0)
                            {
                                //_NBC = Util.Korisno.VratiNabavnuCijenu(DTRoba.Rows[0]["sifra"].ToString(), DTpostavke.Rows[0]["default_skladiste"].ToString());
                            }
                        }
                        dgw.CurrentRow.Cells["nbc"].Value = _NBC.ToString("#0.00");
                        dgw.CurrentRow.Cells["mpc"].Value = Math.Round(mpc, 2).ToString("#0.00");
                        //lblNaDan.ForeColor = Color.Red;
                        //lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima " + dsRobaProdaja.Tables[0].Rows[0]["kolicina"].ToString() + " " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
                    }
                }
                else
                {
                    //lblNaDan.ForeColor = Color.Red;
                    //lblNaDan.Text = "Na dan " + DateTime.Now.ToString() + " na skladištu ima 0,00 " + dgw.CurrentRow.Cells["jmj"].FormattedValue.ToString();
                }
            }
        }

        private void deleteFields()
        {
            txtSifraOdrediste.Text = "";
            txtPartnerNaziv.Text = "";
            rtbNapomena.Text = "";
            txtSifra_robe.Text = "";
            txtIzvrsio.Text = "";
            dgw.Rows.Clear();
        }

        private void SetRoba()
        {
            dgw.Rows.Add();
            int br = dgw.Rows.Count - 1;

            decimal _NBC = Util.Korisno.VratiNabavnuCijenu(DTRoba.Rows[0]["sifra"].ToString(), DTpostavke.Rows[0]["default_skladiste"].ToString());
            if (Class.Postavke.proizvodnja_normativ_pc)
            {
                _NBC = Convert.ToDecimal(DTRoba.Rows[0]["proizvodacka_cijena"].ToString());

                string sqlProizvodjackaCijena = string.Format(@"select sum(r.proizvodacka_cijena * replace(ns.kolicina, ',','.')::numeric) as pc
from normativi_stavke ns
left join normativi n on ns.broj_normativa = n.broj_normativa
left join roba r on ns.sifra_robe = r.sifra
where n.sifra_artikla = '{0}';", DTRoba.Rows[0]["sifra"].ToString());

                DataSet dsProizvodjackaCijena = classSQL.select(sqlProizvodjackaCijena, "ProizvodjackaCijena");

                if (dsProizvodjackaCijena != null && dsProizvodjackaCijena.Tables.Count > 0 && dsProizvodjackaCijena.Tables[0] != null && dsProizvodjackaCijena.Tables[0].Rows.Count > 0)
                {

                    decimal ProizvodjackaCijena = 0;

                    decimal.TryParse(dsProizvodjackaCijena.Tables[0].Rows[0]["pc"].ToString(), out ProizvodjackaCijena);

                    if (ProizvodjackaCijena != 0 && ProizvodjackaCijena != _NBC)
                    {
                        _NBC = ProizvodjackaCijena;
                    }

                }
                if (_NBC == 0)
                {
                    //_NBC = Util.Korisno.VratiNabavnuCijenu(DTRoba.Rows[0]["sifra"].ToString(), DTpostavke.Rows[0]["default_skladiste"].ToString());
                }
            }

            dgw.Rows[br].Cells["sifra"].Value = DTRoba.Rows[0]["sifra"].ToString();
            dgw.Rows[br].Cells["naziv"].Value = DTRoba.Rows[0]["naziv"].ToString();
            dgw.Rows[br].Cells["kolicina"].Value = "1";
            dgw.Rows[br].Cells["nbc"].Value = Math.Round(_NBC, 4).ToString("#0.000");
            dgw.Rows[br].Cells["iznos"].Value = Math.Round(Convert.ToDouble(DTRoba.Rows[0]["nc"].ToString()), 2).ToString("#0.00");
            dgw.Rows[br].Cells["mpc"].Value = Math.Round(Convert.ToDouble((Class.Postavke.proizvodnja_normativ_pc ? DTRoba.Rows[0]["nc"].ToString() : DTRoba.Rows[0]["mpc"].ToString())), 2).ToString("#0.00");
            dgw.Rows[br].Cells["iznos1"].Value = Math.Round(Convert.ToDouble((Class.Postavke.proizvodnja_normativ_pc ? DTRoba.Rows[0]["nc"].ToString() : DTRoba.Rows[0]["mpc"].ToString())), 2).ToString("#0.00");
            dgw.Rows[br].Cells["vpc"].Value = Math.Round(Convert.ToDouble(DTRoba.Rows[0]["vpc"].ToString()), 2).ToString("#0.00");
            dgw.Rows[br].Cells["porez"].Value = Math.Round(Convert.ToDouble(DTRoba.Rows[0]["porez"].ToString()), 2).ToString("#0.00");
            dgw.Rows[br].Cells["oduzmi"].Value = DTRoba.Rows[0]["oduzmi"].ToString();
            dgw.Rows[br].Cells["skladiste"].Value = DTpostavke.Rows[0]["default_skladiste"].ToString();

            //PaintRows(dgw);
            izracun();

            dgw.Select();
            dgw.CurrentCell = dgw.Rows[br].Cells[2];
            dgw.BeginEdit(true);
        }

        private void EnterDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            if (d.CurrentCell.ColumnIndex == 2)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 3)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 4)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                int curent = d.CurrentRow.Index;
                txtSifra_robe.Text = "";
                txtSifra_robe.Focus();
            }
        }

        private void RightDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            if (d.CurrentCell.ColumnIndex == 2)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 3)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 4)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[5];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                int curent = d.CurrentRow.Index;
                txtSifra_robe.Text = "";
                txtSifra_robe.Focus();
            }
        }

        private void UpDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            int curent = d.CurrentRow.Index;
            if (d.CurrentCell.ColumnIndex == 3)
            {
            }
            else if (curent == 0)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index - 1].Cells[3];
                d.BeginEdit(true);
            }
        }

        private void DownDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            int curent = d.CurrentRow.Index;
            if (d.CurrentCell.ColumnIndex == 3)
            {
                SendKeys.Send("{F4}");
            }
            else if (curent == d.RowCount - 1)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
                d.BeginEdit(true);
            }
            else
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index + 1].Cells[3];
                d.BeginEdit(true);
            }
        }

        private void LeftDGW(DataGridView d)
        {
            if (d.Rows.Count < 1)
                return;
            if (d.CurrentCell.ColumnIndex == 2)
            {
                int curent = d.CurrentRow.Index;
                txtSifra_robe.Text = "";
                txtSifra_robe.Focus();
            }
            if (d.CurrentCell.ColumnIndex == 3)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[2];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 4)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[3];
                d.BeginEdit(true);
            }
            else if (d.CurrentCell.ColumnIndex == 5)
            {
                d.CurrentCell = dgw.Rows[d.CurrentRow.Index].Cells[4];
                d.BeginEdit(true);
            }
        }

        private class MyDataGrid : System.Windows.Forms.DataGridView
        {
            public static frmRadniNalog MainForm { get; set; }

            protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
            {
                if (keyData == Keys.Enter)
                {
                    MainForm.EnterDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Right)
                {
                    MainForm.RightDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Left)
                {
                    MainForm.LeftDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Up)
                {
                    MainForm.UpDGW(MainForm.dgw);
                    return true;
                }
                else if (keyData == Keys.Down)
                {
                    MainForm.DownDGW(MainForm.dgw);
                    return true;
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle,
            System.Globalization.CultureInfo.CurrentCulture, out result);
        }

        private void izracun()
        {
            if (dgw.RowCount > 0)
            {
                int rowBR = dgw.CurrentRow.Index;

                double dec_parse;
                if (!double.TryParse(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString(), out dec_parse))
                {
                    dgw.Rows[rowBR].Cells["kolicina"].Value = "1";
                    MessageBox.Show("Greška kod upisa količine.", "Greška");
                    return;
                }

                if (!double.TryParse(dgw.Rows[rowBR].Cells["nbc"].FormattedValue.ToString(), out dec_parse))
                {
                    dgw.Rows[rowBR].Cells["nbc"].Value = "0";
                    MessageBox.Show("Greška kod upisa nabavne cijene.", "Greška");
                    return;
                }

                if (!double.TryParse(dgw.Rows[rowBR].Cells["mpc"].FormattedValue.ToString(), out dec_parse))
                {
                    dgw.Rows[rowBR].Cells["mpc"].Value = "0,00";
                    MessageBox.Show("Greška kod upisa maloprodajne cijene.", "Greška");
                    return;
                }

                if (isNumeric(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) == false)
                {
                    dgw.Rows[rowBR].Cells["kolicina"].Value = "1";
                    MessageBox.Show("Greška kod upisa količine.", "Greška");
                }

                double kol = Convert.ToDouble(dgw.Rows[rowBR].Cells["kolicina"].FormattedValue.ToString());
                double nbc = Convert.ToDouble(dgw.Rows[rowBR].Cells["nbc"].FormattedValue.ToString());
                double mpc = Convert.ToDouble(dgw.Rows[rowBR].Cells["mpc"].FormattedValue.ToString());

                double nbc_ukupno = nbc * kol;
                double mpc_sa_kolicinom = mpc * kol;

                dgw.Rows[rowBR].Cells["iznos"].Value = Math.Round(nbc_ukupno, 2).ToString("#0.00");
                dgw.Rows[rowBR].Cells["iznos1"].Value = Math.Round(mpc_sa_kolicinom, 2).ToString("#0.00");

                double u = 0;

                for (int i = 0; i < dgw.RowCount; i++)
                {
                    u = Convert.ToDouble(dgw.Rows[i].Cells["iznos1"].FormattedValue.ToString()) + u;
                }

                textBox1.Text = "Ukupno sa PDV-om: " + Math.Round(u, 2).ToString("#0.00");
            }
        }

        private void fillComboBox()
        {
            //fill komercijalist
            DS_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici;", "zaposlenici");
            cbIzvrsio.DataSource = DS_Zaposlenik.Tables[0];
            cbIzvrsio.DisplayMember = "IME";
            cbIzvrsio.ValueMember = "id_zaposlenik";
            cbIzvrsio.SelectedValue = Properties.Settings.Default.id_zaposlenik;

            //fill vrsta dokumenta
            DSvd = classSQL.select("SELECT * FROM fakture_vd  WHERE grupa = 'rna' ORDER BY id_vd", "fakture_vd");
            cbVD.DataSource = DSvd.Tables[0];
            cbVD.DisplayMember = "vd";
            cbVD.ValueMember = "id_vd";

            //fill mjTroška
            DataSet DSgrad = classSQL.select("SELECT * FROM grad ORDER BY grad ", "grad");
            cbMjestoTroska.DataSource = DSgrad.Tables[0];
            cbMjestoTroska.DisplayMember = "grad";
            cbMjestoTroska.ValueMember = "id_grad";
            cbMjestoTroska.SelectedValue = Class.PodaciTvrtka.gradPoslovnicaId;

            //fill tko je prijavljen
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
            DS_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste");
        }

        private void ControlDisableEnable(bool novi, bool odustani, bool spremi, bool sve, bool delAll)
        {
            btnNoviUnos.Enabled = novi;

            btnOdustani.Enabled = odustani;

            btnSpremi.Enabled = spremi;

            btnSve.Enabled = sve;

            btnDeleteAll.Enabled = delAll;
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        #endregion Util

        #region Fill Radni nalog

        private void FillRn()
        {
            //fill header

            deleteFields();

            DSponude = classSQL.select("SELECT * FROM radni_nalog WHERE broj_naloga = '" + broj_RN_edit + "'", "radni_nalog").Tables[0];

            cbVD.SelectedValue = DSponude.Rows[0]["vrasta_dokumenta"].ToString();
            txtSifraOdrediste.Text = DSponude.Rows[0]["id_narucioc"].ToString();
            if (DSponude.Rows[0]["id_narucioc"].ToString() != "0")
            {
                txtPartnerNaziv.Text = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + DSponude.Rows[0]["id_narucioc"].ToString() + "'", "partners").Tables[0].Rows[0][0].ToString();
            }
            else
            {
                txtPartnerNaziv.Text = "";
            }
            txtSifraOdrediste.Text = DSponude.Rows[0]["id_narucioc"].ToString();
            rtbNapomena.Text = DSponude.Rows[0]["napomena"].ToString();
            dtpDatumNaloga.Value = Convert.ToDateTime(DSponude.Rows[0]["datum_naloga"].ToString());
            dtpDatumPrimitka.Value = Convert.ToDateTime(DSponude.Rows[0]["datum_primitka"].ToString());
            dtpDatumZavrsetka.Value = Convert.ToDateTime(DSponude.Rows[0]["zavrsna_kartica"].ToString());
            cbIzvrsio.SelectedValue = DSponude.Rows[0]["id_izvrsio"].ToString();
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + DSponude.Rows[0]["id_izradio"].ToString() + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();
            ttxBrojPonude.Text = DSponude.Rows[0]["broj_naloga"].ToString();
            cbMjestoTroska.SelectedValue = DSponude.Rows[0]["mj_troska"].ToString();
            cbIzvrsio.SelectedValue = DSponude.Rows[0]["id_izvrsio"].ToString();
            txtIzvrsio.Text = DSponude.Rows[0]["izvrsio"].ToString().Trim();

            //--------fill faktura stavke------------------------------

            StavkeRadnogNaloga = classSQL.select("SELECT * FROM radni_nalog_stavke WHERE broj_naloga = '" + broj_RN_edit + "'", "radni_nalog_stavke").Tables[0];

            FillRnStavke(StavkeRadnogNaloga);
        }

        private void FillRnStavke(DataTable DSFS)
        {
            for (int i = 0; i < DSFS.Rows.Count; i++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;

                dgw.Rows[br].Cells["sifra"].Value = DSFS.Rows[i]["sifra_robe"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = DSFS.Rows[i]["naziv"].ToString();
                try
                {
                    dgw.Rows[br].Cells["skladiste"].Value = DSFS.Rows[i]["id_skladiste"].ToString();
                }
                catch (Exception)
                {
                }
                dgw.Rows[br].Cells["kolicina"].Value = DSFS.Rows[i]["kolicina"].ToString();
                dgw.Rows[br].Cells["nbc"].Value = string.Format(DSFS.Rows[i]["nbc"].ToString());
                dgw.Rows[br].Cells["mpc"].Value = Math.Round((Convert.ToDouble(DSFS.Rows[i]["vpc"].ToString()) * Convert.ToDouble(Convert.ToDecimal(DSFS.Rows[i]["porez"].ToString()) / 100)) + Convert.ToDouble(DSFS.Rows[i]["vpc"].ToString()), 2).ToString("#0.00");
                dgw.Rows[br].Cells["iznos"].Value = "0,00";
                dgw.Rows[br].Cells["iznos1"].Value = "0,00";
                dgw.Rows[br].Cells["id_stavka"].Value = DSFS.Rows[i]["id_stavka"].ToString();
                dgw.Rows[br].Cells["vpc"].Value = DSFS.Rows[i]["vpc"].ToString();
                dgw.Rows[br].Cells["porez"].Value = DSFS.Rows[i]["porez"].ToString();
                dgw.Rows[br].Cells["oduzmi"].Value = DSFS.Rows[i]["oduzmi"].ToString();

                dgw.Select();
                dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[3];
                izracun();
                ControlDisableEnable(false, true, true, false, true);
            }

            dgw.Columns["iznos1"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgw.Columns["iznos1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        #endregion Fill Radni nalog

        #region Insert/Update radni nalog/stavke/normativi

        /// <summary>
        /// Vpc i nbc s točkom ili zarezom ovisno o konekciji
        /// </summary>
        /// <param name="vpc"></param>
        /// <param name="nbc"></param>
        private void SrediVpcNbc(ref string vpc, ref string nbc)
        {
            if (classSQL.remoteConnectionString == "")
            {
                vpc = vpc.Replace(",", ".");
                nbc = nbc.Replace(",", ".");
            }
            else
            {
                vpc = vpc.Replace(".", ",");
                nbc = nbc.Replace(".", ",");
            }
        }

        private void UpdateRadniNalogStavke(string vpc, string nbc, int red)
        {
            string sql = "UPDATE radni_nalog_stavke SET " +
            " id_skladiste='" + dgw.Rows[red].Cells["skladiste"].Value + "'," +
            " sifra_robe='" + dg(red, "sifra") + "'," +
            " broj_naloga='" + ttxBrojPonude.Text + "'," +
            " vpc='" + vpc + "'," +
            " nbc='" + nbc + "'," +
            " porez='" + dg(red, "porez") + "'," +
            " naziv='" + dg(red, "naziv") + "'," +
            " kolicina='" + dg(red, "kolicina") + "'," +
            " oduzmi='" + dg(red, "oduzmi") + "'" +
            " WHERE id_stavka='" + dg(red, "id_stavka") + "'";
            provjera_sql(classSQL.update(sql));
        }

        private void InsertRadniNalogStavke(string vpc, string nbc, int red)
        {
            string sql_stavke = "INSERT INTO radni_nalog_stavke (" +
                "id_skladiste, sifra_robe, broj_naloga, vpc, nbc, naziv, kolicina, porez, oduzmi)" +
                " VALUES (" +
                "'" + dgw.Rows[red].Cells["skladiste"].Value + "'," +
                "'" + dg(red, "sifra") + "'," +
                "'" + ttxBrojPonude.Text + "'," +
                "'" + vpc + "'," +
                "'" + nbc + "'," +
                "'" + dg(red, "naziv") + "'," +
                "'" + dg(red, "kolicina") + "'," +
                "'" + dg(red, "porez") + "', " +
                "'" + dg(red, "oduzmi") + "'" +
                ")";
            provjera_sql(classSQL.insert(sql_stavke));
        }

        /// <summary>
        /// Ažurira radni nalog
        /// </summary>
        private void UpdateRadniNalogHelper()
        {
            string komercijalista = Properties.Settings.Default.id_zaposlenik;
            if (cbIzvrsio.SelectedValue != null)
                komercijalista = cbIzvrsio.SelectedValue.ToString();

            string sql = "UPDATE radni_nalog SET " +
                " broj_naloga='" + ttxBrojPonude.Text + "'," +
                " godina_naloga='" + nmGodinaPonude.Value.ToString() + "'," +
                " datum_naloga='" + dtpDatumNaloga.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " datum_primitka='" + dtpDatumPrimitka.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " zavrsna_kartica='" + dtpDatumZavrsetka.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                " mj_troska='" + cbMjestoTroska.SelectedValue + "'," +
                " id_narucioc='" + txtSifraOdrediste.Text + "'," +
                " id_izvrsio='" + komercijalista + "'," +
                " napomena='" + rtbNapomena.Text + "'," +
                " izvrsio = '" + txtIzvrsio.Text.Trim() + "'," +
                " vrasta_dokumenta='" + cbVD.SelectedValue + "'" +
                " WHERE broj_naloga='" + ttxBrojPonude.Text + "'";
            classSQL.update(sql);
        }

        /// <summary>
        /// Ažurira radni nalog normativ i skladišta
        /// </summary>
        /// <param name="StavkeNormativaZaRobuISkladiste">Stavke normativa</param>
        /// <param name="stavkaRadnogNaloga">Trenutna stavka</param>
        /// <param name="red">Redak u datagridu</param>
        private void UpdateRadniNalogNormativ(DataTable StavkeNormativaZaRobuISkladiste, DataRow[] stavkaRadnogNaloga, int red)
        {
            string sql_update;

            for (int j = 0; j < StavkeNormativaZaRobuISkladiste.Rows.Count; j++)
            {
                DataRow r = StavkeNormativaZaRobuISkladiste.Rows[j];

                DataTable DTcheck = classSQL.select($"SELECT sifra FROM radni_nalog_normativ WHERE sifra = '{r["sifra_robe"].ToString()}'", "radni_nalog_normativ").Tables[0];
                if (DTcheck.Rows.Count > 0)
                {
                    //string sql_stavke_normativi = "UPDATE radni_nalog_normativ SET kolicina='" + StavkeNormativaZaRobuISkladiste.Rows[j]["kolicina"].ToString() + "'";
                    string sql_stavke_normativi = "UPDATE radni_nalog_normativ SET kolicina='" +
                        Convert.ToDouble(r["kolicina"].ToString())
                        + "'" +
                        " WHERE sifra='" + r["sifra_robe"].ToString() + "'" +
                        " AND id_skladiste='" + r["id_skladiste"].ToString() + "'" +
                        " AND broj='" + ttxBrojPonude.Text + "'";
                    //tu bi trebalo staviti id
                    provjera_sql(classSQL.insert(sql_stavke_normativi));
                }
                else
                {
                    // Insert radni nalog normativ ukoliko je naknadno dodan
                    string selectRadniNalogNormativi = $@"SELECT roba_prodaja.vpc,
	                                                roba_prodaja.porez,
	                                                normativi.sifra_artikla,
	                                                normativi_stavke.sifra_robe,
	                                                roba_prodaja.nc,
	                                                normativi_stavke.kolicina,
	                                                normativi_stavke.id_skladiste
                                                FROM normativi_stavke
                                                LEFT JOIN normativi ON normativi_stavke.broj_normativa=normativi.broj_normativa
                                                LEFT JOIN roba_prodaja ON roba_prodaja.sifra=normativi_stavke.sifra_robe
                                                WHERE normativi_stavke.sifra_robe='{r["sifra_robe"].ToString()}'
                                                AND normativi_stavke.id_skladiste=roba_prodaja.id_skladiste";

                    DataTable DTnormativi = classSQL.select(selectRadniNalogNormativi, "normativi_stavke").Tables[0];
                    if(DTnormativi.Rows.Count > 0)
                    {
                        for(int i = 0; i < DTnormativi.Rows.Count; i++)
                        {
                            DataRow row = DTnormativi.Rows[i];
                            string insertQuery = $@"INSERT INTO radni_nalog_normativ (sifra, vpc, nbc, broj, kolicina, id_skladiste, pdv)
                                                    VALUES ('{row["sifra_robe"].ToString()}', {row["vpc"].ToString().Replace(',', '.')}, {row["nc"].ToString().Replace(',', '.')},
                                                             {ttxBrojPonude.Text}, '{Convert.ToDecimal(row["kolicina"].ToString())}', 
                                                             {row["id_skladiste"].ToString()}, '{row["porez"].ToString()}')";
                            classSQL.insert(insertQuery);
                        }
                    }
                }

                double kolStara = Convert.ToDouble(
                    stavkaRadnogNaloga[0]["kolicina"].ToString()) *
                    Convert.ToDouble(r["kolicina"].ToString());

                double kolNova = Convert.ToDouble(
                    dg(red, "kolicina")) *
                    Convert.ToDouble(r["kolicina"].ToString());

                string k = SQL.ClassSkladiste.GetAmount(
                    r["sifra_robe"].ToString(),
                    r["id_skladiste"].ToString(),
                    (kolStara - kolNova).ToString(),
                    "1",
                    "+");

                sql_update = "UPDATE roba_prodaja SET kolicina='" + k +
                    "' WHERE sifra='" + r["sifra_robe"].ToString() +
                    "' AND id_skladiste='" + r["id_skladiste"].ToString() + "'";

                provjera_sql(classSQL.update(sql_update));
            }
        }

        private void SrediStavkeNormativeSkladisteUpdate(string vpc, string nbc, int red)
        {
            UpdateRadniNalogStavke(vpc, nbc, red);

            //odaberi trenutnu stavku iz skupa svih stavaka iz baze za zadani radni nalog
            DataRow[] stavkaRadnogNaloga = StavkeRadnogNaloga.Select("id_stavka = " + dg(red, "id_stavka"));

            DataTable StavkeNormativaZaRobuISkladiste = new DataTable();

            string sql_update = "";
            string sql = "SELECT roba_prodaja.vpc,roba_prodaja.porez,normativi.sifra_artikla,normativi_stavke.sifra_robe," +
                "roba_prodaja.nc,normativi_stavke.kolicina,normativi_stavke.id_skladiste" +
                " FROM normativi_stavke " +
                " LEFT JOIN normativi ON normativi_stavke.broj_normativa=normativi.broj_normativa " +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=normativi_stavke.sifra_robe" +
                " WHERE normativi.sifra_artikla='" + dgw.Rows[red].Cells["sifra"].FormattedValue.ToString() + "'" +
                " AND normativi_stavke.id_skladiste=roba_prodaja.id_skladiste";
            StavkeNormativaZaRobuISkladiste = classSQL.select(sql, "normativi_stavke").Tables[0];
            decimal nabavna_cijena = Convert.ToDecimal(dg(red, "mpc"));
            decimal proizvodacka_cijena = Convert.ToDecimal(dg(red, "nbc"));

            if (dgw.Rows[red].Cells["skladiste"].Value.ToString() == stavkaRadnogNaloga[0]["id_skladiste"].ToString())
            {
                //////////////////////////////////////////////////////////////ako je isto skladiste//////////////

                UpdateRadniNalogNormativ(StavkeNormativaZaRobuISkladiste, stavkaRadnogNaloga, red);

                string kol = SQL.ClassSkladiste.GetAmount(
                    dg(red, "sifra"),
                    dgw.Rows[red].Cells["skladiste"].Value.ToString(),
                    (Convert.ToDouble(dg(red, "kolicina")) - Convert.ToDouble(stavkaRadnogNaloga[0]["kolicina"].ToString())).ToString(),
                    "1",
                    "+");

                sql_update = "UPDATE roba_prodaja SET kolicina = '" + kol +
                    "' WHERE id_skladiste='" + stavkaRadnogNaloga[0]["id_skladiste"].ToString() +
                    "'AND sifra='" + stavkaRadnogNaloga[0]["sifra_robe"].ToString() + "'";

                provjera_sql(classSQL.update(sql_update));
            }
            else
            {
                //////////////////////////////////////////////////////////////ako je promijenjeno skladiste//////////////

                //Znači, kad se promijeni skladište, normativi, normativi_stavke i radni_nalog_normativ se ne mijenjaju, oni su
                //UVIJEK FIKSNI!!!
                //Jedino kaj se mijenja su količine stavke naloga, tj. na prvom skladištu se mora oduzeti prijašnja količina,
                //a na novom se mora dodati nova količina

                UpdateRadniNalogNormativ(StavkeNormativaZaRobuISkladiste, stavkaRadnogNaloga, red);

                //dodaje u novo skladiste
                string kol = SQL.ClassSkladiste.GetAmount(
                    dg(red, "sifra"),
                    dgw.Rows[red].Cells["skladiste"].Value.ToString(),
                    dg(red, "kolicina"),
                    "1",
                    "+");

                sql_update = "UPDATE roba_prodaja SET kolicina = '" + kol +
                    "' WHERE id_skladiste='" + dgw.Rows[red].Cells["skladiste"].Value.ToString() +
                    "' AND sifra='" + stavkaRadnogNaloga[0]["sifra_robe"].ToString() + "'";
                provjera_sql(classSQL.update(sql_update));

                //oduzima staro skladiste
                kol = SQL.ClassSkladiste.GetAmount(
                    dg(red, "sifra"),
                    stavkaRadnogNaloga[0]["id_skladiste"].ToString(),
                    stavkaRadnogNaloga[0]["kolicina"].ToString(),
                    "1",
                    "-");

                sql_update = "UPDATE roba_prodaja SET kolicina ='" + kol +
                    "' WHERE id_skladiste='" + stavkaRadnogNaloga[0]["id_skladiste"].ToString() +
                    "' AND sifra='" + stavkaRadnogNaloga[0]["sifra_robe"].ToString() + "'";
                provjera_sql(classSQL.update(sql_update));
            }

            if (Class.Postavke.proizvodnja_normativ_pc)
            {
                sql_update = string.Format(@"update roba set nc = '{0}', proizvodacka_cijena = {1} where sifra = '{2}';",
                    nabavna_cijena,
                    proizvodacka_cijena.ToString().Replace(',', '.'),
                    stavkaRadnogNaloga[0]["sifra_robe"].ToString());
                classSQL.update(sql_update);

                sql_update = string.Format(@"update roba_prodaja set nc = '{0}', proizvodacka_cijena = {1} where sifra = '{2}' and id_skladiste = {3};",
                    nabavna_cijena,
                    proizvodacka_cijena.ToString().Replace(',', '.'),
                    stavkaRadnogNaloga[0]["sifra_robe"].ToString(),
                    stavkaRadnogNaloga[0]["id_skladiste"].ToString());
                classSQL.update(sql_update);
            }
        }

        private void SrediStavkeNormativeSkladisteNovi(string vpc, string nbc, int red)
        {
            InsertRadniNalogStavke(vpc, nbc, red);

            InsertRadniNalogNormativ(red);

            string k1 = SQL.ClassSkladiste.GetAmount(
                dg(red, "sifra"),
                dgw.Rows[red].Cells["skladiste"].Value.ToString(),
                dg(red, "kolicina"),
                "1",
                "+");

            string sql_update = "UPDATE roba_prodaja SET kolicina='" + k1 +
                "' WHERE sifra='" + dgw.Rows[red].Cells["sifra"].FormattedValue.ToString() +
                "' AND id_skladiste='" + dgw.Rows[red].Cells["skladiste"].Value + "'";

            provjera_sql(classSQL.update(sql_update));
            decimal nabavna_cijena = Convert.ToDecimal(dg(red, "mpc"));
            decimal proizvodacka_cijena = Convert.ToDecimal(dg(red, "nbc"));
            if (Class.Postavke.proizvodnja_normativ_pc)
            {
                sql_update = string.Format(@"update roba set nc = '{0}', proizvodacka_cijena = {1} where sifra = '{2}';",
                    nabavna_cijena,
                    proizvodacka_cijena.ToString().Replace(',', '.'),
                    dgw.Rows[red].Cells["sifra"].FormattedValue.ToString());
                classSQL.update(sql_update);

                sql_update = string.Format(@"update roba_prodaja set nc = '{0}', proizvodacka_cijena = {1} where sifra = '{2}' and id_skladiste = {3};",
                    nabavna_cijena,
                    proizvodacka_cijena.ToString().Replace(',', '.'),
                    dgw.Rows[red].Cells["sifra"].FormattedValue.ToString(),
                    dgw.Rows[red].Cells["skladiste"].Value);
                classSQL.update(sql_update);
            }
        }

        private void UpdateRadniNalog()
        {
            UpdateRadniNalogHelper();

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                decimal mpc, porez;
                decimal.TryParse(dg(i, "mpc"), out mpc);
                decimal.TryParse(dg(i, "porez"), out porez);

                string vpc = Math.Round((mpc / (1 + (porez / 100))), 4).ToString();
                string nbc = dg(i, "nbc");

                SrediVpcNbc(ref vpc, ref nbc);

                if (dgw.Rows[i].Cells["id_stavka"].Value != null)
                {
                    //if (Class.Postavke.proizvodnja_normativ_pc)
                    //{
                    //    SrediStavkeNormativeSkladisteUpdate(mpc.ToString(), nbc, i);
                    //}
                    //else
                    //{
                    SrediStavkeNormativeSkladisteUpdate(vpc, nbc, i);
                    //}
                }
                else
                {
                    //if (Class.Postavke.proizvodnja_normativ_pc)
                    //{
                    //    SrediStavkeNormativeSkladisteNovi(mpc.ToString(), nbc, i);
                    //}
                    //else
                    //{
                    SrediStavkeNormativeSkladisteNovi(vpc, nbc, i);
                    //}
                }
            }

            MessageBox.Show("Spremljeno.");

            edit = false;
            EnableDisable(false);
            deleteFields();
            btnSpremi.Enabled = false;
            btnSve.Enabled = true;
        }

        private void UpdateRN()
        {
            UpdateRadniNalog();
            Util.AktivnostZaposlenika.SpremiAktivnost(dgw, null, "Radni nalog", ttxBrojPonude.Text, true);
            EnableDisable(false);
            deleteFields();
            ttxBrojPonude.Enabled = true;
            nmGodinaPonude.Enabled = true;
            ControlDisableEnable(true, false, false, true, false);
            ttxBrojPonude.ReadOnly = false;
        }

        /// <summary>
        /// Sprema radni nalog.
        /// </summary>
        private void InsertRadniNalog()
        {
            string komercijalista = Properties.Settings.Default.id_zaposlenik;
            if (cbIzvrsio.SelectedValue != null)
                komercijalista = cbIzvrsio.SelectedValue.ToString();

            string sql = "INSERT INTO radni_nalog (broj_naloga, godina_naloga, datum_naloga, datum_primitka, " +
                "zavrsna_kartica, mj_troska, id_narucioc, id_izradio, id_izvrsio, vrasta_dokumenta,izvrsio, napomena) " +
                " VALUES " +
                "(" +
                "'" + ttxBrojPonude.Text + "'," +
                "'" + nmGodinaPonude.Value.ToString() + "'," +
                "'" + dtpDatumNaloga.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                "'" + dtpDatumPrimitka.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                "'" + dtpDatumZavrsetka.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                "'" + cbMjestoTroska.SelectedValue + "'," +
                "'" + txtSifraOdrediste.Text + "'," +
                "'" + Properties.Settings.Default.id_zaposlenik + "'," +
                "'" + komercijalista + "'," +
                "'" + cbVD.SelectedValue.ToString() + "'," +
                "'" + txtIzvrsio.Text.Trim() + "'," +
                "'" + rtbNapomena.Text + "'" +
                ")";
            provjera_sql(classSQL.insert(sql));
        }

        /// <summary>
        /// Sprema stavke radnog naloga i normative te ažurira skladište
        /// </summary>
        private void InsertRadniNalogStavkeNormativi()
        {
            string k1;
            string sql_update;

            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                InsertRadniNalogStavkeHelper(i);

                InsertRadniNalogNormativ(i);

                k1 = SQL.ClassSkladiste.GetAmount(
                    dg(i, "sifra"),
                    dgw.Rows[i].Cells["skladiste"].Value.ToString(),
                    dg(i, "kolicina"),
                    "1",
                    "+");

                sql_update = "UPDATE roba_prodaja SET kolicina='" + k1 +
                    "' WHERE sifra='" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() +
                    "' AND id_skladiste='" + dgw.Rows[i].Cells["skladiste"].Value + "'";

                provjera_sql(classSQL.update(sql_update));

                decimal nabavna_cijena = Convert.ToDecimal(dg(i, "mpc"));
                decimal proizvodacka_cijena = Convert.ToDecimal(dg(i, "nbc"));
                if (Class.Postavke.proizvodnja_normativ_pc)
                {
                    sql_update = string.Format(@"update roba set nc = '{0}', proizvodacka_cijena = {1} where sifra = '{2}';",
                        nabavna_cijena,
                        proizvodacka_cijena.ToString().Replace(',', '.'),
                        dgw.Rows[i].Cells["sifra"].FormattedValue.ToString());
                    classSQL.update(sql_update);

                    sql_update = string.Format(@"update roba_prodaja set nc = '{0}', proizvodacka_cijena = {1} where sifra = '{2}' and id_skladiste = {3};",
                        nabavna_cijena,
                        proizvodacka_cijena.ToString().Replace(',', '.'),
                        dgw.Rows[i].Cells["sifra"].FormattedValue.ToString(),
                        dgw.Rows[i].Cells["skladiste"].Value);
                    classSQL.update(sql_update);
                }
            }
        }

        /// <summary>
        /// Sprema stavku radnog naloga u bazu.
        /// </summary>
        /// <param name="red">Redak u datagridviewu.</param>
        private void InsertRadniNalogStavkeHelper(int red)
        {
            decimal mpc, porez;
            decimal.TryParse(dg(red, "mpc"), out mpc);
            decimal.TryParse(dg(red, "porez"), out porez);

            string vpc = Math.Round((mpc / (1 + (porez / 100))), 4).ToString();
            string nbc = dg(red, "nbc");

            SrediVpcNbc(ref vpc, ref nbc);

            string sql_stavke = "INSERT INTO radni_nalog_stavke (" +
                "id_skladiste,sifra_robe,broj_naloga,vpc,nbc,naziv,kolicina,porez,oduzmi)" +
                " VALUES (" +
                "'" + dgw.Rows[red].Cells["skladiste"].Value + "'," +
                "'" + dg(red, "sifra") + "'," +
                "'" + ttxBrojPonude.Text + "'," +
                "'" + vpc + "'," +
                "'" + nbc + "'," +
                "'" + dg(red, "naziv") + "'," +
                "'" + dg(red, "kolicina") + "'," +
                "'" + dg(red, "porez") + "'," +
                "'" + dg(red, "oduzmi") + "'" +
                ")";

            decimal vpc_uredjeno = Convert.ToDecimal(dg(red, "mpc")) / (1 + Convert.ToDecimal(dg(red, "porez")) / 100);

            if (Class.Postavke.proizvodnja_normativ_pc)
            {
                vpc_uredjeno = Convert.ToDecimal(dg(red, "mpc"));
            }

            provjera_sql(classSQL.insert(sql_stavke));
            string update_cijene = "update roba_prodaja set vpc = '" + vpc_uredjeno.ToString().Replace(",", ".") + "', nc = '" + nbc.Replace(".", ",") + "' WHERE " +
            " id_skladiste = '" + dgw.Rows[red].Cells["skladiste"].Value + "' AND sifra = '" + dg(red, "sifra") + "';";
            classSQL.update(update_cijene);
        }

        /// <summary>
        /// Za zadani redak iz datagrida traži stavke normativa te oduzima sa skladišta stavke normativa;
        /// ubacuje zapis o normativu u radni_nalog_normativ
        /// </summary>
        /// <param name="red"></param>
        private void InsertRadniNalogNormativ(int red)
        {
            DataTable DSn = new DataTable();
            string upit = "";
            string sql_update = "";

            upit = "SELECT roba_prodaja.vpc,roba_prodaja.porez,normativi.sifra_artikla,normativi_stavke.sifra_robe," +
                " roba_prodaja.nc,normativi_stavke.kolicina,normativi_stavke.id_skladiste" +
                " FROM normativi_stavke " +
                " LEFT JOIN normativi ON normativi_stavke.broj_normativa=normativi.broj_normativa " +
                " LEFT JOIN roba_prodaja ON roba_prodaja.sifra=normativi_stavke.sifra_robe" +
                " WHERE normativi.sifra_artikla='" + dgw.Rows[red].Cells["sifra"].FormattedValue.ToString() + "'" +
                " AND normativi_stavke.id_skladiste=roba_prodaja.id_skladiste";
            //20.02.2013. dodan zadnji AND za skladište jer je inače oduzimal nekoliko puta, kolko je skladišta našel!
            DSn = classSQL.select(upit, "normativi_stavke").Tables[0];

            if (!Class.Dokumenti.isKasica && ((Class.PodaciTvrtka.oibTvrtke == Class.Postavke.OIB_PC1 && Util.Korisno.GodinaKojaSeKoristiUbazi >= Class.Postavke.GODINA_POCETKA_FIFO_PC1) || Util.Korisno.GodinaKojaSeKoristiUbazi >= Class.Postavke.GODINA_POCETKA_FIFO))
            {
                string[] kolone = new string[4] { "kolicina", "nc", "sifra_robe", "id_skladiste" };
                DSn = Class.FIFO.setRacunNbc(DSn, kolone);
            }

            for (int j = 0; j < DSn.Rows.Count; j++)
            {
                decimal mpc, porez, vpc;

                decimal.TryParse(DSn.Rows[j]["vpc"].ToString(), out vpc);
                decimal.TryParse(DSn.Rows[j]["porez"].ToString(), out porez);

                //string vpc = Math.Round((mpc / (1 + (porez / 100))), 4).ToString();

                string sql_stavke_normativi = "INSERT INTO radni_nalog_normativ (" +
                    "id_skladiste,sifra,broj,vpc,nbc,kolicina,pdv)" +
                    " VALUES (" +
                    "'" + DSn.Rows[j]["id_skladiste"].ToString() + "'," +
                    "'" + DSn.Rows[j]["sifra_robe"].ToString() + "'," +
                    "'" + ttxBrojPonude.Text + "'," +
                    "'" + DSn.Rows[j]["vpc"].ToString().Replace(".", ",") + "'," +
                    "'" + DSn.Rows[j]["nc"].ToString().Replace(".", ",") + "'," +
                    "'" + DSn.Rows[j]["kolicina"].ToString().Replace(".", ",") + "'," +
                    "'" + DSn.Rows[j]["porez"].ToString().Replace(".", ",") + "'" +
                    ")";

                provjera_sql(classSQL.insert(sql_stavke_normativi));

                string k = SQL.ClassSkladiste.GetAmount(
                    DSn.Rows[j]["sifra_robe"].ToString(),
                    DSn.Rows[j]["id_skladiste"].ToString(),
                    DSn.Rows[j]["kolicina"].ToString(),
                    dgw.Rows[red].Cells["kolicina"].FormattedValue.ToString(),
                    "-");

                sql_update = "UPDATE roba_prodaja SET kolicina='" + k +
                    "' WHERE sifra='" + DSn.Rows[j]["sifra_robe"].ToString() +
                    "' AND id_skladiste='" + DSn.Rows[j]["id_skladiste"].ToString() + "'";

                provjera_sql(classSQL.update(sql_update));
            }
        }

        #endregion Insert/Update radni nalog/stavke/normativi

        #region Buttons

        private void btnIzlaz_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            edit = false;
            EnableDisable(true);
            btnSve.Enabled = false;
            ttxBrojPonude.Text = brojRadnogNaloga();
            btnDeleteAll.Enabled = false;
            btnSpremi.Enabled = true;
            //ttxBrojPonude.Select();
            ttxBrojPonude.Enabled = false;
            nmGodinaPonude.Enabled = false;
            ControlDisableEnable(false, true, true, false, true);
            cbVD.Select();
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            btnSve.Enabled = true;
            EnableDisable(false);
            deleteFields();
            ttxBrojPonude.Text = brojRadnogNaloga();
            edit = false;
            btnDeleteAll.Enabled = false;
            btnSpremi.Enabled = false;
            ttxBrojPonude.Enabled = true;
            nmGodinaPonude.Enabled = true;
            ttxBrojPonude.ReadOnly = false;
            ControlDisableEnable(true, false, false, true, false);
        }

        private void btnSviRN_Click(object sender, EventArgs e)
        {
            frmSviRadniNalozi srn = new frmSviRadniNalozi();
            srn.sifra_rn = "";
            srn.MainForm = this;
            srn.ShowDialog();
            if (broj_RN_edit != null)
            {
                deleteFields();
                FillRn();
                EnableDisable(true);

                edit = true;
                btnDeleteAll.Enabled = true;
                btnSpremi.Enabled = true;
            }
        }

        private bool SpremiProvjera()
        {
            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                //Tu bude greška jer izbacuje van kad je odabrano 1. skladište (tj. '-------------')
                //inače bu trebalo to izmjeniti
                if (dgw.Rows[i].Cells["skladiste"].Value.ToString() == "")
                {
                    MessageBox.Show("Radni nalog nije spremljen zbog ne odabira skladišta na pojedinim artiklima.", "Greška");
                    return false;
                }
            }

            if (txtSifraOdrediste.Text == "")
            {
                MessageBox.Show("Niste upisali šifru naručioca.", "Greška");
                return false;
            }

            if (isNumeric(txtSifraOdrediste.Text, System.Globalization.NumberStyles.Integer) == false)
            {
                MessageBox.Show("Niste pravilno upisali šifru naručioca.", "Greška");
                return false;
            }

            if (dgw.Rows.Count == 0)
            {
                MessageBox.Show("Nemate niti jednu stavku.", "Greška");
                return false;
            }

            return true;
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            if (!SpremiProvjera()) return;
            if (MessageBox.Show("Spremanjem radnog naloga mijenja se cijena artikla prema trenutnim cijenama u nalogu.\n\r Da li ste sigurni da želite spremiti radni nalog?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }

            if (edit)
            {
                UpdateRN();
                ttxBrojPonude.Text = brojRadnogNaloga();
                PregledKolicine();
                return;
            }

            ttxBrojPonude.Enabled = true;
            nmGodinaPonude.Enabled = true;

            InsertRadniNalog();

            InsertRadniNalogStavkeNormativi();

            Util.AktivnostZaposlenika.SpremiAktivnost(dgw, null, "Radni nalog", ttxBrojPonude.Text, false);
            MessageBox.Show("Spremljeno.");

            ttxBrojPonude.ReadOnly = false;
            edit = false;
            EnableDisable(false);
            deleteFields();
            ControlDisableEnable(true, false, false, true, false);

            ttxBrojPonude.Text = brojRadnogNaloga();

            ttxBrojPonude.Enabled = true;
            nmGodinaPonude.Enabled = true;
            ttxBrojPonude.ReadOnly = false;
            ControlDisableEnable(true, false, false, true, false);

            PregledKolicine();
        }

        private void PregledKolicine()
        {
            //OVO RADI SPREMLJENA PROCEDURA
            try
            {
                if (Class.Postavke.skidajSkladisteProgramski)
                {
                    string _sql = "";

                    foreach (DataGridViewRow r in dgw.Rows)
                    {
                        _sql += "SELECT postavi_kolicinu_sql_funkcija_prema_sifri('" + r.Cells["sifra"].FormattedValue.ToString() + $"'{Global.Functions.GetDateParam()}'') AS odgovor; ";
                    }

                    frmLoad l = new frmLoad();
                    l.Text = "Radim provjeru skladišta";
                    l.Show();
                    classSQL.insert(_sql);
                    if (!Class.Postavke.proizvodnja_normativ_pc)
                    {
                        classSQL.update(@"UPDATE radni_nalog_stavke SET nbc=(SELECT ProvjeraNabavneCijene(radni_nalog_stavke.sifra_robe,
                            (SELECT datum_naloga FROM radni_nalog WHERE radni_nalog.broj_naloga=radni_nalog_stavke.broj_naloga LIMIT 1),
                            CAST(radni_nalog_stavke.id_skladiste AS INTEGER)));");
                    }
                    l.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() != "")
            {
                DataRow[] dataROW = StavkeRadnogNaloga.Select("id_stavka = " + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].Value.ToString());

                if (MessageBox.Show("Brisanjem ove stavke brišete i količinu robe sa skladišta. Da li ste sigurni da želite obrisati ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    string kol = classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dgw.Rows[dgw.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString();
                    kol = (Convert.ToDouble(kol) - Convert.ToDouble(dataROW[0]["kolicina"].ToString())).ToString();
                    classSQL.update("UPDATE roba_prodaja SET kolicina='" + kol + "' WHERE sifra='" + dgw.Rows[dgw.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'");

                    DataTable DSn = new DataTable();
                    string upit = "";
                    string sql_update = "";

                    upit = "SELECT normativi.sifra_artikla,normativi_stavke.sifra_robe,normativi_stavke.kolicina,normativi_stavke.id_skladiste" +
                    "  FROM normativi_stavke INNER JOIN normativi ON normativi_stavke.broj_normativa=normativi.broj_normativa " +
                    " WHERE normativi.sifra_artikla='" + dgw.Rows[dgw.CurrentRow.Index].Cells["sifra"].FormattedValue.ToString() + "'";
                    DSn = classSQL.select(upit, "normativi_stavke").Tables[0];

                    for (int x = 0; x < DSn.Rows.Count; x++)
                    {
                        classSQL.delete("DELETE FROM radni_nalog_normativ WHERE sifra='" + DSn.Rows[x]["sifra_artikla"].ToString() + "' AND broj='" + ttxBrojPonude.Text + "'");

                        string k = SQL.ClassSkladiste.GetAmount(
                            DSn.Rows[x]["sifra_robe"].ToString(),
                            DSn.Rows[x]["id_skladiste"].ToString(),
                            DSn.Rows[x]["kolicina"].ToString(),
                            dataROW[0]["kolicina"].ToString(),
                            "+");
                        sql_update = "UPDATE roba_prodaja SET kolicina='" + k + "' WHERE sifra='" + DSn.Rows[x]["sifra_robe"].ToString() + "' AND id_skladiste='" + DSn.Rows[x]["id_skladiste"].ToString() + "'";
                        provjera_sql(classSQL.update(sql_update));
                    }

                    classSQL.delete("DELETE FROM radni_nalog_stavke WHERE id_stavka='" + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() + "'");
                    dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                    classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") + "','Brisanje stavke sa radnog naloba br." + ttxBrojPonude.Text + "')");
                    Util.AktivnostZaposlenika.SpremiAktivnost(dgw, null, "Radni nalog", ttxBrojPonude.Text, true);
                    //MessageBox.Show("Obrisano.");
                }
            }
            else
            {
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                //MessageBox.Show("Obrisano.");
            }
        }

        private void btnDeleteAllRN_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Brisanjem ovog radnog naloga brišete i količinu robe sa skladišta. Da li ste sigurni da želite obrisati ovaj dokument?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                double skl = 0;
                double fa_kolicina = 0;
                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    if (dgw.Rows[i].Cells["id_stavka"].Value != null)
                    {
                        DataRow[] dataROW = StavkeRadnogNaloga.Select("id_stavka = " + dgw.Rows[i].Cells["id_stavka"].Value.ToString());
                        skl = Convert.ToDouble(classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'", "roba_prodaja").Tables[0].Rows[0][0].ToString());
                        fa_kolicina = Convert.ToDouble(dataROW[0]["kolicina"].ToString());

                        DataTable DSn = new DataTable();
                        string upit = "";
                        string sql_update = "";

                        upit = "SELECT normativi.sifra_artikla,normativi_stavke.sifra_robe,normativi_stavke.kolicina,normativi_stavke.id_skladiste" +
                        "  FROM normativi_stavke INNER JOIN normativi ON normativi_stavke.broj_normativa=normativi.broj_normativa " +
                        " WHERE normativi.sifra_artikla='" + dgw.Rows[i].Cells["sifra"].FormattedValue.ToString() + "'";
                        DSn = classSQL.select(upit, "normativi_stavke").Tables[0];

                        for (int j = 0; j < DSn.Rows.Count; j++)
                        {
                            string k = SQL.ClassSkladiste.GetAmount(
                                DSn.Rows[j]["sifra_robe"].ToString(),
                                DSn.Rows[j]["id_skladiste"].ToString(),
                                DSn.Rows[j]["kolicina"].ToString(),
                                dataROW[0]["kolicina"].ToString(),
                                "+");
                            sql_update = "UPDATE roba_prodaja SET kolicina='" + k + "' WHERE sifra='" + DSn.Rows[j]["sifra_robe"].ToString() + "' AND id_skladiste='" + DSn.Rows[j]["id_skladiste"].ToString() + "'";
                            provjera_sql(classSQL.update(sql_update));
                        }

                        skl = skl - fa_kolicina;
                        classSQL.update("UPDATE roba_prodaja SET kolicina='" + skl + "' WHERE sifra='" + dg(i, "sifra") + "' AND id_skladiste='" + dataROW[0]["id_skladiste"].ToString() + "'");
                    }
                }

                classSQL.delete("DELETE FROM radni_nalog_normativ WHERE broj='" + ttxBrojPonude.Text + "'");
                classSQL.delete("DELETE FROM radni_nalog_stavke WHERE broj_naloga='" + ttxBrojPonude.Text + "'");
                classSQL.delete("DELETE FROM radni_nalog WHERE broj_naloga='" + ttxBrojPonude.Text + "'");
                classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") + "','Brisanje cijelog radnog naloga br." + ttxBrojPonude.Text + "')");
                Util.AktivnostZaposlenika.SpremiAktivnost(new DataGridView(), null, "Radni nalog", ttxBrojPonude.Text, true);
                //MessageBox.Show("Obrisano.");

                edit = false;
                EnableDisable(false);
                deleteFields();
                btnDeleteAll.Enabled = false;
                btnObrisi.Enabled = false;
                ttxBrojPonude.Text = brojRadnogNaloga();
            }
        }

        private void btnSveStavke_Click(object sender, EventArgs e)
        {
            if (dgw.Rows.Count > 0)
            {
                frmStavkeRadnogNaloga frmst = new frmStavkeRadnogNaloga();
                frmst.sf_artikla = dgw.CurrentRow.Cells["sifra"].FormattedValue.ToString();
                //frmst.broj_naloga = Convert.ToInt32(dgw.CurrentRow.Cells[""].FormattedValue);
                frmst.MainForm = this;
                frmst.ShowDialog();
            }
            else
            {
                MessageBox.Show("Nemate niti jednu stavku za pogled.", "Greška");
            }
        }

        private void btnOpenRoba_Click(object sender, EventArgs e)
        {
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            roba_trazi.ShowDialog();
            string propertis_sifra = Properties.Settings.Default.id_roba.Trim();
            if (propertis_sifra != "")
            {
                for (int y = 0; y < dgw.Rows.Count; y++)
                {
                    if (propertis_sifra == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString())
                    {
                        MessageBox.Show("Artikl ili usluga već postoje!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string sql = "SELECT * FROM roba WHERE sifra IN (SELECT sifra_artikla FROM normativi WHERE sifra_artikla='" + propertis_sifra + "')";

                if (oibs.Contains(Class.PodaciTvrtka.oibTvrtke))
                    sql = "SELECT * FROM roba WHERE sifra = '" + propertis_sifra + "';";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();
                    //dgw.Select();
                    //dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[2];
                    ttxBrojPonude.Enabled = false;
                    nmGodinaPonude.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoji normativ.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
                    txtSifraOdrediste.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        #endregion Buttons

        #region KeyDown event

        private void ttxBrojPonude_KeyDown(object sender, KeyEventArgs e)
        {
            nmGodinaPonude.Select();
        }

        private void nmGodinaPonude_KeyDown(object sender, KeyEventArgs e)
        {
            cbVD.Select();
        }

        private void cbVD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpDatumNaloga.Select();
            }
        }

        private void dtpDatumNaloga_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpDatumPrimitka.Select();
            }
        }

        private void dtpDatumPrimitka_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpDatumZavrsetka.Select();
            }
        }

        private void dtpDatumZavrsetka_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbMjestoTroska.Select();
            }
        }

        private void cbKomercijalist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSifraOdrediste.Select();
            }
        }

        private void txtSifraOdrediste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataTable DSpar = classSQL.select("SELECT ime_tvrtke FROM partners WHERE id_partner='" + txtSifraOdrediste.Text + "'", "partners").Tables[0];
                    if (DSpar.Rows.Count > 0)
                    {
                        txtPartnerNaziv.Text = DSpar.Rows[0][0].ToString();
                        txtIzradio.Select();
                    }
                    else
                    {
                        MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                    }
                }
            }
        }

        private void txtPartnerNaziv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtIzradio.Select();
            }
        }

        private void txtIzradio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbIzvrsio.Select();
            }
        }

        private void cbIzvrsio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                rtbNapomena.Select();
            }
        }

        private void rtbNapomena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSifra_robe.Select();
            }
        }

        private void txtSifra_robe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                for (int y = 0; y < dgw.Rows.Count; y++)
                {
                    if (txtSifra_robe.Text == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString())
                    {
                        MessageBox.Show("Artikl ili usluga već postoje u ovom radnom nalogu!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (txtSifra_robe.Text == "")
                {
                    frmRobaTrazi roba = new frmRobaTrazi();
                    roba.ShowDialog();

                    if (Properties.Settings.Default.id_roba != "")
                    {
                        txtSifra_robe.Text = Properties.Settings.Default.id_roba;
                        //CBskladiste.SelectedValue = Convert.ToInt32(Properties.Settings.Default.idSkladiste);
                        //vecSelektiran = false;
                        //txtUnos.Select();
                    }
                    else
                    {
                        return;
                    }
                }

                string sql = "SELECT * FROM roba WHERE sifra IN (SELECT sifra_artikla FROM normativi WHERE sifra_artikla='" + txtSifra_robe.Text + "')";
                if (oibs.Contains(Class.PodaciTvrtka.oibTvrtke))
                    sql = "SELECT * FROM roba WHERE sifra = '" + txtSifra_robe.Text + "';";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();
                    //dgw.Select();
                    //dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[2];
                    ttxBrojPonude.Enabled = false;
                    nmGodinaPonude.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoji normativ.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ttxBrojPonude_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT broj_naloga FROM radni_nalog WHERE broj_naloga='" + ttxBrojPonude.Text + "'", "radni_nalog").Tables[0];
                deleteFields();
                if (DT.Rows.Count == 0)
                {
                    if (brojRadnogNaloga() == ttxBrojPonude.Text.Trim())
                    {
                        edit = false;
                        EnableDisable(true);
                        btnSveStavke.Enabled = false;
                        ttxBrojPonude.Text = brojRadnogNaloga();
                        btnDeleteAll.Enabled = false;
                        txtSifraOdrediste.Select();
                        ttxBrojPonude.ReadOnly = true;
                        nmGodinaPonude.ReadOnly = true;
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    broj_RN_edit = ttxBrojPonude.Text;
                    FillRn();
                    EnableDisable(true);
                    edit = true;
                    btnDeleteAll.Enabled = true;
                    txtSifraOdrediste.Select();
                    ttxBrojPonude.ReadOnly = true;
                    nmGodinaPonude.ReadOnly = true;
                    btnSpremi.Enabled = true;
                }
            }
        }

        #endregion KeyDown event

        #region Datagridview helpers

        private void DGVCREATE()
        {
            DataGridViewTextBoxColumn sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewComboBoxColumn skladiste = new System.Windows.Forms.DataGridViewComboBoxColumn();
            DataGridViewTextBoxColumn naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn nab_cijena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn mpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn iznos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn iznos1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn vpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn nbc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn porez = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn id_stavka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn oduzmi = new System.Windows.Forms.DataGridViewTextBoxColumn();

            // kalkulacijaDataGridView
            //
            this.dgw.AutoGenerateColumns = false;
            this.dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgw.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            sifra,
            naziv,
            skladiste,
            kolicina,
            nab_cijena,
            mpc,
            iznos,
            iznos1,
            vpc,
            nbc,
            porez,id_stavka,oduzmi});

            DataTable DTSK = new DataTable("Roba");
            DTSK.Columns.Add("id_skladiste", typeof(string));
            DTSK.Columns.Add("skladiste", typeof(string));

            DS_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste");
            for (int i = 0; i < DS_Skladiste.Tables[0].Rows.Count; i++)
            {
                DTSK.Rows.Add(DS_Skladiste.Tables[0].Rows[i]["id_skladiste"], DS_Skladiste.Tables[0].Rows[i]["skladiste"]);
            }

            sifra.DataPropertyName = "sifra";
            sifra.HeaderText = "Šifra";
            sifra.Name = "sifra";
            sifra.ReadOnly = true;

            naziv.DataPropertyName = "naziv";
            naziv.HeaderText = "Naziv";
            naziv.Name = "naziv";
            naziv.ReadOnly = true;

            skladiste.DataSource = DTSK;
            skladiste.DataPropertyName = "skladiste";
            skladiste.DisplayMember = "skladiste";
            skladiste.HeaderText = "Skladište";
            skladiste.Name = "skladiste";
            skladiste.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            skladiste.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            skladiste.ValueMember = "id_skladiste";
            //skladiste.FlatStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            //skladiste.DisplayStyle = FlatStyle.System;

            kolicina.DataPropertyName = "kolicina";
            kolicina.HeaderText = "Količina";
            kolicina.Name = "kolicina";

            nab_cijena.DataPropertyName = "nbc";
            if (Class.Postavke.proizvodnja_normativ_pc)
            {
                nab_cijena.HeaderText = "PC";
            }
            else
            {
                nab_cijena.HeaderText = "Nabavna cijena";
            }
            nab_cijena.Name = "nbc";
            //nab_cijena.ReadOnly = true;

            mpc.DataPropertyName = "mpc";
            if (Class.Postavke.proizvodnja_normativ_pc)
            {
                mpc.HeaderText = "NBC";
            }
            else
            {
                mpc.HeaderText = "MPC";
            }
            mpc.Name = "mpc";
            //mpc.ReadOnly = true;

            iznos.DataPropertyName = "iznos";
            iznos.HeaderText = "Iznos";
            iznos.Name = "iznos";
            iznos.ReadOnly = true;

            iznos1.DataPropertyName = "iznos1";
            iznos1.HeaderText = "Iznos ukupno";
            iznos1.Name = "iznos1";
            iznos1.ReadOnly = true;

            nbc.Visible = false;
            nbc.Name = "nbc";

            vpc.Visible = false;
            vpc.Name = "vpc";

            porez.Visible = false;
            porez.Name = "porez";

            id_stavka.Visible = false;
            id_stavka.Name = "id_stavka";

            oduzmi.Visible = false;
            oduzmi.Name = "oduzmi";
        }

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.CurrentCell.ColumnIndex == 2 & dgw.CurrentRow.Cells["oduzmi"].FormattedValue.ToString() == "DA")
            {
                SetCijenaSkladiste();
            }
            else if (dgw.CurrentCell.ColumnIndex == 3)
            {
                if (dgw.CurrentRow.Cells["skladiste"].Value == null & dgw.CurrentRow.Cells["oduzmi"].FormattedValue.ToString() == "DA")
                {
                    MessageBox.Show("Niste odabrali skladište", "Greška");
                    return;
                }

                dgw.CurrentCell.Selected = false;
                dgw.ClearSelection();
                txtSifra_robe.Text = "";
                txtSifra_robe.BackColor = Color.Silver;
                txtSifra_robe.Select();
            }

            //if (MouseButtons != 0) return;

            if (dgw.CurrentCell.ColumnIndex == 2)
            {
                try
                {
                    //dgw.CurrentCell = dgw.CurrentRow.Cells[3];
                    //dgw.BeginEdit(true);
                }
                catch (Exception)
                { }
            }

            izracun();
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.Rows.Count < 1)
                return;
            dgw.BeginEdit(true);
        }

        private string dg(int row, string cell)
        {
            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        #endregion Datagridview helpers
    }
}