namespace SpeedySpots.Business.Models
{
   using System;
   using DataAccess;

   public class CreditCardViewModel
   {
      private readonly Guid _mpUserId;
      private readonly IACustomerCreditCard _customerCreditCard;

      public CreditCardViewModel(Guid mpUserId)
      {
         _mpUserId = mpUserId;
      }

      public CreditCardViewModel(Guid mpUserId, IACustomerCreditCard customerCreditCard)
      {
         _mpUserId = mpUserId;
         _customerCreditCard = customerCreditCard;
      }

      public Guid MPUserId { get { return _mpUserId; } }
      public int CreditCardId { get; set; }
      public long CIMProfileId { get; set; }
      public long CIMPaymentProfileId { get; set; }
      public string CardVerificationCode { get; set; }
      public string CreditCardNumber { get; set; }
      public string ExpirationMonth { get; set; }
      public string ExpirationYear { get; set; }
      public string CardType { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string CompanyName { get; set; }
      public string Address { get; set; }
      public string City { get; set; }
      public string State { get; set; }
      public string ReceiptEmailAddressCsv { get; set; }

      public string LastFourOfCardNumber
      {
         get
         {
            return !string.IsNullOrEmpty(CreditCardNumber) && CreditCardNumber.Length > 4
                      ? CreditCardNumber.Substring(CreditCardNumber.Length - 4, 4)
                      : "xxxx";
         }
      }

      private string _alias;

      public string Alias
      {
         get
         {
            return string.IsNullOrEmpty(_alias)
                      ? string.Format("{0} - {1}", CardType, LastFourOfCardNumber)
                      : _alias;
         }
         set { _alias = value; }
      }

      public DateTime ExpirationDate
      {
         get
         {
            if (string.IsNullOrEmpty(ExpirationMonth) || string.IsNullOrEmpty(ExpirationYear))
            {
               return DateTime.MinValue;
            }

            int year;
            var yearParsed = int.TryParse(ExpirationYear, out year);

            int month;
            var monthParsed = int.TryParse(ExpirationMonth, out month);

            if (yearParsed == false || monthParsed == false)
            {
               return DateTime.MinValue;
            }

            return new DateTime(year, month, 1);
         }
      }

      public IACustomerCreditCard CustomerCreditCard
      {
         get { return _customerCreditCard; }
      }
   }
}