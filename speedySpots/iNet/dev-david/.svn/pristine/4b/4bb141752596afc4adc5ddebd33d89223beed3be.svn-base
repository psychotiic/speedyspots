namespace SpeedySpots
{
   using System;
   using System.Collections.Generic;
   using Business.Services;
   using Objects;
   using Telerik.Web.UI;

   public partial class user_creditcards : SiteBasePage
   {
      private Guid _userId = Guid.Empty;

      protected void Page_Load(object sender, EventArgs e)
      {
         _userId = MemberProtect.CurrentUser.UserID;

         // If the user is a customer or talent and they are NOT viewing their own credit cards, redirect them them away
         // - Only Admins may view other people's profiles
         if (ApplicationContext.IsCustomer || ApplicationContext.IsTalent)
         {
            if (_userId != MemberProtect.CurrentUser.UserID)
            {
               RedirectMessage("~/Default.aspx", "Access denied");
            }
         }

         if (!IsPostBack)
         {
         }
      }

      public Guid MPUserID
      {
         get { return _userId; }
      }

      protected void OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
      {
         var service = GetCardService();
         var creditCards = service.GetCreditCards();
         m_grdList.DataSource = creditCards;
      }

      protected void OnItemDataBound(object sender, GridItemEventArgs e)
      {
      }

      /// <summary>
      /// Allow access to this page for the specified roles
      /// </summary>
      /// <returns></returns>
      public override List<AccessControl> GetAccessControl()
      {
         return new List<AccessControl>
         {
            AccessControl.Admin,
            AccessControl.Staff,
            AccessControl.Talent,
            AccessControl.Customer
         };
      }

      private CreditCardService GetCardService()
      {
         return new CreditCardService(MemberProtect.CurrentUser.UserID, MemberProtect, DataAccess, ApplicationContext.SiteProperites);
      }
   }
}