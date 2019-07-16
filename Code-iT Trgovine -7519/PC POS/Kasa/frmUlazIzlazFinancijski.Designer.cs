namespace PCPOS.Kasa
{
    partial class frmUlazIzlazFinancijski
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUlazIzlazFinancijski));
            this.cbDucan = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.cbZaposlenik = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDO = new System.Windows.Forms.Label();
            this.dtpDO = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpOD = new System.Windows.Forms.DateTimePicker();
            this.chbDucan = new System.Windows.Forms.CheckBox();
            this.chbBlagajnik = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnIspis = new System.Windows.Forms.Button();
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
            // cbZaposlenik
            // 
            this.cbZaposlenik.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbZaposlenik.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbZaposlenik.BackColor = System.Drawing.Color.White;
            this.cbZaposlenik.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbZaposlenik.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbZaposlenik.FormattingEnabled = true;
            this.cbZaposlenik.Location = new System.Drawing.Point(29, 178);
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
            this.label2.Location = new System.Drawing.Point(29, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 15);
            this.label2.TabIndex = 32;
            this.label2.Text = "Zaposlenik";
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
            this.dtpDO.CustomFormat = "dd.MM.yyyy 23:59:59";
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
            this.dtpOD.CustomFormat = "dd.MM.yyyy 0:00:01";
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
            this.chbDucan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpOD_KeyDown);
            // 
            // chbBlagajnik
            // 
            this.chbBlagajnik.AutoSize = true;
            this.chbBlagajnik.BackColor = System.Drawing.Color.Transparent;
            this.chbBlagajnik.Location = new System.Drawing.Point(267, 181);
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
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbDucan);
            this.groupBox1.Controls.Add(this.btnIspis);
            this.groupBox1.Controls.Add(this.cbZaposlenik);
            this.groupBox1.Controls.Add(this.chbBlagajnik);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.chbDucan);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblDO);
            this.groupBox1.Controls.Add(this.dtpOD);
            this.groupBox1.Controls.Add(this.dtpDO);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox1.Location = new System.Drawing.Point(21, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 274);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opcionalni ispis prometa";
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
            this.btnIspis.Location = new System.Drawing.Point(28, 215);
            this.btnIspis.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnIspis.Name = "btnIspis";
            this.btnIspis.Size = new System.Drawing.Size(234, 39);
            this.btnIspis.TabIndex = 11;
            this.btnIspis.Text = "Ispis";
            this.btnIspis.UseVisualStyleBackColor = false;
            this.btnIspis.Click += new System.EventHandler(this.btnIspis_Click);
            this.btnIspis.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpOD_KeyDown);
            // 
            // frmUlazIzlazFinancijski
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(353, 300);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmUlazIzlazFinancijski";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Prodajne statistike";
            this.Load += new System.EventHandler(this.frmPrometKase_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbDucan;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox cbZaposlenik;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDO;
        private System.Windows.Forms.DateTimePicker dtpDO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpOD;
        private System.Windows.Forms.CheckBox chbDucan;
        private System.Windows.Forms.CheckBox chbBlagajnik;
        private System.Windows.Forms.Button btnIspis;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}