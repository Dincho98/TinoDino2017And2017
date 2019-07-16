namespace PCPOS.Sifarnik
{
    partial class frmSkladista
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
            this.dgvSk = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSkladiste = new System.Windows.Forms.TextBox();
            this.cbGrad = new System.Windows.Forms.ComboBox();
            this.cbZemlja = new System.Windows.Forms.ComboBox();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnNovo = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.chbAktivnost = new System.Windows.Forms.CheckBox();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.chbGlavno = new System.Windows.Forms.CheckBox();
            this.skladiste = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drzava = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aktivnost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.glavno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_grad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_drzava = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_skladiste = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSk)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSk
            // 
            this.dgvSk.AllowUserToAddRows = false;
            this.dgvSk.AllowUserToDeleteRows = false;
            this.dgvSk.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSk.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSk.BackgroundColor = System.Drawing.Color.White;
            this.dgvSk.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSk.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.skladiste,
            this.grad,
            this.drzava,
            this.aktivnost,
            this.glavno,
            this.id_grad,
            this.id_drzava,
            this.id_skladiste});
            this.dgvSk.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgvSk.Location = new System.Drawing.Point(12, 144);
            this.dgvSk.Name = "dgvSk";
            this.dgvSk.ReadOnly = true;
            this.dgvSk.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSk.Size = new System.Drawing.Size(610, 266);
            this.dgvSk.TabIndex = 0;
            this.dgvSk.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSk_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(5, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ime skladišta:";
            // 
            // txtSkladiste
            // 
            this.txtSkladiste.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtSkladiste.Location = new System.Drawing.Point(93, 29);
            this.txtSkladiste.Name = "txtSkladiste";
            this.txtSkladiste.Size = new System.Drawing.Size(250, 23);
            this.txtSkladiste.TabIndex = 2;
            // 
            // cbGrad
            // 
            this.cbGrad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGrad.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbGrad.FormattingEnabled = true;
            this.cbGrad.Location = new System.Drawing.Point(93, 55);
            this.cbGrad.Name = "cbGrad";
            this.cbGrad.Size = new System.Drawing.Size(250, 24);
            this.cbGrad.TabIndex = 3;
            // 
            // cbZemlja
            // 
            this.cbZemlja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbZemlja.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbZemlja.FormattingEnabled = true;
            this.cbZemlja.Location = new System.Drawing.Point(93, 82);
            this.cbZemlja.Name = "cbZemlja";
            this.cbZemlja.Size = new System.Drawing.Size(250, 24);
            this.cbZemlja.TabIndex = 4;
            // 
            // btnSpremi
            // 
            this.btnSpremi.Location = new System.Drawing.Point(378, 62);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(146, 28);
            this.btnSpremi.TabIndex = 5;
            this.btnSpremi.Text = "Spremi promjene";
            this.btnSpremi.UseVisualStyleBackColor = true;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(50, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Grad:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(39, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Zemlja:";
            // 
            // btnNovo
            // 
            this.btnNovo.Location = new System.Drawing.Point(378, 28);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(146, 28);
            this.btnNovo.TabIndex = 5;
            this.btnNovo.Text = "Dodaj novo skladište";
            this.btnNovo.UseVisualStyleBackColor = true;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(30, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "Aktivnost:";
            // 
            // chbAktivnost
            // 
            this.chbAktivnost.AutoSize = true;
            this.chbAktivnost.BackColor = System.Drawing.Color.Transparent;
            this.chbAktivnost.Location = new System.Drawing.Point(93, 109);
            this.chbAktivnost.Name = "chbAktivnost";
            this.chbAktivnost.Size = new System.Drawing.Size(15, 14);
            this.chbAktivnost.TabIndex = 6;
            this.chbAktivnost.UseVisualStyleBackColor = false;
            // 
            // btnOdustani
            // 
            this.btnOdustani.Location = new System.Drawing.Point(378, 96);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(146, 28);
            this.btnOdustani.TabIndex = 5;
            this.btnOdustani.Text = "Odustani";
            this.btnOdustani.UseVisualStyleBackColor = true;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            // 
            // chbGlavno
            // 
            this.chbGlavno.AutoSize = true;
            this.chbGlavno.BackColor = System.Drawing.Color.Transparent;
            this.chbGlavno.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chbGlavno.Location = new System.Drawing.Point(169, 108);
            this.chbGlavno.Name = "chbGlavno";
            this.chbGlavno.Size = new System.Drawing.Size(63, 17);
            this.chbGlavno.TabIndex = 9;
            this.chbGlavno.Text = "Glavno:";
            this.chbGlavno.UseVisualStyleBackColor = false;
            // 
            // skladiste
            // 
            this.skladiste.DataPropertyName = "skladiste";
            this.skladiste.HeaderText = "Skladište";
            this.skladiste.Name = "skladiste";
            this.skladiste.ReadOnly = true;
            // 
            // grad
            // 
            this.grad.DataPropertyName = "grad";
            this.grad.HeaderText = "Grad";
            this.grad.Name = "grad";
            this.grad.ReadOnly = true;
            // 
            // drzava
            // 
            this.drzava.DataPropertyName = "drzava";
            this.drzava.HeaderText = "Država";
            this.drzava.Name = "drzava";
            this.drzava.ReadOnly = true;
            // 
            // aktivnost
            // 
            this.aktivnost.DataPropertyName = "aktivnost";
            this.aktivnost.HeaderText = "Aktivnost";
            this.aktivnost.Name = "aktivnost";
            this.aktivnost.ReadOnly = true;
            // 
            // glavno
            // 
            this.glavno.DataPropertyName = "glavno";
            this.glavno.HeaderText = "Glavno";
            this.glavno.Name = "glavno";
            this.glavno.ReadOnly = true;
            // 
            // id_grad
            // 
            this.id_grad.DataPropertyName = "id_grad";
            this.id_grad.HeaderText = "id_grad";
            this.id_grad.Name = "id_grad";
            this.id_grad.ReadOnly = true;
            this.id_grad.Visible = false;
            // 
            // id_drzava
            // 
            this.id_drzava.DataPropertyName = "id_drzava";
            this.id_drzava.HeaderText = "id_drzava";
            this.id_drzava.Name = "id_drzava";
            this.id_drzava.ReadOnly = true;
            this.id_drzava.Visible = false;
            // 
            // id_skladiste
            // 
            this.id_skladiste.DataPropertyName = "id_skladiste";
            this.id_skladiste.HeaderText = "id_skladiste";
            this.id_skladiste.Name = "id_skladiste";
            this.id_skladiste.ReadOnly = true;
            this.id_skladiste.Visible = false;
            // 
            // frmSkladista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 422);
            this.Controls.Add(this.chbGlavno);
            this.Controls.Add(this.chbAktivnost);
            this.Controls.Add(this.btnNovo);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.cbZemlja);
            this.Controls.Add(this.cbGrad);
            this.Controls.Add(this.txtSkladiste);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvSk);
            this.Name = "frmSkladista";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Skladišta";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GasenjeForme_FormClosing);
            this.Load += new System.EventHandler(this.frmSkladista_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSk)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSkladiste;
        private System.Windows.Forms.ComboBox cbGrad;
        private System.Windows.Forms.ComboBox cbZemlja;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chbAktivnost;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.CheckBox chbGlavno;
        private System.Windows.Forms.DataGridViewTextBoxColumn skladiste;
        private System.Windows.Forms.DataGridViewTextBoxColumn grad;
        private System.Windows.Forms.DataGridViewTextBoxColumn drzava;
        private System.Windows.Forms.DataGridViewTextBoxColumn aktivnost;
        private System.Windows.Forms.DataGridViewTextBoxColumn glavno;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_grad;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_drzava;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_skladiste;
    }
}