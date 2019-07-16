namespace PCPOS.sinkronizacija_poslovnica
{
    partial class frmPostavke
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPostavke));
            this.txtSifraPartneraStart = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.clbKalkulacija = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.clbPrimke = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLozinka = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtKorIme = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIpAdresa = new System.Windows.Forms.TextBox();
            this.btnPopust = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.vbAktiviraj = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSifraPartneraStart
            // 
            this.txtSifraPartneraStart.Location = new System.Drawing.Point(15, 40);
            this.txtSifraPartneraStart.Name = "txtSifraPartneraStart";
            this.txtSifraPartneraStart.Size = new System.Drawing.Size(217, 20);
            this.txtSifraPartneraStart.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtSifraPartneraStart);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 254);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(249, 74);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Partneri";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Šifra partnera počinje od broja";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.clbKalkulacija);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(275, 253);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(214, 164);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Kalkulacije";
            // 
            // clbKalkulacija
            // 
            this.clbKalkulacija.FormattingEnabled = true;
            this.clbKalkulacija.Location = new System.Drawing.Point(18, 43);
            this.clbKalkulacija.Name = "clbKalkulacija";
            this.clbKalkulacija.Size = new System.Drawing.Size(181, 109);
            this.clbKalkulacija.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Označi skladiše sa koje se uzima roba";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.clbPrimke);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(275, 79);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(214, 164);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Primke";
            // 
            // clbPrimke
            // 
            this.clbPrimke.FormattingEnabled = true;
            this.clbPrimke.Location = new System.Drawing.Point(18, 43);
            this.clbPrimke.Name = "clbPrimke";
            this.clbPrimke.Size = new System.Drawing.Size(181, 109);
            this.clbPrimke.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Označi skladiše sa koje se uzima roba";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.txtLozinka);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.txtKorIme);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.txtIpAdresa);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(12, 79);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(249, 164);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Parametri za spajanje";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Lozinka";
            // 
            // txtLozinka
            // 
            this.txtLozinka.Location = new System.Drawing.Point(15, 121);
            this.txtLozinka.Name = "txtLozinka";
            this.txtLozinka.Size = new System.Drawing.Size(217, 20);
            this.txtLozinka.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Korisničko ime";
            // 
            // txtKorIme
            // 
            this.txtKorIme.Location = new System.Drawing.Point(15, 80);
            this.txtKorIme.Name = "txtKorIme";
            this.txtKorIme.Size = new System.Drawing.Size(217, 20);
            this.txtKorIme.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "IP adresa";
            // 
            // txtIpAdresa
            // 
            this.txtIpAdresa.Location = new System.Drawing.Point(15, 41);
            this.txtIpAdresa.Name = "txtIpAdresa";
            this.txtIpAdresa.Size = new System.Drawing.Size(217, 20);
            this.txtIpAdresa.TabIndex = 1;
            // 
            // btnPopust
            // 
            this.btnPopust.BackColor = System.Drawing.Color.Transparent;
            this.btnPopust.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPopust.BackgroundImage")));
            this.btnPopust.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPopust.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPopust.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnPopust.FlatAppearance.BorderSize = 0;
            this.btnPopust.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnPopust.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSlateGray;
            this.btnPopust.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSlateGray;
            this.btnPopust.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPopust.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnPopust.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnPopust.Location = new System.Drawing.Point(275, 433);
            this.btnPopust.Name = "btnPopust";
            this.btnPopust.Size = new System.Drawing.Size(214, 35);
            this.btnPopust.TabIndex = 110;
            this.btnPopust.Text = "Spremi promjene";
            this.btnPopust.UseVisualStyleBackColor = false;
            this.btnPopust.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(12, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(227, 24);
            this.label7.TabIndex = 111;
            this.label7.Text = "Postavke za sinkronizaciju";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(10, 343);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(254, 75);
            this.label8.TabIndex = 112;
            this.label8.Text = resources.GetString("label8.Text");
            // 
            // vbAktiviraj
            // 
            this.vbAktiviraj.AutoSize = true;
            this.vbAktiviraj.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.vbAktiviraj.ForeColor = System.Drawing.Color.Black;
            this.vbAktiviraj.Location = new System.Drawing.Point(309, 24);
            this.vbAktiviraj.Name = "vbAktiviraj";
            this.vbAktiviraj.Size = new System.Drawing.Size(180, 24);
            this.vbAktiviraj.TabIndex = 113;
            this.vbAktiviraj.Text = "Aktiviraj sinkronizaciju";
            this.vbAktiviraj.UseVisualStyleBackColor = true;
            this.vbAktiviraj.CheckedChanged += new System.EventHandler(this.vbAktiviraj_CheckedChanged);
            // 
            // frmPostavke
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(517, 480);
            this.Controls.Add(this.vbAktiviraj);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnPopust);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmPostavke";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Postavke za sinkronizaciju";
            this.Load += new System.EventHandler(this.frmPostavke_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSifraPartneraStart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox clbKalkulacija;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckedListBox clbPrimke;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLozinka;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtKorIme;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtIpAdresa;
        private System.Windows.Forms.Button btnPopust;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox vbAktiviraj;
    }
}