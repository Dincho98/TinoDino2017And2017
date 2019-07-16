namespace PCPOS.Robno {
    partial class frmKnjigaPopisa {
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource6 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.listaUniverzalnaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.listaUniverzalna = new PCPOS.Dataset.ListaUniverzalna();
            this.dSRpodaciTvrtkeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSRpodaciTvrtke = new PCPOS.Dataset.DSRpodaciTvrtke();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnUcitaj = new System.Windows.Forms.Button();
            this.cmbSkladiste = new System.Windows.Forms.ComboBox();
            this.lblSkladiste = new System.Windows.Forms.Label();
            this.dtpDatumDo = new System.Windows.Forms.DateTimePicker();
            this.dtpDatumOd = new System.Windows.Forms.DateTimePicker();
            this.lblDatumDo = new System.Windows.Forms.Label();
            this.lblDatumOD = new System.Windows.Forms.Label();
            this.pnlFill = new System.Windows.Forms.Panel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.chbUzmiIUsluge = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.listaUniverzalnaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listaUniverzalna)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtkeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.pnlFill.SuspendLayout();
            this.SuspendLayout();
            // 
            // listaUniverzalnaBindingSource
            // 
            this.listaUniverzalnaBindingSource.DataMember = "DTListaUniverzalna";
            this.listaUniverzalnaBindingSource.DataSource = this.listaUniverzalna;
            // 
            // listaUniverzalna
            // 
            this.listaUniverzalna.DataSetName = "ListaUniverzalna";
            this.listaUniverzalna.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dSRpodaciTvrtkeBindingSource
            // 
            this.dSRpodaciTvrtkeBindingSource.DataMember = "DTRpodaciTvrtke";
            this.dSRpodaciTvrtkeBindingSource.DataSource = this.dSRpodaciTvrtke;
            // 
            // dSRpodaciTvrtke
            // 
            this.dSRpodaciTvrtke.DataSetName = "DSRpodaciTvrtke";
            this.dSRpodaciTvrtke.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.chbUzmiIUsluge);
            this.pnlTop.Controls.Add(this.btnUcitaj);
            this.pnlTop.Controls.Add(this.cmbSkladiste);
            this.pnlTop.Controls.Add(this.lblSkladiste);
            this.pnlTop.Controls.Add(this.dtpDatumDo);
            this.pnlTop.Controls.Add(this.dtpDatumOd);
            this.pnlTop.Controls.Add(this.lblDatumDo);
            this.pnlTop.Controls.Add(this.lblDatumOD);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(984, 100);
            this.pnlTop.TabIndex = 0;
            // 
            // btnUcitaj
            // 
            this.btnUcitaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnUcitaj.Image = global::PCPOS.Properties.Resources._10591;
            this.btnUcitaj.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUcitaj.Location = new System.Drawing.Point(887, 12);
            this.btnUcitaj.Name = "btnUcitaj";
            this.btnUcitaj.Size = new System.Drawing.Size(85, 40);
            this.btnUcitaj.TabIndex = 17;
            this.btnUcitaj.Text = "Učitaj";
            this.btnUcitaj.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUcitaj.UseVisualStyleBackColor = true;
            this.btnUcitaj.Click += new System.EventHandler(this.btnUcitaj_Click);
            // 
            // cmbSkladiste
            // 
            this.cmbSkladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSkladiste.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cmbSkladiste.FormattingEnabled = true;
            this.cmbSkladiste.Location = new System.Drawing.Point(372, 12);
            this.cmbSkladiste.Name = "cmbSkladiste";
            this.cmbSkladiste.Size = new System.Drawing.Size(200, 24);
            this.cmbSkladiste.TabIndex = 5;
            // 
            // lblSkladiste
            // 
            this.lblSkladiste.AutoSize = true;
            this.lblSkladiste.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblSkladiste.Location = new System.Drawing.Point(297, 15);
            this.lblSkladiste.Name = "lblSkladiste";
            this.lblSkladiste.Size = new System.Drawing.Size(69, 17);
            this.lblSkladiste.TabIndex = 4;
            this.lblSkladiste.Text = "Skladište:";
            // 
            // dtpDatumDo
            // 
            this.dtpDatumDo.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dtpDatumDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDatumDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatumDo.Location = new System.Drawing.Point(91, 41);
            this.dtpDatumDo.Name = "dtpDatumDo";
            this.dtpDatumDo.Size = new System.Drawing.Size(200, 23);
            this.dtpDatumDo.TabIndex = 3;
            // 
            // dtpDatumOd
            // 
            this.dtpDatumOd.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dtpDatumOd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDatumOd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatumOd.Location = new System.Drawing.Point(91, 12);
            this.dtpDatumOd.Name = "dtpDatumOd";
            this.dtpDatumOd.Size = new System.Drawing.Size(200, 23);
            this.dtpDatumOd.TabIndex = 2;
            // 
            // lblDatumDo
            // 
            this.lblDatumDo.AutoSize = true;
            this.lblDatumDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblDatumDo.Location = new System.Drawing.Point(12, 44);
            this.lblDatumDo.Name = "lblDatumDo";
            this.lblDatumDo.Size = new System.Drawing.Size(73, 17);
            this.lblDatumDo.TabIndex = 1;
            this.lblDatumDo.Text = "Datum do:";
            // 
            // lblDatumOD
            // 
            this.lblDatumOD.AutoSize = true;
            this.lblDatumOD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblDatumOD.Location = new System.Drawing.Point(12, 15);
            this.lblDatumOD.Name = "lblDatumOD";
            this.lblDatumOD.Size = new System.Drawing.Size(73, 17);
            this.lblDatumOD.TabIndex = 0;
            this.lblDatumOD.Text = "Datum od:";
            // 
            // pnlFill
            // 
            this.pnlFill.Controls.Add(this.reportViewer1);
            this.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFill.Location = new System.Drawing.Point(0, 100);
            this.pnlFill.Name = "pnlFill";
            this.pnlFill.Size = new System.Drawing.Size(984, 568);
            this.pnlFill.TabIndex = 1;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            reportDataSource5.Name = "dsUniverzalna";
            reportDataSource5.Value = this.listaUniverzalnaBindingSource;
            reportDataSource6.Name = "DTRpodaciTvrtke";
            reportDataSource6.Value = this.dSRpodaciTvrtkeBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource5);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource6);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.KnjigaPopisa.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 6);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(960, 550);
            this.reportViewer1.TabIndex = 0;
            // 
            // chbUzmiIUsluge
            // 
            this.chbUzmiIUsluge.AutoSize = true;
            this.chbUzmiIUsluge.Checked = true;
            this.chbUzmiIUsluge.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbUzmiIUsluge.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chbUzmiIUsluge.Location = new System.Drawing.Point(372, 42);
            this.chbUzmiIUsluge.Name = "chbUzmiIUsluge";
            this.chbUzmiIUsluge.Size = new System.Drawing.Size(111, 21);
            this.chbUzmiIUsluge.TabIndex = 18;
            this.chbUzmiIUsluge.Text = "Uzmi i usluge";
            this.chbUzmiIUsluge.UseVisualStyleBackColor = true;
            // 
            // frmKnjigaPopisa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(984, 668);
            this.Controls.Add(this.pnlFill);
            this.Controls.Add(this.pnlTop);
            this.Name = "frmKnjigaPopisa";
            this.Text = "frmKnjigaPopisa";
            this.Load += new System.EventHandler(this.frmKnjigaPopisa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.listaUniverzalnaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listaUniverzalna)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtkeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSRpodaciTvrtke)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlFill.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlFill;
        private System.Windows.Forms.Label lblDatumDo;
        private System.Windows.Forms.Label lblDatumOD;
        private System.Windows.Forms.DateTimePicker dtpDatumDo;
        private System.Windows.Forms.DateTimePicker dtpDatumOd;
        private System.Windows.Forms.ComboBox cmbSkladiste;
        private System.Windows.Forms.Label lblSkladiste;
        private System.Windows.Forms.Button btnUcitaj;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource listaUniverzalnaBindingSource;
        private Dataset.ListaUniverzalna listaUniverzalna;
        private System.Windows.Forms.BindingSource dSRpodaciTvrtkeBindingSource;
        private Dataset.DSRpodaciTvrtke dSRpodaciTvrtke;
        private System.Windows.Forms.CheckBox chbUzmiIUsluge;
    }
}