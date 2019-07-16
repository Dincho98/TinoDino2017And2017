namespace PCPOS.Robno
{
    partial class frmPrimka
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrimka));
            this.label8 = new System.Windows.Forms.Label();
            this.txtOrginalniDok = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSifra_robe = new System.Windows.Forms.TextBox();
            this.txtPartnerNaziv = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIzradio = new System.Windows.Forms.TextBox();
            this.rtbNapomena = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgw = new PCPOS.Robno.frmPrimka.MyDataGrid();
            this.rb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nbc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pdv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rabat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ukupno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_stavka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.primkaID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSifraOdrediste = new System.Windows.Forms.TextBox();
            this.txtBroj = new System.Windows.Forms.TextBox();
            this.nmGodina = new System.Windows.Forms.NumericUpDown();
            this.cbSkladiste = new System.Windows.Forms.ComboBox();
            this.dtpDatum = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblNaDan = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDeleteAllFaktura = new System.Windows.Forms.Button();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.btnOpenRoba = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSveFakture = new System.Windows.Forms.Button();
            this.btnNoviUnos = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbMjesto = new System.Windows.Forms.ComboBox();
            this.btnIspis = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmGodina)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpenRoba)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Arial", 10F);
            this.label8.Location = new System.Drawing.Point(363, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 16);
            this.label8.TabIndex = 9;
            this.label8.Text = "Originalni dok.";
            // 
            // txtOrginalniDok
            // 
            this.txtOrginalniDok.BackColor = System.Drawing.Color.White;
            this.txtOrginalniDok.Enabled = false;
            this.txtOrginalniDok.Font = new System.Drawing.Font("Arial", 10F);
            this.txtOrginalniDok.Location = new System.Drawing.Point(476, 48);
            this.txtOrginalniDok.Name = "txtOrginalniDok";
            this.txtOrginalniDok.Size = new System.Drawing.Size(277, 23);
            this.txtOrginalniDok.TabIndex = 10;
            this.txtOrginalniDok.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtOrginalniDok.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownEnter);
            this.txtOrginalniDok.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 10F);
            this.label7.Location = new System.Drawing.Point(363, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 16);
            this.label7.TabIndex = 7;
            this.label7.Text = "Mjesto troška:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Arial", 10F);
            this.label6.Location = new System.Drawing.Point(17, 299);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "Šifra artikla:";
            // 
            // txtSifra_robe
            // 
            this.txtSifra_robe.Font = new System.Drawing.Font("Arial", 10F);
            this.txtSifra_robe.Location = new System.Drawing.Point(20, 316);
            this.txtSifra_robe.Name = "txtSifra_robe";
            this.txtSifra_robe.Size = new System.Drawing.Size(237, 23);
            this.txtSifra_robe.TabIndex = 11;
            this.txtSifra_robe.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtSifra_robe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifra_robe_KeyDown);
            this.txtSifra_robe.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtPartnerNaziv
            // 
            this.txtPartnerNaziv.BackColor = System.Drawing.Color.White;
            this.txtPartnerNaziv.Font = new System.Drawing.Font("Arial", 10F);
            this.txtPartnerNaziv.Location = new System.Drawing.Point(21, 100);
            this.txtPartnerNaziv.Name = "txtPartnerNaziv";
            this.txtPartnerNaziv.ReadOnly = true;
            this.txtPartnerNaziv.Size = new System.Drawing.Size(308, 23);
            this.txtPartnerNaziv.TabIndex = 6;
            this.txtPartnerNaziv.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtPartnerNaziv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownEnter);
            this.txtPartnerNaziv.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 10F);
            this.label2.Location = new System.Drawing.Point(18, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Izradio:";
            // 
            // txtIzradio
            // 
            this.txtIzradio.BackColor = System.Drawing.Color.White;
            this.txtIzradio.Font = new System.Drawing.Font("Arial", 10F);
            this.txtIzradio.Location = new System.Drawing.Point(94, 21);
            this.txtIzradio.Name = "txtIzradio";
            this.txtIzradio.ReadOnly = true;
            this.txtIzradio.Size = new System.Drawing.Size(235, 23);
            this.txtIzradio.TabIndex = 1;
            this.txtIzradio.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtIzradio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownEnter);
            this.txtIzradio.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // rtbNapomena
            // 
            this.rtbNapomena.Enabled = false;
            this.rtbNapomena.Font = new System.Drawing.Font("Arial", 10F);
            this.rtbNapomena.Location = new System.Drawing.Point(476, 75);
            this.rtbNapomena.Name = "rtbNapomena";
            this.rtbNapomena.Size = new System.Drawing.Size(277, 51);
            this.rtbNapomena.TabIndex = 12;
            this.rtbNapomena.Text = "";
            this.rtbNapomena.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.rtbNapomena.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownEnter);
            this.rtbNapomena.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 10F);
            this.label3.Location = new System.Drawing.Point(363, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Napomena:";
            // 
            // dgw
            // 
            this.dgw.AllowUserToAddRows = false;
            this.dgw.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgw.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgw.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgw.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgw.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgw.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rb,
            this.sifra,
            this.naziv,
            this.nbc,
            this.kolicina,
            this.vpc,
            this.mpc,
            this.pdv,
            this.rabat,
            this.iznos,
            this.ukupno,
            this.id_stavka,
            this.primkaID});
            this.dgw.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgw.GridColor = System.Drawing.Color.Gainsboro;
            this.dgw.Location = new System.Drawing.Point(20, 345);
            this.dgw.MultiSelect = false;
            this.dgw.Name = "dgw";
            this.dgw.RowHeadersWidth = 30;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dgw.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgw.Size = new System.Drawing.Size(949, 247);
            this.dgw.TabIndex = 12;
            this.dgw.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellClick);
            this.dgw.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellEndEdit);
            this.dgw.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.dgw.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // rb
            // 
            this.rb.FillWeight = 30F;
            this.rb.HeaderText = "RB";
            this.rb.Name = "rb";
            this.rb.ReadOnly = true;
            // 
            // sifra
            // 
            this.sifra.FillWeight = 80F;
            this.sifra.HeaderText = "Šifra robe";
            this.sifra.Name = "sifra";
            this.sifra.ReadOnly = true;
            // 
            // naziv
            // 
            this.naziv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.naziv.FillWeight = 105.5838F;
            this.naziv.HeaderText = "Naziv";
            this.naziv.Name = "naziv";
            this.naziv.ReadOnly = true;
            // 
            // nbc
            // 
            this.nbc.FillWeight = 70F;
            this.nbc.HeaderText = "Nabavna";
            this.nbc.Name = "nbc";
            // 
            // kolicina
            // 
            this.kolicina.FillWeight = 70F;
            this.kolicina.HeaderText = "Količina";
            this.kolicina.Name = "kolicina";
            // 
            // vpc
            // 
            this.vpc.HeaderText = "Veleprodajna";
            this.vpc.Name = "vpc";
            // 
            // mpc
            // 
            this.mpc.HeaderText = "Maloprodajna";
            this.mpc.Name = "mpc";
            // 
            // pdv
            // 
            this.pdv.HeaderText = "PDV %";
            this.pdv.Name = "pdv";
            // 
            // rabat
            // 
            this.rabat.HeaderText = "Rabat %";
            this.rabat.Name = "rabat";
            // 
            // iznos
            // 
            this.iznos.HeaderText = "Iznos bez pdv-a";
            this.iznos.Name = "iznos";
            this.iznos.ReadOnly = true;
            // 
            // ukupno
            // 
            this.ukupno.HeaderText = "Ukupno";
            this.ukupno.Name = "ukupno";
            this.ukupno.ReadOnly = true;
            // 
            // id_stavka
            // 
            this.id_stavka.HeaderText = "id_stavka";
            this.id_stavka.Name = "id_stavka";
            this.id_stavka.Visible = false;
            // 
            // primkaID
            // 
            this.primkaID.HeaderText = "primka";
            this.primkaID.Name = "primkaID";
            this.primkaID.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(367, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Broj unosa:";
            // 
            // txtSifraOdrediste
            // 
            this.txtSifraOdrediste.BackColor = System.Drawing.Color.White;
            this.txtSifraOdrediste.Enabled = false;
            this.txtSifraOdrediste.Font = new System.Drawing.Font("Arial", 10F);
            this.txtSifraOdrediste.Location = new System.Drawing.Point(94, 75);
            this.txtSifraOdrediste.Name = "txtSifraOdrediste";
            this.txtSifraOdrediste.Size = new System.Drawing.Size(196, 23);
            this.txtSifraOdrediste.TabIndex = 5;
            this.txtSifraOdrediste.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtSifraOdrediste.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraOdrediste_KeyDown);
            this.txtSifraOdrediste.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtBroj
            // 
            this.txtBroj.BackColor = System.Drawing.Color.White;
            this.txtBroj.Font = new System.Drawing.Font("Arial", 10F);
            this.txtBroj.Location = new System.Drawing.Point(463, 18);
            this.txtBroj.Name = "txtBroj";
            this.txtBroj.Size = new System.Drawing.Size(121, 23);
            this.txtBroj.TabIndex = 3;
            this.txtBroj.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtBroj.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBroj_KeyDown);
            this.txtBroj.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // nmGodina
            // 
            this.nmGodina.BackColor = System.Drawing.Color.White;
            this.nmGodina.Font = new System.Drawing.Font("Arial", 10F);
            this.nmGodina.Location = new System.Drawing.Point(584, 18);
            this.nmGodina.Name = "nmGodina";
            this.nmGodina.Size = new System.Drawing.Size(85, 23);
            this.nmGodina.TabIndex = 4;
            this.nmGodina.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.nmGodina.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBroj_KeyDown);
            this.nmGodina.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // cbSkladiste
            // 
            this.cbSkladiste.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbSkladiste.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbSkladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkladiste.Font = new System.Drawing.Font("Arial", 10F);
            this.cbSkladiste.FormattingEnabled = true;
            this.cbSkladiste.Location = new System.Drawing.Point(106, 18);
            this.cbSkladiste.Name = "cbSkladiste";
            this.cbSkladiste.Size = new System.Drawing.Size(207, 24);
            this.cbSkladiste.TabIndex = 1;
            this.cbSkladiste.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbSkladiste.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBroj_KeyDown);
            this.cbSkladiste.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // dtpDatum
            // 
            this.dtpDatum.Enabled = false;
            this.dtpDatum.Font = new System.Drawing.Font("Arial", 10F);
            this.dtpDatum.Location = new System.Drawing.Point(94, 48);
            this.dtpDatum.Name = "dtpDatum";
            this.dtpDatum.Size = new System.Drawing.Size(235, 23);
            this.dtpDatum.TabIndex = 3;
            this.dtpDatum.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.dtpDatum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownEnter);
            this.dtpDatum.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Arial", 10F);
            this.label4.Location = new System.Drawing.Point(18, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Datum:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Arial", 10F);
            this.label5.Location = new System.Drawing.Point(19, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Odredište:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label17.ForeColor = System.Drawing.Color.Maroon;
            this.label17.Location = new System.Drawing.Point(18, 22);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(82, 18);
            this.label17.TabIndex = 0;
            this.label17.Text = "Skladište:";
            // 
            // lblNaDan
            // 
            this.lblNaDan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNaDan.AutoSize = true;
            this.lblNaDan.BackColor = System.Drawing.Color.Transparent;
            this.lblNaDan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblNaDan.Location = new System.Drawing.Point(24, 595);
            this.lblNaDan.Name = "lblNaDan";
            this.lblNaDan.Size = new System.Drawing.Size(0, 13);
            this.lblNaDan.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.txtBroj);
            this.groupBox1.Controls.Add(this.nmGodina);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbSkladiste);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Location = new System.Drawing.Point(21, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(682, 55);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // btnDeleteAllFaktura
            // 
            this.btnDeleteAllFaktura.Enabled = false;
            this.btnDeleteAllFaktura.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnDeleteAllFaktura.Image = global::PCPOS.Properties.Resources.Recyclebin_Empty;
            this.btnDeleteAllFaktura.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteAllFaktura.Location = new System.Drawing.Point(573, 19);
            this.btnDeleteAllFaktura.Name = "btnDeleteAllFaktura";
            this.btnDeleteAllFaktura.Size = new System.Drawing.Size(130, 40);
            this.btnDeleteAllFaktura.TabIndex = 4;
            this.btnDeleteAllFaktura.Text = "Obriši primku";
            this.btnDeleteAllFaktura.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteAllFaktura.UseVisualStyleBackColor = true;
            this.btnDeleteAllFaktura.Click += new System.EventHandler(this.btnDeleteAllFaktura_Click);
            // 
            // btnObrisi
            // 
            this.btnObrisi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnObrisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnObrisi.Image = global::PCPOS.Properties.Resources.Close;
            this.btnObrisi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnObrisi.Location = new System.Drawing.Point(839, 312);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(130, 30);
            this.btnObrisi.TabIndex = 13;
            this.btnObrisi.Text = "   Obriši stavku";
            this.btnObrisi.UseVisualStyleBackColor = true;
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // btnOpenRoba
            // 
            this.btnOpenRoba.BackColor = System.Drawing.Color.Transparent;
            this.btnOpenRoba.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenRoba.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenRoba.Image")));
            this.btnOpenRoba.Location = new System.Drawing.Point(263, 312);
            this.btnOpenRoba.Name = "btnOpenRoba";
            this.btnOpenRoba.Size = new System.Drawing.Size(39, 31);
            this.btnOpenRoba.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnOpenRoba.TabIndex = 222;
            this.btnOpenRoba.TabStop = false;
            this.btnOpenRoba.Click += new System.EventHandler(this.btnOpenRoba_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(290, 71);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(39, 31);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 215;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.Image = global::PCPOS.Properties.Resources.Actions_application_exit_icon;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(870, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 40);
            this.button1.TabIndex = 6;
            this.button1.Text = "Izlaz   ";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSveFakture
            // 
            this.btnSveFakture.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSveFakture.Image = global::PCPOS.Properties.Resources._10591;
            this.btnSveFakture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSveFakture.Location = new System.Drawing.Point(434, 19);
            this.btnSveFakture.Name = "btnSveFakture";
            this.btnSveFakture.Size = new System.Drawing.Size(130, 40);
            this.btnSveFakture.TabIndex = 3;
            this.btnSveFakture.Text = "Sve primke";
            this.btnSveFakture.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSveFakture.UseVisualStyleBackColor = true;
            this.btnSveFakture.Click += new System.EventHandler(this.btnSveFakture_Click);
            // 
            // btnNoviUnos
            // 
            this.btnNoviUnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnNoviUnos.Image = global::PCPOS.Properties.Resources.folder_open_icon;
            this.btnNoviUnos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNoviUnos.Location = new System.Drawing.Point(20, 19);
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
            this.btnOdustani.Location = new System.Drawing.Point(158, 19);
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
            this.btnSpremi.Location = new System.Drawing.Point(296, 19);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(130, 40);
            this.btnSpremi.TabIndex = 2;
            this.btnSpremi.Text = "Spremi   ";
            this.btnSpremi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSpremi.UseVisualStyleBackColor = true;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.cbMjesto);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.rtbNapomena);
            this.groupBox2.Controls.Add(this.txtOrginalniDok);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtPartnerNaziv);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.dtpDatum);
            this.groupBox2.Controls.Add(this.txtIzradio);
            this.groupBox2.Controls.Add(this.txtSifraOdrediste);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Location = new System.Drawing.Point(21, 138);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(948, 144);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            // 
            // cbMjesto
            // 
            this.cbMjesto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbMjesto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbMjesto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMjesto.Font = new System.Drawing.Font("Arial", 10F);
            this.cbMjesto.FormattingEnabled = true;
            this.cbMjesto.Location = new System.Drawing.Point(476, 18);
            this.cbMjesto.Name = "cbMjesto";
            this.cbMjesto.Size = new System.Drawing.Size(277, 24);
            this.cbMjesto.TabIndex = 8;
            this.cbMjesto.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbMjesto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownEnter);
            this.cbMjesto.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // btnIspis
            // 
            this.btnIspis.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnIspis.Image = global::PCPOS.Properties.Resources.print_printer;
            this.btnIspis.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIspis.Location = new System.Drawing.Point(709, 19);
            this.btnIspis.Name = "btnIspis";
            this.btnIspis.Size = new System.Drawing.Size(108, 40);
            this.btnIspis.TabIndex = 5;
            this.btnIspis.Text = "Ispis    ";
            this.btnIspis.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIspis.UseVisualStyleBackColor = true;
            this.btnIspis.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmPrimka
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(985, 613);
            this.Controls.Add(this.btnIspis);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblNaDan);
            this.Controls.Add(this.btnDeleteAllFaktura);
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.btnOpenRoba);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtSifra_robe);
            this.Controls.Add(this.dgw);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSveFakture);
            this.Controls.Add(this.btnNoviUnos);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnSpremi);
            this.Name = "frmPrimka";
            this.Text = "Primka";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPrimka_FormClosing);
            this.Load += new System.EventHandler(this.frmPrimka_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmGodina)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpenRoba)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDeleteAllFaktura;
        private System.Windows.Forms.Button btnObrisi;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox txtOrginalniDok;
		private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox btnOpenRoba;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSifra_robe;
        public System.Windows.Forms.TextBox txtPartnerNaziv;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtIzradio;
        private System.Windows.Forms.RichTextBox rtbNapomena;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
		//private System.Windows.Forms.DataGridView dgw;
		private frmPrimka.MyDataGrid dgw;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSveFakture;
        private System.Windows.Forms.Button btnNoviUnos;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtSifraOdrediste;
        public System.Windows.Forms.TextBox txtBroj;
        private System.Windows.Forms.NumericUpDown nmGodina;
        private System.Windows.Forms.ComboBox cbSkladiste;
        private System.Windows.Forms.DateTimePicker dtpDatum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblNaDan;
        private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox cbMjesto;
        private System.Windows.Forms.Button btnIspis;
        private System.Windows.Forms.DataGridViewTextBoxColumn rb;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn nbc;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolicina;
        private System.Windows.Forms.DataGridViewTextBoxColumn vpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn mpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn pdv;
        private System.Windows.Forms.DataGridViewTextBoxColumn rabat;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos;
        private System.Windows.Forms.DataGridViewTextBoxColumn ukupno;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_stavka;
        private System.Windows.Forms.DataGridViewTextBoxColumn primkaID;
    }
}