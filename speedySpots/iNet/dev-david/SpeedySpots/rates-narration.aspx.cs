using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.Objects;

namespace SpeedySpots
{
    public partial class rates_narration : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(ApplicationContext.IsCustomer)
                {
                    string sCountry = MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(), "Country");

                    if(sCountry != "United States")
                    {
                        Response.Redirect("~/rates.aspx");
                    }
                }
                else if(ApplicationContext.IsTalent)
                {
                    Response.Redirect("~/rates.aspx");
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