namespace PCPOS.Report.Liste
{
    partial class frmSkladisteFinancijsko
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSkladisteFinancijsko));
            this.cbSkladiste = new System.Windows.Forms.ComboBox();
            this.chbSkladiste = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnIspis = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbSkladiste
            // 
            this.cbSkladiste.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbSkladiste.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbSkladiste.BackColor = System.Drawing.Color.White;
            this.cbSkladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkladiste.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbSkladiste.FormattingEnabled = true;
            this.cbSkladiste.Location = new System.Drawing.Point(12, 33);
            this.cbSkladiste.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbSkladiste.Name = "cbSkladiste";
            this.cbSkladiste.Size = new System.Drawing.Size(234, 24);
            this.cbSkladiste.TabIndex = 33;
            // 
            // chbSkladiste
            // 
            this.chbSkladiste.AutoSize = true;
            this.chbSkladiste.BackColor = System.Drawing.Color.Transparent;
            this.chbSkladiste.Location = new System.Drawing.Point(251, 38);
            this.chbSkladiste.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chbSkladiste.Name = "chbSkladiste";
            this.chbSkladiste.Size = new System.Drawing.Size(15, 14);
            this.chbSkladiste.TabIndex = 34;
            this.chbSkladiste.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 9F);
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 35;
            this.label1.Text = "Skladište";
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
            this.btnIspis.Location = new System.Drawing.Point(12, 75);
            this.btnIspis.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnIspis.Name = "btnIspis";
            this.btnIspis.Size = new System.Drawing.Size(121, 39);
            this.btnIspis.TabIndex = 45;
            this.btnIspis.Text = "Ispis F1";
            this.btnIspis.UseVisualStyleBackColor = false;
            this.btnIspis.Click += new System.EventHandler(this.btnIspis_Click);
            // 
            // frmSkladisteFinancijsko
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(284, 136);
            this.Controls.Add(this.btnIspis);
            this.Controls.Add(this.cbSkladiste);
            this.Controls.Add(this.chbSkladiste);
            this.Controls.Add(this.label1);
            this.Name = "frmSkladisteFinancijsko";
            this.Text = "Skladiste financijsko";
            this.Load += new System.EventHandler(this.frmSkladisteFinancijsko_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbSkladiste;
        private System.Windows.Forms.CheckBox chbSkladiste;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnIspis;
    }
}