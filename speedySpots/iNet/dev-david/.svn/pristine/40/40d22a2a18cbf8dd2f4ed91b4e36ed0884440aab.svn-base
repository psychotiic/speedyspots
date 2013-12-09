using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using InetSolution.Web;
using InetSolution.MemberProtect;
using SpeedySpots.DataAccess;
using SpeedySpots.Objects;
using Telerik.Web.UI;

namespace SpeedySpots.Controls
{
    public class SiteBaseControl : System.Web.UI.UserControl
    {
        #region Message Methods
        public void RedirectMessage(string sUrl, string sText)
        {
            ((BasePage)Page).RedirectMessage(sUrl, sText, MessageTone.Neutral);
        }

        public void RedirectMessage(string sUrl, string sText, MessageTone oTone)
        {
            ((BasePage)Page).RedirectMessage(sUrl, sText, oTone);
        }
        #endregion

        #region Public Properties
        public ApplicationContext ApplicationContext
        {
            get { return ((BasePage)Page).ApplicationContext; }
        }

        public MemberProtect MemberProtect
        {
            get { return ((BasePage)Page).MemberProtect; }
        }

        public DataAccessDataContext DataAccess
        {
            get { return ((BasePage)Page).DataAccess; }
        }

        public ScriptManager ScriptManager
        {
            get { return ((BasePage)Page).ScriptManager; }
        }
        #endregion

        #region Public Methods
        public string StripNewlineCharchters(string sContent)
        {
            return sContent.Replace("\r", "\n").Replace("\n\n", "\n").Replace("\n", string.Empty);
        }
        #endregion
    }
}