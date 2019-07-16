namespace PCPOS.Sifarnik
{
    partial class frmPodGrupe
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
            this.btnDodaj = new System.Windows.Forms.Button();
            this.cbgrupe = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.txtpodgrupa = new System.Windows.Forms.TextBox();
            this.btnobrisi = new System.Windows.Forms.Button();
            this.id_podgrupa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Grupa = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDodaj
            // 
            this.btnDodaj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDodaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnDodaj.Location = new System.Drawing.Point(347, 8);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(128, 32);
            this.btnDodaj.TabIndex = 4;
            this.btnDodaj.Text = "Dodaj podgrupu";
            this.btnDodaj.UseVisualStyleBackColor = true;
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);
            // 
            // cbgrupe
            // 
            this.cbgrupe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbgrupe.FormattingEnabled = true;
            this.cbgrupe.Location = new System.Drawing.Point(75, 8);
            this.cbgrupe.Name = "cbgrupe";
            this.cbgrupe.Size = new System.Drawing.Size(197, 21);
            this.cbgrupe.TabIndex = 6;
            this.cbgrupe.SelectedValueChanged += new System.EventHandler(this.cbgrupe_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(9, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Grupe";
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_podgrupa,
            this.naziv,
            this.Grupa});
            this.dgv.Enabled = false;
            this.dgv.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgv.Location = new System.Drawing.Point(12, 97);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(507, 244);
            this.dgv.TabIndex = 5;
            this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(8, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Podgrupa";
            // 
            // txtpodgrupa
            // 
            this.txtpodgrupa.Location = new System.Drawing.Point(75, 54);
            this.txtpodgrupa.Name = "txtpodgrupa";
            this.txtpodgrupa.Size = new System.Drawing.Size(197, 20);
            this.txtpodgrupa.TabIndex = 9;
            // 
            // btnobrisi
            // 
            this.btnobrisi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnobrisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnobrisi.Location = new System.Drawing.Point(347, 56);
            this.btnobrisi.Name = "btnobrisi";
            this.btnobrisi.Size = new System.Drawing.Size(128, 32);
            this.btnobrisi.TabIndex = 10;
            this.btnobrisi.Text = "Obriši podgrupu";
            this.btnobrisi.UseVisualStyleBackColor = true;
            this.btnobrisi.Click += new System.EventHandler(this.btnobrisi_Click);
            // 
            // id_podgrupa
            // 
            this.id_podgrupa.HeaderText = "ID";
            this.id_podgrupa.Name = "id_podgrupa";
            this.id_podgrupa.ReadOnly = true;
            // 
            // naziv
            // 
            this.naziv.HeaderText = "Ime podgrupe";
            this.naziv.Name = "naziv";
            // 
            // Grupa
            // 
            this.Grupa.HeaderText = "Grupa";
            this.Grupa.Name = "Grupa";
            this.Grupa.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Grupa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // frmPodGrupe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 353);
            this.Controls.Add(this.btnobrisi);
            this.Controls.Add(this.txtpodgrupa);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbgrupe);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.btnDodaj);
            this.Name = "frmPodGrupe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pod grupe";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPodGrupe_FormClosing);
            this.Load += new System.EventHandler(this.frmGrupeProizvoda_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.ComboBox cbgrupe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtpodgrupa;
        private System.Windows.Forms.Button btnobrisi;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_podgrupa;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewComboBoxColumn Grupa;
    }
}