namespace PCPOS.Report.PopisGostiju
{
    partial class FrmPopisGostijuReport
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
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.popisGostijuBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsPopisGostiju = new PCPOS.Report.PopisGostiju.dsPopisGostiju();
            this.podaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRpodaciTvrtke = new PCPOS.Dataset.DSRpodaciTvrtke();
            ((System.ComponentModel.ISupportInitialize)(this.popisGostijuBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPopisGostiju)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.podaciTvrtkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer
            // 
            this.reportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer.Location = new System.Drawing.Point(0, 0);
            this.reportViewer.Name = "ReportViewer";
            this.reportViewer.Size = new System.Drawing.Size(800, 600);
            this.reportViewer.TabIndex = 0;
            // 
            // popisGostijuBindingSource
            // 
            this.popisGostijuBindingSource.DataMember = "dtGost";
            this.popisGostijuBindingSource.DataSource = this.dsPopisGostiju;
            // 
            // dsPopisGostiju
            // 
            this.dsPopisGostiju.DataSetName = "dsPopisGostiju";
            this.dsPopisGostiju.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            // FrmPopisGostijuReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(884, 961);
            this.Name = "FrmPopisGostijuReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Popis gostiju - izvještaj";
            this.Load += new System.EventHandler(this.FrmPopisGostijuReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.popisGostijuBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPopisGostiju)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.podaciTvrtkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.BindingSource popisGostijuBindingSource;
        private dsPopisGostiju dsPopisGostiju;
        private System.Windows.Forms.BindingSource podaciTvrtkeBindingSource;
        private Dataset.DSRpodaciTvrtke dSRpodaciTvrtke;
    }
}