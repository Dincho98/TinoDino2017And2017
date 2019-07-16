namespace PCPOS.Robno {
    partial class frmKnjiznoOdobrenjeAll {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKnjiznoOdobrenjeAll));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnSrch = new System.Windows.Forms.Button();
            this.dtpDo = new System.Windows.Forms.DateTimePicker();
            this.dtpOd = new System.Windows.Forms.DateTimePicker();
            this.pnlFill = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvKnjiznoOdobrenje = new System.Windows.Forms.DataGridView();
            this.dgvKnjiznoOdobrenjeItems = new System.Windows.Forms.DataGridView();
            this.pblBottom = new System.Windows.Forms.Panel();
            this.pnlTop.SuspendLayout();
            this.pnlFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKnjiznoOdobrenje)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKnjiznoOdobrenjeItems)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.btnPrint);
            this.pnlTop.Controls.Add(this.btnSrch);
            this.pnlTop.Controls.Add(this.dtpDo);
            this.pnlTop.Controls.Add(this.dtpOd);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1015, 100);
            this.pnlTop.TabIndex = 0;
            // 
            // btnPrint
            // 
            this.btnPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrint.BackgroundImage")));
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnPrint.Location = new System.Drawing.Point(928, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(80, 45);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "Ispiši";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSrch
            // 
            this.btnSrch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSrch.BackgroundImage")));
            this.btnSrch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSrch.FlatAppearance.BorderSize = 0;
            this.btnSrch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSrch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSrch.Location = new System.Drawing.Point(452, 12);
            this.btnSrch.Name = "btnSrch";
            this.btnSrch.Size = new System.Drawing.Size(80, 45);
            this.btnSrch.TabIndex = 2;
            this.btnSrch.Text = "Prikaži";
            this.btnSrch.UseVisualStyleBackColor = true;
            this.btnSrch.Click += new System.EventHandler(this.btnSrch_Click);
            // 
            // dtpDo
            // 
            this.dtpDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDo.Location = new System.Drawing.Point(218, 12);
            this.dtpDo.Name = "dtpDo";
            this.dtpDo.Size = new System.Drawing.Size(200, 26);
            this.dtpDo.TabIndex = 1;
            // 
            // dtpOd
            // 
            this.dtpOd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpOd.Location = new System.Drawing.Point(12, 12);
            this.dtpOd.Name = "dtpOd";
            this.dtpOd.Size = new System.Drawing.Size(200, 26);
            this.dtpOd.TabIndex = 0;
            // 
            // pnlFill
            // 
            this.pnlFill.Controls.Add(this.splitContainer1);
            this.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFill.Location = new System.Drawing.Point(0, 100);
            this.pnlFill.Name = "pnlFill";
            this.pnlFill.Size = new System.Drawing.Size(1015, 406);
            this.pnlFill.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 6);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvKnjiznoOdobrenje);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvKnjiznoOdobrenjeItems);
            this.splitContainer1.Size = new System.Drawing.Size(991, 388);
            this.splitContainer1.SplitterDistance = 194;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgvKnjiznoOdobrenje
            // 
            this.dgvKnjiznoOdobrenje.AllowUserToAddRows = false;
            this.dgvKnjiznoOdobrenje.AllowUserToDeleteRows = false;
            this.dgvKnjiznoOdobrenje.AllowUserToResizeRows = false;
            this.dgvKnjiznoOdobrenje.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKnjiznoOdobrenje.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvKnjiznoOdobrenje.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvKnjiznoOdobrenje.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKnjiznoOdobrenje.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKnjiznoOdobrenje.Location = new System.Drawing.Point(0, 0);
            this.dgvKnjiznoOdobrenje.MultiSelect = false;
            this.dgvKnjiznoOdobrenje.Name = "dgvKnjiznoOdobrenje";
            this.dgvKnjiznoOdobrenje.ReadOnly = true;
            this.dgvKnjiznoOdobrenje.RowHeadersVisible = false;
            this.dgvKnjiznoOdobrenje.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKnjiznoOdobrenje.Size = new System.Drawing.Size(991, 194);
            this.dgvKnjiznoOdobrenje.TabIndex = 0;
            this.dgvKnjiznoOdobrenje.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKnjiznoOdobrenje_RowEnter);
            // 
            // dgvKnjiznoOdobrenjeItems
            // 
            this.dgvKnjiznoOdobrenjeItems.AllowUserToAddRows = false;
            this.dgvKnjiznoOdobrenjeItems.AllowUserToDeleteRows = false;
            this.dgvKnjiznoOdobrenjeItems.AllowUserToResizeRows = false;
            this.dgvKnjiznoOdobrenjeItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKnjiznoOdobrenjeItems.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvKnjiznoOdobrenjeItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvKnjiznoOdobrenjeItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKnjiznoOdobrenjeItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKnjiznoOdobrenjeItems.Location = new System.Drawing.Point(0, 0);
            this.dgvKnjiznoOdobrenjeItems.MultiSelect = false;
            this.dgvKnjiznoOdobrenjeItems.Name = "dgvKnjiznoOdobrenjeItems";
            this.dgvKnjiznoOdobrenjeItems.ReadOnly = true;
            this.dgvKnjiznoOdobrenjeItems.RowHeadersVisible = false;
            this.dgvKnjiznoOdobrenjeItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKnjiznoOdobrenjeItems.Size = new System.Drawing.Size(991, 190);
            this.dgvKnjiznoOdobrenjeItems.TabIndex = 0;
            // 
            // pblBottom
            // 
            this.pblBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pblBottom.Location = new System.Drawing.Point(0, 506);
            this.pblBottom.Name = "pblBottom";
            this.pblBottom.Size = new System.Drawing.Size(1015, 100);
            this.pblBottom.TabIndex = 2;
            // 
            // frmKnjiznoOdobrenjeAll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(1015, 606);
            this.Controls.Add(this.pnlFill);
            this.Controls.Add(this.pblBottom);
            this.Controls.Add(this.pnlTop);
            this.Name = "frmKnjiznoOdobrenjeAll";
            this.Text = "frmKnjiznoOdobrenjeAll";
            this.Load += new System.EventHandler(this.frmKnjiznoOdobrenjeAll_Load);
            this.SizeChanged += new System.EventHandler(this.frmKnjiznoOdobrenjeAll_SizeChanged);
            this.pnlTop.ResumeLayout(false);
            this.pnlFill.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKnjiznoOdobrenje)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKnjiznoOdobrenjeItems)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlFill;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnSrch;
        private System.Windows.Forms.DateTimePicker dtpDo;
        private System.Windows.Forms.DateTimePicker dtpOd;
        public System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvKnjiznoOdobrenje;
        private System.Windows.Forms.DataGridView dgvKnjiznoOdobrenjeItems;
        private System.Windows.Forms.Panel pblBottom;
    }
}