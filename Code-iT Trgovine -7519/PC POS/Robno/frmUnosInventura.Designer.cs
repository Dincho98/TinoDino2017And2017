namespace PCPOS.Robno
{
    partial class frmUnosInventura
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUnosInventura));
            this.txtSifra_robe = new System.Windows.Forms.TextBox();
            this.dgw = new System.Windows.Forms.DataGridView();
            this.br = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jmj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cijena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KolicinaNaSk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_stavka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.porez = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nbc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBrojInventure = new System.Windows.Forms.TextBox();
            this.nmGodinaInventure = new System.Windows.Forms.NumericUpDown();
            this.cbSkladiste = new System.Windows.Forms.ComboBox();
            this.dtpDatum = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rtbNapomena = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIzradio = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnDeleteAllFaktura = new System.Windows.Forms.Button();
            this.btnSveFakture = new System.Windows.Forms.Button();
            this.btnNoviUnos = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnUcitajSveStavke = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmGodinaInventure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSifra_robe
            // 
            this.txtSifra_robe.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtSifra_robe.Location = new System.Drawing.Point(18, 206);
            this.txtSifra_robe.Name = "txtSifra_robe";
            this.txtSifra_robe.Size = new System.Drawing.Size(185, 24);
            this.txtSifra_robe.TabIndex = 11;
            this.txtSifra_robe.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtSifra_robe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifra_robe_KeyDown);
            this.txtSifra_robe.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
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
            this.br,
            this.sifra,
            this.naziv,
            this.jmj,
            this.kolicina,
            this.vpc,
            this.cijena,
            this.iznos,
            this.KolicinaNaSk,
            this.id_stavka,
            this.porez,
            this.nbc});
            this.dgw.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgw.GridColor = System.Drawing.Color.Gainsboro;
            this.dgw.Location = new System.Drawing.Point(18, 239);
            this.dgw.MultiSelect = false;
            this.dgw.Name = "dgw";
            this.dgw.RowHeadersWidth = 30;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dgw.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgw.Size = new System.Drawing.Size(902, 245);
            this.dgw.TabIndex = 49;
            this.dgw.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellClick);
            this.dgw.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellEndEdit);
            this.dgw.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgw_EditingControlShowing);
            // 
            // br
            // 
            this.br.HeaderText = "Broj";
            this.br.Name = "br";
            // 
            // sifra
            // 
            this.sifra.HeaderText = "Šifra";
            this.sifra.Name = "sifra";
            // 
            // naziv
            // 
            this.naziv.HeaderText = "Naziv";
            this.naziv.Name = "naziv";
            // 
            // jmj
            // 
            this.jmj.HeaderText = "JMJ";
            this.jmj.Name = "jmj";
            // 
            // kolicina
            // 
            this.kolicina.HeaderText = "Količina";
            this.kolicina.Name = "kolicina";
            // 
            // vpc
            // 
            this.vpc.HeaderText = "VPC";
            this.vpc.Name = "vpc";
            // 
            // cijena
            // 
            this.cijena.HeaderText = "Cijena";
            this.cijena.Name = "cijena";
            // 
            // iznos
            // 
            this.iznos.HeaderText = "Iznos";
            this.iznos.Name = "iznos";
            // 
            // KolicinaNaSk
            // 
            this.KolicinaNaSk.HeaderText = "KolicinaNaSk";
            this.KolicinaNaSk.Name = "KolicinaNaSk";
            this.KolicinaNaSk.Visible = false;
            // 
            // id_stavka
            // 
            this.id_stavka.HeaderText = "id_stavka";
            this.id_stavka.Name = "id_stavka";
            this.id_stavka.Visible = false;
            // 
            // porez
            // 
            this.porez.HeaderText = "porez";
            this.porez.Name = "porez";
            this.porez.Visible = false;
            // 
            // nbc
            // 
            this.nbc.HeaderText = "nbc";
            this.nbc.Name = "nbc";
            this.nbc.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(15, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 18);
            this.label1.TabIndex = 62;
            this.label1.Text = "Broj inventure:";
            // 
            // txtBrojInventure
            // 
            this.txtBrojInventure.BackColor = System.Drawing.Color.White;
            this.txtBrojInventure.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtBrojInventure.Location = new System.Drawing.Point(135, 77);
            this.txtBrojInventure.Name = "txtBrojInventure";
            this.txtBrojInventure.Size = new System.Drawing.Size(121, 24);
            this.txtBrojInventure.TabIndex = 6;
            this.txtBrojInventure.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtBrojInventure.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBrojInventure_KeyDown);
            this.txtBrojInventure.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // nmGodinaInventure
            // 
            this.nmGodinaInventure.BackColor = System.Drawing.Color.White;
            this.nmGodinaInventure.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.nmGodinaInventure.Location = new System.Drawing.Point(257, 77);
            this.nmGodinaInventure.Name = "nmGodinaInventure";
            this.nmGodinaInventure.Size = new System.Drawing.Size(85, 24);
            this.nmGodinaInventure.TabIndex = 7;
            this.nmGodinaInventure.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.nmGodinaInventure.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBrojInventure_KeyDown);
            this.nmGodinaInventure.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // cbSkladiste
            // 
            this.cbSkladiste.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbSkladiste.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbSkladiste.Enabled = false;
            this.cbSkladiste.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbSkladiste.FormattingEnabled = true;
            this.cbSkladiste.Location = new System.Drawing.Point(135, 104);
            this.cbSkladiste.Name = "cbSkladiste";
            this.cbSkladiste.Size = new System.Drawing.Size(207, 26);
            this.cbSkladiste.TabIndex = 8;
            this.cbSkladiste.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbSkladiste.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbSkladiste_KeyDown);
            this.cbSkladiste.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // dtpDatum
            // 
            this.dtpDatum.CustomFormat = "dd.MM.yyyy H:mm";
            this.dtpDatum.Enabled = false;
            this.dtpDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatum.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatum.Location = new System.Drawing.Point(135, 133);
            this.dtpDatum.Name = "dtpDatum";
            this.dtpDatum.Size = new System.Drawing.Size(207, 24);
            this.dtpDatum.TabIndex = 9;
            this.dtpDatum.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.dtpDatum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDatum_KeyDown);
            this.dtpDatum.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(15, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 18);
            this.label4.TabIndex = 64;
            this.label4.Text = "Datum inventure:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label17.Location = new System.Drawing.Point(15, 107);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(72, 18);
            this.label17.TabIndex = 65;
            this.label17.Text = "Skladište:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(15, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 18);
            this.label2.TabIndex = 64;
            this.label2.Text = "Šifra artikla:";
            // 
            // rtbNapomena
            // 
            this.rtbNapomena.Enabled = false;
            this.rtbNapomena.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rtbNapomena.Location = new System.Drawing.Point(475, 100);
            this.rtbNapomena.Name = "rtbNapomena";
            this.rtbNapomena.Size = new System.Drawing.Size(239, 57);
            this.rtbNapomena.TabIndex = 10;
            this.rtbNapomena.Text = "";
            this.rtbNapomena.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.rtbNapomena.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbNapomena_KeyDown);
            this.rtbNapomena.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(394, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 18);
            this.label3.TabIndex = 65;
            this.label3.Text = "Napomena:";
            // 
            // txtIzradio
            // 
            this.txtIzradio.BackColor = System.Drawing.Color.White;
            this.txtIzradio.Enabled = false;
            this.txtIzradio.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtIzradio.Location = new System.Drawing.Point(475, 72);
            this.txtIzradio.Name = "txtIzradio";
            this.txtIzradio.ReadOnly = true;
            this.txtIzradio.Size = new System.Drawing.Size(239, 24);
            this.txtIzradio.TabIndex = 61;
            this.txtIzradio.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtIzradio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIzradio_KeyDown);
            this.txtIzradio.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(394, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 18);
            this.label5.TabIndex = 65;
            this.label5.Text = "Izradio:";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.Image = global::PCPOS.Properties.Resources.Actions_application_exit_icon;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(790, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 40);
            this.button1.TabIndex = 77;
            this.button1.Text = "Izlaz      ";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnIzlaz_Click);
            // 
            // btnDeleteAllFaktura
            // 
            this.btnDeleteAllFaktura.Enabled = false;
            this.btnDeleteAllFaktura.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnDeleteAllFaktura.Image = global::PCPOS.Properties.Resources.Recyclebin_Empty;
            this.btnDeleteAllFaktura.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteAllFaktura.Location = new System.Drawing.Point(570, 12);
            this.btnDeleteAllFaktura.Name = "btnDeleteAllFaktura";
            this.btnDeleteAllFaktura.Size = new System.Drawing.Size(144, 40);
            this.btnDeleteAllFaktura.TabIndex = 5;
            this.btnDeleteAllFaktura.Text = "Obriši inventuru";
            this.btnDeleteAllFaktura.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteAllFaktura.UseVisualStyleBackColor = true;
            this.btnDeleteAllFaktura.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // btnSveFakture
            // 
            this.btnSveFakture.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSveFakture.Image = global::PCPOS.Properties.Resources._10591;
            this.btnSveFakture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSveFakture.Location = new System.Drawing.Point(432, 12);
            this.btnSveFakture.Name = "btnSveFakture";
            this.btnSveFakture.Size = new System.Drawing.Size(130, 40);
            this.btnSveFakture.TabIndex = 4;
            this.btnSveFakture.Text = "Sve inventure";
            this.btnSveFakture.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSveFakture.UseVisualStyleBackColor = true;
            this.btnSveFakture.Click += new System.EventHandler(this.SveInventure_Click);
            // 
            // btnNoviUnos
            // 
            this.btnNoviUnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnNoviUnos.Image = global::PCPOS.Properties.Resources.folder_open_icon;
            this.btnNoviUnos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNoviUnos.Location = new System.Drawing.Point(18, 12);
            this.btnNoviUnos.Name = "btnNoviUnos";
            this.btnNoviUnos.Size = new System.Drawing.Size(130, 40);
            this.btnNoviUnos.TabIndex = 1;
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
            this.btnOdustani.Location = new System.Drawing.Point(156, 12);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(130, 40);
            this.btnOdustani.TabIndex = 2;
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
            this.btnSpremi.Location = new System.Drawing.Point(294, 12);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(130, 40);
            this.btnSpremi.TabIndex = 3;
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
            this.btnObrisi.Location = new System.Drawing.Point(790, 202);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(130, 30);
            this.btnObrisi.TabIndex = 78;
            this.btnObrisi.Text = "   Obriši stavku";
            this.btnObrisi.UseVisualStyleBackColor = true;
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(204, 202);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(41, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 79;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.btnOpenRoba_Click);
            // 
            // btnUcitajSveStavke
            // 
            this.btnUcitajSveStavke.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUcitajSveStavke.Enabled = false;
            this.btnUcitajSveStavke.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnUcitajSveStavke.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUcitajSveStavke.Location = new System.Drawing.Point(638, 202);
            this.btnUcitajSveStavke.Name = "btnUcitajSveStavke";
            this.btnUcitajSveStavke.Size = new System.Drawing.Size(142, 30);
            this.btnUcitajSveStavke.TabIndex = 80;
            this.btnUcitajSveStavke.Text = "Učitaj sve stavke";
            this.btnUcitajSveStavke.UseVisualStyleBackColor = true;
            this.btnUcitajSveStavke.Click += new System.EventHandler(this.btnUcitajSveStavke_Click);
            // 
            // frmUnosInventura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 496);
            this.Controls.Add(this.btnUcitajSveStavke);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDeleteAllFaktura);
            this.Controls.Add(this.btnSveFakture);
            this.Controls.Add(this.btnNoviUnos);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.rtbNapomena);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIzradio);
            this.Controls.Add(this.txtBrojInventure);
            this.Controls.Add(this.nmGodinaInventure);
            this.Controls.Add(this.cbSkladiste);
            this.Controls.Add(this.dtpDatum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtSifra_robe);
            this.Controls.Add(this.dgw);
            this.Name = "frmUnosInventura";
            this.Text = "Inventura";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmUnosInventura_FormClosing);
            this.Load += new System.EventHandler(this.frmInventura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmGodinaInventure)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSifra_robe;
        private System.Windows.Forms.DataGridView dgw;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtBrojInventure;
        private System.Windows.Forms.NumericUpDown nmGodinaInventure;
        private System.Windows.Forms.ComboBox cbSkladiste;
        private System.Windows.Forms.DateTimePicker dtpDatum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox rtbNapomena;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtIzradio;
		private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnDeleteAllFaktura;
        private System.Windows.Forms.Button btnSveFakture;
        private System.Windows.Forms.Button btnNoviUnos;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.Button btnObrisi;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnUcitajSveStavke;
        private System.Windows.Forms.DataGridViewTextBoxColumn br;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn jmj;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolicina;
        private System.Windows.Forms.DataGridViewTextBoxColumn vpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn cijena;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos;
        private System.Windows.Forms.DataGridViewTextBoxColumn KolicinaNaSk;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_stavka;
        private System.Windows.Forms.DataGridViewTextBoxColumn porez;
        private System.Windows.Forms.DataGridViewTextBoxColumn nbc;
    }
}