namespace SpeedySpots
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Text.RegularExpressions;
   using System.Web.UI.HtmlControls;
   using System.Web.UI.WebControls;
   using Business;
   using Business.Models;
   using Business.Services;
   using DataAccess;
   using InetSolution.Web;
   using Objects;
   using Telerik.Web.UI;

   public partial class order_details : SiteBasePage
   {
      private const string TransientCardEntryKey = "transientCardEntryKey";

      private IARequest _workRequest;
      private bool _transientCardEntry;

      protected void Page_Load(object sender, EventArgs e)
      {
         if (RequestId > 0)
         {
            _workRequest = DataAccess.IARequests.SingleOrDefault(row => row.IARequestID == RequestId);
         }

         if (_workRequest != null)
         {
            // Validate the request can be viewed by the current user
            if (!ApplicationContext.CanCurrentUserViewOrder(_workRequest))
            {
               Response.Redirect("~/Default.aspx");
            }

            if (!_workRequest.IsLocked)
            {
               var clickSource = "m";
               if (Request.QueryString["s"] != null && Request.QueryString["s"].ToLower() == "a")
               {
                  clickSource = "a";
               }
               var redirectURL = string.Format("~/edit-request.aspx?s={0}&id={1}", clickSource, _workRequest.IARequestID);
               Response.Redirect(redirectURL);
            }

            if (string.Compare(PaymentSourceCombo.SelectedValue, TransientCardEntryKey, true) == 0)
               _transientCardEntry = true;

            if (!IsPostBack)
            {
               Page.Title = string.Format("Speedy Spots :: Request Details :: Request #{0}", IARequest.RequestIdForDisplay);
               SetBreadCrumb();

               m_divButtons.Visible = false;
               m_btnCancel.Visible = false;
               m_btnCancelRequest.Visible = false;
               if (ApplicationContext.CanRequestBeCanceled(_workRequest))
               {
                  m_divButtons.Visible = true;
                  m_btnCancel.Visible = true;
                  m_btnCancelRequest.Visible = true;
               }

               m_oRepeaterProduction.ItemCreated += OnRepeaterProductionItemCreated;
               m_oRepeaterProduction.DataSource = _workRequest.IARequestProductions;
               m_oRepeaterProduction.DataBind();

               m_btnPayment.Visible = false;
               m_btnApproveEstimate.Visible = false;

               if (IARequestEstimate != null)
               {
                  if (IARequestEstimate.IsPaymentRequired && !IARequestEstimate.IsApproved)
                  {
                     m_divPayment.Visible = true;
                     m_btnPayment.Visible = true;

                     if (IARequest.IAJobs.Any())
                     {
                        m_btnCancel.Visible = false;
                        m_btnCancelRequest.Visible = false;
                     }

                     LoadPaymentSources();

                     m_txtEmailReceipt.Text = MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(), "EmailInvoice");

                     ApplicationContext.LoadCreditCardTypes(ref m_cboCreditCardType);
                     ApplicationContext.LoadCreditCardExpirationMonths(ref m_cboCreditCardExpireMonth);
                     ApplicationContext.LoadCreditCardExpirationYears(ref m_cboCreditCardExpireYear);
                  }
                  else
                  {
                     m_divPayment.Visible = false;
                     m_btnPayment.Visible = false;

                     if (!IARequestEstimate.IsApproved)
                     {
                        m_btnApproveEstimate.Visible = true;
                     }
                  }
               }
               else
               {
                  m_divPayment.Visible = false;
               }

               if (!m_btnApproveEstimate.Visible && !m_btnCancelRequest.Visible && !m_btnPayment.Visible)
               {
                  m_divLowerButtons.Visible = false;
               }

               if (DataAccess.fn_Customer_GetInvoices(IARequest.IARequestID).Any())
               {
                  m_repeaterInvoices.DataSource = DataAccess.fn_Customer_GetInvoices(IARequest.IARequestID);
                  m_repeaterInvoices.DataBind();
               }
               else
               {
                  m_divInvoices.Visible = false;
               }
            }
         }
         else
         {
            Response.Redirect("~/Default.aspx");
         }
      }

      protected void PaymentSourceIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
      {
         ClientScript.RegisterStartupScript(GetType(), "hash", "location.hash = '#paymentsource';", true);

         switch (e.Value)
         {
            case "NoneSelected":
               OnetimePaymentSourcePanel.Visible = false;
               ExistingCardPaymentPanel.Visible = false;
               break;

            case TransientCardEntryKey:
               OnetimePaymentSourcePanel.Visible = true;
               ExistingCardPaymentPanel.Visible = false;
               break;

            default:
               OnetimePaymentSourcePanel.Visible = false;
               ExistingCardPaymentPanel.Visible = true;

               // if valid int
               var service = GetCardService();
               var cardId = MemberProtect.Utility.ValidateInteger(e.Value);
               var card = service.GetCreditCard(cardId);

               EmailReceiptTextBox.Text = card.ReceiptEmailAddressCsv;
               CardTypeLabel.Text = card.CardType;
               LastFourOfCardLabel.Text = card.LastFourOfCardNumber;
               MonthExpirationLabel.Text = card.ExpirationMonth;
               YearExpirationLabel.Text = card.ExpirationYear;
               FirstNameLabel.Text = card.FirstName;
               LastNameLabel.Text = card.LastName;
               break;
         }
      }

      private void LoadPaymentSources()
      {
         PaymentSourceCombo.Items.Add(new RadComboBoxItem("(please select)", "NoneSelected"));
         PaymentSourceCombo.SelectedValue = "NoneSelected";
         PaymentSourceCombo.Items.Add(new RadComboBoxItem("Enter new payment information", TransientCardEntryKey));

         var service = GetCardService();
         var cards = service.GetCreditCards();
         PaymentSourceCombo.Items.AddRange(from card in cards select new RadComboBoxItem("Pay with " + card.Alias, card.CreditCardId.ToString()));
      }

      private void SetBreadCrumb()
      {
         if (Request.QueryString["s"] != null && Request.QueryString["s"].ToLower() == "a")
         {
            hlDashboardBreadcrumb.NavigateUrl = "~/user-requests.aspx";
            hlDashboardBreadcrumb.Text = "All Requests";
         }
         else if (ApplicationContext.IsCustomer && _workRequest.MPUserID != MemberProtect.CurrentUser.UserID)
         {
            hlDashboardBreadcrumb.NavigateUrl = "~/user-requests.aspx";
            hlDashboardBreadcrumb.Text = "All Requests";
         }
         else
         {
            hlDashboardBreadcrumb.NavigateUrl = "~/user-dashboard.aspx";
         }
      }

      protected void OnRequestRecut(object sender, EventArgs e)
      {
         var btnRequestRecut = sender as LinkButton;

         foreach (RepeaterItem oItem in m_oRepeaterProduction.Items)
         {
            var oRequestRecut = oItem.FindControl("m_btnTriggerRecut") as LinkButton;
            if (oRequestRecut != btnRequestRecut) continue;

            var divRequestRecut = oItem.FindControl("m_divRequestRecut") as HtmlGenericControl;
            var divRecut = oItem.FindControl("m_divRecut") as HtmlGenericControl;

            if (divRequestRecut != null) divRequestRecut.Visible = false;
            if (divRecut != null) divRecut.Visible = true;
         }
      }

      protected void OnRecut(object sender, EventArgs e)
      {
         var btnRecut = sender as LinkButton;

         foreach (RepeaterItem oItem in m_oRepeaterProduction.Items)
         {
            var oRecut = oItem.FindControl("m_btnRecut") as LinkButton;
            if (oRecut != btnRecut) continue;

            var txtDescription = oItem.FindControl("m_txtDescription") as RadTextBox;

            var oIARequestProduction =
               DataAccess.IARequestProductions.SingleOrDefault(
                  row => row.IARequestProductionID == MemberProtect.Utility.ValidateInteger(btnRecut.CommandArgument));
            if (oIARequestProduction == null) continue;

            if (ApplicationContext.Recut(oIARequestProduction, txtDescription.Text))
            {
               ShowMessage("Re-Cut submitted.", MessageTone.Positive);
            }
         }
      }

      protected void OnRecutCancel(object sender, EventArgs e)
      {
         var btnRecutCancel = sender as LinkButton;
         foreach (RepeaterItem oItem in m_oRepeaterProduction.Items)
         {
            var oRecut = oItem.FindControl("m_btnRecutCancel") as LinkButton;
            if (oRecut != btnRecutCancel) continue;

            var divRequestRecut = oItem.FindControl("m_divRequestRecut") as HtmlGenericControl;
            var divRecut = oItem.FindControl("m_divRecut") as HtmlGenericControl;

            divRequestRecut.Visible = true;
            divRecut.Visible = false;
         }
      }

      protected void OnPayment(object sender, EventArgs e)
      {
         if (!Page.IsValid) return;

         var service = GetCardService();
         var cardProfile = SetCreditCardViewModel(service);

         var cardExpiresOnDate =
            new DateTime(MemberProtect.Utility.ValidateInteger(cardProfile.ExpirationYear),
                         MemberProtect.Utility.ValidateInteger(cardProfile.ExpirationMonth), 1)
               .AddMonths(1).AddDays(-1);

         if (cardExpiresOnDate.CompareTo(DateTime.Today) <= 0)
         {
            SetMessage(
               _transientCardEntry
                  ? "Please choose a valid credit card expiration date in the future."
                  : "This card is expired please choose or add another card.",
               MessageTone.Negative);
            return;
         }

         if (_transientCardEntry)
         {
            var sErrors = string.Empty;
            // MD: need to de-dupe this stuff
            if (cardProfile.CardType == "AmericanExpress")
            {
               var oRegex = new Regex("^\\d{15}$");
               if (!oRegex.Match(cardProfile.CreditCardNumber).Success)
               {
                  sErrors += "Credit Card Number must be a valid American Express number (XXXX-XXXX-XXXX-XXX).<br/>";
               }

               if (cardProfile.CardVerificationCode.Length != 4)
               {
                  sErrors += "Please enter your 4 digit security code.<br/>";
               }
            }
            else
            {
               var oRegex = new Regex("^\\d{16}$");
               if (!oRegex.Match(cardProfile.CreditCardNumber).Success)
               {
                  sErrors += "Credit Card Number must be a valid credit card number (XXXX-XXXX-XXXX-XXXX).<br/>";
               }

               if (cardProfile.CardVerificationCode.Length != 3)
               {
                  sErrors += "Please enter your 3 digit security code.<br/>";
               }
            }

            if (m_chkAgree.Checked == false)
            {
               sErrors += "You must accept the Terms & Conditions.";
            }

            if (sErrors != string.Empty)
            {
               SetMessage(sErrors, MessageTone.Negative);
               return;
            }
         }

         if (_transientCardEntry && m_chkSavePaymentInformation.Checked)
         {
            var errors = service.SaveCreditCard(cardProfile);
            if (errors.Any())
            {
               SetMessage(errors.Aggregate((msg, error) => msg + error + "<br />"), MessageTone.Negative);
               return;
            }
            // just flip the bit?  hope the state is all proper
            _transientCardEntry = false;
         }

         var response = service.PayEstimate(cardProfile, ApplicationContext.RootUrlBuilder(this), _workRequest, IARequestEstimate);

         if (response.SuccessfulCharge)
         {
            ShowMessage("Your request estimate is now approved.", MessageTone.Positive);
         }
         else
         {
            ShowMessage(response.ErrorMessage, MessageTone.Negative);
         }
      }

      private CreditCardViewModel SetCreditCardViewModel(CreditCardService service)
      {
         if (!_transientCardEntry)
         {
            var vm = service.GetCreditCard(int.Parse(PaymentSourceCombo.SelectedValue));
            vm.ReceiptEmailAddressCsv = EmailReceiptTextBox.Text;
            return vm;
         }

         var cardProfile = service.CreateNew();
         cardProfile.CreditCardNumber = m_txtCreditCardNumber.Text;
         cardProfile.CardVerificationCode = m_txtCreditCardSecurityCode.Text;
         cardProfile.ExpirationMonth = m_cboCreditCardExpireMonth.SelectedValue;
         cardProfile.ExpirationYear = m_cboCreditCardExpireYear.SelectedValue;
         cardProfile.ReceiptEmailAddressCsv = m_txtEmailReceipt.Text;
         cardProfile.CardType = m_cboCreditCardType.SelectedValue;
         cardProfile.FirstName = m_txtCreditCardFirstName.Text;
         cardProfile.LastName = m_txtCreditCardLastName.Text;
         cardProfile.CompanyName = CompanyName.Text;
         return cardProfile;
      }

      protected void OnCancel(object sender, EventArgs e)
      {
         _workRequest.IARequestStatusID = ApplicationContext.GetRequestStatusID(RequestStatus.Canceled);
         _workRequest.CreateNote(MemberProtect.CurrentUser.UserID, "Request canceled by customer");
         DataAccess.SubmitChanges();

         var orgID = ApplicationContext.GetOrgID(MemberProtect.CurrentUser.UserID);
         EmailCommunicationService.UserCancelRequestNoticeSend(_workRequest.IARequestID, _workRequest.RequestIdForDisplay, orgID);

         ShowMessage("Request has been canceled.", MessageTone.Positive);
      }

      protected void OnApproveEstimate(object sender, EventArgs e)
      {
         _workRequest.IARequestStatusID = ApplicationContext.GetRequestStatusID(RequestStatus.Processing);

         var oIARequestEstimate = _workRequest.IARequestEstimates.SingleOrDefault();
         if (oIARequestEstimate != null)
         {
            oIARequestEstimate.IsApproved = true;
            oIARequestEstimate.ApprovedDateTime = DateTime.Now;
         }
         IARequest.CreateNote(MemberProtect.CurrentUser.UserID, "Estimate approved");
         DataAccess.SubmitChanges();

         // Send System Notification
         var sSubject = string.Format("Estimate approval for Request #{0}", _workRequest.RequestIdForDisplay);

         var oBody = new StringBuilder();
         oBody.Append(string.Format("{0} {1} of {2} has approved the estimate for request #{3}.", MemberProtect.CurrentUser.GetDataItem("FirstName"),
                                    MemberProtect.CurrentUser.GetDataItem("LastName"),
                                    MemberProtect.Organization.GetName(ApplicationContext.GetOrgID()), _workRequest.RequestIdForDisplay));
         oBody.Append("</br>");
         oBody.Append(string.Format("You can <a href='{0}?rid={1}'>view the request here</a>.</br>",
                                    ApplicationContext.GetRootUrl(this, "create-job.aspx"), _workRequest.IARequestID));

         EmailCommunicationService.SystemNotificationSend(sSubject, oBody.ToString());

         ShowMessage("Estimate has been approved.", MessageTone.Positive);
      }

      #region Repeaters

      private void OnRepeaterProductionItemCreated(object sender, RepeaterItemEventArgs e)
      {
         var oIARequestProduction = ((IARequestProduction) e.Item.DataItem);

         if (oIARequestProduction.IARequestProductionFiles.Any())
         {
            var oRepeaterProductionFiles = e.Item.FindControl("m_oRepeaterProductionFiles") as Repeater;
            oRepeaterProductionFiles.DataSource = oIARequestProduction.IARequestProductionFiles;
            oRepeaterProductionFiles.DataBind();
         }

         var divRequestRecut = e.Item.FindControl("m_divRequestRecut") as HtmlGenericControl;
         var divRecut = e.Item.FindControl("m_divRecut") as HtmlGenericControl;
         var divRecutReceipt = e.Item.FindControl("m_divRecutReceipt") as HtmlGenericControl;

         divRequestRecut.Visible = false;
         divRecut.Visible = false;
         divRecutReceipt.Visible = false;

         if (oIARequestProduction.HasRecutBeenRequested)
         {
            divRecutReceipt.Visible = true;
            divRecutReceipt.InnerHtml = string.Format("<p>Re-Cut Requested on {0:M/dd/yyyy a\t h:mm tt}.</p><p>{1}</p>",
                                                      oIARequestProduction.RecutRequestDateTime, oIARequestProduction.RecutNotes);
         }
         else
         {
            var oLastDateToRequestRecut = oIARequestProduction.IAJob.CompletedDateTime.AddDays(ApplicationContext.SiteProperites.RecutRequestThreshold);

            if (ApplicationContext.SiteProperites.RecutRequestThreshold == 0 || oLastDateToRequestRecut.CompareTo(DateTime.Now) > 0)
            {
               divRequestRecut.Visible = true;
            }
         }

         var btnRecut = e.Item.FindControl("m_btnRecut") as LinkButton;
         btnRecut.CommandArgument = MemberProtect.Utility.FormatInteger(oIARequestProduction.IARequestProductionID);
      }

      protected void OnInvoicesItemDataBound(object sender, RepeaterItemEventArgs e)
      {
         if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
         {
            var invoice = (fn_Customer_GetInvoicesResult) e.Item.DataItem;
            if (!invoice.Paid.Value)
            {
               var hlPayNow = (HyperLink) e.Item.FindControl("hlPayNow");
               hlPayNow.Visible = true;
               hlPayNow.NavigateUrl = string.Format(hlPayNow.NavigateUrl, invoice.InvoiceNumber);
            }
         }
      }

      #endregion

      #region Public Methods

      public string DisplayProgressBar()
      {
         var bIsPayFirst =
            MemberProtect.Utility.YesNoToBool(
               MemberProtect.Organization.GetDataItem(
                  ApplicationContext.GetOrgID(MemberProtect.CurrentUser.UserID),
                  "IsPayFirst"));

         var bSubmitted = false;
         var bEstimateApproval = false;
         var bProcessing = false;
         var bProduction = false;
         var bComplete = false;

         if (_workRequest.IARequestStatusID != ApplicationContext.GetRequestStatusID(RequestStatus.Canceled))
         {
            // Submitted
            if ((_workRequest.IARequestStatusID == ApplicationContext.GetRequestStatusID(RequestStatus.Submitted)) ||
                (!_workRequest.IARequestEstimates.Any() && !_workRequest.IAJobs.Any()))
            {
               bSubmitted = true;
            }

            // Estimate Approval
            if (_workRequest.IARequestEstimates.Any())
            {
               if (!_workRequest.IARequestEstimates[0].IsApproved)
               {
                  bEstimateApproval = true;
               }
            }

            if (_workRequest.IARequestStatusID == ApplicationContext.GetRequestStatusID(RequestStatus.WaitingEstimateApproval))
            {
               bEstimateApproval = true;
            }

            var bEstimateApproved = false;
            if (_workRequest.IARequestEstimates.Any())
            {
               if (_workRequest.IARequestEstimates[0].IsApproved)
               {
                  bEstimateApproved = true;
               }
            }

            // Processing
            if (bEstimateApproved || _workRequest.IARequestStatusID == ApplicationContext.GetRequestStatusID(RequestStatus.Processing))
            {
               bProcessing = true;
            }

            // Production
            if (_workRequest.IARequestStatusID == ApplicationContext.GetRequestStatusID(RequestStatus.InProduction))
            {
               bProduction = true;
            }

            // Complete
            if (_workRequest.IAJobs.Any() &&
                _workRequest.IAJobs.Count() ==
                _workRequest.IAJobs.Count(row => row.IAJobStatusID == ApplicationContext.GetJobStatusID(JobStatus.Complete)))
            {
               bComplete = true;
            }
         }

         var oSB = new StringBuilder();
         oSB.Append("<ul class='checkout group'>");

         if (bSubmitted && !bEstimateApproval && !bProcessing && !bProduction && !bComplete)
         {
            oSB.Append(
               "<li class='first at'><span class=\"step\"><a href=\"#\" class=\"tooltip\">Submitted<span class=\"classic\">Your request has been submitted. A producer will contact you if there are any questions while processing. You’ll be notified if an estimate or prepayment is required.</span></a></span></li>");
         }
         else
         {
            oSB.Append(
               "<li class='first'><span class=\"step\"><a href=\"#\" class=\"tooltip\">Submitted<span class=\"classic\">Your request has been submitted. A producer will contact you if there are any questions while processing. You’ll be notified if an estimate or prepayment is required.</span></a></span></li>");
         }

         if (_workRequest.IARequestEstimates.Any() || bIsPayFirst ||
             _workRequest.IARequestStatusID == ApplicationContext.GetRequestStatusID(RequestStatus.NeedsEstimate) ||
             _workRequest.IARequestStatusID == ApplicationContext.GetRequestStatusID(RequestStatus.WaitingEstimateApproval))
         {
            if (bEstimateApproval && !bProcessing && !bProduction && !bComplete)
            {
               oSB.Append(
                  "<li class='at'><span class=\"step\"><a href=\"#\" class=\"tooltip\">Estimate Approval<span class=\"classic\">An estimate has been sent, please approve to continue. You may be required to prepay, see below.</span></a></span></li>");
            }
            else
            {
               oSB.Append(
                  "<li><span class=\"step\"><a href=\"#\" class=\"tooltip\">Estimate Approval<span class=\"classic\">An estimate has been sent, please approve to continue. You may be required to prepay, see below.</span></a></span></li>");
            }
         }

         if (bProcessing && !bProduction && !bComplete)
         {
            oSB.Append(
               "<li class='at'><span class=\"step\"><a href=\"#\" class=\"tooltip\">Processing<span class=\"classic\">Your request is being prepared by our producers. We’ll contact you with any questions.</span></a></span></li>");
         }
         else
         {
            oSB.Append(
               "<li><span class=\"step\"><a href=\"#\" class=\"tooltip\">Processing<span class=\"classic\">Your request is being prepared by our producers. We’ll contact you with any questions.</span></a></span></li>");
         }

         if (bProduction && !bComplete)
         {
            oSB.Append(
               "<li class='at'><span class=\"step\"><a href=\"#\" class=\"tooltip\">Production<span class=\"classic\">Our talent is recording. Our producers will then check it for quality and apply any requested production.</span></a></span></li>");
         }
         else
         {
            oSB.Append(
               "<li><span class=\"step\"><a href=\"#\" class=\"tooltip\">Production<span class=\"classic\">Our talent is recording. Our producers will then check it for quality and apply any requested production.</span></a></span></li>");
         }

         if (bComplete)
         {
            oSB.Append(
               "<li class='last at'><span class=\"step\"><a href=\"#\" class=\"tooltip\">Complete<span class=\"classic\">Your request is complete and ready to download, see link below.</span></a></span></li>");
         }
         else
         {
            oSB.Append(
               "<li class='last'><span class=\"step\"><a href=\"#\" class=\"tooltip\">Complete<span class=\"classic\">Your request is complete and ready to download, see link below.</span></a></span></li>");
         }
         oSB.Append("</ul>");

         return oSB.ToString();
      }

      protected string DisplaySpecialNotes()
      {
         var sLabels = string.Empty;
         foreach (var oResults in DataAccess.fn_Customer_GetRequestLabels(IARequest.IARequestID))
         {
            sLabels += string.Format("<div class='label'><span>{0}</span></div>", oResults.Text);
         }

         if (sLabels.Length > 0)
         {
            sLabels = string.Format("<div class='label-bar group'><div class='llabel'>Special Notes:</div>{0}</div>", sLabels);
         }

         return sLabels;
      }

      #endregion

      #region Public Properties

      protected IARequest IARequest
      {
         get { return _workRequest; }
      }

      private int RequestId
      {
         get
         {
            var requestId = 0;

            if (Request.QueryString["rid"] != null)
            {
               requestId = MemberProtect.Utility.ValidateInteger(Request.QueryString["rid"]);
            }

            if (requestId == 0 && Request.QueryString["id"] != null)
            {
               requestId = MemberProtect.Utility.ValidateInteger(Request.QueryString["id"]);
            }

            Session["IARequestID"] = requestId;

            return requestId;
         }
      }

      protected IARequestEstimate IARequestEstimate
      {
         get { return DataAccess.IARequestEstimates.FirstOrDefault(row => row.IARequestID == _workRequest.IARequestID); }
      }

      protected string CancelText
      {
         get
         {
            var bShowCancelText = false;

            // Rules to show cancel text:
            // 1. (Request status is 'Submitted' OR 'Needs Estimate' OR 'Waiting Estimate Approval') AND (jobs > 1)
            // 2. Request status is 'Processing' OR 'InProduction'

            // 1. (Request status is 'Submitted' OR 'Needs Estimate' OR 'Waiting Estimate Approval') AND (jobs > 1)
            if ((_workRequest.IARequestStatusID == ApplicationContext.GetRequestStatusID(RequestStatus.Submitted) ||
                 _workRequest.IARequestStatusID == ApplicationContext.GetRequestStatusID(RequestStatus.NeedsEstimate) ||
                 _workRequest.IARequestStatusID == ApplicationContext.GetRequestStatusID(RequestStatus.WaitingEstimateApproval)) &&
                _workRequest.IAJobs.Count() > 0)
            {
               bShowCancelText = true;
            }
               // 2. Request status is 'Processing' OR 'InProduction'
            else if (_workRequest.IARequestStatusID == ApplicationContext.GetRequestStatusID(RequestStatus.Processing) ||
                     _workRequest.IARequestStatusID == ApplicationContext.GetRequestStatusID(RequestStatus.InProduction))
            {
               bShowCancelText = true;
            }

            return bShowCancelText
                      ? "<p>A producer has already started working on your request. If you wish to cancel your request, please <a href='http://www.speedyspots.com/pages/contact_us.aspx'>contact us</a>.</p>"
                      : string.Empty;
         }
      }

      protected string ExpectedDelivery
      {
         get
         {
            if (IARequest.IARequestStatusID == ApplicationContext.GetRequestStatusID(RequestStatus.Completed))
            {
               return "Delivered";
            }
            if (IARequest.IAJobs.Count == 0)
            {
               return "To Be Determined";
            }
            return string.Format("{0:ddd, MMMM dd, yyyy a\\t h:mm tt}",
                                 IARequest.IAJobs.OrderByDescending(row => row.DueDateTime).First().DueDateTime);
         }
      }

      #endregion

      public override bool GetSSL()
      {
         return true;
      }

      public override List<AccessControl> GetAccessControl()
      {
         return new List<AccessControl> {AccessControl.Customer, AccessControl.Admin};
      }

      private CreditCardService GetCardService()
      {
         return new CreditCardService(MemberProtect.CurrentUser.UserID, MemberProtect, DataAccess, ApplicationContext.SiteProperites);
      }
   }
}