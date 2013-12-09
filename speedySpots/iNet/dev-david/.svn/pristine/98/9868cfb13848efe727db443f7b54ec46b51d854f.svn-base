using System;
using System.Collections.Generic;
using SevenZip;
using System.Data;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using SpeedySpots.API.Interfaces;
using log4net;

namespace SSInvoiceServiceLibrary
{
   public class InvoiceProcesses
   {
      string _errorInformation = string.Empty;
      string _connectString = string.Empty;
      string _memberProtectConnString = string.Empty;
      string _fromEmailAddress = string.Empty;
      string _paymentURL = string.Empty;
      string _invoiceURL = string.Empty;
      string _postmarkAPI = string.Empty;
      ArrayList _invoiceList;
      ArrayList _zipFileList;
      ArrayList _datFileList;
      DateTime _dateTimeStart;
      string _errorNoticeEmail = string.Empty;
      ILog _logger = log4net.LogManager.GetLogger("InvoiceProcesses");
      int _invoicesInDatFile = 0;
      int _countInvoicesProcessed = 0;
      int _countInvoicesEmailsSent = 0;
      int _countInvoiceEmailsSkipped = 0;
      int _countInvoicesWithErrorEmailSent = 0;
      string _reportEmailTo = string.Empty;

      public enum DBTables
      {
         Worker_InvoicePackage,
         Worker_InvoiceNoticeSent
      }

      private enum TableFields
      {
         ImportID,
         ZipFilePath,
         InvoiceReceived
      }

      #region Properties

      public string InvoicePath { set; private get; }

      public string ErrorInformation
      {
         get
         {
            return _errorInformation;
         }
      }

      public string ConnectString
      {
         set
         {
            _connectString = value;
         }
      }

      public string MemberConnectConnectstring
      {
         set
         {
            _memberProtectConnString = value;
         }
      }

      public string FromEmailAddress
      {
         set
         {
            _fromEmailAddress = value;
         }
      }

      public string InvoiceURL
      {
         set
         {
            _invoiceURL = value;
         }
      }

      public string PaymentURL
      {
         set
         {
            _paymentURL = value;
         }
      }

      public string PostmarkAPI
      {
         set
         {
            _postmarkAPI = value;
         }
      }

      public string ErrorNoticeEmail
      {
         set
         {
            _errorNoticeEmail = value;
         }
      }

      public string ReportEmailTo
      {
         set
         {
            _reportEmailTo = value;
         }
      }

      #endregion

      public void ProcessInvoices(DataSet ds, string invoicePath)
      {
         // Start date/time stop
         _dateTimeStart = DateTime.Now;

         // Process
         DataTable table = ds.Tables[0];
         string importID = string.Empty;

         // If this is true the entire import ID failed
         bool criticalError = false;

         // List of zip files
         _zipFileList = new ArrayList();

         // Database object
         DBFunctions oDB = new DBFunctions(_connectString);

         foreach (DataRow row in table.Rows)
         {
            try
            {
               // Get the import ID
               importID = row[TableFields.ImportID.ToString()].ToString();

               _logger.DebugFormat("Processing invoice row ID {0}", importID);

               // Update Status
               oDB.SetPackageStatusAndError(DBFunctions.JobStatus.Procesing, string.Empty, importID);

               // Get the full path for the zip file
               string zipFile = row[TableFields.ZipFilePath.ToString()].ToString();

               // Set the number of invoices expected for this zip file
               //int invoicesExpected = Convert.ToInt32(row[TableFields.InvoiceReceived.ToString()].ToString());

               // Confirm that the file is actually there - if not then set error
               if (File.Exists(zipFile) == false)
               {
                  criticalError = true;
                  _errorInformation += " - ERROR: Zip file (" + zipFile + ") cannot be found.";
               }
               else
               {
                  // Add zip file to array
                  _zipFileList.Add(zipFile);

                  // Unzip the file to the invoicePath
                  if (UnzipInvoiceFile(zipFile, InvoicePath) == true)
                  {
                     // Process the dat file and move zip to the invoice path
                     if (ProcessDateFileNew(InvoicePath, importID) == true)
                     {
                        // Any error is not critical - 
                        criticalError = false;
                     }
                     else
                     {
                        criticalError = true;
                        _errorInformation += " - .dat file read failed.";
                     }
                  }
                  else
                  {
                     criticalError = true;
                     _errorInformation += " - Invoice extraction failed.";
                  }
               }
            }
            catch (Exception ex)
            {
               _errorInformation += " - Process Invoice Error: " + ex.ToString();
            }

            // Update DB with error
            if (criticalError == true && _errorInformation.Trim().Length > 0)
            {
               _logger.ErrorFormat("Critical error while processing invoice row ID {0}: {1}", importID, _errorInformation);

               // Update the status with an error
               oDB.SetPackageStatusAndError(DBFunctions.JobStatus.Error, _errorInformation, importID);

               PostMarkEmails oPostmarkEmail = new PostMarkEmails();
               oPostmarkEmail.SendCriticalErrorEmail(_errorNoticeEmail, _fromEmailAddress, DateTime.Now.ToString(), _errorInformation, _postmarkAPI);
            }
            else if (_errorInformation.Trim().Length > 0)
            {
               // A catch all error for non-critical
               _logger.ErrorFormat("Error while processing invoice row ID {0}: {1}", importID, _errorInformation);

               PostMarkEmails oPostmarkEmail = new PostMarkEmails();
               oPostmarkEmail.SendNonCriticalErrorEmail(_errorNoticeEmail, _fromEmailAddress, DateTime.Now.ToString(), _errorInformation, _postmarkAPI);
            }

            // Reset the error ifnormation
            _errorInformation = string.Empty;
         }// End Foreach loop

         // Clean up the files
         DeleteZipFileAndDat();
      }

