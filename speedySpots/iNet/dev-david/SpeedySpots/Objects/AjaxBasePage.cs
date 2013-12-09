using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace SpeedySpots.Objects
{
    public class AjaxBasePage : BasePage
    {
        public bool IsAuthenticated { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            IsAuthenticated = true;

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
            else if(!GetAccessControl().Contains(AccessControl.Public))
            {
                Logoff();
            }

            if (!GetAccessControl().Contains(AccessControl.Public))
            {
                // Ensure user has logged in
                if (!MemberProtect.CurrentUser.IsAuthorized)
                {
                    Logoff();
                    return;
                }

                // Ensure user has appropriate privilege
                if (ApplicationContext.IsAdmin)
                {
                    if (!ApplicationContext.IsStaff && !ApplicationContext.IsCustomer)
                    {
                        Logoff();
                        return;
                    }
                }
                else if (ApplicationContext.IsStaff)
                {
                    if (!GetAccessControl().Contains(AccessControl.Staff))
                    {
                        Logoff();
                        return;
                    }
                }
                else if (ApplicationContext.IsTalent)
                {
                    if (!GetAccessControl().Contains(AccessControl.Talent))
                    {
                        Logoff();
                        return;
                    }
                }
                else if (ApplicationContext.IsCustomer)
                {
                    if (!GetAccessControl().Contains(AccessControl.Customer))
                    {
                        Logoff();
                        return;
                    }
                }

                // Ensure user is not locked out
                if (MemberProtect.CurrentUser.IsLocked)
                {
                    Logoff();
                    return;
                }
            }
        }

        public void Logoff()
        {
            // Remove Session cookie
            HttpCookie oSessionCookie = ApplicationContext.HttpContext.Response.Cookies["ASP.NET_SessionId"];
            oSessionCookie.Value = string.Empty;
            ApplicationContext.HttpContext.Response.Cookies.Set(oSessionCookie);

            // Remove Authenticated cookie
            HttpCookie oAuthenticatedCookie = ApplicationContext.HttpContext.Response.Cookies["Authenticated"];
            oAuthenticatedCookie.Value = string.Empty;
            ApplicationContext.HttpContext.Response.Cookies.Set(oAuthenticatedCookie);

            // Remove Dashboard Url cookie
            HttpCookie oDashboardUrlCookie = ApplicationContext.HttpContext.Response.Cookies["DashboardUrl"];
            oDashboardUrlCookie.Value = string.Empty;
            ApplicationContext.HttpContext.Response.Cookies.Set(oDashboardUrlCookie);

            MemberProtect.CurrentUser.Logoff();
            ApplicationContext.HttpContext.Session.Abandon();
            FormsAuthentication.SignOut();

            IsAuthenticated = false;
        }

        #region Virtual Methods
        public virtual List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Admin);

            return oAccessControl;
        }
        #endregion

    }
}