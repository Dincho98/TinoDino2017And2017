namespace PCPOS
{
    partial class frmTrgovackaKnjigaLoad
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.bttPrikazi = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bttOdustani = new System.Windows.Forms.Button();
            this.cbRabati = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(43, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(43, 38);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 1;
            // 
            // bttPrikazi
            // 
            this.bttPrikazi.Location = new System.Drawing.Point(43, 92);
            this.bttPrikazi.Name = "bttPrikazi";
            this.bttPrikazi.Size = new System.Drawing.Size(75, 23);
            this.bttPrikazi.TabIndex = 2;
            this.bttPrikazi.Text = "Prikaži";
            this.bttPrikazi.UseVisualStyleBackColor = true;
            this.bttPrikazi.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Od:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Do:";
            // 
            // bttOdustani
            // 
            this.bttOdustani.Location = new System.Drawing.Point(124, 92);
            this.bttOdustani.Name = "bttOdustani";
            this.bttOdustani.Size = new System.Drawing.Size(75, 23);
            this.bttOdustani.TabIndex = 5;
            this.bttOdustani.Text = "Odustani";
            this.bttOdustani.UseVisualStyleBackColor = true;
            this.bttOdustani.Click += new System.EventHandler(this.bttOdustani_Click);
            // 
            // cbRabati
            // 
            this.cbRabati.AutoSize = true;
            this.cbRabati.Checked = true;
            this.cbRabati.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRabati.Location = new System.Drawing.Point(98, 66);
            this.cbRabati.Name = "cbRabati";
            this.cbRabati.Size = new System.Drawing.Size(148, 17);
            this.cbRabati.TabIndex = 14;
            this.cbRabati.Text = "Ispis prometa sa rabatima ";
            this.cbRabati.UseVisualStyleBackColor = true;
            // 
            // frmTrgovackaKnjigaLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 132);
            this.Controls.Add(this.cbRabati);
            this.Controls.Add(this.bttOdustani);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bttPrikazi);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmTrgovackaKnjigaLoad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Knjiga Ulaza i Izlaza";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Button bttPrikazi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bttOdustani;
        private System.Windows.Forms.CheckBox cbRabati;
    }
}