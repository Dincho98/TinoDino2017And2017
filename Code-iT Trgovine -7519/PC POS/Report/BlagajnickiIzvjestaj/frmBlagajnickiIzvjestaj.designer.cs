namespace PCPOS.Report.BlagajnickiIzvjestaj {
    partial class frmBlagajnickiIzvjestaj {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBlagajnickiIzvjestaj));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnUtrzak = new System.Windows.Forms.Button();
            this.btnPolog = new System.Windows.Forms.Button();
            this.btnUcitaj = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.txtIznos = new System.Windows.Forms.TextBox();
            this.lblIznos = new System.Windows.Forms.Label();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.btnIsplataGotRn = new System.Windows.Forms.Button();
            this.odDatuma = new System.Windows.Forms.DateTimePicker();
            this.doDatuma = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblUplaceno = new System.Windows.Forms.Label();
            this.lblIzdatak = new System.Windows.Forms.Label();
            this.dtDatumVrijeme = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.btnIspis = new System.Windows.Forms.Button();
            this.btnPocetnoGore = new System.Windows.Forms.Button();
            this.btnPozajmnica = new System.Windows.Forms.Button();
            this.txtOznaka = new System.Windows.Forms.TextBox();
            this.btnPovratPozajmnice = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtPartnerNaziv = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.datum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dokument = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oznaka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Uplaceno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.izdatak = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUtrzak
            // 
            this.btnUtrzak.BackColor = System.Drawing.Color.Transparent;
            this.btnUtrzak.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUtrzak.BackgroundImage")));
            this.btnUtrzak.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUtrzak.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUtrzak.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnUtrzak.FlatAppearance.BorderSize = 0;
            this.btnUtrzak.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnUtrzak.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUtrzak.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUtrzak.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUtrzak.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnUtrzak.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnUtrzak.Location = new System.Drawing.Point(12, 12);
            this.btnUtrzak.Name = "btnUtrzak";
            this.btnUtrzak.Size = new System.Drawing.Size(151, 45);
            this.btnUtrzak.TabIndex = 3;
            this.btnUtrzak.Text = "Novi unos uplate utrška";
            this.btnUtrzak.UseVisualStyleBackColor = false;
            this.btnUtrzak.Click += new System.EventHandler(this.btnUtrzak_Click);
            // 
            // btnPolog
            // 
            this.btnPolog.BackColor = System.Drawing.Color.Transparent;
            this.btnPolog.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPolog.BackgroundImage")));
            this.btnPolog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPolog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPolog.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnPolog.FlatAppearance.BorderSize = 0;
            this.btnPolog.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnPolog.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPolog.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPolog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPolog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnPolog.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnPolog.Location = new System.Drawing.Point(169, 12);
            this.btnPolog.Name = "btnPolog";
            this.btnPolog.Size = new System.Drawing.Size(151, 45);
            this.btnPolog.TabIndex = 4;
            this.btnPolog.Text = "Novi unos polog zajma";
            this.btnPolog.UseVisualStyleBackColor = false;
            this.btnPolog.Click += new System.EventHandler(this.btnPolog_Click);
            // 
            // btnUcitaj
            // 
            this.btnUcitaj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUcitaj.BackColor = System.Drawing.Color.Transparent;
            this.btnUcitaj.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUcitaj.BackgroundImage")));
            this.btnUcitaj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUcitaj.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUcitaj.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnUcitaj.FlatAppearance.BorderSize = 0;
            this.btnUcitaj.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnUcitaj.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUcitaj.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUcitaj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUcitaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnUcitaj.ForeColor = System.Drawing.Color.Black;
            this.btnUcitaj.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnUcitaj.Location = new System.Drawing.Point(885, 65);
            this.btnUcitaj.Name = "btnUcitaj";
            this.btnUcitaj.Size = new System.Drawing.Size(62, 49);
            this.btnUcitaj.TabIndex = 2;
            this.btnUcitaj.Text = "Učitaj";
            this.btnUcitaj.UseVisualStyleBackColor = false;
            this.btnUcitaj.Click += new System.EventHandler(this.btnUcitaj_Click);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.datum,
            this.dokument,
            this.oznaka,
            this.Uplaceno,
            this.izdatak,
            this.id});
            this.dgv.Location = new System.Drawing.Point(12, 166);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(935, 360);
            this.dgv.TabIndex = 14;
            this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEndEdit_1);
            // 
            // txtIznos
            // 
            this.txtIznos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txtIznos.Location = new System.Drawing.Point(12, 80);
            this.txtIznos.Name = "txtIznos";
            this.txtIznos.Size = new System.Drawing.Size(192, 29);
            this.txtIznos.TabIndex = 9;
            // 
            // lblIznos
            // 
            this.lblIznos.AutoSize = true;
            this.lblIznos.BackColor = System.Drawing.Color.Transparent;
            this.lblIznos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblIznos.Location = new System.Drawing.Point(9, 60);
            this.lblIznos.Name = "lblIznos";
            this.lblIznos.Size = new System.Drawing.Size(55, 17);
            this.lblIznos.TabIndex = 16;
            this.lblIznos.Text = "lblIznos";
            // 
            // btnSpremi
            // 
            this.btnSpremi.BackColor = System.Drawing.Color.Transparent;
            this.btnSpremi.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSpremi.BackgroundImage")));
            this.btnSpremi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSpremi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSpremi.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnSpremi.FlatAppearance.BorderSize = 0;
            this.btnSpremi.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnSpremi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSpremi.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSpremi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpremi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSpremi.ForeColor = System.Drawing.Color.Black;
            this.btnSpremi.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSpremi.Location = new System.Drawing.Point(615, 111);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(72, 49);
            this.btnSpremi.TabIndex = 13;
            this.btnSpremi.Text = "Spremi";
            this.btnSpremi.UseVisualStyleBackColor = false;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // btnIsplataGotRn
            // 
            this.btnIsplataGotRn.BackColor = System.Drawing.Color.Transparent;
            this.btnIsplataGotRn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnIsplataGotRn.BackgroundImage")));
            this.btnIsplataGotRn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIsplataGotRn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIsplataGotRn.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnIsplataGotRn.FlatAppearance.BorderSize = 0;
            this.btnIsplataGotRn.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnIsplataGotRn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnIsplataGotRn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnIsplataGotRn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIsplataGotRn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnIsplataGotRn.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnIsplataGotRn.Location = new System.Drawing.Point(326, 12);
            this.btnIsplataGotRn.Name = "btnIsplataGotRn";
            this.btnIsplataGotRn.Size = new System.Drawing.Size(151, 45);
            this.btnIsplataGotRn.TabIndex = 5;
            this.btnIsplataGotRn.Text = "Novi unos isplate po GOT. RN.";
            this.btnIsplataGotRn.UseVisualStyleBackColor = false;
            this.btnIsplataGotRn.Click += new System.EventHandler(this.btnIsplataGotRn_Click);
            // 
            // odDatuma
            // 
            this.odDatuma.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.odDatuma.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.odDatuma.Location = new System.Drawing.Point(759, 65);
            this.odDatuma.Name = "odDatuma";
            this.odDatuma.Size = new System.Drawing.Size(120, 20);
            this.odDatuma.TabIndex = 0;
            // 
            // doDatuma
            // 
            this.doDatuma.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.doDatuma.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.doDatuma.Location = new System.Drawing.Point(759, 94);
            this.doDatuma.Name = "doDatuma";
            this.doDatuma.Size = new System.Drawing.Size(120, 20);
            this.doDatuma.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(727, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 17);
            this.label1.TabIndex = 20;
            this.label1.Text = "OD";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(727, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 17);
            this.label2.TabIndex = 21;
            this.label2.Text = "DO";
            // 
            // lblUplaceno
            // 
            this.lblUplaceno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUplaceno.AutoSize = true;
            this.lblUplaceno.BackColor = System.Drawing.Color.Transparent;
            this.lblUplaceno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblUplaceno.Location = new System.Drawing.Point(26, 545);
            this.lblUplaceno.Name = "lblUplaceno";
            this.lblUplaceno.Size = new System.Drawing.Size(104, 17);
            this.lblUplaceno.TabIndex = 22;
            this.lblUplaceno.Text = "Uplaćeno: 0,00";
            // 
            // lblIzdatak
            // 
            this.lblIzdatak.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblIzdatak.AutoSize = true;
            this.lblIzdatak.BackColor = System.Drawing.Color.Transparent;
            this.lblIzdatak.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblIzdatak.Location = new System.Drawing.Point(195, 545);
            this.lblIzdatak.Name = "lblIzdatak";
            this.lblIzdatak.Size = new System.Drawing.Size(89, 17);
            this.lblIzdatak.TabIndex = 23;
            this.lblIzdatak.Text = "Izdatak: 0,00";
            // 
            // dtDatumVrijeme
            // 
            this.dtDatumVrijeme.CustomFormat = "dd.MM.yyyy   H:mm";
            this.dtDatumVrijeme.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtDatumVrijeme.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDatumVrijeme.Location = new System.Drawing.Point(12, 131);
            this.dtDatumVrijeme.Name = "dtDatumVrijeme";
            this.dtDatumVrijeme.Size = new System.Drawing.Size(192, 29);
            this.dtDatumVrijeme.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(9, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "Datum:";
            // 
            // btnIspis
            // 
            this.btnIspis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIspis.BackColor = System.Drawing.Color.Transparent;
            this.btnIspis.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnIspis.BackgroundImage")));
            this.btnIspis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIspis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIspis.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnIspis.FlatAppearance.BorderSize = 0;
            this.btnIspis.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnIspis.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnIspis.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnIspis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIspis.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnIspis.ForeColor = System.Drawing.Color.Black;
            this.btnIspis.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnIspis.Location = new System.Drawing.Point(807, 532);
            this.btnIspis.Name = "btnIspis";
            this.btnIspis.Size = new System.Drawing.Size(140, 30);
            this.btnIspis.TabIndex = 15;
            this.btnIspis.Text = "Ispis na A4";
            this.btnIspis.UseVisualStyleBackColor = false;
            this.btnIspis.Click += new System.EventHandler(this.btnIspis_Click);
            // 
            // btnPocetnoGore
            // 
            this.btnPocetnoGore.BackColor = System.Drawing.Color.Transparent;
            this.btnPocetnoGore.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPocetnoGore.BackgroundImage")));
            this.btnPocetnoGore.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPocetnoGore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPocetnoGore.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnPocetnoGore.FlatAppearance.BorderSize = 0;
            this.btnPocetnoGore.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnPocetnoGore.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPocetnoGore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPocetnoGore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPocetnoGore.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnPocetnoGore.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnPocetnoGore.Location = new System.Drawing.Point(483, 12);
            this.btnPocetnoGore.Name = "btnPocetnoGore";
            this.btnPocetnoGore.Size = new System.Drawing.Size(151, 45);
            this.btnPocetnoGore.TabIndex = 6;
            this.btnPocetnoGore.Text = "Novi unos početnog stanja";
            this.btnPocetnoGore.UseVisualStyleBackColor = false;
            this.btnPocetnoGore.Click += new System.EventHandler(this.btnPocetnoGore_Click);
            // 
            // btnPozajmnica
            // 
            this.btnPozajmnica.BackColor = System.Drawing.Color.Transparent;
            this.btnPozajmnica.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPozajmnica.BackgroundImage")));
            this.btnPozajmnica.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPozajmnica.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPozajmnica.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnPozajmnica.FlatAppearance.BorderSize = 0;
            this.btnPozajmnica.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnPozajmnica.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPozajmnica.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPozajmnica.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPozajmnica.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnPozajmnica.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnPozajmnica.Location = new System.Drawing.Point(640, 12);
            this.btnPozajmnica.Name = "btnPozajmnica";
            this.btnPozajmnica.Size = new System.Drawing.Size(151, 45);
            this.btnPozajmnica.TabIndex = 7;
            this.btnPozajmnica.Text = "Novi unos pozajmnice";
            this.btnPozajmnica.UseVisualStyleBackColor = false;
            this.btnPozajmnica.Click += new System.EventHandler(this.btnPozajmnica_Click);
            // 
            // txtOznaka
            // 
            this.txtOznaka.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txtOznaka.Location = new System.Drawing.Point(228, 80);
            this.txtOznaka.Name = "txtOznaka";
            this.txtOznaka.Size = new System.Drawing.Size(300, 29);
            this.txtOznaka.TabIndex = 11;
            // 
            // btnPovratPozajmnice
            // 
            this.btnPovratPozajmnice.BackColor = System.Drawing.Color.Transparent;
            this.btnPovratPozajmnice.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPovratPozajmnice.BackgroundImage")));
            this.btnPovratPozajmnice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPovratPozajmnice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPovratPozajmnice.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnPovratPozajmnice.FlatAppearance.BorderSize = 0;
            this.btnPovratPozajmnice.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnPovratPozajmnice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPovratPozajmnice.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPovratPozajmnice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPovratPozajmnice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnPovratPozajmnice.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnPovratPozajmnice.Location = new System.Drawing.Point(797, 12);
            this.btnPovratPozajmnice.Name = "btnPovratPozajmnice";
            this.btnPovratPozajmnice.Size = new System.Drawing.Size(151, 45);
            this.btnPovratPozajmnice.TabIndex = 8;
            this.btnPovratPozajmnice.Text = "Povrat pozajmnice";
            this.btnPovratPozajmnice.UseVisualStyleBackColor = false;
            this.btnPovratPozajmnice.Click += new System.EventHandler(this.btnPovratPozajmnice_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(225, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 17);
            this.label4.TabIndex = 18;
            this.label4.Text = "Oznaka:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::PCPOS.Properties.Resources._1059;
            this.pictureBox1.Location = new System.Drawing.Point(534, 131);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(29, 29);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 163;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // txtPartnerNaziv
            // 
            this.txtPartnerNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.txtPartnerNaziv.Location = new System.Drawing.Point(228, 131);
            this.txtPartnerNaziv.Name = "txtPartnerNaziv";
            this.txtPartnerNaziv.ReadOnly = true;
            this.txtPartnerNaziv.Size = new System.Drawing.Size(300, 29);
            this.txtPartnerNaziv.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.Location = new System.Drawing.Point(225, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 17);
            this.label5.TabIndex = 19;
            this.label5.Text = "Partner:";
            // 
            // datum
            // 
            this.datum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.datum.HeaderText = "Datum";
            this.datum.Name = "datum";
            this.datum.Width = 120;
            // 
            // dokument
            // 
            this.dokument.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dokument.HeaderText = "Dokument";
            this.dokument.Name = "dokument";
            this.dokument.Width = 180;
            // 
            // oznaka
            // 
            this.oznaka.HeaderText = "Oznaka";
            this.oznaka.Name = "oznaka";
            // 
            // Uplaceno
            // 
            this.Uplaceno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.Uplaceno.DefaultCellStyle = dataGridViewCellStyle1;
            this.Uplaceno.HeaderText = "Uplaceno";
            this.Uplaceno.Name = "Uplaceno";
            this.Uplaceno.Width = 120;
            // 
            // izdatak
            // 
            this.izdatak.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            this.izdatak.DefaultCellStyle = dataGridViewCellStyle2;
            this.izdatak.HeaderText = "Izdatak";
            this.izdatak.Name = "izdatak";
            this.izdatak.Width = 120;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // frmBlagajnickiIzvjestaj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(959, 574);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPartnerNaziv);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnPovratPozajmnice);
            this.Controls.Add(this.txtOznaka);
            this.Controls.Add(this.btnPozajmnica);
            this.Controls.Add(this.btnPocetnoGore);
            this.Controls.Add(this.btnIspis);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtDatumVrijeme);
            this.Controls.Add(this.lblIzdatak);
            this.Controls.Add(this.lblUplaceno);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.doDatuma);
            this.Controls.Add(this.odDatuma);
            this.Controls.Add(this.btnIsplataGotRn);
            this.Controls.Add(this.btnSpremi);
            this.Controls.Add(this.lblIznos);
            this.Controls.Add(this.txtIznos);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.btnUcitaj);
            this.Controls.Add(this.btnPolog);
            this.Controls.Add(this.btnUtrzak);
            this.MinimumSize = new System.Drawing.Size(975, 38);
            this.Name = "frmBlagajnickiIzvjestaj";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Blagajnički izvještaj";
            this.Load += new System.EventHandler(this.frmBlagajnickiIzvjestaj_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUtrzak;
        private System.Windows.Forms.Button btnPolog;
        private System.Windows.Forms.Button btnUcitaj;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.TextBox txtIznos;
        private System.Windows.Forms.Label lblIznos;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.Button btnIsplataGotRn;
        private System.Windows.Forms.DateTimePicker doDatuma;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblUplaceno;
        private System.Windows.Forms.Label lblIzdatak;
        public System.Windows.Forms.DateTimePicker odDatuma;
        private System.Windows.Forms.DateTimePicker dtDatumVrijeme;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnIspis;
        private System.Windows.Forms.Button btnPocetnoGore;
        private System.Windows.Forms.Button btnPozajmnica;
        private System.Windows.Forms.TextBox txtOznaka;
        private System.Windows.Forms.Button btnPovratPozajmnice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtPartnerNaziv;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum;
        private System.Windows.Forms.DataGridViewTextBoxColumn dokument;
        private System.Windows.Forms.DataGridViewTextBoxColumn oznaka;
        private System.Windows.Forms.DataGridViewTextBoxColumn Uplaceno;
        private System.Windows.Forms.DataGridViewTextBoxColumn izdatak;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
    }
}