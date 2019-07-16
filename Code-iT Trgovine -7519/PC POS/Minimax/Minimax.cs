using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PCPOS.Minimax.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PCPOS.Minimax
{
    class Minimax
    {
        private static string API_URL = @"https://moj.minimax.hr/hr/api/";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetApiAccessToken()
        {
            string userName = "dejanvibovic";
            string password = "Code123";
            string clientId = "CodeIT_BID";
            string clientSecret = "N62ZvFHf4Y9UsnkA";
            string tokenEndpoint = "https://moj.minimax.hr/HR/AUT/OAuth20/Token";

            string accessToken = "";
            string scope = "minimax.si";

            WebRequest webRequest = WebRequest.Create(tokenEndpoint);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";

            string request = string.Format("grant_type=password&client_id={0}&client_secret={1}&scope={2}&username={3}&password={4}", clientId, clientSecret, scope, userName, password);
            byte[] bytes = Encoding.ASCII.GetBytes(request);
            webRequest.ContentLength = bytes.Length;

            using (Stream outputStream = webRequest.GetRequestStream())
            {
                outputStream.Write(bytes, 0, bytes.Length);
            }

            using (WebResponse webResponse = webRequest.GetResponse())
            {
                using (Stream stream = webResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    Dictionary<string, object> dict = (Dictionary<string, object>)serializer.DeserializeObject(reader.ReadToEnd());
                    accessToken = dict["access_token"].ToString();
                }
            }

            return accessToken;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiMethodUrl"></param>
        /// <param name="accessToken"></param>
        /// <param name="statusCode"></param>
        /// <param name="resultContentStr"></param>
        /// <returns></returns>
        public static bool GetApiResultContent(string apiMethodUrl, string accessToken, out HttpStatusCode statusCode, out string resultContentStr)
        {
            bool success = false;
            resultContentStr = null;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(API_URL + apiMethodUrl);
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            request.Accept = "application/json";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            statusCode = response.StatusCode;

            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                resultContentStr = sr.ReadToEnd();
                success = true;
            }

            return success;
        }

        /// <summary>
        /// Main method used to send POST request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static HttpStatusCode SendRequest<T>(string url, string accessToken, T entity)
        {
            HttpStatusCode result = HttpStatusCode.Created;

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var jsonString = serializer.Serialize(entity);

            var request = (HttpWebRequest)WebRequest.Create(API_URL + url);
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = jsonString.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(Encoding.ASCII.GetBytes(jsonString), 0, jsonString.Length);
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response != null)
                    result = response.StatusCode;
            }

            return result;
        }

        /// <summary>
        /// Method used to change IssuedInvoice status to "issue"
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="invoiceId"></param>
        /// <param name="rowVersion"></param>
        public static void CustomActionIssuedInvoice(int orgId, long invoiceId, string rowVersion)
        {
            var request = (HttpWebRequest)WebRequest.Create(API_URL + $@"api/orgs/{orgId}/issuedinvoices/{invoiceId}/actions/issue?rowVersion={rowVersion}");
            request.Headers.Add("Authorization", "Bearer " + GetApiAccessToken());
            request.Method = "PUT";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.ContentLength = 0;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                HttpStatusCode result = response.StatusCode;
            }
        }

        /// <summary>
        /// Returns current user's organisation
        /// </summary>
        /// <returns></returns>
        public static Organisation CurrentUserOrganisation()
        {
            Organisation organisation = null;
            string currentUserUrl = "api/currentuser/orgs";
            HttpStatusCode code;
            string result;

            if (GetApiResultContent(currentUserUrl, GetApiAccessToken(), out code, out result))
            {
                var jObj = JObject.Parse(result);
                var props = jObj["Rows"][0]["Organisation"].ToString();
                organisation = JsonConvert.DeserializeObject<Organisation>(props);
            }

            return organisation;
        }

        /// <summary>
        /// Get customers from Minimax
        /// </summary>
        /// <returns></returns>
        public static List<Customer> GetCustomers()
        {
            List<Customer> customerList = new List<Customer>();

            Organisation organisation = CurrentUserOrganisation();
            HttpStatusCode resultCode;
            bool hasData = true;
            int page = 1;
            string result;

            while (hasData)
            {
                string customersUrl = $"api/orgs/{organisation.OrganisationId}/customers?PageSize=100&CurrentPage={page}";
                if (GetApiResultContent(customersUrl, GetApiAccessToken(), out resultCode, out result))
                {
                    var jsonObject = JsonConvert.DeserializeObject<RootObject<Customer>>(result);
                    if (jsonObject.Rows.Count > 0)
                    {
                        foreach (var item in jsonObject.Rows)
                        {
                            customerList.Add(item);
                        }
                        page++;
                    }
                    else
                        hasData = false;
                }
            }

            return customerList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<IssuedInvoice> GetIssuedInvoices()
        {
            List<IssuedInvoice> invoicesList = new List<IssuedInvoice>();

            Organisation organisation = CurrentUserOrganisation();
            HttpStatusCode resultCode;
            bool hasData = true;
            int page = 1;
            string result;

            while (hasData)
            {
                string invoiceUrl = $"api/orgs/{organisation.OrganisationId}/issuedinvoices?PageSize=100&CurrentPage={page}";
                if (GetApiResultContent(invoiceUrl, GetApiAccessToken(), out resultCode, out result))
                {
                    var jsonObject = JsonConvert.DeserializeObject<RootObject<IssuedInvoice>>(result);
                    if (jsonObject.Rows.Count > 0)
                    {
                        foreach (var item in jsonObject.Rows)
                        {
                            invoicesList.Add(item);
                        }
                        page++;
                    }
                    else
                        hasData = false;
                }
            }

            return invoicesList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Item> GetItems()
        {
            List<Item> itemList = new List<Item>();

            Organisation organisation = CurrentUserOrganisation();
            HttpStatusCode resultCode;
            bool hasData = true;
            int page = 1;
            string result;

            while (hasData)
            {
                string itemsUrl = $"api/orgs/{organisation.OrganisationId}/items?PageSize=100&CurrentPage={page}";
                if (GetApiResultContent(itemsUrl, GetApiAccessToken(), out resultCode, out result))
                {
                    var jsonObject = JsonConvert.DeserializeObject<RootObject<Item>>(result);
                    if (jsonObject.Rows.Count > 0)
                    {
                        foreach (var item in jsonObject.Rows)
                        {
                            itemList.Add(item);
                        }
                        page++;
                    }
                    else
                        hasData = false;
                }
            }

            return itemList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static VatRate GetVatRate(int orgId, string type)
        {
            VatRate vatRate = null;
            string token = GetApiAccessToken();
            string currentUserUrl = $"api/orgs/{orgId}/vatrates/code({type})";
            HttpStatusCode statusCode;
            string result;

            if (GetApiResultContent(currentUserUrl, token, out statusCode, out result))
                vatRate = JsonConvert.DeserializeObject<VatRate>(result);

            return vatRate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Currency GetCurrencyByCode(int orgId, string code)
        {
            Currency currency = null;
            string token = GetApiAccessToken();
            string currentUserUrl = $"api/orgs/{orgId}/currencies/code({code})";
            HttpStatusCode statusCode;
            string result;

            if (GetApiResultContent(currentUserUrl, token, out statusCode, out result))
                currency = JsonConvert.DeserializeObject<Currency>(result);

            return currency;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Account GetAccountByCode(int orgId, string code)
        {
            Account account = null;
            string token = GetApiAccessToken();
            string currentUserUrl = $"api/orgs/{orgId}/accounts/code({code})";
            HttpStatusCode statusCode;
            string result;

            if (GetApiResultContent(currentUserUrl, token, out statusCode, out result))
                account = JsonConvert.DeserializeObject<Account>(result);

            return account;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static Customer GetCustomerById(int orgId, int customerId)
        {
            Customer customer = null;
            string token = GetApiAccessToken();
            string currentUserUrl = $"api/orgs/{orgId}/customers/{customerId}";
            HttpStatusCode statusCode;
            string result;

            if (GetApiResultContent(currentUserUrl, token, out statusCode, out result))
                customer = JsonConvert.DeserializeObject<Customer>(result);

            return customer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public static Employee GetEmployeeById(int orgId, int employeeId)
        {
            Employee employee = null;
            string token = GetApiAccessToken();
            string currentUserUrl = $"api/orgs/{orgId}/employees/{employeeId}";
            HttpStatusCode statusCode;
            string result;

            if (GetApiResultContent(currentUserUrl, token, out statusCode, out result))
                employee = JsonConvert.DeserializeObject<Employee>(result);

            return employee;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Country GetCountryByCode(int orgId, string code)
        {
            Country country = null;
            string token = GetApiAccessToken();
            string currentUserUrl = $"api/orgs/{orgId}/countries/code({code})";
            HttpStatusCode statusCode;
            string result;

            if (GetApiResultContent(currentUserUrl, token, out statusCode, out result))
                country = JsonConvert.DeserializeObject<Country>(result);

            return country;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public static ReportTemplate GetReportTemplateById(int orgId, int templateId)
        {
            ReportTemplate reportTemplate = null;
            string token = GetApiAccessToken();
            string currentUserUrl = $"api/orgs/{orgId}/report-templates/{templateId}";
            HttpStatusCode statusCode;
            string result;

            if (GetApiResultContent(currentUserUrl, token, out statusCode, out result))
                reportTemplate = JsonConvert.DeserializeObject<ReportTemplate>(result);

            return reportTemplate;
        }
    }
}
