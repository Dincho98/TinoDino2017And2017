namespace PCPOS {
    partial class frmRobuPreuzeo {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRobuPreuzeo));
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.preuzeo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dokumenat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.porez = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtNaziv = new System.Windows.Forms.TextBox();
            this.btnTrazi = new System.Windows.Forms.Button();
            this.btnPartner = new System.Windows.Forms.PictureBox();
            this.btnIspis = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tdOdDatuma = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tdDoDatuma = new System.Windows.Forms.DateTimePicker();
            this.txtNazivPreuzeo = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPreuzeo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPartner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSifra
            // 
            this.txtSifra.Location = new System.Drawing.Point(84, 40);
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(66, 20);
            this.txtSifra.TabIndex = 0;
            this.txtSifra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifra_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Šifra partnera:";
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
            this.preuzeo,
            this.dokumenat,
            this.vpc,
            this.porez,
            this.iznos});
            this.dgv.Location = new System.Drawing.Point(12, 103);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(623, 493);
            this.dgv.TabIndex = 2;
            // 
            // preuzeo
            // 
            this.preuzeo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.preuzeo.HeaderText = "Preuzeo";
            this.preuzeo.Name = "preuzeo";
            this.preuzeo.ReadOnly = true;
            // 
            // dokumenat
            // 
            this.dokumenat.HeaderText = "Dokumenat";
            this.dokumenat.Name = "dokumenat";
            this.dokumenat.ReadOnly = true;
            // 
            // vpc
            // 
            this.vpc.HeaderText = "Bez poreza";
            this.vpc.Name = "vpc";
            this.vpc.ReadOnly = true;
            // 
            // porez
            // 
            this.porez.HeaderText = "Porez";
            this.porez.Name = "porez";
            this.porez.ReadOnly = true;
            // 
            // iznos
            // 
            this.iznos.HeaderText = "Iznos";
            this.iznos.Name = "iznos";
            this.iznos.ReadOnly = true;
            // 
            // txtNaziv
            // 
            this.txtNaziv.Location = new System.Drawing.Point(184, 40);
            this.txtNaziv.Name = "txtNaziv";
            this.txtNaziv.Size = new System.Drawing.Size(349, 20);
            this.txtNaziv.TabIndex = 559;
            // 
            // btnTrazi
            // 
            this.btnTrazi.Location = new System.Drawing.Point(539, 8);
            this.btnTrazi.Name = "btnTrazi";
            this.btnTrazi.Size = new System.Drawing.Size(96, 54);
            this.btnTrazi.TabIndex = 560;
            this.btnTrazi.Text = "Traži";
            this.btnTrazi.UseVisualStyleBackColor = true;
            this.btnTrazi.Click += new System.EventHandler(this.btnTrazi_Click);
            // 
            // btnPartner
            // 
            this.btnPartner.BackColor = System.Drawing.Color.Transparent;
            this.btnPartner.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPartner.Image = ((System.Drawing.Image)(resources.GetObject("btnPartner.Image")));
            this.btnPartner.Location = new System.Drawing.Point(151, 37);
            this.btnPartner.Name = "btnPartner";
            this.btnPartner.Size = new System.Drawing.Size(31, 28);
            this.btnPartner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnPartner.TabIndex = 558;
            this.btnPartner.TabStop = false;
            this.btnPartner.Click += new System.EventHandler(this.btnPartner_Click);
            // 
            // btnIspis
            // 
            this.btnIspis.Location = new System.Drawing.Point(462, 598);
            this.btnIspis.Name = "btnIspis";
            this.btnIspis.Size = new System.Drawing.Size(174, 29);
            this.btnIspis.TabIndex = 561;
            this.btnIspis.Text = "Ispis sortirano po \"Preuzeo\"";
            this.btnIspis.UseVisualStyleBackColor = true;
            this.btnIspis.Click += new System.EventHandler(this.btnIspis_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(242, 598);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(214, 29);
            this.button2.TabIndex = 562;
            this.button2.Text = "Ispis sortirano po \"Broju fakture\"";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tdOdDatuma
            // 
            this.tdOdDatuma.CustomFormat = "dd.MM.yyyy H:mm";
            this.tdOdDatuma.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tdOdDatuma.Location = new System.Drawing.Point(84, 70);
            this.tdOdDatuma.Name = "tdOdDatuma";
            this.tdOdDatuma.Size = new System.Drawing.Size(167, 20);
            this.tdOdDatuma.TabIndex = 563;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 564;
            this.label2.Text = "Od datuma:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(288, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 566;
            this.label3.Text = "Do datuma:";
            // 
            // tdDoDatuma
            // 
            this.tdDoDatuma.CustomFormat = "dd.MM.yyyy H:mm";
            this.tdDoDatuma.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tdDoDatuma.Location = new System.Drawing.Point(356, 71);
            this.tdDoDatuma.Name = "tdDoDatuma";
            this.tdDoDatuma.Size = new System.Drawing.Size(177, 20);
            this.tdDoDatuma.TabIndex = 565;
            // 
            // txtNazivPreuzeo
            // 
            this.txtNazivPreuzeo.Location = new System.Drawing.Point(184, 10);
            this.txtNazivPreuzeo.Name = "txtNazivPreuzeo";
            this.txtNazivPreuzeo.Size = new System.Drawing.Size(349, 20);
            this.txtNazivPreuzeo.TabIndex = 570;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(151, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(31, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 569;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 568;
            this.label4.Text = "Preuzeo:";
            // 
            // txtPreuzeo
            // 
            this.txtPreuzeo.Location = new System.Drawing.Point(84, 10);
            this.txtPreuzeo.Name = "txtPreuzeo";
            this.txtPreuzeo.Size = new System.Drawing.Size(66, 20);
            this.txtPreuzeo.TabIndex = 567;
            this.txtPreuzeo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPreuzeo_KeyDown);
            // 
            // frmRobuPreuzeo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(647, 628);
            this.Controls.Add(this.txtNazivPreuzeo);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPreuzeo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tdDoDatuma);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tdOdDatuma);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnIspis);
            this.Controls.Add(this.btnTrazi);
            this.Controls.Add(this.txtNaziv);
            this.Controls.Add(this.btnPartner);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSifra);
            this.MinimumSize = new System.Drawing.Size(663, 666);
            this.Name = "frmRobuPreuzeo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Robu preuzeo";
            this.Load += new System.EventHandler(this.frmRobuPreuzeo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPartner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn preuzeo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dokumenat;
        private System.Windows.Forms.DataGridViewTextBoxColumn vpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn porez;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos;
        private System.Windows.Forms.PictureBox btnPartner;
        private System.Windows.Forms.TextBox txtNaziv;
        private System.Windows.Forms.Button btnTrazi;
        private System.Windows.Forms.Button btnIspis;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DateTimePicker tdOdDatuma;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker tdDoDatuma;
        private System.Windows.Forms.TextBox txtNazivPreuzeo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPreuzeo;
    }
}