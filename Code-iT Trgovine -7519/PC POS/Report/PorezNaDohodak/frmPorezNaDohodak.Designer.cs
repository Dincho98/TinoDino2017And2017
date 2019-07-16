namespace PCPOS.Report.PorezNaDohodak
{
    partial class frmPorezNaDohodak
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
            this.dTRpodaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRpodaciTvrtke = new PCPOS.Dataset.DSRpodaciTvrtke();
            this.dTListaUniverzalnaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.listaUniverzalna = new PCPOS.Dataset.ListaUniverzalna();
            this.label3 = new System.Windows.Forms.Label();
            this.tdDoDatuma = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.tdOdDatuma = new System.Windows.Forms.DateTimePicker();
            this.btnTrazi = new System.Windows.Forms.Button();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label1 = new System.Windows.Forms.Label();
            this.cbDucan = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTListaUniverzalnaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listaUniverzalna)).BeginInit();
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 575;
            this.label3.Text = "Do datuma:";
            // 
            // tdDoDatuma
            // 
            this.tdDoDatuma.CustomFormat = "dd.MM.yyyy H:mm";
            this.tdDoDatuma.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tdDoDatuma.Location = new System.Drawing.Point(85, 39);
            this.tdDoDatuma.Name = "tdDoDatuma";
            this.tdDoDatuma.Size = new System.Drawing.Size(167, 20);
            this.tdDoDatuma.TabIndex = 574;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 573;
            this.label2.Text = "Od datuma:";
            // 
            // tdOdDatuma
            // 
            this.tdOdDatuma.CustomFormat = "dd.MM.yyyy H:mm";
            this.tdOdDatuma.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tdOdDatuma.Location = new System.Drawing.Point(85, 16);
            this.tdOdDatuma.Name = "tdOdDatuma";
            this.tdOdDatuma.Size = new System.Drawing.Size(167, 20);
            this.tdOdDatuma.TabIndex = 572;
            // 
            // btnTrazi
            // 
            this.btnTrazi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnTrazi.Image = global::PCPOS.Properties.Resources._10591;
            this.btnTrazi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTrazi.Location = new System.Drawing.Point(540, 13);
            this.btnTrazi.Name = "btnTrazi";
            this.btnTrazi.Size = new System.Drawing.Size(96, 46);
            this.btnTrazi.TabIndex = 571;
            this.btnTrazi.Text = "       Traži";
            this.btnTrazi.UseVisualStyleBackColor = true;
            this.btnTrazi.Click += new System.EventHandler(this.btnTrazi_Click);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "DSpodaciTVR";
            reportDataSource1.Value = this.dTRpodaciTvrtkeBindingSource;
            reportDataSource2.Name = "DsPodaci";
            reportDataSource2.Value = this.dTListaUniverzalnaBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.PorezNaDohodak.PorezNaDohodak.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(13, 65);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(792, 758);
            this.reportViewer1.TabIndex = 576;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(264, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 578;
            this.label1.Text = "Poslovnica:";
            // 
            // cbDucan
            // 
            this.cbDucan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDucan.FormattingEnabled = true;
            this.cbDucan.Location = new System.Drawing.Point(328, 17);
            this.cbDucan.Name = "cbDucan";
            this.cbDucan.Size = new System.Drawing.Size(206, 21);
            this.cbDucan.TabIndex = 579;
            // 
            // frmPorezNaDohodak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(816, 835);
            this.Controls.Add(this.cbDucan);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tdDoDatuma);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tdOdDatuma);
            this.Controls.Add(this.btnTrazi);
            this.Name = "frmPorezNaDohodak";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Porez na dohodak";
            this.Load += new System.EventHandler(this.frmListe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dTRpodaciTvrtkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTListaUniverzalnaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listaUniverzalna)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker tdDoDatuma;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker tdOdDatuma;
        private System.Windows.Forms.Button btnTrazi;
        private Dataset.ListaUniverzalna listaUniverzalna;
        private System.Windows.Forms.BindingSource dTListaUniverzalnaBindingSource;
        private Dataset.DSRpodaciTvrtke dSRpodaciTvrtke;
        private System.Windows.Forms.BindingSource dTRpodaciTvrtkeBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbDucan;
    }
}