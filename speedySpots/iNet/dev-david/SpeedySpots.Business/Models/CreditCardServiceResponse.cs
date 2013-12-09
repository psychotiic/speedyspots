namespace SpeedySpots.Business.Models
{
   using InetSolution.Web.API.Payment.AuthorizeNET;

   public class CreditCardServiceResponse
   {
      public bool SuccessfulCharge { get; set; }
      public string ErrorMessage { get; set; }
      public string InfoMessage { get; set; }
      public PaymentGatewayResponse PaymentGatewayResponse { get; set; }
   }
}