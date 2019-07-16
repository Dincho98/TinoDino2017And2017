namespace PCPOS.Report.Komisija
{
    partial class frmRobaNaKomisiji
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRobaNaKomisiji));
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.chbZbirno = new System.Windows.Forms.CheckBox();
            this.btnPonistiPartnera = new System.Windows.Forms.Button();
            this.txtPartnerNaziv = new System.Windows.Forms.TextBox();
            this.btnPartner = new System.Windows.Forms.PictureBox();
            this.txtPartnerSifra = new System.Windows.Forms.TextBox();
            this.lblSifraPartnera = new System.Windows.Forms.Label();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnTrazi = new System.Windows.Forms.Button();
            this.dtpDatumOd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDatumDo = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dSRpodaciTvrtke = new PCPOS.Dataset.DSRpodaciTvrtke();
            this.dSRpodaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRliste = new PCPOS.Dataset.DSRliste();
            this.dSRlisteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.btnPartner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRliste)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRlisteBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // chbZbirno
            // 
            this.chbZbirno.AutoSize = true;
            this.chbZbirno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chbZbirno.Location = new System.Drawing.Point(580, 13);
            this.chbZbirno.Name = "chbZbirno";
            this.chbZbirno.Size = new System.Drawing.Size(68, 21);
            this.chbZbirno.TabIndex = 582;
            this.chbZbirno.Text = "Zbirno";
            this.chbZbirno.UseVisualStyleBackColor = true;
            this.chbZbirno.CheckedChanged += new System.EventHandler(this.chbZbirno_CheckedChanged);
            // 
            // btnPonistiPartnera
            // 
            this.btnPonistiPartnera.Location = new System.Drawing.Point(548, 11);
            this.btnPonistiPartnera.Name = "btnPonistiPartnera";
            this.btnPonistiPartnera.Size = new System.Drawing.Size(25, 25);
            this.btnPonistiPartnera.TabIndex = 581;
            this.btnPonistiPartnera.Text = "X";
            this.btnPonistiPartnera.UseVisualStyleBackColor = true;
            this.btnPonistiPartnera.Click += new System.EventHandler(this.btnPonistiPartnera_Click);
            // 
            // txtPartnerNaziv
            // 
            this.txtPartnerNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPartnerNaziv.Location = new System.Drawing.Point(256, 12);
            this.txtPartnerNaziv.Name = "txtPartnerNaziv";
            this.txtPartnerNaziv.ReadOnly = true;
            this.txtPartnerNaziv.Size = new System.Drawing.Size(286, 23);
            this.txtPartnerNaziv.TabIndex = 579;
            // 
            // btnPartner
            // 
            this.btnPartner.BackColor = System.Drawing.Color.Transparent;
            this.btnPartner.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPartner.Image = ((System.Drawing.Image)(resources.GetObject("btnPartner.Image")));
            this.btnPartner.Location = new System.Drawing.Point(220, 8);
            this.btnPartner.Name = "btnPartner";
            this.btnPartner.Size = new System.Drawing.Size(30, 30);
            this.btnPartner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnPartner.TabIndex = 580;
            this.btnPartner.TabStop = false;
            this.btnPartner.Click += new System.EventHandler(this.btnPartner_Click);
            // 
            // txtPartnerSifra
            // 
            this.txtPartnerSifra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPartnerSifra.Location = new System.Drawing.Point(117, 12);
            this.txtPartnerSifra.Name = "txtPartnerSifra";
            this.txtPartnerSifra.Size = new System.Drawing.Size(97, 23);
            this.txtPartnerSifra.TabIndex = 577;
            this.txtPartnerSifra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartnerSifra_KeyDown);
            // 
            // lblSifraPartnera
            // 
            this.lblSifraPartnera.AutoSize = true;
            this.lblSifraPartnera.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblSifraPartnera.Location = new System.Drawing.Point(12, 15);
            this.lblSifraPartnera.Name = "lblSifraPartnera";
            this.lblSifraPartnera.Size = new System.Drawing.Size(99, 17);
            this.lblSifraPartnera.TabIndex = 578;
            this.lblSifraPartnera.Text = "Šifra partnera:";
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "DSpodaciTVR";
            reportDataSource1.Value = this.dSRpodaciTvrtkeBindingSource;
            reportDataSource2.Name = "DSRliste";
            reportDataSource2.Value = this.dSRlisteBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Komisija.RobaNaKomisiji.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(5, 72);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(766, 735);
            this.reportViewer1.TabIndex = 576;
            // 
            // btnTrazi
            // 
            this.btnTrazi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnTrazi.Image = global::PCPOS.Properties.Resources._10591;
            this.btnTrazi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTrazi.Location = new System.Drawing.Point(675, 12);
            this.btnTrazi.Name = "btnTrazi";
            this.btnTrazi.Size = new System.Drawing.Size(96, 54);
            this.btnTrazi.TabIndex = 571;
            this.btnTrazi.Text = "       Traži";
            this.btnTrazi.UseVisualStyleBackColor = true;
            this.btnTrazi.Click += new System.EventHandler(this.btnTrazi_Click);
            // 
            // dtpDatumOd
            // 
            this.dtpDatumOd.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dtpDatumOd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDatumOd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatumOd.Location = new System.Drawing.Point(117, 41);
            this.dtpDatumOd.Name = "dtpDatumOd";
            this.dtpDatumOd.Size = new System.Drawing.Size(167, 23);
            this.dtpDatumOd.TabIndex = 572;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 17);
            this.label2.TabIndex = 573;
            this.label2.Text = "Od datuma:";
            // 
            // dtpDatumDo
            // 
            this.dtpDatumDo.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dtpDatumDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDatumDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatumDo.Location = new System.Drawing.Point(396, 41);
            this.dtpDatumDo.Name = "dtpDatumDo";
            this.dtpDatumDo.Size = new System.Drawing.Size(177, 23);
            this.dtpDatumDo.TabIndex = 574;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(309, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 17);
            this.label3.TabIndex = 575;
            this.label3.Text = "Do datuma:";
            // 
            // dSRpodaciTvrtke
            // 
            this.dSRpodaciTvrtke.DataSetName = "DSRpodaciTvrtke";
            this.dSRpodaciTvrtke.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dSRpodaciTvrtkeBindingSource
            // 
            this.dSRpodaciTvrtkeBindingSource.DataMember = "DTRpodaciTvrtke";
            this.dSRpodaciTvrtkeBindingSource.DataSource = this.dSRpodaciTvrtke;
            // 
            // dSRliste
            // 
            this.dSRliste.DataSetName = "DSRliste";
            this.dSRliste.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dSRlisteBindingSource
            // 
            this.dSRlisteBindingSource.DataMember = "DTliste";
            this.dSRlisteBindingSource.DataSource = this.dSRliste;
            // 
            // frmRobaNaKomisiji
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(776, 812);
            this.Controls.Add(this.chbZbirno);
            this.Controls.Add(this.btnPonistiPartnera);
            this.Controls.Add(this.btnPartner);
            this.Controls.Add(this.txtPartnerNaziv);
            this.Controls.Add(this.lblSifraPartnera);
            this.Controls.Add(this.txtPartnerSifra);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpDatumDo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpDatumOd);
            this.Controls.Add(this.btnTrazi);
            this.Name = "frmRobaNaKomisiji";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Popis prodane robe";
            this.Load += new System.EventHandler(this.frmListe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnPartner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRliste)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRlisteBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbZbirno;
        private System.Windows.Forms.Button btnPonistiPartnera;
        private System.Windows.Forms.TextBox txtPartnerNaziv;
        private System.Windows.Forms.PictureBox btnPartner;
        private System.Windows.Forms.TextBox txtPartnerSifra;
        private System.Windows.Forms.Label lblSifraPartnera;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Button btnTrazi;
        private System.Windows.Forms.DateTimePicker dtpDatumOd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDatumDo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.BindingSource dSRpodaciTvrtkeBindingSource;
        private Dataset.DSRpodaciTvrtke dSRpodaciTvrtke;
        private System.Windows.Forms.BindingSource dSRlisteBindingSource;
        private Dataset.DSRliste dSRliste;
    }
}