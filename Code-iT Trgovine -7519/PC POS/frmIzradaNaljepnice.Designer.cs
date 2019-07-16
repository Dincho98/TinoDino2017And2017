namespace PCPOS
{
	partial class frmIzradaNaljepnice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIzradaNaljepnice));
            this.dgv = new System.Windows.Forms.DataGridView();
            this.br = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.broj_kalkulacije = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VPC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jmj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.godina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ean = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dokument = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOdKal = new System.Windows.Forms.TextBox();
            this.btnUcitaj = new System.Windows.Forms.Button();
            this.txtDO = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnIspis = new System.Windows.Forms.Button();
            this.txtPocetak = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.chbVPC = new System.Windows.Forms.CheckBox();
            this.chbMPC = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtkol = new System.Windows.Forms.TextBox();
            this.txtartikl = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtimeartikla = new System.Windows.Forms.TextBox();
            this.cbkalk = new System.Windows.Forms.CheckBox();
            this.cbrucno = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.cbSklKal = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbMS = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMDDo = new System.Windows.Forms.TextBox();
            this.txtMDOd = new System.Windows.Forms.TextBox();
            this.cbPC = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPCDo = new System.Windows.Forms.TextBox();
            this.txtPCOd = new System.Windows.Forms.TextBox();
            this.chbMultiItems = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.br,
            this.broj_kalkulacije,
            this.sifra,
            this.naziv,
            this.kolicina,
            this.mpc,
            this.VPC,
            this.jmj,
            this.godina,
            this.ean,
            this.dokument});
            this.dgv.Location = new System.Drawing.Point(12, 193);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(775, 497);
            this.dgv.TabIndex = 5;
            // 
            // br
            // 
            this.br.HeaderText = "BR";
            this.br.Name = "br";
            this.br.ReadOnly = true;
            this.br.Width = 40;
            // 
            // broj_kalkulacije
            // 
            this.broj_kalkulacije.HeaderText = "Broj kalkulacije";
            this.broj_kalkulacije.Name = "broj_kalkulacije";
            this.broj_kalkulacije.ReadOnly = true;
            this.broj_kalkulacije.Width = 130;
            // 
            // sifra
            // 
            this.sifra.FillWeight = 150F;
            this.sifra.HeaderText = "Šifra";
            this.sifra.Name = "sifra";
            this.sifra.ReadOnly = true;
            this.sifra.Width = 150;
            // 
            // naziv
            // 
            this.naziv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.naziv.HeaderText = "Naziv";
            this.naziv.Name = "naziv";
            this.naziv.ReadOnly = true;
            // 
            // kolicina
            // 
            this.kolicina.HeaderText = "Količina";
            this.kolicina.Name = "kolicina";
            // 
            // mpc
            // 
            this.mpc.HeaderText = "MPC";
            this.mpc.Name = "mpc";
            this.mpc.ReadOnly = true;
            // 
            // VPC
            // 
            this.VPC.HeaderText = "VPC";
            this.VPC.Name = "VPC";
            // 
            // jmj
            // 
            this.jmj.HeaderText = "jmj";
            this.jmj.Name = "jmj";
            this.jmj.Visible = false;
            // 
            // godina
            // 
            this.godina.HeaderText = "godina";
            this.godina.Name = "godina";
            this.godina.Visible = false;
            // 
            // ean
            // 
            this.ean.HeaderText = "Barcode";
            this.ean.Name = "ean";
            this.ean.Visible = false;
            // 
            // dokument
            // 
            this.dokument.HeaderText = "dokument";
            this.dokument.Name = "dokument";
            this.dokument.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(9, 55);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 16);
            this.label4.TabIndex = 106;
            this.label4.Text = "Od broja:";
            // 
            // txtOdKal
            // 
            this.txtOdKal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtOdKal.Location = new System.Drawing.Point(79, 52);
            this.txtOdKal.MaxLength = 13;
            this.txtOdKal.Name = "txtOdKal";
            this.txtOdKal.Size = new System.Drawing.Size(155, 22);
            this.txtOdKal.TabIndex = 0;
            this.txtOdKal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOd_KeyDown);
            // 
            // btnUcitaj
            // 
            this.btnUcitaj.BackColor = System.Drawing.Color.Transparent;
            this.btnUcitaj.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUcitaj.BackgroundImage")));
            this.btnUcitaj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUcitaj.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUcitaj.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnUcitaj.FlatAppearance.BorderSize = 0;
            this.btnUcitaj.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnUcitaj.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUcitaj.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUcitaj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUcitaj.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.btnUcitaj.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnUcitaj.Location = new System.Drawing.Point(676, 112);
            this.btnUcitaj.Name = "btnUcitaj";
            this.btnUcitaj.Size = new System.Drawing.Size(111, 36);
            this.btnUcitaj.TabIndex = 3;
            this.btnUcitaj.TabStop = false;
            this.btnUcitaj.Text = "Učitaj";
            this.btnUcitaj.UseVisualStyleBackColor = false;
            this.btnUcitaj.Click += new System.EventHandler(this.btnUcitaj_Click);
            // 
            // txtDO
            // 
            this.txtDO.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtDO.Location = new System.Drawing.Point(79, 75);
            this.txtDO.MaxLength = 13;
            this.txtDO.Name = "txtDO";
            this.txtDO.Size = new System.Drawing.Size(155, 22);
            this.txtDO.TabIndex = 1;
            this.txtDO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDO_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(9, 78);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 16);
            this.label2.TabIndex = 106;
            this.label2.Text = "Do broja:";
            // 
            // btnIspis
            // 
            this.btnIspis.BackColor = System.Drawing.Color.Transparent;
            this.btnIspis.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnIspis.BackgroundImage")));
            this.btnIspis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIspis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIspis.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnIspis.FlatAppearance.BorderSize = 0;
            this.btnIspis.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnIspis.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnIspis.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnIspis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIspis.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.btnIspis.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnIspis.Location = new System.Drawing.Point(676, 154);
            this.btnIspis.Name = "btnIspis";
            this.btnIspis.Size = new System.Drawing.Size(111, 36);
            this.btnIspis.TabIndex = 4;
            this.btnIspis.TabStop = false;
            this.btnIspis.Text = "Ispiši";
            this.btnIspis.UseVisualStyleBackColor = false;
            this.btnIspis.Click += new System.EventHandler(this.btnIspis_Click);
            // 
            // txtPocetak
            // 
            this.txtPocetak.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtPocetak.Location = new System.Drawing.Point(286, 109);
            this.txtPocetak.MaxLength = 13;
            this.txtPocetak.Name = "txtPocetak";
            this.txtPocetak.Size = new System.Drawing.Size(155, 22);
            this.txtPocetak.TabIndex = 2;
            this.txtPocetak.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPocetak_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(179, 112);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 16);
            this.label1.TabIndex = 106;
            this.label1.Text = "Početna kućica:";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.BackColor = System.Drawing.Color.Transparent;
            this.radioButton1.Location = new System.Drawing.Point(455, 141);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(81, 17);
            this.radioButton1.TabIndex = 107;
            this.radioButton1.Text = "po 3 stupca";
            this.radioButton1.UseVisualStyleBackColor = false;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.BackColor = System.Drawing.Color.Transparent;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(455, 161);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(81, 17);
            this.radioButton2.TabIndex = 108;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "po 4 stupca";
            this.radioButton2.UseVisualStyleBackColor = false;
            // 
            // chbVPC
            // 
            this.chbVPC.AutoSize = true;
            this.chbVPC.BackColor = System.Drawing.Color.Transparent;
            this.chbVPC.Location = new System.Drawing.Point(542, 142);
            this.chbVPC.Name = "chbVPC";
            this.chbVPC.Size = new System.Drawing.Size(47, 17);
            this.chbVPC.TabIndex = 109;
            this.chbVPC.Text = "VPC";
            this.chbVPC.UseVisualStyleBackColor = false;
            // 
            // chbMPC
            // 
            this.chbMPC.AutoSize = true;
            this.chbMPC.BackColor = System.Drawing.Color.Transparent;
            this.chbMPC.Checked = true;
            this.chbMPC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbMPC.Location = new System.Drawing.Point(542, 162);
            this.chbMPC.Name = "chbMPC";
            this.chbMPC.Size = new System.Drawing.Size(49, 17);
            this.chbMPC.TabIndex = 110;
            this.chbMPC.Text = "MPC";
            this.chbMPC.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(452, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 111;
            this.label3.Text = "Ispis:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(32, 169);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 16);
            this.label5.TabIndex = 116;
            this.label5.Text = "Količina:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(32, 145);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 16);
            this.label6.TabIndex = 117;
            this.label6.Text = "Šifra artikla:";
            // 
            // txtkol
            // 
            this.txtkol.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtkol.Location = new System.Drawing.Point(139, 165);
            this.txtkol.MaxLength = 13;
            this.txtkol.Name = "txtkol";
            this.txtkol.Size = new System.Drawing.Size(60, 22);
            this.txtkol.TabIndex = 115;
            this.txtkol.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtkol.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtkol.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtartikl
            // 
            this.txtartikl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtartikl.Location = new System.Drawing.Point(139, 142);
            this.txtartikl.MaxLength = 13;
            this.txtartikl.Name = "txtartikl";
            this.txtartikl.Size = new System.Drawing.Size(60, 22);
            this.txtartikl.TabIndex = 114;
            this.txtartikl.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtartikl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtartikl_KeyDown);
            this.txtartikl.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(205, 141);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 23);
            this.button1.TabIndex = 118;
            this.button1.Text = "...";
            this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtimeartikla
            // 
            this.txtimeartikla.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtimeartikla.Location = new System.Drawing.Point(241, 141);
            this.txtimeartikla.MaxLength = 13;
            this.txtimeartikla.Name = "txtimeartikla";
            this.txtimeartikla.ReadOnly = true;
            this.txtimeartikla.Size = new System.Drawing.Size(198, 22);
            this.txtimeartikla.TabIndex = 119;
            // 
            // cbkalk
            // 
            this.cbkalk.AutoSize = true;
            this.cbkalk.BackColor = System.Drawing.Color.Transparent;
            this.cbkalk.Checked = true;
            this.cbkalk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbkalk.Location = new System.Drawing.Point(12, 35);
            this.cbkalk.Name = "cbkalk";
            this.cbkalk.Size = new System.Drawing.Size(77, 17);
            this.cbkalk.TabIndex = 120;
            this.cbkalk.Text = "Kalkulacija";
            this.cbkalk.UseVisualStyleBackColor = false;
            this.cbkalk.CheckedChanged += new System.EventHandler(this.cbkalk_CheckedChanged);
            // 
            // cbrucno
            // 
            this.cbrucno.AutoSize = true;
            this.cbrucno.BackColor = System.Drawing.Color.Transparent;
            this.cbrucno.Location = new System.Drawing.Point(12, 141);
            this.cbrucno.Name = "cbrucno";
            this.cbrucno.Size = new System.Drawing.Size(15, 14);
            this.cbrucno.TabIndex = 121;
            this.cbrucno.UseVisualStyleBackColor = false;
            this.cbrucno.CheckedChanged += new System.EventHandler(this.cbrucno_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(595, 142);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(74, 17);
            this.checkBox1.TabIndex = 122;
            this.checkBox1.Text = "16 redova";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.BackColor = System.Drawing.Color.Transparent;
            this.checkBox2.Location = new System.Drawing.Point(595, 161);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(74, 17);
            this.checkBox2.TabIndex = 123;
            this.checkBox2.Text = "17 redova";
            this.checkBox2.UseVisualStyleBackColor = false;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // cbSklKal
            // 
            this.cbSklKal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSklKal.FormattingEnabled = true;
            this.cbSklKal.Location = new System.Drawing.Point(87, 8);
            this.cbSklKal.Name = "cbSklKal";
            this.cbSklKal.Size = new System.Drawing.Size(155, 21);
            this.cbSklKal.TabIndex = 124;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(13, 9);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 16);
            this.label7.TabIndex = 125;
            this.label7.Text = "Skladište:";
            // 
            // cbMS
            // 
            this.cbMS.AutoSize = true;
            this.cbMS.BackColor = System.Drawing.Color.Transparent;
            this.cbMS.Checked = true;
            this.cbMS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMS.Location = new System.Drawing.Point(269, 35);
            this.cbMS.Name = "cbMS";
            this.cbMS.Size = new System.Drawing.Size(106, 17);
            this.cbMS.TabIndex = 130;
            this.cbMS.Text = "Međuskladišnica";
            this.cbMS.UseVisualStyleBackColor = false;
            this.cbMS.CheckedChanged += new System.EventHandler(this.cbkalk_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(266, 78);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 16);
            this.label8.TabIndex = 129;
            this.label8.Text = "Do broja:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.Location = new System.Drawing.Point(266, 55);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 16);
            this.label9.TabIndex = 128;
            this.label9.Text = "Od broja:";
            // 
            // txtMDDo
            // 
            this.txtMDDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtMDDo.Location = new System.Drawing.Point(336, 75);
            this.txtMDDo.MaxLength = 13;
            this.txtMDDo.Name = "txtMDDo";
            this.txtMDDo.Size = new System.Drawing.Size(155, 22);
            this.txtMDDo.TabIndex = 127;
            // 
            // txtMDOd
            // 
            this.txtMDOd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtMDOd.Location = new System.Drawing.Point(336, 52);
            this.txtMDOd.MaxLength = 13;
            this.txtMDOd.Name = "txtMDOd";
            this.txtMDOd.Size = new System.Drawing.Size(155, 22);
            this.txtMDOd.TabIndex = 126;
            // 
            // cbPC
            // 
            this.cbPC.AutoSize = true;
            this.cbPC.BackColor = System.Drawing.Color.Transparent;
            this.cbPC.Checked = true;
            this.cbPC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPC.Location = new System.Drawing.Point(530, 35);
            this.cbPC.Name = "cbPC";
            this.cbPC.Size = new System.Drawing.Size(148, 17);
            this.cbPC.TabIndex = 135;
            this.cbPC.Text = "Zapisnik o promjeni cijene";
            this.cbPC.UseVisualStyleBackColor = false;
            this.cbPC.CheckedChanged += new System.EventHandler(this.cbkalk_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label10.Location = new System.Drawing.Point(527, 78);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 16);
            this.label10.TabIndex = 134;
            this.label10.Text = "Do broja:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label11.Location = new System.Drawing.Point(527, 55);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 16);
            this.label11.TabIndex = 133;
            this.label11.Text = "Od broja:";
            // 
            // txtPCDo
            // 
            this.txtPCDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtPCDo.Location = new System.Drawing.Point(597, 75);
            this.txtPCDo.MaxLength = 13;
            this.txtPCDo.Name = "txtPCDo";
            this.txtPCDo.Size = new System.Drawing.Size(155, 22);
            this.txtPCDo.TabIndex = 132;
            // 
            // txtPCOd
            // 
            this.txtPCOd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtPCOd.Location = new System.Drawing.Point(597, 52);
            this.txtPCOd.MaxLength = 13;
            this.txtPCOd.Name = "txtPCOd";
            this.txtPCOd.Size = new System.Drawing.Size(155, 22);
            this.txtPCOd.TabIndex = 131;
            // 
            // chbMultiItems
            // 
            this.chbMultiItems.AutoSize = true;
            this.chbMultiItems.BackColor = System.Drawing.Color.Transparent;
            this.chbMultiItems.Location = new System.Drawing.Point(241, 170);
            this.chbMultiItems.Name = "chbMultiItems";
            this.chbMultiItems.Size = new System.Drawing.Size(77, 17);
            this.chbMultiItems.TabIndex = 136;
            this.chbMultiItems.Text = "Više stavki";
            this.chbMultiItems.UseVisualStyleBackColor = false;
            // 
            // frmIzradaNaljepnice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 702);
            this.Controls.Add(this.chbMultiItems);
            this.Controls.Add(this.cbPC);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtPCDo);
            this.Controls.Add(this.txtPCOd);
            this.Controls.Add(this.cbMS);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtMDDo);
            this.Controls.Add(this.txtMDOd);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbSklKal);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.cbrucno);
            this.Controls.Add(this.cbkalk);
            this.Controls.Add(this.txtimeartikla);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtkol);
            this.Controls.Add(this.txtartikl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chbMPC);
            this.Controls.Add(this.chbVPC);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.btnIspis);
            this.Controls.Add(this.btnUcitaj);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDO);
            this.Controls.Add(this.txtPocetak);
            this.Controls.Add(this.txtOdKal);
            this.Name = "frmIzradaNaljepnice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Izrada naljepnice";
            this.Load += new System.EventHandler(this.frmIzradaNaljepnice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dgv;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtOdKal;
		private System.Windows.Forms.Button btnUcitaj;
		private System.Windows.Forms.TextBox txtDO;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnIspis;
		private System.Windows.Forms.TextBox txtPocetak;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.CheckBox chbVPC;
		private System.Windows.Forms.CheckBox chbMPC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtkol;
        private System.Windows.Forms.TextBox txtartikl;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtimeartikla;
        private System.Windows.Forms.CheckBox cbkalk;
        private System.Windows.Forms.CheckBox cbrucno;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.ComboBox cbSklKal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbMS;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMDDo;
        private System.Windows.Forms.TextBox txtMDOd;
        private System.Windows.Forms.CheckBox cbPC;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPCDo;
        private System.Windows.Forms.TextBox txtPCOd;
        private System.Windows.Forms.DataGridViewTextBoxColumn br;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj_kalkulacije;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolicina;
        private System.Windows.Forms.DataGridViewTextBoxColumn mpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn VPC;
        private System.Windows.Forms.DataGridViewTextBoxColumn jmj;
        private System.Windows.Forms.DataGridViewTextBoxColumn godina;
        private System.Windows.Forms.DataGridViewTextBoxColumn ean;
        private System.Windows.Forms.DataGridViewTextBoxColumn dokument;
        private System.Windows.Forms.CheckBox chbMultiItems;
    }
}