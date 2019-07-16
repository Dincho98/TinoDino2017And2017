namespace PCPOS.Resort
{
    partial class FrmPopisGostijuUredi
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
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.comboBoxVrstaUsluge = new System.Windows.Forms.ComboBox();
            this.textBoxPrezimeImeBroj = new System.Windows.Forms.TextBox();
            this.buttonUredi = new System.Windows.Forms.Button();
            this.dateTimePickerDatumKraja = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerDatumPocetka = new System.Windows.Forms.DateTimePicker();
            this.textBoxBrojPutovnice = new System.Windows.Forms.TextBox();
            this.textBoxBrojOsobne = new System.Windows.Forms.TextBox();
            this.textBoxPrezimeImeNaziv = new System.Windows.Forms.TextBox();
            this.textBoxRedniBroj = new System.Windows.Forms.TextBox();
            this.richTextBoxNapomena = new System.Windows.Forms.RichTextBox();
            this.labelDatum = new System.Windows.Forms.Label();
            this.labelDatumKrajaPruzanjaUsluge = new System.Windows.Forms.Label();
            this.labelDatumPocetkaPruzanjaUsluge = new System.Windows.Forms.Label();
            this.labelVrstaUsluge = new System.Windows.Forms.Label();
            this.labelBrojPutovnice = new System.Windows.Forms.Label();
            this.labelBrojOsobne = new System.Windows.Forms.Label();
            this.labelPrezimeIme = new System.Windows.Forms.Label();
            this.labelRedniBroj = new System.Windows.Forms.Label();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.comboBoxVrstaUsluge);
            this.groupBox.Controls.Add(this.textBoxPrezimeImeBroj);
            this.groupBox.Controls.Add(this.buttonUredi);
            this.groupBox.Controls.Add(this.dateTimePickerDatumKraja);
            this.groupBox.Controls.Add(this.dateTimePickerDatumPocetka);
            this.groupBox.Controls.Add(this.textBoxBrojPutovnice);
            this.groupBox.Controls.Add(this.textBoxBrojOsobne);
            this.groupBox.Controls.Add(this.textBoxPrezimeImeNaziv);
            this.groupBox.Controls.Add(this.textBoxRedniBroj);
            this.groupBox.Controls.Add(this.richTextBoxNapomena);
            this.groupBox.Controls.Add(this.labelDatum);
            this.groupBox.Controls.Add(this.labelDatumKrajaPruzanjaUsluge);
            this.groupBox.Controls.Add(this.labelDatumPocetkaPruzanjaUsluge);
            this.groupBox.Controls.Add(this.labelVrstaUsluge);
            this.groupBox.Controls.Add(this.labelBrojPutovnice);
            this.groupBox.Controls.Add(this.labelBrojOsobne);
            this.groupBox.Controls.Add(this.labelPrezimeIme);
            this.groupBox.Controls.Add(this.labelRedniBroj);
            this.groupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox.Location = new System.Drawing.Point(12, 12);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(775, 253);
            this.groupBox.TabIndex = 1;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Uređivanje gosta";
            // 
            // comboBoxVrstaUsluge
            // 
            this.comboBoxVrstaUsluge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVrstaUsluge.FormattingEnabled = true;
            this.comboBoxVrstaUsluge.Location = new System.Drawing.Point(144, 207);
            this.comboBoxVrstaUsluge.Name = "comboBoxVrstaUsluge";
            this.comboBoxVrstaUsluge.Size = new System.Drawing.Size(172, 24);
            this.comboBoxVrstaUsluge.TabIndex = 18;
            // 
            // textBoxPrezimeImeBroj
            // 
            this.textBoxPrezimeImeBroj.Location = new System.Drawing.Point(144, 74);
            this.textBoxPrezimeImeBroj.Name = "textBoxPrezimeImeBroj";
            this.textBoxPrezimeImeBroj.Size = new System.Drawing.Size(46, 22);
            this.textBoxPrezimeImeBroj.TabIndex = 17;
            this.textBoxPrezimeImeBroj.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxPrezimeImeBroj_KeyDown);
            // 
            // buttonUredi
            // 
            this.buttonUredi.Location = new System.Drawing.Point(680, 161);
            this.buttonUredi.Name = "buttonUredi";
            this.buttonUredi.Size = new System.Drawing.Size(75, 44);
            this.buttonUredi.TabIndex = 2;
            this.buttonUredi.Text = "Uredi";
            this.buttonUredi.UseVisualStyleBackColor = true;
            this.buttonUredi.Click += new System.EventHandler(this.buttonUredi_Click);
            // 
            // dateTimePickerDatumKraja
            // 
            this.dateTimePickerDatumKraja.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDatumKraja.Location = new System.Drawing.Point(605, 72);
            this.dateTimePickerDatumKraja.Name = "dateTimePickerDatumKraja";
            this.dateTimePickerDatumKraja.Size = new System.Drawing.Size(164, 22);
            this.dateTimePickerDatumKraja.TabIndex = 16;
            // 
            // dateTimePickerDatumPocetka
            // 
            this.dateTimePickerDatumPocetka.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDatumPocetka.Location = new System.Drawing.Point(605, 30);
            this.dateTimePickerDatumPocetka.Name = "dateTimePickerDatumPocetka";
            this.dateTimePickerDatumPocetka.Size = new System.Drawing.Size(164, 22);
            this.dateTimePickerDatumPocetka.TabIndex = 15;
            // 
            // textBoxBrojPutovnice
            // 
            this.textBoxBrojPutovnice.Location = new System.Drawing.Point(144, 161);
            this.textBoxBrojPutovnice.Name = "textBoxBrojPutovnice";
            this.textBoxBrojPutovnice.Size = new System.Drawing.Size(172, 22);
            this.textBoxBrojPutovnice.TabIndex = 13;
            // 
            // textBoxBrojOsobne
            // 
            this.textBoxBrojOsobne.Location = new System.Drawing.Point(144, 117);
            this.textBoxBrojOsobne.Name = "textBoxBrojOsobne";
            this.textBoxBrojOsobne.Size = new System.Drawing.Size(172, 22);
            this.textBoxBrojOsobne.TabIndex = 12;
            // 
            // textBoxPrezimeImeNaziv
            // 
            this.textBoxPrezimeImeNaziv.Location = new System.Drawing.Point(196, 74);
            this.textBoxPrezimeImeNaziv.Name = "textBoxPrezimeImeNaziv";
            this.textBoxPrezimeImeNaziv.ReadOnly = true;
            this.textBoxPrezimeImeNaziv.Size = new System.Drawing.Size(120, 22);
            this.textBoxPrezimeImeNaziv.TabIndex = 11;
            // 
            // textBoxRedniBroj
            // 
            this.textBoxRedniBroj.Location = new System.Drawing.Point(121, 29);
            this.textBoxRedniBroj.Name = "textBoxRedniBroj";
            this.textBoxRedniBroj.ReadOnly = true;
            this.textBoxRedniBroj.Size = new System.Drawing.Size(57, 22);
            this.textBoxRedniBroj.TabIndex = 10;
            // 
            // richTextBoxNapomena
            // 
            this.richTextBoxNapomena.Location = new System.Drawing.Point(372, 141);
            this.richTextBoxNapomena.Name = "richTextBoxNapomena";
            this.richTextBoxNapomena.Size = new System.Drawing.Size(281, 85);
            this.richTextBoxNapomena.TabIndex = 9;
            this.richTextBoxNapomena.Text = "";
            // 
            // labelDatum
            // 
            this.labelDatum.AutoSize = true;
            this.labelDatum.Location = new System.Drawing.Point(369, 120);
            this.labelDatum.Name = "labelDatum";
            this.labelDatum.Size = new System.Drawing.Size(88, 16);
            this.labelDatum.TabIndex = 8;
            this.labelDatum.Text = "Napomena:";
            // 
            // labelDatumKrajaPruzanjaUsluge
            // 
            this.labelDatumKrajaPruzanjaUsluge.AutoSize = true;
            this.labelDatumKrajaPruzanjaUsluge.Location = new System.Drawing.Point(369, 77);
            this.labelDatumKrajaPruzanjaUsluge.Name = "labelDatumKrajaPruzanjaUsluge";
            this.labelDatumKrajaPruzanjaUsluge.Size = new System.Drawing.Size(208, 16);
            this.labelDatumKrajaPruzanjaUsluge.TabIndex = 7;
            this.labelDatumKrajaPruzanjaUsluge.Text = "Datum kraja pružanja usluge:";
            // 
            // labelDatumPocetkaPruzanjaUsluge
            // 
            this.labelDatumPocetkaPruzanjaUsluge.AutoSize = true;
            this.labelDatumPocetkaPruzanjaUsluge.Location = new System.Drawing.Point(369, 35);
            this.labelDatumPocetkaPruzanjaUsluge.Name = "labelDatumPocetkaPruzanjaUsluge";
            this.labelDatumPocetkaPruzanjaUsluge.Size = new System.Drawing.Size(229, 16);
            this.labelDatumPocetkaPruzanjaUsluge.TabIndex = 6;
            this.labelDatumPocetkaPruzanjaUsluge.Text = "Datum početka pružanja usluge:";
            // 
            // labelVrstaUsluge
            // 
            this.labelVrstaUsluge.AutoSize = true;
            this.labelVrstaUsluge.Location = new System.Drawing.Point(20, 210);
            this.labelVrstaUsluge.Name = "labelVrstaUsluge";
            this.labelVrstaUsluge.Size = new System.Drawing.Size(98, 16);
            this.labelVrstaUsluge.TabIndex = 5;
            this.labelVrstaUsluge.Text = "Vrsta usluge:";
            // 
            // labelBrojPutovnice
            // 
            this.labelBrojPutovnice.AutoSize = true;
            this.labelBrojPutovnice.Location = new System.Drawing.Point(20, 164);
            this.labelBrojPutovnice.Name = "labelBrojPutovnice";
            this.labelBrojPutovnice.Size = new System.Drawing.Size(111, 16);
            this.labelBrojPutovnice.TabIndex = 4;
            this.labelBrojPutovnice.Text = "Broj putovnice:";
            // 
            // labelBrojOsobne
            // 
            this.labelBrojOsobne.AutoSize = true;
            this.labelBrojOsobne.Location = new System.Drawing.Point(20, 120);
            this.labelBrojOsobne.Name = "labelBrojOsobne";
            this.labelBrojOsobne.Size = new System.Drawing.Size(96, 16);
            this.labelBrojOsobne.TabIndex = 3;
            this.labelBrojOsobne.Text = "Broj osobne:";
            // 
            // labelPrezimeIme
            // 
            this.labelPrezimeIme.AutoSize = true;
            this.labelPrezimeIme.Location = new System.Drawing.Point(20, 77);
            this.labelPrezimeIme.Name = "labelPrezimeIme";
            this.labelPrezimeIme.Size = new System.Drawing.Size(105, 16);
            this.labelPrezimeIme.TabIndex = 2;
            this.labelPrezimeIme.Text = "Prezime i ime:";
            // 
            // labelRedniBroj
            // 
            this.labelRedniBroj.AutoSize = true;
            this.labelRedniBroj.Location = new System.Drawing.Point(20, 35);
            this.labelRedniBroj.Name = "labelRedniBroj";
            this.labelRedniBroj.Size = new System.Drawing.Size(84, 16);
            this.labelRedniBroj.TabIndex = 1;
            this.labelRedniBroj.Text = "Redni broj:";
            // 
            // FrmPopisGostijuUredi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(809, 276);
            this.Controls.Add(this.groupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmPopisGostijuUredi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Uredi popis gostiju";
            this.Load += new System.EventHandler(this.FrmPopisGostijuUredi_Load);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.ComboBox comboBoxVrstaUsluge;
        private System.Windows.Forms.TextBox textBoxPrezimeImeBroj;
        private System.Windows.Forms.Button buttonUredi;
        private System.Windows.Forms.DateTimePicker dateTimePickerDatumKraja;
        private System.Windows.Forms.DateTimePicker dateTimePickerDatumPocetka;
        private System.Windows.Forms.TextBox textBoxBrojPutovnice;
        private System.Windows.Forms.TextBox textBoxBrojOsobne;
        private System.Windows.Forms.TextBox textBoxPrezimeImeNaziv;
        private System.Windows.Forms.TextBox textBoxRedniBroj;
        private System.Windows.Forms.RichTextBox richTextBoxNapomena;
        private System.Windows.Forms.Label labelDatum;
        private System.Windows.Forms.Label labelDatumKrajaPruzanjaUsluge;
        private System.Windows.Forms.Label labelDatumPocetkaPruzanjaUsluge;
        private System.Windows.Forms.Label labelVrstaUsluge;
        private System.Windows.Forms.Label labelBrojPutovnice;
        private System.Windows.Forms.Label labelBrojOsobne;
        private System.Windows.Forms.Label labelPrezimeIme;
        private System.Windows.Forms.Label labelRedniBroj;
    }
}