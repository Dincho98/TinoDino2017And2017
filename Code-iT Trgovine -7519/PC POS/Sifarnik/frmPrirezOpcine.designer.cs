namespace PCPOS.Sifarnik
{
    partial class frmPrirezOpcine
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
			this.label1 = new System.Windows.Forms.Label();
			this.txtNaziv = new System.Windows.Forms.TextBox();
			this.btnNovo = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.txtIznos = new System.Windows.Forms.TextBox();
			this.dgvSk = new System.Windows.Forms.DataGridView();
			this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.iznos = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.id_porez = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dgvSk)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label1.Location = new System.Drawing.Point(12, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(81, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "Naziv poreza:";
			// 
			// txtNaziv
			// 
			this.txtNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.txtNaziv.Location = new System.Drawing.Point(93, 29);
			this.txtNaziv.Name = "txtNaziv";
			this.txtNaziv.Size = new System.Drawing.Size(250, 23);
			this.txtNaziv.TabIndex = 2;
			// 
			// btnNovo
			// 
			this.btnNovo.Location = new System.Drawing.Point(197, 97);
			this.btnNovo.Name = "btnNovo";
			this.btnNovo.Size = new System.Drawing.Size(146, 28);
			this.btnNovo.TabIndex = 5;
			this.btnNovo.Text = "Dodaj novi porez";
			this.btnNovo.UseVisualStyleBackColor = true;
			this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label2.Location = new System.Drawing.Point(12, 63);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 15);
			this.label2.TabIndex = 1;
			this.label2.Text = "Iznos poreza:";
			// 
			// txtIznos
			// 
			this.txtIznos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.txtIznos.Location = new System.Drawing.Point(93, 58);
			this.txtIznos.Name = "txtIznos";
			this.txtIznos.Size = new System.Drawing.Size(250, 23);
			this.txtIznos.TabIndex = 2;
			// 
			// dgvSk
			// 
			this.dgvSk.AllowUserToAddRows = false;
			this.dgvSk.AllowUserToDeleteRows = false;
			this.dgvSk.BackgroundColor = System.Drawing.Color.White;
			this.dgvSk.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvSk.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.naziv,
            this.iznos,
            this.id_porez});
			this.dgvSk.Location = new System.Drawing.Point(13, 157);
			this.dgvSk.Name = "dgvSk";
			this.dgvSk.Size = new System.Drawing.Size(330, 253);
			this.dgvSk.TabIndex = 6;
			this.dgvSk.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSk_CellEndEdit);
			// 
			// naziv
			// 
			this.naziv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.naziv.HeaderText = "Naziv";
			this.naziv.Name = "naziv";
			// 
			// iznos
			// 
			this.iznos.HeaderText = "Iznos";
			this.iznos.Name = "iznos";
			// 
			// id_porez
			// 
			this.id_porez.HeaderText = "id_porez";
			this.id_porez.Name = "id_porez";
			this.id_porez.Visible = false;
			// 
			// frmStopePoreza
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(368, 422);
			this.Controls.Add(this.dgvSk);
			this.Controls.Add(this.btnNovo);
			this.Controls.Add(this.txtIznos);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtNaziv);
			this.Controls.Add(this.label1);
			this.Name = "frmStopePoreza";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Porezi (PDV)";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStopePoreza_FormClosing);
			this.Load += new System.EventHandler(this.frmSkladista_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgvSk)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNaziv;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIznos;
        private System.Windows.Forms.DataGridView dgvSk;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_porez;
    }
}