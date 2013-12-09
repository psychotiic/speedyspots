using System;
using System.Data;
using System.Data.SqlClient;
using log4net;

namespace SSInvoiceServiceLibrary
{
   public class DBFunctions
   { 
      /// <summary>
      /// Class to do all Database processing
      /// </summary>
      /// 
      #region Class Level Variables
      // For Event Loging
      const string _source = "Invoice Service App";
      const string _logType = "Application";
      private string _event = string.Empty;
      private string _errorInfo = string.Empty;
      private string _connectString = string.Empty;

      //  Variables used to set the outgoing invoice
      private string _invoiceFirstName = string.Empty;
      private string _invoiceLastName = string.Empty;
      private string _invoiceOrganizationName = string.Empty;
      private string _invoiceEmails = string.Empty;

      ILog _logger = log4net.LogManager.GetLogger("DBFunctions");

      #endregion

      #region Properties

      public string ErrorInformation
      {
         get
         {
            return _errorInfo;
         }
      }

      public string ConnectString
      {
         set
         {
            _connectString = value;
         }
      }

      public string InvoiceFirstName
      {
         get
         {
            return _invoiceFirstName;
         }
      }

      public string InvoiceLastName
      {
         get
         {
            return _invoiceLastName;
         }
      }

      public string InvoiceOrganizationName
      {
         get
         {
            return _invoiceOrganizationName;
         }
      }

      public string InvoiceEmails
      {
         get
         {
            return _invoiceEmails;
         }
      }

      #endregion

      // Enum for Status
      public enum JobStatus
      {
         Waiting,
         Procesing,
         Completed,
         CompletedWithErrors,
         Error
      }

      public DBFunctions(string connectString)
      {
         _connectString = connectString;
      }

      public void UpdatePageStatistics(string importID, int invoicesInDatFile, int invoicesProcessed, int invoiceEmailsSent, int invoieEmailsSkipped, int invoiceEmailErrors, string completedDateTime, string status, string errorMessage)
      {
         // Update the Worker_InvoicePackage with statistics
         string sql = @"UPDATE Worker_InvoicePackage
                        SET InvoiceReceived = @invoicesInDatFile,
                            InvoiceProcessed = @invoicesProcessed,
                            InvoiceEmailsSent = @invoiceEmailsSent,
                            InvoiceEmailsSkipped = @invoiceEmailsSkipped,
                            InvoiceEmailError = @invoiceEmailErrors,
                            ProcessedDateTime = @completedDateTime,
                            Status = @status,
                            ErrorMessage = @errorMessage
                        WHERE ImportID = @importID";
         try
         {
            using (SqlCommand sqlCommand = new SqlCommand(sql))
            {
               sqlCommand.Parameters.Add(new SqlParameter("invoicesInDatFile", invoicesInDatFile.ToString()));
               sqlCommand.Parameters.Add(new SqlParameter("invoicesProcessed", invoicesProcessed.ToString()));
               sqlCommand.Parameters.Add(new SqlParameter("invoiceEmailsSent", invoiceEmailsSent.ToString()));
               sqlCommand.Parameters.Add(new SqlParameter("invoiceEmailsSkipped", invoieEmailsSkipped.ToString()));
               sqlCommand.Parameters.Add(new SqlParameter("invoiceEmailErrors", invoiceEmailErrors.ToString()));
               sqlCommand.Parameters.Add(new SqlParameter("completedDateTime", completedDateTime.ToString()));
               sqlCommand.Parameters.Add(new SqlParameter("status", status));
               sqlCommand.Parameters.Add(new SqlParameter("errorMessage", errorMessage));
               sqlCommand.Parameters.Add(new SqlParameter("importID", importID));

               using (SqlConnection sqlConn = new SqlConnection(_connectString))
               {
                  sqlConn.Open();

                  if (sqlConn.State == ConnectionState.Open)
                  {
                     sqlCommand.Connection = sqlConn;
                     sqlCommand.ExecuteNonQuery();
                  }
               }
            }
         }
         catch (SqlException ex)
         {
            _logger.ErrorFormat("Error UpdatePageStatistics: {0} - {1}", ex.Message, sql);
            _errorInfo = "UpdatePageStatistics: " + ex.ToString();
         }
      }

