namespace SpeedySpots.Business.Services
{
   using System;
   using System.Collections.Generic;
   using System.Globalization;
   using System.Linq;
   using System.Text;
   using DataAccess;
   using Elmah;
   using InetSolution.MemberProtect;
   using InetSolution.Web.API.Payment.AuthorizeNET;
   using InetSolution.Web.API.Payment.AuthorizeNETCIM;
   using log4net;
   using Models;
   using ApplicationException = System.ApplicationException;

   /// <summary>
   /// Credit Card Repository
   /// </summary>
   public class CreditCardService
   {
      private static readonly ILog Log = LogManager.GetLogger(typeof (CreditCardService));

      private readonly MemberProtect _memberProtect;
      private readonly DataAccessDataContext _dataContext;
      private readonly SitePropertiesService _siteProperties;

      private readonly bool _enableDebug;
      private readonly string _loginId;
      private readonly string _transactionKey;
      private readonly Guid _customerMemberProtectUserId;

      public CreditCardService(Guid customerMemberProtectUserId, MemberProtect memberProtect, DataAccessDataContext dataContext, SitePropertiesService siteProperties)
      {
         _memberProtect = memberProtect;
         _dataContext = dataContext;
         _siteProperties = siteProperties;

         _enableDebug = siteProperties.AuthorizeNetIsDebug;
         _loginId = _memberProtect.Cryptography.Decrypt(_siteProperties.AuthorizeNetLoginID);
         _transactionKey = _memberProtect.Cryptography.Decrypt(_siteProperties.AuthorizeNetTransactionKey);
         _customerMemberProtectUserId = customerMemberProtectUserId;

         // audit
         if (memberProtect.CurrentUser.UserID != customerMemberProtectUserId)
         {
            // log that user is access customer credit card data?
         }
      }

      public CreditCardViewModel CreateNew()
      {
         var customerCreditCard = new IACustomerCreditCard {MPUserID = _customerMemberProtectUserId};
         _dataContext.IACustomerCreditCards.InsertOnSubmit(customerCreditCard);
         var vm = new CreditCardViewModel(_customerMemberProtectUserId, customerCreditCard);
         return vm;
      }

      public IEnumerable<CreditCardViewModel> GetCreditCards()
      {
         var viewModels = from card in _dataContext.IACustomerCreditCards
                          where card.MPUserID == _customerMemberProtectUserId
                          select new CreditCardViewModel(_customerMemberProtectUserId, card)
                          {
                             Alias = card.Name,
                             CIMPaymentProfileId = card.CIMPaymentProfileId,
                             CreditCardId = card.IACustomerCreditCardID,
                             CardType = _memberProtect.Cryptography.Decrypt(card.CreditCardType),
                             ExpirationMonth = _memberProtect.Cryptography.Decrypt(card.CreditCardExpirationMonth),
                             ExpirationYear = _memberProtect.Cryptography.Decrypt(card.CreditCardExpirationYear),
                             FirstName = _memberProtect.Cryptography.Decrypt(card.CreditCardFirstName),
                             LastName = _memberProtect.Cryptography.Decrypt(card.CreditCardLastName),
                             Address = _memberProtect.Cryptography.Decrypt(card.CreditCardAddress1),
                             City = _memberProtect.Cryptography.Decrypt(card.CreditCardCity),
                             State = _memberProtect.Cryptography.Decrypt(card.CreditCardState),
                             ReceiptEmailAddressCsv = card.CreditCardEmailReceipt
                          };

         if (!viewModels.Any()) return viewModels;

         var profileId = GetProfileId();
         if (profileId == 0) return viewModels;

         var gateway = GetCardGateway();
         var result = gateway.GetCustomerProfile(profileId);
         LogResults(gateway);

         // create view models
         if (result.Item1)
         {
            foreach (var viewModel in viewModels)
            {
               viewModel.CIMProfileId = profileId;

               if (result.Item2.paymentProfiles == null || result.Item2.paymentProfiles.Length == 0) continue;

               var paymentProfile = (from cimCard in result.Item2.paymentProfiles
                                     where cimCard.customerPaymentProfileId == viewModel.CIMPaymentProfileId.ToString(CultureInfo.InvariantCulture)
                                     select cimCard).DefaultIfEmpty(null).FirstOrDefault();

               if (paymentProfile == null) continue;

               PopulateViewModel(viewModel, paymentProfile);
            }
         }

         return viewModels;
      }

