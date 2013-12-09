using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.DataAccess;
using SpeedySpots.InetActive.Objects;
using Microsoft.Security.Application;
using Telerik.Web.UI;

namespace SpeedySpots.InetActive
{
    public partial class OrganizationList : InetActiveBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Session.Remove("MPOrgID");
            }
        }

        protected void OnFilter(object sender, EventArgs e)
        {
            m_grdList.Rebind();
        }

        #region Grid
        protected void OnNeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            IQueryable<fn_InetActive_GetOrganizationsResult> oResults = DataAccess.fn_InetActive_GetOrganizations();

            bool bFiltering = false;

            if(m_txtName.Text != string.Empty)
            {
                bFiltering = true;
                oResults = oResults.Where(row => row.Name.Contains(m_txtName.Text));
            }

            m_spanFilter.Visible = bFiltering;
            m_lblCount.Text = AntiXss.HtmlEncode(string.Format("{0} Organization(s)", oResults.Count()));
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
                    Session["MPOrgID"] = MemberProtect.Utility.ValidateGuid(oDataItem["MPOrgID"].Text);

                    Response.Redirect("~/InetActive/OrganizationEdit.aspx");
                }
            }
        }
        #endregion
    }
}