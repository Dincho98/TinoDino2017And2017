namespace PCPOS.Robno
{
	partial class frmStanjePoZadnjojInventuri
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStanjePoZadnjojInventuri));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnZadnjaInv = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.cbSkladiste = new System.Windows.Forms.ComboBox();
            this.dgw = new System.Windows.Forms.DataGridView();
            this.backgroundWorkerSetKolicina = new System.ComponentModel.BackgroundWorker();
            this.lblDate = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPostaviCijene = new System.Windows.Forms.Button();
            this.nuGodina = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDanInventure = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuGodina)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnZadnjaInv
            // 
            this.btnZadnjaInv.BackColor = System.Drawing.Color.Transparent;
            this.btnZadnjaInv.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnZadnjaInv.BackgroundImage")));
            this.btnZadnjaInv.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnZadnjaInv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnZadnjaInv.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnZadnjaInv.FlatAppearance.BorderSize = 0;
            this.btnZadnjaInv.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnZadnjaInv.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnZadnjaInv.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnZadnjaInv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZadnjaInv.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.btnZadnjaInv.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnZadnjaInv.Location = new System.Drawing.Point(222, 17);
            this.btnZadnjaInv.Name = "btnZadnjaInv";
            this.btnZadnjaInv.Size = new System.Drawing.Size(164, 29);
            this.btnZadnjaInv.TabIndex = 77;
            this.btnZadnjaInv.TabStop = false;
            this.btnZadnjaInv.Text = "Učitaj zadnju inventuru";
            this.btnZadnjaInv.UseVisualStyleBackColor = false;
            this.btnZadnjaInv.Click += new System.EventHandler(this.btnZadnjaInv_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label17.Location = new System.Drawing.Point(9, 3);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(69, 17);
            this.label17.TabIndex = 76;
            this.label17.Text = "Skladište:";
            // 
            // cbSkladiste
            // 
            this.cbSkladiste.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbSkladiste.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbSkladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkladiste.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbSkladiste.FormattingEnabled = true;
            this.cbSkladiste.Location = new System.Drawing.Point(12, 20);
            this.cbSkladiste.Name = "cbSkladiste";
            this.cbSkladiste.Size = new System.Drawing.Size(207, 24);
            this.cbSkladiste.TabIndex = 75;
            // 
            // dgw
            // 
            this.dgw.AllowUserToAddRows = false;
            this.dgw.AllowUserToDeleteRows = false;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.AliceBlue;
            this.dgw.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            this.dgw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgw.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgw.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgw.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgw.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgw.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgw.GridColor = System.Drawing.Color.Gainsboro;
            this.dgw.Location = new System.Drawing.Point(12, 53);
            this.dgw.MultiSelect = false;
            this.dgw.Name = "dgw";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgw.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dgw.RowHeadersWidth = 30;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dgw.RowsDefaultCellStyle = dataGridViewCellStyle15;
            this.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgw.Size = new System.Drawing.Size(874, 456);
            this.dgw.TabIndex = 74;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(392, 25);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(0, 13);
            this.lblDate.TabIndex = 79;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(12, 514);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(408, 100);
            this.groupBox2.TabIndex = 81;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Postavi početno stanje za novu godinu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(369, 39);
            this.label2.TabIndex = 80;
            this.label2.Text = "Ovu metodu pokrečete SAMO za inventuru koja je namijenjena za početak\r\ngodine! Ak" +
    "o imate robno sa skladištima obavezni ste napraviti početno stanje\r\nna početku n" +
    "ove godine.";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.Maroon;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button1.Location = new System.Drawing.Point(9, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(392, 33);
            this.button1.TabIndex = 78;
            this.button1.TabStop = false;
            this.button1.Text = "Postavi početno stanje po inventuri za ovu godinu.";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnPostaviCijene);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(636, 515);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 100);
            this.groupBox1.TabIndex = 82;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Naknadno cijene";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 26);
            this.label1.TabIndex = 80;
            this.label1.Text = "Ako ste mijenjeli u dokumentima \r\ncijene ili količine koristite ovu funkciju.";
            // 
            // btnPostaviCijene
            // 
            this.btnPostaviCijene.BackColor = System.Drawing.Color.Transparent;
            this.btnPostaviCijene.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPostaviCijene.BackgroundImage")));
            this.btnPostaviCijene.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPostaviCijene.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPostaviCijene.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnPostaviCijene.FlatAppearance.BorderSize = 0;
            this.btnPostaviCijene.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnPostaviCijene.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPostaviCijene.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPostaviCijene.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPostaviCijene.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnPostaviCijene.ForeColor = System.Drawing.Color.Maroon;
            this.btnPostaviCijene.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnPostaviCijene.Location = new System.Drawing.Point(9, 62);
            this.btnPostaviCijene.Name = "btnPostaviCijene";
            this.btnPostaviCijene.Size = new System.Drawing.Size(171, 33);
            this.btnPostaviCijene.TabIndex = 78;
            this.btnPostaviCijene.TabStop = false;
            this.btnPostaviCijene.Text = "Postavi naknadno cijene";
            this.btnPostaviCijene.UseVisualStyleBackColor = false;
            this.btnPostaviCijene.Click += new System.EventHandler(this.btnPostaviCijene_Click);
            // 
            // nuGodina
            // 
            this.nuGodina.Location = new System.Drawing.Point(795, 20);
            this.nuGodina.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nuGodina.Minimum = new decimal(new int[] {
            1990,
            0,
            0,
            0});
            this.nuGodina.Name = "nuGodina";
            this.nuGodina.Size = new System.Drawing.Size(91, 20);
            this.nuGodina.TabIndex = 83;
            this.nuGodina.Value = new decimal(new int[] {
            1990,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(748, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 84;
            this.label3.Text = "Godina";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.btnDanInventure);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(426, 514);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(204, 100);
            this.groupBox3.TabIndex = 85;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Početno stanje na dan inventure";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(10, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(192, 38);
            this.label4.TabIndex = 0;
            this.label4.Text = "Ova metoda uzima datum inventure kao početno stanje.";
            // 
            // btnDanInventure
            // 
            this.btnDanInventure.BackColor = System.Drawing.Color.Gainsboro;
            this.btnDanInventure.FlatAppearance.BorderSize = 0;
            this.btnDanInventure.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDanInventure.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnDanInventure.ForeColor = System.Drawing.Color.Maroon;
            this.btnDanInventure.Location = new System.Drawing.Point(10, 63);
            this.btnDanInventure.Name = "btnDanInventure";
            this.btnDanInventure.Size = new System.Drawing.Size(185, 31);
            this.btnDanInventure.TabIndex = 1;
            this.btnDanInventure.Text = "Postavi";
            this.btnDanInventure.UseVisualStyleBackColor = false;
            this.btnDanInventure.Click += new System.EventHandler(this.btnDanInventure_Click);
            // 
            // frmStanjePoZadnjojInventuri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(109)))), ((int)(((byte)(135)))));
            this.ClientSize = new System.Drawing.Size(898, 626);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nuGodina);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.btnZadnjaInv);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.cbSkladiste);
            this.Controls.Add(this.dgw);
            this.Name = "frmStanjePoZadnjojInventuri";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stanje po inventuri";
            this.Load += new System.EventHandler(this.frmStanjePoZadnjojInventuri_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuGodina)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Button btnZadnjaInv;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.ComboBox cbSkladiste;
		private System.Windows.Forms.DataGridView dgw;
		private System.ComponentModel.BackgroundWorker backgroundWorkerSetKolicina;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPostaviCijene;
        private System.Windows.Forms.NumericUpDown nuGodina;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnDanInventure;
        private System.Windows.Forms.Label label4;
    }
}