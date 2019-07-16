namespace PCPOS.Resort
{
    partial class FrmAgencije
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.labelAgencije = new System.Windows.Forms.Label();
            this.buttonDodaj = new System.Windows.Forms.Button();
            this.buttonUredi = new System.Windows.Forms.Button();
            this.buttonIzbrisi = new System.Windows.Forms.Button();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.broj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imeAgencije = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.napomena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aktivnost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelBroj = new System.Windows.Forms.Label();
            this.labelImeAgencije = new System.Windows.Forms.Label();
            this.labelAktivnost = new System.Windows.Forms.Label();
            this.labelNapomena = new System.Windows.Forms.Label();
            this.textBoxBroj = new System.Windows.Forms.TextBox();
            this.textBoxImeAgencije = new System.Windows.Forms.TextBox();
            this.richTextBoxNapomena = new System.Windows.Forms.RichTextBox();
            this.comboBoxAktivnost = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.broj,
            this.imeAgencije,
            this.napomena,
            this.aktivnost});
            this.dataGridView.Location = new System.Drawing.Point(5, 218);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(540, 261);
            this.dataGridView.TabIndex = 0;
            // 
            // labelAgencije
            // 
            this.labelAgencije.AutoSize = true;
            this.labelAgencije.BackColor = System.Drawing.Color.Transparent;
            this.labelAgencije.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelAgencije.Location = new System.Drawing.Point(243, 9);
            this.labelAgencije.Name = "labelAgencije";
            this.labelAgencije.Size = new System.Drawing.Size(116, 29);
            this.labelAgencije.TabIndex = 6;
            this.labelAgencije.Text = "Agencije";
            // 
            // buttonDodaj
            // 
            this.buttonDodaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDodaj.Location = new System.Drawing.Point(342, 157);
            this.buttonDodaj.Name = "buttonDodaj";
            this.buttonDodaj.Size = new System.Drawing.Size(203, 37);
            this.buttonDodaj.TabIndex = 7;
            this.buttonDodaj.Text = "Dodaj";
            this.buttonDodaj.UseVisualStyleBackColor = true;
            this.buttonDodaj.Click += new System.EventHandler(this.buttonDodaj_Click);
            // 
            // buttonUredi
            // 
            this.buttonUredi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUredi.Location = new System.Drawing.Point(95, 485);
            this.buttonUredi.Name = "buttonUredi";
            this.buttonUredi.Size = new System.Drawing.Size(172, 37);
            this.buttonUredi.TabIndex = 8;
            this.buttonUredi.Text = "Uredi";
            this.buttonUredi.UseVisualStyleBackColor = true;
            this.buttonUredi.Click += new System.EventHandler(this.buttonUredi_Click);
            // 
            // buttonIzbrisi
            // 
            this.buttonIzbrisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonIzbrisi.Location = new System.Drawing.Point(283, 485);
            this.buttonIzbrisi.Name = "buttonIzbrisi";
            this.buttonIzbrisi.Size = new System.Drawing.Size(172, 37);
            this.buttonIzbrisi.TabIndex = 9;
            this.buttonIzbrisi.Text = "Izbriši";
            this.buttonIzbrisi.UseVisualStyleBackColor = true;
            this.buttonIzbrisi.Click += new System.EventHandler(this.buttonIzbrisi_Click);
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // broj
            // 
            this.broj.FillWeight = 30F;
            this.broj.HeaderText = "Broj";
            this.broj.Name = "broj";
            // 
            // imeAgencije
            // 
            this.imeAgencije.HeaderText = "Ime agencije";
            this.imeAgencije.Name = "imeAgencije";
            // 
            // napomena
            // 
            this.napomena.HeaderText = "Napomena";
            this.napomena.Name = "napomena";
            // 
            // aktivnost
            // 
            this.aktivnost.HeaderText = "Aktivnost";
            this.aktivnost.Name = "aktivnost";
            // 
            // labelBroj
            // 
            this.labelBroj.AutoSize = true;
            this.labelBroj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBroj.Location = new System.Drawing.Point(81, 63);
            this.labelBroj.Name = "labelBroj";
            this.labelBroj.Size = new System.Drawing.Size(40, 16);
            this.labelBroj.TabIndex = 10;
            this.labelBroj.Text = "Broj:";
            // 
            // labelImeAgencije
            // 
            this.labelImeAgencije.AutoSize = true;
            this.labelImeAgencije.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelImeAgencije.Location = new System.Drawing.Point(23, 104);
            this.labelImeAgencije.Name = "labelImeAgencije";
            this.labelImeAgencije.Size = new System.Drawing.Size(101, 16);
            this.labelImeAgencije.TabIndex = 11;
            this.labelImeAgencije.Text = "Ime agencije:";
            // 
            // labelAktivnost
            // 
            this.labelAktivnost.AutoSize = true;
            this.labelAktivnost.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAktivnost.Location = new System.Drawing.Point(47, 145);
            this.labelAktivnost.Name = "labelAktivnost";
            this.labelAktivnost.Size = new System.Drawing.Size(75, 16);
            this.labelAktivnost.TabIndex = 12;
            this.labelAktivnost.Text = "Aktivnost:";
            // 
            // labelNapomena
            // 
            this.labelNapomena.AutoSize = true;
            this.labelNapomena.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNapomena.Location = new System.Drawing.Point(339, 62);
            this.labelNapomena.Name = "labelNapomena";
            this.labelNapomena.Size = new System.Drawing.Size(88, 16);
            this.labelNapomena.TabIndex = 13;
            this.labelNapomena.Text = "Napomena:";
            // 
            // textBoxBroj
            // 
            this.textBoxBroj.Location = new System.Drawing.Point(123, 62);
            this.textBoxBroj.Name = "textBoxBroj";
            this.textBoxBroj.ReadOnly = true;
            this.textBoxBroj.Size = new System.Drawing.Size(176, 20);
            this.textBoxBroj.TabIndex = 14;
            // 
            // textBoxImeAgencije
            // 
            this.textBoxImeAgencije.Location = new System.Drawing.Point(123, 103);
            this.textBoxImeAgencije.Name = "textBoxImeAgencije";
            this.textBoxImeAgencije.Size = new System.Drawing.Size(176, 20);
            this.textBoxImeAgencije.TabIndex = 15;
            // 
            // richTextBoxNapomena
            // 
            this.richTextBoxNapomena.Location = new System.Drawing.Point(342, 81);
            this.richTextBoxNapomena.Name = "richTextBoxNapomena";
            this.richTextBoxNapomena.Size = new System.Drawing.Size(203, 68);
            this.richTextBoxNapomena.TabIndex = 16;
            this.richTextBoxNapomena.Text = "";
            // 
            // comboBoxAktivnost
            // 
            this.comboBoxAktivnost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAktivnost.FormattingEnabled = true;
            this.comboBoxAktivnost.Location = new System.Drawing.Point(123, 143);
            this.comboBoxAktivnost.Name = "comboBoxAktivnost";
            this.comboBoxAktivnost.Size = new System.Drawing.Size(176, 21);
            this.comboBoxAktivnost.TabIndex = 17;
            // 
            // FrmAgencije
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(553, 533);
            this.Controls.Add(this.comboBoxAktivnost);
            this.Controls.Add(this.richTextBoxNapomena);
            this.Controls.Add(this.textBoxImeAgencije);
            this.Controls.Add(this.textBoxBroj);
            this.Controls.Add(this.labelNapomena);
            this.Controls.Add(this.labelAktivnost);
            this.Controls.Add(this.labelImeAgencije);
            this.Controls.Add(this.labelBroj);
            this.Controls.Add(this.buttonIzbrisi);
            this.Controls.Add(this.buttonUredi);
            this.Controls.Add(this.buttonDodaj);
            this.Controls.Add(this.labelAgencije);
            this.Controls.Add(this.dataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmAgencije";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agencije";
            this.Load += new System.EventHandler(this.FrmAgencije_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label labelAgencije;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj;
        private System.Windows.Forms.DataGridViewTextBoxColumn imeAgencije;
        private System.Windows.Forms.DataGridViewTextBoxColumn napomena;
        private System.Windows.Forms.DataGridViewTextBoxColumn aktivnost;
        private System.Windows.Forms.Button buttonDodaj;
        private System.Windows.Forms.Button buttonUredi;
        private System.Windows.Forms.Button buttonIzbrisi;
        private System.Windows.Forms.Label labelBroj;
        private System.Windows.Forms.Label labelImeAgencije;
        private System.Windows.Forms.Label labelAktivnost;
        private System.Windows.Forms.Label labelNapomena;
        private System.Windows.Forms.TextBox textBoxBroj;
        private System.Windows.Forms.TextBox textBoxImeAgencije;
        private System.Windows.Forms.RichTextBox richTextBoxNapomena;
        private System.Windows.Forms.ComboBox comboBoxAktivnost;
    }
}