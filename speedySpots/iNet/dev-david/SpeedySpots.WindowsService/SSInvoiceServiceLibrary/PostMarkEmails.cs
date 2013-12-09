using System;
using System.Text;
using PostmarkDotNet;
using System.IO;
using System.Collections.Specialized;

namespace SSInvoiceServiceLibrary
{
   public class PostMarkEmails
   {

      #region Class Level Variables

      private string _postmarkCode = string.Empty;
      private string _postmarkStatus = string.Empty;
      private string _postmarkMessage = string.Empty;
      private string _postmarkMessageID = string.Empty;
      private DateTime _postmarkSubmittedAt = DateTime.MinValue;
      private log4net.ILog _logger = log4net.LogManager.GetLogger("PostMarkEmails");

      #endregion

      #region Properties

      public string PostMarkCode
      { get { return _postmarkCode; } }

      public string PostMarkStatus
      { get { return _postmarkStatus; } }

      public string PostMarkMessage
      { get { return _postmarkMessage; } }

      public string PostMarkMessageID
      { get { return _postmarkMessageID; } }

      public DateTime PostMarkSubmittedAt
      { get { return _postmarkSubmittedAt; } }

      #endregion

      public PostMarkEmails()
      {
         _postmarkSubmittedAt = new DateTime(1950, 1, 1);
      }

      public string SendInvoiceEmail(string sendTo, string ccEmails, string fromEmail, string organizationName,
                           string invoicURL, string fileName, string paymentURL, string invoiceNumber,
                           string invoicePath, string postMarkAPIKey, string subject, string body)
      {
         string returnMessage = string.Empty;

         // Attachment
         string fullInvoicePath = Path.Combine(invoicePath, fileName);
         string postMarkServerTokenDesc = "X-Postmark-Server-Token";

         // Build the email
         try
         {
            PostmarkMessage newMessage = new PostmarkMessage
            {
               From = fromEmail,
               To = sendTo,
               Cc = ccEmails,
               Subject = subject,
               HtmlBody = body,
               TextBody = string.Empty,
               ReplyTo = fromEmail,
               Headers = new NameValueCollection { { postMarkServerTokenDesc, postMarkAPIKey } }
            };

            newMessage.AddAttachment(fullInvoicePath, "application/pdf");

            PostmarkClient client = new PostmarkClient(postMarkAPIKey);
            PostmarkResponse response = client.SendMessage(newMessage);

            if (response.Status != PostmarkStatus.Success)
            {
               returnMessage = response.Message;
            }
            else
            {
               returnMessage = response.ErrorCode.ToString();
            }

            _postmarkCode = response.ErrorCode.ToString();
            _postmarkStatus = response.Status.ToString();
            _postmarkMessage = response.Message.ToString();
            _postmarkMessageID = response.MessageID.ToString();
            _postmarkSubmittedAt = (response.SubmittedAt > DateTime.MinValue) ? response.SubmittedAt : new DateTime(1950, 1, 1);
         }
         catch (PostmarkDotNet.Validation.ValidationException exPM)
         {
            _logger.ErrorFormat("Postmark validation exception while sending invoice email: {0}", exPM.Message);
            returnMessage = exPM.Message;

            _postmarkCode = "ValidationException";
            _postmarkStatus = "PostMarkError";
            _postmarkMessage = exPM.Message;
            _postmarkMessageID = Guid.Empty.ToString();
            _postmarkSubmittedAt = new DateTime(1950, 1, 1);
         }
         catch (Exception ex)
         {
            _logger.ErrorFormat("PM exception while sending invoice email: {0}", ex.Message);
            returnMessage = ex.Message;

            _postmarkCode = "GeneralException";
            _postmarkStatus = "PostMarkError";
            _postmarkMessage = ex.Message;
            _postmarkMessageID = Guid.Empty.ToString();
            _postmarkSubmittedAt = new DateTime(1950, 1, 1);
         }

         return returnMessage;
      }

