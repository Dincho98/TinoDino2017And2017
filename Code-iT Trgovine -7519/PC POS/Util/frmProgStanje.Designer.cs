namespace PCPOS.Util
{
    partial class frmProgStanje
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
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPoravnaj = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.chbSkl = new System.Windows.Forms.CheckBox();
            this.cbSkl = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(12, 24);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(291, 20);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Lozinka za pristup:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPoravnaj);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.chbSkl);
            this.groupBox1.Controls.Add(this.cbSkl);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(291, 116);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Poravnavanje skladišta";
            this.groupBox1.Visible = false;
            // 
            // btnPoravnaj
            // 
            this.btnPoravnaj.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPoravnaj.Location = new System.Drawing.Point(9, 74);
            this.btnPoravnaj.Name = "btnPoravnaj";
            this.btnPoravnaj.Size = new System.Drawing.Size(272, 34);
            this.btnPoravnaj.TabIndex = 72;
            this.btnPoravnaj.Text = "Postavi";
            this.btnPoravnaj.UseVisualStyleBackColor = true;
            this.btnPoravnaj.Click += new System.EventHandler(this.btnPoravnaj_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(7, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 71;
            this.label2.Text = "Odabir skladišta";
            this.label2.Visible = false;
            // 
            // chbSkl
            // 
            this.chbSkl.AutoSize = true;
            this.chbSkl.Checked = true;
            this.chbSkl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbSkl.Location = new System.Drawing.Point(266, 45);
            this.chbSkl.Name = "chbSkl";
            this.chbSkl.Size = new System.Drawing.Size(15, 14);
            this.chbSkl.TabIndex = 70;
            this.chbSkl.UseVisualStyleBackColor = true;
            // 
            // cbSkl
            // 
            this.cbSkl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbSkl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbSkl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbSkl.FormattingEnabled = true;
            this.cbSkl.Location = new System.Drawing.Point(10, 35);
            this.cbSkl.Name = "cbSkl";
            this.cbSkl.Size = new System.Drawing.Size(253, 24);
            this.cbSkl.TabIndex = 69;
            this.cbSkl.Visible = false;
            // 
            // frmProgStanje
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(109)))), ((int)(((byte)(135)))));
            this.ClientSize = new System.Drawing.Size(315, 179);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmProgStanje";
            this.Text = "Poravnavanje";
            this.Load += new System.EventHandler(this.frmProgStanje_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbSkl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chbSkl;
        private System.Windows.Forms.Button btnPoravnaj;
    }
}