namespace PCPOS.Report.Proizvodnja.RekapitulacijaRadnihNaloga
{
    partial class frmRekapitulacijaRadnihNaloga
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
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpDatumDo = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDatumOd = new System.Windows.Forms.DateTimePicker();
            this.btnTrazi = new System.Windows.Forms.Button();
            this.dSRpodaciTvrtke = new PCPOS.Dataset.DSRpodaciTvrtke();
            this.dSRpodaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.listaUniverzalna = new PCPOS.Dataset.ListaUniverzalna();
            this.listaUniverzalnaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listaUniverzalna)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listaUniverzalnaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer
            // 
            this.reportViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "DSRpodaciTvrtke";
            reportDataSource1.Value = this.dSRpodaciTvrtkeBindingSource;
            reportDataSource2.Name = "ListaUniverzalna";
            reportDataSource2.Value = this.listaUniverzalnaBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Proizvodnja.RekapitulacijaRadnihNaloga.Report.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(5, 70);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new System.Drawing.Size(766, 735);
            this.reportViewer.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(17, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 17);
            this.label3.TabIndex = 579;
            this.label3.Text = "Do datuma:";
            // 
            // dtpDatumDo
            // 
            this.dtpDatumDo.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dtpDatumDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDatumDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatumDo.Location = new System.Drawing.Point(122, 41);
            this.dtpDatumDo.Name = "dtpDatumDo";
            this.dtpDatumDo.Size = new System.Drawing.Size(167, 23);
            this.dtpDatumDo.TabIndex = 578;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(17, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 17);
            this.label2.TabIndex = 577;
            this.label2.Text = "Od datuma:";
            // 
            // dtpDatumOd
            // 
            this.dtpDatumOd.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dtpDatumOd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDatumOd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatumOd.Location = new System.Drawing.Point(122, 12);
            this.dtpDatumOd.Name = "dtpDatumOd";
            this.dtpDatumOd.Size = new System.Drawing.Size(167, 23);
            this.dtpDatumOd.TabIndex = 576;
            // 
            // btnTrazi
            // 
            this.btnTrazi.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnTrazi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnTrazi.Image = global::PCPOS.Properties.Resources._10591;
            this.btnTrazi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTrazi.Location = new System.Drawing.Point(676, 12);
            this.btnTrazi.Name = "btnTrazi";
            this.btnTrazi.Size = new System.Drawing.Size(96, 52);
            this.btnTrazi.TabIndex = 580;
            this.btnTrazi.Text = "       Traži";
            this.btnTrazi.UseVisualStyleBackColor = true;
            this.btnTrazi.Click += new System.EventHandler(this.btnTrazi_Click);
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
            // listaUniverzalna
            // 
            this.listaUniverzalna.DataSetName = "ListaUniverzalna";
            this.listaUniverzalna.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // listaUniverzalnaBindingSource
            // 
            this.listaUniverzalnaBindingSource.DataMember = "DTListaUniverzalna";
            this.listaUniverzalnaBindingSource.DataSource = this.listaUniverzalna;
            // 
            // frmRekapitulacijaRadnihNaloga
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(776, 812);
            this.Controls.Add(this.btnTrazi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpDatumDo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpDatumOd);
            this.Controls.Add(this.reportViewer);
            this.Name = "frmRekapitulacijaRadnihNaloga";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rekapitulacija radnih naloga";
            this.Load += new System.EventHandler(this.frmRekapitulacijaRadnihNaloga_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listaUniverzalna)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listaUniverzalnaBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpDatumDo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDatumOd;
        private System.Windows.Forms.Button btnTrazi;
        private System.Windows.Forms.BindingSource dSRpodaciTvrtkeBindingSource;
        private Dataset.DSRpodaciTvrtke dSRpodaciTvrtke;
        private System.Windows.Forms.BindingSource listaUniverzalnaBindingSource;
        private Dataset.ListaUniverzalna listaUniverzalna;
    }
}