using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Configuration;
using InetSolution.MemberProtect;
using SpeedySpots.DataAccess;
using SpeedySpots.InetActive.Objects;
using SpeedySpots.InetActive.Masters;

namespace SpeedySpots.InetActive.Objects
{
    public enum AccessControl
    {
        Private,
        Public,
    }

    public class BasePage : System.Web.UI.Page
    {
        private MemberProtect                       m_oMemberProtect = null;
        private SpeedySpotsDataContext              m_oDataAccess = new SpeedySpotsDataContext();

        #region Overridden Methods
        protected override void OnInit(EventArgs e)
        {
            MemberProtectSettings oSettings = new MemberProtectSettings(ConfigurationManager.ConnectionStrings["SpeedySpotsConnectionString"].ConnectionString, Session.SessionID);
            m_oMemberProtect = new MemberProtect(oSettings);

            if(BodyTag != null)
            {
                BodyTag.ID = System.IO.Path.GetFileNameWithoutExtension(HttpContext.Current.Request.FilePath);
            }

            base.OnInit(e);

            if(GetAccessControl() == AccessControl.Private)
            {
                // Ensure user has logged in
                if(!MemberProtect.CurrentUser.IsAuthorized)
                {
                    Response.Redirect("~/InetActive/login.aspx");
                    return;
                }

                // Ensure user is not locked out
                if(MemberProtect.CurrentUser.IsLocked)
                {
                    RedirectMessage("~/InetActive/login.aspx", "Your account has been locked.", MessageTone.Negative);
                    return;
                }
            }

            // Javascript Includes
            Page.ClientScript.RegisterClientScriptInclude("Common", Page.ResolveUrl("~/InetActive/scripts/common.js"));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            HtmlGenericControl oDivMessage = (HtmlGenericControl)Master.FindControl("m_divMessage");

            if(Session["Message"] != null)
            {
                Message oMessage = Session["Message"] as Message;

                oDivMessage.InnerHtml = oMessage.Text;

                if(oMessage.Tone == MessageTone.Positive)
                {
                    oDivMessage.Attributes["class"] = "message positive";
                }
                else if(oMessage.Tone == MessageTone.Neutral)
                {
                    oDivMessage.Attributes["class"] = "message";
                }
                else if(oMessage.Tone == MessageTone.Negative)
                {
                    oDivMessage.Attributes["class"] = "message negative";
                }

                oDivMessage.Style.Add("display", "block");

                Session.Remove("Message");
            }
            else
            {
                oDivMessage.Style.Add("display", "none");
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);

            if(m_oMemberProtect != null)
            {
                m_oMemberProtect.Dispose();
                m_oMemberProtect = null;
            }
        }
        #endregion

        #region Public Methods
        public void SetMessage(string sText)
        {
            SetMessage(sText, MessageTone.Neutral);
        }

        public void SetMessage(string sText, MessageTone oTone)
        {
            Message oMessage = new Message(sText, oTone);
            Session["Message"] = oMessage;
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
        public virtual AccessControl GetAccessControl()
        {
            return AccessControl.Private;
        }
        #endregion

        #region Public Properties
        public MemberProtect MemberProtect
        {
            get { return m_oMemberProtect; }
        }

        public SpeedySpotsDataContext DataAccess
        {
            get { return m_oDataAccess; }
        }

        public HtmlGenericControl BodyTag
        {
            get
            {
                if(Master != null)
                {
                    return ((Masters.Master)Master).BodyTag;
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion
    }
}
