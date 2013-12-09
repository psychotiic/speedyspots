using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using SpeedySpots.API.Interfaces;
using SpeedySpots.API.Objects;

namespace SpeedySpots.API.Quickbooks
{
    public class Quickbooks
    {
        private string                              m_sEndpoint = string.Empty;
        private string                              m_sApplicationLoginID = string.Empty;
        private string                              m_sApplicationKey = string.Empty;
        private string                              m_sError = string.Empty;

        public Quickbooks(string sEndpoint, string sApplicationLoginID, string sApplicationKey)
        {
            m_sEndpoint = sEndpoint;
            m_sApplicationLoginID = sApplicationLoginID;
            m_sApplicationKey = sApplicationKey;
        }

        #region Public Methods
        public DateTime GetLastInvoiceImportDateTime()
        {
            HttpWebRequest oRequest = (HttpWebRequest)WebRequest.Create(m_sEndpoint + "/v1/Quickbooks/GetLastInvoiceImportDateTime");
            oRequest.Method = "POST";
            oRequest.KeepAlive = true;
            oRequest.ContentType = "multipart/form-data; application/json";
            oRequest.ContentLength = 0;

            WebResponse oResponse = oRequest.GetResponse();
            
            byte[] oResponseData = new byte[(int)oResponse.ContentLength];
            Stream oResponseStream = oRequest.GetResponse().GetResponseStream();
            oResponseStream.Read(oResponseData, 0, (int)oResponse.ContentLength);

            ASCIIEncoding oEncoding = new ASCIIEncoding();
            string sResponse = oEncoding.GetString(oResponseData);

            oResponseStream.Close();

            // Parse Response
            JObject oResult = JObject.Parse(sResponse);
            if((int)oResult["Code"] == 1)
            {
                return (DateTime)oResult["Data"];
            }
            else
            {
                return DateTime.Today;
            }
        }

        public List<string> GetInvoices()
        {
            HttpWebRequest oRequest = (HttpWebRequest)WebRequest.Create(m_sEndpoint + "/v1/Quickbooks/GetInvoices");
            oRequest.Method = "POST";
            oRequest.KeepAlive = true;
            oRequest.ContentType = "multipart/form-data; application/json";
            oRequest.ContentLength = 0;

            WebResponse oResponse = oRequest.GetResponse();

            MemoryStream oResponseData = new MemoryStream((int)oResponse.ContentLength);
            Stream oResponseStream = oRequest.GetResponse().GetResponseStream();

            byte[] oBuffer = new byte[1024];
            int iBytesRead = 0;
            while((iBytesRead = oResponseStream.Read(oBuffer, 0, oBuffer.Length)) != 0)
            {
                oResponseData.Write(oBuffer, 0, iBytesRead);
            }

            ASCIIEncoding oEncoding = new ASCIIEncoding();
            string sResponse = oEncoding.GetString(oResponseData.ToArray());

            oResponseStream.Close();

            // Parse Response
            JObject oResult = JObject.Parse(sResponse);
            if((int)oResult["Code"] == 1)
            {
                string sInvoiceString = (string)oResult["Data"];
                string[] sInvoices = sInvoiceString.Split("|".ToCharArray());


                return sInvoices.ToList();
            }
            else
            {
                return new List<string>();
            }
        }

