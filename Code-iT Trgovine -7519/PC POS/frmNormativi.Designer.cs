namespace PCPOS
{
    partial class frmNormativi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNormativi));
            this.lblNaDan = new System.Windows.Forms.Label();
            this.txtSifra_robe = new System.Windows.Forms.TextBox();
            this.s = new System.Windows.Forms.GroupBox();
            this.txtMpcNormativ = new System.Windows.Forms.TextBox();
            this.txtVpcNormativ = new System.Windows.Forms.TextBox();
            this.lblMpc = new System.Windows.Forms.Label();
            this.lblVpc = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtIzradio = new System.Windows.Forms.TextBox();
            this.txtKomentar = new System.Windows.Forms.TextBox();
            this.txtImeArtikla = new System.Windows.Forms.TextBox();
            this.txtVrstaRobe = new System.Windows.Forms.TextBox();
            this.txtSifraArtikla = new System.Windows.Forms.TextBox();
            this.txtJedinicaMjere = new System.Windows.Forms.TextBox();
            this.btnArtikli = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nuGodina = new System.Windows.Forms.NumericUpDown();
            this.txtBrojNormativa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOpenRoba = new System.Windows.Forms.Button();
            this.dgw = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.btnSve = new System.Windows.Forms.Button();
            this.btnNoviUnos = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skladiste = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jmj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_stavka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pc_iznos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.s.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuGodina)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNaDan
            // 
            this.lblNaDan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNaDan.AutoSize = true;
            this.lblNaDan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblNaDan.Location = new System.Drawing.Point(19, 593);
            this.lblNaDan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNaDan.Name = "lblNaDan";
            this.lblNaDan.Size = new System.Drawing.Size(0, 13);
            this.lblNaDan.TabIndex = 63;
            // 
            // txtSifra_robe
            // 
            this.txtSifra_robe.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtSifra_robe.Location = new System.Drawing.Point(17, 427);
            this.txtSifra_robe.Margin = new System.Windows.Forms.Padding(4);
            this.txtSifra_robe.Name = "txtSifra_robe";
            this.txtSifra_robe.Size = new System.Drawing.Size(199, 26);
            this.txtSifra_robe.TabIndex = 51;
            this.txtSifra_robe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifra_robe_KeyDown);
            // 
            // s
            // 
            this.s.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.s.BackColor = System.Drawing.Color.Transparent;
            this.s.Controls.Add(this.txtMpcNormativ);
            this.s.Controls.Add(this.txtVpcNormativ);
            this.s.Controls.Add(this.lblMpc);
            this.s.Controls.Add(this.lblVpc);
            this.s.Controls.Add(this.label12);
            this.s.Controls.Add(this.txtIzradio);
            this.s.Controls.Add(this.txtKomentar);
            this.s.Controls.Add(this.txtImeArtikla);
            this.s.Controls.Add(this.txtVrstaRobe);
            this.s.Controls.Add(this.txtSifraArtikla);
            this.s.Controls.Add(this.txtJedinicaMjere);
            this.s.Controls.Add(this.btnArtikli);
            this.s.Controls.Add(this.label5);
            this.s.Controls.Add(this.label1);
            this.s.Controls.Add(this.label17);
            this.s.Controls.Add(this.label4);
            this.s.Location = new System.Drawing.Point(17, 174);
            this.s.Margin = new System.Windows.Forms.Padding(4);
            this.s.Name = "s";
            this.s.Padding = new System.Windows.Forms.Padding(4);
            this.s.Size = new System.Drawing.Size(959, 209);
            this.s.TabIndex = 55;
            this.s.TabStop = false;
            // 
            // txtMpcNormativ
            // 
            this.txtMpcNormativ.Enabled = false;
            this.txtMpcNormativ.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtMpcNormativ.Location = new System.Drawing.Point(611, 59);
            this.txtMpcNormativ.Margin = new System.Windows.Forms.Padding(4);
            this.txtMpcNormativ.MaxLength = 100;
            this.txtMpcNormativ.Name = "txtMpcNormativ";
            this.txtMpcNormativ.ReadOnly = true;
            this.txtMpcNormativ.Size = new System.Drawing.Size(167, 23);
            this.txtMpcNormativ.TabIndex = 61;
            // 
            // txtVpcNormativ
            // 
            this.txtVpcNormativ.Enabled = false;
            this.txtVpcNormativ.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtVpcNormativ.Location = new System.Drawing.Point(611, 25);
            this.txtVpcNormativ.Margin = new System.Windows.Forms.Padding(4);
            this.txtVpcNormativ.MaxLength = 100;
            this.txtVpcNormativ.Name = "txtVpcNormativ";
            this.txtVpcNormativ.ReadOnly = true;
            this.txtVpcNormativ.Size = new System.Drawing.Size(167, 23);
            this.txtVpcNormativ.TabIndex = 60;
            // 
            // lblMpc
            // 
            this.lblMpc.AutoSize = true;
            this.lblMpc.BackColor = System.Drawing.Color.Transparent;
            this.lblMpc.Location = new System.Drawing.Point(549, 62);
            this.lblMpc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMpc.Name = "lblMpc";
            this.lblMpc.Size = new System.Drawing.Size(38, 17);
            this.lblMpc.TabIndex = 59;
            this.lblMpc.Text = "Mpc:";
            // 
            // lblVpc
            // 
            this.lblVpc.AutoSize = true;
            this.lblVpc.BackColor = System.Drawing.Color.Transparent;
            this.lblVpc.Location = new System.Drawing.Point(549, 28);
            this.lblVpc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVpc.Name = "lblVpc";
            this.lblVpc.Size = new System.Drawing.Size(36, 17);
            this.lblVpc.TabIndex = 58;
            this.lblVpc.Text = "Vpc:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(12, 118);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 17);
            this.label12.TabIndex = 57;
            this.label12.Text = "Izradio:";
            // 
            // txtIzradio
            // 
            this.txtIzradio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtIzradio.Location = new System.Drawing.Point(130, 112);
            this.txtIzradio.Margin = new System.Windows.Forms.Padding(4);
            this.txtIzradio.MaxLength = 100;
            this.txtIzradio.Name = "txtIzradio";
            this.txtIzradio.ReadOnly = true;
            this.txtIzradio.Size = new System.Drawing.Size(391, 23);
            this.txtIzradio.TabIndex = 54;
            this.txtIzradio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKomentar_KeyDown);
            // 
            // txtKomentar
            // 
            this.txtKomentar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtKomentar.Location = new System.Drawing.Point(130, 165);
            this.txtKomentar.Margin = new System.Windows.Forms.Padding(4);
            this.txtKomentar.MaxLength = 100;
            this.txtKomentar.Name = "txtKomentar";
            this.txtKomentar.Size = new System.Drawing.Size(391, 23);
            this.txtKomentar.TabIndex = 54;
            this.txtKomentar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKomentar_KeyDown);
            // 
            // txtImeArtikla
            // 
            this.txtImeArtikla.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtImeArtikla.Location = new System.Drawing.Point(280, 22);
            this.txtImeArtikla.Margin = new System.Windows.Forms.Padding(4);
            this.txtImeArtikla.Name = "txtImeArtikla";
            this.txtImeArtikla.ReadOnly = true;
            this.txtImeArtikla.Size = new System.Drawing.Size(241, 24);
            this.txtImeArtikla.TabIndex = 56;
            // 
            // txtVrstaRobe
            // 
            this.txtVrstaRobe.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtVrstaRobe.Location = new System.Drawing.Point(130, 82);
            this.txtVrstaRobe.Margin = new System.Windows.Forms.Padding(4);
            this.txtVrstaRobe.Name = "txtVrstaRobe";
            this.txtVrstaRobe.ReadOnly = true;
            this.txtVrstaRobe.Size = new System.Drawing.Size(391, 23);
            this.txtVrstaRobe.TabIndex = 54;
            // 
            // txtSifraArtikla
            // 
            this.txtSifraArtikla.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtSifraArtikla.Location = new System.Drawing.Point(130, 22);
            this.txtSifraArtikla.Margin = new System.Windows.Forms.Padding(4);
            this.txtSifraArtikla.Name = "txtSifraArtikla";
            this.txtSifraArtikla.Size = new System.Drawing.Size(112, 23);
            this.txtSifraArtikla.TabIndex = 54;
            this.txtSifraArtikla.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraArtikla_KeyDown);
            // 
            // txtJedinicaMjere
            // 
            this.txtJedinicaMjere.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtJedinicaMjere.Location = new System.Drawing.Point(130, 53);
            this.txtJedinicaMjere.Margin = new System.Windows.Forms.Padding(4);
            this.txtJedinicaMjere.Name = "txtJedinicaMjere";
            this.txtJedinicaMjere.ReadOnly = true;
            this.txtJedinicaMjere.Size = new System.Drawing.Size(391, 23);
            this.txtJedinicaMjere.TabIndex = 54;
            // 
            // btnArtikli
            // 
            this.btnArtikli.Location = new System.Drawing.Point(243, 21);
            this.btnArtikli.Margin = new System.Windows.Forms.Padding(4);
            this.btnArtikli.Name = "btnArtikli";
            this.btnArtikli.Size = new System.Drawing.Size(37, 26);
            this.btnArtikli.TabIndex = 55;
            this.btnArtikli.Text = "...";
            this.btnArtikli.UseVisualStyleBackColor = true;
            this.btnArtikli.Click += new System.EventHandler(this.btnArtikli_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(13, 172);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 17);
            this.label5.TabIndex = 43;
            this.label5.Text = "Komentar:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(13, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Šifra i ime artikla:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Location = new System.Drawing.Point(12, 57);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(103, 17);
            this.label17.TabIndex = 24;
            this.label17.Text = "Jedinica mjere:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(13, 87);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 17);
            this.label4.TabIndex = 21;
            this.label4.Text = "Vrsta robe:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.nuGodina);
            this.groupBox1.Controls.Add(this.txtBrojNormativa);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(17, 82);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(959, 55);
            this.groupBox1.TabIndex = 54;
            this.groupBox1.TabStop = false;
            // 
            // nuGodina
            // 
            this.nuGodina.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.nuGodina.Location = new System.Drawing.Point(248, 20);
            this.nuGodina.Margin = new System.Windows.Forms.Padding(4);
            this.nuGodina.Name = "nuGodina";
            this.nuGodina.Size = new System.Drawing.Size(112, 23);
            this.nuGodina.TabIndex = 57;
            this.nuGodina.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBrojNormativa_KeyDown);
            // 
            // txtBrojNormativa
            // 
            this.txtBrojNormativa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtBrojNormativa.Location = new System.Drawing.Point(135, 20);
            this.txtBrojNormativa.Margin = new System.Windows.Forms.Padding(4);
            this.txtBrojNormativa.Name = "txtBrojNormativa";
            this.txtBrojNormativa.Size = new System.Drawing.Size(112, 23);
            this.txtBrojNormativa.TabIndex = 54;
            this.txtBrojNormativa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBrojNormativa_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.Color.Maroon;
            this.label2.Location = new System.Drawing.Point(13, 26);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Broj normativa:";
            // 
            // btnOpenRoba
            // 
            this.btnOpenRoba.Location = new System.Drawing.Point(216, 426);
            this.btnOpenRoba.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenRoba.Name = "btnOpenRoba";
            this.btnOpenRoba.Size = new System.Drawing.Size(37, 28);
            this.btnOpenRoba.TabIndex = 53;
            this.btnOpenRoba.Text = "...";
            this.btnOpenRoba.UseVisualStyleBackColor = true;
            this.btnOpenRoba.Click += new System.EventHandler(this.btnOpenRoba_Click);
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
            this.dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgw.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sifra,
            this.skladiste,
            this.naziv,
            this.kolicina,
            this.jmj,
            this.id_stavka,
            this.PC,
            this.pc_iznos});
            this.dgw.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgw.GridColor = System.Drawing.Color.Gainsboro;
            this.dgw.Location = new System.Drawing.Point(17, 463);
            this.dgw.Margin = new System.Windows.Forms.Padding(4);
            this.dgw.MultiSelect = false;
            this.dgw.Name = "dgw";
            this.dgw.RowHeadersWidth = 30;
            this.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgw.Size = new System.Drawing.Size(959, 137);
            this.dgw.TabIndex = 50;
            this.dgw.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellClick);
            this.dgw.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellEndEdit);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(19, 407);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Šifra artikla/usluge:";
            // 
            // btnObrisi
            // 
            this.btnObrisi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnObrisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnObrisi.Image = global::PCPOS.Properties.Resources.Close;
            this.btnObrisi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnObrisi.Location = new System.Drawing.Point(841, 426);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(135, 30);
            this.btnObrisi.TabIndex = 64;
            this.btnObrisi.Text = "   Obriši stavku";
            this.btnObrisi.UseVisualStyleBackColor = true;
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.Image = global::PCPOS.Properties.Resources.Actions_application_exit_icon;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(846, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 40);
            this.button1.TabIndex = 77;
            this.button1.Text = "Izlaz      ";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnIzlaz_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Enabled = false;
            this.btnDeleteAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnDeleteAll.Image = global::PCPOS.Properties.Resources.Recyclebin_Empty;
            this.btnDeleteAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteAll.Location = new System.Drawing.Point(569, 21);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(141, 40);
            this.btnDeleteAll.TabIndex = 76;
            this.btnDeleteAll.Text = "Obriši normativ";
            this.btnDeleteAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteAll.UseVisualStyleBackColor = true;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAllRN_Click);
            // 
            // btnSve
            // 
            this.btnSve.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSve.Image = global::PCPOS.Properties.Resources._10591;
            this.btnSve.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSve.Location = new System.Drawing.Point(431, 21);
            this.btnSve.Name = "btnSve";
            this.btnSve.Size = new System.Drawing.Size(130, 40);
            this.btnSve.TabIndex = 75;
            this.btnSve.Text = "Svi normativi  ";
            this.btnSve.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSve.UseVisualStyleBackColor = true;
            this.btnSve.Click += new System.EventHandler(this.btnSviRN_Click);
            // 
            // btnNoviUnos
            // 
            this.btnNoviUnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnNoviUnos.Image = global::PCPOS.Properties.Resources.folder_open_icon;
            this.btnNoviUnos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNoviUnos.Location = new System.Drawing.Point(17, 21);
            this.btnNoviUnos.Name = "btnNoviUnos";
            this.btnNoviUnos.Size = new System.Drawing.Size(130, 40);
            this.btnNoviUnos.TabIndex = 74;
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
            this.btnOdustani.Location = new System.Drawing.Point(155, 21);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(130, 40);
            this.btnOdustani.TabIndex = 73;
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
            this.btnSpremi.Location = new System.Drawing.Point(293, 21);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(130, 40);
            this.btnSpremi.TabIndex = 72;
            this.btnSpremi.Text = "Spremi   ";
            this.btnSpremi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSpremi.UseVisualStyleBackColor = true;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // sifra
            // 
            this.sifra.HeaderText = "Šifra robe";
            this.sifra.Name = "sifra";
            this.sifra.ReadOnly = true;
            // 
            // skladiste
            // 
            this.skladiste.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.skladiste.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.skladiste.HeaderText = "Skladište";
            this.skladiste.Name = "skladiste";
            this.skladiste.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // naziv
            // 
            this.naziv.HeaderText = "Naziv";
            this.naziv.Name = "naziv";
            this.naziv.ReadOnly = true;
            // 
            // kolicina
            // 
            this.kolicina.HeaderText = "Količina";
            this.kolicina.Name = "kolicina";
            // 
            // jmj
            // 
            this.jmj.HeaderText = "JMJ";
            this.jmj.Name = "jmj";
            this.jmj.ReadOnly = true;
            // 
            // id_stavka
            // 
            this.id_stavka.HeaderText = "id_stavka";
            this.id_stavka.Name = "id_stavka";
            this.id_stavka.Visible = false;
            // 
            // PC
            // 
            this.PC.HeaderText = "PC";
            this.PC.Name = "PC";
            this.PC.Visible = false;
            // 
            // pc_iznos
            // 
            this.pc_iznos.HeaderText = "PC Iznos";
            this.pc_iznos.Name = "pc_iznos";
            this.pc_iznos.Visible = false;
            // 
            // frmNormativi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(993, 614);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDeleteAll);
            this.Controls.Add(this.btnSve);
            this.Controls.Add(this.btnNoviUnos);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.lblNaDan);
            this.Controls.Add(this.txtSifra_robe);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.s);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOpenRoba);
            this.Controls.Add(this.dgw);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmNormativi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Normativi";
            this.Load += new System.EventHandler(this.frmNormativi_Load);
            this.s.ResumeLayout(false);
            this.s.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuGodina)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNaDan;
        private System.Windows.Forms.TextBox txtSifra_robe;
        private System.Windows.Forms.GroupBox s;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpenRoba;
        private System.Windows.Forms.DataGridView dgw;
        private System.Windows.Forms.TextBox txtImeArtikla;
        private System.Windows.Forms.TextBox txtSifraArtikla;
        private System.Windows.Forms.Button btnArtikli;
        private System.Windows.Forms.TextBox txtJedinicaMjere;
        private System.Windows.Forms.TextBox txtKomentar;
        private System.Windows.Forms.TextBox txtVrstaRobe;
        private System.Windows.Forms.TextBox txtBrojNormativa;
        private System.Windows.Forms.NumericUpDown nuGodina;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtIzradio;
        private System.Windows.Forms.Button btnObrisi;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.Button btnSve;
        private System.Windows.Forms.Button btnNoviUnos;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.TextBox txtMpcNormativ;
        private System.Windows.Forms.TextBox txtVpcNormativ;
        private System.Windows.Forms.Label lblMpc;
        private System.Windows.Forms.Label lblVpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewComboBoxColumn skladiste;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolicina;
        private System.Windows.Forms.DataGridViewTextBoxColumn jmj;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_stavka;
        private System.Windows.Forms.DataGridViewTextBoxColumn PC;
        private System.Windows.Forms.DataGridViewTextBoxColumn pc_iznos;
    }
}