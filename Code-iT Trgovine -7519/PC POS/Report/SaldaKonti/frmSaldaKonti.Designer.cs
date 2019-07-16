namespace PCPOS.Report.SaldaKonti
{
    partial class frmSaldaKonti
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSaldaKonti));
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.dTRpodaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRpodaciTvrtke = new PCPOS.Dataset.DSRpodaciTvrtke();
            this.dTlisteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRliste = new PCPOS.Dataset.DSRliste();
            this.dTlisteTekstBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRlisteTekst = new PCPOS.Dataset.DSRlisteTekst();
            this.dTsaldaKontiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSsaldaKonti = new PCPOS.Dataset.DSsaldaKonti();
            this.label3 = new System.Windows.Forms.Label();
            this.tdDoDatuma = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.tdOdDatuma = new System.Windows.Forms.DateTimePicker();
            this.btnTrazi = new System.Windows.Forms.Button();
            this.txtNaziv = new System.Windows.Forms.TextBox();
            this.btnPartner = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.checkBoxPlaceni = new System.Windows.Forms.CheckBox();
            this.checkBoxDjelomicnoPlaceni = new System.Windows.Forms.CheckBox();
            this.checkBoxNeplaceni = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxNesvrstani = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTlisteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRliste)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTlisteTekstBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRlisteTekst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTsaldaKontiBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSsaldaKonti)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPartner)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dTRpodaciTvrtkeBindingSource
            // 
            this.dTRpodaciTvrtkeBindingSource.DataMember = "DTRpodaciTvrtke";
            this.dTRpodaciTvrtkeBindingSource.DataSource = this.dSRpodaciTvrtke;
            // 
            // dSRpodaciTvrtke
            // 
            this.dSRpodaciTvrtke.DataSetName = "DSRpodaciTvrtke";
            this.dSRpodaciTvrtke.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dTlisteBindingSource
            // 
            this.dTlisteBindingSource.DataMember = "DTliste";
            this.dTlisteBindingSource.DataSource = this.dSRliste;
            // 
            // dSRliste
            // 
            this.dSRliste.DataSetName = "DSRliste";
            this.dSRliste.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dTlisteTekstBindingSource
            // 
            this.dTlisteTekstBindingSource.DataMember = "DTlisteTekst";
            this.dTlisteTekstBindingSource.DataSource = this.dSRlisteTekst;
            // 
            // dSRlisteTekst
            // 
            this.dSRlisteTekst.DataSetName = "DSRlisteTekst";
            this.dSRlisteTekst.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dTsaldaKontiBindingSource
            // 
            this.dTsaldaKontiBindingSource.DataMember = "DTsaldaKonti";
            this.dTsaldaKontiBindingSource.DataSource = this.dSsaldaKonti;
            // 
            // dSsaldaKonti
            // 
            this.dSsaldaKonti.DataSetName = "DSsaldaKonti";
            this.dSsaldaKonti.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(289, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 575;
            this.label3.Text = "Do datuma:";
            // 
            // tdDoDatuma
            // 
            this.tdDoDatuma.CustomFormat = "dd.MM.yyyy H:mm";
            this.tdDoDatuma.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tdDoDatuma.Location = new System.Drawing.Point(357, 42);
            this.tdDoDatuma.Name = "tdDoDatuma";
            this.tdDoDatuma.Size = new System.Drawing.Size(177, 20);
            this.tdDoDatuma.TabIndex = 574;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 573;
            this.label2.Text = "Od datuma:";
            // 
            // tdOdDatuma
            // 
            this.tdOdDatuma.CustomFormat = "dd.MM.yyyy H:mm";
            this.tdOdDatuma.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tdOdDatuma.Location = new System.Drawing.Point(85, 41);
            this.tdOdDatuma.Name = "tdOdDatuma";
            this.tdOdDatuma.Size = new System.Drawing.Size(167, 20);
            this.tdOdDatuma.TabIndex = 572;
            // 
            // btnTrazi
            // 
            this.btnTrazi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnTrazi.Image = global::PCPOS.Properties.Resources._10591;
            this.btnTrazi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTrazi.Location = new System.Drawing.Point(540, 11);
            this.btnTrazi.Name = "btnTrazi";
            this.btnTrazi.Size = new System.Drawing.Size(96, 54);
            this.btnTrazi.TabIndex = 571;
            this.btnTrazi.Text = "       Traži";
            this.btnTrazi.UseVisualStyleBackColor = true;
            this.btnTrazi.Click += new System.EventHandler(this.btnTrazi_Click);
            // 
            // txtNaziv
            // 
            this.txtNaziv.Location = new System.Drawing.Point(185, 12);
            this.txtNaziv.Name = "txtNaziv";
            this.txtNaziv.Size = new System.Drawing.Size(349, 20);
            this.txtNaziv.TabIndex = 570;
            // 
            // btnPartner
            // 
            this.btnPartner.BackColor = System.Drawing.Color.Transparent;
            this.btnPartner.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPartner.Image = ((System.Drawing.Image)(resources.GetObject("btnPartner.Image")));
            this.btnPartner.Location = new System.Drawing.Point(152, 9);
            this.btnPartner.Name = "btnPartner";
            this.btnPartner.Size = new System.Drawing.Size(31, 28);
            this.btnPartner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnPartner.TabIndex = 569;
            this.btnPartner.TabStop = false;
            this.btnPartner.Click += new System.EventHandler(this.btnPartner_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 568;
            this.label1.Text = "Šifra partnera:";
            // 
            // txtSifra
            // 
            this.txtSifra.Location = new System.Drawing.Point(85, 12);
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(66, 20);
            this.txtSifra.TabIndex = 567;
            this.txtSifra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifra_KeyDown);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "DSpodaciTVR";
            reportDataSource1.Value = this.dTRpodaciTvrtkeBindingSource;
            reportDataSource2.Name = "DSzaTable";
            reportDataSource2.Value = this.dTlisteBindingSource;
            reportDataSource3.Name = "DStext";
            reportDataSource3.Value = this.dTlisteTekstBindingSource;
            reportDataSource4.Name = "DSsaldaKonti";
            reportDataSource4.Value = this.dTsaldaKontiBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.SaldaKonti.saldakonti.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(5, 120);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(634, 595);
            this.reportViewer1.TabIndex = 576;
            // 
            // checkBoxPlaceni
            // 
            this.checkBoxPlaceni.AutoSize = true;
            this.checkBoxPlaceni.Location = new System.Drawing.Point(29, 16);
            this.checkBoxPlaceni.Name = "checkBoxPlaceni";
            this.checkBoxPlaceni.Size = new System.Drawing.Size(61, 17);
            this.checkBoxPlaceni.TabIndex = 577;
            this.checkBoxPlaceni.Text = "Plačeni";
            this.checkBoxPlaceni.UseVisualStyleBackColor = true;
            this.checkBoxPlaceni.CheckedChanged += new System.EventHandler(this.checkBoxPlaceni_CheckedChanged);
            // 
            // checkBoxDjelomicnoPlaceni
            // 
            this.checkBoxDjelomicnoPlaceni.AutoSize = true;
            this.checkBoxDjelomicnoPlaceni.Location = new System.Drawing.Point(125, 16);
            this.checkBoxDjelomicnoPlaceni.Name = "checkBoxDjelomicnoPlaceni";
            this.checkBoxDjelomicnoPlaceni.Size = new System.Drawing.Size(115, 17);
            this.checkBoxDjelomicnoPlaceni.TabIndex = 578;
            this.checkBoxDjelomicnoPlaceni.Text = "Djelomično plačeni";
            this.checkBoxDjelomicnoPlaceni.UseVisualStyleBackColor = true;
            this.checkBoxDjelomicnoPlaceni.CheckedChanged += new System.EventHandler(this.checkBoxDjelomicnoPlaceni_CheckedChanged);
            // 
            // checkBoxNeplaceni
            // 
            this.checkBoxNeplaceni.AutoSize = true;
            this.checkBoxNeplaceni.Location = new System.Drawing.Point(286, 16);
            this.checkBoxNeplaceni.Name = "checkBoxNeplaceni";
            this.checkBoxNeplaceni.Size = new System.Drawing.Size(74, 17);
            this.checkBoxNeplaceni.TabIndex = 579;
            this.checkBoxNeplaceni.Text = "Neplačeni";
            this.checkBoxNeplaceni.UseVisualStyleBackColor = true;
            this.checkBoxNeplaceni.CheckedChanged += new System.EventHandler(this.checkBoxNeplaceni_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxNesvrstani);
            this.groupBox1.Controls.Add(this.checkBoxNeplaceni);
            this.groupBox1.Controls.Add(this.checkBoxDjelomicnoPlaceni);
            this.groupBox1.Controls.Add(this.checkBoxPlaceni);
            this.groupBox1.Location = new System.Drawing.Point(12, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(522, 39);
            this.groupBox1.TabIndex = 580;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter:";
            // 
            // checkBoxNesvrstani
            // 
            this.checkBoxNesvrstani.AutoSize = true;
            this.checkBoxNesvrstani.Location = new System.Drawing.Point(403, 16);
            this.checkBoxNesvrstani.Name = "checkBoxNesvrstani";
            this.checkBoxNesvrstani.Size = new System.Drawing.Size(77, 17);
            this.checkBoxNesvrstani.TabIndex = 580;
            this.checkBoxNesvrstani.Text = "Kalkulacije";
            this.checkBoxNesvrstani.UseVisualStyleBackColor = true;
            this.checkBoxNesvrstani.CheckedChanged += new System.EventHandler(this.checkBoxNesvrstani_CheckedChanged);
            // 
            // frmSaldaKonti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(645, 721);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tdDoDatuma);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tdOdDatuma);
            this.Controls.Add(this.btnTrazi);
            this.Controls.Add(this.txtNaziv);
            this.Controls.Add(this.btnPartner);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSifra);
            this.Name = "frmSaldaKonti";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Otvorene stavke";
            this.Load += new System.EventHandler(this.frmListe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTlisteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRliste)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTlisteTekstBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRlisteTekst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTsaldaKontiBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSsaldaKonti)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPartner)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Dataset.DSRpodaciTvrtke dSRpodaciTvrtke;
        private System.Windows.Forms.BindingSource dTRpodaciTvrtkeBindingSource;
		private Dataset.DSRlisteTekst dSRlisteTekst;
        private System.Windows.Forms.BindingSource dTlisteTekstBindingSource;
		private Dataset.DSRliste dSRliste;
        private System.Windows.Forms.BindingSource dTlisteBindingSource;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker tdDoDatuma;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker tdOdDatuma;
        private System.Windows.Forms.Button btnTrazi;
        private System.Windows.Forms.TextBox txtNaziv;
        private System.Windows.Forms.PictureBox btnPartner;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSifra;
        private Dataset.DSsaldaKonti dSsaldaKonti;
        private System.Windows.Forms.BindingSource dTsaldaKontiBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.CheckBox checkBoxPlaceni;
        private System.Windows.Forms.CheckBox checkBoxDjelomicnoPlaceni;
        private System.Windows.Forms.CheckBox checkBoxNeplaceni;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxNesvrstani;
    }
}