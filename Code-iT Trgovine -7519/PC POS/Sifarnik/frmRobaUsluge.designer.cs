namespace PCPOS
{
    partial class frmRobaUsluge
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRobaUsluge));
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNaziv = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNabavna = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtVeleprodajna = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMPC = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnDobavljac = new System.Windows.Forms.Button();
            this.cbProizvodac = new System.Windows.Forms.ComboBox();
            this.cbZemljaPodrijetla = new System.Windows.Forms.ComboBox();
            this.cbGrupa = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cbZemljaUvoza = new System.Windows.Forms.ComboBox();
            this.txtJedMj = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPDV = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.bcUzmiSaSkladista = new System.Windows.Forms.ComboBox();
            this.txtSifraDob = new System.Windows.Forms.TextBox();
            this.txtNazivDob = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSveFakture = new System.Windows.Forms.Button();
            this.btnNoviUnos = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.txtPovrNaknada = new System.Windows.Forms.TextBox();
            this.btnKaucija = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtlink_na_sliku = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbAkcija = new System.Windows.Forms.CheckBox();
            this.txtJamstvo = new System.Windows.Forms.TextBox();
            this.rtbxOpisProizvoda = new System.Windows.Forms.RichTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.cbpodgrupa = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtProizvodackaCijena = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.btnBarcode = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSifra
            // 
            this.txtSifra.BackColor = System.Drawing.SystemColors.Window;
            this.txtSifra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSifra.Location = new System.Drawing.Point(148, 108);
            this.txtSifra.MaxLength = 30;
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(266, 23);
            this.txtSifra.TabIndex = 8;
            this.txtSifra.TextChanged += new System.EventHandler(this.txtSifra_TextChanged);
            this.txtSifra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifra_KeyDown);
            this.txtSifra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNaziv_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.Location = new System.Drawing.Point(39, 217);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "Jedinica mjere:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(90, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Grupa:";
            // 
            // txtNaziv
            // 
            this.txtNaziv.Enabled = false;
            this.txtNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtNaziv.Location = new System.Drawing.Point(148, 134);
            this.txtNaziv.MaxLength = 300;
            this.txtNaziv.Name = "txtNaziv";
            this.txtNaziv.Size = new System.Drawing.Size(266, 23);
            this.txtNaziv.TabIndex = 10;
            this.txtNaziv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNaziv_KeyDown);
            this.txtNaziv.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNaziv_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(16, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Naziv robe/usluge:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(4, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Šifra robe/usluge:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(464, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 17);
            this.label3.TabIndex = 29;
            this.label3.Text = "Nabavna cijena:";
            // 
            // txtNabavna
            // 
            this.txtNabavna.Enabled = false;
            this.txtNabavna.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtNabavna.Location = new System.Drawing.Point(580, 108);
            this.txtNabavna.Name = "txtNabavna";
            this.txtNabavna.Size = new System.Drawing.Size(238, 23);
            this.txtNabavna.TabIndex = 30;
            this.txtNabavna.Text = "0,00";
            this.txtNabavna.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtnabavna_KeyDown);
            this.txtNabavna.Leave += new System.EventHandler(this.txtnabavna_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label6.Location = new System.Drawing.Point(437, 137);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 17);
            this.label6.TabIndex = 31;
            this.label6.Text = "Veleprodajna cijena:";
            // 
            // txtVeleprodajna
            // 
            this.txtVeleprodajna.Enabled = false;
            this.txtVeleprodajna.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtVeleprodajna.Location = new System.Drawing.Point(580, 134);
            this.txtVeleprodajna.Name = "txtVeleprodajna";
            this.txtVeleprodajna.Size = new System.Drawing.Size(238, 23);
            this.txtVeleprodajna.TabIndex = 32;
            this.txtVeleprodajna.Text = "0,00";
            this.txtVeleprodajna.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVeleprodajna_KeyDown);
            this.txtVeleprodajna.Leave += new System.EventHandler(this.txtVeleprodajna_Leave_1);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label7.Location = new System.Drawing.Point(435, 190);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(139, 17);
            this.label7.TabIndex = 35;
            this.label7.Text = "Maloprodajna cijena:";
            // 
            // txtMPC
            // 
            this.txtMPC.Enabled = false;
            this.txtMPC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtMPC.Location = new System.Drawing.Point(580, 187);
            this.txtMPC.Name = "txtMPC";
            this.txtMPC.Size = new System.Drawing.Size(238, 23);
            this.txtMPC.TabIndex = 36;
            this.txtMPC.Text = "0,00";
            this.txtMPC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMPC_KeyDown);
            this.txtMPC.Leave += new System.EventHandler(this.txtMPC_Leave_1);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label8.Location = new System.Drawing.Point(492, 217);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 17);
            this.label8.TabIndex = 37;
            this.label8.Text = "Proizvođač:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label9.Location = new System.Drawing.Point(466, 244);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 17);
            this.label9.TabIndex = 39;
            this.label9.Text = "Zemlja porijetla:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label10.Location = new System.Drawing.Point(525, 164);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 17);
            this.label10.TabIndex = 33;
            this.label10.Text = "Porez:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label11.Location = new System.Drawing.Point(68, 271);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 17);
            this.label11.TabIndex = 19;
            this.label11.Text = "Dobavljač:";
            // 
            // btnDobavljac
            // 
            this.btnDobavljac.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnDobavljac.Location = new System.Drawing.Point(390, 267);
            this.btnDobavljac.Name = "btnDobavljac";
            this.btnDobavljac.Size = new System.Drawing.Size(24, 25);
            this.btnDobavljac.TabIndex = 22;
            this.btnDobavljac.Text = "...";
            this.toolTip1.SetToolTip(this.btnDobavljac, "Odaberi");
            this.btnDobavljac.UseVisualStyleBackColor = true;
            this.btnDobavljac.Click += new System.EventHandler(this.btnDobavljac_Click);
            // 
            // cbProizvodac
            // 
            this.cbProizvodac.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbProizvodac.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbProizvodac.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProizvodac.Enabled = false;
            this.cbProizvodac.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbProizvodac.FormattingEnabled = true;
            this.cbProizvodac.Location = new System.Drawing.Point(580, 213);
            this.cbProizvodac.Name = "cbProizvodac";
            this.cbProizvodac.Size = new System.Drawing.Size(238, 24);
            this.cbProizvodac.TabIndex = 38;
            this.cbProizvodac.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbProizvodac_KeyDown);
            // 
            // cbZemljaPodrijetla
            // 
            this.cbZemljaPodrijetla.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbZemljaPodrijetla.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbZemljaPodrijetla.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbZemljaPodrijetla.Enabled = false;
            this.cbZemljaPodrijetla.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbZemljaPodrijetla.FormattingEnabled = true;
            this.cbZemljaPodrijetla.Location = new System.Drawing.Point(580, 240);
            this.cbZemljaPodrijetla.Name = "cbZemljaPodrijetla";
            this.cbZemljaPodrijetla.Size = new System.Drawing.Size(238, 24);
            this.cbZemljaPodrijetla.TabIndex = 40;
            this.cbZemljaPodrijetla.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbZemljaPodrijetla_KeyDown);
            // 
            // cbGrupa
            // 
            this.cbGrupa.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbGrupa.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbGrupa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGrupa.Enabled = false;
            this.cbGrupa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbGrupa.FormattingEnabled = true;
            this.cbGrupa.Location = new System.Drawing.Point(148, 160);
            this.cbGrupa.Name = "cbGrupa";
            this.cbGrupa.Size = new System.Drawing.Size(266, 24);
            this.cbGrupa.TabIndex = 12;
            this.cbGrupa.SelectedIndexChanged += new System.EventHandler(this.cbGrupa_SelectedIndexChanged);
            this.cbGrupa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbGrupa_KeyDown);
            // 
            // cbZemljaUvoza
            // 
            this.cbZemljaUvoza.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbZemljaUvoza.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbZemljaUvoza.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbZemljaUvoza.Enabled = false;
            this.cbZemljaUvoza.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbZemljaUvoza.FormattingEnabled = true;
            this.cbZemljaUvoza.Location = new System.Drawing.Point(580, 267);
            this.cbZemljaUvoza.Name = "cbZemljaUvoza";
            this.cbZemljaUvoza.Size = new System.Drawing.Size(238, 24);
            this.cbZemljaUvoza.TabIndex = 42;
            // 
            // txtJedMj
            // 
            this.txtJedMj.Enabled = false;
            this.txtJedMj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtJedMj.Location = new System.Drawing.Point(148, 214);
            this.txtJedMj.MaxLength = 20;
            this.txtJedMj.Name = "txtJedMj";
            this.txtJedMj.Size = new System.Drawing.Size(266, 23);
            this.txtJedMj.TabIndex = 16;
            this.txtJedMj.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtJedMj_KeyDown);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label12.Location = new System.Drawing.Point(77, 244);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 17);
            this.label12.TabIndex = 17;
            this.label12.Text = "Barcode:";
            // 
            // txtPDV
            // 
            this.txtPDV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtPDV.Enabled = false;
            this.txtPDV.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPDV.FormattingEnabled = true;
            this.txtPDV.Location = new System.Drawing.Point(580, 160);
            this.txtPDV.Name = "txtPDV";
            this.txtPDV.Size = new System.Drawing.Size(238, 24);
            this.txtPDV.TabIndex = 34;
            this.txtPDV.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPDV_KeyDown);
            this.txtPDV.Leave += new System.EventHandler(this.txtPDV_Leave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label13.Location = new System.Drawing.Point(478, 271);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(96, 17);
            this.label13.TabIndex = 41;
            this.label13.Text = "Zemlja uvoza:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label14.Location = new System.Drawing.Point(10, 298);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(132, 17);
            this.label14.TabIndex = 23;
            this.label14.Text = "Uzimaj sa skladišta:";
            // 
            // bcUzmiSaSkladista
            // 
            this.bcUzmiSaSkladista.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bcUzmiSaSkladista.Enabled = false;
            this.bcUzmiSaSkladista.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.bcUzmiSaSkladista.FormattingEnabled = true;
            this.bcUzmiSaSkladista.Location = new System.Drawing.Point(148, 294);
            this.bcUzmiSaSkladista.Name = "bcUzmiSaSkladista";
            this.bcUzmiSaSkladista.Size = new System.Drawing.Size(266, 24);
            this.bcUzmiSaSkladista.TabIndex = 24;
            this.bcUzmiSaSkladista.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bcUzmiSaSkladista_KeyDown);
            // 
            // txtSifraDob
            // 
            this.txtSifraDob.BackColor = System.Drawing.SystemColors.Window;
            this.txtSifraDob.Enabled = false;
            this.txtSifraDob.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSifraDob.Location = new System.Drawing.Point(148, 268);
            this.txtSifraDob.MaxLength = 24;
            this.txtSifraDob.Name = "txtSifraDob";
            this.txtSifraDob.Size = new System.Drawing.Size(68, 23);
            this.txtSifraDob.TabIndex = 20;
            this.txtSifraDob.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbDobavljac_KeyDown);
            // 
            // txtNazivDob
            // 
            this.txtNazivDob.BackColor = System.Drawing.SystemColors.Window;
            this.txtNazivDob.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtNazivDob.Location = new System.Drawing.Point(218, 268);
            this.txtNazivDob.MaxLength = 24;
            this.txtNazivDob.Name = "txtNazivDob";
            this.txtNazivDob.ReadOnly = true;
            this.txtNazivDob.Size = new System.Drawing.Size(171, 23);
            this.txtNazivDob.TabIndex = 21;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.Image = global::PCPOS.Properties.Resources.Actions_application_exit_icon;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(775, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 40);
            this.button1.TabIndex = 53;
            this.button1.Text = "Izlaz      ";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnSveFakture
            // 
            this.btnSveFakture.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSveFakture.Image = global::PCPOS.Properties.Resources._10591;
            this.btnSveFakture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSveFakture.Location = new System.Drawing.Point(420, 12);
            this.btnSveFakture.Name = "btnSveFakture";
            this.btnSveFakture.Size = new System.Drawing.Size(154, 40);
            this.btnSveFakture.TabIndex = 3;
            this.btnSveFakture.Text = "Svi artikli i usluge";
            this.btnSveFakture.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSveFakture.UseVisualStyleBackColor = true;
            this.btnSveFakture.Click += new System.EventHandler(this.btnTrazi_Click);
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
            this.btnNoviUnos.Click += new System.EventHandler(this.btnNew_Click);
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
            this.btnOdustani.Click += new System.EventHandler(this.button1_Click);
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
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label15.Location = new System.Drawing.Point(14, 323);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(128, 17);
            this.label15.TabIndex = 25;
            this.label15.Text = "Povratna naknada:";
            // 
            // txtPovrNaknada
            // 
            this.txtPovrNaknada.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPovrNaknada.Location = new System.Drawing.Point(148, 320);
            this.txtPovrNaknada.MaxLength = 20;
            this.txtPovrNaknada.Name = "txtPovrNaknada";
            this.txtPovrNaknada.Size = new System.Drawing.Size(266, 23);
            this.txtPovrNaknada.TabIndex = 26;
            this.txtPovrNaknada.Text = "0,00";
            // 
            // btnKaucija
            // 
            this.btnKaucija.Enabled = false;
            this.btnKaucija.Location = new System.Drawing.Point(725, 299);
            this.btnKaucija.Name = "btnKaucija";
            this.btnKaucija.Size = new System.Drawing.Size(93, 44);
            this.btnKaucija.TabIndex = 44;
            this.btnKaucija.Text = "Kaucija na artikl";
            this.btnKaucija.UseVisualStyleBackColor = true;
            this.btnKaucija.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(477, 299);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 44);
            this.button2.TabIndex = 43;
            this.button2.Text = "Dodatni podaci o artiklu";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_2);
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtlink_na_sliku
            // 
            this.txtlink_na_sliku.Location = new System.Drawing.Point(148, 467);
            this.txtlink_na_sliku.Name = "txtlink_na_sliku";
            this.txtlink_na_sliku.Size = new System.Drawing.Size(266, 20);
            this.txtlink_na_sliku.TabIndex = 48;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(580, 381);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(238, 168);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 95;
            this.pictureBox1.TabStop = false;
            // 
            // cbAkcija
            // 
            this.cbAkcija.AutoSize = true;
            this.cbAkcija.Location = new System.Drawing.Point(148, 529);
            this.cbAkcija.Name = "cbAkcija";
            this.cbAkcija.Size = new System.Drawing.Size(15, 14);
            this.cbAkcija.TabIndex = 52;
            this.cbAkcija.UseVisualStyleBackColor = true;
            // 
            // txtJamstvo
            // 
            this.txtJamstvo.Location = new System.Drawing.Point(148, 496);
            this.txtJamstvo.Name = "txtJamstvo";
            this.txtJamstvo.Size = new System.Drawing.Size(48, 20);
            this.txtJamstvo.TabIndex = 50;
            this.txtJamstvo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtJamstvo_KeyPress);
            // 
            // rtbxOpisProizvoda
            // 
            this.rtbxOpisProizvoda.Location = new System.Drawing.Point(148, 377);
            this.rtbxOpisProizvoda.Name = "rtbxOpisProizvoda";
            this.rtbxOpisProizvoda.Size = new System.Drawing.Size(266, 80);
            this.rtbxOpisProizvoda.TabIndex = 46;
            this.rtbxOpisProizvoda.Text = "";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label19.Location = new System.Drawing.Point(43, 378);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(99, 16);
            this.label19.TabIndex = 45;
            this.label19.Text = "Opis proizvoda";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label18.Location = new System.Drawing.Point(62, 467);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(80, 16);
            this.label18.TabIndex = 47;
            this.label18.Text = "Link na sliku";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label17.Location = new System.Drawing.Point(97, 527);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(45, 16);
            this.label17.TabIndex = 51;
            this.label17.Text = "Akcija";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label16.Location = new System.Drawing.Point(83, 496);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 16);
            this.label16.TabIndex = 49;
            this.label16.Text = "Jamstvo";
            // 
            // cbpodgrupa
            // 
            this.cbpodgrupa.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbpodgrupa.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbpodgrupa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbpodgrupa.Enabled = false;
            this.cbpodgrupa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbpodgrupa.FormattingEnabled = true;
            this.cbpodgrupa.Location = new System.Drawing.Point(148, 186);
            this.cbpodgrupa.Name = "cbpodgrupa";
            this.cbpodgrupa.Size = new System.Drawing.Size(266, 24);
            this.cbpodgrupa.TabIndex = 14;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label20.Location = new System.Drawing.Point(68, 190);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(74, 17);
            this.label20.TabIndex = 13;
            this.label20.Text = "Podgrupa:";
            // 
            // btnObrisi
            // 
            this.btnObrisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnObrisi.Image = global::PCPOS.Properties.Resources.Actions_application_exit_icon;
            this.btnObrisi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnObrisi.Location = new System.Drawing.Point(580, 12);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(130, 40);
            this.btnObrisi.TabIndex = 4;
            this.btnObrisi.Text = "Obriši artikl";
            this.btnObrisi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnObrisi.UseVisualStyleBackColor = true;
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(12, 55);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(130, 27);
            this.button3.TabIndex = 5;
            this.button3.Text = "Popis robe/usluga";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // btnImport
            // 
            this.btnImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImport.Location = new System.Drawing.Point(148, 55);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(130, 27);
            this.btnImport.TabIndex = 6;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtProizvodackaCijena
            // 
            this.txtProizvodackaCijena.Enabled = false;
            this.txtProizvodackaCijena.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtProizvodackaCijena.Location = new System.Drawing.Point(580, 82);
            this.txtProizvodackaCijena.Name = "txtProizvodackaCijena";
            this.txtProizvodackaCijena.Size = new System.Drawing.Size(238, 23);
            this.txtProizvodackaCijena.TabIndex = 28;
            this.txtProizvodackaCijena.Text = "0,00";
            this.txtProizvodackaCijena.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProizvodackaCijena_KeyDown);
            this.txtProizvodackaCijena.Leave += new System.EventHandler(this.txtProizvodackaCijena_Leave);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label21.Location = new System.Drawing.Point(436, 85);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(138, 17);
            this.label21.TabIndex = 27;
            this.label21.Text = "Proizvođačka cijena:";
            // 
            // btnBarcode
            // 
            this.btnBarcode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBarcode.Enabled = false;
            this.btnBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnBarcode.Location = new System.Drawing.Point(148, 240);
            this.btnBarcode.Name = "btnBarcode";
            this.btnBarcode.Size = new System.Drawing.Size(266, 24);
            this.btnBarcode.TabIndex = 96;
            this.btnBarcode.Text = "Barkodovi";
            this.btnBarcode.UseVisualStyleBackColor = true;
            this.btnBarcode.Click += new System.EventHandler(this.BtnBarcode_Click);
            // 
            // frmRobaUsluge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(917, 565);
            this.Controls.Add(this.btnBarcode);
            this.Controls.Add(this.txtProizvodackaCijena);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.cbpodgrupa);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.txtlink_na_sliku);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cbAkcija);
            this.Controls.Add(this.txtJamstvo);
            this.Controls.Add(this.rtbxOpisProizvoda);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnKaucija);
            this.Controls.Add(this.txtPovrNaknada);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSveFakture);
            this.Controls.Add(this.btnNoviUnos);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.txtNazivDob);
            this.Controls.Add(this.txtSifraDob);
            this.Controls.Add(this.bcUzmiSaSkladista);
            this.Controls.Add(this.txtPDV);
            this.Controls.Add(this.cbZemljaUvoza);
            this.Controls.Add(this.cbGrupa);
            this.Controls.Add(this.cbZemljaPodrijetla);
            this.Controls.Add(this.cbProizvodac);
            this.Controls.Add(this.btnDobavljac);
            this.Controls.Add(this.txtSifra);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtMPC);
            this.Controls.Add(this.txtVeleprodajna);
            this.Controls.Add(this.txtJedMj);
            this.Controls.Add(this.txtNabavna);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNaziv);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Name = "frmRobaUsluge";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Roba i usluge";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GasenjeForme_FormClosing);
            this.Load += new System.EventHandler(this.frmRobaUsluge_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNaziv;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNabavna;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtVeleprodajna;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMPC;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnDobavljac;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cbProizvodac;
        private System.Windows.Forms.ComboBox cbZemljaPodrijetla;
        private System.Windows.Forms.ComboBox cbGrupa;
        private System.Windows.Forms.ComboBox cbZemljaUvoza;
        private System.Windows.Forms.TextBox txtJedMj;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox txtPDV;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox bcUzmiSaSkladista;
        private System.Windows.Forms.TextBox txtNazivDob;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSveFakture;
        private System.Windows.Forms.Button btnNoviUnos;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.TextBox txtSifraDob;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox txtPovrNaknada;
        private System.Windows.Forms.Button btnKaucija;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox txtlink_na_sliku;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox cbAkcija;
        private System.Windows.Forms.TextBox txtJamstvo;
        private System.Windows.Forms.RichTextBox rtbxOpisProizvoda;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cbpodgrupa;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnObrisi;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtProizvodackaCijena;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnBarcode;
    }
}