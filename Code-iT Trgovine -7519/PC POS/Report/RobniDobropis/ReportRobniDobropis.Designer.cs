namespace PCPOS.Report.RobniDobropis
{
    partial class ReportRobniDobropis
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource9 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource10 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource11 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource12 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.podaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRpodaciTvrtke = new PCPOS.Dataset.DSRpodaciTvrtke();
            this.listeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRliste = new PCPOS.Dataset.DSRliste();
            this.dobropisBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.fakturaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSFaktura = new PCPOS.Dataset.DSFaktura();
            ((System.ComponentModel.ISupportInitialize)(this.podaciTvrtkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRliste)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dobropisBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fakturaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSFaktura)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer
            // 
            this.reportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource9.Name = "DSkalk";
            reportDataSource9.Value = null;
            reportDataSource10.Name = "DSpodaci";
            reportDataSource10.Value = null;
            reportDataSource11.Name = "DSkalk_stavke";
            reportDataSource11.Value = null;
            reportDataSource12.Name = "dStope";
            reportDataSource12.Value = null;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource9);
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource10);
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource11);
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource12);
            this.reportViewer.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Kalkulacija2016.Kalkulacija.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(0, 0);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new System.Drawing.Size(1125, 612);
            this.reportViewer.TabIndex = 0;
            // 
            // podaciTvrtkeBindingSource
            // 
            this.podaciTvrtkeBindingSource.DataMember = "DTRpodaciTvrtke";
            this.podaciTvrtkeBindingSource.DataSource = this.dSRpodaciTvrtke;
            // 
            // dSRpodaciTvrtke
            // 
            this.dSRpodaciTvrtke.DataSetName = "DSRpodaciTvrtke";
            this.dSRpodaciTvrtke.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // listeBindingSource
            // 
            this.listeBindingSource.DataMember = "DTliste";
            this.listeBindingSource.DataSource = this.dSRliste;
            // 
            // dSRliste
            // 
            this.dSRliste.DataSetName = "DSRliste";
            this.dSRliste.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dobropisBindingSource
            // 
            this.dobropisBindingSource.DataMember = "DTdobropis";
            this.dobropisBindingSource.DataSource = this.dSRliste;
            // 
            // fakturaBindingSource
            // 
            this.fakturaBindingSource.DataMember = "DTRfaktura";
            this.fakturaBindingSource.DataSource = this.dSFaktura;
            // 
            // dSFaktura
            // 
            this.dSFaktura.DataSetName = "DSFaktura";
            this.dSFaktura.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ReportRobniDobropis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 961);
            this.Name = "ReportRobniDobropis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Robni dobropis";
            this.Load += new System.EventHandler(this.ReportRobniDobropis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.podaciTvrtkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRliste)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dobropisBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fakturaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSFaktura)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.BindingSource podaciTvrtkeBindingSource;
        private Dataset.DSRpodaciTvrtke dSRpodaciTvrtke;
        private System.Windows.Forms.BindingSource listeBindingSource;
        private Dataset.DSRliste dSRliste;
        private System.Windows.Forms.BindingSource dobropisBindingSource;
        private System.Windows.Forms.BindingSource fakturaBindingSource;
        private Dataset.DSFaktura dSFaktura;
    }
}