      public CreditCardViewModel GetCreditCard(int customerCreditCardId)
      {
         var cardQuery = from card in _dataContext.IACustomerCreditCards
                         where card.MPUserID == _customerMemberProtectUserId &&
                               card.IACustomerCreditCardID == customerCreditCardId
                         select new CreditCardViewModel(_customerMemberProtectUserId, card)
                         {
                            Alias = card.Name,
                            CIMPaymentProfileId = card.CIMPaymentProfileId,
                            CreditCardId = card.IACustomerCreditCardID,
                            CardType = _memberProtect.Cryptography.Decrypt(card.CreditCardType),
                            ExpirationMonth = _memberProtect.Cryptography.Decrypt(card.CreditCardExpirationMonth),
                            ExpirationYear = _memberProtect.Cryptography.Decrypt(card.CreditCardExpirationYear),
                            FirstName = _memberProtect.Cryptography.Decrypt(card.CreditCardFirstName),
                            LastName = _memberProtect.Cryptography.Decrypt(card.CreditCardLastName),
                            CompanyName = _memberProtect.Cryptography.Decrypt(card.CreditCardCompanyName),
                            Address = _memberProtect.Cryptography.Decrypt(card.CreditCardAddress1),
                            City = _memberProtect.Cryptography.Decrypt(card.CreditCardCity),
                            State = _memberProtect.Cryptography.Decrypt(card.CreditCardState),
                            ReceiptEmailAddressCsv = card.CreditCardEmailReceipt
                         };

         if (!cardQuery.Any()) return null;
         var viewModel = cardQuery.First();

         var profileId = GetProfileId();
         if (profileId == 0) return viewModel;
         viewModel.CIMProfileId = profileId;

         var gateway = GetCardGateway();
         var result = gateway.GetCustomerProfile(profileId);
         LogResults(gateway);

         // create view models
         if (result.Item1)
         {
            var paymentProfileList = (from cimCard in result.Item2.paymentProfiles
                                      where cimCard.customerPaymentProfileId == viewModel.CIMPaymentProfileId.ToString(CultureInfo.InvariantCulture)
                                      select cimCard).ToList();
            if (!paymentProfileList.Any()) return viewModel;
            var paymentProfile = paymentProfileList.First();
            PopulateViewModel(viewModel, paymentProfile);
         }

         return viewModel;
      }

