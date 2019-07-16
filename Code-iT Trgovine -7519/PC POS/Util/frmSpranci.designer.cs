namespace PCPOS.Util
{
    partial class frmSpranci
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
            this.txtLogo = new System.Windows.Forms.TextBox();
            this.rtbPoslovnica = new System.Windows.Forms.RichTextBox();
            this.txtSlika = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOtvoriSliku = new System.Windows.Forms.Button();
            this.rtbOpis = new System.Windows.Forms.RichTextBox();
            this.txtCijenaKartice = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCijenaGotovina = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOtvoriLink = new System.Windows.Forms.Button();
            this.btnPrikazi = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pbGlavnaSlika = new System.Windows.Forms.PictureBox();
            this.pbZaLogo = new System.Windows.Forms.PictureBox();
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.txtNaziv = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbGlavnaSlika)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbZaLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLogo
            // 
            this.txtLogo.Location = new System.Drawing.Point(15, 127);
            this.txtLogo.Name = "txtLogo";
            this.txtLogo.Size = new System.Drawing.Size(388, 20);
            this.txtLogo.TabIndex = 0;
            this.txtLogo.TextChanged += new System.EventHandler(this.txtLogo_TextChanged);
            // 
            // rtbPoslovnica
            // 
            this.rtbPoslovnica.Location = new System.Drawing.Point(235, 13);
            this.rtbPoslovnica.Name = "rtbPoslovnica";
            this.rtbPoslovnica.Size = new System.Drawing.Size(230, 91);
            this.rtbPoslovnica.TabIndex = 2;
            this.rtbPoslovnica.Text = "";
            // 
            // txtSlika
            // 
            this.txtSlika.Location = new System.Drawing.Point(12, 187);
            this.txtSlika.Name = "txtSlika";
            this.txtSlika.Size = new System.Drawing.Size(391, 20);
            this.txtSlika.TabIndex = 0;
            this.txtSlika.TextChanged += new System.EventHandler(this.txtSlika_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Web link za sliku ili putanja od slike";
            // 
            // btnOtvoriSliku
            // 
            this.btnOtvoriSliku.Location = new System.Drawing.Point(405, 186);
            this.btnOtvoriSliku.Name = "btnOtvoriSliku";
            this.btnOtvoriSliku.Size = new System.Drawing.Size(60, 23);
            this.btnOtvoriSliku.TabIndex = 5;
            this.btnOtvoriSliku.Text = "Otvori";
            this.btnOtvoriSliku.UseVisualStyleBackColor = true;
            this.btnOtvoriSliku.Click += new System.EventHandler(this.btnOtvoriSliku_Click);
            // 
            // rtbOpis
            // 
            this.rtbOpis.Location = new System.Drawing.Point(12, 417);
            this.rtbOpis.Name = "rtbOpis";
            this.rtbOpis.Size = new System.Drawing.Size(453, 83);
            this.rtbOpis.TabIndex = 6;
            this.rtbOpis.Text = "";
            // 
            // txtCijenaKartice
            // 
            this.txtCijenaKartice.Location = new System.Drawing.Point(13, 518);
            this.txtCijenaKartice.Name = "txtCijenaKartice";
            this.txtCijenaKartice.Size = new System.Drawing.Size(123, 20);
            this.txtCijenaKartice.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 503);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Cijena za kartice:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 543);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Cijena za gotovinu";
            // 
            // txtCijenaGotovina
            // 
            this.txtCijenaGotovina.Location = new System.Drawing.Point(14, 558);
            this.txtCijenaGotovina.Name = "txtCijenaGotovina";
            this.txtCijenaGotovina.Size = new System.Drawing.Size(122, 20);
            this.txtCijenaGotovina.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(172, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Web link za logo ili putanja od slike";
            // 
            // btnOtvoriLink
            // 
            this.btnOtvoriLink.Location = new System.Drawing.Point(406, 125);
            this.btnOtvoriLink.Name = "btnOtvoriLink";
            this.btnOtvoriLink.Size = new System.Drawing.Size(60, 23);
            this.btnOtvoriLink.TabIndex = 13;
            this.btnOtvoriLink.Text = "Otvori";
            this.btnOtvoriLink.UseVisualStyleBackColor = true;
            this.btnOtvoriLink.Click += new System.EventHandler(this.btnOtvoriLink_Click);
            // 
            // btnPrikazi
            // 
            this.btnPrikazi.Location = new System.Drawing.Point(326, 538);
            this.btnPrikazi.Name = "btnPrikazi";
            this.btnPrikazi.Size = new System.Drawing.Size(140, 40);
            this.btnPrikazi.TabIndex = 14;
            this.btnPrikazi.Text = "Prikaži";
            this.btnPrikazi.UseVisualStyleBackColor = true;
            this.btnPrikazi.Click += new System.EventHandler(this.btnPrikazi_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(139, 522);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "kn";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(139, 561);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "kn";
            // 
            // pbGlavnaSlika
            // 
            this.pbGlavnaSlika.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbGlavnaSlika.Location = new System.Drawing.Point(13, 234);
            this.pbGlavnaSlika.Name = "pbGlavnaSlika";
            this.pbGlavnaSlika.Size = new System.Drawing.Size(454, 177);
            this.pbGlavnaSlika.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbGlavnaSlika.TabIndex = 3;
            this.pbGlavnaSlika.TabStop = false;
            // 
            // pbZaLogo
            // 
            this.pbZaLogo.Location = new System.Drawing.Point(13, 12);
            this.pbZaLogo.Name = "pbZaLogo";
            this.pbZaLogo.Size = new System.Drawing.Size(179, 92);
            this.pbZaLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbZaLogo.TabIndex = 1;
            this.pbZaLogo.TabStop = false;
            // 
            // txtSifra
            // 
            this.txtSifra.Location = new System.Drawing.Point(47, 150);
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(355, 20);
            this.txtSifra.TabIndex = 18;
            this.txtSifra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifra_KeyDown);
            // 
            // txtNaziv
            // 
            this.txtNaziv.Location = new System.Drawing.Point(94, 211);
            this.txtNaziv.Name = "txtNaziv";
            this.txtNaziv.Size = new System.Drawing.Size(372, 20);
            this.txtNaziv.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 214);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Naziv artikla:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 154);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Šifra:";
            // 
            // frmSpranci
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(478, 590);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtNaziv);
            this.Controls.Add(this.txtSifra);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnPrikazi);
            this.Controls.Add(this.btnOtvoriLink);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCijenaGotovina);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCijenaKartice);
            this.Controls.Add(this.rtbOpis);
            this.Controls.Add(this.btnOtvoriSliku);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbGlavnaSlika);
            this.Controls.Add(this.rtbPoslovnica);
            this.Controls.Add(this.pbZaLogo);
            this.Controls.Add(this.txtSlika);
            this.Controls.Add(this.txtLogo);
            this.Name = "frmSpranci";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Špranci";
            this.Load += new System.EventHandler(this.frmSpranci_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbGlavnaSlika)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbZaLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLogo;
        private System.Windows.Forms.PictureBox pbZaLogo;
        private System.Windows.Forms.RichTextBox rtbPoslovnica;
        private System.Windows.Forms.TextBox txtSlika;
        private System.Windows.Forms.PictureBox pbGlavnaSlika;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOtvoriSliku;
        private System.Windows.Forms.RichTextBox rtbOpis;
        private System.Windows.Forms.TextBox txtCijenaKartice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCijenaGotovina;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOtvoriLink;
        private System.Windows.Forms.Button btnPrikazi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.TextBox txtNaziv;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}