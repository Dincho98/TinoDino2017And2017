﻿namespace PCPOS.Report.lzlazniRacuni
{
    partial class frmListakalk
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
			this.dTlisteBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dSRliste = new PCPOS.Dataset.DSRliste();
			this.dTlisteTekstBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dSRlisteTekst = new PCPOS.Dataset.DSRlisteTekst();
			this.dTRpodaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dSRpodaciTvrtke = new PCPOS.Dataset.DSRpodaciTvrtke();
			this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
			((System.ComponentModel.ISupportInitialize)(this.dTlisteBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dSRliste)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dTlisteTekstBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dSRlisteTekst)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
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
			this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
			reportDataSource1.Name = "DataSet1";
			reportDataSource1.Value = this.dTlisteBindingSource;
			reportDataSource2.Name = "DataSet2";
			reportDataSource2.Value = this.dTlisteTekstBindingSource;
			reportDataSource3.Name = "DataSet3";
			reportDataSource3.Value = this.dTRpodaciTvrtkeBindingSource;
			this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
			this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
			this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
			this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.IzlazniRacuni.Listakalk.rdlc";
			this.reportViewer1.Location = new System.Drawing.Point(0, 0);
			this.reportViewer1.Name = "reportViewer1";
			this.reportViewer1.Size = new System.Drawing.Size(627, 429);
			this.reportViewer1.TabIndex = 0;
			// 
			// frmListakalk
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(627, 429);
			this.Controls.Add(this.reportViewer1);
			this.Name = "frmListakalk";
			this.Text = "Lista";
			this.Load += new System.EventHandler(this.frmListakalk_Load);
			((System.ComponentModel.ISupportInitialize)(this.dTlisteBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dSRliste)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dTlisteTekstBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dSRlisteTekst)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Dataset.DSRliste dSRliste;
        private System.Windows.Forms.BindingSource dTlisteBindingSource;
		private Dataset.DSRlisteTekst dSRlisteTekst;
        private System.Windows.Forms.BindingSource dTlisteTekstBindingSource;
		private Dataset.DSRpodaciTvrtke dSRpodaciTvrtke;
        private System.Windows.Forms.BindingSource dTRpodaciTvrtkeBindingSource;
    }
}