namespace PCPOS.Report.Knjigaprimitakaiizadatak
{
    partial class knjigaPII
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
            this.dTRpodaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRpodaciTvrtke = new PCPOS.Dataset.DSRpodaciTvrtke();
            this.dataTable1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.knjigaPII1 = new PCPOS.Dataset.KnjigaPII();
            this.bindingSource_porezi_prim = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource_porezi_izd = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.knjigaPII1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_porezi_prim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_porezi_izd)).BeginInit();
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
            // dataTable1BindingSource
            // 
            this.dataTable1BindingSource.DataMember = "DataTable1";
            this.dataTable1BindingSource.DataSource = this.knjigaPII1;
            // 
            // knjigaPII1
            // 
            this.knjigaPII1.DataSetName = "KnjigaPII";
            this.knjigaPII1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingSource_porezi_prim
            // 
            this.bindingSource_porezi_prim.DataMember = "porezi_prim";
            this.bindingSource_porezi_prim.DataSource = this.knjigaPII1;
            // 
            // bindingSource_porezi_izd
            // 
            this.bindingSource_porezi_izd.DataMember = "porezi_izd";
            this.bindingSource_porezi_izd.DataSource = this.knjigaPII1;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.dTRpodaciTvrtkeBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.dataTable1BindingSource;
            reportDataSource3.Name = "porez_prim";
            reportDataSource3.Value = this.bindingSource_porezi_prim;
            reportDataSource4.Name = "porez_izd";
            reportDataSource4.Value = this.bindingSource_porezi_izd;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Knjigaprimitakaiizadatak.knjigPII.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1129, 535);
            this.reportViewer1.TabIndex = 0;
            // 
            // knjigaPII
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1129, 535);
            this.Controls.Add(this.reportViewer1);
            this.Name = "knjigaPII";
            this.Text = "Knjiga primitaka i izdataka";
            this.Load += new System.EventHandler(this.knjigaPII_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.knjigaPII1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_porezi_prim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_porezi_izd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Dataset.DSRpodaciTvrtke dSRpodaciTvrtke;
        private System.Windows.Forms.BindingSource dTRpodaciTvrtkeBindingSource;
        private System.Windows.Forms.BindingSource dataTable1BindingSource;
        private System.Windows.Forms.BindingSource bindingSource_porezi_prim;
        private System.Windows.Forms.BindingSource bindingSource_porezi_izd;
        private Dataset.KnjigaPII knjigaPII1;
    }
}