using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.Configuration;
using nsoftware.InQB;
using SevenZip;
using SpeedySpots.API.Quickbooks;
using SpeedySpots.API.Objects;
using Persits.PDF;
using log4net;

namespace SpeedySpots_Quickbook_Connector
{
   public enum Task
   {
      ImportPaymentsAndCustomers,
      ImportInvoices,
      ImportPayments,
      ImportCustomers,
   }

   public partial class Main : Form
   {
      private static int m_iCurrent = 0;
      private static int m_iTotal = 0;
      private static string m_sDirectoryRoot = string.Format(@"Uploads\Invoices");

      private static Quickbooks m_oQuickbooks = null;
      private static BackgroundWorker m_oWorkerProcess = null;
      private static PdfManager m_oPdfManager = new PdfManager();
      private static Invoice m_oInvoice = new Invoice();
      private static List<SpeedySpots.API.Interfaces.Invoice> m_oListInvoices = new List<SpeedySpots.API.Interfaces.Invoice>();
      private ILog _logger = log4net.LogManager.GetLogger("Main");

      // Dialogs
      private SyncInvoices m_oSyncInvoices = null;

      private enum CompanyUniqueness
      {
         Unique,
         Duplicate,
         DuplicateMarkImported
      }

      public Main()
      {
         this.InitializeComponent();

         if (!Environment.CommandLine.Contains("/h."))
         {
            this.m_btnImportInvoices.Visible = false;
            this.m_btnImportPayments.Visible = false;
            this.m_btnImportCustomers.Visible = false;

            this.Height = 110;
         }
      }

      private void OnLoad(object sender, EventArgs e)
      {
         try
         {
            this.m_oProgressUpload.Minimum = 0;
            this.m_oProgressUpload.Maximum = 100;
            this.m_oProgressUpload.Value = 0;

            if (!Directory.Exists("Uploads"))
            {
               Directory.CreateDirectory("Uploads");
            }

            if (!Directory.Exists("Downloads"))
            {
               Directory.CreateDirectory("Downloads");
            }

            m_oQuickbooks = new Quickbooks(ConfigurationManager.AppSettings["SpeedySpotsServicesEndpoint"], "SpeedySpots.com", "1zy8erTp334Deert");
            this.m_oSyncInvoices = new SyncInvoices(m_oQuickbooks);
         }
         catch (Exception oException)
         {
            MessageBox.Show(oException.Message + System.Environment.NewLine + System.Environment.NewLine + oException.StackTrace);
            this.Close();
         }
      }

      private void OnClosed(object sender, FormClosedEventArgs e)
      {
         try
         {
            if (m_oWorkerProcess != null)
            {
               m_oWorkerProcess.CancelAsync();
            }
         }
         catch (Exception oException)
         {
            MessageBox.Show(oException.Message + System.Environment.NewLine + System.Environment.NewLine + oException.StackTrace);
         }
      }

      private void OnSyncPaymentsAndCustomers(object sender, EventArgs e)
      {
         this.ExecuteSync(Task.ImportPaymentsAndCustomers);
      }

      private void OnSyncInvoices(object sender, EventArgs e)
      {
         this.ExecuteSync(Task.ImportInvoices);
      }

      private void OnTestImportInvoices(object sender, EventArgs e)
      {
         this.m_lblSyncTask.Text = "Importing Invoices...";
         this.ExecuteSync(Task.ImportInvoices);
      }

      private void OnTestImportPayments(object sender, EventArgs e)
      {
         this.m_lblSyncTask.Text = "Importing Payments...";
         this.ExecuteSync(Task.ImportPayments);
      }

      private void OnTestImportCustomers(object sender, EventArgs e)
      {
         this.m_lblSyncTask.Text = "Importing Customers...";
         this.ExecuteSync(Task.ImportCustomers);
      }

