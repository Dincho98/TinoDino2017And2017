namespace PCPOS
{
    partial class frmStavkeRadnogNaloga
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblVpc = new System.Windows.Forms.Label();
            this.txtvpcnormativ = new System.Windows.Forms.TextBox();
            this.txtmpcnormativ = new System.Windows.Forms.TextBox();
            this.lblMpc = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(656, 293);
            this.dataGridView1.TabIndex = 0;
            // 
            // lblVpc
            // 
            this.lblVpc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVpc.AutoSize = true;
            this.lblVpc.Location = new System.Drawing.Point(249, 303);
            this.lblVpc.Name = "lblVpc";
            this.lblVpc.Size = new System.Drawing.Size(73, 13);
            this.lblVpc.TabIndex = 1;
            this.lblVpc.Text = "VPC ukupno :";
            // 
            // txtvpcnormativ
            // 
            this.txtvpcnormativ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtvpcnormativ.Location = new System.Drawing.Point(328, 300);
            this.txtvpcnormativ.Name = "txtvpcnormativ";
            this.txtvpcnormativ.ReadOnly = true;
            this.txtvpcnormativ.Size = new System.Drawing.Size(104, 20);
            this.txtvpcnormativ.TabIndex = 2;
            // 
            // txtmpcnormativ
            // 
            this.txtmpcnormativ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtmpcnormativ.Location = new System.Drawing.Point(540, 300);
            this.txtmpcnormativ.Name = "txtmpcnormativ";
            this.txtmpcnormativ.ReadOnly = true;
            this.txtmpcnormativ.Size = new System.Drawing.Size(104, 20);
            this.txtmpcnormativ.TabIndex = 4;
            // 
            // lblMpc
            // 
            this.lblMpc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMpc.AutoSize = true;
            this.lblMpc.Location = new System.Drawing.Point(461, 303);
            this.lblMpc.Name = "lblMpc";
            this.lblMpc.Size = new System.Drawing.Size(75, 13);
            this.lblMpc.TabIndex = 3;
            this.lblMpc.Text = "MPC ukupno :";
            // 
            // frmStavkeRadnogNaloga
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 332);
            this.Controls.Add(this.txtmpcnormativ);
            this.Controls.Add(this.lblMpc);
            this.Controls.Add(this.txtvpcnormativ);
            this.Controls.Add(this.lblVpc);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmStavkeRadnogNaloga";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stavke radnog naloga";
            this.Load += new System.EventHandler(this.frmStavkeRadnogNaloga_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblVpc;
        private System.Windows.Forms.TextBox txtvpcnormativ;
        private System.Windows.Forms.TextBox txtmpcnormativ;
        private System.Windows.Forms.Label lblMpc;
    }
}