      public void SetPackageStatusAndError(JobStatus status, string errorMessage, string importID)
      {
         // Update the Worker_InvoicePackage table status and enter an error if there is one
         string sql = @"UPDATE Worker_InvoicePackage SET Status = @status, ErrorMessage = @errorMessage WHERE ImportId = @importID";

         using (SqlCommand sqlCommand = new SqlCommand(sql))
         {
            sqlCommand.Parameters.Add(new SqlParameter("status", status));
            sqlCommand.Parameters.Add(new SqlParameter("ErrorMessage", errorMessage));
            sqlCommand.Parameters.Add(new SqlParameter("importID", importID));

            using (SqlConnection sqlConn = new SqlConnection(_connectString))
            {
               sqlConn.Open();

               if (sqlConn.State == ConnectionState.Open)
               {
                  sqlCommand.Connection = sqlConn;
                  sqlCommand.ExecuteNonQuery();
               }
            }
         }
      }

      public DataSet ReturnDataSetOfReadyInvoices()
      {
         // Create a data set of the open invoices
         string sql = "SELECT * FROM Worker_InvoicePackage WITH (NOLOCK) WHERE Status = 'Waiting' ";
         DataSet dsInvoices = new DataSet();

         try
         {
            // Create Data adapter
            using (SqlConnection conn = new SqlConnection(_connectString))
            {
               conn.Open();

               if (conn.State == ConnectionState.Open)
               {
                  using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                  {
                     // Fill the dataset with the data
                     da.Fill(dsInvoices);

                     // Close the adapter
                     da.Dispose();
                  }
               }
               else
               {
                  _errorInfo = " - Unable to connect to database";
               }

               conn.Close();
               conn.Dispose();
            }
         }
         catch (SqlException sqlEx)
         {
            _logger.ErrorFormat("Error ReturnDataSetOfReadyInvoices: {0} - {1}", sqlEx.Message, sql);
            _errorInfo += " - SQL Exception: " + sqlEx.ToString();
         }
         catch (InvalidOperationException ex)
         {
            //Utilities.WriteEventLog(_source, _logType, "ReturnDataSetOfReadyInvoices - Error: " + ex.ToString());
            _errorInfo += " - Invalid Operation: " + ex.ToString();
         }
         
         // RETURN
         return dsInvoices;
      }

      public bool SetInvoiceInformation(string jobID)
      {
         // Reset 
         bool foundDetails = false;
         _errorInfo = string.Empty;
         _invoiceOrganizationName = string.Empty;
         _invoiceEmails = string.Empty;

         // Given a jobID and invoiceID set the invoice information
         string sql = @"SELECT 
	                        MPOrg.Name, 
	                        MPOrgData.EmailInvoice, 
	                        IAJob.IAJobID 
                        FROM 
	                        IAJob INNER JOIN
	                        IARequest ON IAJob.IARequestID = IARequest.IARequestID INNER JOIN 
	                        MPOrgUser ON MPOrgUser.MPUserID = IARequest.MPUserID INNER JOIN
	                        MPOrg ON MPOrg.MPOrgID = MPOrgUser.MPOrgID INNER JOIN 
	                        MPOrgData ON MPOrg.MPOrgID = MPOrgData.MPOrgID
                        WHERE
	                        IAJob.IAJobID =  " + jobID;

         try
         {
            using (SqlConnection sqlConn = new SqlConnection(_connectString))
            {
               sqlConn.Open();

               if (sqlConn.State == ConnectionState.Open)
               {
                  using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConn))
                  {
                     using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                     {
                        if (sqlReader.HasRows == true)
                        {
                           sqlReader.Read();

                           _invoiceOrganizationName = sqlReader["Name"].ToString();
                           _invoiceEmails = sqlReader["EmailInvoice"].ToString();
                           foundDetails = true;
                        }

                        sqlReader.Close();
                        sqlReader.Dispose();
                     }

                     sqlCommand.Dispose();
                  }

                  sqlConn.Close();
                  sqlConn.Dispose();
               }
            }
         }
         catch (SqlException ex)
         {
            _logger.ErrorFormat("Error SetInvoiceInfomration: {0} - {1}", ex.Message, sql);
            _errorInfo = "SetInvoiceInfomration: " + ex.ToString();
         }

