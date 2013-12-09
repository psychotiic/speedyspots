namespace SpeedySpots
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using Business;
   using DataAccess;
   using Objects;

   public partial class create_request_confirm : SiteBasePage
   {
      private IARequest m_oIARequest = null;

      protected void Page_Load(object sender, EventArgs e)
      {
         if (IsPostBack) return;

         if (Session["IARequestID"] != null)
         {
            m_oIARequest = DataAccess.IARequests.SingleOrDefault(row => row.IARequestID == (int) Session["IARequestID"]);
         }
         else
         {
            Response.Redirect("~/user-dashboard.aspx");
         }

         if (Session["errorMessage"] != null && !string.IsNullOrEmpty(Session["errorMessage"].ToString()))
         {
            m_divMessageNegative.Visible = true;
            m_litError.Text = string.Format("Issue: There was a {0}", Session["errorMessage"].ToString());
         }
         else
         {
            m_divMessagePositive.Visible = true;
         }

         if (m_oIARequest.IACustomerCreditCardID > 0)
         {
            paymentPreAuthorized.Visible = true;
         }
         else if (IARequest.IARequestStatusID == ApplicationContext.GetRequestStatusID(RequestStatus.NeedsEstimate))
         {
            estimateMessage.Visible = true;
         }
    
      }

      protected IARequest IARequest
      {
         get { return m_oIARequest; }
      }

      public override List<AccessControl> GetAccessControl()
      {
         return new List<AccessControl> {AccessControl.Customer, AccessControl.Staff};
      }
   }
}