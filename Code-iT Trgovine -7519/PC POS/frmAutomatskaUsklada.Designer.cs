namespace PCPOS {
    partial class frmAutomatskaUsklada {
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgwUskladaItems = new System.Windows.Forms.DataGridView();
            this.cmbSkladiste = new System.Windows.Forms.ComboBox();
            this.btnUcitaj = new System.Windows.Forms.Button();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dtpDatumDo = new System.Windows.Forms.DateTimePicker();
            this.bgwAutomatskaUskladaUcitaj = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.bgwAutomatskaUskladaSpremi = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.dgwUskladaItems)).BeginInit();
            this.SuspendLayout();
            // 
            // dgwUskladaItems
            // 
            this.dgwUskladaItems.AllowUserToAddRows = false;
            this.dgwUskladaItems.AllowUserToDeleteRows = false;
            this.dgwUskladaItems.AllowUserToResizeRows = false;
            this.dgwUskladaItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgwUskladaItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgwUskladaItems.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgwUskladaItems.BackgroundColor = System.Drawing.Color.SlateGray;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgwUskladaItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dgwUskladaItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgwUskladaItems.DefaultCellStyle = dataGridViewCellStyle17;
            this.dgwUskladaItems.Location = new System.Drawing.Point(12, 88);
            this.dgwUskladaItems.Name = "dgwUskladaItems";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgwUskladaItems.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dgwUskladaItems.Size = new System.Drawing.Size(785, 363);
            this.dgwUskladaItems.TabIndex = 0;
            // 
            // cmbSkladiste
            // 
            this.cmbSkladiste.BackColor = System.Drawing.SystemColors.Menu;
            this.cmbSkladiste.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSkladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSkladiste.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSkladiste.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.cmbSkladiste.FormattingEnabled = true;
            this.cmbSkladiste.Location = new System.Drawing.Point(12, 50);
            this.cmbSkladiste.Name = "cmbSkladiste";
            this.cmbSkladiste.Size = new System.Drawing.Size(220, 30);
            this.cmbSkladiste.TabIndex = 1;
            this.cmbSkladiste.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbSkladiste_DrawItem);
            // 
            // btnUcitaj
            // 
            this.btnUcitaj.BackColor = System.Drawing.SystemColors.Control;
            this.btnUcitaj.FlatAppearance.BorderSize = 0;
            this.btnUcitaj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUcitaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnUcitaj.Location = new System.Drawing.Point(12, 12);
            this.btnUcitaj.Name = "btnUcitaj";
            this.btnUcitaj.Size = new System.Drawing.Size(121, 32);
            this.btnUcitaj.TabIndex = 2;
            this.btnUcitaj.Text = "Učitaj";
            this.btnUcitaj.UseVisualStyleBackColor = false;
            this.btnUcitaj.Click += new System.EventHandler(this.btnUcitaj_Click);
            // 
            // btnSpremi
            // 
            this.btnSpremi.BackColor = System.Drawing.SystemColors.Control;
            this.btnSpremi.FlatAppearance.BorderSize = 0;
            this.btnSpremi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpremi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSpremi.Location = new System.Drawing.Point(139, 12);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(121, 32);
            this.btnSpremi.TabIndex = 3;
            this.btnSpremi.Text = "Spremi";
            this.btnSpremi.UseVisualStyleBackColor = false;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // btnOdustani
            // 
            this.btnOdustani.BackColor = System.Drawing.SystemColors.Control;
            this.btnOdustani.FlatAppearance.BorderSize = 0;
            this.btnOdustani.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOdustani.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnOdustani.Location = new System.Drawing.Point(266, 12);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(121, 32);
            this.btnOdustani.TabIndex = 4;
            this.btnOdustani.Text = "Odustani";
            this.btnOdustani.UseVisualStyleBackColor = false;
            this.btnOdustani.Visible = false;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            // 
            // btnObrisi
            // 
            this.btnObrisi.BackColor = System.Drawing.SystemColors.Control;
            this.btnObrisi.FlatAppearance.BorderSize = 0;
            this.btnObrisi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnObrisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnObrisi.Location = new System.Drawing.Point(393, 12);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(121, 32);
            this.btnObrisi.TabIndex = 5;
            this.btnObrisi.Text = "Obriši";
            this.btnObrisi.UseVisualStyleBackColor = false;
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.Location = new System.Drawing.Point(344, 457);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 32);
            this.button1.TabIndex = 6;
            this.button1.Text = "Obriši";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            // 
            // dtpDatumDo
            // 
            this.dtpDatumDo.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dtpDatumDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.15F);
            this.dtpDatumDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatumDo.Location = new System.Drawing.Point(238, 50);
            this.dtpDatumDo.Name = "dtpDatumDo";
            this.dtpDatumDo.Size = new System.Drawing.Size(276, 30);
            this.dtpDatumDo.TabIndex = 8;
            // 
            // bgwAutomatskaUskladaUcitaj
            // 
            this.bgwAutomatskaUskladaUcitaj.WorkerReportsProgress = true;
            this.bgwAutomatskaUskladaUcitaj.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwAutomatskaUskladaUcitaj_DoWork);
            this.bgwAutomatskaUskladaUcitaj.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwAutomatskaUskladaUcitaj_RunWorkerCompleted);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(13, 458);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 9;
            this.progressBar1.Visible = false;
            // 
            // bgwAutomatskaUskladaSpremi
            // 
            this.bgwAutomatskaUskladaSpremi.WorkerReportsProgress = true;
            this.bgwAutomatskaUskladaSpremi.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwAutomatskaUskladaSpremi_DoWork);
            // 
            // frmAutomatskaUsklada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(809, 501);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.dtpDatumDo);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.btnUcitaj);
            this.Controls.Add(this.cmbSkladiste);
            this.Controls.Add(this.dgwUskladaItems);
            this.MinimumSize = new System.Drawing.Size(825, 539);
            this.Name = "frmAutomatskaUsklada";
            this.Text = "Automatska usklada";
            this.Load += new System.EventHandler(this.frmAutomatskaUsklada_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwUskladaItems)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwUskladaItems;
        private System.Windows.Forms.ComboBox cmbSkladiste;
        private System.Windows.Forms.Button btnUcitaj;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Button btnObrisi;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dtpDatumDo;
        private System.ComponentModel.BackgroundWorker bgwAutomatskaUskladaUcitaj;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker bgwAutomatskaUskladaSpremi;
    }
}