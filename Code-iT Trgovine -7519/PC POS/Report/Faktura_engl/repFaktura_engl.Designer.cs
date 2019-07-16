namespace PCPOS.Report.Faktura_engl
{
    partial class repFaktura_engl
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.dTRfakturaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSFaktura = new PCPOS.Dataset.DSFaktura();
            this.dTRpodaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRpodaciTvrtke = new PCPOS.Dataset.DSRpodaciTvrtke();
            this.dTfakturaStavkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRfakturaStavke = new PCPOS.Dataset.DSRfakturaStavke();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dSstope = new PCPOS.Dataset.DSstope();
            this.dTstopeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dTRfakturaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSFaktura)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTfakturaStavkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRfakturaStavke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSstope)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTstopeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dTRfakturaBindingSource
            // 
            this.dTRfakturaBindingSource.DataMember = "DTRfaktura";
            this.dTRfakturaBindingSource.DataSource = this.dSFaktura;
            // 
            // dSFaktura
            // 
            this.dSFaktura.DataSetName = "DSFaktura";
            this.dSFaktura.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            // dTfakturaStavkeBindingSource
            // 
            this.dTfakturaStavkeBindingSource.DataMember = "DTfakturaStavke";
            this.dTfakturaStavkeBindingSource.DataSource = this.dSRfakturaStavke;
            // 
            // dSRfakturaStavke
            // 
            this.dSRfakturaStavke.DataSetName = "DSRfakturaStavke";
            this.dSRfakturaStavke.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.DocumentMapWidth = 60;
            reportDataSource1.Name = "DSKaktura";
            reportDataSource1.Value = this.dTRfakturaBindingSource;
            reportDataSource2.Name = "DSfakturaStavke";
            reportDataSource2.Value = this.dTfakturaStavkeBindingSource;
            reportDataSource3.Name = "DSPodaciTvrtke";
            reportDataSource3.Value = this.dTRpodaciTvrtkeBindingSource;
            reportDataSource4.Name = "DSstope";
            reportDataSource4.Value = this.dTstopeBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Faktura_engl.Report.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(836, 775);
            this.reportViewer1.TabIndex = 5;
            // 
            // dSstope
            // 
            this.dSstope.DataSetName = "DSstope";
            this.dSstope.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dTstopeBindingSource
            // 
            this.dTstopeBindingSource.DataMember = "DTstope";
            this.dTstopeBindingSource.DataSource = this.dSstope;
            // 
            // repFaktura_engl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 775);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "repFaktura_engl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Faktura ";
            this.Load += new System.EventHandler(this.repFaktura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dTRfakturaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSFaktura)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTfakturaStavkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRfakturaStavke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSstope)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTstopeBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

		public Dataset.DSFaktura dSFaktura;
		public System.Windows.Forms.BindingSource dTRfakturaBindingSource;
		public Dataset.DSRfakturaStavke dSRfakturaStavke;
		public System.Windows.Forms.BindingSource dTfakturaStavkeBindingSource;
		public Dataset.DSRpodaciTvrtke dSRpodaciTvrtke;
		public System.Windows.Forms.BindingSource dTRpodaciTvrtkeBindingSource;
		public Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Dataset.DSstope dSstope;
        private System.Windows.Forms.BindingSource dTstopeBindingSource;

    }
}