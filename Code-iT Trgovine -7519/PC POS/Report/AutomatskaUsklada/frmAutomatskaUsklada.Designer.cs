namespace PCPOS.Report.AutomatskaUsklada {
    partial class frmAutomatskaUsklada {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent () {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.dtAutomatskaUskladaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsAutomatskaUsklada1 = new PCPOS.Dataset.dsAutomatskaUsklada();
            this.dtAutomatskaUskladaStavkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dSRpodaciTvrtke = new PCPOS.Dataset.DSRpodaciTvrtke();
            this.dSRpodaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dtAutomatskaUskladaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAutomatskaUsklada1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtAutomatskaUskladaStavkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtkeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dtAutomatskaUskladaBindingSource
            // 
            this.dtAutomatskaUskladaBindingSource.DataMember = "dtAutomatskaUsklada";
            this.dtAutomatskaUskladaBindingSource.DataSource = this.dsAutomatskaUsklada1;
            // 
            // dsAutomatskaUsklada1
            // 
            this.dsAutomatskaUsklada1.DataSetName = "dsAutomatskaUsklada";
            this.dsAutomatskaUsklada1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dtAutomatskaUskladaStavkeBindingSource
            // 
            this.dtAutomatskaUskladaStavkeBindingSource.DataMember = "dtAutomatskaUskladaStavke";
            this.dtAutomatskaUskladaStavkeBindingSource.DataSource = this.dsAutomatskaUsklada1;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "dtAutomatskaUsklada";
            reportDataSource1.Value = this.dtAutomatskaUskladaBindingSource;
            reportDataSource2.Name = "dtAutomatskaUskladaStavke";
            reportDataSource2.Value = this.dtAutomatskaUskladaStavkeBindingSource;
            reportDataSource3.Name = "DTRpodaciTvrtke";
            reportDataSource3.Value = this.dSRpodaciTvrtkeBindingSource;
            reportDataSource4.Name = "DataSet1";
            reportDataSource4.Value = this.dSRpodaciTvrtkeBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.AutomatskaUsklada.Report.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(806, 725);
            this.reportViewer1.TabIndex = 0;
            // 
            // dSRpodaciTvrtke
            // 
            this.dSRpodaciTvrtke.DataSetName = "DSRpodaciTvrtke";
            this.dSRpodaciTvrtke.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dSRpodaciTvrtkeBindingSource
            // 
            this.dSRpodaciTvrtkeBindingSource.DataMember = "DTRpodaciTvrtke";
            this.dSRpodaciTvrtkeBindingSource.DataSource = this.dSRpodaciTvrtke;
            // 
            // frmAutomatskaUsklada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 725);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmAutomatskaUsklada";
            this.Text = "frmAutomatskaUsklada";
            this.Load += new System.EventHandler(this.frmAutomatskaUsklada_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtAutomatskaUskladaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAutomatskaUsklada1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtAutomatskaUskladaStavkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtkeBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Dataset.dsAutomatskaUsklada dsAutomatskaUsklada;
        private System.Windows.Forms.BindingSource dtAutomatskaUskladaBindingSource;
        private Dataset.dsAutomatskaUsklada dsAutomatskaUsklada1;
        private System.Windows.Forms.BindingSource dtAutomatskaUskladaStavkeBindingSource;
        private System.Windows.Forms.BindingSource dSRpodaciTvrtkeBindingSource;
        private Dataset.DSRpodaciTvrtke dSRpodaciTvrtke;
    }
}