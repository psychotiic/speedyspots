using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.Objects;

namespace SpeedySpots
{
    public partial class phonetic_tips : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Virtual Methods
        public override List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Customer);
            oAccessControl.Add(AccessControl.Admin);
            oAccessControl.Add(AccessControl.Staff);

            return oAccessControl;
        }
        #endregion
    }
}