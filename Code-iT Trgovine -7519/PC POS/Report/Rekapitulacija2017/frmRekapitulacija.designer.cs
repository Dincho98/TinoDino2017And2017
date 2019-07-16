namespace PCPOS.Report.Rekapitulacija2017
{
    partial class frmRekapitulacija
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
            this.chbZbirno = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSkladiste = new System.Windows.Forms.ComboBox();
            this.rbVPC = new System.Windows.Forms.RadioButton();
            this.rbMpc = new System.Windows.Forms.RadioButton();
            this.chbRabat = new System.Windows.Forms.CheckBox();
            this.chbUzmiUsluge = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbDokumenti = new System.Windows.Forms.ComboBox();
            this.clbSkladiste = new System.Windows.Forms.CheckedListBox();
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
            this.label3.Location = new System.Drawing.Point(12, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 575;
            this.label3.Text = "Do datuma:";
            // 
            // tdDoDatuma
            // 
            this.tdDoDatuma.CustomFormat = "dd.MM.yyyy H:mm:ss";
            this.tdDoDatuma.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tdDoDatuma.Location = new System.Drawing.Point(80, 38);
            this.tdDoDatuma.Name = "tdDoDatuma";
            this.tdDoDatuma.Size = new System.Drawing.Size(167, 20);
            this.tdDoDatuma.TabIndex = 574;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 573;
            this.label2.Text = "Od datuma:";
            // 
            // tdOdDatuma
            // 
            this.tdOdDatuma.CustomFormat = "dd.MM.yyyy H:mm:ss";
            this.tdOdDatuma.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tdOdDatuma.Location = new System.Drawing.Point(80, 12);
            this.tdOdDatuma.Name = "tdOdDatuma";
            this.tdOdDatuma.Size = new System.Drawing.Size(167, 20);
            this.tdOdDatuma.TabIndex = 572;
            // 
            // btnTrazi
            // 
            this.btnTrazi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnTrazi.Image = global::PCPOS.Properties.Resources._10591;
            this.btnTrazi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTrazi.Location = new System.Drawing.Point(709, 12);
            this.btnTrazi.Name = "btnTrazi";
            this.btnTrazi.Size = new System.Drawing.Size(96, 54);
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
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Rekapitulacija2015.Rekapitulacija.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 111);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(793, 619);
            this.reportViewer1.TabIndex = 576;
            // 
            // chbZbirno
            // 
            this.chbZbirno.AutoSize = true;
            this.chbZbirno.Checked = true;
            this.chbZbirno.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbZbirno.Location = new System.Drawing.Point(530, 12);
            this.chbZbirno.Name = "chbZbirno";
            this.chbZbirno.Size = new System.Drawing.Size(144, 17);
            this.chbZbirno.TabIndex = 577;
            this.chbZbirno.Text = "Zbirno prema dokumentu";
            this.chbZbirno.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(540, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 578;
            this.label1.Text = "Skladište:";
            // 
            // cbSkladiste
            // 
            this.cbSkladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkladiste.FormattingEnabled = true;
            this.cbSkladiste.Location = new System.Drawing.Point(599, 81);
            this.cbSkladiste.Name = "cbSkladiste";
            this.cbSkladiste.Size = new System.Drawing.Size(206, 21);
            this.cbSkladiste.TabIndex = 579;
            // 
            // rbVPC
            // 
            this.rbVPC.AutoSize = true;
            this.rbVPC.Location = new System.Drawing.Point(12, 64);
            this.rbVPC.Name = "rbVPC";
            this.rbVPC.Size = new System.Drawing.Size(77, 17);
            this.rbVPC.TabIndex = 580;
            this.rbVPC.Text = "VPC cijene";
            this.rbVPC.UseVisualStyleBackColor = true;
            // 
            // rbMpc
            // 
            this.rbMpc.AutoSize = true;
            this.rbMpc.Checked = true;
            this.rbMpc.Location = new System.Drawing.Point(168, 64);
            this.rbMpc.Name = "rbMpc";
            this.rbMpc.Size = new System.Drawing.Size(79, 17);
            this.rbMpc.TabIndex = 581;
            this.rbMpc.TabStop = true;
            this.rbMpc.Text = "MPC cijene";
            this.rbMpc.UseVisualStyleBackColor = true;
            // 
            // chbRabat
            // 
            this.chbRabat.AutoSize = true;
            this.chbRabat.Location = new System.Drawing.Point(530, 58);
            this.chbRabat.Name = "chbRabat";
            this.chbRabat.Size = new System.Drawing.Size(160, 17);
            this.chbRabat.TabIndex = 582;
            this.chbRabat.Text = "Uzmi u obzir prodajne rabate";
            this.chbRabat.UseVisualStyleBackColor = true;
            // 
            // chbUzmiUsluge
            // 
            this.chbUzmiUsluge.AutoSize = true;
            this.chbUzmiUsluge.Location = new System.Drawing.Point(530, 35);
            this.chbUzmiUsluge.Name = "chbUzmiUsluge";
            this.chbUzmiUsluge.Size = new System.Drawing.Size(117, 17);
            this.chbUzmiUsluge.TabIndex = 583;
            this.chbUzmiUsluge.Text = "Uzmi u obzir usluge";
            this.chbUzmiUsluge.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 584;
            this.label4.Text = "Dokument:";
            // 
            // cmbDokumenti
            // 
            this.cmbDokumenti.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDokumenti.FormattingEnabled = true;
            this.cmbDokumenti.Location = new System.Drawing.Point(80, 84);
            this.cmbDokumenti.Name = "cmbDokumenti";
            this.cmbDokumenti.Size = new System.Drawing.Size(167, 21);
            this.cmbDokumenti.TabIndex = 585;
            // 
            // clbSkladiste
            // 
            this.clbSkladiste.CheckOnClick = true;
            this.clbSkladiste.FormattingEnabled = true;
            this.clbSkladiste.Location = new System.Drawing.Point(253, 12);
            this.clbSkladiste.Name = "clbSkladiste";
            this.clbSkladiste.Size = new System.Drawing.Size(271, 94);
            this.clbSkladiste.TabIndex = 586;
            this.clbSkladiste.ThreeDCheckBoxes = true;
            // 
            // frmRekapitulacija
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(817, 742);
            this.Controls.Add(this.clbSkladiste);
            this.Controls.Add(this.cmbDokumenti);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chbUzmiUsluge);
            this.Controls.Add(this.chbRabat);
            this.Controls.Add(this.rbMpc);
            this.Controls.Add(this.rbVPC);
            this.Controls.Add(this.cbSkladiste);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chbZbirno);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tdDoDatuma);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tdOdDatuma);
            this.Controls.Add(this.btnTrazi);
            this.Name = "frmRekapitulacija";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rekapitulacija analitika";
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
        private System.Windows.Forms.CheckBox chbZbirno;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSkladiste;
        private System.Windows.Forms.RadioButton rbVPC;
        private System.Windows.Forms.RadioButton rbMpc;
        private System.Windows.Forms.CheckBox chbRabat;
        private System.Windows.Forms.CheckBox chbUzmiUsluge;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbDokumenti;
        private System.Windows.Forms.CheckedListBox clbSkladiste;
    }
}