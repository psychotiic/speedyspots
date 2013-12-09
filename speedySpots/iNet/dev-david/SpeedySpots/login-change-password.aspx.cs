using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.Objects;

namespace SpeedySpots
{
    public partial class login_change_password : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                m_txtPassword.Focus();
            }
        }

        protected void OnChangePassword(object sender, EventArgs e)
        {
            MemberProtect.User.ChangePassword(MemberProtect.CurrentUser.UserID, m_txtPassword.Text);

            RedirectMessage("~/user-dashboard.aspx", "Your new password has been saved.", InetSolution.Web.MessageTone.Positive);
        }

        #region Virtual Methods
        public override List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Admin);
            oAccessControl.Add(AccessControl.Customer);
            oAccessControl.Add(AccessControl.Staff);
            oAccessControl.Add(AccessControl.Talent);

            return oAccessControl;
        }
        #endregion
    }
}