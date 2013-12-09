using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SpeedySpots.Objects;
using SpeedySpots.DataAccess;
using Telerik.Web.UI;

namespace SpeedySpots
{
    public partial class admin_dashboard : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // User must be a producer in order to visit this page
            if(!ApplicationContext.IsAdmin && !ApplicationContext.IsStaff)
            {
                Response.Redirect("~/Default.aspx");
            }

            if(!IsPostBack)
            {
                m_grdList.PageSize = ApplicationContext.GetGridPageSize();
            }
        }

        protected void OnFilter(object sender, EventArgs e)
        {
            m_grdList.Rebind();
        }

        protected void OnCreate(object sender, EventArgs e)
        {
            Response.Redirect("~/user-account.aspx?mode=new");
        }

        protected void OnCancel(object sender, EventArgs e)
        {
            Response.Redirect("~/staff-dashboard.aspx");
        }

        #region Grid
        protected void OnNeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            IQueryable<fn_Admin_GetDashboardResult> oResults = DataAccess.fn_Admin_GetDashboard();

            if(ApplicationContext.IsStaff && !ApplicationContext.IsAdmin)
            {
                // Staff Only (Non-Admin) users should only see Customer users here
                oResults = oResults.Where(row => row.IsCustomer == "Y");
            }

            // Apply filters
            if(m_txtName.Text != string.Empty)
            {
                oResults = oResults.Where(row => row.Name.Contains(m_txtName.Text));
            }
            
            if(m_txtEmail.Text != string.Empty)
            {
                oResults = oResults.Where(row => row.Username.Contains(m_txtEmail.Text));
            }
            
            if(m_cboType.Text != string.Empty)
            {
                if(m_cboType.Text == "Customer")
                {
                    oResults = oResults.Where(row => row.IsCustomer == MemberProtect.Utility.BoolToYesNo(true));
                }
                else if(m_cboType.Text == "Staff")
                {
                    oResults = oResults.Where(row => row.IsStaff == MemberProtect.Utility.BoolToYesNo(true));
                }
                else if(m_cboType.Text == "Talent")
                {
                    oResults = oResults.Where(row => row.IsTalent == MemberProtect.Utility.BoolToYesNo(true));
                }
                else if(m_cboType.Text == "Admin")
                {
                    oResults = oResults.Where(row => row.IsAdmin == MemberProtect.Utility.BoolToYesNo(true));
                }
            }

            if(m_chkShowArchived.Checked)
            {
                oResults = oResults.Where(row => row.IsArchived == MemberProtect.Utility.BoolToYesNo(true) || row.IsArchived == MemberProtect.Utility.BoolToYesNo(false));
            }
            else
            {
                oResults = oResults.Where(row => row.IsArchived == MemberProtect.Utility.BoolToYesNo(false));
            }

            m_grdList.DataSource = oResults;
        }

        protected void OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if(e.Item is GridDataItem)
            {
                GridDataItem oDataItem = e.Item as GridDataItem;

                bool bIsArchived = MemberProtect.Utility.YesNoToBool(oDataItem["IsArchived"].Text);
                if(bIsArchived)
                {
                    HtmlGenericControl oSpan = new HtmlGenericControl("span");
                    oSpan.InnerHtml = " - <span style='color: red;'>Archived</span>";
                    oDataItem["Name"].Controls.Add(oSpan);
                }

                string sType = string.Empty;
                if(MemberProtect.Utility.YesNoToBool(oDataItem["IsAdmin"].Text))
                {
                    sType += "Admin, ";
                }
                if(MemberProtect.Utility.YesNoToBool(oDataItem["IsCustomer"].Text))
                {
                    sType += "Customer, ";
                }
                if(MemberProtect.Utility.YesNoToBool(oDataItem["IsStaff"].Text))
                {
                    sType += "Staff, ";
                }
                if(MemberProtect.Utility.YesNoToBool(oDataItem["IsTalent"].Text))
                {
                    sType += "Talent, ";
                }
                
                if(sType.Length > 2)
                {
                    sType = MemberProtect.Utility.Left(sType, sType.Length - 2);
                }

                oDataItem["Type"].Text = sType;

                if(!ApplicationContext.IsAdmin)
                {
                    oDataItem["IsArchived"].Controls.Clear();
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

                    Response.Redirect(string.Format("~/user-account.aspx?id={0}", MemberProtect.Utility.FormatGuid(MemberProtect.Utility.ValidateGuid(oDataItem["MPUserID"].Text)).Replace("-", string.Empty)));
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