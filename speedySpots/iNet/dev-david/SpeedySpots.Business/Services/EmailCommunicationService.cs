namespace SpeedySpots.Business.Services
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Net;
   using System.Net.Mail;
   using System.Text;
   using Helpers;
   using log4net;

   public static class EmailCommunicationService
   {
      private static readonly ILog Log = LogManager.GetLogger(typeof (EmailCommunicationService));

      public static void EstimateEmailSend(StringBuilder emailBody, string subject, string to, List<string> copies)
      {
         var fromAddress = GetStandardFromAddress();
         var messageBody = emailBody.ToString();

         // Going to loop through all of the addresses as they'll all get their own copy of the estimate
         copies.Insert(0, to);

         foreach (var email in copies)
         {
            if (email.Trim() == string.Empty) continue;

            var mailMessage = new MailMessage {From = fromAddress};
            mailMessage.To.Add(new MailAddress(email));
            mailMessage.Subject = subject;
            mailMessage.Body = messageBody;
            mailMessage.IsBodyHtml = true;

            if (!SendMessage(mailMessage) && Log.IsDebugEnabled)
            {
               Log.Debug("Estimate Email send issue");
            }
         }
      }

      public static void JobTalentNotificationSend(StringBuilder emailBody, Guid talentId)
      {
         var addedTo = false;
         var fromAddress = GetStandardFromAddress();

         var mailMessage = new MailMessage
         {
            From = fromAddress,
            Subject = "New Production Order",
            Body = emailBody.ToString(),
            IsBodyHtml = true
         };

         var talentUserName = RequestContextHelper.MemberProtect.User.GetUsername(talentId);
         var toAddresses = RequestContextHelper.MemberProtect.User.GetDataItem(talentId, "AdditionalEmails").Split(',').ToList();
         toAddresses.Insert(0, talentUserName);

         // To as the first email address for the talent
         // CC any additional emails setup for the talent
         foreach (var sEmailTrimed in toAddresses.Select(emailAddress => emailAddress.Trim()).Where(Email.IsValidEmail))
         {
            if (addedTo)
            {
               mailMessage.CC.Add(new MailAddress(sEmailTrimed));
            }
            else
            {
               mailMessage.To.Add(new MailAddress(sEmailTrimed));
               addedTo = true;
            }
         }

         SendMessage(mailMessage);
      }

      public static void JobReactivedBillingNotificationSend(string messageBody, string subject, string fromAddress, string fromName)
      {
         var addedTo = false;
         var siteProperties = new SitePropertiesService();
         var billingEmailAddresses = siteProperties.EmailBillings.Split(',').ToList();

         var fromMailAddress = GetStandardFromAddress();
         var replyToAddress = new MailAddress(fromAddress, fromName);

         var mailMessage = new MailMessage
         {
            From = fromMailAddress,
            ReplyTo = replyToAddress,
            Subject = subject,
            Body = messageBody,
            IsBodyHtml = false,
            Priority = MailPriority.High
         };

         foreach (var email in billingEmailAddresses.Where(Email.IsValidEmail))
         {
            if (addedTo)
            {
               mailMessage.CC.Add(new MailAddress(email.Trim()));
            }
            else
            {
               mailMessage.To.Add(new MailAddress(email.Trim()));
               addedTo = true;
            }
         }

         SendMessage(mailMessage);
      }

      public static void EstimatePaymentCustomerReceiptSend(string subject, StringBuilder messageBody, string toAddresses)
      {
         var addedTo = false;
         var fromAddress = GetStandardFromAddress();
         var replyToAddress = GetStandardBillingFromAddress();
         var recipiantAddresses = toAddresses.Split(',').ToList();

         var mailMessage = new MailMessage
         {
            From = fromAddress,
            ReplyTo = replyToAddress,
            Subject = subject,
            Body = messageBody.ToString(),
            IsBodyHtml = true
         };

         foreach (var email in recipiantAddresses.Where(Email.IsValidEmail))
         {
            if (addedTo)
            {
               mailMessage.CC.Add(new MailAddress(email.Trim()));
            }
            else
            {
               mailMessage.To.Add(new MailAddress(email.Trim()));
               addedTo = true;
            }
         }

         SendMessage(mailMessage);
      }

      public static void EstimatePaymentBillingNoticeSend(StringBuilder messageBody, string subject)
      {
         var addedTo = false;
         var fromAddress = GetStandardFromAddress();
         var siteProperties = new SitePropertiesService();
         var toAddresses = siteProperties.EmailEstimatePayment.Split(',').ToList();

         var mailMessage = new MailMessage
         {
            From = fromAddress,
            Subject = subject,
            Body = messageBody.ToString(),
            IsBodyHtml = true
         };

         foreach (var email in toAddresses.Where(Email.IsValidEmail))
         {
            if (addedTo)
            {
               mailMessage.CC.Add(new MailAddress(email.Trim()));
            }
            else
            {
               mailMessage.To.Add(new MailAddress(email.Trim()));
               addedTo = true;
            }
         }

         SendMessage(mailMessage);
      }

      public static void SystemNotificationSend(string subject, string body)
      {
         var fromAddress = GetStandardFromAddress();
         var siteProperties = new SitePropertiesService();
         var toAddresses = siteProperties.EmailSystemNotifications.Split(',').ToList();

         var mailMessage = new MailMessage
         {
            From = fromAddress,
            Subject = subject,
            Body = body,
            IsBodyHtml = true
         };

         foreach (var email in toAddresses)
         {
            if (Email.IsValidEmail(email))
            {
               mailMessage.To.Clear();
               mailMessage.To.Add(new MailAddress(email.Trim()));
            }

            SendMessage(mailMessage);
         }
      }

      public static void JobPackageDeleteNoticeToCustomer(StringBuilder messageBody, string toAddress)
      {
         var fromAddress = GetStandardFromAddress();
         var toMailAddress = new MailAddress(toAddress);

         var mailMessage = new MailMessage {From = fromAddress};
         mailMessage.To.Add(toMailAddress);
         mailMessage.Subject = "Request Files Temporarily Pulled Down";
         mailMessage.Body = messageBody.ToString();
         mailMessage.IsBodyHtml = true;

         SendMessage(mailMessage);
      }

      public static void JobPackageDeleteNoticeToJobContacts(StringBuilder messageBody, List<string> toAddresses, string customerEmail)
      {
         customerEmail = customerEmail.ToLower();

         var fromAddress = GetStandardFromAddress();

         var mailMessage = new MailMessage
         {
            From = fromAddress,
            Subject = "Request Files Temporarily Pulled Down",
            Body = messageBody.ToString(),
            IsBodyHtml = true
         };

         // Do not re-send an email back to the same original request user, in case they're email is in the notification list as well
         foreach (
            var emailTrimmed in
               toAddresses.Select(email => email.Trim())
                          .Where(emailTrimmed => (emailTrimmed.ToLower() != customerEmail) && Email.IsValidEmail(emailTrimmed)))
         {
            mailMessage.To.Clear();
            mailMessage.To.Add(new MailAddress(emailTrimmed));

            SendMessage(mailMessage);
         }
      }

      public static void JobPackageDeliveryNoticeToJobContacts(StringBuilder messageBody, string subject, string customerEmail,
                                                               List<string> alsoNotifyEmail)
      {
         var fromAddress = GetStandardFromAddress();

         var mailMessage = new MailMessage
         {
            From = fromAddress,
            Subject = subject,
            Body = messageBody.ToString(),
            IsBodyHtml = true
         };
         mailMessage.To.Add(new MailAddress(customerEmail));

         SendMessage(mailMessage);

         //Send to the job's also notify contacts, but not if the orginal customer was included in that list too
         foreach (
            var emailTrimmed in
               alsoNotifyEmail.Select(email => email.Trim())
                              .Where(emailTrimmed => (emailTrimmed.ToLower() != customerEmail.ToLower()) && Email.IsValidEmail(emailTrimmed)))
         {
            mailMessage.To.Clear();
            mailMessage.To.Add(new MailAddress(emailTrimmed));

            SendMessage(mailMessage);
         }
      }

      public static void PasswordResetNoticeSend(StringBuilder messageBody, string emailTo)
      {
         var mailMessage = new MailMessage {From = GetStandardFromAddress()};
         mailMessage.To.Add(new MailAddress(emailTo));
         mailMessage.Subject = "Password Reset";
         mailMessage.Body = messageBody.ToString();
         mailMessage.IsBodyHtml = false;

         SendMessage(mailMessage);
      }

      public static void UserFeedbackNoticeSend(StringBuilder messageBody, string subject, string usersEmail, List<string> emailTo)
      {
         var addedTo = false;

         var emailFrom = GetStandardFromAddress();
         var replyTo = new MailAddress(usersEmail);

         var mailMessage = new MailMessage
         {
            From = emailFrom,
            ReplyTo = replyTo,
            Subject = subject,
            Body = messageBody.ToString(),
            IsBodyHtml = false
         };

         foreach (var email in emailTo.Where(Email.IsValidEmail))
         {
            if (addedTo)
            {
               mailMessage.CC.Add(new MailAddress(email.Trim()));
            }
            else
            {
               mailMessage.To.Add(new MailAddress(email.Trim()));
               addedTo = true;
            }
         }

         SendMessage(mailMessage);
      }

      public static void PaymentCustomerReceiptSend(StringBuilder messageBody, string emailTo)
      {
         var addedTo = false;
         var emailFrom = GetStandardFromAddress();
         var replyTo = GetStandardBillingFromAddress();
         var receiptEmails = emailTo.Split(',').ToList();

         var mailMessage = new MailMessage
         {
            From = emailFrom,
            ReplyTo = replyTo,
            Body = messageBody.ToString(),
            IsBodyHtml = false,
            Subject = "Payment on SpeedySpots.com"
         };

         foreach (var email in receiptEmails.Where(Email.IsValidEmail))
         {
            if (addedTo)
            {
               mailMessage.CC.Add(new MailAddress(email.Trim()));
            }
            else
            {
               mailMessage.To.Add(new MailAddress(email.Trim()));
               addedTo = true;
            }
         }

         SendMessage(mailMessage);
      }

      public static void PaymentBillingNoticeSend(StringBuilder messageBody)
      {
         var addedTo = false;
         var siteProperties = new SitePropertiesService();
         var billingEmails = siteProperties.EmailBillings.Split(',').ToList();

         var mailMessage = new MailMessage
         {
            From = GetStandardFromAddress(),
            Subject = "Payment made on SpeedySpots.com",
            Body = messageBody.ToString(),
            IsBodyHtml = false
         };

         foreach (var email in billingEmails.Where(Email.IsValidEmail))
         {
            if (addedTo)
            {
               mailMessage.CC.Add(new MailAddress(email.Trim()));
            }
            else
            {
               mailMessage.To.Add(new MailAddress(email.Trim()));
               addedTo = true;
            }
         }

         SendMessage(mailMessage);
      }

      public static void UserCancelRequestNoticeSend(int requestId, string requestNumberForDisplay, Guid orgId)
      {
         UserCancelRequestNoticeSend(requestId, requestNumberForDisplay, orgId, string.Empty);
      }

      public static void UserCancelRequestNoticeSend(int requestId, string requestNumberForDisplay, Guid orgId, string optionalMessage)
      {
         var siteProperties = new SitePropertiesService();

         var fullName = string.Format("{0} {1}", RequestContextHelper.MemberProtect.CurrentUser.GetDataItem("FirstName"),
                                      RequestContextHelper.MemberProtect.CurrentUser.GetDataItem("LastName"));
         var requestUrl = string.Format(RequestContextHelper.GetRootUrl("/sms/create-job.aspx?s=r&rid={0}"), requestId);
         var notifyEmails = siteProperties.EmailSystemNotifications.Split(',').ToList();
         var addedTo = false;
         var messageBody = string.Empty;
         var subject = string.Empty;

         if (orgId != Guid.Empty)
         {
            var companyName = RequestContextHelper.MemberProtect.Organization.GetName(orgId);
            messageBody = string.Format("{0} of {1} has cancelled <a href=\"{2}\">request #{3}</a>.", fullName, companyName, requestUrl,
                                        requestNumberForDisplay);
            subject = string.Format("Customer Canceled Request #{0}", requestNumberForDisplay);
         }
         else
         {
            messageBody = string.Format("{0} has cancelled <a href=\"{1}\">request #{2}</a>.", fullName, requestUrl, requestNumberForDisplay);
            subject = string.Format("Staff Canceled Request #{0}", requestNumberForDisplay);
         }

         if (!string.IsNullOrWhiteSpace(optionalMessage))
         {
            messageBody += string.Format("<br />Noting: {0}", optionalMessage.Replace("\n", "<br />"));
         }

         var mailMessage = new MailMessage
         {
            From = GetStandardFromAddress(),
            Subject = subject,
            Body = messageBody,
            IsBodyHtml = true
         };

         foreach (var email in notifyEmails.Where(Email.IsValidEmail))
         {
            if (addedTo)
            {
               mailMessage.CC.Add(new MailAddress(email.Trim()));
            }
            else
            {
               mailMessage.To.Add(new MailAddress(email.Trim()));
               addedTo = true;
            }
         }

         SendMessage(mailMessage);
      }

      public static void UserReactivateNoticeSend(StringBuilder messageBody)
      {
         var siteProperties = new SitePropertiesService();
         var notifyEmails = siteProperties.EmailSystemNotifications.Split(',').ToList();
         var addedTo = false;

         var mailMessage = new MailMessage
         {
            From = GetStandardFromAddress(),
            Subject = "Customer Reactivation Request",
            Body = messageBody.ToString(),
            IsBodyHtml = false
         };

         foreach (var email in notifyEmails.Where(Email.IsValidEmail))
         {
            if (addedTo)
            {
               mailMessage.CC.Add(new MailAddress(email.Trim()));
            }
            else
            {
               mailMessage.To.Add(new MailAddress(email.Trim()));
               addedTo = true;
            }
         }

         SendMessage(mailMessage);
      }

      internal static bool SendMessage(MailMessage message)
      {
         var smtpClient = GetSmtpClient();
         bool sendSuccess;
         try
         {
            smtpClient.Send(message);
            sendSuccess = true;
         }
         catch (Exception ex)
         {
            //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Log.Error(string.Format("{0} send error to {1}", message.Subject, message.To), ex);
            sendSuccess = false;
         }

         return sendSuccess;
      }

      internal static SmtpClient GetSmtpClient()
      {
         var settings = Settings.GetSmtpSettings();
         var client = new SmtpClient(settings.SmtpHost, settings.SmtpPort);

         if (!string.IsNullOrEmpty(settings.SmtpUsername) && !string.IsNullOrEmpty(settings.SmtpPassword))
         {
            var smtpCredentials = new NetworkCredential(settings.SmtpUsername, settings.SmtpPassword);
            client.Credentials = smtpCredentials;
         }

         return client;
      }

      internal static MailAddress GetStandardFromAddress()
      {
         var siteProperties = new SitePropertiesService();
         var fromAddress = new MailAddress(siteProperties.EmailAddressFrom, siteProperties.EmailAddressFromName);

         return fromAddress;
      }

      internal static MailAddress GetStandardBillingFromAddress()
      {
         var siteProperties = new SitePropertiesService();
         var billingEmailAddresses = siteProperties.EmailBillings.Split(',').ToList();
         var fromAddress = new MailAddress(billingEmailAddresses[0], siteProperties.EmailAddressFromName + " Billing");

         return fromAddress;
      }
   }
}