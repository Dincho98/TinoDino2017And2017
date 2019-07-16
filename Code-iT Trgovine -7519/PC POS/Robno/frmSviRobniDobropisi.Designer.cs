namespace PCPOS.Robno
{
    partial class frmSviRobniDobropisi
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnIspis = new System.Windows.Forms.Button();
            this.btnUredi = new System.Windows.Forms.Button();
            this.btnIzlaz = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pbTrazi = new System.Windows.Forms.PictureBox();
            this.cbZaposlenik = new System.Windows.Forms.ComboBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpDatumDo = new System.Windows.Forms.DateTimePicker();
            this.dtpDatumOd = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pbTraziPartner = new System.Windows.Forms.PictureBox();
            this.tbSifraPartnera = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbBrojDobropisa = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.broj_dobropis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Partner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skladiste = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ukupno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTrazi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTraziPartner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // btnIspis
            // 
            this.btnIspis.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnIspis.Image = global::PCPOS.Properties.Resources.print_printer;
            this.btnIspis.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIspis.Location = new System.Drawing.Point(12, 12);
            this.btnIspis.Name = "btnIspis";
            this.btnIspis.Size = new System.Drawing.Size(148, 45);
            this.btnIspis.TabIndex = 0;
            this.btnIspis.Text = "Ispis dobropisa";
            this.btnIspis.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIspis.UseVisualStyleBackColor = true;
            this.btnIspis.Click += new System.EventHandler(this.btnIspis_Click);
            // 
            // btnUredi
            // 
            this.btnUredi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnUredi.Image = global::PCPOS.Properties.Resources.edit_icon;
            this.btnUredi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUredi.Location = new System.Drawing.Point(166, 12);
            this.btnUredi.Name = "btnUredi";
            this.btnUredi.Size = new System.Drawing.Size(139, 45);
            this.btnUredi.TabIndex = 1;
            this.btnUredi.Text = "Uredi dobropis";
            this.btnUredi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUredi.UseVisualStyleBackColor = true;
            this.btnUredi.Click += new System.EventHandler(this.BtnUredi_Click);
            // 
            // btnIzlaz
            // 
            this.btnIzlaz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIzlaz.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnIzlaz.Image = global::PCPOS.Properties.Resources.Actions_application_exit_icon;
            this.btnIzlaz.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIzlaz.Location = new System.Drawing.Point(806, 12);
            this.btnIzlaz.Name = "btnIzlaz";
            this.btnIzlaz.Size = new System.Drawing.Size(107, 45);
            this.btnIzlaz.TabIndex = 2;
            this.btnIzlaz.Text = "Izlaz    ";
            this.btnIzlaz.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIzlaz.UseVisualStyleBackColor = true;
            this.btnIzlaz.Click += new System.EventHandler(this.BtnIzlaz_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.pbTrazi);
            this.groupBox1.Controls.Add(this.cbZaposlenik);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtpDatumDo);
            this.groupBox1.Controls.Add(this.dtpDatumOd);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.pbTraziPartner);
            this.groupBox1.Controls.Add(this.tbSifraPartnera);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbBrojDobropisa);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox1.Location = new System.Drawing.Point(12, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(901, 87);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pretraživanje dobropisa";
            // 
            // pbTrazi
            // 
            this.pbTrazi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbTrazi.Image = global::PCPOS.Properties.Resources._1059;
            this.pbTrazi.Location = new System.Drawing.Point(837, 24);
            this.pbTrazi.Name = "pbTrazi";
            this.pbTrazi.Size = new System.Drawing.Size(52, 50);
            this.pbTrazi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbTrazi.TabIndex = 4;
            this.pbTrazi.TabStop = false;
            this.pbTrazi.Click += new System.EventHandler(this.pbTrazi_Click);
            // 
            // cbZaposlenik
            // 
            this.cbZaposlenik.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbZaposlenik.FormattingEnabled = true;
            this.cbZaposlenik.Location = new System.Drawing.Point(670, 50);
            this.cbZaposlenik.Name = "cbZaposlenik";
            this.cbZaposlenik.Size = new System.Drawing.Size(156, 24);
            this.cbZaposlenik.TabIndex = 12;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(670, 24);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(156, 23);
            this.textBox3.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(583, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "Zaposlenik:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(581, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Šifra artikla:";
            // 
            // dtpDatumDo
            // 
            this.dtpDatumDo.Location = new System.Drawing.Point(387, 50);
            this.dtpDatumDo.Name = "dtpDatumDo";
            this.dtpDatumDo.Size = new System.Drawing.Size(181, 23);
            this.dtpDatumDo.TabIndex = 8;
            // 
            // dtpDatumOd
            // 
            this.dtpDatumOd.Location = new System.Drawing.Point(387, 23);
            this.dtpDatumOd.Name = "dtpDatumOd";
            this.dtpDatumOd.Size = new System.Drawing.Size(181, 23);
            this.dtpDatumOd.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(299, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Do datuma:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(299, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Od datuma:";
            // 
            // pbTraziPartner
            // 
            this.pbTraziPartner.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbTraziPartner.Image = global::PCPOS.Properties.Resources._10591;
            this.pbTraziPartner.ImageLocation = "";
            this.pbTraziPartner.Location = new System.Drawing.Point(264, 51);
            this.pbTraziPartner.Name = "pbTraziPartner";
            this.pbTraziPartner.Size = new System.Drawing.Size(24, 23);
            this.pbTraziPartner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbTraziPartner.TabIndex = 4;
            this.pbTraziPartner.TabStop = false;
            this.pbTraziPartner.Click += new System.EventHandler(this.PbTraziPartner_Click);
            // 
            // tbSifraPartnera
            // 
            this.tbSifraPartnera.Location = new System.Drawing.Point(116, 51);
            this.tbSifraPartnera.Name = "tbSifraPartnera";
            this.tbSifraPartnera.Size = new System.Drawing.Size(142, 23);
            this.tbSifraPartnera.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Šifra partnera:";
            // 
            // tbBrojDobropisa
            // 
            this.tbBrojDobropisa.Location = new System.Drawing.Point(116, 24);
            this.tbBrojDobropisa.Name = "tbBrojDobropisa";
            this.tbBrojDobropisa.Size = new System.Drawing.Size(142, 23);
            this.tbBrojDobropisa.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Broj dobropisa:";
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.broj_dobropis,
            this.Partner,
            this.skladiste,
            this.datum,
            this.ukupno});
            this.dgv.Location = new System.Drawing.Point(12, 156);
            this.dgv.Name = "dgv";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(901, 515);
            this.dgv.TabIndex = 4;
            // 
            // broj_dobropis
            // 
            this.broj_dobropis.FillWeight = 50F;
            this.broj_dobropis.HeaderText = "Broj";
            this.broj_dobropis.Name = "broj_dobropis";
            // 
            // Partner
            // 
            this.Partner.HeaderText = "Partner";
            this.Partner.Name = "Partner";
            // 
            // skladiste
            // 
            this.skladiste.HeaderText = "Skladište";
            this.skladiste.Name = "skladiste";
            // 
            // datum
            // 
            this.datum.HeaderText = "Datum";
            this.datum.Name = "datum";
            // 
            // ukupno
            // 
            this.ukupno.HeaderText = "Ukupno";
            this.ukupno.Name = "ukupno";
            // 
            // frmSviRobniDobropisi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(925, 683);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnIzlaz);
            this.Controls.Add(this.btnUredi);
            this.Controls.Add(this.btnIspis);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmSviRobniDobropisi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Svi robni dobropisi";
            this.Load += new System.EventHandler(this.FrmSviRobniDobropisi_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTrazi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTraziPartner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnIspis;
        private System.Windows.Forms.Button btnUredi;
        private System.Windows.Forms.Button btnIzlaz;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbBrojDobropisa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSifraPartnera;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbTraziPartner;
        private System.Windows.Forms.DateTimePicker dtpDatumDo;
        private System.Windows.Forms.DateTimePicker dtpDatumOd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pbTrazi;
        private System.Windows.Forms.ComboBox cbZaposlenik;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj_dobropis;
        private System.Windows.Forms.DataGridViewTextBoxColumn Partner;
        private System.Windows.Forms.DataGridViewTextBoxColumn skladiste;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum;
        private System.Windows.Forms.DataGridViewTextBoxColumn ukupno;
    }
}