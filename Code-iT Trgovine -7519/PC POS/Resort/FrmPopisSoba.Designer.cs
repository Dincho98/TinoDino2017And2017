namespace PCPOS.Resort
{
    partial class FrmPopisSoba
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnNovaSoba = new System.Windows.Forms.Button();
            this.btnUrediSobu = new System.Windows.Forms.Button();
            this.btnTipSobe = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.broj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tip_sobe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.broj_lezaja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cijena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnObrisiSobu = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNovaSoba
            // 
            this.btnNovaSoba.BackColor = System.Drawing.Color.White;
            this.btnNovaSoba.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNovaSoba.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNovaSoba.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnNovaSoba.Location = new System.Drawing.Point(12, 12);
            this.btnNovaSoba.Name = "btnNovaSoba";
            this.btnNovaSoba.Size = new System.Drawing.Size(145, 39);
            this.btnNovaSoba.TabIndex = 0;
            this.btnNovaSoba.Text = "Nova soba";
            this.btnNovaSoba.UseVisualStyleBackColor = false;
            this.btnNovaSoba.Click += new System.EventHandler(this.BtnNovaSoba_Click);
            // 
            // btnUrediSobu
            // 
            this.btnUrediSobu.BackColor = System.Drawing.Color.White;
            this.btnUrediSobu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUrediSobu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUrediSobu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnUrediSobu.Location = new System.Drawing.Point(163, 12);
            this.btnUrediSobu.Name = "btnUrediSobu";
            this.btnUrediSobu.Size = new System.Drawing.Size(145, 39);
            this.btnUrediSobu.TabIndex = 1;
            this.btnUrediSobu.Text = "Uredi sobu";
            this.btnUrediSobu.UseVisualStyleBackColor = false;
            this.btnUrediSobu.Click += new System.EventHandler(this.BtnUrediSobu_Click);
            // 
            // btnTipSobe
            // 
            this.btnTipSobe.BackColor = System.Drawing.Color.White;
            this.btnTipSobe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTipSobe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTipSobe.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnTipSobe.Location = new System.Drawing.Point(465, 12);
            this.btnTipSobe.Name = "btnTipSobe";
            this.btnTipSobe.Size = new System.Drawing.Size(145, 39);
            this.btnTipSobe.TabIndex = 2;
            this.btnTipSobe.Text = "Tip sobe";
            this.btnTipSobe.UseVisualStyleBackColor = false;
            this.btnTipSobe.Click += new System.EventHandler(this.BtnTipSobe_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Popis soba";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.broj,
            this.naziv,
            this.tip_sobe,
            this.broj_lezaja,
            this.cijena,
            this.id});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(1);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.Location = new System.Drawing.Point(12, 93);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(2);
            this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(747, 481);
            this.dataGridView.TabIndex = 4;
            // 
            // broj
            // 
            this.broj.FillWeight = 30F;
            this.broj.HeaderText = "Br.";
            this.broj.Name = "broj";
            this.broj.ReadOnly = true;
            // 
            // naziv
            // 
            this.naziv.FillWeight = 102.7919F;
            this.naziv.HeaderText = "Naziv sobe";
            this.naziv.Name = "naziv";
            this.naziv.ReadOnly = true;
            // 
            // tip_sobe
            // 
            this.tip_sobe.FillWeight = 102.7919F;
            this.tip_sobe.HeaderText = "Tip sobe";
            this.tip_sobe.Name = "tip_sobe";
            this.tip_sobe.ReadOnly = true;
            // 
            // broj_lezaja
            // 
            this.broj_lezaja.FillWeight = 80F;
            this.broj_lezaja.HeaderText = "Broj ležaja";
            this.broj_lezaja.Name = "broj_lezaja";
            this.broj_lezaja.ReadOnly = true;
            // 
            // cijena
            // 
            this.cijena.FillWeight = 102.7919F;
            this.cijena.HeaderText = "Cijena noćenja";
            this.cijena.Name = "cijena";
            this.cijena.ReadOnly = true;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // btnObrisiSobu
            // 
            this.btnObrisiSobu.BackColor = System.Drawing.Color.White;
            this.btnObrisiSobu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnObrisiSobu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnObrisiSobu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnObrisiSobu.Location = new System.Drawing.Point(314, 12);
            this.btnObrisiSobu.Name = "btnObrisiSobu";
            this.btnObrisiSobu.Size = new System.Drawing.Size(145, 39);
            this.btnObrisiSobu.TabIndex = 5;
            this.btnObrisiSobu.Text = "Obriši sobu";
            this.btnObrisiSobu.UseVisualStyleBackColor = false;
            this.btnObrisiSobu.Click += new System.EventHandler(this.BtnObrisiSobu_Click);
            // 
            // FrmPopisSoba
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(771, 586);
            this.Controls.Add(this.btnObrisiSobu);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTipSobe);
            this.Controls.Add(this.btnUrediSobu);
            this.Controls.Add(this.btnNovaSoba);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmPopisSoba";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Popis soba";
            this.Load += new System.EventHandler(this.FrmSobe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNovaSoba;
        private System.Windows.Forms.Button btnUrediSobu;
        private System.Windows.Forms.Button btnTipSobe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button btnObrisiSobu;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn tip_sobe;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj_lezaja;
        private System.Windows.Forms.DataGridViewTextBoxColumn cijena;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
    }
}