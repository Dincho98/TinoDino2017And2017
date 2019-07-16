using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmPosPrinter : Form
    {
        public frmPosPrinter()
        {
            InitializeComponent();
        }

        private DataTable DTPosPrint;

        private void frmPosPrinter_Load(object sender, EventArgs e)
        {
            System.Drawing.Text.PrivateFontCollection privateFonts = new PrivateFontCollection();
            privateFonts.AddFontFile("slike/msgothic.ttc");
            System.Drawing.Font font = new Font(privateFonts.Families[0], 9.5f);

            //classSQL.Setings_Update("UPDATE pos_print SET ispred_artikl='16',ispred_kolicine='5',ispred_cijene='7',ispred_popust='5',ispred_ukupno='7'");

            chbIspisSifreNaPosPrinter.Checked = Class.PosPrint.ispisSifreNaPosPrinter;
            rtfBottom.Font = font;

            FillData();
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private void FillData()
        {
            DTPosPrint = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];

            //Printer
            txtLogicalPrint.Text = DTPosPrint.Rows[0]["logical_name"].ToString();
            txtDevicePrint.Text = DTPosPrint.Rows[0]["device_category"].ToString();
            txtArtikl.Text = DTPosPrint.Rows[0]["ispred_artikl"].ToString();
            txtKolicina.Text = DTPosPrint.Rows[0]["ispred_kolicine"].ToString();
            txtCijena.Text = DTPosPrint.Rows[0]["ispred_cijene"].ToString();
            txtUkupno.Text = DTPosPrint.Rows[0]["ispred_ukupno"].ToString();
            rtfBottom.Text = DTPosPrint.Rows[0]["bottom_text"].ToString();
            txtPopust.Text = DTPosPrint.Rows[0]["ispred_popust"].ToString();
            txtLinijeBottom.Text = DTPosPrint.Rows[0]["linija_praznih_bottom"].ToString();
            txtputanja.Text = DTpostavke.Rows[0]["logopath"].ToString().Trim();

            if (txtputanja.Text != "")
            {
                try
                {
                    pblogo.Image = new Bitmap(txtputanja.Text);
                    pblogo.SizeMode = PictureBoxSizeMode.StretchImage;
                    pblogo.BorderStyle = BorderStyle.Fixed3D;
                    //pictureBox6.SetBounds(650, 400, 80, 80);
                    pblogo.BringToFront();
                }
                catch { }
            }

            //Display
            txtDisplayDevice.Text = DTPosPrint.Rows[0]["lineDisplay_device_category"].ToString();
            txtDisplayLogical.Text = DTPosPrint.Rows[0]["lineDisplay_logicalName"].ToString();

            //Ladica
            txtLogicalLadica.Text = DTPosPrint.Rows[0]["logical_name_drawer"].ToString();
            txtDeviceLadica.Text = DTPosPrint.Rows[0]["device_category_drawer"].ToString();
            txtBrojSlova.Text = DTPosPrint.Rows[0]["broj_slova_na_racunu"].ToString();

            if (DTPosPrint.Rows[0]["posPrinterBool"].ToString() == "1") { chbPrinter.Checked = true; } else { chbPrinter.Checked = false; }
            if (DTPosPrint.Rows[0]["drawer_bool"].ToString() == "1") { chbLadica.Checked = true; } else { chbLadica.Checked = false; }
            if (DTPosPrint.Rows[0]["lineDisplay_bool"].ToString() == "1") { chbDisplayAktivnost.Checked = true; } else { chbDisplayAktivnost.Checked = false; }
            if (DTpostavke.Rows[0]["ispis_cijele_stavke"].ToString() == "1") { cbispisicijelestavke.Checked = true; } else { cbispisicijelestavke.Checked = false; }
            if (DTpostavke.Rows[0]["napomena_na_racunu"].ToString() == "1") { cbNapomenaNaRacunu.Checked = true; } else { cbNapomenaNaRacunu.Checked = false; }
            if (DTpostavke.Rows[0]["a5print"].ToString() == "1") { cba5print.Checked = true; } else { cba5print.Checked = false; }
            if (DTpostavke.Rows[0]["a6print"].ToString() == "1") { cba6print.Checked = true; } else { cba6print.Checked = false; }
            if (DTpostavke.Rows[0]["logo"].ToString() == "1") { cblogo.Checked = true; } else { cblogo.Checked = false; }
            if (DTpostavke.Rows[0]["grafovi"].ToString() == "1") { chbstatistika.Checked = true; } else { chbstatistika.Checked = false; }

            comboBox1.Items.Add("Nije instaliran");
            cbPrinter2.Items.Add("Nije instaliran");

            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                comboBox1.Items.Add(printer.ToString());
                cbPrinter2.Items.Add(printer.ToString());
            }

            comboBox1.Text = DTPosPrint.Rows[0]["windows_printer_name"].ToString();
            cbPrinter2.Text = DTPosPrint.Rows[0]["windows_printer_name2"].ToString();
            if (DTpostavke.Rows[0]["direct_print"].ToString() == "1") { chbStariPrinter.Checked = true; } else { chbStariPrinter.Checked = false; }

            DataTable DTSK = new DataTable("opcija_ladica");
            DTSK.Columns.Add("id_opcija_ladica", typeof(string));
            DTSK.Columns.Add("opcija_ladica", typeof(string));
            DTSK.Rows.Add(0, "Nije podržano otvaranje ladice.");
            DTSK.Rows.Add(1, "Opcija 1");
            DTSK.Rows.Add(2, "Opcija 2");

            cbLadicaStariPrinter.DataSource = DTSK;
            cbLadicaStariPrinter.DisplayMember = "opcija_ladica";
            cbLadicaStariPrinter.ValueMember = "id_opcija_ladica";
            cbLadicaStariPrinter.SelectedValue = DTpostavke.Rows[0]["ladicaOn"].ToString();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 1000);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ladicabool = chbLadica.Checked ? "1" : "0";

            string printerBarCodeBool = chbPrinter.Checked ? "1" : "0";

            string displayBool_ = chbDisplayAktivnost.Checked ? "1" : "0";

            string stariprinter = chbStariPrinter.Checked ? "1" : "0";

            string ispisi_sve_stavke = cbispisicijelestavke.Checked ? "1" : "0";

            string napomena_na_racunu = cbNapomenaNaRacunu.Checked ? "1" : "0";

            string a5print = cba5print.Checked ? "1" : "0";

            string logo = cblogo.Checked ? "1" : "0";

            string statistika = chbstatistika.Checked ? "1" : "0";
            string a6print = cba6print.Checked ? "1" : "0";

            if (cblogo.Checked)
            {
                txtputanja.Text = txtputanja.Text.Trim();

                if (txtputanja.Text == "")
                {
                    MessageBox.Show("Odaberite putanju slike za logo ili ostavite opciju " +
                        "'Logo na fakturi' neoznačenu!", "Upozorenje!");

                    btnpath.PerformClick();

                    txtputanja.Text = txtputanja.Text.Trim();
                }

                if (txtputanja.Text.Trim() == "") return;

                try
                {
                    Image img = Image.FromFile(txtputanja.Text);
                    if (ImageFormat.Jpeg.Equals(img.RawFormat))
                    {
                        // JPEG
                    }
                    else if (ImageFormat.Png.Equals(img.RawFormat))
                    {
                        // PNG
                    }
                    else if (ImageFormat.Gif.Equals(img.RawFormat))
                    {
                        // GIF
                    }
                    else if (ImageFormat.Bmp.Equals(img.RawFormat))
                    {
                        // Bmp
                    }
                    else
                        return;
                }
                catch
                {
                    MessageBox.Show("Odabrana datoteka nije slika!", "Greška!");
                    return;
                }
            }
            else
            {
                txtputanja.Text = "";
            }

            string sql = "UPDATE pos_print SET " +
                " logical_name='" + txtLogicalPrint.Text + "'," +
                " device_category='" + txtDevicePrint.Text + "'," +
                " ispred_kolicine='" + txtKolicina.Text + "'," +
                " ispred_cijene='" + txtCijena.Text + "'," +
                " ispred_popust='" + txtPopust.Text + "'," +
                " ispred_ukupno='" + txtUkupno.Text + "'," +
                " linija_praznih_bottom='" + txtLinijeBottom.Text + "'," +
                " ispred_artikl='" + txtArtikl.Text + "'," +
                " bottom_text='" + rtfBottom.Text + "'," +
                " posPrinterBool='" + printerBarCodeBool + "'," +
                " logical_name_drawer='" + txtLogicalLadica.Text + "'," +
                " device_category_drawer='" + txtDeviceLadica.Text + "'," +
                " drawer_bool='" + ladicabool + "'," +
                " lineDisplay_logicalName='" + txtDisplayLogical.Text + "'," +
                " lineDisplay_device_category='" + txtDisplayDevice.Text + "'," +
                " windows_printer_name='" + comboBox1.Text + "'," +
                " windows_printer_name2='" + cbPrinter2.Text + "'," +
                " broj_slova_na_racunu='" + txtBrojSlova.Text + "'," +
                " lineDisplay_bool='" + displayBool_ + "'" +
                " WHERE id='1'";
            classSQL.Setings_Update(sql);

            classSQL.Setings_Update("UPDATE postavke SET direct_print='" + stariprinter + "'");
            classSQL.Setings_Update("UPDATE postavke SET ispis_cijele_stavke = '" + ispisi_sve_stavke + "'," +
                " napomena_na_racunu='" + napomena_na_racunu + "', a5print ='" + a5print + "', a6print='" + a6print + "', logopath = '" + txtputanja.Text + "', " +
                " logo = '" + logo + "' ");

            sql = "UPDATE postavke SET ladicaOn='" + cbLadicaStariPrinter.SelectedValue + "', grafovi='" + statistika + "'";
            classSQL.Setings_Update(sql);

            MessageBox.Show("Spremljeno.");
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    PosPrint.classCashDrawer.OpenPortCashDrawer();
        //    PosPrint.classCashDrawer.OpenCashDrawer();
        //    PosPrint.classCashDrawer.ClosePortCashDrawer();
        //}

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    PosPrint.classLineDisplay.StartDisplay();
        //    PosPrint.classLineDisplay.WriteOnDisplay("Vaš display\nradi ispravno.");
        //    PosPrint.classLineDisplay.CloseDisplay();
        //}

        private void btnTest_Click(object sender, EventArgs e)
        {
            DataTable DTsend = new DataTable();
            DTsend.Columns.Add("mpc");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("cijena");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("porez_potrosnja");
            DTsend.Columns.Add("ime");
            DataRow row;

            row = DTsend.NewRow();
            row["mpc"] = "125";
            row["porez"] = "25";
            row["kolicina"] = "1";
            row["rabat"] = "0";
            row["cijena"] = "100";
            row["ime"] = "Artikl 1";
            row["vpc"] = "50";
            row["porez_potrosnja"] = "0";
            DTsend.Rows.Add(row);

            row = DTsend.NewRow();
            row["mpc"] = "1250";
            row["porez"] = "25";
            row["kolicina"] = "1";
            row["rabat"] = "0";
            row["cijena"] = "1000";
            row["ime"] = "Artikl 2";
            row["vpc"] = "100";
            row["porez_potrosnja"] = "0";
            DTsend.Rows.Add(row);

            DataTable DTpostavkePrinter = classSQL.select("SELECT * FROM pos_print", "pos_print").Tables[0];
            string mali = DTpostavkePrinter.Rows[0]["posPrinterBool"].ToString();

            //CheckPosEquipment(PosPrint.classPosPrint.ConnectToPrinter());
            DateTime[] datumi = new DateTime[2];
            datumi[0] = DateTime.Now;
            datumi[1] = datumi[0];
            if (mali == "1") PosPrint.classPosPrintMaloprodaja2.PrintReceipt(DTsend, "Probni blagajnik", "1000000",
                "", "12345678", "100000", "G", datumi, false, mali, false, true, "1", "1");
            else MessageBox.Show("Mali printer nije uključen u postavkama.", "Upozorenje!");
            //PosPrint.classPosPrint.DisconnectFromPrinter();
        }

        private void CheckPosEquipment(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str, "Greška");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void btnpath_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Title = "Open Image";
            openFileDialog1.Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            txtputanja.Text = openFileDialog1.FileName;
                            pblogo.Image = new Bitmap(openFileDialog1.FileName);
                            pblogo.SizeMode = PictureBoxSizeMode.StretchImage;
                            pblogo.BorderStyle = BorderStyle.Fixed3D;
                            //pictureBox6.SetBounds(650, 400, 80, 80);
                            pblogo.BringToFront();
                            // Add the new control to its parent's controls collection
                            //this.Controls.Add(pictureBox6);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void lbla5print_Click(object sender, EventArgs e)
        {
        }

        private void chbIspisSifreNaPosPrinter_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("update pos_print set ispis_sifre_na_pos_printer = {0};", (chbIspisSifreNaPosPrinter.Checked ? 1 : 0));
                //string.Format("update postavke set proizvodnja_faktura_nbc = {0};", (chbSNBC.Checked ? 1 : 0));
                classSQL.Setings_Update(sql);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void frmPosPrinter_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Class.PosPrint.GetPosPrint();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}