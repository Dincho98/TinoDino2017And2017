namespace PCPOS.Robno
{
    partial class frmRobniDobropis
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
            this.btnNoviUnos = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.btnSviDobropisi = new System.Windows.Forms.Button();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.btnIzlaz = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbPartnerNaziv = new System.Windows.Forms.TextBox();
            this.pbTraziOdrediste = new System.Windows.Forms.PictureBox();
            this.tbPartnerSifra = new System.Windows.Forms.TextBox();
            this.cbSkladiste = new System.Windows.Forms.ComboBox();
            this.nmGodina = new System.Windows.Forms.NumericUpDown();
            this.tbBrojUnosa = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtbNapomena = new System.Windows.Forms.RichTextBox();
            this.tbOriginalniDokument = new System.Windows.Forms.TextBox();
            this.cbMjestoTroska = new System.Windows.Forms.ComboBox();
            this.dtpDatum = new System.Windows.Forms.DateTimePicker();
            this.tbIzradio = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbSifraRobe = new System.Windows.Forms.TextBox();
            this.pbTraziStavke = new System.Windows.Forms.PictureBox();
            this.btnObrisiStavku = new System.Windows.Forms.Button();
            this.dgw = new System.Windows.Forms.DataGridView();
            this.br = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv_robe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.porez = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rabat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rabat_iznos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cijena_bez_pdv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos_bez_pdv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos_ukupno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTraziOdrediste)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmGodina)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTraziStavke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNoviUnos
            // 
            this.btnNoviUnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnNoviUnos.Image = global::PCPOS.Properties.Resources.folder_open_icon;
            this.btnNoviUnos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNoviUnos.Location = new System.Drawing.Point(12, 12);
            this.btnNoviUnos.Name = "btnNoviUnos";
            this.btnNoviUnos.Size = new System.Drawing.Size(133, 42);
            this.btnNoviUnos.TabIndex = 0;
            this.btnNoviUnos.Text = "Novi unos   ";
            this.btnNoviUnos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNoviUnos.UseVisualStyleBackColor = true;
            this.btnNoviUnos.Click += new System.EventHandler(this.btnNoviUnos_Click);
            // 
            // btnOdustani
            // 
            this.btnOdustani.Enabled = false;
            this.btnOdustani.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnOdustani.Image = global::PCPOS.Properties.Resources.undo;
            this.btnOdustani.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOdustani.Location = new System.Drawing.Point(151, 12);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(121, 42);
            this.btnOdustani.TabIndex = 1;
            this.btnOdustani.Text = "Odustani  ";
            this.btnOdustani.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOdustani.UseVisualStyleBackColor = true;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            // 
            // btnSpremi
            // 
            this.btnSpremi.Enabled = false;
            this.btnSpremi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSpremi.Image = global::PCPOS.Properties.Resources.filesave;
            this.btnSpremi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSpremi.Location = new System.Drawing.Point(278, 12);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(111, 42);
            this.btnSpremi.TabIndex = 2;
            this.btnSpremi.Text = "Spremi  ";
            this.btnSpremi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSpremi.UseVisualStyleBackColor = true;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // btnSviDobropisi
            // 
            this.btnSviDobropisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSviDobropisi.Image = global::PCPOS.Properties.Resources._10591;
            this.btnSviDobropisi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSviDobropisi.Location = new System.Drawing.Point(395, 12);
            this.btnSviDobropisi.Name = "btnSviDobropisi";
            this.btnSviDobropisi.Size = new System.Drawing.Size(143, 42);
            this.btnSviDobropisi.TabIndex = 3;
            this.btnSviDobropisi.Text = "Svi dobropisi  ";
            this.btnSviDobropisi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSviDobropisi.UseVisualStyleBackColor = true;
            this.btnSviDobropisi.Click += new System.EventHandler(this.BtnSviDobropisi_Click);
            // 
            // btnObrisi
            // 
            this.btnObrisi.Enabled = false;
            this.btnObrisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnObrisi.Image = global::PCPOS.Properties.Resources.Recyclebin_Empty;
            this.btnObrisi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnObrisi.Location = new System.Drawing.Point(544, 12);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(154, 42);
            this.btnObrisi.TabIndex = 4;
            this.btnObrisi.Text = "Obriši dobropis  ";
            this.btnObrisi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnObrisi.UseVisualStyleBackColor = true;
            this.btnObrisi.Click += new System.EventHandler(this.BtnObrisi_Click);
            // 
            // btnIzlaz
            // 
            this.btnIzlaz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIzlaz.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnIzlaz.Image = global::PCPOS.Properties.Resources.Actions_application_exit_icon;
            this.btnIzlaz.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIzlaz.Location = new System.Drawing.Point(960, 12);
            this.btnIzlaz.Name = "btnIzlaz";
            this.btnIzlaz.Size = new System.Drawing.Size(94, 42);
            this.btnIzlaz.TabIndex = 6;
            this.btnIzlaz.Text = "Izlaz  ";
            this.btnIzlaz.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIzlaz.UseVisualStyleBackColor = true;
            this.btnIzlaz.Click += new System.EventHandler(this.btnIzlaz_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.tbPartnerNaziv);
            this.groupBox1.Controls.Add(this.pbTraziOdrediste);
            this.groupBox1.Controls.Add(this.tbPartnerSifra);
            this.groupBox1.Controls.Add(this.cbSkladiste);
            this.groupBox1.Controls.Add(this.nmGodina);
            this.groupBox1.Controls.Add(this.tbBrojUnosa);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(377, 135);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // tbPartnerNaziv
            // 
            this.tbPartnerNaziv.Enabled = false;
            this.tbPartnerNaziv.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbPartnerNaziv.Location = new System.Drawing.Point(117, 102);
            this.tbPartnerNaziv.Name = "tbPartnerNaziv";
            this.tbPartnerNaziv.ReadOnly = true;
            this.tbPartnerNaziv.Size = new System.Drawing.Size(189, 23);
            this.tbPartnerNaziv.TabIndex = 8;
            // 
            // pbTraziOdrediste
            // 
            this.pbTraziOdrediste.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbTraziOdrediste.Enabled = false;
            this.pbTraziOdrediste.Image = global::PCPOS.Properties.Resources._10591;
            this.pbTraziOdrediste.Location = new System.Drawing.Point(312, 68);
            this.pbTraziOdrediste.Name = "pbTraziOdrediste";
            this.pbTraziOdrediste.Size = new System.Drawing.Size(33, 32);
            this.pbTraziOdrediste.TabIndex = 7;
            this.pbTraziOdrediste.TabStop = false;
            this.pbTraziOdrediste.Click += new System.EventHandler(this.PbTraziOdrediste_Click_1);
            // 
            // tbPartnerSifra
            // 
            this.tbPartnerSifra.Enabled = false;
            this.tbPartnerSifra.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbPartnerSifra.Location = new System.Drawing.Point(117, 73);
            this.tbPartnerSifra.Name = "tbPartnerSifra";
            this.tbPartnerSifra.Size = new System.Drawing.Size(189, 23);
            this.tbPartnerSifra.TabIndex = 6;
            this.tbPartnerSifra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbPartnerSifra_KeyDown);
            // 
            // cbSkladiste
            // 
            this.cbSkladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkladiste.Enabled = false;
            this.cbSkladiste.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbSkladiste.FormattingEnabled = true;
            this.cbSkladiste.Location = new System.Drawing.Point(117, 44);
            this.cbSkladiste.Name = "cbSkladiste";
            this.cbSkladiste.Size = new System.Drawing.Size(143, 24);
            this.cbSkladiste.TabIndex = 5;
            // 
            // nmGodina
            // 
            this.nmGodina.Enabled = false;
            this.nmGodina.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.nmGodina.Location = new System.Drawing.Point(266, 15);
            this.nmGodina.Name = "nmGodina";
            this.nmGodina.Size = new System.Drawing.Size(80, 23);
            this.nmGodina.TabIndex = 4;
            // 
            // tbBrojUnosa
            // 
            this.tbBrojUnosa.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbBrojUnosa.Location = new System.Drawing.Point(117, 15);
            this.tbBrojUnosa.Name = "tbBrojUnosa";
            this.tbBrojUnosa.Size = new System.Drawing.Size(143, 23);
            this.tbBrojUnosa.TabIndex = 3;
            this.tbBrojUnosa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbBrojUnosa_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.Color.Maroon;
            this.label3.Location = new System.Drawing.Point(20, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Odredište :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.Color.Maroon;
            this.label2.Location = new System.Drawing.Point(24, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Skladište :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(11, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Broj unosa :";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.rtbNapomena);
            this.groupBox2.Controls.Add(this.tbOriginalniDokument);
            this.groupBox2.Controls.Add(this.cbMjestoTroska);
            this.groupBox2.Controls.Add(this.dtpDatum);
            this.groupBox2.Controls.Add(this.tbIzradio);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 201);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(789, 106);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // rtbNapomena
            // 
            this.rtbNapomena.Enabled = false;
            this.rtbNapomena.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rtbNapomena.Location = new System.Drawing.Point(512, 42);
            this.rtbNapomena.Name = "rtbNapomena";
            this.rtbNapomena.Size = new System.Drawing.Size(260, 51);
            this.rtbNapomena.TabIndex = 12;
            this.rtbNapomena.Text = "";
            // 
            // tbOriginalniDokument
            // 
            this.tbOriginalniDokument.Enabled = false;
            this.tbOriginalniDokument.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbOriginalniDokument.Location = new System.Drawing.Point(512, 13);
            this.tbOriginalniDokument.Name = "tbOriginalniDokument";
            this.tbOriginalniDokument.Size = new System.Drawing.Size(260, 23);
            this.tbOriginalniDokument.TabIndex = 11;
            // 
            // cbMjestoTroska
            // 
            this.cbMjestoTroska.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMjestoTroska.Enabled = false;
            this.cbMjestoTroska.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbMjestoTroska.FormattingEnabled = true;
            this.cbMjestoTroska.Location = new System.Drawing.Point(117, 69);
            this.cbMjestoTroska.Name = "cbMjestoTroska";
            this.cbMjestoTroska.Size = new System.Drawing.Size(229, 24);
            this.cbMjestoTroska.TabIndex = 9;
            // 
            // dtpDatum
            // 
            this.dtpDatum.CalendarFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatum.Enabled = false;
            this.dtpDatum.Location = new System.Drawing.Point(117, 43);
            this.dtpDatum.Name = "dtpDatum";
            this.dtpDatum.Size = new System.Drawing.Size(229, 20);
            this.dtpDatum.TabIndex = 10;
            // 
            // tbIzradio
            // 
            this.tbIzradio.Enabled = false;
            this.tbIzradio.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbIzradio.Location = new System.Drawing.Point(117, 13);
            this.tbIzradio.Name = "tbIzradio";
            this.tbIzradio.ReadOnly = true;
            this.tbIzradio.Size = new System.Drawing.Size(229, 23);
            this.tbIzradio.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(422, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 16);
            this.label8.TabIndex = 4;
            this.label8.Text = "Napomena :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(399, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 16);
            this.label7.TabIndex = 3;
            this.label7.Text = "Originalni dok. :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(11, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 16);
            this.label6.TabIndex = 2;
            this.label6.Text = "Mjesto troška :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(54, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "Datum :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(54, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Izradio :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Arial", 10F);
            this.label9.Location = new System.Drawing.Point(9, 323);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 16);
            this.label9.TabIndex = 11;
            this.label9.Text = "Šifra artikla:";
            // 
            // tbSifraRobe
            // 
            this.tbSifraRobe.Enabled = false;
            this.tbSifraRobe.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbSifraRobe.Location = new System.Drawing.Point(12, 342);
            this.tbSifraRobe.Name = "tbSifraRobe";
            this.tbSifraRobe.Size = new System.Drawing.Size(201, 23);
            this.tbSifraRobe.TabIndex = 9;
            this.tbSifraRobe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbSifraRobe_KeyDown);
            // 
            // pbTraziStavke
            // 
            this.pbTraziStavke.BackColor = System.Drawing.Color.Transparent;
            this.pbTraziStavke.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbTraziStavke.Enabled = false;
            this.pbTraziStavke.Image = global::PCPOS.Properties.Resources._10591;
            this.pbTraziStavke.Location = new System.Drawing.Point(219, 337);
            this.pbTraziStavke.Name = "pbTraziStavke";
            this.pbTraziStavke.Size = new System.Drawing.Size(33, 32);
            this.pbTraziStavke.TabIndex = 9;
            this.pbTraziStavke.TabStop = false;
            this.pbTraziStavke.Click += new System.EventHandler(this.pbTraziStavke_Click);
            // 
            // btnObrisiStavku
            // 
            this.btnObrisiStavku.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnObrisiStavku.Enabled = false;
            this.btnObrisiStavku.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnObrisiStavku.Image = global::PCPOS.Properties.Resources.Close;
            this.btnObrisiStavku.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnObrisiStavku.Location = new System.Drawing.Point(925, 325);
            this.btnObrisiStavku.Name = "btnObrisiStavku";
            this.btnObrisiStavku.Size = new System.Drawing.Size(129, 40);
            this.btnObrisiStavku.TabIndex = 13;
            this.btnObrisiStavku.Text = "Obriši stavku ";
            this.btnObrisiStavku.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnObrisiStavku.UseVisualStyleBackColor = true;
            this.btnObrisiStavku.Click += new System.EventHandler(this.BtnObrisiStavku_Click);
            // 
            // dgw
            // 
            this.dgw.AllowUserToAddRows = false;
            this.dgw.AllowUserToDeleteRows = false;
            this.dgw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgw.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgw.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgw.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgw.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.br,
            this.sifra,
            this.naziv_robe,
            this.jm,
            this.kolicina,
            this.vpc,
            this.porez,
            this.mpc,
            this.rabat,
            this.rabat_iznos,
            this.cijena_bez_pdv,
            this.iznos_bez_pdv,
            this.iznos_ukupno});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgw.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgw.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgw.Location = new System.Drawing.Point(12, 371);
            this.dgw.MultiSelect = false;
            this.dgw.Name = "dgw";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgw.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgw.RowHeadersWidth = 53;
            this.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgw.Size = new System.Drawing.Size(1042, 238);
            this.dgw.TabIndex = 12;
            this.dgw.TabStop = false;
            this.dgw.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellClick);
            this.dgw.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellEndEdit);
            this.dgw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgw_KeyDown);
            // 
            // br
            // 
            this.br.FillWeight = 50F;
            this.br.HeaderText = "Br.";
            this.br.Name = "br";
            this.br.ReadOnly = true;
            // 
            // sifra
            // 
            this.sifra.FillWeight = 60F;
            this.sifra.HeaderText = "Šifra";
            this.sifra.Name = "sifra";
            this.sifra.ReadOnly = true;
            // 
            // naziv_robe
            // 
            this.naziv_robe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.naziv_robe.FillWeight = 200F;
            this.naziv_robe.HeaderText = "Naziv robe ili usluge";
            this.naziv_robe.Name = "naziv_robe";
            this.naziv_robe.ReadOnly = true;
            // 
            // jm
            // 
            this.jm.HeaderText = "JMJ";
            this.jm.Name = "jm";
            this.jm.ReadOnly = true;
            // 
            // kolicina
            // 
            this.kolicina.HeaderText = "Količina";
            this.kolicina.Name = "kolicina";
            // 
            // vpc
            // 
            this.vpc.HeaderText = "VPC";
            this.vpc.Name = "vpc";
            // 
            // porez
            // 
            this.porez.HeaderText = "Porez";
            this.porez.Name = "porez";
            // 
            // mpc
            // 
            this.mpc.HeaderText = "MPC";
            this.mpc.Name = "mpc";
            // 
            // rabat
            // 
            this.rabat.HeaderText = "Rabat";
            this.rabat.Name = "rabat";
            // 
            // rabat_iznos
            // 
            this.rabat_iznos.HeaderText = "Rabat iznos";
            this.rabat_iznos.Name = "rabat_iznos";
            // 
            // cijena_bez_pdv
            // 
            this.cijena_bez_pdv.FillWeight = 120F;
            this.cijena_bez_pdv.HeaderText = "Cijena bez PDV-a";
            this.cijena_bez_pdv.Name = "cijena_bez_pdv";
            // 
            // iznos_bez_pdv
            // 
            this.iznos_bez_pdv.FillWeight = 120F;
            this.iznos_bez_pdv.HeaderText = "Iznos bez PDV-a";
            this.iznos_bez_pdv.Name = "iznos_bez_pdv";
            // 
            // iznos_ukupno
            // 
            this.iznos_ukupno.HeaderText = "Iznos ukupno";
            this.iznos_ukupno.Name = "iznos_ukupno";
            // 
            // frmRobniDobropis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(1066, 621);
            this.Controls.Add(this.btnObrisiStavku);
            this.Controls.Add(this.dgw);
            this.Controls.Add(this.pbTraziStavke);
            this.Controls.Add(this.tbSifraRobe);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnIzlaz);
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.btnSviDobropisi);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnNoviUnos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmRobniDobropis";
            this.Text = "Robni dobropis";
            this.Load += new System.EventHandler(this.frmRobniDobropis_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmRobniDobropis_Paint);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTraziOdrediste)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmGodina)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTraziStavke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNoviUnos;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.Button btnSviDobropisi;
        private System.Windows.Forms.Button btnObrisi;
        private System.Windows.Forms.Button btnIzlaz;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbSkladiste;
        private System.Windows.Forms.NumericUpDown nmGodina;
        private System.Windows.Forms.TextBox tbBrojUnosa;
        private System.Windows.Forms.PictureBox pbTraziOdrediste;
        private System.Windows.Forms.TextBox tbPartnerSifra;
        private System.Windows.Forms.TextBox tbPartnerNaziv;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpDatum;
        private System.Windows.Forms.TextBox tbIzradio;
        private System.Windows.Forms.ComboBox cbMjestoTroska;
        private System.Windows.Forms.TextBox tbOriginalniDokument;
        private System.Windows.Forms.RichTextBox rtbNapomena;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbSifraRobe;
        private System.Windows.Forms.PictureBox pbTraziStavke;
        private System.Windows.Forms.Button btnObrisiStavku;
        private System.Windows.Forms.DataGridView dgw;
        private System.Windows.Forms.DataGridViewTextBoxColumn br;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv_robe;
        private System.Windows.Forms.DataGridViewTextBoxColumn jm;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolicina;
        private System.Windows.Forms.DataGridViewTextBoxColumn vpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn porez;
        private System.Windows.Forms.DataGridViewTextBoxColumn mpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn rabat;
        private System.Windows.Forms.DataGridViewTextBoxColumn rabat_iznos;
        private System.Windows.Forms.DataGridViewTextBoxColumn cijena_bez_pdv;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos_bez_pdv;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos_ukupno;
    }
}