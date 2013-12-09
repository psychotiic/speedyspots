using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Configuration;
using InetSolution.Web;
using InetSolution.MemberProtect;
using SpeedySpots.DataAccess;
using SpeedySpots.Objects;
using Telerik.Web.UI;

namespace SpeedySpots.Objects
{
    /// <summary>
    /// This is the 'Base Page' for all other pages in the site, it is a good idea to consolidate as much common functionality for all web pages in this class as it will
    /// be inhereited by all other pages.
    /// </summary>
    public class BasePage : System.Web.UI.Page
    {
        private ApplicationContext                  m_oApplicationContext = null;

        #region Overridden Methods
        protected override void OnInit(EventArgs e)
        {
            // Create application context
            m_oApplicationContext = new ApplicationContext(HttpContext.Current);

            // Create body tag ID for CSS styling purposes
            if(BodyTag != null)
            {
                BodyTag.ID = System.IO.Path.GetFileNameWithoutExtension(HttpContext.Current.Request.FilePath);
            }

            base.OnInit(e);

            // Perform SSL redirection if necessary
            if(IsProcessSSL())
            {
                RedirectSSL(GetSSL());
            }

            // Javascript Includes
            Page.ClientScript.RegisterClientScriptInclude("Common", Page.ResolveUrl("~/js/common.js"));
        }

        private void SetMessageDisplay(HtmlGenericControl oDivMessage)
        {
            string displayMode = "none";
            if (oDivMessage != null && Session["Message"] != null)
            {
                Message oMessage = Session["Message"] as Message;
                Session.Remove("Message");

                if (!string.IsNullOrWhiteSpace(oMessage.Text))
                {
                    oDivMessage.InnerHtml = oMessage.Text;
                    displayMode = "block";

                    switch (oMessage.Tone)
                    {
                        case MessageTone.Positive:
                            oDivMessage.Attributes["class"] = "message positive";
                            break;
                        case MessageTone.Neutral:
                            oDivMessage.Attributes["class"] = "message";
                            break;
                        case MessageTone.Negative:
                            oDivMessage.Attributes["class"] = "message negative";
                            break;
                        default:
                            displayMode = "none";
                            break;
                    }
                }
            }

            if (oDivMessage != null)
            {
                oDivMessage.Style.Add("display", displayMode);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            HtmlGenericControl oDivMessage = null;
            oDivMessage = (HtmlGenericControl)((Master != null) ? Master.FindControl("m_divMessage") : FindControl("m_divMessage"));
            SetMessageDisplay(oDivMessage);
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);

            if(m_oApplicationContext != null)
            {
                m_oApplicationContext.Dispose();
            }
        }
        #endregion