      [Obsolete]
      public String SendInvoiceEmail(string sendTo, string ccEmails, string fromEmail, string organizationName,
                           string invoicURL, string fileName, string paymentURL, string invoiceNumber,
                           string invoicePath, string postMarkAPIKey)
      {
         string returnMessage = string.Empty;

         // Build the message
         StringBuilder oSB = new StringBuilder();
         oSB.AppendLine(string.Format("{0},", organizationName));
         oSB.AppendLine("<br/>");
         oSB.AppendLine("<br/>");
         oSB.AppendLine(string.Format("Your monthly invoice is attached. You may <a href='{0}{1}'>view it here</a>.<br/>", invoicURL, fileName));
         oSB.AppendLine("<br/>");
         oSB.AppendLine("Please pay special attention to the Balance Due at the bottom of the invoice. If any prepayments or credits have been applied to this invoice then the Balance Due will not be the same as the Invoice Total. Please pay the Balance Due.<br/>");
         oSB.AppendLine(string.Format("To make a payment <a href='{0}?i={1}'>click here</a>.<br/>", paymentURL, invoiceNumber));
         oSB.AppendLine("<br/>");
         oSB.AppendLine("Thank you for your business – we appreciate it very much.<br/>");
         oSB.AppendLine("<br/>");
         oSB.AppendLine("Sincerely,<br/>");
         oSB.AppendLine("<br/>");
         oSB.AppendLine("Rhonda Ball<br/>");
         oSB.AppendLine("Accounting Office Manager<br/>");
         oSB.AppendLine("Speedy Spots, Inc.<br/>");
         oSB.AppendLine("734-475-9327<br/>");
         oSB.AppendLine("734-475-4645 fax<br/><br/>");
         oSB.AppendLine("**All accounts that have an invoice 60 days past due will be put on Prepay status**<br/>");
         string finalBody = oSB.ToString();

         // Attachment
         string fullInvoicePath = Path.Combine(invoicePath, fileName);
         string subject = "Invoice Ready";
         string postMarkServerTokenDesc = "X-Postmark-Server-Token";

         // Build the email
         try
         {

            PostmarkMessage newMessage = new PostmarkMessage
            {
               From = fromEmail,
               To = sendTo,
               Cc = ccEmails,
               Subject = subject,
               HtmlBody = finalBody,
               TextBody = string.Empty,
               ReplyTo = fromEmail,
               Headers = new NameValueCollection { { postMarkServerTokenDesc, postMarkAPIKey } }
            };

            newMessage.AddAttachment(fullInvoicePath, "application/pdf");

            PostmarkClient client = new PostmarkClient(postMarkAPIKey);
            PostmarkResponse response = client.SendMessage(newMessage);

            if (response.Status != PostmarkStatus.Success)
            {
               returnMessage = response.Message;
            }
            else
            {
               returnMessage = response.ErrorCode.ToString();
            }

            _postmarkCode = response.ErrorCode.ToString();
            _postmarkStatus = response.Status.ToString();
            _postmarkMessage = response.Message.ToString();
            _postmarkMessageID = response.MessageID.ToString();
            _postmarkSubmittedAt = (response.SubmittedAt > DateTime.MinValue) ? response.SubmittedAt : new DateTime(1950, 1, 1);
         }
         catch (PostmarkDotNet.Validation.ValidationException exPM)
         {
            _logger.ErrorFormat("Postmark validation exception while sending invoice email: {0}", exPM.Message);
            returnMessage = exPM.Message;

            _postmarkCode = "ValidationException";
            _postmarkStatus = "PostMarkError";
            _postmarkMessage = exPM.Message;
            _postmarkMessageID = Guid.Empty.ToString();
            _postmarkSubmittedAt = new DateTime(1950, 1, 1);
         }
         catch (Exception ex)
         {
            _logger.ErrorFormat("PM exception while sending invoice email: {0}", ex.Message);
            returnMessage = ex.Message;

            _postmarkCode = "GeneralException";
            _postmarkStatus = "PostMarkError";
            _postmarkMessage = ex.Message;
            _postmarkMessageID = Guid.Empty.ToString();
            _postmarkSubmittedAt = new DateTime(1950, 1, 1);
         }

         return returnMessage;
      }

      public string SendSummaryEmail(string sendTo, string emailFrom, string arriveDateTime, string processTimeMinutes,
            string invoiceCountReceived, string invoicesCountProcessed, string invoiceEmailsSentCount, int invoiceEmailsSkippedCount, string invoiceEmailErrorInformation,
            string postMarkAPIKey)
      {
         _logger.DebugFormat("Sending summary email to: {0}", sendTo);

         string returnMessage = string.Empty;

         // Build the message
         StringBuilder oSB = new StringBuilder();
         oSB.AppendLine("The invoice package for " + arriveDateTime + " has completed and took " + processTimeMinutes);
         oSB.AppendLine(" minute(s).");
         oSB.AppendLine("<br/>");
         oSB.AppendLine("<br/>");
         oSB.AppendLine("Invoices Received:  " + invoiceCountReceived);
         oSB.AppendLine("<br/>");
         oSB.AppendLine("Invoices Processed:  " + invoicesCountProcessed);
         oSB.AppendLine("<br/>");
         oSB.AppendLine("Invoice Emails Sent:  " + invoiceEmailsSentCount);
         oSB.AppendLine("<br/>");
         if (invoiceEmailsSkippedCount > 0)
         {
            oSB.AppendLine("Invoice Emails Skipped: " + invoiceEmailsSkippedCount.ToString());
            oSB.AppendLine("<br/>");
         }
         oSB.AppendLine("Invoice Email Errors: ");
         oSB.AppendLine("<br/>");
         oSB.AppendLine("<br/>");
         oSB.AppendLine(invoiceEmailErrorInformation);

         string finalBody = oSB.ToString();

         string emailSubject = "Invoice Package Report for " + Convert.ToDateTime(arriveDateTime).ToString("M/d/yyyy");
         string postMarkServerTokenDesc = "X-Postmark-Server-Token";

         // Build the email
         PostmarkMessage message = new PostmarkMessage(emailFrom, sendTo, emailSubject, finalBody);
         try
         {

            PostmarkMessage newMessage = new PostmarkMessage
            {
               From = emailFrom,
               To = sendTo,
               Subject = emailSubject,
               HtmlBody = finalBody,
               TextBody = string.Empty,
               ReplyTo = emailFrom,
               Headers = new NameValueCollection { { postMarkServerTokenDesc, postMarkAPIKey } }
            };


            PostmarkClient client = new PostmarkClient(postMarkAPIKey);
            PostmarkResponse response = client.SendMessage(newMessage);

            // Set the response information
            string responseMessage = response.Status.ToString();

         }//
         catch (PostmarkDotNet.Validation.ValidationException exPM)
         {
            returnMessage = exPM.ToString();
            _logger.ErrorFormat("Postmark Validation error sending report: {0}", returnMessage);
         }
         catch (Exception ex)
         {
            returnMessage = ex.ToString();
            _logger.ErrorFormat("Exception sending report: {0}", returnMessage);
         }

         return returnMessage;
      }

