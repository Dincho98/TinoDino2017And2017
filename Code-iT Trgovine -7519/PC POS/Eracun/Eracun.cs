using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PCPOS.Eracun.Entities;

namespace PCPOS.Eracun
{
    public static class Eracun
    {
        private static string API_URL = @"https://localhost:44363/";

        public static HttpWebResponse SendRequest<T>(T entity, string endPointURL)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string jsonString = serializer.Serialize(entity);

            var httpRequest = (HttpWebRequest)WebRequest.Create(API_URL + endPointURL);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";
            httpRequest.ContentLength = jsonString.Length;

            using(var stream = httpRequest.GetRequestStream())
            {
                stream.Write(Encoding.ASCII.GetBytes(jsonString), 0, jsonString.Length);
            }

            return (HttpWebResponse)httpRequest.GetResponse();
        }
    }
}
