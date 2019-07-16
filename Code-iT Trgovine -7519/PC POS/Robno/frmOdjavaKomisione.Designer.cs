namespace PCPOS.Robno
{
    partial class frmOdjavaKomisione
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOdjavaKomisione));
			this.dgw = new System.Windows.Forms.DataGridView();
			this.button1 = new System.Windows.Forms.Button();
			this.btnSveFakture = new System.Windows.Forms.Button();
			this.btnNoviUnos = new System.Windows.Forms.Button();
			this.btnOdustani = new System.Windows.Forms.Button();
			this.btnSpremi = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtSifraPartnera = new System.Windows.Forms.TextBox();
			this.txtBroj = new System.Windows.Forms.TextBox();
			this.nmGodina = new System.Windows.Forms.NumericUpDown();
			this.cbSkladiste = new System.Windows.Forms.ComboBox();
			this.dtpDatum = new System.Windows.Forms.DateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.dtpOdDatuma = new System.Windows.Forms.DateTimePicker();
			this.label7 = new System.Windows.Forms.Label();
			this.dtpDoDatuma = new System.Windows.Forms.DateTimePicker();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.txtPartnerIme = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.btnPripremi = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.chbSkladisnica = new System.Windows.Forms.CheckBox();
			this.chbRadniNalozi = new System.Windows.Forms.CheckBox();
			this.chbKasa = new System.Windows.Forms.CheckBox();
			this.chbOtpremnice = new System.Windows.Forms.CheckBox();
			this.chbFakture = new System.Windows.Forms.CheckBox();
			this.rtbNapomena = new System.Windows.Forms.RichTextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtIzradio = new System.Windows.Forms.TextBox();
			this.rb = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dokumenat = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.nbc = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.rabat = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.mpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.vpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.id_stavka = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.table = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nmGodina)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dgw
			// 
			this.dgw.AllowUserToAddRows = false;
			this.dgw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgw.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgw.BackgroundColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgw.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgw.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rb,
            this.sifra,
            this.naziv,
            this.dokumenat,
            this.kolicina,
            this.nbc,
            this.rabat,
            this.mpc,
            this.vpc,
            this.id_stavka,
            this.table,
            this.id});
			this.dgw.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dgw.GridColor = System.Drawing.Color.Gainsboro;
			this.dgw.Location = new System.Drawing.Point(19, 307);
			this.dgw.MultiSelect = false;
			this.dgw.Name = "dgw";
			this.dgw.RowHeadersWidth = 30;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.dgw.RowsDefaultCellStyle = dataGridViewCellStyle2;
			this.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgw.Size = new System.Drawing.Size(947, 349);
			this.dgw.TabIndex = 130;
			this.dgw.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellClick);
			this.dgw.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellEndEdit);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.button1.Image = global::PCPOS.Properties.Resources.Actions_application_exit_icon;
			this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button1.Location = new System.Drawing.Point(836, 16);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(130, 40);
			this.button1.TabIndex = 19;
			this.button1.Text = "Izlaz      ";
			this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// btnSveFakture
			// 
			this.btnSveFakture.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.btnSveFakture.Image = global::PCPOS.Properties.Resources._10591;
			this.btnSveFakture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSveFakture.Location = new System.Drawing.Point(432, 16);
			this.btnSveFakture.Name = "btnSveFakture";
			this.btnSveFakture.Size = new System.Drawing.Size(130, 40);
			this.btnSveFakture.TabIndex = 18;
			this.btnSveFakture.Text = "Sve odjave";
			this.btnSveFakture.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSveFakture.UseVisualStyleBackColor = true;
			this.btnSveFakture.Click += new System.EventHandler(this.btnSveFakture_Click);
			// 
			// btnNoviUnos
			// 
			this.btnNoviUnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.btnNoviUnos.Image = global::PCPOS.Properties.Resources.folder_open_icon;
			this.btnNoviUnos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnNoviUnos.Location = new System.Drawing.Point(18, 16);
			this.btnNoviUnos.Name = "btnNoviUnos";
			this.btnNoviUnos.Size = new System.Drawing.Size(130, 40);
			this.btnNoviUnos.TabIndex = 15;
			this.btnNoviUnos.Text = "Novi unos   ";
			this.btnNoviUnos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnNoviUnos.UseVisualStyleBackColor = true;
			this.btnNoviUnos.Click += new System.EventHandler(this.btnNoviUnos_Click);
			// 
			// btnOdustani
			// 
			this.btnOdustani.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.btnOdustani.Image = global::PCPOS.Properties.Resources.undo;
			this.btnOdustani.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnOdustani.Location = new System.Drawing.Point(156, 16);
			this.btnOdustani.Name = "btnOdustani";
			this.btnOdustani.Size = new System.Drawing.Size(130, 40);
			this.btnOdustani.TabIndex = 16;
			this.btnOdustani.Text = "Odustani   ";
			this.btnOdustani.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnOdustani.UseVisualStyleBackColor = true;
			this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
			// 
			// btnSpremi
			// 
			this.btnSpremi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.btnSpremi.Image = ((System.Drawing.Image)(resources.GetObject("btnSpremi.Image")));
			this.btnSpremi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSpremi.Location = new System.Drawing.Point(294, 16);
			this.btnSpremi.Name = "btnSpremi";
			this.btnSpremi.Size = new System.Drawing.Size(130, 40);
			this.btnSpremi.TabIndex = 17;
			this.btnSpremi.Text = "Spremi   ";
			this.btnSpremi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSpremi.UseVisualStyleBackColor = true;
			this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.label1.ForeColor = System.Drawing.Color.Maroon;
			this.label1.Location = new System.Drawing.Point(15, 93);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(91, 17);
			this.label1.TabIndex = 136;
			this.label1.Text = "Broj unosa:";
			// 
			// txtSifraPartnera
			// 
			this.txtSifraPartnera.BackColor = System.Drawing.Color.White;
			this.txtSifraPartnera.Enabled = false;
			this.txtSifraPartnera.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.txtSifraPartnera.Location = new System.Drawing.Point(441, 146);
			this.txtSifraPartnera.Name = "txtSifraPartnera";
			this.txtSifraPartnera.Size = new System.Drawing.Size(236, 26);
			this.txtSifraPartnera.TabIndex = 6;
			this.txtSifraPartnera.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraPartnera_KeyDown);
			// 
			// txtBroj
			// 
			this.txtBroj.BackColor = System.Drawing.Color.White;
			this.txtBroj.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.txtBroj.Location = new System.Drawing.Point(118, 89);
			this.txtBroj.Name = "txtBroj";
			this.txtBroj.Size = new System.Drawing.Size(121, 26);
			this.txtBroj.TabIndex = 0;
			this.txtBroj.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmOdjavaKomisione_KeyDown);
			// 
			// nmGodina
			// 
			this.nmGodina.BackColor = System.Drawing.Color.White;
			this.nmGodina.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.nmGodina.Location = new System.Drawing.Point(240, 89);
			this.nmGodina.Name = "nmGodina";
			this.nmGodina.Size = new System.Drawing.Size(85, 26);
			this.nmGodina.TabIndex = 1;
			this.nmGodina.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmOdjavaKomisione_KeyDown);
			// 
			// cbSkladiste
			// 
			this.cbSkladiste.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.cbSkladiste.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cbSkladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSkladiste.Enabled = false;
			this.cbSkladiste.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.cbSkladiste.FormattingEnabled = true;
			this.cbSkladiste.Location = new System.Drawing.Point(118, 118);
			this.cbSkladiste.Name = "cbSkladiste";
			this.cbSkladiste.Size = new System.Drawing.Size(207, 28);
			this.cbSkladiste.TabIndex = 2;
			this.cbSkladiste.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbSkladiste_KeyDown);
			// 
			// dtpDatum
			// 
			this.dtpDatum.Enabled = false;
			this.dtpDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.dtpDatum.Location = new System.Drawing.Point(118, 149);
			this.dtpDatum.Name = "dtpDatum";
			this.dtpDatum.Size = new System.Drawing.Size(207, 26);
			this.dtpDatum.TabIndex = 3;
			this.dtpDatum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDatum_KeyDown);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label4.Location = new System.Drawing.Point(15, 150);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(99, 17);
			this.label4.TabIndex = 137;
			this.label4.Text = "Datum odjave:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label5.Location = new System.Drawing.Point(347, 148);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(90, 17);
			this.label5.TabIndex = 139;
			this.label5.Text = "Partner šifra:";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.BackColor = System.Drawing.Color.Transparent;
			this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label17.Location = new System.Drawing.Point(15, 121);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(69, 17);
			this.label17.TabIndex = 141;
			this.label17.Text = "Skladište:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.BackColor = System.Drawing.Color.Transparent;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label6.Location = new System.Drawing.Point(347, 90);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(82, 17);
			this.label6.TabIndex = 137;
			this.label6.Text = "Od datuma:";
			// 
			// dtpOdDatuma
			// 
			this.dtpOdDatuma.Enabled = false;
			this.dtpOdDatuma.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.dtpOdDatuma.Location = new System.Drawing.Point(441, 88);
			this.dtpOdDatuma.Name = "dtpOdDatuma";
			this.dtpOdDatuma.Size = new System.Drawing.Size(277, 26);
			this.dtpOdDatuma.TabIndex = 4;
			this.dtpOdDatuma.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpOdDatuma_KeyDown);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.BackColor = System.Drawing.Color.Transparent;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label7.Location = new System.Drawing.Point(347, 119);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(81, 17);
			this.label7.TabIndex = 137;
			this.label7.Text = "Do datuma:";
			// 
			// dtpDoDatuma
			// 
			this.dtpDoDatuma.Enabled = false;
			this.dtpDoDatuma.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.dtpDoDatuma.Location = new System.Drawing.Point(441, 117);
			this.dtpDoDatuma.Name = "dtpDoDatuma";
			this.dtpDoDatuma.Size = new System.Drawing.Size(277, 26);
			this.dtpDoDatuma.TabIndex = 5;
			this.dtpDoDatuma.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDoDatuma_KeyDown);
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(679, 146);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(38, 26);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 143;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
			// 
			// txtPartnerIme
			// 
			this.txtPartnerIme.BackColor = System.Drawing.Color.White;
			this.txtPartnerIme.Enabled = false;
			this.txtPartnerIme.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.txtPartnerIme.Location = new System.Drawing.Point(441, 175);
			this.txtPartnerIme.Name = "txtPartnerIme";
			this.txtPartnerIme.ReadOnly = true;
			this.txtPartnerIme.Size = new System.Drawing.Size(277, 26);
			this.txtPartnerIme.TabIndex = 7;
			this.txtPartnerIme.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartnerIme_KeyDown);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.BackColor = System.Drawing.Color.Transparent;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label8.Location = new System.Drawing.Point(348, 177);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(85, 17);
			this.label8.TabIndex = 145;
			this.label8.Text = "Partner ime:";
			// 
			// btnPripremi
			// 
			this.btnPripremi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.btnPripremi.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.btnPripremi.Location = new System.Drawing.Point(744, 199);
			this.btnPripremi.Name = "btnPripremi";
			this.btnPripremi.Size = new System.Drawing.Size(204, 84);
			this.btnPripremi.TabIndex = 14;
			this.btnPripremi.Text = "Pripremi podatke";
			this.btnPripremi.UseVisualStyleBackColor = true;
			this.btnPripremi.Click += new System.EventHandler(this.btnPripremi_Click);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Silver;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.chbSkladisnica);
			this.panel1.Controls.Add(this.chbRadniNalozi);
			this.panel1.Controls.Add(this.chbKasa);
			this.panel1.Controls.Add(this.chbOtpremnice);
			this.panel1.Controls.Add(this.chbFakture);
			this.panel1.Location = new System.Drawing.Point(18, 197);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(307, 86);
			this.panel1.TabIndex = 147;
			// 
			// chbSkladisnica
			// 
			this.chbSkladisnica.AutoSize = true;
			this.chbSkladisnica.BackColor = System.Drawing.Color.Transparent;
			this.chbSkladisnica.Checked = true;
			this.chbSkladisnica.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbSkladisnica.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
			this.chbSkladisnica.Location = new System.Drawing.Point(16, 56);
			this.chbSkladisnica.Name = "chbSkladisnica";
			this.chbSkladisnica.Size = new System.Drawing.Size(129, 20);
			this.chbSkladisnica.TabIndex = 13;
			this.chbSkladisnica.Text = "Međuskladišnica";
			this.chbSkladisnica.UseVisualStyleBackColor = false;
			// 
			// chbRadniNalozi
			// 
			this.chbRadniNalozi.AutoSize = true;
			this.chbRadniNalozi.BackColor = System.Drawing.Color.Transparent;
			this.chbRadniNalozi.Checked = true;
			this.chbRadniNalozi.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbRadniNalozi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
			this.chbRadniNalozi.Location = new System.Drawing.Point(159, 31);
			this.chbRadniNalozi.Name = "chbRadniNalozi";
			this.chbRadniNalozi.Size = new System.Drawing.Size(101, 20);
			this.chbRadniNalozi.TabIndex = 13;
			this.chbRadniNalozi.Text = "Radni nalozi";
			this.chbRadniNalozi.UseVisualStyleBackColor = false;
			// 
			// chbKasa
			// 
			this.chbKasa.AutoSize = true;
			this.chbKasa.BackColor = System.Drawing.Color.Transparent;
			this.chbKasa.Checked = true;
			this.chbKasa.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbKasa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
			this.chbKasa.Location = new System.Drawing.Point(16, 31);
			this.chbKasa.Name = "chbKasa";
			this.chbKasa.Size = new System.Drawing.Size(58, 20);
			this.chbKasa.TabIndex = 11;
			this.chbKasa.Text = "Kasa";
			this.chbKasa.UseVisualStyleBackColor = false;
			// 
			// chbOtpremnice
			// 
			this.chbOtpremnice.AutoSize = true;
			this.chbOtpremnice.BackColor = System.Drawing.Color.Transparent;
			this.chbOtpremnice.Checked = true;
			this.chbOtpremnice.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbOtpremnice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
			this.chbOtpremnice.Location = new System.Drawing.Point(159, 7);
			this.chbOtpremnice.Name = "chbOtpremnice";
			this.chbOtpremnice.Size = new System.Drawing.Size(96, 20);
			this.chbOtpremnice.TabIndex = 12;
			this.chbOtpremnice.Text = "Otpremnice";
			this.chbOtpremnice.UseVisualStyleBackColor = false;
			// 
			// chbFakture
			// 
			this.chbFakture.AutoSize = true;
			this.chbFakture.BackColor = System.Drawing.Color.Transparent;
			this.chbFakture.Checked = true;
			this.chbFakture.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbFakture.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
			this.chbFakture.Location = new System.Drawing.Point(16, 7);
			this.chbFakture.Name = "chbFakture";
			this.chbFakture.Size = new System.Drawing.Size(111, 20);
			this.chbFakture.TabIndex = 10;
			this.chbFakture.Text = "Izlazne fakture";
			this.chbFakture.UseVisualStyleBackColor = false;
			// 
			// rtbNapomena
			// 
			this.rtbNapomena.Enabled = false;
			this.rtbNapomena.Location = new System.Drawing.Point(441, 203);
			this.rtbNapomena.Name = "rtbNapomena";
			this.rtbNapomena.Size = new System.Drawing.Size(277, 80);
			this.rtbNapomena.TabIndex = 8;
			this.rtbNapomena.Text = "";
			this.rtbNapomena.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbNapomena_KeyDown);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label3.Location = new System.Drawing.Point(348, 206);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(81, 17);
			this.label3.TabIndex = 149;
			this.label3.Text = "Napomena:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label2.Location = new System.Drawing.Point(741, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 17);
			this.label2.TabIndex = 151;
			this.label2.Text = "Izradio:";
			// 
			// txtIzradio
			// 
			this.txtIzradio.BackColor = System.Drawing.Color.White;
			this.txtIzradio.Enabled = false;
			this.txtIzradio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.txtIzradio.Location = new System.Drawing.Point(744, 84);
			this.txtIzradio.Name = "txtIzradio";
			this.txtIzradio.ReadOnly = true;
			this.txtIzradio.Size = new System.Drawing.Size(204, 26);
			this.txtIzradio.TabIndex = 150;
			// 
			// rb
			// 
			this.rb.FillWeight = 30F;
			this.rb.HeaderText = "RB";
			this.rb.Name = "rb";
			this.rb.ReadOnly = true;
			// 
			// sifra
			// 
			this.sifra.FillWeight = 80F;
			this.sifra.HeaderText = "Šifra robe";
			this.sifra.Name = "sifra";
			this.sifra.ReadOnly = true;
			// 
			// naziv
			// 
			this.naziv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.naziv.FillWeight = 105.5838F;
			this.naziv.HeaderText = "Naziv";
			this.naziv.Name = "naziv";
			this.naziv.ReadOnly = true;
			// 
			// dokumenat
			// 
			this.dokumenat.HeaderText = "Dokument";
			this.dokumenat.Name = "dokumenat";
			this.dokumenat.ReadOnly = true;
			// 
			// kolicina
			// 
			this.kolicina.FillWeight = 70F;
			this.kolicina.HeaderText = "Količina";
			this.kolicina.Name = "kolicina";
			// 
			// nbc
			// 
			this.nbc.FillWeight = 70F;
			this.nbc.HeaderText = "Nabavna";
			this.nbc.Name = "nbc";
			// 
			// rabat
			// 
			this.rabat.HeaderText = "Rabat %";
			this.rabat.Name = "rabat";
			// 
			// mpc
			// 
			this.mpc.HeaderText = "Maloprodajna";
			this.mpc.Name = "mpc";
			// 
			// vpc
			// 
			this.vpc.HeaderText = "Veleprodajna";
			this.vpc.Name = "vpc";
			// 
			// id_stavka
			// 
			this.id_stavka.HeaderText = "id_stavka";
			this.id_stavka.Name = "id_stavka";
			this.id_stavka.Visible = false;
			// 
			// table
			// 
			this.table.HeaderText = "table";
			this.table.Name = "table";
			this.table.Visible = false;
			// 
			// id
			// 
			this.id.HeaderText = "id";
			this.id.Name = "id";
			this.id.Visible = false;
			// 
			// frmOdjavaKomisione
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.SlateGray;
			this.ClientSize = new System.Drawing.Size(984, 668);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtIzradio);
			this.Controls.Add(this.rtbNapomena);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.btnPripremi);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.txtPartnerIme);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.dgw);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.btnSveFakture);
			this.Controls.Add(this.btnNoviUnos);
			this.Controls.Add(this.btnOdustani);
			this.Controls.Add(this.btnSpremi);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtSifraPartnera);
			this.Controls.Add(this.txtBroj);
			this.Controls.Add(this.nmGodina);
			this.Controls.Add(this.cbSkladiste);
			this.Controls.Add(this.dtpDoDatuma);
			this.Controls.Add(this.dtpOdDatuma);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.dtpDatum);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label17);
			this.Name = "frmOdjavaKomisione";
			this.Text = "Odjava komisione robe";
			this.Load += new System.EventHandler(this.frmOdjavaKomisione_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmOdjavaKomisione_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nmGodina)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgw;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSveFakture;
        private System.Windows.Forms.Button btnNoviUnos;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtSifraPartnera;
        public System.Windows.Forms.TextBox txtBroj;
        private System.Windows.Forms.NumericUpDown nmGodina;
        private System.Windows.Forms.ComboBox cbSkladiste;
        private System.Windows.Forms.DateTimePicker dtpDatum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpOdDatuma;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpDoDatuma;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.TextBox txtPartnerIme;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnPripremi;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chbRadniNalozi;
        private System.Windows.Forms.CheckBox chbKasa;
        private System.Windows.Forms.CheckBox chbOtpremnice;
        private System.Windows.Forms.CheckBox chbFakture;
        private System.Windows.Forms.RichTextBox rtbNapomena;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chbSkladisnica;
        private System.Windows.Forms.Label label2;
		public System.Windows.Forms.TextBox txtIzradio;
		private System.Windows.Forms.DataGridViewTextBoxColumn rb;
		private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
		private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
		private System.Windows.Forms.DataGridViewTextBoxColumn dokumenat;
		private System.Windows.Forms.DataGridViewTextBoxColumn kolicina;
		private System.Windows.Forms.DataGridViewTextBoxColumn nbc;
		private System.Windows.Forms.DataGridViewTextBoxColumn rabat;
		private System.Windows.Forms.DataGridViewTextBoxColumn mpc;
		private System.Windows.Forms.DataGridViewTextBoxColumn vpc;
		private System.Windows.Forms.DataGridViewTextBoxColumn id_stavka;
		private System.Windows.Forms.DataGridViewTextBoxColumn table;
		private System.Windows.Forms.DataGridViewTextBoxColumn id;
    }
}