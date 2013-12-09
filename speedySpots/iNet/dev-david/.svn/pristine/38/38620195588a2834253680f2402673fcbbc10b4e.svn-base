using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.Objects;
using Newtonsoft.Json;
using SpeedySpots.Business.Models;
using SpeedySpots.Business.Services;
using SpeedySpots.DataAccess;
using System.Text;

namespace SpeedySpots
{
    public partial class ajax_staff_requests_dashboard : AjaxBasePage
    {
        private StaffRequestDashboardQuery.OrderByOptions _orderBy = StaffRequestDashboardQuery.OrderByOptions.IARequestID;

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();

            if (IsAuthenticated && ApplicationContext.IsStaff)
            {
                LoadDashboard();
            }
        }

        private void LoadDashboard()
        {
            StaffRequestDashboardQuery query = BuildQuery();

            m_RequestDashboard.Query = query;
            m_RequestDashboard.BindData();            
        }

        private StaffRequestDashboardQuery BuildQuery()
        {
            StaffRequestDashboardQuery query = Business.Services.StaffDashboardRequestsService.GetStaffRequestDashboardSettings(MemberProtect.CurrentUser.UserID, DataAccess);
            
            string orderBy = (Request.Form["OrderBy"] == null) ? string.Empty : Request.Form["OrderBy"].ToString();

            query.SetOrderBy(orderBy);
           
            query.PageSize = Request.Form["PageSize"] == null ? 50 : int.Parse(Request.Form["PageSize"].ToString());
            query.PageNumber = Request.Form["PageNumber"] == null ? 1 : int.Parse(Request.Form["PageNumber"].ToString());

            return query;
        }

    }
}