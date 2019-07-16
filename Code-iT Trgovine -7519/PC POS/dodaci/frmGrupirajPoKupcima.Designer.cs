namespace PCPOS.dodaci
{
    partial class frmGrupirajPoKupcima
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvKupac = new System.Windows.Forms.DataGridView();
            this.ime_kupca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.sifra_artikla = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.popust = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skladiste = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Iznos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oduzmi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.porez = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nbc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jmj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cijena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnObrisiStavku = new System.Windows.Forms.Button();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.btnZavrsiRacun = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKupac)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvKupac
            // 
            this.dgvKupac.AllowUserToAddRows = false;
            this.dgvKupac.AllowUserToDeleteRows = false;
            this.dgvKupac.BackgroundColor = System.Drawing.Color.White;
            this.dgvKupac.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKupac.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ime_kupca,
            this.id});
            this.dgvKupac.Location = new System.Drawing.Point(12, 12);
            this.dgvKupac.Name = "dgvKupac";
            this.dgvKupac.ReadOnly = true;
            this.dgvKupac.RowHeadersVisible = false;
            this.dgvKupac.Size = new System.Drawing.Size(240, 504);
            this.dgvKupac.TabIndex = 0;
            this.dgvKupac.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKupac_CellClick);
            // 
            // ime_kupca
            // 
            this.ime_kupca.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ime_kupca.HeaderText = "Ime kupca";
            this.ime_kupca.Name = "ime_kupca";
            this.ime_kupca.ReadOnly = true;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sifra_artikla,
            this.naziv,
            this.kolicina,
            this.popust,
            this.skladiste,
            this.Iznos,
            this.vpc,
            this.oduzmi,
            this.porez,
            this.nbc,
            this.jmj,
            this.cijena});
            this.dgv.Location = new System.Drawing.Point(267, 12);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(497, 504);
            this.dgv.TabIndex = 1;
            // 
            // sifra_artikla
            // 
            this.sifra_artikla.FillWeight = 85F;
            this.sifra_artikla.HeaderText = "Šifra artikla";
            this.sifra_artikla.Name = "sifra_artikla";
            this.sifra_artikla.ReadOnly = true;
            this.sifra_artikla.Width = 85;
            // 
            // naziv
            // 
            this.naziv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.naziv.HeaderText = "Naziv";
            this.naziv.Name = "naziv";
            this.naziv.ReadOnly = true;
            // 
            // kolicina
            // 
            this.kolicina.FillWeight = 40F;
            this.kolicina.HeaderText = "KOL";
            this.kolicina.Name = "kolicina";
            this.kolicina.ReadOnly = true;
            this.kolicina.Width = 40;
            // 
            // popust
            // 
            this.popust.FillWeight = 60F;
            this.popust.HeaderText = "Popust";
            this.popust.Name = "popust";
            this.popust.ReadOnly = true;
            this.popust.Width = 60;
            // 
            // skladiste
            // 
            this.skladiste.FillWeight = 70F;
            this.skladiste.HeaderText = "Skladište";
            this.skladiste.Name = "skladiste";
            this.skladiste.ReadOnly = true;
            this.skladiste.Width = 70;
            // 
            // Iznos
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Iznos.DefaultCellStyle = dataGridViewCellStyle5;
            this.Iznos.HeaderText = "Iznos";
            this.Iznos.Name = "Iznos";
            this.Iznos.ReadOnly = true;
            // 
            // vpc
            // 
            this.vpc.HeaderText = "vpc";
            this.vpc.Name = "vpc";
            this.vpc.ReadOnly = true;
            this.vpc.Visible = false;
            // 
            // oduzmi
            // 
            this.oduzmi.HeaderText = "oduzmi";
            this.oduzmi.Name = "oduzmi";
            this.oduzmi.ReadOnly = true;
            this.oduzmi.Visible = false;
            // 
            // porez
            // 
            this.porez.HeaderText = "porez";
            this.porez.Name = "porez";
            this.porez.ReadOnly = true;
            this.porez.Visible = false;
            // 
            // nbc
            // 
            this.nbc.HeaderText = "nbc";
            this.nbc.Name = "nbc";
            this.nbc.ReadOnly = true;
            this.nbc.Visible = false;
            // 
            // jmj
            // 
            this.jmj.HeaderText = "jmj";
            this.jmj.Name = "jmj";
            this.jmj.ReadOnly = true;
            this.jmj.Visible = false;
            // 
            // cijena
            // 
            this.cijena.HeaderText = "cijena";
            this.cijena.Name = "cijena";
            this.cijena.ReadOnly = true;
            this.cijena.Visible = false;
            // 
            // btnObrisiStavku
            // 
            this.btnObrisiStavku.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnObrisiStavku.BackColor = System.Drawing.Color.Transparent;
            this.btnObrisiStavku.BackgroundImage = global::PCPOS.Properties.Resources.dff;
            this.btnObrisiStavku.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnObrisiStavku.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnObrisiStavku.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnObrisiStavku.FlatAppearance.BorderSize = 0;
            this.btnObrisiStavku.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnObrisiStavku.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnObrisiStavku.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnObrisiStavku.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnObrisiStavku.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold);
            this.btnObrisiStavku.ForeColor = System.Drawing.Color.Red;
            this.btnObrisiStavku.Location = new System.Drawing.Point(267, 521);
            this.btnObrisiStavku.Name = "btnObrisiStavku";
            this.btnObrisiStavku.Size = new System.Drawing.Size(155, 41);
            this.btnObrisiStavku.TabIndex = 77;
            this.btnObrisiStavku.Text = "Obriši stavku";
            this.btnObrisiStavku.UseVisualStyleBackColor = false;
            this.btnObrisiStavku.Click += new System.EventHandler(this.btnObrisiStavku_Click);
            // 
            // btnObrisi
            // 
            this.btnObrisi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnObrisi.BackColor = System.Drawing.Color.Transparent;
            this.btnObrisi.BackgroundImage = global::PCPOS.Properties.Resources.dff;
            this.btnObrisi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnObrisi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnObrisi.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnObrisi.FlatAppearance.BorderSize = 0;
            this.btnObrisi.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnObrisi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnObrisi.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnObrisi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnObrisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold);
            this.btnObrisi.ForeColor = System.Drawing.Color.Red;
            this.btnObrisi.Location = new System.Drawing.Point(12, 521);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(240, 41);
            this.btnObrisi.TabIndex = 76;
            this.btnObrisi.Text = "Obriši kupca";
            this.btnObrisi.UseVisualStyleBackColor = false;
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // btnOdustani
            // 
            this.btnOdustani.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOdustani.BackColor = System.Drawing.Color.Transparent;
            this.btnOdustani.BackgroundImage = global::PCPOS.Properties.Resources.dff;
            this.btnOdustani.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOdustani.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOdustani.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnOdustani.FlatAppearance.BorderSize = 0;
            this.btnOdustani.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnOdustani.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnOdustani.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnOdustani.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOdustani.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold);
            this.btnOdustani.ForeColor = System.Drawing.Color.White;
            this.btnOdustani.Location = new System.Drawing.Point(428, 521);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(169, 41);
            this.btnOdustani.TabIndex = 75;
            this.btnOdustani.Text = "Odustani";
            this.btnOdustani.UseVisualStyleBackColor = false;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            // 
            // btnZavrsiRacun
            // 
            this.btnZavrsiRacun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZavrsiRacun.BackColor = System.Drawing.Color.Transparent;
            this.btnZavrsiRacun.BackgroundImage = global::PCPOS.Properties.Resources.dff;
            this.btnZavrsiRacun.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnZavrsiRacun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnZavrsiRacun.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnZavrsiRacun.FlatAppearance.BorderSize = 0;
            this.btnZavrsiRacun.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnZavrsiRacun.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnZavrsiRacun.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnZavrsiRacun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZavrsiRacun.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold);
            this.btnZavrsiRacun.ForeColor = System.Drawing.Color.White;
            this.btnZavrsiRacun.Location = new System.Drawing.Point(599, 521);
            this.btnZavrsiRacun.Name = "btnZavrsiRacun";
            this.btnZavrsiRacun.Size = new System.Drawing.Size(169, 41);
            this.btnZavrsiRacun.TabIndex = 74;
            this.btnZavrsiRacun.Text = "Završi račun";
            this.btnZavrsiRacun.UseVisualStyleBackColor = false;
            this.btnZavrsiRacun.Click += new System.EventHandler(this.btnZavrsiRacun_Click);
            // 
            // frmGrupirajPoKupcima
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(776, 567);
            this.Controls.Add(this.btnObrisiStavku);
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnZavrsiRacun);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.dgvKupac);
            this.Name = "frmGrupirajPoKupcima";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Završi račun";
            this.Load += new System.EventHandler(this.frmGrupirajPoKupcima_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKupac)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvKupac;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnZavrsiRacun;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.DataGridViewTextBoxColumn ime_kupca;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.Button btnObrisi;
        private System.Windows.Forms.Button btnObrisiStavku;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra_artikla;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolicina;
        private System.Windows.Forms.DataGridViewTextBoxColumn popust;
        private System.Windows.Forms.DataGridViewTextBoxColumn skladiste;
        private System.Windows.Forms.DataGridViewTextBoxColumn Iznos;
        private System.Windows.Forms.DataGridViewTextBoxColumn vpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn oduzmi;
        private System.Windows.Forms.DataGridViewTextBoxColumn porez;
        private System.Windows.Forms.DataGridViewTextBoxColumn nbc;
        private System.Windows.Forms.DataGridViewTextBoxColumn jmj;
        private System.Windows.Forms.DataGridViewTextBoxColumn cijena;
    }
}