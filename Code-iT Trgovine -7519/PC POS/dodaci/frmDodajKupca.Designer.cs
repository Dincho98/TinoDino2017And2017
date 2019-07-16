namespace PCPOS.dodaci
{
    partial class frmDodajKupca
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
            this.btnOdustani = new System.Windows.Forms.Button();
            this.btnDodajKupca = new System.Windows.Forms.Button();
            this.txtImePrezime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv_klijenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOdustani
            // 
            this.btnOdustani.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOdustani.BackColor = System.Drawing.Color.Transparent;
            this.btnOdustani.BackgroundImage = global::PCPOS.Properties.Resources.dff;
            this.btnOdustani.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOdustani.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOdustani.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnOdustani.FlatAppearance.BorderSize = 0;
            this.btnOdustani.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnOdustani.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnOdustani.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnOdustani.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOdustani.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold);
            this.btnOdustani.ForeColor = System.Drawing.Color.White;
            this.btnOdustani.Location = new System.Drawing.Point(230, 394);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(148, 37);
            this.btnOdustani.TabIndex = 77;
            this.btnOdustani.Text = "Odustani";
            this.btnOdustani.UseVisualStyleBackColor = false;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            // 
            // btnDodajKupca
            // 
            this.btnDodajKupca.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDodajKupca.BackColor = System.Drawing.Color.Transparent;
            this.btnDodajKupca.BackgroundImage = global::PCPOS.Properties.Resources.dff;
            this.btnDodajKupca.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDodajKupca.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDodajKupca.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnDodajKupca.FlatAppearance.BorderSize = 0;
            this.btnDodajKupca.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnDodajKupca.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDodajKupca.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDodajKupca.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDodajKupca.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.btnDodajKupca.ForeColor = System.Drawing.Color.White;
            this.btnDodajKupca.Location = new System.Drawing.Point(309, 42);
            this.btnDodajKupca.Name = "btnDodajKupca";
            this.btnDodajKupca.Size = new System.Drawing.Size(67, 29);
            this.btnDodajKupca.TabIndex = 76;
            this.btnDodajKupca.Text = "Dodaj";
            this.btnDodajKupca.UseVisualStyleBackColor = false;
            this.btnDodajKupca.Click += new System.EventHandler(this.btnDodajKupca_Click);
            // 
            // txtImePrezime
            // 
            this.txtImePrezime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtImePrezime.Location = new System.Drawing.Point(16, 45);
            this.txtImePrezime.Name = "txtImePrezime";
            this.txtImePrezime.Size = new System.Drawing.Size(290, 23);
            this.txtImePrezime.TabIndex = 78;
            this.txtImePrezime.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtImePrezime_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(16, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 17);
            this.label1.TabIndex = 79;
            this.label1.Text = "Ime i prezime klijenta";
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.naziv_klijenta});
            this.dgv.Location = new System.Drawing.Point(16, 87);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(359, 304);
            this.dgv.TabIndex = 80;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            // 
            // id
            // 
            this.id.FillWeight = 60F;
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 60;
            // 
            // naziv_klijenta
            // 
            this.naziv_klijenta.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.naziv_klijenta.HeaderText = "Naziv klijenta";
            this.naziv_klijenta.Name = "naziv_klijenta";
            this.naziv_klijenta.ReadOnly = true;
            // 
            // frmDodajKupca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(387, 436);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtImePrezime);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnDodajKupca);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmDodajKupca";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dodaj kupca";
            this.Load += new System.EventHandler(this.frmDodajKupca_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Button btnDodajKupca;
        private System.Windows.Forms.TextBox txtImePrezime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv_klijenta;
    }
}