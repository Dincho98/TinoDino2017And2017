namespace PCPOS.Report.SaldaKonti
{
    partial class frmUnosPocetnogDugovanjaPartnera
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnSrchPartners = new System.Windows.Forms.Button();
            this.btnTrazi = new System.Windows.Forms.Button();
            this.txtNaziv = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.pnlFill = new System.Windows.Forms.Panel();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.btnSpremi = new System.Windows.Forms.Button();
            this.txtPotrazuje = new System.Windows.Forms.TextBox();
            this.txtUplaceno = new System.Windows.Forms.TextBox();
            this.txtIsplaceno = new System.Windows.Forms.TextBox();
            this.txtOtvoreno = new System.Windows.Forms.TextBox();
            this.lblIsplaceno = new System.Windows.Forms.Label();
            this.lblPotrazuje = new System.Windows.Forms.Label();
            this.lblUplaceno = new System.Windows.Forms.Label();
            this.lblOtvoreno = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.pnlFill.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.LightSlateGray;
            this.pnlTop.Controls.Add(this.btnSrchPartners);
            this.pnlTop.Controls.Add(this.btnTrazi);
            this.pnlTop.Controls.Add(this.txtNaziv);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Controls.Add(this.txtSifra);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(917, 80);
            this.pnlTop.TabIndex = 0;
            // 
            // btnSrchPartners
            // 
            this.btnSrchPartners.BackgroundImage = global::PCPOS.Properties.Resources._1059;
            this.btnSrchPartners.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSrchPartners.FlatAppearance.BorderSize = 0;
            this.btnSrchPartners.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSrchPartners.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSrchPartners.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSrchPartners.Location = new System.Drawing.Point(185, 24);
            this.btnSrchPartners.Name = "btnSrchPartners";
            this.btnSrchPartners.Size = new System.Drawing.Size(31, 28);
            this.btnSrchPartners.TabIndex = 2;
            this.btnSrchPartners.UseVisualStyleBackColor = true;
            this.btnSrchPartners.Click += new System.EventHandler(this.btnSrchPartners_Click);
            // 
            // btnTrazi
            // 
            this.btnTrazi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnTrazi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnTrazi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTrazi.Location = new System.Drawing.Point(809, 11);
            this.btnTrazi.Name = "btnTrazi";
            this.btnTrazi.Size = new System.Drawing.Size(96, 54);
            this.btnTrazi.TabIndex = 4;
            this.btnTrazi.Text = "Izlaz";
            this.btnTrazi.UseVisualStyleBackColor = true;
            this.btnTrazi.Click += new System.EventHandler(this.btnTrazi_Click);
            // 
            // txtNaziv
            // 
            this.txtNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtNaziv.Location = new System.Drawing.Point(218, 27);
            this.txtNaziv.Name = "txtNaziv";
            this.txtNaziv.ReadOnly = true;
            this.txtNaziv.Size = new System.Drawing.Size(585, 23);
            this.txtNaziv.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(13, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Šifra partnera:";
            // 
            // txtSifra
            // 
            this.txtSifra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtSifra.Location = new System.Drawing.Point(118, 27);
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(66, 23);
            this.txtSifra.TabIndex = 1;
            this.txtSifra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSifra_KeyDown);
            // 
            // pnlFill
            // 
            this.pnlFill.Controls.Add(this.btnOdustani);
            this.pnlFill.Controls.Add(this.btnSpremi);
            this.pnlFill.Controls.Add(this.txtPotrazuje);
            this.pnlFill.Controls.Add(this.txtUplaceno);
            this.pnlFill.Controls.Add(this.txtIsplaceno);
            this.pnlFill.Controls.Add(this.txtOtvoreno);
            this.pnlFill.Controls.Add(this.lblIsplaceno);
            this.pnlFill.Controls.Add(this.lblPotrazuje);
            this.pnlFill.Controls.Add(this.lblUplaceno);
            this.pnlFill.Controls.Add(this.lblOtvoreno);
            this.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFill.Location = new System.Drawing.Point(0, 80);
            this.pnlFill.Name = "pnlFill";
            this.pnlFill.Size = new System.Drawing.Size(917, 485);
            this.pnlFill.TabIndex = 1;
            // 
            // btnOdustani
            // 
            this.btnOdustani.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnOdustani.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnOdustani.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOdustani.Location = new System.Drawing.Point(561, 170);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(96, 54);
            this.btnOdustani.TabIndex = 10;
            this.btnOdustani.Text = "Odustani";
            this.btnOdustani.UseVisualStyleBackColor = true;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            // 
            // btnSpremi
            // 
            this.btnSpremi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSpremi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSpremi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSpremi.Location = new System.Drawing.Point(459, 170);
            this.btnSpremi.Name = "btnSpremi";
            this.btnSpremi.Size = new System.Drawing.Size(96, 54);
            this.btnSpremi.TabIndex = 9;
            this.btnSpremi.Text = "Spremi";
            this.btnSpremi.UseVisualStyleBackColor = true;
            this.btnSpremi.Click += new System.EventHandler(this.btnSpremi_Click);
            // 
            // txtPotrazuje
            // 
            this.txtPotrazuje.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtPotrazuje.Location = new System.Drawing.Point(185, 143);
            this.txtPotrazuje.Name = "txtPotrazuje";
            this.txtPotrazuje.Size = new System.Drawing.Size(181, 23);
            this.txtPotrazuje.TabIndex = 8;
            // 
            // txtUplaceno
            // 
            this.txtUplaceno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtUplaceno.Location = new System.Drawing.Point(185, 85);
            this.txtUplaceno.Name = "txtUplaceno";
            this.txtUplaceno.Size = new System.Drawing.Size(181, 23);
            this.txtUplaceno.TabIndex = 7;
            // 
            // txtIsplaceno
            // 
            this.txtIsplaceno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtIsplaceno.Location = new System.Drawing.Point(185, 201);
            this.txtIsplaceno.Name = "txtIsplaceno";
            this.txtIsplaceno.Size = new System.Drawing.Size(181, 23);
            this.txtIsplaceno.TabIndex = 6;
            // 
            // txtOtvoreno
            // 
            this.txtOtvoreno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtOtvoreno.Location = new System.Drawing.Point(185, 27);
            this.txtOtvoreno.Name = "txtOtvoreno";
            this.txtOtvoreno.Size = new System.Drawing.Size(181, 23);
            this.txtOtvoreno.TabIndex = 5;
            // 
            // lblIsplaceno
            // 
            this.lblIsplaceno.AutoSize = true;
            this.lblIsplaceno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblIsplaceno.Location = new System.Drawing.Point(107, 204);
            this.lblIsplaceno.Name = "lblIsplaceno";
            this.lblIsplaceno.Size = new System.Drawing.Size(72, 17);
            this.lblIsplaceno.TabIndex = 4;
            this.lblIsplaceno.Text = "Isplačeno:";
            // 
            // lblPotrazuje
            // 
            this.lblPotrazuje.AutoSize = true;
            this.lblPotrazuje.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPotrazuje.Location = new System.Drawing.Point(107, 146);
            this.lblPotrazuje.Name = "lblPotrazuje";
            this.lblPotrazuje.Size = new System.Drawing.Size(72, 17);
            this.lblPotrazuje.TabIndex = 3;
            this.lblPotrazuje.Text = "Potražuje:";
            // 
            // lblUplaceno
            // 
            this.lblUplaceno.AutoSize = true;
            this.lblUplaceno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblUplaceno.Location = new System.Drawing.Point(107, 88);
            this.lblUplaceno.Name = "lblUplaceno";
            this.lblUplaceno.Size = new System.Drawing.Size(72, 17);
            this.lblUplaceno.TabIndex = 2;
            this.lblUplaceno.Text = "Uplačeno:";
            // 
            // lblOtvoreno
            // 
            this.lblOtvoreno.AutoSize = true;
            this.lblOtvoreno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblOtvoreno.Location = new System.Drawing.Point(108, 30);
            this.lblOtvoreno.Name = "lblOtvoreno";
            this.lblOtvoreno.Size = new System.Drawing.Size(71, 17);
            this.lblOtvoreno.TabIndex = 1;
            this.lblOtvoreno.Text = "Otvoreno:";
            // 
            // frmUnosPocetnogDugovanjaPartnera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(917, 565);
            this.Controls.Add(this.pnlFill);
            this.Controls.Add(this.pnlTop);
            this.Name = "frmUnosPocetnogDugovanjaPartnera";
            this.Text = "frmUnosPocetnogDugovanjaPartnera";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlFill.ResumeLayout(false);
            this.pnlFill.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnTrazi;
        private System.Windows.Forms.TextBox txtNaziv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.Panel pnlFill;
        private System.Windows.Forms.Button btnSrchPartners;
        private System.Windows.Forms.Label lblOtvoreno;
        private System.Windows.Forms.Label lblUplaceno;
        private System.Windows.Forms.Label lblPotrazuje;
        private System.Windows.Forms.Label lblIsplaceno;
        private System.Windows.Forms.TextBox txtPotrazuje;
        private System.Windows.Forms.TextBox txtUplaceno;
        private System.Windows.Forms.TextBox txtIsplaceno;
        private System.Windows.Forms.TextBox txtOtvoreno;
        private System.Windows.Forms.Button btnSpremi;
        private System.Windows.Forms.Button btnOdustani;
    }
}