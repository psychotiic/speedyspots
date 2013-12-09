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
    public partial class report_unavailability : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DateTime oStartOfToday = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0, 0);
                DateTime oEndOfToday = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59, 999);

                m_oRepeaterUnavailable.DataSource = DataAccess.fn_Producer_GetUnavailabilityReport(oStartOfToday, oEndOfToday).OrderBy(row => row.Talent);
                m_oRepeaterUnavailable.DataBind();
            }
        }

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