namespace PCPOS
{
    partial class Izlracuni
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
            this.txtODRac = new System.Windows.Forms.TextBox();
            this.txtDORac = new System.Windows.Forms.TextBox();
            this.cmbDokument = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnIspis = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.cbsklkalk = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtPartner = new System.Windows.Forms.TextBox();
            this.btnSrchPartner = new System.Windows.Forms.Button();
            this.txtPartnerNaziv = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gbPartner = new System.Windows.Forms.GroupBox();
            this.chbSamoPouzecem = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gbPartner.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtODRac
            // 
            this.txtODRac.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtODRac.Location = new System.Drawing.Point(109, 19);
            this.txtODRac.Name = "txtODRac";
            this.txtODRac.Size = new System.Drawing.Size(137, 22);
            this.txtODRac.TabIndex = 0;
            this.txtODRac.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // txtDORac
            // 
            this.txtDORac.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtDORac.Location = new System.Drawing.Point(109, 47);
            this.txtDORac.Name = "txtDORac";
            this.txtDORac.Size = new System.Drawing.Size(137, 22);
            this.txtDORac.TabIndex = 1;
            // 
            // cmbDokument
            // 
            this.cmbDokument.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDokument.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cmbDokument.FormattingEnabled = true;
            this.cmbDokument.Location = new System.Drawing.Point(171, 9);
            this.cmbDokument.Name = "cmbDokument";
            this.cmbDokument.Size = new System.Drawing.Size(237, 24);
            this.cmbDokument.TabIndex = 2;
            this.cmbDokument.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F);
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Odaberite izlaznu listu:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Od računa :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(6, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Do računa :";
            // 
            // btnIspis
            // 
            this.btnIspis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIspis.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnIspis.Location = new System.Drawing.Point(509, 160);
            this.btnIspis.Name = "btnIspis";
            this.btnIspis.Size = new System.Drawing.Size(82, 52);
            this.btnIspis.TabIndex = 6;
            this.btnIspis.Text = "Ispiši na ekranu";
            this.btnIspis.UseVisualStyleBackColor = true;
            this.btnIspis.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(6, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Do datuma :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(6, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Od datuma :";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dateTimePicker1.Location = new System.Drawing.Point(126, 19);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(137, 22);
            this.dateTimePicker1.TabIndex = 11;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dateTimePicker2.Location = new System.Drawing.Point(126, 47);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(137, 22);
            this.dateTimePicker2.TabIndex = 12;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Location = new System.Drawing.Point(9, 0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.BackColor = System.Drawing.Color.Transparent;
            this.checkBox2.Location = new System.Drawing.Point(9, 0);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 14;
            this.checkBox2.UseVisualStyleBackColor = false;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(6, 29);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(239, 23);
            this.comboBox2.TabIndex = 17;
            // 
            // cbsklkalk
            // 
            this.cbsklkalk.AutoSize = true;
            this.cbsklkalk.BackColor = System.Drawing.Color.Transparent;
            this.cbsklkalk.Location = new System.Drawing.Point(8, -1);
            this.cbsklkalk.Name = "cbsklkalk";
            this.cbsklkalk.Size = new System.Drawing.Size(15, 14);
            this.cbsklkalk.TabIndex = 18;
            this.cbsklkalk.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(6, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(175, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Kod kalkulacija upisujete njihov ID !";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtODRac);
            this.groupBox1.Controls.Add(this.txtDORac);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 91);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "     Brojevi";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.checkBox2);
            this.groupBox2.Controls.Add(this.dateTimePicker1);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.dateTimePicker2);
            this.groupBox2.Location = new System.Drawing.Point(322, 37);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(269, 91);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "     Datum";
            // 
            // txtPartner
            // 
            this.txtPartner.Location = new System.Drawing.Point(6, 20);
            this.txtPartner.Name = "txtPartner";
            this.txtPartner.Size = new System.Drawing.Size(100, 20);
            this.txtPartner.TabIndex = 22;
            this.txtPartner.TextChanged += new System.EventHandler(this.txtPartner_TextChanged);
            this.txtPartner.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPartner_KeyPress);
            // 
            // btnSrchPartner
            // 
            this.btnSrchPartner.BackColor = System.Drawing.Color.Transparent;
            this.btnSrchPartner.BackgroundImage = global::PCPOS.Properties.Resources._1059;
            this.btnSrchPartner.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSrchPartner.FlatAppearance.BorderSize = 0;
            this.btnSrchPartner.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSrchPartner.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSrchPartner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSrchPartner.Location = new System.Drawing.Point(112, 18);
            this.btnSrchPartner.Name = "btnSrchPartner";
            this.btnSrchPartner.Size = new System.Drawing.Size(46, 23);
            this.btnSrchPartner.TabIndex = 23;
            this.btnSrchPartner.UseVisualStyleBackColor = false;
            this.btnSrchPartner.Click += new System.EventHandler(this.btnSrchPartner_Click);
            // 
            // txtPartnerNaziv
            // 
            this.txtPartnerNaziv.Enabled = false;
            this.txtPartnerNaziv.Location = new System.Drawing.Point(6, 45);
            this.txtPartnerNaziv.Name = "txtPartnerNaziv";
            this.txtPartnerNaziv.Size = new System.Drawing.Size(169, 20);
            this.txtPartnerNaziv.TabIndex = 24;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.comboBox2);
            this.groupBox3.Controls.Add(this.cbsklkalk);
            this.groupBox3.Location = new System.Drawing.Point(13, 135);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(254, 77);
            this.groupBox3.TabIndex = 25;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "     Skladište";
            // 
            // gbPartner
            // 
            this.gbPartner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPartner.BackColor = System.Drawing.Color.Transparent;
            this.gbPartner.Controls.Add(this.txtPartner);
            this.gbPartner.Controls.Add(this.btnSrchPartner);
            this.gbPartner.Controls.Add(this.txtPartnerNaziv);
            this.gbPartner.Location = new System.Drawing.Point(322, 134);
            this.gbPartner.Name = "gbPartner";
            this.gbPartner.Size = new System.Drawing.Size(181, 78);
            this.gbPartner.TabIndex = 26;
            this.gbPartner.TabStop = false;
            this.gbPartner.Text = "Partner";
            // 
            // chbSamoPouzecem
            // 
            this.chbSamoPouzecem.AutoSize = true;
            this.chbSamoPouzecem.Location = new System.Drawing.Point(448, 13);
            this.chbSamoPouzecem.Name = "chbSamoPouzecem";
            this.chbSamoPouzecem.Size = new System.Drawing.Size(105, 17);
            this.chbSamoPouzecem.TabIndex = 27;
            this.chbSamoPouzecem.Text = "Samo pouzećem";
            this.chbSamoPouzecem.UseVisualStyleBackColor = true;
            this.chbSamoPouzecem.Visible = false;
            // 
            // Izlracuni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 223);
            this.Controls.Add(this.chbSamoPouzecem);
            this.Controls.Add(this.gbPartner);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnIspis);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbDokument);
            this.MaximumSize = new System.Drawing.Size(619, 262);
            this.MinimumSize = new System.Drawing.Size(619, 262);
            this.Name = "Izlracuni";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Liste izlaznih računa";
            this.Load += new System.EventHandler(this.Izlracuni_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Izlracuni_Paint);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.gbPartner.ResumeLayout(false);
            this.gbPartner.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtODRac;
        private System.Windows.Forms.TextBox txtDORac;
        private System.Windows.Forms.ComboBox cmbDokument;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnIspis;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.CheckBox cbsklkalk;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtPartner;
        private System.Windows.Forms.Button btnSrchPartner;
        private System.Windows.Forms.TextBox txtPartnerNaziv;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox gbPartner;
        private System.Windows.Forms.CheckBox chbSamoPouzecem;
    }
}