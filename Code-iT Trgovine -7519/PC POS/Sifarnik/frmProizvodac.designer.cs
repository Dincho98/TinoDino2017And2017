namespace PCPOS.Sifarnik
{
    partial class frmProizvodac
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProizvodac));
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btnNoviUnos = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtnazivProizvodaca = new System.Windows.Forms.TextBox();
            this.proizvodac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_proizvodac = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.proizvodac,
            this.id_proizvodac});
            this.dgv.Location = new System.Drawing.Point(14, 111);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(559, 483);
            this.dgv.TabIndex = 102;
            this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEndEdit);
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
            this.btnNoviUnos.Location = new System.Drawing.Point(325, 77);
            this.btnNoviUnos.Name = "btnNoviUnos";
            this.btnNoviUnos.Size = new System.Drawing.Size(183, 29);
            this.btnNoviUnos.TabIndex = 101;
            this.btnNoviUnos.TabStop = false;
            this.btnNoviUnos.Text = "Dodaj novog proizvođača";
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
            this.label7.Size = new System.Drawing.Size(127, 22);
            this.label7.TabIndex = 100;
            this.label7.Text = "Proizvođači:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(12, 84);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 16);
            this.label4.TabIndex = 99;
            this.label4.Text = "Naziv proizvođača:";
            // 
            // txtnazivProizvodaca
            // 
            this.txtnazivProizvodaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtnazivProizvodaca.Location = new System.Drawing.Point(143, 81);
            this.txtnazivProizvodaca.MaxLength = 13;
            this.txtnazivProizvodaca.Name = "txtnazivProizvodaca";
            this.txtnazivProizvodaca.Size = new System.Drawing.Size(170, 22);
            this.txtnazivProizvodaca.TabIndex = 97;
            // 
            // proizvodac
            // 
            this.proizvodac.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.proizvodac.HeaderText = "Proizvođač";
            this.proizvodac.Name = "proizvodac";
            // 
            // id_proizvodac
            // 
            this.id_proizvodac.HeaderText = "id_proizvodac";
            this.id_proizvodac.Name = "id_proizvodac";
            this.id_proizvodac.ReadOnly = true;
            this.id_proizvodac.Visible = false;
            // 
            // frmProizvodac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 613);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.btnNoviUnos);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtnazivProizvodaca);
            this.Name = "frmProizvodac";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Grupe proizvoda";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmProizvodac_FormClosing);
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
        private System.Windows.Forms.TextBox txtnazivProizvodaca;
        private System.Windows.Forms.DataGridViewTextBoxColumn proizvodac;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_proizvodac;

    }
}