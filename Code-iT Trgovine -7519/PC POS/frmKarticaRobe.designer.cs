namespace PCPOS
{
    partial class frmKarticaRobe
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKarticaRobe));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSkladiste = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnIspis = new System.Windows.Forms.Button();
            this.lblMpc = new System.Windows.Forms.Label();
            this.lblVpc = new System.Windows.Forms.Label();
            this.lblNbc = new System.Windows.Forms.Label();
            this.lblStanje = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnOpenRoba = new System.Windows.Forms.PictureBox();
            this.txtImeArtikla = new System.Windows.Forms.TextBox();
            this.txtSifraArtikla = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgw3 = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chbPocetno = new System.Windows.Forms.CheckBox();
            this.chbIFB = new System.Windows.Forms.CheckBox();
            this.chbOtpisRobe = new System.Windows.Forms.CheckBox();
            this.chbIzdatnica = new System.Windows.Forms.CheckBox();
            this.chbPrimka = new System.Windows.Forms.CheckBox();
            this.chbMediskladisnica = new System.Windows.Forms.CheckBox();
            this.chbInventura = new System.Windows.Forms.CheckBox();
            this.chbPovratDob = new System.Windows.Forms.CheckBox();
            this.chbPromjenaCijene = new System.Windows.Forms.CheckBox();
            this.chbRadniNalog = new System.Windows.Forms.CheckBox();
            this.chbOtpremnica = new System.Windows.Forms.CheckBox();
            this.chbFaktura = new System.Windows.Forms.CheckBox();
            this.chbMaloprodaja = new System.Windows.Forms.CheckBox();
            this.chbKalkulacija = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dtpDoDatuma = new System.Windows.Forms.DateTimePicker();
            this.dtpOdDatuma = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgw = new System.Windows.Forms.DataGridView();
            this.tabKartica = new System.Windows.Forms.TabControl();
            this.brRedaka = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpenRoba)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgw3)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
            this.tabKartica.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(5, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Skladište:";
            // 
            // cbSkladiste
            // 
            this.cbSkladiste.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbSkladiste.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbSkladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkladiste.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbSkladiste.FormattingEnabled = true;
            this.cbSkladiste.Location = new System.Drawing.Point(104, 18);
            this.cbSkladiste.Name = "cbSkladiste";
            this.cbSkladiste.Size = new System.Drawing.Size(182, 24);
            this.cbSkladiste.TabIndex = 1;
            this.cbSkladiste.SelectedIndexChanged += new System.EventHandler(this.cbSkladiste_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btnIspis);
            this.groupBox1.Controls.Add(this.lblMpc);
            this.groupBox1.Controls.Add(this.lblVpc);
            this.groupBox1.Controls.Add(this.lblNbc);
            this.groupBox1.Controls.Add(this.lblStanje);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.btnOpenRoba);
            this.groupBox1.Controls.Add(this.txtImeArtikla);
            this.groupBox1.Controls.Add(this.txtSifraArtikla);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbSkladiste);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(14, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(966, 86);
            this.groupBox1.TabIndex = 51;
            this.groupBox1.TabStop = false;
            // 
            // btnIspis
            // 
            this.btnIspis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIspis.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnIspis.Image = global::PCPOS.Properties.Resources.print_printer;
            this.btnIspis.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIspis.Location = new System.Drawing.Point(858, 29);
            this.btnIspis.Name = "btnIspis";
            this.btnIspis.Size = new System.Drawing.Size(102, 38);
            this.btnIspis.TabIndex = 64;
            this.btnIspis.Text = "Ispis";
            this.btnIspis.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIspis.UseVisualStyleBackColor = true;
            this.btnIspis.Click += new System.EventHandler(this.btnIspis_Click);
            // 
            // lblMpc
            // 
            this.lblMpc.AutoSize = true;
            this.lblMpc.BackColor = System.Drawing.Color.Transparent;
            this.lblMpc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblMpc.ForeColor = System.Drawing.Color.Black;
            this.lblMpc.Location = new System.Drawing.Point(680, 58);
            this.lblMpc.Name = "lblMpc";
            this.lblMpc.Size = new System.Drawing.Size(55, 17);
            this.lblMpc.TabIndex = 63;
            this.lblMpc.Text = "0.00 kn";
            // 
            // lblVpc
            // 
            this.lblVpc.AutoSize = true;
            this.lblVpc.BackColor = System.Drawing.Color.Transparent;
            this.lblVpc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblVpc.ForeColor = System.Drawing.Color.Black;
            this.lblVpc.Location = new System.Drawing.Point(680, 39);
            this.lblVpc.Name = "lblVpc";
            this.lblVpc.Size = new System.Drawing.Size(55, 17);
            this.lblVpc.TabIndex = 63;
            this.lblVpc.Text = "0.00 kn";
            // 
            // lblNbc
            // 
            this.lblNbc.AutoSize = true;
            this.lblNbc.BackColor = System.Drawing.Color.Transparent;
            this.lblNbc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblNbc.ForeColor = System.Drawing.Color.Black;
            this.lblNbc.Location = new System.Drawing.Point(680, 20);
            this.lblNbc.Name = "lblNbc";
            this.lblNbc.Size = new System.Drawing.Size(55, 17);
            this.lblNbc.TabIndex = 63;
            this.lblNbc.Text = "0.00 kn";
            // 
            // lblStanje
            // 
            this.lblStanje.AutoSize = true;
            this.lblStanje.BackColor = System.Drawing.Color.Transparent;
            this.lblStanje.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblStanje.ForeColor = System.Drawing.Color.Black;
            this.lblStanje.Location = new System.Drawing.Point(410, 50);
            this.lblStanje.Name = "lblStanje";
            this.lblStanje.Size = new System.Drawing.Size(51, 17);
            this.lblStanje.TabIndex = 62;
            this.lblStanje.Text = "0 kom";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(320, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 17);
            this.label9.TabIndex = 62;
            this.label9.Text = "Stanje:";
            // 
            // btnOpenRoba
            // 
            this.btnOpenRoba.BackColor = System.Drawing.Color.Transparent;
            this.btnOpenRoba.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenRoba.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenRoba.Image")));
            this.btnOpenRoba.Location = new System.Drawing.Point(260, 43);
            this.btnOpenRoba.Name = "btnOpenRoba";
            this.btnOpenRoba.Size = new System.Drawing.Size(35, 29);
            this.btnOpenRoba.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnOpenRoba.TabIndex = 61;
            this.btnOpenRoba.TabStop = false;
            this.btnOpenRoba.Click += new System.EventHandler(this.btnArtikli_Click);
            // 
            // txtImeArtikla
            // 
            this.txtImeArtikla.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtImeArtikla.Location = new System.Drawing.Point(413, 21);
            this.txtImeArtikla.Name = "txtImeArtikla";
            this.txtImeArtikla.ReadOnly = true;
            this.txtImeArtikla.Size = new System.Drawing.Size(181, 23);
            this.txtImeArtikla.TabIndex = 60;
            // 
            // txtSifraArtikla
            // 
            this.txtSifraArtikla.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSifraArtikla.Location = new System.Drawing.Point(104, 44);
            this.txtSifraArtikla.Name = "txtSifraArtikla";
            this.txtSifraArtikla.Size = new System.Drawing.Size(155, 23);
            this.txtSifraArtikla.TabIndex = 2;
            this.txtSifraArtikla.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraArtikla_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Maroon;
            this.label2.Location = new System.Drawing.Point(6, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 17);
            this.label2.TabIndex = 57;
            this.label2.Text = "Šifra artikla:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(320, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 17);
            this.label3.TabIndex = 53;
            this.label3.Text = "Naziv artikla:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(634, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 17);
            this.label8.TabIndex = 2;
            this.label8.Text = "MPC:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(634, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 17);
            this.label6.TabIndex = 2;
            this.label6.Text = "VPC:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(634, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "NBC:";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.dgw3);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(962, 312);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Trenutno stanje na skladištima";
            // 
            // dgw3
            // 
            this.dgw3.AllowUserToAddRows = false;
            this.dgw3.AllowUserToDeleteRows = false;
            this.dgw3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgw3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgw3.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgw3.BackgroundColor = System.Drawing.Color.White;
            this.dgw3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgw3.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgw3.Location = new System.Drawing.Point(9, 12);
            this.dgw3.MultiSelect = false;
            this.dgw3.Name = "dgw3";
            this.dgw3.RowHeadersWidth = 30;
            this.dgw3.Size = new System.Drawing.Size(943, 294);
            this.dgw3.TabIndex = 43;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.chbPocetno);
            this.tabPage1.Controls.Add(this.chbIFB);
            this.tabPage1.Controls.Add(this.chbOtpisRobe);
            this.tabPage1.Controls.Add(this.chbIzdatnica);
            this.tabPage1.Controls.Add(this.chbPrimka);
            this.tabPage1.Controls.Add(this.chbMediskladisnica);
            this.tabPage1.Controls.Add(this.chbInventura);
            this.tabPage1.Controls.Add(this.chbPovratDob);
            this.tabPage1.Controls.Add(this.chbPromjenaCijene);
            this.tabPage1.Controls.Add(this.chbRadniNalog);
            this.tabPage1.Controls.Add(this.chbOtpremnica);
            this.tabPage1.Controls.Add(this.chbFaktura);
            this.tabPage1.Controls.Add(this.chbMaloprodaja);
            this.tabPage1.Controls.Add(this.chbKalkulacija);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.dtpDoDatuma);
            this.tabPage1.Controls.Add(this.dtpOdDatuma);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.dgw);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(962, 312);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Kartica";
            // 
            // chbPocetno
            // 
            this.chbPocetno.AutoSize = true;
            this.chbPocetno.Checked = true;
            this.chbPocetno.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbPocetno.Location = new System.Drawing.Point(556, 88);
            this.chbPocetno.Name = "chbPocetno";
            this.chbPocetno.Size = new System.Drawing.Size(121, 21);
            this.chbPocetno.TabIndex = 63;
            this.chbPocetno.Text = "Početno stanje";
            this.chbPocetno.UseVisualStyleBackColor = true;
            // 
            // chbIFB
            // 
            this.chbIFB.AutoSize = true;
            this.chbIFB.Checked = true;
            this.chbIFB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbIFB.Location = new System.Drawing.Point(556, 42);
            this.chbIFB.Name = "chbIFB";
            this.chbIFB.Size = new System.Drawing.Size(135, 21);
            this.chbIFB.TabIndex = 62;
            this.chbIFB.Text = "Faktura bez robe";
            this.chbIFB.UseVisualStyleBackColor = true;
            // 
            // chbOtpisRobe
            // 
            this.chbOtpisRobe.AutoSize = true;
            this.chbOtpisRobe.Checked = true;
            this.chbOtpisRobe.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbOtpisRobe.Location = new System.Drawing.Point(447, 42);
            this.chbOtpisRobe.Name = "chbOtpisRobe";
            this.chbOtpisRobe.Size = new System.Drawing.Size(93, 21);
            this.chbOtpisRobe.TabIndex = 61;
            this.chbOtpisRobe.Text = "Otpis robe";
            this.chbOtpisRobe.UseVisualStyleBackColor = true;
            // 
            // chbIzdatnica
            // 
            this.chbIzdatnica.AutoSize = true;
            this.chbIzdatnica.Checked = true;
            this.chbIzdatnica.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbIzdatnica.Location = new System.Drawing.Point(702, 42);
            this.chbIzdatnica.Name = "chbIzdatnica";
            this.chbIzdatnica.Size = new System.Drawing.Size(83, 21);
            this.chbIzdatnica.TabIndex = 60;
            this.chbIzdatnica.Text = "Izdatnica";
            this.chbIzdatnica.UseVisualStyleBackColor = true;
            // 
            // chbPrimka
            // 
            this.chbPrimka.AutoSize = true;
            this.chbPrimka.Checked = true;
            this.chbPrimka.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbPrimka.Location = new System.Drawing.Point(702, 19);
            this.chbPrimka.Name = "chbPrimka";
            this.chbPrimka.Size = new System.Drawing.Size(70, 21);
            this.chbPrimka.TabIndex = 59;
            this.chbPrimka.Text = "Primka";
            this.chbPrimka.UseVisualStyleBackColor = true;
            // 
            // chbMediskladisnica
            // 
            this.chbMediskladisnica.AutoSize = true;
            this.chbMediskladisnica.Checked = true;
            this.chbMediskladisnica.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbMediskladisnica.Location = new System.Drawing.Point(702, 65);
            this.chbMediskladisnica.Name = "chbMediskladisnica";
            this.chbMediskladisnica.Size = new System.Drawing.Size(131, 21);
            this.chbMediskladisnica.TabIndex = 14;
            this.chbMediskladisnica.Text = "Međuskladišnice";
            this.chbMediskladisnica.UseVisualStyleBackColor = true;
            // 
            // chbInventura
            // 
            this.chbInventura.AutoSize = true;
            this.chbInventura.Checked = true;
            this.chbInventura.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbInventura.Location = new System.Drawing.Point(447, 65);
            this.chbInventura.Name = "chbInventura";
            this.chbInventura.Size = new System.Drawing.Size(86, 21);
            this.chbInventura.TabIndex = 11;
            this.chbInventura.Text = "Inventura";
            this.chbInventura.UseVisualStyleBackColor = true;
            // 
            // chbPovratDob
            // 
            this.chbPovratDob.AutoSize = true;
            this.chbPovratDob.Checked = true;
            this.chbPovratDob.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbPovratDob.Location = new System.Drawing.Point(556, 19);
            this.chbPovratDob.Name = "chbPovratDob";
            this.chbPovratDob.Size = new System.Drawing.Size(140, 21);
            this.chbPovratDob.TabIndex = 12;
            this.chbPovratDob.Text = "Povrat dobavljaču";
            this.chbPovratDob.UseVisualStyleBackColor = true;
            // 
            // chbPromjenaCijene
            // 
            this.chbPromjenaCijene.AutoSize = true;
            this.chbPromjenaCijene.Checked = true;
            this.chbPromjenaCijene.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbPromjenaCijene.Location = new System.Drawing.Point(556, 65);
            this.chbPromjenaCijene.Name = "chbPromjenaCijene";
            this.chbPromjenaCijene.Size = new System.Drawing.Size(128, 21);
            this.chbPromjenaCijene.TabIndex = 13;
            this.chbPromjenaCijene.Text = "Promjena cijene";
            this.chbPromjenaCijene.UseVisualStyleBackColor = true;
            // 
            // chbRadniNalog
            // 
            this.chbRadniNalog.AutoSize = true;
            this.chbRadniNalog.Checked = true;
            this.chbRadniNalog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbRadniNalog.Location = new System.Drawing.Point(447, 88);
            this.chbRadniNalog.Name = "chbRadniNalog";
            this.chbRadniNalog.Size = new System.Drawing.Size(103, 21);
            this.chbRadniNalog.TabIndex = 10;
            this.chbRadniNalog.Text = "Radni nalog";
            this.chbRadniNalog.UseVisualStyleBackColor = true;
            // 
            // chbOtpremnica
            // 
            this.chbOtpremnica.AutoSize = true;
            this.chbOtpremnica.Checked = true;
            this.chbOtpremnica.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbOtpremnica.Location = new System.Drawing.Point(447, 19);
            this.chbOtpremnica.Name = "chbOtpremnica";
            this.chbOtpremnica.Size = new System.Drawing.Size(100, 21);
            this.chbOtpremnica.TabIndex = 9;
            this.chbOtpremnica.Text = "Otpremnica";
            this.chbOtpremnica.UseVisualStyleBackColor = true;
            // 
            // chbFaktura
            // 
            this.chbFaktura.AutoSize = true;
            this.chbFaktura.Checked = true;
            this.chbFaktura.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbFaktura.Location = new System.Drawing.Point(337, 65);
            this.chbFaktura.Name = "chbFaktura";
            this.chbFaktura.Size = new System.Drawing.Size(75, 21);
            this.chbFaktura.TabIndex = 8;
            this.chbFaktura.Text = "Faktura";
            this.chbFaktura.UseVisualStyleBackColor = true;
            // 
            // chbMaloprodaja
            // 
            this.chbMaloprodaja.AutoSize = true;
            this.chbMaloprodaja.Checked = true;
            this.chbMaloprodaja.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbMaloprodaja.Location = new System.Drawing.Point(337, 42);
            this.chbMaloprodaja.Name = "chbMaloprodaja";
            this.chbMaloprodaja.Size = new System.Drawing.Size(105, 21);
            this.chbMaloprodaja.TabIndex = 7;
            this.chbMaloprodaja.Text = "Maloprodaja";
            this.chbMaloprodaja.UseVisualStyleBackColor = true;
            // 
            // chbKalkulacija
            // 
            this.chbKalkulacija.AutoSize = true;
            this.chbKalkulacija.Checked = true;
            this.chbKalkulacija.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbKalkulacija.Location = new System.Drawing.Point(337, 19);
            this.chbKalkulacija.Name = "chbKalkulacija";
            this.chbKalkulacija.Size = new System.Drawing.Size(94, 21);
            this.chbKalkulacija.TabIndex = 6;
            this.chbKalkulacija.Text = "Kalkulacija";
            this.chbKalkulacija.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::PCPOS.Properties.Resources._1059;
            this.pictureBox1.Location = new System.Drawing.Point(851, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(71, 65);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 58;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.pic_MouseEnter);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pic_MouseLeave);
            // 
            // dtpDoDatuma
            // 
            this.dtpDoDatuma.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDoDatuma.Location = new System.Drawing.Point(93, 51);
            this.dtpDoDatuma.Name = "dtpDoDatuma";
            this.dtpDoDatuma.Size = new System.Drawing.Size(208, 23);
            this.dtpDoDatuma.TabIndex = 5;
            // 
            // dtpOdDatuma
            // 
            this.dtpOdDatuma.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpOdDatuma.Location = new System.Drawing.Point(93, 26);
            this.dtpOdDatuma.Name = "dtpOdDatuma";
            this.dtpOdDatuma.Size = new System.Drawing.Size(208, 23);
            this.dtpOdDatuma.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(11, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 17);
            this.label7.TabIndex = 54;
            this.label7.Text = "Do datuma:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(11, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 17);
            this.label4.TabIndex = 52;
            this.label4.Text = "Od datuma:";
            // 
            // dgw
            // 
            this.dgw.AllowUserToAddRows = false;
            this.dgw.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgw.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgw.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgw.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgw.BackgroundColor = System.Drawing.Color.White;
            this.dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgw.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgw.Location = new System.Drawing.Point(9, 115);
            this.dgw.MultiSelect = false;
            this.dgw.Name = "dgw";
            this.dgw.ReadOnly = true;
            this.dgw.RowHeadersWidth = 30;
            this.dgw.Size = new System.Drawing.Size(943, 191);
            this.dgw.TabIndex = 42;
            this.dgw.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgw_CellMouseDoubleClick);
            // 
            // tabKartica
            // 
            this.tabKartica.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabKartica.Controls.Add(this.tabPage1);
            this.tabKartica.Controls.Add(this.tabPage2);
            this.tabKartica.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.tabKartica.ItemSize = new System.Drawing.Size(45, 30);
            this.tabKartica.Location = new System.Drawing.Point(14, 114);
            this.tabKartica.Name = "tabKartica";
            this.tabKartica.SelectedIndex = 0;
            this.tabKartica.Size = new System.Drawing.Size(970, 350);
            this.tabKartica.TabIndex = 2;
            this.tabKartica.SelectedIndexChanged += new System.EventHandler(this.tabKartica_SelectedIndexChanged);
            // 
            // brRedaka
            // 
            this.brRedaka.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.brRedaka.AutoSize = true;
            this.brRedaka.BackColor = System.Drawing.Color.Transparent;
            this.brRedaka.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.brRedaka.Location = new System.Drawing.Point(11, 467);
            this.brRedaka.Name = "brRedaka";
            this.brRedaka.Size = new System.Drawing.Size(41, 13);
            this.brRedaka.TabIndex = 64;
            this.brRedaka.Text = "label10";
            // 
            // frmKarticaRobe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 483);
            this.Controls.Add(this.brRedaka);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabKartica);
            this.Name = "frmKarticaRobe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kartica robe";
            this.Load += new System.EventHandler(this.frmKartica_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpenRoba)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgw3)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
            this.tabKartica.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbSkladiste;
		private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtImeArtikla;
        private System.Windows.Forms.TextBox txtSifraArtikla;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox btnOpenRoba;
        private System.Windows.Forms.Label lblMpc;
        private System.Windows.Forms.Label lblVpc;
        private System.Windows.Forms.Label lblNbc;
        private System.Windows.Forms.Label lblStanje;
        private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.DataGridView dgw3;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.CheckBox chbIFB;
		private System.Windows.Forms.CheckBox chbOtpisRobe;
		private System.Windows.Forms.CheckBox chbIzdatnica;
		private System.Windows.Forms.CheckBox chbPrimka;
		private System.Windows.Forms.CheckBox chbMediskladisnica;
		private System.Windows.Forms.CheckBox chbInventura;
		private System.Windows.Forms.CheckBox chbPovratDob;
		private System.Windows.Forms.CheckBox chbPromjenaCijene;
		private System.Windows.Forms.CheckBox chbRadniNalog;
		private System.Windows.Forms.CheckBox chbOtpremnica;
		private System.Windows.Forms.CheckBox chbFaktura;
		private System.Windows.Forms.CheckBox chbMaloprodaja;
		private System.Windows.Forms.CheckBox chbKalkulacija;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.DateTimePicker dtpDoDatuma;
		private System.Windows.Forms.DateTimePicker dtpOdDatuma;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DataGridView dgw;
		private System.Windows.Forms.TabControl tabKartica;
		private System.Windows.Forms.Label brRedaka;
        private System.Windows.Forms.CheckBox chbPocetno;
        private System.Windows.Forms.Button btnIspis;
    }
}