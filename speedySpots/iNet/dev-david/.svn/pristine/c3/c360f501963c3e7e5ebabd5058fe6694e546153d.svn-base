using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.InetActive.Objects;
using SpeedySpots.DataAccess;
using InetSolution.Web;
using Microsoft.Security.Application;
using Telerik.Web.UI;

namespace SpeedySpots.InetActive
{
    public partial class RoleEdit : InetActiveBasePage
    {
        private Guid                                m_oMPRoleID = Guid.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["MPRoleID"] != null)
            {
                m_oMPRoleID = (Guid)Session["MPRoleID"];
            }

            if(!IsPostBack)
            {
                if(m_oMPRoleID == Guid.Empty)
                {
                    m_txtName.Focus();
                    m_btnDelete.Visible = false;
                }
                else
                {
                    m_txtName.Text = MemberProtect.Security.Role.GetName(m_oMPRoleID);
                    m_txtDescription.Text = MemberProtect.Security.Role.GetDescription(m_oMPRoleID);
                }

                // Load Privileges
                m_treePrivileges.DataFieldParentID = "ParentID";
                m_treePrivileges.DataFieldID = "MPPrivilegeID";
                m_treePrivileges.DataTextField = "Name";
                m_treePrivileges.DataValueField = "Code";
                m_treePrivileges.DataSource = DataAccess.fn_InetActive_GetPrivileges();
                m_treePrivileges.DataBind();
            }
        }

        protected void OnPrivilegesDataBound(object sender, EventArgs e)
        {
            foreach(RadTreeNode oNode in m_treePrivileges.GetAllNodes())
            {
                if(oNode.Value == string.Empty)
                {
                    oNode.Checkable = false;
                    oNode.Expanded = true;
                }
                else
                {
                    if(MemberProtect.Security.Role.HasPrivilege(m_oMPRoleID, oNode.Value))
                    {
                        oNode.Checked = true;
                    }
                }
            }
        }

        protected void OnSave(object sender, EventArgs e)
        {
            // Create Role
            if(m_oMPRoleID == Guid.Empty)
            {
                m_oMPRoleID = MemberProtect.Security.Role.Add(m_txtName.Text, m_txtDescription.Text, ApplicationContext.GetOrganizationID());
            }

            // Save Information
            MemberProtect.Security.Role.SetName(m_oMPRoleID, m_txtName.Text);
            MemberProtect.Security.Role.SetDescription(m_oMPRoleID, m_txtDescription.Text);

            foreach(RadTreeNode oNode in m_treePrivileges.GetAllNodes())
            {
                if(oNode.Value != string.Empty)
                {
                    if(oNode.Checked)
                    {
                        if(!MemberProtect.Security.Role.HasPrivilege(m_oMPRoleID, oNode.Value))
                        {
                            MemberProtect.Security.Role.AddPrivilege(m_oMPRoleID, oNode.Value);
                        }
                    }
                    else
                    {
                        if(MemberProtect.Security.Role.HasPrivilege(m_oMPRoleID, oNode.Value))
                        {
                            MemberProtect.Security.Role.RemovePrivilege(m_oMPRoleID, oNode.Value);
                        }
                    }
                }
            }

            RedirectMessage("~/InetActive/RoleList.aspx", string.Format("Role '{0}' has been saved.", AntiXss.HtmlEncode(m_txtName.Text)), MessageTone.Positive);
        }

        protected void OnDelete(object sender, EventArgs e)
        {
            string sName = MemberProtect.Security.Role.GetName(m_oMPRoleID);
            MemberProtect.Security.Role.Remove(m_oMPRoleID);

            RedirectMessage("~/InetActive/RoleList.aspx", string.Format("Role '{0}' has been removed.", AntiXss.HtmlEncode(sName)), MessageTone.Positive);
        }
    }
}