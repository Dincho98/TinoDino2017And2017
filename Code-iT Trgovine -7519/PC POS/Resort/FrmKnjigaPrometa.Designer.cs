namespace PCPOS.Resort
{
    partial class FrmKnjigaPrometa
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.broj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.brojRacuna = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nacinPlacanja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateTimePickerDatumPocetka = new System.Windows.Forms.DateTimePicker();
            this.labelPocetniDatum = new System.Windows.Forms.Label();
            this.labelKrajnjiDatum = new System.Windows.Forms.Label();
            this.dateTimePickerDatumKraja = new System.Windows.Forms.DateTimePicker();
            this.buttonTrazi = new System.Windows.Forms.Button();
            this.buttonSve = new System.Windows.Forms.Button();
            this.buttonIspisi = new System.Windows.Forms.Button();
            this.labelKnjigaPrometa = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.broj,
            this.brojRacuna,
            this.datum,
            this.iznos,
            this.nacinPlacanja});
            this.dataGridView1.Location = new System.Drawing.Point(14, 91);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(767, 326);
            this.dataGridView1.TabIndex = 0;
            // 
            // broj
            // 
            this.broj.HeaderText = "Redni broj";
            this.broj.Name = "broj";
            this.broj.ReadOnly = true;
            // 
            // brojRacuna
            // 
            this.brojRacuna.HeaderText = "Broj računa";
            this.brojRacuna.Name = "brojRacuna";
            this.brojRacuna.ReadOnly = true;
            // 
            // datum
            // 
            this.datum.HeaderText = "Datum";
            this.datum.Name = "datum";
            this.datum.ReadOnly = true;
            // 
            // iznos
            // 
            this.iznos.HeaderText = "Iznos";
            this.iznos.Name = "iznos";
            this.iznos.ReadOnly = true;
            // 
            // nacinPlacanja
            // 
            this.nacinPlacanja.HeaderText = "Način plačanja";
            this.nacinPlacanja.Name = "nacinPlacanja";
            this.nacinPlacanja.ReadOnly = true;
            // 
            // dateTimePickerDatumPocetka
            // 
            this.dateTimePickerDatumPocetka.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDatumPocetka.Location = new System.Drawing.Point(127, 56);
            this.dateTimePickerDatumPocetka.Name = "dateTimePickerDatumPocetka";
            this.dateTimePickerDatumPocetka.Size = new System.Drawing.Size(152, 20);
            this.dateTimePickerDatumPocetka.TabIndex = 1;
            // 
            // labelPocetniDatum
            // 
            this.labelPocetniDatum.AutoSize = true;
            this.labelPocetniDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPocetniDatum.Location = new System.Drawing.Point(15, 58);
            this.labelPocetniDatum.Name = "labelPocetniDatum";
            this.labelPocetniDatum.Size = new System.Drawing.Size(110, 16);
            this.labelPocetniDatum.TabIndex = 2;
            this.labelPocetniDatum.Text = "Početni datum:";
            // 
            // labelKrajnjiDatum
            // 
            this.labelKrajnjiDatum.AutoSize = true;
            this.labelKrajnjiDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelKrajnjiDatum.Location = new System.Drawing.Point(291, 58);
            this.labelKrajnjiDatum.Name = "labelKrajnjiDatum";
            this.labelKrajnjiDatum.Size = new System.Drawing.Size(101, 16);
            this.labelKrajnjiDatum.TabIndex = 4;
            this.labelKrajnjiDatum.Text = "Krajnji datum:";
            // 
            // dateTimePickerDatumKraja
            // 
            this.dateTimePickerDatumKraja.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDatumKraja.Location = new System.Drawing.Point(396, 56);
            this.dateTimePickerDatumKraja.Name = "dateTimePickerDatumKraja";
            this.dateTimePickerDatumKraja.Size = new System.Drawing.Size(152, 20);
            this.dateTimePickerDatumKraja.TabIndex = 3;
            // 
            // buttonTrazi
            // 
            this.buttonTrazi.Location = new System.Drawing.Point(679, 54);
            this.buttonTrazi.Name = "buttonTrazi";
            this.buttonTrazi.Size = new System.Drawing.Size(102, 25);
            this.buttonTrazi.TabIndex = 5;
            this.buttonTrazi.Text = "Traži";
            this.buttonTrazi.UseVisualStyleBackColor = true;
            this.buttonTrazi.Click += new System.EventHandler(this.buttonTrazi_Click);
            // 
            // buttonSve
            // 
            this.buttonSve.Location = new System.Drawing.Point(571, 55);
            this.buttonSve.Name = "buttonSve";
            this.buttonSve.Size = new System.Drawing.Size(102, 23);
            this.buttonSve.TabIndex = 6;
            this.buttonSve.Text = "Svi zapisi";
            this.buttonSve.UseVisualStyleBackColor = true;
            this.buttonSve.Click += new System.EventHandler(this.buttonSve_Click);
            // 
            // buttonIspisi
            // 
            this.buttonIspisi.Location = new System.Drawing.Point(294, 423);
            this.buttonIspisi.Name = "buttonIspisi";
            this.buttonIspisi.Size = new System.Drawing.Size(177, 33);
            this.buttonIspisi.TabIndex = 8;
            this.buttonIspisi.Text = "Ispiši";
            this.buttonIspisi.UseVisualStyleBackColor = true;
            this.buttonIspisi.Click += new System.EventHandler(this.buttonIspisi_Click);
            // 
            // labelKnjigaPrometa
            // 
            this.labelKnjigaPrometa.AutoSize = true;
            this.labelKnjigaPrometa.BackColor = System.Drawing.Color.Transparent;
            this.labelKnjigaPrometa.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelKnjigaPrometa.Location = new System.Drawing.Point(289, 9);
            this.labelKnjigaPrometa.Name = "labelKnjigaPrometa";
            this.labelKnjigaPrometa.Size = new System.Drawing.Size(198, 29);
            this.labelKnjigaPrometa.TabIndex = 9;
            this.labelKnjigaPrometa.Text = "Knjiga prometa";
            // 
            // FrmKnjigaPrometa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 462);
            this.Controls.Add(this.labelKnjigaPrometa);
            this.Controls.Add(this.buttonIspisi);
            this.Controls.Add(this.buttonSve);
            this.Controls.Add(this.buttonTrazi);
            this.Controls.Add(this.labelKrajnjiDatum);
            this.Controls.Add(this.dateTimePickerDatumKraja);
            this.Controls.Add(this.labelPocetniDatum);
            this.Controls.Add(this.dateTimePickerDatumPocetka);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmKnjigaPrometa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Knjiga prometa";
            this.Load += new System.EventHandler(this.FrmKnjigaPrometa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker dateTimePickerDatumPocetka;
        private System.Windows.Forms.Label labelPocetniDatum;
        private System.Windows.Forms.Label labelKrajnjiDatum;
        private System.Windows.Forms.DateTimePicker dateTimePickerDatumKraja;
        private System.Windows.Forms.Button buttonTrazi;
        private System.Windows.Forms.Button buttonSve;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj;
        private System.Windows.Forms.DataGridViewTextBoxColumn brojRacuna;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos;
        private System.Windows.Forms.DataGridViewTextBoxColumn nacinPlacanja;
        private System.Windows.Forms.Button buttonIspisi;
        private System.Windows.Forms.Label labelKnjigaPrometa;
    }
}