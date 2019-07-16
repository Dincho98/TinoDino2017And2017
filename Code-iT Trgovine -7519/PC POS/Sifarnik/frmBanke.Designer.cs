namespace PCPOS.Sifarnik
{
    partial class frmBanke
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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.grad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.posta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_zupanija = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drzava = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.naselje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_grad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.grad,
            this.posta,
            this.id_zupanija,
            this.drzava,
            this.naselje,
            this.id_grad});
            this.dgv.Location = new System.Drawing.Point(12, 47);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(526, 318);
            this.dgv.TabIndex = 115;
            // 
            // grad
            // 
            this.grad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.grad.HeaderText = "Grad";
            this.grad.Name = "grad";
            // 
            // posta
            // 
            this.posta.HeaderText = "Pošta";
            this.posta.Name = "posta";
            // 
            // id_zupanija
            // 
            this.id_zupanija.HeaderText = "Županija";
            this.id_zupanija.Name = "id_zupanija";
            this.id_zupanija.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // drzava
            // 
            this.drzava.HeaderText = "Država";
            this.drzava.Name = "drzava";
            this.drzava.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.drzava.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // naselje
            // 
            this.naselje.HeaderText = "Naselje";
            this.naselje.Name = "naselje";
            // 
            // id_grad
            // 
            this.id_grad.HeaderText = "id_grad";
            this.id_grad.Name = "id_grad";
            this.id_grad.ReadOnly = true;
            this.id_grad.Visible = false;
            // 
            // frmBanke
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(559, 377);
            this.Controls.Add(this.dgv);
            this.Name = "frmBanke";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Banke";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn grad;
        private System.Windows.Forms.DataGridViewTextBoxColumn posta;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_zupanija;
        private System.Windows.Forms.DataGridViewComboBoxColumn drzava;
        private System.Windows.Forms.DataGridViewTextBoxColumn naselje;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_grad;
    }
}