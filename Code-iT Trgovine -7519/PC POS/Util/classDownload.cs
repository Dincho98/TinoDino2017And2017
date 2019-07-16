using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace PCPOS.Util
{
    internal class classDownload
    {
        public static bool DownloadFile(string web_adresa, string adresa_komp)
        {
            try
            {
                Uri url = new Uri(web_adresa);
                string sFileSavePath = adresa_komp;
                string sFileName = Path.GetFileName(url.LocalPath);

                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                response.Close();

                long iSize = response.ContentLength;

                long iRunningByteTotal = 0;

                WebClient client = new WebClient();

                Stream strRemote = client.OpenRead(url);

                FileStream strLocal = new FileStream(sFileSavePath, FileMode.Create, FileAccess.Write, FileShare.None);

                int iByteSize = 0;

                byte[] byteBuffer = new byte[1024];

                while ((iByteSize = strRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                {
                    strLocal.Write(byteBuffer, 0, iByteSize);

                    iRunningByteTotal += iByteSize;

                    double dIndex = iRunningByteTotal;

                    double dTotal = iSize;

                    double dProgressPercentage = (dIndex / dTotal);

                    int iProgressPercentage = (int)(dProgressPercentage * 100);
                }

                strRemote.Close();
                return true;
            }
            catch (Exception exM)
            {
                MessageBox.Show("Error: " + exM.Message);
                return false;
            }
        }
    }
}