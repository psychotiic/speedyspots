namespace SpeedySpots
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using Business;
   using Objects;
   using Telerik.Web.UI;

   public partial class talent_dashboard : SiteBasePage
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         if (!IsPostBack)
         {
            m_grdList.PageSize = ApplicationContext.GetGridPageSize();
         }
      }

      #region Grid

      protected void OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
      {
         var results =
            DataAccess.fn_Talent_GetDashboard(MemberProtect.CurrentUser.UserID)
                      .Where(
                         row =>
                         row.IAProductionOrderStatusID == ApplicationContext.GetProductionOrderStatusID(ProductionOrderStatus.Incomplete) && row.SpotsNotOnHold > 0)
                      .ToList();
         m_grdList.DataSource = results;
      }

      protected void OnItemDataBound(object sender, GridItemEventArgs e)
      {
         if (e.Item is GridDataItem)
         {
            var oDataItem = e.Item as GridDataItem;

            if (oDataItem["HasBeenViewedByTalent"].Text == "False")
            {
               oDataItem.Style["font-weight"] = "bold";
            }

            if (oDataItem["IsAsap"].Text == "True")
            {
               if (oDataItem["HasBeenViewedByTalent"].Text == "True")
               {
                  oDataItem.Style["background-color"] = "Pink";
               }
               else
               {
                  oDataItem.Style["background-color"] = "LemonChiffon";
               }

               oDataItem["DueDateTime"].Text += " <span style='color: Red;'>ASAP</span>";
            }

            var oIAProductionOrder =
               DataAccess.IAProductionOrders.SingleOrDefault(
                  row => row.IAProductionOrderID == MemberProtect.Utility.ValidateInteger(oDataItem["IAProductionOrderID"].Text));
            if (oIAProductionOrder != null)
            {
               foreach (var oIASpot in oIAProductionOrder.IASpots)
               {
                  if (oIASpot.IASpotStatusID == ApplicationContext.GetSpotStatusID(SpotStatus.NeedsFix))
                  {
                     oDataItem["Status"].Text = " <span style='color: Red;'>NEEDS RE-RECORD</span>";
                     break;
                  }
               }
            }
         }
      }

      #endregion

      #region Virtual Methods

      public override List<AccessControl> GetAccessControl()
      {
         var oAccessControl = new List<AccessControl>();

         oAccessControl.Add(AccessControl.Talent);

         return oAccessControl;
      }

      #endregion
   }
}