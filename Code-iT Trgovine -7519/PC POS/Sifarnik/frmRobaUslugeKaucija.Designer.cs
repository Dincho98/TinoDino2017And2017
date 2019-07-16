namespace PCPOS
{
	partial class frmRobaUslugeKaucija
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRobaUslugeKaucija));
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.dgw = new System.Windows.Forms.DataGridView();
			this.id_stavka = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.br = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.naziv = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.sifra_kaucija = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.kolicina = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnObrisi = new System.Windows.Forms.Button();
			this.btnOpenRoba = new System.Windows.Forms.PictureBox();
			this.txtSifra_robe = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnSpremi = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnOpenRoba)).BeginInit();
			this.SuspendLayout();
			// 
			// dgw
			// 
			this.dgw.AllowUserToAddRows = false;
			this.dgw.AllowUserToDeleteRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
			this.dgw.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgw.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgw.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_stavka,
            this.br,
            this.naziv,
            this.sifra_kaucija,
            this.kolicina});
			this.dgw.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dgw.GridColor = System.Drawing.Color.Gainsboro;
			this.dgw.Location = new System.Drawing.Point(12, 59);
			this.dgw.MultiSelect = false;
			this.dgw.Name = "dgw";
			this.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgw.Size = new System.Drawing.Size(641, 198);
			this.dgw.TabIndex = 84;
			this.dgw.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellClick);
			this.dgw.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellEndEdit);
			// 
			// id_stavka
			// 
			this.id_stavka.HeaderText = "id_stavka";
			this.id_stavka.Name = "id_stavka";
			this.id_stavka.Visible = false;
			// 
			// br
			// 
			this.br.FillWeight = 27.73358F;
			this.br.HeaderText = "Br.";
			this.br.Name = "br";
			this.br.ReadOnly = true;
			// 
			// naziv
			// 
			this.naziv.FillWeight = 197.2801F;
			this.naziv.HeaderText = "Naziv";
			this.naziv.Name = "naziv";
			this.naziv.ReadOnly = true;
			// 
			// sifra_kaucija
			// 
			this.sifra_kaucija.FillWeight = 134.3771F;
			this.sifra_kaucija.HeaderText = "Šifra";
			this.sifra_kaucija.Name = "sifra_kaucija";
			this.sifra_kaucija.ReadOnly = true;
			// 
			// kolicina
			// 
			this.kolicina.FillWeight = 40.60914F;
			this.kolicina.HeaderText = "Količina";
			this.kolicina.Name = "kolicina";
			// 
			// btnObrisi
			// 
			this.btnObrisi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnObrisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.btnObrisi.Image = global::PCPOS.Properties.Resources.Close;
			this.btnObrisi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnObrisi.Location = new System.Drawing.Point(518, 22);
			this.btnObrisi.Name = "btnObrisi";
			this.btnObrisi.Size = new System.Drawing.Size(135, 30);
			this.btnObrisi.TabIndex = 85;
			this.btnObrisi.Text = "   Obriši stavku";
			this.btnObrisi.UseVisualStyleBackColor = true;
			this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
			// 
			// btnOpenRoba
			// 
			this.btnOpenRoba.BackColor = System.Drawing.Color.Transparent;
			this.btnOpenRoba.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnOpenRoba.Image = global::PCPOS.Properties.Resources._1059;
			this.btnOpenRoba.Location = new System.Drawing.Point(252, 22);
			this.btnOpenRoba.Name = "btnOpenRoba";
			this.btnOpenRoba.Size = new System.Drawing.Size(39, 31);
			this.btnOpenRoba.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.btnOpenRoba.TabIndex = 88;
			this.btnOpenRoba.TabStop = false;
			this.btnOpenRoba.Click += new System.EventHandler(this.btnOpenRoba_Click);
			// 
			// txtSifra_robe
			// 
			this.txtSifra_robe.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.txtSifra_robe.Location = new System.Drawing.Point(12, 30);
			this.txtSifra_robe.Name = "txtSifra_robe";
			this.txtSifra_robe.Size = new System.Drawing.Size(238, 23);
			this.txtSifra_robe.TabIndex = 87;
			this.txtSifra_robe.Enter += new System.EventHandler(this.TRENUTNI_Enter);
			this.txtSifra_robe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifra_robe_KeyDown);
			this.txtSifra_robe.Leave += new System.EventHandler(this.NAPUSTENI_Leave);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label2.Location = new System.Drawing.Point(9, 11);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 17);
			this.label2.TabIndex = 86;
			this.label2.Text = "Šifra:";
			// 
			// btnSpremi
			// 
			this.btnSpremi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.btnSpremi.Image = ((System.Drawing.Image)(resources.GetObject("btnSpremi.Image")));
			this.btnSpremi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSpremi.Location = new System.Drawing.Point(523, 263);
			this.btnSpremi.Name = "btnSpremi";
			this.btnSpremi.Size = new System.Drawing.Size(130, 40);
			this.btnSpremi.TabIndex = 89;
			this.btnSpremi.Text = "Spremi   ";
			this.btnSpremi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSpremi.UseVisualStyleBackColor = true;
			this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
			// 
			// frmRobaUslugeKaucija
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.SlateGray;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.ClientSize = new System.Drawing.Size(666, 322);
			this.Controls.Add(this.btnSpremi);
			this.Controls.Add(this.btnOpenRoba);
			this.Controls.Add(this.txtSifra_robe);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnObrisi);
			this.Controls.Add(this.dgw);
			this.MaximumSize = new System.Drawing.Size(682, 360);
			this.MinimumSize = new System.Drawing.Size(682, 360);
			this.Name = "frmRobaUslugeKaucija";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Kaucija za artikl";
			this.Load += new System.EventHandler(this.frmRobaUsluge_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnOpenRoba)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.DataGridView dgw;
		private System.Windows.Forms.Button btnObrisi;
		private System.Windows.Forms.PictureBox btnOpenRoba;
		private System.Windows.Forms.TextBox txtSifra_robe;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnSpremi;
		private System.Windows.Forms.DataGridViewTextBoxColumn id_stavka;
		private System.Windows.Forms.DataGridViewTextBoxColumn br;
		private System.Windows.Forms.DataGridViewTextBoxColumn naziv;
		private System.Windows.Forms.DataGridViewTextBoxColumn sifra_kaucija;
		private System.Windows.Forms.DataGridViewTextBoxColumn kolicina;
    }
}