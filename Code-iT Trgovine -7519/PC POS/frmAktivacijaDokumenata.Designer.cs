namespace PCPOS
{
    partial class frmAktivacijaDokumenata
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbKasica = new System.Windows.Forms.RadioButton();
            this.rbTrgovina = new System.Windows.Forms.RadioButton();
            this.chbOtpisRobe = new System.Windows.Forms.CheckBox();
            this.chbKartoteka = new System.Windows.Forms.CheckBox();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.chbPrometPoRobi = new System.Windows.Forms.CheckBox();
            this.chbPocetnoStanje = new System.Windows.Forms.CheckBox();
            this.chbPromocije = new System.Windows.Forms.CheckBox();
            this.chbIzdatnice = new System.Windows.Forms.CheckBox();
            this.chbPrimke = new System.Windows.Forms.CheckBox();
            this.chbMeduskl = new System.Windows.Forms.CheckBox();
            this.chbOtpremnica = new System.Windows.Forms.CheckBox();
            this.chbUlazneFak = new System.Windows.Forms.CheckBox();
            this.chbNaljepnice = new System.Windows.Forms.CheckBox();
            this.chbPovratnicaDob = new System.Windows.Forms.CheckBox();
            this.chbZapisnikOpromjeniCijene = new System.Windows.Forms.CheckBox();
            this.chbOdjavaKomisione = new System.Windows.Forms.CheckBox();
            this.chbRN = new System.Windows.Forms.CheckBox();
            this.chbPonude = new System.Windows.Forms.CheckBox();
            this.chbFakBezRobe = new System.Windows.Forms.CheckBox();
            this.chbFakture = new System.Windows.Forms.CheckBox();
            this.chbKarticaRobe = new System.Windows.Forms.CheckBox();
            this.chbInventure = new System.Windows.Forms.CheckBox();
            this.chbKalkulacije = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbKasica);
            this.groupBox1.Controls.Add(this.rbTrgovina);
            this.groupBox1.Controls.Add(this.chbOtpisRobe);
            this.groupBox1.Controls.Add(this.chbKartoteka);
            this.groupBox1.Controls.Add(this.btnSpremi);
            this.groupBox1.Controls.Add(this.chbPrometPoRobi);
            this.groupBox1.Controls.Add(this.chbPocetnoStanje);
            this.groupBox1.Controls.Add(this.chbPromocije);
            this.groupBox1.Controls.Add(this.chbIzdatnice);
            this.groupBox1.Controls.Add(this.chbPrimke);
            this.groupBox1.Controls.Add(this.chbMeduskl);
            this.groupBox1.Controls.Add(this.chbOtpremnica);
            this.groupBox1.Controls.Add(this.chbUlazneFak);
            this.groupBox1.Controls.Add(this.chbNaljepnice);
            this.groupBox1.Controls.Add(this.chbPovratnicaDob);
            this.groupBox1.Controls.Add(this.chbZapisnikOpromjeniCijene);
            this.groupBox1.Controls.Add(this.chbOdjavaKomisione);
            this.groupBox1.Controls.Add(this.chbRN);
            this.groupBox1.Controls.Add(this.chbPonude);
            this.groupBox1.Controls.Add(this.chbFakBezRobe);
            this.groupBox1.Controls.Add(this.chbFakture);
            this.groupBox1.Controls.Add(this.chbKarticaRobe);
            this.groupBox1.Controls.Add(this.chbInventure);
            this.groupBox1.Controls.Add(this.chbKalkulacije);
            this.groupBox1.Location = new System.Drawing.Point(13, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 345);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dokumenti";
            // 
            // rbKasica
            // 
            this.rbKasica.AutoSize = true;
            this.rbKasica.Location = new System.Drawing.Point(97, 19);
            this.rbKasica.Name = "rbKasica";
            this.rbKasica.Size = new System.Drawing.Size(57, 17);
            this.rbKasica.TabIndex = 2;
            this.rbKasica.TabStop = true;
            this.rbKasica.Text = "Kasica";
            this.rbKasica.UseVisualStyleBackColor = true;
            // 
            // rbTrgovina
            // 
            this.rbTrgovina.AutoSize = true;
            this.rbTrgovina.Location = new System.Drawing.Point(240, 19);
            this.rbTrgovina.Name = "rbTrgovina";
            this.rbTrgovina.Size = new System.Drawing.Size(109, 17);
            this.rbTrgovina.TabIndex = 3;
            this.rbTrgovina.TabStop = true;
            this.rbTrgovina.Text = "PC POS Trgovina";
            this.rbTrgovina.UseVisualStyleBackColor = true;
            this.rbTrgovina.CheckedChanged += new System.EventHandler(this.rbTrgovina_CheckedChanged);
            // 
            // chbOtpisRobe
            // 
            this.chbOtpisRobe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbOtpisRobe.AutoSize = true;
            this.chbOtpisRobe.Enabled = false;
            this.chbOtpisRobe.Location = new System.Drawing.Point(6, 300);
            this.chbOtpisRobe.Name = "chbOtpisRobe";
            this.chbOtpisRobe.Size = new System.Drawing.Size(74, 17);
            this.chbOtpisRobe.TabIndex = 3;
            this.chbOtpisRobe.Text = "Otpis robe";
            this.chbOtpisRobe.UseVisualStyleBackColor = true;
            // 
            // chbKartoteka
            // 
            this.chbKartoteka.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbKartoteka.AutoSize = true;
            this.chbKartoteka.Location = new System.Drawing.Point(240, 277);
            this.chbKartoteka.Name = "chbKartoteka";
            this.chbKartoteka.Size = new System.Drawing.Size(72, 17);
            this.chbKartoteka.TabIndex = 2;
            this.chbKartoteka.Text = "Kartoteka";
            this.chbKartoteka.UseVisualStyleBackColor = true;
            // 
            // btnSpremi
            // 
            this.btnSpremi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSpremi.Location = new System.Drawing.Point(349, 317);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(95, 23);
            this.btnSpremi.TabIndex = 1;
            this.btnSpremi.Text = "Spremi";
            this.btnSpremi.UseVisualStyleBackColor = true;
            this.btnSpremi.Click += new System.EventHandler(this.button1_Click);
            // 
            // chbPrometPoRobi
            // 
            this.chbPrometPoRobi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbPrometPoRobi.AutoSize = true;
            this.chbPrometPoRobi.Enabled = false;
            this.chbPrometPoRobi.Location = new System.Drawing.Point(240, 255);
            this.chbPrometPoRobi.Name = "chbPrometPoRobi";
            this.chbPrometPoRobi.Size = new System.Drawing.Size(94, 17);
            this.chbPrometPoRobi.TabIndex = 0;
            this.chbPrometPoRobi.Text = "Promet po robi";
            this.chbPrometPoRobi.UseVisualStyleBackColor = true;
            // 
            // chbPocetnoStanje
            // 
            this.chbPocetnoStanje.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbPocetnoStanje.AutoSize = true;
            this.chbPocetnoStanje.Enabled = false;
            this.chbPocetnoStanje.Location = new System.Drawing.Point(240, 233);
            this.chbPocetnoStanje.Name = "chbPocetnoStanje";
            this.chbPocetnoStanje.Size = new System.Drawing.Size(97, 17);
            this.chbPocetnoStanje.TabIndex = 0;
            this.chbPocetnoStanje.Text = "Početno stanje";
            this.chbPocetnoStanje.UseVisualStyleBackColor = true;
            // 
            // chbPromocije
            // 
            this.chbPromocije.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbPromocije.AutoSize = true;
            this.chbPromocije.Enabled = false;
            this.chbPromocije.Location = new System.Drawing.Point(240, 211);
            this.chbPromocije.Name = "chbPromocije";
            this.chbPromocije.Size = new System.Drawing.Size(72, 17);
            this.chbPromocije.TabIndex = 0;
            this.chbPromocije.Text = "Promocije";
            this.chbPromocije.UseVisualStyleBackColor = true;
            // 
            // chbIzdatnice
            // 
            this.chbIzdatnice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbIzdatnice.AutoSize = true;
            this.chbIzdatnice.Enabled = false;
            this.chbIzdatnice.Location = new System.Drawing.Point(240, 189);
            this.chbIzdatnice.Name = "chbIzdatnice";
            this.chbIzdatnice.Size = new System.Drawing.Size(69, 17);
            this.chbIzdatnice.TabIndex = 0;
            this.chbIzdatnice.Text = "Izdatnice";
            this.chbIzdatnice.UseVisualStyleBackColor = true;
            // 
            // chbPrimke
            // 
            this.chbPrimke.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbPrimke.AutoSize = true;
            this.chbPrimke.Enabled = false;
            this.chbPrimke.Location = new System.Drawing.Point(240, 167);
            this.chbPrimke.Name = "chbPrimke";
            this.chbPrimke.Size = new System.Drawing.Size(58, 17);
            this.chbPrimke.TabIndex = 0;
            this.chbPrimke.Text = "Primke";
            this.chbPrimke.UseVisualStyleBackColor = true;
            // 
            // chbMeduskl
            // 
            this.chbMeduskl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbMeduskl.AutoSize = true;
            this.chbMeduskl.Enabled = false;
            this.chbMeduskl.Location = new System.Drawing.Point(240, 145);
            this.chbMeduskl.Name = "chbMeduskl";
            this.chbMeduskl.Size = new System.Drawing.Size(106, 17);
            this.chbMeduskl.TabIndex = 0;
            this.chbMeduskl.Text = "Međuskladišnica";
            this.chbMeduskl.UseVisualStyleBackColor = true;
            // 
            // chbOtpremnica
            // 
            this.chbOtpremnica.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbOtpremnica.AutoSize = true;
            this.chbOtpremnica.Enabled = false;
            this.chbOtpremnica.Location = new System.Drawing.Point(240, 123);
            this.chbOtpremnica.Name = "chbOtpremnica";
            this.chbOtpremnica.Size = new System.Drawing.Size(80, 17);
            this.chbOtpremnica.TabIndex = 0;
            this.chbOtpremnica.Text = "Otpremnica";
            this.chbOtpremnica.UseVisualStyleBackColor = true;
            // 
            // chbUlazneFak
            // 
            this.chbUlazneFak.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbUlazneFak.AutoSize = true;
            this.chbUlazneFak.Enabled = false;
            this.chbUlazneFak.Location = new System.Drawing.Point(240, 101);
            this.chbUlazneFak.Name = "chbUlazneFak";
            this.chbUlazneFak.Size = new System.Drawing.Size(95, 17);
            this.chbUlazneFak.TabIndex = 0;
            this.chbUlazneFak.Text = "Ulazne fakture";
            this.chbUlazneFak.UseVisualStyleBackColor = true;
            // 
            // chbNaljepnice
            // 
            this.chbNaljepnice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbNaljepnice.AutoSize = true;
            this.chbNaljepnice.Enabled = false;
            this.chbNaljepnice.Location = new System.Drawing.Point(240, 79);
            this.chbNaljepnice.Name = "chbNaljepnice";
            this.chbNaljepnice.Size = new System.Drawing.Size(76, 17);
            this.chbNaljepnice.TabIndex = 0;
            this.chbNaljepnice.Text = "Naljepnice";
            this.chbNaljepnice.UseVisualStyleBackColor = true;
            // 
            // chbPovratnicaDob
            // 
            this.chbPovratnicaDob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbPovratnicaDob.AutoSize = true;
            this.chbPovratnicaDob.Enabled = false;
            this.chbPovratnicaDob.Location = new System.Drawing.Point(6, 277);
            this.chbPovratnicaDob.Name = "chbPovratnicaDob";
            this.chbPovratnicaDob.Size = new System.Drawing.Size(132, 17);
            this.chbPovratnicaDob.TabIndex = 0;
            this.chbPovratnicaDob.Text = "Povratnica dobavljaču";
            this.chbPovratnicaDob.UseVisualStyleBackColor = true;
            // 
            // chbZapisnikOpromjeniCijene
            // 
            this.chbZapisnikOpromjeniCijene.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbZapisnikOpromjeniCijene.AutoSize = true;
            this.chbZapisnikOpromjeniCijene.Enabled = false;
            this.chbZapisnikOpromjeniCijene.Location = new System.Drawing.Point(6, 255);
            this.chbZapisnikOpromjeniCijene.Name = "chbZapisnikOpromjeniCijene";
            this.chbZapisnikOpromjeniCijene.Size = new System.Drawing.Size(148, 17);
            this.chbZapisnikOpromjeniCijene.TabIndex = 0;
            this.chbZapisnikOpromjeniCijene.Text = "Zapisnik o promjeni cijene";
            this.chbZapisnikOpromjeniCijene.UseVisualStyleBackColor = true;
            // 
            // chbOdjavaKomisione
            // 
            this.chbOdjavaKomisione.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbOdjavaKomisione.AutoSize = true;
            this.chbOdjavaKomisione.Enabled = false;
            this.chbOdjavaKomisione.Location = new System.Drawing.Point(6, 233);
            this.chbOdjavaKomisione.Name = "chbOdjavaKomisione";
            this.chbOdjavaKomisione.Size = new System.Drawing.Size(134, 17);
            this.chbOdjavaKomisione.TabIndex = 0;
            this.chbOdjavaKomisione.Text = "Odjava komisione robe";
            this.chbOdjavaKomisione.UseVisualStyleBackColor = true;
            // 
            // chbRN
            // 
            this.chbRN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbRN.AutoSize = true;
            this.chbRN.Enabled = false;
            this.chbRN.Location = new System.Drawing.Point(6, 211);
            this.chbRN.Name = "chbRN";
            this.chbRN.Size = new System.Drawing.Size(84, 17);
            this.chbRN.TabIndex = 0;
            this.chbRN.Text = "Radni nalozi";
            this.chbRN.UseVisualStyleBackColor = true;
            // 
            // chbPonude
            // 
            this.chbPonude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbPonude.AutoSize = true;
            this.chbPonude.Enabled = false;
            this.chbPonude.Location = new System.Drawing.Point(6, 189);
            this.chbPonude.Name = "chbPonude";
            this.chbPonude.Size = new System.Drawing.Size(63, 17);
            this.chbPonude.TabIndex = 0;
            this.chbPonude.Text = "Ponude";
            this.chbPonude.UseVisualStyleBackColor = true;
            // 
            // chbFakBezRobe
            // 
            this.chbFakBezRobe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbFakBezRobe.AutoSize = true;
            this.chbFakBezRobe.Enabled = false;
            this.chbFakBezRobe.Location = new System.Drawing.Point(6, 167);
            this.chbFakBezRobe.Name = "chbFakBezRobe";
            this.chbFakBezRobe.Size = new System.Drawing.Size(106, 17);
            this.chbFakBezRobe.TabIndex = 0;
            this.chbFakBezRobe.Text = "Fakture bez robe";
            this.chbFakBezRobe.UseVisualStyleBackColor = true;
            // 
            // chbFakture
            // 
            this.chbFakture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbFakture.AutoSize = true;
            this.chbFakture.Enabled = false;
            this.chbFakture.Location = new System.Drawing.Point(6, 145);
            this.chbFakture.Name = "chbFakture";
            this.chbFakture.Size = new System.Drawing.Size(62, 17);
            this.chbFakture.TabIndex = 0;
            this.chbFakture.Text = "Fakture";
            this.chbFakture.UseVisualStyleBackColor = true;
            // 
            // chbKarticaRobe
            // 
            this.chbKarticaRobe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbKarticaRobe.AutoSize = true;
            this.chbKarticaRobe.Enabled = false;
            this.chbKarticaRobe.Location = new System.Drawing.Point(6, 123);
            this.chbKarticaRobe.Name = "chbKarticaRobe";
            this.chbKarticaRobe.Size = new System.Drawing.Size(83, 17);
            this.chbKarticaRobe.TabIndex = 0;
            this.chbKarticaRobe.Text = "Kartica robe";
            this.chbKarticaRobe.UseVisualStyleBackColor = true;
            // 
            // chbInventure
            // 
            this.chbInventure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbInventure.AutoSize = true;
            this.chbInventure.Enabled = false;
            this.chbInventure.Location = new System.Drawing.Point(6, 101);
            this.chbInventure.Name = "chbInventure";
            this.chbInventure.Size = new System.Drawing.Size(71, 17);
            this.chbInventure.TabIndex = 0;
            this.chbInventure.Text = "Inventure";
            this.chbInventure.UseVisualStyleBackColor = true;
            // 
            // chbKalkulacije
            // 
            this.chbKalkulacije.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbKalkulacije.AutoSize = true;
            this.chbKalkulacije.Enabled = false;
            this.chbKalkulacije.Location = new System.Drawing.Point(6, 79);
            this.chbKalkulacije.Name = "chbKalkulacije";
            this.chbKalkulacije.Size = new System.Drawing.Size(77, 17);
            this.chbKalkulacije.TabIndex = 0;
            this.chbKalkulacije.Text = "Kalkulacije";
            this.chbKalkulacije.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(451, 60);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Šifra za pristup";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(118, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '*';
            this.textBox1.Size = new System.Drawing.Size(278, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // frmAktivacijaDokumenata
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(475, 441);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximumSize = new System.Drawing.Size(491, 479);
            this.MinimumSize = new System.Drawing.Size(491, 479);
            this.Name = "frmAktivacijaDokumenata";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aktivacija dokumenta";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAktivacijaDokumenata_FormClosing);
            this.Load += new System.EventHandler(this.frmAktivacijaDokumenata_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chbPocetnoStanje;
        private System.Windows.Forms.CheckBox chbPromocije;
        private System.Windows.Forms.CheckBox chbIzdatnice;
        private System.Windows.Forms.CheckBox chbPrimke;
        private System.Windows.Forms.CheckBox chbMeduskl;
        private System.Windows.Forms.CheckBox chbOtpremnica;
        private System.Windows.Forms.CheckBox chbUlazneFak;
        private System.Windows.Forms.CheckBox chbNaljepnice;
        private System.Windows.Forms.CheckBox chbPovratnicaDob;
        private System.Windows.Forms.CheckBox chbZapisnikOpromjeniCijene;
        private System.Windows.Forms.CheckBox chbOdjavaKomisione;
        private System.Windows.Forms.CheckBox chbRN;
        private System.Windows.Forms.CheckBox chbPonude;
        private System.Windows.Forms.CheckBox chbFakBezRobe;
        private System.Windows.Forms.CheckBox chbFakture;
        private System.Windows.Forms.CheckBox chbKarticaRobe;
        private System.Windows.Forms.CheckBox chbInventure;
        private System.Windows.Forms.CheckBox chbKalkulacije;
        private System.Windows.Forms.CheckBox chbPrometPoRobi;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSpremi;
		private System.Windows.Forms.CheckBox chbKartoteka;
		private System.Windows.Forms.CheckBox chbOtpisRobe;
        private System.Windows.Forms.RadioButton rbKasica;
        private System.Windows.Forms.RadioButton rbTrgovina;
    }
}