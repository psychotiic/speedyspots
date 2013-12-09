namespace SpeedySpots.payments
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Text.RegularExpressions;
   using System.Web.UI.WebControls;
   using Business.Helpers;
   using Business.Models;
   using Business.Services;
   using DataAccess;
   using InetSolution.Web;
   using InetSolution.Web.API.Payment.AuthorizeNET;
   using Objects;
   using Telerik.Web.UI;

   public partial class _default : SiteBasePage
   {
      private const string TransientCardEntryKey = "transientCardEntryKey";

      private List<InvoicePayment> _invoicePayments = new List<InvoicePayment>();
      private bool _transientCardEntry;

      protected CreditCardViewModel CardProfile;

      protected void Page_Load(object sender, EventArgs e)
      {
         LoadPayments();
         SavePayments();

         var authorized = MemberProtect.CurrentUser.IsAuthorized;

         if (string.Compare(PaymentSourceCombo.SelectedValue, TransientCardEntryKey, true) == 0 || !authorized)
            _transientCardEntry = true;

         if (IsPostBack) return;

         PaymentSourcePanel.Visible = (authorized && ApplicationContext.IsCustomer);
         OnetimePaymentSourcePanel.Visible = (!authorized);
         YouCanSaveCardsDiv.Visible = (!authorized);

         if (authorized && ApplicationContext.IsCustomer)
         {
            m_txtCompany.Text = MemberProtect.Organization.GetName(ApplicationContext.GetOrgID(MemberProtect.CurrentUser.UserID));
            m_txtCompany.Enabled = false;
            m_txtCompany.Font.Bold = true;
         }

         Session["InvoicePayments"] = null;

         LoadPayments();
         SavePayments();

         m_divReceipt.Visible = false;

         LoadPaymentSources((authorized && ApplicationContext.IsCustomer));

         ApplicationContext.LoadCreditCardTypes(ref m_cboCreditCardType);
         ApplicationContext.LoadCreditCardExpirationMonths(ref m_cboCreditCardExpireMonth);
         ApplicationContext.LoadCreditCardExpirationYears(ref m_cboCreditCardExpireYear);
      }

      private void LoadPaymentSources(bool loadPaymentSources)
      {
         if (!loadPaymentSources) return;

         PaymentSourceCombo.Items.Add(new RadComboBoxItem("(please select)", "NoneSelected"));
         PaymentSourceCombo.SelectedValue = "NoneSelected";
         PaymentSourceCombo.Items.Add(new RadComboBoxItem("Enter new payment information", TransientCardEntryKey));

         var service = GetCardService();
         var cards = service.GetCreditCards();
         PaymentSourceCombo.Items.AddRange(from card in cards select new RadComboBoxItem("Pay with " + card.Alias, card.CreditCardId.ToString()));
      }

      protected void OnAddInvoice(object sender, EventArgs e)
      {
         CreateInvoice();
         SavePayments();
      }

      protected void OnRemoveInvoice(object sender, EventArgs e)
      {
         var oButton = (LinkButton) sender;

         var oInvoicePayment = _invoicePayments.SingleOrDefault(row => row.Id == MemberProtect.Utility.ValidateGuid(oButton.CommandArgument));
         if (oInvoicePayment == null) return;

         _invoicePayments.Remove(oInvoicePayment);
         SavePayments();
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
               if (string.IsNullOrEmpty(m_txtEmail.Text))
                  m_txtEmail.Text = MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(), "EmailInvoice");
               break;

            default:
               OnetimePaymentSourcePanel.Visible = false;
               ExistingCardPaymentPanel.Visible = true;

               // if valid int
               var cardId = MemberProtect.Utility.ValidateInteger(e.Value);
               var service = GetCardService();
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

      protected void OnPay(object sender, EventArgs e)
      {
         if (!Page.IsValid) return;

         var service = GetCardService();
         CardProfile = SetCreditCardViewModel(service);

         var cardExpiresOnDate =
            new DateTime(MemberProtect.Utility.ValidateInteger(CardProfile.ExpirationYear),
                         MemberProtect.Utility.ValidateInteger(CardProfile.ExpirationMonth), 1)
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

         var sErrors = string.Empty;

         if (_transientCardEntry)
         {
            if (CardProfile.CardType == "American Express")
            {
               var oRegex = new Regex("^\\d{15}$");
               if (!oRegex.Match(CardProfile.CreditCardNumber).Success)
               {
                  sErrors += "Credit Card Number must be a valid American Express number (123123412341234).<br/>";
               }

               if (CardProfile.CardVerificationCode.Length != 4)
               {
                  sErrors += "Please enter your 4 digit security code.<br/>";
               }
            }
            else
            {
               var oRegex = new Regex("^\\d{16}$");
               if (!oRegex.Match(CardProfile.CreditCardNumber).Success)
               {
                  sErrors += "Credit Card Number must be a valid credit card number (1234123412341234).<br/>";
               }

               if (CardProfile.CardVerificationCode.Length != 3)
               {
                  sErrors += "Please enter your 3 digit security code.<br/>";
               }
            }

            if (!m_chkAgree.Checked)
            {
               sErrors += "You must accept the Terms & Conditions.";
            }

            if (sErrors != string.Empty)
            {
               SetMessage(sErrors, MessageTone.Negative);
               return;
            }
         }

         var oExpirationDateTime =
            new DateTime(MemberProtect.Utility.ValidateInteger(m_cboCreditCardExpireYear.SelectedValue),
                         MemberProtect.Utility.ValidateInteger(m_cboCreditCardExpireMonth.SelectedValue), 1).AddMonths(1).AddDays(-1);

         var oTransactionInformation = new AuthorizeNETTransactionInformation(CardProfile.CreditCardNumber, GetTotal(), oExpirationDateTime)
         {
            FirstName = CardProfile.FirstName,
            LastName = CardProfile.LastName,
            CreditCardCode = CardProfile.CardVerificationCode,
            Email = CardProfile.ReceiptEmailAddressCsv,
            Description = "Invoice Payment",
            Company = (m_txtCompany.Text.Length > 50) ? m_txtCompany.Text.Substring(0, 49) : m_txtCompany.Text,
            InvoiceNumber = GetListOfInvoiceNumbers()
         };

         if (_transientCardEntry && m_chkSavePaymentInformation.Checked)
         {
            var errors = service.SaveCreditCard(CardProfile);
            if (errors.Any())
            {
               SetMessage(errors.Aggregate((msg, error) => msg + error + "<br />"), MessageTone.Negative);
               return;
            }

            // just flip the bit?  hope the state is all proper
            _transientCardEntry = false;
         }

         var paymentGatewayResponse = new PaymentGatewayResponse(string.Empty);

         if (_transientCardEntry)
         {
            var oAuthorizeNet = new AuthorizeNET(ApplicationContext.SiteProperites.AuthorizeNetIsDebug,
                                                 MemberProtect.Cryptography.Decrypt(ApplicationContext.SiteProperites.AuthorizeNetLoginID),
                                                 MemberProtect.Cryptography.Decrypt(ApplicationContext.SiteProperites.AuthorizeNetTransactionKey));

            if (!oAuthorizeNet.ProcessAuthorizationAndCapture(oTransactionInformation))
            {
               var message = "Your payment could not be processed, please double-check all information and try again.";
               if (ApplicationContext.SiteProperites.AuthorizeNetIsDebug)
               {
                  var response = CreditCards.ParseAuthorizeNetResponse(oAuthorizeNet.Response);
                  message += string.Format(" (Response: {0}, Reason: {1})", response.ResponseCode, response.ResponseReasonCode);
               }

               SetMessage(message, MessageTone.Negative);
               return;
            }

            paymentGatewayResponse = oAuthorizeNet.PaymentGatewayResponse;
         }
         else
         {
            // go through CIM to process transaction
            paymentGatewayResponse = service.CIMAuthorizationAndCapture(oTransactionInformation,
                                                                        CardProfile.CIMProfileId,
                                                                        CardProfile.CIMPaymentProfileId);
            if (paymentGatewayResponse.Errors.Any() || paymentGatewayResponse.GetValue(PaymentGatewayResponse.FieldNames.ResponseCode) != "1")
            {
               var message = paymentGatewayResponse.Errors.Any()
                                ? paymentGatewayResponse.Errors.Aggregate((messages, error) => messages + ", " + error)
                                : paymentGatewayResponse.GetValue(PaymentGatewayResponse.FieldNames.ResponseReasonText);
               ShowMessage(message, MessageTone.Negative);
               return;
            }
         }

         var oIAPayment = SavePayment(paymentGatewayResponse);

         StringBuilder oSpeedySpotsLineItems;
         var oLineItems = SaveLineItems(oIAPayment, out oSpeedySpotsLineItems);

         // QB Import Payment Rule
         // - All of the line items within a payment MUST associate with an actual QB Invoice and the amount on the individual line items must match up with the invoice, otherwise
         var bIsPaymentAutoImported = QuickBooksInvoiceMatching(oIAPayment);

         // Clear the invoice in memory
         Session["InvoicePayments"] = null;

         SendPaymentReceiptEmail(CardProfile, oLineItems, oIAPayment);

         SendBillingEmail(CardProfile, bIsPaymentAutoImported, oSpeedySpotsLineItems, oIAPayment);

         // Clear the invoice in memory
         Session["InvoicePayments"] = null;

         // Display the receipt
         m_divPayment.Visible = false;
         m_divReceipt.Visible = true;
      }

      private IAPayment SavePayment(PaymentGatewayResponse paymentGatewayResponse)
      {
         var oIAPayment = new IAPayment
         {
            Total = GetTotal(),
            Email = CardProfile.ReceiptEmailAddressCsv,
            Company = m_txtCompany.Text,
            Instructions = m_txtInstructions.Text,
            CreditCardFirstName = MemberProtect.Cryptography.Encrypt(CardProfile.FirstName),
            CreditCardLastName = MemberProtect.Cryptography.Encrypt(CardProfile.LastName),
            CreditCardType = MemberProtect.Cryptography.Encrypt(CardProfile.CardType),
            CreditCardNumber = MemberProtect.Cryptography.Encrypt(CardProfile.CreditCardNumber),
            AuthorizeNetTransactionID = paymentGatewayResponse.GetValue(PaymentGatewayResponse.FieldNames.TransactionId),
            AuthorizeNetProcessResponse = paymentGatewayResponse.GetValue(PaymentGatewayResponse.FieldNames.ResponseReasonText),
            CreatedDateTime = DateTime.Now,
            Zip = ""
         };
         DataAccess.IAPayments.InsertOnSubmit(oIAPayment);
         DataAccess.SubmitChanges();
         return oIAPayment;
      }

      private StringBuilder SaveLineItems(IAPayment oIAPayment, out StringBuilder oSpeedySpotsLineItems)
      {
         // Save the individual line items
         var oLineItems = new StringBuilder();
         oSpeedySpotsLineItems = new StringBuilder();
         foreach (RepeaterItem oItem in m_oRepeaterInvoices.Items)
         {
            var m_txtNumber = oItem.FindControl("m_txtNumber") as TextBox;
            var m_txtAmount = oItem.FindControl("m_txtAmount") as RadNumericTextBox;

            // Check to see if any payment has been made in the past against the given invoice
            var iPreviousPayments = 0;
            if (m_txtNumber.Text != string.Empty)
            {
               iPreviousPayments = DataAccess.IAPaymentLineItems.Count(row => row.Invoice == m_txtNumber.Text);
            }

            var oIAPaymentLineItem = new IAPaymentLineItem
            {
               IAPaymentID = oIAPayment.IAPaymentID,
               Invoice = iPreviousPayments == 0 ? m_txtNumber.Text : string.Empty,
               Amount = MemberProtect.Utility.ValidateDecimal(m_txtAmount.Text)
            };

            var oQBInvoice = DataAccess.QBInvoices.SingleOrDefault(row => row.InvoiceNumber == m_txtNumber.Text);
            if (oQBInvoice != null && iPreviousPayments == 0)
            {
               oIAPaymentLineItem.QBInvoiceID = oQBInvoice.QBInvoiceID;
            }
            else
            {
               oIAPaymentLineItem.QBInvoiceID = 0;
            }

            oIAPayment.IAPaymentLineItems.Insert(0, oIAPaymentLineItem);
            DataAccess.SubmitChanges();

            if (m_txtNumber.Text != string.Empty)
            {
               oLineItems.AppendLine(string.Format("Invoice# {0}: {1:c}", m_txtNumber.Text, MemberProtect.Utility.ValidateDecimal(m_txtAmount.Text)));
            }
            else
            {
               oLineItems.AppendLine(string.Format("{0:c}", MemberProtect.Utility.ValidateDecimal(m_txtAmount.Text)));
            }

            if (oIAPaymentLineItem.QBInvoiceID == 0)
            {
               var sInvoiceNumber = (string.IsNullOrEmpty(m_txtNumber.Text) ? "# N/A" : m_txtNumber.Text);
               if (iPreviousPayments > 0)
               {
                  oSpeedySpotsLineItems.AppendLine(string.Format("{0}: {1:c} (additional attempt to pay invoice #{2})", sInvoiceNumber,
                                                                 MemberProtect.Utility.ValidateDecimal(m_txtAmount.Text), m_txtNumber.Text));
               }
               else
               {
                  oSpeedySpotsLineItems.AppendLine(string.Format("{0}: {1:c}", sInvoiceNumber,
                                                                 MemberProtect.Utility.ValidateDecimal(m_txtAmount.Text)));
               }
            }
            else
            {
               oSpeedySpotsLineItems.AppendLine(string.Format("{0}: {1:c}", oIAPaymentLineItem.Invoice,
                                                              MemberProtect.Utility.ValidateDecimal(m_txtAmount.Text)));
            }
         }
         return oLineItems;
      }

      private bool QuickBooksInvoiceMatching(IAPayment oIAPayment)
      {
         // the payment doesn't get automatically imported into Quickbooks via the Connector.
         var bIsPaymentAutoImported = true;
         var oMPOrgID = Guid.Empty;
         foreach (var oIALineItem in oIAPayment.IAPaymentLineItems)
         {
            var oQBInvoice = DataAccess.QBInvoices.SingleOrDefault(row => row.InvoiceNumber == oIALineItem.Invoice);
            if (oQBInvoice != null)
            {
               if (oMPOrgID == Guid.Empty)
               {
                  oMPOrgID = ApplicationContext.GetOrgID(oQBInvoice.IAJob.IARequest.MPUserID);
               }
               else if (oMPOrgID != ApplicationContext.GetOrgID(oQBInvoice.IAJob.IARequest.MPUserID))
               {
                  bIsPaymentAutoImported = false;
                  break;
               }

               if (oQBInvoice.Amount != oIALineItem.Amount)
               {
                  bIsPaymentAutoImported = false;
                  break;
               }
            }
            else
            {
               bIsPaymentAutoImported = false;
               break;
            }
         }

         if (!bIsPaymentAutoImported)
         {
            foreach (var oIALineItem in oIAPayment.IAPaymentLineItems)
            {
               oIALineItem.QBInvoiceID = 0;
            }

            DataAccess.SubmitChanges();
         }
         return bIsPaymentAutoImported;
      }

      private void SendPaymentReceiptEmail(CreditCardViewModel viewModel, StringBuilder oLineItems, IAPayment oIAPayment)
      {
         // Email receipt to payer
         var sGreeting = string.Empty;
         if (_transientCardEntry && m_txtFirstName.Text == string.Empty && m_txtLastName.Text == string.Empty)
         {
            sGreeting = "Hello,";
         }
         else if (viewModel.FirstName == string.Empty)
         {
            sGreeting = "Hello,";
         }
         else
         {
            sGreeting = string.Format("{0} {1},", viewModel.FirstName, viewModel.LastName);
         }

         var oBody = new StringBuilder();
         oBody.AppendLine(sGreeting);
         oBody.AppendLine();
         oBody.AppendLine("Thank you for making a payment on SpeedySpots.com.");
         oBody.AppendLine();
         oBody.AppendLine("Payments:");
         oBody.Append(oLineItems);
         oBody.AppendLine();
         oBody.AppendLine(string.Format("Amount: {0:c}", oIAPayment.Total));
         oBody.AppendLine(string.Format("Card: {0}", viewModel.CardType));
         oBody.AppendLine(string.Format("Card Number: xxxx-xxxx-xxxx-{0}", viewModel.LastFourOfCardNumber));
         oBody.AppendLine(string.Format("Paid On: {0:MMMM dd, yyyy} at {0:h:mm tt}", DateTime.Now));
         if (m_txtInstructions.Text.Length > 0)
         {
            oBody.AppendLine(String.Format("Special Instructions: {0}", m_txtInstructions.Text));
         }
         oBody.AppendLine();
         oBody.AppendLine("Thank you again for using SpeedySpots.com");

         EmailCommunicationService.PaymentCustomerReceiptSend(oBody, viewModel.ReceiptEmailAddressCsv);
      }

      private void SendBillingEmail(CreditCardViewModel viewModel, bool bIsPaymentAutoImported, StringBuilder oSpeedySpotsLineItems,
                                    IAPayment oIAPayment)
      {
         StringBuilder oBody;
         // Email SpeedySpots
         oBody = new StringBuilder();
         if (!bIsPaymentAutoImported)
         {
            oBody.AppendLine("** This payment will NOT be auto-imported **");
            oBody.AppendLine();
         }
         oBody.AppendLine("Payments:");
         oBody.Append(oSpeedySpotsLineItems);
         oBody.AppendLine();
         oBody.AppendLine(string.Format("Customer Name: {0} {1}", viewModel.FirstName, viewModel.LastName));
         oBody.AppendLine(string.Format("Company Name: {0}", viewModel.CompanyName));
         oBody.AppendLine(string.Format("Customer Email(s): {0}", viewModel.ReceiptEmailAddressCsv));
         oBody.AppendLine();
         oBody.AppendLine(string.Format("Amount: {0:c}", oIAPayment.Total));

         if (m_txtInstructions.Text.Length > 0)
         {
            oBody.AppendLine(String.Format("Special Instructions: {0}", m_txtInstructions.Text));
         }
         oBody.AppendLine();
         oBody.AppendLine(string.Format("Card: {0}", viewModel.CardType));
         oBody.AppendLine(string.Format("Card Number: xxxx-xxxx-xxxx-{0}", viewModel.LastFourOfCardNumber));
         oBody.AppendLine(string.Format("Paid On: {0:MMMM dd, yyyy} at {0:h:mm tt}", DateTime.Now));

         EmailCommunicationService.PaymentBillingNoticeSend(oBody);
      }

      private CreditCardViewModel SetCreditCardViewModel(CreditCardService service)
      {
         if (!_transientCardEntry)
         {
            var vm = service.GetCreditCard(int.Parse(PaymentSourceCombo.SelectedValue));
            vm.ReceiptEmailAddressCsv = EmailReceiptTextBox.Text.Trim();
            return vm;
         }

         var cardProfile = service.CreateNew();
         cardProfile.CreditCardNumber = m_txtCreditCardNumber.Text;
         cardProfile.CardVerificationCode = m_txtCreditCardSecurityCode.Text;
         cardProfile.ExpirationMonth = m_cboCreditCardExpireMonth.SelectedValue;
         cardProfile.ExpirationYear = m_cboCreditCardExpireYear.SelectedValue;
         cardProfile.ReceiptEmailAddressCsv = m_txtEmail.Text;
         cardProfile.CardType = m_cboCreditCardType.SelectedItem.Text;
         cardProfile.FirstName = m_txtFirstName.Text;
         cardProfile.LastName = m_txtLastName.Text;
         cardProfile.CompanyName = CompanyNameText.Text;
         return cardProfile;
      }

      private void LoadPayments()
      {
         _invoicePayments = (List<InvoicePayment>) Session["InvoicePayments"];
         if (_invoicePayments != null) return;

         _invoicePayments = new List<InvoicePayment>();
         CreateInvoice();
         SavePayments();
      }

      private void SavePayments()
      {
         foreach (RepeaterItem oItem in m_oRepeaterInvoices.Items)
         {
            var m_txtID = oItem.FindControl("m_txtID") as HiddenField;
            var m_txtNumber = oItem.FindControl("m_txtNumber") as TextBox;
            var m_txtAmount = oItem.FindControl("m_txtAmount") as RadNumericTextBox;

            var oCurrentInvoicePayment = _invoicePayments.SingleOrDefault(row => row.Id == MemberProtect.Utility.ValidateGuid(m_txtID.Value));
            if (oCurrentInvoicePayment != null)
            {
               oCurrentInvoicePayment.Number = m_txtNumber.Text;
               oCurrentInvoicePayment.Amount = MemberProtect.Utility.ValidateDecimal(m_txtAmount.Text);
            }
         }

         Session["InvoicePayments"] = _invoicePayments;

         m_oRepeaterInvoices.DataSource = _invoicePayments;
         m_oRepeaterInvoices.DataBind();
      }

      private void CreateInvoice()
      {
         if (!Page.IsPostBack && Request.QueryString["i"] != null)
         {
            var oInvoicePayment = new InvoicePayment();
            oInvoicePayment.Number = Request.QueryString["i"];
            _invoicePayments.Add(oInvoicePayment);
         }
         else
         {
            _invoicePayments.Add(new InvoicePayment());
         }
      }

      private string GetListOfInvoiceNumbers()
      {
         var invoiceNumbers = "";
         foreach (RepeaterItem oItem in m_oRepeaterInvoices.Items)
         {
            var m_txtNumber = oItem.FindControl("m_txtNumber") as TextBox;
            invoiceNumbers += m_txtNumber.Text + ",";
         }
         if (invoiceNumbers != string.Empty && invoiceNumbers.EndsWith(","))
         {
            invoiceNumbers = invoiceNumbers.Remove(invoiceNumbers.Length - 1, 1);
            invoiceNumbers = (invoiceNumbers.Length > 20) ? invoiceNumbers.Substring(0, 19) : invoiceNumbers;
         }

         return invoiceNumbers;
      }

      public decimal GetTotal()
      {
         decimal fTotal = 0;
         foreach (RepeaterItem oItem in m_oRepeaterInvoices.Items)
         {
            var m_txtNumber = oItem.FindControl("m_txtNumber") as TextBox;
            var m_txtAmount = oItem.FindControl("m_txtAmount") as RadNumericTextBox;

            fTotal += MemberProtect.Utility.ValidateDecimal(m_txtAmount.Text);
         }

         return fTotal;
      }

      public string GetInvoiceDetails()
      {
         var oSB = new StringBuilder();
         foreach (RepeaterItem oItem in m_oRepeaterInvoices.Items)
         {
            var m_txtNumber = oItem.FindControl("m_txtNumber") as TextBox;
            var m_txtAmount = oItem.FindControl("m_txtAmount") as RadNumericTextBox;

            oSB.Append("<div class='group'>");
            oSB.Append("<label>Invoice/Amount:</label>");
            oSB.Append(string.Format("<span class='output'>{0} / {1:c}</span>", (m_txtNumber.Text == string.Empty ? "N/A" : "#" + m_txtNumber.Text),
                                     MemberProtect.Utility.ValidateDecimal(m_txtAmount.Text)));
            oSB.Append("</div>");
         }

         return oSB.ToString();
      }

      public override bool GetSSL()
      {
         return true;
      }

      public override List<AccessControl> GetAccessControl()
      {
         return new List<AccessControl> {AccessControl.Public};
      }

      public List<InvoicePayment> InvoicePayments
      {
         get { return _invoicePayments; }
      }

      private CreditCardService GetCardService()
      {
         return new CreditCardService(MemberProtect.CurrentUser.UserID, MemberProtect, DataAccess, ApplicationContext.SiteProperites);
      }
   }
}