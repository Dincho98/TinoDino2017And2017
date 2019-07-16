namespace PCPOS.Report.Inventura
{
    partial class frmInventura
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.dTlisteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRliste = new PCPOS.Dataset.DSRliste();
            this.dTRpodaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRpodaciTvrtke = new PCPOS.Dataset.DSRpodaciTvrtke();
            this.dTlisteTekstBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRlisteTekst = new PCPOS.Dataset.DSRlisteTekst();
            this.dTListaUniverzalnaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.listaUniverzalna = new PCPOS.Dataset.ListaUniverzalna();
            this.dSstopeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSstope = new PCPOS.Dataset.DSstope();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.visakManjakBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dTlisteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRliste)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTlisteTekstBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRlisteTekst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTListaUniverzalnaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listaUniverzalna)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSstopeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSstope)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.visakManjakBindingSource)).BeginInit();
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
            // dTListaUniverzalnaBindingSource
            // 
            this.dTListaUniverzalnaBindingSource.DataMember = "DTListaUniverzalna";
            this.dTListaUniverzalnaBindingSource.DataSource = this.listaUniverzalna;
            // 
            // listaUniverzalna
            // 
            this.listaUniverzalna.DataSetName = "ListaUniverzalna";
            this.listaUniverzalna.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dSstopeBindingSource
            // 
            this.dSstopeBindingSource.DataMember = "DTstope";
            this.dSstopeBindingSource.DataSource = this.dSstope;
            // 
            // dSstope
            // 
            this.dSstope.DataSetName = "DSstope";
            this.dSstope.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.dTlisteBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.dTRpodaciTvrtkeBindingSource;
            reportDataSource3.Name = "DataSet3";
            reportDataSource3.Value = this.dTlisteTekstBindingSource;
            reportDataSource4.Name = "ListaUni";
            reportDataSource4.Value = this.dTListaUniverzalnaBindingSource;
            reportDataSource5.Name = "DSstope";
            reportDataSource5.Value = this.dSstopeBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource5);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Inventura.inventura.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            //this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(826, 745);
            this.reportViewer1.TabIndex = 0;
            // 
            // visakManjakBindingSource
            // 
            this.visakManjakBindingSource.DataMember = "DTvisakManjakStope";
            this.visakManjakBindingSource.DataSource = this.dSstope;
            // 
            // frmInventura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 745);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmInventura";
            this.Text = "Inventura";
            this.Load += new System.EventHandler(this.frmInventura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dTlisteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRliste)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTlisteTekstBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRlisteTekst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTListaUniverzalnaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listaUniverzalna)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSstopeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSstope)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.visakManjakBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

		private Dataset.DSRliste dSRliste;
        private System.Windows.Forms.BindingSource dTlisteBindingSource;
		private Dataset.DSRlisteTekst dSRlisteTekst;
        private System.Windows.Forms.BindingSource dTlisteTekstBindingSource;
		private Dataset.DSRpodaciTvrtke dSRpodaciTvrtke;
        private System.Windows.Forms.BindingSource dTRpodaciTvrtkeBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource dTListaUniverzalnaBindingSource;
        private Dataset.ListaUniverzalna listaUniverzalna;
        private System.Windows.Forms.BindingSource dSstopeBindingSource;
        private Dataset.DSstope dSstope;
        private System.Windows.Forms.BindingSource visakManjakBindingSource;
    }
}