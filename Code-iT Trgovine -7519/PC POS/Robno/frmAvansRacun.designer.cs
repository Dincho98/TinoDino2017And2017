namespace PCPOS
{
    partial class frmAvansRacun
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
            this.dtpDatumDvo = new System.Windows.Forms.DateTimePicker();
            this.dtpDatum = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpDatumValute = new System.Windows.Forms.DateTimePicker();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.txtOpis = new System.Windows.Forms.TextBox();
            this.txtPartner = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtAvansirati = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.textUkupno = new System.Windows.Forms.TextBox();
            this.textPorez25 = new System.Windows.Forms.TextBox();
            this.textPorez10 = new System.Windows.Forms.TextBox();
            this.textOsnov25 = new System.Windows.Forms.TextBox();
            this.textOsnov10 = new System.Windows.Forms.TextBox();
            this.textNeoporezivo = new System.Windows.Forms.TextBox();
            this.textNultaStp = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.rtbOpis = new System.Windows.Forms.TextBox();
            this.cbNacinPlacanja = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtSifraNacinPlacanja = new System.Windows.Forms.TextBox();
            this.cbZiroRacun = new System.Windows.Forms.ComboBox();
            this.cbValuta = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtIzradio = new System.Windows.Forms.TextBox();
            this.txtTecaj = new System.Windows.Forms.TextBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.cbPDV = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.cbVD = new System.Windows.Forms.ComboBox();
            this.txtBrojAvans = new System.Windows.Forms.TextBox();
            this.numGodina = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.btnIzlaz = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.btnNoviUnos = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGodina)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpDatumDvo
            // 
            this.dtpDatumDvo.CustomFormat = "dd.MM.yyyy. HH:mm:ss";
            this.dtpDatumDvo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatumDvo.Location = new System.Drawing.Point(533, 83);
            this.dtpDatumDvo.Name = "dtpDatumDvo";
            this.dtpDatumDvo.Size = new System.Drawing.Size(206, 23);
            this.dtpDatumDvo.TabIndex = 5;
            this.dtpDatumDvo.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.dtpDatumDvo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownEnter);
            this.dtpDatumDvo.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // dtpDatum
            // 
            this.dtpDatum.CustomFormat = "dd.MM.yyyy. HH:mm:ss";
            this.dtpDatum.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatum.Location = new System.Drawing.Point(100, 83);
            this.dtpDatum.Name = "dtpDatum";
            this.dtpDatum.Size = new System.Drawing.Size(206, 23);
            this.dtpDatum.TabIndex = 7;
            this.dtpDatum.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.dtpDatum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownEnter);
            this.dtpDatum.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.dtpDatumValute);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.txtOpis);
            this.groupBox1.Controls.Add(this.txtPartner);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.txtAvansirati);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.rtbOpis);
            this.groupBox1.Controls.Add(this.cbNacinPlacanja);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtSifraNacinPlacanja);
            this.groupBox1.Controls.Add(this.cbZiroRacun);
            this.groupBox1.Controls.Add(this.cbValuta);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtIzradio);
            this.groupBox1.Controls.Add(this.txtTecaj);
            this.groupBox1.Controls.Add(this.txtModel);
            this.groupBox1.Controls.Add(this.dtpDatumDvo);
            this.groupBox1.Controls.Add(this.dtpDatum);
            this.groupBox1.Controls.Add(this.cbPDV);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox1.Location = new System.Drawing.Point(12, 126);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(768, 422);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // dtpDatumValute
            // 
            this.dtpDatumValute.CustomFormat = "dd.MM.yyyy. HH:mm:ss";
            this.dtpDatumValute.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatumValute.Location = new System.Drawing.Point(101, 110);
            this.dtpDatumValute.Name = "dtpDatumValute";
            this.dtpDatumValute.Size = new System.Drawing.Size(206, 23);
            this.dtpDatumValute.TabIndex = 29;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label22.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label22.Location = new System.Drawing.Point(10, 113);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(95, 17);
            this.label22.TabIndex = 28;
            this.label22.Text = "Datum valute:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(10, 57);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(41, 17);
            this.label21.TabIndex = 27;
            this.label21.Text = "Opis:";
            // 
            // txtOpis
            // 
            this.txtOpis.Location = new System.Drawing.Point(100, 54);
            this.txtOpis.MaxLength = 160;
            this.txtOpis.Name = "txtOpis";
            this.txtOpis.Size = new System.Drawing.Size(639, 23);
            this.txtOpis.TabIndex = 26;
            // 
            // txtPartner
            // 
            this.txtPartner.Location = new System.Drawing.Point(231, 25);
            this.txtPartner.Name = "txtPartner";
            this.txtPartner.ReadOnly = true;
            this.txtPartner.Size = new System.Drawing.Size(508, 23);
            this.txtPartner.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::PCPOS.Properties.Resources._1059;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(170, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(55, 23);
            this.button2.TabIndex = 2;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtAvansirati
            // 
            this.txtAvansirati.Location = new System.Drawing.Point(100, 25);
            this.txtAvansirati.Name = "txtAvansirati";
            this.txtAvansirati.Size = new System.Drawing.Size(64, 23);
            this.txtAvansirati.TabIndex = 1;
            this.txtAvansirati.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtAvansirati.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAvansirati_KeyDown);
            this.txtAvansirati.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(10, 28);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(74, 17);
            this.label18.TabIndex = 0;
            this.label18.Text = "Avansirati:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox8);
            this.groupBox3.Controls.Add(this.textBox9);
            this.groupBox3.Controls.Add(this.textBox10);
            this.groupBox3.Controls.Add(this.textBox11);
            this.groupBox3.Controls.Add(this.textBox12);
            this.groupBox3.Controls.Add(this.textBox13);
            this.groupBox3.Controls.Add(this.textBox14);
            this.groupBox3.Controls.Add(this.textUkupno);
            this.groupBox3.Controls.Add(this.textPorez25);
            this.groupBox3.Controls.Add(this.textPorez10);
            this.groupBox3.Controls.Add(this.textOsnov25);
            this.groupBox3.Controls.Add(this.textOsnov10);
            this.groupBox3.Controls.Add(this.textNeoporezivo);
            this.groupBox3.Controls.Add(this.textNultaStp);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(341, 110);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(398, 216);
            this.groupBox3.TabIndex = 25;
            this.groupBox3.TabStop = false;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(268, 184);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(100, 23);
            this.textBox8.TabIndex = 14;
            this.textBox8.Visible = false;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(268, 154);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(100, 23);
            this.textBox9.TabIndex = 14;
            this.textBox9.Visible = false;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(268, 133);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(100, 23);
            this.textBox10.TabIndex = 14;
            this.textBox10.Visible = false;
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(268, 95);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(100, 23);
            this.textBox11.TabIndex = 14;
            this.textBox11.Visible = false;
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(268, 74);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(100, 23);
            this.textBox12.TabIndex = 14;
            this.textBox12.Visible = false;
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(268, 44);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(100, 23);
            this.textBox13.TabIndex = 14;
            this.textBox13.Visible = false;
            // 
            // textBox14
            // 
            this.textBox14.Location = new System.Drawing.Point(268, 21);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(100, 23);
            this.textBox14.TabIndex = 14;
            this.textBox14.Visible = false;
            // 
            // textUkupno
            // 
            this.textUkupno.Location = new System.Drawing.Point(162, 184);
            this.textUkupno.Name = "textUkupno";
            this.textUkupno.Size = new System.Drawing.Size(100, 23);
            this.textUkupno.TabIndex = 1;
            this.textUkupno.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.textUkupno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textUkupno_KeyDown);
            this.textUkupno.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // textPorez25
            // 
            this.textPorez25.Location = new System.Drawing.Point(162, 154);
            this.textPorez25.Name = "textPorez25";
            this.textPorez25.Size = new System.Drawing.Size(100, 23);
            this.textPorez25.TabIndex = 7;
            this.textPorez25.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.textPorez25.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textPorez25_KeyDown);
            this.textPorez25.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // textPorez10
            // 
            this.textPorez10.Location = new System.Drawing.Point(162, 133);
            this.textPorez10.Name = "textPorez10";
            this.textPorez10.Size = new System.Drawing.Size(100, 23);
            this.textPorez10.TabIndex = 6;
            this.textPorez10.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.textPorez10.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textPorez10_KeyDown);
            this.textPorez10.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // textOsnov25
            // 
            this.textOsnov25.Location = new System.Drawing.Point(162, 95);
            this.textOsnov25.Name = "textOsnov25";
            this.textOsnov25.Size = new System.Drawing.Size(100, 23);
            this.textOsnov25.TabIndex = 5;
            this.textOsnov25.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.textOsnov25.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textOsnov25_KeyDown);
            this.textOsnov25.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // textOsnov10
            // 
            this.textOsnov10.Location = new System.Drawing.Point(162, 74);
            this.textOsnov10.Name = "textOsnov10";
            this.textOsnov10.Size = new System.Drawing.Size(100, 23);
            this.textOsnov10.TabIndex = 4;
            this.textOsnov10.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.textOsnov10.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textOsnov10_KeyDown);
            this.textOsnov10.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // textNeoporezivo
            // 
            this.textNeoporezivo.Location = new System.Drawing.Point(162, 44);
            this.textNeoporezivo.Name = "textNeoporezivo";
            this.textNeoporezivo.Size = new System.Drawing.Size(100, 23);
            this.textNeoporezivo.TabIndex = 3;
            this.textNeoporezivo.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.textNeoporezivo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textNeoporezivo_KeyDown);
            this.textNeoporezivo.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // textNultaStp
            // 
            this.textNultaStp.Location = new System.Drawing.Point(162, 21);
            this.textNultaStp.Name = "textNultaStp";
            this.textNultaStp.Size = new System.Drawing.Size(100, 23);
            this.textNultaStp.TabIndex = 2;
            this.textNultaStp.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.textNultaStp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textNultaStp_KeyDown);
            this.textNultaStp.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label17.ForeColor = System.Drawing.Color.Maroon;
            this.label17.Location = new System.Drawing.Point(48, 187);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(68, 17);
            this.label17.TabIndex = 0;
            this.label17.Text = "Ukupno:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label16.ForeColor = System.Drawing.Color.Maroon;
            this.label16.Location = new System.Drawing.Point(48, 157);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(69, 17);
            this.label16.TabIndex = 0;
            this.label16.Text = "Porez 25:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label12.ForeColor = System.Drawing.Color.Maroon;
            this.label12.Location = new System.Drawing.Point(48, 136);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 17);
            this.label12.TabIndex = 0;
            this.label12.Text = "Porez 10:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label11.ForeColor = System.Drawing.Color.Maroon;
            this.label11.Location = new System.Drawing.Point(48, 98);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 17);
            this.label11.TabIndex = 0;
            this.label11.Text = "Osnovica 25:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label10.ForeColor = System.Drawing.Color.Maroon;
            this.label10.Location = new System.Drawing.Point(48, 77);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 17);
            this.label10.TabIndex = 0;
            this.label10.Text = "Osnovica 10:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label9.ForeColor = System.Drawing.Color.Maroon;
            this.label9.Location = new System.Drawing.Point(48, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 17);
            this.label9.TabIndex = 0;
            this.label9.Text = "Neoporezivo:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label7.ForeColor = System.Drawing.Color.Maroon;
            this.label7.Location = new System.Drawing.Point(48, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "Nulta stopa:";
            // 
            // rtbOpis
            // 
            this.rtbOpis.Location = new System.Drawing.Point(100, 332);
            this.rtbOpis.Multiline = true;
            this.rtbOpis.Name = "rtbOpis";
            this.rtbOpis.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.rtbOpis.Size = new System.Drawing.Size(639, 76);
            this.rtbOpis.TabIndex = 24;
            this.rtbOpis.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.rtbOpis.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownEnter);
            this.rtbOpis.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // cbNacinPlacanja
            // 
            this.cbNacinPlacanja.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbNacinPlacanja.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbNacinPlacanja.BackColor = System.Drawing.Color.White;
            this.cbNacinPlacanja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNacinPlacanja.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbNacinPlacanja.FormattingEnabled = true;
            this.cbNacinPlacanja.Location = new System.Drawing.Point(143, 302);
            this.cbNacinPlacanja.Name = "cbNacinPlacanja";
            this.cbNacinPlacanja.Size = new System.Drawing.Size(164, 24);
            this.cbNacinPlacanja.TabIndex = 22;
            this.cbNacinPlacanja.SelectedIndexChanged += new System.EventHandler(this.cbNacinPlacanja_SelectedIndexChanged);
            this.cbNacinPlacanja.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbNacinPlacanja.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownEnter);
            this.cbNacinPlacanja.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label15.Location = new System.Drawing.Point(10, 306);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 17);
            this.label15.TabIndex = 20;
            this.label15.Text = "Način plać.:";
            // 
            // txtSifraNacinPlacanja
            // 
            this.txtSifraNacinPlacanja.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txtSifraNacinPlacanja.Location = new System.Drawing.Point(99, 302);
            this.txtSifraNacinPlacanja.Name = "txtSifraNacinPlacanja";
            this.txtSifraNacinPlacanja.ReadOnly = true;
            this.txtSifraNacinPlacanja.Size = new System.Drawing.Size(44, 24);
            this.txtSifraNacinPlacanja.TabIndex = 21;
            this.txtSifraNacinPlacanja.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtSifraNacinPlacanja.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraNacinPlacanja_KeyDown);
            this.txtSifraNacinPlacanja.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // cbZiroRacun
            // 
            this.cbZiroRacun.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbZiroRacun.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbZiroRacun.BackColor = System.Drawing.Color.White;
            this.cbZiroRacun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbZiroRacun.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbZiroRacun.FormattingEnabled = true;
            this.cbZiroRacun.Location = new System.Drawing.Point(99, 219);
            this.cbZiroRacun.Name = "cbZiroRacun";
            this.cbZiroRacun.Size = new System.Drawing.Size(208, 24);
            this.cbZiroRacun.TabIndex = 15;
            this.cbZiroRacun.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbZiroRacun.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownEnter);
            this.cbZiroRacun.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // cbValuta
            // 
            this.cbValuta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbValuta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbValuta.BackColor = System.Drawing.Color.White;
            this.cbValuta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbValuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbValuta.FormattingEnabled = true;
            this.cbValuta.Location = new System.Drawing.Point(99, 247);
            this.cbValuta.Name = "cbValuta";
            this.cbValuta.Size = new System.Drawing.Size(208, 24);
            this.cbValuta.TabIndex = 17;
            this.cbValuta.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbValuta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownEnter);
            this.cbValuta.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label13.Location = new System.Drawing.Point(10, 168);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 17);
            this.label13.TabIndex = 10;
            this.label13.Text = "Izradio:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label8.Location = new System.Drawing.Point(9, 362);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 17);
            this.label8.TabIndex = 23;
            this.label8.Text = "Napomena:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label20.Location = new System.Drawing.Point(10, 278);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(47, 17);
            this.label20.TabIndex = 18;
            this.label20.Text = "Tečaj:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label19.Location = new System.Drawing.Point(10, 251);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(52, 17);
            this.label19.TabIndex = 16;
            this.label19.Text = "Valuta:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label6.Location = new System.Drawing.Point(10, 223);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "Žiro račun";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label14.Location = new System.Drawing.Point(10, 195);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(50, 17);
            this.label14.TabIndex = 12;
            this.label14.Text = "Model:";
            // 
            // txtIzradio
            // 
            this.txtIzradio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtIzradio.Location = new System.Drawing.Point(99, 165);
            this.txtIzradio.Name = "txtIzradio";
            this.txtIzradio.ReadOnly = true;
            this.txtIzradio.Size = new System.Drawing.Size(208, 23);
            this.txtIzradio.TabIndex = 11;
            this.txtIzradio.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtIzradio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownEnter);
            this.txtIzradio.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtTecaj
            // 
            this.txtTecaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtTecaj.Location = new System.Drawing.Point(99, 275);
            this.txtTecaj.Name = "txtTecaj";
            this.txtTecaj.ReadOnly = true;
            this.txtTecaj.Size = new System.Drawing.Size(208, 23);
            this.txtTecaj.TabIndex = 19;
            this.txtTecaj.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtTecaj.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownEnter);
            this.txtTecaj.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtModel
            // 
            this.txtModel.BackColor = System.Drawing.Color.White;
            this.txtModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtModel.Location = new System.Drawing.Point(99, 192);
            this.txtModel.MaxLength = 10;
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(208, 23);
            this.txtModel.TabIndex = 13;
            this.txtModel.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtModel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownEnter);
            this.txtModel.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // cbPDV
            // 
            this.cbPDV.FormattingEnabled = true;
            this.cbPDV.Location = new System.Drawing.Point(100, 137);
            this.cbPDV.Name = "cbPDV";
            this.cbPDV.Size = new System.Drawing.Size(63, 24);
            this.cbPDV.TabIndex = 9;
            this.cbPDV.SelectedIndexChanged += new System.EventHandler(this.cbPDV_SelectedIndexChanged);
            this.cbPDV.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbPDV.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownEnter);
            this.cbPDV.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(10, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Stopa PDV:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(9, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Datum:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(443, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Datum DVO:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.cbVD);
            this.groupBox2.Controls.Add(this.txtBrojAvans);
            this.groupBox2.Controls.Add(this.numGodina);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 63);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(768, 57);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.SystemColors.Control;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button3.Location = new System.Drawing.Point(647, 11);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(115, 35);
            this.button3.TabIndex = 5;
            this.button3.Text = "Storno avansa";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // cbVD
            // 
            this.cbVD.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbVD.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbVD.BackColor = System.Drawing.Color.White;
            this.cbVD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbVD.FormattingEnabled = true;
            this.cbVD.Location = new System.Drawing.Point(65, 16);
            this.cbVD.Name = "cbVD";
            this.cbVD.Size = new System.Drawing.Size(208, 24);
            this.cbVD.TabIndex = 1;
            this.cbVD.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.cbVD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownEnter);
            this.cbVD.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // txtBrojAvans
            // 
            this.txtBrojAvans.BackColor = System.Drawing.Color.White;
            this.txtBrojAvans.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBrojAvans.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtBrojAvans.Location = new System.Drawing.Point(477, 17);
            this.txtBrojAvans.Name = "txtBrojAvans";
            this.txtBrojAvans.Size = new System.Drawing.Size(100, 23);
            this.txtBrojAvans.TabIndex = 4;
            this.txtBrojAvans.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.txtBrojAvans.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBrojAvans_KeyDown);
            this.txtBrojAvans.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // numGodina
            // 
            this.numGodina.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.numGodina.Location = new System.Drawing.Point(389, 17);
            this.numGodina.Name = "numGodina";
            this.numGodina.Size = new System.Drawing.Size(82, 23);
            this.numGodina.TabIndex = 3;
            this.numGodina.Enter += new System.EventHandler(this.TRENUTNI_Enter);
            this.numGodina.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownEnter);
            this.numGodina.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Maroon;
            this.label2.Location = new System.Drawing.Point(286, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Broj avansa:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(13, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vrsta:";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.Image = global::PCPOS.Properties.Resources._10591;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(474, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 41);
            this.button1.TabIndex = 6;
            this.button1.Text = "Svi avansi";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            this.button1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownDole);
            // 
            // btnSpremi
            // 
            this.btnSpremi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSpremi.Image = global::PCPOS.Properties.Resources.filesave;
            this.btnSpremi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSpremi.Location = new System.Drawing.Point(255, 12);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(105, 41);
            this.btnSpremi.TabIndex = 4;
            this.btnSpremi.Text = "Spremi";
            this.btnSpremi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSpremi.UseVisualStyleBackColor = true;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            this.btnSpremi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownDole);
            // 
            // btnIzlaz
            // 
            this.btnIzlaz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIzlaz.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnIzlaz.Image = global::PCPOS.Properties.Resources.Log_Out_icon;
            this.btnIzlaz.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIzlaz.Location = new System.Drawing.Point(695, 12);
            this.btnIzlaz.Name = "btnIzlaz";
            this.btnIzlaz.Size = new System.Drawing.Size(85, 41);
            this.btnIzlaz.TabIndex = 7;
            this.btnIzlaz.Text = "Izlaz";
            this.btnIzlaz.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIzlaz.UseVisualStyleBackColor = true;
            this.btnIzlaz.Click += new System.EventHandler(this.button6_Click);
            this.btnIzlaz.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownDole);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnDelete.Image = global::PCPOS.Properties.Resources.Recyclebin_Empty;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(366, 12);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(102, 41);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Obriši";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.button4_Click);
            this.btnDelete.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownDole);
            // 
            // btnOdustani
            // 
            this.btnOdustani.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnOdustani.Image = global::PCPOS.Properties.Resources.undo;
            this.btnOdustani.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOdustani.Location = new System.Drawing.Point(137, 12);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(112, 41);
            this.btnOdustani.TabIndex = 3;
            this.btnOdustani.Text = "Odustani";
            this.btnOdustani.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOdustani.UseVisualStyleBackColor = true;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            this.btnOdustani.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownDole);
            // 
            // btnNoviUnos
            // 
            this.btnNoviUnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnNoviUnos.Image = global::PCPOS.Properties.Resources.folder_open_icon;
            this.btnNoviUnos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNoviUnos.Location = new System.Drawing.Point(12, 12);
            this.btnNoviUnos.Name = "btnNoviUnos";
            this.btnNoviUnos.Size = new System.Drawing.Size(119, 41);
            this.btnNoviUnos.TabIndex = 2;
            this.btnNoviUnos.Text = "Novi unos";
            this.btnNoviUnos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNoviUnos.UseVisualStyleBackColor = true;
            this.btnNoviUnos.Click += new System.EventHandler(this.button1_Click);
            this.btnNoviUnos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownDole);
            // 
            // frmAvansRacun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(792, 560);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.btnIzlaz);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnNoviUnos);
            this.Name = "frmAvansRacun";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Račun za predujam";
            this.Load += new System.EventHandler(this.frmAvans_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGodina)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNoviUnos;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.DateTimePicker dtpDatumDvo;
        private System.Windows.Forms.DateTimePicker dtpDatum;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numGodina;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbPDV;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox rtbOpis;
        private System.Windows.Forms.ComboBox cbNacinPlacanja;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtSifraNacinPlacanja;
        private System.Windows.Forms.ComboBox cbZiroRacun;
        private System.Windows.Forms.ComboBox cbValuta;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtIzradio;
        private System.Windows.Forms.TextBox txtTecaj;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnIzlaz;
        private System.Windows.Forms.TextBox txtBrojAvans;
        //private System.DirectoryServices.DirectoryEntry directoryEntry1;
        private System.Windows.Forms.ComboBox cbVD;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textPorez25;
        private System.Windows.Forms.TextBox textPorez10;
        private System.Windows.Forms.TextBox textOsnov25;
        private System.Windows.Forms.TextBox textOsnov10;
        private System.Windows.Forms.TextBox textNeoporezivo;
        private System.Windows.Forms.TextBox textNultaStp;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textUkupno;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.TextBox txtAvansirati;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtPartner;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtOpis;
        private System.Windows.Forms.DateTimePicker dtpDatumValute;
        private System.Windows.Forms.Label label22;
    }
}