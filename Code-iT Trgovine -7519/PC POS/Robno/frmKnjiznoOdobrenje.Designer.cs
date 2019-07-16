namespace PCPOS.Robno {
    partial class frmKnjiznoOdobrenje {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKnjiznoOdobrenje));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnKnjiznoOdobrenjeAll = new System.Windows.Forms.Button();
            this.txtPorez = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDodaj = new System.Windows.Forms.Button();
            this.cmbPartner = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlFill = new System.Windows.Forms.Panel();
            this.dgvFakture = new System.Windows.Forms.DataGridView();
            this.broj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valuta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ukupno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.storno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Poslovnica = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oznaci = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.fid_ducan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fid_kasa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_ducan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_kasa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTop.SuspendLayout();
            this.pnlFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFakture)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.btnKnjiznoOdobrenjeAll);
            this.pnlTop.Controls.Add(this.txtPorez);
            this.pnlTop.Controls.Add(this.label2);
            this.pnlTop.Controls.Add(this.btnDodaj);
            this.pnlTop.Controls.Add(this.cmbPartner);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1015, 55);
            this.pnlTop.TabIndex = 0;
            // 
            // btnKnjiznoOdobrenjeAll
            // 
            this.btnKnjiznoOdobrenjeAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnKnjiznoOdobrenjeAll.BackgroundImage")));
            this.btnKnjiznoOdobrenjeAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnKnjiznoOdobrenjeAll.FlatAppearance.BorderSize = 0;
            this.btnKnjiznoOdobrenjeAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKnjiznoOdobrenjeAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnKnjiznoOdobrenjeAll.Location = new System.Drawing.Point(913, 5);
            this.btnKnjiznoOdobrenjeAll.Name = "btnKnjiznoOdobrenjeAll";
            this.btnKnjiznoOdobrenjeAll.Size = new System.Drawing.Size(90, 45);
            this.btnKnjiznoOdobrenjeAll.TabIndex = 4;
            this.btnKnjiznoOdobrenjeAll.Text = "Sva odobrenja";
            this.btnKnjiznoOdobrenjeAll.UseVisualStyleBackColor = true;
            this.btnKnjiznoOdobrenjeAll.Click += new System.EventHandler(this.btnKnjiznoOdobrenjeAll_Click);
            // 
            // txtPorez
            // 
            this.txtPorez.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtPorez.Location = new System.Drawing.Point(601, 16);
            this.txtPorez.Name = "txtPorez";
            this.txtPorez.Size = new System.Drawing.Size(100, 22);
            this.txtPorez.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(531, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Postotak:";
            // 
            // btnDodaj
            // 
            this.btnDodaj.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDodaj.BackgroundImage")));
            this.btnDodaj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDodaj.FlatAppearance.BorderSize = 0;
            this.btnDodaj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDodaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnDodaj.Location = new System.Drawing.Point(707, 5);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(90, 45);
            this.btnDodaj.TabIndex = 0;
            this.btnDodaj.Text = "Dodaj";
            this.btnDodaj.UseVisualStyleBackColor = true;
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);
            // 
            // cmbPartner
            // 
            this.cmbPartner.BackColor = System.Drawing.SystemColors.MenuBar;
            this.cmbPartner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPartner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPartner.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cmbPartner.FormattingEnabled = true;
            this.cmbPartner.Location = new System.Drawing.Point(131, 13);
            this.cmbPartner.Name = "cmbPartner";
            this.cmbPartner.Size = new System.Drawing.Size(383, 24);
            this.cmbPartner.TabIndex = 1;
            this.cmbPartner.SelectionChangeCommitted += new System.EventHandler(this.cmbPartner_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Odabir partnera:";
            // 
            // pnlBottom
            // 
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 506);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1015, 100);
            this.pnlBottom.TabIndex = 1;
            // 
            // pnlFill
            // 
            this.pnlFill.Controls.Add(this.dgvFakture);
            this.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFill.Location = new System.Drawing.Point(0, 55);
            this.pnlFill.Name = "pnlFill";
            this.pnlFill.Size = new System.Drawing.Size(1015, 451);
            this.pnlFill.TabIndex = 2;
            // 
            // dgvFakture
            // 
            this.dgvFakture.AllowUserToAddRows = false;
            this.dgvFakture.AllowUserToDeleteRows = false;
            this.dgvFakture.AllowUserToOrderColumns = true;
            this.dgvFakture.AllowUserToResizeColumns = false;
            this.dgvFakture.AllowUserToResizeRows = false;
            this.dgvFakture.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFakture.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvFakture.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFakture.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFakture.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFakture.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.broj,
            this.datum,
            this.valuta,
            this.partner,
            this.ukupno,
            this.storno,
            this.Poslovnica,
            this.oznaci,
            this.fid_ducan,
            this.fid_kasa,
            this.id_ducan,
            this.id_kasa});
            this.dgvFakture.Location = new System.Drawing.Point(12, 6);
            this.dgvFakture.Name = "dgvFakture";
            this.dgvFakture.RowHeadersVisible = false;
            this.dgvFakture.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFakture.Size = new System.Drawing.Size(991, 439);
            this.dgvFakture.TabIndex = 0;
            // 
            // broj
            // 
            this.broj.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.broj.DataPropertyName = "broj_fakture";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.broj.DefaultCellStyle = dataGridViewCellStyle2;
            this.broj.HeaderText = "Broj";
            this.broj.Name = "broj";
            this.broj.Width = 50;
            // 
            // datum
            // 
            this.datum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.datum.DataPropertyName = "date";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.datum.DefaultCellStyle = dataGridViewCellStyle3;
            this.datum.HeaderText = "Datum";
            this.datum.Name = "datum";
            this.datum.Width = 120;
            // 
            // valuta
            // 
            this.valuta.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.valuta.DataPropertyName = "ime_valute";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.valuta.DefaultCellStyle = dataGridViewCellStyle4;
            this.valuta.HeaderText = "Valuta";
            this.valuta.Name = "valuta";
            this.valuta.Width = 70;
            // 
            // partner
            // 
            this.partner.DataPropertyName = "ime_tvrtke";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.partner.DefaultCellStyle = dataGridViewCellStyle5;
            this.partner.HeaderText = "Partner";
            this.partner.Name = "partner";
            // 
            // ukupno
            // 
            this.ukupno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ukupno.DataPropertyName = "ukupno";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            this.ukupno.DefaultCellStyle = dataGridViewCellStyle6;
            this.ukupno.HeaderText = "Ukupno";
            this.ukupno.Name = "ukupno";
            this.ukupno.Width = 80;
            // 
            // storno
            // 
            this.storno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.storno.DataPropertyName = "storno";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.storno.DefaultCellStyle = dataGridViewCellStyle7;
            this.storno.HeaderText = "Storno";
            this.storno.Name = "storno";
            this.storno.Width = 80;
            // 
            // Poslovnica
            // 
            this.Poslovnica.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Poslovnica.DataPropertyName = "ime_ducana";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Poslovnica.DefaultCellStyle = dataGridViewCellStyle8;
            this.Poslovnica.HeaderText = "Poslovnica";
            this.Poslovnica.Name = "Poslovnica";
            this.Poslovnica.Width = 80;
            // 
            // oznaci
            // 
            this.oznaci.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.oznaci.HeaderText = "Označi";
            this.oznaci.Name = "oznaci";
            this.oznaci.Width = 60;
            // 
            // fid_ducan
            // 
            this.fid_ducan.DataPropertyName = "fid_ducan";
            this.fid_ducan.HeaderText = "fid_ducan";
            this.fid_ducan.Name = "fid_ducan";
            this.fid_ducan.Visible = false;
            // 
            // fid_kasa
            // 
            this.fid_kasa.DataPropertyName = "fid_kasa";
            this.fid_kasa.HeaderText = "fid_kasa";
            this.fid_kasa.Name = "fid_kasa";
            this.fid_kasa.Visible = false;
            // 
            // id_ducan
            // 
            this.id_ducan.DataPropertyName = "id_ducan";
            this.id_ducan.HeaderText = "id_ducan";
            this.id_ducan.Name = "id_ducan";
            this.id_ducan.Visible = false;
            // 
            // id_kasa
            // 
            this.id_kasa.DataPropertyName = "id_kasa";
            this.id_kasa.HeaderText = "id_kasa";
            this.id_kasa.Name = "id_kasa";
            this.id_kasa.Visible = false;
            // 
            // frmKnjiznoOdobrenje
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(1015, 606);
            this.Controls.Add(this.pnlFill);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.Name = "frmKnjiznoOdobrenje";
            this.Text = "Knjižno Odobrenje";
            this.Load += new System.EventHandler(this.frmKnjiznoOdobrenje_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFakture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.ComboBox cmbPartner;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlFill;
        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.DataGridView dgvFakture;
        private System.Windows.Forms.TextBox txtPorez;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum;
        private System.Windows.Forms.DataGridViewTextBoxColumn valuta;
        private System.Windows.Forms.DataGridViewTextBoxColumn partner;
        private System.Windows.Forms.DataGridViewTextBoxColumn ukupno;
        private System.Windows.Forms.DataGridViewTextBoxColumn storno;
        private System.Windows.Forms.DataGridViewTextBoxColumn Poslovnica;
        private System.Windows.Forms.DataGridViewCheckBoxColumn oznaci;
        private System.Windows.Forms.DataGridViewTextBoxColumn fid_ducan;
        private System.Windows.Forms.DataGridViewTextBoxColumn fid_kasa;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_ducan;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_kasa;
        private System.Windows.Forms.Button btnKnjiznoOdobrenjeAll;
    }
}