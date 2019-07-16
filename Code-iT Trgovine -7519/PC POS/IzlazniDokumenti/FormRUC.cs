using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCPOS.IzlazniDokumenti
{
    public partial class FormRUC : Form
    {
        public FormRUC()
        {
            InitializeComponent();
        }

        private void btnIspisi_Click(object sender, EventArgs e)
        {
            Report.RUC.ReportRUC form = new Report.RUC.ReportRUC();
            form.datumOd = dtpOd.Value;
            form.datumDo = dtpDo.Value;
            form.ShowDialog();
        }
    }
}
