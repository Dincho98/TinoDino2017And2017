namespace PCPOS {
    partial class frmRaspodjeliTroskovePrijevoza {
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
            this.txtUkupnoPrijevozZaRaspodjelu = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRaspodjeli = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtUkupnoPrijevozZaRaspodjelu
            // 
            this.txtUkupnoPrijevozZaRaspodjelu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtUkupnoPrijevozZaRaspodjelu.Location = new System.Drawing.Point(187, 71);
            this.txtUkupnoPrijevozZaRaspodjelu.Name = "txtUkupnoPrijevozZaRaspodjelu";
            this.txtUkupnoPrijevozZaRaspodjelu.Size = new System.Drawing.Size(92, 23);
            this.txtUkupnoPrijevozZaRaspodjelu.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(13, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 17);
            this.label1.TabIndex = 24;
            this.label1.Text = "Unesite trošak dostave";
            // 
            // btnRaspodjeli
            // 
            this.btnRaspodjeli.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.btnRaspodjeli.Location = new System.Drawing.Point(286, 69);
            this.btnRaspodjeli.Name = "btnRaspodjeli";
            this.btnRaspodjeli.Size = new System.Drawing.Size(66, 27);
            this.btnRaspodjeli.TabIndex = 25;
            this.btnRaspodjeli.Text = "Postavi";
            this.btnRaspodjeli.UseVisualStyleBackColor = true;
            this.btnRaspodjeli.Click += new System.EventHandler(this.btnRaspodjeli_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(12, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 17);
            this.label2.TabIndex = 26;
            this.label2.Text = "Iznos prijevoza u kunama:";
            // 
            // frmRaspodjeliTroskovePrijevoza
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(390, 106);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnRaspodjeli);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUkupnoPrijevozZaRaspodjelu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmRaspodjeliTroskovePrijevoza";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Raspodjeli troskove prijevoza";
            this.Load += new System.EventHandler(this.frmRaspodjeliTroskovePrijevoza_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUkupnoPrijevozZaRaspodjelu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRaspodjeli;
        private System.Windows.Forms.Label label2;
    }
}