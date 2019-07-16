namespace PCPOS.Report.KnjigaPrometa
{
    partial class FrmKnjigaPrometaReport
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.knjigaPrometaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSKnjigaPrometa = new PCPOS.Dataset.DSKnjigaPrometa();
            this.podaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRpodaciTvrtke = new PCPOS.Dataset.DSRpodaciTvrtke();
            ((System.ComponentModel.ISupportInitialize)(this.knjigaPrometaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSKnjigaPrometa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.podaciTvrtkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "ReportViewer";
            this.reportViewer1.Size = new System.Drawing.Size(396, 246);
            this.reportViewer1.TabIndex = 0;
            // 
            // knjigaPrometaBindingSource
            // 
            this.knjigaPrometaBindingSource.DataMember = "DTknjigaPrometa";
            this.knjigaPrometaBindingSource.DataSource = this.dSKnjigaPrometa;
            // 
            // dSKnjigaPrometa
            // 
            this.dSKnjigaPrometa.DataSetName = "DSKnjigaPrometa";
            this.dSKnjigaPrometa.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            // FrmKnjigaPrometaReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "FrmKnjigaPrometaReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KnjigaPrometaReport";
            this.Load += new System.EventHandler(this.KnjigaPrometaReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.knjigaPrometaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSKnjigaPrometa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.podaciTvrtkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource knjigaPrometaBindingSource;
        private Dataset.DSKnjigaPrometa dSKnjigaPrometa;
        private System.Windows.Forms.BindingSource podaciTvrtkeBindingSource;
        private Dataset.DSRpodaciTvrtke dSRpodaciTvrtke;
    }
}