namespace PCPOS.Odrzavanja
{
    partial class frmOdrzavanja
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOdrzavanja));
            this.dgv = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDodaj = new System.Windows.Forms.Button();
            this.btnTraziPartnera = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNazivPartnera = new System.Windows.Forms.TextBox();
            this.txtSifraPartnera = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnGeneriraj = new System.Windows.Forms.Button();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblOdrMpc = new System.Windows.Forms.Label();
            this.lblUkupnoInternetMpc = new System.Windows.Forms.Label();
            this.lblUkOd = new System.Windows.Forms.Label();
            this.lblUkInt = new System.Windows.Forms.Label();
            this.sifra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cijena_interneta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.internet_kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cijena_odrzavanje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolicina_odrzavanje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.web_ured = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tablet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pcpos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pccaffe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pdv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnTraziPartnera)).BeginInit();
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
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sifra,
            this.partner,
            this.cijena_interneta,
            this.internet_kolicina,
            this.cijena_odrzavanje,
            this.kolicina_odrzavanje,
            this.web_ured,
            this.tablet,
            this.pcpos,
            this.pccaffe,
            this.resort,
            this.pdv});
            this.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv.Location = new System.Drawing.Point(13, 134);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 18;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(980, 344);
            this.dgv.TabIndex = 23;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btnDodaj);
            this.groupBox1.Controls.Add(this.btnTraziPartnera);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtNazivPartnera);
            this.groupBox1.Controls.Add(this.txtSifraPartnera);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(980, 104);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dodavanje novog partnera za održavanje";
            // 
            // btnDodaj
            // 
            this.btnDodaj.BackColor = System.Drawing.Color.Transparent;
            this.btnDodaj.BackgroundImage = global::PCPOS.Properties.Resources.Untitled_1;
            this.btnDodaj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDodaj.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDodaj.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnDodaj.FlatAppearance.BorderSize = 0;
            this.btnDodaj.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnDodaj.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDodaj.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDodaj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDodaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold);
            this.btnDodaj.ForeColor = System.Drawing.Color.White;
            this.btnDodaj.Location = new System.Drawing.Point(365, 56);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(59, 34);
            this.btnDodaj.TabIndex = 71;
            this.btnDodaj.Text = "Dodaj";
            this.toolTip1.SetToolTip(this.btnDodaj, "Dodaj ovog partnera na popis");
            this.btnDodaj.UseVisualStyleBackColor = false;
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);
            // 
            // btnTraziPartnera
            // 
            this.btnTraziPartnera.BackColor = System.Drawing.Color.Transparent;
            this.btnTraziPartnera.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTraziPartnera.Image = ((System.Drawing.Image)(resources.GetObject("btnTraziPartnera.Image")));
            this.btnTraziPartnera.Location = new System.Drawing.Point(332, 59);
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
            this.label2.Location = new System.Drawing.Point(97, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 26;
            this.label2.Text = "Naziv";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.label1.Location = new System.Drawing.Point(7, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 15);
            this.label1.TabIndex = 26;
            this.label1.Text = "Šifra";
            // 
            // txtNazivPartnera
            // 
            this.txtNazivPartnera.Location = new System.Drawing.Point(100, 60);
            this.txtNazivPartnera.Name = "txtNazivPartnera";
            this.txtNazivPartnera.Size = new System.Drawing.Size(228, 23);
            this.txtNazivPartnera.TabIndex = 25;
            // 
            // txtSifraPartnera
            // 
            this.txtSifraPartnera.Location = new System.Drawing.Point(7, 60);
            this.txtSifraPartnera.Name = "txtSifraPartnera";
            this.txtSifraPartnera.Size = new System.Drawing.Size(90, 23);
            this.txtSifraPartnera.TabIndex = 25;
            this.txtSifraPartnera.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifraPartnera_KeyDown);
            // 
            // btnGeneriraj
            // 
            this.btnGeneriraj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGeneriraj.BackColor = System.Drawing.Color.Transparent;
            this.btnGeneriraj.BackgroundImage = global::PCPOS.Properties.Resources.Untitled_1;
            this.btnGeneriraj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGeneriraj.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGeneriraj.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnGeneriraj.FlatAppearance.BorderSize = 0;
            this.btnGeneriraj.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnGeneriraj.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnGeneriraj.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnGeneriraj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGeneriraj.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold);
            this.btnGeneriraj.ForeColor = System.Drawing.Color.White;
            this.btnGeneriraj.Location = new System.Drawing.Point(13, 496);
            this.btnGeneriraj.Name = "btnGeneriraj";
            this.btnGeneriraj.Size = new System.Drawing.Size(127, 48);
            this.btnGeneriraj.TabIndex = 72;
            this.btnGeneriraj.Text = "Generiraj";
            this.toolTip1.SetToolTip(this.btnGeneriraj, "Kreiraj samo XML datoteku");
            this.btnGeneriraj.UseVisualStyleBackColor = false;
            this.btnGeneriraj.Click += new System.EventHandler(this.btnGeneriraj_Click);
            // 
            // btnObrisi
            // 
            this.btnObrisi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnObrisi.BackColor = System.Drawing.Color.Transparent;
            this.btnObrisi.BackgroundImage = global::PCPOS.Properties.Resources.Untitled_1;
            this.btnObrisi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnObrisi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnObrisi.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnObrisi.FlatAppearance.BorderSize = 0;
            this.btnObrisi.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSlateGray;
            this.btnObrisi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnObrisi.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnObrisi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnObrisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold);
            this.btnObrisi.ForeColor = System.Drawing.Color.Red;
            this.btnObrisi.Location = new System.Drawing.Point(146, 496);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(141, 48);
            this.btnObrisi.TabIndex = 78;
            this.btnObrisi.Text = "Obriši označenog";
            this.toolTip1.SetToolTip(this.btnObrisi, "Kreiraj samo XML datoteku");
            this.btnObrisi.UseVisualStyleBackColor = false;
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(747, 485);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 17);
            this.label3.TabIndex = 73;
            this.label3.Text = "Ukupno interneta kol:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(747, 501);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(157, 17);
            this.label4.TabIndex = 73;
            this.label4.Text = "Ukupno održavanja kol:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.Location = new System.Drawing.Point(747, 517);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 17);
            this.label5.TabIndex = 73;
            this.label5.Text = "Ukupno interneta:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label6.Location = new System.Drawing.Point(747, 533);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 17);
            this.label6.TabIndex = 73;
            this.label6.Text = "Ukupno održavanja:";
            // 
            // lblOdrMpc
            // 
            this.lblOdrMpc.BackColor = System.Drawing.Color.Transparent;
            this.lblOdrMpc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblOdrMpc.Location = new System.Drawing.Point(910, 533);
            this.lblOdrMpc.Name = "lblOdrMpc";
            this.lblOdrMpc.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblOdrMpc.Size = new System.Drawing.Size(86, 17);
            this.lblOdrMpc.TabIndex = 76;
            this.lblOdrMpc.Text = "0,00";
            // 
            // lblUkupnoInternetMpc
            // 
            this.lblUkupnoInternetMpc.BackColor = System.Drawing.Color.Transparent;
            this.lblUkupnoInternetMpc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblUkupnoInternetMpc.Location = new System.Drawing.Point(910, 517);
            this.lblUkupnoInternetMpc.Name = "lblUkupnoInternetMpc";
            this.lblUkupnoInternetMpc.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblUkupnoInternetMpc.Size = new System.Drawing.Size(86, 17);
            this.lblUkupnoInternetMpc.TabIndex = 77;
            this.lblUkupnoInternetMpc.Text = "0,00";
            // 
            // lblUkOd
            // 
            this.lblUkOd.BackColor = System.Drawing.Color.Transparent;
            this.lblUkOd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblUkOd.Location = new System.Drawing.Point(910, 501);
            this.lblUkOd.Name = "lblUkOd";
            this.lblUkOd.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblUkOd.Size = new System.Drawing.Size(86, 17);
            this.lblUkOd.TabIndex = 74;
            this.lblUkOd.Text = "0,00";
            // 
            // lblUkInt
            // 
            this.lblUkInt.BackColor = System.Drawing.Color.Transparent;
            this.lblUkInt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblUkInt.Location = new System.Drawing.Point(910, 485);
            this.lblUkInt.Name = "lblUkInt";
            this.lblUkInt.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblUkInt.Size = new System.Drawing.Size(86, 17);
            this.lblUkInt.TabIndex = 75;
            this.lblUkInt.Text = "0,00";
            // 
            // sifra
            // 
            this.sifra.HeaderText = "Šifra";
            this.sifra.Name = "sifra";
            this.sifra.ReadOnly = true;
            // 
            // partner
            // 
            this.partner.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.partner.HeaderText = "Partner";
            this.partner.Name = "partner";
            this.partner.ReadOnly = true;
            // 
            // cijena_interneta
            // 
            this.cijena_interneta.HeaderText = "Cijena kasa";
            this.cijena_interneta.Name = "cijena_interneta";
            // 
            // internet_kolicina
            // 
            this.internet_kolicina.HeaderText = "Količina kasa";
            this.internet_kolicina.Name = "internet_kolicina";
            // 
            // cijena_odrzavanje
            // 
            this.cijena_odrzavanje.HeaderText = "Cijena održavanje";
            this.cijena_odrzavanje.Name = "cijena_odrzavanje";
            // 
            // kolicina_odrzavanje
            // 
            this.kolicina_odrzavanje.HeaderText = "Količina održavanje";
            this.kolicina_odrzavanje.Name = "kolicina_odrzavanje";
            // 
            // web_ured
            // 
            this.web_ured.FillWeight = 40F;
            this.web_ured.HeaderText = "Web";
            this.web_ured.Name = "web_ured";
            this.web_ured.Width = 40;
            // 
            // tablet
            // 
            this.tablet.FillWeight = 50F;
            this.tablet.HeaderText = "Tablet";
            this.tablet.Name = "tablet";
            this.tablet.Width = 50;
            // 
            // pcpos
            // 
            this.pcpos.FillWeight = 50F;
            this.pcpos.HeaderText = "PCPOS";
            this.pcpos.Name = "pcpos";
            this.pcpos.Width = 50;
            // 
            // pccaffe
            // 
            this.pccaffe.FillWeight = 60F;
            this.pccaffe.HeaderText = "PCCAFFE";
            this.pccaffe.Name = "pccaffe";
            this.pccaffe.Width = 60;
            // 
            // resort
            // 
            this.resort.FillWeight = 60F;
            this.resort.HeaderText = "RESORT";
            this.resort.Name = "resort";
            this.resort.Width = 60;
            // 
            // pdv
            // 
            this.pdv.HeaderText = "pdv";
            this.pdv.Name = "pdv";
            this.pdv.Visible = false;
            // 
            // frmOdrzavanja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
            this.ClientSize = new System.Drawing.Size(1005, 556);
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.lblOdrMpc);
            this.Controls.Add(this.lblUkupnoInternetMpc);
            this.Controls.Add(this.lblUkOd);
            this.Controls.Add(this.lblUkInt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnGeneriraj);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgv);
            this.Name = "frmOdrzavanja";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Održavanja";
            this.Load += new System.EventHandler(this.frmIzvozIzPrograma_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnTraziPartnera)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNazivPartnera;
        private System.Windows.Forms.TextBox txtSifraPartnera;
        private System.Windows.Forms.PictureBox btnTraziPartnera;
        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnGeneriraj;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblOdrMpc;
        private System.Windows.Forms.Label lblUkupnoInternetMpc;
        private System.Windows.Forms.Label lblUkOd;
        private System.Windows.Forms.Label lblUkInt;
        private System.Windows.Forms.Button btnObrisi;
        private System.Windows.Forms.DataGridViewTextBoxColumn sifra;
        private System.Windows.Forms.DataGridViewTextBoxColumn partner;
        private System.Windows.Forms.DataGridViewTextBoxColumn cijena_interneta;
        private System.Windows.Forms.DataGridViewTextBoxColumn internet_kolicina;
        private System.Windows.Forms.DataGridViewTextBoxColumn cijena_odrzavanje;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolicina_odrzavanje;
        private System.Windows.Forms.DataGridViewTextBoxColumn web_ured;
        private System.Windows.Forms.DataGridViewTextBoxColumn tablet;
        private System.Windows.Forms.DataGridViewTextBoxColumn pcpos;
        private System.Windows.Forms.DataGridViewTextBoxColumn pccaffe;
        private System.Windows.Forms.DataGridViewTextBoxColumn resort;
        private System.Windows.Forms.DataGridViewTextBoxColumn pdv;
    }
}