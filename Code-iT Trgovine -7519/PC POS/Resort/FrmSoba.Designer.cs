namespace PCPOS.Resort
{
    partial class FrmSoba
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbTipSobe = new System.Windows.Forms.ComboBox();
            this.tbNazivSobe = new System.Windows.Forms.TextBox();
            this.tbBroj = new System.Windows.Forms.TextBox();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.numBrojLezaja = new System.Windows.Forms.NumericUpDown();
            this.tbCijenaNocenja = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numBrojLezaja)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(122, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Broj :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(88, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tip sobe :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(71, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Naziv sobe :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(74, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Broj ležaja :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(0, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(169, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Cijena noćenja(MPC) :";
            // 
            // cbTipSobe
            // 
            this.cbTipSobe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipSobe.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbTipSobe.FormattingEnabled = true;
            this.cbTipSobe.Location = new System.Drawing.Point(175, 63);
            this.cbTipSobe.Name = "cbTipSobe";
            this.cbTipSobe.Size = new System.Drawing.Size(165, 24);
            this.cbTipSobe.TabIndex = 3;
            // 
            // tbNazivSobe
            // 
            this.tbNazivSobe.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbNazivSobe.Location = new System.Drawing.Point(175, 38);
            this.tbNazivSobe.Name = "tbNazivSobe";
            this.tbNazivSobe.Size = new System.Drawing.Size(165, 23);
            this.tbNazivSobe.TabIndex = 2;
            this.tbNazivSobe.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbBroj
            // 
            this.tbBroj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbBroj.Location = new System.Drawing.Point(175, 11);
            this.tbBroj.Name = "tbBroj";
            this.tbBroj.ReadOnly = true;
            this.tbBroj.Size = new System.Drawing.Size(165, 23);
            this.tbBroj.TabIndex = 1;
            this.tbBroj.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSpremi
            // 
            this.btnSpremi.BackColor = System.Drawing.Color.White;
            this.btnSpremi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSpremi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpremi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSpremi.Location = new System.Drawing.Point(91, 160);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(175, 44);
            this.btnSpremi.TabIndex = 6;
            this.btnSpremi.Text = "Spremi";
            this.btnSpremi.UseVisualStyleBackColor = false;
            this.btnSpremi.Click += new System.EventHandler(this.BtnSpremi_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(315, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "kn";
            // 
            // numBrojLezaja
            // 
            this.numBrojLezaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.numBrojLezaja.Location = new System.Drawing.Point(175, 91);
            this.numBrojLezaja.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numBrojLezaja.Name = "numBrojLezaja";
            this.numBrojLezaja.Size = new System.Drawing.Size(165, 23);
            this.numBrojLezaja.TabIndex = 4;
            this.numBrojLezaja.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbCijenaNocenja
            // 
            this.tbCijenaNocenja.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbCijenaNocenja.Location = new System.Drawing.Point(175, 117);
            this.tbCijenaNocenja.Name = "tbCijenaNocenja";
            this.tbCijenaNocenja.Size = new System.Drawing.Size(134, 23);
            this.tbCijenaNocenja.TabIndex = 5;
            this.tbCijenaNocenja.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FrmSoba
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(358, 216);
            this.Controls.Add(this.tbCijenaNocenja);
            this.Controls.Add(this.numBrojLezaja);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.tbBroj);
            this.Controls.Add(this.tbNazivSobe);
            this.Controls.Add(this.cbTipSobe);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmSoba";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Soba";
            this.Load += new System.EventHandler(this.FrmSoba_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numBrojLezaja)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbTipSobe;
        private System.Windows.Forms.TextBox tbNazivSobe;
        private System.Windows.Forms.TextBox tbBroj;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numBrojLezaja;
        private System.Windows.Forms.TextBox tbCijenaNocenja;
    }
}