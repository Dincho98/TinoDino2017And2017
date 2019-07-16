namespace PCPOS {
    partial class frmPregledPoGodinama {
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPartnerIme = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSifraArtikla = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNazivArtikli = new System.Windows.Forms.TextBox();
            this.btnTrazi = new System.Windows.Forms.Button();
            this.btnProsleGosine = new System.Windows.Forms.Button();
            this.dokumenat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kupac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.broj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.artikl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rabat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cijena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.opis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dokumenat,
            this.kupac,
            this.datum,
            this.broj,
            this.artikl,
            this.kolicina,
            this.rabat,
            this.cijena,
            this.opis});
            this.dataGridView1.Location = new System.Drawing.Point(13, 115);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(1059, 348);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(13, 482);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1059, 68);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(15, 464);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Opis:";
            // 
            // txtPartnerIme
            // 
            this.txtPartnerIme.Location = new System.Drawing.Point(207, 9);
            this.txtPartnerIme.Name = "txtPartnerIme";
            this.txtPartnerIme.Size = new System.Drawing.Size(291, 20);
            this.txtPartnerIme.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(13, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Pretraži prema partneru";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(13, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Pretraži prema šifri artikla";
            // 
            // txtSifraArtikla
            // 
            this.txtSifraArtikla.Location = new System.Drawing.Point(207, 31);
            this.txtSifraArtikla.Name = "txtSifraArtikla";
            this.txtSifraArtikla.Size = new System.Drawing.Size(291, 20);
            this.txtSifraArtikla.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(13, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(188, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Pretraži prema nazivu artikla";
            // 
            // txtNazivArtikli
            // 
            this.txtNazivArtikli.Location = new System.Drawing.Point(207, 53);
            this.txtNazivArtikli.Name = "txtNazivArtikli";
            this.txtNazivArtikli.Size = new System.Drawing.Size(291, 20);
            this.txtNazivArtikli.TabIndex = 7;
            // 
            // btnTrazi
            // 
            this.btnTrazi.Location = new System.Drawing.Point(504, 8);
            this.btnTrazi.Name = "btnTrazi";
            this.btnTrazi.Size = new System.Drawing.Size(89, 66);
            this.btnTrazi.TabIndex = 9;
            this.btnTrazi.Text = "Traži";
            this.btnTrazi.UseVisualStyleBackColor = true;
            this.btnTrazi.Click += new System.EventHandler(this.btnTrazi_Click);
            // 
            // btnProsleGosine
            // 
            this.btnProsleGosine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProsleGosine.Location = new System.Drawing.Point(914, 7);
            this.btnProsleGosine.Name = "btnProsleGosine";
            this.btnProsleGosine.Size = new System.Drawing.Size(158, 67);
            this.btnProsleGosine.TabIndex = 10;
            this.btnProsleGosine.Text = "Učitaj podatke iz prošlih godina (ovo učitajte samo jednom na početku godine)";
            this.btnProsleGosine.UseVisualStyleBackColor = true;
            this.btnProsleGosine.Click += new System.EventHandler(this.btnProsleGosine_Click);
            // 
            // dokumenat
            // 
            this.dokumenat.HeaderText = "Dokumenat";
            this.dokumenat.Name = "dokumenat";
            this.dokumenat.ReadOnly = true;
            // 
            // kupac
            // 
            this.kupac.HeaderText = "Kupac";
            this.kupac.Name = "kupac";
            this.kupac.ReadOnly = true;
            // 
            // datum
            // 
            this.datum.HeaderText = "Datum";
            this.datum.Name = "datum";
            this.datum.ReadOnly = true;
            // 
            // broj
            // 
            this.broj.HeaderText = "Broj";
            this.broj.Name = "broj";
            this.broj.ReadOnly = true;
            // 
            // artikl
            // 
            this.artikl.HeaderText = "Artikl";
            this.artikl.Name = "artikl";
            this.artikl.ReadOnly = true;
            // 
            // kolicina
            // 
            this.kolicina.HeaderText = "Količina";
            this.kolicina.Name = "kolicina";
            this.kolicina.ReadOnly = true;
            // 
            // rabat
            // 
            this.rabat.HeaderText = "Rabat";
            this.rabat.Name = "rabat";
            this.rabat.ReadOnly = true;
            // 
            // cijena
            // 
            this.cijena.HeaderText = "Cijena";
            this.cijena.Name = "cijena";
            this.cijena.ReadOnly = true;
            // 
            // opis
            // 
            this.opis.HeaderText = "Opis";
            this.opis.Name = "opis";
            this.opis.ReadOnly = true;
            // 
            // frmPregledPoGodinama
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(109)))), ((int)(((byte)(135)))));
            this.ClientSize = new System.Drawing.Size(1084, 559);
            this.Controls.Add(this.btnProsleGosine);
            this.Controls.Add(this.btnTrazi);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNazivArtikli);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSifraArtikla);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPartnerIme);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmPregledPoGodinama";
            this.Text = "Pregled po godinama";
            this.Load += new System.EventHandler(this.frmPregledPoGodinama_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPartnerIme;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSifraArtikla;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNazivArtikli;
        private System.Windows.Forms.Button btnTrazi;
        private System.Windows.Forms.Button btnProsleGosine;
        private System.Windows.Forms.DataGridViewTextBoxColumn dokumenat;
        private System.Windows.Forms.DataGridViewTextBoxColumn kupac;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj;
        private System.Windows.Forms.DataGridViewTextBoxColumn artikl;
        private System.Windows.Forms.DataGridViewTextBoxColumn kolicina;
        private System.Windows.Forms.DataGridViewTextBoxColumn rabat;
        private System.Windows.Forms.DataGridViewTextBoxColumn cijena;
        private System.Windows.Forms.DataGridViewTextBoxColumn opis;
    }
}