using System;
using System.Windows.Forms;

namespace PCPOS.Util
{
    public partial class frmInputPassword : Form
    {
        public frmInputPassword()
        {
            InitializeComponent();
        }

        private string _password = "";
        public string password { get { return _password; } }

        private void btnOk_Click(object sender, EventArgs e)
        {
            _password = txtPassword.Text;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}