        #region Public Methods
        public void RedirectSSL(bool bSSL)
        {
            // Force SSL Redirection
            // - Ignore this logic if it's localhost
            System.Uri oCurrentUrl = System.Web.HttpContext.Current.Request.Url;
            if(!oCurrentUrl.IsLoopback)
            {
                if(bSSL)
                {
                    if(!oCurrentUrl.Scheme.Equals(Uri.UriSchemeHttps, StringComparison.CurrentCultureIgnoreCase))
                    {
                        System.UriBuilder oSecureUrlBuilder = new UriBuilder(oCurrentUrl);
                        oSecureUrlBuilder.Scheme = Uri.UriSchemeHttps;

                        int iPort = -1;                      

                        try
                        {
                            string sSSLPort = ConfigurationManager.AppSettings["SSLPort"];
                            iPort = int.Parse(sSSLPort);
                        }
                        catch
                        {
                            iPort = -1;
                        }

                        // Use the default port
                        oSecureUrlBuilder.Port = iPort;

                        // Redirect and end the response
                        System.Web.HttpContext.Current.Response.Redirect(oSecureUrlBuilder.Uri.ToString());
                    }
                }
                else // Non-SSL
                {
                    if(!oCurrentUrl.Scheme.Equals(Uri.UriSchemeHttp, StringComparison.CurrentCultureIgnoreCase))
                    {
                        System.UriBuilder oSecureUrlBuilder = new UriBuilder(oCurrentUrl);
                        oSecureUrlBuilder.Scheme = Uri.UriSchemeHttp;

                        int iPort = -1;

                        try
                        {
                            string sSSLPort = ConfigurationManager.AppSettings["NonSSLPort"];
                            iPort = int.Parse(sSSLPort);
                            iPort = (iPort == 80) ? -1 : iPort;
                        }
                        catch
                        {
                            iPort = -1;
                        }

                        // Use the default port
                        oSecureUrlBuilder.Port = iPort;

                        // Redirect and end the response
                        System.Web.HttpContext.Current.Response.Redirect(oSecureUrlBuilder.Uri.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Sets the default button for a given page. This will simply attach an 'onKeyPress' event to the button and automatically post the form to the server.
        /// </summary>
        /// <param name="oControl">The button control to set as default button.</param>
        public void SetDefaultButton(WebControl oSourceControl, WebControl oPostbackButton)
        {
            oSourceControl.Attributes.Add("onKeyPress", "if(event.keyCode == 13){ __doPostBack('" + oPostbackButton.UniqueID + "', ''); }");
        }

        public string FormatURLFriendlyGUID(Guid oGuid)
        {
            return oGuid.ToString().Replace("-", string.Empty);
        }

        public string FormatURLFriendlyGUID(string sGuid)
        {
            return sGuid.Replace("-", string.Empty);
        }

        public string StripNewlineCharchters(string sContent)
        {
            return sContent.Replace("\r", "\n").Replace("\n\n", "\n").Replace("\n", string.Empty);
        }
        #endregion

        #region Message Methods
        public void SetMessage(string sText)
        {
            SetMessage(sText, MessageTone.Neutral);
        }

        public void SetMessage(string sText, MessageTone oTone)
        {
            HtmlGenericControl oDivMessage = (HtmlGenericControl)Master.FindControl("m_divMessage");

            if(oDivMessage != null)
            {
                oDivMessage.InnerHtml = sText;

                if(oTone == MessageTone.Positive)
                {
                    oDivMessage.Attributes["class"] = "message positive";
                }
                else if(oTone == MessageTone.Neutral)
                {
                    oDivMessage.Attributes["class"] = "message";
                }
                else if(oTone == MessageTone.Negative)
                {
                    oDivMessage.Attributes["class"] = "message negative";
                }

                oDivMessage.Style.Add("display", "block");
            }
        }

        public void ShowMessage(string sText)
        {
            ShowMessage(sText, MessageTone.Neutral);
        }

        public void ShowMessage(string sText, MessageTone oTone)
        {
            if(Request.QueryString.Count > 0)
            {
                RedirectMessage(Request.ServerVariables["URL"] + "?" + Request.QueryString, sText, oTone);
            }
            else
            {
                RedirectMessage(Request.ServerVariables["URL"], sText, oTone);
            }
        }

        public void RedirectMessage(string sUrl, string sText)
        {
            RedirectMessage(sUrl, sText, MessageTone.Neutral);
        }

        public void RedirectMessage(string sUrl, string sText, MessageTone oTone)
        {
            Message oMessage = new Message(sText, oTone);
            Session["Message"] = oMessage;

            Response.Redirect(sUrl);
        }
        #endregion

        #region Virtual Methods
        public virtual HtmlGenericControl GetBodyTag()
        {
            return null;
        }

        public virtual bool GetSSL()
        {
            return false;
        }

        public virtual bool IsProcessSSL()
        {
            return true;
        }

        public virtual void OnAjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }
        #endregion

        #region Public Properties
        public ApplicationContext ApplicationContext
        {
            get { return m_oApplicationContext; }
        }

        public MemberProtect MemberProtect
        {
            get { return m_oApplicationContext.MemberProtect; }
        }

        public DataAccessDataContext DataAccess
        {
            get { return m_oApplicationContext.DataAccess; }
        }

        public ScriptManager ScriptManager
        {
            get { return ScriptManager; }
        }

        public HtmlGenericControl BodyTag
        {
            get { return GetBodyTag(); }
        }
        #endregion
    }
}