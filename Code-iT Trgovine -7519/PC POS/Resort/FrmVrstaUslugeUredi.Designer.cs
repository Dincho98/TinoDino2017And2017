namespace PCPOS.Resort
{
    partial class FrmVrstaUslugeUredi
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
            this.btnUredi = new System.Windows.Forms.Button();
            this.txtBoxIznos = new System.Windows.Forms.TextBox();
            this.txtBoxNazivUsluge = new System.Windows.Forms.TextBox();
            this.txtBoxBroj = new System.Windows.Forms.TextBox();
            this.richTextBoxNapomene = new System.Windows.Forms.RichTextBox();
            this.labelNaziv = new System.Windows.Forms.Label();
            this.labelIznos = new System.Windows.Forms.Label();
            this.labelNapomene = new System.Windows.Forms.Label();
            this.labelBroj = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnUredi
            // 
            this.btnUredi.BackColor = System.Drawing.Color.White;
            this.btnUredi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUredi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUredi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnUredi.Location = new System.Drawing.Point(83, 249);
            this.btnUredi.Name = "btnUredi";
            this.btnUredi.Size = new System.Drawing.Size(138, 40);
            this.btnUredi.TabIndex = 18;
            this.btnUredi.Text = "Uredi";
            this.btnUredi.UseVisualStyleBackColor = false;
            this.btnUredi.Click += new System.EventHandler(this.btnUredi_Click);
            // 
            // txtBoxIznos
            // 
            this.txtBoxIznos.Location = new System.Drawing.Point(135, 91);
            this.txtBoxIznos.Name = "txtBoxIznos";
            this.txtBoxIznos.Size = new System.Drawing.Size(148, 20);
            this.txtBoxIznos.TabIndex = 17;
            // 
            // txtBoxNazivUsluge
            // 
            this.txtBoxNazivUsluge.Location = new System.Drawing.Point(135, 53);
            this.txtBoxNazivUsluge.Name = "txtBoxNazivUsluge";
            this.txtBoxNazivUsluge.Size = new System.Drawing.Size(148, 20);
            this.txtBoxNazivUsluge.TabIndex = 16;
            // 
            // txtBoxBroj
            // 
            this.txtBoxBroj.Location = new System.Drawing.Point(135, 15);
            this.txtBoxBroj.Name = "txtBoxBroj";
            this.txtBoxBroj.ReadOnly = true;
            this.txtBoxBroj.Size = new System.Drawing.Size(148, 20);
            this.txtBoxBroj.TabIndex = 15;
            // 
            // richTextBoxNapomene
            // 
            this.richTextBoxNapomene.Location = new System.Drawing.Point(27, 155);
            this.richTextBoxNapomene.Name = "richTextBoxNapomene";
            this.richTextBoxNapomene.Size = new System.Drawing.Size(260, 76);
            this.richTextBoxNapomene.TabIndex = 14;
            this.richTextBoxNapomene.Text = "";
            // 
            // labelNaziv
            // 
            this.labelNaziv.AutoSize = true;
            this.labelNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNaziv.Location = new System.Drawing.Point(14, 53);
            this.labelNaziv.Name = "labelNaziv";
            this.labelNaziv.Size = new System.Drawing.Size(106, 17);
            this.labelNaziv.TabIndex = 13;
            this.labelNaziv.Text = "Naziv usluge:";
            // 
            // labelIznos
            // 
            this.labelIznos.AutoSize = true;
            this.labelIznos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelIznos.Location = new System.Drawing.Point(67, 91);
            this.labelIznos.Name = "labelIznos";
            this.labelIznos.Size = new System.Drawing.Size(51, 17);
            this.labelIznos.TabIndex = 12;
            this.labelIznos.Text = "Iznos:";
            // 
            // labelNapomene
            // 
            this.labelNapomene.AutoSize = true;
            this.labelNapomene.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNapomene.Location = new System.Drawing.Point(24, 135);
            this.labelNapomene.Name = "labelNapomene";
            this.labelNapomene.Size = new System.Drawing.Size(90, 17);
            this.labelNapomene.TabIndex = 11;
            this.labelNapomene.Text = "Napomene:";
            // 
            // labelBroj
            // 
            this.labelBroj.AutoSize = true;
            this.labelBroj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelBroj.Location = new System.Drawing.Point(72, 15);
            this.labelBroj.Name = "labelBroj";
            this.labelBroj.Size = new System.Drawing.Size(42, 17);
            this.labelBroj.TabIndex = 10;
            this.labelBroj.Text = "Broj:";
            // 
            // FrmVrstaUslugeUredi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(305, 306);
            this.Controls.Add(this.btnUredi);
            this.Controls.Add(this.txtBoxIznos);
            this.Controls.Add(this.txtBoxNazivUsluge);
            this.Controls.Add(this.txtBoxBroj);
            this.Controls.Add(this.richTextBoxNapomene);
            this.Controls.Add(this.labelNaziv);
            this.Controls.Add(this.labelIznos);
            this.Controls.Add(this.labelNapomene);
            this.Controls.Add(this.labelBroj);
            this.Name = "FrmVrstaUslugeUredi";
            this.Text = "FrmVrstaUslugeUredi";
            this.Load += new System.EventHandler(this.FrmVrstaUslugeUredi_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUredi;
        private System.Windows.Forms.TextBox txtBoxIznos;
        private System.Windows.Forms.TextBox txtBoxNazivUsluge;
        private System.Windows.Forms.TextBox txtBoxBroj;
        private System.Windows.Forms.RichTextBox richTextBoxNapomene;
        private System.Windows.Forms.Label labelNaziv;
        private System.Windows.Forms.Label labelIznos;
        private System.Windows.Forms.Label labelNapomene;
        private System.Windows.Forms.Label labelBroj;
    }
}