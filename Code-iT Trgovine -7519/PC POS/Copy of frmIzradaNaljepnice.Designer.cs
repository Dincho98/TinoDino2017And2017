namespace PCPOS
{
    partial class frmBarkodeServis
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.btnUcitaj = new System.Windows.Forms.Button();
            this.btnIspis = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.lblStart = new System.Windows.Forms.Label();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.txtPocetak = new System.Windows.Forms.NumericUpDown();
            this.listaUniverzalna = new PCPOS.Dataset.ListaUniverzalna();
            this.DTListaUniverzalnaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtPocetak)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listaUniverzalna)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTListaUniverzalnaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUcitaj
            // 
            this.btnUcitaj.BackColor = System.Drawing.Color.Transparent;
            this.btnUcitaj.BackgroundImage = global::PCPOS.Properties.Resources.dff;
            this.btnUcitaj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUcitaj.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUcitaj.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnUcitaj.FlatAppearance.BorderSize = 0;
            this.btnUcitaj.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnUcitaj.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUcitaj.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUcitaj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUcitaj.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.btnUcitaj.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnUcitaj.Location = new System.Drawing.Point(559, 12);
            this.btnUcitaj.Name = "btnUcitaj";
            this.btnUcitaj.Size = new System.Drawing.Size(111, 36);
            this.btnUcitaj.TabIndex = 3;
            this.btnUcitaj.TabStop = false;
            this.btnUcitaj.Text = "Učitaj";
            this.btnUcitaj.UseVisualStyleBackColor = false;
            this.btnUcitaj.Visible = false;
            this.btnUcitaj.Click += new System.EventHandler(this.btnUcitaj_Click);
            // 
            // btnIspis
            // 
            this.btnIspis.BackColor = System.Drawing.Color.Transparent;
            this.btnIspis.BackgroundImage = global::PCPOS.Properties.Resources.dff;
            this.btnIspis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIspis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIspis.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnIspis.FlatAppearance.BorderSize = 0;
            this.btnIspis.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnIspis.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnIspis.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnIspis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIspis.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.btnIspis.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnIspis.Location = new System.Drawing.Point(676, 12);
            this.btnIspis.Name = "btnIspis";
            this.btnIspis.Size = new System.Drawing.Size(111, 36);
            this.btnIspis.TabIndex = 4;
            this.btnIspis.TabStop = false;
            this.btnIspis.Text = "Ispiši";
            this.btnIspis.UseVisualStyleBackColor = false;
            this.btnIspis.Click += new System.EventHandler(this.btnIspis_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.BackColor = System.Drawing.Color.Transparent;
            this.radioButton1.Location = new System.Drawing.Point(356, 11);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(81, 17);
            this.radioButton1.TabIndex = 107;
            this.radioButton1.Text = "po 3 stupca";
            this.radioButton1.UseVisualStyleBackColor = false;
            this.radioButton1.Visible = false;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.BackColor = System.Drawing.Color.Transparent;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(356, 24);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(81, 17);
            this.radioButton2.TabIndex = 108;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "po 4 stupca";
            this.radioButton2.UseVisualStyleBackColor = false;
            this.radioButton2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(319, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 111;
            this.label3.Text = "Ispis:";
            this.label3.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(448, 9);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(74, 17);
            this.checkBox1.TabIndex = 122;
            this.checkBox1.Text = "16 redova";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.Visible = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.BackColor = System.Drawing.Color.Transparent;
            this.checkBox2.Location = new System.Drawing.Point(448, 24);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(74, 17);
            this.checkBox2.TabIndex = 123;
            this.checkBox2.Text = "17 redova";
            this.checkBox2.UseVisualStyleBackColor = false;
            this.checkBox2.Visible = false;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.BackColor = System.Drawing.Color.Transparent;
            this.lblStart.Location = new System.Drawing.Point(13, 13);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(64, 13);
            this.lblStart.TabIndex = 125;
            this.lblStart.Text = "Započni od:";
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource2.Name = "DSLista";
            reportDataSource2.Value = this.DTListaUniverzalnaBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Naljepnice.NaljepniceServisEan.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 58);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(775, 632);
            this.reportViewer1.TabIndex = 126;
            // 
            // txtPocetak
            // 
            this.txtPocetak.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPocetak.Location = new System.Drawing.Point(12, 29);
            this.txtPocetak.Name = "txtPocetak";
            this.txtPocetak.Size = new System.Drawing.Size(120, 23);
            this.txtPocetak.TabIndex = 127;
            this.txtPocetak.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPocetak_KeyDown_1);
            // 
            // listaUniverzalna
            // 
            this.listaUniverzalna.DataSetName = "ListaUniverzalna";
            this.listaUniverzalna.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // DTListaUniverzalnaBindingSource
            // 
            this.DTListaUniverzalnaBindingSource.DataMember = "DTListaUniverzalna";
            this.DTListaUniverzalnaBindingSource.DataSource = this.listaUniverzalna;
            // 
            // frmBarkodeServis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 702);
            this.Controls.Add(this.txtPocetak);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.btnIspis);
            this.Controls.Add(this.btnUcitaj);
            this.Name = "frmBarkodeServis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Izrada naljepnice";
            this.Load += new System.EventHandler(this.frmIzradaNaljepnice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtPocetak)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listaUniverzalna)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DTListaUniverzalnaBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Button btnUcitaj;
        private System.Windows.Forms.Button btnIspis;
		private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label lblStart;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.NumericUpDown txtPocetak;
        private Dataset.ListaUniverzalna listaUniverzalna;
        private System.Windows.Forms.BindingSource DTListaUniverzalnaBindingSource;
	}
}