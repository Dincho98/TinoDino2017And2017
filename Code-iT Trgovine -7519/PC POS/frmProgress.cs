using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmProgress : Form
    {
        public frmProgress()
        {
            InitializeComponent();
        }

        private bool status;

        private void frmUpdate_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Form1_Paint);

            bgWorker1.RunWorkerAsync();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Graphics c = e.Graphics;
            //Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            //c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void bgWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // calculate the progress out of a base "100"

            double dIndex = 10;

            double dTotal = 1000;

            double dProgressPercentage = (dIndex / dTotal);

            int iProgressPercentage = (int)(dProgressPercentage * 100);

            // update the progress bar

            bgWorker1.ReportProgress(iProgressPercentage);
        }

        private void bgWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void bgWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if (status)
            //{
            //    GC.Collect();

            //    MessageBox.Show("Program je uspješno instaliran.");
            //    Application.Exit();
            //}
            //else
            //{
            //    MessageBox.Show("FILE Not Downloaded");
            //    GC.Collect();
            //}
        }

        private static string GetApplicationPath()
        {
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        }
    }
}