      public List<string> SaveCreditCard(CreditCardViewModel creditCard)
      {
         // assure a customer profile exists with AuthorizeNet CIM storage
         creditCard.CIMProfileId = CreateProfileIfNeeded();

         var orgUser = _dataContext.MPOrgUsers.FirstOrDefault(row => row.MPUserID == _customerMemberProtectUserId);
         var company = orgUser == null ? string.Empty : _memberProtect.Organization.GetName(orgUser.MPOrgID);

         // assumes customer profile is already created
         // create new CIM payment profile
         if (creditCard.CIMPaymentProfileId == 0)
         {
            var gateway = GetCardGateway();
            var result = gateway.CreatePaymentProfile(creditCard.CIMProfileId, creditCard.CardType, creditCard.CreditCardNumber,
                                                      string.Format("{0}-{1}", creditCard.ExpirationYear, creditCard.ExpirationMonth),
                                                      creditCard.CardVerificationCode, creditCard.Address, creditCard.City, creditCard.State,
                                                      "", creditCard.FirstName, creditCard.LastName, company);
            LogResults(gateway);

            if (!result.Item1)
            {
               // handle duplicate payment profiles with a friendly error message to the user
               // [E00039] A duplicate customer payment profile already exists.
               //if (gateway.Errors.Any(e => e.Contains("E00039")))
               //{
               //}

               // log it, and ka-boom it
               // can't really recover at this point...
               // throw new ApplicationException("Unable to create AuthorizeNET CIM Payment Profile");
               // not a seam to throw exceptions at, beyond that now.

               return gateway.Errors;
            }
            creditCard.CIMPaymentProfileId = result.Item2;
         }
         else
         {
            var gateway = GetCardGateway();
            var result = gateway.UpdatePaymentProfile(creditCard.CIMProfileId, creditCard.CIMPaymentProfileId, creditCard.CardType, creditCard.CreditCardNumber,
                                                      string.Format("{0}-{1}", creditCard.ExpirationYear, creditCard.ExpirationMonth),
                                                      creditCard.CardVerificationCode, creditCard.Address, creditCard.City, creditCard.State,
                                                      "", creditCard.FirstName, creditCard.LastName);
            LogResults(gateway);
            if (!result)
            {
               return gateway.Errors;
            }
         }

         if (_siteProperties.EnableCreditCardShadowCopy)
         {
            creditCard.CustomerCreditCard.CreditCardNumber = _memberProtect.Cryptography.Encrypt(creditCard.CreditCardNumber);
         }

         creditCard.CustomerCreditCard.Name = creditCard.Alias;
         creditCard.CustomerCreditCard.CIMPaymentProfileId = creditCard.CIMPaymentProfileId;
         creditCard.CustomerCreditCard.CreditCardType = _memberProtect.Cryptography.Encrypt(creditCard.CardType);
         creditCard.CustomerCreditCard.CreditCardExpirationMonth = _memberProtect.Cryptography.Encrypt(creditCard.ExpirationMonth);
         creditCard.CustomerCreditCard.CreditCardExpirationYear = _memberProtect.Cryptography.Encrypt(creditCard.ExpirationYear);
         creditCard.CustomerCreditCard.CreditCardFirstName = _memberProtect.Cryptography.Encrypt(creditCard.FirstName ?? "");
         creditCard.CustomerCreditCard.CreditCardLastName = _memberProtect.Cryptography.Encrypt(creditCard.LastName ?? "");
         creditCard.CustomerCreditCard.CreditCardCompanyName = _memberProtect.Cryptography.Encrypt(creditCard.CompanyName ?? "");
         creditCard.CustomerCreditCard.CreditCardAddress1 = _memberProtect.Cryptography.Encrypt(creditCard.Address ?? "");
         creditCard.CustomerCreditCard.CreditCardAddress2 = string.Empty;
         creditCard.CustomerCreditCard.CreditCardCity = _memberProtect.Cryptography.Encrypt(creditCard.City ?? "");
         creditCard.CustomerCreditCard.CreditCardState = _memberProtect.Cryptography.Encrypt(creditCard.State ?? "");
         creditCard.CustomerCreditCard.CreditCardZip = string.Empty;
         creditCard.CustomerCreditCard.CreditCardEmailReceipt = creditCard.ReceiptEmailAddressCsv;

         // assumed to be the same application level DataContext used to load the card
         _dataContext.SubmitChanges();

         return new List<string>();
      }

      public void DeleteCreditCard(CreditCardViewModel creditCard)
      {
         if (creditCard.CustomerCreditCard == null)
            throw new ApplicationException("Expected creditCard.CustomerCreditCard to be populated.");

         var gateway = GetCardGateway();
         var result = gateway.DeletePaymentProfile(creditCard.CIMProfileId, creditCard.CIMPaymentProfileId);
         LogResults(gateway);

         if (!result) LogError("Unable to remove credit card from CIM storage");

         _dataContext.IACustomerCreditCards.DeleteOnSubmit(creditCard.CustomerCreditCard);

         // assumed to be the same application level DataContext used to load the card
         _dataContext.SubmitChanges();
      }

