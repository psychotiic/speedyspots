namespace SpeedySpots.Controls
{
   using System;
   using System.Linq;
   using System.Web.UI.WebControls;
   using Business.Helpers;
   using Business.Services;
   using DataAccess;

   public partial class RequestDetails : SiteBaseControl
   {
      private IARequest currentRequest = null;

      protected void Page_Load(object sender, EventArgs e)
      {
         if (!Page.IsPostBack)
         {
            ReloadDetails();
         }
      }

      public void ReloadDetails()
      {
         SetCurrentRequest();
         SetRequestDetails();
      }

      protected void OnNotifyEdit(object sender, EventArgs e)
      {
         divNotifyEdit.Visible = true;
         divNotifyOutput.Visible = false;
         txtNotifyEdit.Text = litNotify.Text.Replace(",", "\n").Replace(" ", "");
      }

      protected void OnNotifyUpdate(object sender, EventArgs e)
      {
         if (Page.IsValid)
         {
            SetCurrentRequest();

            litNotify.Text = Email.ConvertMultiLineEmailsToCommaSeperated(txtNotifyEdit.Text);
            RequestsService.UpdateNotifyEMails(currentRequest.IARequestID, litNotify.Text, DataAccess);

            currentRequest.NotificationEmails = litNotify.Text;
            divNotifyEdit.Visible = false;
            divNotifyOutput.Visible = true;
         }
      }

      protected void OnNotifyValidate(object sender, ServerValidateEventArgs e)
      {
         var emails = Email.ConvertMultiLineEmailsToCommaSeperated(txtNotifyEdit.Text);
         e.IsValid = emails.Length >= 3 ? true : false;
      }

      private void SetCurrentRequest()
      {
         var requestId = 0;

         if (Request.QueryString["rid"] != null)
         {
            requestId = MemberProtect.Utility.ValidateInteger(Request.QueryString["rid"].ToString());
         }

         if (requestId == 0 && Request.QueryString["jid"] != null)
         {
            var jobId = MemberProtect.Utility.ValidateInteger(Request.QueryString["jid"].ToString());
            var oIAJob = DataAccess.IAJobs.SingleOrDefault(row => row.IAJobID == jobId);
            if (oIAJob != null)
            {
               requestId = oIAJob.IARequestID;
            }
         }

         if (requestId <= 0 && Session["IARequestID"] != null)
         {
            requestId = int.Parse(Session["IARequestID"].ToString());
         }

         if (requestId > 0)
         {
            Session["IARequestID"] = requestId;
            currentRequest = DataAccess.IARequests.SingleOrDefault(row => row.IARequestID == requestId);
         }
         else
         {
            throw new ApplicationException("Request cannot be found.");
         }
      }

      private void SetRequestDetails()
      {
         litIDForDisplay.Text = currentRequest.RequestIdForDisplay;
         litIDForDisplay.Text += (currentRequest.IsRushOrder) ? " <span style=\"color:Red\">ASAP</span>" : string.Empty;

         divHasEstimate.Visible = (currentRequest.EstimateRequested == "Auto"
                                   || currentRequest.EstimateRequested == "Customer"
                                   || currentRequest.IACustomerCreditCardID > 0);

         litEstimateDisplay.Text = GetEstimateDisplay();

         litStatus.Text = GetDisplayStatus();
         litSubmittedBy.Text = GetSubmittedBy();

         litContactPhone.Text = GetContactPhone();

         litCompany.Text = GetCompany();

         litNotify.Text = currentRequest.NotificationEmails;
         hlNotifyEdit.Visible = ApplicationContext.IsStaff;
         divNotifyEdit.Visible = false;
         divNotifyOutput.Visible = true;

         divScript.Visible = !string.IsNullOrWhiteSpace(currentRequest.Script);
         hlScript.NavigateUrl = string.Format(hlScript.NavigateUrl, currentRequest.IARequestID);

         divProdNotes.Visible = !string.IsNullOrWhiteSpace(currentRequest.ProductionNotes);
         hlProdNotes.NavigateUrl = string.Format(hlProdNotes.NavigateUrl, currentRequest.IARequestID);

         divFiles.Visible = (currentRequest.IARequestFiles.Count > 0);
         rptFiles.DataSource = currentRequest.IARequestFiles;
         rptFiles.DataBind();

         divEstimates.Visible = ((currentRequest.IARequestEstimates.Count > 0) && (!ApplicationContext.IsCustomer));
         if (divEstimates.Visible)
         {
            hlEstimates.NavigateUrl = string.Format(hlEstimates.NavigateUrl, currentRequest.IARequestEstimates[0].IARequestEstimateID);
         }

         litCreatedOn.Text = string.Format("{0:ddd, MMMM dd, yyyy a\\t h:mm tt}", currentRequest.CreatedDateTime);
      }

      private string GetEstimateDisplay()
      {
         var display = string.Empty;

         if (currentRequest.IARequestEstimates.Any())
         {
            switch (currentRequest.EstimateRequested)
            {
               case "Auto":
                  display = "<span class='output'>Mandated Estimate Sent</span>";
                  break;
               default:
                  display = "<span class='output'>Requested Estimate Sent</span>";
                  break;
            }
         }
         else
         {
            switch (currentRequest.EstimateRequested)
            {
               case "Auto":
                  display = "<span class='output mandated'>Mandated Estimate";
                  if (currentRequest.IACustomerCreditCardID > 0)
                     display += " and Authorized Payment";
                  display += "</span>";
                  break;
               case "Customer":
                  display = "<span class='output requested'>Requested Estimate";
                  if (currentRequest.IACustomerCreditCardID > 0)
                     display += " and Pre-Authorized Payment";
                  display += "</span>";
                  break;
               case "No":
                  display = "<span class='output requested'>";
                  if (currentRequest.IACustomerCreditCardID > 0)
                     display += "Pre-Authorized Payment";
                  display += "</span>";
                  break;
            }
         }

         return display;
      }

      public string GetDisplayStatus()
      {
         var tag = "<span class=\"output\">{0}</span>";
         var tagMandated = "<span class=\"output mandated\">{0}</span>";

         tag = currentRequest.IARequestStatus.Name.ToUpper() == "CANCELED" ? tagMandated : tag;

         return string.Format(tag, currentRequest.IARequestStatus.Name);
      }

      public string GetSubmittedBy()
      {
         var tag = string.Empty;

         if (ApplicationContext.IsCustomer)
         {
            tag += string.Format("{0} {1}", MemberProtect.User.GetDataItem(currentRequest.MPUserID, "FirstName"),
                                 MemberProtect.User.GetDataItem(currentRequest.MPUserID, "LastName"));
         }
         else
         {
            var usersNotes = MemberProtect.User.GetDataItem(currentRequest.MPUserID, "Notes");
            if (!string.IsNullOrWhiteSpace(usersNotes))
            {
               tag = string.Format("<a class=\"tooltip notes\" href=\"#\">Notes<span class=\"classic\">{0}</span></a> - ", usersNotes);
            }

            tag += string.Format("<a href=\"user-account.aspx?id={0}\">{1} {2}</a> | ", currentRequest.MPUserID,
                                 MemberProtect.User.GetDataItem(currentRequest.MPUserID, "FirstName"),
                                 MemberProtect.User.GetDataItem(currentRequest.MPUserID, "LastName"));
            tag += string.Format("<a href=\"mailto:{0}?subject=Your Speedy Spot Request {1}\" class=\"emaillink\">Email</a>",
                                 MemberProtect.User.GetUsername(currentRequest.MPUserID), currentRequest.RequestIdForDisplay);
         }

         return tag;
      }

      private string GetContactPhone()
      {
         var phone = currentRequest.ContactPhone;

         if (!string.IsNullOrWhiteSpace(currentRequest.ContactPhoneExtension))
         {
            phone += " x" + currentRequest.ContactPhoneExtension;
         }

         return phone;
      }

      private string GetCompany()
      {
         var tag = string.Empty;
         var companyName = MemberProtect.Organization.GetName(ApplicationContext.GetOrgID(currentRequest.MPUserID));

         if (!ApplicationContext.IsCustomer)
         {
            var companyNotes = MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(currentRequest.MPUserID), "Notes");
            if (!string.IsNullOrWhiteSpace(companyNotes))
            {
               tag = string.Format("<a href=\"#\" class=\"tooltip notes\">Notes<span class=\"classic\">{0}</span></a> - ", companyNotes);
            }

            var isVerified = (MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(currentRequest.MPUserID), "IsVerified") == "Y");
            var verfiedType = isVerified ? "verified" : "unverified";

            tag += string.Format("<span class=\"{0}\">", verfiedType);
            tag += string.Format("<a href=\"company-modify.aspx?id={0}\">{1}</a></span>", ApplicationContext.GetOrgID(currentRequest.MPUserID), companyName);
         }
         else
         {
            tag = companyName;
         }

         return tag;
      }
   }
}