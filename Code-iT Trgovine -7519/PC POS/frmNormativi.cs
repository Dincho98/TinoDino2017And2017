using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmNormativi : Form
    {
        public string broj_normativa_edit { get; set; }

        public frmNormativi()
        {
            InitializeComponent();
        }

        private bool edit = false;
        private DataTable DTRoba = new DataTable();
        private DataSet DS_Skladiste = new DataSet();
        private DataTable DTpostavke = new DataTable();
        public frmMenu MainForm { get; set; }

        private void frmNormativi_Load(object sender, EventArgs e)
        {
            if (Class.Postavke.proizvodnja_normativ_pc)
            {
                lblVpc.Text = "PC";

                lblMpc.Enabled = false;
                lblMpc.Visible = false;

                txtMpcNormativ.Enabled = false;
                txtMpcNormativ.Visible = false;
            }

            txtBrojNormativa.Text = brojNormativa();
            txtSifraArtikla.Select();
            numeric();
            EnableDisable(false);
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();

            DataTable DTSK = new DataTable("Roba");
            DTSK.Columns.Add("id_skladiste", typeof(string));
            DTSK.Columns.Add("skladiste", typeof(string));
            DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

            DS_Skladiste = classSQL.select("SELECT * FROM skladiste", "skladiste");
            for (int i = 0; i < DS_Skladiste.Tables[0].Rows.Count; i++)
            {
                DTSK.Rows.Add(DS_Skladiste.Tables[0].Rows[i]["id_skladiste"], DS_Skladiste.Tables[0].Rows[i]["skladiste"]);
            }

            if (edit == true)
            {
                zbroji_artikle();
            }

            skladiste.DataSource = DTSK;
            skladiste.DataPropertyName = "skladiste";
            skladiste.DisplayMember = "skladiste";
            skladiste.HeaderText = "Skladište";
            skladiste.Name = "skladiste";
            skladiste.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            skladiste.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            skladiste.ValueMember = "id_skladiste";

            txtBrojNormativa.Select();
            ControlDisableEnable(1, 0, 0, 1, 0);

            if (broj_normativa_edit != null) { fillNormativ(); }
            //PaintRows(dgw);
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 1000);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        //private void PaintRows(DataGridView dg)
        //{
        //    int br = 0;
        //    for (int i = 0; i < dg.Rows.Count; i++)
        //    {
        //        if (br == 0)
        //        {
        //            dg.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
        //            br++;
        //        }
        //        else
        //        {
        //            dg.Rows[i].DefaultCellStyle.BackColor = Color.White;
        //            br = 0;
        //        }

        //    }
        //    DataGridViewRow row = dg.RowTemplate;
        //    row.Height = 25;
        //}

        //private void dgw_CellValidated(object sender, DataGridViewCellEventArgs e)
        //{
        //    int row = dgw.CurrentCell.RowIndex;
        //    if (dgw.CurrentCell.ColumnIndex == 2)
        //    {
        //        //SetCijenaSkladiste();
        //    }

        //    else if (dgw.CurrentCell.ColumnIndex == 3)
        //    {
        //        if (dgw.CurrentRow.Cells["skladiste"].Value == null)
        //        {
        //            MessageBox.Show("Niste odabrali skladište", "Greška");
        //            return;
        //        }

        //        //dgw.Rows[0].Selected = false;
        //        txtSifra_robe.Text = "";
        //        txtSifra_robe.BackColor = Color.Silver;
        //        txtSifra_robe.Select();
        //    }
        //    //izracun();
        //}

        //DataSet DSprovjeraRobaProdaja = new DataSet();
        //private void ProvjeriDaliPostojiRobaProdaja(string sif, object skl, int r)
        //{
        //    DSprovjeraRobaProdaja = classSQL.select("SELECT * FROM roba_prodaja WHERE sifra='" + sif + "' AND id_skladiste='" + skl.ToString() + "'", "roba_prodaja");

        //    if (DSprovjeraRobaProdaja.Tables[0].Rows.Count == 0)
        //    {
        //        string sql = "INSERT INTO roba_prodaja (id_skladiste,kolicina,nc,vpc,porez,sifra) VALUES" +
        //            "('" + skl.ToString() + "','0','" + dg(r, "nc").Replace(",", ".") + "','" + dg(r, "vpc").Replace(",", ".") + "','" + dg(r, "porez") + "','" + sif + "')";
        //        classSQL.insert(sql);
        //    }

        //}

        #region Update/fill normativ

        private void UPDATEnormativ()
        {
            string sql = "UPDATE normativi SET " +
                " broj_normativa='" + txtBrojNormativa.Text.Trim() + "'," +
                " sifra_artikla='" + txtSifraArtikla.Text + "'," +
                " komentar='" + txtKomentar.Text + "'," +
                " godina_normativa='" + nuGodina.Value.ToString() + "'" +
                " WHERE broj_normativa='" + txtBrojNormativa.Text.Trim() + "'";
            provjera_sql(classSQL.update(sql));

            string sql_stavke;
            for (int b = 0; b < dgw.Rows.Count; b++)
            {
                decimal kol;
                decimal.TryParse(dg(b, "kolicina").Replace(".", ","), out kol);

                if (dgw.Rows[b].Cells["id_stavka"].Value != null)
                {
                    sql = "UPDATE normativi_stavke SET " +
                    " sifra_robe='" + dg(b, "sifra") + "'," +
                    " kolicina='" + kol.ToString().Replace(".", ",") + "'," +
                    " id_skladiste='" + dgw.Rows[b].Cells["skladiste"].Value + "'" +
                    " WHERE id_stavka='" + dg(b, "id_stavka") + "'";
                    provjera_sql(classSQL.update(sql));
                }
                else
                {
                    sql_stavke = "INSERT INTO normativi_stavke " +
                    "(sifra_robe,kolicina,id_skladiste,broj_normativa)" +
                    "VALUES" +
                    "(" +
                    "'" + dg(b, "sifra") + "'," +
                    "'" + kol.ToString().Replace(".", ",") + "'," +
                    "'" + dgw.Rows[b].Cells[1].Value + "'," +
                    "'" + txtBrojNormativa.Text.Trim() + "'" +
                    ")";
                    provjera_sql(classSQL.insert(sql_stavke));
                }
            }

            MessageBox.Show("Spremljeno.");
            edit = false;
            EnableDisable(false);
            deleteFields();
            btnSpremi.Enabled = false;
            btnSve.Enabled = true;

            classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Editiranje ponude br." + txtBrojNormativa.Text + "')");
        }

        private DataTable DSnormativi = new DataTable();

        private void fillNormativ()
        {
            edit = true;
            ControlDisableEnable(0, 1, 1, 0, 1);
            //fill header

            DSnormativi = classSQL.select("SELECT * FROM normativi WHERE broj_normativa = '" + broj_normativa_edit + "'", "fakture").Tables[0];
            txtBrojNormativa.Text = DSnormativi.Rows[0]["broj_normativa"].ToString();
            nuGodina.Value = Convert.ToInt16(DSnormativi.Rows[0]["godina_normativa"].ToString());
            txtSifraArtikla.Text = DSnormativi.Rows[0]["sifra_artikla"].ToString();

            DataTable DSoArtiklu = new DataTable();
            string ss = "SELECT roba.naziv,roba.jm,grupa.grupa FROM roba INNER JOIN grupa ON roba.id_grupa=grupa.id_grupa WHERE sifra ='" + DSnormativi.Rows[0]["sifra_artikla"].ToString() + "'";
            DSoArtiklu = classSQL.select(ss, "roba").Tables[0];
            txtJedinicaMjere.Text = DSoArtiklu.Rows[0]["jm"].ToString();
            txtImeArtikla.Text = DSoArtiklu.Rows[0]["naziv"].ToString();
            txtVrstaRobe.Text = DSoArtiklu.Rows[0]["grupa"].ToString();
            txtKomentar.Text = DSnormativi.Rows[0]["komentar"].ToString();
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + DSnormativi.Rows[0]["id_zaposlenik"].ToString() + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();

            //--------fill normativ stavke------------------------------

            DataTable dtR = new DataTable();
            dtR = classSQL.select("SELECT normativi_stavke.sifra_robe,normativi_stavke.kolicina,normativi_stavke.id_skladiste,normativi_stavke.id_stavka," +
                "roba.naziv,roba.jm FROM normativi_stavke " +
                " INNER JOIN roba ON roba.sifra = normativi_stavke.sifra_robe WHERE broj_normativa  = '" + DSnormativi.Rows[0]["broj_normativa"].ToString() + "'", "broj_ponude").Tables[0];

            for (int i = 0; i < dtR.Rows.Count; i++)
            {
                dgw.Rows.Add();
                int br = dgw.Rows.Count - 1;

                dgw.Rows[br].Cells["sifra"].Value = dtR.Rows[i]["sifra_robe"].ToString();
                dgw.Rows[br].Cells["kolicina"].Value = dtR.Rows[i]["kolicina"].ToString();
                dgw.Rows[br].Cells["skladiste"].Value = dtR.Rows[i]["id_skladiste"].ToString();
                dgw.Rows[br].Cells["jmj"].Value = dtR.Rows[i]["jm"].ToString();
                dgw.Rows[br].Cells["naziv"].Value = dtR.Rows[i]["naziv"].ToString();
                dgw.Rows[br].Cells["id_stavka"].Value = dtR.Rows[i]["id_stavka"].ToString();

                //if (Class.Postavke.proizvodnja_normativ_pc) {
                //}

                dgw.Select();
                dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[1];
                //PaintRows(dgw);
            }
            zbroji_artikle();
        }

        #endregion Update/fill normativ

        #region Buttons

        private void btnIzlaz_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnArtikli_Click(object sender, EventArgs e)
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

                string sql = "SELECT * FROM roba WHERE sifra='" + propertis_sifra + "'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifraArtikla.Text = DTRoba.Rows[0]["sifra"].ToString();
                    txtImeArtikla.Text = DTRoba.Rows[0]["naziv"].ToString();
                    txtJedinicaMjere.Text = DTRoba.Rows[0]["jm"].ToString();
                    txtKomentar.Select();
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnOpenRoba_Click(object sender, EventArgs e)
        {
            frmRobaTrazi roba_trazi = new frmRobaTrazi();
            roba_trazi.idSkladiste = Class.Postavke.id_default_skladiste_normativ;
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

                string sql = "SELECT * FROM roba WHERE sifra='" + propertis_sifra + "'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();
                    dgw.Select();
                    dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[3];
                    txtBrojNormativa.Enabled = false;
                    nuGodina.Enabled = false;
                    zbroji_artikle();
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void zbroji_artikle()
        {
            decimal vpc = 0;
            decimal mpc = 0;
            decimal kol = 0;
            decimal proizvodacka_cijena = 0;
            string sifra = "";
            for (int y = 0; y < dgw.Rows.Count; y++)
            {
                sifra = dgw.Rows[y].Cells["sifra"].FormattedValue.ToString();
                kol = Convert.ToDecimal(dgw.Rows[y].Cells["kolicina"].FormattedValue.ToString());
                string sql = "SELECT vpc, mpc, proizvodacka_cijena FROM roba WHERE sifra='" + sifra + "'";
                DataTable DTcijene = classSQL.select(sql, "roba").Tables[0];
                decimal vpc_t = Convert.ToDecimal(DTcijene.Rows[0]["vpc"].ToString());
                decimal mpc_t = Convert.ToDecimal(DTcijene.Rows[0]["mpc"].ToString());
                decimal proizvodacka_cijena_t = Convert.ToDecimal(DTcijene.Rows[0]["proizvodacka_cijena"].ToString());
                vpc += vpc_t * kol;
                mpc += mpc_t * kol;
                proizvodacka_cijena += proizvodacka_cijena_t * kol;
            }

            if (Class.Postavke.proizvodnja_normativ_pc)
            {
                txtVpcNormativ.Text = proizvodacka_cijena.ToString("#0.00");
            }
            else
            {
                txtVpcNormativ.Text = vpc.ToString();
                txtMpcNormativ.Text = mpc.ToString();
            }
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() != "")
            {
                if (MessageBox.Show("Da li ste sigurni da želite obrisati ovu stavku?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    provjera_sql(classSQL.delete("DELETE FROM normativi_stavke WHERE id_stavka='" + dgw.Rows[dgw.CurrentRow.Index].Cells["id_stavka"].FormattedValue.ToString() + "'"));
                    dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                    provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje stavke sa normativa br." + txtBrojNormativa.Text + "')"));
                    zbroji_artikle();
                    MessageBox.Show("Obrisano.");
                }
            }
            else
            {
                dgw.Rows.RemoveAt(dgw.CurrentRow.Index);
                zbroji_artikle();
                MessageBox.Show("Obrisano.");
            }
        }

        private void btnDeleteAllRN_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Brisanjem ove fakture brišete i količinu robe sa skladišta. Da li ste sigurni da želite obrisati ovu fakturu?", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                classSQL.delete("DELETE FROM normativi_stavke WHERE broj_normativa='" + txtBrojNormativa.Text + "'");
                classSQL.delete("DELETE FROM normativi WHERE broj_normativa='" + txtBrojNormativa.Text + "'");
                classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Brisanje cijelog normativa br." + txtBrojNormativa.Text + "')");
                MessageBox.Show("Obrisano.");

                edit = false;
                EnableDisable(false);
                deleteFields();
                btnDeleteAll.Enabled = false;
                btnObrisi.Enabled = false;
                ControlDisableEnable(1, 0, 0, 1, 0);
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            btnSve.Enabled = true;
            EnableDisable(false);
            deleteFields();
            txtBrojNormativa.Text = brojNormativa();
            edit = false;
            btnDeleteAll.Enabled = false;
            txtSifraArtikla.ReadOnly = false;
            txtBrojNormativa.ReadOnly = false;
            nuGodina.ReadOnly = false;
            txtBrojNormativa.Enabled = true;
            nuGodina.Enabled = true;
            txtMpcNormativ.Text = "0";
            txtVpcNormativ.Text = "0";
            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            edit = false;
            EnableDisable(true);
            btnSve.Enabled = false;
            txtBrojNormativa.Text = brojNormativa();
            btnDeleteAll.Enabled = false;
            txtSifraArtikla.ReadOnly = false;
            txtMpcNormativ.Text = "0";
            txtVpcNormativ.Text = "0";
            ControlDisableEnable(0, 1, 1, 0, 1);
        }

        private void btnSviRN_Click(object sender, EventArgs e)
        {
            frmSviNormativi sf = new frmSviNormativi();
            sf.sifra_normativa = "";
            sf.MainForm = this;
            sf.ShowDialog();
            if (broj_normativa_edit != null)
            {
                dgw.Rows.Clear();
                fillNormativ();
                EnableDisable(true);
                edit = true;
                sf.Enabled = true;
                txtSifraArtikla.ReadOnly = true;
                txtBrojNormativa.ReadOnly = true;
                nuGodina.ReadOnly = true;
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            decimal iznosNBC = 0;
            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                if (dgw.Rows[i].Cells["skladiste"].Value == null)
                {
                    MessageBox.Show("Normativ nije spremljen zbog ne odabira skladišta na pojedinim artiklima.", "Greška");
                    return;
                }

                string sifra = dgw.Rows[i].Cells["sifra"].FormattedValue.ToString();
                string skladiste = dgw.Rows[i].Cells["skladiste"].Value.ToString();
                string god = DateTime.Now.Year.ToString();
                DataTable DT = classSQL.select("SELECT ProvjeraNabavneCijene('" + sifra + "','" + god + @"-12-31 23:59:59'," + skladiste + ");", "nbc").Tables[0];
                if (DT.Rows.Count > 0)
                {
                    double nbc = 0;
                    double kolicina = 0;
                    double.TryParse(DT.Rows[0][0].ToString(), out nbc);
                    double.TryParse(dgw.Rows[i].Cells["kolicina"].FormattedValue.ToString(), out kolicina);

                    if (!Class.Dokumenti.isKasica && ((Class.PodaciTvrtka.oibTvrtke == Class.Postavke.OIB_PC1 && Util.Korisno.GodinaKojaSeKoristiUbazi >= Class.Postavke.GODINA_POCETKA_FIFO_PC1) || Util.Korisno.GodinaKojaSeKoristiUbazi >= Class.Postavke.GODINA_POCETKA_FIFO))
                    {
                        decimal _nbc = Class.FIFO.getNbc(Util.Korisno.GodinaKojaSeKoristiUbazi, DateTime.Now, Convert.ToInt32(skladiste), kolicina, dgw.Rows[i].Cells["sifra"].Value.ToString(), nbc);
                    }

                    iznosNBC += (decimal)(nbc * kolicina);
                }
            }

            /*if (stavke.Length > 0)
            {
                stavke = stavke.TrimEnd(',');
                string god = DateTime.Now.Year.ToString();
                string sqlU = @"UPDATE roba_prodaja SET nc=(SELECT ProvjeraNabavneCijene(roba_prodaja.sifra,'" + god + @"-12-31 23:59:59',CAST(roba_prodaja.id_skladiste AS INTEGER)))
                             WHERE roba_prodaja.sifra IN(
	                            SELECT * FROM
	                            (
		                            SELECT sifra_robe as sifra FROM racun_stavke GROUP BY sifra_robe
		                            UNION
		                            SELECT sifra as sifra FROM faktura_stavke GROUP BY sifra
		                            UNION
		                            SELECT sifra as sifra FROM kalkulacija_stavke GROUP BY sifra
	                            ) koristivo WHERE roba_prodaja.sifra IN (" +stavke+@") ORDER BY sifra ASC
                            );";
                classSQL.update(sqlU);
            }*/

            if (edit == true)
            {
                classSQL.update("UPDATE roba SET nc='" + iznosNBC.ToString().Replace(".", ",") + "' WHERE sifra='" + txtSifraArtikla.Text + "';");
                classSQL.update("UPDATE roba_prodaja SET nc='" + iznosNBC.ToString().Replace(".", ",") + "' WHERE sifra='" + txtSifraArtikla.Text + "';");

                UPDATEnormativ();
                EnableDisable(false);
                deleteFields();
                btnSve.Enabled = true;
                ControlDisableEnable(1, 0, 0, 1, 0);
                return;
            }

            if (Convert.ToInt32(classSQL.select("SELECT count(*) as br from normativi where sifra_artikla = '" + txtSifraArtikla.Text + "';", "normativi").Tables[0].Rows[0]["br"]) != 0)
            {
                MessageBox.Show("Nije dozvoljeno upisivati više normativa s istom šifrom.");
                return;
            }

            classSQL.update("UPDATE roba SET nc='" + iznosNBC.ToString().Replace(".", ",") + "' WHERE sifra='" + txtSifraArtikla.Text + "';");
            classSQL.update("UPDATE roba_prodaja SET nc='" + iznosNBC.ToString().Replace(".", ",") + "' WHERE sifra='" + txtSifraArtikla.Text + "';");
            if (brojNormativa() != txtBrojNormativa.Text)
            {
                txtBrojNormativa.Text = brojNormativa();
                MessageBox.Show("Broj normativa je več iskorišten, dodijeljen vam je novi broj: " + txtBrojNormativa.Text);
            }

            string sql = "INSERT INTO normativi (broj_normativa,sifra_artikla,komentar,godina_normativa,id_zaposlenik) VALUES " +
                " (" +
                 " '" + txtBrojNormativa.Text + "'," +
                " '" + txtSifraArtikla.Text + "'," +
                " '" + txtKomentar.Text + "'," +
                " '" + nuGodina.Value.ToString() + "'," +
                " '" + Properties.Settings.Default.id_zaposlenik + "'" +
                ")";
            provjera_sql(classSQL.insert(sql));

            string sql_stavke = "";
            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                //ProvjeriDaliPostojiRobaProdaja(dg(i, "sifra"), dgw.Rows[i].Cells[3].Value, i);

                decimal kol; decimal.TryParse(dg(i, "kolicina").Replace(".", ","), out kol);

                sql_stavke = "INSERT INTO normativi_stavke " +
                "(sifra_robe,kolicina,id_skladiste,broj_normativa)" +
                " VALUES" +
                "(" +
                "'" + dg(i, "sifra") + "'," +
                "'" + kol.ToString().Replace(".", ",") + "'," +
                "'" + dgw.Rows[i].Cells[1].Value + "'," +
                "'" + txtBrojNormativa.Text.Trim() + "'" +
                ")";
                provjera_sql(classSQL.insert(sql_stavke));
            }

            MessageBox.Show("Spremljeno.");
            classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES ('" +
                Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") +
                "','Izrada novog normativa br." + txtBrojNormativa.Text + "')");

            ControlDisableEnable(1, 0, 0, 1, 0);
            edit = false;
            EnableDisable(false);
            deleteFields();
            btnSve.Enabled = true;
        }

        #endregion Buttons

        #region Util

        private void SetRoba()
        {
            dgw.Rows.Add();
            int br = dgw.Rows.Count - 1;

            dgw.Select();
            dgw.CurrentCell = dgw.Rows[br].Cells[1];
            dgw.BeginEdit(true);

            dgw.Rows[br].Cells["sifra"].Value = DTRoba.Rows[0]["sifra"].ToString();
            dgw.Rows[br].Cells["naziv"].Value = DTRoba.Rows[0]["naziv"].ToString();
            dgw.Rows[br].Cells["kolicina"].Value = "1";
            dgw.Rows[br].Cells["jmj"].Value = string.Format(DTRoba.Rows[0]["jm"].ToString());
            dgw.Rows[br].Cells["skladiste"].Value = Class.Postavke.id_default_skladiste_normativ.ToString();
            //if (Class.Postavke.proizvodnja_normativ_pc)
            //{
            //    dgw.Rows[br].Cells["PC"].Value = DTpostavke.Rows[0]["proizvodacka_cijena"].ToString();
            //    dgw.Rows[br].Cells["PC_iznos"].Value = DTpostavke.Rows[0]["proizvodacka_cijena"].ToString();
            //}

            //PaintRows(dgw);
        }

        private void numeric()
        {
            nuGodina.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            nuGodina.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            nuGodina.Value = DateTime.Now.Year;
        }

        private string brojNormativa()
        {
            DataTable DSbr = classSQL.select("SELECT (COALESCE(MAX(broj_normativa), 0) zbroj 1) as broj_normativa FROM normativi", "normativi").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString())).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void EnableDisable(bool x)
        {
            txtSifra_robe.Enabled = x;
            btnObrisi.Enabled = x;
            btnOpenRoba.Enabled = x;
            txtSifraArtikla.Enabled = x;
            txtKomentar.Enabled = x;
            btnSpremi.Enabled = x;

            if (x == true)
            {
                nuGodina.Enabled = false;
                txtBrojNormativa.Enabled = false;
            }
            else
            {
                nuGodina.Enabled = true;
                txtBrojNormativa.Enabled = true;
            }
        }

        private void deleteFields()
        {
            //txtBrojNormativa.Text = brojNormativa();
            txtSifraArtikla.Text = "";
            txtSifra_robe.Text = "";
            txtKomentar.Text = "";
            txtImeArtikla.Text = "";
            txtJedinicaMjere.Text = "";
            txtVrstaRobe.Text = "";

            dgw.Rows.Clear();
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void ControlDisableEnable(int novi, int odustani, int spremi, int sve, int delAll)
        {
            if (novi == 0)
            {
                btnNoviUnos.Enabled = false;
            }
            else if (novi == 1)
            {
                btnNoviUnos.Enabled = true;
            }

            if (odustani == 0)
            {
                btnOdustani.Enabled = false;
            }
            else if (odustani == 1)
            {
                btnOdustani.Enabled = true;
            }

            if (spremi == 0)
            {
                btnSpremi.Enabled = false;
            }
            else if (spremi == 1)
            {
                btnSpremi.Enabled = true;
            }

            if (sve == 0)
            {
                btnSve.Enabled = false;
            }
            else if (sve == 1)
            {
                btnSve.Enabled = true;
            }

            if (delAll == 0)
            {
                btnDeleteAll.Enabled = false;
            }
            else if (delAll == 1)
            {
                btnDeleteAll.Enabled = true;
            }
        }

        #endregion Util

        #region KeyDown Event

        private void txtSifra_robe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSifra_robe.Text.Trim().Length == 0)
                {
                    btnOpenRoba.PerformClick();
                    return;
                }
                for (int y = 0; y < dgw.Rows.Count; y++)
                {
                    if (txtSifra_robe.Text == dgw.Rows[y].Cells["sifra"].FormattedValue.ToString().TrimEnd())
                    {
                        MessageBox.Show("Artikl ili usluga već postoje u ovom normativu", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string sql = "SELECT * FROM roba WHERE sifra='" + txtSifra_robe.Text + "'";

                DTRoba = classSQL.select(sql, "roba").Tables[0];
                if (DTRoba.Rows.Count > 0)
                {
                    txtSifra_robe.BackColor = Color.White;
                    SetRoba();

                    txtBrojNormativa.Enabled = false;
                    nuGodina.Enabled = false;
                    txtSifraArtikla.ReadOnly = true;
                }
                else
                {
                    MessageBox.Show("Za ovu šifru ne postoji artikl ili usluga.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtSifraArtikla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DSpar = classSQL.select("SELECT roba.naziv,roba.jm,grupa.grupa FROM roba INNER JOIN grupa ON roba.id_grupa=grupa.id_grupa WHERE sifra='" + txtSifraArtikla.Text + "'", "roba").Tables[0];
                if (DSpar.Rows.Count > 0)
                {
                    txtImeArtikla.Text = DSpar.Rows[0]["naziv"].ToString();
                    txtJedinicaMjere.Text = DSpar.Rows[0]["jm"].ToString();
                    txtVrstaRobe.Text = DSpar.Rows[0]["grupa"].ToString();
                    txtKomentar.Select();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void txtKolicina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtKomentar.Select();
            }
        }

        private void txtKomentar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSifra_robe.Select();
            }
        }

        private void txtBrojNormativa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT broj_normativa FROM normativi WHERE  broj_normativa='" + txtBrojNormativa.Text + "'", "normativi").Tables[0];
                deleteFields();
                if (DT.Rows.Count == 0)
                {
                    if (brojNormativa() == txtBrojNormativa.Text.Trim())
                    {
                        edit = false;
                        EnableDisable(true);
                        txtBrojNormativa.Text = brojNormativa();
                        txtBrojNormativa.ReadOnly = true;
                        txtBrojNormativa.ReadOnly = true;
                        ControlDisableEnable(0, 1, 1, 0, 1);
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                        return;
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    broj_normativa_edit = txtBrojNormativa.Text;
                    fillNormativ();
                    EnableDisable(true);
                    edit = true;
                    txtBrojNormativa.ReadOnly = true;
                    nuGodina.ReadOnly = true;
                }
                txtSifraArtikla.Select();
            }
        }

        #endregion KeyDown Event

        #region datagridview helpers

        private string dg(int row, string cell)
        {
            return dgw.Rows[row].Cells[cell].FormattedValue.ToString();
        }

        private void dgw_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (MouseButtons != 0) return;

            if (dgw.CurrentCell.ColumnIndex == 1)
            {
                try
                {
                    dgw.CurrentCell = dgw.CurrentRow.Cells[3];
                    dgw.BeginEdit(true);
                }
                catch (Exception)
                {
                }
            }
            else if (dgw.CurrentCell.ColumnIndex == 3)
            {
                try
                {
                    txtSifra_robe.Text = "";
                    txtSifra_robe.BackColor = Color.Silver;
                    txtSifra_robe.Select();
                    //if (Class.Postavke.proizvodnja_normativ_pc) {
                    //    decimal pc = 0, k = 0;

                    //    decimal.TryParse(dgw.CurrentRow.Cells["PC"].Value.ToString(), out pc);
                    //    decimal.TryParse(dgw.CurrentRow.Cells["kolicina"].Value.ToString(), out k);

                    //    dgw.CurrentRow.Cells["PC_iznos"].Value = (pc * k).ToString("#0,00");
                    //}

                    dgw.ClearSelection();
                }
                catch (Exception)
                {
                }
            }
            zbroji_artikle();
        }

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw.Rows.Count < 1)
                return;
            dgw.BeginEdit(true);
        }

        #endregion datagridview helpers
    }
}