namespace PCPOS.Report.KnjiznoOdobrenje {
    partial class frmKnjiznoOdobrenje {
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
            this.dtKnjiznoOdobrenjeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSKnjiznoOdobrenje = new PCPOS.Dataset.DSKnjiznoOdobrenje();
            this.DTRfakturaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSFaktura = new PCPOS.Dataset.DSFaktura();
            this.DTRpodaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRpodaciTvrtke = new PCPOS.Dataset.DSRpodaciTvrtke();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dSKnjiznoOdobrenjeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsPodaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DSFaktureBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dtKnjiznoOdobrenjeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSKnjiznoOdobrenje)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTRfakturaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSFaktura)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTRpodaciTvrtkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSKnjiznoOdobrenjeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPodaciTvrtkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DSFaktureBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dtKnjiznoOdobrenjeBindingSource
            // 
            this.dtKnjiznoOdobrenjeBindingSource.DataMember = "dtKnjiznoOdobrenje";
            this.dtKnjiznoOdobrenjeBindingSource.DataSource = this.dSKnjiznoOdobrenje;
            // 
            // dSKnjiznoOdobrenje
            // 
            this.dSKnjiznoOdobrenje.DataSetName = "DSKnjiznoOdobrenje";
            this.dSKnjiznoOdobrenje.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // DTRfakturaBindingSource
            // 
            this.DTRfakturaBindingSource.DataMember = "DTRfaktura";
            this.DTRfakturaBindingSource.DataSource = this.dSFaktura;
            // 
            // dSFaktura
            // 
            this.dSFaktura.DataSetName = "DSFaktura";
            this.dSFaktura.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // DTRpodaciTvrtkeBindingSource
            // 
            this.DTRpodaciTvrtkeBindingSource.DataMember = "DTRpodaciTvrtke";
            this.DTRpodaciTvrtkeBindingSource.DataSource = this.dSRpodaciTvrtke;
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
            reportDataSource1.Value = this.dtKnjiznoOdobrenjeBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.DTRfakturaBindingSource;
            reportDataSource3.Name = "DataSet3";
            reportDataSource3.Value = this.DTRpodaciTvrtkeBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.KnjiznoOdobrenje.repKnjiznoOdobrenje.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(826, 765);
            this.reportViewer1.TabIndex = 0;
            // 
            // dSKnjiznoOdobrenjeBindingSource
            // 
            this.dSKnjiznoOdobrenjeBindingSource.AllowNew = true;
            this.dSKnjiznoOdobrenjeBindingSource.DataMember = "dtKnjiznoOdobrenje";
            this.dSKnjiznoOdobrenjeBindingSource.DataSource = this.dSKnjiznoOdobrenje;
            // 
            // dsPodaciTvrtkeBindingSource
            // 
            this.dsPodaciTvrtkeBindingSource.DataMember = "DTRpodaciTvrtke";
            this.dsPodaciTvrtkeBindingSource.DataSource = this.dSRpodaciTvrtke;
            // 
            // DSFaktureBindingSource
            // 
            this.DSFaktureBindingSource.DataMember = "DTRfaktura";
            this.DSFaktureBindingSource.DataSource = this.dSFaktura;
            // 
            // frmKnjiznoOdobrenje
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 765);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmKnjiznoOdobrenje";
            this.Text = "frmKnjiznoOdobrenje";
            this.Load += new System.EventHandler(this.frmKnjiznoOdobrenje_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtKnjiznoOdobrenjeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSKnjiznoOdobrenje)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTRfakturaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSFaktura)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTRpodaciTvrtkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSKnjiznoOdobrenjeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPodaciTvrtkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DSFaktureBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        public System.Windows.Forms.BindingSource dSKnjiznoOdobrenjeBindingSource;
        public Dataset.DSKnjiznoOdobrenje dSKnjiznoOdobrenje;
        private Dataset.DSFaktura dSFaktura;
        public System.Windows.Forms.BindingSource dtKnjiznoOdobrenjeBindingSource;
        public System.Windows.Forms.BindingSource dsPodaciTvrtkeBindingSource;
        public System.Windows.Forms.BindingSource DSFaktureBindingSource;
        public Dataset.DSRpodaciTvrtke dSRpodaciTvrtke;
        private System.Windows.Forms.BindingSource DTRpodaciTvrtkeBindingSource;
        private System.Windows.Forms.BindingSource DTRfakturaBindingSource;
    }
}