      public CreditCardServiceResponse PayEstimate(CreditCardViewModel cardProfile, Func<string, string> urlBuilder, IARequest request, IARequestEstimate estimate)
      {
         var response = new CreditCardServiceResponse {SuccessfulCharge = true};

         var transaction = new AuthorizeNETTransactionInformation(cardProfile.CreditCardNumber, estimate.Charge, cardProfile.ExpirationDate)
         {
            FirstName = cardProfile.FirstName,
            LastName = cardProfile.LastName,
            CreditCardCode = cardProfile.CardVerificationCode,
            Email = cardProfile.ReceiptEmailAddressCsv,
            InvoiceNumber = string.Format("Est-{0}", request.RequestIdForDisplay),
            Description = "Request Estimate",
            Company = cardProfile.CompanyName.PadRight(50).Substring(0, 50).Trim(),
            Zip = ""
         };

         var paymentGatewayResponse = new PaymentGatewayResponse(string.Empty);

         if (cardProfile.CreditCardId == 0)
         {
            var authorizeNetDirect = new AuthorizeNET(_enableDebug, _loginId, _transactionKey);

            if (!authorizeNetDirect.ProcessAuthorizationAndCapture(transaction))
            {
               response.SuccessfulCharge = false;
               response.ErrorMessage = authorizeNetDirect.Error;
            }

            response.PaymentGatewayResponse = authorizeNetDirect.PaymentGatewayResponse;
         }
         else
         {
            // go through CIM to process transaction
            paymentGatewayResponse = CIMAuthorizationAndCapture(transaction,
                                                                cardProfile.CIMProfileId,
                                                                cardProfile.CIMPaymentProfileId);
            if (paymentGatewayResponse.Errors.Any() || paymentGatewayResponse.GetValue(PaymentGatewayResponse.FieldNames.ResponseCode) != "1")
            {
               var message = paymentGatewayResponse.Errors.Any()
                                ? paymentGatewayResponse.Errors.Aggregate((messages, error) => messages + ", " + error)
                                : paymentGatewayResponse.GetValue(PaymentGatewayResponse.FieldNames.ResponseReasonText);

               response.SuccessfulCharge = false;
               response.ErrorMessage = message;
            }
         }

         if (!response.SuccessfulCharge) return response;

         RecordOrder(cardProfile, paymentGatewayResponse, estimate);

         var requestStatusLookup = new RequestStatusLookup(_dataContext);
         request.IARequestStatusID = requestStatusLookup.GetRequestStatus(RequestStatus.Processing).IARequestStatusID;
         _dataContext.SubmitChanges();

         var approved = DateTime.Now;
         MarkEstimateAsPaid(request, estimate, approved);

         SendCustomerReceipt(cardProfile, request, estimate);

         InformBillingThatCustomerPaid(cardProfile, paymentGatewayResponse, urlBuilder, request, approved, estimate.Charge);

         return response;
      }

      public PaymentGatewayResponse CIMAuthorizationAndCapture(AuthorizeNETTransactionInformation transaction, long profileId, long paymentProfileId)
      {
         var gateway = new AuthorizeNetCIM(_enableDebug, _loginId, _transactionKey);
         var result = gateway.CreateProfileTransaction(profileId, paymentProfileId, transaction);
         var paymentGatewayResponse = result.Item2;
         paymentGatewayResponse.Errors.AddRange(gateway.Errors);
         paymentGatewayResponse.Info.AddRange(gateway.Info);

         return paymentGatewayResponse;
      }

      private void PopulateViewModel(CreditCardViewModel viewModel, customerPaymentProfileMaskedType paymentProfile)
      {
         if (paymentProfile.billTo != null)
         {
            viewModel.Address = paymentProfile.billTo.address;
            viewModel.City = paymentProfile.billTo.city;
            viewModel.FirstName = paymentProfile.billTo.firstName;
            viewModel.LastName = paymentProfile.billTo.lastName;
            viewModel.State = paymentProfile.billTo.state;
         }

         var creditCard = paymentProfile.payment.Item as creditCardMaskedType;
         if (creditCard == null) return;

         viewModel.CreditCardNumber = creditCard.cardNumber;
      }

      private long CreateProfileIfNeeded()
      {
         var profileId = GetProfileId();

         if (profileId == 0)
         {
            var orgUser = _dataContext.MPOrgUsers.FirstOrDefault(row => row.MPUserID == _customerMemberProtectUserId);
            var company = orgUser == null ? string.Empty : _memberProtect.Organization.GetName(orgUser.MPOrgID);
            var mpUser = _memberProtect.User.GetUsername(_customerMemberProtectUserId);

            // create customer profile
            var gateway = GetCardGateway();
            var result = gateway.CreateCustomerProfile(_customerMemberProtectUserId.ToString(), company, mpUser);
            LogResults(gateway);

            if (!result.Item1)
            {
               // log it, ka-boom it, why? do what?
               // can't recover if we're unable to make a CIM profile
               throw new ApplicationException("Failed to connect to Authorize.NET CIM storage.");
            }

            profileId = result.Item2;
            SetProfileId(profileId);
         }

         return profileId;
      }

      private long GetProfileId()
      {
         var userDataQuery = from userData in _dataContext.MPUserDatas
                             where userData.MPUserID == _customerMemberProtectUserId
                             select userData;

         return !userDataQuery.Any() ? 0 : userDataQuery.First().CIMProfileId;
      }

