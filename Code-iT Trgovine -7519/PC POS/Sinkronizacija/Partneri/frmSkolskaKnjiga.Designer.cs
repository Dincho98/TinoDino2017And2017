namespace PCPOS.Sinkronizacija.Partneri
{
    partial class frmSkolskaKnjiga
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSkolskaKnjiga));
            this.dgv = new System.Windows.Forms.DataGridView();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grupa = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.opis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grupa_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Podgrupa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Brand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jamstvo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VPC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MPC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stanje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Akcija = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LinkSlikeWeb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pdv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTraziPartnera = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNazivPartnera = new System.Windows.Forms.TextBox();
            this.txtSifraPartnera = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbSkladiste = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnKreirajXMLiSpremi = new System.Windows.Forms.Button();
            this.btnUcitajExcel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tab = new System.Windows.Forms.TabControl();
            this.txtUvozIzExcela = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPathExcel = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnTraziPartnera)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tab.SuspendLayout();
            this.txtUvozIzExcela.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sifra,
            this.naziv,
            this.grupa,
            this.id,
            this.opis,
            this.grupa_,
            this.Podgrupa,
            this.Brand,
            this.Jamstvo,
            this.VPC,
            this.MPC,
            this.Stanje,
            this.Akcija,
            this.LinkSlikeWeb,
            this.pdv});
            this.dgv.Location = new System.Drawing.Point(6, 22);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(635, 587);
            this.dgv.TabIndex = 23;
            // 
            // sifra
            // 
            this.sifra.HeaderText = "Šifra";
            this.sifra.Name = "sifra";
            // 
            // naziv
            // 
            this.naziv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.naziv.FillWeight = 250F;
            this.naziv.HeaderText = "Naziv";
            this.naziv.Name = "naziv";
            this.naziv.Width = 250;
            // 
            // grupa
            // 
            this.grupa.FillWeight = 260F;
            this.grupa.HeaderText = "Odaberi grupu";
            this.grupa.Name = "grupa";
            this.grupa.Width = 260;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // opis
            // 
            this.opis.HeaderText = "Opis";
            this.opis.Name = "opis";
            // 
            // grupa_
            // 
            this.grupa_.HeaderText = "Grupa";
            this.grupa_.Name = "grupa_";
            // 
            // Podgrupa
            // 
            this.Podgrupa.HeaderText = "Podgrupa";
            this.Podgrupa.Name = "Podgrupa";
            // 
            // Brand
            // 
            this.Brand.HeaderText = "Brand";
            this.Brand.Name = "Brand";
            // 
            // Jamstvo
            // 
            this.Jamstvo.HeaderText = "Jamstvo";
            this.Jamstvo.Name = "Jamstvo";
            // 
            // VPC
            // 
            this.VPC.HeaderText = "VPC";
            this.VPC.Name = "VPC";
            // 
            // MPC
            // 
            this.MPC.HeaderText = "MPC";
            this.MPC.Name = "MPC";
            // 
            // Stanje
            // 
            this.Stanje.HeaderText = "Stanje";
            this.Stanje.Name = "Stanje";
            // 
            // Akcija
            // 
            this.Akcija.HeaderText = "Akcija";
            this.Akcija.Name = "Akcija";
            // 
            // LinkSlikeWeb
            // 
            this.LinkSlikeWeb.HeaderText = "LinkSlikeWeb";
            this.LinkSlikeWeb.Name = "LinkSlikeWeb";
            // 
            // pdv
            // 
            this.pdv.HeaderText = "pdv";
            this.pdv.Name = "pdv";
            this.pdv.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.dgv);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(647, 615);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Uvoz aktikla i grupiranje grupa";
            // 
            // btnTraziPartnera
            // 
            this.btnTraziPartnera.BackColor = System.Drawing.Color.Transparent;
            this.btnTraziPartnera.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTraziPartnera.Image = ((System.Drawing.Image)(resources.GetObject("btnTraziPartnera.Image")));
            this.btnTraziPartnera.Location = new System.Drawing.Point(281, 50);
            this.btnTraziPartnera.Name = "btnTraziPartnera";
            this.btnTraziPartnera.Size = new System.Drawing.Size(28, 26);
            this.btnTraziPartnera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnTraziPartnera.TabIndex = 39;
            this.btnTraziPartnera.TabStop = false;
            this.btnTraziPartnera.Click += new System.EventHandler(this.btnTraziPartnera_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label2.Location = new System.Drawing.Point(10, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 26;
            this.label2.Text = "Naziv";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label1.Location = new System.Drawing.Point(10, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 15);
            this.label1.TabIndex = 26;
            this.label1.Text = "Šifra";
            // 
            // txtNazivPartnera
            // 
            this.txtNazivPartnera.Location = new System.Drawing.Point(53, 53);
            this.txtNazivPartnera.Name = "txtNazivPartnera";
            this.txtNazivPartnera.Size = new System.Drawing.Size(228, 23);
            this.txtNazivPartnera.TabIndex = 25;
            // 
            // txtSifraPartnera
            // 
            this.txtSifraPartnera.Location = new System.Drawing.Point(53, 28);
            this.txtSifraPartnera.Name = "txtSifraPartnera";
            this.txtSifraPartnera.Size = new System.Drawing.Size(90, 23);
            this.txtSifraPartnera.TabIndex = 25;
            this.txtSifraPartnera.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraPartnera_KeyDown);
            this.txtSifraPartnera.Leave += new System.EventHandler(this.txtSifraPartnera_Leave);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbSkladiste);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox2.Location = new System.Drawing.Point(679, 272);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(317, 80);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Odabir skladišta";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label3.Location = new System.Drawing.Point(8, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(271, 15);
            this.label3.TabIndex = 27;
            this.label3.Text = "Odaberite skladište na kojeg želite napraviti uvoz";
            // 
            // cbSkladiste
            // 
            this.cbSkladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkladiste.FormattingEnabled = true;
            this.cbSkladiste.Location = new System.Drawing.Point(10, 47);
            this.cbSkladiste.Name = "cbSkladiste";
            this.cbSkladiste.Size = new System.Drawing.Size(294, 24);
            this.cbSkladiste.TabIndex = 0;
            // 
            // btnKreirajXMLiSpremi
            // 
            this.btnKreirajXMLiSpremi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKreirajXMLiSpremi.BackColor = System.Drawing.Color.Transparent;
            this.btnKreirajXMLiSpremi.BackgroundImage = global::PCPOS.Properties.Resources.Untitled_1;
            this.btnKreirajXMLiSpremi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnKreirajXMLiSpremi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKreirajXMLiSpremi.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnKreirajXMLiSpremi.FlatAppearance.BorderSize = 0;
            this.btnKreirajXMLiSpremi.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnKreirajXMLiSpremi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnKreirajXMLiSpremi.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnKreirajXMLiSpremi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKreirajXMLiSpremi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold);
            this.btnKreirajXMLiSpremi.ForeColor = System.Drawing.Color.White;
            this.btnKreirajXMLiSpremi.Location = new System.Drawing.Point(795, 580);
            this.btnKreirajXMLiSpremi.Name = "btnKreirajXMLiSpremi";
            this.btnKreirajXMLiSpremi.Size = new System.Drawing.Size(197, 48);
            this.btnKreirajXMLiSpremi.TabIndex = 73;
            this.btnKreirajXMLiSpremi.Text = "Pokreni sinkronizaciju";
            this.toolTip1.SetToolTip(this.btnKreirajXMLiSpremi, "Kreiraj XML datoteku i odaberite mjesto spremanja");
            this.btnKreirajXMLiSpremi.UseVisualStyleBackColor = false;
            this.btnKreirajXMLiSpremi.Click += new System.EventHandler(this.btnKreirajXMLiSpremi_Click);
            // 
            // btnUcitajExcel
            // 
            this.btnUcitajExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUcitajExcel.BackColor = System.Drawing.Color.Transparent;
            this.btnUcitajExcel.BackgroundImage = global::PCPOS.Properties.Resources.Untitled_1;
            this.btnUcitajExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUcitajExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUcitajExcel.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnUcitajExcel.FlatAppearance.BorderSize = 0;
            this.btnUcitajExcel.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnUcitajExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUcitajExcel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUcitajExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUcitajExcel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnUcitajExcel.ForeColor = System.Drawing.Color.White;
            this.btnUcitajExcel.Location = new System.Drawing.Point(169, 56);
            this.btnUcitajExcel.Name = "btnUcitajExcel";
            this.btnUcitajExcel.Size = new System.Drawing.Size(135, 40);
            this.btnUcitajExcel.TabIndex = 74;
            this.btnUcitajExcel.Text = "Učitaj excel";
            this.toolTip1.SetToolTip(this.btnUcitajExcel, "Kreiraj XML datoteku i odaberite mjesto spremanja");
            this.btnUcitajExcel.UseVisualStyleBackColor = false;
            this.btnUcitajExcel.Click += new System.EventHandler(this.btnUcitajExcel_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.txtSifraPartnera);
            this.groupBox3.Controls.Add(this.btnTraziPartnera);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtNazivPartnera);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox3.Location = new System.Drawing.Point(679, 171);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(317, 92);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Partner za kojeg želimo uvesti podatke";
            // 
            // tab
            // 
            this.tab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tab.Controls.Add(this.txtUvozIzExcela);
            this.tab.Location = new System.Drawing.Point(675, 23);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(317, 129);
            this.tab.TabIndex = 74;
            // 
            // txtUvozIzExcela
            // 
            this.txtUvozIzExcela.Controls.Add(this.btnUcitajExcel);
            this.txtUvozIzExcela.Controls.Add(this.label4);
            this.txtUvozIzExcela.Controls.Add(this.txtPathExcel);
            this.txtUvozIzExcela.Location = new System.Drawing.Point(4, 22);
            this.txtUvozIzExcela.Name = "txtUvozIzExcela";
            this.txtUvozIzExcela.Padding = new System.Windows.Forms.Padding(3);
            this.txtUvozIzExcela.Size = new System.Drawing.Size(309, 103);
            this.txtUvozIzExcela.TabIndex = 0;
            this.txtUvozIzExcela.Text = "Uvoz iz excela";
            this.txtUvozIzExcela.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Putanja do datoteke od excela";
            // 
            // txtPathExcel
            // 
            this.txtPathExcel.Location = new System.Drawing.Point(9, 29);
            this.txtPathExcel.Name = "txtPathExcel";
            this.txtPathExcel.Size = new System.Drawing.Size(294, 20);
            this.txtPathExcel.TabIndex = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // frmSkolskaKnjiga
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
            this.ClientSize = new System.Drawing.Size(1004, 640);
            this.Controls.Add(this.tab);
            this.Controls.Add(this.btnKreirajXMLiSpremi);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmSkolskaKnjiga";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Školska knjiga";
            this.Load += new System.EventHandler(this.frmIzvozIzPrograma_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnTraziPartnera)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tab.ResumeLayout(false);
            this.txtUvozIzExcela.ResumeLayout(false);
            this.txtUvozIzExcela.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNazivPartnera;
        private System.Windows.Forms.TextBox txtSifraPartnera;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox btnTraziPartnera;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbSkladiste;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnKreirajXMLiSpremi;
        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage txtUvozIzExcela;
        private System.Windows.Forms.Button btnUcitajExcel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPathExcel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
        private System.Windows.Forms.DataGridViewComboBoxColumn grupa;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn opis;
        private System.Windows.Forms.DataGridViewTextBoxColumn grupa_;
        private System.Windows.Forms.DataGridViewTextBoxColumn Podgrupa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Brand;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jamstvo;
        private System.Windows.Forms.DataGridViewTextBoxColumn VPC;
        private System.Windows.Forms.DataGridViewTextBoxColumn MPC;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stanje;
        private System.Windows.Forms.DataGridViewTextBoxColumn Akcija;
        private System.Windows.Forms.DataGridViewTextBoxColumn LinkSlikeWeb;
        private System.Windows.Forms.DataGridViewTextBoxColumn pdv;
    }
}