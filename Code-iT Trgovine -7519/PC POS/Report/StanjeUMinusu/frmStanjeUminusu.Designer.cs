namespace PCPOS.Report.StanjeUMinusu {
    partial class frmStanjeUminusu {
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dsStanjeUMinusuBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsStanjeUMinusu = new PCPOS.Dataset.dsStanjeUMinusu();
            this.dtStanjeUMinusuBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dsStanjeUMinusuBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsStanjeUMinusu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStanjeUMinusuBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.dtStanjeUMinusuBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.StanjeUMinusu.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(806, 725);
            this.reportViewer1.TabIndex = 0;
            // 
            // dsStanjeUMinusuBindingSource
            // 
            this.dsStanjeUMinusuBindingSource.DataSource = this.dsStanjeUMinusu;
            this.dsStanjeUMinusuBindingSource.Position = 0;
            this.dsStanjeUMinusuBindingSource.CurrentChanged += new System.EventHandler(this.dsStanjeUMinusuBindingSource_CurrentChanged);
            // 
            // dsStanjeUMinusu
            // 
            this.dsStanjeUMinusu.DataSetName = "dsStanjeUMinusu";
            this.dsStanjeUMinusu.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dtStanjeUMinusuBindingSource
            // 
            this.dtStanjeUMinusuBindingSource.DataMember = "dtStanjeUMinusu";
            this.dtStanjeUMinusuBindingSource.DataSource = this.dsStanjeUMinusu;
            // 
            // frmStanjeUminusu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 725);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmStanjeUminusu";
            this.Text = "frmStanjeUminusu";
            this.Load += new System.EventHandler(this.frmStanjeUminusu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dsStanjeUMinusuBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsStanjeUMinusu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStanjeUMinusuBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource dsStanjeUMinusuBindingSource;
        private Dataset.dsStanjeUMinusu dsStanjeUMinusu;
        private System.Windows.Forms.BindingSource dtStanjeUMinusuBindingSource;

    }
}