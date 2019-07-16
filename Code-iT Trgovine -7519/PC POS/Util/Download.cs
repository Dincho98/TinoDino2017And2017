using System;
using System.IO;
using System.Windows.Forms;

namespace PCPOS.Util
{
    internal class Download
    {
        static public void SkidajFont()
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\slike\\msgothic.ttc";

            if (System.IO.File.Exists(path))
            {
                return;
            }

            if (!Util.CheckConnection.Check())
            {
                MessageBox.Show("Ne postoji datoteka za postavljanje fonta kod ispisa računa. Pokušaj skidanja\n" +
                    " datoteke s Interneta nije uspio jer niste spojeni na Internet. Provjerite svoju Internet konekciju, " +
                    "jer u suprotnom nećete moći ispisivati račune.", "Upozorenje!");
                return;
            }

            MessageBox.Show("Slijedi pokušaj downloadanja datoteke za ispis računa.", "");

            string sUrlToDnldFile;
            sUrlToDnldFile = "https://www.pc1.hr/pcpos/update/msgothic.zip";

            bool status = false;

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

                    //bgWorker1.ReportProgress(iProgressPercentage);
                }

                strRemote.Close();
                strLocal.Flush();
                strLocal.Close();

                System.IO.File.Copy(sFileSavePath, path);

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

            return;
        }

        static public void SkidajPodrsku()
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\help1.exe";

            if (System.IO.File.Exists(path))
            {
                return;
            }

            if (!Util.CheckConnection.Check())
            {
                MessageBox.Show("Pokušaj skidanja datoteke s Interneta nije uspio jer niste spojeni na Internet. " +
                    "Provjerite svoju Internet konekciju.",
                    "Upozorenje!");
                return;
            }

            MessageBox.Show("Slijedi skidanje programa kojim ćete se moći spojiti na Code-iT...");

            string sUrlToDnldFile;
            sUrlToDnldFile = "https://www.pc1.hr/pcpos/update/help1.exe";

            bool status = false;

            try
            {
                Uri url = new Uri(sUrlToDnldFile);
                string sFileSavePath = "";
                string sFileName = System.IO.Path.GetFileName(url.LocalPath);

                sFileSavePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\help.exe";

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

                    //bgWorker1.ReportProgress(iProgressPercentage);
                }

                strRemote.Close();
                strLocal.Flush();
                strLocal.Close();

                System.IO.File.Copy(sFileSavePath, path);

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

            return;
        }
    }
}