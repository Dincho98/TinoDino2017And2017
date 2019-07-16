namespace PCPOS
{
    partial class frmPopisKalkulacija
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.chbDO = new System.Windows.Forms.CheckBox();
            this.chbArtikl = new System.Windows.Forms.CheckBox();
            this.chbSifra = new System.Windows.Forms.CheckBox();
            this.chbSkladiste = new System.Windows.Forms.CheckBox();
            this.chbValuta = new System.Windows.Forms.CheckBox();
            this.chbOD = new System.Windows.Forms.CheckBox();
            this.chbIzradio = new System.Windows.Forms.CheckBox();
            this.chbVD = new System.Windows.Forms.CheckBox();
            this.chbBroj = new System.Windows.Forms.CheckBox();
            this.cbArtikl = new System.Windows.Forms.TextBox();
            this.txtPartner = new System.Windows.Forms.TextBox();
            this.cbIzradio = new System.Windows.Forms.ComboBox();
            this.cbSkladiste = new System.Windows.Forms.ComboBox();
            this.cbValuta = new System.Windows.Forms.ComboBox();
            this.cbVD = new System.Windows.Forms.ComboBox();
            this.dtpDO = new System.Windows.Forms.DateTimePicker();
            this.txtBroj = new System.Windows.Forms.TextBox();
            this.dtpOD = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ch = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbIzradio = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.btnUrediSifru = new System.Windows.Forms.Button();
            this.btnSveFakture = new System.Windows.Forms.Button();
            this.broj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orginalni_dokumenat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dobavljac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skladiste = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ukupno_vpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ukupno_mpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fakturni_iznos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.placeno = new System.Windows.Forms.DataGridViewButtonColumn();
            this.id_skladiste = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ID = new System.Windows.Forms.DataGridViewButtonColumn();
            this.id_partner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.broj,
            this.orginalni_dokumenat,
            this.dobavljac,
            this.skladiste,
            this.datum,
            this.ukupno_vpc,
            this.ukupno_mpc,
            this.fakturni_iznos,
            this.placeno,
            this.id_skladiste,
            this.ID,
            this.id_partner});
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.GridColor = System.Drawing.Color.Gainsboro;
            this.dataGridView1.Location = new System.Drawing.Point(12, 205);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(980, 382);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.chbDO);
            this.groupBox1.Controls.Add(this.chbArtikl);
            this.groupBox1.Controls.Add(this.chbSifra);
            this.groupBox1.Controls.Add(this.chbSkladiste);
            this.groupBox1.Controls.Add(this.chbValuta);
            this.groupBox1.Controls.Add(this.chbOD);
            this.groupBox1.Controls.Add(this.chbIzradio);
            this.groupBox1.Controls.Add(this.chbVD);
            this.groupBox1.Controls.Add(this.chbBroj);
            this.groupBox1.Controls.Add(this.cbArtikl);
            this.groupBox1.Controls.Add(this.txtPartner);
            this.groupBox1.Controls.Add(this.cbIzradio);
            this.groupBox1.Controls.Add(this.cbSkladiste);
            this.groupBox1.Controls.Add(this.cbValuta);
            this.groupBox1.Controls.Add(this.cbVD);
            this.groupBox1.Controls.Add(this.dtpDO);
            this.groupBox1.Controls.Add(this.txtBroj);
            this.groupBox1.Controls.Add(this.dtpOD);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.ch);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lbIzradio);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox1.Location = new System.Drawing.Point(12, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(980, 117);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pretraživanje kalkulacija";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = global::PCPOS.Properties.Resources._1059;
            this.pictureBox2.Location = new System.Drawing.Point(290, 52);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(22, 21);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 73;
            this.pictureBox2.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox2, "Traži");
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // chbDO
            // 
            this.chbDO.AutoSize = true;
            this.chbDO.Location = new System.Drawing.Point(584, 53);
            this.chbDO.Name = "chbDO";
            this.chbDO.Size = new System.Drawing.Size(15, 14);
            this.chbDO.TabIndex = 68;
            this.chbDO.UseVisualStyleBackColor = true;
            // 
            // chbArtikl
            // 
            this.chbArtikl.AutoSize = true;
            this.chbArtikl.Location = new System.Drawing.Point(882, 23);
            this.chbArtikl.Name = "chbArtikl";
            this.chbArtikl.Size = new System.Drawing.Size(15, 14);
            this.chbArtikl.TabIndex = 66;
            this.chbArtikl.UseVisualStyleBackColor = true;
            // 
            // chbSifra
            // 
            this.chbSifra.AutoSize = true;
            this.chbSifra.Location = new System.Drawing.Point(290, 97);
            this.chbSifra.Name = "chbSifra";
            this.chbSifra.Size = new System.Drawing.Size(15, 14);
            this.chbSifra.TabIndex = 67;
            this.chbSifra.UseVisualStyleBackColor = true;
            this.chbSifra.Visible = false;
            // 
            // chbSkladiste
            // 
            this.chbSkladiste.AutoSize = true;
            this.chbSkladiste.Location = new System.Drawing.Point(882, 80);
            this.chbSkladiste.Name = "chbSkladiste";
            this.chbSkladiste.Size = new System.Drawing.Size(15, 14);
            this.chbSkladiste.TabIndex = 65;
            this.chbSkladiste.UseVisualStyleBackColor = true;
            // 
            // chbValuta
            // 
            this.chbValuta.AutoSize = true;
            this.chbValuta.Location = new System.Drawing.Point(584, 80);
            this.chbValuta.Name = "chbValuta";
            this.chbValuta.Size = new System.Drawing.Size(15, 14);
            this.chbValuta.TabIndex = 65;
            this.chbValuta.UseVisualStyleBackColor = true;
            // 
            // chbOD
            // 
            this.chbOD.AutoSize = true;
            this.chbOD.Location = new System.Drawing.Point(584, 26);
            this.chbOD.Name = "chbOD";
            this.chbOD.Size = new System.Drawing.Size(15, 14);
            this.chbOD.TabIndex = 72;
            this.chbOD.UseVisualStyleBackColor = true;
            // 
            // chbIzradio
            // 
            this.chbIzradio.AutoSize = true;
            this.chbIzradio.Location = new System.Drawing.Point(882, 51);
            this.chbIzradio.Name = "chbIzradio";
            this.chbIzradio.Size = new System.Drawing.Size(15, 14);
            this.chbIzradio.TabIndex = 70;
            this.chbIzradio.UseVisualStyleBackColor = true;
            // 
            // chbVD
            // 
            this.chbVD.AutoSize = true;
            this.chbVD.Location = new System.Drawing.Point(290, 82);
            this.chbVD.Name = "chbVD";
            this.chbVD.Size = new System.Drawing.Size(15, 14);
            this.chbVD.TabIndex = 71;
            this.chbVD.UseVisualStyleBackColor = true;
            // 
            // chbBroj
            // 
            this.chbBroj.AutoSize = true;
            this.chbBroj.Location = new System.Drawing.Point(290, 30);
            this.chbBroj.Name = "chbBroj";
            this.chbBroj.Size = new System.Drawing.Size(15, 14);
            this.chbBroj.TabIndex = 69;
            this.chbBroj.UseVisualStyleBackColor = true;
            // 
            // cbArtikl
            // 
            this.cbArtikl.Location = new System.Drawing.Point(711, 20);
            this.cbArtikl.Name = "cbArtikl";
            this.cbArtikl.Size = new System.Drawing.Size(168, 23);
            this.cbArtikl.TabIndex = 49;
            // 
            // txtPartner
            // 
            this.txtPartner.Location = new System.Drawing.Point(119, 51);
            this.txtPartner.Name = "txtPartner";
            this.txtPartner.Size = new System.Drawing.Size(168, 23);
            this.txtPartner.TabIndex = 50;
            this.txtPartner.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartner_KeyDown);
            // 
            // cbIzradio
            // 
            this.cbIzradio.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbIzradio.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbIzradio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIzradio.FormattingEnabled = true;
            this.cbIzradio.Location = new System.Drawing.Point(711, 46);
            this.cbIzradio.Name = "cbIzradio";
            this.cbIzradio.Size = new System.Drawing.Size(168, 24);
            this.cbIzradio.TabIndex = 63;
            // 
            // cbSkladiste
            // 
            this.cbSkladiste.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbSkladiste.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbSkladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkladiste.FormattingEnabled = true;
            this.cbSkladiste.Location = new System.Drawing.Point(711, 74);
            this.cbSkladiste.Name = "cbSkladiste";
            this.cbSkladiste.Size = new System.Drawing.Size(168, 24);
            this.cbSkladiste.TabIndex = 62;
            // 
            // cbValuta
            // 
            this.cbValuta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbValuta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbValuta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbValuta.FormattingEnabled = true;
            this.cbValuta.Location = new System.Drawing.Point(413, 75);
            this.cbValuta.Name = "cbValuta";
            this.cbValuta.Size = new System.Drawing.Size(168, 24);
            this.cbValuta.TabIndex = 62;
            // 
            // cbVD
            // 
            this.cbVD.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbVD.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbVD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVD.FormattingEnabled = true;
            this.cbVD.Location = new System.Drawing.Point(119, 76);
            this.cbVD.Name = "cbVD";
            this.cbVD.Size = new System.Drawing.Size(168, 24);
            this.cbVD.TabIndex = 64;
            // 
            // dtpDO
            // 
            this.dtpDO.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDO.Location = new System.Drawing.Point(413, 49);
            this.dtpDO.Name = "dtpDO";
            this.dtpDO.Size = new System.Drawing.Size(168, 23);
            this.dtpDO.TabIndex = 61;
            // 
            // txtBroj
            // 
            this.txtBroj.Location = new System.Drawing.Point(119, 26);
            this.txtBroj.Name = "txtBroj";
            this.txtBroj.Size = new System.Drawing.Size(168, 23);
            this.txtBroj.TabIndex = 51;
            // 
            // dtpOD
            // 
            this.dtpOD.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpOD.Location = new System.Drawing.Point(413, 23);
            this.dtpOD.Name = "dtpOD";
            this.dtpOD.Size = new System.Drawing.Size(168, 23);
            this.dtpOD.TabIndex = 60;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(625, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 17);
            this.label7.TabIndex = 54;
            this.label7.Text = "Skladište:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 17);
            this.label1.TabIndex = 52;
            this.label1.Text = "Broj kalkulacije:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(326, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 17);
            this.label5.TabIndex = 54;
            this.label5.Text = "Valuta:";
            // 
            // ch
            // 
            this.ch.AutoSize = true;
            this.ch.Location = new System.Drawing.Point(625, 26);
            this.ch.Name = "ch";
            this.ch.Size = new System.Drawing.Size(83, 17);
            this.ch.TabIndex = 58;
            this.ch.Text = "Šifra artikla:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(326, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 17);
            this.label4.TabIndex = 59;
            this.label4.Text = "Do datuma:";
            // 
            // lbIzradio
            // 
            this.lbIzradio.AutoSize = true;
            this.lbIzradio.Location = new System.Drawing.Point(625, 52);
            this.lbIzradio.Name = "lbIzradio";
            this.lbIzradio.Size = new System.Drawing.Size(81, 17);
            this.lbIzradio.TabIndex = 55;
            this.lbIzradio.Text = "Izradio zap:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 17);
            this.label2.TabIndex = 57;
            this.label2.Text = "Šifra partnera:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 17);
            this.label6.TabIndex = 56;
            this.label6.Text = "Vrsta dok.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(326, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 17);
            this.label3.TabIndex = 53;
            this.label3.Text = "Od datuma:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::PCPOS.Properties.Resources._1059;
            this.pictureBox1.Location = new System.Drawing.Point(906, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(71, 65);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox1, "Traži");
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.pic_MouseEnter);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pic_MouseLeave);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.Image = global::PCPOS.Properties.Resources.Actions_application_exit_icon;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(859, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 38);
            this.button1.TabIndex = 24;
            this.button1.Text = "Izlaz      ";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnUrediSifru
            // 
            this.btnUrediSifru.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnUrediSifru.Image = global::PCPOS.Properties.Resources.edit_icon;
            this.btnUrediSifru.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUrediSifru.Location = new System.Drawing.Point(167, 12);
            this.btnUrediSifru.Name = "btnUrediSifru";
            this.btnUrediSifru.Size = new System.Drawing.Size(150, 38);
            this.btnUrediSifru.TabIndex = 23;
            this.btnUrediSifru.Text = "Uredi kalkulaciju";
            this.btnUrediSifru.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUrediSifru.UseVisualStyleBackColor = true;
            this.btnUrediSifru.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSveFakture
            // 
            this.btnSveFakture.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSveFakture.Image = global::PCPOS.Properties.Resources.print_printer;
            this.btnSveFakture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSveFakture.Location = new System.Drawing.Point(12, 12);
            this.btnSveFakture.Name = "btnSveFakture";
            this.btnSveFakture.Size = new System.Drawing.Size(147, 38);
            this.btnSveFakture.TabIndex = 22;
            this.btnSveFakture.Text = "Ispis kalkulacije";
            this.btnSveFakture.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSveFakture.UseVisualStyleBackColor = true;
            this.btnSveFakture.Click += new System.EventHandler(this.btnSveFakture_Click);
            // 
            // broj
            // 
            this.broj.HeaderText = "Broj";
            this.broj.Name = "broj";
            this.broj.ReadOnly = true;
            // 
            // orginalni_dokumenat
            // 
            this.orginalni_dokumenat.HeaderText = "Org.Doc";
            this.orginalni_dokumenat.Name = "orginalni_dokumenat";
            this.orginalni_dokumenat.ReadOnly = true;
            // 
            // dobavljac
            // 
            this.dobavljac.HeaderText = "Dobavljač";
            this.dobavljac.Name = "dobavljac";
            this.dobavljac.ReadOnly = true;
            // 
            // skladiste
            // 
            this.skladiste.HeaderText = "Skladište";
            this.skladiste.Name = "skladiste";
            this.skladiste.ReadOnly = true;
            // 
            // datum
            // 
            this.datum.HeaderText = "Datum";
            this.datum.Name = "datum";
            this.datum.ReadOnly = true;
            // 
            // ukupno_vpc
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ukupno_vpc.DefaultCellStyle = dataGridViewCellStyle2;
            this.ukupno_vpc.HeaderText = "Ukupno VPC";
            this.ukupno_vpc.Name = "ukupno_vpc";
            this.ukupno_vpc.ReadOnly = true;
            // 
            // ukupno_mpc
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ukupno_mpc.DefaultCellStyle = dataGridViewCellStyle3;
            this.ukupno_mpc.HeaderText = "Ukupno MPC";
            this.ukupno_mpc.Name = "ukupno_mpc";
            this.ukupno_mpc.ReadOnly = true;
            // 
            // fakturni_iznos
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.fakturni_iznos.DefaultCellStyle = dataGridViewCellStyle4;
            this.fakturni_iznos.HeaderText = "Fakturni iznos";
            this.fakturni_iznos.Name = "fakturni_iznos";
            this.fakturni_iznos.ReadOnly = true;
            // 
            // placeno
            // 
            this.placeno.HeaderText = "Plačeno";
            this.placeno.Name = "placeno";
            this.placeno.ReadOnly = true;
            // 
            // id_skladiste
            // 
            this.id_skladiste.HeaderText = "id_skladiste";
            this.id_skladiste.Name = "id_skladiste";
            this.id_skladiste.ReadOnly = true;
            this.id_skladiste.Visible = false;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // id_partner
            // 
            this.id_partner.HeaderText = "id_partner";
            this.id_partner.Name = "id_partner";
            this.id_partner.ReadOnly = true;
            this.id_partner.Visible = false;
            // 
            // frmPopisKalkulacija
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 599);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnUrediSifru);
            this.Controls.Add(this.btnSveFakture);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmPopisKalkulacija";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kalkulacije";
            this.Load += new System.EventHandler(this.frmPopisKalkulacija_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox chbDO;
        private System.Windows.Forms.CheckBox chbArtikl;
        private System.Windows.Forms.CheckBox chbSifra;
        private System.Windows.Forms.CheckBox chbValuta;
        private System.Windows.Forms.CheckBox chbOD;
        private System.Windows.Forms.CheckBox chbIzradio;
        private System.Windows.Forms.CheckBox chbVD;
        private System.Windows.Forms.CheckBox chbBroj;
        public System.Windows.Forms.TextBox cbArtikl;
        public System.Windows.Forms.TextBox txtPartner;
        private System.Windows.Forms.ComboBox cbIzradio;
        private System.Windows.Forms.ComboBox cbValuta;
        private System.Windows.Forms.ComboBox cbVD;
        private System.Windows.Forms.DateTimePicker dtpDO;
        public System.Windows.Forms.TextBox txtBroj;
        private System.Windows.Forms.DateTimePicker dtpOD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label ch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbIzradio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chbSkladiste;
        private System.Windows.Forms.ComboBox cbSkladiste;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnUrediSifru;
        private System.Windows.Forms.Button btnSveFakture;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj;
        private System.Windows.Forms.DataGridViewTextBoxColumn orginalni_dokumenat;
        private System.Windows.Forms.DataGridViewTextBoxColumn dobavljac;
        private System.Windows.Forms.DataGridViewTextBoxColumn skladiste;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum;
        private System.Windows.Forms.DataGridViewTextBoxColumn ukupno_vpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ukupno_mpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn fakturni_iznos;
        private System.Windows.Forms.DataGridViewButtonColumn placeno;
        private System.Windows.Forms.DataGridViewButtonColumn id_skladiste;
        private System.Windows.Forms.DataGridViewButtonColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_partner;
    }
}