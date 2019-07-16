namespace PCPOS.Sinkronizacija.Partneri
{
    partial class frmRobot
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnKreirajXMLiSpremi = new System.Windows.Forms.Button();
            this.btnUcitaj = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.cbSkladiste = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.dgv.Size = new System.Drawing.Size(618, 516);
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
            this.groupBox1.Size = new System.Drawing.Size(630, 544);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Uvoz aktikla i grupiranje grupa";
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
            this.btnKreirajXMLiSpremi.Location = new System.Drawing.Point(499, 582);
            this.btnKreirajXMLiSpremi.Name = "btnKreirajXMLiSpremi";
            this.btnKreirajXMLiSpremi.Size = new System.Drawing.Size(144, 48);
            this.btnKreirajXMLiSpremi.TabIndex = 73;
            this.btnKreirajXMLiSpremi.Text = "Pokreni sinkronizaciju";
            this.toolTip1.SetToolTip(this.btnKreirajXMLiSpremi, "Kreiraj XML datoteku i odaberite mjesto spremanja");
            this.btnKreirajXMLiSpremi.UseVisualStyleBackColor = false;
            this.btnKreirajXMLiSpremi.Click += new System.EventHandler(this.btnKreirajXMLiSpremi_Click);
            // 
            // btnUcitaj
            // 
            this.btnUcitaj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUcitaj.BackColor = System.Drawing.Color.Transparent;
            this.btnUcitaj.BackgroundImage = global::PCPOS.Properties.Resources.Untitled_1;
            this.btnUcitaj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUcitaj.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUcitaj.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnUcitaj.FlatAppearance.BorderSize = 0;
            this.btnUcitaj.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnUcitaj.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUcitaj.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUcitaj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUcitaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold);
            this.btnUcitaj.ForeColor = System.Drawing.Color.White;
            this.btnUcitaj.Location = new System.Drawing.Point(345, 582);
            this.btnUcitaj.Name = "btnUcitaj";
            this.btnUcitaj.Size = new System.Drawing.Size(148, 48);
            this.btnUcitaj.TabIndex = 74;
            this.btnUcitaj.Text = "Učitaj podatke";
            this.toolTip1.SetToolTip(this.btnUcitaj, "Kreiraj XML datoteku i odaberite mjesto spremanja");
            this.btnUcitaj.UseVisualStyleBackColor = false;
            this.btnUcitaj.Click += new System.EventHandler(this.btnUcitaj_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label3.Location = new System.Drawing.Point(11, 586);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(271, 15);
            this.label3.TabIndex = 76;
            this.label3.Text = "Odaberite skladište na kojeg želite napraviti uvoz";
            // 
            // cbSkladiste
            // 
            this.cbSkladiste.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbSkladiste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkladiste.FormattingEnabled = true;
            this.cbSkladiste.Location = new System.Drawing.Point(13, 604);
            this.cbSkladiste.Name = "cbSkladiste";
            this.cbSkladiste.Size = new System.Drawing.Size(294, 21);
            this.cbSkladiste.TabIndex = 75;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label1.Location = new System.Drawing.Point(10, 560);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 15);
            this.label1.TabIndex = 77;
            this.label1.Text = "Na cijenu robot ide + 20%";
            // 
            // frmRobot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
            this.ClientSize = new System.Drawing.Size(655, 641);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbSkladiste);
            this.Controls.Add(this.btnUcitaj);
            this.Controls.Add(this.btnKreirajXMLiSpremi);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmRobot";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Load += new System.EventHandler(this.frmIzvozIzPrograma_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnKreirajXMLiSpremi;
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
        private System.Windows.Forms.Button btnUcitaj;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbSkladiste;
        private System.Windows.Forms.Label label1;
    }
}