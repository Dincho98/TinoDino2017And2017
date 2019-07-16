namespace PCPOS.sinkronizacija_poslovnica {
    partial class frmSkladistaSinkronizacija {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent () {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvSkladiste = new System.Windows.Forms.DataGridView();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.id_skladiste = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skladiste = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_skl_centrala = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSkladiste)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSkladiste
            // 
            this.dgvSkladiste.AllowUserToAddRows = false;
            this.dgvSkladiste.AllowUserToDeleteRows = false;
            this.dgvSkladiste.AllowUserToOrderColumns = true;
            this.dgvSkladiste.AllowUserToResizeRows = false;
            this.dgvSkladiste.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSkladiste.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSkladiste.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSkladiste.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvSkladiste.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSkladiste.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_skladiste,
            this.skladiste,
            this.id_skl_centrala});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSkladiste.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvSkladiste.Location = new System.Drawing.Point(13, 13);
            this.dgvSkladiste.Name = "dgvSkladiste";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSkladiste.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvSkladiste.RowHeadersVisible = false;
            this.dgvSkladiste.RowHeadersWidth = 5;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dgvSkladiste.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvSkladiste.Size = new System.Drawing.Size(363, 251);
            this.dgvSkladiste.TabIndex = 0;
            // 
            // btnSpremi
            // 
            this.btnSpremi.Location = new System.Drawing.Point(301, 270);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(75, 26);
            this.btnSpremi.TabIndex = 1;
            this.btnSpremi.Text = "Spremi";
            this.btnSpremi.UseVisualStyleBackColor = true;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // id_skladiste
            // 
            this.id_skladiste.DataPropertyName = "id_skladiste";
            this.id_skladiste.HeaderText = "ID";
            this.id_skladiste.Name = "id_skladiste";
            this.id_skladiste.Visible = false;
            // 
            // skladiste
            // 
            this.skladiste.DataPropertyName = "skladiste";
            this.skladiste.HeaderText = "Skladište";
            this.skladiste.Name = "skladiste";
            // 
            // id_skl_centrala
            // 
            this.id_skl_centrala.DataPropertyName = "id_skl_centrala";
            this.id_skl_centrala.HeaderText = "Centralno skladište";
            this.id_skl_centrala.Name = "id_skl_centrala";
            // 
            // frmSkladistaSinkronizacija
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(388, 308);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.dgvSkladiste);
            this.MaximumSize = new System.Drawing.Size(404, 346);
            this.MinimumSize = new System.Drawing.Size(404, 346);
            this.Name = "frmSkladistaSinkronizacija";
            this.Text = "frmSkladistaSinkronizacija";
            this.Load += new System.EventHandler(this.frmSkladistaSinkronizacija_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSkladiste)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSkladiste;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_skladiste;
        private System.Windows.Forms.DataGridViewTextBoxColumn skladiste;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_skl_centrala;
    }
}