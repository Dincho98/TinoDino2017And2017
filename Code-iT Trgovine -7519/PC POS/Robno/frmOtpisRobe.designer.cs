namespace PCPOS.Robno
{
    partial class frmOtpisRobe
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOtpisRobe));
			this.label2 = new System.Windows.Forms.Label();
			this.dgw = new PCPOS.Robno.frmOtpisRobe.MyDataGrid();
			this.rb = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.nbc = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.vpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.pdv = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.rabat = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.mpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ukupno = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.id_stavka = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.txtIzradio = new System.Windows.Forms.TextBox();
			this.rtbNapomena = new System.Windows.Forms.RichTextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtBroj = new System.Windows.Forms.TextBox();
			this.nmGodina = new System.Windows.Forms.NumericUpDown();
			this.cbSkladiste = new System.Windows.Forms.ComboBox();
			this.dtpDatum = new System.Windows.Forms.DateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.txtSifra_robe = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtMjestoTroska = new System.Windows.Forms.TextBox();
			this.lblNaDan = new System.Windows.Forms.Label();
			this.btnOpenRoba = new System.Windows.Forms.PictureBox();
			this.button1 = new System.Windows.Forms.Button();
			this.btnSveFakture = new System.Windows.Forms.Button();
			this.btnNoviUnos = new System.Windows.Forms.Button();
			this.btnOdustani = new System.Windows.Forms.Button();
			this.btnSpremi = new System.Windows.Forms.Button();
			this.btnObrisi = new System.Windows.Forms.Button();
			this.btnDeleteAllFaktura = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nmGodina)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnOpenRoba)).BeginInit();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Arial", 10F);
			this.label2.Location = new System.Drawing.Point(375, 99);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 16);
			this.label2.TabIndex = 179;
			this.label2.Text = "Izradio:";
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
			this.dgw.BackgroundColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgw.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgw.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rb,
            this.sifra,
            this.naziv,
            this.kolicina,
            this.nbc,
            this.vpc,
            this.pdv,
            this.rabat,
            this.mpc,
            this.ukupno,
            this.id_stavka});
			this.dgw.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dgw.GridColor = System.Drawing.Color.Gainsboro;
			this.dgw.Location = new System.Drawing.Point(21, 269);
			this.dgw.MultiSelect = false;
			this.dgw.Name = "dgw";
			this.dgw.RowHeadersWidth = 30;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.dgw.RowsDefaultCellStyle = dataGridViewCellStyle3;
			this.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgw.Size = new System.Drawing.Size(947, 375);
			this.dgw.TabIndex = 12;
			this.dgw.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellClick);
			this.dgw.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellEndEdit);
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
			// vpc
			// 
			this.vpc.HeaderText = "Veleprodajna";
			this.vpc.Name = "vpc";
			// 
			// pdv
			// 
			this.pdv.HeaderText = "PDV";
			this.pdv.Name = "pdv";
			this.pdv.ReadOnly = true;
			// 
			// rabat
			// 
			this.rabat.HeaderText = "Rabat";
			this.rabat.Name = "rabat";
			// 
			// mpc
			// 
			this.mpc.HeaderText = "MPC";
			this.mpc.Name = "mpc";
			this.mpc.ReadOnly = true;
			// 
			// ukupno
			// 
			this.ukupno.HeaderText = "Ukupno";
			this.ukupno.Name = "ukupno";
			this.ukupno.ReadOnly = true;
			// 
			// id_stavka
			// 
			this.id_stavka.HeaderText = "id_stavka";
			this.id_stavka.Name = "id_stavka";
			this.id_stavka.Visible = false;
			// 
			// txtIzradio
			// 
			this.txtIzradio.BackColor = System.Drawing.Color.White;
			this.txtIzradio.Font = new System.Drawing.Font("Arial", 10F);
			this.txtIzradio.Location = new System.Drawing.Point(472, 96);
			this.txtIzradio.Name = "txtIzradio";
			this.txtIzradio.ReadOnly = true;
			this.txtIzradio.Size = new System.Drawing.Size(277, 23);
			this.txtIzradio.TabIndex = 7;
			this.txtIzradio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIzradio_KeyDown);
			// 
			// rtbNapomena
			// 
			this.rtbNapomena.Enabled = false;
			this.rtbNapomena.Font = new System.Drawing.Font("Arial", 10F);
			this.rtbNapomena.Location = new System.Drawing.Point(472, 147);
			this.rtbNapomena.Name = "rtbNapomena";
			this.rtbNapomena.Size = new System.Drawing.Size(277, 69);
			this.rtbNapomena.TabIndex = 10;
			this.rtbNapomena.Text = "";
			this.rtbNapomena.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbNapomena_KeyDown);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Arial", 10F);
			this.label3.Location = new System.Drawing.Point(375, 150);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 16);
			this.label3.TabIndex = 177;
			this.label3.Text = "Napomena:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.label1.ForeColor = System.Drawing.Color.Maroon;
			this.label1.Location = new System.Drawing.Point(17, 100);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(91, 17);
			this.label1.TabIndex = 168;
			this.label1.Text = "Broj unosa:";
			// 
			// txtBroj
			// 
			this.txtBroj.BackColor = System.Drawing.Color.White;
			this.txtBroj.Font = new System.Drawing.Font("Arial", 10F);
			this.txtBroj.Location = new System.Drawing.Point(110, 96);
			this.txtBroj.Name = "txtBroj";
			this.txtBroj.Size = new System.Drawing.Size(121, 23);
			this.txtBroj.TabIndex = 1;
			this.txtBroj.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBroj_KeyDown);
			// 
			// nmGodina
			// 
			this.nmGodina.BackColor = System.Drawing.Color.White;
			this.nmGodina.Font = new System.Drawing.Font("Arial", 10F);
			this.nmGodina.Location = new System.Drawing.Point(232, 96);
			this.nmGodina.Name = "nmGodina";
			this.nmGodina.Size = new System.Drawing.Size(85, 23);
			this.nmGodina.TabIndex = 2;
			this.nmGodina.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBroj_KeyDown);
			// 
			// cbSkladiste
			// 
			this.cbSkladiste.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.cbSkladiste.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cbSkladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSkladiste.Enabled = false;
			this.cbSkladiste.Font = new System.Drawing.Font("Arial", 10F);
			this.cbSkladiste.FormattingEnabled = true;
			this.cbSkladiste.Location = new System.Drawing.Point(110, 121);
			this.cbSkladiste.Name = "cbSkladiste";
			this.cbSkladiste.Size = new System.Drawing.Size(207, 24);
			this.cbSkladiste.TabIndex = 3;
			this.cbSkladiste.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbSkladiste_KeyDown);
			// 
			// dtpDatum
			// 
			this.dtpDatum.Enabled = false;
			this.dtpDatum.Font = new System.Drawing.Font("Arial", 10F);
			this.dtpDatum.Location = new System.Drawing.Point(110, 147);
			this.dtpDatum.Name = "dtpDatum";
			this.dtpDatum.Size = new System.Drawing.Size(207, 23);
			this.dtpDatum.TabIndex = 4;
			this.dtpDatum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDatum_KeyDown);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Arial", 10F);
			this.label4.Location = new System.Drawing.Point(17, 151);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(53, 16);
			this.label4.TabIndex = 170;
			this.label4.Text = "Datum:";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.BackColor = System.Drawing.Color.Transparent;
			this.label17.Font = new System.Drawing.Font("Arial", 10F);
			this.label17.Location = new System.Drawing.Point(17, 125);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(69, 16);
			this.label17.TabIndex = 173;
			this.label17.Text = "Skladište:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.BackColor = System.Drawing.Color.Transparent;
			this.label6.Font = new System.Drawing.Font("Arial", 10F);
			this.label6.Location = new System.Drawing.Point(18, 220);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(83, 16);
			this.label6.TabIndex = 185;
			this.label6.Text = "Šifra artikla:";
			// 
			// txtSifra_robe
			// 
			this.txtSifra_robe.Font = new System.Drawing.Font("Arial", 10F);
			this.txtSifra_robe.Location = new System.Drawing.Point(21, 237);
			this.txtSifra_robe.Name = "txtSifra_robe";
			this.txtSifra_robe.Size = new System.Drawing.Size(237, 23);
			this.txtSifra_robe.TabIndex = 11;
			this.txtSifra_robe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifra_robe_KeyDown);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.BackColor = System.Drawing.Color.Transparent;
			this.label7.Font = new System.Drawing.Font("Arial", 10F);
			this.label7.Location = new System.Drawing.Point(375, 125);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(69, 16);
			this.label7.TabIndex = 188;
			this.label7.Text = "Mj.troška:";
			// 
			// txtMjestoTroska
			// 
			this.txtMjestoTroska.BackColor = System.Drawing.Color.White;
			this.txtMjestoTroska.Font = new System.Drawing.Font("Arial", 10F);
			this.txtMjestoTroska.Location = new System.Drawing.Point(472, 122);
			this.txtMjestoTroska.Name = "txtMjestoTroska";
			this.txtMjestoTroska.Size = new System.Drawing.Size(277, 23);
			this.txtMjestoTroska.TabIndex = 8;
			this.txtMjestoTroska.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMjestoTroska_KeyDown);
			// 
			// lblNaDan
			// 
			this.lblNaDan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblNaDan.AutoSize = true;
			this.lblNaDan.BackColor = System.Drawing.Color.Transparent;
			this.lblNaDan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lblNaDan.Location = new System.Drawing.Point(23, 648);
			this.lblNaDan.Name = "lblNaDan";
			this.lblNaDan.Size = new System.Drawing.Size(0, 13);
			this.lblNaDan.TabIndex = 191;
			// 
			// btnOpenRoba
			// 
			this.btnOpenRoba.BackColor = System.Drawing.Color.Transparent;
			this.btnOpenRoba.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnOpenRoba.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenRoba.Image")));
			this.btnOpenRoba.Location = new System.Drawing.Point(259, 231);
			this.btnOpenRoba.Name = "btnOpenRoba";
			this.btnOpenRoba.Size = new System.Drawing.Size(39, 31);
			this.btnOpenRoba.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.btnOpenRoba.TabIndex = 186;
			this.btnOpenRoba.TabStop = false;
			this.btnOpenRoba.Click += new System.EventHandler(this.btnOpenRoba_Click);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.button1.Image = global::PCPOS.Properties.Resources.Actions_application_exit_icon;
			this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button1.Location = new System.Drawing.Point(838, 14);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(130, 40);
			this.button1.TabIndex = 17;
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
			this.btnSveFakture.Location = new System.Drawing.Point(434, 14);
			this.btnSveFakture.Name = "btnSveFakture";
			this.btnSveFakture.Size = new System.Drawing.Size(130, 40);
			this.btnSveFakture.TabIndex = 16;
			this.btnSveFakture.Text = "Svi otpisi ";
			this.btnSveFakture.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSveFakture.UseVisualStyleBackColor = true;
			this.btnSveFakture.Click += new System.EventHandler(this.btnSveFakture_Click);
			// 
			// btnNoviUnos
			// 
			this.btnNoviUnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.btnNoviUnos.Image = global::PCPOS.Properties.Resources.folder_open_icon;
			this.btnNoviUnos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnNoviUnos.Location = new System.Drawing.Point(20, 14);
			this.btnNoviUnos.Name = "btnNoviUnos";
			this.btnNoviUnos.Size = new System.Drawing.Size(130, 40);
			this.btnNoviUnos.TabIndex = 13;
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
			this.btnOdustani.Location = new System.Drawing.Point(158, 14);
			this.btnOdustani.Name = "btnOdustani";
			this.btnOdustani.Size = new System.Drawing.Size(130, 40);
			this.btnOdustani.TabIndex = 45;
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
			this.btnSpremi.Location = new System.Drawing.Point(296, 14);
			this.btnSpremi.Name = "btnSpremi";
			this.btnSpremi.Size = new System.Drawing.Size(130, 40);
			this.btnSpremi.TabIndex = 15;
			this.btnSpremi.Text = "Spremi   ";
			this.btnSpremi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSpremi.UseVisualStyleBackColor = true;
			this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
			// 
			// btnObrisi
			// 
			this.btnObrisi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnObrisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.btnObrisi.Image = global::PCPOS.Properties.Resources.Close;
			this.btnObrisi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnObrisi.Location = new System.Drawing.Point(838, 233);
			this.btnObrisi.Name = "btnObrisi";
			this.btnObrisi.Size = new System.Drawing.Size(130, 30);
			this.btnObrisi.TabIndex = 192;
			this.btnObrisi.Text = "   Obriši stavku";
			this.btnObrisi.UseVisualStyleBackColor = true;
			this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
			// 
			// btnDeleteAllFaktura
			// 
			this.btnDeleteAllFaktura.Enabled = false;
			this.btnDeleteAllFaktura.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.btnDeleteAllFaktura.Image = global::PCPOS.Properties.Resources.Recyclebin_Empty;
			this.btnDeleteAllFaktura.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnDeleteAllFaktura.Location = new System.Drawing.Point(573, 14);
			this.btnDeleteAllFaktura.Name = "btnDeleteAllFaktura";
			this.btnDeleteAllFaktura.Size = new System.Drawing.Size(130, 40);
			this.btnDeleteAllFaktura.TabIndex = 193;
			this.btnDeleteAllFaktura.Text = "Obriši otpis";
			this.btnDeleteAllFaktura.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnDeleteAllFaktura.UseVisualStyleBackColor = true;
			this.btnDeleteAllFaktura.Click += new System.EventHandler(this.btnDeleteAllFaktura_Click);
			// 
			// frmOtpisRobe
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.LightSlateGray;
			this.ClientSize = new System.Drawing.Size(984, 668);
			this.Controls.Add(this.btnDeleteAllFaktura);
			this.Controls.Add(this.btnObrisi);
			this.Controls.Add(this.lblNaDan);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.txtMjestoTroska);
			this.Controls.Add(this.btnOpenRoba);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.txtSifra_robe);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtIzradio);
			this.Controls.Add(this.rtbNapomena);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.dgw);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.btnSveFakture);
			this.Controls.Add(this.btnNoviUnos);
			this.Controls.Add(this.btnOdustani);
			this.Controls.Add(this.btnSpremi);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtBroj);
			this.Controls.Add(this.nmGodina);
			this.Controls.Add(this.cbSkladiste);
			this.Controls.Add(this.dtpDatum);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label17);
			this.Name = "frmOtpisRobe";
			this.Text = "Otpis robe";
			this.Load += new System.EventHandler(this.frmOtpisRobe_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nmGodina)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnOpenRoba)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtIzradio;
        private System.Windows.Forms.RichTextBox rtbNapomena;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSveFakture;
        private System.Windows.Forms.Button btnNoviUnos;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtBroj;
        private System.Windows.Forms.NumericUpDown nmGodina;
        private System.Windows.Forms.ComboBox cbSkladiste;
        private System.Windows.Forms.DateTimePicker dtpDatum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.PictureBox btnOpenRoba;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSifra_robe;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txtMjestoTroska;
        private System.Windows.Forms.DataGridViewTextBoxColumn rb;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolicina;
        private System.Windows.Forms.DataGridViewTextBoxColumn nbc;
        private System.Windows.Forms.DataGridViewTextBoxColumn vpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn pdv;
        private System.Windows.Forms.DataGridViewTextBoxColumn rabat;
        private System.Windows.Forms.DataGridViewTextBoxColumn mpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ukupno;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_stavka;
        private System.Windows.Forms.Label lblNaDan;
        private System.Windows.Forms.Button btnObrisi;
        private System.Windows.Forms.Button btnDeleteAllFaktura;
        private frmOtpisRobe.MyDataGrid dgw;
    }
}