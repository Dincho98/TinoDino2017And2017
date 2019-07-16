using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Util
{
    public partial class frmUpdate : Form
    {
        public frmUpdate()
        {
            InitializeComponent();
        }

        private bool status = false;
        public string path;

        private void frmUpdate_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Form1_Paint);
            bgWorker1.RunWorkerAsync();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void bgWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (System.IO.File.Exists(path))
            {
                return;
            }

            string sUrlToDnldFile;
            sUrlToDnldFile = "https://www.pc1.hr/pcpos/update/msgothic.zip";

            try
            {
                Uri url = new Uri(sUrlToDnldFile);
                string sFileSavePath = "";
                string sFileName = System.IO.Path.GetFileName(url.LocalPath);

                sFileSavePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\msgothic.ttc";

                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);

                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();

                response.Close();

                // gets the size of the file in bytes

                long iSize = response.ContentLength;

                // keeps track of the total bytes downloaded so we can update the progress bar

                long iRunningByteTotal = 0;

                System.Net.WebClient client = new System.Net.WebClient();

                System.IO.Stream strRemote = client.OpenRead(url);

                System.IO.FileStream strLocal = new System.IO.FileStream(sFileSavePath,
                    System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None);

                int iByteSize = 0;

                byte[] byteBuffer = new byte[1024];

                while ((iByteSize = strRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                {
                    // write the bytes to the file system at the file path specified

                    strLocal.Write(byteBuffer, 0, iByteSize);

                    iRunningByteTotal += iByteSize;

                    // calculate the progress out of a base "100"

                    double dIndex = iRunningByteTotal;

                    double dTotal = iSize;

                    double dProgressPercentage = (dIndex / dTotal);

                    int iProgressPercentage = (int)(dProgressPercentage * 100);

                    // update the progress bar

                    bgWorker1.ReportProgress(iProgressPercentage);
                }

                strRemote.Close();
                strLocal.Flush();
                strLocal.Close();

                System.IO.File.Copy(sFileSavePath, path, true);

                MessageBox.Show("Datoteka uspješno skinuta!", "");
                status = true;
            }
            catch (Exception exM)
            {
                //Show if any error Occured

                MessageBox.Show("Pokušaj skidanja datoteke s Interneta nije uspio.\n\n" +
                    exM.Message, "Upozorenje!");
                status = false;
            }
        }

        private void bgWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void bgWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        private static string GetApplicationPath()
        {
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        }
    }
}