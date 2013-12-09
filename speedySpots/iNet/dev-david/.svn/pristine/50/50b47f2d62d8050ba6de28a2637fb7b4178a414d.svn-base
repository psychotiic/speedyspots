using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetSolution.Web;
using SpeedySpots.InetActive.Objects;
using InetSolution.Web.InetActive;

namespace SpeedySpots.InetActive
{
    using InetSolution.MemberProtect;

    public partial class Login : InetActiveBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                SetDefaultButton(m_txtUsername, m_btnLogin);
                SetDefaultButton(m_txtPassword, m_btnLogin);
                m_txtUsername.Focus();

                m_divNews.InnerHtml += new Rss().GetRecentArticles("http://feeds.feedburner.com/TurnLeftBlog", 5);

                if(Request.Cookies["InetActiveCookie"] != null)
                {
                    if(Session["Logoff"] == null)
                    {
                        // Perform silent login
                        if(Request.Cookies["InetActiveCookie"]["Username"] != null && Request.Cookies["InetActiveCookie"]["Password"] != null)
                        {
                            string sUsername = MemberProtect.Cryptography.Decrypt(Request.Cookies["InetActiveCookie"]["Username"]);
                            string sPassword = MemberProtect.Cryptography.Decrypt(Request.Cookies["InetActiveCookie"]["Password"]);

                            if(MemberProtect.Authentication.CheckPassword(sUsername, sPassword))
                            {
                                Session["Persist"] = true;
                                Session["LastLogonDateTime"] = MemberProtect.User.GetLastLoginDateTime(MemberProtect.User.GetUserID(m_txtUsername.Text));
                                MemberProtect.CurrentUser.Login(sUsername);

                                if(MemberProtect.CurrentUser.IsLocked)
                                {
                                    MemberProtect.CurrentUser.Logoff();
                                    RedirectMessage("~/InetActive/Login.aspx", "Your account has been locked.", MessageTone.Negative);
                                }
                                // Ensure user has appropriate privilege
                                else if(!MemberProtect.Utility.YesNoToBool(MemberProtect.CurrentUser.GetDataItem("IsAdmin")))
                                {
                                    MemberProtect.CurrentUser.Logoff();
                                    RedirectMessage("~/InetActive/Login.aspx", "You do not have access.", MessageTone.Negative);
                                }
                                else
                                {
                                    Login(m_txtUsername.Text);

                                    if(Session["LoginUrl"] != null)
                                    {
                                        Response.Redirect((string)Session["LoginUrl"]);
                                    }
                                    else
                                    {
                                        Response.Redirect("~/InetActive/Default.aspx");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void OnLogin(object sender, EventArgs e)
        {
            if(MemberProtect.Authentication.CheckPassword(m_txtUsername.Text, m_txtPassword.Text))
            {
                Session["Persist"] = true;
                Session["LastLogonDateTime"] = MemberProtect.User.GetLastLoginDateTime(MemberProtect.User.GetUserID(m_txtUsername.Text));
                MemberProtect.CurrentUser.Login(m_txtUsername.Text);

                if(MemberProtect.CurrentUser.IsLocked)
                {
                    MemberProtect.CurrentUser.Logoff();
                    RedirectMessage("~/InetActive/Login.aspx", "Your account has been locked.", MessageTone.Negative);
                }
                //else if(!MemberProtect.CurrentUser.CheckPrivilege(ApplicationContext.GetOrganizationID(), "InetActiveManagement"))
                else if(!MemberProtect.Utility.YesNoToBool(MemberProtect.CurrentUser.GetDataItem("IsAdmin")))
                {
                    MemberProtect.CurrentUser.Logoff();
                    RedirectMessage("~/InetActive/Login.aspx", "You do not have access.", MessageTone.Negative);
                }
                else
                {
                    if(m_chkRememberMe.Checked)
                    {
                        HttpCookie oCookie = new HttpCookie("InetActiveCookie");
                        oCookie.Expires = DateTime.Now.AddDays(7);
                        oCookie["Username"] = MemberProtect.Cryptography.Encrypt(m_txtUsername.Text);
                        oCookie["Password"] = MemberProtect.Cryptography.Encrypt(m_txtPassword.Text);
                        Response.Cookies.Add(oCookie);
                    }
                    else
                    {
                        // Expire client cookie to remove it on the client
                        Response.Cookies["InetActiveCookie"].Expires = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0, 0));
                    }

                    Login(m_txtUsername.Text);

                    RedirectMessage("~/InetActive/Default.aspx", "You've successfully logged in.", MessageTone.Positive);
                }
            }
            else
            {
                RedirectMessage("~/InetActive/Login.aspx", "Username or password is incorrect, please try again.", MessageTone.Negative);
            }
        }

        #region Overridden Methods
        public override AccessControl GetAccessControl()
        {
            return AccessControl.Public;
        }
        #endregion
    }
}