using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.Objects;
using SpeedySpots.DataAccess;
using Telerik.Web.UI;

namespace SpeedySpots
{
    public partial class company_modify : SiteBasePage
    {
        private Guid                                m_oMPOrgID = Guid.Empty;
        private int                                 m_iUserCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["id"] != null)
            {
                if(ApplicationContext.IsAdmin || ApplicationContext.IsStaff)
                {
                    m_oMPOrgID = MemberProtect.Utility.ValidateGuid(Request.QueryString["id"]);
                }
            }

            if(!IsPostBack)
            {
                if(MemberProtect.Utility.YesNoToBool(MemberProtect.Organization.GetDataItem(m_oMPOrgID, "IsArchived")))
                {
                    m_btnArchive.Visible = false;
                }
                else
                {
                    m_btnReactivate.Visible = false;
                }

                ApplicationContext.LoadCountries(ref m_cboCountry);
                ApplicationContext.LoadCountries(ref m_cboBillingCountry);
                m_radPayFirst.SelectedValue = "Y";

                if (m_oMPOrgID != Guid.Empty)
                {
                    m_txtCompanyID.Text = ApplicationContext.GetCompanyID(m_oMPOrgID).ToString();
                    m_txtCompanyName.Text = MemberProtect.Organization.GetName(m_oMPOrgID);
                    m_txtAddressLine1.Text = MemberProtect.Organization.GetDataItem(m_oMPOrgID, "Address1");
                    m_txtAddressLine2.Text = MemberProtect.Organization.GetDataItem(m_oMPOrgID, "Address2");
                    m_txtCity.Text = MemberProtect.Organization.GetDataItem(m_oMPOrgID, "City");
                    m_cboState.SelectedValue = MemberProtect.Organization.GetDataItem(m_oMPOrgID, "State");
                    m_cboCountry.SelectedValue = MemberProtect.Organization.GetDataItem(m_oMPOrgID, "Country");
                    m_txtZip.Text = MemberProtect.Organization.GetDataItem(m_oMPOrgID, "Zip");
                    m_txtCompanyPhone.Text = MemberProtect.Organization.GetDataItem(m_oMPOrgID, "Phone");
                    m_txtFax.Text = MemberProtect.Organization.GetDataItem(m_oMPOrgID, "Fax");
                    m_txtEmailInvoice.Text = MemberProtect.Organization.GetDataItem(m_oMPOrgID, "EmailInvoice");
                    m_txtBillingAddress1.Text = MemberProtect.Organization.GetDataItem(m_oMPOrgID, "BillingAddress1");
                    m_txtBillingAddress2.Text = MemberProtect.Organization.GetDataItem(m_oMPOrgID, "BillingAddress2");
                    m_txtBillingCity.Text = MemberProtect.Organization.GetDataItem(m_oMPOrgID, "BillingCity");
                    m_cboBillingState.SelectedValue = MemberProtect.Organization.GetDataItem(m_oMPOrgID, "BillingState");
                    m_cboBillingCountry.SelectedValue = MemberProtect.Organization.GetDataItem(m_oMPOrgID, "BillingCountry");
                    m_txtBillingZip.Text = MemberProtect.Organization.GetDataItem(m_oMPOrgID, "BillingZip");
                    m_txtBillingName.Text = MemberProtect.Organization.GetDataItem(m_oMPOrgID, "BillingName");
                    m_txtBillingPhone.Text = MemberProtect.Organization.GetDataItem(m_oMPOrgID, "BillingPhone");
                    m_radPayFirst.SelectedValue = MemberProtect.Organization.GetDataItem(m_oMPOrgID, "IsPayFirst");
                    m_chkIsVerified.Checked = MemberProtect.Utility.YesNoToBool(MemberProtect.Organization.GetDataItem(m_oMPOrgID, "IsVerified"));
                    m_txtNotes.Content = MemberProtect.Organization.GetDataItem(m_oMPOrgID, "Notes");

                    MPOrg oMPOrg = DataAccess.MPOrgs.SingleOrDefault(row => row.MPOrgID == m_oMPOrgID);
                    if (oMPOrg != null)
                    {
                        m_lblCreatedDateTime.Text = oMPOrg.CreatedOn.ToString("d");
                    }
                }

                m_oRepeaterUsers.DataSource = DataAccess.fn_Admin_GetUsers(m_oMPOrgID);
                m_oRepeaterUsers.DataBind();
            }

