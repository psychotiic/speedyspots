using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using InetSolution.Web;
using InetSolution.MemberProtect;
using SpeedySpots.DataAccess;
using SpeedySpots.Objects;
using Telerik.Web.UI;

namespace SpeedySpots.Objects
{
    public enum AccessControl
    {
        Admin,
        Staff,
        Talent,
        Customer,
        Public,
    }

    public class SiteBasePage : BasePage
    {
        #region Overridden Methods
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Forms Authentication
            HttpCookie oCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (oCookie != null && !string.IsNullOrEmpty(oCookie.Value))
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
                Logoff(Server.UrlEncode(HttpContext.Current.Request.RawUrl));
            }

            if(!GetAccessControl().Contains(AccessControl.Public))
            {
                // Ensure user has logged in
                if(!MemberProtect.CurrentUser.IsAuthorized)
                {
                    Response.Redirect("~/login.aspx");
                    return;
                }

                // Ensure user has appropriate privilege
                if(ApplicationContext.IsAdmin)
                {
                    if(!ApplicationContext.IsStaff && !ApplicationContext.IsCustomer)
                    {
                        RedirectMessage("~/default.aspx", "You do not have access to this page.", MessageTone.Negative);
                        return;
                    }
                }
                else if(ApplicationContext.IsStaff)
                {
                    if(!GetAccessControl().Contains(AccessControl.Staff))
                    {
                        RedirectMessage("~/default.aspx", "You do not have access to this page.", MessageTone.Negative);
                        return;
                    }
                }
                else if(ApplicationContext.IsTalent)
                {
                    if(!GetAccessControl().Contains(AccessControl.Talent))
                    {
                        RedirectMessage("~/default.aspx", "You do not have access to this page.", MessageTone.Negative);
                        return;
                    }
                }
                else if(ApplicationContext.IsCustomer)
                {
                    if(!GetAccessControl().Contains(AccessControl.Customer))
                    {
                        RedirectMessage("~/default.aspx", "You do not have access to this page.", MessageTone.Negative);
                        return;
                    }
                }

                // Ensure user is not locked out
                if(MemberProtect.CurrentUser.IsLocked)
                {
                    RedirectMessage("~/login.aspx", "Your account has been locked. Call us for more information.", MessageTone.Negative);
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
            
            HttpCookie oCookie = new HttpCookie("Authenticated", "True");
            oCookie.Expires = DateTime.Today.AddDays(1);
            Response.Cookies.Add(oCookie);

            string sUrl = GetDashboardUrl();
            HttpCookie oDashboardCookie = new HttpCookie("DashboardUrl", sUrl);
            oDashboardCookie.Expires = DateTime.Today.AddDays(1);
            Response.Cookies.Add(oDashboardCookie);
        }

        public void Logoff()
        {
            Logoff(string.Empty);
        }

        public void Logoff(string sRedirectUrl)
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

            if(sRedirectUrl == string.Empty)
            {
                ApplicationContext.HttpContext.Response.Redirect("~/login.aspx");
            }
            else
            {
                ApplicationContext.HttpContext.Response.Redirect(string.Format("~/login.aspx?ReturnUrl={0}", sRedirectUrl));
            }
        }
        #endregion

        #region Protected Methods
        protected string GetDashboardUrl()
        {
            string sUrl = string.Empty;

            if(ApplicationContext.IsCustomer)
            {
                sUrl = "user-dashboard.aspx";
            }
            else if(ApplicationContext.IsStaff)
            {
                if(MemberProtect.CurrentUser.GetDataItem("DefaultTab") == "In Production")
                {
                    sUrl = "staff-dashboard.aspx?filter=inproduction";
                }
                else
                {
                    sUrl = "staff-dashboard.aspx";
                }
            }
            else if(ApplicationContext.IsTalent)
            {
                sUrl = "talent-dashboard.aspx";
            }
            else
            {
                sUrl = "Default.aspx";
            }

            return sUrl;
        }
        #endregion

        #region Overridden Methods
        public override HtmlGenericControl GetBodyTag()
        {
            if(Master != null)
            {
                return ((Site)Master).BodyTag;
            }
            else
            {
                return null;
            }
        }
        #endregion

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