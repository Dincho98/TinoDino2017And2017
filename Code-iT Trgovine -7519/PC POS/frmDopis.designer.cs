namespace PCPOS
{
    partial class frmDopis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDopis));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnPartner = new System.Windows.Forms.Button();
            this.lblIzradio = new System.Windows.Forms.Label();
            this.dtpDatum = new System.Windows.Forms.DateTimePicker();
            this.lblDatum = new System.Windows.Forms.Label();
            this.lblPartner = new System.Windows.Forms.Label();
            this.txtIzradio = new System.Windows.Forms.TextBox();
            this.txtPartnerNaziv = new System.Windows.Forms.TextBox();
            this.txtPartnerSifra = new System.Windows.Forms.TextBox();
            this.txtText = new System.Windows.Forms.TextBox();
            this.lblText = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblBrojDopisa = new System.Windows.Forms.Label();
            this.txtBrojDopisa = new System.Windows.Forms.TextBox();
            this.nmGodinaDopisa = new System.Windows.Forms.NumericUpDown();
            this.br = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skladiste = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.jmj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.porez = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.porezZaIzracun = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rabat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rabat_iznos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cijena_bez_pdva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos_bez_pdva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos_ukupno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_stavka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_roba_prodaja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oduzmi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.porez_potrosnja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblNaDan = new System.Windows.Forms.Label();
            this.btnIzlaz = new System.Windows.Forms.Button();
            this.btnDeleteAllDopis = new System.Windows.Forms.Button();
            this.btnSviDopisi = new System.Windows.Forms.Button();
            this.btnNoviUnos = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmGodinaDopisa)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.btnPartner);
            this.groupBox2.Controls.Add(this.lblIzradio);
            this.groupBox2.Controls.Add(this.dtpDatum);
            this.groupBox2.Controls.Add(this.lblDatum);
            this.groupBox2.Controls.Add(this.lblPartner);
            this.groupBox2.Controls.Add(this.txtIzradio);
            this.groupBox2.Controls.Add(this.txtPartnerNaziv);
            this.groupBox2.Controls.Add(this.txtPartnerSifra);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox2.Location = new System.Drawing.Point(12, 113);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(991, 120);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // btnPartner
            // 
            this.btnPartner.Location = new System.Drawing.Point(167, 21);
            this.btnPartner.Name = "btnPartner";
            this.btnPartner.Size = new System.Drawing.Size(28, 26);
            this.btnPartner.TabIndex = 2;
            this.btnPartner.Text = "...";
            this.btnPartner.UseVisualStyleBackColor = true;
            this.btnPartner.Click += new System.EventHandler(this.btnPartner_Click);
            // 
            // lblIzradio
            // 
            this.lblIzradio.AutoSize = true;
            this.lblIzradio.BackColor = System.Drawing.Color.Transparent;
            this.lblIzradio.Location = new System.Drawing.Point(13, 84);
            this.lblIzradio.Name = "lblIzradio";
            this.lblIzradio.Size = new System.Drawing.Size(54, 17);
            this.lblIzradio.TabIndex = 20;
            this.lblIzradio.Text = "Izradio:";
            // 
            // dtpDatum
            // 
            this.dtpDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatum.Location = new System.Drawing.Point(114, 52);
            this.dtpDatum.Name = "dtpDatum";
            this.dtpDatum.Size = new System.Drawing.Size(208, 23);
            this.dtpDatum.TabIndex = 11;
            this.dtpDatum.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.dtpDatum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDatum_KeyDown);
            this.dtpDatum.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // lblDatum
            // 
            this.lblDatum.AutoSize = true;
            this.lblDatum.BackColor = System.Drawing.Color.Transparent;
            this.lblDatum.Location = new System.Drawing.Point(13, 55);
            this.lblDatum.Name = "lblDatum";
            this.lblDatum.Size = new System.Drawing.Size(53, 17);
            this.lblDatum.TabIndex = 10;
            this.lblDatum.Text = "Datum:";
            // 
            // lblPartner
            // 
            this.lblPartner.AutoSize = true;
            this.lblPartner.BackColor = System.Drawing.Color.Transparent;
            this.lblPartner.Location = new System.Drawing.Point(13, 26);
            this.lblPartner.Name = "lblPartner";
            this.lblPartner.Size = new System.Drawing.Size(55, 17);
            this.lblPartner.TabIndex = 0;
            this.lblPartner.Text = "Partner";
            // 
            // txtIzradio
            // 
            this.txtIzradio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtIzradio.Location = new System.Drawing.Point(114, 81);
            this.txtIzradio.Name = "txtIzradio";
            this.txtIzradio.ReadOnly = true;
            this.txtIzradio.Size = new System.Drawing.Size(208, 23);
            this.txtIzradio.TabIndex = 21;
            // 
            // txtPartnerNaziv
            // 
            this.txtPartnerNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtPartnerNaziv.Location = new System.Drawing.Point(195, 23);
            this.txtPartnerNaziv.Name = "txtPartnerNaziv";
            this.txtPartnerNaziv.ReadOnly = true;
            this.txtPartnerNaziv.Size = new System.Drawing.Size(127, 23);
            this.txtPartnerNaziv.TabIndex = 3;
            // 
            // txtPartnerSifra
            // 
            this.txtPartnerSifra.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtPartnerSifra.Location = new System.Drawing.Point(114, 22);
            this.txtPartnerSifra.Name = "txtPartnerSifra";
            this.txtPartnerSifra.Size = new System.Drawing.Size(53, 24);
            this.txtPartnerSifra.TabIndex = 1;
            this.txtPartnerSifra.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtPartnerSifra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraOdrediste_KeyDown);
            this.txtPartnerSifra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtPartnerSifra.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtText
            // 
            this.txtText.Location = new System.Drawing.Point(12, 256);
            this.txtText.Multiline = true;
            this.txtText.Name = "txtText";
            this.txtText.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtText.Size = new System.Drawing.Size(991, 338);
            this.txtText.TabIndex = 34;
            this.txtText.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtText.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.BackColor = System.Drawing.Color.Transparent;
            this.lblText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblText.Location = new System.Drawing.Point(25, 236);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(93, 17);
            this.lblText.TabIndex = 33;
            this.lblText.Text = "Tekst dopisa:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.lblBrojDopisa);
            this.groupBox1.Controls.Add(this.txtBrojDopisa);
            this.groupBox1.Controls.Add(this.nmGodinaDopisa);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox1.Location = new System.Drawing.Point(12, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(991, 49);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // lblBrojDopisa
            // 
            this.lblBrojDopisa.AutoSize = true;
            this.lblBrojDopisa.BackColor = System.Drawing.Color.Transparent;
            this.lblBrojDopisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblBrojDopisa.ForeColor = System.Drawing.Color.Maroon;
            this.lblBrojDopisa.Location = new System.Drawing.Point(13, 20);
            this.lblBrojDopisa.Name = "lblBrojDopisa";
            this.lblBrojDopisa.Size = new System.Drawing.Size(95, 17);
            this.lblBrojDopisa.TabIndex = 0;
            this.lblBrojDopisa.Text = "Broj dopisa:";
            // 
            // txtBrojDopisa
            // 
            this.txtBrojDopisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtBrojDopisa.Location = new System.Drawing.Point(114, 15);
            this.txtBrojDopisa.Name = "txtBrojDopisa";
            this.txtBrojDopisa.Size = new System.Drawing.Size(101, 26);
            this.txtBrojDopisa.TabIndex = 1;
            this.txtBrojDopisa.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtBrojDopisa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojPonude_KeyDown);
            this.txtBrojDopisa.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // nmGodinaDopisa
            // 
            this.nmGodinaDopisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.nmGodinaDopisa.Location = new System.Drawing.Point(221, 15);
            this.nmGodinaDopisa.Name = "nmGodinaDopisa";
            this.nmGodinaDopisa.Size = new System.Drawing.Size(101, 26);
            this.nmGodinaDopisa.TabIndex = 2;
            this.nmGodinaDopisa.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.nmGodinaDopisa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojPonude_KeyDown);
            this.nmGodinaDopisa.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // br
            // 
            this.br.FillWeight = 50F;
            this.br.HeaderText = "Br.";
            this.br.Name = "br";
            // 
            // sifra
            // 
            this.sifra.FillWeight = 61.10954F;
            this.sifra.HeaderText = "Šifra";
            this.sifra.Name = "sifra";
            // 
            // naziv
            // 
            this.naziv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.naziv.FillWeight = 61.10954F;
            this.naziv.HeaderText = "Naziv robe ili usluge";
            this.naziv.MinimumWidth = 130;
            this.naziv.Name = "naziv";
            // 
            // skladiste
            // 
            this.skladiste.DataPropertyName = "sifra";
            this.skladiste.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.skladiste.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.skladiste.HeaderText = "Skladište";
            this.skladiste.Name = "skladiste";
            // 
            // jmj
            // 
            this.jmj.HeaderText = "JMJ";
            this.jmj.Name = "jmj";
            // 
            // kolicina
            // 
            this.kolicina.FillWeight = 61.10954F;
            this.kolicina.HeaderText = "Količina";
            this.kolicina.Name = "kolicina";
            // 
            // porez
            // 
            this.porez.HeaderText = "Porez";
            this.porez.Name = "porez";
            // 
            // porezZaIzracun
            // 
            this.porezZaIzracun.HeaderText = "porezZaIzracun";
            this.porezZaIzracun.Name = "porezZaIzracun";
            this.porezZaIzracun.Visible = false;
            // 
            // mpc
            // 
            this.mpc.HeaderText = "MPC";
            this.mpc.Name = "mpc";
            // 
            // rabat
            // 
            this.rabat.FillWeight = 61.10954F;
            this.rabat.HeaderText = "Rabat%";
            this.rabat.Name = "rabat";
            // 
            // rabat_iznos
            // 
            this.rabat_iznos.HeaderText = "Rabat iznos";
            this.rabat_iznos.Name = "rabat_iznos";
            this.rabat_iznos.ReadOnly = true;
            // 
            // cijena_bez_pdva
            // 
            this.cijena_bez_pdva.FillWeight = 120F;
            this.cijena_bez_pdva.HeaderText = "Cijena bez pdv-a";
            this.cijena_bez_pdva.Name = "cijena_bez_pdva";
            this.cijena_bez_pdva.ReadOnly = true;
            // 
            // iznos_bez_pdva
            // 
            this.iznos_bez_pdva.FillWeight = 120F;
            this.iznos_bez_pdva.HeaderText = "Iznos bez pdv-a";
            this.iznos_bez_pdva.Name = "iznos_bez_pdva";
            this.iznos_bez_pdva.ReadOnly = true;
            // 
            // iznos_ukupno
            // 
            this.iznos_ukupno.HeaderText = "Iznos ukupno";
            this.iznos_ukupno.Name = "iznos_ukupno";
            this.iznos_ukupno.ReadOnly = true;
            // 
            // vpc
            // 
            this.vpc.HeaderText = "VPC";
            this.vpc.Name = "vpc";
            this.vpc.Visible = false;
            // 
            // nc
            // 
            this.nc.HeaderText = "nc";
            this.nc.Name = "nc";
            this.nc.Visible = false;
            // 
            // id_stavka
            // 
            this.id_stavka.HeaderText = "id_stavka";
            this.id_stavka.Name = "id_stavka";
            this.id_stavka.Visible = false;
            // 
            // id_roba_prodaja
            // 
            this.id_roba_prodaja.HeaderText = "id_roba_prodaja";
            this.id_roba_prodaja.Name = "id_roba_prodaja";
            this.id_roba_prodaja.Visible = false;
            // 
            // oduzmi
            // 
            this.oduzmi.HeaderText = "oduzmi";
            this.oduzmi.Name = "oduzmi";
            this.oduzmi.Visible = false;
            // 
            // porez_potrosnja
            // 
            this.porez_potrosnja.HeaderText = "porez_potrosnja";
            this.porez_potrosnja.Name = "porez_potrosnja";
            this.porez_potrosnja.Visible = false;
            // 
            // lblNaDan
            // 
            this.lblNaDan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNaDan.AutoSize = true;
            this.lblNaDan.BackColor = System.Drawing.Color.Transparent;
            this.lblNaDan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblNaDan.Location = new System.Drawing.Point(12, 584);
            this.lblNaDan.Name = "lblNaDan";
            this.lblNaDan.Size = new System.Drawing.Size(0, 13);
            this.lblNaDan.TabIndex = 14;
            // 
            // btnIzlaz
            // 
            this.btnIzlaz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIzlaz.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnIzlaz.Image = global::PCPOS.Properties.Resources.Actions_application_exit_icon;
            this.btnIzlaz.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIzlaz.Location = new System.Drawing.Point(873, 12);
            this.btnIzlaz.Name = "btnIzlaz";
            this.btnIzlaz.Size = new System.Drawing.Size(130, 40);
            this.btnIzlaz.TabIndex = 6;
            this.btnIzlaz.Text = "Izlaz      ";
            this.btnIzlaz.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIzlaz.UseVisualStyleBackColor = true;
            this.btnIzlaz.Click += new System.EventHandler(this.btnIzlaz_Click);
            // 
            // btnDeleteAllDopis
            // 
            this.btnDeleteAllDopis.Enabled = false;
            this.btnDeleteAllDopis.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnDeleteAllDopis.Image = global::PCPOS.Properties.Resources.Recyclebin_Empty;
            this.btnDeleteAllDopis.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteAllDopis.Location = new System.Drawing.Point(556, 12);
            this.btnDeleteAllDopis.Name = "btnDeleteAllDopis";
            this.btnDeleteAllDopis.Size = new System.Drawing.Size(130, 40);
            this.btnDeleteAllDopis.TabIndex = 4;
            this.btnDeleteAllDopis.Text = "Obriši dopis  ";
            this.btnDeleteAllDopis.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteAllDopis.UseVisualStyleBackColor = true;
            this.btnDeleteAllDopis.Click += new System.EventHandler(this.btnDeleteAllDopis_Click);
            // 
            // btnSviDopisi
            // 
            this.btnSviDopisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSviDopisi.Image = global::PCPOS.Properties.Resources._10591;
            this.btnSviDopisi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSviDopisi.Location = new System.Drawing.Point(420, 12);
            this.btnSviDopisi.Name = "btnSviDopisi";
            this.btnSviDopisi.Size = new System.Drawing.Size(130, 40);
            this.btnSviDopisi.TabIndex = 3;
            this.btnSviDopisi.Text = "Svi dopisi    ";
            this.btnSviDopisi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSviDopisi.UseVisualStyleBackColor = true;
            this.btnSviDopisi.Click += new System.EventHandler(this.btnSviDopisi_Click);
            // 
            // btnNoviUnos
            // 
            this.btnNoviUnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnNoviUnos.Image = global::PCPOS.Properties.Resources.folder_open_icon;
            this.btnNoviUnos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNoviUnos.Location = new System.Drawing.Point(12, 12);
            this.btnNoviUnos.Name = "btnNoviUnos";
            this.btnNoviUnos.Size = new System.Drawing.Size(130, 40);
            this.btnNoviUnos.TabIndex = 0;
            this.btnNoviUnos.Text = "Novi unos   ";
            this.btnNoviUnos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNoviUnos.UseVisualStyleBackColor = true;
            this.btnNoviUnos.Click += new System.EventHandler(this.btnNoviUnos_Click);
            // 
            // btnOdustani
            // 
            this.btnOdustani.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnOdustani.Image = global::PCPOS.Properties.Resources.undo;
            this.btnOdustani.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOdustani.Location = new System.Drawing.Point(148, 12);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(130, 40);
            this.btnOdustani.TabIndex = 1;
            this.btnOdustani.Text = "Odustani   ";
            this.btnOdustani.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOdustani.UseVisualStyleBackColor = true;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            // 
            // btnSpremi
            // 
            this.btnSpremi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSpremi.Image = ((System.Drawing.Image)(resources.GetObject("btnSpremi.Image")));
            this.btnSpremi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSpremi.Location = new System.Drawing.Point(284, 12);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(130, 40);
            this.btnSpremi.TabIndex = 2;
            this.btnSpremi.Text = "Spremi   ";
            this.btnSpremi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSpremi.UseVisualStyleBackColor = true;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // frmDopis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1015, 606);
            this.Controls.Add(this.txtText);
            this.Controls.Add(this.btnIzlaz);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.btnDeleteAllDopis);
            this.Controls.Add(this.btnSviDopisi);
            this.Controls.Add(this.btnNoviUnos);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.lblNaDan);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmDopis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dopis";
            this.Load += new System.EventHandler(this.frmPonude_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmGodinaDopisa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnPartner;
        private System.Windows.Forms.Label lblIzradio;
        private System.Windows.Forms.DateTimePicker dtpDatum;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Label lblDatum;
        private System.Windows.Forms.Label lblPartner;
        private System.Windows.Forms.TextBox txtIzradio;
        private System.Windows.Forms.TextBox txtPartnerNaziv;
        private System.Windows.Forms.TextBox txtPartnerSifra;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblBrojDopisa;
        public System.Windows.Forms.TextBox txtBrojDopisa;
        private System.Windows.Forms.NumericUpDown nmGodinaDopisa;
        private System.Windows.Forms.Label lblNaDan;
        private System.Windows.Forms.Button btnIzlaz;
        private System.Windows.Forms.Button btnDeleteAllDopis;
        private System.Windows.Forms.Button btnSviDopisi;
        private System.Windows.Forms.Button btnNoviUnos;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Button btnSpremi;
		private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.DataGridViewTextBoxColumn br;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewComboBoxColumn skladiste;
        private System.Windows.Forms.DataGridViewTextBoxColumn jmj;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolicina;
        private System.Windows.Forms.DataGridViewTextBoxColumn porez;
        private System.Windows.Forms.DataGridViewTextBoxColumn porezZaIzracun;
        private System.Windows.Forms.DataGridViewTextBoxColumn mpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn rabat;
        private System.Windows.Forms.DataGridViewTextBoxColumn rabat_iznos;
        private System.Windows.Forms.DataGridViewTextBoxColumn cijena_bez_pdva;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos_bez_pdva;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos_ukupno;
        private System.Windows.Forms.DataGridViewTextBoxColumn vpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn nc;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_stavka;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_roba_prodaja;
        private System.Windows.Forms.DataGridViewTextBoxColumn oduzmi;
        private System.Windows.Forms.DataGridViewTextBoxColumn porez_potrosnja;
    }
}