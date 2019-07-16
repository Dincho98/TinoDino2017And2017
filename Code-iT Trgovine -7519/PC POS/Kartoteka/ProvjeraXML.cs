using System;
using System.Windows.Forms;

namespace PCPOS.Kartoteka
{
    internal class ProvjeraXML
    {
        static public bool Provjeri(string path)
        {
            if (System.IO.File.Exists(path))
            {
                return true;
            }

            if (!Util.CheckConnection.Check())
            {
                MessageBox.Show("Ne postoji datoteka za postavljanje kartoteke. Pokušaj skidanja\n" +
                    " datoteke s Interneta nije uspio jer niste spojeni na Internet.", "Upozorenje!");
                return false;
            }

            MessageBox.Show("Ne postoji datoteka za postavljanje kartoteke. Slijedi pokušaj skidanja datoteke s Interneta.", "");

            string sUrlToDnldFile;
            sUrlToDnldFile = "https://www.pc1.hr/pcpos/update/postavke.xml";

            bool status = false;

            try
            {
                Uri url = new Uri(sUrlToDnldFile);
                string sFileSavePath = "";
                string sFileName = System.IO.Path.GetFileName(url.LocalPath);

                sFileSavePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\postavke.xml";

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

            return status;
        }
    }
}