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
    public partial class companies : SiteBasePage
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
            Response.Redirect("~/company-modify.aspx");
        }

        protected void OnFilter(object sender, EventArgs e)
        {
            m_grdList.Rebind();
        }

        #region Grid
        protected void OnNeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            IQueryable<fn_Admin_GetOrganizationsResult> oResults = DataAccess.fn_Admin_GetOrganizations();

            // Apply filters
            if(m_txtName.Text != string.Empty)
            {
                oResults = oResults.Where(row => row.Name.Contains(m_txtName.Text));
            }

            if(m_txtCity.Text != string.Empty)
            {
                oResults = oResults.Where(row => row.City.Contains(m_txtCity.Text));
            }

            if(m_cboState.SelectedValue != string.Empty)
            {
                oResults = oResults.Where(row => row.State.Contains(m_cboState.SelectedValue));
            }

            if(m_txtZip.Text != string.Empty)
            {
                oResults = oResults.Where(row => row.Zip.Contains(m_txtZip.Text));
            }

            if(m_txtPhone.Text != string.Empty)
            {
                oResults = oResults.Where(row => row.Phone.Contains(m_txtPhone.Text));
            }

            switch (ddlVerified.SelectedValue)
            {
                case "Verified":
                    oResults = oResults.Where(row => row.IsVerified == "Y");
                    break;
                case "Unverified":
                    oResults = oResults.Where(row => row.IsVerified == "N");
                    break;
                default:
                    break;
            }

            switch (ddlPrepay.SelectedValue)
            {
                case "Yes":
                    oResults = oResults.Where(row => row.IsPayFirst == "Y");
                    break;
                case "No":
                    oResults = oResults.Where(row => row.IsPayFirst == "N");
                    break;
                default:
                    break;
            }

            if(!m_chkIsArchived.Checked)
            {
                oResults = oResults.Where(row => row.IsArchived != MemberProtect.Utility.BoolToYesNo(true));
            }

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

                    Response.Redirect(string.Format("~/company-modify.aspx?id={0}", MemberProtect.Utility.FormatGuid(MemberProtect.Utility.ValidateGuid(oDataItem["MPOrgID"].Text)).Replace("-", string.Empty)));
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