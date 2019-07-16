namespace PCPOS.Report.Naljepnice
{
    partial class NaljepniceEan

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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NaljepniceEan));
            this.dTListaUniverzalnaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.listaUniverzalna = new PCPOS.Dataset.ListaUniverzalna();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNaslov = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRedak = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRedak2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtRedak3 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtBrojNaljepnica = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnKreiraj = new System.Windows.Forms.Button();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnPartner = new System.Windows.Forms.PictureBox();
            this.txtZapocniOdBroja = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dTListaUniverzalnaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listaUniverzalna)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPartner)).BeginInit();
            this.SuspendLayout();
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Naslov:";
            // 
            // txtNaslov
            // 
            this.txtNaslov.Location = new System.Drawing.Point(51, 33);
            this.txtNaslov.MaxLength = 30;
            this.txtNaslov.Name = "txtNaslov";
            this.txtNaslov.Size = new System.Drawing.Size(157, 20);
            this.txtNaslov.TabIndex = 1;
            this.txtNaslov.Text = "Dječja majca";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(221, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Max(25)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(221, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Max(30)";
            // 
            // txtSifra
            // 
            this.txtSifra.Location = new System.Drawing.Point(51, 7);
            this.txtSifra.MaxLength = 25;
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(132, 20);
            this.txtSifra.TabIndex = 0;
            this.txtSifra.Text = "2070600461";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Šifra:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(488, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Max(30)";
            // 
            // txtRedak
            // 
            this.txtRedak.Location = new System.Drawing.Point(325, 7);
            this.txtRedak.MaxLength = 30;
            this.txtRedak.Name = "txtRedak";
            this.txtRedak.Size = new System.Drawing.Size(157, 20);
            this.txtRedak.TabIndex = 2;
            this.txtRedak.Text = "Artikal: 456545";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(275, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Redak 1:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(488, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Max(30)";
            // 
            // txtRedak2
            // 
            this.txtRedak2.Location = new System.Drawing.Point(325, 28);
            this.txtRedak2.MaxLength = 30;
            this.txtRedak2.Name = "txtRedak2";
            this.txtRedak2.Size = new System.Drawing.Size(157, 20);
            this.txtRedak2.TabIndex = 3;
            this.txtRedak2.Text = "Veličina: 46";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(275, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Redak 2:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(488, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Max(30)";
            this.label9.Visible = false;
            // 
            // txtRedak3
            // 
            this.txtRedak3.Location = new System.Drawing.Point(325, 49);
            this.txtRedak3.MaxLength = 30;
            this.txtRedak3.Name = "txtRedak3";
            this.txtRedak3.Size = new System.Drawing.Size(157, 20);
            this.txtRedak3.TabIndex = 4;
            this.txtRedak3.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(275, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Redak 3:";
            this.label10.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 58);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(146, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Prema šifri generira se barkod";
            // 
            // txtBrojNaljepnica
            // 
            this.txtBrojNaljepnica.Location = new System.Drawing.Point(726, 6);
            this.txtBrojNaljepnica.Name = "txtBrojNaljepnica";
            this.txtBrojNaljepnica.Size = new System.Drawing.Size(58, 20);
            this.txtBrojNaljepnica.TabIndex = 5;
            this.txtBrojNaljepnica.Text = "10";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(641, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 13);
            this.label12.TabIndex = 19;
            this.label12.Text = "Broj naljepnica:";
            // 
            // btnKreiraj
            // 
            this.btnKreiraj.Location = new System.Drawing.Point(633, 51);
            this.btnKreiraj.Name = "btnKreiraj";
            this.btnKreiraj.Size = new System.Drawing.Size(151, 26);
            this.btnKreiraj.TabIndex = 7;
            this.btnKreiraj.Text = "Kreiraj";
            this.btnKreiraj.UseVisualStyleBackColor = true;
            this.btnKreiraj.Click += new System.EventHandler(this.btnKreiraj_Click);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource4.Name = "DSLista";
            reportDataSource4.Value = this.dTListaUniverzalnaBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PCPOS.Report.Naljepnice.NaljepniceEan.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(15, 83);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(786, 679);
            this.reportViewer1.TabIndex = 8;
            // 
            // btnPartner
            // 
            this.btnPartner.BackColor = System.Drawing.Color.Transparent;
            this.btnPartner.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPartner.Image = ((System.Drawing.Image)(resources.GetObject("btnPartner.Image")));
            this.btnPartner.Location = new System.Drawing.Point(184, 3);
            this.btnPartner.Name = "btnPartner";
            this.btnPartner.Size = new System.Drawing.Size(31, 28);
            this.btnPartner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnPartner.TabIndex = 558;
            this.btnPartner.TabStop = false;
            this.btnPartner.Click += new System.EventHandler(this.btnPartner_Click);
            // 
            // txtZapocniOdBroja
            // 
            this.txtZapocniOdBroja.Location = new System.Drawing.Point(726, 28);
            this.txtZapocniOdBroja.Name = "txtZapocniOdBroja";
            this.txtZapocniOdBroja.Size = new System.Drawing.Size(58, 20);
            this.txtZapocniOdBroja.TabIndex = 6;
            this.txtZapocniOdBroja.Text = "1";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(630, 31);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(90, 13);
            this.label13.TabIndex = 20;
            this.label13.Text = "Započni od broja:";
            // 
            // NaljepniceEan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(810, 774);
            this.Controls.Add(this.txtZapocniOdBroja);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnPartner);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.btnKreiraj);
            this.Controls.Add(this.txtBrojNaljepnica);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtRedak3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtRedak2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtRedak);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSifra);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNaslov);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(826, 812);
            this.MinimumSize = new System.Drawing.Size(826, 812);
            this.Name = "NaljepniceEan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Naljepnice";
            this.Load += new System.EventHandler(this.frmNaljepnice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dTListaUniverzalnaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listaUniverzalna)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPartner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRedak;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRedak2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtRedak3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtBrojNaljepnica;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnKreiraj;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Dataset.ListaUniverzalna listaUniverzalna;
        private System.Windows.Forms.BindingSource dTListaUniverzalnaBindingSource;
        private System.Windows.Forms.TextBox txtNaslov;
        private System.Windows.Forms.PictureBox btnPartner;
        private System.Windows.Forms.TextBox txtZapocniOdBroja;
        private System.Windows.Forms.Label label13;
	}
}