        public PaymentResult GetPayments()
        {
            HttpWebRequest oRequest = (HttpWebRequest)WebRequest.Create(m_sEndpoint + "/v1/Quickbooks/GetPayments");
            oRequest.Method = "POST";
            oRequest.KeepAlive = true;
            oRequest.ContentType = "multipart/form-data; application/json";
            oRequest.ContentLength = 0;

            WebResponse oResponse = oRequest.GetResponse();

            MemoryStream oResponseData = new MemoryStream((int)oResponse.ContentLength);
            Stream oResponseStream = oRequest.GetResponse().GetResponseStream();

            byte[] oBuffer = new byte[1024];
            int iBytesRead = 0;
            while((iBytesRead = oResponseStream.Read(oBuffer, 0, oBuffer.Length)) != 0)
            {
                oResponseData.Write(oBuffer, 0, iBytesRead);
            }

            ASCIIEncoding oEncoding = new ASCIIEncoding();
            string sResponse = oEncoding.GetString(oResponseData.ToArray());

            oResponseStream.Close();

            // Parse Response
            PaymentResult oPaymentResult = new PaymentResult();

            JObject oResult = JObject.Parse(sResponse);
            if((int)oResult["Code"] == 1)
            {
                string sData = (string)oResult["Data"];

                oPaymentResult.Payments = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Payment>>(sData);
            }
            else
            {
                oPaymentResult.IsSuccessfull = false;
                oPaymentResult.ErrorMessage = (string)oResult["Message"];
            }

            return oPaymentResult;
        }

        public CustomerResult GetCustomers()
        {
            HttpWebRequest oRequest = (HttpWebRequest)WebRequest.Create(m_sEndpoint + "/v1/Quickbooks/GetCustomers");
            oRequest.Method = "POST";
            oRequest.KeepAlive = true;
            oRequest.ContentType = "multipart/form-data; application/json";
            oRequest.ContentLength = 0;

            WebResponse oResponse = oRequest.GetResponse();

            MemoryStream oResponseData = new MemoryStream((int)oResponse.ContentLength);
            Stream oResponseStream = oRequest.GetResponse().GetResponseStream();

            byte[] oBuffer = new byte[1024];
            int iBytesRead = 0;
            while((iBytesRead = oResponseStream.Read(oBuffer, 0, oBuffer.Length)) != 0)
            {
                oResponseData.Write(oBuffer, 0, iBytesRead);
            }

            ASCIIEncoding oEncoding = new ASCIIEncoding();
            string sResponse = oEncoding.GetString(oResponseData.ToArray());

            oResponseStream.Close();

            // Parse Response
            CustomerResult oCustomerResult = new CustomerResult();

            JObject oResult = JObject.Parse(sResponse);
            if((int)oResult["Code"] == 1)
            {
                string sData = (string)oResult["Data"];

                oCustomerResult.Customers = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Customer>>(sData);
            }
            else
            {
                oCustomerResult.IsSuccessfull = false;
                oCustomerResult.ErrorMessage = (string)oResult["Message"];
            }

            return oCustomerResult;
        }

        public void UpdateWebServiceOfImportedCustomer(List<int> oSMSIDImported)
        {
            string oJSONArrayOfSMSIDs = GetJSONArrayOfSMSIDs(oSMSIDImported);            

            HttpWebRequest oRequest = (HttpWebRequest)WebRequest.Create(m_sEndpoint + "/v1/Quickbooks/ImportedCustomers");
            oRequest.Method = "POST";
            oRequest.ContentType = "application/json; charset=utf-8";
            oRequest.KeepAlive = true;
            oRequest.ContentLength = oJSONArrayOfSMSIDs.Length;

            using (var oStreamWriter = new StreamWriter(oRequest.GetRequestStream()))
            {
                oStreamWriter.Write(oJSONArrayOfSMSIDs);
            }

            string responseText;
            WebResponse oResponse = oRequest.GetResponse();
            using (StreamReader oStreamReader = new StreamReader(oResponse.GetResponseStream()))
            {
                responseText = oStreamReader.ReadToEnd();
            }
        }

        private string GetJSONArrayOfSMSIDs(List<int> oSMSIDImported)
        {
            bool oHasMultipleItems = false;
            StringBuilder oSMSIDList = new StringBuilder();
            oSMSIDList.Append("{smsids: [");

            foreach (int item in oSMSIDImported)
            {
                if (oHasMultipleItems)
                {
                    oSMSIDList.AppendFormat(",{0}", item);
                }
                else
                {
                    oSMSIDList.AppendFormat("{0}", item);
                    oHasMultipleItems = true;
                }
            }
            oSMSIDList.Append("]}");

            return oSMSIDList.ToString();
        }

