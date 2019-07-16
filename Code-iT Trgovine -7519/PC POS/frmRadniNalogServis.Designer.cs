namespace PCPOS
{
	partial class frmRadniNalogSerivs
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRadniNalogSerivs));
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.txtSifra_robe = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.rtbNapomena = new System.Windows.Forms.TextBox();
			this.btnPartner1 = new System.Windows.Forms.Button();
			this.btnPartner = new System.Windows.Forms.Button();
			this.cbKomercijalist = new System.Windows.Forms.ComboBox();
			this.dtpDanaValuta = new System.Windows.Forms.DateTimePicker();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.dtpDatum = new System.Windows.Forms.DateTimePicker();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtIzradio = new System.Windows.Forms.TextBox();
			this.txtPartnerNaziv = new System.Windows.Forms.TextBox();
			this.txtPartnerNaziv1 = new System.Windows.Forms.TextBox();
			this.txtSifraOdrediste = new System.Windows.Forms.TextBox();
			this.txtSifraFakturirati = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.ttxBrojRN = new System.Windows.Forms.TextBox();
			this.nmGodina = new System.Windows.Forms.NumericUpDown();
			this.dgw = new PCPOS.frmRadniNalogSerivs.MyDataGrid();
			this.br = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.skladiste = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.jmj = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.porez = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.mpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.rabat = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.rabat_iznos = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cijena_bez_pdva = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.iznos_bez_pdva = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.iznos_ukupno = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.vpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.nc = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.id_stavka = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.id_roba_prodaja = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.oduzmi = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.porez_potrosnja = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lblNaDan = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.btnDeleteAllFaktura = new System.Windows.Forms.Button();
			this.btnSveFakture = new System.Windows.Forms.Button();
			this.btnNoviUnos = new System.Windows.Forms.Button();
			this.btnOdustani = new System.Windows.Forms.Button();
			this.btnSpremi = new System.Windows.Forms.Button();
			this.btnOpenRoba = new System.Windows.Forms.PictureBox();
			this.btnObrisi = new System.Windows.Forms.Button();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nmGodina)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnOpenRoba)).BeginInit();
			this.SuspendLayout();
			// 
			// textBox3
			// 
			this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.textBox3.Location = new System.Drawing.Point(293, 577);
			this.textBox3.Name = "textBox3";
			this.textBox3.ReadOnly = true;
			this.textBox3.Size = new System.Drawing.Size(155, 23);
			this.textBox3.TabIndex = 26;
			this.textBox3.Text = "PDV:";
			// 
			// txtSifra_robe
			// 
			this.txtSifra_robe.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
			this.txtSifra_robe.Location = new System.Drawing.Point(11, 346);
			this.txtSifra_robe.Name = "txtSifra_robe";
			this.txtSifra_robe.Size = new System.Drawing.Size(181, 26);
			this.txtSifra_robe.TabIndex = 19;
			this.txtSifra_robe.Enter += new System.EventHandler(this.TRENUTNI_Enter);
			this.txtSifra_robe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifra_robe_KeyDown);
			this.txtSifra_robe.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
			// 
			// textBox2
			// 
			this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.textBox2.Location = new System.Drawing.Point(454, 577);
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(189, 23);
			this.textBox2.TabIndex = 25;
			this.textBox2.Text = "Bez PDV-a:";
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.textBox1.Location = new System.Drawing.Point(649, 577);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(349, 23);
			this.textBox1.TabIndex = 24;
			this.textBox1.Text = "Ukupno sa PDV-om:";
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.BackColor = System.Drawing.Color.Transparent;
			this.groupBox2.Controls.Add(this.rtbNapomena);
			this.groupBox2.Controls.Add(this.btnPartner1);
			this.groupBox2.Controls.Add(this.btnPartner);
			this.groupBox2.Controls.Add(this.cbKomercijalist);
			this.groupBox2.Controls.Add(this.dtpDanaValuta);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.dtpDatum);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label28);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.txtIzradio);
			this.groupBox2.Controls.Add(this.txtPartnerNaziv);
			this.groupBox2.Controls.Add(this.txtPartnerNaziv1);
			this.groupBox2.Controls.Add(this.txtSifraOdrediste);
			this.groupBox2.Controls.Add(this.txtSifraFakturirati);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.groupBox2.Location = new System.Drawing.Point(11, 119);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(987, 202);
			this.groupBox2.TabIndex = 23;
			this.groupBox2.TabStop = false;
			// 
			// rtbNapomena
			// 
			this.rtbNapomena.Location = new System.Drawing.Point(458, 88);
			this.rtbNapomena.Multiline = true;
			this.rtbNapomena.Name = "rtbNapomena";
			this.rtbNapomena.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
			this.rtbNapomena.Size = new System.Drawing.Size(514, 97);
			this.rtbNapomena.TabIndex = 557;
			this.rtbNapomena.Enter += new System.EventHandler(this.TRENUTNI_Enter);
			this.rtbNapomena.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbNapomena_KeyDown_1);
			this.rtbNapomena.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
			// 
			// btnPartner1
			// 
			this.btnPartner1.Location = new System.Drawing.Point(167, 54);
			this.btnPartner1.Name = "btnPartner1";
			this.btnPartner1.Size = new System.Drawing.Size(28, 26);
			this.btnPartner1.TabIndex = 59;
			this.btnPartner1.Text = "...";
			this.btnPartner1.UseVisualStyleBackColor = true;
			this.btnPartner1.Click += new System.EventHandler(this.btnPartner1_Click);
			// 
			// btnPartner
			// 
			this.btnPartner.Location = new System.Drawing.Point(167, 29);
			this.btnPartner.Name = "btnPartner";
			this.btnPartner.Size = new System.Drawing.Size(28, 26);
			this.btnPartner.TabIndex = 60;
			this.btnPartner.Text = "...";
			this.btnPartner.UseVisualStyleBackColor = true;
			this.btnPartner.Click += new System.EventHandler(this.btnPartner_Click);
			// 
			// cbKomercijalist
			// 
			this.cbKomercijalist.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.cbKomercijalist.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cbKomercijalist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbKomercijalist.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.cbKomercijalist.FormattingEnabled = true;
			this.cbKomercijalist.Location = new System.Drawing.Point(458, 31);
			this.cbKomercijalist.Name = "cbKomercijalist";
			this.cbKomercijalist.Size = new System.Drawing.Size(208, 24);
			this.cbKomercijalist.TabIndex = 54;
			this.cbKomercijalist.Enter += new System.EventHandler(this.TRENUTNI_Enter);
			this.cbKomercijalist.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbKomercijalist_KeyDown);
			this.cbKomercijalist.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
			// 
			// dtpDanaValuta
			// 
			this.dtpDanaValuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.dtpDanaValuta.Location = new System.Drawing.Point(114, 110);
			this.dtpDanaValuta.Name = "dtpDanaValuta";
			this.dtpDanaValuta.Size = new System.Drawing.Size(208, 23);
			this.dtpDanaValuta.TabIndex = 45;
			this.dtpDanaValuta.Enter += new System.EventHandler(this.TRENUTNI_Enter);
			this.dtpDanaValuta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDanaValuta_KeyDown);
			this.dtpDanaValuta.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.BackColor = System.Drawing.Color.Transparent;
			this.label13.Location = new System.Drawing.Point(354, 58);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(54, 17);
			this.label13.TabIndex = 38;
			this.label13.Text = "Izradio:";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.BackColor = System.Drawing.Color.Transparent;
			this.label12.Location = new System.Drawing.Point(354, 33);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(99, 17);
			this.label12.TabIndex = 39;
			this.label12.Text = "Komercijalista:";
			// 
			// dtpDatum
			// 
			this.dtpDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.dtpDatum.Location = new System.Drawing.Point(114, 85);
			this.dtpDatum.Name = "dtpDatum";
			this.dtpDatum.Size = new System.Drawing.Size(208, 23);
			this.dtpDatum.TabIndex = 46;
			this.dtpDatum.Enter += new System.EventHandler(this.TRENUTNI_Enter);
			this.dtpDatum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDatum_KeyDown);
			this.dtpDatum.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.BackColor = System.Drawing.Color.Transparent;
			this.label8.Location = new System.Drawing.Point(356, 87);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(81, 17);
			this.label8.TabIndex = 35;
			this.label8.Text = "Napomena:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.BackColor = System.Drawing.Color.Transparent;
			this.label7.Location = new System.Drawing.Point(10, 113);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(71, 17);
			this.label7.TabIndex = 43;
			this.label7.Text = "Vrijedi do:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Location = new System.Drawing.Point(10, 87);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(53, 17);
			this.label4.TabIndex = 21;
			this.label4.Text = "Datum:";
			// 
			// label28
			// 
			this.label28.AutoSize = true;
			this.label28.BackColor = System.Drawing.Color.Transparent;
			this.label28.Location = new System.Drawing.Point(10, 34);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(70, 17);
			this.label28.TabIndex = 31;
			this.label28.Text = "Odredište";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Location = new System.Drawing.Point(10, 59);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(75, 17);
			this.label2.TabIndex = 28;
			this.label2.Text = "Fakturirati:";
			// 
			// txtIzradio
			// 
			this.txtIzradio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.txtIzradio.Location = new System.Drawing.Point(458, 57);
			this.txtIzradio.Name = "txtIzradio";
			this.txtIzradio.ReadOnly = true;
			this.txtIzradio.Size = new System.Drawing.Size(208, 23);
			this.txtIzradio.TabIndex = 11;
			// 
			// txtPartnerNaziv
			// 
			this.txtPartnerNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.txtPartnerNaziv.Location = new System.Drawing.Point(195, 30);
			this.txtPartnerNaziv.Name = "txtPartnerNaziv";
			this.txtPartnerNaziv.ReadOnly = true;
			this.txtPartnerNaziv.Size = new System.Drawing.Size(127, 23);
			this.txtPartnerNaziv.TabIndex = 18;
			// 
			// txtPartnerNaziv1
			// 
			this.txtPartnerNaziv1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.txtPartnerNaziv1.Location = new System.Drawing.Point(195, 55);
			this.txtPartnerNaziv1.Name = "txtPartnerNaziv1";
			this.txtPartnerNaziv1.ReadOnly = true;
			this.txtPartnerNaziv1.Size = new System.Drawing.Size(127, 23);
			this.txtPartnerNaziv1.TabIndex = 19;
			// 
			// txtSifraOdrediste
			// 
			this.txtSifraOdrediste.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.txtSifraOdrediste.Location = new System.Drawing.Point(114, 30);
			this.txtSifraOdrediste.Name = "txtSifraOdrediste";
			this.txtSifraOdrediste.Size = new System.Drawing.Size(53, 24);
			this.txtSifraOdrediste.TabIndex = 20;
			this.txtSifraOdrediste.Enter += new System.EventHandler(this.TRENUTNI_Enter);
			this.txtSifraOdrediste.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraOdrediste_KeyDown);
			this.txtSifraOdrediste.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
			this.txtSifraOdrediste.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
			// 
			// txtSifraFakturirati
			// 
			this.txtSifraFakturirati.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.txtSifraFakturirati.Location = new System.Drawing.Point(114, 55);
			this.txtSifraFakturirati.Name = "txtSifraFakturirati";
			this.txtSifraFakturirati.Size = new System.Drawing.Size(53, 24);
			this.txtSifraFakturirati.TabIndex = 15;
			this.txtSifraFakturirati.Enter += new System.EventHandler(this.TRENUTNI_Enter);
			this.txtSifraFakturirati.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraFakturirati_KeyDown);
			this.txtSifraFakturirati.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
			this.txtSifraFakturirati.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.BackColor = System.Drawing.Color.Transparent;
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.ttxBrojRN);
			this.groupBox1.Controls.Add(this.nmGodina);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.groupBox1.Location = new System.Drawing.Point(11, 68);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(987, 49);
			this.groupBox1.TabIndex = 22;
			this.groupBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.label1.ForeColor = System.Drawing.Color.Maroon;
			this.label1.Location = new System.Drawing.Point(13, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(99, 17);
			this.label1.TabIndex = 2;
			this.label1.Text = "Broj servisa:";
			// 
			// ttxBrojRN
			// 
			this.ttxBrojRN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
			this.ttxBrojRN.Location = new System.Drawing.Point(117, 15);
			this.ttxBrojRN.Name = "ttxBrojRN";
			this.ttxBrojRN.Size = new System.Drawing.Size(77, 26);
			this.ttxBrojRN.TabIndex = 1;
			this.ttxBrojRN.Enter += new System.EventHandler(this.TRENUTNI_Enter);
			this.ttxBrojRN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojPonude_KeyDown);
			this.ttxBrojRN.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
			// 
			// nmGodina
			// 
			this.nmGodina.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
			this.nmGodina.Location = new System.Drawing.Point(195, 15);
			this.nmGodina.Name = "nmGodina";
			this.nmGodina.Size = new System.Drawing.Size(82, 26);
			this.nmGodina.TabIndex = 5;
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
            this.sifra,
            this.naziv,
            this.skladiste,
            this.jmj,
            this.kolicina,
            this.porez,
            this.mpc,
            this.rabat,
            this.rabat_iznos,
            this.cijena_bez_pdva,
            this.iznos_bez_pdva,
            this.iznos_ukupno,
            this.vpc,
            this.nc,
            this.id_stavka,
            this.id_roba_prodaja,
            this.oduzmi,
            this.porez_potrosnja});
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
			this.dgw.Location = new System.Drawing.Point(11, 378);
			this.dgw.MultiSelect = false;
			this.dgw.Name = "dgw";
			this.dgw.RowHeadersWidth = 30;
			this.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgw.Size = new System.Drawing.Size(987, 192);
			this.dgw.TabIndex = 18;
			this.dgw.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellClick);
			this.dgw.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellEndEdit);
			// 
			// br
			// 
			this.br.FillWeight = 50F;
			this.br.HeaderText = "Br.";
			this.br.Name = "br";
			// 
			// sifra
			// 
			this.sifra.FillWeight = 61.10954F;
			this.sifra.HeaderText = "Šifra";
			this.sifra.Name = "sifra";
			this.sifra.ReadOnly = true;
			// 
			// naziv
			// 
			this.naziv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.naziv.FillWeight = 61.10954F;
			this.naziv.HeaderText = "Naziv robe ili usluge";
			this.naziv.MinimumWidth = 130;
			this.naziv.Name = "naziv";
			// 
			// skladiste
			// 
			this.skladiste.DataPropertyName = "sifra";
			this.skladiste.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
			this.skladiste.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.skladiste.HeaderText = "Skladište";
			this.skladiste.Name = "skladiste";
			// 
			// jmj
			// 
			this.jmj.HeaderText = "JMJ";
			this.jmj.Name = "jmj";
			// 
			// kolicina
			// 
			this.kolicina.FillWeight = 61.10954F;
			this.kolicina.HeaderText = "Količina";
			this.kolicina.Name = "kolicina";
			// 
			// porez
			// 
			this.porez.HeaderText = "Porez";
			this.porez.Name = "porez";
			this.porez.ReadOnly = true;
			// 
			// mpc
			// 
			this.mpc.HeaderText = "MPC";
			this.mpc.Name = "mpc";
			// 
			// rabat
			// 
			this.rabat.FillWeight = 61.10954F;
			this.rabat.HeaderText = "Rabat%";
			this.rabat.Name = "rabat";
			// 
			// rabat_iznos
			// 
			this.rabat_iznos.HeaderText = "Rabat iznos";
			this.rabat_iznos.Name = "rabat_iznos";
			this.rabat_iznos.ReadOnly = true;
			// 
			// cijena_bez_pdva
			// 
			this.cijena_bez_pdva.FillWeight = 120F;
			this.cijena_bez_pdva.HeaderText = "Cijena bez pdv-a";
			this.cijena_bez_pdva.Name = "cijena_bez_pdva";
			this.cijena_bez_pdva.ReadOnly = true;
			// 
			// iznos_bez_pdva
			// 
			this.iznos_bez_pdva.FillWeight = 120F;
			this.iznos_bez_pdva.HeaderText = "Iznos bez pdv-a";
			this.iznos_bez_pdva.Name = "iznos_bez_pdva";
			this.iznos_bez_pdva.ReadOnly = true;
			// 
			// iznos_ukupno
			// 
			this.iznos_ukupno.HeaderText = "Iznos ukupno";
			this.iznos_ukupno.Name = "iznos_ukupno";
			this.iznos_ukupno.ReadOnly = true;
			// 
			// vpc
			// 
			this.vpc.HeaderText = "VPC";
			this.vpc.Name = "vpc";
			this.vpc.Visible = false;
			// 
			// nc
			// 
			this.nc.HeaderText = "nc";
			this.nc.Name = "nc";
			this.nc.Visible = false;
			// 
			// id_stavka
			// 
			this.id_stavka.HeaderText = "id_stavka";
			this.id_stavka.Name = "id_stavka";
			this.id_stavka.Visible = false;
			// 
			// id_roba_prodaja
			// 
			this.id_roba_prodaja.HeaderText = "id_roba_prodaja";
			this.id_roba_prodaja.Name = "id_roba_prodaja";
			this.id_roba_prodaja.Visible = false;
			// 
			// oduzmi
			// 
			this.oduzmi.HeaderText = "oduzmi";
			this.oduzmi.Name = "oduzmi";
			this.oduzmi.Visible = false;
			// 
			// porez_potrosnja
			// 
			this.porez_potrosnja.HeaderText = "porez_potrosnja";
			this.porez_potrosnja.Name = "porez_potrosnja";
			this.porez_potrosnja.Visible = false;
			// 
			// lblNaDan
			// 
			this.lblNaDan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblNaDan.AutoSize = true;
			this.lblNaDan.BackColor = System.Drawing.Color.Transparent;
			this.lblNaDan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lblNaDan.Location = new System.Drawing.Point(12, 584);
			this.lblNaDan.Name = "lblNaDan";
			this.lblNaDan.Size = new System.Drawing.Size(0, 13);
			this.lblNaDan.TabIndex = 33;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label5.Location = new System.Drawing.Point(10, 329);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(74, 16);
			this.label5.TabIndex = 38;
			this.label5.Text = "Šifra artikla";
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.button1.Image = global::PCPOS.Properties.Resources.Actions_application_exit_icon;
			this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button1.Location = new System.Drawing.Point(867, 15);
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
			this.btnDeleteAllFaktura.Location = new System.Drawing.Point(567, 15);
			this.btnDeleteAllFaktura.Name = "btnDeleteAllFaktura";
			this.btnDeleteAllFaktura.Size = new System.Drawing.Size(130, 40);
			this.btnDeleteAllFaktura.TabIndex = 76;
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
			this.btnSveFakture.Location = new System.Drawing.Point(429, 15);
			this.btnSveFakture.Name = "btnSveFakture";
			this.btnSveFakture.Size = new System.Drawing.Size(130, 40);
			this.btnSveFakture.TabIndex = 75;
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
			this.btnNoviUnos.Location = new System.Drawing.Point(15, 15);
			this.btnNoviUnos.Name = "btnNoviUnos";
			this.btnNoviUnos.Size = new System.Drawing.Size(130, 40);
			this.btnNoviUnos.TabIndex = 74;
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
			this.btnOdustani.Location = new System.Drawing.Point(153, 15);
			this.btnOdustani.Name = "btnOdustani";
			this.btnOdustani.Size = new System.Drawing.Size(130, 40);
			this.btnOdustani.TabIndex = 73;
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
			this.btnSpremi.Location = new System.Drawing.Point(291, 15);
			this.btnSpremi.Name = "btnSpremi";
			this.btnSpremi.Size = new System.Drawing.Size(130, 40);
			this.btnSpremi.TabIndex = 72;
			this.btnSpremi.Text = "Spremi   ";
			this.btnSpremi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSpremi.UseVisualStyleBackColor = true;
			this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
			// 
			// btnOpenRoba
			// 
			this.btnOpenRoba.BackColor = System.Drawing.Color.Transparent;
			this.btnOpenRoba.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnOpenRoba.Image = global::PCPOS.Properties.Resources._1059;
			this.btnOpenRoba.Location = new System.Drawing.Point(196, 342);
			this.btnOpenRoba.Name = "btnOpenRoba";
			this.btnOpenRoba.Size = new System.Drawing.Size(39, 31);
			this.btnOpenRoba.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.btnOpenRoba.TabIndex = 34;
			this.btnOpenRoba.TabStop = false;
			this.btnOpenRoba.Click += new System.EventHandler(this.btnOpenRoba_Click);
			// 
			// btnObrisi
			// 
			this.btnObrisi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnObrisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.btnObrisi.Image = global::PCPOS.Properties.Resources.Close;
			this.btnObrisi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnObrisi.Location = new System.Drawing.Point(862, 346);
			this.btnObrisi.Name = "btnObrisi";
			this.btnObrisi.Size = new System.Drawing.Size(135, 30);
			this.btnObrisi.TabIndex = 20;
			this.btnObrisi.Text = "   Obriši stavku";
			this.btnObrisi.UseVisualStyleBackColor = true;
			this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
			// 
			// frmRadniNalogSerivs
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
			this.Controls.Add(this.btnOpenRoba);
			this.Controls.Add(this.lblNaDan);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.txtSifra_robe);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnObrisi);
			this.Controls.Add(this.dgw);
			this.Name = "frmRadniNalogSerivs";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Radni nalog servis";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPonude_FormClosing);
			this.Load += new System.EventHandler(this.frmPonude_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nmGodina)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnOpenRoba)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.TextBox txtSifra_robe;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnPartner1;
		private System.Windows.Forms.Button btnPartner;
		private System.Windows.Forms.ComboBox cbKomercijalist;
		private System.Windows.Forms.DateTimePicker dtpDanaValuta;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.DateTimePicker dtpDatum;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtIzradio;
		private System.Windows.Forms.TextBox txtPartnerNaziv;
		private System.Windows.Forms.TextBox txtPartnerNaziv1;
		private System.Windows.Forms.TextBox txtSifraOdrediste;
		private System.Windows.Forms.TextBox txtSifraFakturirati;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		public System.Windows.Forms.TextBox ttxBrojRN;
		private System.Windows.Forms.NumericUpDown nmGodina;
		private System.Windows.Forms.Button btnObrisi;
		private MyDataGrid dgw;
		private System.Windows.Forms.Label lblNaDan;
		private System.Windows.Forms.PictureBox btnOpenRoba;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button btnDeleteAllFaktura;
		private System.Windows.Forms.Button btnSveFakture;
		private System.Windows.Forms.Button btnNoviUnos;
		private System.Windows.Forms.Button btnOdustani;
		private System.Windows.Forms.Button btnSpremi;
		private System.Windows.Forms.TextBox rtbNapomena;
		private System.Windows.Forms.DataGridViewTextBoxColumn br;
		private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
		private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
		private System.Windows.Forms.DataGridViewComboBoxColumn skladiste;
		private System.Windows.Forms.DataGridViewTextBoxColumn jmj;
		private System.Windows.Forms.DataGridViewTextBoxColumn kolicina;
		private System.Windows.Forms.DataGridViewTextBoxColumn porez;
		private System.Windows.Forms.DataGridViewTextBoxColumn mpc;
		private System.Windows.Forms.DataGridViewTextBoxColumn rabat;
		private System.Windows.Forms.DataGridViewTextBoxColumn rabat_iznos;
		private System.Windows.Forms.DataGridViewTextBoxColumn cijena_bez_pdva;
		private System.Windows.Forms.DataGridViewTextBoxColumn iznos_bez_pdva;
		private System.Windows.Forms.DataGridViewTextBoxColumn iznos_ukupno;
		private System.Windows.Forms.DataGridViewTextBoxColumn vpc;
		private System.Windows.Forms.DataGridViewTextBoxColumn nc;
		private System.Windows.Forms.DataGridViewTextBoxColumn id_stavka;
		private System.Windows.Forms.DataGridViewTextBoxColumn id_roba_prodaja;
		private System.Windows.Forms.DataGridViewTextBoxColumn oduzmi;
		private System.Windows.Forms.DataGridViewTextBoxColumn porez_potrosnja;
	}
}