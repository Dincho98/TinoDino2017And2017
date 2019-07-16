namespace PCPOS.Report.Liste
{
    partial class frmPopisArtikala
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource6 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.dTlisteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRliste = new PCPOS.Dataset.DSRliste();
            this.dTRpodaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRpodaciTvrtke = new PCPOS.Dataset.DSRpodaciTvrtke();
            this.dTlisteTekstBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRlisteTekst = new PCPOS.Dataset.DSRlisteTekst();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.chbRobno = new System.Windows.Forms.CheckBox();
            this.chbUsluge = new System.Windows.Forms.CheckBox();
            this.btnUcitaj = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dTlisteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRliste)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTlisteTekstBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRlisteTekst)).BeginInit();
            this.SuspendLayout();
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
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource4.Name = "DataSet1";
            reportDataSource4.Value = this.dTlisteBindingSource;
            reportDataSource5.Name = "DataSet2";
            reportDataSource5.Value = this.dTRpodaciTvrtkeBindingSource;
            reportDataSource6.Name = "DataSet3";
            reportDataSource6.Value = this.dTlisteTekstBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource5);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource6);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.PopisArtikala.ListaArtikala.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 57);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(806, 735);
            this.reportViewer1.TabIndex = 0;
            // 
            // chbRobno
            // 
            this.chbRobno.AutoSize = true;
            this.chbRobno.Location = new System.Drawing.Point(12, 11);
            this.chbRobno.Name = "chbRobno";
            this.chbRobno.Size = new System.Drawing.Size(58, 17);
            this.chbRobno.TabIndex = 2;
            this.chbRobno.Text = "Robno";
            this.chbRobno.UseVisualStyleBackColor = true;
            // 
            // chbUsluge
            // 
            this.chbUsluge.AutoSize = true;
            this.chbUsluge.Location = new System.Drawing.Point(12, 34);
            this.chbUsluge.Name = "chbUsluge";
            this.chbUsluge.Size = new System.Drawing.Size(59, 17);
            this.chbUsluge.TabIndex = 3;
            this.chbUsluge.Text = "Usluge";
            this.chbUsluge.UseVisualStyleBackColor = true;
            // 
            // btnUcitaj
            // 
            this.btnUcitaj.Location = new System.Drawing.Point(86, 10);
            this.btnUcitaj.Name = "btnUcitaj";
            this.btnUcitaj.Size = new System.Drawing.Size(75, 41);
            this.btnUcitaj.TabIndex = 4;
            this.btnUcitaj.Text = "Učitaj";
            this.btnUcitaj.UseVisualStyleBackColor = true;
            this.btnUcitaj.Click += new System.EventHandler(this.btnUcitaj_Click);
            // 
            // frmPopisArtikala
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(806, 792);
            this.Controls.Add(this.btnUcitaj);
            this.Controls.Add(this.chbUsluge);
            this.Controls.Add(this.chbRobno);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmPopisArtikala";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista";
            this.Load += new System.EventHandler(this.frmListe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dTlisteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRliste)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTlisteTekstBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRlisteTekst)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
		private Dataset.DSRlisteTekst dSRlisteTekst;
        private System.Windows.Forms.BindingSource dTlisteTekstBindingSource;
		private Dataset.DSRliste dSRliste;
        private System.Windows.Forms.BindingSource dTlisteBindingSource;
		private Dataset.DSRpodaciTvrtke dSRpodaciTvrtke;
        private System.Windows.Forms.BindingSource dTRpodaciTvrtkeBindingSource;
        private System.Windows.Forms.CheckBox chbRobno;
        private System.Windows.Forms.CheckBox chbUsluge;
        private System.Windows.Forms.Button btnUcitaj;
    }
}