namespace PCPOS.Report.Robno
{
	partial class repPrimka
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
			this.dTRPrimkaBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dSPrimka = new PCPOS.Dataset.DSPrimka();
			this.dTPrimkaStavkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dSPrimkaStavke = new PCPOS.Dataset.DSPrimkaStavke();
			this.dTRpodaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dSRpodaciTvrtke = new PCPOS.Dataset.DSRpodaciTvrtke();
			this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
			((System.ComponentModel.ISupportInitialize)(this.dTRPrimkaBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dSPrimka)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dTPrimkaStavkeBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dSPrimkaStavke)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
			this.SuspendLayout();
			// 
			// dTRPrimkaBindingSource
			// 
			this.dTRPrimkaBindingSource.DataMember = "DTRPrimka";
			this.dTRPrimkaBindingSource.DataSource = this.dSPrimka;
			// 
			// dSPrimka
			// 
			this.dSPrimka.DataSetName = "DSPrimka";
			this.dSPrimka.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// dTPrimkaStavkeBindingSource
			// 
			this.dTPrimkaStavkeBindingSource.DataMember = "DTPrimkaStavke";
			this.dTPrimkaStavkeBindingSource.DataSource = this.dSPrimkaStavke;
			// 
			// dSPrimkaStavke
			// 
			this.dSPrimkaStavke.DataSetName = "DSPrimkaStavke";
			this.dSPrimkaStavke.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
			reportDataSource1.Name = "dSPrimka";
			reportDataSource1.Value = this.dTRPrimkaBindingSource;
			reportDataSource2.Name = "dSPrimkaStavke";
			reportDataSource2.Value = this.dTPrimkaStavkeBindingSource;
			reportDataSource3.Name = "dSPodaciTvrtke";
			reportDataSource3.Value = this.dTRpodaciTvrtkeBindingSource;
			this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
			this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
			this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
			this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Robno.primka.rdlc";
			this.reportViewer1.Location = new System.Drawing.Point(-1, -1);
			this.reportViewer1.Name = "reportViewer1";
			this.reportViewer1.Size = new System.Drawing.Size(852, 741);
			this.reportViewer1.TabIndex = 5;
			// 
			// repPrimka
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(852, 740);
			this.Controls.Add(this.reportViewer1);
			this.Name = "repPrimka";
			this.Text = "Primka";
			this.Load += new System.EventHandler(this.repPrimka_Load);
			((System.ComponentModel.ISupportInitialize)(this.dTRPrimkaBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dSPrimka)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dTPrimkaStavkeBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dSPrimkaStavke)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private Dataset.DSPrimka dSPrimka;
		private Dataset.DSPrimkaStavke dSPrimkaStavke;
		private System.Windows.Forms.BindingSource dTPrimkaStavkeBindingSource;
		private Dataset.DSRpodaciTvrtke dSRpodaciTvrtke;
		private System.Windows.Forms.BindingSource dTRpodaciTvrtkeBindingSource;
		private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
		private System.Windows.Forms.BindingSource dTRPrimkaBindingSource;

	}
}