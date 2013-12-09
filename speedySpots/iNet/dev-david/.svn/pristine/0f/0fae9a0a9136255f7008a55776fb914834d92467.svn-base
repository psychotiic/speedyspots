using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using SpeedySpots.Objects;
using SpeedySpots.DataAccess;
using Telerik.Web.UI;

namespace SpeedySpots
{
   using Business;

   public partial class talent_production_orders_to_qc : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                m_grdList.PageSize = ApplicationContext.GetGridPageSize();

                m_dtFrom.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 0, 0, 0, 0);
                m_dtTo.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month), 23, 59, 59, 999);
            }
        }

        protected void OnFilter(object sender, EventArgs e)
        {
            if (m_dtFrom.SelectedDate == null)
            {
                m_dtFrom.SelectedDate = m_dtFrom.MinDate;
            }

            if (m_dtTo.SelectedDate == null)
            {
                m_dtTo.SelectedDate = m_dtFrom.MaxDate;
            }

            m_grdList.Rebind();
        }

        #region Grid
        protected void OnNeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            IQueryable<fn_Talent_GetProductionOrdersResult> oResults = DataAccess.fn_Talent_GetProductionOrders(MemberProtect.CurrentUser.UserID).Where(row => row.IAProductionOrderStatusID == ApplicationContext.GetProductionOrderStatusID(ProductionOrderStatus.Complete));

            // Apply filter
            if (m_dtFrom.SelectedDate != null && m_dtTo.SelectedDate != null)
            {
                oResults = oResults.Where(row => row.TalentMarkedCompleteDateTime.CompareTo(m_dtFrom.SelectedDate) >= 0 && row.TalentMarkedCompleteDateTime.CompareTo(m_dtTo.SelectedDate) <= 0);
            }

            m_grdList.DataSource = oResults;
        }

        protected void OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem oDataItem = e.Item as GridDataItem;

                DateTime qcMarkDate = DateTime.Parse(oDataItem["QCMarkedCompleteDateTime"].Text.Replace("at ", ""));

                if (qcMarkDate == new DateTime(1950, 1, 1))
                {
                    oDataItem["QCMarkedCompleteDateTime"].Text = string.Empty;
                }
            }
        }

        #endregion

        #region Virtual Methods
        public override List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Talent);

            return oAccessControl;
        }
        #endregion
    }
}