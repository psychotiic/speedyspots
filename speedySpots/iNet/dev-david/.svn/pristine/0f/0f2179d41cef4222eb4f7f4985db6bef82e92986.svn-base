using System;
using System.Linq;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SpeedySpots.DataAccess;
using SpeedySpots.InetActive.Objects;
using Telerik.Web.UI;
using Microsoft.Security.Application;

namespace SpeedySpots.InetActive
{
    public partial class UserList : InetActiveBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Session.Remove("MPUserID");
            }
        }

        protected void OnFilter(object sender, EventArgs e)
        {
            m_grdList.Rebind();
        }

        #region Grid
        protected void OnNeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            IQueryable<fn_InetActive_GetUsersResult> oResults = DataAccess.fn_InetActive_GetUsers();

            bool bFiltering = false;

            if(m_txtUsername.Text != string.Empty)
            {
                bFiltering = true;
                oResults = oResults.Where(row => row.Username.Contains(m_txtUsername.Text));
            }

            m_spanFilter.Visible = bFiltering;
            m_lblCount.Text = AntiXss.HtmlEncode(string.Format("{0} User(s)", oResults.Count()));
            m_grdList.DataSource = oResults;
        }

        protected void OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if(e.Item is GridDataItem)
            {
                GridDataItem oDataItem = e.Item as GridDataItem;
            }
        }

        protected void OnItemCommand(object source, GridCommandEventArgs e)
        {
            if(e.Item is GridDataItem)
            {
                if(e.CommandName == "View")
                {
                    GridDataItem oDataItem = e.Item as GridDataItem;
                    Session["MPUserID"] = MemberProtect.Utility.ValidateGuid(oDataItem["MPUserID"].Text);

                    Response.Redirect("~/InetActive/UserEdit.aspx");
                }
            }
        }
        #endregion
    }
}
