namespace PCPOS.Robno
{
    partial class frmFakturaBezRobe
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFakturaBezRobe));
            this.sa = new PCPOS.Robno.frmFakturaBezRobe.MyDataGrid();
            this.dgw = new PCPOS.Robno.frmFakturaBezRobe.MyDataGrid();
            this.br = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jmj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.porez = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rabat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rabat_iznos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos_ukupno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cijena_bez_pdva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos_bez_pdva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_stavka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ttxBrojFakture = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nmGodinaFakture = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.lblOtpremnica = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.txtOtpremnica = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnPartner = new System.Windows.Forms.PictureBox();
            this.cbNacinPlacanja = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cbValuta = new System.Windows.Forms.ComboBox();
            this.dtpDanaValuta = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.dtpDatumDVO = new System.Windows.Forms.DateTimePicker();
            this.dtpDatum = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtIzradio = new System.Windows.Forms.TextBox();
            this.txtTecaj = new System.Windows.Forms.TextBox();
            this.txtDana = new System.Windows.Forms.TextBox();
            this.txtIznosPredujma = new System.Windows.Forms.TextBox();
            this.txtPartnerNaziv = new System.Windows.Forms.TextBox();
            this.txtSifraOdrediste = new System.Windows.Forms.TextBox();
            this.txtOtprema = new System.Windows.Forms.TextBox();
            this.txtMjestoTroska = new System.Windows.Forms.TextBox();
            this.rtbNapomena = new System.Windows.Forms.TextBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.txtUkupnoIznos = new System.Windows.Forms.TextBox();
            this.txtOsnovicaIznosUkupno = new System.Windows.Forms.TextBox();
            this.txtPdvIznosUkupno = new System.Windows.Forms.TextBox();
            this.lblNaDan = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnDeleteAllFaktura = new System.Windows.Forms.Button();
            this.btnSveFakture = new System.Windows.Forms.Button();
            this.btnNoviUnos = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.lblInsertNovaStavka = new System.Windows.Forms.Label();
            this.lblDeleteStavka = new System.Windows.Forms.Label();
            this.btnOpenRoba = new System.Windows.Forms.PictureBox();
            this.txtSifra_robe = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbBrojPonude = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.sa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmGodinaFakture)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPartner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpenRoba)).BeginInit();
            this.SuspendLayout();
            // 
            // sa
            // 
            this.sa.Location = new System.Drawing.Point(0, 0);
            this.sa.Name = "sa";
            this.sa.Size = new System.Drawing.Size(240, 150);
            this.sa.TabIndex = 0;
            // 
            // dgw
            // 
            this.dgw.AllowUserToAddRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.AliceBlue;
            this.dgw.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgw.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgw.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgw.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.br,
            this.naziv,
            this.jmj,
            this.kolicina,
            this.porez,
            this.vpc,
            this.mpc,
            this.rabat,
            this.rabat_iznos,
            this.iznos_ukupno,
            this.cijena_bez_pdva,
            this.iznos_bez_pdva,
            this.id_stavka});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgw.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgw.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgw.GridColor = System.Drawing.Color.Gainsboro;
            this.dgw.Location = new System.Drawing.Point(12, 430);
            this.dgw.MultiSelect = false;
            this.dgw.Name = "dgw";
            this.dgw.RowHeadersWidth = 30;
            this.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgw.Size = new System.Drawing.Size(969, 168);
            this.dgw.TabIndex = 12;
            this.dgw.TabStop = false;
            this.dgw.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellClick);
            this.dgw.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellEndEdit);
            // 
            // br
            // 
            this.br.FillWeight = 40F;
            this.br.HeaderText = "Br.";
            this.br.Name = "br";
            this.br.Width = 40;
            // 
            // naziv
            // 
            this.naziv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.naziv.FillWeight = 61.10954F;
            this.naziv.HeaderText = "Naziv robe ili usluge";
            this.naziv.MinimumWidth = 130;
            this.naziv.Name = "naziv";
            // 
            // jmj
            // 
            this.jmj.FillWeight = 50F;
            this.jmj.HeaderText = "JMJ";
            this.jmj.Name = "jmj";
            this.jmj.Width = 72;
            // 
            // kolicina
            // 
            this.kolicina.FillWeight = 50F;
            this.kolicina.HeaderText = "Količina";
            this.kolicina.Name = "kolicina";
            this.kolicina.Width = 72;
            // 
            // porez
            // 
            this.porez.FillWeight = 50F;
            this.porez.HeaderText = "Porez";
            this.porez.Name = "porez";
            this.porez.Width = 72;
            // 
            // vpc
            // 
            this.vpc.FillWeight = 70F;
            this.vpc.HeaderText = "VPC";
            this.vpc.Name = "vpc";
            this.vpc.Width = 70;
            // 
            // mpc
            // 
            this.mpc.FillWeight = 70F;
            this.mpc.HeaderText = "MPC";
            this.mpc.Name = "mpc";
            this.mpc.Width = 70;
            // 
            // rabat
            // 
            this.rabat.FillWeight = 60F;
            this.rabat.HeaderText = "Rabat%";
            this.rabat.Name = "rabat";
            this.rabat.Width = 60;
            // 
            // rabat_iznos
            // 
            this.rabat_iznos.HeaderText = "Rabat iznos";
            this.rabat_iznos.Name = "rabat_iznos";
            // 
            // iznos_ukupno
            // 
            this.iznos_ukupno.HeaderText = "Iznos ukupno";
            this.iznos_ukupno.Name = "iznos_ukupno";
            this.iznos_ukupno.ReadOnly = true;
            this.iznos_ukupno.Width = 110;
            // 
            // cijena_bez_pdva
            // 
            this.cijena_bez_pdva.FillWeight = 120F;
            this.cijena_bez_pdva.HeaderText = "Cijena bez pdv-a";
            this.cijena_bez_pdva.Name = "cijena_bez_pdva";
            this.cijena_bez_pdva.ReadOnly = true;
            this.cijena_bez_pdva.Visible = false;
            this.cijena_bez_pdva.Width = 96;
            // 
            // iznos_bez_pdva
            // 
            this.iznos_bez_pdva.FillWeight = 120F;
            this.iznos_bez_pdva.HeaderText = "Iznos bez pdv-a";
            this.iznos_bez_pdva.Name = "iznos_bez_pdva";
            this.iznos_bez_pdva.ReadOnly = true;
            this.iznos_bez_pdva.Visible = false;
            this.iznos_bez_pdva.Width = 96;
            // 
            // id_stavka
            // 
            this.id_stavka.HeaderText = "id_stavka";
            this.id_stavka.Name = "id_stavka";
            this.id_stavka.Visible = false;
            // 
            // ttxBrojFakture
            // 
            this.ttxBrojFakture.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ttxBrojFakture.Location = new System.Drawing.Point(136, 19);
            this.ttxBrojFakture.Name = "ttxBrojFakture";
            this.ttxBrojFakture.Size = new System.Drawing.Size(130, 23);
            this.ttxBrojFakture.TabIndex = 1;
            this.ttxBrojFakture.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.ttxBrojFakture.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown);
            this.ttxBrojFakture.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.ttxBrojFakture.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(32, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Broj fakture:";
            // 
            // nmGodinaFakture
            // 
            this.nmGodinaFakture.BackColor = System.Drawing.Color.White;
            this.nmGodinaFakture.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.nmGodinaFakture.Location = new System.Drawing.Point(315, 19);
            this.nmGodinaFakture.Name = "nmGodinaFakture";
            this.nmGodinaFakture.Size = new System.Drawing.Size(87, 23);
            this.nmGodinaFakture.TabIndex = 2;
            this.nmGodinaFakture.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.nmGodinaFakture.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown_1);
            this.nmGodinaFakture.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.tbBrojPonude);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.nmGodinaFakture);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.lblOtpremnica);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.txtOtpremnica);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ttxBrojFakture);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox1.Location = new System.Drawing.Point(12, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(969, 86);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(272, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 17);
            this.label12.TabIndex = 12;
            this.label12.Text = "God.:";
            // 
            // lblOtpremnica
            // 
            this.lblOtpremnica.AutoSize = true;
            this.lblOtpremnica.Location = new System.Drawing.Point(453, 22);
            this.lblOtpremnica.Name = "lblOtpremnica";
            this.lblOtpremnica.Size = new System.Drawing.Size(85, 17);
            this.lblOtpremnica.TabIndex = 7;
            this.lblOtpremnica.Text = "Otpremnica:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(767, 18);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(43, 24);
            this.button2.TabIndex = 9;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // txtOtpremnica
            // 
            this.txtOtpremnica.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtOtpremnica.Location = new System.Drawing.Point(544, 19);
            this.txtOtpremnica.Name = "txtOtpremnica";
            this.txtOtpremnica.Size = new System.Drawing.Size(217, 23);
            this.txtOtpremnica.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.btnPartner);
            this.groupBox2.Controls.Add(this.cbNacinPlacanja);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.cbValuta);
            this.groupBox2.Controls.Add(this.dtpDanaValuta);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.dtpDatumDVO);
            this.groupBox2.Controls.Add(this.dtpDatum);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label28);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.txtIzradio);
            this.groupBox2.Controls.Add(this.txtTecaj);
            this.groupBox2.Controls.Add(this.txtDana);
            this.groupBox2.Controls.Add(this.txtIznosPredujma);
            this.groupBox2.Controls.Add(this.txtPartnerNaziv);
            this.groupBox2.Controls.Add(this.txtSifraOdrediste);
            this.groupBox2.Controls.Add(this.txtOtprema);
            this.groupBox2.Controls.Add(this.txtMjestoTroska);
            this.groupBox2.Controls.Add(this.rtbNapomena);
            this.groupBox2.Controls.Add(this.txtModel);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox2.Location = new System.Drawing.Point(12, 144);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(969, 246);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // btnPartner
            // 
            this.btnPartner.BackColor = System.Drawing.Color.Transparent;
            this.btnPartner.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPartner.Image = ((System.Drawing.Image)(resources.GetObject("btnPartner.Image")));
            this.btnPartner.Location = new System.Drawing.Point(188, 22);
            this.btnPartner.Name = "btnPartner";
            this.btnPartner.Size = new System.Drawing.Size(31, 28);
            this.btnPartner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnPartner.TabIndex = 557;
            this.btnPartner.TabStop = false;
            this.btnPartner.Click += new System.EventHandler(this.btnPartner_Click);
            // 
            // cbNacinPlacanja
            // 
            this.cbNacinPlacanja.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbNacinPlacanja.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbNacinPlacanja.BackColor = System.Drawing.Color.White;
            this.cbNacinPlacanja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNacinPlacanja.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbNacinPlacanja.FormattingEnabled = true;
            this.cbNacinPlacanja.Location = new System.Drawing.Point(544, 76);
            this.cbNacinPlacanja.Name = "cbNacinPlacanja";
            this.cbNacinPlacanja.Size = new System.Drawing.Size(266, 24);
            this.cbNacinPlacanja.TabIndex = 22;
            this.cbNacinPlacanja.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbNacinPlacanja.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown_1);
            this.cbNacinPlacanja.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Location = new System.Drawing.Point(456, 77);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 17);
            this.label15.TabIndex = 21;
            this.label15.Text = "Način plać.:";
            // 
            // cbValuta
            // 
            this.cbValuta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbValuta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbValuta.BackColor = System.Drawing.Color.White;
            this.cbValuta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbValuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbValuta.FormattingEnabled = true;
            this.cbValuta.Location = new System.Drawing.Point(136, 180);
            this.cbValuta.Name = "cbValuta";
            this.cbValuta.Size = new System.Drawing.Size(266, 24);
            this.cbValuta.TabIndex = 14;
            this.cbValuta.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbValuta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown_1);
            this.cbValuta.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // dtpDanaValuta
            // 
            this.dtpDanaValuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDanaValuta.Location = new System.Drawing.Point(136, 127);
            this.dtpDanaValuta.Name = "dtpDanaValuta";
            this.dtpDanaValuta.Size = new System.Drawing.Size(266, 23);
            this.dtpDanaValuta.TabIndex = 10;
            this.dtpDanaValuta.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.dtpDanaValuta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown_1);
            this.dtpDanaValuta.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Location = new System.Drawing.Point(76, 156);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 17);
            this.label13.TabIndex = 11;
            this.label13.Text = "Izradio:";
            // 
            // dtpDatumDVO
            // 
            this.dtpDatumDVO.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDatumDVO.Location = new System.Drawing.Point(136, 75);
            this.dtpDatumDVO.Name = "dtpDatumDVO";
            this.dtpDatumDVO.Size = new System.Drawing.Size(266, 23);
            this.dtpDatumDVO.TabIndex = 6;
            this.dtpDatumDVO.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.dtpDatumDVO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown_1);
            this.dtpDatumDVO.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // dtpDatum
            // 
            this.dtpDatum.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDatum.Location = new System.Drawing.Point(136, 49);
            this.dtpDatum.Name = "dtpDatum";
            this.dtpDatum.Size = new System.Drawing.Size(266, 23);
            this.dtpDatum.TabIndex = 4;
            this.dtpDatum.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.dtpDatum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown_1);
            this.dtpDatum.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(457, 155);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 17);
            this.label8.TabIndex = 27;
            this.label8.Text = "Napomena:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Location = new System.Drawing.Point(83, 208);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(47, 17);
            this.label20.TabIndex = 15;
            this.label20.Text = "Tečaj:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(42, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 17);
            this.label7.TabIndex = 9;
            this.label7.Text = "Dana valute:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Location = new System.Drawing.Point(430, 129);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(108, 17);
            this.label26.TabIndex = 25;
            this.label26.Text = "Iznos predujma:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Location = new System.Drawing.Point(78, 182);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(52, 17);
            this.label19.TabIndex = 13;
            this.label19.Text = "Valuta:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(84, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "Dana:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(43, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "Datum DVO:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Location = new System.Drawing.Point(471, 103);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(67, 17);
            this.label24.TabIndex = 23;
            this.label24.Text = "Otprema:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(77, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Datum:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Location = new System.Drawing.Point(60, 25);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(70, 17);
            this.label28.TabIndex = 0;
            this.label28.Text = "Odredište";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(442, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 17);
            this.label2.TabIndex = 19;
            this.label2.Text = "Mjesto troška:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Location = new System.Drawing.Point(488, 25);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(50, 17);
            this.label14.TabIndex = 17;
            this.label14.Text = "Model:";
            // 
            // txtIzradio
            // 
            this.txtIzradio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtIzradio.Location = new System.Drawing.Point(136, 153);
            this.txtIzradio.Name = "txtIzradio";
            this.txtIzradio.ReadOnly = true;
            this.txtIzradio.Size = new System.Drawing.Size(266, 23);
            this.txtIzradio.TabIndex = 12;
            this.txtIzradio.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtIzradio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown_1);
            this.txtIzradio.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtTecaj
            // 
            this.txtTecaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtTecaj.Location = new System.Drawing.Point(136, 208);
            this.txtTecaj.Name = "txtTecaj";
            this.txtTecaj.ReadOnly = true;
            this.txtTecaj.Size = new System.Drawing.Size(266, 23);
            this.txtTecaj.TabIndex = 16;
            this.txtTecaj.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtTecaj.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown_1);
            this.txtTecaj.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtDana
            // 
            this.txtDana.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtDana.Location = new System.Drawing.Point(136, 101);
            this.txtDana.Name = "txtDana";
            this.txtDana.Size = new System.Drawing.Size(266, 23);
            this.txtDana.TabIndex = 8;
            this.txtDana.Text = "0";
            this.txtDana.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtDana.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown_1);
            this.txtDana.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtDana.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtIznosPredujma
            // 
            this.txtIznosPredujma.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtIznosPredujma.Location = new System.Drawing.Point(544, 131);
            this.txtIznosPredujma.Name = "txtIznosPredujma";
            this.txtIznosPredujma.ReadOnly = true;
            this.txtIznosPredujma.Size = new System.Drawing.Size(266, 23);
            this.txtIznosPredujma.TabIndex = 26;
            this.txtIznosPredujma.Text = "0,00";
            this.txtIznosPredujma.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtIznosPredujma.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown_1);
            this.txtIznosPredujma.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtPartnerNaziv
            // 
            this.txtPartnerNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPartnerNaziv.Location = new System.Drawing.Point(217, 22);
            this.txtPartnerNaziv.Name = "txtPartnerNaziv";
            this.txtPartnerNaziv.ReadOnly = true;
            this.txtPartnerNaziv.Size = new System.Drawing.Size(185, 23);
            this.txtPartnerNaziv.TabIndex = 2;
            this.txtPartnerNaziv.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtPartnerNaziv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown_1);
            this.txtPartnerNaziv.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtSifraOdrediste
            // 
            this.txtSifraOdrediste.BackColor = System.Drawing.Color.White;
            this.txtSifraOdrediste.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSifraOdrediste.Location = new System.Drawing.Point(136, 22);
            this.txtSifraOdrediste.Name = "txtSifraOdrediste";
            this.txtSifraOdrediste.Size = new System.Drawing.Size(53, 23);
            this.txtSifraOdrediste.TabIndex = 1;
            this.txtSifraOdrediste.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtSifraOdrediste.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown_1);
            this.txtSifraOdrediste.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.provjera_KeyPress);
            this.txtSifraOdrediste.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtOtprema
            // 
            this.txtOtprema.BackColor = System.Drawing.Color.White;
            this.txtOtprema.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtOtprema.Location = new System.Drawing.Point(544, 104);
            this.txtOtprema.Name = "txtOtprema";
            this.txtOtprema.Size = new System.Drawing.Size(266, 23);
            this.txtOtprema.TabIndex = 24;
            this.txtOtprema.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtOtprema.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown_1);
            this.txtOtprema.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtMjestoTroska
            // 
            this.txtMjestoTroska.BackColor = System.Drawing.Color.White;
            this.txtMjestoTroska.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtMjestoTroska.Location = new System.Drawing.Point(544, 49);
            this.txtMjestoTroska.Name = "txtMjestoTroska";
            this.txtMjestoTroska.Size = new System.Drawing.Size(266, 23);
            this.txtMjestoTroska.TabIndex = 20;
            this.txtMjestoTroska.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtMjestoTroska.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown_1);
            this.txtMjestoTroska.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // rtbNapomena
            // 
            this.rtbNapomena.BackColor = System.Drawing.Color.White;
            this.rtbNapomena.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.rtbNapomena.Location = new System.Drawing.Point(544, 158);
            this.rtbNapomena.Multiline = true;
            this.rtbNapomena.Name = "rtbNapomena";
            this.rtbNapomena.Size = new System.Drawing.Size(266, 73);
            this.rtbNapomena.TabIndex = 28;
            this.rtbNapomena.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.rtbNapomena.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown_1);
            this.rtbNapomena.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtModel
            // 
            this.txtModel.BackColor = System.Drawing.Color.White;
            this.txtModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtModel.Location = new System.Drawing.Point(544, 22);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(266, 23);
            this.txtModel.TabIndex = 18;
            this.txtModel.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtModel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttxBrojFakture_KeyDown_1);
            this.txtModel.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtUkupnoIznos
            // 
            this.txtUkupnoIznos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUkupnoIznos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtUkupnoIznos.Location = new System.Drawing.Point(695, 602);
            this.txtUkupnoIznos.Name = "txtUkupnoIznos";
            this.txtUkupnoIznos.ReadOnly = true;
            this.txtUkupnoIznos.Size = new System.Drawing.Size(286, 23);
            this.txtUkupnoIznos.TabIndex = 16;
            this.txtUkupnoIznos.Text = "Ukupno sa PDV-om:";
            // 
            // txtOsnovicaIznosUkupno
            // 
            this.txtOsnovicaIznosUkupno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOsnovicaIznosUkupno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtOsnovicaIznosUkupno.Location = new System.Drawing.Point(420, 602);
            this.txtOsnovicaIznosUkupno.Name = "txtOsnovicaIznosUkupno";
            this.txtOsnovicaIznosUkupno.ReadOnly = true;
            this.txtOsnovicaIznosUkupno.Size = new System.Drawing.Size(266, 23);
            this.txtOsnovicaIznosUkupno.TabIndex = 15;
            this.txtOsnovicaIznosUkupno.Text = "Bez PDV-a:";
            // 
            // txtPdvIznosUkupno
            // 
            this.txtPdvIznosUkupno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPdvIznosUkupno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtPdvIznosUkupno.Location = new System.Drawing.Point(229, 602);
            this.txtPdvIznosUkupno.Name = "txtPdvIznosUkupno";
            this.txtPdvIznosUkupno.ReadOnly = true;
            this.txtPdvIznosUkupno.Size = new System.Drawing.Size(185, 23);
            this.txtPdvIznosUkupno.TabIndex = 14;
            this.txtPdvIznosUkupno.Text = "PDV:";
            // 
            // lblNaDan
            // 
            this.lblNaDan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNaDan.AutoSize = true;
            this.lblNaDan.BackColor = System.Drawing.Color.Transparent;
            this.lblNaDan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblNaDan.Location = new System.Drawing.Point(12, 605);
            this.lblNaDan.Name = "lblNaDan";
            this.lblNaDan.Size = new System.Drawing.Size(57, 17);
            this.lblNaDan.TabIndex = 13;
            this.lblNaDan.Text = "NaDan";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.Image = global::PCPOS.Properties.Resources.Actions_application_exit_icon;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(851, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 40);
            this.button1.TabIndex = 5;
            this.button1.Text = "Izlaz      ";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDeleteAllFaktura
            // 
            this.btnDeleteAllFaktura.Enabled = false;
            this.btnDeleteAllFaktura.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnDeleteAllFaktura.Image = global::PCPOS.Properties.Resources.Recyclebin_Empty;
            this.btnDeleteAllFaktura.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteAllFaktura.Location = new System.Drawing.Point(556, 12);
            this.btnDeleteAllFaktura.Name = "btnDeleteAllFaktura";
            this.btnDeleteAllFaktura.Size = new System.Drawing.Size(130, 40);
            this.btnDeleteAllFaktura.TabIndex = 4;
            this.btnDeleteAllFaktura.Text = "Obriši fakturu";
            this.btnDeleteAllFaktura.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteAllFaktura.UseVisualStyleBackColor = true;
            this.btnDeleteAllFaktura.Click += new System.EventHandler(this.btnDeleteAllFaktura_Click);
            // 
            // btnSveFakture
            // 
            this.btnSveFakture.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSveFakture.Image = global::PCPOS.Properties.Resources._10591;
            this.btnSveFakture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSveFakture.Location = new System.Drawing.Point(420, 12);
            this.btnSveFakture.Name = "btnSveFakture";
            this.btnSveFakture.Size = new System.Drawing.Size(130, 40);
            this.btnSveFakture.TabIndex = 3;
            this.btnSveFakture.Text = "Sve fakture  ";
            this.btnSveFakture.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSveFakture.UseVisualStyleBackColor = true;
            this.btnSveFakture.Click += new System.EventHandler(this.btnSveFakture_Click);
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
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click_1);
            // 
            // lblInsertNovaStavka
            // 
            this.lblInsertNovaStavka.AutoSize = true;
            this.lblInsertNovaStavka.BackColor = System.Drawing.Color.Transparent;
            this.lblInsertNovaStavka.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblInsertNovaStavka.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblInsertNovaStavka.ForeColor = System.Drawing.Color.DarkRed;
            this.lblInsertNovaStavka.Location = new System.Drawing.Point(9, 403);
            this.lblInsertNovaStavka.Name = "lblInsertNovaStavka";
            this.lblInsertNovaStavka.Size = new System.Drawing.Size(170, 17);
            this.lblInsertNovaStavka.TabIndex = 8;
            this.lblInsertNovaStavka.Text = "Nova stavka (INSERT)";
            this.lblInsertNovaStavka.Click += new System.EventHandler(this.label3_Click);
            // 
            // lblDeleteStavka
            // 
            this.lblDeleteStavka.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDeleteStavka.AutoSize = true;
            this.lblDeleteStavka.BackColor = System.Drawing.Color.Transparent;
            this.lblDeleteStavka.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDeleteStavka.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDeleteStavka.ForeColor = System.Drawing.Color.DarkRed;
            this.lblDeleteStavka.Location = new System.Drawing.Point(801, 403);
            this.lblDeleteStavka.Name = "lblDeleteStavka";
            this.lblDeleteStavka.Size = new System.Drawing.Size(180, 17);
            this.lblDeleteStavka.TabIndex = 9;
            this.lblDeleteStavka.Text = "Obriši stavku (DELETE)";
            this.lblDeleteStavka.Click += new System.EventHandler(this.label9_Click);
            // 
            // btnOpenRoba
            // 
            this.btnOpenRoba.BackColor = System.Drawing.Color.Transparent;
            this.btnOpenRoba.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenRoba.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenRoba.Image")));
            this.btnOpenRoba.Location = new System.Drawing.Point(556, 396);
            this.btnOpenRoba.Name = "btnOpenRoba";
            this.btnOpenRoba.Size = new System.Drawing.Size(39, 31);
            this.btnOpenRoba.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnOpenRoba.TabIndex = 41;
            this.btnOpenRoba.TabStop = false;
            this.btnOpenRoba.Click += new System.EventHandler(this.btnOpenRoba_Click);
            // 
            // txtSifra_robe
            // 
            this.txtSifra_robe.BackColor = System.Drawing.Color.White;
            this.txtSifra_robe.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSifra_robe.Location = new System.Drawing.Point(420, 400);
            this.txtSifra_robe.Name = "txtSifra_robe";
            this.txtSifra_robe.Size = new System.Drawing.Size(130, 23);
            this.txtSifra_robe.TabIndex = 11;
            this.txtSifra_robe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifra_robe_KeyDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label10.Location = new System.Drawing.Point(294, 403);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 17);
            this.label10.TabIndex = 10;
            this.label10.Text = "Šifra robe/usluge:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(453, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "Preuzmi stavke iz ponude:";
            // 
            // tbBrojPonude
            // 
            this.tbBrojPonude.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.tbBrojPonude.Location = new System.Drawing.Point(633, 48);
            this.tbBrojPonude.Name = "tbBrojPonude";
            this.tbBrojPonude.Size = new System.Drawing.Size(128, 23);
            this.tbBrojPonude.TabIndex = 14;
            this.tbBrojPonude.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbBrojPonude.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbBrojPonude_KeyDown);
            // 
            // frmFakturaBezRobe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(993, 631);
            this.Controls.Add(this.btnOpenRoba);
            this.Controls.Add(this.txtSifra_robe);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDeleteAllFaktura);
            this.Controls.Add(this.btnSveFakture);
            this.Controls.Add(this.btnNoviUnos);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.lblNaDan);
            this.Controls.Add(this.txtPdvIznosUkupno);
            this.Controls.Add(this.txtOsnovicaIznosUkupno);
            this.Controls.Add(this.txtUkupnoIznos);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblDeleteStavka);
            this.Controls.Add(this.lblInsertNovaStavka);
            this.Controls.Add(this.dgw);
            this.Name = "frmFakturaBezRobe";
            this.Text = "Faktura";
            this.Load += new System.EventHandler(this.frmFaktura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmGodinaFakture)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPartner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpenRoba)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nmGodinaFakture;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbValuta;
        private System.Windows.Forms.DateTimePicker dtpDanaValuta;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtpDatum;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtTecaj;
        private System.Windows.Forms.TextBox txtDana;
        private System.Windows.Forms.TextBox txtPartnerNaziv;
        private System.Windows.Forms.TextBox txtSifraOdrediste;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.TextBox txtIznosPredujma;
        private System.Windows.Forms.TextBox txtUkupnoIznos;
        private System.Windows.Forms.TextBox txtOsnovicaIznosUkupno;
        private System.Windows.Forms.TextBox txtPdvIznosUkupno;
        private System.Windows.Forms.TextBox txtIzradio;
        private System.Windows.Forms.Label lblNaDan;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Button btnNoviUnos;
        private System.Windows.Forms.Button btnSveFakture;
        public System.Windows.Forms.TextBox ttxBrojFakture;
        private System.Windows.Forms.Button btnDeleteAllFaktura;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbNacinPlacanja;
        private System.Windows.Forms.Label label15;
        private frmFakturaBezRobe.MyDataGrid sa;
        private frmFakturaBezRobe.MyDataGrid dgw;
        private System.Windows.Forms.PictureBox btnPartner;
        private System.Windows.Forms.DateTimePicker dtpDatumDVO;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblInsertNovaStavka;
        private System.Windows.Forms.Label lblDeleteStavka;
        private System.Windows.Forms.TextBox txtOtprema;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMjestoTroska;
        private System.Windows.Forms.TextBox rtbNapomena;
        private System.Windows.Forms.DataGridViewTextBoxColumn br;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn jmj;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolicina;
        private System.Windows.Forms.DataGridViewTextBoxColumn porez;
        private System.Windows.Forms.DataGridViewTextBoxColumn vpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn mpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn rabat;
        private System.Windows.Forms.DataGridViewTextBoxColumn rabat_iznos;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos_ukupno;
        private System.Windows.Forms.DataGridViewTextBoxColumn cijena_bez_pdva;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos_bez_pdva;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_stavka;
        private System.Windows.Forms.PictureBox btnOpenRoba;
        private System.Windows.Forms.TextBox txtSifra_robe;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblOtpremnica;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtOtpremnica;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbBrojPonude;
    }
}