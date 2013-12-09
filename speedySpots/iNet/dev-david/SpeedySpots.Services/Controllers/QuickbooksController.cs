namespace SpeedySpots.Services.Controllers
{
   using System;
   using System.Collections.Generic;
   using System.Configuration;
   using System.IO;
   using System.Linq;
   using System.Text;
   using System.Web.Mvc;
   using API.Interfaces;
   using DataAccess;
   using InetSolution.MemberProtect;
   using Models;
   using Models.Attributes;
   using Newtonsoft.Json;

   [HandleError]
   [AuthorizeRequest]
   public class QuickbooksController : Controller
   {
      private DataAccessDataContext m_oDataAccess =
         new DataAccessDataContext(ConfigurationManager.ConnectionStrings["MemberProtectConnectionString"].ConnectionString);

      private MemberProtect m_oMemberProtect = null;
      private Result m_oResult = new Result();
      private string m_sServiceInvoicePath = string.Empty;
      private string m_sWebsiteInvoicePath = string.Empty;
      private string m_sInvoiceUrl = string.Empty;
      private string m_sPaymentUrl = string.Empty;

      public QuickbooksController()
      {
         var oSettings = new MemberProtectSettings(ConfigurationManager.ConnectionStrings["MemberProtectConnectionString"].ConnectionString);
         m_oMemberProtect = new MemberProtect(oSettings);

         m_sServiceInvoicePath = ConfigurationManager.AppSettings["ServiceInvoicePath"];
         m_sWebsiteInvoicePath = ConfigurationManager.AppSettings["WebsiteInvoicePath"];
         m_sInvoiceUrl = ConfigurationManager.AppSettings["InvoiceUrl"];
         m_sPaymentUrl = ConfigurationManager.AppSettings["PaymentUrl"];
      }

      [AcceptVerbs(HttpVerbs.Get)]
      public ActionResult Index()
      {
         return View();
      }

      #region API Methods

      [AcceptVerbs(HttpVerbs.Post)]
      public ActionResult GetLastInvoiceImportDateTime()
      {
         try
         {
            var oIAProperty = m_oDataAccess.IAProperties.SingleOrDefault();
            if (oIAProperty != null)
            {
               m_oResult.Code = 1;
               m_oResult.Data = oIAProperty.QBLastInvoiceImportDateTime;
            }
            else
            {
               m_oResult.Code = 0;
               m_oResult.Message = "IAProperty record does not exist!";
            }

            return new ObjectResult<Result>(m_oResult);
         }
         catch (Exception e)
         {
            m_oResult.Code = 0;
            m_oResult.Message = e.Message + Environment.NewLine + Environment.NewLine + e.StackTrace;

            return new ObjectResult<Result>(m_oResult);
         }
      }

      [AcceptVerbs(HttpVerbs.Post)]
      public ActionResult ValidateInvoices()
      {
         try
         {
            var sResult = string.Empty;

            var oData = new byte[Request.InputStream.Length];
            Request.InputStream.Read(oData, 0, oData.Length);

            var oMemoryStream = new MemoryStream(oData);
            var sData = Encoding.UTF8.GetString(oData);

            if (sData != string.Empty)
            {
               var sPairs = sData.Split("|".ToCharArray());
               foreach (var sPair in sPairs)
               {
                  var sPairData = sPair.Split("-".ToCharArray());
                  var sInvoiceNumber = sPairData[0];
                  var sJobID = sPairData[1];
                  var sSMSID = sPairData[2];

                  var iJobID = 0;
                  var iSMSID = 0;

                  int.TryParse(sJobID, out iJobID);
                  int.TryParse(sSMSID, out iSMSID);

                  if (iJobID > 0 && iSMSID > 0)
                  {
                     var oMPOrg = m_oDataAccess.MPOrgs.SingleOrDefault(row => row.SMSID == iSMSID);
                     var oIAJob = m_oDataAccess.IAJobs.SingleOrDefault(row => row.IAJobID == iJobID);

                     if (oMPOrg != null && oIAJob != null)
                     {
                        var oMPUserID = oIAJob.IARequest.MPUserID;
                        var oMPOrgUser = m_oDataAccess.MPOrgUsers.SingleOrDefault(row => row.MPUserID == oMPUserID);
                        if (oMPUserID != null)
                        {
                           if (oMPOrgUser.MPOrgID == oMPOrg.MPOrgID)
                           {
                              // Validation passed, add it to the list of pairs the are valid
                              sResult += sInvoiceNumber + "|";
                           }
                        }
                     }
                  }
               }

               if (sResult.Length > 1)
               {
                  sResult = sResult.Substring(0, sResult.Length - 1);
               }
            }

            m_oResult.Code = 1;
            m_oResult.Data = sResult;

            return new ObjectResult<Result>(m_oResult);
         }
         catch (Exception e)
         {
            m_oResult.Code = 0;
            m_oResult.Message = e.Message + Environment.NewLine + Environment.NewLine + e.StackTrace;

            return new ObjectResult<Result>(m_oResult);
         }
      }

      [AcceptVerbs(HttpVerbs.Post)]
      public ActionResult GetInvoices()
      {
         try
         {
            var oSB = new StringBuilder();
            foreach (var oQBInvoice in m_oDataAccess.QBInvoices)
            {
               oSB.Append(string.Format("{0}|", oQBInvoice.QBInvoiceRefID));
            }

            var sResult = oSB.ToString();
            if (sResult.Length > 0)
            {
               sResult = sResult.Substring(0, sResult.Length - 1);
            }

            m_oResult.Code = 1;
            m_oResult.Data = sResult;

            return new ObjectResult<Result>(m_oResult);
         }
         catch (Exception e)
         {
            m_oResult.Code = 0;
            m_oResult.Message = e.Message + Environment.NewLine + Environment.NewLine + e.StackTrace;

            return new ObjectResult<Result>(m_oResult);
         }
      }

      [AcceptVerbs(HttpVerbs.Post)]
      public ActionResult GetPayments()
      {
         try
         {
            var oSB = new StringBuilder();
            var oPayments = new List<Payment>();

            foreach (var oIAPayment in m_oDataAccess.IAPayments.Where(row => row.IAPaymentLineItems.Count(child => child.QBInvoiceID > 0 && !child.IsSynched) > 0))
            {
               var oPayment = new Payment();
               oPayment.CreditCardFirstName = m_oMemberProtect.Cryptography.Decrypt(oIAPayment.CreditCardFirstName);
               oPayment.CreditCardLastName = m_oMemberProtect.Cryptography.Decrypt(oIAPayment.CreditCardLastName);
               oPayment.CreditCardType = m_oMemberProtect.Cryptography.Decrypt(oIAPayment.CreditCardType);
               oPayment.AuthorizeNetID = oIAPayment.AuthorizeNetTransactionID;
               oPayment.CreatedDateTime = oIAPayment.CreatedDateTime;

               foreach (var oIAPaymentLineItem in oIAPayment.IAPaymentLineItems.Where(row => row.QBInvoiceID > 0 && !row.IsSynched))
               {
                  var oPaymentLineItem = new PaymentLineItem();
                  oPaymentLineItem.InvoiceID = oIAPaymentLineItem.Invoice;
                  oPaymentLineItem.QBInvoiceID = oIAPaymentLineItem.QBInvoiceID.ToString();
                  oPaymentLineItem.Amount = oIAPaymentLineItem.Amount;

                  oPayment.LineItems.Add(oPaymentLineItem);

                  oIAPaymentLineItem.IsSynched = true;
               }

               oPayments.Add(oPayment);
            }

            // Update all payment line items as synched
            m_oDataAccess.SubmitChanges();

            var json = JsonConvert.SerializeObject(oPayments, Formatting.Indented);

            m_oResult.Code = 1;
            m_oResult.Data = json;

            return new ObjectResult<Result>(m_oResult);
         }
         catch (Exception e)
         {
            m_oResult.Code = 0;
            m_oResult.Message = e.Message + Environment.NewLine + Environment.NewLine + e.StackTrace;

            return new ObjectResult<Result>(m_oResult);
         }
      }

      [AcceptVerbs(HttpVerbs.Post)]
      public ActionResult GetCustomers()
      {
         try
         {
            var oSB = new StringBuilder();
            var oCustomers = new List<Customer>();

            foreach (var oMPOrgData in m_oDataAccess.MPOrgDatas.Where(row => row.IsVerified == "Y" && row.IsSynched != "Y"))
            {
               var sContact = string.Empty;
               var oMPOrgUser = oMPOrgData.MPOrg.MPOrgUsers.OrderByDescending(row => row.CreatedOn).FirstOrDefault();
               if (oMPOrgUser != null)
               {
                  var oMPUserID = oMPOrgUser.MPUserID;
                  var oMPUser = m_oDataAccess.MPUsers.SingleOrDefault(row => row.MPUserID == oMPUserID);
                  if (oMPUser != null)
                  {
                     sContact = string.Format("{0} {1}", oMPUser.MPUserDatas[0].FirstName, oMPUser.MPUserDatas[0].LastName);
                  }
               }

               var oCustomer = new Customer();
               oCustomer.SMSID = oMPOrgData.MPOrg.SMSID;
               oCustomer.Name = oMPOrgData.MPOrg.Name;
               oCustomer.ShippingAddressLine1 = oMPOrgData.Address1;
               oCustomer.ShippingAddressLine2 = oMPOrgData.Address2;
               oCustomer.ShippingCity = oMPOrgData.City;
               oCustomer.ShippingState = oMPOrgData.State;
               oCustomer.ShippingCountry = oMPOrgData.Country;
               oCustomer.ShippingZip = oMPOrgData.Zip;
               oCustomer.BillingAddressLine1 = oMPOrgData.BillingAddress1;
               oCustomer.BillingAddressLine2 = oMPOrgData.BillingAddress2;
               oCustomer.BillingCity = oMPOrgData.BillingCity;
               oCustomer.BillingState = oMPOrgData.BillingState;
               oCustomer.BillingCountry = oMPOrgData.BillingCountry;
               oCustomer.BillingZip = oMPOrgData.BillingZip;
               oCustomer.BillingName = oMPOrgData.BillingName;
               oCustomer.BillingPhone = oMPOrgData.BillingPhone;
               oCustomer.Contact = sContact;
               oCustomer.Phone = oMPOrgData.Phone;
               oCustomer.Fax = oMPOrgData.Fax;
               oCustomer.InvoiceEmails = oMPOrgData.EmailInvoice;

               oCustomers.Add(oCustomer);
            }

            var json = JsonConvert.SerializeObject(oCustomers, Formatting.Indented);

            m_oResult.Code = 1;
            m_oResult.Data = json;

            return new ObjectResult<Result>(m_oResult);
         }
         catch (Exception e)
         {
            m_oResult.Code = 0;
            m_oResult.Message = e.Message + Environment.NewLine + Environment.NewLine + e.StackTrace;

            return new ObjectResult<Result>(m_oResult);
         }
      }

      [HttpPost]
      public JsonResult ImportedCustomers(List<int> smsids)
      {
         if (smsids != null)
         {
            var oListOfSMSIDs = new StringBuilder();

            foreach (var iSMSIDs in smsids)
            {
               var oMPOrgData = m_oDataAccess.MPOrgDatas.Where(row => row.MPOrg.SMSID == iSMSIDs).SingleOrDefault();
               oMPOrgData.IsSynched = "Y";
               oMPOrgData.MPOrg.SynchedDateTime = DateTime.Now;
               m_oDataAccess.SubmitChanges();
            }
         }

         return Json(new {Result = "Ok"});
      }

      [AcceptVerbs(HttpVerbs.Post)]
      public ActionResult Upload()
      {
         try
         {
            var oData = new byte[Request.InputStream.Length];
            Request.InputStream.Read(oData, 0, oData.Length);

            var oMemoryStream = new MemoryStream(oData);

            var oCredentials = new byte[100];
            oMemoryStream.Read(oCredentials, 0, 100);
            var sCredentials = Encoding.UTF8.GetString(oCredentials).Split("&".ToCharArray());
            var sApplicationLoginID = sCredentials[0].Trim();
            var sApplicationKey = sCredentials[1].Trim();

            if (sApplicationLoginID == "SpeedySpots.com" && sApplicationKey == "1zy8erTp334Deert")
            {
               var oFile = new byte[oData.Length - 100];
               oMemoryStream.Read(oFile, 0, oData.Length - 100);
               var oDataStream = new MemoryStream(oFile);

               var fileNameAndPath = string.Format("{0}\\{1}.7z", Server.MapPath(m_sServiceInvoicePath), string.Format("{0:MM-dd-yyyy hhmmss}", DateTime.Now));

               if (!Directory.Exists(Server.MapPath(m_sServiceInvoicePath)))
                  Directory.CreateDirectory(Server.MapPath(m_sServiceInvoicePath));

               System.IO.File.WriteAllBytes(fileNameAndPath, oData);

               m_oDataAccess.ExecuteCommand("INSERT INTO Worker_InvoicePackage (ArrivedDateTime, ZipFilePath, Status) VALUES ({0}, {1}, 'Waiting')", DateTime.Now,
                                            fileNameAndPath);

               m_oResult.Code = 1;
               m_oResult.Message = "File uploaded successfully.";
            }
            else
            {
               m_oResult.Code = 0;
               m_oResult.Message = "Credentials are incorrect.";
            }

            return new ObjectResult<Result>(m_oResult);
         }
         catch (Exception e)
         {
            m_oResult.Code = 0;
            m_oResult.Message = e.Message + Environment.NewLine + Environment.NewLine + e.StackTrace;

            return new ObjectResult<Result>(m_oResult);
         }
      }

      #endregion

      private string DisplayErrors()
      {
         var oSB = new StringBuilder();
         foreach (var oError in ModelState.Keys.SelectMany(key => ModelState[key].Errors))
         {
            oSB.AppendLine(oError.ErrorMessage);
         }

         return oSB.ToString();
      }

      private Guid GetOrgID(Guid oMPuserID)
      {
         var oMPOrgUser = m_oDataAccess.MPOrgUsers.FirstOrDefault(row => row.MPUserID == oMPuserID);
         if (oMPOrgUser != null)
         {
            return oMPOrgUser.MPOrgID;
         }
         else
         {
            return Guid.Empty;
         }
      }
   }
}