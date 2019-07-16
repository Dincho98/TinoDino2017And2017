using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PCPOS.Kartoteka
{
    public partial class galerija : Form
    {
        private bool uredi = false;
        private Int32 var1 = 0;
        private Int32 var2 = 0;
        private Int32 var3 = 0;
        private Int32 var4 = 0;
        private Int32 var5 = 0;
        private Int32 var6 = 0;

        public galerija()
        {
            InitializeComponent();
        }

        public frmPartnerTrazi partner { get; set; }
        public string ime_kupca { get; set; }
        public string prezime_kupca { get; set; }
        public string partner_id { get; set; }
        public string ime_tvrtke { get; set; }
        public string broj_racuna { get; set; }
        public System.Windows.Forms.PictureBox[] imgArray;	//Declaring array of PictureBox

        // public static because we need in another Class (frmView)
        public static string ImageToShow;

        public static string ImageToShow1;
        private int NumOfFiles;
        private string[] imgName;
        private string[] imgExtension; // for Extension of file(JPG, BMP, PNG, GIF)
        private string path = "";

        private void ImagesInFolder()
        {
            FileInfo FInfo;
            // Fill the array (imgName) with all images in any folder
            imgName = Directory.GetFiles(Application.StartupPath + @"\slike\" + partner_id + "");
            // How many Picture files in this folder
            NumOfFiles = imgName.Length;
            imgExtension = new string[NumOfFiles];
            for (int i = 0; i < NumOfFiles; i++)
            {
                FInfo = new FileInfo(imgName[i]);
                imgExtension[i] = FInfo.Extension; // We need to know the Extension
            }
        }

        private void provjeri()
        {
            path = Path.GetDirectoryName(Application.ExecutablePath) + "\\Slike\\" + partner_id + "";
            // provjeri ako postoji folder
            if (Directory.Exists(path))
            {
                return;
            }

            // Stvori folder ako ne postoji
            DirectoryInfo di = Directory.CreateDirectory(path);
            Directory.GetCreationTime(path);
        }

        private void ShowFolderImages()
        {
            int Xpos = 8;
            int Ypos = 8;
            Image img;
            Image.GetThumbnailImageAbort myCallback =
                new Image.GetThumbnailImageAbort(ThumbnailCallback);

            string[] Ext = new string[] { ".GIF", ".JPG", ".BMP", ".PNG" };
            AddControls(NumOfFiles);
            for (int i = 0; i < NumOfFiles; i++)
            {
                switch (imgExtension[i].ToUpper())
                {
                    case ".JPG":
                    case ".BMP":
                    case ".GIF":
                    case ".PNG":
                        img = Image.FromFile(imgName[i]); // or img = new Bitmap(imgName[i]);
                        imgArray[i].Image = img.GetThumbnailImage(64, 64, myCallback, IntPtr.Zero);
                        img = null;
                        if (Xpos > 432) // six images in a line
                        {
                            Xpos = 8; // leave eight pixels at Left
                            Ypos = Ypos + 72;  // height of image + 8
                        }
                        imgArray[i].Left = Xpos;
                        imgArray[i].Top = Ypos;
                        imgArray[i].Width = 64;
                        imgArray[i].Height = 64;
                        imgArray[i].Visible = true;
                        // Fill the (Tag) with name and full path of image
                        imgArray[i].Tag = imgName[i];
                        imgArray[i].MouseDown += new System.Windows.Forms.MouseEventHandler(this.backPanel_MouseDown);
                        imgArray[i].ContextMenuStrip = this.contextMenuStrip1;
                        this.backPanel.Controls.Add(imgArray[i]);
                        Xpos = Xpos + 72; // width of image + 8
                        Application.DoEvents();
                        break;
                }
            }
        }

        private bool ThumbnailCallback()
        {
            return false;
        }

        // Function to add PictureBox Controls
        private void AddControls(int cNumber)
        {
            imgArray = new System.Windows.Forms.PictureBox[cNumber]; // assign number array
            for (int i = 0; i < cNumber; i++)
            {
                imgArray[i] = new System.Windows.Forms.PictureBox(); // Initialize one variable
            }
            // When call this function you determine number of controls
        }

        private void podaci()
        {
        }

        private void galerija_Load(object sender, EventArgs e)
        {
            SetRemoteFields();
            provjeri();
            label4.Text = broj_racuna;
            if (Directory.Exists(path))
            {
                ImagesInFolder(); // Load images
                ShowFolderImages(); // Show images on PictureBox array
            }

            this.Text = "galerija";

            string popun = "SELECT datum , napomena, id, id_partner, broj_racuna FROM kar_kronologija WHERE id_partner = '" + partner_id + "' ORDER BY datum DESC";
            DataTable DT = classSQL.select(popun, "partner").Tables[0];

            dgv1.Rows.Clear();

            foreach (DataRow row in DT.Rows)
            {
                dgv1.Rows.Add(row["datum"].ToString(),
                    row["napomena"].ToString(),
                    row["broj_racuna"].ToString(),
                    row["id"].ToString(),
                    row["id_partner"].ToString());
            }

            dgv1.Columns[3].Visible = false;
            dgv1.Columns[4].Visible = false;

            PaintRows(dgv1);
            if (ime_kupca != "" || prezime_kupca != "")
            {
                lblimekrono.Text = ime_kupca;

                lblprezimekrono.Text = prezime_kupca;
            }
            else
            {
                lblimekrono.Text = ime_tvrtke;
            }

            if (lblprezimekrono.Text == "Prezime")
            {
                lblprezimekrono.Visible = false;
            }
            else
            {
                lblprezimekrono.Visible = true;
            }
            podaci();
        }

        private void PaintRows(DataGridView dg)
        {
            int br = 0;
            for (int i = 0; i < dg.Rows.Count; i++)
            {
                if (br == 0)
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
                    br++;
                }
                else
                {
                    dg.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    br = 0;
                }
            }
            DataGridViewRow row = dg.RowTemplate;
            //row.Height = 35;
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();

                ImageToShow1 = ImageToShow;
                try
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    Array.Clear(imgArray, 0, imgArray.Length);
                    Array.Clear(imgName, 0, imgName.Length);

                    File.Delete(ImageToShow);
                    if (File.Exists(ImageToShow))
                    {
                        MessageBox.Show("Slika nije obrisana !");
                    }
                    else
                    {
                        this.backPanel.Controls.Clear();
                        ImagesInFolder();
                        ShowFolderImages();
                    }

                    Refresh();
                }
                catch (Exception)
                {
                    MessageBox.Show("");
                }
            }
            catch
            {
                return;
            }
        }

        private void SetRemoteFields()
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\postavke.xml";
            XDocument xmlFile = XDocument.Load(path);
            var query = from c in xmlFile.Element("postavke").Elements("email").Elements("gmail") select c;
            foreach (XElement book in query)
            {
                var1 = Convert.ToInt32(book.Attribute("var1").Value);
                var2 = Convert.ToInt32(book.Attribute("var2").Value);
                var3 = Convert.ToInt32(book.Attribute("var3").Value);
                var4 = Convert.ToInt32(book.Attribute("var4").Value);
                var5 = Convert.ToInt32(book.Attribute("var5").Value);
                var6 = Convert.ToInt32(book.Attribute("var6").Value);
            }
        }

        private void galerija_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.FromArgb(var1, var2, var3), Color.FromArgb(var4, var5, var6), 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w + 3, h + 3);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w - 3, h - 3);
        }

        private void backPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (sender is PictureBox)
                {
                    ImageToShow = ((PictureBox)sender).Tag.ToString();
                }
            }
            else
            {
                if (sender is PictureBox)
                {
                    ImageToShow = ((PictureBox)sender).Tag.ToString();
                    Kartoteka.pregled f = new Kartoteka.pregled();
                    f.MainForm_galerija = this;
                    f.ShowDialog();
                }
            }
        }

        private string napomena = "";
        private string datum = "";
        private string id_zapisa = "";

        private void dgv1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int row = dgv1.CurrentCell.RowIndex;

            napomena = dgv1.Rows[row].Cells[1].FormattedValue.ToString();
            datum = dgv1.Rows[row].Cells[0].FormattedValue.ToString();
            id_zapisa = dgv1.Rows[row].Cells[3].FormattedValue.ToString();

            rtbxnapomena.Text = napomena;
            dateTimePicker2.Text = datum;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (rtbxnapomena.Text != "")
            {
                string sql;
                if (uredi) sql = "UPDATE kar_kronologija " +
                    "SET napomena='" + rtbxnapomena.Text.Replace("'", "").Replace("\"", "") + "' " +
                    "WHERE id='" + id_zapisa + "'";
                else sql = "INSERT INTO kar_kronologija " +
                      "(id_partner,datum,napomena,broj_racuna) " +
                      "VALUES('" + partner_id + "','" + dateTimePicker2.Value.ToString("yyyy-MM-dd H:mm:ss") + "','" + rtbxnapomena.Text.Replace("'", "").Replace("\"", "") + "','" + label4.Text + "')";

                classSQL.insert(sql);
                rtbxnapomena.Clear();
                dateTimePicker2.Value.Day.ToString();

                //DataTable popuni_povjest = new DataTable();
                dgv1.Refresh();

                string popun = "SELECT datum , napomena, id, id_partner, broj_racuna FROM kar_kronologija WHERE id_partner = '" + partner_id + "'";
                DataTable DT = classSQL.select(popun, "partner").Tables[0];

                dgv1.Rows.Clear();

                foreach (DataRow row in DT.Rows)
                {
                    dgv1.Rows.Add(row["datum"].ToString(),
                        row["napomena"].ToString(),
                        row["broj_racuna"].ToString(),
                        row["id"].ToString(),
                        row["id_partner"].ToString());
                }

                dgv1.Columns[3].Visible = false;
                dgv1.Columns[4].Visible = false;

                //row.Height = 25;

                PaintRows(dgv1);

                label4.Text = "";
                uredi = false;
                button1.Enabled = true;
            }
            else
            {
                MessageBox.Show("Niste popunili polje napomena !");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tbxfilelocation.Text != "")
            {
                string dirPath = path + "\\";
                string fileName = "a1";
                string[] files = Directory.GetFiles(dirPath);
                int count = files.Count(file => { return file.Contains(fileName); });

                //string newFileName = (count == 0) ? "a1.jpg" : String.Format("{0} ({1})", fileName, count ++);
                int n = 0;
                string fileName1 = "";
                do
                {
                    fileName1 = fileName + "" + n + ".jpg";
                    n++;
                }
                while (System.IO.File.Exists("" + dirPath + fileName1 + "") == true);

                if (System.IO.File.Exists("" + dirPath + fileName + "") != true)
                {
                    string lokacijafilea = tbxfilelocation.Text;
                    File.Copy(@" " + lokacijafilea + " ", @" " + dirPath + fileName1 + " ");
                    pictureBox6.Image = null;

                    if (Directory.Exists(path))
                    {
                        ImagesInFolder(); // Load images
                        ShowFolderImages(); // Show images on PictureBox array
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
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
                            tbxfilelocation.Text = openFileDialog1.FileName;
                            pictureBox6.Image = new Bitmap(openFileDialog1.FileName);
                            pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
                            pictureBox6.BorderStyle = BorderStyle.Fixed3D;
                            //pictureBox6.SetBounds(650, 400, 80, 80);
                            pictureBox6.BringToFront();
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Obrisati stavku iz kronologije ?", "Brisanje", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                classSQL.delete("DELETE FROM kar_kronologija WHERE id = '" + id_zapisa + "' ");
                //DataTable popuni_povjest = new DataTable();
                dgv1.Refresh();

                string popun = "SELECT datum , napomena, id, id_partner, broj_racuna FROM kar_kronologija WHERE id_partner = '" + partner_id + "'";
                DataTable DT = classSQL.select(popun, "partner").Tables[0];

                dgv1.Rows.Clear();

                foreach (DataRow row in DT.Rows)
                {
                    dgv1.Rows.Add(row["datum"].ToString(),
                        row["napomena"].ToString(),
                        row["broj_racuna"].ToString(),
                        row["id"].ToString(),
                        row["id_partner"].ToString());
                }
                dgv1.Columns[3].Visible = false;
                dgv1.Columns[4].Visible = false;
                PaintRows(dgv1);
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int brra;
            int.TryParse(dgv1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString(), out brra);
            if (brra != 0)
            {
                string broj_ra = brra.ToString();
                Report.Faktura.repFaktura pogledajrac = new Report.Faktura.repFaktura();
                pogledajrac.dokumenat = "RAC";
                pogledajrac.broj_dokumenta = broj_ra;
                pogledajrac.ShowDialog();
            }
        }

        private void galerija_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                partner.Close();
            }
            catch
            {
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int row = dgv1.CurrentCell.RowIndex;

            napomena = dgv1.Rows[row].Cells[1].FormattedValue.ToString();
            datum = dgv1.Rows[row].Cells[0].FormattedValue.ToString();
            id_zapisa = dgv1.Rows[row].Cells[3].FormattedValue.ToString();

            rtbxnapomena.Text = napomena;
            dateTimePicker2.Text = datum;

            uredi = true;
            button1.Enabled = false;
        }
    }
}