namespace PCPOS.Resort
{
    partial class FrmSveUgostiteljskeOtpremnice
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnIspisOtpremnice = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSifraSobe = new System.Windows.Forms.Button();
            this.btnPretrazi = new System.Windows.Forms.Button();
            this.cbZaposlenik = new System.Windows.Forms.ComboBox();
            this.dtpDoDatum = new System.Windows.Forms.DateTimePicker();
            this.dtpOdDatum = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbSifraSobe = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnTraziPartner = new System.Windows.Forms.Button();
            this.tbSifraPartner = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbBrojOtpremnice = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnIzlaz = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.broj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.broj_otpremnice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soba = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iznos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnIspisOtpremnice
            // 
            this.btnIspisOtpremnice.BackColor = System.Drawing.Color.White;
            this.btnIspisOtpremnice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIspisOtpremnice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIspisOtpremnice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnIspisOtpremnice.Image = global::PCPOS.Properties.Resources.print_printer;
            this.btnIspisOtpremnice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIspisOtpremnice.Location = new System.Drawing.Point(12, 12);
            this.btnIspisOtpremnice.Name = "btnIspisOtpremnice";
            this.btnIspisOtpremnice.Size = new System.Drawing.Size(171, 46);
            this.btnIspisOtpremnice.TabIndex = 0;
            this.btnIspisOtpremnice.Text = "       Ispis otpremnice";
            this.btnIspisOtpremnice.UseVisualStyleBackColor = false;
            this.btnIspisOtpremnice.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnSifraSobe);
            this.panel1.Controls.Add(this.btnPretrazi);
            this.panel1.Controls.Add(this.cbZaposlenik);
            this.panel1.Controls.Add(this.dtpDoDatum);
            this.panel1.Controls.Add(this.dtpOdDatum);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.tbSifraSobe);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnTraziPartner);
            this.panel1.Controls.Add(this.tbSifraPartner);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tbBrojOtpremnice);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(12, 89);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(980, 68);
            this.panel1.TabIndex = 1;
            // 
            // btnSifraSobe
            // 
            this.btnSifraSobe.BackgroundImage = global::PCPOS.Properties.Resources._10591;
            this.btnSifraSobe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSifraSobe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSifraSobe.FlatAppearance.BorderSize = 0;
            this.btnSifraSobe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSifraSobe.Location = new System.Drawing.Point(858, 7);
            this.btnSifraSobe.Name = "btnSifraSobe";
            this.btnSifraSobe.Size = new System.Drawing.Size(34, 21);
            this.btnSifraSobe.TabIndex = 19;
            this.btnSifraSobe.UseVisualStyleBackColor = true;
            // 
            // btnPretrazi
            // 
            this.btnPretrazi.BackgroundImage = global::PCPOS.Properties.Resources._1059;
            this.btnPretrazi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPretrazi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPretrazi.FlatAppearance.BorderSize = 0;
            this.btnPretrazi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPretrazi.Location = new System.Drawing.Point(908, 7);
            this.btnPretrazi.Name = "btnPretrazi";
            this.btnPretrazi.Size = new System.Drawing.Size(59, 47);
            this.btnPretrazi.TabIndex = 18;
            this.btnPretrazi.UseVisualStyleBackColor = true;
            this.btnPretrazi.Click += new System.EventHandler(this.BtnPretrazi_Click);
            // 
            // cbZaposlenik
            // 
            this.cbZaposlenik.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbZaposlenik.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbZaposlenik.FormattingEnabled = true;
            this.cbZaposlenik.Location = new System.Drawing.Point(700, 31);
            this.cbZaposlenik.Name = "cbZaposlenik";
            this.cbZaposlenik.Size = new System.Drawing.Size(192, 23);
            this.cbZaposlenik.TabIndex = 17;
            // 
            // dtpDoDatum
            // 
            this.dtpDoDatum.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDoDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDoDatum.Location = new System.Drawing.Point(406, 32);
            this.dtpDoDatum.Name = "dtpDoDatum";
            this.dtpDoDatum.Size = new System.Drawing.Size(190, 21);
            this.dtpDoDatum.TabIndex = 13;
            // 
            // dtpOdDatum
            // 
            this.dtpOdDatum.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpOdDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpOdDatum.Location = new System.Drawing.Point(406, 7);
            this.dtpOdDatum.Name = "dtpOdDatum";
            this.dtpOdDatum.Size = new System.Drawing.Size(190, 21);
            this.dtpOdDatum.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(624, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 15);
            this.label7.TabIndex = 16;
            this.label7.Text = "Zaposlenik:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(329, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 15);
            this.label6.TabIndex = 11;
            this.label6.Text = "Do datuma:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(329, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "Od datuma:";
            // 
            // tbSifraSobe
            // 
            this.tbSifraSobe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbSifraSobe.Location = new System.Drawing.Point(700, 7);
            this.tbSifraSobe.Name = "tbSifraSobe";
            this.tbSifraSobe.Size = new System.Drawing.Size(152, 21);
            this.tbSifraSobe.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(629, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Šifra sobe:";
            // 
            // btnTraziPartner
            // 
            this.btnTraziPartner.BackgroundImage = global::PCPOS.Properties.Resources._10591;
            this.btnTraziPartner.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTraziPartner.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTraziPartner.FlatAppearance.BorderSize = 0;
            this.btnTraziPartner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTraziPartner.Location = new System.Drawing.Point(269, 32);
            this.btnTraziPartner.Name = "btnTraziPartner";
            this.btnTraziPartner.Size = new System.Drawing.Size(34, 21);
            this.btnTraziPartner.TabIndex = 7;
            this.btnTraziPartner.UseVisualStyleBackColor = true;
            this.btnTraziPartner.Click += new System.EventHandler(this.BtnTraziPartner_Click);
            // 
            // tbSifraPartner
            // 
            this.tbSifraPartner.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbSifraPartner.Location = new System.Drawing.Point(111, 32);
            this.tbSifraPartner.Name = "tbSifraPartner";
            this.tbSifraPartner.Size = new System.Drawing.Size(152, 21);
            this.tbSifraPartner.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(21, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Šifra partnera:";
            // 
            // tbBrojOtpremnice
            // 
            this.tbBrojOtpremnice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbBrojOtpremnice.Location = new System.Drawing.Point(111, 7);
            this.tbBrojOtpremnice.Name = "tbBrojOtpremnice";
            this.tbBrojOtpremnice.Size = new System.Drawing.Size(192, 21);
            this.tbBrojOtpremnice.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(8, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Broj otpremnice:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 66);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3);
            this.label1.Size = new System.Drawing.Size(171, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Pretraživanje otpremnice";
            // 
            // btnIzlaz
            // 
            this.btnIzlaz.BackColor = System.Drawing.Color.White;
            this.btnIzlaz.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIzlaz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIzlaz.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnIzlaz.Image = global::PCPOS.Properties.Resources.Actions_application_exit_icon;
            this.btnIzlaz.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIzlaz.Location = new System.Drawing.Point(877, 12);
            this.btnIzlaz.Name = "btnIzlaz";
            this.btnIzlaz.Size = new System.Drawing.Size(115, 46);
            this.btnIzlaz.TabIndex = 3;
            this.btnIzlaz.Text = "       Izlaz";
            this.btnIzlaz.UseVisualStyleBackColor = false;
            this.btnIzlaz.Click += new System.EventHandler(this.btnIzlaz_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.broj,
            this.broj_otpremnice,
            this.datum,
            this.soba,
            this.iznos,
            this.status});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView.Location = new System.Drawing.Point(12, 163);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(980, 506);
            this.dataGridView.TabIndex = 4;
            // 
            // broj
            // 
            this.broj.FillWeight = 25F;
            this.broj.HeaderText = "Br.";
            this.broj.Name = "broj";
            this.broj.ReadOnly = true;
            // 
            // broj_otpremnice
            // 
            this.broj_otpremnice.FillWeight = 80F;
            this.broj_otpremnice.HeaderText = "Otpremnica";
            this.broj_otpremnice.Name = "broj_otpremnice";
            this.broj_otpremnice.ReadOnly = true;
            // 
            // datum
            // 
            this.datum.HeaderText = "Datum";
            this.datum.Name = "datum";
            this.datum.ReadOnly = true;
            // 
            // soba
            // 
            this.soba.HeaderText = "Soba";
            this.soba.Name = "soba";
            this.soba.ReadOnly = true;
            // 
            // iznos
            // 
            this.iznos.HeaderText = "Iznos";
            this.iznos.Name = "iznos";
            this.iznos.ReadOnly = true;
            // 
            // status
            // 
            this.status.HeaderText = "Status";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // FrmSveUgostiteljskeOtpremnice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1004, 681);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.btnIzlaz);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnIspisOtpremnice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmSveUgostiteljskeOtpremnice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sve ugostiteljske otpremnice";
            this.Load += new System.EventHandler(this.FrmSveUgostiteljskeOtpremnice_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIspisOtpremnice;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDoDatum;
        private System.Windows.Forms.DateTimePicker dtpOdDatum;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbSifraSobe;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnTraziPartner;
        private System.Windows.Forms.TextBox tbSifraPartner;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbBrojOtpremnice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPretrazi;
        private System.Windows.Forms.ComboBox cbZaposlenik;
        private System.Windows.Forms.Button btnIzlaz;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button btnSifraSobe;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj;
        private System.Windows.Forms.DataGridViewTextBoxColumn broj_otpremnice;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum;
        private System.Windows.Forms.DataGridViewTextBoxColumn soba;
        private System.Windows.Forms.DataGridViewTextBoxColumn iznos;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
    }
}