      private void SetProfileId(long profileId)
      {
         var userDataQuery = from userData in _dataContext.MPUserDatas
                             where userData.MPUserID == _customerMemberProtectUserId
                             select userData;

         var user = userDataQuery.First();
         user.CIMProfileId = profileId;

         // happens later, i hope, once the data is consistent, i hope
         //_dataContext.SubmitChanges();
      }

      private void RecordOrder(CreditCardViewModel paymentDetails, PaymentGatewayResponse response, IARequestEstimate estimate)
      {
         var iaOrder = new IAOrder
         {
            MPUserID = _customerMemberProtectUserId,
            CreditCardType = _memberProtect.Cryptography.Encrypt(paymentDetails.CardType),
            CreditCardNumber = _memberProtect.Cryptography.Encrypt(paymentDetails.CreditCardNumber),
            CreditCardExpirationMonth = _memberProtect.Cryptography.Encrypt(paymentDetails.ExpirationMonth),
            CreditCardExpirationYear = _memberProtect.Cryptography.Encrypt(paymentDetails.ExpirationYear),
            CreditCardFirstName = _memberProtect.Cryptography.Encrypt(paymentDetails.FirstName),
            CreditCardLastName = _memberProtect.Cryptography.Encrypt(paymentDetails.LastName),
            CreditCardZip = "",
            AuthorizeNetResponse = string.Empty,
            AuthorizeNetTransactionID = response.GetValue(PaymentGatewayResponse.FieldNames.TransactionId),
            AuthorizeNetProcessDatetime = DateTime.Now,
            AuthorizeNetProcessResponse = response.GetValue(PaymentGatewayResponse.FieldNames.ResponseCode),
            AuthorizeNetProcessUsername = _dataContext.MPUsers.First(u => u.MPUserID == _customerMemberProtectUserId).Username,
            AuthorizeNetProcessCaptureAmount = estimate.Charge,
            AuthorizeNetProcessCaptureAmountChangeReason = string.Empty,
            AuthorizeNetProcessStatus = "Captured",
            CreatedDateTime = DateTime.Now
         };
         _dataContext.IAOrders.InsertOnSubmit(iaOrder);
         _dataContext.SubmitChanges();

         var iaOrderLineItem = new IAOrderLineItem
         {
            IAOrderID = iaOrder.IAOrderID,
            Description = "Request Estimate",
            Quantity = 1,
            Price = estimate.Charge,
            IsSynched = false
         };
         _dataContext.IAOrderLineItems.InsertOnSubmit(iaOrderLineItem);

         estimate.IAOrderID = iaOrder.IAOrderID;
         _dataContext.SubmitChanges();
      }

      private void MarkEstimateAsPaid(IARequest request, IARequestEstimate estimate, DateTime approved)
      {
         estimate.IsApproved = true;
         estimate.ApprovedDateTime = approved;
         request.CreateNote(_customerMemberProtectUserId, estimate.PreAuthorizedPaymentCharged
                                                             ? string.Format("Estimate payment pre-approved and paid: {0:c}", estimate.Charge)
                                                             : string.Format("Estimate approved and paid: {0:c}", estimate.Charge));
         _dataContext.SubmitChanges();
      }

      private void SendCustomerReceipt(CreditCardViewModel cardProfile, IARequest request, IARequestEstimate estimate)
      {
         var thankYouEmail = new StringBuilder();
         thankYouEmail.AppendLine(string.Format("Thank you for making your estimate payment for request # {0} requested by {1} {2}.<br/>",
                                                request.RequestIdForDisplay, _memberProtect.User.GetDataItem(request.MPUserID, "FirstName"),
                                                _memberProtect.User.GetDataItem(request.MPUserID, "LastName")));
         thankYouEmail.AppendLine("<br/>");
         thankYouEmail.AppendLine("Payment Details<br/>");
         foreach (var job in request.IAJobs)
         {
            thankYouEmail.AppendLine(string.Format("Job: {0}<br />", job.Name));
         }
         thankYouEmail.AppendLine(string.Format("Amount: {0:c}<br/>", estimate.Charge));
         thankYouEmail.AppendLine(string.Format("When: {0:g}<br/>", DateTime.Now));
         thankYouEmail.AppendLine(string.Format("Card: {0} ending in {1}<br/>", cardProfile.CardType,
                                                _memberProtect.Utility.Right(cardProfile.CreditCardNumber, 4)));
         thankYouEmail.AppendLine("<br/>");
         thankYouEmail.AppendLine("Thank you,<br/>");
         thankYouEmail.AppendLine("<a href='http://www.speedyspots.com'>SpeedySpots.com</a><br/>");
         thankYouEmail.AppendLine("<a href='mailto:spots@speedyspots.com'>spots@speedyspots.com</a><br/>");
         thankYouEmail.AppendLine("(734) 475-9327<br/>");
         thankYouEmail.AppendLine("Customer Email: " + cardProfile.ReceiptEmailAddressCsv); //ADDED THIS LINE FOR EMAIL FIx

         var subject = string.Format("Estimate payment received for request {0}.", request.RequestIdForDisplay);
         EmailCommunicationService.EstimatePaymentCustomerReceiptSend(subject, thankYouEmail, cardProfile.ReceiptEmailAddressCsv);
      }

