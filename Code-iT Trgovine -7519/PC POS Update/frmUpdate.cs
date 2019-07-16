using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace PC_POS_Update
{
    public partial class frmUpdate : Form
    {
        public frmUpdate()
        {
            InitializeComponent();
        }
        bool status;
        private void frmUpdate_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Form1_Paint);

            bgWorker1.RunWorkerAsync();
        }

        void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        void ProvjeriVerzijeDll()
        {
            int verzijaDll = 1;
            try
            {
                ////ovo ne radi jer loada assembly pa ga ne može prekopirati
                //Assembly assembly = Assembly.LoadFrom("Raverus.FiskalizacijaDEV.dll");
                //Version ver = assembly.GetName().Version;
                //verzijaDll = ver.Major;

                FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo("Raverus.FiskalizacijaDEV.dll");

                verzijaDll = myFileVersionInfo.FileMajorPart;

                //MessageBox.Show(verzijaDll.ToString());
            }
            catch
            {
                verzijaDll = 1;
            }
            string localPath = Path.GetDirectoryName(Application.ExecutablePath);

            if (verzijaDll < 2)
            {
                //MessageBox.Show("Pokušaj skidanja datoteka potrebnih za fiskalizaciju...\n\n", "Upozorenje!");
                DLL(localPath);
            }
        }

        private void bgWorker1_DoWork(object sender, DoWorkEventArgs e)
        {


            //MessageBox.Show(verzijaDll.ToString() + " " + localPath);

            Process[] pArry = Process.GetProcesses();

            foreach (Process p in pArry)
            {
                string s = p.ProcessName;
                //s = s.ToLower();
                if (s.CompareTo("PC POS") == 0)
                {
                    p.Kill();
                }
            }


            //ProvjeriVerzijeDll();


            string sUrlToDnldFile;
            sUrlToDnldFile = "https://www.pc1.hr/pcpos/update/PC POS.exe";


            try
            {

                Uri url = new Uri(sUrlToDnldFile);
                string sFileSavePath = "";
                string sFileName = Path.GetFileName(url.LocalPath);

                sFileSavePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PCMALOPRODAJA.exe";

                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);

                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();


                response.Close();

                // gets the size of the file in bytes

                long iSize = response.ContentLength;



                // keeps track of the total bytes downloaded so we can update the progress bar

                long iRunningByteTotal = 0;

                WebClient client = new WebClient();

                Stream strRemote = client.OpenRead(url);

                FileStream strLocal = new FileStream(sFileSavePath, FileMode.Create, FileAccess.Write, FileShare.None);

                int iByteSize = 0;

                byte[] byteBuffer = new byte[1024];

                while ((iByteSize = strRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                {

                    // write the bytes to the file system at the file path specified

                    strLocal.Write(byteBuffer, 0, iByteSize);

                    iRunningByteTotal += iByteSize;


                    // calculate the progress out of a base "100"

                    double dIndex = (double)(iRunningByteTotal);

                    double dTotal = (double)iSize;

                    double dProgressPercentage = (dIndex / dTotal);

                    int iProgressPercentage = (int)(dProgressPercentage * 100);


                    // update the progress bar

                    bgWorker1.ReportProgress(iProgressPercentage);

                }

                strRemote.Close();

                status = true;

            }

            catch (Exception exM)
            {

                //Show if any error Occured

                MessageBox.Show("Error: " + exM.Message);

                status = false;

            }

        }

        void DLL(string localPath)
        {
            if (!ZamijeniDllHelper("https://www.pc1.hr/pcpos/update/", "Raverus.FiskalizacijaDEV.dll", localPath))
                return;
            if (!ZamijeniDllHelper("https://www.pc1.hr/pcpos/update/", "Raverus.FiskalizacijaDEV.XmlSerializers.dll", localPath))
                return;
        }

        private void bgWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void bgWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (status)
            {
                GC.Collect();

                MessageBox.Show("Program je uspješno instaliran.");

                if (File.Exists("PC POS.exe"))
                {
                    File.Delete("PC POS.exe");
                }

                File.Copy(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//PCMALOPRODAJA.exe", "PC POS.exe");

                string path = (File.ReadAllText(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/PC POS update.txt")).Replace("file:\\", "");
                Process.Start(path + "\\PC POS.exe");
                Application.Exit();
            }
            else
            {
                MessageBox.Show("FILE Not Downloaded");
                GC.Collect();
            }
        }

        private static string GetApplicationPath()
        {
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        }

        static bool ZamijeniDllHelper(string dllWebFolder, string dllFileName, string localPath)
        {
            string path = localPath + "\\" + dllFileName;

            bool status = false;

            try
            {
                Uri url = new Uri(dllWebFolder + dllFileName);
                string sFileSavePath = "";
                string sFileName = System.IO.Path.GetFileName(url.LocalPath);

                sFileSavePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + dllFileName;

                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);

                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();

                response.Close();

                // gets the size of the file in bytes

                long iSize = response.ContentLength;

                // keeps track of the total bytes downloaded so we can update the progress bar

                long iRunningByteTotal = 0;

                System.Net.WebClient client = new System.Net.WebClient();

                System.IO.Stream strRemote = client.OpenRead(url);

                System.IO.FileStream strLocal = new System.IO.FileStream(sFileSavePath, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None);

                int iByteSize = 0;

                byte[] byteBuffer = new byte[1024];

                while ((iByteSize = strRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                {
                    // write the bytes to the file system at the file path specified

                    strLocal.Write(byteBuffer, 0, iByteSize);

                    iRunningByteTotal += iByteSize;

                    // calculate the progress out of a base "100"

                    double dIndex = (double)(iRunningByteTotal);

                    double dTotal = (double)iSize;

                    double dProgressPercentage = (dIndex / dTotal);

                    int iProgressPercentage = (int)(dProgressPercentage * 100);
                }

                strRemote.Close();
                strLocal.Flush();
                strLocal.Close();

                //MessageBox.Show(sFileSavePath + "\n\n" + path);
                System.IO.File.Copy(sFileSavePath, path, true);

                status = true;

                //MessageBox.Show("Datoteka uspješno skinuta!", "");
            }

            catch (Exception exM)
            {
                //Show if any error Occured

                //MessageBox.Show("Pokušaj skidanja datoteke s Interneta nije uspio.\n\n" +
                //    exM.Message, "Greška!");
                status = false;
                //MessageBox.Show("greška" + "\n\n" + path + "\n\n" + exM.Message);
            }

            return status;
        }
    }
}

