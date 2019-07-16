namespace PCPOS.Kasa
{
    partial class frmPrometKase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrometKase));
            this.cbDucan = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.cbSkladiste = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbZaposlenik = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDO = new System.Windows.Forms.Label();
            this.dtpDO = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpOD = new System.Windows.Forms.DateTimePicker();
            this.chbDucan = new System.Windows.Forms.CheckBox();
            this.chbSkladiste = new System.Windows.Forms.CheckBox();
            this.chbBlagajnik = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbdobavljaci = new System.Windows.Forms.ComboBox();
            this.chbdobavljaci = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbGrupe = new System.Windows.Forms.ComboBox();
            this.chbGrupe = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnIspisPOS = new System.Windows.Forms.Button();
            this.btnIspis = new System.Windows.Forms.Button();
            this.cbKasa = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chbKasa = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbDucan
            // 
            this.cbDucan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbDucan.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDucan.BackColor = System.Drawing.Color.White;
            this.cbDucan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDucan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbDucan.FormattingEnabled = true;
            this.cbDucan.Location = new System.Drawing.Point(29, 133);
            this.cbDucan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbDucan.Name = "cbDucan";
            this.cbDucan.Size = new System.Drawing.Size(234, 24);
            this.cbDucan.TabIndex = 3;
            this.cbDucan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpOD_KeyDown);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Font = new System.Drawing.Font("Arial", 9F);
            this.label28.Location = new System.Drawing.Point(29, 118);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(43, 15);
            this.label28.TabIndex = 32;
            this.label28.Text = "Dućan";
            // 
            // cbSkladiste
            // 
            this.cbSkladiste.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbSkladiste.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbSkladiste.BackColor = System.Drawing.Color.White;
            this.cbSkladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkladiste.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbSkladiste.FormattingEnabled = true;
            this.cbSkladiste.Location = new System.Drawing.Point(29, 224);
            this.cbSkladiste.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbSkladiste.Name = "cbSkladiste";
            this.cbSkladiste.Size = new System.Drawing.Size(234, 24);
            this.cbSkladiste.TabIndex = 4;
            this.cbSkladiste.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpOD_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 9F);
            this.label1.Location = new System.Drawing.Point(29, 209);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 32;
            this.label1.Text = "Skladište";
            // 
            // cbZaposlenik
            // 
            this.cbZaposlenik.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbZaposlenik.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbZaposlenik.BackColor = System.Drawing.Color.White;
            this.cbZaposlenik.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbZaposlenik.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbZaposlenik.FormattingEnabled = true;
            this.cbZaposlenik.Location = new System.Drawing.Point(29, 354);
            this.cbZaposlenik.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbZaposlenik.Name = "cbZaposlenik";
            this.cbZaposlenik.Size = new System.Drawing.Size(234, 24);
            this.cbZaposlenik.TabIndex = 5;
            this.cbZaposlenik.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpOD_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 9F);
            this.label2.Location = new System.Drawing.Point(29, 338);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 15);
            this.label2.TabIndex = 32;
            this.label2.Text = "Blagajnik";
            // 
            // lblDO
            // 
            this.lblDO.AutoSize = true;
            this.lblDO.BackColor = System.Drawing.Color.Transparent;
            this.lblDO.Font = new System.Drawing.Font("Arial", 9F);
            this.lblDO.Location = new System.Drawing.Point(29, 77);
            this.lblDO.Name = "lblDO";
            this.lblDO.Size = new System.Drawing.Size(85, 15);
            this.lblDO.TabIndex = 41;
            this.lblDO.Text = "Završni datum";
            // 
            // dtpDO
            // 
            this.dtpDO.CustomFormat = "dd.MM.yyyy H:mm";
            this.dtpDO.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDO.Location = new System.Drawing.Point(29, 92);
            this.dtpDO.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpDO.Name = "dtpDO";
            this.dtpDO.Size = new System.Drawing.Size(233, 22);
            this.dtpDO.TabIndex = 2;
            this.dtpDO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpOD_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 9F);
            this.label3.Location = new System.Drawing.Point(29, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 15);
            this.label3.TabIndex = 39;
            this.label3.Text = "Početni datum";
            // 
            // dtpOD
            // 
            this.dtpOD.CustomFormat = "dd.MM.yyyy H:mm";
            this.dtpOD.Font = new System.Drawing.Font("Arial", 9.75F);
            this.dtpOD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOD.Location = new System.Drawing.Point(29, 52);
            this.dtpOD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpOD.Name = "dtpOD";
            this.dtpOD.Size = new System.Drawing.Size(233, 22);
            this.dtpOD.TabIndex = 1;
            this.dtpOD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpOD_KeyDown);
            // 
            // chbDucan
            // 
            this.chbDucan.AutoSize = true;
            this.chbDucan.BackColor = System.Drawing.Color.Transparent;
            this.chbDucan.Location = new System.Drawing.Point(268, 138);
            this.chbDucan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chbDucan.Name = "chbDucan";
            this.chbDucan.Size = new System.Drawing.Size(15, 14);
            this.chbDucan.TabIndex = 8;
            this.chbDucan.UseVisualStyleBackColor = false;
            this.chbDucan.CheckedChanged += new System.EventHandler(this.chbDucan_CheckedChanged);
            this.chbDucan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpOD_KeyDown);
            // 
            // chbSkladiste
            // 
            this.chbSkladiste.AutoSize = true;
            this.chbSkladiste.BackColor = System.Drawing.Color.Transparent;
            this.chbSkladiste.Location = new System.Drawing.Point(268, 229);
            this.chbSkladiste.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chbSkladiste.Name = "chbSkladiste";
            this.chbSkladiste.Size = new System.Drawing.Size(15, 14);
            this.chbSkladiste.TabIndex = 9;
            this.chbSkladiste.UseVisualStyleBackColor = false;
            this.chbSkladiste.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpOD_KeyDown);
            // 
            // chbBlagajnik
            // 
            this.chbBlagajnik.AutoSize = true;
            this.chbBlagajnik.BackColor = System.Drawing.Color.Transparent;
            this.chbBlagajnik.Location = new System.Drawing.Point(267, 357);
            this.chbBlagajnik.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chbBlagajnik.Name = "chbBlagajnik";
            this.chbBlagajnik.Size = new System.Drawing.Size(15, 14);
            this.chbBlagajnik.TabIndex = 10;
            this.chbBlagajnik.UseVisualStyleBackColor = false;
            this.chbBlagajnik.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpOD_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.cbKasa);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.chbKasa);
            this.groupBox1.Controls.Add(this.cbdobavljaci);
            this.groupBox1.Controls.Add(this.chbdobavljaci);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbGrupe);
            this.groupBox1.Controls.Add(this.chbGrupe);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbDucan);
            this.groupBox1.Controls.Add(this.btnIspisPOS);
            this.groupBox1.Controls.Add(this.cbSkladiste);
            this.groupBox1.Controls.Add(this.btnIspis);
            this.groupBox1.Controls.Add(this.cbZaposlenik);
            this.groupBox1.Controls.Add(this.chbBlagajnik);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.chbSkladiste);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chbDucan);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblDO);
            this.groupBox1.Controls.Add(this.dtpOD);
            this.groupBox1.Controls.Add(this.dtpDO);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox1.Location = new System.Drawing.Point(21, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 475);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opcionalni ispis prometa";
            // 
            // cbdobavljaci
            // 
            this.cbdobavljaci.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbdobavljaci.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbdobavljaci.BackColor = System.Drawing.Color.White;
            this.cbdobavljaci.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbdobavljaci.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbdobavljaci.FormattingEnabled = true;
            this.cbdobavljaci.Location = new System.Drawing.Point(29, 310);
            this.cbdobavljaci.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbdobavljaci.Name = "cbdobavljaci";
            this.cbdobavljaci.Size = new System.Drawing.Size(234, 24);
            this.cbdobavljaci.TabIndex = 47;
            // 
            // chbdobavljaci
            // 
            this.chbdobavljaci.AutoSize = true;
            this.chbdobavljaci.BackColor = System.Drawing.Color.Transparent;
            this.chbdobavljaci.Location = new System.Drawing.Point(269, 315);
            this.chbdobavljaci.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chbdobavljaci.Name = "chbdobavljaci";
            this.chbdobavljaci.Size = new System.Drawing.Size(15, 14);
            this.chbdobavljaci.TabIndex = 48;
            this.chbdobavljaci.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Arial", 9F);
            this.label5.Location = new System.Drawing.Point(29, 295);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 15);
            this.label5.TabIndex = 49;
            this.label5.Text = "Dobavljači";
            // 
            // cbGrupe
            // 
            this.cbGrupe.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbGrupe.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbGrupe.BackColor = System.Drawing.Color.White;
            this.cbGrupe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGrupe.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbGrupe.FormattingEnabled = true;
            this.cbGrupe.Location = new System.Drawing.Point(29, 267);
            this.cbGrupe.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbGrupe.Name = "cbGrupe";
            this.cbGrupe.Size = new System.Drawing.Size(234, 24);
            this.cbGrupe.TabIndex = 44;
            // 
            // chbGrupe
            // 
            this.chbGrupe.AutoSize = true;
            this.chbGrupe.BackColor = System.Drawing.Color.Transparent;
            this.chbGrupe.Location = new System.Drawing.Point(269, 272);
            this.chbGrupe.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chbGrupe.Name = "chbGrupe";
            this.chbGrupe.Size = new System.Drawing.Size(15, 14);
            this.chbGrupe.TabIndex = 45;
            this.chbGrupe.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Arial", 9F);
            this.label4.Location = new System.Drawing.Point(29, 252);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 15);
            this.label4.TabIndex = 46;
            this.label4.Text = "Grupe";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkBox1.Location = new System.Drawing.Point(27, 385);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(98, 20);
            this.checkBox1.TabIndex = 43;
            this.checkBox1.Text = "Ispiši stavke";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // btnIspisPOS
            // 
            this.btnIspisPOS.BackColor = System.Drawing.Color.Transparent;
            this.btnIspisPOS.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnIspisPOS.BackgroundImage")));
            this.btnIspisPOS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIspisPOS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIspisPOS.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnIspisPOS.FlatAppearance.BorderSize = 0;
            this.btnIspisPOS.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnIspisPOS.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnIspisPOS.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnIspisPOS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIspisPOS.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnIspisPOS.Location = new System.Drawing.Point(27, 425);
            this.btnIspisPOS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnIspisPOS.Name = "btnIspisPOS";
            this.btnIspisPOS.Size = new System.Drawing.Size(128, 39);
            this.btnIspisPOS.TabIndex = 42;
            this.btnIspisPOS.Text = "Ispis na POS F2";
            this.btnIspisPOS.UseVisualStyleBackColor = false;
            this.btnIspisPOS.Click += new System.EventHandler(this.btnIspisPOS_Click);
            this.btnIspisPOS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnIspisPOS_KeyDown);
            // 
            // btnIspis
            // 
            this.btnIspis.BackColor = System.Drawing.Color.Transparent;
            this.btnIspis.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnIspis.BackgroundImage")));
            this.btnIspis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIspis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIspis.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnIspis.FlatAppearance.BorderSize = 0;
            this.btnIspis.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnIspis.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnIspis.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnIspis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIspis.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnIspis.Location = new System.Drawing.Point(161, 425);
            this.btnIspis.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnIspis.Name = "btnIspis";
            this.btnIspis.Size = new System.Drawing.Size(121, 39);
            this.btnIspis.TabIndex = 11;
            this.btnIspis.Text = "Ispis F1";
            this.btnIspis.UseVisualStyleBackColor = false;
            this.btnIspis.Click += new System.EventHandler(this.btnIspis_Click);
            this.btnIspis.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpOD_KeyDown);
            // 
            // cbKasa
            // 
            this.cbKasa.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbKasa.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbKasa.BackColor = System.Drawing.Color.White;
            this.cbKasa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKasa.Enabled = false;
            this.cbKasa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbKasa.FormattingEnabled = true;
            this.cbKasa.Location = new System.Drawing.Point(28, 177);
            this.cbKasa.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbKasa.Name = "cbKasa";
            this.cbKasa.Size = new System.Drawing.Size(234, 24);
            this.cbKasa.TabIndex = 50;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Arial", 9F);
            this.label6.Location = new System.Drawing.Point(28, 162);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 15);
            this.label6.TabIndex = 52;
            this.label6.Text = "Kasa";
            // 
            // chbKasa
            // 
            this.chbKasa.AutoSize = true;
            this.chbKasa.BackColor = System.Drawing.Color.Transparent;
            this.chbKasa.Enabled = false;
            this.chbKasa.Location = new System.Drawing.Point(267, 182);
            this.chbKasa.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chbKasa.Name = "chbKasa";
            this.chbKasa.Size = new System.Drawing.Size(15, 14);
            this.chbKasa.TabIndex = 51;
            this.chbKasa.UseVisualStyleBackColor = false;
            // 
            // frmPrometKase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(357, 500);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmPrometKase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Promet kase";
            this.Load += new System.EventHandler(this.frmPrometKase_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbDucan;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox cbSkladiste;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbZaposlenik;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDO;
        private System.Windows.Forms.DateTimePicker dtpDO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpOD;
        private System.Windows.Forms.CheckBox chbDucan;
        private System.Windows.Forms.CheckBox chbSkladiste;
        private System.Windows.Forms.CheckBox chbBlagajnik;
        private System.Windows.Forms.Button btnIspis;
        private System.Windows.Forms.Button btnIspisPOS;
        private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox cbGrupe;
        private System.Windows.Forms.CheckBox chbGrupe;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbdobavljaci;
        private System.Windows.Forms.CheckBox chbdobavljaci;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbKasa;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chbKasa;
    }
}