      private void InformBillingThatCustomerPaid(CreditCardViewModel paymentDetails, PaymentGatewayResponse paymentGatewayResponse,
                                                 Func<string, string> urlBuilder,
                                                 IARequest request, DateTime approved, decimal chargeAmount)
      {
         string subject;
         string customerType;

         var mpOrg = _dataContext.MPOrgUsers.FirstOrDefault(row => row.MPUserID == _customerMemberProtectUserId);
         var mpOrgId = mpOrg == null ? Guid.Empty : mpOrg.MPOrgID;

         if (_memberProtect.Utility.YesNoToBool(_memberProtect.Organization.GetDataItem(mpOrgId, "IsVerified")))
         {
            subject = "Estimate Payment made (verified)";
            customerType = "a verified";
         }
         else
         {
            subject = "Estimate Payment made (unverified)";
            customerType = "an unverified";
         }

         var paidEmail = new StringBuilder();
         paidEmail.AppendFormat("An estimate payment was made by {0} customer:<br/>", customerType);
         paidEmail.Append("<br/>");
         paidEmail.AppendFormat("Company: <a href='{0}?id={1}'>{2}</a><br/>", urlBuilder("company-modify.aspx"),
                                mpOrgId.ToString().Replace("-", string.Empty), _memberProtect.Organization.GetName(mpOrgId));
         paidEmail.AppendFormat("Customer: <a href='{0}?id={1}'>{2} {3}</a><br/>", urlBuilder("user-account.aspx"),
                                _customerMemberProtectUserId.ToString().Replace("-", string.Empty),
                                _memberProtect.User.GetDataItem(_customerMemberProtectUserId, "FirstName"),
                                _memberProtect.User.GetDataItem(_customerMemberProtectUserId, "LastName"));
         paidEmail.AppendFormat("Email Receipt: {0}<br/>", paymentDetails.ReceiptEmailAddressCsv);
         paidEmail.AppendFormat("Request: <a href='{0}?rid={1}'>{2}</a><br/>", urlBuilder("create-job.aspx"), request.IARequestID, request.RequestIdForDisplay);
         paidEmail.AppendFormat("Amount: {0:c}<br/>", chargeAmount);
         paidEmail.AppendFormat("Card: {0} ending in {1}<br/>", paymentDetails.CardType, _memberProtect.Utility.Right(paymentDetails.CreditCardNumber, 4));
         paidEmail.AppendFormat("Name on Card: {0} {1}<br/>", paymentDetails.FirstName, paymentDetails.LastName);
         paidEmail.AppendFormat("When: {0:g}<br/>", approved);
         paidEmail.AppendFormat("Authorize Receipt: {0}<br/>", paymentGatewayResponse.GetValue(PaymentGatewayResponse.FieldNames.TransactionId));

         EmailCommunicationService.EstimatePaymentBillingNoticeSend(paidEmail, subject);
      }

      private static void LogResults(AuthorizeNetBase gateway)
      {
         if (gateway == null) throw new ArgumentNullException("gateway");

         // todo: add some useful information to log NDC

         if (gateway.Errors.Any())
            Log.Error(gateway.Errors.Aggregate((message, error) => message + ", " + error));

         if (gateway.Info.Any())
            Log.Info(gateway.Info.Aggregate((message, info) => message + ", " + info));
      }

      private static void LogError(string error)
      {
         ErrorSignal.FromCurrentContext().Raise(new ApplicationException(error));
      }

      private AuthorizeNetCIM GetCardGateway()
      {
         return new AuthorizeNetCIM(_enableDebug, _loginId, _transactionKey);
      }
   }
}