      private void ExecuteSync(Task oTask)
      {
         bool bContinue = true;

         if (oTask == Task.ImportInvoices)
         {
            if (this.m_oSyncInvoices.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
               bContinue = false;
            }
         }

         if (bContinue)
         {
            this.m_btnSyncPaymentsAndCustomers.Enabled = false;
            this.m_btnSyncInvoices.Enabled = false;
            this.m_btnImportInvoices.Enabled = false;
            this.m_btnImportPayments.Enabled = false;
            this.m_btnImportCustomers.Enabled = false;

            this.m_oProgressUpload.Value = 0;
            this.m_lblProgress.Text = "0/0";

            m_oWorkerProcess = new BackgroundWorker();
            m_oWorkerProcess.WorkerReportsProgress = true;
            m_oWorkerProcess.WorkerSupportsCancellation = true;
            m_oWorkerProcess.DoWork += new DoWorkEventHandler(OnSync);
            m_oWorkerProcess.ProgressChanged += new ProgressChangedEventHandler(OnSyncProgressChanged);
            m_oWorkerProcess.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OnSyncCompleted);
            m_oWorkerProcess.RunWorkerAsync(oTask);
         }
      }

      private void OnSync(object sender, DoWorkEventArgs e)
      {
         Task oTask = (Task)e.Argument;

         try
         {
            // 1. Import Invoices
            if (oTask == Task.ImportInvoices)
            {
               try
               {
                  this.Invoke((MethodInvoker)delegate() { this.m_lblSyncTask.Text = "(1/1) Importing Invoices..."; });
               }
               catch (Exception)
               {
               }

               this.Task_ImportInvoices();
            }

            // 2. Import Payments
            if (oTask == Task.ImportPayments || oTask == Task.ImportPaymentsAndCustomers)
            {
               if (oTask == Task.ImportPaymentsAndCustomers)
               {
                  try
                  {
                     this.Invoke((MethodInvoker)delegate() { this.m_lblSyncTask.Text = "(1/2) Importing Payments..."; });
                  }
                  catch (Exception)
                  {
                  }
               }

               this.Task_ImportPayments(new Objsearch(), new Invoice(), new Customer(), new Receivepayment(), m_oQuickbooks);
            }

            // 3. Import Customers
            if (oTask == Task.ImportCustomers || oTask == Task.ImportPaymentsAndCustomers)
            {
               if (oTask == Task.ImportPaymentsAndCustomers)
               {
                  try
                  {
                     this.Invoke((MethodInvoker)delegate() { this.m_lblSyncTask.Text = "(2/2) Import Customers..."; });
                  }
                  catch (Exception)
                  {
                  }
               }

               this.Task_ImportCustomers(new Objsearch(), m_oQuickbooks);
            }

            MessageBox.Show("Finished!", "Finished", MessageBoxButtons.OK);
         }
         catch (InQBObjsearchException oException)
         {
            if (oException.Code == 606)
            {
               MessageBox.Show("Please start Quickbooks and load your company information before running sync process.", "Quickbooks Error");
            }
            else
            {
               MessageBox.Show(oException.Message);
            }
         }
         catch (Exception oException)
         {
            MessageBox.Show(oException.Message + System.Environment.NewLine + System.Environment.NewLine + oException.StackTrace);
         }
      }

      private void UpdateProgress()
      {
         int iProgress = (int)((((double)m_iCurrent) / (double)m_iTotal) * (double)100);

         if (m_oWorkerProcess != null)
         {
            m_oWorkerProcess.ReportProgress(iProgress);
         }

         m_iCurrent++;
      }

      private void OnSyncProgressChanged(object sender, ProgressChangedEventArgs e)
      {
         this.m_oProgressUpload.Value = e.ProgressPercentage;
         this.Invoke((MethodInvoker)delegate() { this.m_lblProgress.Text = string.Format("{0}/{1}", m_iCurrent, m_iTotal); });
      }

