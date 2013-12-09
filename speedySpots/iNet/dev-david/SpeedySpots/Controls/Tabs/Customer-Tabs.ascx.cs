using System;
using System.Linq;

namespace SpeedySpots.Controls.Tabs
{
   public partial class Customer_Tabs : SiteBaseControl
   {
      public enum TabOption
      {
         Requests,
         RequestsAll,
         Messages,
         ManageAccount,
         CreditCards
      }

      public TabOption SelectedTab { get; set; }

      protected void Page_Load(object sender, EventArgs e)
      {
         if (!Page.IsPostBack)
         {
            SetActiveTab();

            if (!ApplicationContext.CustomerHasCoworkersAssociated())
            {
               liAllRequests.Visible = false;
            }
         }
      }

      private void SetActiveTab()
      {
         switch (this.SelectedTab)
         {
            case TabOption.ManageAccount:
               liManage.Attributes.Add("class", "at");
               break;
            case TabOption.Messages:
               liMessages.Attributes.Add("class", "at");
               break;
            case TabOption.RequestsAll:
               liAllRequests.Attributes.Add("class", "at");
               break;
            case TabOption.Requests:
               // no-op?
               break;
            case TabOption.CreditCards:
               liCreditCards.Attributes.Add("class", "at");
               break;
            default:
               liRequests.Attributes.Add("class", "at");
               break;
         }
      }
   }
}