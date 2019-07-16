using System;
using System.Windows.Forms;

namespace PCPOS.Util
{
    public partial class frmHtmlInfo : Form
    {
        public frmHtmlInfo()
        {
            InitializeComponent();
        }

        private void frmHtmlInfo_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate(new Uri("https://www.pc1.hr/podrska/pcpos/index.html"));
            webBrowser1.Refresh();
        }
    }
}