namespace PCPOS.Odrzavanja
{
    partial class Odrzavanje_promjena
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
            this.txtTraziPremaSifri = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIme = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Partner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolicina_odr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sifra,
            this.Partner,
            this.kolicina_odr,
            this.tel});
            this.dgv.Location = new System.Drawing.Point(12, 97);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 18;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(936, 344);
            this.dgv.TabIndex = 24;
            this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            // 
            // txtTraziPremaSifri
            // 
            this.txtTraziPremaSifri.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtTraziPremaSifri.Location = new System.Drawing.Point(390, 47);
            this.txtTraziPremaSifri.Name = "txtTraziPremaSifri";
            this.txtTraziPremaSifri.Size = new System.Drawing.Size(339, 26);
            this.txtTraziPremaSifri.TabIndex = 37;
            this.txtTraziPremaSifri.TextChanged += new System.EventHandler(this.txtTraziPremaSifri_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(386, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "Traži prema šifri";
            // 
            // txtIme
            // 
            this.txtIme.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtIme.Location = new System.Drawing.Point(12, 47);
            this.txtIme.Name = "txtIme";
            this.txtIme.Size = new System.Drawing.Size(335, 26);
            this.txtIme.TabIndex = 36;
            this.txtIme.TextChanged += new System.EventHandler(this.txtIme_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(8, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(93, 13);
            this.label10.TabIndex = 38;
            this.label10.Text = "Traži prema imenu";
            // 
            // sifra
            // 
            this.sifra.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sifra.HeaderText = "Šifra";
            this.sifra.Name = "sifra";
            this.sifra.ReadOnly = true;
            this.sifra.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Partner
            // 
            this.Partner.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Partner.HeaderText = "Partner";
            this.Partner.Name = "Partner";
            this.Partner.ReadOnly = true;
            this.Partner.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // kolicina_odr
            // 
            this.kolicina_odr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.kolicina_odr.HeaderText = "Količina održavanja";
            this.kolicina_odr.Name = "kolicina_odr";
            this.kolicina_odr.ReadOnly = true;
            this.kolicina_odr.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // tel
            // 
            this.tel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.tel.HeaderText = "Telefonski broj";
            this.tel.Name = "tel";
            this.tel.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Odrzavanje_promjena
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 453);
            this.Controls.Add(this.txtTraziPremaSifri);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtIme);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dgv);
            this.Name = "Odrzavanje_promjena";
            this.Text = "Odrzavanje_promjena";
            this.Load += new System.EventHandler(this.Odrzavanje_promjena_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Odrzavanje_promjena_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.TextBox txtTraziPremaSifri;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIme;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn Partner;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolicina_odr;
        private System.Windows.Forms.DataGridViewTextBoxColumn tel;
    }
}