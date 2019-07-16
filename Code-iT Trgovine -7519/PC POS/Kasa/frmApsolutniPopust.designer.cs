namespace PCPOS.Kasa
{
    partial class frmApsolutniPopust
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmApsolutniPopust));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.bttOK = new System.Windows.Forms.Button();
            this.bttCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Unesite iznos popusta u kunama:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(183, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(136, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // bttOK
            // 
            this.bttOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.bttOK.BackColor = System.Drawing.Color.Transparent;
            this.bttOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bttOK.BackgroundImage")));
            this.bttOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bttOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttOK.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.bttOK.FlatAppearance.BorderSize = 0;
            this.bttOK.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.bttOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.bttOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.bttOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bttOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.bttOK.Location = new System.Drawing.Point(13, 51);
            this.bttOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bttOK.Name = "bttOK";
            this.bttOK.Size = new System.Drawing.Size(149, 33);
            this.bttOK.TabIndex = 89;
            this.bttOK.Text = "Dodaj (Enter)";
            this.bttOK.UseVisualStyleBackColor = false;
            this.bttOK.Click += new System.EventHandler(this.bttOK_Click);
            this.bttOK.MouseEnter += new System.EventHandler(this.pic_MouseEnter);
            this.bttOK.MouseLeave += new System.EventHandler(this.pic_MouseLeave);
            // 
            // bttCancel
            // 
            this.bttCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.bttCancel.BackColor = System.Drawing.Color.Transparent;
            this.bttCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bttCancel.BackgroundImage")));
            this.bttCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bttCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttCancel.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.bttCancel.FlatAppearance.BorderSize = 0;
            this.bttCancel.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.bttCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.bttCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.bttCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bttCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.bttCancel.Location = new System.Drawing.Point(170, 51);
            this.bttCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bttCancel.Name = "bttCancel";
            this.bttCancel.Size = new System.Drawing.Size(149, 33);
            this.bttCancel.TabIndex = 90;
            this.bttCancel.Text = "Odustani (ESC)";
            this.bttCancel.UseVisualStyleBackColor = false;
            this.bttCancel.Click += new System.EventHandler(this.bttCancel_Click);
            this.bttCancel.MouseEnter += new System.EventHandler(this.pic_MouseEnter);
            this.bttCancel.MouseLeave += new System.EventHandler(this.pic_MouseLeave);
            // 
            // frmApsolutniPopust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(330, 98);
            this.Controls.Add(this.bttCancel);
            this.Controls.Add(this.bttOK);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmApsolutniPopust";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unos Popusta";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button bttOK;
        private System.Windows.Forms.Button bttCancel;
    }
}