         return foundDetails;
      }

      public void InsertEmailSendRecord(string importID, string invoiceNumber, string sentTo, string ccEmails,
         string sendDateTime, string status, string postmarkErrorCode, string postmarkMessage,
         string postmarkMessageID, DateTime postmarkSubmittedAt, string serviceErrorMessage,
         string emailSubject, string emailBody)
      { 
         string sql = @"INSERT INTO Worker_InvoiceNoticeSent
                            (ImportID, InvoiceNumber, SentTo, CCEmailAddresses, SentDateTime, Status, PostMarkErrorCode,
                            PostMarkMessage, PostMarkMessageID, PostMarkSubmittedAt, ServiceErrorMessage, EmailSubject, EmailBody)
                            VALUES (@importID, @invoiceNumber, @sentTo, @ccEmails, @sendDateTime, @status, @postmarkErrorCode,
                            @postmarkMessage, @postmarkMessageID, @postmarkSubmittedAt, @serviceErrorMessage, @emailSubject, @emailBody)";
         try
         {
             emailSubject = string.IsNullOrWhiteSpace(emailSubject) ? string.Empty : emailSubject;
             emailBody = string.IsNullOrWhiteSpace(emailBody) ? string.Empty : emailBody;

            using (SqlCommand sqlCommand = new SqlCommand(sql))
            {
               sqlCommand.Parameters.Add(new SqlParameter("importID", importID));
               sqlCommand.Parameters.Add(new SqlParameter("invoiceNumber", invoiceNumber));
               sqlCommand.Parameters.Add(new SqlParameter("sentTo", sentTo));
               sqlCommand.Parameters.Add(new SqlParameter("ccEmails", ccEmails));
               sqlCommand.Parameters.Add(new SqlParameter("sendDateTime", sendDateTime));
               sqlCommand.Parameters.Add(new SqlParameter("status", status));
               sqlCommand.Parameters.Add(new SqlParameter("postmarkErrorCode", postmarkErrorCode));
               sqlCommand.Parameters.Add(new SqlParameter("postmarkMessage", postmarkMessage));
               sqlCommand.Parameters.Add(new SqlParameter("postmarkMessageID", postmarkMessageID));
               sqlCommand.Parameters.Add(new SqlParameter("postmarkSubmittedAt", postmarkSubmittedAt.ToString()));
               sqlCommand.Parameters.Add(new SqlParameter("serviceErrorMessage", serviceErrorMessage));
               sqlCommand.Parameters.Add(new SqlParameter("emailSubject", emailSubject));
               sqlCommand.Parameters.Add(new SqlParameter("emailBody", emailBody));

               using (SqlConnection sqlConn = new SqlConnection(_connectString))
               {
                  sqlConn.Open();

                  if (sqlConn.State == ConnectionState.Open)
                  {
                     sqlCommand.Connection = sqlConn;
                             
                     sqlCommand.ExecuteNonQuery();
                  }
               }
            }
         }
         catch (SqlException ex)
         {
            _logger.ErrorFormat("Error InsertEmailSendRecord: {0} - {1}", ex.Message, sql);
            _errorInfo = "InsertEmailSendRecord: " + ex.ToString();
         }
      }

      internal void RecordInvoiceInSMS(SpeedySpots.API.Interfaces.Invoice oInvoice)
      {
         string sql = @"INSERT INTO QBInvoice VALUES ( 
                                @QBInvoiceRefID,
                                @QBCustomerRefID,
                                @invoiceNumber,
                                @amount,
                                @fileName,
                                @dueDateTime,
                                @importedDateTime,
                                @IAJobID)";

         try
         {
            using (SqlCommand sqlCommand = new SqlCommand(sql))
            {
               sqlCommand.Parameters.Add(new SqlParameter("QBInvoiceRefID", oInvoice.InvoiceID));
               sqlCommand.Parameters.Add(new SqlParameter("QBCustomerRefID", oInvoice.CustomerID));
               sqlCommand.Parameters.Add(new SqlParameter("invoiceNumber", oInvoice.InvoiceNumber));
               sqlCommand.Parameters.Add(new SqlParameter("amount", oInvoice.Amount));
               sqlCommand.Parameters.Add(new SqlParameter("fileName", oInvoice.Filename));
               sqlCommand.Parameters.Add(new SqlParameter("dueDateTime", oInvoice.DueDateTime));
               sqlCommand.Parameters.Add(new SqlParameter("importedDateTime", DateTime.Now));
               sqlCommand.Parameters.Add(new SqlParameter("IAJobID", oInvoice.IAJobID));

               using (SqlConnection sqlConn = new SqlConnection(_connectString))
               {
                  sqlConn.Open();

                  if (sqlConn.State == ConnectionState.Open)
                  {
                     sqlCommand.Connection = sqlConn;
                     sqlCommand.ExecuteNonQuery();
                  }
               }
            }
         }
         catch (SqlException ex)
         {
            _logger.ErrorFormat("Error RecordInvoiceInSMS: Invoice {0} - {1} - {2}", oInvoice.InvoiceID.ToString(), ex.Message, sql);
         }
      }

      internal void UpdateInvoiceInSMS(SpeedySpots.API.Interfaces.Invoice oInvoice)
      {
         string sql = @"UPDATE QBInvoice SET 
                                QBInvoiceRefID = @QBInvoiceRefID,
                                QBCustomerRefID = @QBCustomerRefID,
                                InvoiceNumber = @invoiceNumber,
                                Amount = @amount,
                                [FileName] = @fileName,
                                DueDateTime = @dueDateTime,
                                ImportedDateTime = @importedDateTime,
                                IAJobID = @iAJobID
                            WHERE QBInvoiceRefID = @QBInvoiceRefID";

         try
         {
            using (SqlCommand sqlCommand = new SqlCommand(sql))
            {
               sqlCommand.Parameters.Add(new SqlParameter("QBInvoiceRefID", oInvoice.InvoiceID));
               sqlCommand.Parameters.Add(new SqlParameter("QBCustomerRefID", oInvoice.CustomerID));
               sqlCommand.Parameters.Add(new SqlParameter("invoiceNumber", oInvoice.InvoiceNumber));
               sqlCommand.Parameters.Add(new SqlParameter("amount", oInvoice.Amount));
               sqlCommand.Parameters.Add(new SqlParameter("fileName", oInvoice.Filename));
               sqlCommand.Parameters.Add(new SqlParameter("dueDateTime", oInvoice.DueDateTime));
               sqlCommand.Parameters.Add(new SqlParameter("importedDateTime", DateTime.Now));
               sqlCommand.Parameters.Add(new SqlParameter("iAJobID", oInvoice.IAJobID));

               using (SqlConnection sqlConn = new SqlConnection(_connectString))
               {
                  sqlConn.Open();

                  if (sqlConn.State == ConnectionState.Open)
                  {
                     sqlCommand.Connection = sqlConn;
                     sqlCommand.ExecuteNonQuery();
                  }
               }
            }
         }
         catch (SqlException ex)
         {
            _logger.ErrorFormat("Error UpdateInvoiceInSMS: Invoice {0} - {1} - {2}", oInvoice.InvoiceID.ToString(), ex.Message, sql);
         }
      }

      internal bool DoesInvoiceExsit(SpeedySpots.API.Interfaces.Invoice oInvoice, out string oldFileName)
      {
         bool invoiceExsist = false;
         oldFileName = string.Empty;

         string sql = "SELECT QBInvoiceID,[Filename] FROM QBInvoice WHERE QBInvoiceRefID = @invoiceID";
         try
         {
            using (SqlCommand sqlCommand = new SqlCommand(sql))
            {
               sqlCommand.Parameters.Add(new SqlParameter("invoiceID", oInvoice.InvoiceID));

               using (SqlConnection sqlConn = new SqlConnection(_connectString))
               {
                  sqlConn.Open();

                  if (sqlConn.State == ConnectionState.Open)
                  {
                     sqlCommand.Connection = sqlConn;

                     using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                     {
                        if (sqlReader.HasRows)
                        {
                           invoiceExsist = true;
                           sqlReader.Read();

                           oldFileName = sqlReader["Filename"].ToString();
                        }
                     }
                  }
               }
            }
         }
         catch (SqlException ex)
         {
            _logger.ErrorFormat("Error DoesInvoiceExsit: {0} - {1}", ex.Message, sql);
         }

         return invoiceExsist;
      }
   }
}