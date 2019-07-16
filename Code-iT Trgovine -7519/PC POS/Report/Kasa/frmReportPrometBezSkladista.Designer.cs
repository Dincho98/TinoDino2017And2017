namespace PCPOS.Report.Kasa
{
	partial class frmReportPrometBezSkladista
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
			this.dTRpodaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dSRpodaciTvrtke = new PCPOS.Dataset.DSRpodaciTvrtke();
			this.dTRRacuniPrometBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dSRacuniPromet = new PCPOS.Dataset.DSRacuniPromet();
			this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
			((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dTRRacuniPrometBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dSRacuniPromet)).BeginInit();
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
			// dTRRacuniPrometBindingSource
			// 
			this.dTRRacuniPrometBindingSource.DataMember = "DTRRacuniPromet";
			this.dTRRacuniPrometBindingSource.DataSource = this.dSRacuniPromet;
			// 
			// dSRacuniPromet
			// 
			this.dSRacuniPromet.DataSetName = "DSRacuniPromet";
			this.dSRacuniPromet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// reportViewer1
			// 
			this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
			reportDataSource1.Name = "dSPodaciTvrtke";
			reportDataSource1.Value = this.dTRpodaciTvrtkeBindingSource;
			reportDataSource2.Name = "dSRacuniPromet";
			reportDataSource2.Value = this.dTRRacuniPrometBindingSource;
			this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
			this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
			this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Kasa.PrometBezSkladista.rdlc";
			this.reportViewer1.Location = new System.Drawing.Point(0, 0);
			this.reportViewer1.Name = "reportViewer1";
			this.reportViewer1.Size = new System.Drawing.Size(933, 858);
			this.reportViewer1.TabIndex = 4;
			// 
			// frmReportPrometBezSkladista
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(933, 858);
			this.Controls.Add(this.reportViewer1);
			this.Name = "frmReportPrometBezSkladista";
			this.Text = "Promet kase po računima";
			this.Load += new System.EventHandler(this.frmReportPrometBezSkladista_Load);
			((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dTRRacuniPrometBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dSRacuniPromet)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
		private Dataset.DSRpodaciTvrtke dSRpodaciTvrtke;
		private System.Windows.Forms.BindingSource dTRpodaciTvrtkeBindingSource;
		private Dataset.DSRacuniPromet dSRacuniPromet;
		private System.Windows.Forms.BindingSource dTRRacuniPrometBindingSource;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
	}
}