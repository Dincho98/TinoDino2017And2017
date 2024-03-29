﻿namespace PCPOS.Report.Kalkulacija
{
    partial class frmKalkulacija
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
            this.dTKalkulacijABindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSkalkulacija = new PCPOS.Dataset.DSkalkulacija();
            this.dTRpodaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRpodaciTvrtke = new PCPOS.Dataset.DSRpodaciTvrtke();
            this.dTkalkDtavkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSkalkulacija_stavke = new PCPOS.Dataset.DSkalkulacija_stavke();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.dTKalkulacijABindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSkalkulacija)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTkalkDtavkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSkalkulacija_stavke)).BeginInit();
            this.SuspendLayout();
            // 
            // dTKalkulacijABindingSource
            // 
            this.dTKalkulacijABindingSource.DataMember = "DTKalkulacijA";
            this.dTKalkulacijABindingSource.DataSource = this.dSkalkulacija;
            // 
            // dSkalkulacija
            // 
            this.dSkalkulacija.DataSetName = "DSkalkulacija";
            this.dSkalkulacija.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dTRpodaciTvrtkeBindingSource
            // 
            this.dTRpodaciTvrtkeBindingSource.DataMember = "DTRpodaciTvrtke";
            this.dTRpodaciTvrtkeBindingSource.DataSource = this.dSRpodaciTvrtke;
            this.dTRpodaciTvrtkeBindingSource.CurrentChanged += new System.EventHandler(this.dTRpodaciTvrtkeBindingSource_CurrentChanged);
            // 
            // dSRpodaciTvrtke
            // 
            this.dSRpodaciTvrtke.DataSetName = "DSRpodaciTvrtke";
            this.dSRpodaciTvrtke.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dTkalkDtavkeBindingSource
            // 
            this.dTkalkDtavkeBindingSource.DataMember = "DTkalkDtavke";
            this.dTkalkDtavkeBindingSource.DataSource = this.dSkalkulacija_stavke;
            // 
            // dSkalkulacija_stavke
            // 
            this.dSkalkulacija_stavke.DataSetName = "DSkalkulacija_stavke";
            this.dSkalkulacija_stavke.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DSkalk";
            reportDataSource1.Value = this.dTKalkulacijABindingSource;
            reportDataSource2.Name = "DSpodaci";
            reportDataSource2.Value = this.dTRpodaciTvrtkeBindingSource;
            reportDataSource3.Name = "DSkalk_stavke";
            reportDataSource3.Value = this.dTkalkDtavkeBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Kalkulacija.Kalkulacija.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1125, 612);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // frmKalkulacija
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 612);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmKalkulacija";
            this.Text = "Kalkulacija";
            this.Load += new System.EventHandler(this.frmKalkulacija_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dTKalkulacijABindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSkalkulacija)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTkalkDtavkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSkalkulacija_stavke)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
		private Dataset.DSRpodaciTvrtke dSRpodaciTvrtke;
        private System.Windows.Forms.BindingSource dTRpodaciTvrtkeBindingSource;
		private Dataset.DSkalkulacija dSkalkulacija;
        private System.Windows.Forms.BindingSource dTKalkulacijABindingSource;
		private Dataset.DSkalkulacija_stavke dSkalkulacija_stavke;
        private System.Windows.Forms.BindingSource dTkalkDtavkeBindingSource;
    }
}