namespace PCPOS
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
            this.cbrekapitulacija = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.cbskladista = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbRabati = new System.Windows.Forms.CheckBox();
            this.cbSamoRobnoBezUsluga = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbrekapitulacija
            // 
            this.cbrekapitulacija.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbrekapitulacija.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbrekapitulacija.FormattingEnabled = true;
            this.cbrekapitulacija.Location = new System.Drawing.Point(201, 26);
            this.cbrekapitulacija.Name = "cbrekapitulacija";
            this.cbrekapitulacija.Size = new System.Drawing.Size(232, 24);
            this.cbrekapitulacija.TabIndex = 0;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dateTimePicker1.Location = new System.Drawing.Point(201, 81);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(232, 22);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dateTimePicker2.Location = new System.Drawing.Point(201, 106);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(232, 22);
            this.dateTimePicker2.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button1.Location = new System.Drawing.Point(272, 175);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(161, 39);
            this.button1.TabIndex = 3;
            this.button1.Text = "Ispis";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbskladista
            // 
            this.cbskladista.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbskladista.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbskladista.FormattingEnabled = true;
            this.cbskladista.Location = new System.Drawing.Point(201, 53);
            this.cbskladista.Name = "cbskladista";
            this.cbskladista.Size = new System.Drawing.Size(232, 24);
            this.cbskladista.TabIndex = 4;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Location = new System.Drawing.Point(445, 61);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(23, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Odaberite vrstu dokumenta:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(23, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Odabir skladišta:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(23, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Do datuma :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(23, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Od datuma :";
            // 
            // cbRabati
            // 
            this.cbRabati.AutoSize = true;
            this.cbRabati.BackColor = System.Drawing.Color.Transparent;
            this.cbRabati.Checked = true;
            this.cbRabati.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRabati.Location = new System.Drawing.Point(292, 134);
            this.cbRabati.Name = "cbRabati";
            this.cbRabati.Size = new System.Drawing.Size(148, 17);
            this.cbRabati.TabIndex = 13;
            this.cbRabati.Text = "Ispis prometa sa rabatima ";
            this.cbRabati.UseVisualStyleBackColor = false;
            // 
            // cbSamoRobnoBezUsluga
            // 
            this.cbSamoRobnoBezUsluga.AutoSize = true;
            this.cbSamoRobnoBezUsluga.BackColor = System.Drawing.Color.Transparent;
            this.cbSamoRobnoBezUsluga.Checked = true;
            this.cbSamoRobnoBezUsluga.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSamoRobnoBezUsluga.Location = new System.Drawing.Point(292, 152);
            this.cbSamoRobnoBezUsluga.Name = "cbSamoRobnoBezUsluga";
            this.cbSamoRobnoBezUsluga.Size = new System.Drawing.Size(173, 17);
            this.cbSamoRobnoBezUsluga.TabIndex = 13;
            this.cbSamoRobnoBezUsluga.Text = "Ispis samo za robno bez usluga";
            this.cbSamoRobnoBezUsluga.UseVisualStyleBackColor = false;
            // 
            // frmRekapitulacija
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 221);
            this.Controls.Add(this.cbSamoRobnoBezUsluga);
            this.Controls.Add(this.cbRabati);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.cbskladista);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.cbrekapitulacija);
            this.Name = "frmRekapitulacija";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Skladišni dokumenti";
            this.Load += new System.EventHandler(this.frmRekapitulacija_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmRekapitulacija_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbrekapitulacija;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbskladista;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbRabati;
        private System.Windows.Forms.CheckBox cbSamoRobnoBezUsluga;
    }
}