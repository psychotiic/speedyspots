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
    public partial class quality_control_spot_details : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["rid"] != null)
            {
                Session["IARequestID"] = MemberProtect.Utility.ValidateInteger(Request.QueryString["rid"].ToString());
            }

            if (Request.QueryString["jid"] != null)
            {
                Session["IAJobID"] = MemberProtect.Utility.ValidateInteger(Request.QueryString["jid"].ToString());
            }

            if (Request.QueryString["poid"] != null)
            {
                Session["IAProductionOrderID"] = MemberProtect.Utility.ValidateInteger(Request.QueryString["poid"].ToString());
            }
        }

        #region Virtual Methods
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