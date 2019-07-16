namespace PCPOS {
    partial class frmAutomatskaUskladaSve {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent () {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtpDatumOd = new System.Windows.Forms.DateTimePicker();
            this.btnIzlaz = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.btnIspis = new System.Windows.Forms.Button();
            this.btnUcitaj = new System.Windows.Forms.Button();
            this.cmbSkladiste = new System.Windows.Forms.ComboBox();
            this.dgwUskladaItems = new System.Windows.Forms.DataGridView();
            this.cbSkladiste = new System.Windows.Forms.CheckBox();
            this.cbDatumOd = new System.Windows.Forms.CheckBox();
            this.cbDatumDo = new System.Windows.Forms.CheckBox();
            this.dtpDatumDo = new System.Windows.Forms.DateTimePicker();
            this.rbVpc = new System.Windows.Forms.RadioButton();
            this.rbMpc = new System.Windows.Forms.RadioButton();
            this.pnlVpcMpc = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgwUskladaItems)).BeginInit();
            this.pnlVpcMpc.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpDatumOd
            // 
            this.dtpDatumOd.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dtpDatumOd.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.15F);
            this.dtpDatumOd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatumOd.Location = new System.Drawing.Point(141, 50);
            this.dtpDatumOd.Name = "dtpDatumOd";
            this.dtpDatumOd.Size = new System.Drawing.Size(248, 30);
            this.dtpDatumOd.TabIndex = 14;
            // 
            // btnIzlaz
            // 
            this.btnIzlaz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIzlaz.BackColor = System.Drawing.SystemColors.Control;
            this.btnIzlaz.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnIzlaz.FlatAppearance.BorderSize = 0;
            this.btnIzlaz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIzlaz.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnIzlaz.Location = new System.Drawing.Point(676, 12);
            this.btnIzlaz.Name = "btnIzlaz";
            this.btnIzlaz.Size = new System.Drawing.Size(121, 32);
            this.btnIzlaz.TabIndex = 13;
            this.btnIzlaz.Text = "Izlaz";
            this.btnIzlaz.UseVisualStyleBackColor = false;
            this.btnIzlaz.Click += new System.EventHandler(this.btnIzlaz_Click);
            // 
            // btnOdustani
            // 
            this.btnOdustani.BackColor = System.Drawing.SystemColors.Control;
            this.btnOdustani.FlatAppearance.BorderSize = 0;
            this.btnOdustani.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOdustani.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnOdustani.Location = new System.Drawing.Point(266, 12);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(121, 32);
            this.btnOdustani.TabIndex = 12;
            this.btnOdustani.Text = "Odustani";
            this.btnOdustani.UseVisualStyleBackColor = false;
            this.btnOdustani.Visible = false;
            // 
            // btnIspis
            // 
            this.btnIspis.BackColor = System.Drawing.SystemColors.Control;
            this.btnIspis.FlatAppearance.BorderSize = 0;
            this.btnIspis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIspis.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnIspis.Location = new System.Drawing.Point(139, 12);
            this.btnIspis.Name = "btnIspis";
            this.btnIspis.Size = new System.Drawing.Size(121, 32);
            this.btnIspis.TabIndex = 11;
            this.btnIspis.Text = "Ispis";
            this.btnIspis.UseVisualStyleBackColor = false;
            this.btnIspis.Click += new System.EventHandler(this.btnIspis_Click);
            // 
            // btnUcitaj
            // 
            this.btnUcitaj.BackColor = System.Drawing.SystemColors.Control;
            this.btnUcitaj.FlatAppearance.BorderSize = 0;
            this.btnUcitaj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUcitaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnUcitaj.Location = new System.Drawing.Point(12, 12);
            this.btnUcitaj.Name = "btnUcitaj";
            this.btnUcitaj.Size = new System.Drawing.Size(121, 32);
            this.btnUcitaj.TabIndex = 10;
            this.btnUcitaj.Text = "Učitaj";
            this.btnUcitaj.UseVisualStyleBackColor = false;
            this.btnUcitaj.Click += new System.EventHandler(this.btnUcitaj_Click);
            // 
            // cmbSkladiste
            // 
            this.cmbSkladiste.BackColor = System.Drawing.SystemColors.Menu;
            this.cmbSkladiste.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSkladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSkladiste.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSkladiste.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.cmbSkladiste.FormattingEnabled = true;
            this.cmbSkladiste.Location = new System.Drawing.Point(577, 50);
            this.cmbSkladiste.Name = "cmbSkladiste";
            this.cmbSkladiste.Size = new System.Drawing.Size(220, 30);
            this.cmbSkladiste.TabIndex = 9;
            this.cmbSkladiste.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbSkladiste_DrawItem);
            // 
            // dgwUskladaItems
            // 
            this.dgwUskladaItems.AllowUserToAddRows = false;
            this.dgwUskladaItems.AllowUserToDeleteRows = false;
            this.dgwUskladaItems.AllowUserToResizeRows = false;
            this.dgwUskladaItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgwUskladaItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgwUskladaItems.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgwUskladaItems.BackgroundColor = System.Drawing.Color.SlateGray;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgwUskladaItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgwUskladaItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgwUskladaItems.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgwUskladaItems.Location = new System.Drawing.Point(12, 122);
            this.dgwUskladaItems.Name = "dgwUskladaItems";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgwUskladaItems.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgwUskladaItems.RowHeadersWidth = 21;
            this.dgwUskladaItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwUskladaItems.Size = new System.Drawing.Size(785, 327);
            this.dgwUskladaItems.TabIndex = 15;
            // 
            // cbSkladiste
            // 
            this.cbSkladiste.AutoSize = true;
            this.cbSkladiste.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbSkladiste.FlatAppearance.BorderSize = 0;
            this.cbSkladiste.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbSkladiste.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.cbSkladiste.Location = new System.Drawing.Point(455, 50);
            this.cbSkladiste.Name = "cbSkladiste";
            this.cbSkladiste.Size = new System.Drawing.Size(117, 30);
            this.cbSkladiste.TabIndex = 16;
            this.cbSkladiste.Text = "Skladište";
            this.cbSkladiste.UseVisualStyleBackColor = true;
            this.cbSkladiste.CheckedChanged += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // cbDatumOd
            // 
            this.cbDatumOd.AutoSize = true;
            this.cbDatumOd.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbDatumOd.FlatAppearance.BorderSize = 0;
            this.cbDatumOd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbDatumOd.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.cbDatumOd.Location = new System.Drawing.Point(12, 50);
            this.cbDatumOd.Name = "cbDatumOd";
            this.cbDatumOd.Size = new System.Drawing.Size(123, 30);
            this.cbDatumOd.TabIndex = 17;
            this.cbDatumOd.Text = "Datum od";
            this.cbDatumOd.UseVisualStyleBackColor = true;
            this.cbDatumOd.CheckedChanged += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // cbDatumDo
            // 
            this.cbDatumDo.AutoSize = true;
            this.cbDatumDo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbDatumDo.FlatAppearance.BorderSize = 0;
            this.cbDatumDo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbDatumDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.cbDatumDo.Location = new System.Drawing.Point(12, 86);
            this.cbDatumDo.Name = "cbDatumDo";
            this.cbDatumDo.Size = new System.Drawing.Size(123, 30);
            this.cbDatumDo.TabIndex = 18;
            this.cbDatumDo.Text = "Datum do";
            this.cbDatumDo.UseVisualStyleBackColor = true;
            this.cbDatumDo.CheckedChanged += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // dtpDatumDo
            // 
            this.dtpDatumDo.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dtpDatumDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.15F);
            this.dtpDatumDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatumDo.Location = new System.Drawing.Point(141, 86);
            this.dtpDatumDo.Name = "dtpDatumDo";
            this.dtpDatumDo.Size = new System.Drawing.Size(248, 30);
            this.dtpDatumDo.TabIndex = 19;
            // 
            // rbVpc
            // 
            this.rbVpc.AutoSize = true;
            this.rbVpc.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbVpc.FlatAppearance.BorderSize = 0;
            this.rbVpc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbVpc.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.rbVpc.Location = new System.Drawing.Point(0, 0);
            this.rbVpc.Name = "rbVpc";
            this.rbVpc.Size = new System.Drawing.Size(75, 30);
            this.rbVpc.TabIndex = 20;
            this.rbVpc.Text = "VPC";
            this.rbVpc.UseVisualStyleBackColor = true;
            // 
            // rbMpc
            // 
            this.rbMpc.AutoSize = true;
            this.rbMpc.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbMpc.Checked = true;
            this.rbMpc.FlatAppearance.BorderSize = 0;
            this.rbMpc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbMpc.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.rbMpc.Location = new System.Drawing.Point(141, 0);
            this.rbMpc.Name = "rbMpc";
            this.rbMpc.Size = new System.Drawing.Size(78, 30);
            this.rbMpc.TabIndex = 21;
            this.rbMpc.TabStop = true;
            this.rbMpc.Text = "MPC";
            this.rbMpc.UseVisualStyleBackColor = true;
            // 
            // pnlVpcMpc
            // 
            this.pnlVpcMpc.Controls.Add(this.rbVpc);
            this.pnlVpcMpc.Controls.Add(this.rbMpc);
            this.pnlVpcMpc.Location = new System.Drawing.Point(577, 86);
            this.pnlVpcMpc.Name = "pnlVpcMpc";
            this.pnlVpcMpc.Size = new System.Drawing.Size(220, 30);
            this.pnlVpcMpc.TabIndex = 22;
            // 
            // frmAutomatskaUskladaSve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.CancelButton = this.btnIzlaz;
            this.ClientSize = new System.Drawing.Size(809, 501);
            this.Controls.Add(this.pnlVpcMpc);
            this.Controls.Add(this.dtpDatumDo);
            this.Controls.Add(this.cbDatumDo);
            this.Controls.Add(this.cbDatumOd);
            this.Controls.Add(this.cbSkladiste);
            this.Controls.Add(this.dgwUskladaItems);
            this.Controls.Add(this.dtpDatumOd);
            this.Controls.Add(this.btnIzlaz);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnIspis);
            this.Controls.Add(this.btnUcitaj);
            this.Controls.Add(this.cmbSkladiste);
            this.MinimumSize = new System.Drawing.Size(825, 539);
            this.Name = "frmAutomatskaUskladaSve";
            this.Text = "Sve automatske usklade";
            this.Load += new System.EventHandler(this.frmAutomatskaUskladaSve_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwUskladaItems)).EndInit();
            this.pnlVpcMpc.ResumeLayout(false);
            this.pnlVpcMpc.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpDatumOd;
        private System.Windows.Forms.Button btnIzlaz;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Button btnIspis;
        private System.Windows.Forms.Button btnUcitaj;
        private System.Windows.Forms.ComboBox cmbSkladiste;
        private System.Windows.Forms.DataGridView dgwUskladaItems;
        private System.Windows.Forms.CheckBox cbSkladiste;
        private System.Windows.Forms.CheckBox cbDatumOd;
        private System.Windows.Forms.CheckBox cbDatumDo;
        private System.Windows.Forms.DateTimePicker dtpDatumDo;
        private System.Windows.Forms.RadioButton rbVpc;
        private System.Windows.Forms.RadioButton rbMpc;
        private System.Windows.Forms.Panel pnlVpcMpc;
    }
}