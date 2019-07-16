namespace PCPOS
{
	partial class frmPovijestPromjena
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
			this.rtbPromjene = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// rtbPromjene
			// 
			this.rtbPromjene.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.rtbPromjene.Location = new System.Drawing.Point(13, 13);
			this.rtbPromjene.Name = "rtbPromjene";
			this.rtbPromjene.ReadOnly = true;
			this.rtbPromjene.Size = new System.Drawing.Size(638, 367);
			this.rtbPromjene.TabIndex = 0;
			this.rtbPromjene.Text = "";
			// 
			// frmPovijestPromjena
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(663, 392);
			this.Controls.Add(this.rtbPromjene);
			this.Name = "frmPovijestPromjena";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Povijest promjena";
			this.Load += new System.EventHandler(this.PovijestPromjena_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox rtbPromjene;
	}
}