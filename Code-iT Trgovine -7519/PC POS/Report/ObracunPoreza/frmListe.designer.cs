namespace PCPOS.Report.ObracunPoreza
{
    partial class frmListe5
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
			Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new Microsoft.Reporting.WinForms.ReportDataSource();
			this.dTRpodaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dSRpodaciTvrtke = new PCPOS.Dataset.DSRpodaciTvrtke();
			this.dTobr1BindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dSobracunpor = new PCPOS.Dataset.DSobracunpor();
			this.dTobr2BindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dSobracunpor1 = new PCPOS.Dataset.DSobracunpor1();
			this.dTobr3BindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dSobracunpor2 = new PCPOS.Dataset.DSobracunpor2();
			this.dTlisteTekstBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dSRlisteTekst = new PCPOS.Dataset.DSRlisteTekst();
			this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
			((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dTobr1BindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dSobracunpor)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dTobr2BindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dSobracunpor1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dTobr3BindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dSobracunpor2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dTlisteTekstBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dSRlisteTekst)).BeginInit();
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
			// dTobr1BindingSource
			// 
			this.dTobr1BindingSource.DataMember = "DTobr1";
			this.dTobr1BindingSource.DataSource = this.dSobracunpor;
			// 
			// dSobracunpor
			// 
			this.dSobracunpor.DataSetName = "DSobracunpor";
			this.dSobracunpor.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// dTobr2BindingSource
			// 
			this.dTobr2BindingSource.DataMember = "DTobr2";
			this.dTobr2BindingSource.DataSource = this.dSobracunpor1;
			// 
			// dSobracunpor1
			// 
			this.dSobracunpor1.DataSetName = "DSobracunpor1";
			this.dSobracunpor1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// dTobr3BindingSource
			// 
			this.dTobr3BindingSource.DataMember = "DTobr3";
			this.dTobr3BindingSource.DataSource = this.dSobracunpor2;
			// 
			// dSobracunpor2
			// 
			this.dSobracunpor2.DataSetName = "DSobracunpor2";
			this.dSobracunpor2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
			this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
			reportDataSource1.Name = "DataSet2";
			reportDataSource1.Value = this.dTRpodaciTvrtkeBindingSource;
			reportDataSource2.Name = "DataSet4";
			reportDataSource2.Value = this.dTobr1BindingSource;
			reportDataSource3.Name = "DataSet5";
			reportDataSource3.Value = this.dTobr2BindingSource;
			reportDataSource4.Name = "DataSet6";
			reportDataSource4.Value = this.dTobr3BindingSource;
			reportDataSource5.Name = "DataSet1";
			reportDataSource5.Value = this.dTlisteTekstBindingSource;
			this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
			this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
			this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
			this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
			this.reportViewer1.LocalReport.DataSources.Add(reportDataSource5);
			this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.ObracunPoreza.Liste.rdlc";
			this.reportViewer1.Location = new System.Drawing.Point(0, 0);
			this.reportViewer1.Name = "reportViewer1";
			this.reportViewer1.Size = new System.Drawing.Size(785, 881);
			this.reportViewer1.TabIndex = 0;
			// 
			// frmListe5
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.BackColor = System.Drawing.Color.SlateGray;
			this.ClientSize = new System.Drawing.Size(785, 881);
			this.Controls.Add(this.reportViewer1);
			this.Name = "frmListe5";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Obračun poreza";
			this.Load += new System.EventHandler(this.frmListe_Load);
			((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dTobr1BindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dSobracunpor)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dTobr2BindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dSobracunpor1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dTobr3BindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dSobracunpor2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dTlisteTekstBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dSRlisteTekst)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
		private Dataset.DSobracunpor2 dSobracunpor2;
        private System.Windows.Forms.BindingSource dTobr3BindingSource;
		private Dataset.DSobracunpor1 dSobracunpor1;
        private System.Windows.Forms.BindingSource dTobr2BindingSource;
		private Dataset.DSobracunpor dSobracunpor;
        private System.Windows.Forms.BindingSource dTobr1BindingSource;
        private Dataset.DSRpodaciTvrtke dSRpodaciTvrtke;
        private System.Windows.Forms.BindingSource dTRpodaciTvrtkeBindingSource;
		private Dataset.DSRlisteTekst dSRlisteTekst;
        private System.Windows.Forms.BindingSource dTlisteTekstBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}