namespace PCPOS
{
    partial class frmRadniNalogSerivs2016
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRadniNalogSerivs2016));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnUrediStatus = new System.Windows.Forms.Button();
            this.btnSpremiStatus = new System.Windows.Forms.Button();
            this.btnNoviStatus = new System.Windows.Forms.Button();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtNapomena = new System.Windows.Forms.TextBox();
            this.dtpDatum = new System.Windows.Forms.DateTimePicker();
            this.lblNapomena = new System.Windows.Forms.Label();
            this.lblDatum = new System.Windows.Forms.Label();
            this.btnPartner = new System.Windows.Forms.Button();
            this.lblIzradio = new System.Windows.Forms.Label();
            this.lblPartner = new System.Windows.Forms.Label();
            this.txtIzradio = new System.Windows.Forms.TextBox();
            this.txtPartnerNaziv = new System.Windows.Forms.TextBox();
            this.txtSifraPartnera = new System.Windows.Forms.TextBox();
            this.gbHeader = new System.Windows.Forms.GroupBox();
            this.txtArtikl = new System.Windows.Forms.TextBox();
            this.btnSrchArtikl = new System.Windows.Forms.Button();
            this.lblSerijskiBroj = new System.Windows.Forms.Label();
            this.txtSerijskiBroj = new System.Windows.Forms.TextBox();
            this.lblNazivUredaja = new System.Windows.Forms.Label();
            this.lblServisnaPrimka = new System.Windows.Forms.Label();
            this.txtBarkode = new System.Windows.Forms.TextBox();
            this.nmGodina = new System.Windows.Forms.NumericUpDown();
            this.dgw = new PCPOS.frmRadniNalogSerivs2016.MyDataGrid2016();
            this.br = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.napomena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.btnDeleteAllFaktura = new System.Windows.Forms.Button();
            this.btnSveFakture = new System.Windows.Forms.Button();
            this.btnNoviUnos = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.gbHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmGodina)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.btnUrediStatus);
            this.groupBox2.Controls.Add(this.btnSpremiStatus);
            this.groupBox2.Controls.Add(this.btnNoviStatus);
            this.groupBox2.Controls.Add(this.cmbStatus);
            this.groupBox2.Controls.Add(this.lblStatus);
            this.groupBox2.Controls.Add(this.txtNapomena);
            this.groupBox2.Controls.Add(this.dtpDatum);
            this.groupBox2.Controls.Add(this.lblNapomena);
            this.groupBox2.Controls.Add(this.lblDatum);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox2.Location = new System.Drawing.Point(12, 144);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(991, 163);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // btnUrediStatus
            // 
            this.btnUrediStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUrediStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnUrediStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUrediStatus.Location = new System.Drawing.Point(850, 127);
            this.btnUrediStatus.Name = "btnUrediStatus";
            this.btnUrediStatus.Size = new System.Drawing.Size(135, 30);
            this.btnUrediStatus.TabIndex = 8;
            this.btnUrediStatus.Text = "Uredi status";
            this.btnUrediStatus.UseVisualStyleBackColor = true;
            this.btnUrediStatus.Click += new System.EventHandler(this.btnUrediStatus_Click);
            // 
            // btnSpremiStatus
            // 
            this.btnSpremiStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSpremiStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSpremiStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSpremiStatus.Location = new System.Drawing.Point(850, 91);
            this.btnSpremiStatus.Name = "btnSpremiStatus";
            this.btnSpremiStatus.Size = new System.Drawing.Size(135, 30);
            this.btnSpremiStatus.TabIndex = 7;
            this.btnSpremiStatus.Text = "Spremi status";
            this.btnSpremiStatus.UseVisualStyleBackColor = true;
            this.btnSpremiStatus.Click += new System.EventHandler(this.btnSpremiStatus_Click);
            // 
            // btnNoviStatus
            // 
            this.btnNoviStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNoviStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnNoviStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNoviStatus.Location = new System.Drawing.Point(850, 55);
            this.btnNoviStatus.Name = "btnNoviStatus";
            this.btnNoviStatus.Size = new System.Drawing.Size(135, 30);
            this.btnNoviStatus.TabIndex = 6;
            this.btnNoviStatus.Text = "Novi status";
            this.btnNoviStatus.UseVisualStyleBackColor = true;
            this.btnNoviStatus.Click += new System.EventHandler(this.btnNoviStatus_Click);
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "Nulti servis",
            "Zaprimljeno",
            "Servis u toku",
            "Na vanjskom servisu",
            "Završen servis",
            "Povrat kupcu"});
            this.cmbStatus.Location = new System.Drawing.Point(6, 85);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(195, 24);
            this.cmbStatus.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(6, 65);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(101, 17);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Status servisa:";
            // 
            // txtNapomena
            // 
            this.txtNapomena.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNapomena.Location = new System.Drawing.Point(237, 39);
            this.txtNapomena.Multiline = true;
            this.txtNapomena.Name = "txtNapomena";
            this.txtNapomena.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtNapomena.Size = new System.Drawing.Size(570, 116);
            this.txtNapomena.TabIndex = 5;
            this.txtNapomena.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtNapomena.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbNapomena_KeyDown_1);
            this.txtNapomena.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // dtpDatum
            // 
            this.dtpDatum.CustomFormat = "dd.MM.yyyy. HH:mm:ss";
            this.dtpDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatum.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatum.Location = new System.Drawing.Point(6, 39);
            this.dtpDatum.Name = "dtpDatum";
            this.dtpDatum.Size = new System.Drawing.Size(195, 23);
            this.dtpDatum.TabIndex = 1;
            this.dtpDatum.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.dtpDatum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDatum_KeyDown);
            this.dtpDatum.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // lblNapomena
            // 
            this.lblNapomena.AutoSize = true;
            this.lblNapomena.BackColor = System.Drawing.Color.Transparent;
            this.lblNapomena.Location = new System.Drawing.Point(234, 19);
            this.lblNapomena.Name = "lblNapomena";
            this.lblNapomena.Size = new System.Drawing.Size(160, 17);
            this.lblNapomena.TabIndex = 4;
            this.lblNapomena.Text = "Napomena (opis kvara):";
            // 
            // lblDatum
            // 
            this.lblDatum.AutoSize = true;
            this.lblDatum.BackColor = System.Drawing.Color.Transparent;
            this.lblDatum.Location = new System.Drawing.Point(6, 19);
            this.lblDatum.Name = "lblDatum";
            this.lblDatum.Size = new System.Drawing.Size(53, 17);
            this.lblDatum.TabIndex = 0;
            this.lblDatum.Text = "Datum:";
            // 
            // btnPartner
            // 
            this.btnPartner.Location = new System.Drawing.Point(516, 20);
            this.btnPartner.Name = "btnPartner";
            this.btnPartner.Size = new System.Drawing.Size(28, 26);
            this.btnPartner.TabIndex = 5;
            this.btnPartner.Text = "...";
            this.btnPartner.UseVisualStyleBackColor = true;
            this.btnPartner.Click += new System.EventHandler(this.btnPartner_Click);
            // 
            // lblIzradio
            // 
            this.lblIzradio.AutoSize = true;
            this.lblIzradio.BackColor = System.Drawing.Color.Transparent;
            this.lblIzradio.Location = new System.Drawing.Point(718, 25);
            this.lblIzradio.Name = "lblIzradio";
            this.lblIzradio.Size = new System.Drawing.Size(54, 17);
            this.lblIzradio.TabIndex = 7;
            this.lblIzradio.Text = "Izradio:";
            // 
            // lblPartner
            // 
            this.lblPartner.AutoSize = true;
            this.lblPartner.BackColor = System.Drawing.Color.Transparent;
            this.lblPartner.Location = new System.Drawing.Point(387, 25);
            this.lblPartner.Name = "lblPartner";
            this.lblPartner.Size = new System.Drawing.Size(55, 17);
            this.lblPartner.TabIndex = 3;
            this.lblPartner.Text = "Partner";
            // 
            // txtIzradio
            // 
            this.txtIzradio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtIzradio.Location = new System.Drawing.Point(778, 22);
            this.txtIzradio.Name = "txtIzradio";
            this.txtIzradio.ReadOnly = true;
            this.txtIzradio.Size = new System.Drawing.Size(207, 23);
            this.txtIzradio.TabIndex = 8;
            // 
            // txtPartnerNaziv
            // 
            this.txtPartnerNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtPartnerNaziv.Location = new System.Drawing.Point(544, 22);
            this.txtPartnerNaziv.Name = "txtPartnerNaziv";
            this.txtPartnerNaziv.ReadOnly = true;
            this.txtPartnerNaziv.Size = new System.Drawing.Size(127, 23);
            this.txtPartnerNaziv.TabIndex = 6;
            // 
            // txtSifraPartnera
            // 
            this.txtSifraPartnera.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtSifraPartnera.Location = new System.Drawing.Point(463, 21);
            this.txtSifraPartnera.Name = "txtSifraPartnera";
            this.txtSifraPartnera.Size = new System.Drawing.Size(53, 24);
            this.txtSifraPartnera.TabIndex = 4;
            this.txtSifraPartnera.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtSifraPartnera.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraOdrediste_KeyDown);
            this.txtSifraPartnera.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtSifraPartnera.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // gbHeader
            // 
            this.gbHeader.BackColor = System.Drawing.Color.Transparent;
            this.gbHeader.Controls.Add(this.txtArtikl);
            this.gbHeader.Controls.Add(this.btnSrchArtikl);
            this.gbHeader.Controls.Add(this.lblSerijskiBroj);
            this.gbHeader.Controls.Add(this.txtSerijskiBroj);
            this.gbHeader.Controls.Add(this.lblNazivUredaja);
            this.gbHeader.Controls.Add(this.lblServisnaPrimka);
            this.gbHeader.Controls.Add(this.txtBarkode);
            this.gbHeader.Controls.Add(this.btnPartner);
            this.gbHeader.Controls.Add(this.nmGodina);
            this.gbHeader.Controls.Add(this.lblIzradio);
            this.gbHeader.Controls.Add(this.txtSifraPartnera);
            this.gbHeader.Controls.Add(this.txtPartnerNaziv);
            this.gbHeader.Controls.Add(this.lblPartner);
            this.gbHeader.Controls.Add(this.txtIzradio);
            this.gbHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gbHeader.Location = new System.Drawing.Point(12, 57);
            this.gbHeader.Name = "gbHeader";
            this.gbHeader.Size = new System.Drawing.Size(991, 81);
            this.gbHeader.TabIndex = 6;
            this.gbHeader.TabStop = false;
            // 
            // txtArtikl
            // 
            this.txtArtikl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtArtikl.Location = new System.Drawing.Point(136, 46);
            this.txtArtikl.Name = "txtArtikl";
            this.txtArtikl.Size = new System.Drawing.Size(507, 26);
            this.txtArtikl.TabIndex = 10;
            this.txtArtikl.TextChanged += new System.EventHandler(this.txtArtikl_TextChanged);
            // 
            // btnSrchArtikl
            // 
            this.btnSrchArtikl.Location = new System.Drawing.Point(643, 46);
            this.btnSrchArtikl.Name = "btnSrchArtikl";
            this.btnSrchArtikl.Size = new System.Drawing.Size(28, 26);
            this.btnSrchArtikl.TabIndex = 11;
            this.btnSrchArtikl.Text = "...";
            this.btnSrchArtikl.UseVisualStyleBackColor = true;
            this.btnSrchArtikl.Click += new System.EventHandler(this.btnSrchArtikl_Click);
            // 
            // lblSerijskiBroj
            // 
            this.lblSerijskiBroj.AutoSize = true;
            this.lblSerijskiBroj.BackColor = System.Drawing.Color.Transparent;
            this.lblSerijskiBroj.Location = new System.Drawing.Point(694, 51);
            this.lblSerijskiBroj.Name = "lblSerijskiBroj";
            this.lblSerijskiBroj.Size = new System.Drawing.Size(78, 17);
            this.lblSerijskiBroj.TabIndex = 12;
            this.lblSerijskiBroj.Text = "Serijski br.:";
            // 
            // txtSerijskiBroj
            // 
            this.txtSerijskiBroj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtSerijskiBroj.Location = new System.Drawing.Point(778, 48);
            this.txtSerijskiBroj.Name = "txtSerijskiBroj";
            this.txtSerijskiBroj.Size = new System.Drawing.Size(207, 23);
            this.txtSerijskiBroj.TabIndex = 13;
            // 
            // lblNazivUredaja
            // 
            this.lblNazivUredaja.AutoSize = true;
            this.lblNazivUredaja.Location = new System.Drawing.Point(31, 51);
            this.lblNazivUredaja.Name = "lblNazivUredaja";
            this.lblNazivUredaja.Size = new System.Drawing.Size(99, 17);
            this.lblNazivUredaja.TabIndex = 9;
            this.lblNazivUredaja.Text = "Naziv uređaja:";
            // 
            // lblServisnaPrimka
            // 
            this.lblServisnaPrimka.AutoSize = true;
            this.lblServisnaPrimka.BackColor = System.Drawing.Color.Transparent;
            this.lblServisnaPrimka.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblServisnaPrimka.ForeColor = System.Drawing.Color.Maroon;
            this.lblServisnaPrimka.Location = new System.Drawing.Point(1, 25);
            this.lblServisnaPrimka.Name = "lblServisnaPrimka";
            this.lblServisnaPrimka.Size = new System.Drawing.Size(129, 17);
            this.lblServisnaPrimka.TabIndex = 0;
            this.lblServisnaPrimka.Text = "Servisna primka:";
            // 
            // txtBarkode
            // 
            this.txtBarkode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtBarkode.Location = new System.Drawing.Point(136, 20);
            this.txtBarkode.Name = "txtBarkode";
            this.txtBarkode.Size = new System.Drawing.Size(149, 26);
            this.txtBarkode.TabIndex = 1;
            this.txtBarkode.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtBarkode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojPonude_KeyDown);
            this.txtBarkode.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // nmGodina
            // 
            this.nmGodina.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.nmGodina.Location = new System.Drawing.Point(285, 20);
            this.nmGodina.Name = "nmGodina";
            this.nmGodina.Size = new System.Drawing.Size(82, 26);
            this.nmGodina.TabIndex = 2;
            this.nmGodina.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.nmGodina.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojPonude_KeyDown);
            this.nmGodina.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
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
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgw.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgw.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.br,
            this.datum,
            this.status,
            this.napomena});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgw.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgw.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgw.GridColor = System.Drawing.Color.Gainsboro;
            this.dgw.Location = new System.Drawing.Point(12, 313);
            this.dgw.MultiSelect = false;
            this.dgw.Name = "dgw";
            this.dgw.ReadOnly = true;
            this.dgw.RowHeadersWidth = 30;
            this.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgw.Size = new System.Drawing.Size(991, 281);
            this.dgw.TabIndex = 8;
            // 
            // br
            // 
            this.br.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.br.HeaderText = "BR.";
            this.br.Name = "br";
            this.br.ReadOnly = true;
            this.br.Width = 60;
            // 
            // datum
            // 
            this.datum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.datum.HeaderText = "Datum";
            this.datum.Name = "datum";
            this.datum.ReadOnly = true;
            this.datum.Width = 150;
            // 
            // status
            // 
            this.status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.status.HeaderText = "Status";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Width = 150;
            // 
            // napomena
            // 
            this.napomena.HeaderText = "Napomena";
            this.napomena.Name = "napomena";
            this.napomena.ReadOnly = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.Image = global::PCPOS.Properties.Resources.Actions_application_exit_icon;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(873, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 40);
            this.button1.TabIndex = 5;
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
            this.btnDeleteAllFaktura.Location = new System.Drawing.Point(556, 12);
            this.btnDeleteAllFaktura.Name = "btnDeleteAllFaktura";
            this.btnDeleteAllFaktura.Size = new System.Drawing.Size(130, 40);
            this.btnDeleteAllFaktura.TabIndex = 4;
            this.btnDeleteAllFaktura.Text = "Obriši servis";
            this.btnDeleteAllFaktura.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteAllFaktura.UseVisualStyleBackColor = true;
            this.btnDeleteAllFaktura.Click += new System.EventHandler(this.btnDeleteAllFaktura_Click);
            // 
            // btnSveFakture
            // 
            this.btnSveFakture.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSveFakture.Image = global::PCPOS.Properties.Resources._10591;
            this.btnSveFakture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSveFakture.Location = new System.Drawing.Point(420, 12);
            this.btnSveFakture.Name = "btnSveFakture";
            this.btnSveFakture.Size = new System.Drawing.Size(130, 40);
            this.btnSveFakture.TabIndex = 3;
            this.btnSveFakture.Text = "Svi servisi";
            this.btnSveFakture.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSveFakture.UseVisualStyleBackColor = true;
            this.btnSveFakture.Click += new System.EventHandler(this.btnSveFakture_Click);
            // 
            // btnNoviUnos
            // 
            this.btnNoviUnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnNoviUnos.Image = global::PCPOS.Properties.Resources.folder_open_icon;
            this.btnNoviUnos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNoviUnos.Location = new System.Drawing.Point(12, 12);
            this.btnNoviUnos.Name = "btnNoviUnos";
            this.btnNoviUnos.Size = new System.Drawing.Size(130, 40);
            this.btnNoviUnos.TabIndex = 0;
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
            this.btnOdustani.Location = new System.Drawing.Point(148, 12);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(130, 40);
            this.btnOdustani.TabIndex = 1;
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
            this.btnSpremi.Location = new System.Drawing.Point(284, 12);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(130, 40);
            this.btnSpremi.TabIndex = 2;
            this.btnSpremi.Text = "Spremi   ";
            this.btnSpremi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSpremi.UseVisualStyleBackColor = true;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // frmRadniNalogSerivs2016
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1015, 606);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDeleteAllFaktura);
            this.Controls.Add(this.btnSveFakture);
            this.Controls.Add(this.btnNoviUnos);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gbHeader);
            this.Controls.Add(this.dgw);
            this.Name = "frmRadniNalogSerivs2016";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Radni nalog servis";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPonude_FormClosing);
            this.Load += new System.EventHandler(this.frmPonude_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbHeader.ResumeLayout(false);
            this.gbHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmGodina)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnPartner;
        private System.Windows.Forms.Label lblIzradio;
		private System.Windows.Forms.DateTimePicker dtpDatum;
        private System.Windows.Forms.Label lblNapomena;
		private System.Windows.Forms.Label lblDatum;
        private System.Windows.Forms.Label lblPartner;
		private System.Windows.Forms.TextBox txtIzradio;
        private System.Windows.Forms.TextBox txtPartnerNaziv;
        private System.Windows.Forms.TextBox txtSifraPartnera;
		private System.Windows.Forms.GroupBox gbHeader;
		private System.Windows.Forms.Label lblServisnaPrimka;
		public System.Windows.Forms.TextBox txtBarkode;
        private System.Windows.Forms.NumericUpDown nmGodina;
        private MyDataGrid2016 dgw;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button btnDeleteAllFaktura;
		private System.Windows.Forms.Button btnSveFakture;
		private System.Windows.Forms.Button btnNoviUnos;
		private System.Windows.Forms.Button btnOdustani;
		private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.TextBox txtNapomena;
        private System.Windows.Forms.TextBox txtSifraPartneratxtArtikl;
        private System.Windows.Forms.Label lblNazivUredaja;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblSerijskiBroj;
        private System.Windows.Forms.TextBox txtSerijskiBroj;
        private System.Windows.Forms.DataGridViewTextBoxColumn br;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn napomena;
        private System.Windows.Forms.Button btnNoviStatus;
        private System.Windows.Forms.Button btnSrchArtikl;
        private System.Windows.Forms.Button btnUrediStatus;
        private System.Windows.Forms.Button btnSpremiStatus;
        public System.Windows.Forms.TextBox txtArtikl;
	}
}