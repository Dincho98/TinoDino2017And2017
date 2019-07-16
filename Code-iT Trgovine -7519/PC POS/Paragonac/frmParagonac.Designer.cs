namespace PCPOS
{
    partial class frmParagonac
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmParagonac));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblBrojRac = new System.Windows.Forms.Label();
            this.GroupBoxUkupnoZaNaplatiti = new System.Windows.Forms.GroupBox();
            this.lblUkupno = new System.Windows.Forms.Label();
            this.lblUkupnoZanemari = new System.Windows.Forms.Label();
            this.btnAlati = new System.Windows.Forms.Button();
            this.btnPartner = new System.Windows.Forms.Button();
            this.btnOdustaniSve = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtImePartnera = new System.Windows.Forms.TextBox();
            this.gbCB = new System.Windows.Forms.GroupBox();
            this.btnOdustaniCB = new System.Windows.Forms.Button();
            this.lblLYvlasnik = new System.Windows.Forms.Label();
            this.lblLYkartica = new System.Windows.Forms.Label();
            this.txtPodaciOvlasnikuCB = new System.Windows.Forms.TextBox();
            this.txtBrojKarticeCB = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.gbSB = new System.Windows.Forms.GroupBox();
            this.btnOdustaniSB = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPodaciOvlasnikuSB = new System.Windows.Forms.TextBox();
            this.txtBrojKarticeSB = new System.Windows.Forms.TextBox();
            this.gbPO = new System.Windows.Forms.GroupBox();
            this.btnOdustaniPO = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPodaciOvlasnikuPO = new System.Windows.Forms.TextBox();
            this.txtBrojKarticePO = new System.Windows.Forms.TextBox();
            this.lblSkladiste = new System.Windows.Forms.Label();
            this.backgroundWorkerConnectPos = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerDisconnect = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerSendToDisplay = new System.ComponentModel.BackgroundWorker();
            this.btnObrisiStavku = new System.Windows.Forms.Button();
            this.dgv = new PCPOS.frmParagonac.MyDataGrid();
            this.RB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jmj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cijena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.popust = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.porez = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOdjava = new System.Windows.Forms.Button();
            this.btnTrazi = new System.Windows.Forms.Button();
            this.btnKartica = new System.Windows.Forms.Button();
            this.btnGotovina = new System.Windows.Forms.Button();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnPregledRacuna = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.GroupBoxUkupnoZaNaplatiti.SuspendLayout();
            this.gbCB.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbSB.SuspendLayout();
            this.gbPO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblBrojRac
            // 
            this.lblBrojRac.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBrojRac.AutoSize = true;
            this.lblBrojRac.BackColor = System.Drawing.Color.Transparent;
            this.lblBrojRac.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblBrojRac.ForeColor = System.Drawing.Color.Black;
            this.lblBrojRac.Location = new System.Drawing.Point(3, 0);
            this.lblBrojRac.Name = "lblBrojRac";
            this.lblBrojRac.Size = new System.Drawing.Size(170, 20);
            this.lblBrojRac.TabIndex = 13;
            this.lblBrojRac.Text = "Broj računa 0002/2012";
            this.lblBrojRac.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBoxUkupnoZaNaplatiti
            // 
            this.GroupBoxUkupnoZaNaplatiti.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxUkupnoZaNaplatiti.BackColor = System.Drawing.Color.Transparent;
            this.GroupBoxUkupnoZaNaplatiti.Controls.Add(this.lblUkupno);
            this.GroupBoxUkupnoZaNaplatiti.Controls.Add(this.lblUkupnoZanemari);
            this.GroupBoxUkupnoZaNaplatiti.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.GroupBoxUkupnoZaNaplatiti.Location = new System.Drawing.Point(481, 563);
            this.GroupBoxUkupnoZaNaplatiti.Name = "GroupBoxUkupnoZaNaplatiti";
            this.GroupBoxUkupnoZaNaplatiti.Size = new System.Drawing.Size(234, 88);
            this.GroupBoxUkupnoZaNaplatiti.TabIndex = 93;
            this.GroupBoxUkupnoZaNaplatiti.TabStop = false;
            this.GroupBoxUkupnoZaNaplatiti.Text = "Ukupno za naplatiti:";
            // 
            // lblUkupno
            // 
            this.lblUkupno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUkupno.AutoSize = true;
            this.lblUkupno.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold);
            this.lblUkupno.Location = new System.Drawing.Point(2, 33);
            this.lblUkupno.Name = "lblUkupno";
            this.lblUkupno.Size = new System.Drawing.Size(163, 46);
            this.lblUkupno.TabIndex = 55;
            this.lblUkupno.Text = "0,00 Kn";
            // 
            // lblUkupnoZanemari
            // 
            this.lblUkupnoZanemari.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUkupnoZanemari.AutoSize = true;
            this.lblUkupnoZanemari.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblUkupnoZanemari.Location = new System.Drawing.Point(458, 574);
            this.lblUkupnoZanemari.Name = "lblUkupnoZanemari";
            this.lblUkupnoZanemari.Size = new System.Drawing.Size(73, 31);
            this.lblUkupnoZanemari.TabIndex = 54;
            this.lblUkupnoZanemari.Text = "0 Kn";
            // 
            // btnAlati
            // 
            this.btnAlati.BackColor = System.Drawing.Color.Transparent;
            this.btnAlati.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAlati.BackgroundImage")));
            this.btnAlati.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAlati.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAlati.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnAlati.FlatAppearance.BorderSize = 0;
            this.btnAlati.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnAlati.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSlateGray;
            this.btnAlati.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSlateGray;
            this.btnAlati.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAlati.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnAlati.Location = new System.Drawing.Point(585, 17);
            this.btnAlati.Name = "btnAlati";
            this.btnAlati.Size = new System.Drawing.Size(129, 96);
            this.btnAlati.TabIndex = 96;
            this.btnAlati.Text = "Opcije F10";
            this.btnAlati.UseVisualStyleBackColor = false;
            this.btnAlati.Click += new System.EventHandler(this.btnAlati_Click);
            this.btnAlati.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmParagonac_KeyDown);
            this.btnAlati.MouseEnter += new System.EventHandler(this.pic_MouseEnter);
            this.btnAlati.MouseLeave += new System.EventHandler(this.pic_MouseLeave);
            // 
            // btnPartner
            // 
            this.btnPartner.BackColor = System.Drawing.Color.Transparent;
            this.btnPartner.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPartner.BackgroundImage")));
            this.btnPartner.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPartner.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPartner.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnPartner.FlatAppearance.BorderSize = 0;
            this.btnPartner.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnPartner.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSlateGray;
            this.btnPartner.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSlateGray;
            this.btnPartner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPartner.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnPartner.Location = new System.Drawing.Point(303, 18);
            this.btnPartner.Name = "btnPartner";
            this.btnPartner.Size = new System.Drawing.Size(129, 96);
            this.btnPartner.TabIndex = 96;
            this.btnPartner.Text = "Kupac F4";
            this.btnPartner.UseVisualStyleBackColor = false;
            this.btnPartner.Click += new System.EventHandler(this.btnPartner_Click);
            this.btnPartner.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmParagonac_KeyDown);
            this.btnPartner.MouseEnter += new System.EventHandler(this.pic_MouseEnter);
            this.btnPartner.MouseLeave += new System.EventHandler(this.pic_MouseLeave);
            // 
            // btnOdustaniSve
            // 
            this.btnOdustaniSve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOdustaniSve.BackColor = System.Drawing.Color.Transparent;
            this.btnOdustaniSve.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOdustaniSve.BackgroundImage")));
            this.btnOdustaniSve.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOdustaniSve.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOdustaniSve.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnOdustaniSve.FlatAppearance.BorderSize = 0;
            this.btnOdustaniSve.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnOdustaniSve.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSlateGray;
            this.btnOdustaniSve.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSlateGray;
            this.btnOdustaniSve.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOdustaniSve.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnOdustaniSve.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnOdustaniSve.Location = new System.Drawing.Point(324, 570);
            this.btnOdustaniSve.Name = "btnOdustaniSve";
            this.btnOdustaniSve.Size = new System.Drawing.Size(140, 88);
            this.btnOdustaniSve.TabIndex = 96;
            this.btnOdustaniSve.Text = "Odustani F7";
            this.btnOdustaniSve.UseVisualStyleBackColor = false;
            this.btnOdustaniSve.Click += new System.EventHandler(this.button15_Click);
            this.btnOdustaniSve.MouseEnter += new System.EventHandler(this.pic_MouseEnter);
            this.btnOdustaniSve.MouseLeave += new System.EventHandler(this.pic_MouseLeave);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(731, 567);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(105, 17);
            this.label10.TabIndex = 1;
            this.label10.Text = "Ime partnera:";
            // 
            // txtImePartnera
            // 
            this.txtImePartnera.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImePartnera.BackColor = System.Drawing.Color.White;
            this.txtImePartnera.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtImePartnera.Location = new System.Drawing.Point(734, 587);
            this.txtImePartnera.Name = "txtImePartnera";
            this.txtImePartnera.ReadOnly = true;
            this.txtImePartnera.Size = new System.Drawing.Size(258, 29);
            this.txtImePartnera.TabIndex = 0;
            this.txtImePartnera.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmParagonac_KeyDown);
            // 
            // gbCB
            // 
            this.gbCB.BackColor = System.Drawing.Color.Transparent;
            this.gbCB.Controls.Add(this.btnOdustaniCB);
            this.gbCB.Controls.Add(this.lblLYvlasnik);
            this.gbCB.Controls.Add(this.lblLYkartica);
            this.gbCB.Controls.Add(this.txtPodaciOvlasnikuCB);
            this.gbCB.Controls.Add(this.txtBrojKarticeCB);
            this.gbCB.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gbCB.Location = new System.Drawing.Point(3, 3);
            this.gbCB.Margin = new System.Windows.Forms.Padding(3, 3, 3, 15);
            this.gbCB.Name = "gbCB";
            this.gbCB.Size = new System.Drawing.Size(267, 164);
            this.gbCB.TabIndex = 95;
            this.gbCB.TabStop = false;
            this.gbCB.Text = "Cashback program";
            // 
            // btnOdustaniCB
            // 
            this.btnOdustaniCB.Location = new System.Drawing.Point(171, 52);
            this.btnOdustaniCB.Name = "btnOdustaniCB";
            this.btnOdustaniCB.Size = new System.Drawing.Size(73, 29);
            this.btnOdustaniCB.TabIndex = 2;
            this.btnOdustaniCB.Text = "Odustani";
            this.btnOdustaniCB.UseVisualStyleBackColor = true;
            this.btnOdustaniCB.Click += new System.EventHandler(this.btnOdustaniCB_Click);
            // 
            // lblLYvlasnik
            // 
            this.lblLYvlasnik.AutoSize = true;
            this.lblLYvlasnik.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblLYvlasnik.Location = new System.Drawing.Point(19, 91);
            this.lblLYvlasnik.Name = "lblLYvlasnik";
            this.lblLYvlasnik.Size = new System.Drawing.Size(112, 17);
            this.lblLYvlasnik.TabIndex = 1;
            this.lblLYvlasnik.Text = "Podaci o vlasniku";
            // 
            // lblLYkartica
            // 
            this.lblLYkartica.AutoSize = true;
            this.lblLYkartica.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblLYkartica.Location = new System.Drawing.Point(17, 35);
            this.lblLYkartica.Name = "lblLYkartica";
            this.lblLYkartica.Size = new System.Drawing.Size(77, 17);
            this.lblLYkartica.TabIndex = 1;
            this.lblLYkartica.Text = "Broj kartice";
            // 
            // txtPodaciOvlasnikuCB
            // 
            this.txtPodaciOvlasnikuCB.BackColor = System.Drawing.Color.White;
            this.txtPodaciOvlasnikuCB.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtPodaciOvlasnikuCB.Location = new System.Drawing.Point(20, 109);
            this.txtPodaciOvlasnikuCB.Name = "txtPodaciOvlasnikuCB";
            this.txtPodaciOvlasnikuCB.ReadOnly = true;
            this.txtPodaciOvlasnikuCB.Size = new System.Drawing.Size(225, 27);
            this.txtPodaciOvlasnikuCB.TabIndex = 0;
            // 
            // txtBrojKarticeCB
            // 
            this.txtBrojKarticeCB.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtBrojKarticeCB.Location = new System.Drawing.Point(19, 53);
            this.txtBrojKarticeCB.Name = "txtBrojKarticeCB";
            this.txtBrojKarticeCB.Size = new System.Drawing.Size(152, 27);
            this.txtBrojKarticeCB.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(276, 173);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(268, 156);
            this.panel1.TabIndex = 98;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(8, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 17);
            this.label4.TabIndex = 106;
            this.label4.Text = "Dućan: 01";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(8, 112);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 17);
            this.label6.TabIndex = 107;
            this.label6.Text = "Blagajnik:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(8, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 105;
            this.label5.Text = "Kupac:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(8, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 103;
            this.label3.Text = "Kasa: 01";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 17);
            this.label1.TabIndex = 104;
            this.label1.Text = "Datum: 27.11.2010";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(-150, 11);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(118, 252);
            this.flowLayoutPanel1.TabIndex = 99;
            // 
            // gbSB
            // 
            this.gbSB.BackColor = System.Drawing.Color.Transparent;
            this.gbSB.Controls.Add(this.btnOdustaniSB);
            this.gbSB.Controls.Add(this.label2);
            this.gbSB.Controls.Add(this.label7);
            this.gbSB.Controls.Add(this.txtPodaciOvlasnikuSB);
            this.gbSB.Controls.Add(this.txtBrojKarticeSB);
            this.gbSB.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gbSB.Location = new System.Drawing.Point(550, 3);
            this.gbSB.Name = "gbSB";
            this.gbSB.Size = new System.Drawing.Size(267, 164);
            this.gbSB.TabIndex = 95;
            this.gbSB.TabStop = false;
            this.gbSB.Text = "Skupljanje bodova";
            // 
            // btnOdustaniSB
            // 
            this.btnOdustaniSB.Location = new System.Drawing.Point(171, 52);
            this.btnOdustaniSB.Name = "btnOdustaniSB";
            this.btnOdustaniSB.Size = new System.Drawing.Size(73, 29);
            this.btnOdustaniSB.TabIndex = 2;
            this.btnOdustaniSB.Text = "Odustani";
            this.btnOdustaniSB.UseVisualStyleBackColor = true;
            this.btnOdustaniSB.Click += new System.EventHandler(this.btnOdustaniSB_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(19, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Podaci o vlasniku";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(17, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 17);
            this.label7.TabIndex = 1;
            this.label7.Text = "Broj kartice";
            // 
            // txtPodaciOvlasnikuSB
            // 
            this.txtPodaciOvlasnikuSB.BackColor = System.Drawing.Color.White;
            this.txtPodaciOvlasnikuSB.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtPodaciOvlasnikuSB.Location = new System.Drawing.Point(20, 109);
            this.txtPodaciOvlasnikuSB.Name = "txtPodaciOvlasnikuSB";
            this.txtPodaciOvlasnikuSB.ReadOnly = true;
            this.txtPodaciOvlasnikuSB.Size = new System.Drawing.Size(225, 27);
            this.txtPodaciOvlasnikuSB.TabIndex = 0;
            // 
            // txtBrojKarticeSB
            // 
            this.txtBrojKarticeSB.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtBrojKarticeSB.Location = new System.Drawing.Point(19, 53);
            this.txtBrojKarticeSB.Name = "txtBrojKarticeSB";
            this.txtBrojKarticeSB.Size = new System.Drawing.Size(152, 27);
            this.txtBrojKarticeSB.TabIndex = 0;
            // 
            // gbPO
            // 
            this.gbPO.BackColor = System.Drawing.Color.Transparent;
            this.gbPO.Controls.Add(this.btnOdustaniPO);
            this.gbPO.Controls.Add(this.label8);
            this.gbPO.Controls.Add(this.label9);
            this.gbPO.Controls.Add(this.txtPodaciOvlasnikuPO);
            this.gbPO.Controls.Add(this.txtBrojKarticePO);
            this.gbPO.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gbPO.Location = new System.Drawing.Point(3, 173);
            this.gbPO.Name = "gbPO";
            this.gbPO.Size = new System.Drawing.Size(267, 164);
            this.gbPO.TabIndex = 95;
            this.gbPO.TabStop = false;
            this.gbPO.Text = "Popust sa prethodnog računa";
            // 
            // btnOdustaniPO
            // 
            this.btnOdustaniPO.Location = new System.Drawing.Point(171, 52);
            this.btnOdustaniPO.Name = "btnOdustaniPO";
            this.btnOdustaniPO.Size = new System.Drawing.Size(73, 29);
            this.btnOdustaniPO.TabIndex = 2;
            this.btnOdustaniPO.Text = "Odustani";
            this.btnOdustaniPO.UseVisualStyleBackColor = true;
            this.btnOdustaniPO.Click += new System.EventHandler(this.btnOdustaniPO_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(19, 91);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 17);
            this.label8.TabIndex = 1;
            this.label8.Text = "Podaci o vlasniku";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.Location = new System.Drawing.Point(17, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 17);
            this.label9.TabIndex = 1;
            this.label9.Text = "Broj sa računa";
            // 
            // txtPodaciOvlasnikuPO
            // 
            this.txtPodaciOvlasnikuPO.BackColor = System.Drawing.Color.White;
            this.txtPodaciOvlasnikuPO.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtPodaciOvlasnikuPO.Location = new System.Drawing.Point(20, 109);
            this.txtPodaciOvlasnikuPO.Name = "txtPodaciOvlasnikuPO";
            this.txtPodaciOvlasnikuPO.ReadOnly = true;
            this.txtPodaciOvlasnikuPO.Size = new System.Drawing.Size(225, 27);
            this.txtPodaciOvlasnikuPO.TabIndex = 0;
            // 
            // txtBrojKarticePO
            // 
            this.txtBrojKarticePO.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtBrojKarticePO.Location = new System.Drawing.Point(19, 53);
            this.txtBrojKarticePO.Name = "txtBrojKarticePO";
            this.txtBrojKarticePO.Size = new System.Drawing.Size(152, 27);
            this.txtBrojKarticePO.TabIndex = 0;
            // 
            // lblSkladiste
            // 
            this.lblSkladiste.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSkladiste.AutoSize = true;
            this.lblSkladiste.BackColor = System.Drawing.Color.Transparent;
            this.lblSkladiste.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblSkladiste.ForeColor = System.Drawing.Color.Lime;
            this.lblSkladiste.Location = new System.Drawing.Point(22, 543);
            this.lblSkladiste.Name = "lblSkladiste";
            this.lblSkladiste.Size = new System.Drawing.Size(0, 20);
            this.lblSkladiste.TabIndex = 101;
            // 
            // backgroundWorkerSendToDisplay
            // 
            this.backgroundWorkerSendToDisplay.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerSendToDisplay_DoWork);
            // 
            // btnObrisiStavku
            // 
            this.btnObrisiStavku.BackColor = System.Drawing.Color.Transparent;
            this.btnObrisiStavku.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnObrisiStavku.BackgroundImage")));
            this.btnObrisiStavku.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnObrisiStavku.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnObrisiStavku.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnObrisiStavku.FlatAppearance.BorderSize = 0;
            this.btnObrisiStavku.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnObrisiStavku.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSlateGray;
            this.btnObrisiStavku.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSlateGray;
            this.btnObrisiStavku.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnObrisiStavku.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnObrisiStavku.Location = new System.Drawing.Point(444, 18);
            this.btnObrisiStavku.Name = "btnObrisiStavku";
            this.btnObrisiStavku.Size = new System.Drawing.Size(129, 96);
            this.btnObrisiStavku.TabIndex = 96;
            this.btnObrisiStavku.Text = "Obriši stavku F9";
            this.btnObrisiStavku.UseVisualStyleBackColor = false;
            this.btnObrisiStavku.Click += new System.EventHandler(this.btnObrisiStavku_Click);
            this.btnObrisiStavku.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmParagonac_KeyDown);
            this.btnObrisiStavku.MouseEnter += new System.EventHandler(this.pic_MouseEnter);
            this.btnObrisiStavku.MouseLeave += new System.EventHandler(this.pic_MouseLeave);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersHeight = 30;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RB,
            this.naziv,
            this.jmj,
            this.cijena,
            this.kolicina,
            this.popust,
            this.porez,
            this.iznos,
            this.vpc,
            this.mpc});
            this.dgv.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.SlateGray;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv.GridColor = System.Drawing.Color.Gainsboro;
            this.dgv.Location = new System.Drawing.Point(24, 161);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(968, 379);
            this.dgv.TabIndex = 0;
            this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            this.dgv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmParagonac_KeyDown);
            // 
            // RB
            // 
            this.RB.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.RB.FillWeight = 30F;
            this.RB.HeaderText = "RB";
            this.RB.Name = "RB";
            this.RB.Width = 30;
            // 
            // naziv
            // 
            this.naziv.FillWeight = 53.43952F;
            this.naziv.HeaderText = "NAZIV";
            this.naziv.Name = "naziv";
            // 
            // jmj
            // 
            this.jmj.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.jmj.FillWeight = 50F;
            this.jmj.HeaderText = "JMJ";
            this.jmj.Name = "jmj";
            this.jmj.Width = 50;
            // 
            // cijena
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cijena.DefaultCellStyle = dataGridViewCellStyle2;
            this.cijena.FillWeight = 17.37738F;
            this.cijena.HeaderText = "CIJENA";
            this.cijena.Name = "cijena";
            // 
            // kolicina
            // 
            this.kolicina.FillWeight = 17.37738F;
            this.kolicina.HeaderText = "KOLIČINA";
            this.kolicina.Name = "kolicina";
            // 
            // popust
            // 
            this.popust.FillWeight = 17.37738F;
            this.popust.HeaderText = "POPUST%";
            this.popust.Name = "popust";
            // 
            // porez
            // 
            this.porez.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.porez.FillWeight = 60F;
            this.porez.HeaderText = "POREZ";
            this.porez.Name = "porez";
            this.porez.Width = 60;
            // 
            // iznos
            // 
            this.iznos.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.iznos.DefaultCellStyle = dataGridViewCellStyle3;
            this.iznos.FillWeight = 17.37738F;
            this.iznos.HeaderText = "IZNOS";
            this.iznos.Name = "iznos";
            // 
            // vpc
            // 
            this.vpc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.vpc.HeaderText = "vpc";
            this.vpc.Name = "vpc";
            this.vpc.Visible = false;
            this.vpc.Width = 228;
            // 
            // mpc
            // 
            this.mpc.HeaderText = "mpc";
            this.mpc.Name = "mpc";
            this.mpc.Visible = false;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel2.Controls.Add(this.lblBrojRac);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(18, 140);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(696, 21);
            this.flowLayoutPanel2.TabIndex = 104;
            // 
            // btnOdjava
            // 
            this.btnOdjava.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOdjava.BackColor = System.Drawing.Color.Transparent;
            this.btnOdjava.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOdjava.BackgroundImage")));
            this.btnOdjava.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOdjava.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOdjava.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnOdjava.FlatAppearance.BorderSize = 0;
            this.btnOdjava.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnOdjava.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSlateGray;
            this.btnOdjava.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSlateGray;
            this.btnOdjava.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOdjava.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnOdjava.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOdjava.Location = new System.Drawing.Point(865, 17);
            this.btnOdjava.Name = "btnOdjava";
            this.btnOdjava.Size = new System.Drawing.Size(127, 96);
            this.btnOdjava.TabIndex = 96;
            this.btnOdjava.Text = "Odjava ESC";
            this.btnOdjava.UseVisualStyleBackColor = false;
            this.btnOdjava.Click += new System.EventHandler(this.btnOdjava_Click);
            this.btnOdjava.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmParagonac_KeyDown);
            this.btnOdjava.MouseEnter += new System.EventHandler(this.pic_MouseEnter);
            this.btnOdjava.MouseLeave += new System.EventHandler(this.pic_MouseLeave);
            // 
            // btnTrazi
            // 
            this.btnTrazi.BackColor = System.Drawing.Color.Transparent;
            this.btnTrazi.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTrazi.BackgroundImage")));
            this.btnTrazi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTrazi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTrazi.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnTrazi.FlatAppearance.BorderSize = 0;
            this.btnTrazi.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnTrazi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSlateGray;
            this.btnTrazi.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSlateGray;
            this.btnTrazi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrazi.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnTrazi.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnTrazi.Location = new System.Drawing.Point(163, 18);
            this.btnTrazi.Name = "btnTrazi";
            this.btnTrazi.Size = new System.Drawing.Size(128, 95);
            this.btnTrazi.TabIndex = 2;
            this.btnTrazi.Text = "Traži F3";
            this.btnTrazi.UseVisualStyleBackColor = false;
            this.btnTrazi.Click += new System.EventHandler(this.btnTrazi_Click);
            this.btnTrazi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmParagonac_KeyDown);
            this.btnTrazi.MouseEnter += new System.EventHandler(this.pic_MouseEnter);
            this.btnTrazi.MouseLeave += new System.EventHandler(this.pic_MouseLeave);
            // 
            // btnKartica
            // 
            this.btnKartica.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnKartica.BackColor = System.Drawing.Color.Transparent;
            this.btnKartica.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnKartica.BackgroundImage")));
            this.btnKartica.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnKartica.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKartica.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnKartica.FlatAppearance.BorderSize = 0;
            this.btnKartica.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnKartica.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSlateGray;
            this.btnKartica.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSlateGray;
            this.btnKartica.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKartica.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnKartica.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnKartica.Location = new System.Drawing.Point(174, 570);
            this.btnKartica.Name = "btnKartica";
            this.btnKartica.Size = new System.Drawing.Size(140, 88);
            this.btnKartica.TabIndex = 94;
            this.btnKartica.Text = "Kartica F6";
            this.btnKartica.UseVisualStyleBackColor = false;
            this.btnKartica.Click += new System.EventHandler(this.btnKartica_Click);
            this.btnKartica.MouseEnter += new System.EventHandler(this.pic_MouseEnter);
            this.btnKartica.MouseLeave += new System.EventHandler(this.pic_MouseLeave);
            // 
            // btnGotovina
            // 
            this.btnGotovina.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGotovina.BackColor = System.Drawing.Color.Transparent;
            this.btnGotovina.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGotovina.BackgroundImage")));
            this.btnGotovina.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGotovina.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGotovina.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnGotovina.FlatAppearance.BorderSize = 0;
            this.btnGotovina.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnGotovina.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSlateGray;
            this.btnGotovina.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSlateGray;
            this.btnGotovina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGotovina.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnGotovina.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnGotovina.Location = new System.Drawing.Point(25, 570);
            this.btnGotovina.Name = "btnGotovina";
            this.btnGotovina.Size = new System.Drawing.Size(140, 88);
            this.btnGotovina.TabIndex = 94;
            this.btnGotovina.Text = "Gotovina F5";
            this.btnGotovina.UseVisualStyleBackColor = false;
            this.btnGotovina.Click += new System.EventHandler(this.btnGotovina_Click);
            this.btnGotovina.MouseEnter += new System.EventHandler(this.pic_MouseEnter);
            this.btnGotovina.MouseLeave += new System.EventHandler(this.pic_MouseLeave);
            // 
            // btnInsert
            // 
            this.btnInsert.BackColor = System.Drawing.Color.Transparent;
            this.btnInsert.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnInsert.BackgroundImage")));
            this.btnInsert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnInsert.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInsert.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnInsert.FlatAppearance.BorderSize = 0;
            this.btnInsert.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnInsert.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSlateGray;
            this.btnInsert.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSlateGray;
            this.btnInsert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInsert.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnInsert.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnInsert.Location = new System.Drawing.Point(24, 17);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(128, 95);
            this.btnInsert.TabIndex = 105;
            this.btnInsert.Text = "Nova stavka (INSERT)";
            this.btnInsert.UseVisualStyleBackColor = false;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            this.btnInsert.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmParagonac_KeyDown);
            this.btnInsert.MouseEnter += new System.EventHandler(this.pic_MouseEnter);
            this.btnInsert.MouseHover += new System.EventHandler(this.pic_MouseLeave);
            // 
            // btnPregledRacuna
            // 
            this.btnPregledRacuna.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPregledRacuna.BackColor = System.Drawing.Color.Transparent;
            this.btnPregledRacuna.BackgroundImage = global::PCPOS.Properties.Resources.dff;
            this.btnPregledRacuna.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPregledRacuna.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPregledRacuna.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnPregledRacuna.FlatAppearance.BorderSize = 0;
            this.btnPregledRacuna.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSlateGray;
            this.btnPregledRacuna.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSlateGray;
            this.btnPregledRacuna.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPregledRacuna.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnPregledRacuna.Location = new System.Drawing.Point(724, 17);
            this.btnPregledRacuna.Name = "btnPregledRacuna";
            this.btnPregledRacuna.Size = new System.Drawing.Size(116, 95);
            this.btnPregledRacuna.TabIndex = 107;
            this.btnPregledRacuna.Text = "Pregledaj račun";
            this.btnPregledRacuna.UseVisualStyleBackColor = false;
            this.btnPregledRacuna.Click += new System.EventHandler(this.btnPregledRacuna_Click);
            this.btnPregledRacuna.MouseEnter += new System.EventHandler(this.pic_MouseEnter);
            this.btnPregledRacuna.MouseLeave += new System.EventHandler(this.pic_MouseLeave);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSlateGray;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSlateGray;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button1.Location = new System.Drawing.Point(853, 119);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 35);
            this.button1.TabIndex = 108;
            this.button1.Text = "Brzi unos klijenta";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // frmParagonac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(1006, 680);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnPregledRacuna);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.btnTrazi);
            this.Controls.Add(this.btnAlati);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnOdjava);
            this.Controls.Add(this.btnObrisiStavku);
            this.Controls.Add(this.txtImePartnera);
            this.Controls.Add(this.btnPartner);
            this.Controls.Add(this.lblSkladiste);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.btnOdustaniSve);
            this.Controls.Add(this.btnKartica);
            this.Controls.Add(this.btnGotovina);
            this.Controls.Add(this.GroupBoxUkupnoZaNaplatiti);
            this.Controls.Add(this.dgv);
            this.MinimumSize = new System.Drawing.Size(1022, 718);
            this.Name = "frmParagonac";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Paragonac prodaja";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmKasa_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmParagonac_KeyDown);
            this.GroupBoxUkupnoZaNaplatiti.ResumeLayout(false);
            this.GroupBoxUkupnoZaNaplatiti.PerformLayout();
            this.gbCB.ResumeLayout(false);
            this.gbCB.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbSB.ResumeLayout(false);
            this.gbSB.PerformLayout();
            this.gbPO.ResumeLayout(false);
            this.gbPO.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBrojRac;
        internal System.Windows.Forms.GroupBox GroupBoxUkupnoZaNaplatiti;
        internal System.Windows.Forms.Label lblUkupno;
        internal System.Windows.Forms.Label lblUkupnoZanemari;
        private System.Windows.Forms.Button btnGotovina;
        private System.Windows.Forms.Button btnKartica;
        private System.Windows.Forms.Button btnAlati;
        private System.Windows.Forms.Button btnPartner;
        private System.Windows.Forms.Button btnOdustaniSve;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtImePartnera;
        private System.Windows.Forms.GroupBox gbCB;
        private System.Windows.Forms.Label lblLYvlasnik;
        private System.Windows.Forms.Label lblLYkartica;
        private System.Windows.Forms.TextBox txtPodaciOvlasnikuCB;
        private System.Windows.Forms.TextBox txtBrojKarticeCB;
        private System.Windows.Forms.Button btnOdustaniCB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox gbSB;
        private System.Windows.Forms.Button btnOdustaniSB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPodaciOvlasnikuSB;
        private System.Windows.Forms.TextBox txtBrojKarticeSB;
        private System.Windows.Forms.GroupBox gbPO;
        private System.Windows.Forms.Button btnOdustaniPO;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPodaciOvlasnikuPO;
        private System.Windows.Forms.TextBox txtBrojKarticePO;
        private System.Windows.Forms.Label lblSkladiste;
        private System.ComponentModel.BackgroundWorker backgroundWorkerConnectPos;
        private System.ComponentModel.BackgroundWorker backgroundWorkerDisconnect;
        private System.ComponentModel.BackgroundWorker backgroundWorkerSendToDisplay;
        private System.Windows.Forms.Button btnObrisiStavku;
        private System.Windows.Forms.Button btnTrazi;
        private System.Windows.Forms.Button btnOdjava;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button btnInsert;
        private frmParagonac.MyDataGrid dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn RB;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewTextBoxColumn jmj;
        private System.Windows.Forms.DataGridViewTextBoxColumn cijena;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolicina;
        private System.Windows.Forms.DataGridViewTextBoxColumn popust;
        private System.Windows.Forms.DataGridViewTextBoxColumn porez;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos;
        private System.Windows.Forms.DataGridViewTextBoxColumn vpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn mpc;
        private System.Windows.Forms.Button btnPregledRacuna;
        private System.Windows.Forms.Button button1;
    }
}