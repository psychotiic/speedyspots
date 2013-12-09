using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.Objects;

namespace SpeedySpots
{
    public partial class Default1 : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(ApplicationContext.IsCustomer)
            {
                Response.Redirect("~/user-dashboard.aspx");
            }
            else if(ApplicationContext.IsTalent)
            {
                Response.Redirect("~/talent-dashboard.aspx");
            }
            else if(ApplicationContext.IsStaff)
            {
                if(MemberProtect.CurrentUser.GetDataItem("DefaultTab") == "In Production")
                {
                    Response.Redirect("~/staff-dashboard.aspx?filter=inproduction");
                }
                else
                {
                    Response.Redirect("~/staff-dashboard.aspx");
                }
            }
        }

        #region Overridden Methods
        public override List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Admin);
            oAccessControl.Add(AccessControl.Staff);
            oAccessControl.Add(AccessControl.Talent);
            oAccessControl.Add(AccessControl.Customer);

            return oAccessControl;
        }
        #endregion
    }
}