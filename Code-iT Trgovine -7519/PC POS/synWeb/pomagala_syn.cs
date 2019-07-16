using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;


//as

namespace PCPOS.synWeb
{
    internal class pomagala_syn
    {
        public string MyWebRequest(string arrPOST, string url)
        {
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.Timeout = 1200000;

            byte[] byteArray = Encoding.GetEncoding("windows-1250").GetBytes(arrPOST + KreirajSigurnuPoruku());

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();

            dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            dataStream.Close();
            response.Close();

            return ((HttpWebResponse)response).StatusDescription + ";" + responseFromServer;
        }

        public DataTable MyWebRequestXML(string arrPOST, string url)
        {
            try
            {
                try
                {
                    if (arrPOST.Length > 0)
                    {
                        string[] arrGodina = arrPOST.Split('&');
                        if (arrGodina.Length <= 1)
                            return new DataTable();

                        string godina = arrGodina[arrGodina.Length - 1];

                        if (godina == "godina=0")
                            return new DataTable();
                    }
                }
                catch (Exception)
                {
                    return new DataTable();
                }

                DataSet ds = new DataSet();

                Encoding encoding = Encoding.GetEncoding("windows-1250");
                arrPOST += KreirajSigurnuPoruku();
                byte[] data = encoding.GetBytes(arrPOST);

                WebRequest reguest = WebRequest.Create(url);
                reguest.Timeout = 90000;
                reguest.Method = "POST";
                reguest.ContentType = "application/x-www-form-urlencoded";
                reguest.ContentLength = data.Length;
                Stream stream = reguest.GetRequestStream();
                stream.Write(data, 0, data.Length);

                WebResponse response = reguest.GetResponse();
                stream = response.GetResponseStream();

                StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("windows-1250"));

                ds.ReadXml(sr);
                sr.Close();
                response.Close();
                stream.Close();

                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
                else
                    return new DataTable();
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }

        //ova metoda sluzi za kreiranje sigurnosne poruke koja se uspoređuje na web-u
        private string KreirajSigurnuPoruku()
        {
            decimal key = 123654789;
            decimal nekiBroj = 5463287658246;
            decimal br = 0;
            decimal izracun = 0;
            string dat = DateTime.Now.ToString("yyyyMMddHmmss");
            decimal.TryParse(dat, out br);
            izracun = br + nekiBroj;
            izracun = izracun / key;
            return "&izracun=" + Math.Round(izracun, 5) + "&datum=" + dat;
        }
    }
}