      private void OnSyncCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         this.m_btnSyncPaymentsAndCustomers.Enabled = true;
         this.m_btnSyncInvoices.Enabled = true;
         this.m_btnImportInvoices.Enabled = true;
         this.m_btnImportPayments.Enabled = true;
         this.m_btnImportCustomers.Enabled = true;
         m_oWorkerProcess.Dispose();
      }

      #region Tasks
      
      private void Task_ImportInvoices()
      {
         Objsearch oQuery = new Objsearch();
         
         try
         {
            oQuery.OpenQBConnection();
            m_oInvoice.OpenQBConnection();
            
            List<SyncInvoice> oInvoices = this.m_oSyncInvoices.GetSelectedInvoices();
            
            Directory.CreateDirectory(m_sDirectoryRoot);
            
            string sRootName = string.Format("{0:MM-dd-yyyy hhmmss}", DateTime.Now);
            string fullFileNameAndPath = string.Format(@"Uploads\{0}.7z", sRootName);
            
            m_iTotal = oInvoices.Count;
            m_iCurrent = 0;
            
            this._logger.DebugFormat("Invoices found for processing: {0}", oInvoices.Count);
            
            for (int i = 0; i < oInvoices.Count; i++)
            {
               this.ProcessInvoice(oInvoices[i]);
            }
            
            if (m_oListInvoices.Count > 0)
            {
               // Serialize invoice information
               string sInvoicesFilename = string.Format(@"{0}\Invoices.dat", m_sDirectoryRoot);
               
               this._logger.DebugFormat("Writing dat file: {0}", sInvoicesFilename);
               
               FileStream oFileStream = new FileStream(sInvoicesFilename, FileMode.Create);
               BinaryFormatter oFormatter = new BinaryFormatter();
               oFormatter.Serialize(oFileStream, m_oListInvoices);
               oFileStream.Close();
               
               this._logger.Debug("Dat file written");
               
               // Create zipped file
               this._logger.DebugFormat("Zipping folder using: {0}", Path.Combine(Application.StartupPath, "7z.dll"));
               
               SevenZipCompressor.SetLibraryPath(Path.Combine(Application.StartupPath, "7z.dll"));
               SevenZipCompressor oCompressor = new SevenZipCompressor();
               oCompressor.PreserveDirectoryRoot = true;
               oCompressor.IncludeEmptyDirectories = true;
               oCompressor.CompressDirectory(Path.Combine(Environment.CurrentDirectory, m_sDirectoryRoot), fullFileNameAndPath);
               
               this._logger.DebugFormat("Folder zipped: {0} {1}", Path.Combine(Environment.CurrentDirectory, m_sDirectoryRoot), fullFileNameAndPath);
               
               // Upload
               if (!m_oQuickbooks.Upload(fullFileNameAndPath))
               {
                  MessageBox.Show("Upload Error: " + m_oQuickbooks.Error);
               }
               else
               {
                  if (this.DeleteInvoiceFolderAndZipFile)
                  {
                     File.Delete(fullFileNameAndPath);
                  }
               }
            }
            
            m_oWorkerProcess.ReportProgress(100);
         }
         catch (Exception ex)
         {
            MessageBox.Show(string.Format("Error: {0} \n\n {1}", ex.Message, ex.StackTrace));
         }
         finally
         {
            // Delete root directory, no longer need it
            if (Directory.Exists(m_sDirectoryRoot) && this.DeleteInvoiceFolderAndZipFile)
            {
               Directory.Delete(m_sDirectoryRoot, true);
            }
            
            m_oListInvoices.Clear();
            
            oQuery.CloseQBConnection();
            m_oInvoice.CloseQBConnection();
         }
      }
      
      private void Task_ImportPayments(Objsearch oQuery, Invoice oInvoice, Customer oCustomer, Receivepayment oReceivePayment, Quickbooks oQuickbooks)
      {
         try
         {
            oQuery.OpenQBConnection();
            oInvoice.OpenQBConnection();
            oCustomer.OpenQBConnection();
            oReceivePayment.OpenQBConnection();
            
            PaymentResult oPaymentResult = oQuickbooks.GetPayments();
            if (oPaymentResult.IsSuccessfull)
            {
               m_iTotal = oPaymentResult.Payments.Count();
               m_iCurrent = 0;
               var errorCount = 0;
               var successCount = 0;
               
               foreach (SpeedySpots.API.Interfaces.Payment oPayment in oPaymentResult.Payments)
               {
                  decimal fTotal = 0;
                  string sInvoices = string.Empty;
                  oReceivePayment.Reset();
                  
                  foreach (SpeedySpots.API.Interfaces.PaymentLineItem oPaymentLineItem in oPayment.LineItems)
                  {
                     oQuery.Reset();
                     oQuery.QBXMLVersion = "5.0";
                     oQuery.QueryType = ObjsearchQueryTypes.qtInvoiceSearch;
                     oQuery.SearchCriteria = new SearchCriteria();
                     oQuery.SearchCriteria.RefNumber = oPaymentLineItem.InvoiceID;
                     oQuery.IterateResults = true;
                     oQuery.MaxResults = 0;
                     oQuery.Search();
                     
                     if (oQuery.Results.Count > 0)
                     {
                        oInvoice.Get(oQuery.Results[0].ResultId);
                        
                        AppliedTo oAppliedTo = new AppliedTo();
                        oAppliedTo.RefId = oQuery.Results[0].ResultId;
                        oAppliedTo.PaymentAmount = oPaymentLineItem.Amount.ToString("0.00");
                        
                        oReceivePayment.AppliedTo.Add(oAppliedTo);
                        
                        fTotal += oPaymentLineItem.Amount;
                        sInvoices += string.Format("{0}, ", oPaymentLineItem.InvoiceID);
                     }
                  }
                  try
                  {
                     oReceivePayment.AutoApply = ReceivepaymentAutoApplies.aaCustom;
                     oReceivePayment.CustomerName = oInvoice.CustomerName;
                     oReceivePayment.Amount = fTotal.ToString();
                     oReceivePayment.TransactionDate = oPayment.CreatedDateTime.ToString("d");
                     
                     if (oPayment.CreditCardFirstName != string.Empty)
                     {
                        oReceivePayment.RefNumber = string.Format("{0} {1}", oPayment.CreditCardFirstName, oPayment.CreditCardLastName);
                     }
                     
                     if (oPayment.CreditCardType == "Visa")
                     {
                        oReceivePayment.PaymentMethodName = "Visa";
                     }
                     else if (oPayment.CreditCardType == "MasterCard")
                     {
                        oReceivePayment.PaymentMethodName = "Mastercard";
                     }
                     else if (oPayment.CreditCardType == "American Express")
                     {
                        oReceivePayment.PaymentMethodName = "American Express";
                     }
                     else if (oPayment.CreditCardType == "Discover")
                     {
                        oReceivePayment.PaymentMethodName = "Discover";
                     }
                     
                     // Remove last ', '
                     if (sInvoices.Length > 2)
                     {
                        sInvoices = sInvoices.Substring(0, sInvoices.Length - 2);
                     }
                     
                     oReceivePayment.Memo = string.Format("Payment for invoice {0} - AuthID: {1}", sInvoices, oPayment.AuthorizeNetID);
                     
                     oReceivePayment.Add();
                     successCount++;
                  }
                  catch (Exception e)
                  {
                     errorCount++;
                     MessageBox.Show(e.Message + System.Environment.NewLine + System.Environment.NewLine + e.StackTrace);
                  }
                  
                  this.UpdateProgress();
                  
                  if (m_oWorkerProcess.CancellationPending)
                  {
                     break;
                  }
               }
               
               if (successCount > 0 || errorCount > 0)
               {
                  MessageBox.Show("Finished Payment Import" + Environment.NewLine + "Success: " + successCount + Environment.NewLine + "Error: " + errorCount);
               }
            }
            else
            {
               MessageBox.Show(oPaymentResult.ErrorMessage, "API/Service Error");
            }
            
            m_oWorkerProcess.ReportProgress(100);
         }
         catch (Exception e)
         {
            MessageBox.Show(e.Message + System.Environment.NewLine + System.Environment.NewLine + e.StackTrace);
         }
         finally
         {
            oQuery.CloseQBConnection();
            oInvoice.CloseQBConnection();
            oCustomer.CloseQBConnection();
            oReceivePayment.CloseQBConnection();
         }
      }
      
      private void Task_ImportCustomers(Objsearch oQuery, Quickbooks oQuickbooks)
      {
         try
         {
            oQuery.OpenQBConnection();
            
            oQuery.QBXMLVersion = "5.0";
            oQuery.QueryType = ObjsearchQueryTypes.qtCustomerSearch;
            
            CustomerResult oCustomerResult = oQuickbooks.GetCustomers();
            
            List<int> oSMSIDImported = new List<int>();
            
            if (oCustomerResult.IsSuccessfull)
            {
               m_iTotal = oCustomerResult.Customers.Count();
               m_iCurrent = 0;
               
               int customerCount = 0;
               int importedCount = 0;
               int duplicateNameCount = 0;
               int errorCount = 0;
               
               customerCount = oCustomerResult.Customers.Count;
               
               foreach (var oCustomer in oCustomerResult.Customers)
               {
                  oQuery.SearchCriteria = new SearchCriteria();
                  oQuery.SearchCriteria.NameStartsWith = oCustomer.Name;
                  oQuery.Search();
                  
                  CompanyUniqueness companyUnique = this.IsUniqueCustomerName(oCustomer.Name, oQuery.Results);
                  
                  if (companyUnique == CompanyUniqueness.Unique)
                  {
                     // Add new customer into Quickbooks
                     Customer oQuickbooksCustomer = new Customer();
                     oQuickbooksCustomer.OpenQBConnection();
                     
                     oQuickbooksCustomer.CustomerName = oCustomer.Name;
                     oQuickbooksCustomer.CompanyName = oCustomer.Name;
                     oQuickbooksCustomer.AltContactName = oCustomer.BillingName;
                     oQuickbooksCustomer.AltPhone = oCustomer.BillingPhone;
                     oQuickbooksCustomer.ContactName = oCustomer.Contact;
                     oQuickbooksCustomer.Phone = oCustomer.Phone;
                     oQuickbooksCustomer.Fax = oCustomer.Fax;
                     
                     if (oCustomer.ShippingCountry == "United States")
                     {
                        oQuickbooksCustomer.ShippingAddress = string.Format("<Addr1>{0}</Addr1><Addr2>{1}</Addr2><Addr3>{2}</Addr3><City>{3}</City><State>{4}</State><PostalCode>{5}</PostalCode><Country>{6}</Country>", oCustomer.Name, oCustomer.ShippingAddressLine1, oCustomer.ShippingAddressLine2, oCustomer.ShippingCity, oCustomer.ShippingState, oCustomer.ShippingZip, oCustomer.ShippingCountry);
                     }
                     else
                     {
                        // Not the United States, ignore the state value completely
                        oQuickbooksCustomer.ShippingAddress = string.Format("<Addr1>{0}</Addr1><Addr2>{1}</Addr2><Addr3>{2}</Addr3><City>{3}</City><PostalCode>{4}</PostalCode><Country>{5}</Country>", oCustomer.Name, oCustomer.ShippingAddressLine1, oCustomer.ShippingAddressLine2, oCustomer.ShippingCity, oCustomer.ShippingZip, oCustomer.ShippingCountry);
                     }
                     
                     if (oCustomer.BillingCountry == "United States")
                     {
                        oQuickbooksCustomer.BillingAddress = string.Format("<Addr1>{0}</Addr1><Addr2>{1}</Addr2><Addr3>{2}</Addr3><City>{3}</City><State>{4}</State><PostalCode>{5}</PostalCode><Country>{6}</Country>", oCustomer.Name, oCustomer.BillingAddressLine1, oCustomer.BillingAddressLine2, oCustomer.BillingCity, oCustomer.BillingState, oCustomer.BillingZip, oCustomer.BillingCountry);
                     }
                     else
                     {
                        // Not the United States, ignore the state value completely
                        oQuickbooksCustomer.BillingAddress = string.Format("<Addr1>{0}</Addr1><Addr2>{1}</Addr2><Addr3>{2}</Addr3><City>{3}</City><PostalCode>{4}</PostalCode><Country>{5}</Country>", oCustomer.Name, oCustomer.BillingAddressLine1, oCustomer.BillingAddressLine2, oCustomer.BillingCity, oCustomer.BillingZip, oCustomer.BillingCountry);
                     }
                     
                     oQuickbooksCustomer.Email = oCustomer.InvoiceEmails.Replace(",", ";");
                     oQuickbooksCustomer.TermsName = "NET 30 DAYS";
                     
                     oQuickbooksCustomer.Config(string.Format("FirstName={0}", oCustomer.Name));
                     oQuickbooksCustomer.Config("LastName=");
                     
                     try
                     {
                        oQuickbooksCustomer.Add();
                        
                        oQuickbooksCustomer.SetCustomField("SMSID", oCustomer.SMSID.ToString());
                        oSMSIDImported.Add(oCustomer.SMSID);
                        importedCount += 1;
                     }
                     catch (Exception oException)
                     {
                        errorCount += 1;
                        MessageBox.Show(oException.Message + System.Environment.NewLine + System.Environment.NewLine + oException.StackTrace);
                     }
                     finally
                     {
                        oQuickbooksCustomer.CloseQBConnection();
                     }
                  }
                  else if (companyUnique == CompanyUniqueness.DuplicateMarkImported)
                  {
                     oSMSIDImported.Add(oCustomer.SMSID);
                     duplicateNameCount += 1;
                  }
                  else
                  {
                     duplicateNameCount += 1;
                  }
                  
                  this.UpdateProgress();
                  
                  if (m_oWorkerProcess.CancellationPending)
                  {
                     break;
                  }
               }
               
               if (customerCount > 0)
               {
                  MessageBox.Show("Customer import results:" + System.Environment.NewLine + "  Attempted: " + customerCount.ToString() + System.Environment.NewLine + "  Imported: " + importedCount.ToString() + System.Environment.NewLine + "  Duplicates: " + duplicateNameCount.ToString() + System.Environment.NewLine + "  Errors: " + errorCount.ToString(), "Import Results");
               }
            }
            else
            {
               MessageBox.Show(oCustomerResult.ErrorMessage, "API/Service Error");
            }
            
            oQuickbooks.UpdateWebServiceOfImportedCustomer(oSMSIDImported);
            
            m_oWorkerProcess.ReportProgress(100);
         }
         catch (Exception e)
         {
            MessageBox.Show(e.Message + System.Environment.NewLine + System.Environment.NewLine + e.StackTrace);
         }
         finally
         {
            oQuery.CloseQBConnection();
         }
      }
      
      private CompanyUniqueness IsUniqueCustomerName(string sNewCompanyName, ObjSearchResultList oSearchResults)
      {
         CompanyUniqueness uniqueness = CompanyUniqueness.Unique;
         if (oSearchResults.Count > 0)
         {
            foreach (ObjSearchResult result in oSearchResults)
            {
               if (result.ResultName.ToLower() == sNewCompanyName.ToLower())
               {
                  bool markCompanyImported = CompanyConfirm.ShowMessage(string.Format("Company \"{0}\" was already found in QuickBooks.", result.ResultName));
                  uniqueness = markCompanyImported ? CompanyUniqueness.DuplicateMarkImported : CompanyUniqueness.Duplicate;
                  break;
               }
            }
         }
         
         return uniqueness;
      }
      
      #endregion
      
      #region Invoice Methods
      
      private void ProcessInvoice(SyncInvoice oSyncInvoice)
      {
         this._logger.DebugFormat("Processing invoice: {0}", oSyncInvoice.ID);

         m_oInvoice.Get(oSyncInvoice.ID);
         
         int iJobID = 0;
         DateTime oDateTime = DateTime.Today;
         DateTime.TryParse(m_oInvoice.GetCustomField("Export to SSMS"), out oDateTime);
         int.TryParse(m_oInvoice.GetCustomField("JobID"), out iJobID);

         this._logger.DebugFormat("Processing invoice job #: {0}", iJobID);
         
         // Only import the invoice under the following conditions:
         // 1. The invoice SSMS value is today's date or before
         // 2. The date cannot be 0001 as that means we couldn't parse the SSMS date
         if (oDateTime.CompareTo(DateTime.Today) <= 0 && oDateTime.Year != 0001 && iJobID > 0)
         {
            this._logger.DebugFormat("Processing invoice: {0}", oSyncInvoice.ID);
            PdfDocument oDocument = m_oPdfManager.CreateDocument();
            
            TextReader oTextReader = File.OpenText(Environment.CurrentDirectory + @"\templates\invoice.html");
            string sHtml = oTextReader.ReadToEnd();

            this._logger.DebugFormat("Read invoice tempate from: {0}", Environment.CurrentDirectory + @"\templates\invoice.html");
            
            string sAddress1 = string.Empty;
            string sAddress2 = string.Empty;
            string sAddress3 = string.Empty;
            string sCity = string.Empty;
            string sState = string.Empty;
            string sZip = string.Empty;
            
            // Extract Address Line 1
            Regex oRegexAddress1 = new Regex("<Addr1>.*</Addr1>");
            Match oMatchAddress1 = oRegexAddress1.Match(m_oInvoice.BillingAddress);
            if (oMatchAddress1.Success)
            {
               sAddress1 = oMatchAddress1.Value;
               sAddress1 = sAddress1.Replace("<Addr1>", string.Empty);
               sAddress1 = sAddress1.Replace("</Addr1>", string.Empty);
            
               sAddress1 = sAddress1.Replace("&apos;", "'");
            }
            
            // Extract Address Line 2
            Regex oRegexAddress2 = new Regex("<Addr2>.*</Addr2>");
            Match oMatchAddress2 = oRegexAddress2.Match(m_oInvoice.BillingAddress);
            if (oMatchAddress2.Success)
            {
               sAddress2 = oMatchAddress2.Value;
               sAddress2 = sAddress2.Replace("<Addr2>", string.Empty);
               sAddress2 = sAddress2.Replace("</Addr2>", string.Empty);
            }
            
            // Extract Address Line 3
            Regex oRegexAddress3 = new Regex("<Addr3>.*</Addr3>");
            Match oMatchAddress3 = oRegexAddress3.Match(m_oInvoice.BillingAddress);
            if (oMatchAddress3.Success)
            {
               sAddress3 = oMatchAddress3.Value;
               sAddress3 = sAddress3.Replace("<Addr3>", string.Empty);
               sAddress3 = sAddress3.Replace("</Addr3>", string.Empty);
            }
            
            // Extract City
            Regex oRegexCity = new Regex("<City>.*</City>");
            Match oMatchCity = oRegexCity.Match(m_oInvoice.BillingAddress);
            if (oMatchCity.Success)
            {
               sCity = oMatchCity.Value;
               sCity = sCity.Replace("<City>", string.Empty);
               sCity = sCity.Replace("</City>", string.Empty);
            }
            
            // Extract State
            Regex oRegexState = new Regex("<State>.*</State>");
            Match oMatchState = oRegexState.Match(m_oInvoice.BillingAddress);
            if (oMatchState.Success)
            {
               sState = oMatchState.Value;
               sState = sState.Replace("<State>", string.Empty);
               sState = sState.Replace("</State>", string.Empty);
            }
            
            // Extract Zip
            Regex oRegexZip = new Regex("<PostalCode>.*</PostalCode>");
            Match oMatchZip = oRegexZip.Match(m_oInvoice.BillingAddress);
            if (oMatchZip.Success)
            {
               sZip = oMatchZip.Value;
               sZip = sZip.Replace("<PostalCode>", string.Empty);
               sZip = sZip.Replace("</PostalCode>", string.Empty);
            }
            
            string sFullAddress = string.Empty;
            if (sAddress1 != string.Empty)
            {
               sFullAddress += sAddress1 + "<br/>";
            }
            if (sAddress2 != string.Empty)
            {
               sFullAddress += sAddress2 + "<br/>";
            }
            if (sAddress3 != string.Empty)
            {
               sFullAddress += sAddress3 + "<br/>";
            }
            sFullAddress += sCity + " " + sState + ", " + sZip;
            
            // Perform replacements
            sHtml = sHtml.Replace("[InvoiceDate]", m_oInvoice.TransactionDate);
            sHtml = sHtml.Replace("[InvoiceNumber]", m_oInvoice.RefNumber);
            sHtml = sHtml.Replace("[InvoiceBillTo]", sFullAddress);
            sHtml = sHtml.Replace("[InvoiceTerms]", "1.5% after 30 days");
            sHtml = sHtml.Replace("[InvoiceDueDate]", m_oInvoice.DueDate);
            if (m_oInvoice.LineItems.Count > 0)
            {
               string sLineItems = string.Empty;
               
               foreach (InvoiceItem oInvoiceItem in m_oInvoice.LineItems)
               {
                  string sServiceDate = string.IsNullOrEmpty(oInvoiceItem.ServiceDate) ? "&nbsp;" : oInvoiceItem.ServiceDate;
                  string sDescription = this.CleanDescription(oInvoiceItem.Description);
               
                  sLineItems += string.Format("<tr><td>{0}</td><td width=\"50%\">{1}</td><td class=\"right\">{2}</td></tr>", sServiceDate, sDescription, oInvoiceItem.Amount);
               }
            
               sHtml = sHtml.Replace("[InvoiceLineItems]", sLineItems);
            }
            sHtml = sHtml.Replace("[CustomerMessage]", m_oInvoice.CustomerMessageName);
            sHtml = sHtml.Replace("[InvoiceTotal]", m_oInvoice.Subtotal);
            sHtml = sHtml.Replace("[InvoiceCredit]", m_oInvoice.AppliedAmount);
            sHtml = sHtml.Replace("[InvoiceBalanceDue]", m_oInvoice.BalanceRemaining);

            this._logger.DebugFormat("Invoice Html: {0}", sHtml);
            
            oDocument.ImportFromUrl(sHtml);
            string sFilename = string.Format("{0}.pdf", Guid.NewGuid());

            this._logger.DebugFormat("Writing PDF: {0}", sFilename);
            
            oDocument.Save(string.Format(@"{0}\{1}", m_sDirectoryRoot, sFilename));
            oDocument.Close();

            this._logger.DebugFormat("PDF Written: {0}", string.Format(@"{0}\{1}", m_sDirectoryRoot, sFilename));
            
            SpeedySpots.API.Interfaces.Invoice oSpeedySpotsInvoice = new SpeedySpots.API.Interfaces.Invoice();
            oSpeedySpotsInvoice.InvoiceID = m_oInvoice.RefId;
            oSpeedySpotsInvoice.IAJobID = iJobID;
            oSpeedySpotsInvoice.CustomerID = m_oInvoice.CustomerId;
            oSpeedySpotsInvoice.InvoiceNumber = m_oInvoice.RefNumber;
            oSpeedySpotsInvoice.IsEmail = oSyncInvoice.IsEmail;
            
            oSpeedySpotsInvoice.EmailSubject = oSyncInvoice.EmailSubject;
            oSpeedySpotsInvoice.EmailBody = oSyncInvoice.EmailBody;
            
            DateTime oDueDateTime = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            DateTime.TryParse(m_oInvoice.DueDate, out oDueDateTime);
            oSpeedySpotsInvoice.DueDateTime = oDueDateTime;
            
            decimal fAmount = 0;
            decimal.TryParse(m_oInvoice.BalanceRemaining, out fAmount);
            oSpeedySpotsInvoice.Amount = fAmount;

            oSpeedySpotsInvoice.Filename = sFilename;

            this._logger.Debug("Added invoice to collection");
         
            m_oListInvoices.Add(oSpeedySpotsInvoice);
         }
         else
         {
            this._logger.DebugFormat("Invoice date didn't match rule: {0}", oDateTime.ToString());
         }
      
         this.UpdateProgress();
      }
      
      private string CleanDescription(string description)
      {
         if (!string.IsNullOrEmpty(description))
         {
            description = System.Web.HttpUtility.HtmlEncode(description);
            description = description.Replace("  ", "&nbsp;&nbsp;");
            description = description.Replace(Environment.NewLine, "<br/>");
            description = description.Replace("\r\n", "<br/>");
            description = description.Replace("\n", "<br/>");
         
            description = this.EncodeNonAsciiCharacters(description);
         }
      
         return description;
      }
      
      private string EncodeNonAsciiCharacters(string value)
      {
         StringBuilder sb = new StringBuilder();
         foreach (char c in value)
         {
            if (c > 127)
            {
               switch ((int)c)
               {
                  case 147:
                  case 148:
                     sb.Append("&#34;"); // Double quote
                     break;
                  case 146:
                     sb.Append("&#39;"); // single quote
                     break;
                  default:
                     break;
               }
            }
            else
            {
               sb.Append(c);
            }
         }
         return sb.ToString();
      }
      
      private bool DeleteInvoiceFolderAndZipFile
      {
         get
         {
            if (ConfigurationManager.AppSettings["RemoveInvoiceFolderAndFile"] == null)
            {
               return true;
            }
            
            if (ConfigurationManager.AppSettings["RemoveInvoiceFolderAndFile"] != null && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["RemoveInvoiceFolderAndFile"].ToString()))
            {
               return bool.Parse(ConfigurationManager.AppSettings["RemoveInvoiceFolderAndFile"].ToString());
            }
         
            return true;
         }
      }

      #endregion

   }
}