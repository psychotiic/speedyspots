using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.Objects;
using SpeedySpots.DataAccess;

namespace SpeedySpots
{
    public partial class view_script : SiteBasePage
    {
        private IARequest                           m_oIARequest = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["id"] != null)
            {
                m_oIARequest = DataAccess.IARequests.SingleOrDefault(row => row.IARequestID == MemberProtect.Utility.ValidateInteger(Request.QueryString["id"]));

                if (ApplicationContext.IsCustomer)
                {
                    if (!ApplicationContext.CanCurrentUserViewOrder(m_oIARequest))
                    {
                        m_oIARequest = null;
                    }
                }
            }

            if(m_oIARequest == null)
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        #region Public Properties
        public IARequest IARequest
        {
            get { return m_oIARequest; }
        }
        #endregion

        #region Virtual Methods
        public override List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Admin);
            oAccessControl.Add(AccessControl.Staff);
            oAccessControl.Add(AccessControl.Customer);

            return oAccessControl;
        }
        #endregion
    }
}