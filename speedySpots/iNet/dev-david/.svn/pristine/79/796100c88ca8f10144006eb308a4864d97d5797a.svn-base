namespace SpeedySpots
{
   using System;
   using System.Collections.Generic;
   using System.Configuration;
   using System.Linq;
   using System.Text.RegularExpressions;
   using System.Web.UI.WebControls;
   using Business;
   using Business.Services;
   using DataAccess;
   using Elmah;
   using InetSolution.Web;
   using Microsoft.Security.Application;
   using Objects;
   using Telerik.Web.UI;

   public partial class create_request : SiteBasePage
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         if (IsPostBack) return;

         m_hdnPageLoadTime.Value = DateTime.Now.ToString();

         SetControlVisibility();

         if (ApplicationContext.IsStaff)
         {
            m_divLookup.Visible = true;
            m_divCompanyName.Visible = true;
         }
         else
         {
            LoadCustomerInformation(MemberProtect.CurrentUser.UserID);
            LoadPaymentSources(MemberProtect.CurrentUser.UserID);
         }
      }

      protected void OnSubmit(object sender, EventArgs e)
      {
         // Validation
         if (m_radContactPhone.SelectedValue == "other")
         {
            if (m_txtContactPhone.Text == string.Empty)
            {
               SetMessage("Please select or enter a contact phone number for this request.", MessageTone.Negative);
               return;
            }
         }

         if (ApplicationContext.IsStaff)
         {
            if (string.IsNullOrEmpty(Request.Form["ffb2"]))
            {
               SetMessage("Please select the customer you are submitting a request on behalf of.", MessageTone.Negative);
               return;
            }
         }

         var currentUserId = MemberProtect.CurrentUser.UserID;
         if (ApplicationContext.IsStaff)
         {
            // Grab the customer ID for whom the producer is requesting on behalf of
            currentUserId = MemberProtect.Utility.ValidateGuid(Request.Form["ffb2"]);
            if (currentUserId == Guid.Empty)
            {
               SetMessage("Please select a valid customer.", MessageTone.Negative);
               return;
            }
         }

         if (!Page.IsValid) return;

         m_txtNotificationEmails.Text = ApplicationContext.CleanAddressList(m_txtNotificationEmails.Text);
         MemberProtect.User.SetDataItem(currentUserId, "NotificationEmails", m_txtNotificationEmails.Text);

         var iaRequest = new IARequest
         {
            MPUserID = currentUserId,
            PageLoadDateTime = DateTime.Parse(m_hdnPageLoadTime.Value),
            MPUserIDOwnedByStaff = Guid.Empty,
            MPUserIDLockedByStaff = Guid.Empty
         };

         switch (m_radContactPhone.SelectedValue)
         {
            case "user":
               iaRequest.ContactPhone = MemberProtect.User.GetDataItem(currentUserId, "Phone");
               iaRequest.ContactPhoneExtension = MemberProtect.User.GetDataItem(currentUserId, "PhoneExtension");
               break;
            case "userMobile":
               iaRequest.ContactPhone = MemberProtect.User.GetDataItem(currentUserId, "MobilePhone");
               iaRequest.ContactPhoneExtension = string.Empty;
               break;
            case "other":
               iaRequest.ContactPhone = m_txtContactPhone.Text.Trim();
               iaRequest.ContactPhoneExtension = m_txtContactPhoneExtension.Text.Trim();
               break;
         }

         iaRequest.NotificationEmails = m_txtNotificationEmails.Text.Trim();
         iaRequest.IsRushOrder = m_radSameDay.Checked;
         iaRequest.IsLocked = true;
         iaRequest.HasBeenViewedByProducer = false;
         iaRequest.EstimateRequested = "No";
         iaRequest.CreatedDateTime = DateTime.Now;

         iaRequest.ProductionNotes = string.Empty;
         iaRequest.Script = string.Empty;

         var sEstimateNote = string.Empty;
         if (m_radBeginProduction.SelectedValue == "estimate" || !m_radBeginProduction.Visible)
         {
            iaRequest.IARequestStatusID = ApplicationContext.GetRequestStatusID(RequestStatus.NeedsEstimate);

            if (!m_radBeginProduction.Visible)
            {
               iaRequest.EstimateRequested = "Auto";
               sEstimateNote = "Auto requested estimate";
            }
            else
            {
               iaRequest.EstimateRequested = "Customer";
               sEstimateNote = "Requested estimate";
            }
         }
         else
         {
            iaRequest.IARequestStatusID = ApplicationContext.GetRequestStatusID(RequestStatus.Submitted);
            iaRequest.EstimateRequested = "No";
         }

         if (PaymentPreApprovalEstimateButton.Checked)
         {
            var id = MemberProtect.Utility.ValidateInteger(PaymentSourceCombo.SelectedValue);
            if (id > 0)
            {
               iaRequest.IACustomerCreditCardID = id;
               sEstimateNote = "Pre-authorized Payment";
            }
         }

         DataAccess.IARequests.InsertOnSubmit(iaRequest);
         DataAccess.SubmitChanges();

         var hadError = false;
         var errorMessage = string.Empty;
         // Add in the production notes
         try
         {
            iaRequest.ProductionNotes = Regex.Replace(ProductionNotes, @"[^\u0000-\u007F]", "");
            DataAccess.SubmitChanges();
         }
         catch (Exception ex)
         {
            ErrorSignal.FromCurrentContext().Raise(ex);
            hadError = true;
            errorMessage = "problem saving production notes";
         }

         // Add in the script
         try
         {
            // what is this?
            iaRequest.Script = Regex.Replace(Script, @"[^\u0000-\u007F]", "");
            DataAccess.SubmitChanges();
         }
         catch (Exception ex)
         {
            ErrorSignal.FromCurrentContext().Raise(ex);
            errorMessage = (hadError) ? "problem saving production notes and script" : "problem saving script";
            hadError = true;
         }

         if (sEstimateNote != string.Empty)
         {
            iaRequest.CreateNote(MemberProtect.CurrentUser.UserID, sEstimateNote);
         }
         DataAccess.SubmitChanges();

         if (ApplicationContext.IsStaff)
         {
            iaRequest.CreateNote(MemberProtect.CurrentUser.UserID, "Request created on behalf of the customer");
            DataAccess.SubmitChanges();
         }

         try
         {
            ProcessUploadedFiles(iaRequest.IARequestID);
         }
         catch (Exception ex)
         {
            ErrorSignal.FromCurrentContext().Raise(ex);
            errorMessage = (hadError) ? errorMessage + " and uploading files" : "problem uploading files.";
         }

         Session["IARequestID"] = iaRequest.IARequestID;
         Session["errorMessage"] = errorMessage;
         Response.Redirect("~/create-request-confirm.aspx");
      }

      public override void OnAjaxRequest(object sender, AjaxRequestEventArgs e)
      {
         var customerGuid = MemberProtect.Utility.ValidateGuid(e.Argument);
         if (customerGuid == Guid.Empty) return;

         LoadCustomerInformation(customerGuid);
         LoadPaymentSources(customerGuid);
      }

      private void LoadPaymentSources(Guid customerMemberProtectUserId)
      {
         var service = GetCardService(customerMemberProtectUserId);
         var cards = service.GetCreditCards().ToList();

         if (!cards.Any())
         {
            paymentpreapprovalDiv.Visible = false;
            return;
         }
         paymentpreapprovalDiv.Visible = true;
         PaymentSourceCombo.Items.AddRange(from card in cards select new RadComboBoxItem("Pay with " + card.Alias, card.CreditCardId.ToString()));
      }

      private void LoadCustomerInformation(Guid userId)
      {
         // Gather some default information for customers submitting requests
         LoadContactPhoneInformation(userId);

         m_lblCompanyName.Text = MemberProtect.Organization.GetName(ApplicationContext.GetOrgID(userId));
         m_lblUserName.Text = AntiXss.HtmlEncode(string.Format("{0} {1}",
                                                               MemberProtect.User.GetDataItem(userId, "FirstName"),
                                                               MemberProtect.User.GetDataItem(userId, "LastName")));
         var notificationEmails = MemberProtect.User.GetDataItem(userId, "NotificationEmails");
         m_txtNotificationEmails.Text = !string.IsNullOrEmpty(notificationEmails)
                                           ? ApplicationContext.CleanAddressList(notificationEmails)
                                           : MemberProtect.User.GetUsername(userId);
         m_divBeginProduction.Visible = !MemberProtect.Utility.YesNoToBool(MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(userId), "IsPayFirst"));
      }

      private void LoadContactPhoneInformation(Guid userId)
      {
         var sUserMobile = MemberProtect.User.GetDataItem(userId, "MobilePhone");
         var sUserPhone = MemberProtect.User.GetDataItem(userId, "Phone");
         if (MemberProtect.User.GetDataItem(userId, "PhoneExtension") != string.Empty)
         {
            sUserPhone += string.Format(" x {0}", MemberProtect.User.GetDataItem(userId, "PhoneExtension"));
         }

         m_radContactPhone.Items.Clear();
         m_radContactPhone.Items.Add(new ListItem(sUserPhone, "user"));
         if (!string.IsNullOrEmpty(sUserMobile))
         {
            sUserMobile = string.Format("{0} - Cell", sUserMobile);
            m_radContactPhone.Items.Add(new ListItem(sUserMobile, "userMobile"));
         }
         m_radContactPhone.Items.Add(new ListItem("Other:", "other"));

         // Pre-select the user phone number
         m_radContactPhone.SelectedIndex = 0;
      }

      private bool ProcessUploadedFiles(int iaRequstId)
      {
         if (!IsIPhone())
         {
            if (m_oUpload.UploadedFiles.Count > 0)
            {
               foreach (UploadedFile oFile in m_oUpload.UploadedFiles)
               {
                  // Create file record
                  var iaRequestFile = new IARequestFile
                  {
                     IARequestID = iaRequstId,
                     Filename = oFile.GetName(),
                     FilenameUnique = string.Format("{0}{1}", Guid.NewGuid(), oFile.GetExtension()),
                     FileSize = oFile.ContentLength,
                     CreatedDateTime = DateTime.Now
                  };
                  DataAccess.IARequestFiles.InsertOnSubmit(iaRequestFile);
                  DataAccess.SubmitChanges();

                  // Save physical file under a new name
                  oFile.SaveAs(string.Format("{0}{1}", ApplicationContext.UploadPath, iaRequestFile.FilenameUnique));
               }

               return true;
            }
         }

         return false;
      }

      private bool IsIPhone()
      {
         var bDebugIPhone = false;
         bool.TryParse(ConfigurationManager.AppSettings["DebugIPhone"], out bDebugIPhone);

         if (bDebugIPhone)
         {
            return true;
         }

         var sUserAgent = Request.UserAgent;
         if (!string.IsNullOrEmpty(sUserAgent))
         {
            return sUserAgent.ToLowerInvariant().Contains("iphone") || sUserAgent.ToLowerInvariant().Contains("ipad");
         }
         return false;
      }

      private void SetControlVisibility()
      {
         m_txtIScript.Visible = IsIPhone();
         m_txtIProductionNotes.Visible = IsIPhone();
         m_divNoIOSUpload.Visible = !IsIPhone();
         m_txtScript.Visible = !IsIPhone();
         m_txtProductionNotes.Visible = !IsIPhone();
         m_oUpload.Visible = !IsIPhone();
      }

      private string Script
      {
         get { return StripNewlineCharchters(IsIPhone() ? m_txtIScript.Text : m_txtScript.Content); }
      }

      private string ProductionNotes
      {
         get
         {
            return IsIPhone()
                      ? m_txtIProductionNotes.Text.Replace("\r", "\n").Replace("\n\n", "\n").Replace("\n", string.Empty)
                      : m_txtProductionNotes.Content.Replace("\r", "\n").Replace("\n\n", "\n").Replace("\n", string.Empty);
         }
      }

      private CreditCardService GetCardService(Guid customerMemberProtectUserId)
      {
         return new CreditCardService(customerMemberProtectUserId, MemberProtect, DataAccess, ApplicationContext.SiteProperites);
      }

      public override List<AccessControl> GetAccessControl()
      {
         return new List<AccessControl> {AccessControl.Customer, AccessControl.Staff};
          
      }
   }
}