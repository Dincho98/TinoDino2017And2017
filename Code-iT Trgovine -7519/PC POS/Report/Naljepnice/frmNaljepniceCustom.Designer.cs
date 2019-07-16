namespace PCPOS.Report.Naljepnice
{
	partial class frmNaljepniceCustom

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
            this.dTnaljepniceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSnaljepnice = new PCPOS.Dataset.DSnaljepnice();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.dTnaljepniceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSnaljepnice)).BeginInit();
            this.SuspendLayout();
            // 
            // dTnaljepniceBindingSource
            // 
            this.dTnaljepniceBindingSource.DataMember = "DTnaljepnice";
            this.dTnaljepniceBindingSource.DataSource = this.dSnaljepnice;
            // 
            // dSnaljepnice
            // 
            this.dSnaljepnice.DataSetName = "DSnaljepnice";
            this.dSnaljepnice.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DSnaljepnice";
            reportDataSource1.Value = this.dTnaljepniceBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Naljepnice.NaljepniceCustom.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(790, 725);
            this.reportViewer1.TabIndex = 0;
            // 
            // frmNaljepniceCustom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 725);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmNaljepniceCustom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Naljepnice";
            this.Load += new System.EventHandler(this.frmNaljepnice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dTnaljepniceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSnaljepnice)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private Dataset.DSnaljepnice dSnaljepnice;
		private System.Windows.Forms.BindingSource dTnaljepniceBindingSource;
		private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
	}
}