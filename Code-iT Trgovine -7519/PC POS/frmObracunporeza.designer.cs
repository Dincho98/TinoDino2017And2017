namespace PCPOS
{
    partial class frmObracunporeza
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
            this.btnObracunpor = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.lblducan = new System.Windows.Forms.Label();
            this.lblkasa = new System.Windows.Forms.Label();
            this.lbldatumod = new System.Windows.Forms.Label();
            this.lbldatumdo = new System.Windows.Forms.Label();
            this.lblblagajnik = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.ckbducan = new System.Windows.Forms.CheckBox();
            this.ckbkasa = new System.Windows.Forms.CheckBox();
            this.ckbblagajnik = new System.Windows.Forms.CheckBox();
            this.ckbracun = new System.Windows.Forms.CheckBox();
            this.lbldoracuna = new System.Windows.Forms.Label();
            this.lblodracuna = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnObracunpor
            // 
            this.btnObracunpor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnObracunpor.Location = new System.Drawing.Point(34, 343);
            this.btnObracunpor.Name = "btnObracunpor";
            this.btnObracunpor.Size = new System.Drawing.Size(90, 63);
            this.btnObracunpor.TabIndex = 0;
            this.btnObracunpor.Text = "Ispisi obračun za MPR";
            this.btnObracunpor.UseVisualStyleBackColor = true;
            this.btnObracunpor.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(53, 35);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(219, 24);
            this.comboBox1.TabIndex = 1;
            // 
            // comboBox2
            // 
            this.comboBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(53, 78);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(219, 24);
            this.comboBox2.TabIndex = 2;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dateTimePicker1.Location = new System.Drawing.Point(54, 246);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(219, 23);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dateTimePicker2.Location = new System.Drawing.Point(54, 287);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(219, 23);
            this.dateTimePicker2.TabIndex = 4;
            // 
            // lblducan
            // 
            this.lblducan.AutoSize = true;
            this.lblducan.BackColor = System.Drawing.Color.Transparent;
            this.lblducan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblducan.Location = new System.Drawing.Point(50, 18);
            this.lblducan.Name = "lblducan";
            this.lblducan.Size = new System.Drawing.Size(57, 17);
            this.lblducan.TabIndex = 5;
            this.lblducan.Text = "Dućan :";
            // 
            // lblkasa
            // 
            this.lblkasa.AutoSize = true;
            this.lblkasa.BackColor = System.Drawing.Color.Transparent;
            this.lblkasa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblkasa.Location = new System.Drawing.Point(50, 61);
            this.lblkasa.Name = "lblkasa";
            this.lblkasa.Size = new System.Drawing.Size(48, 17);
            this.lblkasa.TabIndex = 6;
            this.lblkasa.Text = "Kasa :";
            // 
            // lbldatumod
            // 
            this.lbldatumod.AutoSize = true;
            this.lbldatumod.BackColor = System.Drawing.Color.Transparent;
            this.lbldatumod.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbldatumod.Location = new System.Drawing.Point(51, 229);
            this.lbldatumod.Name = "lbldatumod";
            this.lbldatumod.Size = new System.Drawing.Size(86, 17);
            this.lbldatumod.TabIndex = 7;
            this.lbldatumod.Text = "Od datuma :";
            // 
            // lbldatumdo
            // 
            this.lbldatumdo.AutoSize = true;
            this.lbldatumdo.BackColor = System.Drawing.Color.Transparent;
            this.lbldatumdo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbldatumdo.Location = new System.Drawing.Point(51, 269);
            this.lbldatumdo.Name = "lbldatumdo";
            this.lbldatumdo.Size = new System.Drawing.Size(85, 17);
            this.lbldatumdo.TabIndex = 8;
            this.lbldatumdo.Text = "Do datuma :";
            // 
            // lblblagajnik
            // 
            this.lblblagajnik.AutoSize = true;
            this.lblblagajnik.BackColor = System.Drawing.Color.Transparent;
            this.lblblagajnik.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblblagajnik.Location = new System.Drawing.Point(51, 104);
            this.lblblagajnik.Name = "lblblagajnik";
            this.lblblagajnik.Size = new System.Drawing.Size(73, 17);
            this.lblblagajnik.TabIndex = 10;
            this.lblblagajnik.Text = "Blagajnik :";
            // 
            // comboBox3
            // 
            this.comboBox3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(53, 121);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(219, 24);
            this.comboBox3.TabIndex = 9;
            // 
            // ckbducan
            // 
            this.ckbducan.AutoSize = true;
            this.ckbducan.BackColor = System.Drawing.Color.Transparent;
            this.ckbducan.Location = new System.Drawing.Point(278, 40);
            this.ckbducan.Name = "ckbducan";
            this.ckbducan.Size = new System.Drawing.Size(15, 14);
            this.ckbducan.TabIndex = 11;
            this.ckbducan.UseVisualStyleBackColor = false;
            // 
            // ckbkasa
            // 
            this.ckbkasa.AutoSize = true;
            this.ckbkasa.BackColor = System.Drawing.Color.Transparent;
            this.ckbkasa.Location = new System.Drawing.Point(278, 83);
            this.ckbkasa.Name = "ckbkasa";
            this.ckbkasa.Size = new System.Drawing.Size(15, 14);
            this.ckbkasa.TabIndex = 12;
            this.ckbkasa.UseVisualStyleBackColor = false;
            // 
            // ckbblagajnik
            // 
            this.ckbblagajnik.AutoSize = true;
            this.ckbblagajnik.BackColor = System.Drawing.Color.Transparent;
            this.ckbblagajnik.Location = new System.Drawing.Point(278, 126);
            this.ckbblagajnik.Name = "ckbblagajnik";
            this.ckbblagajnik.Size = new System.Drawing.Size(15, 14);
            this.ckbblagajnik.TabIndex = 13;
            this.ckbblagajnik.UseVisualStyleBackColor = false;
            // 
            // ckbracun
            // 
            this.ckbracun.AutoSize = true;
            this.ckbracun.BackColor = System.Drawing.Color.Transparent;
            this.ckbracun.Location = new System.Drawing.Point(279, 166);
            this.ckbracun.Name = "ckbracun";
            this.ckbracun.Size = new System.Drawing.Size(15, 14);
            this.ckbracun.TabIndex = 19;
            this.ckbracun.UseVisualStyleBackColor = false;
            // 
            // lbldoracuna
            // 
            this.lbldoracuna.AutoSize = true;
            this.lbldoracuna.BackColor = System.Drawing.Color.Transparent;
            this.lbldoracuna.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbldoracuna.Location = new System.Drawing.Point(51, 186);
            this.lbldoracuna.Name = "lbldoracuna";
            this.lbldoracuna.Size = new System.Drawing.Size(82, 17);
            this.lbldoracuna.TabIndex = 18;
            this.lbldoracuna.Text = "Do računa :";
            // 
            // lblodracuna
            // 
            this.lblodracuna.AutoSize = true;
            this.lblodracuna.BackColor = System.Drawing.Color.Transparent;
            this.lblodracuna.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblodracuna.Location = new System.Drawing.Point(51, 146);
            this.lblodracuna.Name = "lblodracuna";
            this.lblodracuna.Size = new System.Drawing.Size(83, 17);
            this.lblodracuna.TabIndex = 16;
            this.lblodracuna.Text = "Od računa :";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.textBox1.Location = new System.Drawing.Point(54, 164);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(219, 23);
            this.textBox1.TabIndex = 20;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.textBox2.Location = new System.Drawing.Point(53, 205);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(219, 23);
            this.textBox2.TabIndex = 21;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(245, 343);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 63);
            this.button1.TabIndex = 22;
            this.button1.Text = "Ispisi obračun za FAK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // frmObracunporeza
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(356, 462);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ckbracun);
            this.Controls.Add(this.lbldoracuna);
            this.Controls.Add(this.lblodracuna);
            this.Controls.Add(this.ckbblagajnik);
            this.Controls.Add(this.ckbkasa);
            this.Controls.Add(this.ckbducan);
            this.Controls.Add(this.lblblagajnik);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.lbldatumdo);
            this.Controls.Add(this.lbldatumod);
            this.Controls.Add(this.lblkasa);
            this.Controls.Add(this.lblducan);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnObracunpor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmObracunporeza";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Obračun poreza";
            this.Load += new System.EventHandler(this.frmObracunporeza_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnObracunpor;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label lblducan;
        private System.Windows.Forms.Label lblkasa;
        private System.Windows.Forms.Label lbldatumod;
        private System.Windows.Forms.Label lbldatumdo;
        private System.Windows.Forms.Label lblblagajnik;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.CheckBox ckbducan;
        private System.Windows.Forms.CheckBox ckbkasa;
        private System.Windows.Forms.CheckBox ckbblagajnik;
        private System.Windows.Forms.CheckBox ckbracun;
        private System.Windows.Forms.Label lbldoracuna;
        private System.Windows.Forms.Label lblodracuna;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
    }
}