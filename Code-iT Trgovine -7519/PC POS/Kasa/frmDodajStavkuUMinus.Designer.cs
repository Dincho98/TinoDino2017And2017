namespace PCPOS.Kasa
{
    partial class frmDodajStavkuUMinus
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnSrch = new System.Windows.Forms.Button();
            this.lblBaza = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.lblBrojRacuna = new System.Windows.Forms.Label();
            this.pnlFill = new System.Windows.Forms.Panel();
            this.pnlFillTop = new System.Windows.Forms.Panel();
            this.pnlFillFill = new System.Windows.Forms.Panel();
            this.dgvRez = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cmbBaza = new System.Windows.Forms.ComboBox();
            this.lblKolicina = new System.Windows.Forms.Label();
            this.txtKolicina = new System.Windows.Forms.TextBox();
            this.lblMinus = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.pnlFill.SuspendLayout();
            this.pnlFillTop.SuspendLayout();
            this.pnlFillFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRez)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.cmbBaza);
            this.pnlTop.Controls.Add(this.btnSrch);
            this.pnlTop.Controls.Add(this.lblBaza);
            this.pnlTop.Controls.Add(this.txtInput);
            this.pnlTop.Controls.Add(this.lblBrojRacuna);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(974, 71);
            this.pnlTop.TabIndex = 0;
            // 
            // btnSrch
            // 
            this.btnSrch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSrch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSrch.Location = new System.Drawing.Point(808, 8);
            this.btnSrch.Name = "btnSrch";
            this.btnSrch.Size = new System.Drawing.Size(154, 55);
            this.btnSrch.TabIndex = 4;
            this.btnSrch.Text = "Pretraži";
            this.btnSrch.UseVisualStyleBackColor = true;
            this.btnSrch.Click += new System.EventHandler(this.btnSrch_Click);
            // 
            // lblBaza
            // 
            this.lblBaza.AutoSize = true;
            this.lblBaza.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblBaza.Location = new System.Drawing.Point(316, 27);
            this.lblBaza.Name = "lblBaza";
            this.lblBaza.Size = new System.Drawing.Size(44, 17);
            this.lblBaza.TabIndex = 2;
            this.lblBaza.Text = "Baza:";
            // 
            // txtInput
            // 
            this.txtInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtInput.Location = new System.Drawing.Point(105, 24);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(195, 23);
            this.txtInput.TabIndex = 1;
            this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
            // 
            // lblBrojRacuna
            // 
            this.lblBrojRacuna.AutoSize = true;
            this.lblBrojRacuna.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblBrojRacuna.Location = new System.Drawing.Point(13, 27);
            this.lblBrojRacuna.Name = "lblBrojRacuna";
            this.lblBrojRacuna.Size = new System.Drawing.Size(85, 17);
            this.lblBrojRacuna.TabIndex = 0;
            this.lblBrojRacuna.Text = "Broj računa:";
            // 
            // pnlFill
            // 
            this.pnlFill.Controls.Add(this.pnlFillFill);
            this.pnlFill.Controls.Add(this.pnlFillTop);
            this.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFill.Location = new System.Drawing.Point(0, 71);
            this.pnlFill.Name = "pnlFill";
            this.pnlFill.Size = new System.Drawing.Size(974, 559);
            this.pnlFill.TabIndex = 1;
            // 
            // pnlFillTop
            // 
            this.pnlFillTop.Controls.Add(this.txtKolicina);
            this.pnlFillTop.Controls.Add(this.lblKolicina);
            this.pnlFillTop.Controls.Add(this.btnAdd);
            this.pnlFillTop.Controls.Add(this.lblMinus);
            this.pnlFillTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFillTop.Location = new System.Drawing.Point(0, 0);
            this.pnlFillTop.Name = "pnlFillTop";
            this.pnlFillTop.Size = new System.Drawing.Size(974, 68);
            this.pnlFillTop.TabIndex = 0;
            // 
            // pnlFillFill
            // 
            this.pnlFillFill.Controls.Add(this.dgvRez);
            this.pnlFillFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFillFill.Location = new System.Drawing.Point(0, 68);
            this.pnlFillFill.Name = "pnlFillFill";
            this.pnlFillFill.Size = new System.Drawing.Size(974, 491);
            this.pnlFillFill.TabIndex = 1;
            // 
            // dgvRez
            // 
            this.dgvRez.AllowUserToAddRows = false;
            this.dgvRez.AllowUserToDeleteRows = false;
            this.dgvRez.AllowUserToResizeRows = false;
            this.dgvRez.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRez.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRez.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRez.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRez.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRez.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRez.Location = new System.Drawing.Point(0, 0);
            this.dgvRez.MultiSelect = false;
            this.dgvRez.Name = "dgvRez";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRez.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRez.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRez.Size = new System.Drawing.Size(974, 491);
            this.dgvRez.TabIndex = 0;
            this.dgvRez.SelectionChanged += new System.EventHandler(this.dgvRez_SelectionChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnAdd.Location = new System.Drawing.Point(808, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(154, 55);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Dodaj";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cmbBaza
            // 
            this.cmbBaza.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBaza.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cmbBaza.FormattingEnabled = true;
            this.cmbBaza.Location = new System.Drawing.Point(367, 25);
            this.cmbBaza.Name = "cmbBaza";
            this.cmbBaza.Size = new System.Drawing.Size(154, 24);
            this.cmbBaza.TabIndex = 5;
            // 
            // lblKolicina
            // 
            this.lblKolicina.AutoSize = true;
            this.lblKolicina.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblKolicina.Location = new System.Drawing.Point(13, 25);
            this.lblKolicina.Name = "lblKolicina";
            this.lblKolicina.Size = new System.Drawing.Size(61, 17);
            this.lblKolicina.TabIndex = 6;
            this.lblKolicina.Text = "Količina:";
            // 
            // txtKolicina
            // 
            this.txtKolicina.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtKolicina.Location = new System.Drawing.Point(105, 22);
            this.txtKolicina.Name = "txtKolicina";
            this.txtKolicina.Size = new System.Drawing.Size(195, 23);
            this.txtKolicina.TabIndex = 7;
            this.txtKolicina.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKolicina_KeyDown);
            // 
            // lblMinus
            // 
            this.lblMinus.AutoSize = true;
            this.lblMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblMinus.Location = new System.Drawing.Point(92, 25);
            this.lblMinus.Name = "lblMinus";
            this.lblMinus.Size = new System.Drawing.Size(13, 17);
            this.lblMinus.TabIndex = 8;
            this.lblMinus.Text = "-";
            // 
            // frmDodajStavkuUMinus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 630);
            this.Controls.Add(this.pnlFill);
            this.Controls.Add(this.pnlTop);
            this.Name = "frmDodajStavkuUMinus";
            this.Text = "Dodaj stavku u minus";
            this.Load += new System.EventHandler(this.frmDodajStavkuUMinus_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlFill.ResumeLayout(false);
            this.pnlFillTop.ResumeLayout(false);
            this.pnlFillTop.PerformLayout();
            this.pnlFillFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRez)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlFill;
        private System.Windows.Forms.Label lblBrojRacuna;
        private System.Windows.Forms.Label lblBaza;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnSrch;
        private System.Windows.Forms.Panel pnlFillTop;
        private System.Windows.Forms.Panel pnlFillFill;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cmbBaza;
        public System.Windows.Forms.DataGridView dgvRez;
        private System.Windows.Forms.Label lblKolicina;
        private System.Windows.Forms.TextBox txtKolicina;
        private System.Windows.Forms.Label lblMinus;
    }
}