        public List<string> ValidateInvoices(string sData)
        {
            byte[] oData = Encoding.UTF8.GetBytes(sData);

            HttpWebRequest oRequest = (HttpWebRequest)WebRequest.Create(m_sEndpoint + "/v1/Quickbooks/ValidateInvoices");
            oRequest.Method = "POST";
            oRequest.KeepAlive = true;
            oRequest.ContentType = "multipart/form-data; application/json";
            oRequest.ContentLength = oData.Length;

            Stream oStream = oRequest.GetRequestStream();

            oStream.Write(oData, 0, oData.Length);

            WebResponse oResponse = oRequest.GetResponse();

            MemoryStream oResponseData = new MemoryStream((int)oResponse.ContentLength);
            Stream oResponseStream = oRequest.GetResponse().GetResponseStream();

            byte[] oBuffer = new byte[1024];
            int iBytesRead = 0;
            while((iBytesRead = oResponseStream.Read(oBuffer, 0, oBuffer.Length)) != 0)
            {
                oResponseData.Write(oBuffer, 0, iBytesRead);
            }

            ASCIIEncoding oEncoding = new ASCIIEncoding();
            string sResponse = oEncoding.GetString(oResponseData.ToArray());

            oResponseStream.Close();

            // Parse Response
            JObject oResult = JObject.Parse(sResponse);
            if((int)oResult["Code"] == 1)
            {
                string sInvoiceString = (string)oResult["Data"];

                if(sInvoiceString != string.Empty)
                {
                    string[] sInvoices = sInvoiceString.Split("|".ToCharArray());

                    return sInvoices.ToList();
                }
                else
                {
                    return new List<string>();
                }
            }
            else
            {
                return new List<string>();
            }
        }

        public bool Upload(string sFilename)
        {
            if(File.Exists(sFilename))
            {
                FileStream oFileStream = new FileStream(sFilename, FileMode.Open);
                byte[] oFileData = new byte[oFileStream.Length];
                oFileStream.Read(oFileData, 0, (int)oFileStream.Length);
                oFileStream.Close();

                string sCredentials = string.Format("{0}&{1}", m_sApplicationLoginID, m_sApplicationKey);
                sCredentials = sCredentials.PadRight(100, ' ');

                byte[] oCredentials = Encoding.UTF8.GetBytes(sCredentials);

                System.Net.ServicePointManager.Expect100Continue = false;
                HttpWebRequest oRequest = (HttpWebRequest)WebRequest.Create(m_sEndpoint + "/v1/Quickbooks/Upload");
                oRequest.Method = "POST";
                oRequest.KeepAlive = true;
                oRequest.ContentType = "multipart/form-data; application/json";
                oRequest.ContentLength = sCredentials.Length + oFileData.Length;
                oRequest.Timeout = 1200000;
                
                Stream oStream = oRequest.GetRequestStream();

                oStream.Write(oCredentials, 0, oCredentials.Length);
                oStream.Write(oFileData, 0, oFileData.Length);

                WebResponse oResponse = oRequest.GetResponse();

                byte[] oResponseData = new byte[(int)oResponse.ContentLength];
                Stream oResponseStream = oRequest.GetResponse().GetResponseStream();
                oResponseStream.Read(oResponseData, 0, (int)oResponse.ContentLength);

                ASCIIEncoding oEncoding = new ASCIIEncoding();
                string sResponse = oEncoding.GetString(oResponseData);

                oStream.Close();
                oResponseStream.Close();

                // Parse Response
                JObject oResult = JObject.Parse(sResponse);
                if((int)oResult["Code"] == 1)
                {
                    return true;
                }
                else
                {
                    m_sError = (string)oResult["Message"];
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Public Properties
        public string Error
        {
            get { return m_sError; }
        }
        #endregion
    }
}
