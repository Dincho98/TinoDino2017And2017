namespace PCPOS.Proizvodnja
{
    partial class frmKarticaRobeNaTudemSkladistu
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
            this.lblPoslovnice = new System.Windows.Forms.Label();
            this.cmbPoslovnice = new System.Windows.Forms.ComboBox();
            this.lblPartner = new System.Windows.Forms.Label();
            this.txtPartnerId = new System.Windows.Forms.TextBox();
            this.btnPartnerSrch = new System.Windows.Forms.Button();
            this.lblDatumOd = new System.Windows.Forms.Label();
            this.dtpDatumOd = new System.Windows.Forms.DateTimePicker();
            this.dtpDatumDo = new System.Windows.Forms.DateTimePicker();
            this.lblDatumDo = new System.Windows.Forms.Label();
            this.btnPrikazi = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.lblPartnerNaziv = new System.Windows.Forms.Label();
            this.cmbSkladiste = new System.Windows.Forms.ComboBox();
            this.lblSkladiste = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPoslovnice
            // 
            this.lblPoslovnice.AutoSize = true;
            this.lblPoslovnice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblPoslovnice.Location = new System.Drawing.Point(13, 48);
            this.lblPoslovnice.Name = "lblPoslovnice";
            this.lblPoslovnice.Size = new System.Drawing.Size(88, 20);
            this.lblPoslovnice.TabIndex = 0;
            this.lblPoslovnice.Text = "Poslovnica:";
            // 
            // cmbPoslovnice
            // 
            this.cmbPoslovnice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPoslovnice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cmbPoslovnice.FormattingEnabled = true;
            this.cmbPoslovnice.Location = new System.Drawing.Point(108, 44);
            this.cmbPoslovnice.Name = "cmbPoslovnice";
            this.cmbPoslovnice.Size = new System.Drawing.Size(146, 28);
            this.cmbPoslovnice.TabIndex = 1;
            // 
            // lblPartner
            // 
            this.lblPartner.AutoSize = true;
            this.lblPartner.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblPartner.Location = new System.Drawing.Point(13, 15);
            this.lblPartner.Name = "lblPartner";
            this.lblPartner.Size = new System.Drawing.Size(65, 20);
            this.lblPartner.TabIndex = 2;
            this.lblPartner.Text = "Partner:";
            // 
            // txtPartnerId
            // 
            this.txtPartnerId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtPartnerId.Location = new System.Drawing.Point(108, 12);
            this.txtPartnerId.Name = "txtPartnerId";
            this.txtPartnerId.Size = new System.Drawing.Size(114, 26);
            this.txtPartnerId.TabIndex = 3;
            this.txtPartnerId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartnerId_KeyDown);
            // 
            // btnPartnerSrch
            // 
            this.btnPartnerSrch.BackgroundImage = global::PCPOS.Properties.Resources._1059;
            this.btnPartnerSrch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPartnerSrch.FlatAppearance.BorderSize = 0;
            this.btnPartnerSrch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPartnerSrch.Location = new System.Drawing.Point(228, 12);
            this.btnPartnerSrch.Name = "btnPartnerSrch";
            this.btnPartnerSrch.Size = new System.Drawing.Size(26, 26);
            this.btnPartnerSrch.TabIndex = 4;
            this.btnPartnerSrch.UseVisualStyleBackColor = true;
            this.btnPartnerSrch.Click += new System.EventHandler(this.btnPartnerSrch_Click);
            // 
            // lblDatumOd
            // 
            this.lblDatumOd.AutoSize = true;
            this.lblDatumOd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblDatumOd.Location = new System.Drawing.Point(13, 81);
            this.lblDatumOd.Name = "lblDatumOd";
            this.lblDatumOd.Size = new System.Drawing.Size(83, 20);
            this.lblDatumOd.TabIndex = 6;
            this.lblDatumOd.Text = "Datum od:";
            // 
            // dtpDatumOd
            // 
            this.dtpDatumOd.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dtpDatumOd.CustomFormat = "dd.MM.yyyy.";
            this.dtpDatumOd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dtpDatumOd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatumOd.Location = new System.Drawing.Point(108, 78);
            this.dtpDatumOd.Name = "dtpDatumOd";
            this.dtpDatumOd.Size = new System.Drawing.Size(146, 26);
            this.dtpDatumOd.TabIndex = 7;
            // 
            // dtpDatumDo
            // 
            this.dtpDatumDo.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dtpDatumDo.CustomFormat = "dd.MM.yyyy.";
            this.dtpDatumDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dtpDatumDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatumDo.Location = new System.Drawing.Point(108, 110);
            this.dtpDatumDo.Name = "dtpDatumDo";
            this.dtpDatumDo.Size = new System.Drawing.Size(146, 26);
            this.dtpDatumDo.TabIndex = 9;
            // 
            // lblDatumDo
            // 
            this.lblDatumDo.AutoSize = true;
            this.lblDatumDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblDatumDo.Location = new System.Drawing.Point(13, 113);
            this.lblDatumDo.Name = "lblDatumDo";
            this.lblDatumDo.Size = new System.Drawing.Size(83, 20);
            this.lblDatumDo.TabIndex = 8;
            this.lblDatumDo.Text = "Datum do:";
            // 
            // btnPrikazi
            // 
            this.btnPrikazi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnPrikazi.Location = new System.Drawing.Point(796, 12);
            this.btnPrikazi.Name = "btnPrikazi";
            this.btnPrikazi.Size = new System.Drawing.Size(91, 61);
            this.btnPrikazi.TabIndex = 10;
            this.btnPrikazi.Text = "Prikaži";
            this.btnPrikazi.UseVisualStyleBackColor = true;
            this.btnPrikazi.Click += new System.EventHandler(this.btnPrikazi_Click);
            // 
            // btnOdustani
            // 
            this.btnOdustani.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnOdustani.Location = new System.Drawing.Point(893, 12);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(91, 61);
            this.btnOdustani.TabIndex = 11;
            this.btnOdustani.Text = "Odustani";
            this.btnOdustani.UseVisualStyleBackColor = true;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            // 
            // lblPartnerNaziv
            // 
            this.lblPartnerNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblPartnerNaziv.Location = new System.Drawing.Point(260, 15);
            this.lblPartnerNaziv.Name = "lblPartnerNaziv";
            this.lblPartnerNaziv.Size = new System.Drawing.Size(530, 20);
            this.lblPartnerNaziv.TabIndex = 12;
            this.lblPartnerNaziv.Text = "Naziv partnera";
            // 
            // cmbSkladiste
            // 
            this.cmbSkladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSkladiste.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cmbSkladiste.FormattingEnabled = true;
            this.cmbSkladiste.Location = new System.Drawing.Point(383, 44);
            this.cmbSkladiste.Name = "cmbSkladiste";
            this.cmbSkladiste.Size = new System.Drawing.Size(250, 28);
            this.cmbSkladiste.TabIndex = 14;
            // 
            // lblSkladiste
            // 
            this.lblSkladiste.AutoSize = true;
            this.lblSkladiste.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblSkladiste.Location = new System.Drawing.Point(288, 48);
            this.lblSkladiste.Name = "lblSkladiste";
            this.lblSkladiste.Size = new System.Drawing.Size(78, 20);
            this.lblSkladiste.TabIndex = 13;
            this.lblSkladiste.Text = "Skladište:";
            // 
            // frmKarticaRobeNaTudemSkladistu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 483);
            this.Controls.Add(this.cmbSkladiste);
            this.Controls.Add(this.lblSkladiste);
            this.Controls.Add(this.lblPartnerNaziv);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnPrikazi);
            this.Controls.Add(this.dtpDatumDo);
            this.Controls.Add(this.lblDatumDo);
            this.Controls.Add(this.dtpDatumOd);
            this.Controls.Add(this.lblDatumOd);
            this.Controls.Add(this.btnPartnerSrch);
            this.Controls.Add(this.txtPartnerId);
            this.Controls.Add(this.lblPartner);
            this.Controls.Add(this.cmbPoslovnice);
            this.Controls.Add(this.lblPoslovnice);
            this.Name = "frmKarticaRobeNaTudemSkladistu";
            this.Text = "Kartica robe na tuđem skladištu";
            this.Load += new System.EventHandler(this.frmKarticaRobeNaTudemSkladistu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPoslovnice;
        private System.Windows.Forms.ComboBox cmbPoslovnice;
        private System.Windows.Forms.Label lblPartner;
        private System.Windows.Forms.TextBox txtPartnerId;
        private System.Windows.Forms.Button btnPartnerSrch;
        private System.Windows.Forms.Label lblDatumOd;
        private System.Windows.Forms.DateTimePicker dtpDatumOd;
        private System.Windows.Forms.DateTimePicker dtpDatumDo;
        private System.Windows.Forms.Label lblDatumDo;
        private System.Windows.Forms.Button btnPrikazi;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Label lblPartnerNaziv;
        private System.Windows.Forms.ComboBox cmbSkladiste;
        private System.Windows.Forms.Label lblSkladiste;
    }
}