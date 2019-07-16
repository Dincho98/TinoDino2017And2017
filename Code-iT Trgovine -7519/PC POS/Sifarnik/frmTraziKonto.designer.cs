namespace PCPOS.Sifarnik
{
    partial class frmTraziKonto
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
            this.label8 = new System.Windows.Forms.Label();
            this.txttraziimekonto = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgw2 = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            this.txttrazisifrakonto = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgw2)).BeginInit();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(12, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(178, 16);
            this.label8.TabIndex = 48;
            this.label8.Text = "Traži konto prema imenu";
            // 
            // txttraziimekonto
            // 
            this.txttraziimekonto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txttraziimekonto.Location = new System.Drawing.Point(196, 57);
            this.txttraziimekonto.Name = "txttraziimekonto";
            this.txttraziimekonto.Size = new System.Drawing.Size(414, 26);
            this.txttraziimekonto.TabIndex = 46;
            this.txttraziimekonto.TextChanged += new System.EventHandler(this.txttraziimekonto_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(8, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 16);
            this.label4.TabIndex = 43;
            this.label4.Text = "Partneri";
            // 
            // dgw2
            // 
            this.dgw2.AllowUserToAddRows = false;
            this.dgw2.AllowUserToDeleteRows = false;
            this.dgw2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgw2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgw2.BackgroundColor = System.Drawing.Color.White;
            this.dgw2.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgw2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgw2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgw2.GridColor = System.Drawing.Color.Gainsboro;
            this.dgw2.Location = new System.Drawing.Point(11, 118);
            this.dgw2.Name = "dgw2";
            this.dgw2.ReadOnly = true;
            this.dgw2.RowHeadersVisible = false;
            this.dgw2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgw2.Size = new System.Drawing.Size(733, 330);
            this.dgw2.TabIndex = 42;
            this.dgw2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw2_CellClick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label10.Location = new System.Drawing.Point(12, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(162, 16);
            this.label10.TabIndex = 41;
            this.label10.Text = "Traži konto prema šifri";
            // 
            // txttrazisifrakonto
            // 
            this.txttrazisifrakonto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txttrazisifrakonto.Location = new System.Drawing.Point(196, 14);
            this.txttrazisifrakonto.Name = "txttrazisifrakonto";
            this.txttrazisifrakonto.Size = new System.Drawing.Size(414, 26);
            this.txttrazisifrakonto.TabIndex = 39;
            this.txttrazisifrakonto.TextChanged += new System.EventHandler(this.txttrazisifrakonto_TextChanged);
            this.txttrazisifrakonto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txttrazisifrakonto_KeyPress);
            // 
            // frmTraziKonto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 484);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txttraziimekonto);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgw2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txttrazisifrakonto);
            this.Name = "frmTraziKonto";
            this.Text = "frmTraziKonto";
            this.Load += new System.EventHandler(this.frmTraziKonto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgw2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txttraziimekonto;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.DataGridView dgw2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txttrazisifrakonto;
    }
}