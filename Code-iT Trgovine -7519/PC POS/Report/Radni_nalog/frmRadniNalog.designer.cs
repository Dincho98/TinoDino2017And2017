namespace PCPOS.Report.Radni_nalog
{
	partial class repRadniNalog
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
			this.dTRpodaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dSRpodaciTvrtke = new PCPOS.Dataset.DSRpodaciTvrtke();
			this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
			this.dSRadniNalogStavke = new PCPOS.Dataset.DSRadniNalogStavke();
			this.dTRadniNalogStavkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dTRRadniNalogBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dSRadniNalog = new PCPOS.Dataset.DSRadniNalog();
			((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dSRadniNalogStavke)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dTRadniNalogStavkeBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dTRRadniNalogBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dSRadniNalog)).BeginInit();
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
			// reportViewer1
			// 
			this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			reportDataSource1.Name = "dSRadniNalog";
			reportDataSource1.Value = this.dTRRadniNalogBindingSource;
			reportDataSource2.Name = "dSRadniNalogStavke";
			reportDataSource2.Value = this.dTRadniNalogStavkeBindingSource;
			reportDataSource3.Name = "dSPodaciTvrtke";
			reportDataSource3.Value = this.dTRpodaciTvrtkeBindingSource;
			this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
			this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
			this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
			this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Radni_nalog.radni_nalog.rdlc";
			this.reportViewer1.Location = new System.Drawing.Point(-1, -1);
			this.reportViewer1.Name = "reportViewer1";
			this.reportViewer1.Size = new System.Drawing.Size(956, 494);
			this.reportViewer1.TabIndex = 5;
			// 
			// dSRadniNalogStavke
			// 
			this.dSRadniNalogStavke.DataSetName = "DSRadniNalogStavke";
			this.dSRadniNalogStavke.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// dTRadniNalogStavkeBindingSource
			// 
			this.dTRadniNalogStavkeBindingSource.DataMember = "DTRadniNalogStavke";
			this.dTRadniNalogStavkeBindingSource.DataSource = this.dSRadniNalogStavke;
			// 
			// dTRRadniNalogBindingSource
			// 
			this.dTRRadniNalogBindingSource.DataMember = "DTRRadniNalog";
			this.dTRRadniNalogBindingSource.DataSource = this.dSRadniNalog;
			// 
			// dSRadniNalog
			// 
			this.dSRadniNalog.DataSetName = "DSRadniNalog";
			this.dSRadniNalog.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// repRadniNalog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(953, 491);
			this.Controls.Add(this.reportViewer1);
			this.Name = "repRadniNalog";
			this.Text = "Radni nalog";
			this.Load += new System.EventHandler(this.repRadniNalog_Load);
			((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dSRadniNalogStavke)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dTRadniNalogStavkeBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dTRRadniNalogBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dSRadniNalog)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private Dataset.DSRpodaciTvrtke dSRpodaciTvrtke;
		private System.Windows.Forms.BindingSource dTRpodaciTvrtkeBindingSource;
		private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
		private Dataset.DSRadniNalogStavke dSRadniNalogStavke;
		private System.Windows.Forms.BindingSource dTRadniNalogStavkeBindingSource;
		private Dataset.DSRadniNalog dSRadniNalog;
		private System.Windows.Forms.BindingSource dTRRadniNalogBindingSource;
	}
}