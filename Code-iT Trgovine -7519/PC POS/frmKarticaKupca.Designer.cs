namespace PCPOS {
    partial class frmKarticaKupca {
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKarticaKupca));
            this.tmTimer1 = new System.Windows.Forms.Timer(this.components);
            this.txtKarticaKupca = new System.Windows.Forms.TextBox();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDodajRacune = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.dtpDo = new System.Windows.Forms.DateTimePicker();
            this.dtpOd = new System.Windows.Forms.DateTimePicker();
            this.lblKarticaKupca = new System.Windows.Forms.Label();
            this.lblUkupno = new System.Windows.Forms.Label();
            this.lblAdresa = new System.Windows.Forms.Label();
            this.lblNaziv = new System.Windows.Forms.Label();
            this.pnlFill = new System.Windows.Forms.Panel();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnResetKartice = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            this.pnlFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // tmTimer1
            // 
            this.tmTimer1.Interval = 500;
            this.tmTimer1.Tick += new System.EventHandler(this.tmTimer1_Tick);
            // 
            // txtKarticaKupca
            // 
            this.txtKarticaKupca.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtKarticaKupca.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtKarticaKupca.Location = new System.Drawing.Point(12, 93);
            this.txtKarticaKupca.Name = "txtKarticaKupca";
            this.txtKarticaKupca.Size = new System.Drawing.Size(218, 29);
            this.txtKarticaKupca.TabIndex = 0;
            this.txtKarticaKupca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKarticaKupca_KeyPress);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Transparent;
            this.pnlTop.Controls.Add(this.btnResetKartice);
            this.pnlTop.Controls.Add(this.label3);
            this.pnlTop.Controls.Add(this.label2);
            this.pnlTop.Controls.Add(this.btnCancel);
            this.pnlTop.Controls.Add(this.btnDodajRacune);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Controls.Add(this.chkAll);
            this.pnlTop.Controls.Add(this.dtpDo);
            this.pnlTop.Controls.Add(this.dtpOd);
            this.pnlTop.Controls.Add(this.lblKarticaKupca);
            this.pnlTop.Controls.Add(this.lblUkupno);
            this.pnlTop.Controls.Add(this.lblAdresa);
            this.pnlTop.Controls.Add(this.lblNaziv);
            this.pnlTop.Controls.Add(this.txtKarticaKupca);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(995, 125);
            this.pnlTop.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(12, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 26);
            this.label3.TabIndex = 134;
            this.label3.Text = "Adresa: ";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 26);
            this.label2.TabIndex = 133;
            this.label2.Text = "Partner: ";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.DarkRed;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCancel.Location = new System.Drawing.Point(885, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(98, 46);
            this.btnCancel.TabIndex = 131;
            this.btnCancel.Text = "Izlaz ESC";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.Enter += new System.EventHandler(this.dtpOd_Enter);
            // 
            // btnDodajRacune
            // 
            this.btnDodajRacune.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDodajRacune.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDodajRacune.BackgroundImage")));
            this.btnDodajRacune.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDodajRacune.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDodajRacune.FlatAppearance.BorderSize = 0;
            this.btnDodajRacune.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDodajRacune.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDodajRacune.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDodajRacune.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnDodajRacune.Location = new System.Drawing.Point(885, 55);
            this.btnDodajRacune.Name = "btnDodajRacune";
            this.btnDodajRacune.Size = new System.Drawing.Size(98, 35);
            this.btnDodajRacune.TabIndex = 9;
            this.btnDodajRacune.Text = "Dodaj račune";
            this.btnDodajRacune.UseVisualStyleBackColor = true;
            this.btnDodajRacune.Visible = false;
            this.btnDodajRacune.Click += new System.EventHandler(this.btnDodajRacune_Click);
            this.btnDodajRacune.Enter += new System.EventHandler(this.dtpOd_Enter);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 26);
            this.label1.TabIndex = 8;
            this.label1.Text = "Br. kartice:";
            // 
            // chkAll
            // 
            this.chkAll.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkAll.AutoSize = true;
            this.chkAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chkAll.Location = new System.Drawing.Point(562, 95);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(117, 28);
            this.chkAll.TabIndex = 7;
            this.chkAll.Text = "Prikaži sve";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            this.chkAll.Enter += new System.EventHandler(this.chkAll_Enter);
            // 
            // dtpDo
            // 
            this.dtpDo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtpDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDo.Location = new System.Drawing.Point(399, 93);
            this.dtpDo.Name = "dtpDo";
            this.dtpDo.Size = new System.Drawing.Size(157, 29);
            this.dtpDo.TabIndex = 6;
            this.dtpDo.ValueChanged += new System.EventHandler(this.dtpDo_ValueChanged);
            this.dtpDo.Enter += new System.EventHandler(this.dtpDo_Enter);
            // 
            // dtpOd
            // 
            this.dtpOd.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtpOd.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpOd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpOd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpOd.Location = new System.Drawing.Point(236, 93);
            this.dtpOd.Name = "dtpOd";
            this.dtpOd.Size = new System.Drawing.Size(157, 29);
            this.dtpOd.TabIndex = 5;
            this.dtpOd.ValueChanged += new System.EventHandler(this.dtpOd_ValueChanged);
            this.dtpOd.Enter += new System.EventHandler(this.dtpOd_Enter);
            // 
            // lblKarticaKupca
            // 
            this.lblKarticaKupca.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblKarticaKupca.AutoSize = true;
            this.lblKarticaKupca.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblKarticaKupca.Location = new System.Drawing.Point(134, 64);
            this.lblKarticaKupca.Name = "lblKarticaKupca";
            this.lblKarticaKupca.Size = new System.Drawing.Size(70, 26);
            this.lblKarticaKupca.TabIndex = 4;
            this.lblKarticaKupca.Text = "label1";
            // 
            // lblUkupno
            // 
            this.lblUkupno.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblUkupno.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblUkupno.Location = new System.Drawing.Point(779, 91);
            this.lblUkupno.Name = "lblUkupno";
            this.lblUkupno.Size = new System.Drawing.Size(204, 30);
            this.lblUkupno.TabIndex = 3;
            this.lblUkupno.Text = "label1";
            this.lblUkupno.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lblAdresa
            // 
            this.lblAdresa.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblAdresa.AutoSize = true;
            this.lblAdresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblAdresa.Location = new System.Drawing.Point(113, 35);
            this.lblAdresa.Name = "lblAdresa";
            this.lblAdresa.Size = new System.Drawing.Size(70, 26);
            this.lblAdresa.TabIndex = 2;
            this.lblAdresa.Text = "label1";
            // 
            // lblNaziv
            // 
            this.lblNaziv.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblNaziv.AutoSize = true;
            this.lblNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblNaziv.Location = new System.Drawing.Point(113, 9);
            this.lblNaziv.Name = "lblNaziv";
            this.lblNaziv.Size = new System.Drawing.Size(70, 26);
            this.lblNaziv.TabIndex = 1;
            this.lblNaziv.Text = "label1";
            // 
            // pnlFill
            // 
            this.pnlFill.BackColor = System.Drawing.Color.Transparent;
            this.pnlFill.Controls.Add(this.dgvData);
            this.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFill.Location = new System.Drawing.Point(0, 125);
            this.pnlFill.Name = "pnlFill";
            this.pnlFill.Size = new System.Drawing.Size(995, 569);
            this.pnlFill.TabIndex = 2;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToResizeColumns = false;
            this.dgvData.AllowUserToResizeRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvData.BackgroundColor = System.Drawing.Color.LightSlateGray;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.GridColor = System.Drawing.SystemColors.Control;
            this.dgvData.Location = new System.Drawing.Point(12, 7);
            this.dgvData.Name = "dgvData";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.Size = new System.Drawing.Size(971, 550);
            this.dgvData.TabIndex = 0;
            // 
            // btnResetKartice
            // 
            this.btnResetKartice.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnResetKartice.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnResetKartice.BackgroundImage")));
            this.btnResetKartice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnResetKartice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResetKartice.FlatAppearance.BorderSize = 0;
            this.btnResetKartice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnResetKartice.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnResetKartice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetKartice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnResetKartice.Location = new System.Drawing.Point(632, 3);
            this.btnResetKartice.Name = "btnResetKartice";
            this.btnResetKartice.Size = new System.Drawing.Size(161, 46);
            this.btnResetKartice.TabIndex = 135;
            this.btnResetKartice.Text = "Reset kartice";
            this.btnResetKartice.UseVisualStyleBackColor = true;
            this.btnResetKartice.Click += new System.EventHandler(this.btnResetKartice_Click);
            // 
            // frmKarticaKupca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(995, 694);
            this.Controls.Add(this.pnlFill);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmKarticaKupca";
            this.Text = "frmKarticaKupca";
            this.Load += new System.EventHandler(this.frmKarticaKupca_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmTimer1;
        private System.Windows.Forms.TextBox txtKarticaKupca;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlFill;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Label lblNaziv;
        private System.Windows.Forms.Label lblAdresa;
        private System.Windows.Forms.Label lblUkupno;
        private System.Windows.Forms.Label lblKarticaKupca;
        private System.Windows.Forms.DateTimePicker dtpDo;
        private System.Windows.Forms.DateTimePicker dtpOd;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDodajRacune;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnResetKartice;
    }
}