namespace PCPOS.Sinkronizacija
{
    partial class frmIzvozIzPrograma
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIzvozIzPrograma));
            this.dgv = new System.Windows.Forms.DataGridView();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDodaj = new System.Windows.Forms.Button();
            this.btnTraziPartnera = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNazivPartnera = new System.Windows.Forms.TextBox();
            this.txtSifraPartnera = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbSkladiste = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnKreiraj = new System.Windows.Forms.Button();
            this.btnKreirajXMLiSpremi = new System.Windows.Forms.Button();
            this.btnSpremiSQL = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMjeseci = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.bgWorker1 = new System.ComponentModel.BackgroundWorker();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnTraziPartnera)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sifra,
            this.partner,
            this.id});
            this.dgv.Location = new System.Drawing.Point(6, 117);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 18;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(419, 270);
            this.dgv.TabIndex = 23;
            // 
            // sifra
            // 
            this.sifra.HeaderText = "Šifra";
            this.sifra.Name = "sifra";
            this.sifra.ReadOnly = true;
            // 
            // partner
            // 
            this.partner.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.partner.HeaderText = "Partner";
            this.partner.Name = "partner";
            this.partner.ReadOnly = true;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btnDodaj);
            this.groupBox1.Controls.Add(this.btnTraziPartnera);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtNazivPartnera);
            this.groupBox1.Controls.Add(this.txtSifraPartnera);
            this.groupBox1.Controls.Add(this.dgv);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(431, 393);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Izvoz aktikla prema dobavljačima";
            // 
            // btnDodaj
            // 
            this.btnDodaj.BackColor = System.Drawing.Color.Transparent;
            this.btnDodaj.BackgroundImage = global::PCPOS.Properties.Resources.Untitled_1;
            this.btnDodaj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDodaj.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDodaj.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnDodaj.FlatAppearance.BorderSize = 0;
            this.btnDodaj.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnDodaj.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDodaj.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDodaj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDodaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold);
            this.btnDodaj.ForeColor = System.Drawing.Color.White;
            this.btnDodaj.Location = new System.Drawing.Point(365, 56);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(59, 34);
            this.btnDodaj.TabIndex = 71;
            this.btnDodaj.Text = "Dodaj";
            this.toolTip1.SetToolTip(this.btnDodaj, "Dodaj ovog partnera na popis");
            this.btnDodaj.UseVisualStyleBackColor = false;
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);
            // 
            // btnTraziPartnera
            // 
            this.btnTraziPartnera.BackColor = System.Drawing.Color.Transparent;
            this.btnTraziPartnera.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTraziPartnera.Image = ((System.Drawing.Image)(resources.GetObject("btnTraziPartnera.Image")));
            this.btnTraziPartnera.Location = new System.Drawing.Point(332, 59);
            this.btnTraziPartnera.Name = "btnTraziPartnera";
            this.btnTraziPartnera.Size = new System.Drawing.Size(28, 26);
            this.btnTraziPartnera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnTraziPartnera.TabIndex = 39;
            this.btnTraziPartnera.TabStop = false;
            this.btnTraziPartnera.Click += new System.EventHandler(this.btnTraziPartnera_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label2.Location = new System.Drawing.Point(97, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 26;
            this.label2.Text = "Naziv";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label1.Location = new System.Drawing.Point(7, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 15);
            this.label1.TabIndex = 26;
            this.label1.Text = "Šifra";
            // 
            // txtNazivPartnera
            // 
            this.txtNazivPartnera.Location = new System.Drawing.Point(100, 60);
            this.txtNazivPartnera.Name = "txtNazivPartnera";
            this.txtNazivPartnera.Size = new System.Drawing.Size(228, 23);
            this.txtNazivPartnera.TabIndex = 25;
            // 
            // txtSifraPartnera
            // 
            this.txtSifraPartnera.Location = new System.Drawing.Point(7, 60);
            this.txtSifraPartnera.Name = "txtSifraPartnera";
            this.txtSifraPartnera.Size = new System.Drawing.Size(90, 23);
            this.txtSifraPartnera.TabIndex = 25;
            this.txtSifraPartnera.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraPartnera_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbSkladiste);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox2.Location = new System.Drawing.Point(454, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(317, 98);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Odabir skladišta";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label3.Location = new System.Drawing.Point(12, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(279, 15);
            this.label3.TabIndex = 27;
            this.label3.Text = "Odaberite skladište sa kojega želite napraviti izvoz";
            // 
            // cbSkladiste
            // 
            this.cbSkladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkladiste.FormattingEnabled = true;
            this.cbSkladiste.Location = new System.Drawing.Point(14, 64);
            this.cbSkladiste.Name = "cbSkladiste";
            this.cbSkladiste.Size = new System.Drawing.Size(294, 24);
            this.cbSkladiste.TabIndex = 0;
            // 
            // btnKreiraj
            // 
            this.btnKreiraj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKreiraj.BackColor = System.Drawing.Color.Transparent;
            this.btnKreiraj.BackgroundImage = global::PCPOS.Properties.Resources.Untitled_1;
            this.btnKreiraj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnKreiraj.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKreiraj.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnKreiraj.FlatAppearance.BorderSize = 0;
            this.btnKreiraj.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnKreiraj.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnKreiraj.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnKreiraj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKreiraj.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold);
            this.btnKreiraj.ForeColor = System.Drawing.Color.White;
            this.btnKreiraj.Location = new System.Drawing.Point(644, 358);
            this.btnKreiraj.Name = "btnKreiraj";
            this.btnKreiraj.Size = new System.Drawing.Size(127, 48);
            this.btnKreiraj.TabIndex = 72;
            this.btnKreiraj.Text = "Kreiraj xml";
            this.toolTip1.SetToolTip(this.btnKreiraj, "Kreiraj samo XML datoteku");
            this.btnKreiraj.UseVisualStyleBackColor = false;
            this.btnKreiraj.Click += new System.EventHandler(this.btnKreiraj_Click);
            // 
            // btnKreirajXMLiSpremi
            // 
            this.btnKreirajXMLiSpremi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKreirajXMLiSpremi.BackColor = System.Drawing.Color.Transparent;
            this.btnKreirajXMLiSpremi.BackgroundImage = global::PCPOS.Properties.Resources.Untitled_1;
            this.btnKreirajXMLiSpremi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnKreirajXMLiSpremi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKreirajXMLiSpremi.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnKreirajXMLiSpremi.FlatAppearance.BorderSize = 0;
            this.btnKreirajXMLiSpremi.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnKreirajXMLiSpremi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnKreirajXMLiSpremi.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnKreirajXMLiSpremi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKreirajXMLiSpremi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold);
            this.btnKreirajXMLiSpremi.ForeColor = System.Drawing.Color.White;
            this.btnKreirajXMLiSpremi.Location = new System.Drawing.Point(469, 358);
            this.btnKreirajXMLiSpremi.Name = "btnKreirajXMLiSpremi";
            this.btnKreirajXMLiSpremi.Size = new System.Drawing.Size(169, 48);
            this.btnKreirajXMLiSpremi.TabIndex = 73;
            this.btnKreirajXMLiSpremi.Text = "Kreiraj xml i spremi";
            this.toolTip1.SetToolTip(this.btnKreirajXMLiSpremi, "Kreiraj XML datoteku i odaberite mjesto spremanja");
            this.btnKreirajXMLiSpremi.UseVisualStyleBackColor = false;
            this.btnKreirajXMLiSpremi.Click += new System.EventHandler(this.btnKreirajXMLiSpremi_Click);
            // 
            // btnSpremiSQL
            // 
            this.btnSpremiSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSpremiSQL.BackColor = System.Drawing.Color.Transparent;
            this.btnSpremiSQL.BackgroundImage = global::PCPOS.Properties.Resources.Untitled_1;
            this.btnSpremiSQL.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSpremiSQL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSpremiSQL.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnSpremiSQL.FlatAppearance.BorderSize = 0;
            this.btnSpremiSQL.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnSpremiSQL.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSpremiSQL.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSpremiSQL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpremiSQL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold);
            this.btnSpremiSQL.ForeColor = System.Drawing.Color.White;
            this.btnSpremiSQL.Location = new System.Drawing.Point(644, 308);
            this.btnSpremiSQL.Name = "btnSpremiSQL";
            this.btnSpremiSQL.Size = new System.Drawing.Size(127, 48);
            this.btnSpremiSQL.TabIndex = 75;
            this.btnSpremiSQL.Text = "Spremi u SQL";
            this.toolTip1.SetToolTip(this.btnSpremiSQL, "Kreiraj samo XML datoteku");
            this.btnSpremiSQL.UseVisualStyleBackColor = false;
            this.btnSpremiSQL.Click += new System.EventHandler(this.btnSpremiSQL_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtMjeseci);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox3.Location = new System.Drawing.Point(454, 117);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(317, 102);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Opcionalni filtar";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label5.Location = new System.Drawing.Point(109, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 15);
            this.label5.TabIndex = 29;
            this.label5.Text = "mjeseci.";
            // 
            // txtMjeseci
            // 
            this.txtMjeseci.Location = new System.Drawing.Point(15, 64);
            this.txtMjeseci.Name = "txtMjeseci";
            this.txtMjeseci.Size = new System.Drawing.Size(90, 23);
            this.txtMjeseci.TabIndex = 28;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label4.Location = new System.Drawing.Point(12, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(290, 15);
            this.label4.TabIndex = 27;
            this.label4.Text = "Ne uzimaj u obzir artikle koje se ne koriste duže od :";
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(454, 228);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(162, 17);
            this.checkBox1.TabIndex = 74;
            this.checkBox1.Text = "Slike se zovu isto kao i šifre?";
            this.checkBox1.UseVisualStyleBackColor = false;
            // 
            // frmIzvozIzPrograma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
            this.ClientSize = new System.Drawing.Size(783, 418);
            this.Controls.Add(this.btnSpremiSQL);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btnKreirajXMLiSpremi);
            this.Controls.Add(this.btnKreiraj);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmIzvozIzPrograma";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Izvoz iz programa u xml";
            this.Load += new System.EventHandler(this.frmIzvozIzPrograma_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnTraziPartnera)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn partner;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNazivPartnera;
        private System.Windows.Forms.TextBox txtSifraPartnera;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox btnTraziPartnera;
        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbSkladiste;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMjeseci;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnKreiraj;
        private System.Windows.Forms.Button btnKreirajXMLiSpremi;
        private System.ComponentModel.BackgroundWorker bgWorker1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnSpremiSQL;
    }
}