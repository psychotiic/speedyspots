namespace SpeedySpots
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Web.UI.WebControls;
   using Business;
   using Business.Models;
   using Business.Services;
   using DataAccess;
   using InetSolution.Web;
   using Objects;
   using Telerik.Web.UI;

   public partial class create_estimate : SiteBasePage
   {
      private IARequest _iaRequest;

      protected void Page_Load(object sender, EventArgs e)
      {
         LoadRequest();

         if (IsPostBack) return;

         hlBreadcrumbRequest.NavigateUrl = string.Format(hlBreadcrumbRequest.NavigateUrl, RequestId);

         LoadEmailTemplates();

         LoadPaymentInformation();

         LoadExistingRequestEstimate();
      }

      private void LoadEmailTemplates()
      {
         m_txtRecipientEmail.Text = _iaRequest.MPUser.Username;
         m_txtSubject.Text = string.Format("Speedy Spots Estimate for Request {0}", IARequest.RequestIdForDisplay);

         m_cboEmailTemplate.DataValueField = "IAEmailTemplateID";
         m_cboEmailTemplate.DataTextField = "Name";
         m_cboEmailTemplate.DataSource = DataAccess.IAEmailTemplates;
         m_cboEmailTemplate.DataBind();

         m_cboEmailTemplate.Items.Insert(0, new ListItem(string.Empty, string.Empty));
         m_cboEmailTemplate.SelectedIndex = 0;
      }

      private void LoadPaymentInformation()
      {
         var cards = LoadPaymentSources();
         // need a guard here?
         PaymentSourceCombo.Items.AddRange(from card in cards select new RadComboBoxItem("Pay with " + card.Alias, card.CreditCardId.ToString()));

         if (_iaRequest.IACustomerCreditCardID < 1) return;
         m_radCharge.Items.FindByValue("PrePay").Selected = true;
         m_btnSubmit.Text = "Pay Now";
         paymentSourceGroup.Style["display"] = "block";

         PaymentSourceCombo.SelectedValue = _iaRequest.IACustomerCreditCardID.ToString();
         var selectedCard = cards.FirstOrDefault(c => c.CreditCardId == _iaRequest.IACustomerCreditCardID);
         if (selectedCard == null) return;
         var receiptList = selectedCard.ReceiptEmailAddressCsv.Split(',');
         if (receiptList.Any())
            
         for (var index = 0; index < receiptList.Count(); index++)
         {
            if (index == 0)
            {
               m_txtRecipientEmail.Text = receiptList[index];
            }
            else
            {
               m_txtRecipientCCEmails.Text += receiptList[index];
               if (index < receiptList.Count() - 1)
                  m_txtRecipientCCEmails.Text += ", ";
            }
         }
         paymentSourceNote.InnerText = string.Format("Customer requested credit card '{0}' be used.", selectedCard.Alias);
      }

      private void LoadExistingRequestEstimate()
      {
         if (!_iaRequest.IARequestEstimates.Any()) return;

         var requestEstimate = _iaRequest.IARequestEstimates[0];

         m_txtRecipientEmail.Text = requestEstimate.EmailRecipient;
         m_txtRecipientCCEmails.Text = requestEstimate.EmailCC;
         m_txtSubject.Text = requestEstimate.EmailSubject;
         m_txtBody.Content = requestEstimate.EmailBody;

         if (requestEstimate.Charge > 0)
         {
            m_txtCharge.Text = MemberProtect.Utility.FormatDecimal(requestEstimate.Charge);
         }

         if (requestEstimate.IsPaymentRequired && !requestEstimate.IsApproved)
         {
            m_radCharge.SelectedValue = "RequiresPayment";
         }
         else if (!requestEstimate.IsPaymentRequired && !requestEstimate.IsApproved)
         {
            m_radCharge.SelectedValue = "RequiresApproval";
         }
         else if (requestEstimate.PreAuthorizedPaymentCharged)
         {
            m_radCharge.SelectedValue = "PrePay";
            m_btnSubmit.Text = "Pay Now";
         }
         else
         {
            m_radCharge.SelectedValue = "JustInformation";
         }
      }

      private void LoadRequest()
      {
         if (RequestId > 0)
         {
            _iaRequest = DataAccess.IARequests.SingleOrDefault(row => row.IARequestID == RequestId);
         }

         if (_iaRequest == null)
         {
            RedirectMessage("~/staff-dashboard.aspx", "Unable to find request.");
         }
      }

      protected void OnChangedEmailTemplate(object sender, EventArgs e)
      {
         var emailTemplate = DataAccess.IAEmailTemplates
                                       .SingleOrDefault(row => row.IAEmailTemplateID == MemberProtect.Utility.ValidateInteger(m_cboEmailTemplate.SelectedValue));
         m_txtBody.Content = emailTemplate != null ? emailTemplate.Body : string.Empty;
      }

      protected void OnSendEstimate(object sender, EventArgs e)
      {
         var isNewRequestEstimate = true;
         var isPrePay = false;
         var requestEstimate = new IARequestEstimate();

         if (_iaRequest.IARequestEstimates.Any())
         {
            isNewRequestEstimate = false;
            requestEstimate = _iaRequest.IARequestEstimates[0];
         }
         else
         {
            requestEstimate.CreatedDateTime = DateTime.Now;
            DataAccess.IARequestEstimates.InsertOnSubmit(requestEstimate);
         }

         requestEstimate.IARequestID = _iaRequest.IARequestID;
         requestEstimate.MPUserID = MemberProtect.CurrentUser.UserID;
         requestEstimate.EmailRecipient = m_txtRecipientEmail.Text;
         requestEstimate.EmailCC = m_txtRecipientCCEmails.Text;
         requestEstimate.EmailSubject = m_txtSubject.Text;
         requestEstimate.EmailBody = m_txtBody.Content;
         switch (m_radCharge.SelectedValue)
         {
            case "RequiresPayment":
               requestEstimate.Charge = MemberProtect.Utility.ValidateDecimal(m_txtCharge.Text);
               requestEstimate.IsPaymentRequired = true;
               requestEstimate.IsApproved = false;
               requestEstimate.ApprovedDateTime = new DateTime(1900, 1, 1, 0, 0, 0, 0);
               _iaRequest.IARequestStatusID = ApplicationContext.GetRequestStatusID(RequestStatus.WaitingEstimateApproval);
               break;
            case "RequiresApproval":
               requestEstimate.Charge = 0;
               requestEstimate.IsPaymentRequired = false;
               requestEstimate.IsApproved = false;
               requestEstimate.ApprovedDateTime = new DateTime(1900, 1, 1, 0, 0, 0, 0);
               _iaRequest.IARequestStatusID = ApplicationContext.GetRequestStatusID(RequestStatus.WaitingEstimateApproval);
               break;
            case "PrePay":
               requestEstimate.Charge = MemberProtect.Utility.ValidateDecimal(m_txtCharge.Text);
               requestEstimate.IsPaymentRequired = false;
               requestEstimate.IsApproved = true;
               requestEstimate.ApprovedDateTime = DateTime.Now;
               _iaRequest.IARequestStatusID = ApplicationContext.GetRequestStatusID(RequestStatus.Submitted);

               var service = GetCardServiceForCustomer();
               var cardId = MemberProtect.Utility.ValidateInteger(PaymentSourceCombo.SelectedValue);
               var card = service.GetCreditCard(cardId);
               if (card == null)
               {
                  ShowMessage("Unable to access that credit card.", MessageTone.Negative);
                  return;
               }

               requestEstimate.PreAuthorizedPaymentCharged = true;
               var response = service.PayEstimate(card, ApplicationContext.RootUrlBuilder(this), _iaRequest, requestEstimate);
               isPrePay = response.SuccessfulCharge;

               break;
            default:
               requestEstimate.Charge = 0;
               requestEstimate.IsPaymentRequired = false;
               requestEstimate.IsApproved = true;
               requestEstimate.ApprovedDateTime = DateTime.Now;
               _iaRequest.IARequestStatusID = ApplicationContext.GetRequestStatusID(RequestStatus.Submitted);
               if (_iaRequest.IAJobs.Any())
               {
                  _iaRequest.IARequestStatusID = ApplicationContext.GetRequestStatusID(RequestStatus.Processing);
               }
               break;
         }

         ApplicationContext.RequestAssignStaff(_iaRequest, MemberProtect.CurrentUser.UserID);
         DataAccess.SubmitChanges();

         if (isPrePay)
         {
            RedirectMessage(string.Format("~/create-job.aspx?rid={0}", IARequest.IARequestID),
                            "The estimate has been charged to the customers credit card.",
                            MessageTone.Positive);
         }
         else
         {
            _iaRequest.CreateNote(MemberProtect.CurrentUser.UserID, isNewRequestEstimate ? "Sent estimate" : "Re-issued estimate");
            DataAccess.SubmitChanges();

            SendEstimateEmail();

            RedirectMessage(string.Format("~/create-job.aspx?rid={0}", IARequest.IARequestID),
                            "The estimate has been saved and sent to the customer.",
                            MessageTone.Positive);
         }
      }

      protected IARequest IARequest
      {
         get { return _iaRequest; }
      }

      private void SendEstimateEmail()
      {
         var stringBuilder = new StringBuilder();
         stringBuilder.Append(string.Format("<a href='{0}?rid={1}#estimate'>View your estimate here</a><br/><br/>",
                                            ApplicationContext.GetRootUrl(this, "order-details.aspx"),
                                            _iaRequest.IARequestID));
         stringBuilder.AppendLine(m_txtBody.Content);

         EmailCommunicationService.EstimateEmailSend(stringBuilder, m_txtSubject.Text, m_txtRecipientEmail.Text, m_txtRecipientCCEmails.Text.Split(',').ToList());
      }

      private IEnumerable<CreditCardViewModel> LoadPaymentSources()
      {
         var service = GetCardServiceForCustomer();
         var cards = service.GetCreditCards();

         return cards;
      }

      private CreditCardService GetCardServiceForCustomer()
      {
         return new CreditCardService(_iaRequest.MPUserID, MemberProtect, DataAccess, ApplicationContext.SiteProperites);
      }

      private int RequestId
      {
         get
         {
            var requestId = 0;

            if (Request.QueryString["rid"] != null)
            {
               requestId = MemberProtect.Utility.ValidateInteger(Request.QueryString["rid"]);
               Session["IARequestID"] = requestId;
            }

            return requestId;
         }
      }

      public override List<AccessControl> GetAccessControl()
      {
         return new List<AccessControl> {AccessControl.Admin, AccessControl.Staff};
      }
   }
}