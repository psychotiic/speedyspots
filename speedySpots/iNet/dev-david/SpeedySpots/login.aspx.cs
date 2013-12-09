using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetSolution.Web;
using SpeedySpots.Objects;

namespace SpeedySpots
{
    public partial class login : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(Session["Legacy-Username"] != null)
                {
                    m_txtUsername.Text = (string)Session["Legacy-Username"];

                    if(Session["Legacy-Remember"] != null)
                    {
                        if((string)Session["Legacy-Remember"] == "1")
                        {
                            m_chkRemember.Checked = true;
                        }
                    }

                    Session.Remove("Legacy-Username");
                    Session.Remove("Legacy-Remember");
                }

                SetDefaultButton(m_txtUsername, m_btnLogin);
                SetDefaultButton(m_txtPassword, m_btnLogin);
                m_txtUsername.Focus();

                if(Request["username"] != null & Request["userpass"] != null)
                {
                    string sUsername = Request["username"];
                    string sPassword = Request["userpass"];

                    if(MemberProtect.Authentication.CheckPassword(sUsername, sPassword))
                    {
                        Session["Persist"] = true;
                        Session["LastLogonDateTime"] = MemberProtect.User.GetLastLoginDateTime(MemberProtect.User.GetUserID(m_txtUsername.Text));
                        MemberProtect.CurrentUser.Login(sUsername);

                        if(MemberProtect.CurrentUser.IsLocked)
                        {
                            MemberProtect.CurrentUser.Logoff();
                            RedirectMessage("~/login.aspx", "Your account has been locked. Call us for more information.", MessageTone.Negative);
                        }
                        else
                        {
                            if(Request["userremember"] != null)
                            {
                                string sRemember = Request["userremember"];

                                if(sRemember == "1")
                                {
                                    HttpCookie oCookie = new HttpCookie("InetActiveCookie");
                                    oCookie.Expires = DateTime.Now.AddDays(30);
                                    oCookie["Username"] = MemberProtect.Cryptography.Encrypt(sUsername);
                                    oCookie["Password"] = MemberProtect.Cryptography.Encrypt(sPassword);
                                    Response.Cookies.Add(oCookie);
                                }
                                else
                                {
                                    // Expire client cookie to remove it on the client
                                    Response.Cookies["InetActiveCookie"].Expires = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0, 0));
                                }
                            }

                            Login(m_txtUsername.Text);

                            ShowDashboard();
                        }
                    }
                    else
                    {
                        Session["Legacy-Username"] = sUsername;

                        if(Request["userremember"] != null)
                        {
                            string sRemember = Request["userremember"];

                            if(sRemember == "1")
                            {
                                Session["Legacy-Remember"] = "1";
                            }
                        }

                        ShowMessage("Username or password is incorrect, please try again.", MessageTone.Negative);
                    }
                }
                else if(Request.Cookies["InetActiveCookie"] != null)
                {
                    if(Session["Logoff"] == null)
                    {
                        // Perform silent login
                        if(Request.Cookies["InetActiveCookie"]["Username"] != null && Request.Cookies["InetActiveCookie"]["Password"] != null)
                        {
                            string sUsername = string.Empty;
                            string sPassword = string.Empty;

                            try
                            {
                                sUsername = MemberProtect.Cryptography.Decrypt(Request.Cookies["InetActiveCookie"]["Username"]);
                                sPassword = MemberProtect.Cryptography.Decrypt(Request.Cookies["InetActiveCookie"]["Password"]);
                            }
                            catch (Exception ex)
                            {
                                // Cookies are poorly formatted for some reason, let's expire it
                                HttpCookie rememberMe = Request.Cookies["InetActiveCookie"];
                                rememberMe.Expires = DateTime.Now.AddDays(-1);
                                Response.Cookies.Set(rememberMe);
                            }

                            if (!string.IsNullOrEmpty(sUsername) && !string.IsNullOrEmpty(sPassword) && MemberProtect.Authentication.CheckPassword(sUsername, sPassword))
                            {
                                Session["Persist"] = true;
                                Session["LastLogonDateTime"] = MemberProtect.User.GetLastLoginDateTime(MemberProtect.User.GetUserID(m_txtUsername.Text));
                                MemberProtect.CurrentUser.Login(sUsername);

                                if(MemberProtect.CurrentUser.IsLocked)
                                {
                                    MemberProtect.CurrentUser.Logoff();
                                    RedirectMessage("~/login.aspx", "Your account has been locked. Call us for more information.", MessageTone.Negative);
                                }
                                // Ensure user has appropriate privilege
                                // -Coming Later
                                else
                                {
                                    //Sliding window expiration
                                    HttpCookie rememberMe = Request.Cookies["InetActiveCookie"];
                                    rememberMe.Expires = DateTime.Now.AddDays(30);
                                    Response.Cookies.Set(rememberMe);

                                    Login(m_txtUsername.Text);

                                    ShowDashboard();
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
                    RedirectMessage("~/login.aspx", "Your account has been locked. Call us for more information.", MessageTone.Negative);
                }
                // Ensure user has appropriate privilege
                // -Coming Later
                else
                {
                    if(m_chkRemember.Checked)
                    {
                        HttpCookie oCookie = new HttpCookie("InetActiveCookie");
                        oCookie.Expires = DateTime.Now.AddDays(30);
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

                    ShowDashboard();
                }
            }
            else if(MemberProtect.Authentication.CheckTemporaryPassword(m_txtUsername.Text, m_txtPassword.Text))
            {
                Session["Persist"] = true;
                Session["LastLogonDateTime"] = MemberProtect.User.GetLastLoginDateTime(MemberProtect.User.GetUserID(m_txtUsername.Text));
                MemberProtect.CurrentUser.Login(m_txtUsername.Text);

                if(MemberProtect.CurrentUser.IsLocked)
                {
                    MemberProtect.CurrentUser.Logoff();
                    RedirectMessage("~/login.aspx", "Your account has been locked. Call us for more information.", MessageTone.Negative);
                }

                Login(m_txtUsername.Text);

                RedirectMessage("~/login-change-password.aspx", "Please change your password.");
            }
            else
            {
                RedirectMessage("~/login.aspx", "Username or password is incorrect, please try again.", MessageTone.Negative);
            }
        }

        #region Private Methods
        private void ShowDashboard()
        {
            if(Request.QueryString["ReturnUrl"] != null)
            {
                Response.Redirect(Request.QueryString["ReturnUrl"]);
            }

            RedirectMessage(string.Format("~/{0}", GetDashboardUrl()), "You've successfully logged in.", MessageTone.Positive);
        }
        #endregion

        #region Overridden Methods
        public override bool GetSSL()
        {
            return true;
        }

        public override List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Public);

            return oAccessControl;
        }
        #endregion
    }
}