      private bool UnzipInvoiceFile(string fileToUnzip, string invoicePath)
      {
         bool extractSuccess = false;

         // Get location where to unzip the file
         string tempUnzip = invoicePath + "\\Temp\\";

         try
         {
            // Make sure the path exists
            if (Directory.Exists(tempUnzip) == false)
            {
               Directory.CreateDirectory(tempUnzip);
            }

            SevenZipExtractor sevenZipExtractor = new SevenZipExtractor(fileToUnzip);
            sevenZipExtractor.PreserveDirectoryStructure = false;
            sevenZipExtractor.ExtractArchive(tempUnzip);

            extractSuccess = true;
         }
         catch (SevenZipException ex)
         {
            _errorInformation += " - Unzip Invoice File Error: " + ex.ToString();
            extractSuccess = false;
         }
         catch (Exception ex2)
         {
            _errorInformation += " - Unzip Invoice File Error: " + ex2.ToString();
            extractSuccess = false;
         }

         return extractSuccess;
      }

      private bool ProcessDateFileNew(string invoicePath, string importID)
      {
         // Return true if not a critical error
         DBFunctions oDBFunctions = new DBFunctions(_connectString);
         PostMarkEmails oEmail = new PostMarkEmails();

         bool datFileProcessingSuccess = false;
         bool criticalErrorArea = true;

         string invoiceNumber = string.Empty;
         string nonCriticalError = string.Empty;

         string dateTimeStamp = string.Empty;
         string toEmail = string.Empty;
         string ccEmails = string.Empty;
         string emailResults = string.Empty;
         string postmarkErrorCode = string.Empty;
         string emailSpecificErrors = string.Empty;
         string emailSpecificNotices = string.Empty;
         bool foundJobDetails = false;
         string oldFilename = string.Empty;

         _invoiceList = new ArrayList();
         _datFileList = new ArrayList();

         // Open dat file
         string datFileName = Path.Combine(invoicePath, "Temp\\Invoices.dat");

         if (File.Exists(datFileName) == true)
         {
            try
            {
               FileStream oReadStream = new FileStream(datFileName, FileMode.Open);
               BinaryFormatter oTest = new BinaryFormatter();
               List<Invoice> oInvoices = (List<Invoice>)oTest.Deserialize(oReadStream);
               oReadStream.Close();

               foreach (Invoice oInvoice in oInvoices)
               {
                  // Not a critical error beyound this point
                  criticalErrorArea = false;

                  // This is not a critical issue area so the process will continue in here
                  try
                  {
                     string invoiceName = oInvoice.Filename;
                     _invoiceList.Add(invoiceName);
                     toEmail = string.Empty;
                     ccEmails = string.Empty;
                     emailResults = string.Empty;

                     // Get basic information
                     string jobID = oInvoice.IAJobID.ToString();

                     string invoiceID = oInvoice.InvoiceID.ToString();
                     invoiceNumber = oInvoice.InvoiceNumber;

                     // Confirm that the invoice exists in the temp directory
                     string tempInvoiceName = Path.Combine(invoicePath, "Temp", invoiceName);

                     if (File.Exists(tempInvoiceName) == true)
                     {
                        // Update the count
                        _invoicesInDatFile++;
                     }

                     foundJobDetails = oDBFunctions.SetInvoiceInformation(jobID);

                     if (foundJobDetails)
                     {
                        // Count the numnber of invoices processed
                        _countInvoicesProcessed++;

                        if (oInvoice.IsEmail)
                        {
                           if (!string.IsNullOrWhiteSpace(oDBFunctions.InvoiceEmails))
                           {
                              // Split emails
                              string[] sEmails = oDBFunctions.InvoiceEmails.Split(',');
                              bool hasToAddress = false;

                              foreach (string sEmail in sEmails)
                              {
                                 if (!string.IsNullOrEmpty(sEmail))
                                 {
                                    if (!hasToAddress)
                                    {
                                       toEmail = sEmail.Trim();
                                       hasToAddress = true;
                                    }
                                    else
                                    {
                                       // Set the rest to cc
                                       ccEmails += sEmail.Trim() + ",";
                                    }
                                 }
                              }

                              ccEmails = ccEmails.EndsWith(",") ? ccEmails.TrimEnd(',') : ccEmails;

                              var customerName = oDBFunctions.InvoiceOrganizationName;

                              // replace tokens in body
                              oInvoice.EmailBody = PersonalizeEmail(oInvoice.EmailBody, customerName, oInvoice.Filename, oInvoice.InvoiceNumber);

                              // Send the email
                              emailResults = oEmail.SendInvoiceEmail(toEmail, ccEmails, _fromEmailAddress,
                                 customerName, _invoiceURL, invoiceName,
                                 _paymentURL, oInvoice.InvoiceNumber, invoicePath + "Temp\\", _postmarkAPI,
                                 oInvoice.EmailSubject, oInvoice.EmailBody);

                              if (emailResults == "0")
                              {
                                 _countInvoicesEmailsSent++;
                                 if (oEmail.PostMarkMessage != "OK")
                                 {
                                    emailSpecificNotices += string.Format("<li>{0} - {1}</li>", oInvoice.InvoiceNumber, oEmail.PostMarkMessage);
                                 }
                              }
                              else
                              {
                                 _countInvoicesWithErrorEmailSent++;
                                 emailSpecificErrors += string.Format("<li>{0} - {1} - {2}</li>", oInvoice.InvoiceNumber, toEmail, oEmail.PostMarkMessage);
                              }
                           }
                           else
                           {
                              nonCriticalError = string.Format("Job {0} has no billing contact email address specified.", jobID);
                           }
                        }
                        else
                        {
                            emailResults = "Skipped";
                           _countInvoiceEmailsSkipped++;
                        }

                        datFileProcessingSuccess = true;
                     }
                     else
                     {
                        nonCriticalError = string.Format("Job {0} found no matching details in the database.", jobID);
                     }
                  }
                  catch (Exception exNon)
                  {
                     nonCriticalError = exNon.ToString() + " - " + oDBFunctions.ErrorInformation;
                  }

                  //-------------------
                  //Update the InvoiceNoticeSent table
                  string invoiceStatus = string.Empty;
                  if (emailResults == "0")
                  {
                     invoiceStatus = "Sent";
                  }
                  else if (emailResults == "Skipped")
                  {
                      invoiceStatus = "Email Skipped";
                  }
                  else if (nonCriticalError.Length > 0)
                  {
                      invoiceStatus = "ServiceError";
                  }
                  else
                  {
                      invoiceStatus = "PostMarkError";
                  }

                  dateTimeStamp = DateTime.Now.ToString();

                  oDBFunctions.InsertEmailSendRecord(importID, invoiceNumber, toEmail, ccEmails, dateTimeStamp, invoiceStatus,
                     oEmail.PostMarkStatus, oEmail.PostMarkMessage, oEmail.PostMarkMessageID, oEmail.PostMarkSubmittedAt,
                     nonCriticalError, oInvoice.EmailSubject, oInvoice.EmailBody);

                  if (nonCriticalError.Length > 0)
                  {
                     _errorInformation += nonCriticalError;
                  }

                  if (oDBFunctions.ErrorInformation.Length > 0)
                  {
                     _errorInformation += oDBFunctions.ErrorInformation;
                  }
                  //===================

                  // Update our DB with the details of the new invoice
                  oldFilename = string.Empty;

                  if (oDBFunctions.DoesInvoiceExsit(oInvoice, out oldFilename))
                  {
                     //Clean up the old file on disk
                     File.Delete(Path.Combine(invoicePath, oldFilename));
                     oDBFunctions.UpdateInvoiceInSMS(oInvoice);
                  }
                  else
                  {
                     oDBFunctions.RecordInvoiceInSMS(oInvoice);
                  }
               }// End of Foreach loop for each invoice
            }// End of try 
            catch (Exception ex)
            {
               _logger.ErrorFormat("Error processing dat file ID: {0} - {1}", importID, ex.Message);
               _errorInformation += ex.ToString();

               if (criticalErrorArea == false)
               {
                  datFileProcessingSuccess = true;
               }
               else
               {
                  datFileProcessingSuccess = false;
               }
            }
         }
         else
         {
            // Package error - CRITICAL
            datFileProcessingSuccess = false;
            criticalErrorArea = true;
            _errorInformation += "Unable to find .dat file";
         }// End of file exists

         try
         {
            // Clean up the package process
            datFileProcessingSuccess = true;

            // Rename the dat files
            string newDatFileName = datFileName + "." + DateTime.Now.ToFileTime().ToString();
            File.Move(datFileName, newDatFileName);
            _datFileList.Add(newDatFileName);

            // Move the invoices to the invoice directory
            foreach (string invoice in _invoiceList)
            {
               // If the files exist, rename
               if (File.Exists(invoicePath + invoice) == true)
               {
                  File.Move(invoicePath + invoice, invoicePath + invoice + "." + DateTime.Now.ToFileTime().ToString());
               }

               File.Move(invoicePath + "Temp\\" + invoice, invoicePath + invoice);
            }

            // Update the status of the package
            string packageStatus = "Completed";

            if (_errorInformation.Length > 0 || _countInvoicesWithErrorEmailSent > 0)
            {
               if (criticalErrorArea == true)
               {
                  packageStatus = "Error";
               }
               else
               {
                  packageStatus = "CompletedWithErrors";
               }
            }

            // Update Status
            oDBFunctions.UpdatePageStatistics(importID, _invoicesInDatFile, _countInvoicesProcessed, _countInvoicesEmailsSent, _countInvoiceEmailsSkipped, _countInvoicesWithErrorEmailSent, DateTime.Now.ToString(), packageStatus, _errorInformation);

            // Send out summary email
            string timeToProcess = DateTime.Now.Subtract(_dateTimeStart).ToString();
            int loc1 = timeToProcess.IndexOf(":");
            if (loc1 > 0)
            {
               timeToProcess = timeToProcess.Substring(loc1 + 1);
            }

            loc1 = timeToProcess.IndexOf(".");
            timeToProcess = timeToProcess.Substring(0, loc1);

            // if no error say so
            string errorInformation = "< No Errors >";
            if (_errorInformation.Length > 0 || _countInvoicesWithErrorEmailSent > 0 || emailSpecificNotices.Length > 0)
            {
               errorInformation = "<ul>";

               if (!string.IsNullOrEmpty(_errorInformation))
               {
                  errorInformation = string.Format("<li>{0}</li>", _errorInformation);
               }

               errorInformation += emailSpecificErrors;
               errorInformation += emailSpecificNotices;
               errorInformation += "</ul>";
            }

            string reportSendingResult = oEmail.SendSummaryEmail(_reportEmailTo, _fromEmailAddress, _dateTimeStart.ToString(), timeToProcess, _invoicesInDatFile.ToString(),
               _countInvoicesProcessed.ToString(), _countInvoicesEmailsSent.ToString(), _countInvoiceEmailsSkipped, errorInformation, _postmarkAPI);
         }
         catch (Exception ex)
         {
            // Non-Critical error
            _logger.ErrorFormat("Uncaught excpetion processing invoices: {0} - {1}", ex.Message, ex.StackTrace);
            _errorInformation = ex.ToString();
         }

         return datFileProcessingSuccess;
      }

      private string PersonalizeEmail(string emailBody, string organizationName, string invoiceFileName, string invoiceNumber)
      {          
         emailBody = emailBody.Replace("{CompanyName}", string.Format("{0}", organizationName));
         emailBody = emailBody.Replace("{InvoiceUrl}", string.Format("<a href=\"{0}{1}\">view it here</a>", _invoiceURL, invoiceFileName));
         emailBody = emailBody.Replace("{PaymentUrl}", string.Format("<a href=\"{0}?i={1}\">click here</a>", _paymentURL, invoiceNumber));
         emailBody = emailBody.Replace("\n", "<br />");
         return emailBody;
      }

      private void DeleteZipFileAndDat()
      {
         // Delete the original zip files
         foreach (string oneZipFile in _zipFileList)
         {
            File.Delete(oneZipFile);
         }

         // Delete dat files
         if (_datFileList != null)
         {
            foreach (string oneDatFile in _datFileList)
            {
               File.Delete(oneDatFile);
            }
         }
      }
   }
}