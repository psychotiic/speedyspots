using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.InetActive.Objects;
using InetSolution.Web;
using Microsoft.Security.Application;

namespace SpeedySpots.InetActive
{
    public partial class OrganizationEdit : InetActiveBasePage
    {
        private Guid                                m_oMPOrgID = Guid.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["MPOrgID"] != null)
            {
                m_oMPOrgID = (Guid)Session["MPOrgID"];
            }

            if(!IsPostBack)
            {
                if(m_oMPOrgID == Guid.Empty)
                {
                    m_txtName.Focus();
                    m_btnDelete.Visible = false;
                }

                // Load Information
                if(m_oMPOrgID != Guid.Empty)
                {
                    m_txtName.Text = MemberProtect.Organization.GetName(m_oMPOrgID);
                    m_txtAddress1.Text = MemberProtect.Organization.GetDataItem(m_oMPOrgID, "Address1");
                    m_txtAddress2.Text = MemberProtect.Organization.GetDataItem(m_oMPOrgID, "Address2");
                    m_txtCity.Text = MemberProtect.Organization.GetDataItem(m_oMPOrgID, "City");
                    m_cboState.SelectedValue = MemberProtect.Organization.GetDataItem(m_oMPOrgID, "State");
                    m_txtZip.Text = MemberProtect.Organization.GetDataItem(m_oMPOrgID, "Zip");
                    m_txtPhone.Text = MemberProtect.Organization.GetDataItem(m_oMPOrgID, "Phone");
                }
            }
        }

        protected void OnSave(object sender, EventArgs e)
        {
            // Create Organization
            if(m_oMPOrgID == Guid.Empty)
            {
                m_oMPOrgID = MemberProtect.Organization.Add(m_txtName.Text, MemberProtect.Organization.GetTypeID("Default"));
            }

            // Save Information
            MemberProtect.Organization.SetName(m_oMPOrgID, m_txtName.Text);
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "Address1", m_txtAddress1.Text);
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "Address2", m_txtAddress2.Text);
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "City", m_txtCity.Text);
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "State", m_cboState.SelectedValue);
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "Zip", m_txtZip.Text);
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "Phone", m_txtPhone.Text);

            RedirectMessage("~/InetActive/OrganizationList.aspx", string.Format("Organization '{0}' has been saved.", AntiXss.HtmlEncode(m_txtName.Text)), MessageTone.Positive);
        }

        protected void OnDelete(object sender, EventArgs e)
        {
            string sName = MemberProtect.Organization.GetName(m_oMPOrgID);
            MemberProtect.Organization.Remove(m_oMPOrgID);

            RedirectMessage("~/InetActive/OrganizationList.aspx", string.Format("Organization '{0}' has been removed.", AntiXss.HtmlEncode(sName)), MessageTone.Positive);
        }
    }
}