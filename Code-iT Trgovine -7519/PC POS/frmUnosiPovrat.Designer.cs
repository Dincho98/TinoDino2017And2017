namespace PCPOS
{
    partial class frmUnosiPovrat
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
            this.button1 = new System.Windows.Forms.Button();
            this.txtUnos = new System.Windows.Forms.TextBox();
            this.txtNatpis = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.Location = new System.Drawing.Point(205, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(38, 27);
            this.button1.TabIndex = 0;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtUnos
            // 
            this.txtUnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtUnos.Location = new System.Drawing.Point(12, 40);
            this.txtUnos.Name = "txtUnos";
            this.txtUnos.Size = new System.Drawing.Size(191, 23);
            this.txtUnos.TabIndex = 1;
            this.txtUnos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUnos_KeyDown);
            // 
            // txtNatpis
            // 
            this.txtNatpis.AutoSize = true;
            this.txtNatpis.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtNatpis.Location = new System.Drawing.Point(12, 11);
            this.txtNatpis.Name = "txtNatpis";
            this.txtNatpis.Size = new System.Drawing.Size(46, 17);
            this.txtNatpis.TabIndex = 2;
            this.txtNatpis.Text = "label1";
            // 
            // frmUnosiPovrat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(253, 74);
            this.Controls.Add(this.txtNatpis);
            this.Controls.Add(this.txtUnos);
            this.Controls.Add(this.button1);
            this.Name = "frmUnosiPovrat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmUnosiPovrat";
            this.Load += new System.EventHandler(this.frmUnosiPovrat_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtUnos;
        public System.Windows.Forms.Label txtNatpis;
    }
}