namespace PCPOS.Sifarnik
{
    partial class frmGrupeProizvoda
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGrupeProizvoda));
            this.dgv = new System.Windows.Forms.DataGridView();
            this.grupa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_grupa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnNoviUnos = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtnazivGrupe = new System.Windows.Forms.TextBox();
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
            this.grupa,
            this.id_grupa});
            this.dgv.Location = new System.Drawing.Point(14, 111);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(559, 483);
            this.dgv.TabIndex = 102;
            this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            // 
            // grupa
            // 
            this.grupa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.grupa.HeaderText = "Grupa";
            this.grupa.Name = "grupa";
            // 
            // id_grupa
            // 
            this.id_grupa.HeaderText = "id_grupa";
            this.id_grupa.Name = "id_grupa";
            this.id_grupa.ReadOnly = true;
            this.id_grupa.Visible = false;
            // 
            // btnNoviUnos
            // 
            this.btnNoviUnos.BackColor = System.Drawing.Color.Transparent;
            this.btnNoviUnos.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNoviUnos.BackgroundImage")));
            this.btnNoviUnos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNoviUnos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNoviUnos.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnNoviUnos.FlatAppearance.BorderSize = 0;
            this.btnNoviUnos.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnNoviUnos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnNoviUnos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnNoviUnos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNoviUnos.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.btnNoviUnos.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnNoviUnos.Location = new System.Drawing.Point(279, 77);
            this.btnNoviUnos.Name = "btnNoviUnos";
            this.btnNoviUnos.Size = new System.Drawing.Size(134, 29);
            this.btnNoviUnos.TabIndex = 101;
            this.btnNoviUnos.TabStop = false;
            this.btnNoviUnos.Text = "Dodaj novu grupu";
            this.btnNoviUnos.UseVisualStyleBackColor = false;
            this.btnNoviUnos.Click += new System.EventHandler(this.btnNoviUnos_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(10, 12);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(173, 22);
            this.label7.TabIndex = 100;
            this.label7.Text = "Grupe proizvoda:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(12, 84);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 16);
            this.label4.TabIndex = 99;
            this.label4.Text = "Naziv grupe:";
            // 
            // txtnazivGrupe
            // 
            this.txtnazivGrupe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtnazivGrupe.Location = new System.Drawing.Point(97, 81);
            this.txtnazivGrupe.MaxLength = 13;
            this.txtnazivGrupe.Name = "txtnazivGrupe";
            this.txtnazivGrupe.Size = new System.Drawing.Size(170, 22);
            this.txtnazivGrupe.TabIndex = 97;
            // 
            // frmGrupeProizvoda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 613);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.btnNoviUnos);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtnazivGrupe);
            this.Name = "frmGrupeProizvoda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Grupe proizvoda";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GasenjeForme_FormClosing);
            this.Load += new System.EventHandler(this.frmGrupeProizvoda_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnNoviUnos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtnazivGrupe;
        private System.Windows.Forms.DataGridViewTextBoxColumn grupa;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_grupa;

    }
}