namespace PCPOS.Sifarnik
{
    partial class frmBrziUpisKlijenta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBrziUpisKlijenta));
            this.txtImeKlijenta = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAdresa = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbGrad = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.txtOIB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtImeKlijenta
            // 
            this.txtImeKlijenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtImeKlijenta.Location = new System.Drawing.Point(12, 33);
            this.txtImeKlijenta.Name = "txtImeKlijenta";
            this.txtImeKlijenta.Size = new System.Drawing.Size(249, 23);
            this.txtImeKlijenta.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(9, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Ime klijenta";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(8, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Adresa";
            // 
            // txtAdresa
            // 
            this.txtAdresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtAdresa.Location = new System.Drawing.Point(12, 119);
            this.txtAdresa.Name = "txtAdresa";
            this.txtAdresa.Size = new System.Drawing.Size(249, 23);
            this.txtAdresa.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(235, 161);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(27, 27);
            this.button2.TabIndex = 5;
            this.button2.Text = "+";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label5.Location = new System.Drawing.Point(-68, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 17);
            this.label5.TabIndex = 111;
            this.label5.Text = "Grad:";
            // 
            // cbGrad
            // 
            this.cbGrad.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbGrad.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbGrad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGrad.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cbGrad.FormattingEnabled = true;
            this.cbGrad.Location = new System.Drawing.Point(12, 162);
            this.cbGrad.Name = "cbGrad";
            this.cbGrad.Size = new System.Drawing.Size(223, 24);
            this.cbGrad.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(10, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Grad";
            // 
            // btnSpremi
            // 
            this.btnSpremi.BackColor = System.Drawing.Color.Transparent;
            this.btnSpremi.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSpremi.BackgroundImage")));
            this.btnSpremi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSpremi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSpremi.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnSpremi.FlatAppearance.BorderSize = 0;
            this.btnSpremi.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnSpremi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSlateGray;
            this.btnSpremi.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSlateGray;
            this.btnSpremi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpremi.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnSpremi.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSpremi.Location = new System.Drawing.Point(121, 205);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(140, 35);
            this.btnSpremi.TabIndex = 4;
            this.btnSpremi.Text = "Spremi";
            this.btnSpremi.UseVisualStyleBackColor = false;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // txtOIB
            // 
            this.txtOIB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtOIB.Location = new System.Drawing.Point(11, 77);
            this.txtOIB.Name = "txtOIB";
            this.txtOIB.Size = new System.Drawing.Size(249, 23);
            this.txtOIB.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(10, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "OIB";
            // 
            // frmBrziUpisKlijenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(274, 250);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtOIB);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbGrad);
            this.Controls.Add(this.txtAdresa);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtImeKlijenta);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmBrziUpisKlijenta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Brzi upis klijenta";
            this.Load += new System.EventHandler(this.frmBrziUpisKlijenta_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtImeKlijenta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAdresa;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbGrad;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.TextBox txtOIB;
        private System.Windows.Forms.Label label4;
    }
}