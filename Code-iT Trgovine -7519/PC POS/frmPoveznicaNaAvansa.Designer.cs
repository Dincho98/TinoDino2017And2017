namespace PCPOS
{
    partial class frmPoveznicaNaAvansa
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
            this.btnDodajNaAvans = new System.Windows.Forms.Button();
            this.txtBrojAvansa = new System.Windows.Forms.TextBox();
            this.frmTrazi = new System.Windows.Forms.Button();
            this.btnUkloniSaAvansa = new System.Windows.Forms.Button();
            this.txtGodinaAvansa = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDodajNaAvans
            // 
            this.btnDodajNaAvans.Location = new System.Drawing.Point(14, 80);
            this.btnDodajNaAvans.Name = "btnDodajNaAvans";
            this.btnDodajNaAvans.Size = new System.Drawing.Size(98, 26);
            this.btnDodajNaAvans.TabIndex = 0;
            this.btnDodajNaAvans.Text = "Dodaj na fakturu";
            this.btnDodajNaAvans.UseVisualStyleBackColor = true;
            this.btnDodajNaAvans.Click += new System.EventHandler(this.btnDodajNaAvans_Click);
            // 
            // txtBrojAvansa
            // 
            this.txtBrojAvansa.Location = new System.Drawing.Point(13, 47);
            this.txtBrojAvansa.Name = "txtBrojAvansa";
            this.txtBrojAvansa.ReadOnly = true;
            this.txtBrojAvansa.Size = new System.Drawing.Size(114, 20);
            this.txtBrojAvansa.TabIndex = 1;
            // 
            // frmTrazi
            // 
            this.frmTrazi.Location = new System.Drawing.Point(180, 46);
            this.frmTrazi.Name = "frmTrazi";
            this.frmTrazi.Size = new System.Drawing.Size(40, 23);
            this.frmTrazi.TabIndex = 2;
            this.frmTrazi.Text = "Traži";
            this.frmTrazi.UseVisualStyleBackColor = true;
            this.frmTrazi.Click += new System.EventHandler(this.frmTrazi_Click);
            // 
            // btnUkloniSaAvansa
            // 
            this.btnUkloniSaAvansa.Location = new System.Drawing.Point(123, 80);
            this.btnUkloniSaAvansa.Name = "btnUkloniSaAvansa";
            this.btnUkloniSaAvansa.Size = new System.Drawing.Size(98, 26);
            this.btnUkloniSaAvansa.TabIndex = 3;
            this.btnUkloniSaAvansa.Text = "Ukloni sa fakture";
            this.btnUkloniSaAvansa.UseVisualStyleBackColor = true;
            this.btnUkloniSaAvansa.Click += new System.EventHandler(this.btnUkloniSaAvansa_Click);
            // 
            // txtGodinaAvansa
            // 
            this.txtGodinaAvansa.Location = new System.Drawing.Point(129, 47);
            this.txtGodinaAvansa.Name = "txtGodinaAvansa";
            this.txtGodinaAvansa.ReadOnly = true;
            this.txtGodinaAvansa.Size = new System.Drawing.Size(50, 20);
            this.txtGodinaAvansa.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(13, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Broj avansa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(129, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Godina";
            // 
            // frmPoveznicaNaAvansa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 127);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtGodinaAvansa);
            this.Controls.Add(this.btnUkloniSaAvansa);
            this.Controls.Add(this.frmTrazi);
            this.Controls.Add(this.txtBrojAvansa);
            this.Controls.Add(this.btnDodajNaAvans);
            this.Name = "frmPoveznicaNaAvansa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Poveznica na avans";
            this.Load += new System.EventHandler(this.frmPoveznicaNaAvansa_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDodajNaAvans;
        private System.Windows.Forms.Button frmTrazi;
        private System.Windows.Forms.Button btnUkloniSaAvansa;
        public System.Windows.Forms.TextBox txtBrojAvansa;
        public System.Windows.Forms.TextBox txtGodinaAvansa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}