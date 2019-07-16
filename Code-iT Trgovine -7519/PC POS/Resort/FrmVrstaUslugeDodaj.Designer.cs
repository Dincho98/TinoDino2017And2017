namespace PCPOS.Resort
{
    partial class FrmVrstaUslugeDodaj
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
            this.labelBroj = new System.Windows.Forms.Label();
            this.labelNapomene = new System.Windows.Forms.Label();
            this.labelIznos = new System.Windows.Forms.Label();
            this.labelNaziv = new System.Windows.Forms.Label();
            this.richTextBoxNapomene = new System.Windows.Forms.RichTextBox();
            this.txtBoxId = new System.Windows.Forms.TextBox();
            this.txtBoxNazivUsluge = new System.Windows.Forms.TextBox();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.txtBoxIznos = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelBroj
            // 
            this.labelBroj.AutoSize = true;
            this.labelBroj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelBroj.Location = new System.Drawing.Point(72, 18);
            this.labelBroj.Name = "labelBroj";
            this.labelBroj.Size = new System.Drawing.Size(42, 17);
            this.labelBroj.TabIndex = 1;
            this.labelBroj.Text = "Broj:";
            // 
            // labelNapomene
            // 
            this.labelNapomene.AutoSize = true;
            this.labelNapomene.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNapomene.Location = new System.Drawing.Point(24, 138);
            this.labelNapomene.Name = "labelNapomene";
            this.labelNapomene.Size = new System.Drawing.Size(90, 17);
            this.labelNapomene.TabIndex = 2;
            this.labelNapomene.Text = "Napomene:";
            // 
            // labelIznos
            // 
            this.labelIznos.AutoSize = true;
            this.labelIznos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelIznos.Location = new System.Drawing.Point(67, 94);
            this.labelIznos.Name = "labelIznos";
            this.labelIznos.Size = new System.Drawing.Size(51, 17);
            this.labelIznos.TabIndex = 3;
            this.labelIznos.Text = "Iznos:";
            // 
            // labelNaziv
            // 
            this.labelNaziv.AutoSize = true;
            this.labelNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNaziv.Location = new System.Drawing.Point(14, 56);
            this.labelNaziv.Name = "labelNaziv";
            this.labelNaziv.Size = new System.Drawing.Size(106, 17);
            this.labelNaziv.TabIndex = 4;
            this.labelNaziv.Text = "Naziv usluge:";
            // 
            // richTextBoxNapomene
            // 
            this.richTextBoxNapomene.Location = new System.Drawing.Point(27, 158);
            this.richTextBoxNapomene.Name = "richTextBoxNapomene";
            this.richTextBoxNapomene.Size = new System.Drawing.Size(260, 76);
            this.richTextBoxNapomene.TabIndex = 5;
            this.richTextBoxNapomene.Text = "";
            // 
            // txtBoxId
            // 
            this.txtBoxId.Location = new System.Drawing.Point(135, 17);
            this.txtBoxId.Name = "txtBoxId";
            this.txtBoxId.ReadOnly = true;
            this.txtBoxId.Size = new System.Drawing.Size(148, 20);
            this.txtBoxId.TabIndex = 6;
            // 
            // txtBoxNazivUsluge
            // 
            this.txtBoxNazivUsluge.Location = new System.Drawing.Point(135, 56);
            this.txtBoxNazivUsluge.Name = "txtBoxNazivUsluge";
            this.txtBoxNazivUsluge.Size = new System.Drawing.Size(148, 20);
            this.txtBoxNazivUsluge.TabIndex = 7;
            // 
            // btnSpremi
            // 
            this.btnSpremi.BackColor = System.Drawing.Color.White;
            this.btnSpremi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSpremi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpremi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSpremi.Location = new System.Drawing.Point(83, 252);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(138, 40);
            this.btnSpremi.TabIndex = 9;
            this.btnSpremi.Text = "Spremi";
            this.btnSpremi.UseVisualStyleBackColor = false;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // txtBoxIznos
            // 
            this.txtBoxIznos.Location = new System.Drawing.Point(135, 94);
            this.txtBoxIznos.Name = "txtBoxIznos";
            this.txtBoxIznos.Size = new System.Drawing.Size(148, 20);
            this.txtBoxIznos.TabIndex = 8;
            // 
            // FrmVrstaUslugeDodaj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(309, 304);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.txtBoxIznos);
            this.Controls.Add(this.txtBoxNazivUsluge);
            this.Controls.Add(this.txtBoxId);
            this.Controls.Add(this.richTextBoxNapomene);
            this.Controls.Add(this.labelNaziv);
            this.Controls.Add(this.labelIznos);
            this.Controls.Add(this.labelNapomene);
            this.Controls.Add(this.labelBroj);
            this.Name = "FrmVrstaUslugeDodaj";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dodaj vrstu usluge";
            this.Load += new System.EventHandler(this.FrmVrstaUslugeDodaj_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBroj;
        private System.Windows.Forms.Label labelNapomene;
        private System.Windows.Forms.Label labelIznos;
        private System.Windows.Forms.Label labelNaziv;
        private System.Windows.Forms.RichTextBox richTextBoxNapomene;
        private System.Windows.Forms.TextBox txtBoxId;
        private System.Windows.Forms.TextBox txtBoxNazivUsluge;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.TextBox txtBoxIznos;
    }
}