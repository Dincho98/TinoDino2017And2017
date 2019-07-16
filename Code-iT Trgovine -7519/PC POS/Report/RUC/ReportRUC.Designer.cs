namespace PCPOS.Report.RUC
{
    partial class ReportRUC
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
            this.podaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRpodaciTvrtke = new PCPOS.Dataset.DSRpodaciTvrtke();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.listeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRlisteTekst = new PCPOS.Dataset.DSRlisteTekst();
            ((System.ComponentModel.ISupportInitialize)(this.podaciTvrtkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRlisteTekst)).BeginInit();
            this.SuspendLayout();
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
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "ReportViewer";
            this.reportViewer1.Size = new System.Drawing.Size(396, 246);
            this.reportViewer1.TabIndex = 0;
            // 
            // listeBindingSource
            // 
            this.listeBindingSource.DataMember = "DTlisteTekst";
            this.listeBindingSource.DataSource = this.dSRlisteTekst;
            // 
            // dSRlisteTekst
            // 
            this.dSRlisteTekst.DataSetName = "DSRlisteTekst";
            this.dSRlisteTekst.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ReportRUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 961);
            this.Name = "ReportRUC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Razlika u cijeni";
            this.Load += new System.EventHandler(this.ReportRUC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.podaciTvrtkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRlisteTekst)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource podaciTvrtkeBindingSource;
        private Dataset.DSRpodaciTvrtke dSRpodaciTvrtke;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource listeBindingSource;
        private Dataset.DSRlisteTekst dSRlisteTekst;
    }
}