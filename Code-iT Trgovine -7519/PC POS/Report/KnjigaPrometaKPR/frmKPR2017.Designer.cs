namespace PCPOS.Report.KnjigaPrometaKPR
{
    partial class frmKPR2017
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
            this.DTListaUniverzalnaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.listaUniverzalna = new PCPOS.Dataset.ListaUniverzalna();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.cmbMonths = new System.Windows.Forms.ComboBox();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DTListaUniverzalnaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listaUniverzalna)).BeginInit();
            this.SuspendLayout();
            // 
            // DTListaUniverzalnaBindingSource
            // 
            this.DTListaUniverzalnaBindingSource.DataMember = "DTListaUniverzalna";
            this.DTListaUniverzalnaBindingSource.DataSource = this.listaUniverzalna;
            // 
            // listaUniverzalna
            // 
            this.listaUniverzalna.DataSetName = "ListaUniverzalna";
            this.listaUniverzalna.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer
            // 
            this.reportViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.DTListaUniverzalnaBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer.LocalReport.ReportEmbeddedResource = "PCPOS.Report.KnjigaPrometaKPR.KPR2017.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(0, 66);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new System.Drawing.Size(1125, 546);
            this.reportViewer.TabIndex = 1;
            // 
            // cmbMonths
            // 
            this.cmbMonths.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonths.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cmbMonths.FormattingEnabled = true;
            this.cmbMonths.Location = new System.Drawing.Point(71, 16);
            this.cmbMonths.Name = "cmbMonths";
            this.cmbMonths.Size = new System.Drawing.Size(246, 28);
            this.cmbMonths.TabIndex = 2;
            // 
            // lblPeriod
            // 
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblPeriod.Location = new System.Drawing.Point(12, 22);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(53, 17);
            this.lblPeriod.TabIndex = 3;
            this.lblPeriod.Text = "Period:";
            // 
            // btnLoad
            // 
            this.btnLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnLoad.Location = new System.Drawing.Point(1003, 12);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(110, 48);
            this.btnLoad.TabIndex = 4;
            this.btnLoad.Text = "Učitaj";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // frmKPR2017
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 612);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.lblPeriod);
            this.Controls.Add(this.cmbMonths);
            this.Controls.Add(this.reportViewer);
            this.Name = "frmKPR2017";
            this.Text = "frmKPR2017";
            this.Load += new System.EventHandler(this.frmKPR2017_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DTListaUniverzalnaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listaUniverzalna)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbMonths;
        private System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.Button btnLoad;
        private Dataset.ListaUniverzalna listaUniverzalna;
        private System.Windows.Forms.BindingSource DTListaUniverzalnaBindingSource;
        public Microsoft.Reporting.WinForms.ReportViewer reportViewer;
    }
}