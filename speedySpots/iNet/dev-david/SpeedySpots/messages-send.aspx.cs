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
    public partial class messages_send : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void OnSend(object sender, EventArgs e)
        {
            DateTime oStartDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
            DateTime oEndDateTime = new DateTime(2100, 1, 1, 0, 0, 0, 0);

            // Validation
            if(m_dtStartDateTime.SelectedDate.HasValue && m_dtEndDateTime.SelectedDate.HasValue)
            {
                oStartDateTime = m_dtStartDateTime.SelectedDate.Value;
                oEndDateTime = m_dtEndDateTime.SelectedDate.Value;

                if(oStartDateTime.CompareTo(oEndDateTime) > 0)
                {
                    SetMessage("Start Date/Time must come before the Stop Date/Time.", MessageTone.Negative);
                    return;
                }
            }

            List<Guid> assignedIndividuals = GetIndividualUsers();
            if (m_chkGroups.SelectedValue == string.Empty && assignedIndividuals.Count() <= 0)
            {
                SetMessage("Please select group(s) or specific user(s) to send your message to.", MessageTone.Negative);
                return;
            }

            // Create message
            IAMessage oIAMessage = new IAMessage();
            oIAMessage.MPUserID = MemberProtect.CurrentUser.UserID;
            oIAMessage.DisplayStartDateTime = oStartDateTime;
            oIAMessage.DisplayEndDateTime = oEndDateTime;
            oIAMessage.Subject = m_txtSubject.Text;
            oIAMessage.Body = m_txtMessage.Content;
            oIAMessage.CreatedDateTime = DateTime.Now;
            DataAccess.IAMessages.InsertOnSubmit(oIAMessage);
            DataAccess.SubmitChanges();

            // Assign users to message recipients
            IQueryable<MPUserData> oMPUsers = null;
            List<IAMessageRecipient> oReceipients = new List<IAMessageRecipient>();
            if(m_chkGroups.SelectedValue != string.Empty)
            {
                foreach(ListItem oItem in m_chkGroups.Items)
                {
                    if(oItem.Selected)
                    {
                        if(oItem.Value == "Staff")
                        {
                            oMPUsers = DataAccess.MPUserDatas.Where(row => row.IsStaff == "Y" && row.IsArchived == "N");
                        }
                        else if(oItem.Value == "Talent")
                        {
                            oMPUsers = DataAccess.MPUserDatas.Where(row => row.IsTalent == "Y" && row.IsArchived == "N");
                        }
                        else if(oItem.Value == "Customers")
                        {
                            oMPUsers = DataAccess.MPUserDatas.Where(row => row.IsCustomer == "Y" && row.IsArchived == "N");
                        }

                        foreach(MPUserData oMPUserData in oMPUsers)
                        {
                            IAMessageRecipient oIAMessageRecipient = new IAMessageRecipient();
                            oIAMessageRecipient.IAMessageID = oIAMessage.IAMessageID;
                            oIAMessageRecipient.MPUserID = oMPUserData.MPUserID;
                            oIAMessageRecipient.IsAcknowledged = false;
                            oIAMessageRecipient.AcknowledgedDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
                            oReceipients.Add(oIAMessageRecipient);
                        }
                    }
                }

                if(oReceipients.Count > 0)
                {
                    DataAccess.IAMessageRecipients.InsertAllOnSubmit(oReceipients);
                    DataAccess.SubmitChanges();
                }
            }

            // Process individual users
            if (assignedIndividuals.Count > 0)
            {
                foreach (Guid assignedUserID in assignedIndividuals)
                {
                    if (DataAccess.IAMessageRecipients.Count(row => row.IAMessageID == oIAMessage.IAMessageID && row.MPUserID == assignedUserID) == 0)
                    {
                        IAMessageRecipient oIAMessageRecipient = new IAMessageRecipient();
                        oIAMessageRecipient.IAMessageID = oIAMessage.IAMessageID;
                        oIAMessageRecipient.MPUserID = assignedUserID;
                        oIAMessageRecipient.IsAcknowledged = false;
                        oIAMessageRecipient.AcknowledgedDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
                        DataAccess.IAMessageRecipients.InsertOnSubmit(oIAMessageRecipient);
                        DataAccess.SubmitChanges();
                    }
                }
            }

            RedirectMessage("~/messages-inbox.aspx", "Message has been sent!", MessageTone.Positive);
        }

        private List<Guid> GetIndividualUsers()
        {
            List<Guid> assignedUsers = new List<Guid>();

            if (!string.IsNullOrEmpty(m_hdnUsers.Value))
            {
                string[] values = m_hdnUsers.Value.Split(',');

                for (int index = 0; index < values.Count(); index++)
                {
                    if (!string.IsNullOrEmpty(values[index]))
                    {
                        Guid value = MemberProtect.Utility.ValidateGuid(values[index]);
                        if(value != Guid.Empty)
                            assignedUsers.Add(value);
                    }
                }
            }

            return assignedUsers;
        }

        #region Virtual Methods
        public override List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Admin);
            oAccessControl.Add(AccessControl.Staff);

            return oAccessControl;
        }
        #endregion
    }
}