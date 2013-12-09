using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetSolution.MemberProtect;
using SpeedySpots.InetActive.Objects;

namespace SpeedySpots.InetActive
{
    public partial class InetActive : System.Web.UI.MasterPage
    {
        public InetActive()
        {
            ID = "Master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(MemberProtect.CurrentUser.IsAuthorized)
            {
                DateTime oLastLoginDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
                if(Session["LastLogonDateTime"] != null)
                {
                    m_lblLastLogin.Text = string.Format("Your last logon was: {0:dddd}, {0:MMMM} {0:dd}, {0:yyyy} at {0:HH:mm:ss tt} | ", (DateTime)Session["LastLogonDateTime"]);
                }
                else
                {
                    m_lblLastLogin.Text = "Your last logon was: Unknown | ";
                }
                
                m_btnModifyUser.Text = MemberProtect.CurrentUser.Username;
            }
            else
            {
                m_pModifyUser.Visible = false;
                m_ulInetActive.Visible = false;
            }
        }

        protected void OnModifyUser(object sender, EventArgs e)
        {
            Session["MPUserID"] = MemberProtect.CurrentUser.UserID;

            Response.Redirect("~/InetActive/UserEdit.aspx");
        }

        #region Public Properties
        public HtmlGenericControl BodyTag
        {
            get { return m_oBodyTag; }
        }

        public MemberProtect MemberProtect
        {
            get { return ((InetActiveBasePage)Page).MemberProtect; }
        }

        public string CopyrightYears
        {
            get { return string.Format("2000-{0}", DateTime.Today.Year); }
        }
        #endregion
    }
}