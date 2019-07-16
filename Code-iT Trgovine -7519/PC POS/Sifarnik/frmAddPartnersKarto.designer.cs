namespace PCPOS.Sifarnik
{
    partial class frmAddPartnersKarto
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
            this.txtIme = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPrezime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTel = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMob = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.rtbNapomena = new System.Windows.Forms.TextBox();
            this.txtZupanija = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtDjelatnost = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.dtpDatum = new System.Windows.Forms.DateTimePicker();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label18 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbGrad = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtAdresa = new System.Windows.Forms.TextBox();
            this.txtSifra = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtIme
            // 
            this.txtIme.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtIme.Location = new System.Drawing.Point(108, 46);
            this.txtIme.MaxLength = 29;
            this.txtIme.Name = "txtIme";
            this.txtIme.Size = new System.Drawing.Size(249, 24);
            this.txtIme.TabIndex = 1;
            this.txtIme.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtIme.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label1.Location = new System.Drawing.Point(30, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ime:";
            // 
            // txtPrezime
            // 
            this.txtPrezime.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPrezime.Location = new System.Drawing.Point(108, 72);
            this.txtPrezime.MaxLength = 29;
            this.txtPrezime.Name = "txtPrezime";
            this.txtPrezime.Size = new System.Drawing.Size(249, 24);
            this.txtPrezime.TabIndex = 2;
            this.txtPrezime.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtPrezime.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label2.Location = new System.Drawing.Point(30, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Prezime:";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtEmail.Location = new System.Drawing.Point(108, 98);
            this.txtEmail.MaxLength = 29;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(249, 24);
            this.txtEmail.TabIndex = 4;
            this.txtEmail.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtEmail.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label3.Location = new System.Drawing.Point(30, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Email:";
            // 
            // txtTel
            // 
            this.txtTel.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtTel.Location = new System.Drawing.Point(108, 227);
            this.txtTel.MaxLength = 29;
            this.txtTel.Name = "txtTel";
            this.txtTel.Size = new System.Drawing.Size(250, 24);
            this.txtTel.TabIndex = 7;
            this.txtTel.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtTel.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label6.Location = new System.Drawing.Point(30, 230);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 17);
            this.label6.TabIndex = 2;
            this.label6.Text = "Tel:";
            // 
            // txtMob
            // 
            this.txtMob.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtMob.Location = new System.Drawing.Point(108, 124);
            this.txtMob.MaxLength = 29;
            this.txtMob.Name = "txtMob";
            this.txtMob.Size = new System.Drawing.Size(249, 24);
            this.txtMob.TabIndex = 9;
            this.txtMob.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtMob.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label7.Location = new System.Drawing.Point(30, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 17);
            this.label7.TabIndex = 2;
            this.label7.Text = "Mob:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label8.Location = new System.Drawing.Point(22, 381);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 17);
            this.label8.TabIndex = 2;
            this.label8.Text = "Napomena:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label12.Location = new System.Drawing.Point(30, 178);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 17);
            this.label12.TabIndex = 2;
            this.label12.Text = "Županija:";
            // 
            // rtbNapomena
            // 
            this.rtbNapomena.Font = new System.Drawing.Font("Tahoma", 10F);
            this.rtbNapomena.Location = new System.Drawing.Point(25, 401);
            this.rtbNapomena.Multiline = true;
            this.rtbNapomena.Name = "rtbNapomena";
            this.rtbNapomena.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.rtbNapomena.Size = new System.Drawing.Size(395, 114);
            this.rtbNapomena.TabIndex = 17;
            // 
            // txtZupanija
            // 
            this.txtZupanija.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtZupanija.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtZupanija.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtZupanija.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtZupanija.FormattingEnabled = true;
            this.txtZupanija.Location = new System.Drawing.Point(108, 175);
            this.txtZupanija.Name = "txtZupanija";
            this.txtZupanija.Size = new System.Drawing.Size(249, 24);
            this.txtZupanija.TabIndex = 13;
            this.txtZupanija.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtZupanija.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label13.Location = new System.Drawing.Point(30, 203);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(76, 17);
            this.label13.TabIndex = 2;
            this.label13.Text = "Djelatnost:";
            // 
            // txtDjelatnost
            // 
            this.txtDjelatnost.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtDjelatnost.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtDjelatnost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtDjelatnost.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDjelatnost.FormattingEnabled = true;
            this.txtDjelatnost.Location = new System.Drawing.Point(108, 201);
            this.txtDjelatnost.Name = "txtDjelatnost";
            this.txtDjelatnost.Size = new System.Drawing.Size(249, 24);
            this.txtDjelatnost.TabIndex = 14;
            this.txtDjelatnost.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtDjelatnost.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label14.Location = new System.Drawing.Point(22, 24);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(228, 26);
            this.label14.TabIndex = 100;
            this.label14.Text = "Unos novog partnera";
            // 
            // btnSpremi
            // 
            this.btnSpremi.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnSpremi.Location = new System.Drawing.Point(293, 521);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(127, 33);
            this.btnSpremi.TabIndex = 20;
            this.btnSpremi.Text = "Spremi";
            this.btnSpremi.UseVisualStyleBackColor = true;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // btnOdustani
            // 
            this.btnOdustani.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnOdustani.Location = new System.Drawing.Point(165, 521);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(122, 33);
            this.btnOdustani.TabIndex = 19;
            this.btnOdustani.Text = "Odustani";
            this.btnOdustani.UseVisualStyleBackColor = true;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label22.Location = new System.Drawing.Point(30, 152);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(64, 17);
            this.label22.TabIndex = 2;
            this.label22.Text = "Dat.Rođ:";
            // 
            // dtpDatum
            // 
            this.dtpDatum.CalendarFont = new System.Drawing.Font("Tahoma", 12F);
            this.dtpDatum.Location = new System.Drawing.Point(108, 150);
            this.dtpDatum.Name = "dtpDatum";
            this.dtpDatum.Size = new System.Drawing.Size(249, 23);
            this.dtpDatum.TabIndex = 10;
            this.dtpDatum.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.dtpDatum.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::PCPOS.Properties.Resources._1059;
            this.pictureBox1.Location = new System.Drawing.Point(371, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(49, 44);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 109;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(348, 3);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(72, 13);
            this.label18.TabIndex = 110;
            this.label18.Text = "Traži partnera";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.button1.Location = new System.Drawing.Point(25, 521);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(134, 33);
            this.button1.TabIndex = 18;
            this.button1.Text = "Novi unos";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cbGrad);
            this.groupBox2.Controls.Add(this.txtAdresa);
            this.groupBox2.Controls.Add(this.txtSifra);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.txtEmail);
            this.groupBox2.Controls.Add(this.txtIme);
            this.groupBox2.Controls.Add(this.txtTel);
            this.groupBox2.Controls.Add(this.txtPrezime);
            this.groupBox2.Controls.Add(this.txtMob);
            this.groupBox2.Controls.Add(this.dtpDatum);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtDjelatnost);
            this.groupBox2.Controls.Add(this.txtZupanija);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox2.Location = new System.Drawing.Point(25, 69);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(395, 309);
            this.groupBox2.TabIndex = 112;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalji kupca";
            // 
            // cbGrad
            // 
            this.cbGrad.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbGrad.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbGrad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGrad.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cbGrad.FormattingEnabled = true;
            this.cbGrad.Location = new System.Drawing.Point(108, 278);
            this.cbGrad.Name = "cbGrad";
            this.cbGrad.Size = new System.Drawing.Size(250, 24);
            this.cbGrad.TabIndex = 6;
            this.cbGrad.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbGrad.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label5.Location = new System.Drawing.Point(30, 281);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Grad:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label4.Location = new System.Drawing.Point(30, 256);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Adresa:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label15.Location = new System.Drawing.Point(30, 27);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(28, 17);
            this.label15.TabIndex = 2;
            this.label15.Text = "ID:";
            // 
            // txtAdresa
            // 
            this.txtAdresa.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtAdresa.Location = new System.Drawing.Point(108, 252);
            this.txtAdresa.MaxLength = 29;
            this.txtAdresa.Name = "txtAdresa";
            this.txtAdresa.Size = new System.Drawing.Size(250, 24);
            this.txtAdresa.TabIndex = 5;
            this.txtAdresa.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtAdresa.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtSifra
            // 
            this.txtSifra.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtSifra.Location = new System.Drawing.Point(108, 20);
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.ReadOnly = true;
            this.txtSifra.Size = new System.Drawing.Size(90, 24);
            this.txtSifra.TabIndex = 1;
            // 
            // frmAddPartnersKarto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(443, 566);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.rtbNapomena);
            this.Controls.Add(this.label8);
            this.Name = "frmAddPartnersKarto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dodaj novog partnera";
            this.Load += new System.EventHandler(this.frmAddPartnersKarto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIme;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPrezime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMob;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox rtbNapomena;
        private System.Windows.Forms.ComboBox txtZupanija;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox txtDjelatnost;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.DateTimePicker dtpDatum;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbGrad;
        private System.Windows.Forms.TextBox txtAdresa;
        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.Label label15;
    }
}