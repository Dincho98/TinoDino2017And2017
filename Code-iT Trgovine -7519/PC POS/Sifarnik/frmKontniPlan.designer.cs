namespace PCPOS.Sifarnik
{
    partial class frmKontniPlan
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
            this.lblsifrakonto = new System.Windows.Forms.Label();
            this.txtsifrakonta = new System.Windows.Forms.TextBox();
            this.btntrazikonto = new System.Windows.Forms.Button();
            this.btnlijevo = new System.Windows.Forms.Button();
            this.btndesno = new System.Windows.Forms.Button();
            this.txtopiskonta = new System.Windows.Forms.TextBox();
            this.cbvrstakonta = new System.Windows.Forms.ComboBox();
            this.lblopiskonta = new System.Windows.Forms.Label();
            this.lblvrstakonta = new System.Windows.Forms.Label();
            this.lblstatus = new System.Windows.Forms.Label();
            this.cbstatus = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnizmjeni = new System.Windows.Forms.Button();
            this.btnspremi = new System.Windows.Forms.Button();
            this.btnnoviunos = new System.Windows.Forms.Button();
            this.btnspreminovi = new System.Windows.Forms.Button();
            this.btnodustani = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblsifrakonto
            // 
            this.lblsifrakonto.AutoSize = true;
            this.lblsifrakonto.BackColor = System.Drawing.Color.Transparent;
            this.lblsifrakonto.Location = new System.Drawing.Point(43, 15);
            this.lblsifrakonto.Name = "lblsifrakonto";
            this.lblsifrakonto.Size = new System.Drawing.Size(58, 13);
            this.lblsifrakonto.TabIndex = 0;
            this.lblsifrakonto.Text = "Šifra konta";
            // 
            // txtsifrakonta
            // 
            this.txtsifrakonta.Location = new System.Drawing.Point(107, 12);
            this.txtsifrakonta.Name = "txtsifrakonta";
            this.txtsifrakonta.Size = new System.Drawing.Size(137, 20);
            this.txtsifrakonta.TabIndex = 1;
            this.txtsifrakonta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtsifrakonta_KeyDown);
            this.txtsifrakonta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtsifrakonta_KeyPress);
            // 
            // btntrazikonto
            // 
            this.btntrazikonto.Location = new System.Drawing.Point(250, 12);
            this.btntrazikonto.Name = "btntrazikonto";
            this.btntrazikonto.Size = new System.Drawing.Size(29, 20);
            this.btntrazikonto.TabIndex = 2;
            this.btntrazikonto.Text = "...";
            this.btntrazikonto.UseVisualStyleBackColor = true;
            this.btntrazikonto.Click += new System.EventHandler(this.btntrazikonto_Click);
            // 
            // btnlijevo
            // 
            this.btnlijevo.Location = new System.Drawing.Point(303, 11);
            this.btnlijevo.Name = "btnlijevo";
            this.btnlijevo.Size = new System.Drawing.Size(75, 23);
            this.btnlijevo.TabIndex = 3;
            this.btnlijevo.Text = "Lijevo";
            this.btnlijevo.UseVisualStyleBackColor = true;
            this.btnlijevo.Click += new System.EventHandler(this.btnlijevo_Click);
            // 
            // btndesno
            // 
            this.btndesno.Location = new System.Drawing.Point(384, 11);
            this.btndesno.Name = "btndesno";
            this.btndesno.Size = new System.Drawing.Size(75, 23);
            this.btndesno.TabIndex = 4;
            this.btndesno.Text = "Desno";
            this.btndesno.UseVisualStyleBackColor = true;
            this.btndesno.Click += new System.EventHandler(this.btndesno_Click);
            // 
            // txtopiskonta
            // 
            this.txtopiskonta.Enabled = false;
            this.txtopiskonta.Location = new System.Drawing.Point(107, 68);
            this.txtopiskonta.Name = "txtopiskonta";
            this.txtopiskonta.Size = new System.Drawing.Size(813, 20);
            this.txtopiskonta.TabIndex = 6;
            // 
            // cbvrstakonta
            // 
            this.cbvrstakonta.Enabled = false;
            this.cbvrstakonta.FormattingEnabled = true;
            this.cbvrstakonta.Location = new System.Drawing.Point(107, 136);
            this.cbvrstakonta.Name = "cbvrstakonta";
            this.cbvrstakonta.Size = new System.Drawing.Size(117, 21);
            this.cbvrstakonta.TabIndex = 7;
            // 
            // lblopiskonta
            // 
            this.lblopiskonta.AutoSize = true;
            this.lblopiskonta.BackColor = System.Drawing.Color.Transparent;
            this.lblopiskonta.Location = new System.Drawing.Point(43, 71);
            this.lblopiskonta.Name = "lblopiskonta";
            this.lblopiskonta.Size = new System.Drawing.Size(58, 13);
            this.lblopiskonta.TabIndex = 5;
            this.lblopiskonta.Text = "Opis konta";
            // 
            // lblvrstakonta
            // 
            this.lblvrstakonta.AutoSize = true;
            this.lblvrstakonta.BackColor = System.Drawing.Color.Transparent;
            this.lblvrstakonta.Location = new System.Drawing.Point(43, 139);
            this.lblvrstakonta.Name = "lblvrstakonta";
            this.lblvrstakonta.Size = new System.Drawing.Size(61, 13);
            this.lblvrstakonta.TabIndex = 8;
            this.lblvrstakonta.Text = "Vrsta konta";
            // 
            // lblstatus
            // 
            this.lblstatus.AutoSize = true;
            this.lblstatus.BackColor = System.Drawing.Color.Transparent;
            this.lblstatus.Location = new System.Drawing.Point(310, 142);
            this.lblstatus.Name = "lblstatus";
            this.lblstatus.Size = new System.Drawing.Size(37, 13);
            this.lblstatus.TabIndex = 10;
            this.lblstatus.Text = "Status";
            // 
            // cbstatus
            // 
            this.cbstatus.Enabled = false;
            this.cbstatus.FormattingEnabled = true;
            this.cbstatus.Location = new System.Drawing.Point(353, 139);
            this.cbstatus.Name = "cbstatus";
            this.cbstatus.Size = new System.Drawing.Size(117, 21);
            this.cbstatus.TabIndex = 9;
            this.cbstatus.SelectedIndexChanged += new System.EventHandler(this.cbstatus_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.GridColor = System.Drawing.Color.Gainsboro;
            this.dataGridView1.Location = new System.Drawing.Point(46, 194);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(874, 379);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // btnizmjeni
            // 
            this.btnizmjeni.Location = new System.Drawing.Point(35, 60);
            this.btnizmjeni.Name = "btnizmjeni";
            this.btnizmjeni.Size = new System.Drawing.Size(83, 22);
            this.btnizmjeni.TabIndex = 12;
            this.btnizmjeni.Text = "Izmjeni konto";
            this.btnizmjeni.UseVisualStyleBackColor = true;
            this.btnizmjeni.Click += new System.EventHandler(this.btnizmjeni_Click);
            // 
            // btnspremi
            // 
            this.btnspremi.Location = new System.Drawing.Point(124, 60);
            this.btnspremi.Name = "btnspremi";
            this.btnspremi.Size = new System.Drawing.Size(90, 22);
            this.btnspremi.TabIndex = 13;
            this.btnspremi.Text = "Spremi izmjene";
            this.btnspremi.UseVisualStyleBackColor = true;
            this.btnspremi.Visible = false;
            this.btnspremi.Click += new System.EventHandler(this.btnspremi_Click);
            // 
            // btnnoviunos
            // 
            this.btnnoviunos.Location = new System.Drawing.Point(35, 19);
            this.btnnoviunos.Name = "btnnoviunos";
            this.btnnoviunos.Size = new System.Drawing.Size(75, 23);
            this.btnnoviunos.TabIndex = 14;
            this.btnnoviunos.Text = "Novi Unos";
            this.btnnoviunos.UseVisualStyleBackColor = true;
            this.btnnoviunos.Click += new System.EventHandler(this.btnnoviunos_Click);
            // 
            // btnspreminovi
            // 
            this.btnspreminovi.Location = new System.Drawing.Point(116, 19);
            this.btnspreminovi.Name = "btnspreminovi";
            this.btnspreminovi.Size = new System.Drawing.Size(75, 23);
            this.btnspreminovi.TabIndex = 15;
            this.btnspreminovi.Text = "Spremi";
            this.btnspreminovi.UseVisualStyleBackColor = true;
            this.btnspreminovi.Visible = false;
            this.btnspreminovi.Click += new System.EventHandler(this.btnspreminovi_Click);
            // 
            // btnodustani
            // 
            this.btnodustani.Location = new System.Drawing.Point(197, 19);
            this.btnodustani.Name = "btnodustani";
            this.btnodustani.Size = new System.Drawing.Size(75, 23);
            this.btnodustani.TabIndex = 16;
            this.btnodustani.Text = "Odustani";
            this.btnodustani.UseVisualStyleBackColor = true;
            this.btnodustani.Visible = false;
            this.btnodustani.Click += new System.EventHandler(this.btnodustani_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btnodustani);
            this.groupBox1.Controls.Add(this.btnspremi);
            this.groupBox1.Controls.Add(this.btnizmjeni);
            this.groupBox1.Controls.Add(this.btnnoviunos);
            this.groupBox1.Controls.Add(this.btnspreminovi);
            this.groupBox1.Location = new System.Drawing.Point(548, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(314, 94);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kontrole";
            // 
            // frmKontniPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 585);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblstatus);
            this.Controls.Add(this.cbstatus);
            this.Controls.Add(this.lblvrstakonta);
            this.Controls.Add(this.cbvrstakonta);
            this.Controls.Add(this.txtopiskonta);
            this.Controls.Add(this.lblopiskonta);
            this.Controls.Add(this.btndesno);
            this.Controls.Add(this.btnlijevo);
            this.Controls.Add(this.btntrazikonto);
            this.Controls.Add(this.txtsifrakonta);
            this.Controls.Add(this.lblsifrakonto);
            this.Name = "frmKontniPlan";
            this.Text = "Kontni Plan";
            this.Load += new System.EventHandler(this.frmKontniPlan_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmKontniPlan_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblsifrakonto;
        private System.Windows.Forms.TextBox txtsifrakonta;
        private System.Windows.Forms.Button btntrazikonto;
        private System.Windows.Forms.Button btnlijevo;
        private System.Windows.Forms.Button btndesno;
        private System.Windows.Forms.TextBox txtopiskonta;
        private System.Windows.Forms.ComboBox cbvrstakonta;
        private System.Windows.Forms.Label lblopiskonta;
        private System.Windows.Forms.Label lblvrstakonta;
        private System.Windows.Forms.Label lblstatus;
        private System.Windows.Forms.ComboBox cbstatus;
        private System.Windows.Forms.Button btnizmjeni;
        private System.Windows.Forms.Button btnspremi;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnnoviunos;
        private System.Windows.Forms.Button btnspreminovi;
        private System.Windows.Forms.Button btnodustani;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}