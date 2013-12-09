using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.Objects;
using Telerik.Web.UI;

namespace SpeedySpots
{
    public partial class email_templates : SiteBasePage
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
            Response.Redirect("~/email-templates-modify.aspx");
        }

        #region Grid
        protected void OnNeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            m_grdList.DataSource = DataAccess.fn_Admin_GetEmailEstimates();
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

                    Response.Redirect(string.Format("~/email-templates-modify.aspx?id={0}", oDataItem["IAEmailTemplateID"].Text));
                }
            }
        }
        #endregion

        #region Overridden Methods
        public override List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Admin);

            return oAccessControl;
        }
        #endregion
    }
}