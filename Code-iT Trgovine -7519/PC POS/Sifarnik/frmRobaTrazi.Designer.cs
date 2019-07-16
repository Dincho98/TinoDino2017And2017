namespace PCPOS
{
    partial class frmRobaTrazi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRobaTrazi));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.nuPrvih = new System.Windows.Forms.NumericUpDown();
            this.chbIspisSvihArtikla = new System.Windows.Forms.CheckBox();
            this.chbRNS = new System.Windows.Forms.CheckBox();
            this.txtTraziPremaSifri = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnKolicina = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.txtIme = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ime = new System.Windows.Forms.RadioButton();
            this.sifra = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chProizvodac = new System.Windows.Forms.CheckBox();
            this.chDobavljac = new System.Windows.Forms.CheckBox();
            this.chGrupa = new System.Windows.Forms.CheckBox();
            this.cbGrupa = new System.Windows.Forms.ComboBox();
            this.cbProizvodac = new System.Windows.Forms.ComboBox();
            this.cbPartner = new System.Windows.Forms.ComboBox();
            this.lm = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTrazi = new System.Windows.Forms.Button();
            this.cmbSkladista = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuPrvih)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tabControl1.ItemSize = new System.Drawing.Size(100, 30);
            this.tabControl1.Location = new System.Drawing.Point(-3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(988, 663);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.SlateGray;
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage1.Controls.Add(this.cmbSkladista);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.nuPrvih);
            this.tabPage1.Controls.Add(this.chbIspisSvihArtikla);
            this.tabPage1.Controls.Add(this.chbRNS);
            this.tabPage1.Controls.Add(this.txtTraziPremaSifri);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.btnKolicina);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.dataGridView2);
            this.tabPage1.Controls.Add(this.txtIme);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(980, 625);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "          Brzo pretraživanje          ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(685, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 17);
            this.label7.TabIndex = 4;
            this.label7.Text = "Prvih:";
            // 
            // nuPrvih
            // 
            this.nuPrvih.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.nuPrvih.Location = new System.Drawing.Point(688, 21);
            this.nuPrvih.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nuPrvih.Name = "nuPrvih";
            this.nuPrvih.Size = new System.Drawing.Size(61, 26);
            this.nuPrvih.TabIndex = 5;
            this.nuPrvih.ValueChanged += new System.EventHandler(this.nuPrvih_ValueChanged);
            // 
            // chbIspisSvihArtikla
            // 
            this.chbIspisSvihArtikla.AutoSize = true;
            this.chbIspisSvihArtikla.Location = new System.Drawing.Point(69, 68);
            this.chbIspisSvihArtikla.Name = "chbIspisSvihArtikla";
            this.chbIspisSvihArtikla.Size = new System.Drawing.Size(126, 21);
            this.chbIspisSvihArtikla.TabIndex = 7;
            this.chbIspisSvihArtikla.Text = "Ispis svih artikla";
            this.chbIspisSvihArtikla.UseVisualStyleBackColor = true;
            this.chbIspisSvihArtikla.CheckedChanged += new System.EventHandler(this.chbIspisSvihArtikla_CheckedChanged);
            // 
            // chbRNS
            // 
            this.chbRNS.AutoSize = true;
            this.chbRNS.Checked = true;
            this.chbRNS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbRNS.Location = new System.Drawing.Point(215, 67);
            this.chbRNS.Name = "chbRNS";
            this.chbRNS.Size = new System.Drawing.Size(233, 21);
            this.chbRNS.TabIndex = 8;
            this.chbRNS.Text = "Pretraži robu koja je na skladištu";
            this.chbRNS.UseVisualStyleBackColor = true;
            this.chbRNS.CheckedChanged += new System.EventHandler(this.chbRNS_CheckedChanged);
            // 
            // txtTraziPremaSifri
            // 
            this.txtTraziPremaSifri.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtTraziPremaSifri.Location = new System.Drawing.Point(400, 22);
            this.txtTraziPremaSifri.Name = "txtTraziPremaSifri";
            this.txtTraziPremaSifri.Size = new System.Drawing.Size(282, 26);
            this.txtTraziPremaSifri.TabIndex = 3;
            this.txtTraziPremaSifri.TextChanged += new System.EventHandler(this.txtTraziPremaSifri_TextChanged_1);
            this.txtTraziPremaSifri.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIme_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(396, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Traži prema šifri";
            // 
            // btnKolicina
            // 
            this.btnKolicina.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKolicina.BackColor = System.Drawing.Color.Transparent;
            this.btnKolicina.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnKolicina.BackgroundImage")));
            this.btnKolicina.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnKolicina.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKolicina.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnKolicina.FlatAppearance.BorderSize = 0;
            this.btnKolicina.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnKolicina.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSlateGray;
            this.btnKolicina.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSlateGray;
            this.btnKolicina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKolicina.Font = new System.Drawing.Font("Arial Narrow", 11F);
            this.btnKolicina.Location = new System.Drawing.Point(773, 21);
            this.btnKolicina.Name = "btnKolicina";
            this.btnKolicina.Size = new System.Drawing.Size(185, 52);
            this.btnKolicina.TabIndex = 10;
            this.btnKolicina.Text = "Dodaj novi artikl";
            this.btnKolicina.UseVisualStyleBackColor = false;
            this.btnKolicina.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(21, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Artikli";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView2.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dataGridView2.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView2.Location = new System.Drawing.Point(21, 89);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(937, 520);
            this.dataGridView2.TabIndex = 11;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            // 
            // txtIme
            // 
            this.txtIme.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtIme.Location = new System.Drawing.Point(21, 23);
            this.txtIme.Name = "txtIme";
            this.txtIme.Size = new System.Drawing.Size(358, 26);
            this.txtIme.TabIndex = 1;
            this.txtIme.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.txtIme.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIme_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 233);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 17);
            this.label9.TabIndex = 12;
            this.label9.Text = "Artikli";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(126, 17);
            this.label10.TabIndex = 0;
            this.label10.Text = "Traži prema imenu";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.LightSlateGray;
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage2.Controls.Add(this.ime);
            this.tabPage2.Controls.Add(this.sifra);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Controls.Add(this.txtSifra);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.btnTrazi);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(980, 625);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "          Napredno pretraživanje          ";
            // 
            // ime
            // 
            this.ime.AutoSize = true;
            this.ime.Location = new System.Drawing.Point(183, 67);
            this.ime.Name = "ime";
            this.ime.Size = new System.Drawing.Size(197, 21);
            this.ime.TabIndex = 4;
            this.ime.Text = "Pretraži prema nazivu robe";
            this.ime.UseVisualStyleBackColor = true;
            // 
            // sifra
            // 
            this.sifra.AutoSize = true;
            this.sifra.Checked = true;
            this.sifra.Location = new System.Drawing.Point(24, 67);
            this.sifra.Name = "sifra";
            this.sifra.Size = new System.Drawing.Size(145, 21);
            this.sifra.TabIndex = 3;
            this.sifra.TabStop = true;
            this.sifra.Text = "Pretraži prema šifri";
            this.sifra.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chProizvodac);
            this.groupBox1.Controls.Add(this.chDobavljac);
            this.groupBox1.Controls.Add(this.chGrupa);
            this.groupBox1.Controls.Add(this.cbGrupa);
            this.groupBox1.Controls.Add(this.cbProizvodac);
            this.groupBox1.Controls.Add(this.cbPartner);
            this.groupBox1.Controls.Add(this.lm);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(20, 103);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(931, 108);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dodatne opcije";
            // 
            // chProizvodac
            // 
            this.chProizvodac.AutoSize = true;
            this.chProizvodac.Location = new System.Drawing.Point(889, 69);
            this.chProizvodac.Name = "chProizvodac";
            this.chProizvodac.Size = new System.Drawing.Size(15, 14);
            this.chProizvodac.TabIndex = 8;
            this.chProizvodac.UseVisualStyleBackColor = true;
            // 
            // chDobavljac
            // 
            this.chDobavljac.AutoSize = true;
            this.chDobavljac.Location = new System.Drawing.Point(575, 71);
            this.chDobavljac.Name = "chDobavljac";
            this.chDobavljac.Size = new System.Drawing.Size(15, 14);
            this.chDobavljac.TabIndex = 5;
            this.chDobavljac.UseVisualStyleBackColor = true;
            // 
            // chGrupa
            // 
            this.chGrupa.AutoSize = true;
            this.chGrupa.Location = new System.Drawing.Point(253, 71);
            this.chGrupa.Name = "chGrupa";
            this.chGrupa.Size = new System.Drawing.Size(15, 14);
            this.chGrupa.TabIndex = 3;
            this.chGrupa.UseVisualStyleBackColor = true;
            // 
            // cbGrupa
            // 
            this.cbGrupa.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbGrupa.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbGrupa.FormattingEnabled = true;
            this.cbGrupa.Items.AddRange(new object[] {
            "     "});
            this.cbGrupa.Location = new System.Drawing.Point(26, 65);
            this.cbGrupa.Name = "cbGrupa";
            this.cbGrupa.Size = new System.Drawing.Size(223, 24);
            this.cbGrupa.TabIndex = 1;
            // 
            // cbProizvodac
            // 
            this.cbProizvodac.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbProizvodac.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbProizvodac.FormattingEnabled = true;
            this.cbProizvodac.Location = new System.Drawing.Point(631, 63);
            this.cbProizvodac.Name = "cbProizvodac";
            this.cbProizvodac.Size = new System.Drawing.Size(255, 24);
            this.cbProizvodac.TabIndex = 7;
            // 
            // cbPartner
            // 
            this.cbPartner.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbPartner.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbPartner.FormattingEnabled = true;
            this.cbPartner.Location = new System.Drawing.Point(286, 64);
            this.cbPartner.Name = "cbPartner";
            this.cbPartner.Size = new System.Drawing.Size(283, 24);
            this.cbPartner.TabIndex = 4;
            // 
            // lm
            // 
            this.lm.AutoSize = true;
            this.lm.Location = new System.Drawing.Point(628, 44);
            this.lm.Name = "lm";
            this.lm.Size = new System.Drawing.Size(160, 17);
            this.lm.TabIndex = 6;
            this.lm.Text = "Traži prema proizviđaču";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(283, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(156, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Traži prema dobavljaču";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Traži po grupi";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.Location = new System.Drawing.Point(21, 253);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(937, 360);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // txtSifra
            // 
            this.txtSifra.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtSifra.Location = new System.Drawing.Point(21, 34);
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(358, 26);
            this.txtSifra.TabIndex = 1;
            this.txtSifra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifra_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(17, 234);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 17);
            this.label6.TabIndex = 6;
            this.label6.Text = "Artikli";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Traži";
            // 
            // btnTrazi
            // 
            this.btnTrazi.Location = new System.Drawing.Point(399, 34);
            this.btnTrazi.Name = "btnTrazi";
            this.btnTrazi.Size = new System.Drawing.Size(114, 26);
            this.btnTrazi.TabIndex = 2;
            this.btnTrazi.Text = "Traži";
            this.btnTrazi.UseVisualStyleBackColor = true;
            this.btnTrazi.Click += new System.EventHandler(this.btnTrazi_Click);
            // 
            // cmbSkladista
            // 
            this.cmbSkladista.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSkladista.FormattingEnabled = true;
            this.cmbSkladista.Location = new System.Drawing.Point(496, 59);
            this.cmbSkladista.Name = "cmbSkladista";
            this.cmbSkladista.Size = new System.Drawing.Size(253, 24);
            this.cmbSkladista.TabIndex = 13;
            // 
            // frmRobaTrazi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmRobaTrazi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Traži artikle/usluge";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmRobaTrazi_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuPrvih)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbGrupa;
        private System.Windows.Forms.ComboBox cbProizvodac;
        private System.Windows.Forms.ComboBox cbPartner;
        private System.Windows.Forms.Label lm;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTrazi;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TextBox txtIme;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton ime;
        private System.Windows.Forms.RadioButton sifra;
        private System.Windows.Forms.CheckBox chProizvodac;
        private System.Windows.Forms.CheckBox chDobavljac;
        private System.Windows.Forms.CheckBox chGrupa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnKolicina;
        private System.Windows.Forms.TextBox txtTraziPremaSifri;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chbRNS;
        private System.Windows.Forms.CheckBox chbIspisSvihArtikla;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nuPrvih;
        private System.Windows.Forms.ComboBox cmbSkladista;
    }
}