namespace PCPOS.Util
{
    partial class frmAvioPodaciDodaj
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
            this.txtRegistracijskaOznaka = new System.Windows.Forms.TextBox();
            this.txtTipZrakoplova = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaksTezinaPolijetanja = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDodaj = new System.Windows.Forms.Button();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Registracijska oznaka:";
            // 
            // txtRegistracijskaOznaka
            // 
            this.txtRegistracijskaOznaka.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtRegistracijskaOznaka.Location = new System.Drawing.Point(16, 34);
            this.txtRegistracijskaOznaka.Name = "txtRegistracijskaOznaka";
            this.txtRegistracijskaOznaka.Size = new System.Drawing.Size(256, 26);
            this.txtRegistracijskaOznaka.TabIndex = 1;
            // 
            // txtTipZrakoplova
            // 
            this.txtTipZrakoplova.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtTipZrakoplova.Location = new System.Drawing.Point(16, 84);
            this.txtTipZrakoplova.Name = "txtTipZrakoplova";
            this.txtTipZrakoplova.Size = new System.Drawing.Size(256, 26);
            this.txtTipZrakoplova.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(13, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tip zrakoplova:";
            // 
            // txtMaksTezinaPolijetanja
            // 
            this.txtMaksTezinaPolijetanja.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtMaksTezinaPolijetanja.Location = new System.Drawing.Point(16, 134);
            this.txtMaksTezinaPolijetanja.Name = "txtMaksTezinaPolijetanja";
            this.txtMaksTezinaPolijetanja.Size = new System.Drawing.Size(256, 26);
            this.txtMaksTezinaPolijetanja.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(13, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Maks. težina polijetanja:";
            // 
            // btnDodaj
            // 
            this.btnDodaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnDodaj.Location = new System.Drawing.Point(191, 203);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(81, 47);
            this.btnDodaj.TabIndex = 6;
            this.btnDodaj.Text = "Dodaj";
            this.btnDodaj.UseVisualStyleBackColor = true;
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);
            // 
            // btnObrisi
            // 
            this.btnObrisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnObrisi.Location = new System.Drawing.Point(16, 203);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(81, 47);
            this.btnObrisi.TabIndex = 7;
            this.btnObrisi.Text = "Obriši";
            this.btnObrisi.UseVisualStyleBackColor = true;
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // frmAvioPodaciDodaj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.ControlBox = false;
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.btnDodaj);
            this.Controls.Add(this.txtMaksTezinaPolijetanja);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTipZrakoplova);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtRegistracijskaOznaka);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "frmAvioPodaciDodaj";
            this.Text = "Podaci zrakoplova";
            this.Load += new System.EventHandler(this.frmAvioPodaciDodaj_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRegistracijskaOznaka;
        private System.Windows.Forms.TextBox txtTipZrakoplova;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaksTezinaPolijetanja;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.Button btnObrisi;
    }
}