            if(IsNew)
            {
                m_lblCreatedDateTime.Text = DateTime.Now.ToString("d");
            }
            else
            {
                m_iUserCount = DataAccess.fn_Admin_GetUsers(m_oMPOrgID).Count();
                m_divIsBillingSame.Visible = false;
            }
        }

        protected void OnSave(object sender, EventArgs e)
        {
            if(m_oMPOrgID == Guid.Empty)
            {
                Guid oOrgTypeID = MemberProtect.Organization.GetTypeID("Client");
                m_oMPOrgID = MemberProtect.Organization.Add(m_txtCompanyName.Text, oOrgTypeID);
                MemberProtect.Organization.SetDataItem(m_oMPOrgID, "IsArchived", MemberProtect.Utility.BoolToYesNo(false));
            }

            MemberProtect.Organization.SetName(m_oMPOrgID, m_txtCompanyName.Text);
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "Address1", m_txtAddressLine1.Text);
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "Address2", m_txtAddressLine2.Text);
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "City", m_txtCity.Text);
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "State", m_cboState.SelectedValue);
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "Country", m_cboCountry.SelectedValue);
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "Zip", m_txtZip.Text);
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "Phone", m_txtCompanyPhone.Text);
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "Fax", m_txtFax.Text);
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "EmailInvoice", ApplicationContext.CleanAddressList(m_txtEmailInvoice.Text));
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "BillingAddress1", m_txtBillingAddress1.Text);
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "BillingAddress2", m_txtBillingAddress2.Text);
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "BillingCity", m_txtBillingCity.Text);
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "BillingState", m_cboBillingState.SelectedValue);
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "BillingCountry", m_cboBillingCountry.SelectedValue);
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "BillingZip", m_txtBillingZip.Text);
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "BillingName", m_txtBillingName.Text);
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "BillingPhone", m_txtBillingPhone.Text);
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "IsPayFirst", m_radPayFirst.SelectedValue);
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "IsVerified", MemberProtect.Utility.BoolToYesNo(m_chkIsVerified.Checked));
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "Notes", m_txtNotes.Content);

            RedirectMessage(string.Format("~/company-modify.aspx?id={0}", m_oMPOrgID), "Company information saved.", InetSolution.Web.MessageTone.Positive);
        }

        protected void OnAddUser(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/user-account.aspx?mode=new&cid={0}", m_oMPOrgID));
        }

        protected void OnBack(object sender, EventArgs e)
        {
            Response.Redirect("~/companies.aspx");
        }

        protected void OnArchive(object sender, EventArgs e)
        {
            // Archive all users in the company
            foreach(MPOrgUser oMPOrgUser in DataAccess.MPOrgUsers.Where(row => row.MPOrgID == m_oMPOrgID))
            {
                ApplicationContext.ArchiveUser(oMPOrgUser.MPUserID);
            }

            // Archive the company
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "IsArchived", MemberProtect.Utility.BoolToYesNo(true));

            m_btnArchive.Visible = false;
            m_btnReactivate.Visible = true;

            m_oRepeaterUsers.DataSource = DataAccess.fn_Admin_GetUsers(m_oMPOrgID);
            m_oRepeaterUsers.DataBind();
        }

        protected void OnReactivate(object sender, EventArgs e)
        {
            MemberProtect.Organization.SetDataItem(m_oMPOrgID, "IsArchived", MemberProtect.Utility.BoolToYesNo(false));

            m_btnArchive.Visible = true;
            m_btnReactivate.Visible = false;
        }

        #region Public Properties
        public string FormatIsArchived(string sValue)
        {
            if(MemberProtect.Utility.YesNoToBool(sValue))
            {
                return string.Format(" - <span style='color: red;'>Archived</span>");
            }
            else
            {
                return string.Empty;
            }
        }

        public bool IsNew
        {
            get
            {
                if(m_oMPOrgID == Guid.Empty)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public int UserCount
        {
            get { return m_iUserCount; }
        }
        #endregion

        #region Overridden Methods
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