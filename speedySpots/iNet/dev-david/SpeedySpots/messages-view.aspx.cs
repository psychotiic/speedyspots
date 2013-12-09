using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetSolution.Web;
using SpeedySpots.Objects;
using SpeedySpots.DataAccess;

namespace SpeedySpots
{
    public partial class messages_view : SiteBasePage
    {
        private IAMessage                           m_oIAMessage = null;
        private IAMessageRecipient                  m_oIAMessageRecipient = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["id"] != null)
            {
                m_oIAMessage = DataAccess.IAMessages.SingleOrDefault(row => row.IAMessageID == MemberProtect.Utility.ValidateInteger(Request.QueryString["id"]));

                if(m_oIAMessage != null)
                {
                    // Admin + Staff can view details of any message
                    if(ApplicationContext.IsAdmin && ApplicationContext.IsStaff)
                    {
                        m_repeaterRecipients.DataSource = DataAccess.fn_Message_GetRecipients(m_oIAMessage.IAMessageID);
                        m_repeaterRecipients.DataBind();
                    }

                    m_oIAMessageRecipient = DataAccess.IAMessageRecipients.SingleOrDefault(row => row.MPUserID == MemberProtect.CurrentUser.UserID && row.IAMessageID == m_oIAMessage.IAMessageID);
                    if(m_oIAMessageRecipient != null)
                    {
                        if(!m_oIAMessageRecipient.IsAcknowledged)
                        {
                            m_oIAMessageRecipient.IsAcknowledged = true;
                            m_oIAMessageRecipient.AcknowledgedDateTime = DateTime.Now;
                            DataAccess.SubmitChanges();
                        }
                    }
                    else
                    {
                        // Admin + Staff can view details of any message
                        if(!(ApplicationContext.IsAdmin && ApplicationContext.IsStaff))
                        {
                            Response.Redirect("~/messages-inbox.aspx");
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/messages-inbox.aspx");
                }
            }
        }

        #region Public Properties
        public IAMessage IAMessage
        {
            get { return m_oIAMessage; }
        }

        public IAMessageRecipient IAMessageRecipient
        {
            get { return m_oIAMessageRecipient; }
        }
        #endregion

        #region Public Methods
        public string GetYesNo(string sValue)
        {
            if(sValue == "True")
            {
                return "Yes";
            }
            else
            {
                return "No";
            }
        }

        public string GetDate(DateTime oDateTime)
        {
            if(oDateTime.Year == 1950)
            {
                return "N/A";
            }
            else
            {
                return string.Format("{0:M/dd/yyyy a\\t h:mm tt}", oDateTime);
            }
        }
        #endregion

        #region Virtual Methods
        public override List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Admin);
            oAccessControl.Add(AccessControl.Customer);
            oAccessControl.Add(AccessControl.Staff);
            oAccessControl.Add(AccessControl.Talent);

            return oAccessControl;
        }
        #endregion
    }
}