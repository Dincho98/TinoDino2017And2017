namespace PCPOS
{
    partial class frmSviNormativi
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chbIzradio = new System.Windows.Forms.CheckBox();
            this.cbArtikl = new System.Windows.Forms.TextBox();
            this.cbIzradio = new System.Windows.Forms.ComboBox();
            this.txtBroj = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ch = new System.Windows.Forms.Label();
            this.lbIzradio = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.btnUrediSifru = new System.Windows.Forms.Button();
            this.btnSveFakture = new System.Windows.Forms.Button();
            this.btnIspisBezCijena = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.chbIzradio);
            this.groupBox1.Controls.Add(this.cbArtikl);
            this.groupBox1.Controls.Add(this.cbIzradio);
            this.groupBox1.Controls.Add(this.txtBroj);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ch);
            this.groupBox1.Controls.Add(this.lbIzradio);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox1.Location = new System.Drawing.Point(9, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(937, 117);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pretraživanje ponuda";
            // 
            // chbIzradio
            // 
            this.chbIzradio.AutoSize = true;
            this.chbIzradio.Location = new System.Drawing.Point(293, 81);
            this.chbIzradio.Name = "chbIzradio";
            this.chbIzradio.Size = new System.Drawing.Size(15, 14);
            this.chbIzradio.TabIndex = 47;
            this.chbIzradio.UseVisualStyleBackColor = true;
            // 
            // cbArtikl
            // 
            this.cbArtikl.Location = new System.Drawing.Point(122, 50);
            this.cbArtikl.Name = "cbArtikl";
            this.cbArtikl.Size = new System.Drawing.Size(168, 23);
            this.cbArtikl.TabIndex = 31;
            // 
            // cbIzradio
            // 
            this.cbIzradio.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbIzradio.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbIzradio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIzradio.FormattingEnabled = true;
            this.cbIzradio.Location = new System.Drawing.Point(122, 75);
            this.cbIzradio.Name = "cbIzradio";
            this.cbIzradio.Size = new System.Drawing.Size(168, 24);
            this.cbIzradio.TabIndex = 42;
            // 
            // txtBroj
            // 
            this.txtBroj.Location = new System.Drawing.Point(122, 25);
            this.txtBroj.Name = "txtBroj";
            this.txtBroj.Size = new System.Drawing.Size(168, 23);
            this.txtBroj.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 17);
            this.label1.TabIndex = 33;
            this.label1.Text = "Broj normativa:";
            // 
            // ch
            // 
            this.ch.AutoSize = true;
            this.ch.Location = new System.Drawing.Point(15, 56);
            this.ch.Name = "ch";
            this.ch.Size = new System.Drawing.Size(83, 17);
            this.ch.TabIndex = 37;
            this.ch.Text = "Šifra artikla:";
            // 
            // lbIzradio
            // 
            this.lbIzradio.AutoSize = true;
            this.lbIzradio.Location = new System.Drawing.Point(15, 82);
            this.lbIzradio.Name = "lbIzradio";
            this.lbIzradio.Size = new System.Drawing.Size(81, 17);
            this.lbIzradio.TabIndex = 36;
            this.lbIzradio.Text = "Izradio zap:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::PCPOS.Properties.Resources._1059;
            this.pictureBox1.Location = new System.Drawing.Point(325, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(71, 65);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgv.Location = new System.Drawing.Point(9, 190);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(937, 333);
            this.dgv.TabIndex = 12;
            this.dgv.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_CellMouseDoubleClick);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.Image = global::PCPOS.Properties.Resources.Actions_application_exit_icon;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(816, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 40);
            this.button1.TabIndex = 24;
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
            this.btnUrediSifru.Location = new System.Drawing.Point(388, 12);
            this.btnUrediSifru.Name = "btnUrediSifru";
            this.btnUrediSifru.Size = new System.Drawing.Size(134, 40);
            this.btnUrediSifru.TabIndex = 23;
            this.btnUrediSifru.Text = "Uredi normativ";
            this.btnUrediSifru.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUrediSifru.UseVisualStyleBackColor = true;
            this.btnUrediSifru.Click += new System.EventHandler(this.btnUrediSifru_Click);
            // 
            // btnSveFakture
            // 
            this.btnSveFakture.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSveFakture.Image = global::PCPOS.Properties.Resources.print_printer;
            this.btnSveFakture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSveFakture.Location = new System.Drawing.Point(12, 12);
            this.btnSveFakture.Name = "btnSveFakture";
            this.btnSveFakture.Size = new System.Drawing.Size(146, 40);
            this.btnSveFakture.TabIndex = 22;
            this.btnSveFakture.Text = "Ispis normativa";
            this.btnSveFakture.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSveFakture.UseVisualStyleBackColor = true;
            this.btnSveFakture.Click += new System.EventHandler(this.btnSveFakture_Click);
            // 
            // btnIspisBezCijena
            // 
            this.btnIspisBezCijena.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnIspisBezCijena.Image = global::PCPOS.Properties.Resources.print_printer;
            this.btnIspisBezCijena.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIspisBezCijena.Location = new System.Drawing.Point(164, 12);
            this.btnIspisBezCijena.Name = "btnIspisBezCijena";
            this.btnIspisBezCijena.Size = new System.Drawing.Size(218, 40);
            this.btnIspisBezCijena.TabIndex = 25;
            this.btnIspisBezCijena.Text = "Ispis normativa bez cijena";
            this.btnIspisBezCijena.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIspisBezCijena.UseVisualStyleBackColor = true;
            this.btnIspisBezCijena.Click += new System.EventHandler(this.btnIspisBezCijena_Click);
            // 
            // frmSviNormativi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 535);
            this.Controls.Add(this.btnIspisBezCijena);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnUrediSifru);
            this.Controls.Add(this.btnSveFakture);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgv);
            this.Name = "frmSviNormativi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Svi normativi";
            this.Load += new System.EventHandler(this.frmSviNormativi_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chbIzradio;
        public System.Windows.Forms.TextBox cbArtikl;
        private System.Windows.Forms.ComboBox cbIzradio;
        public System.Windows.Forms.TextBox txtBroj;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ch;
        private System.Windows.Forms.Label lbIzradio;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnUrediSifru;
        private System.Windows.Forms.Button btnSveFakture;
        private System.Windows.Forms.Button btnIspisBezCijena;
    }
}