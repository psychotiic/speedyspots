using System;
using System.Data;
using System.Linq;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using InetSolution.Web;
using SpeedySpots.InetActive.Objects;
using Microsoft.Security.Application;
using Telerik.Web.UI;

namespace SpeedySpots.InetActive
{
    public partial class UserEdit : InetActiveBasePage
    {
        private Guid                                m_oMPUserID = Guid.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["MPUserID"] != null)
            {
                m_oMPUserID = (Guid)Session["MPUserID"];
            }

            if(!IsPostBack)
            {
                if(m_oMPUserID == Guid.Empty)
                {
                    m_txtUsername.Focus();
                    m_btnDelete.Visible = false;
                    m_oAuditInformation.Visible = false;
                }

                // Load Information
                if(m_oMPUserID != Guid.Empty)
                {
                    m_reqPassword.Enabled = false;

                    m_txtUsername.Text = MemberProtect.User.GetUsername(m_oMPUserID);
                    m_txtUsername.Enabled = false;

                    m_txtFirstName.Text = MemberProtect.User.GetDataItem(m_oMPUserID, "FirstName");
                    m_txtLastName.Text = MemberProtect.User.GetDataItem(m_oMPUserID, "LastName");
                    m_txtEmail.Text = MemberProtect.User.GetDataItem(m_oMPUserID, "Email");

                    m_chkLockedOut.Checked = MemberProtect.User.IsLocked(MemberProtect.User.GetUsername(m_oMPUserID));

                    // Audit Information
                    if(MemberProtect.User.GetCreatedDateTime(m_oMPUserID) != null)
                    {
                        m_lblCreatedOn.Text = string.Format("{0:g}", MemberProtect.User.GetCreatedDateTime(m_oMPUserID));
                    }
                    else
                    {
                        m_lblCreatedOn.Text = "Unknown";
                    }

                    m_lblFailedLoginAttempts.Text = string.Format("{0}", MemberProtect.User.GetFailedLoginAttempts(m_oMPUserID));

                    if(MemberProtect.User.GetIPAddress(MemberProtect.User.GetUsername(m_oMPUserID)) != string.Empty)
                    {
                        m_lblLastIPAddress.Text = MemberProtect.User.GetIPAddress(MemberProtect.User.GetUsername(m_oMPUserID));
                    }
                    else
                    {
                        m_lblLastIPAddress.Text = "Unknown";
                    }

                    if(MemberProtect.User.GetLastLoginDateTime(m_oMPUserID) != null)
                    {
                        m_lblLastLoginDate.Text = string.Format("{0:g}", MemberProtect.User.GetLastLoginDateTime(m_oMPUserID));
                    }
                    else
                    {
                        m_lblLastLoginDate.Text = "Unknown";
                    }

                    if(MemberProtect.User.GetLastFailedLoginDateTime(m_oMPUserID) != null)
                    {
                        m_lblLastFailedLoginDate.Text = string.Format("{0:g}", MemberProtect.User.GetLastFailedLoginDateTime(m_oMPUserID));
                    }
                    else
                    {
                        m_lblLastFailedLoginDate.Text = "Unknown";
                    }

                    if(MemberProtect.User.GetPasswordChangedDateTime(m_oMPUserID) != null)
                    {
                        m_lblPasswordChangedOn.Text = string.Format("{0:g}", MemberProtect.User.GetPasswordChangedDateTime(m_oMPUserID));
                    }
                    else
                    {
                        m_lblPasswordChangedOn.Text = "Unknown";
                    }
                }

                // Load Roles
                m_chkRoles.DataValueField = "MPRoleID";
                m_chkRoles.DataTextField = "Name";
                m_chkRoles.DataSource = DataAccess.fn_InetActive_GetRoles(ApplicationContext.GetOrganizationID()).OrderBy(row => row.Name);
                m_chkRoles.DataBind();
            }
        }

        protected void OnRolesDataBound(object sender, EventArgs e)
        {
            foreach(ListItem oItem in m_chkRoles.Items)
            {
                if(MemberProtect.User.HasRole(m_oMPUserID, MemberProtect.Utility.ValidateGuid(oItem.Value)))
                {
                    oItem.Selected = true;
                }
            }
        }

        protected void OnSave(object sender, EventArgs e)
        {
            // Create User
            if(m_oMPUserID == Guid.Empty)
            {
                if(MemberProtect.User.Exists(m_txtUsername.Text))
                {
                    ShowMessage("Username already exists, please try again.", MessageTone.Negative);
                    return;
                }

                m_oMPUserID = MemberProtect.User.Add(m_txtUsername.Text, m_txtPassword.Text);
            }
            else if(m_txtPassword.Text != string.Empty)
            {
                MemberProtect.User.ChangePassword(m_oMPUserID, m_txtPassword.Text);
            }

            // Save Information
            MemberProtect.User.SetLockedStatus(MemberProtect.User.GetUsername(m_oMPUserID), m_chkLockedOut.Checked);
            MemberProtect.User.SetDataItem(m_oMPUserID, "FirstName", m_txtFirstName.Text);
            MemberProtect.User.SetDataItem(m_oMPUserID, "LastName", m_txtLastName.Text);
            MemberProtect.User.SetDataItem(m_oMPUserID, "Email", m_txtEmail.Text);

            foreach(ListItem oItem in m_chkRoles.Items)
            {
                Guid oRoleID = MemberProtect.Utility.ValidateGuid(oItem.Value);

                if(oItem.Selected)
                {
                    if(!MemberProtect.User.HasRole(m_oMPUserID, oRoleID))
                    {
                        MemberProtect.User.AssignRole(m_oMPUserID, oRoleID);
                    }
                }
                else
                {
                    if(MemberProtect.User.HasRole(m_oMPUserID, oRoleID))
                    {
                        MemberProtect.User.UnassignRole(m_oMPUserID, oRoleID);
                    }
                }
            }

            RedirectMessage("~/InetActive/UserList.aspx", string.Format("User '{0}' has been saved.", AntiXss.HtmlEncode(m_txtUsername.Text)), MessageTone.Positive);
        }

        protected void OnDelete(object sender, EventArgs e)
        {
            string sUsername = MemberProtect.User.GetUsername(m_oMPUserID);
            MemberProtect.User.Remove(m_oMPUserID);

            RedirectMessage("~/InetActive/UserList.aspx", string.Format("User '{0}' has been removed.", AntiXss.HtmlEncode(sUsername)), MessageTone.Positive);
        }
    }
}