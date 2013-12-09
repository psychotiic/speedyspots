using System;
using System.Collections.Generic;
using System.Linq;

namespace SpeedySpots.Business.Helpers
{
   public static class CreditCards
   {
      public static List<KeyValuePair<string, string>> LoadCreditCardExpirationMonths()
      {
         List<KeyValuePair<string, string>> months = new List<KeyValuePair<string, string>>();
            
         months.Add(new KeyValuePair<string, string>("01", "01"));  // Jan
         months.Add(new KeyValuePair<string, string>("02", "02"));
         months.Add(new KeyValuePair<string, string>("03", "03"));
         months.Add(new KeyValuePair<string, string>("04", "04"));
         months.Add(new KeyValuePair<string, string>("05", "05"));
         months.Add(new KeyValuePair<string, string>("06", "06"));
         months.Add(new KeyValuePair<string, string>("07", "07"));
         months.Add(new KeyValuePair<string, string>("08", "08"));
         months.Add(new KeyValuePair<string, string>("09", "09"));
         months.Add(new KeyValuePair<string, string>("10", "10"));
         months.Add(new KeyValuePair<string, string>("11", "11"));
         months.Add(new KeyValuePair<string, string>("12", "12"));

         return months;
      }

      public static List<KeyValuePair<int, int>> LoadCreditCardExpirationYears()
      {
         List<KeyValuePair<int, int>> years = new List<KeyValuePair<int, int>>();

         for (int i = DateTime.Today.Year; i < (DateTime.Today.Year + 15); i++)
         {
            years.Add(new KeyValuePair<int, int>(i, i));
         }

         return years;
      }

      public static List<KeyValuePair<string, string>> LoadCreditCardTypes()
      {
         return new List<KeyValuePair<string, string>>
         {
            new KeyValuePair<string, string>("", ""),
            new KeyValuePair<string, string>("VISA", "VISA"),
            new KeyValuePair<string, string>("MasterCard", "MasterCard"),
            new KeyValuePair<string, string>("Discover", "Discover"),
            new KeyValuePair<string, string>("AmericanExpress", "American Express")
         };
      }

      public static Models.AuthorizeNetSimpleResponse ParseAuthorizeNetResponse(string rawResponse)
      {
         string[] responseArray = rawResponse.Split('|');
         Models.AuthorizeNetSimpleResponse response = new Models.AuthorizeNetSimpleResponse();

         response.ResponseCode = int.Parse(responseArray[0]);
         response.ResponseReasonCode = int.Parse(responseArray[2]);
         response.ResponseReasonText = responseArray[3];
         response.TransactionId = responseArray[6];

         return response;
      }
   }
}