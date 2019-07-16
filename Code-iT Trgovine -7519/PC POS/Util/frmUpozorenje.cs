using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PCPOS.Util
{
    public partial class frmUpozorenje : Form
    {
        public frmUpozorenje()
        {
            InitializeComponent();
        }

        private void frmUpozorenje_Load(object sender, EventArgs e)
        {
            richTextBox1.Select(0, 16);
            System.Drawing.Font currentFont = richTextBox1.SelectionFont;
            System.Drawing.FontStyle newFontStyle = FontStyle.Bold;
            richTextBox1.SelectionFont = new Font(currentFont.FontFamily, 20, newFontStyle);
            richTextBox1.SelectionColor = Color.Red;
            richTextBox1.SelectionLength = 0;
        }

        private void frmUpozorenje_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllText("NadogradnjaPoslovnice", "");
        }
    }
}