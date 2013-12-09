using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.Objects;

namespace SpeedySpots
{
    public partial class logoff : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Remove InetActive 'Remember Me' cookie information
            HttpCookie oInetActiveCookie = ApplicationContext.HttpContext.Response.Cookies["InetActiveCookie"];
            oInetActiveCookie.Value = string.Empty;
            ApplicationContext.HttpContext.Response.Cookies.Set(oInetActiveCookie);

            Logoff();
        }

        #region Overridden Methods
        public override List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Public);

            return oAccessControl;
        }
        #endregion
    }
}