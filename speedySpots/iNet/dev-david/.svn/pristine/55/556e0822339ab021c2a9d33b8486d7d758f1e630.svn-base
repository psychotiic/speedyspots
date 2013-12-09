using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using InetSolution.Web;
using InetSolution.MemberProtect;
using SpeedySpots.DataAccess;
using SpeedySpots.Objects;

namespace SpeedySpots.InetActive.Objects
{
    public enum AccessControl
    {
        Private,
        Public,
    }

    public class InetActiveBasePage : BasePage
    {
        #region Overridden Methods
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Forms Authentication
            HttpCookie oCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if(oCookie != null)
            {
                FormsAuthenticationTicket oTicket = FormsAuthentication.Decrypt(oCookie.Value);
                if(oTicket.Name != (string)Session["FormsAuthenticationUser"])
                {
                    HttpCookie oSessionCookie = Response.Cookies["ASP.NET_SessionId"];
                    oSessionCookie.Value = string.Empty;
                    Response.Cookies.Set(oSessionCookie);

                    Session.Abandon();
                    FormsAuthentication.SignOut();

                    Logoff();
                }
            }
            else if(GetAccessControl() == AccessControl.Private)
            {
                Logoff();
            }

            if(GetAccessControl() == AccessControl.Private)
            {
                // Ensure user has logged in
                if(!MemberProtect.CurrentUser.IsAuthorized)
                {
                    Response.Redirect("~/InetActive/Login.aspx");
                    return;
                }

                // Ensure user has appropriate privilege
                //if(!MemberProtect.CurrentUser.CheckPrivilege(ApplicationContext.GetOrganizationID(), "InetActiveManagement"))
                if(!MemberProtect.Utility.YesNoToBool(MemberProtect.CurrentUser.GetDataItem("IsAdmin")))
                {
                    RedirectMessage("~/InetActive/Login.aspx", "You do not have access.", MessageTone.Negative);
                    return;
                }

                // Ensure user is not locked out
                if(MemberProtect.CurrentUser.IsLocked)
                {
                    RedirectMessage("~/InetActive/Login.aspx", "Your account has been locked.", MessageTone.Negative);
                    return;
                }
            }
        }
        #endregion

        #region Public Methods
        public void Login(string sUsername)
        {
            FormsAuthentication.SetAuthCookie(sUsername, false);
            Session["FormsAuthenticationUser"] = sUsername;
        }

        public void Logoff()
        {
            // Remove Session cookie
            HttpCookie oSessionCookie = ApplicationContext.HttpContext.Response.Cookies["ASP.NET_SessionId"];
            oSessionCookie.Value = string.Empty;
            ApplicationContext.HttpContext.Response.Cookies.Set(oSessionCookie);

            MemberProtect.CurrentUser.Logoff();
            ApplicationContext.HttpContext.Session.Abandon();
            FormsAuthentication.SignOut();

            ApplicationContext.HttpContext.Response.Redirect("~/InetActive/Login.aspx");
        }
        #endregion

        #region Virtual Methods
        public virtual AccessControl GetAccessControl()
        {
            return AccessControl.Private;
        }
        #endregion

        #region Overridden Methods
        public override HtmlGenericControl GetBodyTag()
        {
            return ((InetActive)Master).BodyTag;
        }
        #endregion
    }
}