      public string SendCriticalErrorEmail(string sendTo, string sendFrom, string dateTimeStamp, string errorInformation, string postMarkAPIKey)
      {
         _logger.DebugFormat("Sending critical error to: {0}", sendTo);

         // Send a critical error email
         string returnMessage = string.Empty;

         // Build the message
         StringBuilder oSB = new StringBuilder();
         oSB.AppendLine("As of " + dateTimeStamp + " there was a critical error in the Invoice Processing Service.");
         oSB.AppendLine("<br/>");
         oSB.AppendLine("<br/>");
         oSB.AppendLine("Error Information:");
         oSB.AppendLine("<br/>");
         oSB.AppendLine("<br/>");
         oSB.AppendLine(errorInformation);

         string finalBody = oSB.ToString();

         string emailSubject = "Invoice Processing Critical Service Error - " + dateTimeStamp;
         string postMarkServerTokenDesc = "X-Postmark-Server-Token";

         // Build the email
         PostmarkMessage message = new PostmarkMessage(sendFrom, sendTo, emailSubject, finalBody);
         try
         {

            PostmarkMessage newMessage = new PostmarkMessage
            {
               From = sendFrom,
               To = sendTo,
               Subject = emailSubject,
               HtmlBody = finalBody,
               TextBody = string.Empty,
               ReplyTo = sendFrom,
               Headers = new NameValueCollection { { postMarkServerTokenDesc, postMarkAPIKey } }
            };


            PostmarkClient client = new PostmarkClient(postMarkAPIKey);
            PostmarkResponse response = client.SendMessage(newMessage);

            // Set the response information
            string responseMessage = response.Status.ToString();

         }//
         catch (PostmarkDotNet.Validation.ValidationException exPM)
         {
            returnMessage = exPM.ToString();
            _logger.ErrorFormat("Postmark validation error sending critical error email: {0}", returnMessage);
         }
         catch (Exception ex)
         {
            returnMessage = ex.ToString();
            _logger.ErrorFormat("Exception sending critical error email: {0}", returnMessage);
         }

         return returnMessage;

      }

      public string SendNonCriticalErrorEmail(string sendTo, string sendFrom, string dateTimeStamp, string errorInformation, string postMarkAPIKey)
      {
         _logger.DebugFormat("Sending non-critical error to: {0}", sendTo);

         // Send a critical error email
         string returnMessage = string.Empty;

         // Build the message
         StringBuilder oSB = new StringBuilder();
         oSB.AppendLine("As of " + dateTimeStamp + " there was a non-critical error in the Invoice Processing Service.");
         oSB.AppendLine("<br/>");
         oSB.AppendLine("<br/>");
         oSB.AppendLine("Error Information:");
         oSB.AppendLine("<br/>");
         oSB.AppendLine("<br/>");
         oSB.AppendLine(errorInformation);

         string finalBody = oSB.ToString();

         string emailSubject = "Invoice Processing Non-Critical Service Error - " + dateTimeStamp;
         string postMarkServerTokenDesc = "X-Postmark-Server-Token";

         // Build the email
         PostmarkMessage message = new PostmarkMessage(sendFrom, sendTo, emailSubject, finalBody);
         try
         {

            PostmarkMessage newMessage = new PostmarkMessage
            {
               From = sendFrom,
               To = sendTo,
               Subject = emailSubject,
               HtmlBody = finalBody,
               TextBody = string.Empty,
               ReplyTo = sendFrom,
               Headers = new NameValueCollection { { postMarkServerTokenDesc, postMarkAPIKey } }
            };


            PostmarkClient client = new PostmarkClient(postMarkAPIKey);
            PostmarkResponse response = client.SendMessage(newMessage);

            // Set the response information
            string responseMessage = response.Status.ToString();

         }//
         catch (PostmarkDotNet.Validation.ValidationException exPM)
         {
            returnMessage = exPM.ToString();
            _logger.ErrorFormat("Postmark validation error sending non-critical error email: {0}", returnMessage);
         }
         catch (Exception ex)
         {
            returnMessage = ex.ToString();
            _logger.ErrorFormat("Exception sending non-critical error email: {0}", returnMessage);
         }

         return returnMessage;
      }
   }
}