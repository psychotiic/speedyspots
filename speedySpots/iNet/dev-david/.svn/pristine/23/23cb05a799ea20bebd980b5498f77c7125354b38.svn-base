using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetSolution.MemberProtect;
using SpeedySpots.Objects;
using System.Reflection;
using Telerik.Web.UI;
using SpeedySpots.DataAccess;

namespace SpeedySpots
{
    public partial class Site : System.Web.UI.MasterPage
    {
        public Site()
        {
            ID = "Master";
            IsWhosOnInTrackOnlyMode = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                litVersionNumber.Text = string.Format(" | v{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());

                if (!MemberProtect.CurrentUser.IsAuthorized)
                {
                    divFeedbackform.Visible = false;
                }
            }

            divWhosOnBlock.Visible = Business.Settings.EnableWhosOn;
        }

        protected void AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            ((BasePage)Page).OnAjaxRequest(sender, e);
        }

        #region Public Properties
        public HtmlGenericControl BodyTag
        {
            get { return m_oBodyTag; }
        }

        public bool IsWhosOnInTrackOnlyMode { get; set; }
        
        public string WhosOnInTrackOnlyOutput 
        {
            get
            {
                return IsWhosOnInTrackOnlyMode ? "sWOResponse = '';" : string.Empty;
            }
        }

        public ApplicationContext ApplicationContext
        {
            get { return ((BasePage)Page).ApplicationContext; }
        }

        public DataAccessDataContext DataAccess
        {
            get { return ((BasePage)Page).DataAccess; }
        }

        public MemberProtect MemberProtect
        {
            get { return ((BasePage)Page).MemberProtect; }
        }

        public ScriptManager ScriptManager
        {
            get { return m_oScriptManager; } 
        }
        
        public fn_Message_GetNewResult NewMessage
        {
            get { return ApplicationContext.GetNewMessage(); }
        }

        public string RatesUrl
        {
            get
            {
                if(MemberProtect.CurrentUser.IsAuthorized)
                {
                    return ResolveUrl("~/rates-spots.aspx");
                }
                else
                {
                    return ResolveUrl("~/rates.aspx");
                }
            }
        }
        #endregion
    }
}