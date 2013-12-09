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
    public partial class view_production_notes : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack && Request.QueryString["id"] != null)
            {
                int requestID = MemberProtect.Utility.ValidateInteger(Request.QueryString["id"]);
                SetProductionNotesText(requestID);
            }
        }

        private void SetProductionNotesText(int requestID)
        {
            IARequest request = DataAccess.IARequests.SingleOrDefault(row => row.IARequestID == requestID);
            if (request != null)
            {
                litProductionNotes.Text = request.ProductionNotesForDisplay;

                if (ApplicationContext.IsCustomer && !ApplicationContext.CanCurrentUserViewOrder(request))
                {
                    litProductionNotes.Text = string.Empty;
                }
            }
        }

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