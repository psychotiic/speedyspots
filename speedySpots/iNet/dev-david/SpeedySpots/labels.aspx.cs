using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.Objects;
using SpeedySpots.DataAccess;
using Telerik.Web.UI;

namespace SpeedySpots
{
    public partial class labels : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                m_grdList.PageSize = ApplicationContext.GetGridPageSize();
            }
        }

        protected void OnAdd(object sender, EventArgs e)
        {
            Response.Redirect("~/label-modify.aspx");
        }

        #region Grid
        protected void OnNeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            m_grdList.DataSource = DataAccess.IALabels;
        }

        protected void OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if(e.Item is GridDataItem)
            {
                GridDataItem oDataItem = e.Item as GridDataItem;

                if(oDataItem["IsCustomerVisible"].Text == "True")
                {
                    oDataItem["CustomerVisible"].Text = "Yes";
                }
                else
                {
                    oDataItem["CustomerVisible"].Text = "No";
                }
            }
        }

        protected void OnItemCommand(object source, GridCommandEventArgs e)
        {
            if(e.Item is GridDataItem)
            {
                if(e.CommandName == "View")
                {
                    GridDataItem oDataItem = e.Item as GridDataItem;

                    Response.Redirect(string.Format("~/label-modify.aspx?id={0}", MemberProtect.Utility.ValidateInteger(oDataItem["IALabelID"].Text)));
                }
            }
        }
        #endregion

        #region Overridden Methods
        public override List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Admin);
            oAccessControl.Add(AccessControl.Staff);

            return oAccessControl;
        }
        #endregion
    }
}