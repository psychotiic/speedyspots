namespace SpeedySpots
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using Business.Services;
   using DataAccess;
   using InetSolution.Web.InetActive;
   using Objects;

   public partial class view_estimate : SiteBasePage
   {
      private IARequestEstimate m_oIARequestEstimate = null;

      protected void Page_Load(object sender, EventArgs e)
      {
         if (Request.QueryString["id"] != null)
         {
            m_oIARequestEstimate = DataAccess.IARequestEstimates.SingleOrDefault(row => row.IARequestEstimateID == MemberProtect.Utility.ValidateInteger(Request.QueryString["id"]));
         }
      }

      protected IARequestEstimate IARequestEstimate
      {
         get { return m_oIARequestEstimate; }
      }

      protected string GetCardAlias()
      {
         var creditCardService = new CreditCardService(m_oIARequestEstimate.IARequest.MPUserID, MemberProtect, DataAccess, ApplicationContext.SiteProperites);
         var cardProfile = creditCardService.GetCreditCard(m_oIARequestEstimate.IARequest.IACustomerCreditCardID);
         return cardProfile != null ? cardProfile.Alias : string.Empty;
      }

      public override List<AccessControl> GetAccessControl()
      {
         return new List<AccessControl> { AccessControl.Admin, AccessControl.Staff, AccessControl.Customer };
      }
   }
}