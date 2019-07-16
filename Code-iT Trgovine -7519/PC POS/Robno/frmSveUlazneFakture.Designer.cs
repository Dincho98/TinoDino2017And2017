namespace PCPOS
{
    partial class frmSveUlazneFakture
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSveUlazneFakture));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnHub = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.chbDO = new System.Windows.Forms.CheckBox();
            this.chbSifra = new System.Windows.Forms.CheckBox();
            this.chbValuta = new System.Windows.Forms.CheckBox();
            this.chbOD = new System.Windows.Forms.CheckBox();
            this.chbIzradio = new System.Windows.Forms.CheckBox();
            this.chbBroj = new System.Windows.Forms.CheckBox();
            this.txtPartner = new System.Windows.Forms.TextBox();
            this.cbIzradio = new System.Windows.Forms.ComboBox();
            this.cbValuta = new System.Windows.Forms.ComboBox();
            this.dtpDO = new System.Windows.Forms.DateTimePicker();
            this.txtBroj = new System.Windows.Forms.TextBox();
            this.dtpOD = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbIzradio = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.btnUrediSifru = new System.Windows.Forms.Button();
            this.broj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valuta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ukupno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hub_kreirano = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kreiraj_hub = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.placeno = new System.Windows.Forms.DataGridViewButtonColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.godina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sifra_partner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.broj,
            this.datum,
            this.Valuta,
            this.partner,
            this.ukupno,
            this.hub_kreirano,
            this.kreiraj_hub,
            this.placeno,
            this.id,
            this.godina,
            this.sifra_partner});
            this.dgv.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.GridColor = System.Drawing.Color.Gainsboro;
            this.dgv.Location = new System.Drawing.Point(13, 182);
            this.dgv.Name = "dgv";
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(980, 643);
            this.dgv.TabIndex = 0;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btnHub);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.chbDO);
            this.groupBox1.Controls.Add(this.chbSifra);
            this.groupBox1.Controls.Add(this.chbValuta);
            this.groupBox1.Controls.Add(this.chbOD);
            this.groupBox1.Controls.Add(this.chbIzradio);
            this.groupBox1.Controls.Add(this.chbBroj);
            this.groupBox1.Controls.Add(this.txtPartner);
            this.groupBox1.Controls.Add(this.cbIzradio);
            this.groupBox1.Controls.Add(this.cbValuta);
            this.groupBox1.Controls.Add(this.dtpDO);
            this.groupBox1.Controls.Add(this.txtBroj);
            this.groupBox1.Controls.Add(this.dtpOD);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lbIzradio);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox1.Location = new System.Drawing.Point(13, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(979, 115);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pretraživanje fakture";
            // 
            // btnHub
            // 
            this.btnHub.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnHub.Image = global::PCPOS.Properties.Resources.edit_icon;
            this.btnHub.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHub.Location = new System.Drawing.Point(843, 68);
            this.btnHub.Name = "btnHub";
            this.btnHub.Size = new System.Drawing.Size(130, 38);
            this.btnHub.TabIndex = 74;
            this.btnHub.Text = "Kreiraj HUB";
            this.btnHub.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnHub.UseVisualStyleBackColor = true;
            this.btnHub.Click += new System.EventHandler(this.btnHub_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(294, 53);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 19);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 73;
            this.pictureBox2.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox2, "Traži");
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // chbDO
            // 
            this.chbDO.AutoSize = true;
            this.chbDO.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chbDO.Location = new System.Drawing.Point(581, 53);
            this.chbDO.Name = "chbDO";
            this.chbDO.Size = new System.Drawing.Size(15, 14);
            this.chbDO.TabIndex = 68;
            this.chbDO.UseVisualStyleBackColor = true;
            // 
            // chbSifra
            // 
            this.chbSifra.AutoSize = true;
            this.chbSifra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chbSifra.Location = new System.Drawing.Point(724, 23);
            this.chbSifra.Name = "chbSifra";
            this.chbSifra.Size = new System.Drawing.Size(15, 14);
            this.chbSifra.TabIndex = 67;
            this.chbSifra.UseVisualStyleBackColor = true;
            this.chbSifra.Visible = false;
            // 
            // chbValuta
            // 
            this.chbValuta.AutoSize = true;
            this.chbValuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chbValuta.Location = new System.Drawing.Point(581, 81);
            this.chbValuta.Name = "chbValuta";
            this.chbValuta.Size = new System.Drawing.Size(15, 14);
            this.chbValuta.TabIndex = 65;
            this.chbValuta.UseVisualStyleBackColor = true;
            // 
            // chbOD
            // 
            this.chbOD.AutoSize = true;
            this.chbOD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chbOD.Location = new System.Drawing.Point(581, 26);
            this.chbOD.Name = "chbOD";
            this.chbOD.Size = new System.Drawing.Size(15, 14);
            this.chbOD.TabIndex = 72;
            this.chbOD.UseVisualStyleBackColor = true;
            // 
            // chbIzradio
            // 
            this.chbIzradio.AutoSize = true;
            this.chbIzradio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chbIzradio.Location = new System.Drawing.Point(297, 82);
            this.chbIzradio.Name = "chbIzradio";
            this.chbIzradio.Size = new System.Drawing.Size(15, 14);
            this.chbIzradio.TabIndex = 70;
            this.chbIzradio.UseVisualStyleBackColor = true;
            // 
            // chbBroj
            // 
            this.chbBroj.AutoSize = true;
            this.chbBroj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chbBroj.Location = new System.Drawing.Point(297, 30);
            this.chbBroj.Name = "chbBroj";
            this.chbBroj.Size = new System.Drawing.Size(15, 14);
            this.chbBroj.TabIndex = 69;
            this.chbBroj.UseVisualStyleBackColor = true;
            // 
            // txtPartner
            // 
            this.txtPartner.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtPartner.Location = new System.Drawing.Point(123, 51);
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
            this.cbIzradio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbIzradio.FormattingEnabled = true;
            this.cbIzradio.Location = new System.Drawing.Point(123, 77);
            this.cbIzradio.Name = "cbIzradio";
            this.cbIzradio.Size = new System.Drawing.Size(168, 24);
            this.cbIzradio.TabIndex = 63;
            // 
            // cbValuta
            // 
            this.cbValuta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbValuta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbValuta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbValuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbValuta.FormattingEnabled = true;
            this.cbValuta.Location = new System.Drawing.Point(410, 75);
            this.cbValuta.Name = "cbValuta";
            this.cbValuta.Size = new System.Drawing.Size(168, 24);
            this.cbValuta.TabIndex = 62;
            // 
            // dtpDO
            // 
            this.dtpDO.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDO.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDO.Location = new System.Drawing.Point(410, 49);
            this.dtpDO.Name = "dtpDO";
            this.dtpDO.Size = new System.Drawing.Size(168, 23);
            this.dtpDO.TabIndex = 61;
            // 
            // txtBroj
            // 
            this.txtBroj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtBroj.Location = new System.Drawing.Point(123, 26);
            this.txtBroj.Name = "txtBroj";
            this.txtBroj.Size = new System.Drawing.Size(168, 23);
            this.txtBroj.TabIndex = 51;
            // 
            // dtpOD
            // 
            this.dtpOD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpOD.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpOD.Location = new System.Drawing.Point(410, 23);
            this.dtpOD.Name = "dtpOD";
            this.dtpOD.Size = new System.Drawing.Size(168, 23);
            this.dtpOD.TabIndex = 60;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(22, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 17);
            this.label1.TabIndex = 52;
            this.label1.Text = "Broj fakture:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(323, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 17);
            this.label5.TabIndex = 54;
            this.label5.Text = "Valuta:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(323, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 17);
            this.label4.TabIndex = 59;
            this.label4.Text = "Do datuma:";
            // 
            // lbIzradio
            // 
            this.lbIzradio.AutoSize = true;
            this.lbIzradio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lbIzradio.Location = new System.Drawing.Point(22, 79);
            this.lbIzradio.Name = "lbIzradio";
            this.lbIzradio.Size = new System.Drawing.Size(81, 17);
            this.lbIzradio.TabIndex = 55;
            this.lbIzradio.Text = "Izradio zap:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(22, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 17);
            this.label2.TabIndex = 57;
            this.label2.Text = "Šifra partnera:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(323, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 17);
            this.label3.TabIndex = 53;
            this.label3.Text = "Od datuma:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(626, 26);
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
            this.button1.Location = new System.Drawing.Point(860, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 38);
            this.button1.TabIndex = 21;
            this.button1.Text = "Izlaz      ";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnUrediSifru
            // 
            this.btnUrediSifru.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnUrediSifru.Image = global::PCPOS.Properties.Resources.edit_icon;
            this.btnUrediSifru.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUrediSifru.Location = new System.Drawing.Point(13, 12);
            this.btnUrediSifru.Name = "btnUrediSifru";
            this.btnUrediSifru.Size = new System.Drawing.Size(130, 38);
            this.btnUrediSifru.TabIndex = 20;
            this.btnUrediSifru.Text = "Uredi fakturu";
            this.btnUrediSifru.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUrediSifru.UseVisualStyleBackColor = true;
            this.btnUrediSifru.Click += new System.EventHandler(this.btnUrediSifru_Click);
            // 
            // broj
            // 
            this.broj.HeaderText = "Broj fakture";
            this.broj.Name = "broj";
            this.broj.ReadOnly = true;
            // 
            // datum
            // 
            this.datum.HeaderText = "Datum";
            this.datum.Name = "datum";
            this.datum.ReadOnly = true;
            // 
            // Valuta
            // 
            this.Valuta.HeaderText = "Valuta";
            this.Valuta.Name = "Valuta";
            this.Valuta.ReadOnly = true;
            // 
            // partner
            // 
            this.partner.HeaderText = "Partner";
            this.partner.Name = "partner";
            this.partner.ReadOnly = true;
            // 
            // ukupno
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            this.ukupno.DefaultCellStyle = dataGridViewCellStyle1;
            this.ukupno.HeaderText = "Ukupno";
            this.ukupno.Name = "ukupno";
            this.ukupno.ReadOnly = true;
            // 
            // hub_kreirano
            // 
            this.hub_kreirano.HeaderText = "HUB kreirano";
            this.hub_kreirano.Name = "hub_kreirano";
            this.hub_kreirano.ReadOnly = true;
            // 
            // kreiraj_hub
            // 
            this.kreiraj_hub.HeaderText = "Kreiraj HUB";
            this.kreiraj_hub.Name = "kreiraj_hub";
            // 
            // placeno
            // 
            this.placeno.FillWeight = 125F;
            this.placeno.HeaderText = "Plaćeno";
            this.placeno.Name = "placeno";
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // godina
            // 
            this.godina.HeaderText = "godina";
            this.godina.Name = "godina";
            this.godina.Visible = false;
            // 
            // sifra_partner
            // 
            this.sifra_partner.HeaderText = "Šifra partnera";
            this.sifra_partner.Name = "sifra_partner";
            this.sifra_partner.Visible = false;
            // 
            // frmSveUlazneFakture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(1004, 837);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnUrediSifru);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgv);
            this.Name = "frmSveUlazneFakture";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sve ulazne fakture";
            this.Load += new System.EventHandler(this.frmSveFakture_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox chbDO;
        private System.Windows.Forms.CheckBox chbSifra;
        private System.Windows.Forms.CheckBox chbValuta;
        private System.Windows.Forms.CheckBox chbOD;
        private System.Windows.Forms.CheckBox chbIzradio;
        private System.Windows.Forms.CheckBox chbBroj;
        public System.Windows.Forms.TextBox txtPartner;
        private System.Windows.Forms.ComboBox cbIzradio;
        private System.Windows.Forms.ComboBox cbValuta;
        private System.Windows.Forms.DateTimePicker dtpDO;
        public System.Windows.Forms.TextBox txtBroj;
        private System.Windows.Forms.DateTimePicker dtpOD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbIzradio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnUrediSifru;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnHub;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valuta;
        private System.Windows.Forms.DataGridViewTextBoxColumn partner;
        private System.Windows.Forms.DataGridViewTextBoxColumn ukupno;
        private System.Windows.Forms.DataGridViewTextBoxColumn hub_kreirano;
        private System.Windows.Forms.DataGridViewCheckBoxColumn kreiraj_hub;
        private System.Windows.Forms.DataGridViewButtonColumn placeno;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn godina;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra_partner;
    }
}