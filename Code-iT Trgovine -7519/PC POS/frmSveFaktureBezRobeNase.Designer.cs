namespace PCPOS
{
    partial class frmSveFaktureBezRobeNase
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSveFaktureBezRobeNase));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chbPoslovnica = new System.Windows.Forms.CheckBox();
            this.cbPoslovnica = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chbNapomenackb = new System.Windows.Forms.CheckBox();
            this.textNapomena = new System.Windows.Forms.TextBox();
            this.lvlnapomena = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.chbDO = new System.Windows.Forms.CheckBox();
            this.chbSifra = new System.Windows.Forms.CheckBox();
            this.chbValuta = new System.Windows.Forms.CheckBox();
            this.chbOD = new System.Windows.Forms.CheckBox();
            this.chbIzradio = new System.Windows.Forms.CheckBox();
            this.chbBroj = new System.Windows.Forms.CheckBox();
            this.txtPartner = new System.Windows.Forms.TextBox();
            this.cbIzradio = new System.Windows.Forms.ComboBox();
            this.cbValuta = new System.Windows.Forms.ComboBox();
            this.dtpDO = new System.Windows.Forms.DateTimePicker();
            this.txtBroj = new System.Windows.Forms.TextBox();
            this.dtpOD = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbIzradio = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.btnUrediSifru = new System.Windows.Forms.Button();
            this.btnSveFakture = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chbSkladiste = new System.Windows.Forms.CheckBox();
            this.cmbSkladiste = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.broj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valuta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sifra_partnera = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.broj_ispisa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ukupno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.storno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_ducan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skladiste = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.placeno = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dgv_partner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_kasa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.broj,
            this.datum,
            this.valuta,
            this.sifra_partnera,
            this.partner,
            this.broj_ispisa,
            this.ukupno,
            this.storno,
            this.id_ducan,
            this.skladiste,
            this.placeno,
            this.dgv_partner,
            this.id_kasa});
            this.dgv.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgv.GridColor = System.Drawing.Color.Gainsboro;
            this.dgv.Location = new System.Drawing.Point(12, 178);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(980, 478);
            this.dgv.TabIndex = 0;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.chbSkladiste);
            this.groupBox1.Controls.Add(this.cmbSkladiste);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.chbPoslovnica);
            this.groupBox1.Controls.Add(this.cbPoslovnica);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.chbNapomenackb);
            this.groupBox1.Controls.Add(this.textNapomena);
            this.groupBox1.Controls.Add(this.lvlnapomena);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.chbDO);
            this.groupBox1.Controls.Add(this.chbSifra);
            this.groupBox1.Controls.Add(this.chbValuta);
            this.groupBox1.Controls.Add(this.chbOD);
            this.groupBox1.Controls.Add(this.chbIzradio);
            this.groupBox1.Controls.Add(this.chbBroj);
            this.groupBox1.Controls.Add(this.txtPartner);
            this.groupBox1.Controls.Add(this.cbIzradio);
            this.groupBox1.Controls.Add(this.cbValuta);
            this.groupBox1.Controls.Add(this.dtpDO);
            this.groupBox1.Controls.Add(this.txtBroj);
            this.groupBox1.Controls.Add(this.dtpOD);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lbIzradio);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox1.Location = new System.Drawing.Point(13, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(979, 111);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pretraživanje fakture";
            // 
            // chbPoslovnica
            // 
            this.chbPoslovnica.AutoSize = true;
            this.chbPoslovnica.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chbPoslovnica.Location = new System.Drawing.Point(581, 82);
            this.chbPoslovnica.Name = "chbPoslovnica";
            this.chbPoslovnica.Size = new System.Drawing.Size(15, 14);
            this.chbPoslovnica.TabIndex = 222;
            this.chbPoslovnica.UseVisualStyleBackColor = true;
            // 
            // cbPoslovnica
            // 
            this.cbPoslovnica.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbPoslovnica.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbPoslovnica.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPoslovnica.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbPoslovnica.FormattingEnabled = true;
            this.cbPoslovnica.Location = new System.Drawing.Point(410, 77);
            this.cbPoslovnica.Name = "cbPoslovnica";
            this.cbPoslovnica.Size = new System.Drawing.Size(168, 24);
            this.cbPoslovnica.TabIndex = 221;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(325, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 17);
            this.label6.TabIndex = 220;
            this.label6.Text = "Poslovnica:";
            // 
            // chbNapomenackb
            // 
            this.chbNapomenackb.AutoSize = true;
            this.chbNapomenackb.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chbNapomenackb.Location = new System.Drawing.Point(880, 81);
            this.chbNapomenackb.Name = "chbNapomenackb";
            this.chbNapomenackb.Size = new System.Drawing.Size(15, 14);
            this.chbNapomenackb.TabIndex = 219;
            this.chbNapomenackb.UseVisualStyleBackColor = true;
            // 
            // textNapomena
            // 
            this.textNapomena.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textNapomena.Location = new System.Drawing.Point(709, 78);
            this.textNapomena.Name = "textNapomena";
            this.textNapomena.Size = new System.Drawing.Size(168, 23);
            this.textNapomena.TabIndex = 217;
            // 
            // lvlnapomena
            // 
            this.lvlnapomena.AutoSize = true;
            this.lvlnapomena.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lvlnapomena.Location = new System.Drawing.Point(624, 81);
            this.lvlnapomena.Name = "lvlnapomena";
            this.lvlnapomena.Size = new System.Drawing.Size(81, 17);
            this.lvlnapomena.TabIndex = 218;
            this.lvlnapomena.Text = "Napomena:";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(294, 52);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(23, 22);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 216;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // chbDO
            // 
            this.chbDO.AutoSize = true;
            this.chbDO.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chbDO.Location = new System.Drawing.Point(581, 56);
            this.chbDO.Name = "chbDO";
            this.chbDO.Size = new System.Drawing.Size(15, 14);
            this.chbDO.TabIndex = 68;
            this.chbDO.UseVisualStyleBackColor = true;
            // 
            // chbSifra
            // 
            this.chbSifra.AutoSize = true;
            this.chbSifra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chbSifra.Location = new System.Drawing.Point(6, 22);
            this.chbSifra.Name = "chbSifra";
            this.chbSifra.Size = new System.Drawing.Size(15, 14);
            this.chbSifra.TabIndex = 67;
            this.chbSifra.UseVisualStyleBackColor = true;
            this.chbSifra.Visible = false;
            // 
            // chbValuta
            // 
            this.chbValuta.AutoSize = true;
            this.chbValuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chbValuta.Location = new System.Drawing.Point(295, 80);
            this.chbValuta.Name = "chbValuta";
            this.chbValuta.Size = new System.Drawing.Size(15, 14);
            this.chbValuta.TabIndex = 65;
            this.chbValuta.UseVisualStyleBackColor = true;
            // 
            // chbOD
            // 
            this.chbOD.AutoSize = true;
            this.chbOD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chbOD.Location = new System.Drawing.Point(581, 30);
            this.chbOD.Name = "chbOD";
            this.chbOD.Size = new System.Drawing.Size(15, 14);
            this.chbOD.TabIndex = 72;
            this.chbOD.UseVisualStyleBackColor = true;
            // 
            // chbIzradio
            // 
            this.chbIzradio.AutoSize = true;
            this.chbIzradio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chbIzradio.Location = new System.Drawing.Point(880, 54);
            this.chbIzradio.Name = "chbIzradio";
            this.chbIzradio.Size = new System.Drawing.Size(15, 14);
            this.chbIzradio.TabIndex = 70;
            this.chbIzradio.UseVisualStyleBackColor = true;
            // 
            // chbBroj
            // 
            this.chbBroj.AutoSize = true;
            this.chbBroj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chbBroj.Location = new System.Drawing.Point(297, 31);
            this.chbBroj.Name = "chbBroj";
            this.chbBroj.Size = new System.Drawing.Size(15, 14);
            this.chbBroj.TabIndex = 69;
            this.chbBroj.UseVisualStyleBackColor = true;
            // 
            // txtPartner
            // 
            this.txtPartner.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtPartner.Location = new System.Drawing.Point(123, 51);
            this.txtPartner.Name = "txtPartner";
            this.txtPartner.Size = new System.Drawing.Size(168, 23);
            this.txtPartner.TabIndex = 50;
            this.txtPartner.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartner_KeyDown);
            // 
            // cbIzradio
            // 
            this.cbIzradio.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbIzradio.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbIzradio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIzradio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbIzradio.FormattingEnabled = true;
            this.cbIzradio.Location = new System.Drawing.Point(709, 49);
            this.cbIzradio.Name = "cbIzradio";
            this.cbIzradio.Size = new System.Drawing.Size(168, 24);
            this.cbIzradio.TabIndex = 63;
            // 
            // cbValuta
            // 
            this.cbValuta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbValuta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbValuta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbValuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbValuta.FormattingEnabled = true;
            this.cbValuta.Location = new System.Drawing.Point(123, 76);
            this.cbValuta.Name = "cbValuta";
            this.cbValuta.Size = new System.Drawing.Size(168, 24);
            this.cbValuta.TabIndex = 62;
            // 
            // dtpDO
            // 
            this.dtpDO.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDO.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDO.Location = new System.Drawing.Point(410, 49);
            this.dtpDO.Name = "dtpDO";
            this.dtpDO.Size = new System.Drawing.Size(168, 23);
            this.dtpDO.TabIndex = 61;
            // 
            // txtBroj
            // 
            this.txtBroj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtBroj.Location = new System.Drawing.Point(123, 26);
            this.txtBroj.Name = "txtBroj";
            this.txtBroj.Size = new System.Drawing.Size(168, 23);
            this.txtBroj.TabIndex = 51;
            // 
            // dtpOD
            // 
            this.dtpOD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpOD.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpOD.Location = new System.Drawing.Point(410, 23);
            this.dtpOD.Name = "dtpOD";
            this.dtpOD.Size = new System.Drawing.Size(168, 23);
            this.dtpOD.TabIndex = 60;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(22, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 17);
            this.label1.TabIndex = 52;
            this.label1.Text = "Broj fakture:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(23, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 17);
            this.label5.TabIndex = 54;
            this.label5.Text = "Valuta:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(323, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 17);
            this.label4.TabIndex = 59;
            this.label4.Text = "Do datuma:";
            // 
            // lbIzradio
            // 
            this.lbIzradio.AutoSize = true;
            this.lbIzradio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lbIzradio.Location = new System.Drawing.Point(624, 53);
            this.lbIzradio.Name = "lbIzradio";
            this.lbIzradio.Size = new System.Drawing.Size(81, 17);
            this.lbIzradio.TabIndex = 55;
            this.lbIzradio.Text = "Izradio zap:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(22, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 17);
            this.label2.TabIndex = 57;
            this.label2.Text = "Šifra partnera:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(323, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 17);
            this.label3.TabIndex = 53;
            this.label3.Text = "Od datuma:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(906, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(71, 65);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox1, "Traži");
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.pic_MouseEnter);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pic_MouseLeave);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.Image = global::PCPOS.Properties.Resources.Actions_application_exit_icon;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(860, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 38);
            this.button1.TabIndex = 21;
            this.button1.Text = "Izlaz      ";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnUrediSifru
            // 
            this.btnUrediSifru.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnUrediSifru.Image = global::PCPOS.Properties.Resources.edit_icon;
            this.btnUrediSifru.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUrediSifru.Location = new System.Drawing.Point(12, 12);
            this.btnUrediSifru.Name = "btnUrediSifru";
            this.btnUrediSifru.Size = new System.Drawing.Size(130, 38);
            this.btnUrediSifru.TabIndex = 20;
            this.btnUrediSifru.Text = "Uredi fakturu";
            this.btnUrediSifru.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUrediSifru.UseVisualStyleBackColor = true;
            this.btnUrediSifru.Click += new System.EventHandler(this.btnUrediSifru_Click);
            // 
            // btnSveFakture
            // 
            this.btnSveFakture.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSveFakture.Image = global::PCPOS.Properties.Resources.print_printer;
            this.btnSveFakture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSveFakture.Location = new System.Drawing.Point(148, 12);
            this.btnSveFakture.Name = "btnSveFakture";
            this.btnSveFakture.Size = new System.Drawing.Size(130, 38);
            this.btnSveFakture.TabIndex = 19;
            this.btnSveFakture.Text = "Ispis fakture ";
            this.btnSveFakture.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSveFakture.UseVisualStyleBackColor = true;
            this.btnSveFakture.Click += new System.EventHandler(this.btnSveFakture_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.AliceBlue;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView1.GridColor = System.Drawing.Color.Gainsboro;
            this.dataGridView1.Location = new System.Drawing.Point(12, 662);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(980, 164);
            this.dataGridView1.TabIndex = 70;
            this.dataGridView1.Visible = false;
            // 
            // chbSkladiste
            // 
            this.chbSkladiste.AutoSize = true;
            this.chbSkladiste.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chbSkladiste.Location = new System.Drawing.Point(880, 27);
            this.chbSkladiste.Name = "chbSkladiste";
            this.chbSkladiste.Size = new System.Drawing.Size(15, 14);
            this.chbSkladiste.TabIndex = 225;
            this.chbSkladiste.UseVisualStyleBackColor = true;
            // 
            // cmbSkladiste
            // 
            this.cmbSkladiste.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbSkladiste.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSkladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSkladiste.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cmbSkladiste.FormattingEnabled = true;
            this.cmbSkladiste.Location = new System.Drawing.Point(709, 22);
            this.cmbSkladiste.Name = "cmbSkladiste";
            this.cmbSkladiste.Size = new System.Drawing.Size(168, 24);
            this.cmbSkladiste.TabIndex = 224;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(624, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 17);
            this.label7.TabIndex = 223;
            this.label7.Text = "Skladište:";
            // 
            // broj
            // 
            this.broj.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.broj.FillWeight = 70F;
            this.broj.HeaderText = "Broj";
            this.broj.Name = "broj";
            this.broj.ReadOnly = true;
            this.broj.Width = 70;
            // 
            // datum
            // 
            this.datum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.datum.FillWeight = 81.55006F;
            this.datum.HeaderText = "Datum";
            this.datum.Name = "datum";
            this.datum.ReadOnly = true;
            // 
            // valuta
            // 
            this.valuta.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.valuta.FillWeight = 150F;
            this.valuta.HeaderText = "Valuta";
            this.valuta.Name = "valuta";
            this.valuta.ReadOnly = true;
            this.valuta.Width = 80;
            // 
            // sifra_partnera
            // 
            this.sifra_partnera.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.sifra_partnera.HeaderText = "Šifra partnera";
            this.sifra_partnera.Name = "sifra_partnera";
            this.sifra_partnera.ReadOnly = true;
            this.sifra_partnera.Visible = false;
            this.sifra_partnera.Width = 80;
            // 
            // partner
            // 
            this.partner.FillWeight = 81.55006F;
            this.partner.HeaderText = "Partner";
            this.partner.Name = "partner";
            this.partner.ReadOnly = true;
            // 
            // broj_ispisa
            // 
            this.broj_ispisa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.broj_ispisa.FillWeight = 80F;
            this.broj_ispisa.HeaderText = "BR ispisa";
            this.broj_ispisa.Name = "broj_ispisa";
            this.broj_ispisa.ReadOnly = true;
            this.broj_ispisa.Visible = false;
            this.broj_ispisa.Width = 80;
            // 
            // ukupno
            // 
            this.ukupno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ukupno.HeaderText = "Ukupno";
            this.ukupno.Name = "ukupno";
            this.ukupno.ReadOnly = true;
            // 
            // storno
            // 
            this.storno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.storno.FillWeight = 50F;
            this.storno.HeaderText = "Storno";
            this.storno.Name = "storno";
            this.storno.ReadOnly = true;
            this.storno.Visible = false;
            this.storno.Width = 50;
            // 
            // id_ducan
            // 
            this.id_ducan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.id_ducan.FillWeight = 50F;
            this.id_ducan.HeaderText = "Poslovnica";
            this.id_ducan.Name = "id_ducan";
            this.id_ducan.ReadOnly = true;
            this.id_ducan.Width = 80;
            // 
            // skladiste
            // 
            this.skladiste.HeaderText = "Skladište";
            this.skladiste.Name = "skladiste";
            this.skladiste.ReadOnly = true;
            // 
            // placeno
            // 
            this.placeno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.placeno.FillWeight = 150F;
            this.placeno.HeaderText = "Otpremnica";
            this.placeno.Name = "placeno";
            this.placeno.ReadOnly = true;
            this.placeno.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.placeno.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.placeno.Width = 150;
            // 
            // dgv_partner
            // 
            this.dgv_partner.HeaderText = "dgv_partner";
            this.dgv_partner.Name = "dgv_partner";
            this.dgv_partner.ReadOnly = true;
            this.dgv_partner.Visible = false;
            // 
            // id_kasa
            // 
            this.id_kasa.HeaderText = "id_kasa";
            this.id_kasa.Name = "id_kasa";
            this.id_kasa.ReadOnly = true;
            this.id_kasa.Visible = false;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button2.Image = global::PCPOS.Properties.Resources.print_printer;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(284, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(162, 38);
            this.button2.TabIndex = 71;
            this.button2.Text = "Ispis otpremnice";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // frmSveFaktureBezRobeNase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(1004, 837);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnUrediSifru);
            this.Controls.Add(this.btnSveFakture);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgv);
            this.Name = "frmSveFaktureBezRobeNase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sve fakture";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSveFakture_FormClosing);
            this.Load += new System.EventHandler(this.frmSveFakture_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox chbDO;
        private System.Windows.Forms.CheckBox chbSifra;
        private System.Windows.Forms.CheckBox chbValuta;
        private System.Windows.Forms.CheckBox chbOD;
        private System.Windows.Forms.CheckBox chbIzradio;
        private System.Windows.Forms.CheckBox chbBroj;
        public System.Windows.Forms.TextBox txtPartner;
        private System.Windows.Forms.ComboBox cbIzradio;
        private System.Windows.Forms.ComboBox cbValuta;
        private System.Windows.Forms.DateTimePicker dtpDO;
        public System.Windows.Forms.TextBox txtBroj;
        private System.Windows.Forms.DateTimePicker dtpOD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbIzradio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnUrediSifru;
        private System.Windows.Forms.Button btnSveFakture;
		private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.CheckBox chbNapomenackb;
        public System.Windows.Forms.TextBox textNapomena;
        private System.Windows.Forms.Label lvlnapomena;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox chbPoslovnica;
        private System.Windows.Forms.ComboBox cbPoslovnica;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chbSkladiste;
        private System.Windows.Forms.ComboBox cmbSkladiste;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum;
        private System.Windows.Forms.DataGridViewTextBoxColumn valuta;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra_partnera;
        private System.Windows.Forms.DataGridViewTextBoxColumn partner;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj_ispisa;
        private System.Windows.Forms.DataGridViewTextBoxColumn ukupno;
        private System.Windows.Forms.DataGridViewTextBoxColumn storno;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_ducan;
        private System.Windows.Forms.DataGridViewTextBoxColumn skladiste;
        private System.Windows.Forms.DataGridViewButtonColumn placeno;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_partner;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_kasa;
        private System.Windows.Forms.Button button2;
    }
}