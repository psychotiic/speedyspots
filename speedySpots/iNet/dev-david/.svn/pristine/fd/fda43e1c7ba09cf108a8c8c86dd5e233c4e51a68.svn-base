namespace SpeedySpots.Business.Models
{
   using System;
   using DataAccess;
   using InetSolution.MemberProtect;

   public class CustomerCreditCardViewModel
   {
      private readonly MemberProtect _memberProtect;

      public CustomerCreditCardViewModel(MemberProtect memberProtect, IACustomerCreditCard customerCreditCard)
      {
         IACustomerCreditCard = customerCreditCard;
         _memberProtect = memberProtect;
      }

      public string CIMProfileId { get; set; }

      public string CIMPaymentProfileId { get; set; }

      public int CustomerCreditCardId
      {
         get { return IACustomerCreditCard.IACustomerCreditCardID; }
      }

      public Guid MPUserId
      {
         get { return IACustomerCreditCard.MPUserID; }
      }

      public string CardVerificationCode { get; set; }

      public string LastFourOfCardNumber
      {
         get
         {
            var creditCardNumber = _memberProtect.Cryptography.Decrypt(IACustomerCreditCard.CreditCardNumber);
            return !string.IsNullOrEmpty(creditCardNumber) && creditCardNumber.Length > 4
                      ? creditCardNumber.Substring(creditCardNumber.Length - 4, 4)
                      : "xxxx";
         }
      }

      public string Alias
      {
         get
         {
            return string.IsNullOrEmpty(IACustomerCreditCard.Name)
                      ? string.Format("{0} - {1}", Type, LastFourOfCardNumber)
                      : IACustomerCreditCard.Name;
         }
         set { IACustomerCreditCard.Name = value; }
      }

      public string CreditCardNumber
      {
         set { IACustomerCreditCard.CreditCardNumber = _memberProtect.Cryptography.Encrypt(value); }
      }

      public string ExpirationMonth
      {
         get { return _memberProtect.Cryptography.Decrypt(IACustomerCreditCard.CreditCardExpirationMonth); }
         set { IACustomerCreditCard.CreditCardExpirationMonth = _memberProtect.Cryptography.Encrypt(value); }
      }

      public string ExpirationYear
      {
         get { return _memberProtect.Cryptography.Decrypt(IACustomerCreditCard.CreditCardExpirationYear); }
         set { IACustomerCreditCard.CreditCardExpirationYear = _memberProtect.Cryptography.Encrypt(value); }
      }

      public string ExpirationDate
      {
         get
         {
            if (string.IsNullOrEmpty(ExpirationMonth) || string.IsNullOrEmpty(ExpirationYear))
            {
               return DateTime.MinValue.ToString("MM/yyyy");
            }

            var year = 0;
            var yearParsed = int.TryParse(ExpirationYear, out year);

            var month = 0;
            var monthParsed = int.TryParse(ExpirationMonth, out month);

            if (yearParsed == false || monthParsed == false)
            {
               return DateTime.MinValue.ToString("MM/yyyy");
            }

            return new DateTime(year, month, 1).ToString("MM/yyyy");
         }
      }

      public string Type
      {
         get { return _memberProtect.Cryptography.Decrypt(IACustomerCreditCard.CreditCardType); }
         set { IACustomerCreditCard.CreditCardType = _memberProtect.Cryptography.Encrypt(value); }
      }

      public string FirstName
      {
         get { return _memberProtect.Cryptography.Decrypt(IACustomerCreditCard.CreditCardFirstName); }
         set { IACustomerCreditCard.CreditCardFirstName = _memberProtect.Cryptography.Encrypt(value); }
      }

      public string LastName
      {
         get { return _memberProtect.Cryptography.Decrypt(IACustomerCreditCard.CreditCardLastName); }
         set { IACustomerCreditCard.CreditCardLastName = _memberProtect.Cryptography.Encrypt(value); }
      }

      public string Address1
      {
         get { return _memberProtect.Cryptography.Decrypt(IACustomerCreditCard.CreditCardAddress1); }
         set { IACustomerCreditCard.CreditCardAddress1 = _memberProtect.Cryptography.Encrypt(value); }
      }

      public string Address2
      {
         get { return _memberProtect.Cryptography.Decrypt(IACustomerCreditCard.CreditCardAddress2); }
         set { IACustomerCreditCard.CreditCardAddress2 = _memberProtect.Cryptography.Encrypt(value); }
      }

      public string City
      {
         get { return _memberProtect.Cryptography.Decrypt(IACustomerCreditCard.CreditCardCity); }
         set { IACustomerCreditCard.CreditCardCity = _memberProtect.Cryptography.Encrypt(value); }
      }

      public string State
      {
         get { return _memberProtect.Cryptography.Decrypt(IACustomerCreditCard.CreditCardState); }
         set { IACustomerCreditCard.CreditCardState = _memberProtect.Cryptography.Encrypt(value); }
      }

      public string Zip
      {
         get { return _memberProtect.Cryptography.Decrypt(IACustomerCreditCard.CreditCardZip); }
         set { IACustomerCreditCard.CreditCardZip = _memberProtect.Cryptography.Encrypt(value); }
      }

      public string EmailReceipt
      {
         get { return IACustomerCreditCard.CreditCardEmailReceipt; }
         set { IACustomerCreditCard.CreditCardEmailReceipt = value; }
      }

      internal IACustomerCreditCard IACustomerCreditCard { get; private set; }
   }
}